Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

Public Class FrmAddNewUser

    Dim doctorQrs As New DoctorsDATA
    Dim doctorsList As List(Of Doctors)
    Dim userQrs As New UsersDATA

    Private Sub FrmAddUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Optional: initialize controls

    End Sub

    Private DrId As Integer = Nothing
    Private SecID As Integer = Nothing
    Private EmpID As Integer = Nothing
    Private groupID As Integer = Nothing
    'Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
    '    If String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse
    '      String.IsNullOrWhiteSpace(TxtPassword.Text) OrElse
    '       String.IsNullOrWhiteSpace(CboUserGroup.Text) OrElse
    '        String.IsNullOrWhiteSpace(CboUserLevel.Text) Then
    '        XtraMessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Return
    '    End If

    '    If TxtPassword.Text <> TxtConfirmPassword.Text Then
    '        XtraMessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Return
    '    End If

    '    Dim newSalt As Byte() = New Byte() {}
    '    Dim newHash As Byte() = New Byte() {}
    '    GeneratePasswordHash(TxtPassword.Text, newSalt, newHash)

    '    Dim newUser As New Users With {
    '        .UsName = TxtUsername.Text.Trim,
    '        .UsLvl = CboUserLevel.Text.Trim,
    '        .UsGrp = CboUserGroup.Text.Trim,
    '        .UsSalt = newSalt,
    '        .UsPassHash = newHash,
    '        .DrID = CboDoctor.DrID 'doctorQrs.GetDoctorIdByName(CboDoctor.DrName)
    '    }

    '    Dim userQrs As New UsersDATA()
    '    If userQrs.Add(newUser) Then
    '        XtraMessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Me.Close()
    '    Else
    '        XtraMessageBox.Show("Failed to add user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End If

    'End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        ' === 1. Validation ===
        If String.IsNullOrWhiteSpace(TxtUsername.Text) OrElse
       String.IsNullOrWhiteSpace(TxtPassword.Text) OrElse
       String.IsNullOrWhiteSpace(CboUserGroup.GroupName) OrElse
       String.IsNullOrWhiteSpace(CboUserLevel.Text) Then

            XtraMessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If TxtPassword.Text <> TxtConfirmPassword.Text Then
            XtraMessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' === 2. Hash password ===
        Dim newSalt As Byte() = Nothing
        Dim newHash As Byte() = Nothing
        GeneratePasswordHash(TxtPassword.Text, newSalt, newHash)

        ' === 3. Determine GroupID ===


        ' === 4. Conditional doctor/secretary linking ===
        Dim drIDValue As Integer? = Nothing
        Dim secIDValue As Integer? = Nothing
        Dim empIDValue As Integer? = Nothing

        Select Case groupID
            Case 1 ' Admins
                ' Handle Admin creation with flexible linking options
                HandleAdminCreation(DrId, SecID, EmpID, drIDValue, secIDValue, empIDValue)

            Case 2 ' Doctors
                If DrId <= 0 Then
                    XtraMessageBox.Show("Please select a doctor.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                drIDValue = DrId
                secIDValue = Nothing
                empIDValue = Nothing

            Case 3 ' Secretaries
                If SecID <= 0 Then
                    XtraMessageBox.Show("Please select a secretary.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                secIDValue = SecID
                drIDValue = Nothing
                empIDValue = Nothing

            Case 4 ' Employees
                If EmpID <= 0 Then
                    XtraMessageBox.Show("Please select an employee.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                empIDValue = EmpID
                drIDValue = Nothing
                secIDValue = Nothing

            Case Else
                XtraMessageBox.Show("Invalid user group selected.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
        End Select



        'Select Case groupID
        '    Case 1 ' Admins
        '        If DrId <= 0 Then
        '            Dim result = XtraMessageBox.Show("Create ADMIN without linking to a doctor.", "Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        '            If result = DialogResult.No Then
        '                Return
        '            ElseIf result = DialogResult.Yes Then
        '                drIDValue = Nothing
        '                secIDValue = Nothing
        '                empIDValue = Nothing
        '            End If
        '        ElseIf SecID <= 0 Then

        '            drIDValue = Nothing
        '            secIDValue = SecID
        '            empIDValue = Nothing
        '        ElseIf EmpID <= 0 Then
        '            drIDValue = Nothing
        '            secIDValue = Nothing
        '            empIDValue = EmpID
        '        End If

        '    Case 2 ' Doctors
        '        If DrId = 0 Then
        '            XtraMessageBox.Show("Please select a doctor.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '            Return
        '        End If
        '        drIDValue = DrId
        '        secIDValue = Nothing
        '        empIDValue = Nothing
        '    Case 3 ' Secretaries
        '        If SecID = 0 Then
        '            XtraMessageBox.Show("Please select a secretary.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '            Return
        '        End If
        '        secIDValue = SecID
        '        drIDValue = Nothing
        '        empIDValue = Nothing
        '    Case 4 ' Employees
        '        If EmpID = 0 Then
        '            XtraMessageBox.Show("Please select am employee.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '            Return
        '        End If
        '        empIDValue = EmpID
        '        drIDValue = Nothing
        '        secIDValue = Nothing
        'End Select

        ' === 5. Create new user object ===
        Dim newUser As New USERS
        Select Case RadioLink.SelectedIndex
            Case 0
                Dim new_User As New USERS With {
                      .UsName = TxtUsername.Text.Trim(),
                      .UsLvl = Convert.ToInt32(CboUserLevel.Text),
                      .UsGrp = CboUserGroup.GroupName.Trim(),
                      .GroupID = groupID,
                      .UsSalt = newSalt,
                      .UsPassHash = newHash,
                      .DrID = drIDValue,
                      .SecID = secIDValue,
                      .EmpID = empIDValue
                  }
                newUser = new_User
            Case 1
                Dim new_User As New USERS With {
                       .UsName = TxtUsername.Text.Trim(),
                       .UsLvl = Convert.ToInt32(CboUserLevel.Text),
                       .UsGrp = CboUserGroup.GroupName.Trim(),
                       .GroupID = groupID,
                       .UsSalt = newSalt,
                       .UsPassHash = newHash,
                       .DrID = drIDValue,
                       .SecID = secIDValue,
                       .EmpID = empIDValue
                   }
                newUser = new_User
            Case 2
                Dim new_User As New USERS With {
                      .UsName = TxtUsername.Text.Trim(),
                      .UsLvl = Convert.ToInt32(CboUserLevel.Text),
                      .UsGrp = CboUserGroup.GroupName.Trim(),
                      .GroupID = groupID,
                      .UsSalt = newSalt,
                      .UsPassHash = newHash,
                      .DrID = drIDValue,
                      .SecID = secIDValue,
                      .EmpID = empIDValue
                  }
                newUser = new_User
            Case 3
                Dim new_User As New USERS With {
                                      .UsName = TxtUsername.Text.Trim(),
                                      .UsLvl = Convert.ToInt32(CboUserLevel.Text),
                                      .UsGrp = CboUserGroup.GroupName.Trim(),
                                      .GroupID = groupID,
                                      .UsSalt = newSalt,
                                      .UsPassHash = newHash,
                                      .DrID = drIDValue,
                                      .SecID = secIDValue,
                                      .EmpID = empIDValue
                                  }
                newUser = new_User

        End Select





        ' === 6. Save to database ===
        Dim userQrs As New UsersDATA()
        Try
            If userQrs.Add(newUser) Then
                XtraMessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                XtraMessageBox.Show("Failed to add user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Error while saving user: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Helper method for Admin creation logic
    Private Sub HandleAdminCreation(ByVal DrId As Integer, ByVal SecID As Integer, ByVal EmpID As Integer,
                               ByRef drIDValue As Integer?, ByRef secIDValue As Integer?, ByRef empIDValue As Integer?)
        ' Count how many links are selected
        Dim linksCount As Integer = 0
        If DrId > 0 Then linksCount += 1
        If SecID > 0 Then linksCount += 1
        If EmpID > 0 Then linksCount += 1

        Select Case linksCount
            Case 0 ' No links - standalone admin
                Dim result = XtraMessageBox.Show("Create ADMIN without any links (standalone administrator)?",
                                           "Create Standalone Admin",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    drIDValue = Nothing
                    secIDValue = Nothing
                    empIDValue = Nothing
                Else
                    ' User cancelled or wants to select links
                    Return
                End If

            Case 1 ' Single link
                Dim linkType As String = ""
                If DrId > 0 Then linkType = "doctor"
                If SecID > 0 Then linkType = "secretary"
                If EmpID > 0 Then linkType = "employee"

                Dim result = XtraMessageBox.Show($"Create ADMIN linked to a {linkType}?",
                                           "Create Linked Admin",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    drIDValue = If(DrId > 0, DrId, Nothing)
                    secIDValue = If(SecID > 0, SecID, Nothing)
                    empIDValue = If(EmpID > 0, EmpID, Nothing)
                Else
                    Return
                End If

            Case Else ' Multiple links
                Dim message As New StringBuilder("Multiple links selected for Admin:")
                If DrId > 0 Then message.AppendLine($"• Doctor ID: {DrId}")
                If SecID > 0 Then message.AppendLine($"• Secretary ID: {SecID}")
                If EmpID > 0 Then message.AppendLine($"• Employee ID: {EmpID}")
                message.AppendLine()
                message.AppendLine("Do you want to:")
                message.AppendLine("1. Create with all selected links")
                message.AppendLine("2. Create without any links")
                message.AppendLine("3. Cancel and select different links")

                Dim result = XtraMessageBox.Show(message.ToString(),
                                           "Multiple Links Detected",
                                           MessageBoxButtons.YesNoCancel,
                                           MessageBoxIcon.Question)

                Select Case result
                    Case DialogResult.Yes
                        ' Create with all links
                        drIDValue = If(DrId > 0, DrId, Nothing)
                        secIDValue = If(SecID > 0, SecID, Nothing)
                        empIDValue = If(EmpID > 0, EmpID, Nothing)
                    Case DialogResult.No
                        ' Create without any links
                        drIDValue = Nothing
                        secIDValue = Nothing
                        empIDValue = Nothing
                    Case DialogResult.Cancel
                        Return ' User cancelled
                End Select
        End Select
    End Sub




    Private Sub CboUserGroup_GroupsValueChanged(sender As Object, e As GroupsCombo.GroupsIndexChangedEvent) Handles CboUserGroup.GroupsValueChanged
        groupID = e.GroupID
        If groupID = 1 Then
            LabelDoctor.Visible = False
            CboDoctor.Visible = False
            LabelSec.Visible = False
            CboSec.Visible = False
            LabelEmp.Visible = False
            CboEmps.Visible = False
        ElseIf groupID = 2 Then
            LabelDoctor.Visible = True
            CboDoctor.Visible = True
            LabelSec.Visible = False
            CboSec.Visible = False
            LabelEmp.Visible = False
            CboEmps.Visible = False
        ElseIf groupID = 3 Then
            LabelDoctor.Visible = False
            CboDoctor.Visible = False
            LabelSec.Visible = True
            CboSec.Visible = True
            LabelEmp.Visible = False
            CboEmps.Visible = False
        ElseIf groupID = 4 Then
            LabelDoctor.Visible = False
            CboDoctor.Visible = False
            LabelSec.Visible = False
            CboSec.Visible = False
            LabelEmp.Visible = True
            CboEmps.Visible = True
        End If
    End Sub
    Private Sub CboDoctor_DoctorsValueChanged(sender As Object, e As DoctorsCombo.DoctorsIndexChangedEvent) Handles CboDoctor.DoctorsValueChanged
        DrId = e.DrID
    End Sub
    Private Sub CboSec1_SecretariesValueChanged(sender As Object, e As SecretariesCombo.SecretariesIndexChangedEvent) Handles CboSec.SecretariesValueChanged
        SecID = e.SecID
    End Sub
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub


    Private Sub CboEmps_EmpsValueChanged(sender As Object, e As EmpsCombo.EmpIndexChangedEvent) Handles CboEmps.EmpsValueChanged
        EmpID = e.EmpID
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
            CboUserGroup.SetSelectedGroupID(1)
        ElseIf CboUserLevel.SelectedIndex = 1 Then
            CboUserGroup.SetSelectedGroupID(2)
            RadioLink.SelectedIndex = 1
        ElseIf CboUserLevel.SelectedIndex = 2 Then
            CboUserGroup.SetSelectedGroupID(3)
            RadioLink.SelectedIndex = 2
        ElseIf CboUserLevel.SelectedIndex = 3 Then
            CboUserGroup.SetSelectedGroupID(4)
            RadioLink.SelectedIndex = 3
        Else
            CboUserGroup.SetSelectedGroupID(1)
            RadioLink.SelectedIndex = 0
        End If
    End Sub


End Class
