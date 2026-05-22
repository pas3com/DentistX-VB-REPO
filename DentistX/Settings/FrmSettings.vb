Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.IO.Compression
Imports System.Security.Cryptography
Imports System.Windows.Forms
Imports Infralution.Localization
Imports System.Threading.Tasks
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports System.ComponentModel
Public Class FrmSettings

    ''' <summary>Optional row on Programmer Settings tab; mirrors My.Settings.BackupOnExit.</summary>
    Private chkSilentBackupOnExit As CheckEdit

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = AppIcon
    End Sub
    Public Sub New(ByVal _Trt As String, ByVal _propName As String)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = AppIcon
        HideLayers()
        SetTrtsTreeViewDataSource()
        Treat = _Trt
        FilterTrtsTreeView(_Trt)
        txtSrchTrt.Text = _Trt
        ShowLayer(zSvg.RootItems, _propName)
        ' Select the node matching _Trt after the TreeView is populated
    End Sub

    Private Sub FrmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)
        'Dim appSet As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)
        'Dim lang As String = appSet.Settings.Item("Language").Value
        'Dim db As String = appSet.Settings.Item("DbType").Value
        If Eng Then
            Labelang.Text = "English"
            RadioEng.Checked = True
            If DbT = "Srvr" Then
                LabelDb.Text = "SQL Server Database"
                RadioSql.Checked = True
            ElseIf DbT = "Lcl" Then
                LabelDb.Text = "Local Database"
                RadioLocal.Checked = True
            ElseIf DbT = "Lcl2012" Then
                LabelDb.Text = "Local Database 2012"
                RadioLocal2012.Checked = True
            ElseIf DbT = "LclOther" Then
                LabelDb.Text = "Network Sql Database"
                RadioOther.Checked = True
                txtHostName.Text = HostName
            End If
            'If My.Computer.Screen.Bounds.Width > 1366 Then

            '    RadioLarge.Enabled = True
            'Else
            '    RadioLarge.Enabled = False
            'End If
            'If SizeMode = "عادي" OrElse SizeMode = "Normal" Then
            '    RadioNormal.Checked = True
            '    LabelSize.Text = "Normal"
            'ElseIf SizeMode = "كبير" OrElse SizeMode = "Large" Then
            '    RadioLarge.Checked = True
            '    LabelSize.Text = "Large"
            'End If
            If Start = "MainView1" Then
                RadioStar1.Checked = True
                LabelStart.Text = "MainView1"
            ElseIf Start = "MainView3" Then
                RadioStar3.Checked = True
                LabelStart.Text = "MainView3"
            End If
        Else
            If Start = "MainView1" Then
                RadioStar1.Checked = True
                LabelStart.Text = "MainView1"
            ElseIf Start = "MainView3" Then
                RadioStar3.Checked = True
                LabelStart.Text = "MainView3"
            End If
            Labelang.Text = "العربية"
            RadioArab.Checked = True
            If DbT = "Srvr" Then
                LabelDb.Text = "قاعدة بيانات من الخادم"
                RadioSql.Checked = True
            ElseIf DbT = "Lcl" Then
                LabelDb.Text = "قاعدة بيانات محلية"
                RadioLocal.Checked = True
            ElseIf DbT = "Lcl2012" Then
                LabelDb.Text = "قاعدة بيانات محلية 2012"
                RadioLocal2012.Checked = True
            ElseIf DbT = "LclOther" Then
                LabelDb.Text = "قاعدة بيانات على الشبكة"
                RadioOther.Checked = True
                txtHostName.Text = HostName
            End If
            'If My.Computer.Screen.Bounds.Width < 1550 Then
            '    RadioLarge.Checked = False
            '    RadioLarge.Enabled = False
            'Else
            '    RadioLarge.Enabled = True
            'End If
            If SizeMode = "عادي" OrElse SizeMode = "Normal" Then
                'RadioNormal.Checked = True
                LabelSize.Text = "عادي"
            ElseIf SizeMode = "كبير" OrElse SizeMode = "Large" Then
                'RadioLarge.Checked = True
                LabelSize.Text = "كبير"
            End If

        End If
        'Q0021090892855

        'Private Sub FrmDefColors_Load(sender As Object, e As EventArgs) Handles Me.Load
        'HideLayers()
        'SetTrtsTreeViewDataSource()
        If Not String.IsNullOrEmpty(Treat) Then
            SelectNodeByText(TrtsTreeView, Treat)
        End If
        'End Sub
        EnsureSilentBackupOnExitOptionUi()
    End Sub

    Private Sub EnsureSilentBackupOnExitOptionUi()
        If chkSilentBackupOnExit Is Nothing Then
            chkSilentBackupOnExit = New CheckEdit With {
                .Name = "chkSilentBackupOnExit",
                .Location = New Point(20, 372),
                .Width = 520
            }
            progSetPage.Controls.Add(chkSilentBackupOnExit)
            chkSilentBackupOnExit.BringToFront()
        End If
        chkSilentBackupOnExit.Properties.Caption = If(Eng, "Silent database backup when closing the application", "نسخ احتياطي صامت عند إغلاق البرنامج")
        chkSilentBackupOnExit.Checked = My.Settings.BackupOnExit
    End Sub


#Region "Preferences"


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

        Await Task.WhenAll(
            Helpers.LoadAdultSvgResourcesAsync(AddressOf SetStatusText),
            Helpers.LoadKidSvgResourcesAsync(AddressOf SetStatusText))
        'Me.Close() ' or signal that splash is done


        ' Close the splash after loading is done
        'Me.Invoke(Sub() Me.Close())

    End Sub

    Private Sub btnLoadSvgs_Click(sender As Object, e As EventArgs) Handles btnLoadSvgs.Click
        LoadResources()
    End Sub

    '======================

    Dim lang, srvr, Startup As String
    Dim lclString As String = "Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=DentistX;Integrated Security=True" ' "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBase\DentistX.mdf;Integrated Security=True"
    Dim SrvrString As String = "Data Source=.;Initial Catalog=DentistX;Integrated Security=True"
    Dim lcl012String As String = "Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DBase012\DentistX.mdf;Integrated Security=True" '
    Dim NetwrkString As String = "Data Source=" & HostName & ",1433;Initial Catalog=DentistX;Integrated Security=False;User ID=sa;Password=1234"
    '======================

    Public Sub ChangConstring(ByVal conStr As String)
        Try


            ' Might need to add a reference and Imports for System.Configuration
            If RadioOther.Checked = True Then
                If txtHostName.TextLength = 0 Then
                    MsgBox("You Must Enter Host Name...")
                    Exit Sub
                Else
                    HostName = txtHostName.Text
                    conStr = "Data Source=" & HostName & ",1433;Initial Catalog=DentistX;Integrated Security=False;User ID=sa;Password=1234"
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
                'RadioLarge.Enabled = True
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
    Public Sub ChangHostName(ByVal host As String)
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
            config.Save()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'ConfigurationManager.RefreshSection("connectionStrings")
    End Sub
    Private Sub BtnDefault_Click(sender As Object, e As EventArgs) Handles BtnDefault.Click
        Try
            Try
                ChangStart("MainView3")
                ChangLang("en")
                ChangDB("Srvr")
                ChangSize("Normal")
                ChangConstring(SrvrString)
                Application.Restart()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnSaveExit_Click(sender As Object, e As EventArgs) Handles BtnSaveExit.Click
        Try
            If RadioStar1.Checked = True Then
                Startup = "MainView1"
            ElseIf RadioStar2.Checked = True Then
                Startup = "MainView"
            ElseIf RadioStar3.Checked = True Then
                Startup = "MainView3"
            ElseIf RadioStar4.Checked = True Then
                Startup = "MainView3"
            End If
            If Me.RadioEng.Checked = True Then
                lang = "en"
            Else
                lang = "ar"
            End If
            If Me.RadioArab.Checked = True Then
                lang = "ar"
            Else
                lang = "en"
            End If
            If RadioSql.Checked = True Then
                srvr = "Srvr"
                DbT = "Srvr"
                'Else
                '    srvr = "Lcl"
                '    DbT = "Lcl"
            End If
            If RadioLocal.Checked = True Then
                srvr = "Lcl"
                DbT = "Lcl"
                'Else
                '    srvr = "Srvr"
                '    DbT = "Srvr"
            End If
            If RadioLocal2012.Checked = True Then
                srvr = "Lcl2012"
                DbT = "Lcl2012"
                'Else
                '    srvr = "Srvr"
                '    DbT = "Srvr"
            End If
            If RadioOther.Checked = True Then
                srvr = "LclOther"
                DbT = "LclOther"
                HostName = txtHostName.Text
            End If
            'If RadioNormal.Checked Then
            '    SizeMode = "Normal"
            'Else
            '    SizeMode = "Large"
            'End If
            ChangStart(Startup)
            ChangLang(lang)
            ChangDB(srvr)
            ChangSize(SizeMode)
            ChangHostName(HostName)
            If srvr = "Srvr" Then
                ChangConstring(SrvrString)
            ElseIf srvr = "Lcl" Then
                ChangConstring(lclString)
            ElseIf srvr = "Lcl2012" Then
                ChangConstring(lcl012String)
            ElseIf srvr = "LclOther" Then
                ChangConstring(NetwrkString)
            End If
            If chkSilentBackupOnExit IsNot Nothing Then
                My.Settings.BackupOnExit = chkSilentBackupOnExit.Checked
                My.Settings.Save()
            End If
            Me.Close()
            Application.Restart()
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
    Private Sub RadioOther_CheckedChanged(sender As Object, e As EventArgs) Handles RadioOther.CheckedChanged
        'If RadioOther.Checked = True Then
        '    txtHostName.Visible = True
        'Else
        '    txtHostName.Visible = False
        'End If
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
    Private Sub RadioStar2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioStar2.CheckedChanged

    End Sub

    Private Sub btnCreatApp_Click(sender As Object, e As EventArgs) Handles btnCreatApp.Click
        Const sourceFolder As String = "C:\Jamal\AppPngs"
        Const outputFile As String = "C:\Jamal\AppPngs\Pngs.encrypted"
        Dim password As String = "YourStrongPassword123!" ' Replace with secure password KidsSvgs
        'Dim result = FolderBD.ShowDialog
        ' Compress and encrypt
        CreateEncryptedAppResource(sourceFolder, outputFile, password)
        'LoadResources()
        MsgBox("Resource archive created successfully!")
    End Sub
    Sub CreateEncryptedAppResource(sourceFolder As String, outputFile As String, password As String)
        Try
            Dim tempZip As String = "C:\Jamal\tempSvg.zip"

            ' Delete existing output file (if it exists)
            If File.Exists(outputFile) Then
                File.Delete(outputFile)
            End If

            ' Delete temp ZIP (if leftover from a previous run)
            If File.Exists(tempZip) Then
                File.Delete(tempZip)
            End If
            ' Compress files
            ZipFile.CreateFromDirectory(sourceFolder, tempZip)

            ' Encrypt ZIP
            Using aes As Aes = Aes.Create()
                Dim salt = New Byte() {1, 2, 3, 4, 5, 6, 7, 8}
                Dim iterations = 1000
                Dim key = New Rfc2898DeriveBytes(password, salt, iterations).GetBytes(32)
                aes.Key = key
                aes.GenerateIV()

                Using outputStream = File.Create(outputFile)
                    ' Write IV
                    outputStream.Write(aes.IV, 0, aes.IV.Length)

                    ' Write encrypted data
                    Using encryptor = aes.CreateEncryptor()
                        Using cryptoStream = New CryptoStream(outputStream, encryptor, CryptoStreamMode.Write)
                            ' Fix: Properly dispose the input stream
                            Using inputStream = File.OpenRead(tempZip)
                                inputStream.CopyTo(cryptoStream)
                            End Using ' ← Input stream is now closed here
                        End Using
                    End Using
                End Using
            End Using

            ' Now safe to delete
            File.Delete(tempZip)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnCreateSVG_Click(sender As Object, e As EventArgs) Handles btnCreateSVG.Click
        Const sourceFolder As String = "D:\Jamal\RawSvg29-05-2025"
        Const outputFile As String = "D:\Jamal\RawSvg29-05-2025\Svgs.encrypted"
        Dim password As String = "YourStrongPassword123!" ' Replace with secure password KidsSvgs
        'Dim result = FolderBD.ShowDialog
        ' Compress and encrypt
        CreateEncryptedResourceArchive(sourceFolder, outputFile, password)
        'LoadResources()
        MsgBox("Resource archive created successfully!")
    End Sub
    Sub CreateEncryptedResourceArchive(sourceFolder As String, outputFile As String, password As String)
        Try
            Dim tempZip As String = "D:\Jamal\tempSvg.zip"

            ' Delete existing output file (if it exists)
            If File.Exists(outputFile) Then
                File.Delete(outputFile)
            End If

            ' Delete temp ZIP (if leftover from a previous run)
            If File.Exists(tempZip) Then
                File.Delete(tempZip)
            End If
            ' Compress files
            ZipFile.CreateFromDirectory(sourceFolder, tempZip)

            ' Encrypt ZIP
            Using aes As Aes = Aes.Create()
                Dim salt = New Byte() {1, 2, 3, 4, 5, 6, 7, 8}
                Dim iterations = 1000
                Dim key = New Rfc2898DeriveBytes(password, salt, iterations).GetBytes(32)
                aes.Key = key
                aes.GenerateIV()

                Using outputStream = File.Create(outputFile)
                    ' Write IV
                    outputStream.Write(aes.IV, 0, aes.IV.Length)

                    ' Write encrypted data
                    Using encryptor = aes.CreateEncryptor()
                        Using cryptoStream = New CryptoStream(outputStream, encryptor, CryptoStreamMode.Write)
                            ' Fix: Properly dispose the input stream
                            Using inputStream = File.OpenRead(tempZip)
                                inputStream.CopyTo(cryptoStream)
                            End Using ' ← Input stream is now closed here
                        End Using
                    End Using
                End Using
            End Using

            ' Now safe to delete
            File.Delete(tempZip)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnCreateSVGKids_Click(sender As Object, e As EventArgs) Handles btnCreateSVGKids.Click
        Const sourceFolder As String = "D:\Jamal\svgK"
        Const outputFile As String = "D:\Jamal\svgK\KidsSvgs.encrypted"
        Dim password As String = "YourStrongPassword123!" ' Replace with secure password KidsSvgs

        ' Compress and encrypt
        CreateEncryptedResourceArchive(sourceFolder, outputFile, password)
        'LoadResources()
        MsgBox("Resource archive created successfully!")
    End Sub
    Sub CreateEncryptedResourceArchiveKid(sourceFolder As String, outputFile As String, password As String)
        Try


            ' Create temporary ZIP
            Dim tempZip As String = "D:\Jamal\tempSvgKid.zip" 'Path.GetTempFileName()
            ' Delete existing output file (if it exists)
            If File.Exists(outputFile) Then
                File.Delete(outputFile)
            End If

            ' Delete temp ZIP (if leftover from a previous run)
            If File.Exists(tempZip) Then
                File.Delete(tempZip)
            End If
            ' Compress files
            ZipFile.CreateFromDirectory(sourceFolder, tempZip)

            ' Encrypt ZIP
            Using aes As Aes = Aes.Create()
                Dim salt = New Byte() {1, 2, 3, 4, 5, 6, 7, 8}
                Dim iterations = 1000
                Dim key = New Rfc2898DeriveBytes(password, salt, iterations).GetBytes(32)
                aes.Key = key
                aes.GenerateIV()

                Using outputStream = File.Create(outputFile)
                    ' Write IV
                    outputStream.Write(aes.IV, 0, aes.IV.Length)

                    ' Write encrypted data
                    Using encryptor = aes.CreateEncryptor()
                        Using cryptoStream = New CryptoStream(outputStream, encryptor, CryptoStreamMode.Write)
                            ' Fix: Properly dispose the input stream
                            Using inputStream = File.OpenRead(tempZip)
                                inputStream.CopyTo(cryptoStream)
                            End Using ' ← Input stream is now closed here
                        End Using
                    End Using
                End Using
            End Using

            ' Now safe to delete
            File.Delete(tempZip)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnExtractSVG_Click(sender As Object, e As EventArgs) Handles btnExtractSVG.Click
        Dim encryptedFile As String = "D:\Jamal\RawSvg29-05-2025\Svgs.encrypted"
        Dim password As String = "YourStrongPassword123!" ' Must match the encryption password
        Dim outputFolder As String = "D:\Jamal\AppPngs" ' Folder to extract to

        ' Let user choose output folder
        Dim fileDialog As New OpenFileDialog()
        fileDialog.Title = "Select encrypted file to extract files from"
        fileDialog.DefaultExt = "encrypted files" & vbNullChar & "*.encrypted"
        If fileDialog.ShowDialog = DialogResult.OK Then
            encryptedFile = fileDialog.FileName
        End If

        ' Let user choose output folder
        Dim folderDialog As New FolderBrowserDialog()
        folderDialog.Description = "Select folder to extract files to"
        folderDialog.SelectedPath = outputFolder

        If folderDialog.ShowDialog() = DialogResult.OK Then
            outputFolder = folderDialog.SelectedPath
            ' Decrypt and extract
            ExtractEncryptedResourceArchive(encryptedFile, outputFolder, password)
            MsgBox("Files extracted successfully to: " & outputFolder)
        End If
    End Sub
    Sub ExtractEncryptedResourceArchive(encryptedFile As String, outputFolder As String, password As String)
        Try
            Dim tempZip As String = Path.Combine(Path.GetTempPath(), "tempSvg_decrypted.zip")

            ' Delete temp ZIP if it exists from previous run
            If File.Exists(tempZip) Then
                File.Delete(tempZip)
            End If

            ' Ensure output directory exists
            Directory.CreateDirectory(outputFolder)

            ' Decrypt file
            Using aes As Aes = Aes.Create()
                Dim salt = New Byte() {1, 2, 3, 4, 5, 6, 7, 8}
                Dim iterations = 1000
                Dim key = New Rfc2898DeriveBytes(password, salt, iterations).GetBytes(32)
                aes.Key = key

                Using inputStream = File.OpenRead(encryptedFile)
                    ' Read IV from beginning of file
                    Dim iv As Byte() = New Byte(aes.IV.Length - 1) {}
                    inputStream.Read(iv, 0, iv.Length)
                    aes.IV = iv

                    ' Decrypt the rest of the file
                    Using decryptor = aes.CreateDecryptor()
                        Using cryptoStream = New CryptoStream(inputStream, decryptor, CryptoStreamMode.Read)
                            ' Save decrypted data to temp zip file
                            Using fileStream = File.Create(tempZip)
                                cryptoStream.CopyTo(fileStream)
                            End Using
                        End Using
                    End Using
                End Using
            End Using

            ' Extract ZIP contents
            ZipFile.ExtractToDirectory(tempZip, outputFolder)

            ' Clean up temp file
            File.Delete(tempZip)

        Catch ex As Exception
            MsgBox("Error during extraction: " & ex.Message)
        End Try
    End Sub

#End Region



#Region "Default Colors"








    ' Helper method to find and select a node by its text
    Private Sub SelectNodeByText(treeView As TreeView, textToFind As String)
        ' Search through all nodes recursively
        Dim foundNode As TreeNode = FindNodeByText(treeView.Nodes, textToFind)

        If foundNode IsNot Nothing Then
            treeView.SelectedNode = foundNode
            foundNode.EnsureVisible() ' Scroll to make the node visible
        End If
    End Sub

    Private Sub SelectNodeByTrt(treeView As TreeView, trtToFind As String)
        For Each groupNode As TreeNode In treeView.Nodes
            For Each trtNode As TreeNode In groupNode.Nodes
                Dim tag = TryCast(trtNode.Tag, Object)
                If tag IsNot Nothing AndAlso tag.Trt.ToString().Equals(trtToFind, StringComparison.OrdinalIgnoreCase) Then
                    treeView.SelectedNode = trtNode
                    trtNode.EnsureVisible()
                    Exit Sub
                End If
            Next
        Next
    End Sub
    ' Recursive method to find a node by its text
    Private Function FindNodeByText(nodes As TreeNodeCollection, textToFind As String) As TreeNode
        For Each node As TreeNode In nodes
            ' Check if this node matches
            If node.Text.Equals(textToFind, StringComparison.OrdinalIgnoreCase) Then
                Return node
            End If

            ' Check child nodes recursively
            Dim foundChildNode As TreeNode = FindNodeByText(node.Nodes, textToFind)
            If foundChildNode IsNot Nothing Then
                Return foundChildNode
            End If
        Next

        Return Nothing
    End Function


    Private Sub HideLayers()

        Dim col As SvgImageItemCollection = zSvg.RootItems

        ' Now set visibility only for items with a tag containing "IMG"
        For Each item As SvgImageItem In col 'sv.CustomizedItems
            item.Visible = False
        Next
        zSvg.Visible = True
    End Sub
    'Private slctdFillClr As Color
    'Private slctdBrdrClr As Color
    'Private slctdBrdThick As Int16
    Private propertyName As String
    Private clsTblTrtDATA As New TblTRTSDATA
    Private newClr As String
    Private newbrdCLR As String
    Private newbrdrThic As Integer

    Public Function GetAlphaFromHtmlColor(htmlColor As String) As Integer
        Try
            ' Check for null or empty input
            If String.IsNullOrWhiteSpace(htmlColor) Then
                Throw New ArgumentException("Input cannot be null or empty")
            End If

            ' Remove # and trim whitespace
            Dim cleanColor As String = htmlColor.Trim().Replace("#", "")

            ' Validate length
            Select Case cleanColor.Length
                Case 8 ' ARGB format: #AARRGGBB
                    Dim alphaHex As String = cleanColor.Substring(0, 2)
                    Return Integer.Parse(alphaHex, System.Globalization.NumberStyles.HexNumber)

                Case 6 ' RGB format: #RRGGBB (no alpha, fully opaque)
                    Return 255

                Case 4 ' #ARGB shorthand
                    Dim alphaHex As String = cleanColor.Substring(0, 1) & cleanColor.Substring(0, 1)
                    Return Integer.Parse(alphaHex, System.Globalization.NumberStyles.HexNumber)

                Case 3 ' #RGB shorthand (no alpha, fully opaque)
                    Return 255

                Case Else
                    Throw New ArgumentException("Invalid HTML color format")
            End Select

        Catch ex As Exception
            ' Log error if needed, then return default value
            Debug.WriteLine($"Error extracting alpha: {ex.Message}")
            Return 255 ' Default to fully opaque on error
        End Try
    End Function


    Private Sub btnSET_Click(sender As Object, e As EventArgs) Handles btnSET.Click
        If Treat IsNot Nothing AndAlso trtID <> -1 AndAlso DefaultColor IsNot Nothing AndAlso ShapeName IsNot Nothing Then

            newbrdCLR = ColorTranslator.ToHtml(BRDCLR)
            newClr = ColorTranslator.ToHtml(FillCLR)
            newbrdrThic = BRDRThic

            If clsTblTrtDATA.UpdateDefTrtClr(trtID, newClr, newbrdCLR, newbrdrThic) Then
                MsgBox("Colors Updated")
                SetTrtsTreeViewDataSource()
            End If
        End If

    End Sub


#Region "Colors"

    Property FillCLR As Color
        Get
            Return _fillCLR
        End Get
        Set
            _fillCLR = Value
            Dim col As SvgImageItemCollection = zSvg.RootItems
            For Each item As SvgImageItem In col
                If item.Visible = True AndAlso item IsNot Nothing Then
                    item.Appearance.Normal.FillColor = Value
                    lblFill.BackColor = Value
                End If
            Next
        End Set
    End Property

    Property BRDCLR As Color
        Get
            Return _brdCLR
        End Get
        Set
            _brdCLR = Value
            Dim col As SvgImageItemCollection = zSvg.RootItems
            For Each item As SvgImageItem In col
                If item.Visible = True AndAlso item IsNot Nothing Then
                    item.Appearance.Normal.BorderColor = Value
                    lblBorder.BackColor = Value
                End If
            Next
        End Set
    End Property

    Property BRDRThic As Int16
        Get
            Return _brdrThic
        End Get
        Set
            _brdrThic = Value
            Dim col As SvgImageItemCollection = zSvg.RootItems
            For Each item As SvgImageItem In col
                If item.Visible = True AndAlso item IsNot Nothing Then
                    item.Appearance.Normal.BorderThickness = Value
                    tbThick.Value = Value
                End If
            Next
        End Set
    End Property

    Private Sub clrFillColor_EditValueChanged(sender As Object, e As EventArgs) Handles clrFillColor.EditValueChanged
        FillCLR = clrFillColor.Color

        lblFill.BackColor = FillCLR
    End Sub

    Private Sub clrBorderColor_EditValueChanged(sender As Object, e As EventArgs) Handles clrBorderColor.EditValueChanged
        BRDCLR = clrBorderColor.Color

        lblBorder.BackColor = BRDCLR
    End Sub

    Private Sub tbThick_EditValueChanged(sender As Object, e As EventArgs) Handles tbThick.EditValueChanged
        Dim col As SvgImageItemCollection = zSvg.RootItems
        For Each item As SvgImageItem In col
            If item.Visible = True AndAlso item IsNot Nothing Then
                item.Appearance.Normal.BorderThickness = tbThick.Value
            End If
        Next
    End Sub
    Private Sub tbFillOpacity_EditValueChanged(sender As Object, e As EventArgs) Handles tbFillOpacity.EditValueChanged
        Dim a As Integer = tbFillOpacity.Value
        Dim r As Integer = FillCLR.R
        Dim g As Integer = FillCLR.G
        Dim b As Integer = FillCLR.B
        lblFill.BackColor = Color.FromArgb(a, r, g, b)
        FillCLR = Color.FromArgb(a, r, g, b)
    End Sub

    Private Sub tbBrdrOpacity_EditValueChanged(sender As Object, e As EventArgs) Handles tbBrdrOpacity.EditValueChanged
        Dim a As Integer = tbBrdrOpacity.Value
        Dim r As Integer = BRDCLR.R
        Dim g As Integer = BRDCLR.G
        Dim b As Integer = BRDCLR.B
        lblBorder.BackColor = Color.FromArgb(a, r, g, b)
        BRDCLR = Color.FromArgb(a, r, g, b)
    End Sub



#End Region


#Region "TrtsList and TrtsTreeView"

    'Private originalTrtsTable As DataTable
    'Private originalAddedTrtsTable As DataTable
    Private mouseDownLocation As Point
    Private mouseDownTime As DateTime

#Region "SUBS"


    Dim Treat As String = ""
    Dim trtID As Integer = 0
    Dim TrtClrID As Integer = 0
    Dim treatmentText As String = ""
    Private DefaultColor As String = ""
    Private CurrentColor As String = ""
    Private ShapeID As Integer = 0
    Private ShapeName As String = ""
    Private _fillCLR As Color
    Private _brdCLR As Color
    Private _brdrThic As Short

    Private Sub SetTrtsTreeViewDataSource1()
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Clear existing nodes in the TreeView
        TrtsTreeView.Nodes.Clear()
        ' Define the SQL query with filtering for ToothID and including ShapeName
        Dim query As String = ""
        query = "SELECT  dbo.TblTRTS.TrtID, dbo.TblTRTS.Trt, dbo.TblTRTS.TrtColor AS DefaultColor, dbo.TblTRTS.ShapeID, dbo.Shapes.ShapeName, dbo.TblTRTS.TrtClrID,
                                 dbo.TblTrtClr.FillColor AS CurrentColor, dbo.TblTRTS.TrtGroup
                         FROM    dbo.TblTRTS INNER JOIN
                                 dbo.TblTrtClr ON dbo.TblTRTS.TrtClrID = dbo.TblTrtClr.TrtClrID LEFT OUTER JOIN
                                 dbo.Shapes ON dbo.TblTRTS.ShapeID = dbo.Shapes.ShapeID
                         ORDER BY dbo.TblTRTS.TrtGroup, dbo.TblTRTS.Trt"
        ' Create and open a connection
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            ' Create a command with the parameterized query
            Dim command As New SqlCommand(query, connection)
            ' Create a DataAdapter to fill a DataTable
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            ' Group treatments by TrtGroup
            Dim groups = dataTable.AsEnumerable().GroupBy(Function(row) row.Field(Of String)("TrtGroup"))

            ' Add groups and treatments to the TreeView
            For Each group In groups
                ' Create a node for the group (parent node)
                Dim groupNode As New TreeNode(group.Key) With {
                            .Name = group.Key,
                            .ForeColor = Color.Blue ' Group nodes in blue
                        }

                ' Add child nodes for treatments
                For Each treatment In group
                    Dim treatmentNode As New TreeNode(treatment.Field(Of String)("Trt")) With {
                                .Tag = New With {
                                    Key .TrtID = treatment.Field(Of Integer)("TrtID"),
                                     Key .Treat = treatment.Field(Of String)("Trt"),
                                    Key .ShapeID = treatment.Field(Of Integer)("ShapeID"),
                                    Key .ShapeName = treatment.Field(Of String)("ShapeName"),
                                    Key .DefTrtColor = treatment.Field(Of String)("DefaultColor"),
                                    Key .TrtClrID = treatment.Field(Of Integer)("TrtClrID"),
                                    Key .CustomTrtColor = treatment.Field(Of String)("CurrentColor") ' Include TrtColor in the Tag
                                },
                                .ForeColor = Color.Green ' Treatment nodes in green
                            }
                    groupNode.Nodes.Add(treatmentNode)
                Next
                ' Add the group node to the TreeView
                TrtsTreeView.Nodes.Add(groupNode)
            Next
        End Using
    End Sub

    Private Sub SetTrtsTreeViewDataSource()
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Clear existing nodes in the TreeView
        TrtsTreeView.Nodes.Clear()

        ' Define the SQL query with filtering for ToothID and including ShapeName
        Dim query As String = ""
        query = "SELECT [TrtID],[Trt],TblTRTS.ShapeID,dbo.Shapes.[ShapeName],[TrtColor],[TrtBrdrClr],[TrtBrdrThick],[DefFillColor],[DefBrdrColor],
                    [DefBrdrThick], dbo.TblTRTS.TrtGroup
                    FROM [dbo].[TblTRTS]
                    LEFT OUTER JOIN dbo.Shapes ON dbo.TblTRTS.ShapeID = dbo.Shapes.ShapeID
                    ORDER BY dbo.TblTRTS.TrtGroup, dbo.TblTRTS.Trt"

        ' Create and open a connection
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            ' Create a command with the parameterized query
            Dim command As New SqlCommand(query, connection)
            ' Create a DataAdapter to fill a DataTable
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            ' Group treatments by TrtGroup
            Dim groups = dataTable.AsEnumerable().GroupBy(Function(row) row.Field(Of String)("TrtGroup"))

            ' Add groups and treatments to the TreeView
            For Each group In groups
                ' Create a node for the group (parent node)
                Dim groupNode As New TreeNode(group.Key) With {
                            .Name = group.Key,
                            .ForeColor = Color.Blue ' Group nodes in blue
                        }

                ' Add child nodes for treatments
                For Each treatment In group
                    Dim shapeIdObj = treatment("ShapeID")
                    Dim shapeNameObj = treatment("ShapeName")

                    Dim treatmentNode As New TreeNode(treatment.Field(Of String)("Trt")) With {
                                .Tag = New With {
                                    Key .TrtID = treatment.Field(Of Integer)("TrtID"),
                                    Key .Trt = treatment.Field(Of String)("Trt"), ' Changed from .Treat to .Trt to match field name
                                    Key .ShapeID = If(shapeIdObj Is DBNull.Value, Nothing, CInt(shapeIdObj)),
                                    Key .ShapeName = If(shapeNameObj Is DBNull.Value, String.Empty, CStr(shapeNameObj)),
                                    Key .DefTrtColor = treatment.Field(Of String)("DefFillColor"),
                                    Key .BrdrColor = treatment.Field(Of String)("TrtBrdrClr"),
                                    Key .BrdrThic = treatment.Field(Of Byte)("TrtBrdrThick"),
                                    Key .CurrentColor = treatment.Field(Of String)("TrtColor")
                                },
                                .ForeColor = Color.Green ' Treatment nodes in green
                            }
                    ' Key .TrtClrID = treatment.Field(Of Integer)("TrtClrID"),

                    groupNode.Nodes.Add(treatmentNode)
                Next
                ' Add the group node to the TreeView
                TrtsTreeView.Nodes.Add(groupNode)
            Next
        End Using
    End Sub

    Private Sub FilterTrtsTreeView(searchText As String)
        ' Rebuild the TreeView with filtered data
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        Dim query As String = "SELECT TrtGroup, TrtID, Trt, ToothID FROM TblTRTS WHERE ToothID = 'ALL' OR CHARINDEX(',' + @ToothID + ',', ',' + ToothID + ',') = 0 ORDER BY TrtGroup, Trt"
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ToothID", "ALL") ' Use appropriate ToothID if available
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            ' Clear the TreeView
            TrtsTreeView.Nodes.Clear()

            ' Group treatments by TrtGroup
            Dim groups = dataTable.AsEnumerable().
                        Where(Function(row) row.Field(Of String)("Trt").ToLower().Contains(searchText)).
                        GroupBy(Function(row) row.Field(Of String)("TrtGroup"))

            ' Add groups and treatments to TreeView
            For Each group In groups
                Dim groupNode As New TreeNode(group.Key)
                For Each treatment In group
                    Dim treatmentNode As New TreeNode(treatment.Field(Of String)("Trt")) With {
                                .Tag = treatment.Field(Of Integer)("TrtID")
                            }
                    groupNode.Nodes.Add(treatmentNode)
                Next
                TrtsTreeView.Nodes.Add(groupNode)
            Next
        End Using

        For Each groupNode As TreeNode In TrtsTreeView.Nodes
            Dim groupVisible As Boolean = False ' Track visibility for the parent node
            For Each treatmentNode As TreeNode In groupNode.Nodes
                If treatmentNode.Text.ToLower().Contains(searchText.ToLower()) Then
                    treatmentNode.ForeColor = Color.Red ' Highlight matching child node in red
                    treatmentNode.BackColor = Color.LightYellow ' Optional: Background highlight
                    treatmentNode.EnsureVisible() ' Ensure visibility of the matching node
                    groupVisible = True
                Else
                    treatmentNode.ForeColor = Color.Green ' Non-matching child nodes in green
                    treatmentNode.BackColor = Color.Transparent
                End If
            Next

            If groupVisible Then
                groupNode.ForeColor = Color.Blue ' Parent node in blue if any child is visible
                groupNode.BackColor = Color.Transparent
                groupNode.Expand() ' Expand parent if matches are found in children
            Else
                groupNode.ForeColor = Color.Transparent ' Hide parent node if no matching children
                groupNode.BackColor = Color.Transparent
            End If
        Next
    End Sub


    Private Sub txtSrchTrt_TextChanged(sender As Object, e As EventArgs) Handles txtSrchTrt.TextChanged
        FilterTrtsTreeView(txtSrchTrt.Text)
    End Sub




#End Region


#Region "TrtsTreeViewEvents"

    Public Class DragDropConstants
        Public Const CustomFormat As String = "YourApp.DragDropDataFormat"
    End Class

    <Serializable()>
    Public Class DragDropData
        Public Property treatText As String
        Public Property ShapeName As String
        Public Property ShapeID As Integer
    End Class
    'TrtsTreeView
    Private Sub TrtsTreeView_MouseDown(sender As Object, e As MouseEventArgs) Handles TrtsTreeView.MouseDown
        ' Record the initial position and time of the mouse down
        If e.Button = MouseButtons.Left Then
            mouseDownLocation = e.Location
            mouseDownTime = DateTime.Now
        End If
    End Sub
    Private Sub TrtsTreeView_MouseMove(sender As Object, e As MouseEventArgs) Handles TrtsTreeView.MouseMove
        ' Ensure the mouse down location and time are tracked globally
        If mouseDownLocation = Point.Empty Then Exit Sub

        ' Calculate distance and time since mouse down
        Dim distance As Integer = CInt(Math.Sqrt((e.X - mouseDownLocation.X) ^ 2 + (e.Y - mouseDownLocation.Y) ^ 2))
        Dim timeHeld As TimeSpan = DateTime.Now - mouseDownTime

        ' Initiate drag if left button is held, user has moved enough, and held for over 150 ms
        If e.Button = MouseButtons.Left AndAlso distance > 5 AndAlso timeHeld.TotalMilliseconds > 150 AndAlso TrtsTreeView.SelectedNode IsNot Nothing Then
            ' Get the selected node and ensure it's valid
            Dim selectedNode As TreeNode = TrtsTreeView.SelectedNode
            ' Prevent dragging the root node
            If selectedNode.Parent Is Nothing Then
                ' Root nodes have no parent; skip dragging
                Exit Sub
            End If
            ' Extract treatment data from the node
            'Extract Data from the node's Tag
            Dim trtText As String = selectedNode.Text
            Dim trtShapeName As String = selectedNode.Tag.ShapeName
            Dim shapeID As Integer = selectedNode.Tag.ShapeID

            ' Create a custom object with all data
            Dim dragData As New DragDropData With {
                            .treatText = trtText,
                            .ShapeName = trtShapeName,
                            .ShapeID = shapeID
                        }

            ' Package the data into a DataObject
            Dim dataObj As New DataObject(DragDropConstants.CustomFormat, dragData)

            ' Start drag-and-drop
            TrtsTreeView.DoDragDrop(dataObj, DragDropEffects.Copy)
        End If
    End Sub

    Private Sub TrtsTreeView_KeyDown(sender As Object, e As KeyEventArgs) Handles TrtsTreeView.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Ensure a node is selected
            Dim selectedNode = TrtsTreeView.SelectedNode
            If selectedNode Is Nothing Then
                MessageBox.Show("Please select an SvgImageBox and a treatment node before proceeding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Retrieve the treatment text from the double-clicked node
            Dim treatmentText As String = selectedNode.Text.Trim()
            Dim propertyName As String = ""
            If String.IsNullOrEmpty(treatmentText) Then
                MessageBox.Show("Invalid treatment data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If




        End If
    End Sub
    Private Sub TreeView_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TrtsTreeView.NodeMouseDoubleClick
        ' Ensure a node was double-clicked
        If e.Node Is Nothing OrElse e.Node.Parent Is Nothing Then
            MessageBox.Show("Please select an SvgImageBox before adding a treatment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Retrieve the treatment text from the double-clicked node
        Dim treatmentText As String = e.Node.Text.Trim()
        Dim propertyName As String = ""
        If String.IsNullOrEmpty(treatmentText) Then
            MessageBox.Show("Invalid treatment data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If



    End Sub

    Private Sub TrtsTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TrtsTreeView.AfterSelect
        ' Check if a treatment node is selected
        HideLayers()
        If e.Node?.Tag IsNot Nothing Then
            Try
                Dim selectedData = DirectCast(e.Node.Tag, Object)
                Treat = If(selectedData.Trt?.ToString(), String.Empty)
                trtID = CInt(selectedData.TrtID)
                'TrtClrID = CInt(selectedData.TrtClrID)

                DefaultColor = If(selectedData.DefTrtColor?.ToString(), "#FFFFFF") ' Default to white if null
                lblDefColor.BackColor = ColorTranslator.FromHtml(DefaultColor)

                CurrentColor = If(selectedData.CurrentColor?.ToString(), DefaultColor)
                FillCLR = ColorTranslator.FromHtml(CurrentColor)
                lblFill.BackColor = FillCLR
                clrFillColor.Color = FillCLR
                Dim brdColorStr As String = If(selectedData.BrdrColor?.ToString(), "#000000") ' Default to black if null
                BRDCLR = ColorTranslator.FromHtml(brdColorStr)
                BRDRThic = If(selectedData.BrdrThic IsNot Nothing, Convert.ToInt16(selectedData.BrdrThic), 1)
                tbThick.Value = BRDRThic
                clrBorderColor.Color = BRDCLR
                ShapeName = If(selectedData.ShapeName?.ToString(), String.Empty)
                lblTreat.Text = Treat & vbCrLf & ShapeName
                Dim col As SvgImageItemCollection = zSvg.RootItems
                Dim layer = col.Find(Function(c) c.Id = ShapeName)
                If layer IsNot Nothing Then
                    layer.Visible = True
                    layer.Appearance.Normal.FillColor = ColorTranslator.FromHtml(DefaultColor)
                    lblDefColor.BackColor = ColorTranslator.FromHtml(DefaultColor)
                Else
                    Treat = Nothing
                    trtID = -1
                    DefaultColor = Nothing
                    FillCLR = Color.Transparent
                    ShapeName = Nothing
                    lblDefColor.BackColor = Color.Transparent
                End If
                'ShowLayer(zSvg.RootItems, ShapeName)
            Catch ex As Exception
                MessageBox.Show("Error loading treatment data: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub TrtsTreeView_AfterSelect1(sender As Object, e As TreeViewEventArgs) 'Handles TrtsTreeView.AfterSelect
        ' Check if a treatment node is selected
        If e.Node.Tag IsNot Nothing Then
            Dim selectedData = DirectCast(e.Node.Tag, Object)
            Treat = selectedData.Trt
            trtID = selectedData.TrtID
            TrtClrID = selectedData.TrtClrID
            DefaultColor = selectedData.DefaultColor ' Assign TrtClr from the selected node's Tag
            CurrentColor = selectedData.CustomColor
            FillCLR = ColorTranslator.FromHtml(CurrentColor)
            lblDefColor.BackColor = ColorTranslator.FromHtml(DefaultColor)

            ShapeName = selectedData.ShapeName
            ShowLayer(zSvg.RootItems, ShapeName)
        End If
    End Sub

    Private Sub XtraTabControl1_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        HideLayers()
        SetTrtsTreeViewDataSource()
    End Sub

    ' Usage example with progress reporting
    Private Sub ButtonMigrateData_Click(sender As Object, e As EventArgs) Handles ButtonMigrateData.Click

        Dim Fr As New MigratForm
        Fr.ShowDialog()
        'Try
        '    Dim src = "Data Source=.;Initial Catalog=Dentist;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"
        '    Dim tgt = "Data Source=.;Initial Catalog=DentistX;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"

        '    Dim sync As New DatabaseSynchronizer(src, tgt)
        '    sync.SynchronizeDatabases()
        'Catch ex As Exception
        '    MessageBox.Show($"Migration failed: {ex.Message}", "Error",
        '               MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    Me.Cursor = Cursors.Default
        '    ButtonMigrateData.Enabled = True
        'End Try

        '===========================================================
        'Try
        '    Me.Cursor = Cursors.WaitCursor
        '    ButtonMigrateData.Enabled = False

        '    Dim migrator As New DatabaseMigration()

        '    ' Perform migration
        '    migrator.MigrateDentistToDentistX()

        '    ' Optional: Verify foreign keys
        '    migrator.VerifyForeignKeys()

        '    MessageBox.Show("Database migration completed successfully!", "Migration Complete",
        '               MessageBoxButtons.OK, MessageBoxIcon.Information)

        'Catch ex As Exception
        '    MessageBox.Show($"Migration failed: {ex.Message}", "Error",
        '               MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    Me.Cursor = Cursors.Default
        '    ButtonMigrateData.Enabled = True
        'End Try
    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        Backup(New CancelEventArgs, Me)
    End Sub

    Private Sub ShowLayer(col As SvgImageItemCollection, shapName As String)

        Dim layer = col.FirstOrDefault(Function(c) c.Id = shapName)
        If layer IsNot Nothing Then
            HideLayers()
            layer.Visible = True
            layer.Appearance.Normal.FillColor = ColorTranslator.FromHtml(DefaultColor)
            lblDefColor.BackColor = ColorTranslator.FromHtml(DefaultColor)
        Else
            Treat = Nothing
            trtID = -1
            DefaultColor = Nothing
            FillCLR = Color.Transparent
            ShapeName = Nothing
            lblDefColor.BackColor = Color.Transparent
        End If
    End Sub

    Private Sub btnSaveDefClrsExit_Click(sender As Object, e As EventArgs) Handles btnSaveDefClrsExit.Click
        If Treat IsNot Nothing AndAlso trtID <> -1 AndAlso DefaultColor IsNot Nothing AndAlso ShapeName IsNot Nothing Then

            newbrdCLR = ColorTranslator.ToHtml(BRDCLR)
            newClr = ColorTranslator.ToHtml(FillCLR)
            newbrdrThic = BRDRThic

            If clsTblTrtDATA.UpdateDefTrtClr(trtID, newClr, newbrdCLR, newbrdrThic) Then
                MsgBox("Colors Updated")
                Me.Close()
            End If
        End If

    End Sub










#End Region

#End Region



#End Region
End Class