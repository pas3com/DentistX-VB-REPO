Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraTab

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class FrmSuppInvoices
    Inherits DevExpress.XtraEditors.XtraForm

    Private components As System.ComponentModel.IContainer

    Friend WithEvents panelFilters As PanelControl
    Friend WithEvents lblSupplier As LabelControl
    Friend WithEvents cmbSupplier As LookUpEdit
    Friend WithEvents chkAllSuppliers As CheckEdit
    Friend WithEvents lblSearch As LabelControl
    Friend WithEvents txtSearch As TextEdit
    Friend WithEvents lblDateFrom As LabelControl
    Friend WithEvents dtFrom As DateEdit
    Friend WithEvents lblDateTo As LabelControl
    Friend WithEvents dtTo As DateEdit
    Friend WithEvents btnApplyFilters As SimpleButton
    Friend WithEvents btnClearFilters As SimpleButton

    Friend WithEvents tabMain As XtraTabControl
    Friend WithEvents tabInvoices As XtraTabPage
    Friend WithEvents tabSummary As XtraTabPage

    Friend WithEvents pnlHeaders As PanelControl
    Friend WithEvents lblHdrListTitle As LabelControl
    Friend WithEvents grdHeaders As GridControl
    Friend WithEvents viewHeaders As GridView

    Friend WithEvents grdDetails As GridControl
    Friend WithEvents viewDetails As GridView

    Friend WithEvents panelHeader As PanelControl
    Friend WithEvents lblHdrInvoice As LabelControl
    Friend WithEvents lblHdrSupplier As LabelControl
    Friend WithEvents lblHdrDate As LabelControl
    Friend WithEvents lblHdrTotal As LabelControl

    Friend WithEvents grdSummary As GridControl
    Friend WithEvents viewSummary As GridView

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSuppInvoices))
        Me.panelFilters = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.RadioLang = New DevExpress.XtraEditors.RadioGroup()
        Me.btnWhatsSend = New DevExpress.XtraEditors.SimpleButton()
        Me.lblSupplier = New DevExpress.XtraEditors.LabelControl()
        Me.cmbSupplier = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkAllSuppliers = New DevExpress.XtraEditors.CheckEdit()
        Me.lblSearch = New DevExpress.XtraEditors.LabelControl()
        Me.txtSearch = New DevExpress.XtraEditors.TextEdit()
        Me.lblDateFrom = New DevExpress.XtraEditors.LabelControl()
        Me.dtFrom = New DevExpress.XtraEditors.DateEdit()
        Me.lblDateTo = New DevExpress.XtraEditors.LabelControl()
        Me.dtTo = New DevExpress.XtraEditors.DateEdit()
        Me.btnApplyFilters = New DevExpress.XtraEditors.SimpleButton()
        Me.btnReport2 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnReport = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClearFilters = New DevExpress.XtraEditors.SimpleButton()
        Me.tabMain = New DevExpress.XtraTab.XtraTabControl()
        Me.tabInvoices = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetails = New DevExpress.XtraGrid.GridControl()
        Me.viewDetails = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.panelHeader = New DevExpress.XtraEditors.PanelControl()
        Me.lblHdrInvoice = New DevExpress.XtraEditors.LabelControl()
        Me.lblHdrSupplier = New DevExpress.XtraEditors.LabelControl()
        Me.lblHdrDate = New DevExpress.XtraEditors.LabelControl()
        Me.lblHdrTotal = New DevExpress.XtraEditors.LabelControl()
        Me.grdHeaders = New DevExpress.XtraGrid.GridControl()
        Me.viewHeaders = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.pnlHeaders = New DevExpress.XtraEditors.PanelControl()
        Me.lblHdrListTitle = New DevExpress.XtraEditors.LabelControl()
        Me.tabSummary = New DevExpress.XtraTab.XtraTabPage()
        Me.grdSummary = New DevExpress.XtraGrid.GridControl()
        Me.viewSummary = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.panelFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelFilters.SuspendLayout()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllSuppliers.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.tabInvoices.SuspendLayout()
        CType(Me.grdDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelHeader.SuspendLayout()
        CType(Me.grdHeaders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewHeaders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlHeaders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeaders.SuspendLayout()
        Me.tabSummary.SuspendLayout()
        CType(Me.grdSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelFilters
        '
        resources.ApplyResources(Me.panelFilters, "panelFilters")
        Me.panelFilters.Controls.Add(Me.LabelControl3)
        Me.panelFilters.Controls.Add(Me.RadioLang)
        Me.panelFilters.Controls.Add(Me.btnWhatsSend)
        Me.panelFilters.Controls.Add(Me.lblSupplier)
        Me.panelFilters.Controls.Add(Me.cmbSupplier)
        Me.panelFilters.Controls.Add(Me.chkAllSuppliers)
        Me.panelFilters.Controls.Add(Me.lblSearch)
        Me.panelFilters.Controls.Add(Me.txtSearch)
        Me.panelFilters.Controls.Add(Me.lblDateFrom)
        Me.panelFilters.Controls.Add(Me.dtFrom)
        Me.panelFilters.Controls.Add(Me.lblDateTo)
        Me.panelFilters.Controls.Add(Me.dtTo)
        Me.panelFilters.Controls.Add(Me.btnApplyFilters)
        Me.panelFilters.Controls.Add(Me.btnReport2)
        Me.panelFilters.Controls.Add(Me.btnReport)
        Me.panelFilters.Controls.Add(Me.btnClearFilters)
        Me.panelFilters.Name = "panelFilters"
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'RadioLang
        '
        resources.ApplyResources(Me.RadioLang, "RadioLang")
        Me.RadioLang.EnterMoveNextControl = True
        Me.RadioLang.Name = "RadioLang"
        Me.RadioLang.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioLang.Properties.Appearance.Font = CType(resources.GetObject("RadioLang.Properties.Appearance.Font"), System.Drawing.Font)
        Me.RadioLang.Properties.Appearance.Options.UseBackColor = True
        Me.RadioLang.Properties.Appearance.Options.UseFont = True
        Me.RadioLang.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioLang.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLang.Properties.Items"), Object), resources.GetString("RadioLang.Properties.Items1"), CType(resources.GetObject("RadioLang.Properties.Items2"), Boolean), CType(resources.GetObject("RadioLang.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLang.Properties.Items4"), Object), resources.GetString("RadioLang.Properties.Items5"))})
        '
        'btnWhatsSend
        '
        resources.ApplyResources(Me.btnWhatsSend, "btnWhatsSend")
        Me.btnWhatsSend.ImageOptions.ImageKey = resources.GetString("btnWhatsSend.ImageOptions.ImageKey")
        Me.btnWhatsSend.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnWhatsSend.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.whatsapp
        Me.btnWhatsSend.ImageOptions.SvgImageSize = New System.Drawing.Size(25, 25)
        Me.btnWhatsSend.Name = "btnWhatsSend"
        '
        'lblSupplier
        '
        resources.ApplyResources(Me.lblSupplier, "lblSupplier")
        Me.lblSupplier.Appearance.Font = CType(resources.GetObject("lblSupplier.Appearance.Font"), System.Drawing.Font)
        Me.lblSupplier.Appearance.Options.UseFont = True
        Me.lblSupplier.Name = "lblSupplier"
        '
        'cmbSupplier
        '
        resources.ApplyResources(Me.cmbSupplier, "cmbSupplier")
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Properties.Appearance.Font = CType(resources.GetObject("cmbSupplier.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cmbSupplier.Properties.Appearance.Options.UseFont = True
        Me.cmbSupplier.Properties.NullText = resources.GetString("cmbSupplier.Properties.NullText")
        Me.cmbSupplier.Properties.PopupWidth = 350
        '
        'chkAllSuppliers
        '
        resources.ApplyResources(Me.chkAllSuppliers, "chkAllSuppliers")
        Me.chkAllSuppliers.Name = "chkAllSuppliers"
        Me.chkAllSuppliers.Properties.Appearance.Font = CType(resources.GetObject("chkAllSuppliers.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkAllSuppliers.Properties.Appearance.Options.UseFont = True
        Me.chkAllSuppliers.Properties.Caption = resources.GetString("chkAllSuppliers.Properties.Caption")
        Me.chkAllSuppliers.Properties.DisplayValueChecked = resources.GetString("chkAllSuppliers.Properties.DisplayValueChecked")
        Me.chkAllSuppliers.Properties.DisplayValueGrayed = resources.GetString("chkAllSuppliers.Properties.DisplayValueGrayed")
        Me.chkAllSuppliers.Properties.DisplayValueUnchecked = resources.GetString("chkAllSuppliers.Properties.DisplayValueUnchecked")
        Me.chkAllSuppliers.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkAllSuppliers.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'lblSearch
        '
        resources.ApplyResources(Me.lblSearch, "lblSearch")
        Me.lblSearch.Appearance.Font = CType(resources.GetObject("lblSearch.Appearance.Font"), System.Drawing.Font)
        Me.lblSearch.Appearance.Options.UseFont = True
        Me.lblSearch.Name = "lblSearch"
        '
        'txtSearch
        '
        resources.ApplyResources(Me.txtSearch, "txtSearch")
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Properties.Appearance.Font = CType(resources.GetObject("txtSearch.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtSearch.Properties.Appearance.Options.UseFont = True
        '
        'lblDateFrom
        '
        resources.ApplyResources(Me.lblDateFrom, "lblDateFrom")
        Me.lblDateFrom.Appearance.Font = CType(resources.GetObject("lblDateFrom.Appearance.Font"), System.Drawing.Font)
        Me.lblDateFrom.Appearance.Options.UseFont = True
        Me.lblDateFrom.Name = "lblDateFrom"
        '
        'dtFrom
        '
        resources.ApplyResources(Me.dtFrom, "dtFrom")
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Properties.Appearance.Font = CType(resources.GetObject("dtFrom.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtFrom.Properties.Appearance.Options.UseFont = True
        Me.dtFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtFrom.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtFrom.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblDateTo
        '
        resources.ApplyResources(Me.lblDateTo, "lblDateTo")
        Me.lblDateTo.Appearance.Font = CType(resources.GetObject("lblDateTo.Appearance.Font"), System.Drawing.Font)
        Me.lblDateTo.Appearance.Options.UseFont = True
        Me.lblDateTo.Name = "lblDateTo"
        '
        'dtTo
        '
        resources.ApplyResources(Me.dtTo, "dtTo")
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Properties.Appearance.Font = CType(resources.GetObject("dtTo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtTo.Properties.Appearance.Options.UseFont = True
        Me.dtTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTo.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btnApplyFilters
        '
        resources.ApplyResources(Me.btnApplyFilters, "btnApplyFilters")
        Me.btnApplyFilters.Appearance.Font = CType(resources.GetObject("btnApplyFilters.Appearance.Font"), System.Drawing.Font)
        Me.btnApplyFilters.Appearance.Options.UseFont = True
        Me.btnApplyFilters.ImageOptions.ImageKey = resources.GetString("btnApplyFilters.ImageOptions.ImageKey")
        Me.btnApplyFilters.Name = "btnApplyFilters"
        '
        'btnReport2
        '
        resources.ApplyResources(Me.btnReport2, "btnReport2")
        Me.btnReport2.Appearance.Font = CType(resources.GetObject("btnReport2.Appearance.Font"), System.Drawing.Font)
        Me.btnReport2.Appearance.Options.UseFont = True
        Me.btnReport2.ImageOptions.ImageKey = resources.GetString("btnReport2.ImageOptions.ImageKey")
        Me.btnReport2.Name = "btnReport2"
        '
        'btnReport
        '
        resources.ApplyResources(Me.btnReport, "btnReport")
        Me.btnReport.Appearance.Font = CType(resources.GetObject("btnReport.Appearance.Font"), System.Drawing.Font)
        Me.btnReport.Appearance.Options.UseFont = True
        Me.btnReport.ImageOptions.ImageKey = resources.GetString("btnReport.ImageOptions.ImageKey")
        Me.btnReport.Name = "btnReport"
        '
        'btnClearFilters
        '
        resources.ApplyResources(Me.btnClearFilters, "btnClearFilters")
        Me.btnClearFilters.Appearance.Font = CType(resources.GetObject("btnClearFilters.Appearance.Font"), System.Drawing.Font)
        Me.btnClearFilters.Appearance.Options.UseFont = True
        Me.btnClearFilters.ImageOptions.ImageKey = resources.GetString("btnClearFilters.ImageOptions.ImageKey")
        Me.btnClearFilters.Name = "btnClearFilters"
        '
        'tabMain
        '
        resources.ApplyResources(Me.tabMain, "tabMain")
        Me.tabMain.AppearancePage.Header.Font = CType(resources.GetObject("tabMain.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.tabMain.AppearancePage.Header.Options.UseFont = True
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedTabPage = Me.tabInvoices
        Me.tabMain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabInvoices, Me.tabSummary})
        '
        'tabInvoices
        '
        resources.ApplyResources(Me.tabInvoices, "tabInvoices")
        Me.tabInvoices.Controls.Add(Me.grdDetails)
        Me.tabInvoices.Controls.Add(Me.panelHeader)
        Me.tabInvoices.Controls.Add(Me.grdHeaders)
        Me.tabInvoices.Controls.Add(Me.pnlHeaders)
        Me.tabInvoices.Name = "tabInvoices"
        '
        'grdDetails
        '
        resources.ApplyResources(Me.grdDetails, "grdDetails")
        Me.grdDetails.EmbeddedNavigator.AccessibleDescription = resources.GetString("grdDetails.EmbeddedNavigator.AccessibleDescription")
        Me.grdDetails.EmbeddedNavigator.AccessibleName = resources.GetString("grdDetails.EmbeddedNavigator.AccessibleName")
        Me.grdDetails.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("grdDetails.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.grdDetails.EmbeddedNavigator.Anchor = CType(resources.GetObject("grdDetails.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.grdDetails.EmbeddedNavigator.AutoSize = CType(resources.GetObject("grdDetails.EmbeddedNavigator.AutoSize"), Boolean)
        Me.grdDetails.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("grdDetails.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.grdDetails.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("grdDetails.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.grdDetails.EmbeddedNavigator.ImeMode = CType(resources.GetObject("grdDetails.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.grdDetails.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("grdDetails.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.grdDetails.EmbeddedNavigator.TextLocation = CType(resources.GetObject("grdDetails.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.grdDetails.EmbeddedNavigator.ToolTip = resources.GetString("grdDetails.EmbeddedNavigator.ToolTip")
        Me.grdDetails.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("grdDetails.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.grdDetails.EmbeddedNavigator.ToolTipTitle = resources.GetString("grdDetails.EmbeddedNavigator.ToolTipTitle")
        Me.grdDetails.MainView = Me.viewDetails
        Me.grdDetails.Name = "grdDetails"
        Me.grdDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewDetails})
        '
        'viewDetails
        '
        resources.ApplyResources(Me.viewDetails, "viewDetails")
        Me.viewDetails.Appearance.HeaderPanel.Font = CType(resources.GetObject("viewDetails.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.viewDetails.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewDetails.Appearance.Row.Font = CType(resources.GetObject("viewDetails.Appearance.Row.Font"), System.Drawing.Font)
        Me.viewDetails.Appearance.Row.Options.UseFont = True
        Me.viewDetails.GridControl = Me.grdDetails
        Me.viewDetails.Name = "viewDetails"
        Me.viewDetails.OptionsBehavior.Editable = False
        Me.viewDetails.OptionsView.ColumnAutoWidth = False
        Me.viewDetails.OptionsView.EnableAppearanceEvenRow = True
        Me.viewDetails.OptionsView.EnableAppearanceOddRow = True
        Me.viewDetails.OptionsView.ShowFooter = True
        Me.viewDetails.OptionsView.ShowGroupPanel = False
        '
        'panelHeader
        '
        resources.ApplyResources(Me.panelHeader, "panelHeader")
        Me.panelHeader.Controls.Add(Me.lblHdrInvoice)
        Me.panelHeader.Controls.Add(Me.lblHdrSupplier)
        Me.panelHeader.Controls.Add(Me.lblHdrDate)
        Me.panelHeader.Controls.Add(Me.lblHdrTotal)
        Me.panelHeader.Name = "panelHeader"
        '
        'lblHdrInvoice
        '
        resources.ApplyResources(Me.lblHdrInvoice, "lblHdrInvoice")
        Me.lblHdrInvoice.Appearance.Font = CType(resources.GetObject("lblHdrInvoice.Appearance.Font"), System.Drawing.Font)
        Me.lblHdrInvoice.Appearance.Options.UseFont = True
        Me.lblHdrInvoice.Name = "lblHdrInvoice"
        '
        'lblHdrSupplier
        '
        resources.ApplyResources(Me.lblHdrSupplier, "lblHdrSupplier")
        Me.lblHdrSupplier.Appearance.Font = CType(resources.GetObject("lblHdrSupplier.Appearance.Font"), System.Drawing.Font)
        Me.lblHdrSupplier.Appearance.Options.UseFont = True
        Me.lblHdrSupplier.Name = "lblHdrSupplier"
        '
        'lblHdrDate
        '
        resources.ApplyResources(Me.lblHdrDate, "lblHdrDate")
        Me.lblHdrDate.Appearance.Font = CType(resources.GetObject("lblHdrDate.Appearance.Font"), System.Drawing.Font)
        Me.lblHdrDate.Appearance.Options.UseFont = True
        Me.lblHdrDate.Name = "lblHdrDate"
        '
        'lblHdrTotal
        '
        resources.ApplyResources(Me.lblHdrTotal, "lblHdrTotal")
        Me.lblHdrTotal.Appearance.Font = CType(resources.GetObject("lblHdrTotal.Appearance.Font"), System.Drawing.Font)
        Me.lblHdrTotal.Appearance.Options.UseFont = True
        Me.lblHdrTotal.Name = "lblHdrTotal"
        '
        'grdHeaders
        '
        resources.ApplyResources(Me.grdHeaders, "grdHeaders")
        Me.grdHeaders.EmbeddedNavigator.AccessibleDescription = resources.GetString("grdHeaders.EmbeddedNavigator.AccessibleDescription")
        Me.grdHeaders.EmbeddedNavigator.AccessibleName = resources.GetString("grdHeaders.EmbeddedNavigator.AccessibleName")
        Me.grdHeaders.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("grdHeaders.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.grdHeaders.EmbeddedNavigator.Anchor = CType(resources.GetObject("grdHeaders.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.grdHeaders.EmbeddedNavigator.AutoSize = CType(resources.GetObject("grdHeaders.EmbeddedNavigator.AutoSize"), Boolean)
        Me.grdHeaders.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("grdHeaders.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.grdHeaders.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("grdHeaders.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.grdHeaders.EmbeddedNavigator.ImeMode = CType(resources.GetObject("grdHeaders.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.grdHeaders.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("grdHeaders.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.grdHeaders.EmbeddedNavigator.TextLocation = CType(resources.GetObject("grdHeaders.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.grdHeaders.EmbeddedNavigator.ToolTip = resources.GetString("grdHeaders.EmbeddedNavigator.ToolTip")
        Me.grdHeaders.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("grdHeaders.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.grdHeaders.EmbeddedNavigator.ToolTipTitle = resources.GetString("grdHeaders.EmbeddedNavigator.ToolTipTitle")
        Me.grdHeaders.MainView = Me.viewHeaders
        Me.grdHeaders.Name = "grdHeaders"
        Me.grdHeaders.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewHeaders})
        '
        'viewHeaders
        '
        resources.ApplyResources(Me.viewHeaders, "viewHeaders")
        Me.viewHeaders.Appearance.HeaderPanel.Font = CType(resources.GetObject("viewHeaders.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.viewHeaders.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewHeaders.Appearance.Row.Font = CType(resources.GetObject("viewHeaders.Appearance.Row.Font"), System.Drawing.Font)
        Me.viewHeaders.Appearance.Row.Options.UseFont = True
        Me.viewHeaders.GridControl = Me.grdHeaders
        Me.viewHeaders.Name = "viewHeaders"
        Me.viewHeaders.OptionsBehavior.Editable = False
        Me.viewHeaders.OptionsView.ColumnAutoWidth = False
        Me.viewHeaders.OptionsView.EnableAppearanceEvenRow = True
        Me.viewHeaders.OptionsView.EnableAppearanceOddRow = True
        Me.viewHeaders.OptionsView.ShowAutoFilterRow = True
        Me.viewHeaders.OptionsView.ShowGroupPanel = False
        '
        'pnlHeaders
        '
        resources.ApplyResources(Me.pnlHeaders, "pnlHeaders")
        Me.pnlHeaders.Controls.Add(Me.lblHdrListTitle)
        Me.pnlHeaders.Name = "pnlHeaders"
        '
        'lblHdrListTitle
        '
        resources.ApplyResources(Me.lblHdrListTitle, "lblHdrListTitle")
        Me.lblHdrListTitle.Appearance.Font = CType(resources.GetObject("lblHdrListTitle.Appearance.Font"), System.Drawing.Font)
        Me.lblHdrListTitle.Appearance.Options.UseFont = True
        Me.lblHdrListTitle.Name = "lblHdrListTitle"
        '
        'tabSummary
        '
        resources.ApplyResources(Me.tabSummary, "tabSummary")
        Me.tabSummary.Controls.Add(Me.grdSummary)
        Me.tabSummary.Name = "tabSummary"
        '
        'grdSummary
        '
        resources.ApplyResources(Me.grdSummary, "grdSummary")
        Me.grdSummary.EmbeddedNavigator.AccessibleDescription = resources.GetString("grdSummary.EmbeddedNavigator.AccessibleDescription")
        Me.grdSummary.EmbeddedNavigator.AccessibleName = resources.GetString("grdSummary.EmbeddedNavigator.AccessibleName")
        Me.grdSummary.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("grdSummary.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.grdSummary.EmbeddedNavigator.Anchor = CType(resources.GetObject("grdSummary.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.grdSummary.EmbeddedNavigator.AutoSize = CType(resources.GetObject("grdSummary.EmbeddedNavigator.AutoSize"), Boolean)
        Me.grdSummary.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("grdSummary.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.grdSummary.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("grdSummary.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.grdSummary.EmbeddedNavigator.ImeMode = CType(resources.GetObject("grdSummary.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.grdSummary.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("grdSummary.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.grdSummary.EmbeddedNavigator.TextLocation = CType(resources.GetObject("grdSummary.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.grdSummary.EmbeddedNavigator.ToolTip = resources.GetString("grdSummary.EmbeddedNavigator.ToolTip")
        Me.grdSummary.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("grdSummary.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.grdSummary.EmbeddedNavigator.ToolTipTitle = resources.GetString("grdSummary.EmbeddedNavigator.ToolTipTitle")
        Me.grdSummary.MainView = Me.viewSummary
        Me.grdSummary.Name = "grdSummary"
        Me.grdSummary.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewSummary})
        '
        'viewSummary
        '
        resources.ApplyResources(Me.viewSummary, "viewSummary")
        Me.viewSummary.Appearance.HeaderPanel.Font = CType(resources.GetObject("viewSummary.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.viewSummary.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewSummary.Appearance.Row.Font = CType(resources.GetObject("viewSummary.Appearance.Row.Font"), System.Drawing.Font)
        Me.viewSummary.Appearance.Row.Options.UseFont = True
        Me.viewSummary.GridControl = Me.grdSummary
        Me.viewSummary.Name = "viewSummary"
        Me.viewSummary.OptionsBehavior.Editable = False
        Me.viewSummary.OptionsView.ColumnAutoWidth = False
        Me.viewSummary.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSummary.OptionsView.EnableAppearanceOddRow = True
        Me.viewSummary.OptionsView.ShowFooter = True
        Me.viewSummary.OptionsView.ShowGroupPanel = False
        '
        'FrmSuppInvoices
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.panelFilters)
        Me.Name = "FrmSuppInvoices"
        CType(Me.panelFilters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelFilters.ResumeLayout(False)
        Me.panelFilters.PerformLayout()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllSuppliers.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.tabInvoices.ResumeLayout(False)
        CType(Me.grdDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelHeader.ResumeLayout(False)
        Me.panelHeader.PerformLayout()
        CType(Me.grdHeaders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewHeaders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlHeaders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeaders.ResumeLayout(False)
        Me.pnlHeaders.PerformLayout()
        Me.tabSummary.ResumeLayout(False)
        CType(Me.grdSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnReport As SimpleButton
    Friend WithEvents btnReport2 As SimpleButton
    Friend WithEvents btnWhatsSend As SimpleButton
    Friend WithEvents LabelControl3 As LabelControl
    Friend WithEvents RadioLang As RadioGroup
End Class
