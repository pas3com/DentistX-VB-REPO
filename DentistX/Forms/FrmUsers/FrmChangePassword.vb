Imports DevExpress.XtraEditors
Imports System.Security.Cryptography
Imports System.Windows.Forms

Public Class FrmChangePassword

    Public Property CurrentUser As USERS


    'Public Sub New(ByVal userPassed As USERS)

    '    ' This call is required by the designer.
    '    InitializeComponent()
    '    Me.Icon = AppIcon
    '    ' Add any initialization after the InitializeComponent() call.
    '    _user = userPassed
    'End Sub

    Private Sub FrmChangePassword_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtUser.Text = Me.CurrentUser.UsName
    End Sub



    Private Sub BtnChangePassword_Click(sender As Object, e As EventArgs) Handles BtnChangePassword.Click
        If ValidateInputs() Then
            Dim usrQrs As New UsersDATA

            ' Verify old password
            If Not VerifyPassword(TxtOldPassword.Text, CurrentUser.UsSalt, CurrentUser.UsPassHash) Then
                XtraMessageBox.Show("Old password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Generate new salt + hash
            Dim newSalt As Byte() = New Byte() {}
            Dim newHash As Byte() = New Byte() {}
            GeneratePasswordHash(TxtNewPassword.Text, newSalt, newHash)


            ' Update database
            CurrentUser.UsSalt = newSalt
            CurrentUser.UsPassHash = newHash

            If usrQrs.UpdatePassword(CurrentUser) Then
                XtraMessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                XtraMessageBox.Show("Failed to change password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Function ValidateInputs() As Boolean
        If String.IsNullOrWhiteSpace(TxtOldPassword.Text) Then
            ErrorProvider1.SetError(TxtOldPassword, "Old password required.")
            Return False
        End If
        If String.IsNullOrWhiteSpace(TxtNewPassword.Text) Then
            ErrorProvider1.SetError(TxtNewPassword, "New password required.")
            Return False
        End If
        If TxtNewPassword.Text <> TxtConfirmPassword.Text Then
            ErrorProvider1.SetError(TxtConfirmPassword, "Passwords do not match.")
            Return False
        End If
        ErrorProvider1.ClearErrors()
        Return True
    End Function

    Private Function VerifyPassword1(inputPassword As String, salt As Byte(), storedHash As Byte()) As Boolean
        Dim inputHash As Byte()
        Using deriveBytes As New Rfc2898DeriveBytes(inputPassword, salt, 10000)
            inputHash = deriveBytes.GetBytes(32)
        End Using
        Return inputHash.SequenceEqual(storedHash)
    End Function

    Private Sub GeneratePasswordHash1(password As String, ByRef salt As Byte(), ByRef hash As Byte())
        Using rng As New RNGCryptoServiceProvider()
            salt = New Byte(15) {}
            rng.GetBytes(salt)
        End Using
        Using deriveBytes As New Rfc2898DeriveBytes(password, salt, 10000)
            hash = deriveBytes.GetBytes(32)
        End Using
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub


End Class
