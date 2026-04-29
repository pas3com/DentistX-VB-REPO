Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Partial Public Class FrmPermissions


    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
     If e.Column.Name = "colRowNum" Then
         e.Value = e.ListSourceRowIndex + 1
     End If
 End Sub

    Private Sub bbiPrintPreview_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
        DGV.ShowRibbonPrintPreview()
    End Sub
    Private clsPermissionsData As New PermissionsDATA
    Private clsDentistXData As New DentistXDATA


    Private Sub FrmPermissions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDGV()
        ShowToolStripItems("Cancel")
    End Sub
    Private bAddMode As Boolean = False
    Private bEditMode As Boolean = False
    Private bDeleteMode As Boolean = False
    Private iRow As Int32 = 0

    Public Property AddMode() As Boolean
        Get
            Return bAddMode
        End Get
        Set(ByVal value As Boolean)
            bAddMode = value
        End Set
    End Property

    Public Property EditMode() As Boolean
        Get
            Return bEditMode
        End Get
        Set(ByVal value As Boolean)
            bEditMode = value
        End Set
    End Property

    Public Property DeleteMode() As Boolean
        Get
            Return bDeleteMode
        End Get
        Set(ByVal value As Boolean)
            bDeleteMode = value
        End Set
    End Property

    Public Property Row() As Int32
        Get
            Return iRow
        End Get
        Set(ByVal value As Int32)
            iRow = value
        End Set
    End Property

    Private Sub LoadDGV()
        Try
            With DGV
                .DataSource = clsPermissionsData.SelectAll
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    ClearRecord()
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGV_Click(sender As Object, e As System.EventArgs) Handles DGV.Click
        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGV_SelectionChanged(sender As Object, e As System.EventArgs) Handles dgView.SelectionChanged
        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub EnableRecord(ByVal YesNo As Boolean)

        PermKeyTextEdit.Enabled = YesNo
        PermNameTextEdit.Enabled = YesNo
        PermDescriptionTextEdit.Enabled = YesNo
        CategoryTextEdit.Enabled = YesNo
        IsActiveCheckEdit.Enabled = YesNo

    End Sub

    Private Sub butCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ShowToolStripItems("Cancel")
    End Sub


    Private Sub Display()
        ClearRecord()
        Dim clsPermissions As New Permissions
        clsPermissions.PermID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colPermID))
        clsPermissions = clsPermissionsData.Select_Record(clsPermissions)
        If Not clsPermissions Is Nothing Then
            Try
                PermIDSpinEdit.Value = System.Convert.ToInt32(clsPermissions.PermID)
                PermKeyTextEdit.Text = Convert.ToString(clsPermissions.PermKey)
                PermNameTextEdit.Text = Convert.ToString(clsPermissions.PermName)
                PermDescriptionTextEdit.Text = Convert.ToString(clsPermissions.PermDescription)
                CategoryTextEdit.Text = Convert.ToString(clsPermissions.Category)
                IsActiveCheckEdit.Checked = System.Convert.ToBoolean(clsPermissions.IsActive)
                CreatedAtDateEdit.Text = System.Convert.ToDateTime(clsPermissions.CreatedAt)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Add()
        Me.AddMode = True
        Me.EditMode = False
        Me.DeleteMode = False
        ClearRecord()
        ShowToolStripItems("Add")
        PermIDSpinEdit.Select()
        PermIDSpinEdit.Text = DentistXDATA.getAutoID("New", "Permissions")
    End Sub

    Private Sub Edit()
        Me.AddMode = False
        Me.EditMode = True
        Me.DeleteMode = False
        ShowToolStripItems("Edit")
    End Sub

    Private Sub Delete()
        Me.AddMode = False
        Me.EditMode = False
        Me.DeleteMode = True
        ShowToolStripItems("Delete")
    End Sub

    Private Sub ClearRecord()
        PermIDSpinEdit.Value = 0
        PermKeyTextEdit.Text = Nothing
        PermNameTextEdit.Text = Nothing
        PermDescriptionTextEdit.Text = Nothing
        CategoryTextEdit.Text = Nothing
        IsActiveCheckEdit.Checked = False
        CreatedAtDateEdit.Text = Nothing
    End Sub

    Private Sub GoBack_To_Grid()
        RemoveHandler dgView.SelectionChanged, AddressOf DGV_SelectionChanged
        Dim gridOK As Boolean = False
        Try
            LoadDGV()
            ShowToolStripItems("Cancel")
            dgView.SelectRow(Row)
            dgView.FocusedRowHandle = Row
            dgView.Focus()
            gridOK = True
        Catch ex As Exception
            MsgBox(ex.Message)
            'Hide error message.
        Finally
            If gridOK = False Then
                ''''
                ShowToolStripItems("Cancel")
                ''''
            End If
        End Try
        AddHandler dgView.SelectionChanged, AddressOf DGV_SelectionChanged
    End Sub
    Private Sub SetData(ByVal clsPermissions As Permissions)
        With clsPermissions
            .PermID = System.Convert.ToInt32(PermIDSpinEdit.Value)
            .PermKey = System.Convert.ToString(PermKeyTextEdit.Text)
            .PermName = System.Convert.ToString(PermNameTextEdit.Text)
            .PermDescription = System.Convert.ToString(PermDescriptionTextEdit.Text)
            .Category = System.Convert.ToString(CategoryTextEdit.Text)
            .IsActive = System.Convert.ToBoolean(IsActiveCheckEdit.Checked)
            .CreatedAt = System.Convert.ToDateTime(CreatedAtDateEdit.Text)
        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsPermissions As New Permissions
        If VerifyData() = True Then
            SetData(clsPermissions)
            Dim bSuccess As Boolean
            bSuccess = clsPermissionsData.Add(clsPermissions)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsPermissions As New Permissions
        Dim clsPermissions As New Permissions
        oclsPermissions.PermID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colPermID))
        oclsPermissions = clsPermissionsData.Select_Record(oclsPermissions)
        If VerifyData() = True Then
            SetData(clsPermissions)
            Dim bSuccess As Boolean
            bSuccess = clsPermissionsData.Update(oclsPermissions, clsPermissions)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsPermissions As New Permissions
        clsPermissions.PermID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colPermID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsPermissionsData.Delete(clsPermissions)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields
        If PermIDSpinEdit.Value = Nothing OrElse PermIDSpinEdit.Value.ToString() = "" Then
            MsgBox("PermID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            PermIDSpinEdit.Select()
            Return False
        End If
        If PermNameTextEdit.Text = "" Then
            MsgBox("PermName is required.", MsgBoxStyle.OkOnly, "Entry Error")
            PermNameTextEdit.Select()
            Return False
        End If
        Return True
    End Function

    Private Sub ShowToolStripItems(ByVal Item As String)
        Select Case Item
            Case "Add"
                EnableRecord(True)
            Case "Edit"
                EnableRecord(True)
            Case "Delete"
                EnableRecord(False)
            Case "Cancel"
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    EnableRecord(False)
                    DGV.Focus()
                End If
            Case "Refresh"
                LoadDGV()
            Case "No Record"
        End Select
    End Sub

    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Select Case btnDel.Text
            Case "Delete"
                Delete()
                btnDel.Text = "Apply Delete"
            Case "Apply Delete"
                Me.DeleteRecord()
                ClearRecord()
                btnDel.Text = "Delete"
                Me.DeleteMode = False
        End Select
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Select Case btnEdit.Text
            Case "Edit"
                Edit()
                btnEdit.Text = "Apply Edit"
            Case "Apply Edit"
                Me.UpdateRecord()
                ClearRecord()
                btnEdit.Text = "Edit"
                Me.EditMode = False
        End Select
    End Sub
    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Me.Close()
    End Sub

    Private Sub butApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Select Case btnAdd.Text
            Case "Add"
                Add()
                btnAdd.Text = "Apply Add"
            Case "Apply Add"
                Me.InsertRecord()
                ClearRecord()
                btnAdd.Text = "Add"
                Me.AddMode = False
        End Select



        'If Me.AddMode = True Then

        'ElseIf Me.EditMode = True Then
        '    Me.UpdateRecord()
        'ElseIf Me.DeleteMode = True Then
        '    Me.DeleteRecord()
        'End If
    End Sub

End Class
