Public Class PatientPayment
    Public Property PayID As Integer
    Public Property TrtID As Integer
    Public Property PatientID As Integer?
    Public Property PayValue As Decimal
    Public Property PayDate As Date
    Public Property Notes As String
    Public Property PayType As String
    Public Property ChqOwner As String
    Public Property AccountNumber As String
    Public Property ChqNumber As String
    Public Property ChqDueDate As Date?
    Public Property ChqBank As String
    Public Property IsCashed As Boolean?
    Public Property InsuranceCompany As String
    Public Property InsuranceNotes As String
    Public Property IsForward As Boolean?
    Public Property ForwardFromTo As String
    Public Property PatientName As String ' For display
    Public Property TreatmentDetail As String ' For display
End Class
