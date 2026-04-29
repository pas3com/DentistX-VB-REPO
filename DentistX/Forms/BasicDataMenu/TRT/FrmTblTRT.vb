Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Partial Public Class FrmTblTRTS


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
 Private clsTblTRTSData As New TblTRTSData
 Private clsDentistXData As New DentistXData
 
 
 Private Sub FrmTblTRTS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
     LoadDGV()
     IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(DGV)
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
                .DataSource = clsTblTRTSData.SelectAll2
                bsiRecordsCount.Caption = "RECORDS : " & .DataSource.Count
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

        TrtTextEdit.Enabled = YesNo
        TrtValueSpinEdit.Enabled = YesNo
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
     Dim clsTblTRTS As New TblTRTS
        clsTblTRTS.TrtID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colTrtID))
    clsTblTRTS = clsTblTRTSData.Select_Record(clsTblTRTS)
     If Not clsTblTRTS Is Nothing Then
            Try
                TrtIDSpinEdit.Value = System.Convert.ToInt32(clsTblTRTS.TrtID)
                TrtTextEdit.Text = Convert.ToString(clsTblTRTS.Trt)
                TrtValueSpinEdit.Value = System.Convert.ToInt32(clsTblTRTS.TrtValue)
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
     TrtIDSpinEdit.Select
        TrtIDSpinEdit.Text = DentistXDATA.getAutoID("New", "TblTRTS")
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
        TrtIDSpinEdit.Value = 0
        TrtTextEdit.Text = Nothing
        TrtValueSpinEdit.Value = 0
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
 Private Sub SetData(ByVal clsTblTRTS As TblTRTS)
        With clsTblTRTS
            .TrtID = System.Convert.ToInt32(TrtIDSpinEdit.Value)
            .Trt = System.Convert.ToString(TrtTextEdit.Text)
            .TrtValue = System.Convert.ToInt32(TrtValueSpinEdit.Value)
        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsTblTRTS As New TblTRTS
        If VerifyData() = True Then
            SetData(clsTblTRTS)
            Dim bSuccess As Boolean
            bSuccess = clsTblTRTSData.Add(clsTblTRTS)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsTblTRTS As New TblTRTS
        Dim clsTblTRTS As New TblTRTS
        oclsTblTRTS.TrtID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colTrtID))
        oclsTblTRTS = clsTblTRTSData.Select_Record(oclsTblTRTS)
        If VerifyData() = True Then
            SetData(clsTblTRTS)
            Dim bSuccess As Boolean
            bSuccess = clsTblTRTSData.UpdateTrtValue(clsTblTRTS)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsTblTRTS As New TblTRTS
        clsTblTRTS.TrtID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colTrtID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsTblTRTSData.Delete(clsTblTRTS)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields
        If TrtIDSpinEdit.Value = Nothing OrElse TrtIDSpinEdit.Value.ToString() = "" Then
            MsgBox("TrtID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            TrtIDSpinEdit.Select()
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
