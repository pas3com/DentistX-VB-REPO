Public Class AppointmentC
    Public Property AppointmentID As Integer
    Public Property PatientID As Integer
    Public Property DrID As Integer
    Public Property AppDate As DateTime 'Time   ' ✅ must match column name exactly
    Public Property StartDateTime As DateTime
    Public Property EndDateTime As DateTime
    Public Property Reason As String
    Public Property Notes As String
    Public Property Status As String
    Public Property CreatedBy As String
    Public Property CreatedAt As DateTime
    ''' <summary>When true, WhatsApp appointment text (manual + 24h/short-lead queue) includes the Reason line.</summary>
    Public Property WhatsIncludeReason As Boolean = True
    ''' <summary>When true, WhatsApp appointment text includes the Notes line.</summary>
    Public Property WhatsIncludeNotes As Boolean = True
End Class
