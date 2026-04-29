Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Partial Public Class FrmLab


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
    Private clsLabData As New LabDATA
    Private clsDentistXData As New DentistXDATA


    Private Sub FrmLab_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                .DataSource = clsLabData.SelectAll
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
        LabNameTextEdit.Enabled = YesNo
        AdresTextEdit.Enabled = YesNo
        PhoneTextEdit.Enabled = YesNo
        MobileTextEdit.Enabled = YesNo
        labWhatsPrefixCombo.Enabled = YesNo
        labWhatsTextEdit.Enabled = YesNo
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
        Dim clsLab As New Lab
        clsLab.LabID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabID))
        clsLab = clsLabData.Select_Record(clsLab)
        If Not clsLab Is Nothing Then
            Try
                LabIDSpinEdit.Value = System.Convert.ToInt32(clsLab.LabID)
                LabNameTextEdit.Text = Convert.ToString(clsLab.LabName)
                AdresTextEdit.Text = Convert.ToString(clsLab.Adres)
                PhoneTextEdit.Text = Convert.ToString(clsLab.Phone)
                MobileTextEdit.Text = Convert.ToString(clsLab.Mobile)
                WhatsHelper.BindGenericWhatsPrefixAndLocal(labWhatsPrefixCombo, labWhatsTextEdit,
                    Convert.ToString(clsLab.WhatsAppPrefix), Convert.ToString(clsLab.WhatsApp))
                WhatsHelper.RefreshFullWhatsDigitsLabel(labWhatsPrefixCombo, labWhatsTextEdit, lblLabWhats)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub EmpWhats_FullNumberRefresh(sender As Object, e As EventArgs) Handles labWhatsPrefixCombo.SelectedIndexChanged, labWhatsPrefixCombo.EditValueChanged, labWhatsTextEdit.EditValueChanged
        WhatsHelper.RefreshFullWhatsDigitsLabel(labWhatsPrefixCombo, labWhatsTextEdit, lblLabWhats)
    End Sub
    Private Sub Add()
        Me.AddMode = True
        Me.EditMode = False
        Me.DeleteMode = False
        ClearRecord()
        ShowToolStripItems("Add")
        LabIDSpinEdit.Select()
        LabIDSpinEdit.Text = DentistXDATA.getAutoID("New", "Lab")
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
        LabIDSpinEdit.Value = 0
        LabNameTextEdit.Text = Nothing
        AdresTextEdit.Text = Nothing
        PhoneTextEdit.Text = Nothing
        MobileTextEdit.Text = Nothing
        labWhatsTextEdit.Text = Nothing
        lblLabWhats.Text = ""
        WhatsHelper.FillCboPrefixOnce(labWhatsPrefixCombo)
        If labWhatsPrefixCombo.Properties.Items.Count > 0 Then labWhatsPrefixCombo.SelectedIndex = 0
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
    Private Sub SetData(ByVal clsLab As Lab)
        With clsLab
            .LabID = System.Convert.ToInt32(LabIDSpinEdit.Value)
            .LabName = System.Convert.ToString(LabNameTextEdit.Text)
            .Adres = System.Convert.ToString(AdresTextEdit.Text)
            .Phone = System.Convert.ToString(PhoneTextEdit.Text)
            .Mobile = System.Convert.ToString(MobileTextEdit.Text)
            Dim waP As String = Nothing
            Dim waL As String = Nothing
            WhatsHelper.ReadGenericWhatsFromControls(labWhatsPrefixCombo, labWhatsTextEdit, waP, waL)
            .WhatsAppPrefix = waP
            .WhatsApp = waL

        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsLab As New Lab
        If VerifyData() = True Then
            SetData(clsLab)
            Dim bSuccess As Boolean
            bSuccess = clsLabData.Add(clsLab)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsLab As New Lab
        Dim clsLab As New Lab
        oclsLab.LabID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabID))
        oclsLab = clsLabData.Select_Record(oclsLab)
        If VerifyData() = True Then
            SetData(clsLab)
            Dim bSuccess As Boolean
            bSuccess = clsLabData.Update(oclsLab, clsLab)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsLab As New Lab
        clsLab.LabID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsLabData.Delete(clsLab)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields
        If LabIDSpinEdit.Value = Nothing OrElse LabIDSpinEdit.Value.ToString() = "" Then
            MsgBox("LabID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            LabIDSpinEdit.Select()
            Return False
        End If
        If LabNameTextEdit.Text = "" Then
            MsgBox("LabName is required.", MsgBoxStyle.OkOnly, "Entry Error")
            LabNameTextEdit.Select()
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
