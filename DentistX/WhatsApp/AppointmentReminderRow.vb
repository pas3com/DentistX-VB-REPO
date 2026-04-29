''' <summary>Display row for FrmApptsWhats grid: appointment reminder data plus sent status.</summary>
Public Class AppointmentReminderRow
    Public Property AppointmentID As Integer
    Public Property PatientName As String
    Public Property DrName As String
    Public Property StartDateTime As DateTime
    Public Property EndDateTime As DateTime
    Public Property Reason As String
    Public Property PatientPhone As String
    Public Property ReminderSent As Boolean

    Public ReadOnly Property ReminderStatus As String
        Get
            Return If(ReminderSent, "تم الإرسال", "لم يُرسل")
        End Get
    End Property

    ''' <summary>Original DTO for sending reminder.</summary>
    Public Property Dto As AppointmentReminderDto
End Class
