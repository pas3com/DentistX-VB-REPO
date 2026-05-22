Imports System.Collections.Generic
Imports System.Globalization
Imports System.Threading.Tasks

''' <summary>Sends due 24h/2h legs from dbo.ApptTwoHourWhatsAppQueue when send time is reached.</summary>
Public NotInheritable Class ApptWhatsAppReminderQueueProcessor

    Public Shared Async Function ProcessDueRemindersAsync(clinicId As String, result As ReminderRunResult) As Task
        If result Is Nothing Then result = New ReminderRunResult()
        If String.IsNullOrWhiteSpace(clinicId) Then Return

        ApptTwoHourReminderQueueRepository.DeleteOrphansMissingAppointment()

        Dim rows = ApptTwoHourReminderQueueRepository.GetRowsWithDueReminders()
        If rows Is Nothing OrElse rows.Count = 0 Then Return

        Dim refreshRepo As New AppointmentCRepository()
        Dim now = DateTime.Now

        For Each row In rows
            Dim dto = refreshRepo.GetAppointmentReminderById(row.AppointmentId)
            If dto Is Nothing OrElse Not refreshRepo.AppointmentExists(row.AppointmentId) Then
                Dim previews = ApptReminderAppointmentMissingNotifier.FormatQueueRowPreviewsForDialog(row)
                ApptReminderAppointmentMissingNotifier.Show(row.AppointmentId, row.PatientName, row.DrName, row.ApptStartSnapshot, previews, Nothing)
                ApptTwoHourReminderQueueRepository.DeleteByAppointmentId(row.AppointmentId)
                Continue For
            End If
            If Not ApptTwoHourReminderQueueRepository.AppointmentStartMatchesQueueSnapshot(dto.StartDateTime, row.ApptStartSnapshot) Then
                ApptTwoHourReminderQueueRepository.ResyncFromAppointmentPreservingLegInclusion(row)
                Continue For
            End If

            Dim number As String = ResolvePhone(dto, row)

            If row.Status24h = ApptTwoHourReminderQueueRepository.StatusPending AndAlso
                row.SendAt24h.HasValue AndAlso row.SendAt24h.Value <= now Then
                Await SendLegAsync(refreshRepo, clinicId, row, dto, number, is24h:=True, result)
            End If

            If row.Status2h = ApptTwoHourReminderQueueRepository.StatusPending AndAlso
                row.SendAt2h.HasValue AndAlso row.SendAt2h.Value <= now AndAlso
                refreshRepo.AppointmentExists(row.AppointmentId) Then
                Await SendLegAsync(refreshRepo, clinicId, row, dto, number, is24h:=False, result)
            End If
        Next
    End Function

    Private Shared Function ResolvePhone(dto As AppointmentReminderDto, row As ApptTwoHourReminderQueueRow) As String
        Dim s As String = ""
        If dto IsNot Nothing Then
            s = If(dto.PatientPhone, "").Trim()
            If String.IsNullOrWhiteSpace(s) Then
                s = WhatsHelper.BuildInternationalWhatsDigitsFromPatient(
                    If(dto.PatientWhatsLocal, ""),
                    If(dto.PatientPhoneFallback, ""),
                    If(dto.PatientWhatsAppPrefix, ""))
            End If
        End If
        If String.IsNullOrWhiteSpace(s) Then s = If(row.TargetPhone, "").Trim()
        Return WhatsHelper.NormalizeWhatsDigits(s)
    End Function

    Private Shared Async Function SendLegAsync(refreshRepo As AppointmentCRepository, clinicId As String, row As ApptTwoHourReminderQueueRow,
                                              dto As AppointmentReminderDto, number As String, is24h As Boolean,
                                              result As ReminderRunResult) As Task
        ' Prefer text stored at queue sync (includes RadioLang from save). Rebuild from dto only if preview missing (Arabic unless messageEnglish was supplied at sync).
        Dim body = If(is24h, If(row.MessagePreview24h, ""), If(row.MessagePreview2h, "")).Trim()
        If String.IsNullOrWhiteSpace(body) AndAlso dto IsNot Nothing Then
            body = If(is24h, AppointmentReminderService.BuildReminderMessageBody(dto), AppointmentTwoHourReminderService.BuildTwoHourReminderBody(dto)).Trim()
        End If

        If Not refreshRepo.AppointmentExists(row.AppointmentId) Then
            Dim legEn = If(is24h, "You were about to send the ~24-hour reminder.", "You were about to send the short-lead reminder.")
            Dim legAr = If(is24h, "كنت على وشك إرسال تذكير نحو 24 ساعة.", "كنت على وشك إرسال التذكير قبل بدء الموعد.")
            ApptReminderAppointmentMissingNotifier.Show(row.AppointmentId, row.PatientName, row.DrName, row.ApptStartSnapshot, body,
                If(Eng, legEn, legAr))
            ApptTwoHourReminderQueueRepository.DeleteByAppointmentId(row.AppointmentId)
            Return
        End If

        If String.IsNullOrWhiteSpace(number) Then
            If is24h Then
                ApptTwoHourReminderQueueRepository.TryFinalize24h(row.QueueId, ApptTwoHourReminderQueueRepository.StatusSkippedNoPhone, Nothing, Nothing)
            Else
                ApptTwoHourReminderQueueRepository.TryFinalize2h(row.QueueId, ApptTwoHourReminderQueueRepository.StatusSkippedNoPhone, Nothing, Nothing)
            End If
            Return
        End If

        If String.IsNullOrWhiteSpace(body) Then
            If is24h Then
                ApptTwoHourReminderQueueRepository.TryFinalize24h(row.QueueId, ApptTwoHourReminderQueueRepository.StatusFailed, "Empty message body.", Nothing)
            Else
                ApptTwoHourReminderQueueRepository.TryFinalize2h(row.QueueId, ApptTwoHourReminderQueueRepository.StatusFailed, "Empty message body.", Nothing)
            End If
            Return
        End If

        Dim category As String
        Dim sourceHint As String
        If is24h Then
            category = WhatsAppMessageCategories.AppointmentReminder
            sourceHint = NameOf(AppointmentReminderService)
        Else
            category = WhatsAppMessageCategories.AppointmentTwoHourReminder
            sourceHint = NameOf(AppointmentTwoHourReminderService)
        End If

        Dim patId As Integer? = row.PatientId
        If dto IsNot Nothing Then patId = dto.PatientID

        Dim disp = If(String.IsNullOrWhiteSpace(row.PatientName), Nothing, row.PatientName.Trim())
        Dim ctx = New WhatsAppSendContext With {
            .Category = category,
            .PatientId = patId,
            .SourceHint = sourceHint,
            .DisplayName = disp,
            .SentAfterLocalQueue = True
        }

        If WhatsAppOutboundRepository.IsOutboundInfrastructureReady() Then
            Dim clinicEnqueue = If(String.IsNullOrWhiteSpace(row.ClinicId), clinicId, row.ClinicId).Trim()
            If String.IsNullOrWhiteSpace(clinicEnqueue) Then
                If is24h Then ApptTwoHourReminderQueueRepository.TryFinalize24h(row.QueueId, ApptTwoHourReminderQueueRepository.StatusFailed, "No clinic Id for outbound.", Nothing)
                Else ApptTwoHourReminderQueueRepository.TryFinalize2h(row.QueueId, ApptTwoHourReminderQueueRepository.StatusFailed, "No clinic Id for outbound.", Nothing)
                Return
            End If
            If WhatsAppOutboundRepository.TryEnqueueAppointmentReminder(clinicEnqueue, row, dto, number, body, is24h) Then
                WhatsAppOutboundDispatchService.RequestImmediateDispatch()
            End If
            Return
        End If

        Try
            Dim minUtc = DateTime.UtcNow.AddSeconds(-15)
            Dim waSend As New WhatsAppService()
            Await waSend.SendMessageAsync(clinicId, number, body, "", ctx)
            Dim logId = WhatsAppActivityLogRepository.TryGetLatestLogId(patId, category, minUtc)
            Dim logNullable As Long? = If(logId > 0, logId, Nothing)

            Dim ok As Boolean
            If is24h Then
                ok = ApptTwoHourReminderQueueRepository.TryFinalize24h(row.QueueId, ApptTwoHourReminderQueueRepository.StatusSent, Nothing, logNullable)
            Else
                ok = ApptTwoHourReminderQueueRepository.TryFinalize2h(row.QueueId, ApptTwoHourReminderQueueRepository.StatusSent, Nothing, logNullable)
            End If

            If ok Then
                result.SentCount += 1
                Dim name = If(String.IsNullOrWhiteSpace(row.PatientName), "(no name)", row.PatientName.Trim())
                Dim dr = If(String.IsNullOrWhiteSpace(row.DrName), "", row.DrName.Trim())
                Dim tag = If(is24h, "24h", "2h")
                Dim dPart = WhatsHelper.FormatWhatsAppDateLong(row.ApptStartSnapshot.Date, True) & " " & row.ApptStartSnapshot.ToString("HH:mm", CultureInfo.InvariantCulture)
                result.Lines.Add($"{tag} reminder · {name} · {dPart}" & If(String.IsNullOrEmpty(dr), "", $" · {dr}"))
                If is24h Then
                    AppointmentReminderService.MarkReminderSent(row.AppointmentId)
                End If
            End If
        Catch ex As Exception
            If is24h Then
                ApptTwoHourReminderQueueRepository.TryFinalize24h(row.QueueId, ApptTwoHourReminderQueueRepository.StatusFailed, ex.Message, Nothing)
            Else
                ApptTwoHourReminderQueueRepository.TryFinalize2h(row.QueueId, ApptTwoHourReminderQueueRepository.StatusFailed, ex.Message, Nothing)
            End If
        End Try
    End Function

    ''' <summary>Process any due sends (after saving appointments this adds rows). Enqueues outbound without requiring WhatsApp connected; dispatcher flushes when online.</summary>
    Public Shared Async Function RunEnqueueAllAndProcessAsync() As Task(Of ReminderRunResult)
        Dim result As New ReminderRunResult()
        Dim rawClinicId As String = WhatsAppService.GetCurrentClinicId()
        Await ProcessDueRemindersAsync(rawClinicId, result)
        Try
            Await WhatsAppOutboundDispatchService.FlushOutstandingAsync(result, DateTime.UtcNow.AddSeconds(26), Nothing).ConfigureAwait(False)
        Catch
        End Try
        Return result
    End Function
End Class
