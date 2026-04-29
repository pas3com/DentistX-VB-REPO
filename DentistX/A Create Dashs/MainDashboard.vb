Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports DentistX.DashDataModel.DatabaseHelper
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraBars
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class MainDashboard

    Private ReadOnly _dbHelper As New DashDataModel.DatabaseHelper()
    Private ReadOnly _connectionString As String = DashDataModel.DatabaseHelper._connectionString
    Private _currentFilter As New DashboardFilter()
    Private _loading As Boolean = False
    Private _patients As List(Of Patient)
    Private _appointments As List(Of ApptDash)
    Private _treatments As List(Of PatientTreatment)
    Private _payments As List(Of PatientPayment)
    Private _revenueTrend As List(Of RevenueTrend)
    Private _demographics As List(Of PatientDemographic)
    Private _TreatmentStatistics As New TreatmentStatistics
    Private _DoctorID As Integer? = Nothing
    Friend WithEvents statusLabel As ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToastNotifications As DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager

    Private _apptPage As ApptPage
    Private _finPage As FinancialPage
    Private _trtPage As TreatmentsPage
    Private _trtPage2 As TreatmentsPage2
    Private _anlPage As AnalyticsPage

    Private _dashboardKPI As DashboardKPI

    Public Sub New()
        InitializeComponent()
        SetupDashboard()
        LoadDashboardDataAsync()
        CheckAlerts()
    End Sub
    Private Sub SetupDashboard()
        ' Set default date range
        dateEditFrom.EditValue = Date.Today.AddDays(-30)
        DateEditTo.EditValue = Date.Today

        ToastNotifications = New DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager()
        CreateStatusBar()
        ' Configure chart appearance
        ConfigureCharts()
        ' Set up grid formats
        ConfigureGrids()
        ' Initialize filters
        InitializeFilters()
        'AddHandler demographicChart.MouseClick, AddressOf DemographicChart_MouseClick

    End Sub
    ' Create StatusBar
    Private Sub CreateStatusBar()
        StatusStrip1 = New StatusStrip()
        statusLabel = New ToolStripStatusLabel(If(Eng, "Ready", "جاهز"))
        StatusStrip1.Items.Add(statusLabel)
        ' Add progress bar
        Dim progressBar As New ToolStripProgressBar()
        progressBar.Visible = False
        StatusStrip1.Items.Add(progressBar)
        StatusStrip1.Dock = DockStyle.Bottom
        Me.Controls.Add(StatusStrip1)
    End Sub
    ' ============ CHARTS CONFIGURATION ============
#Region "Chart Configuration"

    Private Sub ConfigureCharts()
        ' Revenue Chart
        revenueChart.SeriesTemplate.View = New DevExpress.XtraCharts.SideBySideBarSeriesView()
        revenueChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False
        revenueChart.Titles.Add(New ChartTitle() With {.Text = If(Eng, "Revenue Trend (Last 30 Days)", "اتجاه الإيرادات (آخر 30 يومًا)")})
        ' Demographic Chart
        demographicChart.SeriesTemplate.ChangeView(DevExpress.XtraCharts.ViewType.Pie3D)
        demographicChart.Titles.Add(New ChartTitle() With {.Text = If(Eng, "Patient Demographics", "التركيبة السكانية للمرضى")})
    End Sub

    Private Sub BindDemographicChartPie3D()
        If _demographics Is Nothing Then Return

        demographicChart.Series.Clear()

        Dim series As New Series("Demographics", ViewType.Pie3D)
        series.DataSource = _demographics

        series.ArgumentScaleType = ScaleType.Qualitative
        series.ValueScaleType = ScaleType.Numerical

        series.ArgumentDataMember = "Category"
        series.ValueDataMembers.AddRange("Count")

        ' LABELS: show male / female counts
        series.Label.TextPattern =
        "{A}: {V}" & vbCrLf &
        "M: {MaleCount}  F: {FemaleCount}" & vbCrLf &
        "({VP:##.##}%)"

        series.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default
        series.Label.DXFont =
        New DevExpress.Drawing.DXFont("Tahoma", 9, DevExpress.Drawing.DXFontStyle.Bold)

        ' TOOLTIP (cleaner when labels overlap)
        series.ToolTipPointPattern =
        "{A}" & vbCrLf &
        "Total: {V}" & vbCrLf &
        "Male: {MaleCount}" & vbCrLf &
        "Female: {FemaleCount}"

        demographicChart.Series.Add(series)
        demographicChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True

        series.SeriesPointsSorting = SortingMode.Descending
        series.SeriesPointsSortingKey = SeriesPointKey.Value_1

        demographicChart.RefreshData()
    End Sub
    Private Sub BindDemographicChartPie()
        If _demographics Is Nothing Then Return

        demographicChart.Series.Clear()
        demographicChart.Titles.Clear()

        Dim series As New Series("Age Distribution", ViewType.Pie)
        series.DataSource = _demographics

        series.ArgumentDataMember = "Category"
        series.ValueDataMembers.AddRange("Count")

        series.Label.TextPattern =
        "{A}: {V}" & vbCrLf &
        "M: {MaleCount}  F: {FemaleCount}" & vbCrLf &
        "({VP:##.##}%)"

        series.ToolTipPointPattern =
        "{A}" & vbCrLf &
        "Total: {V}" & vbCrLf &
        "Male: {MaleCount}" & vbCrLf &
        "Female: {FemaleCount}"

        demographicChart.Series.Add(series)

        ' Tag each point with its demographic object
        'For i = 0 To series.Points.Count - 1
        '    series.Points(i).Tag = _demographics(i)
        'Next
        For Each pt As SeriesPoint In series.Points
            Dim category = pt.Argument.ToString()
            pt.Tag = _demographics.FirstOrDefault(Function(d) d.Category = category)
        Next

        ApplyGenderDominanceColors(series)
        UpdateDemographicLegendSummary()

        ' Enable interaction
        AddHandler demographicChart.ObjectHotTracked, AddressOf DemographicChart_ObjectHotTracked
        AddHandler demographicChart.ObjectSelected, AddressOf DemographicChart_ObjectSelected

        demographicChart.RefreshData()
    End Sub

    Private Sub ApplyGenderDominanceColors(series As Series)

        For Each pt As SeriesPoint In series.Points
            Dim demo = TryCast(pt.Tag, PatientDemographic)
            If demo Is Nothing Then Continue For

            If demo.MaleCount > demo.FemaleCount Then
                pt.Color = Color.FromArgb(70, 130, 180) ' SteelBlue
            ElseIf demo.FemaleCount > demo.MaleCount Then
                pt.Color = Color.FromArgb(219, 112, 147) ' PaleVioletRed
            Else
                pt.Color = Color.FromArgb(160, 160, 160) ' Neutral
            End If
        Next
    End Sub
    Private Sub UpdateDemographicLegendSummary()
        If _demographics Is Nothing Then Return

        Dim totalMale = _demographics.Sum(Function(d) d.MaleCount)
        Dim totalFemale = _demographics.Sum(Function(d) d.FemaleCount)

        demographicChart.Titles.Clear()
        demographicChart.Titles.Add(New ChartTitle With {
        .Text = $"Patient Demographics   |   ♂ {totalMale}   ♀ {totalFemale}"
    })
    End Sub
    Private Sub DemographicChart_ObjectSelected(sender As Object, e As HotTrackEventArgs)
        Dim hit = TryCast(e.HitInfo, ChartHitInfo)
        If hit Is Nothing OrElse hit.SeriesPoint Is Nothing Then Return

        Dim demo = TryCast(hit.SeriesPoint.Tag, PatientDemographic)
        If demo Is Nothing Then Return

        ShowGenderDrillDown(demo)
    End Sub
    Private Sub ShowGenderDrillDown(demo As PatientDemographic)
        demographicChart.Series.Clear()
        demographicChart.Titles.Clear()

        Dim barSeries As New Series(demo.Category & " – Gender Split", ViewType.Bar)

        barSeries.Points.Add(New SeriesPoint("Male", demo.MaleCount))
        barSeries.Points.Add(New SeriesPoint("Female", demo.FemaleCount))

        barSeries.Points(0).Color = Color.FromArgb(70, 130, 180)
        barSeries.Points(1).Color = Color.FromArgb(219, 112, 147)

        demographicChart.Series.Add(barSeries)

        demographicChart.Titles.Add(New ChartTitle With {
        .Text = $"{demo.Category} – Gender Breakdown (click background to return)"
    })

        ' Smooth animation
        demographicChart.AnimationStartMode = ChartAnimationMode.OnDataChanged
    End Sub
    Private _isRebinding As Boolean = False

    Private Sub DemographicChart_ObjectHotTracked(sender As Object, e As HotTrackEventArgs)
        If _isRebinding Then Exit Sub

        If e.HitInfo Is Nothing OrElse e.HitInfo.SeriesPoint Is Nothing Then
            _isRebinding = True
            BindDemographicChartPie3D()
            _isRebinding = False
        End If
    End Sub

    Private Sub DemographicChart_MouseClick(sender As Object, e As MouseEventArgs)
        Dim chart = CType(sender, DevExpress.XtraCharts.ChartControl)

        Dim hitInfo As ChartHitInfo = chart.CalcHitInfo(e.Location)

        ' Did we click a pie slice?
        If hitInfo IsNot Nothing AndAlso hitInfo.SeriesPoint IsNot Nothing Then
            Dim demo = TryCast(hitInfo.SeriesPoint.Tag, PatientDemographic)
            If demo IsNot Nothing Then
                ShowGenderDrillDown(demo)
            End If
        Else
            ' Clicked background → return to main pie
            BindDemographicChartPie3D()
        End If
    End Sub

    ' ============ CHART DATA BINDING ============
    Private Sub BindRevenueChart()
        If _revenueTrend Is Nothing Then Return

        revenueChart.Series.Clear()
        revenueChart.DataSource = _revenueTrend

        revenueChart.SeriesDataMember = Nothing
        revenueChart.SeriesTemplate.ArgumentDataMember = "Period"
        revenueChart.SeriesTemplate.ValueDataMembers.Clear()
        revenueChart.SeriesTemplate.ValueDataMembers.AddRange("Revenue")

        Dim targetSeries As New Series("Target", ViewType.Line)
        targetSeries.DataSource = _revenueTrend
        targetSeries.ArgumentDataMember = "Period"
        targetSeries.ValueDataMembers.AddRange("Target")

        With CType(targetSeries.View, LineSeriesView)
            .Color = Color.Red
            .LineStyle.DashStyle = DashStyle.Dash
        End With

        revenueChart.Series.Add(targetSeries)
    End Sub

#End Region

    ' ============ GRID CONFIGURATIONS ============
#Region "GRID CONFIGURATIONS"


    Private Sub ConfigureGrids()
        ' Configure Patients Grid
        If gridViewPatients IsNot Nothing Then
            ConfigurePatientGrid()
        End If
        ' Configure Appointments Grid (if you have one)
        ' If gridViewAppointments IsNot Nothing Then
        '     ConfigureAppointmentGrid()
        ' End If

        ' Configure Financial Grid
        ' If gridViewFinancial IsNot Nothing Then
        '     ConfigureFinancialGrid()
        ' End If

        ' Configure Treatments Grid
        ' If gridViewTreatments IsNot Nothing Then
        '     ConfigureTreatmentGrid()
        ' End If
    End Sub
    Private Sub ConfigurePatientGrid()
        With gridViewPatients
            .OptionsView.ShowAutoFilterRow = True
            .OptionsView.ShowGroupPanel = True
            .OptionsBehavior.Editable = False
            .OptionsSelection.MultiSelect = True
            .OptionsSelection.EnableAppearanceFocusedCell = False
            .OptionsDetail.EnableMasterViewMode = False
            ' Clear existing columns if any
            .Columns.Clear()
            ' Add columns with proper mapping
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "PatientID",
                    .Caption = If(Eng, "ID", "المعرف"),
                    .Visible = False,
                    .VisibleIndex = 0,
                    .Width = 30})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "PatientName",
                    .Caption = If(Eng, "Patient Name", "اسم المريض"),
                    .Visible = True,
                    .VisibleIndex = 1}) ',
            '.Width = 200})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "PatientNumber",
                    .Caption = If(Eng, "Patient #", "رقم المريض"),
                    .Visible = True,
                    .VisibleIndex = 2,
                    .Width = 50})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "Sex",
                    .Caption = If(Eng, "Gender", "الجنس"),
                    .Visible = True,
                    .VisibleIndex = 3,
                    .Width = 50})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "Age",
                    .Caption = If(Eng, "Age", "العمر"),
                    .Visible = True,
                    .VisibleIndex = 4,
                    .Width = 50})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "Phone",
                    .Caption = If(Eng, "Phone", "الهاتف"),
                    .Visible = True,
                    .VisibleIndex = 5,
                    .Width = 60})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "CreateDate",
                    .Caption = If(Eng, "Registration Date", "تاريخ التسجيل"),
                    .Visible = True,
                    .VisibleIndex = 6,
                    .Width = 60})
            If _currentFilter.ShowOnlyUnpaid Then
                .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                                   .FieldName = "Balance",
                                   .Caption = If(Eng, "Balance", "الرصيد"),
                                   .Visible = True,
                                   .VisibleIndex = 6,
                                   .Width = 30})
            End If
            ' Add row style event for highlighting If(_currentFilter.ShowOnlyUnpaid, 1, 2)
            AddHandler .RowStyle, AddressOf PatientsGrid_RowStyle
            ' Add double-click event
            AddHandler .DoubleClick, AddressOf GridViewPatients_DoubleClick
            AddHandler .FocusedRowChanged, AddressOf GridViewPatients_FocusedRowChanged
        End With

        With GridViewPayments
            .OptionsView.ShowAutoFilterRow = True
            .OptionsView.ShowGroupPanel = True
            .OptionsBehavior.Editable = False
            .OptionsSelection.MultiSelect = True
            .OptionsSelection.EnableAppearanceFocusedCell = False
            .OptionsDetail.EnableMasterViewMode = False
            ' Clear existing columns if any
            .Columns.Clear()
            ' Add columns with proper mapping
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "PayID",
                    .Caption = If(Eng, "ID", "المعرف"),
                    .Visible = False,
                    .VisibleIndex = 0,
                    .Width = 25})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "PayDate",
                    .Caption = If(Eng, "Registration Date", "تاريخ الدفعة"),
                    .Visible = True,
                    .VisibleIndex = 1,
                    .Width = 50})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "PayValue",
                    .Caption = If(Eng, "Pay Value ", "قيمة الدفعة"),
                    .Visible = True,
                    .VisibleIndex = 2,
                    .Width = 30})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "PayType",
                    .Caption = If(Eng, "PayType", "نوع الدفعة"),
                    .Visible = True,
                    .VisibleIndex = 3,
                    .Width = 30})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "Notes",
                    .Caption = If(Eng, "Notes", "ملاحظات"),
                    .Visible = True,
                    .VisibleIndex = 4}) ',
            '.Width = 100

        End With
        With GridViewTreatments
            .OptionsView.ShowAutoFilterRow = True
            .OptionsView.ShowGroupPanel = True
            .OptionsBehavior.Editable = False
            .OptionsSelection.MultiSelect = True
            .OptionsSelection.EnableAppearanceFocusedCell = False
            .OptionsDetail.EnableMasterViewMode = False
            ' Clear existing columns if any
            .Columns.Clear()
            ' Add columns with proper mapping
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "TrtID",
                    .Caption = If(Eng, "ID", "المعرف"),
                    .Visible = False,
                    .VisibleIndex = 0,
                    .Width = 25})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "TrtDate",
                    .Caption = If(Eng, "Treat Date", "تاريخ العلاج"),
                    .Visible = True,
                    .VisibleIndex = 1,
                    .Width = 50})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "TrtValue",
                    .Caption = If(Eng, "Treat Value ", "قيمة العلاج"),
                    .Visible = True,
                    .VisibleIndex = 2,
                    .Width = 30})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "Discount",
                    .Caption = If(Eng, "Discount", "الخصم"),
                    .Visible = True,
                    .VisibleIndex = 3,
                    .Width = 30})
            .Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                    .FieldName = "TreatmentName",
                    .Caption = If(Eng, "Treat", "العلاج"),
                    .Visible = True,
                    .VisibleIndex = 4}) ',
            '.Width = 100

        End With

        If gridPatientPayments IsNot Nothing Then IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(gridPatientPayments)
        If gridPatientTreatments IsNot Nothing Then IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(gridPatientTreatments)

    End Sub
    Private Sub GridViewPatients_DoubleClick(sender As Object, e As EventArgs)
        Dim view As GridView = CType(sender, GridView)
        If view.FocusedRowHandle >= 0 Then
            Dim patient As Patient = CType(view.GetRow(view.FocusedRowHandle), Patient)
            If patient IsNot Nothing Then
                ' Open patient details form
                ShowPatientDetails(patient.PatientID)
            End If
        End If
    End Sub
    Private Sub ShowPatientDetails(patientID As Integer)
        ' Implement patient details form opening
        Dim patientForm As New PatientInfoForm(patientID)
        patientForm.ShowDialog()
    End Sub
    ' ============ GRID CONFIGURATIONS ============
    Private Sub PatientsGrid_RowStyle(sender As Object, e As RowStyleEventArgs)
        Dim view As GridView = TryCast(sender, GridView)
        If view Is Nothing Then Return
        Dim patient As Patient = TryCast(view.GetRow(e.RowHandle), Patient)
        If patient Is Nothing Then Return
        ' Highlight children
        If patient.Age.HasValue AndAlso patient.Age.Value < 11 Then
            e.Appearance.BackColor = Color.FromArgb(255, 255, 200) ' Light yellow
        End If
        ' Highlight patients needing Diag
        If patient.Diag.HasValue AndAlso patient.Diag.Value Then
            e.Appearance.BackColor = Color.FromArgb(255, 200, 200) ' Light red
        End If
        ' Highlight patients needing Implant
        If patient.Implant.HasValue AndAlso patient.Implant.Value Then
            e.Appearance.BackColor = Color.LightGreen ' Light Green
        End If
    End Sub
    Private Sub GridViewPatients_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Dim view As GridView = TryCast(sender, GridView)
        If view Is Nothing Then Return
        Dim patient As Patient = TryCast(view.GetRow(e.FocusedRowHandle), Patient)
        If patient IsNot Nothing Then
            LoadPatientDetails(patient.PatientID)
        End If
    End Sub

#End Region



    ' ============ FILTER CONTROLS ============
#Region "FILTER CONTROLS"

    Private Sub UpdateCurrentFilter()
        _currentFilter.DateFrom = dateEditFrom.DateTime
        _currentFilter.DateTo = DateEditTo.DateTime

        'Dim drId = comboDoctor.DrID
        '_currentFilter.DoctorID = If(drId > 0, drId, Nothing)

        '_currentFilter.Status =
        'If(comboStatus.SelectedIndex > 0, comboStatus.Text, Nothing)

        _currentFilter.ShowOnlyUnpaid = CheckEditUnpaid.Checked
    End Sub
    Private Sub ApplyFilters(sender As Object, e As EventArgs)
        UpdateCurrentFilter()
        ConfigurePatientGrid()
        'LoadDashboardDataAsync()
        LoadPatientsAsync()
    End Sub
    Private Sub InitializeFilters()
        ' Date Range
        _currentFilter.DateFrom = dateEditFrom.EditValue
        _currentFilter.DateTo = DateEditTo.EditValue
        _currentFilter.ShowOnlyUnpaid = CheckEditUnpaid.Checked
        ' Doctor ComboBox   
        '_DoctorID = comboDoctor.DrID
        '_currentFilter.DoctorID = _DoctorID
        '' Status ComboBox
        'comboStatus.Properties.Items.AddRange(New String() {
        '   "All", "Pending", "Completed", "PostPoned", "Cancelled", "No Show"})
        'comboStatus.SelectedIndex = 0
        '_currentFilter.Status = comboStatus.Text
        ' Wire up events
        AddHandler SimpleButtonApply.Click, AddressOf ApplyFilters
        AddHandler SimpleButtonReset.Click, AddressOf ResetFilters
        AddHandler dateEditFrom.EditValueChanged, AddressOf DateFilterChanged
        AddHandler DateEditTo.EditValueChanged, AddressOf DateFilterChanged
    End Sub
    Private Sub DateFilterChanged(sender As Object, e As EventArgs)
        Dim dateEdit As DateEdit = CType(sender, DateEdit)
        If dateEdit Is dateEditFrom Then
            ' From date changed
            If dateEditFrom.DateTime > DateEditTo.DateTime Then
                DateEditTo.EditValue = dateEditFrom.DateTime
            End If
        ElseIf dateEdit Is DateEditTo Then
            ' To date changed
            If DateEditTo.DateTime < dateEditFrom.DateTime Then
                dateEditFrom.EditValue = DateEditTo.DateTime
            End If
        End If
        ' Optional: Auto-apply filters
        ' ApplyFilters(sender, e)
    End Sub


    Private Sub ResetFilters(sender As Object, e As EventArgs)
        dateEditFrom.EditValue = Date.Today.AddDays(-30)
        DateEditTo.EditValue = Date.Today
        'comboDoctor.DrID = -1
        'comboStatus.SelectedIndex = 0
        CheckEditUnpaid.Checked = False
        ApplyFilters(sender, e)
    End Sub


#End Region


    ' ============ DATA LOADING ============
#Region "DATA LOADING "
    Private Async Sub LoadDashboardDataAsync()
        If _loading Then Return

        _loading = True
        UpdateStatus(If(Eng, "Loading dashboard data...", "جارٍ تحميل بيانات اللوحة..."))

        Try
            Await LoadAllDashboardDataAsync()

            BindKPIData()
            BindPatientGrid()
            BindAppointmentGrid()
            BindFinancialGrid()
            BindCharts()

            UpdateStatus(If(Eng, "Ready", "جاهز"))
        Catch ex As Exception
            XtraMessageBox.Show($"Error loading data: {ex.Message}", If(Eng, "Error", "خطأ"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateStatus(If(Eng, "Error loading data", "خطأ في تحميل البيانات"))
        Finally
            _loading = False
        End Try
    End Sub

    Private Async Function LoadAllDashboardDataAsync() As Task
        Await Task.WhenAll(
        Task.Run(AddressOf LoadKPIData),
        Task.Run(AddressOf LoadPatientData),
        Task.Run(AddressOf LoadAppointmentData),
        Task.Run(AddressOf LoadFinancialData),
        Task.Run(AddressOf LoadChartData)
    )
    End Function
    'load patient data
    Private Async Sub LoadPatientsAsync()
        If _loading Then Return

        _loading = True
        UpdateStatus(If(Eng, "Loading Patients data...", "جارٍ تحميل بيانات المرضى..."))

        Try
            Await LoadAllPatientAsync()

            BindPatientGrid()


            UpdateStatus(If(Eng, "Ready", "جاهز"))
        Catch ex As Exception
            XtraMessageBox.Show($"Error loading data: {ex.Message}", If(Eng, "Error", "خطأ"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateStatus(If(Eng, "Error loading data", "خطأ في تحميل البيانات"))
        Finally
            _loading = False
        End Try
    End Sub

    Private Async Function LoadAllPatientAsync() As Task
        Await Task.WhenAll(
        Task.Run(AddressOf LoadPatientData)
    )
    End Function
    Private Sub LoadKPIData()
        Dim kpis = _dbHelper.GetDashboardKPIs(_currentFilter)
        ' Store in form-level variable for binding
        _dashboardKPI = kpis
    End Sub
    ' ============ KPI TILES ============
    Private Sub CreateKPITiles()
        ' Make sure tileContainer is initialized
        If tileContainer Is Nothing Then
            tileContainer = New DevExpress.XtraEditors.TileControl()
            tileContainer.Dock = DockStyle.Top
            tileContainer.Height = 180
        End If

        ' Create a tile group to hold the tiles
        Dim tileGroup As New DevExpress.XtraEditors.TileGroup()
        tileGroup.Name = "groupKPIs"

        ' Tile 1: Total Patients
        'Dim tilePatients As New DevExpress.XtraEditors.TileItem()
        tilePatients.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide
        tilePatients.Text = If(Eng, "TOTAL PATIENTS", "إجمالي المرضى")
        tilePatients.AppearanceItem.Normal.FontSizeDelta = 2
        tilePatients.AppearanceItem.Normal.ForeColor = Color.White
        tilePatients.AppearanceItem.Normal.BackColor = Color.FromArgb(42, 157, 143)

        ' Set tile properties correctly
        tilePatients.TextAlignment = TileItemContentAlignment.MiddleCenter

        ' Tile 2: Today's Appointments
        'Dim tileAppointments As New DevExpress.XtraEditors.TileItem()
        tileAppointments.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium
        tileAppointments.Text = If(Eng, "TODAY'S APPOINTMENTS", "مواعيد اليوم")
        tileAppointments.AppearanceItem.Normal.BackColor = Color.FromArgb(38, 70, 83)
        tileAppointments.AppearanceItem.Normal.ForeColor = Color.White
        tileAppointments.TextAlignment = TileItemContentAlignment.MiddleCenter

        ' Add more tiles if needed...
        ' Dim tileRevenue As New DevExpress.XtraEditors.TileItem()...
        ' Dim tileOutstanding As New DevExpress.XtraEditors.TileItem()...

        '' Add tiles to the group
        'tileGroup.Items.Add(tilePatients)
        'tileGroup.Items.Add(tileAppointments)

        '' Add the group to tileContainer
        'tileContainer.Groups.Add(tileGroup)

        ' Alternative method: Add items directly to groups collection
        ' tileContainer.Groups(0).Items.AddRange(New DevExpress.XtraEditors.TileItem() {
        '     tilePatients, tileAppointments
        ' })
    End Sub
    Private Sub LoadPatientData()
        _patients = _dbHelper.GetPatients(_currentFilter)
    End Sub
    Private Sub LoadAppointmentData()
        _appointments = _dbHelper.GetAppointments(_currentFilter)
    End Sub
    Private Sub LoadFinancialData()
        _treatments = _dbHelper.GetTreatments(_currentFilter)
        _payments = _dbHelper.GetPayments(_currentFilter)
        _TreatmentStatistics = _dbHelper.GetTreatmentStats(_currentFilter)
    End Sub
    Private Sub LoadChartData()
        _revenueTrend = _dbHelper.GetRevenueTrend(30)
        _demographics = _dbHelper.GetPatientDemographics()
    End Sub


#End Region

    'Binding
#Region "Binding"
    Private Sub BindKPIData()
        If _dashboardKPI IsNot Nothing Then
            tilePatients.Text = $"{If(Eng, "TOTAL PATIENTS", "إجمالي المرضى")}{vbCrLf}{_dashboardKPI.TotalPatients:N0}"
            tileAppointments.Text = $"{If(Eng, "TODAY'S APPOINTMENTS", "مواعيد اليوم")}{vbCrLf}{_dashboardKPI.TodayAppointments:N0}"
            tileRevenue.Text = $"{If(Eng, "MONTHLY REVENUE", "الإيراد الشهري")}{vbCrLf}{_dashboardKPI.MonthlyRevenue:C0}"
            tileThisMonth.Text = $"{If(Eng, "THIS MONTH REVENUE", "إيراد هذا الشهر")}{vbCrLf}{_dashboardKPI.ThisMonthRevenue:C0}"
            tileOutstanding.Text = $"{If(Eng, "OUTSTANDING", "المستحق")}{vbCrLf}{_dashboardKPI.OutstandingBalance:C0}"
            tileActiveTreatments.Text = $"{If(Eng, "Active Treatments", "العلاجات النشطة")}{vbCrLf}{_dashboardKPI.ActiveTreatments:N0}"
            tileNewPatientsThisMonth.Text = $"{If(Eng, "New Patients This Month", "مرضى جدد هذا الشهر")}{vbCrLf}{_dashboardKPI.NewPatientsThisMonth:N0}"
            tileCollectionRate.Text = $"{If(Eng, "Collection Rate", "معدل التحصيل")}{vbCrLf}{_dashboardKPI.CollectionRate:P1}"
        End If
    End Sub

    Private Sub BindPatientGrid()
        gridControlPatients.DataSource = _patients
    End Sub
    Private Sub BindAppointmentGrid()
        ' Implementation depends on how you're displaying appointments

        If _apptPageCreated Then
            _apptPage.LoadAppointments(_currentFilter)

        End If
    End Sub
    Private Sub BindFinancialGrid()
        ' Implementation for financial data
    End Sub

    Private Sub BindCharts()
        BindRevenueChart()
        BindDemographicChartPie3D()
    End Sub
#End Region
    ' ============ EXPORT FUNCTIONALITY ============
    Private Sub ExportToExcel()
        If gridControlPatients.Visible Then
            Using saveDialog As New SaveFileDialog()
                saveDialog.Filter = "Excel Files|*.xlsx"
                saveDialog.FileName = $"Patients_Export_{Date.Now:yyyyMMdd}.xlsx"
                If saveDialog.ShowDialog() = DialogResult.OK Then
                    gridViewPatients.ExportToXlsx(saveDialog.FileName)
                    XtraMessageBox.Show("Export completed successfully!", "Export",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        End If
    End Sub
    ' ============ REAL-TIME UPDATES ============
    Private timerRefresh As New Timer()
    Private Sub InitializeAutoRefresh()
        timerRefresh.Interval = 500000 ' 5 minutes
        AddHandler timerRefresh.Tick, AddressOf TimerRefresh_Tick
        timerRefresh.Start()
    End Sub
    Private Sub TimerRefresh_Tick(sender As Object, e As EventArgs)
        If Not _loading Then LoadDashboardDataAsync()
    End Sub
    ' ============ ALERT SYSTEM ============
    Private Sub CheckAlerts()
        Dim alerts = _dbHelper.GetDashboardAlerts()

        If alerts.Any() Then
            ShowToastNotification("Alerts", String.Join(vbCrLf, alerts))
        End If
    End Sub

    Private Sub ShowToastNotification(title As String, message As String)
        ' Using DevExpress Toast Notification
        Dim toast As New DevExpress.XtraBars.ToastNotifications.ToastNotification(
    Guid.NewGuid(),
     Nothing,
            title,
            message,
            Nothing,
            DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.Text01)

        ToastNotifications.ShowNotification(toast)
    End Sub
    ' ============ STATUS BAR UPDATES ============
    Private Sub UpdateStatus(message As String)
        If StatusStrip1.InvokeRequired Then
            StatusStrip1.Invoke(Sub() statusLabel.Text = message)
        Else
            statusLabel.Text = message
        End If
    End Sub
    ' Similar methods for other pages (Appointments, Financial, Treatments, Analytics)
    ' ============ EVENT HANDLERS ============
    'Private Sub LoadPatientDetails(patientID As Integer)
    ' Load detailed patient information
    ' This could include treatment history, payments, etc.
    Private Sub LoadPatientDetails(patientID As Integer)

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            ' ---------- Treatments ----------
            Dim sqlTrt As String = "
        SELECT
            pt.TrtID,
            pt.TrtDate,
            pt.TrtValue,
            pt.Discount,
            CASE
                WHEN pt.ToothTrtID IS NOT NULL THEN t.Treat
                WHEN pt.OrthoID IS NOT NULL THEN o.Khota
                WHEN pt.OtherTrtID IS NOT NULL THEN ot.Trt
                ELSE 'Unknown'
            END AS TreatmentName
        FROM Patient_Trts pt
        INNER JOIN Patient p ON pt.PatientID = p.PatientID
        LEFT JOIN Patient_ToothTrt t ON pt.ToothTrtID = t.ToothTrtID
        LEFT JOIN OrthoInf o ON pt.OrthoID = o.OrthoID
        LEFT JOIN Patient_OtherTrt ot ON pt.OtherTrtID = ot.OtherTrtID
        WHERE pt.PatientID = @PatientID
        ORDER BY pt.TrtDate DESC"

            Using da As New SqlDataAdapter(sqlTrt, conn)
                da.SelectCommand.Parameters.AddWithValue("@PatientID", patientID)
                Dim dtTrt As New DataTable()
                da.Fill(dtTrt)

                gridPatientTreatments.DataSource = dtTrt
            End Using

            ' ---------- Payments ----------
            Dim sqlPay As String = "
        SELECT
            PayID,
            PayDate,
            PayValue,
            PayType,
            Notes,
            ReceivedBy
        FROM Patient_Pays
        WHERE PatientID = @PatientID
        ORDER BY PayDate DESC"

            Using da As New SqlDataAdapter(sqlPay, conn)
                da.SelectCommand.Parameters.AddWithValue("@PatientID", patientID)
                Dim dtPay As New DataTable()
                da.Fill(dtPay)

                gridPatientPayments.DataSource = dtPay
            End Using

        End Using

    End Sub

    Private Sub BarButtonExport_ItemClick(sender As Object, e As ItemClickEventArgs)
        ExportToExcel()
    End Sub
    Private Sub BarButtonPrint_ItemClick(sender As Object, e As ItemClickEventArgs)
        PrintCurrentView()
    End Sub
    Private Sub BarButtonRefresh_ItemClick(sender As Object, e As ItemClickEventArgs)
        LoadDashboardDataAsync()
    End Sub
    ' Print
    Private Sub PrintCurrentView()
        If gridControlPatients.Visible Then
            gridControlPatients.ShowPrintPreview()
        End If
    End Sub
    ' ============ CLEANUP ============
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        timerRefresh.Stop()
        timerRefresh.Dispose()
        MyBase.OnFormClosing(e)
    End Sub
    ' ============ FORM PAGES CREATION ============
    Private Sub itemOverview_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles itemOverview.LinkClicked
        ' Navigate to the appropriate page based on clicked item
        NavigationFrame1.SelectedPage = pageOverview
    End Sub
    Private Sub itemPatientList_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles itemPatientList.LinkClicked

        ' Navigate to the appropriate page based on clicked item
        NavigationFrame1.SelectedPage = pagePatients
    End Sub

    Private _apptPageCreated As Boolean = False
    Private Sub itemShowAppt_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles itemShowAppt.LinkClicked
        UpdateCurrentFilter()

        If _apptPage Is Nothing OrElse _apptPage.IsDisposed Then
            _apptPage = New ApptPage(_currentFilter)
            pageAppointments.Controls.Add(_apptPage)
            _apptPageCreated = True
        End If

        _apptPage.BringToFront()
        NavigationFrame1.SelectedPage = pageAppointments
    End Sub
    Private Sub itemShowFinancial_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles itemShowFinancial.LinkClicked

        UpdateCurrentFilter()

        If _finPage Is Nothing OrElse _finPage.IsDisposed Then
            _finPage = New FinancialPage(_currentFilter)
            pageFinancial.Controls.Add(_finPage)
        End If

        _finPage.BringToFront()
        NavigationFrame1.SelectedPage = pageFinancial
    End Sub
    Private Sub itemShowTreats_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles itemShowTreats.LinkClicked

        UpdateCurrentFilter()

        If _trtPage Is Nothing OrElse _trtPage.IsDisposed Then
            _trtPage = New TreatmentsPage(_currentFilter)
            pageTreatments.Controls.Add(_trtPage)
        End If

        _trtPage.BringToFront()
        NavigationFrame1.SelectedPage = pageTreatments
    End Sub
    Private Sub itemShowAnalytics_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles itemShowAnalytics.LinkClicked
        UpdateCurrentFilter()

        If _anlPage Is Nothing OrElse _anlPage.IsDisposed Then
            _anlPage = New AnalyticsPage(_currentFilter)
            pageAnalytics.Controls.Add(_anlPage)
        End If

        _anlPage.BringToFront()
        NavigationFrame1.SelectedPage = pageAnalytics
    End Sub

    Private Sub itemShowTreats2_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles itemShowTreats2.LinkClicked
        UpdateCurrentFilter()

        If _trtPage2 Is Nothing OrElse _trtPage2.IsDisposed Then
            _trtPage2 = New TreatmentsPage2(_currentFilter)
            pageTreatments2.Controls.Add(_trtPage2)
        End If

        _trtPage2.BringToFront()
        NavigationFrame1.SelectedPage = pageTreatments2
    End Sub

    Private Sub SimpleButtonApply_Click(sender As Object, e As EventArgs) Handles SimpleButtonApply.Click

    End Sub
End Class

'Private Sub ApplyTheme()
'    ' Apply DevExpress theme
'    DevExpress.Skins.SkinManager.EnableFormSkins()
'    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful")
'End Sub
'Private Sub BindDemographicChart3()
'    If _demographics Is Nothing OrElse _demographics.Count = 0 Then Return

'    demographicChart.Series.Clear()

'    ' Create donut series (better for showing both count and percentage)
'    Dim series As New Series("Patient Demographics", ViewType.Doughnut3D)
'    series.DataSource = _demographics
'    series.ArgumentDataMember = "Category"
'    series.ValueDataMembers.AddRange(New String() {"Count"})

'    Dim doughnutView As Doughnut3DSeriesView = CType(series.View, Doughnut3DSeriesView)

'    ' Show labels with both count and percentage
'    doughnutView.Titles.Add(New SeriesTitle() With {
'        .Text = "{A}" & vbCrLf & "{V} ({VP:##.##}%)",
'        .Font = New Font("Tahoma", 8, FontStyle.Regular),
'        .Indent = 5
'    })

'    ' Configure hole size
'    doughnutView.HoleRadiusPercent = 40

'    ' Configure legend
'    demographicChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
'    demographicChart.Legend.Title.Text = "{A}: {V} patients"

'    demographicChart.Series.Add(series)

'    ' Add chart title
'    demographicChart.Titles.Clear()
'    demographicChart.Titles.Add(New ChartTitle() With {
'        .Text = "Patient Age Distribution",
'        .Font = New Font("Tahoma", 12, FontStyle.Bold)
'    })
'End Sub
