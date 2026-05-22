''' <summary>Row from dbo.WhatsAppOutboundMessage for Dapper.</summary>
Public Class WhatsAppOutboundMessageRow
    Public Property MessageId As Long
    Public Property CorrelationId As Guid
    Public Property IdempotencyKey As String
    Public Property ClinicId As String
    Public Property MessageCategory As String
    Public Property SourceHint As String
    Public Property CallerDisplayName As String
    Public Property PatientId As Integer?
    Public Property AppointmentId As Integer?
    Public Property ReminderLeg As String
    Public Property AppointmentStartUtc As DateTime?
    Public Property TargetDigits As String
    Public Property BodyPlain As String
    Public Property AttachmentPath As String
    Public Property Priority As Byte
    Public Property Status As String
    Public Property GatewayJobId As String
    Public Property CreatedAtUtc As DateTime
    Public Property UpdatedAtUtc As DateTime
    Public Property NotBeforeUtc As DateTime
    Public Property ExpiresAtUtc As DateTime?
    Public Property NextAttemptAtUtc As DateTime
    Public Property AttemptCount As Integer
    Public Property LastError As String
    Public Property CancelledBeforeSend As Boolean
    Public Property SuppressUiFeedback As Boolean
    Public Property RevealMessageCenter As Boolean
    Public Property ReminderQueueId As Long?
    Public Property WaGroupKey As String
    Public Property WaPartIndex As Byte?
    Public Property WaPartTotal As Byte?
    Public Property WaSchedulerMeta As String
End Class
