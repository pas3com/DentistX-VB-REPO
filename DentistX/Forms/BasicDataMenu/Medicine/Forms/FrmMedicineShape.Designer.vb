<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMedicineShape
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
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colShapeID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicineItemID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicineShape = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colShapeInfo = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.ShapeIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.ShapeIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.MedicineItemIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.MedicineItemsCombo = New DentistX.MedicineItemsCombo()
        Me.MedicineShapeLabel = New DevExpress.XtraEditors.LabelControl()
        Me.MedicineShapeTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.ShapeInfoLabel = New DevExpress.XtraEditors.LabelControl()
        Me.ShapeInfoTextEdit = New DevExpress.XtraEditors.TextEdit()
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
        CType(Me.ShapeIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineShapeTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ShapeInfoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLPBut.SuspendLayout()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Splitter1.Horizontal = False
        Me.Splitter1.Location = New System.Drawing.Point(0, 153)
        Me.Splitter1.Name = "Splitter1"
        '
        'Splitter1.Panel1
        '
        Me.Splitter1.Panel1.Controls.Add(Me.DGV)
        '
        'Splitter1.Panel2
        '
        Me.Splitter1.Panel2.Controls.Add(Me.TableData)
        Me.Splitter1.Size = New System.Drawing.Size(992, 586)
        Me.Splitter1.SplitterPosition = 293
        Me.Splitter1.TabIndex = 0
        '
        'DGV
        '
        Me.DGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.DGV.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.DGV.Location = New System.Drawing.Point(0, 0)
        Me.DGV.MainView = Me.dgView
        Me.DGV.MenuManager = Me.ribbonControl
        Me.DGV.Name = "DGV"
        Me.DGV.Size = New System.Drawing.Size(992, 293)
        Me.DGV.TabIndex = 0
        Me.DGV.UseEmbeddedNavigator = True
        Me.DGV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        Me.dgView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
        Me.dgView.Appearance.Row.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.dgView.Appearance.Row.Options.UseFont = True
        Me.dgView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colShapeID, Me.colMedicineItemID, Me.colMedicineShape, Me.colShapeInfo})
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
        Me.colRowNum.Caption = "Number"
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        Me.colRowNum.Visible = True
        Me.colRowNum.VisibleIndex = 0
        '
        'colShapeID
        '
        Me.colShapeID.FieldName = "ShapeID"
        Me.colShapeID.Name = "colShapeID"
        Me.colShapeID.Visible = True
        Me.colShapeID.VisibleIndex = 1
        '
        'colMedicineItemID
        '
        Me.colMedicineItemID.FieldName = "MedicineItemID"
        Me.colMedicineItemID.Name = "colMedicineItemID"
        Me.colMedicineItemID.Visible = True
        Me.colMedicineItemID.VisibleIndex = 2
        '
        'colMedicineShape
        '
        Me.colMedicineShape.FieldName = "MedicineShape"
        Me.colMedicineShape.Name = "colMedicineShape"
        Me.colMedicineShape.Visible = True
        Me.colMedicineShape.VisibleIndex = 3
        '
        'colShapeInfo
        '
        Me.colShapeInfo.FieldName = "ShapeInfo"
        Me.colShapeInfo.Name = "colShapeInfo"
        Me.colShapeInfo.Visible = True
        Me.colShapeInfo.VisibleIndex = 4
        '
        'ribbonControl
        '
        Me.ribbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(30, 32, 30, 32)
        Me.ribbonControl.ExpandCollapseItem.Id = 0
        Me.ribbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.ribbonControl.ExpandCollapseItem, Me.bbiPrintPreview, Me.bsiRecordsCount, Me.bAdd, Me.bEdit, Me.bDelete, Me.bRefresh, Me.bCancel})
        Me.ribbonControl.Location = New System.Drawing.Point(0, 0)
        Me.ribbonControl.MaxItemId = 21
        Me.ribbonControl.Name = "ribbonControl"
        Me.ribbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.ribbonPage1})
        Me.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013
        Me.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribbonControl.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.[False]
        Me.ribbonControl.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide
        Me.ribbonControl.ShowQatLocationSelector = False
        Me.ribbonControl.ShowToolbarCustomizeItem = False
        Me.ribbonControl.Size = New System.Drawing.Size(992, 153)
        Me.ribbonControl.StatusBar = Me.ribbonStatusBar
        Me.ribbonControl.Toolbar.ShowCustomizeItem = False
        Me.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden
        '
        'bbiPrintPreview
        '
        Me.bbiPrintPreview.Caption = "Print Preview"
        Me.bbiPrintPreview.Id = 14
        Me.bbiPrintPreview.ImageOptions.ImageUri.Uri = "Preview"
        Me.bbiPrintPreview.ItemAppearance.Normal.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
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
        Me.bAdd.Caption = "Add"
        Me.bAdd.Id = 16
        Me.bAdd.ImageOptions.ImageUri.Uri = "New"
        Me.bAdd.ItemAppearance.Normal.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.bAdd.ItemAppearance.Normal.Options.UseFont = True
        Me.bAdd.Name = "bAdd"
        '
        'bEdit
        '
        Me.bEdit.Caption = "Edit"
        Me.bEdit.Id = 17
        Me.bEdit.ImageOptions.ImageUri.Uri = "Edit"
        Me.bEdit.ItemAppearance.Normal.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.bEdit.ItemAppearance.Normal.Options.UseFont = True
        Me.bEdit.Name = "bEdit"
        '
        'bDelete
        '
        Me.bDelete.Caption = "Delete"
        Me.bDelete.Id = 18
        Me.bDelete.ImageOptions.ImageUri.Uri = "Delete"
        Me.bDelete.ItemAppearance.Normal.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.bDelete.ItemAppearance.Normal.Options.UseFont = True
        Me.bDelete.Name = "bDelete"
        '
        'bRefresh
        '
        Me.bRefresh.Caption = "Refresh"
        Me.bRefresh.Id = 19
        Me.bRefresh.ImageOptions.ImageUri.Uri = "Refresh"
        Me.bRefresh.ItemAppearance.Normal.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.bRefresh.ItemAppearance.Normal.Options.UseFont = True
        Me.bRefresh.Name = "bRefresh"
        '
        'bCancel
        '
        Me.bCancel.Caption = "Cancel"
        Me.bCancel.Id = 20
        Me.bCancel.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.undo
        Me.bCancel.ItemAppearance.Normal.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
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
        Me.ribbonStatusBar.Location = New System.Drawing.Point(0, 739)
        Me.ribbonStatusBar.Name = "ribbonStatusBar"
        Me.ribbonStatusBar.Ribbon = Me.ribbonControl
        Me.ribbonStatusBar.Size = New System.Drawing.Size(992, 25)
        '
        'TableData
        '
        Me.TableData.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.0!)})
        Me.TableData.Controls.Add(Me.ShapeIDLabel)
        Me.TableData.Controls.Add(Me.ShapeIDSpinEdit)
        Me.TableData.Controls.Add(Me.MedicineItemIDLabel)
        Me.TableData.Controls.Add(Me.MedicineItemsCombo)
        Me.TableData.Controls.Add(Me.MedicineShapeLabel)
        Me.TableData.Controls.Add(Me.MedicineShapeTextEdit)
        Me.TableData.Controls.Add(Me.ShapeInfoLabel)
        Me.TableData.Controls.Add(Me.ShapeInfoTextEdit)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableData.Location = New System.Drawing.Point(0, 0)
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TableData.Size = New System.Drawing.Size(992, 287)
        Me.TableData.TabIndex = 0
        Me.TableData.UseSkinIndents = True
        '
        'ShapeIDLabel
        '
        Me.ShapeIDLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ShapeIDLabel.Appearance.Options.UseFont = True
        Me.ShapeIDLabel.Appearance.Options.UseTextOptions = True
        Me.ShapeIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ShapeIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ShapeIDLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.ShapeIDLabel, 1)
        Me.ShapeIDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ShapeIDLabel.Location = New System.Drawing.Point(207, 12)
        Me.ShapeIDLabel.Name = "ShapeIDLabel"
        Me.TableData.SetRow(Me.ShapeIDLabel, 0)
        Me.ShapeIDLabel.Size = New System.Drawing.Size(135, 22)
        Me.ShapeIDLabel.TabIndex = 0
        Me.ShapeIDLabel.Text = "ShapeID"
        '
        'ShapeIDSpinEdit
        '
        Me.TableData.SetColumn(Me.ShapeIDSpinEdit, 2)
        Me.ShapeIDSpinEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ShapeIDSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.ShapeIDSpinEdit.Enabled = False
        Me.ShapeIDSpinEdit.Location = New System.Drawing.Point(346, 12)
        Me.ShapeIDSpinEdit.Name = "ShapeIDSpinEdit"
        Me.ShapeIDSpinEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ShapeIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.ShapeIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ShapeIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ShapeIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.ShapeIDSpinEdit, 0)
        Me.ShapeIDSpinEdit.Size = New System.Drawing.Size(135, 22)
        Me.ShapeIDSpinEdit.TabIndex = 1
        '
        'MedicineItemIDLabel
        '
        Me.MedicineItemIDLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MedicineItemIDLabel.Appearance.Options.UseFont = True
        Me.MedicineItemIDLabel.Appearance.Options.UseTextOptions = True
        Me.MedicineItemIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MedicineItemIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.MedicineItemIDLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.MedicineItemIDLabel, 4)
        Me.MedicineItemIDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedicineItemIDLabel.Location = New System.Drawing.Point(512, 12)
        Me.MedicineItemIDLabel.Name = "MedicineItemIDLabel"
        Me.TableData.SetRow(Me.MedicineItemIDLabel, 0)
        Me.MedicineItemIDLabel.Size = New System.Drawing.Size(135, 22)
        Me.MedicineItemIDLabel.TabIndex = 0
        Me.MedicineItemIDLabel.Text = "MedicineItemID"
        '
        'MedicineItemsCombo
        '
        Me.MedicineItemsCombo.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MedicineItemsCombo.Appearance.Options.UseFont = True
        Me.TableData.SetColumn(Me.MedicineItemsCombo, 5)
        Me.MedicineItemsCombo.CommName = Nothing
        Me.MedicineItemsCombo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedicineItemsCombo.Location = New System.Drawing.Point(650, 12)
        Me.MedicineItemsCombo.MedicineItemID = 0
        Me.MedicineItemsCombo.Name = "MedicineItemsCombo"
        Me.TableData.SetRow(Me.MedicineItemsCombo, 0)
        Me.MedicineItemsCombo.Size = New System.Drawing.Size(135, 22)
        Me.MedicineItemsCombo.TabIndex = 1
        '
        'MedicineShapeLabel
        '
        Me.MedicineShapeLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MedicineShapeLabel.Appearance.Options.UseFont = True
        Me.MedicineShapeLabel.Appearance.Options.UseTextOptions = True
        Me.MedicineShapeLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MedicineShapeLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.MedicineShapeLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.MedicineShapeLabel, 1)
        Me.MedicineShapeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedicineShapeLabel.Location = New System.Drawing.Point(207, 38)
        Me.MedicineShapeLabel.Name = "MedicineShapeLabel"
        Me.TableData.SetRow(Me.MedicineShapeLabel, 1)
        Me.MedicineShapeLabel.Size = New System.Drawing.Size(135, 22)
        Me.MedicineShapeLabel.TabIndex = 2
        Me.MedicineShapeLabel.Text = "MedicineShape"
        '
        'MedicineShapeTextEdit
        '
        Me.TableData.SetColumn(Me.MedicineShapeTextEdit, 2)
        Me.MedicineShapeTextEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedicineShapeTextEdit.Location = New System.Drawing.Point(346, 38)
        Me.MedicineShapeTextEdit.Name = "MedicineShapeTextEdit"
        Me.MedicineShapeTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MedicineShapeTextEdit.Properties.Appearance.Options.UseFont = True
        Me.MedicineShapeTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.MedicineShapeTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MedicineShapeTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.MedicineShapeTextEdit, 1)
        Me.MedicineShapeTextEdit.Size = New System.Drawing.Size(135, 22)
        Me.MedicineShapeTextEdit.TabIndex = 3
        '
        'ShapeInfoLabel
        '
        Me.ShapeInfoLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ShapeInfoLabel.Appearance.Options.UseFont = True
        Me.ShapeInfoLabel.Appearance.Options.UseTextOptions = True
        Me.ShapeInfoLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ShapeInfoLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ShapeInfoLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.ShapeInfoLabel, 4)
        Me.ShapeInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ShapeInfoLabel.Location = New System.Drawing.Point(512, 38)
        Me.ShapeInfoLabel.Name = "ShapeInfoLabel"
        Me.TableData.SetRow(Me.ShapeInfoLabel, 1)
        Me.ShapeInfoLabel.Size = New System.Drawing.Size(135, 22)
        Me.ShapeInfoLabel.TabIndex = 2
        Me.ShapeInfoLabel.Text = "ShapeInfo"
        '
        'ShapeInfoTextEdit
        '
        Me.TableData.SetColumn(Me.ShapeInfoTextEdit, 5)
        Me.ShapeInfoTextEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ShapeInfoTextEdit.Location = New System.Drawing.Point(650, 38)
        Me.ShapeInfoTextEdit.Name = "ShapeInfoTextEdit"
        Me.ShapeInfoTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ShapeInfoTextEdit.Properties.Appearance.Options.UseFont = True
        Me.ShapeInfoTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ShapeInfoTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ShapeInfoTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.ShapeInfoTextEdit, 1)
        Me.ShapeInfoTextEdit.Size = New System.Drawing.Size(135, 22)
        Me.ShapeInfoTextEdit.TabIndex = 3
        '
        'TLPBut
        '
        Me.TableData.SetColumn(Me.TLPBut, 0)
        Me.TLPBut.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!)})
        Me.TableData.SetColumnSpan(Me.TLPBut, 7)
        Me.TLPBut.Controls.Add(Me.butApply)
        Me.TLPBut.Controls.Add(Me.butCancel)
        Me.TLPBut.Controls.Add(Me.butClose)
        Me.TLPBut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPBut.Location = New System.Drawing.Point(13, 64)
        Me.TLPBut.Name = "TLPBut"
        Me.TableData.SetRow(Me.TLPBut, 3)
        Me.TLPBut.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TLPBut.Size = New System.Drawing.Size(966, 210)
        Me.TLPBut.TabIndex = 4
        Me.TLPBut.UseSkinIndents = True
        '
        'butApply
        '
        Me.butApply.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.butApply.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.butApply, 1)
        Me.butApply.Location = New System.Drawing.Point(283, 93)
        Me.butApply.Name = "butApply"
        Me.TLPBut.SetRow(Me.butApply, 0)
        Me.butApply.Size = New System.Drawing.Size(77, 23)
        Me.butApply.TabIndex = 0
        Me.butApply.Text = "Apply"
        '
        'butCancel
        '
        Me.butCancel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.butCancel.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.butCancel, 3)
        Me.butCancel.Location = New System.Drawing.Point(445, 93)
        Me.butCancel.Name = "butCancel"
        Me.TLPBut.SetRow(Me.butCancel, 0)
        Me.butCancel.Size = New System.Drawing.Size(77, 23)
        Me.butCancel.TabIndex = 1
        Me.butCancel.Text = "Cancel"
        '
        'butClose
        '
        Me.butClose.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.butClose.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.butClose, 5)
        Me.butClose.Location = New System.Drawing.Point(606, 93)
        Me.butClose.Name = "butClose"
        Me.TLPBut.SetRow(Me.butClose, 0)
        Me.butClose.Size = New System.Drawing.Size(77, 23)
        Me.butClose.TabIndex = 2
        Me.butClose.Text = "Close"
        '
        'FrmMedicineShape
        '
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 764)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ribbonStatusBar)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "FrmMedicineShape"
        Me.Ribbon = Me.ribbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.StatusBar = Me.ribbonStatusBar
        Me.Text = "FrmMedicineShape"
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
        CType(Me.ShapeIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineShapeTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ShapeInfoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
		Friend WithEvents ShapeIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents ShapeIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colShapeID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents MedicineItemIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents MedicineItemIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colMedicineItemID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents MedicineShapeTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents MedicineShapeLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colMedicineShape As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents ShapeInfoTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents ShapeInfoLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colShapeInfo As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents MedicineItemsCombo As DentistX.MedicineItemsCombo

End Class
