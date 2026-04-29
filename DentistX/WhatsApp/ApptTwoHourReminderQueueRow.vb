''' <summary>One row per appointment in dbo.ApptTwoHourWhatsAppQueue (24h + 2h reminder legs).</summary>
Public Class ApptTwoHourReminderQueueRow
    Public Property QueueId As Long
    Public Property AppointmentId As Integer
    Public Property ApptStartSnapshot As DateTime
    Public Property PatientId As Integer?
    Public Property PatientName As String
    Public Property DrName As String
    Public Property TargetPhone As String
    Public Property MessagePreview24h As String
    Public Property MessagePreview2h As String
    Public Property SendAt24h As DateTime?
    Public Property SendAt2h As DateTime?
    Public Property Status24h As String
    Public Property Status2h As String
    Public Property Processed24hAtUtc As DateTime?
    Public Property Processed2hAtUtc As DateTime?
    Public Property Error24h As String
    Public Property Error2h As String
    Public Property WhatsAppLogId24h As Long?
    Public Property WhatsAppLogId2h As Long?
    Public Property ClinicId As String
    Public Property CreatedAtUtc As DateTime

#Region "Legacy single-kind columns (optional)"
    Public Property ReminderKind As String
    Public Property MessagePreview As String
    Public Property Status As String
    Public Property ProcessedAtUtc As DateTime?
    Public Property ErrorMessage As String
    Public Property WhatsAppLogId As Long?
#End Region
End Class
