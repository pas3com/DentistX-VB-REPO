<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLabMsgDetailChoice
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
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLabMsgDetailChoice))
		Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
		Me.DGV = New DevExpress.XtraGrid.GridControl()
		Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
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
		Me.TLPBut = New DevExpress.Utils.Layout.TablePanel()
		Me.butApply = New DevExpress.XtraEditors.SimpleButton()
		Me.butCancel = New DevExpress.XtraEditors.SimpleButton()
		Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.TableData.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {
        New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.0!),
        New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!),
        New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!),
        New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10.0!),
        New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!),
        New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!),
        New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.0!)})
        ' Add a new row for LabMsgDetailChoiceID
        Me.TableData.Rows.Add(New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!))
        ' GridColumn for LabMsgDetailChoiceID
        Me.colLabMsgDetailChoiceID = New DevExpress.XtraGrid.Columns.GridColumn()
        ' Label for LabMsgDetailChoiceID
        Me.LabMsgDetailChoiceIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.LabMsgDetailChoiceIDLabel.Name = "LabMsgDetailChoiceIDLabel"
        Me.LabMsgDetailChoiceIDLabel.Size = New System.Drawing.Size(100, 23)
        Me.LabMsgDetailChoiceIDLabel.TabIndex = 0
        Me.LabMsgDetailChoiceIDLabel.Text = "LabMsgDetailChoiceID"
        Me.TableData.SetRow(Me.LabMsgDetailChoiceIDLabel, 0)
        Me.TableData.SetColumn(Me.LabMsgDetailChoiceIDLabel, 1)
        Me.TableData.Controls.Add(Me.LabMsgDetailChoiceIDLabel)
        ' Control for LabMsgDetailChoiceID
        Me.LabMsgDetailChoiceIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.LabMsgDetailChoiceIDSpinEdit.Name = "LabMsgDetailChoiceIDSpinEdit"
        Me.LabMsgDetailChoiceIDSpinEdit.Size = New System.Drawing.Size(200, 23)
        Me.LabMsgDetailChoiceIDSpinEdit.TabIndex = 1
        Me.TableData.SetRow(Me.LabMsgDetailChoiceIDSpinEdit, 0)
        Me.TableData.SetColumn(Me.LabMsgDetailChoiceIDSpinEdit, 2)
        Me.TableData.Controls.Add(Me.LabMsgDetailChoiceIDSpinEdit)
        Me.LabMsgDetailChoiceIDLabel.Appearance.Options.UseTextOptions = True
        Me.LabMsgDetailChoiceIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabMsgDetailChoiceIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabMsgDetailChoiceIDLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabMsgDetailChoiceIDLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabMsgDetailChoiceIDLabel.Appearance.Options.UseFont = True
        Me.LabMsgDetailChoiceIDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabMsgDetailChoiceIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.LabMsgDetailChoiceIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabMsgDetailChoiceIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabMsgDetailChoiceIDSpinEdit.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabMsgDetailChoiceIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.LabMsgDetailChoiceIDSpinEdit.Dock = System.Windows.Forms.DockStyle.Fill
        ' GridColumn for LabMsgSubjectID
        Me.colLabMsgSubjectID = New DevExpress.XtraGrid.Columns.GridColumn()
        ' Label for LabMsgSubjectID
        Me.LabMsgSubjectIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.LabMsgSubjectIDLabel.Name = "LabMsgSubjectIDLabel"
        Me.LabMsgSubjectIDLabel.Size = New System.Drawing.Size(100, 23)
        Me.LabMsgSubjectIDLabel.TabIndex = 0
        Me.LabMsgSubjectIDLabel.Text = "LabMsgSubjectID"
        Me.TableData.SetRow(Me.LabMsgSubjectIDLabel, 0)
        Me.TableData.SetColumn(Me.LabMsgSubjectIDLabel, 4)
        Me.TableData.Controls.Add(Me.LabMsgSubjectIDLabel)
        ' Combo for foreign key LabMsgSubjectID
        Me.cboLabMsgSubjectCombo = New DentistX.LabMsgSubjectCombo()
        Me.cboLabMsgSubjectCombo.Name = "cboLabMsgSubjectCombo"
        Me.cboLabMsgSubjectCombo.Size = New System.Drawing.Size(200, 23)
        Me.cboLabMsgSubjectCombo.TabIndex = 1
        Me.TableData.SetRow(Me.cboLabMsgSubjectCombo, 0)
        Me.TableData.SetColumn(Me.cboLabMsgSubjectCombo, 5)
        Me.TableData.Controls.Add(Me.cboLabMsgSubjectCombo)
        Me.LabMsgSubjectIDLabel.Appearance.Options.UseTextOptions = True
        Me.LabMsgSubjectIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabMsgSubjectIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabMsgSubjectIDLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabMsgSubjectIDLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabMsgSubjectIDLabel.Appearance.Options.UseFont = True
        Me.LabMsgSubjectIDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboLabMsgSubjectCombo.CboLabMsgSubject.Properties.Appearance.Options.UseTextOptions = True
        Me.cboLabMsgSubjectCombo.CboLabMsgSubject.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cboLabMsgSubjectCombo.CboLabMsgSubject.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.cboLabMsgSubjectCombo.CboLabMsgSubject.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cboLabMsgSubjectCombo.CboLabMsgSubject.Properties.Appearance.Options.UseFont = True
        Me.cboLabMsgSubjectCombo.CboLabMsgSubject.Dock = System.Windows.Forms.DockStyle.Fill
        ' Add a new row for DetailText
        Me.TableData.Rows.Add(New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!))
        ' GridColumn for DetailText
        Me.colDetailText = New DevExpress.XtraGrid.Columns.GridColumn()
        ' Label for DetailText
        Me.DetailTextLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DetailTextLabel.Name = "DetailTextLabel"
        Me.DetailTextLabel.Size = New System.Drawing.Size(100, 23)
        Me.DetailTextLabel.TabIndex = 2
        Me.DetailTextLabel.Text = "DetailText"
        Me.TableData.SetRow(Me.DetailTextLabel, 1)
        Me.TableData.SetColumn(Me.DetailTextLabel, 1)
        Me.TableData.Controls.Add(Me.DetailTextLabel)
        ' Control for DetailText
        Me.DetailTextTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DetailTextTextEdit.Name = "DetailTextTextEdit"
        Me.DetailTextTextEdit.Size = New System.Drawing.Size(200, 23)
        Me.DetailTextTextEdit.TabIndex = 3
        Me.TableData.SetRow(Me.DetailTextTextEdit, 1)
        Me.TableData.SetColumn(Me.DetailTextTextEdit, 2)
        Me.TableData.Controls.Add(Me.DetailTextTextEdit)
        Me.DetailTextLabel.Appearance.Options.UseTextOptions = True
        Me.DetailTextLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DetailTextLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DetailTextLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.DetailTextLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.DetailTextLabel.Appearance.Options.UseFont = True
        Me.DetailTextLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DetailTextTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DetailTextTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DetailTextTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DetailTextTextEdit.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.DetailTextTextEdit.Properties.Appearance.Options.UseFont = True
        Me.DetailTextTextEdit.Dock = System.Windows.Forms.DockStyle.Fill
        ' GridColumn for SortOrder
        Me.colSortOrder = New DevExpress.XtraGrid.Columns.GridColumn()
        ' Label for SortOrder
        Me.SortOrderLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SortOrderLabel.Name = "SortOrderLabel"
        Me.SortOrderLabel.Size = New System.Drawing.Size(100, 23)
        Me.SortOrderLabel.TabIndex = 2
        Me.SortOrderLabel.Text = "SortOrder"
        Me.TableData.SetRow(Me.SortOrderLabel, 1)
        Me.TableData.SetColumn(Me.SortOrderLabel, 4)
        Me.TableData.Controls.Add(Me.SortOrderLabel)
        ' Control for SortOrder
        Me.SortOrderSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.SortOrderSpinEdit.Name = "SortOrderSpinEdit"
        Me.SortOrderSpinEdit.Size = New System.Drawing.Size(200, 23)
        Me.SortOrderSpinEdit.TabIndex = 3
        Me.TableData.SetRow(Me.SortOrderSpinEdit, 1)
        Me.TableData.SetColumn(Me.SortOrderSpinEdit, 5)
        Me.TableData.Controls.Add(Me.SortOrderSpinEdit)
        Me.SortOrderLabel.Appearance.Options.UseTextOptions = True
        Me.SortOrderLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SortOrderLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SortOrderLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SortOrderLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SortOrderLabel.Appearance.Options.UseFont = True
        Me.SortOrderLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SortOrderSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SortOrderSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SortOrderSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SortOrderSpinEdit.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SortOrderSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.SortOrderSpinEdit.Dock = System.Windows.Forms.DockStyle.Fill
        ' Add a new row for IsActive
        Me.TableData.Rows.Add(New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!))
        ' GridColumn for IsActive
        Me.colIsActive = New DevExpress.XtraGrid.Columns.GridColumn()
        ' Label for IsActive
        Me.IsActiveLabel = New DevExpress.XtraEditors.LabelControl()
        Me.IsActiveLabel.Name = "IsActiveLabel"
        Me.IsActiveLabel.Size = New System.Drawing.Size(100, 23)
        Me.IsActiveLabel.TabIndex = 4
        Me.IsActiveLabel.Text = "IsActive"
        Me.TableData.SetRow(Me.IsActiveLabel, 2)
        Me.TableData.SetColumn(Me.IsActiveLabel, 1)
        Me.TableData.Controls.Add(Me.IsActiveLabel)
        ' Control for IsActive
        Me.IsActiveCheckEdit = New DevExpress.XtraEditors.CheckEdit()
        Me.IsActiveCheckEdit.Name = "IsActiveCheckEdit"
        Me.IsActiveCheckEdit.Size = New System.Drawing.Size(200, 23)
        Me.IsActiveCheckEdit.TabIndex = 5
        Me.TableData.SetRow(Me.IsActiveCheckEdit, 2)
        Me.TableData.SetColumn(Me.IsActiveCheckEdit, 2)
        Me.TableData.Controls.Add(Me.IsActiveCheckEdit)
        Me.IsActiveLabel.Appearance.Options.UseTextOptions = True
        Me.IsActiveLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.IsActiveLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.IsActiveLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IsActiveLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IsActiveLabel.Appearance.Options.UseFont = True
        Me.IsActiveLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IsActiveCheckEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.IsActiveCheckEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.IsActiveCheckEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.IsActiveCheckEdit.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IsActiveCheckEdit.Properties.Appearance.Options.UseFont = True
        Me.IsActiveCheckEdit.Dock = System.Windows.Forms.DockStyle.Fill
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
		CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TLPBut.SuspendLayout()
		Me.SuspendLayout()
		'
		'Splitter1
		'
		Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Splitter1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
		Resources.ApplyResources(Me.Splitter1, "Splitter1")
		Me.Splitter1.Horizontal = False
		Me.Splitter1.Name = "Splitter1"
		'
		'Splitter1.Panel1
		'
		Me.Splitter1.Panel1.Controls.Add(Me.DGV)
		Resources.ApplyResources(Me.Splitter1.Panel1, "Splitter1.Panel1")
		'
		'Splitter1.Panel2
		'
		Me.Splitter1.Panel2.Controls.Add(Me.TableData)
		Resources.ApplyResources(Me.Splitter1.Panel2, "Splitter1.Panel2")
		Me.Splitter1.SplitterPosition = 293
		'
		'DGV
		'
		Me.DGV.Dock = System.Windows.Forms.DockStyle.Fill
		 Resources.ApplyResources(Me.DGV, "DGV")
		 Me.DGV.EmbeddedNavigator.Appearance.Font = CType(Resources.GetObject("DGV.EmbeddedNavigator.Appearance.Font"), System.Drawing.Font)
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
		Me.dgView.Appearance.HeaderPanel.Font =  New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
		Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
		Me.dgView.Appearance.Row.Font =  New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
		Me.dgView.Appearance.Row.Options.UseFont = True
		Me.dgView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum})
		Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colLabMsgDetailChoiceID})
		Me.colLabMsgDetailChoiceID.FieldName = "LabMsgDetailChoiceID"
		Me.colLabMsgDetailChoiceID.Name = "colLabMsgDetailChoiceID"
		Me.colLabMsgDetailChoiceID.Caption ="LabMsgDetailChoiceID"
		Me.colLabMsgDetailChoiceID.Visible = True
		Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colLabMsgSubjectID})
		Me.colLabMsgSubjectID.FieldName = "LabMsgSubjectID"
		Me.colLabMsgSubjectID.Name = "colLabMsgSubjectID"
		Me.colLabMsgSubjectID.Caption ="LabMsgSubjectID"
		Me.colLabMsgSubjectID.Visible = True
		Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colDetailText})
		Me.colDetailText.FieldName = "DetailText"
		Me.colDetailText.Name = "colDetailText"
		Me.colDetailText.Caption ="DetailText"
		Me.colDetailText.Visible = True
		Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSortOrder})
		Me.colSortOrder.FieldName = "SortOrder"
		Me.colSortOrder.Name = "colSortOrder"
		Me.colSortOrder.Caption ="SortOrder"
		Me.colSortOrder.Visible = True
		Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIsActive})
		Me.colIsActive.FieldName = "IsActive"
		Me.colIsActive.Name = "colIsActive"
		Me.colIsActive.Caption ="IsActive"
		Me.colIsActive.Visible = True
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
		Resources.ApplyResources(Me.colRowNum, "colRowNum")
		Me.colRowNum.Visible = True
		Me.colRowNum.FieldName = "colRowNum"
		Me.colRowNum.Caption = "Number"
		Me.colRowNum.Name = "colRowNum"
		Me.colRowNum.UnboundDataType = GetType(Integer)
		'
		'ribbonControl
		'
		Me.ribbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(30, 32, 30, 32)
		Me.ribbonControl.ExpandCollapseItem.Id = 0
		Me.ribbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.ribbonControl.ExpandCollapseItem, Me.bbiPrintPreview, Me.bsiRecordsCount, Me.bAdd, Me.bEdit, Me.bDelete, Me.bRefresh, Me.bCancel})
		Resources.ApplyResources(Me.ribbonControl, "ribbonControl")
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
		Resources.ApplyResources(Me.bbiPrintPreview, "bbiPrintPreview")
		Me.bbiPrintPreview.Id = 14
		Me.bbiPrintPreview.ImageOptions.ImageUri.Uri = "Preview"
		Me.bbiPrintPreview.ItemAppearance.Normal.Font =  New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
		Me.bbiPrintPreview.ItemAppearance.Normal.Options.UseFont = True
		Me.bbiPrintPreview.Name = "bbiPrintPreview"
		Me.bbiPrintPreview.Caption = "Print Preview"
		'
		'bsiRecordsCount
		'
		Resources.ApplyResources(Me.bsiRecordsCount, "bsiRecordsCount")
		Me.bsiRecordsCount.Id = 15
		Me.bsiRecordsCount.Name = "bsiRecordsCount"
		'
		'bAdd
		'
		Resources.ApplyResources(Me.bAdd, "bAdd")
		Me.bAdd.Id = 16
		Me.bAdd.ImageOptions.ImageUri.Uri = "New"
		Me.bAdd.ItemAppearance.Normal.Options.UseFont = True
		Me.bAdd.ItemAppearance.Normal.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
		Me.bAdd.Name = "bAdd"
		Me.bAdd.Caption = "Add"
		'
		'bEdit
		'
		Resources.ApplyResources(Me.bEdit, "bEdit")
		Me.bEdit.Id = 17
		Me.bEdit.ImageOptions.ImageUri.Uri = "Edit"
		Me.bEdit.ItemAppearance.Normal.Font =  New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
		Me.bEdit.ItemAppearance.Normal.Options.UseFont = True
		Me.bEdit.Name = "bEdit"
		Me.bEdit.Caption = "Edit"
		'
		'bDelete
		'
		Resources.ApplyResources(Me.bDelete, "bDelete")
		Me.bDelete.Id = 18
		Me.bDelete.ImageOptions.ImageUri.Uri = "Delete"
		Me.bDelete.ItemAppearance.Normal.Font =  New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
		Me.bDelete.ItemAppearance.Normal.Options.UseFont = True
		Me.bDelete.Name = "bDelete"
		Me.bDelete.Caption = "Delete"
		'
		'bRefresh
		'
		Resources.ApplyResources(Me.bRefresh, "bRefresh")
		Me.bRefresh.Id = 19
		Me.bRefresh.ImageOptions.ImageUri.Uri = "Refresh"
		Me.bRefresh.ItemAppearance.Normal.Font =  New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
		Me.bRefresh.ItemAppearance.Normal.Options.UseFont = True
		Me.bRefresh.Name = "bRefresh"
		Me.bRefresh.Caption = "Refresh"
		'
		'bCancel
		'
		Resources.ApplyResources(Me.bCancel, "bCancel")
		Me.bCancel.Id = 20
		Me.bCancel.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.undo
		Me.bCancel.ItemAppearance.Normal.Font =  New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
		Me.bCancel.ItemAppearance.Normal.Options.UseFont = True
		Me.bCancel.Name = "bCancel"
		Me.bCancel.Caption = "Cancel"
		'
		'ribbonPage1
		'
		Me.ribbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.ribbonPageGroup1, Me.ribbonPageGroup2})
		Me.ribbonPage1.MergeOrder = 0
		Me.ribbonPage1.Name = "ribbonPage1"
		Resources.ApplyResources(Me.ribbonPage1, "ribbonPage1")
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
		Resources.ApplyResources(Me.ribbonPageGroup1, "ribbonPageGroup1")
		'
		'ribbonPageGroup2
		'
		Me.ribbonPageGroup2.AllowTextClipping = False
		Me.ribbonPageGroup2.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
		Me.ribbonPageGroup2.ItemLinks.Add(Me.bbiPrintPreview)
		Me.ribbonPageGroup2.Name = "ribbonPageGroup2"
		Resources.ApplyResources(Me.ribbonPageGroup2, "ribbonPageGroup2")
		'
		'ribbonStatusBar
		'
		Me.ribbonStatusBar.ItemLinks.Add(Me.bsiRecordsCount)
		Resources.ApplyResources(Me.ribbonStatusBar, "ribbonStatusBar")
		Me.ribbonStatusBar.Name = "ribbonStatusBar"
		Me.ribbonStatusBar.Ribbon = Me.ribbonControl
		'
		'TableData
		'
		Me.TableData.Dock = System.Windows.Forms.DockStyle.Fill
		Resources.ApplyResources(Me.TableData, "TableData")
		Me.TableData.Name = "TableData"
		Me.TableData.UseSkinIndents = True
		'
		'TLPBut
		'
Me.TableData.Rows.Add(New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!))
		Me.TLPBut.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!)})
		Me.TLPBut.Controls.Add(Me.butApply)
		Me.TLPBut.Controls.Add(Me.butCancel)
		Me.TLPBut.Controls.Add(Me.butClose)
		Resources.ApplyResources(Me.TLPBut, "TLPBut")
		Me.TLPBut.Name = "TLPBut"
		Me.TLPBut.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
		Me.TLPBut.UseSkinIndents = True
		Me.TableData.Controls.Add(Me.TLPBut)
Me.TableData.SetRow(Me.TLPBut, 3)
Me.TableData.SetColumn(Me.TLPBut,0)
Me.TableData.SetColumnSpan(Me.TLPBut,7)
		Me.TLPBut.Dock = System.Windows.Forms.DockStyle.Fill
		'
		'butApply
		'
		Me.butApply.Appearance.Font =  New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
		Me.butApply.Appearance.Options.UseFont = True
		Me.TLPBut.SetColumn(Me.butApply, 1)
		Resources.ApplyResources(Me.butApply, "butApply")
		Me.butApply.Name = "butApply"
		Me.TLPBut.SetRow(Me.butApply, 0)
		Me.butApply.Text = "Apply"
		'
		'butCancel
		'
		Me.butCancel.Appearance.Font =  New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
		Me.butCancel.Appearance.Options.UseFont = True
		Me.TLPBut.SetColumn(Me.butCancel, 3)
		Resources.ApplyResources(Me.butCancel, "butCancel")
		Me.butCancel.Name = "butCancel"
		Me.TLPBut.SetRow(Me.butCancel, 0)
		Me.butCancel.Text = "Cancel"
		'
		'butClose
		'
		Me.butClose.Appearance.Font =  New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
		Me.butClose.Appearance.Options.UseFont = True
		Me.TLPBut.SetColumn(Me.butClose, 5)
		Resources.ApplyResources(Me.butClose, "butClose")
		Me.butClose.Name = "butClose"
		Me.TLPBut.SetRow(Me.butClose, 0)
		Me.butClose.Text = "Close"
		'
		'
		'LabMsgDetailChoice
		'
		Me.Size = New System.Drawing.Size(1000, 768)
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
		Me.Appearance.Options.UseFont = True
		Resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Splitter1)
		Me.Controls.Add(Me.ribbonStatusBar)
		Me.Controls.Add(Me.ribbonControl)
		Me.Name = "FrmLabMsgDetailChoice"
		Me.Text = "LabMsgDetailChoice Form"
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
	Friend WithEvents LabMsgDetailChoiceIDSpinEdit As DevExpress.XtraEditors.SpinEdit
	Friend WithEvents LabMsgDetailChoiceIDLabel As DevExpress.XtraEditors.LabelControl
	Friend WithEvents colLabMsgDetailChoiceID As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents LabMsgSubjectIDLabel As DevExpress.XtraEditors.LabelControl
	Friend WithEvents colLabMsgSubjectID As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents DetailTextTextEdit As DevExpress.XtraEditors.TextEdit
	Friend WithEvents DetailTextLabel As DevExpress.XtraEditors.LabelControl
	Friend WithEvents colDetailText As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents SortOrderSpinEdit As DevExpress.XtraEditors.SpinEdit
	Friend WithEvents SortOrderLabel As DevExpress.XtraEditors.LabelControl
	Friend WithEvents colSortOrder As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents IsActiveCheckEdit As DevExpress.XtraEditors.CheckEdit
	Friend WithEvents IsActiveLabel As DevExpress.XtraEditors.LabelControl
	Friend WithEvents colIsActive As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents cboLabMsgSubjectCombo As DentistX.LabMsgSubjectCombo
		 
End Class
