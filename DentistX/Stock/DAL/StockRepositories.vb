Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports System.Linq
Imports Dapper

Module StockDAL

    Public Class SupplierRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetAll() As IEnumerable(Of Supplier)
            Dim sql = "SELECT * FROM Suppliers ORDER BY SupplierName"
            Return _db.Query(Of Supplier)(sql)
        End Function

        Public Function GetById(id As Integer) As Supplier
            Dim sql = "SELECT * FROM Suppliers WHERE SupplierID = @SupplierID"
            Return _db.Query(Of Supplier)(sql, New With {.SupplierID = id}).FirstOrDefault()
        End Function

        Public Async Function InsertAsync(s As Supplier) As Task(Of Integer)
            Dim sql = "
                INSERT INTO Suppliers (SupplierName, ContactPerson, PhoneNumber, EmailAddress, PhysicalAddress, PaymentTerms, WhatsAppPrefix, WhatsApp)
                VALUES (@SupplierName, @ContactPerson, @PhoneNumber, @EmailAddress, @PhysicalAddress, @PaymentTerms, @WhatsAppPrefix, @WhatsApp);
                SELECT CAST(SCOPE_IDENTITY() AS int);"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, s)
        End Function

        Public Async Function UpdateAsync(s As Supplier) As Task
            Dim sql = "
                UPDATE Suppliers
                SET SupplierName=@SupplierName, ContactPerson=@ContactPerson, PhoneNumber=@PhoneNumber,
                    EmailAddress=@EmailAddress, PhysicalAddress=@PhysicalAddress, PaymentTerms=@PaymentTerms,
                    WhatsAppPrefix=@WhatsAppPrefix, WhatsApp=@WhatsApp
                WHERE SupplierID=@SupplierID"
            Await _db.ExecuteAsync(sql, s)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM Suppliers WHERE SupplierID=@SupplierID"
            Await _db.ExecuteAsync(sql, New With {.SupplierID = id})
        End Function
    End Class

    Public Class CategoryRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetAll() As IEnumerable(Of Category)
            Dim sql = "SELECT * FROM Categories ORDER BY CategoryName"
            Return _db.Query(Of Category)(sql)
        End Function

        Public Async Function InsertAsync(c As Category) As Task(Of Integer)
            Dim sql = "
                INSERT INTO Categories (CategoryName)
                VALUES (@CategoryName);
                SELECT CAST(SCOPE_IDENTITY() AS int);"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, c)
        End Function

        Public Async Function UpdateAsync(c As Category) As Task
            Dim sql = "UPDATE Categories SET CategoryName=@CategoryName WHERE CategoryID=@CategoryID"
            Await _db.ExecuteAsync(sql, c)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM Categories WHERE CategoryID=@CategoryID"
            Await _db.ExecuteAsync(sql, New With {.CategoryID = id})
        End Function
    End Class

    Public Class UnitRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetAll() As IEnumerable(Of Unit)
            Dim sql = "SELECT * FROM Units ORDER BY UnitName"
            Return _db.Query(Of Unit)(sql)
        End Function

        Public Async Function InsertAsync(u As Unit) As Task(Of Integer)
            Dim sql = "
                INSERT INTO Units (UnitName)
                VALUES (@UnitName);
                SELECT CAST(SCOPE_IDENTITY() AS int);"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, u)
        End Function

        Public Async Function UpdateAsync(u As Unit) As Task
            Dim sql = "UPDATE Units SET UnitName=@UnitName WHERE UnitID=@UnitID"
            Await _db.ExecuteAsync(sql, u)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM Units WHERE UnitID=@UnitID"
            Await _db.ExecuteAsync(sql, New With {.UnitID = id})
        End Function
    End Class

    Public Class ProductRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetAll() As IEnumerable(Of Product)
            Dim sql = "
                SELECT p.*, c.CategoryName, u.UnitName
                FROM Products p
                JOIN Categories c ON p.CategoryID = c.CategoryID
                JOIN Units u ON p.UnitID = u.UnitID
                ORDER BY p.ProductName"
            Return _db.Query(Of Product)(sql)
        End Function

        Public Function GetById(id As Integer) As Product
            Dim sql = "SELECT * FROM Products WHERE ProductID=@ProductID"
            Return _db.Query(Of Product)(sql, New With {.ProductID = id}).FirstOrDefault()
        End Function

        Public Async Function InsertAsync(p As Product) As Task(Of Integer)
            Dim sql = "
                INSERT INTO Products (ProductName, ProductDescription, UnitID, CategoryID, ReorderLevel)
                VALUES (@ProductName, @ProductDescription, @UnitID, @CategoryID, @ReorderLevel);
                SELECT CAST(SCOPE_IDENTITY() AS int);"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, p)
        End Function

        Public Async Function UpdateAsync(p As Product) As Task
            Dim sql = "
                UPDATE Products
                SET ProductName=@ProductName, ProductDescription=@ProductDescription,
                    UnitID=@UnitID, CategoryID=@CategoryID, ReorderLevel=@ReorderLevel
                WHERE ProductID=@ProductID"
            Await _db.ExecuteAsync(sql, p)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM Products WHERE ProductID=@ProductID"
            Await _db.ExecuteAsync(sql, New With {.ProductID = id})
        End Function
    End Class

    Public Class BarcodeRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetByProduct(productId As Integer) As IEnumerable(Of Barcode)
            Dim sql = "SELECT * FROM Barcodes WHERE ProductID=@ProductID"
            Return _db.Query(Of Barcode)(sql, New With {.ProductID = productId})
        End Function

        Public Async Function InsertAsync(b As Barcode) As Task(Of Integer)
            Dim sql = "
                INSERT INTO Barcodes (ProductID, BarcodeValue, BarcodeType)
                VALUES (@ProductID, @BarcodeValue, @BarcodeType);
                SELECT CAST(SCOPE_IDENTITY() AS int);"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, b)
        End Function

        Public Async Function UpdateAsync(b As Barcode) As Task
            Dim sql = "
                UPDATE Barcodes
                SET ProductID=@ProductID, BarcodeValue=@BarcodeValue, BarcodeType=@BarcodeType
                WHERE BarcodeID=@BarcodeID"
            Await _db.ExecuteAsync(sql, b)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM Barcodes WHERE BarcodeID=@BarcodeID"
            Await _db.ExecuteAsync(sql, New With {.BarcodeID = id})
        End Function
    End Class

    Public Class BatchRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetByProduct(productId As Integer) As IEnumerable(Of Batch)
            Dim sql = "SELECT * FROM Batchess WHERE ProductID=@ProductID ORDER BY ExpirationDate"
            Return _db.Query(Of Batch)(sql, New With {.ProductID = productId})
        End Function

        Public Async Function GetByBarcodeAsync(barcode As String) As Task(Of BatchDetailsDto)
            Dim sql = "
                SELECT b.*, p.ProductName, s.SupplierName
                FROM Batchess b
                JOIN Products p ON b.ProductID = p.ProductID
                JOIN Suppliers s ON b.SupplierID = s.SupplierID
                WHERE b.Barcode = @Barcode"
            Dim results = Await _db.QueryAsync(Of BatchDetailsDto)(sql, New With {.Barcode = barcode})
            Return results.FirstOrDefault()
        End Function

        Public Async Function InsertAsync(b As Batch) As Task(Of Integer)
            Dim sql = "
                INSERT INTO Batchess (ProductID, SupplierID, BatchNumber, ManufacturingDate, ExpirationDate, ReceivedQuantity, CurrentQuantity, Barcode)
                VALUES (@ProductID, @SupplierID, @BatchNumber, @ManufacturingDate, @ExpirationDate, @ReceivedQuantity, @CurrentQuantity, @Barcode);
                SELECT CAST(SCOPE_IDENTITY() AS int);"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, b)
        End Function

        Public Async Function UpdateAsync(b As Batch) As Task
            Dim sql = "
                UPDATE Batchess
                SET ProductID=@ProductID, SupplierID=@SupplierID, BatchNumber=@BatchNumber,
                    ManufacturingDate=@ManufacturingDate, ExpirationDate=@ExpirationDate,
                    ReceivedQuantity=@ReceivedQuantity, CurrentQuantity=@CurrentQuantity, Barcode=@Barcode
                WHERE BatchID=@BatchID"
            Await _db.ExecuteAsync(sql, b)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM Batchess WHERE BatchID=@BatchID"
            Await _db.ExecuteAsync(sql, New With {.BatchID = id})
        End Function

        Public Async Function UpdateQuantityAsync(batchId As Integer, newQty As Integer) As Task
            Dim sql = "UPDATE Batchess SET CurrentQuantity=@CurrentQuantity WHERE BatchID=@BatchID"
            Await _db.ExecuteAsync(sql, New With {.CurrentQuantity = newQty, .BatchID = batchId})
        End Function
    End Class

    Public Class BuyInvoiceRepo
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetById(id As Integer) As BuyInvoice
            Dim sql = "
                SELECT i.*, s.SupplierName
                FROM BuyInvoices i
                JOIN Suppliers s ON i.SupplierID = s.SupplierID
                WHERE i.InvoiceID=@InvoiceID"
            Return _db.Query(Of BuyInvoice)(sql, New With {.InvoiceID = id}).FirstOrDefault()
        End Function

        Public Function GetBySupplier(supplierId As Integer) As IEnumerable(Of BuyInvoice)
            Dim sql = "
                SELECT i.*, s.SupplierName
                FROM BuyInvoices i
                JOIN Suppliers s ON i.SupplierID = s.SupplierID
                WHERE i.SupplierID=@SupplierID
                ORDER BY i.InvoiceDate DESC"
            Return _db.Query(Of BuyInvoice)(sql, New With {.SupplierID = supplierId})
        End Function

        Public Function GetOpenInvoices() As IEnumerable(Of BuyInvoice)
            Dim sql = "
                SELECT i.*, s.SupplierName
                FROM BuyInvoices i
                JOIN Suppliers s ON i.SupplierID = s.SupplierID
                WHERE i.InvoiceStatus <> 'Paid'
                ORDER BY i.InvoiceDate DESC"
            Return _db.Query(Of BuyInvoice)(sql)
        End Function

        ''' <summary>All invoices for all suppliers, optionally in date range. For account statement "All suppliers".</summary>
        Public Function GetAllInvoices(Optional fromDate As Date? = Nothing, Optional toDate As Date? = Nothing) As IEnumerable(Of BuyInvoice)
            Dim sql = "
                SELECT i.*, s.SupplierName
                FROM BuyInvoices i
                JOIN Suppliers s ON i.SupplierID = s.SupplierID"
            If fromDate.HasValue AndAlso toDate.HasValue Then
                sql &= " WHERE i.InvoiceDate >= @FromDate AND i.InvoiceDate <= @ToDate"
            End If
            sql &= " ORDER BY i.InvoiceDate DESC"
            If fromDate.HasValue AndAlso toDate.HasValue Then
                Return _db.Query(Of BuyInvoice)(sql, New With {.FromDate = fromDate.Value, .ToDate = toDate.Value})
            End If
            Return _db.Query(Of BuyInvoice)(sql)
        End Function

        Public Async Function InsertAsync(inv As BuyInvoice) As Task(Of Integer)
            Dim sql = "
                INSERT INTO BuyInvoices (SupplierID, InvoiceDate, TotalAmount, DueDate, InvoiceStatus, CreatedDate)
                VALUES (@SupplierID, @InvoiceDate, @TotalAmount, @DueDate, @InvoiceStatus, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() AS int);"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, inv)
        End Function

        Public Async Function UpdateAsync(inv As BuyInvoice) As Task
            Dim sql = "
                UPDATE BuyInvoices
                SET SupplierID=@SupplierID, InvoiceDate=@InvoiceDate, TotalAmount=@TotalAmount,
                    DueDate=@DueDate, InvoiceStatus=@InvoiceStatus, CreatedDate=@CreatedDate
                WHERE InvoiceID=@InvoiceID"
            Await _db.ExecuteAsync(sql, inv)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM BuyInvoices WHERE InvoiceID=@InvoiceID"
            Await _db.ExecuteAsync(sql, New With {.InvoiceID = id})
        End Function

        Public Async Function UpdateStatusAsync(invoiceId As Integer, status As String, totalAmount As Decimal) As Task
            Dim sql = "
                UPDATE BuyInvoices
                SET InvoiceStatus=@InvoiceStatus, TotalAmount=@TotalAmount
                WHERE InvoiceID=@InvoiceID"
            Await _db.ExecuteAsync(sql, New With {.InvoiceStatus = status, .TotalAmount = totalAmount, .InvoiceID = invoiceId})
        End Function
    End Class

    Public Class BuyInvoiceLineItemRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetByInvoice(invoiceId As Integer) As IEnumerable(Of BuyInvoiceLineItem)
            Dim sql = "
                SELECT li.*, p.ProductName
                FROM BuyInvoiceLineItems li
                JOIN Products p ON li.ProductID = p.ProductID
                WHERE li.InvoiceID=@InvoiceID"
            Return _db.Query(Of BuyInvoiceLineItem)(sql, New With {.InvoiceID = invoiceId})
        End Function

        Public Async Function InsertAsync(li As BuyInvoiceLineItem) As Task(Of Integer)
            Dim sql = "
                INSERT INTO BuyInvoiceLineItems (InvoiceID, ProductID, Quantity, UnitPrice, BatchNumber, ExpirationDate)
                VALUES (@InvoiceID, @ProductID, @Quantity, @UnitPrice, @BatchNumber, @ExpirationDate);
                SELECT CAST(SCOPE_IDENTITY() AS int);"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, li)
        End Function

        Public Async Function UpdateAsync(li As BuyInvoiceLineItem) As Task
            Dim sql = "
                UPDATE BuyInvoiceLineItems
                SET InvoiceID=@InvoiceID, ProductID=@ProductID, Quantity=@Quantity,
                    UnitPrice=@UnitPrice, BatchNumber=@BatchNumber, ExpirationDate=@ExpirationDate
                WHERE LineItemID=@LineItemID"
            Await _db.ExecuteAsync(sql, li)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM BuyInvoiceLineItems WHERE LineItemID=@LineItemID"
            Await _db.ExecuteAsync(sql, New With {.LineItemID = id})
        End Function
    End Class

    Public Class PaymentRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetByInvoice(invoiceId As Integer) As IEnumerable(Of Payment)
            Dim sql = "
                SELECT p.*, s.SupplierName
                FROM Payments p
                JOIN BuyInvoices i ON p.InvoiceID = i.InvoiceID
                JOIN Suppliers s ON s.SupplierID = ISNULL(p.SupplierID, i.SupplierID)
                WHERE p.InvoiceID=@InvoiceID
                ORDER BY p.PaymentDate"
            Dim list = _db.Query(Of Payment)(sql, New With {.InvoiceID = invoiceId}).ToList()
            For Each p In list
                p.PaymentMethod = PaymentMethodStorage.NormalizeForPaymentsTable(p.PaymentMethod)
            Next
            Return list
        End Function

        Public Function GetTotalPaid(invoiceId As Integer) As Decimal
            Dim sql = "SELECT ISNULL(SUM(Amount), 0) FROM Payments WHERE InvoiceID=@InvoiceID"
            Return _db.Query(Of Decimal)(sql, New With {.InvoiceID = invoiceId}).FirstOrDefault()
        End Function

        Public Function GetTotalPaidForInvoiceIds(invoiceIds As List(Of Integer)) As Decimal
            If invoiceIds Is Nothing OrElse invoiceIds.Count = 0 Then Return 0D
            Dim sql = "SELECT ISNULL(SUM(Amount), 0) FROM Payments WHERE InvoiceID IN @InvoiceIds"
            Return _db.Query(Of Decimal)(sql, New With {.InvoiceIds = invoiceIds}).FirstOrDefault()
        End Function

        Public Async Function InsertAsync(p As Payment) As Task(Of Integer)
            p.PaymentMethod = PaymentMethodStorage.NormalizeForPaymentsTable(p.PaymentMethod)
            Dim sql = "
                INSERT INTO Payments (InvoiceID, SupplierID, Amount, PaymentDate, PaymentMethod,
                    ChqOwner, AccountNumber, ChqNumber, ChqDueDate, ChqBank)
                VALUES (@InvoiceID, @SupplierID, @Amount, @PaymentDate, @PaymentMethod,
                    @ChqOwner, @AccountNumber, @ChqNumber, @ChqDueDate, @ChqBank);
                SELECT CAST(SCOPE_IDENTITY() AS int);"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, p)
        End Function

        Public Async Function UpdateAsync(p As Payment) As Task
            p.PaymentMethod = PaymentMethodStorage.NormalizeForPaymentsTable(p.PaymentMethod)
            Dim sql = "
                UPDATE Payments
                SET InvoiceID=@InvoiceID, SupplierID=@SupplierID, Amount=@Amount, PaymentDate=@PaymentDate,
                    PaymentMethod=@PaymentMethod,
                    ChqOwner=@ChqOwner, AccountNumber=@AccountNumber, ChqNumber=@ChqNumber,
                    ChqDueDate=@ChqDueDate, ChqBank=@ChqBank
                WHERE PaymentID=@PaymentID"
            Await _db.ExecuteAsync(sql, p)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM Payments WHERE PaymentID=@PaymentID"
            Await _db.ExecuteAsync(sql, New With {.PaymentID = id})
        End Function
    End Class

    Public Class ExpenseCategoryRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetAll() As IEnumerable(Of ExpenseCategory)
            Dim sql = "SELECT * FROM ExpenseCategories ORDER BY CategoryName"
            Return _db.Query(Of ExpenseCategory)(sql)
        End Function

        Public Async Function InsertAsync(c As ExpenseCategory) As Task(Of Integer)
            Dim sql = "
                INSERT INTO ExpenseCategories (CategoryName)
                VALUES (@CategoryName);
                SELECT CAST(SCOPE_IDENTITY() AS int);"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, c)
        End Function

        Public Async Function UpdateAsync(c As ExpenseCategory) As Task
            Dim sql = "UPDATE ExpenseCategories SET CategoryName=@CategoryName WHERE ExpenseCategoryID=@ExpenseCategoryID"
            Await _db.ExecuteAsync(sql, c)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM ExpenseCategories WHERE ExpenseCategoryID=@ExpenseCategoryID"
            Await _db.ExecuteAsync(sql, New With {.ExpenseCategoryID = id})
        End Function
    End Class

    Public Class ExpenseRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetByRange(startDate As Date, endDate As Date) As IEnumerable(Of Expense)
            Dim sql = "
                SELECT e.*, c.CategoryName, s.SupplierName
                FROM Expenses e
                JOIN ExpenseCategories c ON e.ExpenseCategoryID = c.ExpenseCategoryID
                LEFT JOIN Suppliers s ON e.SupplierID = s.SupplierID
                WHERE e.ExpenseDate BETWEEN @StartDate AND @EndDate
                ORDER BY e.ExpenseDate DESC"
            Return _db.Query(Of Expense)(sql, New With {.StartDate = startDate, .EndDate = endDate})
        End Function

        Public Async Function InsertAsync(e As Expense) As Task(Of Integer)
            Dim sql = "
                INSERT INTO Expenses (ExpenseCategoryID, SupplierID, ExpenseDate, Amount, PaymentMethod, ReferenceNumber, Notes, CreatedDate)
                VALUES (@ExpenseCategoryID, @SupplierID, @ExpenseDate, @Amount, @PaymentMethod, @ReferenceNumber, @Notes, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() AS int);"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, e)
        End Function

        Public Async Function UpdateAsync(e As Expense) As Task
            Dim sql = "
                UPDATE Expenses
                SET ExpenseCategoryID=@ExpenseCategoryID, SupplierID=@SupplierID, ExpenseDate=@ExpenseDate,
                    Amount=@Amount, PaymentMethod=@PaymentMethod, ReferenceNumber=@ReferenceNumber, Notes=@Notes
                WHERE ExpenseID=@ExpenseID"
            Await _db.ExecuteAsync(sql, e)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM Expenses WHERE ExpenseID=@ExpenseID"
            Await _db.ExecuteAsync(sql, New With {.ExpenseID = id})
        End Function
    End Class

    Public Class StockMovementRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Async Function InsertAsync(movement As StockMovement) As Task
            Dim sql = "
                INSERT INTO StockMovements (BatchID, ProductID, QuantityChange, MovementType, Notes)
                VALUES (@BatchID, @ProductID, @QuantityChange, @MovementType, @Notes)"
            Await _db.ExecuteAsync(sql, movement)
        End Function

        Public Async Function UpdateAsync(movement As StockMovement) As Task
            Dim sql = "
                UPDATE StockMovements
                SET BatchID=@BatchID, ProductID=@ProductID, QuantityChange=@QuantityChange,
                    MovementType=@MovementType, MovementDate=@MovementDate, Notes=@Notes
                WHERE MovementID=@MovementID"
            Await _db.ExecuteAsync(sql, movement)
        End Function

        Public Async Function DeleteAsync(id As Integer) As Task
            Dim sql = "DELETE FROM StockMovements WHERE MovementID=@MovementID"
            Await _db.ExecuteAsync(sql, New With {.MovementID = id})
        End Function
    End Class

    Public Class StockTrackingRepository
        Private ReadOnly _db As DapperHelper
        Public Sub New(connectionString As String)
            _db = New DapperHelper(connectionString)
        End Sub

        Public Function GetSnapshot() As IEnumerable(Of StockSnapshotDto)
            Dim sql = "
                SELECT p.ProductID, p.ProductName, c.CategoryName, u.UnitName, p.ReorderLevel,
                       ISNULL(SUM(b.CurrentQuantity), 0) AS OnHandQuantity
                FROM Products p
                JOIN Categories c ON p.CategoryID = c.CategoryID
                JOIN Units u ON p.UnitID = u.UnitID
                LEFT JOIN Batchess b ON b.ProductID = p.ProductID
                GROUP BY p.ProductID, p.ProductName, c.CategoryName, u.UnitName, p.ReorderLevel
                ORDER BY p.ProductName"
            Return _db.Query(Of StockSnapshotDto)(sql)
        End Function

        Public Function GetLowStock() As IEnumerable(Of StockSnapshotDto)
            Dim sql = "
                SELECT p.ProductID, p.ProductName, c.CategoryName, u.UnitName, p.ReorderLevel,
                       ISNULL(SUM(b.CurrentQuantity), 0) AS OnHandQuantity
                FROM Products p
                JOIN Categories c ON p.CategoryID = c.CategoryID
                JOIN Units u ON p.UnitID = u.UnitID
                LEFT JOIN Batchess b ON b.ProductID = p.ProductID
                GROUP BY p.ProductID, p.ProductName, c.CategoryName, u.UnitName, p.ReorderLevel
                HAVING ISNULL(SUM(b.CurrentQuantity), 0) <= p.ReorderLevel
                ORDER BY OnHandQuantity ASC"
            Return _db.Query(Of StockSnapshotDto)(sql)
        End Function

        Public Function GetExpiringBatches(withinDays As Integer) As IEnumerable(Of ExpiringBatchDto)
            Dim sql = "
                SELECT b.BatchID, b.ProductID, p.ProductName, b.BatchNumber, b.ExpirationDate,
                       b.CurrentQuantity,
                       DATEDIFF(day, CAST(GETDATE() AS date), b.ExpirationDate) AS DaysToExpire
                FROM Batchess b
                JOIN Products p ON p.ProductID = b.ProductID
                WHERE b.CurrentQuantity > 0
                  AND b.ExpirationDate <= DATEADD(day, @WithinDays, CAST(GETDATE() AS date))
                ORDER BY b.ExpirationDate ASC"
            Return _db.Query(Of ExpiringBatchDto)(sql, New With {.WithinDays = withinDays})
        End Function

        Public Function GetSupplierBalances() As IEnumerable(Of SupplierBalanceDto)
            Dim sql = "
                SELECT s.SupplierID, s.SupplierName,
                       ISNULL(inv.TotalInvoiced, 0) AS TotalInvoiced,
                       ISNULL(pay.TotalPaid, 0) AS TotalPaid,
                       ISNULL(inv.TotalInvoiced, 0) - ISNULL(pay.TotalPaid, 0) AS Balance
                FROM Suppliers s
                LEFT JOIN (
                    SELECT SupplierID, SUM(TotalAmount) AS TotalInvoiced
                    FROM BuyInvoices
                    GROUP BY SupplierID
                ) inv ON inv.SupplierID = s.SupplierID
                LEFT JOIN (
                    SELECT ISNULL(p.SupplierID, i.SupplierID) AS SupplierID, SUM(p.Amount) AS TotalPaid
                    FROM Payments p
                    INNER JOIN BuyInvoices i ON p.InvoiceID = i.InvoiceID
                    GROUP BY ISNULL(p.SupplierID, i.SupplierID)
                ) pay ON pay.SupplierID = s.SupplierID
                ORDER BY Balance DESC"
            Return _db.Query(Of SupplierBalanceDto)(sql)
        End Function
    End Class


End Module
