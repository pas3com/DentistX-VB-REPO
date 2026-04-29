<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockDashboardForm
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    Friend WithEvents _lblLow As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _lblExp As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _lblOpen As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _lblValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _tabs As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents _tabOverview As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents _tabCharts As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents _tabTrends As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents _chartSuppliers As DevExpress.XtraCharts.ChartControl
    Friend WithEvents _chartPayments As DevExpress.XtraCharts.ChartControl
    Friend WithEvents _chartExpenses As DevExpress.XtraCharts.ChartControl
    Friend WithEvents _chartTopProducts As DevExpress.XtraCharts.ChartControl
    Friend WithEvents _chartStockTrend As DevExpress.XtraCharts.ChartControl

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StockDashboardForm))
        Me.panelTop = New DevExpress.XtraEditors.PanelControl()
        Me._btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.overviewPanel = New DevExpress.XtraEditors.PanelControl()
        Me.layout = New System.Windows.Forms.TableLayoutPanel()
        Me.panelLow = New DevExpress.XtraEditors.PanelControl()
        Me._lblLow = New DevExpress.XtraEditors.LabelControl()
        Me.titleLow = New DevExpress.XtraEditors.LabelControl()
        Me.panelExp = New DevExpress.XtraEditors.PanelControl()
        Me._lblExp = New DevExpress.XtraEditors.LabelControl()
        Me.titleExp = New DevExpress.XtraEditors.LabelControl()
        Me.panelOpen = New DevExpress.XtraEditors.PanelControl()
        Me._lblOpen = New DevExpress.XtraEditors.LabelControl()
        Me.titleOpen = New DevExpress.XtraEditors.LabelControl()
        Me.panelValue = New DevExpress.XtraEditors.PanelControl()
        Me._lblValue = New DevExpress.XtraEditors.LabelControl()
        Me.titleValue = New DevExpress.XtraEditors.LabelControl()
        Me.chartsLayout = New System.Windows.Forms.TableLayoutPanel()
        Me._chartSuppliers = New DevExpress.XtraCharts.ChartControl()
        Me._chartPayments = New DevExpress.XtraCharts.ChartControl()
        Me._chartExpenses = New DevExpress.XtraCharts.ChartControl()
        Me._chartTopProducts = New DevExpress.XtraCharts.ChartControl()
        Me._tabs = New DevExpress.XtraTab.XtraTabControl()
        Me._tabOverview = New DevExpress.XtraTab.XtraTabPage()
        Me._tabCharts = New DevExpress.XtraTab.XtraTabPage()
        Me._tabTrends = New DevExpress.XtraTab.XtraTabPage()
        Me._chartStockTrend = New DevExpress.XtraCharts.ChartControl()
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelTop.SuspendLayout()
        CType(Me.overviewPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.overviewPanel.SuspendLayout()
        Me.layout.SuspendLayout()
        CType(Me.panelLow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelLow.SuspendLayout()
        CType(Me.panelExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelExp.SuspendLayout()
        CType(Me.panelOpen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelOpen.SuspendLayout()
        CType(Me.panelValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelValue.SuspendLayout()
        Me.chartsLayout.SuspendLayout()
        CType(Me._chartSuppliers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._chartPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._chartExpenses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._chartTopProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._tabs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._tabs.SuspendLayout()
        Me._tabOverview.SuspendLayout()
        Me._tabCharts.SuspendLayout()
        Me._tabTrends.SuspendLayout()
        CType(Me._chartStockTrend, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelTop
        '
        Me.panelTop.Controls.Add(Me._btnRefresh)
        resources.ApplyResources(Me.panelTop, "panelTop")
        Me.panelTop.Name = "panelTop"
        '
        '_btnRefresh
        '
        Me._btnRefresh.Appearance.Font = CType(resources.GetObject("_btnRefresh.Appearance.Font"), System.Drawing.Font)
        Me._btnRefresh.Appearance.Options.UseFont = True
        resources.ApplyResources(Me._btnRefresh, "_btnRefresh")
        Me._btnRefresh.Name = "_btnRefresh"
        '
        'overviewPanel
        '
        Me.overviewPanel.Controls.Add(Me.layout)
        Me.overviewPanel.Controls.Add(Me.panelTop)
        resources.ApplyResources(Me.overviewPanel, "overviewPanel")
        Me.overviewPanel.Name = "overviewPanel"
        '
        'layout
        '
        resources.ApplyResources(Me.layout, "layout")
        Me.layout.Controls.Add(Me.panelLow, 0, 0)
        Me.layout.Controls.Add(Me.panelExp, 1, 0)
        Me.layout.Controls.Add(Me.panelOpen, 2, 0)
        Me.layout.Controls.Add(Me.panelValue, 3, 0)
        Me.layout.Name = "layout"
        '
        'panelLow
        '
        Me.panelLow.Controls.Add(Me._lblLow)
        Me.panelLow.Controls.Add(Me.titleLow)
        resources.ApplyResources(Me.panelLow, "panelLow")
        Me.panelLow.Name = "panelLow"
        '
        '_lblLow
        '
        Me._lblLow.Appearance.Options.UseTextOptions = True
        Me._lblLow.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me._lblLow.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me._lblLow, "_lblLow")
        Me._lblLow.Name = "_lblLow"
        '
        'titleLow
        '
        resources.ApplyResources(Me.titleLow, "titleLow")
        Me.titleLow.Name = "titleLow"
        '
        'panelExp
        '
        Me.panelExp.Controls.Add(Me._lblExp)
        Me.panelExp.Controls.Add(Me.titleExp)
        resources.ApplyResources(Me.panelExp, "panelExp")
        Me.panelExp.Name = "panelExp"
        '
        '_lblExp
        '
        Me._lblExp.Appearance.Options.UseTextOptions = True
        Me._lblExp.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me._lblExp.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me._lblExp, "_lblExp")
        Me._lblExp.Name = "_lblExp"
        '
        'titleExp
        '
        resources.ApplyResources(Me.titleExp, "titleExp")
        Me.titleExp.Name = "titleExp"
        '
        'panelOpen
        '
        Me.panelOpen.Controls.Add(Me._lblOpen)
        Me.panelOpen.Controls.Add(Me.titleOpen)
        resources.ApplyResources(Me.panelOpen, "panelOpen")
        Me.panelOpen.Name = "panelOpen"
        '
        '_lblOpen
        '
        Me._lblOpen.Appearance.Options.UseTextOptions = True
        Me._lblOpen.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me._lblOpen.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me._lblOpen, "_lblOpen")
        Me._lblOpen.Name = "_lblOpen"
        '
        'titleOpen
        '
        resources.ApplyResources(Me.titleOpen, "titleOpen")
        Me.titleOpen.Name = "titleOpen"
        '
        'panelValue
        '
        Me.panelValue.Controls.Add(Me._lblValue)
        Me.panelValue.Controls.Add(Me.titleValue)
        resources.ApplyResources(Me.panelValue, "panelValue")
        Me.panelValue.Name = "panelValue"
        '
        '_lblValue
        '
        Me._lblValue.Appearance.Options.UseTextOptions = True
        Me._lblValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me._lblValue.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me._lblValue, "_lblValue")
        Me._lblValue.Name = "_lblValue"
        '
        'titleValue
        '
        resources.ApplyResources(Me.titleValue, "titleValue")
        Me.titleValue.Name = "titleValue"
        '
        'chartsLayout
        '
        resources.ApplyResources(Me.chartsLayout, "chartsLayout")
        Me.chartsLayout.Controls.Add(Me._chartSuppliers, 0, 0)
        Me.chartsLayout.Controls.Add(Me._chartPayments, 1, 0)
        Me.chartsLayout.Controls.Add(Me._chartExpenses, 0, 1)
        Me.chartsLayout.Controls.Add(Me._chartTopProducts, 1, 1)
        Me.chartsLayout.Name = "chartsLayout"
        '
        '_chartSuppliers
        '
        resources.ApplyResources(Me._chartSuppliers, "_chartSuppliers")
        Me._chartSuppliers.Name = "_chartSuppliers"
        Me._chartSuppliers.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        '
        '_chartPayments
        '
        resources.ApplyResources(Me._chartPayments, "_chartPayments")
        Me._chartPayments.Name = "_chartPayments"
        Me._chartPayments.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        '
        '_chartExpenses
        '
        resources.ApplyResources(Me._chartExpenses, "_chartExpenses")
        Me._chartExpenses.Name = "_chartExpenses"
        Me._chartExpenses.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        '
        '_chartTopProducts
        '
        resources.ApplyResources(Me._chartTopProducts, "_chartTopProducts")
        Me._chartTopProducts.Name = "_chartTopProducts"
        Me._chartTopProducts.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        '
        '_tabs
        '
        Me._tabs.AppearancePage.Header.Font = CType(resources.GetObject("_tabs.AppearancePage.Header.Font"), System.Drawing.Font)
        Me._tabs.AppearancePage.Header.Options.UseFont = True
        resources.ApplyResources(Me._tabs, "_tabs")
        Me._tabs.Name = "_tabs"
        Me._tabs.SelectedTabPage = Me._tabOverview
        Me._tabs.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me._tabOverview, Me._tabCharts, Me._tabTrends})
        '
        '_tabOverview
        '
        Me._tabOverview.Controls.Add(Me.overviewPanel)
        resources.ApplyResources(Me._tabOverview, "_tabOverview")
        Me._tabOverview.Name = "_tabOverview"
        '
        '_tabCharts
        '
        Me._tabCharts.Controls.Add(Me.chartsLayout)
        resources.ApplyResources(Me._tabCharts, "_tabCharts")
        Me._tabCharts.Name = "_tabCharts"
        '
        '_tabTrends
        '
        Me._tabTrends.Controls.Add(Me._chartStockTrend)
        resources.ApplyResources(Me._tabTrends, "_tabTrends")
        Me._tabTrends.Name = "_tabTrends"
        '
        '_chartStockTrend
        '
        resources.ApplyResources(Me._chartStockTrend, "_chartStockTrend")
        Me._chartStockTrend.Name = "_chartStockTrend"
        Me._chartStockTrend.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        '
        'StockDashboardForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me._tabs)
        Me.Name = "StockDashboardForm"
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelTop.ResumeLayout(False)
        CType(Me.overviewPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.overviewPanel.ResumeLayout(False)
        Me.layout.ResumeLayout(False)
        CType(Me.panelLow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelLow.ResumeLayout(False)
        Me.panelLow.PerformLayout()
        CType(Me.panelExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelExp.ResumeLayout(False)
        Me.panelExp.PerformLayout()
        CType(Me.panelOpen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelOpen.ResumeLayout(False)
        Me.panelOpen.PerformLayout()
        CType(Me.panelValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelValue.ResumeLayout(False)
        Me.panelValue.PerformLayout()
        Me.chartsLayout.ResumeLayout(False)
        CType(Me._chartSuppliers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._chartPayments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._chartExpenses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._chartTopProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._tabs, System.ComponentModel.ISupportInitialize).EndInit()
        Me._tabs.ResumeLayout(False)
        Me._tabOverview.ResumeLayout(False)
        Me._tabCharts.ResumeLayout(False)
        Me._tabTrends.ResumeLayout(False)
        CType(Me._chartStockTrend, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelTop As DevExpress.XtraEditors.PanelControl
    Friend WithEvents overviewPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents layout As TableLayoutPanel
    Friend WithEvents panelLow As DevExpress.XtraEditors.PanelControl
    Friend WithEvents titleLow As DevExpress.XtraEditors.LabelControl
    Friend WithEvents panelExp As DevExpress.XtraEditors.PanelControl
    Friend WithEvents titleExp As DevExpress.XtraEditors.LabelControl
    Friend WithEvents panelOpen As DevExpress.XtraEditors.PanelControl
    Friend WithEvents titleOpen As DevExpress.XtraEditors.LabelControl
    Friend WithEvents panelValue As DevExpress.XtraEditors.PanelControl
    Friend WithEvents titleValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chartsLayout As TableLayoutPanel
End Class

