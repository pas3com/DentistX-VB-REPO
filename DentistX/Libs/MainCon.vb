Imports System.Data.SqlClient
Imports System.Configuration
Public Class MainCon

    ' Might need to add a reference and Imports for System.Configuration
    Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Dim section As ConnectionStringsSection = DirectCast(config.GetSection("connectionStrings"), ConnectionStringsSection)
    ' See the App.config file for the name to be used in the following
    Dim settings As ConnectionStringSettings = section.ConnectionStrings("Project.My.Settings.Default.DentistXConnectionString")
    'Dim builder As New System.Data.OleDb.OleDbConnectionStringBuilder(settings.ConnectionString)
    Dim builder As New System.Data.SqlClient.SqlConnectionStringBuilder(settings.ConnectionString)

    Dim str As String
    Private Shared _ConString As String

    Public Shared Property ConString As String
        Get
            Return My.MySettingsProperty.Settings.DentistXConnectionString
        End Get
        Set(ByVal value As String)
            _ConString = value
        End Set
    End Property

    Public Sub New() '(str As String)
        'Me.str = str
        builder("Data Source") = _ConString ' "C:\User\Documents\db1.mdb"
        settings.ConnectionString = builder.ConnectionString
        config.Save()


    End Sub


    Public Sub ConnDB()

        Using connection As New SqlConnection(ConString)
            AddHandler connection.StateChange, AddressOf Me.OnConnectionStateChange
            Try
                connection.Open()
                'Do stuff..
            Catch ex As Exception
                Throw ex
            Finally
                RemoveHandler connection.StateChange, AddressOf Me.OnConnectionStateChange
            End Try
        End Using

    End Sub

    Private Sub OnConnectionStateChange(sender As Object, e As StateChangeEventArgs)

        MsgBox(e.CurrentState.ToString)
    End Sub
End Class
