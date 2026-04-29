''' <summary>Display row for FrmAccountWhats grid.</summary>
Public Class AccountingSummaryRow
    Public Property PatientID As Integer
    Public Property PatientName As String
    Public Property PatientPhone As String
    Public Property TotalTreatments As Decimal
    Public Property TotalPayments As Decimal
    Public Property Balance As Decimal
    Public Property ReminderSent As Boolean

    Public ReadOnly Property ReminderStatus As String
        Get
            Return If(ReminderSent, If(Eng, "Sent", "تم الإرسال"), If(Eng, "Not sent", "لم يُرسل"))
        End Get
    End Property
End Class
