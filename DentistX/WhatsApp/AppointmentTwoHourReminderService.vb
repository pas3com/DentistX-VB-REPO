Imports System.Globalization
Imports System.Threading.Tasks

''' <summary>Short-lead leg of appointment WhatsApp reminders; rows and send times live in <see cref="ApptTwoHourReminderQueueRepository"/>.</summary>
Public Class AppointmentTwoHourReminderService

    Public Shared Async Function RunAsync() As Task(Of ReminderRunResult)
        Dim result As New ReminderRunResult()
        ApptTwoHourReminderQueueRepository.FinalizeOverduePendingLegs()
        Dim rawClinicId As String = WhatsAppService.GetCurrentClinicId()

        Dim connected = False
        If Not String.IsNullOrWhiteSpace(rawClinicId) Then
            Try
                Dim wa As New WhatsAppService()
                connected = Await wa.GetConnectionStatusAsync(rawClinicId)
            Catch
            End Try
        End If
        If connected Then
            Await ApptWhatsAppReminderQueueProcessor.ProcessDueRemindersAsync(rawClinicId, result)
        End If
        Return result
    End Function

    ''' <summary>Legacy no-op: rows are synced on appointment save.</summary>
    Public Shared Sub EnqueueTwoHourCandidates(clinicId As String)
    End Sub

    ''' <summary>~2h leg message: same clinic/title/salutation/header rules as manual appointment WhatsApp; title includes urgency (≈N hours).</summary>
    Public Shared Function BuildTwoHourReminderBody(appt As AppointmentReminderDto, Optional messageEnglish As Boolean? = Nothing) As String
        If appt Is Nothing Then Return ""
        Dim useE = If(messageEnglish.HasValue, messageEnglish.Value, Eng)
        Dim hRaw = ApptTwoHourReminderQueueRepository.GetShortReminderHours()
        Dim hrs = CInt(Math.Floor(hRaw))
        If hrs < 1 Then hrs = 1
        Dim titleEn As String
        If Math.Abs(hRaw - hrs) > 0.05R Then
            titleEn = "Appointment reminder — starting within about " & hRaw.ToString("0.#", CultureInfo.InvariantCulture) & " hours"
        Else
            titleEn = "Appointment reminder — starting within about " & hrs.ToString() & If(hrs = 1, " hour", " hours")
        End If
        Dim titleAr = "تذكير بموعد — يبدأ خلال " & ApptTwoHourReminderQueueRepository.FormatArabicApproximateDurationFromShortReminderSetting()
        Return WhatsHelper.BuildAppointmentWhatsAppMessage(
            If(appt.PatientName, ""),
            If(appt.DrName, ""),
            appt.StartDateTime.Date,
            appt.StartDateTime,
            appt.EndDateTime,
            If(appt.Reason, ""),
            If(appt.Notes, ""),
            If(appt.Status, ""),
            useE,
            patientSex:=If(appt.PatientSex, Nothing),
            titleEnglish:=titleEn,
            titleArabic:=titleAr,
            includeReason:=appt.WhatsIncludeReason,
            includeNotes:=appt.WhatsIncludeNotes)
    End Function
End Class
