Imports DevExpress.DataAccess.Native.Json

Public Class AppSettings
    Public Shared Property RememberMe As Boolean
        Get
            Return My.Settings.RememberMe
        End Get
        Set(value As Boolean)
            My.Settings.RememberMe = value
            My.Settings.Save()
        End Set
    End Property

    Public Shared Property SavedUsername As String
        Get
            Return If(My.Settings.UserName, "")
        End Get
        Set(value As String)
            My.Settings.UserName = value
            My.Settings.Save()
        End Set
    End Property

    Public Shared Property SavedPassword As String
        Get
            Return If(My.Settings.Password, "")
        End Get
        Set(value As String)
            My.Settings.Password = value
            My.Settings.Save()
        End Set
    End Property

    Public Shared Sub ClearSavedCredentials()
        My.Settings.RememberMe = False
        My.Settings.UserName = ""
        My.Settings.Password = ""
        My.Settings.Save()
    End Sub
End Class