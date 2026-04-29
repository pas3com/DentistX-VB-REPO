Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class ApptCardCtl
    Inherits XtraUserControl

    Private ReadOnly _rootPanel As PanelControl
    Private ReadOnly _accentPanel As PanelControl
    Private ReadOnly _startTimeLabel As LabelControl
    Private ReadOnly _endTimeLabel As LabelControl
    Private ReadOnly _patientLabel As LabelControl
    Private ReadOnly _doctorLabel As LabelControl
    Private ReadOnly _reasonLabel As LabelControl
    Private ReadOnly _notesLabel As LabelControl
    Private ReadOnly _statusLabel As ArrowLable
    Private _currentModel As ApptCardVm
    Private _indicatorColor As Color = Color.Transparent
    ''' <summary>After <see cref="LayoutFields"/>, minimum height of the root panel to fit the left column (excludes docked status strip).</summary>
    Private _measuredContentHeightInRoot As Integer

    Public Enum CardEdge
        None
        Top
        Bottom
    End Enum

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        UpdateStyles()
        Appearance.BackColor = Color.Transparent
        Appearance.Options.UseBackColor = True
        BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        DoubleBuffered = True

        _rootPanel = New PanelControl() With {
            .Dock = DockStyle.Fill,
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        }
        _rootPanel.Appearance.BackColor = Color.Transparent
        _rootPanel.Appearance.Options.UseBackColor = True
        AddHandler _rootPanel.Paint, AddressOf RootPanel_Paint

        _accentPanel = New PanelControl() With {
            .Width = 6,
            .Dock = DockStyle.Left,
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        }

        _startTimeLabel = CreateTimeFieldLabel()
        _endTimeLabel = CreateTimeFieldLabel()
        _patientLabel = CreateFieldLabel()
        _doctorLabel = CreateFieldLabel()
        _reasonLabel = CreateFieldLabel()
        _notesLabel = CreateFieldLabel()

        _statusLabel = New ArrowLable() With {
            .Dock = DockStyle.Right,
            .UseSimpleLabel = True,
            .ShowLeftTriangle = False,
            .ShowRightTriangle = False,
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .CornerRadius = 4,
            .BorderStyle = BorderStyle.None
        }

        _rootPanel.Controls.Add(_accentPanel)
        _rootPanel.Controls.Add(_statusLabel)
        _rootPanel.Controls.Add(_startTimeLabel)
        _rootPanel.Controls.Add(_endTimeLabel)
        _rootPanel.Controls.Add(_patientLabel)
        _rootPanel.Controls.Add(_doctorLabel)
        _rootPanel.Controls.Add(_reasonLabel)
        _rootPanel.Controls.Add(_notesLabel)
        Controls.Add(_rootPanel)

        AddHandler Resize, AddressOf Card_Resize
        WireClickRelay(Me)
        WireClickRelay(_rootPanel)
        WireClickRelay(_startTimeLabel)
        WireClickRelay(_endTimeLabel)
        WireClickRelay(_patientLabel)
        WireClickRelay(_doctorLabel)
        WireClickRelay(_reasonLabel)
        WireClickRelay(_notesLabel)
        WireClickRelay(_statusLabel)
        AddHandler _statusLabel.MouseUp, AddressOf StatusLabel_MouseUp
        
        WireDragRelay(Me)
        WireDragRelay(_rootPanel)
        WireDragRelay(_accentPanel)
        WireDragRelay(_startTimeLabel)
        WireDragRelay(_endTimeLabel)
        WireDragRelay(_patientLabel)
        WireDragRelay(_doctorLabel)
        WireDragRelay(_reasonLabel)
        WireDragRelay(_notesLabel)
        WireDragRelay(_statusLabel)
    End Sub

    Public Property IndicatorColor As Color
        Get
            Return _indicatorColor
        End Get
        Set(value As Color)
            _indicatorColor = value
            _rootPanel.Invalidate()
            _rootPanel.Update() ' Force immediate paint for hold-timer indicator
        End Set
    End Property

    Public Function GetEdgeAt(pt As Point) As CardEdge
        Const edgeW = 8
        If pt.Y < edgeW Then Return CardEdge.Top
        If pt.Y > Height - edgeW Then Return CardEdge.Bottom
        Return CardEdge.None
    End Function

    Private Sub RootPanel_Paint(sender As Object, e As PaintEventArgs)
        If _indicatorColor = Color.Transparent OrElse _indicatorColor.A = 0 Then Return
        Using p As New Pen(_indicatorColor, 3.0F)
            Dim r = _rootPanel.ClientRectangle
            r.Inflate(-1, -1)
            e.Graphics.DrawRectangle(p, r)
        End Using
    End Sub

    Private Sub WireDragRelay(ctrl As Control)
        AddHandler ctrl.MouseDown,
            Sub(s, e)
                Dim pt = PointToClient(ctrl.PointToScreen(e.Location))
                RaiseEvent CardDragMouseDown(Me, New MouseEventArgs(e.Button, e.Clicks, pt.X, pt.Y, e.Delta))
            End Sub
        AddHandler ctrl.MouseUp,
            Sub(s, e)
                Dim pt = PointToClient(ctrl.PointToScreen(e.Location))
                RaiseEvent CardDragMouseUp(Me, New MouseEventArgs(e.Button, e.Clicks, pt.X, pt.Y, e.Delta))
            End Sub
        AddHandler ctrl.MouseMove,
            Sub(s, e)
                Dim pt = PointToClient(ctrl.PointToScreen(e.Location))
                RaiseEvent CardDragMouseMove(Me, New MouseEventArgs(e.Button, e.Clicks, pt.X, pt.Y, e.Delta))
            End Sub
    End Sub

    Public Event AppointmentClicked As Action(Of AppointmentC)
    Public Event AppointmentDoubleClicked As Action(Of AppointmentC)
    ''' <summary>Right-click status → Edit… (same as opening the editor from double-click).</summary>
    Public Event StatusContextEditRequested As Action(Of AppointmentC)
    ''' <summary>English status key + menu chip color.</summary>
    Public Event StatusChangeRequested As Action(Of AppointmentC, String, Color)

    ''' <summary>Unified relay for DND and Resizing (replaces WeekColumnDrag events).</summary>
    Public Event CardDragMouseDown As Action(Of ApptCardCtl, MouseEventArgs)
    Public Event CardDragMouseUp As Action(Of ApptCardCtl, MouseEventArgs)
    Public Event CardDragMouseMove As Action(Of ApptCardCtl, MouseEventArgs)

    Public ReadOnly Property BoundAppointment As AppointmentC
        Get
            Return If(_currentModel Is Nothing, Nothing, _currentModel.Appointment)
        End Get
    End Property

    ''' <summary>Sets control height to the vertical stack of body fields (times + text); status column stays docked right. Only used from <see cref="ApptWeekCtl"/>.</summary>
    Friend Sub ApplyContentHeightToFitForWeekView()
        If _currentModel Is Nothing OrElse _currentModel.Appearance Is Nothing Then Return
        LayoutFields()
        If _measuredContentHeightInRoot <= 0 Then Return
        Dim h = Padding.Top + _measuredContentHeightInRoot + Padding.Bottom
        h = Math.Max(1, h)
        If Height = h Then Return
        Height = h
    End Sub

    Public Sub Bind(model As ApptCardVm, use24Hour As Boolean)
        _currentModel = model
        If model Is Nothing OrElse model.Appointment Is Nothing Then
            Visible = False
            Return
        End If

        Visible = True
        Dim appearance = If(model.Appearance, New ApptCardAppearance())

        _startTimeLabel.Text = FormatAppointmentTime(model.Appointment.StartDateTime, use24Hour)
        _endTimeLabel.Text = FormatAppointmentTime(model.Appointment.EndDateTime, use24Hour)
        _patientLabel.Text = model.PatientName
        _doctorLabel.Text = model.DoctorName
        _reasonLabel.Text = If(model.Appointment.Reason, "")
        _notesLabel.Text = If(model.Appointment.Notes, "")
        _statusLabel.Text = GetAppointmentStatusDisplayText(model.Appointment)

        ApplyCardAppearance(appearance)
        PerformLayout()
        LayoutFields()
    End Sub

    ''' <summary>Re-applies the bound <see cref="ApptCardVm.Appearance"/> after you change colors/styles on the model (e.g. <see cref="ApptCardAppearance.UniformTextForeColor"/>). Does not alter drag/drop wiring.</summary>
    Public Sub ReapplyAppearance()
        If _currentModel Is Nothing OrElse _currentModel.Appointment Is Nothing Then Return
        Dim appearance = If(_currentModel.Appearance, New ApptCardAppearance())
        ApplyCardAppearance(appearance)
        PerformLayout()
        LayoutFields()
    End Sub

    ''' <summary>Convenience: sets uniform text colors on the bound model and refreshes the card. Drag/drop unchanged.</summary>
    Public Sub ApplyUniformTextColors(foreColor As Color, Optional backColor As Color? = Nothing)
        If _currentModel Is Nothing Then Return
        If _currentModel.Appearance Is Nothing Then _currentModel.Appearance = New ApptCardAppearance()
        _currentModel.Appearance.UniformTextColors(foreColor, backColor)
        ReapplyAppearance()
    End Sub

    Private Shared Function CreateTimeFieldLabel() As LabelControl
        Dim label = New LabelControl() With {
            .AutoSizeMode = LabelAutoSizeMode.None,
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
            .Padding = New Padding(5, 1, 5, 1)
        }
        label.Appearance.Options.UseTextOptions = True
        label.Appearance.TextOptions.WordWrap = WordWrap.NoWrap
        label.Appearance.TextOptions.VAlignment = VertAlignment.Center
        label.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        Return label
    End Function

    Private Shared Function CreateFieldLabel() As LabelControl
        Dim label = New LabelControl() With {
            .AutoSizeMode = LabelAutoSizeMode.None,
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        }
        label.Appearance.Options.UseTextOptions = True
        label.Appearance.TextOptions.WordWrap = WordWrap.Wrap
        label.Appearance.TextOptions.VAlignment = VertAlignment.Top
        label.Appearance.TextOptions.HAlignment = HorzAlignment.Near
        Return label
    End Function

    Private Sub ApplyCardAppearance(appearance As ApptCardAppearance)
        Margin = appearance.CardMargin
        Padding = appearance.CardPadding

        _rootPanel.Appearance.BackColor = appearance.CardBackColor
        _rootPanel.Appearance.Options.UseBackColor = True
        _rootPanel.BorderStyle = If(appearance.ShowBorder, DevExpress.XtraEditors.Controls.BorderStyles.Simple, DevExpress.XtraEditors.Controls.BorderStyles.NoBorder)
        _rootPanel.LookAndFeel.UseDefaultLookAndFeel = False
        _rootPanel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat

        _accentPanel.Appearance.BackColor = appearance.AccentColor
        _accentPanel.Appearance.Options.UseBackColor = True

        ApplyTextAppearance(_startTimeLabel, appearance.StartTimeStyle)
        ApplyTextAppearance(_endTimeLabel, appearance.EndTimeStyle)
        ApplyTextAppearance(_patientLabel, appearance.PatientStyle)
        ApplyTextAppearance(_doctorLabel, appearance.DoctorStyle)
        _doctorLabel.Padding = New Padding(5, 2, 5, 2)
        ApplyTextAppearance(_reasonLabel, appearance.ReasonStyle)
        ApplyTextAppearance(_notesLabel, appearance.NotesStyle)
        ApplyStatusAppearance(_statusLabel, appearance.StatusStyle)
    End Sub

    Private Shared Sub ApplyTextAppearance(label As LabelControl, appearance As ApptTextAppearance)
        label.Visible = appearance.Visible AndAlso Not String.IsNullOrWhiteSpace(label.Text)
        If Not label.Visible Then Return

        label.Appearance.Font = appearance.CreateFont()
        label.Appearance.Options.UseFont = True
        label.Appearance.ForeColor = appearance.ForeColor
        label.Appearance.Options.UseForeColor = True
        label.Appearance.BackColor = appearance.BackColor
        label.Appearance.Options.UseBackColor = appearance.BackColor <> Color.Transparent AndAlso appearance.BackColor.A > 0
        label.Tag = appearance
    End Sub

    Private Shared Sub ApplyStatusAppearance(label As ArrowLable, appearance As ApptTextAppearance)
        label.Visible = appearance.Visible AndAlso Not String.IsNullOrWhiteSpace(label.Text)
        If Not label.Visible Then Return

        label.Font = appearance.CreateFont()
        label.ForeColor = appearance.ForeColor
        Dim bg = appearance.BackColor
        If bg.A = 0 OrElse bg = Color.Transparent Then
            bg = Color.FromArgb(103, 114, 229)
        End If
        label.GlassBaseColor = bg
        label.BorderColor = ControlPaint.Dark(bg, 0.08F)
        label.TextDirection = If(Eng, ArrowLable.ArrowTextDirection.BottomToTop, ArrowLable.ArrowTextDirection.TopToBottom)
        label.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        label.Tag = appearance
    End Sub

    Private Sub LayoutFields()
        _measuredContentHeightInRoot = 0
        Dim appearance = If(_currentModel Is Nothing, Nothing, _currentModel.Appearance)
        If appearance Is Nothing Then Return

        Dim vStep = Math.Max(1, appearance.FieldSpacing)
        Dim contentLeft = _accentPanel.Width + appearance.CardPadding.Left + 4
        Dim contentTop = appearance.CardPadding.Top
        Dim contentWidth = Math.Max(120, _rootPanel.ClientSize.Width - contentLeft - appearance.CardPadding.Right - 4)

        Dim statusWidth = 0
        If _statusLabel.Visible Then
            statusWidth = MeasureStatusColumnWidth(_statusLabel.Text, _statusLabel.Font)
            _statusLabel.Width = statusWidth
        Else
            _statusLabel.Width = 0
        End If

        Dim bodyWidth = Math.Max(90, contentWidth - If(_statusLabel.Visible, statusWidth + 6, 0))

        Dim timeBottom = LayoutTimeRow(contentLeft, contentTop, bodyWidth)
        Dim patientTop As Integer
        If timeBottom <= contentTop AndAlso Not _startTimeLabel.Visible AndAlso Not _endTimeLabel.Visible Then
            patientTop = contentTop
        Else
            patientTop = timeBottom + vStep
        End If

        LayoutField(_patientLabel, contentLeft, patientTop, bodyWidth)
        LayoutField(_doctorLabel, contentLeft, _patientLabel.Bottom + vStep, bodyWidth)
        LayoutField(_reasonLabel, contentLeft, _doctorLabel.Bottom + vStep, bodyWidth)
        LayoutField(_notesLabel, contentLeft, _reasonLabel.Bottom + vStep, bodyWidth)

        Dim bottomMost = appearance.CardPadding.Top
        For Each field In New Control() {_startTimeLabel, _endTimeLabel, _patientLabel, _doctorLabel, _reasonLabel, _notesLabel}
            If field.Visible Then
                bottomMost = Math.Max(bottomMost, field.Bottom)
            End If
        Next

        _measuredContentHeightInRoot = bottomMost + appearance.CardPadding.Bottom
        ' Height is controlled by the parent timeline (e.g. ApptDayCtl) to match duration.
        ' We no longer overwrite it here based on content (ApptWeekCtl calls ApplyContentHeightToFitForWeekView).
    End Sub

    ''' <summary>Lays out start/end time on one row. Returns Y coordinate just below the row.</summary>
    Private Function LayoutTimeRow(contentLeft As Integer, top As Integer, bodyWidth As Integer) As Integer
        Dim startVis = _startTimeLabel.Visible
        Dim endVis = _endTimeLabel.Visible
        If Not startVis AndAlso Not endVis Then
            _startTimeLabel.SetBounds(contentLeft, top, 0, 0)
            _endTimeLabel.SetBounds(contentLeft, top, 0, 0)
            Return top
        End If

        Const timeGap As Integer = 4
        Dim startW = 0
        Dim endW = 0
        If startVis Then
            startW = TextRenderer.MeasureText(_startTimeLabel.Text, _startTimeLabel.Appearance.Font).Width + _startTimeLabel.Padding.Horizontal + 4
        End If
        If endVis Then
            endW = TextRenderer.MeasureText(_endTimeLabel.Text, _endTimeLabel.Appearance.Font).Width + _endTimeLabel.Padding.Horizontal + 4
        End If

        If startVis AndAlso endVis AndAlso startW + timeGap + endW > bodyWidth Then
            startW = Math.Max(40, (bodyWidth - timeGap) \ 2)
            endW = Math.Max(40, bodyWidth - timeGap - startW)
        ElseIf startVis AndAlso Not endVis Then
            startW = Math.Min(Math.Max(startW, 40), bodyWidth)
        ElseIf endVis AndAlso Not startVis Then
            endW = Math.Min(Math.Max(endW, 40), bodyWidth)
        End If

        Dim startH = If(startVis, MeasureTextHeight(_startTimeLabel.Text, _startTimeLabel.Appearance.Font, startW, 1), 0)
        Dim endH = If(endVis, MeasureTextHeight(_endTimeLabel.Text, _endTimeLabel.Appearance.Font, endW, 1), 0)
        Dim rowH = Math.Max(startH, endH)

        If startVis Then
            _startTimeLabel.SetBounds(contentLeft, top, startW, rowH)
        Else
            _startTimeLabel.SetBounds(contentLeft, top, 0, 0)
        End If

        If endVis Then
            Dim endLeft = If(startVis, contentLeft + startW + timeGap, contentLeft)
            Dim endAvail = bodyWidth - If(startVis, startW + timeGap, 0)
            Dim useW = Math.Max(40, Math.Min(endW, endAvail))
            _endTimeLabel.SetBounds(endLeft, top, useW, rowH)
        Else
            _endTimeLabel.SetBounds(contentLeft, top, 0, 0)
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

    Private Shared Sub LayoutField(label As LabelControl, x As Integer, y As Integer, width As Integer)
        If Not label.Visible Then
            label.SetBounds(x, y, width, 0)
            Return
        End If

        Dim appearance = TryCast(label.Tag, ApptTextAppearance)
        Dim maxLines = If(appearance Is Nothing, 1, appearance.MaxLines)
        Dim height = MeasureTextHeight(label.Text, label.Appearance.Font, width, maxLines)
        label.SetBounds(x, y, width, height)
    End Sub

    Private Sub Card_Resize(sender As Object, e As EventArgs)
        If _currentModel Is Nothing Then Return
        LayoutFields()
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

    Private Sub StatusLabel_MouseUp(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Right Then Return
        If _currentModel Is Nothing OrElse _currentModel.Appointment Is Nothing Then Return
        If Not _statusLabel.Visible Then Return
        ShowAppointmentStatusContextMenu(e)
    End Sub

    ''' <summary>Same structure as <see cref="SchedulerNew.ShowAppointmentStatusContextMenu"/>.</summary>
    Private Sub ShowAppointmentStatusContextMenu(e As MouseEventArgs)
        Dim ap = _currentModel.Appointment
        Dim statusColors = GetStandardAppointmentStatusColors()
        Dim menuFont As New Font("Calibri", 10, FontStyle.Bold)
        Dim contextMenu As New ContextMenuStrip() With {
            .RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes),
            .Font = menuFont,
            .ShowImageMargin = False
        }
        Dim editItem = New ToolStripMenuItem(If(Eng, "Edit appointment...", "تعديل الموعد...")) With {.Font = menuFont}
        AddHandler editItem.Click, Sub(s, ea) RaiseEvent StatusContextEditRequested(ap)
        contextMenu.Items.Add(editItem)
        Dim sep As New ToolStripSeparator() With {.Font = menuFont}
        contextMenu.Items.Add(sep)

        For Each kvp In statusColors
            Dim statusKey = kvp.Key
            Dim c = kvp.Value
            Dim menuItem = New ToolStripMenuItem(TranslateAppointmentStatus(statusKey)) With {.Font = menuFont}
            AddHandler menuItem.Click, Sub(s, ea) RaiseEvent StatusChangeRequested(ap, statusKey, c)
            contextMenu.Items.Add(menuItem)
        Next

        contextMenu.Show(_statusLabel, e.Location)
    End Sub

    ''' <summary>Serializes the card for week HTML export (selectable, colored); independent of the bitmap snapshot.</summary>
    Friend Function TryGetWeekHtmlExportRow() As WeekSnapshotHtmlAppt
        If _currentModel Is Nothing OrElse _currentModel.Appointment Is Nothing OrElse Not Visible Then Return Nothing
        Dim m = _currentModel
        Dim appearance = If(m.Appearance, New ApptCardAppearance())
        Dim tr As String
        If _startTimeLabel.Visible AndAlso _endTimeLabel.Visible AndAlso
            Not String.IsNullOrWhiteSpace(_startTimeLabel.Text) AndAlso Not String.IsNullOrWhiteSpace(_endTimeLabel.Text) Then
            tr = _startTimeLabel.Text & " – " & _endTimeLabel.Text
        ElseIf _startTimeLabel.Visible AndAlso Not String.IsNullOrWhiteSpace(_startTimeLabel.Text) Then
            tr = _startTimeLabel.Text
        ElseIf _endTimeLabel.Visible AndAlso Not String.IsNullOrWhiteSpace(_endTimeLabel.Text) Then
            tr = _endTimeLabel.Text
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
            .Patient = If(_patientLabel.Visible, _patientLabel.Text, ""),
            .Doctor = If(_doctorLabel.Visible, _doctorLabel.Text, ""),
            .Reason = If(_reasonLabel.Visible, _reasonLabel.Text, ""),
            .Notes = If(_notesLabel.Visible, _notesLabel.Text, ""),
            .StatusText = If(_statusLabel.Visible, _statusLabel.Text, ""),
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
