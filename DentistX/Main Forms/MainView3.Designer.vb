Imports DevExpress.Utils

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainView3
    Inherits DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainView3))
        Me.CultureMngr = New Infralution.Localization.CultureManager(Me.components)
        Me.ContainerA = New DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer()
        Me.MainAccordion = New DevExpress.XtraBars.Navigation.AccordionControl()
        Me.ClinicButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.TreatsButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator2 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.OrthoButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator1 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.DiagButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator3 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.AuxButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccountsButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator5 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.VisitsButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator6 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.NotesButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator7 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.RxButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator8 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.ImagesButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator4 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.AccordionControlSeparator9 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.ScheduleAdmin = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator14 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.AccordionControlSeparator10 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.AccordionControlSeparator11 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.btnCsiImage = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator15 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.PatientsButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.btnListPatients = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.btnPatientsDebts = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator12 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.DoctorsButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.RibMenuDocs = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.btnSecretaries = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.btnEmployees = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.AccordionControlSeparator13 = New DevExpress.XtraBars.Navigation.AccordionControlSeparator()
        Me.LaboratoriesButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.btnListLabs = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.btnLabOrder = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.btnRecieveOrder = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.btnLabPay = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.VendorsButton = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.btnOLdScheduler = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.FluentDesignFormControl1 = New DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl()
        Me.MainMenu = New DevExpress.XtraBars.BarSubItem()
        Me.DBaseMnu = New DevExpress.XtraBars.BarSubItem()
        Me.ListChckConnMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.ListBackupMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.ListRestoreMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.SettingsMnu = New DevExpress.XtraBars.BarSubItem()
        Me.ListSettingsMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.ListUserMngmntMnu = New DevExpress.XtraBars.BarSubItem()
        Me.ListUsersMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.ListAddNewUsrGrpMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.ListPermissionMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.ListChangePassMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.LstRestPassMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.bntLogInOUT = New DevExpress.XtraBars.BarButtonItem()
        Me.ExitMnu = New DevExpress.XtraBars.BarSubItem()
        Me.btnMnuExit = New DevExpress.XtraBars.BarButtonItem()
        Me.btnAbout = New DevExpress.XtraBars.BarButtonItem()
        Me.SkinDropDownButtonItem1 = New DevExpress.XtraBars.SkinDropDownButtonItem()
        Me.BasicDataMenu = New DevExpress.XtraBars.BarSubItem()
        Me.CitiesMnu = New DevExpress.XtraBars.BarSubItem()
        Me.ListCitiesMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.HealthMnu = New DevExpress.XtraBars.BarSubItem()
        Me.ListHealthMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.TreatsMnu = New DevExpress.XtraBars.BarSubItem()
        Me.ListTreatsMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.WireTypeMnu = New DevExpress.XtraBars.BarSubItem()
        Me.ListWireTypeMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.WireMeasureMnu = New DevExpress.XtraBars.BarSubItem()
        Me.ListWireMeasureMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.VisitTypeMnu = New DevExpress.XtraBars.BarSubItem()
        Me.ListVisitTypeMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.RxDetailsMnu = New DevExpress.XtraBars.BarSubItem()
        Me.ListRxDetailsMnu = New DevExpress.XtraBars.BarButtonItem()
        Me.BtnMedic = New DevExpress.XtraBars.BarButtonItem()
        Me.BtnDashCreate = New DevExpress.XtraBars.BarButtonItem()
        Me.BtnFinancePass = New DevExpress.XtraBars.BarButtonItem()
        Me.btnRxFly = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClinicInfo = New DevExpress.XtraBars.BarButtonItem()
        Me.UseHdrCheckItem = New DevExpress.XtraBars.BarCheckItem()
        Me.HelperBarSub = New DevExpress.XtraBars.BarSubItem()
        Me.BarWhats = New DevExpress.XtraBars.BarSubItem()
        Me.btnWhatsApp = New DevExpress.XtraBars.BarButtonItem()
        Me.btnApptSend = New DevExpress.XtraBars.BarButtonItem()
        Me.btnAccountWhats = New DevExpress.XtraBars.BarButtonItem()
        Me.btnAccountReminder = New DevExpress.XtraBars.BarButtonItem()
        Me.btnWhatsAppActivityLog = New DevExpress.XtraBars.BarButtonItem()
        Me.btnApptsReminder = New DevExpress.XtraBars.BarButtonItem()
        Me.spinShortReminder = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.btnCheuqes = New DevExpress.XtraBars.BarButtonItem()
        Me.TodayButton = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSettings = New DevExpress.XtraBars.BarButtonItem()
        Me.BtnListVendors = New DevExpress.XtraBars.BarButtonItem()
        Me.btnStaffMange = New DevExpress.XtraBars.BarButtonItem()
        Me.btnLast10Patients = New DevExpress.XtraBars.BarButtonItem()
        Me.btnScheduler = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSnapshotSender = New DevExpress.XtraBars.BarButtonItem()
        Me.ToggleSwch = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.langCombo = New DevExpress.XtraBars.BarEditItem()
        Me.langComboItems = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.FluentFormDefaultManager1 = New DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(Me.components)
        Me.RepositoryItemHypertextLabel1 = New DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.SplashScreenManager1 = New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(Global.DentistX.WaitForm1), True, True)
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.StatusBar3 = New DevExpress.XtraBars.Bar()
        Me.stLoggedUserLbl = New DevExpress.XtraBars.BarStaticItem()
        Me.stUserNameTxt = New DevExpress.XtraBars.BarHeaderItem()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.stFormNameLbl = New DevExpress.XtraBars.BarStaticItem()
        Me.stFormNameTxt = New DevExpress.XtraBars.BarHeaderItem()
        Me.stPatientNameLbl = New DevExpress.XtraBars.BarStaticItem()
        Me.stPatientNameTxt = New DevExpress.XtraBars.BarStaticItem()
        Me.stLoadingLbl = New DevExpress.XtraBars.BarHeaderItem()
        Me.chkBackup = New DevExpress.XtraBars.BarCheckItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerRight = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockMessageCenter = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel2_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.flowWhatsSessionFeed = New System.Windows.Forms.FlowLayoutPanel()
        Me.MsgCenterToolbarPanel = New DevExpress.XtraEditors.PanelControl()
        Me.btnMsgCenterCopyRow = New DevExpress.XtraEditors.SimpleButton()
        Me.btnMsgCenterOpenLog = New DevExpress.XtraEditors.SimpleButton()
        Me.btnMsgCenterClear = New DevExpress.XtraEditors.SimpleButton()
        Me.chkMsgCenterFailuresOnly = New DevExpress.XtraEditors.CheckEdit()
        Me.txtMsgCenterSearch = New System.Windows.Forms.TextBox()
        Me.lblMsgCenterTitle = New DevExpress.XtraEditors.LabelControl()
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockGlobalInfo = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel4_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.hideContainerLeft = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockColorKeys = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockCurrentPatient = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel3_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.panelCurrentPatientHost = New DevExpress.XtraEditors.PanelControl()
        Me.lblCurrentPatientEmpty = New DevExpress.XtraEditors.LabelControl()
        Me.panelCurrentPatientBody = New DevExpress.XtraEditors.PanelControl()
        Me.pnlCpPatientTable = New DevExpress.Utils.Layout.TablePanel()
        Me.grpCpPatientInfo = New DevExpress.XtraEditors.GroupControl()
        Me.flowCpGrpPatientInfo = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblCpHealth = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpName = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpMeta = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpPhone = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpWhats = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpWhatsNumber = New DevExpress.XtraEditors.LabelControl()
        Me.grpCpAppts = New DevExpress.XtraEditors.GroupControl()
        Me.flowCpGrpAppts = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblCpApptTotal = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpApptFirst = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpApptLast = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpNextAppt = New DevExpress.XtraEditors.LabelControl()
        Me.hlCpAppts = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.grpCpTreats = New DevExpress.XtraEditors.GroupControl()
        Me.flowCpGrpTreats = New System.Windows.Forms.FlowLayoutPanel()
        Me.hlCpTrtCount = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.lblCpTrtFirst = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpTrtLast = New DevExpress.XtraEditors.LabelControl()
        Me.hlCpTrtSum = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.hlCpPaySum = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.lblCpBalance = New DevExpress.XtraEditors.LabelControl()
        Me.grpCpOrtho = New DevExpress.XtraEditors.GroupControl()
        Me.flowCpGrpOrtho = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblCpOrthoStart = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpOrthoLast = New DevExpress.XtraEditors.LabelControl()
        Me.hlCpOrthoFlag = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.grpCpDiag = New DevExpress.XtraEditors.GroupControl()
        Me.flowCpGrpDiag = New System.Windows.Forms.FlowLayoutPanel()
        Me.hlCpDiagFlag = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.lblCpDiagCount = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpDiagFirst = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpDiagLast = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpDiagAgreements = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpDiagDetFirst = New DevExpress.XtraEditors.LabelControl()
        Me.lblCpDiagDetLast = New DevExpress.XtraEditors.LabelControl()
        Me.grpCpImages = New DevExpress.XtraEditors.GroupControl()
        Me.flowCpGrpImages = New System.Windows.Forms.FlowLayoutPanel()
        Me.hlCpImgBefore = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.hlCpImgDuring = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.hlCpImgAfter = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.grpCpLabOrders = New DevExpress.XtraEditors.GroupControl()
        Me.flowCpGrpLabs = New System.Windows.Forms.FlowLayoutPanel()
        Me.hlCpLabs = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.pnlCpActions = New DevExpress.XtraEditors.PanelControl()
        Me.flowCpActions = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnCpCopyPhone = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCpFocusWorkspace = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCpRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.SkinBarSubItem1 = New DevExpress.XtraBars.SkinBarSubItem()
        Me.BarHeaderItem1 = New DevExpress.XtraBars.BarHeaderItem()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RadioEn = New System.Windows.Forms.RadioButton()
        Me.RadioAr = New System.Windows.Forms.RadioButton()
        CType(Me.MainAccordion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FluentDesignFormControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.langComboItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FluentFormDefaultManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHypertextLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerRight.SuspendLayout()
        Me.DockMessageCenter.SuspendLayout()
        Me.DockPanel2_Container.SuspendLayout()
        CType(Me.MsgCenterToolbarPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MsgCenterToolbarPanel.SuspendLayout()
        CType(Me.chkMsgCenterFailuresOnly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockGlobalInfo.SuspendLayout()
        Me.hideContainerLeft.SuspendLayout()
        Me.DockColorKeys.SuspendLayout()
        Me.hideContainerTop.SuspendLayout()
        Me.DockCurrentPatient.SuspendLayout()
        Me.DockPanel3_Container.SuspendLayout()
        CType(Me.panelCurrentPatientHost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelCurrentPatientHost.SuspendLayout()
        CType(Me.panelCurrentPatientBody, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelCurrentPatientBody.SuspendLayout()
        CType(Me.pnlCpPatientTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCpPatientTable.SuspendLayout()
        CType(Me.grpCpPatientInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCpPatientInfo.SuspendLayout()
        Me.flowCpGrpPatientInfo.SuspendLayout()
        CType(Me.grpCpAppts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCpAppts.SuspendLayout()
        Me.flowCpGrpAppts.SuspendLayout()
        CType(Me.grpCpTreats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCpTreats.SuspendLayout()
        Me.flowCpGrpTreats.SuspendLayout()
        CType(Me.grpCpOrtho, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCpOrtho.SuspendLayout()
        Me.flowCpGrpOrtho.SuspendLayout()
        CType(Me.grpCpDiag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCpDiag.SuspendLayout()
        Me.flowCpGrpDiag.SuspendLayout()
        CType(Me.grpCpImages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCpImages.SuspendLayout()
        Me.flowCpGrpImages.SuspendLayout()
        CType(Me.grpCpLabOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCpLabOrders.SuspendLayout()
        Me.flowCpGrpLabs.SuspendLayout()
        CType(Me.pnlCpActions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCpActions.SuspendLayout()
        Me.flowCpActions.SuspendLayout()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CultureMngr
        '
        Me.CultureMngr.ManagedControl = Me
        '
        'ContainerA
        '
        resources.ApplyResources(Me.ContainerA, "ContainerA")
        Me.ContainerA.Name = "ContainerA"
        '
        'MainAccordion
        '
        Me.MainAccordion.DistanceBetweenRootGroups = 3
        resources.ApplyResources(Me.MainAccordion, "MainAccordion")
        Me.MainAccordion.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.ClinicButton, Me.AccordionControlSeparator3, Me.AuxButton, Me.AccordionControlSeparator10, Me.AccordionControlSeparator11, Me.btnCsiImage, Me.PatientsButton, Me.AccordionControlSeparator12, Me.DoctorsButton, Me.AccordionControlSeparator13, Me.LaboratoriesButton, Me.VendorsButton})
        Me.MainAccordion.Name = "MainAccordion"
        Me.MainAccordion.OptionsHamburgerMenu.HighlightRootElements = DevExpress.Utils.DefaultBoolean.[True]
        Me.MainAccordion.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Fluent
        Me.MainAccordion.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Always
        Me.MainAccordion.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu
        '
        'ClinicButton
        '
        Me.ClinicButton.Appearance.Default.Font = CType(resources.GetObject("ClinicButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.ClinicButton.Appearance.Default.Options.UseFont = True
        Me.ClinicButton.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.TreatsButton, Me.AccordionControlSeparator2, Me.OrthoButton, Me.AccordionControlSeparator1, Me.DiagButton})
        Me.ClinicButton.Expanded = True
        resources.ApplyResources(Me.ClinicButton, "ClinicButton")
        Me.ClinicButton.Name = "ClinicButton"
        '
        'TreatsButton
        '
        Me.TreatsButton.Appearance.Default.Font = CType(resources.GetObject("TreatsButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.TreatsButton.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.TreatsButton, "TreatsButton")
        Me.TreatsButton.Name = "TreatsButton"
        Me.TreatsButton.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'AccordionControlSeparator2
        '
        Me.AccordionControlSeparator2.Name = "AccordionControlSeparator2"
        '
        'OrthoButton
        '
        Me.OrthoButton.Appearance.Default.Font = CType(resources.GetObject("OrthoButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.OrthoButton.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.OrthoButton, "OrthoButton")
        Me.OrthoButton.Name = "OrthoButton"
        Me.OrthoButton.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'AccordionControlSeparator1
        '
        Me.AccordionControlSeparator1.Name = "AccordionControlSeparator1"
        '
        'DiagButton
        '
        Me.DiagButton.Appearance.Default.Font = CType(resources.GetObject("DiagButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.DiagButton.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.DiagButton, "DiagButton")
        Me.DiagButton.Name = "DiagButton"
        Me.DiagButton.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'AccordionControlSeparator3
        '
        Me.AccordionControlSeparator3.Name = "AccordionControlSeparator3"
        '
        'AuxButton
        '
        Me.AuxButton.Appearance.Default.Font = CType(resources.GetObject("AuxButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.AuxButton.Appearance.Default.Options.UseFont = True
        Me.AuxButton.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.AccountsButton, Me.AccordionControlSeparator5, Me.VisitsButton, Me.AccordionControlSeparator6, Me.NotesButton, Me.AccordionControlSeparator7, Me.RxButton, Me.AccordionControlSeparator8, Me.ImagesButton, Me.AccordionControlSeparator4, Me.AccordionControlSeparator9, Me.ScheduleAdmin})
        Me.AuxButton.Expanded = True
        resources.ApplyResources(Me.AuxButton, "AuxButton")
        Me.AuxButton.Name = "AuxButton"
        '
        'AccountsButton
        '
        Me.AccountsButton.Appearance.Default.Font = CType(resources.GetObject("AccountsButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.AccountsButton.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.AccountsButton, "AccountsButton")
        Me.AccountsButton.Name = "AccountsButton"
        Me.AccountsButton.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'AccordionControlSeparator5
        '
        Me.AccordionControlSeparator5.Name = "AccordionControlSeparator5"
        '
        'VisitsButton
        '
        Me.VisitsButton.Appearance.Default.Font = CType(resources.GetObject("VisitsButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.VisitsButton.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.VisitsButton, "VisitsButton")
        Me.VisitsButton.Name = "VisitsButton"
        Me.VisitsButton.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'AccordionControlSeparator6
        '
        Me.AccordionControlSeparator6.Name = "AccordionControlSeparator6"
        '
        'NotesButton
        '
        Me.NotesButton.Appearance.Default.Font = CType(resources.GetObject("NotesButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.NotesButton.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.NotesButton, "NotesButton")
        Me.NotesButton.Name = "NotesButton"
        Me.NotesButton.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'AccordionControlSeparator7
        '
        Me.AccordionControlSeparator7.Name = "AccordionControlSeparator7"
        '
        'RxButton
        '
        Me.RxButton.Appearance.Default.Font = CType(resources.GetObject("RxButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.RxButton.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.RxButton, "RxButton")
        Me.RxButton.Name = "RxButton"
        Me.RxButton.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'AccordionControlSeparator8
        '
        Me.AccordionControlSeparator8.Name = "AccordionControlSeparator8"
        '
        'ImagesButton
        '
        Me.ImagesButton.Appearance.Default.Font = CType(resources.GetObject("ImagesButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.ImagesButton.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.ImagesButton, "ImagesButton")
        Me.ImagesButton.Name = "ImagesButton"
        Me.ImagesButton.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'AccordionControlSeparator4
        '
        Me.AccordionControlSeparator4.Name = "AccordionControlSeparator4"
        '
        'AccordionControlSeparator9
        '
        Me.AccordionControlSeparator9.Name = "AccordionControlSeparator9"
        '
        'ScheduleAdmin
        '
        Me.ScheduleAdmin.Appearance.Normal.Font = CType(resources.GetObject("ScheduleAdmin.Appearance.Normal.Font"), System.Drawing.Font)
        Me.ScheduleAdmin.Appearance.Normal.Options.UseFont = True
        Me.ScheduleAdmin.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.AccordionControlSeparator14})
        resources.ApplyResources(Me.ScheduleAdmin, "ScheduleAdmin")
        Me.ScheduleAdmin.ImageOptions.SvgImage = CType(resources.GetObject("ScheduleAdmin.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.ScheduleAdmin.ImageOptions.SvgImageSize = New System.Drawing.Size(32, 32)
        Me.ScheduleAdmin.Name = "ScheduleAdmin"
        Me.ScheduleAdmin.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'AccordionControlSeparator14
        '
        Me.AccordionControlSeparator14.Name = "AccordionControlSeparator14"
        Me.AccordionControlSeparator14.Visible = False
        '
        'AccordionControlSeparator10
        '
        Me.AccordionControlSeparator10.Name = "AccordionControlSeparator10"
        '
        'AccordionControlSeparator11
        '
        Me.AccordionControlSeparator11.Name = "AccordionControlSeparator11"
        '
        'btnCsiImage
        '
        Me.btnCsiImage.Appearance.Normal.Font = CType(resources.GetObject("btnCsiImage.Appearance.Normal.Font"), System.Drawing.Font)
        Me.btnCsiImage.Appearance.Normal.Options.UseFont = True
        Me.btnCsiImage.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.AccordionControlSeparator15})
        Me.btnCsiImage.ImageOptions.Image = CType(resources.GetObject("btnCsiImage.ImageOptions.Image"), System.Drawing.Image)
        Me.btnCsiImage.Name = "btnCsiImage"
        Me.btnCsiImage.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        resources.ApplyResources(Me.btnCsiImage, "btnCsiImage")
        '
        'AccordionControlSeparator15
        '
        Me.AccordionControlSeparator15.Name = "AccordionControlSeparator15"
        Me.AccordionControlSeparator15.Visible = False
        '
        'PatientsButton
        '
        Me.PatientsButton.Appearance.Default.Font = CType(resources.GetObject("PatientsButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.PatientsButton.Appearance.Default.Options.UseFont = True
        Me.PatientsButton.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.btnListPatients, Me.btnPatientsDebts})
        Me.PatientsButton.Expanded = True
        resources.ApplyResources(Me.PatientsButton, "PatientsButton")
        Me.PatientsButton.Name = "PatientsButton"
        '
        'btnListPatients
        '
        Me.btnListPatients.Appearance.Default.Font = CType(resources.GetObject("btnListPatients.Appearance.Default.Font"), System.Drawing.Font)
        Me.btnListPatients.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.btnListPatients, "btnListPatients")
        Me.btnListPatients.Name = "btnListPatients"
        Me.btnListPatients.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'btnPatientsDebts
        '
        Me.btnPatientsDebts.Appearance.Default.Font = CType(resources.GetObject("btnPatientsDebts.Appearance.Default.Font"), System.Drawing.Font)
        Me.btnPatientsDebts.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.btnPatientsDebts, "btnPatientsDebts")
        Me.btnPatientsDebts.Name = "btnPatientsDebts"
        Me.btnPatientsDebts.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'AccordionControlSeparator12
        '
        Me.AccordionControlSeparator12.Name = "AccordionControlSeparator12"
        '
        'DoctorsButton
        '
        Me.DoctorsButton.Appearance.Default.Font = CType(resources.GetObject("DoctorsButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.DoctorsButton.Appearance.Default.Options.UseFont = True
        Me.DoctorsButton.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.RibMenuDocs, Me.btnSecretaries, Me.btnEmployees})
        Me.DoctorsButton.Expanded = True
        resources.ApplyResources(Me.DoctorsButton, "DoctorsButton")
        Me.DoctorsButton.Name = "DoctorsButton"
        '
        'RibMenuDocs
        '
        Me.RibMenuDocs.Appearance.Default.Font = CType(resources.GetObject("RibMenuDocs.Appearance.Default.Font"), System.Drawing.Font)
        Me.RibMenuDocs.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.RibMenuDocs, "RibMenuDocs")
        Me.RibMenuDocs.Name = "RibMenuDocs"
        Me.RibMenuDocs.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'btnSecretaries
        '
        Me.btnSecretaries.Appearance.Default.Font = CType(resources.GetObject("btnSecretaries.Appearance.Default.Font"), System.Drawing.Font)
        Me.btnSecretaries.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.btnSecretaries, "btnSecretaries")
        Me.btnSecretaries.ImageOptions.SvgImage = CType(resources.GetObject("btnSecretaries.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnSecretaries.ImageOptions.SvgImageSize = New System.Drawing.Size(32, 32)
        Me.btnSecretaries.Name = "btnSecretaries"
        Me.btnSecretaries.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'btnEmployees
        '
        Me.btnEmployees.Appearance.Default.Font = CType(resources.GetObject("btnEmployees.Appearance.Default.Font"), System.Drawing.Font)
        Me.btnEmployees.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.btnEmployees, "btnEmployees")
        Me.btnEmployees.ImageOptions.SvgImage = CType(resources.GetObject("btnEmployees.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnEmployees.ImageOptions.SvgImageSize = New System.Drawing.Size(32, 32)
        Me.btnEmployees.Name = "btnEmployees"
        Me.btnEmployees.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'AccordionControlSeparator13
        '
        Me.AccordionControlSeparator13.Name = "AccordionControlSeparator13"
        '
        'LaboratoriesButton
        '
        Me.LaboratoriesButton.Appearance.Default.Font = CType(resources.GetObject("LaboratoriesButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.LaboratoriesButton.Appearance.Default.Options.UseFont = True
        Me.LaboratoriesButton.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.btnListLabs, Me.btnLabOrder, Me.btnRecieveOrder, Me.btnLabPay})
        Me.LaboratoriesButton.Expanded = True
        resources.ApplyResources(Me.LaboratoriesButton, "LaboratoriesButton")
        Me.LaboratoriesButton.Name = "LaboratoriesButton"
        '
        'btnListLabs
        '
        Me.btnListLabs.Appearance.Default.Font = CType(resources.GetObject("btnListLabs.Appearance.Default.Font"), System.Drawing.Font)
        Me.btnListLabs.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.btnListLabs, "btnListLabs")
        Me.btnListLabs.Name = "btnListLabs"
        Me.btnListLabs.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'btnLabOrder
        '
        Me.btnLabOrder.Appearance.Default.Font = CType(resources.GetObject("btnLabOrder.Appearance.Default.Font"), System.Drawing.Font)
        Me.btnLabOrder.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.btnLabOrder, "btnLabOrder")
        Me.btnLabOrder.Name = "btnLabOrder"
        Me.btnLabOrder.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'btnRecieveOrder
        '
        Me.btnRecieveOrder.Appearance.Default.Font = CType(resources.GetObject("btnRecieveOrder.Appearance.Default.Font"), System.Drawing.Font)
        Me.btnRecieveOrder.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.btnRecieveOrder, "btnRecieveOrder")
        Me.btnRecieveOrder.Name = "btnRecieveOrder"
        Me.btnRecieveOrder.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'btnLabPay
        '
        Me.btnLabPay.Appearance.Default.Font = CType(resources.GetObject("btnLabPay.Appearance.Default.Font"), System.Drawing.Font)
        Me.btnLabPay.Appearance.Default.Options.UseFont = True
        resources.ApplyResources(Me.btnLabPay, "btnLabPay")
        Me.btnLabPay.Name = "btnLabPay"
        Me.btnLabPay.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        '
        'VendorsButton
        '
        Me.VendorsButton.Appearance.Default.Font = CType(resources.GetObject("VendorsButton.Appearance.Default.Font"), System.Drawing.Font)
        Me.VendorsButton.Appearance.Default.Options.UseFont = True
        Me.VendorsButton.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.btnOLdScheduler})
        Me.VendorsButton.Expanded = True
        resources.ApplyResources(Me.VendorsButton, "VendorsButton")
        Me.VendorsButton.Name = "VendorsButton"
        '
        'btnOLdScheduler
        '
        Me.btnOLdScheduler.Name = "btnOLdScheduler"
        Me.btnOLdScheduler.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item
        resources.ApplyResources(Me.btnOLdScheduler, "btnOLdScheduler")
        '
        'FluentDesignFormControl1
        '
        Me.FluentDesignFormControl1.FluentDesignForm = Me
        Me.FluentDesignFormControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MainMenu, Me.DBaseMnu, Me.ListChckConnMnu, Me.ListBackupMnu, Me.SkinDropDownButtonItem1, Me.ListRestoreMnu, Me.SettingsMnu, Me.ListSettingsMnu, Me.ExitMnu, Me.btnMnuExit, Me.BasicDataMenu, Me.CitiesMnu, Me.HealthMnu, Me.TreatsMnu, Me.WireTypeMnu, Me.WireMeasureMnu, Me.VisitTypeMnu, Me.RxDetailsMnu, Me.ListCitiesMnu, Me.ListHealthMnu, Me.ListTreatsMnu, Me.ListWireTypeMnu, Me.ListWireMeasureMnu, Me.ListVisitTypeMnu, Me.ListRxDetailsMnu, Me.ListUserMngmntMnu, Me.ListUsersMnu, Me.ListAddNewUsrGrpMnu, Me.ListPermissionMnu, Me.ListChangePassMnu, Me.LstRestPassMnu, Me.bntLogInOUT, Me.UseHdrCheckItem, Me.BtnDashCreate, Me.BtnFinancePass, Me.btnAbout, Me.BtnMedic, Me.btnRxFly, Me.btnClinicInfo, Me.HelperBarSub, Me.btnCheuqes, Me.TodayButton, Me.btnSettings, Me.BtnListVendors, Me.btnWhatsApp, Me.btnApptSend, Me.btnAccountWhats, Me.btnAccountReminder, Me.btnWhatsAppActivityLog, Me.BarWhats, Me.btnStaffMange, Me.btnApptsReminder, Me.btnLast10Patients, Me.spinShortReminder, Me.btnScheduler, Me.ToggleSwch, Me.langCombo, Me.btnSnapshotSender})
        resources.ApplyResources(Me.FluentDesignFormControl1, "FluentDesignFormControl1")
        Me.FluentDesignFormControl1.Manager = Me.FluentFormDefaultManager1
        Me.FluentDesignFormControl1.Name = "FluentDesignFormControl1"
        Me.FluentDesignFormControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemHypertextLabel1, Me.RepositoryItemSpinEdit1, Me.RepositoryItemTextEdit1, Me.langComboItems})
        Me.FluentDesignFormControl1.TabStop = False
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.MainMenu)
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.SkinDropDownButtonItem1)
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.BasicDataMenu)
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.UseHdrCheckItem)
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.HelperBarSub)
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.ToggleSwch)
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.langCombo)
        '
        'MainMenu
        '
        Me.MainMenu.Border = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        resources.ApplyResources(Me.MainMenu, "MainMenu")
        Me.MainMenu.Id = 0
        Me.MainMenu.ItemAppearance.Normal.Font = CType(resources.GetObject("MainMenu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MainMenu.ItemAppearance.Normal.Options.UseFont = True
        Me.MainMenu.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MainMenu.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MainMenu.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MainMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.DBaseMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.SettingsMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.ListUserMngmntMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.bntLogInOUT), New DevExpress.XtraBars.LinkPersistInfo(Me.ExitMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.btnAbout)})
        Me.MainMenu.MenuAppearance.HeaderItemAppearance.Font = CType(resources.GetObject("MainMenu.MenuAppearance.HeaderItemAppearance.Font"), System.Drawing.Font)
        Me.MainMenu.MenuAppearance.HeaderItemAppearance.Options.UseFont = True
        Me.MainMenu.MenuAppearance.MenuBar.Font = CType(resources.GetObject("MainMenu.MenuAppearance.MenuBar.Font"), System.Drawing.Font)
        Me.MainMenu.MenuAppearance.MenuBar.Options.UseFont = True
        Me.MainMenu.MenuAppearance.MenuCaption.Font = CType(resources.GetObject("MainMenu.MenuAppearance.MenuCaption.Font"), System.Drawing.Font)
        Me.MainMenu.MenuAppearance.MenuCaption.Options.UseFont = True
        Me.MainMenu.MenuAppearance.SideStrip.Font = CType(resources.GetObject("MainMenu.MenuAppearance.SideStrip.Font"), System.Drawing.Font)
        Me.MainMenu.MenuAppearance.SideStrip.Options.UseFont = True
        Me.MainMenu.Name = "MainMenu"
        '
        'DBaseMnu
        '
        resources.ApplyResources(Me.DBaseMnu, "DBaseMnu")
        Me.DBaseMnu.Id = 1
        Me.DBaseMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("DBaseMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.DBaseMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.DBaseMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("DBaseMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.DBaseMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.DBaseMnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ListChckConnMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.ListBackupMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.ListRestoreMnu)})
        Me.DBaseMnu.Name = "DBaseMnu"
        '
        'ListChckConnMnu
        '
        resources.ApplyResources(Me.ListChckConnMnu, "ListChckConnMnu")
        Me.ListChckConnMnu.Id = 2
        Me.ListChckConnMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListChckConnMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListChckConnMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListChckConnMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListChckConnMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListChckConnMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListChckConnMnu.Name = "ListChckConnMnu"
        '
        'ListBackupMnu
        '
        resources.ApplyResources(Me.ListBackupMnu, "ListBackupMnu")
        Me.ListBackupMnu.Id = 3
        Me.ListBackupMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListBackupMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListBackupMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListBackupMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListBackupMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListBackupMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListBackupMnu.Name = "ListBackupMnu"
        '
        'ListRestoreMnu
        '
        resources.ApplyResources(Me.ListRestoreMnu, "ListRestoreMnu")
        Me.ListRestoreMnu.Id = 8
        Me.ListRestoreMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListRestoreMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListRestoreMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListRestoreMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListRestoreMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListRestoreMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListRestoreMnu.Name = "ListRestoreMnu"
        '
        'SettingsMnu
        '
        resources.ApplyResources(Me.SettingsMnu, "SettingsMnu")
        Me.SettingsMnu.Id = 9
        Me.SettingsMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("SettingsMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.SettingsMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.SettingsMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("SettingsMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.SettingsMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.SettingsMnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ListSettingsMnu)})
        Me.SettingsMnu.Name = "SettingsMnu"
        '
        'ListSettingsMnu
        '
        resources.ApplyResources(Me.ListSettingsMnu, "ListSettingsMnu")
        Me.ListSettingsMnu.Id = 10
        Me.ListSettingsMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListSettingsMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListSettingsMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListSettingsMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListSettingsMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListSettingsMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListSettingsMnu.Name = "ListSettingsMnu"
        '
        'ListUserMngmntMnu
        '
        resources.ApplyResources(Me.ListUserMngmntMnu, "ListUserMngmntMnu")
        Me.ListUserMngmntMnu.Id = 32
        Me.ListUserMngmntMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListUserMngmntMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListUserMngmntMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListUserMngmntMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListUserMngmntMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListUserMngmntMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListUserMngmntMnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ListUsersMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.ListAddNewUsrGrpMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.ListPermissionMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.ListChangePassMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.LstRestPassMnu)})
        Me.ListUserMngmntMnu.MenuAppearance.AppearanceMenu.Normal.Font = CType(resources.GetObject("ListUserMngmntMnu.MenuAppearance.AppearanceMenu.Normal.Font"), System.Drawing.Font)
        Me.ListUserMngmntMnu.MenuAppearance.AppearanceMenu.Normal.Options.UseFont = True
        Me.ListUserMngmntMnu.MenuAppearance.HeaderItemAppearance.Font = CType(resources.GetObject("ListUserMngmntMnu.MenuAppearance.HeaderItemAppearance.Font"), System.Drawing.Font)
        Me.ListUserMngmntMnu.MenuAppearance.HeaderItemAppearance.Options.UseFont = True
        Me.ListUserMngmntMnu.Name = "ListUserMngmntMnu"
        '
        'ListUsersMnu
        '
        resources.ApplyResources(Me.ListUsersMnu, "ListUsersMnu")
        Me.ListUsersMnu.Id = 33
        Me.ListUsersMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListUsersMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListUsersMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListUsersMnu.Name = "ListUsersMnu"
        '
        'ListAddNewUsrGrpMnu
        '
        resources.ApplyResources(Me.ListAddNewUsrGrpMnu, "ListAddNewUsrGrpMnu")
        Me.ListAddNewUsrGrpMnu.Id = 34
        Me.ListAddNewUsrGrpMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListAddNewUsrGrpMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListAddNewUsrGrpMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListAddNewUsrGrpMnu.Name = "ListAddNewUsrGrpMnu"
        '
        'ListPermissionMnu
        '
        resources.ApplyResources(Me.ListPermissionMnu, "ListPermissionMnu")
        Me.ListPermissionMnu.Id = 35
        Me.ListPermissionMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListPermissionMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListPermissionMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListPermissionMnu.Name = "ListPermissionMnu"
        '
        'ListChangePassMnu
        '
        resources.ApplyResources(Me.ListChangePassMnu, "ListChangePassMnu")
        Me.ListChangePassMnu.Id = 36
        Me.ListChangePassMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListChangePassMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListChangePassMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListChangePassMnu.Name = "ListChangePassMnu"
        '
        'LstRestPassMnu
        '
        resources.ApplyResources(Me.LstRestPassMnu, "LstRestPassMnu")
        Me.LstRestPassMnu.Id = 37
        Me.LstRestPassMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("LstRestPassMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.LstRestPassMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.LstRestPassMnu.Name = "LstRestPassMnu"
        '
        'bntLogInOUT
        '
        resources.ApplyResources(Me.bntLogInOUT, "bntLogInOUT")
        Me.bntLogInOUT.Id = 38
        Me.bntLogInOUT.ItemAppearance.Normal.Font = CType(resources.GetObject("bntLogInOUT.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bntLogInOUT.ItemAppearance.Normal.Options.UseFont = True
        Me.bntLogInOUT.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("bntLogInOUT.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.bntLogInOUT.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.bntLogInOUT.Name = "bntLogInOUT"
        '
        'ExitMnu
        '
        resources.ApplyResources(Me.ExitMnu, "ExitMnu")
        Me.ExitMnu.Id = 11
        Me.ExitMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ExitMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ExitMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ExitMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ExitMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ExitMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ExitMnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnMnuExit)})
        Me.ExitMnu.Name = "ExitMnu"
        '
        'btnMnuExit
        '
        resources.ApplyResources(Me.btnMnuExit, "btnMnuExit")
        Me.btnMnuExit.Id = 12
        Me.btnMnuExit.ItemAppearance.Normal.Font = CType(resources.GetObject("btnMnuExit.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnMnuExit.ItemAppearance.Normal.Options.UseFont = True
        Me.btnMnuExit.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("btnMnuExit.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnMnuExit.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.btnMnuExit.Name = "btnMnuExit"
        '
        'btnAbout
        '
        resources.ApplyResources(Me.btnAbout, "btnAbout")
        Me.btnAbout.Id = 41
        Me.btnAbout.ItemAppearance.Normal.Font = CType(resources.GetObject("btnAbout.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnAbout.ItemAppearance.Normal.Options.UseFont = True
        Me.btnAbout.Name = "btnAbout"
        '
        'SkinDropDownButtonItem1
        '
        Me.SkinDropDownButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.SkinDropDownButtonItem1.Id = 6
        Me.SkinDropDownButtonItem1.Name = "SkinDropDownButtonItem1"
        '
        'BasicDataMenu
        '
        Me.BasicDataMenu.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        resources.ApplyResources(Me.BasicDataMenu, "BasicDataMenu")
        Me.BasicDataMenu.Id = 14
        Me.BasicDataMenu.ItemAppearance.Normal.Font = CType(resources.GetObject("BasicDataMenu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BasicDataMenu.ItemAppearance.Normal.Options.UseFont = True
        Me.BasicDataMenu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BasicDataMenu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BasicDataMenu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BasicDataMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CitiesMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.HealthMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.TreatsMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.WireTypeMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.WireMeasureMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.VisitTypeMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.RxDetailsMnu), New DevExpress.XtraBars.LinkPersistInfo(Me.BtnMedic), New DevExpress.XtraBars.LinkPersistInfo(Me.BtnDashCreate), New DevExpress.XtraBars.LinkPersistInfo(Me.BtnFinancePass), New DevExpress.XtraBars.LinkPersistInfo(Me.btnRxFly), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClinicInfo)})
        Me.BasicDataMenu.Name = "BasicDataMenu"
        Me.BasicDataMenu.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.[True]
        '
        'CitiesMnu
        '
        resources.ApplyResources(Me.CitiesMnu, "CitiesMnu")
        Me.CitiesMnu.Id = 16
        Me.CitiesMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("CitiesMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.CitiesMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.CitiesMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("CitiesMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.CitiesMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.CitiesMnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ListCitiesMnu)})
        Me.CitiesMnu.Name = "CitiesMnu"
        '
        'ListCitiesMnu
        '
        resources.ApplyResources(Me.ListCitiesMnu, "ListCitiesMnu")
        Me.ListCitiesMnu.Id = 24
        Me.ListCitiesMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListCitiesMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListCitiesMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListCitiesMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListCitiesMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListCitiesMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListCitiesMnu.Name = "ListCitiesMnu"
        '
        'HealthMnu
        '
        resources.ApplyResources(Me.HealthMnu, "HealthMnu")
        Me.HealthMnu.Id = 17
        Me.HealthMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("HealthMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.HealthMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.HealthMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("HealthMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.HealthMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.HealthMnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ListHealthMnu)})
        Me.HealthMnu.Name = "HealthMnu"
        '
        'ListHealthMnu
        '
        resources.ApplyResources(Me.ListHealthMnu, "ListHealthMnu")
        Me.ListHealthMnu.Id = 25
        Me.ListHealthMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListHealthMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListHealthMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListHealthMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListHealthMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListHealthMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListHealthMnu.Name = "ListHealthMnu"
        '
        'TreatsMnu
        '
        resources.ApplyResources(Me.TreatsMnu, "TreatsMnu")
        Me.TreatsMnu.Id = 18
        Me.TreatsMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("TreatsMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.TreatsMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.TreatsMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("TreatsMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.TreatsMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.TreatsMnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ListTreatsMnu)})
        Me.TreatsMnu.Name = "TreatsMnu"
        '
        'ListTreatsMnu
        '
        resources.ApplyResources(Me.ListTreatsMnu, "ListTreatsMnu")
        Me.ListTreatsMnu.Id = 26
        Me.ListTreatsMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListTreatsMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListTreatsMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListTreatsMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListTreatsMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListTreatsMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListTreatsMnu.Name = "ListTreatsMnu"
        '
        'WireTypeMnu
        '
        resources.ApplyResources(Me.WireTypeMnu, "WireTypeMnu")
        Me.WireTypeMnu.Id = 19
        Me.WireTypeMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("WireTypeMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.WireTypeMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.WireTypeMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("WireTypeMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.WireTypeMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.WireTypeMnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ListWireTypeMnu)})
        Me.WireTypeMnu.Name = "WireTypeMnu"
        '
        'ListWireTypeMnu
        '
        resources.ApplyResources(Me.ListWireTypeMnu, "ListWireTypeMnu")
        Me.ListWireTypeMnu.Id = 27
        Me.ListWireTypeMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListWireTypeMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListWireTypeMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListWireTypeMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListWireTypeMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListWireTypeMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListWireTypeMnu.Name = "ListWireTypeMnu"
        '
        'WireMeasureMnu
        '
        resources.ApplyResources(Me.WireMeasureMnu, "WireMeasureMnu")
        Me.WireMeasureMnu.Id = 20
        Me.WireMeasureMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("WireMeasureMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.WireMeasureMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.WireMeasureMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("WireMeasureMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.WireMeasureMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.WireMeasureMnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ListWireMeasureMnu)})
        Me.WireMeasureMnu.Name = "WireMeasureMnu"
        '
        'ListWireMeasureMnu
        '
        resources.ApplyResources(Me.ListWireMeasureMnu, "ListWireMeasureMnu")
        Me.ListWireMeasureMnu.Id = 28
        Me.ListWireMeasureMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListWireMeasureMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListWireMeasureMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListWireMeasureMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListWireMeasureMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListWireMeasureMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListWireMeasureMnu.Name = "ListWireMeasureMnu"
        '
        'VisitTypeMnu
        '
        resources.ApplyResources(Me.VisitTypeMnu, "VisitTypeMnu")
        Me.VisitTypeMnu.Id = 21
        Me.VisitTypeMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("VisitTypeMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.VisitTypeMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.VisitTypeMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("VisitTypeMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.VisitTypeMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.VisitTypeMnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ListVisitTypeMnu)})
        Me.VisitTypeMnu.Name = "VisitTypeMnu"
        '
        'ListVisitTypeMnu
        '
        resources.ApplyResources(Me.ListVisitTypeMnu, "ListVisitTypeMnu")
        Me.ListVisitTypeMnu.Id = 29
        Me.ListVisitTypeMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListVisitTypeMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListVisitTypeMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListVisitTypeMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListVisitTypeMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListVisitTypeMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListVisitTypeMnu.Name = "ListVisitTypeMnu"
        '
        'RxDetailsMnu
        '
        resources.ApplyResources(Me.RxDetailsMnu, "RxDetailsMnu")
        Me.RxDetailsMnu.Id = 22
        Me.RxDetailsMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("RxDetailsMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.RxDetailsMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.RxDetailsMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("RxDetailsMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.RxDetailsMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.RxDetailsMnu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ListRxDetailsMnu)})
        Me.RxDetailsMnu.Name = "RxDetailsMnu"
        '
        'ListRxDetailsMnu
        '
        resources.ApplyResources(Me.ListRxDetailsMnu, "ListRxDetailsMnu")
        Me.ListRxDetailsMnu.Id = 30
        Me.ListRxDetailsMnu.ItemAppearance.Normal.Font = CType(resources.GetObject("ListRxDetailsMnu.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListRxDetailsMnu.ItemAppearance.Normal.Options.UseFont = True
        Me.ListRxDetailsMnu.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ListRxDetailsMnu.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ListRxDetailsMnu.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ListRxDetailsMnu.Name = "ListRxDetailsMnu"
        '
        'BtnMedic
        '
        resources.ApplyResources(Me.BtnMedic, "BtnMedic")
        Me.BtnMedic.Id = 42
        Me.BtnMedic.ItemAppearance.Normal.Font = CType(resources.GetObject("BtnMedic.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BtnMedic.ItemAppearance.Normal.Options.UseFont = True
        Me.BtnMedic.Name = "BtnMedic"
        '
        'BtnDashCreate
        '
        resources.ApplyResources(Me.BtnDashCreate, "BtnDashCreate")
        Me.BtnDashCreate.Id = 40
        Me.BtnDashCreate.ItemAppearance.Normal.Font = CType(resources.GetObject("BtnDashCreate.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BtnDashCreate.ItemAppearance.Normal.Options.UseFont = True
        Me.BtnDashCreate.Name = "BtnDashCreate"
        '
        'BtnFinancePass
        '
        resources.ApplyResources(Me.BtnFinancePass, "BtnFinancePass")
        Me.BtnFinancePass.Id = 60
        Me.BtnFinancePass.ItemAppearance.Normal.Font = CType(resources.GetObject("BtnFinancePass.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BtnFinancePass.ItemAppearance.Normal.Options.UseFont = True
        Me.BtnFinancePass.Name = "BtnFinancePass"
        '
        'btnRxFly
        '
        resources.ApplyResources(Me.btnRxFly, "btnRxFly")
        Me.btnRxFly.Id = 43
        Me.btnRxFly.ItemAppearance.Normal.Font = CType(resources.GetObject("btnRxFly.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnRxFly.ItemAppearance.Normal.Options.UseFont = True
        Me.btnRxFly.Name = "btnRxFly"
        '
        'btnClinicInfo
        '
        resources.ApplyResources(Me.btnClinicInfo, "btnClinicInfo")
        Me.btnClinicInfo.Id = 44
        Me.btnClinicInfo.ItemAppearance.Normal.Font = CType(resources.GetObject("btnClinicInfo.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnClinicInfo.ItemAppearance.Normal.Options.UseFont = True
        Me.btnClinicInfo.Name = "btnClinicInfo"
        '
        'UseHdrCheckItem
        '
        Me.UseHdrCheckItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.UseHdrCheckItem.BindableChecked = True
        resources.ApplyResources(Me.UseHdrCheckItem, "UseHdrCheckItem")
        Me.UseHdrCheckItem.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.AfterText
        Me.UseHdrCheckItem.Checked = True
        Me.UseHdrCheckItem.Id = 39
        Me.UseHdrCheckItem.ItemAppearance.Normal.Font = CType(resources.GetObject("UseHdrCheckItem.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.UseHdrCheckItem.ItemAppearance.Normal.Options.UseFont = True
        Me.UseHdrCheckItem.Name = "UseHdrCheckItem"
        '
        'HelperBarSub
        '
        resources.ApplyResources(Me.HelperBarSub, "HelperBarSub")
        Me.HelperBarSub.Id = 45
        Me.HelperBarSub.ItemAppearance.Normal.Font = CType(resources.GetObject("HelperBarSub.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.HelperBarSub.ItemAppearance.Normal.Options.UseFont = True
        Me.HelperBarSub.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarWhats), New DevExpress.XtraBars.LinkPersistInfo(Me.btnCheuqes), New DevExpress.XtraBars.LinkPersistInfo(Me.TodayButton), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSettings), New DevExpress.XtraBars.LinkPersistInfo(Me.BtnListVendors), New DevExpress.XtraBars.LinkPersistInfo(Me.btnStaffMange), New DevExpress.XtraBars.LinkPersistInfo(Me.btnLast10Patients), New DevExpress.XtraBars.LinkPersistInfo(Me.btnScheduler), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSnapshotSender)})
        Me.HelperBarSub.Name = "HelperBarSub"
        '
        'BarWhats
        '
        resources.ApplyResources(Me.BarWhats, "BarWhats")
        Me.BarWhats.Id = 54
        Me.BarWhats.ItemAppearance.Normal.Font = CType(resources.GetObject("BarWhats.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarWhats.ItemAppearance.Normal.Options.UseFont = True
        Me.BarWhats.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnWhatsApp), New DevExpress.XtraBars.LinkPersistInfo(Me.btnApptSend), New DevExpress.XtraBars.LinkPersistInfo(Me.btnAccountWhats), New DevExpress.XtraBars.LinkPersistInfo(Me.btnAccountReminder), New DevExpress.XtraBars.LinkPersistInfo(Me.btnWhatsAppActivityLog), New DevExpress.XtraBars.LinkPersistInfo(Me.btnApptsReminder), New DevExpress.XtraBars.LinkPersistInfo(Me.spinShortReminder)})
        Me.BarWhats.Name = "BarWhats"
        '
        'btnWhatsApp
        '
        resources.ApplyResources(Me.btnWhatsApp, "btnWhatsApp")
        Me.btnWhatsApp.Id = 50
        Me.btnWhatsApp.ItemAppearance.Normal.Font = CType(resources.GetObject("btnWhatsApp.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnWhatsApp.ItemAppearance.Normal.Options.UseFont = True
        Me.btnWhatsApp.Name = "btnWhatsApp"
        '
        'btnApptSend
        '
        resources.ApplyResources(Me.btnApptSend, "btnApptSend")
        Me.btnApptSend.Id = 51
        Me.btnApptSend.ItemAppearance.Normal.Font = CType(resources.GetObject("btnApptSend.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnApptSend.ItemAppearance.Normal.Options.UseFont = True
        Me.btnApptSend.Name = "btnApptSend"
        '
        'btnAccountWhats
        '
        resources.ApplyResources(Me.btnAccountWhats, "btnAccountWhats")
        Me.btnAccountWhats.Id = 52
        Me.btnAccountWhats.ItemAppearance.Normal.Font = CType(resources.GetObject("btnAccountWhats.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnAccountWhats.ItemAppearance.Normal.Options.UseFont = True
        Me.btnAccountWhats.Name = "btnAccountWhats"
        '
        'btnAccountReminder
        '
        resources.ApplyResources(Me.btnAccountReminder, "btnAccountReminder")
        Me.btnAccountReminder.Id = 53
        Me.btnAccountReminder.ItemAppearance.Normal.Font = CType(resources.GetObject("btnAccountReminder.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnAccountReminder.ItemAppearance.Normal.Options.UseFont = True
        Me.btnAccountReminder.Name = "btnAccountReminder"
        '
        'btnWhatsAppActivityLog
        '
        resources.ApplyResources(Me.btnWhatsAppActivityLog, "btnWhatsAppActivityLog")
        Me.btnWhatsAppActivityLog.Id = 56
        Me.btnWhatsAppActivityLog.ItemAppearance.Normal.Font = CType(resources.GetObject("btnWhatsAppActivityLog.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnWhatsAppActivityLog.ItemAppearance.Normal.Options.UseFont = True
        Me.btnWhatsAppActivityLog.Name = "btnWhatsAppActivityLog"
        '
        'btnApptsReminder
        '
        resources.ApplyResources(Me.btnApptsReminder, "btnApptsReminder")
        Me.btnApptsReminder.Id = 57
        Me.btnApptsReminder.ItemAppearance.Normal.Font = CType(resources.GetObject("btnApptsReminder.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnApptsReminder.ItemAppearance.Normal.Options.UseFont = True
        Me.btnApptsReminder.Name = "btnApptsReminder"
        '
        'spinShortReminder
        '
        resources.ApplyResources(Me.spinShortReminder, "spinShortReminder")
        Me.spinShortReminder.Edit = Me.RepositoryItemSpinEdit1
        Me.spinShortReminder.EditValue = "3"
        Me.spinShortReminder.Id = 61
        Me.spinShortReminder.ItemAppearance.Normal.Font = CType(resources.GetObject("spinShortReminder.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.spinShortReminder.ItemAppearance.Normal.Options.UseFont = True
        Me.spinShortReminder.Name = "spinShortReminder"
        '
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.Appearance.Font = CType(resources.GetObject("RepositoryItemSpinEdit1.Appearance.Font"), System.Drawing.Font)
        Me.RepositoryItemSpinEdit1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.RepositoryItemSpinEdit1, "RepositoryItemSpinEdit1")
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemSpinEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemSpinEdit1.IsFloatValue = False
        Me.RepositoryItemSpinEdit1.MaskSettings.Set("mask", "d")
        Me.RepositoryItemSpinEdit1.MaxValue = New Decimal(New Integer() {72, 0, 0, 0})
        Me.RepositoryItemSpinEdit1.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        '
        'btnCheuqes
        '
        resources.ApplyResources(Me.btnCheuqes, "btnCheuqes")
        Me.btnCheuqes.Id = 46
        Me.btnCheuqes.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.Cheuqe1
        Me.btnCheuqes.ItemAppearance.Normal.Font = CType(resources.GetObject("btnCheuqes.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnCheuqes.ItemAppearance.Normal.Options.UseFont = True
        Me.btnCheuqes.Name = "btnCheuqes"
        '
        'TodayButton
        '
        resources.ApplyResources(Me.TodayButton, "TodayButton")
        Me.TodayButton.Id = 47
        Me.TodayButton.ItemAppearance.Normal.Font = CType(resources.GetObject("TodayButton.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.TodayButton.ItemAppearance.Normal.Options.UseFont = True
        Me.TodayButton.Name = "TodayButton"
        '
        'btnSettings
        '
        resources.ApplyResources(Me.btnSettings, "btnSettings")
        Me.btnSettings.Id = 48
        Me.btnSettings.ItemAppearance.Normal.Font = CType(resources.GetObject("btnSettings.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnSettings.ItemAppearance.Normal.Options.UseFont = True
        Me.btnSettings.Name = "btnSettings"
        '
        'BtnListVendors
        '
        resources.ApplyResources(Me.BtnListVendors, "BtnListVendors")
        Me.BtnListVendors.Id = 49
        Me.BtnListVendors.ItemAppearance.Normal.Font = CType(resources.GetObject("BtnListVendors.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BtnListVendors.ItemAppearance.Normal.Options.UseFont = True
        Me.BtnListVendors.Name = "BtnListVendors"
        '
        'btnStaffMange
        '
        resources.ApplyResources(Me.btnStaffMange, "btnStaffMange")
        Me.btnStaffMange.Id = 55
        Me.btnStaffMange.ItemAppearance.Normal.Font = CType(resources.GetObject("btnStaffMange.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnStaffMange.ItemAppearance.Normal.Options.UseFont = True
        Me.btnStaffMange.Name = "btnStaffMange"
        '
        'btnLast10Patients
        '
        resources.ApplyResources(Me.btnLast10Patients, "btnLast10Patients")
        Me.btnLast10Patients.Id = 58
        Me.btnLast10Patients.ItemAppearance.Normal.Font = CType(resources.GetObject("btnLast10Patients.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnLast10Patients.ItemAppearance.Normal.Options.UseFont = True
        Me.btnLast10Patients.Name = "btnLast10Patients"
        '
        'btnScheduler
        '
        resources.ApplyResources(Me.btnScheduler, "btnScheduler")
        Me.btnScheduler.Id = 62
        Me.btnScheduler.ItemAppearance.Normal.Font = CType(resources.GetObject("btnScheduler.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnScheduler.ItemAppearance.Normal.Options.UseFont = True
        Me.btnScheduler.Name = "btnScheduler"
        '
        'btnSnapshotSender
        '
        resources.ApplyResources(Me.btnSnapshotSender, "btnSnapshotSender")
        Me.btnSnapshotSender.Id = 67
        Me.btnSnapshotSender.ItemAppearance.Normal.Font = CType(resources.GetObject("btnSnapshotSender.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnSnapshotSender.ItemAppearance.Normal.Options.UseFont = True
        Me.btnSnapshotSender.Name = "btnSnapshotSender"
        '
        'ToggleSwch
        '
        Me.ToggleSwch.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        resources.ApplyResources(Me.ToggleSwch, "ToggleSwch")
        Me.ToggleSwch.Id = 63
        Me.ToggleSwch.ItemAppearance.Normal.Font = CType(resources.GetObject("ToggleSwch.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.ToggleSwch.ItemAppearance.Normal.Options.UseFont = True
        Me.ToggleSwch.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("ToggleSwch.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.ToggleSwch.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.ToggleSwch.Name = "ToggleSwch"
        '
        'langCombo
        '
        Me.langCombo.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        resources.ApplyResources(Me.langCombo, "langCombo")
        Me.langCombo.Edit = Me.langComboItems
        Me.langCombo.Id = 66
        Me.langCombo.Name = "langCombo"
        '
        'langComboItems
        '
        resources.ApplyResources(Me.langComboItems, "langComboItems")
        Me.langComboItems.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("langComboItems.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.langComboItems.Name = "langComboItems"
        '
        'FluentFormDefaultManager1
        '
        Me.FluentFormDefaultManager1.DockWindowTabFont = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FluentFormDefaultManager1.Form = Me
        Me.FluentFormDefaultManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MainMenu, Me.DBaseMnu, Me.ListChckConnMnu, Me.ListBackupMnu, Me.SkinDropDownButtonItem1, Me.ListRestoreMnu, Me.SettingsMnu, Me.ListSettingsMnu, Me.ExitMnu, Me.btnMnuExit, Me.BasicDataMenu, Me.CitiesMnu, Me.HealthMnu, Me.TreatsMnu, Me.WireTypeMnu, Me.WireMeasureMnu, Me.VisitTypeMnu, Me.RxDetailsMnu, Me.ListCitiesMnu, Me.ListHealthMnu, Me.ListTreatsMnu, Me.ListWireTypeMnu, Me.ListWireMeasureMnu, Me.ListVisitTypeMnu, Me.ListRxDetailsMnu, Me.ListUserMngmntMnu, Me.ListUsersMnu, Me.ListAddNewUsrGrpMnu, Me.ListPermissionMnu, Me.ListChangePassMnu, Me.LstRestPassMnu, Me.bntLogInOUT, Me.UseHdrCheckItem, Me.BtnDashCreate, Me.BtnFinancePass, Me.btnAbout, Me.BtnMedic, Me.btnRxFly, Me.btnClinicInfo, Me.HelperBarSub, Me.btnCheuqes, Me.TodayButton, Me.btnSettings, Me.BtnListVendors, Me.btnWhatsApp, Me.btnApptSend, Me.btnAccountWhats, Me.btnAccountReminder, Me.btnWhatsAppActivityLog, Me.BarWhats, Me.btnStaffMange, Me.btnApptsReminder, Me.btnLast10Patients, Me.spinShortReminder, Me.btnScheduler, Me.ToggleSwch, Me.langCombo, Me.btnSnapshotSender})
        Me.FluentFormDefaultManager1.MaxItemId = 68
        Me.FluentFormDefaultManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemHypertextLabel1, Me.RepositoryItemSpinEdit1, Me.RepositoryItemTextEdit1, Me.langComboItems})
        Me.FluentFormDefaultManager1.ShowFullMenus = True
        '
        'RepositoryItemHypertextLabel1
        '
        Me.RepositoryItemHypertextLabel1.Name = "RepositoryItemHypertextLabel1"
        '
        'RepositoryItemTextEdit1
        '
        resources.ApplyResources(Me.RepositoryItemTextEdit1, "RepositoryItemTextEdit1")
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'SplashScreenManager1
        '
        Me.SplashScreenManager1.ClosingDelay = 500
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.StatusBar3})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.DockManager = Me.DockManager1
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.SkinBarSubItem1, Me.BarHeaderItem1, Me.stLoggedUserLbl, Me.stUserNameTxt, Me.stFormNameLbl, Me.stFormNameTxt, Me.BarStaticItem1, Me.stPatientNameLbl, Me.stPatientNameTxt, Me.stLoadingLbl, Me.chkBackup})
        Me.BarManager1.MaxItemId = 17
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPictureEdit1})
        Me.BarManager1.StatusBar = Me.StatusBar3
        '
        'StatusBar3
        '
        Me.StatusBar3.BarName = "Status bar"
        Me.StatusBar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.StatusBar3.DockCol = 0
        Me.StatusBar3.DockRow = 0
        Me.StatusBar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.StatusBar3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.stLoggedUserLbl), New DevExpress.XtraBars.LinkPersistInfo(Me.stUserNameTxt), New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.stFormNameLbl), New DevExpress.XtraBars.LinkPersistInfo(Me.stFormNameTxt), New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.stPatientNameLbl), New DevExpress.XtraBars.LinkPersistInfo(Me.stPatientNameTxt), New DevExpress.XtraBars.LinkPersistInfo(Me.stLoadingLbl), New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.chkBackup)})
        Me.StatusBar3.OptionsBar.AllowQuickCustomization = False
        Me.StatusBar3.OptionsBar.DrawDragBorder = False
        Me.StatusBar3.OptionsBar.UseWholeRow = True
        resources.ApplyResources(Me.StatusBar3, "StatusBar3")
        '
        'stLoggedUserLbl
        '
        resources.ApplyResources(Me.stLoggedUserLbl, "stLoggedUserLbl")
        Me.stLoggedUserLbl.Id = 8
        Me.stLoggedUserLbl.ItemAppearance.Normal.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.stLoggedUserLbl.ItemAppearance.Normal.Font = CType(resources.GetObject("stLoggedUserLbl.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.stLoggedUserLbl.ItemAppearance.Normal.Options.UseBackColor = True
        Me.stLoggedUserLbl.ItemAppearance.Normal.Options.UseFont = True
        Me.stLoggedUserLbl.Name = "stLoggedUserLbl"
        '
        'stUserNameTxt
        '
        Me.stUserNameTxt.Appearance.Font = CType(resources.GetObject("stUserNameTxt.Appearance.Font"), System.Drawing.Font)
        Me.stUserNameTxt.Appearance.Options.UseFont = True
        Me.stUserNameTxt.Id = 9
        Me.stUserNameTxt.Name = "stUserNameTxt"
        '
        'BarStaticItem1
        '
        resources.ApplyResources(Me.BarStaticItem1, "BarStaticItem1")
        Me.BarStaticItem1.Id = 12
        Me.BarStaticItem1.Name = "BarStaticItem1"
        '
        'stFormNameLbl
        '
        resources.ApplyResources(Me.stFormNameLbl, "stFormNameLbl")
        Me.stFormNameLbl.Id = 10
        Me.stFormNameLbl.ItemAppearance.Normal.BackColor = System.Drawing.Color.Aquamarine
        Me.stFormNameLbl.ItemAppearance.Normal.Font = CType(resources.GetObject("stFormNameLbl.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.stFormNameLbl.ItemAppearance.Normal.Options.UseBackColor = True
        Me.stFormNameLbl.ItemAppearance.Normal.Options.UseFont = True
        Me.stFormNameLbl.Name = "stFormNameLbl"
        '
        'stFormNameTxt
        '
        Me.stFormNameTxt.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.stFormNameTxt.Appearance.Font = CType(resources.GetObject("stFormNameTxt.Appearance.Font"), System.Drawing.Font)
        Me.stFormNameTxt.Appearance.Options.UseBackColor = True
        Me.stFormNameTxt.Appearance.Options.UseFont = True
        Me.stFormNameTxt.Id = 11
        Me.stFormNameTxt.Name = "stFormNameTxt"
        '
        'stPatientNameLbl
        '
        resources.ApplyResources(Me.stPatientNameLbl, "stPatientNameLbl")
        Me.stPatientNameLbl.Id = 13
        Me.stPatientNameLbl.ItemAppearance.Normal.BackColor = System.Drawing.Color.LightSkyBlue
        Me.stPatientNameLbl.ItemAppearance.Normal.Font = CType(resources.GetObject("stPatientNameLbl.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.stPatientNameLbl.ItemAppearance.Normal.Options.UseBackColor = True
        Me.stPatientNameLbl.ItemAppearance.Normal.Options.UseFont = True
        Me.stPatientNameLbl.Name = "stPatientNameLbl"
        '
        'stPatientNameTxt
        '
        Me.stPatientNameTxt.Id = 14
        Me.stPatientNameTxt.ItemAppearance.Normal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.stPatientNameTxt.ItemAppearance.Normal.Font = CType(resources.GetObject("stPatientNameTxt.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.stPatientNameTxt.ItemAppearance.Normal.Options.UseBackColor = True
        Me.stPatientNameTxt.ItemAppearance.Normal.Options.UseFont = True
        Me.stPatientNameTxt.Name = "stPatientNameTxt"
        '
        'stLoadingLbl
        '
        Me.stLoadingLbl.Appearance.Font = CType(resources.GetObject("stLoadingLbl.Appearance.Font"), System.Drawing.Font)
        Me.stLoadingLbl.Appearance.ForeColor = System.Drawing.Color.Red
        Me.stLoadingLbl.Appearance.Options.UseFont = True
        Me.stLoadingLbl.Appearance.Options.UseForeColor = True
        Me.stLoadingLbl.ContentHorizontalAlignment = DevExpress.XtraBars.BarItemContentAlignment.Center
        Me.stLoadingLbl.Id = 15
        Me.stLoadingLbl.Name = "stLoadingLbl"
        '
        'chkBackup
        '
        resources.ApplyResources(Me.chkBackup, "chkBackup")
        Me.chkBackup.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.AfterText
        Me.chkBackup.Id = 16
        Me.chkBackup.ItemAppearance.Normal.BackColor = System.Drawing.Color.Yellow
        Me.chkBackup.ItemAppearance.Normal.Font = CType(resources.GetObject("chkBackup.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.chkBackup.ItemAppearance.Normal.Options.UseBackColor = True
        Me.chkBackup.ItemAppearance.Normal.Options.UseFont = True
        Me.chkBackup.Name = "chkBackup"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        resources.ApplyResources(Me.barDockControlTop, "barDockControlTop")
        Me.barDockControlTop.Manager = Me.BarManager1
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        resources.ApplyResources(Me.barDockControlBottom, "barDockControlBottom")
        Me.barDockControlBottom.Manager = Me.BarManager1
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        resources.ApplyResources(Me.barDockControlLeft, "barDockControlLeft")
        Me.barDockControlLeft.Manager = Me.BarManager1
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        resources.ApplyResources(Me.barDockControlRight, "barDockControlRight")
        Me.barDockControlRight.Manager = Me.BarManager1
        '
        'DockManager1
        '
        Me.DockManager1.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerRight, Me.hideContainerBottom, Me.hideContainerLeft, Me.hideContainerTop})
        Me.DockManager1.DockingOptions.AllowDockToCenter = DevExpress.Utils.DefaultBoolean.[False]
        Me.DockManager1.DockingOptions.CursorFloatCanceled = System.Windows.Forms.Cursors.No
        Me.DockManager1.DockingOptions.FloatOnDblClick = False
        Me.DockManager1.DockingOptions.ShowCloseButton = False
        Me.DockManager1.DockingOptions.ShowMaximizeButton = False
        Me.DockManager1.DockingOptions.ShowMinimizeButton = False
        Me.DockManager1.Form = Me
        Me.DockManager1.MenuManager = Me.BarManager1
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane", "DevExpress.XtraBars.TabFormControl", "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl", "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"})
        '
        'hideContainerRight
        '
        Me.hideContainerRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.hideContainerRight.Controls.Add(Me.DockMessageCenter)
        resources.ApplyResources(Me.hideContainerRight, "hideContainerRight")
        Me.hideContainerRight.Name = "hideContainerRight"
        '
        'DockMessageCenter
        '
        Me.DockMessageCenter.Controls.Add(Me.DockPanel2_Container)
        Me.DockMessageCenter.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right
        Me.DockMessageCenter.ID = New System.Guid("b2f5f7fe-339f-449d-b67c-10ea65ed3b3c")
        resources.ApplyResources(Me.DockMessageCenter, "DockMessageCenter")
        Me.DockMessageCenter.Name = "DockMessageCenter"
        Me.DockMessageCenter.OriginalSize = New System.Drawing.Size(254, 200)
        Me.DockMessageCenter.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right
        Me.DockMessageCenter.SavedIndex = 0
        Me.DockMessageCenter.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel2_Container
        '
        Me.DockPanel2_Container.Controls.Add(Me.flowWhatsSessionFeed)
        Me.DockPanel2_Container.Controls.Add(Me.MsgCenterToolbarPanel)
        resources.ApplyResources(Me.DockPanel2_Container, "DockPanel2_Container")
        Me.DockPanel2_Container.Name = "DockPanel2_Container"
        '
        'flowWhatsSessionFeed
        '
        resources.ApplyResources(Me.flowWhatsSessionFeed, "flowWhatsSessionFeed")
        Me.flowWhatsSessionFeed.BackColor = System.Drawing.SystemColors.Window
        Me.flowWhatsSessionFeed.Name = "flowWhatsSessionFeed"
        '
        'MsgCenterToolbarPanel
        '
        Me.MsgCenterToolbarPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.MsgCenterToolbarPanel.Controls.Add(Me.btnMsgCenterCopyRow)
        Me.MsgCenterToolbarPanel.Controls.Add(Me.btnMsgCenterOpenLog)
        Me.MsgCenterToolbarPanel.Controls.Add(Me.btnMsgCenterClear)
        Me.MsgCenterToolbarPanel.Controls.Add(Me.chkMsgCenterFailuresOnly)
        Me.MsgCenterToolbarPanel.Controls.Add(Me.txtMsgCenterSearch)
        Me.MsgCenterToolbarPanel.Controls.Add(Me.lblMsgCenterTitle)
        resources.ApplyResources(Me.MsgCenterToolbarPanel, "MsgCenterToolbarPanel")
        Me.MsgCenterToolbarPanel.Name = "MsgCenterToolbarPanel"
        '
        'btnMsgCenterCopyRow
        '
        Me.btnMsgCenterCopyRow.Appearance.Font = CType(resources.GetObject("btnMsgCenterCopyRow.Appearance.Font"), System.Drawing.Font)
        Me.btnMsgCenterCopyRow.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnMsgCenterCopyRow, "btnMsgCenterCopyRow")
        Me.btnMsgCenterCopyRow.Name = "btnMsgCenterCopyRow"
        '
        'btnMsgCenterOpenLog
        '
        Me.btnMsgCenterOpenLog.Appearance.Font = CType(resources.GetObject("btnMsgCenterOpenLog.Appearance.Font"), System.Drawing.Font)
        Me.btnMsgCenterOpenLog.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnMsgCenterOpenLog, "btnMsgCenterOpenLog")
        Me.btnMsgCenterOpenLog.Name = "btnMsgCenterOpenLog"
        '
        'btnMsgCenterClear
        '
        Me.btnMsgCenterClear.Appearance.Font = CType(resources.GetObject("btnMsgCenterClear.Appearance.Font"), System.Drawing.Font)
        Me.btnMsgCenterClear.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnMsgCenterClear, "btnMsgCenterClear")
        Me.btnMsgCenterClear.Name = "btnMsgCenterClear"
        '
        'chkMsgCenterFailuresOnly
        '
        resources.ApplyResources(Me.chkMsgCenterFailuresOnly, "chkMsgCenterFailuresOnly")
        Me.chkMsgCenterFailuresOnly.Name = "chkMsgCenterFailuresOnly"
        Me.chkMsgCenterFailuresOnly.Properties.Appearance.Font = CType(resources.GetObject("chkMsgCenterFailuresOnly.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkMsgCenterFailuresOnly.Properties.Appearance.Options.UseFont = True
        Me.chkMsgCenterFailuresOnly.Properties.Caption = resources.GetString("chkMsgCenterFailuresOnly.Properties.Caption")
        '
        'txtMsgCenterSearch
        '
        resources.ApplyResources(Me.txtMsgCenterSearch, "txtMsgCenterSearch")
        Me.txtMsgCenterSearch.Name = "txtMsgCenterSearch"
        '
        'lblMsgCenterTitle
        '
        Me.lblMsgCenterTitle.Appearance.Font = CType(resources.GetObject("lblMsgCenterTitle.Appearance.Font"), System.Drawing.Font)
        Me.lblMsgCenterTitle.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblMsgCenterTitle, "lblMsgCenterTitle")
        Me.lblMsgCenterTitle.Name = "lblMsgCenterTitle"
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockGlobalInfo)
        resources.ApplyResources(Me.hideContainerBottom, "hideContainerBottom")
        Me.hideContainerBottom.Name = "hideContainerBottom"
        '
        'DockGlobalInfo
        '
        Me.DockGlobalInfo.Controls.Add(Me.DockPanel4_Container)
        Me.DockGlobalInfo.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockGlobalInfo.ID = New System.Guid("6d035935-569b-415a-a5c0-d65852c1ae14")
        resources.ApplyResources(Me.DockGlobalInfo, "DockGlobalInfo")
        Me.DockGlobalInfo.Name = "DockGlobalInfo"
        Me.DockGlobalInfo.OriginalSize = New System.Drawing.Size(200, 200)
        Me.DockGlobalInfo.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockGlobalInfo.SavedIndex = 2
        Me.DockGlobalInfo.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel4_Container
        '
        resources.ApplyResources(Me.DockPanel4_Container, "DockPanel4_Container")
        Me.DockPanel4_Container.Name = "DockPanel4_Container"
        '
        'hideContainerLeft
        '
        Me.hideContainerLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.hideContainerLeft.Controls.Add(Me.DockColorKeys)
        resources.ApplyResources(Me.hideContainerLeft, "hideContainerLeft")
        Me.hideContainerLeft.Name = "hideContainerLeft"
        '
        'DockColorKeys
        '
        Me.DockColorKeys.Controls.Add(Me.DockPanel1_Container)
        Me.DockColorKeys.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.DockColorKeys.ID = New System.Guid("9934a43e-1e2f-4c69-95c0-e6626b8a95bd")
        resources.ApplyResources(Me.DockColorKeys, "DockColorKeys")
        Me.DockColorKeys.Name = "DockColorKeys"
        Me.DockColorKeys.OriginalSize = New System.Drawing.Size(200, 200)
        Me.DockColorKeys.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.DockColorKeys.SavedIndex = 0
        Me.DockColorKeys.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        resources.ApplyResources(Me.DockPanel1_Container, "DockPanel1_Container")
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.hideContainerTop.Controls.Add(Me.DockCurrentPatient)
        resources.ApplyResources(Me.hideContainerTop, "hideContainerTop")
        Me.hideContainerTop.Name = "hideContainerTop"
        '
        'DockCurrentPatient
        '
        Me.DockCurrentPatient.Controls.Add(Me.DockPanel3_Container)
        Me.DockCurrentPatient.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.DockCurrentPatient.ID = New System.Guid("38d5fe6a-3342-4462-b3ec-324cbf32ea60")
        resources.ApplyResources(Me.DockCurrentPatient, "DockCurrentPatient")
        Me.DockCurrentPatient.Name = "DockCurrentPatient"
        Me.DockCurrentPatient.OriginalSize = New System.Drawing.Size(200, 284)
        Me.DockCurrentPatient.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.DockCurrentPatient.SavedIndex = 0
        Me.DockCurrentPatient.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel3_Container
        '
        Me.DockPanel3_Container.Controls.Add(Me.panelCurrentPatientHost)
        resources.ApplyResources(Me.DockPanel3_Container, "DockPanel3_Container")
        Me.DockPanel3_Container.Name = "DockPanel3_Container"
        '
        'panelCurrentPatientHost
        '
        Me.panelCurrentPatientHost.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.panelCurrentPatientHost.Controls.Add(Me.lblCurrentPatientEmpty)
        Me.panelCurrentPatientHost.Controls.Add(Me.panelCurrentPatientBody)
        resources.ApplyResources(Me.panelCurrentPatientHost, "panelCurrentPatientHost")
        Me.panelCurrentPatientHost.Name = "panelCurrentPatientHost"
        '
        'lblCurrentPatientEmpty
        '
        Me.lblCurrentPatientEmpty.Appearance.Font = CType(resources.GetObject("lblCurrentPatientEmpty.Appearance.Font"), System.Drawing.Font)
        Me.lblCurrentPatientEmpty.Appearance.Options.UseFont = True
        Me.lblCurrentPatientEmpty.Appearance.Options.UseTextOptions = True
        Me.lblCurrentPatientEmpty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblCurrentPatientEmpty, "lblCurrentPatientEmpty")
        Me.lblCurrentPatientEmpty.Name = "lblCurrentPatientEmpty"
        '
        'panelCurrentPatientBody
        '
        Me.panelCurrentPatientBody.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.panelCurrentPatientBody.Controls.Add(Me.pnlCpPatientTable)
        Me.panelCurrentPatientBody.Controls.Add(Me.pnlCpActions)
        resources.ApplyResources(Me.panelCurrentPatientBody, "panelCurrentPatientBody")
        Me.panelCurrentPatientBody.Name = "panelCurrentPatientBody"
        '
        'pnlCpPatientTable
        '
        Me.pnlCpPatientTable.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 60.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25.0!)})
        Me.pnlCpPatientTable.Controls.Add(Me.grpCpPatientInfo)
        Me.pnlCpPatientTable.Controls.Add(Me.grpCpAppts)
        Me.pnlCpPatientTable.Controls.Add(Me.grpCpTreats)
        Me.pnlCpPatientTable.Controls.Add(Me.grpCpOrtho)
        Me.pnlCpPatientTable.Controls.Add(Me.grpCpDiag)
        Me.pnlCpPatientTable.Controls.Add(Me.grpCpImages)
        Me.pnlCpPatientTable.Controls.Add(Me.grpCpLabOrders)
        resources.ApplyResources(Me.pnlCpPatientTable, "pnlCpPatientTable")
        Me.pnlCpPatientTable.Name = "pnlCpPatientTable"
        Me.pnlCpPatientTable.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.pnlCpPatientTable.UseSkinIndents = True
        '
        'grpCpPatientInfo
        '
        Me.grpCpPatientInfo.AppearanceCaption.Font = CType(resources.GetObject("grpCpPatientInfo.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpCpPatientInfo.AppearanceCaption.Options.UseFont = True
        Me.pnlCpPatientTable.SetColumn(Me.grpCpPatientInfo, 0)
        Me.grpCpPatientInfo.Controls.Add(Me.flowCpGrpPatientInfo)
        resources.ApplyResources(Me.grpCpPatientInfo, "grpCpPatientInfo")
        Me.grpCpPatientInfo.Name = "grpCpPatientInfo"
        Me.pnlCpPatientTable.SetRow(Me.grpCpPatientInfo, 0)
        '
        'flowCpGrpPatientInfo
        '
        resources.ApplyResources(Me.flowCpGrpPatientInfo, "flowCpGrpPatientInfo")
        Me.flowCpGrpPatientInfo.Controls.Add(Me.lblCpHealth)
        Me.flowCpGrpPatientInfo.Controls.Add(Me.lblCpName)
        Me.flowCpGrpPatientInfo.Controls.Add(Me.lblCpMeta)
        Me.flowCpGrpPatientInfo.Controls.Add(Me.lblCpPhone)
        Me.flowCpGrpPatientInfo.Controls.Add(Me.lblCpWhats)
        Me.flowCpGrpPatientInfo.Controls.Add(Me.lblCpWhatsNumber)
        Me.flowCpGrpPatientInfo.Name = "flowCpGrpPatientInfo"
        '
        'lblCpHealth
        '
        Me.lblCpHealth.Appearance.Font = CType(resources.GetObject("lblCpHealth.Appearance.Font"), System.Drawing.Font)
        Me.lblCpHealth.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCpHealth.Appearance.Options.UseFont = True
        Me.lblCpHealth.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpHealth, "lblCpHealth")
        Me.lblCpHealth.Name = "lblCpHealth"
        '
        'lblCpName
        '
        Me.lblCpName.Appearance.Font = CType(resources.GetObject("lblCpName.Appearance.Font"), System.Drawing.Font)
        Me.lblCpName.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblCpName, "lblCpName")
        Me.lblCpName.Name = "lblCpName"
        '
        'lblCpMeta
        '
        Me.lblCpMeta.Appearance.Font = CType(resources.GetObject("lblCpMeta.Appearance.Font"), System.Drawing.Font)
        Me.lblCpMeta.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCpMeta.Appearance.Options.UseFont = True
        Me.lblCpMeta.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpMeta, "lblCpMeta")
        Me.lblCpMeta.Name = "lblCpMeta"
        '
        'lblCpPhone
        '
        Me.lblCpPhone.Appearance.Font = CType(resources.GetObject("lblCpPhone.Appearance.Font"), System.Drawing.Font)
        Me.lblCpPhone.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblCpPhone, "lblCpPhone")
        Me.lblCpPhone.Name = "lblCpPhone"
        '
        'lblCpWhats
        '
        Me.lblCpWhats.Appearance.Font = CType(resources.GetObject("lblCpWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblCpWhats.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblCpWhats, "lblCpWhats")
        Me.lblCpWhats.Name = "lblCpWhats"
        '
        'lblCpWhatsNumber
        '
        Me.lblCpWhatsNumber.Appearance.Font = CType(resources.GetObject("lblCpWhatsNumber.Appearance.Font"), System.Drawing.Font)
        Me.lblCpWhatsNumber.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCpWhatsNumber.Appearance.Options.UseFont = True
        Me.lblCpWhatsNumber.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpWhatsNumber, "lblCpWhatsNumber")
        Me.lblCpWhatsNumber.Name = "lblCpWhatsNumber"
        '
        'grpCpAppts
        '
        Me.grpCpAppts.AppearanceCaption.Font = CType(resources.GetObject("grpCpAppts.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpCpAppts.AppearanceCaption.Options.UseFont = True
        Me.pnlCpPatientTable.SetColumn(Me.grpCpAppts, 1)
        Me.grpCpAppts.Controls.Add(Me.flowCpGrpAppts)
        resources.ApplyResources(Me.grpCpAppts, "grpCpAppts")
        Me.grpCpAppts.Name = "grpCpAppts"
        Me.pnlCpPatientTable.SetRow(Me.grpCpAppts, 0)
        '
        'flowCpGrpAppts
        '
        resources.ApplyResources(Me.flowCpGrpAppts, "flowCpGrpAppts")
        Me.flowCpGrpAppts.Controls.Add(Me.lblCpApptTotal)
        Me.flowCpGrpAppts.Controls.Add(Me.lblCpApptFirst)
        Me.flowCpGrpAppts.Controls.Add(Me.lblCpApptLast)
        Me.flowCpGrpAppts.Controls.Add(Me.lblCpNextAppt)
        Me.flowCpGrpAppts.Controls.Add(Me.hlCpAppts)
        Me.flowCpGrpAppts.Name = "flowCpGrpAppts"
        '
        'lblCpApptTotal
        '
        Me.lblCpApptTotal.Appearance.Font = CType(resources.GetObject("lblCpApptTotal.Appearance.Font"), System.Drawing.Font)
        Me.lblCpApptTotal.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblCpApptTotal, "lblCpApptTotal")
        Me.lblCpApptTotal.Name = "lblCpApptTotal"
        '
        'lblCpApptFirst
        '
        Me.lblCpApptFirst.Appearance.Font = CType(resources.GetObject("lblCpApptFirst.Appearance.Font"), System.Drawing.Font)
        Me.lblCpApptFirst.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCpApptFirst.Appearance.Options.UseFont = True
        Me.lblCpApptFirst.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpApptFirst, "lblCpApptFirst")
        Me.lblCpApptFirst.Name = "lblCpApptFirst"
        '
        'lblCpApptLast
        '
        Me.lblCpApptLast.Appearance.Font = CType(resources.GetObject("lblCpApptLast.Appearance.Font"), System.Drawing.Font)
        Me.lblCpApptLast.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCpApptLast.Appearance.Options.UseFont = True
        Me.lblCpApptLast.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpApptLast, "lblCpApptLast")
        Me.lblCpApptLast.Name = "lblCpApptLast"
        '
        'lblCpNextAppt
        '
        Me.lblCpNextAppt.Appearance.Font = CType(resources.GetObject("lblCpNextAppt.Appearance.Font"), System.Drawing.Font)
        Me.lblCpNextAppt.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblCpNextAppt, "lblCpNextAppt")
        Me.lblCpNextAppt.Name = "lblCpNextAppt"
        '
        'hlCpAppts
        '
        Me.hlCpAppts.Appearance.Font = CType(resources.GetObject("hlCpAppts.Appearance.Font"), System.Drawing.Font)
        Me.hlCpAppts.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.hlCpAppts.Appearance.Options.UseFont = True
        Me.hlCpAppts.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.hlCpAppts, "hlCpAppts")
        Me.hlCpAppts.Cursor = System.Windows.Forms.Cursors.Hand
        Me.hlCpAppts.Name = "hlCpAppts"
        '
        'grpCpTreats
        '
        Me.grpCpTreats.AppearanceCaption.Font = CType(resources.GetObject("grpCpTreats.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpCpTreats.AppearanceCaption.Options.UseFont = True
        Me.pnlCpPatientTable.SetColumn(Me.grpCpTreats, 2)
        Me.grpCpTreats.Controls.Add(Me.flowCpGrpTreats)
        resources.ApplyResources(Me.grpCpTreats, "grpCpTreats")
        Me.grpCpTreats.Name = "grpCpTreats"
        Me.pnlCpPatientTable.SetRow(Me.grpCpTreats, 0)
        '
        'flowCpGrpTreats
        '
        resources.ApplyResources(Me.flowCpGrpTreats, "flowCpGrpTreats")
        Me.flowCpGrpTreats.Controls.Add(Me.hlCpTrtCount)
        Me.flowCpGrpTreats.Controls.Add(Me.lblCpTrtFirst)
        Me.flowCpGrpTreats.Controls.Add(Me.lblCpTrtLast)
        Me.flowCpGrpTreats.Controls.Add(Me.hlCpTrtSum)
        Me.flowCpGrpTreats.Controls.Add(Me.hlCpPaySum)
        Me.flowCpGrpTreats.Controls.Add(Me.lblCpBalance)
        Me.flowCpGrpTreats.Name = "flowCpGrpTreats"
        '
        'hlCpTrtCount
        '
        Me.hlCpTrtCount.Appearance.Font = CType(resources.GetObject("hlCpTrtCount.Appearance.Font"), System.Drawing.Font)
        Me.hlCpTrtCount.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.hlCpTrtCount.Appearance.Options.UseFont = True
        Me.hlCpTrtCount.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.hlCpTrtCount, "hlCpTrtCount")
        Me.hlCpTrtCount.Cursor = System.Windows.Forms.Cursors.Hand
        Me.hlCpTrtCount.Name = "hlCpTrtCount"
        '
        'lblCpTrtFirst
        '
        Me.lblCpTrtFirst.Appearance.Font = CType(resources.GetObject("lblCpTrtFirst.Appearance.Font"), System.Drawing.Font)
        Me.lblCpTrtFirst.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCpTrtFirst.Appearance.Options.UseFont = True
        Me.lblCpTrtFirst.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpTrtFirst, "lblCpTrtFirst")
        Me.lblCpTrtFirst.Name = "lblCpTrtFirst"
        '
        'lblCpTrtLast
        '
        Me.lblCpTrtLast.Appearance.Font = CType(resources.GetObject("lblCpTrtLast.Appearance.Font"), System.Drawing.Font)
        Me.lblCpTrtLast.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCpTrtLast.Appearance.Options.UseFont = True
        Me.lblCpTrtLast.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpTrtLast, "lblCpTrtLast")
        Me.lblCpTrtLast.Name = "lblCpTrtLast"
        '
        'hlCpTrtSum
        '
        Me.hlCpTrtSum.Appearance.Font = CType(resources.GetObject("hlCpTrtSum.Appearance.Font"), System.Drawing.Font)
        Me.hlCpTrtSum.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.hlCpTrtSum.Appearance.Options.UseFont = True
        Me.hlCpTrtSum.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.hlCpTrtSum, "hlCpTrtSum")
        Me.hlCpTrtSum.Cursor = System.Windows.Forms.Cursors.Hand
        Me.hlCpTrtSum.Name = "hlCpTrtSum"
        '
        'hlCpPaySum
        '
        Me.hlCpPaySum.Appearance.Font = CType(resources.GetObject("hlCpPaySum.Appearance.Font"), System.Drawing.Font)
        Me.hlCpPaySum.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.hlCpPaySum.Appearance.Options.UseFont = True
        Me.hlCpPaySum.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.hlCpPaySum, "hlCpPaySum")
        Me.hlCpPaySum.Cursor = System.Windows.Forms.Cursors.Hand
        Me.hlCpPaySum.Name = "hlCpPaySum"
        '
        'lblCpBalance
        '
        Me.lblCpBalance.Appearance.Font = CType(resources.GetObject("lblCpBalance.Appearance.Font"), System.Drawing.Font)
        Me.lblCpBalance.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblCpBalance, "lblCpBalance")
        Me.lblCpBalance.Name = "lblCpBalance"
        '
        'grpCpOrtho
        '
        Me.grpCpOrtho.AppearanceCaption.Font = CType(resources.GetObject("grpCpOrtho.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpCpOrtho.AppearanceCaption.Options.UseFont = True
        Me.pnlCpPatientTable.SetColumn(Me.grpCpOrtho, 3)
        Me.grpCpOrtho.Controls.Add(Me.flowCpGrpOrtho)
        resources.ApplyResources(Me.grpCpOrtho, "grpCpOrtho")
        Me.grpCpOrtho.Name = "grpCpOrtho"
        Me.pnlCpPatientTable.SetRow(Me.grpCpOrtho, 0)
        '
        'flowCpGrpOrtho
        '
        resources.ApplyResources(Me.flowCpGrpOrtho, "flowCpGrpOrtho")
        Me.flowCpGrpOrtho.Controls.Add(Me.lblCpOrthoStart)
        Me.flowCpGrpOrtho.Controls.Add(Me.lblCpOrthoLast)
        Me.flowCpGrpOrtho.Controls.Add(Me.hlCpOrthoFlag)
        Me.flowCpGrpOrtho.Name = "flowCpGrpOrtho"
        '
        'lblCpOrthoStart
        '
        Me.lblCpOrthoStart.Appearance.Font = CType(resources.GetObject("lblCpOrthoStart.Appearance.Font"), System.Drawing.Font)
        Me.lblCpOrthoStart.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblCpOrthoStart, "lblCpOrthoStart")
        Me.lblCpOrthoStart.Name = "lblCpOrthoStart"
        '
        'lblCpOrthoLast
        '
        Me.lblCpOrthoLast.Appearance.Font = CType(resources.GetObject("lblCpOrthoLast.Appearance.Font"), System.Drawing.Font)
        Me.lblCpOrthoLast.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCpOrthoLast.Appearance.Options.UseFont = True
        Me.lblCpOrthoLast.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpOrthoLast, "lblCpOrthoLast")
        Me.lblCpOrthoLast.Name = "lblCpOrthoLast"
        '
        'hlCpOrthoFlag
        '
        Me.hlCpOrthoFlag.Appearance.Font = CType(resources.GetObject("hlCpOrthoFlag.Appearance.Font"), System.Drawing.Font)
        Me.hlCpOrthoFlag.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.hlCpOrthoFlag.Appearance.Options.UseFont = True
        Me.hlCpOrthoFlag.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.hlCpOrthoFlag, "hlCpOrthoFlag")
        Me.hlCpOrthoFlag.Cursor = System.Windows.Forms.Cursors.Hand
        Me.hlCpOrthoFlag.Name = "hlCpOrthoFlag"
        '
        'grpCpDiag
        '
        Me.grpCpDiag.AppearanceCaption.Font = CType(resources.GetObject("grpCpDiag.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpCpDiag.AppearanceCaption.Options.UseFont = True
        Me.pnlCpPatientTable.SetColumn(Me.grpCpDiag, 4)
        Me.grpCpDiag.Controls.Add(Me.flowCpGrpDiag)
        resources.ApplyResources(Me.grpCpDiag, "grpCpDiag")
        Me.grpCpDiag.Name = "grpCpDiag"
        Me.pnlCpPatientTable.SetRow(Me.grpCpDiag, 0)
        '
        'flowCpGrpDiag
        '
        resources.ApplyResources(Me.flowCpGrpDiag, "flowCpGrpDiag")
        Me.flowCpGrpDiag.Controls.Add(Me.hlCpDiagFlag)
        Me.flowCpGrpDiag.Controls.Add(Me.lblCpDiagCount)
        Me.flowCpGrpDiag.Controls.Add(Me.lblCpDiagFirst)
        Me.flowCpGrpDiag.Controls.Add(Me.lblCpDiagLast)
        Me.flowCpGrpDiag.Controls.Add(Me.lblCpDiagAgreements)
        Me.flowCpGrpDiag.Controls.Add(Me.lblCpDiagDetFirst)
        Me.flowCpGrpDiag.Controls.Add(Me.lblCpDiagDetLast)
        Me.flowCpGrpDiag.Name = "flowCpGrpDiag"
        '
        'hlCpDiagFlag
        '
        Me.hlCpDiagFlag.Appearance.Font = CType(resources.GetObject("hlCpDiagFlag.Appearance.Font"), System.Drawing.Font)
        Me.hlCpDiagFlag.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.hlCpDiagFlag.Appearance.Options.UseFont = True
        Me.hlCpDiagFlag.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.hlCpDiagFlag, "hlCpDiagFlag")
        Me.hlCpDiagFlag.Cursor = System.Windows.Forms.Cursors.Hand
        Me.hlCpDiagFlag.Name = "hlCpDiagFlag"
        '
        'lblCpDiagCount
        '
        Me.lblCpDiagCount.Appearance.Font = CType(resources.GetObject("lblCpDiagCount.Appearance.Font"), System.Drawing.Font)
        Me.lblCpDiagCount.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblCpDiagCount, "lblCpDiagCount")
        Me.lblCpDiagCount.Name = "lblCpDiagCount"
        '
        'lblCpDiagFirst
        '
        Me.lblCpDiagFirst.Appearance.Font = CType(resources.GetObject("lblCpDiagFirst.Appearance.Font"), System.Drawing.Font)
        Me.lblCpDiagFirst.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCpDiagFirst.Appearance.Options.UseFont = True
        Me.lblCpDiagFirst.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpDiagFirst, "lblCpDiagFirst")
        Me.lblCpDiagFirst.Name = "lblCpDiagFirst"
        '
        'lblCpDiagLast
        '
        Me.lblCpDiagLast.Appearance.Font = CType(resources.GetObject("lblCpDiagLast.Appearance.Font"), System.Drawing.Font)
        Me.lblCpDiagLast.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCpDiagLast.Appearance.Options.UseFont = True
        Me.lblCpDiagLast.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpDiagLast, "lblCpDiagLast")
        Me.lblCpDiagLast.Name = "lblCpDiagLast"
        '
        'lblCpDiagAgreements
        '
        Me.lblCpDiagAgreements.Appearance.Font = CType(resources.GetObject("lblCpDiagAgreements.Appearance.Font"), System.Drawing.Font)
        Me.lblCpDiagAgreements.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblCpDiagAgreements, "lblCpDiagAgreements")
        Me.lblCpDiagAgreements.Name = "lblCpDiagAgreements"
        '
        'lblCpDiagDetFirst
        '
        Me.lblCpDiagDetFirst.Appearance.Font = CType(resources.GetObject("lblCpDiagDetFirst.Appearance.Font"), System.Drawing.Font)
        Me.lblCpDiagDetFirst.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCpDiagDetFirst.Appearance.Options.UseFont = True
        Me.lblCpDiagDetFirst.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpDiagDetFirst, "lblCpDiagDetFirst")
        Me.lblCpDiagDetFirst.Name = "lblCpDiagDetFirst"
        '
        'lblCpDiagDetLast
        '
        Me.lblCpDiagDetLast.Appearance.Font = CType(resources.GetObject("lblCpDiagDetLast.Appearance.Font"), System.Drawing.Font)
        Me.lblCpDiagDetLast.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCpDiagDetLast.Appearance.Options.UseFont = True
        Me.lblCpDiagDetLast.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblCpDiagDetLast, "lblCpDiagDetLast")
        Me.lblCpDiagDetLast.Name = "lblCpDiagDetLast"
        '
        'grpCpImages
        '
        Me.grpCpImages.AppearanceCaption.Font = CType(resources.GetObject("grpCpImages.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpCpImages.AppearanceCaption.Options.UseFont = True
        Me.pnlCpPatientTable.SetColumn(Me.grpCpImages, 6)
        Me.grpCpImages.Controls.Add(Me.flowCpGrpImages)
        resources.ApplyResources(Me.grpCpImages, "grpCpImages")
        Me.grpCpImages.Name = "grpCpImages"
        Me.pnlCpPatientTable.SetRow(Me.grpCpImages, 0)
        '
        'flowCpGrpImages
        '
        resources.ApplyResources(Me.flowCpGrpImages, "flowCpGrpImages")
        Me.flowCpGrpImages.Controls.Add(Me.hlCpImgBefore)
        Me.flowCpGrpImages.Controls.Add(Me.hlCpImgDuring)
        Me.flowCpGrpImages.Controls.Add(Me.hlCpImgAfter)
        Me.flowCpGrpImages.Name = "flowCpGrpImages"
        '
        'hlCpImgBefore
        '
        Me.hlCpImgBefore.Appearance.Font = CType(resources.GetObject("hlCpImgBefore.Appearance.Font"), System.Drawing.Font)
        Me.hlCpImgBefore.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.hlCpImgBefore.Appearance.Options.UseFont = True
        Me.hlCpImgBefore.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.hlCpImgBefore, "hlCpImgBefore")
        Me.hlCpImgBefore.Cursor = System.Windows.Forms.Cursors.Hand
        Me.hlCpImgBefore.Name = "hlCpImgBefore"
        '
        'hlCpImgDuring
        '
        Me.hlCpImgDuring.Appearance.Font = CType(resources.GetObject("hlCpImgDuring.Appearance.Font"), System.Drawing.Font)
        Me.hlCpImgDuring.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.hlCpImgDuring.Appearance.Options.UseFont = True
        Me.hlCpImgDuring.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.hlCpImgDuring, "hlCpImgDuring")
        Me.hlCpImgDuring.Cursor = System.Windows.Forms.Cursors.Hand
        Me.hlCpImgDuring.Name = "hlCpImgDuring"
        '
        'hlCpImgAfter
        '
        Me.hlCpImgAfter.Appearance.Font = CType(resources.GetObject("hlCpImgAfter.Appearance.Font"), System.Drawing.Font)
        Me.hlCpImgAfter.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.hlCpImgAfter.Appearance.Options.UseFont = True
        Me.hlCpImgAfter.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.hlCpImgAfter, "hlCpImgAfter")
        Me.hlCpImgAfter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.hlCpImgAfter.Name = "hlCpImgAfter"
        '
        'grpCpLabOrders
        '
        Me.grpCpLabOrders.AppearanceCaption.Font = CType(resources.GetObject("grpCpLabOrders.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpCpLabOrders.AppearanceCaption.Options.UseFont = True
        Me.pnlCpPatientTable.SetColumn(Me.grpCpLabOrders, 5)
        Me.grpCpLabOrders.Controls.Add(Me.flowCpGrpLabs)
        resources.ApplyResources(Me.grpCpLabOrders, "grpCpLabOrders")
        Me.grpCpLabOrders.Name = "grpCpLabOrders"
        Me.pnlCpPatientTable.SetRow(Me.grpCpLabOrders, 0)
        '
        'flowCpGrpLabs
        '
        resources.ApplyResources(Me.flowCpGrpLabs, "flowCpGrpLabs")
        Me.flowCpGrpLabs.Controls.Add(Me.hlCpLabs)
        Me.flowCpGrpLabs.Name = "flowCpGrpLabs"
        '
        'hlCpLabs
        '
        Me.hlCpLabs.Appearance.Font = CType(resources.GetObject("hlCpLabs.Appearance.Font"), System.Drawing.Font)
        Me.hlCpLabs.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(106, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.hlCpLabs.Appearance.Options.UseFont = True
        Me.hlCpLabs.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.hlCpLabs, "hlCpLabs")
        Me.hlCpLabs.Cursor = System.Windows.Forms.Cursors.Hand
        Me.hlCpLabs.Name = "hlCpLabs"
        '
        'pnlCpActions
        '
        Me.pnlCpActions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlCpActions.Controls.Add(Me.flowCpActions)
        resources.ApplyResources(Me.pnlCpActions, "pnlCpActions")
        Me.pnlCpActions.Name = "pnlCpActions"
        '
        'flowCpActions
        '
        Me.flowCpActions.Controls.Add(Me.btnCpCopyPhone)
        Me.flowCpActions.Controls.Add(Me.btnCpFocusWorkspace)
        Me.flowCpActions.Controls.Add(Me.btnCpRefresh)
        resources.ApplyResources(Me.flowCpActions, "flowCpActions")
        Me.flowCpActions.Name = "flowCpActions"
        '
        'btnCpCopyPhone
        '
        Me.btnCpCopyPhone.Appearance.Font = CType(resources.GetObject("btnCpCopyPhone.Appearance.Font"), System.Drawing.Font)
        Me.btnCpCopyPhone.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnCpCopyPhone, "btnCpCopyPhone")
        Me.btnCpCopyPhone.Name = "btnCpCopyPhone"
        '
        'btnCpFocusWorkspace
        '
        Me.btnCpFocusWorkspace.Appearance.Font = CType(resources.GetObject("btnCpFocusWorkspace.Appearance.Font"), System.Drawing.Font)
        Me.btnCpFocusWorkspace.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnCpFocusWorkspace, "btnCpFocusWorkspace")
        Me.btnCpFocusWorkspace.Name = "btnCpFocusWorkspace"
        '
        'btnCpRefresh
        '
        Me.btnCpRefresh.Appearance.Font = CType(resources.GetObject("btnCpRefresh.Appearance.Font"), System.Drawing.Font)
        Me.btnCpRefresh.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnCpRefresh, "btnCpRefresh")
        Me.btnCpRefresh.Name = "btnCpRefresh"
        '
        'SkinBarSubItem1
        '
        resources.ApplyResources(Me.SkinBarSubItem1, "SkinBarSubItem1")
        Me.SkinBarSubItem1.Id = 2
        Me.SkinBarSubItem1.Name = "SkinBarSubItem1"
        '
        'BarHeaderItem1
        '
        resources.ApplyResources(Me.BarHeaderItem1, "BarHeaderItem1")
        Me.BarHeaderItem1.Id = 3
        Me.BarHeaderItem1.Name = "BarHeaderItem1"
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        '
        'RadioEn
        '
        resources.ApplyResources(Me.RadioEn, "RadioEn")
        Me.RadioEn.Name = "RadioEn"
        Me.RadioEn.TabStop = True
        Me.RadioEn.UseVisualStyleBackColor = True
        '
        'RadioAr
        '
        resources.ApplyResources(Me.RadioAr, "RadioAr")
        Me.RadioAr.Name = "RadioAr"
        Me.RadioAr.TabStop = True
        Me.RadioAr.UseVisualStyleBackColor = True
        '
        'MainView3
        '
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ControlContainer = Me.ContainerA
        Me.Controls.Add(Me.ContainerA)
        Me.Controls.Add(Me.RadioAr)
        Me.Controls.Add(Me.RadioEn)
        Me.Controls.Add(Me.MainAccordion)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.hideContainerLeft)
        Me.Controls.Add(Me.hideContainerRight)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Controls.Add(Me.FluentDesignFormControl1)
        Me.FluentDesignFormControl = Me.FluentDesignFormControl1
        Me.IconOptions.Image = CType(resources.GetObject("MainView3.IconOptions.Image"), System.Drawing.Image)
        Me.Name = "MainView3"
        Me.NavigationControl = Me.MainAccordion
        CType(Me.MainAccordion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FluentDesignFormControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.langComboItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FluentFormDefaultManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHypertextLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerRight.ResumeLayout(False)
        Me.DockMessageCenter.ResumeLayout(False)
        Me.DockPanel2_Container.ResumeLayout(False)
        CType(Me.MsgCenterToolbarPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MsgCenterToolbarPanel.ResumeLayout(False)
        Me.MsgCenterToolbarPanel.PerformLayout()
        CType(Me.chkMsgCenterFailuresOnly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockGlobalInfo.ResumeLayout(False)
        Me.hideContainerLeft.ResumeLayout(False)
        Me.DockColorKeys.ResumeLayout(False)
        Me.hideContainerTop.ResumeLayout(False)
        Me.DockCurrentPatient.ResumeLayout(False)
        Me.DockPanel3_Container.ResumeLayout(False)
        CType(Me.panelCurrentPatientHost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelCurrentPatientHost.ResumeLayout(False)
        CType(Me.panelCurrentPatientBody, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelCurrentPatientBody.ResumeLayout(False)
        CType(Me.pnlCpPatientTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCpPatientTable.ResumeLayout(False)
        CType(Me.grpCpPatientInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCpPatientInfo.ResumeLayout(False)
        Me.flowCpGrpPatientInfo.ResumeLayout(False)
        Me.flowCpGrpPatientInfo.PerformLayout()
        CType(Me.grpCpAppts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCpAppts.ResumeLayout(False)
        Me.flowCpGrpAppts.ResumeLayout(False)
        Me.flowCpGrpAppts.PerformLayout()
        CType(Me.grpCpTreats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCpTreats.ResumeLayout(False)
        Me.flowCpGrpTreats.ResumeLayout(False)
        Me.flowCpGrpTreats.PerformLayout()
        CType(Me.grpCpOrtho, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCpOrtho.ResumeLayout(False)
        Me.flowCpGrpOrtho.ResumeLayout(False)
        Me.flowCpGrpOrtho.PerformLayout()
        CType(Me.grpCpDiag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCpDiag.ResumeLayout(False)
        Me.flowCpGrpDiag.ResumeLayout(False)
        Me.flowCpGrpDiag.PerformLayout()
        CType(Me.grpCpImages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCpImages.ResumeLayout(False)
        Me.flowCpGrpImages.ResumeLayout(False)
        Me.flowCpGrpImages.PerformLayout()
        CType(Me.grpCpLabOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCpLabOrders.ResumeLayout(False)
        Me.flowCpGrpLabs.ResumeLayout(False)
        Me.flowCpGrpLabs.PerformLayout()
        CType(Me.pnlCpActions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCpActions.ResumeLayout(False)
        Me.flowCpActions.ResumeLayout(False)
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ContainerA As DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer
    Friend WithEvents MainAccordion As DevExpress.XtraBars.Navigation.AccordionControl
    Friend WithEvents ClinicButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents FluentDesignFormControl1 As DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl
    Friend WithEvents FluentFormDefaultManager1 As DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager
    Friend WithEvents TreatsButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator1 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents OrthoButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator2 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents DiagButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator3 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents AuxButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccountsButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator4 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents VisitsButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator5 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents NotesButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator6 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents RxButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator7 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents ImagesButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator8 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents AccordionControlSeparator9 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents MainMenu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents DBaseMnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents ListChckConnMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ListBackupMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SkinDropDownButtonItem1 As DevExpress.XtraBars.SkinDropDownButtonItem
    Friend WithEvents ListRestoreMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SettingsMnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents ListSettingsMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ExitMnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents btnMnuExit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemHypertextLabel1 As DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel
    Friend WithEvents BasicDataMenu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents CitiesMnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents HealthMnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents TreatsMnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents WireTypeMnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents WireMeasureMnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents VisitTypeMnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents RxDetailsMnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents ListCitiesMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ListHealthMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ListTreatsMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ListWireTypeMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ListWireMeasureMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ListVisitTypeMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ListRxDetailsMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SplashScreenManager1 As DevExpress.XtraSplashScreen.SplashScreenManager
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents StatusBar3 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarHeaderItem1 As DevExpress.XtraBars.BarHeaderItem
    Friend WithEvents SkinBarSubItem1 As DevExpress.XtraBars.SkinBarSubItem
    Friend WithEvents stLoggedUserLbl As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents stUserNameTxt As DevExpress.XtraBars.BarHeaderItem
    Friend WithEvents stFormNameLbl As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents stFormNameTxt As DevExpress.XtraBars.BarHeaderItem
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents stPatientNameLbl As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents stPatientNameTxt As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents stLoadingLbl As DevExpress.XtraBars.BarHeaderItem
    Friend WithEvents chkBackup As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents ListUserMngmntMnu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents ListUsersMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ListAddNewUsrGrpMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ListPermissionMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ListChangePassMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents LstRestPassMnu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bntLogInOUT As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents UseHdrCheckItem As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents BtnDashCreate As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BtnFinancePass As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents AccordionControlSeparator10 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents LaboratoriesButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents btnListLabs As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents btnLabOrder As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents btnRecieveOrder As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents btnLabPay As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator11 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents PatientsButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents btnListPatients As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents btnPatientsDebts As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator12 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents DoctorsButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents RibMenuDocs As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents btnSecretaries As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents btnEmployees As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator13 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents VendorsButton As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents btnAbout As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ScheduleAdmin As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents BtnMedic As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnRxFly As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnClinicInfo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents HelperBarSub As DevExpress.XtraBars.BarSubItem
    Friend WithEvents btnCheuqes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents TodayButton As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSettings As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BtnListVendors As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnWhatsApp As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnApptSend As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnAccountWhats As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnAccountReminder As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnWhatsAppActivityLog As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarWhats As DevExpress.XtraBars.BarSubItem
    Friend WithEvents btnStaffMange As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnApptsReminder As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnLast10Patients As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents DockMessageCenter As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel2_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents MsgCenterToolbarPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblMsgCenterTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtMsgCenterSearch As System.Windows.Forms.TextBox
    Friend WithEvents chkMsgCenterFailuresOnly As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnMsgCenterClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnMsgCenterOpenLog As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnMsgCenterCopyRow As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents flowWhatsSessionFeed As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockGlobalInfo As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel4_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents DockCurrentPatient As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel3_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents panelCurrentPatientHost As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblCurrentPatientEmpty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents panelCurrentPatientBody As DevExpress.XtraEditors.PanelControl
    Friend WithEvents pnlCpPatientTable As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents lblCpName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpMeta As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpPhone As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpWhatsNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpBalance As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpNextAppt As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpHealth As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnlCpActions As DevExpress.XtraEditors.PanelControl
    Friend WithEvents flowCpActions As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnCpCopyPhone As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCpFocusWorkspace As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCpRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpCpLabOrders As DevExpress.XtraEditors.GroupControl
    Friend WithEvents flowCpGrpLabs As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents grpCpImages As DevExpress.XtraEditors.GroupControl
    Friend WithEvents flowCpGrpImages As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents grpCpDiag As DevExpress.XtraEditors.GroupControl
    Friend WithEvents flowCpGrpDiag As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents grpCpOrtho As DevExpress.XtraEditors.GroupControl
    Friend WithEvents flowCpGrpOrtho As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents grpCpTreats As DevExpress.XtraEditors.GroupControl
    Friend WithEvents flowCpGrpTreats As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents grpCpAppts As DevExpress.XtraEditors.GroupControl
    Friend WithEvents flowCpGrpAppts As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblCpApptTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpApptFirst As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpApptLast As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpOrthoStart As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpOrthoLast As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpTrtFirst As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpTrtLast As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpDiagCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpDiagFirst As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpDiagLast As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpDiagAgreements As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpDiagDetFirst As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCpDiagDetLast As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpCpPatientInfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents flowCpGrpPatientInfo As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents hlCpAppts As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents hlCpTrtCount As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents hlCpTrtSum As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents hlCpPaySum As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents hlCpOrthoFlag As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents hlCpDiagFlag As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents hlCpImgBefore As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents hlCpImgDuring As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents hlCpImgAfter As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents hlCpLabs As DevExpress.XtraEditors.HyperlinkLabelControl
    Friend WithEvents DockColorKeys As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents hideContainerLeft As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents hideContainerRight As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents spinShortReminder As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents btnScheduler As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents CultureMngr As Infralution.Localization.CultureManager
    Friend WithEvents ToggleSwch As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RadioAr As RadioButton
    Friend WithEvents RadioEn As RadioButton
    Friend WithEvents langCombo As DevExpress.XtraBars.BarEditItem
    Friend WithEvents langComboItems As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents btnSnapshotSender As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnCsiImage As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents AccordionControlSeparator14 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents AccordionControlSeparator15 As DevExpress.XtraBars.Navigation.AccordionControlSeparator
    Friend WithEvents btnOLdScheduler As DevExpress.XtraBars.Navigation.AccordionControlElement
End Class
