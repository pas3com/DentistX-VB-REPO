<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLab
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLab))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLabID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLabName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAdres = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPhone = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMobile = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.lblLabWhats = New DevExpress.XtraEditors.LabelControl()
        Me.labWhatsTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.labWhatsLabel = New DevExpress.XtraEditors.LabelControl()
        Me.labWhatsPrefixCombo = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.labWhatsPrefixLabel = New DevExpress.XtraEditors.LabelControl()
        Me.LabIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.LabIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.LabNameLabel = New DevExpress.XtraEditors.LabelControl()
        Me.LabNameTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.AdresLabel = New DevExpress.XtraEditors.LabelControl()
        Me.AdresTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.PhoneLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PhoneTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.MobileLabel = New DevExpress.XtraEditors.LabelControl()
        Me.MobileTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.TLPBut = New DevExpress.Utils.Layout.TablePanel()
        Me.butApply = New DevExpress.XtraEditors.SimpleButton()
        Me.butCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.colWhatsApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWhatsPrefix = New DevExpress.XtraGrid.Columns.GridColumn()
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
        CType(Me.labWhatsTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.labWhatsPrefixCombo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AdresTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MobileTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Splitter1.SplitterPosition = 293
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colLabID, Me.colLabName, Me.colAdres, Me.colPhone, Me.colMobile, Me.colWhatsPrefix, Me.colWhatsApp})
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
        'colLabID
        '
        Me.colLabID.FieldName = "LabID"
        Me.colLabID.Name = "colLabID"
        '
        'colLabName
        '
        Me.colLabName.FieldName = "LabName"
        Me.colLabName.Name = "colLabName"
        resources.ApplyResources(Me.colLabName, "colLabName")
        '
        'colAdres
        '
        Me.colAdres.FieldName = "Adres"
        Me.colAdres.Name = "colAdres"
        resources.ApplyResources(Me.colAdres, "colAdres")
        '
        'colPhone
        '
        Me.colPhone.FieldName = "Phone"
        Me.colPhone.Name = "colPhone"
        resources.ApplyResources(Me.colPhone, "colPhone")
        '
        'colMobile
        '
        Me.colMobile.FieldName = "Mobile"
        Me.colMobile.Name = "colMobile"
        resources.ApplyResources(Me.colMobile, "colMobile")
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
        Me.TableData.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.0!)})
        Me.TableData.Controls.Add(Me.lblLabWhats)
        Me.TableData.Controls.Add(Me.labWhatsTextEdit)
        Me.TableData.Controls.Add(Me.labWhatsLabel)
        Me.TableData.Controls.Add(Me.labWhatsPrefixCombo)
        Me.TableData.Controls.Add(Me.labWhatsPrefixLabel)
        Me.TableData.Controls.Add(Me.LabIDLabel)
        Me.TableData.Controls.Add(Me.LabIDSpinEdit)
        Me.TableData.Controls.Add(Me.LabNameLabel)
        Me.TableData.Controls.Add(Me.LabNameTextEdit)
        Me.TableData.Controls.Add(Me.AdresLabel)
        Me.TableData.Controls.Add(Me.AdresTextEdit)
        Me.TableData.Controls.Add(Me.PhoneLabel)
        Me.TableData.Controls.Add(Me.PhoneTextEdit)
        Me.TableData.Controls.Add(Me.MobileLabel)
        Me.TableData.Controls.Add(Me.MobileTextEdit)
        Me.TableData.Controls.Add(Me.TLPBut)
        resources.ApplyResources(Me.TableData, "TableData")
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TableData.UseSkinIndents = True
        '
        'lblLabWhats
        '
        Me.lblLabWhats.Appearance.Font = CType(resources.GetObject("lblLabWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblLabWhats.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblLabWhats.Appearance.Options.UseFont = True
        Me.lblLabWhats.Appearance.Options.UseForeColor = True
        Me.lblLabWhats.Appearance.Options.UseTextOptions = True
        Me.lblLabWhats.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblLabWhats, "lblLabWhats")
        Me.TableData.SetColumn(Me.lblLabWhats, 6)
        Me.lblLabWhats.Name = "lblLabWhats"
        Me.TableData.SetRow(Me.lblLabWhats, 3)
        '
        'labWhatsTextEdit
        '
        Me.TableData.SetColumn(Me.labWhatsTextEdit, 5)
        resources.ApplyResources(Me.labWhatsTextEdit, "labWhatsTextEdit")
        Me.labWhatsTextEdit.Name = "labWhatsTextEdit"
        Me.labWhatsTextEdit.Properties.Appearance.Font = CType(resources.GetObject("labWhatsTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.labWhatsTextEdit.Properties.Appearance.Options.UseFont = True
        Me.labWhatsTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.labWhatsTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.labWhatsTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.labWhatsTextEdit.Properties.MaxLength = 10
        Me.TableData.SetRow(Me.labWhatsTextEdit, 3)
        '
        'labWhatsLabel
        '
        Me.labWhatsLabel.Appearance.Font = CType(resources.GetObject("labWhatsLabel.Appearance.Font"), System.Drawing.Font)
        Me.labWhatsLabel.Appearance.Options.UseFont = True
        Me.labWhatsLabel.Appearance.Options.UseTextOptions = True
        Me.labWhatsLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.labWhatsLabel, "labWhatsLabel")
        Me.TableData.SetColumn(Me.labWhatsLabel, 4)
        Me.labWhatsLabel.Name = "labWhatsLabel"
        Me.TableData.SetRow(Me.labWhatsLabel, 3)
        '
        'labWhatsPrefixCombo
        '
        Me.TableData.SetColumn(Me.labWhatsPrefixCombo, 2)
        resources.ApplyResources(Me.labWhatsPrefixCombo, "labWhatsPrefixCombo")
        Me.labWhatsPrefixCombo.Name = "labWhatsPrefixCombo"
        Me.labWhatsPrefixCombo.Properties.Appearance.Font = CType(resources.GetObject("labWhatsPrefixCombo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.labWhatsPrefixCombo.Properties.Appearance.Options.UseFont = True
        Me.labWhatsPrefixCombo.Properties.AppearanceDropDown.Font = CType(resources.GetObject("labWhatsPrefixCombo.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.labWhatsPrefixCombo.Properties.AppearanceDropDown.Options.UseFont = True
        Me.labWhatsPrefixCombo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("labWhatsPrefixCombo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.labWhatsPrefixCombo, 3)
        '
        'labWhatsPrefixLabel
        '
        Me.labWhatsPrefixLabel.Appearance.Font = CType(resources.GetObject("labWhatsPrefixLabel.Appearance.Font"), System.Drawing.Font)
        Me.labWhatsPrefixLabel.Appearance.Options.UseFont = True
        Me.labWhatsPrefixLabel.Appearance.Options.UseTextOptions = True
        Me.labWhatsPrefixLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.labWhatsPrefixLabel, "labWhatsPrefixLabel")
        Me.TableData.SetColumn(Me.labWhatsPrefixLabel, 1)
        Me.labWhatsPrefixLabel.Name = "labWhatsPrefixLabel"
        Me.TableData.SetRow(Me.labWhatsPrefixLabel, 3)
        '
        'LabIDLabel
        '
        Me.LabIDLabel.Appearance.Font = CType(resources.GetObject("LabIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.LabIDLabel.Appearance.Options.UseFont = True
        Me.LabIDLabel.Appearance.Options.UseTextOptions = True
        Me.LabIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabIDLabel, "LabIDLabel")
        Me.TableData.SetColumn(Me.LabIDLabel, 1)
        Me.LabIDLabel.Name = "LabIDLabel"
        Me.TableData.SetRow(Me.LabIDLabel, 0)
        '
        'LabIDSpinEdit
        '
        Me.TableData.SetColumn(Me.LabIDSpinEdit, 2)
        resources.ApplyResources(Me.LabIDSpinEdit, "LabIDSpinEdit")
        Me.LabIDSpinEdit.Name = "LabIDSpinEdit"
        Me.LabIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("LabIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.LabIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.LabIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.LabIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.LabIDSpinEdit, 0)
        '
        'LabNameLabel
        '
        Me.LabNameLabel.Appearance.Font = CType(resources.GetObject("LabNameLabel.Appearance.Font"), System.Drawing.Font)
        Me.LabNameLabel.Appearance.Options.UseFont = True
        Me.LabNameLabel.Appearance.Options.UseTextOptions = True
        Me.LabNameLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabNameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabNameLabel, "LabNameLabel")
        Me.TableData.SetColumn(Me.LabNameLabel, 4)
        Me.LabNameLabel.Name = "LabNameLabel"
        Me.TableData.SetRow(Me.LabNameLabel, 0)
        '
        'LabNameTextEdit
        '
        Me.TableData.SetColumn(Me.LabNameTextEdit, 5)
        resources.ApplyResources(Me.LabNameTextEdit, "LabNameTextEdit")
        Me.LabNameTextEdit.Name = "LabNameTextEdit"
        Me.LabNameTextEdit.Properties.Appearance.Font = CType(resources.GetObject("LabNameTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.LabNameTextEdit.Properties.Appearance.Options.UseFont = True
        Me.LabNameTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.LabNameTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabNameTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.LabNameTextEdit, 0)
        '
        'AdresLabel
        '
        Me.AdresLabel.Appearance.Font = CType(resources.GetObject("AdresLabel.Appearance.Font"), System.Drawing.Font)
        Me.AdresLabel.Appearance.Options.UseFont = True
        Me.AdresLabel.Appearance.Options.UseTextOptions = True
        Me.AdresLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AdresLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.AdresLabel, "AdresLabel")
        Me.TableData.SetColumn(Me.AdresLabel, 1)
        Me.AdresLabel.Name = "AdresLabel"
        Me.TableData.SetRow(Me.AdresLabel, 1)
        '
        'AdresTextEdit
        '
        Me.TableData.SetColumn(Me.AdresTextEdit, 2)
        Me.TableData.SetColumnSpan(Me.AdresTextEdit, 4)
        resources.ApplyResources(Me.AdresTextEdit, "AdresTextEdit")
        Me.AdresTextEdit.Name = "AdresTextEdit"
        Me.AdresTextEdit.Properties.Appearance.Font = CType(resources.GetObject("AdresTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.AdresTextEdit.Properties.Appearance.Options.UseFont = True
        Me.AdresTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.AdresTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AdresTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.AdresTextEdit, 1)
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
        'MobileLabel
        '
        Me.MobileLabel.Appearance.Font = CType(resources.GetObject("MobileLabel.Appearance.Font"), System.Drawing.Font)
        Me.MobileLabel.Appearance.Options.UseFont = True
        Me.MobileLabel.Appearance.Options.UseTextOptions = True
        Me.MobileLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MobileLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.MobileLabel, "MobileLabel")
        Me.TableData.SetColumn(Me.MobileLabel, 4)
        Me.MobileLabel.Name = "MobileLabel"
        Me.TableData.SetRow(Me.MobileLabel, 2)
        '
        'MobileTextEdit
        '
        Me.TableData.SetColumn(Me.MobileTextEdit, 5)
        resources.ApplyResources(Me.MobileTextEdit, "MobileTextEdit")
        Me.MobileTextEdit.Name = "MobileTextEdit"
        Me.MobileTextEdit.Properties.Appearance.Font = CType(resources.GetObject("MobileTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.MobileTextEdit.Properties.Appearance.Options.UseFont = True
        Me.MobileTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.MobileTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MobileTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.MobileTextEdit, 2)
        '
        'TLPBut
        '
        Me.TableData.SetColumn(Me.TLPBut, 0)
        Me.TLPBut.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!)})
        Me.TableData.SetColumnSpan(Me.TLPBut, 7)
        Me.TLPBut.Controls.Add(Me.butApply)
        Me.TLPBut.Controls.Add(Me.butCancel)
        Me.TLPBut.Controls.Add(Me.butClose)
        resources.ApplyResources(Me.TLPBut, "TLPBut")
        Me.TLPBut.Name = "TLPBut"
        Me.TableData.SetRow(Me.TLPBut, 4)
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
        'colWhatsApp
        '
        resources.ApplyResources(Me.colWhatsApp, "colWhatsApp")
        Me.colWhatsApp.FieldName = "WhatsApp"
        Me.colWhatsApp.Name = "colWhatsApp"
        '
        'colWhatsPrefix
        '
        resources.ApplyResources(Me.colWhatsPrefix, "colWhatsPrefix")
        Me.colWhatsPrefix.FieldName = "WhatsAppPrefix"
        Me.colWhatsPrefix.Name = "colWhatsPrefix"
        '
        'FrmLab
        '
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ribbonStatusBar)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "FrmLab"
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
        CType(Me.labWhatsTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.labWhatsPrefixCombo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AdresTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MobileTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
		Friend WithEvents LabIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents LabIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colLabID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents LabNameTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents LabNameLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colLabName As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents AdresTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents AdresLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colAdres As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PhoneTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents PhoneLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPhone As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents MobileTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents MobileLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colMobile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents labWhatsPrefixLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents labWhatsPrefixCombo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents labWhatsLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents labWhatsTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblLabWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colWhatsPrefix As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colWhatsApp As DevExpress.XtraGrid.Columns.GridColumn
End Class
