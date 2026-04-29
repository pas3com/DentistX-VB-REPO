Imports System.Data.SqlClient
Imports System.Threading.Tasks

Public Class DashboardService
    Private ReadOnly _dbHelper As New DashDataModel.DatabaseHelper()
    Private ReadOnly _connectionString As String = DashDataModel.DatabaseHelper._connectionString

    Public Async Function GetDashboardDataAsync(filter As DashboardFilter) As Task(Of DashboardData)
        Dim data As New DashboardData()

        Await Task.Run(Sub()
                           ' Load all data in parallel
                           Dim kpiTask = Task.Run(Function() _dbHelper.GetDashboardKPIs(filter))
                           Dim patientsTask = Task.Run(Function() _dbHelper.GetPatients(filter))
                           Dim appointmentsTask = Task.Run(Function() _dbHelper.GetAppointments(filter))
                           Dim treatmentsTask = Task.Run(Function() _dbHelper.GetTreatments(filter))
                           Dim revenueTask = Task.Run(Function() _dbHelper.GetRevenueTrend(30))

                           Task.WaitAll(kpiTask, patientsTask, appointmentsTask, treatmentsTask, revenueTask)

                           data.KPIs = kpiTask.Result
                           data.Patients = patientsTask.Result
                           data.Appointments = appointmentsTask.Result
                           data.Treatments = treatmentsTask.Result
                           data.RevenueTrend = revenueTask.Result
                           data.PatientDemographics = _dbHelper.GetPatientDemographics()
                       End Sub)

        Return data
    End Function

    Public Function GetAlerts() As List(Of Alert)
        Dim alerts As New List(Of Alert)()

        Try
            ' Check for overdue payments
            Using conn As New SqlConnection(_connectionString)
                conn.Open()

                Dim sql = "
                        SELECT TOP 10 
                            p.PatientName,
                            pt.Detail,
                            pt.TrtValue - ISNULL(SUM(pp.PayValue), 0) as Outstanding,
                            DATEDIFF(DAY, pt.TrtDate, GETDATE()) as DaysOverdue
                        FROM Patient_Trts pt
                        LEFT JOIN Patient p ON pt.PatientID = p.PatientID
                        LEFT JOIN Patient_Pays pp ON pt.TrtID = pp.TrtID
                        WHERE pt.TrtValue > ISNULL(SUM(pp.PayValue), 0)
                        GROUP BY pt.TrtID, p.PatientName, pt.Detail, pt.TrtValue, pt.TrtDate
                        HAVING DATEDIFF(DAY, pt.TrtDate, GETDATE()) > 30
                        ORDER BY DaysOverdue DESC"

                Using cmd As New SqlCommand(sql, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            alerts.Add(New Alert() With {
                                .Type = "Payment",
                                .Title = "Overdue Payment",
                                .Message = $"Patient: {reader("PatientName")} - Outstanding: {Convert.ToDecimal(reader("Outstanding")):C2}",
                                .Priority = Alert.AlertPriority.High,
                                .Timestamp = DateTime.Now
                            })
                        End While
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Log error
        End Try

        Return alerts
    End Function
End Class
