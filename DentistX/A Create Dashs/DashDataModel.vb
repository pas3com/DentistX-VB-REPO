Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Dapper
Module DashDataModel

    Public Class DatabaseHelper
        Public Shared _connectionString As String

        Public Sub New()
            _connectionString = DentistXDATA.GetConnection.ConnectionString ' ConfigurationManager.ConnectionStrings("DentalClinicConnection").ConnectionString
        End Sub

        Public Function GetDashboardKPIs(filter As DashboardFilter) As DashboardKPI
            Dim kpi As New DashboardKPI()

            Using conn As New SqlConnection(_connectionString)
                conn.Open()

                ' Total Patients
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM Patient", conn)
                    kpi.TotalPatients = CInt(cmd.ExecuteScalar())
                End Using

                ' Today's Appointments
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM AppointmentC WHERE CAST(AppDate AS DATE) = @Today", conn)
                    cmd.Parameters.AddWithValue("@Today", Date.Today)
                    kpi.TodayAppointments = CInt(cmd.ExecuteScalar())
                End Using

                ' This Month Revenue
                Using cmd As New SqlCommand("
                    SELECT ISNULL(SUM(TrtValue), 0) 
                    FROM Patient_Trts 
                    WHERE MONTH(TrtDate) = MONTH(GETDATE()) 
                    AND YEAR(TrtDate) = YEAR(GETDATE())", conn)
                    kpi.ThisMonthRevenue = CDec(cmd.ExecuteScalar())
                End Using

                ' Monthly Revenue
                ' Average Monthly Revenue since first treatment
                Using cmd As New SqlCommand("
                                            SELECT 
                                                CASE 
                                                    WHEN COUNT(*) = 0 THEN 0
                                                    ELSE
                                                        SUM(TrtValue) * 1.0 /
                                                        (DATEDIFF(MONTH, MIN(TrtDate), GETDATE()) + 1)
                                                END
                                            FROM Patient_Trts
                                            WHERE TrtDate <= GETDATE()", conn)

                    kpi.MonthlyRevenue = CDec(cmd.ExecuteScalar())
                End Using


                ' Outstanding Balance
                Using cmd As New SqlCommand("
                    SELECT ISNULL(SUM(TrtValue - ISNULL(TotalPaid, 0)), 0) 
                    FROM Patient_Trts pt
                    LEFT JOIN (
                        SELECT TrtID, SUM(PayValue) as TotalPaid 
                        FROM Patient_Pays 
                        GROUP BY TrtID
                    ) pp ON pt.TrtID = pp.TrtID
                    WHERE TrtValue > ISNULL(TotalPaid, 0)", conn)
                    kpi.OutstandingBalance = CDec(cmd.ExecuteScalar())
                End Using

                ' Active Treatments
                Using cmd As New SqlCommand("
                    SELECT COUNT(DISTINCT PatientID) 
                    FROM Patient_Trts 
                    WHERE TrtDate > @DateFrom", conn)
                    cmd.Parameters.AddWithValue("@DateFrom", Date.Today.AddDays(-90))
                    kpi.ActiveTreatments = CInt(cmd.ExecuteScalar())
                End Using

                ' New Patients This Month
                Using cmd As New SqlCommand("
                    SELECT COUNT(*) 
                    FROM Patient 
                    WHERE MONTH(CreateDate) = MONTH(GETDATE()) 
                    AND YEAR(CreateDate) = YEAR(GETDATE())", conn)
                    kpi.NewPatientsThisMonth = CInt(cmd.ExecuteScalar())
                End Using

                ' Collection Rate
                Dim totalRevenue As Decimal = 0
                Dim totalCollected As Decimal = 0

                Using cmd As New SqlCommand("SELECT ISNULL(SUM(TrtValue), 0) FROM Patient_Trts WHERE YEAR(TrtDate) = YEAR(GETDATE())", conn)
                    totalRevenue = CDec(cmd.ExecuteScalar())
                End Using

                Using cmd As New SqlCommand("SELECT ISNULL(SUM(PayValue), 0) FROM Patient_Pays WHERE YEAR(PayDate) = YEAR(GETDATE())", conn)
                    totalCollected = CDec(cmd.ExecuteScalar())
                End Using

                kpi.CollectionRate = If(totalRevenue > 0, Math.Round((totalCollected / totalRevenue) * 100, 1), 0)
            End Using

            Return kpi
        End Function

        ''' <summary>
        ''' Returns per-patient financial summary (TotalTreats, TotalPays, Balance)
        ''' filtered between two dates (inclusive). Dates come from DashboardFilter.
        ''' </summary>
        Public Function GetPatientBalances(filter As DashboardFilter) As List(Of PatientBalanceSummary)
            Dim results As New List(Of PatientBalanceSummary)()

            Using conn As New SqlConnection(_connectionString)
                conn.Open()

                Dim sql As String = "
                    SELECT 
                        p.PatientID,
                        p.PatientName,
                        ISNULL(SUM(pt.TrtValue), 0) AS TotalTreats,
                        ISNULL(SUM(pp.PayValue), 0) AS TotalPays,
                        ISNULL(SUM(pt.TrtValue), 0) - ISNULL(SUM(pp.PayValue), 0) AS Balance
                    FROM Patient p
                    LEFT JOIN Patient_Trts pt 
                        ON p.PatientID = pt.PatientID
                        AND (@DateFrom IS NULL OR pt.TrtDate >= @DateFrom)
                        AND (@DateTo   IS NULL OR pt.TrtDate <= @DateTo)
                    LEFT JOIN Patient_Pays pp 
                        ON pt.TrtID = pp.TrtID
                        AND (@DateFrom IS NULL OR pp.PayDate >= @DateFrom)
                        AND (@DateTo   IS NULL OR pp.PayDate <= @DateTo)
                    GROUP BY 
                        p.PatientID,
                        p.PatientName
                    HAVING 
                        ISNULL(SUM(pt.TrtValue), 0) <> 0
                        OR ISNULL(SUM(pp.PayValue), 0) <> 0
                    ORDER BY 
                        p.PatientName"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@DateFrom", If(filter.DateFrom = Date.MinValue, DBNull.Value, filter.DateFrom))
                    cmd.Parameters.AddWithValue("@DateTo", If(filter.DateTo = Date.MinValue, DBNull.Value, filter.DateTo))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim item As New PatientBalanceSummary() With {
                                .PatientID = Convert.ToInt32(reader("PatientID")),
                                .PatientName = reader("PatientName").ToString(),
                                .TotalTreats = Convert.ToDecimal(reader("TotalTreats")),
                                .TotalPays = Convert.ToDecimal(reader("TotalPays")),
                                .Balance = Convert.ToDecimal(reader("Balance"))
                            }
                            results.Add(item)
                        End While
                    End Using
                End Using
            End Using

            Return results
        End Function
        Public Function GetDoctors() As IEnumerable(Of Doctors)
            Using conn As New SqlConnection(_connectionString)
                conn.Open()
                Return conn.Query(Of Doctors)("SELECT * FROM Doctors  ORDER BY DrName")
            End Using
        End Function
        Public Function GetPatients(filter As DashboardFilter) As List(Of Patient)
            Dim patients As New List(Of Patient)
            Dim sql As String = ""
            If filter.ShowOnlyUnpaid Then
                sql = " SELECT Patient.*,BALANCE.BAL
            FROM Patient
            inner join BALANCE on Patient.PatientID=BALANCE.PatientID
                 WHERE (@DateFrom IS NULL OR CreateDate >= @DateFrom)
              AND (@DateTo   IS NULL OR CreateDate <= @DateTo)
              AND BALANCE.BAL<0"
            Else
                sql = "
            SELECT *
            FROM Patient
            WHERE (@DateFrom IS NULL OR CreateDate >= @DateFrom)
              AND (@DateTo   IS NULL OR CreateDate <= @DateTo)
            ORDER BY PatientName"

            End If
            Using conn As New SqlConnection(_connectionString)
                conn.Open()


                Using cmd As New SqlCommand(sql, conn)

                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = If(filter.DateFrom = Date.MinValue, DBNull.Value, filter.DateFrom)

                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = If(filter.DateTo = Date.MinValue, DBNull.Value, filter.DateTo)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            patients.Add(New Patient With {
                        .PatientID = CInt(reader("PatientID")),
                        .PatientName = reader("PatientName").ToString(),
                        .PatientNumber = If(reader.IsDBNull(reader.GetOrdinal("PatientNumber")), "", reader("PatientNumber").ToString()),
                        .Sex = If(reader.IsDBNull(reader.GetOrdinal("Sex")), "", reader("Sex").ToString()),
                        .Age = If(reader.IsDBNull(reader.GetOrdinal("Age")), CType(Nothing, Integer?), CInt(reader("Age"))),
                        .Phone = If(reader.IsDBNull(reader.GetOrdinal("Phone")), "", reader("Phone").ToString()),
                        .Address = If(reader.IsDBNull(reader.GetOrdinal("Address")), "", reader("Address").ToString()),
                        .Health = If(reader.IsDBNull(reader.GetOrdinal("Health")), "", reader("Health").ToString()),
                        .Treat = If(reader.IsDBNull(reader.GetOrdinal("Treat")), CType(Nothing, Boolean?), CBool(reader("Treat"))),
                        .Implant = If(reader.IsDBNull(reader.GetOrdinal("Implant")), CType(Nothing, Boolean?), CBool(reader("Implant"))),
                        .Ortho = If(reader.IsDBNull(reader.GetOrdinal("Ortho")), CType(Nothing, Boolean?), CBool(reader("Ortho"))),
                        .Diag = If(reader.IsDBNull(reader.GetOrdinal("Diag")), CType(Nothing, Boolean?), CBool(reader("Diag"))),
                        .CreateDate = If(reader.IsDBNull(reader.GetOrdinal("CreateDate")), CType(Nothing, Date?), CDate(reader("CreateDate")))
                    })
                        End While
                    End Using
                End Using
            End Using
            '                        .Mobile = If(reader.IsDBNull(reader.GetOrdinal("Mobile")), CType(Nothing, Boolean?), CBool(reader("Mobile"))),

            Return patients
        End Function

        Public Function GetPatients1(filter As DashboardFilter) As List(Of Patient)
            Dim patients As New List(Of Patient)()

            Using conn As New SqlConnection(_connectionString)
                conn.Open()

                Dim sql As String = "
                    SELECT * FROM Patient 
                    WHERE (@DateFrom IS NULL OR CreateDate >= @DateFrom)
                    AND (@DateTo IS NULL OR CreateDate <= @DateTo)
                    ORDER BY PatientName"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@DateFrom", If(filter.DateFrom = Date.MinValue, DBNull.Value, filter.DateFrom))
                    cmd.Parameters.AddWithValue("@DateTo", If(filter.DateTo = Date.MinValue, DBNull.Value, filter.DateTo))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim patient As New Patient() With {
                                .PatientID = Convert.ToInt32(reader("PatientID")),
                                .PatientName = reader("PatientName").ToString(),
                                .PatientNumber = If(reader("PatientNumber") Is DBNull.Value, "", reader("PatientNumber").ToString()),
                                .Sex = If(reader("Sex") Is DBNull.Value, "", reader("Sex").ToString()),
                                .Age = If(reader("Age") Is DBNull.Value, Nothing, Convert.ToInt32(reader("Age"))),
                                .Phone = If(reader("Phone") Is DBNull.Value, "", reader("Phone").ToString()),
                                .Address = If(reader("Address") Is DBNull.Value, "", reader("Address").ToString()),
                                .Health = If(reader("Health") Is DBNull.Value, "", reader("Health").ToString()),
                                .Treat = If(reader("Treat") Is DBNull.Value, Nothing, Convert.ToBoolean(reader("Treat"))),
                                .Implant = If(reader("Implant") Is DBNull.Value, Nothing, Convert.ToBoolean(reader("Implant"))),
                                .Ortho = If(reader("Ortho") Is DBNull.Value, Nothing, Convert.ToBoolean(reader("Ortho"))),
                                .CreateDate = If(reader("CreateDate") Is DBNull.Value, Nothing, Convert.ToDateTime(reader("CreateDate")))
                            }
                            patients.Add(patient)
                        End While
                    End Using
                End Using
            End Using

            Return patients
        End Function

        ' Add these methods to the DatabaseHelper class

        Public Function GetPayments(filter As DashboardFilter) As List(Of PatientPayment)
            Dim payments As New List(Of PatientPayment)()

            Using conn As New SqlConnection(_connectionString)
                conn.Open()

                Dim sql As String = "
            SELECT 
                pp.*,
                p.PatientName,
                pt.Detail as TreatmentDetail
            FROM Patient_Pays pp
            LEFT JOIN Patient p ON pp.PatientID = p.PatientID
            LEFT JOIN Patient_Trts pt ON pp.TrtID = pt.TrtID
            WHERE (@DateFrom IS NULL OR pp.PayDate >= @DateFrom)
            AND (@DateTo IS NULL OR pp.PayDate <= @DateTo)
            ORDER BY pp.PayDate DESC"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@DateFrom", If(filter.DateFrom = Date.MinValue, DBNull.Value, filter.DateFrom))
                    cmd.Parameters.AddWithValue("@DateTo", If(filter.DateTo = Date.MinValue, DBNull.Value, filter.DateTo))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim payment As New PatientPayment() With {
                        .PayID = Convert.ToInt32(reader("PayID")),
                        .TrtID = Convert.ToInt32(reader("TrtID")),
                        .PatientID = If(reader("PatientID") Is DBNull.Value, Nothing, Convert.ToInt32(reader("PatientID"))),
                        .PayValue = Convert.ToDecimal(reader("PayValue")),
                        .PayDate = Convert.ToDateTime(reader("PayDate")),
                        .Notes = If(reader("Notes") Is DBNull.Value, "", reader("Notes").ToString()),
                        .PayType = If(reader("PayType") Is DBNull.Value, "", reader("PayType").ToString()),
                        .PatientName = If(reader("PatientName") Is DBNull.Value, "", reader("PatientName").ToString()),
                        .TreatmentDetail = If(reader("TreatmentDetail") Is DBNull.Value, "", reader("TreatmentDetail").ToString())
                    }
                            payments.Add(payment)
                        End While
                    End Using
                End Using
            End Using

            Return payments
        End Function

        Public Function GetTreatmentStats(filter As DashboardFilter) As TreatmentStatistics
            Dim stats As New TreatmentStatistics()

            Using conn As New SqlConnection(_connectionString)
                conn.Open()

                ' Total treatments
                Dim sql = "
            SELECT 
                COUNT(*) as TotalTreatments,
                SUM(TrtValue) as TotalRevenue,
                AVG(TrtValue) as AvgTreatmentValue
            FROM Patient_Trts
            WHERE (@DateFrom IS NULL OR TrtDate >= @DateFrom)
            AND (@DateTo IS NULL OR TrtDate <= @DateTo)"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@DateFrom", If(filter.DateFrom = Date.MinValue, DBNull.Value, filter.DateFrom))
                    cmd.Parameters.AddWithValue("@DateTo", If(filter.DateTo = Date.MinValue, DBNull.Value, filter.DateTo))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            stats.TotalTreatments = Convert.ToInt32(reader("TotalTreatments"))
                            stats.TotalRevenue = Convert.ToDecimal(reader("TotalRevenue"))
                            stats.AvgTreatmentValue = Convert.ToDecimal(reader("AvgTreatmentValue"))
                        End If
                    End Using
                End Using

                ' Treatment types breakdown
                sql = "
            SELECT 
                CASE 
                    WHEN ToothTrtID IS NOT NULL THEN 'Tooth Treatment'
                    WHEN OrthoID IS NOT NULL THEN 'Orthodontics'
                    WHEN OtherTrtID IS NOT NULL THEN 'Other Treatment'
                    WHEN MobileID IS NOT NULL THEN 'Mobile'
                    ELSE 'Unknown'
                END as TreatmentType,
                COUNT(*) as Count,
                SUM(TrtValue) as Revenue
            FROM Patient_Trts
            WHERE (@DateFrom IS NULL OR TrtDate >= @DateFrom)
            AND (@DateTo IS NULL OR TrtDate <= @DateTo)
            GROUP BY 
                CASE 
                    WHEN ToothTrtID IS NOT NULL THEN 'Tooth Treatment'
                    WHEN OrthoID IS NOT NULL THEN 'Orthodontics'
                    WHEN OtherTrtID IS NOT NULL THEN 'Other Treatment'
                    WHEN MobileID IS NOT NULL THEN 'Mobile'
                    ELSE 'Unknown'
                END"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@DateFrom", If(filter.DateFrom = Date.MinValue, DBNull.Value, filter.DateFrom))
                    cmd.Parameters.AddWithValue("@DateTo", If(filter.DateTo = Date.MinValue, DBNull.Value, filter.DateTo))

                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        stats.TreatmentTypes = dt
                    End Using
                End Using
            End Using

            Return stats
        End Function

        Public Class TreatmentStatistics
            Public Property TotalTreatments As Integer
            Public Property TotalRevenue As Decimal
            Public Property AvgTreatmentValue As Decimal
            Public Property TreatmentTypes As DataTable
        End Class
        Public Function GetAppointments(filter As DashboardFilter) As List(Of ApptDash)
            Dim appointments As New List(Of ApptDash)()
            If filter.DoctorID = 0 Then
                filter.DoctorID = Nothing
            End If
            Using conn As New SqlConnection(_connectionString)
                conn.Open()

                Dim sql As String = "
                    SELECT a.*, p.PatientName, d.DrName
                    FROM AppointmentC a
                    LEFT JOIN Patient p ON a.PatientID = p.PatientID
                    LEFT JOIN Doctors d ON a.DrID = d.DrID
                    WHERE (@DateFrom IS NULL OR a.AppDate >= @DateFrom)
                    AND (@DateTo IS NULL OR a.AppDate <= @DateTo)
                    AND (@DrID IS NULL OR a.DrID = @DrID)
                    AND (@Status IS NULL OR a.Status = @Status)
                    ORDER BY a.StartDateTime"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@DateFrom", If(filter.DateFrom = Date.MinValue, DBNull.Value, filter.DateFrom))
                    cmd.Parameters.AddWithValue("@DateTo", If(filter.DateTo = Date.MinValue, DBNull.Value, filter.DateTo))
                    cmd.Parameters.AddWithValue("@DrID", If(filter.DoctorID, DBNull.Value))
                    cmd.Parameters.AddWithValue("@Status", If(String.IsNullOrWhiteSpace(filter.Status), DBNull.Value, filter.Status))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim appointment As New ApptDash() With {
                                .AppointmentID = Convert.ToInt32(reader("AppointmentID")),
                                .PatientID = Convert.ToInt32(reader("PatientID")),
                                .DrID = Convert.ToInt32(reader("DrID")),
                                .AppDate = Convert.ToDateTime(reader("AppDate")),
                                .StartDateTime = Convert.ToDateTime(reader("StartDateTime")),
                                .EndDateTime = Convert.ToDateTime(reader("EndDateTime")),
                                .Reason = If(reader("Reason") Is DBNull.Value, "", reader("Reason").ToString()),
                                .Notes = If(reader("Notes") Is DBNull.Value, "", reader("Notes").ToString()),
                                .Status = If(reader("Status") Is DBNull.Value, "", reader("Status").ToString()),
                                .CreatedBy = If(reader("CreatedBy") Is DBNull.Value, "", reader("CreatedBy").ToString()),
                                .CreatedAt = Convert.ToDateTime(reader("CreatedAt")),
                                .PatientName = If(reader("PatientName") Is DBNull.Value, "", reader("PatientName").ToString()),
                                .DoctorName = If(reader("DrName") Is DBNull.Value, "", reader("DrName").ToString())
                            }
                            appointments.Add(appointment)
                        End While
                    End Using
                End Using
            End Using

            Return appointments
        End Function

        Public Function GetTreatments(filter As DashboardFilter) As List(Of PatientTreatment)
            Dim treatments As New List(Of PatientTreatment)()

            Using conn As New SqlConnection(_connectionString)
                conn.Open()

                Dim sql As String = "
                    SELECT pt.*, p.PatientName,
                        (SELECT ISNULL(SUM(PayValue), 0) 
                         FROM Patient_Pays pp 
                         WHERE pp.TrtID = pt.TrtID) as TotalPaid
                    FROM Patient_Trts pt
                    LEFT JOIN Patient p ON pt.PatientID = p.PatientID
                    WHERE (@DateFrom IS NULL OR pt.TrtDate >= @DateFrom)
                    AND (@DateTo IS NULL OR pt.TrtDate <= @DateTo)
                    ORDER BY pt.TrtDate DESC"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@DateFrom", If(filter.DateFrom = Date.MinValue, DBNull.Value, filter.DateFrom))
                    cmd.Parameters.AddWithValue("@DateTo", If(filter.DateTo = Date.MinValue, DBNull.Value, filter.DateTo))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim trtValue As Decimal = Convert.ToDecimal(reader("TrtValue"))
                            Dim totalPaid As Decimal = Convert.ToDecimal(reader("TotalPaid"))

                            Dim treatment As New PatientTreatment() With {
                                .TrtID = Convert.ToInt32(reader("TrtID")),
                                .PatientID = Convert.ToInt32(reader("PatientID")),
                                .Detail = reader("Detail").ToString(),
                                .TrtDate = Convert.ToDateTime(reader("TrtDate")),
                                .TrtValue = trtValue,
                                .TotalPaid = totalPaid,
                                .Balance = totalPaid - trtValue,
                                .PatientName = If(reader("PatientName") Is DBNull.Value, "", reader("PatientName").ToString())
                            }
                            treatments.Add(treatment)
                        End While
                    End Using
                End Using
            End Using

            Return treatments
        End Function

        Public Function GetRevenueTrend(days As Integer) As List(Of RevenueTrend)
            Dim trends As New List(Of RevenueTrend)()

            Using conn As New SqlConnection(_connectionString)
                conn.Open()

                Dim sql As String = "
                    SELECT 
                        CONVERT(VARCHAR, DATEADD(DAY, DATEDIFF(DAY, 0, TrtDate), 0), 23) as Period,
                        ISNULL(SUM(TrtValue), 0) as Revenue
                    FROM Patient_Trts
                    WHERE TrtDate >= DATEADD(DAY, -@Days, GETDATE())
                    GROUP BY DATEADD(DAY, DATEDIFF(DAY, 0, TrtDate), 0)
                    ORDER BY Period"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Days", days)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim trend As New RevenueTrend() With {
                                .Period = reader("Period").ToString(),
                                .Revenue = Convert.ToDecimal(reader("Revenue")),
                                .Target = Convert.ToDecimal(reader("Revenue")) * 1.1 ' 10% target increase
                            }
                            trends.Add(trend)
                        End While
                    End Using
                End Using
            End Using

            Return trends
        End Function
        Public Function GetPatientDemographics() As List(Of PatientDemographic)
            Dim demographics As New List(Of PatientDemographic)()

            Using conn As New SqlConnection(_connectionString)
                conn.Open()

                Dim sql As String = "
            SELECT 
                CASE 
                    WHEN Age BETWEEN 1 AND 18 THEN 'Under 18'
                    WHEN Age BETWEEN 18 AND 30 THEN '18-30'
                    WHEN Age BETWEEN 31 AND 50 THEN '31-50'
                    WHEN Age > 50 THEN 'Over 50'
                    WHEN Age = 0 THEN 'Not Set'
                    ELSE 'Unknown'
                END AS Category,

                COUNT(*) AS TotalCount,
                SUM(CASE WHEN Sex = 'MALE' OR Sex = 'Male' OR Sex = 'ذكر'  THEN 1 ELSE 0 END) AS MaleCount,
                SUM(CASE WHEN Sex = 'FEMALE' OR Sex = 'Female' OR Sex = 'أنثى'  THEN 1 ELSE 0 END) AS FemaleCount

            FROM Patient
            GROUP BY 
                CASE 
                    WHEN Age BETWEEN 1 AND 18 THEN 'Under 18'
                    WHEN Age BETWEEN 18 AND 30 THEN '18-30'
                    WHEN Age BETWEEN 31 AND 50 THEN '31-50'
                    WHEN Age > 50 THEN 'Over 50'
                    WHEN Age = 0 THEN 'Not Set'
                    ELSE 'Unknown'
                END"

                Using cmd As New SqlCommand(sql, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            demographics.Add(New PatientDemographic() With {
                        .Category = reader("Category").ToString(),
                        .Count = Convert.ToInt32(reader("TotalCount")),
                        .MaleCount = Convert.ToInt32(reader("MaleCount")),
                        .FemaleCount = Convert.ToInt32(reader("FemaleCount"))
                    })
                        End While
                    End Using
                End Using
            End Using

            ' Percentages based on total population
            Dim total As Integer = demographics.Sum(Function(d) d.Count)

            For Each demo In demographics
                demo.Percentage =
            If(total > 0,
               Math.Round((demo.Count / total) * 100D, 1),
               0D)
            Next

            Return demographics
        End Function

        Public Function GetPatientDemographics1() As List(Of PatientDemographic)
            Dim demographics As New List(Of PatientDemographic)()

            Using conn As New SqlConnection(_connectionString)
                conn.Open()
                Dim sexSql As String = "
                                        SELECT 
                                            CASE 
                                                WHEN Age BETWEEN 1 AND 18 THEN 'Under 18'
                                                WHEN Age BETWEEN 18 AND 30 THEN '18-30'
                                                WHEN Age BETWEEN 31 AND 50 THEN '31-50'
                                                WHEN Age > 50 THEN 'Over 50'
                                                WHEN Age = 0 THEN 'Not Set'
                                                ELSE 'Unknown'
                                            END AS Category,

                                            COUNT(*) AS TotalCount,

                                            SUM(CASE WHEN Sex = 'M' OR Sex = 'Male' THEN 1 ELSE 0 END) AS MaleCount,
                                            SUM(CASE WHEN Sex = 'F' OR Sex = 'Female' THEN 1 ELSE 0 END) AS FemaleCount

                                        FROM Patient
                                        GROUP BY 
                                            CASE 
                                                WHEN Age BETWEEN 1 AND 18 THEN 'Under 18'
                                                WHEN Age BETWEEN 18 AND 30 THEN '18-30'
                                                WHEN Age BETWEEN 31 AND 50 THEN '31-50'
                                                WHEN Age > 50 THEN 'Over 50'
                                                WHEN Age = 0 THEN 'Not Set'
                                                ELSE 'Unknown'
                                            END
                                        "

                ' Age Groups
                Dim ageSql As String = "
                Select Case
                        CASE 
                            WHEN Age BETWEEN 1 AND  18 THEN 'Under 18'
                            WHEN Age BETWEEN 18 AND 30 THEN '18-30'
                            WHEN Age BETWEEN 31 AND 50 THEN '31-50'
                            WHEN Age > 50 THEN 'Over 50'
                            WHEN Age = 0 THEN 'Not Set'
                            ELSE 'Unknown'
                        END as Category,
                        COUNT(*) as Count
                    FROM Patient
                    GROUP BY 
                        CASE 
                            WHEN Age BETWEEN 1 AND  18 THEN 'Under 18'
                            WHEN Age BETWEEN 18 AND 30 THEN '18-30'
                            WHEN Age BETWEEN 31 AND 50 THEN '31-50'
                            WHEN Age > 50 THEN 'Over 50'
                            WHEN Age = 0 THEN 'Not Set'
                            ELSE 'Unknown'
                        END"

                Using cmd As New SqlCommand(ageSql, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim demo As New PatientDemographic() With {
                                .Category = reader("Category").ToString(),
                                .Count = Convert.ToInt32(reader("Count"))
                            }
                            demographics.Add(demo)
                        End While
                    End Using
                End Using
            End Using

            ' Calculate percentages
            Dim total As Integer = demographics.Sum(Function(d) d.Count)
            For Each demo In demographics
                demo.Percentage = If(total > 0, Math.Round((demo.Count / total) * 100, 1), 0)
            Next

            Return demographics
        End Function

        Public Function GetDashboardAlerts() As List(Of String)
            Dim alerts As New List(Of String)()

            ' Check overdue payments
            Using conn As New SqlConnection(_connectionString)
                conn.Open()
                Dim sql = "
                    SELECT COUNT(*) 
                    FROM Patient_Trts pt
                    LEFT JOIN (
                        SELECT TrtID, SUM(PayValue) as TotalPaid 
                        FROM Patient_Pays 
                        GROUP BY TrtID
                    ) pp ON pt.TrtID = pp.TrtID
                    WHERE TrtValue > ISNULL(TotalPaid, 0)
                    AND pt.TrtDate < DATEADD(DAY, -30, GETDATE())"

                Using cmd As New SqlCommand(sql, conn)
                    Dim overdueCount As Integer = CInt(cmd.ExecuteScalar())
                    If overdueCount > 0 Then
                        alerts.Add($"You have {overdueCount} overdue payments")
                    End If
                End Using

                ' Check upcoming appointments
                sql = "
                    SELECT COUNT(*) 
                    FROM AppointmentC 
                    WHERE StartDateTime BETWEEN GETDATE() AND DATEADD(HOUR, 24, GETDATE())
                    AND Status = 'Scheduled'"

                Using cmd As New SqlCommand(sql, conn)
                    Dim upcomingCount As Integer = CInt(cmd.ExecuteScalar())
                    If upcomingCount > 0 Then
                        alerts.Add($"You have {upcomingCount} appointments in next 24 hours")
                    End If
                End Using
            End Using

            Return alerts
        End Function

        Public Class PatientBalanceSummary
            Public Property PatientID As Integer
            Public Property PatientName As String
            Public Property TotalTreats As Decimal
            Public Property TotalPays As Decimal
            Public Property Balance As Decimal
        End Class
    End Class

End Module
