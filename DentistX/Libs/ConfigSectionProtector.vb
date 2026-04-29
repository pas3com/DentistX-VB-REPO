Imports System.Configuration
Imports System.Reflection

''' <summary>
''' This class protects (encrypts) a section in the applications configuration file.
''' </summary>
''' <remarks>The <seealso cref="RsaProtectedConfigurationProvider" /> is used in this implementation.</remarks>
Public Class ConfigSectionProtector

    Private m_Section As String

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <param name="section">The section name.</param>
    Public Sub New(ByVal section As String)
        If String.IsNullOrEmpty(section) Then Throw New ArgumentNullException("ConfigurationSection")

        m_Section = section
    End Sub

    ''' <summary>
    ''' This method protects a section in the applications configuration file. 
    ''' </summary>
    ''' <remarks>The <seealso cref="RsaProtectedConfigurationProvider" /> is used in this implementation.</remarks>
    Public Sub ProtectSection()
        'https://www.codeproject.com/Articles/18209/Encrypting-the-app-config-File-for-Windows-Forms-A
        ' Get the current configuration file.
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim protectedSection As ConfigurationSection = config.GetSection(m_Section)

        ' Encrypts when possible
        If ((protectedSection IsNot Nothing) _
        AndAlso (Not protectedSection.IsReadOnly) _
        AndAlso (Not protectedSection.SectionInformation.IsProtected) _
        AndAlso (Not protectedSection.SectionInformation.IsLocked) _
        AndAlso (protectedSection.SectionInformation.IsDeclared)) Then
            ' Protect (encrypt)the section.
            protectedSection.SectionInformation.ProtectSection(Nothing)
            ' Save the encrypted section.
            protectedSection.SectionInformation.ForceSave = True
            config.Save(ConfigurationSaveMode.Full)
            ResetConfigMechanism()
        End If
    End Sub
    '====================================
    Public Sub ProtectAppSection()
        'https://www.codeproject.com/Articles/18209/Encrypting-the-app-config-File-for-Windows-Forms-A
        ' Get the current configuration file.
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

        Dim isProtected, isLocked As Boolean
        Dim protectedSection As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)
        If protectedSection Is Nothing Then
            Exit Sub
        End If

        isProtected = protectedSection.SectionInformation.IsProtected
        If isProtected = True Then
            protectedSection.SectionInformation.UnprotectSection()
        End If
        isLocked = protectedSection.SectionInformation.IsLocked
        If isLocked = True Then
            protectedSection.LockItem = False
        End If

        ' Encrypts when possible
        If ((protectedSection IsNot Nothing) _
                AndAlso (Not protectedSection.SectionInformation.IsProtected) _
        AndAlso (Not protectedSection.SectionInformation.IsLocked)
       ) Then
            ' Protect (encrypt)the section.
            protectedSection.SectionInformation.ProtectSection(Nothing)
            ' Save the encrypted section.
            protectedSection.SectionInformation.ForceSave = True
            config.Save(ConfigurationSaveMode.Full)
        Else
            MsgBox("")
        End If
        'AndAlso (Not protectedSection.IsReadOnly) _
        ' AndAlso (protectedSection.SectionInformation.IsDeclared)
    End Sub
    '================================================
    Private Sub ProtectConfiguration()
        ' Connection string encryption
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        If Not config.ConnectionStrings.SectionInformation.IsProtected Then
            config.ConnectionStrings.SectionInformation.ProtectSection(Nothing)

            ' We must save the changes to the configuration file.
            config.Save(ConfigurationSaveMode.Minimal, True)
        End If
        If Not config.AppSettings.SectionInformation.IsProtected Then
            config.ConnectionStrings.SectionInformation.ProtectSection(Nothing)

            ' We must save the changes to the configuration file.
            config.Save(ConfigurationSaveMode.Minimal, True)
        End If
    End Sub
    '============================

    Public Sub ProtectAppSettings1()
        ' Connection string encryption
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

        If Not config.AppSettings.SectionInformation.IsProtected Then
            config.ConnectionStrings.SectionInformation.ProtectSection(Nothing)

            ' We must save the changes to the configuration file.
            config.Save(ConfigurationSaveMode.Minimal, True)
            ResetConfigMechanism()
        End If
    End Sub



    Public Sub ProtectAppSettings()
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim section As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)

        If section IsNot Nothing AndAlso Not section.SectionInformation.IsProtected Then
            section.SectionInformation.ProtectSection(Nothing)
            section.SectionInformation.ForceSave = True
            config.Save(ConfigurationSaveMode.Full)
            ResetConfigMechanism()
        End If
    End Sub

    Public Sub UnprotectAppSettings()
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim section As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)

        If section IsNot Nothing AndAlso section.SectionInformation.IsProtected Then
            section.SectionInformation.UnprotectSection()
            section.SectionInformation.ForceSave = True
            config.Save(ConfigurationSaveMode.Full)
            ResetConfigMechanism()
        End If
    End Sub

    'Private Sub ResetConfigMechanism()
    '    Call GetType(ConfigurationManager).GetField("s_initState", BindingFlags.NonPublic Or BindingFlags.Static).SetValue(Nothing, 0)
    '    Call GetType(ConfigurationManager).GetField("s_configSystem", BindingFlags.NonPublic Or BindingFlags.Static).SetValue(Nothing, Nothing)
    '    Call GetType(ConfigurationManager).Assembly.GetTypes().First(Function(x) x.FullName = "System.Configuration.ClientConfigPaths").GetField("s_current", BindingFlags.NonPublic Or BindingFlags.Static).SetValue(Nothing, Nothing)
    'End Sub
    Private Sub ResetConfigMechanism()
        Call GetType(ConfigurationManager).GetField("s_initState", BindingFlags.NonPublic Or BindingFlags.Static).SetValue(Nothing, 0)

        Call GetType(ConfigurationManager).GetField("s_configSystem", BindingFlags.NonPublic Or BindingFlags.Static).SetValue(Nothing, Nothing)

        Call GetType(ConfigurationManager).Assembly.GetTypes().Where(Function(x) x.FullName = "System.Configuration.ClientConfigPaths").First().GetField("s_current", BindingFlags.NonPublic Or BindingFlags.Static).SetValue(Nothing, Nothing)
    End Sub
    '===================================================
    Public Sub New()

    End Sub
    Public Sub ChngConstring(ByVal conStr As String)
        ' Might need to add a reference and Imports for System.Configuration
        'Global.DentistX.My.MySettings.Default.DentistXConnectionString
        Dim config As Configuration =
            ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim section As ConnectionStringsSection = DirectCast(config.GetSection("connectionStrings"), ConnectionStringsSection)
        ' See the App.config file for the name to be used in the following
        Dim settings As ConnectionStringSettings = section.ConnectionStrings("DentistX.My.MySettings.DentistXConnectionString") '("DentistX.Settings.DentistXConnectionString") 'Project.My.MySettings.DentistXConnectionString
        'Dim settings As ConnectionStringSettings = section.ConnectionStrings("Project.My.MySettings.DentistXConnectionString") 'DentistX.Settings.DentistXConnectionString

        settings.ConnectionString = conStr
        config.Save()
        ConfigurationManager.RefreshSection("connectionStrings")
        ResetConfigMechanism()
        Try
            My.Settings.Reload()
        Catch
        End Try
    End Sub



    Public Sub ChanglastRunDate(ByVal lastRunDate As String)
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


            'Dim vaultSection As NameValueCollection = CType(ConfigurationManager.GetSection("Vault"), NameValueCollection)
            ''vaultSection.Set("Language", lang)
            'vaultSection.Item("Language") = lang
            'config.Save()
            protectedSection.Settings.Item("LastCheckDate").Value = lastRunDate
            config.Save()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'ConfigurationManager.RefreshSection("connectionStrings")
    End Sub

    'To Be Used Later
    Public Sub SetNewConnection()
        Dim config As System.Configuration.Configuration
        Dim fileMap As New ExeConfigurationFileMap()
        'Code the path of App.config
        fileMap.ExeConfigFilename = "Path of app.config"
        config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None)

        '   Sets values to config file.
        If config.HasFile() Then

            config.AppSettings.Settings.Item("ConnectionString").Value = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBase\DentistX.mdf;Integrated Security=True"
            config.Save(ConfigurationSaveMode.Modified)
            ConfigurationManager.RefreshSection("AppSettings")

        End If
    End Sub

    'To Be Used Later
    Public Function GetCurrentConn() As String
        Dim strConnString As String = ""
        'Dim config As System.Configuration.Configuration
        'Dim fileMap As New ExeConfigurationFileMap()
        ''Code the path of App.config
        'fileMap.ExeConfigFilename = "Path of app.config"
        'config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None)
        '   Sets values to config file.
        'If config.HasFile() Then

        '    strConnString = config.AppSettings.Settings.Item("ConnectionString").Value

        'End If
        strConnString = My.Settings.DentistXConnectionString.ToString 'config.Sections.Item("ConnectionString").Value


        Return strConnString
    End Function



End Class
'====================
'Dim builder As New SqlConnectionStringBuilder(settings.ConnectionString)
''builder("Data Source") = "C:\User\Documents\db1.mdb"
'builder.DataSource = "(LocalDB)\MSSQLLocalDB"
'builder.AttachDBFilename = "|DataDirectory|\DBase\DentistX.mdf"
'settings.ConnectionString = builder.ConnectionString
'config.Save()
'Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBase\DentistX.mdf;Integrated Security=True
'===================================
'Another Code
'Dim config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
'Dim connectionStringsSection = DirectCast(config.GetSection("connectionStrings"), ConnectionStringsSection)
'connectionStringsSection.ConnectionStrings("DentistXConnectionString").ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBase\DentistX.mdf;Integrated Security=True"
