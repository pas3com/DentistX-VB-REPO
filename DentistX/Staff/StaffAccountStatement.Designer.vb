<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StaffAccountStatement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StaffAccountStatement))
        Me.panelFilter = New DevExpress.XtraEditors.PanelControl()
        Me.lblPersonType = New DevExpress.XtraEditors.LabelControl()
        Me.cboPersonType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblPerson = New DevExpress.XtraEditors.LabelControl()
        Me.cboPerson = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblDateFrom = New DevExpress.XtraEditors.LabelControl()
        Me.dteDateFrom = New DevExpress.XtraEditors.DateEdit()
        Me.lblDateTo = New DevExpress.XtraEditors.LabelControl()
        Me.dteDateTo = New DevExpress.XtraEditors.DateEdit()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.gridPayments = New DevExpress.XtraGrid.GridControl()
        Me.viewPayments = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colPaymentID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPersonName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPaymentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPaymentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.panelSummary = New DevExpress.XtraEditors.PanelControl()
        Me.lblTotalCaption = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotal = New DevExpress.XtraEditors.LabelControl()
        CType(Me.panelFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelFilter.SuspendLayout()
        CType(Me.cboPersonType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPerson.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dteDateFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dteDateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dteDateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dteDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelSummary.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelFilter
        '
        resources.ApplyResources(Me.panelFilter, "panelFilter")
        Me.panelFilter.Controls.Add(Me.lblPersonType)
        Me.panelFilter.Controls.Add(Me.cboPersonType)
        Me.panelFilter.Controls.Add(Me.lblPerson)
        Me.panelFilter.Controls.Add(Me.cboPerson)
        Me.panelFilter.Controls.Add(Me.lblDateFrom)
        Me.panelFilter.Controls.Add(Me.dteDateFrom)
        Me.panelFilter.Controls.Add(Me.lblDateTo)
        Me.panelFilter.Controls.Add(Me.dteDateTo)
        Me.panelFilter.Controls.Add(Me.btnRefresh)
        Me.panelFilter.Name = "panelFilter"
        '
        'lblPersonType
        '
        resources.ApplyResources(Me.lblPersonType, "lblPersonType")
        Me.lblPersonType.Appearance.Font = CType(resources.GetObject("lblPersonType.Appearance.Font"), System.Drawing.Font)
        Me.lblPersonType.Appearance.Options.UseFont = True
        Me.lblPersonType.Name = "lblPersonType"
        '
        'cboPersonType
        '
        resources.ApplyResources(Me.cboPersonType, "cboPersonType")
        Me.cboPersonType.Name = "cboPersonType"
        Me.cboPersonType.Properties.Appearance.Font = CType(resources.GetObject("cboPersonType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboPersonType.Properties.Appearance.Options.UseFont = True
        Me.cboPersonType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPersonType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboPersonType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'lblPerson
        '
        resources.ApplyResources(Me.lblPerson, "lblPerson")
        Me.lblPerson.Appearance.Font = CType(resources.GetObject("lblPerson.Appearance.Font"), System.Drawing.Font)
        Me.lblPerson.Appearance.Options.UseFont = True
        Me.lblPerson.Name = "lblPerson"
        '
        'cboPerson
        '
        resources.ApplyResources(Me.cboPerson, "cboPerson")
        Me.cboPerson.Name = "cboPerson"
        Me.cboPerson.Properties.Appearance.Font = CType(resources.GetObject("cboPerson.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboPerson.Properties.Appearance.Options.UseFont = True
        Me.cboPerson.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPerson.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboPerson.Properties.NullText = resources.GetString("cboPerson.Properties.NullText")
        '
        'lblDateFrom
        '
        resources.ApplyResources(Me.lblDateFrom, "lblDateFrom")
        Me.lblDateFrom.Appearance.Font = CType(resources.GetObject("lblDateFrom.Appearance.Font"), System.Drawing.Font)
        Me.lblDateFrom.Appearance.Options.UseFont = True
        Me.lblDateFrom.Name = "lblDateFrom"
        '
        'dteDateFrom
        '
        resources.ApplyResources(Me.dteDateFrom, "dteDateFrom")
        Me.dteDateFrom.Name = "dteDateFrom"
        Me.dteDateFrom.Properties.Appearance.Font = CType(resources.GetObject("dteDateFrom.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dteDateFrom.Properties.Appearance.Options.UseFont = True
        Me.dteDateFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dteDateFrom.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dteDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dteDateFrom.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblDateTo
        '
        resources.ApplyResources(Me.lblDateTo, "lblDateTo")
        Me.lblDateTo.Appearance.Font = CType(resources.GetObject("lblDateTo.Appearance.Font"), System.Drawing.Font)
        Me.lblDateTo.Appearance.Options.UseFont = True
        Me.lblDateTo.Name = "lblDateTo"
        '
        'dteDateTo
        '
        resources.ApplyResources(Me.dteDateTo, "dteDateTo")
        Me.dteDateTo.Name = "dteDateTo"
        Me.dteDateTo.Properties.Appearance.Font = CType(resources.GetObject("dteDateTo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dteDateTo.Properties.Appearance.Options.UseFont = True
        Me.dteDateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dteDateTo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dteDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dteDateTo.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btnRefresh
        '
        resources.ApplyResources(Me.btnRefresh, "btnRefresh")
        Me.btnRefresh.Appearance.Font = CType(resources.GetObject("btnRefresh.Appearance.Font"), System.Drawing.Font)
        Me.btnRefresh.Appearance.Options.UseFont = True
        Me.btnRefresh.ImageOptions.ImageKey = resources.GetString("btnRefresh.ImageOptions.ImageKey")
        Me.btnRefresh.Name = "btnRefresh"
        '
        'gridPayments
        '
        resources.ApplyResources(Me.gridPayments, "gridPayments")
        Me.gridPayments.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridPayments.EmbeddedNavigator.AccessibleDescription")
        Me.gridPayments.EmbeddedNavigator.AccessibleName = resources.GetString("gridPayments.EmbeddedNavigator.AccessibleName")
        Me.gridPayments.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridPayments.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridPayments.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridPayments.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridPayments.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridPayments.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridPayments.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridPayments.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridPayments.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridPayments.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridPayments.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridPayments.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridPayments.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridPayments.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridPayments.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridPayments.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridPayments.EmbeddedNavigator.ToolTip = resources.GetString("gridPayments.EmbeddedNavigator.ToolTip")
        Me.gridPayments.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridPayments.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridPayments.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridPayments.EmbeddedNavigator.ToolTipTitle")
        Me.gridPayments.MainView = Me.viewPayments
        Me.gridPayments.Name = "gridPayments"
        Me.gridPayments.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewPayments})
        '
        'viewPayments
        '
        resources.ApplyResources(Me.viewPayments, "viewPayments")
        Me.viewPayments.Appearance.HeaderPanel.Font = CType(resources.GetObject("viewPayments.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.viewPayments.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewPayments.Appearance.Row.Font = CType(resources.GetObject("viewPayments.Appearance.Row.Font"), System.Drawing.Font)
        Me.viewPayments.Appearance.Row.Options.UseFont = True
        Me.viewPayments.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colPaymentID, Me.colPersonName, Me.colPaymentDate, Me.colPaymentType, Me.colAmount, Me.colDescription})
        Me.viewPayments.GridControl = Me.gridPayments
        Me.viewPayments.Name = "viewPayments"
        Me.viewPayments.OptionsBehavior.Editable = False
        Me.viewPayments.OptionsView.ShowFooter = True
        '
        'colPaymentID
        '
        resources.ApplyResources(Me.colPaymentID, "colPaymentID")
        Me.colPaymentID.FieldName = "PaymentID"
        Me.colPaymentID.ImageOptions.ImageKey = resources.GetString("colPaymentID.ImageOptions.ImageKey")
        Me.colPaymentID.Name = "colPaymentID"
        '
        'colPersonName
        '
        resources.ApplyResources(Me.colPersonName, "colPersonName")
        Me.colPersonName.FieldName = "PersonName"
        Me.colPersonName.ImageOptions.ImageKey = resources.GetString("colPersonName.ImageOptions.ImageKey")
        Me.colPersonName.Name = "colPersonName"
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
        'colAmount
        '
        resources.ApplyResources(Me.colAmount, "colAmount")
        Me.colAmount.FieldName = "Amount"
        Me.colAmount.ImageOptions.ImageKey = resources.GetString("colAmount.ImageOptions.ImageKey")
        Me.colAmount.Name = "colAmount"
        Me.colAmount.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(CType(resources.GetObject("colAmount.Summary"), DevExpress.Data.SummaryItemType), resources.GetString("colAmount.Summary1"), resources.GetString("colAmount.Summary2"))})
        '
        'colDescription
        '
        resources.ApplyResources(Me.colDescription, "colDescription")
        Me.colDescription.FieldName = "Description"
        Me.colDescription.ImageOptions.ImageKey = resources.GetString("colDescription.ImageOptions.ImageKey")
        Me.colDescription.Name = "colDescription"
        '
        'panelSummary
        '
        resources.ApplyResources(Me.panelSummary, "panelSummary")
        Me.panelSummary.Controls.Add(Me.lblTotalCaption)
        Me.panelSummary.Controls.Add(Me.lblTotal)
        Me.panelSummary.Name = "panelSummary"
        '
        'lblTotalCaption
        '
        resources.ApplyResources(Me.lblTotalCaption, "lblTotalCaption")
        Me.lblTotalCaption.Appearance.Font = CType(resources.GetObject("lblTotalCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblTotalCaption.Appearance.Options.UseFont = True
        Me.lblTotalCaption.Name = "lblTotalCaption"
        '
        'lblTotal
        '
        resources.ApplyResources(Me.lblTotal, "lblTotal")
        Me.lblTotal.Appearance.Font = CType(resources.GetObject("lblTotal.Appearance.Font"), System.Drawing.Font)
        Me.lblTotal.Appearance.Options.UseFont = True
        Me.lblTotal.Name = "lblTotal"
        '
        'StaffAccountStatement
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gridPayments)
        Me.Controls.Add(Me.panelSummary)
        Me.Controls.Add(Me.panelFilter)
        Me.Name = "StaffAccountStatement"
        CType(Me.panelFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelFilter.ResumeLayout(False)
        Me.panelFilter.PerformLayout()
        CType(Me.cboPersonType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPerson.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dteDateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dteDateFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dteDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dteDateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridPayments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewPayments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelSummary.ResumeLayout(False)
        Me.panelSummary.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelFilter As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblPersonType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPersonType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblPerson As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPerson As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblDateFrom As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dteDateFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblDateTo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dteDateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gridPayments As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewPayments As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colPaymentID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPersonName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPaymentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPaymentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents panelSummary As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblTotalCaption As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotal As DevExpress.XtraEditors.LabelControl
End Class
