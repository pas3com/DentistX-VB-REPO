Imports DevExpress.XtraBars

Public Class FrmManager
    Private Sub BarAddUser_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarAddUser.ItemClick
        Dim F As New FrmAddNewUser
        F.ShowDialog(Me)
    End Sub
End Class