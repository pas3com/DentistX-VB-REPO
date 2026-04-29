Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Globalization
Imports Dapper
Imports System

''' <summary>
''' One queue row per appointment: scheduled send times for 24h and short-lead WhatsApp reminders, independent statuses.
''' Short-lead hours come from user setting ShortReminder (see GetShortReminderHours).
''' Rows are created/updated when <see cref="AppointmentC"/> is saved (start beyond &quot;now + short lead + buffer&quot;, not cancelled).
''' </summary>
Public NotInheritable Class ApptTwoHourReminderQueueRepository
    ''' <summary>Hours before appointment start for the short WhatsApp leg; from My.Settings.ShortReminder, clamped to 1–72 (invalid → 2).</summary>
    Public Shared Function GetShortReminderHours() As Double
        Try
            Dim h As Double = CDbl(My.Settings.ShortReminder)
            If h < 1R OrElse Double.IsNaN(h) OrElse Double.IsInfinity(h) Then Return 2R
            If h > 72R Then Return 72R
            Return h
        Catch
            Return 2R
        End Try
    End Function

    ''' <summary>Arabic duration phrase for the current <see cref="GetShortReminderHours"/> (floored), e.g. «نحو ساعتين».</summary>
    Public Shared Function FormatArabicApproximateDurationFromShortReminderSetting() As String
        Dim hrs = CInt(Math.Floor(GetShortReminderHours()))
        If hrs < 1 Then hrs = 1
        Return FormatArabicApproximateHoursCore(hrs)
    End Function

    Private Shared Function FormatArabicApproximateHoursCore(hours As Integer) As String
        If hours < 1 Then hours = 1
        Select Case hours
            Case 1
                Return "نحو ساعة"
            Case 2
                Return "نحو ساعتين"
            Case 3 To 10
                Return "نحو " & hours.ToString() & " ساعات"
            Case Else
                Return "نحو " & hours.ToString() & " ساعة"
        End Select
    End Function

    ''' <summary>Short-lead checkbox caption; hours from <see cref="GetShortReminderHours"/> (same as WhatsApp short-leg title).</summary>
    Public Shared Function GetShortLeadReminderCheckboxCaption(useEnglishUi As Boolean) As String
        Dim hRaw = GetShortReminderHours()
        Dim hInt = CInt(Math.Floor(hRaw))
        If hInt < 1 Then hInt = 1
        If useEnglishUi Then
            If Math.Abs(hRaw - hInt) > 0.05R Then
                Return "Send reminder ~" & hRaw.ToString("0.#", CultureInfo.InvariantCulture) & " hours before start"
            End If
            If hInt = 1 Then Return "Send reminder ~1 hour before start"
            Return "Send reminder ~" & hInt.ToString() & " hours before start"
        End If
        Return "إرسال تذكير قبل " & FormatArabicApproximateHoursCore(hInt) & " من البدء"
    End Function

    ''' <summary>Only queue when appointment start is later than <c>Now + short reminder hours + 0.5</c>.</summary>
    Public Shared Function GetMinLeadHoursBeforeApptForQueue() As Double
        Return GetShortReminderHours() + 0.5R
    End Function

    Public Const Kind24Hour As String = "24h"
    Public Const Kind2Hour As String = "2h"

    Public Const StatusPending As String = "Pending"
    Public Const StatusSent As String = "Sent"
    Public Const StatusFailed As String = "Failed"
    Public Const StatusSkippedNoPhone As String = "SkippedNoPhone"
    ''' <summary>Send time was already past when scheduled or sync ran.</summary>
    Public Const StatusMissed As String = "Missed"
    ''' <summary>Leg not scheduled on last save (user left reminder type unchecked).</summary>
    Public Const StatusSkippedNotRequested As String = "SkippedNotRequested"

    ''' <summary>Last SyncFromAppointmentId failure (empty if OK).</summary>
    Public Shared LastSyncError As String = ""

    Private Shared Function IsCancelledStatus(status As String) As Boolean
        If String.IsNullOrWhiteSpace(status) Then Return False
        Dim s = status.Trim().ToLowerInvariant()
        Return s.Contains("cancel") OrElse s = "completed"
    End Function

    ''' <summary>Remove queue row when appointment deleted or no longer qualifies.</summary>
    Public Shared Sub DeleteByAppointmentId(appointmentId As Integer)
        If appointmentId <= 0 Then Return
        Try
            Using cn As New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
                cn.Execute("DELETE FROM dbo.ApptTwoHourWhatsAppQueue WHERE AppointmentId = @id", New With {.id = appointmentId})
            End Using
        Catch
        End Try
    End Sub

    ''' <summary>Removes queue rows whose appointment no longer exists (e.g. deleted bypassing <see cref="AppointmentCRepository.Delete"/>).</summary>
    Public Shared Sub DeleteOrphansMissingAppointment()
        Try
            Using cn As New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
                cn.Execute(
                    "DELETE q FROM dbo.ApptTwoHourWhatsAppQueue q WHERE NOT EXISTS (SELECT 1 FROM dbo.AppointmentC a WHERE a.AppointmentID = q.AppointmentId)")
            End Using
        Catch ex As Exception
            Trace.TraceError("ApptTwoHourWhatsAppQueue DeleteOrphansMissingAppointment: " & ex.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Call after any synchronous change to dbo.ApptTwoHourWhatsAppQueue so due legs are re-evaluated without waiting for the 60s timer.
    ''' (Also invoked at the end of <see cref="SyncFromAppointmentId"/>.)
    ''' </summary>
    Public Shared Sub BumpProcessingAfterQueueChange()
        ApptWhatsAppReminderBackgroundPoller.EnsureStarted()
        ApptWhatsAppReminderBackgroundPoller.RequestImmediatePoll()
    End Sub

    ''' <summary>True when live appointment start matches the snapshot stored on the queue row (allows sub-second SQL rounding).</summary>
    Public Shared Function AppointmentStartMatchesQueueSnapshot(apptStart As DateTime, snapshot As DateTime) As Boolean
        Return Math.Abs((apptStart - snapshot).TotalSeconds) < 2R
    End Function

    Private Shared Function LegWasRequestedOnQueue(status As String) As Boolean
        If String.IsNullOrWhiteSpace(status) Then Return True
        Return Not String.Equals(status.Trim(), StatusSkippedNotRequested, StringComparison.OrdinalIgnoreCase)
    End Function

    ''' <summary>
    ''' Re-runs <see cref="SyncFromAppointmentId"/> after the appointment time changed, keeping the same 24h/2h inclusion
    ''' as the current queue row (not the default checkboxes).
    ''' </summary>
    Public Shared Sub ResyncFromAppointmentPreservingLegInclusion(row As ApptTwoHourReminderQueueRow)
        If row Is Nothing OrElse row.AppointmentId <= 0 Then Return
        Dim incl24 = LegWasRequestedOnQueue(row.Status24h)
        Dim incl2 = LegWasRequestedOnQueue(row.Status2h)
        SyncFromAppointmentId(row.AppointmentId, Nothing, incl24, incl2)
    End Sub

    ''' <summary>After <see cref="AppointmentC"/> insert/update. Inserts/updates one row if start is far enough in the future (see <see cref="GetMinLeadHoursBeforeApptForQueue"/>); otherwise deletes queue row.</summary>
    ''' <param name="reminderMessageEnglish">From editor RadioLang when saving; Nothing uses global Eng for 24h/2h preview text.</param>
    ''' <param name="include24HourReminder">When false, the 24h leg is not queued (send time cleared, status SkippedNotRequested).</param>
    ''' <param name="includeShortLeadReminder">When false, the short-lead (e.g. 2h) leg is not queued.</param>
    Public Shared Sub SyncFromAppointmentId(appointmentId As Integer, Optional reminderMessageEnglish As Boolean? = Nothing,
                                         Optional include24HourReminder As Boolean = True, Optional includeShortLeadReminder As Boolean = True)
        LastSyncError = ""
        If appointmentId <= 0 Then Return

        Dim apptRepo As New AppointmentCRepository()
        Dim dto = apptRepo.GetAppointmentReminderById(appointmentId)
        If dto Is Nothing Then
            DeleteByAppointmentId(appointmentId)
            BumpProcessingAfterQueueChange()
            Return
        End If

        If IsCancelledStatus(dto.Status) Then
            DeleteByAppointmentId(appointmentId)
            BumpProcessingAfterQueueChange()
            Return
        End If

        Dim now = DateTime.Now
        If dto.StartDateTime <= now.AddHours(GetMinLeadHoursBeforeApptForQueue()) Then
            DeleteByAppointmentId(appointmentId)
            BumpProcessingAfterQueueChange()
            Return
        End If

        If Not include24HourReminder AndAlso Not includeShortLeadReminder Then
            DeleteByAppointmentId(appointmentId)
            BumpProcessingAfterQueueChange()
            Return
        End If

        Dim send24 = dto.StartDateTime.AddHours(-24)
        Dim shortLeadH = GetShortReminderHours()
        Dim send2 = dto.StartDateTime.AddHours(-shortLeadH)
        Dim msg24 = AppointmentReminderService.BuildReminderMessageBody(dto, reminderMessageEnglish)
        Dim msg2 = AppointmentTwoHourReminderService.BuildTwoHourReminderBody(dto, reminderMessageEnglish)
        Dim sendAt24 As DateTime? = Nothing
        Dim sendAt2 As DateTime? = Nothing
        Dim status24 As String = StatusSkippedNotRequested
        Dim status2 As String = StatusSkippedNotRequested
        If include24HourReminder Then
            sendAt24 = send24
            status24 = If(send24 > now, StatusPending, StatusMissed)
        Else
            msg24 = ""
        End If
        If includeShortLeadReminder Then
            sendAt2 = send2
            status2 = If(send2 > now, StatusPending, StatusMissed)
        Else
            msg2 = ""
        End If
        Dim clinicId = If(String.IsNullOrWhiteSpace(WhatsAppService.GetCurrentClinicId()), "", WhatsAppService.GetCurrentClinicId())
        Dim phoneDigits As String = If(String.IsNullOrWhiteSpace(dto.PatientPhone), "", dto.PatientPhone.Trim())
        If String.IsNullOrWhiteSpace(phoneDigits) Then
            phoneDigits = WhatsHelper.BuildInternationalWhatsDigitsFromPatient(
                If(dto.PatientWhatsLocal, ""),
                If(dto.PatientPhoneFallback, ""),
                If(dto.PatientWhatsAppPrefix, ""))
        End If
        phoneDigits = WhatsHelper.NormalizeWhatsDigits(phoneDigits)
        ' Re-read Patient row if still empty (avoids rare DTO mapping / timing gaps on first insert)
        If String.IsNullOrWhiteSpace(phoneDigits) AndAlso dto.PatientID > 0 Then
            Try
                Using cnR As New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
                    Dim pr = cnR.QueryFirstOrDefault(Of PatientWhatsRawRow)(
                        "SELECT NULLIF(RTRIM(WhatsApp), N'') AS WhatsApp, NULLIF(RTRIM(Phone), N'') AS Phone, NULLIF(RTRIM(WhatsAppPrefix), N'') AS WhatsAppPrefix FROM dbo.Patient WHERE PatientID = @id",
                        New With {.id = dto.PatientID})
                    If pr IsNot Nothing Then
                        phoneDigits = WhatsHelper.NormalizeWhatsDigits(
                            WhatsHelper.BuildInternationalWhatsDigitsFromPatient(If(pr.WhatsApp, ""), If(pr.Phone, ""), If(pr.WhatsAppPrefix, "")))
                    End If
                End Using
            Catch
            End Try
        End If
        Dim targetPhoneSqlVal As Object = If(String.IsNullOrWhiteSpace(phoneDigits), CType(DBNull.Value, Object), CType(phoneDigits, Object))

        Try
            Using cn As New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
                Dim existing = cn.QueryFirstOrDefault(Of ApptTwoHourReminderQueueRow)(
                    "SELECT QueueId, ApptStartSnapshot, Status24h, Status2h FROM dbo.ApptTwoHourWhatsAppQueue WHERE AppointmentId = @aid",
                    New With {.aid = appointmentId})

                If existing IsNot Nothing AndAlso existing.ApptStartSnapshot = dto.StartDateTime Then
                    If include24HourReminder Then
                        status24 = PreserveOrReevaluateStatus(existing.Status24h, send24, now, status24)
                    End If
                    If includeShortLeadReminder Then
                        status2 = PreserveOrReevaluateStatus(existing.Status2h, send2, now, status2)
                    End If
                End If

                If existing Is Nothing Then
                    cn.Execute("
INSERT INTO dbo.ApptTwoHourWhatsAppQueue (
  AppointmentId, ApptStartSnapshot, PatientId, PatientName, DrName, TargetPhone,
  MessagePreview24h, MessagePreview2h, SendAt24h, SendAt2h, Status24h, Status2h, ClinicId)
VALUES (
  @AppointmentId, @ApptStartSnapshot, @PatientId, @PatientName, @DrName, @TargetPhone,
  @MessagePreview24h, @MessagePreview2h, @SendAt24h, @SendAt2h, @Status24h, @Status2h, @ClinicId)",
                        New With {
                            .AppointmentId = appointmentId,
                            .ApptStartSnapshot = dto.StartDateTime,
                            .PatientId = dto.PatientID,
                            .PatientName = Trunc(dto.PatientName, 200),
                            .DrName = Trunc(dto.DrName, 200),
                            .TargetPhone = targetPhoneSqlVal,
                            .MessagePreview24h = Trunc(msg24, 500),
                            .MessagePreview2h = Trunc(msg2, 500),
                            .SendAt24h = sendAt24,
                            .SendAt2h = sendAt2,
                            .Status24h = status24,
                            .Status2h = status2,
                            .ClinicId = clinicId
                        })
                Else
                    Dim snapChanged = existing.ApptStartSnapshot <> dto.StartDateTime
                    If snapChanged Then
                        cn.Execute("
UPDATE dbo.ApptTwoHourWhatsAppQueue SET
  ApptStartSnapshot = @ApptStartSnapshot,
  PatientId = @PatientId, PatientName = @PatientName, DrName = @DrName, TargetPhone = @TargetPhone,
  MessagePreview24h = @MessagePreview24h, MessagePreview2h = @MessagePreview2h,
  SendAt24h = @SendAt24h, SendAt2h = @SendAt2h,
  Status24h = @Status24h, Status2h = @Status2h,
  Processed24hAtUtc = NULL, Processed2hAtUtc = NULL, Error24h = NULL, Error2h = NULL,
  WhatsAppLogId24h = NULL, WhatsAppLogId2h = NULL,
  ClinicId = @ClinicId
WHERE AppointmentId = @AppointmentId",
                            New With {
                                .AppointmentId = appointmentId,
                                .ApptStartSnapshot = dto.StartDateTime,
                                .PatientId = dto.PatientID,
                                .PatientName = Trunc(dto.PatientName, 200),
                                .DrName = Trunc(dto.DrName, 200),
                                .TargetPhone = targetPhoneSqlVal,
                                .MessagePreview24h = Trunc(msg24, 500),
                                .MessagePreview2h = Trunc(msg2, 500),
                                .SendAt24h = sendAt24,
                                .SendAt2h = sendAt2,
                                .Status24h = status24,
                                .Status2h = status2,
                                .ClinicId = clinicId
                            })
                    Else
                        cn.Execute("
UPDATE dbo.ApptTwoHourWhatsAppQueue SET
  PatientId = @PatientId, PatientName = @PatientName, DrName = @DrName, TargetPhone = @TargetPhone,
  MessagePreview24h = @MessagePreview24h, MessagePreview2h = @MessagePreview2h,
  SendAt24h = @SendAt24h, SendAt2h = @SendAt2h,
  Status24h = @Status24h, Status2h = @Status2h,
  ClinicId = @ClinicId
WHERE AppointmentId = @AppointmentId",
                            New With {
                                .AppointmentId = appointmentId,
                                .PatientId = dto.PatientID,
                                .PatientName = Trunc(dto.PatientName, 200),
                                .DrName = Trunc(dto.DrName, 200),
                                .TargetPhone = targetPhoneSqlVal,
                                .MessagePreview24h = Trunc(msg24, 500),
                                .MessagePreview2h = Trunc(msg2, 500),
                                .SendAt24h = sendAt24,
                                .SendAt2h = sendAt2,
                                .Status24h = status24,
                                .Status2h = status2,
                                .ClinicId = clinicId
                            })
                    End If
                End If
            End Using
            DeleteOrphansMissingAppointment()
            BumpProcessingAfterQueueChange()
        Catch ex As Exception
            LastSyncError = ex.ToString()
            Trace.TraceError("ApptTwoHourWhatsAppQueue SyncFromAppointmentId: " & ex.ToString())
        End Try
    End Sub

    Private Shared Function PreserveOrReevaluateStatus(current As String, sendAt As DateTime, now As DateTime, computedMissedOrPending As String) As String
        If String.IsNullOrWhiteSpace(current) Then Return computedMissedOrPending
        If current = StatusSent OrElse current = StatusFailed OrElse current = StatusSkippedNoPhone Then Return current
        If current = StatusMissed Then Return current
        If current = StatusSkippedNotRequested Then Return computedMissedOrPending
        If current = StatusPending AndAlso sendAt <= now Then Return StatusMissed
        Return current
    End Function

    Private Shared ReadOnly SelectCols As String =
        "QueueId, AppointmentId, ApptStartSnapshot, PatientId, PatientName, DrName, TargetPhone, " &
        "MessagePreview24h, MessagePreview2h, SendAt24h, SendAt2h, Status24h, Status2h, " &
        "Processed24hAtUtc, Processed2hAtUtc, Error24h, Error2h, WhatsAppLogId24h, WhatsAppLogId2h, ClinicId, CreatedAtUtc"

    ''' <summary>Rows that have at least one reminder leg due now and still pending (uses app clock, not only SQL GETDATE()).</summary>
    Public Shared Function GetRowsWithDueReminders() As List(Of ApptTwoHourReminderQueueRow)
        Try
            Dim nowLocal = DateTime.Now
            Using cn As New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
                Dim sql = "SELECT " & SelectCols & " FROM dbo.ApptTwoHourWhatsAppQueue WHERE " &
                    "(Status24h = N'Pending' AND SendAt24h IS NOT NULL AND SendAt24h <= @Now) OR " &
                    "(Status2h = N'Pending' AND SendAt2h IS NOT NULL AND SendAt2h <= @Now) " &
                    "ORDER BY AppointmentId"
                Return cn.Query(Of ApptTwoHourReminderQueueRow)(sql, New With {.Now = nowLocal}).ToList()
            End Using
        Catch
            Return New List(Of ApptTwoHourReminderQueueRow)
        End Try
    End Function

    ''' <summary>
    ''' Closes out legs that are still Pending but the appointment has already started (or send time is far in the past).
    ''' Runs even when WhatsApp is disconnected so Status does not stay Pending forever.
    ''' <para><b>Column lifecycle:</b> While Pending, Processed*AtUtc / Error* / WhatsAppLogId* stay NULL until send is attempted.
    ''' On Sent/Failed/Missed/SkippedNoPhone, TryFinalize* sets Processed*AtUtc, Error* (optional), and log id when sent.</para>
    ''' </summary>
    Public Shared Sub FinalizeOverduePendingLegs()
        Const msg As String = "Missed: appointment time passed or reminder was not sent while due (e.g. WhatsApp offline)."
        Try
            Using cn As New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
                cn.Execute(
                    "UPDATE dbo.ApptTwoHourWhatsAppQueue SET Status24h = N'Missed', Processed24hAtUtc = SYSUTCDATETIME(), Error24h = @Msg " &
                    "WHERE Status24h = N'Pending' AND (ApptStartSnapshot <= GETDATE() OR SendAt24h < DATEADD(hour, -24, GETDATE()))",
                    New With {.Msg = msg})
                Dim negStale = -CInt(Math.Truncate(GetShortReminderHours()))
                If negStale > -1 Then negStale = -1
                cn.Execute(
                    "UPDATE dbo.ApptTwoHourWhatsAppQueue SET Status2h = N'Missed', Processed2hAtUtc = SYSUTCDATETIME(), Error2h = @Msg " &
                    "WHERE Status2h = N'Pending' AND (ApptStartSnapshot <= GETDATE() OR SendAt2h < DATEADD(hour, @NegStaleHours, GETDATE()))",
                    New With {.Msg = msg, .NegStaleHours = negStale})
            End Using
        Catch ex As Exception
            Trace.TraceError("ApptTwoHourWhatsAppQueue FinalizeOverduePendingLegs: " & ex.ToString())
        End Try
    End Sub

    Private NotInheritable Class PatientWhatsRawRow
        Public Property WhatsApp As String
        Public Property Phone As String
        Public Property WhatsAppPrefix As String
    End Class

    Public Shared Function TryFinalize24h(queueId As Long, status As String, errorMessage As String, whatsAppLogId As Long?) As Boolean
        Dim err = Trunc(If(errorMessage, ""), 2000)
        If String.IsNullOrWhiteSpace(err) Then err = Nothing
        Try
            Const upd As String = "UPDATE dbo.ApptTwoHourWhatsAppQueue SET Status24h = @Status, Processed24hAtUtc = SYSUTCDATETIME(), Error24h = @ErrorMessage, WhatsAppLogId24h = @WhatsAppLogId " &
                "WHERE QueueId = @QueueId AND Status24h = N'Pending'"
            Using cn As New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
                Dim n = cn.Execute(upd, New With {.QueueId = queueId, .Status = status, .ErrorMessage = err, .WhatsAppLogId = whatsAppLogId})
                Return n > 0
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Function TryFinalize2h(queueId As Long, status As String, errorMessage As String, whatsAppLogId As Long?) As Boolean
        Dim err = Trunc(If(errorMessage, ""), 2000)
        If String.IsNullOrWhiteSpace(err) Then err = Nothing
        Try
            Const upd As String = "UPDATE dbo.ApptTwoHourWhatsAppQueue SET Status2h = @Status, Processed2hAtUtc = SYSUTCDATETIME(), Error2h = @ErrorMessage, WhatsAppLogId2h = @WhatsAppLogId " &
                "WHERE QueueId = @QueueId AND Status2h = N'Pending'"
            Using cn As New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
                Dim n = cn.Execute(upd, New With {.QueueId = queueId, .Status = status, .ErrorMessage = err, .WhatsAppLogId = whatsAppLogId})
                Return n > 0
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Function GetRecentRows(take As Integer) As List(Of ApptTwoHourReminderQueueRow)
        If take < 1 Then take = 100
        If take > 5000 Then take = 5000
        Try
            Using cn As New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
                Dim sql = "SELECT TOP (@take) " & SelectCols & " FROM dbo.ApptTwoHourWhatsAppQueue ORDER BY CreatedAtUtc DESC"
                Return cn.Query(Of ApptTwoHourReminderQueueRow)(sql, New With {.take = take}).ToList()
            End Using
        Catch
            Return New List(Of ApptTwoHourReminderQueueRow)
        End Try
    End Function

    Private Shared Function Trunc(s As String, maxLen As Integer) As String
        If String.IsNullOrEmpty(s) Then Return ""
        If s.Length <= maxLen Then Return s
        Return s.Substring(0, maxLen)
    End Function

#Region "Deprecated — kept for any stray callers; no longer used by processor"
    <Obsolete("Use SyncFromAppointmentId / GetRowsWithDueReminders.")>
    Public Shared Function TryEnqueue(appt As AppointmentReminderDto, messagePreview As String, clinicId As String, reminderKind As String) As Boolean
        If appt Is Nothing Then Return False
        SyncFromAppointmentId(appt.AppointmentID)
        Return True
    End Function

    <Obsolete("Use GetRowsWithDueReminders.")>
    Public Shared Function GetPendingRows() As List(Of ApptTwoHourReminderQueueRow)
        Return GetRowsWithDueReminders()
    End Function

    <Obsolete("Use TryFinalize24h/TryFinalize2h.")>
    Public Shared Function TryFinalize(queueId As Long, status As String, errorMessage As String, whatsAppLogId As Long?) As Boolean
        Return TryFinalize2h(queueId, status, errorMessage, whatsAppLogId)
    End Function
#End Region
End Class
