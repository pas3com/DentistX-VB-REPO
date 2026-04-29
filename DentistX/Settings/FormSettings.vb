Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.IO.Compression
Imports System.Windows.Forms
Imports Infralution.Localization
Imports System.Threading.Tasks

Public Class FormSettings




    'To connect through network do the following
    '1 install sql server on the main pc with mixed authentication -sa password 1234
    '2 add 2 inbound rules in the firewall on the main pc
    '    port 1433 tcp
    '    port 1434 udp
    '3 on the client choose Network DBase And enter the main pc name followed by coma And 1433
    '    example: MainPc,1433



    Private Sub FrmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnCreatApp.Enabled = Perms.CanDo("Admins.ALl")
        btnCreateSVG.Enabled = Perms.CanDo("Admins.ALl")
        btnCreateSVGKids.Enabled = Perms.CanDo("Admins.ALl")
        btnLoadSvgs.Enabled = Perms.CanDo("Admins.ALl")
        btnExtractSVG.Enabled = Perms.CanDo("Admins.ALl")

        Me.Icon = AppIcon
        Me.Size = New Size(564, 653)
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
                txtSrvrPass.Text = HostPass

            End If
            'If My.Computer.Screen.Bounds.Width > 1366 Then

            '    RadioLarge.Enabled = True
            'Else
            '    RadioLarge.Enabled = False
            'End If
            If SizeMode = "عادي" OrElse SizeMode = "Normal" Then
                RadioNormal.Checked = True
                LabelSize.Text = "Normal"
            ElseIf SizeMode = "كبير" OrElse SizeMode = "Large" Then
                RadioLarge.Checked = True
                LabelSize.Text = "Large"
            End If
            If Start = "MainView4" Then
                RadioStar1.Checked = True
                LabelStart.Text = "MainView4"
            ElseIf Start = "MainView3" Then
                RadioStar3.Checked = True
                LabelStart.Text = "MainView3"
            End If
        Else
            If Start = "MainView4" Then
                RadioStar1.Checked = True
                LabelStart.Text = "MainView4"
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
                RadioNormal.Checked = True
                LabelSize.Text = "عادي"
            ElseIf SizeMode = "كبير" OrElse SizeMode = "Large" Then
                RadioLarge.Checked = True
                LabelSize.Text = "كبير"
            End If

        End If
        'ShortRemind.Value = ShortReminder
        ShortRemind.Value = My.Settings.ShortReminder
        'Q0021090892855
    End Sub


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
            If RadioOther.Checked = True Then
                If txtHostName.TextLength = 0 Then
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
                RadioLarge.Enabled = True
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


    Private Sub BtnDefault_Click(sender As Object, e As EventArgs) Handles BtnDefault.Click
        Try
            Try
                My.Settings.ShortReminder = 2
                My.Settings.Save()
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
            My.Settings.ShortReminder = ShortRemind.Value
            My.Settings.Save()
            If RadioStar1.Checked = True Then
                Startup = "MainView4"
            ElseIf RadioStar3.Checked = True Then
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

            End If
            If RadioOther.Checked = True Then
                srvr = "LclOther"
                DbT = "LclOther"
                HostName = txtHostName.Text
                HostPass = txtSrvrPass.Text
            End If
            If RadioNormal.Checked Then
                SizeMode = "Normal"
            Else
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
            Me.Close()
            ' Ensure debug mode on restart
            EnsureDebugMode()

            Application.Restart()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub EnsureDebugMode()
#If DEBUG Then
        ' Only in debug builds
        If System.Diagnostics.Debugger.IsAttached Then
            ' This will prompt to attach debugger to the new process
            System.Diagnostics.Debugger.Launch()
        End If
#End If
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

    Private Sub RadioStar2_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnCreateSVG_Click(sender As Object, e As EventArgs) Handles btnCreateSVG.Click

        'Const sourceFolder As String = "D:\Jamal\RawSvg29-05-2025"
        'Const outputFile As String = "D:\Jamal\RawSvg29-05-2025\Svgs.encrypted"
        'Dim password As String = "YourStrongPassword123!" ' Replace with secure password KidsSvgs

        '' Compress and encrypt
        'CreateEncryptedResourceArchive(sourceFolder, outputFile, password)
        'MsgBox("Resource archive created successfully!")
    End Sub

    Private Sub btnCreateSVGKids_Click(sender As Object, e As EventArgs) Handles btnCreateSVGKids.Click
        Const sourceFolder As String = "D:\Jamal\svgK"
        Const outputFile As String = "D:\Jamal\svgK\KidsSvgs.encrypted"
        Dim password As String = "YourStrongPassword123!" ' Replace with secure password KidsSvgs

        ' Compress and encrypt
        CreateEncryptedResourceArchive(sourceFolder, outputFile, password)
        MsgBox("Resource archive created successfully!")
    End Sub

    Private Sub txtSrvrPass_TextChanged(sender As Object, e As EventArgs) Handles txtSrvrPass.TextChanged

    End Sub

    Sub CreateEncryptedResourceArchive(sourceFolder As String, outputFile As String, password As String)
        Try
            Dim tempZip As String = "D:\Jamal\tempSvg.zip"

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

    Sub CreateEncryptedResourceArchiveKid(sourceFolder As String, outputFile As String, password As String)
        Try


            ' Create temporary ZIP
            Dim tempZip As String = "D:\Jamal\tempSvgKid.zip" 'Path.GetTempFileName()

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

    Private Sub txtSrvrPass_MouseDown(sender As Object, e As MouseEventArgs) Handles txtSrvrPass.MouseDown
        If e.Button = MouseButtons.Right Then
            ' Remove password character to reveal text
            txtSrvrPass.PasswordChar = ControlChars.NullChar
        End If
    End Sub

    Private Sub txtSrvrPass_MouseUp(sender As Object, e As MouseEventArgs) Handles txtSrvrPass.MouseUp
        If e.Button = MouseButtons.Right Then
            ' Restore password character to hide text
            txtSrvrPass.PasswordChar = "*"c
        End If
    End Sub

    Private Sub txtSrvrPass_MouseLeave(sender As Object, e As EventArgs) Handles txtSrvrPass.MouseLeave
        ' Ensure password is hidden if mouse leaves while right button is pressed
        txtSrvrPass.PasswordChar = "*"c
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

    Private Sub btnLoadSvgs_Click(sender As Object, e As EventArgs) Handles btnLoadSvgs.Click
        LoadResources()
    End Sub

    Private Sub btnCreatApp_Click(sender As Object, e As EventArgs) Handles btnCreatApp.Click
        Const sourceFolder As String = "D:\Jamal\AppPngs"
        Const outputFile As String = "D:\Jamal\AppPngs\Pngs.encrypted"
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


End Class