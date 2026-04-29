<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SnapShotSender
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SnapShotSender))
        Me.splitMain = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gridJobs = New DevExpress.XtraGrid.GridControl()
        Me.viewJobs = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colJobId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colJobEnabled = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colJobTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colJobDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colJobRecipCount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colJobCaption = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.flowJobButtons = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnNewJob = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDeleteJob = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefreshJobs = New DevExpress.XtraEditors.SimpleButton()
        Me.tabRight = New DevExpress.XtraTab.XtraTabControl()
        Me.tabSetup = New DevExpress.XtraTab.XtraTabPage()
        Me.gridRecipients = New DevExpress.XtraGrid.GridControl()
        Me.viewRecipients = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRecId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRecSource = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRecPk = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRecName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRecPrefix = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRecLocal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRecActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.panelAddRecipient = New DevExpress.XtraEditors.PanelControl()
        Me.btnRemoveRecipient = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddRecipient = New DevExpress.XtraEditors.SimpleButton()
        Me.lookUpSource = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblPick = New DevExpress.XtraEditors.LabelControl()
        Me.cboSourceType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblSourceType = New DevExpress.XtraEditors.LabelControl()
        Me.panelJobFields = New DevExpress.XtraEditors.PanelControl()
        Me.btnTestSend = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSaveJob = New DevExpress.XtraEditors.SimpleButton()
        Me.memoCaption = New DevExpress.XtraEditors.MemoEdit()
        Me.lblCaption = New DevExpress.XtraEditors.LabelControl()
        Me.txtJobNotes = New DevExpress.XtraEditors.TextEdit()
        Me.lblJobNotes = New DevExpress.XtraEditors.LabelControl()
        Me.flowDays = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblDays = New DevExpress.XtraEditors.LabelControl()
        Me.timeSend = New DevExpress.XtraEditors.TimeEdit()
        Me.lblSendTime = New DevExpress.XtraEditors.LabelControl()
        Me.chkJobEnabled = New DevExpress.XtraEditors.CheckEdit()
        Me.lblSendContent = New DevExpress.XtraEditors.LabelControl()
        Me.rgSendContent = New DevExpress.XtraEditors.RadioGroup()
        Me.tabHistory = New DevExpress.XtraTab.XtraTabPage()
        Me.gridLog = New DevExpress.XtraGrid.GridControl()
        Me.viewLog = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colLogId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRunDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStarted = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCompleted = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLogRecip = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colErr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedia = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnRefreshLog = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitMain.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        CType(Me.splitMain.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        CType(Me.gridJobs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewJobs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.flowJobButtons.SuspendLayout()
        CType(Me.tabRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabRight.SuspendLayout()
        Me.tabSetup.SuspendLayout()
        CType(Me.gridRecipients, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewRecipients, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelAddRecipient, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelAddRecipient.SuspendLayout()
        CType(Me.lookUpSource.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSourceType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelJobFields, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelJobFields.SuspendLayout()
        CType(Me.memoCaption.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJobNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.timeSend.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkJobEnabled.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgSendContent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabHistory.SuspendLayout()
        CType(Me.gridLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'splitMain
        '
        resources.ApplyResources(Me.splitMain, "splitMain")
        Me.splitMain.CaptionImageOptions.ImageKey = resources.GetString("splitMain.CaptionImageOptions.ImageKey")
        Me.splitMain.Horizontal = False
        Me.splitMain.Name = "splitMain"
        '
        'splitMain.Panel1
        '
        resources.ApplyResources(Me.splitMain.Panel1, "splitMain.Panel1")
        Me.splitMain.Panel1.Controls.Add(Me.gridJobs)
        Me.splitMain.Panel1.Controls.Add(Me.flowJobButtons)
        Me.splitMain.Panel1.MinSize = 120
        '
        'splitMain.Panel2
        '
        resources.ApplyResources(Me.splitMain.Panel2, "splitMain.Panel2")
        Me.splitMain.Panel2.Controls.Add(Me.tabRight)
        Me.splitMain.Panel2.Controls.Add(Me.btnClose)
        Me.splitMain.SplitterPosition = 220
        '
        'gridJobs
        '
        resources.ApplyResources(Me.gridJobs, "gridJobs")
        Me.gridJobs.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridJobs.EmbeddedNavigator.AccessibleDescription")
        Me.gridJobs.EmbeddedNavigator.AccessibleName = resources.GetString("gridJobs.EmbeddedNavigator.AccessibleName")
        Me.gridJobs.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridJobs.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridJobs.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridJobs.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridJobs.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridJobs.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridJobs.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridJobs.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridJobs.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridJobs.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridJobs.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridJobs.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridJobs.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridJobs.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridJobs.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridJobs.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridJobs.EmbeddedNavigator.ToolTip = resources.GetString("gridJobs.EmbeddedNavigator.ToolTip")
        Me.gridJobs.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridJobs.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridJobs.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridJobs.EmbeddedNavigator.ToolTipTitle")
        Me.gridJobs.MainView = Me.viewJobs
        Me.gridJobs.Name = "gridJobs"
        Me.gridJobs.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewJobs})
        '
        'viewJobs
        '
        resources.ApplyResources(Me.viewJobs, "viewJobs")
        Me.viewJobs.Appearance.HeaderPanel.Font = CType(resources.GetObject("viewJobs.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.viewJobs.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewJobs.Appearance.Row.Font = CType(resources.GetObject("viewJobs.Appearance.Row.Font"), System.Drawing.Font)
        Me.viewJobs.Appearance.Row.Options.UseFont = True
        Me.viewJobs.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colJobId, Me.colJobEnabled, Me.colJobTime, Me.colJobDays, Me.colJobRecipCount, Me.colJobCaption})
        Me.viewJobs.GridControl = Me.gridJobs
        Me.viewJobs.Name = "viewJobs"
        Me.viewJobs.OptionsBehavior.AutoPopulateColumns = False
        Me.viewJobs.OptionsBehavior.Editable = False
        Me.viewJobs.OptionsView.ShowGroupPanel = False
        '
        'colJobId
        '
        resources.ApplyResources(Me.colJobId, "colJobId")
        Me.colJobId.FieldName = "JobId"
        Me.colJobId.ImageOptions.ImageKey = resources.GetString("colJobId.ImageOptions.ImageKey")
        Me.colJobId.Name = "colJobId"
        '
        'colJobEnabled
        '
        resources.ApplyResources(Me.colJobEnabled, "colJobEnabled")
        Me.colJobEnabled.FieldName = "IsEnabled"
        Me.colJobEnabled.ImageOptions.ImageKey = resources.GetString("colJobEnabled.ImageOptions.ImageKey")
        Me.colJobEnabled.Name = "colJobEnabled"
        '
        'colJobTime
        '
        resources.ApplyResources(Me.colJobTime, "colJobTime")
        Me.colJobTime.FieldName = "SendTimeLocal"
        Me.colJobTime.ImageOptions.ImageKey = resources.GetString("colJobTime.ImageOptions.ImageKey")
        Me.colJobTime.Name = "colJobTime"
        '
        'colJobDays
        '
        resources.ApplyResources(Me.colJobDays, "colJobDays")
        Me.colJobDays.FieldName = "DaysText"
        Me.colJobDays.ImageOptions.ImageKey = resources.GetString("colJobDays.ImageOptions.ImageKey")
        Me.colJobDays.Name = "colJobDays"
        Me.colJobDays.UnboundDataType = GetType(String)
        '
        'colJobRecipCount
        '
        resources.ApplyResources(Me.colJobRecipCount, "colJobRecipCount")
        Me.colJobRecipCount.FieldName = "ActiveRecipientCount"
        Me.colJobRecipCount.ImageOptions.ImageKey = resources.GetString("colJobRecipCount.ImageOptions.ImageKey")
        Me.colJobRecipCount.Name = "colJobRecipCount"
        '
        'colJobCaption
        '
        resources.ApplyResources(Me.colJobCaption, "colJobCaption")
        Me.colJobCaption.FieldName = "MessageCaption"
        Me.colJobCaption.ImageOptions.ImageKey = resources.GetString("colJobCaption.ImageOptions.ImageKey")
        Me.colJobCaption.Name = "colJobCaption"
        '
        'flowJobButtons
        '
        resources.ApplyResources(Me.flowJobButtons, "flowJobButtons")
        Me.flowJobButtons.Controls.Add(Me.btnNewJob)
        Me.flowJobButtons.Controls.Add(Me.btnDeleteJob)
        Me.flowJobButtons.Controls.Add(Me.btnRefreshJobs)
        Me.flowJobButtons.Name = "flowJobButtons"
        '
        'btnNewJob
        '
        resources.ApplyResources(Me.btnNewJob, "btnNewJob")
        Me.btnNewJob.Appearance.Font = CType(resources.GetObject("btnNewJob.Appearance.Font"), System.Drawing.Font)
        Me.btnNewJob.Appearance.Options.UseFont = True
        Me.btnNewJob.ImageOptions.ImageKey = resources.GetString("btnNewJob.ImageOptions.ImageKey")
        Me.btnNewJob.Name = "btnNewJob"
        '
        'btnDeleteJob
        '
        resources.ApplyResources(Me.btnDeleteJob, "btnDeleteJob")
        Me.btnDeleteJob.Appearance.Font = CType(resources.GetObject("btnDeleteJob.Appearance.Font"), System.Drawing.Font)
        Me.btnDeleteJob.Appearance.Options.UseFont = True
        Me.btnDeleteJob.ImageOptions.ImageKey = resources.GetString("btnDeleteJob.ImageOptions.ImageKey")
        Me.btnDeleteJob.Name = "btnDeleteJob"
        '
        'btnRefreshJobs
        '
        resources.ApplyResources(Me.btnRefreshJobs, "btnRefreshJobs")
        Me.btnRefreshJobs.Appearance.Font = CType(resources.GetObject("btnRefreshJobs.Appearance.Font"), System.Drawing.Font)
        Me.btnRefreshJobs.Appearance.Options.UseFont = True
        Me.btnRefreshJobs.ImageOptions.ImageKey = resources.GetString("btnRefreshJobs.ImageOptions.ImageKey")
        Me.btnRefreshJobs.Name = "btnRefreshJobs"
        '
        'tabRight
        '
        resources.ApplyResources(Me.tabRight, "tabRight")
        Me.tabRight.AppearancePage.Header.Font = CType(resources.GetObject("tabRight.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.tabRight.AppearancePage.Header.Options.UseFont = True
        Me.tabRight.Name = "tabRight"
        Me.tabRight.SelectedTabPage = Me.tabSetup
        Me.tabRight.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabSetup, Me.tabHistory})
        '
        'tabSetup
        '
        resources.ApplyResources(Me.tabSetup, "tabSetup")
        Me.tabSetup.Controls.Add(Me.gridRecipients)
        Me.tabSetup.Controls.Add(Me.panelAddRecipient)
        Me.tabSetup.Controls.Add(Me.panelJobFields)
        Me.tabSetup.Name = "tabSetup"
        '
        'gridRecipients
        '
        resources.ApplyResources(Me.gridRecipients, "gridRecipients")
        Me.gridRecipients.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridRecipients.EmbeddedNavigator.AccessibleDescription")
        Me.gridRecipients.EmbeddedNavigator.AccessibleName = resources.GetString("gridRecipients.EmbeddedNavigator.AccessibleName")
        Me.gridRecipients.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridRecipients.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridRecipients.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridRecipients.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridRecipients.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridRecipients.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridRecipients.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridRecipients.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridRecipients.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridRecipients.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridRecipients.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridRecipients.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridRecipients.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridRecipients.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridRecipients.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridRecipients.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridRecipients.EmbeddedNavigator.ToolTip = resources.GetString("gridRecipients.EmbeddedNavigator.ToolTip")
        Me.gridRecipients.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridRecipients.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridRecipients.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridRecipients.EmbeddedNavigator.ToolTipTitle")
        Me.gridRecipients.MainView = Me.viewRecipients
        Me.gridRecipients.Name = "gridRecipients"
        Me.gridRecipients.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewRecipients})
        '
        'viewRecipients
        '
        resources.ApplyResources(Me.viewRecipients, "viewRecipients")
        Me.viewRecipients.Appearance.HeaderPanel.Font = CType(resources.GetObject("viewRecipients.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.viewRecipients.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewRecipients.Appearance.Row.Font = CType(resources.GetObject("viewRecipients.Appearance.Row.Font"), System.Drawing.Font)
        Me.viewRecipients.Appearance.Row.Options.UseFont = True
        Me.viewRecipients.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRecId, Me.colRecSource, Me.colRecPk, Me.colRecName, Me.colRecPrefix, Me.colRecLocal, Me.colRecActive})
        Me.viewRecipients.GridControl = Me.gridRecipients
        Me.viewRecipients.Name = "viewRecipients"
        Me.viewRecipients.OptionsBehavior.AutoPopulateColumns = False
        Me.viewRecipients.OptionsView.ShowGroupPanel = False
        '
        'colRecId
        '
        resources.ApplyResources(Me.colRecId, "colRecId")
        Me.colRecId.FieldName = "RecipientId"
        Me.colRecId.ImageOptions.ImageKey = resources.GetString("colRecId.ImageOptions.ImageKey")
        Me.colRecId.Name = "colRecId"
        '
        'colRecSource
        '
        resources.ApplyResources(Me.colRecSource, "colRecSource")
        Me.colRecSource.FieldName = "SourceType"
        Me.colRecSource.ImageOptions.ImageKey = resources.GetString("colRecSource.ImageOptions.ImageKey")
        Me.colRecSource.Name = "colRecSource"
        '
        'colRecPk
        '
        resources.ApplyResources(Me.colRecPk, "colRecPk")
        Me.colRecPk.FieldName = "SourcePk"
        Me.colRecPk.ImageOptions.ImageKey = resources.GetString("colRecPk.ImageOptions.ImageKey")
        Me.colRecPk.Name = "colRecPk"
        '
        'colRecName
        '
        resources.ApplyResources(Me.colRecName, "colRecName")
        Me.colRecName.FieldName = "DisplayName"
        Me.colRecName.ImageOptions.ImageKey = resources.GetString("colRecName.ImageOptions.ImageKey")
        Me.colRecName.Name = "colRecName"
        '
        'colRecPrefix
        '
        resources.ApplyResources(Me.colRecPrefix, "colRecPrefix")
        Me.colRecPrefix.FieldName = "WhatsAppPrefix"
        Me.colRecPrefix.ImageOptions.ImageKey = resources.GetString("colRecPrefix.ImageOptions.ImageKey")
        Me.colRecPrefix.Name = "colRecPrefix"
        '
        'colRecLocal
        '
        resources.ApplyResources(Me.colRecLocal, "colRecLocal")
        Me.colRecLocal.FieldName = "WhatsAppLocal"
        Me.colRecLocal.ImageOptions.ImageKey = resources.GetString("colRecLocal.ImageOptions.ImageKey")
        Me.colRecLocal.Name = "colRecLocal"
        '
        'colRecActive
        '
        resources.ApplyResources(Me.colRecActive, "colRecActive")
        Me.colRecActive.FieldName = "IsActive"
        Me.colRecActive.ImageOptions.ImageKey = resources.GetString("colRecActive.ImageOptions.ImageKey")
        Me.colRecActive.Name = "colRecActive"
        '
        'panelAddRecipient
        '
        resources.ApplyResources(Me.panelAddRecipient, "panelAddRecipient")
        Me.panelAddRecipient.Controls.Add(Me.btnRemoveRecipient)
        Me.panelAddRecipient.Controls.Add(Me.btnAddRecipient)
        Me.panelAddRecipient.Controls.Add(Me.lookUpSource)
        Me.panelAddRecipient.Controls.Add(Me.lblPick)
        Me.panelAddRecipient.Controls.Add(Me.cboSourceType)
        Me.panelAddRecipient.Controls.Add(Me.lblSourceType)
        Me.panelAddRecipient.Name = "panelAddRecipient"
        '
        'btnRemoveRecipient
        '
        resources.ApplyResources(Me.btnRemoveRecipient, "btnRemoveRecipient")
        Me.btnRemoveRecipient.Appearance.Font = CType(resources.GetObject("btnRemoveRecipient.Appearance.Font"), System.Drawing.Font)
        Me.btnRemoveRecipient.Appearance.Options.UseFont = True
        Me.btnRemoveRecipient.ImageOptions.ImageKey = resources.GetString("btnRemoveRecipient.ImageOptions.ImageKey")
        Me.btnRemoveRecipient.Name = "btnRemoveRecipient"
        '
        'btnAddRecipient
        '
        resources.ApplyResources(Me.btnAddRecipient, "btnAddRecipient")
        Me.btnAddRecipient.Appearance.Font = CType(resources.GetObject("btnAddRecipient.Appearance.Font"), System.Drawing.Font)
        Me.btnAddRecipient.Appearance.Options.UseFont = True
        Me.btnAddRecipient.ImageOptions.ImageKey = resources.GetString("btnAddRecipient.ImageOptions.ImageKey")
        Me.btnAddRecipient.Name = "btnAddRecipient"
        '
        'lookUpSource
        '
        resources.ApplyResources(Me.lookUpSource, "lookUpSource")
        Me.lookUpSource.Name = "lookUpSource"
        Me.lookUpSource.Properties.Appearance.Font = CType(resources.GetObject("lookUpSource.Properties.Appearance.Font"), System.Drawing.Font)
        Me.lookUpSource.Properties.Appearance.Options.UseFont = True
        Me.lookUpSource.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("lookUpSource.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.lookUpSource.Properties.NullText = resources.GetString("lookUpSource.Properties.NullText")
        '
        'lblPick
        '
        resources.ApplyResources(Me.lblPick, "lblPick")
        Me.lblPick.Appearance.Font = CType(resources.GetObject("lblPick.Appearance.Font"), System.Drawing.Font)
        Me.lblPick.Appearance.Options.UseFont = True
        Me.lblPick.Name = "lblPick"
        '
        'cboSourceType
        '
        resources.ApplyResources(Me.cboSourceType, "cboSourceType")
        Me.cboSourceType.Name = "cboSourceType"
        Me.cboSourceType.Properties.Appearance.Font = CType(resources.GetObject("cboSourceType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboSourceType.Properties.Appearance.Options.UseFont = True
        Me.cboSourceType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboSourceType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboSourceType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'lblSourceType
        '
        resources.ApplyResources(Me.lblSourceType, "lblSourceType")
        Me.lblSourceType.Appearance.Font = CType(resources.GetObject("lblSourceType.Appearance.Font"), System.Drawing.Font)
        Me.lblSourceType.Appearance.Options.UseFont = True
        Me.lblSourceType.Name = "lblSourceType"
        '
        'panelJobFields
        '
        resources.ApplyResources(Me.panelJobFields, "panelJobFields")
        Me.panelJobFields.Controls.Add(Me.btnTestSend)
        Me.panelJobFields.Controls.Add(Me.btnSaveJob)
        Me.panelJobFields.Controls.Add(Me.rgSendContent)
        Me.panelJobFields.Controls.Add(Me.lblSendContent)
        Me.panelJobFields.Controls.Add(Me.memoCaption)
        Me.panelJobFields.Controls.Add(Me.lblCaption)
        Me.panelJobFields.Controls.Add(Me.txtJobNotes)
        Me.panelJobFields.Controls.Add(Me.lblJobNotes)
        Me.panelJobFields.Controls.Add(Me.flowDays)
        Me.panelJobFields.Controls.Add(Me.lblDays)
        Me.panelJobFields.Controls.Add(Me.timeSend)
        Me.panelJobFields.Controls.Add(Me.lblSendTime)
        Me.panelJobFields.Controls.Add(Me.chkJobEnabled)
        Me.panelJobFields.Name = "panelJobFields"
        '
        'btnTestSend
        '
        resources.ApplyResources(Me.btnTestSend, "btnTestSend")
        Me.btnTestSend.Appearance.Font = CType(resources.GetObject("btnTestSend.Appearance.Font"), System.Drawing.Font)
        Me.btnTestSend.Appearance.Options.UseFont = True
        Me.btnTestSend.ImageOptions.ImageKey = resources.GetString("btnTestSend.ImageOptions.ImageKey")
        Me.btnTestSend.Name = "btnTestSend"
        '
        'btnSaveJob
        '
        resources.ApplyResources(Me.btnSaveJob, "btnSaveJob")
        Me.btnSaveJob.Appearance.Font = CType(resources.GetObject("btnSaveJob.Appearance.Font"), System.Drawing.Font)
        Me.btnSaveJob.Appearance.Options.UseFont = True
        Me.btnSaveJob.ImageOptions.ImageKey = resources.GetString("btnSaveJob.ImageOptions.ImageKey")
        Me.btnSaveJob.Name = "btnSaveJob"
        '
        'memoCaption
        '
        resources.ApplyResources(Me.memoCaption, "memoCaption")
        Me.memoCaption.Name = "memoCaption"
        Me.memoCaption.Properties.Appearance.Font = CType(resources.GetObject("memoCaption.Properties.Appearance.Font"), System.Drawing.Font)
        Me.memoCaption.Properties.Appearance.Options.UseFont = True
        '
        'lblCaption
        '
        resources.ApplyResources(Me.lblCaption, "lblCaption")
        Me.lblCaption.Appearance.Font = CType(resources.GetObject("lblCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblCaption.Appearance.Options.UseFont = True
        Me.lblCaption.Name = "lblCaption"
        '
        'txtJobNotes
        '
        resources.ApplyResources(Me.txtJobNotes, "txtJobNotes")
        Me.txtJobNotes.Name = "txtJobNotes"
        Me.txtJobNotes.Properties.Appearance.Font = CType(resources.GetObject("txtJobNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtJobNotes.Properties.Appearance.Options.UseFont = True
        '
        'lblJobNotes
        '
        resources.ApplyResources(Me.lblJobNotes, "lblJobNotes")
        Me.lblJobNotes.Appearance.Font = CType(resources.GetObject("lblJobNotes.Appearance.Font"), System.Drawing.Font)
        Me.lblJobNotes.Appearance.Options.UseFont = True
        Me.lblJobNotes.Name = "lblJobNotes"
        '
        'flowDays
        '
        resources.ApplyResources(Me.flowDays, "flowDays")
        Me.flowDays.Name = "flowDays"
        '
        'lblDays
        '
        resources.ApplyResources(Me.lblDays, "lblDays")
        Me.lblDays.Appearance.Font = CType(resources.GetObject("lblDays.Appearance.Font"), System.Drawing.Font)
        Me.lblDays.Appearance.Options.UseFont = True
        Me.lblDays.Name = "lblDays"
        '
        'timeSend
        '
        resources.ApplyResources(Me.timeSend, "timeSend")
        Me.timeSend.Name = "timeSend"
        Me.timeSend.Properties.Appearance.Font = CType(resources.GetObject("timeSend.Properties.Appearance.Font"), System.Drawing.Font)
        Me.timeSend.Properties.Appearance.Options.UseFont = True
        Me.timeSend.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("timeSend.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblSendTime
        '
        resources.ApplyResources(Me.lblSendTime, "lblSendTime")
        Me.lblSendTime.Appearance.Font = CType(resources.GetObject("lblSendTime.Appearance.Font"), System.Drawing.Font)
        Me.lblSendTime.Appearance.Options.UseFont = True
        Me.lblSendTime.Name = "lblSendTime"
        '
        'chkJobEnabled
        '
        resources.ApplyResources(Me.chkJobEnabled, "chkJobEnabled")
        Me.chkJobEnabled.Name = "chkJobEnabled"
        Me.chkJobEnabled.Properties.Appearance.Font = CType(resources.GetObject("chkJobEnabled.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkJobEnabled.Properties.Appearance.Options.UseFont = True
        Me.chkJobEnabled.Properties.Caption = resources.GetString("chkJobEnabled.Properties.Caption")
        Me.chkJobEnabled.Properties.DisplayValueChecked = resources.GetString("chkJobEnabled.Properties.DisplayValueChecked")
        Me.chkJobEnabled.Properties.DisplayValueGrayed = resources.GetString("chkJobEnabled.Properties.DisplayValueGrayed")
        Me.chkJobEnabled.Properties.DisplayValueUnchecked = resources.GetString("chkJobEnabled.Properties.DisplayValueUnchecked")
        Me.chkJobEnabled.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkJobEnabled.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'lblSendContent
        '
        Me.lblSendContent.Appearance.Font = CType(resources.GetObject("lblCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblSendContent.Appearance.Options.UseFont = True
        Me.lblSendContent.Location = New System.Drawing.Point(14, 94)
        Me.lblSendContent.Name = "lblSendContent"
        Me.lblSendContent.Size = New System.Drawing.Size(100, 15)
        Me.lblSendContent.Text = "Send as"
        '
        'rgSendContent
        '
        Me.rgSendContent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rgSendContent.Location = New System.Drawing.Point(120, 90)
        Me.rgSendContent.Name = "rgSendContent"
        Me.rgSendContent.Properties.Appearance.Font = CType(resources.GetObject("lblCaption.Appearance.Font"), System.Drawing.Font)
        Me.rgSendContent.Properties.Appearance.Options.UseFont = True
        Me.rgSendContent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.rgSendContent.Size = New System.Drawing.Size(900, 32)
        Me.rgSendContent.TabStop = False
        '
        'tabHistory
        '
        resources.ApplyResources(Me.tabHistory, "tabHistory")
        Me.tabHistory.Controls.Add(Me.gridLog)
        Me.tabHistory.Controls.Add(Me.btnRefreshLog)
        Me.tabHistory.Name = "tabHistory"
        '
        'gridLog
        '
        resources.ApplyResources(Me.gridLog, "gridLog")
        Me.gridLog.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridLog.EmbeddedNavigator.AccessibleDescription")
        Me.gridLog.EmbeddedNavigator.AccessibleName = resources.GetString("gridLog.EmbeddedNavigator.AccessibleName")
        Me.gridLog.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridLog.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridLog.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridLog.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridLog.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridLog.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridLog.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridLog.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridLog.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridLog.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridLog.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridLog.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridLog.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridLog.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridLog.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridLog.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridLog.EmbeddedNavigator.ToolTip = resources.GetString("gridLog.EmbeddedNavigator.ToolTip")
        Me.gridLog.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridLog.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridLog.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridLog.EmbeddedNavigator.ToolTipTitle")
        Me.gridLog.MainView = Me.viewLog
        Me.gridLog.Name = "gridLog"
        Me.gridLog.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewLog})
        '
        'viewLog
        '
        resources.ApplyResources(Me.viewLog, "viewLog")
        Me.viewLog.Appearance.HeaderPanel.Font = CType(resources.GetObject("viewLog.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.viewLog.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewLog.Appearance.Row.Font = CType(resources.GetObject("viewLog.Appearance.Row.Font"), System.Drawing.Font)
        Me.viewLog.Appearance.Row.Options.UseFont = True
        Me.viewLog.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colLogId, Me.colRunDate, Me.colStatus, Me.colStarted, Me.colCompleted, Me.colLogRecip, Me.colErr, Me.colMedia})
        Me.viewLog.GridControl = Me.gridLog
        Me.viewLog.Name = "viewLog"
        Me.viewLog.OptionsBehavior.AutoPopulateColumns = False
        Me.viewLog.OptionsBehavior.Editable = False
        Me.viewLog.OptionsView.ShowGroupPanel = False
        '
        'colLogId
        '
        resources.ApplyResources(Me.colLogId, "colLogId")
        Me.colLogId.FieldName = "LogId"
        Me.colLogId.ImageOptions.ImageKey = resources.GetString("colLogId.ImageOptions.ImageKey")
        Me.colLogId.Name = "colLogId"
        '
        'colRunDate
        '
        resources.ApplyResources(Me.colRunDate, "colRunDate")
        Me.colRunDate.FieldName = "RunDate"
        Me.colRunDate.ImageOptions.ImageKey = resources.GetString("colRunDate.ImageOptions.ImageKey")
        Me.colRunDate.Name = "colRunDate"
        '
        'colStatus
        '
        resources.ApplyResources(Me.colStatus, "colStatus")
        Me.colStatus.FieldName = "Status"
        Me.colStatus.ImageOptions.ImageKey = resources.GetString("colStatus.ImageOptions.ImageKey")
        Me.colStatus.Name = "colStatus"
        '
        'colStarted
        '
        resources.ApplyResources(Me.colStarted, "colStarted")
        Me.colStarted.FieldName = "StartedAt"
        Me.colStarted.ImageOptions.ImageKey = resources.GetString("colStarted.ImageOptions.ImageKey")
        Me.colStarted.Name = "colStarted"
        '
        'colCompleted
        '
        resources.ApplyResources(Me.colCompleted, "colCompleted")
        Me.colCompleted.FieldName = "CompletedAt"
        Me.colCompleted.ImageOptions.ImageKey = resources.GetString("colCompleted.ImageOptions.ImageKey")
        Me.colCompleted.Name = "colCompleted"
        '
        'colLogRecip
        '
        resources.ApplyResources(Me.colLogRecip, "colLogRecip")
        Me.colLogRecip.FieldName = "RecipientId"
        Me.colLogRecip.ImageOptions.ImageKey = resources.GetString("colLogRecip.ImageOptions.ImageKey")
        Me.colLogRecip.Name = "colLogRecip"
        '
        'colErr
        '
        resources.ApplyResources(Me.colErr, "colErr")
        Me.colErr.FieldName = "ErrorMessage"
        Me.colErr.ImageOptions.ImageKey = resources.GetString("colErr.ImageOptions.ImageKey")
        Me.colErr.Name = "colErr"
        '
        'colMedia
        '
        resources.ApplyResources(Me.colMedia, "colMedia")
        Me.colMedia.FieldName = "MediaPath"
        Me.colMedia.ImageOptions.ImageKey = resources.GetString("colMedia.ImageOptions.ImageKey")
        Me.colMedia.Name = "colMedia"
        '
        'btnRefreshLog
        '
        resources.ApplyResources(Me.btnRefreshLog, "btnRefreshLog")
        Me.btnRefreshLog.Appearance.Font = CType(resources.GetObject("btnRefreshLog.Appearance.Font"), System.Drawing.Font)
        Me.btnRefreshLog.Appearance.Options.UseFont = True
        Me.btnRefreshLog.ImageOptions.ImageKey = resources.GetString("btnRefreshLog.ImageOptions.ImageKey")
        Me.btnRefreshLog.Name = "btnRefreshLog"
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.Appearance.Font = CType(resources.GetObject("btnClose.Appearance.Font"), System.Drawing.Font)
        Me.btnClose.Appearance.Options.UseFont = True
        Me.btnClose.ImageOptions.ImageKey = resources.GetString("btnClose.ImageOptions.ImageKey")
        Me.btnClose.Name = "btnClose"
        '
        'SnapShotSender
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.splitMain)
        Me.Name = "SnapShotSender"
        CType(Me.splitMain.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel1.PerformLayout()
        CType(Me.splitMain.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        CType(Me.gridJobs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewJobs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.flowJobButtons.ResumeLayout(False)
        CType(Me.tabRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabRight.ResumeLayout(False)
        Me.tabSetup.ResumeLayout(False)
        CType(Me.gridRecipients, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewRecipients, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelAddRecipient, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelAddRecipient.ResumeLayout(False)
        Me.panelAddRecipient.PerformLayout()
        CType(Me.lookUpSource.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSourceType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelJobFields, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelJobFields.ResumeLayout(False)
        Me.panelJobFields.PerformLayout()
        CType(Me.memoCaption.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJobNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.timeSend.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkJobEnabled.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgSendContent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabHistory.ResumeLayout(False)
        CType(Me.gridLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents splitMain As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gridJobs As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewJobs As DevExpress.XtraGrid.Views.Grid.GridView
    Friend colJobId As DevExpress.XtraGrid.Columns.GridColumn
    Friend colJobEnabled As DevExpress.XtraGrid.Columns.GridColumn
    Friend colJobTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend colJobDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend colJobRecipCount As DevExpress.XtraGrid.Columns.GridColumn
    Friend colJobCaption As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents flowJobButtons As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnNewJob As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDeleteJob As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRefreshJobs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tabRight As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabSetup As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents panelJobFields As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnTestSend As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSaveJob As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents memoCaption As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents lblCaption As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtJobNotes As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblJobNotes As DevExpress.XtraEditors.LabelControl
    Friend WithEvents flowDays As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblDays As DevExpress.XtraEditors.LabelControl
    Friend WithEvents timeSend As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents lblSendTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkJobEnabled As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblSendContent As DevExpress.XtraEditors.LabelControl
    Friend WithEvents rgSendContent As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents gridRecipients As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewRecipients As DevExpress.XtraGrid.Views.Grid.GridView
    Friend colRecId As DevExpress.XtraGrid.Columns.GridColumn
    Friend colRecSource As DevExpress.XtraGrid.Columns.GridColumn
    Friend colRecPk As DevExpress.XtraGrid.Columns.GridColumn
    Friend colRecName As DevExpress.XtraGrid.Columns.GridColumn
    Friend colRecPrefix As DevExpress.XtraGrid.Columns.GridColumn
    Friend colRecLocal As DevExpress.XtraGrid.Columns.GridColumn
    Friend colRecActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents panelAddRecipient As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnRemoveRecipient As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddRecipient As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lookUpSource As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblPick As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboSourceType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblSourceType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tabHistory As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents btnRefreshLog As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gridLog As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewLog As DevExpress.XtraGrid.Views.Grid.GridView
    Friend colLogId As DevExpress.XtraGrid.Columns.GridColumn
    Friend colRunDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend colStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend colStarted As DevExpress.XtraGrid.Columns.GridColumn
    Friend colCompleted As DevExpress.XtraGrid.Columns.GridColumn
    Friend colLogRecip As DevExpress.XtraGrid.Columns.GridColumn
    Friend colErr As DevExpress.XtraGrid.Columns.GridColumn
    Friend colMedia As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
End Class
