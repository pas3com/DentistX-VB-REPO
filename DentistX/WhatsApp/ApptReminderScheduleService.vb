Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports Dapper

''' <summary>Manages recurring appointment reminders: which patients get reminders and how often.</summary>
''' <remarks>Storage: %LocalAppData%\DentistX\ApptReminderSchedule.txt, format: PatientID|IntervalDays|LastSentAt</remarks>
Public Class ApptReminderScheduleService
    Private Shared ReadOnly _schedulePath As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "DentistX", "ApptReminderSchedule.txt")

    ''' <summary>Schedule entry: PatientID, IntervalDays, LastSentAt (Nothing = never sent).</summary>
    Public Class ScheduleEntry
        Public Property PatientID As Integer
        Public Property IntervalDays As Integer
        Public Property LastSentAt As DateTime?
        ''' <summary>When Nothing, scheduled send uses Arabic until the user sets a language on the row.</summary>
        Public Property MessageEnglish As Boolean?
    End Class

    ''' <summary>Get all scheduled patients.</summary>
    Public Shared Function GetAllScheduled() As List(Of ScheduleEntry)
        Dim result As New List(Of ScheduleEntry)
        Try
            If Not File.Exists(_schedulePath) Then Return result
            For Each line In File.ReadAllLines(_schedulePath)
                Dim parts = line.Split("|"c)
                If parts.Length >= 2 Then
                    Dim pid As Integer
                    Dim days As Integer
                        If Integer.TryParse(parts(0), pid) AndAlso Integer.TryParse(parts(1), days) AndAlso days > 0 Then
                            Dim lastSent As DateTime? = Nothing
                            If parts.Length >= 3 AndAlso Not String.IsNullOrWhiteSpace(parts(2)) Then
                                Dim dt As DateTime
                                If DateTime.TryParse(parts(2), dt) Then lastSent = dt
                            End If
                            Dim msgEng As Boolean? = Nothing
                            If parts.Length >= 4 AndAlso Not String.IsNullOrWhiteSpace(parts(3)) Then
                                Dim p4 = parts(3).Trim()
                                Dim b As Boolean
                                If Boolean.TryParse(p4, b) Then
                                    msgEng = b
                                ElseIf p4 = "1" Then
                                    msgEng = True
                                ElseIf p4 = "0" Then
                                    msgEng = False
                                End If
                            End If
                            result.Add(New ScheduleEntry() With {.PatientID = pid, .IntervalDays = days, .LastSentAt = lastSent, .MessageEnglish = msgEng})
                        End If
                End If
            Next
        Catch
        End Try
        Return result
    End Function

    ''' <summary>Add or update a patient in the schedule.</summary>
    Public Shared Sub AddOrUpdate(patientId As Integer, intervalDays As Integer, Optional messageEnglish As Boolean? = Nothing)
        If intervalDays < 1 Then intervalDays = 1
        Dim list = GetAllScheduled()
        Dim existing = list.FirstOrDefault(Function(x) x.PatientID = patientId)
        If existing IsNot Nothing Then
            existing.IntervalDays = intervalDays
            If messageEnglish.HasValue Then existing.MessageEnglish = messageEnglish
        Else
            list.Add(New ScheduleEntry() With {
                .PatientID = patientId,
                .IntervalDays = intervalDays,
                .LastSentAt = Nothing,
                .MessageEnglish = messageEnglish
            })
        End If
        SaveAll(list)
    End Sub

    ''' <summary>Remove a patient from the schedule.</summary>
    Public Shared Sub Remove(patientId As Integer)
        Dim list = GetAllScheduled().Where(Function(x) x.PatientID <> patientId).ToList()
        SaveAll(list)
    End Sub

    ''' <summary>Run scheduled reminders: send to patients whose interval has elapsed.</summary>
    Public Shared Async Function RunAsync() As Task(Of Integer)
        Dim clinicId As String = WhatsAppService.GetCurrentClinicId()
        If String.IsNullOrWhiteSpace(clinicId) Then Return 0

        If Not Await WhatsAppService.TrySilentWhatsReconnectBackgroundAsync(clinicId).ConfigureAwait(False) Then Return 0

        Dim schedule = GetAllScheduled()
        If schedule.Count = 0 Then Return 0

        Dim now = DateTime.Now
        Dim toSend As New List(Of ScheduleEntry)
        For Each entry In schedule
            Dim shouldSend As Boolean
            If Not entry.LastSentAt.HasValue Then
                shouldSend = True
            Else
                Dim elapsed = (now - entry.LastSentAt.Value).TotalDays
                shouldSend = elapsed >= entry.IntervalDays
            End If
            If shouldSend Then toSend.Add(entry)
        Next
        If toSend.Count = 0 Then Return 0

        Dim sentCount As Integer = 0
        Dim list = GetAllScheduled()
        Dim waServiceSend As New WhatsAppService()
        For Each entry In toSend
            Dim patientName = ""
            Dim patientSex As String = Nothing
            Dim patientPhone = ""
            Try
                Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                    Dim row = conn.QueryFirstOrDefault(Of PatientPhoneRow)(
                        "SELECT PatientName, Sex AS PatientSex, ISNULL(NULLIF(RTRIM(WhatsApp), ''), RTRIM(Phone)) AS PatientPhone FROM Patient WHERE PatientID=@ID",
                        New With {.ID = entry.PatientID})
                    If row IsNot Nothing Then
                        patientName = If(row.PatientName, "")
                        patientSex = If(row.PatientSex, Nothing)
                        patientPhone = If(row.PatientPhone, "").Trim()
                    End If
                End Using
            Catch
            End Try
            If String.IsNullOrWhiteSpace(patientPhone) Then Continue For

            Dim useEng = If(entry.MessageEnglish.HasValue, entry.MessageEnglish.Value, False)
            Dim msg As String = BuildAppointmentScheduleMessageForPatient(patientName, patientSex, useEng)
            If String.IsNullOrWhiteSpace(msg) Then Continue For

            Try
                Dim ctx = New WhatsAppSendContext With {
                    .Category = WhatsAppMessageCategories.AppointmentSchedule,
                    .PatientId = entry.PatientID,
                    .SourceHint = NameOf(ApptReminderScheduleService),
                    .DisplayName = patientName,
                    .IdempotencyKey = "SCHED|" & entry.PatientID.ToString(CultureInfo.InvariantCulture) & "|" & now.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
                }
                Await waServiceSend.SendMessageAsync(clinicId, patientPhone, msg, "", ctx)
                sentCount += 1
                Dim s = list.FirstOrDefault(Function(x) x.PatientID = entry.PatientID)
                If s IsNot Nothing Then s.LastSentAt = now
            Catch
                ' Ignore send errors; continue with next patient.
            End Try
        Next
        If sentCount > 0 Then SaveAll(list)
        Return sentCount
    End Function

    ''' <summary>Generic recurring appointment nudge: same clinic/title/salutation header as other WhatsApp patient messages.</summary>
    Private Shared Function BuildAppointmentScheduleMessageForPatient(patientName As String, patientSex As String, useEnglish As Boolean) As String
        Dim name = If(patientName, "").Trim()
        Dim title = If(useEnglish, "Appointment reminder", "تذكير بالمواعيد")
        Return WhatsHelper.BuildWhatsMessageWithStandardHeader(title, name, If(patientSex, Nothing), useEnglish,
            Sub(sb)
                If useEnglish Then
                    sb.AppendLine("This is a reminder about your appointments at the dental clinic.")
                    sb.AppendLine("Please contact the clinic for questions or to reschedule.")
                Else
                    sb.AppendLine("هذا تذكير بمواعيدك في عيادة الأسنان.")
                    sb.AppendLine("للاستفسار أو تعديل الموعد، يرجى التواصل مع العيادة.")
                End If
            End Sub)
    End Function

    Private Shared Sub SaveAll(entries As List(Of ScheduleEntry))
        Try
            Dim dir = Path.GetDirectoryName(_schedulePath)
            If Not String.IsNullOrEmpty(dir) Then Directory.CreateDirectory(dir)
            Dim lines = entries.Select(Function(e)
                                           Dim lastStr = If(e.LastSentAt.HasValue, e.LastSentAt.Value.ToString("yyyy-MM-dd HH:mm:ss"), "")
                                           Dim langStr = If(e.MessageEnglish.HasValue, If(e.MessageEnglish.Value, "1", "0"), "")
                                           Return $"{e.PatientID}|{e.IntervalDays}|{lastStr}|{langStr}"
                                       End Function).ToArray()
            File.WriteAllLines(_schedulePath, lines)
        Catch
        End Try
    End Sub

    Private Class PatientPhoneRow
        Public Property PatientName As String
        Public Property PatientSex As String
        Public Property PatientPhone As String
    End Class
End Class

