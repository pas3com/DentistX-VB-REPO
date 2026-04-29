Imports System.IO

Public Module CursorLicLogger
    Private ReadOnly LogFileName As String = "lic_guard.log"

    Public Sub Info(message As String)
        Write("INFO", message)
    End Sub

    Public Sub Warn(message As String)
        Write("WARN", message)
    End Sub

    Public Sub [Error](message As String)
        Write("ERROR", message)
    End Sub

    Private Sub Write(level As String, message As String)
        Try
            Dim dir = CursorLicConfig.GetDataDir()
            Directory.CreateDirectory(dir)
            Dim path1 = Path.Combine(dir, LogFileName)
            Dim line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}"
            File.AppendAllText(path1, line & Environment.NewLine)
        Catch
            ' Avoid crashing if logging fails.
        End Try
    End Sub
End Module
