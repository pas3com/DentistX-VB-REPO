Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms
Imports DentistX

Partial Public Class FrmRecieveLabOrder

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
    Private clsLabOrderData As New LabOrderDATA
    Private clsDentistXData As New DentistXDATA


    Private Sub FrmRecieveLabOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        EnableRecord(False)
        bAdd.Enabled = False
        bEdit.Enabled = False
        bDelete.Enabled = False
        bRefresh.Enabled = False
        butApply.Enabled = False
        butCancel.Enabled = False
    End Sub

    Private Sub FrmRecieveLabOrder_Shown(sender As Object, e As EventArgs) Handles Me.Shown
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
                .DataSource = clsLabOrderData.SelectAll
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
        LabOrderIDSpinEdit.Enabled = False
        colLabCombo.Enabled = False
        colPatientCombo.Enabled = False
        ImpressionCombo1.Enabled = False
        ImprDetCombo1.Enabled = False
        ImpClrsCombo1.Enabled = False
        ImprCountSpinEdit.Enabled = False
        DeliveryDateDateEdit.Enabled = False
        PriceSpinEdit.Enabled = False
        OrderDetailsTextEdit.Enabled = False
        RecieveDateDateEdit.Enabled = YesNo
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

    Private Sub ApplyImprClrFromLabOrder(imprClr As Object)
        Dim s = Convert.ToString(imprClr)
        If String.IsNullOrWhiteSpace(s) Then
            ImpClrsCombo1.Text = String.Empty
        Else
            ImpClrsCombo1.Text = s
        End If
    End Sub

    Private Sub Display()
        ClearRecord()
        Dim clsLabOrder As LabOrder = TryCast(dgView.GetRow(Row), LabOrder)
        If clsLabOrder Is Nothing Then
            clsLabOrder = New LabOrder
            clsLabOrder.LabOrderID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabOrderID))
            clsLabOrder = clsLabOrderData.Select_Record(clsLabOrder)
        End If
        If Not clsLabOrder Is Nothing Then
            Try
                LabOrderIDSpinEdit.Value = System.Convert.ToInt32(clsLabOrder.LabOrderID)
                colLabCombo.Text = Convert.ToString(clsLabOrder.LabName)
                colPatientCombo.Text = Convert.ToString(clsLabOrder.PatientName)
                ImpressionCombo1.Text = Convert.ToString(clsLabOrder.ImprType)
                ImprDetCombo1.Text = Convert.ToString(clsLabOrder.ImprDet)
                ApplyImprClrFromLabOrder(clsLabOrder.ImprClr)
                ImprCountSpinEdit.Value = System.Convert.ToInt32(clsLabOrder.ImprCount)
                If clsLabOrder.DeliveryDate.HasValue Then
                    DeliveryDateDateEdit.DateTime = clsLabOrder.DeliveryDate.Value
                Else
                    DeliveryDateDateEdit.EditValue = Nothing
                End If
                PriceSpinEdit.Value = System.Convert.ToInt32(clsLabOrder.Price)
                OrderDetailsTextEdit.Text = Convert.ToString(clsLabOrder.OrderDetails)
                If clsLabOrder.RecieveDate.HasValue Then
                    RecieveDateDateEdit.DateTime = clsLabOrder.RecieveDate.Value
                Else
                    RecieveDateDateEdit.EditValue = Nothing
                End If
                NotesTextEdit.Text = Convert.ToString(clsLabOrder.Notes)
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
        LabOrderIDSpinEdit.Text = DentistXDATA.getAutoID("New", "LabOrder")

        LabOrderIDSpinEdit.Enabled = False
        colLabCombo.Select()
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
        LabOrderIDSpinEdit.Value = 0
        colLabCombo.Text = Nothing
        colPatientCombo.Text = Nothing
        ImpressionCombo1.Text = Nothing
        ImprDetCombo1.Text = Nothing
        ImpClrsCombo1.Text = Nothing
        ImprCountSpinEdit.Value = 0
        DeliveryDateDateEdit.Text = Nothing
        PriceSpinEdit.Value = 0
        OrderDetailsTextEdit.Text = Nothing
        RecieveDateDateEdit.Text = Nothing
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
    Private Sub InsertRecord()
        MsgBox("Adding lab orders is not supported from this receive form.", MsgBoxStyle.OkOnly, "Info")
    End Sub

    Private Sub UpdateRecord()
        If VerifyData() = True Then
            Dim bSuccess As Boolean
            Dim receiveDate As Nullable(Of DateTime) = Nothing
            If Not (RecieveDateDateEdit.EditValue Is Nothing OrElse RecieveDateDateEdit.EditValue Is DBNull.Value) Then
                receiveDate = RecieveDateDateEdit.DateTime
            End If
            bSuccess = clsLabOrderData.UpdateReceiveInfo(System.Convert.ToInt32(LabOrderIDSpinEdit.Value), receiveDate, System.Convert.ToString(NotesTextEdit.Text))
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsLabOrder As New LabOrder
        clsLabOrder.LabOrderID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabOrderID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsLabOrderData.Delete(clsLabOrder)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        If LabOrderIDSpinEdit.Value <= 0 Then
            MsgBox("Please select a lab order.", MsgBoxStyle.OkOnly, "Entry Error")
            LabOrderIDSpinEdit.Select()
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
