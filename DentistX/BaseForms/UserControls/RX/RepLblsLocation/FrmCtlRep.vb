Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

Partial Public Class FrmCtlRep


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
    Private clsCtlRepData As New CtlRepDATA
    Private clsDentistXData As New DentistXDATA
    Private clsCtlRep As CtlRep

    Private bAddMode As Boolean = False
    Private bEditMode As Boolean = False
    Private bDeleteMode As Boolean = False
    Private iRow As Int32 = 0

    Private Sub FrmCtlRep_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DefaultLocations()
        LoadDGV()
        ShowToolStripItems("Cancel")
    End Sub

    Private Sub DefaultLocations()
        lblPatientName.Tag = New Point(777.88, 158.93)
        lblAge.Tag = New Point(42.33, 158.93)
        lblSex.Tag = New Point(777.88, 221.35)
        lblRX.Tag = New Point(202.08, 334.1)
        lblRxDate.Tag = New Point(42.33, 1292.77)
        lblDrName.Tag = New Point(952.5, 1292.77)
    End Sub
    Private Sub ResetLabelColors()
        lblPatientName.BackColor = Color.Transparent
        lblAge.BackColor = Color.Transparent
        lblSex.BackColor = Color.Transparent
        lblRX.BackColor = Color.Transparent
        lblRxDate.BackColor = Color.Transparent
        lblDrName.BackColor = Color.Transparent
    End Sub
    Private slctedLabel As LabelControl = Nothing
    Private Sub Label_Click(sender As Object, e As EventArgs) _
    Handles lblRxDate.Click, lblSex.Click, lblDrName.Click, lblAge.Click, lblPatientName.Click, lblRX.Click
        Try
            ResetLabelColors()
            DirectCast(sender, LabelControl).BackColor = Color.LightBlue
            Dim lbl As LabelControl = DirectCast(sender, LabelControl)
            slctedLabel = lbl
            If lbl.Tag IsNot Nothing AndAlso TypeOf lbl.Tag Is Point Then
                Dim p As Point = DirectCast(lbl.Tag, Point)
                RemoveHandler XSpinEdit.EditValueChanged, AddressOf XSpinEdit_EditValueChanged
                RemoveHandler YSpinEdit.EditValueChanged, AddressOf YSpinEdit_EditValueChanged
                XSpinEdit.Value = CDec(p.X)
                YSpinEdit.Value = CDec(p.Y)
                AddHandler XSpinEdit.EditValueChanged, AddressOf XSpinEdit_EditValueChanged
                AddHandler YSpinEdit.EditValueChanged, AddressOf YSpinEdit_EditValueChanged
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub XSpinEdit_EditValueChanged(sender As Object, e As EventArgs) Handles XSpinEdit.EditValueChanged
        If slctedLabel IsNot Nothing Then
            Dim p As Point = DirectCast(slctedLabel.Tag, Point)
            p.X = Convert.ToSingle(XSpinEdit.Value)
            slctedLabel.Tag = p
            Select Case slctedLabel.Name
                Case "lblPatientName"
                    lblPatientName.Location = New Point(p.X, lblPatientName.Location.Y)
                Case "lblAge"
                    lblAge.Location = New Point(p.X, lblAge.Location.Y)
                Case "lblSex"
                    lblSex.Location = New Point(p.X, lblSex.Location.Y)
                Case "lblRX"
                    lblRX.Location = New Point(p.X, lblRX.Location.Y)
                Case "lblRxDate"
                    lblRxDate.Location = New Point(p.X, lblRxDate.Location.Y)
                Case "lblDrName"
                    lblDrName.Location = New Point(p.X, lblDrName.Location.Y)
            End Select
        End If
    End Sub

    Private Sub YSpinEdit_EditValueChanged(sender As Object, e As EventArgs) Handles YSpinEdit.EditValueChanged
        If slctedLabel IsNot Nothing Then
            Dim p As Point = DirectCast(slctedLabel.Tag, Point)
            p.Y = Convert.ToSingle(YSpinEdit.Value)
            slctedLabel.Tag = p
            Select Case slctedLabel.Name
                Case "lblPatientName"
                    lblPatientName.Location = New Point(lblPatientName.Location.X, p.Y)
                Case "lblAge"
                    lblAge.Location = New Point(lblAge.Location.X, p.Y)
                Case "lblSex"
                    lblSex.Location = New Point(lblSex.Location.X, p.Y)
                Case "lblRX"
                    lblRX.Location = New Point(lblRX.Location.X, p.Y)
                Case "lblRxDate"
                    lblRxDate.Location = New Point(lblRxDate.Location.X, p.Y)
                Case "lblDrName"
                    lblDrName.Location = New Point(lblDrName.Location.X, p.Y)
            End Select
        End If
    End Sub




    Public Property RepLbls() As IEnumerable(Of CtlRep)


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
                .DataSource = clsCtlRepData.SelectAll
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
        CtlTextEdit.Enabled = YesNo
        XSpinEdit.Enabled = YesNo
        YSpinEdit.Enabled = YesNo
    End Sub


    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Select Case btnDel.Text
            Case "Delete"
                Delete()
                btnDel.Text = "Apply Delete"
            Case "Apply Delete"
                Me.DeleteRecord()
                ClearRecord()
                btnDel.Text = "Delete"
                Me.DeleteMode = False
        End Select
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Select Case btnEdit.Text
            Case "Edit"
                Edit()
                btnEdit.Text = "Apply Edit"
            Case "Apply Edit"
                Me.UpdateRecord()
                ClearRecord()
                btnEdit.Text = "Edit"
                Me.EditMode = False
        End Select
    End Sub
    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        RepLbls = clsCtlRepData.SelectAll
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Select Case btnAdd.Text
            Case "Add"
                Add()
                btnAdd.Text = "Apply Add"
            Case "Apply Add"
                Me.InsertRecord()
                ClearRecord()
                btnAdd.Text = "Add"
                Me.AddMode = False
        End Select
    End Sub




    Private Sub Display()
        ClearRecord()
        Dim clsCtlRep As New CtlRep
        clsCtlRep.CtlID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colCtlID))
        clsCtlRep = clsCtlRepData.Select_Record(clsCtlRep)
        If Not clsCtlRep Is Nothing Then
            Try
                CtlIDSpinEdit.Value = System.Convert.ToInt32(clsCtlRep.CtlID)
                CtlTextEdit.Text = Convert.ToString(clsCtlRep.Ctl)
                XSpinEdit.Value = System.Convert.ToInt32(clsCtlRep.X)
                YSpinEdit.Value = System.Convert.ToInt32(clsCtlRep.Y)
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
        CtlIDSpinEdit.Select()
        CtlIDSpinEdit.Text = DentistXDATA.getAutoID("New", "CtlRep")
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
        CtlIDSpinEdit.Value = 0
        CtlTextEdit.Text = Nothing
        XSpinEdit.Text = Nothing
        YSpinEdit.Text = Nothing
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
    Private Sub SetData(ByVal clsCtlRep As CtlRep)
        With clsCtlRep
            .CtlID = System.Convert.ToInt32(CtlIDSpinEdit.Value)
            .Ctl = System.Convert.ToString(CtlTextEdit.Text)
            .X = System.Convert.ToInt32(XSpinEdit.Value)
            .Y = System.Convert.ToInt32(YSpinEdit.Value)
        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsCtlRep As New CtlRep
        If VerifyData() = True Then
            SetData(clsCtlRep)
            Dim bSuccess As Boolean
            bSuccess = clsCtlRepData.Add(clsCtlRep)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsCtlRep As New CtlRep
        Dim clsCtlRep As New CtlRep
        oclsCtlRep.CtlID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colCtlID))
        oclsCtlRep = clsCtlRepData.Select_Record(oclsCtlRep)
        If VerifyData() = True Then
            SetData(clsCtlRep)
            Dim bSuccess As Boolean
            bSuccess = clsCtlRepData.Update(oclsCtlRep, clsCtlRep)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsCtlRep As New CtlRep
        clsCtlRep.CtlID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colCtlID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsCtlRepData.Delete(clsCtlRep)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields
        If CtlIDSpinEdit.Value = Nothing OrElse CtlIDSpinEdit.Value.ToString() = "" Then
            MsgBox("CtlID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            CtlIDSpinEdit.Select()
            Return False
        End If
        If CtlTextEdit.Text = "" Then
            MsgBox("Ctl is required.", MsgBoxStyle.OkOnly, "Entry Error")
            CtlTextEdit.Select()
            Return False
        End If
        If XSpinEdit.Value = Nothing OrElse XSpinEdit.Value.ToString() = "" Then
            MsgBox("X is required.", MsgBoxStyle.OkOnly, "Entry Error")
            XSpinEdit.Select()
            Return False
        End If
        If YSpinEdit.Value = Nothing OrElse YSpinEdit.Value.ToString() = "" Then
            MsgBox("Y is required.", MsgBoxStyle.OkOnly, "Entry Error")
            YSpinEdit.Select()
            Return False
        End If
        Return True
    End Function

    Private Sub ShowToolStripItems(ByVal Item As String)

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

                    DGV.Focus()
                End If
            Case "Refresh"

                LoadDGV()
            Case "No Record"

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


End Class
