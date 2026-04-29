
Public Class DashboardData
    Public Property KPIs As DashboardKPI
    Public Property Patients As List(Of Patient)
    Public Property Appointments As List(Of ApptDash)
    Public Property Treatments As List(Of PatientTreatment)
    Public Property RevenueTrend As List(Of RevenueTrend)
    Public Property PatientDemographics As List(Of PatientDemographic)
End Class