<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDrWork
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDrWork))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWorkID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDrID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDrName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWrkDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWrkDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWrkVal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayVal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colImp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOrth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSurg = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.WorkIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.WorkIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.DrIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DrIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.PatientIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PatientIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.WrkDateLabel = New DevExpress.XtraEditors.LabelControl()
        Me.WrkDateDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.WrkDetailLabel = New DevExpress.XtraEditors.LabelControl()
        Me.WrkDetailTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.WrkValLabel = New DevExpress.XtraEditors.LabelControl()
        Me.WrkValSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.PayValLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PayValSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.ImpLabel = New DevExpress.XtraEditors.LabelControl()
        Me.ImpCheckEdit = New DevExpress.XtraEditors.CheckEdit()
        Me.OrthLabel = New DevExpress.XtraEditors.LabelControl()
        Me.OrthCheckEdit = New DevExpress.XtraEditors.CheckEdit()
        Me.SurgLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SurgCheckEdit = New DevExpress.XtraEditors.CheckEdit()
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
        CType(Me.WorkIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WrkDateDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WrkDateDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WrkDetailTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WrkValSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayValSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImpCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OrthCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SurgCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colWorkID, Me.colDrID, Me.colDrName, Me.colPatientID, Me.colPatientName, Me.colWrkDate, Me.colWrkDetail, Me.colWrkVal, Me.colPayVal, Me.colImp, Me.colOrth, Me.colSurg, Me.colNotes})
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
        'colWorkID
        '
        resources.ApplyResources(Me.colWorkID, "colWorkID")
        Me.colWorkID.FieldName = "WorkID"
        Me.colWorkID.ImageOptions.ImageKey = resources.GetString("colWorkID.ImageOptions.ImageKey")
        Me.colWorkID.Name = "colWorkID"
        '
        'colDrID
        '
        resources.ApplyResources(Me.colDrID, "colDrID")
        Me.colDrID.FieldName = "DrID"
        Me.colDrID.ImageOptions.ImageKey = resources.GetString("colDrID.ImageOptions.ImageKey")
        Me.colDrID.Name = "colDrID"
        '
        'colDrName
        '
        resources.ApplyResources(Me.colDrName, "colDrName")
        Me.colDrName.FieldName = "DrName"
        Me.colDrName.ImageOptions.ImageKey = resources.GetString("colDrName.ImageOptions.ImageKey")
        Me.colDrName.Name = "colDrName"
        '
        'colPatientID
        '
        resources.ApplyResources(Me.colPatientID, "colPatientID")
        Me.colPatientID.FieldName = "PatientID"
        Me.colPatientID.ImageOptions.ImageKey = resources.GetString("colPatientID.ImageOptions.ImageKey")
        Me.colPatientID.Name = "colPatientID"
        '
        'colPatientName
        '
        resources.ApplyResources(Me.colPatientName, "colPatientName")
        Me.colPatientName.FieldName = "PatientName"
        Me.colPatientName.ImageOptions.ImageKey = resources.GetString("colPatientName.ImageOptions.ImageKey")
        Me.colPatientName.Name = "colPatientName"
        '
        'colWrkDate
        '
        resources.ApplyResources(Me.colWrkDate, "colWrkDate")
        Me.colWrkDate.FieldName = "WrkDate"
        Me.colWrkDate.ImageOptions.ImageKey = resources.GetString("colWrkDate.ImageOptions.ImageKey")
        Me.colWrkDate.Name = "colWrkDate"
        '
        'colWrkDetail
        '
        resources.ApplyResources(Me.colWrkDetail, "colWrkDetail")
        Me.colWrkDetail.FieldName = "WrkDetail"
        Me.colWrkDetail.ImageOptions.ImageKey = resources.GetString("colWrkDetail.ImageOptions.ImageKey")
        Me.colWrkDetail.Name = "colWrkDetail"
        '
        'colWrkVal
        '
        resources.ApplyResources(Me.colWrkVal, "colWrkVal")
        Me.colWrkVal.FieldName = "WrkVal"
        Me.colWrkVal.ImageOptions.ImageKey = resources.GetString("colWrkVal.ImageOptions.ImageKey")
        Me.colWrkVal.Name = "colWrkVal"
        '
        'colPayVal
        '
        resources.ApplyResources(Me.colPayVal, "colPayVal")
        Me.colPayVal.FieldName = "PayVal"
        Me.colPayVal.ImageOptions.ImageKey = resources.GetString("colPayVal.ImageOptions.ImageKey")
        Me.colPayVal.Name = "colPayVal"
        '
        'colImp
        '
        resources.ApplyResources(Me.colImp, "colImp")
        Me.colImp.FieldName = "Imp"
        Me.colImp.ImageOptions.ImageKey = resources.GetString("colImp.ImageOptions.ImageKey")
        Me.colImp.Name = "colImp"
        '
        'colOrth
        '
        resources.ApplyResources(Me.colOrth, "colOrth")
        Me.colOrth.FieldName = "Orth"
        Me.colOrth.ImageOptions.ImageKey = resources.GetString("colOrth.ImageOptions.ImageKey")
        Me.colOrth.Name = "colOrth"
        '
        'colSurg
        '
        resources.ApplyResources(Me.colSurg, "colSurg")
        Me.colSurg.FieldName = "Surg"
        Me.colSurg.ImageOptions.ImageKey = resources.GetString("colSurg.ImageOptions.ImageKey")
        Me.colSurg.Name = "colSurg"
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
        Me.TableData.Controls.Add(Me.WorkIDLabel)
        Me.TableData.Controls.Add(Me.WorkIDSpinEdit)
        Me.TableData.Controls.Add(Me.DrIDLabel)
        Me.TableData.Controls.Add(Me.DrIDSpinEdit)
        Me.TableData.Controls.Add(Me.PatientIDLabel)
        Me.TableData.Controls.Add(Me.PatientIDSpinEdit)
        Me.TableData.Controls.Add(Me.WrkDateLabel)
        Me.TableData.Controls.Add(Me.WrkDateDateEdit)
        Me.TableData.Controls.Add(Me.WrkDetailLabel)
        Me.TableData.Controls.Add(Me.WrkDetailTextEdit)
        Me.TableData.Controls.Add(Me.WrkValLabel)
        Me.TableData.Controls.Add(Me.WrkValSpinEdit)
        Me.TableData.Controls.Add(Me.PayValLabel)
        Me.TableData.Controls.Add(Me.PayValSpinEdit)
        Me.TableData.Controls.Add(Me.ImpLabel)
        Me.TableData.Controls.Add(Me.ImpCheckEdit)
        Me.TableData.Controls.Add(Me.OrthLabel)
        Me.TableData.Controls.Add(Me.OrthCheckEdit)
        Me.TableData.Controls.Add(Me.SurgLabel)
        Me.TableData.Controls.Add(Me.SurgCheckEdit)
        Me.TableData.Controls.Add(Me.NotesLabel)
        Me.TableData.Controls.Add(Me.NotesTextEdit)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TableData.UseSkinIndents = True
        '
        'WorkIDLabel
        '
        resources.ApplyResources(Me.WorkIDLabel, "WorkIDLabel")
        Me.WorkIDLabel.Appearance.Font = CType(resources.GetObject("WorkIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.WorkIDLabel.Appearance.Options.UseFont = True
        Me.WorkIDLabel.Appearance.Options.UseTextOptions = True
        Me.WorkIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WorkIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.WorkIDLabel, 1)
        Me.WorkIDLabel.Name = "WorkIDLabel"
        Me.TableData.SetRow(Me.WorkIDLabel, 0)
        '
        'WorkIDSpinEdit
        '
        resources.ApplyResources(Me.WorkIDSpinEdit, "WorkIDSpinEdit")
        Me.TableData.SetColumn(Me.WorkIDSpinEdit, 2)
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
        Me.TableData.SetColumn(Me.DrIDLabel, 4)
        Me.DrIDLabel.Name = "DrIDLabel"
        Me.TableData.SetRow(Me.DrIDLabel, 0)
        '
        'DrIDSpinEdit
        '
        resources.ApplyResources(Me.DrIDSpinEdit, "DrIDSpinEdit")
        Me.TableData.SetColumn(Me.DrIDSpinEdit, 5)
        Me.DrIDSpinEdit.Name = "DrIDSpinEdit"
        Me.DrIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("DrIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DrIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.DrIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.DrIDSpinEdit, 0)
        '
        'PatientIDLabel
        '
        resources.ApplyResources(Me.PatientIDLabel, "PatientIDLabel")
        Me.PatientIDLabel.Appearance.Font = CType(resources.GetObject("PatientIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.PatientIDLabel.Appearance.Options.UseFont = True
        Me.PatientIDLabel.Appearance.Options.UseTextOptions = True
        Me.PatientIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PatientIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.PatientIDLabel, 1)
        Me.PatientIDLabel.Name = "PatientIDLabel"
        Me.TableData.SetRow(Me.PatientIDLabel, 1)
        '
        'PatientIDSpinEdit
        '
        resources.ApplyResources(Me.PatientIDSpinEdit, "PatientIDSpinEdit")
        Me.TableData.SetColumn(Me.PatientIDSpinEdit, 2)
        Me.PatientIDSpinEdit.Name = "PatientIDSpinEdit"
        Me.PatientIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("PatientIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PatientIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.PatientIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PatientIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PatientIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.PatientIDSpinEdit, 1)
        '
        'WrkDateLabel
        '
        resources.ApplyResources(Me.WrkDateLabel, "WrkDateLabel")
        Me.WrkDateLabel.Appearance.Font = CType(resources.GetObject("WrkDateLabel.Appearance.Font"), System.Drawing.Font)
        Me.WrkDateLabel.Appearance.Options.UseFont = True
        Me.WrkDateLabel.Appearance.Options.UseTextOptions = True
        Me.WrkDateLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WrkDateLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.WrkDateLabel, 4)
        Me.WrkDateLabel.Name = "WrkDateLabel"
        Me.TableData.SetRow(Me.WrkDateLabel, 1)
        '
        'WrkDateDateEdit
        '
        resources.ApplyResources(Me.WrkDateDateEdit, "WrkDateDateEdit")
        Me.TableData.SetColumn(Me.WrkDateDateEdit, 5)
        Me.WrkDateDateEdit.Name = "WrkDateDateEdit"
        Me.WrkDateDateEdit.Properties.Appearance.Font = CType(resources.GetObject("WrkDateDateEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.WrkDateDateEdit.Properties.Appearance.Options.UseFont = True
        Me.WrkDateDateEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.WrkDateDateEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WrkDateDateEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.WrkDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("WrkDateDateEdit.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.WrkDateDateEdit, 1)
        '
        'WrkDetailLabel
        '
        resources.ApplyResources(Me.WrkDetailLabel, "WrkDetailLabel")
        Me.WrkDetailLabel.Appearance.Font = CType(resources.GetObject("WrkDetailLabel.Appearance.Font"), System.Drawing.Font)
        Me.WrkDetailLabel.Appearance.Options.UseFont = True
        Me.WrkDetailLabel.Appearance.Options.UseTextOptions = True
        Me.WrkDetailLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WrkDetailLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.WrkDetailLabel, 1)
        Me.WrkDetailLabel.Name = "WrkDetailLabel"
        Me.TableData.SetRow(Me.WrkDetailLabel, 2)
        '
        'WrkDetailTextEdit
        '
        resources.ApplyResources(Me.WrkDetailTextEdit, "WrkDetailTextEdit")
        Me.TableData.SetColumn(Me.WrkDetailTextEdit, 2)
        Me.WrkDetailTextEdit.Name = "WrkDetailTextEdit"
        Me.WrkDetailTextEdit.Properties.Appearance.Font = CType(resources.GetObject("WrkDetailTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.WrkDetailTextEdit.Properties.Appearance.Options.UseFont = True
        Me.WrkDetailTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.WrkDetailTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WrkDetailTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.WrkDetailTextEdit, 2)
        '
        'WrkValLabel
        '
        resources.ApplyResources(Me.WrkValLabel, "WrkValLabel")
        Me.WrkValLabel.Appearance.Font = CType(resources.GetObject("WrkValLabel.Appearance.Font"), System.Drawing.Font)
        Me.WrkValLabel.Appearance.Options.UseFont = True
        Me.WrkValLabel.Appearance.Options.UseTextOptions = True
        Me.WrkValLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WrkValLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.WrkValLabel, 4)
        Me.WrkValLabel.Name = "WrkValLabel"
        Me.TableData.SetRow(Me.WrkValLabel, 2)
        '
        'WrkValSpinEdit
        '
        resources.ApplyResources(Me.WrkValSpinEdit, "WrkValSpinEdit")
        Me.TableData.SetColumn(Me.WrkValSpinEdit, 5)
        Me.WrkValSpinEdit.Name = "WrkValSpinEdit"
        Me.WrkValSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("WrkValSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.WrkValSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.WrkValSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.WrkValSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WrkValSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.WrkValSpinEdit, 2)
        '
        'PayValLabel
        '
        resources.ApplyResources(Me.PayValLabel, "PayValLabel")
        Me.PayValLabel.Appearance.Font = CType(resources.GetObject("PayValLabel.Appearance.Font"), System.Drawing.Font)
        Me.PayValLabel.Appearance.Options.UseFont = True
        Me.PayValLabel.Appearance.Options.UseTextOptions = True
        Me.PayValLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayValLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.PayValLabel, 1)
        Me.PayValLabel.Name = "PayValLabel"
        Me.TableData.SetRow(Me.PayValLabel, 3)
        '
        'PayValSpinEdit
        '
        resources.ApplyResources(Me.PayValSpinEdit, "PayValSpinEdit")
        Me.TableData.SetColumn(Me.PayValSpinEdit, 2)
        Me.PayValSpinEdit.Name = "PayValSpinEdit"
        Me.PayValSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("PayValSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PayValSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.PayValSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PayValSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayValSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.PayValSpinEdit, 3)
        '
        'ImpLabel
        '
        resources.ApplyResources(Me.ImpLabel, "ImpLabel")
        Me.ImpLabel.Appearance.Font = CType(resources.GetObject("ImpLabel.Appearance.Font"), System.Drawing.Font)
        Me.ImpLabel.Appearance.Options.UseFont = True
        Me.ImpLabel.Appearance.Options.UseTextOptions = True
        Me.ImpLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ImpLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.ImpLabel, 4)
        Me.ImpLabel.Name = "ImpLabel"
        Me.TableData.SetRow(Me.ImpLabel, 3)
        '
        'ImpCheckEdit
        '
        resources.ApplyResources(Me.ImpCheckEdit, "ImpCheckEdit")
        Me.TableData.SetColumn(Me.ImpCheckEdit, 5)
        Me.ImpCheckEdit.Name = "ImpCheckEdit"
        Me.ImpCheckEdit.Properties.Appearance.Font = CType(resources.GetObject("ImpCheckEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ImpCheckEdit.Properties.Appearance.Options.UseFont = True
        Me.ImpCheckEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ImpCheckEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ImpCheckEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ImpCheckEdit.Properties.Caption = resources.GetString("ImpCheckEdit.Properties.Caption")
        Me.ImpCheckEdit.Properties.DisplayValueChecked = resources.GetString("ImpCheckEdit.Properties.DisplayValueChecked")
        Me.ImpCheckEdit.Properties.DisplayValueGrayed = resources.GetString("ImpCheckEdit.Properties.DisplayValueGrayed")
        Me.ImpCheckEdit.Properties.DisplayValueUnchecked = resources.GetString("ImpCheckEdit.Properties.DisplayValueUnchecked")
        Me.ImpCheckEdit.Properties.GlyphVerticalAlignment = CType(resources.GetObject("ImpCheckEdit.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        Me.TableData.SetRow(Me.ImpCheckEdit, 3)
        '
        'OrthLabel
        '
        resources.ApplyResources(Me.OrthLabel, "OrthLabel")
        Me.OrthLabel.Appearance.Font = CType(resources.GetObject("OrthLabel.Appearance.Font"), System.Drawing.Font)
        Me.OrthLabel.Appearance.Options.UseFont = True
        Me.OrthLabel.Appearance.Options.UseTextOptions = True
        Me.OrthLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.OrthLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.OrthLabel, 1)
        Me.OrthLabel.Name = "OrthLabel"
        Me.TableData.SetRow(Me.OrthLabel, 4)
        '
        'OrthCheckEdit
        '
        resources.ApplyResources(Me.OrthCheckEdit, "OrthCheckEdit")
        Me.TableData.SetColumn(Me.OrthCheckEdit, 2)
        Me.OrthCheckEdit.Name = "OrthCheckEdit"
        Me.OrthCheckEdit.Properties.Appearance.Font = CType(resources.GetObject("OrthCheckEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.OrthCheckEdit.Properties.Appearance.Options.UseFont = True
        Me.OrthCheckEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.OrthCheckEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.OrthCheckEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.OrthCheckEdit.Properties.Caption = resources.GetString("OrthCheckEdit.Properties.Caption")
        Me.OrthCheckEdit.Properties.DisplayValueChecked = resources.GetString("OrthCheckEdit.Properties.DisplayValueChecked")
        Me.OrthCheckEdit.Properties.DisplayValueGrayed = resources.GetString("OrthCheckEdit.Properties.DisplayValueGrayed")
        Me.OrthCheckEdit.Properties.DisplayValueUnchecked = resources.GetString("OrthCheckEdit.Properties.DisplayValueUnchecked")
        Me.OrthCheckEdit.Properties.GlyphVerticalAlignment = CType(resources.GetObject("OrthCheckEdit.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        Me.TableData.SetRow(Me.OrthCheckEdit, 4)
        '
        'SurgLabel
        '
        resources.ApplyResources(Me.SurgLabel, "SurgLabel")
        Me.SurgLabel.Appearance.Font = CType(resources.GetObject("SurgLabel.Appearance.Font"), System.Drawing.Font)
        Me.SurgLabel.Appearance.Options.UseFont = True
        Me.SurgLabel.Appearance.Options.UseTextOptions = True
        Me.SurgLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SurgLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.SurgLabel, 4)
        Me.SurgLabel.Name = "SurgLabel"
        Me.TableData.SetRow(Me.SurgLabel, 4)
        '
        'SurgCheckEdit
        '
        resources.ApplyResources(Me.SurgCheckEdit, "SurgCheckEdit")
        Me.TableData.SetColumn(Me.SurgCheckEdit, 5)
        Me.SurgCheckEdit.Name = "SurgCheckEdit"
        Me.SurgCheckEdit.Properties.Appearance.Font = CType(resources.GetObject("SurgCheckEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SurgCheckEdit.Properties.Appearance.Options.UseFont = True
        Me.SurgCheckEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SurgCheckEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SurgCheckEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SurgCheckEdit.Properties.Caption = resources.GetString("SurgCheckEdit.Properties.Caption")
        Me.SurgCheckEdit.Properties.DisplayValueChecked = resources.GetString("SurgCheckEdit.Properties.DisplayValueChecked")
        Me.SurgCheckEdit.Properties.DisplayValueGrayed = resources.GetString("SurgCheckEdit.Properties.DisplayValueGrayed")
        Me.SurgCheckEdit.Properties.DisplayValueUnchecked = resources.GetString("SurgCheckEdit.Properties.DisplayValueUnchecked")
        Me.SurgCheckEdit.Properties.GlyphVerticalAlignment = CType(resources.GetObject("SurgCheckEdit.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        Me.TableData.SetRow(Me.SurgCheckEdit, 4)
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
        Me.TableData.SetRow(Me.NotesLabel, 5)
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
        Me.TableData.SetRow(Me.NotesTextEdit, 5)
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
        Me.TableData.SetRow(Me.TLPBut, 6)
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
        'FrmDrWork
        '
        resources.ApplyResources(Me, "$this")
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ribbonStatusBar)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "FrmDrWork"
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
        CType(Me.WorkIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WrkDateDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WrkDateDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WrkDetailTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WrkValSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayValSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImpCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OrthCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SurgCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
		Friend WithEvents WorkIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents WorkIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colWorkID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents DrIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents DrIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colDrID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PatientIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents PatientIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents WrkDateDateEdit As DevExpress.XtraEditors.DateEdit
		Friend WithEvents WrkDateLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colWrkDate As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents WrkDetailTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents WrkDetailLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colWrkDetail As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents WrkValSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents WrkValLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colWrkVal As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PayValSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents PayValLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPayVal As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents ImpCheckEdit As DevExpress.XtraEditors.CheckEdit
		Friend WithEvents ImpLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colImp As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents OrthCheckEdit As DevExpress.XtraEditors.CheckEdit
		Friend WithEvents OrthLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colOrth As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents SurgCheckEdit As DevExpress.XtraEditors.CheckEdit
		Friend WithEvents SurgLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colSurg As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents NotesTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents NotesLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDrName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientName As DevExpress.XtraGrid.Columns.GridColumn
End Class
