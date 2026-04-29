Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Partial Public Class Frm_Health

    Public Sub New()
        InitializeComponent()

        Me.Icon = AppIcon
        'gridControl.DataSource = dataSource
        bsiRecordsCount.Caption = "RECORDS : " & HealthBindingSource.Count
    End Sub

    ''to show number
    'Private Sub GridView_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gridView.CustomDrawCell

    '    If e.Column.Name = "colRowNum" Then
    '        e.DisplayText = (1 + e.RowHandle).ToString()
    '    End If

    'End Sub

    'to show number in Print
    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1

        End If
    End Sub

    Private Sub bbiPrintPreview_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles bbiPrintPreview.ItemClick
        DGV.ShowRibbonPrintPreview()
    End Sub



    Private Sub FrmHealth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''TODO: This line of code loads data into the 'DsCities.Health' table. You can move, or remove it, as needed.
        'Me.HealthTableAdapter.Fill(Me.DsCities.Health)
        ''TODO: This line of code loads data into the 'DsNames.Health' table. You can move, or remove it, as needed.
        'Me.HealthTableAdapter.Fill(Me.DsNames.Health)
        LoadDGV()
        ShowToolStripItems("Cancel")

    End Sub

    '====================================================================

    Private clsHealthData As New HealthData
    Private clsDentistXData As New DentistXData

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
                '.RowHeadersVisible = False
                .DataSource = clsHealthData.SelectAll
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    ClearRecord()

                End If
            End With
        Catch
        End Try
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub DGV_Click(sender As Object, e As System.EventArgs) Handles DGV.Click

        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch
        End Try
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub DGV_SelectionChanged(sender As Object, e As System.EventArgs) Handles dgView.SelectionChanged

        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch
        End Try
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Display()

        ClearRecord()

        Dim clsHealth As New Health
        clsHealth.HID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colHID))
        clsHealth = clsHealthData.Select_Record(clsHealth)

        If Not clsHealth Is Nothing Then
            Try
                HIDSpinEdit.Value = System.Convert.ToInt32(clsHealth.HID)
                HealthTextEdit.Text = Convert.ToString(clsHealth.HealthStat)


            Catch
            End Try
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Add()

        Me.AddMode = True
        Me.EditMode = False
        Me.DeleteMode = False

        ClearRecord()
        ShowToolStripItems("Add")

        HealthTextEdit.Select()
        HIDSpinEdit.Value = DentistXDATA.getAutoID("New", "Health")

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Edit()
        Splitter1.Collapsed = False
        Me.AddMode = False
        Me.EditMode = True
        Me.DeleteMode = False

        ShowToolStripItems("Edit")
        dgView.FocusedRowHandle = Row
        HealthTextEdit.Select()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Delete()

        Me.AddMode = False
        Me.EditMode = False
        Me.DeleteMode = True

        ShowToolStripItems("Delete")
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub EnableRecord(ByVal YesNo As Boolean)
        'Me.HIDSpinEdit.Enabled = False
        Me.HealthTextEdit.Enabled = YesNo

    End Sub

    Private Sub ClearRecord()

        Me.HIDSpinEdit.Value = 0
        Me.HealthTextEdit.Text = Nothing

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
        Catch
            'Hide error message.
        Finally
            If gridOK = False Then
                ''''
                ShowToolStripItems("Cancel")
                ''''
            End If
        End Try
        AddHandler dgView.SelectionChanged, AddressOf DGV_SelectionChanged
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub butCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butCancel.Click

        ShowToolStripItems("Cancel")
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click

        Me.Close()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub butApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butApply.Click

        If Me.AddMode = True Then
            Me.InsertRecord()
        ElseIf Me.EditMode = True Then
            Me.UpdateRecord()
        ElseIf Me.DeleteMode = True Then
            Me.DeleteRecord()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetData(ByVal clsHealth As Health)
        With clsHealth
            .HealthStat = System.Convert.ToString(HealthTextEdit.Text)

        End With
    End Sub

    Private Sub InsertRecord()

        Dim clsHealth As New Health
        If VerifyData() = True Then
            SetData(clsHealth)
            Dim bSucess As Boolean
            bSucess = clsHealthData.Add(clsHealth)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub UpdateRecord()

        Dim oclsHealth As New Health
        Dim clsHealth As New Health

        oclsHealth.HID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colHID))
        oclsHealth = clsHealthData.Select_Record(oclsHealth)

        If VerifyData() = True Then
            SetData(clsHealth)
            Dim bSucess As Boolean
            bSucess = clsHealthData.Update(oclsHealth, clsHealth)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub DeleteRecord()

        Dim clsHealth As New Health
        clsHealth.HID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colHID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.Yes Then
            SetData(clsHealth)
            Dim bSucess As Boolean
            bSucess = clsHealthData.Delete(clsHealth)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function VerifyData() As Boolean
        If HealthTextEdit.Text = "" Then
            MsgBox("Health Name is Required", MsgBoxStyle.OkOnly, "Entry Error")
            HealthTextEdit.Select()
            Return False
        End If
        Return True
    End Function

    Private Sub ShowToolStripItems(ByVal Item As String)

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
                    DGV.Focus()
                End If
            Case "Refresh"
                bAdd.Enabled = True
                bEdit.Enabled = True
                bDelete.Enabled = True
                bRefresh.Enabled = True
                butApply.Enabled = False
                butCancel.Enabled = False
                LoadDGV()
            Case "No Record"
                bAdd.Enabled = True
                bRefresh.Enabled = False
                butApply.Enabled = False
                butCancel.Enabled = False
        End Select
        Me.Text = "Health - " & Item.Trim
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub



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
        LoadDGV()
    End Sub

    Private Sub bCancel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bCancel.ItemClick
        ShowToolStripItems("Cancel")
    End Sub







End Class
