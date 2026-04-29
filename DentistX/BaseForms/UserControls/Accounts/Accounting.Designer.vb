<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Accounting
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Accounting))
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule2 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue2 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Me.SplitterCtl = New DevExpress.XtraEditors.SplitContainerControl()
        Me.scannedFilesList = New DevExpress.XtraEditors.ListBoxControl()
        Me.scanPreview = New DevExpress.XtraEditors.PictureEdit()
        Me.colTrtValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grpTrtDet = New DevExpress.XtraEditors.GroupControl()
        Me.TrtDate = New DevExpress.XtraEditors.DateEdit()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.btnWhatsSend = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddTrt = New DevExpress.XtraEditors.SimpleButton()
        Me.chkAddTrt = New DevExpress.XtraEditors.CheckEdit()
        Me.btnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.btnInvoiceGen = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditGrid = New DevExpress.XtraEditors.CheckButton()
        Me.SidePanel1 = New DevExpress.XtraEditors.SidePanel()
        Me.Patient_TrtsGridControl = New DevExpress.XtraGrid.GridControl()
        Me.Patient_TrtsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GridViewTrts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTrtID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colToothTrtID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOrthoID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOtherTrtID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTrtDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIsMultiTooth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDiscount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDiscountType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TrtNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel5 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton23 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton24 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox5 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton25 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton26 = New System.Windows.Forms.ToolStripButton()
        Me.TrtSavNav = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnTrtDel = New System.Windows.Forms.ToolStripButton()
        Me.txtTrtDetails = New DevExpress.XtraEditors.TextEdit()
        Me.DetailText = New DevExpress.XtraEditors.TextEdit()
        Me.showAsChildChck = New DevExpress.XtraEditors.CheckEdit()
        Me.btnModifyValue = New DevExpress.XtraEditors.SimpleButton()
        Me.btnResetPatientAccount = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl23 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTrtDet = New DevExpress.XtraEditors.LabelControl()
        Me.lblTrtID = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TrtValue = New DevExpress.XtraEditors.SpinEdit()
        Me.TrtDiscount = New DevExpress.XtraEditors.SpinEdit()
        Me.RadioFilter = New DevExpress.XtraEditors.RadioGroup()
        Me.BalanceGroup = New DevExpress.XtraEditors.GroupControl()
        Me.lblBal = New DevExpress.XtraEditors.LabelControl()
        Me.LabelFinalBalance = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalPays = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalDisc = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalTrts = New DevExpress.XtraEditors.LabelControl()
        Me.LabeTotalPayments = New DevExpress.XtraEditors.LabelControl()
        Me.LabelTotalDiscounts = New DevExpress.XtraEditors.LabelControl()
        Me.LabelTotalTreatments = New DevExpress.XtraEditors.LabelControl()
        Me.PatientBalanceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grpPayDet = New DevExpress.XtraEditors.GroupControl()
        Me.payDetailsPanel = New DevExpress.XtraEditors.SidePanel()
        Me.chqInsurTab = New DevExpress.XtraTab.XtraTabControl()
        Me.ChqPage = New DevExpress.XtraTab.XtraTabPage()
        Me.btnBrowse = New DevExpress.XtraEditors.SimpleButton()
        Me.btnResetChqs = New DevExpress.XtraEditors.SimpleButton()
        Me.btnScanAndPay = New DevExpress.XtraEditors.SimpleButton()
        Me.btnScan = New DevExpress.XtraEditors.SimpleButton()
        Me.txtChqOwner = New DevExpress.XtraEditors.TextEdit()
        Me.chkIsCashed = New DevExpress.XtraEditors.CheckEdit()
        Me.dtChqDueDate = New DevExpress.XtraEditors.DateEdit()
        Me.txtChqBank = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.txtChqNumber = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.txtAccountNumber = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.ChqValue = New DevExpress.XtraEditors.SpinEdit()
        Me.InsurePage = New DevExpress.XtraTab.XtraTabPage()
        Me.txtInsurNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.txtInureComp = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl21 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl20 = New DevExpress.XtraEditors.LabelControl()
        Me.PayMainDetPanel = New DevExpress.XtraEditors.SidePanel()
        Me.cboPayType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnAddPay = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTreat = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl24 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.PayDate = New DevExpress.XtraEditors.DateEdit()
        Me.NotesText = New DevExpress.XtraEditors.MemoEdit()
        Me.PayValue = New DevExpress.XtraEditors.SpinEdit()
        Me.payGridsPanel = New DevExpress.XtraEditors.SidePanel()
        Me.GridTabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.CashGridPage = New DevExpress.XtraTab.XtraTabPage()
        Me.Patient_PaysGridControl = New DevExpress.XtraGrid.GridControl()
        Me.Patient_PaysBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GridViewPays = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTrtID1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ChqGridPage = New DevExpress.XtraTab.XtraTabPage()
        Me.ChqsPayGrid = New DevExpress.XtraGrid.GridControl()
        Me.ChqsGridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqAccount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqDueDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqBank = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqOwner = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ScannedPage = New DevExpress.XtraTab.XtraTabPage()
        Me.AttachedPage = New DevExpress.XtraTab.XtraTabPage()
        Me.GridAttached = New DevExpress.XtraGrid.GridControl()
        Me.ViewAttached = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.PayNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton15 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton16 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox3 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton17 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton18 = New System.Windows.Forms.ToolStripButton()
        Me.PaySavNav = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPayDel = New System.Windows.Forms.ToolStripButton()
        Me.PatientTrtPaysBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TrtsPaysListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.SplitterCtl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitterCtl.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitterCtl.Panel1.SuspendLayout()
        CType(Me.SplitterCtl.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitterCtl.Panel2.SuspendLayout()
        Me.SplitterCtl.SuspendLayout()
        CType(Me.scannedFilesList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scanPreview.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpTrtDet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTrtDet.SuspendLayout()
        CType(Me.TrtDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAddTrt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SidePanel1.SuspendLayout()
        CType(Me.Patient_TrtsGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Patient_TrtsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewTrts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TrtNavigator.SuspendLayout()
        CType(Me.txtTrtDetails.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetailText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.showAsChildChck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtDiscount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BalanceGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BalanceGroup.SuspendLayout()
        CType(Me.PatientBalanceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpPayDet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPayDet.SuspendLayout()
        Me.payDetailsPanel.SuspendLayout()
        CType(Me.chqInsurTab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.chqInsurTab.SuspendLayout()
        Me.ChqPage.SuspendLayout()
        CType(Me.txtChqOwner.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsCashed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtChqDueDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtChqDueDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqBank.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccountNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChqValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.InsurePage.SuspendLayout()
        CType(Me.txtInsurNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInureComp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PayMainDetPanel.SuspendLayout()
        CType(Me.cboPayType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.payGridsPanel.SuspendLayout()
        CType(Me.GridTabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GridTabControl.SuspendLayout()
        Me.CashGridPage.SuspendLayout()
        CType(Me.Patient_PaysGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Patient_PaysBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewPays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ChqGridPage.SuspendLayout()
        CType(Me.ChqsPayGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChqsGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ScannedPage.SuspendLayout()
        Me.AttachedPage.SuspendLayout()
        CType(Me.GridAttached, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewAttached, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PayNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PayNavigator.SuspendLayout()
        CType(Me.PatientTrtPaysBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtsPaysListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'colTrtValue
        '
        resources.ApplyResources(Me.colTrtValue, "colTrtValue")
        Me.colTrtValue.DisplayFormat.FormatString = "n0"
        Me.colTrtValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colTrtValue.FieldName = "TrtValue"
        Me.colTrtValue.ImageOptions.ImageKey = resources.GetString("colTrtValue.ImageOptions.ImageKey")
        Me.colTrtValue.Name = "colTrtValue"
        Me.colTrtValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("colTrtValue.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("colTrtValue.Summary1"), resources.GetString("colTrtValue.Summary2"))})
        Me.colTrtValue.UnboundDataType = GetType(Decimal)
        '
        'grpTrtDet
        '
        Me.grpTrtDet.AppearanceCaption.BorderColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTrtDet.AppearanceCaption.Font = CType(resources.GetObject("grpTrtDet.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpTrtDet.AppearanceCaption.ForeColor = System.Drawing.Color.Blue
        Me.grpTrtDet.AppearanceCaption.Options.UseBorderColor = True
        Me.grpTrtDet.AppearanceCaption.Options.UseFont = True
        Me.grpTrtDet.AppearanceCaption.Options.UseForeColor = True
        Me.grpTrtDet.AppearanceCaption.Options.UseTextOptions = True
        Me.grpTrtDet.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.grpTrtDet.Controls.Add(Me.TrtDate)
        Me.grpTrtDet.Controls.Add(Me.lblWhats)
        Me.grpTrtDet.Controls.Add(Me.cboPrefix)
        Me.grpTrtDet.Controls.Add(Me.txtWhats)
        Me.grpTrtDet.Controls.Add(Me.btnWhatsSend)
        Me.grpTrtDet.Controls.Add(Me.btnAddTrt)
        Me.grpTrtDet.Controls.Add(Me.chkAddTrt)
        Me.grpTrtDet.Controls.Add(Me.btnPrint)
        Me.grpTrtDet.Controls.Add(Me.btnInvoiceGen)
        Me.grpTrtDet.Controls.Add(Me.btnEditGrid)
        Me.grpTrtDet.Controls.Add(Me.SidePanel1)
        Me.grpTrtDet.Controls.Add(Me.txtTrtDetails)
        Me.grpTrtDet.Controls.Add(Me.DetailText)
        Me.grpTrtDet.Controls.Add(Me.showAsChildChck)
        Me.grpTrtDet.Controls.Add(Me.btnModifyValue)
        Me.grpTrtDet.Controls.Add(Me.btnResetPatientAccount)
        Me.grpTrtDet.Controls.Add(Me.LabelControl3)
        Me.grpTrtDet.Controls.Add(Me.LabelControl23)
        Me.grpTrtDet.Controls.Add(Me.LabelControl2)
        Me.grpTrtDet.Controls.Add(Me.lblTrtDet)
        Me.grpTrtDet.Controls.Add(Me.lblTrtID)
        Me.grpTrtDet.Controls.Add(Me.LabelControl1)
        Me.grpTrtDet.Controls.Add(Me.TrtValue)
        Me.grpTrtDet.Controls.Add(Me.TrtDiscount)
        Me.grpTrtDet.Controls.Add(Me.RadioFilter)
        resources.ApplyResources(Me.grpTrtDet, "grpTrtDet")
        Me.grpTrtDet.Name = "grpTrtDet"
        '
        'TrtDate
        '
        resources.ApplyResources(Me.TrtDate, "TrtDate")
        Me.TrtDate.Name = "TrtDate"
        Me.TrtDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("TrtDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TrtDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("TrtDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblWhats
        '
        Me.lblWhats.Appearance.Font = CType(resources.GetObject("lblWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblWhats.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblWhats.Appearance.Options.UseFont = True
        Me.lblWhats.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblWhats, "lblWhats")
        Me.lblWhats.Name = "lblWhats"
        '
        'cboPrefix
        '
        resources.ApplyResources(Me.cboPrefix, "cboPrefix")
        Me.cboPrefix.Name = "cboPrefix"
        Me.cboPrefix.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPrefix.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'txtWhats
        '
        resources.ApplyResources(Me.txtWhats, "txtWhats")
        Me.txtWhats.Name = "txtWhats"
        Me.txtWhats.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtWhats.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtWhats.Properties.MaskSettings.Set("mask", "d")
        '
        'btnWhatsSend
        '
        Me.btnWhatsSend.ImageOptions.ImageKey = resources.GetString("btnWhatsSend.ImageOptions.ImageKey")
        Me.btnWhatsSend.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.whatsapp
        Me.btnWhatsSend.ImageOptions.SvgImageSize = New System.Drawing.Size(30, 30)
        resources.ApplyResources(Me.btnWhatsSend, "btnWhatsSend")
        Me.btnWhatsSend.Name = "btnWhatsSend"
        '
        'btnAddTrt
        '
        Me.btnAddTrt.Appearance.Font = CType(resources.GetObject("btnAddTrt.Appearance.Font"), System.Drawing.Font)
        Me.btnAddTrt.Appearance.Options.UseFont = True
        Me.btnAddTrt.ImageOptions.ImageKey = resources.GetString("btnAddTrt.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnAddTrt, "btnAddTrt")
        Me.btnAddTrt.Name = "btnAddTrt"
        '
        'chkAddTrt
        '
        resources.ApplyResources(Me.chkAddTrt, "chkAddTrt")
        Me.chkAddTrt.Name = "chkAddTrt"
        Me.chkAddTrt.Properties.Appearance.Font = CType(resources.GetObject("chkAddTrt.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkAddTrt.Properties.Appearance.Options.UseFont = True
        Me.chkAddTrt.Properties.Caption = resources.GetString("chkAddTrt.Properties.Caption")
        '
        'btnPrint
        '
        Me.btnPrint.Appearance.Font = CType(resources.GetObject("btnPrint.Appearance.Font"), System.Drawing.Font)
        Me.btnPrint.Appearance.Options.UseFont = True
        Me.btnPrint.ImageOptions.ImageKey = resources.GetString("btnPrint.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnPrint, "btnPrint")
        Me.btnPrint.Name = "btnPrint"
        '
        'btnInvoiceGen
        '
        Me.btnInvoiceGen.Appearance.Font = CType(resources.GetObject("btnInvoiceGen.Appearance.Font"), System.Drawing.Font)
        Me.btnInvoiceGen.Appearance.Options.UseFont = True
        Me.btnInvoiceGen.ImageOptions.ImageKey = resources.GetString("btnInvoiceGen.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnInvoiceGen, "btnInvoiceGen")
        Me.btnInvoiceGen.Name = "btnInvoiceGen"
        '
        'btnEditGrid
        '
        Me.btnEditGrid.Appearance.Font = CType(resources.GetObject("btnEditGrid.Appearance.Font"), System.Drawing.Font)
        Me.btnEditGrid.Appearance.Options.UseFont = True
        Me.btnEditGrid.ImageOptions.ImageKey = resources.GetString("btnEditGrid.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnEditGrid, "btnEditGrid")
        Me.btnEditGrid.Name = "btnEditGrid"
        '
        'SidePanel1
        '
        Me.SidePanel1.Controls.Add(Me.Patient_TrtsGridControl)
        Me.SidePanel1.Controls.Add(Me.TrtNavigator)
        resources.ApplyResources(Me.SidePanel1, "SidePanel1")
        Me.SidePanel1.Name = "SidePanel1"
        '
        'Patient_TrtsGridControl
        '
        Me.Patient_TrtsGridControl.DataSource = Me.Patient_TrtsBindingSource
        resources.ApplyResources(Me.Patient_TrtsGridControl, "Patient_TrtsGridControl")
        Me.Patient_TrtsGridControl.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("Patient_TrtsGridControl.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Anchor = CType(resources.GetObject("Patient_TrtsGridControl.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Appearance.Font = CType(resources.GetObject("Patient_TrtsGridControl.EmbeddedNavigator.Appearance.Font"), System.Drawing.Font)
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.Patient_TrtsGridControl.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("Patient_TrtsGridControl.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Buttons.Append.Hint = resources.GetString("Patient_TrtsGridControl.EmbeddedNavigator.Buttons.Append.Hint")
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Buttons.CancelEdit.Hint = resources.GetString("Patient_TrtsGridControl.EmbeddedNavigator.Buttons.CancelEdit.Hint")
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Buttons.CancelEdit.Tag = "Cancel"
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Buttons.Edit.Hint = resources.GetString("Patient_TrtsGridControl.EmbeddedNavigator.Buttons.Edit.Hint")
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Buttons.Edit.Tag = "Edit"
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Buttons.EndEdit.Hint = resources.GetString("Patient_TrtsGridControl.EmbeddedNavigator.Buttons.EndEdit.Hint")
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Buttons.EndEdit.Tag = "EndEdit"
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Buttons.Remove.Hint = resources.GetString("Patient_TrtsGridControl.EmbeddedNavigator.Buttons.Remove.Hint")
        Me.Patient_TrtsGridControl.EmbeddedNavigator.Buttons.Remove.Tag = "Remove"
        Me.Patient_TrtsGridControl.EmbeddedNavigator.ImeMode = CType(resources.GetObject("Patient_TrtsGridControl.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.Patient_TrtsGridControl.EmbeddedNavigator.TextLocation = CType(resources.GetObject("Patient_TrtsGridControl.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.Patient_TrtsGridControl.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("Patient_TrtsGridControl.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.Patient_TrtsGridControl.MainView = Me.GridViewTrts
        Me.Patient_TrtsGridControl.Name = "Patient_TrtsGridControl"
        Me.Patient_TrtsGridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewTrts})
        '
        'Patient_TrtsBindingSource
        '
        Me.Patient_TrtsBindingSource.DataSource = GetType(DentistX.Patient_Trts)
        '
        'GridViewTrts
        '
        Me.GridViewTrts.Appearance.FooterPanel.FontStyleDelta = CType(resources.GetObject("GridViewTrts.Appearance.FooterPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridViewTrts.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue
        Me.GridViewTrts.Appearance.FooterPanel.Options.UseFont = True
        Me.GridViewTrts.Appearance.FooterPanel.Options.UseForeColor = True
        Me.GridViewTrts.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.GridViewTrts.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewTrts.Appearance.GroupFooter.FontStyleDelta = CType(resources.GetObject("GridViewTrts.Appearance.GroupFooter.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridViewTrts.Appearance.GroupFooter.Options.UseFont = True
        Me.GridViewTrts.Appearance.GroupFooter.Options.UseTextOptions = True
        Me.GridViewTrts.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewTrts.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridViewTrts.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridViewTrts.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewTrts.Appearance.Row.Font = CType(resources.GetObject("GridViewTrts.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridViewTrts.Appearance.Row.Options.UseFont = True
        Me.GridViewTrts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colTrtID, Me.colPatientID, Me.colToothTrtID, Me.colOrthoID, Me.colOtherTrtID, Me.colDetail, Me.colTrtDate, Me.colTrtValue, Me.colIsMultiTooth, Me.colDiscount, Me.colDiscountType})
        GridFormatRule1.Column = Me.colTrtValue
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        FormatConditionRuleValue1.Appearance.Font = CType(resources.GetObject("resource.Font"), System.Drawing.Font)
        FormatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.Blue
        FormatConditionRuleValue1.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue1.Appearance.Options.UseFont = True
        FormatConditionRuleValue1.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue1.Expression = "[TrtValue] > 0"
        GridFormatRule1.Rule = FormatConditionRuleValue1
        GridFormatRule2.Column = Me.colTrtValue
        GridFormatRule2.Name = "Format1"
        FormatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        FormatConditionRuleValue2.Appearance.Font = CType(resources.GetObject("resource.Font1"), System.Drawing.Font)
        FormatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.Red
        FormatConditionRuleValue2.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue2.Appearance.Options.UseFont = True
        FormatConditionRuleValue2.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Less
        FormatConditionRuleValue2.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        GridFormatRule2.Rule = FormatConditionRuleValue2
        Me.GridViewTrts.FormatRules.Add(GridFormatRule1)
        Me.GridViewTrts.FormatRules.Add(GridFormatRule2)
        Me.GridViewTrts.GridControl = Me.Patient_TrtsGridControl
        Me.GridViewTrts.Name = "GridViewTrts"
        Me.GridViewTrts.OptionsDetail.EnableMasterViewMode = False
        Me.GridViewTrts.OptionsView.AutoCalcPreviewLineCount = True
        Me.GridViewTrts.OptionsView.EnableAppearanceEvenRow = True
        Me.GridViewTrts.OptionsView.ShowFooter = True
        '
        'colRowNum
        '
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.ImageOptions.ImageKey = resources.GetString("colRowNum.ImageOptions.ImageKey")
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colTrtID
        '
        Me.colTrtID.FieldName = "TrtID"
        Me.colTrtID.ImageOptions.ImageKey = resources.GetString("colTrtID.ImageOptions.ImageKey")
        Me.colTrtID.Name = "colTrtID"
        '
        'colPatientID
        '
        Me.colPatientID.FieldName = "PatientID"
        Me.colPatientID.ImageOptions.ImageKey = resources.GetString("colPatientID.ImageOptions.ImageKey")
        Me.colPatientID.Name = "colPatientID"
        '
        'colToothTrtID
        '
        resources.ApplyResources(Me.colToothTrtID, "colToothTrtID")
        Me.colToothTrtID.FieldName = "ToothTrtID"
        Me.colToothTrtID.ImageOptions.ImageKey = resources.GetString("colToothTrtID.ImageOptions.ImageKey")
        Me.colToothTrtID.Name = "colToothTrtID"
        '
        'colOrthoID
        '
        resources.ApplyResources(Me.colOrthoID, "colOrthoID")
        Me.colOrthoID.FieldName = "OrthoID"
        Me.colOrthoID.ImageOptions.ImageKey = resources.GetString("colOrthoID.ImageOptions.ImageKey")
        Me.colOrthoID.Name = "colOrthoID"
        '
        'colOtherTrtID
        '
        resources.ApplyResources(Me.colOtherTrtID, "colOtherTrtID")
        Me.colOtherTrtID.FieldName = "OtherTrtID"
        Me.colOtherTrtID.ImageOptions.ImageKey = resources.GetString("colOtherTrtID.ImageOptions.ImageKey")
        Me.colOtherTrtID.Name = "colOtherTrtID"
        '
        'colDetail
        '
        resources.ApplyResources(Me.colDetail, "colDetail")
        Me.colDetail.FieldName = "Detail"
        Me.colDetail.ImageOptions.ImageKey = resources.GetString("colDetail.ImageOptions.ImageKey")
        Me.colDetail.Name = "colDetail"
        '
        'colTrtDate
        '
        resources.ApplyResources(Me.colTrtDate, "colTrtDate")
        Me.colTrtDate.FieldName = "TrtDate"
        Me.colTrtDate.ImageOptions.ImageKey = resources.GetString("colTrtDate.ImageOptions.ImageKey")
        Me.colTrtDate.Name = "colTrtDate"
        Me.colTrtDate.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("colTrtDate.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("colTrtDate.Summary1"), resources.GetString("colTrtDate.Summary2"))})
        '
        'colIsMultiTooth
        '
        resources.ApplyResources(Me.colIsMultiTooth, "colIsMultiTooth")
        Me.colIsMultiTooth.FieldName = "IsMultiTooth"
        Me.colIsMultiTooth.ImageOptions.ImageKey = resources.GetString("colIsMultiTooth.ImageOptions.ImageKey")
        Me.colIsMultiTooth.Name = "colIsMultiTooth"
        '
        'colDiscount
        '
        resources.ApplyResources(Me.colDiscount, "colDiscount")
        Me.colDiscount.DisplayFormat.FormatString = "n0"
        Me.colDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colDiscount.FieldName = "Discount"
        Me.colDiscount.ImageOptions.ImageKey = resources.GetString("colDiscount.ImageOptions.ImageKey")
        Me.colDiscount.Name = "colDiscount"
        Me.colDiscount.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("colDiscount.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("colDiscount.Summary1"), resources.GetString("colDiscount.Summary2"))})
        Me.colDiscount.UnboundDataType = GetType(Decimal)
        '
        'colDiscountType
        '
        resources.ApplyResources(Me.colDiscountType, "colDiscountType")
        Me.colDiscountType.FieldName = "DiscountType"
        Me.colDiscountType.ImageOptions.ImageKey = resources.GetString("colDiscountType.ImageOptions.ImageKey")
        Me.colDiscountType.Name = "colDiscountType"
        '
        'TrtNavigator
        '
        Me.TrtNavigator.AddNewItem = Nothing
        resources.ApplyResources(Me.TrtNavigator, "TrtNavigator")
        Me.TrtNavigator.BindingSource = Me.Patient_TrtsBindingSource
        Me.TrtNavigator.CountItem = Me.ToolStripLabel5
        Me.TrtNavigator.DeleteItem = Nothing
        Me.TrtNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TrtNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton23, Me.ToolStripButton24, Me.ToolStripSeparator11, Me.ToolStripTextBox5, Me.ToolStripLabel5, Me.ToolStripSeparator12, Me.ToolStripButton25, Me.ToolStripButton26, Me.TrtSavNav, Me.ToolStripSeparator1, Me.btnTrtDel})
        Me.TrtNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.TrtNavigator.MoveFirstItem = Me.ToolStripButton23
        Me.TrtNavigator.MoveLastItem = Me.ToolStripButton26
        Me.TrtNavigator.MoveNextItem = Me.ToolStripButton25
        Me.TrtNavigator.MovePreviousItem = Me.ToolStripButton24
        Me.TrtNavigator.Name = "TrtNavigator"
        Me.TrtNavigator.PositionItem = Me.ToolStripTextBox5
        '
        'ToolStripLabel5
        '
        Me.ToolStripLabel5.Name = "ToolStripLabel5"
        resources.ApplyResources(Me.ToolStripLabel5, "ToolStripLabel5")
        '
        'ToolStripButton23
        '
        Me.ToolStripButton23.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton23, "ToolStripButton23")
        Me.ToolStripButton23.Name = "ToolStripButton23"
        '
        'ToolStripButton24
        '
        Me.ToolStripButton24.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton24, "ToolStripButton24")
        Me.ToolStripButton24.Name = "ToolStripButton24"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        resources.ApplyResources(Me.ToolStripSeparator11, "ToolStripSeparator11")
        '
        'ToolStripTextBox5
        '
        resources.ApplyResources(Me.ToolStripTextBox5, "ToolStripTextBox5")
        Me.ToolStripTextBox5.Name = "ToolStripTextBox5"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        resources.ApplyResources(Me.ToolStripSeparator12, "ToolStripSeparator12")
        '
        'ToolStripButton25
        '
        Me.ToolStripButton25.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton25, "ToolStripButton25")
        Me.ToolStripButton25.Name = "ToolStripButton25"
        '
        'ToolStripButton26
        '
        Me.ToolStripButton26.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton26, "ToolStripButton26")
        Me.ToolStripButton26.Name = "ToolStripButton26"
        '
        'TrtSavNav
        '
        Me.TrtSavNav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.TrtSavNav, "TrtSavNav")
        Me.TrtSavNav.Name = "TrtSavNav"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'btnTrtDel
        '
        Me.btnTrtDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.btnTrtDel, "btnTrtDel")
        Me.btnTrtDel.Name = "btnTrtDel"
        '
        'txtTrtDetails
        '
        Me.txtTrtDetails.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtTrtDetails, "txtTrtDetails")
        Me.txtTrtDetails.Name = "txtTrtDetails"
        Me.txtTrtDetails.Properties.Appearance.Font = CType(resources.GetObject("txtTrtDetails.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtTrtDetails.Properties.Appearance.Options.UseFont = True
        Me.txtTrtDetails.Properties.AutoHeight = CType(resources.GetObject("txtTrtDetails.Properties.AutoHeight"), Boolean)
        Me.txtTrtDetails.Properties.UseReadOnlyAppearance = False
        '
        'DetailText
        '
        Me.DetailText.EnterMoveNextControl = True
        resources.ApplyResources(Me.DetailText, "DetailText")
        Me.DetailText.Name = "DetailText"
        Me.DetailText.Properties.Appearance.Font = CType(resources.GetObject("DetailText.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DetailText.Properties.Appearance.Options.UseFont = True
        Me.DetailText.Properties.AutoHeight = CType(resources.GetObject("DetailText.Properties.AutoHeight"), Boolean)
        Me.DetailText.Properties.UseReadOnlyAppearance = False
        '
        'showAsChildChck
        '
        resources.ApplyResources(Me.showAsChildChck, "showAsChildChck")
        Me.showAsChildChck.Name = "showAsChildChck"
        Me.showAsChildChck.Properties.Appearance.Font = CType(resources.GetObject("showAsChildChck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.showAsChildChck.Properties.Appearance.Options.UseFont = True
        Me.showAsChildChck.Properties.Caption = resources.GetString("showAsChildChck.Properties.Caption")
        '
        'btnModifyValue
        '
        Me.btnModifyValue.Appearance.Font = CType(resources.GetObject("btnModifyValue.Appearance.Font"), System.Drawing.Font)
        Me.btnModifyValue.Appearance.Options.UseFont = True
        Me.btnModifyValue.ImageOptions.ImageKey = resources.GetString("btnModifyValue.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnModifyValue, "btnModifyValue")
        Me.btnModifyValue.Name = "btnModifyValue"
        '
        'btnResetPatientAccount
        '
        Me.btnResetPatientAccount.Appearance.Font = CType(resources.GetObject("btnResetPatientAccount.Appearance.Font"), System.Drawing.Font)
        Me.btnResetPatientAccount.Appearance.Options.UseFont = True
        Me.btnResetPatientAccount.ImageOptions.ImageKey = resources.GetString("btnResetPatientAccount.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnResetPatientAccount, "btnResetPatientAccount")
        Me.btnResetPatientAccount.Name = "btnResetPatientAccount"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Name = "LabelControl3"
        '
        'LabelControl23
        '
        Me.LabelControl23.Appearance.Font = CType(resources.GetObject("LabelControl23.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl23.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl23, "LabelControl23")
        Me.LabelControl23.Name = "LabelControl23"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Name = "LabelControl2"
        '
        'lblTrtDet
        '
        Me.lblTrtDet.Appearance.Font = CType(resources.GetObject("lblTrtDet.Appearance.Font"), System.Drawing.Font)
        Me.lblTrtDet.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblTrtDet, "lblTrtDet")
        Me.lblTrtDet.Name = "lblTrtDet"
        '
        'lblTrtID
        '
        Me.lblTrtID.Appearance.Font = CType(resources.GetObject("lblTrtID.Appearance.Font"), System.Drawing.Font)
        Me.lblTrtID.Appearance.Options.UseFont = True
        Me.lblTrtID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.Patient_TrtsBindingSource, "TrtID", True))
        resources.ApplyResources(Me.lblTrtID, "lblTrtID")
        Me.lblTrtID.Name = "lblTrtID"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'TrtValue
        '
        resources.ApplyResources(Me.TrtValue, "TrtValue")
        Me.TrtValue.EnterMoveNextControl = True
        Me.TrtValue.Name = "TrtValue"
        '
        'TrtDiscount
        '
        resources.ApplyResources(Me.TrtDiscount, "TrtDiscount")
        Me.TrtDiscount.Name = "TrtDiscount"
        '
        'RadioFilter
        '
        Me.RadioFilter.EnterMoveNextControl = True
        resources.ApplyResources(Me.RadioFilter, "RadioFilter")
        Me.RadioFilter.Name = "RadioFilter"
        Me.RadioFilter.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadioFilter.Properties.Appearance.Font = CType(resources.GetObject("RadioFilter.Properties.Appearance.Font"), System.Drawing.Font)
        Me.RadioFilter.Properties.Appearance.Options.UseBackColor = True
        Me.RadioFilter.Properties.Appearance.Options.UseFont = True
        Me.RadioFilter.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioFilter.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioFilter.Properties.Items"), Object), resources.GetString("RadioFilter.Properties.Items1"), CType(resources.GetObject("RadioFilter.Properties.Items2"), Boolean), CType(resources.GetObject("RadioFilter.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioFilter.Properties.Items4"), Object), resources.GetString("RadioFilter.Properties.Items5")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioFilter.Properties.Items6"), Object), resources.GetString("RadioFilter.Properties.Items7")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioFilter.Properties.Items8"), Object), resources.GetString("RadioFilter.Properties.Items9"))})
        '
        'BalanceGroup
        '
        Me.BalanceGroup.AppearanceCaption.BorderColor = System.Drawing.Color.Fuchsia
        Me.BalanceGroup.AppearanceCaption.Font = CType(resources.GetObject("BalanceGroup.AppearanceCaption.Font"), System.Drawing.Font)
        Me.BalanceGroup.AppearanceCaption.ForeColor = System.Drawing.Color.ForestGreen
        Me.BalanceGroup.AppearanceCaption.Options.UseBorderColor = True
        Me.BalanceGroup.AppearanceCaption.Options.UseFont = True
        Me.BalanceGroup.AppearanceCaption.Options.UseForeColor = True
        Me.BalanceGroup.AppearanceCaption.Options.UseTextOptions = True
        Me.BalanceGroup.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BalanceGroup.Controls.Add(Me.lblBal)
        Me.BalanceGroup.Controls.Add(Me.LabelFinalBalance)
        Me.BalanceGroup.Controls.Add(Me.lblTotalPays)
        Me.BalanceGroup.Controls.Add(Me.lblTotalDisc)
        Me.BalanceGroup.Controls.Add(Me.lblTotalTrts)
        Me.BalanceGroup.Controls.Add(Me.LabeTotalPayments)
        Me.BalanceGroup.Controls.Add(Me.LabelTotalDiscounts)
        Me.BalanceGroup.Controls.Add(Me.LabelTotalTreatments)
        resources.ApplyResources(Me.BalanceGroup, "BalanceGroup")
        Me.BalanceGroup.Name = "BalanceGroup"
        '
        'lblBal
        '
        Me.lblBal.Appearance.Font = CType(resources.GetObject("lblBal.Appearance.Font"), System.Drawing.Font)
        Me.lblBal.Appearance.Options.UseFont = True
        Me.lblBal.Appearance.Options.UseTextOptions = True
        Me.lblBal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblBal.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.lblBal, "lblBal")
        Me.lblBal.Name = "lblBal"
        '
        'LabelFinalBalance
        '
        Me.LabelFinalBalance.Appearance.Font = CType(resources.GetObject("LabelFinalBalance.Appearance.Font"), System.Drawing.Font)
        Me.LabelFinalBalance.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelFinalBalance, "LabelFinalBalance")
        Me.LabelFinalBalance.Name = "LabelFinalBalance"
        '
        'lblTotalPays
        '
        Me.lblTotalPays.Appearance.Font = CType(resources.GetObject("lblTotalPays.Appearance.Font"), System.Drawing.Font)
        Me.lblTotalPays.Appearance.Options.UseFont = True
        Me.lblTotalPays.Appearance.Options.UseTextOptions = True
        Me.lblTotalPays.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblTotalPays.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.lblTotalPays, "lblTotalPays")
        Me.lblTotalPays.Name = "lblTotalPays"
        '
        'lblTotalDisc
        '
        Me.lblTotalDisc.Appearance.Font = CType(resources.GetObject("lblTotalDisc.Appearance.Font"), System.Drawing.Font)
        Me.lblTotalDisc.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTotalDisc.Appearance.Options.UseFont = True
        Me.lblTotalDisc.Appearance.Options.UseForeColor = True
        Me.lblTotalDisc.Appearance.Options.UseTextOptions = True
        Me.lblTotalDisc.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblTotalDisc.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.lblTotalDisc, "lblTotalDisc")
        Me.lblTotalDisc.Name = "lblTotalDisc"
        '
        'lblTotalTrts
        '
        Me.lblTotalTrts.Appearance.Font = CType(resources.GetObject("lblTotalTrts.Appearance.Font"), System.Drawing.Font)
        Me.lblTotalTrts.Appearance.Options.UseFont = True
        Me.lblTotalTrts.Appearance.Options.UseTextOptions = True
        Me.lblTotalTrts.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblTotalTrts.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.lblTotalTrts, "lblTotalTrts")
        Me.lblTotalTrts.Name = "lblTotalTrts"
        '
        'LabeTotalPayments
        '
        Me.LabeTotalPayments.Appearance.Font = CType(resources.GetObject("LabeTotalPayments.Appearance.Font"), System.Drawing.Font)
        Me.LabeTotalPayments.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabeTotalPayments, "LabeTotalPayments")
        Me.LabeTotalPayments.Name = "LabeTotalPayments"
        '
        'LabelTotalDiscounts
        '
        Me.LabelTotalDiscounts.Appearance.Font = CType(resources.GetObject("LabelTotalDiscounts.Appearance.Font"), System.Drawing.Font)
        Me.LabelTotalDiscounts.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelTotalDiscounts, "LabelTotalDiscounts")
        Me.LabelTotalDiscounts.Name = "LabelTotalDiscounts"
        '
        'LabelTotalTreatments
        '
        Me.LabelTotalTreatments.Appearance.Font = CType(resources.GetObject("LabelTotalTreatments.Appearance.Font"), System.Drawing.Font)
        Me.LabelTotalTreatments.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelTotalTreatments, "LabelTotalTreatments")
        Me.LabelTotalTreatments.Name = "LabelTotalTreatments"
        '
        'grpPayDet
        '
        Me.grpPayDet.AppearanceCaption.BorderColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grpPayDet.AppearanceCaption.Font = CType(resources.GetObject("grpPayDet.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpPayDet.AppearanceCaption.ForeColor = System.Drawing.Color.Green
        Me.grpPayDet.AppearanceCaption.Options.UseBorderColor = True
        Me.grpPayDet.AppearanceCaption.Options.UseFont = True
        Me.grpPayDet.AppearanceCaption.Options.UseForeColor = True
        Me.grpPayDet.AppearanceCaption.Options.UseTextOptions = True
        Me.grpPayDet.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.grpPayDet.Controls.Add(Me.payDetailsPanel)
        Me.grpPayDet.Controls.Add(Me.payGridsPanel)
        resources.ApplyResources(Me.grpPayDet, "grpPayDet")
        Me.grpPayDet.Name = "grpPayDet"
        '
        'payDetailsPanel
        '
        Me.payDetailsPanel.Controls.Add(Me.chqInsurTab)
        Me.payDetailsPanel.Controls.Add(Me.PayMainDetPanel)
        resources.ApplyResources(Me.payDetailsPanel, "payDetailsPanel")
        Me.payDetailsPanel.Name = "payDetailsPanel"
        '
        'chqInsurTab
        '
        Me.chqInsurTab.AppearancePage.Header.Font = CType(resources.GetObject("chqInsurTab.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.chqInsurTab.AppearancePage.Header.Options.UseFont = True
        resources.ApplyResources(Me.chqInsurTab, "chqInsurTab")
        Me.chqInsurTab.Name = "chqInsurTab"
        Me.chqInsurTab.SelectedTabPage = Me.ChqPage
        Me.chqInsurTab.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.ChqPage, Me.InsurePage})
        '
        'ChqPage
        '
        Me.ChqPage.Controls.Add(Me.btnBrowse)
        Me.ChqPage.Controls.Add(Me.btnResetChqs)
        Me.ChqPage.Controls.Add(Me.btnScanAndPay)
        Me.ChqPage.Controls.Add(Me.btnScan)
        Me.ChqPage.Controls.Add(Me.txtChqOwner)
        Me.ChqPage.Controls.Add(Me.chkIsCashed)
        Me.ChqPage.Controls.Add(Me.dtChqDueDate)
        Me.ChqPage.Controls.Add(Me.txtChqBank)
        Me.ChqPage.Controls.Add(Me.LabelControl14)
        Me.ChqPage.Controls.Add(Me.txtChqNumber)
        Me.ChqPage.Controls.Add(Me.LabelControl15)
        Me.ChqPage.Controls.Add(Me.txtAccountNumber)
        Me.ChqPage.Controls.Add(Me.LabelControl18)
        Me.ChqPage.Controls.Add(Me.LabelControl17)
        Me.ChqPage.Controls.Add(Me.LabelControl16)
        Me.ChqPage.Controls.Add(Me.LabelControl19)
        Me.ChqPage.Controls.Add(Me.ChqValue)
        Me.ChqPage.Name = "ChqPage"
        resources.ApplyResources(Me.ChqPage, "ChqPage")
        '
        'btnBrowse
        '
        Me.btnBrowse.Appearance.Font = CType(resources.GetObject("btnBrowse.Appearance.Font"), System.Drawing.Font)
        Me.btnBrowse.Appearance.Options.UseFont = True
        Me.btnBrowse.ImageOptions.ImageKey = resources.GetString("btnBrowse.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnBrowse, "btnBrowse")
        Me.btnBrowse.Name = "btnBrowse"
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
        'btnScan
        '
        Me.btnScan.Appearance.Font = CType(resources.GetObject("btnScan.Appearance.Font"), System.Drawing.Font)
        Me.btnScan.Appearance.Options.UseFont = True
        Me.btnScan.ImageOptions.ImageKey = resources.GetString("btnScan.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnScan, "btnScan")
        Me.btnScan.Name = "btnScan"
        '
        'txtChqOwner
        '
        Me.txtChqOwner.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqOwner, "txtChqOwner")
        Me.txtChqOwner.Name = "txtChqOwner"
        Me.txtChqOwner.Properties.Appearance.Font = CType(resources.GetObject("txtChqOwner.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqOwner.Properties.Appearance.Options.UseFont = True
        '
        'chkIsCashed
        '
        resources.ApplyResources(Me.chkIsCashed, "chkIsCashed")
        Me.chkIsCashed.Name = "chkIsCashed"
        Me.chkIsCashed.Properties.Appearance.Font = CType(resources.GetObject("chkIsCashed.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkIsCashed.Properties.Appearance.Options.UseFont = True
        Me.chkIsCashed.Properties.Caption = resources.GetString("chkIsCashed.Properties.Caption")
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
        'txtChqBank
        '
        Me.txtChqBank.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqBank, "txtChqBank")
        Me.txtChqBank.Name = "txtChqBank"
        Me.txtChqBank.Properties.Appearance.Font = CType(resources.GetObject("txtChqBank.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqBank.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.Font = CType(resources.GetObject("LabelControl14.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl14.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl14, "LabelControl14")
        Me.LabelControl14.Name = "LabelControl14"
        '
        'txtChqNumber
        '
        Me.txtChqNumber.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqNumber, "txtChqNumber")
        Me.txtChqNumber.Name = "txtChqNumber"
        Me.txtChqNumber.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtChqNumber.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtChqNumber.Properties.MaskSettings.Set("mask", "d")
        '
        'LabelControl15
        '
        Me.LabelControl15.Appearance.Font = CType(resources.GetObject("LabelControl15.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl15.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl15, "LabelControl15")
        Me.LabelControl15.Name = "LabelControl15"
        '
        'txtAccountNumber
        '
        Me.txtAccountNumber.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtAccountNumber, "txtAccountNumber")
        Me.txtAccountNumber.Name = "txtAccountNumber"
        Me.txtAccountNumber.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtAccountNumber.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtAccountNumber.Properties.MaskSettings.Set("mask", "d")
        '
        'LabelControl18
        '
        Me.LabelControl18.Appearance.Font = CType(resources.GetObject("LabelControl18.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl18.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl18, "LabelControl18")
        Me.LabelControl18.Name = "LabelControl18"
        '
        'LabelControl17
        '
        Me.LabelControl17.Appearance.Font = CType(resources.GetObject("LabelControl17.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl17.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl17, "LabelControl17")
        Me.LabelControl17.Name = "LabelControl17"
        '
        'LabelControl16
        '
        Me.LabelControl16.Appearance.Font = CType(resources.GetObject("LabelControl16.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl16.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl16, "LabelControl16")
        Me.LabelControl16.Name = "LabelControl16"
        '
        'LabelControl19
        '
        Me.LabelControl19.Appearance.Font = CType(resources.GetObject("LabelControl19.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl19.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl19, "LabelControl19")
        Me.LabelControl19.Name = "LabelControl19"
        '
        'ChqValue
        '
        resources.ApplyResources(Me.ChqValue, "ChqValue")
        Me.ChqValue.Name = "ChqValue"
        '
        'InsurePage
        '
        Me.InsurePage.Controls.Add(Me.txtInsurNotes)
        Me.InsurePage.Controls.Add(Me.txtInureComp)
        Me.InsurePage.Controls.Add(Me.LabelControl21)
        Me.InsurePage.Controls.Add(Me.LabelControl20)
        Me.InsurePage.Name = "InsurePage"
        resources.ApplyResources(Me.InsurePage, "InsurePage")
        '
        'txtInsurNotes
        '
        resources.ApplyResources(Me.txtInsurNotes, "txtInsurNotes")
        Me.txtInsurNotes.Name = "txtInsurNotes"
        '
        'txtInureComp
        '
        resources.ApplyResources(Me.txtInureComp, "txtInureComp")
        Me.txtInureComp.Name = "txtInureComp"
        '
        'LabelControl21
        '
        Me.LabelControl21.Appearance.Font = CType(resources.GetObject("LabelControl21.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl21.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl21, "LabelControl21")
        Me.LabelControl21.Name = "LabelControl21"
        '
        'LabelControl20
        '
        Me.LabelControl20.Appearance.Font = CType(resources.GetObject("LabelControl20.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl20.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl20, "LabelControl20")
        Me.LabelControl20.Name = "LabelControl20"
        '
        'PayMainDetPanel
        '
        Me.PayMainDetPanel.Controls.Add(Me.cboPayType)
        Me.PayMainDetPanel.Controls.Add(Me.btnAddPay)
        Me.PayMainDetPanel.Controls.Add(Me.LabelControl7)
        Me.PayMainDetPanel.Controls.Add(Me.LabelControl13)
        Me.PayMainDetPanel.Controls.Add(Me.LabelControl8)
        Me.PayMainDetPanel.Controls.Add(Me.lblTreat)
        Me.PayMainDetPanel.Controls.Add(Me.LabelControl24)
        Me.PayMainDetPanel.Controls.Add(Me.LabelControl9)
        Me.PayMainDetPanel.Controls.Add(Me.PayDate)
        Me.PayMainDetPanel.Controls.Add(Me.NotesText)
        Me.PayMainDetPanel.Controls.Add(Me.PayValue)
        resources.ApplyResources(Me.PayMainDetPanel, "PayMainDetPanel")
        Me.PayMainDetPanel.Name = "PayMainDetPanel"
        '
        'cboPayType
        '
        resources.ApplyResources(Me.cboPayType, "cboPayType")
        Me.cboPayType.EnterMoveNextControl = True
        Me.cboPayType.Name = "cboPayType"
        Me.cboPayType.Properties.Appearance.Font = CType(resources.GetObject("cboPayType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboPayType.Properties.Appearance.Options.UseFont = True
        Me.cboPayType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPayType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboPayType.Properties.Items.AddRange(New Object() {resources.GetString("cboPayType.Properties.Items"), resources.GetString("cboPayType.Properties.Items1"), resources.GetString("cboPayType.Properties.Items2"), resources.GetString("cboPayType.Properties.Items3"), resources.GetString("cboPayType.Properties.Items4")})
        '
        'btnAddPay
        '
        Me.btnAddPay.Appearance.Font = CType(resources.GetObject("btnAddPay.Appearance.Font"), System.Drawing.Font)
        Me.btnAddPay.Appearance.Options.UseFont = True
        Me.btnAddPay.ImageOptions.ImageKey = resources.GetString("btnAddPay.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnAddPay, "btnAddPay")
        Me.btnAddPay.Name = "btnAddPay"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Name = "LabelControl7"
        '
        'LabelControl13
        '
        Me.LabelControl13.Appearance.Font = CType(resources.GetObject("LabelControl13.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl13.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl13, "LabelControl13")
        Me.LabelControl13.Name = "LabelControl13"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Name = "LabelControl8"
        '
        'lblTreat
        '
        Me.lblTreat.Appearance.Font = CType(resources.GetObject("lblTreat.Appearance.Font"), System.Drawing.Font)
        Me.lblTreat.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblTreat.Appearance.Options.UseFont = True
        Me.lblTreat.Appearance.Options.UseForeColor = True
        Me.lblTreat.Appearance.Options.UseTextOptions = True
        Me.lblTreat.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.lblTreat.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblTreat.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        resources.ApplyResources(Me.lblTreat, "lblTreat")
        Me.lblTreat.Name = "lblTreat"
        '
        'LabelControl24
        '
        Me.LabelControl24.Appearance.Font = CType(resources.GetObject("LabelControl24.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl24.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl24.Appearance.Options.UseFont = True
        Me.LabelControl24.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl24, "LabelControl24")
        Me.LabelControl24.Name = "LabelControl24"
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = CType(resources.GetObject("LabelControl9.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl9.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl9, "LabelControl9")
        Me.LabelControl9.Name = "LabelControl9"
        '
        'PayDate
        '
        resources.ApplyResources(Me.PayDate, "PayDate")
        Me.PayDate.EnterMoveNextControl = True
        Me.PayDate.Name = "PayDate"
        Me.PayDate.Properties.Appearance.Font = CType(resources.GetObject("PayDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PayDate.Properties.Appearance.Options.UseFont = True
        Me.PayDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("PayDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.PayDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("PayDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.PayDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.PayDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.PayDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.PayDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '
        'NotesText
        '
        resources.ApplyResources(Me.NotesText, "NotesText")
        Me.NotesText.Name = "NotesText"
        Me.NotesText.Properties.Appearance.Font = CType(resources.GetObject("NotesText.Properties.Appearance.Font"), System.Drawing.Font)
        Me.NotesText.Properties.Appearance.Options.UseFont = True
        '
        'PayValue
        '
        resources.ApplyResources(Me.PayValue, "PayValue")
        Me.PayValue.Name = "PayValue"
        '
        'payGridsPanel
        '
        Me.payGridsPanel.Controls.Add(Me.GridTabControl)
        Me.payGridsPanel.Controls.Add(Me.PanelControl1)
        resources.ApplyResources(Me.payGridsPanel, "payGridsPanel")
        Me.payGridsPanel.Name = "payGridsPanel"
        '
        'GridTabControl
        '
        Me.GridTabControl.AppearancePage.Header.Font = CType(resources.GetObject("GridTabControl.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.GridTabControl.AppearancePage.Header.Options.UseFont = True
        resources.ApplyResources(Me.GridTabControl, "GridTabControl")
        Me.GridTabControl.Name = "GridTabControl"
        Me.GridTabControl.SelectedTabPage = Me.CashGridPage
        Me.GridTabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.CashGridPage, Me.ChqGridPage, Me.ScannedPage, Me.AttachedPage})
        '
        'CashGridPage
        '
        Me.CashGridPage.Controls.Add(Me.Patient_PaysGridControl)
        Me.CashGridPage.Name = "CashGridPage"
        resources.ApplyResources(Me.CashGridPage, "CashGridPage")
        '
        'Patient_PaysGridControl
        '
        Me.Patient_PaysGridControl.DataSource = Me.Patient_PaysBindingSource
        resources.ApplyResources(Me.Patient_PaysGridControl, "Patient_PaysGridControl")
        Me.Patient_PaysGridControl.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("Patient_PaysGridControl.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.Patient_PaysGridControl.EmbeddedNavigator.Anchor = CType(resources.GetObject("Patient_PaysGridControl.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.Patient_PaysGridControl.EmbeddedNavigator.Appearance.Font = CType(resources.GetObject("Patient_PaysGridControl.EmbeddedNavigator.Appearance.Font"), System.Drawing.Font)
        Me.Patient_PaysGridControl.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.Patient_PaysGridControl.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("Patient_PaysGridControl.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.Patient_PaysGridControl.EmbeddedNavigator.Buttons.Append.Hint = resources.GetString("Patient_PaysGridControl.EmbeddedNavigator.Buttons.Append.Hint")
        Me.Patient_PaysGridControl.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.Patient_PaysGridControl.EmbeddedNavigator.ImeMode = CType(resources.GetObject("Patient_PaysGridControl.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.Patient_PaysGridControl.EmbeddedNavigator.TextLocation = CType(resources.GetObject("Patient_PaysGridControl.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.Patient_PaysGridControl.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("Patient_PaysGridControl.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.Patient_PaysGridControl.MainView = Me.GridViewPays
        Me.Patient_PaysGridControl.Name = "Patient_PaysGridControl"
        Me.Patient_PaysGridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewPays})
        '
        'Patient_PaysBindingSource
        '
        Me.Patient_PaysBindingSource.DataMember = "Patient_PaysIEnumerable"
        Me.Patient_PaysBindingSource.DataSource = Me.Patient_TrtsBindingSource
        '
        'GridViewPays
        '
        Me.GridViewPays.Appearance.FooterPanel.FontStyleDelta = CType(resources.GetObject("GridViewPays.Appearance.FooterPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridViewPays.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Red
        Me.GridViewPays.Appearance.FooterPanel.Options.UseFont = True
        Me.GridViewPays.Appearance.FooterPanel.Options.UseForeColor = True
        Me.GridViewPays.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.GridViewPays.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewPays.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridViewPays.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridViewPays.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewPays.Appearance.Row.Font = CType(resources.GetObject("GridViewPays.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridViewPays.Appearance.Row.Options.UseFont = True
        Me.GridViewPays.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum1, Me.colPayID, Me.colTrtID1, Me.colPatientID1, Me.colPayValue, Me.colPayDate, Me.colNotes})
        Me.GridViewPays.GridControl = Me.Patient_PaysGridControl
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
        resources.ApplyResources(Me.colRowNum1, "colRowNum1")
        Me.colRowNum1.FieldName = "colRowNum1"
        Me.colRowNum1.ImageOptions.ImageKey = resources.GetString("colRowNum1.ImageOptions.ImageKey")
        Me.colRowNum1.Name = "colRowNum1"
        Me.colRowNum1.UnboundDataType = GetType(Integer)
        '
        'colPayID
        '
        Me.colPayID.FieldName = "PayID"
        Me.colPayID.ImageOptions.ImageKey = resources.GetString("colPayID.ImageOptions.ImageKey")
        Me.colPayID.Name = "colPayID"
        '
        'colTrtID1
        '
        Me.colTrtID1.FieldName = "TrtID"
        Me.colTrtID1.ImageOptions.ImageKey = resources.GetString("colTrtID1.ImageOptions.ImageKey")
        Me.colTrtID1.Name = "colTrtID1"
        '
        'colPatientID1
        '
        Me.colPatientID1.FieldName = "PatientID"
        Me.colPatientID1.ImageOptions.ImageKey = resources.GetString("colPatientID1.ImageOptions.ImageKey")
        Me.colPatientID1.Name = "colPatientID1"
        '
        'colPayValue
        '
        resources.ApplyResources(Me.colPayValue, "colPayValue")
        Me.colPayValue.DisplayFormat.FormatString = "n0"
        Me.colPayValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPayValue.FieldName = "PayValue"
        Me.colPayValue.ImageOptions.ImageKey = resources.GetString("colPayValue.ImageOptions.ImageKey")
        Me.colPayValue.Name = "colPayValue"
        Me.colPayValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("colPayValue.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("colPayValue.Summary1"), resources.GetString("colPayValue.Summary2"))})
        Me.colPayValue.UnboundDataType = GetType(Double)
        '
        'colPayDate
        '
        resources.ApplyResources(Me.colPayDate, "colPayDate")
        Me.colPayDate.FieldName = "PayDate"
        Me.colPayDate.ImageOptions.ImageKey = resources.GetString("colPayDate.ImageOptions.ImageKey")
        Me.colPayDate.Name = "colPayDate"
        Me.colPayDate.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("colPayDate.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("colPayDate.Summary1"), resources.GetString("colPayDate.Summary2"))})
        '
        'colNotes
        '
        resources.ApplyResources(Me.colNotes, "colNotes")
        Me.colNotes.FieldName = "Notes"
        Me.colNotes.ImageOptions.ImageKey = resources.GetString("colNotes.ImageOptions.ImageKey")
        Me.colNotes.Name = "colNotes"
        '
        'ChqGridPage
        '
        Me.ChqGridPage.Controls.Add(Me.ChqsPayGrid)
        Me.ChqGridPage.Name = "ChqGridPage"
        resources.ApplyResources(Me.ChqGridPage, "ChqGridPage")
        '
        'ChqsPayGrid
        '
        Me.ChqsPayGrid.DataSource = Me.Patient_PaysBindingSource
        resources.ApplyResources(Me.ChqsPayGrid, "ChqsPayGrid")
        Me.ChqsPayGrid.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("ChqsPayGrid.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.ChqsPayGrid.EmbeddedNavigator.Anchor = CType(resources.GetObject("ChqsPayGrid.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.ChqsPayGrid.EmbeddedNavigator.Appearance.Font = CType(resources.GetObject("ChqsPayGrid.EmbeddedNavigator.Appearance.Font"), System.Drawing.Font)
        Me.ChqsPayGrid.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.ChqsPayGrid.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("ChqsPayGrid.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.ChqsPayGrid.EmbeddedNavigator.Buttons.Append.Hint = resources.GetString("ChqsPayGrid.EmbeddedNavigator.Buttons.Append.Hint")
        Me.ChqsPayGrid.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.ChqsPayGrid.EmbeddedNavigator.ImeMode = CType(resources.GetObject("ChqsPayGrid.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.ChqsPayGrid.EmbeddedNavigator.TextLocation = CType(resources.GetObject("ChqsPayGrid.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.ChqsPayGrid.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("ChqsPayGrid.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.ChqsPayGrid.MainView = Me.ChqsGridView
        Me.ChqsPayGrid.Name = "ChqsPayGrid"
        Me.ChqsPayGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ChqsGridView})
        '
        'ChqsGridView
        '
        Me.ChqsGridView.Appearance.FooterPanel.FontStyleDelta = CType(resources.GetObject("ChqsGridView.Appearance.FooterPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.ChqsGridView.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Red
        Me.ChqsGridView.Appearance.FooterPanel.Options.UseFont = True
        Me.ChqsGridView.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ChqsGridView.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.ChqsGridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ChqsGridView.Appearance.HeaderPanel.Font = CType(resources.GetObject("ChqsGridView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.ChqsGridView.Appearance.HeaderPanel.Options.UseFont = True
        Me.ChqsGridView.Appearance.Row.Font = CType(resources.GetObject("ChqsGridView.Appearance.Row.Font"), System.Drawing.Font)
        Me.ChqsGridView.Appearance.Row.Options.UseFont = True
        Me.ChqsGridView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum2, Me.colChqsPayID, Me.colChqPatientID, Me.colChqsPayValue, Me.colChqsPayDate, Me.colChqsPayNotes, Me.colChqNumber, Me.colChqAccount, Me.colChqDueDate, Me.colChqBank, Me.colChqOwner})
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
        resources.ApplyResources(Me.colRowNum2, "colRowNum2")
        Me.colRowNum2.FieldName = "colRowNum2"
        Me.colRowNum2.ImageOptions.ImageKey = resources.GetString("colRowNum2.ImageOptions.ImageKey")
        Me.colRowNum2.Name = "colRowNum2"
        Me.colRowNum2.UnboundDataType = GetType(Integer)
        '
        'colChqsPayID
        '
        Me.colChqsPayID.FieldName = "PayID"
        Me.colChqsPayID.ImageOptions.ImageKey = resources.GetString("colChqsPayID.ImageOptions.ImageKey")
        Me.colChqsPayID.Name = "colChqsPayID"
        '
        'colChqPatientID
        '
        resources.ApplyResources(Me.colChqPatientID, "colChqPatientID")
        Me.colChqPatientID.FieldName = "PatientID"
        Me.colChqPatientID.ImageOptions.ImageKey = resources.GetString("colChqPatientID.ImageOptions.ImageKey")
        Me.colChqPatientID.Name = "colChqPatientID"
        '
        'colChqsPayValue
        '
        resources.ApplyResources(Me.colChqsPayValue, "colChqsPayValue")
        Me.colChqsPayValue.DisplayFormat.FormatString = "n0"
        Me.colChqsPayValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colChqsPayValue.FieldName = "PayValue"
        Me.colChqsPayValue.ImageOptions.ImageKey = resources.GetString("colChqsPayValue.ImageOptions.ImageKey")
        Me.colChqsPayValue.Name = "colChqsPayValue"
        Me.colChqsPayValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("colChqsPayValue.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("colChqsPayValue.Summary1"), resources.GetString("colChqsPayValue.Summary2"))})
        Me.colChqsPayValue.UnboundDataType = GetType(Double)
        '
        'colChqsPayDate
        '
        resources.ApplyResources(Me.colChqsPayDate, "colChqsPayDate")
        Me.colChqsPayDate.FieldName = "PayDate"
        Me.colChqsPayDate.ImageOptions.ImageKey = resources.GetString("colChqsPayDate.ImageOptions.ImageKey")
        Me.colChqsPayDate.Name = "colChqsPayDate"
        Me.colChqsPayDate.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("colChqsPayDate.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("colChqsPayDate.Summary1"), resources.GetString("colChqsPayDate.Summary2"))})
        '
        'colChqsPayNotes
        '
        resources.ApplyResources(Me.colChqsPayNotes, "colChqsPayNotes")
        Me.colChqsPayNotes.FieldName = "Notes"
        Me.colChqsPayNotes.ImageOptions.ImageKey = resources.GetString("colChqsPayNotes.ImageOptions.ImageKey")
        Me.colChqsPayNotes.Name = "colChqsPayNotes"
        '
        'colChqNumber
        '
        resources.ApplyResources(Me.colChqNumber, "colChqNumber")
        Me.colChqNumber.FieldName = "ChqNumber"
        Me.colChqNumber.ImageOptions.ImageKey = resources.GetString("colChqNumber.ImageOptions.ImageKey")
        Me.colChqNumber.Name = "colChqNumber"
        '
        'colChqAccount
        '
        resources.ApplyResources(Me.colChqAccount, "colChqAccount")
        Me.colChqAccount.FieldName = "AccountNumber"
        Me.colChqAccount.ImageOptions.ImageKey = resources.GetString("colChqAccount.ImageOptions.ImageKey")
        Me.colChqAccount.Name = "colChqAccount"
        '
        'colChqDueDate
        '
        resources.ApplyResources(Me.colChqDueDate, "colChqDueDate")
        Me.colChqDueDate.FieldName = "ChqDueDate"
        Me.colChqDueDate.ImageOptions.ImageKey = resources.GetString("colChqDueDate.ImageOptions.ImageKey")
        Me.colChqDueDate.Name = "colChqDueDate"
        '
        'colChqBank
        '
        resources.ApplyResources(Me.colChqBank, "colChqBank")
        Me.colChqBank.FieldName = "ChqBank"
        Me.colChqBank.ImageOptions.ImageKey = resources.GetString("colChqBank.ImageOptions.ImageKey")
        Me.colChqBank.Name = "colChqBank"
        '
        'colChqOwner
        '
        resources.ApplyResources(Me.colChqOwner, "colChqOwner")
        Me.colChqOwner.FieldName = "ChqOwner"
        Me.colChqOwner.ImageOptions.ImageKey = resources.GetString("colChqOwner.ImageOptions.ImageKey")
        Me.colChqOwner.Name = "colChqOwner"
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
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.PayNavigator)
        resources.ApplyResources(Me.PanelControl1, "PanelControl1")
        Me.PanelControl1.Name = "PanelControl1"
        '
        'PayNavigator
        '
        Me.PayNavigator.AddNewItem = Nothing
        resources.ApplyResources(Me.PayNavigator, "PayNavigator")
        Me.PayNavigator.BindingSource = Me.Patient_PaysBindingSource
        Me.PayNavigator.CountItem = Me.ToolStripLabel3
        Me.PayNavigator.DeleteItem = Nothing
        Me.PayNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.PayNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton15, Me.ToolStripButton16, Me.ToolStripSeparator6, Me.ToolStripTextBox3, Me.ToolStripLabel3, Me.ToolStripSeparator7, Me.ToolStripButton17, Me.ToolStripButton18, Me.PaySavNav, Me.ToolStripSeparator2, Me.btnPayDel})
        Me.PayNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.PayNavigator.MoveFirstItem = Me.ToolStripButton15
        Me.PayNavigator.MoveLastItem = Me.ToolStripButton18
        Me.PayNavigator.MoveNextItem = Me.ToolStripButton17
        Me.PayNavigator.MovePreviousItem = Me.ToolStripButton16
        Me.PayNavigator.Name = "PayNavigator"
        Me.PayNavigator.PositionItem = Me.ToolStripTextBox3
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        resources.ApplyResources(Me.ToolStripLabel3, "ToolStripLabel3")
        '
        'ToolStripButton15
        '
        Me.ToolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton15, "ToolStripButton15")
        Me.ToolStripButton15.Name = "ToolStripButton15"
        '
        'ToolStripButton16
        '
        Me.ToolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton16, "ToolStripButton16")
        Me.ToolStripButton16.Name = "ToolStripButton16"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        resources.ApplyResources(Me.ToolStripSeparator6, "ToolStripSeparator6")
        '
        'ToolStripTextBox3
        '
        resources.ApplyResources(Me.ToolStripTextBox3, "ToolStripTextBox3")
        Me.ToolStripTextBox3.Name = "ToolStripTextBox3"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        resources.ApplyResources(Me.ToolStripSeparator7, "ToolStripSeparator7")
        '
        'ToolStripButton17
        '
        Me.ToolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton17, "ToolStripButton17")
        Me.ToolStripButton17.Name = "ToolStripButton17"
        '
        'ToolStripButton18
        '
        Me.ToolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.ToolStripButton18, "ToolStripButton18")
        Me.ToolStripButton18.Name = "ToolStripButton18"
        '
        'PaySavNav
        '
        Me.PaySavNav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.PaySavNav, "PaySavNav")
        Me.PaySavNav.Name = "PaySavNav"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'btnPayDel
        '
        Me.btnPayDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.btnPayDel, "btnPayDel")
        Me.btnPayDel.Name = "btnPayDel"
        '
        'Accounting
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpPayDet)
        Me.Controls.Add(Me.BalanceGroup)
        Me.Controls.Add(Me.grpTrtDet)
        Me.Name = "Accounting"
        CType(Me.SplitterCtl.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtl.Panel1.ResumeLayout(False)
        CType(Me.SplitterCtl.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtl.Panel2.ResumeLayout(False)
        CType(Me.SplitterCtl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtl.ResumeLayout(False)
        CType(Me.scannedFilesList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.scanPreview.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpTrtDet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTrtDet.ResumeLayout(False)
        Me.grpTrtDet.PerformLayout()
        CType(Me.TrtDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAddTrt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SidePanel1.ResumeLayout(False)
        CType(Me.Patient_TrtsGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Patient_TrtsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewTrts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TrtNavigator.ResumeLayout(False)
        Me.TrtNavigator.PerformLayout()
        CType(Me.txtTrtDetails.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetailText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.showAsChildChck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtDiscount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BalanceGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BalanceGroup.ResumeLayout(False)
        Me.BalanceGroup.PerformLayout()
        CType(Me.PatientBalanceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpPayDet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPayDet.ResumeLayout(False)
        Me.payDetailsPanel.ResumeLayout(False)
        CType(Me.chqInsurTab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.chqInsurTab.ResumeLayout(False)
        Me.ChqPage.ResumeLayout(False)
        Me.ChqPage.PerformLayout()
        CType(Me.txtChqOwner.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsCashed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtChqDueDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtChqDueDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqBank.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccountNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChqValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.InsurePage.ResumeLayout(False)
        Me.InsurePage.PerformLayout()
        CType(Me.txtInsurNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInureComp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PayMainDetPanel.ResumeLayout(False)
        Me.PayMainDetPanel.PerformLayout()
        CType(Me.cboPayType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NotesText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.payGridsPanel.ResumeLayout(False)
        CType(Me.GridTabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GridTabControl.ResumeLayout(False)
        Me.CashGridPage.ResumeLayout(False)
        CType(Me.Patient_PaysGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Patient_PaysBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewPays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ChqGridPage.ResumeLayout(False)
        CType(Me.ChqsPayGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChqsGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ScannedPage.ResumeLayout(False)
        Me.AttachedPage.ResumeLayout(False)
        CType(Me.GridAttached, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewAttached, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PayNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PayNavigator.ResumeLayout(False)
        Me.PayNavigator.PerformLayout()
        CType(Me.PatientTrtPaysBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtsPaysListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpTrtDet As DevExpress.XtraEditors.GroupControl
    Friend WithEvents BalanceGroup As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpPayDet As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TrtNavigator As BindingNavigator
    Friend WithEvents Patient_TrtsBindingSource As BindingSource
    Friend WithEvents ToolStripLabel5 As ToolStripLabel
    Friend WithEvents ToolStripButton23 As ToolStripButton
    Friend WithEvents ToolStripButton24 As ToolStripButton
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox5 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents ToolStripButton25 As ToolStripButton
    Friend WithEvents ToolStripButton26 As ToolStripButton
    Friend WithEvents TrtSavNav As ToolStripButton
    Friend WithEvents Patient_PaysBindingSource As BindingSource
    Friend WithEvents PatientTrtPaysBindingSource As BindingSource
    Friend WithEvents PatientBalanceBindingSource As BindingSource
    Friend WithEvents TrtsPaysListBindingSource As BindingSource
    Friend WithEvents Patient_TrtsGridControl As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewTrts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrtID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colToothTrtID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrtDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrtValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents payDetailsPanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents chqInsurTab As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents ChqPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents btnResetChqs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtChqOwner As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkIsCashed As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dtChqDueDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtChqBank As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtChqNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtAccountNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents InsurePage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents txtInsurNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtInureComp As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl21 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl20 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PayMainDetPanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents cboPayType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnAddPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PayDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents payGridsPanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents SidePanel1 As DevExpress.XtraEditors.SidePanel
    Friend WithEvents btnEditGrid As DevExpress.XtraEditors.CheckButton
    Friend WithEvents DetailText As DevExpress.XtraEditors.TextEdit
    Friend WithEvents showAsChildChck As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnModifyValue As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnResetPatientAccount As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl23 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTrtID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PayNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents ToolStripButton15 As ToolStripButton
    Friend WithEvents ToolStripButton16 As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox3 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripButton17 As ToolStripButton
    Friend WithEvents ToolStripButton18 As ToolStripButton
    Friend WithEvents PaySavNav As ToolStripButton
    Friend WithEvents GridTabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents CashGridPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Patient_PaysGridControl As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewPays As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrtID1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientID1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ChqGridPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ChqsPayGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents ChqsGridView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqsPayID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqsPayValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqsPayDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqsPayNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqAccount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqDueDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqBank As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqOwner As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblTotalPays As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalTrts As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabeTotalPayments As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelTotalTreatments As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelFinalBalance As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTreat As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl24 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnTrtDel As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnPayDel As ToolStripButton
    Friend WithEvents colOrthoID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOtherTrtID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIsMultiTooth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiscount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiscountType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RadioFilter As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents btnInvoiceGen As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkAddTrt As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtTrtDetails As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblTrtDet As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnAddTrt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTotalDisc As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnScan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBrowse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ScannedPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitterCtl As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents scannedFilesList As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents scanPreview As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents AttachedPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridAttached As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewAttached As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents NotesText As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents TrtValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents ChqValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents PayValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents TrtDiscount As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents btnScanAndPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnWhatsSend As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelTotalDiscounts As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TrtDate As DevExpress.XtraEditors.DateEdit
End Class
