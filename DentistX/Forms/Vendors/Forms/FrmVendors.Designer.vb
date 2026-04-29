<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmVendors
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmVendors))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVendID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVendName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCityID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVendAddress = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colContacts = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.VendIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.VendIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.VendNameLabel = New DevExpress.XtraEditors.LabelControl()
        Me.VendNameTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.CityIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.CityCombo1 = New DentistX.CityCombo()
        Me.VendAddressLabel = New DevExpress.XtraEditors.LabelControl()
        Me.VendAddressTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.ContactsLabel = New DevExpress.XtraEditors.LabelControl()
        Me.ContactsTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.TLPBut = New DevExpress.Utils.Layout.TablePanel()
        Me.butApply = New DevExpress.XtraEditors.SimpleButton()
        Me.butCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
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
        CType(Me.VendIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VendNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VendAddressTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContactsTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLPBut.SuspendLayout()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.CaptionImageOptions.ImageKey = resources.GetString("Splitter1.CaptionImageOptions.ImageKey")
        Me.Splitter1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        Me.Splitter1.Horizontal = False
        Me.Splitter1.Name = "Splitter1"
        '
        'Splitter1.Panel1
        '
        resources.ApplyResources(Me.Splitter1.Panel1, "Splitter1.Panel1")
        Me.Splitter1.Panel1.Controls.Add(Me.DGV)
        '
        'Splitter1.Panel2
        '
        resources.ApplyResources(Me.Splitter1.Panel2, "Splitter1.Panel2")
        Me.Splitter1.Panel2.Controls.Add(Me.TableData)
        Me.Splitter1.SplitterPosition = 293
        '
        'DGV
        '
        resources.ApplyResources(Me.DGV, "DGV")
        Me.DGV.EmbeddedNavigator.AccessibleDescription = resources.GetString("DGV.EmbeddedNavigator.AccessibleDescription")
        Me.DGV.EmbeddedNavigator.AccessibleName = resources.GetString("DGV.EmbeddedNavigator.AccessibleName")
        Me.DGV.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("DGV.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.DGV.EmbeddedNavigator.Anchor = CType(resources.GetObject("DGV.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.DGV.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.DGV.EmbeddedNavigator.AutoSize = CType(resources.GetObject("DGV.EmbeddedNavigator.AutoSize"), Boolean)
        Me.DGV.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("DGV.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.DGV.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("DGV.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.DGV.EmbeddedNavigator.Buttons.Append.Hint = resources.GetString("DGV.EmbeddedNavigator.Buttons.Append.Hint")
        Me.DGV.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.CancelEdit.Hint = resources.GetString("DGV.EmbeddedNavigator.Buttons.CancelEdit.Hint")
        Me.DGV.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Edit.Hint = resources.GetString("DGV.EmbeddedNavigator.Buttons.Edit.Hint")
        Me.DGV.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.EndEdit.Hint = resources.GetString("DGV.EmbeddedNavigator.Buttons.EndEdit.Hint")
        Me.DGV.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Remove.Hint = resources.GetString("DGV.EmbeddedNavigator.Buttons.Remove.Hint")
        Me.DGV.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.DGV.EmbeddedNavigator.ImeMode = CType(resources.GetObject("DGV.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.DGV.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("DGV.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.DGV.EmbeddedNavigator.TextLocation = CType(resources.GetObject("DGV.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.DGV.EmbeddedNavigator.ToolTip = resources.GetString("DGV.EmbeddedNavigator.ToolTip")
        Me.DGV.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("DGV.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.DGV.EmbeddedNavigator.ToolTipTitle = resources.GetString("DGV.EmbeddedNavigator.ToolTipTitle")
        Me.DGV.MainView = Me.dgView
        Me.DGV.MenuManager = Me.ribbonControl
        Me.DGV.Name = "DGV"
        Me.DGV.UseEmbeddedNavigator = True
        Me.DGV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        resources.ApplyResources(Me.dgView, "dgView")
        Me.dgView.Appearance.HeaderPanel.Font = CType(resources.GetObject("dgView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
        Me.dgView.Appearance.Row.Font = CType(resources.GetObject("dgView.Appearance.Row.Font"), System.Drawing.Font)
        Me.dgView.Appearance.Row.Options.UseFont = True
        Me.dgView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colVendID, Me.colVendName, Me.colCityID, Me.colVendAddress, Me.colContacts})
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
        Me.colRowNum.ImageOptions.ImageKey = resources.GetString("colRowNum.ImageOptions.ImageKey")
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colVendID
        '
        resources.ApplyResources(Me.colVendID, "colVendID")
        Me.colVendID.FieldName = "VendID"
        Me.colVendID.ImageOptions.ImageKey = resources.GetString("colVendID.ImageOptions.ImageKey")
        Me.colVendID.Name = "colVendID"
        '
        'colVendName
        '
        resources.ApplyResources(Me.colVendName, "colVendName")
        Me.colVendName.FieldName = "VendName"
        Me.colVendName.ImageOptions.ImageKey = resources.GetString("colVendName.ImageOptions.ImageKey")
        Me.colVendName.Name = "colVendName"
        Me.colVendName.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colCityID
        '
        resources.ApplyResources(Me.colCityID, "colCityID")
        Me.colCityID.FieldName = "CityID"
        Me.colCityID.ImageOptions.ImageKey = resources.GetString("colCityID.ImageOptions.ImageKey")
        Me.colCityID.Name = "colCityID"
        '
        'colVendAddress
        '
        resources.ApplyResources(Me.colVendAddress, "colVendAddress")
        Me.colVendAddress.FieldName = "VendAddress"
        Me.colVendAddress.ImageOptions.ImageKey = resources.GetString("colVendAddress.ImageOptions.ImageKey")
        Me.colVendAddress.Name = "colVendAddress"
        Me.colVendAddress.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colContacts
        '
        resources.ApplyResources(Me.colContacts, "colContacts")
        Me.colContacts.FieldName = "Contacts"
        Me.colContacts.ImageOptions.ImageKey = resources.GetString("colContacts.ImageOptions.ImageKey")
        Me.colContacts.Name = "colContacts"
        Me.colContacts.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'ribbonControl
        '
        resources.ApplyResources(Me.ribbonControl, "ribbonControl")
        Me.ribbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(30, 32, 30, 32)
        Me.ribbonControl.ExpandCollapseItem.Id = 0
        Me.ribbonControl.ExpandCollapseItem.ImageOptions.ImageIndex = CType(resources.GetObject("ribbonControl.ExpandCollapseItem.ImageOptions.ImageIndex"), Integer)
        Me.ribbonControl.ExpandCollapseItem.ImageOptions.ImageKey = resources.GetString("ribbonControl.ExpandCollapseItem.ImageOptions.ImageKey")
        Me.ribbonControl.ExpandCollapseItem.ImageOptions.LargeImageIndex = CType(resources.GetObject("ribbonControl.ExpandCollapseItem.ImageOptions.LargeImageIndex"), Integer)
        Me.ribbonControl.ExpandCollapseItem.ImageOptions.LargeImageKey = resources.GetString("ribbonControl.ExpandCollapseItem.ImageOptions.LargeImageKey")
        Me.ribbonControl.ExpandCollapseItem.ImageOptions.SvgImage = CType(resources.GetObject("ribbonControl.ExpandCollapseItem.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.ribbonControl.ExpandCollapseItem.SearchTags = resources.GetString("ribbonControl.ExpandCollapseItem.SearchTags")
        Me.ribbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.ribbonControl.ExpandCollapseItem, Me.bbiPrintPreview, Me.bsiRecordsCount, Me.bAdd, Me.bEdit, Me.bDelete, Me.bRefresh, Me.bCancel})
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
        Me.bbiPrintPreview.ImageOptions.ImageIndex = CType(resources.GetObject("bbiPrintPreview.ImageOptions.ImageIndex"), Integer)
        Me.bbiPrintPreview.ImageOptions.ImageKey = resources.GetString("bbiPrintPreview.ImageOptions.ImageKey")
        Me.bbiPrintPreview.ImageOptions.ImageUri.Uri = "Preview"
        Me.bbiPrintPreview.ImageOptions.LargeImageIndex = CType(resources.GetObject("bbiPrintPreview.ImageOptions.LargeImageIndex"), Integer)
        Me.bbiPrintPreview.ImageOptions.LargeImageKey = resources.GetString("bbiPrintPreview.ImageOptions.LargeImageKey")
        Me.bbiPrintPreview.ImageOptions.SvgImage = CType(resources.GetObject("bbiPrintPreview.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.bbiPrintPreview.ItemAppearance.Normal.Font = CType(resources.GetObject("bbiPrintPreview.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bbiPrintPreview.ItemAppearance.Normal.Options.UseFont = True
        Me.bbiPrintPreview.Name = "bbiPrintPreview"
        '
        'bsiRecordsCount
        '
        resources.ApplyResources(Me.bsiRecordsCount, "bsiRecordsCount")
        Me.bsiRecordsCount.Id = 15
        Me.bsiRecordsCount.ImageOptions.ImageIndex = CType(resources.GetObject("bsiRecordsCount.ImageOptions.ImageIndex"), Integer)
        Me.bsiRecordsCount.ImageOptions.ImageKey = resources.GetString("bsiRecordsCount.ImageOptions.ImageKey")
        Me.bsiRecordsCount.ImageOptions.LargeImageIndex = CType(resources.GetObject("bsiRecordsCount.ImageOptions.LargeImageIndex"), Integer)
        Me.bsiRecordsCount.ImageOptions.LargeImageKey = resources.GetString("bsiRecordsCount.ImageOptions.LargeImageKey")
        Me.bsiRecordsCount.ImageOptions.SvgImage = CType(resources.GetObject("bsiRecordsCount.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.bsiRecordsCount.Name = "bsiRecordsCount"
        '
        'bAdd
        '
        resources.ApplyResources(Me.bAdd, "bAdd")
        Me.bAdd.Id = 16
        Me.bAdd.ImageOptions.ImageIndex = CType(resources.GetObject("bAdd.ImageOptions.ImageIndex"), Integer)
        Me.bAdd.ImageOptions.ImageKey = resources.GetString("bAdd.ImageOptions.ImageKey")
        Me.bAdd.ImageOptions.ImageUri.Uri = "New"
        Me.bAdd.ImageOptions.LargeImageIndex = CType(resources.GetObject("bAdd.ImageOptions.LargeImageIndex"), Integer)
        Me.bAdd.ImageOptions.LargeImageKey = resources.GetString("bAdd.ImageOptions.LargeImageKey")
        Me.bAdd.ImageOptions.SvgImage = CType(resources.GetObject("bAdd.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.bAdd.ItemAppearance.Normal.Font = CType(resources.GetObject("bAdd.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bAdd.ItemAppearance.Normal.Options.UseFont = True
        Me.bAdd.Name = "bAdd"
        '
        'bEdit
        '
        resources.ApplyResources(Me.bEdit, "bEdit")
        Me.bEdit.Id = 17
        Me.bEdit.ImageOptions.ImageIndex = CType(resources.GetObject("bEdit.ImageOptions.ImageIndex"), Integer)
        Me.bEdit.ImageOptions.ImageKey = resources.GetString("bEdit.ImageOptions.ImageKey")
        Me.bEdit.ImageOptions.ImageUri.Uri = "Edit"
        Me.bEdit.ImageOptions.LargeImageIndex = CType(resources.GetObject("bEdit.ImageOptions.LargeImageIndex"), Integer)
        Me.bEdit.ImageOptions.LargeImageKey = resources.GetString("bEdit.ImageOptions.LargeImageKey")
        Me.bEdit.ImageOptions.SvgImage = CType(resources.GetObject("bEdit.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.bEdit.ItemAppearance.Normal.Font = CType(resources.GetObject("bEdit.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bEdit.ItemAppearance.Normal.Options.UseFont = True
        Me.bEdit.Name = "bEdit"
        '
        'bDelete
        '
        resources.ApplyResources(Me.bDelete, "bDelete")
        Me.bDelete.Id = 18
        Me.bDelete.ImageOptions.ImageIndex = CType(resources.GetObject("bDelete.ImageOptions.ImageIndex"), Integer)
        Me.bDelete.ImageOptions.ImageKey = resources.GetString("bDelete.ImageOptions.ImageKey")
        Me.bDelete.ImageOptions.ImageUri.Uri = "Delete"
        Me.bDelete.ImageOptions.LargeImageIndex = CType(resources.GetObject("bDelete.ImageOptions.LargeImageIndex"), Integer)
        Me.bDelete.ImageOptions.LargeImageKey = resources.GetString("bDelete.ImageOptions.LargeImageKey")
        Me.bDelete.ImageOptions.SvgImage = CType(resources.GetObject("bDelete.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.bDelete.ItemAppearance.Normal.Font = CType(resources.GetObject("bDelete.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bDelete.ItemAppearance.Normal.Options.UseFont = True
        Me.bDelete.Name = "bDelete"
        '
        'bRefresh
        '
        resources.ApplyResources(Me.bRefresh, "bRefresh")
        Me.bRefresh.Id = 19
        Me.bRefresh.ImageOptions.ImageIndex = CType(resources.GetObject("bRefresh.ImageOptions.ImageIndex"), Integer)
        Me.bRefresh.ImageOptions.ImageKey = resources.GetString("bRefresh.ImageOptions.ImageKey")
        Me.bRefresh.ImageOptions.ImageUri.Uri = "Refresh"
        Me.bRefresh.ImageOptions.LargeImageIndex = CType(resources.GetObject("bRefresh.ImageOptions.LargeImageIndex"), Integer)
        Me.bRefresh.ImageOptions.LargeImageKey = resources.GetString("bRefresh.ImageOptions.LargeImageKey")
        Me.bRefresh.ImageOptions.SvgImage = CType(resources.GetObject("bRefresh.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.bRefresh.ItemAppearance.Normal.Font = CType(resources.GetObject("bRefresh.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bRefresh.ItemAppearance.Normal.Options.UseFont = True
        Me.bRefresh.Name = "bRefresh"
        '
        'bCancel
        '
        resources.ApplyResources(Me.bCancel, "bCancel")
        Me.bCancel.Id = 20
        Me.bCancel.ImageOptions.ImageIndex = CType(resources.GetObject("bCancel.ImageOptions.ImageIndex"), Integer)
        Me.bCancel.ImageOptions.ImageKey = resources.GetString("bCancel.ImageOptions.ImageKey")
        Me.bCancel.ImageOptions.LargeImageIndex = CType(resources.GetObject("bCancel.ImageOptions.LargeImageIndex"), Integer)
        Me.bCancel.ImageOptions.LargeImageKey = resources.GetString("bCancel.ImageOptions.LargeImageKey")
        Me.bCancel.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.undo
        Me.bCancel.ItemAppearance.Normal.Font = CType(resources.GetObject("bCancel.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.bCancel.ItemAppearance.Normal.Options.UseFont = True
        Me.bCancel.Name = "bCancel"
        '
        'ribbonPage1
        '
        resources.ApplyResources(Me.ribbonPage1, "ribbonPage1")
        Me.ribbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.ribbonPageGroup1, Me.ribbonPageGroup2})
        Me.ribbonPage1.ImageOptions.ImageKey = resources.GetString("ribbonPage1.ImageOptions.ImageKey")
        Me.ribbonPage1.MergeOrder = 0
        Me.ribbonPage1.Name = "ribbonPage1"
        '
        'ribbonPageGroup1
        '
        resources.ApplyResources(Me.ribbonPageGroup1, "ribbonPageGroup1")
        Me.ribbonPageGroup1.AllowTextClipping = False
        Me.ribbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribbonPageGroup1.ImageOptions.ImageKey = resources.GetString("ribbonPageGroup1.ImageOptions.ImageKey")
        Me.ribbonPageGroup1.ItemLinks.Add(Me.bAdd)
        Me.ribbonPageGroup1.ItemLinks.Add(Me.bEdit)
        Me.ribbonPageGroup1.ItemLinks.Add(Me.bDelete)
        Me.ribbonPageGroup1.ItemLinks.Add(Me.bRefresh)
        Me.ribbonPageGroup1.ItemLinks.Add(Me.bCancel)
        Me.ribbonPageGroup1.Name = "ribbonPageGroup1"
        '
        'ribbonPageGroup2
        '
        resources.ApplyResources(Me.ribbonPageGroup2, "ribbonPageGroup2")
        Me.ribbonPageGroup2.AllowTextClipping = False
        Me.ribbonPageGroup2.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribbonPageGroup2.ImageOptions.ImageKey = resources.GetString("ribbonPageGroup2.ImageOptions.ImageKey")
        Me.ribbonPageGroup2.ItemLinks.Add(Me.bbiPrintPreview)
        Me.ribbonPageGroup2.Name = "ribbonPageGroup2"
        '
        'ribbonStatusBar
        '
        resources.ApplyResources(Me.ribbonStatusBar, "ribbonStatusBar")
        Me.ribbonStatusBar.ItemLinks.Add(Me.bsiRecordsCount)
        Me.ribbonStatusBar.Name = "ribbonStatusBar"
        Me.ribbonStatusBar.Ribbon = Me.ribbonControl
        '
        'TableData
        '
        resources.ApplyResources(Me.TableData, "TableData")
        Me.TableData.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.0!)})
        Me.TableData.Controls.Add(Me.VendIDLabel)
        Me.TableData.Controls.Add(Me.VendIDSpinEdit)
        Me.TableData.Controls.Add(Me.VendNameLabel)
        Me.TableData.Controls.Add(Me.VendNameTextEdit)
        Me.TableData.Controls.Add(Me.CityIDLabel)
        Me.TableData.Controls.Add(Me.CityCombo1)
        Me.TableData.Controls.Add(Me.VendAddressLabel)
        Me.TableData.Controls.Add(Me.VendAddressTextEdit)
        Me.TableData.Controls.Add(Me.ContactsLabel)
        Me.TableData.Controls.Add(Me.ContactsTextEdit)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TableData.UseSkinIndents = True
        '
        'VendIDLabel
        '
        resources.ApplyResources(Me.VendIDLabel, "VendIDLabel")
        Me.VendIDLabel.Appearance.Font = CType(resources.GetObject("VendIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.VendIDLabel.Appearance.Options.UseFont = True
        Me.VendIDLabel.Appearance.Options.UseTextOptions = True
        Me.VendIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.VendIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.VendIDLabel, 1)
        Me.VendIDLabel.Name = "VendIDLabel"
        Me.TableData.SetRow(Me.VendIDLabel, 0)
        '
        'VendIDSpinEdit
        '
        resources.ApplyResources(Me.VendIDSpinEdit, "VendIDSpinEdit")
        Me.TableData.SetColumn(Me.VendIDSpinEdit, 2)
        Me.VendIDSpinEdit.Name = "VendIDSpinEdit"
        Me.VendIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("VendIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.VendIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.VendIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.VendIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.VendIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.VendIDSpinEdit, 0)
        '
        'VendNameLabel
        '
        resources.ApplyResources(Me.VendNameLabel, "VendNameLabel")
        Me.VendNameLabel.Appearance.Font = CType(resources.GetObject("VendNameLabel.Appearance.Font"), System.Drawing.Font)
        Me.VendNameLabel.Appearance.Options.UseFont = True
        Me.VendNameLabel.Appearance.Options.UseTextOptions = True
        Me.VendNameLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.VendNameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.VendNameLabel, 4)
        Me.VendNameLabel.Name = "VendNameLabel"
        Me.TableData.SetRow(Me.VendNameLabel, 0)
        '
        'VendNameTextEdit
        '
        resources.ApplyResources(Me.VendNameTextEdit, "VendNameTextEdit")
        Me.TableData.SetColumn(Me.VendNameTextEdit, 5)
        Me.VendNameTextEdit.Name = "VendNameTextEdit"
        Me.VendNameTextEdit.Properties.Appearance.Font = CType(resources.GetObject("VendNameTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.VendNameTextEdit.Properties.Appearance.Options.UseFont = True
        Me.VendNameTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.VendNameTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.VendNameTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.VendNameTextEdit, 0)
        '
        'CityIDLabel
        '
        resources.ApplyResources(Me.CityIDLabel, "CityIDLabel")
        Me.CityIDLabel.Appearance.Font = CType(resources.GetObject("CityIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.CityIDLabel.Appearance.Options.UseFont = True
        Me.CityIDLabel.Appearance.Options.UseTextOptions = True
        Me.CityIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CityIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.CityIDLabel, 1)
        Me.CityIDLabel.Name = "CityIDLabel"
        Me.TableData.SetRow(Me.CityIDLabel, 1)
        '
        'CityCombo1
        '
        resources.ApplyResources(Me.CityCombo1, "CityCombo1")
        Me.CityCombo1.Appearance.Font = CType(resources.GetObject("CityCombo1.Appearance.Font"), System.Drawing.Font)
        Me.CityCombo1.Appearance.Options.UseFont = True
        Me.CityCombo1.CityID = 0
        Me.CityCombo1.CityName = Nothing
        Me.TableData.SetColumn(Me.CityCombo1, 2)
        Me.CityCombo1.Name = "CityCombo1"
        Me.TableData.SetRow(Me.CityCombo1, 1)
        '
        'VendAddressLabel
        '
        resources.ApplyResources(Me.VendAddressLabel, "VendAddressLabel")
        Me.VendAddressLabel.Appearance.Font = CType(resources.GetObject("VendAddressLabel.Appearance.Font"), System.Drawing.Font)
        Me.VendAddressLabel.Appearance.Options.UseFont = True
        Me.VendAddressLabel.Appearance.Options.UseTextOptions = True
        Me.VendAddressLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.VendAddressLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.VendAddressLabel, 4)
        Me.VendAddressLabel.Name = "VendAddressLabel"
        Me.TableData.SetRow(Me.VendAddressLabel, 1)
        '
        'VendAddressTextEdit
        '
        resources.ApplyResources(Me.VendAddressTextEdit, "VendAddressTextEdit")
        Me.TableData.SetColumn(Me.VendAddressTextEdit, 5)
        Me.VendAddressTextEdit.Name = "VendAddressTextEdit"
        Me.VendAddressTextEdit.Properties.Appearance.Font = CType(resources.GetObject("VendAddressTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.VendAddressTextEdit.Properties.Appearance.Options.UseFont = True
        Me.VendAddressTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.VendAddressTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.VendAddressTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.VendAddressTextEdit, 1)
        '
        'ContactsLabel
        '
        resources.ApplyResources(Me.ContactsLabel, "ContactsLabel")
        Me.ContactsLabel.Appearance.Font = CType(resources.GetObject("ContactsLabel.Appearance.Font"), System.Drawing.Font)
        Me.ContactsLabel.Appearance.Options.UseFont = True
        Me.ContactsLabel.Appearance.Options.UseTextOptions = True
        Me.ContactsLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ContactsLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.ContactsLabel, 1)
        Me.ContactsLabel.Name = "ContactsLabel"
        Me.TableData.SetRow(Me.ContactsLabel, 2)
        '
        'ContactsTextEdit
        '
        resources.ApplyResources(Me.ContactsTextEdit, "ContactsTextEdit")
        Me.TableData.SetColumn(Me.ContactsTextEdit, 2)
        Me.ContactsTextEdit.Name = "ContactsTextEdit"
        Me.ContactsTextEdit.Properties.Appearance.Font = CType(resources.GetObject("ContactsTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ContactsTextEdit.Properties.Appearance.Options.UseFont = True
        Me.ContactsTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ContactsTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ContactsTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.ContactsTextEdit, 2)
        '
        'TLPBut
        '
        resources.ApplyResources(Me.TLPBut, "TLPBut")
        Me.TableData.SetColumn(Me.TLPBut, 0)
        Me.TLPBut.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!)})
        Me.TableData.SetColumnSpan(Me.TLPBut, 7)
        Me.TLPBut.Controls.Add(Me.butApply)
        Me.TLPBut.Controls.Add(Me.butCancel)
        Me.TLPBut.Controls.Add(Me.butClose)
        Me.TLPBut.Name = "TLPBut"
        Me.TableData.SetRow(Me.TLPBut, 3)
        Me.TLPBut.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TLPBut.UseSkinIndents = True
        '
        'butApply
        '
        resources.ApplyResources(Me.butApply, "butApply")
        Me.butApply.Appearance.Font = CType(resources.GetObject("butApply.Appearance.Font"), System.Drawing.Font)
        Me.butApply.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.butApply, 1)
        Me.butApply.ImageOptions.ImageKey = resources.GetString("butApply.ImageOptions.ImageKey")
        Me.butApply.Name = "butApply"
        Me.TLPBut.SetRow(Me.butApply, 0)
        '
        'butCancel
        '
        resources.ApplyResources(Me.butCancel, "butCancel")
        Me.butCancel.Appearance.Font = CType(resources.GetObject("butCancel.Appearance.Font"), System.Drawing.Font)
        Me.butCancel.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.butCancel, 3)
        Me.butCancel.ImageOptions.ImageKey = resources.GetString("butCancel.ImageOptions.ImageKey")
        Me.butCancel.Name = "butCancel"
        Me.TLPBut.SetRow(Me.butCancel, 0)
        '
        'butClose
        '
        resources.ApplyResources(Me.butClose, "butClose")
        Me.butClose.Appearance.Font = CType(resources.GetObject("butClose.Appearance.Font"), System.Drawing.Font)
        Me.butClose.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.butClose, 5)
        Me.butClose.ImageOptions.ImageKey = resources.GetString("butClose.ImageOptions.ImageKey")
        Me.butClose.Name = "butClose"
        Me.TLPBut.SetRow(Me.butClose, 0)
        '
        'FrmVendors
        '
        resources.ApplyResources(Me, "$this")
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ribbonStatusBar)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "FrmVendors"
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
        CType(Me.VendIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VendNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VendAddressTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContactsTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents VendIDSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents VendIDLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colVendID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents VendNameTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents VendNameLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colVendName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CityIDSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents CityIDLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colCityID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents VendAddressTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents VendAddressLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colVendAddress As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ContactsTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ContactsLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colContacts As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCityCombo As DentistX.CityCombo
    Friend WithEvents CityCombo1 As DentistX.CityCombo
End Class
