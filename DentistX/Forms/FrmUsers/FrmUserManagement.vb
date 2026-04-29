Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

Public Class FrmUserManagement
    Dim doctorQrs As New DoctorsDATA
    Dim doctorsList As List(Of Doctors)
    Dim userQrs As New UsersDATA
    Private Sub FrmUserManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Populate ComboBox Levels & Groups
        CboUserLevel.Properties.Items.AddRange({1, 2, 3}) ' 1=Admin, 2=Doctor, 3=Receptionist
        CboUserGroup.Properties.Items.AddRange({"Admin", "Doctor", "Receptionist"})
        Me.Icon = AppIcon
        ' Load doctors for association
        LoadDoctors()
    End Sub

    Private Sub LoadDoctors()
        doctorsList = doctorQrs.SelectAll()
        CboDoctor.Properties.Items.Clear()
        CboDoctor.Properties.Items.Add("(No Doctor Association)")

        For Each doc In doctorsList
            CboDoctor.Properties.Items.Add(doc.DrName)
        Next
        CboDoctor.SelectedIndex = 0
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse
           String.IsNullOrWhiteSpace(TxtPassword.Text) OrElse
           String.IsNullOrWhiteSpace(TxtConfirmPassword.Text) Then
            XtraMessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If TxtPassword.Text <> TxtConfirmPassword.Text Then
            XtraMessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim newSalt As Byte() = New Byte() {}
        Dim newHash As Byte() = New Byte() {}
        GeneratePasswordHash(TxtPassword.Text, newSalt, newHash)

        ' Determine Doctor ID based on selection
        Dim selectedDoctorId As Integer? = Nothing
        If CboDoctor.SelectedIndex > 0 Then
            selectedDoctorId = doctorsList(CboDoctor.SelectedIndex - 1).DrID
        End If

        Dim newUser As New Users With {
            .UsName = TxtUsername.Text.Trim,
            .UsLvl = CInt(CboUserLevel.Text.Trim),
            .UsGrp = CboUserGroup.Text.Trim,
            .UsSalt = newSalt,
            .UsPassHash = newHash,
            .DrID = selectedDoctorId
        }

        Dim userQrs As New UsersDATA()
        If userQrs.Add(newUser) Then
            XtraMessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Else
            XtraMessageBox.Show("Failed to add user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        ' Example: Updating an existing user
        If String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse
           String.IsNullOrWhiteSpace(TxtPassword.Text) OrElse
           String.IsNullOrWhiteSpace(TxtConfirmPassword.Text) Then
            XtraMessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If TxtPassword.Text <> TxtConfirmPassword.Text Then
            XtraMessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim UsrQrs As New UsersDATA
        Dim oldUser = UsrQrs.GetUserByUsername(TxtUsername.Text)

        Dim newSalt As Byte() = New Byte() {}
        Dim newHash As Byte() = New Byte() {}
        GeneratePasswordHash(TxtPassword.Text, newSalt, newHash)

        ' Determine Doctor ID based on selection
        Dim selectedDoctorId As Integer? = Nothing
        If CboDoctor.SelectedIndex > 0 Then
            selectedDoctorId = doctorsList(CboDoctor.SelectedIndex - 1).DrID
        End If

        Dim newUser As New Users With {
            .UsName = TxtUsername.Text.Trim,
            .UsLvl = CInt(CboUserLevel.Text.Trim),
            .UsGrp = CboUserGroup.Text.Trim,
            .UsSalt = newSalt,
            .UsPassHash = newHash,
            .DrID = selectedDoctorId
        }

        If userQrs.Update(oldUser, newUser) Then
            XtraMessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Else
            XtraMessageBox.Show("Failed to update user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Update the form designer to include CboDoctor ComboBox
    ' Add this to your form design:
    ' - Label: "Associated Doctor"
    ' - ComboBoxEdit: Name = "CboDoctor"

    Private Sub CboUserLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboUserLevel.SelectedIndexChanged
        ' Auto-set user group based on level
        If CboUserLevel.SelectedIndex >= 0 Then
            Dim level = CInt(CboUserLevel.Text)
            Select Case level
                Case 1
                    CboUserGroup.Text = "Admin"
                Case 2
                    CboUserGroup.Text = "Doctor"
                Case 3
                    CboUserGroup.Text = "Receptionist"
            End Select

            ' Show doctor selection only for doctor users
            CboDoctor.Visible = (level = 2)
            LabelDoctor.Visible = (level = 2)
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Function ValidateInputs() As Boolean
        If String.IsNullOrWhiteSpace(TxtUsername.Text) Then
            ErrorProvider1.SetError(TxtUsername, "Username is required.")
            Return False
        End If
        If String.IsNullOrWhiteSpace(TxtPassword.Text) Then
            ErrorProvider1.SetError(TxtPassword, "Password is required.")
            Return False
        End If
        If TxtPassword.Text <> TxtConfirmPassword.Text Then
            ErrorProvider1.SetError(TxtConfirmPassword, "Passwords do not match.")
            Return False
        End If
        If CboUserLevel.SelectedIndex = -1 Then
            ErrorProvider1.SetError(CboUserLevel, "Please select a user level.")
            Return False
        End If
        If CboUserGroup.SelectedIndex = -1 Then
            ErrorProvider1.SetError(CboUserGroup, "Please select a user group.")
            Return False
        End If

        ' Validate doctor association for doctor users
        If CInt(CboUserLevel.Text) = 2 AndAlso CboDoctor.SelectedIndex = -1 Then
            ErrorProvider1.SetError(CboDoctor, "Doctor users must be associated with a doctor.")
            Return False
        End If

        ErrorProvider1.ClearErrors()
        Return True
    End Function

    Private Sub ClearInputs()
        TxtUsername.Text = ""
        TxtPassword.Text = ""
        TxtConfirmPassword.Text = ""
        CboUserLevel.SelectedIndex = -1
        CboUserGroup.SelectedIndex = -1
        CboDoctor.SelectedIndex = 0
    End Sub

    Private Sub BtnResetPassword_Click(sender As Object, e As EventArgs)
        ' Reset password logic here
    End Sub
End Class

'Public Class FrmUserManagement

'    Private Sub FrmUserManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        ' Populate ComboBox Levels & Groups
'        CboUserLevel.Properties.Items.AddRange({1, 2, 3}) '({"Admin", "Manager", "User"})
'        CboUserGroup.Properties.Items.AddRange({"Admin", "Manager", "User"}) '({"Group A", "Group B", "Group C"})
'    End Sub

'    Private Sub BtnSave_Click1(sender As Object, e As EventArgs) Handles BtnSave.Click
'        If ValidateInputs() Then
'            Dim newUser As New Users()
'            newUser.UsName = TxtUsername.Text.Trim

'            ' Generate salt and hash
'            Dim salt As Byte() = GenerateSalt()
'            Dim hash As Byte() = HashPassword(TxtPassword.Text, salt)

'            newUser.UsPassHash = hash
'            newUser.UsSalt = salt
'            newUser.UsLvl = CboUserLevel.SelectedIndex ' Assuming 0 = Admin, 1 = Manager, 2 = User
'            newUser.UsGrp = CboUserGroup.SelectedIndex

'            Dim userQrs As New UsersDATA() ' Your DataAccess class
'            If userQrs.Add(newUser) Then
'                XtraMessageBox.Show("User created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
'                ClearInputs()
'            Else
'                XtraMessageBox.Show("Error adding user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            End If
'        End If
'    End Sub

'    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
'        If String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse
'           String.IsNullOrWhiteSpace(TxtPassword.Text) OrElse
'           String.IsNullOrWhiteSpace(TxtConfirmPassword.Text) Then
'            XtraMessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'            Return
'        End If

'        If TxtPassword.Text <> TxtConfirmPassword.Text Then
'            XtraMessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'            Return
'        End If

'        Dim newSalt As Byte() = New Byte() {}
'        Dim newHash As Byte() = New Byte() {}
'        GeneratePasswordHash(TxtPassword.Text, newSalt, newHash)

'        Dim newUser As New Users With {
'            .UsName = TxtUsername.Text.Trim,
'            .UsLvl = CboUserLevel.Text.Trim,
'            .UsGrp = CboUserGroup.Text.Trim,
'            .UsSalt = newSalt,
'            .UsPassHash = newHash
'        }

'        Dim userQrs As New UsersDATA()
'        If userQrs.Add(newUser) Then
'            XtraMessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
'            Me.Close()
'        Else
'            XtraMessageBox.Show("Failed to add user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End If

'    End Sub
'    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
'        ' Example: Updating an existing user
'        ' You would need the old user object and the modified user object
'        If String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse
'           String.IsNullOrWhiteSpace(TxtPassword.Text) OrElse
'           String.IsNullOrWhiteSpace(TxtConfirmPassword.Text) Then
'            XtraMessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'            Return
'        End If

'        If TxtPassword.Text <> TxtConfirmPassword.Text Then
'            XtraMessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'            Return
'        End If

'        Dim UsrQrs As New UsersDATA
'        Dim oldUser = UsrQrs.GetUserByUsername(TxtUsername.Text)

'        Dim newSalt As Byte() = New Byte() {}
'        Dim newHash As Byte() = New Byte() {}
'        GeneratePasswordHash(TxtPassword.Text, newSalt, newHash)

'        Dim newUser As New Users With {
'            .UsName = TxtUsername.Text.Trim,
'            .UsLvl = CboUserLevel.Text.Trim,
'            .UsGrp = CboUserGroup.Text.Trim,
'            .UsSalt = newSalt,
'            .UsPassHash = newHash
'        }

'        Dim userQrs As New UsersDATA()
'        If userQrs.Update(oldUser, newUser) Then
'            XtraMessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
'            Me.Close()
'        Else
'            XtraMessageBox.Show("Failed to add user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End If
'        XtraMessageBox.Show("Update function needs old and new user objects!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
'    End Sub

'    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
'        Me.Close()
'    End Sub

'    Private Function ValidateInputs() As Boolean
'        If String.IsNullOrWhiteSpace(TxtUsername.Text) Then
'            ErrorProvider1.SetError(TxtUsername, "Username is required.")
'            Return False
'        End If
'        If String.IsNullOrWhiteSpace(TxtPassword.Text) Then
'            ErrorProvider1.SetError(TxtPassword, "Password is required.")
'            Return False
'        End If
'        If TxtPassword.Text <> TxtConfirmPassword.Text Then
'            ErrorProvider1.SetError(TxtConfirmPassword, "Passwords do not match.")
'            Return False
'        End If
'        If CboUserLevel.SelectedIndex = -1 Then
'            ErrorProvider1.SetError(CboUserLevel, "Please select a user level.")
'            Return False
'        End If
'        If CboUserGroup.SelectedIndex = -1 Then
'            ErrorProvider1.SetError(CboUserGroup, "Please select a user group.")
'            Return False
'        End If
'        ErrorProvider1.ClearErrors()
'        Return True
'    End Function

'    Private Sub ClearInputs()
'        TxtUsername.Text = ""
'        TxtPassword.Text = ""
'        TxtConfirmPassword.Text = ""
'        CboUserLevel.SelectedIndex = -1
'        CboUserGroup.SelectedIndex = -1
'    End Sub

'    Private Sub BtnResetPassword_Click(sender As Object, e As EventArgs)

'    End Sub
'End Class
