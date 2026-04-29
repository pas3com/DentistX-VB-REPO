Imports DevExpress.XtraEditors
Imports System.Security.Cryptography
Imports System.Windows.Forms

Public Class FrmLogin
    Dim userQrs As New UsersDATA
    Dim doctorQrs As New DoctorsDATA
    Dim secQrs As New SecretariesDATA
    Dim empQrs As New EmpDATA

    Private permData As New PermissionsDATA()
    Private permItems As IEnumerable(Of PermissionItem)

    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If userQrs.GetUsersCount Then
            LoadSavedCredentials()
        Else
            FrmAddNewUser.Icon = Me.Icon
            FrmAddNewUser.ShowDialog(Me)
        End If

    End Sub

    Private Sub LoadSavedCredentials()
        If AppSettings.RememberMe AndAlso Not String.IsNullOrEmpty(AppSettings.SavedUsername) Then
            TxtUsername.Text = AppSettings.SavedUsername
            TxtPassword.Text = AppSettings.SavedPassword
            chkRememberMe.Checked = True
        Else
            chkRememberMe.Checked = False
        End If
    End Sub

    Private Sub HandleRememberMe(username As String)
        If chkRememberMe.Checked Then
            AppSettings.RememberMe = True
            AppSettings.SavedUsername = username
            AppSettings.SavedPassword = TxtPassword.Text.Trim
        Else
            AppSettings.ClearSavedCredentials()
        End If
    End Sub

    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        If ValidateInputs() Then
            Dim userRecord As Users = userQrs.GetUserByUsername(TxtUsername.Text.Trim)
            If userRecord IsNot Nothing Then
                If VerifyPassword(TxtPassword.Text, userRecord.UsSalt, userRecord.UsPassHash) Then
                    ' Set user information
                    LoggedInUserID = userRecord.UsID
                    CurrentUser = userQrs.Select_Record(userRecord)
                    LoggedInUserName = CurrentUser.UsName

                    ' Set doctor information if associated
                    If CurrentUser.DrID.HasValue Then
                        LoggedInDoctorID = CurrentUser.DrID.Value
                        CurrentDoctor = doctorQrs.GetDoctorById(CurrentUser.DrID.Value)
                        ' Log doctor association
                        MainView3.stUserNameTxt.Caption = ($"User {CurrentUser.UsName} is associated with Doctor. {CurrentDoctor.DrName}")

                    End If
                    ' Set secretary information if associated
                    If CurrentUser.SecID.HasValue Then
                        LoggedInSecID = CurrentUser.SecID.Value
                        CurrentSecretary = secQrs.GetSecretaryById(CurrentUser.SecID.Value)
                        ' Log doctor association
                        MainView3.stUserNameTxt.Caption = ($"User {CurrentUser.UsName} is associated with Secretary. {CurrentSecretary.SecName}")
                    End If
                    ' Set Employee information if associated
                    If CurrentUser.EmpID.HasValue Then
                        LoggedInEmpID = CurrentUser.EmpID.Value
                        CurrentEmp = empQrs.GetEmpById(CurrentUser.EmpID.Value)
                        ' Log doctor association
                        MainView3.stUserNameTxt.Caption = ($"User {CurrentUser.UsName} is associated with Employee. {CurrentEmp.EmpName}")
                    End If

                    If CurrentDoctor Is Nothing AndAlso CurrentSecretary Is Nothing AndAlso CurrentEmp Is Nothing Then
                        MainView3.stUserNameTxt.Caption = CurrentUser.UsName & " == No Link"
                    End If
                    'XtraMessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    permItems = permData.GetGroupPermissions(CurrentUser.GroupID)
                    Perms = New PermissionService(permItems)

                    CurrentGroup = New GroupDATA().SelectRecord(CurrentUser.GroupID)
                    FormAccessGate.RefreshAfterLogin()

                    HandleRememberMe(TxtUsername.Text.Trim)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    XtraMessageBox.Show("Invalid password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                XtraMessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub
    Private Function ValidateInputs() As Boolean
        If String.IsNullOrWhiteSpace(TxtUsername.Text) Then
            ErrorProvider1.SetError(TxtUsername, "Username required.")
            Return False
        End If
        If String.IsNullOrWhiteSpace(TxtPassword.Text) Then
            ErrorProvider1.SetError(TxtPassword, "Password required.")
            Return False
        End If
        ErrorProvider1.ClearErrors()
        Return True
    End Function

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub lnkForgotPass_Click(sender As Object, e As EventArgs) Handles lnkForgotPass.Click
        ErrorProvider1.ClearErrors()
        If String.IsNullOrWhiteSpace(TxtUsername.Text) Then
            ErrorProvider1.SetError(TxtUsername, "Username required.")
            XtraMessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtUsername.Focus()
            Exit Sub
        Else
            Dim userRecord As Users = userQrs.GetUserByUsername(TxtUsername.Text.Trim)
            If userRecord IsNot Nothing Then
                If userRecord.UsLvl <> 1 OrElse userRecord.UsGrp <> "Admins" Then
                    XtraMessageBox.Show("User is not Admin." & vbCrLf & "Refer to your Admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    TxtUsername.Focus()
                    Exit Sub
                Else
                    Dim frm As New FrmResetPassword(userRecord)
                    frm.ShowDialog(Me)

                End If
            End If
        End If
    End Sub
    Private Sub chkRememberMe_CheckedChanged(sender As Object, e As EventArgs) Handles chkRememberMe.CheckedChanged
        ' Optional: Clear saved username if user unchecks Remember Me
        If Not chkRememberMe.Checked AndAlso AppSettings.RememberMe Then
            AppSettings.ClearSavedCredentials()
        Else
            HandleRememberMe(TxtUsername.Text.Trim)
        End If

    End Sub

    '' Add keyboard shortcuts
    'Private Sub TxtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPassword.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        BtnLogin.PerformClick()
    '    End If
    'End Sub

    'Private Sub TxtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtUsername.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        TxtPassword.Focus()
    '    End If
    'End Sub
End Class




