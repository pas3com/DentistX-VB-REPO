<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRecieveLabOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRecieveLabOrder))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLabOrderID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLabName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLabID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colImprType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colImprDet = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colImprClr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colImprCount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRecieveDate = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.RecieveDateDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.DeliveryDateDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.ImprDetCombo1 = New DentistX.ImprDetCombo()
        Me.ImpClrsCombo1 = New DentistX.ImpClrsCombo()
        Me.ImpressionCombo1 = New DentistX.ImpressionCombo()
        Me.LabOrderIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.LabOrderIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.LabIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.colLabCombo = New DentistX.LabCombo()
        Me.PatientIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.colPatientCombo = New DentistX.PatientCombo()
        Me.ImprTypeLabel = New DevExpress.XtraEditors.LabelControl()
        Me.ImprDetLabel = New DevExpress.XtraEditors.LabelControl()
        Me.ImprClrLabel = New DevExpress.XtraEditors.LabelControl()
        Me.ImprCountLabel = New DevExpress.XtraEditors.LabelControl()
        Me.ImprCountSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.DeliveryDateLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PriceLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PriceSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.RecieveDateLabel = New DevExpress.XtraEditors.LabelControl()
        Me.NotesLabel = New DevExpress.XtraEditors.LabelControl()
        Me.NotesTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.TLPBut = New DevExpress.Utils.Layout.TablePanel()
        Me.butApply = New DevExpress.XtraEditors.SimpleButton()
        Me.butCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.OrderDetailsTextEdit = New DevExpress.XtraEditors.TextEdit()
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
        CType(Me.RecieveDateDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RecieveDateDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DeliveryDateDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DeliveryDateDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabOrderIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImprCountSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PriceSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLPBut.SuspendLayout()
        CType(Me.OrderDetailsTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colLabOrderID, Me.colLabName, Me.colLabID, Me.colPatientID, Me.colPatientName, Me.colImprType, Me.colImprDet, Me.colImprClr, Me.colImprCount, Me.colDeliveryDate, Me.colPrice, Me.colRecieveDate, Me.colNotes})
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
        'colLabOrderID
        '
        Me.colLabOrderID.FieldName = "LabOrderID"
        Me.colLabOrderID.Name = "colLabOrderID"
        resources.ApplyResources(Me.colLabOrderID, "colLabOrderID")
        '
        'colLabName
        '
        resources.ApplyResources(Me.colLabName, "colLabName")
        Me.colLabName.FieldName = "LabName"
        Me.colLabName.Name = "colLabName"
        '
        'colLabID
        '
        Me.colLabID.FieldName = "LabID"
        Me.colLabID.Name = "colLabID"
        '
        'colPatientID
        '
        Me.colPatientID.FieldName = "PatientID"
        Me.colPatientID.Name = "colPatientID"
        '
        'colPatientName
        '
        resources.ApplyResources(Me.colPatientName, "colPatientName")
        Me.colPatientName.FieldName = "PatientName"
        Me.colPatientName.Name = "colPatientName"
        '
        'colImprType
        '
        Me.colImprType.FieldName = "ImprType"
        Me.colImprType.Name = "colImprType"
        resources.ApplyResources(Me.colImprType, "colImprType")
        '
        'colImprDet
        '
        Me.colImprDet.FieldName = "ImprDet"
        Me.colImprDet.Name = "colImprDet"
        resources.ApplyResources(Me.colImprDet, "colImprDet")
        '
        'colImprClr
        '
        Me.colImprClr.FieldName = "ImprClr"
        Me.colImprClr.Name = "colImprClr"
        resources.ApplyResources(Me.colImprClr, "colImprClr")
        '
        'colImprCount
        '
        Me.colImprCount.FieldName = "ImprCount"
        Me.colImprCount.Name = "colImprCount"
        resources.ApplyResources(Me.colImprCount, "colImprCount")
        '
        'colDeliveryDate
        '
        Me.colDeliveryDate.FieldName = "DeliveryDate"
        Me.colDeliveryDate.Name = "colDeliveryDate"
        resources.ApplyResources(Me.colDeliveryDate, "colDeliveryDate")
        '
        'colPrice
        '
        Me.colPrice.FieldName = "Price"
        Me.colPrice.Name = "colPrice"
        resources.ApplyResources(Me.colPrice, "colPrice")
        '
        'colRecieveDate
        '
        Me.colRecieveDate.FieldName = "RecieveDate"
        Me.colRecieveDate.Name = "colRecieveDate"
        resources.ApplyResources(Me.colRecieveDate, "colRecieveDate")
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
        Me.bAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
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
        Me.bDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
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
        Me.TableData.Controls.Add(Me.RecieveDateDateEdit)
        Me.TableData.Controls.Add(Me.DeliveryDateDateEdit)
        Me.TableData.Controls.Add(Me.ImprDetCombo1)
        Me.TableData.Controls.Add(Me.ImpClrsCombo1)
        Me.TableData.Controls.Add(Me.ImpressionCombo1)
        Me.TableData.Controls.Add(Me.LabOrderIDLabel)
        Me.TableData.Controls.Add(Me.LabOrderIDSpinEdit)
        Me.TableData.Controls.Add(Me.LabIDLabel)
        Me.TableData.Controls.Add(Me.colLabCombo)
        Me.TableData.Controls.Add(Me.PatientIDLabel)
        Me.TableData.Controls.Add(Me.colPatientCombo)
        Me.TableData.Controls.Add(Me.ImprTypeLabel)
        Me.TableData.Controls.Add(Me.ImprDetLabel)
        Me.TableData.Controls.Add(Me.ImprClrLabel)
        Me.TableData.Controls.Add(Me.ImprCountLabel)
        Me.TableData.Controls.Add(Me.ImprCountSpinEdit)
        Me.TableData.Controls.Add(Me.DeliveryDateLabel)
        Me.TableData.Controls.Add(Me.PriceLabel)
        Me.TableData.Controls.Add(Me.PriceSpinEdit)
        Me.TableData.Controls.Add(Me.RecieveDateLabel)
        Me.TableData.Controls.Add(Me.NotesLabel)
        Me.TableData.Controls.Add(Me.NotesTextEdit)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Controls.Add(Me.LabelControl1)
        Me.TableData.Controls.Add(Me.OrderDetailsTextEdit)
        resources.ApplyResources(Me.TableData, "TableData")
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TableData.UseSkinIndents = True
        '
        'RecieveDateDateEdit
        '
        Me.TableData.SetColumn(Me.RecieveDateDateEdit, 5)
        resources.ApplyResources(Me.RecieveDateDateEdit, "RecieveDateDateEdit")
        Me.RecieveDateDateEdit.MenuManager = Me.ribbonControl
        Me.RecieveDateDateEdit.Name = "RecieveDateDateEdit"
        Me.RecieveDateDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RecieveDateDateEdit.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RecieveDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RecieveDateDateEdit.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.RecieveDateDateEdit, 4)
        '
        'DeliveryDateDateEdit
        '
        Me.TableData.SetColumn(Me.DeliveryDateDateEdit, 5)
        resources.ApplyResources(Me.DeliveryDateDateEdit, "DeliveryDateDateEdit")
        Me.DeliveryDateDateEdit.MenuManager = Me.ribbonControl
        Me.DeliveryDateDateEdit.Name = "DeliveryDateDateEdit"
        Me.DeliveryDateDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("DeliveryDateDateEdit.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.DeliveryDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("DeliveryDateDateEdit.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.DeliveryDateDateEdit, 3)
        '
        'ImprDetCombo1
        '
        Me.TableData.SetColumn(Me.ImprDetCombo1, 2)
        Me.ImprDetCombo1.ImpDetID = 0
        Me.ImprDetCombo1.ImprDetail = "0"
        Me.ImprDetCombo1.ImprID = 0
        resources.ApplyResources(Me.ImprDetCombo1, "ImprDetCombo1")
        Me.ImprDetCombo1.Name = "ImprDetCombo1"
        Me.TableData.SetRow(Me.ImprDetCombo1, 2)
        '
        'ImpClrsCombo1
        '
        Me.TableData.SetColumn(Me.ImpClrsCombo1, 5)
        Me.ImpClrsCombo1.ImpClr = Nothing
        Me.ImpClrsCombo1.ImpClrID = 0
        resources.ApplyResources(Me.ImpClrsCombo1, "ImpClrsCombo1")
        Me.ImpClrsCombo1.Name = "ImpClrsCombo1"
        Me.TableData.SetRow(Me.ImpClrsCombo1, 2)
        '
        'ImpressionCombo1
        '
        Me.TableData.SetColumn(Me.ImpressionCombo1, 5)
        Me.ImpressionCombo1.ImprID = 0
        Me.ImpressionCombo1.ImprType = Nothing
        resources.ApplyResources(Me.ImpressionCombo1, "ImpressionCombo1")
        Me.ImpressionCombo1.Name = "ImpressionCombo1"
        Me.TableData.SetRow(Me.ImpressionCombo1, 1)
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
        Me.TableData.SetRow(Me.LabOrderIDLabel, 0)
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
        Me.TableData.SetRow(Me.LabOrderIDSpinEdit, 0)
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
        'colLabCombo
        '
        Me.colLabCombo.Appearance.Font = CType(resources.GetObject("colLabCombo.Appearance.Font"), System.Drawing.Font)
        Me.colLabCombo.Appearance.Options.UseFont = True
        Me.TableData.SetColumn(Me.colLabCombo, 5)
        resources.ApplyResources(Me.colLabCombo, "colLabCombo")
        Me.colLabCombo.LabID = 0
        Me.colLabCombo.LabName = Nothing
        Me.colLabCombo.Name = "colLabCombo"
        Me.TableData.SetRow(Me.colLabCombo, 0)
        '
        'PatientIDLabel
        '
        Me.PatientIDLabel.Appearance.Font = CType(resources.GetObject("PatientIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.PatientIDLabel.Appearance.Options.UseFont = True
        Me.PatientIDLabel.Appearance.Options.UseTextOptions = True
        Me.PatientIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PatientIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PatientIDLabel, "PatientIDLabel")
        Me.TableData.SetColumn(Me.PatientIDLabel, 1)
        Me.PatientIDLabel.Name = "PatientIDLabel"
        Me.TableData.SetRow(Me.PatientIDLabel, 1)
        '
        'colPatientCombo
        '
        Me.colPatientCombo.Appearance.Font = CType(resources.GetObject("colPatientCombo.Appearance.Font"), System.Drawing.Font)
        Me.colPatientCombo.Appearance.Options.UseFont = True
        Me.TableData.SetColumn(Me.colPatientCombo, 2)
        resources.ApplyResources(Me.colPatientCombo, "colPatientCombo")
        Me.colPatientCombo.Name = "colPatientCombo"
        Me.colPatientCombo.PatientID = 0
        Me.colPatientCombo.PatientName = Nothing
        Me.TableData.SetRow(Me.colPatientCombo, 1)
        '
        'ImprTypeLabel
        '
        Me.ImprTypeLabel.Appearance.Font = CType(resources.GetObject("ImprTypeLabel.Appearance.Font"), System.Drawing.Font)
        Me.ImprTypeLabel.Appearance.Options.UseFont = True
        Me.ImprTypeLabel.Appearance.Options.UseTextOptions = True
        Me.ImprTypeLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ImprTypeLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ImprTypeLabel, "ImprTypeLabel")
        Me.TableData.SetColumn(Me.ImprTypeLabel, 4)
        Me.ImprTypeLabel.Name = "ImprTypeLabel"
        Me.TableData.SetRow(Me.ImprTypeLabel, 1)
        '
        'ImprDetLabel
        '
        Me.ImprDetLabel.Appearance.Font = CType(resources.GetObject("ImprDetLabel.Appearance.Font"), System.Drawing.Font)
        Me.ImprDetLabel.Appearance.Options.UseFont = True
        Me.ImprDetLabel.Appearance.Options.UseTextOptions = True
        Me.ImprDetLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ImprDetLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ImprDetLabel, "ImprDetLabel")
        Me.TableData.SetColumn(Me.ImprDetLabel, 1)
        Me.ImprDetLabel.Name = "ImprDetLabel"
        Me.TableData.SetRow(Me.ImprDetLabel, 2)
        '
        'ImprClrLabel
        '
        Me.ImprClrLabel.Appearance.Font = CType(resources.GetObject("ImprClrLabel.Appearance.Font"), System.Drawing.Font)
        Me.ImprClrLabel.Appearance.Options.UseFont = True
        Me.ImprClrLabel.Appearance.Options.UseTextOptions = True
        Me.ImprClrLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ImprClrLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ImprClrLabel, "ImprClrLabel")
        Me.TableData.SetColumn(Me.ImprClrLabel, 4)
        Me.ImprClrLabel.Name = "ImprClrLabel"
        Me.TableData.SetRow(Me.ImprClrLabel, 2)
        '
        'ImprCountLabel
        '
        Me.ImprCountLabel.Appearance.Font = CType(resources.GetObject("ImprCountLabel.Appearance.Font"), System.Drawing.Font)
        Me.ImprCountLabel.Appearance.Options.UseFont = True
        Me.ImprCountLabel.Appearance.Options.UseTextOptions = True
        Me.ImprCountLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ImprCountLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ImprCountLabel, "ImprCountLabel")
        Me.TableData.SetColumn(Me.ImprCountLabel, 1)
        Me.ImprCountLabel.Name = "ImprCountLabel"
        Me.TableData.SetRow(Me.ImprCountLabel, 3)
        '
        'ImprCountSpinEdit
        '
        Me.TableData.SetColumn(Me.ImprCountSpinEdit, 2)
        resources.ApplyResources(Me.ImprCountSpinEdit, "ImprCountSpinEdit")
        Me.ImprCountSpinEdit.Name = "ImprCountSpinEdit"
        Me.ImprCountSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("ImprCountSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ImprCountSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.ImprCountSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ImprCountSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ImprCountSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.ImprCountSpinEdit, 3)
        '
        'DeliveryDateLabel
        '
        Me.DeliveryDateLabel.Appearance.Font = CType(resources.GetObject("DeliveryDateLabel.Appearance.Font"), System.Drawing.Font)
        Me.DeliveryDateLabel.Appearance.Options.UseFont = True
        Me.DeliveryDateLabel.Appearance.Options.UseTextOptions = True
        Me.DeliveryDateLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DeliveryDateLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.DeliveryDateLabel, "DeliveryDateLabel")
        Me.TableData.SetColumn(Me.DeliveryDateLabel, 4)
        Me.DeliveryDateLabel.Name = "DeliveryDateLabel"
        Me.TableData.SetRow(Me.DeliveryDateLabel, 3)
        '
        'PriceLabel
        '
        Me.PriceLabel.Appearance.Font = CType(resources.GetObject("PriceLabel.Appearance.Font"), System.Drawing.Font)
        Me.PriceLabel.Appearance.Options.UseFont = True
        Me.PriceLabel.Appearance.Options.UseTextOptions = True
        Me.PriceLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PriceLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PriceLabel, "PriceLabel")
        Me.TableData.SetColumn(Me.PriceLabel, 1)
        Me.PriceLabel.Name = "PriceLabel"
        Me.TableData.SetRow(Me.PriceLabel, 4)
        '
        'PriceSpinEdit
        '
        Me.TableData.SetColumn(Me.PriceSpinEdit, 2)
        resources.ApplyResources(Me.PriceSpinEdit, "PriceSpinEdit")
        Me.PriceSpinEdit.Name = "PriceSpinEdit"
        Me.PriceSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("PriceSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PriceSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.PriceSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PriceSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PriceSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.PriceSpinEdit, 4)
        '
        'RecieveDateLabel
        '
        Me.RecieveDateLabel.Appearance.Font = CType(resources.GetObject("RecieveDateLabel.Appearance.Font"), System.Drawing.Font)
        Me.RecieveDateLabel.Appearance.Options.UseFont = True
        Me.RecieveDateLabel.Appearance.Options.UseTextOptions = True
        Me.RecieveDateLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RecieveDateLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.RecieveDateLabel, "RecieveDateLabel")
        Me.TableData.SetColumn(Me.RecieveDateLabel, 4)
        Me.RecieveDateLabel.Name = "RecieveDateLabel"
        Me.TableData.SetRow(Me.RecieveDateLabel, 4)
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
        Me.TableData.SetRow(Me.NotesLabel, 6)
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
        Me.TableData.SetRow(Me.NotesTextEdit, 6)
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
        Me.TableData.SetRow(Me.TLPBut, 7)
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
        Me.TableData.SetRow(Me.LabelControl1, 5)
        '
        'OrderDetailsTextEdit
        '
        Me.TableData.SetColumn(Me.OrderDetailsTextEdit, 2)
        Me.TableData.SetColumnSpan(Me.OrderDetailsTextEdit, 4)
        resources.ApplyResources(Me.OrderDetailsTextEdit, "OrderDetailsTextEdit")
        Me.OrderDetailsTextEdit.Name = "OrderDetailsTextEdit"
        Me.OrderDetailsTextEdit.Properties.Appearance.Font = CType(resources.GetObject("OrderDetailsTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.OrderDetailsTextEdit.Properties.Appearance.Options.UseFont = True
        Me.OrderDetailsTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.OrderDetailsTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.OrderDetailsTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetRow(Me.OrderDetailsTextEdit, 5)
        '
        'FrmRecieveLabOrder
        '
        Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[True]
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ribbonStatusBar)
        Me.Controls.Add(Me.ribbonControl)
        Me.Name = "FrmRecieveLabOrder"
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
        CType(Me.RecieveDateDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RecieveDateDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DeliveryDateDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DeliveryDateDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabOrderIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImprCountSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PriceSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NotesTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLPBut.ResumeLayout(False)
        CType(Me.OrderDetailsTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
		Friend WithEvents LabOrderIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents LabOrderIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colLabOrderID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents LabIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents LabIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colLabID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PatientIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents PatientIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ImprTypeLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colImprType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ImprDetLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colImprDet As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ImprClrLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colImprClr As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents ImprCountSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents ImprCountLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colImprCount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DeliveryDateLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PriceSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents PriceLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RecieveDateLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colRecieveDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents NotesTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents NotesLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLabCombo As DentistX.LabCombo
    Friend WithEvents colPatientCombo As DentistX.PatientCombo
    Friend WithEvents ImprDetCombo1 As ImprDetCombo
    Friend WithEvents ImpClrsCombo1 As ImpClrsCombo
    Friend WithEvents ImpressionCombo1 As ImpressionCombo
    Friend WithEvents colLabName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents OrderDetailsTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RecieveDateDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DeliveryDateDateEdit As DevExpress.XtraEditors.DateEdit
End Class
