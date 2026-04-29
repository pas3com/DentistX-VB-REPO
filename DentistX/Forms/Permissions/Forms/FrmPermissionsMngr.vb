Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Public Class FrmPermissionsMngr

    Private UsersList As List(Of UserInfo)
    Private FullPermissionList As List(Of PermissionItem) = New List(Of PermissionItem)()
    Private filterFlashTimer As System.Windows.Forms.Timer
    Private flashRowHandles As List(Of Integer) = New List(Of Integer)()
    Private CurrentGroupID As Integer?
    Private CurrentUserID As Integer? 'CurrentPermID
    Private CurrentPermID As Integer? '




    Private Sub FrmPermissionManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GroupsCombo1.Enabled = Perms.CanDo("Patients.View")
    End Sub

    Private Sub GroupsCombo1_GroupsValueChanged(sender As Object, e As GroupsCombo.GroupsIndexChangedEvent) Handles GroupsCombo1.GroupsValueChanged
        CurrentGroupID = e.GroupID
        LoadGroupPermissions(e.GroupID)
        RemoveHandler USERSCombo1.USERSValueChanged, AddressOf USERSCombo1_USERSValueChanged
        USERSCombo1.ResetSelection() ' clear user selection
        AddHandler USERSCombo1.USERSValueChanged, AddressOf USERSCombo1_USERSValueChanged
        grpSlctdUser_GRP.Text = If(Eng, $"Permissions for Group: {e.GroupName}", $"الأذونات للمجموعة : {e.GroupName}")
    End Sub

    Private Sub USERSCombo1_USERSValueChanged(sender As Object, e As USERSCombo.USERSIndexChangedEvent) Handles USERSCombo1.USERSValueChanged
        CurrentUserID = e.UsID
        LoadUserPermissions(e.UsID)
        RemoveHandler GroupsCombo1.GroupsValueChanged, AddressOf GroupsCombo1_GroupsValueChanged
        GroupsCombo1.ResetSelection() ' clear group selection
        AddHandler GroupsCombo1.GroupsValueChanged, AddressOf GroupsCombo1_GroupsValueChanged
        grpSlctdUser_GRP.Text = If(Eng, $"Permissions for User: {e.UsName}", $"الأذونات للمستخدم :  {e.UsName}")
    End Sub

    'Private Sub PermissionsCombo1_PermissionsValueChanged(sender As Object, e As PermissionsCombo.PermissionsIndexChangedEvent) Handles PermissionsCombo1.PermissionsValueChanged
    'i want to display the groups related to the permission selected and the users related to the permission selected
    'write a query to get the groups and users related to the permission selected
    'write a sub to bind the grid to the data



    Private Sub PermissionsCombo1_PermissionsValueChanged(sender As Object, e As PermissionsCombo.PermissionsIndexChangedEvent) Handles PermissionsCombo1.PermissionsValueChanged

        Try
            ResetGrid(GridViewPermissions, GridPermissions)

            If e.PermID <= 0 Then
                GridPermissions.DataSource = Nothing
                Return
            End If
            CurrentPermID = e.PermID
            ' choose one (or show both in tabs / split panels)
            If btnShowGrpUsr.IsOn Then
                ' Show Users
                LoadPermissionUsers(e.PermID)
            Else
                ' Show Groups
                LoadPermissionGroups(e.PermID)
            End If

            'LoadPermissionGroups(e.PermID)
            ' LoadPermissionUsers(e.PermID)

            GridViewPermissions.BestFitColumns()

        Catch ex As Exception
            XtraMessageBox.Show(
            If(Eng, "Error loading permission relations: ", "خطأ في تحميل ارتباطات الصلاحيات: ") & ex.Message,
            If(Eng, "Permission View", "عرض الصلاحيات"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )
        End Try
    End Sub




    Private Sub LoadGroupPermissions(groupId As Integer)
        Dim sql = "
        SELECT p.PermID, p.PermName, gp.IsAllowed AS GroupAllowed,
               NULL AS UserOverride,
               ISNULL(gp.IsAllowed, 0) AS IsAllowed
        FROM Permissions p
        LEFT JOIN GroupPermissions gp ON gp.PermID = p.PermID AND gp.GroupID = @GroupID
        ORDER BY p.PermName;"
        Dim data = Conn.Query(Of PermissionItem)(sql, New With {.GroupID = groupId}).ToList()
        FullPermissionList = data
        BindGrid(data, isUserMode:=False)
    End Sub
    Private Sub LoadUserPermissions2(userId As Integer)
        Dim sql = "EXEC GetEffectivePermissionsByUser @UsID"
        Dim data = Conn.Query(Of PermissionItem)(sql, New With {.UsID = userId}).ToList()
        BindGrid(data, isUserMode:=True)
    End Sub
    Private Sub LoadUserPermissions(userId As Integer)
        Dim sql = "
                SELECT 
                p.PermID,
                p.PermName,
                gp.IsAllowed AS GroupAllowed,
                up.IsAllowed AS UserOverride,
                ISNULL(up.IsAllowed, gp.IsAllowed) AS IsAllowed
            FROM Permissions p
            INNER JOIN USERS u ON u.UsID = @UsID
            LEFT JOIN GroupPermissions gp ON gp.PermID = p.PermID AND gp.GroupID = u.GroupID
            LEFT JOIN UserPermissions up ON up.UsID = u.UsID AND up.PermID = p.PermID
            ORDER BY p.PermName;
        "
        Dim data = Conn.Query(Of PermissionItem)(sql, New With {.UsID = userId}).ToList()
        FullPermissionList = data
        BindGrid(data, isUserMode:=True)
    End Sub
    Private Sub BindGrid(data As List(Of PermissionItem), isUserMode As Boolean)
        GridSlctdUser_Grp.DataSource = Nothing
        GridSlctdUser_Grp.DataSource = data
        ' Toggle switch with emoji
        Dim repoSwitch As New DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch() With {
        .OnText = "✅",
        .OffText = "🚫",
        .GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
    }
        GridSlctdUser_Grp.RepositoryItems.Clear()
        GridSlctdUser_Grp.RepositoryItems.Add(repoSwitch)
        If GridViewUserGrp.Columns("IsAllowed") IsNot Nothing Then
            GridViewUserGrp.Columns("IsAllowed").ColumnEdit = repoSwitch
            GridViewUserGrp.Columns("IsAllowed").OptionsColumn.AllowEdit = True
        End If
        ApplyPermissionListCaptions(GridViewUserGrp)
        ' Ensure PermName column is visible and wide
        If GridViewUserGrp.Columns("PermName") IsNot Nothing Then GridViewUserGrp.Columns("PermName").Width = 300
        ' Attach RowStyle for colorful rows
        RemoveHandler GridViewUserGrp.RowStyle, AddressOf GridViewUserGrp_RowStyle
        AddHandler GridViewUserGrp.RowStyle, AddressOf GridViewUserGrp_RowStyle
        GridViewUserGrp.BestFitColumns()
    End Sub
    Private Sub GridViewUserGrp_RowStyle(sender As Object, e As RowStyleEventArgs)
        Dim view = TryCast(sender, GridView)
        If view Is Nothing OrElse e.RowHandle < 0 Then Return
        Try
            Dim item = TryCast(view.GetRow(e.RowHandle), PermissionItem)
            If item Is Nothing Then Return

            ' Determine coloring priority:
            ' 1) If there is a UserOverride -> highlight as override (orange)
            ' 2) Else if IsAllowed = True -> green
            ' 3) Else -> light red
            If item.UserOverride.HasValue Then
                ' override
                e.Appearance.BackColor = Color.FromArgb(255, 244, 214) ' warm light orange
                e.Appearance.ForeColor = Color.FromArgb(102, 51, 0) ' dark warm text
            ElseIf item.IsAllowed Then
                e.Appearance.BackColor = Color.FromArgb(223, 240, 216) ' light green
                e.Appearance.ForeColor = Color.FromArgb(0, 73, 36)
            Else
                e.Appearance.BackColor = Color.FromArgb(253, 236, 234) ' light red/pink
                e.Appearance.ForeColor = Color.FromArgb(102, 0, 0)
            End If
            ' Make name bold to be cheerful
            e.Appearance.Font = New Font("Segoe UI", 9.5!, FontStyle.Bold)
        Catch ex As Exception
            ' ignore style exceptions
        End Try
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Dim data = CType(GridSlctdUser_Grp.DataSource, List(Of PermissionItem))
        If CurrentGroupID.HasValue Then
            SaveGroupPermissions(CurrentGroupID.Value, data)
        ElseIf CurrentUserID.HasValue Then
            SaveUserPermissions(CurrentUserID.Value, data)
        Else
            XtraMessageBox.Show(If(Eng, "Please select a Group or User first.", "يرجى اختيار مجموعة أو مستخدم أولاً."),
                                If(Eng, "Info", "معلومة"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub SaveGroupPermissions(groupId As Integer, data As List(Of PermissionItem))
        For Each item In data
            Conn.Execute("MERGE GroupPermissions AS target
                          USING (SELECT @GroupID AS GroupID, @PermID AS PermID, @IsAllowed AS IsAllowed) AS src
                          ON target.GroupID = src.GroupID AND target.PermID = src.PermID
                          WHEN MATCHED THEN UPDATE SET IsAllowed = src.IsAllowed
                          WHEN NOT MATCHED THEN INSERT (GroupID, PermID, IsAllowed)
                          VALUES (src.GroupID, src.PermID, src.IsAllowed);",
                          New With {.GroupID = groupId, .PermID = item.PermID, .IsAllowed = item.IsAllowed})
        Next
        XtraMessageBox.Show(If(Eng, "✅ Group permissions saved successfully!", "✅ تم حفظ صلاحيات المجموعة بنجاح!"),
                            If(Eng, "Success", "نجاح"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
    End Sub
    Private Sub SaveUserPermissions(userId As Integer, data As List(Of PermissionItem))
        For Each item In data
            Conn.Execute("MERGE UserPermissions AS target
                          USING (SELECT @UsID AS UsID, @PermID AS PermID, @IsAllowed AS IsAllowed) AS src
                          ON target.UsID = src.UsID AND target.PermID = src.PermID
                          WHEN MATCHED THEN UPDATE SET IsAllowed = src.IsAllowed
                          WHEN NOT MATCHED THEN INSERT (UsID, PermID, IsAllowed)
                          VALUES (src.UsID, src.PermID, src.IsAllowed);",
                          New With {.UsID = userId, .PermID = item.PermID, .IsAllowed = item.IsAllowed})
        Next
        XtraMessageBox.Show(If(Eng, "✅ User permissions saved successfully!", "✅ تم حفظ صلاحيات المستخدم بنجاح!"),
                            If(Eng, "Success", "نجاح"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
    End Sub

    '=========================================
    Private Sub LoadPermissionGroups(permId As Integer)
        Dim sql = "
        SELECT g.GroupID, g.GroupName, gp.IsAllowed
        FROM Groups g
        LEFT JOIN GroupPermissions gp 
            ON gp.GroupID = g.GroupID
           AND gp.PermID = @PermID
        ORDER BY g.GroupName;"

        Dim data = Conn.Query(Of PermissionGroupView)(sql, New With {.PermID = permId}).ToList()
        GridPermissions.DataSource = data
        ApplyPermissionGroupsCaptions(GridViewPermissions)
    End Sub

    Private Sub LoadPermissionUsers(permId As Integer)
        Dim sql = "
        SELECT
            u.UsID,
            u.UsName,
            g.GroupName,
            gp.IsAllowed AS GroupAllowed,
            up.IsAllowed AS UserOverride,
            ISNULL(up.IsAllowed, gp.IsAllowed) AS IsAllowed
        FROM USERS u
        INNER JOIN Groups g ON g.GroupID = u.GroupID
        LEFT JOIN GroupPermissions gp 
            ON gp.GroupID = u.GroupID
           AND gp.PermID = @PermID
        LEFT JOIN UserPermissions up
            ON up.UsID = u.UsID
           AND up.PermID = @PermID
        ORDER BY g.GroupName, u.UsName;"

        Dim data = Conn.Query(Of PermissionUserView)(sql, New With {.PermID = permId}).ToList()
        GridPermissions.DataSource = data
        ApplyPermissionUsersCaptions(GridViewPermissions)
    End Sub
    Private Sub ResetGrid(view As GridView, grid As GridControl)
        view.BeginUpdate()
        Try
            view.Columns.Clear()
            view.SortInfo.Clear()
            view.GroupCount = 0
            view.ClearSelection()
            grid.DataSource = Nothing
        Finally
            view.EndUpdate()
        End Try
    End Sub
    Private Sub StartFilterFlash()
        If filterFlashTimer Is Nothing Then
            filterFlashTimer = New System.Windows.Forms.Timer()
            filterFlashTimer.Interval = 120 ' ms
            AddHandler filterFlashTimer.Tick, AddressOf OnFilterFlashTick
        End If
        filterFlashTimer.Tag = 0
        filterFlashTimer.Start()
    End Sub
    Private Sub OnFilterFlashTick(sender As Object, e As EventArgs)
        Try
            Dim stepIndex As Integer = CInt(filterFlashTimer.Tag)
            ' toggle highlight intensity by alternating row fore/back colors slightly
            For Each Handle1 In flashRowHandles
                If Handle1 < 0 Or Handle1 >= GridViewUserGrp.RowCount Then Continue For
                If stepIndex Mod 2 = 0 Then
                    GridViewUserGrp.SetRowCellValue(Handle1, GridViewUserGrp.Columns("PermName"), GridViewUserGrp.GetRowCellValue(Handle, "PermName")) ' force repaint
                    GridViewUserGrp.FocusedRowHandle = Handle1
                End If
            Next
            stepIndex += 1
            filterFlashTimer.Tag = stepIndex
            If stepIndex > 6 Then
                filterFlashTimer.Stop()
                GridViewUserGrp.ClearSelection()
                GridViewUserGrp.RefreshData()
            End If
        Catch ex As Exception
            filterFlashTimer.Stop()
        End Try
    End Sub

    Private Sub btnShowGrpUsr_Toggled(sender As Object, e As EventArgs) Handles btnShowGrpUsr.Toggled
        If btnShowGrpUsr.IsOn Then
            If CurrentPermID.HasValue Then
                ResetGrid(GridViewPermissions, GridPermissions)
                LoadPermissionUsers(CurrentPermID.Value)
            End If
            grpPermissions.Text = If(Eng, "🔐 Show Users Permission", "🔐 عرض صلاحيات المستخدمين")
        Else
            If CurrentPermID.HasValue Then
                ResetGrid(GridViewPermissions, GridPermissions)

                LoadPermissionGroups(CurrentPermID.Value)
            End If
            grpPermissions.Text = If(Eng, "🔐 Show Groups Permission", "🔐 عرض صلاحيات المجموعات")
        End If
    End Sub

    Private Sub ApplyPermissionListCaptions(view As GridView)
        If view Is Nothing Then Return
        SetColumnCaption(view, "PermID", "Perm ID", "رقم الصلاحية")
        SetColumnCaption(view, "PermName", "Permission", "الصلاحية")
        SetColumnCaption(view, "GroupAllowed", "Group Allowed", "إذن المجموعة")
        SetColumnCaption(view, "UserOverride", "User Override", "تجاوز المستخدم")
        SetColumnCaption(view, "IsAllowed", "Allowed", "مسموح")
    End Sub

    Private Sub ApplyPermissionGroupsCaptions(view As GridView)
        If view Is Nothing Then Return
        SetColumnCaption(view, "GroupID", "Group ID", "رقم المجموعة")
        SetColumnCaption(view, "GroupName", "Group", "المجموعة")
        SetColumnCaption(view, "IsAllowed", "Allowed", "مسموح")
    End Sub

    Private Sub ApplyPermissionUsersCaptions(view As GridView)
        If view Is Nothing Then Return
        SetColumnCaption(view, "UsID", "User ID", "رقم المستخدم")
        SetColumnCaption(view, "UsName", "User", "المستخدم")
        SetColumnCaption(view, "GroupName", "Group", "المجموعة")
        SetColumnCaption(view, "GroupAllowed", "Group Allowed", "إذن المجموعة")
        SetColumnCaption(view, "UserOverride", "User Override", "تجاوز المستخدم")
        SetColumnCaption(view, "IsAllowed", "Allowed", "مسموح")
    End Sub

    Private Sub SetColumnCaption(view As GridView, fieldName As String, engCaption As String, arCaption As String)
        Dim col = view.Columns(fieldName)
        If col Is Nothing Then Return
        col.Caption = If(Eng, engCaption, arCaption)
    End Sub
End Class




