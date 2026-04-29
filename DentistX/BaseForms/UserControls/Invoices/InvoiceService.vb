Imports System.Data
Imports System.Data.SqlClient
Imports Dapper

Public Class InvoiceTotals
    Public Property SubTotal As Decimal
    Public Property DiscountAmount As Decimal
    Public Property TaxAmount As Decimal
    Public Property TotalBeforePayments As Decimal
    Public Property TotalPayments As Decimal
    Public Property BalanceDue As Decimal
    Public Property SelectedTreatments As Integer
    Public Property SelectedPayments As Integer
End Class

Public Class InvoiceSaveResult
    Public Property InvoiceId As Integer
    Public Property InvoiceNumber As String
    Public Property Totals As InvoiceTotals
End Class

Public Class InvoiceService
    Private ReadOnly _connectionString As String

    Public Sub New(connectionString As String)
        _connectionString = connectionString
    End Sub

    Public Function GetPatients(Optional specificPatientID As Integer = 0) As List(Of KeyValuePair(Of Integer, String))
        Using conn As New SqlConnection(_connectionString)
            If specificPatientID > 0 Then
                Dim patient = conn.Query(Of Patient)(
                    "SELECT PatientID, PatientName FROM Patient WHERE PatientID = @PatientID",
                    New With {.PatientID = specificPatientID}).FirstOrDefault()
                If patient Is Nothing Then
                    Return New List(Of KeyValuePair(Of Integer, String))()
                End If
                Return New List(Of KeyValuePair(Of Integer, String)) From {
                    New KeyValuePair(Of Integer, String)(patient.PatientID, patient.PatientName)
                }
            End If

            Dim patients = conn.Query(Of Patient)(
                "SELECT PatientID, PatientName FROM Patient ORDER BY PatientName").ToList()
            Return patients.Select(Function(p) New KeyValuePair(Of Integer, String)(p.PatientID, p.PatientName)).ToList()
        End Using
    End Function

    Public Function GetTreatments(patientId As Integer) As List(Of TreatmentItem)
        Const query As String = "
            SELECT 
                pt.TrtID,
                pt.Detail,
                pt.TrtDate,
                CASE 
                    WHEN EXISTS(SELECT 1 FROM Patient_Trts pt2 WHERE pt2.TrtValue > 0 AND pt2.PatientID = pt.PatientID) 
                    THEN pt.TrtValue
                END AS TrtValue,
                ISNULL(pt.Discount, 0) AS Discount,
                CASE 
                    WHEN EXISTS(SELECT 1 FROM Invoice_Items ii WHERE ii.TrtID = pt.TrtID) THEN 1 
                    ELSE 0 
                END AS IsInvoiced
            FROM Patient_Trts pt
            WHERE pt.PatientID = @PatientID AND pt.TrtValue > 0
            ORDER BY pt.TrtDate DESC"

        Using conn As New SqlConnection(_connectionString)
            Dim rows = conn.Query(query, New With {.PatientID = patientId}).ToList()
            Dim results As New List(Of TreatmentItem)
            For Each row In rows
                results.Add(New TreatmentItem With {
                    .TrtID = row.TrtID,
                    .Detail = CStr(row.Detail),
                    .TrtDate = CDate(row.TrtDate),
                    .TrtValue = CDec(row.TrtValue),
                    .Discount = CDec(row.Discount),
                    .IsInvoiced = CInt(row.IsInvoiced) = 1,
                    .IsSelected = False
                })
            Next
            Return results
        End Using
    End Function

    Public Function GetPayments(patientId As Integer) As List(Of PaymentItem)
        Const query As String = "
            SELECT 
                pp.PayID,
                pp.PayType,
                pp.Notes,
                pp.PayDate,
                CASE 
                    WHEN EXISTS(SELECT 1 FROM Patient_Pays pp2 WHERE pp2.PayValue > 0 AND pp2.PatientID = pp.PatientID) 
                    THEN pp.PayValue
                END AS PayValue,
                CASE 
                    WHEN EXISTS(SELECT 1 FROM Invoice_Payments ii WHERE ii.PayID = pp.PayID) THEN 1 
                    ELSE 0 
                END AS IsInvoiced
            FROM Patient_Pays pp
            WHERE pp.PatientID = @PatientID AND pp.PayValue > 0
            ORDER BY pp.PayDate DESC"

        Using conn As New SqlConnection(_connectionString)
            Dim rows = conn.Query(query, New With {.PatientID = patientId}).ToList()
            Dim results As New List(Of PaymentItem)
            For Each row In rows
                results.Add(New PaymentItem With {
                    .PayID = row.PayID,
                    .PayType = CStr(row.PayType),
                    .Notes = CStr(row.Notes),
                    .PayDate = CDate(row.PayDate),
                    .PayValue = CDec(row.PayValue),
                    .IsInvoiced = CInt(row.IsInvoiced) = 1,
                    .IsSelected = False
                })
            Next
            Return results
        End Using
    End Function

    Public Function CalculateTotals(treatments As IEnumerable(Of TreatmentItem),
                                   payments As IEnumerable(Of PaymentItem),
                                   includeTax As Boolean,
                                   taxRate As Decimal) As InvoiceTotals
        Dim totals As New InvoiceTotals()
        If treatments IsNot Nothing Then
            For Each t In treatments
                If t.IsSelected Then
                    totals.SubTotal += t.TrtValue
                    totals.DiscountAmount += t.Discount
                    totals.SelectedTreatments += 1
                End If
            Next
        End If

        If payments IsNot Nothing Then
            For Each p In payments
                If p.IsSelected Then
                    totals.TotalPayments += p.PayValue
                    totals.SelectedPayments += 1
                End If
            Next
        End If

        totals.TaxAmount = (totals.SubTotal - totals.DiscountAmount) * taxRate
        If includeTax Then
            totals.TotalBeforePayments = (totals.SubTotal - totals.DiscountAmount) + totals.TaxAmount
        Else
            totals.TotalBeforePayments = (totals.SubTotal - totals.DiscountAmount)
        End If
        totals.BalanceDue = totals.TotalBeforePayments - totals.TotalPayments
        Return totals
    End Function

    Public Function GetNextInvoiceNumber() As String
        Dim datePart As String = Date.Today.ToString("yyyyMMdd")
        Dim sequence As Integer = 1
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim query As String = "
                SELECT COUNT(*) + 1 AS NextNumber 
                FROM Invoices 
                WHERE InvoiceNumber LIKE 'INV-" & datePart & "-%'"
            Dim result = conn.ExecuteScalar(query)
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                sequence = CInt(result)
            End If
        End Using
        Return $"INV-{datePart}-{sequence:000}"
    End Function

    Public Function CreateInvoice(patientId As Integer,
                                  invoiceDate As Date,
                                  dueDate As Date,
                                  notes As String,
                                  includeTax As Boolean,
                                  taxRate As Decimal,
                                  selectedTreatments As IEnumerable(Of TreatmentItem),
                                  selectedPayments As IEnumerable(Of PaymentItem),
                                  allocatedByUserId As Integer) As InvoiceSaveResult
        If selectedTreatments Is Nothing OrElse Not selectedTreatments.Any(Function(t) t.IsSelected) Then
            Throw New InvalidOperationException("No treatments selected.")
        End If

        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Using tx = conn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim invoiceNumber = GenerateInvoiceNumberLocked(conn, tx)
                    Dim totals = CalculateTotals(selectedTreatments, selectedPayments, includeTax, taxRate)

                    Dim insertInvoiceQuery As String = "
                        INSERT INTO Invoices 
                        (InvoiceNumber, PatientID, InvoiceDate, DueDate, InvoiceStatus, 
                         SubTotal, TaxAmount, DiscountAmount, TotalAmount, AmountPaid, BalanceDue, Notes, CreatedDate)
                        VALUES 
                        (@InvoiceNumber, @PatientID, @InvoiceDate, @DueDate, 1, 
                         @SubTotal, @TaxAmount, @DiscountAmount, @TotalAmount, 0, @TotalAmount, @Notes, GETDATE())
                        SELECT SCOPE_IDENTITY()"

                    Dim invoiceID As Integer = CInt(conn.ExecuteScalar(insertInvoiceQuery, New With {
                        .InvoiceNumber = invoiceNumber,
                        .PatientID = patientId,
                        .InvoiceDate = invoiceDate,
                        .DueDate = dueDate,
                        .SubTotal = totals.SubTotal,
                        .TaxAmount = totals.TaxAmount,
                        .DiscountAmount = totals.DiscountAmount,
                        .TotalAmount = totals.TotalBeforePayments,
                        .Notes = If(String.IsNullOrEmpty(notes), DBNull.Value, notes)
                    }, tx))

                    Dim insertItemQuery As String = "
                        INSERT INTO Invoice_Items 
                        (InvoiceID, TrtID, ItemDescription, Quantity, UnitPrice, Discount, TaxRate, LineTotal)
                        VALUES 
                        (@InvoiceID, @TrtID, @ItemDescription, 1, @UnitPrice, @Discount, @TaxRate, @LineTotal)"

                    For Each treatment In selectedTreatments.Where(Function(t) t.IsSelected)
                        conn.Execute(insertItemQuery, New With {
                            .InvoiceID = invoiceID,
                            .TrtID = treatment.TrtID,
                            .ItemDescription = treatment.Detail,
                            .UnitPrice = treatment.TrtValue,
                            .Discount = treatment.Discount,
                            .TaxRate = taxRate,
                            .LineTotal = treatment.TrtValue - treatment.Discount
                        }, tx)
                    Next

                    Dim insertHistoryQuery As String = "
                        INSERT INTO Invoice_History 
                        (InvoiceID, OldStatus, NewStatus, ChangeDate, ChangeReason)
                        VALUES 
                        (@InvoiceID, NULL, 1, GETDATE(), 'Invoice Created')"
                    conn.Execute(insertHistoryQuery, New With {.InvoiceID = invoiceID}, tx)

                    If selectedPayments IsNot Nothing AndAlso selectedPayments.Any(Function(p) p.IsSelected) Then
                        Dim insertAllocationQuery As String = "
                            INSERT INTO Invoice_Payments 
                            (InvoiceID, PayID, AllocatedAmount, AllocationDate, AllocatedBy, Notes)
                            VALUES 
                            (@InvoiceID, @PayID, @AllocatedAmount, GETDATE(), @AllocatedBy, @Notes)"
                        Dim paymentAmount As Decimal = 0

                        For Each payment In selectedPayments.Where(Function(p) p.IsSelected)
                            conn.Execute(insertAllocationQuery, New With {
                                .InvoiceID = invoiceID,
                                .PayID = payment.PayID,
                                .AllocatedAmount = payment.PayValue,
                                .AllocatedBy = allocatedByUserId,
                                .Notes = payment.Notes
                            }, tx)
                            paymentAmount += payment.PayValue
                        Next

                        Dim updateInvoiceQuery As String = "
                            UPDATE Invoices 
                            SET 
                                AmountPaid = AmountPaid + @PaymentAmount,
                                BalanceDue = BalanceDue - @PaymentAmount,
                                InvoiceStatus = CASE 
                                    WHEN (BalanceDue - @PaymentAmount) <= 0 THEN 2
                                    WHEN (BalanceDue - @PaymentAmount) < TotalAmount THEN 3
                                    ELSE InvoiceStatus 
                                END,
                                ModifiedDate = GETDATE()
                            WHERE InvoiceID = @InvoiceID"
                        conn.Execute(updateInvoiceQuery, New With {.PaymentAmount = paymentAmount, .InvoiceID = invoiceID}, tx)

                        Dim insertHistoryQuery2 As String = "
                            INSERT INTO Invoice_History 
                            (InvoiceID, OldStatus, NewStatus, ChangeDate, ChangeReason)
                            SELECT 
                                @InvoiceID,
                                InvoiceStatus,
                                CASE 
                                    WHEN (BalanceDue - @PaymentAmount) <= 0 THEN 2
                                    WHEN (BalanceDue - @PaymentAmount) < TotalAmount THEN 3
                                    ELSE InvoiceStatus 
                                END,
                                GETDATE(),
                                @ChangeReason
                            FROM Invoices 
                            WHERE InvoiceID = @InvoiceID"
                        conn.Execute(insertHistoryQuery2, New With {
                            .InvoiceID = invoiceID,
                            .PaymentAmount = paymentAmount,
                            .ChangeReason = $"Payment of {paymentAmount.ToCurrencyString(CurrencyType.ILS_Ar)} received "
                        }, tx)
                    End If

                    tx.Commit()
                    Return New InvoiceSaveResult With {.InvoiceId = invoiceID, .InvoiceNumber = invoiceNumber, .Totals = totals}
                Catch
                    tx.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Function

    Private Function GenerateInvoiceNumberLocked(conn As SqlConnection, tx As SqlTransaction) As String
        Dim datePart As String = Date.Today.ToString("yyyyMMdd")
        Dim sequence As Integer = 1
        Dim query As String = "
            SELECT COUNT(*) + 1 AS NextNumber 
            FROM Invoices WITH (UPDLOCK, HOLDLOCK)
            WHERE InvoiceNumber LIKE 'INV-" & datePart & "-%'"
        Dim result = conn.ExecuteScalar(query, transaction:=tx)
        If result IsNot Nothing AndAlso Not IsDBNull(result) Then
            sequence = CInt(result)
        End If
        Return $"INV-{datePart}-{sequence:000}"
    End Function
End Class
