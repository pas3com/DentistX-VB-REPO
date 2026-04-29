''' <summary>Session-scoped notifications for every WhatsApp send attempt logged by <see cref="WhatsAppService"/>.</summary>
Public NotInheritable Class WhatsAppSessionMessageCenter
    Private Sub New()
    End Sub

    Public Shared Event MessageProcessed As EventHandler(Of WhatsAppSessionMessageEventArgs)

    Public Shared Sub RaiseMessageProcessed(row As WhatsAppActivityLogRow, revealMessageCenterDock As Boolean)
        If row Is Nothing Then Return
        RaiseEvent MessageProcessed(Nothing, New WhatsAppSessionMessageEventArgs(row, revealMessageCenterDock))
    End Sub
End Class
