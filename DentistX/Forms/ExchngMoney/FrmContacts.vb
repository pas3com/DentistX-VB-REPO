Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Partial Public Class FrmContacts


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
    Private clsContactData As New ContactDATA
    Private clsDentistXData As New DentistXDATA


    Private Sub FrmContact_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WhatsHelper.AttachWhatsLocalDigitsOnlyKeyDown(WhatsAppTextEdit)
        LoadDGV()
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
                .DataSource = clsContactData.SelectAll
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
        NameTextEdit.Enabled = YesNo
        PhoneTextEdit.Enabled = YesNo
        EmailTextEdit.Enabled = YesNo
        NotesTextEdit.Enabled = YesNo
        CreatedAtDateEdit.Enabled = YesNo
        WhatsAppPrefixCombo.Enabled = YesNo
        WhatsAppTextEdit.Enabled = YesNo
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
        Dim clsContact As New Contact
        clsContact.ContactID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colContactID))
        clsContact = clsContactData.Select_Record(clsContact)
        If Not clsContact Is Nothing Then
            Try
                ContactIDSpinEdit.Value = System.Convert.ToInt32(clsContact.ContactID)
                NameTextEdit.Text = Convert.ToString(clsContact.CName)
                PhoneTextEdit.Text = Convert.ToString(clsContact.Phone)
                EmailTextEdit.Text = Convert.ToString(clsContact.Email)
                NotesTextEdit.Text = Convert.ToString(clsContact.Notes)
                CreatedAtDateEdit.Text = System.Convert.ToDateTime(clsContact.CreatedAt)
                WhatsHelper.BindGenericWhatsPrefixAndLocal(WhatsAppPrefixCombo, WhatsAppTextEdit,
                    Convert.ToString(clsContact.WhatsAppPrefix), Convert.ToString(clsContact.WhatsApp))
                WhatsHelper.RefreshFullWhatsDigitsLabel(WhatsAppPrefixCombo, WhatsAppTextEdit, lblWhats)
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
        ContactIDSpinEdit.Value = 0
        NameTextEdit.Text = Nothing
        PhoneTextEdit.Text = Nothing
        EmailTextEdit.Text = Nothing
        NotesTextEdit.Text = Nothing
        CreatedAtDateEdit.Text = Nothing
        WhatsAppTextEdit.Text = Nothing
        lblWhats.Text = ""
        WhatsHelper.FillCboPrefixOnce(WhatsAppPrefixCombo)
        If WhatsAppPrefixCombo.Properties.Items.Count > 0 Then WhatsAppPrefixCombo.SelectedIndex = 0
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
    Private Sub SetData(ByVal clsContact As Contact)
        With clsContact
            .ContactID = System.Convert.ToInt32(ContactIDSpinEdit.Value)
            .CName = System.Convert.ToString(NameTextEdit.Text)
            .Phone = System.Convert.ToString(PhoneTextEdit.Text)
            .Email = System.Convert.ToString(EmailTextEdit.Text)
            .Notes = System.Convert.ToString(NotesTextEdit.Text)
            .CreatedAt = Now 'System.Convert.ToDateTime(CreatedAtDateEdit.EditValue)
            Dim waP As String = Nothing
            Dim waL As String = Nothing
            WhatsHelper.ReadGenericWhatsFromControls(WhatsAppPrefixCombo, WhatsAppTextEdit, waP, waL)
            .WhatsAppPrefix = waP
            .WhatsApp = waL
        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsContact As New Contact
        If VerifyData() = True Then
            SetData(clsContact)
            Dim bSuccess As Boolean
            bSuccess = clsContactData.Add(clsContact)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsContact As New Contact
        Dim clsContact As New Contact
        oclsContact.ContactID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colContactID))
        oclsContact = clsContactData.Select_Record(oclsContact)
        If VerifyData() = True Then
            SetData(clsContact)
            Dim bSuccess As Boolean
            bSuccess = clsContactData.Update(oclsContact, clsContact)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsContact As New Contact
        clsContact.ContactID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colContactID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsContactData.Delete(clsContact)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields
        If ContactIDSpinEdit.Value = Nothing OrElse ContactIDSpinEdit.Value.ToString() = "" Then
            MsgBox("ContactID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            ContactIDSpinEdit.Select()
            Return False
        End If
        If NameTextEdit.Text = "" Then
            MsgBox("Name is required.", MsgBoxStyle.OkOnly, "Entry Error")
            NameTextEdit.Select()
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
                butApply.Enabled = True
                butCancel.Enabled = True
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
                butApply.Enabled = True
                butCancel.Enabled = True
        End Select
    End Sub

    Private Sub bAdd_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bAdd.ItemClick
        ShowToolStripItems("Add")
        AddMode = True
    End Sub

    Private Sub bEdit_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bEdit.ItemClick
        ShowToolStripItems("Edit")
        EditMode = True
    End Sub

    Private Sub bDelete_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bDelete.ItemClick

    End Sub

    Private Sub bRefresh_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bRefresh.ItemClick

    End Sub

    Private Sub bCancel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bCancel.ItemClick

    End Sub

    Private Sub ContactsWhats_FullNumberRefresh(sender As Object, e As EventArgs) Handles WhatsAppPrefixCombo.SelectedIndexChanged, WhatsAppPrefixCombo.EditValueChanged, WhatsAppTextEdit.EditValueChanged
        WhatsHelper.RefreshFullWhatsDigitsLabel(WhatsAppPrefixCombo, WhatsAppTextEdit, lblWhats)
    End Sub
End Class
