Public Class PatientBalance
    Public Property PatientID As Integer
    Public Property TotalTreatments As Decimal
    Public Property TotalPayments As Decimal
    Public Property Balance As Decimal

    ' Default constructor
    Public Sub New()
    End Sub

    ' Parameterized constructor
    Public Sub New(patientId As Integer, totalTreatments As Decimal, totalPayments As Decimal, balance As Decimal)
        Me.PatientID = patientId
        Me.TotalTreatments = totalTreatments
        Me.TotalPayments = totalPayments
        Me.Balance = balance
    End Sub
End Class
