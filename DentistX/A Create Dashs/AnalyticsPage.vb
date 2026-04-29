Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.Design
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraPivotGrid

Public Class AnalyticsPage

    Private ReadOnly _dbHelper As New DashDataModel.DatabaseHelper()
    Private ReadOnly _connectionString As String = DashDataModel.DatabaseHelper._connectionString
    Private _currentFilter As New DashboardFilter()

    Private pivotGrid As PivotGridControl
    Private chartDoctorPerformance As ChartControl
    Private DoctorPerformanceBarChart As ChartControl
    Private pnl As SidePanel
    Private chartPatientRetention As ChartControl
    Private chartTreatmentSuccess As ChartControl
    Private dateEditFrom As DateEdit
    Private dateEditTo As DateEdit
    Private simpleButtonApply As SimpleButton
    Private simpleButtonExport As SimpleButton

    Public Sub New(_Filter As DashboardFilter)
        InitializeComponent()
        InitializeComponent1()

        _currentFilter = _Filter

        SetupAnalyticsDashboard()
        LoadAnalyticsData()
    End Sub

    Private Sub InitializeComponent1()
        Me.Dock = DockStyle.Fill
        Me.Padding = New Padding(10)
        Me.BackColor = Color.White
    End Sub
    Private Sub SetupAnalyticsDashboard()
        ' Create main layout control
        Dim layoutControl As New DevExpress.XtraLayout.LayoutControl()
        layoutControl.Dock = DockStyle.Fill
        Me.Controls.Add(layoutControl)
        ' Create filter controls
        CreateFilterControls(layoutControl)
        ' Create tab control for different analytics views
        Dim tabControl As New DevExpress.XtraTab.XtraTabControl()
        tabControl.Dock = DockStyle.Fill
        tabControl.Name = "tabControlAnalytics"
        ' Tab 1: Pivot Analysis
        Dim tabPivot As New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Pivot Analysis", "تحليل محوري")}
        pivotGrid = New PivotGridControl()
        pivotGrid.Dock = DockStyle.Fill
        tabPivot.Controls.Add(pivotGrid)
        tabControl.TabPages.Add(tabPivot)
        ' Tab 2: Performance Analytics
        Dim tabPerformance As New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Performance", "الأداء")}
        Dim splitPerformance As New SplitContainer()
        splitPerformance.Dock = DockStyle.Fill
        splitPerformance.Orientation = Orientation.Vertical
        chartDoctorPerformance = New ChartControl()
        chartDoctorPerformance.Dock = DockStyle.Fill

        splitPerformance.Panel1.Controls.Add(chartDoctorPerformance)

        chartTreatmentSuccess = New ChartControl()
        chartTreatmentSuccess.Dock = DockStyle.Right
        pnl = New SidePanel()
        pnl.Dock = DockStyle.Fill
        DoctorPerformanceBarChart = New ChartControl()
        DoctorPerformanceBarChart.Dock = DockStyle.Fill
        DoctorPerformanceBarChart.BringToFront()
        pnl.Controls.Add(DoctorPerformanceBarChart)
        splitPerformance.Panel2.Controls.Add(chartTreatmentSuccess)
        splitPerformance.Panel2.Controls.Add(pnl)


        tabPerformance.Controls.Add(splitPerformance)
        tabControl.TabPages.Add(tabPerformance)
        ' Tab 3: Patient Analytics
        Dim tabPatient As New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Patient Analytics", "تحليلات المرضى")}
        chartPatientRetention = New ChartControl()
        chartPatientRetention.Dock = DockStyle.Fill
        tabPatient.Controls.Add(chartPatientRetention)
        tabControl.TabPages.Add(tabPatient)
        ' Add tab control to layout
        layoutControl.AddControl(tabControl)
        ' Configure layouts
        Dim layoutGroupFilters = layoutControl.Root.AddGroup()
        layoutGroupFilters.Text = If(Eng, "Filters", "عوامل التصفية")
        ' Create LayoutControlItems directly in AddItem
        layoutGroupFilters.AddItem(New DevExpress.XtraLayout.LayoutControlItem() With {
                                    .Control = dateEditFrom, .Text = If(Eng, "From Date:", "من تاريخ:"), .TextVisible = True})
        layoutGroupFilters.AddItem(New DevExpress.XtraLayout.LayoutControlItem() With {
                                    .Control = dateEditTo, .Text = If(Eng, "To Date:", "إلى تاريخ:"), .TextVisible = True})
        layoutGroupFilters.AddItem(New DevExpress.XtraLayout.LayoutControlItem() With {
                                    .Control = simpleButtonApply, .TextVisible = False})
        layoutGroupFilters.AddItem(New DevExpress.XtraLayout.LayoutControlItem() With {
                                    .Control = simpleButtonExport, .TextVisible = False})
        Dim layoutGroupAnalytics = layoutControl.Root.AddGroup()
        layoutGroupAnalytics.Text = If(Eng, "Analytics", "تحليلات")
        layoutGroupAnalytics.AddItem(New DevExpress.XtraLayout.LayoutControlItem() With {
    .Control = tabControl,
    .TextVisible = False})
        ' Configure controls
        ConfigurePivotGrid()
        ConfigureCharts()
        ' Wire up events
        AddHandler simpleButtonApply.Click, AddressOf ApplyAnalyticsFilters
        AddHandler simpleButtonExport.Click, AddressOf ExportAnalyticsData
        AddHandler dateEditFrom.EditValueChanged, AddressOf DateFilterChanged
        AddHandler dateEditTo.EditValueChanged, AddressOf DateFilterChanged
    End Sub

    Private Sub CreateFilterControls(layoutControl As DevExpress.XtraLayout.LayoutControl)
        ' Date range controls
        dateEditFrom = New DateEdit() With {.EditValue = Date.Today.AddMonths(-6)}
        dateEditTo = New DateEdit() With {.EditValue = Date.Today}
        ' Buttons
        simpleButtonApply = New SimpleButton() With {.Text = If(Eng, "Apply Filters", "تطبيق التصفية")}
        simpleButtonExport = New SimpleButton() With {.Text = If(Eng, "Export Data", "تصدير البيانات")}
    End Sub
    Private Sub ConfigurePivotGrid()
        With pivotGrid
            .OptionsData.DataProcessingEngine = PivotDataProcessingEngine.Optimized
            .OptionsView.ShowColumnGrandTotals = True
            .OptionsView.ShowRowGrandTotals = True
            .OptionsView.ShowColumnTotals = True
            .OptionsView.ShowRowTotals = True
            ' Add Doctor field
            Dim fieldDoctor As New PivotGridField() With {
                .FieldName = "DrName",
                .Area = PivotArea.RowArea,
                .Caption = If(Eng, "Doctor", "الطبيب"),
                .AreaIndex = 0
            }
            .Fields.Add(fieldDoctor)
            ' Add Month field
            Dim fieldMonth As New PivotGridField() With {
                .FieldName = "Month",
                .Area = PivotArea.ColumnArea,
                .Caption = If(Eng, "Month", "الشهر"),
                .AreaIndex = 0
            }
            .Fields.Add(fieldMonth)
            ' Add Revenue field
            Dim fieldRevenue As New PivotGridField() With {
                .FieldName = "Revenue",
                .Area = PivotArea.DataArea,
                .Caption = If(Eng, "Revenue", "الإيراد"),
                .AreaIndex = 0,
                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum
            }
            .Fields.Add(fieldRevenue)
            ' Add Patient Count field
            Dim fieldPatients As New PivotGridField() With {
                .FieldName = "PatientCount",
                .Area = PivotArea.DataArea,
                .Caption = If(Eng, "Patients", "المرضى"),
                .AreaIndex = 1,
                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum
            }
            .Fields.Add(fieldPatients)
            ' Add Average Revenue per Patient field
            Dim fieldAvgRevenue As New PivotGridField() With {
                .FieldName = "AvgRevenuePerPatient",
                .Area = PivotArea.DataArea,
                .Caption = If(Eng, "Avg Rev/Patient", "متوسط الإيراد/مريض"),
                .AreaIndex = 2,
                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Average
            }
            .Fields.Add(fieldAvgRevenue)
        End With
    End Sub

    Private Sub ConfigureDoctorPerformanceBarChart()
        DoctorPerformanceBarChart.Series.Clear()
        DoctorPerformanceBarChart.Titles.Clear()

        DoctorPerformanceBarChart.Titles.Add(New ChartTitle() With {
        .Text = If(Eng, "Doctor Performance Comparison", "مقارنة أداء الأطباء"),
        .Font = New Font("Tahoma", 12, FontStyle.Bold)
    })

        DoctorPerformanceBarChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
        DoctorPerformanceBarChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right
        DoctorPerformanceBarChart.Legend.AlignmentVertical = LegendAlignmentVertical.Top
    End Sub


    Private Sub ConfigureCharts()
        ' Configure Doctor Performance Chart (Radar)
        Dim seriesDoctor As New Series("Doctor Performance", ViewType.RadarPoint)
        seriesDoctor.ArgumentDataMember = "Metric"
        seriesDoctor.ValueDataMembers.AddRange(New String() {"Value"})
        chartDoctorPerformance.Series.Add(seriesDoctor)
        Dim viewDoctor As RadarPointSeriesView = CType(seriesDoctor.View, RadarPointSeriesView)
        chartDoctorPerformance.Titles.Add(New ChartTitle() With {
            .Text = If(Eng, "Doctor Performance Comparison", "مقارنة أداء الأطباء"),
            .Font = New Font("Tahoma", 12, FontStyle.Bold),
            .TextColor = Color.FromArgb(64, 64, 64)
        })
        chartDoctorPerformance.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
        chartDoctorPerformance.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right

        ' Configure Doctor Performance Chart (Bar)
        Dim seriesDoctorBar As New Series("Doctor Performance", ViewType.Bar)
        seriesDoctorBar.ArgumentDataMember = "Metric"
        seriesDoctorBar.ValueDataMembers.AddRange(New String() {"Value"})
        DoctorPerformanceBarChart.Series.Add(seriesDoctorBar)
        Dim viewDoctorBar As SideBySideBarSeriesView = CType(seriesDoctorBar.View, SideBySideBarSeriesView)
        DoctorPerformanceBarChart.Titles.Add(New ChartTitle() With {
            .Text = If(Eng, "Doctor Performance Comparison", "مقارنة أداء الأطباء"),
            .Font = New Font("Tahoma", 12, FontStyle.Bold),
            .TextColor = Color.FromArgb(64, 64, 64)
        })
        DoctorPerformanceBarChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
        DoctorPerformanceBarChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right

        ' Configure Treatment Success Chart (Pie)
        Dim seriesSuccess As New Series("Treatment Success", ViewType.Pie3D)
        seriesSuccess.ArgumentDataMember = "Status"
        seriesSuccess.ValueDataMembers.AddRange(New String() {"Count"})
        chartTreatmentSuccess.Series.Add(seriesSuccess)
        Dim viewSuccess As Pie3DSeriesView = CType(seriesSuccess.View, Pie3DSeriesView)
        viewSuccess.Titles.Add(New SeriesTitle() With {.Text = "{A}: {V}"})
        chartTreatmentSuccess.Titles.Add(New ChartTitle() With {
            .Text = If(Eng, "Treatment Success Rate", "معدل نجاح العلاج"),
            .Font = New Font("Tahoma", 12, FontStyle.Bold),
            .TextColor = Color.FromArgb(64, 64, 64)
        })
        ' Configure Patient Retention Chart (Line)
        Dim seriesRetention As New Series("Retention Rate", ViewType.Line)
        seriesRetention.ArgumentDataMember = "Period"
        seriesRetention.ValueDataMembers.AddRange(New String() {"RetentionRate"})
        chartPatientRetention.Series.Add(seriesRetention)
        Dim viewRetention As LineSeriesView = CType(seriesRetention.View, LineSeriesView)
        viewRetention.LineMarkerOptions.Kind = MarkerKind.Circle
        viewRetention.LineStyle.DashStyle = DashStyle.Solid
        viewRetention.Color = Color.FromArgb(42, 157, 143)
        chartPatientRetention.Titles.Add(New ChartTitle() With {
            .Text = If(Eng, "Patient Retention Rate (Last 12 Months)", "معدل الاحتفاظ بالمرضى (آخر 12 شهرًا)"),
            .Font = New Font("Tahoma", 12, FontStyle.Bold),
            .TextColor = Color.FromArgb(64, 64, 64)
        })
        ' Configure Y-axis for percentage
        Dim axisY As New SecondaryAxisY(If(Eng, "Retention Rate %", "معدل الاحتفاظ %"))
        axisY.Title.Text = If(Eng, "Retention Rate %", "معدل الاحتفاظ %")
        axisY.Title.Visibility = True
        chartPatientRetention.Diagram.Assign(axisY)
    End Sub
    Private Sub LoadAnalyticsData()
        Try
            Cursor.Current = Cursors.WaitCursor
            ' Load pivot grid data
            LoadPivotData()
            ' Load doctor performance data
            LoadDoctorPerformanceData()
            ' Load treatment success data
            LoadTreatmentSuccessData()
            ' Load patient retention data
            LoadPatientRetentionData()
        Catch ex As Exception
            XtraMessageBox.Show($"Error loading analytics data: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Sub LoadPivotData()
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql = "
                    SELECT 
                            ISNULL(d.DrName, 'Unknown') as DrName,
                            DATEFROMPARTS(YEAR(a.AppDate), MONTH(a.AppDate), 1) as Month,
                            COUNT(DISTINCT a.PatientID) as PatientCount,
                            ISNULL(SUM(pt.TrtValue), 0) as Revenue,
                            CASE 
                                WHEN COUNT(DISTINCT a.PatientID) > 0 
                                THEN ISNULL(SUM(pt.TrtValue), 0) / COUNT(DISTINCT a.PatientID)
                                ELSE 0 
                            END as AvgRevenuePerPatient
                        FROM AppointmentC a
                        LEFT JOIN Doctors d ON a.DrID = d.DrID
                        LEFT JOIN Patient_Trts pt 
                            ON a.PatientID = pt.PatientID
                            AND MONTH(pt.TrtDate) = MONTH(a.AppDate)
                            AND YEAR(pt.TrtDate) = YEAR(a.AppDate)
                        WHERE a.AppDate >= @DateFrom
                          AND a.AppDate <= @DateTo
                        GROUP BY 
                            ISNULL(d.DrName, 'Unknown'),
                            DATEFROMPARTS(YEAR(a.AppDate), MONTH(a.AppDate), 1)
                        ORDER BY Month, DrName
                        "
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@DateFrom", dateEditFrom.DateTime)
                cmd.Parameters.AddWithValue("@DateTo", dateEditTo.DateTime)
                Using adapter As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    pivotGrid.DataSource = dt
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadDoctorPerformanceData()
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql = "
                    WITH DoctorMetrics AS (
                                            SELECT 
                                                ISNULL(d.DrName, 'Unknown') as DrName,
                                                AVG(CASE WHEN a.Status = 'Completed' THEN 1.0 ELSE 0.0 END) * 100 as CompletionRate,
                                                COUNT(DISTINCT a.PatientID) as UniquePatients,
                                                ISNULL(AVG(DATEDIFF(MINUTE, a.StartDateTime, a.EndDateTime)), 0) as AvgDuration,
                                                COUNT(*) as TotalAppointments,
                                                SUM(CASE WHEN a.Status = 'No Show' THEN 1 ELSE 0 END) as NoShows
                                            FROM AppointmentC a
                                            LEFT JOIN Doctors d ON a.DrID = d.DrID
                                            WHERE a.AppDate >= @DateFrom
                                              AND a.AppDate <= @DateTo
                                            GROUP BY ISNULL(d.DrName, 'Unknown')
                                        )
                                SELECT 
                                    'Completion Rate' as Metric,
                                    DrName as Doctor,
                                    CompletionRate / 100.0 as Value
                                FROM DoctorMetrics
                                UNION ALL
                                SELECT 
                                    'Unique Patients',
                                    DrName,
                                    UniquePatients * 1.0 / NULLIF(MAX(UniquePatients) OVER (), 0)
                                FROM DoctorMetrics
                                UNION ALL
                                SELECT 
                                    'Avg Duration (min)',
                                    DrName,
                                    AvgDuration / NULLIF(MAX(AvgDuration) OVER (), 0)
                                FROM DoctorMetrics
                                UNION ALL
                                SELECT 
                                    'Appointment Count',
                                    DrName,
                                    TotalAppointments * 1.0 / NULLIF(MAX(TotalAppointments) OVER (), 0)
                                FROM DoctorMetrics
                                UNION ALL
                                SELECT 
                                    'No Show Rate',
                                    DrName,
                                    NoShows * 1.0 / NULLIF(TotalAppointments, 0)
                                FROM DoctorMetrics
                                ORDER BY Doctor, Metric
                                "
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@DateFrom", dateEditFrom.DateTime)
                cmd.Parameters.AddWithValue("@DateTo", dateEditTo.DateTime)
                Using adapter As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    ' Clear existing series and add new ones for each doctor
                    chartDoctorPerformance.Series.Clear()
                    Dim doctors As List(Of String) = dt.AsEnumerable() _
                        .Select(Function(row) row.Field(Of String)("Doctor")) _
                        .Distinct() _
                        .ToList()
                    For Each doctor In doctors
                        Dim series As New Series(doctor, ViewType.RadarPoint)
                        series.DataSource = dt
                        series.FilterString = $"[Doctor] = '{doctor}'"
                        series.ArgumentDataMember = "Metric"
                        series.ValueDataMembers.AddRange(New String() {"Value"})
                        Dim view As RadarPointSeriesView = CType(series.View, RadarPointSeriesView)
                        chartDoctorPerformance.Series.Add(series)
                    Next
                    Try
                        ' Prepare chart
                        DoctorPerformanceBarChart.Series.Clear()

                        Dim doctorsBar As List(Of String) =
                            dt.AsEnumerable().
                               Select(Function(r) r.Field(Of String)("Doctor")).
                               Distinct().
                               ToList()

                        Dim diagramConfigured As Boolean = False

                        For Each doctor In doctorsBar
                            Dim series As New Series(doctor, ViewType.Bar)

                            series.DataSource = dt
                            series.FilterString = $"[Doctor] = '{doctor}'"
                            series.ArgumentDataMember = "Metric"
                            series.ValueDataMembers.AddRange("Value")

                            Dim view As SideBySideBarSeriesView =
                                CType(series.View, SideBySideBarSeriesView)

                            view.BarWidth = 0.6
                            view.FillStyle.FillMode = FillMode.Solid

                            DoctorPerformanceBarChart.Series.Add(series)

                            ' 🔴 FIRST SERIES ONLY → configure diagram
                            If Not diagramConfigured Then
                                Dim diagram As XYDiagram =
                                    CType(DoctorPerformanceBarChart.Diagram, XYDiagram)

                                diagram.AxisX.Title.Text = If(Eng, "Performance", "الأداء")
                                diagram.AxisX.Title.Visible = True
                                diagram.AxisX.NumericOptions.Format = NumericFormat.Percent
                                diagram.AxisX.NumericOptions.Precision = 0
                                diagram.AxisX.WholeRange.MinValue = 0
                                diagram.AxisX.WholeRange.MaxValue = 1

                                diagram.AxisY.Title.Text = If(Eng, "Metric", "المؤشر")
                                diagram.AxisY.Title.Visible = True
                                diagram.AxisY.Label.Angle = 0

                                diagramConfigured = True
                            End If
                        Next

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadTreatmentSuccessData()
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql = "
                    SELECT 
                        CASE 
                            WHEN tt.Finished = 2 THEN 'Completed'
                            WHEN tt.Finished = 1 THEN 'In Progress'
                            WHEN tt.Finished = 0 THEN 'Not Started'
                            ELSE 'Unknown'
                        END as Status,
                        COUNT(*) as Count
                    FROM Patient_ToothTrt tt
                    WHERE tt.TreatDate >= @DateFrom
                    AND tt.TreatDate <= @DateTo
                    GROUP BY 
                        CASE 
                            WHEN tt.Finished = 2 THEN 'Completed'
                            WHEN tt.Finished = 1 THEN 'In Progress'
                            WHEN tt.Finished = 0 THEN 'Not Started'
                            ELSE 'Unknown'
                        END"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@DateFrom", dateEditFrom.DateTime)
                cmd.Parameters.AddWithValue("@DateTo", dateEditTo.DateTime)
                Using adapter As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        chartTreatmentSuccess.DataSource = dt
                        chartTreatmentSuccess.Series(0).DataSource = dt
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadPatientRetentionData()
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql = "
                    WITH PatientVisits AS (
                        SELECT 
                            PatientID,
                            YEAR(AppDate) as VisitYear,
                            MONTH(AppDate) as VisitMonth,
                            COUNT(*) as VisitCount
                        FROM AppointmentC
                        WHERE AppDate >= DATEADD(MONTH, -12, GETDATE())
                        AND AppDate <= GETDATE()
                        GROUP BY PatientID, YEAR(AppDate), MONTH(AppDate)
                                        ),
                    MonthlyStats AS (
                        SELECT 
                            FORMAT(DATEFROMPARTS(VisitYear, VisitMonth, 1), 'MMM yyyy') as Period,
                            DATEFROMPARTS(VisitYear, VisitMonth, 1) as PeriodDate,
                            COUNT(DISTINCT PatientID) as ActivePatients,
                            COUNT(DISTINCT CASE WHEN VisitCount > 1 THEN PatientID END) as ReturningPatients
                        FROM PatientVisits
                        GROUP BY VisitYear, VisitMonth
                                    )
                    SELECT 
                        Period,
                        ActivePatients,
                        ReturningPatients,
                        CASE 
                            WHEN ActivePatients > 0 
                            THEN CAST(ReturningPatients AS FLOAT) / ActivePatients
                            ELSE 0 
                        END as RetentionRate
                    FROM MonthlyStats
                    ORDER BY PeriodDate"
            Using cmd As New SqlCommand(sql, conn)
                Using adapter As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        chartPatientRetention.DataSource = dt
                        chartPatientRetention.Series(0).DataSource = dt
                        ' Configure axis
                        Dim diagram As XYDiagram = CType(chartPatientRetention.Diagram, XYDiagram)
                        diagram.AxisX.Title.Text = If(Eng, "Month", "الشهر")
                        diagram.AxisX.Title.Visible = True
                        If diagram.SecondaryAxesY.Count > 0 Then
                            diagram.SecondaryAxesY(0).Visible = True
                            diagram.SecondaryAxesY(0).NumericOptions.Format = NumericFormat.Percent
                            diagram.SecondaryAxesY(0).NumericOptions.Precision = 1
                            diagram.SecondaryAxesY(0).WholeRange.MaxValue = 1
                            diagram.SecondaryAxesY(0).WholeRange.MinValue = 0
                        End If
                    End If
                End Using
            End Using
        End Using
    End Sub
    Private Sub ApplyAnalyticsFilters(sender As Object, e As EventArgs)
        LoadAnalyticsData()
    End Sub
    Private Sub DateFilterChanged(sender As Object, e As EventArgs)
        ' Optional: Add validation or auto-refresh logic
        If dateEditFrom.DateTime > dateEditTo.DateTime Then
            XtraMessageBox.Show("Start date cannot be after end date.",
                              "Validation Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning)
            dateEditFrom.EditValue = dateEditTo.DateTime.AddMonths(-1)
        End If
    End Sub
    Private Sub ExportAnalyticsData(sender As Object, e As EventArgs)
        Try
            Using saveDialog As New SaveFileDialog()
                saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|PDF Files (*.pdf)|*.pdf"
                saveDialog.FileName = $"Analytics_Report_{Date.Now:yyyyMMdd_HHmmss}"
                If saveDialog.ShowDialog() = DialogResult.OK Then
                    Select Case saveDialog.FilterIndex
                        Case 1 ' Excel
                            pivotGrid.ExportToXlsx(saveDialog.FileName)
                        Case 2 ' PDF
                            pivotGrid.ExportToPdf(saveDialog.FileName)
                    End Select
                    XtraMessageBox.Show($"Export completed successfully!{vbCrLf}File: {saveDialog.FileName}",
                                      "Export Complete",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            XtraMessageBox.Show($"Error exporting data: {ex.Message}",
                              "Export Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub RefreshData()
        LoadAnalyticsData()
    End Sub
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            ' Clean up resources
            If pivotGrid IsNot Nothing Then
                pivotGrid.Dispose()
                pivotGrid = Nothing
            End If
            If chartDoctorPerformance IsNot Nothing Then
                chartDoctorPerformance.Dispose()
                chartDoctorPerformance = Nothing
            End If
            If chartPatientRetention IsNot Nothing Then
                chartPatientRetention.Dispose()
                chartPatientRetention = Nothing
            End If
            If chartTreatmentSuccess IsNot Nothing Then
                chartTreatmentSuccess.Dispose()
                chartTreatmentSuccess = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
End Class

'Public Class AnalyticsPage

'    Private ReadOnly _dbHelper As New DashDataModel.DatabaseHelper()
'    Private ReadOnly _connectionString As String = DashDataModel.DatabaseHelper._connectionString
'    Private _currentFilter As New DashboardFilter()

'    Private pivotGrid As PivotGridControl
'    Private chartDoctorPerformance As ChartControl
'    Private chartPatientRetention As ChartControl
'    Private chartTreatmentSuccess As ChartControl
'    Private dateEditFrom As DateEdit
'    Private dateEditTo As DateEdit
'    Private simpleButtonApply As SimpleButton
'    Private simpleButtonExport As SimpleButton

'    Public Sub New(_Filter As DashboardFilter)
'        InitializeComponent()
'        InitializeComponent1()
'        SetupAnalyticsDashboard()
'        LoadAnalyticsData()
'    End Sub
'    Private Sub InitializeComponent1()
'        Me.Dock = DockStyle.Fill
'        Me.Padding = New Padding(10)
'        Me.BackColor = Color.White
'    End Sub
'    Private Sub SetupAnalyticsDashboard()
'        ' Create main layout control
'        Dim layoutControl As New DevExpress.XtraLayout.LayoutControl()
'        layoutControl.Dock = DockStyle.Fill
'        Me.Controls.Add(layoutControl)
'        ' Create filter controls
'        CreateFilterControls(layoutControl)
'        ' Create tab control for different analytics views
'        Dim tabControl As New DevExpress.XtraTab.XtraTabControl()
'        tabControl.Dock = DockStyle.Fill
'        tabControl.Name = "tabControlAnalytics"
'        ' Tab 1: Pivot Analysis
'        Dim tabPivot As New DevExpress.XtraTab.XtraTabPage() With {.Text = "Pivot Analysis"}
'        pivotGrid = New PivotGridControl()
'        pivotGrid.Dock = DockStyle.Fill
'        tabPivot.Controls.Add(pivotGrid)
'        tabControl.TabPages.Add(tabPivot)
'        ' Tab 2: Performance Analytics
'        Dim tabPerformance As New DevExpress.XtraTab.XtraTabPage() With {.Text = "Performance"}
'        Dim splitPerformance As New SplitContainer()
'        splitPerformance.Dock = DockStyle.Fill
'        splitPerformance.Orientation = Orientation.Vertical
'        chartDoctorPerformance = New ChartControl()
'        chartDoctorPerformance.Dock = DockStyle.Fill
'        splitPerformance.Panel1.Controls.Add(chartDoctorPerformance)
'        chartTreatmentSuccess = New ChartControl()
'        chartTreatmentSuccess.Dock = DockStyle.Fill
'        splitPerformance.Panel2.Controls.Add(chartTreatmentSuccess)
'        tabPerformance.Controls.Add(splitPerformance)
'        tabControl.TabPages.Add(tabPerformance)
'        ' Tab 3: Patient Analytics
'        Dim tabPatient As New DevExpress.XtraTab.XtraTabPage() With {.Text = "Patient Analytics"}
'        chartPatientRetention = New ChartControl()
'        chartPatientRetention.Dock = DockStyle.Fill
'        tabPatient.Controls.Add(chartPatientRetention)
'        tabControl.TabPages.Add(tabPatient)
'        ' Add tab control to layout
'        layoutControl.AddControl(tabControl)
'        ' Configure layouts
'        Dim layoutGroupFilters = layoutControl.Root.AddGroup()
'        layoutGroupFilters.Text = "Filters"
'        ' Create LayoutControlItems directly in AddItem
'        layoutGroupFilters.AddItem(New DevExpress.XtraLayout.LayoutControlItem() With {
'                                    .Control = dateEditFrom, .Text = "From Date:", .TextVisible = True})
'        layoutGroupFilters.AddItem(New DevExpress.XtraLayout.LayoutControlItem() With {
'                                    .Control = dateEditTo, .Text = "To Date:", .TextVisible = True})
'        layoutGroupFilters.AddItem(New DevExpress.XtraLayout.LayoutControlItem() With {
'                                    .Control = simpleButtonApply, .TextVisible = False})
'        layoutGroupFilters.AddItem(New DevExpress.XtraLayout.LayoutControlItem() With {
'                                    .Control = simpleButtonExport, .TextVisible = False})
'        Dim layoutGroupAnalytics = layoutControl.Root.AddGroup()
'        layoutGroupAnalytics.Text = "Analytics"
'        layoutGroupAnalytics.AddItem(New DevExpress.XtraLayout.LayoutControlItem() With {
'    .Control = tabControl,
'    .TextVisible = False})
'        ' Configure controls
'        ConfigurePivotGrid()
'        ConfigureCharts()
'        ' Wire up events
'        AddHandler simpleButtonApply.Click, AddressOf ApplyAnalyticsFilters
'        AddHandler simpleButtonExport.Click, AddressOf ExportAnalyticsData
'        AddHandler dateEditFrom.EditValueChanged, AddressOf DateFilterChanged
'        AddHandler dateEditTo.EditValueChanged, AddressOf DateFilterChanged
'    End Sub

'    Private Sub CreateFilterControls(layoutControl As DevExpress.XtraLayout.LayoutControl)
'        ' Date range controls
'        dateEditFrom = New DateEdit() With {.EditValue = Date.Today.AddMonths(-6)}
'        dateEditTo = New DateEdit() With {.EditValue = Date.Today}
'        ' Buttons
'        simpleButtonApply = New SimpleButton() With {.Text = "Apply Filters"}
'        simpleButtonExport = New SimpleButton() With {.Text = "Export Data"}
'    End Sub
'    Private Sub ConfigurePivotGrid()
'        With pivotGrid
'            .OptionsData.DataProcessingEngine = PivotDataProcessingEngine.Optimized
'            .OptionsView.ShowColumnGrandTotals = True
'            .OptionsView.ShowRowGrandTotals = True
'            .OptionsView.ShowColumnTotals = True
'            .OptionsView.ShowRowTotals = True
'            ' Add Doctor field
'            Dim fieldDoctor As New PivotGridField() With {
'                .FieldName = "DrName",
'                .Area = PivotArea.RowArea,
'                .Caption = "Doctor",
'                .AreaIndex = 0
'            }
'            .Fields.Add(fieldDoctor)
'            ' Add Month field
'            Dim fieldMonth As New PivotGridField() With {
'                .FieldName = "Month",
'                .Area = PivotArea.ColumnArea,
'                .Caption = "Month",
'                .AreaIndex = 0
'            }
'            .Fields.Add(fieldMonth)
'            ' Add Revenue field
'            Dim fieldRevenue As New PivotGridField() With {
'                .FieldName = "Revenue",
'                .Area = PivotArea.DataArea,
'                .Caption = "Revenue",
'                .AreaIndex = 0,
'                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum
'            }
'            .Fields.Add(fieldRevenue)
'            ' Add Patient Count field
'            Dim fieldPatients As New PivotGridField() With {
'                .FieldName = "PatientCount",
'                .Area = PivotArea.DataArea,
'                .Caption = "Patients",
'                .AreaIndex = 1,
'                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum
'            }
'            .Fields.Add(fieldPatients)
'            ' Add Average Revenue per Patient field
'            Dim fieldAvgRevenue As New PivotGridField() With {
'                .FieldName = "AvgRevenuePerPatient",
'                .Area = PivotArea.DataArea,
'                .Caption = "Avg Rev/Patient",
'                .AreaIndex = 2,
'                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Average
'            }
'            .Fields.Add(fieldAvgRevenue)
'        End With
'    End Sub
'    Private Sub ConfigureCharts()
'        ' Configure Doctor Performance Chart (Radar)
'        Dim seriesDoctor As New Series("Doctor Performance", ViewType.RadarPoint)
'        seriesDoctor.ArgumentDataMember = "Metric"
'        seriesDoctor.ValueDataMembers.AddRange(New String() {"Value"})
'        chartDoctorPerformance.Series.Add(seriesDoctor)
'        Dim viewDoctor As RadarPointSeriesView = CType(seriesDoctor.View, RadarPointSeriesView)
'        chartDoctorPerformance.Titles.Add(New ChartTitle() With {
'            .Text = "Doctor Performance Comparison",
'            .Font = New Font("Tahoma", 12, FontStyle.Bold),
'            .TextColor = Color.FromArgb(64, 64, 64)
'        })
'        chartDoctorPerformance.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
'        chartDoctorPerformance.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right
'        ' Configure Treatment Success Chart (Pie)
'        Dim seriesSuccess As New Series("Treatment Success", ViewType.Pie3D)
'        seriesSuccess.ArgumentDataMember = "Status"
'        seriesSuccess.ValueDataMembers.AddRange(New String() {"Count"})
'        chartTreatmentSuccess.Series.Add(seriesSuccess)
'        Dim viewSuccess As Pie3DSeriesView = CType(seriesSuccess.View, Pie3DSeriesView)
'        viewSuccess.Titles.Add(New SeriesTitle() With {.Text = "{A}: {V}"})
'        chartTreatmentSuccess.Titles.Add(New ChartTitle() With {
'            .Text = "Treatment Success Rate",
'            .Font = New Font("Tahoma", 12, FontStyle.Bold),
'            .TextColor = Color.FromArgb(64, 64, 64)
'        })
'        ' Configure Patient Retention Chart (Line)
'        Dim seriesRetention As New Series("Retention Rate", ViewType.Line)
'        seriesRetention.ArgumentDataMember = "Period"
'        seriesRetention.ValueDataMembers.AddRange(New String() {"RetentionRate"})
'        chartPatientRetention.Series.Add(seriesRetention)
'        Dim viewRetention As LineSeriesView = CType(seriesRetention.View, LineSeriesView)
'        viewRetention.LineMarkerOptions.Kind = MarkerKind.Circle
'        viewRetention.LineStyle.DashStyle = DashStyle.Solid
'        viewRetention.Color = Color.FromArgb(42, 157, 143)
'        chartPatientRetention.Titles.Add(New ChartTitle() With {
'            .Text = "Patient Retention Rate (Last 12 Months)",
'            .Font = New Font("Tahoma", 12, FontStyle.Bold),
'            .TextColor = Color.FromArgb(64, 64, 64)
'        })
'        ' Configure Y-axis for percentage
'        Dim axisY As New SecondaryAxisY("Retention Rate %")
'        axisY.Title.Text = "Retention Rate %"
'        axisY.Title.Visibility = True
'        chartPatientRetention.Diagram.Assign(axisY)
'    End Sub
'    Private Sub LoadAnalyticsData()
'        Try
'            Cursor.Current = Cursors.WaitCursor
'            ' Load pivot grid data
'            LoadPivotData()
'            ' Load doctor performance data
'            LoadDoctorPerformanceData()
'            ' Load treatment success data
'            LoadTreatmentSuccessData()
'            ' Load patient retention data
'            LoadPatientRetentionData()
'        Catch ex As Exception
'            XtraMessageBox.Show($"Error loading analytics data: {ex.Message}",
'                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        Finally
'            Cursor.Current = Cursors.Default
'        End Try
'    End Sub
'    Private Sub LoadPivotData()
'        Using conn As New SqlConnection(_connectionString)
'            conn.Open()
'            Dim sqlOld = "
'                    SELECT 
'                        d.DrName,
'                        FORMAT(a.AppDate, 'yyyy-MM') as Month,
'                        COUNT(DISTINCT a.PatientID) as PatientCount,
'                        ISNULL(SUM(pt.TrtValue), 0) as Revenue
'                    FROM AppointmentC a
'                    LEFT JOIN Doctors d ON a.DrID = d.DrID
'                    LEFT JOIN Patient_Trts pt ON a.PatientID = pt.PatientID 
'                        AND MONTH(pt.TrtDate) = MONTH(a.AppDate)
'                        AND YEAR(pt.TrtDate) = YEAR(a.AppDate)
'                    WHERE a.AppDate >= DATEADD(MONTH, -12, GETDATE())
'                    GROUP BY d.DrName, FORMAT(a.AppDate, 'yyyy-MM')
'                    ORDER BY Month, DrName"
'            Dim sql = "
'                    SELECT 
'                        ISNULL(d.DrName, 'Unknown') as DrName,
'                        FORMAT(a.AppDate, 'yyyy-MM') as Month,
'                        COUNT(DISTINCT a.PatientID) as PatientCount,
'                        ISNULL(SUM(pt.TrtValue), 0) as Revenue,
'                        CASE 
'                            WHEN COUNT(DISTINCT a.PatientID) > 0 
'                            THEN ISNULL(SUM(pt.TrtValue), 0) / COUNT(DISTINCT a.PatientID)
'                            ELSE 0 
'                        END as AvgRevenuePerPatient
'                    FROM AppointmentC a
'                    LEFT JOIN Doctors d ON a.DrID = d.DrID
'                    LEFT JOIN Patient_Trts pt ON a.PatientID = pt.PatientID 
'                        AND MONTH(pt.TrtDate) = MONTH(a.AppDate)
'                        AND YEAR(pt.TrtDate) = YEAR(a.AppDate)
'                    WHERE a.AppDate >= @DateFrom
'                    AND a.AppDate <= @DateTo
'                    GROUP BY ISNULL(d.DrName, 'Unknown'), FORMAT(a.AppDate, 'yyyy-MM')
'                    ORDER BY Month, DrName"

'            Using cmd As New SqlCommand(sql, conn)
'                cmd.Parameters.AddWithValue("@DateFrom", dateEditFrom.DateTime)
'                cmd.Parameters.AddWithValue("@DateTo", dateEditTo.DateTime)
'                Using adapter As New SqlDataAdapter(cmd)
'                    Dim dt As New DataTable()
'                    adapter.Fill(dt)
'                    pivotGrid.DataSource = dt
'                End Using
'            End Using
'        End Using
'    End Sub
'    Private Sub LoadDoctorPerformanceData()
'        Using conn As New SqlConnection(_connectionString)
'            conn.Open()
'            Dim sqlOld = "
'                    SELECT 
'                        d.DrName,
'                        AVG(CASE WHEN a.Status = 'Completed' THEN 1 ELSE 0 END) * 100 as CompletionRate,
'                        COUNT(DISTINCT a.PatientID) as UniquePatients,
'                        ISNULL(AVG(DATEDIFF(MINUTE, a.StartDateTime, a.EndDateTime)), 0) as AvgDuration
'                    FROM AppointmentC a
'                    LEFT JOIN Doctors d ON a.DrID = d.DrID
'                    WHERE a.AppDate >= DATEADD(MONTH, -3, GETDATE())
'                    GROUP BY d.DrName"
'            Dim sql = "
'                    WITH DoctorMetrics AS (
'                        SELECT 
'                            ISNULL(d.DrName, 'Unknown') as DrName,
'                            AVG(CASE WHEN a.Status = 'Completed' THEN 1.0 ELSE 0.0 END) * 100 as CompletionRate,
'                            COUNT(DISTINCT a.PatientID) as UniquePatients,
'                            ISNULL(AVG(DATEDIFF(MINUTE, a.StartDateTime, a.EndDateTime)), 0) as AvgDuration,
'                            COUNT(*) as TotalAppointments,
'                            SUM(CASE WHEN a.Status = 'No Show' THEN 1 ELSE 0 END) as NoShows
'                        FROM AppointmentC a
'                        LEFT JOIN Doctors d ON a.DrID = d.DrID
'                        WHERE a.AppDate >= @DateFrom
'                        AND a.AppDate <= @DateTo
'                        GROUP BY ISNULL(d.DrName, 'Unknown')
'                    )
'                    SELECT 
'                        'Completion Rate' as Metric,
'                        DrName as Doctor,
'                        CompletionRate as Value
'                    FROM DoctorMetrics
'                    UNION ALL
'                    SELECT 
'                        'Unique Patients',
'                        DrName,
'                        UniquePatients
'                    FROM DoctorMetrics
'                    UNION ALL
'                    SELECT 
'                        'Avg Duration (min)',
'                        DrName,
'                        AvgDuration
'                    FROM DoctorMetrics
'                    UNION ALL
'                    SELECT 
'                        'Appointment Count',
'                        DrName,
'                        TotalAppointments
'                    FROM DoctorMetrics
'                    UNION ALL
'                    SELECT 
'                        'No Show Rate',
'                        DrName,
'                        CASE WHEN TotalAppointments > 0 
'                             THEN (NoShows * 100.0 / TotalAppointments)
'                             ELSE 0 
'                        END
'                    FROM DoctorMetrics
'                    ORDER BY Doctor, Metric"

'            Using cmd As New SqlCommand(sql, conn)
'                cmd.Parameters.AddWithValue("@DateFrom", dateEditFrom.DateTime)
'                cmd.Parameters.AddWithValue("@DateTo", dateEditTo.DateTime)
'                Using adapter As New SqlDataAdapter(cmd)
'                    Dim dt As New DataTable()
'                    adapter.Fill(dt)
'                    ' Clear existing series and add new ones for each doctor
'                    chartDoctorPerformance.Series.Clear()
'                    Dim doctors As List(Of String) = dt.AsEnumerable() _
'                        .Select(Function(row) row.Field(Of String)("Doctor")) _
'                        .Distinct() _
'                        .ToList()
'                    For Each doctor In doctors
'                        Dim series As New Series(doctor, ViewType.RadarPoint)
'                        series.DataSource = dt
'                        series.FilterString = $"[Doctor] = '{doctor}'"
'                        series.ArgumentDataMember = "Metric"
'                        series.ValueDataMembers.AddRange(New String() {"Value"})
'                        Dim view As RadarPointSeriesView = CType(series.View, RadarPointSeriesView)
'                        chartDoctorPerformance.Series.Add(series)
'                    Next
'                End Using
'            End Using
'        End Using
'    End Sub
'    Private Sub LoadTreatmentSuccessData()
'        Using conn As New SqlConnection(_connectionString)
'            conn.Open()
'            Dim sql = "
'                    SELECT 
'                        CASE 
'                            WHEN tt.Finished = 2 THEN 'Completed'
'                            WHEN tt.Finished = 1 THEN 'In Progress'
'                            WHEN tt.Finished = 0 THEN 'Not Started'
'                            ELSE 'Unknown'
'                        END as Status,
'                        COUNT(*) as Count
'                    FROM Patient_ToothTrt tt
'                    WHERE tt.TreatDate >= @DateFrom
'                    AND tt.TreatDate <= @DateTo
'                    GROUP BY 
'                        CASE 
'                            WHEN tt.Finished = 2 THEN 'Completed'
'                            WHEN tt.Finished = 1 THEN 'In Progress'
'                            WHEN tt.Finished = 0 THEN 'Not Started'
'                            ELSE 'Unknown'
'                        END"

'            Using cmd As New SqlCommand(sql, conn)
'                cmd.Parameters.AddWithValue("@DateFrom", dateEditFrom.DateTime)
'                cmd.Parameters.AddWithValue("@DateTo", dateEditTo.DateTime)
'                Using adapter As New SqlDataAdapter(cmd)
'                    Dim dt As New DataTable()
'                    adapter.Fill(dt)
'                    If dt.Rows.Count > 0 Then
'                        chartTreatmentSuccess.DataSource = dt
'                        chartTreatmentSuccess.Series(0).DataSource = dt
'                    End If
'                End Using
'            End Using
'        End Using
'    End Sub
'    Private Sub LoadPatientRetentionData()
'        Using conn As New SqlConnection(_connectionString)
'            conn.Open()
'            Dim sqlOld = "
'                    WITH PatientVisits AS (
'                        SELECT 
'                            PatientID,
'                            YEAR(AppDate) as VisitYear,
'                            MONTH(AppDate) as VisitMonth,
'                            COUNT(*) as VisitCount
'                        FROM AppointmentC
'                        WHERE AppDate >= DATEADD(MONTH, -12, GETDATE())
'                        GROUP BY PatientID, YEAR(AppDate), MONTH(AppDate)
'                                        )
'                    SELECT 
'                        FORMAT(DATEFROMPARTS(VisitYear, VisitMonth, 1), 'MMM yyyy') as Period,
'                        COUNT(DISTINCT PatientID) as ActivePatients,
'                        COUNT(DISTINCT CASE WHEN VisitCount > 1 THEN PatientID END) as ReturningPatients,
'                        CAST(COUNT(DISTINCT CASE WHEN VisitCount > 1 THEN PatientID END) AS FLOAT) / 
'                        NULLIF(COUNT(DISTINCT PatientID), 0) * 100 as RetentionRate
'                    FROM PatientVisits
'                    GROUP BY VisitYear, VisitMonth
'                    ORDER BY VisitYear, VisitMonth"
'            Dim sql = "
'                    WITH PatientVisits AS (
'                        SELECT 
'                            PatientID,
'                            YEAR(AppDate) as VisitYear,
'                            MONTH(AppDate) as VisitMonth,
'                            COUNT(*) as VisitCount
'                        FROM AppointmentC
'                        WHERE AppDate >= DATEADD(MONTH, -12, GETDATE())
'                        AND AppDate <= GETDATE()
'                        GROUP BY PatientID, YEAR(AppDate), MONTH(AppDate)
'                                        ),
'                    MonthlyStats AS (
'                        SELECT 
'                            FORMAT(DATEFROMPARTS(VisitYear, VisitMonth, 1), 'MMM yyyy') as Period,
'                            DATEFROMPARTS(VisitYear, VisitMonth, 1) as PeriodDate,
'                            COUNT(DISTINCT PatientID) as ActivePatients,
'                            COUNT(DISTINCT CASE WHEN VisitCount > 1 THEN PatientID END) as ReturningPatients
'                        FROM PatientVisits
'                        GROUP BY VisitYear, VisitMonth
'                                    )
'                    SELECT 
'                        Period,
'                        ActivePatients,
'                        ReturningPatients,
'                        CASE 
'                            WHEN ActivePatients > 0 
'                            THEN CAST(ReturningPatients AS FLOAT) / ActivePatients
'                            ELSE 0 
'                        END as RetentionRate
'                    FROM MonthlyStats
'                    ORDER BY PeriodDate"

'            Using cmd As New SqlCommand(sql, conn)
'                Using adapter As New SqlDataAdapter(cmd)
'                    Dim dt As New DataTable()
'                    adapter.Fill(dt)
'                    If dt.Rows.Count > 0 Then
'                        chartPatientRetention.DataSource = dt
'                        chartPatientRetention.Series(0).DataSource = dt
'                        ' Configure axis
'                        Dim diagram As XYDiagram = CType(chartPatientRetention.Diagram, XYDiagram)
'                        diagram.AxisX.Title.Text = "Month"
'                        diagram.AxisX.Title.Visible = True
'                        If diagram.SecondaryAxesY.Count > 0 Then
'                            diagram.SecondaryAxesY(0).Visible = True
'                            diagram.SecondaryAxesY(0).NumericOptions.Format = NumericFormat.Percent
'                            diagram.SecondaryAxesY(0).NumericOptions.Precision = 1
'                            diagram.SecondaryAxesY(0).WholeRange.MaxValue = 1
'                            diagram.SecondaryAxesY(0).WholeRange.MinValue = 0
'                        End If
'                    End If
'                End Using
'            End Using
'        End Using
'    End Sub
'    Private Sub ApplyAnalyticsFilters(sender As Object, e As EventArgs)
'        LoadAnalyticsData()
'    End Sub
'    Private Sub DateFilterChanged(sender As Object, e As EventArgs)
'        ' Optional: Add validation or auto-refresh logic
'        If dateEditFrom.DateTime > dateEditTo.DateTime Then
'            XtraMessageBox.Show("Start date cannot be after end date.",
'                              "Validation Error",
'                              MessageBoxButtons.OK,
'                              MessageBoxIcon.Warning)
'            dateEditFrom.EditValue = dateEditTo.DateTime.AddMonths(-1)
'        End If
'    End Sub
'    Private Sub ExportAnalyticsData(sender As Object, e As EventArgs)
'        Try
'            Using saveDialog As New SaveFileDialog()
'                saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|PDF Files (*.pdf)|*.pdf"
'                saveDialog.FileName = $"Analytics_Report_{Date.Now:yyyyMMdd_HHmmss}"
'                If saveDialog.ShowDialog() = DialogResult.OK Then
'                    Select Case saveDialog.FilterIndex
'                        Case 1 ' Excel
'                            pivotGrid.ExportToXlsx(saveDialog.FileName)
'                        Case 2 ' PDF
'                            pivotGrid.ExportToPdf(saveDialog.FileName)
'                    End Select
'                    XtraMessageBox.Show($"Export completed successfully!{vbCrLf}File: {saveDialog.FileName}",
'                                      "Export Complete",
'                                      MessageBoxButtons.OK,
'                                      MessageBoxIcon.Information)
'                End If
'            End Using
'        Catch ex As Exception
'            XtraMessageBox.Show($"Error exporting data: {ex.Message}",
'                              "Export Error",
'                              MessageBoxButtons.OK,
'                              MessageBoxIcon.Error)
'        End Try
'    End Sub
'    Public Sub RefreshData()
'        LoadAnalyticsData()
'    End Sub
'    Protected Overrides Sub Dispose(disposing As Boolean)
'        If disposing Then
'            ' Clean up resources
'            If pivotGrid IsNot Nothing Then
'                pivotGrid.Dispose()
'                pivotGrid = Nothing
'            End If
'            If chartDoctorPerformance IsNot Nothing Then
'                chartDoctorPerformance.Dispose()
'                chartDoctorPerformance = Nothing
'            End If
'            If chartPatientRetention IsNot Nothing Then
'                chartPatientRetention.Dispose()
'                chartPatientRetention = Nothing
'            End If
'            If chartTreatmentSuccess IsNot Nothing Then
'                chartTreatmentSuccess.Dispose()
'                chartTreatmentSuccess = Nothing
'            End If
'        End If
'        MyBase.Dispose(disposing)
'    End Sub
'End Class




