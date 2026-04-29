Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base

Public Class FrmListUsers



    Dim userQrs As New UsersDATA
    Dim doctorQrs As New DoctorsDATA
    Dim secQrs As New SecretariesDATA
    Dim empQrs As New EmpDATA

    'Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData
    '    If e.Column.Name = "DrIdCol" Then
    '        Dim id As Integer = CInt(e.Value)
    '        e.Value = doctorQrs.GetDoctorById(id).DrName
    '    End If
    'End Sub

    Private Sub GridView1_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles GridView1.CustomColumnDisplayText
        If e.Column.FieldName = "DrID" OrElse e.Column.Name = "DrIdCol" Then
            If e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
                Dim drId As Integer = Convert.ToInt32(e.Value)
                e.DisplayText = doctorQrs.GetDoctorById(drId)?.DrName
            End If
        End If
        'Sec
        If e.Column.FieldName = "SecID" OrElse e.Column.Name = "SecIdCol" Then
            If e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
                Dim secId As Integer = Convert.ToInt32(e.Value)
                e.DisplayText = secQrs.GetSecretaryById(secId)?.SecName
            End If
        End If
        'Emp
        If e.Column.FieldName = "EmpID" OrElse e.Column.Name = "EmpIdCol" Then
            If e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
                Dim empId As Integer = Convert.ToInt32(e.Value)
                e.DisplayText = empQrs.GetEmpById(empId)?.EmpName
            End If
        End If
    End Sub
    Private Sub FrmListUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUsers()
        Me.Icon = AppIcon
    End Sub

    Private Sub LoadUsers()
        Dim userQrs As New UsersDATA()
        Dim usersList = userQrs.SelectAll
        GridControl1.DataSource = usersList
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If CurrentUser.UsLvl <> 1 Then
            XtraMessageBox.Show("The Logged IN User Is not in Admins Group.", "Admin User Required.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim frm As New FrmAddNewUser()
        frm.ShowDialog(Me)
        LoadUsers() ' Refresh after adding
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        If CurrentUser.UsLvl <> 1 Then
            XtraMessageBox.Show("The Logged IN User Is not in Admins Group.", "Admin User Required.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If GridView1.FocusedRowHandle < 0 Then
            XtraMessageBox.Show("Please select a user.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedUser As USERS = CType(GridView1.GetRow(GridView1.FocusedRowHandle), USERS)
        Dim frm As New FrmEditNewUser(selectedUser)
        frm.ShowDialog(Me)
        LoadUsers()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If CurrentUser.UsLvl <> 1 Then
            XtraMessageBox.Show("The Logged IN User Is not in Admins Group.", "Admin User Required.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If GridView1.FocusedRowHandle < 0 Then
            XtraMessageBox.Show("Please select a user.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If XtraMessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim selectedUser As USERS = CType(GridView1.GetRow(GridView1.FocusedRowHandle), USERS)
            Dim userQrs As New UsersDATA()
            If userQrs.Delete(selectedUser) Then
                XtraMessageBox.Show("User deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadUsers()
            Else
                XtraMessageBox.Show("Failed to delete user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub BtnResetPassword_Click(sender As Object, e As EventArgs) Handles BtnResetPassword.Click
        Dim selectedUser As USERS = CType(GridView1.GetRow(GridView1.FocusedRowHandle), USERS)
        If selectedUser.UsLvl <> CurrentUser.UsLvl Then
            XtraMessageBox.Show("You Can Only Change Your Own Password.", "Admin User Required.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
            'Else
            '    Dim frm As New FrmResetPassword()
            '    frm.ShowDialog(Me)
            '    LoadUsers() ' Refresh after adding
        End If
        'If CurrentUser.UsLvl <> 1 Then
        '    XtraMessageBox.Show("The Logged IN User Is not in Admins Group.", "Admin User Required.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Return
        'End If
        Dim frm As New FrmResetPassword(selectedUser)
        frm.ShowDialog(Me)
        LoadUsers() ' Refresh after adding
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
