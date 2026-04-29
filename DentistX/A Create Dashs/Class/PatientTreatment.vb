Public Class PatientTreatment
    Public Property TrtID As Integer
    Public Property PatientID As Integer
    Public Property ToothTrtID As Integer?
    Public Property OrthoID As Integer?
    Public Property OtherTrtID As Integer?
    Public Property Detail As String
    Public Property TrtDate As Date
    Public Property TrtValue As Decimal
    Public Property IsMultiTooth As Boolean?
    Public Property Discount As Decimal?
    Public Property DiscountType As Byte?
    Public Property TotalPaid As Decimal ' Calculated
    Public Property Balance As Decimal ' Calculated
    Public Property PatientName As String ' For display
End Class

