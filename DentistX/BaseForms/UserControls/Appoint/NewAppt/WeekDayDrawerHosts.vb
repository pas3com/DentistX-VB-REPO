#Region "Week day drawer (ApptCard list)"
Friend NotInheritable Class WeekDrawerDimPanel
    Inherits ApptDrawerDimScrimPanel

    Public Sub New(host As ApptWeekCtl)
        MyBase.New(Sub()
                       If host IsNot Nothing Then host.RequestCloseWeekDrawer()
                   End Sub)
    End Sub
End Class

Friend NotInheritable Class WeekDayCardDrawerHost
    Inherits Panel

    Private ReadOnly _weekCtl As ApptWeekCtl
    Private ReadOnly _header As Panel
    Private ReadOnly _lblTitle As Label
    Private ReadOnly _btnExpand As Label
    Private ReadOnly _btnClose As Label
    Private ReadOnly _scroll As Panel
    Private _currentDay As Date
    Private _currentAppts As List(Of AppointmentC)
    Private _currentData As ApptDataBundle
    Private _currentState As ApptState
    Private _currentRequest As ApptViewRequest

    Public Sub New(weekCtl As ApptWeekCtl)
        _weekCtl = weekCtl
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
        Width = 232
        BackColor = DrawerDayPanelWash
        BorderStyle = BorderStyle.Fixed3D
        Margin = New Padding(0)
        _header = New Panel With {.Dock = DockStyle.Top, .Height = 52, .BackColor = DrawerDayHeaderSolid}
        _lblTitle = New Label With {
            .AutoSize = False,
            .Dock = DockStyle.Fill,
            .Padding = New Padding(88, 14, 14, 8),
            .Font = CreateCalibriFont(11.0F, FontStyle.Bold),
            .ForeColor = Color.White,
            .BackColor = Color.Transparent,
            .TextAlign = ContentAlignment.TopLeft
        }
        _btnExpand = New Label With {
            .Text = "MAX",
            .Size = New Size(42, 24),
            .Font = CreateCalibriFont(8.5F, FontStyle.Bold),
            .ForeColor = Color.White,
            .BackColor = Color.FromArgb(36, 128, 145),
            .Cursor = Cursors.Hand,
            .TextAlign = ContentAlignment.MiddleCenter
        }
        _btnClose = New Label With {
            .Text = "X",
            .Size = New Size(36, 36),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left,
            .Location = New Point(6, 8),
            .Font = CreateCalibriFont(12.0F, FontStyle.Bold),
            .ForeColor = DrawerHeaderCloseIdle,
            .BackColor = Color.Transparent,
            .Cursor = Cursors.Hand,
            .TextAlign = ContentAlignment.MiddleCenter
        }

        AddHandler _btnExpand.Click,
            Sub()
                _weekCtl.RequestOpenWeekDrawerDialog(_currentDay, _currentAppts, _currentData, _currentState, _currentRequest)
            End Sub
        AddHandler _btnExpand.MouseEnter, Sub() _btnExpand.BackColor = Color.FromArgb(48, 146, 164)
        AddHandler _btnExpand.MouseLeave, Sub() _btnExpand.BackColor = Color.FromArgb(36, 128, 145)
        AddHandler _btnClose.Click, Sub() _weekCtl.RequestCloseWeekDrawer()
        AddHandler _btnClose.MouseEnter, Sub() _btnClose.ForeColor = Color.FromArgb(255, 200, 200)
        AddHandler _btnClose.MouseLeave, Sub() _btnClose.ForeColor = DrawerHeaderCloseIdle

        _header.Controls.Add(_lblTitle)
        _header.Controls.Add(_btnExpand)
        _header.Controls.Add(_btnClose)
        _btnClose.BringToFront()
        _btnExpand.BringToFront()
        AddHandler _header.SizeChanged, Sub() PositionHeaderButtons()

        _scroll = New Panel With {
            .Margin = New Padding(5),
            .BorderStyle = BorderStyle.Fixed3D,
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .BackColor = Color.FromArgb(232, 242, 252),
            .Padding = New Padding(10, 10, 6, 14)
        }

        Controls.Add(_scroll)
        Controls.Add(_header)

        AddHandler _scroll.SizeChanged, Sub() RelayoutDrawerCards()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        If Width <= 0 OrElse Height <= 0 Then Return
        Dim g = e.Graphics
        Const seamPx As Integer = 3
        Using heavy As New SolidBrush(Color.FromArgb(0, 95, 108))
            If Dock = DockStyle.Right Then
                g.FillRectangle(heavy, 0, 0, seamPx, Height)
            Else
                g.FillRectangle(heavy, Width - seamPx, 0, seamPx, Height)
            End If
        End Using
        Using frame As New Pen(Color.FromArgb(44, 72, 84), 1.0F)
            g.DrawRectangle(frame, 0, 0, Width - 1, Height - 1)
        End Using
        If _header IsNot Nothing Then
            Using rule As New Pen(DrawerDayHeaderRule, 1.0F)
                Dim ySep = Math.Min(Math.Max(_header.Bottom - 1, 0), Height - 1)
                g.DrawLine(rule, 0, ySep, Width - 1, ySep)
            End Using
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        Using br As New SolidBrush(BackColor)
            e.Graphics.FillRectangle(br, ClientRectangle)
        End Using
    End Sub

    Private Sub PositionHeaderButtons()
        If _header Is Nothing OrElse _btnClose Is Nothing OrElse _btnExpand Is Nothing Then Return
        Dim y = (_header.ClientSize.Height - _btnClose.Height) \ 2
        If Eng Then
            _btnClose.Left = 6
            _btnClose.Top = y
            _btnExpand.Left = _btnClose.Right + 6
            _btnExpand.Top = y + ((_btnClose.Height - _btnExpand.Height) \ 2)
        Else
            _btnClose.Left = _header.ClientSize.Width - _btnClose.Width - 6
            _btnClose.Top = y
            _btnExpand.Left = _btnClose.Left - _btnExpand.Width - 6
            _btnExpand.Top = y + ((_btnClose.Height - _btnExpand.Height) \ 2)
        End If
    End Sub

    Friend Sub RelayoutAfterOpen()
        PerformLayout()
        RelayoutDrawerCards()
        PositionHeaderButtons()
        Invalidate(True)
    End Sub

    Private Sub RelayoutDrawerCards()
        If _scroll Is Nothing Then Return
        Dim inner = Math.Max(80, _scroll.ClientSize.Width - _scroll.Padding.Horizontal)
        Dim w = Math.Max(80, inner - 4)
        Dim y = 0
        Const gap = 8
        For Each c As Control In _scroll.Controls.OfType(Of Control)().Where(Function(ctrl) ctrl.Visible)
            c.Width = w
            Dim weekCard = TryCast(c, ApptCard)
            If weekCard IsNot Nothing Then
                weekCard.ApplyContentHeightToFitForWeekView()
            End If
            c.Left = 2
            c.Top = y
            y += c.Height + gap
        Next
    End Sub

    Private Function GetOrCreateEmptyLabel() As Label
        Dim existing = _scroll.Controls.OfType(Of Label)().FirstOrDefault(Function(lbl) String.Equals(lbl.Name, "weekDrawerEmptyLabel", StringComparison.Ordinal))
        If existing IsNot Nothing Then Return existing
        Dim emptyLbl As New Label With {
            .Name = "weekDrawerEmptyLabel",
            .AutoSize = False,
            .Height = 56,
            .Text = If(Eng, "No appointments scheduled for this day.", "لا توجد مواعيد في هذا اليوم."),
            .Font = CreateCalibriFont(10.5F, FontStyle.Italic),
            .ForeColor = Color.FromArgb(118, 128, 145),
            .BackColor = DrawerDayPanelWash,
            .TextAlign = ContentAlignment.TopCenter,
            .Visible = False
        }
        _scroll.Controls.Add(emptyLbl)
        Return emptyLbl
    End Function

    Private Function EnsureDrawerCardPool(requiredCount As Integer, day As Date, weekHost As ApptWeekCtl) As List(Of ApptCard)
        Return AppointmentCardPool.Ensure(_scroll, requiredCount, day,
            Sub(card, d)
                If weekHost IsNot Nothing Then weekHost.WireDrawerDayCard(card, d)
            End Sub)
    End Function

    Public Sub Populate(day As Date, appts As List(Of AppointmentC), data As ApptDataBundle, state As ApptState, request As ApptViewRequest, weekHost As ApptWeekCtl)
        _currentDay = day
        _currentAppts = If(appts, New List(Of AppointmentC)())
        _currentData = data
        _currentState = state
        _currentRequest = request
        If Eng Then
            RightToLeft = RightToLeft.No
            _lblTitle.RightToLeft = RightToLeft.No
            _scroll.RightToLeft = RightToLeft.No
            _header.RightToLeft = RightToLeft.No
            _lblTitle.Padding = New Padding(88, 14, 14, 8)
            _btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Left
            _btnExpand.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Else
            RightToLeft = RightToLeft.Yes
            _lblTitle.RightToLeft = RightToLeft.Yes
            _scroll.RightToLeft = RightToLeft.Yes
            _header.RightToLeft = RightToLeft.Yes
            _lblTitle.Padding = New Padding(14, 14, 88, 8)
            _btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            _btnExpand.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        End If
        PositionHeaderButtons()
        _lblTitle.TextAlign = If(Eng, ContentAlignment.TopLeft, ContentAlignment.TopRight)
        _lblTitle.Text = FormatCaptionDayFull(day)

        Dim innerW = Math.Max(80, _scroll.ClientSize.Width - _scroll.Padding.Horizontal - 8)
        Dim emptyLbl = GetOrCreateEmptyLabel()
        emptyLbl.Width = innerW
        emptyLbl.Visible = False
        If appts.Count = 0 Then
            emptyLbl.Visible = True
            For Each card In _scroll.Controls.OfType(Of ApptCard)()
                card.Visible = False
            Next
        Else
            Dim cards = EnsureDrawerCardPool(appts.Count, day, weekHost)
            For i = 0 To appts.Count - 1
                Dim card = cards(i)
                card.Width = innerW
                card.Left = 4
                BindAppointmentCard(card, appts(i), data, state, If(request Is Nothing, Nothing, request.AppointmentAppearanceSelector), state.Use24HourFormat)
            Next
        End If
        _scroll.PerformLayout()
        RelayoutDrawerCards()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        PositionHeaderButtons()
    End Sub
End Class

Friend NotInheritable Class WeekDayCardDialogHost
    Inherits Panel

    Private ReadOnly _weekCtl As ApptWeekCtl
    Private ReadOnly _surface As Panel
    Private ReadOnly _header As Panel
    Private ReadOnly _title As Label
    Private ReadOnly _subtitle As Label
    Private ReadOnly _btnClose As Label
    Private ReadOnly _scroll As Panel

    Public Sub New(weekCtl As ApptWeekCtl)
        _weekCtl = weekCtl
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
        BackColor = Color.Transparent
        _surface = New Panel With {
            .BackColor = Color.FromArgb(248, 251, 255),
            .BorderStyle = BorderStyle.None
        }
        _header = New Panel With {
            .Dock = DockStyle.Top,
            .Height = 66,
            .BackColor = Color.FromArgb(18, 111, 126)
        }
        _title = New Label With {
            .Dock = DockStyle.Top,
            .Height = 34,
            .Padding = New Padding(18, 12, 56, 0),
            .Font = CreateCalibriFont(12.0F, FontStyle.Bold),
            .ForeColor = Color.White,
            .BackColor = Color.Transparent,
            .TextAlign = ContentAlignment.MiddleLeft
        }
        _subtitle = New Label With {
            .Dock = DockStyle.Fill,
            .Padding = New Padding(18, 0, 56, 10),
            .Font = CreateCalibriFont(9.5F, FontStyle.Regular),
            .ForeColor = Color.FromArgb(224, 239, 244),
            .BackColor = Color.Transparent,
            .TextAlign = ContentAlignment.TopLeft
        }
        _btnClose = New Label With {
            .Text = "X",
            .Size = New Size(34, 34),
            .Font = CreateCalibriFont(12.0F, FontStyle.Bold),
            .ForeColor = Color.White,
            .BackColor = Color.Transparent,
            .Cursor = Cursors.Hand,
            .TextAlign = ContentAlignment.MiddleCenter
        }
        _scroll = New Panel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .BackColor = Color.FromArgb(241, 246, 251),
            .Padding = New Padding(18, 16, 18, 18)
        }

        AddHandler _btnClose.Click, Sub() _weekCtl.RequestCloseWeekDrawerDialog()
        AddHandler _btnClose.MouseEnter, Sub() _btnClose.ForeColor = Color.FromArgb(255, 220, 220)
        AddHandler _btnClose.MouseLeave, Sub() _btnClose.ForeColor = Color.White
        AddHandler _header.SizeChanged, Sub() PositionHeaderChrome()

        _header.Controls.Add(_subtitle)
        _header.Controls.Add(_title)
        _header.Controls.Add(_btnClose)
        _btnClose.BringToFront()

        _surface.Controls.Add(_scroll)
        _surface.Controls.Add(_header)
        Controls.Add(_surface)
    End Sub

    Public Sub Populate(day As Date, appts As List(Of AppointmentC), data As ApptDataBundle, state As ApptState, request As ApptViewRequest, weekHost As ApptWeekCtl)
        _title.Text = FormatCaptionDayFull(day)
        Dim apptCount = If(appts Is Nothing, 0, appts.Count)
        _subtitle.Text = If(Eng,
            $"{apptCount} appointment(s) in focus view",
            $"عرض مكبر لعدد {apptCount} موعد")

        Dim innerW = Math.Max(220, _scroll.ClientSize.Width - _scroll.Padding.Horizontal - 6)
        Dim emptyLbl = GetOrCreateEmptyLabel()
        emptyLbl.Width = innerW
        emptyLbl.Visible = False
        If appts Is Nothing OrElse appts.Count = 0 Then
            emptyLbl.Visible = True
            For Each card In _scroll.Controls.OfType(Of ApptCard)()
                card.Visible = False
            Next
        Else
            Dim cards = EnsureDialogCardPool(appts.Count, day, weekHost)
            For i = 0 To appts.Count - 1
                Dim card = cards(i)
                card.Width = innerW
                BindAppointmentCard(card, appts(i), data, state, If(request Is Nothing, Nothing, request.AppointmentAppearanceSelector), state.Use24HourFormat)
            Next
        End If

        LayoutCards()
        CenterSurface()
        Invalidate(True)
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        CenterSurface()
        PositionHeaderChrome()
        LayoutCards()
    End Sub

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        Using br As New SolidBrush(Color.FromArgb(180, 120, 190, 245))
            e.Graphics.FillRectangle(br, ClientRectangle)
        End Using
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)
        If Not _surface.Bounds.Contains(e.Location) Then
            _weekCtl.RequestCloseWeekDrawerDialog()
        End If
    End Sub

    Private Sub CenterSurface()
        If _surface Is Nothing Then Return
        Const sideMargin As Integer = 15
        Dim w = Math.Max(0, ClientSize.Width - (sideMargin * 2))
        _surface.Bounds = New Rectangle(sideMargin, 0, w, Math.Max(0, ClientSize.Height))
    End Sub

    Private Sub PositionHeaderChrome()
        If _btnClose Is Nothing OrElse _header Is Nothing Then Return
        _btnClose.Left = _header.ClientSize.Width - _btnClose.Width - 10
        _btnClose.Top = (_header.ClientSize.Height - _btnClose.Height) \ 2
    End Sub

    Private Sub LayoutCards()
        If _scroll Is Nothing Then Return
        Dim y = 0
        Dim innerW = Math.Max(220, _scroll.ClientSize.Width - _scroll.Padding.Horizontal - 6)
        For Each c As Control In _scroll.Controls.OfType(Of Control)().Where(Function(ctrl) ctrl.Visible)
            c.Left = 0
            c.Top = y
            c.Width = innerW
            Dim weekCard = TryCast(c, ApptCard)
            If weekCard IsNot Nothing Then
                weekCard.ApplyContentHeightToFitForWeekView()
            End If
            y += c.Height + 10
        Next
    End Sub

    Private Function GetOrCreateEmptyLabel() As Label
        Dim existing = _scroll.Controls.OfType(Of Label)().FirstOrDefault(Function(lbl) String.Equals(lbl.Name, "weekDialogEmptyLabel", StringComparison.Ordinal))
        If existing IsNot Nothing Then Return existing
        Dim emptyLbl As New Label With {
            .Name = "weekDialogEmptyLabel",
            .AutoSize = False,
            .Height = 64,
            .Text = If(Eng, "No appointments scheduled for this day.", "لا توجد مواعيد في هذا اليوم."),
            .Font = CreateCalibriFont(10.5F, FontStyle.Italic),
            .ForeColor = Color.FromArgb(92, 105, 120),
            .BackColor = Color.Transparent,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Visible = False
        }
        _scroll.Controls.Add(emptyLbl)
        Return emptyLbl
    End Function

    Private Function EnsureDialogCardPool(requiredCount As Integer, day As Date, weekHost As ApptWeekCtl) As List(Of ApptCard)
        Return AppointmentCardPool.Ensure(_scroll, requiredCount, day,
            Sub(card, d)
                If weekHost IsNot Nothing Then weekHost.WireDrawerDayCard(card, d)
            End Sub)
    End Function
End Class
#End Region
