Imports System.Security.Cryptography
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

Public Class FrmLockScreen

    Public Property CurrentUser As Users
    Private Sub BtnUnlock_Click(sender As Object, e As EventArgs) Handles BtnUnlock.Click
        If TxtPassword.Text.Length < 3 Then
            XtraMessageBox.Show("Incorrect password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtPassword.SelectAll()
            TxtPassword.Focus()
            Exit Sub
        End If

        If VerifyPassword(TxtPassword.Text, CurrentUser.UsSalt, CurrentUser.UsPassHash) Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            XtraMessageBox.Show("Incorrect password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtPassword.SelectAll()
            TxtPassword.Focus()
        End If
    End Sub

    Private Sub FrmLockScreen_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Icon = AppIcon
        If CurrentUser IsNot Nothing Then
            lblUserName.Text = CurrentUser.UsName
        End If

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Application.Exit()
    End Sub

    'Private Function VerifyPassword(inputPassword As String, salt As Byte(), storedHash As Byte()) As Boolean
    '    Dim inputHash As Byte()
    '    Using deriveBytes As New Rfc2898DeriveBytes(inputPassword, salt, 10000)
    '        inputHash = deriveBytes.GetBytes(32)
    '    End Using
    '    Return inputHash.SequenceEqual(storedHash)
    'End Function

End Class
