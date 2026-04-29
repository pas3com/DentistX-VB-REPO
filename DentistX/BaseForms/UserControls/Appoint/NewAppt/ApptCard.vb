Imports System
Imports System.Drawing
Imports System.Windows.Forms

''' <summary>Week view and day drawer: styled fields via plain <see cref="Label"/> controls (same layout model as <see cref="ApptCardCtl"/>).</summary>
Partial Public Class ApptCard
    Inherits UserControl

    Private _currentModel As ApptCardVm
    Private _use24Hour As Boolean
    Private _indicatorColor As Color = Color.Transparent
    Private _measuredContentHeightInRoot As Integer
    ''' <summary>pnlRoot.ClientSize.Width after the last <see cref="LayoutAll"/> — skips redundant layout in week height fit when only <see cref="Height"/> must change.</summary>
    Private _layoutAllForClientWidth As Integer = -1
    Private Const TimeGap As Integer = 4

    Public Sub New()
        ' ResizeRedraw off: reduces full invalidates on width changes; layout in ApplyContentHeight/Resize still updates children.
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        UpdateStyles()
        DoubleBuffered = True
        InitializeComponent()
        ApptTheme.SetControlDoubleBuffered(pnlRoot)
        startLbl.AutoEllipsis = False
        endLbl.AutoEllipsis = False
        patientnameLbl.AutoEllipsis = False
        doctorLbl.AutoEllipsis = False
        reasonLbl.AutoEllipsis = False
        notesLbl.AutoEllipsis = False
        For Each c As Label In New Label() {startLbl, endLbl, patientnameLbl, doctorLbl, reasonLbl, notesLbl}
            c.UseCompatibleTextRendering = False
        Next
        ' Match ApptCardCtl time label padding
        startLbl.Padding = New Padding(5, 1, 5, 1)
        endLbl.Padding = New Padding(5, 1, 5, 1)
        AddHandler Resize, AddressOf ApptCard_Resize
        WireClickRelay(pnlRoot)
        WireClickRelay(Me)
        For Each c In New Control() {accentLBl, startLbl, endLbl, patientnameLbl, doctorLbl, reasonLbl, notesLbl, statusLbl}
            WireClickRelay(c)
        Next
        WireDragRelay(pnlRoot)
        WireDragRelay(Me)
        For Each c In New Control() {accentLBl, startLbl, endLbl, patientnameLbl, doctorLbl, reasonLbl, notesLbl, statusLbl}
            WireDragRelay(c)
        Next
        AddHandler statusLbl.MouseUp, AddressOf StatusLabel_MouseUp
    End Sub

    Public Event AppointmentClicked As Action(Of AppointmentC)
    Public Event AppointmentDoubleClicked As Action(Of AppointmentC)
    Public Event StatusContextEditRequested As Action(Of AppointmentC)
    Public Event StatusChangeRequested As Action(Of AppointmentC, String, Color)
    Public Event CardDragMouseDown As Action(Of ApptCard, MouseEventArgs)
    Public Event CardDragMouseUp As Action(Of ApptCard, MouseEventArgs)
    Public Event CardDragMouseMove As Action(Of ApptCard, MouseEventArgs)

    Public ReadOnly Property BoundAppointment As AppointmentC
        Get
            Return If(_currentModel Is Nothing, Nothing, _currentModel.Appointment)
        End Get
    End Property

    Public Property IndicatorColor As Color
        Get
            Return _indicatorColor
        End Get
        Set(value As Color)
            _indicatorColor = value
            Invalidate()
        End Set
    End Property

    Public Sub Bind(model As ApptCardVm, use24Hour As Boolean)
        _currentModel = model
        _use24Hour = use24Hour
        If model Is Nothing OrElse model.Appointment Is Nothing Then
            Visible = False
            Return
        End If

        Visible = True
        _layoutAllForClientWidth = -1
        startLbl.Text = ApptTheme.FormatAppointmentTime(model.Appointment.StartDateTime, use24Hour)
        endLbl.Text = ApptTheme.FormatAppointmentTime(model.Appointment.EndDateTime, use24Hour)
        patientnameLbl.Text = If(model.PatientName, "")
        doctorLbl.Text = model.DoctorName
        reasonLbl.Text = If(model.Appointment.Reason, "")
        notesLbl.Text = If(model.Appointment.Notes, "")
        statusLbl.Text = ApptTheme.GetAppointmentStatusDisplayText(model.Appointment)
        ApplyCardAppearance(If(model.Appearance, New ApptCardAppearance()))
        LayoutAll()
    End Sub

    Public Sub ReapplyAppearance()
        If _currentModel Is Nothing OrElse _currentModel.Appointment Is Nothing Then Return
        ApplyCardAppearance(If(_currentModel.Appearance, New ApptCardAppearance()))
        LayoutAll()
    End Sub

    Public Sub ApplyUniformTextColors(foreColor As Color, Optional backColor As Color? = Nothing)
        If _currentModel Is Nothing Then Return
        If _currentModel.Appearance Is Nothing Then _currentModel.Appearance = New ApptCardAppearance()
        _currentModel.Appearance.UniformTextColors(foreColor, backColor)
        ReapplyAppearance()
    End Sub

    ''' <summary>Same as <see cref="ApptCardCtl.ApplyContentHeightToFitForWeekView"/> — used by <see cref="ApptWeekCtl"/>.</summary>
    Friend Sub ApplyContentHeightToFitForWeekView()
        If _currentModel Is Nothing OrElse _currentModel.Appearance Is Nothing Then Return
        If _currentModel.Appointment Is Nothing OrElse Not Visible Then Return

        Dim appearance = _currentModel.Appearance
        Margin = appearance.CardMargin
        Padding = appearance.CardPadding
        pnlRoot.BackColor = appearance.CardBackColor
        BackColor = appearance.CardBackColor

        Dim cw = pnlRoot.ClientSize.Width
        If _measuredContentHeightInRoot > 0 AndAlso cw = _layoutAllForClientWidth Then
            Dim h = Padding.Top + _measuredContentHeightInRoot + Padding.Bottom
            h = Math.Max(1, h)
            If Height <> h Then Height = h
            Return
        End If

        LayoutAll()
        If _measuredContentHeightInRoot <= 0 Then Return
        Dim h2 = Padding.Top + _measuredContentHeightInRoot + Padding.Bottom
        h2 = Math.Max(1, h2)
        If Height <> h2 Then Height = h2
    End Sub

    Private Sub ApplyCardAppearance(appearance As ApptCardAppearance)
        accentLBl.BackColor = appearance.AccentColor
        pnlRoot.BackColor = appearance.CardBackColor
        BackColor = appearance.CardBackColor

        ApplyTextAppearanceW(startLbl, appearance.StartTimeStyle)
        ApplyTextAppearanceW(endLbl, appearance.EndTimeStyle)
        ApplyTextAppearanceW(patientnameLbl, appearance.PatientStyle)
        doctorLbl.Padding = New Padding(5, 2, 5, 2)
        ApplyTextAppearanceW(doctorLbl, appearance.DoctorStyle)
        ApplyTextAppearanceW(reasonLbl, appearance.ReasonStyle)
        ApplyTextAppearanceW(notesLbl, appearance.NotesStyle)
        ApplyStatusAppearanceW(statusLbl, appearance.StatusStyle)
        If Eng Then
            patientnameLbl.TextAlign = ContentAlignment.TopLeft
            doctorLbl.TextAlign = ContentAlignment.TopLeft
            reasonLbl.TextAlign = ContentAlignment.TopLeft
            notesLbl.TextAlign = ContentAlignment.TopLeft
        Else
            patientnameLbl.TextAlign = ContentAlignment.TopRight
            doctorLbl.TextAlign = ContentAlignment.TopRight
            reasonLbl.TextAlign = ContentAlignment.TopRight
            notesLbl.TextAlign = ContentAlignment.TopRight
        End If
    End Sub

    Private Shared Sub ApplyTextAppearanceW(lbl As Label, appearance As ApptTextAppearance)
        If appearance Is Nothing Then Return
        lbl.Visible = appearance.Visible AndAlso Not String.IsNullOrWhiteSpace(lbl.Text)
        If Not lbl.Visible Then Return

        lbl.Font = appearance.CreateFont()
        lbl.ForeColor = appearance.ForeColor
        If appearance.BackColor <> Color.Transparent AndAlso appearance.BackColor.A > 0 Then
            lbl.BackColor = appearance.BackColor
        Else
            lbl.BackColor = Color.Transparent
        End If
        lbl.Tag = appearance
    End Sub

    Private Shared Sub ApplyStatusAppearanceW(label As ApptCardStatusLabel, appearance As ApptTextAppearance)
        If appearance Is Nothing Then Return
        label.Visible = appearance.Visible AndAlso Not String.IsNullOrWhiteSpace(label.Text)
        If Not label.Visible Then Return
        label.Font = appearance.CreateFont()
        label.ForeColor = appearance.ForeColor
        Dim bg = appearance.BackColor
        If bg.A = 0 OrElse bg = Color.Transparent Then
            bg = Color.FromArgb(103, 114, 229)
        End If
        label.BackColor = bg
        label.BorderColor = ControlPaint.Dark(bg, 0.08F)
        label.TextDirection = If(Eng, ApptCardStatusLabel.StatusTextDirection.BottomToTop, ApptCardStatusLabel.StatusTextDirection.TopToBottom)
        label.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        label.Tag = appearance
    End Sub

    Private Sub LayoutAll()
        _measuredContentHeightInRoot = 0
        Dim appearance = If(_currentModel Is Nothing, Nothing, _currentModel.Appearance)
        If appearance Is Nothing Then Return

        Dim vStep = Math.Max(1, appearance.FieldSpacing)
        Dim contentLeft = accentLBl.Width + appearance.CardPadding.Left + 4
        Dim contentTop = appearance.CardPadding.Top
        Dim contentWidth = Math.Max(120, pnlRoot.ClientSize.Width - contentLeft - appearance.CardPadding.Right - 4)

        Dim statusWidth = 0
        If statusLbl.Visible Then
            statusWidth = MeasureStatusColumnWidth(statusLbl.Text, statusLbl.Font)
            statusLbl.Width = statusWidth
        Else
            statusLbl.Width = 0
        End If

        Dim bodyWidth = Math.Max(90, contentWidth - If(statusLbl.Visible, statusWidth + 6, 0))
        accentLBl.SendToBack()

        Dim timeBottom = LayoutTimeRowW(contentLeft, contentTop, bodyWidth)
        Dim patientTop As Integer
        If timeBottom <= contentTop AndAlso Not startLbl.Visible AndAlso Not endLbl.Visible Then
            patientTop = contentTop
        Else
            patientTop = timeBottom + vStep
        End If

        LayoutFieldW(patientnameLbl, contentLeft, patientTop, bodyWidth)
        LayoutFieldW(doctorLbl, contentLeft, patientnameLbl.Bottom + vStep, bodyWidth)
        LayoutFieldW(reasonLbl, contentLeft, doctorLbl.Bottom + vStep, bodyWidth)
        LayoutFieldW(notesLbl, contentLeft, reasonLbl.Bottom + vStep, bodyWidth)

        Dim bottomMost = appearance.CardPadding.Top
        For Each field In New Control() {startLbl, endLbl, patientnameLbl, doctorLbl, reasonLbl, notesLbl}
            If field.Visible Then
                bottomMost = Math.Max(bottomMost, field.Bottom)
            End If
        Next
        _measuredContentHeightInRoot = bottomMost + appearance.CardPadding.Bottom
        If statusLbl.Visible AndAlso _measuredContentHeightInRoot > 0 Then
            statusLbl.Height = _measuredContentHeightInRoot
        End If
        statusLbl.BringToFront()
        _layoutAllForClientWidth = pnlRoot.ClientSize.Width
    End Sub

    Private Function LayoutTimeRowW(contentLeft As Integer, top As Integer, bodyWidth As Integer) As Integer
        Const gap As Integer = TimeGap
        Dim startVis = startLbl.Visible
        Dim endVis = endLbl.Visible
        If Not startVis AndAlso Not endVis Then
            startLbl.SetBounds(contentLeft, top, 0, 0)
            endLbl.SetBounds(contentLeft, top, 0, 0)
            Return top
        End If

        Dim startW = 0
        Dim endW = 0
        If startVis Then
            startW = TextRenderer.MeasureText(If(startLbl.Text, ""), startLbl.Font).Width + startLbl.Padding.Horizontal + 4
        End If
        If endVis Then
            endW = TextRenderer.MeasureText(If(endLbl.Text, ""), endLbl.Font).Width + endLbl.Padding.Horizontal + 4
        End If
        If startVis AndAlso endVis AndAlso startW + gap + endW > bodyWidth Then
            startW = Math.Max(40, (bodyWidth - gap) \ 2)
            endW = Math.Max(40, bodyWidth - gap - startW)
        ElseIf startVis AndAlso Not endVis Then
            startW = Math.Min(Math.Max(startW, 40), bodyWidth)
        ElseIf endVis AndAlso Not startVis Then
            endW = Math.Min(Math.Max(endW, 40), bodyWidth)
        End If

        Dim startH = If(startVis, ApptTheme.MeasureTextHeight(If(startLbl.Text, ""), startLbl.Font, startW, 1), 0)
        Dim endH = If(endVis, ApptTheme.MeasureTextHeight(If(endLbl.Text, ""), endLbl.Font, endW, 1), 0)
        Dim rowH = Math.Max(startH, endH)
        If startVis Then
            startLbl.SetBounds(contentLeft, top, startW, rowH)
        Else
            startLbl.SetBounds(contentLeft, top, 0, 0)
        End If
        If endVis Then
            Dim endLeft = If(startVis, contentLeft + startW + gap, contentLeft)
            Dim endAvail = bodyWidth - If(startVis, startW + gap, 0)
            Dim useW = Math.Max(40, Math.Min(endW, endAvail))
            endLbl.SetBounds(endLeft, top, useW, rowH)
        Else
            endLbl.SetBounds(contentLeft, top, 0, 0)
        End If
        Return top + rowH
    End Function

    Private Shared Function MeasureStatusColumnWidth(statusText As String, font As Font) As Integer
        If String.IsNullOrWhiteSpace(statusText) OrElse font Is Nothing Then Return 28
        Dim m = 0
        For Each ch In statusText.Trim()
            m = Math.Max(m, TextRenderer.MeasureText(ch.ToString(), font).Width)
        Next
        Dim byHeight = CInt(Math.Ceiling(font.GetHeight())) + 8
        Return Math.Max(26, Math.Max(m, byHeight) + 10)
    End Function

    Private Shared Sub LayoutFieldW(lbl As Label, x As Integer, y As Integer, width As Integer)
        If Not lbl.Visible Then
            lbl.SetBounds(x, y, width, 0)
            Return
        End If
        Dim appearance = TryCast(lbl.Tag, ApptTextAppearance)
        Dim maxLines = If(appearance Is Nothing, 1, appearance.MaxLines)
        Dim height = ApptTheme.MeasureTextHeight(If(lbl.Text, ""), lbl.Font, width, maxLines)
        lbl.SetBounds(x, y, width, height)
    End Sub

    Private Sub ApptCard_Resize(sender As Object, e As EventArgs)
        If _currentModel Is Nothing Then Return
        LayoutAll()
        ' Without ControlStyles.ResizeRedraw, request one composite paint after layout.
        Invalidate()
    End Sub

    Private Sub WireClickRelay(ctrl As Control)
        AddHandler ctrl.Click, AddressOf RelayClick
        AddHandler ctrl.DoubleClick, AddressOf RelayDoubleClick
    End Sub

    Private Sub RelayClick(sender As Object, e As EventArgs)
        If _currentModel Is Nothing OrElse _currentModel.Appointment Is Nothing Then Return
        RaiseEvent AppointmentClicked(_currentModel.Appointment)
    End Sub

    Private Sub RelayDoubleClick(sender As Object, e As EventArgs)
        If _currentModel Is Nothing OrElse _currentModel.Appointment Is Nothing Then Return
        RaiseEvent AppointmentDoubleClicked(_currentModel.Appointment)
    End Sub

    Private Sub WireDragRelay(ctrl As Control)
        AddHandler ctrl.MouseDown,
            Sub(s, e0)
                Dim pt = PointToClient(ctrl.PointToScreen(e0.Location))
                RaiseEvent CardDragMouseDown(Me, New MouseEventArgs(e0.Button, e0.Clicks, pt.X, pt.Y, e0.Delta))
            End Sub
        AddHandler ctrl.MouseUp,
            Sub(s, e0)
                Dim pt = PointToClient(ctrl.PointToScreen(e0.Location))
                RaiseEvent CardDragMouseUp(Me, New MouseEventArgs(e0.Button, e0.Clicks, pt.X, pt.Y, e0.Delta))
            End Sub
        AddHandler ctrl.MouseMove,
            Sub(s, e0)
                Dim pt = PointToClient(ctrl.PointToScreen(e0.Location))
                RaiseEvent CardDragMouseMove(Me, New MouseEventArgs(e0.Button, e0.Clicks, pt.X, pt.Y, e0.Delta))
            End Sub
    End Sub

    Private Sub StatusLabel_MouseUp(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Right Then Return
        If _currentModel Is Nothing OrElse _currentModel.Appointment Is Nothing Then Return
        If Not statusLbl.Visible Then Return
        ShowAppointmentStatusContextMenu(e)
    End Sub

    Private Sub ShowAppointmentStatusContextMenu(e As MouseEventArgs)
        Dim ap = _currentModel.Appointment
        Dim statusColors = ApptTheme.GetStandardAppointmentStatusColors()
        Dim menuFont As New Font("Calibri", 10, FontStyle.Bold)
        Dim contextMenu As New ContextMenuStrip() With {
            .RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes),
            .Font = menuFont,
            .ShowImageMargin = False
        }
        Dim editItem = New ToolStripMenuItem(If(Eng, "Edit appointment...", "تعديل الموعد...")) With {.Font = menuFont}
        AddHandler editItem.Click, Sub() RaiseEvent StatusContextEditRequested(ap)
        contextMenu.Items.Add(editItem)
        contextMenu.Items.Add(New ToolStripSeparator() With {.Font = menuFont})
        For Each kvp In statusColors
            Dim statusKey = kvp.Key
            Dim c = kvp.Value
            Dim menuItem = New ToolStripMenuItem(ApptTheme.TranslateAppointmentStatus(statusKey)) With {.Font = menuFont}
            AddHandler menuItem.Click, Sub() RaiseEvent StatusChangeRequested(ap, statusKey, c)
            contextMenu.Items.Add(menuItem)
        Next
        contextMenu.Show(statusLbl, e.Location)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        If _currentModel Is Nothing OrElse _currentModel.Appearance Is Nothing Then Return
        Dim app = _currentModel.Appearance
        If app.ShowBorder Then
            Using p As New Pen(app.BorderColor, 1.0F)
                Dim r = ClientRectangle
                r.Width -= 1
                r.Height -= 1
                e.Graphics.DrawRectangle(p, r)
            End Using
        End If
        If _indicatorColor <> Color.Transparent AndAlso _indicatorColor.A > 0 Then
            Using p As New Pen(_indicatorColor, 3.0F)
                Dim r = ClientRectangle
                r.Inflate(-1, -1)
                e.Graphics.DrawRectangle(p, r)
            End Using
        End If
    End Sub

    Friend Function TryGetWeekHtmlExportRow() As WeekSnapshotHtmlAppt
        If _currentModel Is Nothing OrElse _currentModel.Appointment Is Nothing OrElse Not Visible Then Return Nothing
        Dim m = _currentModel
        Dim appearance = If(m.Appearance, New ApptCardAppearance())
        Dim tr As String
        If startLbl.Visible AndAlso endLbl.Visible AndAlso
            Not String.IsNullOrWhiteSpace(startLbl.Text) AndAlso Not String.IsNullOrWhiteSpace(endLbl.Text) Then
            tr = startLbl.Text & " – " & endLbl.Text
        ElseIf startLbl.Visible AndAlso Not String.IsNullOrWhiteSpace(startLbl.Text) Then
            tr = startLbl.Text
        ElseIf endLbl.Visible AndAlso Not String.IsNullOrWhiteSpace(endLbl.Text) Then
            tr = endLbl.Text
        Else
            tr = ""
        End If
        Dim stBg = appearance.StatusStyle.BackColor
        If stBg.A = 0 OrElse stBg = Color.Transparent Then
            stBg = Color.FromArgb(103, 114, 229)
        End If
        Dim bdr = appearance.BorderColor
        If bdr.A = 0 OrElse bdr = Color.Transparent Then
            bdr = Color.FromArgb(205, 210, 220)
        End If
        Return New WeekSnapshotHtmlAppt With {
            .TimeRange = tr,
            .Patient = If(patientnameLbl.Visible, patientnameLbl.Text, ""),
            .Doctor = If(doctorLbl.Visible, doctorLbl.Text, ""),
            .Reason = If(reasonLbl.Visible, reasonLbl.Text, ""),
            .Notes = If(notesLbl.Visible, notesLbl.Text, ""),
            .StatusText = If(statusLbl.Visible, statusLbl.Text, ""),
            .CardBackColor = appearance.CardBackColor,
            .CardBorderColor = bdr,
            .AccentColor = appearance.AccentColor,
            .TimeStartFore = appearance.StartTimeStyle.ForeColor,
            .TimeEndFore = appearance.EndTimeStyle.ForeColor,
            .PatientFore = appearance.PatientStyle.ForeColor,
            .DoctorFore = appearance.DoctorStyle.ForeColor,
            .ReasonFore = appearance.ReasonStyle.ForeColor,
            .NotesFore = appearance.NotesStyle.ForeColor,
            .StatusBack = stBg,
            .StatusFore = appearance.StatusStyle.ForeColor
        }
    End Function
End Class
