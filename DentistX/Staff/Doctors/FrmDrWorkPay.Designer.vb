<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDrWorkPay
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDrWorkPay))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWorkPayID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWorkDetails = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWorkID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDrName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDrID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotes = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.WorkPayIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.WorkPayIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.WorkIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.WorkIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.DrIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DrIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.PayValueLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PayValueSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.PayDateLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PayDateDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.NotesLabel = New DevExpress.XtraEditors.LabelControl()
        Me.NotesTextEdit = New DevExpress.XtraEditors.TextEdit()
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
        CType(Me.WorkPayIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WorkIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayValueSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDateDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDateDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colWorkPayID, Me.colWorkDetails, Me.colWorkID, Me.colDrName, Me.colDrID, Me.colPayValue, Me.colPayDate, Me.colNotes})
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
        'colWorkPayID
        '
        resources.ApplyResources(Me.colWorkPayID, "colWorkPayID")
        Me.colWorkPayID.FieldName = "WorkPayID"
        Me.colWorkPayID.ImageOptions.ImageKey = resources.GetString("colWorkPayID.ImageOptions.ImageKey")
        Me.colWorkPayID.Name = "colWorkPayID"
        '
        'colWorkDetails
        '
        resources.ApplyResources(Me.colWorkDetails, "colWorkDetails")
        Me.colWorkDetails.FieldName = "WrkDetail"
        Me.colWorkDetails.ImageOptions.ImageKey = resources.GetString("colWorkDetails.ImageOptions.ImageKey")
        Me.colWorkDetails.Name = "colWorkDetails"
        '
        'colWorkID
        '
        resources.ApplyResources(Me.colWorkID, "colWorkID")
        Me.colWorkID.FieldName = "WorkID"
        Me.colWorkID.ImageOptions.ImageKey = resources.GetString("colWorkID.ImageOptions.ImageKey")
        Me.colWorkID.Name = "colWorkID"
        '
        'colDrName
        '
        resources.ApplyResources(Me.colDrName, "colDrName")
        Me.colDrName.FieldName = "DrName"
        Me.colDrName.ImageOptions.ImageKey = resources.GetString("colDrName.ImageOptions.ImageKey")
        Me.colDrName.Name = "colDrName"
        '
        'colDrID
        '
        resources.ApplyResources(Me.colDrID, "colDrID")
        Me.colDrID.FieldName = "DrID"
        Me.colDrID.ImageOptions.ImageKey = resources.GetString("colDrID.ImageOptions.ImageKey")
        Me.colDrID.Name = "colDrID"
        '
        'colPayValue
        '
        resources.ApplyResources(Me.colPayValue, "colPayValue")
        Me.colPayValue.FieldName = "PayValue"
        Me.colPayValue.ImageOptions.ImageKey = resources.GetString("colPayValue.ImageOptions.ImageKey")
        Me.colPayValue.Name = "colPayValue"
        '
        'colPayDate
        '
        resources.ApplyResources(Me.colPayDate, "colPayDate")
        Me.colPayDate.FieldName = "PayDate"
        Me.colPayDate.ImageOptions.ImageKey = resources.GetString("colPayDate.ImageOptions.ImageKey")
        Me.colPayDate.Name = "colPayDate"
        '
        'colNotes
        '
        resources.ApplyResources(Me.colNotes, "colNotes")
        Me.colNotes.FieldName = "Notes"
        Me.colNotes.ImageOptions.ImageKey = resources.GetString("colNotes.ImageOptions.ImageKey")
        Me.colNotes.Name = "colNotes"
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
        Me.TableData.Controls.Add(Me.WorkPayIDLabel)
        Me.TableData.Controls.Add(Me.WorkPayIDSpinEdit)
        Me.TableData.Controls.Add(Me.WorkIDLabel)
        Me.TableData.Controls.Add(Me.WorkIDSpinEdit)
        Me.TableData.Controls.Add(Me.DrIDLabel)
        Me.TableData.Controls.Add(Me.DrIDSpinEdit)
        Me.TableData.Controls.Add(Me.PayValueLabel)
        Me.TableData.Controls.Add(Me.PayValueSpinEdit)
        Me.TableData.Controls.Add(Me.PayDateLabel)
        Me.TableData.Controls.Add(Me.PayDateDateEdit)
        Me.TableData.Controls.Add(Me.NotesLabel)
        Me.TableData.Controls.Add(Me.NotesTextEdit)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TableData.UseSkinIndents = True
        '
        'WorkPayIDLabel
        '
        resources.ApplyResources(Me.WorkPayIDLabel, "WorkPayIDLabel")
        Me.WorkPayIDLabel.Appearance.Font = CType(resources.GetObject("WorkPayIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.WorkPayIDLabel.Appearance.Options.UseFont = True
        Me.WorkPayIDLabel.Appearance.Options.UseTextOptions = True
        Me.WorkPayIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WorkPayIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.WorkPayIDLabel, 1)
        Me.WorkPayIDLabel.Name = "WorkPayIDLabel"
        Me.TableData.SetRow(Me.WorkPayIDLabel, 0)
        '
        'WorkPayIDSpinEdit
        '
        resources.ApplyResources(Me.WorkPayIDSpinEdit, "WorkPayIDSpinEdit")
        Me.TableData.SetColumn(Me.WorkPayIDSpinEdit, 2)
        Me.WorkPayIDSpinEdit.Name = "WorkPayIDSpinEdit"
        Me.WorkPayIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("WorkPayIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.WorkPayIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.WorkPayIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.WorkPayIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WorkPayIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.WorkPayIDSpinEdit, 0)
        '
        'WorkIDLabel
        '
        resources.ApplyResources(Me.WorkIDLabel, "WorkIDLabel")
        Me.WorkIDLabel.Appearance.Font = CType(resources.GetObject("WorkIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.WorkIDLabel.Appearance.Options.UseFont = True
        Me.WorkIDLabel.Appearance.Options.UseTextOptions = True
        Me.WorkIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WorkIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.WorkIDLabel, 4)
        Me.WorkIDLabel.Name = "WorkIDLabel"
        Me.TableData.SetRow(Me.WorkIDLabel, 0)
        '
        'WorkIDSpinEdit
        '
        resources.ApplyResources(Me.WorkIDSpinEdit, "WorkIDSpinEdit")
        Me.TableData.SetColumn(Me.WorkIDSpinEdit, 5)
        Me.WorkIDSpinEdit.Name = "WorkIDSpinEdit"
        Me.WorkIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("WorkIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.WorkIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.WorkIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.WorkIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WorkIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.WorkIDSpinEdit, 0)
        '
        'DrIDLabel
        '
        resources.ApplyResources(Me.DrIDLabel, "DrIDLabel")
        Me.DrIDLabel.Appearance.Font = CType(resources.GetObject("DrIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.DrIDLabel.Appearance.Options.UseFont = True
        Me.DrIDLabel.Appearance.Options.UseTextOptions = True
        Me.DrIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.DrIDLabel, 1)
        Me.DrIDLabel.Name = "DrIDLabel"
        Me.TableData.SetRow(Me.DrIDLabel, 1)
        '
        'DrIDSpinEdit
        '
        resources.ApplyResources(Me.DrIDSpinEdit, "DrIDSpinEdit")
        Me.TableData.SetColumn(Me.DrIDSpinEdit, 2)
        Me.DrIDSpinEdit.Name = "DrIDSpinEdit"
        Me.DrIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("DrIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DrIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.DrIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.DrIDSpinEdit, 1)
        '
        'PayValueLabel
        '
        resources.ApplyResources(Me.PayValueLabel, "PayValueLabel")
        Me.PayValueLabel.Appearance.Font = CType(resources.GetObject("PayValueLabel.Appearance.Font"), System.Drawing.Font)
        Me.PayValueLabel.Appearance.Options.UseFont = True
        Me.PayValueLabel.Appearance.Options.UseTextOptions = True
        Me.PayValueLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayValueLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.PayValueLabel, 4)
        Me.PayValueLabel.Name = "PayValueLabel"
        Me.TableData.SetRow(Me.PayValueLabel, 1)
        '
        'PayValueSpinEdit
        '
        resources.ApplyResources(Me.PayValueSpinEdit, "PayValueSpinEdit")
        Me.TableData.SetColumn(Me.PayValueSpinEdit, 5)
        Me.PayValueSpinEdit.Name = "PayValueSpinEdit"
        Me.PayValueSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("PayValueSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PayValueSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.PayValueSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PayValueSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayValueSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.PayValueSpinEdit, 1)
        '
        'PayDateLabel
        '
        resources.ApplyResources(Me.PayDateLabel, "PayDateLabel")
        Me.PayDateLabel.Appearance.Font = CType(resources.GetObject("PayDateLabel.Appearance.Font"), System.Drawing.Font)
        Me.PayDateLabel.Appearance.Options.UseFont = True
        Me.PayDateLabel.Appearance.Options.UseTextOptions = True
        Me.PayDateLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayDateLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.PayDateLabel, 1)
        Me.PayDateLabel.Name = "PayDateLabel"
        Me.TableData.SetRow(Me.PayDateLabel, 2)
        '
        'PayDateDateEdit
        '
        resources.ApplyResources(Me.PayDateDateEdit, "PayDateDateEdit")
        Me.TableData.SetColumn(Me.PayDateDateEdit, 2)
        Me.PayDateDateEdit.Name = "PayDateDateEdit"
        Me.PayDateDateEdit.Properties.Appearance.Font = CType(resources.GetObject("PayDateDateEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PayDateDateEdit.Properties.Appearance.Options.UseFont = True
        Me.PayDateDateEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PayDateDateEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayDateDateEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.PayDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("PayDateDateEdit.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.PayDateDateEdit, 2)
        '
        'NotesLabel
        '
        resources.ApplyResources(Me.NotesLabel, "NotesLabel")
        Me.NotesLabel.Appearance.Font = CType(resources.GetObject("NotesLabel.Appearance.Font"), System.Drawing.Font)
        Me.NotesLabel.Appearance.Options.UseFont = True
        Me.NotesLabel.Appearance.Options.UseTextOptions = True
        Me.NotesLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NotesLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.NotesLabel, 4)
        Me.NotesLabel.Name = "NotesLabel"
        Me.TableData.SetRow(Me.NotesLabel, 2)
        '
        'NotesTextEdit
        '
        resources.ApplyResources(Me.NotesTextEdit, "NotesTextEdit")
        Me.TableData.SetColumn(Me.NotesTextEdit, 5)
        Me.NotesTextEdit.Name = "NotesTextEdit"
        Me.NotesTextEdit.Properties.Appearance.Font = CType(resources.GetObject("NotesTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.NotesTextEdit.Properties.Appearance.Options.UseFont = True
        Me.NotesTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.NotesTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NotesTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.NotesTextEdit, 2)
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
        'FrmDrWorkPay
        '
        resources.ApplyResources(Me, "$this")
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ribbonStatusBar)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "FrmDrWorkPay"
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
        CType(Me.WorkPayIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WorkIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayValueSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDateDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDateDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
		Friend WithEvents WorkPayIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents WorkPayIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colWorkPayID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents WorkIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents WorkIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colWorkID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents DrIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents DrIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colDrID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PayValueSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents PayValueLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPayValue As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PayDateDateEdit As DevExpress.XtraEditors.DateEdit
		Friend WithEvents PayDateLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPayDate As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents NotesTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents NotesLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colWorkDetails As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDrName As DevExpress.XtraGrid.Columns.GridColumn
End Class
