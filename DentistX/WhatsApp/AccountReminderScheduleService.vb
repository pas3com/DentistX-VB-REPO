Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports Dapper

''' <summary>Manages recurring account reminders: which patients get reminders and how often.</summary>
''' <remarks>Storage: %LocalAppData%\DentistX\AccountReminderSchedule.txt, format: PatientID|IntervalDays|LastSentAt</remarks>
Public Class AccountReminderScheduleService
    Private Shared ReadOnly _schedulePath As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "DentistX", "AccountReminderSchedule.txt")

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
    Public Shared Async Function RunAsync() As Task(Of ReminderRunResult)
        Dim result As New ReminderRunResult()
        Dim clinicId As String = WhatsAppService.GetCurrentClinicId()
        If String.IsNullOrWhiteSpace(clinicId) Then Return result

        If Not Await WhatsAppService.TrySilentWhatsReconnectBackgroundAsync(clinicId).ConfigureAwait(False) Then Return result

        Dim schedule = GetAllScheduled()
        If schedule.Count = 0 Then Return result

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
        If toSend.Count = 0 Then Return result

        Dim list = GetAllScheduled()
        For Each entry In toSend
            Dim patientName = ""
            Dim patientPhone = ""
            Try
                Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                    Dim row = conn.QueryFirstOrDefault(Of PatientPhoneRow)(
                        "SELECT PatientName, ISNULL(NULLIF(RTRIM(WhatsApp), ''), RTRIM(Phone)) AS PatientPhone FROM Patient WHERE PatientID=@ID",
                        New With {.ID = entry.PatientID})
                    If row IsNot Nothing Then
                        patientName = If(row.PatientName, "")
                        patientPhone = If(row.PatientPhone, "").Trim()
                    End If
                End Using
            Catch
            End Try
            If String.IsNullOrWhiteSpace(patientPhone) Then Continue For

            Dim useEng = If(entry.MessageEnglish.HasValue, entry.MessageEnglish.Value, False)
            Dim ok = Await AccountingWhatsAppService.SendAccountForPatientAsync(entry.PatientID, patientName, patientPhone, useEng)
            If ok Then
                result.SentCount += 1
                Dim s = list.FirstOrDefault(Function(x) x.PatientID = entry.PatientID)
                If s IsNot Nothing Then s.LastSentAt = now
                Dim dispName = If(String.IsNullOrWhiteSpace(patientName), $"Patient #{entry.PatientID}", patientName.Trim())
                result.Lines.Add($"Account reminder · {dispName} (ID {entry.PatientID})")
            End If
        Next
        If result.SentCount > 0 Then SaveAll(list)
        Return result
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
        Public Property PatientPhone As String
    End Class
End Class
