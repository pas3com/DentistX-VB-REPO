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
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

Public Class ApptHeaderCtl

    Private _syncing As Boolean
    Private ReadOnly _doctorInfos As New List(Of ApptDoctorInfo)()

    Private Const DoctorFilterSlimScrollHeight As Integer = 8
    Private Shared ReadOnly _filtersBaseR0 As Single = 36.53F
    Private Shared ReadOnly _filtersBaseR1 As Single = 32.65F
    Private Shared ReadOnly _filtersBaseR2 As Single = 29.82F
    Private Const FiltersDoctorRowBoostTakeFromNeighbor As Single = 2.0F

    Private _doctorFilterSlimScroll As DevExpress.XtraEditors.HScrollBar
    Private _doctorFilterSlimScrollWired As Boolean
    Private _doctorFilterCompactionBaseCaptured As Boolean
    Private _doctorFilterBasePanelPadding As Padding
    Private _doctorFilterBaseFlowPadding As Padding
    Private ReadOnly _doctorFilterButtonBaseMargins As New Dictionary(Of String, Padding)(StringComparer.OrdinalIgnoreCase)
    Private _filtersDoctorRowBoostActive As Boolean

    Private _headerToolbarButtonHeightHooked As Boolean
    Private _headerToolbarRelayoutPending As Boolean
    Private _inHeaderToolbarCentering As Boolean
    Private _headerToolbarCenteringHooks As Boolean
    Private _weekSnapshotToolTip As ToolTip

    Public Event PrevRequested As Action
    Public Event NextRequested As Action
    Public Event TodayRequested As Action
    Public Event AddRequested As Action
    Public Event WeekSnapshotRequested As Action
    Public Event CurrentDateChanged As Action(Of DateTime)
    Public Event ViewChanged As Action(Of ApptViewMode)
    Public Event Use24Changed As Action(Of Boolean)
    Public Event AllPatientsRequested As Action
    Public Event ThisPatientRequested As Action
    Public Event DoctorFilterChanged As Action(Of Integer?)
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

    Private _cardPatientNameColor As Color
    Private _cardReasonColor As Color
    Private _cardNotesColor As Color

    Public Sub New()
        InitializeComponent()
        Dock = DockStyle.Fill
        cmbView.Properties.Items.Clear()
        InitializeComboValues()
        LocalizeStaticText()
        pnlDoctorFilterScroll.AutoScroll = False
        WireDoctorSlotButtons()
        WireEvents()
        AddHandler Resize, AddressOf Header_Resize
        AddHandler pnlDoctorFilterScroll.Resize, AddressOf DoctorFilterScrollPanel_Resize
        AdjustDoctorFilterScrollExtent()
        pnlHeader.AutoScroll = True
        legendPanel.AutoScroll = True
        InitializeApptCardTimeColorPickersFromSettings()
        InitializeApptCardLabelColorCacheFromSettings()
        InitializeViewNavButtons()
        InitializeWeekSnapshotButton()
    End Sub

    ''' <summary>Matches <see cref="SchedulerNew.UpdateViewNavButtonVisibility"/>.</summary>
    Public Sub UpdateViewNavButtonVisibility(backCount As Integer, forwardCount As Integer)
        If btnPrevView IsNot Nothing Then btnPrevView.Visible = backCount > 0
        If btnNextView IsNot Nothing Then btnNextView.Visible = forwardCount > 0
    End Sub

    ''' <summary>Updates the view combo without recording view history (used for Prev/Next view and host <see cref="ApptHostCtl.SetView"/>).</summary>
    Public Sub SyncViewComboToState(state As ApptState)
        If state Is Nothing OrElse cmbView Is Nothing Then Return
        _syncing = True
        Try
            SelectComboValue(cmbView, state.CurrentView)
        Finally
            _syncing = False
        End Try
    End Sub

    Public Sub ApplyState(state As ApptState, data As ApptDataBundle, patientCaption As String, hasCurrentPatient As Boolean)
        _syncing = True
        Try
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
            RefreshStatusButtons(state)
            RefreshDoctorButtons(state)
            RefreshCountChips()
            If startColor IsNot Nothing Then startColor.EditValue = state.ApptCardStartTimeColor
            If endColor IsNot Nothing Then endColor.EditValue = state.ApptCardEndTimeColor
            ApplyHeaderTimeLabelColors(state.ApptCardStartTimeColor, state.ApptCardEndTimeColor)
            _cardPatientNameColor = state.ApptCardPatientNameColor
            _cardReasonColor = state.ApptCardReasonColor
            _cardNotesColor = state.ApptCardNotesColor
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
        btnFilterDoctor0.Text = If(Eng, "All Doctors", "كل الاطباء")
        btnAllPatients.Text = If(Eng, "All Patients", "كل المرضى")
        btnThisPatient.Text = If(Eng, "This Patient", "هذا المريض")
        btnAllReasons.Text = If(Eng, "All Reasons", "كل الأسباب")
        btnPending.Text = If(Eng, "Pending", "قيد الانتظار")
        btnRunning.Text = If(Eng, "Running", "قيد التنفيذ")
        btnCompleted.Text = If(Eng, "Completed", "منجز")
        btnCanceled.Text = If(Eng, "Canceled", "ملغى")
        btnPostponed.Text = If(Eng, "Postponed", "مؤجل")
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

        ' Same as SchedulerNew.LoadDoctorLegend: LTR for English keeps filtersTable/flow layout stable; Arabic mirrors legend.
        legendPanel.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        grpFilters.Text = If(Eng, "Filter Tools", "أدوات التصفية")
        legendPanel.Text = If(Eng, "Filters", "تصفية")
        If flpDoctorFilterButtons IsNot Nothing Then
            flpDoctorFilterButtons.RightToLeft = legendPanel.RightToLeft
            flpDoctorFilterButtons.FlowDirection = If(legendPanel.RightToLeft = RightToLeft.Yes, FlowDirection.RightToLeft, FlowDirection.LeftToRight)
        End If
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

    Private Sub RefreshStatusButtons(state As ApptState)
        Dim buttons = {btnAllReasons, btnPending, btnRunning, btnCompleted, btnCanceled, btnPostponed}
        Dim sel = 0
        Select Case NormalizeStatus(state.VisibleStatus)
            Case "Pending" : sel = 1
            Case "Running" : sel = 2
            Case "Completed" : sel = 3
            Case "Canceled" : sel = 4
            Case "Postponed" : sel = 5
        End Select

        For i = 0 To buttons.Length - 1
            StyleFilterChip(buttons(i), sel = i, True, ReasonIdleColor(i))
        Next
    End Sub

    Private Sub RefreshDoctorButtons(state As ApptState)
        StyleFilterChip(btnFilterDoctor0, Not state.DoctorFilterId.HasValue, True, Color.FromArgb(200, 230, 200))
        For i = 0 To _doctorInfos.Count - 1
            Dim doctor = _doctorInfos(i)
            Dim btn = DoctorSlotButton(i + 1)
            StyleFilterChip(btn, state.DoctorFilterId.HasValue AndAlso state.DoctorFilterId.Value = doctor.DrID, btn.Enabled, doctor.DrColor)
        Next
    End Sub

    Private Sub ApplyDoctorLegend(doctors As IEnumerable(Of ApptDoctorInfo))
        _doctorInfos.Clear()
        For slot = 1 To 8
            Dim b = DoctorSlotButton(slot)
            b.Tag = Nothing
            b.Text = String.Empty
            b.Enabled = False
            StyleFilterChip(b, False, False, Color.WhiteSmoke)
        Next

        If doctors Is Nothing Then
            AdjustDoctorFilterScrollExtent()
            Return
        End If

        Dim ordered = doctors.OrderBy(Function(d) d.DrName).Take(8).ToList()
        For i = 0 To ordered.Count - 1
            Dim doctor = ordered(i)
            _doctorInfos.Add(doctor)
            Dim b = DoctorSlotButton(i + 1)
            b.Text = doctor.DrName
            b.Tag = doctor.DrID
            b.Enabled = True
        Next
        AdjustDoctorFilterScrollExtent()
    End Sub

    Private Function DoctorSlotButton(slot As Integer) As SimpleButton
        Select Case slot
            Case 1 : Return btnFilterDoctor1
            Case 2 : Return btnFilterDoctor2
            Case 3 : Return btnFilterDoctor3
            Case 4 : Return btnFilterDoctor4
            Case 5 : Return btnFilterDoctor5
            Case 6 : Return btnFilterDoctor6
            Case 7 : Return btnFilterDoctor7
            Case 8 : Return btnFilterDoctor8
            Case Else : Throw New ArgumentOutOfRangeException(NameOf(slot))
        End Select
    End Function

    Private Sub WireDoctorSlotButtons()
        For slot = 1 To 8
            AddHandler DoctorSlotButton(slot).Click, AddressOf DoctorFilterButton_Click
        Next
    End Sub

    Private Sub WireEvents()
        AddHandler btnPrev.Click, Sub() RaiseEvent PrevRequested()
        AddHandler btnNext.Click, Sub() RaiseEvent NextRequested()
        AddHandler btnToday.Click, Sub() RaiseEvent TodayRequested()
        AddHandler btnAdd.Click, Sub() RaiseEvent AddRequested()
        AddHandler btnPrevView.Click, Sub() RaiseEvent PrevViewRequested()
        AddHandler btnNextView.Click, Sub() RaiseEvent NextViewRequested()
        AddHandler btnAllPatients.Click, Sub() If Not _syncing Then RaiseEvent AllPatientsRequested()
        AddHandler btnThisPatient.Click, Sub() If Not _syncing Then RaiseEvent ThisPatientRequested()
        AddHandler btnFilterDoctor0.Click, Sub() If Not _syncing Then RaiseEvent DoctorFilterChanged(Nothing)
        AddHandler btnAllReasons.Click, Sub() If Not _syncing Then RaiseEvent StatusFilterChanged(Nothing)
        AddHandler btnPending.Click, Sub() If Not _syncing Then RaiseEvent StatusFilterChanged("Pending")
        AddHandler btnRunning.Click, Sub() If Not _syncing Then RaiseEvent StatusFilterChanged("Running")
        AddHandler btnCompleted.Click, Sub() If Not _syncing Then RaiseEvent StatusFilterChanged("Completed")
        AddHandler btnCanceled.Click, Sub() If Not _syncing Then RaiseEvent StatusFilterChanged("Canceled")
        AddHandler btnPostponed.Click, Sub() If Not _syncing Then RaiseEvent StatusFilterChanged("Postponed")
        AddHandler chkUse24.CheckedChanged, Sub() If Not _syncing Then RaiseEvent Use24Changed(chkUse24.Checked)
        AddHandler gotoDate.EditValueChanged, Sub() If Not _syncing AndAlso gotoDate.EditValue IsNot Nothing Then RaiseEvent CurrentDateChanged(Convert.ToDateTime(gotoDate.EditValue))
        AddHandler cmbView.SelectedIndexChanged, AddressOf CmbView_SelectedIndexChanged
        AddHandler includeReasonCheck.CheckedChanged, Sub() If Not _syncing Then RaiseEvent IncludeReasonChanged(includeReasonCheck.Checked)
        AddHandler boldFontCheck.CheckedChanged, Sub() If Not _syncing Then RaiseEvent FontProfileChanged(boldFontCheck.Checked, sizeFontCheck.Checked)
        AddHandler sizeFontCheck.CheckedChanged, Sub() If Not _syncing Then RaiseEvent FontProfileChanged(boldFontCheck.Checked, sizeFontCheck.Checked)
        AddHandler dtStartTime.EditValueChanged, AddressOf WorkingHours_EditValueChanged
        AddHandler dtEndTime.EditValueChanged, AddressOf WorkingHours_EditValueChanged
        If startColor IsNot Nothing Then AddHandler startColor.EditValueChanged, AddressOf ApptCardTimeColorPickers_EditValueChanged
        If endColor IsNot Nothing Then AddHandler endColor.EditValueChanged, AddressOf ApptCardTimeColorPickers_EditValueChanged
        If btnLabelsColors IsNot Nothing Then AddHandler btnLabelsColors.Click, AddressOf BtnLabelsColors_Click
    End Sub

    Private Sub InitializeViewNavButtons()
        If btnPrevView IsNot Nothing Then btnPrevView.Visible = False
        If btnNextView IsNot Nothing Then btnNextView.Visible = False
    End Sub

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
    End Sub

    Private Sub BtnLabelsColors_Click(sender As Object, e As EventArgs)
        If _syncing Then Return
        Using dlg As New ApptCardLabelColorsDialog(_cardPatientNameColor, _cardReasonColor, _cardNotesColor)
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
            _cardPatientNameColor = dlg.ResultPatientNameColor
            _cardReasonColor = dlg.ResultReasonColor
            _cardNotesColor = dlg.ResultNotesColor
            RaiseEvent ApptCardLabelColorsChanged(
                dlg.ResultPatientNameColor,
                dlg.ResultReasonColor,
                dlg.ResultNotesColor)
        End Using
    End Sub

    Private Sub ApptCardTimeColorPickers_EditValueChanged(sender As Object, e As EventArgs)
        If _syncing Then Return
        Dim sc = ColorFromPicker(startColor, Color.Red)
        Dim ec = ColorFromPicker(endColor, Color.SteelBlue)
        ApplyHeaderTimeLabelColors(sc, ec)
        RaiseEvent ApptCardTimeColorsChanged(sc, ec)
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
        Catch
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
        If _syncing Then Return
        Dim item = TryCast(cmbView.SelectedItem, ComboValueItem(Of ApptViewMode))
        If item Is Nothing Then Return
        RaiseEvent ViewChanged(item.Value)
    End Sub

    Private Sub WorkingHours_EditValueChanged(sender As Object, e As EventArgs)
        If _syncing Then Return
        Dim startTime = If(dtStartTime.EditValue Is Nothing, Date.Today.AddHours(8), Convert.ToDateTime(dtStartTime.EditValue))
        Dim endTime = If(dtEndTime.EditValue Is Nothing, Date.Today.AddHours(16), Convert.ToDateTime(dtEndTime.EditValue))
        RaiseEvent WorkingHoursChanged(startTime.TimeOfDay, endTime.TimeOfDay)
    End Sub

    Private Sub DoctorFilterButton_Click(sender As Object, e As EventArgs)
        If _syncing Then Return
        Dim btn = TryCast(sender, SimpleButton)
        If btn Is Nothing OrElse btn.Tag Is Nothing Then Return
        RaiseEvent DoctorFilterChanged(CInt(btn.Tag))
    End Sub

    Private Sub Header_Resize(sender As Object, e As EventArgs)
        AdjustDoctorFilterScrollExtent()
        RefreshLegendFiltersLayout()
        'ScheduleHeaderToolbarRelayout()
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
            Catch
            End Try
        End Try
    End Sub


    Private Sub DoctorFilterScrollPanel_Resize(sender As Object, e As EventArgs)
        PositionDoctorFilterSlimScroll()
        AdjustDoctorFilterScrollExtent()
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
            sb.Margin = If(compact, New Padding(baseMargin.Left, 0, baseMargin.Right, 0), baseMargin)
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

    Private Sub SyncDoctorStripButtonHeights(scrollReserve As Integer)
        If pnlDoctorFilterScroll Is Nothing OrElse flpDoctorFilterButtons Is Nothing Then Return
        Dim ch = Math.Max(1, pnlDoctorFilterScroll.ClientSize.Height - scrollReserve)
        Dim h = Math.Max(22, ch - 2)
        For slot = 1 To 8
            Dim btn = DoctorSlotButton(slot)
            If Not btn.Enabled Then Continue For
            btn.Height = Math.Max(22, h - 4)
            btn.MinimumSize = New Size(72, btn.Height)
        Next
    End Sub

    Private Sub AdjustDoctorFilterScrollExtent()
        If pnlDoctorFilterScroll Is Nothing OrElse flpDoctorFilterButtons Is Nothing Then Return
        EnsureDoctorFilterSlimScroll()

        Dim needH As Boolean
        SyncDoctorStripButtonHeights(0)
        flpDoctorFilterButtons.PerformLayout()
        Dim prefW = flpDoctorFilterButtons.PreferredSize.Width
        Dim cw = Math.Max(1, pnlDoctorFilterScroll.ClientSize.Width)
        needH = prefW > cw

        SyncDoctorStripButtonHeights(If(needH, DoctorFilterSlimScrollHeight, 0))
        flpDoctorFilterButtons.PerformLayout()
        prefW = flpDoctorFilterButtons.PreferredSize.Width
        cw = Math.Max(1, pnlDoctorFilterScroll.ClientSize.Width)
        needH = prefW > cw

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
            SyncDoctorStripButtonHeights(DoctorFilterSlimScrollHeight)
            flpDoctorFilterButtons.PerformLayout()
            prefW = flpDoctorFilterButtons.PreferredSize.Width
            cw = Math.Max(1, pnlDoctorFilterScroll.ClientSize.Width)
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

    Private Shared Function ReasonIdleColor(index As Integer) As Color
        Select Case index
            Case 0 : Return Color.PaleTurquoise
            Case 1 : Return Color.LightGoldenrodYellow
            Case 2 : Return Color.LightSkyBlue
            Case 3 : Return Color.LightGreen
            Case 4 : Return Color.LightCoral
            Case 5 : Return Color.LightGray
            Case Else : Return Color.WhiteSmoke
        End Select
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
        RaiseEvent WeekSnapshotRequested()
    End Sub

    Private Sub FilterDoctorAllButton_Click(sender As Object, e As EventArgs)

    End Sub
End Class
