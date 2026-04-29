<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPatient
		Inherits DevExpress.XtraBars.Ribbon.RibbonForm
		  ''' <summary>
		  ''' Required designer variable.
		  ''' </summary>
		  Private components As System.ComponentModel.IContainer = Nothing
		  ''' <summary>
		  ''' Clean up any resources being used..
		  ''' </summary>
		  '''  <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		    If disposing AndAlso (components IsNot Nothing) Then
			  components.Dispose()  
		  End If 
		  MyBase.Dispose(disposing)  
	  End Sub 
#Region "Windows Form Designer generated code"

		  ''' <summary>
		  ''' Required method for Designer support - do not modify
		  ''' the contents of this method with the code editor.
		  ''' </summary>
	    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPatient))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSex = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAge = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStillKid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPhone = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWhatsAppPrefix = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWhatsApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAddress = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colHealth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTreat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colImplant = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMobile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOrtho = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDiag = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStruc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBirthY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCreatedBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCreateDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ribbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.bbiPrintPreview = New DevExpress.XtraBars.BarButtonItem()
        Me.bsiRecordsCount = New DevExpress.XtraBars.BarStaticItem()
        Me.bAdd = New DevExpress.XtraBars.BarButtonItem()
        Me.bEdit = New DevExpress.XtraBars.BarButtonItem()
        Me.bDelete = New DevExpress.XtraBars.BarButtonItem()
        Me.bRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.bCancel = New DevExpress.XtraBars.BarButtonItem()
        Me.ribbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.ribbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.ribbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.ribbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.TableData = New DevExpress.Utils.Layout.TablePanel()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.CboSex = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CboHlthStat = New DentistX.HlthCombo()
        Me.CboAddress = New DentistX.CityCombo()
        Me.PatientIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PatientIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.PatientNameLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PatientNameTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.SexLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SexTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.AgeLabel = New DevExpress.XtraEditors.LabelControl()
        Me.AgeSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.StillKidLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PhoneLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PhoneTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.AddressLabel = New DevExpress.XtraEditors.LabelControl()
        Me.AddressTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.HealthLabel = New DevExpress.XtraEditors.LabelControl()
        Me.HealthTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.TreatLabel = New DevExpress.XtraEditors.LabelControl()
        Me.TreatCheckEdit = New DevExpress.XtraEditors.CheckEdit()
        Me.MobileLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DiagCheckEdit = New DevExpress.XtraEditors.CheckEdit()
        Me.OrthoLabel = New DevExpress.XtraEditors.LabelControl()
        Me.OrthoCheckEdit = New DevExpress.XtraEditors.CheckEdit()
        Me.NotesLabel = New DevExpress.XtraEditors.LabelControl()
        Me.NotesTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.TLPBut = New DevExpress.Utils.Layout.TablePanel()
        Me.butApply = New DevExpress.XtraEditors.SimpleButton()
        Me.butCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel1.SuspendLayout()
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel2.SuspendLayout()
        Me.Splitter1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ribbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableData.SuspendLayout()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboSex.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SexTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AgeSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AddressTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HealthTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreatCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DiagCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OrthoCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLPBut.SuspendLayout()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.Horizontal = False
        Me.Splitter1.Name = "Splitter1"
        '
        'Splitter1.Panel1
        '
        Me.Splitter1.Panel1.Controls.Add(Me.DGV)
        '
        'Splitter1.Panel2
        '
        Me.Splitter1.Panel2.Controls.Add(Me.TableData)
        Me.Splitter1.SplitterPosition = 275
        '
        'DGV
        '
        resources.ApplyResources(Me.DGV, "DGV")
        Me.DGV.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.DGV.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.DGV.MainView = Me.dgView
        Me.DGV.MenuManager = Me.ribbonControl
        Me.DGV.Name = "DGV"
        Me.DGV.UseEmbeddedNavigator = True
        Me.DGV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        Me.dgView.Appearance.HeaderPanel.Font = CType(resources.GetObject("dgView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
        Me.dgView.Appearance.Row.Font = CType(resources.GetObject("dgView.Appearance.Row.Font"), System.Drawing.Font)
        Me.dgView.Appearance.Row.Options.UseFont = True
        Me.dgView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colPatientID, Me.colPatientNum, Me.colPatientName, Me.colSex, Me.colAge, Me.colStillKid, Me.colPhone, Me.colWhatsAppPrefix, Me.colWhatsApp, Me.colAddress, Me.colHealth, Me.colTreat, Me.colImplant, Me.colMobile, Me.colOrtho, Me.colDiag, Me.colStruc, Me.colNotes, Me.colBirthY, Me.colCreatedBy, Me.colCreateDate})
        Me.dgView.DetailHeight = 377
        Me.dgView.GridControl = Me.DGV
        Me.dgView.Name = "dgView"
        Me.dgView.OptionsBehavior.Editable = False
        Me.dgView.OptionsBehavior.ReadOnly = True
        Me.dgView.OptionsDetail.EnableMasterViewMode = False
        Me.dgView.OptionsView.EnableAppearanceEvenRow = True
        Me.dgView.OptionsView.ShowFooter = True
        '
        'colRowNum
        '
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colPatientID
        '
        Me.colPatientID.FieldName = "PatientID"
        Me.colPatientID.Name = "colPatientID"
        resources.ApplyResources(Me.colPatientID, "colPatientID")
        '
        'colPatientNum
        '
        resources.ApplyResources(Me.colPatientNum, "colPatientNum")
        Me.colPatientNum.FieldName = "PatientNumber"
        Me.colPatientNum.Name = "colPatientNum"
        '
        'colPatientName
        '
        Me.colPatientName.FieldName = "PatientName"
        Me.colPatientName.Name = "colPatientName"
        resources.ApplyResources(Me.colPatientName, "colPatientName")
        '
        'colSex
        '
        Me.colSex.FieldName = "Sex"
        Me.colSex.Name = "colSex"
        resources.ApplyResources(Me.colSex, "colSex")
        '
        'colAge
        '
        Me.colAge.FieldName = "Age"
        Me.colAge.Name = "colAge"
        resources.ApplyResources(Me.colAge, "colAge")
        '
        'colStillKid
        '
        Me.colStillKid.FieldName = "StillKid"
        Me.colStillKid.Name = "colStillKid"
        '
        'colPhone
        '
        Me.colPhone.FieldName = "Phone"
        Me.colPhone.Name = "colPhone"
        resources.ApplyResources(Me.colPhone, "colPhone")
        '
        'colWhatsAppPrefix
        '
        resources.ApplyResources(Me.colWhatsAppPrefix, "colWhatsAppPrefix")
        Me.colWhatsAppPrefix.FieldName = "WhatsAppPrefix"
        Me.colWhatsAppPrefix.Name = "colWhatsAppPrefix"
        '
        'colWhatsApp
        '
        resources.ApplyResources(Me.colWhatsApp, "colWhatsApp")
        Me.colWhatsApp.FieldName = "WhatsApp"
        Me.colWhatsApp.Name = "colWhatsApp"
        '
        'colAddress
        '
        Me.colAddress.FieldName = "Address"
        Me.colAddress.Name = "colAddress"
        resources.ApplyResources(Me.colAddress, "colAddress")
        '
        'colHealth
        '
        Me.colHealth.FieldName = "Health"
        Me.colHealth.Name = "colHealth"
        resources.ApplyResources(Me.colHealth, "colHealth")
        '
        'colTreat
        '
        Me.colTreat.FieldName = "Treat"
        Me.colTreat.Name = "colTreat"
        resources.ApplyResources(Me.colTreat, "colTreat")
        '
        'colImplant
        '
        Me.colImplant.FieldName = "Implant"
        Me.colImplant.Name = "colImplant"
        resources.ApplyResources(Me.colImplant, "colImplant")
        '
        'colMobile
        '
        Me.colMobile.FieldName = "Mobile"
        Me.colMobile.Name = "colMobile"
        resources.ApplyResources(Me.colMobile, "colMobile")
        '
        'colOrtho
        '
        Me.colOrtho.FieldName = "Ortho"
        Me.colOrtho.Name = "colOrtho"
        resources.ApplyResources(Me.colOrtho, "colOrtho")
        '
        'colDiag
        '
        Me.colDiag.FieldName = "Diag"
        Me.colDiag.Name = "colDiag"
        resources.ApplyResources(Me.colDiag, "colDiag")
        '
        'colStruc
        '
        Me.colStruc.FieldName = "Struc"
        Me.colStruc.Name = "colStruc"
        '
        'colNotes
        '
        Me.colNotes.FieldName = "Notes"
        Me.colNotes.Name = "colNotes"
        resources.ApplyResources(Me.colNotes, "colNotes")
        '
        'colBirthY
        '
        Me.colBirthY.FieldName = "BirthY"
        Me.colBirthY.Name = "colBirthY"
        resources.ApplyResources(Me.colBirthY, "colBirthY")
        '
        'colCreatedBy
        '
        Me.colCreatedBy.FieldName = "CreatedBy"
        Me.colCreatedBy.Name = "colCreatedBy"
        '
        'colCreateDate
        '
        Me.colCreateDate.FieldName = "CreateDate"
        Me.colCreateDate.Name = "colCreateDate"
        '
        'ribbonControl
        '
        Me.ribbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(30, 32, 30, 32)
        Me.ribbonControl.ExpandCollapseItem.Id = 0
        Me.ribbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.ribbonControl.ExpandCollapseItem, Me.bbiPrintPreview, Me.bsiRecordsCount, Me.bAdd, Me.bEdit, Me.bDelete, Me.bRefresh, Me.bCancel})
        resources.ApplyResources(Me.ribbonControl, "ribbonControl")
        Me.ribbonControl.MaxItemId = 21
        Me.ribbonControl.Name = "ribbonControl"
        Me.ribbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.ribbonPage1})
        Me.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013
        Me.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribbonControl.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribbonControl.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide
        Me.ribbonControl.ShowQatLocationSelector = False
        Me.ribbonControl.ShowToolbarCustomizeItem = False
        Me.ribbonControl.StatusBar = Me.ribbonStatusBar
        Me.ribbonControl.Toolbar.ShowCustomizeItem = False
        Me.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden
        '
        'bbiPrintPreview
        '
        resources.ApplyResources(Me.bbiPrintPreview, "bbiPrintPreview")
        Me.bbiPrintPreview.Id = 14
        Me.bbiPrintPreview.ImageOptions.ImageUri.Uri = "Preview"
        Me.bbiPrintPreview.ItemAppearance.Normal.Font = CType(resources.GetObject("bbiPrintPreview.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bbiPrintPreview.ItemAppearance.Normal.Options.UseFont = True
        Me.bbiPrintPreview.Name = "bbiPrintPreview"
        '
        'bsiRecordsCount
        '
        Me.bsiRecordsCount.Id = 15
        Me.bsiRecordsCount.Name = "bsiRecordsCount"
        '
        'bAdd
        '
        resources.ApplyResources(Me.bAdd, "bAdd")
        Me.bAdd.Id = 16
        Me.bAdd.ImageOptions.ImageUri.Uri = "New"
        Me.bAdd.ItemAppearance.Normal.Font = CType(resources.GetObject("bAdd.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bAdd.ItemAppearance.Normal.Options.UseFont = True
        Me.bAdd.Name = "bAdd"
        '
        'bEdit
        '
        resources.ApplyResources(Me.bEdit, "bEdit")
        Me.bEdit.Id = 17
        Me.bEdit.ImageOptions.ImageUri.Uri = "Edit"
        Me.bEdit.ItemAppearance.Normal.Font = CType(resources.GetObject("bEdit.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bEdit.ItemAppearance.Normal.Options.UseFont = True
        Me.bEdit.Name = "bEdit"
        '
        'bDelete
        '
        resources.ApplyResources(Me.bDelete, "bDelete")
        Me.bDelete.Id = 18
        Me.bDelete.ImageOptions.ImageUri.Uri = "Delete"
        Me.bDelete.ItemAppearance.Normal.Font = CType(resources.GetObject("bDelete.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bDelete.ItemAppearance.Normal.Options.UseFont = True
        Me.bDelete.Name = "bDelete"
        '
        'bRefresh
        '
        resources.ApplyResources(Me.bRefresh, "bRefresh")
        Me.bRefresh.Id = 19
        Me.bRefresh.ImageOptions.ImageUri.Uri = "Refresh"
        Me.bRefresh.ItemAppearance.Normal.Font = CType(resources.GetObject("bRefresh.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bRefresh.ItemAppearance.Normal.Options.UseFont = True
        Me.bRefresh.Name = "bRefresh"
        '
        'bCancel
        '
        resources.ApplyResources(Me.bCancel, "bCancel")
        Me.bCancel.Id = 20
        Me.bCancel.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.undo
        Me.bCancel.ItemAppearance.Normal.Font = CType(resources.GetObject("bCancel.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bCancel.ItemAppearance.Normal.Options.UseFont = True
        Me.bCancel.Name = "bCancel"
        '
        'ribbonPage1
        '
        Me.ribbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.ribbonPageGroup1, Me.ribbonPageGroup2})
        Me.ribbonPage1.MergeOrder = 0
        Me.ribbonPage1.Name = "ribbonPage1"
        '
        'ribbonPageGroup1
        '
        Me.ribbonPageGroup1.AllowTextClipping = False
        Me.ribbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribbonPageGroup1.ItemLinks.Add(Me.bAdd)
        Me.ribbonPageGroup1.ItemLinks.Add(Me.bEdit)
        Me.ribbonPageGroup1.ItemLinks.Add(Me.bDelete)
        Me.ribbonPageGroup1.ItemLinks.Add(Me.bRefresh)
        Me.ribbonPageGroup1.ItemLinks.Add(Me.bCancel)
        Me.ribbonPageGroup1.Name = "ribbonPageGroup1"
        '
        'ribbonPageGroup2
        '
        Me.ribbonPageGroup2.AllowTextClipping = False
        Me.ribbonPageGroup2.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribbonPageGroup2.ItemLinks.Add(Me.bbiPrintPreview)
        Me.ribbonPageGroup2.Name = "ribbonPageGroup2"
        '
        'ribbonStatusBar
        '
        Me.ribbonStatusBar.ItemLinks.Add(Me.bsiRecordsCount)
        resources.ApplyResources(Me.ribbonStatusBar, "ribbonStatusBar")
        Me.ribbonStatusBar.Name = "ribbonStatusBar"
        Me.ribbonStatusBar.Ribbon = Me.ribbonControl
        '
        'TableData
        '
        Me.TableData.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 66.36!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 39.88!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 46.86!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 43.54!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 28.28!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 57.68!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 67.4!)})
        Me.TableData.Controls.Add(Me.lblWhats)
        Me.TableData.Controls.Add(Me.cboPrefix)
        Me.TableData.Controls.Add(Me.txtWhats)
        Me.TableData.Controls.Add(Me.CboSex)
        Me.TableData.Controls.Add(Me.CboHlthStat)
        Me.TableData.Controls.Add(Me.CboAddress)
        Me.TableData.Controls.Add(Me.PatientIDLabel)
        Me.TableData.Controls.Add(Me.PatientIDSpinEdit)
        Me.TableData.Controls.Add(Me.PatientNameLabel)
        Me.TableData.Controls.Add(Me.PatientNameTextEdit)
        Me.TableData.Controls.Add(Me.SexLabel)
        Me.TableData.Controls.Add(Me.SexTextEdit)
        Me.TableData.Controls.Add(Me.AgeLabel)
        Me.TableData.Controls.Add(Me.AgeSpinEdit)
        Me.TableData.Controls.Add(Me.StillKidLabel)
        Me.TableData.Controls.Add(Me.PhoneLabel)
        Me.TableData.Controls.Add(Me.PhoneTextEdit)
        Me.TableData.Controls.Add(Me.AddressLabel)
        Me.TableData.Controls.Add(Me.AddressTextEdit)
        Me.TableData.Controls.Add(Me.HealthLabel)
        Me.TableData.Controls.Add(Me.HealthTextEdit)
        Me.TableData.Controls.Add(Me.TreatLabel)
        Me.TableData.Controls.Add(Me.TreatCheckEdit)
        Me.TableData.Controls.Add(Me.MobileLabel)
        Me.TableData.Controls.Add(Me.DiagCheckEdit)
        Me.TableData.Controls.Add(Me.OrthoLabel)
        Me.TableData.Controls.Add(Me.OrthoCheckEdit)
        Me.TableData.Controls.Add(Me.NotesLabel)
        Me.TableData.Controls.Add(Me.NotesTextEdit)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Controls.Add(Me.LabelControl1)
        resources.ApplyResources(Me.TableData, "TableData")
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TableData.UseSkinIndents = True
        '
        'cboPrefix
        '
        Me.TableData.SetColumn(Me.cboPrefix, 2)
        resources.ApplyResources(Me.cboPrefix, "cboPrefix")
        Me.cboPrefix.Name = "cboPrefix"
        Me.cboPrefix.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPrefix.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.cboPrefix, 3)
        '
        'txtWhats
        '
        Me.TableData.SetColumn(Me.txtWhats, 5)
        resources.ApplyResources(Me.txtWhats, "txtWhats")
        Me.txtWhats.MenuManager = Me.ribbonControl
        Me.txtWhats.Name = "txtWhats"
        Me.TableData.SetRow(Me.txtWhats, 3)
        '
        'CboSex
        '
        resources.ApplyResources(Me.CboSex, "CboSex")
        Me.CboSex.MenuManager = Me.ribbonControl
        Me.CboSex.Name = "CboSex"
        Me.CboSex.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboSex.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'CboHlthStat
        '
        Me.TableData.SetColumn(Me.CboHlthStat, 5)
        Me.CboHlthStat.HealthStat = Nothing
        Me.CboHlthStat.HID = 0
        resources.ApplyResources(Me.CboHlthStat, "CboHlthStat")
        Me.CboHlthStat.Name = "CboHlthStat"
        Me.TableData.SetRow(Me.CboHlthStat, 4)
        '
        'CboAddress
        '
        Me.CboAddress.CityID = 0
        Me.CboAddress.CityName = Nothing
        Me.TableData.SetColumn(Me.CboAddress, 2)
        resources.ApplyResources(Me.CboAddress, "CboAddress")
        Me.CboAddress.Name = "CboAddress"
        Me.TableData.SetRow(Me.CboAddress, 4)
        '
        'PatientIDLabel
        '
        Me.PatientIDLabel.Appearance.Font = CType(resources.GetObject("PatientIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.PatientIDLabel.Appearance.Options.UseFont = True
        Me.PatientIDLabel.Appearance.Options.UseTextOptions = True
        Me.PatientIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PatientIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PatientIDLabel, "PatientIDLabel")
        Me.PatientIDLabel.Name = "PatientIDLabel"
        '
        'PatientIDSpinEdit
        '
        resources.ApplyResources(Me.PatientIDSpinEdit, "PatientIDSpinEdit")
        Me.PatientIDSpinEdit.Name = "PatientIDSpinEdit"
        Me.PatientIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("PatientIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PatientIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.PatientIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PatientIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PatientIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'PatientNameLabel
        '
        Me.PatientNameLabel.Appearance.Font = CType(resources.GetObject("PatientNameLabel.Appearance.Font"), System.Drawing.Font)
        Me.PatientNameLabel.Appearance.Options.UseFont = True
        Me.PatientNameLabel.Appearance.Options.UseTextOptions = True
        Me.PatientNameLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PatientNameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PatientNameLabel, "PatientNameLabel")
        Me.PatientNameLabel.Name = "PatientNameLabel"
        '
        'PatientNameTextEdit
        '
        resources.ApplyResources(Me.PatientNameTextEdit, "PatientNameTextEdit")
        Me.PatientNameTextEdit.Name = "PatientNameTextEdit"
        Me.PatientNameTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PatientNameTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PatientNameTextEdit.Properties.Appearance.Options.UseFont = True
        Me.PatientNameTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PatientNameTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PatientNameTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'SexLabel
        '
        Me.SexLabel.Appearance.Font = CType(resources.GetObject("SexLabel.Appearance.Font"), System.Drawing.Font)
        Me.SexLabel.Appearance.Options.UseFont = True
        Me.SexLabel.Appearance.Options.UseTextOptions = True
        Me.SexLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SexLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.SexLabel, "SexLabel")
        Me.SexLabel.Name = "SexLabel"
        '
        'SexTextEdit
        '
        resources.ApplyResources(Me.SexTextEdit, "SexTextEdit")
        Me.SexTextEdit.Name = "SexTextEdit"
        Me.SexTextEdit.Properties.Appearance.Font = CType(resources.GetObject("SexTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SexTextEdit.Properties.Appearance.Options.UseFont = True
        Me.SexTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SexTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SexTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'AgeLabel
        '
        Me.AgeLabel.Appearance.Font = CType(resources.GetObject("AgeLabel.Appearance.Font"), System.Drawing.Font)
        Me.AgeLabel.Appearance.Options.UseFont = True
        Me.AgeLabel.Appearance.Options.UseTextOptions = True
        Me.AgeLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AgeLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.AgeLabel, "AgeLabel")
        Me.AgeLabel.Name = "AgeLabel"
        '
        'AgeSpinEdit
        '
        resources.ApplyResources(Me.AgeSpinEdit, "AgeSpinEdit")
        Me.AgeSpinEdit.Name = "AgeSpinEdit"
        Me.AgeSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("AgeSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.AgeSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.AgeSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.AgeSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AgeSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'StillKidLabel
        '
        Me.StillKidLabel.Appearance.Font = CType(resources.GetObject("StillKidLabel.Appearance.Font"), System.Drawing.Font)
        Me.StillKidLabel.Appearance.Options.UseFont = True
        Me.StillKidLabel.Appearance.Options.UseTextOptions = True
        Me.StillKidLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.StillKidLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.StillKidLabel, "StillKidLabel")
        Me.TableData.SetColumn(Me.StillKidLabel, 4)
        Me.StillKidLabel.Name = "StillKidLabel"
        Me.TableData.SetRow(Me.StillKidLabel, 3)
        '
        'PhoneLabel
        '
        Me.PhoneLabel.Appearance.Font = CType(resources.GetObject("PhoneLabel.Appearance.Font"), System.Drawing.Font)
        Me.PhoneLabel.Appearance.Options.UseFont = True
        Me.PhoneLabel.Appearance.Options.UseTextOptions = True
        Me.PhoneLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PhoneLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PhoneLabel, "PhoneLabel")
        Me.TableData.SetColumn(Me.PhoneLabel, 1)
        Me.PhoneLabel.Name = "PhoneLabel"
        Me.TableData.SetRow(Me.PhoneLabel, 2)
        '
        'PhoneTextEdit
        '
        Me.TableData.SetColumn(Me.PhoneTextEdit, 2)
        resources.ApplyResources(Me.PhoneTextEdit, "PhoneTextEdit")
        Me.PhoneTextEdit.Name = "PhoneTextEdit"
        Me.PhoneTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PhoneTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PhoneTextEdit.Properties.Appearance.Options.UseFont = True
        Me.PhoneTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PhoneTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PhoneTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.PhoneTextEdit, 2)
        '
        'AddressLabel
        '
        Me.AddressLabel.Appearance.Font = CType(resources.GetObject("AddressLabel.Appearance.Font"), System.Drawing.Font)
        Me.AddressLabel.Appearance.Options.UseFont = True
        Me.AddressLabel.Appearance.Options.UseTextOptions = True
        Me.AddressLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AddressLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.AddressLabel, "AddressLabel")
        Me.TableData.SetColumn(Me.AddressLabel, 1)
        Me.AddressLabel.Name = "AddressLabel"
        Me.TableData.SetRow(Me.AddressLabel, 4)
        '
        'AddressTextEdit
        '
        Me.TableData.SetColumn(Me.AddressTextEdit, 3)
        resources.ApplyResources(Me.AddressTextEdit, "AddressTextEdit")
        Me.AddressTextEdit.Name = "AddressTextEdit"
        Me.AddressTextEdit.Properties.Appearance.Font = CType(resources.GetObject("AddressTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.AddressTextEdit.Properties.Appearance.Options.UseFont = True
        Me.AddressTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.AddressTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AddressTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.AddressTextEdit, 4)
        '
        'HealthLabel
        '
        Me.HealthLabel.Appearance.Font = CType(resources.GetObject("HealthLabel.Appearance.Font"), System.Drawing.Font)
        Me.HealthLabel.Appearance.Options.UseFont = True
        Me.HealthLabel.Appearance.Options.UseTextOptions = True
        Me.HealthLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.HealthLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.HealthLabel, "HealthLabel")
        Me.TableData.SetColumn(Me.HealthLabel, 4)
        Me.HealthLabel.Name = "HealthLabel"
        Me.TableData.SetRow(Me.HealthLabel, 4)
        '
        'HealthTextEdit
        '
        Me.TableData.SetColumn(Me.HealthTextEdit, 6)
        resources.ApplyResources(Me.HealthTextEdit, "HealthTextEdit")
        Me.HealthTextEdit.Name = "HealthTextEdit"
        Me.HealthTextEdit.Properties.Appearance.Font = CType(resources.GetObject("HealthTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.HealthTextEdit.Properties.Appearance.Options.UseFont = True
        Me.HealthTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.HealthTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.HealthTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.HealthTextEdit, 4)
        '
        'TreatLabel
        '
        Me.TreatLabel.Appearance.Font = CType(resources.GetObject("TreatLabel.Appearance.Font"), System.Drawing.Font)
        Me.TreatLabel.Appearance.Options.UseFont = True
        Me.TreatLabel.Appearance.Options.UseTextOptions = True
        Me.TreatLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TreatLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.TreatLabel, "TreatLabel")
        Me.TableData.SetColumn(Me.TreatLabel, 1)
        Me.TreatLabel.Name = "TreatLabel"
        Me.TableData.SetRow(Me.TreatLabel, 5)
        '
        'TreatCheckEdit
        '
        Me.TableData.SetColumn(Me.TreatCheckEdit, 2)
        resources.ApplyResources(Me.TreatCheckEdit, "TreatCheckEdit")
        Me.TreatCheckEdit.Name = "TreatCheckEdit"
        Me.TreatCheckEdit.Properties.Appearance.Font = CType(resources.GetObject("TreatCheckEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TreatCheckEdit.Properties.Appearance.Options.UseFont = True
        Me.TreatCheckEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.TreatCheckEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TreatCheckEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TreatCheckEdit.Properties.Caption = resources.GetString("TreatCheckEdit.Properties.Caption")
        Me.TableData.SetRow(Me.TreatCheckEdit, 5)
        '
        'MobileLabel
        '
        Me.MobileLabel.Appearance.Font = CType(resources.GetObject("MobileLabel.Appearance.Font"), System.Drawing.Font)
        Me.MobileLabel.Appearance.Options.UseFont = True
        Me.MobileLabel.Appearance.Options.UseTextOptions = True
        Me.MobileLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MobileLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.MobileLabel, "MobileLabel")
        Me.TableData.SetColumn(Me.MobileLabel, 1)
        Me.MobileLabel.Name = "MobileLabel"
        Me.TableData.SetRow(Me.MobileLabel, 6)
        '
        'DiagCheckEdit
        '
        Me.TableData.SetColumn(Me.DiagCheckEdit, 2)
        resources.ApplyResources(Me.DiagCheckEdit, "DiagCheckEdit")
        Me.DiagCheckEdit.Name = "DiagCheckEdit"
        Me.DiagCheckEdit.Properties.Appearance.Font = CType(resources.GetObject("DiagCheckEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DiagCheckEdit.Properties.Appearance.Options.UseFont = True
        Me.DiagCheckEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DiagCheckEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DiagCheckEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DiagCheckEdit.Properties.Caption = resources.GetString("DiagCheckEdit.Properties.Caption")
        Me.TableData.SetRow(Me.DiagCheckEdit, 6)
        '
        'OrthoLabel
        '
        Me.OrthoLabel.Appearance.Font = CType(resources.GetObject("OrthoLabel.Appearance.Font"), System.Drawing.Font)
        Me.OrthoLabel.Appearance.Options.UseFont = True
        Me.OrthoLabel.Appearance.Options.UseTextOptions = True
        Me.OrthoLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.OrthoLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.OrthoLabel, "OrthoLabel")
        Me.TableData.SetColumn(Me.OrthoLabel, 4)
        Me.OrthoLabel.Name = "OrthoLabel"
        Me.TableData.SetRow(Me.OrthoLabel, 6)
        '
        'OrthoCheckEdit
        '
        Me.TableData.SetColumn(Me.OrthoCheckEdit, 5)
        resources.ApplyResources(Me.OrthoCheckEdit, "OrthoCheckEdit")
        Me.OrthoCheckEdit.Name = "OrthoCheckEdit"
        Me.OrthoCheckEdit.Properties.Appearance.Font = CType(resources.GetObject("OrthoCheckEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.OrthoCheckEdit.Properties.Appearance.Options.UseFont = True
        Me.OrthoCheckEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.OrthoCheckEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.OrthoCheckEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.OrthoCheckEdit.Properties.Caption = resources.GetString("OrthoCheckEdit.Properties.Caption")
        Me.TableData.SetRow(Me.OrthoCheckEdit, 6)
        '
        'NotesLabel
        '
        Me.NotesLabel.Appearance.Font = CType(resources.GetObject("NotesLabel.Appearance.Font"), System.Drawing.Font)
        Me.NotesLabel.Appearance.Options.UseFont = True
        Me.NotesLabel.Appearance.Options.UseTextOptions = True
        Me.NotesLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NotesLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.NotesLabel, "NotesLabel")
        Me.TableData.SetColumn(Me.NotesLabel, 1)
        Me.NotesLabel.Name = "NotesLabel"
        Me.TableData.SetRow(Me.NotesLabel, 7)
        '
        'NotesTextEdit
        '
        Me.TableData.SetColumn(Me.NotesTextEdit, 2)
        Me.TableData.SetColumnSpan(Me.NotesTextEdit, 4)
        resources.ApplyResources(Me.NotesTextEdit, "NotesTextEdit")
        Me.NotesTextEdit.Name = "NotesTextEdit"
        Me.NotesTextEdit.Properties.Appearance.Font = CType(resources.GetObject("NotesTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.NotesTextEdit.Properties.Appearance.Options.UseFont = True
        Me.NotesTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.NotesTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NotesTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.NotesTextEdit, 7)
        '
        'TLPBut
        '
        Me.TLPBut.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!)})
        Me.TLPBut.Controls.Add(Me.butApply)
        Me.TLPBut.Controls.Add(Me.butCancel)
        Me.TLPBut.Controls.Add(Me.butClose)
        resources.ApplyResources(Me.TLPBut, "TLPBut")
        Me.TLPBut.Name = "TLPBut"
        Me.TLPBut.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TLPBut.UseSkinIndents = True
        '
        'butApply
        '
        Me.butApply.Appearance.Font = CType(resources.GetObject("butApply.Appearance.Font"), System.Drawing.Font)
        Me.butApply.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.butApply, 1)
        resources.ApplyResources(Me.butApply, "butApply")
        Me.butApply.Name = "butApply"
        Me.TLPBut.SetRow(Me.butApply, 0)
        '
        'butCancel
        '
        Me.butCancel.Appearance.Font = CType(resources.GetObject("butCancel.Appearance.Font"), System.Drawing.Font)
        Me.butCancel.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.butCancel, 3)
        resources.ApplyResources(Me.butCancel, "butCancel")
        Me.butCancel.Name = "butCancel"
        Me.TLPBut.SetRow(Me.butCancel, 0)
        '
        'butClose
        '
        Me.butClose.Appearance.Font = CType(resources.GetObject("butClose.Appearance.Font"), System.Drawing.Font)
        Me.butClose.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.butClose, 5)
        resources.ApplyResources(Me.butClose, "butClose")
        Me.butClose.Name = "butClose"
        Me.TLPBut.SetRow(Me.butClose, 0)
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.TableData.SetColumn(Me.LabelControl1, 1)
        Me.LabelControl1.Name = "LabelControl1"
        Me.TableData.SetRow(Me.LabelControl1, 3)
        '
        'lblWhats
        '
        Me.lblWhats.Appearance.Font = CType(resources.GetObject("lblWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblWhats.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblWhats.Appearance.Options.UseFont = True
        Me.lblWhats.Appearance.Options.UseForeColor = True
        Me.TableData.SetColumn(Me.lblWhats, 6)
        resources.ApplyResources(Me.lblWhats, "lblWhats")
        Me.lblWhats.Name = "lblWhats"
        Me.TableData.SetRow(Me.lblWhats, 3)
        '
        'FrmPatient
        '
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ribbonStatusBar)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "FrmPatient"
        Me.Ribbon = Me.ribbonControl
        Me.StatusBar = Me.ribbonStatusBar
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel1.ResumeLayout(False)
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel2.ResumeLayout(False)
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.ResumeLayout(False)
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ribbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableData.ResumeLayout(False)
        Me.TableData.PerformLayout()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboSex.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SexTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AgeSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AddressTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HealthTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreatCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DiagCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OrthoCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NotesTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLPBut.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private WithEvents DGV As DevExpress.XtraGrid.GridControl
		Private WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
		Private WithEvents ribbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
		Private WithEvents ribbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
		Private WithEvents ribbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
		Private WithEvents bbiPrintPreview As DevExpress.XtraBars.BarButtonItem
		Private WithEvents ribbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
		Private WithEvents ribbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
		Private WithEvents bsiRecordsCount As DevExpress.XtraBars.BarStaticItem
		Private WithEvents bAdd As DevExpress.XtraBars.BarButtonItem
		Private WithEvents bEdit As DevExpress.XtraBars.BarButtonItem
		Private WithEvents bDelete As DevExpress.XtraBars.BarButtonItem
		Private WithEvents bRefresh As DevExpress.XtraBars.BarButtonItem
		Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents Splitter1 As DevExpress.XtraEditors.SplitContainerControl
		Friend WithEvents bCancel As DevExpress.XtraBars.BarButtonItem
		Friend WithEvents TableData As DevExpress.Utils.Layout.TablePanel
		Friend WithEvents TLPBut As DevExpress.Utils.Layout.TablePanel
		Friend WithEvents butApply As DevExpress.XtraEditors.SimpleButton
		Friend WithEvents butCancel As DevExpress.XtraEditors.SimpleButton
		Friend WithEvents butClose As DevExpress.XtraEditors.SimpleButton
		Friend WithEvents PatientIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents PatientIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PatientNameTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents PatientNameLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPatientName As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents SexTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents SexLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colSex As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents AgeSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents AgeLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colAge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents StillKidLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colStillKid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PhoneTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PhoneLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colPhone As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AddressTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents AddressLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colAddress As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents HealthTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents HealthLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colHealth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TreatCheckEdit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TreatLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colTreat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colImplant As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DiagCheckEdit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents MobileLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colMobile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents OrthoLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colOrtho As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStruc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents NotesTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents NotesLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBirthY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCreatedBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCreateDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CboHlthStat As HlthCombo
    Friend WithEvents CboAddress As CityCombo
    Friend WithEvents CboSex As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents colPatientNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiag As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents OrthoCheckEdit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents colWhatsApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
    Friend WithEvents colWhatsAppPrefix As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
End Class
