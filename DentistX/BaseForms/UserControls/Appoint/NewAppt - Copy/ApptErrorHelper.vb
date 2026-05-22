Imports System
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Windows.Forms

Friend Module ApptErrorHelper

    Friend Sub Report(ex As Exception,
                      context As String,
                      Optional showUser As Boolean = True,
                      Optional owner As IWin32Window = Nothing,
                      Optional englishMessage As String = Nothing,
                      Optional arabicMessage As String = Nothing,
                      Optional englishTitle As String = "Error",
                      Optional arabicTitle As String = "خطأ")
        If ex Is Nothing Then Return

        Try
            TryWriteErrorReport(context, ex)
            Trace.WriteLine($"[{Date.Now:yyyy-MM-dd HH:mm:ss}] {context}: {ex}")
            Debug.WriteLine($"[{Date.Now:yyyy-MM-dd HH:mm:ss}] {context}: {ex}")
        Catch
        End Try

        If Not showUser Then Return

        Dim message = BuildUserMessage(ex, englishMessage, arabicMessage)
        Dim title = If(Eng, englishTitle, arabicTitle)

        If owner IsNot Nothing Then
            MessageBox.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Friend Function TryRun(action As Action,
                           context As String,
                           Optional showUser As Boolean = True,
                           Optional owner As IWin32Window = Nothing,
                           Optional englishMessage As String = Nothing,
                           Optional arabicMessage As String = Nothing,
                           Optional englishTitle As String = "Error",
                           Optional arabicTitle As String = "خطأ") As Boolean
        If action Is Nothing Then Return False
        Try
            action()
            Return True
        Catch ex As Exception
            Report(ex, context, showUser, owner, englishMessage, arabicMessage, englishTitle, arabicTitle)
            Return False
        End Try
    End Function

    Friend Sub SafeInvalidate(control As Control,
                              context As String,
                              Optional invalidateChildren As Boolean = False)
        If control Is Nothing OrElse control.IsDisposed Then Return
        Try
            If invalidateChildren Then
                control.Invalidate(True)
            Else
                control.Invalidate()
            End If
        Catch ex As Exception
            Report(ex, context, showUser:=False)
        End Try
    End Sub

    Friend Sub SafeRefresh(control As Control, context As String)
        If control Is Nothing OrElse control.IsDisposed Then Return
        Try
            control.Refresh()
        Catch ex As Exception
            Report(ex, context, showUser:=False)
        End Try
    End Sub

    Friend Sub SafeFocus(control As Control, context As String)
        If control Is Nothing OrElse control.IsDisposed Then Return
        Try
            control.Focus()
        Catch ex As Exception
            Report(ex, context, showUser:=False)
        End Try
    End Sub

    Friend Sub ReportDiagnostic(context As String, message As String)
        If String.IsNullOrWhiteSpace(context) AndAlso String.IsNullOrWhiteSpace(message) Then Return
        Try
            TryWriteDiagnosticReport(context, message)
            Dim line = $"[{Date.Now:yyyy-MM-dd HH:mm:ss}] {context}: {message}"
            Trace.WriteLine(line)
            Debug.WriteLine(line)
        Catch
        End Try
    End Sub

    Private Function BuildUserMessage(ex As Exception, englishMessage As String, arabicMessage As String) As String
        Dim prefix = If(Eng, englishMessage, arabicMessage)
        If String.IsNullOrWhiteSpace(prefix) Then
            Return ex.Message
        End If
        Return prefix.Trim() & Environment.NewLine & ex.Message
    End Function

    Private Function GetErrorLogDirectory() As String
        Dim appBase As String = AppDomain.CurrentDomain.BaseDirectory
        If String.IsNullOrEmpty(appBase) Then appBase = Environment.CurrentDirectory
        appBase = appBase.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
        Dim primary = Path.Combine(appBase, "crash_logs")
        If TryCreateLogDir(primary) Then Return primary
        Dim fallback = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DentistX", "crash_logs")
        If TryCreateLogDir(fallback) Then Return fallback
        Return primary
    End Function

    Private Function TryCreateLogDir(dir As String) As Boolean
        Try
            Directory.CreateDirectory(dir)
            Dim probe = Path.Combine(dir, ".dentistx_error_probe_" & Guid.NewGuid().ToString("N") & ".tmp")
            File.WriteAllText(probe, "x", Encoding.UTF8)
            File.Delete(probe)
            Return True
        Catch
            Return False
        End Try
    End Function

    Private Sub TryWriteErrorReport(context As String, ex As Exception)
        If ex Is Nothing Then Return

        Try
            Dim dir = GetErrorLogDirectory()
            Dim filePath = Path.Combine(dir, $"error_{DateTime.Now:yyyyMMdd_HHmmss_fff}.txt")
            Dim sb As New StringBuilder()
            sb.AppendLine("=== DentistX non-fatal error log ===")
            sb.AppendLine("Context: " & If(context, ""))
            sb.AppendLine("Utc: " & DateTime.UtcNow.ToString("u", CultureInfo.InvariantCulture))
            sb.AppendLine("Local: " & DateTime.Now.ToString("u", CultureInfo.InvariantCulture))
            sb.AppendLine("Machine: " & Environment.MachineName)
            sb.AppendLine("User: " & Environment.UserDomainName & "\" & Environment.UserName)
            sb.AppendLine("OS: " & Environment.OSVersion.ToString())
            sb.AppendLine("64-bit OS: " & Environment.Is64BitOperatingSystem.ToString() & ", 64-bit process: " & Environment.Is64BitProcess.ToString())
            sb.AppendLine("CLR: " & Environment.Version.ToString())
            Try
                sb.AppendLine("Process: " & Process.GetCurrentProcess().MainModule.FileName)
            Catch
                sb.AppendLine("Process: (unavailable)")
            End Try
            sb.AppendLine("App base directory: " & AppDomain.CurrentDomain.BaseDirectory)
            sb.AppendLine("Working directory: " & Environment.CurrentDirectory)
            sb.AppendLine("Crash log directory: " & dir)
            sb.AppendLine()
            AppendExceptionDetail(sb, ex)
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8)
        Catch
        End Try
    End Sub

    Private Sub TryWriteDiagnosticReport(context As String, message As String)
        Try
            Dim dir = GetErrorLogDirectory()
            Dim filePath = Path.Combine(dir, $"diag_{DateTime.Now:yyyyMMdd_HHmmss_fff}.txt")
            Dim sb As New StringBuilder()
            sb.AppendLine("=== DentistX diagnostic log ===")
            sb.AppendLine("Context: " & If(context, ""))
            sb.AppendLine("Utc: " & DateTime.UtcNow.ToString("u", CultureInfo.InvariantCulture))
            sb.AppendLine("Local: " & DateTime.Now.ToString("u", CultureInfo.InvariantCulture))
            sb.AppendLine("Machine: " & Environment.MachineName)
            sb.AppendLine("User: " & Environment.UserDomainName & "\" & Environment.UserName)
            sb.AppendLine("OS: " & Environment.OSVersion.ToString())
            sb.AppendLine("64-bit OS: " & Environment.Is64BitOperatingSystem.ToString() & ", 64-bit process: " & Environment.Is64BitProcess.ToString())
            sb.AppendLine("CLR: " & Environment.Version.ToString())
            sb.AppendLine("App base directory: " & AppDomain.CurrentDomain.BaseDirectory)
            sb.AppendLine("Working directory: " & Environment.CurrentDirectory)
            sb.AppendLine("Crash log directory: " & dir)
            sb.AppendLine()
            sb.AppendLine("--- Message ---")
            sb.AppendLine(If(message, ""))
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8)
        Catch
        End Try
    End Sub

    Private Sub AppendExceptionDetail(sb As StringBuilder, ex As Exception)
        sb.AppendLine("--- Exception detail ---")
        Dim depth = 0
        Dim current = ex
        While current IsNot Nothing AndAlso depth < 32
            sb.AppendLine("--- Level " & depth.ToString(CultureInfo.InvariantCulture) & " ---")
            sb.AppendLine("Type: " & current.GetType().FullName)
            sb.AppendLine("Message: " & current.Message)
            If current.TargetSite IsNot Nothing Then
                Dim declaringType = current.TargetSite.DeclaringType
                sb.AppendLine("TargetSite: " & If(declaringType Is Nothing, "?", declaringType.FullName) & "." & current.TargetSite.Name)
            End If
            If Not String.IsNullOrEmpty(current.Source) Then
                sb.AppendLine("Source: " & current.Source)
            End If
            If Not String.IsNullOrEmpty(current.StackTrace) Then
                sb.AppendLine("Stack:")
                sb.AppendLine(current.StackTrace)
            End If
            current = current.InnerException
            depth += 1
        End While
        sb.AppendLine()
        sb.AppendLine("--- Full ToString() (includes inners) ---")
        sb.AppendLine(ex.ToString())
    End Sub
End Module
