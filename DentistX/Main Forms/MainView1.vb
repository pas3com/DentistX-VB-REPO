
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks
Imports DentistXLicense
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.ToastNotifications
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports AppResources
Imports DevExpress

Public Class MainView1

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.DoubleBuffered = True
        AppIcon = Me.Icon
        AutoAssignResources(Me.RibbonControl)
    End Sub
    Dim Finished As Boolean = False

    Private _weeklyBackupTimer As Timer
    Private _appointmentReminderTimer As Timer
    ''' <summary>Polls short-lead WhatsApp queue every minute; hours-before-appt from My.Settings.ShortReminder (same as MainView3).</summary>
    Private _twoHourApptReminderTimer As Timer
    Private _toastNotifications As ToastNotificationsManager
    ''' <summary>Lines for the last batch of WhatsApp reminders (status label hint + click).</summary>
    Private _lastReminderDetails As String = ""


    'Public WithEvents FulHdr As ThinHDRChrt 'FullHeader 'With {.Dock = DockStyle.Fill} FullHdrMob
    ''=============================
    'Private WithEvents thnHDR As ThinHDRChrt



    ' Automatically map controls to resources
    Private Sub AutoAssignResources(ribbon As DevExpress.XtraBars.Ribbon.RibbonControl)
        ' Handle buttons in Ribbon pages and groups
        For Each page As DevExpress.XtraBars.Ribbon.RibbonPage In ribbon.Pages
            For Each group As DevExpress.XtraBars.Ribbon.RibbonPageGroup In page.Groups
                For Each link As DevExpress.XtraBars.BarItemLink In group.ItemLinks
                    AssignImageToBarButton(link.Item)
                Next
            Next
        Next

        ' Handle ApplicationMenu items
        If ribbon.ApplicationButtonDropDownControl IsNot Nothing AndAlso TypeOf ribbon.ApplicationButtonDropDownControl Is DevExpress.XtraBars.Ribbon.ApplicationMenu Then
            Dim appMenu = CType(ribbon.ApplicationButtonDropDownControl, DevExpress.XtraBars.Ribbon.ApplicationMenu)
            For Each link As DevExpress.XtraBars.BarItemLink In appMenu.ItemLinks
                AssignImageToBarButton(link.Item)
            Next
        End If

        ' Handle PopupMenus
        ' PopupMenus declared on the form
        For Each field In Me.GetType().GetFields(Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
            If TypeOf field.GetValue(Me) Is DevExpress.XtraBars.PopupMenu Then
                Dim popup As DevExpress.XtraBars.PopupMenu = CType(field.GetValue(Me), DevExpress.XtraBars.PopupMenu)
                For Each link As DevExpress.XtraBars.BarItemLink In popup.ItemLinks
                    AssignImageToBarButton(link.Item)
                Next
            End If
        Next
    End Sub

    Private Sub AssignImageToBarButton(item As DevExpress.XtraBars.BarItem)
        If TypeOf item Is DevExpress.XtraBars.BarButtonItem Then
            Dim btn = CType(item, DevExpress.XtraBars.BarButtonItem)
            Dim imgKey As String = If(Not String.IsNullOrEmpty(btn.Name), btn.Name, btn.Caption)
            'btn.ImageOptions.Image = ResourceLoader.GetImage($"{imgKey}.png")
            If Not imgKey.StartsWith("S") OrElse imgKey.StartsWith("s") Then
                btn.ImageOptions.Image = ResourceLoader.GetPngResource($"{imgKey}.png")
            End If
        End If
    End Sub



    Private _cachedImages As New Dictionary(Of String, Image)

    Public Function GetCachedImage(key As String) As Image
        If Not _cachedImages.ContainsKey(key) Then
            '_cachedImages.Add(key, ResourceLoader.GetImage(key))
        End If
        Return _cachedImages(key)
    End Function

    '=================Main Form Events
#Region "Form Loading And DBase Connection (aligned with MainView3_Load)"

    Private Sub NewMainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged

        NewSplash.ShowDialog(Me)
        EnsureDatabaseConnection()

        Try
            ' Workspace tint for jaws when MainView1 is the host (MainView3 uses ContainerA.BackColor).
            BackClr = Me.BackColor

            BasePatientWorkspace.UseHdrTestModHeader = My.Settings.UseHdrTestModHeader

            Me.TodayButton.Caption = DateTime.Today.Date.ToString("yyyy/MM/dd")

            Finished = True

            If Eng Then
                Me.Text += " " & SizeMode
            Else
                If SizeMode = "Normal" Then
                    Me.Text += " " & "عادي"
                ElseIf SizeMode = "Large" Then
                    Me.Text += " " & "كبير"
                End If
            End If

            _weeklyBackupTimer = New Timer With {.Interval = 60 * 60 * 1000}
            AddHandler _weeklyBackupTimer.Tick, AddressOf WeeklyBackupTimer_Tick
            _weeklyBackupTimer.Start()
            WeeklyBackupTimer_Tick(Nothing, EventArgs.Empty)

            _appointmentReminderTimer = New Timer With {.Interval = 60 * 60 * 1000}
            AddHandler _appointmentReminderTimer.Tick, AddressOf AppointmentReminderTimer_Tick
            _appointmentReminderTimer.Start()
            Task.Run(Async Function()
                         Await Task.Delay(60 * 1000)
                         If _appointmentReminderTimer IsNot Nothing AndAlso IsHandleCreated Then
                             BeginInvoke(New Action(Sub() AppointmentReminderTimer_Tick(Nothing, EventArgs.Empty)))
                         End If
                     End Function)

            ApptWhatsAppReminderBackgroundPoller.EnsureStarted()

            _twoHourApptReminderTimer = New Timer With {.Interval = 60 * 1000}
            AddHandler _twoHourApptReminderTimer.Tick, AddressOf TwoHourApptReminderTimer_Tick
            _twoHourApptReminderTimer.Start()
            Task.Run(Async Function()
                         Await Task.Delay(15 * 1000)
                         If _twoHourApptReminderTimer IsNot Nothing AndAlso IsHandleCreated Then
                             BeginInvoke(New Action(Sub() TwoHourApptReminderTimer_Tick(Nothing, EventArgs.Empty)))
                         End If
                     End Function)

            _toastNotifications = New ToastNotificationsManager()
            WhatsAppToastHost.Register(Me, _toastNotifications)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Friend Sub ApplyRuntimeUiLanguageAfterCultureChange(culture As CultureInfo)
        RuntimeUiLanguage.ApplyFormControlTreeResources(Me, culture)
        If TodayButton IsNot Nothing Then
            TodayButton.Caption = DateTime.Today.Date.ToString("yyyy/MM/dd", culture)
        End If
    End Sub

    Private Sub ApplyLimitedModeUi()
        If StartupDataPage IsNot Nothing Then StartupDataPage.Visible = False
        If SettingsGroup IsNot Nothing Then SettingsGroup.Enabled = False
        If btnSettings IsNot Nothing Then btnSettings.Enabled = False
        If btnBackUpDB IsNot Nothing Then btnBackUpDB.Enabled = False
        If btnRestore IsNot Nothing Then btnRestore.Enabled = False
        If chkBackup IsNot Nothing Then chkBackup.Enabled = False
    End Sub

    Private Async Sub TwoHourApptReminderTimer_Tick(sender As Object, e As EventArgs)
        Try
            If _twoHourApptReminderTimer IsNot Nothing Then _twoHourApptReminderTimer.Stop()
            Await AppointmentTwoHourReminderService.RunAsync()
        Catch
        Finally
            If _twoHourApptReminderTimer IsNot Nothing AndAlso Not _twoHourApptReminderTimer.Enabled Then
                _twoHourApptReminderTimer.Start()
            End If
        End Try
    End Sub

    Private Async Sub AppointmentReminderTimer_Tick(sender As Object, e As EventArgs)
        Try
            If _appointmentReminderTimer IsNot Nothing Then _appointmentReminderTimer.Stop()
            Dim apptRes = Await AppointmentReminderService.RunAsync()
            Dim acctRes = Await AccountReminderScheduleService.RunAsync()
            Dim sentAppt = apptRes.SentCount
            Dim sentAcct = acctRes.SentCount
            If (sentAppt + sentAcct) > 0 AndAlso stLoadingLbl IsNot Nothing AndAlso IsHandleCreated Then
                Dim parts As New List(Of String)
                If sentAppt > 0 Then parts.Add($"{sentAppt} appointment reminder(s)")
                If sentAcct > 0 Then parts.Add($"{sentAcct} account reminder(s)")
                Dim lines As New List(Of String)
                lines.AddRange(apptRes.Lines)
                lines.AddRange(acctRes.Lines)
                _lastReminderDetails = String.Join(Environment.NewLine, lines)
                Dim caption = "Sent via WhatsApp: " & String.Join(", ", parts)
                Dim hintIntro = If(Eng, "Click for details:", "انقر للتفاصيل:")
                Dim hintText = If(String.IsNullOrEmpty(_lastReminderDetails), caption, hintIntro & Environment.NewLine & _lastReminderDetails)
                BeginInvoke(New Action(Sub()
                                           stLoadingLbl.Caption = caption
                                           stLoadingLbl.Hint = hintText
                                       End Sub))
            End If
        Catch
        Finally
            If _appointmentReminderTimer IsNot Nothing AndAlso Not _appointmentReminderTimer.Enabled Then
                _appointmentReminderTimer.Start()
            End If
        End Try
    End Sub

    Private Sub RibbonControl_ItemClick(sender As Object, e As ItemClickEventArgs) Handles RibbonControl.ItemClick
        If e.Item IsNot stLoadingLbl Then Return
        TryShowReminderDetailsFromStatusLabel()
    End Sub

    Private Sub TryShowReminderDetailsFromStatusLabel()
        If String.IsNullOrWhiteSpace(_lastReminderDetails) Then Return
        If stLoadingLbl Is Nothing OrElse stLoadingLbl.Caption Is Nothing OrElse Not stLoadingLbl.Caption.StartsWith("Sent via WhatsApp", StringComparison.OrdinalIgnoreCase) Then Return
        Dim title = If(Eng, "WhatsApp reminders sent", "تذكيرات واتساب مرسلة")
        XtraMessageBox.Show(_lastReminderDetails, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub WeeklyBackupTimer_Tick(sender As Object, e As EventArgs)
        Try
            DriveScheduledBackup.TryRunIfDue(Me)
            Dim lastStr As String = If(My.Settings.LastWeeklyBackupDate, "").Trim()
            Dim lastDate As DateTime
            Dim shouldRun As Boolean = False
            If String.IsNullOrEmpty(lastStr) Then
                shouldRun = True
            ElseIf DateTime.TryParse(lastStr, lastDate) Then
                If (DateTime.Today - lastDate.Date).TotalDays >= 7 Then
                    shouldRun = True
                End If
            Else
                shouldRun = True
            End If
            If shouldRun Then
                _weeklyBackupTimer.Stop()
                Backup(New CancelEventArgs)
                My.Settings.LastWeeklyBackupDate = DateTime.Today.ToString("yyyy-MM-dd")
                My.Settings.Save()
                _weeklyBackupTimer.Start()
            End If
        Catch ex As Exception
            If _weeklyBackupTimer IsNot Nothing AndAlso _weeklyBackupTimer.Enabled = False Then
                _weeklyBackupTimer.Start()
            End If
        End Try
    End Sub

    Private Sub EnsureDatabaseConnection()
        While True
            Try
                If TestDatabaseConnection() Then
                    If Not LicenseStartupGate.RunOnceAfterDatabaseConnected() Then
                        Me.Close()
                        Exit While
                    End If
                    If CursorLicManager.IsLimitedMode Then
                        ApplyLimitedModeUi()
                    End If
                    ShowLoginForm()
                    Exit While
                Else
                    Throw New Exception("Database connection test failed")
                End If
            Catch ex As Exception
                Dim result = MessageBox.Show($"Cannot connect to database: {ex.Message}" &
                                       Environment.NewLine &
                                       "Would you like to configure database connection?",
                                       "Database Connection Error",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                If result = DialogResult.Yes Then
                    ShowConnectionConfigurationForm()
                Else
                    Me.Close()
                    Exit While
                End If
            End Try
        End While
    End Sub

    Private Function TestDatabaseConnection() As Boolean
        Try
            Using connection As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                connection.Open()
                Return connection.State = ConnectionState.Open
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Sub ShowConnectionConfigurationForm()
        Using configForm As New FrmChooseConn()
            configForm.Icon = AppIcon
            If configForm.ShowDialog(Me) <> DialogResult.OK Then
                MsgBox("User canceled connection configuration")
                Me.Close()
            End If
        End Using
    End Sub

    Private Sub ShowLoginForm()
        FrmLogin.Icon = AppIcon
        Dim result = FrmLogin.ShowDialog(Me)
        If result <> DialogResult.OK Then
            Me.Close()
        Else
            RefreshStatusBarUserAndPatient()
        End If
    End Sub

    ''' <summary>Matches MainView3 status bar updates after Log IN (bntLogInOUT).</summary>
    Friend Sub RefreshStatusBarUserAndPatient()
        If CurrentUser Is Nothing Then Return
        If CurrentDoctor IsNot Nothing Then
            stUserNameTxt.Caption = $"User {CurrentUser.UsName} is associated with Doctor. {CurrentDoctor.DrName}"
        End If
        If CurrentSecretary IsNot Nothing Then
            stUserNameTxt.Caption = $"User {CurrentUser.UsName} is associated with Secretary. {CurrentSecretary.SecName}"
        End If
        If CurrentEmp IsNot Nothing Then
            stUserNameTxt.Caption = $"User {CurrentUser.UsName} is associated with Employee. {CurrentEmp.EmpName}"
        End If
        If CurrentDoctor Is Nothing AndAlso CurrentSecretary Is Nothing AndAlso CurrentEmp Is Nothing Then
            stUserNameTxt.Caption = CurrentUser.UsName & " == No Link"
        End If
        If CurrentDoctor Is Nothing AndAlso CurrentSecretary Is Nothing AndAlso CurrentEmp Is Nothing Then
            stUserNameTxt.Caption = CurrentUser.UsName & " == No Link"
        End If
        If currentPatient IsNot Nothing Then
            stPatientNameTxt.Caption = currentPatient.PatientName
        Else
            stPatientNameTxt.Caption = ""
        End If
    End Sub

#End Region
    Private Sub MainViewCopy_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            'todayForm.StartPosition = FormStartPosition.CenterParent
            'todayForm.ShowDialog(Me)
            'ViewInvoice.StartPosition = FormStartPosition.CenterParent

            'ThumbsFrm.ShowDialog(Me)


        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub


    Private Sub RibBtnExit_ItemClick(sender As Object, e As EventArgs) Handles btnExit.ItemClick
        Application.Exit()
    End Sub

    Private Sub btnWhatsAppLog_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnWhatsAppLog.ItemClick
        FrmWhatsAppActivityLog.ShowArchive(Me)
    End Sub

    Private Sub btnWhatsAppHub_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnWhatsAppHub.ItemClick
        Using hub As New FrmWhatsHub()
            hub.ShellIcon = AppIcon
            hub.ShowDialog(Me)
        End Using
    End Sub

    Private Sub MainView1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        WhatsAppToastHost.Unregister(Me)
        If _toastNotifications IsNot Nothing Then
            _toastNotifications.Dispose()
            _toastNotifications = Nothing
        End If
        RemoveHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
        If chkBackup.Checked Then
            Backup(New CancelEventArgs)
        End If
        If _weeklyBackupTimer IsNot Nothing Then
            _weeklyBackupTimer.Stop()
            RemoveHandler _weeklyBackupTimer.Tick, AddressOf WeeklyBackupTimer_Tick
            _weeklyBackupTimer.Dispose()
            _weeklyBackupTimer = Nothing
        End If
        If _appointmentReminderTimer IsNot Nothing Then
            _appointmentReminderTimer.Stop()
            RemoveHandler _appointmentReminderTimer.Tick, AddressOf AppointmentReminderTimer_Tick
            _appointmentReminderTimer.Dispose()
            _appointmentReminderTimer = Nothing
        End If
        If _twoHourApptReminderTimer IsNot Nothing Then
            _twoHourApptReminderTimer.Stop()
            RemoveHandler _twoHourApptReminderTimer.Tick, AddressOf TwoHourApptReminderTimer_Tick
            _twoHourApptReminderTimer.Dispose()
            _twoHourApptReminderTimer = Nothing
        End If
    End Sub

    '=================Main Form Events End

    Private _filter As String = "Treat"
    Private currentPatient As Patient

    Private Sub HandlePatientChanged(sender As Object, e As PatientChangedEventArgs)
        If e.NewPatient Is Nothing Then Return
        PatientID = e.NewPatient.PatientID
        PatientName = e.NewPatient.PatientName
        currentPatient = e.NewPatient
        currentPatient.IsKid = e.ShowHimAsKid
        stPatientNameTxt.Caption = $"{e.NewPatient.PatientName} ** {e.NewPatient.PatientNumber}"
    End Sub

    Public Sub SetStatusText(text As String)
        Dim apply =
            Sub()
                stLoadingLbl.Caption = text
                stLoadingLbl.Hint = Nothing
                _lastReminderDetails = ""
            End Sub
        If Me.InvokeRequired Then
            Me.Invoke(apply)
        Else
            apply()
        End If
    End Sub

    ''' <summary>Same role as MainView3.SyncPatientFromHdrTest when the first patient is added from the header (e.g. Navigator2).</summary>
    Public Sub SyncPatientFromHdrTest(patient As Patient)
        currentPatient = patient
        Select Case _filter
            Case "Treat"
                Try
                    If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
                    SplashScreenManager1.ShowWaitForm()
                    If stFormNameTxt IsNot Nothing Then stFormNameTxt.Caption = "Treatments Form"
                    FormManager.Instance.SwitchUserControl(GetType(TreatsUserControl), "Treat", patient, Me)
                Catch ex As Exception
                    MsgBox("Treatments form could not load: " & vbCrLf & vbCrLf & GetInnermostExceptionMessage(ex))
                Finally
                    SplashScreenManager1.CloseWaitForm()
                    Me.Cursor = Cursors.Default
                End Try
            Case "Ortho"
                Try
                    If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
                    SplashScreenManager1.ShowWaitForm()
                    If stFormNameTxt IsNot Nothing Then stFormNameTxt.Caption = "Orthodontic Form"
                    FormManager.Instance.SwitchUserControl(GetType(FullOrthoTreating), "Ortho", patient, Me)
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    SplashScreenManager1.CloseWaitForm()
                    Me.Cursor = Cursors.Default
                End Try
            Case "Diag"
                Try
                    If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
                    SplashScreenManager1.ShowWaitForm()
                    If stFormNameTxt IsNot Nothing Then stFormNameTxt.Caption = "Diagnostics Form"
                    FormManager.Instance.SwitchUserControl(GetType(DiagUserControl), "Diag", patient, Me)
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    SplashScreenManager1.CloseWaitForm()
                    Me.Cursor = Cursors.Default
                End Try
        End Select
    End Sub


#Region "Clinic Menu"
    Dim isKid As Boolean = False
    Private Shared Function GetInnermostExceptionMessage(ex As Exception) As String
        Dim inner = ex
        While inner.InnerException IsNot Nothing
            inner = inner.InnerException
        End While
        Return $"{inner.GetType().Name}: {inner.Message}"
    End Function

    ''' <summary>Screen rectangle between ribbon and status bar — same role as MainView3.ContainerA for aligning <see cref="BasePatientWorkspace"/>.</summary>
    Friend Function GetPatientWorkspaceScreenBounds() As Rectangle
        Dim top = RibbonControl.Bottom
        Dim bottom = StatusBar1.Top
        If bottom <= top OrElse ClientSize.Width <= 0 Then
            Return Rectangle.Empty
        End If
        Dim r As New Rectangle(0, top, ClientSize.Width, bottom - top)
        Return RectangleToScreen(r)
    End Function

    Public Sub TreatsButton_ItemClick(sender As Object, e As EventArgs) Handles TreatsButton.ItemClick

        Try
            _filter = "Treat"
            If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
            SplashScreenManager1.ShowWaitForm()
            If stFormNameTxt IsNot Nothing Then stFormNameTxt.Caption = "Treatments Form"
            Dim passPatient = If(currentPatient IsNot Nothing AndAlso currentPatient.PatientID > 0, currentPatient, Nothing)
            FormManager.Instance.SwitchUserControl(GetType(TreatsUserControl), "Treat", passPatient, Me)
        Catch ex As Exception
            MsgBox("Treatments form could not load: " & vbCrLf & vbCrLf & GetInnermostExceptionMessage(ex))
        Finally
            SplashScreenManager1.CloseWaitForm()
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub OrthoButton_ItemClick(sender As Object, e As EventArgs) Handles OrthoButton.ItemClick
        Try
            _filter = "Ortho"
            If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
            SplashScreenManager1.ShowWaitForm()
            If stFormNameTxt IsNot Nothing Then stFormNameTxt.Caption = "Orthodontic Form"
            Orthready = True
            Dim passPatient = If(currentPatient IsNot Nothing AndAlso currentPatient.PatientID > 0, currentPatient, Nothing)
            FormManager.Instance.SwitchUserControl(GetType(FullOrthoTreating), "Ortho", passPatient, Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SplashScreenManager1.CloseWaitForm()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub MobileButton_ItemClick(sender As Object, e As EventArgs) Handles DiagButton.ItemClick
        Try
            _filter = "Diag"
            If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
            SplashScreenManager1.ShowWaitForm()
            If stFormNameTxt IsNot Nothing Then stFormNameTxt.Caption = "Diagnostics Form"
            Dim passPatient = If(currentPatient IsNot Nothing AndAlso currentPatient.PatientID > 0, currentPatient, Nothing)
            FormManager.Instance.SwitchUserControl(GetType(DiagUserControl), "Diag", passPatient, Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SplashScreenManager1.CloseWaitForm()
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    '=================Clinic Menu End




#End Region
#Region "Auxiliary Menu"


    '=================Auxiliary Menu
    Private Sub AccountsButton_ItemClick(sender As Object, e As EventArgs) Handles AccountsButton.ItemClick
        Try
            CtAcntReady = True
            Dim passPatient = If(currentPatient IsNot Nothing AndAlso currentPatient.PatientID > 0, currentPatient, Nothing)
            FormManager.Instance.SwitchUserControl(GetType(NewAccounting), "Accounts", passPatient, Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub VisitsButton_ItemClick(sender As Object, e As EventArgs) Handles VisitsButton.ItemClick
        Try
            CtVisitReady = True
            Dim passPatient = If(currentPatient IsNot Nothing AndAlso currentPatient.PatientID > 0, currentPatient, Nothing)
            FormManager.Instance.SwitchUserControl(GetType(PatientVisitsCtl), "Visits", passPatient, Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub NotesButton_ItemClick(sender As Object, e As EventArgs) Handles NotesButton.ItemClick
        Try
            CtNoteReady = True
            Dim passPatient = If(currentPatient IsNot Nothing AndAlso currentPatient.PatientID > 0, currentPatient, Nothing)
            FormManager.Instance.SwitchUserControl(GetType(CtlNotes), "Notes", passPatient, Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RxButton_ItemClick(sender As Object, e As EventArgs) Handles RxButton.ItemClick
        Try
            If SplashScreenManager1.IsSplashFormVisible Then
                SplashScreenManager1.CloseWaitForm()
            End If
            SplashScreenManager1.ShowWaitForm()
            CtRxReady = True
            Dim passPatient = If(currentPatient IsNot Nothing AndAlso currentPatient.PatientID > 0, currentPatient, Nothing)
            FormManager.Instance.SwitchUserControl(GetType(RxCTLNEW), "Rx", passPatient, Me)
            SplashScreenManager1.CloseWaitForm()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub ImagesButton_ItemClick(sender As Object, e As EventArgs) Handles ImagesButton.ItemClick
        Try
            CtImgReady = True
            Dim passPatient = If(currentPatient IsNot Nothing AndAlso currentPatient.PatientID > 0, currentPatient, Nothing)
            FormManager.Instance.SwitchUserControl(GetType(ThumbNailViewer), "Images", passPatient, Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TodayButton_ItemClick(sender As Object, e As EventArgs) Handles TodayButton.ItemClick
        Try
            Dim fr As New TodayApptEditorForm
            fr.Icon = AppIcon
            fr.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CloseAuxButton_ItemClick(sender As Object, e As EventArgs) Handles CloseAuxButton.ItemClick
        Try
            PatientID = 0
            PatientName = ""
            currentPatient = Nothing
            If FormManager.Instance.IsBasePatientFormOpen Then
                FormManager.Instance.CloseBaseForm()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    '=================Auxiliary Menu End
#End Region
#Region "Patient Menu"


    '=================Patient Menu



    Private Sub ListPatientsButton_ItemClick(sender As Object, e As EventArgs) Handles btnListPatients.ItemClick
        Try
            'Dim F As New PatientChartsForm 'FrmListPatient2
            'F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnPatientsDebts_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnPatientsDebts.ItemClick
        Try
            'Dim F As New PatientDebtsForm 'FrmListPatient2
            'F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    '=================Patient Menu End
#End Region
#Region "Medic Menu"


    '=================Medic Menu

    '=================Medic Menu End
#End Region
#Region "Today Menu"


    '=================Today Menu
    'Private Sub RibBtnToday_ItemClick(sender As Object, e As EventArgs) Handles RibBtnToday.ItemClick
    '    Try

    '        'todayForm.StartPosition = FormStartPosition.CenterParent
    '        'todayForm.ShowDialog(Me)
    '        ''PrntForm2.StartPosition = FormStartPosition.CenterParent
    '        ''PrntForm2.ShowDialog(Me)
    '        FormAcntDet.Show()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)

    '    End Try
    'End Sub
    '=================Today Menu End
#End Region
#Region "Out Docs Menu"


    '=================Out Docs Menu
    Private Sub RibBtnLstDocs_ItemClick(sender As Object, e As EventArgs) Handles btnListDoctors.ItemClick
        Try
            'Dim F As New FrmOutDocs
            'F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub RibBtnDrWork_ItemClick(sender As Object, e As EventArgs) Handles btnDoctorsWork.ItemClick
        Try
            'Dim F As New FrmDrWork
            'F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Private Sub RibBtnDrPays_ItemClick(sender As Object, e As EventArgs) Handles btnDoctorsPay.ItemClick
        Try
            'Dim F As New FrmDrPay
            'F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    '=================Out Docs Menu End
#End Region
#Region "Labs Menu"



    '=================Labs Menu
    Private Sub RibBtnLabs_ItemClick(sender As Object, e As EventArgs) Handles btnListLabs.ItemClick
        Try
            'Dim F As New FrmLabs
            'F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub RibBtnLabOrdrs_ItemClick(sender As Object, e As EventArgs) Handles btnLabOrder.ItemClick
        Try
            If Trtready = True OrElse Orthready = True OrElse MobReady = True Then
                FromMenu = False
                OnePatient = True
            End If
            If Trtready = False And Orthready = False And MobReady = False Then
                FromMenu = True
                OnePatient = False
            End If
            'Dim F As New FrmLabOrder
            'F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub RibBtnLabPays_ItemClick(sender As Object, e As EventArgs) Handles btnLabPay.ItemClick
        Try
            If Trtready = True OrElse Orthready = True OrElse MobReady = True Then
                FromMenu = False
                OnePatient = True
            End If
            If Trtready = False And Orthready = False And MobReady = False Then
                FromMenu = True
                OnePatient = False
            End If
            'Dim F As New FrmLabPay 'LabPayments '
            'F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    '=================Labs Menu End

#End Region
#Region "Impressions Menu"


    '=================Impressions Menu


    '=================Impressions Menu End
#End Region
#Region "Invoices Menu"


    '=================Invoices Menu



    '=================Invoices Menu End
#End Region
#Region "Main Menu"


    '=================Dbase Menu
    Private Sub RibBtnRestore_ItemClick(sender As Object, e As EventArgs) Handles btnRestore.ItemClick
        Try
            FormRestoreAdv.Icon = AppIcon
            FormRestoreAdv.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub RibBtnBackup_ItemClick(sender As Object, e As EventArgs) Handles btnBackUpDB.ItemClick
        Try
            FormBackup.Icon = GetIcon()
            FormBackup.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub




    '=================Dbase Menu End
#End Region
#Region "Basic Data Menu"


    '=================Basic Data Menu
    Private Sub RibBtnCities_ItemClick(sender As Object, e As EventArgs) Handles btnCities.ItemClick
        Try
            Dim FR As New Frm_TblCities
            FR.Icon = AppIcon
            FR.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub RibBtnSupps_ItemClick(sender As Object, e As EventArgs) Handles btnVendors.ItemClick
        Try
            Dim FR As New FrmTblWireType
            FR.Icon = AppIcon
            FR.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub RibBtnTrts_ItemClick(sender As Object, e As EventArgs) Handles btnTrtTypes.ItemClick
        Try
            Dim FR As New FrmTblTRTS
            FR.Icon = AppIcon
            FR.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub RibBtnhealth_ItemClick(sender As Object, e As EventArgs) Handles btnHealth.ItemClick
        Try
            Dim F As New Frm_Health
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub RibBtnMedic_ItemClick(sender As Object, e As EventArgs) Handles RibBtnMedic.ItemClick
        Try
            Dim F As New MedicFormDS 'MedicForm 'FrmDefColors 'FrmCreateDash 'FrmDiagram 'FormSettings '
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub RibBtnRxFly_ItemClick(sender As Object, e As EventArgs) Handles btnRxonFly.ItemClick
        Try
            Dim F As New PatientRXFlyFrm 'FrmDiagram 'FormSettings ''MedicForm 'FrmDefColors 'FrmTrtDash
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub




    Private Sub RibBtnRxDetails_ItemClick(sender As Object, e As EventArgs) Handles btnRxDetails.ItemClick
        RxDetailFrm.ShowDialog(Me)
    End Sub
    '=================Basic Data Menu End
#End Region
#Region "Finance Menu"


    '=================Finance Menu


    '=================Finance Menu End
#End Region
#Region "Settings Menu"




    Private Sub RibBtnAbout_ItemClick(sender As Object, e As EventArgs) Handles btnAbout.ItemClick
        Try
            Dim F As New FormAbout
            F.ShowDialog(Me)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



#End Region

#Region "Quorters"


#End Region



    Dim BolDates() As Date = Nothing

    'Private Function GeBolds() As Date()
    '    Me.VisitsTableAdapter.Fill(Me.DsVisit.Visits)
    '    BolDates = DsVisit.Visits.AsEnumerable().Select(Function(r) r.Field(Of Date)(7)).ToArray()
    '    Return BolDates
    'End Function

    'Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '    Try
    '        BolDates = GeBolds()
    '        If BolDates.Length = 0 Then Exit Sub
    '        For i = 0 To BolDates.Length - 1
    '            If BolDates(i).ToShortDateString = Now.ToShortDateString Then
    '                If BolDates(i).ToShortTimeString = Now.ToShortTimeString Then
    '                    todayForm.ShowDialog(Me)
    '                End If
    '            End If
    '        Next

    '    Catch ex As Exception

    '    End Try
    'End Sub




    Private Function GetFileName(ByVal path As String) As String
        Dim value As String = ""
        Dim _filename As String = System.IO.Path.GetFileName(path)
        If path.Contains(".") Then
            Dim separator As String = "."
            Dim separatorIndex = _filename.IndexOf(separator)
            value = _filename.Substring(0, separatorIndex) ' + separator.Length)
        Else
            value = path
        End If

        Return value
    End Function



    Private Sub MainViewCopy_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'BackUp(e)

    End Sub





#Region "Skins"

    Protected Overrides Sub OnShown(ByVal e As EventArgs)
        MyBase.OnShown(e)
        RestorePalette()
        ShowSettings()
    End Sub

    Private Sub SavePalette()
        'Dim settings = My.Settings
        'settings.SkinName = UserLookAndFeel.Default.SkinName
        'settings.Palette = UserLookAndFeel.Default.ActiveSvgPaletteName
        'settings.CompactMode = UserLookAndFeel.Default.CompactUIModeForced
        ''settings.Lang = _lang
        'settings.Save()
    End Sub

    Private Sub RestorePalette()
        Dim settings = My.Settings
        'If Not String.IsNullOrEmpty(settings.SkinName) Then
        '    If settings.CompactMode Then UserLookAndFeel.ForceCompactUIMode(True, False)
        '    If Not String.IsNullOrEmpty(settings.Palette) Then
        '        UserLookAndFeel.Default.SetSkinStyle(settings.SkinName, settings.Palette)
        '    Else
        '        UserLookAndFeel.Default.SetSkinStyle(settings.SkinName)
        '    End If
        'End If
        ''If Not String.IsNullOrEmpty(settings.Lang) Then
        ''    _lang = settings.Lang
        ''    If _lang = "en" Then
        ''        Eng = True
        ''    Else
        ''        Eng = False
        ''    End If
        ''End If
    End Sub

    Private Sub ShowSettings()

        'labelControl1.Text = MySettings.Default.SkinName
        'labelControl4.Text = If(Not Equals(MySettings.Default.Palette, String.Empty), MySettings.Default.Palette, "n/a (default palette or raster skin)")
        'labelControl6.Text = If(MySettings.Default.CompactMode, "Yes", "No")
    End Sub


#End Region
    '==========================================================================
    '==========================================================================
#Region "Backup"

    Private Sub Backup(ByVal e As CancelEventArgs)
        Try
            SavePalette()
            BackupModule.Backup(e, Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnSettings_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnSettings.ItemClick
        Try
            Dim F As New FormSettings
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub




#End Region


End Class