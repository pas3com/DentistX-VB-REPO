<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockSupplierPaymentsForm
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

    Friend WithEvents _gridInvoices As DevExpress.XtraGrid.GridControl
    Friend WithEvents _viewInvoices As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents _cmbSupplier As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents _gridPayments As DevExpress.XtraGrid.GridControl
    Friend WithEvents _viewPayments As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents _spinAmount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents _datePayment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents _cmbMethod As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents _btnPay As DevExpress.XtraEditors.SimpleButton

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StockSupplierPaymentsForm))
        Me.SplitterCtl = New DevExpress.XtraEditors.SplitContainerControl()
        Me.scannedFilesList = New DevExpress.XtraEditors.ListBoxControl()
        Me.scanPreview = New DevExpress.XtraEditors.PictureEdit()
        Me.split = New DevExpress.XtraEditors.SplitContainerControl()
        Me._gridInvoices = New DevExpress.XtraGrid.GridControl()
        Me._viewInvoices = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.filterPanel = New DevExpress.XtraEditors.PanelControl()
        Me.lblSupplier = New DevExpress.XtraEditors.LabelControl()
        Me._cmbSupplier = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblOpenInvoicesSum = New DevExpress.XtraEditors.LabelControl()
        Me.lblOpenInvoices = New DevExpress.XtraEditors.LabelControl()
        Me.lblPaymentsSum = New DevExpress.XtraEditors.LabelControl()
        Me.lblPayments = New DevExpress.XtraEditors.LabelControl()
        Me.GridTabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.ChqGridPage = New DevExpress.XtraTab.XtraTabPage()
        Me._gridPayments = New DevExpress.XtraGrid.GridControl()
        Me._viewPayments = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ScannedPage = New DevExpress.XtraTab.XtraTabPage()
        Me.AttachedPage = New DevExpress.XtraTab.XtraTabPage()
        Me.GridAttached = New DevExpress.XtraGrid.GridControl()
        Me.ViewAttached = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.payPanel = New DevExpress.XtraEditors.PanelControl()
        Me.GroupCheques = New DevExpress.XtraEditors.GroupControl()
        Me.ChkDrive = New DevExpress.XtraEditors.CheckEdit()
        Me.btnBrowse = New DevExpress.XtraEditors.SimpleButton()
        Me.btnScan = New DevExpress.XtraEditors.SimpleButton()
        Me.btnResetChqs = New DevExpress.XtraEditors.SimpleButton()
        Me.btnScanAndPay = New DevExpress.XtraEditors.SimpleButton()
        Me.dtChqDueDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.txtChqValue = New DevExpress.XtraEditors.TextEdit()
        Me.txtChqBank = New DevExpress.XtraEditors.TextEdit()
        Me.txtChqNumber = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.txtChqOwner = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.txtAccountNumber = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.lblAmount = New DevExpress.XtraEditors.LabelControl()
        Me._spinAmount = New DevExpress.XtraEditors.TextEdit()
        Me.lblDate = New DevExpress.XtraEditors.LabelControl()
        Me._datePayment = New DevExpress.XtraEditors.DateEdit()
        Me.lblMethod = New DevExpress.XtraEditors.LabelControl()
        Me._cmbMethod = New DevExpress.XtraEditors.ComboBoxEdit()
        Me._btnEditPay = New DevExpress.XtraEditors.SimpleButton()
        Me._btnPay = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.SplitterCtl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitterCtl.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitterCtl.Panel1.SuspendLayout()
        CType(Me.SplitterCtl.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitterCtl.Panel2.SuspendLayout()
        Me.SplitterCtl.SuspendLayout()
        CType(Me.scannedFilesList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scanPreview.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.split, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.split.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.split.Panel1.SuspendLayout()
        CType(Me.split.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.split.Panel2.SuspendLayout()
        Me.split.SuspendLayout()
        CType(Me._gridInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._viewInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.filterPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.filterPanel.SuspendLayout()
        CType(Me._cmbSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridTabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GridTabControl.SuspendLayout()
        Me.ChqGridPage.SuspendLayout()
        CType(Me._gridPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._viewPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ScannedPage.SuspendLayout()
        Me.AttachedPage.SuspendLayout()
        CType(Me.GridAttached, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewAttached, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.payPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.payPanel.SuspendLayout()
        CType(Me.GroupCheques, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupCheques.SuspendLayout()
        CType(Me.ChkDrive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtChqDueDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtChqDueDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqBank.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqOwner.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccountNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._spinAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._datePayment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._datePayment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._cmbMethod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitterCtl
        '
        Me.SplitterCtl.CaptionImageOptions.ImageKey = resources.GetString("SplitterCtl.CaptionImageOptions.ImageKey")
        resources.ApplyResources(Me.SplitterCtl, "SplitterCtl")
        Me.SplitterCtl.Name = "SplitterCtl"
        '
        'SplitterCtl.Panel1
        '
        Me.SplitterCtl.Panel1.Controls.Add(Me.scannedFilesList)
        resources.ApplyResources(Me.SplitterCtl.Panel1, "SplitterCtl.Panel1")
        '
        'SplitterCtl.Panel2
        '
        Me.SplitterCtl.Panel2.Controls.Add(Me.scanPreview)
        resources.ApplyResources(Me.SplitterCtl.Panel2, "SplitterCtl.Panel2")
        Me.SplitterCtl.SplitterPosition = 263
        '
        'scannedFilesList
        '
        Me.scannedFilesList.Appearance.Font = CType(resources.GetObject("scannedFilesList.Appearance.Font"), System.Drawing.Font)
        Me.scannedFilesList.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.scannedFilesList, "scannedFilesList")
        Me.scannedFilesList.Name = "scannedFilesList"
        '
        'scanPreview
        '
        resources.ApplyResources(Me.scanPreview, "scanPreview")
        Me.scanPreview.Name = "scanPreview"
        Me.scanPreview.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.scanPreview.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'split
        '
        resources.ApplyResources(Me.split, "split")
        Me.split.Name = "split"
        '
        'split.Panel1
        '
        Me.split.Panel1.Controls.Add(Me._gridInvoices)
        Me.split.Panel1.Controls.Add(Me.filterPanel)
        '
        'split.Panel2
        '
        Me.split.Panel2.Controls.Add(Me.GridTabControl)
        Me.split.Panel2.Controls.Add(Me.payPanel)
        Me.split.SplitterPosition = 604
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
        Me._viewInvoices.OptionsEditForm.PopupEditFormWidth = 933
        '
        'filterPanel
        '
        Me.filterPanel.Controls.Add(Me.lblSupplier)
        Me.filterPanel.Controls.Add(Me._cmbSupplier)
        Me.filterPanel.Controls.Add(Me.lblOpenInvoicesSum)
        Me.filterPanel.Controls.Add(Me.lblOpenInvoices)
        Me.filterPanel.Controls.Add(Me.lblPaymentsSum)
        Me.filterPanel.Controls.Add(Me.lblPayments)
        resources.ApplyResources(Me.filterPanel, "filterPanel")
        Me.filterPanel.Name = "filterPanel"
        '
        'lblSupplier
        '
        Me.lblSupplier.Appearance.Font = CType(resources.GetObject("lblSupplier.Appearance.Font"), System.Drawing.Font)
        Me.lblSupplier.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblSupplier, "lblSupplier")
        Me.lblSupplier.Name = "lblSupplier"
        '
        '_cmbSupplier
        '
        resources.ApplyResources(Me._cmbSupplier, "_cmbSupplier")
        Me._cmbSupplier.Name = "_cmbSupplier"
        Me._cmbSupplier.Properties.Appearance.Font = CType(resources.GetObject("_cmbSupplier.Properties.Appearance.Font"), System.Drawing.Font)
        Me._cmbSupplier.Properties.Appearance.Options.UseFont = True
        '
        'lblOpenInvoicesSum
        '
        Me.lblOpenInvoicesSum.Appearance.Font = CType(resources.GetObject("lblOpenInvoicesSum.Appearance.Font"), System.Drawing.Font)
        Me.lblOpenInvoicesSum.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblOpenInvoicesSum.Appearance.Options.UseFont = True
        Me.lblOpenInvoicesSum.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblOpenInvoicesSum, "lblOpenInvoicesSum")
        Me.lblOpenInvoicesSum.Name = "lblOpenInvoicesSum"
        '
        'lblOpenInvoices
        '
        Me.lblOpenInvoices.Appearance.Font = CType(resources.GetObject("lblOpenInvoices.Appearance.Font"), System.Drawing.Font)
        Me.lblOpenInvoices.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblOpenInvoices, "lblOpenInvoices")
        Me.lblOpenInvoices.Name = "lblOpenInvoices"
        '
        'lblPaymentsSum
        '
        Me.lblPaymentsSum.Appearance.Font = CType(resources.GetObject("lblPaymentsSum.Appearance.Font"), System.Drawing.Font)
        Me.lblPaymentsSum.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblPaymentsSum.Appearance.Options.UseFont = True
        Me.lblPaymentsSum.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblPaymentsSum, "lblPaymentsSum")
        Me.lblPaymentsSum.Name = "lblPaymentsSum"
        '
        'lblPayments
        '
        Me.lblPayments.Appearance.Font = CType(resources.GetObject("lblPayments.Appearance.Font"), System.Drawing.Font)
        Me.lblPayments.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblPayments, "lblPayments")
        Me.lblPayments.Name = "lblPayments"
        '
        'GridTabControl
        '
        Me.GridTabControl.AppearancePage.Header.Font = CType(resources.GetObject("GridTabControl.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.GridTabControl.AppearancePage.Header.Options.UseFont = True
        resources.ApplyResources(Me.GridTabControl, "GridTabControl")
        Me.GridTabControl.Name = "GridTabControl"
        Me.GridTabControl.SelectedTabPage = Me.ChqGridPage
        Me.GridTabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.ChqGridPage, Me.ScannedPage, Me.AttachedPage})
        '
        'ChqGridPage
        '
        Me.ChqGridPage.Controls.Add(Me._gridPayments)
        Me.ChqGridPage.Name = "ChqGridPage"
        resources.ApplyResources(Me.ChqGridPage, "ChqGridPage")
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
        Me._viewPayments.OptionsEditForm.PopupEditFormWidth = 933
        '
        'ScannedPage
        '
        Me.ScannedPage.Controls.Add(Me.SplitterCtl)
        Me.ScannedPage.Name = "ScannedPage"
        resources.ApplyResources(Me.ScannedPage, "ScannedPage")
        '
        'AttachedPage
        '
        Me.AttachedPage.Controls.Add(Me.GridAttached)
        Me.AttachedPage.Name = "AttachedPage"
        resources.ApplyResources(Me.AttachedPage, "AttachedPage")
        '
        'GridAttached
        '
        resources.ApplyResources(Me.GridAttached, "GridAttached")
        Me.GridAttached.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridAttached.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridAttached.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridAttached.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridAttached.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridAttached.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridAttached.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridAttached.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridAttached.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridAttached.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridAttached.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridAttached.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridAttached.MainView = Me.ViewAttached
        Me.GridAttached.Name = "GridAttached"
        Me.GridAttached.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewAttached})
        '
        'ViewAttached
        '
        Me.ViewAttached.GridControl = Me.GridAttached
        Me.ViewAttached.Name = "ViewAttached"
        '
        'payPanel
        '
        Me.payPanel.Controls.Add(Me.GroupCheques)
        Me.payPanel.Controls.Add(Me.lblAmount)
        Me.payPanel.Controls.Add(Me._spinAmount)
        Me.payPanel.Controls.Add(Me.lblDate)
        Me.payPanel.Controls.Add(Me._datePayment)
        Me.payPanel.Controls.Add(Me.lblMethod)
        Me.payPanel.Controls.Add(Me._cmbMethod)
        Me.payPanel.Controls.Add(Me._btnEditPay)
        Me.payPanel.Controls.Add(Me._btnPay)
        resources.ApplyResources(Me.payPanel, "payPanel")
        Me.payPanel.Name = "payPanel"
        '
        'GroupCheques
        '
        Me.GroupCheques.AppearanceCaption.Font = CType(resources.GetObject("GroupCheques.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupCheques.AppearanceCaption.Options.UseFont = True
        Me.GroupCheques.Controls.Add(Me.ChkDrive)
        Me.GroupCheques.Controls.Add(Me.btnBrowse)
        Me.GroupCheques.Controls.Add(Me.btnScan)
        Me.GroupCheques.Controls.Add(Me.btnResetChqs)
        Me.GroupCheques.Controls.Add(Me.btnScanAndPay)
        Me.GroupCheques.Controls.Add(Me.dtChqDueDate)
        Me.GroupCheques.Controls.Add(Me.LabelControl18)
        Me.GroupCheques.Controls.Add(Me.LabelControl19)
        Me.GroupCheques.Controls.Add(Me.txtChqValue)
        Me.GroupCheques.Controls.Add(Me.txtChqBank)
        Me.GroupCheques.Controls.Add(Me.txtChqNumber)
        Me.GroupCheques.Controls.Add(Me.LabelControl15)
        Me.GroupCheques.Controls.Add(Me.LabelControl17)
        Me.GroupCheques.Controls.Add(Me.txtChqOwner)
        Me.GroupCheques.Controls.Add(Me.LabelControl14)
        Me.GroupCheques.Controls.Add(Me.txtAccountNumber)
        Me.GroupCheques.Controls.Add(Me.LabelControl16)
        resources.ApplyResources(Me.GroupCheques, "GroupCheques")
        Me.GroupCheques.Name = "GroupCheques"
        '
        'ChkDrive
        '
        Me.ChkDrive.EnterMoveNextControl = True
        resources.ApplyResources(Me.ChkDrive, "ChkDrive")
        Me.ChkDrive.Name = "ChkDrive"
        Me.ChkDrive.Properties.Appearance.Font = CType(resources.GetObject("ChkDrive.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ChkDrive.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.ChkDrive.Properties.Appearance.Options.UseFont = True
        Me.ChkDrive.Properties.Appearance.Options.UseForeColor = True
        Me.ChkDrive.Properties.Caption = resources.GetString("ChkDrive.Properties.Caption")
        '
        'btnBrowse
        '
        Me.btnBrowse.Appearance.Font = CType(resources.GetObject("btnBrowse.Appearance.Font"), System.Drawing.Font)
        Me.btnBrowse.Appearance.Options.UseFont = True
        Me.btnBrowse.ImageOptions.ImageKey = resources.GetString("btnBrowse.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnBrowse, "btnBrowse")
        Me.btnBrowse.Name = "btnBrowse"
        '
        'btnScan
        '
        Me.btnScan.Appearance.Font = CType(resources.GetObject("btnScan.Appearance.Font"), System.Drawing.Font)
        Me.btnScan.Appearance.Options.UseFont = True
        Me.btnScan.ImageOptions.ImageKey = resources.GetString("btnScan.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnScan, "btnScan")
        Me.btnScan.Name = "btnScan"
        '
        'btnResetChqs
        '
        Me.btnResetChqs.Appearance.Font = CType(resources.GetObject("btnResetChqs.Appearance.Font"), System.Drawing.Font)
        Me.btnResetChqs.Appearance.Options.UseFont = True
        Me.btnResetChqs.ImageOptions.ImageKey = resources.GetString("btnResetChqs.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnResetChqs, "btnResetChqs")
        Me.btnResetChqs.Name = "btnResetChqs"
        '
        'btnScanAndPay
        '
        Me.btnScanAndPay.Appearance.Font = CType(resources.GetObject("btnScanAndPay.Appearance.Font"), System.Drawing.Font)
        Me.btnScanAndPay.Appearance.Options.UseFont = True
        Me.btnScanAndPay.ImageOptions.ImageKey = resources.GetString("btnScanAndPay.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnScanAndPay, "btnScanAndPay")
        Me.btnScanAndPay.Name = "btnScanAndPay"
        '
        'dtChqDueDate
        '
        resources.ApplyResources(Me.dtChqDueDate, "dtChqDueDate")
        Me.dtChqDueDate.EnterMoveNextControl = True
        Me.dtChqDueDate.Name = "dtChqDueDate"
        Me.dtChqDueDate.Properties.Appearance.Font = CType(resources.GetObject("dtChqDueDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtChqDueDate.Properties.Appearance.Options.UseFont = True
        Me.dtChqDueDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtChqDueDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtChqDueDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtChqDueDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtChqDueDate.Properties.CalendarTimeProperties.MaskSettings.Set("mask", "d")
        Me.dtChqDueDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Classic
        Me.dtChqDueDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.dtChqDueDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtChqDueDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.dtChqDueDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtChqDueDate.Properties.ShowOk = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtChqDueDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        '
        'LabelControl18
        '
        Me.LabelControl18.Appearance.Font = CType(resources.GetObject("LabelControl18.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl18.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl18, "LabelControl18")
        Me.LabelControl18.Name = "LabelControl18"
        '
        'LabelControl19
        '
        Me.LabelControl19.Appearance.Font = CType(resources.GetObject("LabelControl19.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl19.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl19, "LabelControl19")
        Me.LabelControl19.Name = "LabelControl19"
        '
        'txtChqValue
        '
        resources.ApplyResources(Me.txtChqValue, "txtChqValue")
        Me.txtChqValue.EnterMoveNextControl = True
        Me.txtChqValue.Name = "txtChqValue"
        Me.txtChqValue.Properties.Appearance.Font = CType(resources.GetObject("txtChqValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqValue.Properties.Appearance.Options.UseFont = True
        '
        'txtChqBank
        '
        Me.txtChqBank.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqBank, "txtChqBank")
        Me.txtChqBank.Name = "txtChqBank"
        Me.txtChqBank.Properties.Appearance.Font = CType(resources.GetObject("txtChqBank.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqBank.Properties.Appearance.Options.UseFont = True
        '
        'txtChqNumber
        '
        Me.txtChqNumber.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqNumber, "txtChqNumber")
        Me.txtChqNumber.Name = "txtChqNumber"
        Me.txtChqNumber.Properties.Appearance.Font = CType(resources.GetObject("txtChqNumber.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqNumber.Properties.Appearance.Options.UseFont = True
        Me.txtChqNumber.Properties.MaxLength = 10
        Me.txtChqNumber.Properties.NullValuePrompt = resources.GetString("txtChqNumber.Properties.NullValuePrompt")
        '
        'LabelControl15
        '
        Me.LabelControl15.Appearance.Font = CType(resources.GetObject("LabelControl15.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl15.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl15, "LabelControl15")
        Me.LabelControl15.Name = "LabelControl15"
        '
        'LabelControl17
        '
        Me.LabelControl17.Appearance.Font = CType(resources.GetObject("LabelControl17.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl17.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl17, "LabelControl17")
        Me.LabelControl17.Name = "LabelControl17"
        '
        'txtChqOwner
        '
        Me.txtChqOwner.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqOwner, "txtChqOwner")
        Me.txtChqOwner.Name = "txtChqOwner"
        Me.txtChqOwner.Properties.Appearance.Font = CType(resources.GetObject("txtChqOwner.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqOwner.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.Font = CType(resources.GetObject("LabelControl14.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl14.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl14, "LabelControl14")
        Me.LabelControl14.Name = "LabelControl14"
        '
        'txtAccountNumber
        '
        Me.txtAccountNumber.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtAccountNumber, "txtAccountNumber")
        Me.txtAccountNumber.Name = "txtAccountNumber"
        Me.txtAccountNumber.Properties.Appearance.Font = CType(resources.GetObject("txtAccountNumber.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtAccountNumber.Properties.Appearance.Options.UseFont = True
        Me.txtAccountNumber.Properties.MaxLength = 10
        Me.txtAccountNumber.Properties.NullValuePrompt = resources.GetString("txtAccountNumber.Properties.NullValuePrompt")
        '
        'LabelControl16
        '
        Me.LabelControl16.Appearance.Font = CType(resources.GetObject("LabelControl16.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl16.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl16, "LabelControl16")
        Me.LabelControl16.Name = "LabelControl16"
        '
        'lblAmount
        '
        Me.lblAmount.Appearance.Font = CType(resources.GetObject("lblAmount.Appearance.Font"), System.Drawing.Font)
        Me.lblAmount.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblAmount, "lblAmount")
        Me.lblAmount.Name = "lblAmount"
        '
        '_spinAmount
        '
        resources.ApplyResources(Me._spinAmount, "_spinAmount")
        Me._spinAmount.EnterMoveNextControl = True
        Me._spinAmount.Name = "_spinAmount"
        Me._spinAmount.Properties.Appearance.Font = CType(resources.GetObject("_spinAmount.Properties.Appearance.Font"), System.Drawing.Font)
        Me._spinAmount.Properties.Appearance.Options.UseFont = True
        '
        'lblDate
        '
        Me.lblDate.Appearance.Font = CType(resources.GetObject("lblDate.Appearance.Font"), System.Drawing.Font)
        Me.lblDate.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblDate, "lblDate")
        Me.lblDate.Name = "lblDate"
        '
        '_datePayment
        '
        resources.ApplyResources(Me._datePayment, "_datePayment")
        Me._datePayment.EnterMoveNextControl = True
        Me._datePayment.Name = "_datePayment"
        Me._datePayment.Properties.Appearance.Font = CType(resources.GetObject("_datePayment.Properties.Appearance.Font"), System.Drawing.Font)
        Me._datePayment.Properties.Appearance.Options.UseFont = True
        Me._datePayment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_datePayment.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me._datePayment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_datePayment.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblMethod
        '
        Me.lblMethod.Appearance.Font = CType(resources.GetObject("lblMethod.Appearance.Font"), System.Drawing.Font)
        Me.lblMethod.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblMethod, "lblMethod")
        Me.lblMethod.Name = "lblMethod"
        '
        '_cmbMethod
        '
        Me._cmbMethod.EnterMoveNextControl = True
        resources.ApplyResources(Me._cmbMethod, "_cmbMethod")
        Me._cmbMethod.Name = "_cmbMethod"
        Me._cmbMethod.Properties.Appearance.Font = CType(resources.GetObject("_cmbMethod.Properties.Appearance.Font"), System.Drawing.Font)
        Me._cmbMethod.Properties.Appearance.Options.UseFont = True
        Me._cmbMethod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_cmbMethod.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me._cmbMethod.Properties.Items.AddRange(New Object() {resources.GetString("_cmbMethod.Properties.Items"), resources.GetString("_cmbMethod.Properties.Items1"), resources.GetString("_cmbMethod.Properties.Items2"), resources.GetString("_cmbMethod.Properties.Items3")})
        '
        '_btnEditPay
        '
        Me._btnEditPay.Appearance.Font = CType(resources.GetObject("_btnEditPay.Appearance.Font"), System.Drawing.Font)
        Me._btnEditPay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me._btnEditPay, "_btnEditPay")
        Me._btnEditPay.Name = "_btnEditPay"
        '
        '_btnPay
        '
        Me._btnPay.Appearance.Font = CType(resources.GetObject("_btnPay.Appearance.Font"), System.Drawing.Font)
        Me._btnPay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me._btnPay, "_btnPay")
        Me._btnPay.Name = "_btnPay"
        '
        'StockSupplierPaymentsForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.split)
        Me.Name = "StockSupplierPaymentsForm"
        CType(Me.SplitterCtl.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtl.Panel1.ResumeLayout(False)
        CType(Me.SplitterCtl.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtl.Panel2.ResumeLayout(False)
        CType(Me.SplitterCtl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtl.ResumeLayout(False)
        CType(Me.scannedFilesList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.scanPreview.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.split.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.split.Panel1.ResumeLayout(False)
        CType(Me.split.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.split.Panel2.ResumeLayout(False)
        CType(Me.split, System.ComponentModel.ISupportInitialize).EndInit()
        Me.split.ResumeLayout(False)
        CType(Me._gridInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._viewInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.filterPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.filterPanel.ResumeLayout(False)
        Me.filterPanel.PerformLayout()
        CType(Me._cmbSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridTabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GridTabControl.ResumeLayout(False)
        Me.ChqGridPage.ResumeLayout(False)
        CType(Me._gridPayments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._viewPayments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ScannedPage.ResumeLayout(False)
        Me.AttachedPage.ResumeLayout(False)
        CType(Me.GridAttached, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewAttached, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.payPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.payPanel.ResumeLayout(False)
        Me.payPanel.PerformLayout()
        CType(Me.GroupCheques, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupCheques.ResumeLayout(False)
        Me.GroupCheques.PerformLayout()
        CType(Me.ChkDrive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtChqDueDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtChqDueDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqBank.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqOwner.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccountNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._spinAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._datePayment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._datePayment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._cmbMethod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents split As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents filterPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblSupplier As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOpenInvoices As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPayments As DevExpress.XtraEditors.LabelControl
    Friend WithEvents payPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblAmount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblMethod As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupCheques As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtChqOwner As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtAccountNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtChqBank As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtChqNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtChqDueDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtChqValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnBrowse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnScan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnResetChqs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnScanAndPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ChkDrive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblOpenInvoicesSum As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPaymentsSum As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridTabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents ChqGridPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ScannedPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitterCtl As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents scannedFilesList As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents scanPreview As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents AttachedPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridAttached As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewAttached As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents _btnEditPay As DevExpress.XtraEditors.SimpleButton
End Class

