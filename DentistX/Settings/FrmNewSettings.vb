
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Threading.Tasks
Imports System.Windows.Forms

Public Class FrmNewSettings

    'To connect through network do the following
    '1 install sql server on the main pc with mixed authentication -sa password 1234
    '2 add 2 inbound rules in the firewall on the main pc
    '    port 1433 tcp
    '    port 1434 udp
    '3 on the client choose Network DBase And enter the main pc name followed by coma And 1433
    '    example: MainPc,1433



    Private Sub FrmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Icon = AppIcon
        Me.Size = New Size(600, 654)
        'Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)
        'Dim appSet As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)
        'Dim lang As String = appSet.Settings.Item("Language").Value
        'Dim db As String = appSet.Settings.Item("DbType").Value
        If Eng Then
            Labelang.Text = "English"
            RadioLang.SelectedIndex = 1

            If DbT = "Srvr" Then
                LabelDb.Text = "SQL Server Database"
                RadioDbType.SelectedIndex = 0
            ElseIf DbT = "Lcl" Then
                LabelDb.Text = "Local Database"
                RadioDbType.SelectedIndex = 2
            ElseIf DbT = "Lcl2012" Then
                LabelDb.Text = "Local Database 2012"
                RadioDbType.SelectedIndex = 3
            ElseIf DbT = "LclOther" Then
                LabelDb.Text = "Network Sql Database"
                RadioDbType.SelectedIndex = 1
                txtHostName.Text = HostName
                txtSrvrPass.Text = HostPass

            End If
            'If My.Computer.Screen.Bounds.Width > 1366 Then

            '    RadioLarge.Enabled = True
            'Else
            '    RadioLarge.Enabled = False
            'End If
            If SizeMode = "عادي" OrElse SizeMode = "Normal" Then
                RadioSize.SelectedIndex = 0
                LabelSize.Text = "Normal"
            ElseIf SizeMode = "كبير" OrElse SizeMode = "Large" Then
                RadioSize.SelectedIndex = 1
                LabelSize.Text = "Large"
            End If
            If Start = "MainView1" Then
                RadioStartFrm.SelectedIndex = 0
                LabelStart.Text = "MainView1"
            ElseIf Start = "MainView3" Then
                RadioStartFrm.SelectedIndex = 1
                LabelStart.Text = "MainView3"
            End If
        Else
            If Start = "MainView1" Then
                RadioStartFrm.SelectedIndex = 0
                LabelStart.Text = "MainView1"
            ElseIf Start = "MainView3" Then
                RadioStartFrm.SelectedIndex = 1
                LabelStart.Text = "MainView3"
            End If
            Labelang.Text = "العربية"
            RadioLang.SelectedIndex = 0
            If DbT = "Srvr" Then
                LabelDb.Text = "قاعدة بيانات من الخادم"
                RadioDbType.SelectedIndex = 0
            ElseIf DbT = "Lcl" Then
                LabelDb.Text = "قاعدة بيانات محلية"
                RadioDbType.SelectedIndex = 2
            ElseIf DbT = "Lcl2012" Then
                LabelDb.Text = "قاعدة بيانات محلية 2012"
                RadioDbType.SelectedIndex = 3
            ElseIf DbT = "LclOther" Then
                LabelDb.Text = "قاعدة بيانات على الشبكة"
                RadioDbType.SelectedIndex = 1
                txtHostName.Text = HostName
            End If
            'If My.Computer.Screen.Bounds.Width < 1550 Then
            '    RadioLarge.Checked = False
            '    RadioLarge.Enabled = False
            'Else
            '    RadioLarge.Enabled = True
            'End If
            If SizeMode = "عادي" OrElse SizeMode = "Normal" Then
                RadioSize.SelectedIndex = 0
                LabelSize.Text = "عادي"
            ElseIf SizeMode = "كبير" OrElse SizeMode = "Large" Then
                RadioSize.SelectedIndex = 1
                LabelSize.Text = "كبير"
            End If

        End If
        'ShortRemind.Value = ShortReminder
        ShortRemind.Value = My.Settings.ShortReminder
        Dim day0 = DateTime.Today.Date
        dtStartTime.EditValue = day0.Add(My.Settings.SchedulerWorkingDayStart)
        dtEndTime.EditValue = day0.Add(My.Settings.SchedulerWorkingDayEnd)
        'Q0021090892855

        _sessionDbT = DbT
        _sessionLang = If(Eng, "en", "ar")
        _sessionHost = HostName
        _sessionPass = HostPass
        _sessionStart = Start
    End Sub

    Private _sessionDbT As String
    Private _sessionLang As String
    Private _sessionHost As String
    Private _sessionPass As String
    Private _sessionStart As String


    '======================

    Dim lang, srvr, Startup As String
    Dim lclString As String = "Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=DentistX;Integrated Security=True" ' "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBase\DentistX.mdf;Integrated Security=True"
    Dim SrvrString As String = "Data Source=.;Initial Catalog=DentistX;Integrated Security=True"
    Dim lcl012String As String = "Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DBase012\DentistX.mdf;Integrated Security=True" '
    Dim NetwrkString As String = "Data Source=" & HostName & ",1433;Initial Catalog=DentistX;Integrated Security=False;User ID=sa;Password=" & HostPass & ""
    '======================

    Public Sub ChangConstring(ByVal conStr As String)
        Try


            ' Might need to add a reference and Imports for System.Configuration
            If RadioDbType.SelectedIndex = 1 Then
                If txtHostName.Text.Length = 0 Then
                    MsgBox("You Must Enter Host Name...")
                    Exit Sub
                Else
                    HostName = txtHostName.Text
                    HostPass = txtSrvrPass.Text
                    conStr = "Data Source=" & HostName & ",1433;Initial Catalog=DentistX;Integrated Security=False;User ID=sa;Password=" & HostPass & ""
                End If
            End If
            Dim config As Configuration =
                ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)
            Dim section As ConnectionStringsSection = DirectCast(config.GetSection("connectionStrings"), ConnectionStringsSection)
            ' See the App.config file for the name to be used in the following
            Dim settings As ConnectionStringSettings = section.ConnectionStrings("DentistX.My.MySettings.DentistXConnectionString") 'Project.My.MySettings.DentistXConnectionString
            'Dim settings As ConnectionStringSettings = section.ConnectionStrings("Project.My.MySettings.DentistXConnectionString") 'DentistX.Settings.DentistXConnectionString

            settings.ConnectionString = conStr
            config.Save()
            ConfigurationManager.RefreshSection("connectionStrings")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    '===============================
    Public Sub ChangLang(ByVal lang As String)
        SettingsRuntimeApply.PersistLanguageToAppConfig(lang)
    End Sub

    '================================
    Public Sub ChangStart(ByVal start As String)

        Try
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim protectedSection As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)
            protectedSection.SectionInformation.UnprotectSection() '
            ' We must save the changes to the configuration file.
            config.Save() '(ConfigurationSaveMode.Full, True)
            '================

            protectedSection.Settings.Item("Start").Value = start
            config.Save()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    '============================
    Public Sub ChangSize(ByVal sizeM As String)
        '' Might need to add a reference and Imports for System.Configuration
        'Private map = New ExeConfigurationFileMap With {.ExeConfigFilename = "c:\\foo.config"}
        'Private configuration = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None)
        'Private myParamsSection = configuration.GetSection("MyParams")
        'Private myParamsCollection = myParamsSection.GetAs(Of NameValueCollection)()

        Try
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim protectedSection As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)
            protectedSection.SectionInformation.UnprotectSection() '
            ' We must save the changes to the configuration file.
            config.Save() '(ConfigurationSaveMode.Full, True)
            '================
            If Screen.PrimaryScreen.Bounds.Width > 1366 Then
                RadioSize.Enabled = True
            End If

            'Dim vaultSection As NameValueCollection = CType(ConfigurationManager.GetSection("Vault"), NameValueCollection)
            ''vaultSection.Set("Language", lang)
            'vaultSection.Item("Language") = lang
            'config.Save()
            protectedSection.Settings.Item("Size").Value = sizeM  'SizeMode
            config.Save()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'ConfigurationManager.RefreshSection("connectionStrings")
    End Sub
    '====================================
    Public Sub ChangDB(ByVal Db As String)
        ' Might need to add a reference and Imports for System.Configuration
        Try

            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)
            Dim protectedSection As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)
            protectedSection.SectionInformation.UnprotectSection()
            'Dim section As AppSettingsSection = config.AppSettings 'DirectCast(config.AppSettings("Vault"), ApplicationSettingsGroup)
            '' See the App.config file for the name to be used in the following
            'section.Settings.Item("DbType").Value = Db
            'Dim vaultSection As NameValueCollection = CType(ConfigurationManager.GetSection("Vault"), NameValueCollection)
            'vaultSection.Item("DbType") = Db
            'config.Save()
            protectedSection.Settings.Item("DbType").Value = Db
            config.Save()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'ConfigurationManager.RefreshSection("connectionStrings")
    End Sub

    '====================================
    Public Sub ChangHostName(ByVal host As String, ByVal pass As String)
        ' Might need to add a reference and Imports for System.Configuration
        Try

            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)
            Dim protectedSection As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)
            protectedSection.SectionInformation.UnprotectSection()
            'Dim section As AppSettingsSection = config.AppSettings 'DirectCast(config.AppSettings("Vault"), ApplicationSettingsGroup)
            '' See the App.config file for the name to be used in the following
            'section.Settings.Item("DbType").Value = Db
            'Dim vaultSection As NameValueCollection = CType(ConfigurationManager.GetSection("Vault"), NameValueCollection)
            'vaultSection.Item("DbType") = Db
            'config.Save()
            protectedSection.Settings.Item("Server").Value = host
            protectedSection.Settings.Item("ServerPass").Value = pass
            config.Save()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'ConfigurationManager.RefreshSection("connectionStrings")
    End Sub

    Private Sub AfterSettingsSavedNotifyAndClose(langSaved As String, srvrSaved As String, startupSaved As String, hostSaved As String, passSaved As String)
        Dim dbTypeChanged = Not String.Equals(srvrSaved, _sessionDbT, StringComparison.OrdinalIgnoreCase)
        Dim startChanged = Not String.Equals(startupSaved, _sessionStart, StringComparison.OrdinalIgnoreCase)

        Start = startupSaved
        SettingsRuntimeApply.ApplyLanguageFromSettingsCode(langSaved)
        Try
            ConfigurationManager.RefreshSection("appSettings")
            ConfigurationManager.RefreshSection("connectionStrings")
            My.Settings.Reload()
        Catch
        End Try

        Dim msg = BuildPostSaveUserMessage(dbTypeChanged, startChanged)
        MessageBox.Show(msg, If(Eng, "Settings", "الإعدادات"), MessageBoxButtons.OK, MessageBoxIcon.Information)

        _sessionDbT = srvrSaved
        _sessionLang = langSaved
        _sessionStart = startupSaved
        _sessionHost = hostSaved
        _sessionPass = passSaved

        Close()
    End Sub

    Private Function BuildPostSaveUserMessage(dbTypeChanged As Boolean, startChanged As Boolean) As String
        Dim parts As New List(Of String)
        parts.Add(If(Eng, "Settings were saved.", "تم حفظ الإعدادات."))
        If dbTypeChanged Then
            parts.Add(If(Eng,
                "You changed the database type. Fully close DentistX (all windows), then start it again so every part uses the new database mode.",
                "لقد غيّرت نوع قاعدة البيانات. أغلق DentistX بالكامل (كل النوافذ)، ثم شغّله من جديد ليتم استخدام الوضع الجديد في كل البرنامج."))
        End If
        If startChanged Then
            parts.Add(If(Eng,
                "You changed the startup screen. Fully close DentistX, then start it again to open with that main window.",
                "لقد غيّرت شاشة البداية. أغلق DentistX بالكامل، ثم شغّله من جديد لفتح النافذة الرئيسية التي اخترتها."))
        End If
        Return String.Join(Environment.NewLine & Environment.NewLine, parts)
    End Function


    Private Sub BtnDefault_Click(sender As Object, e As EventArgs) Handles BtnDefault.Click
        Try
            Try
                My.Settings.ShortReminder = 2
                Dim day0 = DateTime.Today.Date
                Dim startTod = New TimeSpan(8, 0, 0)
                Dim endTod = New TimeSpan(16, 0, 0)
                My.Settings.SchedulerWorkingDayStart = startTod
                My.Settings.SchedulerWorkingDayEnd = endTod
                dtStartTime.EditValue = day0.Add(startTod)
                dtEndTime.EditValue = day0.Add(endTod)
                My.Settings.Save()
                ChangStart("MainView3")
                ChangLang("en")
                ChangDB("Srvr")
                ChangSize("Normal")
                ChangConstring(SrvrString)
                DbT = "Srvr"
                SizeMode = "Normal"
                AfterSettingsSavedNotifyAndClose("en", "Srvr", "MainView3", HostName, HostPass)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Shared Function SchedulerTimeOfDayFromEditValue(editValue As Object, defaultTod As TimeSpan) As TimeSpan
        If editValue Is Nothing OrElse Object.ReferenceEquals(editValue, DBNull.Value) Then
            Return defaultTod
        End If
        If TypeOf editValue Is TimeSpan Then
            Return DirectCast(editValue, TimeSpan)
        End If
        If TypeOf editValue Is DateTime Then
            Return DirectCast(editValue, DateTime).TimeOfDay
        End If
        Try
            Return CType(editValue, DateTime).TimeOfDay
        Catch
            Return defaultTod
        End Try
    End Function

    Private Sub BtnSaveExit_Click(sender As Object, e As EventArgs) Handles BtnSaveExit.Click
        Try
            My.Settings.ShortReminder = ShortRemind.Value
            Dim startTod = SchedulerTimeOfDayFromEditValue(dtStartTime.EditValue, New TimeSpan(7, 0, 0))
            Dim endTod = SchedulerTimeOfDayFromEditValue(dtEndTime.EditValue, New TimeSpan(20, 0, 0))
            My.Settings.SchedulerWorkingDayStart = startTod
            My.Settings.SchedulerWorkingDayEnd = endTod
            My.Settings.Save()
            If RadioStartFrm.SelectedIndex = 0 Then
                Startup = "MainView1"
            ElseIf RadioStartFrm.SelectedIndex = 1 Then
                Startup = "MainView3"
            End If

            If RadioLang.SelectedIndex = 1 Then
                lang = "en"
            ElseIf RadioLang.SelectedIndex = 0 Then
                lang = "ar"
            End If

            If RadioDbType.SelectedIndex = 0 Then
                srvr = "Srvr"
                DbT = "Srvr"
            ElseIf RadioDbType.SelectedIndex = 2 Then
                srvr = "Lcl"
                DbT = "Lcl"
            ElseIf RadioDbType.SelectedIndex = 3 Then
                srvr = "Lcl2012"
                DbT = "Lcl2012"
            ElseIf RadioDbType.SelectedIndex = 1 Then
                srvr = "LclOther"
                DbT = "LclOther"
                HostName = txtHostName.Text
                HostPass = txtSrvrPass.Text
            End If

            If RadioSize.SelectedIndex = 0 Then
                SizeMode = "Normal"
            ElseIf RadioSize.SelectedIndex = 1 Then
                SizeMode = "Large"
            End If

            ChangStart(Startup)
            ChangLang(lang)
            ChangDB(srvr)
            ChangSize(SizeMode)
            ChangHostName(HostName, HostPass)
            If srvr = "Srvr" Then
                ChangConstring(SrvrString)
            ElseIf srvr = "Lcl" Then
                ChangConstring(lclString)
            ElseIf srvr = "Lcl2012" Then
                ChangConstring(lcl012String)
            ElseIf srvr = "LclOther" Then
                ChangConstring(NetwrkString)
            End If
            AfterSettingsSavedNotifyAndClose(lang, srvr, Startup, HostName, HostPass)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub BtnChngCon_Click(sender As Object, e As EventArgs)
        Dim config As System.Configuration.Configuration
        Dim fileMap As New ExeConfigurationFileMap()
        Dim dir As String = AppDomain.CurrentDomain.BaseDirectory

        fileMap.ExeConfigFilename = dir & "DentistX.exe.Config"
        config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None)

        '   Sets values to config file.
        If config.HasFile() Then

            config.AppSettings.Settings.Item("DentistXConnectionString").Value = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBase\DentistX.mdf;Integrated Security=True"
            config.Save(ConfigurationSaveMode.Modified)
            ConfigurationManager.RefreshSection("AppSettings")

        End If
    End Sub
    Private Sub ReadAppSection()
        Dim config As Configuration =
           ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)

        Dim section As AppSettingsSection = config.AppSettings 'DirectCast(config.AppSettings("Vault"), ApplicationSettingsGroup)
        ' See the App.config file for the name to be used in the following
        Dim _appConfig As New AppConfig

        LabelDb.Text = "DataBase Is -- " & section.Settings.Item("DbType").Value
        Labelang.Text = "Language Is -- " & section.Settings.Item("Language").Value
        LabelSize.Text = "SizeMode Is -- " & section.Settings.Item("Size").Value
        txtHostName.Text = section.Settings.Item("Server").Value
    End Sub




    Private Sub BtnCheckCon_Click(sender As Object, e As EventArgs)
        Dim dt As New DataTable
        Dim strConnString As String = ""
        Dim config As System.Configuration.Configuration
        Dim fileMap As New ExeConfigurationFileMap()
        Dim dir As String = AppDomain.CurrentDomain.BaseDirectory
        fileMap.ExeConfigFilename = dir & "DentistX.exe.Config"
        config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None)

        '   Sets values to config file.
        If config.HasFile() Then
            strConnString = config.AppSettings.Settings.Item("ConnectionString").Value
        End If
        Dim conn As New SqlConnection(strConnString)
        conn.Open()
        Dim sql As String = "select * from Health"
        Dim cmd As New SqlCommand(sql, conn)
        Dim adapter As New SqlDataAdapter(cmd)
        adapter.Fill(dt)
        conn.Close()
    End Sub


    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub RadioStar2_CheckedChanged(sender As Object, e As EventArgs)

    End Sub



    Private Sub txtSrvrPass_TextChanged(sender As Object, e As EventArgs) Handles txtSrvrPass.TextChanged

    End Sub

    Private Sub txtSrvrPass_MouseDown(sender As Object, e As MouseEventArgs) Handles txtSrvrPass.MouseDown
        If e.Button = MouseButtons.Right Then
            ' Remove password character to reveal text
            txtSrvrPass.Properties.PasswordChar = ControlChars.NullChar
        End If
    End Sub

    Private Sub txtSrvrPass_MouseUp(sender As Object, e As MouseEventArgs) Handles txtSrvrPass.MouseUp
        If e.Button = MouseButtons.Right Then
            ' Restore password character to hide text
            txtSrvrPass.Properties.PasswordChar = "*"c
        End If
    End Sub

    Private Sub txtSrvrPass_MouseLeave(sender As Object, e As EventArgs) Handles txtSrvrPass.MouseLeave
        ' Ensure password is hidden if mouse leaves while right button is pressed
        txtSrvrPass.Properties.PasswordChar = "*"c
    End Sub


#Region "LatestButtons"
    Public Sub SetStatusText(text As String)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() MainView3.stLoadingLbl.Caption = text)
        Else
            MainView3.stLoadingLbl.Caption = text
        End If
    End Sub

    Private Async Sub LoadResources()
        AdultResourceMapping.Clear()
        KidResourceMapping.Clear()

        Await Task.Run(Sub()
                           Dim unused = Helpers.LoadAdultSvgResourcesAsync(AddressOf SetStatusText)
                           Dim unused1 = Helpers.LoadKidSvgResourcesAsync(AddressOf SetStatusText)
                       End Sub)
        'Me.Close() ' or signal that splash is done


        ' Close the splash after loading is done
        'Me.Invoke(Sub() Me.Close())

    End Sub






#End Region


End Class