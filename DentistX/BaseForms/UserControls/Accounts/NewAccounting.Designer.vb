<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NewAccounting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewAccounting))
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule2 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue2 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Me.MainSplit = New DevExpress.XtraEditors.SplitContainerControl()
        Me.TrtSplit = New DevExpress.XtraEditors.SplitContainerControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.chkFullDetail = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.RadioLang = New DevExpress.XtraEditors.RadioGroup()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.btnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.btnInvoiceGen = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddTreat = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditTrt = New DevExpress.XtraEditors.SimpleButton()
        Me.btnResetPatientAccount = New DevExpress.XtraEditors.SimpleButton()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnWhatsSend = New DevExpress.XtraEditors.SimpleButton()
        Me.showAsChildChck = New DevExpress.XtraEditors.CheckEdit()
        Me.RadioFilter = New DevExpress.XtraEditors.RadioGroup()
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
        Me.colTrtValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIsMultiTooth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDiscount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDiscount2 = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.lblTreatsDetails = New DevExpress.XtraEditors.LabelControl()
        Me.PaysSplit = New DevExpress.XtraEditors.SplitContainerControl()
        Me.SidePanel2 = New DevExpress.XtraEditors.SidePanel()
        Me.SidePanel1 = New DevExpress.XtraEditors.SidePanel()
        Me.btnBrowse = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditPay = New DevExpress.XtraEditors.SimpleButton()
        Me.btnScanScan2Pages = New DevExpress.XtraEditors.SimpleButton()
        Me.cboPayType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnScan = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddPay = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.GridTabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.AllPaysPage = New DevExpress.XtraTab.XtraTabPage()
        Me.AllPaysGrid = New DevExpress.XtraGrid.GridControl()
        Me.Patient_PaysBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.AllPaysView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CashGridPage = New DevExpress.XtraTab.XtraTabPage()
        Me.Patient_PaysGridControl = New DevExpress.XtraGrid.GridControl()
        Me.GridViewPays = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTrtID1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ChqGridPage = New DevExpress.XtraTab.XtraTabPage()
        Me.ChqsPayGrid = New DevExpress.XtraGrid.GridControl()
        Me.ChqsGridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayTypeChq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqsPayNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqAccount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqDueDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqBank = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqOwner = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ScannedPage = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitterCtlScan = New DevExpress.XtraEditors.SplitContainerControl()
        Me.scannedFilesList = New DevExpress.XtraEditors.ListBoxControl()
        Me.scanPreview = New DevExpress.XtraEditors.PictureEdit()
        Me.AttachedPage = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitterCtlAttach = New DevExpress.XtraEditors.SplitContainerControl()
        Me.attachedFilesList = New DevExpress.XtraEditors.ListBoxControl()
        Me.attachPreview = New DevExpress.XtraEditors.PictureEdit()
        Me.FileInfoPage = New DevExpress.XtraTab.XtraTabPage()
        Me.GridAttached = New DevExpress.XtraGrid.GridControl()
        Me.ViewAttached = New DevExpress.XtraGrid.Views.Grid.GridView()
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
        Me.lblPaysDetails = New DevExpress.XtraEditors.LabelControl()
        Me.BalanceGroup = New DevExpress.XtraEditors.GroupControl()
        Me.LabelTotalDiscounts = New DevExpress.XtraEditors.LabelControl()
        Me.lblBal = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalPays = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalDisc = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalTrts = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.PatientBalanceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PatientTrtPaysBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TrtsPaysListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MainScroll = New DevExpress.XtraEditors.XtraScrollableControl()
        CType(Me.MainSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MainSplit.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainSplit.Panel1.SuspendLayout()
        CType(Me.MainSplit.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainSplit.Panel2.SuspendLayout()
        Me.MainSplit.SuspendLayout()
        CType(Me.TrtSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtSplit.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TrtSplit.Panel1.SuspendLayout()
        CType(Me.TrtSplit.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TrtSplit.Panel2.SuspendLayout()
        Me.TrtSplit.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.chkFullDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.showAsChildChck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Patient_TrtsGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Patient_TrtsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewTrts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TrtNavigator.SuspendLayout()
        CType(Me.PaysSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PaysSplit.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PaysSplit.Panel1.SuspendLayout()
        CType(Me.PaysSplit.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PaysSplit.Panel2.SuspendLayout()
        Me.PaysSplit.SuspendLayout()
        Me.SidePanel1.SuspendLayout()
        CType(Me.cboPayType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridTabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GridTabControl.SuspendLayout()
        Me.AllPaysPage.SuspendLayout()
        CType(Me.AllPaysGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Patient_PaysBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AllPaysView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CashGridPage.SuspendLayout()
        CType(Me.Patient_PaysGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewPays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ChqGridPage.SuspendLayout()
        CType(Me.ChqsPayGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChqsGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ScannedPage.SuspendLayout()
        CType(Me.SplitterCtlScan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitterCtlScan.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitterCtlScan.Panel1.SuspendLayout()
        CType(Me.SplitterCtlScan.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitterCtlScan.Panel2.SuspendLayout()
        Me.SplitterCtlScan.SuspendLayout()
        CType(Me.scannedFilesList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scanPreview.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AttachedPage.SuspendLayout()
        CType(Me.SplitterCtlAttach, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitterCtlAttach.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitterCtlAttach.Panel1.SuspendLayout()
        CType(Me.SplitterCtlAttach.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitterCtlAttach.Panel2.SuspendLayout()
        Me.SplitterCtlAttach.SuspendLayout()
        CType(Me.attachedFilesList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.attachPreview.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FileInfoPage.SuspendLayout()
        CType(Me.GridAttached, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewAttached, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PayNavigator.SuspendLayout()
        CType(Me.BalanceGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BalanceGroup.SuspendLayout()
        CType(Me.PatientBalanceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientTrtPaysBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtsPaysListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainScroll.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainSplit
        '
        resources.ApplyResources(Me.MainSplit, "MainSplit")
        Me.MainSplit.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None
        Me.MainSplit.Horizontal = False
        Me.MainSplit.Name = "MainSplit"
        '
        'MainSplit.Panel1
        '
        resources.ApplyResources(Me.MainSplit.Panel1, "MainSplit.Panel1")
        Me.MainSplit.Panel1.Controls.Add(Me.TrtSplit)
        Me.MainSplit.Panel1.Controls.Add(Me.lblTreatsDetails)
        '
        'MainSplit.Panel2
        '
        resources.ApplyResources(Me.MainSplit.Panel2, "MainSplit.Panel2")
        Me.MainSplit.Panel2.Controls.Add(Me.PaysSplit)
        Me.MainSplit.Panel2.Controls.Add(Me.lblPaysDetails)
        Me.MainSplit.SplitterPosition = 299
        '
        'TrtSplit
        '
        resources.ApplyResources(Me.TrtSplit, "TrtSplit")
        Me.TrtSplit.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None
        Me.TrtSplit.Name = "TrtSplit"
        '
        'TrtSplit.Panel1
        '
        resources.ApplyResources(Me.TrtSplit.Panel1, "TrtSplit.Panel1")
        Me.TrtSplit.Panel1.Controls.Add(Me.PanelControl1)
        Me.TrtSplit.Panel1.Controls.Add(Me.RadioFilter)
        '
        'TrtSplit.Panel2
        '
        resources.ApplyResources(Me.TrtSplit.Panel2, "TrtSplit.Panel2")
        Me.TrtSplit.Panel2.Controls.Add(Me.Patient_TrtsGridControl)
        Me.TrtSplit.Panel2.Controls.Add(Me.TrtNavigator)
        Me.TrtSplit.SplitterPosition = 409
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.chkFullDetail)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.RadioLang)
        Me.PanelControl1.Controls.Add(Me.txtWhats)
        Me.PanelControl1.Controls.Add(Me.btnPrint)
        Me.PanelControl1.Controls.Add(Me.btnInvoiceGen)
        Me.PanelControl1.Controls.Add(Me.btnAddTreat)
        Me.PanelControl1.Controls.Add(Me.btnEditTrt)
        Me.PanelControl1.Controls.Add(Me.btnResetPatientAccount)
        Me.PanelControl1.Controls.Add(Me.lblWhats)
        Me.PanelControl1.Controls.Add(Me.cboPrefix)
        Me.PanelControl1.Controls.Add(Me.btnWhatsSend)
        Me.PanelControl1.Controls.Add(Me.showAsChildChck)
        resources.ApplyResources(Me.PanelControl1, "PanelControl1")
        Me.PanelControl1.Name = "PanelControl1"
        '
        'chkFullDetail
        '
        resources.ApplyResources(Me.chkFullDetail, "chkFullDetail")
        Me.chkFullDetail.Name = "chkFullDetail"
        Me.chkFullDetail.Properties.Appearance.Font = CType(resources.GetObject("chkFullDetail.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkFullDetail.Properties.Appearance.Options.UseFont = True
        Me.chkFullDetail.Properties.Caption = resources.GetString("chkFullDetail.Properties.Caption")
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Name = "LabelControl3"
        '
        'RadioLang
        '
        Me.RadioLang.EnterMoveNextControl = True
        resources.ApplyResources(Me.RadioLang, "RadioLang")
        Me.RadioLang.Name = "RadioLang"
        Me.RadioLang.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioLang.Properties.Appearance.Font = CType(resources.GetObject("RadioLang.Properties.Appearance.Font"), System.Drawing.Font)
        Me.RadioLang.Properties.Appearance.Options.UseBackColor = True
        Me.RadioLang.Properties.Appearance.Options.UseFont = True
        Me.RadioLang.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioLang.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLang.Properties.Items"), Object), resources.GetString("RadioLang.Properties.Items1"), CType(resources.GetObject("RadioLang.Properties.Items2"), Boolean), CType(resources.GetObject("RadioLang.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLang.Properties.Items4"), Object), resources.GetString("RadioLang.Properties.Items5"))})
        '
        'txtWhats
        '
        Me.txtWhats.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtWhats, "txtWhats")
        Me.txtWhats.Name = "txtWhats"
        Me.txtWhats.Properties.Appearance.Font = CType(resources.GetObject("txtWhats.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtWhats.Properties.Appearance.Options.UseFont = True
        Me.txtWhats.Properties.MaxLength = 10
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
        'btnAddTreat
        '
        Me.btnAddTreat.Appearance.Font = CType(resources.GetObject("btnAddTreat.Appearance.Font"), System.Drawing.Font)
        Me.btnAddTreat.Appearance.Options.UseFont = True
        Me.btnAddTreat.ImageOptions.ImageKey = resources.GetString("btnAddTreat.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnAddTreat, "btnAddTreat")
        Me.btnAddTreat.Name = "btnAddTreat"
        '
        'btnEditTrt
        '
        Me.btnEditTrt.Appearance.Font = CType(resources.GetObject("btnEditTrt.Appearance.Font"), System.Drawing.Font)
        Me.btnEditTrt.Appearance.Options.UseFont = True
        Me.btnEditTrt.ImageOptions.ImageKey = resources.GetString("btnEditTrt.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnEditTrt, "btnEditTrt")
        Me.btnEditTrt.Name = "btnEditTrt"
        '
        'btnResetPatientAccount
        '
        Me.btnResetPatientAccount.Appearance.Font = CType(resources.GetObject("btnResetPatientAccount.Appearance.Font"), System.Drawing.Font)
        Me.btnResetPatientAccount.Appearance.Options.UseFont = True
        Me.btnResetPatientAccount.ImageOptions.ImageKey = resources.GetString("btnResetPatientAccount.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnResetPatientAccount, "btnResetPatientAccount")
        Me.btnResetPatientAccount.Name = "btnResetPatientAccount"
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
        'btnWhatsSend
        '
        Me.btnWhatsSend.ImageOptions.ImageKey = resources.GetString("btnWhatsSend.ImageOptions.ImageKey")
        Me.btnWhatsSend.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnWhatsSend.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.whatsapp
        Me.btnWhatsSend.ImageOptions.SvgImageSize = New System.Drawing.Size(25, 25)
        resources.ApplyResources(Me.btnWhatsSend, "btnWhatsSend")
        Me.btnWhatsSend.Name = "btnWhatsSend"
        '
        'showAsChildChck
        '
        resources.ApplyResources(Me.showAsChildChck, "showAsChildChck")
        Me.showAsChildChck.Name = "showAsChildChck"
        Me.showAsChildChck.Properties.Appearance.Font = CType(resources.GetObject("showAsChildChck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.showAsChildChck.Properties.Appearance.Options.UseFont = True
        Me.showAsChildChck.Properties.Caption = resources.GetString("showAsChildChck.Properties.Caption")
        '
        'RadioFilter
        '
        resources.ApplyResources(Me.RadioFilter, "RadioFilter")
        Me.RadioFilter.EnterMoveNextControl = True
        Me.RadioFilter.Name = "RadioFilter"
        Me.RadioFilter.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadioFilter.Properties.Appearance.Font = CType(resources.GetObject("RadioFilter.Properties.Appearance.Font"), System.Drawing.Font)
        Me.RadioFilter.Properties.Appearance.Options.UseBackColor = True
        Me.RadioFilter.Properties.Appearance.Options.UseFont = True
        Me.RadioFilter.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioFilter.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioFilter.Properties.Items"), Object), resources.GetString("RadioFilter.Properties.Items1"), CType(resources.GetObject("RadioFilter.Properties.Items2"), Boolean), CType(resources.GetObject("RadioFilter.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioFilter.Properties.Items4"), Object), resources.GetString("RadioFilter.Properties.Items5")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioFilter.Properties.Items6"), Object), resources.GetString("RadioFilter.Properties.Items7"))})
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
        Me.GridViewTrts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colTrtID, Me.colPatientID, Me.colToothTrtID, Me.colOrthoID, Me.colOtherTrtID, Me.colDetail, Me.colTrtDate, Me.colTrtValue, Me.colIsMultiTooth, Me.colDiscount, Me.colDiscount2, Me.colDiscountType})
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
        'colTrtValue
        '
        resources.ApplyResources(Me.colTrtValue, "colTrtValue")
        Me.colTrtValue.DisplayFormat.FormatString = "f0"
        Me.colTrtValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colTrtValue.FieldName = "TrtValue"
        Me.colTrtValue.ImageOptions.ImageKey = resources.GetString("colTrtValue.ImageOptions.ImageKey")
        Me.colTrtValue.Name = "colTrtValue"
        Me.colTrtValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("colTrtValue.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("colTrtValue.Summary1"), resources.GetString("colTrtValue.Summary2"))})
        Me.colTrtValue.UnboundDataType = GetType(Decimal)
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
        'colDiscount2
        '
        resources.ApplyResources(Me.colDiscount2, "colDiscount2")
        Me.colDiscount2.DisplayFormat.FormatString = "n0"
        Me.colDiscount2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colDiscount2.FieldName = "Discount2"
        Me.colDiscount2.Name = "colDiscount2"
        Me.colDiscount2.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("colDiscount2.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("colDiscount2.Summary1"), resources.GetString("colDiscount2.Summary2"))})
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
        'lblTreatsDetails
        '
        Me.lblTreatsDetails.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblTreatsDetails.Appearance.Font = CType(resources.GetObject("lblTreatsDetails.Appearance.Font"), System.Drawing.Font)
        Me.lblTreatsDetails.Appearance.Options.UseBackColor = True
        Me.lblTreatsDetails.Appearance.Options.UseFont = True
        Me.lblTreatsDetails.Appearance.Options.UseTextOptions = True
        Me.lblTreatsDetails.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblTreatsDetails.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.lblTreatsDetails, "lblTreatsDetails")
        Me.lblTreatsDetails.Name = "lblTreatsDetails"
        '
        'PaysSplit
        '
        resources.ApplyResources(Me.PaysSplit, "PaysSplit")
        Me.PaysSplit.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None
        Me.PaysSplit.Name = "PaysSplit"
        '
        'PaysSplit.Panel1
        '
        resources.ApplyResources(Me.PaysSplit.Panel1, "PaysSplit.Panel1")
        Me.PaysSplit.Panel1.Controls.Add(Me.SidePanel2)
        Me.PaysSplit.Panel1.Controls.Add(Me.SidePanel1)
        '
        'PaysSplit.Panel2
        '
        resources.ApplyResources(Me.PaysSplit.Panel2, "PaysSplit.Panel2")
        Me.PaysSplit.Panel2.Controls.Add(Me.GridTabControl)
        Me.PaysSplit.Panel2.Controls.Add(Me.PayNavigator)
        Me.PaysSplit.SplitterPosition = 409
        '
        'SidePanel2
        '
        resources.ApplyResources(Me.SidePanel2, "SidePanel2")
        Me.SidePanel2.Name = "SidePanel2"
        '
        'SidePanel1
        '
        Me.SidePanel1.Controls.Add(Me.btnBrowse)
        Me.SidePanel1.Controls.Add(Me.btnEditPay)
        Me.SidePanel1.Controls.Add(Me.btnScanScan2Pages)
        Me.SidePanel1.Controls.Add(Me.cboPayType)
        Me.SidePanel1.Controls.Add(Me.btnScan)
        Me.SidePanel1.Controls.Add(Me.btnAddPay)
        Me.SidePanel1.Controls.Add(Me.LabelControl13)
        resources.ApplyResources(Me.SidePanel1, "SidePanel1")
        Me.SidePanel1.Name = "SidePanel1"
        '
        'btnBrowse
        '
        Me.btnBrowse.Appearance.Font = CType(resources.GetObject("btnBrowse.Appearance.Font"), System.Drawing.Font)
        Me.btnBrowse.Appearance.Options.UseFont = True
        Me.btnBrowse.ImageOptions.ImageKey = resources.GetString("btnBrowse.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnBrowse, "btnBrowse")
        Me.btnBrowse.Name = "btnBrowse"
        '
        'btnEditPay
        '
        Me.btnEditPay.Appearance.Font = CType(resources.GetObject("btnEditPay.Appearance.Font"), System.Drawing.Font)
        Me.btnEditPay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnEditPay, "btnEditPay")
        Me.btnEditPay.Name = "btnEditPay"
        '
        'btnScanScan2Pages
        '
        Me.btnScanScan2Pages.Appearance.Font = CType(resources.GetObject("btnScanScan2Pages.Appearance.Font"), System.Drawing.Font)
        Me.btnScanScan2Pages.Appearance.Options.UseFont = True
        Me.btnScanScan2Pages.ImageOptions.ImageKey = resources.GetString("btnScanScan2Pages.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnScanScan2Pages, "btnScanScan2Pages")
        Me.btnScanScan2Pages.Name = "btnScanScan2Pages"
        '
        'cboPayType
        '
        resources.ApplyResources(Me.cboPayType, "cboPayType")
        Me.cboPayType.EnterMoveNextControl = True
        Me.cboPayType.Name = "cboPayType"
        Me.cboPayType.Properties.Appearance.Font = CType(resources.GetObject("cboPayType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboPayType.Properties.Appearance.Options.UseFont = True
        Me.cboPayType.Properties.AppearanceDropDown.Font = CType(resources.GetObject("cboPayType.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.cboPayType.Properties.AppearanceDropDown.Options.UseFont = True
        Me.cboPayType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPayType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboPayType.Properties.Items.AddRange(New Object() {resources.GetString("cboPayType.Properties.Items"), resources.GetString("cboPayType.Properties.Items1"), resources.GetString("cboPayType.Properties.Items2"), resources.GetString("cboPayType.Properties.Items3"), resources.GetString("cboPayType.Properties.Items4"), resources.GetString("cboPayType.Properties.Items5")})
        '
        'btnScan
        '
        Me.btnScan.Appearance.Font = CType(resources.GetObject("btnScan.Appearance.Font"), System.Drawing.Font)
        Me.btnScan.Appearance.Options.UseFont = True
        Me.btnScan.ImageOptions.ImageKey = resources.GetString("btnScan.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnScan, "btnScan")
        Me.btnScan.Name = "btnScan"
        '
        'btnAddPay
        '
        Me.btnAddPay.Appearance.Font = CType(resources.GetObject("btnAddPay.Appearance.Font"), System.Drawing.Font)
        Me.btnAddPay.Appearance.Options.UseFont = True
        Me.btnAddPay.ImageOptions.ImageKey = resources.GetString("btnAddPay.ImageOptions.ImageKey")
        resources.ApplyResources(Me.btnAddPay, "btnAddPay")
        Me.btnAddPay.Name = "btnAddPay"
        '
        'LabelControl13
        '
        Me.LabelControl13.Appearance.Font = CType(resources.GetObject("LabelControl13.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl13.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl13, "LabelControl13")
        Me.LabelControl13.Name = "LabelControl13"
        '
        'GridTabControl
        '
        Me.GridTabControl.AppearancePage.Header.Font = CType(resources.GetObject("GridTabControl.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.GridTabControl.AppearancePage.Header.Options.UseFont = True
        resources.ApplyResources(Me.GridTabControl, "GridTabControl")
        Me.GridTabControl.Name = "GridTabControl"
        Me.GridTabControl.SelectedTabPage = Me.AllPaysPage
        Me.GridTabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.AllPaysPage, Me.CashGridPage, Me.ChqGridPage, Me.ScannedPage, Me.AttachedPage, Me.FileInfoPage})
        '
        'AllPaysPage
        '
        Me.AllPaysPage.Controls.Add(Me.AllPaysGrid)
        Me.AllPaysPage.Name = "AllPaysPage"
        resources.ApplyResources(Me.AllPaysPage, "AllPaysPage")
        '
        'AllPaysGrid
        '
        Me.AllPaysGrid.DataSource = Me.Patient_PaysBindingSource
        resources.ApplyResources(Me.AllPaysGrid, "AllPaysGrid")
        Me.AllPaysGrid.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("AllPaysGrid.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.AllPaysGrid.EmbeddedNavigator.Anchor = CType(resources.GetObject("AllPaysGrid.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.AllPaysGrid.EmbeddedNavigator.Appearance.Font = CType(resources.GetObject("AllPaysGrid.EmbeddedNavigator.Appearance.Font"), System.Drawing.Font)
        Me.AllPaysGrid.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.AllPaysGrid.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("AllPaysGrid.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.AllPaysGrid.EmbeddedNavigator.Buttons.Append.Hint = resources.GetString("AllPaysGrid.EmbeddedNavigator.Buttons.Append.Hint")
        Me.AllPaysGrid.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.AllPaysGrid.EmbeddedNavigator.ImeMode = CType(resources.GetObject("AllPaysGrid.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.AllPaysGrid.EmbeddedNavigator.TextLocation = CType(resources.GetObject("AllPaysGrid.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.AllPaysGrid.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("AllPaysGrid.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.AllPaysGrid.MainView = Me.AllPaysView
        Me.AllPaysGrid.Name = "AllPaysGrid"
        Me.AllPaysGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.AllPaysView})
        '
        'Patient_PaysBindingSource
        '
        Me.Patient_PaysBindingSource.DataMember = "Patient_PaysIEnumerable"
        Me.Patient_PaysBindingSource.DataSource = Me.Patient_TrtsBindingSource
        '
        'AllPaysView
        '
        Me.AllPaysView.Appearance.FooterPanel.FontStyleDelta = CType(resources.GetObject("AllPaysView.Appearance.FooterPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.AllPaysView.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Red
        Me.AllPaysView.Appearance.FooterPanel.Options.UseFont = True
        Me.AllPaysView.Appearance.FooterPanel.Options.UseForeColor = True
        Me.AllPaysView.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.AllPaysView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AllPaysView.Appearance.HeaderPanel.Font = CType(resources.GetObject("AllPaysView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.AllPaysView.Appearance.HeaderPanel.Options.UseFont = True
        Me.AllPaysView.Appearance.Row.Font = CType(resources.GetObject("AllPaysView.Appearance.Row.Font"), System.Drawing.Font)
        Me.AllPaysView.Appearance.Row.Options.UseFont = True
        Me.AllPaysView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum3, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn13, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.GridColumn12})
        Me.AllPaysView.GridControl = Me.AllPaysGrid
        Me.AllPaysView.Name = "AllPaysView"
        Me.AllPaysView.OptionsView.EnableAppearanceEvenRow = True
        Me.AllPaysView.OptionsView.ShowErrorPanel = DevExpress.Utils.DefaultBoolean.[False]
        Me.AllPaysView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.AllPaysView.OptionsView.ShowFooter = True
        Me.AllPaysView.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.AllPaysView.OptionsView.ShowGroupPanel = False
        '
        'colRowNum3
        '
        resources.ApplyResources(Me.colRowNum3, "colRowNum3")
        Me.colRowNum3.FieldName = "colRowNum3"
        Me.colRowNum3.ImageOptions.ImageKey = resources.GetString("colRowNum3.ImageOptions.ImageKey")
        Me.colRowNum3.Name = "colRowNum3"
        Me.colRowNum3.UnboundDataType = GetType(Integer)
        '
        'GridColumn2
        '
        Me.GridColumn2.FieldName = "PayID"
        Me.GridColumn2.ImageOptions.ImageKey = resources.GetString("GridColumn2.ImageOptions.ImageKey")
        Me.GridColumn2.Name = "GridColumn2"
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "PayType"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "PatientID"
        Me.GridColumn4.ImageOptions.ImageKey = resources.GetString("GridColumn4.ImageOptions.ImageKey")
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn5
        '
        resources.ApplyResources(Me.GridColumn5, "GridColumn5")
        Me.GridColumn5.DisplayFormat.FormatString = "f0"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "PayValue"
        Me.GridColumn5.ImageOptions.ImageKey = resources.GetString("GridColumn5.ImageOptions.ImageKey")
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("GridColumn5.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("GridColumn5.Summary1"), resources.GetString("GridColumn5.Summary2"))})
        Me.GridColumn5.UnboundDataType = GetType(Double)
        '
        'GridColumn6
        '
        resources.ApplyResources(Me.GridColumn6, "GridColumn6")
        Me.GridColumn6.FieldName = "PayDate"
        Me.GridColumn6.ImageOptions.ImageKey = resources.GetString("GridColumn6.ImageOptions.ImageKey")
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("GridColumn6.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("GridColumn6.Summary1"), resources.GetString("GridColumn6.Summary2"))})
        '
        'GridColumn13
        '
        resources.ApplyResources(Me.GridColumn13, "GridColumn13")
        Me.GridColumn13.FieldName = "ReceivedBy"
        Me.GridColumn13.Name = "GridColumn13"
        '
        'GridColumn7
        '
        resources.ApplyResources(Me.GridColumn7, "GridColumn7")
        Me.GridColumn7.FieldName = "Notes"
        Me.GridColumn7.ImageOptions.ImageKey = resources.GetString("GridColumn7.ImageOptions.ImageKey")
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn8
        '
        resources.ApplyResources(Me.GridColumn8, "GridColumn8")
        Me.GridColumn8.FieldName = "ChqNumber"
        Me.GridColumn8.ImageOptions.ImageKey = resources.GetString("GridColumn8.ImageOptions.ImageKey")
        Me.GridColumn8.Name = "GridColumn8"
        '
        'GridColumn9
        '
        resources.ApplyResources(Me.GridColumn9, "GridColumn9")
        Me.GridColumn9.FieldName = "AccountNumber"
        Me.GridColumn9.ImageOptions.ImageKey = resources.GetString("GridColumn9.ImageOptions.ImageKey")
        Me.GridColumn9.Name = "GridColumn9"
        '
        'GridColumn10
        '
        resources.ApplyResources(Me.GridColumn10, "GridColumn10")
        Me.GridColumn10.FieldName = "ChqDueDate"
        Me.GridColumn10.ImageOptions.ImageKey = resources.GetString("GridColumn10.ImageOptions.ImageKey")
        Me.GridColumn10.Name = "GridColumn10"
        '
        'GridColumn11
        '
        resources.ApplyResources(Me.GridColumn11, "GridColumn11")
        Me.GridColumn11.FieldName = "ChqBank"
        Me.GridColumn11.ImageOptions.ImageKey = resources.GetString("GridColumn11.ImageOptions.ImageKey")
        Me.GridColumn11.Name = "GridColumn11"
        '
        'GridColumn12
        '
        resources.ApplyResources(Me.GridColumn12, "GridColumn12")
        Me.GridColumn12.FieldName = "ChqOwner"
        Me.GridColumn12.ImageOptions.ImageKey = resources.GetString("GridColumn12.ImageOptions.ImageKey")
        Me.GridColumn12.Name = "GridColumn12"
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
        Me.GridViewPays.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum1, Me.colPayID, Me.colTrtID1, Me.colPayType, Me.colPatientID1, Me.colPayValue, Me.colPayDate, Me.colNotes, Me.GridColumn1})
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
        'colPayType
        '
        resources.ApplyResources(Me.colPayType, "colPayType")
        Me.colPayType.FieldName = "PayType"
        Me.colPayType.Name = "colPayType"
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
        Me.colPayValue.DisplayFormat.FormatString = "f0"
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
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "ReceivedBy"
        Me.GridColumn1.Name = "GridColumn1"
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
        Me.ChqsGridView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum2, Me.colChqsPayID, Me.colPayTypeChq, Me.colChqPatientID, Me.colChqsPayValue, Me.colChqsPayDate, Me.colChqsPayNotes, Me.GridColumn14, Me.colChqNumber, Me.colChqAccount, Me.colChqDueDate, Me.colChqBank, Me.colChqOwner})
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
        'colPayTypeChq
        '
        resources.ApplyResources(Me.colPayTypeChq, "colPayTypeChq")
        Me.colPayTypeChq.FieldName = "PayType"
        Me.colPayTypeChq.Name = "colPayTypeChq"
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
        Me.colChqsPayValue.DisplayFormat.FormatString = "f0"
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
        'GridColumn14
        '
        resources.ApplyResources(Me.GridColumn14, "GridColumn14")
        Me.GridColumn14.FieldName = "ReceivedBy"
        Me.GridColumn14.Name = "GridColumn14"
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
        Me.ScannedPage.Controls.Add(Me.SplitterCtlScan)
        Me.ScannedPage.Name = "ScannedPage"
        resources.ApplyResources(Me.ScannedPage, "ScannedPage")
        '
        'SplitterCtlScan
        '
        Me.SplitterCtlScan.CaptionImageOptions.ImageKey = resources.GetString("SplitterCtlScan.CaptionImageOptions.ImageKey")
        resources.ApplyResources(Me.SplitterCtlScan, "SplitterCtlScan")
        Me.SplitterCtlScan.Name = "SplitterCtlScan"
        '
        'SplitterCtlScan.Panel1
        '
        Me.SplitterCtlScan.Panel1.Controls.Add(Me.scannedFilesList)
        resources.ApplyResources(Me.SplitterCtlScan.Panel1, "SplitterCtlScan.Panel1")
        '
        'SplitterCtlScan.Panel2
        '
        Me.SplitterCtlScan.Panel2.Controls.Add(Me.scanPreview)
        resources.ApplyResources(Me.SplitterCtlScan.Panel2, "SplitterCtlScan.Panel2")
        Me.SplitterCtlScan.SplitterPosition = 263
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
        'AttachedPage
        '
        Me.AttachedPage.Controls.Add(Me.SplitterCtlAttach)
        Me.AttachedPage.Name = "AttachedPage"
        resources.ApplyResources(Me.AttachedPage, "AttachedPage")
        '
        'SplitterCtlAttach
        '
        Me.SplitterCtlAttach.CaptionImageOptions.ImageKey = resources.GetString("SplitterCtlAttach.CaptionImageOptions.ImageKey")
        resources.ApplyResources(Me.SplitterCtlAttach, "SplitterCtlAttach")
        Me.SplitterCtlAttach.Name = "SplitterCtlAttach"
        '
        'SplitterCtlAttach.Panel1
        '
        Me.SplitterCtlAttach.Panel1.Controls.Add(Me.attachedFilesList)
        resources.ApplyResources(Me.SplitterCtlAttach.Panel1, "SplitterCtlAttach.Panel1")
        '
        'SplitterCtlAttach.Panel2
        '
        Me.SplitterCtlAttach.Panel2.Controls.Add(Me.attachPreview)
        resources.ApplyResources(Me.SplitterCtlAttach.Panel2, "SplitterCtlAttach.Panel2")
        Me.SplitterCtlAttach.SplitterPosition = 263
        '
        'attachedFilesList
        '
        Me.attachedFilesList.Appearance.Font = CType(resources.GetObject("attachedFilesList.Appearance.Font"), System.Drawing.Font)
        Me.attachedFilesList.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.attachedFilesList, "attachedFilesList")
        Me.attachedFilesList.Name = "attachedFilesList"
        '
        'attachPreview
        '
        resources.ApplyResources(Me.attachPreview, "attachPreview")
        Me.attachPreview.Name = "attachPreview"
        Me.attachPreview.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.attachPreview.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'FileInfoPage
        '
        Me.FileInfoPage.Controls.Add(Me.GridAttached)
        Me.FileInfoPage.Name = "FileInfoPage"
        resources.ApplyResources(Me.FileInfoPage, "FileInfoPage")
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
        'lblPaysDetails
        '
        Me.lblPaysDetails.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPaysDetails.Appearance.Font = CType(resources.GetObject("lblPaysDetails.Appearance.Font"), System.Drawing.Font)
        Me.lblPaysDetails.Appearance.Options.UseBackColor = True
        Me.lblPaysDetails.Appearance.Options.UseFont = True
        Me.lblPaysDetails.Appearance.Options.UseTextOptions = True
        Me.lblPaysDetails.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblPaysDetails.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.lblPaysDetails, "lblPaysDetails")
        Me.lblPaysDetails.Name = "lblPaysDetails"
        '
        'BalanceGroup
        '
        Me.BalanceGroup.AppearanceCaption.BorderColor = System.Drawing.Color.Fuchsia
        Me.BalanceGroup.AppearanceCaption.Font = CType(resources.GetObject("BalanceGroup.AppearanceCaption.Font"), System.Drawing.Font)
        Me.BalanceGroup.AppearanceCaption.ForeColor = System.Drawing.Color.Fuchsia
        Me.BalanceGroup.AppearanceCaption.Options.UseBorderColor = True
        Me.BalanceGroup.AppearanceCaption.Options.UseFont = True
        Me.BalanceGroup.AppearanceCaption.Options.UseForeColor = True
        Me.BalanceGroup.AppearanceCaption.Options.UseTextOptions = True
        Me.BalanceGroup.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BalanceGroup.Controls.Add(Me.LabelTotalDiscounts)
        Me.BalanceGroup.Controls.Add(Me.lblBal)
        Me.BalanceGroup.Controls.Add(Me.LabelControl12)
        Me.BalanceGroup.Controls.Add(Me.lblTotalPays)
        Me.BalanceGroup.Controls.Add(Me.lblTotalDisc)
        Me.BalanceGroup.Controls.Add(Me.lblTotalTrts)
        Me.BalanceGroup.Controls.Add(Me.LabelControl11)
        Me.BalanceGroup.Controls.Add(Me.LabelControl10)
        resources.ApplyResources(Me.BalanceGroup, "BalanceGroup")
        Me.BalanceGroup.Name = "BalanceGroup"
        '
        'LabelTotalDiscounts
        '
        Me.LabelTotalDiscounts.Appearance.Font = CType(resources.GetObject("LabelTotalDiscounts.Appearance.Font"), System.Drawing.Font)
        Me.LabelTotalDiscounts.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelTotalDiscounts, "LabelTotalDiscounts")
        Me.LabelTotalDiscounts.Name = "LabelTotalDiscounts"
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
        'LabelControl12
        '
        Me.LabelControl12.Appearance.Font = CType(resources.GetObject("LabelControl12.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl12.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl12, "LabelControl12")
        Me.LabelControl12.Name = "LabelControl12"
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
        'LabelControl11
        '
        Me.LabelControl11.Appearance.Font = CType(resources.GetObject("LabelControl11.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl11.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl11, "LabelControl11")
        Me.LabelControl11.Name = "LabelControl11"
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = CType(resources.GetObject("LabelControl10.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl10.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl10, "LabelControl10")
        Me.LabelControl10.Name = "LabelControl10"
        '
        'MainScroll
        '
        Me.MainScroll.Controls.Add(Me.MainSplit)
        Me.MainScroll.Controls.Add(Me.BalanceGroup)
        resources.ApplyResources(Me.MainScroll, "MainScroll")
        Me.MainScroll.Name = "MainScroll"
        '
        'NewAccounting
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MainScroll)
        Me.Name = "NewAccounting"
        CType(Me.MainSplit.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainSplit.Panel1.ResumeLayout(False)
        CType(Me.MainSplit.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainSplit.Panel2.ResumeLayout(False)
        CType(Me.MainSplit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainSplit.ResumeLayout(False)
        CType(Me.TrtSplit.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TrtSplit.Panel1.ResumeLayout(False)
        CType(Me.TrtSplit.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TrtSplit.Panel2.ResumeLayout(False)
        CType(Me.TrtSplit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TrtSplit.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.chkFullDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.showAsChildChck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Patient_TrtsGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Patient_TrtsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewTrts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TrtNavigator.ResumeLayout(False)
        Me.TrtNavigator.PerformLayout()
        CType(Me.PaysSplit.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PaysSplit.Panel1.ResumeLayout(False)
        CType(Me.PaysSplit.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PaysSplit.Panel2.ResumeLayout(False)
        CType(Me.PaysSplit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PaysSplit.ResumeLayout(False)
        Me.SidePanel1.ResumeLayout(False)
        Me.SidePanel1.PerformLayout()
        CType(Me.cboPayType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridTabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GridTabControl.ResumeLayout(False)
        Me.AllPaysPage.ResumeLayout(False)
        CType(Me.AllPaysGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Patient_PaysBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AllPaysView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CashGridPage.ResumeLayout(False)
        CType(Me.Patient_PaysGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewPays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ChqGridPage.ResumeLayout(False)
        CType(Me.ChqsPayGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChqsGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ScannedPage.ResumeLayout(False)
        CType(Me.SplitterCtlScan.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtlScan.Panel1.ResumeLayout(False)
        CType(Me.SplitterCtlScan.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtlScan.Panel2.ResumeLayout(False)
        CType(Me.SplitterCtlScan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtlScan.ResumeLayout(False)
        CType(Me.scannedFilesList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.scanPreview.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AttachedPage.ResumeLayout(False)
        CType(Me.SplitterCtlAttach.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtlAttach.Panel1.ResumeLayout(False)
        CType(Me.SplitterCtlAttach.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtlAttach.Panel2.ResumeLayout(False)
        CType(Me.SplitterCtlAttach, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitterCtlAttach.ResumeLayout(False)
        CType(Me.attachedFilesList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.attachPreview.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FileInfoPage.ResumeLayout(False)
        CType(Me.GridAttached, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewAttached, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PayNavigator.ResumeLayout(False)
        Me.PayNavigator.PerformLayout()
        CType(Me.BalanceGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BalanceGroup.ResumeLayout(False)
        Me.BalanceGroup.PerformLayout()
        CType(Me.PatientBalanceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientTrtPaysBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtsPaysListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainScroll.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BalanceGroup As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Patient_TrtsBindingSource As BindingSource
    Friend WithEvents Patient_PaysBindingSource As BindingSource
    Friend WithEvents PatientTrtPaysBindingSource As BindingSource
    Friend WithEvents PatientBalanceBindingSource As BindingSource
    Friend WithEvents TrtsPaysListBindingSource As BindingSource
    Friend WithEvents lblTotalPays As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalTrts As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalDisc As DevExpress.XtraEditors.LabelControl
    Friend WithEvents MainSplit As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TrtSplit As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents lblTreatsDetails As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PaysSplit As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents lblPaysDetails As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RadioFilter As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnWhatsSend As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents showAsChildChck As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnInvoiceGen As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnResetPatientAccount As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TrtNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel5 As ToolStripLabel
    Friend WithEvents ToolStripButton23 As ToolStripButton
    Friend WithEvents ToolStripButton24 As ToolStripButton
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox5 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents ToolStripButton25 As ToolStripButton
    Friend WithEvents ToolStripButton26 As ToolStripButton
    Friend WithEvents TrtSavNav As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnTrtDel As ToolStripButton
    Friend WithEvents Patient_TrtsGridControl As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewTrts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrtID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colToothTrtID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrthoID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOtherTrtID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrtDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrtValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIsMultiTooth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiscount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiscountType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cboPayType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnAddPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SidePanel2 As DevExpress.XtraEditors.SidePanel
    Friend WithEvents SidePanel1 As DevExpress.XtraEditors.SidePanel
    Friend WithEvents btnBrowse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnScanScan2Pages As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnScan As DevExpress.XtraEditors.SimpleButton
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
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnPayDel As ToolStripButton
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
    Friend WithEvents ScannedPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitterCtlScan As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents scannedFilesList As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents scanPreview As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents AttachedPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents btnEditTrt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddTreat As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents colDiscount2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelTotalDiscounts As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnEditPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RadioLang As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents MainScroll As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents chkFullDetail As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents colPayType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayTypeChq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AllPaysPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents AllPaysGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents AllPaysView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FileInfoPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridAttached As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewAttached As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SplitterCtlAttach As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents attachedFilesList As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents attachPreview As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
End Class
