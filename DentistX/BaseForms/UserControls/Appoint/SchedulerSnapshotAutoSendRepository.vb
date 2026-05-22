Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Linq
Imports Dapper

''' <summary>Persistence for scheduled scheduler snapshot send jobs (header, recipients, run log).</summary>
Public NotInheritable Class SchedulerSnapshotAutoSendRepository

    ''' <summary>0=PNG only, 1=HTML only, 2=PNG and HTML (four WhatsApp messages: two weeks × formats).</summary>
    Public Enum SendContentMode As Byte
        PngOnly = 0
        HtmlOnly = 1
        PngAndHtml = 2
    End Enum

    Public Const SourceDoctor As String = "Doctor"
    Public Const SourceEmployee As String = "Employee"
    Public Const SourceSecretary As String = "Secretary"
    Public Const SourceContact As String = "Contact"

    Public Const LogStatusPending As String = "Pending"
    Public Const LogStatusSent As String = "Sent"
    Public Const LogStatusFailed As String = "Failed"
    Public Const LogStatusSkippedNoWhats As String = "SkippedNoWhatsApp"

    Private Shared Function Cn() As SqlConnection
        Return New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
    End Function

    Public Shared Function GetJobs() As IList(Of SchedulerSnapshotAutoSendJobRow)
        Const sql = "
SELECT j.JobId, j.IsEnabled, j.SendTimeLocal, j.SendTimeLocal2, j.DaysOfWeekMask, j.MessageCaption, j.Notes, j.SendContentMode, j.CreatedAt, j.UpdatedAt,
       (SELECT COUNT(*) FROM dbo.SchedulerSnapshotAutoSendRecipient r WHERE r.JobId = j.JobId AND r.IsActive = 1) AS ActiveRecipientCount
FROM dbo.SchedulerSnapshotAutoSendJob j
ORDER BY j.JobId"
        Using cn1 = Cn()
            Return cn1.Query(Of SchedulerSnapshotAutoSendJobRow)(sql).ToList()
        End Using
    End Function

    Public Shared Function GetJob(jobId As Integer) As SchedulerSnapshotAutoSendJobRow
        Const sql = "
SELECT j.JobId, j.IsEnabled, j.SendTimeLocal, j.SendTimeLocal2, j.DaysOfWeekMask, j.MessageCaption, j.Notes, j.SendContentMode, j.CreatedAt, j.UpdatedAt,
       (SELECT COUNT(*) FROM dbo.SchedulerSnapshotAutoSendRecipient r WHERE r.JobId = j.JobId AND r.IsActive = 1) AS ActiveRecipientCount
FROM dbo.SchedulerSnapshotAutoSendJob j
WHERE j.JobId = @id"
        Using cn1 = Cn()
            Return cn1.QuerySingleOrDefault(Of SchedulerSnapshotAutoSendJobRow)(sql, New With {.id = jobId})
        End Using
    End Function

    Public Shared Function InsertJob(row As SchedulerSnapshotAutoSendJobRow) As Integer
        Const sql = "
INSERT INTO dbo.SchedulerSnapshotAutoSendJob (IsEnabled, SendTimeLocal, SendTimeLocal2, DaysOfWeekMask, MessageCaption, Notes, SendContentMode, UpdatedAt)
VALUES (@IsEnabled, @SendTimeLocal, @SendTimeLocal2, @DaysOfWeekMask, @MessageCaption, @Notes, @SendContentMode, SYSUTCDATETIME());
SELECT CAST(SCOPE_IDENTITY() AS INT);"
        Using cn1 = Cn()
            Return cn1.ExecuteScalar(Of Integer)(sql, row)
        End Using
    End Function

    Public Shared Sub UpdateJob(row As SchedulerSnapshotAutoSendJobRow)
        Const sql = "
UPDATE dbo.SchedulerSnapshotAutoSendJob
SET IsEnabled = @IsEnabled, SendTimeLocal = @SendTimeLocal, SendTimeLocal2 = @SendTimeLocal2, DaysOfWeekMask = @DaysOfWeekMask,
    MessageCaption = @MessageCaption, Notes = @Notes, SendContentMode = @SendContentMode, UpdatedAt = SYSUTCDATETIME()
WHERE JobId = @JobId"
        Using cn1 = Cn()
            cn1.Execute(sql, row)
        End Using
    End Sub

    Public Shared Sub DeleteJob(jobId As Integer)
        Using cn1 = Cn()
            cn1.Execute("DELETE FROM dbo.SchedulerSnapshotAutoSendJob WHERE JobId = @id", New With {.id = jobId})
        End Using
    End Sub

    Public Shared Function GetRecipients(jobId As Integer) As IList(Of SchedulerSnapshotAutoSendRecipientRow)
        Const sql = "
SELECT RecipientId, JobId, SourceType, SourcePk, DisplayName, WhatsAppPrefix, WhatsAppLocal, IsActive, CreatedAt
FROM dbo.SchedulerSnapshotAutoSendRecipient
WHERE JobId = @id
ORDER BY SourceType, DisplayName"
        Using cn1 = Cn()
            Return cn1.Query(Of SchedulerSnapshotAutoSendRecipientRow)(sql, New With {.id = jobId}).ToList()
        End Using
    End Function

    Public Shared Function InsertRecipient(r As SchedulerSnapshotAutoSendRecipientRow) As Long
        Const sql = "
INSERT INTO dbo.SchedulerSnapshotAutoSendRecipient (JobId, SourceType, SourcePk, DisplayName, WhatsAppPrefix, WhatsAppLocal, IsActive)
VALUES (@JobId, @SourceType, @SourcePk, @DisplayName, @WhatsAppPrefix, @WhatsAppLocal, @IsActive);
SELECT CAST(SCOPE_IDENTITY() AS BIGINT);"
        Using cn1 = Cn()
            Return cn1.ExecuteScalar(Of Long)(sql, r)
        End Using
    End Function

    Public Shared Sub UpdateRecipient(r As SchedulerSnapshotAutoSendRecipientRow)
        Const sql = "
UPDATE dbo.SchedulerSnapshotAutoSendRecipient
SET DisplayName = @DisplayName, WhatsAppPrefix = @WhatsAppPrefix, WhatsAppLocal = @WhatsAppLocal, IsActive = @IsActive
WHERE RecipientId = @RecipientId"
        Using cn1 = Cn()
            cn1.Execute(sql, r)
        End Using
    End Sub

    Public Shared Sub DeleteRecipient(recipientId As Long)
        Using cn1 = Cn()
            cn1.Execute("DELETE FROM dbo.SchedulerSnapshotAutoSendRecipient WHERE RecipientId = @id", New With {.id = recipientId})
        End Using
    End Sub

    ''' <summary>Called after each send attempt. When <paramref name="excludeFromDedupe"/> is True (manual test send success),
    ''' <see cref="HasRecipientSentOnRunDate"/> ignores the row for that send slot so automatic send can still run the same calendar day.</summary>
    ''' <param name="sendSlot">1 = first scheduled time, 2 = second (dedupe per slot per calendar day).</param>
    Public Shared Sub InsertLogEntry(jobId As Integer, recipientId As Long?, runDate As Date, status As String,
                                     startedAt As DateTime, completedAt As DateTime?, errorMessage As String, mediaPath As String,
                                     Optional excludeFromDedupe As Boolean = False,
                                     Optional sendSlot As Byte = 1)
        Const sql = "
INSERT INTO dbo.SchedulerSnapshotAutoSendLog (JobId, RecipientId, RunDate, SendSlot, Status, StartedAt, CompletedAt, ErrorMessage, MediaPath, ExcludeFromDedupe)
VALUES (@JobId, @RecipientId, @RunDate, @SendSlot, @Status, @StartedAt, @CompletedAt, @ErrorMessage, @MediaPath, @ExcludeFromDedupe)"
        Using cn1 = Cn()
            cn1.Execute(sql, New With {
                .JobId = jobId,
                .RecipientId = recipientId,
                .RunDate = runDate,
                .SendSlot = sendSlot,
                .Status = status,
                .StartedAt = startedAt,
                .CompletedAt = completedAt,
                .ErrorMessage = errorMessage,
                .MediaPath = mediaPath,
                .ExcludeFromDedupe = excludeFromDedupe
            })
        End Using
    End Sub

    Public Shared Function GetRecentLogs(jobId As Integer, take As Integer) As IList(Of SchedulerSnapshotAutoSendLogRow)
        If take < 1 Then take = 100
        If take > 2000 Then take = 2000
        Const sql = "
SELECT TOP (@take) LogId, JobId, RecipientId, RunDate, SendSlot, Status, StartedAt, CompletedAt, ErrorMessage, MediaPath, ExcludeFromDedupe
FROM dbo.SchedulerSnapshotAutoSendLog
WHERE JobId = @id
ORDER BY StartedAt DESC"
        Using cn1 = Cn()
            Return cn1.Query(Of SchedulerSnapshotAutoSendLogRow)(sql, New With {.id = jobId, .take = take}).ToList()
        End Using
    End Function

    ''' <summary>True if this recipient already has a successful send for this job slot on the given local calendar day.</summary>
    Public Shared Function HasRecipientSentOnRunDate(jobId As Integer, recipientId As Long, runDate As Date, sendSlot As Byte) As Boolean
        Const sql = "
SELECT COUNT(1) FROM dbo.SchedulerSnapshotAutoSendLog
WHERE JobId = @jobId AND RecipientId = @recipientId AND RunDate = @runDate AND Status = @sent
  AND ISNULL(ExcludeFromDedupe, 0) = 0 AND ISNULL(SendSlot, 1) = @sendSlot"
        Using cn1 = Cn()
            Dim n = cn1.ExecuteScalar(Of Integer)(sql, New With {
                .jobId = jobId,
                .recipientId = recipientId,
                .runDate = runDate.Date,
                .sent = LogStatusSent,
                .sendSlot = sendSlot
            })
            Return n > 0
        End Using
    End Function

    ''' <summary>Rows that count toward daily auto-send dedupe (Sent and not excluded).</summary>
    Public Shared Function CountDedupeBlockingSentRows(jobId As Integer, runDate As Date) As Integer
        Const sql = "
SELECT COUNT(1) FROM dbo.SchedulerSnapshotAutoSendLog
WHERE JobId = @jobId AND RunDate = @runDate AND Status = @sent AND ISNULL(ExcludeFromDedupe, 0) = 0"
        Using cn1 = Cn()
            Return cn1.ExecuteScalar(Of Integer)(sql, New With {
                .jobId = jobId,
                .runDate = runDate.Date,
                .sent = LogStatusSent
            })
        End Using
    End Function

    ''' <summary>Successful manual test sends for the day (excluded from dedupe).</summary>
    Public Shared Function CountSentExcludedFromDedupe(jobId As Integer, runDate As Date) As Integer
        Const sql = "
SELECT COUNT(1) FROM dbo.SchedulerSnapshotAutoSendLog
WHERE JobId = @jobId AND RunDate = @runDate AND Status = @sent AND ISNULL(ExcludeFromDedupe, 0) <> 0"
        Using cn1 = Cn()
            Return cn1.ExecuteScalar(Of Integer)(sql, New With {
                .jobId = jobId,
                .runDate = runDate.Date,
                .sent = LogStatusSent
            })
        End Using
    End Function

    ''' <summary>Maps .NET DayOfWeek (Sunday=0) to Mon=1 .. Sun=64 bitmask bit position value.</summary>
    Public Shared Function DayOfWeekToBit(d As DayOfWeek) As Integer
        Select Case d
            Case DayOfWeek.Monday
                Return 1
            Case DayOfWeek.Tuesday
                Return 2
            Case DayOfWeek.Wednesday
                Return 4
            Case DayOfWeek.Thursday
                Return 8
            Case DayOfWeek.Friday
                Return 16
            Case DayOfWeek.Saturday
                Return 32
            Case Else
                Return 64
        End Select
    End Function

    Public Shared Function FormatDaysMask(mask As Integer, english As Boolean) As String
        If mask <= 0 Then Return If(english, "(none)", "(لا شيء)")
        Dim names = If(english,
            New String() {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"},
            New String() {"إثنين", "ثلاثاء", "أربعاء", "خميس", "جمعة", "سبت", "أحد"})
        Dim bits = {1, 2, 4, 8, 16, 32, 64}
        Dim parts As New List(Of String)()
        For i = 0 To 6
            If (mask And bits(i)) <> 0 Then parts.Add(names(i))
        Next
        Return String.Join(", ", parts)
    End Function

End Class

Public Class SchedulerSnapshotAutoSendJobRow
    Public Property JobId As Integer
    Public Property IsEnabled As Boolean
    Public Property SendTimeLocal As TimeSpan
    Public Property SendTimeLocal2 As TimeSpan?
    Public Property DaysOfWeekMask As Byte
    Public Property MessageCaption As String
    Public Property Notes As String
    Public Property SendContentMode As Byte
    Public Property CreatedAt As DateTime
    Public Property UpdatedAt As DateTime?
    Public Property ActiveRecipientCount As Integer
End Class

Public Class SchedulerSnapshotAutoSendRecipientRow
    Public Property RecipientId As Long
    Public Property JobId As Integer
    Public Property SourceType As String
    Public Property SourcePk As Integer
    Public Property DisplayName As String
    Public Property WhatsAppPrefix As String
    Public Property WhatsAppLocal As String
    Public Property IsActive As Boolean
    Public Property CreatedAt As DateTime
End Class

Public Class SchedulerSnapshotAutoSendLogRow
    Public Property LogId As Long
    Public Property JobId As Integer
    Public Property RecipientId As Long?
    Public Property RunDate As Date
    Public Property SendSlot As Byte
    Public Property Status As String
    Public Property StartedAt As DateTime
    Public Property CompletedAt As DateTime?
    Public Property ErrorMessage As String
    Public Property MediaPath As String
    ''' <summary>True when row was from manual test send — does not satisfy daily auto-send dedupe.</summary>
    Public Property ExcludeFromDedupe As Boolean
End Class
