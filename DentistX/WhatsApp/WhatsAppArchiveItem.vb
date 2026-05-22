''' <summary>One row from GET /api/whatsapp-archive/:clinicId (optional ?number=).</summary>
Public Class WhatsAppArchiveItem
    Public Property Id As String
    Public Property TargetNumber As String
    Public Property MessageText As String
    Public Property HasAttachment As Boolean
    Public Property Status As String
    Public Property ErrorMessage As String
    Public Property CreatedAtUtc As DateTime?
    Public Property LastUpdatedAtUtc As DateTime?
End Class
