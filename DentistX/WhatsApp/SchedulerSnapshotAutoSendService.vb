Imports System
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms

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
    Private Const DueGraceMinutes As Integer = 15
    Private Shared _tickBusy As Integer
    Private Shared _timer As Windows.Forms.Timer
    Private Shared _uiSuppressionCount As Integer

    Public Shared Sub EnsureStarted()
        SyncLock SyncRoot
            If _timer Is Nothing Then
                _timer = New Windows.Forms.Timer With {.Interval = 60000}
                AddHandler _timer.Tick, AddressOf OnTimerTick
                _timer.Start()
            End If
        End SyncLock
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
                     Catch
                     Finally
                         System.Threading.Interlocked.Exchange(_tickBusy, 0)
                     End Try
                 End Function)
    End Sub

    Private Shared Async Function RunDueJobsAsync() As Task
        Dim mv = TryCast(Application.OpenForms("MainView3"), MainView3)
        If mv Is Nothing OrElse mv.IsDisposed OrElse Not mv.IsHandleCreated Then Return
        If Threading.Volatile.Read(_uiSuppressionCount) > 0 OrElse mv.ShouldDeferSchedulerBackgroundWork() Then Return

        Dim nowLocal = DateTime.Now
        Dim today = nowLocal.Date

        Dim jobs As System.Collections.Generic.IList(Of SchedulerSnapshotAutoSendJobRow) = Nothing
        Try
            jobs = SchedulerSnapshotAutoSendRepository.GetJobs()
        Catch
            Return
        End Try

        If jobs Is Nothing OrElse jobs.Count = 0 Then Return

        For Each job In jobs
            If Not job.IsEnabled Then Continue For
            If Not IsJobDueWithinGraceWindow(job, nowLocal) Then Continue For

            Dim runForDay As Date = today
            Dim tcs As New Threading.Tasks.TaskCompletionSource(Of Boolean)
            mv.BeginInvoke(Async Sub()
                               Try
                                   Await RunJobAsync(job, runForDay, forceRun:=False).ConfigureAwait(False)
                               Catch
                               Finally
                                   tcs.SetResult(True)
                               End Try
                           End Sub)
            Await tcs.Task.ConfigureAwait(False)
        Next
    End Function

    Private Shared Function IsJobDueAtThisMinute(job As SchedulerSnapshotAutoSendJobRow, nowLocal As DateTime) As Boolean
        Dim bit = SchedulerSnapshotAutoSendRepository.DayOfWeekToBit(nowLocal.DayOfWeek)
        If (job.DaysOfWeekMask And bit) = 0 Then Return False
        Dim st = job.SendTimeLocal
        Return nowLocal.Hour = st.Hours AndAlso nowLocal.Minute = st.Minutes
    End Function

    Private Shared Function IsJobDueWithinGraceWindow(job As SchedulerSnapshotAutoSendJobRow, nowLocal As DateTime) As Boolean
        Dim bit = SchedulerSnapshotAutoSendRepository.DayOfWeekToBit(nowLocal.DayOfWeek)
        If (job.DaysOfWeekMask And bit) = 0 Then Return False

        Dim st = job.SendTimeLocal
        Dim dueLocal = nowLocal.Date.Add(st)
        Dim delta = nowLocal - dueLocal
        Return delta >= TimeSpan.Zero AndAlso delta <= TimeSpan.FromMinutes(DueGraceMinutes)
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

    ''' <summary>
    ''' Call from the UI thread: captures snapshots on the UI thread, then sends WhatsApp without blocking the UI (no sync .GetResult()).
    ''' </summary>
    Public Shared Async Function RunJobAsync(job As SchedulerSnapshotAutoSendJobRow, runDate As Date, forceRun As Boolean) As Task(Of Boolean)
        Dim recipients = SchedulerSnapshotAutoSendRepository.GetRecipients(job.JobId).
            Where(Function(r) r.IsActive).ToList()
        If recipients.Count = 0 Then Return False

        Dim pending = If(forceRun,
            recipients,
            recipients.Where(Function(r) Not SchedulerSnapshotAutoSendRepository.HasRecipientSentOnRunDate(job.JobId, r.RecipientId, runDate)).ToList())
        If pending.Count = 0 Then Return False

        Dim mode = GetSendContentMode(job)
        Dim capCur As String = Nothing
        Dim capNxt As String = Nothing
        Dim pathPngCur As String = Nothing
        Dim pathPngNxt As String = Nothing
        Dim pathHtmlCur As String = Nothing
        Dim pathHtmlNxt As String = Nothing

        Dim ok = SchedulerWeekSnapshotBackgroundCapture.TrySaveCurrentAndNextWeekMedia(
            runDate, mode, capCur, capNxt, pathPngCur, pathPngNxt, pathHtmlCur, pathHtmlNxt,
            useScheduledWeekRule:=True)

        Dim pathBundle = BuildLogMediaBundle(mode, pathPngCur, pathPngNxt, pathHtmlCur, pathHtmlNxt)

        If Not ok OrElse Not AreCapturedPathsUsable(mode, pathPngCur, pathPngNxt, pathHtmlCur, pathHtmlNxt) Then
            Dim started = DateTime.UtcNow
            Dim err = CaptureFailureMessage(mode)
            For Each r In pending
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusFailed,
                    started, DateTime.UtcNow, err, pathBundle)
            Next
            Return False
        End If

        Dim ownerWin = TryCast(Application.OpenForms("MainView3"), MainView3)
        If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(ownerWin).ConfigureAwait(True) Then
            Dim stFail = DateTime.UtcNow
            For Each r In pending
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusFailed,
                    stFail, DateTime.UtcNow, If(Eng, "WhatsApp not connected or cancelled.", "واتساب غير متصل أو تم الإلغاء."), pathBundle)
            Next
            Return False
        End If

        Dim baseCaption = If(String.IsNullOrWhiteSpace(job.MessageCaption), If(Eng, "Schedule snapshot", "لقطة الجدول"), job.MessageCaption.Trim())
        Dim cap1 = If(String.IsNullOrWhiteSpace(capCur), baseCaption, baseCaption & " — " & capCur.Trim())
        Dim cap2 = If(String.IsNullOrWhiteSpace(capNxt), baseCaption, baseCaption & " — " & capNxt.Trim())

        Return Await SendWhatsAppForPendingAsync(job, pending, runDate, mode, pathPngCur, pathPngNxt, pathHtmlCur, pathHtmlNxt, cap1, cap2, pathBundle, forceRun).ConfigureAwait(False)
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
                                                            forceRun As Boolean) As Task(Of Boolean)
        Dim clinicId = WhatsAppService.GetCurrentClinicId()
        If String.IsNullOrWhiteSpace(clinicId) Then
            Dim st = DateTime.UtcNow
            For Each r In pending
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusFailed,
                    st, DateTime.UtcNow, If(Eng, "No clinic for WhatsApp.", "لا توجد عيادة لواتساب."), pathBundleForLog)
            Next
            Return False
        End If

        Dim ctxBase As New WhatsAppSendContext With {
            .Category = WhatsAppMessageCategories.SchedulerSnapshotAuto,
            .SourceHint = If(forceRun, NameOf(SnapShotSender) & " · Test send", NameOf(SchedulerNew) & " · Auto snapshot"),
            .RevealMessageCenter = forceRun
        }
        Dim queuedJobIds As New List(Of String)()

        For Each r In pending
            Dim digits = WhatsHelper.BuildInternationalWhatsDigits(If(r.WhatsAppLocal, ""), If(r.WhatsAppPrefix, ""))
            Dim started = DateTime.UtcNow
            If String.IsNullOrWhiteSpace(digits) OrElse digits.Length < 8 Then
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusSkippedNoWhats,
                    started, DateTime.UtcNow, Nothing, pathBundleForLog)
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
                    started, DateTime.UtcNow, Nothing, pathBundleForLog)
            Catch ex As Exception
                SchedulerSnapshotAutoSendRepository.InsertLogEntry(
                    job.JobId, r.RecipientId, runDate,
                    SchedulerSnapshotAutoSendRepository.LogStatusFailed,
                    started, DateTime.UtcNow, ex.Message, pathBundleForLog)
            End Try
        Next

        If Not forceRun Then Return False
        Return Await ConfirmMessagesReachedQueueAsync(clinicId, queuedJobIds).ConfigureAwait(False)
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
        For attempt = 0 To 4
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
            If attempt < 4 Then Await Task.Delay(700).ConfigureAwait(False)
        Next
        Return False
    End Function

    Private Shared Function CombinedMediaPaths(ParamArray parts As String()) As String
        Dim list = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Where(parts, Function(s1) Not String.IsNullOrEmpty(s1)))
        If list.Count = 0 Then Return Nothing
        Dim s = String.Join("|", list)
        If s.Length <= 500 Then Return s
        Return s.Substring(0, 500)
    End Function
End Class
