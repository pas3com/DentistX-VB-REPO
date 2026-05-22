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
Partial Public Class ApptWeekCtl
    Inherits XtraUserControl
    Implements IApptViewCtl

#Region "Week view render performance (toggles + legacy revert note)"
    ' Legacy revert: set the two Bools to False, re-add  AddHandler _layout.Resize / per-column  scrollHost.Resize,
    ' restore simple LayoutWeekColumns (SetBounds only) and Adjust at end of BuildDayPanel, remove coalescing types/timer/Dispose.
    ''' <summary>True: one scroll refresh for all day columns from LayoutWeekColumns (not each columnâ€™s Resize) â€” big win on maximize.</summary>
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
    ''' <summary>True keeps the side-squeezed layout and also dims the remaining week grid for focus.</summary>
    Private Const WeekDayDrawerUseDimOverlay As Boolean = True
    ''' <summary>Default <see cref="WeekColumnRenderMode.ApptCard"/>; set <see cref="WeekColumnRenderMode.ApptCardWithCompactPaintOverflow"/> to preview capped rows without extra card handles.</summary>
    Private Const WeekViewColumnRenderMode As WeekColumnRenderMode = WeekColumnRenderMode.ApptCard
    ''' <summary>Painted overflow preview rows when <see cref="WeekViewColumnRenderMode"/> is <see cref="WeekColumnRenderMode.ApptCardWithCompactPaintOverflow"/>.</summary>
    Private Const WeekViewCompactPaintOverflowMaxPreviewRows As Integer = 12
#End Region

#Region "Native (batched week scroll refresh)"
    Private NotInheritable Class NativeWm
        <DllImport("user32.dll", EntryPoint:="SendMessageW", SetLastError:=False)>
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        End Function
        Public Const WM_SETREDRAW As Integer = 11
    End Class
#End Region

    Private Const WeekDaySlimScrollWidth As Integer = 8
    ''' <summary>Vertical gap between stacked appointment cards inside a day column.</summary>
    Private Const DayColumnCardVerticalGap As Integer = 2
    ''' <summary>Equal left/right inset for cards in the 6-day week column (inside the cards layer).</summary>
    Private Const DayColumnCardHorizontalInsetSixDay As Integer = 2

    ''' <summary>Satâ†’Fri tints, same as <see cref="SchedulerNew"/> week band.</summary>
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
    Private ReadOnly _drawerDialogHost As WeekDayCardDialogHost
    Private ReadOnly _dayColumns As New List(Of WeekDayColumnController)()
    Private ReadOnly _scheduleHeader As New ApptScheduleViewHeaderStrip() With {.Dock = DockStyle.Top}
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
    Private _dialogOverlayParent As Control
    Private _dialogExpandedHost As ApptHostCtl
    Private _dialogExpandedHostTemporarily As Boolean

    Private NotInheritable Class WeekReusableCardContext
        Public Property DayDate As Date
        Public Property IsWired As Boolean
    End Class

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
        _drawerDialogHost = New WeekDayCardDialogHost(Me) With {.Dock = DockStyle.Fill, .Visible = False}
        _workArea.Controls.Add(_drawerDialogHost)
        _weekRoot.Controls.Add(_workArea)
        _weekRoot.Controls.Add(_scheduleHeader)
        Controls.Add(_weekRoot)

        ApptTheme.SetControlDoubleBuffered(_weekRoot)
        ApptTheme.SetControlDoubleBuffered(_workArea)
        ApptTheme.SetControlDoubleBuffered(_layout)

        AddHandler Resize, AddressOf WeekView_Resize
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            Try
                If _dialogOverlayParent IsNot Nothing Then
                    RemoveHandler _dialogOverlayParent.SizeChanged, AddressOf DialogOverlayParent_SizeChanged
                    _dialogOverlayParent = Nothing
                End If
                _dialogExpandedHost = Nothing
                _dialogExpandedHostTemporarily = False
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "ApptWeekCtl.Dispose.DialogOverlayParentHandler", showUser:=False)
            End Try

            Try
                _weekDragTimer.Stop()
                RemoveHandler _weekDragTimer.Tick, AddressOf WeekDragTimer_Tick
                _weekDragTimer.Dispose()
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "ApptWeekCtl.Dispose.WeekDragTimer", showUser:=False)
            End Try

            Try
                If _weekResizeDebounce IsNot Nothing Then
                    _weekResizeDebounce.Stop()
                    RemoveHandler _weekResizeDebounce.Tick, AddressOf WeekResizeDebounce_Tick
                    _weekResizeDebounce.Dispose()
                End If
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "ApptWeekCtl.Dispose.WeekResizeDebounce", showUser:=False)
            End Try

            Try
                RemoveHandler Resize, AddressOf WeekView_Resize
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "ApptWeekCtl.Dispose.ResizeHandler", showUser:=False)
            End Try
        End If
        MyBase.Dispose(disposing)
    End Sub

    Public Property InteractionHub As ApptInteractionHub Implements IApptViewCtl.InteractionHub

    Public Sub BindData(request As ApptViewRequest) Implements IApptViewCtl.BindData
        CloseWeekDayDrawerImmediate()
        _request = request
        _scheduleHeader.Apply(request)
        RenderWeek()
        If request IsNot Nothing Then TryScrollToAppointment(request.PendingScrollAppointment)
    End Sub

End Class
