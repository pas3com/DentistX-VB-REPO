
Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.CodeParser
Imports DevExpress.XtraScheduler

Public Class AppointmentCRepository

    Private ReadOnly _hasConnectionOverride As Boolean
    Private ReadOnly _overrideConnectionString As String

    Public Sub New()
        _hasConnectionOverride = False
        _overrideConnectionString = Nothing
    End Sub

    Public Sub New(connectionString As String)
        _hasConnectionOverride = True
        _overrideConnectionString = connectionString
    End Sub

    Private Function ResolveConnectionString() As String
        If _hasConnectionOverride Then Return _overrideConnectionString
        Return DentistXDATA.GetEffectiveConnectionString()
    End Function

    Private Function GetConnection() As SqlConnection
        Return New SqlConnection(ResolveConnectionString())
    End Function

    Public Function GetPatientName(patientId As Integer) As String
        Using conn = GetConnection()
            Return conn.ExecuteScalar(Of String)("SELECT PatientName FROM Patient WHERE PatientID=@ID", New With {.ID = patientId})
        End Using
    End Function

    Public Function GetAllAppointments() As List(Of AppointmentC)
        Using conn = GetConnection()
            Dim sql = "SELECT * FROM AppointmentC"
            Return conn.Query(Of AppointmentC)(sql).ToList()
        End Using
    End Function

    Public Function GetDoctorID(drName As String) As Integer
        Using conn = GetConnection()
            Return conn.ExecuteScalar(Of Integer)("SELECT DrID FROM Doctors WHERE DrName=@DrName", New With {.DrName = drName})
        End Using
    End Function
    Public Function GetDoctorName(drId As Integer) As String
        Using conn = GetConnection()
            Return conn.ExecuteScalar(Of String)("SELECT DrName FROM Doctors WHERE DrID=@ID", New With {.ID = drId})
        End Using
    End Function

    Public Function GetDoctorColor(drId As Integer) As String
        Using conn = GetConnection()
            Return conn.ExecuteScalar(Of String)("SELECT DrColor FROM Doctors WHERE DrID=@ID", New With {.ID = drId})
        End Using
    End Function

    Public Function GetApptById(id As Integer) As AppointmentC
        Using cn = GetConnection()
            Return cn.QuerySingleOrDefault(Of AppointmentC)(
                "SELECT * FROM dbo.AppointmentC WHERE AppointmentID = @id",
                New With {.id = id})
        End Using
    End Function

    ''' <summary>True when a row exists in dbo.AppointmentC (used before sending a queued WhatsApp reminder).</summary>
    Public Function AppointmentExists(appointmentId As Integer) As Boolean
        If appointmentId <= 0 Then Return False
        Try
            Using cn = GetConnection()
                Dim n = cn.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.AppointmentC WHERE AppointmentID = @id",
                    New With {.id = appointmentId})
                Return n > 0
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Function GetApptByStatus(status As String) As AppointmentC
        Using cn = GetConnection()
            Return cn.QuerySingleOrDefault(Of AppointmentC)(
                "SELECT * FROM dbo.AppointmentC WHERE Status = @status",
                New With {.status = status})
        End Using
    End Function

    ''' <summary>Earliest appointment at or after local <paramref name="fromLocal"/> for this patient, or Nothing.</summary>
    Public Function GetNextFutureAppointment(patientId As Integer, Optional fromLocal As DateTime? = Nothing) As AppointmentC
        If patientId <= 0 Then Return Nothing
        Dim fromDt = If(fromLocal, DateTime.Now)
        Try
            Using cn = GetConnection()
                Return cn.QueryFirstOrDefault(Of AppointmentC)(
                    "SELECT TOP 1 * FROM dbo.AppointmentC WHERE PatientId = @p AND StartDateTime >= @fromDt ORDER BY StartDateTime ASC",
                    New With {.p = patientId, .fromDt = fromDt})
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    Public Function GetByPatientId(id As Integer) As List(Of AppointmentC)
        Using cn = GetConnection()
            Return cn.Query(Of AppointmentC)(
                "SELECT * FROM dbo.AppointmentC WHERE PatientId = @id",
                New With {.id = id})
        End Using
    End Function

    Public Function GetByDrId(id As Integer) As List(Of AppointmentC)
        Using cn = GetConnection()
            Return cn.Query(Of AppointmentC)(
                "SELECT * FROM dbo.AppointmentC WHERE DrID = @id",
                New With {.id = id})
        End Using
    End Function

    Public Function GetByRange(start As DateTime, [end] As DateTime) As List(Of AppointmentC)
        Using cn = GetConnection()
            Return cn.Query(Of AppointmentC)(
                "SELECT * FROM dbo.AppointmentC 
                 WHERE NOT (EndDateTime <= @start OR StartDateTime >= @end)
                 ORDER BY StartDateTime",
                New With {.start = start, .[end] = [end]}).ToList()
        End Using
    End Function

    Public Function GetByDate(AppDate As DateTime) As List(Of AppointmentC)
        Using cn = GetConnection()
            Return cn.Query(Of AppointmentC)(
                "SELECT * FROM dbo.AppointmentC 
                 WHERE AppDate = CAST(@AppDate AS DATE)
                 ORDER BY StartDateTime",
                New With {.AppDate = AppDate}).ToList()
        End Using
    End Function

    Private Shared Sub ApplyReminderDtoPhone(d As AppointmentReminderDto)
        If d Is Nothing Then Return
        d.PatientPhone = WhatsHelper.BuildInternationalWhatsDigitsFromPatient(
            If(d.PatientWhatsLocal, ""),
            If(d.PatientPhoneFallback, ""),
            If(d.PatientWhatsAppPrefix, ""))
    End Sub

    Private Const ReminderPatientSelectSql As String =
                       "p.PatientName, " &
                       "p.Sex AS PatientSex, " &
                       "NULLIF(RTRIM(p.WhatsApp), N'') AS PatientWhatsLocal, " &
                       "NULLIF(RTRIM(p.Phone), N'') AS PatientPhoneFallback, " &
                       "NULLIF(RTRIM(p.WhatsAppPrefix), N'') AS PatientWhatsAppPrefix, " &
                       "d.DrName"

    Private Const ReminderAppointmentCols As String =
                       "a.AppointmentID, a.PatientID, a.DrID, a.StartDateTime, a.EndDateTime, a.Reason, a.Notes, a.Status, " &
                       "a.WhatsIncludeReason, a.WhatsIncludeNotes"

    ''' <summary>Get future appointments in a date range with patient and doctor info for WhatsApp reminders.</summary>
    ''' <param name="startDate">Start of period (inclusive).</param>
    ''' <param name="endDate">End of period (inclusive).</param>
    ''' <param name="doctorId">Optional doctor filter; Nothing = all doctors.</param>
    Public Function GetFutureAppointments(startDate As DateTime, endDate As DateTime, Optional doctorId As Integer? = Nothing) As List(Of AppointmentReminderDto)
        Using cn = GetConnection()
            Dim now = DateTime.Now
            Dim sql = "
                SELECT " & ReminderAppointmentCols & ",
                       " & ReminderPatientSelectSql & "
                FROM dbo.AppointmentC a
                INNER JOIN dbo.Patient p ON a.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON a.DrID = d.DrID
                WHERE a.StartDateTime >= @Now
                  AND CAST(a.StartDateTime AS DATE) >= @StartDate
                  AND CAST(a.StartDateTime AS DATE) <= @EndDate
                  AND (@DrID <= 0 OR a.DrID = @DrID)
                ORDER BY a.StartDateTime"
            Dim param = New With {
                .Now = now,
                .StartDate = startDate.Date,
                .EndDate = endDate.Date,
                .DrID = If(doctorId.HasValue, doctorId.Value, 0)
            }
            Dim lstReminder = cn.Query(Of AppointmentReminderDto)(sql, param).ToList()
            For Each rItem As AppointmentReminderDto In lstReminder
                ApplyReminderDtoPhone(rItem)
            Next
            Return lstReminder
        End Using
    End Function

    ''' <summary>Get appointments starting in ~24 hours (23h–25h from now) with patient and doctor info for WhatsApp reminders.</summary>
    Public Function GetAppointmentsFor24HourReminder() As List(Of AppointmentReminderDto)
        Using cn = GetConnection()
            Dim now = DateTime.Now
            Dim windowStart = now.AddHours(23)
            Dim windowEnd = now.AddHours(25)
            Dim sql = "
                SELECT " & ReminderAppointmentCols & ",
                       " & ReminderPatientSelectSql & "
                FROM dbo.AppointmentC a
                INNER JOIN dbo.Patient p ON a.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON a.DrID = d.DrID
                WHERE a.StartDateTime >= @WindowStart AND a.StartDateTime < @WindowEnd
                ORDER BY a.StartDateTime"
            Dim lstReminder = cn.Query(Of AppointmentReminderDto)(sql, New With {.WindowStart = windowStart, .WindowEnd = windowEnd}).ToList()
            For Each rItem As AppointmentReminderDto In lstReminder
                ApplyReminderDtoPhone(rItem)
            Next
            Return lstReminder
        End Using
    End Function

    ''' <summary>
    ''' Appointments whose start falls in a short window ~2 hours ahead (centre 120 min), so each visit gets one 2h reminder
    ''' enqueue when the clock reaches that band (use with a 1-minute poll). Excludes cancelled-like statuses.
    ''' </summary>
    Public Function GetAppointmentsForTwoHourReminder() As List(Of AppointmentReminderDto)
        Using cn = GetConnection()
            Dim now = DateTime.Now
            ' Enqueue when time-to-start is between ~110 and ~130 minutes (20-minute band around "2 hours before").
            Dim windowStart = now.AddMinutes(110)
            Dim windowEnd = now.AddMinutes(130)
            Dim sql = "
                SELECT " & ReminderAppointmentCols & ",
                       " & ReminderPatientSelectSql & "
                FROM dbo.AppointmentC a
                INNER JOIN dbo.Patient p ON a.PatientID = p.PatientID
                INNER JOIN dbo.Doctors d ON a.DrID = d.DrID
                WHERE a.StartDateTime >= @WindowStart AND a.StartDateTime <= @WindowEnd
                  AND LOWER(ISNULL(a.Status, N'')) NOT LIKE N'%cancel%'
                ORDER BY a.StartDateTime"
            Dim lstReminder = cn.Query(Of AppointmentReminderDto)(sql, New With {.WindowStart = windowStart, .WindowEnd = windowEnd}).ToList()
            For Each rItem As AppointmentReminderDto In lstReminder
                ApplyReminderDtoPhone(rItem)
            Next
            Return lstReminder
        End Using
    End Function

    Public Function GetAppointmentReminderById(appointmentId As Integer) As AppointmentReminderDto
        Using cn = GetConnection()
            Dim sql = "
                SELECT " & ReminderAppointmentCols & ",
                       " & ReminderPatientSelectSql & "
                FROM dbo.AppointmentC a
                LEFT JOIN dbo.Patient p ON a.PatientID = p.PatientID
                LEFT JOIN dbo.Doctors d ON a.DrID = d.DrID
                WHERE a.AppointmentID = @Id"
            Dim rDto = cn.QueryFirstOrDefault(Of AppointmentReminderDto)(sql, New With {.Id = appointmentId})
            ApplyReminderDtoPhone(rDto)
            Return rDto
        End Using
    End Function

    Public Sub UpdateAppointmentStatus(id As Integer, newStatus As String)
        Using con = GetConnection()
            con.Execute("UPDATE AppointmentC SET Status = @Status WHERE AppointmentID = @ID",
                     New With {.Status = newStatus, .ID = id})
        End Using
        If id > 0 Then AppointmentWhatsAppQueueService.NotifyAppointmentSaved(id)
    End Sub

#Region "Basic CRUD OperationsOld"

    'Public Function GetFiltered(start As DateTime, [end] As DateTime, Optional patientId As Integer? = Nothing, Optional drId As Integer? = Nothing, Optional reasonLike As String = Nothing) As List(Of AppointmentC)
    '    Using cn = GetConnection()
    '        Dim sql = New System.Text.StringBuilder()
    '        sql.AppendLine("SELECT * FROM dbo.AppointmentC WHERE NOT (EndDateTime <= @start OR StartDateTime >= @end) ")

    '        If patientId.HasValue Then sql.AppendLine(" AND PatientID = @patientId")
    '        If drId.HasValue Then sql.AppendLine(" AND DrID = @drId")
    '        If Not String.IsNullOrWhiteSpace(reasonLike) Then sql.AppendLine(" AND Reason LIKE @reasonLike")

    '        sql.AppendLine("ORDER BY StartDateTime")

    '        Return cn.Query(Of AppointmentC)(sql.ToString(), New With {
    '            .start = start,
    '            .[end] = [end],
    '            .patientId = If(patientId, Nothing),
    '            .drId = If(drId, Nothing),
    '            .reasonLike = If(String.IsNullOrWhiteSpace(reasonLike), Nothing, "%" & reasonLike & "%")
    '        }).ToList()
    '    End Using
    'End Function

    'Public Function Insert(appt As AppointmentC) As Integer
    '    Using cn = GetConnection()
    '        Dim sql = "INSERT INTO dbo.AppointmentC (PatientID, DrID, StartDateTime, EndDateTime, Reason, Notes, CreatedBy, CreatedAt)
    '                   VALUES (@PatientID, @DrID, @StartDateTime, @EndDateTime, @Reason, @Notes, @CreatedBy, @CreatedAt);
    '                   SELECT CAST(SCOPE_IDENTITY() AS INT)"
    '        Return cn.QuerySingle(Of Integer)(sql, New With {
    '            appt.PatientID, appt.DrID, appt.StartDateTime, appt.EndDateTime, appt.Reason, appt.Notes, appt.CreatedBy, appt.CreatedAt
    '        })
    '    End Using
    'End Function

    'Public Function Update(appt As AppointmentC) As Integer
    '    Using cn = GetConnection()
    '        Dim sql = "UPDATE dbo.AppointmentC SET
    '                    PatientID = @PatientID,
    '                    DrID = @DrID,
    '                    StartDateTime = @StartDateTime,
    '                    EndDateTime = @EndDateTime,
    '                    Reason = @Reason,
    '                    Notes = @Notes
    '                   WHERE AppointmentCID = @AppointmentCID"
    '        Return cn.Execute(sql, appt)
    '    End Using
    'End Function

    'Public Function Delete(apptId As Integer) As Integer
    '    Using cn = GetConnection()
    '        Return cn.Execute("DELETE FROM dbo.AppointmentC WHERE AppointmentCID = @id", New With {.id = apptId})
    '    End Using
    '    End Function

#End Region

#Region "Helper Methods for Scheduler Integration"

    Public Function AppointmentOverlaps(conn As SqlConnection, drId As Integer, patientId As Integer, startDt As DateTime, endDt As DateTime, Optional excludeId As Integer? = Nothing) As Boolean
        Dim sql As String = "
    SELECT COUNT(1)
    FROM dbo.AppointmentC
    WHERE (
        -- Check for doctor overlap (same doctor, overlapping time)
        (DrID = @DrID AND NOT (EndDateTime <= @StartDt OR StartDateTime >= @EndDt))
        OR 
        -- Check for patient overlap (same patient, overlapping time with ANY doctor)
        (PatientID = @PatientID AND NOT (EndDateTime <= @StartDt OR StartDateTime >= @EndDt))
    )
    " & If(excludeId.HasValue, "AND AppointmentID <> @AppointmentID", "") & ";"

        Return conn.ExecuteScalar(Of Integer)(sql, New With {
        .DrID = drId,
        .PatientID = patientId,
        .StartDt = startDt,
        .EndDt = endDt,
        .AppointmentID = If(excludeId, 0)
    }) > 0
    End Function

    Public Function GetOverlappingAppointments(conn As SqlConnection, drId As Integer, patientId As Integer, startDt As DateTime, endDt As DateTime, Optional excludeId As Integer? = Nothing) As List(Of String)
        ' Same-day only: avoid false positives from date/time boundaries
        Dim sql As String = "
    SELECT 
        'Doctor: ' + COALESCE(NULLIF(RTRIM(ISNULL(d.DrName, N'')), N''), N'ID ' + CAST(a.DrID AS NVARCHAR(20)))
        + ', Patient: ' + COALESCE(NULLIF(RTRIM(ISNULL(p.PatientName, N'')), N''), N'ID ' + CAST(a.PatientID AS NVARCHAR(20)))
        + ', Time: ' + CONVERT(NVARCHAR(16), a.StartDateTime, 120) + ' to ' + CONVERT(NVARCHAR(16), a.EndDateTime, 120) AS OverlapInfo
    FROM dbo.AppointmentC a
    INNER JOIN dbo.Doctors d ON a.DrID = d.DrID
    INNER JOIN dbo.Patient p ON a.PatientID = p.PatientID
    WHERE CAST(a.StartDateTime AS DATE) = CAST(@StartDt AS DATE)
    AND (
        (a.DrID = @DrID AND NOT (a.EndDateTime <= @StartDt OR a.StartDateTime >= @EndDt))
        OR 
        (a.PatientID = @PatientID AND NOT (a.EndDateTime <= @StartDt OR a.StartDateTime >= @EndDt))
    )
    " & If(excludeId.HasValue, "AND a.AppointmentID <> @AppointmentID", "") & ";"

        Return conn.Query(Of String)(sql, New With {
        .DrID = drId,
        .PatientID = patientId,
        .StartDt = startDt,
        .EndDt = endDt,
        .AppointmentID = If(excludeId, 0)
    }).ToList()
    End Function

    Public Function GetOverlappingAppointmentsTransactional(conn As SqlConnection, trans As SqlTransaction, drId As Integer, patientId As Integer, startDt As DateTime, endDt As DateTime, Optional excludeId As Integer? = Nothing) As List(Of String)
        Dim sql As String = "
    SELECT 
        'Doctor: ' + COALESCE(NULLIF(RTRIM(ISNULL(d.DrName, N'')), N''), N'ID ' + CAST(a.DrID AS NVARCHAR(20)))
        + ', Patient: ' + COALESCE(NULLIF(RTRIM(ISNULL(p.PatientName, N'')), N''), N'ID ' + CAST(a.PatientID AS NVARCHAR(20)))
        + ', Time: ' + CONVERT(NVARCHAR(16), a.StartDateTime, 120) + ' to ' + CONVERT(NVARCHAR(16), a.EndDateTime, 120) AS OverlapInfo
    FROM dbo.AppointmentC a WITH (UPDLOCK, HOLDLOCK)
    INNER JOIN dbo.Doctors d ON a.DrID = d.DrID
    INNER JOIN dbo.Patient p ON a.PatientID = p.PatientID
    WHERE CAST(a.StartDateTime AS DATE) = CAST(@StartDt AS DATE)
    AND (
        (a.DrID = @DrID AND NOT (a.EndDateTime <= @StartDt OR a.StartDateTime >= @EndDt))
        OR 
        (a.PatientID = @PatientID AND NOT (a.EndDateTime <= @StartDt OR a.StartDateTime >= @EndDt))
    )
    " & If(excludeId.HasValue, "AND a.AppointmentID <> @AppointmentID", "") & ";"

        Return conn.Query(Of String)(sql, New With {
        .DrID = drId,
        .PatientID = patientId,
        .StartDt = startDt,
        .EndDt = endDt,
        .AppointmentID = If(excludeId, 0)
    }, transaction:=trans).ToList()
    End Function



    Public Function GetFiltered(Optional startDate As DateTime? = Nothing,
                                Optional endDate As DateTime? = Nothing,
                                Optional patientId As Integer? = Nothing,
                                Optional doctorId As Integer? = Nothing,
                                Optional reason As String = Nothing,
                                Optional status As String = Nothing) As List(Of AppointmentC)

        Using conn = GetConnection()
            Dim sql As String =
"SELECT 
    AppointmentID,
    PatientID,
    DrID,
    StartDateTime,
    EndDateTime,
    Reason,
    Notes,
    Status,
    CreatedBy,
    CreatedAt,
    AppDate,
    WhatsIncludeReason,
    WhatsIncludeNotes
FROM dbo.AppointmentC
WHERE 1=1"

            Dim parameters As New DynamicParameters()

            ' Apply filters dynamically — interval overlap (real times) OR calendar AppDate in [Start,End) so rows
            ' booked on a day (AppDate) are not lost when StartDateTime/EndDateTime do not fall inside the range.
            If startDate.HasValue AndAlso endDate.HasValue Then
                sql &= " AND ( NOT (EndDateTime <= @StartDate OR StartDateTime >= @EndDate) " &
                    "OR (AppDate >= @StartDate AND AppDate < @EndDate) )"
                parameters.Add("@StartDate", startDate.Value)
                parameters.Add("@EndDate", endDate.Value)
            ElseIf startDate.HasValue Then
                sql &= " AND AppDate >= @StartDate"
                parameters.Add("@StartDate", startDate.Value.Date)
            ElseIf endDate.HasValue Then
                sql &= " AND AppDate <= @EndDate"
                parameters.Add("@EndDate", endDate.Value.Date)
            End If

            If patientId.HasValue Then
                sql &= " AND PatientID = @PatientID"
                parameters.Add("@PatientID", patientId.Value)
            End If

            If doctorId.HasValue Then
                sql &= " AND DrID = @DrID"
                parameters.Add("@DrID", doctorId.Value)
            End If

            If Not String.IsNullOrWhiteSpace(reason) Then
                sql &= " AND Reason LIKE @Reason"
                parameters.Add("@Reason", "%" & reason & "%")
            End If
            If Not String.IsNullOrWhiteSpace(status) Then
                sql &= " AND Status = @Status"
                parameters.Add("@Status", status)
            End If
            sql &= " ORDER BY StartDateTime;"

            Dim result = conn.Query(Of AppointmentC)(sql, parameters).ToList()

            ' Guarantee AppDate is never MinValue (safety)
            For Each ap In result
                If ap.AppDate = Date.MinValue Then
                    ap.AppDate = ap.StartDateTime.Date
                End If
            Next

            Return result
        End Using
    End Function


    Public Function GetFiltered1(start As DateTime, [end] As DateTime,
                                Optional patientId As Integer? = Nothing,
                                Optional drId As Integer? = Nothing,
                                Optional reasonLike As String = Nothing) As List(Of AppointmentC)
        Using cn = GetConnection()
            Dim sb As New Text.StringBuilder()
            sb.AppendLine("SELECT  AppointmentID, PatientID, DrID, AppDate, StartDateTime, EndDateTime, Reason, Notes, Status, CreatedBy, CreatedAt, WhatsIncludeReason, WhatsIncludeNotes
                            FROM dbo.AppointmentC")
            sb.AppendLine("WHERE NOT (EndDateTime <= @start OR StartDateTime >= @end)")
            If patientId.HasValue Then sb.AppendLine(" AND PatientID = @patientId")
            If drId.HasValue Then sb.AppendLine(" AND DrID = @drId")
            If Not String.IsNullOrWhiteSpace(reasonLike) Then sb.AppendLine(" AND Reason LIKE @reason")
            sb.AppendLine("ORDER BY StartDateTime")
            Dim param = New With {
                .start = start,
                .[end] = [end],
                .patientId = If(patientId, Nothing),
                .drId = If(drId, Nothing),
                .reason = If(String.IsNullOrWhiteSpace(reasonLike), Nothing, "%" & reasonLike & "%")
            }
            Return cn.Query(Of AppointmentC)(sb.ToString(), param).ToList()
        End Using
    End Function


    Public Function Insert(appt As AppointmentC, Optional reminderMessageEnglish As Boolean? = Nothing) As Integer
        Using cn = GetConnection()
            Dim sql = "INSERT INTO dbo.AppointmentC (PatientID, DrID, AppDate, StartDateTime, EndDateTime, Reason, Notes, Status, CreatedBy, CreatedAt, WhatsIncludeReason, WhatsIncludeNotes)
                       VALUES (@PatientID, @DrID, @AppDate, @StartDateTime, @EndDateTime, @Reason, @Notes, @Status, @CreatedBy, @CreatedAt, @WhatsIncludeReason, @WhatsIncludeNotes);
                       SELECT CAST(SCOPE_IDENTITY() AS INT);"
            Dim newId = cn.QuerySingle(Of Integer)(sql, appt)
            ApptTwoHourReminderQueueRepository.SyncFromAppointmentId(newId, reminderMessageEnglish)
            Return newId
        End Using
    End Function

    Public Function InsertTransactional(conn As SqlConnection, trans As SqlTransaction, appt As AppointmentC) As Integer
        Dim sql = "INSERT INTO dbo.AppointmentC (PatientID, DrID, AppDate, StartDateTime, EndDateTime, Reason, Notes, Status, CreatedBy, CreatedAt, WhatsIncludeReason, WhatsIncludeNotes)
                   VALUES (@PatientID, @DrID, @AppDate, @StartDateTime, @EndDateTime, @Reason, @Notes, @Status, @CreatedBy, @CreatedAt, @WhatsIncludeReason, @WhatsIncludeNotes);
                   SELECT CAST(SCOPE_IDENTITY() AS INT);"
        Return conn.QuerySingle(Of Integer)(sql, appt, transaction:=trans)
    End Function
    Public Function UpdateAppointmentTimes(AppID As Integer, newStart As DateTime, newEnd As DateTime) As Integer
        Using cn = GetConnection()
            Dim appt As AppointmentC = GetApptById(AppID)
            appt.StartDateTime = newStart
            appt.EndDateTime = newEnd
            Dim sql = "UPDATE dbo.AppointmentC SET
                        PatientID = @PatientID,
                        DrID = @DrID,
                        AppDate = @AppDate,
                        StartDateTime = @StartDateTime,
                        EndDateTime = @EndDateTime,
                        Reason = @Reason,
                        Notes = @Notes,
                        Status = @Status,
                        WhatsIncludeReason = @WhatsIncludeReason,
                        WhatsIncludeNotes = @WhatsIncludeNotes
                       WHERE AppointmentID = @AppointmentID"
            Dim n = cn.Execute(sql, appt)
            If n > 0 AndAlso AppID > 0 Then ApptTwoHourReminderQueueRepository.SyncFromAppointmentId(AppID)
            Return n
        End Using
    End Function



    Public Function Update(appt As AppointmentC, Optional reminderMessageEnglish As Boolean? = Nothing) As Integer
        Using cn = GetConnection()
            Dim sql = "UPDATE dbo.AppointmentC SET
                        PatientID = @PatientID,
                        DrID = @DrID,
                        AppDate = @AppDate,
                        StartDateTime = @StartDateTime,
                        EndDateTime = @EndDateTime,
                        Reason = @Reason,
                        Notes = @Notes,
                        Status = @Status,
                        WhatsIncludeReason = @WhatsIncludeReason,
                        WhatsIncludeNotes = @WhatsIncludeNotes
                       WHERE AppointmentID = @AppointmentID"
            Dim n = cn.Execute(sql, appt)
            If n > 0 AndAlso appt.AppointmentID > 0 Then ApptTwoHourReminderQueueRepository.SyncFromAppointmentId(appt.AppointmentID, reminderMessageEnglish)
            Return n
        End Using
    End Function

    Public Function UpdateTransactional(conn As SqlConnection, trans As SqlTransaction, appt As AppointmentC) As Integer
        Dim sql = "UPDATE dbo.AppointmentC SET
                    PatientID = @PatientID,
                    DrID = @DrID,
                    AppDate = @AppDate,
                    StartDateTime = @StartDateTime,
                    EndDateTime = @EndDateTime,
                    Reason = @Reason,
                    Notes = @Notes,
                    Status = @Status,
                    WhatsIncludeReason = @WhatsIncludeReason,
                    WhatsIncludeNotes = @WhatsIncludeNotes
                   WHERE AppointmentID = @AppointmentID"
        Return conn.Execute(sql, appt, transaction:=trans)
    End Function

    ''' <summary>Deletes the appointment row. Removes dbo.ApptTwoHourWhatsAppQueue for this id first, then cleans orphan queue rows and wakes the reminder processor.</summary>
    Public Function Delete(apptId As Integer) As Integer
        ApptTwoHourReminderQueueRepository.DeleteByAppointmentId(apptId)
        Dim n As Integer
        Using cn = GetConnection()
            n = cn.Execute("DELETE FROM dbo.AppointmentC WHERE AppointmentID = @id", New With {.id = apptId})
        End Using
        ApptTwoHourReminderQueueRepository.DeleteOrphansMissingAppointment()
        ApptTwoHourReminderQueueRepository.BumpProcessingAfterQueueChange()
        Return n
    End Function

#End Region
End Class

