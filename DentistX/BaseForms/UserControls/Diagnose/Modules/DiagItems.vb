Public Class DiagItems
	Property DiagID As Integer
	Property PatientID As Nullable(Of Integer)
	Property ToothNum As Nullable(Of Byte)
	Property ToothName As String
	Property TreatDate As Nullable(Of DateTime)
	Property Treat As String ' Status
	Property Status As String ' 
	Public Property IsSelected As Boolean
End Class
