Imports System.Configuration

Public Class FrmChooseConn

    Dim lang, srvr, Startup As String
    Dim lclString As String = "Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=DentistX;Integrated Security=True" ' "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBase\DentistX.mdf;Integrated Security=True"
    Dim SrvrString As String = "Data Source=.;Initial Catalog=DentistX;Integrated Security=True"
    Dim lcl012String As String = "Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DBase012\DentistX.mdf;Integrated Security=True" '
    Dim NetwrkString As String = "Data Source=" & HostName & ",1433;Initial Catalog=DentistX;Integrated Security=False;User ID=sa;Password=" & HostPass & ""

    Private Sub FrmChooseConn_Load(sender As Object, e As EventArgs) Handles Me.Load


        If DbT = "Srvr" Then
            RadioSql.Checked = True
        ElseIf DbT = "Lcl" Then
            RadioLocal.Checked = True
        ElseIf DbT = "Lcl2012" Then
            RadioLocal2012.Checked = True
        ElseIf DbT = "LclOther" Then
            RadioOther.Checked = True
            txtHostName.Text = HostName
            txtSrvrPass.Text = HostPass
        End If

    End Sub

    Public Sub ChangConstring(ByVal conStr As String)
        Try
            ' Might need to add a reference and Imports for System.Configuration
            If RadioOther.Checked = True Then
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


    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Try

            If RadioSql.Checked = True Then
                srvr = "Srvr"
                DbT = "Srvr"
            End If
            If RadioLocal.Checked = True Then
                srvr = "Lcl"
                DbT = "Lcl"
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

            ChangDB(srvr)
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
            Application.Restart()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class