Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Configuration
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Infralution.Localization

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Const CrashReportWhatsRecipient As String = "972599392764"
        Private Shared ReadOnly CrashWhatsSendLock As New Object()

        ''' <summary>Under the application directory (same folder as the exe). Fallback to LocalAppData if not writable (e.g. Program Files).</summary>
        Private Shared Function GetCrashLogDirectory() As String
            Dim appBase As String = AppDomain.CurrentDomain.BaseDirectory
            If String.IsNullOrEmpty(appBase) Then appBase = Environment.CurrentDirectory
            appBase = appBase.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            Dim primary = Path.Combine(appBase, "crash_logs")
            If TryCreateCrashDir(primary) Then Return primary
            Dim fallback = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DentistX", "crash_logs")
            If TryCreateCrashDir(fallback) Then Return fallback
            Return primary
        End Function

        ''' <summary>Primary and LocalAppData crash log folders (may not exist). Same layout as <see cref="GetCrashLogDirectory"/>.</summary>
        Private Shared Function GetCrashLogSearchRoots() As String()
            Dim appBase As String = AppDomain.CurrentDomain.BaseDirectory
            If String.IsNullOrEmpty(appBase) Then appBase = Environment.CurrentDirectory
            appBase = appBase.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            Dim primary = Path.Combine(appBase, "crash_logs")
            Dim fallback = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DentistX", "crash_logs")
            If String.Equals(primary, fallback, StringComparison.OrdinalIgnoreCase) Then Return New String() {primary}
            Return New String() {primary, fallback}
        End Function

        ''' <summary>Newest <c>crash_*.txt</c> by last write time (UTC), scanning all known crash folders.</summary>
        Private Shared Function FindNewestCrashReportPath() As String
            Dim bestPath As String = Nothing
            Dim bestTicks As Long = -1
            For Each root In GetCrashLogSearchRoots()
                If Not Directory.Exists(root) Then Continue For
                Dim paths As String() = Nothing
                Try
                    paths = Directory.GetFiles(root, "crash_*.txt")
                Catch
                    Continue For
                End Try
                For Each fp In paths
                    Dim ticks As Long
                    Try
                        ticks = File.GetLastWriteTimeUtc(fp).Ticks
                    Catch
                        Continue For
                    End Try
                    If ticks > bestTicks Then
                        bestTicks = ticks
                        bestPath = fp
                    End If
                Next
            Next
            Return bestPath
        End Function

        Friend Shared Function BuildCrashReportSentSignature(fullPath As String) As String
            Try
                Return fullPath & "|" & File.GetLastWriteTimeUtc(fullPath).Ticks.ToString(CultureInfo.InvariantCulture)
            Catch
                Return Nothing
            End Try
        End Function

        Friend Shared Sub TryPersistLastCrashWhatsReportSignature(signature As String)
            If String.IsNullOrWhiteSpace(signature) Then Return
            SyncLock CrashWhatsSendLock
                My.Settings.LastCrashWhatsReportSignature = signature
                My.Settings.Save()
            End SyncLock
        End Sub

        Friend Shared Sub TryPersistCrashSignatureAfterOutboundSend(fullAttachmentPath As String)
            TryPersistLastCrashWhatsReportSignature(BuildCrashReportSentSignature(fullAttachmentPath))
        End Sub

        ''' <summary>Runs on a thread-pool worker. Uses synchronous wait instead of Async in this partial class to avoid known vbc crashes.</summary>
        Private Shared Sub TrySendLatestCrashReportViaWhatsApp()
            Dim newest = FindNewestCrashReportPath()
            If String.IsNullOrEmpty(newest) Then Return
            Dim sig = BuildCrashReportSentSignature(newest)
            If String.IsNullOrEmpty(sig) Then Return

            Dim already As String = Nothing
            SyncLock CrashWhatsSendLock
                already = My.Settings.LastCrashWhatsReportSignature
            End SyncLock
            If String.Equals(already, sig, StringComparison.Ordinal) Then Return

            Dim clinicId As String = Nothing
            Try
                clinicId = WhatsAppService.GetCurrentClinicId()
            Catch
                Return
            End Try
            If String.IsNullOrWhiteSpace(clinicId) Then Return

            Dim wa As New WhatsAppService()
            Dim connected As Boolean = False
            Try
                connected = WhatsAppService.TrySilentWhatsReconnectBackgroundAsync(clinicId).ConfigureAwait(False).GetAwaiter().GetResult()
            Catch
                Return
            End Try
            If Not connected Then Return

            Dim caption = "DentistX crash report — " & Environment.MachineName & " — " & Path.GetFileName(newest)
            Dim idem = WhatsAppOutboundRepository.BuildCrashReportOutboundIdempotencyKey(newest)
            Dim ctx As New WhatsAppSendContext With {
                .Category = WhatsAppMessageCategories.CrashReport,
                .SourceHint = "CrashReportStartup",
                .RevealMessageCenter = False,
                .SuppressUiFeedback = True,
                .IdempotencyKey = idem
            }
            Try
                Dim resp = wa.SendMessageAsync(clinicId, CrashReportWhatsRecipient, caption, newest, ctx).ConfigureAwait(False).GetAwaiter().GetResult()
                Dim intr As WhatsAppOutboundSendInterpretation = Nothing
                If WhatsAppService.TryInterpretOutboundSendResponse(resp, intr) Then
                    If Not String.IsNullOrWhiteSpace(intr.TerminalPriorStatus) Then
                        If String.Equals(intr.TerminalPriorStatus, WhatsAppOutboundStatuses.Sent, StringComparison.OrdinalIgnoreCase) Then
                            TryPersistLastCrashWhatsReportSignature(sig)
                        End If
                        Return
                    End If
                    Return
                End If
                TryPersistLastCrashWhatsReportSignature(sig)
            Catch
                Return
            End Try
        End Sub

        Private Sub QueueLatestCrashReportWhatsUploadIfNeeded()
            Task.Run(Sub()
                         Try
                             TrySendLatestCrashReportViaWhatsApp()
                         Catch
                         End Try
                     End Sub)
        End Sub

        Private Shared Function TryCreateCrashDir(dir As String) As Boolean
            Try
                Directory.CreateDirectory(dir)
                Dim probe = Path.Combine(dir, ".dentistx_crash_probe_" & Guid.NewGuid().ToString("N") & ".tmp")
                File.WriteAllText(probe, "x", Encoding.UTF8)
                File.Delete(probe)
                Return True
            Catch
                Return False
            End Try
        End Function

        Private Shared Function ClipText(s As String, maxLen As Integer) As String
            If String.IsNullOrEmpty(s) Then Return ""
            If s.Length <= maxLen Then Return s
            Return s.Substring(0, maxLen) & "…"
        End Function

        ''' <summary>Best-effort: active form, focused control, and parent chain (WinForms).</summary>
        Private Shared Sub AppendUiFocusContext(sb As StringBuilder)
            sb.AppendLine("--- UI context (best effort) ---")
            Try
                Dim curThread = Thread.CurrentThread
                sb.AppendLine("Thread: ManagedId=" & curThread.ManagedThreadId.ToString() & ", Name=" & If(String.IsNullOrEmpty(curThread.Name), "(default)", curThread.Name) & ", UI=" & curThread.GetApartmentState().ToString())
                Dim af = Form.ActiveForm
                If af Is Nothing Then
                    sb.AppendLine("ActiveForm: (none — modal dialog, splash, or focus outside app)")
                Else
                    sb.AppendLine("ActiveForm: " & af.GetType().FullName & " | Text=" & ClipText(af.Text, 200))
                    Dim leaf As Control = af.ActiveControl
                    If leaf Is Nothing Then
                        sb.AppendLine("ActiveForm.ActiveControl: (none)")
                    Else
                        sb.AppendLine("Focused control (leaf): " & DescribeControl(leaf))
                        Dim walk As Control = leaf.Parent
                        Dim depth = 0
                        While walk IsNot Nothing AndAlso depth < 24
                            sb.AppendLine("  Parent[" & depth.ToString() & "]: " & DescribeControl(walk))
                            walk = walk.Parent
                            depth += 1
                        End While
                    End If
                End If
            Catch ex As Exception
                sb.AppendLine("(UI context error: " & ex.Message & ")")
            End Try
            sb.AppendLine()
        End Sub

        Private Shared Function DescribeControl(c As Control) As String
            If c Is Nothing Then Return "(null)"
            Dim typeName = c.GetType().FullName
            Dim nm = If(String.IsNullOrEmpty(c.Name), "(unnamed)", c.Name)
            Dim txt = ""
            Try
                txt = "; Text=" & ClipText(Convert.ToString(c.Text), 120)
            Catch
            End Try
            Return typeName & " | Name=" & nm & txt
        End Function

#Region "Last opened form / user control (crash report)"

        Private Shared ReadOnly LastOpenedLock As New Object()
        Private Shared _lastFormSummary As String
        Private Shared _lastFormAtUtc As String
        Private Shared _lastUserControlSummary As String
        Private Shared _lastUserControlAtUtc As String
        Private Shared ReadOnly TrackedFormsLock As New Object()
        Private Shared ReadOnly TrackedForms As New HashSet(Of Form)()
        Private Shared ReadOnly WiredUserControls As New HashSet(Of UserControl)()
        Private Shared _lastDevExpressPopupSweepUtc As DateTime = DateTime.MinValue
        Private Shared ReadOnly DevExpressPopupSweepInterval As TimeSpan = TimeSpan.FromSeconds(20)

        Private Shared Function SummarizeForm(f As Form) As String
            If f Is Nothing Then Return "(null)"
            Return f.GetType().FullName & " | Text=" & ClipText(If(f.Text, String.Empty), 200)
        End Function

        Private Shared Function SummarizeUserControl(uc As UserControl) As String
            If uc Is Nothing Then Return "(null)"
            Dim parentF = TryCast(uc.FindForm(), Form)
            Dim p = If(parentF Is Nothing, "?", parentF.GetType().FullName & " / " & ClipText(If(parentF.Text, String.Empty), 120))
            Return uc.GetType().FullName & " | Name=" & If(String.IsNullOrEmpty(uc.Name), "(unnamed)", uc.Name) & " | ParentForm=" & p
        End Function

        Private Shared Sub RecordLastOpenedForm(f As Form)
            If f Is Nothing OrElse f.IsDisposed Then Return
            SyncLock LastOpenedLock
                _lastFormSummary = SummarizeForm(f)
                _lastFormAtUtc = DateTime.UtcNow.ToString("u", CultureInfo.InvariantCulture)
            End SyncLock
        End Sub

        Private Shared Sub RecordLastVisibleUserControl(uc As UserControl)
            If uc Is Nothing OrElse uc.IsDisposed Then Return
            SyncLock LastOpenedLock
                _lastUserControlSummary = SummarizeUserControl(uc)
                _lastUserControlAtUtc = DateTime.UtcNow.ToString("u", CultureInfo.InvariantCulture)
            End SyncLock
        End Sub

        Private Shared Sub OnTrackedFormShown(sender As Object, e As EventArgs)
            RecordLastOpenedForm(TryCast(sender, Form))
        End Sub

        Private Shared Sub OnUserControlVisibleChanged(sender As Object, e As EventArgs)
            Dim uc = TryCast(sender, UserControl)
            If uc Is Nothing OrElse uc.IsDisposed OrElse Not uc.Visible Then Return
            RecordLastVisibleUserControl(uc)
        End Sub

        Private Shared Sub OnTrackedFormClosed(sender As Object, e As FormClosedEventArgs)
            Dim f = TryCast(sender, Form)
            If f Is Nothing Then Return
            Try
                RemoveHandler f.Shown, AddressOf OnTrackedFormShown
            Catch
            End Try
            Try
                RemoveHandler f.FormClosed, AddressOf OnTrackedFormClosed
            Catch
            End Try
            Try
                RemoveHandler f.ControlAdded, AddressOf OnSubtreeControlAdded
            Catch
            End Try
            UnwireFormSubtree(f)
            SyncLock TrackedFormsLock
                TrackedForms.Remove(f)
            End SyncLock
        End Sub

        Private Shared Sub TryWireUserControlVisible(uc As UserControl)
            If uc Is Nothing OrElse Not WiredUserControls.Add(uc) Then Return
            AddHandler uc.VisibleChanged, AddressOf OnUserControlVisibleChanged
            If uc.Visible Then
                RecordLastVisibleUserControl(uc)
            End If
        End Sub

        Private Shared Sub WalkControlsAndWire(root As Control)
            If root Is Nothing Then Return
            Dim uc = TryCast(root, UserControl)
            If uc IsNot Nothing Then
                TryWireUserControlVisible(uc)
            End If
            For Each c As Control In root.Controls
                WalkControlsAndWire(c)
            Next
        End Sub

        Private Shared Sub OnSubtreeControlAdded(sender As Object, e As ControlEventArgs)
            If e Is Nothing OrElse e.Control Is Nothing Then Return
            WalkControlsAndWire(e.Control)
        End Sub

        Private Shared Sub UnwireFormSubtree(ctl As Control)
            If ctl Is Nothing Then Return
            Dim uc = TryCast(ctl, UserControl)
            If uc IsNot Nothing Then
                If WiredUserControls.Remove(uc) Then
                    Try
                        RemoveHandler uc.VisibleChanged, AddressOf OnUserControlVisibleChanged
                    Catch
                    End Try
                End If
            End If
            For Each c As Control In ctl.Controls
                UnwireFormSubtree(c)
            Next
        End Sub

        Private Shared Sub AttachFormTracking(f As Form)
            If f Is Nothing Then Return
            AddHandler f.Shown, AddressOf OnTrackedFormShown
            AddHandler f.FormClosed, AddressOf OnTrackedFormClosed
            AddHandler f.ControlAdded, AddressOf OnSubtreeControlAdded
            If f.IsHandleCreated AndAlso f.Visible Then
                RecordLastOpenedForm(f)
            End If
            WalkControlsAndWire(f)
        End Sub

        Private Shared Sub OnIdleTrackLastOpenedUi(sender As Object, e As EventArgs)
            Try
                For Each f As Form In System.Windows.Forms.Application.OpenForms
                    If f Is Nothing OrElse f.IsDisposed Then Continue For
                    Dim isNew As Boolean = False
                    SyncLock TrackedFormsLock
                        isNew = TrackedForms.Add(f)
                    End SyncLock
                    If isNew Then
                        AttachFormTracking(f)
                    End If
                Next
                TryReclaimIdleDevExpressPopups()
            Catch
            End Try
        End Sub

        ''' <summary>Closes hidden DevExpress editor/tooltip shells left in <see cref="Application.OpenForms"/> so USER handle quota is not exhausted (Win32 1158).</summary>
        Private Shared Sub TryReclaimIdleDevExpressPopups()
            Dim nowUtc = DateTime.UtcNow
            If nowUtc - _lastDevExpressPopupSweepUtc < DevExpressPopupSweepInterval Then Return
            _lastDevExpressPopupSweepUtc = nowUtc

            Dim active = Form.ActiveForm
            Dim main = TryCast(System.Windows.Forms.Application.OpenForms.Cast(Of Form)().FirstOrDefault(Function(f) TypeOf f Is MainView3), Form)

            For Each f As Form In System.Windows.Forms.Application.OpenForms
                If f Is Nothing OrElse f.IsDisposed Then Continue For
                If ReferenceEquals(f, active) OrElse ReferenceEquals(f, main) Then Continue For
                If TypeOf f Is WaitForm1 Then Continue For

                Dim typeName = f.GetType().FullName
                If String.IsNullOrEmpty(typeName) OrElse Not typeName.StartsWith("DevExpress.", StringComparison.Ordinal) Then Continue For
                If f.Modal Then Continue For
                If f.Visible AndAlso f.ContainsFocus Then Continue For

                Try
                    f.Hide()
                    f.Close()
                Catch
                End Try
            Next
        End Sub

        Private Shared Sub AppendLastOpenedFormAndUserControl(sb As StringBuilder)
            sb.AppendLine("--- Last opened UI (session) ---")
            SyncLock LastOpenedLock
                sb.AppendLine("Last form (Shown/visible at hook): " & If(String.IsNullOrEmpty(_lastFormSummary), "(not recorded yet)", _lastFormSummary))
                sb.AppendLine("  At (UTC): " & If(String.IsNullOrEmpty(_lastFormAtUtc), "-", _lastFormAtUtc))
                sb.AppendLine("Last user control (last became visible): " & If(String.IsNullOrEmpty(_lastUserControlSummary), "(not recorded yet)", _lastUserControlSummary))
                sb.AppendLine("  At (UTC): " & If(String.IsNullOrEmpty(_lastUserControlAtUtc), "-", _lastUserControlAtUtc))
            End SyncLock
            sb.AppendLine()
        End Sub

#End Region

        ''' <summary>Surfaces innermost exception first (where the bug usually is) and notes Invoke wrappers.</summary>
        Private Shared Sub AppendInvocationRootCauseSummary(sb As StringBuilder, ex As Exception)
            If ex Is Nothing Then Return
            Dim tie = TryCast(ex, TargetInvocationException)
            If tie IsNot Nothing AndAlso tie.InnerException IsNot Nothing Then
                sb.AppendLine(">>> Reflection.TargetInvocationException: the real error is almost always InnerException below (Invoke/Control.BeginInvoke marshaling).")
                sb.AppendLine()
            End If
            Dim deepest As Exception = ex
            While deepest.InnerException IsNot Nothing
                deepest = deepest.InnerException
            End While
            If ReferenceEquals(deepest, ex) Then Return
            sb.AppendLine("--- Innermost exception (read this first — root cause) ---")
            sb.AppendLine("Type: " & deepest.GetType().FullName)
            sb.AppendLine("Message: " & deepest.Message)
            Try
                Dim hr = Marshal.GetHRForException(deepest)
                sb.AppendLine("HResult: 0x" & hr.ToString("X8"))
            Catch
            End Try
            If deepest.TargetSite IsNot Nothing Then
                Dim decl = deepest.TargetSite.DeclaringType
                sb.AppendLine("TargetSite: " & If(decl Is Nothing, "?", decl.FullName) & "." & deepest.TargetSite.Name)
            End If
            If Not String.IsNullOrEmpty(deepest.StackTrace) Then
                sb.AppendLine("Stack:")
                sb.AppendLine(deepest.StackTrace)
            End If
            sb.AppendLine()
        End Sub

        Private Shared Sub AppendExceptionChain(sb As StringBuilder, ex As Exception)
            sb.AppendLine("--- Exception detail ---")
            If ex Is Nothing Then
                sb.AppendLine("(null exception)")
                Return
            End If
            AppendInvocationRootCauseSummary(sb, ex)
            Dim agg = TryCast(ex, AggregateException)
            If agg IsNot Nothing Then
                sb.AppendLine("AggregateException — flattened inners:")
                Dim idx = 0
                For Each innerEx As Exception In agg.Flatten().InnerExceptions
                    sb.AppendLine("  Inner[" & idx.ToString() & "]: " & innerEx.GetType().FullName & " — " & innerEx.Message)
                    idx += 1
                Next
                sb.AppendLine()
            End If
            Dim depth = 0
            Dim cur As Exception = ex
            While cur IsNot Nothing AndAlso depth < 32
                sb.AppendLine("--- Level " & depth.ToString() & " ---")
                sb.AppendLine("Type: " & cur.GetType().FullName)
                sb.AppendLine("Message: " & cur.Message)
                Try
                    Dim hr = Marshal.GetHRForException(cur)
                    sb.AppendLine("HResult: 0x" & hr.ToString("X8"))
                Catch
                End Try
                Dim w32 = TryCast(cur, Win32Exception)
                If w32 IsNot Nothing Then
                    sb.AppendLine("NativeErrorCode: " & w32.NativeErrorCode.ToString())
                End If
                Dim ioEx = TryCast(cur, IOException)
                If ioEx IsNot Nothing Then
                    sb.AppendLine("IOException HResult: 0x" & ioEx.HResult.ToString("X8"))
                End If
                If cur.TargetSite IsNot Nothing Then
                    Dim decl = cur.TargetSite.DeclaringType
                    sb.AppendLine("TargetSite: " & If(decl Is Nothing, "?", decl.FullName) & "." & cur.TargetSite.Name)
                End If
                If Not String.IsNullOrEmpty(cur.Source) Then
                    sb.AppendLine("Source: " & cur.Source)
                End If
                If Not String.IsNullOrEmpty(cur.StackTrace) Then
                    sb.AppendLine("Stack:")
                    sb.AppendLine(cur.StackTrace)
                End If
                cur = cur.InnerException
                depth += 1
            End While
            sb.AppendLine()
            sb.AppendLine("--- Full ToString() (includes inners) ---")
            sb.AppendLine(ex.ToString())
        End Sub

        ''' <summary>Writes full exception + environment context to a UTF-8 text file. Returns the file path, or Nothing on failure.</summary>
        Private Function TryWriteCrashReport(title As String, ex As Exception) As String
            If ex Is Nothing Then Return Nothing
            Dim dir = GetCrashLogDirectory()
            Dim filePath = Path.Combine(dir, $"crash_{DateTime.Now:yyyyMMdd_HHmmss_fff}.txt")
            Dim sb As New StringBuilder()
            sb.AppendLine("=== DentistX crash log ===")
            sb.AppendLine("Title: " & title)
            sb.AppendLine("Utc: " & DateTime.UtcNow.ToString("u", CultureInfo.InvariantCulture))
            sb.AppendLine("Local: " & DateTime.Now.ToString("u", CultureInfo.InvariantCulture))
            sb.AppendLine("Machine: " & Environment.MachineName)
            sb.AppendLine("User: " & Environment.UserDomainName & "\" & Environment.UserName)
            Try
                Dim clinicData As New ClinicDATA()
                Dim c = clinicData.SelectAll().FirstOrDefault()
                If c IsNot Nothing Then
                    sb.AppendLine("[ClinicNameEn]: " & If(c.ClinicNameEn, ""))
                    sb.AppendLine("[ClinicNameAr]: " & If(c.ClinicNameAr, ""))
                Else
                    sb.AppendLine("[ClinicNameEn]: ")
                    sb.AppendLine("[ClinicNameAr]: ")
                End If
            Catch ex1 As Exception
                sb.AppendLine("[ClinicNameEn]: (unavailable — " & ex1.Message & ")")
                sb.AppendLine("[ClinicNameAr]: (unavailable — " & ex1.Message & ")")
            End Try
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
            Try
                Dim mf = TryCast(MainForm, Form)
                sb.AppendLine("Main form: " & If(mf Is Nothing, "(none)", mf.GetType().FullName & " — " & mf.Text))
                sb.AppendLine("Open forms (" & System.Windows.Forms.Application.OpenForms.Count.ToString() & "):")
                For Each f As Form In System.Windows.Forms.Application.OpenForms
                    If f Is Nothing Then Continue For
                    Dim activeMark = If(ReferenceEquals(f, Form.ActiveForm), " [ACTIVE] ", " ")
                    sb.AppendLine("  -" & activeMark & f.GetType().FullName & " | " & f.Text)
                Next
            Catch
                sb.AppendLine("Open forms: (could not enumerate)")
            End Try
            sb.AppendLine()
            AppendLastOpenedFormAndUserControl(sb)
            AppendUiFocusContext(sb)
            AppendExceptionChain(sb, ex)
            Try
                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8)
                Return filePath
            Catch
                Return Nothing
            End Try
        End Function

        Private Sub ShowCrashNoticeAndExit(logPath As String, message As String)
            Dim msg As String
            If Not String.IsNullOrEmpty(logPath) Then
                msg = message & Environment.NewLine & Environment.NewLine &
                    "A detailed report was saved to:" & Environment.NewLine & logPath
            Else
                msg = message & Environment.NewLine & Environment.NewLine &
                    "Could not write a report file. Check folder permissions for:" & Environment.NewLine & GetCrashLogDirectory()
            End If
            Try
                MessageBox.Show(msg, "DentistX — Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch
            End Try
        End Sub

        Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            Try
                System.Windows.Forms.Application.SetUnhandledExceptionMode(System.Windows.Forms.UnhandledExceptionMode.CatchException)
            Catch
            End Try
            AddHandler System.Windows.Forms.Application.ThreadException, AddressOf OnApplicationThreadException
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnAppDomainUnhandledException
            AddHandler System.Windows.Forms.Application.Idle, AddressOf OnIdleTrackLastOpenedUi

            ' Connection string encryption
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim Settings As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)
            config.ConnectionStrings.SectionInformation.ProtectSection(Nothing) 'UnprotectSection() '
            ' We must save the changes to the configuration file.
            config.Save() '(ConfigurationSaveMode.Full, True)
            '================
            Dim lang As String = Settings.Settings.Item("Language").Value
            If String.IsNullOrWhiteSpace(lang) Then lang = "en"
            ' Per-user UI language (reliable); exe.config can be overwritten on rebuild or fail to save in protected folders.
            If Not String.IsNullOrWhiteSpace(My.Settings.UiLanguage) Then
                Dim u = My.Settings.UiLanguage.Trim()
                If String.Equals(u, "en", StringComparison.OrdinalIgnoreCase) OrElse String.Equals(u, "ar", StringComparison.OrdinalIgnoreCase) Then
                    lang = u.ToLowerInvariant()
                End If
            End If
            Dim db As String = Settings.Settings.Item("DbType").Value
            Dim sizeM As String = Settings.Settings.Item("Size").Value
            Dim EndDateStr As String = Settings.Settings.Item("EndDate").Value ' the format is dd/MM/yyyy
            Dim lastRunDateStr As String = Settings.Settings.Item("LastCheckDate").Value
            Dim Con As String = Settings.Settings.Item("Con").Value
            Dim host As String = Settings.Settings.Item("Server").Value
            Dim hostPass As String = Settings.Settings.Item("ServerPass").Value
            Dim StartUp As String = Settings.Settings.Item("Start").Value

            DbT = db
            HostName = host
            Start = StartUp


            If String.Equals(lang, "en", StringComparison.OrdinalIgnoreCase) Then
                Eng = True
                Dim uiCulture As New CultureInfo("en")
                Dim regionalCulture = CultureInfo.CreateSpecificCulture("en-US")
                Thread.CurrentThread.CurrentUICulture = uiCulture
                Thread.CurrentThread.CurrentCulture = regionalCulture
                CultureInfo.DefaultThreadCurrentCulture = regionalCulture
                CultureInfo.DefaultThreadCurrentUICulture = uiCulture
                CultureManager.ApplicationUICulture = uiCulture
            ElseIf String.Equals(lang, "ar", StringComparison.OrdinalIgnoreCase) Then
                Eng = False
                Dim regionalCulture = SettingsRuntimeApply.CreateArabicRegionalCultureGregorian()
                Dim uiCulture = CType(regionalCulture.Clone(), CultureInfo)
                Thread.CurrentThread.CurrentUICulture = uiCulture
                Thread.CurrentThread.CurrentCulture = regionalCulture
                CultureInfo.DefaultThreadCurrentCulture = regionalCulture
                CultureInfo.DefaultThreadCurrentUICulture = uiCulture
                CultureManager.ApplicationUICulture = uiCulture
            End If

            PrevLang = If(Eng, "en", "ar")


            ''=== Safe date parsing ===
            'Dim theLastRun As Date
            'Dim lastRun As Boolean = Date.TryParseExact(lastRunDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, LastCheckDate)
            'If lastRun Then
            '    theLastRun = Date.Parse(lastRunDateStr)
            'End If
            'Dim trial As Date
            'Dim success As Boolean = Date.TryParseExact(EndDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, trial)
            'If Not success Then
            '    MsgBox("Trial date is invalid. Application will now close.", MsgBoxStyle.Critical)
            '    End
            'End If

            'If Date.Now.Date > trial Then
            '    MsgBox("Trial Has Ended.", MsgBoxStyle.Critical)
            '    End
            'End If

            Dim lclDBCon As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBase\DentistX.mdf;Integrated Security=True"
            Dim lclSRVR As String = "Data Source=.;Initial Catalog=DentistX;Integrated Security=True"
            Dim lcDB2012 As String = "Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DBase012\DentistX.mdf;Integrated Security=True" '
            Dim remoteSRVR As String = "Data Source=" & host & ",1433;Initial Catalog=DentistX;Integrated Security=False;User ID=sa;Password=" & hostPass & ""
            ' Protect our vault
            Dim vaultConnection As New ConfigSectionProtector '("Vault")
            'vaultConnection.ChanglastRunDate(Now.ToShortDateString)
            If db = "Srvr" Then
                'vaultConnection.ChngConstring(Con)
                vaultConnection.ChngConstring(lclSRVR)
                DbT = "Srvr"
            ElseIf db = "Lcl" Then
                'vaultConnection.ChngConstring(Con)
                vaultConnection.ChngConstring(lclDBCon)
                DbT = "Lcl"
            ElseIf db = "Lcl2012" Then
                'vaultConnection.ChngConstring(Con)
                vaultConnection.ChngConstring(lcDB2012)
                DbT = "Lcl2012"
            ElseIf DbT = "LclOther" Then
                'vaultConnection.ChngConstring(Con)
                vaultConnection.ChngConstring(remoteSRVR)
                DbT = "LclOther"
            End If
            Dim vaultProtector As New ConfigSectionProtector("Vault")
            vaultProtector.ProtectSection()
            Dim vaultProtector1 As New ConfigSectionProtector("appSettings")
            vaultProtector1.ProtectSection()
            vaultProtector1.ProtectAppSection()

            AppointmentWhatsAppQueueService.InitializeAtApplicationStartup()
            QueueLatestCrashReportWhatsUploadIfNeeded()

            ' License: CursorLicManager must run after SQL is reachable (PC_Trials). See LicenseStartupGate.RunOnceAfterDatabaseConnected
            ' in MainView1 / MainView3 EnsureDatabaseConnection (after TestDatabaseConnection succeeds).

        End Sub

        Private Sub MyApplication_StartupNextInstance(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            e.BringToForeground = True
            Dim f = TryCast(MainForm, Form)
            If f Is Nothing OrElse f.IsDisposed Then Return
            If f.WindowState = FormWindowState.Minimized Then
                f.WindowState = FormWindowState.Normal
            End If
            f.Show()
            f.BringToFront()
            f.Activate()
        End Sub

        ''' <summary>UI-thread exceptions that are not handled by a Try/Catch (WinForms pipeline).</summary>
        Private Sub OnApplicationThreadException(sender As Object, e As System.Threading.ThreadExceptionEventArgs)
            If e Is Nothing Then Return
            If e.Exception Is Nothing Then Return
            If IsRecoverableNoUserHandlesTooltipFailure(e.Exception) Then
                ' ERROR_NO_MORE_USER_HANDLES (1158): USER/window-manager handle quota exhausted — often 32-bit + many DevExpress popups.
                ' DevExpress ToolTipController uses a WinForms Timer (native window) on mouse-move; failure is non-fatal; full exit makes it worse.
                Try
                    Debug.WriteLine("DentistX: recovered UI thread — Win32 1158 during tooltip/timer (close stray popups or use x64 build if available). " & e.Exception.Message)
                Catch
                End Try
                Return
            End If
            Dim logPath = TryWriteCrashReport("Application.ThreadException (UI thread)", e.Exception)
            ShowCrashNoticeAndExit(logPath, "An unexpected error occurred on the UI thread." & Environment.NewLine & e.Exception.Message)
            Try
                System.Windows.Forms.Application.Exit()
            Catch
                Environment.Exit(1)
            End Try
        End Sub

        ''' <summary>Non-UI threads (e.g. Task.Run, Timer); often fatal when <see cref="UnhandledExceptionEventArgs.IsTerminating"/> is true.</summary>
        Private Sub OnAppDomainUnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
            Dim ex = TryCast(e.ExceptionObject, Exception)
            If ex IsNot Nothing Then
                TryWriteCrashReport("AppDomain.UnhandledException (IsTerminating=" & e.IsTerminating.ToString() & ")", ex)
            End If
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            If e.Exception IsNot Nothing AndAlso IsRecoverableNoUserHandlesTooltipFailure(e.Exception) Then
                e.ExitApplication = False
                Try
                    Debug.WriteLine("DentistX: recovered UnhandledException path — Win32 1158 during tooltip/timer. " & e.Exception.Message)
                Catch
                End Try
                Return
            End If
            Dim msg = If(e.Exception IsNot Nothing, e.Exception.Message, "(no exception object)")
            Dim logPath = TryWriteCrashReport("MyApplication.UnhandledException", e.Exception)
            ShowCrashNoticeAndExit(logPath, "An unexpected error occurred." & Environment.NewLine & msg)
            e.ExitApplication = True
        End Sub

        ''' <summary>Win32 ERROR_NO_MORE_USER_HANDLES (1158) while DevExpress tooltip/scrollbar/timer creates a native window — safe to continue.</summary>
        Private Shared Function IsRecoverableNoUserHandlesTooltipFailure(ex As Exception) As Boolean
            Dim w32 = TryCast(ex, Win32Exception)
            If w32 Is Nothing Then Return False
            If w32.NativeErrorCode <> 1158 Then Return False
            Dim stack = If(ex.StackTrace, String.Empty)
            If stack.IndexOf("ToolTipController", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                stack.IndexOf("TimerNativeWindow", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                stack.IndexOf("FloatingScrollbarBase", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                stack.IndexOf("RestartAutoHideTimer", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                stack.IndexOf("NativeWindow.CreateHandle", StringComparison.OrdinalIgnoreCase) >= 0 Then
                Return True
            End If
            Dim msg = If(ex.Message, String.Empty)
            Return msg.IndexOf("window handle", StringComparison.OrdinalIgnoreCase) >= 0
        End Function
    End Class
End Namespace
