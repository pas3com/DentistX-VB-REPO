Imports System.Configuration

Public Class DependencyContainer
    'Private Shared ReadOnly _connString As String = DentistXDATA.GetConnection.ConnectionString
    ''SecurityHelper.DecryptConnectionString(ConfigurationManager.ConnectionStrings("DentistXConnectionString").ConnectionString)

    'Public Shared ReadOnly Property ProductService As ProductRepository
    '    Get
    '        Return New ProductRepository(_connString)
    '    End Get
    'End Property

    'Public Shared ReadOnly Property PurchaseService As PurchaseManager
    '    Get
    '        Return New PurchaseManager(
    '            New BuyInvoiceRepository(_connString),
    '            ProductService,
    '            New BatchRepository(_connString)
    '        )
    '    End Get
    'End Property

    'Public Shared ReadOnly Property InventoryService As InventoryManager
    '    Get
    '        Return New InventoryManager(
    '            New BatchRepository(_connString),
    '            ProductService,
    '            New StockMovementRepository(_connString),
    '            New BarcodeService(_connString)
    '        )
    '    End Get
    'End Property
End Class