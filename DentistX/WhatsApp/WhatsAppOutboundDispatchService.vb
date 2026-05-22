Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports Dapper
Imports System.Diagnostics
Imports System.Globalization
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks

''' <summary>Background dispatcher for dbo.WhatsAppOutboundMessage (reminders, scheduler snapshots, future casual queue).</summary>
Public NotInheritable Class WhatsAppOutboundDispatchService
    Private Sub New()
    End Sub

    Private Shared ReadOnly Gate As New Object()
    Private Shared _timer As Timer
    Private Shared _tickBusy As Integer

    Public Shared Sub EnsureStarted()
        SyncLock Gate
            If _timer Is Nothing Then
                _timer = New Timer(AddressOf TimerCallback, Nothing, 4000, 20000)
            End If
        End SyncLock
    End Sub

    Private Shared Sub TimerCallback(state As Object)
        Task.Run(Async Function()
                     If Interlocked.CompareExchange(_tickBusy, 1, 0) <> 0 Then Return
                     Try
                         Await FlushOutstandingAsync(Nothing, DateTime.UtcNow.AddSeconds(10), Nothing).ConfigureAwait(False)
                     Catch ex As Exception
                         Trace.TraceError("WhatsAppOutboundDispatchService tick: " & ex.Message)
                     Finally
                         Interlocked.Exchange(_tickBusy, 0)
                     End Try
                 End Function)
    End Sub

    Public Shared Sub RequestImmediateDispatch()
        EnsureStarted()
        Task.Run(Async Function()
                     If Interlocked.CompareExchange(_tickBusy, 1, 0) <> 0 Then Return
                     Try
                         Await FlushOutstandingAsync(Nothing, DateTime.UtcNow.AddSeconds(35), Nothing).ConfigureAwait(False)
                     Finally
                         Interlocked.Exchange(_tickBusy, 0)
                     End Try
                 End Function)
    End Sub

    ''' <summary>Drains due rows until empty, deadline, or max rounds.</summary>
    Public Shared Async Function FlushOutstandingAsync(Optional reminderAggregate As ReminderRunResult = Nothing,
                                                       Optional utcDeadline As DateTime? = Nothing,
                                                       Optional schedulerGatewayJobCollect As ICollection(Of String) = Nothing) As Task
        WhatsAppOutboundRepository.EnsureSchemaAndRecoverStuck()
        If Not WhatsAppOutboundRepository.IsOutboundInfrastructureReady() Then Return

        Dim deadline = If(utcDeadline, DateTime.UtcNow.AddSeconds(18))
        Dim clinicIdRaw = WhatsAppService.GetCurrentClinicId()
        If String.IsNullOrWhiteSpace(clinicIdRaw) Then Return

        Dim connected As Boolean = False
        Try
            connected = Await WhatsAppService.TrySilentWhatsReconnectBackgroundAsync(clinicIdRaw).ConfigureAwait(False)
        Catch
        End Try
        If Not connected Then Return

        Dim wa As New WhatsAppService()
        Dim rounds = 0
        While rounds < 60 AndAlso DateTime.UtcNow < deadline
            Dim batch = WhatsAppOutboundRepository.DequeueDueBatch(16)
            If batch Is Nothing OrElse batch.Count = 0 Then Return

            For Each msg In batch
                If msg Is Nothing Then Continue For

                Dim clinicUse = If(String.IsNullOrWhiteSpace(msg.ClinicId), clinicIdRaw, msg.ClinicId).Trim()

                If Not WhatsAppOutboundRepository.TryClaimSending(msg.MessageId) Then Continue For

                Dim path = If(String.IsNullOrWhiteSpace(msg.AttachmentPath), "", msg.AttachmentPath.Trim())
                If Not String.IsNullOrWhiteSpace(path) AndAlso Not IO.File.Exists(path) Then
                    Dim deadMiss = WhatsAppOutboundRepository.ApplySendFailure(msg.MessageId, "Attachment missing: " & path)
                    If deadMiss Then FinalizeOutboundDeadReminderAndMaybeScheduler(msg)
                    Continue For
                End If

                Try
                    Dim ctx = BuildContext(msg)
                    Dim resp = Await wa.SendMessageAsync(clinicUse, msg.TargetDigits, msg.BodyPlain, path, ctx).ConfigureAwait(False)
                    Dim gid = ExtractGatewayJobId(resp)
                    WhatsAppOutboundRepository.MarkSendSucceeded(msg.MessageId, gid)
                    If schedulerGatewayJobCollect IsNot Nothing AndAlso msg.MessageCategory = WhatsAppMessageCategories.SchedulerSnapshotAuto AndAlso
                        Not String.IsNullOrWhiteSpace(gid) Then
                        schedulerGatewayJobCollect.Add(gid.Trim())
                    End If
                    HandleOutboundSuccess(msg, reminderAggregate)
                Catch ex As Exception
                    Dim isDeadNow = WhatsAppOutboundRepository.ApplySendFailure(msg.MessageId, ex.Message)
                    If isDeadNow Then FinalizeOutboundDeadReminderAndMaybeScheduler(msg)
                End Try
            Next

            rounds += 1
        End While
    End Function

    ''' <summary>Drains pending snapshot outbox rows for the given <see cref="WhatsAppOutboundMessageRow.WaGroupKey"/> set (avoids waiting behind lower-priority reminder backlog during Test send).</summary>
    Public Shared Async Function FlushWaGroupKeysAsync(waGroupKeys As IEnumerable(Of String),
                                                      utcDeadline As DateTime,
                                                      Optional schedulerGatewayJobCollect As ICollection(Of String) = Nothing) As Task
        WhatsAppOutboundRepository.EnsureSchemaAndRecoverStuck()
        If Not WhatsAppOutboundRepository.IsOutboundInfrastructureReady() Then Return
        If waGroupKeys Is Nothing Then Return
        Dim keyList = waGroupKeys.
            Where(Function(s) Not String.IsNullOrWhiteSpace(s)).
            Select(Function(s) s.Trim()).
            Distinct(StringComparer.OrdinalIgnoreCase).
            ToList()
        If keyList.Count = 0 Then Return

        Dim clinicIdRaw = WhatsAppService.GetCurrentClinicId()
        If String.IsNullOrWhiteSpace(clinicIdRaw) Then Return

        Dim connected As Boolean = False
        Try
            connected = Await WhatsAppService.TrySilentWhatsReconnectBackgroundAsync(clinicIdRaw).ConfigureAwait(False)
        Catch
        End Try
        If Not connected Then Return

        Dim wa As New WhatsAppService()
        Dim rounds = 0
        While rounds < 200 AndAlso DateTime.UtcNow < utcDeadline
            Dim batch = WhatsAppOutboundRepository.DequeuePendingBatchForWaGroupKeys(keyList, 32)
            If batch Is Nothing OrElse batch.Count = 0 Then Return

            For Each msg In batch
                If msg Is Nothing Then Continue For

                Dim clinicUse = If(String.IsNullOrWhiteSpace(msg.ClinicId), clinicIdRaw, msg.ClinicId).Trim()

                If Not WhatsAppOutboundRepository.TryClaimSending(msg.MessageId) Then Continue For

                Dim path = If(String.IsNullOrWhiteSpace(msg.AttachmentPath), "", msg.AttachmentPath.Trim())
                If Not String.IsNullOrWhiteSpace(path) AndAlso Not IO.File.Exists(path) Then
                    Dim deadMiss = WhatsAppOutboundRepository.ApplySendFailure(msg.MessageId, "Attachment missing: " & path)
                    If deadMiss Then FinalizeOutboundDeadReminderAndMaybeScheduler(msg)
                    Continue For
                End If

                Try
                    Dim ctx = BuildContext(msg)
                    Dim resp = Await wa.SendMessageAsync(clinicUse, msg.TargetDigits, msg.BodyPlain, path, ctx).ConfigureAwait(False)
                    Dim gid = ExtractGatewayJobId(resp)
                    WhatsAppOutboundRepository.MarkSendSucceeded(msg.MessageId, gid)
                    If schedulerGatewayJobCollect IsNot Nothing AndAlso msg.MessageCategory = WhatsAppMessageCategories.SchedulerSnapshotAuto AndAlso
                        Not String.IsNullOrWhiteSpace(gid) Then
                        schedulerGatewayJobCollect.Add(gid.Trim())
                    End If
                    HandleOutboundSuccess(msg, Nothing)
                Catch ex As Exception
                    Dim isDeadNow = WhatsAppOutboundRepository.ApplySendFailure(msg.MessageId, ex.Message)
                    If isDeadNow Then FinalizeOutboundDeadReminderAndMaybeScheduler(msg)
                End Try
            Next

            rounds += 1
        End While
    End Function

    Private Shared Function BuildContext(msg As WhatsAppOutboundMessageRow) As WhatsAppSendContext
        Return New WhatsAppSendContext With {
            .Category = msg.MessageCategory,
            .PatientId = msg.PatientId,
            .SourceHint = msg.SourceHint,
            .DisplayName = If(String.IsNullOrWhiteSpace(msg.CallerDisplayName), Nothing, msg.CallerDisplayName.Trim()),
            .SentAfterLocalQueue = msg.MessageCategory = WhatsAppMessageCategories.AppointmentReminder OrElse
                                   msg.MessageCategory = WhatsAppMessageCategories.AppointmentTwoHourReminder,
            .SuppressUiFeedback = msg.SuppressUiFeedback,
            .RevealMessageCenter = msg.RevealMessageCenter,
            .BypassOutboundQueue = True
        }
    End Function

    Private Shared Function ExtractGatewayJobId(responseJson As String) As String
        If String.IsNullOrWhiteSpace(responseJson) Then Return Nothing
        Dim row As New WhatsAppActivityLogRow With {.ResponseOrError = responseJson, .SentAtUtc = DateTime.UtcNow}
        WhatsAppActivityLogRow.TryApplyQueueMetadataFromResponse(row)
        Dim j = If(row.QueueJobId, "").Trim()
        Return If(j.Length > 0, j, Nothing)
    End Function

    Private Shared Sub HandleOutboundSuccess(msg As WhatsAppOutboundMessageRow, agg As ReminderRunResult)
        If msg Is Nothing Then Return

        If String.Equals(msg.MessageCategory, WhatsAppMessageCategories.CrashReport, StringComparison.OrdinalIgnoreCase) Then
            Dim ap = If(msg.AttachmentPath, "").Trim()
            If ap.Length > 0 Then My.MyApplication.TryPersistCrashSignatureAfterOutboundSend(ap)
        End If

        If msg.MessageCategory = WhatsAppMessageCategories.AppointmentReminder OrElse
            msg.MessageCategory = WhatsAppMessageCategories.AppointmentTwoHourReminder Then
            FinishOutboundReminderSucceeded(msg, agg)
            Return
        End If
        If msg.MessageCategory = WhatsAppMessageCategories.SchedulerSnapshotAuto Then
            MaybeFinalizeSchedulerSnapshotGroup(msg.WaGroupKey)
        End If
    End Sub

    Private Shared Sub FinalizeOutboundDeadReminderAndMaybeScheduler(msg As WhatsAppOutboundMessageRow)
        If msg.MessageCategory = WhatsAppMessageCategories.AppointmentReminder OrElse
            msg.MessageCategory = WhatsAppMessageCategories.AppointmentTwoHourReminder Then
            FinishOutboundReminderDead(msg)
        End If
        If msg.MessageCategory = WhatsAppMessageCategories.SchedulerSnapshotAuto Then
            MaybeFinalizeSchedulerSnapshotGroup(msg.WaGroupKey)
        End If
    End Sub

    Private Shared Sub FinishOutboundReminderSucceeded(msg As WhatsAppOutboundMessageRow, agg As ReminderRunResult)
        Dim is24 = String.Equals(msg.MessageCategory, WhatsAppMessageCategories.AppointmentReminder, StringComparison.OrdinalIgnoreCase)

        If Not msg.ReminderQueueId.HasValue Then
            If is24 AndAlso msg.AppointmentId.GetValueOrDefault() > 0 Then
                AppointmentReminderService.MarkReminderSent(msg.AppointmentId.Value)
            End If
            Return
        End If

        Dim minUtc = DateTime.UtcNow.AddSeconds(-30)
        Dim logId = WhatsAppActivityLogRepository.TryGetLatestLogId(msg.PatientId, msg.MessageCategory, minUtc)
        Dim logNullable As Long? = If(logId > 0, logId, Nothing)

        Dim finalized As Boolean = False
        If is24 Then
            finalized = ApptTwoHourReminderQueueRepository.TryFinalize24h(msg.ReminderQueueId.Value, ApptTwoHourReminderQueueRepository.StatusSent, Nothing, logNullable)
        Else
            finalized = ApptTwoHourReminderQueueRepository.TryFinalize2h(msg.ReminderQueueId.Value, ApptTwoHourReminderQueueRepository.StatusSent, Nothing, logNullable)
        End If

        If finalized Then
            If is24 AndAlso msg.AppointmentId.HasValue Then
                AppointmentReminderService.MarkReminderSent(msg.AppointmentId.Value)
            End If
            If agg IsNot Nothing Then
                agg.SentCount += 1
                agg.Lines.Add(BuildReminderAggLine(msg, is24))
            End If
        End If
    End Sub

    Private Shared Function BuildReminderAggLine(msg As WhatsAppOutboundMessageRow, is24h As Boolean) As String
        Dim patientName = InferPatientName(msg)
        Dim snapUtc = msg.AppointmentStartUtc.GetValueOrDefault(DateTime.UtcNow)
        If snapUtc.Kind <> DateTimeKind.Utc Then snapUtc = DateTime.SpecifyKind(snapUtc, DateTimeKind.Utc)
        Dim snapshotLocal = snapUtc.ToLocalTime()

        Dim tag As String = If(is24h, "24h", ApptTwoHourReminderQueueRepository.GetShortReminderHours().ToString("0.#", CultureInfo.InvariantCulture) & "h")
        Dim datePartLocal = WhatsHelper.FormatWhatsAppDateLong(snapshotLocal.Date, True) & " " &
            snapshotLocal.ToString("HH:mm", CultureInfo.InvariantCulture)

        Return $"{tag} reminder · {patientName} · {datePartLocal}"
    End Function

    Private Shared Function InferPatientName(msg As WhatsAppOutboundMessageRow) As String
        If msg.AppointmentId.GetValueOrDefault() <= 0 Then Return "(no name)"
        Try
            Dim repo As New AppointmentCRepository()
            Dim dto = repo.GetAppointmentReminderById(msg.AppointmentId.Value)
            If dto Is Nothing Then Return "(no name)"
            Dim pn = If(dto.PatientName, "").Trim()
            Return If(String.IsNullOrWhiteSpace(pn), "(no name)", pn)
        Catch
            Return "(no name)"
        End Try
    End Function

    Private Shared Sub FinishOutboundReminderDead(msg As WhatsAppOutboundMessageRow)
        If Not msg.ReminderQueueId.HasValue Then Return
        Dim detail As String = Nothing
        Try
            Using cn As New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
                detail = cn.ExecuteScalar(Of String)(
                    "SELECT LastError FROM dbo.WhatsAppOutboundMessage WHERE MessageId = @Id",
                    New With {.Id = msg.MessageId})
            End Using
        Catch
        End Try

        Dim errOut = WhatsAppOutboundRepository.Trunc(If(detail, "").Trim(), 2000)
        If String.IsNullOrWhiteSpace(errOut) Then errOut = WhatsAppOutboundRepository.Trunc(If(msg.LastError, "WhatsApp outbound dead/expired."), 2000)

        Dim is24 = String.Equals(msg.MessageCategory, WhatsAppMessageCategories.AppointmentReminder, StringComparison.OrdinalIgnoreCase)
        If is24 Then
            ApptTwoHourReminderQueueRepository.TryFinalize24h(msg.ReminderQueueId.Value, ApptTwoHourReminderQueueRepository.StatusFailed, errOut, Nothing)
        Else
            ApptTwoHourReminderQueueRepository.TryFinalize2h(msg.ReminderQueueId.Value, ApptTwoHourReminderQueueRepository.StatusFailed, errOut, Nothing)
        End If
    End Sub

    Private Shared Sub MaybeFinalizeSchedulerSnapshotGroup(waGroupKey As String)
        If String.IsNullOrWhiteSpace(waGroupKey) Then Return

        Dim counts = WhatsAppOutboundRepository.GetWaGroupStatusCounts(waGroupKey)
        Dim pend = SumCount(counts, WhatsAppOutboundStatuses.Pending) + SumCount(counts, WhatsAppOutboundStatuses.Sending)
        If pend > 0 Then Return

        Dim totalRows = WhatsAppOutboundRepository.CountRowsForWaGroup(waGroupKey)
        If totalRows <= 0 Then Return

        Dim sent = SumCount(counts, WhatsAppOutboundStatuses.Sent)
        Dim dead = SumCount(counts, WhatsAppOutboundStatuses.Dead)
        If sent + dead < totalRows Then Return

        Dim metaRaw As String = Nothing
        Try
            Using cn As New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
                metaRaw = cn.ExecuteScalar(Of String)(
                    "SELECT TOP 1 WaSchedulerMeta FROM dbo.WhatsAppOutboundMessage WHERE WaGroupKey = @K ORDER BY MessageId",
                    New With {.K = waGroupKey.Trim()})
            End Using
        Catch
        End Try

        Dim meta = ParseSchedulerMeta(metaRaw)
        Dim already = WhatsAppOutboundRepository.CountSchedulerLogsForRecipientRun(meta.JobId, meta.RecipientId, meta.RunDate, meta.SendSlot)
        If already > 0 Then Return

        Dim completedUtc = DateTime.UtcNow
        If dead > 0 Then
            Dim errDead = WhatsAppOutboundRepository.TryGetFirstSchedulerFailureInGroup(waGroupKey)
            Dim detailLog = WhatsAppOutboundRepository.Trunc(
                If(Not String.IsNullOrWhiteSpace(errDead), errDead, If(Eng, "One or more snapshot WhatsApp deliveries failed.", "فشل أحد مقاطع إرسال لقطة الجدول.")),
                1900)
            SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                meta.JobId, meta.RecipientId, meta.RunDate,
                SchedulerSnapshotAutoSendRepository.LogStatusFailed,
                meta.StartedUtc, completedUtc, detailLog, meta.PathBundle,
                excludeFromDedupe:=meta.ExcludeFromDedupe, sendSlot:=meta.SendSlot)
            Return
        End If

        If sent = totalRows Then
            SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                meta.JobId, meta.RecipientId, meta.RunDate,
                SchedulerSnapshotAutoSendRepository.LogStatusSent,
                meta.StartedUtc, completedUtc, Nothing, meta.PathBundle,
                excludeFromDedupe:=meta.ExcludeFromDedupe, sendSlot:=meta.SendSlot)
        End If
    End Sub

    Private Structure SchedulerMetaParsed
        Public JobId As Integer
        Public RecipientId As Long
        Public RunDate As Date
        Public SendSlot As Byte
        Public PathBundle As String
        Public ExcludeFromDedupe As Boolean
        Public StartedUtc As DateTime
    End Structure

    Private Shared Function ParseSchedulerMeta(raw As String) As SchedulerMetaParsed
        Dim m As New SchedulerMetaParsed With {
            .JobId = 0,
            .RecipientId = 0,
            .RunDate = Date.Today,
            .SendSlot = 1,
            .PathBundle = "",
            .ExcludeFromDedupe = False,
            .StartedUtc = DateTime.UtcNow
        }

        If String.IsNullOrWhiteSpace(raw) Then Return m

        Dim parts = raw.Replace(vbCrLf, vbLf).Split(New Char() {ChrW(10)})
        Dim lines = parts.Select(Function(s) If(s, "").Trim()).Where(Function(s) s.Length > 0).ToList()

        Try
            If lines.Count >= 1 Then Integer.TryParse(lines(0), m.JobId)
            If lines.Count >= 2 Then Long.TryParse(lines(1), m.RecipientId)

            Dim dt As DateTime
            If lines.Count >= 3 AndAlso Date.TryParseExact(lines(2), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, dt) Then
                m.RunDate = dt.Date
            End If

            If lines.Count >= 4 Then Byte.TryParse(lines(3), m.SendSlot)
            If lines.Count >= 5 Then m.PathBundle = WhatsAppOutboundRepository.Trunc(lines(4), 2000)

            Dim excludeStr As String = ""
            If lines.Count >= 7 Then excludeStr = lines(6).Trim()
            If excludeStr.Length = 0 AndAlso lines.Count >= 6 Then excludeStr = lines(5).Trim()
            Dim bExclude As Boolean = False
            If Boolean.TryParse(excludeStr, bExclude) Then
                m.ExcludeFromDedupe = bExclude
            ElseIf excludeStr = "1" Then
                m.ExcludeFromDedupe = True
            End If

            Dim tickStr As String = ""
            If lines.Count >= 8 Then tickStr = lines(7).Trim()
            Dim ticks As Long = 0
            If Long.TryParse(tickStr, NumberStyles.Integer, CultureInfo.InvariantCulture, ticks) Then
                m.StartedUtc = New DateTime(ticks, DateTimeKind.Utc)
            End If
        Catch
        End Try

        Return m
    End Function

    Private Shared Function SumCount(d As Dictionary(Of String, Integer), status As String) As Integer
        If d Is Nothing OrElse String.IsNullOrWhiteSpace(status) Then Return 0
        Dim v As Integer
        Return If(d.TryGetValue(status.Trim(), v), v, 0)
    End Function
End Class
