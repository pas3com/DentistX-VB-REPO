Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Windows.Forms

''' <summary>
''' Diagnostic append-only log for scheduled scheduler snapshot sends (auto path).
''' Writes under Attachments\Logs\SchedulerSnapshotAutoSendDiag_yyyyMMdd.log so skipped polls,
''' deferrals, "already sent today", capture/WhatsApp failures are visible outside SQL recipient rows.
''' </summary>
Friend NotInheritable Class SchedulerSnapshotSnapLog
    Private Sub New()
    End Sub

    Private Shared ReadOnly SyncRoot As New Object()

    Public Shared Sub Write(category As String, detail As String)
        Dim ts = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)
        Dim c = If(category, "").Trim()
        Dim d = If(detail, "").Replace(vbCr, " ").Replace(vbLf, " ").Trim()
        Dim line = ts & "Z | " & c & " | " & d
        SyncLock SyncRoot
            Try
                Dim dir = IO.Path.Combine(Application.StartupPath, "Attachments", "Logs")
                Directory.CreateDirectory(dir)
                Dim path = IO.Path.Combine(dir, "SchedulerSnapshotAutoSendDiag_" & Date.UtcNow.ToString("yyyyMMdd", CultureInfo.InvariantCulture) & ".log")
                File.AppendAllText(path, line & vbCrLf, New UTF8Encoding(encoderShouldEmitUTF8Identifier:=False))
            Catch
            End Try
        End SyncLock
    End Sub
End Class
