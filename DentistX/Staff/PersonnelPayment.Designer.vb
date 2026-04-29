<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PersonnelPayment
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PersonnelPayment))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPaymentID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPersonType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPersonID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPersonName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPaymentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPaymentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayPeriodStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayPeriodEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReferenceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TableData = New DevExpress.Utils.Layout.TablePanel()
        Me.lblPersonType = New DevExpress.XtraEditors.LabelControl()
        Me.cboPersonType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblPersonID = New DevExpress.XtraEditors.LabelControl()
        Me.cboPersonID = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblAmount = New DevExpress.XtraEditors.LabelControl()
        Me.spnAmount = New DevExpress.XtraEditors.SpinEdit()
        Me.lblPaymentDate = New DevExpress.XtraEditors.LabelControl()
        Me.dtePaymentDate = New DevExpress.XtraEditors.DateEdit()
        Me.lblPaymentType = New DevExpress.XtraEditors.LabelControl()
        Me.cboPaymentType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblPayPeriodStart = New DevExpress.XtraEditors.LabelControl()
        Me.dtePayPeriodStart = New DevExpress.XtraEditors.DateEdit()
        Me.lblPayPeriodEnd = New DevExpress.XtraEditors.LabelControl()
        Me.dtePayPeriodEnd = New DevExpress.XtraEditors.DateEdit()
        Me.lblDescription = New DevExpress.XtraEditors.LabelControl()
        Me.txtDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.lblReferenceNo = New DevExpress.XtraEditors.LabelControl()
        Me.txtReferenceNo = New DevExpress.XtraEditors.TextEdit()
        Me.TLPBut = New DevExpress.Utils.Layout.TablePanel()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel1.SuspendLayout()
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel2.SuspendLayout()
        Me.Splitter1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableData.SuspendLayout()
        CType(Me.cboPersonType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPersonID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spnAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtePaymentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtePaymentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPaymentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtePayPeriodStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtePayPeriodStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtePayPeriodEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtePayPeriodEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReferenceNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLPBut.SuspendLayout()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.CaptionImageOptions.ImageKey = resources.GetString("Splitter1.CaptionImageOptions.ImageKey")
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
        Me.Splitter1.SplitterPosition = 240
        '
        'DGV
        '
        resources.ApplyResources(Me.DGV, "DGV")
        Me.DGV.EmbeddedNavigator.AccessibleDescription = resources.GetString("DGV.EmbeddedNavigator.AccessibleDescription")
        Me.DGV.EmbeddedNavigator.AccessibleName = resources.GetString("DGV.EmbeddedNavigator.AccessibleName")
        Me.DGV.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("DGV.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.DGV.EmbeddedNavigator.Anchor = CType(resources.GetObject("DGV.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.DGV.EmbeddedNavigator.AutoSize = CType(resources.GetObject("DGV.EmbeddedNavigator.AutoSize"), Boolean)
        Me.DGV.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("DGV.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.DGV.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("DGV.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.DGV.EmbeddedNavigator.ImeMode = CType(resources.GetObject("DGV.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.DGV.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("DGV.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.DGV.EmbeddedNavigator.TextLocation = CType(resources.GetObject("DGV.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.DGV.EmbeddedNavigator.ToolTip = resources.GetString("DGV.EmbeddedNavigator.ToolTip")
        Me.DGV.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("DGV.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.DGV.EmbeddedNavigator.ToolTipTitle = resources.GetString("DGV.EmbeddedNavigator.ToolTipTitle")
        Me.DGV.MainView = Me.dgView
        Me.DGV.Name = "DGV"
        Me.DGV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        resources.ApplyResources(Me.dgView, "dgView")
        Me.dgView.Appearance.HeaderPanel.Font = CType(resources.GetObject("dgView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
        Me.dgView.Appearance.Row.Font = CType(resources.GetObject("dgView.Appearance.Row.Font"), System.Drawing.Font)
        Me.dgView.Appearance.Row.Options.UseFont = True
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colPaymentID, Me.colPersonType, Me.colPersonID, Me.colPersonName, Me.colAmount, Me.colPaymentDate, Me.colPaymentType, Me.colPayPeriodStart, Me.colPayPeriodEnd, Me.colDescription, Me.colReferenceNo})
        Me.dgView.GridControl = Me.DGV
        Me.dgView.Name = "dgView"
        Me.dgView.OptionsBehavior.Editable = False
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
        'colPaymentID
        '
        resources.ApplyResources(Me.colPaymentID, "colPaymentID")
        Me.colPaymentID.FieldName = "PaymentID"
        Me.colPaymentID.ImageOptions.ImageKey = resources.GetString("colPaymentID.ImageOptions.ImageKey")
        Me.colPaymentID.Name = "colPaymentID"
        '
        'colPersonType
        '
        resources.ApplyResources(Me.colPersonType, "colPersonType")
        Me.colPersonType.FieldName = "PersonType"
        Me.colPersonType.ImageOptions.ImageKey = resources.GetString("colPersonType.ImageOptions.ImageKey")
        Me.colPersonType.Name = "colPersonType"
        '
        'colPersonID
        '
        resources.ApplyResources(Me.colPersonID, "colPersonID")
        Me.colPersonID.FieldName = "PersonID"
        Me.colPersonID.ImageOptions.ImageKey = resources.GetString("colPersonID.ImageOptions.ImageKey")
        Me.colPersonID.Name = "colPersonID"
        '
        'colPersonName
        '
        resources.ApplyResources(Me.colPersonName, "colPersonName")
        Me.colPersonName.FieldName = "PersonName"
        Me.colPersonName.ImageOptions.ImageKey = resources.GetString("colPersonName.ImageOptions.ImageKey")
        Me.colPersonName.Name = "colPersonName"
        '
        'colAmount
        '
        resources.ApplyResources(Me.colAmount, "colAmount")
        Me.colAmount.FieldName = "Amount"
        Me.colAmount.ImageOptions.ImageKey = resources.GetString("colAmount.ImageOptions.ImageKey")
        Me.colAmount.Name = "colAmount"
        '
        'colPaymentDate
        '
        resources.ApplyResources(Me.colPaymentDate, "colPaymentDate")
        Me.colPaymentDate.FieldName = "PaymentDate"
        Me.colPaymentDate.ImageOptions.ImageKey = resources.GetString("colPaymentDate.ImageOptions.ImageKey")
        Me.colPaymentDate.Name = "colPaymentDate"
        '
        'colPaymentType
        '
        resources.ApplyResources(Me.colPaymentType, "colPaymentType")
        Me.colPaymentType.FieldName = "PaymentType"
        Me.colPaymentType.ImageOptions.ImageKey = resources.GetString("colPaymentType.ImageOptions.ImageKey")
        Me.colPaymentType.Name = "colPaymentType"
        '
        'colPayPeriodStart
        '
        resources.ApplyResources(Me.colPayPeriodStart, "colPayPeriodStart")
        Me.colPayPeriodStart.FieldName = "PayPeriodStart"
        Me.colPayPeriodStart.ImageOptions.ImageKey = resources.GetString("colPayPeriodStart.ImageOptions.ImageKey")
        Me.colPayPeriodStart.Name = "colPayPeriodStart"
        '
        'colPayPeriodEnd
        '
        resources.ApplyResources(Me.colPayPeriodEnd, "colPayPeriodEnd")
        Me.colPayPeriodEnd.FieldName = "PayPeriodEnd"
        Me.colPayPeriodEnd.ImageOptions.ImageKey = resources.GetString("colPayPeriodEnd.ImageOptions.ImageKey")
        Me.colPayPeriodEnd.Name = "colPayPeriodEnd"
        '
        'colDescription
        '
        resources.ApplyResources(Me.colDescription, "colDescription")
        Me.colDescription.FieldName = "Description"
        Me.colDescription.ImageOptions.ImageKey = resources.GetString("colDescription.ImageOptions.ImageKey")
        Me.colDescription.Name = "colDescription"
        '
        'colReferenceNo
        '
        resources.ApplyResources(Me.colReferenceNo, "colReferenceNo")
        Me.colReferenceNo.FieldName = "ReferenceNo"
        Me.colReferenceNo.ImageOptions.ImageKey = resources.GetString("colReferenceNo.ImageOptions.ImageKey")
        Me.colReferenceNo.Name = "colReferenceNo"
        '
        'TableData
        '
        resources.ApplyResources(Me.TableData, "TableData")
        Me.TableData.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 75.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 75.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 75.0!)})
        Me.TableData.Controls.Add(Me.lblPersonType)
        Me.TableData.Controls.Add(Me.cboPersonType)
        Me.TableData.Controls.Add(Me.lblPersonID)
        Me.TableData.Controls.Add(Me.cboPersonID)
        Me.TableData.Controls.Add(Me.lblAmount)
        Me.TableData.Controls.Add(Me.spnAmount)
        Me.TableData.Controls.Add(Me.lblPaymentDate)
        Me.TableData.Controls.Add(Me.dtePaymentDate)
        Me.TableData.Controls.Add(Me.lblPaymentType)
        Me.TableData.Controls.Add(Me.cboPaymentType)
        Me.TableData.Controls.Add(Me.lblPayPeriodStart)
        Me.TableData.Controls.Add(Me.dtePayPeriodStart)
        Me.TableData.Controls.Add(Me.lblPayPeriodEnd)
        Me.TableData.Controls.Add(Me.dtePayPeriodEnd)
        Me.TableData.Controls.Add(Me.lblDescription)
        Me.TableData.Controls.Add(Me.txtDescription)
        Me.TableData.Controls.Add(Me.lblReferenceNo)
        Me.TableData.Controls.Add(Me.txtReferenceNo)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 60.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 16.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 36.0!)})
        '
        'lblPersonType
        '
        resources.ApplyResources(Me.lblPersonType, "lblPersonType")
        Me.lblPersonType.Appearance.Font = CType(resources.GetObject("lblPersonType.Appearance.Font"), System.Drawing.Font)
        Me.lblPersonType.Appearance.Options.UseFont = True
        Me.lblPersonType.Appearance.Options.UseTextOptions = True
        Me.lblPersonType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblPersonType.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblPersonType, 0)
        Me.lblPersonType.Name = "lblPersonType"
        Me.TableData.SetRow(Me.lblPersonType, 0)
        '
        'cboPersonType
        '
        resources.ApplyResources(Me.cboPersonType, "cboPersonType")
        Me.TableData.SetColumn(Me.cboPersonType, 1)
        Me.cboPersonType.Name = "cboPersonType"
        Me.cboPersonType.Properties.Appearance.Font = CType(resources.GetObject("cboPersonType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboPersonType.Properties.Appearance.Options.UseFont = True
        Me.cboPersonType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPersonType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboPersonType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.TableData.SetRow(Me.cboPersonType, 0)
        '
        'lblPersonID
        '
        resources.ApplyResources(Me.lblPersonID, "lblPersonID")
        Me.lblPersonID.Appearance.Font = CType(resources.GetObject("lblPersonID.Appearance.Font"), System.Drawing.Font)
        Me.lblPersonID.Appearance.Options.UseFont = True
        Me.lblPersonID.Appearance.Options.UseTextOptions = True
        Me.lblPersonID.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblPersonID.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblPersonID, 2)
        Me.lblPersonID.Name = "lblPersonID"
        Me.TableData.SetRow(Me.lblPersonID, 0)
        '
        'cboPersonID
        '
        resources.ApplyResources(Me.cboPersonID, "cboPersonID")
        Me.TableData.SetColumn(Me.cboPersonID, 3)
        Me.cboPersonID.Name = "cboPersonID"
        Me.cboPersonID.Properties.Appearance.Font = CType(resources.GetObject("cboPersonID.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboPersonID.Properties.Appearance.Options.UseFont = True
        Me.cboPersonID.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPersonID.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboPersonID.Properties.NullText = resources.GetString("cboPersonID.Properties.NullText")
        Me.TableData.SetRow(Me.cboPersonID, 0)
        '
        'lblAmount
        '
        resources.ApplyResources(Me.lblAmount, "lblAmount")
        Me.lblAmount.Appearance.Font = CType(resources.GetObject("lblAmount.Appearance.Font"), System.Drawing.Font)
        Me.lblAmount.Appearance.Options.UseFont = True
        Me.lblAmount.Appearance.Options.UseTextOptions = True
        Me.lblAmount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblAmount.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblAmount, 4)
        Me.lblAmount.Name = "lblAmount"
        Me.TableData.SetRow(Me.lblAmount, 0)
        '
        'spnAmount
        '
        resources.ApplyResources(Me.spnAmount, "spnAmount")
        Me.TableData.SetColumn(Me.spnAmount, 5)
        Me.spnAmount.Name = "spnAmount"
        Me.spnAmount.Properties.Appearance.Font = CType(resources.GetObject("spnAmount.Properties.Appearance.Font"), System.Drawing.Font)
        Me.spnAmount.Properties.Appearance.Options.UseFont = True
        Me.spnAmount.Properties.DisplayFormat.FormatString = "n2"
        Me.spnAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.spnAmount.Properties.EditFormat.FormatString = "n2"
        Me.spnAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.spnAmount.Properties.MaskSettings.Set("mask", "n2")
        Me.TableData.SetRow(Me.spnAmount, 0)
        '
        'lblPaymentDate
        '
        resources.ApplyResources(Me.lblPaymentDate, "lblPaymentDate")
        Me.lblPaymentDate.Appearance.Font = CType(resources.GetObject("lblPaymentDate.Appearance.Font"), System.Drawing.Font)
        Me.lblPaymentDate.Appearance.Options.UseFont = True
        Me.lblPaymentDate.Appearance.Options.UseTextOptions = True
        Me.lblPaymentDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblPaymentDate.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblPaymentDate, 0)
        Me.lblPaymentDate.Name = "lblPaymentDate"
        Me.TableData.SetRow(Me.lblPaymentDate, 1)
        '
        'dtePaymentDate
        '
        resources.ApplyResources(Me.dtePaymentDate, "dtePaymentDate")
        Me.TableData.SetColumn(Me.dtePaymentDate, 1)
        Me.dtePaymentDate.Name = "dtePaymentDate"
        Me.dtePaymentDate.Properties.Appearance.Font = CType(resources.GetObject("dtePaymentDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtePaymentDate.Properties.Appearance.Options.UseFont = True
        Me.dtePaymentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtePaymentDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtePaymentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtePaymentDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.dtePaymentDate, 1)
        '
        'lblPaymentType
        '
        resources.ApplyResources(Me.lblPaymentType, "lblPaymentType")
        Me.lblPaymentType.Appearance.Font = CType(resources.GetObject("lblPaymentType.Appearance.Font"), System.Drawing.Font)
        Me.lblPaymentType.Appearance.Options.UseFont = True
        Me.lblPaymentType.Appearance.Options.UseTextOptions = True
        Me.lblPaymentType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblPaymentType.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblPaymentType, 2)
        Me.lblPaymentType.Name = "lblPaymentType"
        Me.TableData.SetRow(Me.lblPaymentType, 1)
        '
        'cboPaymentType
        '
        resources.ApplyResources(Me.cboPaymentType, "cboPaymentType")
        Me.TableData.SetColumn(Me.cboPaymentType, 3)
        Me.cboPaymentType.Name = "cboPaymentType"
        Me.cboPaymentType.Properties.Appearance.Font = CType(resources.GetObject("cboPaymentType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboPaymentType.Properties.Appearance.Options.UseFont = True
        Me.cboPaymentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPaymentType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboPaymentType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.TableData.SetRow(Me.cboPaymentType, 1)
        '
        'lblPayPeriodStart
        '
        resources.ApplyResources(Me.lblPayPeriodStart, "lblPayPeriodStart")
        Me.lblPayPeriodStart.Appearance.Font = CType(resources.GetObject("lblPayPeriodStart.Appearance.Font"), System.Drawing.Font)
        Me.lblPayPeriodStart.Appearance.Options.UseFont = True
        Me.lblPayPeriodStart.Appearance.Options.UseTextOptions = True
        Me.lblPayPeriodStart.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblPayPeriodStart.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblPayPeriodStart, 0)
        Me.lblPayPeriodStart.Name = "lblPayPeriodStart"
        Me.TableData.SetRow(Me.lblPayPeriodStart, 2)
        '
        'dtePayPeriodStart
        '
        resources.ApplyResources(Me.dtePayPeriodStart, "dtePayPeriodStart")
        Me.TableData.SetColumn(Me.dtePayPeriodStart, 1)
        Me.dtePayPeriodStart.Name = "dtePayPeriodStart"
        Me.dtePayPeriodStart.Properties.Appearance.Font = CType(resources.GetObject("dtePayPeriodStart.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtePayPeriodStart.Properties.Appearance.Options.UseFont = True
        Me.dtePayPeriodStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtePayPeriodStart.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtePayPeriodStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtePayPeriodStart.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.dtePayPeriodStart, 2)
        '
        'lblPayPeriodEnd
        '
        resources.ApplyResources(Me.lblPayPeriodEnd, "lblPayPeriodEnd")
        Me.lblPayPeriodEnd.Appearance.Font = CType(resources.GetObject("lblPayPeriodEnd.Appearance.Font"), System.Drawing.Font)
        Me.lblPayPeriodEnd.Appearance.Options.UseFont = True
        Me.lblPayPeriodEnd.Appearance.Options.UseTextOptions = True
        Me.lblPayPeriodEnd.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblPayPeriodEnd.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblPayPeriodEnd, 2)
        Me.lblPayPeriodEnd.Name = "lblPayPeriodEnd"
        Me.TableData.SetRow(Me.lblPayPeriodEnd, 2)
        '
        'dtePayPeriodEnd
        '
        resources.ApplyResources(Me.dtePayPeriodEnd, "dtePayPeriodEnd")
        Me.TableData.SetColumn(Me.dtePayPeriodEnd, 3)
        Me.dtePayPeriodEnd.Name = "dtePayPeriodEnd"
        Me.dtePayPeriodEnd.Properties.Appearance.Font = CType(resources.GetObject("dtePayPeriodEnd.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtePayPeriodEnd.Properties.Appearance.Options.UseFont = True
        Me.dtePayPeriodEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtePayPeriodEnd.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtePayPeriodEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtePayPeriodEnd.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.dtePayPeriodEnd, 2)
        '
        'lblDescription
        '
        resources.ApplyResources(Me.lblDescription, "lblDescription")
        Me.lblDescription.Appearance.Font = CType(resources.GetObject("lblDescription.Appearance.Font"), System.Drawing.Font)
        Me.lblDescription.Appearance.Options.UseFont = True
        Me.lblDescription.Appearance.Options.UseTextOptions = True
        Me.lblDescription.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblDescription.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblDescription, 0)
        Me.lblDescription.Name = "lblDescription"
        Me.TableData.SetRow(Me.lblDescription, 3)
        '
        'txtDescription
        '
        resources.ApplyResources(Me.txtDescription, "txtDescription")
        Me.TableData.SetColumn(Me.txtDescription, 1)
        Me.TableData.SetColumnSpan(Me.txtDescription, 3)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Properties.Appearance.Font = CType(resources.GetObject("txtDescription.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtDescription.Properties.Appearance.Options.UseFont = True
        Me.txtDescription.Properties.MaxLength = 500
        Me.TableData.SetRow(Me.txtDescription, 3)
        '
        'lblReferenceNo
        '
        resources.ApplyResources(Me.lblReferenceNo, "lblReferenceNo")
        Me.lblReferenceNo.Appearance.Font = CType(resources.GetObject("lblReferenceNo.Appearance.Font"), System.Drawing.Font)
        Me.lblReferenceNo.Appearance.Options.UseFont = True
        Me.lblReferenceNo.Appearance.Options.UseTextOptions = True
        Me.lblReferenceNo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblReferenceNo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblReferenceNo, 4)
        Me.lblReferenceNo.Name = "lblReferenceNo"
        Me.TableData.SetRow(Me.lblReferenceNo, 2)
        '
        'txtReferenceNo
        '
        resources.ApplyResources(Me.txtReferenceNo, "txtReferenceNo")
        Me.TableData.SetColumn(Me.txtReferenceNo, 5)
        Me.txtReferenceNo.Name = "txtReferenceNo"
        Me.txtReferenceNo.Properties.Appearance.Font = CType(resources.GetObject("txtReferenceNo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtReferenceNo.Properties.Appearance.Options.UseFont = True
        Me.txtReferenceNo.Properties.MaxLength = 100
        Me.TableData.SetRow(Me.txtReferenceNo, 2)
        '
        'TLPBut
        '
        resources.ApplyResources(Me.TLPBut, "TLPBut")
        Me.TableData.SetColumn(Me.TLPBut, 0)
        Me.TLPBut.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!)})
        Me.TableData.SetColumnSpan(Me.TLPBut, 6)
        Me.TLPBut.Controls.Add(Me.btnAdd)
        Me.TLPBut.Controls.Add(Me.btnEdit)
        Me.TLPBut.Controls.Add(Me.btnDelete)
        Me.TLPBut.Controls.Add(Me.btnSave)
        Me.TLPBut.Controls.Add(Me.btnCancel)
        Me.TLPBut.Controls.Add(Me.btnClose)
        Me.TLPBut.Name = "TLPBut"
        Me.TableData.SetRow(Me.TLPBut, 5)
        Me.TLPBut.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32.0!)})
        '
        'btnAdd
        '
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.Appearance.Font = CType(resources.GetObject("btnAdd.Appearance.Font"), System.Drawing.Font)
        Me.btnAdd.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnAdd, 1)
        Me.btnAdd.ImageOptions.ImageKey = resources.GetString("btnAdd.ImageOptions.ImageKey")
        Me.btnAdd.Name = "btnAdd"
        Me.TLPBut.SetRow(Me.btnAdd, 0)
        '
        'btnEdit
        '
        resources.ApplyResources(Me.btnEdit, "btnEdit")
        Me.btnEdit.Appearance.Font = CType(resources.GetObject("btnEdit.Appearance.Font"), System.Drawing.Font)
        Me.btnEdit.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnEdit, 2)
        Me.btnEdit.ImageOptions.ImageKey = resources.GetString("btnEdit.ImageOptions.ImageKey")
        Me.btnEdit.Name = "btnEdit"
        Me.TLPBut.SetRow(Me.btnEdit, 0)
        '
        'btnDelete
        '
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.Appearance.Font = CType(resources.GetObject("btnDelete.Appearance.Font"), System.Drawing.Font)
        Me.btnDelete.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnDelete, 3)
        Me.btnDelete.ImageOptions.ImageKey = resources.GetString("btnDelete.ImageOptions.ImageKey")
        Me.btnDelete.Name = "btnDelete"
        Me.TLPBut.SetRow(Me.btnDelete, 0)
        '
        'btnSave
        '
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Appearance.Font = CType(resources.GetObject("btnSave.Appearance.Font"), System.Drawing.Font)
        Me.btnSave.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnSave, 4)
        Me.btnSave.ImageOptions.ImageKey = resources.GetString("btnSave.ImageOptions.ImageKey")
        Me.btnSave.Name = "btnSave"
        Me.TLPBut.SetRow(Me.btnSave, 0)
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnCancel, 5)
        Me.btnCancel.ImageOptions.ImageKey = resources.GetString("btnCancel.ImageOptions.ImageKey")
        Me.btnCancel.Name = "btnCancel"
        Me.TLPBut.SetRow(Me.btnCancel, 0)
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.Appearance.Font = CType(resources.GetObject("btnClose.Appearance.Font"), System.Drawing.Font)
        Me.btnClose.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnClose, 6)
        Me.btnClose.ImageOptions.ImageKey = resources.GetString("btnClose.ImageOptions.ImageKey")
        Me.btnClose.Name = "btnClose"
        Me.TLPBut.SetRow(Me.btnClose, 0)
        '
        'PersonnelPayment
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "PersonnelPayment"
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel1.ResumeLayout(False)
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel2.ResumeLayout(False)
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.ResumeLayout(False)
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableData.ResumeLayout(False)
        Me.TableData.PerformLayout()
        CType(Me.cboPersonType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPersonID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spnAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtePaymentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtePaymentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPaymentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtePayPeriodStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtePayPeriodStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtePayPeriodEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtePayPeriodEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReferenceNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLPBut.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Splitter1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents DGV As DevExpress.XtraGrid.GridControl
    Friend WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPaymentID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPersonType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPersonID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPersonName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPaymentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPaymentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayPeriodStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayPeriodEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReferenceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TableData As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents lblPersonType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPersonType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblPersonID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPersonID As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblAmount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents spnAmount As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents lblPaymentDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtePaymentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblPaymentType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPaymentType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblPayPeriodStart As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtePayPeriodStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblPayPeriodEnd As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtePayPeriodEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblDescription As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents lblReferenceNo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtReferenceNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TLPBut As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
End Class
