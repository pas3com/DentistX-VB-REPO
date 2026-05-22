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

Partial Public Class ApptWeekCtl
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
            If _dayColumns.Count > 0 AndAlso WeekViewScrollHeightsLookUndersized() Then
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
            DisposeChildControls(_layout)
            _layout.Columns.Clear()
            _layout.Rows.Clear()
            _dayColumns.Clear()
            InvalidateWeekViewCache()

            _layout.Rows.Add(New TablePanelRow(TablePanelEntityStyle.Relative, 100.0F))

            For i = 0 To visibleDays - 1
                _layout.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Relative, 100.0F / visibleDays))
            Next

            For i = 0 To visibleDays - 1
                Dim dayDate = weekStart.AddDays(i)
                Dim dayColumn = WeekDayColumnController.Create(Me, dayDate, i, state, _request.Data)
                dayColumn.Shell.Dock = DockStyle.None
                _layout.Controls.Add(dayColumn.Shell)
                _layout.SetColumn(dayColumn.Shell, i)
                _layout.SetRow(dayColumn.Shell, 0)
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
        If _dayColumns.Count > 0 AndAlso WeekViewScrollHeightsLookUndersized() Then
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
            DisposeChildControls(_layout)
            _layout.Columns.Clear()
            _layout.Rows.Clear()
            _dayColumns.Clear()
            InvalidateWeekViewCache()
        Finally
            _layout.ResumeLayout(True)
        End Try
    End Sub

    Private Function CanSoftRefreshWeek(weekStart As Date, visibleDays As Integer, compact As Boolean) As Boolean
        If _dayColumns.Count = 0 Then Return False
        If _dayColumns.Count <> visibleDays Then Return False
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
                Dim dayColumn = _dayColumns(i)
                dayColumn.UpdateHeader(apptCount, state)
                dayColumn.BindAppointments(state, data)
                dayColumn.ResetScrollLayoutCache()
            Next
            LayoutWeekColumns(False)
        Finally
            If hWorkRedraw <> IntPtr.Zero Then
                NativeWm.SendMessage(hWorkRedraw, NativeWm.WM_SETREDRAW, New IntPtr(1), IntPtr.Zero)
                _workArea.Invalidate(True)
            End If
        End Try
    End Sub
    Private Function WeekViewScrollHeightsLookUndersized() As Boolean
        If _dayColumns Is Nothing OrElse _dayColumns.Count = 0 Then Return True
        For Each dayColumn In _dayColumns
            If dayColumn.ScrollHostHeightLooksUndersized() Then Return True
        Next
        Return False
    End Function

    Private Sub OnWeekViewScrollLayoutDeferred()
        If IsDisposed OrElse Not IsHandleCreated OrElse _dayColumns.Count = 0 Then Return
        LayoutWeekColumns()
        If _request IsNot Nothing AndAlso _request.PendingScrollAppointment IsNot Nothing Then
            TryScrollToAppointment(_request.PendingScrollAppointment)
        End If
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
End Class
