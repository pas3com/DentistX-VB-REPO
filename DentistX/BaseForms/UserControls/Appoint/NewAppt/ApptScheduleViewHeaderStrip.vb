Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

''' <summary>
''' Schedule strip: center caption; left cluster [Prev period][Prev appt] and right [Next appt][Next period]
''' (RTL swaps clusters). Expand/add on the right. Wired from <see cref="ApptViewRequest"/> by <see cref="ApptHostCtl"/>.
''' </summary>
Public Class ApptScheduleViewHeaderStrip
    Inherits Panel

    ''' <summary>Horizontal space between the label bounds and the inner edge of each nav button (not button width).</summary>
    Private Const NavGapPx As Integer = 650
    Private Const ActionBtnW As Integer = 50 ' 34
    Private Const ActionBtnH As Integer = 28
    ''' <summary>Default strip height — fits up to <see cref="CaptionMaxLines"/> caption lines (embedded workspace is narrower than modal scheduler).</summary>
    Private Const DefaultStripHeightPx As Integer = 54
    Private Const CaptionMaxLines As Integer = 2
    ''' <summary>Gap between period nav and appointment-edge nav (same side of caption).</summary>
    Private Const ApptNavInnerGapPx As Integer = 4

    Private Shared _cachedApptNavArrowLeft As Image

    Private ReadOnly _lblCaption As New Label With {
        .AutoEllipsis = False,
        .AutoSize = False,
        .UseMnemonic = False,
        .UseCompatibleTextRendering = False,
        .TextAlign = ContentAlignment.MiddleCenter,
        .ForeColor = Color.FromArgb(32, 42, 58),
        .BackColor = Color.Transparent
    }

    Private ReadOnly _flowRight As New FlowLayoutPanel With {
        .AutoSize = True,
        .AutoSizeMode = AutoSizeMode.GrowAndShrink,
        .WrapContents = False,
        .FlowDirection = FlowDirection.LeftToRight,
        .Padding = New Padding(0, 2, 0, 2),
        .Margin = New Padding(0),
        .BackColor = Color.Transparent
    }

    Private ReadOnly _btnPrev As New SimpleButton()
    Private ReadOnly _btnNext As New SimpleButton()
    Private ReadOnly _btnPrevAppts As New SimpleButton()
    Private ReadOnly _btnNextAppts As New SimpleButton()
    Private ReadOnly _btnExpand As New SimpleButton()
    Private ReadOnly _btnAdd As New SimpleButton()

    Private _bound As ApptViewRequest

    Public Sub New()
        Height = DefaultStripHeightPx
        MinimumSize = New Size(0, DefaultStripHeightPx)
        BackColor = Color.FromArgb(248, 250, 252)
        Padding = New Padding(8, 2, 8, 2)

        _lblCaption.Font = CreateCalibriFont(10.75F, FontStyle.Bold)

        InitActionButton(_btnPrev, If(Eng, "Prev", "السابق"), If(Eng, "Previous period", "الفترة السابقة"))
        InitActionButton(_btnNext, If(Eng, "Next", "التالي"), If(Eng, "Next period", "الفترة التالية"))
        InitActionButton(_btnExpand, "▲", If(Eng, "Expand schedule (full height)", "توسيع الجدول بكامل الارتفاع"))
        InitActionButton(_btnAdd, String.Empty, If(Eng, "Add appointment (same as toolbar)", "إضافة موعد (كزر شريط الأدوات)"))
        InitApptEdgeNavButtonsOnStrip()
        _btnAdd.ImageOptions.Image = Global.DentistX.My.Resources.Resources.add_16x16
        _btnAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        _btnAdd.Width = 34
        _btnExpand.Width = 34
        AddHandler _btnPrev.Click, AddressOf BtnPrev_Click
        AddHandler _btnNext.Click, AddressOf BtnNext_Click
        AddHandler _btnPrevAppts.Click, AddressOf BtnPrevAppts_Click
        AddHandler _btnNextAppts.Click, AddressOf BtnNextAppts_Click
        AddHandler _btnExpand.Click, AddressOf BtnExpand_Click
        AddHandler _btnAdd.Click, AddressOf BtnAdd_Click

        _btnPrev.Visible = True
        _btnNext.Visible = True
        _btnPrev.Appearance.Options.UseBackColor = True
        _btnNext.Appearance.Options.UseBackColor = True
        _btnPrev.Appearance.BackColor = Color.Red
        _btnNext.Appearance.BackColor = Color.Red
        _btnPrevAppts.Visible = True
        _btnNextAppts.Visible = True
        _flowRight.Controls.Add(_btnExpand)
        _flowRight.Controls.Add(_btnAdd)

        Controls.Add(_btnPrev)
        Controls.Add(_btnNext)
        Controls.Add(_btnPrevAppts)
        Controls.Add(_btnNextAppts)
        Controls.Add(_lblCaption)
        Controls.Add(_flowRight)

        If Not Eng Then
            _lblCaption.RightToLeft = RightToLeft.Yes
        End If

        DoubleBuffered = True
    End Sub

    ''' <summary>When false, no caption text; Prev/Next stay symmetric around the horizontal center.</summary>
    Public Property ShowCaption As Boolean = True

    ''' <summary>When true, <see cref="Apply"/> does not assign <see cref="ApptTheme.BuildRangeCaption"/>; use <see cref="SetCaptionTextOneLine"/>.</summary>
    Public Property SuppressBuiltInRangeCaption As Boolean

    Private Shared Sub InitActionButton(btn As SimpleButton, text As String, tip As String)
        btn.Text = text
        btn.ToolTip = tip
        btn.Size = New Size(ActionBtnW, ActionBtnH)
        btn.Margin = Padding.Empty
        btn.Appearance.Font = CreateCalibriFont(8.75F, FontStyle.Bold)
        btn.Appearance.Options.UseFont = True
    End Sub

    Private Sub InitApptEdgeNavButtonsOnStrip()
        Dim rightArrow = Global.DentistX.My.Resources.Resources.tbtnArrowRight16
        Dim leftArrow = GetCachedApptNavArrowLeftImage(rightArrow)
        For Each b In {_btnPrevAppts, _btnNextAppts}
            b.Text = String.Empty
            b.Size = New Size(ActionBtnW, ActionBtnH)
            b.Margin = Padding.Empty
            b.Appearance.Font = CreateCalibriFont(8.75F, FontStyle.Bold)
            b.Appearance.Options.UseFont = True
            b.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
            b.Appearance.Options.UseBackColor = True
            b.Appearance.BackColor = Color.Cyan
        Next
        _btnPrevAppts.ImageOptions.Image = If(Eng, leftArrow, rightArrow)
        _btnPrevAppts.ToolTip = If(Eng, "Previous appointment (filtered)", "الموعد السابق (بعد التصفية)")
        _btnNextAppts.ImageOptions.Image = If(Eng, rightArrow, leftArrow)
        _btnNextAppts.ToolTip = If(Eng, "Next appointment (filtered)", "الموعد التالي (بعد التصفية)")
    End Sub

    Private Shared Function GetCachedApptNavArrowLeftImage(rightArrow As Image) As Image
        If rightArrow Is Nothing Then Return Nothing
        If _cachedApptNavArrowLeft IsNot Nothing Then Return _cachedApptNavArrowLeft
        Dim mirrored = DirectCast(DirectCast(rightArrow, Bitmap).Clone(), Bitmap)
        mirrored.RotateFlip(RotateFlipType.RotateNoneFlipX)
        _cachedApptNavArrowLeft = mirrored
        Return _cachedApptNavArrowLeft
    End Function

    Protected Overrides Sub OnLayout(levent As LayoutEventArgs)
        MyBase.OnLayout(levent)
        LayoutStrip()
    End Sub

    Private Sub LayoutStrip()
        Dim cr = ClientRectangle
        Dim pad = Padding
        Dim inner As New Rectangle(cr.X + pad.Left, cr.Y + pad.Top, Math.Max(1, cr.Width - pad.Horizontal), Math.Max(1, cr.Height - pad.Vertical))

        _flowRight.PerformLayout()
        Dim rw = Math.Max(_flowRight.PreferredSize.Width, 1)
        Dim rh = Math.Min(inner.Height, Math.Max(_flowRight.PreferredSize.Height, ActionBtnH))
        Dim rx = inner.Right - rw
        Dim ry = inner.Y + (inner.Height - rh) \ 2
        _flowRight.SetBounds(rx, ry, rw, rh)

        Const chromePad As Integer = 6
        Dim midRight = rx - chromePad
        If midRight <= inner.Left Then midRight = inner.Right \ 2

        Dim midLeft = inner.Left
        Dim midW = Math.Max(1, midRight - midLeft)
        Const pw = ActionBtnW
        Const gap = NavGapPx
        Dim innerGap = ApptNavInnerGapPx
        Dim clusterW = gap + pw + innerGap + pw
        Dim btnY = inner.Y + (inner.Height - ActionBtnH) \ 2
        Dim labelW As Integer = 0
        Dim labelX As Integer
        Dim lblY As Integer = inner.Y + 2
        Dim lblH As Integer = Math.Max(1, inner.Height - 4)
        Dim wrapFlags = TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl Or
                        TextFormatFlags.HorizontalCenter Or TextFormatFlags.NoPrefix
        Dim singleLineFlags = TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix

        If ShowCaption AndAlso Not String.IsNullOrEmpty(_lblCaption.Text) Then
            ' Hard floor: leave room for two nav buttons + a small margin on each side; never reserve NavGapPx if it would starve the caption.
            Const sideMargin As Integer = 12
            Dim minButtonsW = 2 * pw + innerGap
            Dim absoluteMaxLabelW = Math.Max(40, midW - 2 * minButtonsW - 2 * sideMargin)

            ' Preferred max keeps the original NavGapPx breathing room when the strip is wide enough.
            Dim preferredMaxLabelW = midW - 2 * clusterW
            If preferredMaxLabelW < 40 Then preferredMaxLabelW = absoluteMaxLabelW

            Dim lineH = TextRenderer.MeasureText("Ay", _lblCaption.Font, Size.Empty, singleLineFlags).Height
            If lineH <= 0 Then lineH = _lblCaption.Font.Height
            Dim maxCaptionH = Math.Max(lineH, lineH * CaptionMaxLines)

            Dim singleSz = TextRenderer.MeasureText(_lblCaption.Text, _lblCaption.Font,
                New Size(Integer.MaxValue, Integer.MaxValue), singleLineFlags)

            If singleSz.Width <= preferredMaxLabelW Then
                ' Fits on one line with the original gap.
                labelW = Math.Max(1, singleSz.Width)
                lblH = lineH
            ElseIf singleSz.Width <= absoluteMaxLabelW Then
                ' Fits on one line if we sacrifice NavGapPx.
                labelW = Math.Max(1, singleSz.Width)
                lblH = lineH
            Else
                ' Needs wrap: use full available width so word boundaries can break cleanly.
                Dim wrapSz = TextRenderer.MeasureText(_lblCaption.Text, _lblCaption.Font,
                    New Size(absoluteMaxLabelW, maxCaptionH), wrapFlags)
                labelW = absoluteMaxLabelW
                lblH = Math.Min(maxCaptionH, Math.Max(lineH, wrapSz.Height))
            End If

            lblY = inner.Y + Math.Max(0, (inner.Height - lblH) \ 2)
            labelX = midLeft + (midW - labelW) \ 2
        Else
            labelX = midLeft + midW \ 2
        End If

        Dim periodPrevLeft = labelX - clusterW
        Dim apptPrevLeft = periodPrevLeft + pw + innerGap
        Dim apptNextLeft = labelX + labelW + gap
        Dim periodNextLeft = apptNextLeft + pw + innerGap

        If periodPrevLeft < midLeft Then
            Dim shift = midLeft - periodPrevLeft
            labelX += shift
            periodPrevLeft += shift
            apptPrevLeft += shift
            apptNextLeft += shift
            periodNextLeft += shift
        End If

        If periodNextLeft + pw > midRight Then
            Dim over = (periodNextLeft + pw) - midRight
            labelX -= over
            periodPrevLeft -= over
            apptPrevLeft -= over
            apptNextLeft -= over
            periodNextLeft -= over
            If periodPrevLeft < midLeft Then periodPrevLeft = midLeft
        End If

        If Not Eng Then
            Dim oldPPL = periodPrevLeft
            Dim oldPNL = periodNextLeft
            periodPrevLeft = oldPNL
            apptPrevLeft = periodPrevLeft - innerGap - pw
            periodNextLeft = oldPPL
            apptNextLeft = periodNextLeft + pw + innerGap
        End If

        _btnPrev.SetBounds(periodPrevLeft, btnY, ActionBtnW, ActionBtnH)
        _btnPrevAppts.SetBounds(apptPrevLeft, btnY, ActionBtnW, ActionBtnH)
        _lblCaption.SetBounds(labelX, lblY, Math.Max(1, labelW), lblH)
        _btnNextAppts.SetBounds(apptNextLeft, btnY, ActionBtnW, ActionBtnH)
        _btnNext.SetBounds(periodNextLeft, btnY, ActionBtnW, ActionBtnH)
        _lblCaption.Visible = ShowCaption AndAlso Not String.IsNullOrEmpty(_lblCaption.Text)
    End Sub

    Public Sub Apply(request As ApptViewRequest)
        _bound = request
        If request Is Nothing OrElse request.State Is Nothing Then
            _lblCaption.Text = ""
            _btnPrev.Visible = True
            _btnNext.Visible = True
            _btnPrevAppts.Visible = True
            _btnNextAppts.Visible = True
            SetActionsEnabled(False)
            PerformLayout()
            Return
        End If

        If ShowCaption Then
            If Not SuppressBuiltInRangeCaption Then
                _lblCaption.Text = If(request.Data Is Nothing, "",
                    ApptTheme.BuildRangeCaption(request.State, request.Data))
            End If
        Else
            _lblCaption.Text = ""
        End If

        SyncExpandGlyph(request.IsBodyWorkspaceExpanded)
        SetActionsEnabled(True)
        PerformLayout()
    End Sub

    Public Sub SyncExpandGlyph(bodyExpanded As Boolean)
        If _btnExpand Is Nothing Then Return
        If bodyExpanded Then
            _btnExpand.Text = "▼"
            _btnExpand.ToolTip = If(Eng, "Restore schedule header", "استعادة رأس الجدول والفلاتر")
        Else
            _btnExpand.Text = "▲"
            _btnExpand.ToolTip = If(Eng, "Expand schedule (full height)", "توسيع الجدول بكامل الارتفاع")
        End If
        PerformLayout()
    End Sub

    Private Sub SetActionsEnabled(enabled As Boolean)
        Dim has = enabled AndAlso _bound IsNot Nothing
        _btnPrev.Enabled = has AndAlso _bound.NavigateDatePrevious IsNot Nothing
        _btnNext.Enabled = has AndAlso _bound.NavigateDateNext IsNot Nothing
        _btnPrevAppts.Enabled = has AndAlso _bound.NavigateAppointmentPrevious IsNot Nothing
        _btnNextAppts.Enabled = has AndAlso _bound.NavigateAppointmentNext IsNot Nothing
        _btnExpand.Enabled = has AndAlso _bound.ToggleBodyWorkspaceExpand IsNot Nothing
        _btnAdd.Enabled = has AndAlso _bound.QuickAddAppointment IsNot Nothing
    End Sub

    Private Sub BtnPrev_Click(sender As Object, e As EventArgs)
        Try
            If _bound IsNot Nothing AndAlso _bound.NavigateDatePrevious IsNot Nothing Then
                _bound.NavigateDatePrevious.Invoke()
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptScheduleViewHeaderStrip.BtnPrev_Click", showUser:=False)
        End Try
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs)
        Try
            If _bound IsNot Nothing AndAlso _bound.NavigateDateNext IsNot Nothing Then
                _bound.NavigateDateNext.Invoke()
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptScheduleViewHeaderStrip.BtnNext_Click", showUser:=False)
        End Try
    End Sub

    Private Sub BtnPrevAppts_Click(sender As Object, e As EventArgs)
        Try
            If _bound IsNot Nothing AndAlso _bound.NavigateAppointmentPrevious IsNot Nothing Then
                _bound.NavigateAppointmentPrevious.Invoke()
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptScheduleViewHeaderStrip.BtnPrevAppts_Click", showUser:=False)
        End Try
    End Sub

    Private Sub BtnNextAppts_Click(sender As Object, e As EventArgs)
        Try
            If _bound IsNot Nothing AndAlso _bound.NavigateAppointmentNext IsNot Nothing Then
                _bound.NavigateAppointmentNext.Invoke()
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptScheduleViewHeaderStrip.BtnNextAppts_Click", showUser:=False)
        End Try
    End Sub

    Private Sub BtnExpand_Click(sender As Object, e As EventArgs)
        Try
            If _bound IsNot Nothing AndAlso _bound.ToggleBodyWorkspaceExpand IsNot Nothing Then
                _bound.ToggleBodyWorkspaceExpand.Invoke()
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptScheduleViewHeaderStrip.BtnExpand_Click", showUser:=False)
        End Try
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs)
        Try
            If _bound IsNot Nothing AndAlso _bound.QuickAddAppointment IsNot Nothing Then
                _bound.QuickAddAppointment.Invoke()
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptScheduleViewHeaderStrip.BtnAdd_Click", showUser:=False)
        End Try
    End Sub

    ''' <summary>Doctors day (and similar): set the center caption after <see cref="Apply"/> without using the built-in range formatter.</summary>
    Public Sub SetCaptionTextOneLine(text As String)
        SetCaptionText(text)
    End Sub

    ''' <summary>Center caption (wraps up to two lines inside the strip).</summary>
    Public Sub SetCaptionText(text As String)
        _lblCaption.Text = If(text, "")
        _lblCaption.Visible = ShowCaption AndAlso Not String.IsNullOrEmpty(_lblCaption.Text)
        Height = DefaultStripHeightPx
        PerformLayout()
    End Sub
End Class
