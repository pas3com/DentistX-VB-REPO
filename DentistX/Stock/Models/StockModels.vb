Imports System

Module StockModels
    Public Class Supplier
        Public Property SupplierID As Integer
        Public Property SupplierName As String
        Public Property ContactPerson As String
        Public Property PhoneNumber As String
        Public Property EmailAddress As String
        Public Property PhysicalAddress As String
        Public Property PaymentTerms As String
        Public Property WhatsAppPrefix As String
        Public Property WhatsApp As String
    End Class

    Public Class Category
        Public Property CategoryID As Integer
        Public Property CategoryName As String
    End Class

    Public Class Unit
        Public Property UnitID As Integer
        Public Property UnitName As String
    End Class

    Public Class Product
        Public Property ProductID As Integer
        Public Property ProductName As String
        Public Property ProductDescription As String
        Public Property UnitID As Integer
        Public Property CategoryID As Integer
        Public Property ReorderLevel As Integer
        Public Property UnitName As String
        Public Property CategoryName As String

        Public Overridable Property Batchess As ICollection(Of Batch)
    End Class

    Public Class Barcode
        Public Property BarcodeID As Integer
        Public Property ProductID As Integer
        Public Property BarcodeValue As String
        Public Property BarcodeType As String
    End Class

    Public Class Batch
        Public Property BatchID As Integer
        Public Property ProductID As Integer
        Public Property SupplierID As Integer
        Public Property BatchNumber As String
        Public Property ManufacturingDate As Date?
        Public Property ExpirationDate As Date
        Public Property ReceivedQuantity As Integer
        Public Property CurrentQuantity As Integer
        Public Property Barcode As String

    End Class

    Public Class BuyInvoice
        Public Property InvoiceID As Integer
        Public Property SupplierID As Integer
        Public Property SupplierName As String
        Public Property InvoiceDate As Date
        Public Property TotalAmount As Decimal
        Public Property DueDate As Date
        Public Property InvoiceStatus As String
        Public Property CreatedDate As DateTime?
    End Class

    Public Class BuyInvoiceLineItem
        Public Property LineItemID As Integer
        Public Property InvoiceID As Integer
        Public Property ProductID As Integer
        Public Property ProductName As String
        Public Property Quantity As Integer
        Public Property UnitPrice As Decimal
        Public Property BatchNumber As String
        Public Property ExpirationDate As Date?
    End Class

    Public Class Payment
        Public Property PaymentID As Integer
        Public Property InvoiceID As Integer
        Public Property SupplierID As Integer?
        Public Property SupplierName As String
        Public Property Amount As Decimal
        Public Property PaymentDate As Date
        Public Property PaymentMethod As String
        Public Property ChqOwner As String
        Public Property AccountNumber As String
        Public Property ChqNumber As String
        Public Property ChqDueDate As DateTime?
        Public Property ChqBank As String
    End Class

    Public Class ExpenseCategory
        Public Property ExpenseCategoryID As Integer
        Public Property CategoryName As String
    End Class

    Public Class Expense
        Public Property ExpenseID As Integer
        Public Property ExpenseCategoryID As Integer
        Public Property CategoryName As String
        Public Property SupplierID As Integer?
        Public Property SupplierName As String
        Public Property ExpenseDate As Date
        Public Property Amount As Decimal
        Public Property PaymentMethod As String
        Public Property ReferenceNumber As String
        Public Property Notes As String
        Public Property CreatedDate As DateTime?
    End Class

    Public Class StockMovement
        Public Property MovementID As Integer
        Public Property BatchID As Integer?
        Public Property ProductID As Integer
        Public Property QuantityChange As Integer
        Public Property MovementType As String
        Public Property MovementDate As DateTime
        Public Property Notes As String
    End Class

    Public Class StockSnapshotDto
        Public Property ProductID As Integer
        Public Property ProductName As String
        Public Property CategoryName As String
        Public Property UnitName As String
        Public Property ReorderLevel As Integer
        Public Property OnHandQuantity As Integer
    End Class

    Public Class ExpiringBatchDto
        Public Property BatchID As Integer
        Public Property ProductID As Integer
        Public Property ProductName As String
        Public Property BatchNumber As String
        Public Property ExpirationDate As Date
        Public Property CurrentQuantity As Integer
        Public Property DaysToExpire As Integer
    End Class

    Public Class BatchDetailsDto
        Inherits Batch
        Public Property ProductName As String
        Public Property SupplierName As String
    End Class

    Public Enum InventoryStatus
        InStock
        Expired
        LowStock
        OutOfStock
    End Enum

    Public Class StockTrendDto
        Public Property Month As String
        Public Property StockLevel As Integer
    End Class

    Public Class SupplierBalanceDto
        Public Property SupplierID As Integer
        Public Property SupplierName As String
        Public Property TotalInvoiced As Decimal
        Public Property TotalPaid As Decimal
        Public Property Balance As Decimal
    End Class
End Module
