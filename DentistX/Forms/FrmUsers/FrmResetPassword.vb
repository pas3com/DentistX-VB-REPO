Imports System.Security.Cryptography
Imports System.Text
Imports Dapper
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class FrmResetPassword

    Private _user As USERS
    Private username As String
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = AppIcon
    End Sub

    Public Sub New(ByVal userPassed As USERS)

        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        ' Add any initialization after the InitializeComponent() call.
        _user = userPassed
        txtUser.Text = userPassed.UsName
    End Sub
    Public Sub New(ByVal _username As String)

        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        ' Add any initialization after the InitializeComponent() call.
        username = _username
    End Sub
    Private Sub FrmResetPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LoadUsers()
    End Sub

    Private Sub LoadUsers()
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString) 'DentistXDATA.GetConnection.ConnectionString
            Dim users = conn.Query(Of USERS)("SELECT * FROM USERS").ToList()
            CmbUsers.Properties.Items.Clear()
            For Each usr In users
                CmbUsers.Properties.Items.Add(usr.UsName)
            Next
            If Not String.IsNullOrWhiteSpace(_user.UsName) Then
                CmbUsers.SelectedItem = _user
            End If
        End Using
    End Sub

    Private Sub BtnResetPassword_Click(sender As Object, e As EventArgs) Handles BtnResetPassword.Click
        'If CmbUsers.SelectedItem Is Nothing OrElse String.IsNullOrWhiteSpace(TxtNewPassword.Text) Then
        '    MessageBox.Show("Please select a user and enter a new password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Return
        'End If

        'Dim username As String = CmbUsers.SelectedItem.ToString()
        Dim UsrQrs As New UsersDATA
        Dim user = UsrQrs.GetUserByUsername(username)

        If _user Is Nothing Then
            MessageBox.Show("User not found.")
            Return
        End If

        ' Generate new salt and hash
        Dim newSalt As Byte() = Nothing
        Dim newHash As Byte() = Nothing
        GeneratePasswordHash(TxtNewPassword.Text.Trim(), newSalt, newHash)

        _user.UsSalt = newSalt
        _user.UsPassHash = newHash

        If UsrQrs.UpdatePassword(_user) Then
            MessageBox.Show("Password reset successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LogAudit("ResetPassword", "Users", _user.UsID, Nothing, "Password reset by Admin", LoggedInUserName)
            If Not String.IsNullOrWhiteSpace(_user.UsName) Then
                Me.DialogResult = DialogResult.OK
                FrmLogin.TxtPassword.Text = TxtNewPassword.Text.Trim()
                Me.Close()
            End If
        Else
            MessageBox.Show("Failed to reset password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

End Class
