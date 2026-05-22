Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks

''' <summary>
''' Sends WhatsApp reminders automatically for appointments that will occur in ~24 hours.
''' Run periodically (e.g. hourly) from the main form timer.
''' </summary>
Public Class AppointmentReminderService
    Private Shared ReadOnly _sentFilePath As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "DentistX", "AppointmentRemindersSent.txt")

    ''' <summary>
    ''' Sends reminders for all appointments starting in 23–25 hours. Tracks sent reminders to avoid duplicates.
    ''' </summary>
    Public Shared Async Function RunAsync() As Task(Of ReminderRunResult)
        Dim result As New ReminderRunResult()
        ApptTwoHourReminderQueueRepository.FinalizeOverduePendingLegs()
        Dim rawClinicId As String = WhatsAppService.GetCurrentClinicId()
        Dim clinicIdForQueue As String = If(String.IsNullOrWhiteSpace(rawClinicId), "", rawClinicId)

        Await ApptWhatsAppReminderQueueProcessor.ProcessDueRemindersAsync(clinicIdForQueue, result).ConfigureAwait(False)
        Try
            Await WhatsAppOutboundDispatchService.FlushOutstandingAsync(result, DateTime.UtcNow.AddSeconds(26), Nothing).ConfigureAwait(False)
        Catch
        End Try
        Return result
    End Function

    ''' <summary>Legacy no-op: 24h rows are created when appointments are saved. Kept for callers.</summary>
    Public Shared Sub Enqueue24HourCandidates(clinicId As String)
    End Sub

    ''' <summary>Body text for ~24h automatic reminder. <paramref name="messageEnglish"/> when set (e.g. editor RadioLang at sync time); otherwise Arabic until the user picks English.</summary>
    Public Shared Function BuildReminderMessageBody(appt As AppointmentReminderDto, Optional messageEnglish As Boolean? = Nothing) As String
        If appt Is Nothing Then Return ""
        Dim useE = If(messageEnglish.HasValue, messageEnglish.Value, False)
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
            includeReason:=appt.WhatsIncludeReason,
            includeNotes:=appt.WhatsIncludeNotes)
    End Function

    Private Shared Function BuildReminderMessage(appt As AppointmentReminderDto) As String
        Return BuildReminderMessageBody(appt)
    End Function

    ''' <summary>Check if a reminder was already sent for this appointment.</summary>
    Public Shared Function IsReminderSent(appointmentId As Integer) As Boolean
        Dim sent = LoadSentAppointmentIds()
        Return sent.ContainsKey(appointmentId)
    End Function

    ''' <summary>Mark an appointment reminder as sent.</summary>
    Public Shared Sub MarkReminderSent(appointmentId As Integer)
        Dim sent = LoadSentAppointmentIds()
        sent(appointmentId) = DateTime.Now
        SaveSentAppointmentIds(sent)
    End Sub

    ''' <summary>Send a single reminder for one appointment. Returns True if sent successfully.</summary>
    Public Shared Async Function SendReminderForAppointmentAsync(appt As AppointmentReminderDto,
                                                                Optional messageEnglish As Boolean? = Nothing,
                                                                Optional patientPhoneDigitsOverride As String = Nothing) As Task(Of Boolean)
        Dim clinicId As String = WhatsAppService.GetCurrentClinicId()
        If String.IsNullOrWhiteSpace(clinicId) Then Return False

        If Not Await WhatsAppService.TrySilentWhatsReconnectBackgroundAsync(clinicId).ConfigureAwait(False) Then Return False

        Dim number As String
        If Not String.IsNullOrWhiteSpace(patientPhoneDigitsOverride) Then
            number = WhatsHelper.NormalizeWhatsDigits(patientPhoneDigitsOverride)
        Else
            number = If(String.IsNullOrWhiteSpace(appt.PatientPhone), "", appt.PatientPhone.Trim())
            If String.IsNullOrWhiteSpace(number) Then
                number = WhatsHelper.BuildInternationalWhatsDigitsFromPatient(
                    If(appt.PatientWhatsLocal, ""),
                    If(appt.PatientPhoneFallback, ""),
                    If(appt.PatientWhatsAppPrefix, ""))
            End If
            number = WhatsHelper.NormalizeWhatsDigits(number)
        End If
        If String.IsNullOrWhiteSpace(number) Then Return False

        Try
            Dim msg = BuildReminderMessageBody(appt, messageEnglish)
            Dim waService2 As New WhatsAppService()
            Dim ctx = New WhatsAppSendContext With {
                .Category = WhatsAppMessageCategories.AppointmentReminder,
                .PatientId = appt.PatientID,
                .SourceHint = NameOf(AppointmentReminderService),
                .DisplayName = appt.PatientName,
                .AppointmentId = appt.AppointmentID,
                .AppointmentStartUtc = appt.StartDateTime,
                .IdempotencyKey = WhatsAppOutboundRepository.BuildManual24hReminderIdempotencyKey(appt.AppointmentID, appt.StartDateTime)
            }
            Dim resp = Await waService2.SendMessageAsync(clinicId, number, msg, "", ctx)
            Dim intr As WhatsAppOutboundSendInterpretation = Nothing
            If WhatsAppService.TryInterpretOutboundSendResponse(resp, intr) Then
                If Not String.IsNullOrWhiteSpace(intr.TerminalPriorStatus) Then
                    If Not String.Equals(intr.TerminalPriorStatus, WhatsAppOutboundStatuses.Sent, StringComparison.OrdinalIgnoreCase) Then
                        Return False
                    End If
                    MarkReminderSent(appt.AppointmentID)
                    Return True
                End If
                Return True
            End If
            MarkReminderSent(appt.AppointmentID)
            Return True
        Catch
            Return False
        End Try
    End Function

    Private Shared Function LoadSentAppointmentIds() As Dictionary(Of Integer, DateTime)
        Dim result As New Dictionary(Of Integer, DateTime)
        Try
            If Not File.Exists(_sentFilePath) Then Return result
            Dim cutoff = DateTime.Now.AddHours(-26)
            Dim lines = File.ReadAllLines(_sentFilePath)
            For Each line In lines
                Dim parts = line.Split("|"c)
                If parts.Length >= 2 Then
                    Dim id As Integer
                    Dim dt As DateTime
                    If Integer.TryParse(parts(0), id) AndAlso DateTime.TryParse(parts(1), dt) AndAlso dt > cutoff Then
                        result(id) = dt
                    End If
                End If
            Next
        Catch
        End Try
        Return result
    End Function

    Private Shared Sub SaveSentAppointmentIds(ids As Dictionary(Of Integer, DateTime))
        Try
            Dim dir = Path.GetDirectoryName(_sentFilePath)
            If Not String.IsNullOrEmpty(dir) Then Directory.CreateDirectory(dir)
            Dim lines = ids.Select(Function(kv) $"{kv.Key}|{kv.Value:yyyy-MM-dd HH:mm:ss}").ToArray()
            File.WriteAllLines(_sentFilePath, lines)
        Catch
        End Try
    End Sub
End Class
