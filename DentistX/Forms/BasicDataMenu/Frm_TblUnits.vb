Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Partial Public Class Frm_TblUnits

    Public Sub New()
        InitializeComponent()
        Me.Icon = AppIcon

        'gridControl.DataSource = dataSource
        bsiRecordsCount.Caption = "RECORDS : " & TblUnitsBindingSource.Count
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



    Private Sub FrmTblUnits_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''TODO: This line of code loads data into the 'DsCities.TblUnits' table. You can move, or remove it, as needed.
        'Me.TblUnitsTableAdapter.Fill(Me.DsCities.TblUnits)
        ''TODO: This line of code loads data into the 'DsNames.TblUnits' table. You can move, or remove it, as needed.
        'Me.TblUnitsTableAdapter.Fill(Me.DsNames.TblUnits)
        LoadDGV()
        ShowToolStripItems("Cancel")

    End Sub

    '====================================================================

    Private clsTblUnitsData As New TblUnitsData
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
                .DataSource = clsTblUnitsData.SelectAll
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

        Dim clsTblUnits As New TblUnits
        clsTblUnits.UnitID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colUnitID))
        clsTblUnits = clsTblUnitsData.Select_Record(clsTblUnits)

        If Not clsTblUnits Is Nothing Then
            Try
                UnitIDSpinEdit.Value = System.Convert.ToInt32(clsTblUnits.UnitID)
                UnitNameTextEdit.Text = Convert.ToString(clsTblUnits.UnitName)


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

        UnitNameTextEdit.Select()
        UnitIDSpinEdit.Value = DentistXDATA.getAutoID("New", "TblUnits")

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Edit()
        Splitter1.Collapsed = False
        Me.AddMode = False
        Me.EditMode = True
        Me.DeleteMode = False

        ShowToolStripItems("Edit")
        dgView.FocusedRowHandle = Row
        UnitNameTextEdit.Select()
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
        Me.UnitIDSpinEdit.Enabled = False
        Me.UnitNameTextEdit.Enabled = YesNo

    End Sub

    Private Sub ClearRecord()

        Me.UnitIDSpinEdit.Value = 0
        Me.UnitNameTextEdit.Text = Nothing

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

    Private Sub butCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        ShowToolStripItems("Cancel")
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Me.Close()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub butApply_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If Me.AddMode = True Then
            Me.InsertRecord()
        ElseIf Me.EditMode = True Then
            Me.UpdateRecord()
        ElseIf Me.DeleteMode = True Then
            Me.DeleteRecord()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetData(ByVal clsTblUnits As TblUnits)
        With clsTblUnits
            .UnitName = System.Convert.ToString(UnitNameTextEdit.Text)

        End With
    End Sub

    Private Sub InsertRecord()

        Dim clsTblUnits As New TblUnits
        If VerifyData() = True Then
            SetData(clsTblUnits)
            Dim bSucess As Boolean
            bSucess = clsTblUnitsData.Add(clsTblUnits)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub UpdateRecord()

        Dim oclsTblUnits As New TblUnits
        Dim clsTblUnits As New TblUnits

        oclsTblUnits.UnitID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colUnitID))
        oclsTblUnits = clsTblUnitsData.Select_Record(oclsTblUnits)

        If VerifyData() = True Then
            SetData(clsTblUnits)
            Dim bSucess As Boolean
            bSucess = clsTblUnitsData.Update(oclsTblUnits, clsTblUnits)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub DeleteRecord()

        Dim clsTblUnits As New TblUnits
        clsTblUnits.UnitID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colUnitID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.Yes Then
            SetData(clsTblUnits)
            Dim bSucess As Boolean
            bSucess = clsTblUnitsData.Delete(clsTblUnits)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function VerifyData() As Boolean
        If UnitNameTextEdit.Text = "" Then
            MsgBox("TblUnits Name is Required", MsgBoxStyle.OkOnly, "Entry Error")
            UnitNameTextEdit.Select()
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
        Me.Text = "TblUnits - " & Item.Trim
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

    Private Sub TblUnitsBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.TblUnitsBindingSource.EndEdit()

    End Sub


    '===============================================================================================
    'Private Sub bbiNew_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bAdd.ItemClick
    '    'Dim F As New TblCatEdit
    '    'With F
    '    '    .btnDelete.Visible = False
    '    '    .btnSave.Visible = False
    '    '    .btnAdd.Visible = True
    '    '    .ShowDialog(Me)
    '    '    If .DialogResult = Windows.Forms.DialogResult.OK Then
    '    '        Me.TblUnitsTableAdapter.Fill(Me.DsNames.TblUnits)
    '    '    End If
    '    'End With

    'End Sub


    'Dim rowIndex As Integer
    'Private Sub bbiEdit_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bEdit.ItemClick
    '    Try
    '        Splitter1.Collapsed = False
    '        'dgView.ShowInplaceEditForm()
    '        dgView.MakeRowVisible(rowIndex)
    '        If dgView.IsRowVisible(rowIndex) Then
    '            MsgBox("vv")
    '        End If
    '        If dgView.RowCount = 0 Then Exit Sub
    '        'Load CashTrans & InChqs & Transes
    '        Dim UnitID, oldAge, oldBirthY As Integer
    '        Dim oldTRT, oldOrth, oldImp, oldMob, oldStruc, IsMale As Boolean

    '        Dim oldUnitName, oldSex, oldPhone, oldHealth, oldAdres, oldNotes As String

    '        '=================================
    '        rowIndex = dgView.FocusedRowHandle

    '        UnitID = CInt(dgView.GetRowCellDisplayText(rowIndex, colUnitID))
    '        'oldAge = CInt(dgView.GetRowCellDisplayText(row, colAge))
    '        Dim ageText As String = dgView.GetRowCellDisplayText(rowIndex, colAge)
    '        oldAge = If(String.IsNullOrEmpty(ageText), 0, CInt(Val(ageText)))

    '        Dim birthText As String = dgView.GetRowCellDisplayText(rowIndex, colBirthY)
    '        oldBirthY = If(String.IsNullOrEmpty(birthText), 0, CInt(Val(birthText)))

    '        oldUnitName = dgView.GetRowCellDisplayText(rowIndex, colUnitName)
    '        oldSex = dgView.GetRowCellDisplayText(rowIndex, colSex)
    '        If oldSex = "Male" Or oldSex = "ذكر" Then
    '            IsMale = True
    '        Else
    '            IsMale = False
    '        End If
    '        oldPhone = dgView.GetRowCellDisplayText(rowIndex, colPhone)

    '        oldHealth = dgView.GetRowCellDisplayText(rowIndex, colHealth)
    '        oldAdres = dgView.GetRowCellDisplayText(rowIndex, colAddress)
    '        oldNotes = dgView.GetRowCellDisplayText(rowIndex, colNotes)

    '        Dim Trt As String = dgView.GetRowCellDisplayText(rowIndex, colTreat)
    '        'oldTRT = If(String.IsNullOrEmpty(Trt), False, CBool(Trt))

    '        If Trt = "Checked" Or Trt = "true" Or Trt = "1" Then
    '            oldTRT = True
    '        ElseIf Trt = "Unchecked" Or Trt = "Indeterminate" Or Trt = "false" Or Trt = "0" Then
    '            oldTRT = False
    '        End If

    '        Dim Orth As String = dgView.GetRowCellDisplayText(rowIndex, colOrtho)
    '        'oldOrth = If(String.IsNullOrEmpty(Orth), False, CBool(Orth))
    '        If Orth = "Checked" Or Orth = "true" Or Orth = "1" Then
    '            oldOrth = True
    '        ElseIf Orth = "Unchecked" Or Orth = "Indeterminate" Or Orth = "false" Or Orth = "0" Then
    '            oldOrth = False
    '        End If

    '        Dim Imp As String = dgView.GetRowCellDisplayText(rowIndex, colImplant)
    '        'oldImp = If(String.IsNullOrEmpty(Imp), False, CBool(Imp))
    '        If Imp = "Checked" Or Imp = "true" Or Imp = "1" Then
    '            oldImp = True
    '        ElseIf Imp = "Unchecked" Or Imp = "Indeterminate" Or Imp = "false" Or Imp = "0" Then
    '            oldImp = False
    '        End If
    '        Dim Mob As String = dgView.GetRowCellDisplayText(rowIndex, colMobile)
    '        'oldMob = If(String.IsNullOrEmpty(Mob), False, CBool(Mob))
    '        If Mob = "Checked" Or Mob = "true" Or Mob = "1" Then
    '            oldMob = True
    '        ElseIf Mob = "Unchecked" Or Mob = "Indeterminate" Or Mob = "false" Or Mob = "0" Then
    '            oldMob = False
    '        End If

    '        Dim Struc As String = dgView.GetRowCellDisplayText(rowIndex, colStruc)
    '        'oldStruc = If(String.IsNullOrEmpty(Struc), False, CBool(Struc))
    '        If Struc = "Checked" Or Struc = "true" Or Struc = "1" Then
    '            oldStruc = True
    '        ElseIf Struc = "Unchecked" Or Struc = "Indeterminate" Or Struc = "false" Or Struc = "0" Then
    '            oldStruc = False
    '        End If
    '        'Dim F As New TblCatEdit
    '        'With F
    '        '    .btnDelete.Visible = False
    '        '    .btnSave.Visible = True
    '        '    .btnAdd.Visible = False
    '        '    .UnitIDEdit.Text = UnitID
    '        '    .TxtName.Text = oldUnitName
    '        '    .RadioMale.Checked = IsMale
    '        '    .RadioFemale.Checked = Not IsMale
    '        '    .TxtAge.Text = oldAge
    '        '    .TxtAdrs.Text = oldAdres
    '        '    .TxtHealth.Text = oldHealth
    '        '    .TxtNotes.Text = oldNotes
    '        '    .TxtPhone.Text = oldPhone
    '        '    .TreatCheck.Checked = oldTRT
    '        '    .OrthoCheck.Checked = oldOrth
    '        '    .StrucCheck.Checked = oldStruc
    '        '    .ImplntCheck.Checked = oldImp
    '        '    .MobileCheck.Checked = oldMob
    '        '    .ShowDialog(Me)
    '        '    If .DialogResult = Windows.Forms.DialogResult.OK Then
    '        '        Me.TblUnitsTableAdapter.Fill(Me.DsNames.TblUnits)
    '        '    End If
    '        'End With
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub bbiDelete_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bDelete.ItemClick

    '    Try
    '        If dgView.RowCount = 0 Then Exit Sub
    '        'Load CashTrans & InChqs & Transes
    '        Dim row, UnitID, oldAge, oldBirthY As Integer
    '        Dim oldTRT, oldOrth, oldImp, oldMob, oldStruc, IsMale As Boolean

    '        Dim oldUnitName, oldSex, oldPhone, oldHealth, oldAdres, oldNotes As String

    '        '=================================
    '        row = dgView.FocusedRowHandle

    '        UnitID = CInt(dgView.GetRowCellDisplayText(row, colUnitID))
    '        'oldAge = CInt(dgView.GetRowCellDisplayText(row, colAge))
    '        Dim ageText As String = dgView.GetRowCellDisplayText(row, colAge)
    '        oldAge = If(String.IsNullOrEmpty(ageText), 0, CInt(Val(ageText)))

    '        Dim birthText As String = dgView.GetRowCellDisplayText(row, colBirthY)
    '        oldBirthY = If(String.IsNullOrEmpty(birthText), 0, CInt(Val(birthText)))

    '        oldUnitName = dgView.GetRowCellDisplayText(row, colUnitName)
    '        oldSex = dgView.GetRowCellDisplayText(row, colSex)
    '        If oldSex = "Male" Or oldSex = "ذكر" Then
    '            IsMale = True
    '        Else
    '            IsMale = False
    '        End If
    '        oldPhone = dgView.GetRowCellDisplayText(row, colPhone)

    '        oldHealth = dgView.GetRowCellDisplayText(row, colHealth)
    '        oldAdres = dgView.GetRowCellDisplayText(row, colAddress)
    '        oldNotes = dgView.GetRowCellDisplayText(row, colNotes)

    '        Dim Trt As String = dgView.GetRowCellDisplayText(row, colTreat)
    '        'oldTRT = If(String.IsNullOrEmpty(Trt), False, CBool(Trt))

    '        If Trt = "Checked" Or Trt = "true" Or Trt = "1" Then
    '            oldTRT = True
    '        ElseIf Trt = "Unchecked" Or Trt = "Indeterminate" Or Trt = "false" Or Trt = "0" Then
    '            oldTRT = False
    '        End If

    '        Dim Orth As String = dgView.GetRowCellDisplayText(row, colOrtho)
    '        'oldOrth = If(String.IsNullOrEmpty(Orth), False, CBool(Orth))
    '        If Orth = "Checked" Or Orth = "true" Or Orth = "1" Then
    '            oldOrth = True
    '        ElseIf Orth = "Unchecked" Or Orth = "Indeterminate" Or Orth = "false" Or Orth = "0" Then
    '            oldOrth = False
    '        End If

    '        Dim Imp As String = dgView.GetRowCellDisplayText(row, colImplant)
    '        'oldImp = If(String.IsNullOrEmpty(Imp), False, CBool(Imp))
    '        If Imp = "Checked" Or Imp = "true" Or Imp = "1" Then
    '            oldImp = True
    '        ElseIf Imp = "Unchecked" Or Imp = "Indeterminate" Or Imp = "false" Or Imp = "0" Then
    '            oldImp = False
    '        End If
    '        Dim Mob As String = dgView.GetRowCellDisplayText(row, colMobile)
    '        'oldMob = If(String.IsNullOrEmpty(Mob), False, CBool(Mob))
    '        If Mob = "Checked" Or Mob = "true" Or Mob = "1" Then
    '            oldMob = True
    '        ElseIf Mob = "Unchecked" Or Mob = "Indeterminate" Or Mob = "false" Or Mob = "0" Then
    '            oldMob = False
    '        End If

    '        Dim Struc As String = dgView.GetRowCellDisplayText(row, colStruc)
    '        'oldStruc = If(String.IsNullOrEmpty(Struc), False, CBool(Struc))
    '        If Struc = "Checked" Or Struc = "true" Or Struc = "1" Then
    '            oldStruc = True
    '        ElseIf Struc = "Unchecked" Or Struc = "Indeterminate" Or Struc = "false" Or Struc = "0" Then
    '            oldStruc = False
    '        End If
    '        'Dim F As New TblCatEdit
    '        'With F
    '        '    .btnDelete.Visible = True
    '        '    .btnSave.Visible = False
    '        '    .btnAdd.Visible = False
    '        '    .UnitIDEdit.Text = UnitID
    '        '    .TxtName.Text = oldUnitName
    '        '    .RadioMale.Checked = IsMale
    '        '    .RadioFemale.Checked = Not IsMale
    '        '    .TxtAge.Text = oldAge
    '        '    .TxtAdrs.Text = oldAdres
    '        '    .TxtHealth.Text = oldHealth
    '        '    .TxtNotes.Text = oldNotes
    '        '    .TxtPhone.Text = oldPhone
    '        '    .TreatCheck.Checked = oldTRT
    '        '    .OrthoCheck.Checked = oldOrth
    '        '    .StrucCheck.Checked = oldStruc
    '        '    .ImplntCheck.Checked = oldImp
    '        '    .MobileCheck.Checked = oldMob
    '        '    .ShowDialog(Me)
    '        '    If .DialogResult = Windows.Forms.DialogResult.OK Then
    '        '        Me.TblUnitsTableAdapter.Fill(Me.DsNames.TblUnits)
    '        '    End If
    '        'End With
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub bbiRefresh_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bRefresh.ItemClick
    '    Me.TblUnitsTableAdapter.Fill(Me.DsNames.TblUnits)
    'End Sub




End Class
