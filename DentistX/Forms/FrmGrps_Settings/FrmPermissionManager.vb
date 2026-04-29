Imports System.Windows.Forms

Public Class FrmPermissionManager
    Dim permissionData As New PermissionDATA
    Dim groupData As New GroupDATA
    Dim formData As New FormAccessDATA
    Dim userData As New UsersDATA



    ' Initialize the form and load groups, available forms, and existing permissions
    Private Sub FrmPermissionManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGroups()
        LoadForms()
        LoadUserPermissions() ' Load user permissions initially if needed
    End Sub
    ' Load users  into ComboBox
    Private Sub LoadUsers()
        Dim allUsers = New UsersDATA().SelectAll()
        CboUser.Properties.Items.Clear()
        For Each u In allUsers
            CboUser.Properties.Items.Add(New ComboBoxItem With {.ID = u.UsID, .Name = u.UsName})
        Next

    End Sub
    ' Load users  into ComboBox
    Private Sub LoadUsersByGroup()
        Dim selectedItemGrp As ComboBoxItem = TryCast(CboGroup.SelectedItem, ComboBoxItem)
        If selectedItemGrp Is Nothing Then Return
        Dim selectedGroupID = selectedItemGrp.ID
        Dim allUsers = New UsersDATA().Select_ByGroupID(selectedGroupID)
        CboUser.Properties.Items.Clear()
        For Each u In allUsers
            CboUser.Properties.Items.Add(New ComboBoxItem With {.ID = u.UsID, .Name = u.UsName})
        Next

    End Sub
    ' Load user groups into ComboBox
    Private Sub LoadGroups()
        Dim groups = New GroupDATA().SelectAll()
        CboGroup.Properties.Items.Clear()
        For Each g In groups
            CboGroup.Properties.Items.Add(New ComboBoxItem With {.ID = g.GroupID, .Name = g.GroupName})
        Next
        If groups.Any Then
            LoadUsersByGroup()
        End If
    End Sub


    ' Load available forms into ComboBox
    Private Sub LoadForms()
        Dim forms = New FormAccessDATA().SelectAll()
        CboForms.Properties.Items.Clear()

        For Each f In forms
            Dim item As New DevExpress.XtraEditors.Controls.ComboBoxItem(f.FormName)
            CboForms.Properties.Items.Add(New ComboBoxItem With {.ID = f.FormID, .Name = f.FormName})
        Next
    End Sub


    ' Load the current permissions into the Grid based on selected group
    Private Sub LoadUserPermissions()
        Dim selectedItem As ComboBoxItem = DirectCast(CboGroup.SelectedItem, ComboBoxItem)
        If selectedItem Is Nothing Then Return

        If selectedItem IsNot Nothing Then
            Dim selectedGroupID = selectedItem.ID
            Dim permissions = permissionData.SelectPermissionsByGroup(selectedGroupID)
            GridControl1.DataSource = permissions
        End If

    End Sub

    ' When the user or group is selected, load the permissions and users for that group
    Private Sub CboGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboGroup.SelectedIndexChanged
        LoadUsersByGroup()
        LoadUserPermissions()
    End Sub
    ' When the Save button is clicked, save the permission settings
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSavePermissions.Click
        Dim selectedItemGrp As ComboBoxItem = TryCast(CboGroup.SelectedItem, ComboBoxItem)
        Dim selectedItemFrm As ComboBoxItem = TryCast(CboForms.SelectedItem, ComboBoxItem) ' Fixed this line

        If selectedItemGrp Is Nothing OrElse selectedItemFrm Is Nothing Then
            MessageBox.Show("Please select a group and form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim selectedGroupID = selectedItemGrp.ID
        Dim selectedGroupName = selectedItemGrp.Name.Trim()
        Dim selectedFormID = selectedItemFrm.ID

        ' Special case: Admin group - set all permissions for all forms
        If selectedGroupName.Equals("ADMINS", StringComparison.OrdinalIgnoreCase) Then
            Dim allForms = formData.SelectAll() ' This should return a list of all Form records
            Dim allSuccess As Boolean = True

            For Each form In allForms
                Dim perm As New Permission With {
                .GroupID = selectedGroupID,
                .FormID = form.FormID,
                .CanView = True,
                .CanEdit = True,
                .CanDelete = True
            }

                If Not permissionData.SavePermission(perm) Then
                    allSuccess = False
                End If
            Next

            If allSuccess Then
                MessageBox.Show("Admin permissions applied to all forms.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Some admin permissions failed to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            LoadUserPermissions()
            Return
        End If

        ' Regular save for selected group + form
        Dim canView = chkCanView.Checked
        Dim canEdit = chkCanEdit.Checked
        Dim canDelete = chkCanDelete.Checked

        Dim permission As New Permission With {
        .GroupID = selectedGroupID,
        .FormID = selectedFormID,
        .CanView = canView,
        .CanEdit = canEdit,
        .CanDelete = canDelete
    }

        If permissionData.SavePermission(permission) Then
            MessageBox.Show("Permission saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadUserPermissions()
        Else
            MessageBox.Show("Failed to save permission.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' When Cancel is clicked, close the form
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close() ' Or clear selections
    End Sub

    Private Sub BtnEditPermission_Click(sender As Object, e As EventArgs) Handles BtnEditPermission.Click
        Dim selectedPermission As Permission = TryCast(GridView1.GetFocusedRow(), Permission)
        If selectedPermission Is Nothing Then
            MessageBox.Show("Please select a permission to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Read updated values from the checkboxes
        selectedPermission.CanView = chkCanView.Checked
        selectedPermission.CanEdit = chkCanEdit.Checked
        selectedPermission.CanDelete = chkCanDelete.Checked

        ' Update the record
        If permissionData.Update(selectedPermission) Then
            MessageBox.Show("Permission updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadUserPermissions()
        Else
            MessageBox.Show("Failed to update permission.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    Private Sub BtnDelPermission_Click(sender As Object, e As EventArgs) Handles BtnDelPermission.Click
        Dim selectedPermission As Permission = TryCast(GridView1.GetFocusedRow(), Permission)
        If selectedPermission Is Nothing Then
            MessageBox.Show("Please select a permission to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim confirm = MessageBox.Show("Are you sure you want to delete this permission?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm <> DialogResult.Yes Then Return

        If permissionData.Delete(selectedPermission.PermissionID) Then
            MessageBox.Show("Permission deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadUserPermissions()
        Else
            MessageBox.Show("Failed to delete permission.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

End Class
