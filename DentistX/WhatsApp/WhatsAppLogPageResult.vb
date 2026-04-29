Public Class WhatsAppLogPageResult
    Public Property TotalCount As Integer
    Public Property Rows As List(Of WhatsAppActivityLogRow)

    Public Sub New()
        Rows = New List(Of WhatsAppActivityLogRow)()
    End Sub
End Class
