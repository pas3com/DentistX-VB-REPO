<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class TblCategForm
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TblCategForm))
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gridCode = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colParentCategory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCategoryName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCategoryID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ribbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.BarAndDockingController1 = New DevExpress.XtraBars.BarAndDockingController(Me.components)
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
        Me.SS = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.TLPBut = New DevExpress.Utils.Layout.TablePanel()
        Me.butApply = New DevExpress.XtraEditors.SimpleButton()
        Me.butCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.TLP = New DevExpress.Utils.Layout.TablePanel()
        Me.nudParentCategory = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbCategoryName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.nudCategoryID = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.gridCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ribbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarAndDockingController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLPBut.SuspendLayout()
        CType(Me.TLP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLP.SuspendLayout()
        CType(Me.nudParentCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCategoryName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCategoryID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        resources.ApplyResources(Me.SplitContainerControl1, "SplitContainerControl1")
        Me.SplitContainerControl1.CaptionImageOptions.ImageKey = resources.GetString("SplitContainerControl1.CaptionImageOptions.ImageKey")
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        resources.ApplyResources(Me.SplitContainerControl1.Panel1, "SplitContainerControl1.Panel1")
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gridCode)
        '
        'SplitContainerControl1.Panel2
        '
        resources.ApplyResources(Me.SplitContainerControl1.Panel2, "SplitContainerControl1.Panel2")
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.TLPBut)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.TLP)
        Me.SplitContainerControl1.SplitterPosition = 240
        '
        'gridCode
        '
        resources.ApplyResources(Me.gridCode, "gridCode")
        Me.gridCode.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridCode.EmbeddedNavigator.AccessibleDescription")
        Me.gridCode.EmbeddedNavigator.AccessibleName = resources.GetString("gridCode.EmbeddedNavigator.AccessibleName")
        Me.gridCode.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridCode.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridCode.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridCode.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridCode.EmbeddedNavigator.Appearance.Font = CType(resources.GetObject("gridCode.EmbeddedNavigator.Appearance.Font"), System.Drawing.Font)
        Me.gridCode.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.gridCode.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridCode.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridCode.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridCode.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridCode.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridCode.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridCode.EmbeddedNavigator.Buttons.Append.Hint = resources.GetString("gridCode.EmbeddedNavigator.Buttons.Append.Hint")
        Me.gridCode.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.gridCode.EmbeddedNavigator.Buttons.CancelEdit.Hint = resources.GetString("gridCode.EmbeddedNavigator.Buttons.CancelEdit.Hint")
        Me.gridCode.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.gridCode.EmbeddedNavigator.Buttons.Edit.Hint = resources.GetString("gridCode.EmbeddedNavigator.Buttons.Edit.Hint")
        Me.gridCode.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.gridCode.EmbeddedNavigator.Buttons.EndEdit.Hint = resources.GetString("gridCode.EmbeddedNavigator.Buttons.EndEdit.Hint")
        Me.gridCode.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.gridCode.EmbeddedNavigator.Buttons.Remove.Hint = resources.GetString("gridCode.EmbeddedNavigator.Buttons.Remove.Hint")
        Me.gridCode.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.gridCode.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridCode.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridCode.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridCode.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridCode.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridCode.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridCode.EmbeddedNavigator.ToolTip = resources.GetString("gridCode.EmbeddedNavigator.ToolTip")
        Me.gridCode.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridCode.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridCode.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridCode.EmbeddedNavigator.ToolTipTitle")
        Me.gridCode.MainView = Me.dgView
        Me.gridCode.MenuManager = Me.ribbonControl
        Me.gridCode.Name = "gridCode"
        Me.gridCode.UseEmbeddedNavigator = True
        Me.gridCode.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        resources.ApplyResources(Me.dgView, "dgView")
        Me.dgView.Appearance.HeaderPanel.Font = CType(resources.GetObject("dgView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
        Me.dgView.Appearance.Row.Font = CType(resources.GetObject("dgView.Appearance.Row.Font"), System.Drawing.Font)
        Me.dgView.Appearance.Row.Options.UseFont = True
        Me.dgView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colParentCategory, Me.colCategoryName, Me.colCategoryID, Me.colRowNum})
        Me.dgView.DetailHeight = 377
        Me.dgView.GridControl = Me.gridCode
        Me.dgView.Name = "dgView"
        Me.dgView.OptionsBehavior.Editable = False
        Me.dgView.OptionsBehavior.ReadOnly = True
        Me.dgView.OptionsDetail.EnableMasterViewMode = False
        Me.dgView.OptionsView.EnableAppearanceEvenRow = True
        Me.dgView.OptionsView.ShowFooter = True
        '
        'colParentCategory
        '
        resources.ApplyResources(Me.colParentCategory, "colParentCategory")
        Me.colParentCategory.FieldName = "ParentCategory"
        Me.colParentCategory.ImageOptions.ImageKey = resources.GetString("colParentCategory.ImageOptions.ImageKey")
        Me.colParentCategory.Name = "colParentCategory"
        '
        'colCategoryName
        '
        resources.ApplyResources(Me.colCategoryName, "colCategoryName")
        Me.colCategoryName.FieldName = "CategoryName"
        Me.colCategoryName.ImageOptions.ImageKey = resources.GetString("colCategoryName.ImageOptions.ImageKey")
        Me.colCategoryName.Name = "colCategoryName"
        '
        'colCategoryID
        '
        resources.ApplyResources(Me.colCategoryID, "colCategoryID")
        Me.colCategoryID.FieldName = "CategoryID"
        Me.colCategoryID.ImageOptions.ImageKey = resources.GetString("colCategoryID.ImageOptions.ImageKey")
        Me.colCategoryID.Name = "colCategoryID"
        '
        'colRowNum
        '
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.ImageOptions.ImageKey = resources.GetString("colRowNum.ImageOptions.ImageKey")
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'ribbonControl
        '
        resources.ApplyResources(Me.ribbonControl, "ribbonControl")
        Me.ribbonControl.Controller = Me.BarAndDockingController1
        Me.ribbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(30, 32, 30, 32)
        Me.ribbonControl.ExpandCollapseItem.Id = 0
        Me.ribbonControl.ExpandCollapseItem.ImageOptions.ImageIndex = CType(resources.GetObject("ribbonControl.ExpandCollapseItem.ImageOptions.ImageIndex"), Integer)
        Me.ribbonControl.ExpandCollapseItem.ImageOptions.ImageKey = resources.GetString("ribbonControl.ExpandCollapseItem.ImageOptions.ImageKey")
        Me.ribbonControl.ExpandCollapseItem.ImageOptions.LargeImageIndex = CType(resources.GetObject("ribbonControl.ExpandCollapseItem.ImageOptions.LargeImageIndex"), Integer)
        Me.ribbonControl.ExpandCollapseItem.ImageOptions.LargeImageKey = resources.GetString("ribbonControl.ExpandCollapseItem.ImageOptions.LargeImageKey")
        Me.ribbonControl.ExpandCollapseItem.ImageOptions.SvgImage = CType(resources.GetObject("ribbonControl.ExpandCollapseItem.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.ribbonControl.ExpandCollapseItem.SearchTags = resources.GetString("ribbonControl.ExpandCollapseItem.SearchTags")
        Me.ribbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.ribbonControl.ExpandCollapseItem, Me.bbiPrintPreview, Me.bsiRecordsCount, Me.bAdd, Me.bEdit, Me.bDelete, Me.bRefresh, Me.bCancel})
        Me.ribbonControl.MaxItemId = 22
        Me.ribbonControl.Name = "ribbonControl"
        Me.ribbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.ribbonPage1})
        Me.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013
        Me.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribbonControl.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribbonControl.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide
        Me.ribbonControl.ShowQatLocationSelector = False
        Me.ribbonControl.ShowToolbarCustomizeItem = False
        Me.ribbonControl.StatusBar = Me.SS
        Me.ribbonControl.Toolbar.ShowCustomizeItem = False
        Me.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden
        '
        'BarAndDockingController1
        '
        Me.BarAndDockingController1.AppearancesBar.ItemsFont = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.BarAndDockingController1.AppearancesBar.StatusBarAppearance.Normal.Font = CType(resources.GetObject("BarAndDockingController1.AppearancesBar.StatusBarAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarAndDockingController1.AppearancesBar.StatusBarAppearance.Normal.Options.UseFont = True
        Me.BarAndDockingController1.AppearancesRibbon.Editor.Font = CType(resources.GetObject("BarAndDockingController1.AppearancesRibbon.Editor.Font"), System.Drawing.Font)
        Me.BarAndDockingController1.AppearancesRibbon.Editor.Options.UseFont = True
        Me.BarAndDockingController1.AppearancesRibbon.FormCaption.Font = CType(resources.GetObject("BarAndDockingController1.AppearancesRibbon.FormCaption.Font"), System.Drawing.Font)
        Me.BarAndDockingController1.AppearancesRibbon.FormCaption.Options.UseFont = True
        Me.BarAndDockingController1.AppearancesRibbon.PageCategory.Font = CType(resources.GetObject("BarAndDockingController1.AppearancesRibbon.PageCategory.Font"), System.Drawing.Font)
        Me.BarAndDockingController1.AppearancesRibbon.PageCategory.Options.UseFont = True
        Me.BarAndDockingController1.AppearancesRibbon.PageGroupCaption.Font = CType(resources.GetObject("BarAndDockingController1.AppearancesRibbon.PageGroupCaption.Font"), System.Drawing.Font)
        Me.BarAndDockingController1.AppearancesRibbon.PageGroupCaption.Options.UseFont = True
        Me.BarAndDockingController1.AppearancesRibbon.PageHeader.Font = CType(resources.GetObject("BarAndDockingController1.AppearancesRibbon.PageHeader.Font"), System.Drawing.Font)
        Me.BarAndDockingController1.AppearancesRibbon.PageHeader.Options.UseFont = True
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
        Me.bCancel.Id = 21
        Me.bCancel.ImageOptions.ImageIndex = CType(resources.GetObject("bCancel.ImageOptions.ImageIndex"), Integer)
        Me.bCancel.ImageOptions.ImageKey = resources.GetString("bCancel.ImageOptions.ImageKey")
        Me.bCancel.ImageOptions.LargeImageIndex = CType(resources.GetObject("bCancel.ImageOptions.LargeImageIndex"), Integer)
        Me.bCancel.ImageOptions.LargeImageKey = resources.GetString("bCancel.ImageOptions.LargeImageKey")
        Me.bCancel.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.undo
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
        'SS
        '
        resources.ApplyResources(Me.SS, "SS")
        Me.SS.ItemLinks.Add(Me.bsiRecordsCount)
        Me.SS.Name = "SS"
        Me.SS.Ribbon = Me.ribbonControl
        '
        'TLPBut
        '
        resources.ApplyResources(Me.TLPBut, "TLPBut")
        Me.TLPBut.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 83.24!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 78.74!)})
        Me.TLPBut.Controls.Add(Me.butApply)
        Me.TLPBut.Controls.Add(Me.butCancel)
        Me.TLPBut.Controls.Add(Me.butClose)
        Me.TLPBut.Name = "TLPBut"
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
        'TLP
        '
        resources.ApplyResources(Me.TLP, "TLP")
        Me.TLP.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 213.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 46.5!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 85.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 253.0!)})
        Me.TLP.Controls.Add(Me.nudParentCategory)
        Me.TLP.Controls.Add(Me.LabelControl3)
        Me.TLP.Controls.Add(Me.tbCategoryName)
        Me.TLP.Controls.Add(Me.LabelControl2)
        Me.TLP.Controls.Add(Me.nudCategoryID)
        Me.TLP.Controls.Add(Me.LabelControl1)
        Me.TLP.Name = "TLP"
        Me.TLP.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26.0!)})
        Me.TLP.UseSkinIndents = True
        '
        'nudParentCategory
        '
        resources.ApplyResources(Me.nudParentCategory, "nudParentCategory")
        Me.TLP.SetColumn(Me.nudParentCategory, 2)
        Me.nudParentCategory.MenuManager = Me.ribbonControl
        Me.nudParentCategory.Name = "nudParentCategory"
        Me.nudParentCategory.Properties.Appearance.Font = CType(resources.GetObject("nudParentCategory.Properties.Appearance.Font"), System.Drawing.Font)
        Me.nudParentCategory.Properties.Appearance.Options.UseFont = True
        Me.TLP.SetRow(Me.nudParentCategory, 2)
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.TLP.SetColumn(Me.LabelControl3, 1)
        Me.LabelControl3.Name = "LabelControl3"
        Me.TLP.SetRow(Me.LabelControl3, 2)
        '
        'tbCategoryName
        '
        resources.ApplyResources(Me.tbCategoryName, "tbCategoryName")
        Me.TLP.SetColumn(Me.tbCategoryName, 2)
        Me.tbCategoryName.MenuManager = Me.ribbonControl
        Me.tbCategoryName.Name = "tbCategoryName"
        Me.tbCategoryName.Properties.Appearance.Font = CType(resources.GetObject("tbCategoryName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.tbCategoryName.Properties.Appearance.Options.UseFont = True
        Me.TLP.SetRow(Me.tbCategoryName, 1)
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.TLP.SetColumn(Me.LabelControl2, 1)
        Me.LabelControl2.Name = "LabelControl2"
        Me.TLP.SetRow(Me.LabelControl2, 1)
        '
        'nudCategoryID
        '
        resources.ApplyResources(Me.nudCategoryID, "nudCategoryID")
        Me.TLP.SetColumn(Me.nudCategoryID, 2)
        Me.nudCategoryID.MenuManager = Me.ribbonControl
        Me.nudCategoryID.Name = "nudCategoryID"
        Me.nudCategoryID.Properties.Appearance.Font = CType(resources.GetObject("nudCategoryID.Properties.Appearance.Font"), System.Drawing.Font)
        Me.nudCategoryID.Properties.Appearance.Options.UseFont = True
        Me.TLP.SetRow(Me.nudCategoryID, 0)
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.TLP.SetColumn(Me.LabelControl1, 1)
        Me.LabelControl1.Name = "LabelControl1"
        Me.TLP.SetRow(Me.LabelControl1, 0)
        '
        'TblCategForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.SS)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "TblCategForm"
        Me.Ribbon = Me.ribbonControl
        Me.StatusBar = Me.SS
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gridCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ribbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarAndDockingController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLPBut.ResumeLayout(False)
        CType(Me.TLP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLP.ResumeLayout(False)
        Me.TLP.PerformLayout()
        CType(Me.nudParentCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCategoryName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCategoryID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private WithEvents gridCode As DevExpress.XtraGrid.GridControl
    Private WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents ribbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Private WithEvents ribbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Private WithEvents ribbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Private WithEvents bbiPrintPreview As DevExpress.XtraBars.BarButtonItem
    Private WithEvents ribbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Private WithEvents SS As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Private WithEvents bsiRecordsCount As DevExpress.XtraBars.BarStaticItem
    Private WithEvents bAdd As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bEdit As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bDelete As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarAndDockingController1 As DevExpress.XtraBars.BarAndDockingController
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colParentCategory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCategoryName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCategoryID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents TLPBut As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents butApply As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents butCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents butClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TLP As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents nudParentCategory As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbCategoryName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents nudCategoryID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents bCancel As DevExpress.XtraBars.BarButtonItem
End Class
