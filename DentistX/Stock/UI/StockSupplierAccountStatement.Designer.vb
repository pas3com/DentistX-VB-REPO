<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockSupplierAccountStatement
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

    Friend WithEvents filterPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblSupplier As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _cmbSupplier As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblFrom As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _dateFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents _dateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblView As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _cmbView As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblSearch As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _txtSearch As DevExpress.XtraEditors.TextEdit
    Friend WithEvents _btnRefresh As DevExpress.XtraEditors.SimpleButton

    Friend WithEvents split As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents _gridInvoices As DevExpress.XtraGrid.GridControl
    Friend WithEvents _viewInvoices As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents _gridPayments As DevExpress.XtraGrid.GridControl
    Friend WithEvents _viewPayments As DevExpress.XtraGrid.Views.Grid.GridView

    Friend WithEvents summaryPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblTotalBuysCaption As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalPaymentsCaption As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBalanceCaption As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _lblTotalBuys As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _lblTotalPayments As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _lblBalance As DevExpress.XtraEditors.LabelControl

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StockSupplierAccountStatement))
        Me.split = New DevExpress.XtraEditors.SplitContainerControl()
        Me._gridInvoices = New DevExpress.XtraGrid.GridControl()
        Me._viewInvoices = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me._gridPayments = New DevExpress.XtraGrid.GridControl()
        Me._viewPayments = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.filterPanel = New DevExpress.XtraEditors.PanelControl()
        Me._btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me._txtSearch = New DevExpress.XtraEditors.TextEdit()
        Me.lblSearch = New DevExpress.XtraEditors.LabelControl()
        Me._cmbView = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblView = New DevExpress.XtraEditors.LabelControl()
        Me._dateTo = New DevExpress.XtraEditors.DateEdit()
        Me.lblTo = New DevExpress.XtraEditors.LabelControl()
        Me._dateFrom = New DevExpress.XtraEditors.DateEdit()
        Me.lblFrom = New DevExpress.XtraEditors.LabelControl()
        Me._cmbSupplier = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblSupplier = New DevExpress.XtraEditors.LabelControl()
        Me.summaryPanel = New DevExpress.XtraEditors.PanelControl()
        Me._lblBalance = New DevExpress.XtraEditors.LabelControl()
        Me.lblBalanceCaption = New DevExpress.XtraEditors.LabelControl()
        Me._lblTotalPayments = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalPaymentsCaption = New DevExpress.XtraEditors.LabelControl()
        Me._lblTotalBuys = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalBuysCaption = New DevExpress.XtraEditors.LabelControl()
        CType(Me.split, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.split.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.split.Panel1.SuspendLayout()
        CType(Me.split.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.split.Panel2.SuspendLayout()
        Me.split.SuspendLayout()
        CType(Me._gridInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._viewInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._gridPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._viewPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.filterPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.filterPanel.SuspendLayout()
        CType(Me._txtSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._cmbView.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._cmbSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.summaryPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.summaryPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'split
        '
        resources.ApplyResources(Me.split, "split")
        Me.split.Name = "split"
        '
        'split.Panel1
        '
        Me.split.Panel1.Controls.Add(Me._gridInvoices)
        '
        'split.Panel2
        '
        Me.split.Panel2.Controls.Add(Me._gridPayments)
        Me.split.SplitterPosition = 650
        '
        '_gridInvoices
        '
        resources.ApplyResources(Me._gridInvoices, "_gridInvoices")
        Me._gridInvoices.EmbeddedNavigator.Margin = CType(resources.GetObject("_gridInvoices.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
        Me._gridInvoices.MainView = Me._viewInvoices
        Me._gridInvoices.Name = "_gridInvoices"
        Me._gridInvoices.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me._viewInvoices})
        '
        '_viewInvoices
        '
        Me._viewInvoices.Appearance.HeaderPanel.Font = CType(resources.GetObject("_viewInvoices.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me._viewInvoices.Appearance.HeaderPanel.Options.UseFont = True
        Me._viewInvoices.Appearance.Row.Font = CType(resources.GetObject("_viewInvoices.Appearance.Row.Font"), System.Drawing.Font)
        Me._viewInvoices.Appearance.Row.Options.UseFont = True
        Me._viewInvoices.DetailHeight = 404
        Me._viewInvoices.GridControl = Me._gridInvoices
        Me._viewInvoices.Name = "_viewInvoices"
        Me._viewInvoices.OptionsFind.AlwaysVisible = True
        Me._viewInvoices.OptionsView.ShowGroupPanel = False
        '
        '_gridPayments
        '
        resources.ApplyResources(Me._gridPayments, "_gridPayments")
        Me._gridPayments.EmbeddedNavigator.Margin = CType(resources.GetObject("_gridPayments.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
        Me._gridPayments.MainView = Me._viewPayments
        Me._gridPayments.Name = "_gridPayments"
        Me._gridPayments.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me._viewPayments})
        '
        '_viewPayments
        '
        Me._viewPayments.Appearance.HeaderPanel.Font = CType(resources.GetObject("_viewPayments.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me._viewPayments.Appearance.HeaderPanel.Options.UseFont = True
        Me._viewPayments.Appearance.Row.Font = CType(resources.GetObject("_viewPayments.Appearance.Row.Font"), System.Drawing.Font)
        Me._viewPayments.Appearance.Row.Options.UseFont = True
        Me._viewPayments.DetailHeight = 404
        Me._viewPayments.GridControl = Me._gridPayments
        Me._viewPayments.Name = "_viewPayments"
        Me._viewPayments.OptionsFind.AlwaysVisible = True
        Me._viewPayments.OptionsView.ShowGroupPanel = False
        '
        'filterPanel
        '
        Me.filterPanel.Controls.Add(Me._btnRefresh)
        Me.filterPanel.Controls.Add(Me._txtSearch)
        Me.filterPanel.Controls.Add(Me.lblSearch)
        Me.filterPanel.Controls.Add(Me._cmbView)
        Me.filterPanel.Controls.Add(Me.lblView)
        Me.filterPanel.Controls.Add(Me._dateTo)
        Me.filterPanel.Controls.Add(Me.lblTo)
        Me.filterPanel.Controls.Add(Me._dateFrom)
        Me.filterPanel.Controls.Add(Me.lblFrom)
        Me.filterPanel.Controls.Add(Me._cmbSupplier)
        Me.filterPanel.Controls.Add(Me.lblSupplier)
        resources.ApplyResources(Me.filterPanel, "filterPanel")
        Me.filterPanel.Name = "filterPanel"
        '
        '_btnRefresh
        '
        Me._btnRefresh.Appearance.Font = CType(resources.GetObject("_btnRefresh.Appearance.Font"), System.Drawing.Font)
        Me._btnRefresh.Appearance.Options.UseFont = True
        resources.ApplyResources(Me._btnRefresh, "_btnRefresh")
        Me._btnRefresh.Name = "_btnRefresh"
        '
        '_txtSearch
        '
        resources.ApplyResources(Me._txtSearch, "_txtSearch")
        Me._txtSearch.Name = "_txtSearch"
        Me._txtSearch.Properties.Appearance.Font = CType(resources.GetObject("_txtSearch.Properties.Appearance.Font"), System.Drawing.Font)
        Me._txtSearch.Properties.Appearance.Options.UseFont = True
        '
        'lblSearch
        '
        Me.lblSearch.Appearance.Font = CType(resources.GetObject("lblSearch.Appearance.Font"), System.Drawing.Font)
        Me.lblSearch.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblSearch, "lblSearch")
        Me.lblSearch.Name = "lblSearch"
        '
        '_cmbView
        '
        resources.ApplyResources(Me._cmbView, "_cmbView")
        Me._cmbView.Name = "_cmbView"
        Me._cmbView.Properties.Appearance.Font = CType(resources.GetObject("_cmbView.Properties.Appearance.Font"), System.Drawing.Font)
        Me._cmbView.Properties.Appearance.Options.UseFont = True
        Me._cmbView.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_cmbView.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me._cmbView.Properties.Items.AddRange(New Object() {resources.GetString("_cmbView.Properties.Items"), resources.GetString("_cmbView.Properties.Items1"), resources.GetString("_cmbView.Properties.Items2")})
        '
        'lblView
        '
        Me.lblView.Appearance.Font = CType(resources.GetObject("lblView.Appearance.Font"), System.Drawing.Font)
        Me.lblView.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblView, "lblView")
        Me.lblView.Name = "lblView"
        '
        '_dateTo
        '
        resources.ApplyResources(Me._dateTo, "_dateTo")
        Me._dateTo.Name = "_dateTo"
        Me._dateTo.Properties.Appearance.Font = CType(resources.GetObject("_dateTo.Properties.Appearance.Font"), System.Drawing.Font)
        Me._dateTo.Properties.Appearance.Options.UseFont = True
        Me._dateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateTo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me._dateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateTo.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblTo
        '
        Me.lblTo.Appearance.Font = CType(resources.GetObject("lblTo.Appearance.Font"), System.Drawing.Font)
        Me.lblTo.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblTo, "lblTo")
        Me.lblTo.Name = "lblTo"
        '
        '_dateFrom
        '
        resources.ApplyResources(Me._dateFrom, "_dateFrom")
        Me._dateFrom.Name = "_dateFrom"
        Me._dateFrom.Properties.Appearance.Font = CType(resources.GetObject("_dateFrom.Properties.Appearance.Font"), System.Drawing.Font)
        Me._dateFrom.Properties.Appearance.Options.UseFont = True
        Me._dateFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateFrom.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me._dateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateFrom.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblFrom
        '
        Me.lblFrom.Appearance.Font = CType(resources.GetObject("lblFrom.Appearance.Font"), System.Drawing.Font)
        Me.lblFrom.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblFrom, "lblFrom")
        Me.lblFrom.Name = "lblFrom"
        '
        '_cmbSupplier
        '
        resources.ApplyResources(Me._cmbSupplier, "_cmbSupplier")
        Me._cmbSupplier.Name = "_cmbSupplier"
        Me._cmbSupplier.Properties.Appearance.Font = CType(resources.GetObject("_cmbSupplier.Properties.Appearance.Font"), System.Drawing.Font)
        Me._cmbSupplier.Properties.Appearance.Options.UseFont = True
        Me._cmbSupplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_cmbSupplier.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblSupplier
        '
        Me.lblSupplier.Appearance.Font = CType(resources.GetObject("lblSupplier.Appearance.Font"), System.Drawing.Font)
        Me.lblSupplier.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblSupplier, "lblSupplier")
        Me.lblSupplier.Name = "lblSupplier"
        '
        'summaryPanel
        '
        Me.summaryPanel.Controls.Add(Me._lblBalance)
        Me.summaryPanel.Controls.Add(Me.lblBalanceCaption)
        Me.summaryPanel.Controls.Add(Me._lblTotalPayments)
        Me.summaryPanel.Controls.Add(Me.lblTotalPaymentsCaption)
        Me.summaryPanel.Controls.Add(Me._lblTotalBuys)
        Me.summaryPanel.Controls.Add(Me.lblTotalBuysCaption)
        resources.ApplyResources(Me.summaryPanel, "summaryPanel")
        Me.summaryPanel.Name = "summaryPanel"
        '
        '_lblBalance
        '
        Me._lblBalance.Appearance.Font = CType(resources.GetObject("_lblBalance.Appearance.Font"), System.Drawing.Font)
        Me._lblBalance.Appearance.ForeColor = System.Drawing.Color.Red
        Me._lblBalance.Appearance.Options.UseFont = True
        Me._lblBalance.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me._lblBalance, "_lblBalance")
        Me._lblBalance.Name = "_lblBalance"
        '
        'lblBalanceCaption
        '
        Me.lblBalanceCaption.Appearance.Font = CType(resources.GetObject("lblBalanceCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblBalanceCaption.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblBalanceCaption, "lblBalanceCaption")
        Me.lblBalanceCaption.Name = "lblBalanceCaption"
        '
        '_lblTotalPayments
        '
        Me._lblTotalPayments.Appearance.Font = CType(resources.GetObject("_lblTotalPayments.Appearance.Font"), System.Drawing.Font)
        Me._lblTotalPayments.Appearance.ForeColor = System.Drawing.Color.Green
        Me._lblTotalPayments.Appearance.Options.UseFont = True
        Me._lblTotalPayments.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me._lblTotalPayments, "_lblTotalPayments")
        Me._lblTotalPayments.Name = "_lblTotalPayments"
        '
        'lblTotalPaymentsCaption
        '
        Me.lblTotalPaymentsCaption.Appearance.Font = CType(resources.GetObject("lblTotalPaymentsCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblTotalPaymentsCaption.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblTotalPaymentsCaption, "lblTotalPaymentsCaption")
        Me.lblTotalPaymentsCaption.Name = "lblTotalPaymentsCaption"
        '
        '_lblTotalBuys
        '
        Me._lblTotalBuys.Appearance.Font = CType(resources.GetObject("_lblTotalBuys.Appearance.Font"), System.Drawing.Font)
        Me._lblTotalBuys.Appearance.ForeColor = System.Drawing.Color.Blue
        Me._lblTotalBuys.Appearance.Options.UseFont = True
        Me._lblTotalBuys.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me._lblTotalBuys, "_lblTotalBuys")
        Me._lblTotalBuys.Name = "_lblTotalBuys"
        '
        'lblTotalBuysCaption
        '
        Me.lblTotalBuysCaption.Appearance.Font = CType(resources.GetObject("lblTotalBuysCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblTotalBuysCaption.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblTotalBuysCaption, "lblTotalBuysCaption")
        Me.lblTotalBuysCaption.Name = "lblTotalBuysCaption"
        '
        'StockSupplierAccountStatement
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.split)
        Me.Controls.Add(Me.summaryPanel)
        Me.Controls.Add(Me.filterPanel)
        Me.Name = "StockSupplierAccountStatement"
        CType(Me.split.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.split.Panel1.ResumeLayout(False)
        CType(Me.split.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.split.Panel2.ResumeLayout(False)
        CType(Me.split, System.ComponentModel.ISupportInitialize).EndInit()
        Me.split.ResumeLayout(False)
        CType(Me._gridInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._viewInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._gridPayments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._viewPayments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.filterPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.filterPanel.ResumeLayout(False)
        Me.filterPanel.PerformLayout()
        CType(Me._txtSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._cmbView.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._cmbSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.summaryPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.summaryPanel.ResumeLayout(False)
        Me.summaryPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
End Class
