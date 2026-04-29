<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVendorBuy
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmVendorBuy))
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule2 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue2 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Me.colSalesValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.vendorPanel = New DevExpress.XtraEditors.PanelControl()
        Me.balanceGroup = New DevExpress.XtraEditors.GroupControl()
        Me.VendTotalBuys = New DevExpress.XtraEditors.LabelControl()
        Me.VenTotalPays = New DevExpress.XtraEditors.LabelControl()
        Me.VendBalance = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.vendorGroup = New DevExpress.XtraEditors.GroupControl()
        Me.VendIdLbl = New DevExpress.XtraEditors.LabelControl()
        Me.VenNameLbl = New DevExpress.XtraEditors.LabelControl()
        Me.btnAddvendor = New DevExpress.XtraEditors.SimpleButton()
        Me.CboVendor = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.VendContactLbl = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.VenAdresLbl = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.VendorPaysBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.VendorsBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.VendorBuysBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.payPanel = New DevExpress.XtraEditors.SidePanel()
        Me.GridTabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.CashGridPage = New DevExpress.XtraTab.XtraTabPage()
        Me.VendorPaysGrid = New DevExpress.XtraGrid.GridControl()
        Me.GridViewPays = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVendIDPay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSalesIDPay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ChqGridPage = New DevExpress.XtraTab.XtraTabPage()
        Me.ChqsPayGrid = New DevExpress.XtraGrid.GridControl()
        Me.ChqsGridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsVendID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsSalesID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqAccount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqDueDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqBank = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqOwner = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SidePanel3 = New DevExpress.XtraEditors.SidePanel()
        Me.chqCashTab = New DevExpress.XtraTab.XtraTabControl()
        Me.CashPage = New DevExpress.XtraTab.XtraTabPage()
        Me.txtPayNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl24 = New DevExpress.XtraEditors.LabelControl()
        Me.PayValue = New DevExpress.XtraEditors.TextEdit()
        Me.PayDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.ChqPage = New DevExpress.XtraTab.XtraTabPage()
        Me.txtChqNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl25 = New DevExpress.XtraEditors.LabelControl()
        Me.PayDateChq = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.txtChqBank = New DevExpress.XtraEditors.TextEdit()
        Me.btnResetChqs = New DevExpress.XtraEditors.SimpleButton()
        Me.lblPayID = New DevExpress.XtraEditors.LabelControl()
        Me.cboUncashedChqs = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtChqOwner = New DevExpress.XtraEditors.TextEdit()
        Me.chkIsCashed = New DevExpress.XtraEditors.CheckEdit()
        Me.dtChqDueDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.txtChqNumber = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.txtAccountNumber = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl20 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl21 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl22 = New DevExpress.XtraEditors.LabelControl()
        Me.txtChqValue = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl23 = New DevExpress.XtraEditors.LabelControl()
        Me.SidePanel1 = New DevExpress.XtraEditors.SidePanel()
        Me.PayNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.btnPayDel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton15 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton16 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox3 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton17 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton18 = New System.Windows.Forms.ToolStripButton()
        Me.PaySavNav = New System.Windows.Forms.ToolStripButton()
        Me.btnAddPay = New DevExpress.XtraEditors.SimpleButton()
        Me.payTypePanel = New DevExpress.XtraEditors.SidePanel()
        Me.cboPayType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.buyPanel = New DevExpress.XtraEditors.PanelControl()
        Me.VendorBuysGrid = New DevExpress.XtraGrid.GridControl()
        Me.GridViewBuys = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSalesID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVendID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSalesDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.trtInputPanel = New DevExpress.XtraEditors.SidePanel()
        Me.txtBuyDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.cboUnPaid = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.showAsChildChck = New DevExpress.XtraEditors.CheckEdit()
        Me.BuyNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel5 = New System.Windows.Forms.ToolStripLabel()
        Me.btnBuyDel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton23 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton24 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox5 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton25 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton26 = New System.Windows.Forms.ToolStripButton()
        Me.btnBuySavNav = New System.Windows.Forms.ToolStripButton()
        Me.BuyValue = New DevExpress.XtraEditors.TextEdit()
        Me.btnAllBuys = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddBuy = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.lblSalesID = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.BuyDate = New DevExpress.XtraEditors.DateEdit()
        CType(Me.vendorPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vendorPanel.SuspendLayout()
        CType(Me.balanceGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.balanceGroup.SuspendLayout()
        CType(Me.vendorGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vendorGroup.SuspendLayout()
        CType(Me.CboVendor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VendorPaysBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VendorsBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VendorBuysBS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.payPanel.SuspendLayout()
        CType(Me.GridTabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GridTabControl.SuspendLayout()
        Me.CashGridPage.SuspendLayout()
        CType(Me.VendorPaysGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewPays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ChqGridPage.SuspendLayout()
        CType(Me.ChqsPayGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChqsGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SidePanel3.SuspendLayout()
        CType(Me.chqCashTab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.chqCashTab.SuspendLayout()
        Me.CashPage.SuspendLayout()
        CType(Me.txtPayNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ChqPage.SuspendLayout()
        CType(Me.txtChqNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDateChq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDateChq.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqBank.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboUncashedChqs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqOwner.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsCashed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtChqDueDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtChqDueDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccountNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SidePanel1.SuspendLayout()
        CType(Me.PayNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PayNavigator.SuspendLayout()
        Me.payTypePanel.SuspendLayout()
        CType(Me.cboPayType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.buyPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.buyPanel.SuspendLayout()
        CType(Me.VendorBuysGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewBuys, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.trtInputPanel.SuspendLayout()
        CType(Me.txtBuyDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboUnPaid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.showAsChildChck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BuyNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BuyNavigator.SuspendLayout()
        CType(Me.BuyValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BuyDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BuyDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'colSalesValue
        '
        Me.colSalesValue.Caption = "Buy Value"
        Me.colSalesValue.DisplayFormat.FormatString = "n0"
        Me.colSalesValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colSalesValue.FieldName = "SalesValue"
        Me.colSalesValue.Name = "colSalesValue"
        Me.colSalesValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SalesValue", "{0:n0}")})
        Me.colSalesValue.UnboundDataType = GetType(Decimal)
        Me.colSalesValue.Visible = True
        Me.colSalesValue.VisibleIndex = 2
        Me.colSalesValue.Width = 158
        '
        'vendorPanel
        '
        Me.vendorPanel.Controls.Add(Me.balanceGroup)
        Me.vendorPanel.Controls.Add(Me.vendorGroup)
        Me.vendorPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.vendorPanel.Location = New System.Drawing.Point(0, 0)
        Me.vendorPanel.Name = "vendorPanel"
        Me.vendorPanel.Size = New System.Drawing.Size(1360, 121)
        Me.vendorPanel.TabIndex = 0
        '
        'balanceGroup
        '
        Me.balanceGroup.AppearanceCaption.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.balanceGroup.AppearanceCaption.Options.UseFont = True
        Me.balanceGroup.Controls.Add(Me.VendTotalBuys)
        Me.balanceGroup.Controls.Add(Me.VenTotalPays)
        Me.balanceGroup.Controls.Add(Me.VendBalance)
        Me.balanceGroup.Controls.Add(Me.LabelControl15)
        Me.balanceGroup.Controls.Add(Me.LabelControl16)
        Me.balanceGroup.Controls.Add(Me.LabelControl17)
        Me.balanceGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.balanceGroup.Location = New System.Drawing.Point(1164, 2)
        Me.balanceGroup.Name = "balanceGroup"
        Me.balanceGroup.Size = New System.Drawing.Size(194, 117)
        Me.balanceGroup.TabIndex = 1
        Me.balanceGroup.Text = "Vendor Balance"
        '
        'VendTotalBuys
        '
        Me.VendTotalBuys.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.VendTotalBuys.Appearance.ForeColor = System.Drawing.Color.Yellow
        Me.VendTotalBuys.Appearance.Options.UseFont = True
        Me.VendTotalBuys.Appearance.Options.UseForeColor = True
        Me.VendTotalBuys.Location = New System.Drawing.Point(91, 31)
        Me.VendTotalBuys.Name = "VendTotalBuys"
        Me.VendTotalBuys.Size = New System.Drawing.Size(77, 15)
        Me.VendTotalBuys.TabIndex = 1
        Me.VendTotalBuys.Text = "LabelControl2"
        '
        'VenTotalPays
        '
        Me.VenTotalPays.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.VenTotalPays.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.VenTotalPays.Appearance.Options.UseFont = True
        Me.VenTotalPays.Appearance.Options.UseForeColor = True
        Me.VenTotalPays.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.VenTotalPays.Location = New System.Drawing.Point(91, 64)
        Me.VenTotalPays.Name = "VenTotalPays"
        Me.VenTotalPays.Size = New System.Drawing.Size(77, 15)
        Me.VenTotalPays.TabIndex = 3
        Me.VenTotalPays.Text = "LabelControl2"
        '
        'VendBalance
        '
        Me.VendBalance.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.VendBalance.Appearance.Options.UseFont = True
        Me.VendBalance.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.VendBalance.Location = New System.Drawing.Point(91, 96)
        Me.VendBalance.Name = "VendBalance"
        Me.VendBalance.Size = New System.Drawing.Size(77, 15)
        Me.VendBalance.TabIndex = 5
        Me.VendBalance.Text = "LabelControl2"
        '
        'LabelControl15
        '
        Me.LabelControl15.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl15.Appearance.Options.UseFont = True
        Me.LabelControl15.Location = New System.Drawing.Point(27, 96)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Size = New System.Drawing.Size(41, 15)
        Me.LabelControl15.TabIndex = 4
        Me.LabelControl15.Text = "Balance"
        '
        'LabelControl16
        '
        Me.LabelControl16.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl16.Appearance.Options.UseFont = True
        Me.LabelControl16.Location = New System.Drawing.Point(27, 64)
        Me.LabelControl16.Name = "LabelControl16"
        Me.LabelControl16.Size = New System.Drawing.Size(54, 15)
        Me.LabelControl16.TabIndex = 2
        Me.LabelControl16.Text = "Total Pays"
        '
        'LabelControl17
        '
        Me.LabelControl17.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl17.Appearance.Options.UseFont = True
        Me.LabelControl17.Location = New System.Drawing.Point(27, 31)
        Me.LabelControl17.Name = "LabelControl17"
        Me.LabelControl17.Size = New System.Drawing.Size(55, 15)
        Me.LabelControl17.TabIndex = 0
        Me.LabelControl17.Text = "Total Buys"
        '
        'vendorGroup
        '
        Me.vendorGroup.AppearanceCaption.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.vendorGroup.AppearanceCaption.Options.UseFont = True
        Me.vendorGroup.Controls.Add(Me.VendIdLbl)
        Me.vendorGroup.Controls.Add(Me.VenNameLbl)
        Me.vendorGroup.Controls.Add(Me.btnAddvendor)
        Me.vendorGroup.Controls.Add(Me.CboVendor)
        Me.vendorGroup.Controls.Add(Me.VendContactLbl)
        Me.vendorGroup.Controls.Add(Me.LabelControl1)
        Me.vendorGroup.Controls.Add(Me.LabelControl8)
        Me.vendorGroup.Controls.Add(Me.VenAdresLbl)
        Me.vendorGroup.Controls.Add(Me.LabelControl6)
        Me.vendorGroup.Controls.Add(Me.LabelControl4)
        Me.vendorGroup.Controls.Add(Me.LabelControl2)
        Me.vendorGroup.Dock = System.Windows.Forms.DockStyle.Left
        Me.vendorGroup.Location = New System.Drawing.Point(2, 2)
        Me.vendorGroup.Name = "vendorGroup"
        Me.vendorGroup.Size = New System.Drawing.Size(1162, 117)
        Me.vendorGroup.TabIndex = 0
        Me.vendorGroup.Text = "Vendor Details"
        '
        'VendIdLbl
        '
        Me.VendIdLbl.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.VendIdLbl.Appearance.Options.UseFont = True
        Me.VendIdLbl.Location = New System.Drawing.Point(561, 38)
        Me.VendIdLbl.Name = "VendIdLbl"
        Me.VendIdLbl.Size = New System.Drawing.Size(77, 15)
        Me.VendIdLbl.TabIndex = 10
        Me.VendIdLbl.Text = "LabelControl2"
        '
        'VenNameLbl
        '
        Me.VenNameLbl.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.VenNameLbl.Appearance.Options.UseFont = True
        Me.VenNameLbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.VenNameLbl.Location = New System.Drawing.Point(112, 73)
        Me.VenNameLbl.Name = "VenNameLbl"
        Me.VenNameLbl.Size = New System.Drawing.Size(251, 26)
        Me.VenNameLbl.TabIndex = 4
        Me.VenNameLbl.Text = "LabelControl2"
        '
        'btnAddvendor
        '
        Me.btnAddvendor.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnAddvendor.Appearance.Options.UseFont = True
        Me.btnAddvendor.Location = New System.Drawing.Point(288, 34)
        Me.btnAddvendor.Name = "btnAddvendor"
        Me.btnAddvendor.Size = New System.Drawing.Size(75, 23)
        Me.btnAddvendor.TabIndex = 2
        Me.btnAddvendor.Text = "Add Vendor"
        '
        'CboVendor
        '
        Me.CboVendor.Location = New System.Drawing.Point(118, 35)
        Me.CboVendor.Name = "CboVendor"
        Me.CboVendor.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboVendor.Properties.Appearance.Options.UseFont = True
        Me.CboVendor.Properties.Appearance.Options.UseTextOptions = True
        Me.CboVendor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CboVendor.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.CboVendor.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboVendor.Properties.AppearanceDropDown.Options.UseFont = True
        Me.CboVendor.Properties.AppearanceDropDown.Options.UseTextOptions = True
        Me.CboVendor.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CboVendor.Properties.AppearanceDropDown.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.CboVendor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CboVendor.Size = New System.Drawing.Size(150, 22)
        Me.CboVendor.TabIndex = 1
        '
        'VendContactLbl
        '
        Me.VendContactLbl.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.VendContactLbl.Appearance.Options.UseFont = True
        Me.VendContactLbl.Appearance.Options.UseTextOptions = True
        Me.VendContactLbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.VendContactLbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.VendContactLbl.Location = New System.Drawing.Point(742, 73)
        Me.VendContactLbl.Name = "VendContactLbl"
        Me.VendContactLbl.Size = New System.Drawing.Size(251, 26)
        Me.VendContactLbl.TabIndex = 8
        Me.VendContactLbl.Text = "LabelControl2"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(20, 39)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(77, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Select Vendor"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Location = New System.Drawing.Point(676, 79)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(66, 15)
        Me.LabelControl8.TabIndex = 7
        Me.LabelControl8.Text = "Contact Info"
        '
        'VenAdresLbl
        '
        Me.VenAdresLbl.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.VenAdresLbl.Appearance.Options.UseFont = True
        Me.VenAdresLbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.VenAdresLbl.Location = New System.Drawing.Point(419, 73)
        Me.VenAdresLbl.Name = "VenAdresLbl"
        Me.VenAdresLbl.Size = New System.Drawing.Size(251, 26)
        Me.VenAdresLbl.TabIndex = 6
        Me.VenAdresLbl.Text = "LabelControl2"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Location = New System.Drawing.Point(369, 79)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(44, 15)
        Me.LabelControl6.TabIndex = 5
        Me.LabelControl6.Text = "Address"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(14, 79)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(77, 15)
        Me.LabelControl4.TabIndex = 3
        Me.LabelControl4.Text = "Vendor Name"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(463, 38)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(55, 15)
        Me.LabelControl2.TabIndex = 9
        Me.LabelControl2.Text = "Vendor ID"
        '
        'VendorPaysBS
        '
        '
        'VendorsBS
        '
        '
        'VendorBuysBS
        '
        '
        'payPanel
        '
        Me.payPanel.Controls.Add(Me.GridTabControl)
        Me.payPanel.Controls.Add(Me.SidePanel3)
        Me.payPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.payPanel.Location = New System.Drawing.Point(0, 393)
        Me.payPanel.Name = "payPanel"
        Me.payPanel.Size = New System.Drawing.Size(1360, 343)
        Me.payPanel.TabIndex = 1
        Me.payPanel.Text = "SidePanel1"
        '
        'GridTabControl
        '
        Me.GridTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridTabControl.Location = New System.Drawing.Point(502, 1)
        Me.GridTabControl.Name = "GridTabControl"
        Me.GridTabControl.SelectedTabPage = Me.CashGridPage
        Me.GridTabControl.Size = New System.Drawing.Size(858, 342)
        Me.GridTabControl.TabIndex = 26
        Me.GridTabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.CashGridPage, Me.ChqGridPage})
        '
        'CashGridPage
        '
        Me.CashGridPage.Controls.Add(Me.VendorPaysGrid)
        Me.CashGridPage.Name = "CashGridPage"
        Me.CashGridPage.Size = New System.Drawing.Size(853, 314)
        Me.CashGridPage.Text = "Cash Payments"
        '
        'VendorPaysGrid
        '
        Me.VendorPaysGrid.DataSource = Me.VendorPaysBS
        Me.VendorPaysGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VendorPaysGrid.EmbeddedNavigator.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.VendorPaysGrid.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.VendorPaysGrid.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.VendorPaysGrid.Location = New System.Drawing.Point(0, 0)
        Me.VendorPaysGrid.MainView = Me.GridViewPays
        Me.VendorPaysGrid.Name = "VendorPaysGrid"
        Me.VendorPaysGrid.Size = New System.Drawing.Size(853, 314)
        Me.VendorPaysGrid.TabIndex = 0
        Me.VendorPaysGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewPays})
        '
        'GridViewPays
        '
        Me.GridViewPays.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold
        Me.GridViewPays.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Red
        Me.GridViewPays.Appearance.FooterPanel.Options.UseFont = True
        Me.GridViewPays.Appearance.FooterPanel.Options.UseForeColor = True
        Me.GridViewPays.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.GridViewPays.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewPays.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridViewPays.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewPays.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridViewPays.Appearance.Row.Options.UseFont = True
        Me.GridViewPays.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum1, Me.colPayID, Me.colVendIDPay, Me.colSalesIDPay, Me.colPayValue, Me.colPayDate, Me.colNotes})
        Me.GridViewPays.GridControl = Me.VendorPaysGrid
        Me.GridViewPays.Name = "GridViewPays"
        Me.GridViewPays.OptionsView.EnableAppearanceEvenRow = True
        Me.GridViewPays.OptionsView.ShowErrorPanel = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridViewPays.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridViewPays.OptionsView.ShowFooter = True
        Me.GridViewPays.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.GridViewPays.OptionsView.ShowGroupPanel = False
        '
        'colRowNum1
        '
        Me.colRowNum1.Caption = "Number"
        Me.colRowNum1.FieldName = "colRowNum1"
        Me.colRowNum1.Name = "colRowNum1"
        Me.colRowNum1.UnboundDataType = GetType(Integer)
        Me.colRowNum1.Visible = True
        Me.colRowNum1.VisibleIndex = 0
        Me.colRowNum1.Width = 61
        '
        'colPayID
        '
        Me.colPayID.FieldName = "PayID"
        Me.colPayID.Name = "colPayID"
        '
        'colVendIDPay
        '
        Me.colVendIDPay.Caption = "VendID"
        Me.colVendIDPay.FieldName = "VendID"
        Me.colVendIDPay.Name = "colVendIDPay"
        '
        'colSalesIDPay
        '
        Me.colSalesIDPay.Caption = "SalesID"
        Me.colSalesIDPay.FieldName = "SalesID"
        Me.colSalesIDPay.Name = "colSalesIDPay"
        '
        'colPayValue
        '
        Me.colPayValue.Caption = "Pay Value"
        Me.colPayValue.DisplayFormat.FormatString = "n0"
        Me.colPayValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPayValue.FieldName = "PayValue"
        Me.colPayValue.Name = "colPayValue"
        Me.colPayValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PayValue", "{0:n0}")})
        Me.colPayValue.UnboundDataType = GetType(Double)
        Me.colPayValue.Visible = True
        Me.colPayValue.VisibleIndex = 2
        Me.colPayValue.Width = 156
        '
        'colPayDate
        '
        Me.colPayDate.Caption = "Pay Date"
        Me.colPayDate.FieldName = "PayDate"
        Me.colPayDate.Name = "colPayDate"
        Me.colPayDate.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "PayDate", "{0}")})
        Me.colPayDate.Visible = True
        Me.colPayDate.VisibleIndex = 3
        Me.colPayDate.Width = 152
        '
        'colNotes
        '
        Me.colNotes.Caption = "Notes"
        Me.colNotes.FieldName = "Notes"
        Me.colNotes.Name = "colNotes"
        Me.colNotes.Visible = True
        Me.colNotes.VisibleIndex = 1
        Me.colNotes.Width = 317
        '
        'ChqGridPage
        '
        Me.ChqGridPage.Controls.Add(Me.ChqsPayGrid)
        Me.ChqGridPage.Name = "ChqGridPage"
        Me.ChqGridPage.Size = New System.Drawing.Size(853, 314)
        Me.ChqGridPage.Text = "Cheques Payment"
        '
        'ChqsPayGrid
        '
        Me.ChqsPayGrid.DataSource = Me.VendorPaysBS
        Me.ChqsPayGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChqsPayGrid.EmbeddedNavigator.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ChqsPayGrid.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.ChqsPayGrid.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.ChqsPayGrid.Location = New System.Drawing.Point(0, 0)
        Me.ChqsPayGrid.MainView = Me.ChqsGridView
        Me.ChqsPayGrid.Name = "ChqsPayGrid"
        Me.ChqsPayGrid.Size = New System.Drawing.Size(853, 314)
        Me.ChqsPayGrid.TabIndex = 1
        Me.ChqsPayGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ChqsGridView})
        '
        'ChqsGridView
        '
        Me.ChqsGridView.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold
        Me.ChqsGridView.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Red
        Me.ChqsGridView.Appearance.FooterPanel.Options.UseFont = True
        Me.ChqsGridView.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ChqsGridView.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.ChqsGridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ChqsGridView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ChqsGridView.Appearance.HeaderPanel.Options.UseFont = True
        Me.ChqsGridView.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ChqsGridView.Appearance.Row.Options.UseFont = True
        Me.ChqsGridView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum2, Me.colChqsPayID, Me.colChqsVendID, Me.colChqsSalesID, Me.colChqsPayValue, Me.colChqsPayDate, Me.colChqsPayNotes, Me.colChqNumber, Me.colChqAccount, Me.colChqDueDate, Me.colChqBank, Me.colChqOwner})
        Me.ChqsGridView.GridControl = Me.ChqsPayGrid
        Me.ChqsGridView.Name = "ChqsGridView"
        Me.ChqsGridView.OptionsView.EnableAppearanceEvenRow = True
        Me.ChqsGridView.OptionsView.ShowErrorPanel = DevExpress.Utils.DefaultBoolean.[False]
        Me.ChqsGridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ChqsGridView.OptionsView.ShowFooter = True
        Me.ChqsGridView.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ChqsGridView.OptionsView.ShowGroupPanel = False
        '
        'colRowNum2
        '
        Me.colRowNum2.Caption = "Number"
        Me.colRowNum2.FieldName = "colRowNum2"
        Me.colRowNum2.Name = "colRowNum2"
        Me.colRowNum2.UnboundDataType = GetType(Integer)
        Me.colRowNum2.Visible = True
        Me.colRowNum2.VisibleIndex = 0
        Me.colRowNum2.Width = 61
        '
        'colChqsPayID
        '
        Me.colChqsPayID.FieldName = "PayID"
        Me.colChqsPayID.Name = "colChqsPayID"
        '
        'colChqsVendID
        '
        Me.colChqsVendID.Caption = "VendID"
        Me.colChqsVendID.FieldName = "VendID"
        Me.colChqsVendID.Name = "colChqsVendID"
        '
        'colChqsSalesID
        '
        Me.colChqsSalesID.Caption = "SalesID"
        Me.colChqsSalesID.FieldName = "SalesID"
        Me.colChqsSalesID.Name = "colChqsSalesID"
        '
        'colChqsPayValue
        '
        Me.colChqsPayValue.Caption = "Pay Value"
        Me.colChqsPayValue.DisplayFormat.FormatString = "n0"
        Me.colChqsPayValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colChqsPayValue.FieldName = "PayValue"
        Me.colChqsPayValue.Name = "colChqsPayValue"
        Me.colChqsPayValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PayValue", "{0:n0}")})
        Me.colChqsPayValue.UnboundDataType = GetType(Double)
        Me.colChqsPayValue.Visible = True
        Me.colChqsPayValue.VisibleIndex = 2
        Me.colChqsPayValue.Width = 156
        '
        'colChqsPayDate
        '
        Me.colChqsPayDate.Caption = "Pay Date"
        Me.colChqsPayDate.FieldName = "PayDate"
        Me.colChqsPayDate.Name = "colChqsPayDate"
        Me.colChqsPayDate.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "PayDate", "{0}")})
        Me.colChqsPayDate.Visible = True
        Me.colChqsPayDate.VisibleIndex = 3
        Me.colChqsPayDate.Width = 152
        '
        'colChqsPayNotes
        '
        Me.colChqsPayNotes.Caption = "Notes"
        Me.colChqsPayNotes.FieldName = "Notes"
        Me.colChqsPayNotes.Name = "colChqsPayNotes"
        Me.colChqsPayNotes.Visible = True
        Me.colChqsPayNotes.VisibleIndex = 1
        Me.colChqsPayNotes.Width = 317
        '
        'colChqNumber
        '
        Me.colChqNumber.Caption = "Chq Number"
        Me.colChqNumber.FieldName = "ChqNumber"
        Me.colChqNumber.Name = "colChqNumber"
        Me.colChqNumber.Visible = True
        Me.colChqNumber.VisibleIndex = 4
        '
        'colChqAccount
        '
        Me.colChqAccount.Caption = "Chq Account"
        Me.colChqAccount.FieldName = "ChqAccount"
        Me.colChqAccount.Name = "colChqAccount"
        Me.colChqAccount.Visible = True
        Me.colChqAccount.VisibleIndex = 5
        '
        'colChqDueDate
        '
        Me.colChqDueDate.Caption = "ChqDueDate"
        Me.colChqDueDate.FieldName = "ChqDueDate"
        Me.colChqDueDate.Name = "colChqDueDate"
        Me.colChqDueDate.Visible = True
        Me.colChqDueDate.VisibleIndex = 6
        '
        'colChqBank
        '
        Me.colChqBank.Caption = "Chq Bank"
        Me.colChqBank.FieldName = "ChqBank"
        Me.colChqBank.Name = "colChqBank"
        Me.colChqBank.Visible = True
        Me.colChqBank.VisibleIndex = 7
        '
        'colChqOwner
        '
        Me.colChqOwner.Caption = "Chq Owner"
        Me.colChqOwner.FieldName = "ChqOwner"
        Me.colChqOwner.Name = "colChqOwner"
        Me.colChqOwner.Visible = True
        Me.colChqOwner.VisibleIndex = 8
        '
        'SidePanel3
        '
        Me.SidePanel3.Controls.Add(Me.chqCashTab)
        Me.SidePanel3.Controls.Add(Me.SidePanel1)
        Me.SidePanel3.Controls.Add(Me.payTypePanel)
        Me.SidePanel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.SidePanel3.Location = New System.Drawing.Point(0, 1)
        Me.SidePanel3.Name = "SidePanel3"
        Me.SidePanel3.Size = New System.Drawing.Size(502, 342)
        Me.SidePanel3.TabIndex = 25
        Me.SidePanel3.Text = "SidePanel3"
        '
        'chqCashTab
        '
        Me.chqCashTab.AppearancePage.Header.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.chqCashTab.AppearancePage.Header.Options.UseFont = True
        Me.chqCashTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chqCashTab.Location = New System.Drawing.Point(0, 32)
        Me.chqCashTab.Name = "chqCashTab"
        Me.chqCashTab.SelectedTabPage = Me.CashPage
        Me.chqCashTab.Size = New System.Drawing.Size(501, 250)
        Me.chqCashTab.TabIndex = 1
        Me.chqCashTab.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.CashPage, Me.ChqPage})
        '
        'CashPage
        '
        Me.CashPage.Controls.Add(Me.txtPayNotes)
        Me.CashPage.Controls.Add(Me.LabelControl24)
        Me.CashPage.Controls.Add(Me.PayValue)
        Me.CashPage.Controls.Add(Me.PayDate)
        Me.CashPage.Controls.Add(Me.LabelControl12)
        Me.CashPage.Controls.Add(Me.LabelControl11)
        Me.CashPage.Name = "CashPage"
        Me.CashPage.Size = New System.Drawing.Size(496, 222)
        Me.CashPage.Text = "Cash"
        '
        'txtPayNotes
        '
        Me.txtPayNotes.Location = New System.Drawing.Point(128, 32)
        Me.txtPayNotes.Name = "txtPayNotes"
        Me.txtPayNotes.Size = New System.Drawing.Size(308, 59)
        Me.txtPayNotes.TabIndex = 1
        '
        'LabelControl24
        '
        Me.LabelControl24.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl24.Appearance.Options.UseFont = True
        Me.LabelControl24.Location = New System.Drawing.Point(31, 54)
        Me.LabelControl24.Name = "LabelControl24"
        Me.LabelControl24.Size = New System.Drawing.Size(89, 15)
        Me.LabelControl24.TabIndex = 0
        Me.LabelControl24.Text = "Payment Notes:"
        '
        'PayValue
        '
        Me.PayValue.EnterMoveNextControl = True
        Me.PayValue.Location = New System.Drawing.Point(128, 152)
        Me.PayValue.Name = "PayValue"
        Me.PayValue.Size = New System.Drawing.Size(100, 22)
        Me.PayValue.TabIndex = 5
        '
        'PayDate
        '
        Me.PayDate.EditValue = Nothing
        Me.PayDate.EnterMoveNextControl = True
        Me.PayDate.Location = New System.Drawing.Point(128, 114)
        Me.PayDate.Name = "PayDate"
        Me.PayDate.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.PayDate.Properties.Appearance.Options.UseFont = True
        Me.PayDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PayDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PayDate.Size = New System.Drawing.Size(100, 22)
        Me.PayDate.TabIndex = 3
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl12.Appearance.Options.UseFont = True
        Me.LabelControl12.Location = New System.Drawing.Point(31, 155)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(57, 15)
        Me.LabelControl12.TabIndex = 4
        Me.LabelControl12.Text = "Pay Value:"
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl11.Appearance.Options.UseFont = True
        Me.LabelControl11.Location = New System.Drawing.Point(31, 117)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(52, 15)
        Me.LabelControl11.TabIndex = 2
        Me.LabelControl11.Text = "Pay Date:"
        '
        'ChqPage
        '
        Me.ChqPage.Controls.Add(Me.txtChqNotes)
        Me.ChqPage.Controls.Add(Me.LabelControl25)
        Me.ChqPage.Controls.Add(Me.PayDateChq)
        Me.ChqPage.Controls.Add(Me.LabelControl14)
        Me.ChqPage.Controls.Add(Me.txtChqBank)
        Me.ChqPage.Controls.Add(Me.btnResetChqs)
        Me.ChqPage.Controls.Add(Me.lblPayID)
        Me.ChqPage.Controls.Add(Me.cboUncashedChqs)
        Me.ChqPage.Controls.Add(Me.LabelControl3)
        Me.ChqPage.Controls.Add(Me.txtChqOwner)
        Me.ChqPage.Controls.Add(Me.chkIsCashed)
        Me.ChqPage.Controls.Add(Me.dtChqDueDate)
        Me.ChqPage.Controls.Add(Me.LabelControl18)
        Me.ChqPage.Controls.Add(Me.txtChqNumber)
        Me.ChqPage.Controls.Add(Me.LabelControl19)
        Me.ChqPage.Controls.Add(Me.txtAccountNumber)
        Me.ChqPage.Controls.Add(Me.LabelControl20)
        Me.ChqPage.Controls.Add(Me.LabelControl21)
        Me.ChqPage.Controls.Add(Me.LabelControl22)
        Me.ChqPage.Controls.Add(Me.txtChqValue)
        Me.ChqPage.Controls.Add(Me.LabelControl23)
        Me.ChqPage.Name = "ChqPage"
        Me.ChqPage.Size = New System.Drawing.Size(496, 222)
        Me.ChqPage.Text = "Cheques"
        '
        'txtChqNotes
        '
        Me.txtChqNotes.Location = New System.Drawing.Point(129, 38)
        Me.txtChqNotes.Name = "txtChqNotes"
        Me.txtChqNotes.Size = New System.Drawing.Size(364, 26)
        Me.txtChqNotes.TabIndex = 20
        '
        'LabelControl25
        '
        Me.LabelControl25.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl25.Appearance.Options.UseFont = True
        Me.LabelControl25.Location = New System.Drawing.Point(31, 44)
        Me.LabelControl25.Name = "LabelControl25"
        Me.LabelControl25.Size = New System.Drawing.Size(89, 15)
        Me.LabelControl25.TabIndex = 19
        Me.LabelControl25.Text = "Payment Notes:"
        '
        'PayDateChq
        '
        Me.PayDateChq.EditValue = Nothing
        Me.PayDateChq.EnterMoveNextControl = True
        Me.PayDateChq.Location = New System.Drawing.Point(132, 70)
        Me.PayDateChq.Name = "PayDateChq"
        Me.PayDateChq.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.PayDateChq.Properties.Appearance.Options.UseFont = True
        Me.PayDateChq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PayDateChq.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PayDateChq.Size = New System.Drawing.Size(100, 22)
        Me.PayDateChq.TabIndex = 18
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl14.Appearance.Options.UseFont = True
        Me.LabelControl14.Location = New System.Drawing.Point(35, 73)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Size = New System.Drawing.Size(52, 15)
        Me.LabelControl14.TabIndex = 17
        Me.LabelControl14.Text = "Pay Date:"
        '
        'txtChqBank
        '
        Me.txtChqBank.EnterMoveNextControl = True
        Me.txtChqBank.Location = New System.Drawing.Point(132, 182)
        Me.txtChqBank.Name = "txtChqBank"
        Me.txtChqBank.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqBank.Properties.Appearance.Options.UseFont = True
        Me.txtChqBank.Size = New System.Drawing.Size(154, 22)
        Me.txtChqBank.TabIndex = 9
        '
        'btnResetChqs
        '
        Me.btnResetChqs.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnResetChqs.Appearance.Options.UseFont = True
        Me.btnResetChqs.Location = New System.Drawing.Point(380, 194)
        Me.btnResetChqs.Name = "btnResetChqs"
        Me.btnResetChqs.Size = New System.Drawing.Size(75, 23)
        Me.btnResetChqs.TabIndex = 16
        Me.btnResetChqs.Text = "Reset Fields"
        '
        'lblPayID
        '
        Me.lblPayID.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblPayID.Appearance.Options.UseFont = True
        Me.lblPayID.Location = New System.Drawing.Point(178, 186)
        Me.lblPayID.Name = "lblPayID"
        Me.lblPayID.Size = New System.Drawing.Size(30, 15)
        Me.lblPayID.TabIndex = 15
        Me.lblPayID.Text = "PayID"
        '
        'cboUncashedChqs
        '
        Me.cboUncashedChqs.EnterMoveNextControl = True
        Me.cboUncashedChqs.Location = New System.Drawing.Point(128, 14)
        Me.cboUncashedChqs.Name = "cboUncashedChqs"
        Me.cboUncashedChqs.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUncashedChqs.Properties.Appearance.Options.UseFont = True
        Me.cboUncashedChqs.Properties.Appearance.Options.UseTextOptions = True
        Me.cboUncashedChqs.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cboUncashedChqs.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.cboUncashedChqs.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUncashedChqs.Properties.AppearanceDropDown.Options.UseFont = True
        Me.cboUncashedChqs.Properties.AppearanceDropDown.Options.UseTextOptions = True
        Me.cboUncashedChqs.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cboUncashedChqs.Properties.AppearanceDropDown.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.cboUncashedChqs.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboUncashedChqs.Size = New System.Drawing.Size(365, 22)
        Me.cboUncashedChqs.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(9, 18)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(111, 15)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "UnCashed Cheques :"
        '
        'txtChqOwner
        '
        Me.txtChqOwner.EnterMoveNextControl = True
        Me.txtChqOwner.Location = New System.Drawing.Point(132, 98)
        Me.txtChqOwner.Name = "txtChqOwner"
        Me.txtChqOwner.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqOwner.Properties.Appearance.Options.UseFont = True
        Me.txtChqOwner.Size = New System.Drawing.Size(154, 22)
        Me.txtChqOwner.TabIndex = 3
        '
        'chkIsCashed
        '
        Me.chkIsCashed.Location = New System.Drawing.Point(380, 169)
        Me.chkIsCashed.Name = "chkIsCashed"
        Me.chkIsCashed.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.chkIsCashed.Properties.Appearance.Options.UseFont = True
        Me.chkIsCashed.Properties.Caption = "Is Cashed"
        Me.chkIsCashed.Size = New System.Drawing.Size(75, 19)
        Me.chkIsCashed.TabIndex = 14
        '
        'dtChqDueDate
        '
        Me.dtChqDueDate.EditValue = Nothing
        Me.dtChqDueDate.EnterMoveNextControl = True
        Me.dtChqDueDate.Location = New System.Drawing.Point(380, 98)
        Me.dtChqDueDate.Name = "dtChqDueDate"
        Me.dtChqDueDate.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtChqDueDate.Properties.Appearance.Options.UseFont = True
        Me.dtChqDueDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtChqDueDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtChqDueDate.Size = New System.Drawing.Size(100, 22)
        Me.dtChqDueDate.TabIndex = 11
        '
        'LabelControl18
        '
        Me.LabelControl18.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl18.Appearance.Options.UseFont = True
        Me.LabelControl18.Location = New System.Drawing.Point(13, 130)
        Me.LabelControl18.Name = "LabelControl18"
        Me.LabelControl18.Size = New System.Drawing.Size(100, 15)
        Me.LabelControl18.TabIndex = 4
        Me.LabelControl18.Text = "Account Number :"
        '
        'txtChqNumber
        '
        Me.txtChqNumber.EnterMoveNextControl = True
        Me.txtChqNumber.Location = New System.Drawing.Point(132, 154)
        Me.txtChqNumber.Name = "txtChqNumber"
        Me.txtChqNumber.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqNumber.Properties.Appearance.Options.UseFont = True
        Me.txtChqNumber.Size = New System.Drawing.Size(154, 22)
        Me.txtChqNumber.TabIndex = 7
        '
        'LabelControl19
        '
        Me.LabelControl19.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl19.Appearance.Options.UseFont = True
        Me.LabelControl19.Location = New System.Drawing.Point(13, 158)
        Me.LabelControl19.Name = "LabelControl19"
        Me.LabelControl19.Size = New System.Drawing.Size(98, 15)
        Me.LabelControl19.TabIndex = 6
        Me.LabelControl19.Text = "Cheque Number :"
        '
        'txtAccountNumber
        '
        Me.txtAccountNumber.EnterMoveNextControl = True
        Me.txtAccountNumber.Location = New System.Drawing.Point(132, 126)
        Me.txtAccountNumber.Name = "txtAccountNumber"
        Me.txtAccountNumber.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountNumber.Properties.Appearance.Options.UseFont = True
        Me.txtAccountNumber.Size = New System.Drawing.Size(154, 22)
        Me.txtAccountNumber.TabIndex = 5
        '
        'LabelControl20
        '
        Me.LabelControl20.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl20.Appearance.Options.UseFont = True
        Me.LabelControl20.Location = New System.Drawing.Point(294, 102)
        Me.LabelControl20.Name = "LabelControl20"
        Me.LabelControl20.Size = New System.Drawing.Size(58, 15)
        Me.LabelControl20.TabIndex = 10
        Me.LabelControl20.Text = "Due Date :"
        '
        'LabelControl21
        '
        Me.LabelControl21.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl21.Appearance.Options.UseFont = True
        Me.LabelControl21.Location = New System.Drawing.Point(13, 186)
        Me.LabelControl21.Name = "LabelControl21"
        Me.LabelControl21.Size = New System.Drawing.Size(78, 15)
        Me.LabelControl21.TabIndex = 8
        Me.LabelControl21.Text = "Cheque Bank :"
        '
        'LabelControl22
        '
        Me.LabelControl22.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl22.Appearance.Options.UseFont = True
        Me.LabelControl22.Location = New System.Drawing.Point(13, 102)
        Me.LabelControl22.Name = "LabelControl22"
        Me.LabelControl22.Size = New System.Drawing.Size(90, 15)
        Me.LabelControl22.TabIndex = 2
        Me.LabelControl22.Text = "Cheque Owner :"
        '
        'txtChqValue
        '
        Me.txtChqValue.EnterMoveNextControl = True
        Me.txtChqValue.Location = New System.Drawing.Point(380, 126)
        Me.txtChqValue.Name = "txtChqValue"
        Me.txtChqValue.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqValue.Properties.Appearance.Options.UseFont = True
        Me.txtChqValue.Size = New System.Drawing.Size(100, 22)
        Me.txtChqValue.TabIndex = 13
        '
        'LabelControl23
        '
        Me.LabelControl23.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl23.Appearance.Options.UseFont = True
        Me.LabelControl23.Location = New System.Drawing.Point(294, 130)
        Me.LabelControl23.Name = "LabelControl23"
        Me.LabelControl23.Size = New System.Drawing.Size(83, 15)
        Me.LabelControl23.TabIndex = 12
        Me.LabelControl23.Text = "Cheque Value :"
        '
        'SidePanel1
        '
        Me.SidePanel1.Controls.Add(Me.PayNavigator)
        Me.SidePanel1.Controls.Add(Me.btnAddPay)
        Me.SidePanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SidePanel1.Location = New System.Drawing.Point(0, 282)
        Me.SidePanel1.Name = "SidePanel1"
        Me.SidePanel1.Size = New System.Drawing.Size(501, 60)
        Me.SidePanel1.TabIndex = 2
        Me.SidePanel1.Text = "SidePanel1"
        '
        'PayNavigator
        '
        Me.PayNavigator.AddNewItem = Nothing
        Me.PayNavigator.AutoSize = False
        Me.PayNavigator.BindingSource = Me.VendorPaysBS
        Me.PayNavigator.CountItem = Me.ToolStripLabel3
        Me.PayNavigator.DeleteItem = Me.btnPayDel
        Me.PayNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PayNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.PayNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton15, Me.ToolStripButton16, Me.ToolStripSeparator6, Me.ToolStripTextBox3, Me.ToolStripLabel3, Me.ToolStripSeparator7, Me.ToolStripButton17, Me.ToolStripButton18, Me.btnPayDel, Me.PaySavNav})
        Me.PayNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.PayNavigator.Location = New System.Drawing.Point(0, 39)
        Me.PayNavigator.MoveFirstItem = Me.ToolStripButton15
        Me.PayNavigator.MoveLastItem = Me.ToolStripButton18
        Me.PayNavigator.MoveNextItem = Me.ToolStripButton17
        Me.PayNavigator.MovePreviousItem = Me.ToolStripButton16
        Me.PayNavigator.Name = "PayNavigator"
        Me.PayNavigator.PositionItem = Me.ToolStripTextBox3
        Me.PayNavigator.Size = New System.Drawing.Size(501, 21)
        Me.PayNavigator.TabIndex = 1
        Me.PayNavigator.Text = "BindingNavigator1"
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(35, 15)
        Me.ToolStripLabel3.Text = "of {0}"
        Me.ToolStripLabel3.ToolTipText = "Total number of items"
        '
        'btnPayDel
        '
        Me.btnPayDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPayDel.Image = CType(resources.GetObject("btnPayDel.Image"), System.Drawing.Image)
        Me.btnPayDel.Name = "btnPayDel"
        Me.btnPayDel.RightToLeftAutoMirrorImage = True
        Me.btnPayDel.Size = New System.Drawing.Size(23, 20)
        Me.btnPayDel.Text = "Delete"
        '
        'ToolStripButton15
        '
        Me.ToolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton15.Image = CType(resources.GetObject("ToolStripButton15.Image"), System.Drawing.Image)
        Me.ToolStripButton15.Name = "ToolStripButton15"
        Me.ToolStripButton15.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton15.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton15.Text = "Move first"
        '
        'ToolStripButton16
        '
        Me.ToolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton16.Image = CType(resources.GetObject("ToolStripButton16.Image"), System.Drawing.Image)
        Me.ToolStripButton16.Name = "ToolStripButton16"
        Me.ToolStripButton16.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton16.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton16.Text = "Move previous"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripTextBox3
        '
        Me.ToolStripTextBox3.AccessibleName = "Position"
        Me.ToolStripTextBox3.AutoSize = False
        Me.ToolStripTextBox3.Name = "ToolStripTextBox3"
        Me.ToolStripTextBox3.Size = New System.Drawing.Size(25, 23)
        Me.ToolStripTextBox3.Text = "0"
        Me.ToolStripTextBox3.ToolTipText = "Current position"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripButton17
        '
        Me.ToolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton17.Image = CType(resources.GetObject("ToolStripButton17.Image"), System.Drawing.Image)
        Me.ToolStripButton17.Name = "ToolStripButton17"
        Me.ToolStripButton17.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton17.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton17.Text = "Move next"
        '
        'ToolStripButton18
        '
        Me.ToolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton18.Image = CType(resources.GetObject("ToolStripButton18.Image"), System.Drawing.Image)
        Me.ToolStripButton18.Name = "ToolStripButton18"
        Me.ToolStripButton18.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton18.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton18.Text = "Move last"
        '
        'PaySavNav
        '
        Me.PaySavNav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PaySavNav.Image = CType(resources.GetObject("PaySavNav.Image"), System.Drawing.Image)
        Me.PaySavNav.Name = "PaySavNav"
        Me.PaySavNav.Size = New System.Drawing.Size(23, 20)
        Me.PaySavNav.Text = "Save Data"
        '
        'btnAddPay
        '
        Me.btnAddPay.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnAddPay.Appearance.Options.UseFont = True
        Me.btnAddPay.Location = New System.Drawing.Point(381, 9)
        Me.btnAddPay.Name = "btnAddPay"
        Me.btnAddPay.Size = New System.Drawing.Size(86, 27)
        Me.btnAddPay.TabIndex = 0
        Me.btnAddPay.Text = "Add Pay"
        '
        'payTypePanel
        '
        Me.payTypePanel.Controls.Add(Me.cboPayType)
        Me.payTypePanel.Controls.Add(Me.LabelControl13)
        Me.payTypePanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.payTypePanel.Location = New System.Drawing.Point(0, 0)
        Me.payTypePanel.Name = "payTypePanel"
        Me.payTypePanel.Size = New System.Drawing.Size(501, 32)
        Me.payTypePanel.TabIndex = 0
        Me.payTypePanel.Text = "SidePanel1"
        '
        'cboPayType
        '
        Me.cboPayType.EditValue = "Cash"
        Me.cboPayType.EnterMoveNextControl = True
        Me.cboPayType.Location = New System.Drawing.Point(248, 5)
        Me.cboPayType.Name = "cboPayType"
        Me.cboPayType.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPayType.Properties.Appearance.Options.UseFont = True
        Me.cboPayType.Properties.Appearance.Options.UseTextOptions = True
        Me.cboPayType.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cboPayType.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.cboPayType.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPayType.Properties.AppearanceDropDown.Options.UseFont = True
        Me.cboPayType.Properties.AppearanceDropDown.Options.UseTextOptions = True
        Me.cboPayType.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cboPayType.Properties.AppearanceDropDown.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.cboPayType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboPayType.Properties.Items.AddRange(New Object() {"Cash", "Cheque"})
        Me.cboPayType.Size = New System.Drawing.Size(100, 22)
        Me.cboPayType.TabIndex = 1
        '
        'LabelControl13
        '
        Me.LabelControl13.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl13.Appearance.Options.UseFont = True
        Me.LabelControl13.Location = New System.Drawing.Point(153, 9)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Size = New System.Drawing.Size(52, 15)
        Me.LabelControl13.TabIndex = 0
        Me.LabelControl13.Text = "Pay Type:"
        '
        'buyPanel
        '
        Me.buyPanel.Controls.Add(Me.VendorBuysGrid)
        Me.buyPanel.Controls.Add(Me.trtInputPanel)
        Me.buyPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.buyPanel.Location = New System.Drawing.Point(0, 121)
        Me.buyPanel.Name = "buyPanel"
        Me.buyPanel.Size = New System.Drawing.Size(1360, 272)
        Me.buyPanel.TabIndex = 0
        '
        'VendorBuysGrid
        '
        Me.VendorBuysGrid.DataSource = Me.VendorBuysBS
        Me.VendorBuysGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VendorBuysGrid.EmbeddedNavigator.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.VendorBuysGrid.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.VendorBuysGrid.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.VendorBuysGrid.EmbeddedNavigator.Buttons.CancelEdit.Tag = "Cancel"
        Me.VendorBuysGrid.EmbeddedNavigator.Buttons.Edit.Tag = "Edit"
        Me.VendorBuysGrid.EmbeddedNavigator.Buttons.EndEdit.Tag = "EndEdit"
        Me.VendorBuysGrid.EmbeddedNavigator.Buttons.Remove.Tag = "Remove"
        Me.VendorBuysGrid.Location = New System.Drawing.Point(506, 2)
        Me.VendorBuysGrid.MainView = Me.GridViewBuys
        Me.VendorBuysGrid.Name = "VendorBuysGrid"
        Me.VendorBuysGrid.Size = New System.Drawing.Size(852, 268)
        Me.VendorBuysGrid.TabIndex = 1
        Me.VendorBuysGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewBuys})
        '
        'GridViewBuys
        '
        Me.GridViewBuys.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold
        Me.GridViewBuys.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue
        Me.GridViewBuys.Appearance.FooterPanel.Options.UseFont = True
        Me.GridViewBuys.Appearance.FooterPanel.Options.UseForeColor = True
        Me.GridViewBuys.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.GridViewBuys.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewBuys.Appearance.GroupFooter.FontStyleDelta = System.Drawing.FontStyle.Bold
        Me.GridViewBuys.Appearance.GroupFooter.Options.UseFont = True
        Me.GridViewBuys.Appearance.GroupFooter.Options.UseTextOptions = True
        Me.GridViewBuys.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewBuys.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridViewBuys.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewBuys.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridViewBuys.Appearance.Row.Options.UseFont = True
        Me.GridViewBuys.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colSalesID, Me.colVendID, Me.colDetail, Me.colSalesDate, Me.colSalesValue})
        GridFormatRule1.Column = Me.colSalesValue
        GridFormatRule1.Description = Nothing
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        FormatConditionRuleValue1.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        FormatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.Blue
        FormatConditionRuleValue1.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue1.Appearance.Options.UseFont = True
        FormatConditionRuleValue1.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue1.Expression = "[TrtValue] > 0"
        GridFormatRule1.Rule = FormatConditionRuleValue1
        GridFormatRule2.Column = Me.colSalesValue
        GridFormatRule2.Description = Nothing
        GridFormatRule2.Name = "Format1"
        FormatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        FormatConditionRuleValue2.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        FormatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.Red
        FormatConditionRuleValue2.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue2.Appearance.Options.UseFont = True
        FormatConditionRuleValue2.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Less
        FormatConditionRuleValue2.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        GridFormatRule2.Rule = FormatConditionRuleValue2
        Me.GridViewBuys.FormatRules.Add(GridFormatRule1)
        Me.GridViewBuys.FormatRules.Add(GridFormatRule2)
        Me.GridViewBuys.GridControl = Me.VendorBuysGrid
        Me.GridViewBuys.Name = "GridViewBuys"
        Me.GridViewBuys.OptionsDetail.EnableMasterViewMode = False
        Me.GridViewBuys.OptionsView.AutoCalcPreviewLineCount = True
        Me.GridViewBuys.OptionsView.EnableAppearanceEvenRow = True
        Me.GridViewBuys.OptionsView.ShowFooter = True
        '
        'colRowNum
        '
        Me.colRowNum.Caption = "Number"
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        Me.colRowNum.Visible = True
        Me.colRowNum.VisibleIndex = 0
        Me.colRowNum.Width = 59
        '
        'colSalesID
        '
        Me.colSalesID.Caption = "SalesID"
        Me.colSalesID.FieldName = "SalesID"
        Me.colSalesID.Name = "colSalesID"
        '
        'colVendID
        '
        Me.colVendID.Caption = "VendID"
        Me.colVendID.FieldName = "VendID"
        Me.colVendID.Name = "colVendID"
        '
        'colDetail
        '
        Me.colDetail.Caption = "Details"
        Me.colDetail.FieldName = "Detail"
        Me.colDetail.Name = "colDetail"
        Me.colDetail.Visible = True
        Me.colDetail.VisibleIndex = 1
        Me.colDetail.Width = 320
        '
        'colSalesDate
        '
        Me.colSalesDate.Caption = "Buy Date"
        Me.colSalesDate.FieldName = "SalesDate"
        Me.colSalesDate.Name = "colSalesDate"
        Me.colSalesDate.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "SalesDate", "{0}")})
        Me.colSalesDate.Visible = True
        Me.colSalesDate.VisibleIndex = 3
        Me.colSalesDate.Width = 154
        '
        'trtInputPanel
        '
        Me.trtInputPanel.Controls.Add(Me.txtBuyDetail)
        Me.trtInputPanel.Controls.Add(Me.cboUnPaid)
        Me.trtInputPanel.Controls.Add(Me.showAsChildChck)
        Me.trtInputPanel.Controls.Add(Me.BuyNavigator)
        Me.trtInputPanel.Controls.Add(Me.BuyValue)
        Me.trtInputPanel.Controls.Add(Me.btnAllBuys)
        Me.trtInputPanel.Controls.Add(Me.btnAddBuy)
        Me.trtInputPanel.Controls.Add(Me.LabelControl5)
        Me.trtInputPanel.Controls.Add(Me.LabelControl7)
        Me.trtInputPanel.Controls.Add(Me.lblSalesID)
        Me.trtInputPanel.Controls.Add(Me.LabelControl9)
        Me.trtInputPanel.Controls.Add(Me.LabelControl10)
        Me.trtInputPanel.Controls.Add(Me.BuyDate)
        Me.trtInputPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.trtInputPanel.Location = New System.Drawing.Point(2, 2)
        Me.trtInputPanel.Name = "trtInputPanel"
        Me.trtInputPanel.Size = New System.Drawing.Size(504, 268)
        Me.trtInputPanel.TabIndex = 0
        Me.trtInputPanel.Text = "SidePanel1"
        '
        'txtBuyDetail
        '
        Me.txtBuyDetail.Location = New System.Drawing.Point(128, 39)
        Me.txtBuyDetail.Name = "txtBuyDetail"
        Me.txtBuyDetail.Size = New System.Drawing.Size(365, 84)
        Me.txtBuyDetail.TabIndex = 3
        '
        'cboUnPaid
        '
        Me.cboUnPaid.EnterMoveNextControl = True
        Me.cboUnPaid.Location = New System.Drawing.Point(128, 11)
        Me.cboUnPaid.Name = "cboUnPaid"
        Me.cboUnPaid.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUnPaid.Properties.Appearance.Options.UseFont = True
        Me.cboUnPaid.Properties.Appearance.Options.UseTextOptions = True
        Me.cboUnPaid.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cboUnPaid.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.cboUnPaid.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUnPaid.Properties.AppearanceDropDown.Options.UseFont = True
        Me.cboUnPaid.Properties.AppearanceDropDown.Options.UseTextOptions = True
        Me.cboUnPaid.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cboUnPaid.Properties.AppearanceDropDown.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.cboUnPaid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboUnPaid.Size = New System.Drawing.Size(364, 22)
        Me.cboUnPaid.TabIndex = 1
        '
        'showAsChildChck
        '
        Me.showAsChildChck.Location = New System.Drawing.Point(357, 157)
        Me.showAsChildChck.Name = "showAsChildChck"
        Me.showAsChildChck.Properties.Caption = "Show Pays As Child"
        Me.showAsChildChck.Size = New System.Drawing.Size(140, 19)
        Me.showAsChildChck.TabIndex = 9
        '
        'BuyNavigator
        '
        Me.BuyNavigator.AddNewItem = Nothing
        Me.BuyNavigator.AutoSize = False
        Me.BuyNavigator.BindingSource = Me.VendorBuysBS
        Me.BuyNavigator.CountItem = Me.ToolStripLabel5
        Me.BuyNavigator.DeleteItem = Me.btnBuyDel
        Me.BuyNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BuyNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.BuyNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton23, Me.ToolStripButton24, Me.ToolStripSeparator11, Me.ToolStripTextBox5, Me.ToolStripLabel5, Me.ToolStripSeparator12, Me.ToolStripButton25, Me.ToolStripButton26, Me.btnBuyDel, Me.btnBuySavNav})
        Me.BuyNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.BuyNavigator.Location = New System.Drawing.Point(0, 247)
        Me.BuyNavigator.MoveFirstItem = Me.ToolStripButton23
        Me.BuyNavigator.MoveLastItem = Me.ToolStripButton26
        Me.BuyNavigator.MoveNextItem = Me.ToolStripButton25
        Me.BuyNavigator.MovePreviousItem = Me.ToolStripButton24
        Me.BuyNavigator.Name = "BuyNavigator"
        Me.BuyNavigator.PositionItem = Me.ToolStripTextBox5
        Me.BuyNavigator.Size = New System.Drawing.Size(503, 21)
        Me.BuyNavigator.TabIndex = 12
        Me.BuyNavigator.Text = "BindingNavigator1"
        '
        'ToolStripLabel5
        '
        Me.ToolStripLabel5.Name = "ToolStripLabel5"
        Me.ToolStripLabel5.Size = New System.Drawing.Size(35, 15)
        Me.ToolStripLabel5.Text = "of {0}"
        Me.ToolStripLabel5.ToolTipText = "Total number of items"
        '
        'btnBuyDel
        '
        Me.btnBuyDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnBuyDel.Image = CType(resources.GetObject("btnBuyDel.Image"), System.Drawing.Image)
        Me.btnBuyDel.Name = "btnBuyDel"
        Me.btnBuyDel.RightToLeftAutoMirrorImage = True
        Me.btnBuyDel.Size = New System.Drawing.Size(23, 20)
        Me.btnBuyDel.Text = "Delete"
        '
        'ToolStripButton23
        '
        Me.ToolStripButton23.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton23.Image = CType(resources.GetObject("ToolStripButton23.Image"), System.Drawing.Image)
        Me.ToolStripButton23.Name = "ToolStripButton23"
        Me.ToolStripButton23.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton23.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton23.Text = "Move first"
        '
        'ToolStripButton24
        '
        Me.ToolStripButton24.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton24.Image = CType(resources.GetObject("ToolStripButton24.Image"), System.Drawing.Image)
        Me.ToolStripButton24.Name = "ToolStripButton24"
        Me.ToolStripButton24.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton24.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton24.Text = "Move previous"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripTextBox5
        '
        Me.ToolStripTextBox5.AccessibleName = "Position"
        Me.ToolStripTextBox5.AutoSize = False
        Me.ToolStripTextBox5.Name = "ToolStripTextBox5"
        Me.ToolStripTextBox5.Size = New System.Drawing.Size(25, 23)
        Me.ToolStripTextBox5.Text = "0"
        Me.ToolStripTextBox5.ToolTipText = "Current position"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripButton25
        '
        Me.ToolStripButton25.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton25.Image = CType(resources.GetObject("ToolStripButton25.Image"), System.Drawing.Image)
        Me.ToolStripButton25.Name = "ToolStripButton25"
        Me.ToolStripButton25.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton25.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton25.Text = "Move next"
        '
        'ToolStripButton26
        '
        Me.ToolStripButton26.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton26.Image = CType(resources.GetObject("ToolStripButton26.Image"), System.Drawing.Image)
        Me.ToolStripButton26.Name = "ToolStripButton26"
        Me.ToolStripButton26.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton26.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton26.Text = "Move last"
        '
        'btnBuySavNav
        '
        Me.btnBuySavNav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnBuySavNav.Image = CType(resources.GetObject("btnBuySavNav.Image"), System.Drawing.Image)
        Me.btnBuySavNav.Name = "btnBuySavNav"
        Me.btnBuySavNav.Size = New System.Drawing.Size(23, 20)
        Me.btnBuySavNav.Text = "Save Data"
        '
        'BuyValue
        '
        Me.BuyValue.EditValue = 0R
        Me.BuyValue.EnterMoveNextControl = True
        Me.BuyValue.Location = New System.Drawing.Point(128, 164)
        Me.BuyValue.Name = "BuyValue"
        Me.BuyValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BuyValue.Size = New System.Drawing.Size(100, 22)
        Me.BuyValue.TabIndex = 7
        '
        'btnAllBuys
        '
        Me.btnAllBuys.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnAllBuys.Appearance.Options.UseFont = True
        Me.btnAllBuys.Location = New System.Drawing.Point(233, 212)
        Me.btnAllBuys.Name = "btnAllBuys"
        Me.btnAllBuys.Size = New System.Drawing.Size(86, 27)
        Me.btnAllBuys.TabIndex = 10
        Me.btnAllBuys.Text = "Show All Buys"
        '
        'btnAddBuy
        '
        Me.btnAddBuy.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnAddBuy.Appearance.Options.UseFont = True
        Me.btnAddBuy.Location = New System.Drawing.Point(233, 159)
        Me.btnAddBuy.Name = "btnAddBuy"
        Me.btnAddBuy.Size = New System.Drawing.Size(86, 27)
        Me.btnAddBuy.TabIndex = 8
        Me.btnAddBuy.Text = "Add Buy"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Location = New System.Drawing.Point(31, 141)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(53, 15)
        Me.LabelControl5.TabIndex = 4
        Me.LabelControl5.Text = "Buy Date:"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Location = New System.Drawing.Point(31, 168)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(58, 15)
        Me.LabelControl7.TabIndex = 6
        Me.LabelControl7.Text = "Buy Value:"
        '
        'lblSalesID
        '
        Me.lblSalesID.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblSalesID.Appearance.Options.UseFont = True
        Me.lblSalesID.Location = New System.Drawing.Point(386, 219)
        Me.lblSalesID.Name = "lblSalesID"
        Me.lblSalesID.Size = New System.Drawing.Size(38, 15)
        Me.lblSalesID.TabIndex = 11
        Me.lblSalesID.Text = "SalesID"
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Location = New System.Drawing.Point(31, 15)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(73, 15)
        Me.LabelControl9.TabIndex = 0
        Me.LabelControl9.Text = "Unpaid Buys :"
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl10.Appearance.Options.UseFont = True
        Me.LabelControl10.Location = New System.Drawing.Point(31, 69)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(64, 15)
        Me.LabelControl10.TabIndex = 2
        Me.LabelControl10.Text = "Buy Details:"
        '
        'BuyDate
        '
        Me.BuyDate.EditValue = Nothing
        Me.BuyDate.EnterMoveNextControl = True
        Me.BuyDate.Location = New System.Drawing.Point(128, 137)
        Me.BuyDate.Name = "BuyDate"
        Me.BuyDate.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.BuyDate.Properties.Appearance.Options.UseFont = True
        Me.BuyDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.BuyDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.BuyDate.Size = New System.Drawing.Size(100, 22)
        Me.BuyDate.TabIndex = 5
        '
        'FrmVendorBuy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1360, 736)
        Me.Controls.Add(Me.buyPanel)
        Me.Controls.Add(Me.payPanel)
        Me.Controls.Add(Me.vendorPanel)
        Me.Name = "FrmVendorBuy"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmVendorBuy"
        CType(Me.vendorPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vendorPanel.ResumeLayout(False)
        CType(Me.balanceGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.balanceGroup.ResumeLayout(False)
        Me.balanceGroup.PerformLayout()
        CType(Me.vendorGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vendorGroup.ResumeLayout(False)
        Me.vendorGroup.PerformLayout()
        CType(Me.CboVendor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VendorPaysBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VendorsBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VendorBuysBS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.payPanel.ResumeLayout(False)
        CType(Me.GridTabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GridTabControl.ResumeLayout(False)
        Me.CashGridPage.ResumeLayout(False)
        CType(Me.VendorPaysGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewPays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ChqGridPage.ResumeLayout(False)
        CType(Me.ChqsPayGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChqsGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SidePanel3.ResumeLayout(False)
        CType(Me.chqCashTab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.chqCashTab.ResumeLayout(False)
        Me.CashPage.ResumeLayout(False)
        Me.CashPage.PerformLayout()
        CType(Me.txtPayNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ChqPage.ResumeLayout(False)
        Me.ChqPage.PerformLayout()
        CType(Me.txtChqNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDateChq.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDateChq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqBank.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboUncashedChqs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqOwner.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsCashed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtChqDueDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtChqDueDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccountNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SidePanel1.ResumeLayout(False)
        CType(Me.PayNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PayNavigator.ResumeLayout(False)
        Me.PayNavigator.PerformLayout()
        Me.payTypePanel.ResumeLayout(False)
        Me.payTypePanel.PerformLayout()
        CType(Me.cboPayType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.buyPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.buyPanel.ResumeLayout(False)
        CType(Me.VendorBuysGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewBuys, System.ComponentModel.ISupportInitialize).EndInit()
        Me.trtInputPanel.ResumeLayout(False)
        Me.trtInputPanel.PerformLayout()
        CType(Me.txtBuyDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboUnPaid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.showAsChildChck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BuyNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BuyNavigator.ResumeLayout(False)
        Me.BuyNavigator.PerformLayout()
        CType(Me.BuyValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BuyDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BuyDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents vendorPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents balanceGroup As DevExpress.XtraEditors.GroupControl
    Friend WithEvents VendTotalBuys As DevExpress.XtraEditors.LabelControl
    Friend WithEvents VenTotalPays As DevExpress.XtraEditors.LabelControl
    Friend WithEvents VendBalance As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents vendorGroup As DevExpress.XtraEditors.GroupControl
    Friend WithEvents VendIdLbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents VenNameLbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnAddvendor As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CboVendor As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents VendContactLbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents VenAdresLbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents VendorBuysBS As BindingSource
    Friend WithEvents payPanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents VendorPaysBS As BindingSource
    Friend WithEvents VendorsBS As BindingSource
    Friend WithEvents buyPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents trtInputPanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents cboUnPaid As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents showAsChildChck As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents BuyNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel5 As ToolStripLabel
    Friend WithEvents btnBuyDel As ToolStripButton
    Friend WithEvents ToolStripButton23 As ToolStripButton
    Friend WithEvents ToolStripButton24 As ToolStripButton
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox5 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents ToolStripButton25 As ToolStripButton
    Friend WithEvents ToolStripButton26 As ToolStripButton
    Friend WithEvents btnBuySavNav As ToolStripButton
    Friend WithEvents BuyValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnAllBuys As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddBuy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSalesID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BuyDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents VendorBuysGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewBuys As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSalesID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVendID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSalesDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSalesValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SidePanel3 As DevExpress.XtraEditors.SidePanel
    Friend WithEvents cboPayType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents PayValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnAddPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PayDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents chqCashTab As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents ChqPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents txtChqOwner As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkIsCashed As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dtChqDueDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtChqBank As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtChqNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtAccountNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl20 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl21 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl22 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtChqValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl23 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CashPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents txtPayNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl24 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents VendorPaysGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewPays As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVendIDPay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSalesIDPay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PayNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents btnPayDel As ToolStripButton
    Friend WithEvents ToolStripButton15 As ToolStripButton
    Friend WithEvents ToolStripButton16 As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox3 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripButton17 As ToolStripButton
    Friend WithEvents ToolStripButton18 As ToolStripButton
    Friend WithEvents PaySavNav As ToolStripButton
    Friend WithEvents SidePanel1 As DevExpress.XtraEditors.SidePanel
    Friend WithEvents payTypePanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents txtBuyDetail As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents cboUncashedChqs As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridTabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents CashGridPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ChqGridPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ChqsPayGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents ChqsGridView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqsPayID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqsVendID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqsSalesID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqsPayValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqsPayDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqsPayNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqAccount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqDueDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqBank As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqOwner As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblPayID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnResetChqs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtChqNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl25 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PayDateChq As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
End Class
