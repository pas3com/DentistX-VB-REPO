Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.DataProcessing
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraTab
Imports DevExpress.XtraTreeList

Public Class TreatmentsPage

    Private ReadOnly _dbHelper As New DashDataModel.DatabaseHelper()
    Private ReadOnly _connectionString As String = DashDataModel.DatabaseHelper._connectionString
    Private _currentFilter As New DashboardFilter()

    Friend WithEvents chartMostRepeated As ChartControl
    Friend WithEvents chartMostRepeatedV2 As ChartControl
    Friend WithEvents chartMostExpensive As ChartControl
    Friend WithEvents chartMostTreatedTeeth As ChartControl

    Friend WithEvents treeListTreatments As TreeList
    Friend WithEvents gridControlActiveTreatments As GridControl
    Friend WithEvents gridActView As GridView
    Friend WithEvents chartTreatmentTypes As ChartControl
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl

    Friend WithEvents tileContainer As DevExpress.XtraEditors.TileControl
    Friend WithEvents tileLast10TrtPatients As DevExpress.XtraEditors.TileItem
    Friend WithEvents tileLast10ApptPatients As DevExpress.XtraEditors.TileItem
    Friend WithEvents tileLast10VistPatients As DevExpress.XtraEditors.TileItem
    Friend WithEvents tileOutstanding As DevExpress.XtraEditors.TileItem

    Friend WithEvents gridLast10Trt As GridControl
    Friend WithEvents viewLast10Trt As GridView
    Friend WithEvents gridLast10Appt As GridControl
    Friend WithEvents viewLast10Appt As GridView
    Friend WithEvents gridLast10Vist As GridControl
    Friend WithEvents viewLast10Vist As GridView
    Friend WithEvents top10Trts As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents top10Appts As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents top10Vist As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents tabLast10TrtPatients As XtraTabPage
    Friend WithEvents tabLast10Appts As XtraTabPage
    Friend WithEvents tabLast10Vist As XtraTabPage


    Public Sub New(_Filter As DashboardFilter)
        InitializeComponent()
        InitializeComponent1()
        SetupTreatmentsDashboard()
        _currentFilter = _Filter
        LoadTreatmentData(_currentFilter)
    End Sub
    Private Sub InitializeComponent1()
        Me.Dock = DockStyle.Fill

        Dim pnlBody As New DevExpress.XtraEditors.SidePanel()
        Me.Controls.Add(pnlBody)
        pnlBody.Dock = DockStyle.Fill
        ' Create tab control for different views
        tabControl = New DevExpress.XtraTab.XtraTabControl()
        pnlBody.Controls.Add(tabControl)
        tabControl.Dock = DockStyle.Fill
        ' Tiles
        tileContainer = New DevExpress.XtraEditors.TileControl()
        tileContainer.Dock = DockStyle.Fill
        Dim pnl As New DevExpress.XtraEditors.SidePanel()
        pnl.Height = 180
        pnl.Padding = New Padding(10)
        pnl.Dock = DockStyle.Top
        pnl.Controls.Add(tileContainer)
        Me.Controls.Add(pnl)
        ' Tab 1: Active Treatments
        Dim tabActive As New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Active Treatments", "العلاجات النشطة")}
        gridControlActiveTreatments = New GridControl() With {.Dock = DockStyle.Fill}
        gridActView = New GridView(gridControlActiveTreatments)
        gridControlActiveTreatments.MainView = gridActView
        gridControlActiveTreatments.ViewCollection.Add(gridActView)

        tabActive.Controls.Add(gridControlActiveTreatments)
        tabControl.TabPages.Add(tabActive)
        ' Tab 2: Treatment Tree
        Dim tabTree As New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Treatment Hierarchy", "هرمية العلاجات")}
        treeListTreatments = New TreeList()
        treeListTreatments.Dock = DockStyle.Fill
        tabTree.Controls.Add(treeListTreatments)
        tabControl.TabPages.Add(tabTree)
        ' Tab 3: Treatment Analytics
        Dim tabAnalytics As New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Analytics", "تحليلات")}
        chartTreatmentTypes = New ChartControl()
        chartTreatmentTypes.Dock = DockStyle.Fill
        tabAnalytics.Controls.Add(chartTreatmentTypes)
        tabControl.TabPages.Add(tabAnalytics)
        ' --- Tab 4: Most Repeated Treatments
        Dim tabMostRepeated As New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Most Repeated Treatments", "العلاجات الأكثر تكرارًا")}
        chartMostRepeated = New ChartControl() With {.Dock = DockStyle.Fill}
        tabMostRepeated.Controls.Add(chartMostRepeated)
        tabControl.TabPages.Add(tabMostRepeated)
        ' --- Tab 4 V2: Most Repeated Treatments
        Dim tabMostRepeatedV2 As New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Most Repeated Treatments V2", "العلاجات الأكثر تكرارًا (إصدار 2)")}
        chartMostRepeatedV2 = New ChartControl() With {.Dock = DockStyle.Fill}
        tabMostRepeatedV2.Controls.Add(chartMostRepeatedV2)
        tabControl.TabPages.Add(tabMostRepeatedV2)
        ' --- Tab 5: Most Expensive Treatments
        Dim tabMostExpensive As New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Most Expensive Treatments", "العلاجات الأعلى تكلفة")}
        chartMostExpensive = New ChartControl() With {.Dock = DockStyle.Fill}
        tabMostExpensive.Controls.Add(chartMostExpensive)
        tabControl.TabPages.Add(tabMostExpensive)

        ' --- Tab 6: Most Treated Teeth
        Dim tabMostTreatedTeeth As New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Most Treated Teeth", "الأسنان الأكثر معالجة")}
        chartMostTreatedTeeth = New ChartControl() With {.Dock = DockStyle.Fill}
        tabMostTreatedTeeth.Controls.Add(chartMostTreatedTeeth)
        tabControl.TabPages.Add(tabMostTreatedTeeth)

        ' --- Tab 7: Last Treated Patients
        tabLast10TrtPatients = New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Last Treated Patients", "آخر المرضى المعالجين")}
        tabLast10TrtPatients.Name = "tabLast10TrtPatients"
        ' --- Top panel
        Dim pnlTopTrts As New PanelControl() With {.Dock = DockStyle.Top, .Height = 45, .Padding = New Padding(10, 8, 10, 8)}

        ' --- Label
        Dim lblTrts As New LabelControl() With {.Text = If(Eng, "Select Count Of Patients", "اختر عدد المرضى"),
                                                .AutoSizeMode = LabelAutoSizeMode.None,
                                                .Width = 150,
                                                .Font = New Font("Calibri", 10, FontStyle.Bold)}

        ' --- SpinEdit
        top10Trts = New DevExpress.XtraEditors.SpinEdit() With {.Value = 10, .Width = 80}

        With top10Trts.Properties
            .MinValue = 5
            .MaxValue = 100
            .Increment = 5
            .IsFloatValue = False
        End With

        ' --- Manual placement inside top panel (clean & predictable)
        lblTrts.Location = New Point(5, 12)
        top10Trts.Location = New Point(lblTrts.Right + 10, 9)

        pnlTopTrts.Controls.Add(lblTrts)
        pnlTopTrts.Controls.Add(top10Trts)

        ' --- Grid
        gridLast10Trt = New GridControl() With {.Dock = DockStyle.Fill}

        viewLast10Trt = New GridView(gridLast10Trt)
        gridLast10Trt.MainView = viewLast10Trt
        gridLast10Trt.ViewCollection.Add(viewLast10Trt)

        ' --- Assemble tab
        tabLast10TrtPatients.Controls.Add(gridLast10Trt)
        tabLast10TrtPatients.Controls.Add(pnlTopTrts)

        tabControl.TabPages.Add(tabLast10TrtPatients)

        ' --- Tab 8: Last10 Appt
        tabLast10Appts = New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Last 10 Appointments", "آخر 10 مواعيد")}
        tabLast10Appts.Name = "tabLast10Appts"
        ' --- Top panel
        Dim pnlTopAppts As New PanelControl() With {.Dock = DockStyle.Top, .Height = 45, .Padding = New Padding(10, 8, 10, 8)}

        ' --- Label
        Dim lblAppts As New LabelControl() With {.Text = If(Eng, "Select Count Of Appointments", "اختر عدد المواعيد"),
                                                .AutoSizeMode = LabelAutoSizeMode.None,
                                                .Width = 170,
                                                .Font = New Font("Calibri", 10, FontStyle.Bold)}

        ' --- SpinEdit
        top10Appts = New DevExpress.XtraEditors.SpinEdit() With {.Value = 10, .Width = 80}

        With top10Appts.Properties
            .MinValue = 5
            .MaxValue = 100
            .Increment = 5
            .IsFloatValue = False
        End With

        ' --- Manual placement inside top panel (clean & predictable)
        lblAppts.Location = New Point(5, 12)
        top10Appts.Location = New Point(lblAppts.Right + 10, 9)

        pnlTopAppts.Controls.Add(lblAppts)
        pnlTopAppts.Controls.Add(top10Appts)

        gridLast10Appt = New GridControl() With {.Dock = DockStyle.Fill}
        viewLast10Appt = New GridView(gridControlActiveTreatments)
        gridLast10Appt.MainView = viewLast10Appt
        gridLast10Appt.ViewCollection.Add(viewLast10Appt)
        ' --- Assemble tab
        tabLast10Appts.Controls.Add(gridLast10Appt)
        tabLast10Appts.Controls.Add(pnlTopAppts)
        tabControl.TabPages.Add(tabLast10Appts)

        ' --- Tab 9: Last10 Visits
        tabLast10Vist = New DevExpress.XtraTab.XtraTabPage() With {.Text = If(Eng, "Last 10 Visits", "آخر 10 زيارات")}
        tabLast10Vist.Name = "tabLast10Vist"
        ' --- Top panel
        Dim pnlTopVist As New PanelControl() With {.Dock = DockStyle.Top, .Height = 45, .Padding = New Padding(10, 8, 10, 8)}

        ' --- Label
        Dim lblVist As New LabelControl() With {.Text = If(Eng, "Select Count Of Visits", "اختر عدد الزيارات"),
                                                .AutoSizeMode = LabelAutoSizeMode.None,
                                                .Width = 150,
                                                .Font = New Font("Calibri", 10, FontStyle.Bold)}

        ' --- SpinEdit
        top10Vist = New DevExpress.XtraEditors.SpinEdit() With {.Value = 10, .Width = 80}

        With top10Vist.Properties
            .MinValue = 5
            .MaxValue = 100
            .Increment = 5
            .IsFloatValue = False
        End With

        ' --- Manual placement inside top panel (clean & predictable)
        lblVist.Location = New Point(5, 12)
        top10Vist.Location = New Point(lblVist.Right + 10, 9)

        pnlTopVist.Controls.Add(lblVist)
        pnlTopVist.Controls.Add(top10Vist)

        gridLast10Vist = New GridControl() With {.Dock = DockStyle.Fill}
        viewLast10Vist = New GridView(gridControlActiveTreatments)
        gridLast10Vist.MainView = viewLast10Vist
        gridLast10Vist.ViewCollection.Add(viewLast10Vist)

        tabLast10Vist.Controls.Add(gridLast10Vist)
        tabLast10Vist.Controls.Add(pnlTopVist)
        tabControl.TabPages.Add(tabLast10Vist)


        CreateKPITiles()
        ConfigureActiveTreatmentsGrid()
        ConfigureTreatmentTree()
        ConfigureTreatmentChart()
    End Sub

#Region "KPIs"


    ' ============ KPI TILES ============
    Private Sub CreateKPITiles()

        ' Make sure tileContainer is initialized
        If tileContainer Is Nothing Then
            tileContainer = New DevExpress.XtraEditors.TileControl()
            tileContainer.Dock = DockStyle.Fill
            tileContainer.Height = 100
        End If

        ' Create a tile group to hold the tiles
        Dim tileGroup As New DevExpress.XtraEditors.TileGroup()
        tileGroup.Name = "groupKPIs"

        If tileLast10TrtPatients Is Nothing Then
            ' Tile 1: Last 10 Treated Patients
            tileLast10TrtPatients = New DevExpress.XtraEditors.TileItem()
            tileLast10TrtPatients.Name = "tileLast10TrtPatients"
            tileLast10TrtPatients.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide
            tileLast10TrtPatients.Text = If(Eng, "Last 10 Treated PATIENTS", "آخر 10 مرضى معالجين")
            tileLast10TrtPatients.AppearanceItem.Normal.FontSizeDelta = 2
            tileLast10TrtPatients.AppearanceItem.Normal.ForeColor = Color.White
            tileLast10TrtPatients.AppearanceItem.Normal.BackColor = Color.FromArgb(42, 157, 143)
            tileLast10TrtPatients.TextAlignment = TileItemContentAlignment.MiddleCenter
            AddHandler tileLast10TrtPatients.ItemClick, AddressOf tile_ItemClick
            ' Set tile properties correctly
        End If
        If tileLast10ApptPatients Is Nothing Then
            ' Tile 2: Last 10's Appointments
            tileLast10ApptPatients = New DevExpress.XtraEditors.TileItem()
            tileLast10ApptPatients.Name = "tileLast10ApptPatients"
            tileLast10ApptPatients.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide
            tileLast10ApptPatients.Text = If(Eng, "Last 10 APPOINTMENTS", "آخر 10 مواعيد")
            tileLast10ApptPatients.AppearanceItem.Normal.FontSizeDelta = 2
            tileLast10ApptPatients.AppearanceItem.Normal.BackColor = Color.FromArgb(38, 70, 83)
            tileLast10ApptPatients.AppearanceItem.Normal.ForeColor = Color.White
            tileLast10ApptPatients.TextAlignment = TileItemContentAlignment.MiddleCenter
            AddHandler tileLast10ApptPatients.ItemClick, AddressOf tile_ItemClick
        End If
        If tileLast10VistPatients Is Nothing Then
            ' Tile 2: Last 10's VISITS
            tileLast10VistPatients = New DevExpress.XtraEditors.TileItem()
            tileLast10VistPatients.Name = "tileLast10VistPatients"
            tileLast10VistPatients.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide
            tileLast10VistPatients.Text = If(Eng, "Last 10 VISITS", "آخر 10 زيارات")
            tileLast10VistPatients.AppearanceItem.Normal.FontSizeDelta = 2
            tileLast10VistPatients.AppearanceItem.Normal.BackColor = Color.FromArgb(48, 170, 183)
            tileLast10VistPatients.AppearanceItem.Normal.ForeColor = Color.White
            tileLast10VistPatients.TextAlignment = TileItemContentAlignment.MiddleCenter
            AddHandler tileLast10VistPatients.ItemClick, AddressOf tile_ItemClick
        End If

        ' Add more tiles if needed...
        ' Dim tileRevenue As New DevExpress.XtraEditors.TileItem()...
        ' Dim tileOutstanding As New DevExpress.XtraEditors.TileItem()...

        ' Add tiles to the group
        tileGroup.Items.Add(tileLast10TrtPatients)
        tileGroup.Items.Add(tileLast10ApptPatients)
        tileGroup.Items.Add(tileLast10VistPatients)
        ' Add the group to tileContainer
        tileContainer.Groups.Add(tileGroup)

        ' Alternative method: Add items directly to groups collection
        ' tileContainer.Groups(0).Items.AddRange(New DevExpress.XtraEditors.TileItem() {
        '     tilePatients, tileAppointments
        ' })
    End Sub

    Private Sub tile_ItemClick(sender As Object, e As TileItemEventArgs)
        Try
            Select Case e.Item.Name
                Case "tileLast10TrtPatients"
                    tabControl.SelectedTabPage = tabLast10TrtPatients
                    LoadLast10Patients(gridLast10Trt, viewLast10Trt, CInt(top10Trts.Value))
                Case "tileLast10ApptPatients"
                    tabControl.SelectedTabPage = tabLast10Appts
                    LoadLastAppts(gridLast10Appt, viewLast10Appt, CInt(top10Appts.Value))
                Case "tileLast10VistPatients"
                    tabControl.SelectedTabPage = tabLast10Vist
                    LoadLastVisits(gridLast10Vist, viewLast10Vist, CInt(top10Vist.Value))
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub LoadLast10Patients(grid As GridControl, view As GridView, Optional topN As Integer = 10)

        Dim sql1 As String =
        "SELECT TOP (10)
            p.PatientID,
            p.PatientNumber,
            p.PatientName,
            pt.TreatDate,
            pt.Treat ,
            pt.Finished,
            CASE pt.Finished
                WHEN 0 THEN 'In Progress'
                WHEN 1 THEN 'Completed'
                WHEN 2 THEN 'Canceled'
                ELSE 'Unknown'
            END AS FinishedText
         FROM Patient p
         INNER JOIN Patient_ToothTrt pt
            ON p.PatientID = pt.PatientID
         ORDER BY pt.TreatDate DESC"

        Dim sql As String =
        "WITH LastTreat AS
         (
             SELECT
                 p.PatientID,
                 p.PatientNumber,
                 p.PatientName,
                 pt.TreatDate,
                 pt.Treat,
                 pt.Finished,
                 CASE pt.Finished
                     WHEN 0 THEN 'In Progress'
                     WHEN 1 THEN 'Completed'
                     WHEN 2 THEN 'Canceled'
                     ELSE 'Unknown'
                 END AS FinishedText,
                 ROW_NUMBER() OVER
                 (
                     PARTITION BY p.PatientID
                     ORDER BY pt.TreatDate DESC
                 ) AS rn
             FROM Patient p
             INNER JOIN Patient_ToothTrt pt
                 ON p.PatientID = pt.PatientID
         )
         SELECT TOP (@TopN)
             PatientID,
             PatientNumber,
             PatientName,
             TreatDate,
             Treat,
             Finished,
             FinishedText
         FROM LastTreat
         WHERE rn = 1
         ORDER BY TreatDate DESC"
        Dim dt As New DataTable

        Using con As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.Add("@TopN", SqlDbType.Int).Value = topN
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        grid.DataSource = dt
        ConfigureLastPatientsGrid(view)

    End Sub

    Private Sub ConfigureLastPatientsGrid(view As GridView)

        view.BeginUpdate()

        view.OptionsBehavior.Editable = False
        view.OptionsView.ShowGroupPanel = False
        view.OptionsView.ShowIndicator = False
        view.OptionsView.ColumnAutoWidth = False
        view.OptionsView.EnableAppearanceEvenRow = True

        view.Columns("PatientID").Visible = False
        view.Columns("Finished").Visible = False   ' keep numeric value hidden

        view.Columns("PatientNumber").Caption = If(Eng, "No.", "رقم")
        view.Columns("PatientNumber").Width = 80

        view.Columns("PatientName").Caption = If(Eng, "Patient", "المريض")
        view.Columns("PatientName").Width = 180

        view.Columns("TreatDate").Caption = If(Eng, "Date", "التاريخ")
        view.Columns("TreatDate").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        view.Columns("TreatDate").DisplayFormat.FormatString = "dd/MM/yyyy"
        view.Columns("TreatDate").Width = 130

        view.Columns("Treat").Caption = If(Eng, "Treatment", "العلاج")
        view.Columns("Treat").Width = 250

        view.Columns("FinishedText").Caption = If(Eng, "Status", "الحالة")
        view.Columns("FinishedText").Width = 110

        ' Sort by date descending (visual safety net)
        view.SortInfo.Clear()
        view.SortInfo.Add(view.Columns("TreatDate"), DevExpress.Data.ColumnSortOrder.Descending)

        'view.BestFitColumns()

        view.EndUpdate()

    End Sub

    Private Sub LoadLastVisits(grid As GridControl, view As GridView, Optional topN As Integer = 10)
        Dim sw As New Stopwatch
        sw.Start()
        Dim sql As String = $"
        SELECT TOP (@TopN)
            v.PatientID,
            p.PatientNumber,
            p.PatientName,
            CAST(v.VisDateTime AS DATE) AS VisitDate,
            CAST(v.VisDateTime AS TIME) AS VisitTime,
            v.VisDetail,
            v.VisNotes
        FROM dbo.Visits v
        INNER JOIN dbo.Patient p ON v.PatientID = p.PatientID
        ORDER BY v.VisDateTime DESC
    "
        Dim dt As New DataTable()
        Using conn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TopN", topN)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        grid.DataSource = dt
        sw.Stop()
        MsgBox(sw.Elapsed.TotalSeconds.ToString("F2") & " seconds")
        sw.Restart()
        ConfigureLastVisitsGrid(view)
        sw.Stop()
        MsgBox(sw.Elapsed.TotalSeconds.ToString("F2") & " seconds")
    End Sub
    Private Sub ConfigureLastVisitsGrid(view As GridView)
        view.BeginUpdate()

        view.OptionsBehavior.Editable = False
        view.OptionsView.ShowGroupPanel = False
        view.OptionsView.ShowIndicator = False
        view.OptionsView.ColumnAutoWidth = False
        view.OptionsView.EnableAppearanceEvenRow = True
        'view.BestFitColumns()

        view.Columns("PatientID").Visible = False

        view.Columns("PatientNumber").Caption = If(Eng, "No.", "رقم")
        view.Columns("PatientNumber").Width = 80

        view.Columns("PatientName").Caption = If(Eng, "Patient", "المريض")
        view.Columns("PatientName").Width = 180

        view.Columns("VisitDate").Caption = If(Eng, "Date", "التاريخ")
        view.Columns("VisitDate").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        view.Columns("VisitDate").DisplayFormat.FormatString = "dd/MM/yyyy"
        view.Columns("VisitDate").Width = 130

        view.Columns("VisitTime").Caption = If(Eng, "Time", "الوقت")
        view.Columns("VisitTime").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        view.Columns("VisitTime").DisplayFormat.FormatString = "HH:mm"
        view.Columns("VisitTime").Width = 130

        view.Columns("VisDetail").Caption = If(Eng, "Visit Detail", "تفاصيل الزيارة")
        view.Columns("VisDetail").Width = 220

        view.Columns("VisNotes").Caption = If(Eng, "Notes", "ملاحظات")
        view.Columns("VisNotes").Width = 220

        ' Sort by date descending (visual safety net)
        view.SortInfo.Clear()
        view.SortInfo.Add(view.Columns("VisitDate"), DevExpress.Data.ColumnSortOrder.Descending)

        view.EndUpdate()
    End Sub

    Private Sub LoadLastAppts(grid As GridControl, view As GridView, Optional topN As Integer = 10)

        Dim sql As String = $"
        SELECT TOP (@TopN)
            a.AppointmentID,
            a.PatientID,
            p.PatientNumber,
            p.PatientName,
            d.DrName,
            CAST(a.AppDate AS DATE) AS AppDate,
            CAST(a.StartDateTime AS TIME) AS StartTime,
            CAST(a.EndDateTime AS TIME) AS EndTime,
            a.Reason,
            a.Notes
        FROM dbo.AppointmentC a
        INNER JOIN dbo.Patient p ON a.PatientID = p.PatientID
        LEFT JOIN dbo.Doctors d ON a.DrID = d.DrID
        ORDER BY a.StartDateTime DESC
    "
        Dim dt As New DataTable()
        Using conn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TopN", topN)


                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using


            End Using
        End Using
        grid.DataSource = dt
        ConfigureLastApptsGrid(view)
    End Sub
    Private Sub ConfigureLastApptsGrid(view As GridView)
        view.BeginUpdate()

        view.OptionsBehavior.Editable = False
        view.OptionsView.ShowGroupPanel = False
        view.OptionsView.ShowIndicator = False
        view.OptionsView.ColumnAutoWidth = False
        view.OptionsView.EnableAppearanceEvenRow = True
        'view.BestFitColumns()

        view.Columns("AppointmentID").Visible = False
        view.Columns("PatientID").Visible = False


        view.Columns("PatientNumber").Caption = If(Eng, "No.", "رقم")
        view.Columns("PatientNumber").Width = 80

        view.Columns("PatientName").Caption = If(Eng, "Patient", "المريض")
        view.Columns("PatientName").Width = 180

        view.Columns("DrName").Caption = If(Eng, "Doctor", "الطبيب")
        view.Columns("DrName").Width = 180

        view.Columns("AppDate").Caption = If(Eng, "Date", "التاريخ")
        view.Columns("AppDate").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        view.Columns("AppDate").DisplayFormat.FormatString = "dd/MM/yyyy"
        view.Columns("AppDate").Width = 130

        view.Columns("StartTime").Caption = If(Eng, "Start", "البداية")
        view.Columns("StartTime").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        view.Columns("StartTime").DisplayFormat.FormatString = "HH:mm tt"
        view.Columns("StartTime").Width = 130

        view.Columns("EndTime").Caption = If(Eng, "End", "النهاية")
        view.Columns("EndTime").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        view.Columns("EndTime").DisplayFormat.FormatString = "HH:mm"
        view.Columns("EndTime").Width = 130

        view.Columns("Reason").Caption = If(Eng, "Reason", "السبب")
        view.Columns("Reason").Width = 250


        view.Columns("Notes").Caption = If(Eng, "Notes", "ملاحظات")
        view.Columns("Notes").Width = 250

        ' Sort by date descending (visual safety net)
        view.SortInfo.Clear()
        view.SortInfo.Add(view.Columns("AppDate"), DevExpress.Data.ColumnSortOrder.Descending)

        view.EndUpdate()
    End Sub




#End Region
    Private Sub SetupTreatmentsDashboard()
        ' Set default tab selection
        If tabControl.TabPages.Count > 0 Then
            tabControl.SelectedTabPageIndex = 0
        End If
    End Sub
    Private Sub ConfigureTreatmentChart()
        ' Configure the chart for treatment analytics
        If chartTreatmentTypes Is Nothing Then Return
        ' Clear any existing series
        chartTreatmentTypes.Series.Clear()
        ' Create a series for treatment types
        Dim series As New Series(If(Eng, "Treatment Types", "أنواع العلاجات"), ViewType.Bar)
        series.ArgumentScaleType = ScaleType.Qualitative
        series.ValueScaleType = ScaleType.Numerical
        series.Label.TextPattern = "{A}: {V}"
        ' Configure series view
        Dim view As BarSeriesView = CType(series.View, BarSeriesView)
        view.ColorEach = True
        view.Border.Visibility = True
        view.Border.Color = Color.DarkGray
        view.Border.Thickness = 1
        ' Configure chart title
        chartTreatmentTypes.Titles.Clear()
        chartTreatmentTypes.Titles.Add(New ChartTitle() With {
        .Text = If(Eng, "Treatment Type Distribution", "توزيع أنواع العلاجات"),
        .Font = New Font("Tahoma", 12, FontStyle.Bold),
        .TextColor = Color.FromArgb(64, 64, 64)
    })
        ' Configure legend
        chartTreatmentTypes.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
        chartTreatmentTypes.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right
        chartTreatmentTypes.Legend.AlignmentVertical = LegendAlignmentVertical.Top
        ' Configure diagram
        Dim diagram As XYDiagram = CType(chartTreatmentTypes.Diagram, XYDiagram)
        If diagram IsNot Nothing Then
            diagram.AxisX.Title.Text = If(Eng, "Treatment Type", "نوع العلاج")
            diagram.AxisX.Title.Visibility = True
            diagram.AxisX.Label.Angle = -45
            diagram.AxisY.Title.Text = If(Eng, "Count", "العدد")
            diagram.AxisY.Title.Visibility = True
            diagram.AxisY.Label.TextPattern = "{V:0}"
        End If
        ' Add series to chart
        chartTreatmentTypes.Series.Add(series)
    End Sub
    Private Sub ConfigureBarChart(chart As ChartControl, title As String, arabicTitle As String)
        chart.Series.Clear()
        Dim series As New Series(If(Eng, title, arabicTitle), ViewType.Bar)
        series.ArgumentScaleType = ScaleType.Qualitative
        series.ValueScaleType = ScaleType.Numerical
        series.Label.TextPattern = "{A}: {V}"
        Dim view As BarSeriesView = CType(series.View, BarSeriesView)
        view.ColorEach = True
        view.Border.Visibility = True
        view.Border.Color = Color.DarkGray
        view.Border.Thickness = 1
        chart.Titles.Clear()
        chart.Titles.Add(New ChartTitle() With {
        .Text = If(Eng, title, arabicTitle),
        .Font = New Font("Tahoma", 12, FontStyle.Bold),
        .TextColor = Color.FromArgb(64, 64, 64)
    })
        chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
        chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right
        chart.Legend.AlignmentVertical = LegendAlignmentVertical.Top
        chart.Series.Add(series)
    End Sub

    '  load treatment data for the chart:
    Private Sub LoadTreatmentChartData()
        Try
            Using conn As New SqlConnection(_connectionString)
                conn.Open()
                Dim sql = "
                            SELECT 
                                CASE 
                                    WHEN pt.ToothTrtID IS NOT NULL THEN 'Tooth Treatment'
                                    WHEN pt.OrthoID IS NOT NULL THEN 'Orthodontics'
                                    WHEN pt.OtherTrtID IS NOT NULL THEN 'Other Treatment'
                                    ELSE 'Unknown'
                                END AS TreatmentType,
                                COUNT(*) AS TreatmentCount
                            FROM Patient_Trts pt
                            WHERE pt.TrtDate >= (
                                SELECT MIN(TrtDate)
                                FROM Patient_Trts
                            )
                            GROUP BY 
                                CASE 
                                    WHEN pt.ToothTrtID IS NOT NULL THEN 'Tooth Treatment'
                                    WHEN pt.OrthoID IS NOT NULL THEN 'Orthodontics'
                                    WHEN pt.OtherTrtID IS NOT NULL THEN 'Other Treatment'
                                    ELSE 'Unknown'
                                END
                            ORDER BY TreatmentCount DESC"


                Using cmd As New SqlCommand(sql, conn)
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        ' Bind data to chart
                        chartTreatmentTypes.DataSource = dt
                        chartTreatmentTypes.Series(0).ArgumentDataMember = "TreatmentType"
                        chartTreatmentTypes.Series(0).ValueDataMembers.AddRange(New String() {"TreatmentCount"})
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle error
            Debug.WriteLine($"Error loading treatment chart data: {ex.Message}")
        End Try
    End Sub
    ' Update the LoadTreatmentData method to call LoadTreatmentChartData:
    Private Sub LoadTreatmentData(filter As DashboardFilter)
        ' Load active treatments
        Dim treatments = _dbHelper.GetTreatments(filter)
        gridControlActiveTreatments.DataSource = treatments
        ' Load tooth treatments for tree
        LoadToothTreatments()
        ' Load treatment type distribution for chart
        LoadTreatmentChartData() ' Add this line

        ' New analytics
        LoadMostRepeatedTreatments()
        LoadMostRepeatedTreatmentsV2()
        LoadMostExpensiveTreatments()
        LoadMostTreatedTeeth()
    End Sub
    Private Sub ConfigureActiveTreatmentsGrid()
        Dim view As GridView = gridActView ' TryCast(gridControlActiveTreatments.MainView, GridView)
        'With view
        '    .OptionsView.ShowAutoFilterRow = True
        '    .OptionsView.ShowGroupPanel = True
        '    ' Add columns
        '    .Columns.AddField("PatientName").Caption = "Patient"
        '    .Columns.AddField("Detail").Caption = "Treatment"
        '    .Columns.AddField("TrtDate").Caption = "Start Date"
        '    .Columns("TrtDate").DisplayFormat.FormatString = "dd/MM/yyyy"
        '    .Columns.AddField("TrtValue").Caption = "Value"
        '    .Columns("TrtValue").DisplayFormat.FormatString = "C2"
        '    .Columns.AddField("Balance").Caption = "Balance"
        '    .Columns("Balance").DisplayFormat.FormatString = "C2"
        '    .Columns.AddField("DaysActive").Caption = "Days Active"
        '    ' Color code by balance
        '    AddHandler .RowCellStyle, AddressOf TreatmentGrid_RowCellStyle
        'End With
        With view
            .OptionsView.ShowAutoFilterRow = True
            .OptionsView.ShowGroupPanel = True

            .Columns.Clear()

            Dim col = .Columns.AddField("PatientName")
            col.Caption = If(Eng, "Patient", "المريض")
            col.Visible = True

            col = .Columns.AddField("Detail")
            col.Caption = If(Eng, "Treatment", "العلاج")
            col.Visible = True

            col = .Columns.AddField("TrtDate")
            col.Caption = If(Eng, "Start Date", "تاريخ البدء")
            col.DisplayFormat.FormatString = "dd/MM/yyyy"
            col.Visible = True

            col = .Columns.AddField("TrtValue")
            col.Caption = If(Eng, "Value", "القيمة")
            col.DisplayFormat.FormatString = "C2"
            col.Visible = True

            col = .Columns.AddField("Balance")
            col.Caption = If(Eng, "Balance", "الرصيد")
            col.DisplayFormat.FormatString = "C2"
            col.Visible = True

            col = .Columns.AddField("DaysActive")
            col.Caption = If(Eng, "Days Active", "أيام النشاط")
            col.Visible = True
            AddHandler .RowCellStyle, AddressOf TreatmentGrid_RowCellStyle
        End With

    End Sub
    Private Sub TreatmentGrid_RowCellStyle(sender As Object, e As RowCellStyleEventArgs)
        Dim view As GridView = TryCast(sender, GridView)
        Dim treatment = TryCast(view.GetRow(e.RowHandle), PatientTreatment)
        If treatment IsNot Nothing AndAlso e.Column.FieldName = "Balance" Then
            If treatment.Balance > 0 Then
                e.Appearance.BackColor = Color.FromArgb(255, 200, 200) ' Red for outstanding
            ElseIf treatment.Balance = 0 Then
                e.Appearance.BackColor = Color.FromArgb(200, 255, 200) ' Green for paid
            End If
        End If
    End Sub
    Private Sub ConfigureTreatmentTree()
        With treeListTreatments
            .Columns.Clear()
            .KeyFieldName = "ToothTrtID"
            .ParentFieldName = "ParentToothTrtID"

            Dim col = .Columns.Add()
            col.Caption = If(Eng, "Tooth Number", "رقم السن")
            col.FieldName = "ToothNum"
            col.Visible = True

            col = .Columns.Add()
            col.Caption = If(Eng, "Treatment", "العلاج")
            col.FieldName = "Treat"
            col.Visible = True

            col = .Columns.Add()
            col.Caption = If(Eng, "Date", "التاريخ")
            col.FieldName = "TreatDate"
            col.Visible = True

            col = .Columns.Add()
            col.Caption = If(Eng, "Status", "الحالة")
            col.FieldName = "StatusText"
            col.Visible = True
        End With

    End Sub
    Private Sub LoadToothTreatments()
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql = "
                        SELECT 
                            tt.ToothTrtID,
                            tt.ParentToothTrtID,
                            CASE 
                                WHEN tt.ParentToothTrtID IS NULL THEN tt.ToothTrtID 
                                ELSE tt.ParentToothTrtID 
                            END AS GroupToothTrtID,
                            tt.ToothNum,
                            tt.Treat,
                            tt.TreatDate,
                            tt.Finished,
                            CASE tt.Finished
                                WHEN 0 THEN 'In Progress'
                                WHEN 1 THEN 'Completed'
                                WHEN 2 THEN 'Canceled'
                                WHEN 3 THEN 'On Hold'
                                ELSE 'Unknown'
                            END AS StatusText,
                            p.PatientName
                        FROM Patient_ToothTrt tt
                        LEFT JOIN Patient p ON tt.PatientID = p.PatientID
                        ORDER BY 
                                GroupToothTrtID,
                                CASE WHEN tt.ParentToothTrtID IS NULL THEN 0 ELSE 1 END,
                                CASE tt.Finished
                                    WHEN 0 THEN 1   -- In Progress first
                                    WHEN 3 THEN 2   -- On Hold
                                    WHEN 1 THEN 3   -- Completed
                                    WHEN 2 THEN 4   -- Canceled
                                    ELSE 5
                                END,
                                tt.TreatDate DESC
                            "


            Using cmd As New SqlCommand(sql, conn)
                Using adapter As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    treeListTreatments.DataSource = dt
                    treeListTreatments.ExpandAll()
                End Using
            End Using
        End Using
    End Sub
    Private Sub LoadTreatmentTypeChart()
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql = "
                            SELECT 
                                CASE 
                                    WHEN pt.ToothTrtID IS NOT NULL THEN 'Tooth Treatment'
                                    WHEN pt.OrthoID IS NOT NULL THEN 'Orthodontics'
                                    WHEN pt.OtherTrtID IS NOT NULL THEN 'Other Treatment'
                                    ELSE 'Unknown'
                                END AS TreatmentType,
                                COUNT(*) AS TreatmentCount
                            FROM Patient_Trts pt
                            WHERE pt.TrtDate >= (
                                SELECT MIN(TrtDate)
                                FROM Patient_Trts
                            )
                            GROUP BY 
                                CASE 
                                    WHEN pt.ToothTrtID IS NOT NULL THEN 'Tooth Treatment'
                                    WHEN pt.OrthoID IS NOT NULL THEN 'Orthodontics'
                                    WHEN pt.OtherTrtID IS NOT NULL THEN 'Other Treatment'
                                    ELSE 'Unknown'
                                END
                            ORDER BY TreatmentCount DESC"

            Using cmd As New SqlCommand(sql, conn)
                Using adapter As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    chartTreatmentTypes.DataSource = dt
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadMostRepeatedTreatments()
        'Configure the chart
        ConfigureBarChart(chartMostRepeated, "Most Repeated Treatments", "العلاجات الأكثر تكرارًا")
        Try
            Using conn As New SqlConnection(_connectionString)
                conn.Open()
                Dim sql As String = "SELECT TOP 10 TreatmentName, COUNT(*) AS TreatmentCount,
                                    -- Conceptual counters
                                    SUM(CASE WHEN TreatmentName LIKE '%IMPLANT%' THEN 1 ELSE 0 END) AS ImplantCount,
                                    SUM(CASE WHEN TreatmentName LIKE '%EXTRACTION%' THEN 1 ELSE 0 END) AS ExtractionCount,
                                    SUM(CASE WHEN TreatmentName LIKE '%BRIDGE%' THEN 1 ELSE 0 END) AS BridgeCount
                                    FROM ( -- Tooth Treatments
                                        SELECT t.Treat AS TreatmentName FROM Patient_ToothTrt t
                                        INNER JOIN Patient_Trts pt ON t.ToothTrtID = pt.ToothTrtID UNION ALL
                                        -- Orthodontics
                                        SELECT o.Khota AS TreatmentName FROM OrthoInf o INNER JOIN
                                            Patient_Trts pt ON o.OrthoID = pt.OrthoID UNION ALL
                                        -- Other Treatments
                                        SELECT ot.Trt AS TreatmentName FROM Patient_OtherTrt ot INNER JOIN
                                            Patient_Trts pt ON ot.OtherTrtID = pt.OtherTrtID ) AS AllTreatments
                                    GROUP BY TreatmentName ORDER BY TreatmentCount DESC "
                Using cmd As New SqlCommand(sql, conn)
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        ' Bind to chart
                        chartMostRepeated.DataSource = dt
                        With chartMostRepeated.Series(0)
                            .ArgumentDataMember = "TreatmentName"
                            .ValueDataMembers.AddRange("TreatmentCount")
                        End With
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine($"Error loading most repeated treatments: {ex.Message}")
        End Try
    End Sub
    Private Sub LoadMostRepeatedTreatmentsV2()
        ConfigureBarChart(chartMostRepeatedV2, "Most Repeated Treatments", "العلاجات الأكثر تكرارًا")
        Try
            Using conn As New SqlConnection(_connectionString)
                conn.Open()
                Dim sql As String = "SELECT TOP 10
                                        TreatmentName,
                                        COUNT(*) AS TreatmentCount,

                                        -- Conceptual counters
                                        SUM(CASE WHEN TreatmentName LIKE '%IMPLANT%' THEN 1 ELSE 0 END) AS ImplantCount,
                                        SUM(CASE WHEN TreatmentName LIKE '%EXTRACTION%' THEN 1 ELSE 0 END) AS ExtractionCount,
                                        SUM(CASE WHEN TreatmentName LIKE '%BRIDGE%' THEN 1 ELSE 0 END) AS BridgeCount

                                    FROM
                                    (
                                        -- Tooth Treatments
                                        SELECT t.Treat AS TreatmentName
                                        FROM Patient_ToothTrt t
                                        INNER JOIN Patient_Trts pt ON t.ToothTrtID = pt.ToothTrtID

                                        UNION ALL

                                        -- Orthodontics
                                        SELECT o.Khota AS TreatmentName
                                        FROM OrthoInf o
                                        INNER JOIN Patient_Trts pt ON o.OrthoID = pt.OrthoID

                                        UNION ALL

                                        -- Other Treatments
                                        SELECT ot.Trt AS TreatmentName
                                        FROM Patient_OtherTrt ot
                                        INNER JOIN Patient_Trts pt ON ot.OtherTrtID = pt.OtherTrtID
                                    ) AS AllTreatments

                                    GROUP BY TreatmentName
                                    ORDER BY TreatmentCount DESC
                                    "
                Using cmd As New SqlCommand(sql, conn)
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)

                        '' Bind to chart
                        'chartMostRepeated.DataSource = dt
                        'With chartMostRepeated.Series(0)
                        '    .ArgumentDataMember = "TreatmentName"
                        '    .ValueDataMembers.AddRange("TreatmentCount")
                        'End With
                        ' Bind datasource
                        chartMostRepeatedV2.DataSource = dt
                        chartMostRepeatedV2.Series.Clear()

                        ' === Total Treatments ===
                        Dim sTotal As New Series("Total", ViewType.Bar)
                        sTotal.ArgumentDataMember = "TreatmentName"
                        sTotal.ValueDataMembers.AddRange("TreatmentCount")

                        ' === Implant ===
                        Dim sImplant As New Series("Implant", ViewType.Bar)
                        sImplant.ArgumentDataMember = "TreatmentName"
                        sImplant.ValueDataMembers.AddRange("ImplantCount")

                        ' === Extraction ===
                        Dim sExtraction As New Series("Extraction", ViewType.Bar)
                        sExtraction.ArgumentDataMember = "TreatmentName"
                        sExtraction.ValueDataMembers.AddRange("ExtractionCount")

                        ' === Bridge ===
                        Dim sBridge As New Series("Bridge", ViewType.Bar)
                        sBridge.ArgumentDataMember = "TreatmentName"
                        sBridge.ValueDataMembers.AddRange("BridgeCount")

                        ' Styling (optional but consistent with your look)
                        For Each s In New Series() {sTotal, sImplant, sExtraction, sBridge}
                            s.ArgumentScaleType = ScaleType.Qualitative
                            s.ValueScaleType = ScaleType.Numerical
                            CType(s.View, BarSeriesView).Border.Visibility = True
                        Next

                        chartMostRepeatedV2.Series.AddRange(New Series() {sTotal, sImplant, sExtraction, sBridge})

                    End Using
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine($"Error loading most repeated treatments: {ex.Message}")
        End Try
    End Sub
    Private Sub LoadMostExpensiveTreatments()
        ConfigureBarChart(chartMostExpensive, "Most Expensive Treatments", "العلاجات الأعلى تكلفة")
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql = "
            SELECT TOP 10 TreatmentName, SUM(TrtValue) AS TotalValue FROM
                        (
                            SELECT t.Treat AS TreatmentName, pt.TrtValue
                            FROM Patient_ToothTrt t
                            INNER JOIN Patient_Trts pt ON t.ToothTrtID = pt.ToothTrtID

                            UNION ALL

                            SELECT o.Khota AS TreatmentName, pt.TrtValue
                            FROM OrthoInf o
                            INNER JOIN Patient_Trts pt ON o.OrthoID = pt.OrthoID

                            UNION ALL

                            SELECT ot.Trt AS TreatmentName, pt.TrtValue
                            FROM Patient_OtherTrt ot
                            INNER JOIN Patient_Trts pt ON ot.OtherTrtID = pt.OtherTrtID
                        ) AS AllTreatments
                        GROUP BY TreatmentName
                        ORDER BY TotalValue DESC
                        "
            Dim dt As New DataTable()
            Using cmd As New SqlCommand(sql, conn)
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
            chartMostExpensive.DataSource = dt
            chartMostExpensive.Series(0).ArgumentDataMember = "TreatmentName"
            chartMostExpensive.Series(0).ValueDataMembers.AddRange("TotalValue")
        End Using
    End Sub
    Private Sub LoadMostTreatedTeeth()
        ConfigureBarChart(chartMostTreatedTeeth, "Most Treated Teeth", "الأسنان الأكثر معالجة")
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql = "
            SELECT TOP 10 ToothName AS Tooth, COUNT(*) AS TreatmentCount
            FROM Patient_ToothTrt
            GROUP BY ToothName
            ORDER BY TreatmentCount DESC"
            Dim dt As New DataTable()
            Using cmd As New SqlCommand(sql, conn)
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
            chartMostTreatedTeeth.DataSource = dt
            chartMostTreatedTeeth.Series(0).ArgumentDataMember = "Tooth"
            chartMostTreatedTeeth.Series(0).ValueDataMembers.AddRange("TreatmentCount")
        End Using
    End Sub


End Class


'Dim filter As New DashboardFilter() With {
'.DateFrom = Date.Today.AddDays(-90),
'.DateTo = Date.Today}


'Private Sub LoadTreatmentData1()
'    Dim filter As New DashboardFilter() With {
'            .DateFrom = Date.Today.AddDays(-90),
'            .DateTo = Date.Today
'        }
'    ' Load active treatments
'    Dim treatments = _dbHelper.GetTreatments(filter)
'    gridControlActiveTreatments.DataSource = treatments
'    ' Load tooth treatments for tree
'    LoadToothTreatments()
'    ' Load treatment type distribution
'    LoadTreatmentTypeChart()
'End Sub


'Dim sql1 As String = "
'                SELECT TOP 10 TreatmentName, COUNT(*) AS TreatmentCount
'                FROM
'                (
'                    -- Tooth Treatments
'                    SELECT t.Treat AS TreatmentName
'                    FROM Patient_ToothTrt t
'                    INNER JOIN Patient_Trts pt ON t.ToothTrtID = pt.ToothTrtID

'                    UNION ALL

'                    -- Orthodontics
'                    SELECT o.Khota AS TreatmentName
'                    FROM OrthoInf o
'                    INNER JOIN Patient_Trts pt ON o.OrthoID = pt.OrthoID

'                    UNION ALL

'                    -- Other Treatments
'                    SELECT ot.Trt AS TreatmentName
'                    FROM Patient_OtherTrt ot
'                    INNER JOIN Patient_Trts pt ON ot.OtherTrtID = pt.OtherTrtID
'                ) AS AllTreatments
'                GROUP BY TreatmentName
'                ORDER BY TreatmentCount DESC"

'chartMostRepeated, chartMostExpensive, chartMostTreatedTeeth