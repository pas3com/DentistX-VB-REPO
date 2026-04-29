<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLabPay
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLabPay))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLabPayID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLabName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLabID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOrderDetails = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLabOrderID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayDetail = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.LabOrderCombo1 = New DentistX.LabOrderCombo()
        Me.LabPayIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.LabPayIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.LabIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.LabCombo1 = New DentistX.LabCombo()
        Me.LabOrderIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.LabOrderIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.PayValueLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PayValueSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.PayDateLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PayDateDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.PayDetailLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PayDetailTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.NotesLabel = New DevExpress.XtraEditors.LabelControl()
        Me.NotesTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.TLPBut = New DevExpress.Utils.Layout.TablePanel()
        Me.butApply = New DevExpress.XtraEditors.SimpleButton()
        Me.butCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
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
        CType(Me.LabPayIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabOrderIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayValueSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDateDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDateDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDetailTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colLabPayID, Me.colLabName, Me.colLabID, Me.colOrderDetails, Me.colLabOrderID, Me.colPayValue, Me.colPayDate, Me.colPayDetail, Me.colNotes})
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
        'colLabPayID
        '
        Me.colLabPayID.FieldName = "LabPayID"
        Me.colLabPayID.Name = "colLabPayID"
        '
        'colLabName
        '
        resources.ApplyResources(Me.colLabName, "colLabName")
        Me.colLabName.FieldName = "LabName"
        Me.colLabName.Name = "colLabName"
        '
        'colLabID
        '
        resources.ApplyResources(Me.colLabID, "colLabID")
        Me.colLabID.FieldName = "LabID"
        Me.colLabID.Name = "colLabID"
        '
        'colOrderDetails
        '
        resources.ApplyResources(Me.colOrderDetails, "colOrderDetails")
        Me.colOrderDetails.FieldName = "OrderDetails"
        Me.colOrderDetails.Name = "colOrderDetails"
        '
        'colLabOrderID
        '
        Me.colLabOrderID.FieldName = "LabOrderID"
        Me.colLabOrderID.Name = "colLabOrderID"
        '
        'colPayValue
        '
        Me.colPayValue.FieldName = "PayValue"
        Me.colPayValue.Name = "colPayValue"
        resources.ApplyResources(Me.colPayValue, "colPayValue")
        '
        'colPayDate
        '
        Me.colPayDate.FieldName = "PayDate"
        Me.colPayDate.Name = "colPayDate"
        resources.ApplyResources(Me.colPayDate, "colPayDate")
        '
        'colPayDetail
        '
        Me.colPayDetail.FieldName = "PayDetail"
        Me.colPayDetail.Name = "colPayDetail"
        resources.ApplyResources(Me.colPayDetail, "colPayDetail")
        '
        'colNotes
        '
        Me.colNotes.FieldName = "Notes"
        Me.colNotes.Name = "colNotes"
        resources.ApplyResources(Me.colNotes, "colNotes")
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
        Me.TableData.Controls.Add(Me.LabOrderCombo1)
        Me.TableData.Controls.Add(Me.LabPayIDLabel)
        Me.TableData.Controls.Add(Me.LabPayIDSpinEdit)
        Me.TableData.Controls.Add(Me.LabIDLabel)
        Me.TableData.Controls.Add(Me.LabCombo1)
        Me.TableData.Controls.Add(Me.LabOrderIDLabel)
        Me.TableData.Controls.Add(Me.LabOrderIDSpinEdit)
        Me.TableData.Controls.Add(Me.PayValueLabel)
        Me.TableData.Controls.Add(Me.PayValueSpinEdit)
        Me.TableData.Controls.Add(Me.PayDateLabel)
        Me.TableData.Controls.Add(Me.PayDateDateEdit)
        Me.TableData.Controls.Add(Me.PayDetailLabel)
        Me.TableData.Controls.Add(Me.PayDetailTextEdit)
        Me.TableData.Controls.Add(Me.NotesLabel)
        Me.TableData.Controls.Add(Me.NotesTextEdit)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Controls.Add(Me.LabelControl1)
        resources.ApplyResources(Me.TableData, "TableData")
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TableData.UseSkinIndents = True
        '
        'LabOrderCombo1
        '
        Me.TableData.SetColumn(Me.LabOrderCombo1, 2)
        Me.TableData.SetColumnSpan(Me.LabOrderCombo1, 4)
        resources.ApplyResources(Me.LabOrderCombo1, "LabOrderCombo1")
        Me.LabOrderCombo1.LabOrderID = 0
        Me.LabOrderCombo1.Name = "LabOrderCombo1"
        Me.LabOrderCombo1.OrderDetails = ""
        Me.TableData.SetRow(Me.LabOrderCombo1, 1)
        '
        'LabPayIDLabel
        '
        Me.LabPayIDLabel.Appearance.Font = CType(resources.GetObject("LabPayIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.LabPayIDLabel.Appearance.Options.UseFont = True
        Me.LabPayIDLabel.Appearance.Options.UseTextOptions = True
        Me.LabPayIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabPayIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabPayIDLabel, "LabPayIDLabel")
        Me.TableData.SetColumn(Me.LabPayIDLabel, 1)
        Me.LabPayIDLabel.Name = "LabPayIDLabel"
        Me.TableData.SetRow(Me.LabPayIDLabel, 0)
        '
        'LabPayIDSpinEdit
        '
        Me.TableData.SetColumn(Me.LabPayIDSpinEdit, 2)
        resources.ApplyResources(Me.LabPayIDSpinEdit, "LabPayIDSpinEdit")
        Me.LabPayIDSpinEdit.Name = "LabPayIDSpinEdit"
        Me.LabPayIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("LabPayIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.LabPayIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.LabPayIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.LabPayIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabPayIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.LabPayIDSpinEdit, 0)
        '
        'LabIDLabel
        '
        Me.LabIDLabel.Appearance.Font = CType(resources.GetObject("LabIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.LabIDLabel.Appearance.Options.UseFont = True
        Me.LabIDLabel.Appearance.Options.UseTextOptions = True
        Me.LabIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabIDLabel, "LabIDLabel")
        Me.TableData.SetColumn(Me.LabIDLabel, 4)
        Me.LabIDLabel.Name = "LabIDLabel"
        Me.TableData.SetRow(Me.LabIDLabel, 0)
        '
        'LabCombo1
        '
        Me.LabCombo1.Appearance.Font = CType(resources.GetObject("colLabCombo.Appearance.Font"), System.Drawing.Font)
        Me.LabCombo1.Appearance.Options.UseFont = True
        Me.TableData.SetColumn(Me.LabCombo1, 5)
        resources.ApplyResources(Me.LabCombo1, "LabCombo1")
        Me.LabCombo1.LabID = 0
        Me.LabCombo1.LabName = Nothing
        Me.LabCombo1.Name = "LabCombo1"
        Me.TableData.SetRow(Me.LabCombo1, 0)
        '
        'LabOrderIDLabel
        '
        Me.LabOrderIDLabel.Appearance.Font = CType(resources.GetObject("LabOrderIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.LabOrderIDLabel.Appearance.Options.UseFont = True
        Me.LabOrderIDLabel.Appearance.Options.UseTextOptions = True
        Me.LabOrderIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabOrderIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabOrderIDLabel, "LabOrderIDLabel")
        Me.TableData.SetColumn(Me.LabOrderIDLabel, 1)
        Me.LabOrderIDLabel.Name = "LabOrderIDLabel"
        Me.TableData.SetRow(Me.LabOrderIDLabel, 2)
        '
        'LabOrderIDSpinEdit
        '
        Me.TableData.SetColumn(Me.LabOrderIDSpinEdit, 2)
        resources.ApplyResources(Me.LabOrderIDSpinEdit, "LabOrderIDSpinEdit")
        Me.LabOrderIDSpinEdit.Name = "LabOrderIDSpinEdit"
        Me.LabOrderIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("LabOrderIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.LabOrderIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.LabOrderIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.LabOrderIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabOrderIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.LabOrderIDSpinEdit, 2)
        '
        'PayValueLabel
        '
        Me.PayValueLabel.Appearance.Font = CType(resources.GetObject("PayValueLabel.Appearance.Font"), System.Drawing.Font)
        Me.PayValueLabel.Appearance.Options.UseFont = True
        Me.PayValueLabel.Appearance.Options.UseTextOptions = True
        Me.PayValueLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayValueLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PayValueLabel, "PayValueLabel")
        Me.TableData.SetColumn(Me.PayValueLabel, 4)
        Me.PayValueLabel.Name = "PayValueLabel"
        Me.TableData.SetRow(Me.PayValueLabel, 2)
        '
        'PayValueSpinEdit
        '
        Me.TableData.SetColumn(Me.PayValueSpinEdit, 5)
        resources.ApplyResources(Me.PayValueSpinEdit, "PayValueSpinEdit")
        Me.PayValueSpinEdit.Name = "PayValueSpinEdit"
        Me.PayValueSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("PayValueSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PayValueSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.PayValueSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PayValueSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayValueSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.PayValueSpinEdit, 2)
        '
        'PayDateLabel
        '
        Me.PayDateLabel.Appearance.Font = CType(resources.GetObject("PayDateLabel.Appearance.Font"), System.Drawing.Font)
        Me.PayDateLabel.Appearance.Options.UseFont = True
        Me.PayDateLabel.Appearance.Options.UseTextOptions = True
        Me.PayDateLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayDateLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PayDateLabel, "PayDateLabel")
        Me.TableData.SetColumn(Me.PayDateLabel, 1)
        Me.PayDateLabel.Name = "PayDateLabel"
        Me.TableData.SetRow(Me.PayDateLabel, 3)
        '
        'PayDateDateEdit
        '
        Me.TableData.SetColumn(Me.PayDateDateEdit, 2)
        resources.ApplyResources(Me.PayDateDateEdit, "PayDateDateEdit")
        Me.PayDateDateEdit.Name = "PayDateDateEdit"
        Me.PayDateDateEdit.Properties.Appearance.Font = CType(resources.GetObject("PayDateDateEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PayDateDateEdit.Properties.Appearance.Options.UseFont = True
        Me.PayDateDateEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PayDateDateEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayDateDateEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.PayDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("PayDateDateEdit.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.PayDateDateEdit, 3)
        '
        'PayDetailLabel
        '
        Me.PayDetailLabel.Appearance.Font = CType(resources.GetObject("PayDetailLabel.Appearance.Font"), System.Drawing.Font)
        Me.PayDetailLabel.Appearance.Options.UseFont = True
        Me.PayDetailLabel.Appearance.Options.UseTextOptions = True
        Me.PayDetailLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayDetailLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PayDetailLabel, "PayDetailLabel")
        Me.TableData.SetColumn(Me.PayDetailLabel, 4)
        Me.PayDetailLabel.Name = "PayDetailLabel"
        Me.TableData.SetRow(Me.PayDetailLabel, 3)
        '
        'PayDetailTextEdit
        '
        Me.TableData.SetColumn(Me.PayDetailTextEdit, 5)
        resources.ApplyResources(Me.PayDetailTextEdit, "PayDetailTextEdit")
        Me.PayDetailTextEdit.Name = "PayDetailTextEdit"
        Me.PayDetailTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PayDetailTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PayDetailTextEdit.Properties.Appearance.Options.UseFont = True
        Me.PayDetailTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PayDetailTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PayDetailTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.PayDetailTextEdit, 3)
        '
        'NotesLabel
        '
        Me.NotesLabel.Appearance.Font = CType(resources.GetObject("NotesLabel.Appearance.Font"), System.Drawing.Font)
        Me.NotesLabel.Appearance.Options.UseFont = True
        Me.NotesLabel.Appearance.Options.UseTextOptions = True
        Me.NotesLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NotesLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.NotesLabel, "NotesLabel")
        Me.TableData.SetColumn(Me.NotesLabel, 1)
        Me.NotesLabel.Name = "NotesLabel"
        Me.TableData.SetRow(Me.NotesLabel, 4)
        '
        'NotesTextEdit
        '
        Me.TableData.SetColumn(Me.NotesTextEdit, 2)
        Me.TableData.SetColumnSpan(Me.NotesTextEdit, 4)
        resources.ApplyResources(Me.NotesTextEdit, "NotesTextEdit")
        Me.NotesTextEdit.Name = "NotesTextEdit"
        Me.NotesTextEdit.Properties.Appearance.Font = CType(resources.GetObject("NotesTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.NotesTextEdit.Properties.Appearance.Options.UseFont = True
        Me.NotesTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.NotesTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.NotesTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.NotesTextEdit, 4)
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
        Me.TableData.SetRow(Me.TLPBut, 5)
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
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.TableData.SetColumn(Me.LabelControl1, 1)
        Me.LabelControl1.Name = "LabelControl1"
        Me.TableData.SetRow(Me.LabelControl1, 1)
        '
        'FrmLabPay
        '
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ribbonStatusBar)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "FrmLabPay"
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
        CType(Me.LabPayIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabOrderIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayValueSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDateDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDateDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDetailTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
		Friend WithEvents LabPayIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents LabPayIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colLabPayID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents LabIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents LabIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colLabID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents LabOrderIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents LabOrderIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colLabOrderID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PayValueSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents PayValueLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPayValue As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PayDateDateEdit As DevExpress.XtraEditors.DateEdit
		Friend WithEvents PayDateLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPayDate As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PayDetailTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents PayDetailLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPayDetail As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents NotesTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents NotesLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents LabCombo1 As DentistX.LabCombo
    Friend WithEvents colLabName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabOrderCombo1 As LabOrderCombo
    Friend WithEvents colOrderDetails As DevExpress.XtraGrid.Columns.GridColumn
End Class
