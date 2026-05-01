Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DevExpress.Utils.Layout
Imports DevExpress.XtraEditors

Public Class ApptHostCtl
    Inherits XtraUserControl
    Implements IPatientAwareUserControl

    Private Const GrGdiObjects As Integer = 0
    Private Const GrUserObjects As Integer = 1

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetGuiResources(hProcess As IntPtr, uiFlags As Integer) As Integer
    End Function

    ''' <summary>Absolute header band height — same pattern as <see cref="SchedulerNew"/> <c>tlpMain</c> row 0 (152px).</summary>
    Private Const MainHeaderRowPixels As Single = 169.0F
    Private Const BodyEdgeHintWidth As Integer = 46
    Private Const BodyEdgeHintHeight As Integer = 90
    Private Const BodyEdgeHintHostPadding As Integer = 6
    Private Const BodyEdgeHintBandWidth As Single = BodyEdgeHintWidth + (BodyEdgeHintHostPadding * 2)

    Public Enum ViewMode
        DayView
        ThisWeek
        ThisWeekFull
        MonthlyWeek
        MonthView
        DaysTimeline
        DoctorsDay
    End Enum

    Private ReadOnly _state As ApptState
    Private ReadOnly _interactionHub As ApptInteractionHub
    Private ReadOnly _viewFactory As ApptViewFactory
    Private ReadOnly _layout As TablePanel
    Private ReadOnly _header As ApptHeaderCtl
    Private _bodyHost As PanelControl
    Private _bodyViewHost As PanelControl
    Private _bodyPrevHintHost As PanelControl
    Private _bodyNextHintHost As PanelControl
    Private _provider As ApptDataProvider
    Private _currentView As XtraUserControl
    Private _currentData As ApptDataBundle
    ''' <summary>Full-timeline list for PREV/NEXT hints (same patient/doctor/status/reason as view, not limited to filter dates).</summary>
    Private _edgeHintAppointments As List(Of AppointmentC)
    Private _loadedPatientId As Integer?
    Private _currentPatient As Patient
    Private _suppressRefresh As Boolean
    Private ReadOnly _viewNavBack As New Stack(Of ApptViewMode)
    Private ReadOnly _viewNavForward As New Stack(Of ApptViewMode)
    Private _suppressViewNavRecording As Boolean

    Private _btnBodyExpand As SimpleButton
    Private _btnBodyQuickAdd As SimpleButton
    Private _bodyWorkspaceMaximized As Boolean
    Private _layoutRowsCaptured As Boolean
    Private _layoutRow0Style As TablePanelEntityStyle
    Private _layoutRow0Height As Single
    Private _layoutRow1Style As TablePanelEntityStyle
    Private _layoutRow1Height As Single

    Private _bodyPrevHint As ArrowLable
    Private _bodyNextHint As ArrowLable
    Private _pendingScrollAppt As AppointmentC
    Private _edgeNavPrevAnchor As DateTime?
    Private _edgeNavNextAnchor As DateTime?
    Private _edgeNavPrevScrollAppt As AppointmentC
    Private _edgeNavNextScrollAppt As AppointmentC
    Private _bodyEdgeHostEventsWired As Boolean
    ''' <summary>True after PREV/NEXT <see cref="ArrowLable"/> are parented inside the body workspace instead of the root host.</summary>
    Private _bodyEdgeHintsInBody As Boolean

    Public Sub New()
        _state = New ApptState()
        _state.ApptCardStartTimeColor = My.Settings.ApptModuleCardStartTimeColor
        _state.ApptCardEndTimeColor = My.Settings.ApptModuleCardEndTimeColor
        _state.ApptCardPatientNameColor = My.Settings.ApptModuleCardPatientNameColor
        _state.ApptCardReasonColor = My.Settings.ApptModuleCardReasonColor
        _state.ApptCardNotesColor = My.Settings.ApptModuleCardNotesColor
        ApplyWorkingHoursFromUserSettings()
        _interactionHub = New ApptInteractionHub()
        _viewFactory = New ApptViewFactory()
        _provider = New ApptDataProvider(New AppointmentCRepository(DentistXDATA.GetConnection.ConnectionString))
        _header = New ApptHeaderCtl()

        _layout = BuildShell()
        Controls.Add(_layout)
        WireHostEvents()
        EnsureBodyWorkspaceExpandButton()
        EnsureBodyEdgeHints()
    End Sub

    Public Sub New(repo As AppointmentCRepository)
        _state = New ApptState()
        _state.ApptCardStartTimeColor = My.Settings.ApptModuleCardStartTimeColor
        _state.ApptCardEndTimeColor = My.Settings.ApptModuleCardEndTimeColor
        _state.ApptCardPatientNameColor = My.Settings.ApptModuleCardPatientNameColor
        _state.ApptCardReasonColor = My.Settings.ApptModuleCardReasonColor
        _state.ApptCardNotesColor = My.Settings.ApptModuleCardNotesColor
        ApplyWorkingHoursFromUserSettings()
        _interactionHub = New ApptInteractionHub()
        _viewFactory = New ApptViewFactory()
        _provider = New ApptDataProvider(repo)
        _header = New ApptHeaderCtl()

        _layout = BuildShell()
        Controls.Add(_layout)
        WireHostEvents()
        EnsureBodyWorkspaceExpandButton()
        EnsureBodyEdgeHints()
    End Sub

    ''' <summary>Initializes <see cref="ApptState"/>'s work range from the same user settings as <see cref="SchedulerNew"/>.</summary>
    Private Sub ApplyWorkingHoursFromUserSettings()
        If _state Is Nothing Then Return
        Try
            Dim s = My.Settings.SchedulerWorkingDayStart
            Dim e_ = My.Settings.SchedulerWorkingDayEnd
            If s < TimeSpan.Zero OrElse s >= TimeSpan.FromDays(1) Then Return
            If e_ <= TimeSpan.Zero OrElse e_ > TimeSpan.FromDays(1) OrElse e_ <= s Then Return
            _state.WorkStartTime = s
            _state.WorkEndTime = e_
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptHostCtl.ApplyWorkingHoursFromUserSettings", showUser:=False)
        End Try
    End Sub

    Public Property AppointmentAppearanceSelector As Func(Of ApptCardVm, ApptCardAppearance)
    Public Property AllowAddWithoutCurrentPatient As Boolean = True
    Public Property DragHoldTimeMs As Integer = 750

    Public Property ShowPatientLbls As Boolean
        Get
            Return _state.ShowPatientLabels
        End Get
        Set(value As Boolean)
            _state.ShowPatientLabels = value
            LoadAndRender()
        End Set
    End Property

    Public Property WorkStartTime As TimeSpan
        Get
            Return _state.WorkStartTime
        End Get
        Set(value As TimeSpan)
            _state.WorkStartTime = value
            LoadAndRender()
        End Set
    End Property

    Public Property WorkEndTime As TimeSpan
        Get
            Return _state.WorkEndTime
        End Get
        Set(value As TimeSpan)
            _state.WorkEndTime = value
            LoadAndRender()
        End Set
    End Property

    Public Property Use24HourFormat As Boolean
        Get
            Return _state.Use24HourFormat
        End Get
        Set(value As Boolean)
            _state.Use24HourFormat = value
            LoadAndRender()
        End Set
    End Property

    Public Property FilterPatientId As Integer?
        Get
            Return _state.PatientFilterId
        End Get
        Set(value As Integer?)
            _state.PatientFilterId = value
            LoadAndRender()
        End Set
    End Property

    Public Property FilterDoctorId As Integer?
        Get
            Return _state.DoctorFilterId
        End Get
        Set(value As Integer?)
            _state.DoctorFilterId = value
            LoadAndRender()
        End Set
    End Property

    Public Property FilterReason As String
        Get
            Return _state.VisibleReason
        End Get
        Set(value As String)
            _state.VisibleReason = value
            LoadAndRender()
        End Set
    End Property

    Public Property FilterStatus As String
        Get
            Return _state.VisibleStatus
        End Get
        Set(value As String)
            _state.VisibleStatus = value
            LoadAndRender()
        End Set
    End Property

    Public Property FilterStartDate As DateTime
        Get
            Return _state.FilterStartDate
        End Get
        Set(value As DateTime)
            _state.FilterStartDate = value
        End Set
    End Property

    Public Property FilterEndDate As DateTime
        Get
            Return _state.FilterEndDate
        End Get
        Set(value As DateTime)
            _state.FilterEndDate = value
        End Set
    End Property

    Public Property StartHour As Integer
        Get
            Return CInt(Math.Floor(_state.WorkStartTime.TotalHours))
        End Get
        Set(value As Integer)
            _state.WorkStartTime = TimeSpan.FromHours(Math.Max(0, Math.Min(23, value)))
            If _state.WorkEndTime <= _state.WorkStartTime Then
                _state.WorkEndTime = _state.WorkStartTime.Add(TimeSpan.FromHours(1))
            End If
            LoadAndRender()
        End Set
    End Property

    Public Property EndHour As Integer
        Get
            Return CInt(Math.Ceiling(_state.WorkEndTime.TotalHours))
        End Get
        Set(value As Integer)
            Dim safeEnd = Math.Max(StartHour + 1, Math.Min(24, value))
            _state.WorkEndTime = TimeSpan.FromHours(safeEnd)
            LoadAndRender()
        End Set
    End Property

    Public WriteOnly Property ValueFromParent As Integer
        Set(value As Integer)
            LoadAndRender()
        End Set
    End Property

    Public Event AppointmentClicked As Action(Of AppointmentC)
    Public Event AppointmentDoubleClicked As Action(Of AppointmentC)
    Public Event AppointmentDeleted As Action(Of Integer)

    Public Property CurrentPatient As Patient
        Get
            Return _currentPatient
        End Get
        Private Set(value As Patient)
            _currentPatient = value
        End Set
    End Property

    Private Function BuildShell() As TablePanel
        Dock = DockStyle.Fill
        Appearance.BackColor = Color.Transparent
        Appearance.Options.UseBackColor = True

        Dim layout = New TablePanel With {
            .Dock = DockStyle.Fill,
            .Name = "apptHostMainTable",
            .Padding = Padding.Empty,
            .Margin = Padding.Empty
        }
        layout.Appearance.BackColor = Color.Transparent
        layout.Appearance.Options.UseBackColor = True

        layout.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Relative, 50.0F))
        layout.Rows.Add(New TablePanelRow(TablePanelEntityStyle.Absolute, MainHeaderRowPixels))
        layout.Rows.Add(New TablePanelRow(TablePanelEntityStyle.Relative, 100.0F))

        _bodyHost = New PanelControl() With {.Dock = DockStyle.Fill, .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder}
        _bodyHost.Appearance.BackColor = Color.Transparent
        _bodyHost.Appearance.Options.UseBackColor = True
        _bodyViewHost = New PanelControl() With {.Dock = DockStyle.Fill, .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder}
        _bodyViewHost.Appearance.BackColor = Color.Transparent
        _bodyViewHost.Appearance.Options.UseBackColor = True
        _bodyPrevHintHost = CreateBodyEdgeHintHost("bodyPrevHintHost")
        _bodyNextHintHost = CreateBodyEdgeHintHost("bodyNextHintHost")
        Dim bodyLayout = New TablePanel With {
            .Dock = DockStyle.Fill,
            .Name = "apptHostBodyTable",
            .Padding = Padding.Empty,
            .Margin = Padding.Empty
        }
        bodyLayout.Appearance.BackColor = Color.Transparent
        bodyLayout.Appearance.Options.UseBackColor = True
        bodyLayout.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Absolute, BodyEdgeHintBandWidth))
        bodyLayout.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Relative, 100.0F))
        bodyLayout.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Absolute, BodyEdgeHintBandWidth))
        bodyLayout.Rows.Add(New TablePanelRow(TablePanelEntityStyle.Relative, 100.0F))
        bodyLayout.Controls.Add(_bodyPrevHintHost)
        bodyLayout.Controls.Add(_bodyViewHost)
        bodyLayout.Controls.Add(_bodyNextHintHost)
        bodyLayout.SetColumn(_bodyPrevHintHost, 0)
        bodyLayout.SetRow(_bodyPrevHintHost, 0)
        bodyLayout.SetColumn(_bodyViewHost, 1)
        bodyLayout.SetRow(_bodyViewHost, 0)
        bodyLayout.SetColumn(_bodyNextHintHost, 2)
        bodyLayout.SetRow(_bodyNextHintHost, 0)
        _bodyHost.Controls.Add(bodyLayout)

        _header.Dock = DockStyle.Fill
        _header.Margin = Padding.Empty

        layout.Controls.Add(_bodyHost)
        layout.Controls.Add(_header)
        layout.SetColumn(_bodyHost, 0)
        layout.SetRow(_bodyHost, 1)
        layout.SetColumn(_header, 0)
        layout.SetRow(_header, 0)
        Return layout
    End Function

    Private Shared Function CreateBodyEdgeHintHost(name As String) As PanelControl
        Dim host = New PanelControl With {
            .Name = name,
            .Dock = DockStyle.Fill,
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        }
        host.Appearance.BackColor = Color.Transparent
        host.Appearance.Options.UseBackColor = True
        Return host
    End Function

    Private Sub WireHostEvents()
        AddHandler Load, Sub() LoadAndRender()

        AddHandler _interactionHub.AppointmentClicked, Sub(ap) RaiseEvent AppointmentClicked(ap)
        AddHandler _interactionHub.AppointmentDoubleClicked, Sub(ap) RaiseEvent AppointmentDoubleClicked(ap)
        AddHandler _interactionHub.EmptyDateInvoked, AddressOf OnEmptyDateInvokedFromHub
        AddHandler _interactionHub.AppointmentStatusChangeRequested, AddressOf OnAppointmentStatusChangeFromHub
        AddHandler _interactionHub.WeekColumnAppointmentDrop, AddressOf OnWeekColumnAppointmentDrop
        AddHandler _interactionHub.AppointmentTimeChangeRequested, AddressOf OnAppointmentTimeChangeFromHub

        AddHandler _header.PrevRequested, AddressOf NavigatePrevious
        AddHandler _header.NextRequested, AddressOf NavigateNext
        AddHandler _header.TodayRequested, Sub() UpdateState(Sub() _state.CurrentDate = Date.Today)
        AddHandler _header.AddRequested, AddressOf CreateAppointmentFromHeader
        AddHandler _header.WeekSnapshotRequested, AddressOf OnWeekSnapshotRequested
        AddHandler _header.CurrentDateChanged, Sub(value) UpdateState(Sub() _state.CurrentDate = value.Date)
        AddHandler _header.ViewChanged, AddressOf OnHeaderViewChanged
        AddHandler _header.Use24Changed, Sub(value) UpdateState(Sub() _state.Use24HourFormat = value)
        AddHandler _header.DoctorFilterChanged, Sub(value) UpdateState(Sub() _state.DoctorFilterId = value)
        AddHandler _header.StatusFilterChanged, Sub(value) UpdateState(Sub() _state.VisibleStatus = value)
        AddHandler _header.AllPatientsRequested,
            Sub()
                UpdateState(
                    Sub()
                        _state.PatientOnlyMode = False
                        _state.PatientFilterId = Nothing
                    End Sub)
            End Sub
        AddHandler _header.ThisPatientRequested,
            Sub()
                UpdateState(
                    Sub()
                        If _loadedPatientId.HasValue Then
                            _state.PatientOnlyMode = True
                            _state.PatientFilterId = _loadedPatientId
                        End If
                    End Sub)
            End Sub
        AddHandler _header.IncludeReasonChanged, Sub(enabled) UpdateState(Sub() _state.IncludeReason = enabled)
        AddHandler _header.FontProfileChanged,
            Sub(useBold, useLarge)
                UpdateState(
                    Sub()
                        _state.UseBoldAppointments = useBold
                        _state.UseLargeAppointments = useLarge
                    End Sub)
            End Sub
        AddHandler _header.WorkingHoursChanged,
            Sub(startTime, endTime)
                UpdateState(
                    Sub()
                        _state.WorkStartTime = startTime
                        _state.WorkEndTime = If(endTime <= startTime, startTime.Add(TimeSpan.FromHours(1)), endTime)
                        Try
                            My.Settings.SchedulerWorkingDayStart = _state.WorkStartTime
                            My.Settings.SchedulerWorkingDayEnd = _state.WorkEndTime
                            My.Settings.Save()
                        Catch ex As Exception
                            ApptErrorHelper.Report(ex, "ApptHostCtl.WorkingHoursChanged.SaveSettings", showUser:=False)
                        End Try
                    End Sub)
            End Sub
        AddHandler _header.ApptCardTimeColorsChanged, AddressOf OnApptCardTimeColorsChanged
        AddHandler _header.ApptCardLabelColorsChanged, AddressOf OnApptCardLabelColorsChanged
        AddHandler _header.PrevViewRequested, AddressOf NavigateViewBack
        AddHandler _header.NextViewRequested, AddressOf NavigateViewForward
    End Sub

    Private Shared Function CurrentProcessUserHandleCount() As Integer
        Try
            Return GetGuiResources(Process.GetCurrentProcess().Handle, GrUserObjects)
        Catch
            Return -1
        End Try
    End Function

    Private Shared Function CurrentProcessGdiHandleCount() As Integer
        Try
            Return GetGuiResources(Process.GetCurrentProcess().Handle, GrGdiObjects)
        Catch
            Return -1
        End Try
    End Function

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

    Private Sub LogResourceSnapshot(stage As String)
        Try
            ApptErrorHelper.ReportDiagnostic(
                "ApptHostCtl.ResourceSnapshot",
                $"{stage}; USER={CurrentProcessUserHandleCount()}; GDI={CurrentProcessGdiHandleCount()}; POPUPS={CountTransientPopupForms()}; OPEN_FORMS={Application.OpenForms.Count}; CURRENT_VIEW={If(_currentView Is Nothing, "(none)", _currentView.GetType().Name)}")
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptHostCtl.LogResourceSnapshot", showUser:=False)
        End Try
    End Sub

    Private Sub UpdateState(mutate As Action)
        If mutate Is Nothing Then Return
        Try
            _suppressRefresh = True
            mutate()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.UpdateState",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not apply the appointment view change.",
                                   arabicMessage:="تعذر تطبيق تغيير عرض المواعيد.")
        Finally
            _suppressRefresh = False
        End Try
        LoadAndRender()
    End Sub

    ''' <summary>Mirrors <see cref="SchedulerNew.UpdateAppointmentCtatus"/> for modular cards (full refresh after save).</summary>
    Private Sub OnEmptyDateInvokedFromHub(clickedAt As DateTime)
        Dim start = clickedAt
        If start.TimeOfDay = TimeSpan.Zero Then
            start = start.Date.Add(_state.WorkStartTime)
        End If
        CreateAppointmentAt(start)
    End Sub

    Private Sub OnWeekColumnAppointmentDrop(appt As AppointmentC, sourceDay As DateTime, targetDay As DateTime)
        Try
            If appt Is Nothing Then Return
            If sourceDay.Date = targetDay.Date Then
                XtraMessageBox.Show(
                    If(Eng, "Appointment is already on this day.", "الموعد موجود بالفعل في هذا اليوم."),
                    If(Eng, "No Move Needed", "لا حاجة للنقل"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
                Return
            End If
            If _provider.TryMoveAppointmentToDate(appt, targetDay.Date) Then
                XtraMessageBox.Show(
                    If(Eng, "Appointment moved successfully!", "تم نقل الموعد بنجاح!"),
                    If(Eng, "Success", "نجاح"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
                LoadAndRender()
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.OnWeekColumnAppointmentDrop",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not move the appointment.",
                                   arabicMessage:="تعذر نقل الموعد.")
        End Try
    End Sub

    Private Sub OnAppointmentStatusChangeFromHub(appt As AppointmentC, newStatus As String, statusColor As Color)
        If appt Is Nothing Then Return
        appt.Status = newStatus
        Try
            _provider.UpdateAppointment(appt)
            Dim statusMsg = If(Eng, newStatus, TranslateAppointmentStatus(newStatus))
            XtraMessageBox.Show(
                If(Eng, $"Status updated to {newStatus}", $"تم تحديث الحالة إلى {statusMsg}"),
                If(Eng, "Success", "نجاح"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
            LoadAndRender()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.OnAppointmentStatusChangeFromHub",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not update the appointment status.",
                                   arabicMessage:="تعذر تحديث حالة الموعد.")
        End Try
    End Sub

    Private Sub OnHeaderViewChanged(newView As ApptViewMode)
        Dim previous = _state.CurrentView
        If Not _suppressViewNavRecording AndAlso previous <> newView Then
            _viewNavBack.Push(previous)
            _viewNavForward.Clear()
        End If
        UpdateState(Sub() _state.CurrentView = newView)
    End Sub

    Private Sub NavigateViewBack()
        If _viewNavBack.Count = 0 Then Return
        _viewNavForward.Push(_state.CurrentView)
        Dim target = _viewNavBack.Pop()
        ApplyViewFromNavigationStack(target)
    End Sub

    Private Sub NavigateViewForward()
        If _viewNavForward.Count = 0 Then Return
        _viewNavBack.Push(_state.CurrentView)
        Dim target = _viewNavForward.Pop()
        ApplyViewFromNavigationStack(target)
    End Sub

    Private Sub ApplyViewFromNavigationStack(target As ApptViewMode)
        _suppressViewNavRecording = True
        Try
            _state.CurrentView = target
            _header.SyncViewComboToState(_state)
        Finally
            _suppressViewNavRecording = False
        End Try
        LoadAndRender()
    End Sub

    Private Sub SyncViewNavButtonsVisibility()
        _header.UpdateViewNavButtonVisibility(_viewNavBack.Count, _viewNavForward.Count)
    End Sub

    Private Sub OnApptCardTimeColorsChanged(startColor As Color, endColor As Color)
        My.Settings.ApptModuleCardStartTimeColor = startColor
        My.Settings.ApptModuleCardEndTimeColor = endColor
        My.Settings.Save()
        UpdateState(
            Sub()
                _state.ApptCardStartTimeColor = startColor
                _state.ApptCardEndTimeColor = endColor
            End Sub)
    End Sub

    Private Sub OnApptCardLabelColorsChanged(patientNameColor As Color, reasonColor As Color, notesColor As Color)
        My.Settings.ApptModuleCardPatientNameColor = patientNameColor
        My.Settings.ApptModuleCardReasonColor = reasonColor
        My.Settings.ApptModuleCardNotesColor = notesColor
        My.Settings.Save()
        UpdateState(
            Sub()
                _state.ApptCardPatientNameColor = patientNameColor
                _state.ApptCardReasonColor = reasonColor
                _state.ApptCardNotesColor = notesColor
            End Sub)
    End Sub

    Private Sub NavigatePrevious()
        UpdateState(
            Sub()
                Select Case _state.CurrentView
                    Case ApptViewMode.DayView, ApptViewMode.DoctorsDay
                        _state.CurrentDate = _state.CurrentDate.AddDays(-1)
                    Case ApptViewMode.ThisWeek, ApptViewMode.ThisWeekFull, ApptViewMode.DaysTimeline
                        _state.CurrentDate = _state.CurrentDate.AddDays(-7)
                    Case ApptViewMode.MonthlyWeek
                        _state.CurrentDate = _state.CurrentDate.AddDays(-7)
                    Case Else
                        _state.CurrentDate = _state.CurrentDate.AddMonths(-1)
                End Select
            End Sub)
    End Sub

    Private Sub NavigateNext()
        UpdateState(
            Sub()
                Select Case _state.CurrentView
                    Case ApptViewMode.DayView, ApptViewMode.DoctorsDay
                        _state.CurrentDate = _state.CurrentDate.AddDays(1)
                    Case ApptViewMode.ThisWeek, ApptViewMode.ThisWeekFull, ApptViewMode.DaysTimeline
                        _state.CurrentDate = _state.CurrentDate.AddDays(7)
                    Case ApptViewMode.MonthlyWeek
                        _state.CurrentDate = _state.CurrentDate.AddDays(7)
                    Case Else
                        _state.CurrentDate = _state.CurrentDate.AddMonths(1)
                End Select
            End Sub)
    End Sub

    Private Sub CaptureLayoutRowMetricsOnce()
        If _layoutRowsCaptured Then Return
        If _layout Is Nothing OrElse _layout.Rows.Count < 2 Then Return
        _layoutRow0Style = _layout.Rows(0).Style
        _layoutRow0Height = _layout.Rows(0).Height
        _layoutRow1Style = _layout.Rows(1).Style
        _layoutRow1Height = _layout.Rows(1).Height
        _layoutRowsCaptured = True
    End Sub

    Private Sub EnsureBodyWorkspaceExpandButton()
        If _btnBodyExpand IsNot Nothing Then Return
        CaptureLayoutRowMetricsOnce()
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
        AddHandler _bodyHost.SizeChanged, AddressOf BodyHost_ChangedForExpandBtnLayout
        AddHandler _bodyHost.LocationChanged, AddressOf BodyHost_ChangedForExpandBtnLayout
        If _bodyViewHost IsNot Nothing Then AddHandler _bodyViewHost.SizeChanged, AddressOf BodyHost_ChangedForExpandBtnLayout
        Controls.Add(_btnBodyExpand)
        Controls.Add(_btnBodyQuickAdd)
        SyncBodyExpandButtonVisual()
        LayoutBodyWorkspaceExpandButton()
    End Sub

    Private Sub BodyQuickAddButton_Click(sender As Object, e As EventArgs)
        CreateAppointmentFromHeader()
    End Sub

    Private Sub SyncBodyQuickAddButtonVisual()
        If _btnBodyQuickAdd Is Nothing Then Return
        _btnBodyQuickAdd.Text = If(Eng, "Add", "إضافة")
        _btnBodyQuickAdd.ToolTip = If(Eng, "Add appointment (same as toolbar)", "إضافة موعد (كزر شريط الأدوات)")
        _btnBodyQuickAdd.Visible = _bodyWorkspaceMaximized
    End Sub

    Private Sub BodyHost_ChangedForExpandBtnLayout(sender As Object, e As EventArgs)
        LayoutBodyWorkspaceExpandButton()
    End Sub

    ''' <summary>Top-right overlay on the body host; parented to the host control so <c>_bodyHost.Controls.Clear()</c> never removes it. Quick-add sits left of expand when expanded.</summary>
    Private Sub LayoutBodyWorkspaceExpandButton()
        If _btnBodyExpand Is Nothing OrElse _bodyHost Is Nothing Then Return
        Const pad As Integer = 3
        Const gap As Integer = 4
        Dim targetHost = If(_bodyViewHost, _bodyHost)
        Dim clientY = pad
        Dim expandClientX = targetHost.ClientSize.Width - _btnBodyExpand.Width - pad
        Dim ptExpand = PointToClient(targetHost.PointToScreen(New Point(expandClientX, clientY)))
        _btnBodyExpand.Location = ptExpand
        _btnBodyExpand.BringToFront()
        If _btnBodyQuickAdd IsNot Nothing Then
            Dim quickClientX = expandClientX - gap - _btnBodyQuickAdd.Width
            Dim ptQuick = PointToClient(targetHost.PointToScreen(New Point(quickClientX, clientY)))
            _btnBodyQuickAdd.Location = ptQuick
            _btnBodyQuickAdd.BringToFront()
        End If
    End Sub

    Private Sub SyncBodyExpandButtonVisual()
        If _btnBodyExpand Is Nothing Then Return
        If _bodyWorkspaceMaximized Then
            _btnBodyExpand.Text = "▼"
            _btnBodyExpand.ToolTip = If(Eng, "Restore header", "استعادة الرأس والفلاتر")
        Else
            _btnBodyExpand.Text = "▲"
            _btnBodyExpand.ToolTip = If(Eng, "Expand schedule (full height)", "توسيع الجدول بكامل الارتفاع")
        End If
        SyncBodyQuickAddButtonVisual()
    End Sub

    Private Sub ApplyBodyWorkspaceMaximizedState()
        CaptureLayoutRowMetricsOnce()
        If _layout Is Nothing OrElse _layout.Rows.Count < 2 OrElse Not _layoutRowsCaptured Then Return
        SuspendLayout()
        _layout.SuspendLayout()
        If _header IsNot Nothing Then _header.SuspendLayout()
        If _bodyHost IsNot Nothing Then _bodyHost.SuspendLayout()
        Try
            If _bodyWorkspaceMaximized Then
                _header.Visible = False
                _layout.Rows(0).Style = TablePanelEntityStyle.Absolute
                _layout.Rows(0).Height = 2.0F
                _layout.Rows(1).Style = TablePanelEntityStyle.Relative
                _layout.Rows(1).Height = 100.0F
            Else
                _header.Visible = True
                _layout.Rows(0).Style = _layoutRow0Style
                _layout.Rows(0).Height = _layoutRow0Height
                _layout.Rows(1).Style = _layoutRow1Style
                _layout.Rows(1).Height = _layoutRow1Height
            End If
        Finally
            If _bodyHost IsNot Nothing Then _bodyHost.ResumeLayout(False)
            If _header IsNot Nothing Then _header.ResumeLayout(False)
            _layout.ResumeLayout(False)
            ResumeLayout(False)
        End Try
        _layout.PerformLayout()
        If _header IsNot Nothing Then _header.PerformLayout()
        If _bodyHost IsNot Nothing Then _bodyHost.PerformLayout()
        PerformLayout()
        LayoutBodyWorkspaceExpandButton()
    End Sub

    Private Sub BodyExpandButton_Click(sender As Object, e As EventArgs)
        _bodyWorkspaceMaximized = Not _bodyWorkspaceMaximized
        SyncBodyExpandButtonVisual()
        ApplyBodyWorkspaceMaximizedState()
        LoadAndRender()
        LayoutBodyWorkspaceExpandButton()
    End Sub

    Public Sub EnsureBodyWorkspaceExpanded()
        If _bodyWorkspaceMaximized Then Return
        _bodyWorkspaceMaximized = True
        SyncBodyExpandButtonVisual()
        ApplyBodyWorkspaceMaximizedState()
        LoadAndRender()
        LayoutBodyWorkspaceExpandButton()
    End Sub

    Public Sub EnsureBodyWorkspaceCollapsed()
        If Not _bodyWorkspaceMaximized Then Return
        _bodyWorkspaceMaximized = False
        SyncBodyExpandButtonVisual()
        ApplyBodyWorkspaceMaximizedState()
        LoadAndRender()
        LayoutBodyWorkspaceExpandButton()
    End Sub

    Public ReadOnly Property IsBodyWorkspaceExpanded As Boolean
        Get
            Return _bodyWorkspaceMaximized
        End Get
    End Property

    Protected Overrides Sub OnLayout(levent As LayoutEventArgs)
        MyBase.OnLayout(levent)
        PositionBodyEdgeHints()
        LayoutBodyWorkspaceExpandButton()
    End Sub

    ''' <summary><see cref="SchedulerNew.InvalidatePnlBodyEdgeHintsAfterClear"/> — avoid using disposed hint controls.</summary>
    Private Sub InvalidateBodyEdgeHintsIfDisposed()
        If _bodyPrevHint IsNot Nothing AndAlso _bodyPrevHint.IsDisposed Then _bodyPrevHint = Nothing
        If _bodyNextHint IsNot Nothing AndAlso _bodyNextHint.IsDisposed Then _bodyNextHint = Nothing
    End Sub

    Private Sub EnsureBodyEdgeHints()
        InvalidateBodyEdgeHintsIfDisposed()
        If _bodyPrevHint Is Nothing Then
            _bodyPrevHint = New ArrowLable With {
                .AutoSize = False,
                .Width = BodyEdgeHintWidth,
                .Height = BodyEdgeHintHeight,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Text = If(Eng, "PREV", "السابق"),
                .Visible = False,
                .Cursor = Cursors.Hand,
                .ShowLeftTriangle = True,
                .ShowRightTriangle = False,
                .TextDirection = ArrowLable.ArrowTextDirection.BottomToTop,
                .GlassBaseColor = Color.FromArgb(70, 160, 255),
                .GlassAccentColor = Color.FromArgb(140, 80, 220),
                .TriangleColor = Color.FromArgb(230, 255, 140, 120),
                .ForeColor = Color.White
            }
            AddHandler _bodyPrevHint.Click, Sub() BodyEdgePrev_Click()
        End If
        If _bodyNextHint Is Nothing Then
            _bodyNextHint = New ArrowLable With {
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
                .GlassBaseColor = Color.FromArgb(70, 160, 255),
                .GlassAccentColor = Color.FromArgb(140, 80, 220),
                .TriangleColor = Color.FromArgb(230, 255, 140, 120),
                .ForeColor = Color.White
            }
            AddHandler _bodyNextHint.Click, Sub() BodyEdgeNext_Click()
        End If
        ' Reserve fixed left/right bands for the global PREV/NEXT hints so the active view never sits underneath them.
        If _bodyPrevHintHost IsNot Nothing AndAlso _bodyNextHintHost IsNot Nothing Then
            If _bodyPrevHint IsNot Nothing AndAlso _bodyPrevHint.Parent IsNot Nothing AndAlso Not Object.ReferenceEquals(_bodyPrevHint.Parent, _bodyPrevHintHost) Then
                _bodyPrevHint.Parent.Controls.Remove(_bodyPrevHint)
            End If
            If _bodyNextHint IsNot Nothing AndAlso _bodyNextHint.Parent IsNot Nothing AndAlso Not Object.ReferenceEquals(_bodyNextHint.Parent, _bodyNextHintHost) Then
                _bodyNextHint.Parent.Controls.Remove(_bodyNextHint)
            End If
            If _bodyPrevHint IsNot Nothing AndAlso _bodyPrevHint.Parent Is Nothing Then
                _bodyPrevHintHost.Controls.Add(_bodyPrevHint)
            End If
            If _bodyNextHint IsNot Nothing AndAlso _bodyNextHint.Parent Is Nothing Then
                _bodyNextHintHost.Controls.Add(_bodyNextHint)
            End If
            _bodyEdgeHintsInBody = True
        ElseIf _bodyHost IsNot Nothing Then
            If _bodyPrevHint IsNot Nothing AndAlso _bodyPrevHint.Parent IsNot Nothing AndAlso Not Object.ReferenceEquals(_bodyPrevHint.Parent, _bodyHost) Then
                _bodyPrevHint.Parent.Controls.Remove(_bodyPrevHint)
            End If
            If _bodyNextHint IsNot Nothing AndAlso _bodyNextHint.Parent IsNot Nothing AndAlso Not Object.ReferenceEquals(_bodyNextHint.Parent, _bodyHost) Then
                _bodyNextHint.Parent.Controls.Remove(_bodyNextHint)
            End If
            If _bodyPrevHint IsNot Nothing AndAlso _bodyPrevHint.Parent Is Nothing Then
                _bodyHost.Controls.Add(_bodyPrevHint)
            End If
            If _bodyNextHint IsNot Nothing AndAlso _bodyNextHint.Parent Is Nothing Then
                _bodyHost.Controls.Add(_bodyNextHint)
            End If
            _bodyEdgeHintsInBody = True
        Else
            If _bodyPrevHint IsNot Nothing AndAlso _bodyPrevHint.Parent Is Nothing Then
                Controls.Add(_bodyPrevHint)
            End If
            If _bodyNextHint IsNot Nothing AndAlso _bodyNextHint.Parent Is Nothing Then
                Controls.Add(_bodyNextHint)
            End If
        End If
        If _bodyPrevHint IsNot Nothing Then _bodyPrevHint.BringToFront()
        If _bodyNextHint IsNot Nothing Then _bodyNextHint.BringToFront()
        If _bodyHost IsNot Nothing AndAlso Not _bodyEdgeHostEventsWired Then
            AddHandler _bodyHost.SizeChanged, AddressOf BodyHost_ChangedForEdgeHints
            If _bodyPrevHintHost IsNot Nothing Then AddHandler _bodyPrevHintHost.SizeChanged, AddressOf BodyHost_ChangedForEdgeHints
            If _bodyNextHintHost IsNot Nothing Then AddHandler _bodyNextHintHost.SizeChanged, AddressOf BodyHost_ChangedForEdgeHints
            _bodyEdgeHostEventsWired = True
        End If
        PositionBodyEdgeHints()
    End Sub

    Private Sub BodyHost_ChangedForEdgeHints(sender As Object, e As EventArgs)
        PositionBodyEdgeHints()
    End Sub

    ''' <summary>PREV/NEXT on the body’s left/right (children of <see cref="_bodyHost"/>; same idea as <see cref="SchedulerNew.PositionWeekEdgeHints"/>).</summary>
    Private Sub PositionBodyEdgeHints()
        If _bodyPrevHint Is Nothing OrElse _bodyNextHint Is Nothing OrElse _bodyHost Is Nothing Then Return
        If _bodyHost.ClientSize.Width < 1 OrElse _bodyHost.ClientSize.Height < 1 Then Return
        If _bodyPrevHintHost IsNot Nothing AndAlso _bodyNextHintHost IsNot Nothing AndAlso
           _bodyPrevHint.Parent Is _bodyPrevHintHost AndAlso _bodyNextHint.Parent Is _bodyNextHintHost Then
            Dim prevX = Math.Max(0, (_bodyPrevHintHost.ClientSize.Width - _bodyPrevHint.Width) \ 2)
            Dim prevY = Math.Max(0, (_bodyPrevHintHost.ClientSize.Height - _bodyPrevHint.Height) \ 2)
            Dim nextX = Math.Max(0, (_bodyNextHintHost.ClientSize.Width - _bodyNextHint.Width) \ 2)
            Dim nextY = Math.Max(0, (_bodyNextHintHost.ClientSize.Height - _bodyNextHint.Height) \ 2)
            _bodyPrevHint.Location = New Point(prevX, prevY)
            _bodyNextHint.Location = New Point(nextX, nextY)
        ElseIf _bodyEdgeHintsInBody AndAlso _bodyPrevHint.Parent Is _bodyHost AndAlso _bodyNextHint.Parent Is _bodyHost Then
            Dim midY = Math.Max(0, (_bodyHost.ClientSize.Height - _bodyPrevHint.Height) \ 2)
            _bodyPrevHint.Location = New Point(0, midY)
            _bodyNextHint.Location = New Point(Math.Max(0, _bodyHost.ClientSize.Width - _bodyNextHint.Width), midY)
        Else
            Dim midY = Math.Max(0, (_bodyHost.ClientSize.Height - _bodyPrevHint.Height) \ 2)
            Dim ptL = PointToClient(_bodyHost.PointToScreen(New Point(0, midY)))
            Dim ptR = PointToClient(_bodyHost.PointToScreen(New Point(_bodyHost.ClientSize.Width - _bodyNextHint.Width, midY)))
            _bodyPrevHint.Location = ptL
            _bodyNextHint.Location = ptR
        End If
        _bodyPrevHint.BringToFront()
        _bodyNextHint.BringToFront()
    End Sub

    Private Sub ApplyBodyEdgeHintVisualThemeDay()
        If _bodyPrevHint Is Nothing OrElse _bodyNextHint Is Nothing Then Return
        _bodyPrevHint.GlassBaseColor = Color.FromArgb(70, 160, 255)
        _bodyPrevHint.GlassAccentColor = Color.FromArgb(140, 80, 220)
        _bodyPrevHint.TriangleColor = Color.FromArgb(230, 255, 140, 120)
        _bodyNextHint.GlassBaseColor = Color.FromArgb(70, 160, 255)
        _bodyNextHint.GlassAccentColor = Color.FromArgb(140, 80, 220)
        _bodyNextHint.TriangleColor = Color.FromArgb(230, 255, 140, 120)
    End Sub

    Private Sub ApplyBodyEdgeHintVisualThemeWeek()
        If _bodyPrevHint Is Nothing OrElse _bodyNextHint Is Nothing Then Return
        _bodyPrevHint.GlassBaseColor = Color.FromArgb(80, 210, 180)
        _bodyPrevHint.GlassAccentColor = Color.FromArgb(120, 90, 230)
        _bodyPrevHint.TriangleColor = Color.FromArgb(235, 255, 200, 90)
        _bodyNextHint.GlassBaseColor = Color.FromArgb(80, 210, 180)
        _bodyNextHint.GlassAccentColor = Color.FromArgb(120, 90, 230)
        _bodyNextHint.TriangleColor = Color.FromArgb(235, 255, 200, 90)
    End Sub

    Private Sub ApplyBodyEdgeHintVisualThemeMonth()
        If _bodyPrevHint Is Nothing OrElse _bodyNextHint Is Nothing Then Return
        _bodyPrevHint.GlassBaseColor = Color.FromArgb(255, 150, 80)
        _bodyPrevHint.GlassAccentColor = Color.FromArgb(255, 90, 160)
        _bodyPrevHint.TriangleColor = Color.FromArgb(230, 120, 200, 255)
        _bodyNextHint.GlassBaseColor = Color.FromArgb(255, 150, 80)
        _bodyNextHint.GlassAccentColor = Color.FromArgb(255, 90, 160)
        _bodyNextHint.TriangleColor = Color.FromArgb(230, 120, 200, 255)
    End Sub

    Private Sub BodyEdgePrev_Click()
        If Not _edgeNavPrevAnchor.HasValue Then Return
        _pendingScrollAppt = _edgeNavPrevScrollAppt
        Dim d = _edgeNavPrevAnchor.Value
        UpdateState(Sub() _state.CurrentDate = d.Date)
    End Sub

    Private Sub BodyEdgeNext_Click()
        If Not _edgeNavNextAnchor.HasValue Then Return
        _pendingScrollAppt = _edgeNavNextScrollAppt
        Dim d = _edgeNavNextAnchor.Value
        UpdateState(Sub() _state.CurrentDate = d.Date)
    End Sub

    ''' <summary>Mirrors <see cref="SchedulerNew"/> day / week / month / timeline edge hints using the loaded appointment list.</summary>
    Private Sub UpdateBodyEdgeHints()
        EnsureBodyEdgeHints()
        If _bodyPrevHint Is Nothing OrElse _bodyNextHint Is Nothing Then Return

        _bodyPrevHint.Visible = False
        _bodyNextHint.Visible = False
        _edgeNavPrevAnchor = Nothing
        _edgeNavNextAnchor = Nothing
        _edgeNavPrevScrollAppt = Nothing
        _edgeNavNextScrollAppt = Nothing

        _bodyPrevHint.Text = If(Eng, "PREV", "السابق")
        _bodyNextHint.Text = If(Eng, "NEXT", "التالي")

        Dim appts = _edgeHintAppointments
        If appts Is Nothing OrElse appts.Count = 0 Then
            PositionBodyEdgeHints()
            Return
        End If

        Select Case _state.CurrentView
            Case ApptViewMode.DayView, ApptViewMode.DoctorsDay
                ApplyBodyEdgeHintVisualThemeDay()
                Dim cur = _state.CurrentDate.Date
                Dim prevDate = appts.Select(Function(a) a.AppDate.Date).Where(Function(d) d < cur).OrderByDescending(Function(d) d).FirstOrDefault()
                Dim nextDate = appts.Select(Function(a) a.AppDate.Date).Where(Function(d) d > cur).OrderBy(Function(d) d).FirstOrDefault()
                If prevDate <> Date.MinValue Then
                    _edgeNavPrevAnchor = prevDate.Date
                    _edgeNavPrevScrollAppt = appts.Where(Function(a) a.AppDate.Date = prevDate).OrderBy(Function(a) a.StartDateTime).FirstOrDefault()
                    _bodyPrevHint.Visible = True
                End If
                If nextDate <> Date.MinValue Then
                    _edgeNavNextAnchor = nextDate.Date
                    _edgeNavNextScrollAppt = appts.Where(Function(a) a.AppDate.Date = nextDate).OrderBy(Function(a) a.StartDateTime).FirstOrDefault()
                    _bodyNextHint.Visible = True
                End If

            Case ApptViewMode.ThisWeek, ApptViewMode.ThisWeekFull, ApptViewMode.DaysTimeline
                ApplyBodyEdgeHintVisualThemeWeek()
                Dim daysInWeek = If(_state.CurrentView = ApptViewMode.ThisWeek, 6, 7)
                Dim currentStart = GetWeekStartSaturday(_state.CurrentDate)
                Dim endExclusive = currentStart.AddDays(daysInWeek)
                Dim hintDays = appts.Select(Function(a) a.StartDateTime.Date)
                Dim prevD = hintDays.Where(Function(d) d < currentStart).OrderByDescending(Function(d) d).FirstOrDefault()
                Dim nextD = hintDays.Where(Function(d) d >= endExclusive).OrderBy(Function(d) d).FirstOrDefault()
                If prevD <> Date.MinValue Then
                    Dim wk = GetWeekStartSaturday(prevD)
                    _edgeNavPrevAnchor = wk
                    _edgeNavPrevScrollAppt = appts.Where(Function(a) a.StartDateTime.Date >= wk AndAlso a.StartDateTime.Date < wk.AddDays(daysInWeek)).OrderBy(Function(a) a.StartDateTime).FirstOrDefault()
                    _bodyPrevHint.Visible = True
                End If
                If nextD <> Date.MinValue Then
                    Dim wkN = GetWeekStartSaturday(nextD)
                    ' 6-day week shows Sat..Thu; the first "outside" day is Fri (start+6), still the same GetWeekStartSaturday.
                    ' Without this, the NEXT target equals current week and the strip appears stuck. Jump to the following block (next Sat+7 like header Nav).
                    If _state.CurrentView = ApptViewMode.ThisWeek AndAlso daysInWeek = 6 AndAlso wkN = currentStart Then
                        wkN = currentStart.AddDays(7)
                    End If
                    _edgeNavNextAnchor = wkN
                    _edgeNavNextScrollAppt = appts.Where(Function(a) a.StartDateTime.Date >= wkN AndAlso a.StartDateTime.Date < wkN.AddDays(daysInWeek)).OrderBy(Function(a) a.StartDateTime).FirstOrDefault()
                    _bodyNextHint.Visible = True
                End If

            Case ApptViewMode.MonthlyWeek
                ApplyBodyEdgeHintVisualThemeWeek()
                Dim vr = GetViewRange(_state)
                Dim rangeStart = vr.StartDate.Date
                Dim rangeEndEx = rangeStart.AddDays(7)
                Dim hintDaysW = appts.Select(Function(a) a.StartDateTime.Date)
                Dim prevW = hintDaysW.Where(Function(d) d < rangeStart).OrderByDescending(Function(d) d).FirstOrDefault()
                Dim nextW = hintDaysW.Where(Function(d) d >= rangeEndEx).OrderBy(Function(d) d).FirstOrDefault()
                If prevW <> Date.MinValue Then
                    _edgeNavPrevAnchor = GetWeekStartSunday(prevW)
                    _bodyPrevHint.Visible = True
                End If
                If nextW <> Date.MinValue Then
                    _edgeNavNextAnchor = GetWeekStartSunday(nextW)
                    _bodyNextHint.Visible = True
                End If

            Case ApptViewMode.MonthView
                ApplyBodyEdgeHintVisualThemeMonth()
                Dim startOfMonth = New DateTime(_state.CurrentDate.Year, _state.CurrentDate.Month, 1)
                Dim endExclusiveM = startOfMonth.AddMonths(1)
                Dim prevM = appts.Select(Function(a) a.AppDate.Date).Where(Function(d) d < startOfMonth).OrderByDescending(Function(d) d).FirstOrDefault()
                Dim nextM = appts.Select(Function(a) a.AppDate.Date).Where(Function(d) d >= endExclusiveM).OrderBy(Function(d) d).FirstOrDefault()
                If prevM <> Date.MinValue Then
                    _edgeNavPrevAnchor = New DateTime(prevM.Year, prevM.Month, 1)
                    _bodyPrevHint.Visible = True
                End If
                If nextM <> Date.MinValue Then
                    _edgeNavNextAnchor = New DateTime(nextM.Year, nextM.Month, 1)
                    _bodyNextHint.Visible = True
                End If
        End Select

        PositionBodyEdgeHints()
        _bodyPrevHint.BringToFront()
        _bodyNextHint.BringToFront()
    End Sub

    Private Sub CreateAppointmentFromHeader()
        CreateAppointmentAt(_state.CurrentDate.Date.Add(_state.WorkStartTime))
    End Sub

    Private Sub CreateAppointmentAt(startDate As DateTime)
        Dim patientId = If(CurrentPatient IsNot Nothing, CurrentPatient.PatientID, 0)
        If patientId <= 0 AndAlso Not AllowAddWithoutCurrentPatient Then
            XtraMessageBox.Show(If(Eng, "Select a patient first.", "اختر مريضاً اولاً"))
            Return
        End If

        Dim appointment = New AppointmentC With {
            .PatientID = patientId,
            .DrID = If(_state.DoctorFilterId.HasValue, _state.DoctorFilterId.Value, 0),
            .AppDate = startDate.Date,
            .StartDateTime = startDate,
            .EndDateTime = startDate.AddMinutes(30),
            .Status = "Pending",
            .CreatedAt = Date.Now,
            .CreatedBy = Environment.UserName,
            .WhatsIncludeReason = True,
            .WhatsIncludeNotes = True
        }

        Using editor As New AppointCEditorForm(appointment, True)
            If editor.ShowDialog(FindForm()) = DialogResult.OK Then
                AddAppointment(editor.AppointmentC)
            End If
        End Using
    End Sub

    Private Sub EnsureActiveView()
        Dim target = CType(_state.CurrentView, ApptViewMode)
        Dim expectedType = _viewFactory.Create(target).GetType()
        If _currentView IsNot Nothing AndAlso _currentView.GetType() Is expectedType Then
            Return
        End If

        If _currentView IsNot Nothing Then
            Dim currentParent = _currentView.Parent
            If currentParent IsNot Nothing Then
                currentParent.Controls.Remove(_currentView)
            End If
            _currentView.Dispose()
            _currentView = Nothing
        End If

        _currentView = _viewFactory.Create(target)
        _currentView.Dock = DockStyle.Fill
        Dim modularView = TryCast(_currentView, IApptViewCtl)
        If modularView IsNot Nothing Then
            modularView.InteractionHub = _interactionHub
        End If
        ' One layout/paint when swapping views; week view in particular is control-heavy. Revert: remove this block.
        Dim viewHost = If(_bodyViewHost, _bodyHost)
        viewHost.SuspendLayout()
        Try
            viewHost.Controls.Add(_currentView)
        Finally
            viewHost.ResumeLayout(True)
        End Try
    End Sub

    Public Sub SetFilters(patientId As Integer?, DrID As Integer?, reason As String, start As DateTime, [end] As DateTime)
        Try
            _state.PatientFilterId = patientId
            _state.DoctorFilterId = DrID
            _state.VisibleReason = reason
            _state.FilterStartDate = start
            _state.FilterEndDate = [end]
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.SetFilters",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not apply the appointment filters.",
                                   arabicMessage:="تعذر تطبيق فلاتر المواعيد.")
        Finally
            LoadAndRender()
        End Try
    End Sub

    Public Sub SetView(view As ViewMode)
        Try
            Dim newView = CType(view, ApptViewMode)
            Dim previous = _state.CurrentView
            If previous <> newView Then
                _viewNavBack.Push(previous)
                _viewNavForward.Clear()
            End If
            _suppressViewNavRecording = True
            _state.CurrentView = newView
            _header.SyncViewComboToState(_state)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.SetView",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not switch the appointment view.",
                                   arabicMessage:="تعذر تغيير عرض المواعيد.")
        Finally
            _suppressViewNavRecording = False
            LoadAndRender()
        End Try
    End Sub

    Public Sub LoadAndRender()
        If _suppressRefresh OrElse _provider Is Nothing Then Return

        Try
            If _state.PatientOnlyMode Then
                _state.PatientFilterId = _loadedPatientId
            End If

            Dim stateSnapshot = _state.Clone()
            _edgeHintAppointments = _provider.LoadEdgeHintAppointments(stateSnapshot)
            _currentData = _provider.Load(stateSnapshot)
            EnsureActiveView()

            Dim scrollPending = _pendingScrollAppt
            _pendingScrollAppt = Nothing

            Dim modularView = TryCast(_currentView, IApptViewCtl)
            If modularView IsNot Nothing Then
                modularView.BindData(New ApptViewRequest With {
                    .State = stateSnapshot.Clone(),
                    .Data = _currentData,
                    .AppointmentAppearanceSelector = AppointmentAppearanceSelector,
                    .PendingScrollAppointment = scrollPending,
                    .DragHoldTimeMs = DragHoldTimeMs
                })
            End If

            _header.ApplyState(stateSnapshot.Clone(), _currentData, BuildPatientCaption(), CurrentPatient IsNot Nothing)
            SyncViewNavButtonsVisibility()
            UpdateBodyEdgeHints()
            If _btnBodyExpand IsNot Nothing Then
                SyncBodyExpandButtonVisual()
                LayoutBodyWorkspaceExpandButton()
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.LoadAndRender",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not refresh the appointment view.",
                                   arabicMessage:="تعذر تحديث عرض المواعيد.")
        End Try
    End Sub

    Public Sub AddAppointment(appt As AppointmentC, Optional reminderMessageEnglish As Boolean? = Nothing)
        Try
            If appt Is Nothing Then Return
            _provider.AddAppointment(appt, reminderMessageEnglish)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.AddAppointment",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not add the appointment.",
                                   arabicMessage:="تعذر إضافة الموعد.")
        Finally
            LoadAndRender()
        End Try
    End Sub

    Public Sub UpdateAppointment(appt As AppointmentC, Optional reminderMessageEnglish As Boolean? = Nothing)
        Try
            If appt Is Nothing Then Return
            _provider.UpdateAppointment(appt, reminderMessageEnglish)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.UpdateAppointment",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not update the appointment.",
                                   arabicMessage:="تعذر تحديث الموعد.")
        Finally
            LoadAndRender()
        End Try
    End Sub

    Public Sub DeleteAppointment(apptId As Integer)
        Try
            If apptId <= 0 Then Return
            _provider.DeleteAppointment(apptId)
            RaiseEvent AppointmentDeleted(apptId)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.DeleteAppointment",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not delete the appointment.",
                                   arabicMessage:="تعذر حذف الموعد.")
        Finally
            LoadAndRender()
        End Try
    End Sub

    Friend Function TryCaptureWeekSnapshotBitmap(anchorDate As Date, weekOffset As Integer, clearFiltersForBroadcast As Boolean,
                                                 ByRef weekCaptionOut As String) As Bitmap
        Dim dummyHtml As String = Nothing
        Return TryCaptureWeekSnapshotBitmap(anchorDate, weekOffset, clearFiltersForBroadcast, weekCaptionOut,
                                            alsoWritePng:=True, alsoWriteHtml:=False, htmlExportDir:=Nothing, weekHtmlFilePathOut:=dummyHtml)
    End Function

    Friend Function TryCaptureWeekSnapshotBitmap(anchorDate As Date, weekOffset As Integer, clearFiltersForBroadcast As Boolean,
                                                 ByRef weekCaptionOut As String,
                                                 alsoWritePng As Boolean, alsoWriteHtml As Boolean, htmlExportDir As String,
                                                 ByRef weekHtmlFilePathOut As String) As Bitmap
        weekCaptionOut = Nothing
        weekHtmlFilePathOut = Nothing
        Dim savedState = _state.Clone()

        Try
            If clearFiltersForBroadcast Then
                _state.PatientOnlyMode = False
                _state.PatientFilterId = Nothing
                _state.DoctorFilterId = Nothing
                _state.VisibleReason = Nothing
                _state.VisibleStatus = Nothing
            End If

            _state.CurrentView = ApptViewMode.ThisWeekFull
            _state.CurrentDate = GetWeekStartSaturday(anchorDate.Date).AddDays(weekOffset * 7)
            LoadAndRender()
            Application.DoEvents()
            weekCaptionOut = BuildRangeCaption(_state, _currentData)
            Dim bmp As Bitmap = Nothing
            If alsoWritePng Then
                bmp = SchedulerSnapshotShared.CaptureSnapshot(If(_bodyEdgeHintsInBody, _bodyHost, CType(Me, Control)), _bodyHost, CType(_state.CurrentView, SchedulerNew.ViewMode), Nothing)
            End If
            If alsoWriteHtml AndAlso Not String.IsNullOrWhiteSpace(htmlExportDir) Then
                Try
                    Directory.CreateDirectory(htmlExportDir)
                Catch
                End Try
                Try
                    Dim req = New ApptViewRequest With {
                        .State = _state.Clone(),
                        .Data = _currentData,
                        .AppointmentAppearanceSelector = AppointmentAppearanceSelector,
                        .DragHoldTimeMs = DragHoldTimeMs
                    }
                    Dim ctx = ApptSnapshotHtmlBuilder.BuildForApptModule(req, _currentView, weekCaptionOut)
                    If ctx IsNot Nothing Then
                        Dim wk0 = GetWeekStartSaturday(anchorDate.Date).AddDays(weekOffset * 7)
                        Dim wkEndF = wk0.AddDays(6)
                        Dim fn = $"Snapshot-{wk0:yyyyMMdd}-{wkEndF:yyyyMMdd}.html"
                        weekHtmlFilePathOut = Path.Combine(htmlExportDir, fn)
                        File.WriteAllText(weekHtmlFilePathOut, WeekSnapshotHtmlWriter.BuildDocument(ctx), New System.Text.UTF8Encoding(False))
                    End If
                Catch ex As Exception
                    weekHtmlFilePathOut = Nothing
                    ApptErrorHelper.Report(ex, "ApptHostCtl.TryCaptureWeekSnapshotBitmap.HtmlExport", showUser:=False)
                End Try
            End If
            Return bmp
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.TryCaptureWeekSnapshotBitmap",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not capture the schedule snapshot.",
                                   arabicMessage:="تعذر التقاط لقطة الجدول.")
            Return Nothing
        Finally
            _state.CurrentDate = savedState.CurrentDate
            _state.CurrentView = savedState.CurrentView
            _state.WorkStartTime = savedState.WorkStartTime
            _state.WorkEndTime = savedState.WorkEndTime
            _state.Use24HourFormat = savedState.Use24HourFormat
            _state.ShowPatientLabels = savedState.ShowPatientLabels
            _state.IncludeReason = savedState.IncludeReason
            _state.UseBoldAppointments = savedState.UseBoldAppointments
            _state.UseLargeAppointments = savedState.UseLargeAppointments
            _state.DoctorFilterId = savedState.DoctorFilterId
            _state.PatientFilterId = savedState.PatientFilterId
            _state.VisibleReason = savedState.VisibleReason
            _state.VisibleStatus = savedState.VisibleStatus
            _state.FilterStartDate = savedState.FilterStartDate
            _state.FilterEndDate = savedState.FilterEndDate
            _state.PatientOnlyMode = savedState.PatientOnlyMode
            LoadAndRender()
        End Try
    End Function

    ''' <summary>Same root folder as <see cref="SchedulerNew"/> snapshots: <c>Application.StartupPath\Attachments</c>.</summary>
    Private Shared Function GetApptModuleExportsDirectory() As String
        Return Path.Combine(Application.StartupPath, "Attachments")
    End Function

    ''' <summary>Stable English file prefix per view, aligned with <see cref="SchedulerNew"/> <c>GetSchedulerSnapshotFileNamePrefix</c>.</summary>
    Private Function GetApptModuleSnapshotFileNamePrefix() As String
        Select Case _state.CurrentView
            Case ApptViewMode.DayView
                Return "DailyView"
            Case ApptViewMode.ThisWeekFull
                Return "WeekView7Days"
            Case ApptViewMode.ThisWeek
                Return "WeekView6Days"
            Case ApptViewMode.MonthlyWeek
                Return "MonthweeklyView"
            Case ApptViewMode.MonthView
                Return "MonthView"
            Case ApptViewMode.DaysTimeline
                Return "DayTimeLineView"
            Case ApptViewMode.DoctorsDay
                Return "DoctorsDayView"
            Case Else
                Return "ScheduleView"
        End Select
    End Function

    ''' <summary>Mirrors <see cref="SchedulerNew.btnWeekSnapshot_Click"/>: capture current body, refresh, then preview/save/WhatsApp.</summary>
    Private Sub OnWeekSnapshotRequested()
        Dim bmp As Bitmap = Nothing
        Try
            Application.DoEvents()
            ApptErrorHelper.SafeRefresh(_bodyHost, "ApptHostCtl.OnWeekSnapshotRequested.RefreshBodyHost")
            Application.DoEvents()
            bmp = SchedulerSnapshotShared.CaptureSnapshot(If(_bodyEdgeHintsInBody, _bodyHost, CType(Me, Control)), _bodyHost, CType(_state.CurrentView, SchedulerNew.ViewMode), Nothing)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.OnWeekSnapshotRequested.Capture",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not capture the current schedule view.",
                                   arabicMessage:="تعذر التقاط عرض الجدول الحالي.")
        Finally
            LoadAndRender()
        End Try

        If bmp Is Nothing Then
            XtraMessageBox.Show(
                If(Eng, "Could not capture the schedule view.", "تعذر التقاط الصورة."),
                If(Eng, "Snapshot", "لقطة"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
            Return
        End If

        Dim caption = BuildRangeCaption(_state, _currentData)
        Dim htmlContext As WeekSnapshotHtmlContext = Nothing
        Try
            Dim req = New ApptViewRequest With {
                .State = _state.Clone(),
                .Data = _currentData,
                .AppointmentAppearanceSelector = AppointmentAppearanceSelector,
                .DragHoldTimeMs = DragHoldTimeMs
            }
            htmlContext = ApptSnapshotHtmlBuilder.BuildForApptModule(req, _currentView, caption)
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptHostCtl.OnWeekSnapshotRequested.HtmlContext", showUser:=False)
        End Try
        Dim owner = TryCast(FindForm(), IWin32Window)
        Using preview As New SchedulerWeekSnapshotPreviewForm(
            bmp,
            GetApptModuleExportsDirectory(),
            caption,
            _state.PatientFilterId,
            GetApptModuleSnapshotFileNamePrefix(),
            htmlContext)
            If owner IsNot Nothing Then
                preview.ShowDialog(owner)
            Else
                preview.ShowDialog()
            End If
        End Using
    End Sub

    Private Function BuildPatientCaption() As String
        If Not _state.ShowPatientLabels Then Return ""
        If _state.PatientOnlyMode AndAlso CurrentPatient IsNot Nothing Then
            Return CurrentPatient.PatientName
        End If
        Return If(Eng, "All Patients", "كل المرضى")
    End Function

    Private Sub SyncCurrentPatientFromForm(patientId As Integer)
        Dim parentWs = PatientAwareHelper.FindPatientWorkspace(Me)
        If parentWs IsNot Nothing AndAlso parentWs.Current_Patient IsNot Nothing AndAlso parentWs.Current_Patient.PatientID = patientId Then
            CurrentPatient = parentWs.Current_Patient
        ElseIf FormManager.Instance.IsBasePatientFormOpen AndAlso FormManager.Instance.GetCurrentPatient() IsNot Nothing AndAlso FormManager.Instance.GetCurrentPatient().PatientID = patientId Then
            CurrentPatient = FormManager.Instance.GetCurrentPatient()
        Else
            CurrentPatient = Nothing
        End If
    End Sub

    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        LoadPatientData(patientId)
    End Sub

    Public Sub LoadPatientData(patientId As Integer)
        Try
            _loadedPatientId = patientId
            SyncCurrentPatientFromForm(patientId)
            If _state.PatientOnlyMode Then
                _state.PatientFilterId = patientId
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.LoadPatientData",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not load patient appointment data.",
                                   arabicMessage:="تعذر تحميل بيانات مواعيد المريض.")
        Finally
            LoadAndRender()
        End Try
    End Sub

    Private Sub OnAppointmentTimeChangeFromHub(appt As AppointmentC, newStart As DateTime, newEnd As DateTime)
        Try
            If appt Is Nothing Then Return
            If _provider.TryUpdateAppointmentTimes(appt, newStart, newEnd) Then
                LoadAndRender()
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHostCtl.OnAppointmentTimeChangeFromHub",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not change the appointment time.",
                                   arabicMessage:="تعذر تغيير وقت الموعد.")
        End Try
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            LogResourceSnapshot("before-dispose")

            Try
                RemoveHandler _interactionHub.EmptyDateInvoked, AddressOf OnEmptyDateInvokedFromHub
                RemoveHandler _interactionHub.AppointmentStatusChangeRequested, AddressOf OnAppointmentStatusChangeFromHub
                RemoveHandler _interactionHub.WeekColumnAppointmentDrop, AddressOf OnWeekColumnAppointmentDrop
                RemoveHandler _interactionHub.AppointmentTimeChangeRequested, AddressOf OnAppointmentTimeChangeFromHub
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "ApptHostCtl.Dispose.InteractionHubHandlers", showUser:=False)
            End Try

            Try
                RemoveHandler _header.PrevRequested, AddressOf NavigatePrevious
                RemoveHandler _header.NextRequested, AddressOf NavigateNext
                RemoveHandler _header.AddRequested, AddressOf CreateAppointmentFromHeader
                RemoveHandler _header.WeekSnapshotRequested, AddressOf OnWeekSnapshotRequested
                RemoveHandler _header.ViewChanged, AddressOf OnHeaderViewChanged
                RemoveHandler _header.ApptCardTimeColorsChanged, AddressOf OnApptCardTimeColorsChanged
                RemoveHandler _header.ApptCardLabelColorsChanged, AddressOf OnApptCardLabelColorsChanged
                RemoveHandler _header.PrevViewRequested, AddressOf NavigateViewBack
                RemoveHandler _header.NextViewRequested, AddressOf NavigateViewForward
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "ApptHostCtl.Dispose.HeaderHandlers", showUser:=False)
            End Try

            Try
                If _btnBodyExpand IsNot Nothing Then RemoveHandler _btnBodyExpand.Click, AddressOf BodyExpandButton_Click
                If _btnBodyQuickAdd IsNot Nothing Then RemoveHandler _btnBodyQuickAdd.Click, AddressOf BodyQuickAddButton_Click
                If _bodyHost IsNot Nothing Then
                    RemoveHandler _bodyHost.SizeChanged, AddressOf BodyHost_ChangedForExpandBtnLayout
                    RemoveHandler _bodyHost.LocationChanged, AddressOf BodyHost_ChangedForExpandBtnLayout
                    RemoveHandler _bodyHost.SizeChanged, AddressOf BodyHost_ChangedForEdgeHints
                End If
                If _bodyViewHost IsNot Nothing Then RemoveHandler _bodyViewHost.SizeChanged, AddressOf BodyHost_ChangedForExpandBtnLayout
                If _bodyPrevHintHost IsNot Nothing Then RemoveHandler _bodyPrevHintHost.SizeChanged, AddressOf BodyHost_ChangedForEdgeHints
                If _bodyNextHintHost IsNot Nothing Then RemoveHandler _bodyNextHintHost.SizeChanged, AddressOf BodyHost_ChangedForEdgeHints
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "ApptHostCtl.Dispose.BodyHandlers", showUser:=False)
            End Try

            Try
                If _currentView IsNot Nothing Then
                    Dim currentParent = _currentView.Parent
                    If currentParent IsNot Nothing Then
                        currentParent.Controls.Remove(_currentView)
                    End If
                    _currentView.Dispose()
                    _currentView = Nothing
                End If
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "ApptHostCtl.Dispose.CurrentView", showUser:=False)
            End Try

            Try
                If _bodyHost IsNot Nothing Then DisposeChildControls(_bodyHost)
                If _bodyViewHost IsNot Nothing Then DisposeChildControls(_bodyViewHost)
                If _bodyPrevHintHost IsNot Nothing Then DisposeChildControls(_bodyPrevHintHost)
                If _bodyNextHintHost IsNot Nothing Then DisposeChildControls(_bodyNextHintHost)
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "ApptHostCtl.Dispose.ClearChildHosts", showUser:=False)
            End Try

            LogResourceSnapshot("after-dispose-cleanup")
        End If

        MyBase.Dispose(disposing)
    End Sub
End Class
