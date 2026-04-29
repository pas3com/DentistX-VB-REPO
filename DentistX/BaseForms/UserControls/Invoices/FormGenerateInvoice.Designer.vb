<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormGenerateInvoice
    Inherits DevExpress.XtraEditors.XtraForm


    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormGenerateInvoice))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New DevExpress.XtraEditors.LabelControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New DevExpress.XtraEditors.GroupControl()
        Me.SpinTax = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.lblPatientID = New DevExpress.XtraEditors.LabelControl()
        Me.Label2 = New DevExpress.XtraEditors.LabelControl()
        Me.cbPatients = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New DevExpress.XtraEditors.GroupControl()
        Me.ctlTabs = New DevExpress.XtraTab.XtraTabControl()
        Me.TreatPage = New DevExpress.XtraTab.XtraTabPage()
        Me.TreatmentsGrid = New DevExpress.XtraGrid.GridControl()
        Me.GridViewTrts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTrtID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDiscount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colInvoiced = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PaymentPage = New DevExpress.XtraTab.XtraTabPage()
        Me.PaymentsGrid = New DevExpress.XtraGrid.GridControl()
        Me.GridViewPays = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSelectp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayIDp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayDatep = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotesp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayValuep = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colInvoicedp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnSelectNone = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSelectAll = New DevExpress.XtraEditors.SimpleButton()
        Me.lblSelectedPayCount = New DevExpress.XtraEditors.LabelControl()
        Me.lblSelectedTrtCount = New DevExpress.XtraEditors.LabelControl()
        Me.GroupBox3 = New DevExpress.XtraEditors.GroupControl()
        Me.txtNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.Label10 = New DevExpress.XtraEditors.LabelControl()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New DevExpress.XtraEditors.LabelControl()
        Me.dtpInvoiceDate = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New DevExpress.XtraEditors.LabelControl()
        Me.lblInvoiceNumber = New DevExpress.XtraEditors.LabelControl()
        Me.Label7 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupBox4 = New DevExpress.XtraEditors.GroupControl()
        Me.chkTax = New DevExpress.XtraEditors.CheckEdit()
        Me.lblTotalAmount = New DevExpress.XtraEditors.LabelControl()
        Me.Label6 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTaxAmount = New DevExpress.XtraEditors.LabelControl()
        Me.LabelTax = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalPays = New DevExpress.XtraEditors.LabelControl()
        Me.lblDiscount = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.Label4 = New DevExpress.XtraEditors.LabelControl()
        Me.lblSubTotal = New DevExpress.XtraEditors.LabelControl()
        Me.Label3 = New DevExpress.XtraEditors.LabelControl()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnViewInvoices = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPreview = New DevExpress.XtraEditors.SimpleButton()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.GroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.SpinTax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.ctlTabs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ctlTabs.SuspendLayout()
        Me.TreatPage.SuspendLayout()
        CType(Me.TreatmentsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewTrts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PaymentPage.SuspendLayout()
        CType(Me.PaymentsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewPays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.chkTax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Name = "Panel1"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Appearance.Font = CType(resources.GetObject("Label1.Appearance.Font"), System.Drawing.Font)
        Me.Label1.Appearance.ForeColor = System.Drawing.Color.White
        Me.Label1.Appearance.Options.UseFont = True
        Me.Label1.Appearance.Options.UseForeColor = True
        Me.Label1.Name = "Label1"
        '
        'Panel2
        '
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.GroupBox4)
        Me.Panel2.Name = "Panel2"
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.AppearanceCaption.Font = CType(resources.GetObject("GroupBox1.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupBox1.AppearanceCaption.Options.UseFont = True
        Me.GroupBox1.Controls.Add(Me.SpinTax)
        Me.GroupBox1.Controls.Add(Me.LabelControl3)
        Me.GroupBox1.Controls.Add(Me.LabelControl2)
        Me.GroupBox1.Controls.Add(Me.lblPatientID)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cbPatients)
        Me.GroupBox1.Name = "GroupBox1"
        '
        'SpinTax
        '
        resources.ApplyResources(Me.SpinTax, "SpinTax")
        Me.SpinTax.Name = "SpinTax"
        Me.SpinTax.Properties.Appearance.Font = CType(resources.GetObject("SpinTax.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SpinTax.Properties.Appearance.Options.UseFont = True
        Me.SpinTax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("SpinTax.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.SpinTax.Properties.MaxValue = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'lblPatientID
        '
        resources.ApplyResources(Me.lblPatientID, "lblPatientID")
        Me.lblPatientID.Appearance.Font = CType(resources.GetObject("lblPatientID.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientID.Appearance.Options.UseFont = True
        Me.lblPatientID.Name = "lblPatientID"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Appearance.Font = CType(resources.GetObject("Label2.Appearance.Font"), System.Drawing.Font)
        Me.Label2.Appearance.Options.UseFont = True
        Me.Label2.Name = "Label2"
        '
        'cbPatients
        '
        resources.ApplyResources(Me.cbPatients, "cbPatients")
        Me.cbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPatients.FormattingEnabled = True
        Me.cbPatients.Name = "cbPatients"
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.AppearanceCaption.Font = CType(resources.GetObject("GroupBox2.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupBox2.AppearanceCaption.Options.UseFont = True
        Me.GroupBox2.Controls.Add(Me.ctlTabs)
        Me.GroupBox2.Controls.Add(Me.btnSelectNone)
        Me.GroupBox2.Controls.Add(Me.btnSelectAll)
        Me.GroupBox2.Controls.Add(Me.lblSelectedPayCount)
        Me.GroupBox2.Controls.Add(Me.lblSelectedTrtCount)
        Me.GroupBox2.Name = "GroupBox2"
        '
        'ctlTabs
        '
        resources.ApplyResources(Me.ctlTabs, "ctlTabs")
        Me.ctlTabs.AppearancePage.Header.Font = CType(resources.GetObject("ctlTabs.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.ctlTabs.AppearancePage.Header.Options.UseFont = True
        Me.ctlTabs.Name = "ctlTabs"
        Me.ctlTabs.SelectedTabPage = Me.TreatPage
        Me.ctlTabs.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TreatPage, Me.PaymentPage})
        '
        'TreatPage
        '
        resources.ApplyResources(Me.TreatPage, "TreatPage")
        Me.TreatPage.Controls.Add(Me.TreatmentsGrid)
        Me.TreatPage.Name = "TreatPage"
        '
        'TreatmentsGrid
        '
        resources.ApplyResources(Me.TreatmentsGrid, "TreatmentsGrid")
        Me.TreatmentsGrid.EmbeddedNavigator.AccessibleDescription = resources.GetString("TreatmentsGrid.EmbeddedNavigator.AccessibleDescription")
        Me.TreatmentsGrid.EmbeddedNavigator.AccessibleName = resources.GetString("TreatmentsGrid.EmbeddedNavigator.AccessibleName")
        Me.TreatmentsGrid.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("TreatmentsGrid.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.TreatmentsGrid.EmbeddedNavigator.Anchor = CType(resources.GetObject("TreatmentsGrid.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.TreatmentsGrid.EmbeddedNavigator.AutoSize = CType(resources.GetObject("TreatmentsGrid.EmbeddedNavigator.AutoSize"), Boolean)
        Me.TreatmentsGrid.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("TreatmentsGrid.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.TreatmentsGrid.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("TreatmentsGrid.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.TreatmentsGrid.EmbeddedNavigator.ImeMode = CType(resources.GetObject("TreatmentsGrid.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.TreatmentsGrid.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("TreatmentsGrid.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.TreatmentsGrid.EmbeddedNavigator.TextLocation = CType(resources.GetObject("TreatmentsGrid.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.TreatmentsGrid.EmbeddedNavigator.ToolTip = resources.GetString("TreatmentsGrid.EmbeddedNavigator.ToolTip")
        Me.TreatmentsGrid.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("TreatmentsGrid.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.TreatmentsGrid.EmbeddedNavigator.ToolTipTitle = resources.GetString("TreatmentsGrid.EmbeddedNavigator.ToolTipTitle")
        Me.TreatmentsGrid.MainView = Me.GridViewTrts
        Me.TreatmentsGrid.Name = "TreatmentsGrid"
        Me.TreatmentsGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewTrts})
        '
        'GridViewTrts
        '
        resources.ApplyResources(Me.GridViewTrts, "GridViewTrts")
        Me.GridViewTrts.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridViewTrts.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridViewTrts.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewTrts.Appearance.Row.Font = CType(resources.GetObject("GridViewTrts.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridViewTrts.Appearance.Row.Options.UseFont = True
        Me.GridViewTrts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSelect, Me.colTrtID, Me.colDate, Me.colDetail, Me.colValue, Me.colDiscount, Me.colInvoiced})
        Me.GridViewTrts.GridControl = Me.TreatmentsGrid
        Me.GridViewTrts.Name = "GridViewTrts"
        Me.GridViewTrts.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click
        Me.GridViewTrts.OptionsView.EnableAppearanceEvenRow = True
        Me.GridViewTrts.OptionsView.ShowFooter = True
        '
        'colSelect
        '
        resources.ApplyResources(Me.colSelect, "colSelect")
        Me.colSelect.FieldName = "Select"
        Me.colSelect.ImageOptions.ImageKey = resources.GetString("colSelect.ImageOptions.ImageKey")
        Me.colSelect.Name = "colSelect"
        Me.colSelect.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        Me.colSelect.UnboundDataType = GetType(Boolean)
        '
        'colTrtID
        '
        resources.ApplyResources(Me.colTrtID, "colTrtID")
        Me.colTrtID.FieldName = "TrtID"
        Me.colTrtID.ImageOptions.ImageKey = resources.GetString("colTrtID.ImageOptions.ImageKey")
        Me.colTrtID.Name = "colTrtID"
        Me.colTrtID.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colDate
        '
        resources.ApplyResources(Me.colDate, "colDate")
        Me.colDate.FieldName = "Date"
        Me.colDate.ImageOptions.ImageKey = resources.GetString("colDate.ImageOptions.ImageKey")
        Me.colDate.Name = "colDate"
        Me.colDate.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colDetail
        '
        resources.ApplyResources(Me.colDetail, "colDetail")
        Me.colDetail.FieldName = "Detail"
        Me.colDetail.ImageOptions.ImageKey = resources.GetString("colDetail.ImageOptions.ImageKey")
        Me.colDetail.Name = "colDetail"
        Me.colDetail.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colValue
        '
        resources.ApplyResources(Me.colValue, "colValue")
        Me.colValue.FieldName = "Value"
        Me.colValue.ImageOptions.ImageKey = resources.GetString("colValue.ImageOptions.ImageKey")
        Me.colValue.Name = "colValue"
        Me.colValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colDiscount
        '
        resources.ApplyResources(Me.colDiscount, "colDiscount")
        Me.colDiscount.FieldName = "Discount"
        Me.colDiscount.ImageOptions.ImageKey = resources.GetString("colDiscount.ImageOptions.ImageKey")
        Me.colDiscount.Name = "colDiscount"
        Me.colDiscount.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colInvoiced
        '
        resources.ApplyResources(Me.colInvoiced, "colInvoiced")
        Me.colInvoiced.FieldName = "Invoiced"
        Me.colInvoiced.ImageOptions.ImageKey = resources.GetString("colInvoiced.ImageOptions.ImageKey")
        Me.colInvoiced.Name = "colInvoiced"
        Me.colInvoiced.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'PaymentPage
        '
        resources.ApplyResources(Me.PaymentPage, "PaymentPage")
        Me.PaymentPage.Controls.Add(Me.PaymentsGrid)
        Me.PaymentPage.Name = "PaymentPage"
        '
        'PaymentsGrid
        '
        resources.ApplyResources(Me.PaymentsGrid, "PaymentsGrid")
        Me.PaymentsGrid.EmbeddedNavigator.AccessibleDescription = resources.GetString("PaymentsGrid.EmbeddedNavigator.AccessibleDescription")
        Me.PaymentsGrid.EmbeddedNavigator.AccessibleName = resources.GetString("PaymentsGrid.EmbeddedNavigator.AccessibleName")
        Me.PaymentsGrid.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("PaymentsGrid.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.PaymentsGrid.EmbeddedNavigator.Anchor = CType(resources.GetObject("PaymentsGrid.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.PaymentsGrid.EmbeddedNavigator.AutoSize = CType(resources.GetObject("PaymentsGrid.EmbeddedNavigator.AutoSize"), Boolean)
        Me.PaymentsGrid.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("PaymentsGrid.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.PaymentsGrid.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("PaymentsGrid.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.PaymentsGrid.EmbeddedNavigator.ImeMode = CType(resources.GetObject("PaymentsGrid.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.PaymentsGrid.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("PaymentsGrid.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.PaymentsGrid.EmbeddedNavigator.TextLocation = CType(resources.GetObject("PaymentsGrid.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.PaymentsGrid.EmbeddedNavigator.ToolTip = resources.GetString("PaymentsGrid.EmbeddedNavigator.ToolTip")
        Me.PaymentsGrid.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("PaymentsGrid.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.PaymentsGrid.EmbeddedNavigator.ToolTipTitle = resources.GetString("PaymentsGrid.EmbeddedNavigator.ToolTipTitle")
        Me.PaymentsGrid.MainView = Me.GridViewPays
        Me.PaymentsGrid.Name = "PaymentsGrid"
        Me.PaymentsGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewPays})
        '
        'GridViewPays
        '
        resources.ApplyResources(Me.GridViewPays, "GridViewPays")
        Me.GridViewPays.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridViewPays.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridViewPays.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewPays.Appearance.Row.Font = CType(resources.GetObject("GridViewPays.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridViewPays.Appearance.Row.Options.UseFont = True
        Me.GridViewPays.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSelectp, Me.colPayType, Me.colPayIDp, Me.colPayDatep, Me.colNotesp, Me.colPayValuep, Me.colInvoicedp})
        Me.GridViewPays.GridControl = Me.PaymentsGrid
        Me.GridViewPays.Name = "GridViewPays"
        Me.GridViewPays.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click
        Me.GridViewPays.OptionsView.EnableAppearanceEvenRow = True
        Me.GridViewPays.OptionsView.ShowFooter = True
        '
        'colSelectp
        '
        resources.ApplyResources(Me.colSelectp, "colSelectp")
        Me.colSelectp.FieldName = "Select"
        Me.colSelectp.ImageOptions.ImageKey = resources.GetString("colSelectp.ImageOptions.ImageKey")
        Me.colSelectp.Name = "colSelectp"
        Me.colSelectp.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        Me.colSelectp.UnboundDataType = GetType(Boolean)
        '
        'colPayType
        '
        resources.ApplyResources(Me.colPayType, "colPayType")
        Me.colPayType.FieldName = "PayType"
        Me.colPayType.ImageOptions.ImageKey = resources.GetString("colPayType.ImageOptions.ImageKey")
        Me.colPayType.Name = "colPayType"
        Me.colPayType.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colPayIDp
        '
        resources.ApplyResources(Me.colPayIDp, "colPayIDp")
        Me.colPayIDp.FieldName = "PayID"
        Me.colPayIDp.ImageOptions.ImageKey = resources.GetString("colPayIDp.ImageOptions.ImageKey")
        Me.colPayIDp.Name = "colPayIDp"
        '
        'colPayDatep
        '
        resources.ApplyResources(Me.colPayDatep, "colPayDatep")
        Me.colPayDatep.FieldName = "PayDate"
        Me.colPayDatep.ImageOptions.ImageKey = resources.GetString("colPayDatep.ImageOptions.ImageKey")
        Me.colPayDatep.Name = "colPayDatep"
        Me.colPayDatep.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colNotesp
        '
        resources.ApplyResources(Me.colNotesp, "colNotesp")
        Me.colNotesp.FieldName = "Notes"
        Me.colNotesp.ImageOptions.ImageKey = resources.GetString("colNotesp.ImageOptions.ImageKey")
        Me.colNotesp.Name = "colNotesp"
        Me.colNotesp.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colPayValuep
        '
        resources.ApplyResources(Me.colPayValuep, "colPayValuep")
        Me.colPayValuep.FieldName = "PayValue"
        Me.colPayValuep.ImageOptions.ImageKey = resources.GetString("colPayValuep.ImageOptions.ImageKey")
        Me.colPayValuep.Name = "colPayValuep"
        Me.colPayValuep.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colInvoicedp
        '
        resources.ApplyResources(Me.colInvoicedp, "colInvoicedp")
        Me.colInvoicedp.FieldName = "Invoiced"
        Me.colInvoicedp.ImageOptions.ImageKey = resources.GetString("colInvoicedp.ImageOptions.ImageKey")
        Me.colInvoicedp.Name = "colInvoicedp"
        Me.colInvoicedp.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'btnSelectNone
        '
        resources.ApplyResources(Me.btnSelectNone, "btnSelectNone")
        Me.btnSelectNone.Appearance.Font = CType(resources.GetObject("btnSelectNone.Appearance.Font"), System.Drawing.Font)
        Me.btnSelectNone.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.btnSelectNone.Appearance.Options.UseFont = True
        Me.btnSelectNone.Appearance.Options.UseForeColor = True
        Me.btnSelectNone.ImageOptions.ImageKey = resources.GetString("btnSelectNone.ImageOptions.ImageKey")
        Me.btnSelectNone.Name = "btnSelectNone"
        '
        'btnSelectAll
        '
        resources.ApplyResources(Me.btnSelectAll, "btnSelectAll")
        Me.btnSelectAll.Appearance.Font = CType(resources.GetObject("btnSelectAll.Appearance.Font"), System.Drawing.Font)
        Me.btnSelectAll.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.btnSelectAll.Appearance.Options.UseFont = True
        Me.btnSelectAll.Appearance.Options.UseForeColor = True
        Me.btnSelectAll.ImageOptions.ImageKey = resources.GetString("btnSelectAll.ImageOptions.ImageKey")
        Me.btnSelectAll.Name = "btnSelectAll"
        '
        'lblSelectedPayCount
        '
        resources.ApplyResources(Me.lblSelectedPayCount, "lblSelectedPayCount")
        Me.lblSelectedPayCount.Appearance.Font = CType(resources.GetObject("lblSelectedPayCount.Appearance.Font"), System.Drawing.Font)
        Me.lblSelectedPayCount.Appearance.Options.UseFont = True
        Me.lblSelectedPayCount.Name = "lblSelectedPayCount"
        '
        'lblSelectedTrtCount
        '
        resources.ApplyResources(Me.lblSelectedTrtCount, "lblSelectedTrtCount")
        Me.lblSelectedTrtCount.Appearance.Font = CType(resources.GetObject("lblSelectedTrtCount.Appearance.Font"), System.Drawing.Font)
        Me.lblSelectedTrtCount.Appearance.Options.UseFont = True
        Me.lblSelectedTrtCount.Name = "lblSelectedTrtCount"
        '
        'GroupBox3
        '
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.AppearanceCaption.Font = CType(resources.GetObject("GroupBox3.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupBox3.AppearanceCaption.Options.UseFont = True
        Me.GroupBox3.Controls.Add(Me.txtNotes)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.dtpDueDate)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.dtpInvoiceDate)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.lblInvoiceNumber)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Name = "GroupBox3"
        '
        'txtNotes
        '
        resources.ApplyResources(Me.txtNotes, "txtNotes")
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Properties.Appearance.Font = CType(resources.GetObject("txtNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtNotes.Properties.Appearance.Options.UseFont = True
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Appearance.Font = CType(resources.GetObject("Label10.Appearance.Font"), System.Drawing.Font)
        Me.Label10.Appearance.Options.UseFont = True
        Me.Label10.Name = "Label10"
        '
        'dtpDueDate
        '
        resources.ApplyResources(Me.dtpDueDate, "dtpDueDate")
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDueDate.Name = "dtpDueDate"
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Appearance.Font = CType(resources.GetObject("Label9.Appearance.Font"), System.Drawing.Font)
        Me.Label9.Appearance.Options.UseFont = True
        Me.Label9.Name = "Label9"
        '
        'dtpInvoiceDate
        '
        resources.ApplyResources(Me.dtpInvoiceDate, "dtpInvoiceDate")
        Me.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInvoiceDate.Name = "dtpInvoiceDate"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Appearance.Font = CType(resources.GetObject("Label8.Appearance.Font"), System.Drawing.Font)
        Me.Label8.Appearance.Options.UseFont = True
        Me.Label8.Name = "Label8"
        '
        'lblInvoiceNumber
        '
        resources.ApplyResources(Me.lblInvoiceNumber, "lblInvoiceNumber")
        Me.lblInvoiceNumber.Appearance.Font = CType(resources.GetObject("lblInvoiceNumber.Appearance.Font"), System.Drawing.Font)
        Me.lblInvoiceNumber.Appearance.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblInvoiceNumber.Appearance.Options.UseFont = True
        Me.lblInvoiceNumber.Appearance.Options.UseForeColor = True
        Me.lblInvoiceNumber.Name = "lblInvoiceNumber"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Appearance.Font = CType(resources.GetObject("Label7.Appearance.Font"), System.Drawing.Font)
        Me.Label7.Appearance.Options.UseFont = True
        Me.Label7.Name = "Label7"
        '
        'GroupBox4
        '
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.AppearanceCaption.Font = CType(resources.GetObject("GroupBox4.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupBox4.AppearanceCaption.Options.UseFont = True
        Me.GroupBox4.Controls.Add(Me.chkTax)
        Me.GroupBox4.Controls.Add(Me.lblTotalAmount)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.lblTaxAmount)
        Me.GroupBox4.Controls.Add(Me.LabelTax)
        Me.GroupBox4.Controls.Add(Me.lblTotalPays)
        Me.GroupBox4.Controls.Add(Me.lblDiscount)
        Me.GroupBox4.Controls.Add(Me.LabelControl1)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.lblSubTotal)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Name = "GroupBox4"
        '
        'chkTax
        '
        resources.ApplyResources(Me.chkTax, "chkTax")
        Me.chkTax.Name = "chkTax"
        Me.chkTax.Properties.Appearance.Font = CType(resources.GetObject("chkTax.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkTax.Properties.Appearance.Options.UseFont = True
        Me.chkTax.Properties.Caption = resources.GetString("chkTax.Properties.Caption")
        Me.chkTax.Properties.DisplayValueChecked = resources.GetString("chkTax.Properties.DisplayValueChecked")
        Me.chkTax.Properties.DisplayValueGrayed = resources.GetString("chkTax.Properties.DisplayValueGrayed")
        Me.chkTax.Properties.DisplayValueUnchecked = resources.GetString("chkTax.Properties.DisplayValueUnchecked")
        Me.chkTax.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkTax.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'lblTotalAmount
        '
        resources.ApplyResources(Me.lblTotalAmount, "lblTotalAmount")
        Me.lblTotalAmount.Appearance.Font = CType(resources.GetObject("lblTotalAmount.Appearance.Font"), System.Drawing.Font)
        Me.lblTotalAmount.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblTotalAmount.Appearance.Options.UseFont = True
        Me.lblTotalAmount.Appearance.Options.UseForeColor = True
        Me.lblTotalAmount.Name = "lblTotalAmount"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Appearance.Font = CType(resources.GetObject("Label6.Appearance.Font"), System.Drawing.Font)
        Me.Label6.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Appearance.Options.UseFont = True
        Me.Label6.Appearance.Options.UseForeColor = True
        Me.Label6.Name = "Label6"
        '
        'lblTaxAmount
        '
        resources.ApplyResources(Me.lblTaxAmount, "lblTaxAmount")
        Me.lblTaxAmount.Appearance.Font = CType(resources.GetObject("lblTaxAmount.Appearance.Font"), System.Drawing.Font)
        Me.lblTaxAmount.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblTaxAmount.Appearance.Options.UseFont = True
        Me.lblTaxAmount.Appearance.Options.UseForeColor = True
        Me.lblTaxAmount.Name = "lblTaxAmount"
        '
        'LabelTax
        '
        resources.ApplyResources(Me.LabelTax, "LabelTax")
        Me.LabelTax.Appearance.Font = CType(resources.GetObject("LabelTax.Appearance.Font"), System.Drawing.Font)
        Me.LabelTax.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LabelTax.Appearance.Options.UseFont = True
        Me.LabelTax.Appearance.Options.UseForeColor = True
        Me.LabelTax.Name = "LabelTax"
        '
        'lblTotalPays
        '
        resources.ApplyResources(Me.lblTotalPays, "lblTotalPays")
        Me.lblTotalPays.Appearance.Font = CType(resources.GetObject("lblTotalPays.Appearance.Font"), System.Drawing.Font)
        Me.lblTotalPays.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblTotalPays.Appearance.Options.UseFont = True
        Me.lblTotalPays.Appearance.Options.UseForeColor = True
        Me.lblTotalPays.Name = "lblTotalPays"
        '
        'lblDiscount
        '
        resources.ApplyResources(Me.lblDiscount, "lblDiscount")
        Me.lblDiscount.Appearance.Font = CType(resources.GetObject("lblDiscount.Appearance.Font"), System.Drawing.Font)
        Me.lblDiscount.Appearance.Options.UseFont = True
        Me.lblDiscount.Name = "lblDiscount"
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Appearance.Font = CType(resources.GetObject("Label4.Appearance.Font"), System.Drawing.Font)
        Me.Label4.Appearance.Options.UseFont = True
        Me.Label4.Name = "Label4"
        '
        'lblSubTotal
        '
        resources.ApplyResources(Me.lblSubTotal, "lblSubTotal")
        Me.lblSubTotal.Appearance.Font = CType(resources.GetObject("lblSubTotal.Appearance.Font"), System.Drawing.Font)
        Me.lblSubTotal.Appearance.Options.UseFont = True
        Me.lblSubTotal.Name = "lblSubTotal"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Appearance.Font = CType(resources.GetObject("Label3.Appearance.Font"), System.Drawing.Font)
        Me.Label3.Appearance.Options.UseFont = True
        Me.Label3.Name = "Label3"
        '
        'Panel3
        '
        resources.ApplyResources(Me.Panel3, "Panel3")
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.Panel3.Controls.Add(Me.btnViewInvoices)
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Controls.Add(Me.btnSave)
        Me.Panel3.Controls.Add(Me.btnPreview)
        Me.Panel3.Name = "Panel3"
        '
        'btnViewInvoices
        '
        resources.ApplyResources(Me.btnViewInvoices, "btnViewInvoices")
        Me.btnViewInvoices.Appearance.Font = CType(resources.GetObject("btnViewInvoices.Appearance.Font"), System.Drawing.Font)
        Me.btnViewInvoices.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.btnViewInvoices.Appearance.Options.UseFont = True
        Me.btnViewInvoices.Appearance.Options.UseForeColor = True
        Me.btnViewInvoices.ImageOptions.ImageKey = resources.GetString("btnViewInvoices.ImageOptions.ImageKey")
        Me.btnViewInvoices.Name = "btnViewInvoices"
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.Appearance.Options.UseForeColor = True
        Me.btnCancel.ImageOptions.ImageKey = resources.GetString("btnCancel.ImageOptions.ImageKey")
        Me.btnCancel.Name = "btnCancel"
        '
        'btnSave
        '
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Appearance.Font = CType(resources.GetObject("btnSave.Appearance.Font"), System.Drawing.Font)
        Me.btnSave.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.btnSave.Appearance.Options.UseFont = True
        Me.btnSave.Appearance.Options.UseForeColor = True
        Me.btnSave.ImageOptions.ImageKey = resources.GetString("btnSave.ImageOptions.ImageKey")
        Me.btnSave.Name = "btnSave"
        '
        'btnPreview
        '
        resources.ApplyResources(Me.btnPreview, "btnPreview")
        Me.btnPreview.Appearance.Font = CType(resources.GetObject("btnPreview.Appearance.Font"), System.Drawing.Font)
        Me.btnPreview.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.btnPreview.Appearance.Options.UseFont = True
        Me.btnPreview.Appearance.Options.UseForeColor = True
        Me.btnPreview.ImageOptions.ImageKey = resources.GetString("btnPreview.ImageOptions.ImageKey")
        Me.btnPreview.Name = "btnPreview"
        '
        'FormGenerateInvoice
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormGenerateInvoice"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.GroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.SpinTax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.ctlTabs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ctlTabs.ResumeLayout(False)
        Me.TreatPage.ResumeLayout(False)
        CType(Me.TreatmentsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewTrts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PaymentPage.ResumeLayout(False)
        CType(Me.PaymentsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewPays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.chkTax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cbPatients As ComboBox
    Friend WithEvents Label2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupBox2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblSelectedTrtCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupBox3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupBox4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblPatientID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents Label10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpDueDate As DateTimePicker
    Friend WithEvents Label9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpInvoiceDate As DateTimePicker
    Friend WithEvents Label8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblInvoiceNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalAmount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTaxAmount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelTax As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDiscount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSubTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnViewInvoices As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TreatmentsGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewTrts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrtID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiscount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colInvoiced As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnSelectAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSelectNone As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkTax As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ctlTabs As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents TreatPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PaymentPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PaymentsGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewPays As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSelectp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayIDp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayDatep As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNotesp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayValuep As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colInvoicedp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblTotalPays As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSelectedPayCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colPayType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SpinTax As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class