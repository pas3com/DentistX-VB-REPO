Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports DevExpress.Utils.Layout
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

''' <summary>
''' Standalone 7-day week schedule: chrome header plus embedded <see cref="ApptWeekCtl"/> (cards rendered internally there).
''' Holds local <see cref="ApptState"/>, <see cref="ApptDataProvider"/>, and <see cref="ApptInteractionHub"/> — does not require <see cref="ApptHostCtl"/>.
''' </summary>
Public Class FullWeekSched
    Inherits XtraUserControl
    Implements IPatientAwareUserControl

    Private Const StandaloneHeaderRowPx As Single = 85.0F
    Private Const BodyEdgeHintWidth As Integer = 46
    Private Const BodyEdgeHintHeight As Integer = 90
    Private Const BodyEdgeHintHostPadding As Integer = 6
    Private Const BodyEdgeHintBandWidth As Single = BodyEdgeHintWidth + (BodyEdgeHintHostPadding * 2)
    ''' <summary>Batches filter-strip PerformLayout during continuous resize (reduces lookup/grid jitter).</summary>
    Private Const ChromeResizeDebounceMs As Integer = 72

    Private _syncing As Boolean
    Private _suppressRefresh As Boolean
    Private _weekSnapshotToolTip As ToolTip

    Private ReadOnly _state As New ApptState()
    Private ReadOnly _interactionHub As New ApptInteractionHub()
    Private _provider As ApptDataProvider
    Private ReadOnly _weekView As New ApptWeekCtl()
    Private ReadOnly _bodyHost As New PanelControl()
    Private _bodyViewHost As PanelControl
    Private _bodyPrevHintHost As PanelControl
    Private _bodyNextHintHost As PanelControl
    Private _bodyInnerLayout As TablePanel
    Private _rootLayout As TablePanel

    ''' <summary>Monotonic render generation so stale async edge-hint loads never repaint after a newer refresh.</summary>
    Private _renderGeneration As Integer

    Private _edgeHintAppointments As List(Of AppointmentC)
    ''' <summary>Set after appointment mutations so PREV/NEXT hint SQL runs again (navigation-only refreshes reuse cached list).</summary>
    Private _invalidateEdgeHintsCache As Boolean
    Private _edgeHintsCacheFilterSig As String
    Private _edgeHintsCacheList As List(Of AppointmentC)
    Private _chromeResizeDebounce As Global.System.Windows.Forms.Timer
    Private _bodyPrevHint As ArrowLable
    Private _bodyNextHint As ArrowLable
    Private _pendingScrollAppt As AppointmentC
    Private _edgeNavPrevAnchor As DateTime?
    Private _edgeNavNextAnchor As DateTime?
    Private _edgeNavPrevScrollAppt As AppointmentC
    Private _edgeNavNextScrollAppt As AppointmentC
    Private _bodyEdgeHostEventsWired As Boolean

    Private _btnBodyExpand As SimpleButton
    Private _bodyWorkspaceMaximized As Boolean = True
    Private _layoutRowsCaptured As Boolean
    Private _layoutRow0Style As TablePanelEntityStyle
    Private _layoutRow0Height As Single
    Private _layoutRow1Style As TablePanelEntityStyle
    Private _layoutRow1Height As Single
    Private _currentData As ApptDataBundle
    Private _loadedPatientId As Integer?
    Private _currentPatient As Patient

    Private _cardPatientNameColor As Color
    Private _cardReasonColor As Color
    Private _cardNotesColor As Color

    Private NotInheritable Class PatientPickRow
        Public Property PatientID As Integer
        Public Property PatientName As String
    End Class

    Private NotInheritable Class StatusPickRow
        Public Property KeyStr As String
        Public Property Display As String
    End Class

    Public Event AppointmentClicked As Action(Of AppointmentC)
    Public Event AppointmentDoubleClicked As Action(Of AppointmentC)
    Public Event AppointmentDeleted As Action(Of Integer)

    Public Property AppointmentAppearanceSelector As Func(Of ApptCardVm, ApptCardAppearance)
    Public Property AllowAddWithoutCurrentPatient As Boolean = True
    Public Property DragHoldTimeMs As Integer = 750

    Public Property CurrentPatient As Patient
        Get
            Return _currentPatient
        End Get
        Private Set(value As Patient)
            _currentPatient = value
        End Set
    End Property

    Public Sub New()
        Me.New(New AppointmentCRepository(DentistXDATA.GetConnection.ConnectionString))
    End Sub

    Public Sub New(repo As AppointmentCRepository)
        Try
            InitializeComponent()
            Dock = DockStyle.Fill

            _state.ApptCardStartTimeColor = My.Settings.ApptModuleCardStartTimeColor
            _state.ApptCardEndTimeColor = My.Settings.ApptModuleCardEndTimeColor
            _state.ApptCardPatientNameColor = My.Settings.ApptModuleCardPatientNameColor
            _state.ApptCardReasonColor = My.Settings.ApptModuleCardReasonColor
            _state.ApptCardNotesColor = My.Settings.ApptModuleCardNotesColor
            _state.CurrentView = ApptViewMode.ThisWeekFull
            Try
                Dim ordDr = My.Settings.ApptModuleOrderByDoctorId
                _state.OrderByDoctorId = If(ordDr > 0, CType(ordDr, Integer?), Nothing)
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "FullWeekSched.New.OrderByDoctorId", showUser:=False)
            End Try
            ApplyWorkingHoursFromSettings()

            _provider = New ApptDataProvider(repo)

            BuildStandaloneShell()
            _weekView.Dock = DockStyle.Fill
            _weekView.InteractionHub = _interactionHub

            WireInteractionHubHandlers()
            LocalizeStaticText()
            ConfigureWeekOnlyChrome()
            ConfigureLookupEditors()
            WireStandaloneHeaderHandlers()

            AddHandler Resize, AddressOf Header_Resize
            pnlHeader.AutoScroll = True
            legendPanel.AutoScroll = True

            _chromeResizeDebounce = New Global.System.Windows.Forms.Timer With {.Interval = ChromeResizeDebounceMs, .Enabled = False}
            AddHandler _chromeResizeDebounce.Tick, AddressOf ChromeResizeDebounce_Tick

            InitializeApptCardTimeColorPickersFromSettings()
            InitializeApptCardLabelColorCacheFromSettings()
            InitializeWeekSnapshotButton()

            EnsureBodyEdgeHints()
            EnsureBodyWorkspaceExpandButton()

            AddHandler Load, Sub(sender, e)
                                 ApplyDefaultBodyWorkspaceExpandedState()
                                 LoadAndRender()
                             End Sub
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.New",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="The full-week scheduler could not be initialized.",
                                   arabicMessage:="تعذر تهيئة جدول الأسبوع الكامل.")
        End Try
    End Sub

    Private Sub BuildStandaloneShell()
        Controls.Remove(pnlHeader)

        _rootLayout = New TablePanel With {
            .Dock = DockStyle.Fill,
            .Name = "fullWeekRootTable",
            .Padding = Padding.Empty,
            .Margin = Padding.Empty
        }
        _rootLayout.Appearance.BackColor = Color.Transparent
        _rootLayout.Appearance.Options.UseBackColor = True
        _rootLayout.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Relative, 100.0F))
        _rootLayout.Rows.Add(New TablePanelRow(TablePanelEntityStyle.Absolute, StandaloneHeaderRowPx))
        _rootLayout.Rows.Add(New TablePanelRow(TablePanelEntityStyle.Relative, 100.0F))

        pnlHeader.Dock = DockStyle.Fill
        _rootLayout.Controls.Add(pnlHeader)
        _rootLayout.SetColumn(pnlHeader, 0)
        _rootLayout.SetRow(pnlHeader, 0)

        _bodyHost.Dock = DockStyle.Fill
        _bodyHost.BorderStyle = BorderStyles.NoBorder
        _bodyHost.Appearance.BackColor = Color.Transparent
        _bodyHost.Appearance.Options.UseBackColor = True
        _bodyHost.Name = "fullWeekBodyHost"

        _bodyPrevHintHost = CreateBodyEdgeHintHost("fullWeekPrevHintHost")
        _bodyNextHintHost = CreateBodyEdgeHintHost("fullWeekNextHintHost")
        _bodyViewHost = New PanelControl With {
            .Dock = DockStyle.Fill,
            .BorderStyle = BorderStyles.NoBorder,
            .Name = "fullWeekBodyViewHost"
        }
        _bodyViewHost.Appearance.BackColor = Color.Transparent
        _bodyViewHost.Appearance.Options.UseBackColor = True

        _bodyInnerLayout = New TablePanel With {
            .Dock = DockStyle.Fill,
            .Name = "fullWeekBodyInnerTable",
            .Padding = Padding.Empty,
            .Margin = Padding.Empty
        }
        _bodyInnerLayout.Appearance.BackColor = Color.Transparent
        _bodyInnerLayout.Appearance.Options.UseBackColor = True
        _bodyInnerLayout.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Absolute, BodyEdgeHintBandWidth))
        _bodyInnerLayout.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Relative, 100.0F))
        _bodyInnerLayout.Columns.Add(New TablePanelColumn(TablePanelEntityStyle.Absolute, BodyEdgeHintBandWidth))
        _bodyInnerLayout.Rows.Add(New TablePanelRow(TablePanelEntityStyle.Relative, 100.0F))
        _bodyInnerLayout.Controls.Add(_bodyPrevHintHost)
        _bodyInnerLayout.Controls.Add(_bodyViewHost)
        _bodyInnerLayout.Controls.Add(_bodyNextHintHost)
        _bodyInnerLayout.SetColumn(_bodyPrevHintHost, 0)
        _bodyInnerLayout.SetRow(_bodyPrevHintHost, 0)
        _bodyInnerLayout.SetColumn(_bodyViewHost, 1)
        _bodyInnerLayout.SetRow(_bodyViewHost, 0)
        _bodyInnerLayout.SetColumn(_bodyNextHintHost, 2)
        _bodyInnerLayout.SetRow(_bodyNextHintHost, 0)

        _bodyHost.Controls.Add(_bodyInnerLayout)
        _bodyViewHost.Controls.Add(_weekView)

        _rootLayout.Controls.Add(_bodyHost)
        _rootLayout.SetColumn(_bodyHost, 0)
        _rootLayout.SetRow(_bodyHost, 1)

        Controls.Add(_rootLayout)
    End Sub

    Private Shared Function CreateBodyEdgeHintHost(name As String) As PanelControl
        Dim host = New PanelControl With {
            .Name = name,
            .Dock = DockStyle.Fill,
            .BorderStyle = BorderStyles.NoBorder
        }
        host.Appearance.BackColor = Color.Transparent
        host.Appearance.Options.UseBackColor = True
        Return host
    End Function

    Private Sub ApplyWorkingHoursFromSettings()
        Try
            Dim s = My.Settings.SchedulerWorkingDayStart
            Dim e_ = My.Settings.SchedulerWorkingDayEnd
            If s < TimeSpan.Zero OrElse s >= TimeSpan.FromDays(1) Then Return
            If e_ <= TimeSpan.Zero OrElse e_ > TimeSpan.FromDays(1) OrElse e_ <= s Then Return
            _state.WorkStartTime = s
            _state.WorkEndTime = e_
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FullWeekSched.ApplyWorkingHoursFromSettings", showUser:=False)
        End Try
    End Sub

    Private Sub ConfigureWeekOnlyChrome()
        If cmbView IsNot Nothing Then cmbView.Visible = False
        If btnPrevView IsNot Nothing Then btnPrevView.Visible = False
        If btnNextView IsNot Nothing Then btnNextView.Visible = False
    End Sub

    Private Sub WireInteractionHubHandlers()
        AddHandler _interactionHub.AppointmentClicked, Sub(ap) RaiseEvent AppointmentClicked(ap)
        AddHandler _interactionHub.AppointmentDoubleClicked, Sub(ap) RaiseEvent AppointmentDoubleClicked(ap)
        AddHandler _interactionHub.EmptyDateInvoked, AddressOf OnEmptyDateInvokedFromHub
        AddHandler _interactionHub.AppointmentStatusChangeRequested, AddressOf OnAppointmentStatusChangeFromHub
        AddHandler _interactionHub.WeekColumnAppointmentDrop, AddressOf OnWeekColumnAppointmentDrop
        AddHandler _interactionHub.AppointmentTimeChangeRequested, AddressOf OnAppointmentTimeChangeFromHub
    End Sub

    Private Sub WireStandaloneHeaderHandlers()
        AddHandler btnPrev.Click, Sub() SafeRaiseHeaderAction("FullWeekSched.NavigateWeekPrev", AddressOf NavigateWeekPrevious)
        AddHandler btnNext.Click, Sub() SafeRaiseHeaderAction("FullWeekSched.NavigateWeekNext", AddressOf NavigateWeekNext)
        AddHandler btnToday.Click, Sub() SafeRaiseHeaderAction("FullWeekSched.NavigateWeekToday", AddressOf NavigateWeekToday)
        AddHandler btnAdd.Click, Sub() SafeRaiseHeaderAction("FullWeekSched.AddAppointment", AddressOf CreateAppointmentFromToolbar)
        AddHandler chkUse24.CheckedChanged, Sub() SafeRaiseHeaderAction("FullWeekSched.Use24", AddressOf OnUse24ChangedInternal)
        AddHandler gotoDate.EditValueChanged, Sub() SafeRaiseHeaderAction("FullWeekSched.GotoDate", AddressOf OnGotoDateInternal)
        AddHandler includeReasonCheck.CheckedChanged, Sub() SafeRaiseHeaderAction("FullWeekSched.IncludeReason", AddressOf OnIncludeReasonInternal)
        AddHandler boldFontCheck.CheckedChanged, Sub() SafeRaiseHeaderAction("FullWeekSched.FontBold", AddressOf OnFontProfileInternal)
        AddHandler sizeFontCheck.CheckedChanged, Sub() SafeRaiseHeaderAction("FullWeekSched.FontSize", AddressOf OnFontProfileInternal)
        AddHandler dtStartTime.EditValueChanged, AddressOf WorkingHours_EditValueChanged
        AddHandler dtEndTime.EditValueChanged, AddressOf WorkingHours_EditValueChanged
        If startColor IsNot Nothing Then AddHandler startColor.EditValueChanged, AddressOf ApptCardTimeColorPickers_EditValueChanged
        If endColor IsNot Nothing Then AddHandler endColor.EditValueChanged, AddressOf ApptCardTimeColorPickers_EditValueChanged
        If btnLabelsColors IsNot Nothing Then AddHandler btnLabelsColors.Click, AddressOf BtnLabelsColors_Click

        If lookUpDoctors IsNot Nothing Then AddHandler lookUpDoctors.EditValueChanged, AddressOf LookUpDoctors_EditValueChanged
        If LookUpPatient IsNot Nothing Then AddHandler LookUpPatient.EditValueChanged, AddressOf LookUpPatient_EditValueChanged
        If LookUpStatus IsNot Nothing Then AddHandler LookUpStatus.EditValueChanged, AddressOf LookUpStatus_EditValueChanged
    End Sub

    Private Sub NavigateWeekPrevious()
        UpdateState(Sub() _state.CurrentDate = _state.CurrentDate.AddDays(-7))
    End Sub

    Private Sub NavigateWeekNext()
        UpdateState(Sub() _state.CurrentDate = _state.CurrentDate.AddDays(7))
    End Sub

    Private Sub NavigateWeekToday()
        UpdateState(Sub() _state.CurrentDate = Date.Today)
    End Sub

    Private Sub OnUse24ChangedInternal()
        If _syncing Then Return
        UpdateState(Sub() _state.Use24HourFormat = chkUse24.Checked)
    End Sub

    Private Sub OnGotoDateInternal()
        If _syncing OrElse gotoDate.EditValue Is Nothing Then Return
        UpdateState(Sub() _state.CurrentDate = Convert.ToDateTime(gotoDate.EditValue).Date)
    End Sub

    Private Sub OnIncludeReasonInternal()
        If _syncing Then Return
        UpdateState(Sub() _state.IncludeReason = includeReasonCheck.Checked)
    End Sub

    Private Sub OnFontProfileInternal()
        If _syncing Then Return
        UpdateState(Sub()
                        _state.UseBoldAppointments = boldFontCheck.Checked
                        _state.UseLargeAppointments = sizeFontCheck.Checked
                    End Sub)
    End Sub

    ''' <summary>Wide-range PREV/NEXT query filters match this signature — reused across week navigation until invalidated.</summary>
    Private Shared Function BuildEdgeHintCacheSignature(state As ApptState) As String
        If state Is Nothing Then Return "|"
        Dim pf = If(state.PatientFilterId.HasValue, state.PatientFilterId.Value.ToString(CultureInfo.InvariantCulture), "")
        Dim df = If(state.DoctorFilterId.HasValue, state.DoctorFilterId.Value.ToString(CultureInfo.InvariantCulture), "")
        Dim vr = If(state.VisibleReason, "")
        Dim vs = If(state.VisibleStatus, "")
        Dim pom = If(state.PatientOnlyMode, "1", "0")
        Return pom & "|" & pf & "|" & df & "|" & vr & "|" & vs
    End Function

    Private Sub InvalidateEdgeHintsCache()
        _invalidateEdgeHintsCache = True
    End Sub

    ''' <summary>If appointments changed elsewhere while filters stayed the same, call before <see cref="RefreshSchedule"/> so PREV/NEXT hints reload.</summary>
    Public Sub InvalidateWeekNavigationCaches()
        InvalidateEdgeHintsCache()
    End Sub

    Private Sub DisposeChromeResizeDebouncer()
        If _chromeResizeDebounce Is Nothing Then Return
        Try
            _chromeResizeDebounce.Stop()
            RemoveHandler _chromeResizeDebounce.Tick, AddressOf ChromeResizeDebounce_Tick
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FullWeekSched.DisposeChromeResizeDebouncer.RemoveHandlers", showUser:=False)
        End Try
        Try
            _chromeResizeDebounce.Dispose()
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FullWeekSched.DisposeChromeResizeDebouncer.Dispose", showUser:=False)
        End Try
        _chromeResizeDebounce = Nothing
    End Sub

    Private Sub UpdateState(mutate As Action)
        If mutate Is Nothing Then Return
        Try
            _suppressRefresh = True
            mutate()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.UpdateState",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not apply the appointment change.",
                                   arabicMessage:="تعذر تطبيق التغيير على المواعيد.")
        Finally
            _suppressRefresh = False
        End Try
        LoadAndRender()
    End Sub

    Public Sub LoadAndRender()
        If _suppressRefresh OrElse _provider Is Nothing Then Return
        Dim gen = Interlocked.Increment(_renderGeneration)
        Try
            Dim stateSnapshot = _state.Clone()
            Dim filterSig = BuildEdgeHintCacheSignature(stateSnapshot)

            If _invalidateEdgeHintsCache Then
                _invalidateEdgeHintsCache = False
                _edgeHintsCacheList = Nothing
                _edgeHintsCacheFilterSig = Nothing
            End If

            Dim edgeHintsCacheHit = _edgeHintsCacheList IsNot Nothing AndAlso
                _edgeHintsCacheFilterSig IsNot Nothing AndAlso
                String.Equals(_edgeHintsCacheFilterSig, filterSig, StringComparison.Ordinal)

            _edgeHintAppointments = Nothing
            UpdateBodyEdgeHints()

            _currentData = _provider.Load(stateSnapshot)

            Dim scrollPending = _pendingScrollAppt
            _pendingScrollAppt = Nothing

            _weekView.InteractionHub = _interactionHub

            If _bodyViewHost IsNot Nothing Then _bodyViewHost.SuspendLayout()
            Try
                _weekView.BindData(New ApptViewRequest With {
                    .State = stateSnapshot.Clone(),
                    .Data = _currentData,
                    .AppointmentAppearanceSelector = AppointmentAppearanceSelector,
                    .PendingScrollAppointment = scrollPending,
                    .DragHoldTimeMs = DragHoldTimeMs,
                    .NavigateDatePrevious = Sub() SafeRaiseHeaderAction("FullWeekSched.NavigateWeekPrev", AddressOf NavigateWeekPrevious),
                    .NavigateDateNext = Sub() SafeRaiseHeaderAction("FullWeekSched.NavigateWeekNext", AddressOf NavigateWeekNext),
                    .NavigateAppointmentPrevious = Sub() BodyEdgePrev_Click(),
                    .NavigateAppointmentNext = Sub() BodyEdgeNext_Click(),
                    .ToggleBodyWorkspaceExpand = AddressOf ToggleBodyWorkspaceExpandedInternal,
                    .QuickAddAppointment = Sub() SafeRaiseHeaderAction("FullWeekSched.AddAppointment", AddressOf CreateAppointmentFromToolbar),
                    .IsBodyWorkspaceExpanded = _bodyWorkspaceMaximized
                })
            Finally
                If _bodyViewHost IsNot Nothing Then _bodyViewHost.ResumeLayout(True)
            End Try

            RefreshChrome(stateSnapshot.Clone(), _currentData)

            If edgeHintsCacheHit Then
                _edgeHintAppointments = _edgeHintsCacheList
                UpdateBodyEdgeHints()
                If _btnBodyExpand IsNot Nothing Then
                    SyncBodyExpandButtonVisual()
                    LayoutBodyWorkspaceExpandButton()
                End If
            Else
                Dim hintState = stateSnapshot.Clone()
                Dim prov = _provider
                Dim capturedSig = filterSig
                Task.Factory.StartNew(Function() As List(Of AppointmentC)
                                          Try
                                              Return prov.LoadEdgeHintAppointments(hintState)
                                          Catch ex As Exception
                                              ApptErrorHelper.Report(ex, "FullWeekSched.LoadEdgeHintsAsync", showUser:=False)
                                              Return New List(Of AppointmentC)()
                                          End Try
                                      End Function).
                    ContinueWith(Sub(t As Task(Of List(Of AppointmentC)))
                                     Try
                                         If IsDisposed OrElse Not IsHandleCreated Then Return
                                         BeginInvoke(New MethodInvoker(Sub()
                                                                           Try
                                                                               If gen <> Interlocked.CompareExchange(_renderGeneration, 0, 0) Then Return
                                                                               Dim applied As List(Of AppointmentC)
                                                                               If t.Status = TaskStatus.RanToCompletion AndAlso t.Result IsNot Nothing Then
                                                                                   applied = t.Result
                                                                               Else
                                                                                   applied = New List(Of AppointmentC)()
                                                                               End If
                                                                               _edgeHintAppointments = applied
                                                                               _edgeHintsCacheList = applied
                                                                               _edgeHintsCacheFilterSig = capturedSig
                                                                               UpdateBodyEdgeHints()
                                                                               If _btnBodyExpand IsNot Nothing Then
                                                                                   SyncBodyExpandButtonVisual()
                                                                                   LayoutBodyWorkspaceExpandButton()
                                                                               End If
                                                                           Catch uiEx As Exception
                                                                               ApptErrorHelper.Report(uiEx, "FullWeekSched.EdgeHintsUiApply", showUser:=False)
                                                                           End Try
                                                                       End Sub))
                                     Catch ex As Exception
                                         ApptErrorHelper.Report(ex, "FullWeekSched.EdgeHintsContinueWith", showUser:=False)
                                     End Try
                                 End Sub, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default)

                If _btnBodyExpand IsNot Nothing Then
                    SyncBodyExpandButtonVisual()
                    LayoutBodyWorkspaceExpandButton()
                End If
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.LoadAndRender",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not refresh the week view.",
                                   arabicMessage:="تعذر تحديث عرض الأسبوع.")
        End Try
    End Sub

    ''' <summary>Calls <see cref="LoadAndRender"/> after embedding host tweaks filters programmatically.</summary>
    Public Sub RefreshSchedule()
        LoadAndRender()
    End Sub

    Private Sub RefreshChrome(state As ApptState, data As ApptDataBundle)
        If pnlHeader IsNot Nothing Then pnlHeader.SuspendLayout()
        Try
            _syncing = True
            lblPatient.Text = BuildPatientCaption(state)
            lblRange.Text = ApptTheme.BuildRangeCaption(state, data)
            lblCount.Text = ApptTheme.CountAppointmentsForCurrentView(state, data).ToString()
            chkUse24.Checked = state.Use24HourFormat
            gotoDate.EditValue = state.CurrentDate
            dtStartTime.EditValue = Date.Today.Add(state.WorkStartTime)
            dtEndTime.EditValue = Date.Today.Add(state.WorkEndTime)
            includeReasonCheck.Checked = state.IncludeReason
            boldFontCheck.Checked = state.UseBoldAppointments
            sizeFontCheck.Checked = state.UseLargeAppointments
            ApplyDoctorLegend(If(data Is Nothing, Nothing, data.DoctorInfos.Values))
            ApplyPatientLookup(data)
            RefreshLookupSelections(state)
            RefreshCountChips()
            If startColor IsNot Nothing Then startColor.EditValue = state.ApptCardStartTimeColor
            If endColor IsNot Nothing Then endColor.EditValue = state.ApptCardEndTimeColor
            ApplyHeaderTimeLabelColors(state.ApptCardStartTimeColor, state.ApptCardEndTimeColor)
            _cardPatientNameColor = state.ApptCardPatientNameColor
            _cardReasonColor = state.ApptCardReasonColor
            _cardNotesColor = state.ApptCardNotesColor
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FullWeekSched.RefreshChrome", showUser:=False)
        Finally
            _syncing = False
            If pnlHeader IsNot Nothing Then pnlHeader.ResumeLayout(True)
        End Try
    End Sub

    Private Function BuildPatientCaption(state As ApptState) As String
        If state Is Nothing OrElse Not state.ShowPatientLabels Then Return ""
        If state.PatientOnlyMode AndAlso CurrentPatient IsNot Nothing Then
            Return CurrentPatient.PatientName
        End If
        Return If(Eng, "All Patients", "كل المرضى")
    End Function

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
                InvalidateEdgeHintsCache()
                LoadAndRender()
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.OnWeekColumnAppointmentDrop",
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
            Dim statusMsg = If(Eng, newStatus, ApptTheme.TranslateAppointmentStatus(newStatus))
            XtraMessageBox.Show(
                If(Eng, $"Status updated to {newStatus}", $"تم تحديث الحالة إلى {statusMsg}"),
                If(Eng, "Success", "نجاح"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
            InvalidateEdgeHintsCache()
            LoadAndRender()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.OnAppointmentStatusChangeFromHub",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not update the appointment status.",
                                   arabicMessage:="تعذر تحديث حالة الموعد.")
        End Try
    End Sub

    Private Sub OnAppointmentTimeChangeFromHub(appt As AppointmentC, newStart As DateTime, newEnd As DateTime)
        Try
            If appt Is Nothing Then Return
            If _provider.TryUpdateAppointmentTimes(appt, newStart, newEnd) Then
                InvalidateEdgeHintsCache()
                LoadAndRender()
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.OnAppointmentTimeChangeFromHub",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not change the appointment time.",
                                   arabicMessage:="تعذر تغيير وقت الموعد.")
        End Try
    End Sub

    Private Sub CreateAppointmentFromToolbar()
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

        ' setAppt:=True — editor does not INSERT; host AddAppointment performs single persist.
        Using editor As New AppointCEditorForm(appointment, isNew:=True, setAppt:=True)
            If editor.ShowDialog(FindForm()) = DialogResult.OK Then
                AddAppointment(editor.AppointmentC, editor.ReminderMessageEnglish)
            End If
        End Using
    End Sub

    Public Sub AddAppointment(appt As AppointmentC, Optional reminderMessageEnglish As Boolean? = Nothing)
        Try
            If appt Is Nothing Then Return
            _provider.AddAppointment(appt, reminderMessageEnglish)
            InvalidateEdgeHintsCache()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.AddAppointment",
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
            InvalidateEdgeHintsCache()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.UpdateAppointment",
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
            InvalidateEdgeHintsCache()
            RaiseEvent AppointmentDeleted(apptId)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.DeleteAppointment",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not delete the appointment.",
                                   arabicMessage:="تعذر حذف الموعد.")
        Finally
            LoadAndRender()
        End Try
    End Sub

    Private Shared Function GetExportsDirectory() As String
        Return Path.Combine(Application.StartupPath, "Attachments")
    End Function

    Private Function GetSnapshotFileNamePrefix() As String
        Select Case _state.CurrentView
            Case ApptViewMode.ThisWeekFull
                Return "WeekView7Days"
            Case ApptViewMode.ThisWeek
                Return "WeekView6Days"
            Case Else
                Return "WeekView7Days"
        End Select
    End Function

    Private Sub CaptureWeekSnapshot()
        Dim bmp As Bitmap = Nothing
        Try
            Application.DoEvents()
            ApptErrorHelper.SafeRefresh(_bodyHost, "FullWeekSched.CaptureWeekSnapshot.RefreshBody")
            Application.DoEvents()
            bmp = SchedulerSnapshotShared.CaptureSnapshot(Me, _bodyHost, CType(_state.CurrentView, SchedulerNew.ViewMode), Nothing)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.CaptureWeekSnapshot.Capture",
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

        Dim caption = ApptTheme.BuildRangeCaption(_state, _currentData)
        Dim htmlContext As WeekSnapshotHtmlContext = Nothing
        Try
            Dim req = New ApptViewRequest With {
                .State = _state.Clone(),
                .Data = _currentData,
                .AppointmentAppearanceSelector = AppointmentAppearanceSelector,
                .DragHoldTimeMs = DragHoldTimeMs
            }
            htmlContext = ApptSnapshotHtmlBuilder.BuildForApptModule(req, _weekView, caption)
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FullWeekSched.CaptureWeekSnapshot.HtmlContext", showUser:=False)
        End Try

        Dim owner = TryCast(FindForm(), IWin32Window)
        Using preview As New SchedulerWeekSnapshotPreviewForm(
            bmp,
            GetExportsDirectory(),
            caption,
            _state.PatientFilterId,
            GetSnapshotFileNamePrefix(),
            htmlContext)
            If owner IsNot Nothing Then
                preview.ShowDialog(owner)
            Else
                preview.ShowDialog()
            End If
        End Using
    End Sub

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

    Public Sub LoadPatientData(patientId As Integer)
        Try
            _loadedPatientId = patientId
            SyncCurrentPatientFromForm(patientId)
            If _state.PatientOnlyMode Then
                _state.PatientFilterId = patientId
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.LoadPatientData",
                                   showUser:=True,
                                   owner:=FindForm(),
                                   englishMessage:="Could not load patient appointment data.",
                                   arabicMessage:="تعذر تحميل بيانات مواعيد المريض.")
        Finally
            LoadAndRender()
        End Try
    End Sub

    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        LoadPatientData(patientId)
    End Sub

#Region "Header chrome (designer controls)"
    Private Sub LocalizeStaticText()
        btnFilterDoctor0.Text = If(Eng, "All Doctors", "كل الاطباء")
        btnFilterDoctor0.Visible = False
        btnAllPatients.Text = If(Eng, "All Patients", "كل المرضى")
        btnAllPatients.Visible = False
        btnThisPatient.Text = If(Eng, "This Patient", "هذا المريض")
        btnThisPatient.Visible = False
        btnAllReasons.Text = If(Eng, "All Reasons", "كل الأسباب")
        btnAllReasons.Visible = False
        lblPatients.Text = If(Eng, "Patients", "المرضى")
        lblDoc.Text = If(Eng, "Doctors", "الاطباء")
        lblOptions.Text = If(Eng, "Options", "خيارات")
        lblStatusFilter.Text = If(Eng, "Status", "الحالة")
        lblStartTime.Text = If(Eng, "Start Time", "وقت البداية")
        lblEndTime.Text = If(Eng, "End Time", "وقت النهاية")
        includeReasonCheck.Properties.Caption = If(Eng, "Include Notes", "تضمين السبب")
        chkUse24.Properties.Caption = If(Eng, "Use 24 Hour", "24 ساعة")
        boldFontCheck.Properties.Caption = If(Eng, "Bold", "غامق")
        sizeFontCheck.Properties.Caption = If(Eng, "Size 10", "حجم 10")
        btnPrevView.Text = If(Eng, "Previous", "السابق")
        btnNextView.Text = If(Eng, "Next", "التالي")
        btnPrevView.ToolTip = If(Eng, "Previous view", "العرض السابق")
        btnNextView.ToolTip = If(Eng, "Next view", "العرض التالي")
        lblApptsCount.Text = If(Eng, "Appts Count", "عدد المواعيد")
        btnAdd.Text = If(Eng, "Add", "اضافة")
        btnPrev.Text = If(Eng, "Prev", "السابق")
        btnNext.Text = If(Eng, "Next", "التالي")
        btnToday.Text = If(Eng, "Today", "اليوم")
        LabelControl1.Text = If(Eng, "Go To", "اذهب الى")
        If btnLabelsColors IsNot Nothing Then
            btnLabelsColors.Text = If(Eng, "Label colors", "ألوان التسميات")
        End If
        ApplyLegendChipColors(lblApptsCount, Color.HotPink, Color.White)
        ApplyLegendChipColors(lblCount, Color.HotPink, Color.White)
        ApplySectionChipColors(lblPatients)
        ApplySectionChipColors(lblDoc)
        ApplySectionChipColors(lblOptions)
        ApplySectionChipColors(lblStatusFilter)

        legendPanel.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        grpFilters.Text = If(Eng, "Filter Tools", "أدوات التصفية")
        legendPanel.Text = If(Eng, "Filters", "تصفية")
    End Sub

    Private Sub ConfigureLookupEditors()
        If lookUpDoctors IsNot Nothing Then
            lookUpDoctors.Properties.Columns.Clear()
            lookUpDoctors.Properties.Columns.Add(New LookUpColumnInfo(NameOf(ApptDoctorInfo.DrName), If(Eng, "Doctor", "طبيب")))
            lookUpDoctors.Properties.DisplayMember = NameOf(ApptDoctorInfo.DrName)
            lookUpDoctors.Properties.ValueMember = NameOf(ApptDoctorInfo.DrID)
            lookUpDoctors.Properties.NullText = If(Eng, "All doctors", "كل الأطباء")
        End If

        If LookUpPatient IsNot Nothing Then
            LookUpPatient.Properties.Columns.Clear()
            LookUpPatient.Properties.Columns.Add(New LookUpColumnInfo(NameOf(PatientPickRow.PatientName), If(Eng, "Patient", "مريض")))
            LookUpPatient.Properties.DisplayMember = NameOf(PatientPickRow.PatientName)
            LookUpPatient.Properties.ValueMember = NameOf(PatientPickRow.PatientID)
            LookUpPatient.Properties.NullText = If(Eng, "All patients", "كل المرضى")
        End If

        If LookUpStatus IsNot Nothing Then
            Dim rows As New List(Of StatusPickRow) From {
                New StatusPickRow With {.KeyStr = "", .Display = If(Eng, "All statuses", "كل الحالات")},
                New StatusPickRow With {.KeyStr = "Pending", .Display = ApptTheme.TranslateAppointmentStatus("Pending")},
                New StatusPickRow With {.KeyStr = "Running", .Display = ApptTheme.TranslateAppointmentStatus("Running")},
                New StatusPickRow With {.KeyStr = "Completed", .Display = ApptTheme.TranslateAppointmentStatus("Completed")},
                New StatusPickRow With {.KeyStr = "Canceled", .Display = ApptTheme.TranslateAppointmentStatus("Canceled")},
                New StatusPickRow With {.KeyStr = "Postponed", .Display = ApptTheme.TranslateAppointmentStatus("Postponed")}
            }
            LookUpStatus.Properties.DataSource = rows
            LookUpStatus.Properties.Columns.Clear()
            LookUpStatus.Properties.Columns.Add(New LookUpColumnInfo(NameOf(StatusPickRow.Display), If(Eng, "Status", "الحالة")))
            LookUpStatus.Properties.DisplayMember = NameOf(StatusPickRow.Display)
            LookUpStatus.Properties.ValueMember = NameOf(StatusPickRow.KeyStr)
            LookUpStatus.Properties.NullText = If(Eng, "All statuses", "كل الحالات")
        End If
    End Sub

    Private Sub RefreshCountChips()
        ApplyLegendChipColors(lblApptsCount, Color.HotPink, Color.White)
        ApplyLegendChipColors(lblCount, Color.HotPink, Color.White)
    End Sub

    Private Sub RefreshLookupSelections(state As ApptState)
        If state Is Nothing Then Return
        If lookUpDoctors IsNot Nothing Then
            ' Sentinel row uses DrID 0 -> cleared filter (see ApplyDoctorLegend / LookUpDoctors_EditValueChanged).
            lookUpDoctors.EditValue = If(state.DoctorFilterId.HasValue, CObj(state.DoctorFilterId.Value), CObj(0))
        End If
        If LookUpPatient IsNot Nothing Then
            LookUpPatient.EditValue = If(state.PatientFilterId.HasValue, CObj(state.PatientFilterId.Value), CObj(0))
        End If
        If LookUpStatus IsNot Nothing Then
            Dim key = If(String.IsNullOrWhiteSpace(state.VisibleStatus), "", NormalizeStatus(state.VisibleStatus))
            LookUpStatus.EditValue = key
        End If
    End Sub

    Private Sub ApplyPatientLookup(data As ApptDataBundle)
        If LookUpPatient Is Nothing Then Return
        Dim rows As New List(Of PatientPickRow) From {
            New PatientPickRow With {.PatientID = 0, .PatientName = If(Eng, "All patients", "كل المرضى")}
        }
        If data IsNot Nothing AndAlso data.PatientNames IsNot Nothing Then
            For Each kv In data.PatientNames.Where(Function(k) k.Key <> 0).OrderBy(Function(k) k.Value)
                rows.Add(New PatientPickRow With {.PatientID = kv.Key, .PatientName = kv.Value})
            Next
        End If
        If _state.PatientFilterId.HasValue Then
            Dim pid = _state.PatientFilterId.Value
            If pid <> 0 AndAlso Not rows.Any(Function(r) r.PatientID = pid) Then
                Dim nm As String
                If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID = pid Then
                    nm = CurrentPatient.PatientName
                ElseIf data IsNot Nothing Then
                    nm = data.ResolvePatientName(pid)
                Else
                    nm = $"Patient {pid}"
                End If
                rows.Add(New PatientPickRow With {.PatientID = pid, .PatientName = nm})
                Dim head = rows(0)
                Dim rest = rows.Skip(1).OrderBy(Function(r) r.PatientName).ToList()
                rows.Clear()
                rows.Add(head)
                rows.AddRange(rest)
            End If
        End If
        LookUpPatient.Properties.DataSource = rows
    End Sub

    Private Sub LookUpDoctors_EditValueChanged(sender As Object, e As EventArgs)
        Try
            If _syncing Then Return
            Dim v = lookUpDoctors.EditValue
            If v Is Nothing OrElse v Is DBNull.Value Then
                UpdateState(Sub() _state.DoctorFilterId = Nothing)
            Else
                Dim id = CInt(v)
                If id = 0 Then
                    UpdateState(Sub() _state.DoctorFilterId = Nothing)
                Else
                    UpdateState(Sub() _state.DoctorFilterId = id)
                End If
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.LookUpDoctors_EditValueChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not apply the doctor filter.",
                                   arabicMessage:="تعذر تطبيق فلتر الطبيب.")
        End Try
    End Sub

    Private Sub LookUpPatient_EditValueChanged(sender As Object, e As EventArgs)
        Try
            If _syncing Then Return
            Dim v = LookUpPatient.EditValue
            If v Is Nothing OrElse v Is DBNull.Value Then
                UpdateState(Sub()
                                _state.PatientOnlyMode = False
                                _state.PatientFilterId = Nothing
                            End Sub)
            Else
                Dim id = CInt(v)
                If id = 0 Then
                    UpdateState(Sub()
                                    _state.PatientOnlyMode = False
                                    _state.PatientFilterId = Nothing
                                End Sub)
                Else
                    UpdateState(Sub()
                                    _state.PatientOnlyMode = True
                                    _state.PatientFilterId = id
                                End Sub)
                End If
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.LookUpPatient_EditValueChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not apply the patient filter.",
                                   arabicMessage:="تعذر تطبيق فلتر المريض.")
        End Try
    End Sub

    Private Sub LookUpStatus_EditValueChanged(sender As Object, e As EventArgs)
        Try
            If _syncing Then Return
            Dim v = LookUpStatus.EditValue
            Dim s As String = Nothing
            If v Is Nothing OrElse v Is DBNull.Value Then
                s = Nothing
            Else
                s = Convert.ToString(v)
                If String.IsNullOrEmpty(s) Then s = Nothing
            End If
            UpdateState(Sub() _state.VisibleStatus = s)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.LookUpStatus_EditValueChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not apply the status filter.",
                                   arabicMessage:="تعذر تطبيق فلتر الحالة.")
        End Try
    End Sub

    Private Sub ApplyDoctorLegend(doctors As IEnumerable(Of ApptDoctorInfo))
        If lookUpDoctors Is Nothing Then Return
        Dim list As New List(Of ApptDoctorInfo) From {
            New ApptDoctorInfo With {.DrID = 0, .DrName = If(Eng, "All doctors", "كل الأطباء"), .DrColor = Color.FromArgb(200, 230, 200)}
        }
        If doctors IsNot Nothing Then
            For Each d In doctors.OrderBy(Function(x) x.DrName)
                If d Is Nothing OrElse d.DrID = 0 Then Continue For
                list.Add(d)
            Next
        End If
        lookUpDoctors.Properties.DataSource = list
    End Sub

#Region "Body PREV/NEXT edge hints (ApptHostCtl parity)"
    Private Sub InvalidateBodyEdgeHintsIfDisposed()
        If _bodyPrevHint IsNot Nothing AndAlso _bodyPrevHint.IsDisposed Then _bodyPrevHint = Nothing
        If _bodyNextHint IsNot Nothing AndAlso _bodyNextHint.IsDisposed Then _bodyNextHint = Nothing
    End Sub

    Private Sub EnsureBodyEdgeHints()
        InvalidateBodyEdgeHintsIfDisposed()
        If _bodyPrevHintHost Is Nothing OrElse _bodyNextHintHost Is Nothing Then Return
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
        If _bodyPrevHint.Parent IsNot Nothing AndAlso Not Object.ReferenceEquals(_bodyPrevHint.Parent, _bodyPrevHintHost) Then
            _bodyPrevHint.Parent.Controls.Remove(_bodyPrevHint)
        End If
        If _bodyNextHint.Parent IsNot Nothing AndAlso Not Object.ReferenceEquals(_bodyNextHint.Parent, _bodyNextHintHost) Then
            _bodyNextHint.Parent.Controls.Remove(_bodyNextHint)
        End If
        _bodyPrevHintHost.Controls.Add(_bodyPrevHint)
        _bodyNextHintHost.Controls.Add(_bodyNextHint)
        _bodyPrevHint.BringToFront()
        _bodyNextHint.BringToFront()
        If Not _bodyEdgeHostEventsWired Then
            AddHandler _bodyPrevHintHost.SizeChanged, AddressOf BodyHost_ChangedForEdgeHints
            AddHandler _bodyNextHintHost.SizeChanged, AddressOf BodyHost_ChangedForEdgeHints
            _bodyEdgeHostEventsWired = True
        End If
        If _bodyPrevHint IsNot Nothing AndAlso _bodyNextHint IsNot Nothing Then ArrowLable.ApplyReadingOrderToEdgePair(_bodyPrevHint, _bodyNextHint, Eng)
        PositionBodyEdgeHints()
    End Sub

    Private Sub BodyHost_ChangedForEdgeHints(sender As Object, e As EventArgs)
        PositionBodyEdgeHints()
    End Sub

    Private Sub PositionBodyEdgeHints()
        If _bodyPrevHint Is Nothing OrElse _bodyNextHint Is Nothing Then Return
        If _bodyPrevHintHost Is Nothing OrElse _bodyNextHintHost Is Nothing Then Return
        If _bodyPrevHintHost.ClientSize.Width < 1 OrElse _bodyPrevHintHost.ClientSize.Height < 1 Then Return
        Dim prevX = Math.Max(0, (_bodyPrevHintHost.ClientSize.Width - _bodyPrevHint.Width) \ 2)
        Dim prevY = Math.Max(0, (_bodyPrevHintHost.ClientSize.Height - _bodyPrevHint.Height) \ 2)
        Dim nextX = Math.Max(0, (_bodyNextHintHost.ClientSize.Width - _bodyNextHint.Width) \ 2)
        Dim nextY = Math.Max(0, (_bodyNextHintHost.ClientSize.Height - _bodyNextHint.Height) \ 2)
        _bodyPrevHint.Location = New Point(prevX, prevY)
        _bodyNextHint.Location = New Point(nextX, nextY)
        _bodyPrevHint.BringToFront()
        _bodyNextHint.BringToFront()
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

    ''' <summary>Full-week: jump to previous/next Saturday-week that has appointments (same filter scope as <see cref="ApptDataProvider.LoadEdgeHintAppointments"/>).</summary>
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
        ArrowLable.ApplyReadingOrderToEdgePair(_bodyPrevHint, _bodyNextHint, Eng)

        Dim appts = _edgeHintAppointments
        If appts Is Nothing OrElse appts.Count = 0 Then
            PositionBodyEdgeHints()
            Return
        End If

        ApplyBodyEdgeHintVisualThemeWeek()
        Const daysInWeek As Integer = 7
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
            _edgeNavNextAnchor = wkN
            _edgeNavNextScrollAppt = appts.Where(Function(a) a.StartDateTime.Date >= wkN AndAlso a.StartDateTime.Date < wkN.AddDays(daysInWeek)).OrderBy(Function(a) a.StartDateTime).FirstOrDefault()
            _bodyNextHint.Visible = True
        End If

        PositionBodyEdgeHints()
        _bodyPrevHint.BringToFront()
        _bodyNextHint.BringToFront()
    End Sub
#End Region

#Region "Expand / collapse body (ApptHostCtl parity)"
    Private Sub CaptureLayoutRowMetricsOnce()
        If _layoutRowsCaptured Then Return
        If _rootLayout Is Nothing OrElse _rootLayout.Rows.Count < 2 Then Return
        _layoutRow0Style = _rootLayout.Rows(0).Style
        _layoutRow0Height = _rootLayout.Rows(0).Height
        _layoutRow1Style = _rootLayout.Rows(1).Style
        _layoutRow1Height = _rootLayout.Rows(1).Height
        _layoutRowsCaptured = True
    End Sub

    Private Sub EnsureBodyWorkspaceExpandButton()
        If _btnBodyExpand IsNot Nothing Then Return
        CaptureLayoutRowMetricsOnce()
        _btnBodyExpand = New SimpleButton With {
            .Name = "btnFullWeekBodyExpand",
            .Size = New Size(26, 26),
            .Cursor = Cursors.Hand,
            .TabStop = False
        }
        _btnBodyExpand.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        _btnBodyExpand.Appearance.TextOptions.VAlignment = VertAlignment.Center
        AddHandler _btnBodyExpand.Click, AddressOf BodyExpandButton_Click
        AddHandler _bodyHost.SizeChanged, AddressOf BodyHost_ChangedForExpandBtnLayout
        AddHandler _bodyHost.LocationChanged, AddressOf BodyHost_ChangedForExpandBtnLayout
        If _bodyViewHost IsNot Nothing Then AddHandler _bodyViewHost.SizeChanged, AddressOf BodyHost_ChangedForExpandBtnLayout
        Controls.Add(_btnBodyExpand)
        SyncBodyExpandButtonVisual()
        LayoutBodyWorkspaceExpandButton()
    End Sub

    Private Sub BodyHost_ChangedForExpandBtnLayout(sender As Object, e As EventArgs)
        LayoutBodyWorkspaceExpandButton()
    End Sub

    Private Sub LayoutBodyWorkspaceExpandButton()
        If _btnBodyExpand Is Nothing OrElse _bodyHost Is Nothing Then Return
        Const pad As Integer = 3
        Dim targetHost = If(_bodyViewHost IsNot Nothing, _bodyViewHost, _bodyHost)
        Dim clientY = pad
        Dim expandClientX = targetHost.ClientSize.Width - _btnBodyExpand.Width - pad
        Dim ptExpand = PointToClient(targetHost.PointToScreen(New Point(expandClientX, clientY)))
        _btnBodyExpand.Location = ptExpand
        _btnBodyExpand.BringToFront()
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
    End Sub

    Private Sub ApplyBodyWorkspaceMaximizedState()
        CaptureLayoutRowMetricsOnce()
        If _rootLayout Is Nothing OrElse _rootLayout.Rows.Count < 2 OrElse Not _layoutRowsCaptured Then Return
        SuspendLayout()
        _rootLayout.SuspendLayout()
        If pnlHeader IsNot Nothing Then pnlHeader.SuspendLayout()
        If _bodyHost IsNot Nothing Then _bodyHost.SuspendLayout()
        Try
            If _bodyWorkspaceMaximized Then
                If pnlHeader IsNot Nothing Then pnlHeader.Visible = False
                _rootLayout.Rows(0).Style = TablePanelEntityStyle.Absolute
                _rootLayout.Rows(0).Height = 2.0F
                _rootLayout.Rows(1).Style = TablePanelEntityStyle.Relative
                _rootLayout.Rows(1).Height = 100.0F
            Else
                If pnlHeader IsNot Nothing Then pnlHeader.Visible = True
                _rootLayout.Rows(0).Style = _layoutRow0Style
                _rootLayout.Rows(0).Height = _layoutRow0Height
                _rootLayout.Rows(1).Style = _layoutRow1Style
                _rootLayout.Rows(1).Height = _layoutRow1Height
            End If
        Finally
            If _bodyHost IsNot Nothing Then _bodyHost.ResumeLayout(False)
            If pnlHeader IsNot Nothing Then pnlHeader.ResumeLayout(False)
            _rootLayout.ResumeLayout(False)
            ResumeLayout(False)
        End Try
        _rootLayout.PerformLayout()
        If pnlHeader IsNot Nothing Then pnlHeader.PerformLayout()
        If _bodyHost IsNot Nothing Then _bodyHost.PerformLayout()
        PerformLayout()
        LayoutBodyWorkspaceExpandButton()
    End Sub

    ''' <summary>Full-week view always opens with the body expanded (not persisted).</summary>
    Private Sub ApplyDefaultBodyWorkspaceExpandedState()
        Try
            _bodyWorkspaceMaximized = True
            SyncBodyExpandButtonVisual()
            ApplyBodyWorkspaceMaximizedState()
            LayoutBodyWorkspaceExpandButton()
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FullWeekSched.ApplyDefaultBodyWorkspaceExpandedState", showUser:=False)
        End Try
    End Sub

    Private Sub ToggleBodyWorkspaceExpandedInternal()
        _bodyWorkspaceMaximized = Not _bodyWorkspaceMaximized
        SyncBodyExpandButtonVisual()
        ApplyBodyWorkspaceMaximizedState()
        LoadAndRender()
    End Sub

    Private Sub BodyExpandButton_Click(sender As Object, e As EventArgs)
        ToggleBodyWorkspaceExpandedInternal()
    End Sub

    Public Sub EnsureBodyWorkspaceExpanded()
        If _bodyWorkspaceMaximized Then Return
        _bodyWorkspaceMaximized = True
        SyncBodyExpandButtonVisual()
        ApplyBodyWorkspaceMaximizedState()
        LayoutBodyWorkspaceExpandButton()
        LoadAndRender()
    End Sub

    Public Sub EnsureBodyWorkspaceCollapsed()
        If Not _bodyWorkspaceMaximized Then Return
        _bodyWorkspaceMaximized = False
        SyncBodyExpandButtonVisual()
        ApplyBodyWorkspaceMaximizedState()
        LayoutBodyWorkspaceExpandButton()
        LoadAndRender()
    End Sub

    Public ReadOnly Property IsBodyWorkspaceExpanded As Boolean
        Get
            Return _bodyWorkspaceMaximized
        End Get
    End Property
#End Region

    Protected Overrides Sub OnLayout(levent As LayoutEventArgs)
        MyBase.OnLayout(levent)
        PositionBodyEdgeHints()
        LayoutBodyWorkspaceExpandButton()
    End Sub

    Private Sub SafeRaiseHeaderAction(context As String, action As Action)
        If action Is Nothing Then Return
        Try
            action()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   context,
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not apply the header action.",
                                   arabicMessage:="تعذر تنفيذ إجراء الرأس.")
        End Try
    End Sub

    Private Sub InitializeWeekSnapshotButton()
        If btnWeekSnapshot Is Nothing OrElse btnWeekSnapshot.IsDisposed Then Return
        btnWeekSnapshot.Text = If(Eng, "Snapshot", "لقطة")
        If _weekSnapshotToolTip IsNot Nothing Then
            _weekSnapshotToolTip.Dispose()
            _weekSnapshotToolTip = Nothing
        End If
        _weekSnapshotToolTip = New ToolTip()
        Dim tip = If(Eng,
            "Capture the current schedule view as an image (scroll areas expanded where supported).",
            "التقاط صورة للعرض الحالي (مع توسيع المناطق القابلة للتمرير حيثما أمكن).")
        _weekSnapshotToolTip.SetToolTip(btnWeekSnapshot, tip)
    End Sub

    Private Sub InitializeApptCardTimeColorPickersFromSettings()
        Dim s = My.Settings.ApptModuleCardStartTimeColor
        Dim eColor = My.Settings.ApptModuleCardEndTimeColor
        _syncing = True
        Try
            If startColor IsNot Nothing Then startColor.EditValue = s
            If endColor IsNot Nothing Then endColor.EditValue = eColor
            ApplyHeaderTimeLabelColors(s, eColor)
        Finally
            _syncing = False
        End Try
    End Sub

    Private Sub InitializeApptCardLabelColorCacheFromSettings()
        _cardPatientNameColor = My.Settings.ApptModuleCardPatientNameColor
        _cardReasonColor = My.Settings.ApptModuleCardReasonColor
        _cardNotesColor = My.Settings.ApptModuleCardNotesColor
    End Sub

    Private Sub BtnLabelsColors_Click(sender As Object, e As EventArgs)
        'Try
        '    If _syncing Then Return
        '    Using dlg As New ApptCardLabelColorsDialog(_cardPatientNameColor, _cardReasonColor, _cardNotesColor)
        '        If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
        '        My.Settings.ApptModuleCardPatientNameColor = dlg.ResultPatientNameColor
        '        My.Settings.ApptModuleCardReasonColor = dlg.ResultReasonColor
        '        My.Settings.ApptModuleCardNotesColor = dlg.ResultNotesColor
        '        My.Settings.Save()
        '        UpdateState(Sub()
        '                        _state.ApptCardPatientNameColor = dlg.ResultPatientNameColor
        '                        _state.ApptCardReasonColor = dlg.ResultReasonColor
        '                        _state.ApptCardNotesColor = dlg.ResultNotesColor
        '                    End Sub)
        '    End Using
        'Catch ex As Exception
        '    ApptErrorHelper.Report(ex,
        '                           "FullWeekSched.BtnLabelsColors_Click",
        '                           showUser:=True,
        '                           owner:=Me,
        '                           englishMessage:="Could not update appointment label colors.",
        '                           arabicMessage:="تعذر تحديث ألوان تسميات الموعد.")
        'End Try
    End Sub

    Private Sub ApptCardTimeColorPickers_EditValueChanged(sender As Object, e As EventArgs)
        Try
            If _syncing Then Return
            Dim sc = ColorFromPicker(startColor, Color.Red)
            Dim ec = ColorFromPicker(endColor, Color.SteelBlue)
            ApplyHeaderTimeLabelColors(sc, ec)
            My.Settings.ApptModuleCardStartTimeColor = sc
            My.Settings.ApptModuleCardEndTimeColor = ec
            My.Settings.Save()
            UpdateState(Sub()
                            _state.ApptCardStartTimeColor = sc
                            _state.ApptCardEndTimeColor = ec
                        End Sub)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.ApptCardTimeColorPickers_EditValueChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not update appointment time colors.",
                                   arabicMessage:="تعذر تحديث ألوان وقت الموعد.")
        End Try
    End Sub

    Private Shared Function ColorFromPicker(picker As ColorEdit, fallback As Color) As Color
        If picker Is Nothing Then Return fallback
        Return NormalizeColorValue(picker.EditValue, fallback)
    End Function

    Private Shared Function NormalizeColorValue(editValue As Object, fallback As Color) As Color
        If editValue Is Nothing OrElse editValue Is DBNull.Value Then Return fallback
        If TypeOf editValue Is Color Then
            Dim c = DirectCast(editValue, Color)
            If c.IsEmpty Then Return fallback
            Return c
        End If
        Try
            Return Color.FromArgb(Convert.ToInt32(editValue))
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FullWeekSched.NormalizeColorValue", showUser:=False)
            Return fallback
        End Try
    End Function

    Private Sub ApplyHeaderTimeLabelColors(startC As Color, endC As Color)
        If lblStartTime IsNot Nothing Then
            lblStartTime.Appearance.ForeColor = startC
            lblStartTime.Appearance.Options.UseForeColor = True
        End If
        If lblEndTime IsNot Nothing Then
            lblEndTime.Appearance.ForeColor = endC
            lblEndTime.Appearance.Options.UseForeColor = True
        End If
    End Sub

    Private Sub WorkingHours_EditValueChanged(sender As Object, e As EventArgs)
        Try
            If _syncing Then Return
            Dim startTime = If(dtStartTime.EditValue Is Nothing, Date.Today.AddHours(8), Convert.ToDateTime(dtStartTime.EditValue))
            Dim endTime = If(dtEndTime.EditValue Is Nothing, Date.Today.AddHours(16), Convert.ToDateTime(dtEndTime.EditValue))
            Dim st = startTime.TimeOfDay
            Dim en = endTime.TimeOfDay
            Try
                My.Settings.SchedulerWorkingDayStart = st
                My.Settings.SchedulerWorkingDayEnd = If(en <= st, st.Add(TimeSpan.FromHours(1)), en)
                My.Settings.Save()
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "FullWeekSched.WorkingHours.SaveSettings", showUser:=False)
            End Try
            UpdateState(Sub()
                            _state.WorkStartTime = st
                            _state.WorkEndTime = If(en <= st, st.Add(TimeSpan.FromHours(1)), en)
                        End Sub)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.WorkingHours_EditValueChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not update working hours.",
                                   arabicMessage:="تعذر تحديث ساعات العمل.")
        End Try
    End Sub

    Private Sub Header_Resize(sender As Object, e As EventArgs)
        If _chromeResizeDebounce Is Nothing Then
            RefreshLegendFiltersLayout()
            Return
        End If
        _chromeResizeDebounce.Stop()
        _chromeResizeDebounce.Start()
    End Sub

    Private Sub ChromeResizeDebounce_Tick(sender As Object, e As EventArgs)
        If _chromeResizeDebounce Is Nothing Then Return
        _chromeResizeDebounce.Stop()
        If IsDisposed Then Return
        RefreshLegendFiltersLayout()
    End Sub

    Private Sub RefreshLegendFiltersLayout()
        If filtersTable IsNot Nothing AndAlso Not filtersTable.IsDisposed Then filtersTable.PerformLayout()
        If grpFilters IsNot Nothing AndAlso Not grpFilters.IsDisposed Then grpFilters.PerformLayout()
    End Sub

    Private Sub ApplyLegendChipColors(lc As LabelControl, back As Color, fore As Color)
        lc.Appearance.BackColor = back
        lc.Appearance.ForeColor = fore
        lc.Appearance.Options.UseBackColor = True
        lc.Appearance.Options.UseForeColor = True
    End Sub

    Private Sub ApplySectionChipColors(lc As LabelControl)
        lc.Appearance.BackColor = Color.Blue
        lc.Appearance.ForeColor = Color.White
        lc.Appearance.Options.UseBackColor = True
        lc.Appearance.Options.UseForeColor = True
    End Sub

    Private Shared Function NormalizeStatus(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then Return Nothing
        Return value.Trim()
    End Function

    Private Sub btnWeekSnapshot_Click(sender As Object, e As EventArgs) Handles btnWeekSnapshot.Click
        Try
            CaptureWeekSnapshot()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.btnWeekSnapshot_Click",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not start the snapshot action.",
                                   arabicMessage:="تعذر بدء إجراء اللقطة.")
        End Try
    End Sub

    Private Sub btnLabs_Click(sender As Object, e As EventArgs) Handles btnLabs.Click
        Try
            Using F As New FrmLabSendWhats
                F.ShowDialog(Me)
            End Using
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FullWeekSched.btnLabs_Click",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not open the lab WhatsApp window.",
                                   arabicMessage:="تعذر فتح نافذة واتساب المختبر.")
        End Try
    End Sub
#End Region

End Class
