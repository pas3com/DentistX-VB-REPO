<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmContacts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmContacts))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colContactID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPhone = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEmail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCreatedAt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWhatsAppPrefix = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWhatsApp = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.ContactIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.ContactIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.NameLabel = New DevExpress.XtraEditors.LabelControl()
        Me.NameTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.PhoneLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PhoneTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.EmailLabel = New DevExpress.XtraEditors.LabelControl()
        Me.EmailTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.NotesLabel = New DevExpress.XtraEditors.LabelControl()
        Me.NotesTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.CreatedAtLabel = New DevExpress.XtraEditors.LabelControl()
        Me.CreatedAtDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.WhatsAppPrefixLabel = New DevExpress.XtraEditors.LabelControl()
        Me.WhatsAppPrefixCombo = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.WhatsAppLabel = New DevExpress.XtraEditors.LabelControl()
        Me.WhatsAppTextEdit = New DevExpress.XtraEditors.TextEdit()
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
        CType(Me.ContactIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CreatedAtDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CreatedAtDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WhatsAppPrefixCombo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WhatsAppTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colContactID, Me.colName, Me.colPhone, Me.colEmail, Me.colNotes, Me.colCreatedAt, Me.colWhatsAppPrefix, Me.colWhatsApp})
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
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colContactID
        '
        resources.ApplyResources(Me.colContactID, "colContactID")
        Me.colContactID.FieldName = "ContactID"
        Me.colContactID.ImageOptions.ImageKey = resources.GetString("colContactID.ImageOptions.ImageKey")
        Me.colContactID.Name = "colContactID"
        '
        'colName
        '
        resources.ApplyResources(Me.colName, "colName")
        Me.colName.FieldName = "CName"
        Me.colName.ImageOptions.ImageKey = resources.GetString("colName.ImageOptions.ImageKey")
        Me.colName.Name = "colName"
        '
        'colPhone
        '
        resources.ApplyResources(Me.colPhone, "colPhone")
        Me.colPhone.FieldName = "Phone"
        Me.colPhone.ImageOptions.ImageKey = resources.GetString("colPhone.ImageOptions.ImageKey")
        Me.colPhone.Name = "colPhone"
        '
        'colEmail
        '
        resources.ApplyResources(Me.colEmail, "colEmail")
        Me.colEmail.FieldName = "Email"
        Me.colEmail.ImageOptions.ImageKey = resources.GetString("colEmail.ImageOptions.ImageKey")
        Me.colEmail.Name = "colEmail"
        '
        'colNotes
        '
        resources.ApplyResources(Me.colNotes, "colNotes")
        Me.colNotes.FieldName = "Notes"
        Me.colNotes.ImageOptions.ImageKey = resources.GetString("colNotes.ImageOptions.ImageKey")
        Me.colNotes.Name = "colNotes"
        '
        'colCreatedAt
        '
        resources.ApplyResources(Me.colCreatedAt, "colCreatedAt")
        Me.colCreatedAt.FieldName = "CreatedAt"
        Me.colCreatedAt.ImageOptions.ImageKey = resources.GetString("colCreatedAt.ImageOptions.ImageKey")
        Me.colCreatedAt.Name = "colCreatedAt"
        '
        'colWhatsAppPrefix
        '
        resources.ApplyResources(Me.colWhatsAppPrefix, "colWhatsAppPrefix")
        Me.colWhatsAppPrefix.FieldName = "WhatsAppPrefix"
        Me.colWhatsAppPrefix.ImageOptions.ImageKey = resources.GetString("colWhatsAppPrefix.ImageOptions.ImageKey")
        Me.colWhatsAppPrefix.Name = "colWhatsAppPrefix"
        '
        'colWhatsApp
        '
        resources.ApplyResources(Me.colWhatsApp, "colWhatsApp")
        Me.colWhatsApp.FieldName = "WhatsApp"
        Me.colWhatsApp.ImageOptions.ImageKey = resources.GetString("colWhatsApp.ImageOptions.ImageKey")
        Me.colWhatsApp.Name = "colWhatsApp"
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
        Me.TableData.Controls.Add(Me.lblWhats)
        Me.TableData.Controls.Add(Me.ContactIDLabel)
        Me.TableData.Controls.Add(Me.ContactIDSpinEdit)
        Me.TableData.Controls.Add(Me.NameLabel)
        Me.TableData.Controls.Add(Me.NameTextEdit)
        Me.TableData.Controls.Add(Me.PhoneLabel)
        Me.TableData.Controls.Add(Me.PhoneTextEdit)
        Me.TableData.Controls.Add(Me.EmailLabel)
        Me.TableData.Controls.Add(Me.EmailTextEdit)
        Me.TableData.Controls.Add(Me.NotesLabel)
        Me.TableData.Controls.Add(Me.NotesTextEdit)
        Me.TableData.Controls.Add(Me.CreatedAtLabel)
        Me.TableData.Controls.Add(Me.CreatedAtDateEdit)
        Me.TableData.Controls.Add(Me.WhatsAppPrefixLabel)
        Me.TableData.Controls.Add(Me.WhatsAppPrefixCombo)
        Me.TableData.Controls.Add(Me.WhatsAppLabel)
        Me.TableData.Controls.Add(Me.WhatsAppTextEdit)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TableData.UseSkinIndents = True
        '
        'lblWhats
        '
        resources.ApplyResources(Me.lblWhats, "lblWhats")
        Me.lblWhats.Appearance.Font = CType(resources.GetObject("lblWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblWhats.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblWhats.Appearance.Options.UseFont = True
        Me.lblWhats.Appearance.Options.UseForeColor = True
        Me.TableData.SetColumn(Me.lblWhats, 6)
        Me.lblWhats.Name = "lblWhats"
        Me.TableData.SetRow(Me.lblWhats, 3)
        '
        'ContactIDLabel
        '
        resources.ApplyResources(Me.ContactIDLabel, "ContactIDLabel")
        Me.ContactIDLabel.Appearance.Font = CType(resources.GetObject("ContactIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.ContactIDLabel.Appearance.Options.UseFont = True
        Me.ContactIDLabel.Appearance.Options.UseTextOptions = True
        Me.ContactIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ContactIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.ContactIDLabel, 1)
        Me.ContactIDLabel.Name = "ContactIDLabel"
        Me.TableData.SetRow(Me.ContactIDLabel, 0)
        '
        'ContactIDSpinEdit
        '
        resources.ApplyResources(Me.ContactIDSpinEdit, "ContactIDSpinEdit")
        Me.TableData.SetColumn(Me.ContactIDSpinEdit, 2)
        Me.ContactIDSpinEdit.Name = "ContactIDSpinEdit"
        Me.ContactIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("ContactIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ContactIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.ContactIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ContactIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ContactIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.ContactIDSpinEdit, 0)
        '
        'NameLabel
        '
        resources.ApplyResources(Me.NameLabel, "NameLabel")
        Me.NameLabel.Appearance.Font = CType(resources.GetObject("NameLabel.Appearance.Font"), System.Drawing.Font)
        Me.NameLabel.Appearance.Options.UseFont = True
        Me.NameLabel.Appearance.Options.UseTextOptions = True
        Me.NameLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.NameLabel, 4)
        Me.NameLabel.Name = "NameLabel"
        Me.TableData.SetRow(Me.NameLabel, 0)
        '
        'NameTextEdit
        '
        resources.ApplyResources(Me.NameTextEdit, "NameTextEdit")
        Me.TableData.SetColumn(Me.NameTextEdit, 5)
        Me.NameTextEdit.Name = "NameTextEdit"
        Me.NameTextEdit.Properties.Appearance.Font = CType(resources.GetObject("NameTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.NameTextEdit.Properties.Appearance.Options.UseFont = True
        Me.NameTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.NameTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NameTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.NameTextEdit, 0)
        '
        'PhoneLabel
        '
        resources.ApplyResources(Me.PhoneLabel, "PhoneLabel")
        Me.PhoneLabel.Appearance.Font = CType(resources.GetObject("PhoneLabel.Appearance.Font"), System.Drawing.Font)
        Me.PhoneLabel.Appearance.Options.UseFont = True
        Me.PhoneLabel.Appearance.Options.UseTextOptions = True
        Me.PhoneLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PhoneLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.PhoneLabel, 1)
        Me.PhoneLabel.Name = "PhoneLabel"
        Me.TableData.SetRow(Me.PhoneLabel, 1)
        '
        'PhoneTextEdit
        '
        resources.ApplyResources(Me.PhoneTextEdit, "PhoneTextEdit")
        Me.TableData.SetColumn(Me.PhoneTextEdit, 2)
        Me.PhoneTextEdit.Name = "PhoneTextEdit"
        Me.PhoneTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PhoneTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PhoneTextEdit.Properties.Appearance.Options.UseFont = True
        Me.PhoneTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PhoneTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PhoneTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.PhoneTextEdit, 1)
        '
        'EmailLabel
        '
        resources.ApplyResources(Me.EmailLabel, "EmailLabel")
        Me.EmailLabel.Appearance.Font = CType(resources.GetObject("EmailLabel.Appearance.Font"), System.Drawing.Font)
        Me.EmailLabel.Appearance.Options.UseFont = True
        Me.EmailLabel.Appearance.Options.UseTextOptions = True
        Me.EmailLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmailLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.EmailLabel, 4)
        Me.EmailLabel.Name = "EmailLabel"
        Me.TableData.SetRow(Me.EmailLabel, 1)
        '
        'EmailTextEdit
        '
        resources.ApplyResources(Me.EmailTextEdit, "EmailTextEdit")
        Me.TableData.SetColumn(Me.EmailTextEdit, 5)
        Me.EmailTextEdit.Name = "EmailTextEdit"
        Me.EmailTextEdit.Properties.Appearance.Font = CType(resources.GetObject("EmailTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.EmailTextEdit.Properties.Appearance.Options.UseFont = True
        Me.EmailTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.EmailTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmailTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.EmailTextEdit, 1)
        '
        'NotesLabel
        '
        resources.ApplyResources(Me.NotesLabel, "NotesLabel")
        Me.NotesLabel.Appearance.Font = CType(resources.GetObject("NotesLabel.Appearance.Font"), System.Drawing.Font)
        Me.NotesLabel.Appearance.Options.UseFont = True
        Me.NotesLabel.Appearance.Options.UseTextOptions = True
        Me.NotesLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NotesLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.NotesLabel, 1)
        Me.NotesLabel.Name = "NotesLabel"
        Me.TableData.SetRow(Me.NotesLabel, 2)
        '
        'NotesTextEdit
        '
        resources.ApplyResources(Me.NotesTextEdit, "NotesTextEdit")
        Me.TableData.SetColumn(Me.NotesTextEdit, 2)
        Me.NotesTextEdit.Name = "NotesTextEdit"
        Me.NotesTextEdit.Properties.Appearance.Font = CType(resources.GetObject("NotesTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.NotesTextEdit.Properties.Appearance.Options.UseFont = True
        Me.NotesTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.NotesTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NotesTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.NotesTextEdit, 2)
        '
        'CreatedAtLabel
        '
        resources.ApplyResources(Me.CreatedAtLabel, "CreatedAtLabel")
        Me.CreatedAtLabel.Appearance.Font = CType(resources.GetObject("CreatedAtLabel.Appearance.Font"), System.Drawing.Font)
        Me.CreatedAtLabel.Appearance.Options.UseFont = True
        Me.CreatedAtLabel.Appearance.Options.UseTextOptions = True
        Me.CreatedAtLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CreatedAtLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.CreatedAtLabel, 4)
        Me.CreatedAtLabel.Name = "CreatedAtLabel"
        Me.TableData.SetRow(Me.CreatedAtLabel, 2)
        '
        'CreatedAtDateEdit
        '
        resources.ApplyResources(Me.CreatedAtDateEdit, "CreatedAtDateEdit")
        Me.TableData.SetColumn(Me.CreatedAtDateEdit, 5)
        Me.CreatedAtDateEdit.Name = "CreatedAtDateEdit"
        Me.CreatedAtDateEdit.Properties.Appearance.Font = CType(resources.GetObject("CreatedAtDateEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CreatedAtDateEdit.Properties.Appearance.Options.UseFont = True
        Me.CreatedAtDateEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.CreatedAtDateEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CreatedAtDateEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.CreatedAtDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CreatedAtDateEdit.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.CreatedAtDateEdit, 2)
        '
        'WhatsAppPrefixLabel
        '
        resources.ApplyResources(Me.WhatsAppPrefixLabel, "WhatsAppPrefixLabel")
        Me.WhatsAppPrefixLabel.Appearance.Font = CType(resources.GetObject("WhatsAppPrefixLabel.Appearance.Font"), System.Drawing.Font)
        Me.WhatsAppPrefixLabel.Appearance.Options.UseFont = True
        Me.WhatsAppPrefixLabel.Appearance.Options.UseTextOptions = True
        Me.WhatsAppPrefixLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WhatsAppPrefixLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.WhatsAppPrefixLabel, 1)
        Me.WhatsAppPrefixLabel.Name = "WhatsAppPrefixLabel"
        Me.TableData.SetRow(Me.WhatsAppPrefixLabel, 3)
        '
        'WhatsAppPrefixCombo
        '
        resources.ApplyResources(Me.WhatsAppPrefixCombo, "WhatsAppPrefixCombo")
        Me.TableData.SetColumn(Me.WhatsAppPrefixCombo, 2)
        Me.WhatsAppPrefixCombo.Name = "WhatsAppPrefixCombo"
        Me.WhatsAppPrefixCombo.Properties.Appearance.Font = CType(resources.GetObject("WhatsAppPrefixCombo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.WhatsAppPrefixCombo.Properties.Appearance.Options.UseFont = True
        Me.WhatsAppPrefixCombo.Properties.Appearance.Options.UseTextOptions = True
        Me.WhatsAppPrefixCombo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WhatsAppPrefixCombo.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.WhatsAppPrefixCombo.Properties.AppearanceDropDown.Font = CType(resources.GetObject("WhatsAppPrefixCombo.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.WhatsAppPrefixCombo.Properties.AppearanceDropDown.Options.UseFont = True
        Me.WhatsAppPrefixCombo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("WhatsAppPrefixCombo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.WhatsAppPrefixCombo, 3)
        '
        'WhatsAppLabel
        '
        resources.ApplyResources(Me.WhatsAppLabel, "WhatsAppLabel")
        Me.WhatsAppLabel.Appearance.Font = CType(resources.GetObject("WhatsAppLabel.Appearance.Font"), System.Drawing.Font)
        Me.WhatsAppLabel.Appearance.Options.UseFont = True
        Me.WhatsAppLabel.Appearance.Options.UseTextOptions = True
        Me.WhatsAppLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WhatsAppLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.WhatsAppLabel, 4)
        Me.WhatsAppLabel.Name = "WhatsAppLabel"
        Me.TableData.SetRow(Me.WhatsAppLabel, 3)
        '
        'WhatsAppTextEdit
        '
        resources.ApplyResources(Me.WhatsAppTextEdit, "WhatsAppTextEdit")
        Me.TableData.SetColumn(Me.WhatsAppTextEdit, 5)
        Me.WhatsAppTextEdit.Name = "WhatsAppTextEdit"
        Me.WhatsAppTextEdit.Properties.Appearance.Font = CType(resources.GetObject("WhatsAppTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.WhatsAppTextEdit.Properties.Appearance.Options.UseFont = True
        Me.WhatsAppTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.WhatsAppTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WhatsAppTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.WhatsAppTextEdit.Properties.MaxLength = 10
        Me.TableData.SetRow(Me.WhatsAppTextEdit, 3)
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
        Me.TableData.SetRow(Me.TLPBut, 4)
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
        'FrmContacts
        '
        resources.ApplyResources(Me, "$this")
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ribbonStatusBar)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "FrmContacts"
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
        CType(Me.ContactIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NotesTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CreatedAtDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CreatedAtDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WhatsAppPrefixCombo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WhatsAppTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
		Friend WithEvents ContactIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents ContactIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colContactID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents NameTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents NameLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colName As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PhoneTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents PhoneLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPhone As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents EmailTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents EmailLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colEmail As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents NotesTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents NotesLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents CreatedAtDateEdit As DevExpress.XtraEditors.DateEdit
		Friend WithEvents CreatedAtLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colCreatedAt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colWhatsAppPrefix As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colWhatsApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents WhatsAppPrefixLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents WhatsAppPrefixCombo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents WhatsAppLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents WhatsAppTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
End Class
