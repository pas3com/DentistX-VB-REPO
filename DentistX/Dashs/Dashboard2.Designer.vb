Namespace Win_Dashboards
    Partial Public Class Dashboard2
        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Component Designer generated code"

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim Dimension1 As DevExpress.DashboardCommon.Dimension = New DevExpress.DashboardCommon.Dimension()
            Dim Measure1 As DevExpress.DashboardCommon.Measure = New DevExpress.DashboardCommon.Measure()
            Dim Measure2 As DevExpress.DashboardCommon.Measure = New DevExpress.DashboardCommon.Measure()
            Dim Measure3 As DevExpress.DashboardCommon.Measure = New DevExpress.DashboardCommon.Measure()
            Dim Measure4 As DevExpress.DashboardCommon.Measure = New DevExpress.DashboardCommon.Measure()
            Dim Dimension2 As DevExpress.DashboardCommon.Dimension = New DevExpress.DashboardCommon.Dimension()
            Dim Measure5 As DevExpress.DashboardCommon.Measure = New DevExpress.DashboardCommon.Measure()
            Dim Dimension3 As DevExpress.DashboardCommon.Dimension = New DevExpress.DashboardCommon.Dimension()
            Dim ChartPane1 As DevExpress.DashboardCommon.ChartPane = New DevExpress.DashboardCommon.ChartPane()
            Dim SimpleSeries1 As DevExpress.DashboardCommon.SimpleSeries = New DevExpress.DashboardCommon.SimpleSeries()
            Dim Dimension4 As DevExpress.DashboardCommon.Dimension = New DevExpress.DashboardCommon.Dimension()
            Dim GridDimensionColumn1 As DevExpress.DashboardCommon.GridDimensionColumn = New DevExpress.DashboardCommon.GridDimensionColumn()
            Dim Measure6 As DevExpress.DashboardCommon.Measure = New DevExpress.DashboardCommon.Measure()
            Dim GridMeasureColumn1 As DevExpress.DashboardCommon.GridMeasureColumn = New DevExpress.DashboardCommon.GridMeasureColumn()
            Dim Dimension5 As DevExpress.DashboardCommon.Dimension = New DevExpress.DashboardCommon.Dimension()
            Dim Measure7 As DevExpress.DashboardCommon.Measure = New DevExpress.DashboardCommon.Measure()
            Dim DashboardLayoutGroup1 As DevExpress.DashboardCommon.DashboardLayoutGroup = New DevExpress.DashboardCommon.DashboardLayoutGroup()
            Dim DashboardLayoutItem1 As DevExpress.DashboardCommon.DashboardLayoutItem = New DevExpress.DashboardCommon.DashboardLayoutItem()
            Dim DashboardLayoutGroup2 As DevExpress.DashboardCommon.DashboardLayoutGroup = New DevExpress.DashboardCommon.DashboardLayoutGroup()
            Dim DashboardLayoutItem2 As DevExpress.DashboardCommon.DashboardLayoutItem = New DevExpress.DashboardCommon.DashboardLayoutItem()
            Dim DashboardLayoutItem3 As DevExpress.DashboardCommon.DashboardLayoutItem = New DevExpress.DashboardCommon.DashboardLayoutItem()
            Me.PieDashboardItem1 = New DevExpress.DashboardCommon.PieDashboardItem()
            Me.DashboardObjectDataSource1 = New DevExpress.DashboardCommon.DashboardObjectDataSource()
            Me.MainDS1 = New DentistX.MainDS()
            Me.PatientTableAdapter1 = New DentistX.MainDSTableAdapters.PatientTableAdapter()
            Me.ChartDashboardItem1 = New DevExpress.DashboardCommon.ChartDashboardItem()
            Me.GridDashboardItem1 = New DevExpress.DashboardCommon.GridDashboardItem()
            CType(Me.PieDashboardItem1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Dimension1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Measure1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Measure2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Measure3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Measure4, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DashboardObjectDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.MainDS1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ChartDashboardItem1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Dimension2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Measure5, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Dimension3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GridDashboardItem1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Dimension4, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Measure6, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Dimension5, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Measure7, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'PieDashboardItem1
            '
            Dimension1.DataMember = "Sex"
            Me.PieDashboardItem1.Arguments.AddRange(New DevExpress.DashboardCommon.Dimension() {Dimension1})
            Me.PieDashboardItem1.ComponentName = "PieDashboardItem1"
            Measure1.DataMember = "PatientID"
            Measure1.SummaryType = DevExpress.DashboardCommon.SummaryType.Count
            Measure2.DataMember = "Treat"
            Measure2.Name = "Treat"
            Measure2.SummaryType = DevExpress.DashboardCommon.SummaryType.Count
            Measure3.DataMember = "Ortho"
            Measure3.SummaryType = DevExpress.DashboardCommon.SummaryType.Count
            Measure4.DataMember = "Mobile"
            Measure4.SummaryType = DevExpress.DashboardCommon.SummaryType.Count
            Me.PieDashboardItem1.DataItemRepository.Clear()
            Me.PieDashboardItem1.DataItemRepository.Add(Dimension1, "DataItem0")
            Me.PieDashboardItem1.DataItemRepository.Add(Measure1, "DataItem1")
            Me.PieDashboardItem1.DataItemRepository.Add(Measure2, "DataItem2")
            Me.PieDashboardItem1.DataItemRepository.Add(Measure3, "DataItem3")
            Me.PieDashboardItem1.DataItemRepository.Add(Measure4, "DataItem4")
            Me.PieDashboardItem1.DataSource = Me.DashboardObjectDataSource1
            Me.PieDashboardItem1.HiddenMeasures.AddRange(New DevExpress.DashboardCommon.Measure() {Measure2, Measure3, Measure4})
            Me.PieDashboardItem1.InteractivityOptions.IgnoreMasterFilters = False
            Me.PieDashboardItem1.Name = "Pies 1"
            Me.PieDashboardItem1.Values.AddRange(New DevExpress.DashboardCommon.Measure() {Measure1})
            '
            'DashboardObjectDataSource1
            '
            Me.DashboardObjectDataSource1.ComponentName = "DashboardObjectDataSource1"
            Me.DashboardObjectDataSource1.DataMember = "Patient"
            Me.DashboardObjectDataSource1.DataSource = Me.MainDS1
            Me.DashboardObjectDataSource1.Name = "Object Data Source 1"
            '
            'MainDS1
            '
            Me.MainDS1.DataSetName = "MainDS"
            Me.MainDS1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            '
            'PatientTableAdapter1
            '
            Me.PatientTableAdapter1.ClearBeforeFill = True
            '
            'ChartDashboardItem1
            '
            Dimension2.DataMember = "Address"
            Me.ChartDashboardItem1.Arguments.AddRange(New DevExpress.DashboardCommon.Dimension() {Dimension2})
            Me.ChartDashboardItem1.AxisX.TitleVisible = False
            Me.ChartDashboardItem1.ComponentName = "ChartDashboardItem1"
            Measure5.DataMember = "Sex"
            Measure5.SummaryType = DevExpress.DashboardCommon.SummaryType.Count
            Dimension3.DataMember = "Sex"
            Me.ChartDashboardItem1.DataItemRepository.Clear()
            Me.ChartDashboardItem1.DataItemRepository.Add(Dimension2, "DataItem0")
            Me.ChartDashboardItem1.DataItemRepository.Add(Measure5, "DataItem1")
            Me.ChartDashboardItem1.DataItemRepository.Add(Dimension3, "DataItem2")
            Me.ChartDashboardItem1.DataSource = Me.DashboardObjectDataSource1
            Me.ChartDashboardItem1.InteractivityOptions.IgnoreMasterFilters = False
            Me.ChartDashboardItem1.Name = "Chart 1"
            ChartPane1.Name = "Pane 1"
            ChartPane1.PrimaryAxisY.AlwaysShowZeroLevel = True
            ChartPane1.PrimaryAxisY.ShowGridLines = True
            ChartPane1.PrimaryAxisY.TitleVisible = True
            ChartPane1.SecondaryAxisY.AlwaysShowZeroLevel = True
            ChartPane1.SecondaryAxisY.ShowGridLines = False
            ChartPane1.SecondaryAxisY.TitleVisible = True
            SimpleSeries1.AddDataItem("Value", Measure5)
            ChartPane1.Series.AddRange(New DevExpress.DashboardCommon.ChartSeries() {SimpleSeries1})
            Me.ChartDashboardItem1.Panes.AddRange(New DevExpress.DashboardCommon.ChartPane() {ChartPane1})
            Me.ChartDashboardItem1.SeriesDimensions.AddRange(New DevExpress.DashboardCommon.Dimension() {Dimension3})
            '
            'GridDashboardItem1
            '
            Dimension4.DataMember = "Address"
            GridDimensionColumn1.WidthType = DevExpress.DashboardCommon.GridColumnFixedWidthType.Weight
            GridDimensionColumn1.AddDataItem("Dimension", Dimension4)
            Measure6.DataMember = "Sex"
            Measure6.SummaryType = DevExpress.DashboardCommon.SummaryType.Count
            GridMeasureColumn1.WidthType = DevExpress.DashboardCommon.GridColumnFixedWidthType.Weight
            GridMeasureColumn1.AddDataItem("Measure", Measure6)
            Me.GridDashboardItem1.Columns.AddRange(New DevExpress.DashboardCommon.GridColumnBase() {GridDimensionColumn1, GridMeasureColumn1})
            Me.GridDashboardItem1.ComponentName = "GridDashboardItem1"
            Dimension5.DataMember = "Age"
            Measure7.DataMember = "Treat"
            Measure7.SummaryType = DevExpress.DashboardCommon.SummaryType.Count
            Me.GridDashboardItem1.DataItemRepository.Clear()
            Me.GridDashboardItem1.DataItemRepository.Add(Dimension4, "DataItem0")
            Me.GridDashboardItem1.DataItemRepository.Add(Dimension5, "DataItem1")
            Me.GridDashboardItem1.DataItemRepository.Add(Measure6, "DataItem2")
            Me.GridDashboardItem1.DataItemRepository.Add(Measure7, "DataItem3")
            Me.GridDashboardItem1.DataSource = Me.DashboardObjectDataSource1
            Me.GridDashboardItem1.HiddenMeasures.AddRange(New DevExpress.DashboardCommon.Measure() {Measure7})
            Me.GridDashboardItem1.InteractivityOptions.IgnoreMasterFilters = False
            Me.GridDashboardItem1.Name = "Grid 1"
            Me.GridDashboardItem1.SparklineArgument = Dimension5
            '
            'Dashboard2
            '
            Me.DataSources.AddRange(New DevExpress.DashboardCommon.IDashboardDataSource() {Me.DashboardObjectDataSource1})
            Me.Items.AddRange(New DevExpress.DashboardCommon.DashboardItem() {Me.PieDashboardItem1, Me.ChartDashboardItem1, Me.GridDashboardItem1})
            DashboardLayoutItem1.DashboardItem = Me.PieDashboardItem1
            DashboardLayoutItem1.Weight = 49.943117178612056R
            DashboardLayoutItem2.DashboardItem = Me.ChartDashboardItem1
            DashboardLayoutItem2.Weight = 38.423645320197046R
            DashboardLayoutItem3.DashboardItem = Me.GridDashboardItem1
            DashboardLayoutItem3.Weight = 61.576354679802954R
            DashboardLayoutGroup2.ChildNodes.AddRange(New DevExpress.DashboardCommon.DashboardLayoutNode() {DashboardLayoutItem2, DashboardLayoutItem3})
            DashboardLayoutGroup2.DashboardItem = Nothing
            DashboardLayoutGroup2.Orientation = DevExpress.DashboardCommon.DashboardLayoutGroupOrientation.Vertical
            DashboardLayoutGroup2.Weight = 50.056882821387944R
            DashboardLayoutGroup1.ChildNodes.AddRange(New DevExpress.DashboardCommon.DashboardLayoutNode() {DashboardLayoutItem1, DashboardLayoutGroup2})
            DashboardLayoutGroup1.DashboardItem = Nothing
            DashboardLayoutGroup1.Weight = 100.0R
            Me.LayoutRoot = DashboardLayoutGroup1
            Me.Title.Text = "Dashboard"
            CType(Dimension1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Measure1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Measure2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Measure3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Measure4, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PieDashboardItem1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DashboardObjectDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.MainDS1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Dimension2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Measure5, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Dimension3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ChartDashboardItem1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Dimension4, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Measure6, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Dimension5, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Measure7, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GridDashboardItem1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        Friend WithEvents DashboardObjectDataSource1 As DevExpress.DashboardCommon.DashboardObjectDataSource
        Friend WithEvents MainDS1 As MainDS
        Friend WithEvents PatientTableAdapter1 As MainDSTableAdapters.PatientTableAdapter
        Friend WithEvents PieDashboardItem1 As DevExpress.DashboardCommon.PieDashboardItem
        Friend WithEvents ChartDashboardItem1 As DevExpress.DashboardCommon.ChartDashboardItem
        Friend WithEvents GridDashboardItem1 As DevExpress.DashboardCommon.GridDashboardItem

#End Region
    End Class
End Namespace