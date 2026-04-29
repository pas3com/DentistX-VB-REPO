Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.Utils.Layout
Imports DevExpress.XtraEditors

''' <summary>
''' Week view uses <see cref="ApptCard"/> (per-field colors/fonts via plain labels). To switch back to <see cref="HtmlCard"/>,
''' replace <c>ApptCard</c> with <c>HtmlCard</c> in this file; update <see cref="ApptDayCtl"/> and <see cref="ApptDayDoctors"/> the same way
''' (their type is <see cref="ApptCardCtl"/>).
''' </summary>
Public Class ApptWeekCtl
    Inherits XtraUserControl
    Implements IApptViewCtl

#Region "Week view render performance (toggles + legacy revert note)"
    ' Legacy revert: set the two Bools to False, re-add  AddHandler _layout.Resize / per-column  scrollHost.Resize,
    ' restore simple LayoutWeekColumns (SetBounds only) and Adjust at end of BuildDayPanel, remove coalescing types/timer/Dispose.
    ''' <summary>True: one scroll refresh for all day columns from LayoutWeekColumns (not each column’s Resize) — big win on maximize.</summary>
    Private Const WeekViewUseCoalescedScrollRefresh As Boolean = True
    ''' <summary>True: debounce relayout after host resize. False: layout on every resize message.</summary>
    Private Const WeekViewDebounceLayoutOnResize As Boolean = True
    ''' <summary>Base debounce (ms) after a small resize. Large jumps (e.g. restore/maximize) use <see cref="WeekViewResizeDebounceMaxMs"/>.</summary>
    Private Const WeekViewResizeDebounceMs As Integer = 55
    Private Const WeekViewResizeDebounceMaxMs As Integer = 95
    Private Const WeekViewResizeBigJumpTotalPx As Integer = 120
    ''' <summary>
    ''' Max <see cref="ApptCard"/> rows per day column. 0 = unlimited (slower for busy days &amp; on maximize). Default 40 keeps UI responsive; open day header for full list.
    ''' </summary>
    Private Const WeekViewMaxApptCardsPerDay As Integer = 40
    ''' <summary>False: day drawer test without dim scrim over week grid. Set True to restore normal dimmed backdrop.</summary>
    Private Const WeekDayDrawerUseDimOverlay As Boolean = True 'False
#End Region

#Region "Native (batched week scroll refresh)"
    Private NotInheritable Class NativeWm
        <DllImport("user32.dll", EntryPoint:="SendMessageW", SetLastError:=False)>
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        End Function
        Public Const WM_SETREDRAW As Integer = 11
    End Class
#End Region

    Private Structure DayColumnScrollInfo
        Public Scroll As XtraScrollableControl
        Public Cards As PanelControl
        Public Bar As DevExpress.XtraEditors.VScrollBar
        ''' <summary>Last scroll-host client width used; -1 = not yet laid out.</summary>
        Public LastLayoutCw As Integer
        ''' <summary>Last scroll-host client height used; -1 = not yet laid out.</summary>
        Public LastLayoutCh As Integer
    End Structure

    Private Structure WeekSnapshotColState
        Public ShellH As Integer
        Public BarVisible As Boolean
        Public BarValue As Integer
        Public CardsH As Integer
        Public CardsTop As Integer
    End Structure

    Private Const WeekDaySlimScrollWidth As Integer = 8
    ''' <summary>Vertical gap between stacked appointment cards inside a day column.</summary>
    Private Const DayColumnCardVerticalGap As Integer = 2
    ''' <summary>Equal left/right inset for cards in the 6-day week column (inside the cards layer).</summary>
    Private Const DayColumnCardHorizontalInsetSixDay As Integer = 2

    ''' <summary>Sat→Fri tints, same as <see cref="SchedulerNew"/> week band.</summary>
    Private Shared ReadOnly WeekViewDayHeaderColors As Color() = {
        Color.FromArgb(255, 230, 230),
        Color.FromArgb(255, 245, 220),
        Color.FromArgb(240, 255, 230),
        Color.FromArgb(230, 250, 255),
        Color.FromArgb(245, 230, 255),
        Color.FromArgb(255, 255, 230),
        Color.FromArgb(230, 255, 240)
    }

    Private Shared ReadOnly WeekViewTodayHeaderColor As Color = Color.FromArgb(180, 220, 255)

    Private ReadOnly _layout As TablePanel
    Private _request As ApptViewRequest
    Private ReadOnly _weekRoot As Panel
    Private ReadOnly _workArea As Panel
    Private ReadOnly _dimOverlay As WeekDrawerDimPanel
    Private ReadOnly _dayDrawerHost As WeekDayCardDrawerHost
    Private ReadOnly _dayHosts As New List(Of PanelControl)()
    Private ReadOnly _dayCardLayers As New List(Of PanelControl)()
    Private ReadOnly _dayColumnScrolls As New List(Of DayColumnScrollInfo)()
    Private ReadOnly _dayHeaderRefs As New List(Of DayColumnHeaderRef)()
    ''' <summary>After a full build, used with <see cref="CanSoftRefreshWeek"/> to skip tearing down columns when the same week is rebound.</summary>
    Private _weekViewCacheStart As Date = Date.MinValue
    Private _weekViewCacheVisibleDays As Integer = -1
    Private _weekViewCacheCompact As Boolean
    Private _weekViewCacheUseEng As Boolean?
    Private _compactSixDayWeek As Boolean

    Private ReadOnly _weekDragTimer As New Timer()
    Private ReadOnly _weekResizeDebounce As Timer
    Private _weekDragCard As ApptCard
    Private _weekDragSourceDay As DateTime?
    Private _weekColumnDragInProgress As Boolean
    Private _lastWeekClientW As Integer = -1
    Private _lastWeekClientH As Integer = -1

    Public Sub New()
        Appearance.BackColor = Color.Transparent
        Appearance.Options.UseBackColor = True
        DoubleBuffered = True
        AutoScroll = False

        _weekDragTimer.Enabled = False
        AddHandler _weekDragTimer.Tick, AddressOf WeekDragTimer_Tick

        _weekResizeDebounce = New Timer() With {.Enabled = False, .Interval = WeekViewResizeDebounceMs}
        AddHandler _weekResizeDebounce.Tick, AddressOf WeekResizeDebounce_Tick

        _weekRoot = New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.Transparent}
        _workArea = New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.Transparent}
        _layout = New TablePanel() With {
            .Dock = DockStyle.Fill,
            .Name = "weekTable"
        }
        _layout.Appearance.BackColor = Color.Transparent
        _layout.Appearance.Options.UseBackColor = True
        _workArea.Controls.Add(_layout)

        _dimOverlay = New WeekDrawerDimPanel(Me) With {.Dock = DockStyle.Fill, .Visible = False}
        _workArea.Controls.Add(_dimOverlay)

        _dayDrawerHost = New WeekDayCardDrawerHost(Me) With {
            .Dock = If(Eng, DockStyle.Right, DockStyle.Left),
            .Width = 0,
            .Visible = False,
            .BackColor = DrawerDayPanelWash,
            .Margin = New Padding(0)
        }
        _workArea.Controls.Add(_dayDrawerHost)
        _weekRoot.Controls.Add(_workArea)
        Controls.Add(_weekRoot)

        ApptTheme.SetControlDoubleBuffered(_weekRoot)
        ApptTheme.SetControlDoubleBuffered(_workArea)
        ApptTheme.SetControlDoubleBuffered(_layout)

        AddHandler Resize, AddressOf WeekView_Resize
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso _weekResizeDebounce IsNot Nothing Then
            RemoveHandler _weekResizeDebounce.Tick, AddressOf WeekResizeDebounce_Tick
            _weekResizeDebounce.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Public Property InteractionHub As ApptInteractionHub Implements IApptViewCtl.InteractionHub

    Public Sub BindData(request As ApptViewRequest) Implements IApptViewCtl.BindData
        CloseWeekDayDrawerImmediate()
        _request = request
        RenderWeek()
        If request IsNot Nothing Then TryScrollToAppointment(request.PendingScrollAppointment)
    End Sub

    Friend Sub RequestCloseWeekDrawer()
        CloseWeekDayDrawerImmediate()
    End Sub

    Private Sub CloseWeekDayDrawerImmediate()
        If _dayDrawerHost IsNot Nothing Then
            _dayDrawerHost.Visible = False
            _dayDrawerHost.Width = 0
        End If
        If _dimOverlay IsNot Nothing Then _dimOverlay.Visible = False
        LayoutWeekColumns()
    End Sub

    Private Const WeekDayDrawerWidthPx As Integer = 240

    Private Sub ShowWeekDayDrawer(dayDate As DateTime, state As ApptState, data As ApptDataBundle)
        If _request Is Nothing OrElse data Is Nothing OrElse _dayDrawerHost Is Nothing OrElse _dimOverlay Is Nothing Then Return
        Dim appts = ApptTheme.OrderAppointmentsForDisplay(
            If(data.Appointments, New List(Of AppointmentC)()).Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = dayDate.Date),
            data, linkedDoctorAtEnd:=True)
        _dayDrawerHost.SuspendLayout()
        Try
            _dayDrawerHost.Visible = True
            _dayDrawerHost.Width = WeekDayDrawerWidthPx
            If WeekDayDrawerUseDimOverlay Then
                _dimOverlay.Visible = True
                _dimOverlay.BringToFront()
            Else
                If _dimOverlay IsNot Nothing Then _dimOverlay.Visible = False
            End If
            _dayDrawerHost.BringToFront()
            _weekRoot.PerformLayout()
            _workArea.PerformLayout()
            _dayDrawerHost.Populate(dayDate.Date, appts, data, state, _request, Me)
            LayoutWeekColumns()
        Finally
            _dayDrawerHost.ResumeLayout(True)
        End Try
        _dayDrawerHost.Refresh()
        _dayDrawerHost.Focus()
        BeginInvoke(New MethodInvoker(Sub()
                                          If _dayDrawerHost IsNot Nothing AndAlso _dayDrawerHost.Visible Then
                                              _dayDrawerHost.RelayoutAfterOpen()
                                          End If
                                      End Sub))
    End Sub

    ''' <summary>Wires hub, status menu, column drag/drop for cards hosted in the week day drawer.</summary>
    Friend Sub WireDrawerDayCard(card As ApptCard, dayDate As DateTime)
        If card Is Nothing Then Return
        AddHandler card.AppointmentClicked, Sub(ap) If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentClicked(ap)
        AddHandler card.AppointmentDoubleClicked, Sub(ap) If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentDoubleClicked(ap)
        AddHandler card.StatusContextEditRequested, Sub(ap) If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentDoubleClicked(ap)
        AddHandler card.StatusChangeRequested,
            Sub(ap, statusKey, col)
                If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentStatusChange(ap, statusKey, col)
            End Sub
        WireWeekDropTarget(card, dayDate)
        AddHandler card.CardDragMouseDown, Sub(c, ev) OnWeekCardDragMouseDown(c, dayDate, ev)
        AddHandler card.CardDragMouseUp, Sub(c, ev) OnWeekCardDragMouseUp(c, ev)
    End Sub

    Private Sub RenderWeek()
        _weekResizeDebounce.Stop()
        If _request Is Nothing OrElse _request.State Is Nothing OrElse _request.Data Is Nothing Then
            FullClearWeekView()
            Return
        End If

        Dim state = _request.State
        _compactSixDayWeek = state.GetWeekVariant() = ApptWeekVariant.CompactSixDay
        Dim weekStart = GetWeekStartSaturday(state.CurrentDate).Date
        Dim visibleDays = If(_compactSixDayWeek, 6, 7)
        Dim compact = _compactSixDayWeek

        If CanSoftRefreshWeek(weekStart, visibleDays, compact) Then
            SoftRefreshWeek(weekStart, visibleDays, state, _request.Data)
            If _dayHosts.Count > 0 AndAlso WeekViewScrollHeightsLookUndersized() Then
                BeginInvoke(New MethodInvoker(AddressOf OnWeekViewScrollLayoutDeferred))
            End If
            Return
        End If

        Dim hWorkRedraw = If(_workArea IsNot Nothing AndAlso _workArea.IsHandleCreated, _workArea.Handle, IntPtr.Zero)
        If hWorkRedraw <> IntPtr.Zero Then
            NativeWm.SendMessage(hWorkRedraw, NativeWm.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero)
        End If
        _layout.SuspendLayout()
        Try
            _layout.Controls.Clear()
            _layout.Columns.Clear()
            _layout.Rows.Clear()
            _dayHosts.Clear()
            _dayCardLayers.Clear()
            _dayColumnScrolls.Clear()
            _dayHeaderRefs.Clear()
            InvalidateWeekViewCache()

            _layout.Rows.Add(New TablePanelRow(TablePanelEntityStyle.Relative, 100.0F))

            For i = 0 To visibleDays - 1
                _layout.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Relative, 100.0F / visibleDays))
            Next

            For i = 0 To visibleDays - 1
                Dim dayDate = weekStart.AddDays(i)
                Dim dayPanel = BuildDayPanel(dayDate, i, state, _request.Data)
                dayPanel.Dock = DockStyle.None
                _layout.Controls.Add(dayPanel)
                _layout.SetColumn(dayPanel, i)
                _layout.SetRow(dayPanel, 0)
                _dayHosts.Add(dayPanel)
            Next

            _weekViewCacheStart = weekStart
            _weekViewCacheVisibleDays = visibleDays
            _weekViewCacheCompact = compact
            _weekViewCacheUseEng = Eng

            LayoutWeekColumns(False)
        Finally
            _layout.ResumeLayout(True)
            If hWorkRedraw <> IntPtr.Zero Then
                NativeWm.SendMessage(hWorkRedraw, NativeWm.WM_SETREDRAW, New IntPtr(1), IntPtr.Zero)
                _workArea.Invalidate(True)
            End If
        End Try
        If _dayHosts.Count > 0 AndAlso WeekViewScrollHeightsLookUndersized() Then
            BeginInvoke(New MethodInvoker(AddressOf OnWeekViewScrollLayoutDeferred))
        End If
    End Sub

    Private Sub InvalidateWeekViewCache()
        _weekViewCacheStart = Date.MinValue
        _weekViewCacheVisibleDays = -1
        _weekViewCacheUseEng = Nothing
    End Sub

    Private Sub FullClearWeekView()
        _layout.SuspendLayout()
        Try
            _layout.Controls.Clear()
            _layout.Columns.Clear()
            _layout.Rows.Clear()
            _dayHosts.Clear()
            _dayCardLayers.Clear()
            _dayColumnScrolls.Clear()
            _dayHeaderRefs.Clear()
            InvalidateWeekViewCache()
        Finally
            _layout.ResumeLayout(True)
        End Try
    End Sub

    Private Function CanSoftRefreshWeek(weekStart As Date, visibleDays As Integer, compact As Boolean) As Boolean
        If _dayHosts.Count = 0 Then Return False
        If _dayHeaderRefs.Count <> visibleDays OrElse _dayCardLayers.Count <> visibleDays OrElse _dayColumnScrolls.Count <> visibleDays Then Return False
        If _dayHosts.Count <> visibleDays Then Return False
        If _weekViewCacheVisibleDays <> visibleDays Then Return False
        If _weekViewCacheCompact <> compact Then Return False
        If _weekViewCacheStart = Date.MinValue Then Return False
        If _weekViewCacheStart.Date <> weekStart.Date Then Return False
        If Not _weekViewCacheUseEng.HasValue OrElse _weekViewCacheUseEng.Value <> Eng Then Return False
        Return True
    End Function

    Private Sub SoftRefreshWeek(weekStart As Date, visibleDays As Integer, state As ApptState, data As ApptDataBundle)
        Dim hWorkRedraw = If(_workArea IsNot Nothing AndAlso _workArea.IsHandleCreated, _workArea.Handle, IntPtr.Zero)
        If hWorkRedraw <> IntPtr.Zero Then
            NativeWm.SendMessage(hWorkRedraw, NativeWm.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero)
        End If
        Try
            For i = 0 To visibleDays - 1
                Dim dayDate = weekStart.AddDays(i)
                Dim apptCount = If(data.Appointments Is Nothing, 0, data.Appointments.Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = dayDate.Date).Count())
                UpdateDayColumnHeader(i, dayDate, apptCount, state)
                PopulateDayCards(_dayCardLayers(i), dayDate, state, data)
            Next
            For c = 0 To _dayColumnScrolls.Count - 1
                Dim s = _dayColumnScrolls(c)
                s.LastLayoutCw = -1
                s.LastLayoutCh = -1
                _dayColumnScrolls(c) = s
            Next
            LayoutWeekColumns(False)
        Finally
            If hWorkRedraw <> IntPtr.Zero Then
                NativeWm.SendMessage(hWorkRedraw, NativeWm.WM_SETREDRAW, New IntPtr(1), IntPtr.Zero)
                _workArea.Invalidate(True)
            End If
        End Try
    End Sub

    Private Sub UpdateDayColumnHeader(columnIndex As Integer, dayDate As DateTime, apptCount As Integer, state As ApptState)
        If columnIndex < 0 OrElse columnIndex >= _dayHeaderRefs.Count Then Return
        Dim h = _dayHeaderRefs(columnIndex)
        If h.HeaderPanel Is Nothing OrElse h.TitleLabel Is Nothing OrElse h.DateLineLabel Is Nothing Then Return

        Dim dayHeaderBg = If(dayDate.Date = Date.Today,
            WeekViewTodayHeaderColor,
            WeekViewDayHeaderColors(columnIndex Mod WeekViewDayHeaderColors.Length))
        h.HeaderPanel.Appearance.BackColor = dayHeaderBg
        h.HeaderPanel.Appearance.Options.UseBackColor = True

        h.TitleLabel.Text = dayDate.ToString("dddd")
        Dim titleFontStyle = If(dayDate.Date = Date.Today, FontStyle.Bold Or FontStyle.Italic, FontStyle.Bold)
        h.TitleLabel.Appearance.Font = CreateCalibriFont(10.5F, titleFontStyle)
        h.TitleLabel.Appearance.Options.UseFont = True

        Dim apptSuffix = If(Eng, If(apptCount = 1, "Appt", "Appts"), If(apptCount = 1, "موعد", "مواعيد"))
        h.DateLineLabel.Text = $"{dayDate:dd MMM yyyy}.({apptCount} {apptSuffix})"
    End Sub

    Private Function WeekViewScrollHeightsLookUndersized() As Boolean
        If _dayColumnScrolls Is Nothing OrElse _dayColumnScrolls.Count = 0 Then Return True
        For i = 0 To _dayColumnScrolls.Count - 1
            Dim s = _dayColumnScrolls(i)
            If s.Scroll Is Nothing Then Return True
            If s.Scroll.ClientSize.Height < 20 Then Return True
        Next
        Return False
    End Function

    Private Sub OnWeekViewScrollLayoutDeferred()
        If IsDisposed OrElse Not IsHandleCreated OrElse _dayHosts.Count = 0 Then Return
        LayoutWeekColumns()
        If _request IsNot Nothing AndAlso _request.PendingScrollAppointment IsNot Nothing Then
            TryScrollToAppointment(_request.PendingScrollAppointment)
        End If
    End Sub

    Private Function BuildDayPanel(dayDate As DateTime, dayColumnIndex As Integer, state As ApptState, data As ApptDataBundle) As PanelControl
        Dim shell = New PanelControl() With {
            .Dock = DockStyle.Fill,
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple,
            .Margin = New Padding(4)
        }
        shell.Appearance.BackColor = Color.Transparent 'FromArgb(250, 251, 253)
        shell.Appearance.Options.UseBackColor = True
        WireWeekDropTarget(shell, dayDate)

        Dim apptCount = If(data.Appointments Is Nothing, 0, data.Appointments.Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = dayDate.Date).Count())
        Dim dayHeaderBg = If(dayDate.Date = Date.Today,
                              WeekViewTodayHeaderColor,
                              WeekViewDayHeaderColors(dayColumnIndex Mod WeekViewDayHeaderColors.Length))

        Dim headerPanel = New PanelControl() With {
            .Dock = DockStyle.Top,
            .Height = 40,
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        }
        headerPanel.Appearance.BackColor = dayHeaderBg
        headerPanel.Appearance.Options.UseBackColor = True

        Dim titleLabel = New LabelControl() With {
            .Dock = DockStyle.Top,
            .AutoSizeMode = LabelAutoSizeMode.None,
            .Height = 18,
            .Text = dayDate.ToString("dddd")
        }
        Dim titleFontStyle = If(dayDate.Date = Date.Today, FontStyle.Bold Or FontStyle.Italic, FontStyle.Bold)
        titleLabel.Appearance.Font = CreateCalibriFont(10.5F, titleFontStyle)
        titleLabel.Appearance.Options.UseFont = True
        titleLabel.Appearance.ForeColor = Color.FromArgb(36, 64, 120)
        titleLabel.Appearance.Options.UseForeColor = True
        titleLabel.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        titleLabel.Appearance.TextOptions.VAlignment = VertAlignment.Center
        titleLabel.Appearance.Options.UseTextOptions = True

        Dim apptSuffix = If(Eng, If(apptCount = 1, "Appt", "Appts"), If(apptCount = 1, "موعد", "مواعيد"))
        Dim dateLabel = New LabelControl() With {
            .Dock = DockStyle.Fill,
            .AutoSizeMode = LabelAutoSizeMode.None,
            .Text = $"{dayDate:dd MMM yyyy}.({apptCount} {apptSuffix})"
        }
        dateLabel.Appearance.Font = CreateCalibriFont(9.5F, FontStyle.Bold)
        dateLabel.Appearance.Options.UseFont = True
        dateLabel.Appearance.ForeColor = Color.FromArgb(75, 84, 99)
        dateLabel.Appearance.Options.UseForeColor = True
        dateLabel.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        dateLabel.Appearance.TextOptions.VAlignment = VertAlignment.Center
        dateLabel.Appearance.Options.UseTextOptions = True

        headerPanel.Controls.Add(dateLabel)
        headerPanel.Controls.Add(titleLabel)
        WireWeekDropTarget(headerPanel, dayDate)

        headerPanel.Cursor = Cursors.Hand
        titleLabel.Cursor = Cursors.Hand
        dateLabel.Cursor = Cursors.Hand
        Dim openDrawer =
            Sub(s As Object, e As MouseEventArgs)
                If e.Button <> MouseButtons.Left Then Return
                If _request Is Nothing OrElse _request.State Is Nothing OrElse _request.Data Is Nothing Then Return
                ShowWeekDayDrawer(dayDate.Date, _request.State, _request.Data)
            End Sub
        AddHandler headerPanel.MouseClick, openDrawer
        AddHandler titleLabel.MouseClick, openDrawer
        AddHandler dateLabel.MouseClick, openDrawer

        Dim scrollHost = New XtraScrollableControl() With {
            .Dock = DockStyle.Fill,
            .Name = $"scroll_{dayDate:yyyyMMdd}",
            .AutoScroll = False
        }
        scrollHost.Appearance.BackColor = Color.White
        scrollHost.Appearance.Options.UseBackColor = True
        WireWeekDropTarget(scrollHost, dayDate)

        Dim slimV = New DevExpress.XtraEditors.VScrollBar With {
            .Name = $"dayVScroll_{dayDate:yyyyMMdd}",
            .Width = WeekDaySlimScrollWidth,
            .Visible = False,
            .SmallChange = 24,
            .LargeChange = 120,
            .Minimum = 0,
            .Value = 0
        }

        Dim cardsLayer = New PanelControl() With {
            .Dock = DockStyle.None,
            .Location = Point.Empty,
            .Height = 40,
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        }
        cardsLayer.Appearance.BackColor = Color.White
        cardsLayer.Appearance.Options.UseBackColor = True
        WireWeekDropTarget(cardsLayer, dayDate)
        ApptTheme.SetControlDoubleBuffered(shell)
        ApptTheme.SetControlDoubleBuffered(scrollHost)
        ApptTheme.SetControlDoubleBuffered(cardsLayer)

        scrollHost.Controls.Add(cardsLayer)
        scrollHost.Controls.Add(slimV)
        slimV.BringToFront()
        _dayCardLayers.Add(cardsLayer)
        _dayColumnScrolls.Add(New DayColumnScrollInfo With {
            .Scroll = scrollHost,
            .Cards = cardsLayer,
            .Bar = slimV,
            .LastLayoutCw = -1,
            .LastLayoutCh = -1
        })

        AddHandler scrollHost.DoubleClick, Sub() RaiseEmptyDate(dayDate.Date)
        AddHandler slimV.Scroll, Sub(sender, e) DayColumnClampBarAndSyncTop(slimV, cardsLayer)
        If Not WeekViewUseCoalescedScrollRefresh Then
            AddHandler scrollHost.Resize, Sub(sender, e) AdjustDayScrollExtent(scrollHost, cardsLayer, slimV)
        End If

        Dim wh = Sub(sender As Object, e As MouseEventArgs) DayColumn_DoMouseWheel(slimV, cardsLayer, e)
        AddHandler scrollHost.MouseWheel, wh
        AddHandler cardsLayer.MouseWheel, wh
        AddHandler headerPanel.MouseWheel, wh
        AddHandler titleLabel.MouseWheel, wh
        AddHandler dateLabel.MouseWheel, wh
        AddHandler shell.MouseWheel, wh

        shell.Controls.Add(scrollHost)
        shell.Controls.Add(headerPanel)

        PopulateDayCards(cardsLayer, dayDate, state, data)
        If Not WeekViewUseCoalescedScrollRefresh Then
            AdjustDayScrollExtent(scrollHost, cardsLayer, slimV)
        End If
        _dayHeaderRefs.Add(New DayColumnHeaderRef With {
            .HeaderPanel = headerPanel,
            .TitleLabel = titleLabel,
            .DateLineLabel = dateLabel
        })
        Return shell
    End Function

    Private Sub PopulateDayCards(cardsLayer As PanelControl, dayDate As DateTime, state As ApptState, data As ApptDataBundle)
        cardsLayer.SuspendLayout()
        Try
            cardsLayer.Controls.Clear()
            cardsLayer.BackColor = Color.Transparent
            Dim dayAppointments = ApptTheme.OrderAppointmentsForDisplay(
                If(data.Appointments, New List(Of AppointmentC)()).Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = dayDate.Date),
                data, linkedDoctorAtEnd:=True)

            Dim totalDay = dayAppointments.Count
            Dim hiddenAfterCap = 0
            If WeekViewMaxApptCardsPerDay > 0 AndAlso totalDay > WeekViewMaxApptCardsPerDay Then
                hiddenAfterCap = totalDay - WeekViewMaxApptCardsPerDay
                dayAppointments = dayAppointments.Take(WeekViewMaxApptCardsPerDay).ToList()
            End If

            If dayAppointments.Count = 0 Then
                Dim emptyLabel = New LabelControl() With {
                    .Dock = DockStyle.Top,
                    .AutoSizeMode = LabelAutoSizeMode.None,
                    .Height = 48,
                    .Text = "No appointments"
                }
                emptyLabel.Appearance.Font = CreateCalibriFont(10.0F, FontStyle.Italic)
                emptyLabel.Appearance.Options.UseFont = True
                emptyLabel.Appearance.ForeColor = Color.Gray
                emptyLabel.Appearance.Options.UseForeColor = True
                emptyLabel.Appearance.TextOptions.HAlignment = HorzAlignment.Center
                emptyLabel.Appearance.TextOptions.VAlignment = VertAlignment.Center
                emptyLabel.Appearance.Options.UseTextOptions = True
                AddHandler emptyLabel.DoubleClick, Sub() RaiseEmptyDate(dayDate.Date)
                cardsLayer.Controls.Add(emptyLabel)
                cardsLayer.Height = emptyLabel.Height + 12
                Return
            End If

            For Each appointment In dayAppointments
                Dim model = New ApptCardVm With {
                    .Appointment = appointment,
                    .PatientName = data.ResolvePatientName(appointment.PatientID),
                    .DoctorInfo = data.ResolveDoctor(appointment.DrID)
                }
                model.Appearance = BuildDefaultCardAppearance(model, state)
                If _request.AppointmentAppearanceSelector IsNot Nothing Then
                    Dim selectedAppearance = _request.AppointmentAppearanceSelector(model)
                    If selectedAppearance IsNot Nothing Then
                        model.Appearance = selectedAppearance
                    End If
                End If

                Dim card = New ApptCard() With {
                    .Dock = DockStyle.None,
                    .Width = If(_compactSixDayWeek,
                        Math.Max(1, cardsLayer.Width - 2 * DayColumnCardHorizontalInsetSixDay),
                        Math.Max(190, cardsLayer.Width - 4))
                }
                card.Bind(model, state.Use24HourFormat)
                AddHandler card.AppointmentClicked, Sub(ap) If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentClicked(ap)
                AddHandler card.AppointmentDoubleClicked, Sub(ap) If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentDoubleClicked(ap)
                AddHandler card.StatusContextEditRequested, Sub(ap) If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentDoubleClicked(ap)
                AddHandler card.StatusChangeRequested,
                    Sub(ap, statusKey, col)
                        If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentStatusChange(ap, statusKey, col)
                    End Sub
                WireWeekDropTarget(card, dayDate)
                AddHandler card.CardDragMouseDown, Sub(c, ev) OnWeekCardDragMouseDown(c, dayDate, ev)
                AddHandler card.CardDragMouseUp, Sub(c, ev) OnWeekCardDragMouseUp(c, ev)
                cardsLayer.Controls.Add(card)
            Next

            If hiddenAfterCap > 0 Then
                Dim moreTxt = If(Eng,
                    $"+{hiddenAfterCap} more — click to show all",
                    $"و{hiddenAfterCap} إضافية — انقر لعرض الكل")
                Dim moreLabel = New LabelControl() With {
                    .Dock = DockStyle.None,
                    .AutoSizeMode = LabelAutoSizeMode.None,
                    .Height = 34,
                    .Text = moreTxt,
                    .Cursor = Cursors.Hand
                }
                moreLabel.Appearance.Font = CreateCalibriFont(9.0F, FontStyle.Italic)
                moreLabel.Appearance.Options.UseFont = True
                moreLabel.Appearance.ForeColor = Color.FromArgb(37, 99, 235)
                moreLabel.Appearance.Options.UseForeColor = True
                moreLabel.Appearance.TextOptions.HAlignment = HorzAlignment.Center
                moreLabel.Appearance.TextOptions.VAlignment = VertAlignment.Center
                moreLabel.Appearance.Options.UseTextOptions = True
                AddHandler moreLabel.MouseClick,
                    Sub(s, e)
                        If e.Button = MouseButtons.Left Then ShowWeekDayDrawer(dayDate.Date, state, data)
                    End Sub
                ' Add first, then move to index 0 so the layout Reverse() pass ends with "more" at the bottom of the column.
                cardsLayer.Controls.Add(moreLabel)
                cardsLayer.Controls.SetChildIndex(moreLabel, 0)
            End If

            LayoutDayCards(cardsLayer)
        Finally
            cardsLayer.ResumeLayout(True)
        End Try
    End Sub

    ''' <param name="remeasureUnchangedWidth">When false, <see cref="ApptCard.ApplyContentHeightToFitForWeekView"/> runs only if the card width actually changed (resize / scrollbar toggle). Use true after data bind or snapshot.</param>
    Private Sub LayoutDayCards(cardsLayer As PanelControl, Optional remeasureUnchangedWidth As Boolean = True)
        Dim top = 8
        Dim hInset As Integer
        Dim width As Integer
        If _compactSixDayWeek Then
            hInset = DayColumnCardHorizontalInsetSixDay
            width = Math.Max(1, cardsLayer.Width - 2 * hInset)
        Else
            hInset = 2
            width = Math.Max(180, cardsLayer.Width - 4)
        End If

        For Each ctrl As Control In cardsLayer.Controls.OfType(Of Control)().Reverse()
            Dim wChanged = (ctrl.Width <> width)
            ctrl.Width = width
            Dim weekCard = TryCast(ctrl, ApptCard)
            If weekCard IsNot Nothing AndAlso (wChanged OrElse remeasureUnchangedWidth) Then
                weekCard.ApplyContentHeightToFitForWeekView()
            End If
            ctrl.Left = hInset
            ctrl.Top = top
            top += ctrl.Height + DayColumnCardVerticalGap
        Next

        cardsLayer.Height = Math.Max(40, top)
    End Sub

    Private Sub PositionDaySlimVScroll(scrollHost As XtraScrollableControl, bar As DevExpress.XtraEditors.VScrollBar)
        If scrollHost Is Nothing OrElse bar Is Nothing Then Return
        Dim cw = Math.Max(1, scrollHost.ClientSize.Width)
        Dim ch = Math.Max(1, scrollHost.ClientSize.Height)
        Dim w = Math.Min(WeekDaySlimScrollWidth, cw)
        bar.SetBounds(Math.Max(0, cw - w), 0, w, ch)
    End Sub

    ''' <summary>
    ''' Largest <see cref="VScrollBar.Value"/> that keeps the bottom of <paramref name="cardsLayer"/> at the viewport bottom (no empty tail).
    ''' Matches <c>Maximum - LargeChange + 1</c> when <see cref="AdjustDayScrollExtentAt"/> sets <c>Maximum = maxOffset + LargeChange - 1</c>.
    ''' </summary>
    Private Shared Function DayColumnMaxScrollValue(cardsLayer As PanelControl, bar As DevExpress.XtraEditors.VScrollBar) As Integer
        If cardsLayer Is Nothing OrElse bar Is Nothing Then Return 0
        Dim host = TryCast(cardsLayer.Parent, XtraScrollableControl)
        If host Is Nothing Then Return 0
        Dim ch = Math.Max(1, host.ClientSize.Height)
        Return Math.Max(0, cardsLayer.Height - ch)
    End Function

    Private Shared Sub DayColumnClampBarAndSyncTop(bar As DevExpress.XtraEditors.VScrollBar, cardsLayer As PanelControl)
        If bar Is Nothing OrElse cardsLayer Is Nothing Then Return
        Dim maxV = DayColumnMaxScrollValue(cardsLayer, bar)
        If bar.Value > maxV Then bar.Value = maxV
        If bar.Value < bar.Minimum Then bar.Value = bar.Minimum
        cardsLayer.Top = -bar.Value
    End Sub

    ''' <summary>Custom slim vertical scrollbar for day columns (same idea as SchedulerNew doctor strip horizontal bar).</summary>
    Private Sub DayColumn_DoMouseWheel(bar As DevExpress.XtraEditors.VScrollBar, cardsLayer As PanelControl, e As MouseEventArgs)
        If e Is Nothing OrElse e.Delta = 0 OrElse bar Is Nothing OrElse cardsLayer Is Nothing Then Return
        If Not bar.Visible Then Return
        Dim lines = Math.Max(1, SystemInformation.MouseWheelScrollLines)
        Dim stepPx = Math.Max(bar.SmallChange, bar.SmallChange * lines \ 2)
        Dim delta = If(e.Delta > 0, -stepPx, stepPx)
        Dim nv = bar.Value + delta
        Dim maxV = DayColumnMaxScrollValue(cardsLayer, bar)
        nv = Math.Max(bar.Minimum, Math.Min(maxV, nv))
        bar.Value = nv
        cardsLayer.Top = -bar.Value
    End Sub

    Private Function FindDayScrollColumnIndex(scrollHost As XtraScrollableControl, cardsLayer As PanelControl, bar As DevExpress.XtraEditors.VScrollBar) As Integer
        If _dayColumnScrolls Is Nothing Then Return -1
        For i = 0 To _dayColumnScrolls.Count - 1
            Dim s = _dayColumnScrolls(i)
            If s.Scroll Is scrollHost AndAlso s.Cards Is cardsLayer AndAlso s.Bar Is bar Then Return i
        Next
        Return -1
    End Function

    Private Sub AdjustDayScrollExtent(scrollHost As XtraScrollableControl, cardsLayer As PanelControl, bar As DevExpress.XtraEditors.VScrollBar)
        Dim ix = FindDayScrollColumnIndex(scrollHost, cardsLayer, bar)
        If ix >= 0 Then
            AdjustDayScrollExtentAt(ix)
        End If
    End Sub

    Private Sub AdjustDayScrollExtentAt(columnIndex As Integer)
        If columnIndex < 0 OrElse columnIndex >= _dayColumnScrolls.Count Then Return
        Dim s = _dayColumnScrolls(columnIndex)
        Dim scrollHost = s.Scroll
        Dim cardsLayer = s.Cards
        Dim bar = s.Bar
        If scrollHost Is Nothing OrElse cardsLayer Is Nothing OrElse bar Is Nothing Then Return

        Dim cw = Math.Max(1, scrollHost.ClientSize.Width)
        Dim ch = Math.Max(1, scrollHost.ClientSize.Height)
        If s.LastLayoutCw = cw AndAlso s.LastLayoutCh = ch AndAlso s.LastLayoutCw >= 0 Then
            PositionDaySlimVScroll(scrollHost, bar)
            cardsLayer.Left = 0
            cardsLayer.Top = -bar.Value
            Return
        End If

        Dim innerFull As Integer
        Dim innerWithScroll As Integer
        If _compactSixDayWeek Then
            innerFull = cw
            innerWithScroll = Math.Max(1, cw - WeekDaySlimScrollWidth)
        Else
            innerFull = Math.Max(180, cw - 16)
            innerWithScroll = Math.Max(180, cw - 16 - WeekDaySlimScrollWidth)
        End If

        Dim needV As Boolean
        cardsLayer.SuspendLayout()
        Try
            cardsLayer.Width = innerFull
            LayoutDayCards(cardsLayer, remeasureUnchangedWidth:=False)
            needV = cardsLayer.Height > ch

            If needV Then
                cardsLayer.Width = innerWithScroll
                LayoutDayCards(cardsLayer, remeasureUnchangedWidth:=False)
                needV = cardsLayer.Height > ch
            End If

            If Not needV AndAlso cardsLayer.Width <> innerFull Then
                cardsLayer.Width = innerFull
                LayoutDayCards(cardsLayer, remeasureUnchangedWidth:=False)
            End If
        Finally
            ' True: one layout pass for ApptCard children after width/height adjustments.
            cardsLayer.ResumeLayout(True)
        End Try

        PositionDaySlimVScroll(scrollHost, bar)
        Dim maxOffset = Math.Max(0, cardsLayer.Height - ch)

        If needV Then
            bar.Visible = True
            bar.Minimum = 0
            bar.LargeChange = Math.Max(1, ch)
            bar.SmallChange = Math.Max(1, Math.Min(64, ch \ 8))
            bar.Maximum = maxOffset + bar.LargeChange - 1
            If bar.Value > maxOffset Then bar.Value = maxOffset
        Else
            bar.Value = 0
            bar.Visible = False
        End If

        cardsLayer.Left = 0
        cardsLayer.Top = -bar.Value

        s.LastLayoutCw = cw
        s.LastLayoutCh = ch
        _dayColumnScrolls(columnIndex) = s
    End Sub

    Private Sub RaiseEmptyDate(dayDate As DateTime)
        If InteractionHub IsNot Nothing Then
            InteractionHub.PublishEmptyDateInvoked(dayDate)
        End If
    End Sub

    Private Sub WireWeekDropTarget(c As Control, dayDate As DateTime)
        c.Tag = dayDate.Date
        c.AllowDrop = True
        AddHandler c.DragEnter, Sub(s, e) WeekColumn_DragEnter(e)
        AddHandler c.DragDrop, Sub(s, e) WeekColumn_DragDrop(dayDate.Date, e)
    End Sub

    Private Sub WeekColumn_DragEnter(e As DragEventArgs)
        If e.Data IsNot Nothing AndAlso e.Data.GetDataPresent("Appointment") Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub WeekColumn_DragDrop(targetDay As Date, e As DragEventArgs)
        If e.Data Is Nothing OrElse Not e.Data.GetDataPresent("Appointment") Then Return
        Dim appt = TryCast(e.Data.GetData("Appointment"), AppointmentC)
        If appt Is Nothing OrElse InteractionHub Is Nothing Then Return
        Dim sourceDay = CDate(e.Data.GetData("SourceDay"))
        InteractionHub.PublishWeekColumnAppointmentDrop(appt, sourceDay, targetDay)
    End Sub

    Private Sub OnWeekCardDragMouseDown(card As ApptCard, sourceDay As Date, e As MouseEventArgs)
        If e.Button <> MouseButtons.Left Then Return
        If card.BoundAppointment Is Nothing OrElse card.BoundAppointment.AppointmentID <= 0 Then Return
        _weekDragTimer.Stop()
        _weekDragCard = card
        _weekDragSourceDay = sourceDay.Date
        _weekDragTimer.Interval = Math.Max(200, If(_request IsNot Nothing, _request.DragHoldTimeMs, 750))
        _weekDragTimer.Start()
    End Sub

    Private Sub OnWeekCardDragMouseUp(card As ApptCard, e As MouseEventArgs)
        _weekDragTimer.Stop()
        If Not _weekColumnDragInProgress Then
            _weekDragCard = Nothing
            _weekDragSourceDay = Nothing
        End If
    End Sub

    Private Sub WeekDragTimer_Tick(sender As Object, e As EventArgs)
        _weekDragTimer.Stop()
        If _weekDragCard Is Nothing OrElse Not _weekDragSourceDay.HasValue Then Return
        If _weekDragCard.BoundAppointment Is Nothing OrElse _weekDragCard.BoundAppointment.AppointmentID <= 0 Then Return
        _weekColumnDragInProgress = True
        StartWeekCardDrag(_weekDragCard, _weekDragSourceDay.Value)
    End Sub

    ''' <summary>Same payload as <see cref="SchedulerNew.StartLabelDrag"/> (Appointment + SourceDay + SourceDoctor).</summary>
    Private Sub StartWeekCardDrag(card As ApptCard, sourceDay As Date)
        Try
            Dim ap = card.BoundAppointment
            If ap Is Nothing Then Return
            Dim dragData As New DataObject()
            dragData.SetData("Appointment", ap)
            dragData.SetData("SourceDay", sourceDay.Date)
            dragData.SetData("SourceDoctor", ap.DrID)
            card.DoDragDrop(dragData, DragDropEffects.Move)
        Finally
            _weekColumnDragInProgress = False
            _weekDragCard = Nothing
            _weekDragSourceDay = Nothing
        End Try
    End Sub

    ''' <summary>Scroll the day column so <paramref name="ap"/> is near the top (after host navigation).</summary>
    Private Sub RefreshAllDayColumnScrollExtents()
        If _workArea Is Nothing OrElse _dayColumnScrolls.Count = 0 Then Return
        ' WM_SETREDRAW is batched in LayoutWeekColumns; avoid double toggle here.
        For i = 0 To _dayColumnScrolls.Count - 1
            AdjustDayScrollExtentAt(i)
        Next
    End Sub

    Private Sub TryScrollToAppointment(ap As AppointmentC)
        If ap Is Nothing OrElse _request Is Nothing OrElse _dayHosts.Count = 0 Then Return
        Dim weekStart = GetWeekStartSaturday(_request.State.CurrentDate)
        Dim visibleDays = If(_compactSixDayWeek, 6, 7)
        Dim ix = CInt((ap.StartDateTime.Date - weekStart).TotalDays)
        If ix < 0 OrElse ix >= visibleDays Then Return
        Dim shell = _dayHosts(ix)
        Dim scrollHost = shell.Controls.OfType(Of XtraScrollableControl)().FirstOrDefault()
        If scrollHost Is Nothing Then Return
        Dim slimV = scrollHost.Controls.OfType(Of DevExpress.XtraEditors.VScrollBar)().FirstOrDefault()
        Dim cardsLayer = scrollHost.Controls.OfType(Of PanelControl)().FirstOrDefault()
        If slimV Is Nothing OrElse cardsLayer Is Nothing Then Return
        Dim cardCtl = cardsLayer.Controls.OfType(Of ApptCard)().FirstOrDefault(
            Function(c) c.BoundAppointment IsNot Nothing AndAlso c.BoundAppointment.AppointmentID > 0 AndAlso c.BoundAppointment.AppointmentID = ap.AppointmentID)
        If cardCtl Is Nothing Then
            cardCtl = cardsLayer.Controls.OfType(Of ApptCard)().FirstOrDefault(
                Function(c) c.BoundAppointment IsNot Nothing AndAlso
                    c.BoundAppointment.StartDateTime = ap.StartDateTime AndAlso
                    c.BoundAppointment.PatientID = ap.PatientID)
        End If
        If cardCtl Is Nothing Then Return
        Dim desired = Math.Max(0, cardCtl.Top - 8)
        If slimV.Visible Then
            Dim maxOff = Math.Max(0, slimV.Maximum - slimV.LargeChange + 1)
            slimV.Value = Math.Max(slimV.Minimum, Math.Min(desired, maxOff))
        Else
            slimV.Value = 0
        End If
        cardsLayer.Top = -slimV.Value
    End Sub

    Private Sub WeekView_Resize(sender As Object, e As EventArgs)
        If Not WeekViewDebounceLayoutOnResize Then
            LayoutWeekColumns()
            Return
        End If
        _weekResizeDebounce.Stop()
        Dim w = ClientSize.Width
        Dim h = ClientSize.Height
        Dim d = 0
        If _lastWeekClientW >= 0 Then
            d = Math.Abs(w - _lastWeekClientW) + Math.Abs(h - _lastWeekClientH)
        End If
        _lastWeekClientW = w
        _lastWeekClientH = h
        _weekResizeDebounce.Interval = If(d > WeekViewResizeBigJumpTotalPx, WeekViewResizeDebounceMaxMs, WeekViewResizeDebounceMs)
        _weekResizeDebounce.Start()
    End Sub

    Private Sub WeekResizeDebounce_Tick(sender As Object, e As EventArgs)
        _weekResizeDebounce.Stop()
        If IsDisposed OrElse Not IsHandleCreated Then Return
        LayoutWeekColumns()
    End Sub

    ''' <summary>Card width for snapshot when the slim vertical bar is hidden (match <see cref="AdjustDayScrollExtent"/> full-width path).</summary>
    Private Function WeekSnapshotInnerFullWidthForCards(scrollHost As XtraScrollableControl) As Integer
        If scrollHost Is Nothing Then Return 1
        Dim cw = Math.Max(1, scrollHost.ClientSize.Width)
        If _compactSixDayWeek Then
            Return cw
        End If
        Return Math.Max(180, cw - 16)
    End Function

    Private Shared Function FindTopDockedHeaderControl(shell As Control, scroll As Control) As Control
        If shell Is Nothing OrElse scroll Is Nothing Then Return Nothing
        For Each c As Control In shell.Controls
            If c IsNot scroll AndAlso c.Dock = DockStyle.Top Then
                Return c
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>Full unclipped week image for <see cref="SchedulerSnapshotShared"/>: each day column is expanded and composited to match classic scheduler snapshot behavior.</summary>
    ''' <remarks>
    ''' Do not use <see cref="Control.DrawToBitmap"/> on the day shell: <see cref="XtraScrollableControl"/> only paints the viewport, so a tall bitmap would be mostly empty below
    ''' the on-screen area. We composite the day header and cards layer directly, like the expanded in-flow week capture in the classic scheduler.
    ''' </remarks>
    Friend Function CaptureStitchedWeekSnapshotBitmap() As Bitmap
        If _dayHosts.Count = 0 OrElse _dayColumnScrolls.Count <> _dayHosts.Count Then Return Nothing
        For i = 0 To _dayHosts.Count - 1
            Dim sc = _dayColumnScrolls(i)
            If sc.Scroll Is Nothing OrElse sc.Bar Is Nothing OrElse sc.Cards Is Nothing Then Return Nothing
        Next

        Dim snaps As New List(Of WeekSnapshotColState)()
        Try
            For i = 0 To _dayHosts.Count - 1
                Dim shell = _dayHosts(i)
                Dim sc = _dayColumnScrolls(i)
                Dim s As WeekSnapshotColState
                s.ShellH = shell.Height
                s.BarVisible = sc.Bar.Visible
                s.BarValue = sc.Bar.Value
                s.CardsH = sc.Cards.Height
                s.CardsTop = sc.Cards.Top
                snaps.Add(s)
                sc.Bar.Visible = False
                sc.Bar.Value = 0
            Next
            ' Apply Bar.Visible = False before we read scroll ClientSize and inner full width.
            If _layout IsNot Nothing Then _layout.PerformLayout()
            If IsHandleCreated Then Application.DoEvents()

            ' Expand cards to full (no slim bar) width, restack, then use laid-out height.
            For i = 0 To _dayHosts.Count - 1
                Dim sc = _dayColumnScrolls(i)
                Dim innerFull = WeekSnapshotInnerFullWidthForCards(sc.Scroll)
                sc.Cards.SuspendLayout()
                sc.Cards.Width = innerFull
                sc.Cards.Top = 0
                sc.Cards.ResumeLayout(True)
                LayoutDayCards(sc.Cards)
            Next

            Dim finalMax = 0
            For i = 0 To _dayHosts.Count - 1
                Dim shell = _dayHosts(i)
                Dim sc = _dayColumnScrolls(i)
                Dim headerP = FindTopDockedHeaderControl(shell, sc.Scroll)
                Dim hdrH = If(headerP Is Nothing, 0, headerP.Height)
                finalMax = Math.Max(finalMax, hdrH + sc.Cards.Height + 4)
            Next
            If finalMax < 20 Then Return Nothing

            For i = 0 To _dayHosts.Count - 1
                _dayHosts(i).Height = finalMax
            Next

            _workArea.SuspendLayout()
            _weekRoot.SuspendLayout()
            _layout.SuspendLayout()
            _layout.ResumeLayout(True)
            _weekRoot.ResumeLayout(True)
            _workArea.ResumeLayout(True)
            _layout.PerformLayout()
            _weekRoot.PerformLayout()
            _workArea.PerformLayout()
            Application.DoEvents()

            Dim totalW = 1
            For Each h2 As Control In _dayHosts
                totalW = Math.Max(totalW, h2.Right + 8)
            Next
            Dim bg = _layout.BackColor
            Dim out As New Bitmap(Math.Max(1, totalW), finalMax)
            Using g As Graphics = Graphics.FromImage(out)
                g.Clear(bg)
                For Each sh In _dayHosts.OrderBy(Function(x) x.Left)
                    If sh.Width < 1 OrElse finalMax < 1 Then Continue For
                    Dim idx = _dayHosts.IndexOf(sh)
                    If idx < 0 OrElse idx >= _dayColumnScrolls.Count Then Continue For
                    Dim sc = _dayColumnScrolls(idx)
                    If sc.Cards Is Nothing OrElse sc.Scroll Is Nothing Then Continue For

                    Dim headerP = FindTopDockedHeaderControl(sh, sc.Scroll)
                    Dim hdrH = If(headerP Is Nothing, 0, headerP.Height)
                    Dim colW = sh.Width
                    Using colBmp As New Bitmap(colW, finalMax)
                        Using gCol As Graphics = Graphics.FromImage(colBmp)
                            gCol.Clear(bg)
                            If headerP IsNot Nothing AndAlso headerP.Width > 0 AndAlso headerP.Height > 0 Then
                                Using hb As New Bitmap(Math.Max(1, headerP.Width), Math.Max(1, headerP.Height))
                                    headerP.DrawToBitmap(hb, New Rectangle(0, 0, hb.Width, hb.Height))
                                    gCol.DrawImageUnscaled(hb, 0, 0)
                                End Using
                            End If
                            Dim cw = Math.Max(1, sc.Cards.Width)
                            Dim ch = Math.Max(1, sc.Cards.Height)
                            Using cb As New Bitmap(cw, ch)
                                sc.Cards.DrawToBitmap(cb, New Rectangle(0, 0, cw, ch))
                                Dim drawY = hdrH + sc.Cards.Top
                                gCol.DrawImageUnscaled(cb, sc.Cards.Left, drawY)
                            End Using
                        End Using
                        g.DrawImageUnscaled(colBmp, sh.Left, 0)
                    End Using
                Next
            End Using
            Return out
        Finally
            For i = 0 To _dayHosts.Count - 1
                If i >= snaps.Count OrElse i >= _dayColumnScrolls.Count Then Exit For
                Dim shell = _dayHosts(i)
                Dim sc = _dayColumnScrolls(i)
                Dim s = snaps(i)
                shell.Height = s.ShellH
                sc.Bar.Visible = s.BarVisible
                sc.Bar.Value = s.BarValue
                sc.Cards.Height = s.CardsH
                sc.Cards.Top = s.CardsTop
            Next
            LayoutWeekColumns()
        End Try
    End Function

    ''' <summary>Builds structured data for a readable week HTML page (see <see cref="WeekSnapshotHtmlWriter"/>), matching day headers and per-card colors.</summary>
    Friend Function BuildHtmlExportContext(weekCaption As String) As WeekSnapshotHtmlContext
        If _request Is Nothing OrElse _dayHosts.Count = 0 OrElse _dayColumnScrolls.Count <> _dayHosts.Count Then Return Nothing
        Dim weekStart = GetWeekStartSaturday(_request.State.CurrentDate)
        Dim ctx As New WeekSnapshotHtmlContext() With {
            .Caption = If(weekCaption, ""),
            .GeneratedUtc = DateTime.UtcNow
        }
        For i = 0 To _dayHosts.Count - 1
            Dim shell = _dayHosts(i)
            Dim sc = _dayColumnScrolls(i)
            If sc.Cards Is Nothing Then Continue For
            Dim dayDate As Date
            If shell IsNot Nothing AndAlso shell.Tag IsNot Nothing Then
                If TypeOf shell.Tag Is DateTime Then
                    dayDate = CDate(CType(shell.Tag, DateTime))
                ElseIf TypeOf shell.Tag Is Date Then
                    dayDate = CDate(CType(shell.Tag, Date))
                Else
                    dayDate = weekStart.AddDays(i).Date
                End If
            Else
                dayDate = weekStart.AddDays(i).Date
            End If

            Dim appts As New List(Of WeekSnapshotHtmlAppt)()
            For Each c In sc.Cards.Controls.OfType(Of ApptCard)().OrderBy(Function(x) x.Top)
                Dim row = c.TryGetWeekHtmlExportRow()
                If row IsNot Nothing Then appts.Add(row)
            Next
            Dim apptCount = appts.Count
            Dim apptSuffix = If(Eng, If(apptCount = 1, "Appt", "Appts"), If(apptCount = 1, "موعد", "مواعيد"))
            Dim dayHeaderBg = If(dayDate = Date.Today, WeekViewTodayHeaderColor, WeekViewDayHeaderColors(i Mod WeekViewDayHeaderColors.Length))
            Dim col As New WeekSnapshotHtmlColumn With {
                .DayTitle = dayDate.ToString("dddd", CultureInfo.CurrentCulture),
                .DateLine = $"{dayDate:dd MMM yyyy}.({apptCount} {apptSuffix})",
                .HeaderBackColor = dayHeaderBg,
                .HeaderTitleForeColor = Color.FromArgb(36, 64, 120),
                .DateLineForeColor = Color.FromArgb(75, 84, 99),
                .IsToday = (dayDate = Date.Today)
            }
            col.Appointments.AddRange(appts)
            ctx.Columns.Add(col)
        Next
        Return ctx
    End Function

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        If HandleWeekViewMouseWheel(e) Then Return
        MyBase.OnMouseWheel(e)
    End Sub

    Private Function HandleWeekViewMouseWheel(e As MouseEventArgs) As Boolean
        If e Is Nothing OrElse e.Delta = 0 OrElse _dayHosts Is Nothing OrElse _dayColumnScrolls Is Nothing Then Return False
        If _dayHosts.Count = 0 OrElse _dayColumnScrolls.Count <> _dayHosts.Count Then Return False
        Dim p = Me.PointToClient(Control.MousePosition)
        For i = 0 To _dayHosts.Count - 1
            Dim d = _dayHosts(i)
            If d Is Nothing OrElse Not d.Visible Then Continue For
            If i >= _dayColumnScrolls.Count Then Exit For
            Dim rScreen = d.RectangleToScreen(d.ClientRectangle)
            Dim r = Me.RectangleToClient(rScreen)
            If r.Contains(p) AndAlso _dayColumnScrolls(i).Bar IsNot Nothing AndAlso _dayColumnScrolls(i).Bar.Visible Then
                DayColumn_DoMouseWheel(_dayColumnScrolls(i).Bar, _dayColumnScrolls(i).Cards, e)
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub LayoutWeekColumns(Optional useWorkAreaRedraw As Boolean = True)
        If _dayHosts.Count = 0 Then Return

        Dim hWnd = _workArea.Handle
        Dim batchPaint = useWorkAreaRedraw AndAlso _workArea.IsHandleCreated AndAlso hWnd <> IntPtr.Zero
        If batchPaint Then NativeWm.SendMessage(hWnd, NativeWm.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero)
        Try
            _workArea.SuspendLayout()
            _weekRoot.SuspendLayout()
            For Each d In _dayHosts
                d.SuspendLayout()
            Next
            Try
                Dim spacing = 2
                Dim width = Math.Max(0, _layout.ClientSize.Width)
                Dim height = Math.Max(0, _layout.ClientSize.Height)
                Dim columnWidth = Math.Max(140, (width - (spacing * (_dayHosts.Count - 1))) \ _dayHosts.Count)
                Dim x = 0

                For Each host In _dayHosts
                    host.SetBounds(x, 0, columnWidth, height)
                    x += columnWidth + spacing
                Next
            Finally
                For Each d In _dayHosts
                    d.ResumeLayout(True)
                Next
                _weekRoot.ResumeLayout(True)
                _workArea.ResumeLayout(True)
            End Try

            If WeekViewUseCoalescedScrollRefresh Then
                RefreshAllDayColumnScrollExtents()
            End If
        Finally
            If batchPaint Then
                NativeWm.SendMessage(hWnd, NativeWm.WM_SETREDRAW, New IntPtr(1), IntPtr.Zero)
                _workArea.Invalidate(True)
            End If
        End Try
    End Sub

    Private NotInheritable Class DayColumnHeaderRef
        Public HeaderPanel As PanelControl
        Public TitleLabel As LabelControl
        Public DateLineLabel As LabelControl
    End Class
End Class

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
    Private ReadOnly _btnClose As Label
    Private ReadOnly _scroll As Panel

    Public Sub New(weekCtl As ApptWeekCtl)
        _weekCtl = weekCtl
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
        Width = 240
        BackColor = DrawerDayPanelWash
        BorderStyle = BorderStyle.Fixed3D
        Margin = New Padding(0)
        _header = New Panel With {.Dock = DockStyle.Top, .Height = 52, .BackColor = DrawerDayHeaderSolid}
        _lblTitle = New Label With {
            .AutoSize = False,
            .Dock = DockStyle.Fill,
            .Padding = New Padding(44, 14, 14, 8),
            .Font = CreateCalibriFont(11.0F, FontStyle.Bold),
            .ForeColor = Color.White,
            .BackColor = Color.Transparent,
            .TextAlign = ContentAlignment.TopLeft
        }
        _btnClose = New Label With {
            .Text = "✕",
            .Size = New Size(36, 36),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left,
            .Location = New Point(6, 8),
            .Font = CreateCalibriFont(12.0F, FontStyle.Bold),
            .ForeColor = DrawerHeaderCloseIdle,
            .BackColor = Color.Transparent,
            .Cursor = Cursors.Hand,
            .TextAlign = ContentAlignment.MiddleCenter
        }

        AddHandler _btnClose.Click, Sub() _weekCtl.RequestCloseWeekDrawer()
        AddHandler _btnClose.MouseEnter, Sub() _btnClose.ForeColor = Color.FromArgb(255, 200, 200)
        AddHandler _btnClose.MouseLeave, Sub() _btnClose.ForeColor = DrawerHeaderCloseIdle

        _header.Controls.Add(_lblTitle)
        _header.Controls.Add(_btnClose)
        _btnClose.BringToFront()
        AddHandler _header.SizeChanged, Sub() PositionCloseButton()

        _scroll = New Panel With {
            .Margin = New Padding(5),
            .BorderStyle = BorderStyle.Fixed3D,
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .BackColor = Color.DeepSkyBlue,'Color.Transparent,
            .Padding = New Padding(10, 10, 10, 14)
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

    Private Sub PositionCloseButton()
        If _header Is Nothing OrElse _btnClose Is Nothing Then Return
        Dim y = (_header.ClientSize.Height - _btnClose.Height) \ 2
        If Eng Then
            _btnClose.Left = 6
            _btnClose.Top = y
        Else
            _btnClose.Left = _header.ClientSize.Width - _btnClose.Width - 6
            _btnClose.Top = y
        End If
    End Sub

    Friend Sub RelayoutAfterOpen()
        PerformLayout()
        RelayoutDrawerCards()
        PositionCloseButton()
        Invalidate(True)
    End Sub

    Private Sub RelayoutDrawerCards()
        If _scroll Is Nothing Then Return
        Dim inner = Math.Max(80, _scroll.ClientSize.Width - _scroll.Padding.Horizontal)
        Dim w = Math.Max(80, inner - 8)
        Dim y = 0
        Const gap = 8
        For Each c As Control In _scroll.Controls
            c.Width = w
            Dim weekCard = TryCast(c, ApptCard)
            If weekCard IsNot Nothing Then
                weekCard.ApplyContentHeightToFitForWeekView()
            End If
            c.Left = 4
            c.Top = y
            y += c.Height + gap
        Next
    End Sub

    Public Sub Populate(day As Date, appts As List(Of AppointmentC), data As ApptDataBundle, state As ApptState, request As ApptViewRequest, weekHost As ApptWeekCtl)
        _scroll.Controls.Clear()
        If Eng Then
            RightToLeft = RightToLeft.No
            _lblTitle.RightToLeft = RightToLeft.No
            _scroll.RightToLeft = RightToLeft.No
            _header.RightToLeft = RightToLeft.No
            _lblTitle.Padding = New Padding(44, 14, 14, 8)
            _btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Else
            RightToLeft = RightToLeft.Yes
            _lblTitle.RightToLeft = RightToLeft.Yes
            _scroll.RightToLeft = RightToLeft.Yes
            _header.RightToLeft = RightToLeft.Yes
            _lblTitle.Padding = New Padding(14, 14, 44, 8)
            _btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        End If
        PositionCloseButton()
        _lblTitle.TextAlign = If(Eng, ContentAlignment.TopLeft, ContentAlignment.TopRight)
        _lblTitle.Text = FormatCaptionDayFull(day)

        Dim innerW = Math.Max(80, _scroll.ClientSize.Width - _scroll.Padding.Horizontal - 8)
        If appts.Count = 0 Then
            Dim emptyLbl As New Label With {
                .AutoSize = False,
                .Width = innerW,
                .Height = 56,
                .Text = If(Eng, "No appointments scheduled for this day.", "لا توجد مواعيد في هذا اليوم."),
                .Font = CreateCalibriFont(10.5F, FontStyle.Italic),
                .ForeColor = Color.FromArgb(118, 128, 145),
                .BackColor = DrawerDayPanelWash,
                .TextAlign = ContentAlignment.TopCenter
            }
            _scroll.Controls.Add(emptyLbl)
        Else
            For Each appointment In appts
                Dim model As New ApptCardVm With {
                    .Appointment = appointment,
                    .PatientName = data.ResolvePatientName(appointment.PatientID),
                    .DoctorInfo = data.ResolveDoctor(appointment.DrID)
                }
                model.Appearance = BuildDefaultCardAppearance(model, state)
                If request IsNot Nothing AndAlso request.AppointmentAppearanceSelector IsNot Nothing Then
                    Dim sel = request.AppointmentAppearanceSelector(model)
                    If sel IsNot Nothing Then model.Appearance = sel
                End If
                Dim card As New ApptCard With {
                    .Width = innerW,
                    .Left = 4
                }
                card.Bind(model, state.Use24HourFormat)
                weekHost.WireDrawerDayCard(card, day)
                _scroll.Controls.Add(card)
            Next
        End If
        _scroll.PerformLayout()
        RelayoutDrawerCards()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        PositionCloseButton()
    End Sub
End Class
#End Region
