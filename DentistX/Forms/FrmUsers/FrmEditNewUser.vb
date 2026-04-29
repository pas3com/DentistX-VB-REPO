Imports System.Windows.Forms
Imports DevExpress.XtraDiagram.Bars
Imports DevExpress.XtraEditors

Public Class FrmEditNewUser

    Private editUser As USERS
    Dim doctorQrs As New DoctorsDATA
    Dim doctorsList As List(Of Doctors)
    Dim userQrs As New UsersDATA
    Private grpID As Integer
    Dim secQrs As New SecretariesDATA
    Dim empQrs As New EmpDATA
    Public Sub New(UserToEdit As USERS)
        InitializeComponent()
        editUser = UserToEdit
    End Sub



    Private Sub FrmEditUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        ' Populate ComboBox Levels & Groups
        CboUserLevel.Properties.Items.AddRange({1, 2, 3, 4, 5}) '({"Admin", "Manager", "User"})
        'CboUserGroup.Properties.Items.AddRange({"Admins", "Doctors", "Secretary"}) '({"Group A", "Group B", "Group C"}Admins, Doctors, Secretary)
        CboUserLevel.SelectedIndex = -1
        CboEmps.CboEmps.SelectedIndex = -1
        CboDoctor.CboDoctors.SelectedIndex = -1
        CboSec.CboSecretaries.SelectedIndex = -1


        TxtUsername.Text = editUser.UsName

        SetComboBoxTextWithSelection(CboUserLevel, editUser.UsLvl)
        'CboUserLevel.Text = editUser.UsLvl

        'CboUserGroup.Text = editUser.UsGrp
        CboUserGroup.SetSelectedGroupName(editUser.UsGrp)
        If editUser IsNot Nothing AndAlso editUser.DrID.HasValue Then
            'CboDoctor.Text = doctorQrs.GetDoctorById(editUser.DrID).DrName
            CboDoctor.SetSelectedDrName(editUser.DrID)
        ElseIf editUser IsNot Nothing AndAlso editUser.SecID.HasValue Then
            'CboSec.Text = secQrs.GetSecretaryById(editUser.SecID).SecName
            CboSec.SetSelectedSecName(editUser.SecID)
        ElseIf editUser IsNot Nothing AndAlso editUser.EmpID.HasValue Then
            'CboEmps.Text = empQrs.GetEmpById(editUser.EmpID).EmpName
            CboEmps.SetSelectedEmpName(editUser.EmpID)
        End If

        Me.Icon = AppIcon
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse
           String.IsNullOrWhiteSpace(CboUserGroup.GroupName) OrElse
            String.IsNullOrWhiteSpace(CboUserLevel.Text) Then
            XtraMessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '          String.IsNullOrWhiteSpace(TxtPassword.Text) OrElse
            '   OrElse
            'String.IsNullOrWhiteSpace(TxtConfirmPassword.Text)
            Return
        End If
        If RadioLink.SelectedIndex = 1 Then
            If String.IsNullOrWhiteSpace(CboDoctor.DrName) Then
                XtraMessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        ElseIf RadioLink.SelectedIndex = 2 Then
            If String.IsNullOrWhiteSpace(CboSec.SecName) Then
                XtraMessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        ElseIf RadioLink.SelectedIndex = 3 Then
            If String.IsNullOrWhiteSpace(CboEmps.EmpName) Then
                XtraMessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If


        'If TxtPassword.Text <> TxtConfirmPassword.Text Then
        '    XtraMessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Return
        'End If



        If CboUserGroup.GroupName.Length > 0 Then

            grpID = CboUserGroup.GroupID
        Else
            XtraMessageBox.Show("Please select a valid group.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        'Dim newSalt As Byte() = New Byte() {}
        'Dim newHash As Byte() = New Byte() {}
        'GeneratePasswordHash(TxtPassword.Text, newSalt, newHash)

        Dim newUser As New USERS
        Select Case RadioLink.SelectedIndex
            Case 0
                Dim new_User As New USERS With {
                .UsID = editUser.UsID,
                .UsName = TxtUsername.Text.Trim,
                .UsLvl = CboUserLevel.Text.Trim,
                .UsGrp = CboUserGroup.Text.Trim,
                .GroupID = grpID,
                .EmpID = Nothing,
                .SecID = Nothing,
                .DrID = Nothing
            }
                newUser = new_User
            Case 1
                Dim new_User As New USERS With {
                .UsID = editUser.UsID,
                .UsName = TxtUsername.Text.Trim,
                .UsLvl = CboUserLevel.Text.Trim,
                .UsGrp = CboUserGroup.Text.Trim,
                .GroupID = grpID,
                .DrID = doctorQrs.GetDoctorIdByName(CboDoctor.DrName),
                .EmpID = Nothing,
                .SecID = Nothing
            }
                newUser = new_User
            Case 2
                Dim new_User As New USERS With {
                .UsID = editUser.UsID,
                .UsName = TxtUsername.Text.Trim,
                .UsLvl = CboUserLevel.Text.Trim,
                .UsGrp = CboUserGroup.Text.Trim,
                .GroupID = grpID,
                .DrID = Nothing,
                .SecID = secQrs.GetSecretaryByName(CboSec.SecName),
                .EmpID = Nothing
            }
                newUser = new_User
            Case 3
                Dim new_User As New USERS With {
                .UsID = editUser.UsID,
                .UsName = TxtUsername.Text.Trim,
                .UsLvl = CboUserLevel.Text.Trim,
                .UsGrp = CboUserGroup.Text.Trim,
                .GroupID = grpID,
                .DrID = Nothing,
                .SecID = Nothing,
                .EmpID = empQrs.GetEmpIdByName(CboEmps.EmpName)
            }
                newUser = new_User
        End Select

        '.UsSalt = newSalt,
        '.UsPassHash = newHash,
        Try
            Dim userQrs As New UsersDATA()
            If userQrs.Update(editUser, newUser) Then
                XtraMessageBox.Show("User Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                XtraMessageBox.Show("Failed to Updated user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            XtraMessageBox.Show($"Failed to Updated user. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub
    Private Sub CboUserGroup_GroupsValueChanged(sender As Object, e As GroupsCombo.GroupsIndexChangedEvent) Handles CboUserGroup.GroupsValueChanged
        If CboUserGroup.CboGroups.SelectedIndex >= 0 Then
            grpID = CboUserGroup.GroupID
            If grpID = 1 Then
                LabelDoctor.Visible = False
                CboDoctor.Visible = False
                LabelSec.Visible = False
                CboSec.Visible = False
                LabelEmp.Visible = False
                CboEmps.Visible = False
            ElseIf grpID = 2 Then
                LabelDoctor.Visible = True
                CboDoctor.Visible = True
                LabelSec.Visible = False
                CboSec.Visible = False
                LabelEmp.Visible = False
                CboEmps.Visible = False
            ElseIf grpID = 3 Then
                LabelDoctor.Visible = False
                CboDoctor.Visible = False
                LabelSec.Visible = True
                CboSec.Visible = True
                LabelEmp.Visible = False
                CboEmps.Visible = False
            ElseIf grpID = 4 Then
                LabelDoctor.Visible = False
                CboDoctor.Visible = False
                LabelSec.Visible = False
                CboSec.Visible = False
                LabelEmp.Visible = True
                CboEmps.Visible = True
            End If
        Else
            XtraMessageBox.Show("Please select a valid group.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

    End Sub

    'currentUser.UsName = TxtUsername.Text.Trim
    'currentUser.UsLvl = CboUserLevel.Text.Trim
    'currentUser.UsGrp = CboUserGroup.Text.Trim

    'If Not String.IsNullOrWhiteSpace(TxtPassword.Text) Then
    '    Dim newSalt As Byte() = New Byte() {}
    '    Dim newHash As Byte() = New Byte() {}

    '    GeneratePasswordHash(TxtPassword.Text, newSalt, newHash)
    '    currentUser.UsSalt = newSalt
    '    currentUser.UsPassHash = newHash
    'End If

    'Dim userQrs As New UsersDATA()
    'If userQrs.Update(selectedUser, currentUser) Then
    '    XtraMessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Me.Close()
    'Else
    '    XtraMessageBox.Show("Failed to update user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    'End If
    '==========================

    Public Sub SetComboBoxTextWithSelection(comboBox As ComboBoxEdit, text As String)
        ' First set the text
        comboBox.Text = text

        ' Force selection by finding matching item
        For i As Integer = 0 To comboBox.Properties.Items.Count - 1
            If comboBox.Properties.Items(i).ToString() = text Then
                comboBox.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

    ' Usage
    'SetComboBoxTextWithSelection(CboUserGroup, editUser.UsGrp)

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub RadioLink_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioLink.SelectedIndexChanged
        If RadioLink.SelectedIndex = 0 Then
            LabelDoctor.Visible = False
            CboDoctor.Visible = False
            LabelSec.Visible = False
            CboSec.Visible = False
            LabelEmp.Visible = False
            CboEmps.Visible = False
        ElseIf RadioLink.SelectedIndex = 1 Then
            LabelDoctor.Visible = True
            CboDoctor.Visible = True
            LabelSec.Visible = False
            CboSec.Visible = False
            LabelEmp.Visible = False
            CboEmps.Visible = False
        ElseIf RadioLink.SelectedIndex = 2 Then
            LabelDoctor.Visible = False
            CboDoctor.Visible = False
            LabelSec.Visible = True
            CboSec.Visible = True
            LabelEmp.Visible = False
            CboEmps.Visible = False
        ElseIf RadioLink.SelectedIndex = 3 Then
            LabelDoctor.Visible = False
            CboDoctor.Visible = False
            LabelSec.Visible = False
            CboSec.Visible = False
            LabelEmp.Visible = True
            CboEmps.Visible = True
        End If
    End Sub
    Private Sub CboUserLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboUserLevel.SelectedIndexChanged
        If CboUserLevel.SelectedIndex = 0 Then
            RadioLink.SelectedIndex = 0
            CboUserGroup.SetSelectedGroupName("Admins")
        ElseIf CboUserLevel.SelectedIndex = 1 Then
            CboUserGroup.SetSelectedGroupName("Doctors")
            RadioLink.SelectedIndex = 1
        ElseIf CboUserLevel.SelectedIndex = 2 Then
            CboUserGroup.SetSelectedGroupName("Secretaries")
            RadioLink.SelectedIndex = 2
        ElseIf CboUserLevel.SelectedIndex = 3 Then
            CboUserGroup.SetSelectedGroupName("Employees")
            RadioLink.SelectedIndex = 3
        Else
            CboUserGroup.SetSelectedGroupName("Admins")
            RadioLink.SelectedIndex = 0
        End If
    End Sub

End Class
