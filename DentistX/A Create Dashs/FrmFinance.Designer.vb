<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmFinance
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFinance))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode4 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode5 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode6 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode7 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.splitFinTreat = New DevExpress.XtraEditors.SplitContainerControl()
        Me.pnlTreatHeader = New DevExpress.XtraEditors.PanelControl()
        Me.lblTitleTreat = New DevExpress.XtraEditors.LabelControl()
        Me.btnBackTreat = New DevExpress.XtraEditors.SimpleButton()
        Me.gridFinTreatments = New DevExpress.XtraGrid.GridControl()
        Me.gridViewFinTreatments = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.splitFinExp = New DevExpress.XtraEditors.SplitContainerControl()
        Me.pnlExpHeader = New DevExpress.XtraEditors.PanelControl()
        Me.lblTitleExp = New DevExpress.XtraEditors.LabelControl()
        Me.btnBackExp = New DevExpress.XtraEditors.SimpleButton()
        Me.gridFinExpenses = New DevExpress.XtraGrid.GridControl()
        Me.gridViewFinExpenses = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.splitFinPurch = New DevExpress.XtraEditors.SplitContainerControl()
        Me.pnlPurchHeader = New DevExpress.XtraEditors.PanelControl()
        Me.lblTitlePurch = New DevExpress.XtraEditors.LabelControl()
        Me.btnBackPurch = New DevExpress.XtraEditors.SimpleButton()
        Me.gridFinPurchases = New DevExpress.XtraGrid.GridControl()
        Me.gridViewFinPurchases = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.splitFinLab = New DevExpress.XtraEditors.SplitContainerControl()
        Me.pnlLabHeader = New DevExpress.XtraEditors.PanelControl()
        Me.lblTitleLab = New DevExpress.XtraEditors.LabelControl()
        Me.btnBackLab = New DevExpress.XtraEditors.SimpleButton()
        Me.gridFinLab = New DevExpress.XtraGrid.GridControl()
        Me.gridViewFinLab = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.splitFinStaff = New DevExpress.XtraEditors.SplitContainerControl()
        Me.pnlStaffHeader = New DevExpress.XtraEditors.PanelControl()
        Me.lblTitleStaff = New DevExpress.XtraEditors.LabelControl()
        Me.btnBackStaff = New DevExpress.XtraEditors.SimpleButton()
        Me.gridFinStaff = New DevExpress.XtraGrid.GridControl()
        Me.gridViewFinStaff = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.splitFinPatientPays = New DevExpress.XtraEditors.SplitContainerControl()
        Me.pnlPatPayHeader = New DevExpress.XtraEditors.PanelControl()
        Me.lblTitlePatPay = New DevExpress.XtraEditors.LabelControl()
        Me.btnBackPatPay = New DevExpress.XtraEditors.SimpleButton()
        Me.gridFinPatientPays = New DevExpress.XtraGrid.GridControl()
        Me.gridViewFinPatientPays = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.splitFinSupplierPays = New DevExpress.XtraEditors.SplitContainerControl()
        Me.pnlSupPayHeader = New DevExpress.XtraEditors.PanelControl()
        Me.lblTitleSupPay = New DevExpress.XtraEditors.LabelControl()
        Me.btnBackSupPay = New DevExpress.XtraEditors.SimpleButton()
        Me.gridFinSupplierPays = New DevExpress.XtraGrid.GridControl()
        Me.gridViewFinSupplierPays = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.pnlRoot = New System.Windows.Forms.Panel()
        Me.navFinance = New DevExpress.XtraBars.Navigation.NavigationFrame()
        Me.pageFinOverview = New DevExpress.XtraBars.Navigation.NavigationPage()
        Me.pnlOverview = New DevExpress.XtraEditors.PanelControl()
        Me.flowKpi = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlKpiTreat = New DevExpress.XtraEditors.PanelControl()
        Me.lblKpiTreatGo = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiTreatHint = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiTreatTitle = New DevExpress.XtraEditors.LabelControl()
        Me.pnlKpiPatientPays = New DevExpress.XtraEditors.PanelControl()
        Me.lblKpiPatientPaysGo = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiPatientPaysHint = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiPatientPaysTitle = New DevExpress.XtraEditors.LabelControl()
        Me.pnlKpiExp = New DevExpress.XtraEditors.PanelControl()
        Me.lblKpiExpGo = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiExpHint = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiExpTitle = New DevExpress.XtraEditors.LabelControl()
        Me.pnlKpiPurch = New DevExpress.XtraEditors.PanelControl()
        Me.lblKpiPurchGo = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiPurchHint = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiPurchTitle = New DevExpress.XtraEditors.LabelControl()
        Me.pnlKpiSupplierPays = New DevExpress.XtraEditors.PanelControl()
        Me.lblKpiSupplierPaysGo = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiSupplierPaysHint = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiSupplierPaysTitle = New DevExpress.XtraEditors.LabelControl()
        Me.pnlKpiLab = New DevExpress.XtraEditors.PanelControl()
        Me.lblKpiLabGo = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiLabHint = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiLabTitle = New DevExpress.XtraEditors.LabelControl()
        Me.pnlKpiStaff = New DevExpress.XtraEditors.PanelControl()
        Me.lblKpiStaffGo = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiStaffHint = New DevExpress.XtraEditors.LabelControl()
        Me.lblKpiStaffTitle = New DevExpress.XtraEditors.LabelControl()
        Me.lblOverviewIntro = New DevExpress.XtraEditors.LabelControl()
        Me.pageFinTreatments = New DevExpress.XtraBars.Navigation.NavigationPage()
        Me.pageFinPatientPays = New DevExpress.XtraBars.Navigation.NavigationPage()
        Me.pageFinExpenses = New DevExpress.XtraBars.Navigation.NavigationPage()
        Me.pageFinPurchases = New DevExpress.XtraBars.Navigation.NavigationPage()
        Me.pageFinSupplierPays = New DevExpress.XtraBars.Navigation.NavigationPage()
        Me.pageFinLab = New DevExpress.XtraBars.Navigation.NavigationPage()
        Me.pageFinStaff = New DevExpress.XtraBars.Navigation.NavigationPage()
        Me.pnlFinanceToolbar = New DevExpress.XtraEditors.PanelControl()
        Me.lblHeaderNet = New DevExpress.XtraEditors.LabelControl()
        Me.lblNetCaption = New DevExpress.XtraEditors.LabelControl()
        Me.lblHeaderOutflows = New DevExpress.XtraEditors.LabelControl()
        Me.lblOutflowsCaption = New DevExpress.XtraEditors.LabelControl()
        Me.lblHeaderIncome = New DevExpress.XtraEditors.LabelControl()
        Me.lblIncomeCaption = New DevExpress.XtraEditors.LabelControl()
        Me.btnFinanceRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.dateFinanceTo = New DevExpress.XtraEditors.DateEdit()
        Me.dateFinanceFrom = New DevExpress.XtraEditors.DateEdit()
        Me.lblDateToCaption = New DevExpress.XtraEditors.LabelControl()
        Me.lblDateFromCaption = New DevExpress.XtraEditors.LabelControl()
        CType(Me.splitFinTreat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinTreat.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinTreat.Panel1.SuspendLayout()
        CType(Me.splitFinTreat.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinTreat.Panel2.SuspendLayout()
        Me.splitFinTreat.SuspendLayout()
        CType(Me.pnlTreatHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTreatHeader.SuspendLayout()
        CType(Me.gridFinTreatments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridViewFinTreatments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinExp.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinExp.Panel1.SuspendLayout()
        CType(Me.splitFinExp.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinExp.Panel2.SuspendLayout()
        Me.splitFinExp.SuspendLayout()
        CType(Me.pnlExpHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlExpHeader.SuspendLayout()
        CType(Me.gridFinExpenses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridViewFinExpenses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinPurch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinPurch.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinPurch.Panel1.SuspendLayout()
        CType(Me.splitFinPurch.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinPurch.Panel2.SuspendLayout()
        Me.splitFinPurch.SuspendLayout()
        CType(Me.pnlPurchHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPurchHeader.SuspendLayout()
        CType(Me.gridFinPurchases, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridViewFinPurchases, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinLab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinLab.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinLab.Panel1.SuspendLayout()
        CType(Me.splitFinLab.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinLab.Panel2.SuspendLayout()
        Me.splitFinLab.SuspendLayout()
        CType(Me.pnlLabHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLabHeader.SuspendLayout()
        CType(Me.gridFinLab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridViewFinLab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinStaff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinStaff.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinStaff.Panel1.SuspendLayout()
        CType(Me.splitFinStaff.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinStaff.Panel2.SuspendLayout()
        Me.splitFinStaff.SuspendLayout()
        CType(Me.pnlStaffHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStaffHeader.SuspendLayout()
        CType(Me.gridFinStaff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridViewFinStaff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinPatientPays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinPatientPays.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinPatientPays.Panel1.SuspendLayout()
        CType(Me.splitFinPatientPays.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinPatientPays.Panel2.SuspendLayout()
        Me.splitFinPatientPays.SuspendLayout()
        CType(Me.pnlPatPayHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPatPayHeader.SuspendLayout()
        CType(Me.gridFinPatientPays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridViewFinPatientPays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinSupplierPays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitFinSupplierPays.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinSupplierPays.Panel1.SuspendLayout()
        CType(Me.splitFinSupplierPays.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFinSupplierPays.Panel2.SuspendLayout()
        Me.splitFinSupplierPays.SuspendLayout()
        CType(Me.pnlSupPayHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSupPayHeader.SuspendLayout()
        CType(Me.gridFinSupplierPays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridViewFinSupplierPays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRoot.SuspendLayout()
        CType(Me.navFinance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.navFinance.SuspendLayout()
        Me.pageFinOverview.SuspendLayout()
        CType(Me.pnlOverview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlOverview.SuspendLayout()
        Me.flowKpi.SuspendLayout()
        CType(Me.pnlKpiTreat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlKpiTreat.SuspendLayout()
        CType(Me.pnlKpiPatientPays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlKpiPatientPays.SuspendLayout()
        CType(Me.pnlKpiExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlKpiExp.SuspendLayout()
        CType(Me.pnlKpiPurch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlKpiPurch.SuspendLayout()
        CType(Me.pnlKpiSupplierPays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlKpiSupplierPays.SuspendLayout()
        CType(Me.pnlKpiLab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlKpiLab.SuspendLayout()
        CType(Me.pnlKpiStaff, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlKpiStaff.SuspendLayout()
        Me.pageFinTreatments.SuspendLayout()
        Me.pageFinPatientPays.SuspendLayout()
        Me.pageFinExpenses.SuspendLayout()
        Me.pageFinPurchases.SuspendLayout()
        Me.pageFinSupplierPays.SuspendLayout()
        Me.pageFinLab.SuspendLayout()
        Me.pageFinStaff.SuspendLayout()
        CType(Me.pnlFinanceToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFinanceToolbar.SuspendLayout()
        CType(Me.dateFinanceTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateFinanceTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateFinanceFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateFinanceFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'splitFinTreat
        '
        resources.ApplyResources(Me.splitFinTreat, "splitFinTreat")
        Me.splitFinTreat.Horizontal = False
        Me.splitFinTreat.Name = "splitFinTreat"
        '
        'splitFinTreat.Panel1
        '
        Me.splitFinTreat.Panel1.Controls.Add(Me.pnlTreatHeader)
        resources.ApplyResources(Me.splitFinTreat.Panel1, "splitFinTreat.Panel1")
        '
        'splitFinTreat.Panel2
        '
        Me.splitFinTreat.Panel2.Controls.Add(Me.gridFinTreatments)
        resources.ApplyResources(Me.splitFinTreat.Panel2, "splitFinTreat.Panel2")
        Me.splitFinTreat.SplitterPosition = 52
        '
        'pnlTreatHeader
        '
        Me.pnlTreatHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlTreatHeader.Controls.Add(Me.lblTitleTreat)
        Me.pnlTreatHeader.Controls.Add(Me.btnBackTreat)
        resources.ApplyResources(Me.pnlTreatHeader, "pnlTreatHeader")
        Me.pnlTreatHeader.Name = "pnlTreatHeader"
        '
        'lblTitleTreat
        '
        resources.ApplyResources(Me.lblTitleTreat, "lblTitleTreat")
        Me.lblTitleTreat.Name = "lblTitleTreat"
        '
        'btnBackTreat
        '
        Me.btnBackTreat.Appearance.Font = CType(resources.GetObject("btnBackTreat.Appearance.Font"), System.Drawing.Font)
        Me.btnBackTreat.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBackTreat, "btnBackTreat")
        Me.btnBackTreat.Name = "btnBackTreat"
        '
        'gridFinTreatments
        '
        resources.ApplyResources(Me.gridFinTreatments, "gridFinTreatments")
        GridLevelNode1.RelationName = "Level1"
        Me.gridFinTreatments.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.gridFinTreatments.MainView = Me.gridViewFinTreatments
        Me.gridFinTreatments.Name = "gridFinTreatments"
        Me.gridFinTreatments.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridViewFinTreatments})
        '
        'gridViewFinTreatments
        '
        Me.gridViewFinTreatments.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.gridViewFinTreatments.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.gridViewFinTreatments.GridControl = Me.gridFinTreatments
        Me.gridViewFinTreatments.Name = "gridViewFinTreatments"
        Me.gridViewFinTreatments.OptionsBehavior.Editable = False
        Me.gridViewFinTreatments.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gridViewFinTreatments.OptionsView.ShowFooter = True
        Me.gridViewFinTreatments.OptionsView.ShowGroupPanel = False
        '
        'splitFinExp
        '
        resources.ApplyResources(Me.splitFinExp, "splitFinExp")
        Me.splitFinExp.Horizontal = False
        Me.splitFinExp.Name = "splitFinExp"
        '
        'splitFinExp.Panel1
        '
        Me.splitFinExp.Panel1.Controls.Add(Me.pnlExpHeader)
        resources.ApplyResources(Me.splitFinExp.Panel1, "splitFinExp.Panel1")
        '
        'splitFinExp.Panel2
        '
        Me.splitFinExp.Panel2.Controls.Add(Me.gridFinExpenses)
        resources.ApplyResources(Me.splitFinExp.Panel2, "splitFinExp.Panel2")
        Me.splitFinExp.SplitterPosition = 52
        '
        'pnlExpHeader
        '
        Me.pnlExpHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlExpHeader.Controls.Add(Me.lblTitleExp)
        Me.pnlExpHeader.Controls.Add(Me.btnBackExp)
        resources.ApplyResources(Me.pnlExpHeader, "pnlExpHeader")
        Me.pnlExpHeader.Name = "pnlExpHeader"
        '
        'lblTitleExp
        '
        resources.ApplyResources(Me.lblTitleExp, "lblTitleExp")
        Me.lblTitleExp.Name = "lblTitleExp"
        '
        'btnBackExp
        '
        Me.btnBackExp.Appearance.Font = CType(resources.GetObject("btnBackExp.Appearance.Font"), System.Drawing.Font)
        Me.btnBackExp.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBackExp, "btnBackExp")
        Me.btnBackExp.Name = "btnBackExp"
        '
        'gridFinExpenses
        '
        resources.ApplyResources(Me.gridFinExpenses, "gridFinExpenses")
        GridLevelNode2.RelationName = "Level1"
        Me.gridFinExpenses.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.gridFinExpenses.MainView = Me.gridViewFinExpenses
        Me.gridFinExpenses.Name = "gridFinExpenses"
        Me.gridFinExpenses.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridViewFinExpenses})
        '
        'gridViewFinExpenses
        '
        Me.gridViewFinExpenses.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.gridViewFinExpenses.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.gridViewFinExpenses.GridControl = Me.gridFinExpenses
        Me.gridViewFinExpenses.Name = "gridViewFinExpenses"
        Me.gridViewFinExpenses.OptionsBehavior.Editable = False
        Me.gridViewFinExpenses.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gridViewFinExpenses.OptionsView.ShowFooter = True
        Me.gridViewFinExpenses.OptionsView.ShowGroupPanel = False
        '
        'splitFinPurch
        '
        resources.ApplyResources(Me.splitFinPurch, "splitFinPurch")
        Me.splitFinPurch.Horizontal = False
        Me.splitFinPurch.Name = "splitFinPurch"
        '
        'splitFinPurch.Panel1
        '
        Me.splitFinPurch.Panel1.Controls.Add(Me.pnlPurchHeader)
        resources.ApplyResources(Me.splitFinPurch.Panel1, "splitFinPurch.Panel1")
        '
        'splitFinPurch.Panel2
        '
        Me.splitFinPurch.Panel2.Controls.Add(Me.gridFinPurchases)
        resources.ApplyResources(Me.splitFinPurch.Panel2, "splitFinPurch.Panel2")
        Me.splitFinPurch.SplitterPosition = 52
        '
        'pnlPurchHeader
        '
        Me.pnlPurchHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlPurchHeader.Controls.Add(Me.lblTitlePurch)
        Me.pnlPurchHeader.Controls.Add(Me.btnBackPurch)
        resources.ApplyResources(Me.pnlPurchHeader, "pnlPurchHeader")
        Me.pnlPurchHeader.Name = "pnlPurchHeader"
        '
        'lblTitlePurch
        '
        resources.ApplyResources(Me.lblTitlePurch, "lblTitlePurch")
        Me.lblTitlePurch.Name = "lblTitlePurch"
        '
        'btnBackPurch
        '
        Me.btnBackPurch.Appearance.Font = CType(resources.GetObject("btnBackPurch.Appearance.Font"), System.Drawing.Font)
        Me.btnBackPurch.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBackPurch, "btnBackPurch")
        Me.btnBackPurch.Name = "btnBackPurch"
        '
        'gridFinPurchases
        '
        resources.ApplyResources(Me.gridFinPurchases, "gridFinPurchases")
        GridLevelNode3.RelationName = "Level1"
        Me.gridFinPurchases.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode3})
        Me.gridFinPurchases.MainView = Me.gridViewFinPurchases
        Me.gridFinPurchases.Name = "gridFinPurchases"
        Me.gridFinPurchases.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridViewFinPurchases})
        '
        'gridViewFinPurchases
        '
        Me.gridViewFinPurchases.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.gridViewFinPurchases.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.gridViewFinPurchases.GridControl = Me.gridFinPurchases
        Me.gridViewFinPurchases.Name = "gridViewFinPurchases"
        Me.gridViewFinPurchases.OptionsBehavior.Editable = False
        Me.gridViewFinPurchases.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gridViewFinPurchases.OptionsView.ShowFooter = True
        Me.gridViewFinPurchases.OptionsView.ShowGroupPanel = False
        '
        'splitFinLab
        '
        resources.ApplyResources(Me.splitFinLab, "splitFinLab")
        Me.splitFinLab.Horizontal = False
        Me.splitFinLab.Name = "splitFinLab"
        '
        'splitFinLab.Panel1
        '
        Me.splitFinLab.Panel1.Controls.Add(Me.pnlLabHeader)
        resources.ApplyResources(Me.splitFinLab.Panel1, "splitFinLab.Panel1")
        '
        'splitFinLab.Panel2
        '
        Me.splitFinLab.Panel2.Controls.Add(Me.gridFinLab)
        resources.ApplyResources(Me.splitFinLab.Panel2, "splitFinLab.Panel2")
        Me.splitFinLab.SplitterPosition = 52
        '
        'pnlLabHeader
        '
        Me.pnlLabHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlLabHeader.Controls.Add(Me.lblTitleLab)
        Me.pnlLabHeader.Controls.Add(Me.btnBackLab)
        resources.ApplyResources(Me.pnlLabHeader, "pnlLabHeader")
        Me.pnlLabHeader.Name = "pnlLabHeader"
        '
        'lblTitleLab
        '
        resources.ApplyResources(Me.lblTitleLab, "lblTitleLab")
        Me.lblTitleLab.Name = "lblTitleLab"
        '
        'btnBackLab
        '
        Me.btnBackLab.Appearance.Font = CType(resources.GetObject("btnBackLab.Appearance.Font"), System.Drawing.Font)
        Me.btnBackLab.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBackLab, "btnBackLab")
        Me.btnBackLab.Name = "btnBackLab"
        '
        'gridFinLab
        '
        resources.ApplyResources(Me.gridFinLab, "gridFinLab")
        GridLevelNode4.RelationName = "Level1"
        Me.gridFinLab.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode4})
        Me.gridFinLab.MainView = Me.gridViewFinLab
        Me.gridFinLab.Name = "gridFinLab"
        Me.gridFinLab.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridViewFinLab})
        '
        'gridViewFinLab
        '
        Me.gridViewFinLab.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.gridViewFinLab.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.gridViewFinLab.GridControl = Me.gridFinLab
        Me.gridViewFinLab.Name = "gridViewFinLab"
        Me.gridViewFinLab.OptionsBehavior.Editable = False
        Me.gridViewFinLab.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gridViewFinLab.OptionsView.ShowFooter = True
        Me.gridViewFinLab.OptionsView.ShowGroupPanel = False
        '
        'splitFinStaff
        '
        resources.ApplyResources(Me.splitFinStaff, "splitFinStaff")
        Me.splitFinStaff.Horizontal = False
        Me.splitFinStaff.Name = "splitFinStaff"
        '
        'splitFinStaff.Panel1
        '
        Me.splitFinStaff.Panel1.Controls.Add(Me.pnlStaffHeader)
        resources.ApplyResources(Me.splitFinStaff.Panel1, "splitFinStaff.Panel1")
        '
        'splitFinStaff.Panel2
        '
        Me.splitFinStaff.Panel2.Controls.Add(Me.gridFinStaff)
        resources.ApplyResources(Me.splitFinStaff.Panel2, "splitFinStaff.Panel2")
        Me.splitFinStaff.SplitterPosition = 52
        '
        'pnlStaffHeader
        '
        Me.pnlStaffHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlStaffHeader.Controls.Add(Me.lblTitleStaff)
        Me.pnlStaffHeader.Controls.Add(Me.btnBackStaff)
        resources.ApplyResources(Me.pnlStaffHeader, "pnlStaffHeader")
        Me.pnlStaffHeader.Name = "pnlStaffHeader"
        '
        'lblTitleStaff
        '
        resources.ApplyResources(Me.lblTitleStaff, "lblTitleStaff")
        Me.lblTitleStaff.Name = "lblTitleStaff"
        '
        'btnBackStaff
        '
        Me.btnBackStaff.Appearance.Font = CType(resources.GetObject("btnBackStaff.Appearance.Font"), System.Drawing.Font)
        Me.btnBackStaff.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBackStaff, "btnBackStaff")
        Me.btnBackStaff.Name = "btnBackStaff"
        '
        'gridFinStaff
        '
        resources.ApplyResources(Me.gridFinStaff, "gridFinStaff")
        GridLevelNode5.RelationName = "Level1"
        Me.gridFinStaff.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode5})
        Me.gridFinStaff.MainView = Me.gridViewFinStaff
        Me.gridFinStaff.Name = "gridFinStaff"
        Me.gridFinStaff.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridViewFinStaff})
        '
        'gridViewFinStaff
        '
        Me.gridViewFinStaff.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.gridViewFinStaff.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.gridViewFinStaff.GridControl = Me.gridFinStaff
        Me.gridViewFinStaff.Name = "gridViewFinStaff"
        Me.gridViewFinStaff.OptionsBehavior.Editable = False
        Me.gridViewFinStaff.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gridViewFinStaff.OptionsView.ShowFooter = True
        Me.gridViewFinStaff.OptionsView.ShowGroupPanel = False
        '
        'splitFinPatientPays
        '
        resources.ApplyResources(Me.splitFinPatientPays, "splitFinPatientPays")
        Me.splitFinPatientPays.Horizontal = False
        Me.splitFinPatientPays.Name = "splitFinPatientPays"
        '
        'splitFinPatientPays.Panel1
        '
        Me.splitFinPatientPays.Panel1.Controls.Add(Me.pnlPatPayHeader)
        '
        'splitFinPatientPays.Panel2
        '
        Me.splitFinPatientPays.Panel2.Controls.Add(Me.gridFinPatientPays)
        Me.splitFinPatientPays.SplitterPosition = 52
        '
        'pnlPatPayHeader
        '
        Me.pnlPatPayHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlPatPayHeader.Controls.Add(Me.lblTitlePatPay)
        Me.pnlPatPayHeader.Controls.Add(Me.btnBackPatPay)
        resources.ApplyResources(Me.pnlPatPayHeader, "pnlPatPayHeader")
        Me.pnlPatPayHeader.Name = "pnlPatPayHeader"
        '
        'lblTitlePatPay
        '
        resources.ApplyResources(Me.lblTitlePatPay, "lblTitlePatPay")
        Me.lblTitlePatPay.Name = "lblTitlePatPay"
        '
        'btnBackPatPay
        '
        Me.btnBackPatPay.Appearance.Font = CType(resources.GetObject("btnBackPatPay.Appearance.Font"), System.Drawing.Font)
        Me.btnBackPatPay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBackPatPay, "btnBackPatPay")
        Me.btnBackPatPay.Name = "btnBackPatPay"
        '
        'gridFinPatientPays
        '
        resources.ApplyResources(Me.gridFinPatientPays, "gridFinPatientPays")
        GridLevelNode6.RelationName = "Level1"
        Me.gridFinPatientPays.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode6})
        Me.gridFinPatientPays.MainView = Me.gridViewFinPatientPays
        Me.gridFinPatientPays.Name = "gridFinPatientPays"
        Me.gridFinPatientPays.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridViewFinPatientPays})
        '
        'gridViewFinPatientPays
        '
        Me.gridViewFinPatientPays.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.gridViewFinPatientPays.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.gridViewFinPatientPays.GridControl = Me.gridFinPatientPays
        Me.gridViewFinPatientPays.Name = "gridViewFinPatientPays"
        Me.gridViewFinPatientPays.OptionsBehavior.Editable = False
        Me.gridViewFinPatientPays.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gridViewFinPatientPays.OptionsView.ShowFooter = True
        Me.gridViewFinPatientPays.OptionsView.ShowGroupPanel = False
        '
        'splitFinSupplierPays
        '
        resources.ApplyResources(Me.splitFinSupplierPays, "splitFinSupplierPays")
        Me.splitFinSupplierPays.Horizontal = False
        Me.splitFinSupplierPays.Name = "splitFinSupplierPays"
        '
        'splitFinSupplierPays.Panel1
        '
        Me.splitFinSupplierPays.Panel1.Controls.Add(Me.pnlSupPayHeader)
        '
        'splitFinSupplierPays.Panel2
        '
        Me.splitFinSupplierPays.Panel2.Controls.Add(Me.gridFinSupplierPays)
        Me.splitFinSupplierPays.SplitterPosition = 52
        '
        'pnlSupPayHeader
        '
        Me.pnlSupPayHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlSupPayHeader.Controls.Add(Me.lblTitleSupPay)
        Me.pnlSupPayHeader.Controls.Add(Me.btnBackSupPay)
        resources.ApplyResources(Me.pnlSupPayHeader, "pnlSupPayHeader")
        Me.pnlSupPayHeader.Name = "pnlSupPayHeader"
        '
        'lblTitleSupPay
        '
        resources.ApplyResources(Me.lblTitleSupPay, "lblTitleSupPay")
        Me.lblTitleSupPay.Name = "lblTitleSupPay"
        '
        'btnBackSupPay
        '
        Me.btnBackSupPay.Appearance.Font = CType(resources.GetObject("btnBackSupPay.Appearance.Font"), System.Drawing.Font)
        Me.btnBackSupPay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBackSupPay, "btnBackSupPay")
        Me.btnBackSupPay.Name = "btnBackSupPay"
        '
        'gridFinSupplierPays
        '
        resources.ApplyResources(Me.gridFinSupplierPays, "gridFinSupplierPays")
        GridLevelNode7.RelationName = "Level1"
        Me.gridFinSupplierPays.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode7})
        Me.gridFinSupplierPays.MainView = Me.gridViewFinSupplierPays
        Me.gridFinSupplierPays.Name = "gridFinSupplierPays"
        Me.gridFinSupplierPays.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridViewFinSupplierPays})
        '
        'gridViewFinSupplierPays
        '
        Me.gridViewFinSupplierPays.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.gridViewFinSupplierPays.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.gridViewFinSupplierPays.GridControl = Me.gridFinSupplierPays
        Me.gridViewFinSupplierPays.Name = "gridViewFinSupplierPays"
        Me.gridViewFinSupplierPays.OptionsBehavior.Editable = False
        Me.gridViewFinSupplierPays.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gridViewFinSupplierPays.OptionsView.ShowFooter = True
        Me.gridViewFinSupplierPays.OptionsView.ShowGroupPanel = False
        '
        'pnlRoot
        '
        Me.pnlRoot.Controls.Add(Me.navFinance)
        Me.pnlRoot.Controls.Add(Me.pnlFinanceToolbar)
        resources.ApplyResources(Me.pnlRoot, "pnlRoot")
        Me.pnlRoot.Name = "pnlRoot"
        '
        'navFinance
        '
        Me.navFinance.Controls.Add(Me.pageFinOverview)
        Me.navFinance.Controls.Add(Me.pageFinTreatments)
        Me.navFinance.Controls.Add(Me.pageFinPatientPays)
        Me.navFinance.Controls.Add(Me.pageFinExpenses)
        Me.navFinance.Controls.Add(Me.pageFinPurchases)
        Me.navFinance.Controls.Add(Me.pageFinSupplierPays)
        Me.navFinance.Controls.Add(Me.pageFinLab)
        Me.navFinance.Controls.Add(Me.pageFinStaff)
        resources.ApplyResources(Me.navFinance, "navFinance")
        Me.navFinance.Name = "navFinance"
        Me.navFinance.Pages.AddRange(New DevExpress.XtraBars.Navigation.NavigationPageBase() {Me.pageFinOverview, Me.pageFinTreatments, Me.pageFinPatientPays, Me.pageFinExpenses, Me.pageFinPurchases, Me.pageFinSupplierPays, Me.pageFinLab, Me.pageFinStaff})
        Me.navFinance.SelectedPage = Me.pageFinOverview
        '
        'pageFinOverview
        '
        resources.ApplyResources(Me.pageFinOverview, "pageFinOverview")
        Me.pageFinOverview.Controls.Add(Me.pnlOverview)
        Me.pageFinOverview.Name = "pageFinOverview"
        '
        'pnlOverview
        '
        Me.pnlOverview.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlOverview.Controls.Add(Me.flowKpi)
        Me.pnlOverview.Controls.Add(Me.lblOverviewIntro)
        resources.ApplyResources(Me.pnlOverview, "pnlOverview")
        Me.pnlOverview.Name = "pnlOverview"
        '
        'flowKpi
        '
        Me.flowKpi.BackColor = System.Drawing.Color.Transparent
        Me.flowKpi.Controls.Add(Me.pnlKpiTreat)
        Me.flowKpi.Controls.Add(Me.pnlKpiPatientPays)
        Me.flowKpi.Controls.Add(Me.pnlKpiExp)
        Me.flowKpi.Controls.Add(Me.pnlKpiPurch)
        Me.flowKpi.Controls.Add(Me.pnlKpiSupplierPays)
        Me.flowKpi.Controls.Add(Me.pnlKpiLab)
        Me.flowKpi.Controls.Add(Me.pnlKpiStaff)
        resources.ApplyResources(Me.flowKpi, "flowKpi")
        Me.flowKpi.Name = "flowKpi"
        '
        'pnlKpiTreat
        '
        Me.pnlKpiTreat.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.pnlKpiTreat.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(107, Byte), Integer))
        Me.pnlKpiTreat.Appearance.Options.UseBackColor = True
        Me.pnlKpiTreat.Appearance.Options.UseBorderColor = True
        Me.pnlKpiTreat.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.pnlKpiTreat.Controls.Add(Me.lblKpiTreatGo)
        Me.pnlKpiTreat.Controls.Add(Me.lblKpiTreatHint)
        Me.pnlKpiTreat.Controls.Add(Me.lblKpiTreatTitle)
        Me.pnlKpiTreat.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pnlKpiTreat, "pnlKpiTreat")
        Me.pnlKpiTreat.Name = "pnlKpiTreat"
        '
        'lblKpiTreatGo
        '
        Me.lblKpiTreatGo.Appearance.Font = CType(resources.GetObject("lblKpiTreatGo.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiTreatGo.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(107, Byte), Integer))
        Me.lblKpiTreatGo.Appearance.Options.UseFont = True
        Me.lblKpiTreatGo.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiTreatGo, "lblKpiTreatGo")
        Me.lblKpiTreatGo.Name = "lblKpiTreatGo"
        '
        'lblKpiTreatHint
        '
        Me.lblKpiTreatHint.Appearance.Font = CType(resources.GetObject("lblKpiTreatHint.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiTreatHint.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.lblKpiTreatHint.Appearance.Options.UseFont = True
        Me.lblKpiTreatHint.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiTreatHint, "lblKpiTreatHint")
        Me.lblKpiTreatHint.Name = "lblKpiTreatHint"
        '
        'lblKpiTreatTitle
        '
        Me.lblKpiTreatTitle.Appearance.Font = CType(resources.GetObject("lblKpiTreatTitle.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiTreatTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.lblKpiTreatTitle.Appearance.Options.UseFont = True
        Me.lblKpiTreatTitle.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiTreatTitle, "lblKpiTreatTitle")
        Me.lblKpiTreatTitle.Name = "lblKpiTreatTitle"
        '
        'pnlKpiPatientPays
        '
        Me.pnlKpiPatientPays.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlKpiPatientPays.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.pnlKpiPatientPays.Appearance.Options.UseBackColor = True
        Me.pnlKpiPatientPays.Appearance.Options.UseBorderColor = True
        Me.pnlKpiPatientPays.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.pnlKpiPatientPays.Controls.Add(Me.lblKpiPatientPaysGo)
        Me.pnlKpiPatientPays.Controls.Add(Me.lblKpiPatientPaysHint)
        Me.pnlKpiPatientPays.Controls.Add(Me.lblKpiPatientPaysTitle)
        Me.pnlKpiPatientPays.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pnlKpiPatientPays, "pnlKpiPatientPays")
        Me.pnlKpiPatientPays.Name = "pnlKpiPatientPays"
        '
        'lblKpiPatientPaysGo
        '
        Me.lblKpiPatientPaysGo.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.lblKpiPatientPaysGo.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiPatientPaysGo, "lblKpiPatientPaysGo")
        Me.lblKpiPatientPaysGo.Name = "lblKpiPatientPaysGo"
        '
        'lblKpiPatientPaysHint
        '
        Me.lblKpiPatientPaysHint.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.lblKpiPatientPaysHint.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiPatientPaysHint, "lblKpiPatientPaysHint")
        Me.lblKpiPatientPaysHint.Name = "lblKpiPatientPaysHint"
        '
        'lblKpiPatientPaysTitle
        '
        Me.lblKpiPatientPaysTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.lblKpiPatientPaysTitle.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiPatientPaysTitle, "lblKpiPatientPaysTitle")
        Me.lblKpiPatientPaysTitle.Name = "lblKpiPatientPaysTitle"
        '
        'pnlKpiExp
        '
        Me.pnlKpiExp.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.pnlKpiExp.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.pnlKpiExp.Appearance.Options.UseBackColor = True
        Me.pnlKpiExp.Appearance.Options.UseBorderColor = True
        Me.pnlKpiExp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.pnlKpiExp.Controls.Add(Me.lblKpiExpGo)
        Me.pnlKpiExp.Controls.Add(Me.lblKpiExpHint)
        Me.pnlKpiExp.Controls.Add(Me.lblKpiExpTitle)
        Me.pnlKpiExp.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pnlKpiExp, "pnlKpiExp")
        Me.pnlKpiExp.Name = "pnlKpiExp"
        '
        'lblKpiExpGo
        '
        Me.lblKpiExpGo.Appearance.Font = CType(resources.GetObject("lblKpiExpGo.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiExpGo.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lblKpiExpGo.Appearance.Options.UseFont = True
        Me.lblKpiExpGo.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiExpGo, "lblKpiExpGo")
        Me.lblKpiExpGo.Name = "lblKpiExpGo"
        '
        'lblKpiExpHint
        '
        Me.lblKpiExpHint.Appearance.Font = CType(resources.GetObject("lblKpiExpHint.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiExpHint.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.lblKpiExpHint.Appearance.Options.UseFont = True
        Me.lblKpiExpHint.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiExpHint, "lblKpiExpHint")
        Me.lblKpiExpHint.Name = "lblKpiExpHint"
        '
        'lblKpiExpTitle
        '
        Me.lblKpiExpTitle.Appearance.Font = CType(resources.GetObject("lblKpiExpTitle.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiExpTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.lblKpiExpTitle.Appearance.Options.UseFont = True
        Me.lblKpiExpTitle.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiExpTitle, "lblKpiExpTitle")
        Me.lblKpiExpTitle.Name = "lblKpiExpTitle"
        '
        'pnlKpiPurch
        '
        Me.pnlKpiPurch.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlKpiPurch.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.pnlKpiPurch.Appearance.Options.UseBackColor = True
        Me.pnlKpiPurch.Appearance.Options.UseBorderColor = True
        Me.pnlKpiPurch.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.pnlKpiPurch.Controls.Add(Me.lblKpiPurchGo)
        Me.pnlKpiPurch.Controls.Add(Me.lblKpiPurchHint)
        Me.pnlKpiPurch.Controls.Add(Me.lblKpiPurchTitle)
        Me.pnlKpiPurch.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pnlKpiPurch, "pnlKpiPurch")
        Me.pnlKpiPurch.Name = "pnlKpiPurch"
        '
        'lblKpiPurchGo
        '
        Me.lblKpiPurchGo.Appearance.Font = CType(resources.GetObject("lblKpiPurchGo.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiPurchGo.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblKpiPurchGo.Appearance.Options.UseFont = True
        Me.lblKpiPurchGo.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiPurchGo, "lblKpiPurchGo")
        Me.lblKpiPurchGo.Name = "lblKpiPurchGo"
        '
        'lblKpiPurchHint
        '
        Me.lblKpiPurchHint.Appearance.Font = CType(resources.GetObject("lblKpiPurchHint.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiPurchHint.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.lblKpiPurchHint.Appearance.Options.UseFont = True
        Me.lblKpiPurchHint.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiPurchHint, "lblKpiPurchHint")
        Me.lblKpiPurchHint.Name = "lblKpiPurchHint"
        '
        'lblKpiPurchTitle
        '
        Me.lblKpiPurchTitle.Appearance.Font = CType(resources.GetObject("lblKpiPurchTitle.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiPurchTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.lblKpiPurchTitle.Appearance.Options.UseFont = True
        Me.lblKpiPurchTitle.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiPurchTitle, "lblKpiPurchTitle")
        Me.lblKpiPurchTitle.Name = "lblKpiPurchTitle"
        '
        'pnlKpiSupplierPays
        '
        Me.pnlKpiSupplierPays.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.pnlKpiSupplierPays.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.pnlKpiSupplierPays.Appearance.Options.UseBackColor = True
        Me.pnlKpiSupplierPays.Appearance.Options.UseBorderColor = True
        Me.pnlKpiSupplierPays.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.pnlKpiSupplierPays.Controls.Add(Me.lblKpiSupplierPaysGo)
        Me.pnlKpiSupplierPays.Controls.Add(Me.lblKpiSupplierPaysHint)
        Me.pnlKpiSupplierPays.Controls.Add(Me.lblKpiSupplierPaysTitle)
        Me.pnlKpiSupplierPays.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pnlKpiSupplierPays, "pnlKpiSupplierPays")
        Me.pnlKpiSupplierPays.Name = "pnlKpiSupplierPays"
        '
        'lblKpiSupplierPaysGo
        '
        Me.lblKpiSupplierPaysGo.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.lblKpiSupplierPaysGo.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiSupplierPaysGo, "lblKpiSupplierPaysGo")
        Me.lblKpiSupplierPaysGo.Name = "lblKpiSupplierPaysGo"
        '
        'lblKpiSupplierPaysHint
        '
        Me.lblKpiSupplierPaysHint.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.lblKpiSupplierPaysHint.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiSupplierPaysHint, "lblKpiSupplierPaysHint")
        Me.lblKpiSupplierPaysHint.Name = "lblKpiSupplierPaysHint"
        '
        'lblKpiSupplierPaysTitle
        '
        Me.lblKpiSupplierPaysTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.lblKpiSupplierPaysTitle.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiSupplierPaysTitle, "lblKpiSupplierPaysTitle")
        Me.lblKpiSupplierPaysTitle.Name = "lblKpiSupplierPaysTitle"
        '
        'pnlKpiLab
        '
        Me.pnlKpiLab.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlKpiLab.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.pnlKpiLab.Appearance.Options.UseBackColor = True
        Me.pnlKpiLab.Appearance.Options.UseBorderColor = True
        Me.pnlKpiLab.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.pnlKpiLab.Controls.Add(Me.lblKpiLabGo)
        Me.pnlKpiLab.Controls.Add(Me.lblKpiLabHint)
        Me.pnlKpiLab.Controls.Add(Me.lblKpiLabTitle)
        Me.pnlKpiLab.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pnlKpiLab, "pnlKpiLab")
        Me.pnlKpiLab.Name = "pnlKpiLab"
        '
        'lblKpiLabGo
        '
        Me.lblKpiLabGo.Appearance.Font = CType(resources.GetObject("lblKpiLabGo.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiLabGo.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lblKpiLabGo.Appearance.Options.UseFont = True
        Me.lblKpiLabGo.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiLabGo, "lblKpiLabGo")
        Me.lblKpiLabGo.Name = "lblKpiLabGo"
        '
        'lblKpiLabHint
        '
        Me.lblKpiLabHint.Appearance.Font = CType(resources.GetObject("lblKpiLabHint.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiLabHint.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.lblKpiLabHint.Appearance.Options.UseFont = True
        Me.lblKpiLabHint.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiLabHint, "lblKpiLabHint")
        Me.lblKpiLabHint.Name = "lblKpiLabHint"
        '
        'lblKpiLabTitle
        '
        Me.lblKpiLabTitle.Appearance.Font = CType(resources.GetObject("lblKpiLabTitle.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiLabTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.lblKpiLabTitle.Appearance.Options.UseFont = True
        Me.lblKpiLabTitle.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiLabTitle, "lblKpiLabTitle")
        Me.lblKpiLabTitle.Name = "lblKpiLabTitle"
        '
        'pnlKpiStaff
        '
        Me.pnlKpiStaff.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.pnlKpiStaff.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.pnlKpiStaff.Appearance.Options.UseBackColor = True
        Me.pnlKpiStaff.Appearance.Options.UseBorderColor = True
        Me.pnlKpiStaff.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.pnlKpiStaff.Controls.Add(Me.lblKpiStaffGo)
        Me.pnlKpiStaff.Controls.Add(Me.lblKpiStaffHint)
        Me.pnlKpiStaff.Controls.Add(Me.lblKpiStaffTitle)
        Me.pnlKpiStaff.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pnlKpiStaff, "pnlKpiStaff")
        Me.pnlKpiStaff.Name = "pnlKpiStaff"
        '
        'lblKpiStaffGo
        '
        Me.lblKpiStaffGo.Appearance.Font = CType(resources.GetObject("lblKpiStaffGo.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiStaffGo.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.lblKpiStaffGo.Appearance.Options.UseFont = True
        Me.lblKpiStaffGo.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiStaffGo, "lblKpiStaffGo")
        Me.lblKpiStaffGo.Name = "lblKpiStaffGo"
        '
        'lblKpiStaffHint
        '
        Me.lblKpiStaffHint.Appearance.Font = CType(resources.GetObject("lblKpiStaffHint.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiStaffHint.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.lblKpiStaffHint.Appearance.Options.UseFont = True
        Me.lblKpiStaffHint.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiStaffHint, "lblKpiStaffHint")
        Me.lblKpiStaffHint.Name = "lblKpiStaffHint"
        '
        'lblKpiStaffTitle
        '
        Me.lblKpiStaffTitle.Appearance.Font = CType(resources.GetObject("lblKpiStaffTitle.Appearance.Font"), System.Drawing.Font)
        Me.lblKpiStaffTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.lblKpiStaffTitle.Appearance.Options.UseFont = True
        Me.lblKpiStaffTitle.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblKpiStaffTitle, "lblKpiStaffTitle")
        Me.lblKpiStaffTitle.Name = "lblKpiStaffTitle"
        '
        'lblOverviewIntro
        '
        resources.ApplyResources(Me.lblOverviewIntro, "lblOverviewIntro")
        Me.lblOverviewIntro.Name = "lblOverviewIntro"
        '
        'pageFinTreatments
        '
        resources.ApplyResources(Me.pageFinTreatments, "pageFinTreatments")
        Me.pageFinTreatments.Controls.Add(Me.splitFinTreat)
        Me.pageFinTreatments.Name = "pageFinTreatments"
        '
        'pageFinPatientPays
        '
        resources.ApplyResources(Me.pageFinPatientPays, "pageFinPatientPays")
        Me.pageFinPatientPays.Controls.Add(Me.splitFinPatientPays)
        Me.pageFinPatientPays.Name = "pageFinPatientPays"
        '
        'pageFinExpenses
        '
        resources.ApplyResources(Me.pageFinExpenses, "pageFinExpenses")
        Me.pageFinExpenses.Controls.Add(Me.splitFinExp)
        Me.pageFinExpenses.Name = "pageFinExpenses"
        '
        'pageFinPurchases
        '
        resources.ApplyResources(Me.pageFinPurchases, "pageFinPurchases")
        Me.pageFinPurchases.Controls.Add(Me.splitFinPurch)
        Me.pageFinPurchases.Name = "pageFinPurchases"
        '
        'pageFinSupplierPays
        '
        resources.ApplyResources(Me.pageFinSupplierPays, "pageFinSupplierPays")
        Me.pageFinSupplierPays.Controls.Add(Me.splitFinSupplierPays)
        Me.pageFinSupplierPays.Name = "pageFinSupplierPays"
        '
        'pageFinLab
        '
        resources.ApplyResources(Me.pageFinLab, "pageFinLab")
        Me.pageFinLab.Controls.Add(Me.splitFinLab)
        Me.pageFinLab.Name = "pageFinLab"
        '
        'pageFinStaff
        '
        resources.ApplyResources(Me.pageFinStaff, "pageFinStaff")
        Me.pageFinStaff.Controls.Add(Me.splitFinStaff)
        Me.pageFinStaff.Name = "pageFinStaff"
        '
        'pnlFinanceToolbar
        '
        Me.pnlFinanceToolbar.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlFinanceToolbar.Appearance.Options.UseBackColor = True
        Me.pnlFinanceToolbar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlFinanceToolbar.Controls.Add(Me.lblHeaderNet)
        Me.pnlFinanceToolbar.Controls.Add(Me.lblNetCaption)
        Me.pnlFinanceToolbar.Controls.Add(Me.lblHeaderOutflows)
        Me.pnlFinanceToolbar.Controls.Add(Me.lblOutflowsCaption)
        Me.pnlFinanceToolbar.Controls.Add(Me.lblHeaderIncome)
        Me.pnlFinanceToolbar.Controls.Add(Me.lblIncomeCaption)
        Me.pnlFinanceToolbar.Controls.Add(Me.btnFinanceRefresh)
        Me.pnlFinanceToolbar.Controls.Add(Me.dateFinanceTo)
        Me.pnlFinanceToolbar.Controls.Add(Me.dateFinanceFrom)
        Me.pnlFinanceToolbar.Controls.Add(Me.lblDateToCaption)
        Me.pnlFinanceToolbar.Controls.Add(Me.lblDateFromCaption)
        resources.ApplyResources(Me.pnlFinanceToolbar, "pnlFinanceToolbar")
        Me.pnlFinanceToolbar.Name = "pnlFinanceToolbar"
        '
        'lblHeaderNet
        '
        Me.lblHeaderNet.Appearance.Font = CType(resources.GetObject("lblHeaderNet.Appearance.Font"), System.Drawing.Font)
        Me.lblHeaderNet.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblHeaderNet.Appearance.Options.UseFont = True
        Me.lblHeaderNet.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblHeaderNet, "lblHeaderNet")
        Me.lblHeaderNet.Name = "lblHeaderNet"
        '
        'lblNetCaption
        '
        Me.lblNetCaption.Appearance.Font = CType(resources.GetObject("lblNetCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblNetCaption.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.lblNetCaption.Appearance.Options.UseFont = True
        Me.lblNetCaption.Appearance.Options.UseForeColor = True
        Me.lblNetCaption.Appearance.Options.UseTextOptions = True
        Me.lblNetCaption.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        resources.ApplyResources(Me.lblNetCaption, "lblNetCaption")
        Me.lblNetCaption.Name = "lblNetCaption"
        '
        'lblHeaderOutflows
        '
        Me.lblHeaderOutflows.Appearance.Font = CType(resources.GetObject("lblHeaderOutflows.Appearance.Font"), System.Drawing.Font)
        Me.lblHeaderOutflows.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblHeaderOutflows.Appearance.Options.UseFont = True
        Me.lblHeaderOutflows.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblHeaderOutflows, "lblHeaderOutflows")
        Me.lblHeaderOutflows.Name = "lblHeaderOutflows"
        '
        'lblOutflowsCaption
        '
        Me.lblOutflowsCaption.Appearance.Font = CType(resources.GetObject("lblOutflowsCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblOutflowsCaption.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.lblOutflowsCaption.Appearance.Options.UseFont = True
        Me.lblOutflowsCaption.Appearance.Options.UseForeColor = True
        Me.lblOutflowsCaption.Appearance.Options.UseTextOptions = True
        Me.lblOutflowsCaption.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        resources.ApplyResources(Me.lblOutflowsCaption, "lblOutflowsCaption")
        Me.lblOutflowsCaption.Name = "lblOutflowsCaption"
        '
        'lblHeaderIncome
        '
        Me.lblHeaderIncome.Appearance.Font = CType(resources.GetObject("lblHeaderIncome.Appearance.Font"), System.Drawing.Font)
        Me.lblHeaderIncome.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.lblHeaderIncome.Appearance.Options.UseFont = True
        Me.lblHeaderIncome.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblHeaderIncome, "lblHeaderIncome")
        Me.lblHeaderIncome.Name = "lblHeaderIncome"
        '
        'lblIncomeCaption
        '
        Me.lblIncomeCaption.Appearance.Font = CType(resources.GetObject("lblIncomeCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblIncomeCaption.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(79, Byte), Integer))
        Me.lblIncomeCaption.Appearance.Options.UseFont = True
        Me.lblIncomeCaption.Appearance.Options.UseForeColor = True
        Me.lblIncomeCaption.Appearance.Options.UseTextOptions = True
        Me.lblIncomeCaption.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        resources.ApplyResources(Me.lblIncomeCaption, "lblIncomeCaption")
        Me.lblIncomeCaption.Name = "lblIncomeCaption"
        '
        'btnFinanceRefresh
        '
        Me.btnFinanceRefresh.Appearance.Font = CType(resources.GetObject("btnFinanceRefresh.Appearance.Font"), System.Drawing.Font)
        Me.btnFinanceRefresh.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnFinanceRefresh, "btnFinanceRefresh")
        Me.btnFinanceRefresh.Name = "btnFinanceRefresh"
        '
        'dateFinanceTo
        '
        resources.ApplyResources(Me.dateFinanceTo, "dateFinanceTo")
        Me.dateFinanceTo.Name = "dateFinanceTo"
        Me.dateFinanceTo.Properties.Appearance.Font = CType(resources.GetObject("dateFinanceTo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dateFinanceTo.Properties.Appearance.Options.UseFont = True
        Me.dateFinanceTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dateFinanceTo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dateFinanceTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dateFinanceTo.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'dateFinanceFrom
        '
        resources.ApplyResources(Me.dateFinanceFrom, "dateFinanceFrom")
        Me.dateFinanceFrom.Name = "dateFinanceFrom"
        Me.dateFinanceFrom.Properties.Appearance.Font = CType(resources.GetObject("dateFinanceFrom.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dateFinanceFrom.Properties.Appearance.Options.UseFont = True
        Me.dateFinanceFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dateFinanceFrom.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dateFinanceFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dateFinanceFrom.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblDateToCaption
        '
        Me.lblDateToCaption.Appearance.Font = CType(resources.GetObject("lblDateToCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblDateToCaption.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblDateToCaption, "lblDateToCaption")
        Me.lblDateToCaption.Name = "lblDateToCaption"
        '
        'lblDateFromCaption
        '
        Me.lblDateFromCaption.Appearance.Font = CType(resources.GetObject("lblDateFromCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblDateFromCaption.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblDateFromCaption, "lblDateFromCaption")
        Me.lblDateFromCaption.Name = "lblDateFromCaption"
        '
        'FrmFinance
        '
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlRoot)
        Me.Name = "FrmFinance"
        CType(Me.splitFinTreat.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinTreat.Panel1.ResumeLayout(False)
        CType(Me.splitFinTreat.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinTreat.Panel2.ResumeLayout(False)
        CType(Me.splitFinTreat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinTreat.ResumeLayout(False)
        CType(Me.pnlTreatHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTreatHeader.ResumeLayout(False)
        Me.pnlTreatHeader.PerformLayout()
        CType(Me.gridFinTreatments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridViewFinTreatments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.splitFinExp.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinExp.Panel1.ResumeLayout(False)
        CType(Me.splitFinExp.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinExp.Panel2.ResumeLayout(False)
        CType(Me.splitFinExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinExp.ResumeLayout(False)
        CType(Me.pnlExpHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlExpHeader.ResumeLayout(False)
        Me.pnlExpHeader.PerformLayout()
        CType(Me.gridFinExpenses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridViewFinExpenses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.splitFinPurch.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinPurch.Panel1.ResumeLayout(False)
        CType(Me.splitFinPurch.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinPurch.Panel2.ResumeLayout(False)
        CType(Me.splitFinPurch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinPurch.ResumeLayout(False)
        CType(Me.pnlPurchHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPurchHeader.ResumeLayout(False)
        Me.pnlPurchHeader.PerformLayout()
        CType(Me.gridFinPurchases, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridViewFinPurchases, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.splitFinLab.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinLab.Panel1.ResumeLayout(False)
        CType(Me.splitFinLab.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinLab.Panel2.ResumeLayout(False)
        CType(Me.splitFinLab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinLab.ResumeLayout(False)
        CType(Me.pnlLabHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLabHeader.ResumeLayout(False)
        Me.pnlLabHeader.PerformLayout()
        CType(Me.gridFinLab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridViewFinLab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.splitFinStaff.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinStaff.Panel1.ResumeLayout(False)
        CType(Me.splitFinStaff.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinStaff.Panel2.ResumeLayout(False)
        CType(Me.splitFinStaff, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinStaff.ResumeLayout(False)
        CType(Me.pnlStaffHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStaffHeader.ResumeLayout(False)
        Me.pnlStaffHeader.PerformLayout()
        CType(Me.gridFinStaff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridViewFinStaff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.splitFinPatientPays.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinPatientPays.Panel1.ResumeLayout(False)
        CType(Me.splitFinPatientPays.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinPatientPays.Panel2.ResumeLayout(False)
        CType(Me.splitFinPatientPays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinPatientPays.ResumeLayout(False)
        CType(Me.pnlPatPayHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPatPayHeader.ResumeLayout(False)
        CType(Me.gridFinPatientPays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridViewFinPatientPays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.splitFinSupplierPays.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinSupplierPays.Panel1.ResumeLayout(False)
        CType(Me.splitFinSupplierPays.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinSupplierPays.Panel2.ResumeLayout(False)
        CType(Me.splitFinSupplierPays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFinSupplierPays.ResumeLayout(False)
        CType(Me.pnlSupPayHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSupPayHeader.ResumeLayout(False)
        CType(Me.gridFinSupplierPays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridViewFinSupplierPays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRoot.ResumeLayout(False)
        CType(Me.navFinance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.navFinance.ResumeLayout(False)
        Me.pageFinOverview.ResumeLayout(False)
        CType(Me.pnlOverview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlOverview.ResumeLayout(False)
        Me.flowKpi.ResumeLayout(False)
        CType(Me.pnlKpiTreat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlKpiTreat.ResumeLayout(False)
        Me.pnlKpiTreat.PerformLayout()
        CType(Me.pnlKpiPatientPays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlKpiPatientPays.ResumeLayout(False)
        CType(Me.pnlKpiExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlKpiExp.ResumeLayout(False)
        Me.pnlKpiExp.PerformLayout()
        CType(Me.pnlKpiPurch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlKpiPurch.ResumeLayout(False)
        Me.pnlKpiPurch.PerformLayout()
        CType(Me.pnlKpiSupplierPays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlKpiSupplierPays.ResumeLayout(False)
        CType(Me.pnlKpiLab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlKpiLab.ResumeLayout(False)
        Me.pnlKpiLab.PerformLayout()
        CType(Me.pnlKpiStaff, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlKpiStaff.ResumeLayout(False)
        Me.pnlKpiStaff.PerformLayout()
        Me.pageFinTreatments.ResumeLayout(False)
        Me.pageFinPatientPays.ResumeLayout(False)
        Me.pageFinExpenses.ResumeLayout(False)
        Me.pageFinPurchases.ResumeLayout(False)
        Me.pageFinSupplierPays.ResumeLayout(False)
        Me.pageFinLab.ResumeLayout(False)
        Me.pageFinStaff.ResumeLayout(False)
        CType(Me.pnlFinanceToolbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFinanceToolbar.ResumeLayout(False)
        Me.pnlFinanceToolbar.PerformLayout()
        CType(Me.dateFinanceTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateFinanceTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateFinanceFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateFinanceFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlRoot As System.Windows.Forms.Panel
    Friend WithEvents pnlFinanceToolbar As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblDateFromCaption As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDateToCaption As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dateFinanceFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dateFinanceTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnFinanceRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblIncomeCaption As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblHeaderIncome As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOutflowsCaption As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblHeaderOutflows As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNetCaption As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblHeaderNet As DevExpress.XtraEditors.LabelControl
    Friend WithEvents navFinance As DevExpress.XtraBars.Navigation.NavigationFrame
    Friend WithEvents pageFinOverview As DevExpress.XtraBars.Navigation.NavigationPage
    Friend WithEvents pnlOverview As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblOverviewIntro As DevExpress.XtraEditors.LabelControl
    Friend WithEvents flowKpi As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlKpiTreat As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblKpiTreatTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiTreatHint As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiTreatGo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnlKpiExp As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblKpiExpTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiExpHint As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiExpGo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnlKpiPurch As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblKpiPurchTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiPurchHint As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiPurchGo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnlKpiPatientPays As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblKpiPatientPaysTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiPatientPaysHint As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiPatientPaysGo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnlKpiSupplierPays As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblKpiSupplierPaysTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiSupplierPaysHint As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiSupplierPaysGo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnlKpiLab As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblKpiLabTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiLabHint As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiLabGo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnlKpiStaff As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblKpiStaffTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiStaffHint As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblKpiStaffGo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pageFinTreatments As DevExpress.XtraBars.Navigation.NavigationPage
    Friend WithEvents pageFinPatientPays As DevExpress.XtraBars.Navigation.NavigationPage
    Friend WithEvents splitFinTreat As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents pnlTreatHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnBackTreat As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTitleTreat As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gridFinTreatments As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridViewFinTreatments As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pageFinExpenses As DevExpress.XtraBars.Navigation.NavigationPage
    Friend WithEvents splitFinExp As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents pnlExpHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnBackExp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTitleExp As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gridFinExpenses As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridViewFinExpenses As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pageFinPurchases As DevExpress.XtraBars.Navigation.NavigationPage
    Friend WithEvents pageFinSupplierPays As DevExpress.XtraBars.Navigation.NavigationPage
    Friend WithEvents splitFinPurch As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents pnlPurchHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnBackPurch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTitlePurch As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gridFinPurchases As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridViewFinPurchases As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pageFinLab As DevExpress.XtraBars.Navigation.NavigationPage
    Friend WithEvents splitFinLab As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents pnlLabHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnBackLab As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTitleLab As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gridFinLab As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridViewFinLab As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pageFinStaff As DevExpress.XtraBars.Navigation.NavigationPage
    Friend WithEvents splitFinStaff As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents pnlStaffHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnBackStaff As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTitleStaff As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gridFinStaff As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridViewFinStaff As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents splitFinPatientPays As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents pnlPatPayHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblTitlePatPay As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnBackPatPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gridFinPatientPays As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridViewFinPatientPays As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents splitFinSupplierPays As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents pnlSupPayHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblTitleSupPay As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnBackSupPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gridFinSupplierPays As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridViewFinSupplierPays As DevExpress.XtraGrid.Views.Grid.GridView

End Class
