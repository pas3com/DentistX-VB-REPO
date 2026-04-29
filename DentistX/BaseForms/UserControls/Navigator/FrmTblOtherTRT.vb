Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Partial Public Class FrmTblOtherTRT


    Public Sub New()
        InitializeComponent()
        bsiRecordsCount.Caption = "RECORDS : " & dgView.RowCount
    End Sub

    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub

    Private Sub bbiPrintPreview_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles bbiPrintPreview.ItemClick
        DGV.ShowRibbonPrintPreview()
    End Sub
    Private clsTblOtherTRTData As New TblOtherTRTDATA
    Private clsDentistXData As New DentistXDATA


    Private Sub FrmTblOtherTRT_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                .DataSource = clsTblOtherTRTData.SelectAll
                bsiRecordsCount.Caption = "RECORDS : " & .DataSource.Count
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    ClearRecord()
                End If
            End With
        Catch
        End Try
    End Sub

    Private Sub DGV_Click(sender As Object, e As System.EventArgs) Handles DGV.Click
        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch
        End Try
    End Sub

    Private Sub DGV_SelectionChanged(sender As Object, e As System.EventArgs) Handles dgView.SelectionChanged
        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch
        End Try
    End Sub

    Private Sub EnableRecord(ByVal YesNo As Boolean)
        TblOtherTrtIDSpinEdit.Enabled = YesNo
        TrtTextEdit.Enabled = YesNo
    End Sub

    Private Sub butCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butCancel.Click
        ShowToolStripItems("Cancel")
    End Sub

    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Me.Close()
    End Sub

    Private Sub butApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butApply.Click
        If Me.AddMode = True Then
            Me.InsertRecord()
        ElseIf Me.EditMode = True Then
            Me.UpdateRecord()
        ElseIf Me.DeleteMode = True Then
            Me.DeleteRecord()
        End If
    End Sub
    Private Sub Display()
        ClearRecord()
        Dim clsTblOtherTRT As New TblOtherTRT
        clsTblOtherTRT.TblOtherTrtID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colTblOtherTrtID))
        clsTblOtherTRT = clsTblOtherTRTData.Select_Record(clsTblOtherTRT)
        If Not clsTblOtherTRT Is Nothing Then
            Try
                TblOtherTrtIDSpinEdit.Value = System.Convert.ToInt32(clsTblOtherTRT.TblOtherTrtID)
                TrtTextEdit.Text = Convert.ToString(clsTblOtherTRT.Trt)
            Catch
            End Try
        End If
    End Sub

    Private Sub Add()
        Me.AddMode = True
        Me.EditMode = False
        Me.DeleteMode = False
        ClearRecord()
        ShowToolStripItems("Add")
        TblOtherTrtIDSpinEdit.Select()
        TblOtherTrtIDSpinEdit.Text = clsDentistXData.getAutoID("New", "TblOtherTRT")
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
        TblOtherTrtIDSpinEdit.Value = 0
        TrtTextEdit.Text = Nothing
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
    End Sub
    Private Sub SetData(ByVal clsTblOtherTRT As TblOtherTRT)
        With clsTblOtherTRT
            .TblOtherTrtID = System.Convert.ToInt32(TblOtherTrtIDSpinEdit.Value)
            .Trt = System.Convert.ToString(TrtTextEdit.Text)
        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsTblOtherTRT As New TblOtherTRT
        If VerifyData() = True Then
            SetData(clsTblOtherTRT)
            Dim bSuccess As Boolean
            bSuccess = clsTblOtherTRTData.Add(clsTblOtherTRT)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsTblOtherTRT As New TblOtherTRT
        Dim clsTblOtherTRT As New TblOtherTRT
        oclsTblOtherTRT.TblOtherTrtID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colTblOtherTrtID))
        oclsTblOtherTRT = clsTblOtherTRTData.Select_Record(oclsTblOtherTRT)
        If VerifyData() = True Then
            SetData(clsTblOtherTRT)
            Dim bSuccess As Boolean
            bSuccess = clsTblOtherTRTData.Update(oclsTblOtherTRT, clsTblOtherTRT)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsTblOtherTRT As New TblOtherTRT
        clsTblOtherTRT.TblOtherTrtID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colTblOtherTrtID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsTblOtherTRTData.Delete(clsTblOtherTRT)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields
        If TblOtherTrtIDSpinEdit.Value = Nothing OrElse TblOtherTrtIDSpinEdit.Value.ToString() = "" Then
            MsgBox("TblOtherTrtID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            TblOtherTrtIDSpinEdit.Select()
            Return False
        End If
        If TrtTextEdit.Text = "" Then
            MsgBox("Trt is required.", MsgBoxStyle.OkOnly, "Entry Error")
            TrtTextEdit.Select()
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
            Case "Edit"
                EnableRecord(True)
            Case "Delete"
                EnableRecord(False)
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
