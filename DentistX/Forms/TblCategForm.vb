Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports System.Windows.Forms

Partial Public Class TblCategForm

    Public Sub New()
        InitializeComponent()

        'Dim dataSource As BindingList(Of Category) = GetDataSource()
        'gridCode.DataSource = dataSource

        'bsiRecordsCount.Caption = "RECORDS : " & dataSource.Count
        Me.Icon = AppIcon
    End Sub

    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1

        End If
    End Sub




    '#Region "OldCode"


    '    Public Function GetDataSource() As BindingList(Of Category)
    '        Dim result As New BindingList(Of Category)()
    '        Dim dt As DataTable = GetData()

    '        For Each row As DataRow In dt.Rows
    '            Dim category As New Category With {
    '            .CategoryID = Convert.ToInt32(row("CategoryID")),
    '            .CategoryName = row("CategoryName").ToString(),
    '            .ParentCategory = If(IsDBNull(row("ParentCategory")), Nothing, Convert.ToInt32(row("ParentCategory")))
    '        }
    '            result.Add(category)
    '        Next

    '        Return result
    '    End Function
    '    Public Class Category
    '        <Key, Display(AutoGenerateField:=True)>
    '        Public Property CategoryID() As Integer
    '        <Required>
    '        Public Property CategoryName() As String
    '        Public Property ParentCategory() As Integer

    '    End Class

    '    Function GetData() As DataTable
    '        Dim connectionString As String = My.Settings.DentistXConnectionString
    '        Dim query As String = "
    '        SELECT  [CategoryID]
    '        ,[CategoryName]
    '        ,[ParentCategory]
    '        FROM [dbo].[TblCategories]"

    '        Dim dt As New DataTable()

    '        Using conn As New SqlConnection(connectionString)
    '            Using cmd As New SqlCommand(query, conn)
    '                conn.Open()
    '                Dim reader As SqlDataReader = cmd.ExecuteReader()
    '                dt.Load(reader)
    '            End Using
    '        End Using

    '        Return dt
    '    End Function

    '    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
    '        If e.Column.Name = "colRowNum" Then
    '            e.Value = e.ListSourceRowIndex + 1

    '        End If
    '    End Sub

    '    Private Sub bbiPrintPreview_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles bbiPrintPreview.ItemClick
    '        gridCode.ShowRibbonPrintPreview()
    '    End Sub


    '#End Region

#Region "NewCode"



    Private clsTblCategoriesData As New TblCategoriesDATA
    Private clsDentistXData As New DentistXDATA

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

    Private Sub frmTblCategories_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        gridCode.DataSource = clsTblCategoriesData.SelectAll
        LoadGridCode()
        ShowToolStripItems("Cancel")
        'Dim filterManager As New DgvFilterManager(gridCode)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub LoadGridCode()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Try
            gridCode.DataSource = clsTblCategoriesData.SelectAll
            With dgView
                '.RowHeadersVisible = False
                '.DataSource = clsTblCategoriesData.SelectAll
                If .RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    ClearRecord()
                    '.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    '.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            End With
        Catch
        End Try
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub gridCode_Click(sender As Object, e As System.EventArgs) Handles gridCode.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch
        End Try
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub gridCode_SelectionChanged(sender As Object, e As System.EventArgs) Handles dgView.SelectionChanged
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch
        End Try
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Display()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ClearRecord()

        Dim clsTblCategories As New TblCategories
        clsTblCategories.CategoryID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colCategoryID))
        clsTblCategories = clsTblCategoriesData.Select_Record(clsTblCategories)

        If Not clsTblCategories Is Nothing Then
            Try
                nudCategoryID.Text = System.Convert.ToInt32(clsTblCategories.CategoryID)
                tbCategoryName.Text = If(clsTblCategories.CategoryName Is Nothing, Nothing, Convert.ToString(clsTblCategories.CategoryName))
                nudParentCategory.Text = If(clsTblCategories.ParentCategory Is Nothing, Nothing, System.Convert.ToInt32(clsTblCategories.ParentCategory))
            Catch
            End Try
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Add()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.AddMode = True
        Me.EditMode = False
        Me.DeleteMode = False

        ClearRecord()
        ShowToolStripItems("Add")

        tbCategoryName.Select()
        nudCategoryID.Text = clsDentistXData.getAutoID("New", "TblCategories")

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Edit()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.AddMode = False
        Me.EditMode = True
        Me.DeleteMode = False

        ShowToolStripItems("Edit")

        tbCategoryName.Select()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Delete()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.AddMode = False
        Me.EditMode = False
        Me.DeleteMode = True

        ShowToolStripItems("Delete")
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub EnableRecord(ByVal YesNo As Boolean)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.tbCategoryName.Enabled = YesNo
        Me.nudParentCategory.Enabled = YesNo
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ClearRecord()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.nudCategoryID.Text = 0
        Me.tbCategoryName.Text = Nothing
        Me.nudParentCategory.Text = 0
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub GoBack_To_Grid()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        RemoveHandler dgView.SelectionChanged, AddressOf gridCode_SelectionChanged
        Dim gridOK As Boolean = False
        Try
            LoadGridCode()
            ShowToolStripItems("Cancel")
            dgView.SelectRow(0)
            dgView.SelectCell(0, colCategoryID)

            gridCode.Focus()
            gridOK = True
        Catch
            'Hide error message.
        Finally
            If gridOK = False Then
                ''''
                ShowToolStripItems("Cancel")
                ''''
            End If
        End Try
        AddHandler dgView.SelectionChanged, AddressOf gridCode_SelectionChanged
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub butCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butCancel.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowToolStripItems("Cancel")
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.Close()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub butApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butApply.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If Me.AddMode = True Then
            Me.InsertRecord()
        ElseIf Me.EditMode = True Then
            Me.UpdateRecord()
        ElseIf Me.DeleteMode = True Then
            Me.DeleteRecord()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetData(ByVal clsTblCategories As TblCategories)
        With clsTblCategories
            .CategoryName = If(String.IsNullOrEmpty(tbCategoryName.Text), Nothing, tbCategoryName.Text)
            .ParentCategory = If(String.IsNullOrEmpty(nudParentCategory.Text), Nothing, CType(nudParentCategory.Text, Int32?))
        End With
    End Sub

    Private Sub InsertRecord()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Dim clsTblCategories As New TblCategories
        If VerifyData() = True Then
            SetData(clsTblCategories)
            Dim bSucess As Boolean
            bSucess = clsTblCategoriesData.Add(clsTblCategories)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub UpdateRecord()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Dim oclsTblCategories As New TblCategories
        Dim clsTblCategories As New TblCategories

        oclsTblCategories.CategoryID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colCategoryID))
        oclsTblCategories = clsTblCategoriesData.Select_Record(oclsTblCategories)

        If VerifyData() = True Then
            SetData(clsTblCategories)
            Dim bSucess As Boolean
            bSucess = clsTblCategoriesData.Update(oclsTblCategories, clsTblCategories)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub DeleteRecord()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Dim clsTblCategories As New TblCategories
        clsTblCategories.CategoryID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colCategoryID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.Yes Then
            SetData(clsTblCategories)
            Dim bSucess As Boolean
            bSucess = clsTblCategoriesData.Delete(clsTblCategories)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function VerifyData() As Boolean
        Return True
    End Function

    Private Sub ShowToolStripItems(ByVal Item As String)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        bAdd.Enabled = False
        bEdit.Enabled = False
        bDelete.Enabled = False
        bCancel.Enabled = False
        bRefresh.Enabled = False
        butApply.Enabled = True
        butCancel.Enabled = True
        Select Case Item
            Case "Add"
                EnableRecord(True)
                bCancel.Enabled = True
            Case "Edit"
                EnableRecord(True)
                bCancel.Enabled = True
            Case "Delete"
                EnableRecord(False)
                bCancel.Enabled = True
            Case "Cancel"
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    EnableRecord(False)
                    bAdd.Enabled = True
                    bEdit.Enabled = True
                    bDelete.Enabled = True
                    bRefresh.Enabled = True
                    butApply.Enabled = False
                    butCancel.Enabled = False
                    gridCode.Focus()
                End If
            Case "Refresh"
                bAdd.Enabled = True
                bEdit.Enabled = True
                bDelete.Enabled = True
                bRefresh.Enabled = True
                butApply.Enabled = False
                butCancel.Enabled = False
                LoadGridCode()
            Case "No Record"
                bAdd.Enabled = True
                bRefresh.Enabled = False
                butApply.Enabled = False
                butCancel.Enabled = False
        End Select
        Me.Text = "[Dbo. Tbl Categories] - " & Item.Trim
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    'Private Sub TS_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles TS.ItemClicked
    '    Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
    '    If e.ClickedItem.Text <> "Export" Then
    '        ShowToolStripItems(e.ClickedItem.Text)
    '    End If
    '    Select Case e.ClickedItem.Text
    '        Case "Add"
    '            Add()
    '        Case "Edit"
    '            Display()
    '            Edit()
    '        Case "Delete"
    '            Display()
    '            Delete()
    '        Case "Cancel"
    '            ShowToolStripItems("Cancel")
    '        Case "Refresh"
    '            LoadGridCode()
    '    End Select
    '    Me.Cursor = System.Windows.Forms.Cursors.Default
    'End Sub

    Private Sub bAdd_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bAdd.ItemClick
        Add()
    End Sub

    Private Sub bEdit_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bEdit.ItemClick
        Display()
        Edit()
    End Sub

    Private Sub bDelete_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bDelete.ItemClick
        Display()
        Delete()
    End Sub

    Private Sub bRefresh_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bRefresh.ItemClick
        LoadGridCode()
    End Sub

    Private Sub bCancel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bCancel.ItemClick
        ShowToolStripItems("Cancel")
    End Sub

    'Sub bWord_All_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bWord_All.Click
    '    Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
    '    'clsDentistXData.ExportToWord("Many", Me.Text, gridCode)
    '    Me.Cursor = System.Windows.Forms.Cursors.Default
    'End Sub

    'Sub bWord_Current_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bWord_Current.Click
    '    Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
    '    'clsDentistXData.ExportToWord("One", Me.Text, gridCode)
    '    Me.Cursor = System.Windows.Forms.Cursors.Default
    'End Sub

    'Sub bExcel_All_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bExcel_All.Click
    '    Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
    '    clsDentistXData.ExportToExcel("Many", Me.Text, gridCode)
    '    Me.Cursor = System.Windows.Forms.Cursors.Default
    'End Sub

    'Sub bExcel_Current_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bExcel_Current.Click
    '    Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
    '    clsDentistXData.ExportToExcel("One", Me.Text, gridCode)
    '    Me.Cursor = System.Windows.Forms.Cursors.Default
    'End Sub

    'Sub bPDF_All_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bPDF_All.Click
    '    Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
    '    'clsDentistXData.ExportToPdf("Many", Me.Text, gridCode, gridCode.CurrentRow)
    '    Me.Cursor = System.Windows.Forms.Cursors.Default
    'End Sub

    'Sub bPDF_Current_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bPDF_Current.Click
    '    Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
    '    'clsDentistXData.ExportToPdf("One", Me.Text, gridCode, gridCode.CurrentRow)
    '    Me.Cursor = System.Windows.Forms.Cursors.Default
    'End Sub



#End Region



End Class


''to show number
'Private Sub GridView_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gridView.CustomDrawCell

'    If e.Column.Name = "colRowNum" Then
'        e.DisplayText = (1 + e.RowHandle).ToString()
'    End If

'End Sub

'to show number in Print
