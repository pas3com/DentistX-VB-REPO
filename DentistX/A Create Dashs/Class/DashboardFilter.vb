Public Class DashboardFilter
    Public Property DateFrom As Date = Date.Today.AddDays(-30)
    Public Property DateTo As Date = Date.Today
    Public Property DoctorID As Integer?
    Public Property TreatmentType As String
    Public Property Status As String
    Public Property ShowOnlyUnpaid As Boolean = False
End Class
