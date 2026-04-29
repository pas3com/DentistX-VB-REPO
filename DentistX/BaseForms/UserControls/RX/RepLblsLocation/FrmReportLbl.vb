
Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.Skins
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base

Public Class FrmReportLbl




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
    Private clsReportLabelData As New ReportLabelDATA
    Private clsDentistXData As New DentistXDATA
    Private clsReportLabel As ReportLabel

    Private bAddMode As Boolean = False
    Private bEditMode As Boolean = False
    Private bDeleteMode As Boolean = False
    Private iRow As Int32 = 0


    Private _loadedOffsets As List(Of ReportLabel)

    Private Sub FrmReportLabel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SetDefaultLocations()
        'DefaultLocationsTags()
        LoadLabelOffsets()
        LoadDGV()
        ShowToolStripItems("Cancel")
    End Sub

    Private Sub SetDefaultLocations()
        DefaultLocations()
        For Each lbl As LabelControl In A5Panel.Controls.OfType(Of LabelControl)()
            lbl.Location = GetDefaultLocation(lbl.Name) ' CType(lbl.Tag, Point)
        Next
    End Sub
    Private Sub DefaultLocations()
        lblPatientName.Tag = New Point(CInt(77.79 * PixelsPerMm), CInt(15.89 * PixelsPerMm))
        lblAge.Tag = New Point(CInt(4.23 * PixelsPerMm), CInt(15.89 * PixelsPerMm))
        lblSex.Tag = New Point(CInt(77.79 * PixelsPerMm), CInt(22.14 * PixelsPerMm))
        lblRX.Tag = New Point(CInt(20.21 * PixelsPerMm), CInt(33.41 * PixelsPerMm))
        lblRxDate.Tag = New Point(CInt(95.25 * PixelsPerMm), CInt(129.28 * PixelsPerMm))
        lblDrName.Tag = New Point(CInt(4.23 * PixelsPerMm), CInt(129.28 * PixelsPerMm))
    End Sub
    Private Sub LoadLabelOffsets()
        Try
            Dim sql As String =
        "SELECT LblName, OffsetXmm, OffsetYmm
         FROM ReportLabel"
            Dim offsets As List(Of ReportLabel)
            Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                offsets = cn.Query(Of ReportLabel)(sql).ToList()
            End Using

            _loadedOffsets = offsets

            For Each item In offsets

                Dim lbl As LabelControl =
                A5Panel.Controls.OfType(Of LabelControl)().
                    FirstOrDefault(Function(l) l.Name = item.LblName)
                If lbl Is Nothing Then Continue For
                'Dim def As PointF = CType(lbl.Tag, PointF)
                'lbl.Left = CInt(def.X + item.OffsetXmm * PixelsPerMm)
                'lbl.Top = CInt(def.Y + item.OffsetYmm * PixelsPerMm)
                'Dim def = GetDefaultPosFromTag(lbl)

                lbl.Left = CInt(lbl.Left + item.OffsetXmm * PixelsPerMm)
                lbl.Top = CInt(lbl.Top + item.OffsetYmm * PixelsPerMm)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SaveLabelOffsets1()
        Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            cn.Open()
            For Each lbl As LabelControl In A5Panel.Controls.OfType(Of LabelControl)()
                Dim def As Point = CType(lbl.Tag, Point)
                Dim offsetXmm As Decimal =
                Math.Round((lbl.Left - def.X) / PixelsPerMm, 2)
                Dim offsetYmm As Decimal =
                Math.Round((lbl.Top - def.Y) / PixelsPerMm, 2)
                Dim sql As String =
                "UPDATE ReportLabel
                 SET OffsetXmm = @OffsetXmm,
                     OffsetYmm = @OffsetYmm
                 WHERE LblName = @LblName"
                cn.Execute(sql, New With {
                .LblName = lbl.Name,
                .OffsetXmm = offsetXmm,
                .OffsetYmm = offsetYmm
            })
            Next
        End Using
        LoadDGV()
    End Sub

    Private Sub SaveLabelOffsets()
        Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            cn.Open()

            For Each lbl As LabelControl In A5Panel.Controls.OfType(Of LabelControl)()

                Dim def As Point = GetDefaultLocation(lbl.Name)

                Dim offsetXmm As Decimal =
                Math.Round((lbl.Left - def.X) / PixelsPerMm, 2)

                Dim offsetYmm As Decimal =
                Math.Round((lbl.Top - def.Y) / PixelsPerMm, 2)

                Dim sql As String =
                "UPDATE ReportLabel
                 SET OffsetXmm = @OffsetXmm,
                     OffsetYmm = @OffsetYmm
                 WHERE LblName = @LblName"

                cn.Execute(sql, New With {
                .LblName = lbl.Name,
                .OffsetXmm = offsetXmm,
                .OffsetYmm = offsetYmm
            })
            Next
        End Using

        LoadDGV()
    End Sub

    Private Function GetDefaultPosFromTag(lbl As LabelControl) As (X As Decimal, Y As Decimal)

        If lbl.Tag Is Nothing Then Return (0D, 0D)

        lblPatientName.Location = New Point(CInt(77.79 * PixelsPerMm), CInt(15.89 * PixelsPerMm / 2))
        lblAge.Location = New Point(CInt(4.23 * PixelsPerMm), CInt(15.89 * PixelsPerMm / 2))
        lblSex.Location = New Point(CInt(77.79 * PixelsPerMm), CInt(22.14 * PixelsPerMm / 2))
        lblRX.Location = New Point(CInt(20.21 * PixelsPerMm), CInt(33.41 * PixelsPerMm / 2))
        lblRxDate.Location = New Point(CInt(95.25 * PixelsPerMm), CInt(129.28 * PixelsPerMm / 2))
        lblDrName.Location = New Point(CInt(4.23 * PixelsPerMm), CInt(129.28 * PixelsPerMm / 2))

    End Function
    Private Function GetDefaultLocation(lblName As String) As Point
        Select Case lblName
            Case "lblPatientName" : Return New Point(311, 64)'New Point(CInt(77.79 * PixelsPerMm), CInt(15.89 * PixelsPerMm))'
            Case "lblAge" : Return New Point(17, 64)'New Point(CInt(4.23 * PixelsPerMm), CInt(15.89 * PixelsPerMm))'
            Case "lblSex" : Return New Point(311, 92)' 89)'New Point(CInt(77.79 * PixelsPerMm), CInt(22.14 * PixelsPerMm))'
            Case "lblRX" : Return New Point(81, 134)'New Point(CInt(20.21 * PixelsPerMm), CInt(33.41 * PixelsPerMm))'
            Case "lblRxDate" : Return New Point(381, 517)'New Point(CInt(95.25 * PixelsPerMm), CInt(129.28 * PixelsPerMm))'
            Case "lblDrName" : Return New Point(17, 517) 'New Point(CInt(4.23 * PixelsPerMm), CInt(129.28 * PixelsPerMm)) '
                ' add the rest here
            Case Else
                MsgBox("No default location defined for " & lblName)
        End Select
    End Function


#Region "Label Dragging and Positioning"
    ' Paper simulation scale
    Private Const PixelsPerMm As Single = 4.0F   ' 1 mm = 4 px
    Private Const xPixelsPerMm As Single = 4.0F   ' 1 mm = 4 px
    Private Const yPixelsPerMm As Single = 2.0F   ' 1 mm = 4 px
    Private _draggingLabel As LabelControl = Nothing
    Private _dragStartPoint As Point
    Private Sub btnDef_Click(sender As Object, e As EventArgs) Handles btnDef.Click
        SetDefaultLocations()
    End Sub


    Private Sub DefaultLocationsTags()
        lblPatientName.Tag = New Point(77.79, 15.89)
        lblAge.Tag = New Point(4.23, 15.89)
        lblSex.Tag = New Point(77.79, 22.14)
        lblRX.Tag = New Point(20.21, 33.41)
        lblRxDate.Tag = New Point(95.25, 129.28)
        lblDrName.Tag = New Point(4.23, 129.28)
    End Sub

    Private Sub ApplyOffsets(lbl As LabelControl, offsetXmm As Decimal, offsetYmm As Decimal)
        Dim def As Point = CType(lbl.Tag, Point)
        lbl.Left = def.X + CInt(offsetXmm * PixelsPerMm)
        lbl.Top = def.Y + CInt(offsetYmm * PixelsPerMm)
    End Sub

    Private Sub Label_MouseDown(sender As Object, e As MouseEventArgs) Handles lblPatientName.MouseDown,
            lblAge.MouseDown,
            lblSex.MouseDown,
            lblRX.MouseDown,
            lblRxDate.MouseDown,
            lblDrName.MouseDown
        _draggingLabel = CType(sender, LabelControl)
        _dragStartPoint = e.Location
    End Sub
    Private Sub Label_MouseMove(sender As Object, e As MouseEventArgs) _
    Handles lblPatientName.MouseMove,
            lblAge.MouseMove,
            lblSex.MouseMove,
            lblRX.MouseMove,
            lblRxDate.MouseMove,
            lblDrName.MouseMove

        'If _draggingLabel Is Nothing Then Exit Sub
        'Dim dx = e.X - _dragStartPoint.X
        'Dim dy = e.Y - _dragStartPoint.Y
        '_draggingLabel.Left += dx
        '_draggingLabel.Top += dy
        'UpdateSpinEdits(_draggingLabel)
        If _draggingLabel Is Nothing Then Exit Sub

        Dim dxPx = e.X - _dragStartPoint.X
        Dim dyPx = e.Y - _dragStartPoint.Y

        _draggingLabel.Left += dxPx
        _draggingLabel.Top += dyPx

        Dim item = _loadedOffsets.
            First(Function(x) x.LblName = _draggingLabel.Name)

        item.OffsetXmm += dxPx / PixelsPerMm
        item.OffsetYmm += dyPx / yPixelsPerMm

        XSpinEdit.EditValue = Math.Round(item.OffsetXmm, 2)
        YSpinEdit.EditValue = Math.Round(item.OffsetYmm, 2)
    End Sub
    Private Sub Label_MouseUp(sender As Object, e As MouseEventArgs) _
    Handles lblPatientName.MouseUp,
            lblAge.MouseUp,
            lblSex.MouseUp,
            lblRX.MouseUp,
            lblRxDate.MouseUp,
            lblDrName.MouseUp
        _draggingLabel = Nothing
    End Sub
    Private Sub UpdateSpinEdits(lbl As LabelControl)
        'Dim def As Point = CType(lbl.Tag, Point)
        'Dim offsetXmm As Decimal =
        '(lbl.Left - def.X) / PixelsPerMm
        'Dim offsetYmm As Decimal =
        '(lbl.Top - def.Y) / PixelsPerMm
        'XSpinEdit.EditValue = Math.Round(offsetXmm, 2)
        'YSpinEdit.EditValue = Math.Round(offsetYmm, 2)
        Dim item = _loadedOffsets.
       FirstOrDefault(Function(x) x.LblName = lbl.Name)

        If item Is Nothing Then Exit Sub

        Dim offsetXmm As Decimal =
            Math.Round(item.OffsetXmm, 2)

        Dim offsetYmm As Decimal =
            Math.Round(item.OffsetYmm, 2)

        XSpinEdit.EditValue = offsetXmm
        YSpinEdit.EditValue = offsetYmm
    End Sub
    Private SelectedLabel As LabelControl = Nothing


    'Private Sub XSpinEdit_EditValueChanged(sender As Object, e As EventArgs) _
    'Handles XSpinEdit.EditValueChanged, YSpinEdit.EditValueChanged
    '    'If SelectedLabel Is Nothing Then Exit Sub
    '    'Dim def As Point = CType(SelectedLabel.Tag, Point)
    '    'Dim offsetXmm As Decimal = CDec(XSpinEdit.EditValue)
    '    'Dim offsetYmm As Decimal = CDec(YSpinEdit.EditValue)
    '    'SelectedLabel.Left = def.X + CInt(offsetXmm * PixelsPerMm)
    '    'SelectedLabel.Top = def.Y + CInt(offsetYmm * PixelsPerMm)
    '    If SelectedLabel Is Nothing Then Exit Sub

    '    Dim item = _loadedOffsets.
    '        First(Function(x) x.LblName = SelectedLabel.Name)

    '    item.OffsetXmm = CDec(XSpinEdit.EditValue)
    '    item.OffsetYmm = CDec(YSpinEdit.EditValue)

    '    SelectedLabel.Left =
    '        SelectedLabel.Left - CInt(item.OffsetXmm * PixelsPerMm) +
    '        CInt(item.OffsetXmm * PixelsPerMm)

    '    SelectedLabel.Top =
    '        SelectedLabel.Top - CInt(item.OffsetYmm * PixelsPerMm) +
    '        CInt(item.OffsetYmm * PixelsPerMm)
    'End Sub

    Private Sub XSpinEdit_EditValueChanged(sender As Object, e As EventArgs) _
    Handles XSpinEdit.EditValueChanged

        If SelectedLabel Is Nothing Then Exit Sub

        Dim def As Point = GetDefaultLocation(SelectedLabel.Name)

        Dim offsetXmm As Decimal = CDec(XSpinEdit.EditValue)

        SelectedLabel.Left =
        CInt(def.X + offsetXmm * PixelsPerMm)
    End Sub

    Private Sub YSpinEdit_EditValueChanged(sender As Object, e As EventArgs) _
    Handles YSpinEdit.EditValueChanged

        If SelectedLabel Is Nothing Then Exit Sub

        Dim def As Point = GetDefaultLocation(SelectedLabel.Name)

        Dim offsetYmm As Decimal = CDec(YSpinEdit.EditValue)

        SelectedLabel.Top =
        CInt(def.Y + offsetYmm * yPixelsPerMm)
    End Sub


    Private Sub Label_Click(sender As Object, e As EventArgs) _
    Handles lblRxDate.Click, lblSex.Click, lblDrName.Click, lblAge.Click, lblPatientName.Click, lblRX.Click
        Try
            Labl.Text = DirectCast(sender, LabelControl).Location.ToString()
            ResetLabelColors()
            DirectCast(sender, LabelControl).BackColor = Color.LightBlue
            SelectedLabel = DirectCast(sender, LabelControl)

            lblNameEdit.Text = SelectedLabel.Name
            RemoveHandler XSpinEdit.EditValueChanged, AddressOf XSpinEdit_EditValueChanged
            RemoveHandler YSpinEdit.EditValueChanged, AddressOf YSpinEdit_EditValueChanged
            UpdateSpinEdits(SelectedLabel)
            AddHandler YSpinEdit.EditValueChanged, AddressOf YSpinEdit_EditValueChanged
            AddHandler XSpinEdit.EditValueChanged, AddressOf XSpinEdit_EditValueChanged

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub A5Panel_Click(sender As Object, e As EventArgs) Handles A5Panel.Click
        SelectedLabel = Nothing
    End Sub



    'Private Sub XSpinEdit_EditValueChanged(sender As Object, e As EventArgs) Handles XSpinEdit.EditValueChanged
    '    If slctedLabel IsNot Nothing Then
    '        Dim p As Point = DirectCast(slctedLabel.Tag, Point)
    '        p.X = Convert.ToSingle(XSpinEdit.Value)
    '        slctedLabel.Tag = p
    '        Select Case slctedLabel.Name
    '            Case "lblPatientName"
    '                lblPatientName.Location = New Point(p.X, lblPatientName.Location.Y)
    '            Case "lblAge"
    '                lblAge.Location = New Point(p.X, lblAge.Location.Y)
    '            Case "lblSex"
    '                lblSex.Location = New Point(p.X, lblSex.Location.Y)
    '            Case "lblRX"
    '                lblRX.Location = New Point(p.X, lblRX.Location.Y)
    '            Case "lblRxDate"
    '                lblRxDate.Location = New Point(p.X, lblRxDate.Location.Y)
    '            Case "lblDrName"
    '                lblDrName.Location = New Point(p.X, lblDrName.Location.Y)
    '        End Select
    '    End If
    'End Sub

    'Private Sub YSpinEdit_EditValueChanged(sender As Object, e As EventArgs) Handles YSpinEdit.EditValueChanged
    '    If slctedLabel IsNot Nothing Then
    '        Dim p As Point = DirectCast(slctedLabel.Tag, Point)
    '        p.Y = Convert.ToSingle(YSpinEdit.Value)
    '        slctedLabel.Tag = p
    '        Select Case slctedLabel.Name
    '            Case "lblPatientName"
    '                lblPatientName.Location = New Point(lblPatientName.Location.X, p.Y)
    '            Case "lblAge"
    '                lblAge.Location = New Point(lblAge.Location.X, p.Y)
    '            Case "lblSex"
    '                lblSex.Location = New Point(lblSex.Location.X, p.Y)
    '            Case "lblRX"
    '                lblRX.Location = New Point(lblRX.Location.X, p.Y)
    '            Case "lblRxDate"
    '                lblRxDate.Location = New Point(lblRxDate.Location.X, p.Y)
    '            Case "lblDrName"
    '                lblDrName.Location = New Point(lblDrName.Location.X, p.Y)
    '        End Select
    '    End If
    'End Sub
    Private Sub ResetLabelColors()
        lblPatientName.BackColor = Color.Transparent
        lblAge.BackColor = Color.Transparent
        lblSex.BackColor = Color.Transparent
        lblRX.BackColor = Color.Transparent
        lblRxDate.BackColor = Color.Transparent
        lblDrName.BackColor = Color.Transparent
    End Sub

#End Region
    Public Property RepLbls() As IEnumerable(Of ReportLabel)


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
                .DataSource = clsReportLabelData.SelectAll
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
        lblNameEdit.Enabled = YesNo
        'XSpinEdit.Enabled = YesNo
        'YSpinEdit.Enabled = YesNo
    End Sub


    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        RepLbls = clsReportLabelData.SelectAll
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        SaveLabelOffsets()

        'Select Case btnUpdate.Text
        '    Case "Add"
        '        Add()
        '        btnUpdate.Text = "Apply Add"
        '    Case "Apply Add"
        '        Me.InsertRecord()
        '        ClearRecord()
        '        btnUpdate.Text = "Add"
        '        Me.AddMode = False
        'End Select
    End Sub




    Private Sub Display()
        ClearRecord()
        Dim clsReportLabel As New ReportLabel
        clsReportLabel.LblID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colCtlID))
        clsReportLabel = clsReportLabelData.Select_Record(clsReportLabel)
        If Not clsReportLabel Is Nothing Then
            Try
                CtlIDSpinEdit.Value = System.Convert.ToInt32(clsReportLabel.LblID)
                lblNameEdit.Text = Convert.ToString(clsReportLabel.LblName)
                XSpinEdit.Value = System.Convert.ToInt32(clsReportLabel.OffsetXmm)
                YSpinEdit.Value = System.Convert.ToInt32(clsReportLabel.OffsetYmm)
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
        CtlIDSpinEdit.Text = DentistXDATA.getAutoID("New", "ReportLabel")
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
        lblNameEdit.Text = Nothing
        RemoveHandler XSpinEdit.EditValueChanged, AddressOf XSpinEdit_EditValueChanged
        XSpinEdit.Text = Nothing
        AddHandler XSpinEdit.EditValueChanged, AddressOf XSpinEdit_EditValueChanged
        RemoveHandler YSpinEdit.EditValueChanged, AddressOf YSpinEdit_EditValueChanged
        YSpinEdit.Text = Nothing
        AddHandler YSpinEdit.EditValueChanged, AddressOf YSpinEdit_EditValueChanged
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
    Private Sub SetData(ByVal clsReportLabel As ReportLabel)
        With clsReportLabel
            .LblID = System.Convert.ToInt32(CtlIDSpinEdit.Value)
            .LblName = System.Convert.ToString(lblNameEdit.Text)
            .OffsetXmm = System.Convert.ToInt32(XSpinEdit.Value)
            .OffsetYmm = System.Convert.ToInt32(YSpinEdit.Value)
        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsReportLabel As New ReportLabel
        If VerifyData() = True Then
            SetData(clsReportLabel)
            Dim bSuccess As Boolean
            bSuccess = clsReportLabelData.Add(clsReportLabel)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsReportLabel As New ReportLabel
        Dim clsReportLabel As New ReportLabel
        oclsReportLabel.LblID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colCtlID))
        oclsReportLabel = clsReportLabelData.Select_Record(oclsReportLabel)
        If VerifyData() = True Then
            SetData(clsReportLabel)
            Dim bSuccess As Boolean
            bSuccess = clsReportLabelData.Update(oclsReportLabel, clsReportLabel)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsReportLabel As New ReportLabel
        clsReportLabel.LblID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colCtlID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsReportLabelData.Delete(clsReportLabel)
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
        If lblNameEdit.Text = "" Then
            MsgBox("Ctl is required.", MsgBoxStyle.OkOnly, "Entry Error")
            lblNameEdit.Select()
            Return False
        End If
        Dim x As Decimal = CDec(XSpinEdit.EditValue)
        If x < -50 Or x > 50 Then
            MsgBox("X offset must be between -50 and 50 mm.", MsgBoxStyle.OkOnly, "Entry Error")
            XSpinEdit.Select()
            Return False
        End If
        Dim y As Decimal = CDec(YSpinEdit.EditValue)
        If y < -50 Or y > 50 Then
            MsgBox("Y offset must be between -50 and 50 mm.", MsgBoxStyle.OkOnly, "Entry Error")
            YSpinEdit.Select()
            Return False
        End If
        'If XSpinEdit.Value = Nothing OrElse Not IsNumeric(CDec(XSpinEdit.EditValue)) Then
        '    MsgBox("X is required.", MsgBoxStyle.OkOnly, "Entry Error")
        '    XSpinEdit.Select()
        '    Return False
        'End If
        'If YSpinEdit.Value = Nothing OrElse Not IsNumeric(CDec(YSpinEdit.EditValue)) Then
        '    MsgBox("Y is required.", MsgBoxStyle.OkOnly, "Entry Error")
        '    YSpinEdit.Select()
        '    Return False
        'End If
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
