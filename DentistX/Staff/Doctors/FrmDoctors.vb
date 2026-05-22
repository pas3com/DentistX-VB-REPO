Imports System.ComponentModel
Imports System.Data.SqlClient
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms
Imports DentistX

Partial Public Class FrmDoctors


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
    Private clsDoctorsData As New DoctorsDATA
    Private clsDentistXData As New DentistXDATA


    Private Sub FrmDoctors2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WhatsHelper.AttachWhatsLocalDigitsOnlyKeyDown(DrWhatsTextEdit)
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
                .DataSource = clsDoctorsData.SelectAll
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
        DrIDSpinEdit.Enabled = False
        DrNameTextEdit.Enabled = YesNo
        DrAdresTextEdit.Enabled = YesNo
        DrphoneTextEdit.Enabled = YesNo
        DrMobileTextEdit.Enabled = YesNo
        DrWhatsPrefixCombo.Enabled = YesNo
        DrWhatsTextEdit.Enabled = YesNo
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
        Dim clsDoctors As New Doctors
        clsDoctors.DrID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colDrID))
        clsDoctors = clsDoctorsData.Select_Record(clsDoctors)
        If Not clsDoctors Is Nothing Then
            Try
                DrIDSpinEdit.Value = System.Convert.ToInt32(clsDoctors.DrID)
                DrNameTextEdit.Text = Convert.ToString(clsDoctors.DrName)
                DrAdresTextEdit.Text = Convert.ToString(clsDoctors.DrAdres)
                DrphoneTextEdit.Text = Convert.ToString(clsDoctors.DrPhone)
                DrMobileTextEdit.Text = Convert.ToString(clsDoctors.DrMobile)
                WhatsHelper.BindGenericWhatsPrefixAndLocal(DrWhatsPrefixCombo, DrWhatsTextEdit,
                    Convert.ToString(clsDoctors.WhatsAppPrefix), Convert.ToString(clsDoctors.WhatsApp))
                WhatsHelper.RefreshFullWhatsDigitsLabel(DrWhatsPrefixCombo, DrWhatsTextEdit, lblWhats)
                ColorMark.Color = ColorTranslator.FromHtml(clsDoctors.DrColor)
                ColorMark.BackColor = ColorTranslator.FromHtml(clsDoctors.DrColor)
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
        DrIDSpinEdit.Select()
        DrIDSpinEdit.Text = DentistXDATA.getAutoID("New", "Doctors")
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
        DrIDSpinEdit.Value = 0
        DrNameTextEdit.Text = Nothing
        DrAdresTextEdit.Text = Nothing
        DrphoneTextEdit.Text = Nothing
        DrMobileTextEdit.Text = Nothing
        DrWhatsTextEdit.Text = Nothing
        lblWhats.Text = ""
        WhatsHelper.FillCboPrefixOnce(DrWhatsPrefixCombo)
        If DrWhatsPrefixCombo.Properties.Items.Count > 0 Then DrWhatsPrefixCombo.SelectedIndex = 0
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
    Private Sub SetData(ByVal clsDoctors As Doctors)
        With clsDoctors
            .DrID = System.Convert.ToInt32(DrIDSpinEdit.Value)
            .DrName = System.Convert.ToString(DrNameTextEdit.Text)
            .DrAdres = System.Convert.ToString(DrAdresTextEdit.Text)
            .DrPhone = System.Convert.ToString(DrphoneTextEdit.Text)
            .DrMobile = System.Convert.ToString(DrMobileTextEdit.Text)
            .DrColor = System.Convert.ToString(ColorTranslator.ToHtml(ColorMark.Color))
            Dim waP As String = Nothing
            Dim waL As String = Nothing
            WhatsHelper.ReadGenericWhatsFromControls(DrWhatsPrefixCombo, DrWhatsTextEdit, waP, waL)
            .WhatsAppPrefix = waP
            .WhatsApp = waL
        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsDoctors As New Doctors
        If VerifyData() = True Then
            SetData(clsDoctors)
            Dim bSuccess As Boolean
            bSuccess = clsDoctorsData.Add(clsDoctors)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsDoctors As New Doctors
        Dim clsDoctors As New Doctors
        oclsDoctors.DrID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colDrID))
        oclsDoctors = clsDoctorsData.Select_Record(oclsDoctors)
        If VerifyData() = True Then
            SetData(clsDoctors)
            Dim bSuccess As Boolean
            bSuccess = clsDoctorsData.Update(oclsDoctors, clsDoctors)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsDoctors As New Doctors
        clsDoctors.DrID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colDrID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim apCount = clsDoctorsData.CountAppointmentCForDoctor(clsDoctors.DrID)
            If apCount > 0 Then
                MessageBox.Show(
                    If(Eng, $"Cannot delete this doctor: {apCount} appointment(s) still reference them. Remove or change the doctor on those appointments first.",
                       $"لا يمكن حذف الطبيب: ما زال هناك {apCount} موعد مرتبط به. أزل الطبيب من المواعيد أو غيّره أولاً."),
                    If(Eng, "Cannot delete", "تعذر الحذف"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Dim userCount = clsDoctorsData.CountUsersLinkedToDoctor(clsDoctors.DrID)
            If userCount > 0 Then
                MessageBox.Show(
                    If(Eng, $"Cannot delete this doctor: {userCount} user account(s) are linked to them. Unlink or reassign those users first.",
                       $"لا يمكن حذف الطبيب: ما زال هناك {userCount} مستخدم مرتبط به. افصل الارتباط أو عيّن طبيباً آخر."),
                    If(Eng, "Cannot delete", "تعذر الحذف"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim bSuccess As Boolean
            Try
                bSuccess = clsDoctorsData.Delete(clsDoctors)
            Catch ex As SqlException
                If ex.Number = 547 Then
                    MessageBox.Show(
                        If(Eng, "Cannot delete this doctor: the record is still referenced elsewhere in the database." & Environment.NewLine & ex.Message,
                           "لا يمكن حذف الطبيب: السجل ما زال مستخدماً في قاعدة البيانات." & Environment.NewLine & ex.Message),
                        If(Eng, "Cannot delete", "تعذر الحذف"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show(ex.Message, If(Eng, "Database error", "خطأ في قاعدة البيانات"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Return
            Catch ex As Exception
                MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields
        If DrIDSpinEdit.Value = Nothing OrElse DrIDSpinEdit.Value.ToString() = "" Then
            MsgBox("DrID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            DrIDSpinEdit.Select()
            Return False
        End If
        If DrNameTextEdit.Text = "" Then
            MsgBox("DrName is required.", MsgBoxStyle.OkOnly, "Entry Error")
            DrNameTextEdit.Select()
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Restores Add / Edit / Delete captions from the form resources (neutral or localized).
    ''' </summary>
    Private Sub ResetCrudButtonCaptions()
        Dim resources As New ComponentResourceManager(GetType(FrmDoctors))
        resources.ApplyResources(btnAdd, btnAdd.Name)
        resources.ApplyResources(btnEdit, btnEdit.Name)
        resources.ApplyResources(btnDel, btnDel.Name)
    End Sub

    ''' <summary>
    ''' Same enable/disable rules as FrmBracets.ShowToolStripItems: during Add/Edit/Delete only the
    ''' active action stays enabled; in browse mode all three CRUD buttons are enabled (if there are rows).
    ''' </summary>
    Private Sub ShowToolStripItems(ByVal Item As String)
        ' Same pattern as FrmBracets: during Add/Edit/Delete, Cancel is enabled; in browse/No Record it is off.
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
    Private Sub bAdd_ItemClick(sender As Object, e As ItemClickEventArgs)
        Add()
    End Sub
    Private Sub bEdit_ItemClick(sender As Object, e As ItemClickEventArgs)
        Display()
        Edit()
    End Sub
    Private Sub bDelete_ItemClick(sender As Object, e As ItemClickEventArgs)
        Display()
        Delete()
    End Sub
    Private Sub bRefresh_ItemClick(sender As Object, e As ItemClickEventArgs)
        LoadDGV()
    End Sub
    Private Sub bCancel_ItemClick(sender As Object, e As ItemClickEventArgs)
        ShowToolStripItems("Cancel")
    End Sub

    Private Sub ColorMark_EditValueChanged(sender As Object, e As EventArgs) Handles ColorMark.EditValueChanged
        lblMark.BackColor = ColorMark.Color
    End Sub

    Private Sub DrWhats_FullNumberRefresh(sender As Object, e As EventArgs) Handles DrWhatsPrefixCombo.SelectedIndexChanged, DrWhatsPrefixCombo.EditValueChanged, DrWhatsTextEdit.EditValueChanged
        WhatsHelper.RefreshFullWhatsDigitsLabel(DrWhatsPrefixCombo, DrWhatsTextEdit, lblWhats)
    End Sub
End Class
