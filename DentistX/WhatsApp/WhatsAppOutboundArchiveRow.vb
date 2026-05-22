''' <summary>Grid row for browsing dbo.WhatsAppOutboundMessage (truncated previews).</summary>
Public Class WhatsAppOutboundArchiveRow
    Public Property MessageId As Long
    Public Property CreatedAtUtc As DateTime
    Public Property UpdatedAtUtc As DateTime
    Public Property Status As String
    Public Property MessageCategory As String
    Public Property SourceHint As String
    Public Property TargetDigits As String
    Public Property BodyPreview As String
    Public Property AttachmentPathPreview As String
    Public Property PatientId As Integer?
    Public Property AppointmentId As Integer?
    Public Property Priority As Byte
    Public Property LastErrorPreview As String
    Public Property CancelledBeforeSend As Boolean
    Public Property GatewayJobId As String
    Public Property ClinicId As String
    Public Property NextAttemptAtUtc As DateTime
    Public Property AttemptCount As Integer
    Public Property ExpiresAtUtc As DateTime?
End Class
