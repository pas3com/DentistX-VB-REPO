Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Partial Public Class FrmLabMsgDetailChoice


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
 Private clsLabMsgDetailChoiceData As New LabMsgDetailChoiceData
 Private clsDentistXData As New DentistXData
 
 
 Private Sub FrmLabMsgDetailChoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
             .DataSource = clsLabMsgDetailChoiceData.SelectAll
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
        LabMsgDetailChoiceIDSpinEdit.Enabled = False
        cboLabMsgSubjectCombo.Enabled = YesNo
        DetailTextTextEdit.Enabled = YesNo
        SortOrderSpinEdit.Enabled = YesNo
        IsActiveCheckEdit.Enabled = YesNo
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
     Dim clsLabMsgDetailChoice As New LabMsgDetailChoice
     clsLabMsgDetailChoice.LabMsgDetailChoiceID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabMsgDetailChoiceID))
     clsLabMsgDetailChoice = clsLabMsgDetailChoiceData.Select_Record(clsLabMsgDetailChoice.LabMsgDetailChoiceID)
     If Not clsLabMsgDetailChoice Is Nothing Then
     Try
        LabMsgDetailChoiceIDSpinEdit.Value = System.Convert.ToInt32(clsLabMsgDetailChoice.LabMsgDetailChoiceID)
                cboLabMsgSubjectCombo.SetSubjectNameByLabMsgSubjectID(System.Convert.ToInt32(clsLabMsgDetailChoice.LabMsgSubjectID))
                DetailTextTextEdit.Text = Convert.ToString(clsLabMsgDetailChoice.DetailText)
        SortOrderSpinEdit.Value = System.Convert.ToInt32(clsLabMsgDetailChoice.SortOrder)
        IsActiveCheckEdit.Checked = System.Convert.ToBoolean(clsLabMsgDetailChoice.IsActive)
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
     LabMsgDetailChoiceIDSpinEdit.Select
     LabMsgDetailChoiceIDSpinEdit.Text = DentistXDATA.getAutoID("New", "LabMsgDetailChoice")
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
    LabMsgDetailChoiceIDSpinEdit.Value = 0
        cboLabMsgSubjectCombo.ClearSelection()
        DetailTextTextEdit.Text = Nothing
    SortOrderSpinEdit.Value = 0
    IsActiveCheckEdit.Checked = False
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
 Private Sub SetData(ByVal clsLabMsgDetailChoice As LabMsgDetailChoice)
     With clsLabMsgDetailChoice
        .LabMsgDetailChoiceID = System.Convert.ToInt32(LabMsgDetailChoiceIDSpinEdit.Value)
            .LabMsgSubjectID = System.Convert.ToInt32(cboLabMsgSubjectCombo.LabMsgSubjectID)
            .DetailText = System.Convert.ToString(DetailTextTextEdit.Text)
        .SortOrder = System.Convert.ToInt32(SortOrderSpinEdit.Value)
        .IsActive = System.Convert.ToBoolean(IsActiveCheckEdit.Checked)
     End With
 End Sub

    Private Sub InsertRecord()
        Dim clsLabMsgDetailChoice As New LabMsgDetailChoice
        If VerifyData() = True Then
            SetData(clsLabMsgDetailChoice)
            Dim bSuccess As Boolean
            bSuccess = clsLabMsgDetailChoiceData.Add(clsLabMsgDetailChoice)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsLabMsgDetailChoice As New LabMsgDetailChoice
        Dim clsLabMsgDetailChoice As New LabMsgDetailChoice
        oclsLabMsgDetailChoice.LabMsgDetailChoiceID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabMsgDetailChoiceID))
        oclsLabMsgDetailChoice = clsLabMsgDetailChoiceData.Select_Record(oclsLabMsgDetailChoice.LabMsgDetailChoiceID)
        If VerifyData() = True Then
            SetData(clsLabMsgDetailChoice)
            Dim bSuccess As Boolean
            bSuccess = clsLabMsgDetailChoiceData.Update(oclsLabMsgDetailChoice, clsLabMsgDetailChoice)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsLabMsgDetailChoice As New LabMsgDetailChoice
        clsLabMsgDetailChoice.LabMsgDetailChoiceID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabMsgDetailChoiceID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsLabMsgDetailChoiceData.Delete(clsLabMsgDetailChoice)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields
        If LabMsgDetailChoiceIDSpinEdit.Value = Nothing OrElse LabMsgDetailChoiceIDSpinEdit.Value.ToString() = "" Then
            MsgBox("LabMsgDetailChoiceID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            LabMsgDetailChoiceIDSpinEdit.Select()
            Return False
        End If
        If cboLabMsgSubjectCombo.SubjectName = Nothing OrElse cboLabMsgSubjectCombo.SubjectName.ToString() = "" Then
            MsgBox("LabMsgSubjectID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            cboLabMsgSubjectCombo.Select()
            Return False
        End If
        If DetailTextTextEdit.Text = "" Then
            MsgBox("DetailText is required.", MsgBoxStyle.OkOnly, "Entry Error")
            DetailTextTextEdit.Select()
            Return False
        End If
        If SortOrderSpinEdit.Value = Nothing OrElse SortOrderSpinEdit.Value.ToString() = "" Then
            MsgBox("SortOrder is required.", MsgBoxStyle.OkOnly, "Entry Error")
            SortOrderSpinEdit.Select()
            Return False
        End If
        If Not IsActiveCheckEdit.Checked = False Then
            MsgBox("IsActive is required.", MsgBoxStyle.OkOnly, "Entry Error")
            IsActiveCheckEdit.Select()
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
