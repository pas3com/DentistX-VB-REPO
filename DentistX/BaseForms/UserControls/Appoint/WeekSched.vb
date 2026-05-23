Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Reflection
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils.Layout
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

Public Class WeekSched
    Implements IPatientAwareUserControl



    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        SyncCurrentPatientFromForm(patientId)
        Me.SuspendLayout()
        LoadPatientData(patientId)
        Me.ResumeLayout()
    End Sub
    ''' <summary>Sync CurrentPatient from base form or FormManager so control sees current patient.</summary>
    Private Sub SyncCurrentPatientFromForm(patientId As Integer)
        Dim parentWs = PatientAwareHelper.FindPatientWorkspace(Me)
        If parentWs IsNot Nothing AndAlso parentWs.Current_Patient IsNot Nothing AndAlso parentWs.Current_Patient.PatientID = patientId Then
            CurrentPatient = parentWs.Current_Patient
        ElseIf FormManager.Instance.IsBasePatientFormOpen AndAlso FormManager.Instance.GetCurrentPatient() IsNot Nothing AndAlso FormManager.Instance.GetCurrentPatient().PatientID = patientId Then
            CurrentPatient = FormManager.Instance.GetCurrentPatient()
        End If
    End Sub

    Public Sub LoadPatientData(patientId As Integer)
        ' patientId kept for IPatientAware contract / SyncCurrentPatientFromForm; default list is all patients.
        FilterPatientId = Nothing
        FilterDoctorId = Nothing
        FilterReason = Nothing
        FilterStatus = Nothing
        lblPatient.Text = If(Eng, "All Patients", "كل المرضى")
        LoadAndRender()
    End Sub

    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 1 — GLOBALS                                                 ║
    ' ║  All class-level field declarations, organized by subsystem.         ║
    ' ╚══════════════════════════════════════════════════════════════════════╝



#Region "═══ 1. GLOBALS ═══"

    ' ──────────────────────────────────────────
    '  1.1  Enums
    ' ──────────────────────────────────────────

#Region "1.1 Enums"
    Public Enum ViewMode
        'DayView
        'ThisWeek
        ThisWeekFull
        'MonthlyWeek
        'MonthView
        'DaysTimeline
        'DoctorsDay
    End Enum
#End Region

    ' ──────────────────────────────────────────
    '  1.2  Core State
    ' ──────────────────────────────────────────

#Region "1.2 Core State"
    Private _repo As AppointmentCRepository
    Private _AppointmentC As BindingList(Of AppointmentC)
    Private _currentDate As DateTime = DateTime.Today
    ''' <summary>Default: 7-day week (Sat–Fri). Kept in sync with cmbView on first paint.</summary>
    Private _view As ViewMode = ViewMode.ThisWeekFull
    Private _allAppts As List(Of AppointmentC) = New List(Of AppointmentC)()
    Private _appointmentCachePrimed As Boolean
    Private localPatientID As Integer = 0
#End Region

    ' ──────────────────────────────────────────
    '  1.3  Working Hours
    ' ──────────────────────────────────────────

#Region "1.3 Working Hours"
    Private _workStart As TimeSpan = New TimeSpan(8, 0, 0)
    Private _workEnd As TimeSpan = New TimeSpan(16, 0, 0)
    Private _startHour As Integer = 8
    Private _endHour As Integer = 16
    Private _slotHeight As Integer = 60
    ''' <summary>Blocks feedback when programmatically syncing dtEndTime with dtStartTime.</summary>
    Private _suppressWorkingHoursPickerSync As Boolean
    ''' <summary>When start time changes, end time is set to start + this many hours (same calendar day, clamped to 23:59:59).</summary>
    Private Const SchedulerAutoEndOffsetHours As Double = 8.0R
#End Region

    ' ──────────────────────────────────────────
    '  1.4  Day View — Rendering & Card State
    ' ──────────────────────────────────────────

#Region "1.4 Day View Rendering State"
    Private statusLabel As Label = Nothing
    Private _activeCard As Panel = Nothing
    Private _dragMode As String = ""            ' "Drag", "ResizeTop", "ResizeBottom"
    Private _pendingDragMode As String = ""
    Private _dragOffset As Point
    Private _startMouseScreenY As Integer
    Private _dragChanged As Boolean
    Private _dragReady As Boolean
    Private _dragHoldTimer As System.Windows.Forms.Timer
    Private _dragHoldDelayMs As Integer = 750
    Private _ghost As Panel = Nothing           ' Ghost panel for drag preview
    Private _previewLine As Panel = Nothing     ' Snap preview line
    Private _renderDay As DateTime
    Private _renderHeaderTop As Integer
    ''' <summary>Top Y pixel of the first time-slot row (day view: header + subheader; Doctors Day: header row only).</summary>
    Private _renderGridTop As Integer
    Private _renderPixelsPerMinute As Double
    Private _renderDoctorBaseLeft As Integer
    Private _renderDoctorWidth As Integer
    Private _renderDoctorCount As Integer
    ''' <summary>Doctors Day only: column geometry for drag → doctor resolution (cleared in day view render).</summary>
    Private _ddDoctorLefts As Integer()
    Private _ddDoctorWidths As Integer()
    Private _ddDoctorIds As Integer()
    ''' <summary>Reserved for legacy day-view export filtering; unused in week-only host.</summary>
    Private _dayViewSingleDoctorFilterId As Integer
    Private currentDay As Date
    Private workingStartHour As Integer
    Private originalStartTime As DateTime
    Private originalEndTime As DateTime
    Private originalDrId As Integer
    Private originalAppDate As Date
    Private infoLabel As Label = Nothing
    Private ReadOnly _snapMinutes As Integer = 15
#End Region

    ' ──────────────────────────────────────────
    '  1.5  Drag & Drop (shared across views)
    ' ──────────────────────────────────────────


#Region "1.5 Drag & Drop"
    Private _dragTimer As System.Windows.Forms.Timer
    Private _dragSourceList As ListBox = Nothing
    Private _dragStartPoint As Point
    Private _dragHoldMs As Integer = 1000       ' hold time before drag starts (ms)
    Private _dragMoveThreshold As Integer = 6   ' pixels of movement before cancelling
    Private _dragInitiated As Boolean = False
    ' Week view drag
    Private _dragTimerThisWeek As New Timer()
    Private _isDragOperation As Boolean = False
    Private _dragStartLabel As Label = Nothing
    Private _dragStartCard As Panel = Nothing
    Private _isShuttingDown As Boolean = False
#End Region

    ' ──────────────────────────────────────────
    '  1.6  Edge Hint Arrows (prev/next nav)
    ' ──────────────────────────────────────────


#Region "1.6 Edge Hint Arrows"
    ' Day view hints
    Private _dayPrevHint As ArrowLable
    Private _dayNextHint As ArrowLable
    Private _dayAppointmentCForHints As List(Of AppointmentC)
    Private _dayPrevTarget As AppointmentC
    Private _dayNextTarget As AppointmentC
    Private _pendingScrollTarget As AppointmentC
    ' Week view hints
    Private _weekPrevHint As ArrowLable
    Private _weekNextHint As ArrowLable
    Private _weekAppointmentCForHints As List(Of AppointmentC)
    Private _weekPrevTargetStart As DateTime?
    Private _weekNextTargetStart As DateTime?
    ' Month view hints
    Private _monthPrevHint As ArrowLable
    Private _monthNextHint As ArrowLable
    Private _monthAppointmentCForHints As List(Of AppointmentC)
    Private _monthPrevTargetStart As DateTime?
    Private _monthNextTargetStart As DateTime?
    ' Days Timeline hints
    Private _tlPrevHint As ArrowLable
    Private _tlNextHint As ArrowLable
    Private _tlAppointmentCForHints As List(Of AppointmentC)
    Private _tlPrevTargetStart As DateTime?
    Private _tlNextTargetStart As DateTime?
#End Region

    ' ──────────────────────────────────────────
    '  1.7  Slide Animation
    ' ──────────────────────────────────────────


#Region "1.7 Slide Animation"
    Private _slideTimer As System.Windows.Forms.Timer
    Private _slidePanel As Panel
    Private _slideFrom As PictureBox
    Private _slideTo As PictureBox
    Private _slideStartTime As DateTime
    Private _slideDirection As Integer
    Private _slideAnimating As Boolean = False
    Private _slideDurationMs As Integer = 1000
#End Region

    ' ──────────────────────────────────────────
    '  1.8  Resize
    ' ──────────────────────────────────────────


#Region "1.8 Resize"
    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1387
    Private Const OriginalPanelHeight As Integer = 660
    Private ReadOnly originalPanelSize As New Size(OriginalPanelWidth, OriginalPanelHeight)
    Private _resizeTimer As System.Windows.Forms.Timer
#End Region

    ' ──────────────────────────────────────────
    '  1.8b Body workspace expand (hide header / full-height pnlBody)
    ' ──────────────────────────────────────────

#Region "1.8b Body workspace expand"
    Private Const BodyEdgeHintWidth As Integer = 36
    Private Const BodyEdgeHintHeight As Integer = 90
    Private Const BodyEdgeHintHostPadding As Integer = 0
    Private Const BodyEdgeHintBandWidth As Single = BodyEdgeHintWidth

    Private _bodyViewHost As PanelControl
    Private _bodyPrevHintHost As PanelControl
    Private _bodyNextHintHost As PanelControl
    Private _bodyLayoutHost As TablePanel
    Private _bodyWorkspaceEventsWired As Boolean
    Private _btnBodyExpand As SimpleButton
    ''' <summary>Toolbar duplicate for Add; visible only while body is expanded (maximized).</summary>
    Private _btnBodyQuickAdd As SimpleButton
    Private _pnlBodyWorkspaceMaximized As Boolean
    Private _tlpMainRowsCaptured As Boolean
    Private _tlpMainRow0Style As TablePanelEntityStyle
    Private _tlpMainRow0Height As Single
    Private _tlpMainRow1Style As TablePanelEntityStyle
    Private _tlpMainRow1Height As Single
#End Region

    ' ──────────────────────────────────────────
    '  1.9  UI Control References
    ' ──────────────────────────────────────────


#Region "1.9 UI Control References"
    Private _appointmentFont As Font
    Private _appointmentFontKey As String
    Private _cboDocsLegend As ComboBoxEdit
    Private _schedulerToolTip As ToolTip
    Private _weekSnapshotToolTip As ToolTip
    Private _statusContextMenu As ContextMenuStrip
    Private _statusContextMenuFont As Font
    Private _statusContextMenuAppointment As AppointmentC
    Private _statusContextMenuLabel As Control
    Private _statusContextMenuColors As Dictionary(Of String, Color)
    Private _legendOptionHandlersWired As Boolean
    Private ReadOnly _viewNavBack As New Stack(Of ViewMode)
    Private ReadOnly _viewNavForward As New Stack(Of ViewMode)
    Private _suppressViewNavRecording As Boolean
    ''' <summary>Blocks LoadAndRender from cmbView while the designer applies resources (avoids Month/day flash).</summary>
    Private _suppressComboRenderDuringInit As Boolean
    Private _suppressGotoDateSync As Boolean
    ''' <summary>When switching to day view from a week card, filter columns to this doctor once.</summary>
    Private _pendingDayViewDoctorFilter As Integer?
    Private _loadAndRenderInProgress As Boolean
    Private _loadAndRenderQueued As Boolean
    Private _lastSchedulerHandlePressureLogUtc As DateTime = DateTime.MinValue
#End Region

    ' ──────────────────────────────────────────
    '  1.10  Doctor Selection
    ' ──────────────────────────────────────────


#Region "1.10 Doctor Selection"
    Private Property SelectedDoctorID As Integer?
    Private Property SelectedDoctorName As String = String.Empty
    Private doctorQrs As New DoctorsDATA
#End Region

#End Region ' ═══ END GLOBALS ═══



    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 2 — PUBLIC PROPERTIES                                       ║
    ' ╚══════════════════════════════════════════════════════════════════════╝




#Region "═══ 2. PUBLIC PROPERTIES ═══"
    Public Property ShowPatientLbls As Boolean
    Public Property WorkStartTime As TimeSpan = TimeSpan.FromHours(8)
    Public Property WorkEndTime As TimeSpan = TimeSpan.FromHours(16)
    Public Property Use24HourFormat As Boolean = False

    ''' <summary>Start/end clock on appointment cards, chips, exports, and list rows (matches <see cref="Use24HourFormat"/>).</summary>
    Private Function AppointmentCardTimeFormatString() As String
        Return If(Use24HourFormat, "HH:mm", "hh:mm tt")
    End Function

    ''' <summary>
    ''' When false (default), Add opens only if <see cref="CurrentPatient"/> is set (patient workspace / main form).
    ''' When true, Add still prefills from current patient if available; otherwise opens the editor with PatientID 0 so the user picks a patient in the dialog (e.g. <see cref="FrmSchedulerFull"/> modal).
    ''' </summary>
    Public Property AllowAddWithoutCurrentPatient As Boolean

    Public Property DragHoldTimeMs As Integer
        Get
            Return _dragHoldMs
        End Get
        Set(value As Integer)
            _dragHoldMs = Math.Max(200, value)
            If _dragTimer IsNot Nothing Then _dragTimer.Interval = _dragHoldMs
        End Set
    End Property

    Public Property StartHour As Integer
        Get
            Return _startHour
        End Get
        Set(value As Integer)
            _startHour = Math.Max(0, Math.Min(23, value))
            LoadAndRender()
        End Set
    End Property

    Public Property EndHour As Integer
        Get
            Return _endHour
        End Get
        Set(value As Integer)
            _endHour = Math.Max(_startHour + 1, Math.Min(24, value))
            LoadAndRender()
        End Set
    End Property

    Private Sub chkUse24_CheckedChanged(sender As Object, e As EventArgs) Handles chkUse24.CheckedChanged
        If chkUse24.Checked = True Then
            Use24HourFormat = True
            LoadAndRender()
        Else
            Use24HourFormat = False
            LoadAndRender()
        End If
    End Sub

    ''' <summary>Popup commit runs after EditValueChanged; saving in EditValue alone can persist the previous time.</summary>
    Private Sub dtStartTime_EditValueChanged(sender As Object, e As EventArgs) Handles dtStartTime.EditValueChanged
        If _suppressWorkingHoursPickerSync Then Return
        SyncAutoEndTimeFromStartPicker()
        ApplyWorkingHoursFromPickers(persistToSettings:=False, refreshView:=True)
    End Sub

    Private Sub dtEndTime_EditValueChanged(sender As Object, e As EventArgs) Handles dtEndTime.EditValueChanged
        If _suppressWorkingHoursPickerSync Then Return
        ApplyWorkingHoursFromPickers(persistToSettings:=False, refreshView:=True)
    End Sub

    Private Sub dtStartTime_CloseUp(sender As Object, e As CloseUpEventArgs) Handles dtStartTime.CloseUp
        If e.CloseMode = PopupCloseMode.Cancel Then Return
        If _suppressWorkingHoursPickerSync Then Return
        ' After OK, some skins fire CloseUp without a final EditValueChanged — sync end and persist once.
        SyncAutoEndTimeFromStartPicker()
        ApplyWorkingHoursFromPickers(persistToSettings:=True, refreshView:=True)
    End Sub

    Private Sub dtEndTime_CloseUp(sender As Object, e As CloseUpEventArgs) Handles dtEndTime.CloseUp
        If e.CloseMode = PopupCloseMode.Cancel Then Return
        If _suppressWorkingHoursPickerSync Then Return
        ApplyWorkingHoursFromPickers(persistToSettings:=True, refreshView:=True)
    End Sub

    Private Sub dtStartTime_Leave(sender As Object, e As EventArgs) Handles dtStartTime.Leave
        If _suppressWorkingHoursPickerSync Then Return
        ' Do not auto-shift end here: focus can pass through start unchanged; would wipe a custom end time.
        ApplyWorkingHoursFromPickers(persistToSettings:=True, refreshView:=True)
    End Sub

    Private Sub dtEndTime_Leave(sender As Object, e As EventArgs) Handles dtEndTime.Leave
        If _suppressWorkingHoursPickerSync Then Return
        ApplyWorkingHoursFromPickers(persistToSettings:=True, refreshView:=True)
    End Sub

    Private Sub SyncAutoEndTimeFromStartPicker()
        _suppressWorkingHoursPickerSync = True
        Try
            Dim st = AppointmentTimeFromDateEdit(dtStartTime, New TimeSpan(8, 0, 0))
            Dim endCand = st.AddHours(SchedulerAutoEndOffsetHours)
            If endCand.Date > st.Date Then
                endCand = st.Date.AddDays(1).AddTicks(-1L)
            End If
            dtEndTime.EditValue = endCand
        Finally
            _suppressWorkingHoursPickerSync = False
        End Try
    End Sub

    Private Sub InitializeWorkingHoursPickersFromSettings()
        If dtStartTime Is Nothing OrElse dtEndTime Is Nothing Then Return
        Dim ws As TimeSpan
        Dim we As TimeSpan
        If Not TryLoadWorkingHoursFromUserSettings(ws, we) Then
            ws = New TimeSpan(8, 0, 0)
            we = New TimeSpan(16, 0, 0)
        End If
        If we <= ws Then we = ws.Add(TimeSpan.FromHours(SchedulerAutoEndOffsetHours))
        _suppressWorkingHoursPickerSync = True
        Try
            Dim d0 = DateTime.Today.Date
            dtStartTime.EditValue = d0.Add(ws)
            dtEndTime.EditValue = d0.Add(we)
        Finally
            _suppressWorkingHoursPickerSync = False
        End Try
        ApplyWorkingHoursFromPickers(persistToSettings:=False, refreshView:=False)
    End Sub

    ''' <summary>Reads persisted clinic hours; false if settings are missing or invalid (corrupt user.config TimeSpan).</summary>
    Private Function TryLoadWorkingHoursFromUserSettings(ByRef startTod As TimeSpan, ByRef endTod As TimeSpan) As Boolean
        Try
            Dim s = My.Settings.SchedulerWorkingDayStart
            Dim e_ = My.Settings.SchedulerWorkingDayEnd
            If s < TimeSpan.Zero OrElse s >= TimeSpan.FromDays(1) Then Return False
            If e_ <= TimeSpan.Zero OrElse e_ > TimeSpan.FromDays(1) Then Return False
            startTod = s
            endTod = e_
            Return True
        Catch
            Return False
        End Try
    End Function

    Private Function AppointmentTimeFromDateEdit(de As DateEdit, defaultTimeOfDay As TimeSpan) As DateTime
        If de Is Nothing OrElse de.EditValue Is Nothing OrElse IsDBNull(de.EditValue) Then
            Dim fallback As DateTime
            If TryGetDateTimeFromEditorValue(If(de Is Nothing, Nothing, de.Text), fallback) Then
                Return DateTime.Today.Add(fallback.TimeOfDay)
            End If
            Return DateTime.Today.Add(defaultTimeOfDay)
        End If
        Dim raw As DateTime
        If TryGetDateTimeFromEditorValue(de.EditValue, raw) OrElse
           TryGetDateTimeFromEditorValue(de.Text, raw) Then
            Return DateTime.Today.Add(raw.TimeOfDay)
        End If
        Return DateTime.Today.Add(defaultTimeOfDay)
    End Function

    Private Shared Function TryGetDateTimeFromEditorValue(raw As Object, ByRef value As DateTime) As Boolean
        value = DateTime.MinValue
        If raw Is Nothing OrElse IsDBNull(raw) Then Return False

        If TypeOf raw Is DateTime Then
            value = DirectCast(raw, DateTime)
            Return True
        End If

        If TypeOf raw Is Date Then
            value = CDate(raw)
            Return True
        End If

        Dim text = TryCast(raw, String)
        If String.IsNullOrWhiteSpace(text) Then
            text = Convert.ToString(raw, CultureInfo.CurrentCulture)
        End If
        If String.IsNullOrWhiteSpace(text) Then Return False

        Return DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces, value) OrElse
               DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, value)
    End Function

    Private Sub ApplyWorkingHoursFromPickers(persistToSettings As Boolean, refreshView As Boolean)
        If dtStartTime Is Nothing OrElse dtEndTime Is Nothing Then Return
        Dim stComb = AppointmentTimeFromDateEdit(dtStartTime, New TimeSpan(8, 0, 0))
        Dim defaultEnd = stComb.AddHours(SchedulerAutoEndOffsetHours).TimeOfDay
        Dim etComb = AppointmentTimeFromDateEdit(dtEndTime, defaultEnd)
        _workStart = stComb.TimeOfDay
        _workEnd = etComb.TimeOfDay
        If _workEnd <= _workStart Then
            _workEnd = _workStart.Add(TimeSpan.FromHours(1))
            _suppressWorkingHoursPickerSync = True
            Try
                dtEndTime.EditValue = DateTime.Today.Add(_workEnd)
            Finally
                _suppressWorkingHoursPickerSync = False
            End Try
        End If
        WorkStartTime = _workStart
        WorkEndTime = _workEnd
        _startHour = Math.Max(0, Math.Min(23, CInt(Math.Floor(_workStart.TotalHours))))
        _endHour = Math.Min(24, Math.Max(_startHour + 1, CInt(Math.Ceiling(_workEnd.TotalHours))))
        If persistToSettings Then
            My.Settings.SchedulerWorkingDayStart = _workStart
            My.Settings.SchedulerWorkingDayEnd = _workEnd
            My.Settings.Save()
        End If
        If refreshView Then LoadAndRender()
    End Sub
#End Region




    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 3 — FILTER PROPERTIES                                       ║
    ' ╚══════════════════════════════════════════════════════════════════════╝



#Region "═══ 3. FILTER PROPERTIES ═══"
    Public Property FilterPatientId As Nullable(Of Integer)
    Public Property FilterDoctorId As Nullable(Of Integer)
    Public Property FilterReason As String
    ''' <summary>Filter by appointment status (e.g. Pending, Running, Completed, Canceled, Postponed). Nothing or empty = all statuses.</summary>
    Public Property FilterStatus As String
    Public Property FilterStartDate As DateTime
    Public Property FilterEndDate As DateTime
    ''' <summary>DrID for each doctor filter slot (slot 1 → first doctor button in <see cref="flpDoctorFilterButtons"/>). Parallel to <see cref="_doctorSlotColors"/>.</summary>
    Private ReadOnly _doctorSlotDrIds As New List(Of Integer)()
    ''' <summary>Background for each doctor slot; used by filter chip styling.</summary>
    Private ReadOnly _doctorSlotColors As New List(Of Color)()
    ''' <summary>Doctor filter buttons beyond the first 8 designer slots (same row, horizontal scroll).</summary>
    Private ReadOnly _extraDoctorFilterButtons As New List(Of SimpleButton)()
    Private _doctorSlotClickHandlersWired As Boolean
    ''' <summary>Baseline relative row weights for <see cref="filtersTable"/> (must match WeekSched.Designer).</summary>
    Private Shared ReadOnly _filtersBaseR0 As Single = 36.53F
    Private Shared ReadOnly _filtersBaseR1 As Single = 35.8F
    Private Shared ReadOnly _filtersBaseR2 As Single = 26.67F
    ''' <summary>Weight shifted from row 0 and row 2 into row 1 when the doctor chip strip needs horizontal scroll.</summary>
    Private Const FiltersDoctorRowBoostTakeFromNeighbor As Single = 2.0F
    ''' <summary>Custom slim horizontal scrollbar height for doctor filter strip.</summary>
    Private Const DoctorFilterSlimScrollHeight As Integer = 8
    Private _doctorFilterSlimScroll As DevExpress.XtraEditors.HScrollBar
    Private _doctorFilterSlimScrollWired As Boolean
    Private _doctorFilterCompactionBaseCaptured As Boolean
    Private _doctorFilterBasePanelPadding As Padding
    Private _doctorFilterBaseFlowPadding As Padding
    Private ReadOnly _doctorFilterButtonBaseMargins As New Dictionary(Of String, Padding)(StringComparer.OrdinalIgnoreCase)
    Private _filtersDoctorRowBoostActive As Boolean
    ''' <summary>Skips filter button <c>Click</c> side effects when syncing visuals from <see cref="FilterPatientId"/> / <see cref="FilterStatus"/> / <see cref="FilterDoctorId"/>.</summary>
    Private _suppressRadioFilterSync As Boolean
#End Region





    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 4 — PUBLIC EVENTS                                           ║
    ' ╚══════════════════════════════════════════════════════════════════════╝





#Region "═══ 4. PUBLIC EVENTS ═══"
    Public Event AppointmentClicked As Action(Of AppointmentC)
    Public Event AppointmentDoubleClicked As Action(Of AppointmentC)
    Public Event AppointmentDeleted As Action(Of Integer)
#End Region




    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 5 — CONSTRUCTORS & INITIALIZATION                           ║
    ' ╚══════════════════════════════════════════════════════════════════════╝




#Region "═══ 5. CONSTRUCTORS & INITIALIZATION ═══"

    Public Sub New()

        ' Repository before InitializeComponent: DateEdit may raise EditValueChanged during designer init,
        ' which must not call LoadAndRender before _repo exists.
        _repo = New AppointmentCRepository(DentistXDATA.GetConnection.ConnectionString)
        ' This call is required by the designer.
        _suppressComboRenderDuringInit = True
        Try
            InitializeComponent()
        Finally
            _suppressComboRenderDuringInit = False
        End Try
        ApplyPerformanceRenderingOptions()
        'If lblRange IsNot Nothing Then lblRange.Appearance.TextOptions.Trimming = DevExpress.Utils.TextTrimming.None

        ' Add any initialization after the InitializeComponent() call.
        ' default date range
        FilterStartDate = DateTime.Today.AddMonths(-1)
        FilterEndDate = DateTime.Today.AddMonths(1)
        ' in constructor / Load
        _dragTimer = New System.Windows.Forms.Timer() With {.Interval = _dragHoldMs}
        AddHandler _dragTimer.Tick, AddressOf DragTimer_Tick
        _resizeTimer = New System.Windows.Forms.Timer() With {.Interval = 50}
        AddHandler _resizeTimer.Tick, AddressOf ResizeTimer_Tick
        AddHandler Me.Resize, AddressOf OnScheduleResize
        AddHandler Me.Disposed, Sub()
                                    If _appointmentFont IsNot Nothing Then
                                        _appointmentFont.Dispose()
                                        _appointmentFont = Nothing
                                    End If
                                    If _schedulerToolTip IsNot Nothing Then
                                        _schedulerToolTip.Dispose()
                                        _schedulerToolTip = Nothing
                                    End If
                                    If _weekSnapshotToolTip IsNot Nothing Then
                                        _weekSnapshotToolTip.Dispose()
                                        _weekSnapshotToolTip = Nothing
                                    End If
                                    If _statusContextMenu IsNot Nothing Then
                                        _statusContextMenu.Dispose()
                                        _statusContextMenu = Nothing
                                    End If
                                    If _statusContextMenuFont IsNot Nothing Then
                                        _statusContextMenuFont.Dispose()
                                        _statusContextMenuFont = Nothing
                                    End If
                                End Sub
        If btnPrevView IsNot Nothing Then btnPrevView.Visible = False
        If btnNextView IsNot Nothing Then btnNextView.Visible = False
        EnsureHeaderToolbarButtonHeightHook()
        EnsureHeaderToolbarCenteringHooks()
        EnsureFiltersTableButtonHeightHook()
        InitializeWorkingHoursPickersFromSettings()
        SyncDefaultWeekViewFromCombo()
        EnsureSchedulerBodyWorkspace()
        EnsureBodyWorkspaceExpandButton()
        InitializeWeekSnapshotButton()
        'StoreOriginalBounds(Me)
    End Sub
    Public Sub New(repo As AppointmentCRepository)
        _repo = repo
        _AppointmentC = New BindingList(Of AppointmentC)()
        _suppressComboRenderDuringInit = True
        Try
            InitializeComponent()
        Finally
            _suppressComboRenderDuringInit = False
        End Try
        ApplyPerformanceRenderingOptions()
        'If lblRange IsNot Nothing Then lblRange.Appearance.TextOptions.Trimming = DevExpress.Utils.TextTrimming.None
        ' default date range
        FilterStartDate = DateTime.Today.AddMonths(-1)
        FilterEndDate = DateTime.Today.AddMonths(1)
        ' in constructor / Load
        _dragTimer = New System.Windows.Forms.Timer() With {.Interval = _dragHoldMs}
        AddHandler _dragTimer.Tick, AddressOf DragTimer_Tick
        _resizeTimer = New System.Windows.Forms.Timer() With {.Interval = 50}
        AddHandler _resizeTimer.Tick, AddressOf ResizeTimer_Tick
        AddHandler Me.Resize, AddressOf OnScheduleResize
        AddHandler Me.Disposed, Sub()
                                    If _appointmentFont IsNot Nothing Then
                                        _appointmentFont.Dispose()
                                        _appointmentFont = Nothing
                                    End If
                                    If _schedulerToolTip IsNot Nothing Then
                                        _schedulerToolTip.Dispose()
                                        _schedulerToolTip = Nothing
                                    End If
                                    If _weekSnapshotToolTip IsNot Nothing Then
                                        _weekSnapshotToolTip.Dispose()
                                        _weekSnapshotToolTip = Nothing
                                    End If
                                    If _statusContextMenu IsNot Nothing Then
                                        _statusContextMenu.Dispose()
                                        _statusContextMenu = Nothing
                                    End If
                                    If _statusContextMenuFont IsNot Nothing Then
                                        _statusContextMenuFont.Dispose()
                                        _statusContextMenuFont = Nothing
                                    End If
                                End Sub
        If btnPrevView IsNot Nothing Then btnPrevView.Visible = False
        If btnNextView IsNot Nothing Then btnNextView.Visible = False
        EnsureHeaderToolbarButtonHeightHook()
        EnsureHeaderToolbarCenteringHooks()
        EnsureFiltersTableButtonHeightHook()
        InitializeWorkingHoursPickersFromSettings()
        SyncDefaultWeekViewFromCombo()
        EnsureSchedulerBodyWorkspace()
        EnsureBodyWorkspaceExpandButton()
        InitializeWeekSnapshotButton()
        StoreOriginalBounds(Me)
    End Sub
    Private Sub ScheduleAdmin_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' DateEdit often drops ctor-time EditValue until the control is loaded; re-bind from My.Settings before first render.
        InitializeWorkingHoursPickersFromSettings()
        ApplyLocalizedSchedulerFilterRadioCaptions()
        ReleaseSchedulerTransientUiHandles()
        If _view <> ViewMode.ThisWeekFull OrElse cmbView.SelectedIndex <> ComboIndexFromViewMode(ViewMode.ThisWeekFull) Then
            SetView(ViewMode.ThisWeekFull)
        Else
            ' Combo already matches week view — SetView is skipped, so ensure first LoadAndRender after layout is valid.
            LoadAndRender()
        End If
        LayoutBodyWorkspaceExpandButton()
    End Sub
    Public WriteOnly Property ValueFromParent() As Integer
        Set(ByVal Value As Integer)
            ' Default patient filter remains "all"; refresh schedule when parent signals patient context.
            LoadAndRender()
        End Set
    End Property

    Private Function SetApptsCount() As String
        Dim count As String = ""
        count = _AppointmentC.Count.ToString
        Return count
    End Function

    ''' <summary>Count of appointments in the current view's date range (day/week/month/timeline). Matches what is actually displayed.</summary>
    Private Function GetDisplayedAppointmentCount() As Integer
        If _AppointmentC Is Nothing Then Return 0
        Select Case _view
            'Case ViewMode.DayView
            '    Return GetDayViewAppointmentsForDate(_currentDate.Date).Count
            'Case ViewMode.DoctorsDay
            '    Return GetDayViewAppointmentsForDate(_currentDate.Date).Count
            Case ViewMode.ThisWeekFull
                Dim dow7 As Integer = CInt(_currentDate.DayOfWeek)
                Dim toSat7 As Integer = (dow7 - 6 + 7) Mod 7
                Dim ws7 As DateTime = _currentDate.Date.AddDays(-toSat7)
                Dim we7 As DateTime = ws7.AddDays(6)
                Return _AppointmentC.Where(Function(a) a.StartDateTime.Date >= ws7 AndAlso a.StartDateTime.Date <= we7).Count()
                'Case ViewMode.ThisWeek
                '    Dim dow6 As Integer = CInt(_currentDate.DayOfWeek)
                '    Dim toSat6 As Integer = (dow6 - 6 + 7) Mod 7
                '    Dim ws6 As DateTime = _currentDate.Date.AddDays(-toSat6)
                '    Dim we6 As DateTime = ws6.AddDays(5)
                '    Return _AppointmentC.Where(Function(a) a.StartDateTime.Date >= ws6 AndAlso a.StartDateTime.Date <= we6).Count()
                'Case ViewMode.MonthlyWeek
                '    Dim sow As DateTime = _currentDate.Date.AddDays(-CInt(_currentDate.DayOfWeek))
                '    Dim eow As DateTime = sow.AddDays(6)
                '    Return _AppointmentC.Where(Function(a) a.StartDateTime.Date >= sow AndAlso a.StartDateTime.Date <= eow).Count()
                'Case ViewMode.MonthView
                '    Dim ms As DateTime = New DateTime(_currentDate.Year, _currentDate.Month, 1)
                '    Dim me_ As DateTime = ms.AddMonths(1).AddDays(-1)
                '    Return _AppointmentC.Where(Function(a) a.StartDateTime.Date >= ms AndAlso a.StartDateTime.Date <= me_).Count()
                'Case ViewMode.DaysTimeline
                '    Dim tldow As Integer = CInt(_currentDate.DayOfWeek)
                '    Dim tlsat As Integer = (tldow - 6 + 7) Mod 7
                '    Dim tls As DateTime = _currentDate.Date.AddDays(-tlsat)
                '    Dim tle As DateTime = tls.AddDays(6)
                '    Return _AppointmentC.Where(Function(a) a.StartDateTime.Date >= tls AndAlso a.StartDateTime.Date <= tle).Count()
            Case Else
                Return _AppointmentC.Count
        End Select
    End Function

    ''' <summary>Updates all lblCount labels to the count of appointments displayed in the current view.</summary>
    Private Sub UpdateLblCountDisplay()
        Dim count As Integer = GetDisplayedAppointmentCount()
        If lblCount IsNot Nothing Then lblCount.Text = count.ToString()
    End Sub
#End Region



    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 5b — RESIZE HANDLING                                        ║
    ' ║  Proportional resize of controls when the user control is resized.   ║
    ' ╚══════════════════════════════════════════════════════════════════════╝




#Region "═══ 5b. RESIZE HANDLING ═══"
    ''' <summary>DevExpress TablePanels must own child bounds; proportional SetBounds on their children breaks layout (shrunk buttons/labels).</summary>
    Private Function IsManagedTablePanel(ctrl As Control) As Boolean
        Return ctrl IsNot Nothing AndAlso (ctrl Is headerTable OrElse ctrl Is filtersTable)
    End Function

    ''' <summary>legendPanel uses docked chrome (top toolbar + fill filter strip). Forcing proportional SetBounds breaks Dock layout and can hide grpFilters.</summary>
    Private Function IsLegendDockedChrome(ctrl As Control) As Boolean
        Return ctrl IsNot Nothing AndAlso (ctrl Is grpFilters OrElse ctrl Is pnlButtons)
    End Function

    Private Sub StoreOriginalBounds(container As Control)
        Dim sw As New Stopwatch
        sw.Start()
        For Each ctrl As Control In container.Controls
            If IsManagedTablePanel(ctrl) OrElse IsLegendDockedChrome(ctrl) Then Continue For
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If Not ctrl.HasChildren Then Continue For
            StoreOriginalBounds(ctrl)
        Next
        sw.Stop()
    End Sub

    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return
        Dim sw As New Stopwatch
        sw.Start()
        Dim widthRatio = CSng(Me.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.Height) / OriginalPanelHeight
        For Each kvp In controlBoundsCache
            If IsManagedTablePanel(kvp.Key) OrElse IsLegendDockedChrome(kvp.Key) Then Continue For
            kvp.Key.SetBounds(
                CInt(kvp.Value.X * widthRatio),
                CInt(kvp.Value.Y * heightRatio),
                CInt(kvp.Value.Width * widthRatio),
                CInt(kvp.Value.Height * heightRatio))
        Next
        If headerTable IsNot Nothing AndAlso Not headerTable.IsDisposed Then headerTable.PerformLayout()
        If filtersTable IsNot Nothing AndAlso Not filtersTable.IsDisposed Then filtersTable.PerformLayout()

        sw.Stop()
    End Sub

    Private Sub ScheduleAdmin_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If controlBoundsCache.IsEmpty Then Return
        ResizeControlsProportionally()

    End Sub

    ' DevExpress TablePanel often lays out SimpleButtons shorter than DateEdit/ComboBox in the same row.
    ' Skin indents + AutoSize + Dock fight the cell; we sync min height and bounds after layout.
    Private _headerToolbarButtonHeightHooked As Boolean
    Private _headerToolbarHeightInvokePending As Boolean
    Private _inHeaderToolbarCentering As Boolean
    Private Sub EnsureHeaderToolbarButtonHeightHook()
        If _headerToolbarButtonHeightHooked OrElse headerTable Is Nothing Then Return
        _headerToolbarButtonHeightHooked = True
        AddHandler headerTable.Layout, AddressOf HeaderTable_LayoutSyncButtonHeights
    End Sub

    Private Sub HeaderTable_LayoutSyncButtonHeights(sender As Object, e As LayoutEventArgs)
        Dim host = TryCast(sender, Control)
        If host Is Nothing OrElse host.IsDisposed OrElse Not host.IsHandleCreated Then Return
        If _headerToolbarHeightInvokePending Then Return
        _headerToolbarHeightInvokePending = True
        Try
            host.BeginInvoke(New MethodInvoker(Sub()
                                                   _headerToolbarHeightInvokePending = False
                                                   Try
                                                       ApplyHeaderToolbarButtonHeights()
                                                   Catch
                                                       ' best-effort layout; avoid breaking navigation / tab switch
                                                   End Try
                                               End Sub))
        Catch
            _headerToolbarHeightInvokePending = False
        End Try
    End Sub

    Private Sub ApplyHeaderToolbarButtonHeights()
        If headerTable Is Nothing OrElse headerTable.IsDisposed OrElse Not headerTable.IsHandleCreated Then Return
        If cmbView Is Nothing OrElse cmbView.IsDisposed OrElse gotoDate Is Nothing OrElse gotoDate.IsDisposed Then Return
        RemoveHandler headerTable.Layout, AddressOf HeaderTable_LayoutSyncButtonHeights
        Try
            Dim refH = Math.Max(cmbView.Height, gotoDate.Height)
            If chkUse24 IsNot Nothing AndAlso Not chkUse24.IsDisposed Then refH = Math.Max(refH, chkUse24.Height)
            refH = Math.Max(refH, 24)

            For Each sb In New SimpleButton() {btnAdd, btnPrev, btnNext, btnToday, btnWeekSnapshot}
                If sb Is Nothing OrElse sb.IsDisposed Then Continue For
                sb.AutoSize = False
                sb.MinimumSize = New Size(2, refH)
                Dim r = sb.Bounds
                If r.Width <= 0 Then Continue For
                Dim y = r.Y + Math.Max(0, (r.Height - refH) \ 2)
                sb.SuspendLayout()
                sb.Dock = DockStyle.None
                sb.Anchor = AnchorStyles.Top Or AnchorStyles.Left
                If sb.Height <> refH OrElse sb.Top <> y Then
                    sb.SetBounds(r.X, y, r.Width, refH)
                End If
                sb.ResumeLayout(False)
            Next
            ApplyHeaderToolbarCentering()
        Finally
            Try
                If headerTable IsNot Nothing AndAlso Not headerTable.IsDisposed Then
                    AddHandler headerTable.Layout, AddressOf HeaderTable_LayoutSyncButtonHeights
                End If
            Catch
            End Try
        End Try
    End Sub

    Private _headerToolbarCenteringHooks As Boolean
    Private Sub EnsureHeaderToolbarCenteringHooks()
        If _headerToolbarCenteringHooks Then Return
        If pnlHeaderLeft Is Nothing OrElse pnlHeaderCenter Is Nothing OrElse pnlHeaderRight Is Nothing Then Return
        _headerToolbarCenteringHooks = True
        AddHandler pnlHeaderLeft.SizeChanged, AddressOf HeaderChromeZone_SizeChanged
        AddHandler pnlHeaderCenter.SizeChanged, AddressOf HeaderChromeZone_SizeChanged
        AddHandler pnlHeaderRight.SizeChanged, AddressOf HeaderChromeZone_SizeChanged
    End Sub

    Private Sub HeaderChromeZone_SizeChanged(sender As Object, e As EventArgs)
        ApplyHeaderToolbarCentering()
    End Sub

    ''' <summary>Patient (left) and range (right) use the outer thirds; toolbar cluster stays horizontally centered in the middle third.</summary>
    Private Sub ApplyHeaderToolbarCentering()
        If _inHeaderToolbarCentering Then Return
        If pnlHeaderLeft Is Nothing OrElse pnlHeaderCenter Is Nothing OrElse pnlHeaderRight Is Nothing Then Return
        If pnlHeaderToolbarFlow Is Nothing OrElse lblPatient Is Nothing OrElse lblRange Is Nothing Then Return
        If pnlHeaderLeft.IsDisposed OrElse pnlHeaderCenter.IsDisposed OrElse pnlHeaderRight.IsDisposed Then Return
        _inHeaderToolbarCentering = True
        Try
            Dim lh = Math.Max(1, pnlHeaderLeft.ClientSize.Height)
            Dim ch = Math.Max(1, pnlHeaderCenter.ClientSize.Height)
            Dim rh = Math.Max(1, pnlHeaderRight.ClientSize.Height)

            Dim padHL = pnlHeaderLeft.Padding.Left + pnlHeaderLeft.Padding.Right
            Dim lw = Math.Max(40, pnlHeaderLeft.ClientSize.Width - padHL)
            lblPatient.SuspendLayout()
            lblPatient.Dock = DockStyle.None
            lblPatient.Width = lw
            lblPatient.Height = lh
            lblPatient.Location = New Point(pnlHeaderLeft.Padding.Left, 0)
            lblPatient.ResumeLayout(False)

            Dim padHR = pnlHeaderRight.Padding.Left + pnlHeaderRight.Padding.Right
            Dim rw = Math.Max(40, pnlHeaderRight.ClientSize.Width - padHR)
            lblRange.SuspendLayout()
            lblRange.Dock = DockStyle.None
            lblRange.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            lblRange.Width = rw
            lblRange.Height = rh
            lblRange.Location = New Point(pnlHeaderRight.Padding.Left, 0)
            lblRange.ResumeLayout(False)

            pnlHeaderToolbarFlow.PerformLayout()
            Dim fx = Math.Max(0, (pnlHeaderCenter.ClientSize.Width - pnlHeaderToolbarFlow.Width) \ 2)
            Dim fy = Math.Max(0, (pnlHeaderCenter.ClientSize.Height - pnlHeaderToolbarFlow.Height) \ 2)
            pnlHeaderToolbarFlow.Location = New Point(fx, fy)
        Finally
            _inHeaderToolbarCentering = False
        End Try
    End Sub

    Private _filtersTableButtonHeightHooked As Boolean
    Private _filtersTableHeightInvokePending As Boolean

    Private Sub EnsureFiltersTableButtonHeightHook()
        If _filtersTableButtonHeightHooked OrElse filtersTable Is Nothing Then Return
        _filtersTableButtonHeightHooked = True
        AddHandler filtersTable.Layout, AddressOf FiltersTable_LayoutSyncButtonHeights
    End Sub

    Private Sub FiltersTable_LayoutSyncButtonHeights(sender As Object, e As LayoutEventArgs)
        Dim host = TryCast(sender, Control)
        If host Is Nothing OrElse host.IsDisposed OrElse Not host.IsHandleCreated Then Return
        If _filtersTableHeightInvokePending Then Return
        _filtersTableHeightInvokePending = True
        Try
            host.BeginInvoke(New MethodInvoker(Sub()
                                                   _filtersTableHeightInvokePending = False
                                                   Try
                                                       ApplyFiltersTableButtonHeights()
                                                   Catch
                                                       ' best-effort layout; avoid breaking navigation / tab switch
                                                   End Try
                                               End Sub))
        Catch
            _filtersTableHeightInvokePending = False
        End Try
    End Sub

    Private Sub ApplyFiltersTableButtonHeights()
        If filtersTable Is Nothing OrElse filtersTable.IsDisposed OrElse Not filtersTable.IsHandleCreated Then Return
        If includeReasonCheck Is Nothing OrElse includeReasonCheck.IsDisposed Then Return
        RemoveHandler filtersTable.Layout, AddressOf FiltersTable_LayoutSyncButtonHeights
        Try
            Dim refH = includeReasonCheck.Height
            If boldFontCheck IsNot Nothing AndAlso Not boldFontCheck.IsDisposed Then refH = Math.Max(refH, boldFontCheck.Height)
            If sizeFontCheck IsNot Nothing AndAlso Not sizeFontCheck.IsDisposed Then refH = Math.Max(refH, sizeFontCheck.Height)
            If lblDoc IsNot Nothing AndAlso Not lblDoc.IsDisposed Then refH = Math.Max(refH, lblDoc.Height)
            refH = Math.Max(refH, 24)

            For Each sb In New SimpleButton() {btnPrevView, btnNextView}
                If sb Is Nothing OrElse sb.IsDisposed Then Continue For
                sb.AutoSize = False
                sb.MinimumSize = New Size(2, refH)
                Dim r = sb.Bounds
                If r.Width <= 0 Then Continue For
                Dim targetH = Math.Max(refH, r.Height)
                Dim y = r.Y + Math.Max(0, (r.Height - targetH) \ 2)
                sb.SuspendLayout()
                sb.Dock = DockStyle.None
                sb.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
                If sb.Height <> targetH OrElse sb.Top <> y Then
                    sb.SetBounds(r.X, y, r.Width, targetH)
                End If
                sb.ResumeLayout(False)
            Next
        Finally
            Try
                If filtersTable IsNot Nothing AndAlso Not filtersTable.IsDisposed Then
                    AddHandler filtersTable.Layout, AddressOf FiltersTable_LayoutSyncButtonHeights
                End If
            Catch
            End Try
        End Try
    End Sub
#End Region




    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 6 — DOCTOR LEGEND & UI SETUP                                ║
    ' ╚══════════════════════════════════════════════════════════════════════╝




#Region "═══ 6. DOCTOR LEGEND & UI SETUP ═══"

#Region "cboDocs ComboBox"
    Private Sub UpdateUIBasedOnDoctorSelection()
        ' Example: Enable/disable controls based on doctor selection
        If SelectedDoctorID.HasValue Then
            LoadAndRender()
            ' Doctor is selected - enable doctor-specific features
            'BtnCreateAppointment.Enabled = True
            'BtnViewSchedule.Enabled = True
        Else
            ' No doctor selected - disable doctor-specific features
            'BtnCreateAppointment.Enabled = False
            'BtnViewSchedule.Enabled = False
        End If
    End Sub
    Private Sub cboDocsSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim cboDocs As DevExpress.XtraEditors.ComboBoxEdit = TryCast(sender, DevExpress.XtraEditors.ComboBoxEdit)
        If cboDocs IsNot Nothing Then
            Dim selectedIndex As Integer = cboDocs.SelectedIndex

            If selectedIndex = 0 Then
                ' "(No Doctor Association)" selected
                Console.WriteLine("No doctor association selected")
                ' You can set a variable or property to indicate no doctor selected
                SelectedDoctorID = Nothing
                SelectedDoctorName = "(No Doctor Association)"
            ElseIf selectedIndex > 0 Then
                ' Doctor selected - get the actual index in doctors list (subtract 1 for the "No Doctor" item)

                SelectedDoctorID = doctorQrs.GetDoctorIdByName(CStr(cboDocs.SelectedItem))
                SelectedDoctorName = CStr(cboDocs.SelectedItem)
                Console.WriteLine($"Selected Doctor: {SelectedDoctorName} (ID: {SelectedDoctorID})")
                FilterDoctorId = SelectedDoctorID

                ' You can also do additional actions here like:
                ' - Update a label with doctor details
                ' - Enable/disable other controls
                ' - Load doctor-specific data

            Else
                ' No selection (-1)
                SelectedDoctorID = Nothing
                SelectedDoctorName = String.Empty
            End If
            Console.WriteLine($"cboDocs Width is  {cboDocs.Width}")
            ' Optional: Update UI based on selection
            UpdateUIBasedOnDoctorSelection()
        End If
    End Sub

#End Region

#Region "Doctor Legend"
    Private Sub WireLegendOptionCheckHandlersOnce()
        If _legendOptionHandlersWired Then Return
        AddHandler includeReasonCheck.CheckedChanged, Sub(s, e) LoadAndRender()
        AddHandler boldFontCheck.CheckedChanged, Sub(s, e)
                                                     GetAppointmentFont()
                                                     Render()
                                                 End Sub
        AddHandler sizeFontCheck.CheckedChanged, Sub(s, e)
                                                     GetAppointmentFont()
                                                     Render()
                                                 End Sub
        _legendOptionHandlersWired = True
    End Sub

    Private Sub EnsureDoctorComboLegend(doctors As List(Of (DrID As Integer, DrName As String, DrColor As String)))
        If _cboDocsLegend Is Nothing OrElse _cboDocsLegend.IsDisposed Then
            _cboDocsLegend = New ComboBoxEdit With {.Width = 220}
            _cboDocsLegend.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
            AddHandler _cboDocsLegend.SelectedIndexChanged, AddressOf cboDocsSelectedIndexChanged
            legendPanel.Controls.Add(_cboDocsLegend)
        End If
        _cboDocsLegend.Visible = True
        _cboDocsLegend.Properties.Items.Clear()
        _cboDocsLegend.Properties.Items.Add("(No Doctor Association)")
        For Each doc In doctors
            _cboDocsLegend.Properties.Items.Add(doc.DrName)
        Next
        If CurrentDoctor IsNot Nothing Then
            _cboDocsLegend.SelectedItem = CurrentDoctor.DrName
        Else
            _cboDocsLegend.SelectedIndex = 0
        End If
    End Sub

    Private Const LegendPadPx As Integer = 8

    ''' <param name="apptsForDoctorStrip">Legacy: same filtered appointment list as the grid (without doctor filter). Doctor legend buttons use <c>dbo.Doctors</c> (max 8), not distinct doctors from this list.</param>
    Private Sub LoadDoctorLegend(apptsForDoctorStrip As IList(Of AppointmentC))
        ' Match localized designer: Arabic resx sets $this.RightToLeft = Yes; forcing No here
        ' made grpFilters/filtersTable stay LTR at runtime. Mirror only for English flow layout.
        legendPanel.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        legendPanel.AutoScroll = True
        pnlHeader.AutoScroll = True
        WireLegendOptionCheckHandlersOnce()
        ApplyLocalizedSchedulerFilterRadioCaptions()

        grpFilters.Text = If(Eng, "Filter Tools", "أدوات التصفية")
        legendPanel.Text = If(Eng, "Filters", "تصفية")

        Using conn As New SqlClient.SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim allDoctors = conn.Query(Of (DrID As Integer, DrName As String, DrColor As String))(
                "SELECT DrID, [DrName], [DrColor] FROM [dbo].[Doctors] ORDER BY DrName").ToList()

            If FilterDoctorId.HasValue AndAlso Not allDoctors.Any(Function(d) d.DrID = FilterDoctorId.Value) Then
                FilterDoctorId = Nothing
            End If

            If ShowPatientLbls Then
                grpFilters.Visible = False
                EnsureDoctorComboLegend(allDoctors)
                _cboDocsLegend.Location = New Point(If(Eng, LegendPadPx, Math.Max(LegendPadPx, legendPanel.ClientSize.Width - _cboDocsLegend.Width - LegendPadPx)), pnlButtons.Bottom + LegendPadPx)
                UpdateLblCountDisplay()
                UpdateViewNavButtonVisibility()
                For Each ctrl As Control In legendPanel.Controls
                    controlBoundsCache.TryRemove(ctrl, Nothing)
                Next
                StoreOriginalBounds(legendPanel)
                Return
            End If

            grpFilters.Visible = True
            If _cboDocsLegend IsNot Nothing Then _cboDocsLegend.Visible = False

            ' Doctor filter buttons match dbo.Doctors (same basis as Doctors Day); overflow uses horizontal scroll in pnlDoctorFilterScroll.
            Dim doctors As List(Of (DrID As Integer, DrName As String, DrColor As String)) = allDoctors.ToList()

            lblPatients.Text = If(Eng, "Patients", "المرضى")
            lblDoc.Text = If(Eng, "Doctors", "الاطباء")
            lblOptions.Text = If(Eng, "Options", "خيارات")
            lblStatusFilter.Text = If(Eng, "Status", "الحالة")

            _doctorSlotDrIds.Clear()
            _doctorSlotColors.Clear()
            For Each d In doctors
                _doctorSlotDrIds.Add(d.DrID)
                _doctorSlotColors.Add(SchedulerParseDoctorColor(d.DrColor))
            Next

            If btnFilterDoctor0 IsNot Nothing Then
                btnFilterDoctor0.Text = If(Eng, "All Doctors", "كل الاطباء")
            End If
            If flpDoctorFilterButtons IsNot Nothing Then
                flpDoctorFilterButtons.RightToLeft = legendPanel.RightToLeft
                flpDoctorFilterButtons.FlowDirection = If(legendPanel.RightToLeft = RightToLeft.Yes, FlowDirection.RightToLeft, FlowDirection.LeftToRight)
            End If
            WireDoctorFilterSlotHandlersOnce()
            EnsureExtraDoctorFilterButtons(doctors.Count)
            _suppressRadioFilterSync = True
            Try
                For ri = 1 To doctors.Count
                    Dim b = DoctorFilterButton(ri)
                    If b Is Nothing Then Continue For
                    Dim dr = doctors(ri - 1)
                    b.Text = dr.DrName
                    b.Enabled = True
                    b.Visible = True
                Next
                For ri = doctors.Count + 1 To 8
                    Dim b = DoctorFilterButton(ri)
                    If b Is Nothing Then Continue For
                    b.Text = String.Empty
                    b.Enabled = False
                    b.Visible = False
                Next
            Finally
                _suppressRadioFilterSync = False
            End Try
            RefreshDoctorFilterButtonStyles()
            AdjustDoctorFilterScrollExtent()

            lblApptsCount.Text = If(Eng, "Appts Count", "عدد المواعيد")
            ApplyLegendChipColors(lblApptsCount, Color.HotPink, Color.White)
            ApplyLegendChipColors(lblCount, Color.HotPink, Color.White)

            includeReasonCheck.Properties.Caption = If(Eng, "Include Notes", "تضمين السبب")
            boldFontCheck.Properties.Caption = If(Eng, "Bold", "غامق")
            sizeFontCheck.Properties.Caption = If(Eng, "Size 10", "حجم 10")
        End Using

        If btnPrevView IsNot Nothing Then
            btnPrevView.Text = If(Eng, "Previous", "السابق")
            btnPrevView.ToolTip = If(Eng, "Previous ", "العرض السابق")
        End If
        If btnNextView IsNot Nothing Then
            btnNextView.Text = If(Eng, "Next", "التالي")
            btnNextView.ToolTip = If(Eng, "Next View ", "العرض التالي")
        End If

        UpdateLblCountDisplay()
        SyncFilterRadiosFromState()
        UpdateViewNavButtonVisibility()

        For Each ctrl As Control In legendPanel.Controls
            controlBoundsCache.TryRemove(ctrl, Nothing)
        Next
        StoreOriginalBounds(legendPanel)
    End Sub

    Private Sub ApplyLegendChipColors(lc As LabelControl, back As Color, fore As Color)
        lc.Appearance.BackColor = back
        lc.Appearance.ForeColor = fore
        lc.Appearance.Options.UseBackColor = True
        lc.Appearance.Options.UseForeColor = True
    End Sub

    Private Shared Function SchedulerParseDoctorColor(drColor As String) As Color
        Try
            If Not String.IsNullOrWhiteSpace(drColor) Then
                Dim hex = drColor.Trim()
                If Not hex.StartsWith("#", StringComparison.Ordinal) Then hex = "#" & hex
                Return ColorTranslator.FromHtml(hex)
            End If
        Catch
        End Try
        Return Color.LightSteelBlue
    End Function

    Private Shared Function SchedulerFilterContrastFore(back As Color) As Color
        Dim lum = (0.299F * back.R + 0.587F * back.G + 0.114F * back.B) / 255.0F
        Return If(lum < 0.45F, Color.White, Color.Black)
    End Function

    Private Function FilterChipBaseFont() As Font
        If lblPatients IsNot Nothing AndAlso lblPatients.Appearance.Options.UseFont AndAlso lblPatients.Appearance.Font IsNot Nothing Then
            Return lblPatients.Appearance.Font
        End If
        Return SystemFonts.DefaultFont
    End Function

    Private Sub StyleSchedulerFilterChip(btn As SimpleButton, selected As Boolean, enabled As Boolean, idleBack As Color)
        If btn Is Nothing Then Return
        Dim baseFont = FilterChipBaseFont()
        If Not enabled Then
            btn.Appearance.BackColor = Color.FromArgb(245, 245, 245)
            btn.Appearance.ForeColor = Color.DimGray
            btn.Appearance.Options.UseBackColor = True
            btn.Appearance.Options.UseForeColor = True
            btn.Appearance.Font = New Font(baseFont, FontStyle.Regular)
            btn.Appearance.Options.UseFont = True
            Return
        End If
        If selected Then
            btn.Appearance.BackColor = Color.Yellow
            btn.Appearance.ForeColor = Color.Black
            btn.Appearance.Options.UseBackColor = True
            btn.Appearance.Options.UseForeColor = True
            btn.Appearance.Font = New Font(baseFont, FontStyle.Bold)
            btn.Appearance.Options.UseFont = True
        Else
            btn.Appearance.BackColor = idleBack
            btn.Appearance.ForeColor = SchedulerFilterContrastFore(idleBack)
            btn.Appearance.Options.UseBackColor = True
            btn.Appearance.Options.UseForeColor = True
            btn.Appearance.Font = New Font(baseFont, FontStyle.Regular)
            btn.Appearance.Options.UseFont = True
        End If
    End Sub

    Private Function DoctorFilterButton(slot As Integer) As SimpleButton
        Select Case slot
            Case 0
                Return btnFilterDoctor0
            Case 1
                Return btnFilterDoctor1
            Case 2
                Return btnFilterDoctor2
            Case 3
                Return btnFilterDoctor3
            Case 4
                Return btnFilterDoctor4
            Case 5
                Return btnFilterDoctor5
            Case 6
                Return btnFilterDoctor6
            Case 7
                Return btnFilterDoctor7
            Case 8
                Return btnFilterDoctor8
            Case Else
                Dim ix = slot - 9
                If ix >= 0 AndAlso ix < _extraDoctorFilterButtons.Count Then Return _extraDoctorFilterButtons(ix)
                Return Nothing
        End Select
    End Function

    Private Sub WireDoctorFilterSlotHandlersOnce()
        If _doctorSlotClickHandlersWired OrElse flpDoctorFilterButtons Is Nothing Then Return
        _doctorSlotClickHandlersWired = True
        For i = 1 To 8
            Dim b = DoctorFilterButton(i)
            If b Is Nothing Then Continue For
            AddHandler b.Click, AddressOf FilterDoctorSlotButton_Click
        Next
        If pnlDoctorFilterScroll IsNot Nothing Then
            AddHandler pnlDoctorFilterScroll.Resize, AddressOf DoctorFilterScrollPanel_Resize
        End If
    End Sub

    Private Sub DoctorFilterScrollPanel_Resize(sender As Object, e As EventArgs)
        PositionDoctorFilterSlimScroll()
        AdjustDoctorFilterScrollExtent()
    End Sub

    ''' <summary>Grow the doctor filter row (middle) by shifting relative weight from the rows above and below so a horizontal scrollbar does not cover most of the chips.</summary>
    Private Sub ApplyFiltersTableDoctorRowBoost(needsHorizontalScroll As Boolean)
        If filtersTable Is Nothing OrElse filtersTable.Rows.Count < 3 Then Return
        Dim take = FiltersDoctorRowBoostTakeFromNeighbor
        If needsHorizontalScroll Then
            If Not _filtersDoctorRowBoostActive Then
                filtersTable.Rows(0).Height = Math.Max(8.0F, _filtersBaseR0 - take)
                filtersTable.Rows(1).Height = _filtersBaseR1 + 2.0F * take
                filtersTable.Rows(2).Height = Math.Max(8.0F, _filtersBaseR2 - take)
                _filtersDoctorRowBoostActive = True
            End If
        Else
            If _filtersDoctorRowBoostActive Then
                filtersTable.Rows(0).Height = _filtersBaseR0
                filtersTable.Rows(1).Height = _filtersBaseR1
                filtersTable.Rows(2).Height = _filtersBaseR2
                _filtersDoctorRowBoostActive = False
            End If
        End If
    End Sub

    Private Sub EnsureDoctorFilterCompactionBaseCaptured()
        If _doctorFilterCompactionBaseCaptured Then Return
        If pnlDoctorFilterScroll Is Nothing OrElse flpDoctorFilterButtons Is Nothing Then Return
        _doctorFilterBasePanelPadding = pnlDoctorFilterScroll.Padding
        _doctorFilterBaseFlowPadding = flpDoctorFilterButtons.Padding
        For Each c As Control In flpDoctorFilterButtons.Controls
            Dim sb = TryCast(c, SimpleButton)
            If sb Is Nothing Then Continue For
            If Not _doctorFilterButtonBaseMargins.ContainsKey(sb.Name) Then
                _doctorFilterButtonBaseMargins(sb.Name) = sb.Margin
            End If
        Next
        _doctorFilterCompactionBaseCaptured = True
    End Sub

    Private Sub ApplyDoctorFilterVerticalCompaction(compact As Boolean)
        If pnlDoctorFilterScroll Is Nothing OrElse flpDoctorFilterButtons Is Nothing Then Return
        EnsureDoctorFilterCompactionBaseCaptured()
        If Not _doctorFilterCompactionBaseCaptured Then Return

        If compact Then
            pnlDoctorFilterScroll.Padding = New Padding(_doctorFilterBasePanelPadding.Left, 0, _doctorFilterBasePanelPadding.Right, 0)
            flpDoctorFilterButtons.Padding = New Padding(_doctorFilterBaseFlowPadding.Left, 0, _doctorFilterBaseFlowPadding.Right, 0)
        Else
            pnlDoctorFilterScroll.Padding = _doctorFilterBasePanelPadding
            flpDoctorFilterButtons.Padding = _doctorFilterBaseFlowPadding
        End If

        For Each c As Control In flpDoctorFilterButtons.Controls
            Dim sb = TryCast(c, SimpleButton)
            If sb Is Nothing Then Continue For
            If Not _doctorFilterButtonBaseMargins.ContainsKey(sb.Name) Then
                _doctorFilterButtonBaseMargins(sb.Name) = sb.Margin
            End If
            Dim baseMargin = _doctorFilterButtonBaseMargins(sb.Name)
            sb.Margin = If(compact,
                           New Padding(baseMargin.Left, 0, baseMargin.Right, 0),
                           baseMargin)
        Next
    End Sub

    Private Sub EnsureDoctorFilterSlimScroll()
        If pnlDoctorFilterScroll Is Nothing Then Return
        pnlDoctorFilterScroll.AutoScroll = False
        If _doctorFilterSlimScroll Is Nothing OrElse _doctorFilterSlimScroll.IsDisposed Then
            _doctorFilterSlimScroll = New DevExpress.XtraEditors.HScrollBar With {
                .Name = "doctorFilterSlimScroll",
                .Height = DoctorFilterSlimScrollHeight,
                .Visible = False,
                .SmallChange = 24,
                .LargeChange = 120,
                .Minimum = 0,
                .Value = 0
            }
            pnlDoctorFilterScroll.Controls.Add(_doctorFilterSlimScroll)
            _doctorFilterSlimScroll.BringToFront()
        End If
        If Not _doctorFilterSlimScrollWired Then
            AddHandler _doctorFilterSlimScroll.Scroll, AddressOf DoctorFilterSlimScroll_Scroll
            _doctorFilterSlimScrollWired = True
        End If
        PositionDoctorFilterSlimScroll()
    End Sub

    Private Sub PositionDoctorFilterSlimScroll()
        If pnlDoctorFilterScroll Is Nothing OrElse _doctorFilterSlimScroll Is Nothing Then Return
        Dim cw = Math.Max(1, pnlDoctorFilterScroll.ClientSize.Width)
        Dim ch = Math.Max(1, pnlDoctorFilterScroll.ClientSize.Height)
        Dim h = Math.Min(DoctorFilterSlimScrollHeight, ch)
        _doctorFilterSlimScroll.SetBounds(0, Math.Max(0, ch - h), cw, h)
    End Sub

    Private Sub DoctorFilterSlimScroll_Scroll(sender As Object, e As ScrollEventArgs)
        If flpDoctorFilterButtons Is Nothing OrElse _doctorFilterSlimScroll Is Nothing Then Return
        flpDoctorFilterButtons.Left = -_doctorFilterSlimScroll.Value
    End Sub

    Private Sub AdjustDoctorFilterScrollExtent()
        If pnlDoctorFilterScroll Is Nothing OrElse flpDoctorFilterButtons Is Nothing Then Return
        EnsureDoctorFilterSlimScroll()
        flpDoctorFilterButtons.PerformLayout()
        Dim prefW = flpDoctorFilterButtons.PreferredSize.Width
        Dim cw = Math.Max(1, pnlDoctorFilterScroll.ClientSize.Width)
        Dim needH = prefW > cw

        ApplyDoctorFilterVerticalCompaction(needH)
        flpDoctorFilterButtons.PerformLayout()
        prefW = flpDoctorFilterButtons.PreferredSize.Width
        cw = Math.Max(1, pnlDoctorFilterScroll.ClientSize.Width)
        needH = prefW > cw
        ApplyDoctorFilterVerticalCompaction(needH)

        ApplyFiltersTableDoctorRowBoost(needH)

        If needH AndAlso filtersTable IsNot Nothing Then
            filtersTable.PerformLayout()
            If grpFilters IsNot Nothing Then grpFilters.PerformLayout()
            pnlDoctorFilterScroll.PerformLayout()
        End If

        cw = Math.Max(1, pnlDoctorFilterScroll.ClientSize.Width)
        PositionDoctorFilterSlimScroll()
        Dim swVisible = needH AndAlso _doctorFilterSlimScroll IsNot Nothing
        Dim maxOffset = Math.Max(0, prefW - cw)
        If _doctorFilterSlimScroll IsNot Nothing Then
            If swVisible Then
                _doctorFilterSlimScroll.Visible = True
                _doctorFilterSlimScroll.LargeChange = Math.Max(1, cw)
                _doctorFilterSlimScroll.SmallChange = Math.Max(1, Math.Min(64, cw \ 8))
                _doctorFilterSlimScroll.Minimum = 0
                _doctorFilterSlimScroll.Maximum = maxOffset + _doctorFilterSlimScroll.LargeChange - 1
                If _doctorFilterSlimScroll.Value > maxOffset Then _doctorFilterSlimScroll.Value = maxOffset
            Else
                _doctorFilterSlimScroll.Value = 0
                _doctorFilterSlimScroll.Visible = False
            End If
        End If
        flpDoctorFilterButtons.Left = If(_doctorFilterSlimScroll Is Nothing, 0, -_doctorFilterSlimScroll.Value)
        flpDoctorFilterButtons.Top = 0
        flpDoctorFilterButtons.Width = Math.Max(prefW, cw)
    End Sub

    Private Sub EnsureExtraDoctorFilterButtons(totalDoctorCount As Integer)
        If flpDoctorFilterButtons Is Nothing OrElse btnFilterDoctor1 Is Nothing Then Return
        Dim extraNeeded = Math.Max(0, totalDoctorCount - 8)
        While _extraDoctorFilterButtons.Count > extraNeeded
            Dim last = _extraDoctorFilterButtons(_extraDoctorFilterButtons.Count - 1)
            RemoveHandler last.Click, AddressOf FilterDoctorSlotButton_Click
            flpDoctorFilterButtons.Controls.Remove(last)
            last.Dispose()
            _extraDoctorFilterButtons.RemoveAt(_extraDoctorFilterButtons.Count - 1)
        End While
        While _extraDoctorFilterButtons.Count < extraNeeded
            _extraDoctorFilterButtons.Add(CreateExtraDoctorFilterButton())
        End While
    End Sub

    Private Function CreateExtraDoctorFilterButton() As SimpleButton
        Dim tpl = btnFilterDoctor1
        Dim nb As New SimpleButton With {
            .Name = "btnFilterDoctorExtra" & _extraDoctorFilterButtons.Count.ToString(),
            .Dock = DockStyle.None,
            .Margin = tpl.Margin,
            .MinimumSize = tpl.MinimumSize,
            .Size = tpl.Size,
            .Visible = True
        }
        nb.Appearance.Font = tpl.Appearance.Font
        nb.Appearance.Options.UseFont = True
        nb.Appearance.Options.UseTextOptions = True
        nb.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        AddHandler nb.Click, AddressOf FilterDoctorSlotButton_Click
        flpDoctorFilterButtons.Controls.Add(nb)
        Return nb
    End Function

    Private Shared Function ReasonFilterIdleBack(idx As Integer) As Color
        Select Case idx
            Case 0
                Return Color.PaleTurquoise
            Case 1
                Return Color.LightGoldenrodYellow
            Case 2
                Return Color.LightSkyBlue
            Case 3
                Return Color.LightGreen
            Case 4
                Return Color.LightCoral
            Case 5
                Return Color.LightGray
            Case Else
                Return Color.WhiteSmoke
        End Select
    End Function

    Private Function CurrentReasonFilterSelectionIndex() As Integer
        If String.IsNullOrWhiteSpace(FilterStatus) Then Return 0
        Select Case FilterStatus
            Case "Pending"
                Return 1
            Case "Running"
                Return 2
            Case "Completed"
                Return 3
            Case "Canceled"
                Return 4
            Case "Postponed"
                Return 5
            Case Else
                Return 0
        End Select
    End Function

    Private Function DoctorFilterSelectionIndex() As Integer
        If Not FilterDoctorId.HasValue Then Return 0
        For j = 0 To _doctorSlotDrIds.Count - 1
            If _doctorSlotDrIds(j) <> 0 AndAlso _doctorSlotDrIds(j) = FilterDoctorId.Value Then Return j + 1
        Next
        Return 0
    End Function

    Private Sub RefreshPatientFilterButtonStyles()
        If btnAllPatients Is Nothing OrElse btnThisPatient Is Nothing Then Return
        Dim sel = If(FilterPatientId.HasValue, 1, 0)
        StyleSchedulerFilterChip(btnAllPatients, sel = 0, True, Color.FromArgb(230, 255, 230))
        StyleSchedulerFilterChip(btnThisPatient, sel = 1, True, Color.FromArgb(255, 220, 188))
    End Sub

    Private Sub RefreshReasonFilterButtonStyles()
        If btnAllReasons Is Nothing Then Return
        Dim sel = CurrentReasonFilterSelectionIndex()
        Dim buttons = {btnAllReasons, btnPending, btnRunning, btnCompleted, btnCanceled, btnPostponed}
        For i = 0 To buttons.Length - 1
            Dim b = buttons(i)
            If b Is Nothing Then Continue For
            StyleSchedulerFilterChip(b, sel = i, True, ReasonFilterIdleBack(i))
        Next
    End Sub

    Private Sub RefreshDoctorFilterButtonStyles()
        If btnFilterDoctor0 Is Nothing Then Return
        Dim sel = DoctorFilterSelectionIndex()
        Dim maxSlot = _doctorSlotDrIds.Count
        For i = 0 To maxSlot
            Dim b = DoctorFilterButton(i)
            If b Is Nothing Then Continue For
            Dim en = b.Enabled
            Dim idle As Color
            If i = 0 Then
                idle = Color.FromArgb(200, 230, 200)
            Else
                idle = If(i - 1 < _doctorSlotColors.Count, _doctorSlotColors(i - 1), Color.LightSteelBlue)
            End If
            StyleSchedulerFilterChip(b, sel = i AndAlso en, en, idle)
        Next
    End Sub

    Private Sub RefreshSchedulerFilterButtonStyles()
        RefreshPatientFilterButtonStyles()
        RefreshReasonFilterButtonStyles()
        RefreshDoctorFilterButtonStyles()
    End Sub

    ''' <summary>Patient + status filter captions on <see cref="btnAllPatients"/> / <see cref="btnAllReasons"/>; localize with <c>If(Eng, en, ar)</c>.</summary>
    Private Sub ApplyLocalizedSchedulerFilterRadioCaptions()
        Try
            If btnAllPatients IsNot Nothing Then btnAllPatients.Text = If(Eng, "All Patients", "كل المرضى")
            If btnThisPatient IsNot Nothing Then btnThisPatient.Text = If(Eng, "This Patient", "هذا المريض")
            If btnAllReasons IsNot Nothing Then btnAllReasons.Text = If(Eng, "All Reasons", "كل الأسباب")
            If btnPending IsNot Nothing Then btnPending.Text = If(Eng, "Pending", "قيد الانتظار")
            If btnRunning IsNot Nothing Then btnRunning.Text = If(Eng, "Running", "قيد التنفيذ")
            If btnCompleted IsNot Nothing Then btnCompleted.Text = If(Eng, "Completed", "منجز")
            If btnCanceled IsNot Nothing Then btnCanceled.Text = If(Eng, "Canceled", "ملغى")
            If btnPostponed IsNot Nothing Then btnPostponed.Text = If(Eng, "Postponed", "مؤجل")
            RefreshSchedulerFilterButtonStyles()
        Catch
        End Try
    End Sub

    ''' <summary>Call after <see cref="Module1.Eng"/> changes so legend, filters, and layout match the language.</summary>
    Friend Sub ApplyLanguageRuntimeRefresh()
        Try
            If IsDisposed OrElse Not IsHandleCreated Then Return
            ApplyLocalizedSchedulerFilterRadioCaptions()
            legendPanel.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
            grpFilters.Text = If(Eng, "Filter Tools", "أدوات التصفية")
            legendPanel.Text = If(Eng, "Filters", "تصفية")
            includeReasonCheck.Properties.Caption = If(Eng, "Include Notes", "تضمين السبب")
            boldFontCheck.Properties.Caption = If(Eng, "Bold", "غامق")
            sizeFontCheck.Properties.Caption = If(Eng, "Size 10", "حجم 10")
            If btnPrevView IsNot Nothing Then
                btnPrevView.Text = If(Eng, "Previous", "السابق")
                btnPrevView.ToolTip = If(Eng, "Previous ", "العرض السابق")
            End If
            If btnNextView IsNot Nothing Then
                btnNextView.Text = If(Eng, "Next", "التالي")
                btnNextView.ToolTip = If(Eng, "Next View ", "العرض التالي")
            End If
            SyncBodyExpandButtonVisual()
            LoadAndRender()
        Catch
        End Try
    End Sub

    ''' <summary><see cref="btnAllPatients"/> / <see cref="btnThisPatient"/>; status row <see cref="btnAllReasons"/> … <see cref="btnPostponed"/>; doctors <see cref="btnFilterDoctor0"/> … <see cref="btnFilterDoctor8"/>.</summary>
    Private Sub SyncFilterRadiosFromState()
        If grpFilters Is Nothing OrElse Not grpFilters.Visible Then Return
        If btnAllPatients Is Nothing OrElse btnAllReasons Is Nothing OrElse btnFilterDoctor0 Is Nothing Then Return
        _suppressRadioFilterSync = True
        Try
            RefreshSchedulerFilterButtonStyles()
        Finally
            _suppressRadioFilterSync = False
        End Try
    End Sub

    Private Sub btnAllPatients_Click(sender As Object, e As EventArgs) Handles btnAllPatients.Click
        If _suppressRadioFilterSync Then Return
        FilterPatientId = Nothing
        lblPatient.Text = If(Eng, "All Patients", "كل المرضى")
        RefreshPatientFilterButtonStyles()
        LoadAndRender()
    End Sub

    Private Sub btnThisPatient_Click(sender As Object, e As EventArgs) Handles btnThisPatient.Click
        If _suppressRadioFilterSync Then Return
        If CurrentPatient Is Nothing Then
            RefreshPatientFilterButtonStyles()
            Return
        End If
        FilterPatientId = CurrentPatient.PatientID
        lblPatient.Text = CurrentPatient.PatientName
        RefreshPatientFilterButtonStyles()
        LoadAndRender()
    End Sub

    Private Sub FilterReasonButton_Click(sender As Object, e As EventArgs) Handles btnAllReasons.Click, btnPending.Click, btnRunning.Click, btnCompleted.Click, btnCanceled.Click, btnPostponed.Click
        If _suppressRadioFilterSync Then Return
        If sender Is btnAllReasons Then
            FilterReason = Nothing
            FilterStatus = Nothing
        ElseIf sender Is btnPending Then
            FilterReason = Nothing
            FilterStatus = "Pending"
        ElseIf sender Is btnRunning Then
            FilterReason = Nothing
            FilterStatus = "Running"
        ElseIf sender Is btnCompleted Then
            FilterReason = Nothing
            FilterStatus = "Completed"
        ElseIf sender Is btnCanceled Then
            FilterReason = Nothing
            FilterStatus = "Canceled"
        ElseIf sender Is btnPostponed Then
            FilterReason = Nothing
            FilterStatus = "Postponed"
        Else
            Return
        End If
        RefreshReasonFilterButtonStyles()
        LoadAndRender()
    End Sub

    Private Sub FilterDoctorAllButton_Click(sender As Object, e As EventArgs) Handles btnFilterDoctor0.Click
        If _suppressRadioFilterSync Then Return
        FilterDoctorId = Nothing
        RefreshDoctorFilterButtonStyles()
        LoadAndRender()
    End Sub

    Private Sub FilterDoctorSlotButton_Click(sender As Object, e As EventArgs)
        If _suppressRadioFilterSync Then Return
        Dim clicked = TryCast(sender, SimpleButton)
        If clicked Is Nothing Then Return
        Dim idx = GetDoctorFilterSlotIndexFromButton(clicked)
        If idx < 1 Then Return
        If idx > _doctorSlotDrIds.Count OrElse _doctorSlotDrIds(idx - 1) = 0 OrElse Not clicked.Enabled Then Return
        FilterDoctorId = _doctorSlotDrIds(idx - 1)
        RefreshDoctorFilterButtonStyles()
        LoadAndRender()
    End Sub

    Private Function GetDoctorFilterSlotIndexFromButton(clicked As SimpleButton) As Integer
        If clicked Is btnFilterDoctor0 Then Return 0
        For i = 1 To 8
            If DoctorFilterButton(i) Is clicked Then Return i
        Next
        For i = 0 To _extraDoctorFilterButtons.Count - 1
            If _extraDoctorFilterButtons(i) Is clicked Then Return 9 + i
        Next
        Return -1
    End Function

    Private Function PositionControlOld(control As Control, currentX As Integer, currentY As Integer,
                                 availableWidth As Integer, ByRef currentLineHeight As Integer,
                                 Optional fixedWidth As Integer = 100,
                                 Optional spacing As Integer = 8) As Integer
        control.Width = fixedWidth

        ' Wrap to next line if exceeds available width
        If currentX + control.Width + spacing > availableWidth - 10 Then
            currentX = 10
            currentY += currentLineHeight + spacing
            currentLineHeight = 0
        End If

        control.Location = New Point(currentX, currentY)
        currentLineHeight = Math.Max(currentLineHeight, control.Height)

        Return currentX + control.Width + spacing
    End Function

    ''' <summary>Flow layout for legend chips: LTR packs from <paramref name="innerLeft"/>, RTL from <paramref name="innerRight"/>; symmetric 10px-style bounds.</summary>
    Private Function PositionControl(control As Control,
                                 currentX As Integer,
                                 ByRef currentY As Integer,
                                 innerLeft As Integer,
                                 innerRight As Integer,
                                 ByRef currentLineHeight As Integer,
                                 Optional fixedWidth As Integer = 100,
                                 Optional spacing As Integer = 8) As Integer

        control.Width = fixedWidth

        If Eng Then
            If currentX + control.Width + spacing > innerRight Then
                currentX = innerLeft
                currentY += currentLineHeight + spacing
                currentLineHeight = 0
            End If

            control.Location = New Point(currentX, currentY)
            currentLineHeight = Math.Max(currentLineHeight, control.Height)
            Return currentX + control.Width + spacing

        Else
            If currentX - control.Width - spacing < innerLeft Then
                currentX = innerRight
                currentY += currentLineHeight + spacing
                currentLineHeight = 0
            End If

            control.Location = New Point(currentX - control.Width, currentY)
            currentLineHeight = Math.Max(currentLineHeight, control.Height)

            Return currentX - (control.Width + spacing)
        End If

    End Function

    Private Function CreateLabel(text As String, isHeader As Boolean) As Label
        Dim lbl As New Label()
        lbl.Text = text
        lbl.AutoSize = True
        lbl.Font = New Font("Segoe UI", 9.0!, If(isHeader, FontStyle.Bold, FontStyle.Regular))
        lbl.ForeColor = Color.Black
        lbl.Padding = New Padding(4)
        lbl.Margin = New Padding(2)
        Return lbl
    End Function
    Private Function CreateDoctorLabel(dr As (DrID As Integer, DrName As String, DrColor As String)) As Label
        Dim lbl As New Label()
        lbl.AutoSize = False
        lbl.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        lbl.ForeColor = Color.Black
        lbl.BackColor = ColorTranslator.FromHtml(dr.DrColor)
        lbl.TextAlign = ContentAlignment.MiddleCenter
        lbl.Padding = New Padding(8, 4, 8, 4)
        lbl.Margin = New Padding(3)
        lbl.Height = 30
        lbl.Tag = dr.DrID
        lbl.Cursor = Cursors.Hand
        lbl.Text = dr.DrName
        Return lbl
    End Function
    Private Function CreateDoctorComboBox(doctors As IEnumerable(Of (DrID As Integer, DrName As String, DrColor As String))) As DevExpress.XtraEditors.ComboBoxEdit
        Dim cboDocs As New DevExpress.XtraEditors.ComboBoxEdit()
        cboDocs.Width = 150
        cboDocs.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        cboDocs.ForeColor = Color.Black
        cboDocs.Padding = New Padding(4)
        cboDocs.Margin = New Padding(2)

        cboDocs.Properties.Items.Clear()
        cboDocs.Properties.Items.Add("(No Doctor Association)")

        For Each doc In doctors
            cboDocs.Properties.Items.Add(doc.DrName)
        Next
        If CurrentDoctor IsNot Nothing Then
            cboDocs.SelectedItem = CurrentDoctor.DrName
        Else
            cboDocs.SelectedIndex = 0
        End If

        AddHandler cboDocs.SelectedIndexChanged, AddressOf cboDocsSelectedIndexChanged
        Return cboDocs
    End Function

    Private Function ComboIndexFromViewMode(vm As ViewMode) As Integer
        Select Case vm
            'Case ViewMode.DayView
            '    Return 0
            Case ViewMode.ThisWeekFull
                Return 1
                'Case ViewMode.ThisWeek
                '    Return 2
                'Case ViewMode.MonthlyWeek
                '    Return 3
                'Case ViewMode.MonthView
                '    Return 4
                'Case ViewMode.DaysTimeline
                '    Return 5
                'Case ViewMode.DoctorsDay
                '    Return 6
            Case Else
                Return 1
        End Select
    End Function

    Private Sub ApplyViewModeToCombo(vm As ViewMode)
        Dim idx = ComboIndexFromViewMode(vm)
        If cmbView.SelectedIndex <> idx Then
            cmbView.SelectedIndex = idx
        Else
            _view = vm
        End If
    End Sub

    Private Sub UpdateViewNavButtonVisibility()
        If btnPrevView IsNot Nothing Then btnPrevView.Visible = _viewNavBack.Count > 0
        If btnNextView IsNot Nothing Then btnNextView.Visible = _viewNavForward.Count > 0
    End Sub

    Private Sub BtnPrevView_Click(sender As Object, e As EventArgs) Handles btnPrevView.Click
        If _viewNavBack.Count = 0 Then Return
        _viewNavForward.Push(_view)
        Dim previousView = _view
        Dim target = _viewNavBack.Pop()
        _suppressViewNavRecording = True
        Try
            ApplyViewModeToCombo(target)
        Finally
            _suppressViewNavRecording = False
        End Try
        _view = target
        LoadAndRenderWithViewTransition(previousView, -1)
        UpdateViewNavButtonVisibility()
    End Sub

    Private Sub BtnNextView_Click(sender As Object, e As EventArgs) Handles btnNextView.Click
        If _viewNavForward.Count = 0 Then Return
        _viewNavBack.Push(_view)
        Dim previousView = _view
        Dim target = _viewNavForward.Pop()
        _suppressViewNavRecording = True
        Try
            ApplyViewModeToCombo(target)
        Finally
            _suppressViewNavRecording = False
        End Try
        _view = target
        LoadAndRenderWithViewTransition(previousView, 1)
        UpdateViewNavButtonVisibility()
    End Sub

#End Region ' Doctor Legend
    Public Sub SetFilters(patientId As Integer?, DrID As Integer?, reason As String, start As DateTime, [end] As DateTime)
        FilterPatientId = patientId
        FilterDoctorId = DrID
        FilterReason = reason
        FilterStartDate = start
        FilterEndDate = [end]
        LoadAndRender()
    End Sub

    Public Sub SetView(view As ViewMode)
        Dim previousView = _view
        If previousView <> view Then
            _viewNavBack.Push(previousView)
            _viewNavForward.Clear()
        End If
        _suppressViewNavRecording = True
        Try
            ApplyViewModeToCombo(view)
        Finally
            _suppressViewNavRecording = False
        End Try
        _view = view
        LoadAndRenderWithViewTransition(previousView)
        UpdateViewNavButtonVisibility()
    End Sub
#End Region ' ═══ END DOCTOR LEGEND & UI SETUP ═══



    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 7 — DATA LOADING                                            ║
    ' ╚══════════════════════════════════════════════════════════════════════╝





#Region "═══ 7. DATA LOADING ═══"
    Private Sub EnsureAppointmentCachePrimed()
        If _appointmentCachePrimed Then Return
        If _repo Is Nothing Then Return
        _appointmentCachePrimed = True
        _allAppts = _repo.GetAllAppointments()
    End Sub

    Private Sub InvalidateFullAppointmentCache()
        _appointmentCachePrimed = False
    End Sub

    ''' <summary>Refetches the full appointment list and rebuilds PREV/NEXT edge-hint data. Call after any DB insert/update so hints (esp. with patient/doctor filters) match the database; <see cref="LoadAndRender"/> alone still uses the cached list until this runs.</summary>
    Private Sub RefreshAllAppointmentsCacheAndNavigationHints()
        If _repo Is Nothing Then Return
        _appointmentCachePrimed = False
        EnsureAppointmentCachePrimed()
        Dim reasonFilter As String = If(String.IsNullOrWhiteSpace(FilterReason), Nothing, FilterReason.Trim())
        Dim statusFilter As String = If(String.IsNullOrWhiteSpace(FilterStatus), Nothing, FilterStatus.Trim())
        Dim effectiveDoctorFilter As Integer? = FilterDoctorId
        'If _view = ViewMode.DoctorsDay Then effectiveDoctorFilter = Nothing
        RebuildNavigationHintLists(reasonFilter, statusFilter, effectiveDoctorFilter)
    End Sub

    Public Sub LoadAndRender()
        If _repo Is Nothing Then Return
        If _loadAndRenderInProgress Then
            _loadAndRenderQueued = True
            Return
        End If

        _loadAndRenderInProgress = True
        Try
            Do
                _loadAndRenderQueued = False
                EnsureAppointmentCachePrimed()
                Dim rangeStart As DateTime
                Dim rangeEnd As DateTime

                Select Case _view
                    'Case ViewMode.DayView
                    '    rangeStart = _currentDate.Date
                    '    rangeEnd = _currentDate.Date.AddDays(1)
                    Case ViewMode.ThisWeekFull
                        Dim currentDayOfWeek As Integer = CInt(_currentDate.DayOfWeek)
                        Dim daysToSaturday As Integer = (currentDayOfWeek - 6 + 7) Mod 7
                        Dim weekStart As DateTime = _currentDate.Date.AddDays(-daysToSaturday)
                        rangeStart = weekStart
                        rangeEnd = weekStart.AddDays(7)
                        'Case ViewMode.ThisWeek
                        '    Dim currentDayOfWeek As Integer = CInt(_currentDate.DayOfWeek)
                        '    Dim daysToSaturday As Integer = (currentDayOfWeek - 6 + 7) Mod 7
                        '    Dim weekStart As DateTime = _currentDate.Date.AddDays(-daysToSaturday)
                        '    rangeStart = weekStart
                        '    rangeEnd = weekStart.AddDays(6)
                        'Case ViewMode.MonthlyWeek
                        '    Dim d0 = _currentDate.Date.AddDays(-(CInt(_currentDate.DayOfWeek)))
                        '    rangeStart = d0
                        '    rangeEnd = d0.AddDays(7)
                        'Case ViewMode.DaysTimeline
                        '    Dim currentDow As Integer = CInt(_currentDate.DayOfWeek)
                        '    Dim daysToSat As Integer = (currentDow - 6 + 7) Mod 7
                        '    Dim tlWeekStart As DateTime = _currentDate.Date.AddDays(-daysToSat)
                        '    rangeStart = tlWeekStart
                        '    rangeEnd = tlWeekStart.AddDays(7)
                        'Case ViewMode.DoctorsDay
                        '    rangeStart = _currentDate.Date
                        '    rangeEnd = _currentDate.Date.AddDays(1)
                    Case Else
                        rangeStart = New DateTime(_currentDate.Year, _currentDate.Month, 1)
                        rangeEnd = rangeStart.AddMonths(1)
                End Select

                If FilterStartDate <> Date.MinValue Then rangeStart = Min(rangeStart, FilterStartDate)
                If FilterEndDate <> Date.MinValue Then rangeEnd = Max(rangeEnd, FilterEndDate)

                Dim reasonFilter As String = If(String.IsNullOrWhiteSpace(FilterReason), Nothing, FilterReason.Trim())
                Dim statusFilter As String = If(String.IsNullOrWhiteSpace(FilterStatus), Nothing, FilterStatus.Trim())
                Dim apptsForDoctorStrip = _repo.GetFiltered(rangeStart, rangeEnd, FilterPatientId, Nothing, reasonFilter, statusFilter)
                Dim effectiveDoctorFilter As Integer? = FilterDoctorId
                'If _view = ViewMode.DoctorsDay Then effectiveDoctorFilter = Nothing
                Dim loadedAppts = _repo.GetFiltered(rangeStart, rangeEnd, FilterPatientId, effectiveDoctorFilter, reasonFilter, statusFilter)
                Dim orderedAppts = ApptTheme.OrderAppointmentsForDisplay(loadedAppts, Function(id) _repo.GetDoctorName(id))
                _AppointmentC = New BindingList(Of AppointmentC)(orderedAppts)
                LoadDoctorLegend(apptsForDoctorStrip)
                RebuildNavigationHintLists(reasonFilter, statusFilter, effectiveDoctorFilter)
                Render()
            Loop While _loadAndRenderQueued AndAlso Not _isShuttingDown
        Finally
            _loadAndRenderInProgress = False
        End Try
    End Sub

    ''' <summary>PREV/NEXT edge hints must see appointments outside the visible date window (e.g. prior year). Uses full cache with the same filters as the grid, without date clipping.</summary>
    Private Sub RebuildNavigationHintLists(reasonFilter As String, statusFilter As String, doctorFilterForHints As Integer?)
        EnsureAppointmentCachePrimed()
        If _allAppts Is Nothing OrElse _allAppts.Count = 0 Then
            Dim empty = New List(Of AppointmentC)()
            _weekAppointmentCForHints = empty
            _monthAppointmentCForHints = empty
            _dayAppointmentCForHints = empty
            _tlAppointmentCForHints = empty
            Return
        End If
        Dim q = _allAppts.AsEnumerable()
        If FilterPatientId.HasValue Then q = q.Where(Function(a) a.PatientID = FilterPatientId.Value)
        If doctorFilterForHints.HasValue Then q = q.Where(Function(a) a.DrID = doctorFilterForHints.Value)
        If Not String.IsNullOrWhiteSpace(statusFilter) Then q = q.Where(Function(a) a.Status = statusFilter)
        If Not String.IsNullOrWhiteSpace(reasonFilter) Then
            Dim r = reasonFilter.Trim()
            q = q.Where(Function(a) a.Reason IsNot Nothing AndAlso a.Reason.IndexOf(r, StringComparison.OrdinalIgnoreCase) >= 0)
        End If
        Dim list = ApptTheme.OrderAppointmentsForDisplay(q.ToList(), Function(id) _repo.GetDoctorName(id))
        _weekAppointmentCForHints = list
        _monthAppointmentCForHints = list
        _dayAppointmentCForHints = list
        _tlAppointmentCForHints = list
    End Sub

    Private Function Min(a As DateTime, b As DateTime) As DateTime
        Return If(a < b, a, b)
    End Function
    Private Function Max(a As DateTime, b As DateTime) As DateTime
        Return If(a > b, a, b)
    End Function
#End Region ' ═══ END DATA LOADING ═══



    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 8 — RENDER DISPATCHER & SHARED HELPERS                      ║
    ' ╚══════════════════════════════════════════════════════════════════════╝





#Region "═══ 8. RENDER DISPATCHER & SHARED HELPERS ═══"

    Private Const WM_SETREDRAW As Integer = &HB

    ' VB uses the declared name as the DLL symbol unless EntryPoint is set; user32 exports "SendMessage".
    <DllImport("user32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessageRedraw(hWnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetGuiResources(hProcess As IntPtr, uiFlags As Integer) As Integer
    End Function

    Private Shared Sub SetControlDoubleBuffered(c As Control)
        If c Is Nothing Then Return
        Try
            Dim pi = GetType(Control).GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
            If pi IsNot Nothing Then pi.SetValue(c, True, Nothing)
        Catch
        End Try
    End Sub

    ''' <summary>Limits repaints to pnlBody during Clear/rebuild (recursive WM_SETREDRAW caused blank/clipped UI with DevExpress).</summary>
    Private Sub SuspendPnlBodyRedraw()
        If pnlBody Is Nothing OrElse Not pnlBody.IsHandleCreated Then Return
        SendMessageRedraw(pnlBody.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero)
    End Sub

    Private Sub ResumePnlBodyRedraw()
        If pnlBody Is Nothing OrElse Not pnlBody.IsHandleCreated Then Return
        SendMessageRedraw(pnlBody.Handle, WM_SETREDRAW, New IntPtr(1), IntPtr.Zero)
        ApptErrorHelper.SafeRefresh(pnlBody, "WeekSched.ResumePnlBodyRedraw.RefreshBody")
    End Sub

    ''' <summary>
    ''' <see cref="pnlBody"/>.Controls.Clear disposes children; cached edge-hint controls must be cleared or
    ''' <see cref="EnsureWeekEdgeHints"/> may re-add disposed controls and exhaust window handles.
    ''' </summary>
    Private Sub InvalidatePnlBodyEdgeHintsAfterClear()
        If _bodyViewHost IsNot Nothing AndAlso _bodyViewHost.IsDisposed Then _bodyViewHost = Nothing
        If _bodyPrevHintHost IsNot Nothing AndAlso _bodyPrevHintHost.IsDisposed Then _bodyPrevHintHost = Nothing
        If _bodyNextHintHost IsNot Nothing AndAlso _bodyNextHintHost.IsDisposed Then _bodyNextHintHost = Nothing
        If _bodyLayoutHost IsNot Nothing AndAlso _bodyLayoutHost.IsDisposed Then _bodyLayoutHost = Nothing
        If _weekPrevHint IsNot Nothing AndAlso _weekPrevHint.IsDisposed Then _weekPrevHint = Nothing
        If _weekNextHint IsNot Nothing AndAlso _weekNextHint.IsDisposed Then _weekNextHint = Nothing
        If _dayPrevHint IsNot Nothing AndAlso _dayPrevHint.IsDisposed Then _dayPrevHint = Nothing
        If _dayNextHint IsNot Nothing AndAlso _dayNextHint.IsDisposed Then _dayNextHint = Nothing
        If _monthPrevHint IsNot Nothing AndAlso _monthPrevHint.IsDisposed Then _monthPrevHint = Nothing
        If _monthNextHint IsNot Nothing AndAlso _monthNextHint.IsDisposed Then _monthNextHint = Nothing
        If _tlPrevHint IsNot Nothing AndAlso _tlPrevHint.IsDisposed Then _tlPrevHint = Nothing
        If _tlNextHint IsNot Nothing AndAlso _tlNextHint.IsDisposed Then _tlNextHint = Nothing
    End Sub

    ''' <summary>Closes DevExpress dropdown/calendar popups so popup forms do not accumulate during rapid refresh.</summary>
    Private Sub CloseSchedulerDevExpressPopups()
        Try
            If dtStartTime IsNot Nothing AndAlso Not dtStartTime.IsDisposed Then dtStartTime.ClosePopup()
            If dtEndTime IsNot Nothing AndAlso Not dtEndTime.IsDisposed Then dtEndTime.ClosePopup()
            If gotoDate IsNot Nothing AndAlso Not gotoDate.IsDisposed Then gotoDate.ClosePopup()
            If cmbView IsNot Nothing AndAlso Not cmbView.IsDisposed Then cmbView.ClosePopup()
            If _cboDocsLegend IsNot Nothing AndAlso Not _cboDocsLegend.IsDisposed Then _cboDocsLegend.ClosePopup()
        Catch
        End Try
    End Sub

    Private Shared Function IsTransientDevExpressPopupForm(f As Form) As Boolean
        If f Is Nothing OrElse f.IsDisposed Then Return False
        Dim typeName = f.GetType().FullName
        If String.IsNullOrEmpty(typeName) Then Return False
        Return typeName.IndexOf("DevExpress.XtraEditors.Popup.", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               typeName.IndexOf("DevExpress.Utils.SuperToolTipWindow", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               typeName.IndexOf("DevExpress.Utils.FlyoutPanelToolForm", StringComparison.OrdinalIgnoreCase) >= 0
    End Function

    Private Shared Function CountTransientPopupForms() As Integer
        Try
            Dim count = 0
            For Each f As Form In Application.OpenForms
                If IsTransientDevExpressPopupForm(f) Then count += 1
            Next
            Return count
        Catch
            Return -1
        End Try
    End Function

    Private Sub CloseGlobalTransientPopupForms()
        Try
            Dim toClose As New List(Of Form)()
            For Each f As Form In Application.OpenForms
                If IsTransientDevExpressPopupForm(f) Then toClose.Add(f)
            Next
            For Each popupForm As Form In toClose
                Try
                    popupForm.Hide()
                Catch
                End Try
                Try
                    popupForm.Close()
                Catch
                End Try
                Try
                    popupForm.Dispose()
                Catch
                End Try
            Next
        Catch
        End Try
    End Sub

    Private Sub EnsureSchedulerTransientUiHelpers()
        If _schedulerToolTip Is Nothing Then
            _schedulerToolTip = New ToolTip With {
                .AutomaticDelay = 300,
                .AutoPopDelay = 8000,
                .InitialDelay = 300,
                .ReshowDelay = 100
            }
        End If
        If _weekSnapshotToolTip Is Nothing Then
            _weekSnapshotToolTip = New ToolTip With {
                .AutomaticDelay = 300,
                .AutoPopDelay = 7000,
                .InitialDelay = 300,
                .ReshowDelay = 100
            }
        End If
        If _statusContextMenuFont Is Nothing Then
            _statusContextMenuFont = New Font("Calibri", 10, FontStyle.Bold)
        End If
        If _statusContextMenu Is Nothing Then
            _statusContextMenu = New ContextMenuStrip With {
                .ShowImageMargin = False,
                .Font = _statusContextMenuFont
            }
        End If
    End Sub

    Private Sub ClearSchedulerRenderToolTips()
        If _schedulerToolTip Is Nothing Then Return
        Try
            _schedulerToolTip.RemoveAll()
        Catch
        End Try
    End Sub

    Private Function SchedulerRenderToolTip() As ToolTip
        EnsureSchedulerTransientUiHelpers()
        Return _schedulerToolTip
    End Function

    Private Sub ReleaseSchedulerTransientUiHandles()
        CloseSchedulerDevExpressPopups()
        CloseGlobalTransientPopupForms()
        ClearSchedulerRenderToolTips()
        Try
            If _statusContextMenu IsNot Nothing Then _statusContextMenu.Close()
        Catch
        End Try
    End Sub

    Private Shared Function CurrentProcessUserHandleCount() As Integer
        Try
            Return GetGuiResources(Process.GetCurrentProcess().Handle, 1)
        Catch
            Return -1
        End Try
    End Function

    Private Shared Function CurrentProcessGdiHandleCount() As Integer
        Try
            Return GetGuiResources(Process.GetCurrentProcess().Handle, 0)
        Catch
            Return -1
        End Try
    End Function

    Private Function TryRecoverSchedulerResources() As Boolean
        ReleaseSchedulerTransientUiHandles()
        Try
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        Catch
        End Try
        Dim userHandles = CurrentProcessUserHandleCount()
        Dim gdiHandles = CurrentProcessGdiHandleCount()
        Dim popupForms = CountTransientPopupForms()
        Return (userHandles < 0 OrElse userHandles < 6500) AndAlso
               (gdiHandles < 0 OrElse gdiHandles < 8000) AndAlso
               (popupForms < 0 OrElse popupForms <= 2)
    End Function

    Private Function CurrentViewUsesLightweightSurface() As Boolean
        Return _view = ViewMode.ThisWeekFull
    End Function

    Private Function EnsureSchedulerCanRenderHeavyView() As Boolean
        Dim userHandles = CurrentProcessUserHandleCount()
        Dim gdiHandles = CurrentProcessGdiHandleCount()
        Dim popupForms = CountTransientPopupForms()
        Dim userHandleLimit = If(CurrentViewUsesLightweightSurface(), 9000, 6500)
        If (userHandles >= 0 AndAlso userHandles >= userHandleLimit) OrElse
           (gdiHandles >= 0 AndAlso gdiHandles >= 8000) OrElse
           (popupForms >= 0 AndAlso popupForms > 2) Then
            If Not TryRecoverSchedulerResources() Then
                If pnlBody IsNot Nothing Then
                    ClearSchedulerBodyContent()
                    Dim warn As New Label With {
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Font = New Font("Segoe UI", 11, FontStyle.Bold),
                        .ForeColor = Color.DarkRed,
                        .BackColor = Color.MistyRose,
                        .Text = If(Eng,
                                   "Scheduler rendering was paused because Windows UI handle usage is too high." & Environment.NewLine &
                                   "Close popup windows or switch views, then try again.",
                                   "تم إيقاف رسم الجدول لأن استهلاك مقابض واجهة ويندوز مرتفع جداً." & Environment.NewLine &
                                   "أغلق النوافذ المنبثقة أو بدّل العرض ثم حاول مرة أخرى.")
                    }
                    AddSchedulerBodyContent(warn)
                End If
                If DateTime.UtcNow - _lastSchedulerHandlePressureLogUtc > TimeSpan.FromSeconds(30) Then
                    _lastSchedulerHandlePressureLogUtc = DateTime.UtcNow
                    ApptErrorHelper.Report(
                        New InvalidOperationException($"Scheduler render blocked due to handle pressure. USER={userHandles}, GDI={gdiHandles}, POPUPS={popupForms}."),
                        "WeekSched.EnsureSchedulerCanRenderHeavyView",
                        showUser:=False)
                End If
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>Suppress painting on the schedule chrome while toggling header visibility + row heights (expand/collapse body).</summary>
    Private Sub BeginBatchSchedulerVisualUpdate()
        If tlpMain IsNot Nothing AndAlso tlpMain.IsHandleCreated Then
            SendMessageRedraw(tlpMain.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero)
        End If
        If Me.IsHandleCreated Then
            SendMessageRedraw(Me.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero)
        End If
    End Sub

    Private Sub EndBatchSchedulerVisualUpdate()
        If tlpMain IsNot Nothing AndAlso tlpMain.IsHandleCreated Then
            SendMessageRedraw(tlpMain.Handle, WM_SETREDRAW, New IntPtr(1), IntPtr.Zero)
        End If
        If Me.IsHandleCreated Then
            SendMessageRedraw(Me.Handle, WM_SETREDRAW, New IntPtr(1), IntPtr.Zero)
            ApptErrorHelper.SafeRefresh(Me, "WeekSched.EndBatchSchedulerVisualUpdate.RefreshSelf")
        End If
    End Sub

#Region "8a. Body layout metrics (geometric fit to pnlBody)"
    Private Function SchedulerBodyClientWidth() As Integer
        Dim targetHost = CType(If(_bodyViewHost, pnlBody), Control)
        If targetHost Is Nothing Then Return 800
        Return Math.Max(1, targetHost.ClientSize.Width)
    End Function

    Private Function SchedulerBodyClientHeight() As Integer
        Dim targetHost = CType(If(_bodyViewHost, pnlBody), Control)
        If targetHost Is Nothing Then Return 600
        Return Math.Max(1, targetHost.ClientSize.Height)
    End Function

    ''' <summary>Usable width for FlowLayoutPanels with horizontal padding (left + right) already subtracted from body.</summary>
    Private Function SchedulerBodyInnerFlowWidth(flowHorizontalPadding As Integer) As Integer
        Return Math.Max(1, SchedulerBodyClientWidth() - flowHorizontalPadding - SystemInformation.VerticalScrollBarWidth - 2)
    End Function

    ''' <summary>Height for the horizontal day-columns band in week views (below the week title label).</summary>
    Private Function SchedulerWeekDaysBandHeight(weekHeaderHeight As Integer, mainFlowPadTop As Integer, mainFlowPadBottom As Integer, gapAfterHeader As Integer) As Integer
        Dim h = SchedulerBodyClientHeight() - weekHeaderHeight - mainFlowPadTop - mainFlowPadBottom - gapAfterHeader
        Return Math.Max(120, h)
    End Function
#End Region

    Private Sub ApplyPerformanceRenderingOptions()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        SetControlDoubleBuffered(Me)
        SetControlDoubleBuffered(tlpMain)
        SetControlDoubleBuffered(pnlBody)
        SetControlDoubleBuffered(pnlHeader)
        If legendPanel IsNot Nothing Then SetControlDoubleBuffered(legendPanel)
        If grpFilters IsNot Nothing Then SetControlDoubleBuffered(grpFilters)
        If filtersTable IsNot Nothing Then SetControlDoubleBuffered(filtersTable)
    End Sub

    Private Sub CaptureTlpMainRowMetricsOnce()
        If _tlpMainRowsCaptured Then Return
        If tlpMain Is Nothing OrElse tlpMain.Rows.Count < 2 Then Return
        _tlpMainRow0Style = tlpMain.Rows(0).Style
        _tlpMainRow0Height = tlpMain.Rows(0).Height
        _tlpMainRow1Style = tlpMain.Rows(1).Style
        _tlpMainRow1Height = tlpMain.Rows(1).Height
        _tlpMainRowsCaptured = True
    End Sub

    Private Sub EnsureBodyWorkspaceExpandButton()
        If _btnBodyExpand IsNot Nothing Then Return
        CaptureTlpMainRowMetricsOnce()
        _btnBodyExpand = New SimpleButton With {
            .Name = "btnBodyExpand",
            .Size = New Size(26, 26),
            .Cursor = Cursors.Hand,
            .TabStop = False
        }
        _btnBodyExpand.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        _btnBodyExpand.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        AddHandler _btnBodyExpand.Click, AddressOf BodyExpandButton_Click
        _btnBodyQuickAdd = New SimpleButton With {
            .Name = "btnBodyQuickAdd",
            .Size = New Size(52, 26),
            .Cursor = Cursors.Hand,
            .TabStop = False,
            .Visible = False
        }
        _btnBodyQuickAdd.Appearance.Font = New Font("Calibri", 10.0F, FontStyle.Bold)
        _btnBodyQuickAdd.Appearance.Options.UseFont = True
        _btnBodyQuickAdd.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        _btnBodyQuickAdd.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        AddHandler _btnBodyQuickAdd.Click, AddressOf BodyQuickAddButton_Click
        AddHandler pnlBody.SizeChanged, AddressOf PnlBody_ChangedForExpandBtnLayout
        AddHandler pnlBody.LocationChanged, AddressOf PnlBody_ChangedForExpandBtnLayout
        Controls.Add(_btnBodyExpand)
        Controls.Add(_btnBodyQuickAdd)
        SyncBodyExpandButtonVisual()
        LayoutBodyWorkspaceExpandButton()
    End Sub

    Private Sub BodyQuickAddButton_Click(sender As Object, e As EventArgs)
        ' Same behavior as toolbar btnAdd (PerformClick does not reliably fire the handler when the header is hidden in expanded mode).
        If btnAdd Is Nothing OrElse btnAdd.IsDisposed Then Return
        btnAdd_Click(btnAdd, EventArgs.Empty)
    End Sub

    Private Sub SyncBodyQuickAddButtonVisual()
        If _btnBodyQuickAdd Is Nothing Then Return
        _btnBodyQuickAdd.Text = If(Eng, "Add", "إضافة")
        _btnBodyQuickAdd.ToolTip = If(Eng, "Add appointment (same as toolbar)", "إضافة موعد (كزر شريط الأدوات)")
        _btnBodyQuickAdd.Visible = _pnlBodyWorkspaceMaximized
    End Sub

    Private Sub PnlBody_ChangedForExpandBtnLayout(sender As Object, e As EventArgs)
        LayoutBodyWorkspaceExpandButton()
        RefreshEdgeHintLayoutAfterBodyResize()
    End Sub

    Private Shared Function CreateBodyEdgeHintHost(name As String) As PanelControl
        Dim host = New PanelControl() With {
            .Dock = DockStyle.Fill,
            .Name = name,
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
            .Padding = New Padding(BodyEdgeHintHostPadding, 0, BodyEdgeHintHostPadding, 0),
            .Margin = Padding.Empty
        }
        host.Appearance.BackColor = Color.Transparent
        host.Appearance.Options.UseBackColor = True
        Return host
    End Function

    Private Sub EnsureSchedulerBodyWorkspace()
        If pnlBody Is Nothing OrElse pnlBody.IsDisposed Then Return
        If _bodyLayoutHost IsNot Nothing AndAlso Not _bodyLayoutHost.IsDisposed AndAlso _bodyLayoutHost.Parent Is pnlBody Then
            Return
        End If

        pnlBody.SuspendLayout()
        Try
            If _bodyLayoutHost IsNot Nothing AndAlso Not _bodyLayoutHost.IsDisposed AndAlso _bodyLayoutHost.Parent IsNot Nothing Then
                _bodyLayoutHost.Parent.Controls.Remove(_bodyLayoutHost)
                _bodyLayoutHost.Dispose()
            End If

            _bodyViewHost = New PanelControl() With {
                .Dock = DockStyle.Fill,
                .Name = "schedulerBodyViewHost",
                .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
                .Margin = Padding.Empty
            }
            _bodyViewHost.Appearance.BackColor = Color.Transparent
            _bodyViewHost.Appearance.Options.UseBackColor = True

            _bodyPrevHintHost = CreateBodyEdgeHintHost("schedulerBodyPrevHintHost")
            _bodyNextHintHost = CreateBodyEdgeHintHost("schedulerBodyNextHintHost")

            _bodyLayoutHost = New TablePanel With {
                .Dock = DockStyle.Fill,
                .Name = "schedulerBodyLayoutHost",
                .Padding = Padding.Empty,
                .Margin = Padding.Empty
            }
            _bodyLayoutHost.Appearance.BackColor = Color.Transparent
            _bodyLayoutHost.Appearance.Options.UseBackColor = True
            _bodyLayoutHost.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Absolute, BodyEdgeHintBandWidth))
            _bodyLayoutHost.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Relative, 100.0F))
            _bodyLayoutHost.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Absolute, BodyEdgeHintBandWidth))
            _bodyLayoutHost.Rows.Add(New TablePanelRow(TablePanelEntityStyle.Relative, 100.0F))
            _bodyLayoutHost.Controls.Add(_bodyPrevHintHost)
            _bodyLayoutHost.Controls.Add(_bodyViewHost)
            _bodyLayoutHost.Controls.Add(_bodyNextHintHost)
            _bodyLayoutHost.SetColumn(_bodyPrevHintHost, 0)
            _bodyLayoutHost.SetRow(_bodyPrevHintHost, 0)
            _bodyLayoutHost.SetColumn(_bodyViewHost, 1)
            _bodyLayoutHost.SetRow(_bodyViewHost, 0)
            _bodyLayoutHost.SetColumn(_bodyNextHintHost, 2)
            _bodyLayoutHost.SetRow(_bodyNextHintHost, 0)

            DisposeChildControls(pnlBody)
            pnlBody.Controls.Add(_bodyLayoutHost)

            If Not _bodyWorkspaceEventsWired Then
                AddHandler pnlBody.SizeChanged, AddressOf PnlBody_ChangedForExpandBtnLayout
                _bodyWorkspaceEventsWired = True
            End If
            RemoveHandler _bodyViewHost.SizeChanged, AddressOf PnlBody_ChangedForExpandBtnLayout
            RemoveHandler _bodyPrevHintHost.SizeChanged, AddressOf PnlBody_ChangedForExpandBtnLayout
            RemoveHandler _bodyNextHintHost.SizeChanged, AddressOf PnlBody_ChangedForExpandBtnLayout
            AddHandler _bodyViewHost.SizeChanged, AddressOf PnlBody_ChangedForExpandBtnLayout
            AddHandler _bodyPrevHintHost.SizeChanged, AddressOf PnlBody_ChangedForExpandBtnLayout
            AddHandler _bodyNextHintHost.SizeChanged, AddressOf PnlBody_ChangedForExpandBtnLayout
        Finally
            pnlBody.ResumeLayout(False)
        End Try
    End Sub

    Private Function SchedulerBodyContentHost() As PanelControl
        EnsureSchedulerBodyWorkspace()
        Return If(_bodyViewHost, pnlBody)
    End Function

    Private Sub ClearSchedulerBodyContent()
        Dim host = SchedulerBodyContentHost()
        If host Is Nothing Then Return
        DisposeChildControls(host)
        InvalidatePnlBodyEdgeHintsAfterClear()
    End Sub

    Private Sub AddSchedulerBodyContent(ctrl As Control)
        If ctrl Is Nothing Then Return
        Dim host = SchedulerBodyContentHost()
        If host Is Nothing Then Return
        host.Controls.Add(ctrl)
    End Sub

    ''' <summary>Top-right overlay on pnlBody; parented to the scheduler so pnlBody.Controls.Clear() never removes it. Quick-add sits to the left of expand when expanded.</summary>
    Private Sub LayoutBodyWorkspaceExpandButton()
        If _btnBodyExpand Is Nothing OrElse pnlBody Is Nothing Then Return
        Dim targetHost = CType(If(_bodyViewHost, pnlBody), Control)
        If targetHost Is Nothing Then Return
        Const pad As Integer = 3
        Const gap As Integer = 4
        Dim clientY = pad
        Dim expandClientX = targetHost.ClientSize.Width - _btnBodyExpand.Width - pad
        Dim ptExpand = Me.PointToClient(targetHost.PointToScreen(New Point(expandClientX, clientY)))
        _btnBodyExpand.Location = ptExpand
        _btnBodyExpand.BringToFront()
        If _btnBodyQuickAdd IsNot Nothing Then
            Dim quickClientX = expandClientX - gap - _btnBodyQuickAdd.Width
            Dim ptQuick = Me.PointToClient(targetHost.PointToScreen(New Point(quickClientX, clientY)))
            _btnBodyQuickAdd.Location = ptQuick
            _btnBodyQuickAdd.BringToFront()
        End If
    End Sub

    Private Sub SyncBodyExpandButtonVisual()
        If _btnBodyExpand Is Nothing Then Return
        If _pnlBodyWorkspaceMaximized Then
            _btnBodyExpand.Text = "▼"
            _btnBodyExpand.ToolTip = If(Eng, "Restore header", "استعادة الرأس والفلاتر")
        Else
            _btnBodyExpand.Text = "▲"
            _btnBodyExpand.ToolTip = If(Eng, "Expand schedule (full height)", "توسيع الجدول بكامل الارتفاع")
        End If
        SyncBodyQuickAddButtonVisual()
    End Sub

    Private Sub ApplyBodyWorkspaceMaximizedState()
        CaptureTlpMainRowMetricsOnce()
        If tlpMain Is Nothing OrElse tlpMain.Rows.Count < 2 OrElse Not _tlpMainRowsCaptured Then Return
        Me.SuspendLayout()
        tlpMain.SuspendLayout()
        If pnlHeader IsNot Nothing Then pnlHeader.SuspendLayout()
        If pnlBody IsNot Nothing Then pnlBody.SuspendLayout()
        Try
            If _pnlBodyWorkspaceMaximized Then
                pnlHeader.Visible = False
                ' Absolute 0 left row 1 with no height on some TablePanel builds; keep a tiny row for stable layout.
                tlpMain.Rows(0).Style = TablePanelEntityStyle.Absolute
                tlpMain.Rows(0).Height = 2.0F
                tlpMain.Rows(1).Style = TablePanelEntityStyle.Relative
                tlpMain.Rows(1).Height = 100.0F
            Else
                pnlHeader.Visible = True
                tlpMain.Rows(0).Style = _tlpMainRow0Style
                tlpMain.Rows(0).Height = _tlpMainRow0Height
                tlpMain.Rows(1).Style = _tlpMainRow1Style
                tlpMain.Rows(1).Height = _tlpMainRow1Height
            End If
        Finally
            If pnlBody IsNot Nothing Then pnlBody.ResumeLayout(False)
            If pnlHeader IsNot Nothing Then pnlHeader.ResumeLayout(False)
            tlpMain.ResumeLayout(False)
            Me.ResumeLayout(False)
        End Try
        tlpMain.PerformLayout()
        If pnlHeader IsNot Nothing Then pnlHeader.PerformLayout()
        If pnlBody IsNot Nothing Then pnlBody.PerformLayout()
        Me.PerformLayout()
        LayoutBodyWorkspaceExpandButton()
    End Sub

    Private Sub BodyExpandButton_Click(sender As Object, e As EventArgs)
        _pnlBodyWorkspaceMaximized = Not _pnlBodyWorkspaceMaximized
        SyncBodyExpandButtonVisual()
        BeginBatchSchedulerVisualUpdate()
        Try
            ApplyBodyWorkspaceMaximizedState()
            LoadAndRender()
        Finally
            EndBatchSchedulerVisualUpdate()
        End Try
        LayoutBodyWorkspaceExpandButton()
    End Sub

    Protected Overrides Sub OnLayout(levent As LayoutEventArgs)
        MyBase.OnLayout(levent)
        LayoutBodyWorkspaceExpandButton()
    End Sub

    ''' <summary>Aligns _view and combo to 7-day week without triggering LoadAndRender (caller runs LoadAndRender once).</summary>
    Private Sub SyncDefaultWeekViewFromCombo()
        _suppressViewNavRecording = True
        Try
            ApplyViewModeToCombo(ViewMode.ThisWeekFull)
        Finally
            _suppressViewNavRecording = False
        End Try
        _view = ViewMode.ThisWeekFull
    End Sub

    Private Sub Render()
        If _isShuttingDown OrElse IsDisposed OrElse Disposing OrElse pnlBody Is Nothing Then Return
        EnsureSchedulerBodyWorkspace()
        ReleaseSchedulerTransientUiHandles()
        Me.SuspendLayout()
        If pnlBody IsNot Nothing Then pnlBody.SuspendLayout()
        SuspendPnlBodyRedraw()
        Try
            If pnlBody IsNot Nothing Then
                ClearSchedulerBodyContent()
            End If
            If Not EnsureSchedulerCanRenderHeavyView() Then Return

            ' 🔹 Update lblRange text dynamically
            ' 🔹 Update lblCount to match displayed appts for current view (day/week/month/timeline)
            Select Case _view
                'Case ViewMode.DayView
                '    lblRange.Text = AppointDateFormat.FormatDayDate(_currentDate)
                Case ViewMode.ThisWeekFull
                    Dim currentDayOfWeek As Integer = CInt(_currentDate.DayOfWeek)
                    Dim daysToSaturday As Integer = (currentDayOfWeek - 6 + 7) Mod 7
                    Dim weekStart As DateTime = _currentDate.Date.AddDays(-daysToSaturday)
                    Dim weekEnd As DateTime = weekStart.AddDays(6)
                    lblRange.Text = AppointDateFormat.FormatDateRange(weekStart, weekEnd) ' Sat to Fri (7 days)
                    'Case ViewMode.ThisWeek
                    '    Dim currentDayOfWeek6 As Integer = CInt(_currentDate.DayOfWeek)
                    '    Dim daysToSaturday6 As Integer = (currentDayOfWeek6 - 6 + 7) Mod 7
                    '    Dim weekStart6 As DateTime = _currentDate.Date.AddDays(-daysToSaturday6)
                    '    Dim weekEnd6 As DateTime = weekStart6.AddDays(5)
                    '    lblRange.Text = AppointDateFormat.FormatDateRange(weekStart6, weekEnd6) ' Sat to Thu (6 days)
                    'Case ViewMode.MonthlyWeek
                    '    Dim startOfWeek = _currentDate.Date.AddDays(-CInt(_currentDate.DayOfWeek))
                    '    Dim endOfWeek = startOfWeek.AddDays(6)
                    '    lblRange.Text = AppointDateFormat.FormatDateRange(startOfWeek, endOfWeek)
                    'Case ViewMode.MonthView
                    '    Dim monthStart = New DateTime(_currentDate.Year, _currentDate.Month, 1)
                    '    Dim monthEnd = monthStart.AddMonths(1).AddDays(-1)
                    '    lblRange.Text = AppointDateFormat.FormatMonthYear(_currentDate)
                    'Case ViewMode.DaysTimeline
                    '    Dim tlDow As Integer = CInt(_currentDate.DayOfWeek)
                    '    Dim tlDaysToSat As Integer = (tlDow - 6 + 7) Mod 7
                    '    Dim tlStart As DateTime = _currentDate.Date.AddDays(-tlDaysToSat)
                    '    Dim tlEnd As DateTime = tlStart.AddDays(6)
                    '    lblRange.Text = AppointDateFormat.FormatDateRange(tlStart, tlEnd)
                    'Case ViewMode.DoctorsDay
                    '    lblRange.Text = AppointDateFormat.FormatDayDate(_currentDate)
            End Select
            UpdateLblCountDisplay()

            Select Case _view
                'Case ViewMode.DayView
                '    If _pendingDayViewDoctorFilter.HasValue Then
                '        Dim focusDr = _pendingDayViewDoctorFilter.Value
                '        _pendingDayViewDoctorFilter = Nothing
                '        RenderDayView(_currentDate, focusDr)
                '    Else
                '        RenderDayView(_currentDate)
                '    End If
                Case ViewMode.ThisWeekFull
                    RenderCurrentWeek7Days(_currentDate)
                    'Case ViewMode.ThisWeek
                    '    'RenderCurrentWeekView(_currentDate)
                    '    RenderCurrentWeek6Days(_currentDate)
                    'Case ViewMode.MonthlyWeek
                    '    RenderMonthWeeksView(_currentDate)
                    'Case ViewMode.MonthView
                    '    RenderMonthView(_currentDate)
                    'Case ViewMode.DaysTimeline
                    '    RenderDaysTimelineView(_currentDate)
                    'Case ViewMode.DoctorsDay
                    '    RenderDoctorsDayView(_currentDate)
            End Select
        Finally
            ResumePnlBodyRedraw()
            If pnlBody IsNot Nothing Then pnlBody.ResumeLayout(True)
        End Try
        Me.ResumeLayout(True)
        LayoutBodyWorkspaceExpandButton()
        Select Case _view
            Case ViewMode.ThisWeekFull ', ViewMode.ThisWeek, ViewMode.MonthlyWeek, ViewMode.MonthView
                ScheduleEdgeHintsLayoutAfterLayout()
        End Select
        ApplyHeaderToolbarCentering()
    End Sub

    ''' <summary>Repositions PREV/NEXT edge hints after pnlBody has a real ClientSize (first paint often had 0×0 during Render).</summary>
    Private Sub RefreshEdgeHintLayoutAfterBodyResize()
        If pnlBody Is Nothing Then Return
        If pnlBody.ClientSize.Width < 1 OrElse pnlBody.ClientSize.Height < 1 Then Return
        Select Case _view
            'Case ViewMode.DayView, ViewMode.DoctorsDay
            '    If _dayPrevHint IsNot Nothing AndAlso _dayNextHint IsNot Nothing Then PositionDayEdgeHints()
            Case ViewMode.ThisWeekFull ', ViewMode.ThisWeek, ViewMode.MonthlyWeek
                If _weekPrevHint IsNot Nothing AndAlso _weekNextHint IsNot Nothing Then PositionWeekEdgeHints()
                'Case ViewMode.MonthView
                '    If _monthPrevHint IsNot Nothing AndAlso _monthNextHint IsNot Nothing Then PositionMonthEdgeHints()
                'Case ViewMode.DaysTimeline
                '    If _tlPrevHint IsNot Nothing AndAlso _tlNextHint IsNot Nothing Then PositionTimelineEdgeHints()
        End Select
    End Sub

    Private Sub ScheduleEdgeHintsLayoutAfterLayout()
        If pnlBody Is Nothing OrElse Not IsHandleCreated Then Return
        BeginInvoke(New Action(Sub()
                                   If IsDisposed OrElse pnlBody Is Nothing Then Return
                                   RefreshEdgeHintLayoutAfterBodyResize()
                               End Sub))
    End Sub

    Private Function GetPastelColor(index As Integer) As Color
        Dim hue = (index * 60) Mod 360 ' distribute hues
        Return Color.FromArgb(255, ColorFromHSV(hue, 0.3, 1))
    End Function

    Private Function ColorFromHSV(hue As Double, saturation As Double, value As Double) As Color
        Dim hi As Integer = CInt(Math.Floor(hue / 60)) Mod 6
        Dim f As Double = hue / 60 - Math.Floor(hue / 60)
        Dim v As Integer = CInt(value * 255)
        Dim p As Integer = CInt(v * (1 - saturation))
        Dim q As Integer = CInt(v * (1 - f * saturation))
        Dim t As Integer = CInt(v * (1 - (1 - f) * saturation))

        Select Case hi
            Case 0 : Return Color.FromArgb(255, v, t, p)
            Case 1 : Return Color.FromArgb(255, q, v, p)
            Case 2 : Return Color.FromArgb(255, p, v, t)
            Case 3 : Return Color.FromArgb(255, p, q, v)
            Case 4 : Return Color.FromArgb(255, t, p, v)
            Case 5 : Return Color.FromArgb(255, v, p, q)
            Case Else : Return Color.Black
        End Select
    End Function

    Private Function MeasureWrappedLabelHeight(text As String, font As Font, width As Integer) As Integer
        Dim maxWidth = Math.Max(1, width)
        Dim measured = TextRenderer.MeasureText(text, font, New Size(maxWidth, Integer.MaxValue), TextFormatFlags.WordBreak)
        Return Math.Max(20, measured.Height + 4)
    End Function

    ''' <summary>Days-timeline card body: tight height from real text (no generic 20px floor) so compact rows don’t show a false “reason-sized” gap.</summary>
    Private Function MeasureDaysTimelineAppTextHeight(text As String, font As Font, width As Integer) As Integer
        Dim maxWidth = Math.Max(1, width)
        Dim measured = TextRenderer.MeasureText(text, font, New Size(maxWidth, Integer.MaxValue),
            TextFormatFlags.WordBreak Or TextFormatFlags.NoPadding)
        Return Math.Max(font.Height, measured.Height + 2)
    End Function

    ''' <summary>Compact height for one-line labels (week band / day band). Width = maxWidth; height = text + vertical padding.</summary>
    Private Shared Function MeasureSingleLineLabelSize(text As String, font As Font, maxWidth As Integer, padH As Integer, padV As Integer) As Size
        Dim innerW = Math.Max(1, maxWidth - padH * 2)
        Dim sz = TextRenderer.MeasureText(text, font, New Size(innerW, Integer.MaxValue), TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPadding)
        Return New Size(Math.Max(1, maxWidth), Math.Max(sz.Height + padV * 2, font.Height + padV * 2))
    End Function

    ' 🔹 Enforce global time window
    Private Function IsWithinWorkingHours(ap As AppointmentC) As Boolean
        Dim startT = ap.StartDateTime.TimeOfDay
        Dim endT = ap.EndDateTime.TimeOfDay
        Return startT >= _workStart AndAlso endT <= _workEnd
    End Function

    Private Sub OnScheduleResize(sender As Object, e As EventArgs)
        If _isShuttingDown OrElse IsDisposed OrElse Disposing OrElse Not IsHandleCreated Then Return
        If _resizeTimer Is Nothing Then
            LoadAndRender()
            Return
        End If
        _resizeTimer.Stop()
        _resizeTimer.Start()
    End Sub

    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        If _resizeTimer IsNot Nothing Then _resizeTimer.Stop()
        If _isShuttingDown OrElse IsDisposed OrElse Disposing OrElse pnlBody Is Nothing Then Return
        Try
            Render()
            LayoutBodyWorkspaceExpandButton()
        Catch ex As Exception
            Debug.WriteLine($"ResizeTimer_Tick skipped due to render error: {ex.Message}")
        End Try
    End Sub

    Private Function IncludeReasonInWeekView() As Boolean
        If includeReasonCheck Is Nothing OrElse includeReasonCheck.IsDisposed Then Return True
        Return includeReasonCheck.Checked
    End Function

    ''' <summary>When include-details is on, appends non-empty Reason then Notes (each on its own line).</summary>
    Private Sub AppendReasonThenNotesIfEnabled(ByRef appointmentText As String, ap As AppointmentC)
        If ap Is Nothing OrElse Not IncludeReasonInWeekView() Then Return
        Dim r = If(ap.Reason, "").Trim()
        Dim n = If(ap.Notes, "").Trim()
        If r.Length > 0 Then appointmentText &= vbCrLf & r
        If n.Length > 0 Then appointmentText &= vbCrLf & n
    End Sub

    Private Function FormatDayCardReasonNotesInline(ap As AppointmentC) As String
        If ap Is Nothing OrElse Not IncludeReasonInWeekView() Then Return ""
        Dim r = If(ap.Reason, "").Trim()
        Dim n = If(ap.Notes, "").Trim()
        If String.IsNullOrWhiteSpace(r) AndAlso String.IsNullOrWhiteSpace(n) Then Return If(Eng, "(no reason / notes)", "(لا سبب / ملاحظات)")
        If String.IsNullOrWhiteSpace(n) Then Return r
        If String.IsNullOrWhiteSpace(r) Then Return n
        Return r & " | " & n
    End Function

    Private Function FormatDayCardReasonNotesMultiline(ap As AppointmentC) As String
        If ap Is Nothing OrElse Not IncludeReasonInWeekView() Then Return ""
        Dim r = If(ap.Reason, "").Trim()
        Dim n = If(ap.Notes, "").Trim()
        If String.IsNullOrWhiteSpace(r) AndAlso String.IsNullOrWhiteSpace(n) Then Return If(Eng, "(no reason / notes)", "(لا سبب / ملاحظات)")
        If String.IsNullOrWhiteSpace(n) Then Return r
        If String.IsNullOrWhiteSpace(r) Then Return n
        Return r & vbCrLf & n
    End Function

    Private Function BuildDoctorFlowAppointmentCardText(ap As AppointmentC, patientName As String) As String
        Dim appointmentText = $"{ap.StartDateTime.ToString(AppointmentCardTimeFormatString())}-{ap.EndDateTime.ToString(AppointmentCardTimeFormatString())}" & vbCrLf & patientName
        AppendReasonThenNotesIfEnabled(appointmentText, ap)
        Return appointmentText
    End Function

    Private Function FormatMonthPreviewReasonNotes(ap As AppointmentC) As String
        If ap Is Nothing OrElse Not IncludeReasonInWeekView() Then Return ""
        Dim r = If(ap.Reason, "").Trim()
        Dim n = If(ap.Notes, "").Trim()
        If String.IsNullOrWhiteSpace(r) AndAlso String.IsNullOrWhiteSpace(n) Then Return If(Eng, "(no reason / notes)", "(لا سبب / ملاحظات)")
        If String.IsNullOrWhiteSpace(n) Then Return r
        If String.IsNullOrWhiteSpace(r) Then Return n
        Return r & " · " & n
    End Function

    Private Function FormatMonthCellFirstAppointmentText(ap As AppointmentC, patientName As String, doctorName As String) As String
        If Not IncludeReasonInWeekView() Then Return $"{patientName} ({doctorName})"
        Dim r = If(ap.Reason, "").Trim()
        Dim n = If(ap.Notes, "").Trim()
        If String.IsNullOrWhiteSpace(r) AndAlso String.IsNullOrWhiteSpace(n) Then Return $"{If(Eng, "—", "—")} ({doctorName})"
        Dim body As String
        If String.IsNullOrWhiteSpace(n) Then
            body = r
        ElseIf String.IsNullOrWhiteSpace(r) Then
            body = n
        Else
            body = r & vbCrLf & n
        End If
        Return $"{body} ({doctorName})"
    End Function

    Private Function GetAppointmentFont() As Font
        Dim isBold = If(boldFontCheck Is Nothing OrElse boldFontCheck.IsDisposed, True, boldFontCheck.Checked)
        Dim useSize10 = If(sizeFontCheck Is Nothing OrElse sizeFontCheck.IsDisposed, True, sizeFontCheck.Checked)
        Dim fontStyle As FontStyle = If(isBold, FontStyle.Bold, FontStyle.Regular)
        Dim fontSize As Single = If(useSize10, 10.0F, 8.0F)
        Dim key = $"{If(isBold, "B", "R")}-{fontSize}"
        If _appointmentFont Is Nothing OrElse _appointmentFontKey <> key Then
            If _appointmentFont IsNot Nothing Then
                _appointmentFont.Dispose()
            End If
            _appointmentFont = New Font("Calibri", fontSize, fontStyle)
            _appointmentFontKey = key
        End If
        Return _appointmentFont
    End Function

    Private Function TrySaveAppointmentTransactional(appt As AppointmentC, isNew As Boolean, overlapHeaderEn As String, overlapHeaderAr As String, Optional reminderMessageEnglish As Boolean? = Nothing) As Boolean
        Try
            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open()
                Using trans As SqlTransaction = conn.BeginTransaction(System.Data.IsolationLevel.Serializable)
                    Dim excludeId As Integer? = If(isNew, CType(Nothing, Integer?), appt.AppointmentID)
                    Dim DrID As Integer = If(appt.DrID, appt.DrID, 0)
                    Dim overlaps = _repo.GetOverlappingAppointmentsTransactional(conn, trans, DrID, appt.PatientID, appt.StartDateTime, appt.EndDateTime, excludeId)
                    If overlaps.Any() Then
                        Dim message As New StringBuilder()
                        message.AppendLine(If(Eng, overlapHeaderEn, overlapHeaderAr))
                        message.AppendLine()
                        For Each overlap In overlaps
                            message.AppendLine($"• {overlap}")
                        Next
                        MessageBox.Show(message.ToString(),
                                        If(Eng, "Overlap Detected", "تم اكتشاف تعارض"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        trans.Rollback()
                        Return False
                    End If
                    If isNew Then
                        appt.AppointmentID = _repo.InsertTransactional(conn, trans, appt)
                    Else
                        _repo.UpdateTransactional(conn, trans, appt)
                    End If
                    trans.Commit()
                    If appt.AppointmentID > 0 Then ApptTwoHourReminderQueueRepository.SyncFromAppointmentId(appt.AppointmentID, reminderMessageEnglish)
                    RefreshAllAppointmentsCacheAndNavigationHints()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(If(Eng,
                               $"Error saving appointment: {ex.Message}",
                               $"خطأ في حفظ الموعد: {ex.Message}"),
                            If(Eng, "Error", "خطأ"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

#End Region ' ═══ END RENDER DISPATCHER & SHARED HELPERS ═══

#Region "═══ Week navigation, edge hints, slide (single week view) ═══"

    Private Shared Function GetStatusText(appt As AppointmentC) As String
        If appt Is Nothing Then Return ""
        Return ApptTheme.TranslateAppointmentStatus(If(appt.Status, ""))
    End Function

    Private Function SchedulerDoctorColor(drId As Integer) As Color
        Try
            Dim colorString As String = _repo.GetDoctorColor(drId)
            If Not String.IsNullOrWhiteSpace(colorString) Then Return ColorTranslator.FromHtml(colorString)
        Catch
        End Try
        Dim hue As Double = (drId * 37) Mod 360
        Return ColorFromHSV(hue, 0.25, 0.95)
    End Function

    Friend Function SnapshotGdi_DayViewBitmap() As Bitmap
        Return Nothing
    End Function

    Friend Function SnapshotGdi_DoctorsDayBitmap() As Bitmap
        Return Nothing
    End Function

    Private Sub ApplyPatientHintsForEmptyState(referenceDate As DateTime)
        _renderDay = referenceDate.Date
        If Not FilterPatientId.HasValue Then
            _dayAppointmentCForHints = New List(Of AppointmentC)()
            _weekAppointmentCForHints = New List(Of AppointmentC)()
            _monthAppointmentCForHints = New List(Of AppointmentC)()
            _tlAppointmentCForHints = New List(Of AppointmentC)()
            Return
        End If
        Dim patientAppts = _repo.GetByPatientId(FilterPatientId.Value)
        If patientAppts Is Nothing Then patientAppts = New List(Of AppointmentC)()
        If Not String.IsNullOrWhiteSpace(FilterStatus) Then
            patientAppts = patientAppts.Where(Function(a) a.Status = FilterStatus).ToList()
        End If
        If Not String.IsNullOrWhiteSpace(FilterReason) Then
            Dim r As String = FilterReason.Trim()
            patientAppts = patientAppts.Where(Function(a) a.Reason IsNot Nothing AndAlso a.Reason.IndexOf(r, StringComparison.OrdinalIgnoreCase) >= 0).ToList()
        End If
        _dayAppointmentCForHints = patientAppts
        _weekAppointmentCForHints = patientAppts
        _monthAppointmentCForHints = patientAppts
        _tlAppointmentCForHints = patientAppts
    End Sub

    Private Sub NavigateWithSlide(direction As Integer, changeTarget As Action)
        If changeTarget Is Nothing Then Return
        If _slideAnimating Then
            changeTarget()
            LoadAndRender()
            Return
        End If
        changeTarget()
        LoadAndRender()
    End Sub

    Private Sub LoadAndRenderWithViewTransition(previousView As ViewMode, Optional historyDirection As Integer? = Nothing)
        If previousView = _view Then
            LoadAndRender()
            Return
        End If
        Dim bodyContentHost = SchedulerBodyContentHost()
        If _slideAnimating OrElse pnlBody Is Nothing OrElse Not pnlBody.IsHandleCreated OrElse
           bodyContentHost Is Nothing OrElse bodyContentHost.Controls.Count = 0 OrElse
           pnlBody.Width <= 0 OrElse pnlBody.Height <= 0 Then
            LoadAndRender()
            Return
        End If

        Dim oldBmp = CapturePanelBitmap(pnlBody)
        LoadAndRender()
        Dim newBmp = CapturePanelBitmap(pnlBody)

        If oldBmp Is Nothing OrElse newBmp Is Nothing Then
            If oldBmp IsNot Nothing Then oldBmp.Dispose()
            If newBmp IsNot Nothing Then newBmp.Dispose()
            Return
        End If

        Dim direction As Integer = If(historyDirection.HasValue, historyDirection.Value,
            If(ComboIndexFromViewMode(_view) >= ComboIndexFromViewMode(previousView), 1, -1))
        StartSlideAnimation(oldBmp, newBmp, direction)
    End Sub

    Private Function CapturePanelBitmap(panel As PanelControl) As Bitmap
        If panel Is Nothing OrElse panel.Width <= 0 OrElse panel.Height <= 0 Then Return Nothing
        Dim bmp As New Bitmap(panel.Width, panel.Height)
        Try
            panel.DrawToBitmap(bmp, New Rectangle(0, 0, panel.Width, panel.Height))
        Catch
            bmp.Dispose()
            Return Nothing
        End Try
        Return bmp
    End Function

    Private Sub InitializeWeekSnapshotButton()
        If btnWeekSnapshot Is Nothing OrElse btnWeekSnapshot.IsDisposed Then Return
        EnsureSchedulerTransientUiHelpers()
        btnWeekSnapshot.Text = If(Eng, "Snapshot", "لقطة")
        Dim tip = If(Eng,
                      "Capture the current schedule view as an image (scroll areas expanded where supported).",
                      "التقاط صورة للعرض الحالي (مع توسيع المناطق القابلة للتمرير حيثما أمكن).")
        _weekSnapshotToolTip.SetToolTip(btnWeekSnapshot, tip)
    End Sub

    Private Sub StartSlideAnimation(oldBmp As Bitmap, newBmp As Bitmap, direction As Integer)
        _slideAnimating = True
        _slideDirection = If(direction < 0, -1, 1)
        _slideStartTime = DateTime.Now

        If _slidePanel Is Nothing Then
            _slidePanel = New Panel With {.Visible = False}
            _slideFrom = New PictureBox With {.SizeMode = PictureBoxSizeMode.StretchImage}
            _slideTo = New PictureBox With {.SizeMode = PictureBoxSizeMode.StretchImage}
            _slidePanel.Controls.Add(_slideFrom)
            _slidePanel.Controls.Add(_slideTo)
        End If

        Dim host = pnlBody.Parent
        If host Is Nothing Then _slideAnimating = False : Return

        _slidePanel.Bounds = pnlBody.Bounds
        _slidePanel.BackColor = pnlBody.BackColor
        _slideFrom.Image = oldBmp
        _slideTo.Image = newBmp
        _slideFrom.Bounds = New Rectangle(0, 0, _slidePanel.Width, _slidePanel.Height)
        _slideTo.Bounds = New Rectangle(_slideDirection * _slidePanel.Width, 0, _slidePanel.Width, _slidePanel.Height)

        If _slidePanel.Parent IsNot host Then host.Controls.Add(_slidePanel)
        _slidePanel.BringToFront()
        _slidePanel.Visible = True
        pnlBody.Visible = False

        If _slideTimer Is Nothing Then
            _slideTimer = New System.Windows.Forms.Timer() With {.Interval = 15}
            AddHandler _slideTimer.Tick, AddressOf SlideTimer_Tick
        End If
        _slideTimer.Stop()
        _slideTimer.Start()
    End Sub

    Private Sub SlideTimer_Tick(sender As Object, e As EventArgs)
        If _slidePanel Is Nothing OrElse _slideFrom Is Nothing OrElse _slideTo Is Nothing Then Return
        Dim elapsed = (DateTime.Now - _slideStartTime).TotalMilliseconds
        Dim t = Math.Min(1.0, elapsed / _slideDurationMs)
        Dim eased = 1 - Math.Pow(1 - t, 3)
        Dim w = _slidePanel.Width
        Dim offset = CInt(w * eased)

        If _slideDirection > 0 Then
            _slideFrom.Left = -offset
            _slideTo.Left = w - offset
        Else
            _slideFrom.Left = offset
            _slideTo.Left = -w + offset
        End If

        If t >= 1 Then
            _slideTimer.Stop()
            _slidePanel.Visible = False
            pnlBody.Visible = True
            If _slideFrom.Image IsNot Nothing Then _slideFrom.Image.Dispose()
            If _slideTo.Image IsNot Nothing Then _slideTo.Image.Dispose()
            _slideFrom.Image = Nothing
            _slideTo.Image = Nothing
            _slideAnimating = False
        End If
    End Sub

    Private Function GetWeekStartSaturday(dateValue As DateTime) As DateTime
        Dim currentDayOfWeek As Integer = CInt(dateValue.DayOfWeek)
        Dim daysToSaturday As Integer = (currentDayOfWeek - 6 + 7) Mod 7
        Return dateValue.Date.AddDays(-daysToSaturday)
    End Function

    Private Function GetWeekStartSunday(dateValue As DateTime) As DateTime
        Dim dow As Integer = CInt(dateValue.DayOfWeek)
        Return dateValue.Date.AddDays(-dow)
    End Function

    Private Sub EnsureWeekEdgeHints()
        InvalidatePnlBodyEdgeHintsAfterClear()
        If _weekPrevHint Is Nothing Then
            _weekPrevHint = New ArrowLable With {
                .AutoSize = False,
                .Width = BodyEdgeHintWidth,
                .Height = BodyEdgeHintHeight,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Text = If(Eng, "PREV", "السابق"),
                .Visible = False,
                .Cursor = Cursors.Hand,
                .ShowLeftTriangle = True,
                .ShowRightTriangle = False,
                .TextDirection = ArrowLable.ArrowTextDirection.TopToBottom,
                .GlassBaseColor = Color.FromArgb(80, 210, 180),
                .GlassAccentColor = Color.FromArgb(120, 90, 230),
                .TriangleColor = Color.FromArgb(235, 255, 200, 90),
                .ForeColor = Color.White
            }
            AddHandler _weekPrevHint.Click,
                Sub()
                    If _weekPrevTargetStart.HasValue Then
                        NavigateWithSlide(-1, Sub() _currentDate = _weekPrevTargetStart.Value.Date)
                    End If
                End Sub
        End If
        If _bodyPrevHintHost IsNot Nothing AndAlso _weekPrevHint.Parent IsNot _bodyPrevHintHost Then
            If _weekPrevHint.Parent IsNot Nothing Then _weekPrevHint.Parent.Controls.Remove(_weekPrevHint)
            _bodyPrevHintHost.Controls.Add(_weekPrevHint)
            _weekPrevHint.BringToFront()
        End If
        If _weekNextHint Is Nothing Then
            _weekNextHint = New ArrowLable With {
                .AutoSize = False,
                .Width = BodyEdgeHintWidth,
                .Height = BodyEdgeHintHeight,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Text = If(Eng, "NEXT", "التالي"),
                .Visible = False,
                .Cursor = Cursors.Hand,
                .ShowLeftTriangle = False,
                .ShowRightTriangle = True,
                .TextDirection = ArrowLable.ArrowTextDirection.TopToBottom,
                .GlassBaseColor = Color.FromArgb(80, 210, 180),
                .GlassAccentColor = Color.FromArgb(120, 90, 230),
                .TriangleColor = Color.FromArgb(235, 255, 200, 90),
                .ForeColor = Color.White
            }
            AddHandler _weekNextHint.Click,
                Sub()
                    If _weekNextTargetStart.HasValue Then
                        NavigateWithSlide(1, Sub() _currentDate = _weekNextTargetStart.Value.Date)
                    End If
                End Sub
        End If
        If _bodyNextHintHost IsNot Nothing AndAlso _weekNextHint.Parent IsNot _bodyNextHintHost Then
            If _weekNextHint.Parent IsNot Nothing Then _weekNextHint.Parent.Controls.Remove(_weekNextHint)
            _bodyNextHintHost.Controls.Add(_weekNextHint)
            _weekNextHint.BringToFront()
        End If
        If _weekPrevHint IsNot Nothing AndAlso _weekNextHint IsNot Nothing Then ArrowLable.ApplyReadingOrderToEdgePair(_weekPrevHint, _weekNextHint, Eng)
        PositionWeekEdgeHints()
    End Sub

    Private Sub PositionWeekEdgeHints()
        If _weekPrevHint Is Nothing OrElse _weekNextHint Is Nothing Then Return
        If _bodyPrevHintHost Is Nothing OrElse _bodyNextHintHost Is Nothing Then Return
        Dim prevX = Math.Max(0, (_bodyPrevHintHost.ClientSize.Width - _weekPrevHint.Width) \ 2)
        Dim prevY = Math.Max(0, (_bodyPrevHintHost.ClientSize.Height - _weekPrevHint.Height) \ 2)
        Dim nextX = Math.Max(0, (_bodyNextHintHost.ClientSize.Width - _weekNextHint.Width) \ 2)
        Dim nextY = Math.Max(0, (_bodyNextHintHost.ClientSize.Height - _weekNextHint.Height) \ 2)
        _weekPrevHint.Location = New Point(prevX, prevY)
        _weekNextHint.Location = New Point(nextX, nextY)
        _weekPrevHint.BringToFront()
        _weekNextHint.BringToFront()
    End Sub

    Private Sub UpdateWeekEdgeHints(currentWeekStart As DateTime, daysInWeek As Integer)
        EnsureWeekEdgeHints()
        If _weekPrevHint Is Nothing OrElse _weekNextHint Is Nothing Then Return
        _weekPrevHint.Visible = False
        _weekNextHint.Visible = False
        If _weekAppointmentCForHints Is Nothing OrElse _weekAppointmentCForHints.Count = 0 Then
            PositionWeekEdgeHints()
            Return
        End If
        _weekPrevHint.Text = If(Eng, "PREV", "السابق")
        _weekNextHint.Text = If(Eng, "NEXT", "التالي")
        ArrowLable.ApplyReadingOrderToEdgePair(_weekPrevHint, _weekNextHint, Eng)
        Dim currentStart = currentWeekStart.Date
        Dim endExclusive = currentStart.AddDays(daysInWeek)
        Dim hintDays = _weekAppointmentCForHints.Select(Function(ap) ap.StartDateTime.Date)
        Dim prevDate = hintDays.Where(Function(d) d < currentStart).OrderByDescending(Function(d) d).FirstOrDefault()
        Dim nextDate = hintDays.Where(Function(d) d >= endExclusive).OrderBy(Function(d) d).FirstOrDefault()
        _weekPrevTargetStart = If(prevDate <> Date.MinValue, GetWeekStartSaturday(prevDate), CType(Nothing, DateTime?))
        Dim wkNxt = If(nextDate <> Date.MinValue, GetWeekStartSaturday(nextDate), CType(Nothing, DateTime?))
        If wkNxt.HasValue AndAlso daysInWeek = 6 AndAlso wkNxt.Value = currentStart Then
            wkNxt = currentStart.AddDays(7)
        End If
        _weekNextTargetStart = wkNxt
        _weekPrevHint.Visible = _weekPrevTargetStart.HasValue
        _weekNextHint.Visible = _weekNextTargetStart.HasValue
        PositionWeekEdgeHints()
        _weekPrevHint.BringToFront()
        _weekNextHint.BringToFront()
    End Sub

    Private Sub DragTimer_Tick(sender As Object, e As EventArgs)
        Try
            _dragTimer.Stop()
            If _dragSourceList Is Nothing Then Return
            _dragInitiated = True
            BeginDragFromList(_dragSourceList)
        Catch ex As Exception
            Debug.WriteLine("DragTimer_Tick exception: " & ex.Message)
        End Try
    End Sub

    Private Sub BeginDragFromList(lb As ListBox)
        If lb Is Nothing OrElse lb.SelectedItem Is Nothing Then Return
        Try
            Dim dataObj = lb.SelectedItem
            lb.DoDragDrop(dataObj, DragDropEffects.Move)
        Catch ex As Exception
            Debug.WriteLine("BeginDragFromList exception: " & ex.Message)
        Finally
            _dragSourceList = Nothing
            _dragInitiated = False
        End Try
    End Sub

#End Region










    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 11 — WEEK VIEW (6 + 7 Days)                                 ║
    ' ║  Renders full week (Sat-Fri) or work week (Sat-Thu).                 ║
    ' ╚══════════════════════════════════════════════════════════════════════╝





#Region "═══ 11. WEEK VIEW (6 + 7 Days) ═══"
    Private Sub RenderCurrentWeek7Days(currentDate As DateTime)
        ' Pick the color based on statuses
        Dim statusColors As New Dictionary(Of String, Color) From {
            {"Pending", Color.LightGoldenrodYellow},
            {"Running", Color.LightSkyBlue},
            {"Completed", Color.LightGreen},
            {"Canceled", Color.LightCoral},
            {"Postponed", Color.LightGray}
        }
        Dim doctorNameCache As New Dictionary(Of Integer, String)()
        Dim patientNameCache As New Dictionary(Of Integer, String)()
        Dim doctorColorCache As New Dictionary(Of Integer, Color)()
        Dim getDoctorName As Func(Of Integer, String) =
            Function(id)
                If Not doctorNameCache.ContainsKey(id) Then
                    doctorNameCache(id) = _repo.GetDoctorName(id)
                End If
                Return doctorNameCache(id)
            End Function
        Dim getPatientName As Func(Of Integer, String) =
            Function(id)
                If Not patientNameCache.ContainsKey(id) Then
                    patientNameCache(id) = _repo.GetPatientName(id)
                End If
                Return patientNameCache(id)
            End Function
        Dim getDoctorColor As Func(Of Integer, Color) =
            Function(id)
                If doctorColorCache.ContainsKey(id) Then Return doctorColorCache(id)
                Dim color As Color = Color.WhiteSmoke
                Try
                    Dim colorString = _repo.GetDoctorColor(id)
                    If Not String.IsNullOrWhiteSpace(colorString) Then
                        color = ColorTranslator.FromHtml(colorString)
                    Else
                        Dim hue As Double = (id * 37) Mod 360
                        color = ColorFromHSV(hue, 0.25, 0.95)
                    End If
                Catch
                    Dim hue As Double = (id * 37) Mod 360
                    color = ColorFromHSV(hue, 0.25, 0.95)
                End Try
                doctorColorCache(id) = color
                Return color
            End Function
        ' pnlBody cleared in Render(); do not Clear here (avoids double teardown and hint bugs).
        pnlBody.SuspendLayout()
        InitializeDragTimerThisWeek()
        If _AppointmentC Is Nothing OrElse _AppointmentC.Count = 0 Then
            Dim noDataLabel As New Label With {
        .Text = If(Eng, "No AppointmentC to display.", "لا توجد مواعيد للعرض."),
        .Dock = DockStyle.Fill,
        .Font = New Font("Segoe UI", 11, FontStyle.Italic),
        .TextAlign = ContentAlignment.MiddleCenter
    }
            AddSchedulerBodyContent(noDataLabel)
            ApplyPatientHintsForEmptyState(currentDate)
            Dim todayEmpty As DateTime = currentDate.Date
            Dim dowEmpty As Integer = CInt(todayEmpty.DayOfWeek)
            Dim daysToSatEmpty As Integer = (dowEmpty - 6 + 7) Mod 7
            Dim weekStartEmpty As DateTime = todayEmpty.AddDays(-daysToSatEmpty)
            EnsureWeekEdgeHints()
            UpdateWeekEdgeHints(weekStartEmpty, 7)
            pnlBody.ResumeLayout()
            Exit Sub
        End If

        Dim weekToolTip = SchedulerRenderToolTip()
        ' Calculate current week range (Sat-Fri) using the passed currentDate parameter
        Dim today As DateTime = currentDate.Date
        Dim currentDayOfWeek As Integer = CInt(today.DayOfWeek)

        ' Adjust to get Saturday as start of week (Saturday = 0 in our custom week)
        Dim daysToSaturday As Integer = (currentDayOfWeek - 6 + 7) Mod 7
        Dim weekStart As DateTime = today.AddDays(-daysToSaturday)
        ' Create main container
        Dim mainFlow As New FlowLayoutPanel With {
                                                .Dock = DockStyle.Fill,
                                                .AutoScroll = True,
                                                .FlowDirection = FlowDirection.TopDown,
                                                .WrapContents = False,
                                                .Padding = New Padding(8, 0, 8, 8),
                                                .BackColor = Color.White
                                                }
        mainFlow.SuspendLayout()

        ' Get unique doctors for this week
        Dim weekAppts = _AppointmentC.Where(Function(a) a.StartDateTime.Date >= weekStart AndAlso a.StartDateTime.Date < weekStart.AddDays(7)).ToList()

        ' Create week header (tight height to text)
        Dim weekHeaderText = If(Eng,
                                $"Current Week: {AppointDateFormat.FormatDateRange(weekStart, weekStart.AddDays(6))}",
                                $"الأسبوع الحالي: {AppointDateFormat.FormatDateRange(weekStart, weekStart.AddDays(6))}")
        Dim weekHeaderFont As New Font("Calibri", 12, FontStyle.Bold)
        Dim weekFlowPadH As Integer = mainFlow.Padding.Left + mainFlow.Padding.Right + 4
        Dim weekHeaderW = SchedulerBodyInnerFlowWidth(weekFlowPadH)
        Dim weekHeaderSz = MeasureSingleLineLabelSize(weekHeaderText, weekHeaderFont, weekHeaderW, 8, 4)
        Dim weekHeader As New Label With {
                            .Text = weekHeaderText,
                            .Size = weekHeaderSz,
                            .Font = weekHeaderFont,
                            .TextAlign = ContentAlignment.MiddleCenter,
                            .BackColor = Color.FromArgb(210, 235, 255),
                            .ForeColor = Color.Navy,
                            .Margin = New Padding(0)
                             }
        mainFlow.Controls.Add(weekHeader)

        Dim daysBandH As Integer = SchedulerWeekDaysBandHeight(weekHeaderSz.Height, mainFlow.Padding.Top, mainFlow.Padding.Bottom, 6)
        ' Create days container (horizontal flow for Sat-Fri)
        Dim daysFlow As New FlowLayoutPanel With {
                                                .Width = weekHeaderW,
                                                .Height = daysBandH,
                                                .AutoScroll = False,
                                                .FlowDirection = FlowDirection.LeftToRight,
                                                .WrapContents = False,
                                                .Padding = New Padding(5, 0, 5, 5),
                                                .Margin = New Padding(0),
                                                .BackColor = Color.WhiteSmoke,
                                                .AllowDrop = True
                                                }
        daysFlow.SuspendLayout()

        ' Day colors for Sat-Fri (7 days)
        Dim dayColors As Color() = {
                                    Color.FromArgb(255, 230, 230), ' Sat
                                    Color.FromArgb(255, 245, 220), ' Sun
                                    Color.FromArgb(240, 255, 230), ' Mon
                                    Color.FromArgb(230, 250, 255), ' Tue
                                    Color.FromArgb(245, 230, 255), ' Wed
                                    Color.FromArgb(255, 255, 230), ' Thu
                                    Color.FromArgb(230, 255, 240)  ' Fri
                                    }

        ' Calculate day column width with proper margins
        Dim dayColumnWidth As Integer = CInt((daysFlow.Width - 80) / 7) ' 80px for total margins
        Dim dayColumnHeight As Integer = Math.Max(80, daysBandH - daysFlow.Padding.Top - daysFlow.Padding.Bottom)

        ' Create day columns for Sat-Fri (7 days)
        For dayIndex As Integer = 0 To 6
            Dim currentDay As DateTime = weekStart.AddDays(dayIndex)
            Dim dayAppts = ApptTheme.OrderAppointmentsForDisplay(
                _AppointmentC.Where(Function(a) a.StartDateTime.Date = currentDay.Date),
                getDoctorName)

            ' Day column container - centered in daysFlow
            Dim dayColumn As New Panel With {
                                            .Width = dayColumnWidth,
                                            .Height = dayColumnHeight,
                                            .BorderStyle = BorderStyle.FixedSingle,
                                            .BackColor = Color.White,
                                            .Tag = currentDay,
                                            .AutoScroll = True, ' Vertical scrolling only
                                            .AllowDrop = True,
                                            .Margin = New Padding(5, 0, 5, 0) ' Centered margins
                                            }

            ' Day header — single line, tight height
            Dim dayHeaderText = $"{AppointDateFormat.FormatDayShortDate(currentDay)} · " &
                                            If(Eng, $"({dayAppts.Count} appt{If(dayAppts.Count <> 1, "s", "")})",
                                               $"({dayAppts.Count} موعد{If(dayAppts.Count > 10 Or dayAppts.Count = 0, "", "اً")})")
            Dim dayHeaderFont As Font = If(currentDay.Date = today,
                                          New Font("Calibri", 9.75, FontStyle.Bold Or FontStyle.Italic),
                                          New Font("Calibri", 9.75, FontStyle.Bold))
            Dim dayHeaderW = dayColumn.Width - 4
            Dim dayHeaderSz = MeasureSingleLineLabelSize(dayHeaderText, dayHeaderFont, dayHeaderW, 4, 3)
            Dim lblDay As New Label With {
                                            .Text = dayHeaderText,
                                            .Size = dayHeaderSz,
                                            .Location = New Point(2, 2),
                                            .TextAlign = ContentAlignment.MiddleCenter,
                                            .BackColor = If(currentDay.Date = today, Color.FromArgb(180, 220, 255), dayColors(dayIndex Mod dayColors.Length)),
                                            .Font = dayHeaderFont
                                        }

            dayColumn.Controls.Add(lblDay)

            ' Doctors container for this day (flush under day header; small bottom inset only)
            Dim doctorsFlow As New FlowLayoutPanel With {
                                                        .Location = New Point(5, lblDay.Bottom),
                                                        .Size = New Size(dayColumn.Width - 10, Math.Max(1, dayColumn.Height - lblDay.Bottom - 5)),
                                                        .AutoScroll = False,
                                                        .FlowDirection = FlowDirection.TopDown,
                                                        .WrapContents = False,
                                                        .BackColor = Color.Transparent,
                                                        .AutoSize = True,
                                                        .Padding = New Padding(0),
                                                        .AllowDrop = True
                                                        }
            AddHandler doctorsFlow.Click, Sub(sender, e)
                                              lblRange.Text = AppointDateFormat.FormatDateRange(weekStart, weekStart.AddDays(6))
                                              UpdateLblCountDisplay()
                                          End Sub
            ' Drag and Drop Events for Empty Day Area
            AddHandler doctorsFlow.DragEnter, Sub(sender As Object, e As DragEventArgs)
                                                  If e.Data.GetDataPresent("Appointment") OrElse e.Data.GetDataPresent("DoctorCard") Then
                                                      e.Effect = DragDropEffects.Move
                                                  Else
                                                      e.Effect = DragDropEffects.None
                                                  End If
                                              End Sub

            AddHandler doctorsFlow.DragDrop, Sub(sender As Object, e As DragEventArgs)
                                                 If e.Data.GetDataPresent("Appointment") Then
                                                     Dim appt = DirectCast(e.Data.GetData("Appointment"), AppointmentC)
                                                     Dim sourceDay = DirectCast(e.Data.GetData("SourceDay"), DateTime)
                                                     Dim sourceDoctor = DirectCast(e.Data.GetData("SourceDoctor"), Integer)
                                                     Dim targetFlow = DirectCast(sender, FlowLayoutPanel)
                                                     Dim targetDayColumn = DirectCast(targetFlow.Parent, Panel)
                                                     Dim targetDay = DirectCast(targetDayColumn.Tag, DateTime)

                                                     ' Check if moving to same day
                                                     If sourceDay.Date = targetDay.Date Then
                                                         MessageBox.Show(If(Eng,
                                                                            "Appointment is already on this day.",
                                                                            "الموعد موجود بالفعل في هذا اليوم."),
                                                                            If(Eng, "No Move Needed", "لا حاجة للنقل"),
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                         Return
                                                     End If

                                                     ' Update appointment with new day (keep same doctor and time)
                                                     Dim timeOfDay = appt.StartDateTime.TimeOfDay
                                                     Dim duration = appt.EndDateTime - appt.StartDateTime

                                                     Dim originalStart = appt.StartDateTime
                                                     Dim originalEnd = appt.EndDateTime
                                                     Dim originalAppDate = appt.AppDate

                                                     appt.AppDate = targetDay.Date
                                                     appt.StartDateTime = targetDay.Date.Add(timeOfDay)
                                                     appt.EndDateTime = appt.StartDateTime.Add(duration)
                                                     ' Doctor remains the same

                                                     If Not TrySaveAppointmentTransactional(appt, False,
                                                                                            "Cannot move appointment - overlaps detected:",
                                                                                            "لا يمكن نقل الموعد - تم اكتشاف تعارضات:") Then
                                                         appt.StartDateTime = originalStart
                                                         appt.EndDateTime = originalEnd
                                                         appt.AppDate = originalAppDate
                                                         Return
                                                     End If

                                                     ' Refresh the view
                                                     LoadAndRender()
                                                     MessageBox.Show(If(Eng,
                                                                         "Appointment moved successfully!",
                                                                         "تم نقل الموعد بنجاح!"),
                                                                         If(Eng, "Success", "نجاح"),
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 End If
                                             End Sub

            ' Group AppointmentC by doctor for this day (linked doctor first, then name asc)
            Dim byDrW7 = dayAppts.GroupBy(Function(a) a.DrID).ToDictionary(Function(g) g.Key, Function(g) g)
            Dim doctorIdsW7 = ApptTheme.OrderDoctorColumnIdsForDisplay(dayAppts.Select(Function(a) a.DrID), getDoctorName).
                Where(Function(id) byDrW7.ContainsKey(id)).ToList()

            For Each doctorId In doctorIdsW7
                Dim doctorGroup = byDrW7(doctorId)
                Dim doctorName = getDoctorName(doctorId)
                Dim doctorAppts = ApptTheme.OrderAppointmentsForDisplay(doctorGroup, getDoctorName)

                Dim labelSpacing As Integer = 4
                Dim padding As Integer = 6
                Dim appointmentFont As Font = GetAppointmentFont()

                ' Doctor card - centered in doctorsFlow with proper margins
                Dim doctorCard As New Panel With {
            .Width = doctorsFlow.Width - 10, ' Centered with 5px margin on each side
            .Height = 60,
            .BorderStyle = BorderStyle.FixedSingle,
            .BackColor = Color.White,
            .Margin = New Padding(0, 0, 0, 5),
            .Tag = New With {.DoctorId = doctorId, .Day = currentDay},
            .AllowDrop = True,
            .Font = New Font("Segoe UI", 10.75, FontStyle.Bold)
        }

                ' Get doctor color and create lighter/darker variants
                Dim doctorColor As Color = getDoctorColor(doctorId)

                ' Create color variants for labels
                Dim lighterColor As Color = ControlPaint.Light(doctorColor, 0.7F)
                Dim darkerColor As Color = ControlPaint.Dark(doctorColor, 0.3F)

                doctorCard.BackColor = doctorColor

                ' Doctor header — height from text (single line)
                Dim doctorHdrFont As New Font("Segoe UI", 10, FontStyle.Bold)
                Dim lblDoctorInnerW = doctorCard.Width - 8
                Dim lblDoctorSz = MeasureSingleLineLabelSize(doctorName, doctorHdrFont, lblDoctorInnerW, 0, 2)
                Dim lblDoctor As New Label With {
                                                .Text = doctorName,
                                                .Size = lblDoctorSz,
                                                .Location = New Point(4, 2),
                                                .TextAlign = ContentAlignment.MiddleLeft,
                                                .Font = doctorHdrFont,
                                                .BackColor = Color.Transparent,
                                                .ForeColor = GetContrastColor(doctorColor)
                                                }
                doctorCard.Controls.Add(lblDoctor)

                ' AppointmentC list - centered in card
                Dim AppointmentCPanel As New Panel With {
                                                .Location = New Point(4, lblDoctor.Bottom + 2), ' Centered with margin
                                                .Size = New Size(doctorCard.Width - 8, 1), ' Height adjusted after rendering
                                                .AutoScroll = False,
                                                .BackColor = Color.Transparent
                                                    }

                Dim yPos As Integer = 0
                For Each appointment In doctorAppts
                    Dim patientName = getPatientName(appointment.PatientID)
                    Dim appointmentText = $"{appointment.StartDateTime.ToString(AppointmentCardTimeFormatString())}-{appointment.EndDateTime.ToString(AppointmentCardTimeFormatString())}" & vbCrLf & patientName
                    AppendReasonThenNotesIfEnabled(appointmentText, appointment)

                    ' Alternate between lighter and darker colors for visual interest
                    Dim labelColor As Color = If(doctorAppts.IndexOf(appointment) Mod 2 = 0, lighterColor, darkerColor)

                    ' --- Status strip (same control/theme as day view / ApptWeekCtl) ---
                    Dim lblStatus As New ApptCardStatusLabel With {
                        .AutoSize = False,
                        .Tag = appointment,
                        .TextAlign = ContentAlignment.MiddleCenter
                    }
                    ApptTheme.StyleApptCardStatusLabelForAppointment(lblStatus, appointment, doctorColor, Nothing)
                    Dim stripW As Integer = If(lblStatus.Visible,
                        Math.Max(26, Math.Min(72, ApptTheme.MeasureApptCardStatusColumnWidth(lblStatus.Text, lblStatus.Font))),
                        0)

                    ' Appointment label - centered in AppointmentC panel
                    Dim labelWidth As Integer = Math.Max(80, AppointmentCPanel.Width - stripW - 6)
                    Dim labelHeight As Integer = MeasureWrappedLabelHeight(appointmentText, appointmentFont, labelWidth)
                    If stripW > 0 Then
                        Dim dpiYStrip As Single = 96.0F
                        Try
                            dpiYStrip = CSng(DeviceDpi)
                        Catch
                        End Try
                        Dim mhStrip = ApptCardStatusLabel.MeasureMinimumStripHeight(lblStatus.Text, lblStatus.Font, stripW, dpiYStrip)
                        labelHeight = Math.Max(labelHeight, mhStrip + 2)
                    End If
                    Dim appointmentLabel As New Label With {
                                                .Text = appointmentText,
                                                .Size = New Size(labelWidth, labelHeight), ' Leave space for status strip
                                                .Location = New Point(2, yPos), ' Centered with small margin
                                                .Font = appointmentFont,
                                                .TextAlign = ContentAlignment.TopLeft,
                                                .BorderStyle = BorderStyle.FixedSingle,
                                                .BackColor = labelColor,
                                                .ForeColor = GetContrastColor(labelColor),
                                                .Tag = appointment,
                                                .AllowDrop = False
                                            }

                    ' Enhanced Mouse Events with Timer for Drag/DoubleClick
                    AddHandler appointmentLabel.MouseDown, Sub(sender As Object, e As MouseEventArgs)
                                                               If e.Button = MouseButtons.Left Then
                                                                   _isDragOperation = False
                                                                   _dragStartLabel = DirectCast(sender, Label)
                                                                   _dragTimerThisWeek.Start()
                                                               End If
                                                           End Sub
                    AddHandler appointmentLabel.MouseUp, Sub(sender As Object, e As MouseEventArgs)
                                                             _dragTimerThisWeek.Stop()
                                                             ' Only reset if we haven't started dragging
                                                             If Not _isDragOperation Then
                                                                 _dragStartLabel = Nothing
                                                             End If
                                                         End Sub
                    AddHandler appointmentLabel.MouseMove, Sub(sender As Object, e As MouseEventArgs)
                                                               If e.Button = MouseButtons.Left AndAlso _isDragOperation Then
                                                                   _dragTimerThisWeek.Stop()
                                                                   StartLabelDrag(DirectCast(sender, Label))
                                                               End If
                                                           End Sub
                    ' Clean double-click handler without drag interference
                    AddHandler appointmentLabel.DoubleClick, Sub(sender As Object, e As EventArgs)
                                                                 _dragTimerThisWeek.Stop()
                                                                 _isDragOperation = False
                                                                 _dragStartLabel = Nothing
                                                                 Dim lbl = DirectCast(sender, Label)
                                                                 Dim appt = DirectCast(lbl.Tag, AppointmentC)
                                                                 _currentDate = appt.StartDateTime.Date
                                                                 _pendingDayViewDoctorFilter = appt.DrID
                                                                 'SetView(ViewMode.DayView)
                                                             End Sub

                    AppointmentCPanel.Controls.Add(appointmentLabel)
                    ' Tooltip
                    Dim timeFormat As String = AppointmentCardTimeFormatString()
                    weekToolTip.SetToolTip(appointmentLabel, If(Eng, $"Doctor: {doctorName}{vbCrLf}Patient: {patientName}{vbCrLf}Reason: {If(appointment.Reason, "")}{vbCrLf}Notes: {If(appointment.Notes, "")}{vbCrLf}Time: {appointment.StartDateTime.ToString(timeFormat)} - {appointment.EndDateTime.ToString(timeFormat)}",
                $"الطبيب: {doctorName}{vbCrLf}المريض: {patientName}{vbCrLf}السبب: {If(appointment.Reason, "")}{vbCrLf}ملاحظات: {If(appointment.Notes, "")}{vbCrLf}الوقت: {appointment.StartDateTime.ToString(timeFormat)} - {appointment.EndDateTime.ToString(timeFormat)}"))

                    lblStatus.Width = stripW
                    lblStatus.Height = Math.Max(1, appointmentLabel.Height - 2)
                    lblStatus.Location = New Point(appointmentLabel.Right + 2, appointmentLabel.Top + 1)
                    AddHandler lblStatus.MouseUp, Sub(sender As Object, e As MouseEventArgs)
                                                      If e.Button <> MouseButtons.Right Then Return
                                                      Dim chip = DirectCast(sender, Control)
                                                      ShowAppointmentStatusContextMenu(chip, appointment, statusColors, e, chip)
                                                  End Sub
                    AppointmentCPanel.Controls.Add(lblStatus)
                    lblStatus.BringToFront()
                    yPos += labelHeight + labelSpacing
                Next
                Dim contentHeight As Integer = Math.Max(0, yPos - labelSpacing)
                AppointmentCPanel.Height = contentHeight
                doctorCard.Height = Math.Max(60, lblDoctor.Bottom + 2 + contentHeight + padding)
                doctorCard.Controls.Add(AppointmentCPanel)
                ' Enhanced Mouse Events for Doctor Card
                AddHandler doctorCard.MouseDown, Sub(sender As Object, e As MouseEventArgs)
                                                     If e.Button = MouseButtons.Left Then
                                                         _isDragOperation = False
                                                         _dragStartCard = DirectCast(sender, Panel)
                                                         _dragTimerThisWeek.Start()
                                                     End If
                                                 End Sub
                AddHandler doctorCard.MouseUp, Sub(sender As Object, e As MouseEventArgs)
                                                   _dragTimerThisWeek.Stop()
                                                   ' Only reset if we haven't started dragging
                                                   If Not _isDragOperation Then
                                                       _dragStartCard = Nothing
                                                   End If
                                               End Sub
                AddHandler doctorCard.MouseMove, Sub(sender As Object, e As MouseEventArgs)
                                                     If e.Button = MouseButtons.Left AndAlso _isDragOperation Then
                                                         _dragTimerThisWeek.Stop()
                                                         StartCardDrag(DirectCast(sender, Panel))
                                                     End If
                                                 End Sub
                ' Clean double-click handler for doctor card
                AddHandler doctorCard.DoubleClick, Sub(sender As Object, e As EventArgs)
                                                       _dragTimerThisWeek.Stop()
                                                       _isDragOperation = False
                                                       _dragStartCard = Nothing
                                                       Dim card = DirectCast(sender, Panel)
                                                       Dim tagInfo = DirectCast(card.Tag, Object)
                                                       OpenDoctorDayView(tagInfo.DoctorId, tagInfo.Day)
                                                   End Sub

                ' Drag and Drop Events for Doctor Card (Drop Target)
                AddHandler doctorCard.DragEnter, Sub(sender As Object, e As DragEventArgs)
                                                     If e.Data.GetDataPresent("Appointment") OrElse e.Data.GetDataPresent("DoctorCard") Then
                                                         e.Effect = DragDropEffects.Move
                                                     Else
                                                         e.Effect = DragDropEffects.None
                                                     End If
                                                 End Sub
                AddHandler doctorCard.DragDrop, Sub(sender As Object, e As DragEventArgs)
                                                    If e.Data.GetDataPresent("Appointment") Then
                                                        Dim appt = DirectCast(e.Data.GetData("Appointment"), AppointmentC)
                                                        Dim sourceDay = DirectCast(e.Data.GetData("SourceDay"), DateTime)
                                                        Dim sourceDoctor = DirectCast(e.Data.GetData("SourceDoctor"), Integer)
                                                        Dim targetCard = DirectCast(sender, Panel)
                                                        Dim tagInfo = DirectCast(targetCard.Tag, Object)
                                                        Dim targetDay = tagInfo.Day
                                                        Dim targetDoctor = tagInfo.DoctorId

                                                        ' Check if moving to same day and doctor
                                                        If sourceDay.Date = targetDay.Date AndAlso sourceDoctor = targetDoctor Then
                                                            MessageBox.Show(If(Eng,
                                                                            "Appointment is already on this day.",
                                                                            "الموعد موجود بالفعل في هذا اليوم."),
                                                                            If(Eng, "No Move Needed", "لا حاجة للنقل"),
                                                                            MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                            Return
                                                        End If

                                                        ' Update appointment with new day and doctor
                                                        Dim timeOfDay = appt.StartDateTime.TimeOfDay
                                                        Dim duration = appt.EndDateTime - appt.StartDateTime

                                                        Dim originalStart = appt.StartDateTime
                                                        Dim originalEnd = appt.EndDateTime
                                                        Dim originalAppDate = appt.AppDate
                                                        Dim originalDoctor = appt.DrID

                                                        appt.AppDate = targetDay.Date
                                                        appt.StartDateTime = targetDay.Date.Add(timeOfDay)
                                                        appt.EndDateTime = appt.StartDateTime.Add(duration)
                                                        appt.DrID = targetDoctor

                                                        If Not TrySaveAppointmentTransactional(appt, False,
                                                                                               "Cannot move appointment - overlaps detected:",
                                                                                               "لا يمكن نقل الموعد - تم اكتشاف تعارضات:") Then
                                                            appt.StartDateTime = originalStart
                                                            appt.EndDateTime = originalEnd
                                                            appt.AppDate = originalAppDate
                                                            appt.DrID = originalDoctor
                                                            Return
                                                        End If

                                                        ' Refresh the view
                                                        LoadAndRender()
                                                        MessageBox.Show(If(Eng,
                                                                         "Appointment moved successfully!",
                                                                         "تم نقل الموعد بنجاح!"),
                                                                         If(Eng, "Success", "نجاح"),
                                                                         MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                    End If
                                                End Sub

                doctorsFlow.Controls.Add(doctorCard)
            Next

            ' If no AppointmentC for this day, show message
            If doctorIdsW7.Count = 0 Then
                Dim noApptsLabel As New Label With {
            .Text = If(Eng, "No AppointmentC", "لا توجد مواعيد"),
            .Size = New Size(doctorsFlow.Width - 10, 30), ' Centered with margin
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = New Font("Segoe UI", 8, FontStyle.Italic),
            .ForeColor = Color.Gray
        }
                doctorsFlow.Controls.Add(noApptsLabel)
            End If

            dayColumn.Controls.Add(doctorsFlow)
            daysFlow.Controls.Add(dayColumn)
        Next

        mainFlow.Controls.Add(daysFlow)
        AddSchedulerBodyContent(mainFlow)
        daysFlow.ResumeLayout()
        mainFlow.ResumeLayout()
        pnlBody.ResumeLayout()
        UpdateLblCountDisplay()
        'pnlBody.ResumeLayout()
        mainFlow.BringToFront()
        EnsureWeekEdgeHints()
        UpdateWeekEdgeHints(weekStart, 7)
    End Sub


    ''' <param name="statusLabel">When set (week/day status chip), updates label in place; otherwise refreshes via LoadAndRender (month views).</param>
    Private Sub ShowAppointmentStatusContextMenu(host As Control, appt As AppointmentC, statusColors As Dictionary(Of String, Color), e As MouseEventArgs, Optional statusLabel As Control = Nothing)
        If e.Button <> MouseButtons.Right Then Return
        If appt Is Nothing OrElse host Is Nothing Then Return
        EnsureSchedulerTransientUiHelpers()

        _statusContextMenuAppointment = appt
        _statusContextMenuLabel = statusLabel
        _statusContextMenuColors = statusColors
        _statusContextMenu.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        _statusContextMenu.Font = _statusContextMenuFont
        _statusContextMenu.Items.Clear()

        Dim editItem = New ToolStripMenuItem(If(Eng, "Edit appointment...", "تعديل الموعد...")) With {
            .Font = _statusContextMenuFont,
            .Tag = "__edit__"
        }
        AddHandler editItem.Click, AddressOf StatusContextMenuItem_Click
        _statusContextMenu.Items.Add(editItem)
        _statusContextMenu.Items.Add(New ToolStripSeparator())

        For Each kvp In statusColors
            Dim menuItem = New ToolStripMenuItem(ApptTheme.TranslateAppointmentStatus(kvp.Key)) With {
                .Font = _statusContextMenuFont,
                .Tag = kvp.Key
            }
            AddHandler menuItem.Click, AddressOf StatusContextMenuItem_Click
            _statusContextMenu.Items.Add(menuItem)
        Next

        _statusContextMenu.Show(host, e.Location)
    End Sub

    Private Sub StatusContextMenuItem_Click(sender As Object, e As EventArgs)
        Dim item = TryCast(sender, ToolStripMenuItem)
        If item Is Nothing OrElse _statusContextMenuAppointment Is Nothing Then Return

        Dim command = Convert.ToString(item.Tag)
        If String.Equals(command, "__edit__", StringComparison.Ordinal) Then
            OpenAppointmentEditor(_statusContextMenuAppointment, False)
            Return
        End If

        If _statusContextMenuColors Is Nothing OrElse Not _statusContextMenuColors.ContainsKey(command) Then Return
        UpdateAppointmentCtatus(_statusContextMenuAppointment, command, _statusContextMenuColors(command), _statusContextMenuLabel)
    End Sub

    Private Sub UpdateAppointmentCtatus(appt As AppointmentC, newStatus As String, color As Color, Optional statusChip As Control = Nothing)
        appt.Status = newStatus
        If statusChip IsNot Nothing Then
            Dim strip = TryCast(statusChip, ApptCardStatusLabel)
            If strip IsNot Nothing Then
                ApptTheme.StyleApptCardStatusLabelForAppointment(strip, appt, SchedulerDoctorColor(appt.DrID), Nothing)
                strip.Width = If(strip.Visible,
                    Math.Max(26, Math.Min(72, ApptTheme.MeasureApptCardStatusColumnWidth(strip.Text, strip.Font))),
                    0)
            Else
                Dim lbl = TryCast(statusChip, Label)
                If lbl IsNot Nothing Then
                    lbl.Text = GetStatusText(appt)
                    lbl.BackColor = color
                End If
            End If
        End If

        Try
            Using conn As SqlConnection = DentistXDATA.GetConnection
                _repo.Update(appt)
            End Using
            Dim statusMsg = If(Eng, newStatus, ApptTheme.TranslateAppointmentStatus(newStatus))
            MessageBox.Show(If(Eng,
                            $"Status updated to {newStatus}",
                            $"تم تحديث الحالة إلى {statusMsg}"),
                            If(Eng, "Success", "نجاح"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            If statusChip Is Nothing Then
                InvalidateFullAppointmentCache()
                LoadAndRender()
            End If
        Catch ex As Exception
            MessageBox.Show(If(Eng,
                            $"Error: {ex.Message}",
                            $"خطأ: {ex.Message}"),
                            If(Eng, "Error", "خطأ"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region ' ═══ END WEEK VIEW ═══




    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 14 — NAVIGATION BUTTONS                                     ║
    ' ║  Prev/Next, Today, View combo, GoTo date, Add button handlers.       ║
    ' ╚══════════════════════════════════════════════════════════════════════╝



#Region "═══ 14. NAVIGATION BUTTONS ═══"
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim prefilledPatientId As Integer = 0
        If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID > 0 Then
            prefilledPatientId = CurrentPatient.PatientID
        ElseIf Not AllowAddWithoutCurrentPatient Then
            MessageBox.Show(If(Eng, "Please select a patient first.", "يرجى اختيار مريض أولاً."), If(Eng, "Info", "معلومات"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim day0 = _currentDate.Date
        Dim newAppt As New AppointmentC With {
            .PatientID = prefilledPatientId,
            .DrID = 1,
            .AppDate = day0,
            .StartDateTime = day0.AddHours(9),
            .EndDateTime = day0.AddHours(9).AddMinutes(30),
            .Notes = "",
            .CreatedAt = DateTime.Now
        }
        ' setAppt:=True so editor does NOT insert; AddAppointment does overlap check + single insert
        Dim dlg = New AppointCEditorForm(newAppt, True, True)
        If dlg.ShowDialog() = DialogResult.OK Then
            AddAppointment(dlg.AppointmentC)
        End If
    End Sub
    Private Sub BtnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        NavigateWithSlide(-1,
                          Sub()
                              Select Case _view
                                  'Case ViewMode.DayView : _currentDate = _currentDate.AddDays(-1)
                                  Case ViewMode.ThisWeekFull : _currentDate = _currentDate.AddDays(-7)
                                      'Case ViewMode.ThisWeek : _currentDate = _currentDate.AddDays(-6) ' Move back 6 days (Sat-Thu week)
                                      'Case ViewMode.MonthlyWeek : _currentDate = _currentDate.AddMonths(-1) ' _currentDate.AddDays(-7)
                                      'Case ViewMode.MonthView : _currentDate = _currentDate.AddMonths(-1)
                                      'Case ViewMode.DaysTimeline : _currentDate = _currentDate.AddDays(-7)
                                      'Case ViewMode.DoctorsDay : _currentDate = _currentDate.AddDays(-1)
                              End Select
                          End Sub)
    End Sub
    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        NavigateWithSlide(1,
                          Sub()
                              Select Case _view
                                  'Case ViewMode.DayView : _currentDate = _currentDate.AddDays(1)
                                  Case ViewMode.ThisWeekFull : _currentDate = _currentDate.AddDays(7)
                                      'Case ViewMode.ThisWeek : _currentDate = _currentDate.AddDays(6) ' Move forward 6 days (Sat-Thu week)
                                      'Case ViewMode.MonthlyWeek : _currentDate = _currentDate.AddMonths(1) ' _currentDate.AddDays(7)
                                      'Case ViewMode.MonthView : _currentDate = _currentDate.AddMonths(1)
                                      'Case ViewMode.DaysTimeline : _currentDate = _currentDate.AddDays(7)
                                      'Case ViewMode.DoctorsDay : _currentDate = _currentDate.AddDays(1)
                              End Select
                          End Sub)
    End Sub
    Private Sub BtnToday_Click(sender As Object, e As EventArgs) Handles btnToday.Click
        _currentDate = DateTime.Today

        LoadAndRender()
    End Sub
    Private Sub CmbView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbView.SelectedIndexChanged
        Dim previous = _view
        Select Case cmbView.SelectedIndex
            'Case 0
            '    _view = ViewMode.DayView
            Case 1
                _view = ViewMode.ThisWeekFull
                'Case 2
                '    _view = ViewMode.ThisWeek
                'Case 3
                '    _view = ViewMode.MonthlyWeek
                'Case 4
                '    _view = ViewMode.MonthView
                'Case 5
                '    _view = ViewMode.DaysTimeline
                'Case 6
                '    _view = ViewMode.DoctorsDay
            Case Else
                ' e.g. -1 when EditValue/Text does not match an item — do not default to month view
                If cmbView.SelectedIndex < 0 Then Return
                _view = ViewMode.ThisWeekFull
        End Select
        If Not _suppressViewNavRecording AndAlso previous <> _view Then
            _viewNavBack.Push(previous)
            _viewNavForward.Clear()
        End If
        UpdateViewNavButtonVisibility()
        If Not _suppressViewNavRecording AndAlso Not _suppressComboRenderDuringInit Then LoadAndRenderWithViewTransition(previous)
    End Sub
    Private Sub gotoDate_EditValueChanged(sender As Object, e As EventArgs) Handles gotoDate.EditValueChanged
        If _suppressGotoDateSync Then Return
        Dim selectedDate As DateTime
        If Not TryGetDateTimeFromEditorValue(gotoDate.EditValue, selectedDate) AndAlso
           Not TryGetDateTimeFromEditorValue(gotoDate.Text, selectedDate) Then
            Return
        End If
        _currentDate = selectedDate.Date
        Select Case _view
            'Case ViewMode.DayView : _currentDate = _currentDate.AddDays(0)
            Case ViewMode.ThisWeekFull : _currentDate = _currentDate.AddDays(0)
                'Case ViewMode.ThisWeek : _currentDate = _currentDate.AddDays(0) ' Move forward 6 days (Sat-Thu week)
                'Case ViewMode.MonthlyWeek : _currentDate = _currentDate.AddDays(0)
                'Case ViewMode.MonthView : _currentDate = _currentDate.AddMonths(0)
                'Case ViewMode.DaysTimeline : _currentDate = _currentDate.AddDays(0)
                'Case ViewMode.DoctorsDay : _currentDate = _currentDate.AddDays(0)
        End Select
        LoadAndRender()
    End Sub
#End Region ' ═══ END NAVIGATION BUTTONS ═══

    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 15 — CRUD OPERATIONS                                        ║
    ' ║  Open editor, Add, Update, Delete AppointmentC.                      ║
    ' ╚══════════════════════════════════════════════════════════════════════╝




#Region "═══ 15. CRUD OPERATIONS ═══"
    Private Sub OpenAppointmentEditor(appt As AppointmentC, Optional isNew As Boolean = False)
        Using frm As New AppointCEditorForm(appt, isNew, setAppt:=True)
            frm.apptDate.EditValue = appt.AppDate

            If frm.ShowDialog() = DialogResult.OK Then
                Try
                    If isNew Then
                        AddAppointment(frm.AppointmentC, frm.ReminderMessageEnglish)
                    Else
                        UpdateAppointment(frm.AppointmentC, frm.ReminderMessageEnglish)
                    End If
                Catch ex As Exception
                    MessageBox.Show("Failed to save appointment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub
    Public Sub AddAppointment(appt As AppointmentC, Optional reminderMessageEnglish As Boolean? = Nothing)
        If Not IsWithinWorkingHours(appt) Then
            MessageBox.Show("Appointment must be within working hours!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If Not TrySaveAppointmentTransactional(appt, True,
                                               "Appointment overlaps detected:",
                                               "تم اكتشاف تعارضات في الموعد:",
                                               reminderMessageEnglish) Then
            Exit Sub
        End If
        LoadAndRender()
    End Sub
    Public Sub UpdateAppointment(appt As AppointmentC, Optional reminderMessageEnglish As Boolean? = Nothing)
        If Not IsWithinWorkingHours(appt) Then
            MessageBox.Show("Appointment must be within working hours!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If Not TrySaveAppointmentTransactional(appt, False,
                                               "Appointment overlaps detected:",
                                               "تم اكتشاف تعارضات في الموعد:",
                                               reminderMessageEnglish) Then
            Exit Sub
        End If
        LoadAndRender()
    End Sub
    ''' <summary>Deletes an appointment in the DB, refreshes the view, and notifies subscribers. Does not throw to the UI thread.</summary>
    Public Sub DeleteAppointment(apptId As Integer)
        If _repo Is Nothing OrElse apptId <= 0 Then Return
        Try
            If _repo.Delete(apptId) > 0 Then
                InvalidateFullAppointmentCache()
                LoadAndRender()
                RaiseEvent AppointmentDeleted(apptId)
            Else
                MessageBox.Show(If(Eng, "The appointment could not be deleted (it may have already been removed).", "تعذر حذف الموعد."),
                                If(Eng, "Delete", "حذف"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show(If(Eng, "Could not delete appointment: " & ex.Message, "تعذر حذف الموعد: " & ex.Message),
                            If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>ListView HitTest SubItem reference may not match SubItems(i) with owner-draw; resolve by index.</summary>
    Private Shared Function TryGetClickedSubItemIndex(item As ListViewItem, clickedSubItem As ListViewItem.ListViewSubItem) As Integer
        If item Is Nothing OrElse clickedSubItem Is Nothing Then Return -1
        For i As Integer = 0 To item.SubItems.Count - 1
            If clickedSubItem Is item.SubItems(i) Then Return i
        Next
        Return -1
    End Function

    ''' <summary>Used by day appointment popup lists: delete in DB, refresh scheduler, optional extra action (e.g. close popup).</summary>
    Private Sub TryDeleteAppointmentFromDayPopup(appt As AppointmentC, Optional afterSuccess As Action = Nothing)
        If _repo Is Nothing OrElse appt Is Nothing Then Return
        Try
            If _repo.Delete(appt.AppointmentID) <= 0 Then
                MessageBox.Show(If(Eng, "The appointment could not be deleted (it may have already been removed).", "تعذر حذف الموعد."),
                                If(Eng, "Delete", "حذف"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            InvalidateFullAppointmentCache()
            LoadAndRender()
            If afterSuccess IsNot Nothing Then afterSuccess.Invoke()
        Catch ex As Exception
            MessageBox.Show(If(Eng, "Could not delete appointment: " & ex.Message, "تعذر حذف الموعد: " & ex.Message),
                            If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region ' ═══ END CRUD OPERATIONS ═══

    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 16 — UTILITY HELPERS & INNER CLASSES                        ║
    ' ║  Color conversion, text measurement, ApptItem wrapper class, etc.    ║
    ' ╚══════════════════════════════════════════════════════════════════════╝



#Region "═══ 16. UTILITY HELPERS & INNER CLASSES ═══"
    Private Function HexToColor(hex As String) As Color
        If String.IsNullOrWhiteSpace(hex) Then Return Color.LightGray
        If hex.StartsWith("#") Then hex = hex.Substring(1)
        If hex.Length = 6 Then
            Return Color.FromArgb(
            Convert.ToInt32(hex.Substring(0, 2), 16),
            Convert.ToInt32(hex.Substring(2, 2), 16),
            Convert.ToInt32(hex.Substring(4, 2), 16)
        )
        End If
        Return Color.LightGray
    End Function
    ' Simple wrapper so ListBox/drag-drop holds a friendly object and shows nice text
    Private Class ApptItem
        Public Property Ap As AppointmentC
        Public Property Text As String
        'Public Sub New(a As AppointmentC, display As String)
        '    Ap = a
        '    Text = display
        'End Sub
        'Public Overrides Function ToString() As String
        '    Return Text
        'End Function

        Public Property Appointment As AppointmentC
        Public Property Display As String
        Public Property Status As String
        Public Sub New(appt As AppointmentC, displayText As String)
            Ap = appt
            Appointment = appt
            Display = displayText
            Text = displayText
            Status = GetStatusText(appt)
        End Sub
        Public Overrides Function ToString() As String
            Return Display
        End Function
    End Class
#End Region ' ═══ END UTILITY HELPERS & INNER CLASSES ═══

    ' ╔══════════════════════════════════════════════════════════════════════╗
    ' ║  SECTION 17 — WEEK VIEW DRAG & DROP + ALTERNATE RENDERERS            ║
    ' ║  Drag timer, label/card drag, 6-day alternate renderer,              ║
    ' ║  OpenDoctorDayView, card event helpers, and dispose cleanup.         ║
    ' ╚══════════════════════════════════════════════════════════════════════╝




#Region "═══ 17. WEEK VIEW DRAG & DROP ═══"
    Private Sub InitializeDragTimerThisWeek()
        If _isShuttingDown OrElse IsDisposed OrElse Disposing Then Return
        If _dragTimerThisWeek Is Nothing Then
            _dragTimerThisWeek = New Timer()
        End If
        Try
            _dragTimerThisWeek.Interval = SystemInformation.DoubleClickTime ' Use system double-click time
            _dragTimerThisWeek.Stop()
            RemoveHandler _dragTimerThisWeek.Tick, AddressOf DragTimer_TickThisWeek
            AddHandler _dragTimerThisWeek.Tick, AddressOf DragTimer_TickThisWeek
        Catch ex As ObjectDisposedException
            _dragTimerThisWeek = New Timer() With {.Interval = SystemInformation.DoubleClickTime}
            RemoveHandler _dragTimerThisWeek.Tick, AddressOf DragTimer_TickThisWeek
            AddHandler _dragTimerThisWeek.Tick, AddressOf DragTimer_TickThisWeek
        End Try
    End Sub
    Private Sub DragTimer_TickThisWeek(sender As Object, e As EventArgs)
        If _isShuttingDown OrElse IsDisposed OrElse Disposing Then Return
        If _dragTimerThisWeek Is Nothing Then Return


        _dragTimerThisWeek.Stop()

        ' Set the flag to indicate this is a drag operation
        _isDragOperation = True

        ' Start drag operation
        If _dragStartLabel IsNot Nothing Then
            StartLabelDrag(_dragStartLabel)
        ElseIf _dragStartCard IsNot Nothing Then
            StartCardDrag(_dragStartCard)
        End If

        ' Clear the old list-based drag variables (they're from different functionality)
        _dragSourceList = Nothing
        _dragInitiated = False
    End Sub
    Private Sub StartLabelDrag(label As Label)
        Dim appt = DirectCast(label.Tag, AppointmentC)
        Dim doctorCard = DirectCast(label.Parent.Parent, Panel) ' Label -> AppointmentCPanel -> doctorCard
        Dim tagInfo = DirectCast(doctorCard.Tag, Object)
        Dim currentDay = tagInfo.Day
        Dim doctorId = tagInfo.DoctorId

        ' Store drag data
        Dim dragData As New DataObject()
        dragData.SetData("Appointment", appt)
        dragData.SetData("SourceDay", currentDay)
        dragData.SetData("SourceDoctor", doctorId)

        ' Start the drag operation
        label.DoDragDrop(dragData, DragDropEffects.Move)

        ' Clean up after drag
        _isDragOperation = False
        _dragStartLabel = Nothing
    End Sub
    Private Sub StartCardDrag(card As Panel)
        Dim tagInfo = DirectCast(card.Tag, Object)
        Dim doctorId = tagInfo.DoctorId
        Dim currentDay = tagInfo.Day

        ' Create a dummy appointment for card drag (or use first appointment if any)
        Dim dragData As New DataObject()
        dragData.SetData("DoctorCard", True)
        dragData.SetData("DoctorId", doctorId)
        dragData.SetData("Day", currentDay)

        ' Start the drag operation
        card.DoDragDrop(dragData, DragDropEffects.Move)

        ' Clean up after drag
        _isDragOperation = False
        _dragStartCard = Nothing
    End Sub
    Private Sub OpenDoctorDayView(doctorId As Integer, day As DateTime)
        Dim appts As New List(Of AppointmentC)()
        If _AppointmentC IsNot Nothing Then
            appts.AddRange(_AppointmentC.Where(Function(a) a IsNot Nothing AndAlso a.DrID = doctorId AndAlso a.StartDateTime.Date = day.Date))
        End If
        appts = appts.OrderBy(Function(a) a.StartDateTime).ToList()

        Dim dlg As New DevExpress.XtraEditors.XtraForm With {
            .Text = If(Eng,
                $"Day — {_repo.GetDoctorName(doctorId)} — {AppointDateFormat.FormatDate(day)}",
                $"يوم — {_repo.GetDoctorName(doctorId)} — {AppointDateFormat.FormatDate(day)}"),
            .Size = New Size(640, 480),
            .StartPosition = FormStartPosition.CenterParent,
            .FormBorderStyle = FormBorderStyle.SizableToolWindow}

        Dim list As New ListBox With {
            .Dock = DockStyle.Fill,
            .IntegralHeight = False,
            .Font = New Font("Segoe UI", 10.0F)
        }
        list.DisplayMember = "Display"
        For Each ap In appts
            Dim pname = _repo.GetPatientName(ap.PatientID)
            Dim line = $"{ap.StartDateTime:t} — {ap.EndDateTime:t}  ({pname})"
            list.Items.Add(New ApptItem(ap, line))
        Next

        AddHandler list.DoubleClick,
            Sub()
                Dim ai = TryCast(list.SelectedItem, ApptItem)
                If ai Is Nothing Then Return
                OpenAppointmentEditor(ai.Appointment, False)
            End Sub

        Dim hint As New Label With {
            .Dock = DockStyle.Top,
            .Height = 32,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Padding = New Padding(8, 8, 8, 0),
            .Text = If(Eng, "Double-click an appointment to edit.", "انقر نقرتين لتعديل موعد.")
        }

        dlg.Controls.Add(hint)
        dlg.Controls.Add(list)
        dlg.ShowDialog(Me)
    End Sub
    ' Helper function to ensure text is readable on colored backgrounds
    Private Function GetContrastColor(backColor As Color) As Color
        Dim luminance As Double = (0.299 * backColor.R + 0.587 * backColor.G + 0.114 * backColor.B) / 255
        Return If(luminance > 0.5, Color.Black, Color.White)
    End Function
#End Region ' ═══ END WEEK VIEW DRAG & DROP ═══




    ' Attach click/double-click handlers to the card and all child controls (recursive).
    Private Sub AttachCardEventHandlers(card As Panel)
        ' Main handlers on the card itself (MouseClick + MouseDoubleClick are reliable)
        AddHandler card.MouseClick, Sub(s As Object, e As MouseEventArgs)
                                        OnCardClicked(card, e)
                                    End Sub
        AddHandler card.MouseDoubleClick, Sub(s As Object, e As MouseEventArgs)
                                              OnCardDoubleClicked(card, e)
                                          End Sub

        ' Ensure child controls forward clicks/double-clicks to the parent card
        ForwardChildClicksToParent(card)
    End Sub

    Private Sub ForwardChildClicksToParent(parent As Control)
        For Each c As Control In parent.Controls
            ' If child is not clickable or we want to allow some control to keep its behavior, you can skip it here.
            ' Use Mouse events (more reliable than Click) and forward them.
            AddHandler c.MouseClick, Sub(s As Object, e As MouseEventArgs)
                                         OnCardClicked(parent, e)
                                     End Sub
            AddHandler c.MouseDoubleClick, Sub(s As Object, e As MouseEventArgs)
                                               OnCardDoubleClicked(parent, e)
                                           End Sub

            ' If the child itself contains other controls, recurse
            If c.HasChildren Then
                ForwardChildClicksToParent(c)
            End If
        Next
    End Sub

    ' Centralized handlers that raise your public events
    Private Sub OnCardClicked(card As Panel, e As MouseEventArgs)
        Try
            ' optional: visual feedback on click
            card.Focus()
            ' Raise the public event with the AppointmentC object stored in Tag
            Dim ap = TryCast(card.Tag, AppointmentC)
            If ap IsNot Nothing Then
                RaiseEvent AppointmentClicked(ap)
            End If
        Catch ex As Exception
            Debug.WriteLine($"OnCardClicked error: {ex.Message}")
        End Try
    End Sub

    Private Sub OnCardDoubleClicked(card As Panel, e As MouseEventArgs)
        Try
            Dim ap = TryCast(card.Tag, AppointmentC)
            If ap IsNot Nothing Then
                RaiseEvent AppointmentDoubleClicked(ap)
            End If
        Catch ex As Exception
            Debug.WriteLine($"OnCardDoubleClicked error: {ex.Message}")
        End Try
    End Sub

    Private Sub PatientCombo1_PatientValueChanged(sender As Object, e As PatientCombo.PatientIndexChangedEvent)
        lblPatient.Text = e.PatientName
        If e.PatientID > 0 Then
            Dim clsData As New PatientDATA
            CurrentPatient = clsData.Select_RecordByID(e.PatientID)
            localPatientID = e.PatientID
        End If

        'LoadAndRender()
    End Sub

    Private Sub ScheduleAdmin_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Try
            _isShuttingDown = True
            RemoveHandler Me.Resize, AddressOf OnScheduleResize
            'If _dragTimer IsNot Nothing Then
            '    RemoveHandler _dragTimer.Tick, AddressOf DragTimer_Tick
            '    _dragTimer.Stop()
            '    _dragTimer.Dispose()
            '    _dragTimer = Nothing
            'End If
            If _resizeTimer IsNot Nothing Then
                RemoveHandler _resizeTimer.Tick, AddressOf ResizeTimer_Tick
                _resizeTimer.Stop()
                _resizeTimer.Dispose()
                _resizeTimer = Nothing
            End If
            'If _dragHoldTimer IsNot Nothing Then
            '    _dragHoldTimer.Stop()
            '    _dragHoldTimer.Dispose()
            '    _dragHoldTimer = Nothing
            'End If
            'If _slideTimer IsNot Nothing Then
            '    RemoveHandler _slideTimer.Tick, AddressOf SlideTimer_Tick
            '    _slideTimer.Stop()
            '    _slideTimer.Dispose()
            '    _slideTimer = Nothing
            'End If
            If _dragTimerThisWeek IsNot Nothing Then
                RemoveHandler _dragTimerThisWeek.Tick, AddressOf DragTimer_TickThisWeek
                _dragTimerThisWeek.Stop()
                _dragTimerThisWeek.Dispose()
                _dragTimerThisWeek = Nothing
            End If
            If _slideFrom IsNot Nothing AndAlso _slideFrom.Image IsNot Nothing Then
                _slideFrom.Image.Dispose()
                _slideFrom.Image = Nothing
            End If
            If _slideTo IsNot Nothing AndAlso _slideTo.Image IsNot Nothing Then
                _slideTo.Image.Dispose()
                _slideTo.Image = Nothing
            End If
        Catch
            ' keep dispose safe
        End Try
    End Sub

    Private Sub btnLabs_Click(sender As Object, e As EventArgs) Handles btnLabs.Click
        Try
            Using F As New FrmLabSendWhats
                F.ShowDialog(Me)
            End Using
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.btnLabs_Click",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not open the lab WhatsApp window.",
                                   arabicMessage:="تعذر فتح نافذة واتساب المختبر.")
        End Try
    End Sub
End Class

''' <summary>Preview for <see cref="WeekSched"/> snapshot: Save, WhatsApp, or Cancel. Takes ownership of the preview image.</summary>
Friend NotInheritable Class SchedulerWeekSnapshotPreviewForm1
    Inherits XtraForm

    Private NotInheritable Class DoctorWhatsTarget1
        Public Property DrID As Integer
        Public Property DrName As String
        Public Property WhatsNumber As String
    End Class

    Private NotInheritable Class SnapshotWhatsRecipient1
        Public Property DisplayName As String
        Public Property Number As String
        Public Property IsPrimary As Boolean
    End Class

    Private ReadOnly _image As Image
    Private ReadOnly _exportsDir As String
    Private ReadOnly _weekCaption As String
    Private ReadOnly _fileNamePrefix As String
    Private ReadOnly _htmlContext As WeekSnapshotHtmlContext
    Private ReadOnly _uiFont As Font
    Private _whatsBinder As PatientWhatsControlsBinder
    Private _usePatientWhatsBinder As Boolean
    Private ReadOnly _cboPrefix As ComboBoxEdit
    Private ReadOnly _txtWhats As TextEdit
    Private ReadOnly _lblWhats As LabelControl
    Private ReadOnly _chkDoctors As CheckedComboBoxEdit
    Private ReadOnly _radSendWhatsPng As RadioButton
    Private ReadOnly _radSendWhatsHtml As RadioButton
    Private ReadOnly _doctorTargetsById As Dictionary(Of Integer, DoctorWhatsTarget1) = New Dictionary(Of Integer, DoctorWhatsTarget1)()
    Private _suppressDoctorsSelectionSave As Boolean

    ''' <summary>Week / month use <see cref="WeekSnapshotHtmlContext.Columns"/>; day view uses <see cref="WeekSnapshotHtmlContext.DayTimeline"/>; days timeline uses <see cref="WeekSnapshotHtmlContext.WeekHorizontal"/>.</summary>
    Private Shared Function HasSnapshotHtmlData(ctx As WeekSnapshotHtmlContext) As Boolean
        If ctx Is Nothing Then Return False
        If ctx.WeekHorizontal IsNot Nothing Then Return True
        If ctx.DayTimeline IsNot Nothing Then Return True
        Return ctx.Columns IsNot Nothing AndAlso ctx.Columns.Count > 0
    End Function

    Public Sub New(preview As Image, exportsDir As String, weekCaption As String, Optional filterPatientId As Integer? = Nothing, Optional fileNamePrefix As String = Nothing, Optional htmlContext As WeekSnapshotHtmlContext = Nothing)
        _image = preview
        _exportsDir = If(exportsDir, "")
        _weekCaption = If(weekCaption, "")
        _fileNamePrefix = If(String.IsNullOrWhiteSpace(fileNamePrefix), "ScheduleView", fileNamePrefix.Trim())
        _htmlContext = htmlContext
        _uiFont = New Font("Calibri", 10, FontStyle.Bold)
        Text = If(Eng, "Schedule snapshot", "لقطة الجدول")
        Width = 1000
        Height = 720
        Icon = GetIcon()
        Font = _uiFont
        StartPosition = FormStartPosition.CenterParent
        KeyPreview = True
        AddHandler KeyDown, Sub(s, e2) If e2.KeyCode = Keys.Escape Then Close()

        Dim scroll As New Panel With {.Dock = DockStyle.Fill, .AutoScroll = True, .BackColor = SystemColors.ControlDark}
        Dim pic As New PictureBox With {
            .Image = preview,
            .SizeMode = PictureBoxSizeMode.AutoSize,
            .Location = New Point(0, 0),
            .BackColor = Color.White
        }
        scroll.Controls.Add(pic)

        Dim footer As New TableLayoutPanel With {
            .Dock = DockStyle.Bottom,
            .AutoSize = True,
            .ColumnCount = 1,
            .RowCount = 2
        }
        footer.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        footer.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        Dim whatsRow As New FlowLayoutPanel With {
            .AutoSize = True,
            .AutoSizeMode = AutoSizeMode.GrowAndShrink,
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = False,
            .Padding = New Padding(8, 6, 8, 4)
        }
        Dim lblWhatsApp As New Label With {
            .AutoSize = True,
            .Text = If(Eng, "WhatsApp:", "واتساب:"),
            .Font = _uiFont,
            .Margin = New Padding(0, 8, 6, 0)
        }
        _cboPrefix = New ComboBoxEdit With {.Width = 210, .Margin = New Padding(0, 4, 8, 0)}
        _cboPrefix.Properties.Appearance.Font = _uiFont
        _cboPrefix.Properties.AppearanceDropDown.Font = _uiFont
        _txtWhats = New TextEdit With {.Width = 130, .Margin = New Padding(0, 4, 8, 0)}
        _txtWhats.Properties.Appearance.Font = _uiFont
        _txtWhats.Properties.MaxLength = 10
        _lblWhats = New LabelControl With {
            .Width = 240,
            .Height = 22,
            .Margin = New Padding(0, 8, 0, 0)
        }
        _lblWhats.Appearance.Font = _uiFont
        _lblWhats.Appearance.ForeColor = Color.Blue
        _lblWhats.Appearance.Options.UseFont = True
        _lblWhats.Appearance.Options.UseForeColor = True

        whatsRow.Controls.Add(lblWhatsApp)
        whatsRow.Controls.Add(_cboPrefix)
        whatsRow.Controls.Add(_txtWhats)
        whatsRow.Controls.Add(_lblWhats)

        Dim lblDoctors As New Label With {
            .AutoSize = True,
            .Text = If(Eng, "Also send to doctors:", "أرسل أيضًا إلى الأطباء:"),
            .Font = _uiFont,
            .Margin = New Padding(18, 8, 6, 0)
        }
        _chkDoctors = New CheckedComboBoxEdit With {.Width = 250, .Margin = New Padding(0, 4, 8, 0)}
        _chkDoctors.Properties.Appearance.Font = _uiFont
        _chkDoctors.Properties.AppearanceDropDown.Font = _uiFont
        _chkDoctors.Properties.SelectAllItemVisible = True
        _chkDoctors.Properties.NullText = If(Eng, "Select doctors", "اختر الأطباء")
        whatsRow.Controls.Add(lblDoctors)
        whatsRow.Controls.Add(_chkDoctors)

        Dim btnRow As New FlowLayoutPanel With {
            .AutoSize = True,
            .AutoSizeMode = AutoSizeMode.GrowAndShrink,
            .FlowDirection = FlowDirection.LeftToRight,
            .Padding = New Padding(8, 4, 8, 8),
            .WrapContents = False
        }

        Dim btnSave As New SimpleButton With {.Text = If(Eng, "Save", "حفظ"), .Margin = New Padding(4)}
        btnSave.Appearance.Font = _uiFont
        Dim btnHtml As New SimpleButton With {
            .Text = If(Eng, "Save HTML", "حفظ HTML"),
            .Margin = New Padding(4),
            .Enabled = HasSnapshotHtmlData(htmlContext)
        }
        btnHtml.Appearance.Font = _uiFont
        If btnHtml.Enabled AndAlso _htmlContext IsNot Nothing Then
            btnHtml.ToolTip = If(Eng,
                "Save a single HTML page with the current schedule, appointment text, and card colors (week columns, day timeline, or month layout).",
                "حفظ صفحة HTML للجدول الحالي والمواعيد والألوان (أسبوعي أو يومي أو شهري).")
        Else
            btnHtml.ToolTip = If(Eng, "HTML export is unavailable (no data or could not build).", "تصدير HTML غير متاح (لا بيانات أو تعذر الإنشاء).")
        End If
        Dim canHtmlWhats = HasSnapshotHtmlData(htmlContext)
        Dim lblWaAttach As New Label With {
            .AutoSize = True,
            .Text = If(Eng, "WhatsApp file:", "ملف واتساب:"),
            .Font = _uiFont,
            .Margin = New Padding(12, 8, 4, 0)
        }
        _radSendWhatsPng = New RadioButton With {
            .Text = If(Eng, "Image (PNG)", "صورة (PNG)"),
            .AutoSize = True,
            .Font = _uiFont,
            .Checked = True,
            .Margin = New Padding(0, 4, 4, 0)
        }
        _radSendWhatsHtml = New RadioButton With {
            .Text = If(Eng, "HTML", "HTML"),
            .AutoSize = True,
            .Font = _uiFont,
            .Enabled = canHtmlWhats,
            .Margin = New Padding(0, 4, 4, 0)
        }
        Dim ttWaFiles As New ToolTip()
        ttWaFiles.SetToolTip(_radSendWhatsPng, If(Eng, "Send the schedule snapshot as a PNG image (same as Save).", "إرسال لقطة الجدول كصورة PNG (كالحفظ)."))
        ttWaFiles.SetToolTip(_radSendWhatsHtml, If(Eng, "Send the week layout as an HTML file (same as Save HTML, readable in a browser).", "إرسال أسبوع كملف HTML (كحفظ HTML، قابل للقراءة في المتصفح)."))
        ttWaFiles.SetToolTip(lblWaAttach, If(Eng, "Attachment type for the WhatsApp send button.", "نوع المرفق لزر واتساب."))
        Dim btnWhats As New SimpleButton With {.Text = If(Eng, "WhatsApp", "واتساب"), .Margin = New Padding(4)}
        btnWhats.Appearance.Font = _uiFont
        Dim btnCancel As New SimpleButton With {.Text = If(Eng, "Cancel", "إلغاء"), .Margin = New Padding(4)}
        btnCancel.Appearance.Font = _uiFont

        AddHandler btnCancel.Click, Sub(s, e) Close()
        AddHandler btnSave.Click, AddressOf BtnSave_OnClick
        AddHandler btnHtml.Click, AddressOf BtnHtml_OnClick
        AddHandler btnWhats.Click, AddressOf BtnWhats_OnClick

        btnRow.Controls.Add(btnSave)
        btnRow.Controls.Add(btnHtml)
        btnRow.Controls.Add(lblWaAttach)
        btnRow.Controls.Add(_radSendWhatsPng)
        btnRow.Controls.Add(_radSendWhatsHtml)
        btnRow.Controls.Add(btnWhats)
        btnRow.Controls.Add(btnCancel)

        footer.Controls.Add(whatsRow, 0, 0)
        footer.Controls.Add(btnRow, 0, 1)

        Controls.Add(scroll)
        Controls.Add(footer)

        _whatsBinder = New PatientWhatsControlsBinder(_cboPrefix, _txtWhats, _lblWhats, Me)
        WhatsHelper.FillCboPrefixOnce(_cboPrefix)
        LoadDoctorTargets(ResolveCurrentLinkedDoctorId())
        AddHandler _chkDoctors.EditValueChanged, AddressOf ChkDoctors_EditValueChanged
        RestoreSavedDoctorSelections()
        _usePatientWhatsBinder = False
        If filterPatientId.HasValue AndAlso filterPatientId.Value > 0 Then
            Try
                Dim pd As New PatientDATA()
                Dim p = pd.Select_RecordByID(filterPatientId.Value)
                If p IsNot Nothing Then
                    _whatsBinder.BindToPatient(p, False)
                    _usePatientWhatsBinder = True
                End If
            Catch
            End Try
        End If
#If DEBUG Then
        If System.Diagnostics.Debugger.IsAttached Then
            System.Diagnostics.Debug.Assert(WeekSnapshotHtmlExportSelfTest.RunSanityCheck(), "WeekSnapshotHtmlExportSelfTest")
        End If
#End If
        If Not _usePatientWhatsBinder Then
            AddHandler _cboPrefix.SelectedIndexChanged, Sub(s, e) RefreshStandaloneWhatsLabel()
            AddHandler _cboPrefix.EditValueChanged, Sub(s, e) RefreshStandaloneWhatsLabel()
            AddHandler _txtWhats.EditValueChanged, Sub(s, e) RefreshStandaloneWhatsLabel()
            ApplyLinkedDoctorDefaultWhats()
            RefreshStandaloneWhatsLabel()
        Else
            AddHandler _cboPrefix.SelectedIndexChanged, Sub(s, e) _whatsBinder.OnCboPrefixSelectedIndexChanged()
            AddHandler _cboPrefix.EditValueChanged, Sub(s, e) _whatsBinder.OnCboPrefixEditValueChanged()
            AddHandler _txtWhats.EditValueChanged, Sub(s, e) _whatsBinder.OnTxtWhatsEditValueChanged()
            AddHandler _txtWhats.Leave, Sub(s, e) _whatsBinder.OnTxtWhatsLeave()
            AddHandler _txtWhats.Validated, Sub(s, e) _whatsBinder.OnTxtWhatsValidated()
        End If
        AddHandler _txtWhats.KeyDown, AddressOf TxtWhats_KeyDownDigitsOnly
    End Sub

    Private Function ResolveCurrentLinkedDoctorId() As Integer
        Try
            If CurrentDoctor IsNot Nothing AndAlso CurrentDoctor.DrID > 0 Then
                Return CurrentDoctor.DrID
            End If
        Catch
        End Try
        Try
            If CurrentUser IsNot Nothing AndAlso CurrentUser.DrID.HasValue AndAlso CurrentUser.DrID.Value > 0 Then
                Return CurrentUser.DrID.Value
            End If
        Catch
        End Try
        If LoggedInDoctorID > 0 Then Return LoggedInDoctorID
        Return 0
    End Function

    Private Function ResolveCurrentLinkedDoctor() As Doctors
        Try
            Dim id = ResolveCurrentLinkedDoctorId()
            If id <= 0 Then Return Nothing
            Dim dd As New DoctorsDATA()
            Return dd.GetDoctorById(id)
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub ApplyLinkedDoctorDefaultWhats()
        Dim d = ResolveCurrentLinkedDoctor()
        If d Is Nothing Then Return
        Dim full = WhatsHelper.BuildInternationalWhatsDigits(If(d.WhatsApp, ""), If(d.WhatsAppPrefix, "")).Trim()
        If String.IsNullOrWhiteSpace(full) OrElse full.Length < 8 Then Return
        WhatsHelper.SelectComboFromStoredPrefixColumn(_cboPrefix, If(d.WhatsAppPrefix, ""))
        _txtWhats.Text = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(If(d.WhatsApp, ""))
    End Sub

    Private Sub LoadDoctorTargets(excludeDoctorId As Integer)
        _doctorTargetsById.Clear()
        _chkDoctors.Properties.Items.Clear()
        Try
            Dim dd As New DoctorsDATA()
            Dim allDoctors = dd.SelectAll()
            If allDoctors Is Nothing Then Return
            For Each d In allDoctors
                If d Is Nothing OrElse d.DrID <= 0 Then Continue For
                Dim full = WhatsHelper.BuildInternationalWhatsDigits(If(d.WhatsApp, ""), If(d.WhatsAppPrefix, "")).Trim()
                If String.IsNullOrWhiteSpace(full) OrElse full.Length < 8 Then Continue For
                _doctorTargetsById(d.DrID) = New DoctorWhatsTarget1 With {
                    .DrID = d.DrID,
                    .DrName = If(d.DrName, "").Trim(),
                    .WhatsNumber = full
                }
                If d.DrID = excludeDoctorId Then Continue For
                _chkDoctors.Properties.Items.Add(New CheckedListBoxItem(d.DrID, If(d.DrName, "").Trim(), CheckState.Unchecked, True))
            Next
        Catch
            ' Keep form usable even if doctor lookup fails.
        End Try
    End Sub

    Private Function DoctorsSelectionStorageKey() As String
        Dim uid As Integer = 0
        Try
            If CurrentUser IsNot Nothing AndAlso CurrentUser.UsID > 0 Then
                uid = CurrentUser.UsID
            End If
        Catch
        End Try
        If uid <= 0 AndAlso LoggedInUserID > 0 Then uid = LoggedInUserID
        Return "SnapshotExtraDoctors_User_" & uid.ToString()
    End Function

    Private Sub RestoreSavedDoctorSelections()
        Dim raw As String = ""
        Try
            raw = GetSetting("DentistX", "SchedulerSnapshotPreview", DoctorsSelectionStorageKey(), "")
        Catch
            raw = ""
        End Try
        If String.IsNullOrWhiteSpace(raw) Then Return
        Dim wanted As New HashSet(Of Integer)()
        For Each s In raw.Split(","c)
            Dim id As Integer
            If Integer.TryParse(If(s, "").Trim(), id) AndAlso id > 0 Then
                wanted.Add(id)
            End If
        Next
        If wanted.Count = 0 Then Return
        _suppressDoctorsSelectionSave = True
        Try
            For Each rawItem In _chkDoctors.Properties.Items
                Dim it = TryCast(rawItem, CheckedListBoxItem)
                If it Is Nothing OrElse it.Value Is Nothing Then Continue For
                Dim id As Integer
                If Integer.TryParse(it.Value.ToString(), id) Then
                    it.CheckState = If(wanted.Contains(id), CheckState.Checked, CheckState.Unchecked)
                End If
            Next
        Finally
            _suppressDoctorsSelectionSave = False
        End Try
    End Sub

    Private Function BuildCheckedDoctorsCsv() As String
        Dim ids As New List(Of String)()
        For Each rawItem In _chkDoctors.Properties.Items
            Dim it = TryCast(rawItem, CheckedListBoxItem)
            If it Is Nothing OrElse it.CheckState <> CheckState.Checked OrElse it.Value Is Nothing Then Continue For
            Dim id As Integer
            If Integer.TryParse(it.Value.ToString(), id) AndAlso id > 0 Then
                ids.Add(id.ToString())
            End If
        Next
        Return String.Join(",", ids)
    End Function

    Private Sub ChkDoctors_EditValueChanged(sender As Object, e As EventArgs)
        If _suppressDoctorsSelectionSave Then Return
        Try
            SaveSetting("DentistX", "SchedulerSnapshotPreview", DoctorsSelectionStorageKey(), BuildCheckedDoctorsCsv())
        Catch
            ' Ignore preference persistence failures.
        End Try
    End Sub

    Private Function BuildSelectedRecipients(primaryNumber As String) As List(Of SnapshotWhatsRecipient1)
        Dim list As New List(Of SnapshotWhatsRecipient1)()
        Dim seen As New HashSet(Of String)(StringComparer.Ordinal)
        Dim firstNumber = If(primaryNumber, "").Trim()
        If Not String.IsNullOrWhiteSpace(firstNumber) Then
            list.Add(New SnapshotWhatsRecipient1 With {
                .DisplayName = If(Eng, "Default recipient", "المستلم الافتراضي"),
                .Number = firstNumber,
                .IsPrimary = True
            })
            seen.Add(firstNumber)
        End If
        For Each raw In _chkDoctors.Properties.Items
            Dim item = TryCast(raw, CheckedListBoxItem)
            If item Is Nothing OrElse item.CheckState <> CheckState.Checked Then Continue For
            Dim drId As Integer
            If item.Value Is Nothing OrElse Not Integer.TryParse(item.Value.ToString(), drId) Then Continue For
            Dim target As DoctorWhatsTarget1 = Nothing
            If Not _doctorTargetsById.TryGetValue(drId, target) OrElse target Is Nothing Then Continue For
            Dim digits = If(target.WhatsNumber, "").Trim()
            If String.IsNullOrWhiteSpace(digits) Then Continue For
            If seen.Contains(digits) Then Continue For
            list.Add(New SnapshotWhatsRecipient1 With {
                .DisplayName = If(target.DrName, ""),
                .Number = digits,
                .IsPrimary = False
            })
            seen.Add(digits)
        Next
        Return list
    End Function

    Private Sub RefreshStandaloneWhatsLabel()
        _lblWhats.Text = WhatsHelper.BuildInternationalWhatsDigits(If(_txtWhats.Text, ""), WhatsHelper.GetPrefixTextForStorage(_cboPrefix))
    End Sub

    Private Shared Sub TxtWhats_KeyDownDigitsOnly(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
           e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
           e.KeyCode = Keys.Up OrElse e.KeyCode = Keys.Down OrElse
           e.KeyCode = Keys.Tab OrElse e.KeyCode = Keys.Home OrElse e.KeyCode = Keys.End Then
            Return
        End If
        Dim topRow = (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9)
        Dim numPad = (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9)
        If (topRow OrElse numPad) AndAlso Not e.Shift Then Return
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub

    Private Function DefaultFileName() As String
        Return $"{_fileNamePrefix}_{DateTime.Now:yyyyMMdd_HHmmss}.png"
    End Function

    Private Function DefaultHtmlFileName() As String
        Return $"{_fileNamePrefix}_{DateTime.Now:yyyyMMdd_HHmmss}.html"
    End Function

    Private Sub BtnHtml_OnClick(sender As Object, e As EventArgs)
        If Not HasSnapshotHtmlData(_htmlContext) Then Return
        Try
            Directory.CreateDirectory(_exportsDir)
        Catch
        End Try
        Using dlg As New SaveFileDialog()
            dlg.InitialDirectory = If(Directory.Exists(_exportsDir), _exportsDir, Application.StartupPath)
            dlg.FileName = DefaultHtmlFileName()
            dlg.DefaultExt = "html"
            dlg.Filter = If(Eng, "HTML page|*.html", "صفحة HTML|*.html")
            dlg.AddExtension = True
            dlg.OverwritePrompt = True
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                Dim page = WeekSnapshotHtmlWriter.BuildDocument(_htmlContext)
                File.WriteAllText(dlg.FileName, page, Encoding.UTF8)
                MessageBox.Show(
                    If(Eng, "HTML file saved.", "تم حفظ ملف HTML."),
                    Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message, If(Eng, "Save error", "خطأ في الحفظ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub BtnSave_OnClick(sender As Object, e As EventArgs)
        Try
            Directory.CreateDirectory(_exportsDir)
        Catch
        End Try
        Using dlg As New SaveFileDialog()
            dlg.InitialDirectory = If(Directory.Exists(_exportsDir), _exportsDir, Application.StartupPath)
            dlg.FileName = DefaultFileName()
            dlg.DefaultExt = "png"
            dlg.Filter = If(Eng, "PNG image|*.png", "صورة PNG|*.png")
            dlg.AddExtension = True
            dlg.OverwritePrompt = True
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                _image.Save(dlg.FileName, ImageFormat.Png)
                MessageBox.Show(
                    If(Eng, "Image saved.", "تم حفظ الصورة."),
                    Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message, If(Eng, "Save error", "خطأ في الحفظ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Async Sub BtnWhats_OnClick(sender As Object, e As EventArgs)
        Dim title = If(Eng, "Send via WhatsApp", "إرسال عبر واتساب")
        If _usePatientWhatsBinder AndAlso _whatsBinder IsNot Nothing Then
            _whatsBinder.SaveIfDirty()
        Else
            RefreshStandaloneWhatsLabel()
        End If
        Dim number = If(_lblWhats.Text, "").Trim()
        Dim recipients = BuildSelectedRecipients(number)
        If recipients.Count = 0 Then
            MessageBox.Show(
                If(Eng, "Enter a valid WhatsApp number or select at least one doctor.", "أدخل رقم واتساب صالح أو اختر طبيبًا واحدًا على الأقل."),
                title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me).ConfigureAwait(True) Then Return
        Dim clinicId = WhatsAppService.GetCurrentClinicId()
        If String.IsNullOrWhiteSpace(clinicId) Then Return
        Try
            Directory.CreateDirectory(_exportsDir)
        Catch
        End Try
        Dim canSendHtml = HasSnapshotHtmlData(_htmlContext)
        Dim useHtml = canSendHtml AndAlso _radSendWhatsHtml IsNot Nothing AndAlso _radSendWhatsHtml.Checked
        Dim path As String
        If useHtml Then
            path = IO.Path.Combine(_exportsDir, DefaultHtmlFileName())
            Try
                File.WriteAllText(path, WeekSnapshotHtmlWriter.BuildDocument(_htmlContext), Encoding.UTF8)
            Catch ex As Exception
                MessageBox.Show(ex.Message, If(Eng, "Save error", "خطأ في الحفظ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try
        Else
            path = IO.Path.Combine(_exportsDir, DefaultFileName())
            Try
                _image.Save(path, ImageFormat.Png)
            Catch ex As Exception
                MessageBox.Show(ex.Message, If(Eng, "Save error", "خطأ في الحفظ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try
        End If
        Dim cap = If(String.IsNullOrWhiteSpace(_weekCaption), If(Eng, "Schedule snapshot", "لقطة الجدول"), _weekCaption)
        Dim pid As Integer? = Nothing
        Dim pname As String = Nothing
        If _usePatientWhatsBinder AndAlso _whatsBinder IsNot Nothing AndAlso _whatsBinder.BoundPatient IsNot Nothing Then
            pid = _whatsBinder.BoundPatient.PatientID
            pname = If(_whatsBinder.BoundPatient.PatientName, "").Trim()
        End If
        If _usePatientWhatsBinder AndAlso _whatsBinder IsNot Nothing Then _whatsBinder.BeginSuppressAutoSaveWhileModal()
        Try
            Dim wa As New WhatsAppService()
            Dim okCount As Integer = 0
            Dim failCount As Integer = 0
            For Each recipient In recipients
                Try
                    Dim ctx As New WhatsAppSendContext With {
                        .Category = WhatsAppMessageCategories.ManualSend,
                        .SourceHint = NameOf(WeekSched) & " · Schedule snapshot",
                        .RevealMessageCenter = True,
                        .PatientId = If(recipient.IsPrimary, pid, Nothing),
                        .DisplayName = If(recipient.IsPrimary AndAlso Not String.IsNullOrWhiteSpace(pname), pname, recipient.DisplayName)
                    }
                    Await wa.SendMessageAsync(clinicId, recipient.Number, cap, path, ctx).ConfigureAwait(True)
                    okCount += 1
                Catch
                    failCount += 1
                End Try
            Next
            If okCount > 0 AndAlso failCount = 0 Then
                MsgBox(If(Eng, "Message queued for sending.", "تم وضع الرسالة في الطابور للإرسال."), MsgBoxStyle.Information)
            ElseIf failCount > 0 Then
                MessageBox.Show(
                    If(Eng,
                       $"Sent to {okCount} recipient(s); {failCount} failed. Check WhatsApp activity log for details.",
                       $"تم الإرسال إلى {okCount} مستلم(ين)؛ وفشل {failCount}. راجع سجل نشاط واتساب للتفاصيل."),
                    title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If _usePatientWhatsBinder AndAlso _whatsBinder IsNot Nothing Then _whatsBinder.EndSuppressAutoSaveAndSync()
        End Try
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        MyBase.OnFormClosed(e)
        If _uiFont IsNot Nothing Then _uiFont.Dispose()
        If _image IsNot Nothing Then _image.Dispose()
    End Sub
End Class
