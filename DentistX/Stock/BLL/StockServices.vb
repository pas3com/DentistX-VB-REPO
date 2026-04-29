Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports Dapper

Module StockBLL3
    Public Class PurchaseService
        ''' <summary>Command timeout in seconds for buy-invoice save.</summary>
        Private Const BuyInvoiceCommandTimeoutSeconds As Integer = 120

        Private ReadOnly _connectionString As String

        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub

        'Kept for other callers, but simplified to only touch BuyInvoices and BuyInvoiceLineItems.
        Public Async Function CreateBuyInvoiceAsync(invoice As BuyInvoice, lineItems As IEnumerable(Of BuyInvoiceLineItem)) As Task(Of Integer)
            Dim total = lineItems.Sum(Function(li) li.Quantity * li.UnitPrice)
            invoice.TotalAmount = total
            If String.IsNullOrWhiteSpace(invoice.InvoiceStatus) Then
                invoice.InvoiceStatus = "Unpaid"
            End If
            If invoice.CreatedDate Is Nothing Then
                invoice.CreatedDate = DateTime.Now
            End If

            Using conn As New SqlConnection(_connectionString)
                Await conn.OpenAsync()
                Using tx = conn.BeginTransaction()
                    Try
                        Dim invId = Await conn.QuerySingleAsync(Of Integer)(
                            "INSERT INTO BuyInvoices (SupplierID, InvoiceDate, TotalAmount, DueDate, InvoiceStatus, CreatedDate)
                             VALUES (@SupplierID, @InvoiceDate, @TotalAmount, @DueDate, @InvoiceStatus, @CreatedDate);
                             SELECT CAST(SCOPE_IDENTITY() AS int);",
                            invoice, tx, commandTimeout:=BuyInvoiceCommandTimeoutSeconds)

                        For Each li In lineItems
                            li.InvoiceID = invId
                            Await conn.ExecuteAsync(
                                "INSERT INTO BuyInvoiceLineItems (InvoiceID, ProductID, Quantity, UnitPrice, BatchNumber, ExpirationDate)
                                 VALUES (@InvoiceID, @ProductID, @Quantity, @UnitPrice, @BatchNumber, @ExpirationDate);",
                                li, tx, commandTimeout:=BuyInvoiceCommandTimeoutSeconds)
                        Next

                        tx.Commit()
                        Return invId
                    Catch
                        tx.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Synchronous version used by the UI save process – also only touches BuyInvoices and BuyInvoiceLineItems.
        ''' </summary>
        Public Function CreateBuyInvoice(invoice As BuyInvoice, lineItems As IEnumerable(Of BuyInvoiceLineItem)) As Integer
            Dim total = lineItems.Sum(Function(li) li.Quantity * li.UnitPrice)
            invoice.TotalAmount = total
            If String.IsNullOrWhiteSpace(invoice.InvoiceStatus) Then
                invoice.InvoiceStatus = "Unpaid"
            End If
            If invoice.CreatedDate Is Nothing Then
                invoice.CreatedDate = DateTime.Now
            End If

            Using conn As New SqlConnection(_connectionString)
                conn.Open()
                Using tx = conn.BeginTransaction()
                    Try
                        Dim invId = conn.QuerySingle(Of Integer)(
                            "INSERT INTO BuyInvoices (SupplierID, InvoiceDate, TotalAmount, DueDate, InvoiceStatus, CreatedDate)
                             VALUES (@SupplierID, @InvoiceDate, @TotalAmount, @DueDate, @InvoiceStatus, @CreatedDate);
                             SELECT CAST(SCOPE_IDENTITY() AS int);",
                            invoice, tx, BuyInvoiceCommandTimeoutSeconds)

                        For Each li In lineItems
                            li.InvoiceID = invId
                            conn.Execute(
                                "INSERT INTO BuyInvoiceLineItems (InvoiceID, ProductID, Quantity, UnitPrice, BatchNumber, ExpirationDate)
                                 VALUES (@InvoiceID, @ProductID, @Quantity, @UnitPrice, @BatchNumber, @ExpirationDate);",
                                li, tx, BuyInvoiceCommandTimeoutSeconds)
                        Next

                        tx.Commit()
                        Return invId
                    Catch
                        tx.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function
    End Class

    Public Class PaymentService
        Private ReadOnly _connectionString As String
        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub

        Private Async Function RefreshBuyInvoiceStatusAsync(conn As SqlConnection, tx As SqlTransaction, invoiceId As Integer) As Task
            Dim totalInvoice = Await conn.QuerySingleAsync(Of Decimal)(
                "SELECT TotalAmount FROM BuyInvoices WHERE InvoiceID=@InvoiceID", New With {.InvoiceID = invoiceId}, tx)
            Dim totalPaid = Await conn.QuerySingleAsync(Of Decimal)(
                "SELECT ISNULL(SUM(Amount),0) FROM Payments WHERE InvoiceID=@InvoiceID", New With {.InvoiceID = invoiceId}, tx)

            Dim newStatus As String
            If totalPaid >= totalInvoice Then
                newStatus = "Paid"
            ElseIf totalPaid > 0 Then
                newStatus = "Partial"
            Else
                newStatus = "Unpaid"
            End If

            Await conn.ExecuteAsync(
                "UPDATE BuyInvoices SET InvoiceStatus=@InvoiceStatus WHERE InvoiceID=@InvoiceID",
                New With {.InvoiceStatus = newStatus, .InvoiceID = invoiceId}, tx)
        End Function

        Public Async Function AddPaymentAsync(payment As Payment) As Task(Of Integer)
            payment.PaymentMethod = PaymentMethodStorage.NormalizeForPaymentsTable(payment.PaymentMethod)
            Using conn As New SqlConnection(_connectionString)
                Await conn.OpenAsync()
                Using tx = conn.BeginTransaction()
                    Try
                        Dim newPaymentId = Await conn.QuerySingleAsync(Of Integer)(
                            "INSERT INTO Payments (InvoiceID, SupplierID, Amount, PaymentDate, PaymentMethod,
                                ChqOwner, AccountNumber, ChqNumber, ChqDueDate, ChqBank)
                             OUTPUT INSERTED.PaymentID
                             VALUES (@InvoiceID, @SupplierID, @Amount, @PaymentDate, @PaymentMethod,
                                @ChqOwner, @AccountNumber, @ChqNumber, @ChqDueDate, @ChqBank)",
                            payment, tx)

                        If newPaymentId <= 0 Then
                            Throw New InvalidOperationException("Could not read new PaymentID after insert.")
                        End If

                        Await RefreshBuyInvoiceStatusAsync(conn, tx, payment.InvoiceID)

                        tx.Commit()
                        Return newPaymentId
                    Catch
                        tx.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function

        Public Async Function UpdatePaymentAsync(payment As Payment) As Task
            payment.PaymentMethod = PaymentMethodStorage.NormalizeForPaymentsTable(payment.PaymentMethod)
            If payment.PaymentID <= 0 Then Throw New ArgumentException("PaymentID required.", NameOf(payment))
            Using conn As New SqlConnection(_connectionString)
                Await conn.OpenAsync()
                Using tx = conn.BeginTransaction()
                    Try
                        Await conn.ExecuteAsync(
                            "UPDATE Payments
                             SET InvoiceID=@InvoiceID, SupplierID=@SupplierID, Amount=@Amount, PaymentDate=@PaymentDate, PaymentMethod=@PaymentMethod,
                                 ChqOwner=@ChqOwner, AccountNumber=@AccountNumber, ChqNumber=@ChqNumber, ChqDueDate=@ChqDueDate, ChqBank=@ChqBank
                             WHERE PaymentID=@PaymentID",
                            payment, tx)

                        Await RefreshBuyInvoiceStatusAsync(conn, tx, payment.InvoiceID)

                        tx.Commit()
                    Catch
                        tx.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function
    End Class


    Public Class StockService
        Private ReadOnly _connectionString As String
        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub

        Public Async Function AdjustStockAsync(productId As Integer, quantityChange As Integer, notes As String) As Task
            Using conn As New SqlConnection(_connectionString)
                Await conn.ExecuteAsync(
                    "INSERT INTO StockMovements (BatchID, ProductID, QuantityChange, MovementType, Notes)
                     VALUES (NULL, @ProductID, @QuantityChange, @MovementType, @Notes);",
                    New With {
                        .ProductID = productId,
                        .QuantityChange = quantityChange,
                        .MovementType = "Adjustment",
                        .Notes = notes
                    })
            End Using
        End Function
    End Class
End Module
