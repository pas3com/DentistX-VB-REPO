Public Class WhatsAppOutboundArchivePageResult
    Public Property TotalCount As Integer
    Public Property Rows As List(Of WhatsAppOutboundArchiveRow)

    Public Sub New()
        Rows = New List(Of WhatsAppOutboundArchiveRow)()
    End Sub
End Class
