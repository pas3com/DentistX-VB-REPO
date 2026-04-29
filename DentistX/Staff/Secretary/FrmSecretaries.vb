Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms
Imports DentistX

Partial Public Class FrmSecretaries


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
    Private clsSecretariesData As New SecretariesDATA
    Private clsDentistXData As New DentistXDATA


    Private Sub FrmSecretaries_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WhatsHelper.AttachWhatsLocalDigitsOnlyKeyDown(SecWhatsTextEdit)
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
                .DataSource = clsSecretariesData.SelectAll
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
        SecIDSpinEdit.Enabled = False
        SecNameTextEdit.Enabled = YesNo
        SecAdresTextEdit.Enabled = YesNo
        SecPhoneTextEdit.Enabled = YesNo
        SecMobileTextEdit.Enabled = YesNo
        SecColorTextEdit.Enabled = YesNo
        SecWhatsPrefixCombo.Enabled = YesNo
        SecWhatsTextEdit.Enabled = YesNo
        ColorMark.Enabled = YesNo
    End Sub

    Private Sub SetApplyCaptionAdd()
        btnAdd.Text = If(Eng, "Apply Add", "تطبيق الإضافة")
    End Sub

    Private Sub SetApplyCaptionEdit()
        btnEdit.Text = If(Eng, "Apply Edit", "تطبيق التعديل")
    End Sub

    Private Sub SetApplyCaptionDelete()
        btnDel.Text = If(Eng, "Apply Delete", "تطبيق الحذف")
    End Sub

    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel.Click
        If Not bDeleteMode Then
            Display()
            Delete()
            SetApplyCaptionDelete()
        Else
            DeleteRecord()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Not bEditMode Then
            Display()
            Edit()
            SetApplyCaptionEdit()
        Else
            UpdateRecord()
        End If
    End Sub

    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Me.Close()
    End Sub

    Private Sub butCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butCancel.Click
        ShowToolStripItems("Cancel")
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Not bAddMode Then
            Add()
            SetApplyCaptionAdd()
        Else
            InsertRecord()
        End If
    End Sub




    Private Sub Display()
        ClearRecord()
        Dim clsSecretaries As New Secretaries
        clsSecretaries.SecID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colSecID))
        clsSecretaries = clsSecretariesData.Select_Record(clsSecretaries)
        If Not clsSecretaries Is Nothing Then
            Try
                SecIDSpinEdit.Value = System.Convert.ToInt32(clsSecretaries.SecID)
                SecNameTextEdit.Text = Convert.ToString(clsSecretaries.SecName)
                SecAdresTextEdit.Text = Convert.ToString(clsSecretaries.SecAdres)
                SecPhoneTextEdit.Text = Convert.ToString(clsSecretaries.SecPhone)
                SecMobileTextEdit.Text = Convert.ToString(clsSecretaries.SecMobile)
                SecColorTextEdit.Text = Convert.ToString(clsSecretaries.SecColor)
                ColorMark.BackColor = ColorTranslator.FromHtml(clsSecretaries.SecColor)
                WhatsHelper.BindGenericWhatsPrefixAndLocal(SecWhatsPrefixCombo, SecWhatsTextEdit,
                    Convert.ToString(clsSecretaries.WhatsAppPrefix), Convert.ToString(clsSecretaries.WhatsApp))
                WhatsHelper.RefreshFullWhatsDigitsLabel(SecWhatsPrefixCombo, SecWhatsTextEdit, lblSecWhats)
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
        SecIDSpinEdit.Select()
        SecIDSpinEdit.Text = DentistXDATA.getAutoID("New", "Secretaries")
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
        SecIDSpinEdit.Value = 0
        SecNameTextEdit.Text = Nothing
        SecAdresTextEdit.Text = Nothing
        SecPhoneTextEdit.Text = Nothing
        SecMobileTextEdit.Text = Nothing
        SecColorTextEdit.Text = Nothing
        SecWhatsTextEdit.Text = Nothing
        lblSecWhats.Text = ""
        WhatsHelper.FillCboPrefixOnce(SecWhatsPrefixCombo)
        If SecWhatsPrefixCombo.Properties.Items.Count > 0 Then SecWhatsPrefixCombo.SelectedIndex = 0
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
    Private Sub SetData(ByVal clsSecretaries As Secretaries)
        With clsSecretaries
            .SecID = System.Convert.ToInt32(SecIDSpinEdit.Value)
            .SecName = System.Convert.ToString(SecNameTextEdit.Text)
            .SecAdres = System.Convert.ToString(SecAdresTextEdit.Text)
            .SecPhone = System.Convert.ToString(SecPhoneTextEdit.Text)
            .SecMobile = System.Convert.ToString(SecMobileTextEdit.Text)
            .SecColor = System.Convert.ToString(ColorTranslator.ToHtml(ColorMark.Color))
            Dim waP As String = Nothing
            Dim waL As String = Nothing
            WhatsHelper.ReadGenericWhatsFromControls(SecWhatsPrefixCombo, SecWhatsTextEdit, waP, waL)
            .WhatsAppPrefix = waP
            .WhatsApp = waL
        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsSecretaries As New Secretaries
        If VerifyData() = True Then
            SetData(clsSecretaries)
            Dim bSuccess As Boolean
            bSuccess = clsSecretariesData.Add(clsSecretaries)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsSecretaries As New Secretaries
        Dim clsSecretaries As New Secretaries
        oclsSecretaries.SecID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colSecID))
        oclsSecretaries = clsSecretariesData.Select_Record(oclsSecretaries)
        If VerifyData() = True Then
            SetData(clsSecretaries)
            Dim bSuccess As Boolean
            bSuccess = clsSecretariesData.Update(oclsSecretaries, clsSecretaries)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsSecretaries As New Secretaries
        clsSecretaries.SecID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colSecID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsSecretariesData.Delete(clsSecretaries)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields
        If SecIDSpinEdit.Value = Nothing OrElse SecIDSpinEdit.Value.ToString() = "" Then
            MsgBox("SecID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            SecIDSpinEdit.Select()
            Return False
        End If
        If SecNameTextEdit.Text = "" Then
            MsgBox("SecName is required.", MsgBoxStyle.OkOnly, "Entry Error")
            SecNameTextEdit.Select()
            Return False
        End If
        Return True
    End Function

    Private Sub ResetCrudButtonCaptions()
        Dim resources As New ComponentResourceManager(GetType(FrmSecretaries))
        resources.ApplyResources(btnAdd, btnAdd.Name)
        resources.ApplyResources(btnEdit, btnEdit.Name)
        resources.ApplyResources(btnDel, btnDel.Name)
    End Sub

    Private Sub ShowToolStripItems(ByVal Item As String)
        btnAdd.Enabled = False
        btnEdit.Enabled = False
        btnDel.Enabled = False
        butCancel.Enabled = True

        Select Case Item
            Case "Add"
                EnableRecord(True)
                btnAdd.Enabled = True
            Case "Edit"
                EnableRecord(True)
                btnEdit.Enabled = True
            Case "Delete"
                EnableRecord(False)
                btnDel.Enabled = True
            Case "Cancel"
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    ResetCrudButtonCaptions()
                    AddMode = False
                    EditMode = False
                    DeleteMode = False
                    EnableRecord(False)
                    btnAdd.Enabled = True
                    btnEdit.Enabled = True
                    btnDel.Enabled = True
                    butCancel.Enabled = False
                    Try
                        If Row >= 0 AndAlso Row < dgView.RowCount Then
                            Display()
                        End If
                    Catch
                    End Try
                    DGV.Focus()
                End If
            Case "Refresh"
                ResetCrudButtonCaptions()
                AddMode = False
                EditMode = False
                DeleteMode = False
                btnAdd.Enabled = True
                btnEdit.Enabled = True
                btnDel.Enabled = True
                butCancel.Enabled = False
                LoadDGV()
            Case "No Record"
                ResetCrudButtonCaptions()
                AddMode = False
                EditMode = False
                DeleteMode = False
                btnAdd.Enabled = True
                butCancel.Enabled = False
                EnableRecord(False)
        End Select
    End Sub


    Private Sub ColorMark_EditValueChanged(sender As Object, e As EventArgs) Handles ColorMark.EditValueChanged
        SecColorTextEdit.BackColor = ColorMark.Color
    End Sub

    Private Sub SecWhats_FullNumberRefresh(sender As Object, e As EventArgs) Handles SecWhatsPrefixCombo.SelectedIndexChanged, SecWhatsPrefixCombo.EditValueChanged, SecWhatsTextEdit.EditValueChanged
        WhatsHelper.RefreshFullWhatsDigitsLabel(SecWhatsPrefixCombo, SecWhatsTextEdit, lblSecWhats)
    End Sub
End Class
