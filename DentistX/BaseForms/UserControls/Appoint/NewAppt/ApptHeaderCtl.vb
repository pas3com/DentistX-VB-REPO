Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
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
Imports DevExpress.Utils.Win
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

Public Class ApptHeaderCtl

    Private _syncing As Boolean
    Private _syncingFilterCombos As Boolean
    Private ReadOnly _doctorInfos As New List(Of ApptDoctorInfo)()

    Private Shared ReadOnly FlyoutCardLabelColorEditUiFont As New Font("Calibri", 10.0F, FontStyle.Bold)
    Private Shared _apptNavArrowLeftImage As Image

    Private _headerToolbarButtonHeightHooked As Boolean
    Private _headerToolbarRelayoutPending As Boolean
    Private _inHeaderToolbarCentering As Boolean
    Private _headerToolbarCenteringHooks As Boolean
    Private _weekSnapshotToolTip As ToolTip

    Public Event PrevRequested As Action
    Public Event NextRequested As Action
    Public Event PrevApptRequested As Action
    Public Event NextApptRequested As Action
    Public Event TodayRequested As Action
    Public Event AddRequested As Action
    Public Event WeekSnapshotRequested As Action
    Public Event CurrentDateChanged As Action(Of DateTime)
    Public Event ViewChanged As Action(Of ApptViewMode)
    Public Event Use24Changed As Action(Of Boolean)
    Public Event AllPatientsRequested As Action
    Public Event ThisPatientRequested As Action
    Public Event DoctorFilterChanged As Action(Of Integer?)
    ''' <summary>Prefer this doctor first when sorting appointments / columns (persisted); <see langword="Nothing"/> restores linked-user default ordering.</summary>
    Public Event OrderByDoctorChanged As Action(Of Integer?)
    Public Event StatusFilterChanged As Action(Of String)
    Public Event IncludeReasonChanged As Action(Of Boolean)
    Public Event FontProfileChanged As Action(Of Boolean, Boolean)
    Public Event WorkingHoursChanged As Action(Of TimeSpan, TimeSpan)
    Public Event PrevViewRequested As Action
    Public Event NextViewRequested As Action
    ''' <summary>Fired when start/end time label colors change; host persists user settings and refreshes cards.</summary>
    Public Event ApptCardTimeColorsChanged As Action(Of Color, Color)
    ''' <summary>Fired when patient name / reason / notes label colors change; host persists and refreshes cards.</summary>
    Public Event ApptCardLabelColorsChanged As Action(Of Color, Color, Color)
    ''' <summary>Fired after <see cref="grpFilters"/> show/hide; host adjusts the chrome row so the schedule body expands/contracts.</summary>
    Public Event FilterStripVisibilityChanged(filtersVisible As Boolean)

    Private _cardPatientNameColor As Color
    Private _cardReasonColor As Color
    Private _cardNotesColor As Color

    Public Sub New()
        Try
            InitializeComponent()
            Dock = DockStyle.Fill
            cmbView.Properties.Items.Clear()
            InitializeComboValues()
            InitializeFilterCombos()
            LocalizeStaticText()
            WireEvents()
            AddHandler Resize, AddressOf Header_Resize
            pnlHeader.AutoScroll = True
            legendPanel.AutoScroll = True
            InitializeApptCardTimeColorPickersFromSettings()
            InitializeApptCardLabelColorCacheFromSettings()
            InitializeViewNavButtons()
            InitializeApptNavButtons()
            InitializeWeekSnapshotButton()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.New",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="The appointment header could not be initialized.",
                                   arabicMessage:="تعذر تهيئة رأس المواعيد.")
        End Try
    End Sub

    ''' <summary>Matches <see cref="SchedulerNew.UpdateViewNavButtonVisibility"/>.</summary>
    Public Sub UpdateViewNavButtonVisibility(backCount As Integer, forwardCount As Integer)
        Try
            If btnPrevView IsNot Nothing Then btnPrevView.Visible = backCount > 0
            If btnNextView IsNot Nothing Then btnNextView.Visible = forwardCount > 0
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptHeaderCtl.UpdateViewNavButtonVisibility", showUser:=False)
        End Try
    End Sub

    ''' <summary>Enables header appointment jump buttons when filtered PREV/NEXT targets exist.</summary>
    Public Sub UpdateApptNavButtonVisibility(prevAvailable As Boolean, nextAvailable As Boolean)
        Try
            If btnPrevAppt IsNot Nothing Then btnPrevAppt.Enabled = prevAvailable
            If btnNextAppt IsNot Nothing Then btnNextAppt.Enabled = nextAvailable
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptHeaderCtl.UpdateApptNavButtonVisibility", showUser:=False)
        End Try
    End Sub

    ''' <summary>Same as <see cref="btnOptions"/> — used by <see cref="ApptHostCtl"/> schedule context menu.</summary>
    Public Sub ToggleFilterStrip()
        BtnOptions_Click(btnOptions, EventArgs.Empty)
    End Sub

    ''' <summary>Doctor/patient/status filter strip (<see cref="grpFilters"/>) visibility; drives host chrome row height in <see cref="ApptHostCtl"/>.</summary>
    Public ReadOnly Property FiltersStripVisible As Boolean
        Get
            Return grpFilters IsNot Nothing AndAlso grpFilters.Visible
        End Get
    End Property

    ''' <summary>Updates the view combo without recording view history (used for Prev/Next view and host <see cref="ApptHostCtl.SetView"/>).</summary>
    Public Sub SyncViewComboToState(state As ApptState)
        Try
            If state Is Nothing OrElse cmbView Is Nothing Then Return
            _syncing = True
            SelectComboValue(cmbView, state.CurrentView)
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptHeaderCtl.SyncViewComboToState", showUser:=False)
        Finally
            _syncing = False
        End Try
    End Sub

    Public Sub ApplyState(state As ApptState, data As ApptDataBundle, patientCaption As String, hasCurrentPatient As Boolean)
        Try
            _syncing = True
            lblPatient.Text = patientCaption
            lblRange.Text = BuildRangeCaption(state, data)
            lblCount.Text = ApptTheme.CountAppointmentsForCurrentView(state, data).ToString()
            chkUse24.Checked = state.Use24HourFormat
            gotoDate.EditValue = state.CurrentDate
            dtStartTime.EditValue = Date.Today.Add(state.WorkStartTime)
            dtEndTime.EditValue = Date.Today.Add(state.WorkEndTime)
            includeReasonCheck.Checked = state.IncludeReason
            boldFontCheck.Checked = state.UseBoldAppointments
            sizeFontCheck.Checked = state.UseLargeAppointments
            btnThisPatient.Enabled = hasCurrentPatient
            SelectComboValue(cmbView, state.CurrentView)
            ApplyDoctorLegend(If(data Is Nothing, Nothing, data.DoctorInfos.Values))
            RefreshPatientButtons(state, hasCurrentPatient)
            SyncStatusCombo(state)
            SyncDoctorCombo(state)
            SyncFirstDoctorCombo(state)
            RefreshCountChips()
            If startColor IsNot Nothing Then startColor.EditValue = state.ApptCardStartTimeColor
            If endColor IsNot Nothing Then endColor.EditValue = state.ApptCardEndTimeColor
            ApplyHeaderTimeLabelColors(state.ApptCardStartTimeColor, state.ApptCardEndTimeColor)
            _cardPatientNameColor = state.ApptCardPatientNameColor
            _cardReasonColor = state.ApptCardReasonColor
            _cardNotesColor = state.ApptCardNotesColor
            SyncFlyoutApptCardLabelPickersSuppressEvents()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.ApplyState",
                                   showUser:=False,
                                   owner:=Me)
        Finally

            _syncing = False
        End Try
    End Sub

    Private Sub InitializeComboValues()
        cmbView.Properties.Items.AddRange(New Object() {
            New ComboValueItem(Of ApptViewMode)(ApptViewMode.DayView, "Day View"),
            New ComboValueItem(Of ApptViewMode)(ApptViewMode.ThisWeek, "This Week"),
            New ComboValueItem(Of ApptViewMode)(ApptViewMode.ThisWeekFull, "This Week Full"),
            New ComboValueItem(Of ApptViewMode)(ApptViewMode.MonthlyWeek, "Monthly Week"),
            New ComboValueItem(Of ApptViewMode)(ApptViewMode.MonthView, "Month View"),
            New ComboValueItem(Of ApptViewMode)(ApptViewMode.DaysTimeline, "Days Timeline"),
            New ComboValueItem(Of ApptViewMode)(ApptViewMode.DoctorsDay, "Doctors Day")
        })
    End Sub

    Private Sub LocalizeStaticText()
        btnAllPatients.Text = If(Eng, "All Patients", "كل المرضى")
        btnThisPatient.Text = If(Eng, "This Patient", "هذا المريض")
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
        SyncApptNavButtonVisuals()
        btnToday.Text = If(Eng, "Today", "اليوم")
        LabelControl2.Text = If(Eng, "Go To", "اذهب الى")
        If btnLabelsColors IsNot Nothing Then
            btnLabelsColors.Text = If(Eng, "Label colors", "ألوان التسميات")
        End If
        If lbl_Patient IsNot Nothing Then lbl_Patient.Text = If(Eng, "Patient name", "اسم المريض")
        If lbl_Reason IsNot Nothing Then lbl_Reason.Text = If(Eng, "Reason", "السبب")
        If lbl_Notes IsNot Nothing Then lbl_Notes.Text = If(Eng, "Notes", "الملاحظات")
        If btnMoreScheduleOpts IsNot Nothing Then
            btnMoreScheduleOpts.Text = If(Eng, "Schedule options…", "خيارات الجدولة…")
            btnMoreScheduleOpts.ToolTip = If(Eng, "Working hours, colors, fonts, notes", "ساعات العمل، الألوان، الخط، الملاحظات")
        End If
        If cmbFirstDoctor IsNot Nothing Then
            cmbFirstDoctor.ToolTip = If(Eng,
                "Show this doctor's appointments first in each day column (week, timeline, doctors day).",
                "عرض مواعيد هذا الطبيب أولا في عمود كل يوم (الأسبوع، الخط الزمني، يوم الأطباء).")
        End If
        SyncFiltersToggleUi()
        ApplyLegendChipColors(lblApptsCount, Color.HotPink, Color.White)
        ApplyLegendChipColors(lblCount, Color.HotPink, Color.White)
        ApplySectionChipColors(lblPatients)
        ApplySectionChipColors(lblDoc)
        ApplySectionChipColors(lblOptions)
        ApplySectionChipColors(lblStatusFilter)

        ' Same as SchedulerNew.LoadDoctorLegend: LTR for English keeps filtersTable/flow layout stable; Arabic mirrors legend.
        legendPanel.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        grpFilters.Text = If(Eng, "Filter Tools", "أدوات التصفية")
        legendPanel.Text = If(Eng, "Filters", "تصفية")
        If grpFilters IsNot Nothing Then grpFilters.RightToLeft = legendPanel.RightToLeft
        If filtersTable IsNot Nothing Then filtersTable.RightToLeft = legendPanel.RightToLeft
        If pnlHeaderToolbarFlow IsNot Nothing Then pnlHeaderToolbarFlow.RightToLeft = legendPanel.RightToLeft
        RebuildStatusComboItems()
    End Sub

    ''' <summary>Toolbar control: toggles visibility of <see cref="grpFilters"/>.</summary>
    Private Sub SyncFiltersToggleUi()
        If btnOptions Is Nothing OrElse grpFilters Is Nothing Then Return
        Dim expanded = grpFilters.Visible
        btnOptions.Text = If(Eng,
            If(expanded, "Hide filters ▲", "Show filters ▼"),
            If(expanded, "إخفاء التصفية ▲", "إظهار التصفية ▼"))
        btnOptions.ToolTip = If(Eng,
            If(expanded, "Hide doctor / patient / status filter bar", "Show doctor / patient / status filter bar"),
            If(expanded, "إخفاء شريط التصفية", "إظهار شريط التصفية"))
    End Sub

    Private Sub BtnOptions_Click(sender As Object, e As EventArgs) Handles btnOptions.Click
        SafeRaiseHeaderAction(
            "ApptHeaderCtl.BtnOptions_Click",
            Sub()
                If grpFilters Is Nothing Then Return
                grpFilters.Visible = Not grpFilters.Visible
                If Not grpFilters.Visible AndAlso flyoutScheduleOptions IsNot Nothing Then
                    Try
                        flyoutScheduleOptions.HidePopup(False)
                    Catch
                    End Try
                End If
                SyncFiltersToggleUi()
                RefreshLegendFiltersLayout()
                RaiseEvent FilterStripVisibilityChanged(grpFilters.Visible)
            End Sub)
    End Sub

    Private Sub RefreshCountChips()
        ApplyLegendChipColors(lblApptsCount, Color.HotPink, Color.White)
        ApplyLegendChipColors(lblCount, Color.HotPink, Color.White)
    End Sub

    Private Sub RefreshPatientButtons(state As ApptState, hasCurrentPatient As Boolean)
        Dim sel = If(state.PatientFilterId.HasValue, 1, 0)
        StyleFilterChip(btnAllPatients, sel = 0, True, Color.FromArgb(230, 255, 230))
        StyleFilterChip(btnThisPatient, sel = 1, hasCurrentPatient, Color.FromArgb(255, 220, 188))
    End Sub

    Private Sub InitializeFilterCombos()
        RebuildStatusComboItems()
    End Sub

    Private Sub RebuildStatusComboItems()
        If cmbStatusFilter Is Nothing Then Return
        Dim prev As String = Nothing
        Dim hadPrev As Boolean = False
        Dim curSel = TryCast(cmbStatusFilter.SelectedItem, ComboValueItem(Of String))
        If curSel IsNot Nothing Then
            prev = curSel.Value
            hadPrev = True
        End If
        _syncingFilterCombos = True
        Try
            cmbStatusFilter.Properties.Items.Clear()
            cmbStatusFilter.Properties.Items.Add(New ComboValueItem(Of String)(Nothing, If(Eng, "All statuses", "كل الحالات")))
            cmbStatusFilter.Properties.Items.Add(New ComboValueItem(Of String)("Pending", If(Eng, "Pending", "قيد الانتظار")))
            cmbStatusFilter.Properties.Items.Add(New ComboValueItem(Of String)("Running", If(Eng, "Running", "قيد التنفيذ")))
            cmbStatusFilter.Properties.Items.Add(New ComboValueItem(Of String)("Completed", If(Eng, "Completed", "منجز")))
            cmbStatusFilter.Properties.Items.Add(New ComboValueItem(Of String)("Canceled", If(Eng, "Canceled", "ملغى")))
            cmbStatusFilter.Properties.Items.Add(New ComboValueItem(Of String)("Postponed", If(Eng, "Postponed", "مؤجل")))
            If hadPrev Then
                SelectComboStringKey(cmbStatusFilter, prev)
            ElseIf cmbStatusFilter.Properties.Items.Count > 0 Then
                cmbStatusFilter.SelectedIndex = 0
            End If
        Finally
            _syncingFilterCombos = False
        End Try
    End Sub


    Private Sub SyncStatusCombo(state As ApptState)
        If cmbStatusFilter Is Nothing Then Return
        Dim key As String = Nothing
        Dim ns = NormalizeStatus(state.VisibleStatus)
        If Not String.IsNullOrEmpty(ns) Then key = ns
        _syncingFilterCombos = True
        Try
            SelectComboStringKey(cmbStatusFilter, key)
        Finally
            _syncingFilterCombos = False
        End Try
    End Sub

    Private Sub SyncDoctorCombo(state As ApptState)
        If cmbDoctorFilter Is Nothing Then Return
        If cmbDoctorFilter.Properties.Items.Count = 0 Then Return
        _syncingFilterCombos = True
        Try
            If Not state.DoctorFilterId.HasValue Then
                cmbDoctorFilter.SelectedIndex = 0
                Return
            End If
            Dim id = state.DoctorFilterId.Value
            For i = 0 To cmbDoctorFilter.Properties.Items.Count - 1
                Dim it = TryCast(cmbDoctorFilter.Properties.Items(i), ComboValueItem(Of Integer?))
                If it Is Nothing Then Continue For
                If it.Value.HasValue AndAlso it.Value.Value = id Then
                    cmbDoctorFilter.SelectedIndex = i
                    Exit For
                End If
            Next
        Finally
            _syncingFilterCombos = False
        End Try
    End Sub

    Private Sub SyncFirstDoctorCombo(state As ApptState)
        If cmbFirstDoctor Is Nothing Then Return
        If cmbFirstDoctor.Properties.Items.Count = 0 Then Return
        _syncingFilterCombos = True
        Try
            If state Is Nothing OrElse Not state.OrderByDoctorId.HasValue OrElse state.OrderByDoctorId.Value <= 0 Then
                cmbFirstDoctor.SelectedIndex = 0
                Return
            End If
            Dim id = state.OrderByDoctorId.Value
            For i = 0 To cmbFirstDoctor.Properties.Items.Count - 1
                Dim it = TryCast(cmbFirstDoctor.Properties.Items(i), ComboValueItem(Of Integer?))
                If it Is Nothing Then Continue For
                If it.Value.HasValue AndAlso it.Value.Value = id Then
                    cmbFirstDoctor.SelectedIndex = i
                    Exit For
                End If
            Next
        Finally
            _syncingFilterCombos = False
        End Try
    End Sub

    Private Shared Sub SelectComboStringKey(combo As ComboBoxEdit, key As String)
        If combo Is Nothing Then Return
        For i = 0 To combo.Properties.Items.Count - 1
            Dim item = TryCast(combo.Properties.Items(i), ComboValueItem(Of String))
            If item Is Nothing Then Continue For
            If key Is Nothing Then
                If item.Value Is Nothing Then
                    combo.SelectedIndex = i
                    Exit Sub
                End If
            ElseIf item.Value IsNot Nothing AndAlso String.Equals(item.Value, key, StringComparison.OrdinalIgnoreCase) Then
                combo.SelectedIndex = i
                Exit Sub
            End If
        Next
        If combo.Properties.Items.Count > 0 Then combo.SelectedIndex = 0
    End Sub

    Private Sub ApplyDoctorLegend(doctors As IEnumerable(Of ApptDoctorInfo))
        _doctorInfos.Clear()
        If cmbDoctorFilter Is Nothing Then Return
        Dim prevId As Integer?
        If Not _syncing Then
            Dim cur = TryCast(cmbDoctorFilter.SelectedItem, ComboValueItem(Of Integer?))
            If cur IsNot Nothing Then prevId = cur.Value
        End If

        _syncingFilterCombos = True
        Try
            cmbDoctorFilter.Properties.Items.Clear()
            cmbDoctorFilter.Properties.Items.Add(New ComboValueItem(Of Integer?)(Nothing, If(Eng, "All doctors", "كل الاطباء")))
            If cmbFirstDoctor IsNot Nothing Then
                cmbFirstDoctor.Properties.Items.Clear()
                cmbFirstDoctor.Properties.Items.Add(New ComboValueItem(Of Integer?)(Nothing, If(Eng, "Default (logged-in doctor)", "الافتراضي (طبيب الدخول)")))
            End If
            If doctors Is Nothing Then
                cmbDoctorFilter.SelectedIndex = 0
                If cmbFirstDoctor IsNot Nothing Then cmbFirstDoctor.SelectedIndex = 0
                Return
            End If

            Dim ordered = doctors.OrderBy(Function(d) d.DrName).ToList()
            For Each doctor In ordered
                _doctorInfos.Add(doctor)
                cmbDoctorFilter.Properties.Items.Add(New ComboValueItem(Of Integer?)(doctor.DrID, doctor.DrName))
                If cmbFirstDoctor IsNot Nothing Then
                    cmbFirstDoctor.Properties.Items.Add(New ComboValueItem(Of Integer?)(doctor.DrID, doctor.DrName))
                End If
            Next

            Dim idx As Integer = 0
            If prevId.HasValue Then
                For i = 0 To cmbDoctorFilter.Properties.Items.Count - 1
                    Dim it = TryCast(cmbDoctorFilter.Properties.Items(i), ComboValueItem(Of Integer?))
                    If it Is Nothing OrElse Not it.Value.HasValue Then Continue For
                    If it.Value.Value = prevId.Value Then
                        idx = i
                        Exit For
                    End If
                Next
            End If
            If idx >= 0 AndAlso idx < cmbDoctorFilter.Properties.Items.Count Then cmbDoctorFilter.SelectedIndex = idx
        Finally
            _syncingFilterCombos = False
        End Try
    End Sub

    Private Sub WireEvents()
        AddHandler btnPrev.Click, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.PrevRequested", Sub() RaiseEvent PrevRequested())
        AddHandler btnNext.Click, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.NextRequested", Sub() RaiseEvent NextRequested())
        If btnPrevAppt IsNot Nothing Then
            AddHandler btnPrevAppt.Click, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.PrevApptRequested", Sub() RaiseEvent PrevApptRequested())
        End If
        If btnNextAppt IsNot Nothing Then
            AddHandler btnNextAppt.Click, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.NextApptRequested", Sub() RaiseEvent NextApptRequested())
        End If
        AddHandler btnToday.Click, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.TodayRequested", Sub() RaiseEvent TodayRequested())
        AddHandler btnAdd.Click, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.AddRequested", Sub() RaiseEvent AddRequested())
        AddHandler btnPrevView.Click, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.PrevViewRequested", Sub() RaiseEvent PrevViewRequested())
        AddHandler btnNextView.Click, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.NextViewRequested", Sub() RaiseEvent NextViewRequested())
        AddHandler btnAllPatients.Click, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.AllPatientsRequested", Sub() If Not _syncing Then RaiseEvent AllPatientsRequested())
        AddHandler btnThisPatient.Click, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.ThisPatientRequested", Sub() If Not _syncing Then RaiseEvent ThisPatientRequested())
        If cmbDoctorFilter IsNot Nothing Then AddHandler cmbDoctorFilter.SelectedIndexChanged, AddressOf CmbDoctorFilter_SelectedIndexChanged
        If cmbFirstDoctor IsNot Nothing Then AddHandler cmbFirstDoctor.SelectedIndexChanged, AddressOf CmbFirstDoctor_SelectedIndexChanged
        If cmbStatusFilter IsNot Nothing Then AddHandler cmbStatusFilter.SelectedIndexChanged, AddressOf CmbStatusFilter_SelectedIndexChanged
        If btnMoreScheduleOpts IsNot Nothing Then AddHandler btnMoreScheduleOpts.Click, AddressOf BtnMoreScheduleOpts_Click
        AddHandler chkUse24.CheckedChanged, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.Use24Changed", Sub() If Not _syncing Then RaiseEvent Use24Changed(chkUse24.Checked))
        AddHandler gotoDate.EditValueChanged, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.CurrentDateChanged", Sub() If Not _syncing AndAlso gotoDate.EditValue IsNot Nothing Then RaiseEvent CurrentDateChanged(Convert.ToDateTime(gotoDate.EditValue)))
        AddHandler cmbView.SelectedIndexChanged, AddressOf CmbView_SelectedIndexChanged
        AddHandler includeReasonCheck.CheckedChanged, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.IncludeReasonChanged", Sub() If Not _syncing Then RaiseEvent IncludeReasonChanged(includeReasonCheck.Checked))
        AddHandler boldFontCheck.CheckedChanged, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.FontProfileChanged.Bold", Sub() If Not _syncing Then RaiseEvent FontProfileChanged(boldFontCheck.Checked, sizeFontCheck.Checked))
        AddHandler sizeFontCheck.CheckedChanged, Sub() SafeRaiseHeaderAction("ApptHeaderCtl.FontProfileChanged.Size", Sub() If Not _syncing Then RaiseEvent FontProfileChanged(boldFontCheck.Checked, sizeFontCheck.Checked))
        AddHandler dtStartTime.EditValueChanged, AddressOf WorkingHours_EditValueChanged
        AddHandler dtEndTime.EditValueChanged, AddressOf WorkingHours_EditValueChanged
        If startColor IsNot Nothing Then AddHandler startColor.EditValueChanged, AddressOf ApptCardTimeColorPickers_EditValueChanged
        If endColor IsNot Nothing Then AddHandler endColor.EditValueChanged, AddressOf ApptCardTimeColorPickers_EditValueChanged
        If btnLabelsColors IsNot Nothing Then AddHandler btnLabelsColors.Click, AddressOf BtnLabelsColors_Click
        If color_Patient IsNot Nothing Then AddHandler color_Patient.EditValueChanged, AddressOf FlyoutApptCardLabelColors_EditValueChanged
        If color_Reason IsNot Nothing Then AddHandler color_Reason.EditValueChanged, AddressOf FlyoutApptCardLabelColors_EditValueChanged
        If color_Notes IsNot Nothing Then AddHandler color_Notes.EditValueChanged, AddressOf FlyoutApptCardLabelColors_EditValueChanged
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

    Private Sub InitializeViewNavButtons()
        If btnPrevView IsNot Nothing Then btnPrevView.Visible = False
        If btnNextView IsNot Nothing Then btnNextView.Visible = False
    End Sub

    Private Sub InitializeApptNavButtons()
        If btnPrevAppt IsNot Nothing Then btnPrevAppt.Enabled = False
        If btnNextAppt IsNot Nothing Then btnNextAppt.Enabled = False
        SyncApptNavButtonVisuals()
    End Sub

    Private Sub SyncApptNavButtonVisuals()
        If btnPrevAppt Is Nothing AndAlso btnNextAppt Is Nothing Then Return

        Dim rightArrow = Global.DentistX.My.Resources.Resources.tbtnArrowRight16
        Dim leftArrow = ApptNavArrowLeftImage(rightArrow)

        If btnPrevAppt IsNot Nothing Then
            btnPrevAppt.Text = String.Empty
            btnPrevAppt.ImageOptions.Location = ImageLocation.MiddleCenter
            btnPrevAppt.ImageOptions.Image = If(Eng, leftArrow, rightArrow)
            btnPrevAppt.ToolTip = If(Eng, "Previous appointment (filtered)", "الموعد السابق (بعد التصفية)")
        End If
        If btnNextAppt IsNot Nothing Then
            btnNextAppt.Text = String.Empty
            btnNextAppt.ImageOptions.Location = ImageLocation.MiddleCenter
            btnNextAppt.ImageOptions.Image = If(Eng, rightArrow, leftArrow)
            btnNextAppt.ToolTip = If(Eng, "Next appointment (filtered)", "الموعد التالي (بعد التصفية)")
        End If
    End Sub

    Private Shared Function ApptNavArrowLeftImage(rightArrow As Image) As Image
        If rightArrow Is Nothing Then Return Nothing
        If _apptNavArrowLeftImage IsNot Nothing Then Return _apptNavArrowLeftImage
        Dim mirrored = DirectCast(DirectCast(rightArrow, Bitmap).Clone(), Bitmap)
        mirrored.RotateFlip(RotateFlipType.RotateNoneFlipX)
        _apptNavArrowLeftImage = mirrored
        Return _apptNavArrowLeftImage
    End Function

    ''' <summary>Same as <see cref="SchedulerNew.InitializeWeekSnapshotButton"/>.</summary>
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
        ConfigureFlyoutApptCardLabelColorEdits()
        SyncFlyoutApptCardLabelPickersSuppressEvents()
    End Sub

    ''' <summary>Matches <see cref="ApptCardLabelColorsDialog"/> color-edit setup for the flyout (<c>color_Patient</c> …).</summary>
    Private Sub ConfigureFlyoutApptCardLabelColorEdits()
        ConfigureFlyoutSingleApptCardLabelColorEdit(color_Patient)
        ConfigureFlyoutSingleApptCardLabelColorEdit(color_Reason)
        ConfigureFlyoutSingleApptCardLabelColorEdit(color_Notes)
    End Sub

    Private Shared Sub ConfigureFlyoutSingleApptCardLabelColorEdit(edit As ColorEdit)
        If edit Is Nothing Then Return
        CType(edit.Properties, ISupportInitialize).BeginInit()
        Try
            edit.Properties.Appearance.Font = FlyoutCardLabelColorEditUiFont
            edit.Properties.Appearance.Options.UseFont = True
            edit.Properties.ColorDialogType = DevExpress.XtraEditors.Popup.ColorDialogType.Advanced
            edit.Properties.Buttons.Clear()
            edit.Properties.Buttons.Add(New EditorButton(ButtonPredefines.Combo))
        Finally
            CType(edit.Properties, ISupportInitialize).EndInit()
        End Try
    End Sub

    Private Sub SyncFlyoutApptCardLabelPickersSuppressEvents()
        If color_Patient Is Nothing AndAlso color_Reason Is Nothing AndAlso color_Notes Is Nothing Then Return
        _syncing = True
        Try
            If color_Patient IsNot Nothing Then color_Patient.EditValue = _cardPatientNameColor
            If color_Reason IsNot Nothing Then color_Reason.EditValue = _cardReasonColor
            If color_Notes IsNot Nothing Then color_Notes.EditValue = _cardNotesColor
        Finally
            _syncing = False
        End Try
    End Sub

    Private Sub BtnLabelsColors_Click(sender As Object, e As EventArgs)
        'Try
        '    If _syncing Then Return
        '    Using dlg As New ApptCardLabelColorsDialog(_cardPatientNameColor, _cardReasonColor, _cardNotesColor)
        '        If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
        '        _cardPatientNameColor = dlg.ResultPatientNameColor
        '        _cardReasonColor = dlg.ResultReasonColor
        '        _cardNotesColor = dlg.ResultNotesColor
        '        SyncFlyoutApptCardLabelPickersSuppressEvents()
        '        RaiseEvent ApptCardLabelColorsChanged(
        '            dlg.ResultPatientNameColor,
        '            dlg.ResultReasonColor,
        '            dlg.ResultNotesColor)
        '    End Using
        'Catch ex As Exception
        '    ApptErrorHelper.Report(ex,
        '                           "ApptHeaderCtl.BtnLabelsColors_Click",
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
            RaiseEvent ApptCardTimeColorsChanged(sc, ec)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.ApptCardTimeColorPickers_EditValueChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not update appointment time colors.",
                                   arabicMessage:="تعذر تحديث ألوان وقت الموعد.")
        End Try
    End Sub

    Private Sub FlyoutApptCardLabelColors_EditValueChanged(sender As Object, e As EventArgs)
        Try
            If _syncing Then Return
            Dim p = ColorFromPicker(color_Patient, _cardPatientNameColor)
            Dim r = ColorFromPicker(color_Reason, _cardReasonColor)
            Dim n = ColorFromPicker(color_Notes, _cardNotesColor)
            _cardPatientNameColor = p
            _cardReasonColor = r
            _cardNotesColor = n
            RaiseEvent ApptCardLabelColorsChanged(p, r, n)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.FlyoutApptCardLabelColors_EditValueChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not update appointment card label colors.",
                                   arabicMessage:="تعذر تحديث ألوان تسميات الموعد.")
        End Try
    End Sub

    Private Shared Function ColorFromPicker(picker As DevExpress.XtraEditors.ColorEdit, fallback As Color) As Color
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
            ApptErrorHelper.Report(ex, "ApptHeaderCtl.NormalizeColorValue", showUser:=False)
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

    Private Sub CmbView_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If _syncing Then Return
            Dim item = TryCast(cmbView.SelectedItem, ComboValueItem(Of ApptViewMode))
            If item Is Nothing Then Return
            RaiseEvent ViewChanged(item.Value)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.CmbView_SelectedIndexChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not change the appointment view.",
                                   arabicMessage:="تعذر تغيير عرض المواعيد.")
        End Try
    End Sub

    Private Sub WorkingHours_EditValueChanged(sender As Object, e As EventArgs)
        Try
            If _syncing Then Return
            Dim startTime = If(dtStartTime.EditValue Is Nothing, Date.Today.AddHours(8), Convert.ToDateTime(dtStartTime.EditValue))
            Dim endTime = If(dtEndTime.EditValue Is Nothing, Date.Today.AddHours(16), Convert.ToDateTime(dtEndTime.EditValue))
            RaiseEvent WorkingHoursChanged(startTime.TimeOfDay, endTime.TimeOfDay)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.WorkingHours_EditValueChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not update working hours.",
                                   arabicMessage:="تعذر تحديث ساعات العمل.")
        End Try
    End Sub

    Private Sub CmbDoctorFilter_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If _syncing OrElse _syncingFilterCombos Then Return
            Dim item = TryCast(cmbDoctorFilter.SelectedItem, ComboValueItem(Of Integer?))
            If item Is Nothing Then Return
            RaiseEvent DoctorFilterChanged(item.Value)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.CmbDoctorFilter_SelectedIndexChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not apply the doctor filter.",
                                   arabicMessage:="تعذر تطبيق فلتر الطبيب.")
        End Try
    End Sub

    Private Sub CmbFirstDoctor_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If _syncing OrElse _syncingFilterCombos Then Return
            If cmbFirstDoctor Is Nothing Then Return
            Dim item = TryCast(cmbFirstDoctor.SelectedItem, ComboValueItem(Of Integer?))
            If item Is Nothing Then Return
            RaiseEvent OrderByDoctorChanged(item.Value)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.CmbFirstDoctor_SelectedIndexChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not apply preferred doctor ordering.",
                                   arabicMessage:="تعذر تطبيق ترتيب الطبيب.")
        End Try
    End Sub

    Private Sub CmbStatusFilter_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If _syncing OrElse _syncingFilterCombos Then Return
            Dim item = TryCast(cmbStatusFilter.SelectedItem, ComboValueItem(Of String))
            If item Is Nothing Then Return
            RaiseEvent StatusFilterChanged(item.Value)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.CmbStatusFilter_SelectedIndexChanged",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not apply the status filter.",
                                   arabicMessage:="تعذر تطبيق فلتر الحالة.")
        End Try
    End Sub

    Private Sub BtnMoreScheduleOpts_Click(sender As Object, e As EventArgs)
        SafeRaiseHeaderAction("ApptHeaderCtl.BtnMoreScheduleOpts_Click", AddressOf ShowScheduleOptionsFlyout)
    End Sub

    Private Sub ShowScheduleOptionsFlyout()
        Try
            If flyoutScheduleOptions Is Nothing OrElse grpFilters Is Nothing OrElse btnMoreScheduleOpts Is Nothing Then Return
            If Not IsHandleCreated Then Return
            BeginInvoke(Sub()
                            Try
                                If IsDisposed Then Return
                                flyoutScheduleOptions.OwnerControl = grpFilters
                                flyoutScheduleOptions.Options.AnimationType = PopupToolWindowAnimation.Slide
                                flyoutScheduleOptions.Options.AnchorType = PopupToolWindowAnchor.Manual
                                Dim loc = grpFilters.PointToClient(btnMoreScheduleOpts.PointToScreen(Point.Empty))
                                If Eng Then
                                    flyoutScheduleOptions.Options.Location = New Point(loc.X, loc.Y + btnMoreScheduleOpts.Height + 4)
                                Else
                                    flyoutScheduleOptions.Options.Location = New Point(loc.X - flyoutScheduleOptions.Width + btnMoreScheduleOpts.Width, loc.Y + btnMoreScheduleOpts.Height + 4)
                                End If
                                flyoutScheduleOptions.ShowPopup()
                            Catch ex As Exception
                                ApptErrorHelper.Report(ex, "ApptHeaderCtl.ShowScheduleOptionsFlyout.Deferred", showUser:=False)
                            End Try
                        End Sub)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.ShowScheduleOptionsFlyout",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not open schedule options.",
                                   arabicMessage:="تعذر فتح خيارات الجدولة.")
        End Try
    End Sub

    Private Sub Header_Resize(sender As Object, e As EventArgs)
        RefreshLegendFiltersLayout()
    End Sub

    ''' <summary>Mirrors SchedulerNew.ResizeControlsProportionally tail: docked grpFilters/filtersTable must relayout via TablePanel, not SetBounds.</summary>
    Private Sub RefreshLegendFiltersLayout()
        If filtersTable IsNot Nothing AndAlso Not filtersTable.IsDisposed Then filtersTable.PerformLayout()
        If grpFilters IsNot Nothing AndAlso Not grpFilters.IsDisposed Then grpFilters.PerformLayout()
    End Sub

    ''' <summary>Same pattern as <see cref="SchedulerNew"/>: sync SimpleButton heights to combo/date row, then center the toolbar flow.</summary>
    Private Sub EnsureHeaderToolbarButtonHeightHook()
        If _headerToolbarButtonHeightHooked OrElse headerTable Is Nothing Then Return
        _headerToolbarButtonHeightHooked = True
        AddHandler headerTable.Layout, AddressOf HeaderTable_LayoutSyncButtonHeights
    End Sub

    Private Sub HeaderTable_LayoutSyncButtonHeights(sender As Object, e As LayoutEventArgs)
        'ScheduleHeaderToolbarRelayout()
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
            'ApplyHeaderToolbarCentering()
        Finally
            Try
                If headerTable IsNot Nothing AndAlso Not headerTable.IsDisposed Then
                    AddHandler headerTable.Layout, AddressOf HeaderTable_LayoutSyncButtonHeights
                End If
            Catch ex As Exception
                ApptErrorHelper.Report(ex, "ApptHeaderCtl.ApplyHeaderToolbarButtonHeights.Rehook", showUser:=False)
            End Try
        End Try
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

    Private Function FilterChipBaseFont() As Font
        If lblPatients.Appearance.Font IsNot Nothing Then Return lblPatients.Appearance.Font
        Return CreateCalibriFont(10.0F)
    End Function

    Private Sub StyleFilterChip(btn As SimpleButton, selected As Boolean, enabled As Boolean, idleBack As Color)
        Dim baseFont = FilterChipBaseFont()
        If Not enabled Then
            btn.Appearance.BackColor = Color.FromArgb(245, 245, 245)
            btn.Appearance.ForeColor = Color.DimGray
            btn.Appearance.Font = New Font(baseFont, FontStyle.Regular)
        ElseIf selected Then
            btn.Appearance.BackColor = Color.Yellow
            btn.Appearance.ForeColor = Color.Black
            btn.Appearance.Font = New Font(baseFont, FontStyle.Bold)
        Else
            btn.Appearance.BackColor = idleBack
            btn.Appearance.ForeColor = FilterContrastFore(idleBack)
            btn.Appearance.Font = New Font(baseFont, FontStyle.Regular)
        End If
        btn.Appearance.Options.UseBackColor = True
        btn.Appearance.Options.UseForeColor = True
        btn.Appearance.Options.UseFont = True
    End Sub

    Private Shared Function FilterContrastFore(back As Color) As Color
        Dim lum = (0.299F * back.R + 0.587F * back.G + 0.114F * back.B) / 255.0F
        Return If(lum < 0.45F, Color.White, Color.Black)
    End Function

    Private Shared Function NormalizeStatus(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then Return Nothing
        Return value.Trim()
    End Function

    Private Shared Sub SelectComboValue(Of T)(combo As ComboBoxEdit, value As T)
        For i = 0 To combo.Properties.Items.Count - 1
            Dim item = TryCast(combo.Properties.Items(i), ComboValueItem(Of T))
            If item IsNot Nothing AndAlso EqualityComparer(Of T).Default.Equals(item.Value, value) Then
                combo.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Private NotInheritable Class ComboValueItem(Of T)
        Public Sub New(value As T, textValue As String)
            Me.Value = value
            Me.Text = textValue
        End Sub

        Public ReadOnly Property Value As T
        Public ReadOnly Property Text As String

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    Private Sub FilterReasonButton_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub dtStartTime_CloseUp(sender As Object, e As CloseUpEventArgs)

    End Sub

    Private Sub dtStartTime_EditValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub dtStartTime_Leave(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnAllPatients_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnThisPatient_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BtnNextView_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BtnPrevView_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub dtEndTime_CloseUp(sender As Object, e As CloseUpEventArgs)

    End Sub

    Private Sub dtEndTime_EditValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub dtEndTime_Leave(sender As Object, e As EventArgs)

    End Sub

    Private Sub chkUse24_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BtnPrev_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BtnToday_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub gotoDate_EditValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnWeekSnapshot_Click(sender As Object, e As EventArgs) Handles btnWeekSnapshot.Click
        Try
            RaiseEvent WeekSnapshotRequested()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptHeaderCtl.btnWeekSnapshot_Click",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not start the snapshot action.",
                                   arabicMessage:="تعذر بدء إجراء اللقطة.")
        End Try
    End Sub

    Private Sub FilterDoctorAllButton_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnLabs_Click(sender As Object, e As EventArgs) Handles btnLabs.Click
        Try
            Using F As New FrmLabSendWhats
                Dim patientId = TryGetScheduleCurrentPatientId()
                If patientId.HasValue AndAlso patientId.Value > 0 Then
                    F.patientCombo.SetCurrentPatientName(patientId.Value)
                End If
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

    ''' <summary>
    ''' Resolves patient for lab WhatsApp: schedule <see cref="ApptHostCtl.CurrentPatient"/>,
    ''' then open patient workspace (<see cref="FormManager.GetCurrentPatient"/>),
    ''' then schedule single-patient filter <see cref="ApptHostCtl.FilterPatientId"/> when set without a loaded <c>CurrentPatient</c>.
    ''' </summary>
    Private Function TryGetScheduleCurrentPatientId() As Integer?
        Dim host = TryFindApptHostAncestor(Me)
        If host IsNot Nothing AndAlso host.CurrentPatient IsNot Nothing AndAlso host.CurrentPatient.PatientID > 0 Then
            Return host.CurrentPatient.PatientID
        End If
        If FormManager.Instance IsNot Nothing AndAlso FormManager.Instance.IsBasePatientFormOpen Then
            Dim p = FormManager.Instance.GetCurrentPatient()
            If p IsNot Nothing AndAlso p.PatientID > 0 Then Return p.PatientID
        End If
        If host IsNot Nothing AndAlso host.FilterPatientId.HasValue AndAlso host.FilterPatientId.Value > 0 Then
            Return host.FilterPatientId.Value
        End If
        Return Nothing
    End Function

    Private Shared Function TryFindApptHostAncestor(start As Control) As ApptHostCtl
        Dim c As Control = start
        While c IsNot Nothing
            Dim host = TryCast(c, ApptHostCtl)
            If host IsNot Nothing Then Return host
            c = c.Parent
        End While
        Return Nothing
    End Function
End Class
