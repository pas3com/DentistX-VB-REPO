Imports System
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Microsoft.Win32

''' <summary>
''' Scheduled send: up to two 7-day week PNGs and/or two HTML files per recipient via WhatsApp.
''' Normal days send current + next Sat-Fri weeks; Thursday shifts forward to next + week-after-next.
''' Capture uses <see cref="SchedulerWeekSnapshotBackgroundCapture"/> on the UI thread (no need to open the scheduler).
''' WhatsApp I/O runs with ConfigureAwait(False) so the UI thread is not blocked or deadlocked.
''' </summary>
Public NotInheritable Class SchedulerSnapshotAutoSendService
    Private Sub New()
    End Sub

    Private Shared ReadOnly SyncRoot As New Object()
    Private Shared _tickBusy As Integer
    Private Shared _timer As Windows.Forms.Timer
    Private Shared _uiSuppressionCount As Integer
    Private Shared _powerResumeHooked As Boolean

    Public Shared Sub EnsureStarted()
        SyncLock SyncRoot
            If _timer Is Nothing Then
                _timer = New Windows.Forms.Timer With {.Interval = 60000}
                AddHandler _timer.Tick, AddressOf OnTimerTick
                _timer.Start()
                SchedulerSnapshotSnapLog.Write("SERVICE", "SchedulerSnapshotAutoSend timer started intervalMs=60000")
            End If
            EnsurePowerResumeHook()
        End SyncLock
        RequestImmediateRun()
    End Sub

    Private Shared Sub EnsurePowerResumeHook()
        If _powerResumeHooked Then Return
        _powerResumeHooked = True
        AddHandler SystemEvents.PowerModeChanged, AddressOf OnPowerModeChanged
    End Sub

    Private Shared Sub OnPowerModeChanged(sender As Object, e As PowerModeChangedEventArgs)
        If e.Mode <> PowerModes.Resume Then Return
        SchedulerSnapshotSnapLog.Write("SERVICE", "Power resume — immediate snapshot send poll")
        RequestImmediateRun()
    End Sub

    Public Shared Sub PushUiSuppression()
        Threading.Interlocked.Increment(_uiSuppressionCount)
    End Sub

    Public Shared Sub PopUiSuppression(Optional shouldRequestImmediateRun As Boolean = True)
        Dim newValue = Threading.Interlocked.Decrement(_uiSuppressionCount)
        If newValue < 0 Then
            Threading.Interlocked.Exchange(_uiSuppressionCount, 0)
            newValue = 0
        End If
        If shouldRequestImmediateRun AndAlso newValue = 0 Then
            RequestImmediateRun()
        End If
    End Sub

    Public Shared Sub RequestImmediateRun()
        QueueDueJobsRun()
    End Sub

    Private Shared Sub OnTimerTick(sender As Object, e As EventArgs)
        QueueDueJobsRun()
    End Sub

    Private Shared Sub QueueDueJobsRun()
        Task.Run(Async Function()
                     If System.Threading.Interlocked.CompareExchange(_tickBusy, 1, 0) <> 0 Then Return
                     Try
                         Await RunDueJobsAsync().ConfigureAwait(False)
                     Catch ex As Exception
                         SchedulerSnapshotSnapLog.Write("COORD_LOOP_EXCEPTION", ex.GetType().Name & ": " & ex.Message)
                     Finally
                         System.Threading.Interlocked.Exchange(_tickBusy, 0)
                     End Try
                 End Function)
    End Sub

    Private Shared Async Function RunDueJobsAsync() As Task
        Dim mv = TryCast(Application.OpenForms("MainView3"), MainView3)
        If mv Is Nothing OrElse mv.IsDisposed OrElse Not mv.IsHandleCreated Then
            SchedulerSnapshotSnapLog.Write("COORD_SKIP", "MainView3 missing or not ready")
            Return
        End If

        Dim sup = Threading.Volatile.Read(_uiSuppressionCount)
        Dim deferModal = mv.ShouldDeferSchedulerBackgroundWork()
        If sup > 0 OrElse deferModal Then
            SchedulerSnapshotSnapLog.Write("COORD_SKIP", "uiSuppression=" & sup.ToString(CultureInfo.InvariantCulture) & " modalSchedulerDefer=" & deferModal.ToString())
            Return
        End If

        Dim nowLocal = DateTime.Now
        Dim today = nowLocal.Date

        Dim jobs As System.Collections.Generic.IList(Of SchedulerSnapshotAutoSendJobRow) = Nothing
        Try
            jobs = SchedulerSnapshotAutoSendRepository.GetJobs()
        Catch ex As Exception
            SchedulerSnapshotSnapLog.Write("COORD_DB", "GetJobs failed: " & ex.Message)
            Return
        End Try

        If jobs Is Nothing OrElse jobs.Count = 0 Then Return

        Dim duePairs As New List(Of String)()
        Dim enabledCount = 0
        For Each job In jobs
            If Not job.IsEnabled Then Continue For
            enabledCount += 1
            For Each slot In GetDueSendSlotsForJob(job, nowLocal)
                duePairs.Add(job.JobId.ToString(CultureInfo.InvariantCulture) & "@" & slot.ToString(CultureInfo.InvariantCulture))
            Next
        Next

        SchedulerSnapshotSnapLog.Write("COORD_POLL",
            String.Format(CultureInfo.InvariantCulture, "localNow={0:O} enabledJobs={1} due=[{2}]",
                nowLocal, enabledCount, String.Join(",", duePairs)))

        For Each job In jobs
            If Not job.IsEnabled Then Continue For
            For Each sendSlot In GetDueSendSlotsForJob(job, nowLocal)
                Dim runForDay As Date = today
                SchedulerSnapshotSnapLog.Write("COORD_INVOKE",
                    String.Format(CultureInfo.InvariantCulture, "BeginInvoke RunJobAsync jobId={0} sendSlot={1} runDate={2:yyyy-MM-dd}", job.JobId, sendSlot, runForDay))
                Dim tcs As New Threading.Tasks.TaskCompletionSource(Of Boolean)
                Dim jb = job
                Dim sb = sendSlot
                mv.BeginInvoke(Async Sub()
                                   Try
                                       Await RunJobAsync(jb, runForDay, forceRun:=False, sendSlot:=sb).ConfigureAwait(False)
                                   Catch ex As Exception
                                       SchedulerSnapshotSnapLog.Write("RUNJOB_EXCEPTION",
                                           String.Format(CultureInfo.InvariantCulture, "jobId={0} slot={1} {2}: {3}", jb.JobId, sb, ex.GetType().Name, ex.Message))
                                   Finally
                                       tcs.SetResult(True)
                                   End Try
                               End Sub)
                Await tcs.Task.ConfigureAwait(False)
            Next
        Next
    End Function

    Private Shared Function LocalClockMinuteEquals(a As TimeSpan, b As TimeSpan) As Boolean
        Return a.Hours = b.Hours AndAlso a.Minutes = b.Minutes
    End Function

    ''' <summary>Returns 1 and/or 2 for send slots currently inside the grace window (same weekday mask as the job).</summary>
    Private Shared Function GetDueSendSlotsForJob(job As SchedulerSnapshotAutoSendJobRow, nowLocal As DateTime) As List(Of Byte)
        Dim slots As New List(Of Byte)()
        If IsSendSlotDueWithinGraceWindow(job.DaysOfWeekMask, job.SendTimeLocal, nowLocal) Then slots.Add(1)
        Dim t2 = job.SendTimeLocal2
        If t2.HasValue Then
            If Not LocalClockMinuteEquals(job.SendTimeLocal, t2.Value) AndAlso IsSendSlotDueWithinGraceWindow(job.DaysOfWeekMask, t2.Value, nowLocal) Then
                slots.Add(2)
            End If
        End If
        Return slots
    End Function

    ''' <summary>True after the slot's local time on the same calendar day until midnight; dedupe stops repeats once Sent.</summary>
    Private Shared Function IsSendSlotDueWithinGraceWindow(daysMask As Byte, sendTime As TimeSpan, nowLocal As DateTime) As Boolean
        Dim bit = SchedulerSnapshotAutoSendRepository.DayOfWeekToBit(nowLocal.DayOfWeek)
        If (daysMask And bit) = 0 Then Return False
        Dim dueLocal = nowLocal.Date.Add(sendTime)
        Return nowLocal >= dueLocal AndAlso nowLocal.Date = dueLocal.Date
    End Function

    Private Shared Function GetSendContentMode(job As SchedulerSnapshotAutoSendJobRow) As SchedulerSnapshotAutoSendRepository.SendContentMode
        Dim b = job.SendContentMode
        If b = CByte(SchedulerSnapshotAutoSendRepository.SendContentMode.PngOnly) OrElse
            b = CByte(SchedulerSnapshotAutoSendRepository.SendContentMode.HtmlOnly) OrElse
            b = CByte(SchedulerSnapshotAutoSendRepository.SendContentMode.PngAndHtml) Then
            Return CType(b, SchedulerSnapshotAutoSendRepository.SendContentMode)
        End If
        Return SchedulerSnapshotAutoSendRepository.SendContentMode.PngOnly
    End Function

    ''' <summary>For Test send: pick slot 1 or 2 using whichever configured send time is closer to local clock now (same PNG/HTML capture for both).</summary>
    Friend Shared Function ResolveTestSendSlotForJob(job As SchedulerSnapshotAutoSendJobRow) As Byte
        If job Is Nothing OrElse Not job.SendTimeLocal2.HasValue Then Return CByte(1)
        Dim t2 = job.SendTimeLocal2.Value
        If LocalClockMinuteEquals(job.SendTimeLocal, t2) Then Return CByte(1)
        Dim nowClock = DateTime.Now.TimeOfDay
        Dim d1 = CircMinutesDelta(nowClock, job.SendTimeLocal)
        Dim d2 = CircMinutesDelta(nowClock, t2)
        Return If(d2 < d1, CByte(2), CByte(1))
    End Function

    ''' <returns>Shortest distance between two times-of-day on a 24h circle (minutes, max 720).</returns>
    Private Shared Function CircMinutesDelta(a As TimeSpan, b As TimeSpan) As Double
        Dim m = Math.Abs((a - b).TotalMinutes)
        If m > 720R Then m = 1440R - m
        Return CDbl(m)
    End Function

    ''' <summary>
    ''' Call from the UI thread: captures snapshots on the UI thread, then sends WhatsApp without blocking the UI (no sync .GetResult()).
    ''' Week capture uses <see cref="SchedulerWeekSnapshotBackgroundCapture.TrySaveCurrentAndNextWeekMedia"/> with <c>useScheduledWeekRule:=True</c> (Thursday uses next + following Sat–Fri weeks, same as <see cref="SchedulerNew"/>).
    ''' </summary>
    Public Shared Async Function RunJobAsync(job As SchedulerSnapshotAutoSendJobRow, runDate As Date, forceRun As Boolean,
                                             Optional sendSlot As Byte = 1) As Task(Of Boolean)
        Dim recipients = SchedulerSnapshotAutoSendRepository.GetRecipients(job.JobId).
            Where(Function(r) r.IsActive).ToList()
        If recipients.Count = 0 Then
            SchedulerSnapshotSnapLog.Write("RUNJOB_SKIP",
                String.Format(CultureInfo.InvariantCulture, "jobId={0} sendSlot={1} runDate={2:yyyy-MM-dd} forceRun={3} reason=no_active_recipients", job.JobId, sendSlot, runDate, forceRun))
            Return False
        End If

        Dim pending = If(forceRun,
            recipients,
            recipients.Where(Function(r) Not SchedulerSnapshotAutoSendRepository.HasRecipientSentOnRunDate(job.JobId, r.RecipientId, runDate, sendSlot)).ToList())
        If pending.Count = 0 Then
            Dim blockSent = SchedulerSnapshotAutoSendRepository.CountDedupeBlockingSentRows(job.JobId, runDate)
            Dim testSent = SchedulerSnapshotAutoSendRepository.CountSentExcludedFromDedupe(job.JobId, runDate)
            SchedulerSnapshotSnapLog.Write("RUNJOB_SKIP",
                String.Format(CultureInfo.InvariantCulture,
                    "jobId={0} sendSlot={1} runDate={2:yyyy-MM-dd} forceRun={3} reason=all_already_sent_this_slot activeRecipients={4} dedupeBlockingSentRows={5} excludedTestSentRows={6} note=blocking rows have ExcludeFromDedupe=0 (see SchedulerSnapshotAutoSend_ResetDayDedupe_TestOnly.sql)",
                    job.JobId, sendSlot, runDate, forceRun, recipients.Count, blockSent, testSent))
            Return False
        End If

        Dim mode = GetSendContentMode(job)
        Dim capCur As String = Nothing
        Dim capNxt As String = Nothing
        Dim pathPngCur As String = Nothing
        Dim pathPngNxt As String = Nothing
        Dim pathHtmlCur As String = Nothing
        Dim pathHtmlNxt As String = Nothing

        ' Same week-selection rule as interactive SchedulerNew (Sat–Fri): on anchor Thursday, capture next + following week (offset +1/+2).
        Dim ok = SchedulerWeekSnapshotBackgroundCapture.TrySaveCurrentAndNextWeekMedia(
            runDate, mode, capCur, capNxt, pathPngCur, pathPngNxt, pathHtmlCur, pathHtmlNxt,
            useScheduledWeekRule:=True)

        Dim pathBundle = BuildLogMediaBundle(mode, pathPngCur, pathPngNxt, pathHtmlCur, pathHtmlNxt)

        If Not ok OrElse Not AreCapturedPathsUsable(mode, pathPngCur, pathPngNxt, pathHtmlCur, pathHtmlNxt) Then
            SchedulerSnapshotSnapLog.Write("RUNJOB_CAPTURE",
                String.Format(CultureInfo.InvariantCulture,
                    "jobId={0} sendSlot={1} mode={2} ok=False usable={3} bundlePreview={4}",
                    job.JobId, sendSlot, mode, AreCapturedPathsUsable(mode, pathPngCur, pathPngNxt, pathHtmlCur, pathHtmlNxt),
                    ShortenForLog(pathBundle, 240)))
            Dim started = DateTime.UtcNow
            Dim err = CaptureFailureMessage(mode)
            For Each r In pending
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusFailed,
                    started, DateTime.UtcNow, err, pathBundle,
                    excludeFromDedupe:=False, sendSlot:=sendSlot)
            Next
            Return False
        End If

        SchedulerSnapshotSnapLog.Write("RUNJOB_CAPTURE",
            String.Format(CultureInfo.InvariantCulture, "jobId={0} sendSlot={1} mode={2} ok=True bundlePreview={3}", job.JobId, sendSlot, mode, ShortenForLog(pathBundle, 240)))

        Dim clinicWaId = WhatsAppService.GetCurrentClinicId()
        Dim snapshotConnected = Not String.IsNullOrWhiteSpace(clinicWaId) AndAlso
            Await WhatsAppService.TrySilentWhatsReconnectBackgroundAsync(clinicWaId).ConfigureAwait(True)
        Dim ownerWin = TryCast(Application.OpenForms("MainView3"), MainView3)
        If Not snapshotConnected AndAlso Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(ownerWin).ConfigureAwait(True) Then
            SchedulerSnapshotSnapLog.Write("RUNJOB_WA", String.Format(CultureInfo.InvariantCulture, "jobId={0} sendSlot={1} connected=False (cancelled or not linked)", job.JobId, sendSlot))
            Dim stFail = DateTime.UtcNow
            For Each r In pending
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusFailed,
                    stFail, DateTime.UtcNow, If(Eng, "WhatsApp not connected or cancelled.", "واتساب غير متصل أو تم الإلغاء."), pathBundle,
                    excludeFromDedupe:=False, sendSlot:=sendSlot)
            Next
            Return False
        End If

        SchedulerSnapshotSnapLog.Write("RUNJOB_WA", String.Format(CultureInfo.InvariantCulture, "jobId={0} sendSlot={1} connected=True", job.JobId, sendSlot))

        Dim baseCaption = If(String.IsNullOrWhiteSpace(job.MessageCaption), If(Eng, "Schedule snapshot", "لقطة الجدول"), job.MessageCaption.Trim())
        Dim cap1 = If(String.IsNullOrWhiteSpace(capCur), baseCaption, baseCaption & " — " & capCur.Trim())
        Dim cap2 = If(String.IsNullOrWhiteSpace(capNxt), baseCaption, baseCaption & " — " & capNxt.Trim())

        Return Await SendWhatsAppForPendingAsync(job, pending, runDate, mode, pathPngCur, pathPngNxt, pathHtmlCur, pathHtmlNxt, cap1, cap2, pathBundle, sendSlot, forceRun).ConfigureAwait(False)
    End Function

    Private Shared Function BuildLogMediaBundle(mode As SchedulerSnapshotAutoSendRepository.SendContentMode,
                                               pathPngCur As String, pathPngNxt As String,
                                               pathHtmlCur As String, pathHtmlNxt As String) As String
        Select Case mode
            Case SchedulerSnapshotAutoSendRepository.SendContentMode.PngOnly
                Return CombinedMediaPaths(pathPngCur, pathPngNxt)
            Case SchedulerSnapshotAutoSendRepository.SendContentMode.HtmlOnly
                Return CombinedMediaPaths(pathHtmlCur, pathHtmlNxt)
            Case Else
                Return CombinedMediaPaths(pathPngCur, pathHtmlCur, pathPngNxt, pathHtmlNxt)
        End Select
    End Function

    Private Shared Function AreCapturedPathsUsable(mode As SchedulerSnapshotAutoSendRepository.SendContentMode,
                                                  pathPngCur As String, pathPngNxt As String,
                                                  pathHtmlCur As String, pathHtmlNxt As String) As Boolean
        Select Case mode
            Case SchedulerSnapshotAutoSendRepository.SendContentMode.PngOnly
                Return Not String.IsNullOrWhiteSpace(pathPngCur) AndAlso Not String.IsNullOrWhiteSpace(pathPngNxt) AndAlso
                    File.Exists(pathPngCur) AndAlso File.Exists(pathPngNxt)
            Case SchedulerSnapshotAutoSendRepository.SendContentMode.HtmlOnly
                Return Not String.IsNullOrWhiteSpace(pathHtmlCur) AndAlso Not String.IsNullOrWhiteSpace(pathHtmlNxt) AndAlso
                    File.Exists(pathHtmlCur) AndAlso File.Exists(pathHtmlNxt)
            Case Else
                Return Not String.IsNullOrWhiteSpace(pathPngCur) AndAlso Not String.IsNullOrWhiteSpace(pathPngNxt) AndAlso
                    File.Exists(pathPngCur) AndAlso File.Exists(pathPngNxt) AndAlso
                    Not String.IsNullOrWhiteSpace(pathHtmlCur) AndAlso Not String.IsNullOrWhiteSpace(pathHtmlNxt) AndAlso
                    File.Exists(pathHtmlCur) AndAlso File.Exists(pathHtmlNxt)
        End Select
    End Function

    Private Shared Function CaptureFailureMessage(mode As SchedulerSnapshotAutoSendRepository.SendContentMode) As String
        Select Case mode
            Case SchedulerSnapshotAutoSendRepository.SendContentMode.PngOnly
                Return If(Eng, "Could not capture the two scheduled week snapshots as PNG.", "تعذر التقاط لقطتي الأسبوع المجدولتين كصورة.")
            Case SchedulerSnapshotAutoSendRepository.SendContentMode.HtmlOnly
                Return If(Eng, "Could not export the two scheduled week HTML files.", "تعذر تصدير ملفي HTML للأسبوعين المجدولين.")
            Case Else
                Return If(Eng, "Could not capture or export the two scheduled weeks (PNG and HTML).", "تعذر التقاط أو تصدير الأسبوعين المجدولين (صورة وHTML).")
        End Select
    End Function

    Private Shared Function ComputeSnapshotSlotExpiresUtc(runDate As Date, sendSlot As Byte, job As SchedulerSnapshotAutoSendJobRow) As DateTime
        Dim endOfRunDayLocal = DateTime.SpecifyKind(runDate.Date.AddDays(1), DateTimeKind.Local)
        Dim expiresUtc = endOfRunDayLocal.ToUniversalTime()
        Dim nowUtc = DateTime.UtcNow
        If expiresUtc < nowUtc Then expiresUtc = nowUtc
        Return expiresUtc
    End Function

    Private Shared Function BuildSchedulerOutboundMetaLines(jobId As Integer, recipientId As Long, runDate As Date, sendSlot As Byte,
                                                            mediaBundle As String, forceRun As Boolean, startedUtc As DateTime) As String
        Dim bundle = WhatsAppOutboundRepository.Trunc(If(mediaBundle, ""), 1900)
        Dim lines As String() = {
            jobId.ToString(CultureInfo.InvariantCulture),
            recipientId.ToString(CultureInfo.InvariantCulture),
            runDate.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            sendSlot.ToString(CultureInfo.InvariantCulture),
            bundle,
            If(forceRun, "1", "0"),
            If(forceRun, "1", "0"),
            startedUtc.Ticks.ToString(CultureInfo.InvariantCulture)
        }
        Return String.Join(vbLf, lines)
    End Function

    Private Shared Function BuildSnapshotPartPairs(mode As SchedulerSnapshotAutoSendRepository.SendContentMode,
                                                  cap1 As String, cap2 As String,
                                                  pathPngCur As String, pathPngNxt As String,
                                                  pathHtmlCur As String, pathHtmlNxt As String) As List(Of Tuple(Of String, String))
        Dim list As New List(Of Tuple(Of String, String))()
        Select Case mode
            Case SchedulerSnapshotAutoSendRepository.SendContentMode.PngOnly
                list.Add(Tuple.Create(cap1, pathPngCur))
                list.Add(Tuple.Create(cap2, pathPngNxt))
            Case SchedulerSnapshotAutoSendRepository.SendContentMode.HtmlOnly
                list.Add(Tuple.Create(cap1, pathHtmlCur))
                list.Add(Tuple.Create(cap2, pathHtmlNxt))
            Case Else
                list.Add(Tuple.Create(cap1, pathPngCur))
                list.Add(Tuple.Create(cap1, pathHtmlCur))
                list.Add(Tuple.Create(cap2, pathPngNxt))
                list.Add(Tuple.Create(cap2, pathHtmlNxt))
        End Select
        Return list
    End Function

    ''' <summary>Queues each WhatsApp attachment as dbo.WhatsAppOutboundMessage rows; Dispatcher retries through the local run day.</summary>
    Private Shared Async Function SendSchedulerSnapshotsThroughOutboxAsync(job As SchedulerSnapshotAutoSendJobRow,
                                                                        pending As System.Collections.Generic.List(Of SchedulerSnapshotAutoSendRecipientRow),
                                                                        runDate As Date,
                                                                        mode As SchedulerSnapshotAutoSendRepository.SendContentMode,
                                                                        pathPngCur As String, pathPngNxt As String,
                                                                        pathHtmlCur As String, pathHtmlNxt As String,
                                                                        cap1 As String, cap2 As String,
                                                                        pathBundleForLog As String,
                                                                        sendSlot As Byte,
                                                                        forceRun As Boolean,
                                                                        clinicId As String) As Task(Of Boolean)
        Dim expiresUtc = ComputeSnapshotSlotExpiresUtc(runDate, sendSlot, job)
        Dim sourceHint = If(forceRun, NameOf(SnapShotSender) & " · Test send", NameOf(SchedulerNew) & " · Auto snapshot")
        Dim queuedJobIds As New List(Of String)()
        Dim sentOk As Integer = 0
        Dim skippedWa As Integer = 0
        Dim failedSend As Integer = 0

        Dim partTemplate = BuildSnapshotPartPairs(mode, cap1, cap2, pathPngCur, pathPngNxt, pathHtmlCur, pathHtmlNxt)
        Dim flushGroupKeys As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        For Each r In pending
            Dim digits = WhatsHelper.BuildInternationalWhatsDigits(If(r.WhatsAppLocal, ""), If(r.WhatsAppPrefix, ""))
            Dim started = DateTime.UtcNow
            If String.IsNullOrWhiteSpace(digits) OrElse digits.Length < 8 Then
                skippedWa += 1
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusSkippedNoWhats,
                    started, DateTime.UtcNow, Nothing, pathBundleForLog,
                    excludeFromDedupe:=False, sendSlot:=sendSlot)
                Continue For
            End If

            Dim groupKey = $"{job.JobId}-{r.RecipientId}-{runDate:yyyyMMdd}-s{CInt(sendSlot)}-m{CInt(mode)}"
            Dim meta = BuildSchedulerOutboundMetaLines(job.JobId, r.RecipientId, runDate, sendSlot, pathBundleForLog, forceRun, started)

            Dim parts = partTemplate.Where(Function(tp) tp IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(tp.Item2) AndAlso IO.File.Exists(tp.Item2)).ToList()
            If parts.Count = 0 Then
                failedSend += 1
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusFailed,
                    started, DateTime.UtcNow, If(Eng, "No snapshot media files to queue.", "لا ملفات لقطة لتسجيل الإرسال."), pathBundleForLog,
                    excludeFromDedupe:=False, sendSlot:=sendSlot)
                Continue For
            End If

            Dim total = CByte(parts.Count)
            Dim enqueueFailed As Boolean = False
            For pi = 0 To parts.Count - 1
                Dim idemRaw = $"{groupKey}|part{pi.ToString(CultureInfo.InvariantCulture)}"
                Dim idem = WhatsAppOutboundRepository.Trunc(idemRaw, 128)
                Dim okOne = WhatsAppOutboundRepository.TryEnqueueSchedulerSnapshotPart(
                    clinicId.Trim(),
                    digits,
                    parts(pi).Item1,
                    parts(pi).Item2,
                    expiresUtc,
                    idem,
                    groupKey,
                    CByte(pi + 1),
                    total,
                    meta,
                    sourceHint,
                    revealMessageCenter:=forceRun,
                    suppressUi:=Not forceRun)
                If Not okOne Then
                    enqueueFailed = True
                    Exit For
                End If
            Next
            If enqueueFailed Then
                WhatsAppOutboundRepository.DeletePendingRowsForWaGroup(groupKey)
                failedSend += 1
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusFailed,
                    started, DateTime.UtcNow, If(Eng, "Failed to enqueue snapshot WhatsApp outbox.", "فشل تسجيل إرسال لقطة الجدول في قائمة انتظار واتساب."), pathBundleForLog,
                    excludeFromDedupe:=False, sendSlot:=sendSlot)
                Continue For
            End If

            flushGroupKeys.Add(groupKey)
            sentOk += 1
        Next

        Try
            If forceRun AndAlso flushGroupKeys.Count > 0 Then
                Await WhatsAppOutboundDispatchService.FlushWaGroupKeysAsync(flushGroupKeys, DateTime.UtcNow.AddSeconds(120), queuedJobIds).ConfigureAwait(False)
            Else
                Await WhatsAppOutboundDispatchService.FlushOutstandingAsync(Nothing, DateTime.UtcNow.AddSeconds(120), queuedJobIds).ConfigureAwait(False)
            End If
        Catch
        End Try

        SchedulerSnapshotSnapLog.Write("RUNJOB_SEND_SUMMARY",
            String.Format(CultureInfo.InvariantCulture,
                "jobId={0} sendSlot={1} runDate={2:yyyy-MM-dd} forceRun={3} clinicResolved={4} outbox recipientsQueued={5} skippedNoWhatsApp={6} enqueueFailed={7} queuedIdsCollected={8}",
                job.JobId, sendSlot, runDate, forceRun, True, sentOk, skippedWa, failedSend, queuedJobIds.Count))

        If Not forceRun Then Return False
        Dim queueMirrorOk = Await ConfirmMessagesReachedQueueAsync(clinicId, queuedJobIds).ConfigureAwait(False)
        If queueMirrorOk Then Return True
        If queuedJobIds.Count > 0 Then
            SchedulerSnapshotSnapLog.Write(
                "RUNJOB_CONFIRM_FALLBACK_DISPATCH_IDS",
                String.Format(CultureInfo.InvariantCulture,
                    "jobId={0} sendSlot={1} gatewayJobIdsCollected={2} hint=TryGetQueue did not list all IDs; dispatch returned job ids — treating test as OK",
                    job.JobId, sendSlot, queuedJobIds.Count))
            Return True
        End If
        Return False
    End Function

    Private Shared Async Function SendWhatsAppForPendingAsync(job As SchedulerSnapshotAutoSendJobRow,
                                                            pending As System.Collections.Generic.List(Of SchedulerSnapshotAutoSendRecipientRow),
                                                            runDate As Date,
                                                            mode As SchedulerSnapshotAutoSendRepository.SendContentMode,
                                                            pathPngCur As String,
                                                            pathPngNxt As String,
                                                            pathHtmlCur As String,
                                                            pathHtmlNxt As String,
                                                            cap1 As String,
                                                            cap2 As String,
                                                            pathBundleForLog As String,
                                                            sendSlot As Byte,
                                                            forceRun As Boolean) As Task(Of Boolean)
        Dim clinicId = WhatsAppService.GetCurrentClinicId()
        If String.IsNullOrWhiteSpace(clinicId) Then
            SchedulerSnapshotSnapLog.Write("RUNJOB_SEND_SUMMARY",
                String.Format(CultureInfo.InvariantCulture,
                    "jobId={0} sendSlot={1} runDate={2:yyyy-MM-dd} forceRun={3} clinicId=(none) sent=0 skippedNoWhatsApp=0 failed={4}",
                    job.JobId, sendSlot, runDate, forceRun, pending.Count))
            Dim st = DateTime.UtcNow
            For Each r In pending
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusFailed,
                    st, DateTime.UtcNow, If(Eng, "No clinic for WhatsApp.", "لا توجد عيادة لواتساب."), pathBundleForLog,
                    excludeFromDedupe:=False, sendSlot:=sendSlot)
            Next
            Return False
        End If

        If WhatsAppOutboundRepository.IsOutboundInfrastructureReady() Then
            Return Await SendSchedulerSnapshotsThroughOutboxAsync(
                job, pending, runDate, mode, pathPngCur, pathPngNxt, pathHtmlCur, pathHtmlNxt, cap1, cap2, pathBundleForLog, sendSlot, forceRun, clinicId.Trim()).ConfigureAwait(False)
        End If

        Dim ctxBase As New WhatsAppSendContext With {
            .Category = WhatsAppMessageCategories.SchedulerSnapshotAuto,
            .SourceHint = If(forceRun, NameOf(SnapShotSender) & " · Test send", NameOf(SchedulerNew) & " · Auto snapshot"),
            .RevealMessageCenter = forceRun
        }
        Dim queuedJobIds As New List(Of String)()
        Dim sentOk As Integer = 0
        Dim skippedWa As Integer = 0
        Dim failedSend As Integer = 0

        For Each r In pending
            Dim digits = WhatsHelper.BuildInternationalWhatsDigits(If(r.WhatsAppLocal, ""), If(r.WhatsAppPrefix, ""))
            Dim started = DateTime.UtcNow
            If String.IsNullOrWhiteSpace(digits) OrElse digits.Length < 8 Then
                skippedWa += 1
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusSkippedNoWhats,
                    started, DateTime.UtcNow, Nothing, pathBundleForLog,
                    excludeFromDedupe:=False, sendSlot:=sendSlot)
                Continue For
            End If

            Try
                Dim wa2 As New WhatsAppService()
                Select Case mode
                    Case SchedulerSnapshotAutoSendRepository.SendContentMode.PngOnly
                        CollectQueueJobId(queuedJobIds, Await wa2.SendMessageAsync(clinicId, digits, cap1, pathPngCur, ctxBase).ConfigureAwait(False))
                        CollectQueueJobId(queuedJobIds, Await wa2.SendMessageAsync(clinicId, digits, cap2, pathPngNxt, ctxBase).ConfigureAwait(False))
                    Case SchedulerSnapshotAutoSendRepository.SendContentMode.HtmlOnly
                        CollectQueueJobId(queuedJobIds, Await wa2.SendMessageAsync(clinicId, digits, cap1, pathHtmlCur, ctxBase).ConfigureAwait(False))
                        CollectQueueJobId(queuedJobIds, Await wa2.SendMessageAsync(clinicId, digits, cap2, pathHtmlNxt, ctxBase).ConfigureAwait(False))
                    Case Else
                        CollectQueueJobId(queuedJobIds, Await wa2.SendMessageAsync(clinicId, digits, cap1, pathPngCur, ctxBase).ConfigureAwait(False))
                        CollectQueueJobId(queuedJobIds, Await wa2.SendMessageAsync(clinicId, digits, cap1, pathHtmlCur, ctxBase).ConfigureAwait(False))
                        CollectQueueJobId(queuedJobIds, Await wa2.SendMessageAsync(clinicId, digits, cap2, pathPngNxt, ctxBase).ConfigureAwait(False))
                        CollectQueueJobId(queuedJobIds, Await wa2.SendMessageAsync(clinicId, digits, cap2, pathHtmlNxt, ctxBase).ConfigureAwait(False))
                End Select
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusSent,
                    started, DateTime.UtcNow, Nothing, pathBundleForLog,
                    excludeFromDedupe:=forceRun, sendSlot:=sendSlot)
                sentOk += 1
            Catch ex As Exception
                failedSend += 1
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusFailed,
                    started, DateTime.UtcNow, ex.Message, pathBundleForLog,
                    excludeFromDedupe:=False, sendSlot:=sendSlot)
            End Try
        Next

        SchedulerSnapshotSnapLog.Write("RUNJOB_SEND_SUMMARY",
            String.Format(CultureInfo.InvariantCulture,
                "jobId={0} sendSlot={1} runDate={2:yyyy-MM-dd} forceRun={3} clinicResolved={4} sentRows={5} skippedNoWhatsApp={6} failed={7} queuedIdsCollected={8}",
                job.JobId, sendSlot, runDate, forceRun, True, sentOk, skippedWa, failedSend, queuedJobIds.Count))

        If Not forceRun Then Return False
        Dim legacyMirrorOk = Await ConfirmMessagesReachedQueueAsync(clinicId, queuedJobIds).ConfigureAwait(False)
        If legacyMirrorOk Then Return True
        If queuedJobIds.Count > 0 Then
            SchedulerSnapshotSnapLog.Write(
                "RUNJOB_CONFIRM_FALLBACK_LEGACY_IDS",
                String.Format(CultureInfo.InvariantCulture, "jobId={0} collectedIds={1}", job.JobId, queuedJobIds.Count))
            Return True
        End If
        Return False
    End Function

    Private Shared Sub CollectQueueJobId(target As IList(Of String), responseJson As String)
        If target Is Nothing OrElse String.IsNullOrWhiteSpace(responseJson) Then Return
        Dim row As New WhatsAppActivityLogRow With {
            .SentAtUtc = DateTime.UtcNow,
            .ResponseOrError = responseJson
        }
        WhatsAppActivityLogRow.TryApplyQueueMetadataFromResponse(row)
        If Not String.IsNullOrWhiteSpace(row.QueueJobId) Then
            target.Add(row.QueueJobId.Trim())
        End If
    End Sub

    Private Shared Async Function ConfirmMessagesReachedQueueAsync(clinicId As String, queuedJobIds As IList(Of String)) As Task(Of Boolean)
        If String.IsNullOrWhiteSpace(clinicId) OrElse queuedJobIds Is Nothing OrElse queuedJobIds.Count = 0 Then Return False
        Dim expected = queuedJobIds.
            Where(Function(x) Not String.IsNullOrWhiteSpace(x)).
            Select(Function(x) x.Trim()).
            Distinct(StringComparer.OrdinalIgnoreCase).
            ToList()
        If expected.Count = 0 Then Return False

        Dim wa As New WhatsAppService()
        For attempt = 0 To 14
            Dim q = Await wa.TryGetQueueAsync(clinicId).ConfigureAwait(False)
            If q.HttpOk AndAlso q.Items IsNot Nothing Then
                Dim liveIds = q.Items.
                    Where(Function(x) x IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(x.JobId)).
                    Select(Function(x) x.JobId.Trim()).
                    Distinct(StringComparer.OrdinalIgnoreCase).
                    ToList()
                If expected.All(Function(id) liveIds.Contains(id, StringComparer.OrdinalIgnoreCase)) Then
                    Return True
                End If
            End If
            If attempt < 14 Then Await Task.Delay(850).ConfigureAwait(False)
        Next
        Return False
    End Function

    Private Shared Function ShortenForLog(s As String, maxLen As Integer) As String
        If String.IsNullOrEmpty(s) Then Return ""
        If maxLen < 4 Then Return ""
        If s.Length <= maxLen Then Return s
        Return s.Substring(0, maxLen - 3) & "..."
    End Function

    Private Shared Function CombinedMediaPaths(ParamArray parts As String()) As String
        Dim list = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Where(parts, Function(s1) Not String.IsNullOrEmpty(s1)))
        If list.Count = 0 Then Return Nothing
        Dim s = String.Join("|", list)
        If s.Length <= 500 Then Return s
        Return s.Substring(0, 500)
    End Function
End Class
