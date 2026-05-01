Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Partial Public Class FrmLabPay

    Private _initialDataQueued As Boolean = False
    Private _loadingGrid As Boolean = False
    Private _suspendRowDisplay As Boolean = False

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
    Private clsLabPayData As New LabPayDATA
    Private clsDentistXData As New DentistXDATA


    Private Sub FrmLabPay_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(DGV)
        EnableRecord(False)
        bAdd.Enabled = False
        bEdit.Enabled = False
        bDelete.Enabled = False
        bRefresh.Enabled = False
        butApply.Enabled = False
        butCancel.Enabled = False
    End Sub

    Private Sub FrmLabPay_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If _initialDataQueued Then Return
        _initialDataQueued = True
        BeginInvoke(New MethodInvoker(AddressOf InitializeAfterShown))
    End Sub

    Private Sub InitializeAfterShown()
        _suspendRowDisplay = True
        Try
            LoadDGV()
            ShowToolStripItems("Cancel")
        Finally
            _suspendRowDisplay = False
        End Try
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
            _loadingGrid = True
            With DGV
                .DataSource = clsLabPayData.SelectAll
                bsiRecordsCount.Caption = "RECORDS : " & .DataSource.Count
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    ClearRecord()
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            _loadingGrid = False
        End Try
    End Sub

    Private Sub DGV_Click(sender As Object, e As System.EventArgs) Handles DGV.Click
        Try
            If _loadingGrid OrElse _suspendRowDisplay Then Return
            Row = dgView.FocusedRowHandle
            Display()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGV_SelectionChanged(sender As Object, e As System.EventArgs) Handles dgView.SelectionChanged
        Try
            If _loadingGrid OrElse _suspendRowDisplay Then Return
            Row = dgView.FocusedRowHandle
            Display()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub EnableRecord(ByVal YesNo As Boolean)
        LabCombo1.Enabled = YesNo
        LabOrderCombo1.Enabled = YesNo
        PayValueSpinEdit.Enabled = YesNo
        PayDateDateEdit.Enabled = YesNo
        PayDetailTextEdit.Enabled = YesNo
        NotesTextEdit.Enabled = YesNo
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
        Dim clsLabPay As LabPay = TryCast(dgView.GetRow(Row), LabPay)
        If clsLabPay Is Nothing Then
            clsLabPay = New LabPay
            clsLabPay.LabPayID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabPayID))
            clsLabPay = clsLabPayData.Select_Record(clsLabPay)
        End If
        If Not clsLabPay Is Nothing Then
            Try
                LabPayIDSpinEdit.Value = System.Convert.ToInt32(clsLabPay.LabPayID)
                LabCombo1.SetSelectedLabName(System.Convert.ToInt32(clsLabPay.LabID))
                LabOrderCombo1.SetLabOrderLabFilter(System.Convert.ToInt32(clsLabPay.LabID))
                LabOrderCombo1.SetOrderDetailsByLabOrderID(System.Convert.ToInt32(clsLabPay.LabOrderID))
                PayValueSpinEdit.Value = System.Convert.ToInt32(clsLabPay.PayValue)
                PayDateDateEdit.Text = System.Convert.ToDateTime(clsLabPay.PayDate)
                PayDetailTextEdit.Text = Convert.ToString(clsLabPay.PayDetail)
                NotesTextEdit.Text = Convert.ToString(clsLabPay.Notes)
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
        LabPayIDSpinEdit.Select()
        LabPayIDSpinEdit.Text = DentistXDATA.getAutoID("New", "LabPay")
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
        LabPayIDSpinEdit.Value = 0
        LabCombo1.SetSelectedLabName(-1)
        LabCombo1.LabName = Nothing
        LabOrderCombo1.SetLabOrderLabFilter(0)
        LabOrderIDSpinEdit.Value = 0
        PayValueSpinEdit.Value = 0
        PayDateDateEdit.Text = Nothing
        PayDetailTextEdit.Text = Nothing
        NotesTextEdit.Text = Nothing
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
    Private Sub SetData(ByVal clsLabPay As LabPay)
        With clsLabPay
            .LabPayID = System.Convert.ToInt32(LabPayIDSpinEdit.Value)
            .LabID = System.Convert.ToInt32(LabCombo1.LabID)
            .LabOrderID = System.Convert.ToInt32(LabOrderCombo1.LabOrderID)
            .PayValue = System.Convert.ToInt32(PayValueSpinEdit.Value)
            .PayDate = System.Convert.ToDateTime(PayDateDateEdit.Text)
            .PayDetail = System.Convert.ToString(PayDetailTextEdit.Text)
            .Notes = System.Convert.ToString(NotesTextEdit.Text)
        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsLabPay As New LabPay
        If VerifyData() = True Then
            SetData(clsLabPay)
            Dim bSuccess As Boolean
            bSuccess = clsLabPayData.Add(clsLabPay)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsLabPay As New LabPay
        Dim clsLabPay As New LabPay
        oclsLabPay.LabPayID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabPayID))
        oclsLabPay = clsLabPayData.Select_Record(oclsLabPay)
        If VerifyData() = True Then
            SetData(clsLabPay)
            Dim bSuccess As Boolean
            bSuccess = clsLabPayData.Update(oclsLabPay, clsLabPay)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsLabPay As New LabPay
        clsLabPay.LabPayID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabPayID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsLabPayData.Delete(clsLabPay)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields
        If LabPayIDSpinEdit.Value = Nothing OrElse LabPayIDSpinEdit.Value.ToString() = "" Then
            MsgBox("LabPayID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            LabPayIDSpinEdit.Select()
            Return False
        End If
        If LabCombo1.LabID <= 0 OrElse String.IsNullOrWhiteSpace(LabCombo1.LabName) Then
            MsgBox("Please select a lab.", MsgBoxStyle.OkOnly, "Entry Error")
            LabCombo1.Select()
            Return False
        End If
        If LabOrderCombo1.LabOrderID <= 0 Then
            MsgBox("Please select a lab order.", MsgBoxStyle.OkOnly, "Entry Error")
            LabOrderCombo1.Select()
            Return False
        End If
        If PayValueSpinEdit.Value = Nothing OrElse PayValueSpinEdit.Value.ToString() = "" Then
            MsgBox("PayValue is required.", MsgBoxStyle.OkOnly, "Entry Error")
            PayValueSpinEdit.Select()
            Return False
        End If
        If PayDateDateEdit.Text = "" Then
            MsgBox("PayDate is required.", MsgBoxStyle.OkOnly, "Entry Error")
            PayDateDateEdit.Select()
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



    Private Sub LabCombo1_LabValueChanged(sender As Object, e As LabCombo.LabIndexChangedEvent) Handles LabCombo1.LabValueChanged
        LabOrderCombo1.SetLabOrderLabFilter(e.LabID)
        LabOrderIDSpinEdit.Value = LabOrderCombo1.LabOrderID
    End Sub

    Private Sub LabOrderCombo1_LabOrderValueChanged(sender As Object, e As LabOrderCombo.LabOrderIndexChangedEvent) Handles LabOrderCombo1.LabOrderValueChanged
        LabOrderIDSpinEdit.Value = e.LabOrderID
    End Sub
End Class
