Imports Dapper
Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class PermissionsForm

    Private Mode As String = "Group" ' Default mode
    Private SelectedID As Integer

    Private Sub FrmPermissions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CboLevel.SelectedIndex = 0
        LoadTargets()
    End Sub

    Private Sub CboLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboLevel.SelectedIndexChanged
        Mode = CboLevel.Text
        LoadTargets()
    End Sub

    Private Sub LoadTargets()
        If Mode = "Group" Then
            CboTarget.Properties.DataSource = Conn.Query("SELECT GroupID AS ID, GroupName AS Name FROM Groups")
            CboTarget.Properties.DisplayMember = "Name"
            CboTarget.Properties.ValueMember = "ID"
        Else
            CboTarget.Properties.DataSource = Conn.Query("SELECT UsID AS ID, UsName AS Name FROM USERS")
            CboTarget.Properties.DisplayMember = "Name"
            CboTarget.Properties.ValueMember = "ID"
        End If
    End Sub

    Private Sub CboTarget_EditValueChanged(sender As Object, e As EventArgs) Handles CboTarget.EditValueChanged
        If CboTarget.EditValue IsNot Nothing Then
            SelectedID = CboTarget.EditValue
            LoadPermissions()
        End If
    End Sub

    Private Sub LoadPermissions()
        Dim sql As String
        If Mode = "Group" Then
            sql = "SELECT p.PermID, p.PermName, ISNULL(gp.IsAllowed, 0) AS IsAllowed
                   FROM Permissions p
                   LEFT JOIN GroupPermissions gp ON gp.PermID = p.PermID AND gp.GroupID = @ID"
        Else
            sql = "EXEC GetEffectivePermissionsByUser @UsID = @ID"
        End If

        Dim dt = Conn.Query(Of PermissionResult)(sql, New With {.ID = SelectedID}).ToList()
        GridControl1.DataSource = dt
        GridView1.Columns("IsAllowed").Caption = "Allowed"
        GridView1.Columns("IsAllowed").OptionsColumn.AllowEdit = True
        GridView1.Columns("IsAllowed").ColumnEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        If SelectedID <> 0 Then LoadPermissions()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Dim list = CType(GridControl1.DataSource, List(Of PermissionResult))
        If Mode = "Group" Then
            For Each item In list
                Conn.Execute("
                    MERGE GroupPermissions AS target
                    USING (SELECT @GID AS GroupID, @PID AS PermID, @Allowed AS IsAllowed) AS src
                    ON target.GroupID = src.GroupID AND target.PermID = src.PermID
                    WHEN MATCHED THEN UPDATE SET IsAllowed = src.IsAllowed
                    WHEN NOT MATCHED THEN INSERT (GroupID, PermID, IsAllowed) VALUES (src.GroupID, src.PermID, src.IsAllowed);",
                    New With {.GID = SelectedID, .PID = item.PermID, .Allowed = item.IsAllowed})
            Next
            XtraMessageBox.Show("Group permissions saved successfully.")
        Else
            For Each item In list
                Conn.Execute("
                    MERGE UserPermissions AS target
                    USING (SELECT @UID AS UsID, @PID AS PermID, @Allowed AS IsAllowed) AS src
                    ON target.UsID = src.UsID AND target.PermID = src.PermID
                    WHEN MATCHED THEN UPDATE SET IsAllowed = src.IsAllowed
                    WHEN NOT MATCHED THEN INSERT (UsID, PermID, IsAllowed) VALUES (src.UsID, src.PermID, src.IsAllowed);",
                    New With {.UID = SelectedID, .PID = item.PermID, .Allowed = item.IsAllowed})
            Next
            XtraMessageBox.Show("User permissions saved successfully.")
        End If
    End Sub

    Private Class PermissionResult
        Public Property PermID As Integer
        Public Property PermName As String
        Public Property IsAllowed As Boolean
    End Class

End Class
