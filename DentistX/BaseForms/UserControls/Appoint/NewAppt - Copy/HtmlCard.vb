Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

''' <summary>
''' Optional card: <see cref="ApptCardVm"/> body as DevExpress HTML on <see cref="LabelControl"/>, status strip <see cref="ApptCardStatusLabel"/>.
''' Week/day hosts currently use <see cref="ApptCard"/> / <see cref="ApptCardCtl"/>; to try this control, replace those types with <c>HtmlCard</c>
''' in <see cref="ApptWeekCtl"/>, <see cref="ApptDayCtl"/>, and <see cref="ApptDayDoctors"/>.
''' </summary>
Public Class HtmlCard
    Inherits XtraUserControl

    Private _currentModel As ApptCardVm
    Private _use24Hour As Boolean
    Private _indicatorColor As Color = Color.Transparent

    Public Enum CardEdge
        None
        Top
        Bottom
    End Enum

    Public Sub New()
        InitializeComponent()
        Appearance.BackColor = Color.Transparent
        Appearance.Options.UseBackColor = True
        BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        DoubleBuffered = True
        ApplyRtlLayout()
        For Each c As Control In New Control() {LabelControl1, pnlCard, accentStrip, statusLbl}
            WireClickRelay(c)
            WireDragRelay(c)
        Next
        AddHandler statusLbl.MouseUp, AddressOf StatusLbl_MouseUp
    End Sub

    Public Property IndicatorColor As Color
        Get
            Return _indicatorColor
        End Get
        Set(value As Color)
            _indicatorColor = value
            ApplyIndicatorChrome()
        End Set
    End Property

    Public Function GetEdgeAt(pt As Point) As CardEdge
        Const edgeW = 8
        If pt.Y < edgeW Then Return CardEdge.Top
        If pt.Y > Height - edgeW Then Return CardEdge.Bottom
        Return CardEdge.None
    End Function

    Public ReadOnly Property BoundAppointment As AppointmentC
        Get
            Return If(_currentModel Is Nothing, Nothing, _currentModel.Appointment)
        End Get
    End Property

    Public Event AppointmentClicked As Action(Of AppointmentC)
    Public Event AppointmentDoubleClicked As Action(Of AppointmentC)
    Public Event StatusContextEditRequested As Action(Of AppointmentC)
    Public Event StatusChangeRequested As Action(Of AppointmentC, String, Color)
    Public Event CardDragMouseDown As Action(Of HtmlCard, MouseEventArgs)
    Public Event CardDragMouseUp As Action(Of HtmlCard, MouseEventArgs)
    Public Event CardDragMouseMove As Action(Of HtmlCard, MouseEventArgs)

    Public Sub Bind(model As ApptCardVm, use24Hour As Boolean)
        _currentModel = model
        _use24Hour = use24Hour
        If model Is Nothing OrElse model.Appointment Is Nothing Then
            Visible = False
            Return
        End If

        Visible = True
        Dim appearance = If(model.Appearance, New ApptCardAppearance())
        ApplyChrome(appearance)
        BindStatusStrip(model, appearance)
        LabelControl1.Text = BuildBodyHtml(model, appearance, use24Hour)
        ApplyIndicatorChrome()
        ApplyRtlLayout()
    End Sub

    Public Sub ReapplyAppearance()
        If _currentModel Is Nothing OrElse _currentModel.Appointment Is Nothing Then Return
        Dim appearance = If(_currentModel.Appearance, New ApptCardAppearance())
        ApplyChrome(appearance)
        BindStatusStrip(_currentModel, appearance)
        LabelControl1.Text = BuildBodyHtml(_currentModel, appearance, _use24Hour)
        ApplyIndicatorChrome()
    End Sub

    Public Sub ApplyUniformTextColors(foreColor As Color, Optional backColor As Color? = Nothing)
        If _currentModel Is Nothing Then Return
        If _currentModel.Appearance Is Nothing Then _currentModel.Appearance = New ApptCardAppearance()
        _currentModel.Appearance.UniformTextColors(foreColor, backColor)
        ReapplyAppearance()
    End Sub

    ''' <summary>Week column auto-height (same role as <see cref="ApptCardCtl.ApplyContentHeightToFitForWeekView"/>).</summary>
    ''' <param name="skipChromeAndStatus">After <see cref="Bind"/>, pass True to remeasure height only (no <see cref="ApplyChrome"/> / <see cref="BindStatusStrip"/>).</param>
    Friend Sub ApplyContentHeightToFitForWeekView(Optional skipChromeAndStatus As Boolean = False)
        If _currentModel Is Nothing OrElse _currentModel.Appearance Is Nothing Then Return
        If _currentModel.Appointment Is Nothing OrElse Not Visible Then Return

        Dim appearance = _currentModel.Appearance
        If Not skipChromeAndStatus Then
            Margin = appearance.CardMargin
            Padding = appearance.CardPadding
            ApplyChrome(appearance)
            BindStatusStrip(_currentModel, appearance)
        End If
        Dim plain = BuildPlainMeasureText(_currentModel, appearance, _use24Hour)
        Dim statusW = If(statusLbl.Visible, statusLbl.Width, 0)
        Dim innerW = Math.Max(40, Width - Padding.Horizontal - accentStrip.Width - statusW - LabelControl1.Padding.Horizontal - 6)
        Dim font = LabelControl1.Appearance.Font
        If font Is Nothing Then font = appearance.PatientStyle.CreateFont()
        Dim measureFlags = TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl Or TextFormatFlags.NoPadding
        Dim sz = TextRenderer.MeasureText(If(plain, " "), font, New Size(innerW, Integer.MaxValue), measureFlags)
        Dim bodyInnerH = LabelControl1.Padding.Top + LabelControl1.Padding.Bottom + sz.Height
        Dim stackMin = bodyInnerH
        If statusLbl.Visible Then
            Dim dpiY As Single = 96.0F
            Try
                dpiY = CSng(DeviceDpi)
            Catch
            End Try
            Dim mh = ApptCardStatusLabel.MeasureMinimumStripHeight(statusLbl.Text, statusLbl.Font, statusLbl.Width, dpiY)
            stackMin = Math.Max(stackMin, mh)
        End If
        Dim h = Padding.Vertical + stackMin
        h = Math.Max(1, h)
        If Height <> h Then Height = h
    End Sub

    ''' <summary>Serializes the card for week HTML export (selectable, colored); mirrors <see cref="ApptCardCtl.TryGetWeekHtmlExportRow"/>.</summary>
    Friend Function TryGetWeekHtmlExportRow() As WeekSnapshotHtmlAppt
        If _currentModel Is Nothing OrElse _currentModel.Appointment Is Nothing OrElse Not Visible Then Return Nothing
        Dim m = _currentModel
        Dim a = m.Appointment
        Dim appearance = If(m.Appearance, New ApptCardAppearance())
        Dim t1 = ApptTheme.FormatAppointmentTime(a.StartDateTime, _use24Hour)
        Dim t2 = ApptTheme.FormatAppointmentTime(a.EndDateTime, _use24Hour)
        Dim tr As String
        If TimeFieldVisible(appearance.StartTimeStyle, t1) AndAlso TimeFieldVisible(appearance.EndTimeStyle, t2) Then
            tr = t1 & " – " & t2
        ElseIf TimeFieldVisible(appearance.StartTimeStyle, t1) Then
            tr = t1
        ElseIf TimeFieldVisible(appearance.EndTimeStyle, t2) Then
            tr = t2
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
        Dim patientTxt = If(m.PatientName, "")
        Dim doctorTxt = If(m.DoctorName, "")
        Dim reasonTxt = If(a.Reason, "")
        Dim notesTxt = If(a.Notes, "")
        Dim statusTxt = ApptTheme.GetAppointmentStatusDisplayText(a)
        Return New WeekSnapshotHtmlAppt With {
            .TimeRange = tr,
            .Patient = If(appearance.PatientStyle.Visible AndAlso Not String.IsNullOrWhiteSpace(patientTxt), patientTxt, ""),
            .Doctor = If(appearance.DoctorStyle.Visible AndAlso Not String.IsNullOrWhiteSpace(doctorTxt), doctorTxt, ""),
            .Reason = If(appearance.ReasonStyle.Visible AndAlso Not String.IsNullOrWhiteSpace(reasonTxt), reasonTxt, ""),
            .Notes = If(appearance.NotesStyle.Visible AndAlso Not String.IsNullOrWhiteSpace(notesTxt), notesTxt, ""),
            .StatusText = If(appearance.StatusStyle.Visible AndAlso Not String.IsNullOrWhiteSpace(statusTxt), statusTxt, ""),
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

    Private Sub ApplyRtlLayout()
        RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        'RightToLeftLayout = Not Eng
        LabelControl1.RightToLeft = RightToLeft
    End Sub

    Private Sub ApplyChrome(appearance As ApptCardAppearance)
        Margin = appearance.CardMargin
        Padding = appearance.CardPadding
        BackColor = appearance.CardBackColor
        BorderStyle = If(appearance.ShowBorder, DevExpress.XtraEditors.Controls.BorderStyles.Simple, DevExpress.XtraEditors.Controls.BorderStyles.NoBorder)
        appearance.BorderColor = appearance.BorderColor
        'appearance.UseBorderColor = appearance.ShowBorder
        LookAndFeel.UseDefaultLookAndFeel = False
        LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat

        pnlCard.BackColor = appearance.CardBackColor
        accentStrip.BackColor = appearance.AccentColor

        LabelControl1.Appearance.BackColor = appearance.CardBackColor
        LabelControl1.Appearance.Options.UseBackColor = True
        LabelControl1.Appearance.Font = appearance.PatientStyle.CreateFont()
        LabelControl1.Appearance.Options.UseFont = True
    End Sub

    Private Sub BindStatusStrip(model As ApptCardVm, appearance As ApptCardAppearance)
        If model Is Nothing OrElse model.Appointment Is Nothing Then Return
        statusLbl.Text = ApptTheme.GetAppointmentStatusDisplayText(model.Appointment)
        ApplyStatusAppearance(statusLbl, appearance.StatusStyle)
        If statusLbl.Visible Then
            statusLbl.Width = MeasureStatusColumnWidth(statusLbl.Text, statusLbl.Font)
        Else
            statusLbl.Width = 0
        End If
    End Sub

    Private Shared Sub ApplyStatusAppearance(label As ApptCardStatusLabel, appearance As ApptTextAppearance)
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
        label.TextDirection = If(Eng, ApptCardStatusLabel.StatusTextDirection.BottomToTop, ApptCardStatusLabel.StatusTextDirection.BottomToTop)
        label.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        label.Tag = appearance
    End Sub

    Private Shared Function MeasureStatusColumnWidth(statusText As String, font As Font) As Integer
        If String.IsNullOrWhiteSpace(statusText) OrElse font Is Nothing Then Return 28
        Dim m = 0
        For Each ch In statusText.Trim()
            m = Math.Max(m, TextRenderer.MeasureText(ch.ToString(), font).Width)
        Next
        Dim byHeight = CInt(Math.Ceiling(font.GetHeight())) + 8
        Return Math.Max(26, Math.Max(m, byHeight) + 10)
    End Function

    Private Sub ApplyIndicatorChrome()
        If _indicatorColor = Color.Transparent OrElse _indicatorColor.A = 0 Then
            LabelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            LabelControl1.Appearance.Options.UseBorderColor = False
        Else
            LabelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
            LabelControl1.Appearance.BorderColor = _indicatorColor
            LabelControl1.Appearance.Options.UseBorderColor = True
        End If
    End Sub

    Private Shared Function TimeFieldVisible(ap As ApptTextAppearance, formatted As String) As Boolean
        Return ap.Visible AndAlso Not String.IsNullOrWhiteSpace(formatted)
    End Function

    Private Shared Function SolidBackForHtml(c As Color) As Color
        If c.A >= 255 Then Return Color.FromArgb(255, c.R, c.G, c.B)
        Dim a = c.A / 255.0F
        Return Color.FromArgb(255,
            CInt(Math.Min(255, Math.Max(0, c.R * a + 255 * (1 - a)))),
            CInt(Math.Min(255, Math.Max(0, c.G * a + 255 * (1 - a)))),
            CInt(Math.Min(255, Math.Max(0, c.B * a + 255 * (1 - a)))))
    End Function

    Private Shared Function WrapDxField(text As String, ap As ApptTextAppearance) As String
        If String.IsNullOrWhiteSpace(text) OrElse Not ap.Visible Then Return ""
        Dim enc = System.Net.WebUtility.HtmlEncode(text)
        Dim sb As New StringBuilder()
        If ap.FontStyle.HasFlag(FontStyle.Bold) Then sb.Append("<b>")
        Dim size = Math.Max(6.0F, ap.FontSize).ToString("0.#", CultureInfo.InvariantCulture)
        sb.Append("<size=").Append(size).Append(">")
        Dim fore = ColorTranslator.ToHtml(ap.ForeColor)
        Dim useBack = ap.BackColor.A > 8 AndAlso ap.BackColor <> Color.Transparent
        If useBack Then
            sb.Append("<backcolor=").Append(ColorTranslator.ToHtml(SolidBackForHtml(ap.BackColor))).Append(">")
        End If
        sb.Append("<color=").Append(fore).Append(">").Append(enc).Append("</color>")
        If useBack Then sb.Append("</backcolor>")
        sb.Append("</size>")
        If ap.FontStyle.HasFlag(FontStyle.Bold) Then sb.Append("</b>")
        Return sb.ToString()
    End Function

    Private Shared Function BuildBodyHtml(model As ApptCardVm, appearance As ApptCardAppearance, use24Hour As Boolean) As String
        Dim a = model.Appointment
        Dim t1 = ApptTheme.FormatAppointmentTime(a.StartDateTime, use24Hour)
        Dim t2 = ApptTheme.FormatAppointmentTime(a.EndDateTime, use24Hour)
        Dim startH = WrapDxField(t1, appearance.StartTimeStyle)
        Dim endH = WrapDxField(t2, appearance.EndTimeStyle)
        Dim timeRow As String
        If startH <> "" AndAlso endH <> "" Then
            timeRow = startH & "   " & endH
        ElseIf startH <> "" Then
            timeRow = startH
        ElseIf endH <> "" Then
            timeRow = endH
        Else
            timeRow = ""
        End If

        Dim br = If(appearance.FieldSpacing <= 1, "<br/>", String.Join("", Enumerable.Repeat("<br/>", appearance.FieldSpacing)))

        Dim parts As New List(Of String)()
        If Not String.IsNullOrEmpty(timeRow) Then parts.Add(timeRow)
        Dim p = WrapDxField(If(model.PatientName, ""), appearance.PatientStyle)
        If p <> "" Then parts.Add(p)
        Dim d = WrapDxField(If(model.DoctorName, ""), appearance.DoctorStyle)
        If d <> "" Then parts.Add(d)
        Dim r = WrapDxField(If(a.Reason, ""), appearance.ReasonStyle)
        If r <> "" Then parts.Add(r)
        Dim n = WrapDxField(If(a.Notes, ""), appearance.NotesStyle)
        If n <> "" Then parts.Add(n)

        Return String.Join(br, parts)
    End Function

    Private Shared Function BuildPlainMeasureText(model As ApptCardVm, appearance As ApptCardAppearance, use24Hour As Boolean) As String
        Dim a = model.Appointment
        Dim lines As New List(Of String)()
        Dim t1 = ApptTheme.FormatAppointmentTime(a.StartDateTime, use24Hour)
        Dim t2 = ApptTheme.FormatAppointmentTime(a.EndDateTime, use24Hour)
        Dim timeLine As String = ""
        If TimeFieldVisible(appearance.StartTimeStyle, t1) AndAlso TimeFieldVisible(appearance.EndTimeStyle, t2) Then
            timeLine = t1 & " – " & t2
        ElseIf TimeFieldVisible(appearance.StartTimeStyle, t1) Then
            timeLine = t1
        ElseIf TimeFieldVisible(appearance.EndTimeStyle, t2) Then
            timeLine = t2
        End If
        If timeLine <> "" Then lines.Add(timeLine)

        If appearance.PatientStyle.Visible AndAlso Not String.IsNullOrWhiteSpace(model.PatientName) Then lines.Add(model.PatientName)
        If appearance.DoctorStyle.Visible AndAlso Not String.IsNullOrWhiteSpace(model.DoctorName) Then lines.Add(model.DoctorName)
        If appearance.ReasonStyle.Visible AndAlso Not String.IsNullOrWhiteSpace(a.Reason) Then lines.Add(a.Reason)
        If appearance.NotesStyle.Visible AndAlso Not String.IsNullOrWhiteSpace(a.Notes) Then lines.Add(a.Notes)

        Return String.Join(Environment.NewLine, lines)
    End Function

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

    Private Sub StatusLbl_MouseUp(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Right Then Return
        If _currentModel Is Nothing OrElse _currentModel.Appointment Is Nothing Then Return
        Dim appearance = If(_currentModel.Appearance, New ApptCardAppearance())
        If Not appearance.StatusStyle.Visible Then Return
        If Not statusLbl.Visible Then Return
        If String.IsNullOrWhiteSpace(ApptTheme.GetAppointmentStatusDisplayText(_currentModel.Appointment)) Then Return
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
        AddHandler editItem.Click, Sub(s, ea) RaiseEvent StatusContextEditRequested(ap)
        contextMenu.Items.Add(editItem)
        contextMenu.Items.Add(New ToolStripSeparator() With {.Font = menuFont})

        For Each kvp In statusColors
            Dim statusKey = kvp.Key
            Dim c = kvp.Value
            Dim menuItem = New ToolStripMenuItem(ApptTheme.TranslateAppointmentStatus(statusKey)) With {.Font = menuFont}
            AddHandler menuItem.Click, Sub(s, ea) RaiseEvent StatusChangeRequested(ap, statusKey, c)
            contextMenu.Items.Add(menuItem)
        Next

        contextMenu.Show(statusLbl, e.Location)
    End Sub
End Class
