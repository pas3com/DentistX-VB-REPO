Imports System.ComponentModel
Imports System.Collections.Generic
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms
Imports DentistX

Partial Public Class FrmLabOrder2


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


    Private Sub FrmLabOrder2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDGV()
        ShowToolStripItems("Cancel")
        'If colLabCombo.CboLab.Properties.Items.Count > 0 Then
        '    colLabCombo.CboLab.SelectedIndex = 0
        'End If
        'If colPatientCombo.CboPatient.Properties.Items.Count > 0 Then
        '    colPatientCombo.CboPatient.SelectedIndex = 0
        'End If
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
        LabOrderIDSpinEdit.Enabled = YesNo
        colLabCombo.Enabled = YesNo
        colPatientCombo.Enabled = YesNo
        ImpressionCombo1.Enabled = YesNo
        ImprDetCombo1.Enabled = YesNo
        ImpClrsCombo1.Enabled = YesNo
        ImprCountSpinEdit.Enabled = YesNo
        DeliveryDateDateEdit.Enabled = YesNo
        PriceSpinEdit.Enabled = YesNo
        OrderDetailsTextEdit.Enabled = YesNo
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
            ImpClrsCombo1.SetSelectedImpClr(-1)
        Else
            ImpClrsCombo1.SetSelectedImpClrID(s)
        End If
    End Sub

    Private Sub Display()
        ClearRecord()
        Dim clsLabOrder As New LabOrder
        clsLabOrder.LabOrderID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabOrderID))
        clsLabOrder = clsLabOrderData.Select_Record(clsLabOrder)
        If Not clsLabOrder Is Nothing Then
            Try
                LabOrderIDSpinEdit.Value = System.Convert.ToInt32(clsLabOrder.LabOrderID)
                colLabCombo.SetSelectedLabName(System.Convert.ToInt32(clsLabOrder.LabID))
                colPatientCombo.SetCurrentPatientName(System.Convert.ToInt32(clsLabOrder.PatientID))
                ImpressionCombo1.SetSelectedImprID(Convert.ToString(clsLabOrder.ImprType))
                ImprDetCombo1.SetSelectedImprDetail(Convert.ToString(clsLabOrder.ImprDet))
                ApplyImprClrFromLabOrder(clsLabOrder.ImprClr)
                ImprCountSpinEdit.Value = System.Convert.ToInt32(clsLabOrder.ImprCount)
                DeliveryDateDateEdit.Text = System.Convert.ToDateTime(clsLabOrder.DeliveryDate)
                PriceSpinEdit.Value = System.Convert.ToInt32(clsLabOrder.Price)
                RefreshOrderDetailsFromImpressionControls()
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
        colLabCombo.SetSelectedLabName(-1)
        colLabCombo.LabName = Nothing
        colPatientCombo.SetCurrentPatientName(-1)
        ImpressionCombo1.ImprID = -1
        ImprDetCombo1.ImpDetID = -1
        ImpClrsCombo1.SetSelectedImpClr(-1)
        ImprCountSpinEdit.Value = 0
        DeliveryDateDateEdit.Text = Nothing
        PriceSpinEdit.Value = 0
        OrderDetailsTextEdit.Text = Nothing
        RecieveDateDateEdit.Text = Nothing
        NotesTextEdit.Text = Nothing
        RefreshOrderDetailsFromImpressionControls()
    End Sub

    Private Shared Function IsUsableOrderDetailsPart(value As String) As Boolean
        If String.IsNullOrWhiteSpace(value) Then Return False
        Dim t = value.Trim()
        If t = "-1" OrElse t = "0" Then Return False
        Return True
    End Function

    ''' <summary>Builds a single line for OrderDetails from impression type, detail, shade/color, and count (max 250 chars).</summary>
    Private Function BuildOrderDetailsSummary() As String
        Dim parts As New List(Of String)()
        If IsUsableOrderDetailsPart(ImpressionCombo1.ImprType) Then
            parts.Add(ImpressionCombo1.ImprType.Trim())
        End If
        If IsUsableOrderDetailsPart(ImprDetCombo1.ImprDetail) Then
            parts.Add(ImprDetCombo1.ImprDetail.Trim())
        End If
        Dim impClrPart = If(ImpClrsCombo1.ImpClr, String.Empty)
        If IsUsableOrderDetailsPart(impClrPart) Then
            parts.Add(impClrPart.Trim())
        End If
        Dim qty = CInt(ImprCountSpinEdit.Value)
        If qty > 0 Then
            parts.Add("×" & qty.ToString())
        End If
        Dim s = String.Join(" • ", parts)
        Const maxLen As Integer = 250
        If s.Length > maxLen Then
            s = s.Substring(0, maxLen)
        End If
        Return s
    End Function

    Private Sub RefreshOrderDetailsFromImpressionControls()
        OrderDetailsTextEdit.Text = BuildOrderDetailsSummary()
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
    Private Sub SetData(ByVal clsLabOrder As LabOrder)
        With clsLabOrder
            .LabOrderID = System.Convert.ToInt32(LabOrderIDSpinEdit.Value)
            .LabID = System.Convert.ToInt32(colLabCombo.LabID)
            .LabName = System.Convert.ToString(colLabCombo.LabName)
            .PatientID = System.Convert.ToInt32(colPatientCombo.PatientID)
            .PatientName = System.Convert.ToString(colPatientCombo.PatientName)
            .ImprType = System.Convert.ToString(ImpressionCombo1.ImprType)
            .ImprDet = System.Convert.ToString(ImprDetCombo1.ImprDetail)
            .ImprClr = System.Convert.ToString(ImpClrsCombo1.ImpClr)
            .ImprCount = System.Convert.ToInt32(ImprCountSpinEdit.Value)
            .DeliveryDate = System.Convert.ToDateTime(DeliveryDateDateEdit.DateTime)
            .Price = System.Convert.ToInt32(PriceSpinEdit.Value)
            .OrderDetails = System.Convert.ToString(OrderDetailsTextEdit.Text)
            If RecieveDateDateEdit.EditValue Is Nothing OrElse RecieveDateDateEdit.EditValue Is DBNull.Value Then
                .RecieveDate = Nothing
            Else
                .RecieveDate = RecieveDateDateEdit.DateTime
            End If
            .Notes = System.Convert.ToString(NotesTextEdit.Text)
        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsLabOrder As New LabOrder
        If VerifyData() = True Then
            SetData(clsLabOrder)
            Dim bSuccess As Boolean
            bSuccess = clsLabOrderData.Add(clsLabOrder)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsLabOrder As New LabOrder
        Dim clsLabOrder As New LabOrder
        oclsLabOrder.LabOrderID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colLabOrderID))
        oclsLabOrder = clsLabOrderData.Select_Record(oclsLabOrder)
        If VerifyData() = True Then
            SetData(clsLabOrder)
            Dim bSuccess As Boolean
            bSuccess = clsLabOrderData.Update(oclsLabOrder, clsLabOrder)
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

    Private Shared Function IsNullOrDbNull(ByVal value As Object) As Boolean
        Return value Is Nothing OrElse IsDBNull(value)
    End Function

    Private Shared Function TryGetDateTime(ByVal value As Object, ByRef result As DateTime) As Boolean
        result = DateTime.MinValue
        If IsNullOrDbNull(value) Then Return False
        If TypeOf value Is DateTime Then
            result = DirectCast(value, DateTime)
            Return True
        End If
        If TypeOf value Is String Then
            Dim s = DirectCast(value, String)
            If String.IsNullOrWhiteSpace(s) Then Return False
            Return DateTime.TryParse(s, result)
        End If
        Try
            result = Convert.ToDateTime(value)
            Return True
        Catch
            Return False
        End Try
    End Function

    Private Shared Function IsSpinEditValueMissing(ByVal spin As DevExpress.XtraEditors.SpinEdit) As Boolean
        Dim v = spin.EditValue
        If IsNullOrDbNull(v) Then Return True
        Dim s = TryCast(v, String)
        If s IsNot Nothing Then Return String.IsNullOrWhiteSpace(s)
        Return False
    End Function

    Private Function VerifyData() As Boolean
        ' Check required fields
        If IsSpinEditValueMissing(LabOrderIDSpinEdit) Then
            MsgBox("LabOrderID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            LabOrderIDSpinEdit.Select()
            Return False
        End If
        If colLabCombo.LabID <= 0 OrElse String.IsNullOrWhiteSpace(colLabCombo.LabName) Then
            MsgBox("Please select a lab.", MsgBoxStyle.OkOnly, "Entry Error")
            colLabCombo.Select()
            Return False
        End If
        If colPatientCombo.PatientID <= 0 OrElse String.IsNullOrWhiteSpace(colPatientCombo.PatientName) Then
            MsgBox("PatientID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            colPatientCombo.Select()
            Return False
        End If
        'If ImpressionCombo1.Text = "" Then
        '    MsgBox("ImprType is required.", MsgBoxStyle.OkOnly, "Entry Error")
        '    ImpressionCombo1.Select()
        '    Return False
        'End If
        If OrderDetailsTextEdit.Text = "" Then
            MsgBox("Order Details is required.", MsgBoxStyle.OkOnly, "Entry Error")
            OrderDetailsTextEdit.Select()
            Return False
        End If
        ''If ImpClrsCombo1.Text = "" Then
        ''    MsgBox("ImprClr is required.", MsgBoxStyle.OkOnly, "Entry Error")
        ''    ImpClrsCombo1.Select()
        ''    Return False
        ''End If
        'If ImprCountSpinEdit.Value = Nothing OrElse ImprCountSpinEdit.Value.ToString() = "" Then
        '    MsgBox("ImprCount is required.", MsgBoxStyle.OkOnly, "Entry Error")
        '    ImprCountSpinEdit.Select()
        '    Return False
        'End If
        Dim deliveryDate As DateTime
        If Not TryGetDateTime(DeliveryDateDateEdit.EditValue, deliveryDate) Then
            MsgBox("Delivery Date is required.", MsgBoxStyle.OkOnly, "Entry Error")
            DeliveryDateDateEdit.Select()
            Return False
        End If
        Dim receiveDate As DateTime
        If TryGetDateTime(RecieveDateDateEdit.EditValue, receiveDate) Then
            If receiveDate.Date < deliveryDate.Date Then
                MsgBox("Recieve Date Cannot Be Before Delivery Date .", MsgBoxStyle.OkOnly, "Entry Error")
                DeliveryDateDateEdit.Select()
                Return False
            End If
        End If
        If IsSpinEditValueMissing(PriceSpinEdit) Then
            MsgBox("Price is required.", MsgBoxStyle.OkOnly, "Entry Error")
            PriceSpinEdit.Select()
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


    Private Sub ImpressionCombo1_ImpressionValueChanged(sender As Object, e As ImpressionCombo.ImpressionIndexChangedEvent) Handles ImpressionCombo1.ImpressionValueChanged
        ImprDetCombo1.UpdateImpComboByParentID(e.ImprID)
        RefreshOrderDetailsFromImpressionControls()
    End Sub

    Private Sub ImprDetCombo1_ImprDetValueChanged(sender As Object, e As ImprDetCombo.ImprDetIndexChangedEvent) Handles ImprDetCombo1.ImprDetValueChanged
        RefreshOrderDetailsFromImpressionControls()
    End Sub

    Private Sub ImpClrsCombo1_ImpClrsValueChanged(sender As Object, e As ImpClrsCombo.ImpClrsIndexChangedEvent) Handles ImpClrsCombo1.ImpClrsValueChanged
        RefreshOrderDetailsFromImpressionControls()
    End Sub

    Private Sub ImprCountSpinEdit_EditValueChanged(sender As Object, e As EventArgs) Handles ImprCountSpinEdit.EditValueChanged
        RefreshOrderDetailsFromImpressionControls()
    End Sub
End Class
