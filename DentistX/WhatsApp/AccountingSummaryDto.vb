''' <summary>DTO for patient accounting summary (treatments, payments, balance) in a period.</summary>
Public Class AccountingSummaryDto
    Public Property PatientID As Integer
    Public Property PatientName As String
    Public Property PatientPhone As String
    Public Property TotalTreatments As Decimal
    Public Property TotalPayments As Decimal
    Public Property Balance As Decimal
End Class
