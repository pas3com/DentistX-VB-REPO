<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMedicineFamily
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
        Me.colSubCatID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicineID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicineSubCat = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.SubCatIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SubCatIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.MedicineIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.MedicineGroupsCombo = New DentistX.MedicineGroupsCombo()
        Me.MedicineSubCatLabel = New DevExpress.XtraEditors.LabelControl()
        Me.MedicineSubCatTextEdit = New DevExpress.XtraEditors.TextEdit()
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
        CType(Me.SubCatIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineSubCatTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colSubCatID, Me.colMedicineID, Me.colMedicineSubCat})
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
        'colSubCatID
        '
        Me.colSubCatID.FieldName = "SubCatID"
        Me.colSubCatID.Name = "colSubCatID"
        Me.colSubCatID.Visible = True
        Me.colSubCatID.VisibleIndex = 1
        '
        'colMedicineID
        '
        Me.colMedicineID.FieldName = "MedicineSubCat"
        Me.colMedicineID.Name = "colMedicineID"
        Me.colMedicineID.Visible = True
        Me.colMedicineID.VisibleIndex = 2
        '
        'colMedicineSubCat
        '
        Me.colMedicineSubCat.FieldName = "MedicineSubCat"
        Me.colMedicineSubCat.Name = "colMedicineSubCat"
        Me.colMedicineSubCat.Visible = True
        Me.colMedicineSubCat.VisibleIndex = 3
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
        Me.TableData.Controls.Add(Me.SubCatIDLabel)
        Me.TableData.Controls.Add(Me.SubCatIDSpinEdit)
        Me.TableData.Controls.Add(Me.MedicineIDLabel)
        Me.TableData.Controls.Add(Me.MedicineGroupsCombo)
        Me.TableData.Controls.Add(Me.MedicineSubCatLabel)
        Me.TableData.Controls.Add(Me.MedicineSubCatTextEdit)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableData.Location = New System.Drawing.Point(0, 0)
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TableData.Size = New System.Drawing.Size(992, 287)
        Me.TableData.TabIndex = 0
        Me.TableData.UseSkinIndents = True
        '
        'SubCatIDLabel
        '
        Me.SubCatIDLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SubCatIDLabel.Appearance.Options.UseFont = True
        Me.SubCatIDLabel.Appearance.Options.UseTextOptions = True
        Me.SubCatIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SubCatIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SubCatIDLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.SubCatIDLabel, 1)
        Me.SubCatIDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SubCatIDLabel.Location = New System.Drawing.Point(207, 12)
        Me.SubCatIDLabel.Name = "SubCatIDLabel"
        Me.TableData.SetRow(Me.SubCatIDLabel, 0)
        Me.SubCatIDLabel.Size = New System.Drawing.Size(135, 22)
        Me.SubCatIDLabel.TabIndex = 0
        Me.SubCatIDLabel.Text = "SubCatID"
        '
        'SubCatIDSpinEdit
        '
        Me.TableData.SetColumn(Me.SubCatIDSpinEdit, 2)
        Me.SubCatIDSpinEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SubCatIDSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.SubCatIDSpinEdit.Enabled = False
        Me.SubCatIDSpinEdit.Location = New System.Drawing.Point(346, 12)
        Me.SubCatIDSpinEdit.Name = "SubCatIDSpinEdit"
        Me.SubCatIDSpinEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SubCatIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.SubCatIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SubCatIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SubCatIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.SubCatIDSpinEdit, 0)
        Me.SubCatIDSpinEdit.Size = New System.Drawing.Size(135, 22)
        Me.SubCatIDSpinEdit.TabIndex = 1
        '
        'MedicineIDLabel
        '
        Me.MedicineIDLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MedicineIDLabel.Appearance.Options.UseFont = True
        Me.MedicineIDLabel.Appearance.Options.UseTextOptions = True
        Me.MedicineIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MedicineIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.MedicineIDLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.MedicineIDLabel, 4)
        Me.MedicineIDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedicineIDLabel.Location = New System.Drawing.Point(512, 12)
        Me.MedicineIDLabel.Name = "MedicineIDLabel"
        Me.TableData.SetRow(Me.MedicineIDLabel, 0)
        Me.MedicineIDLabel.Size = New System.Drawing.Size(135, 22)
        Me.MedicineIDLabel.TabIndex = 0
        Me.MedicineIDLabel.Text = "MedicineSubCat"
        '
        'MedicineGroupsCombo
        '
        Me.MedicineGroupsCombo.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MedicineGroupsCombo.Appearance.Options.UseFont = True
        Me.TableData.SetColumn(Me.MedicineGroupsCombo, 5)
        Me.MedicineGroupsCombo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedicineGroupsCombo.Location = New System.Drawing.Point(650, 12)
        Me.MedicineGroupsCombo.MedicineFamily = Nothing
        Me.MedicineGroupsCombo.MedicineID = 0
        Me.MedicineGroupsCombo.Name = "MedicineGroupsCombo"
        Me.TableData.SetRow(Me.MedicineGroupsCombo, 0)
        Me.MedicineGroupsCombo.Size = New System.Drawing.Size(135, 22)
        Me.MedicineGroupsCombo.TabIndex = 1
        '
        'MedicineSubCatLabel
        '
        Me.MedicineSubCatLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MedicineSubCatLabel.Appearance.Options.UseFont = True
        Me.MedicineSubCatLabel.Appearance.Options.UseTextOptions = True
        Me.MedicineSubCatLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MedicineSubCatLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.MedicineSubCatLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.MedicineSubCatLabel, 1)
        Me.MedicineSubCatLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedicineSubCatLabel.Location = New System.Drawing.Point(207, 38)
        Me.MedicineSubCatLabel.Name = "MedicineSubCatLabel"
        Me.TableData.SetRow(Me.MedicineSubCatLabel, 1)
        Me.MedicineSubCatLabel.Size = New System.Drawing.Size(135, 22)
        Me.MedicineSubCatLabel.TabIndex = 2
        Me.MedicineSubCatLabel.Text = "MedicineSubCat"
        '
        'MedicineSubCatTextEdit
        '
        Me.TableData.SetColumn(Me.MedicineSubCatTextEdit, 2)
        Me.MedicineSubCatTextEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedicineSubCatTextEdit.Location = New System.Drawing.Point(346, 38)
        Me.MedicineSubCatTextEdit.Name = "MedicineSubCatTextEdit"
        Me.MedicineSubCatTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MedicineSubCatTextEdit.Properties.Appearance.Options.UseFont = True
        Me.MedicineSubCatTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.MedicineSubCatTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MedicineSubCatTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.MedicineSubCatTextEdit, 1)
        Me.MedicineSubCatTextEdit.Size = New System.Drawing.Size(135, 22)
        Me.MedicineSubCatTextEdit.TabIndex = 3
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
        Me.TableData.SetRow(Me.TLPBut, 2)
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
        'FrmMedicineFamily
        '
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 764)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ribbonStatusBar)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "FrmMedicineFamily"
        Me.Ribbon = Me.ribbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.StatusBar = Me.ribbonStatusBar
        Me.Text = "FrmMedicineFamily"
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
        CType(Me.SubCatIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineSubCatTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
		Friend WithEvents SubCatIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents SubCatIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colSubCatID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents MedicineIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents MedicineIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colMedicineID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents MedicineSubCatTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents MedicineSubCatLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colMedicineSubCat As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents MedicineGroupsCombo As DentistX.MedicineGroupsCombo

End Class
