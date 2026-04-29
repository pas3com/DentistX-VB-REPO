''' <summary>DTO for appointment reminder with patient and doctor info.</summary>
Public Class AppointmentReminderDto
    Public Property AppointmentID As Integer
    Public Property PatientID As Integer
    Public Property DrID As Integer
    Public Property StartDateTime As DateTime
    Public Property EndDateTime As DateTime
    Public Property Reason As String
    Public Property Notes As String
    Public Property Status As String
    Public Property PatientName As String
    ''' <summary>From Patient.Sex; used for WhatsApp salutation (Mr./Ms./Mr\Mrs).</summary>
    Public Property PatientSex As String
    ''' <summary>Digits-only target for WhatsApp (prefix + local). Filled after load from PatientWhatsLocal/Prefix/Fallback.</summary>
    Public Property PatientPhone As String
    ''' <summary>Raw RTRIM(p.WhatsApp). Used with <see cref="PatientWhatsAppPrefix"/> to build <see cref="PatientPhone"/>.</summary>
    Public Property PatientWhatsLocal As String
    Public Property PatientPhoneFallback As String
    Public Property PatientWhatsAppPrefix As String
    Public Property DrName As String
    ''' <summary>From AppointmentC; queued/manual reminder bodies omit Reason line when false.</summary>
    Public Property WhatsIncludeReason As Boolean = True
    ''' <summary>From AppointmentC; omit Notes line when false.</summary>
    Public Property WhatsIncludeNotes As Boolean = True
End Class
