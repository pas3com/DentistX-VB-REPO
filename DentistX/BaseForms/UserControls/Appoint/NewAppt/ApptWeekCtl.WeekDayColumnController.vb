Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.Utils.Layout
Imports DevExpress.XtraEditors

Partial Public Class ApptWeekCtl

    ''' <summary>One Saturday-week day column: header, scroll host, card layer, and slim scrollbar.</summary>
    Friend NotInheritable Class WeekDayColumnController
        Private ReadOnly _owner As ApptWeekCtl

        Public ReadOnly Property Shell As PanelControl
        Public ReadOnly Property Scroll As XtraScrollableControl
        Public ReadOnly Property Cards As PanelControl
        Public ReadOnly Property Bar As DevExpress.XtraEditors.VScrollBar
        Public ReadOnly Property HeaderPanel As PanelControl
        Public ReadOnly Property TitleLabel As LabelControl
        Public ReadOnly Property DateLineLabel As LabelControl
        Public ReadOnly Property MaxLabel As Label
        Public ReadOnly Property DayDate As DateTime
        Public ReadOnly Property ColumnIndex As Integer

        Private _lastLayoutCw As Integer = -1
        Private _lastLayoutCh As Integer = -1

        Private Sub New(owner As ApptWeekCtl, dayDate As DateTime, columnIndex As Integer, state As ApptState, data As ApptDataBundle)
            _owner = owner
            Me.DayDate = dayDate
            Me.ColumnIndex = columnIndex
            Shell = BuildShell(dayDate, columnIndex, state, data)
            Scroll = ResolveScrollHost(Shell)
            Bar = ResolveSlimVScroll(Scroll)
            Cards = ResolveCardsLayer(Scroll)
            HeaderPanel = ResolveHeaderPanel(Shell)
            TitleLabel = ResolveTitleLabel(HeaderPanel)
            DateLineLabel = ResolveDateLineLabel(HeaderPanel, TitleLabel)
            MaxLabel = ResolveMaxLabel(HeaderPanel)
        End Sub

        Private Shared Function ResolveScrollHost(shell As PanelControl) As XtraScrollableControl
            Dim byName = TryCast(shell.Controls("wkColScroll"), XtraScrollableControl)
            If byName IsNot Nothing Then Return byName
            Return shell.Controls.OfType(Of XtraScrollableControl)().FirstOrDefault()
        End Function

        Private Shared Function ResolveSlimVScroll(scroll As XtraScrollableControl) As DevExpress.XtraEditors.VScrollBar
            If scroll Is Nothing Then Return Nothing
            Dim byName = TryCast(scroll.Controls("wkColVBar"), DevExpress.XtraEditors.VScrollBar)
            If byName IsNot Nothing Then Return byName
            Return scroll.Controls.OfType(Of DevExpress.XtraEditors.VScrollBar)().FirstOrDefault()
        End Function

        Private Shared Function ResolveCardsLayer(scroll As XtraScrollableControl) As PanelControl
            If scroll Is Nothing Then Return Nothing
            Dim byName = TryCast(scroll.Controls("wkColCards"), PanelControl)
            If byName IsNot Nothing Then Return byName
            Return scroll.Controls.OfType(Of PanelControl)().FirstOrDefault()
        End Function

        Private Shared Function ResolveHeaderPanel(shell As PanelControl) As PanelControl
            Dim byName = TryCast(shell.Controls("wkColHdr"), PanelControl)
            If byName IsNot Nothing Then Return byName
            Return shell.Controls.OfType(Of PanelControl)().FirstOrDefault(Function(p) p.Dock = DockStyle.Top)
        End Function

        Private Shared Function ResolveTitleLabel(header As PanelControl) As LabelControl
            If header Is Nothing Then Return Nothing
            Dim byName = TryCast(header.Controls("weekHdrTitle"), LabelControl)
            If byName IsNot Nothing Then Return byName
            Return header.Controls.OfType(Of LabelControl)().FirstOrDefault(Function(l) String.Equals(l.Name, "weekHdrTitle", StringComparison.Ordinal))
        End Function

        Private Shared Function ResolveDateLineLabel(header As PanelControl, title As LabelControl) As LabelControl
            If header Is Nothing Then Return Nothing
            Dim byName = TryCast(header.Controls("weekHdrDateLine"), LabelControl)
            If byName IsNot Nothing Then Return byName
            Dim byNm = header.Controls.OfType(Of LabelControl)().FirstOrDefault(Function(l) String.Equals(l.Name, "weekHdrDateLine", StringComparison.Ordinal))
            If byNm IsNot Nothing Then Return byNm
            Return header.Controls.OfType(Of LabelControl)().FirstOrDefault(Function(l) l IsNot title)
        End Function

        Private Shared Function ResolveMaxLabel(header As PanelControl) As Label
            If header Is Nothing Then Return Nothing
            Dim byName = TryCast(header.Controls("weekHdrMax"), Label)
            If byName IsNot Nothing Then Return byName
            Return header.Controls.OfType(Of Label)().FirstOrDefault(Function(l) String.Equals(l.Text, "MAX", StringComparison.OrdinalIgnoreCase))
        End Function

        Friend Shared Function Create(owner As ApptWeekCtl, dayDate As DateTime, columnIndex As Integer, state As ApptState, data As ApptDataBundle) As WeekDayColumnController
            Dim col As New WeekDayColumnController(owner, dayDate, columnIndex, state, data)
            If col.Scroll Is Nothing OrElse col.Bar Is Nothing OrElse col.Cards Is Nothing OrElse
                col.HeaderPanel Is Nothing OrElse col.TitleLabel Is Nothing OrElse col.DateLineLabel Is Nothing OrElse col.MaxLabel Is Nothing Then
                Throw New InvalidOperationException(
                    "ApptWeekCtl: week column shell failed to resolve scroll, header, cards, or labels (wkColScroll / wkColHdr / weekHdr*).")
            End If
            owner._dayColumns.Add(col)
            Return col
        End Function

        Friend Sub UpdateHeader(apptCount As Integer, state As ApptState)
            If HeaderPanel Is Nothing OrElse TitleLabel Is Nothing OrElse DateLineLabel Is Nothing OrElse MaxLabel Is Nothing Then Return

            Dim dayHeaderBg = If(DayDate.Date = Date.Today,
                WeekViewTodayHeaderColor,
                WeekViewDayHeaderColors(ColumnIndex Mod WeekViewDayHeaderColors.Length))
            HeaderPanel.Appearance.BackColor = dayHeaderBg
            HeaderPanel.Appearance.Options.UseBackColor = True
            HeaderPanel.Visible = True

            TitleLabel.Visible = True
            DateLineLabel.Visible = True
            TitleLabel.Text = ApptTheme.FormatSchedulerStyleDayColumnTitle(DayDate)
            Dim titleFontStyle = If(DayDate.Date = Date.Today, FontStyle.Bold Or FontStyle.Italic, FontStyle.Bold)
            TitleLabel.Appearance.Font = CreateCalibriFont(10.5F, titleFontStyle)
            TitleLabel.Appearance.Options.UseFont = True

            DateLineLabel.Text = "· " & ApptTheme.FormatSchedulerStyleDayColumnApptCountParens(apptCount)
            _owner.LayoutDayPanelHeader(HeaderPanel, TitleLabel, DateLineLabel, MaxLabel)
        End Sub

        Friend Sub BindAppointments(state As ApptState, data As ApptDataBundle)
            _owner.PopulateDayCards(Cards, DayDate, state, data)
        End Sub

        Friend Sub ResetScrollLayoutCache()
            _lastLayoutCw = -1
            _lastLayoutCh = -1
        End Sub

        Friend Sub AdjustScrollExtent()
            If Scroll Is Nothing OrElse Cards Is Nothing OrElse Bar Is Nothing Then Return

            Dim cw = Math.Max(1, Scroll.ClientSize.Width)
            Dim ch = Math.Max(1, Scroll.ClientSize.Height)
            If _lastLayoutCw = cw AndAlso _lastLayoutCh = ch AndAlso _lastLayoutCw >= 0 Then
                PositionSlimVScroll()
                Cards.Left = 0
                Cards.Top = -Bar.Value
                Return
            End If

            Dim innerFull As Integer
            Dim innerWithScroll As Integer
            If _owner._compactSixDayWeek Then
                innerFull = cw
                innerWithScroll = Math.Max(1, cw - WeekDaySlimScrollWidth)
            Else
                innerFull = Math.Max(180, cw - 16)
                innerWithScroll = Math.Max(180, cw - 16 - WeekDaySlimScrollWidth)
            End If

            Dim needV As Boolean
            Cards.SuspendLayout()
            Try
                Cards.Width = innerFull
                _owner.LayoutDayCards(Cards, remeasureUnchangedWidth:=False)
                needV = Cards.Height > ch

                If needV Then
                    Cards.Width = innerWithScroll
                    _owner.LayoutDayCards(Cards, remeasureUnchangedWidth:=False)
                    needV = Cards.Height > ch
                End If

                If Not needV AndAlso Cards.Width <> innerFull Then
                    Cards.Width = innerFull
                    _owner.LayoutDayCards(Cards, remeasureUnchangedWidth:=False)
                End If
            Finally
                Cards.ResumeLayout(True)
            End Try

            PositionSlimVScroll()
            Dim maxOffset = Math.Max(0, Cards.Height - ch)

            If needV Then
                Bar.Visible = True
                Bar.Minimum = 0
                Bar.LargeChange = Math.Max(1, ch)
                Bar.SmallChange = Math.Max(1, Math.Min(64, ch \ 8))
                Bar.Maximum = maxOffset + Bar.LargeChange - 1
                If Bar.Value > maxOffset Then Bar.Value = maxOffset
            Else
                Bar.Value = 0
                Bar.Visible = False
            End If

            Cards.Left = 0
            Cards.Top = -Bar.Value
            _lastLayoutCw = cw
            _lastLayoutCh = ch
        End Sub

        Friend Sub TryScrollToAppointment(ap As AppointmentC)
            If ap Is Nothing OrElse Cards Is Nothing OrElse Bar Is Nothing Then Return
            Dim cardCtl = Cards.Controls.OfType(Of ApptCard)().Where(Function(c) c.Visible).FirstOrDefault(
                Function(c) c.BoundAppointment IsNot Nothing AndAlso c.BoundAppointment.AppointmentID > 0 AndAlso c.BoundAppointment.AppointmentID = ap.AppointmentID)
            If cardCtl Is Nothing Then
                cardCtl = Cards.Controls.OfType(Of ApptCard)().Where(Function(c) c.Visible).FirstOrDefault(
                    Function(c) c.BoundAppointment IsNot Nothing AndAlso
                        c.BoundAppointment.StartDateTime = ap.StartDateTime AndAlso
                        c.BoundAppointment.PatientID = ap.PatientID)
            End If
            If cardCtl Is Nothing Then Return
            Dim desired = Math.Max(0, cardCtl.Top - 8)
            If Bar.Visible Then
                Dim maxOff = Math.Max(0, Bar.Maximum - Bar.LargeChange + 1)
                Bar.Value = Math.Max(Bar.Minimum, Math.Min(desired, maxOff))
            Else
                Bar.Value = 0
            End If
            Cards.Top = -Bar.Value
        End Sub

        Friend Function ScrollHostHeightLooksUndersized() As Boolean
            Return Scroll Is Nothing OrElse Scroll.ClientSize.Height < 20
        End Function

        Private Sub PositionSlimVScroll()
            If Scroll Is Nothing OrElse Bar Is Nothing Then Return
            Dim cw = Math.Max(1, Scroll.ClientSize.Width)
            Dim ch = Math.Max(1, Scroll.ClientSize.Height)
            Dim w = Math.Min(WeekDaySlimScrollWidth, cw)
            Bar.SetBounds(Math.Max(0, cw - w), 0, w, ch)
        End Sub

        Private Function BuildShell(dayDate As DateTime, dayColumnIndex As Integer, state As ApptState, data As ApptDataBundle) As PanelControl
            Dim shell = New PanelControl() With {
                .Dock = DockStyle.Fill,
                .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple,
                .Margin = New Padding(4)
            }
            shell.Appearance.BackColor = Color.Transparent
            shell.Appearance.Options.UseBackColor = True
            _owner.WireWeekDropTarget(shell, dayDate)

            Dim apptCount = If(data.Appointments Is Nothing, 0, data.Appointments.Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = dayDate.Date).Count())
            Dim dayHeaderBg = If(dayDate.Date = Date.Today,
                                  WeekViewTodayHeaderColor,
                                  WeekViewDayHeaderColors(dayColumnIndex Mod WeekViewDayHeaderColors.Length))

            Dim headerPanel = New PanelControl() With {
                .Name = "wkColHdr",
                .Dock = DockStyle.Top,
                .Height = 40,
                .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            }
            headerPanel.Appearance.BackColor = dayHeaderBg
            headerPanel.Appearance.Options.UseBackColor = True
            headerPanel.Visible = True

            Dim titleLabel = New LabelControl() With {
                .Name = "weekHdrTitle",
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Height = 18,
                .Visible = True,
                .Text = ApptTheme.FormatSchedulerStyleDayColumnTitle(dayDate)
            }
            Dim titleFontStyle = If(dayDate.Date = Date.Today, FontStyle.Bold Or FontStyle.Italic, FontStyle.Bold)
            titleLabel.Appearance.Font = CreateCalibriFont(10.5F, titleFontStyle)
            titleLabel.Appearance.Options.UseFont = True
            titleLabel.Appearance.ForeColor = Color.FromArgb(36, 64, 120)
            titleLabel.Appearance.Options.UseForeColor = True
            titleLabel.Appearance.TextOptions.HAlignment = HorzAlignment.Center
            titleLabel.Appearance.TextOptions.VAlignment = VertAlignment.Center
            titleLabel.Appearance.Options.UseTextOptions = True

            Dim dateLabel = New LabelControl() With {
                .Name = "weekHdrDateLine",
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Height = 18,
                .Visible = True,
                .Text = "· " & ApptTheme.FormatSchedulerStyleDayColumnApptCountParens(apptCount)
            }
            dateLabel.Appearance.Font = CreateCalibriFont(9.5F, FontStyle.Bold)
            dateLabel.Appearance.Options.UseFont = True
            dateLabel.Appearance.ForeColor = Color.FromArgb(75, 84, 99)
            dateLabel.Appearance.Options.UseForeColor = True
            dateLabel.Appearance.TextOptions.HAlignment = HorzAlignment.Center
            dateLabel.Appearance.TextOptions.VAlignment = VertAlignment.Center
            dateLabel.Appearance.Options.UseTextOptions = True

            Dim maxLabel = New Label With {
                .Name = "weekHdrMax",
                .Text = "MAX",
                .Size = New Size(42, 24),
                .Font = CreateCalibriFont(8.5F, FontStyle.Bold),
                .ForeColor = Color.White,
                .BackColor = Color.FromArgb(36, 128, 145),
                .Cursor = Cursors.Hand,
                .TextAlign = ContentAlignment.MiddleCenter
            }
            AddHandler maxLabel.Click, Sub() _owner.OpenWeekDayDrawerDialog(dayDate.Date)
            AddHandler maxLabel.MouseEnter, Sub() maxLabel.BackColor = Color.FromArgb(48, 146, 164)
            AddHandler maxLabel.MouseLeave, Sub() maxLabel.BackColor = Color.FromArgb(36, 128, 145)

            headerPanel.Controls.Add(dateLabel)
            headerPanel.Controls.Add(titleLabel)
            headerPanel.Controls.Add(maxLabel)
            maxLabel.BringToFront()
            _owner.WireWeekDropTarget(headerPanel, dayDate)

            headerPanel.Cursor = Cursors.Hand
            titleLabel.Cursor = Cursors.Hand
            dateLabel.Cursor = Cursors.Hand
            Dim openDrawer =
                Sub(s As Object, e As MouseEventArgs)
                    If e.Button <> MouseButtons.Left Then Return
                    If _owner._request Is Nothing OrElse _owner._request.State Is Nothing OrElse _owner._request.Data Is Nothing Then Return
                    _owner.ShowWeekDayDrawer(dayDate.Date, _owner._request.State, _owner._request.Data)
                End Sub
            AddHandler headerPanel.MouseClick, openDrawer
            AddHandler titleLabel.MouseClick, openDrawer
            AddHandler dateLabel.MouseClick, openDrawer
            AddHandler headerPanel.SizeChanged, Sub() _owner.LayoutDayPanelHeader(headerPanel, titleLabel, dateLabel, maxLabel)
            _owner.LayoutDayPanelHeader(headerPanel, titleLabel, dateLabel, maxLabel)

            Dim scrollHost = New XtraScrollableControl() With {
                .Dock = DockStyle.Fill,
                .Name = "wkColScroll",
                .AutoScroll = False
            }
            scrollHost.Appearance.BackColor = Color.White
            scrollHost.Appearance.Options.UseBackColor = True
            _owner.WireWeekDropTarget(scrollHost, dayDate)

            Dim slimV = New DevExpress.XtraEditors.VScrollBar With {
                .Name = "wkColVBar",
                .Width = WeekDaySlimScrollWidth,
                .Visible = False,
                .SmallChange = 24,
                .LargeChange = 120,
                .Minimum = 0,
                .Value = 0
            }

            Dim cardsLayer = New PanelControl() With {
                .Name = "wkColCards",
                .Dock = DockStyle.None,
                .Location = Point.Empty,
                .Height = 40,
                .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            }
            cardsLayer.Appearance.BackColor = Color.White
            cardsLayer.Appearance.Options.UseBackColor = True
            _owner.WireWeekDropTarget(cardsLayer, dayDate)
            ApptTheme.SetControlDoubleBuffered(shell)
            ApptTheme.SetControlDoubleBuffered(scrollHost)
            ApptTheme.SetControlDoubleBuffered(cardsLayer)

            scrollHost.Controls.Add(cardsLayer)
            scrollHost.Controls.Add(slimV)
            slimV.BringToFront()

            AddHandler scrollHost.DoubleClick, Sub() _owner.RaiseEmptyDate(dayDate.Date)
            AddHandler slimV.Scroll, Sub(sender, e) DayColumnClampBarAndSyncTop(slimV, cardsLayer)
            If Not WeekViewUseCoalescedScrollRefresh Then
                AddHandler scrollHost.Resize, Sub(sender, e) _owner.AdjustDayScrollExtent(scrollHost, cardsLayer, slimV)
            End If

            Dim wh = Sub(sender As Object, e As MouseEventArgs) _owner.DayColumn_DoMouseWheel(slimV, cardsLayer, e)
            AddHandler scrollHost.MouseWheel, wh
            AddHandler cardsLayer.MouseWheel, wh
            AddHandler headerPanel.MouseWheel, wh
            AddHandler titleLabel.MouseWheel, wh
            AddHandler dateLabel.MouseWheel, wh
            AddHandler maxLabel.MouseWheel, wh
            AddHandler shell.MouseWheel, wh

            ' Scroll first, header last: WinForms z-order + dock pass must keep the header above the cards
            ' (same as legacy BuildDayPanel in ApptWeekCtl - Copy). Reversed order makes the Fill scroll
            ' overlap the header band so the first cards sit under the painted day header.
            headerPanel.Dock = DockStyle.Top
            scrollHost.Dock = DockStyle.Fill
            shell.Controls.Add(scrollHost)
            shell.Controls.Add(headerPanel)

            _owner.PopulateDayCards(cardsLayer, dayDate, state, data)
            If Not WeekViewUseCoalescedScrollRefresh Then
                _owner.AdjustDayScrollExtent(scrollHost, cardsLayer, slimV)
            End If
            Return shell
        End Function
    End Class

End Class
