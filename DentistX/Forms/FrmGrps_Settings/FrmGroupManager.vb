Imports System.Windows.Forms
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FrmGroupManager
    Dim groupData As New GroupDATA

    Private Sub FrmGroupManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGroups()
    End Sub

    Private Sub LoadGroups()
        TxtGroupName.ResetText()
        Dim groups = groupData.SelectAll()
        GridControl1.DataSource = groups
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAddGroup.Click
        Dim groupName = TxtGroupName.Text.Trim()
        If String.IsNullOrEmpty(groupName) Then
            MessageBox.Show("Group name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim newGroup As New Group With {.GroupName = groupName}
        If groupData.Add(newGroup) Then
            MessageBox.Show("Group added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadGroups()
        Else
            MessageBox.Show("Failed to add group.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEditGroup.Click
        If GridView1.FocusedRowHandle < 0 Then
            MessageBox.Show("Select a group to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim selectedGroup As Group = GridView1.GetRow(GridView1.FocusedRowHandle)
        TxtGroupName.Text = selectedGroup.GroupName
        Dim groupName = TxtGroupName.Text.Trim()
        If String.IsNullOrEmpty(groupName) Then
            MessageBox.Show("Group name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim newGroup As New Group With {.GroupID = selectedGroup.GroupID, .GroupName = groupName}
        If groupData.Update(newGroup) Then
            MessageBox.Show("Group Updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadGroups()
        Else
            MessageBox.Show("Failed to Update group.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelGroup.Click
        If GridView1.FocusedRowHandle < 0 Then
            MessageBox.Show("Select a group to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim selectedGroup As Group = GridView1.GetRow(GridView1.FocusedRowHandle)
        If MessageBox.Show("Delete this group?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        If groupData.Delete(selectedGroup.GroupID) Then
            MessageBox.Show("Group deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadGroups()
        Else
            MessageBox.Show("Failed to delete group.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
End Class
