<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMedicineItems
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
        Me.colMedicineItemID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colScincID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCommName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCompany = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.MedicineItemIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.MedicineItemIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.ScincIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.MedScienceFamilyCombo = New DentistX.MedScienceFamilyCombo()
        Me.CommNameLabel = New DevExpress.XtraEditors.LabelControl()
        Me.CommNameTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.CompanyLabel = New DevExpress.XtraEditors.LabelControl()
        Me.CompanyTextEdit = New DevExpress.XtraEditors.TextEdit()
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
        CType(Me.MedicineItemIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CommNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CompanyTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colMedicineItemID, Me.colScincID, Me.colCommName, Me.colCompany, Me.colNotes})
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
        'colMedicineItemID
        '
        Me.colMedicineItemID.FieldName = "MedicineItemID"
        Me.colMedicineItemID.Name = "colMedicineItemID"
        Me.colMedicineItemID.Visible = True
        Me.colMedicineItemID.VisibleIndex = 1
        '
        'colScincID
        '
        Me.colScincID.FieldName = "ScincID"
        Me.colScincID.Name = "colScincID"
        Me.colScincID.Visible = True
        Me.colScincID.VisibleIndex = 2
        '
        'colCommName
        '
        Me.colCommName.FieldName = "CommName"
        Me.colCommName.Name = "colCommName"
        Me.colCommName.Visible = True
        Me.colCommName.VisibleIndex = 3
        '
        'colCompany
        '
        Me.colCompany.FieldName = "Company"
        Me.colCompany.Name = "colCompany"
        Me.colCompany.Visible = True
        Me.colCompany.VisibleIndex = 4
        '
        'colNotes
        '
        Me.colNotes.FieldName = "Notes"
        Me.colNotes.Name = "colNotes"
        Me.colNotes.Visible = True
        Me.colNotes.VisibleIndex = 5
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
        Me.TableData.Controls.Add(Me.MedicineItemIDLabel)
        Me.TableData.Controls.Add(Me.MedicineItemIDSpinEdit)
        Me.TableData.Controls.Add(Me.ScincIDLabel)
        Me.TableData.Controls.Add(Me.MedScienceFamilyCombo)
        Me.TableData.Controls.Add(Me.CommNameLabel)
        Me.TableData.Controls.Add(Me.CommNameTextEdit)
        Me.TableData.Controls.Add(Me.CompanyLabel)
        Me.TableData.Controls.Add(Me.CompanyTextEdit)
        Me.TableData.Controls.Add(Me.NotesLabel)
        Me.TableData.Controls.Add(Me.NotesTextEdit)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableData.Location = New System.Drawing.Point(0, 0)
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TableData.Size = New System.Drawing.Size(992, 287)
        Me.TableData.TabIndex = 0
        Me.TableData.UseSkinIndents = True
        '
        'MedicineItemIDLabel
        '
        Me.MedicineItemIDLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MedicineItemIDLabel.Appearance.Options.UseFont = True
        Me.MedicineItemIDLabel.Appearance.Options.UseTextOptions = True
        Me.MedicineItemIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MedicineItemIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.MedicineItemIDLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.MedicineItemIDLabel, 1)
        Me.MedicineItemIDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedicineItemIDLabel.Location = New System.Drawing.Point(207, 12)
        Me.MedicineItemIDLabel.Name = "MedicineItemIDLabel"
        Me.TableData.SetRow(Me.MedicineItemIDLabel, 0)
        Me.MedicineItemIDLabel.Size = New System.Drawing.Size(135, 22)
        Me.MedicineItemIDLabel.TabIndex = 0
        Me.MedicineItemIDLabel.Text = "MedicineItemID"
        '
        'MedicineItemIDSpinEdit
        '
        Me.TableData.SetColumn(Me.MedicineItemIDSpinEdit, 2)
        Me.MedicineItemIDSpinEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedicineItemIDSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.MedicineItemIDSpinEdit.Enabled = False
        Me.MedicineItemIDSpinEdit.Location = New System.Drawing.Point(346, 12)
        Me.MedicineItemIDSpinEdit.Name = "MedicineItemIDSpinEdit"
        Me.MedicineItemIDSpinEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MedicineItemIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.MedicineItemIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.MedicineItemIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MedicineItemIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.MedicineItemIDSpinEdit, 0)
        Me.MedicineItemIDSpinEdit.Size = New System.Drawing.Size(135, 22)
        Me.MedicineItemIDSpinEdit.TabIndex = 1
        '
        'ScincIDLabel
        '
        Me.ScincIDLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ScincIDLabel.Appearance.Options.UseFont = True
        Me.ScincIDLabel.Appearance.Options.UseTextOptions = True
        Me.ScincIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ScincIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ScincIDLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.ScincIDLabel, 4)
        Me.ScincIDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScincIDLabel.Location = New System.Drawing.Point(512, 12)
        Me.ScincIDLabel.Name = "ScincIDLabel"
        Me.TableData.SetRow(Me.ScincIDLabel, 0)
        Me.ScincIDLabel.Size = New System.Drawing.Size(135, 22)
        Me.ScincIDLabel.TabIndex = 0
        Me.ScincIDLabel.Text = "ScincID"
        '
        'MedScienceFamilyCombo
        '
        Me.MedScienceFamilyCombo.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MedScienceFamilyCombo.Appearance.Options.UseFont = True
        Me.TableData.SetColumn(Me.MedScienceFamilyCombo, 5)
        Me.MedScienceFamilyCombo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedScienceFamilyCombo.Location = New System.Drawing.Point(650, 12)
        Me.MedScienceFamilyCombo.Name = "MedScienceFamilyCombo"
        Me.TableData.SetRow(Me.MedScienceFamilyCombo, 0)
        Me.MedScienceFamilyCombo.ScienceName = Nothing
        Me.MedScienceFamilyCombo.ScincID = 0
        Me.MedScienceFamilyCombo.Size = New System.Drawing.Size(135, 22)
        Me.MedScienceFamilyCombo.TabIndex = 1
        '
        'CommNameLabel
        '
        Me.CommNameLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CommNameLabel.Appearance.Options.UseFont = True
        Me.CommNameLabel.Appearance.Options.UseTextOptions = True
        Me.CommNameLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CommNameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.CommNameLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.CommNameLabel, 1)
        Me.CommNameLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CommNameLabel.Location = New System.Drawing.Point(207, 38)
        Me.CommNameLabel.Name = "CommNameLabel"
        Me.TableData.SetRow(Me.CommNameLabel, 1)
        Me.CommNameLabel.Size = New System.Drawing.Size(135, 22)
        Me.CommNameLabel.TabIndex = 2
        Me.CommNameLabel.Text = "CommName"
        '
        'CommNameTextEdit
        '
        Me.TableData.SetColumn(Me.CommNameTextEdit, 2)
        Me.CommNameTextEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CommNameTextEdit.Location = New System.Drawing.Point(346, 38)
        Me.CommNameTextEdit.Name = "CommNameTextEdit"
        Me.CommNameTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CommNameTextEdit.Properties.Appearance.Options.UseFont = True
        Me.CommNameTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.CommNameTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CommNameTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.CommNameTextEdit, 1)
        Me.CommNameTextEdit.Size = New System.Drawing.Size(135, 22)
        Me.CommNameTextEdit.TabIndex = 3
        '
        'CompanyLabel
        '
        Me.CompanyLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CompanyLabel.Appearance.Options.UseFont = True
        Me.CompanyLabel.Appearance.Options.UseTextOptions = True
        Me.CompanyLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CompanyLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.CompanyLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.CompanyLabel, 4)
        Me.CompanyLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CompanyLabel.Location = New System.Drawing.Point(512, 38)
        Me.CompanyLabel.Name = "CompanyLabel"
        Me.TableData.SetRow(Me.CompanyLabel, 1)
        Me.CompanyLabel.Size = New System.Drawing.Size(135, 22)
        Me.CompanyLabel.TabIndex = 2
        Me.CompanyLabel.Text = "Company"
        '
        'CompanyTextEdit
        '
        Me.TableData.SetColumn(Me.CompanyTextEdit, 5)
        Me.CompanyTextEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CompanyTextEdit.Location = New System.Drawing.Point(650, 38)
        Me.CompanyTextEdit.Name = "CompanyTextEdit"
        Me.CompanyTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CompanyTextEdit.Properties.Appearance.Options.UseFont = True
        Me.CompanyTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.CompanyTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CompanyTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.CompanyTextEdit, 1)
        Me.CompanyTextEdit.Size = New System.Drawing.Size(135, 22)
        Me.CompanyTextEdit.TabIndex = 3
        '
        'NotesLabel
        '
        Me.NotesLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.NotesLabel.Appearance.Options.UseFont = True
        Me.NotesLabel.Appearance.Options.UseTextOptions = True
        Me.NotesLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NotesLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.NotesLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TableData.SetColumn(Me.NotesLabel, 1)
        Me.NotesLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NotesLabel.Location = New System.Drawing.Point(207, 64)
        Me.NotesLabel.Name = "NotesLabel"
        Me.TableData.SetRow(Me.NotesLabel, 2)
        Me.NotesLabel.Size = New System.Drawing.Size(135, 22)
        Me.NotesLabel.TabIndex = 4
        Me.NotesLabel.Text = "Notes"
        '
        'NotesTextEdit
        '
        Me.TableData.SetColumn(Me.NotesTextEdit, 2)
        Me.NotesTextEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NotesTextEdit.Location = New System.Drawing.Point(346, 64)
        Me.NotesTextEdit.Name = "NotesTextEdit"
        Me.NotesTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.NotesTextEdit.Properties.Appearance.Options.UseFont = True
        Me.NotesTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.NotesTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NotesTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.NotesTextEdit, 2)
        Me.NotesTextEdit.Size = New System.Drawing.Size(135, 22)
        Me.NotesTextEdit.TabIndex = 5
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
        Me.TLPBut.Location = New System.Drawing.Point(13, 90)
        Me.TLPBut.Name = "TLPBut"
        Me.TableData.SetRow(Me.TLPBut, 3)
        Me.TLPBut.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TLPBut.Size = New System.Drawing.Size(966, 184)
        Me.TLPBut.TabIndex = 6
        Me.TLPBut.UseSkinIndents = True
        '
        'butApply
        '
        Me.butApply.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.butApply.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.butApply, 1)
        Me.butApply.Location = New System.Drawing.Point(283, 80)
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
        Me.butCancel.Location = New System.Drawing.Point(445, 80)
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
        Me.butClose.Location = New System.Drawing.Point(606, 80)
        Me.butClose.Name = "butClose"
        Me.TLPBut.SetRow(Me.butClose, 0)
        Me.butClose.Size = New System.Drawing.Size(77, 23)
        Me.butClose.TabIndex = 2
        Me.butClose.Text = "Close"
        '
        'FrmMedicineItems
        '
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 764)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ribbonStatusBar)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "FrmMedicineItems"
        Me.Ribbon = Me.ribbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.StatusBar = Me.ribbonStatusBar
        Me.Text = "FrmMedicineItems"
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
        CType(Me.MedicineItemIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CommNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CompanyTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
		Friend WithEvents MedicineItemIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents MedicineItemIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colMedicineItemID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents ScincIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents ScincIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colScincID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents CommNameTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents CommNameLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colCommName As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents CompanyTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents CompanyLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colCompany As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents NotesTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents NotesLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents MedScienceFamilyCombo As DentistX.MedScienceFamilyCombo

End Class
