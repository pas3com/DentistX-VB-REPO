<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPatientDebts
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPatientDebts))
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSex = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAge = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPhone = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAddress = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colHealth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTreat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colImplant = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMobile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOrtho = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStruc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBirthY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTotalTreats = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTotalPays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBalance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelFilters = New System.Windows.Forms.Panel()
        Me.btnClearFilters = New System.Windows.Forms.Button()
        Me.btnApplyFilters = New System.Windows.Forms.Button()
        Me.txtBalanceMax = New System.Windows.Forms.TextBox()
        Me.lblBalanceTo = New System.Windows.Forms.Label()
        Me.txtBalanceMin = New System.Windows.Forms.TextBox()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.txtPaysMax = New System.Windows.Forms.TextBox()
        Me.lblPaysTo = New System.Windows.Forms.Label()
        Me.txtPaysMin = New System.Windows.Forms.TextBox()
        Me.lblPays = New System.Windows.Forms.Label()
        Me.txtTreatsMax = New System.Windows.Forms.TextBox()
        Me.lblTreatsTo = New System.Windows.Forms.Label()
        Me.txtTreatsMin = New System.Windows.Forms.TextBox()
        Me.lblTreats = New System.Windows.Forms.Label()
        Me.txtFilterName = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.BS = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFilters.SuspendLayout()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'SplitContainerControl1.Panel2
        '
        resources.ApplyResources(Me.SplitContainerControl1.Panel2, "SplitContainerControl1.Panel2")
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.DGV)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.PanelFilters)
        Me.SplitContainerControl1.SplitterPosition = 214
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colPatientID, Me.colPatientName, Me.colSex, Me.colAge, Me.colPhone, Me.colAddress, Me.colHealth, Me.colTreat, Me.colImplant, Me.colMobile, Me.colOrtho, Me.colStruc, Me.colNotes, Me.colBirthY, Me.colTotalTreats, Me.colTotalPays, Me.colBalance})
        Me.dgView.DetailHeight = 377
        Me.dgView.GridControl = Me.DGV
        Me.dgView.Name = "dgView"
        Me.dgView.OptionsBehavior.Editable = False
        Me.dgView.OptionsBehavior.ReadOnly = True
        Me.dgView.OptionsDetail.EnableMasterViewMode = False
        Me.dgView.OptionsFind.AlwaysVisible = True
        Me.dgView.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always
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
        'colSex
        '
        resources.ApplyResources(Me.colSex, "colSex")
        Me.colSex.FieldName = "Sex"
        Me.colSex.ImageOptions.ImageKey = resources.GetString("colSex.ImageOptions.ImageKey")
        Me.colSex.Name = "colSex"
        '
        'colAge
        '
        resources.ApplyResources(Me.colAge, "colAge")
        Me.colAge.FieldName = "Age"
        Me.colAge.ImageOptions.ImageKey = resources.GetString("colAge.ImageOptions.ImageKey")
        Me.colAge.Name = "colAge"
        '
        'colPhone
        '
        resources.ApplyResources(Me.colPhone, "colPhone")
        Me.colPhone.FieldName = "Phone"
        Me.colPhone.ImageOptions.ImageKey = resources.GetString("colPhone.ImageOptions.ImageKey")
        Me.colPhone.Name = "colPhone"
        '
        'colAddress
        '
        resources.ApplyResources(Me.colAddress, "colAddress")
        Me.colAddress.FieldName = "Address"
        Me.colAddress.ImageOptions.ImageKey = resources.GetString("colAddress.ImageOptions.ImageKey")
        Me.colAddress.Name = "colAddress"
        '
        'colHealth
        '
        resources.ApplyResources(Me.colHealth, "colHealth")
        Me.colHealth.FieldName = "Health"
        Me.colHealth.ImageOptions.ImageKey = resources.GetString("colHealth.ImageOptions.ImageKey")
        Me.colHealth.Name = "colHealth"
        '
        'colTreat
        '
        resources.ApplyResources(Me.colTreat, "colTreat")
        Me.colTreat.FieldName = "Treat"
        Me.colTreat.ImageOptions.ImageKey = resources.GetString("colTreat.ImageOptions.ImageKey")
        Me.colTreat.Name = "colTreat"
        '
        'colImplant
        '
        resources.ApplyResources(Me.colImplant, "colImplant")
        Me.colImplant.FieldName = "Implant"
        Me.colImplant.ImageOptions.ImageKey = resources.GetString("colImplant.ImageOptions.ImageKey")
        Me.colImplant.Name = "colImplant"
        '
        'colMobile
        '
        resources.ApplyResources(Me.colMobile, "colMobile")
        Me.colMobile.FieldName = "Mobile"
        Me.colMobile.ImageOptions.ImageKey = resources.GetString("colMobile.ImageOptions.ImageKey")
        Me.colMobile.Name = "colMobile"
        '
        'colOrtho
        '
        resources.ApplyResources(Me.colOrtho, "colOrtho")
        Me.colOrtho.FieldName = "Ortho"
        Me.colOrtho.ImageOptions.ImageKey = resources.GetString("colOrtho.ImageOptions.ImageKey")
        Me.colOrtho.Name = "colOrtho"
        '
        'colStruc
        '
        resources.ApplyResources(Me.colStruc, "colStruc")
        Me.colStruc.FieldName = "Struc"
        Me.colStruc.ImageOptions.ImageKey = resources.GetString("colStruc.ImageOptions.ImageKey")
        Me.colStruc.Name = "colStruc"
        '
        'colNotes
        '
        resources.ApplyResources(Me.colNotes, "colNotes")
        Me.colNotes.FieldName = "Notes"
        Me.colNotes.ImageOptions.ImageKey = resources.GetString("colNotes.ImageOptions.ImageKey")
        Me.colNotes.Name = "colNotes"
        '
        'colBirthY
        '
        resources.ApplyResources(Me.colBirthY, "colBirthY")
        Me.colBirthY.FieldName = "BirthY"
        Me.colBirthY.ImageOptions.ImageKey = resources.GetString("colBirthY.ImageOptions.ImageKey")
        Me.colBirthY.Name = "colBirthY"
        '
        'colTotalTreats
        '
        resources.ApplyResources(Me.colTotalTreats, "colTotalTreats")
        Me.colTotalTreats.DisplayFormat.FormatString = "N0"
        Me.colTotalTreats.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colTotalTreats.FieldName = "TotalTreats"
        Me.colTotalTreats.ImageOptions.ImageKey = resources.GetString("colTotalTreats.ImageOptions.ImageKey")
        Me.colTotalTreats.Name = "colTotalTreats"
        '
        'colTotalPays
        '
        resources.ApplyResources(Me.colTotalPays, "colTotalPays")
        Me.colTotalPays.DisplayFormat.FormatString = "N0"
        Me.colTotalPays.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colTotalPays.FieldName = "TotalPays"
        Me.colTotalPays.ImageOptions.ImageKey = resources.GetString("colTotalPays.ImageOptions.ImageKey")
        Me.colTotalPays.Name = "colTotalPays"
        '
        'colBalance
        '
        resources.ApplyResources(Me.colBalance, "colBalance")
        Me.colBalance.DisplayFormat.FormatString = "N0"
        Me.colBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colBalance.FieldName = "Balance"
        Me.colBalance.ImageOptions.ImageKey = resources.GetString("colBalance.ImageOptions.ImageKey")
        Me.colBalance.Name = "colBalance"
        '
        'PanelFilters
        '
        resources.ApplyResources(Me.PanelFilters, "PanelFilters")
        Me.PanelFilters.Controls.Add(Me.btnClearFilters)
        Me.PanelFilters.Controls.Add(Me.btnApplyFilters)
        Me.PanelFilters.Controls.Add(Me.txtBalanceMax)
        Me.PanelFilters.Controls.Add(Me.lblBalanceTo)
        Me.PanelFilters.Controls.Add(Me.txtBalanceMin)
        Me.PanelFilters.Controls.Add(Me.lblBalance)
        Me.PanelFilters.Controls.Add(Me.txtPaysMax)
        Me.PanelFilters.Controls.Add(Me.lblPaysTo)
        Me.PanelFilters.Controls.Add(Me.txtPaysMin)
        Me.PanelFilters.Controls.Add(Me.lblPays)
        Me.PanelFilters.Controls.Add(Me.txtTreatsMax)
        Me.PanelFilters.Controls.Add(Me.lblTreatsTo)
        Me.PanelFilters.Controls.Add(Me.txtTreatsMin)
        Me.PanelFilters.Controls.Add(Me.lblTreats)
        Me.PanelFilters.Controls.Add(Me.txtFilterName)
        Me.PanelFilters.Controls.Add(Me.lblName)
        Me.PanelFilters.Name = "PanelFilters"
        '
        'btnClearFilters
        '
        resources.ApplyResources(Me.btnClearFilters, "btnClearFilters")
        Me.btnClearFilters.Name = "btnClearFilters"
        Me.btnClearFilters.UseVisualStyleBackColor = True
        '
        'btnApplyFilters
        '
        resources.ApplyResources(Me.btnApplyFilters, "btnApplyFilters")
        Me.btnApplyFilters.Name = "btnApplyFilters"
        Me.btnApplyFilters.UseVisualStyleBackColor = True
        '
        'txtBalanceMax
        '
        resources.ApplyResources(Me.txtBalanceMax, "txtBalanceMax")
        Me.txtBalanceMax.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBalanceMax.Name = "txtBalanceMax"
        '
        'lblBalanceTo
        '
        resources.ApplyResources(Me.lblBalanceTo, "lblBalanceTo")
        Me.lblBalanceTo.Name = "lblBalanceTo"
        '
        'txtBalanceMin
        '
        resources.ApplyResources(Me.txtBalanceMin, "txtBalanceMin")
        Me.txtBalanceMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBalanceMin.Name = "txtBalanceMin"
        '
        'lblBalance
        '
        resources.ApplyResources(Me.lblBalance, "lblBalance")
        Me.lblBalance.Name = "lblBalance"
        '
        'txtPaysMax
        '
        resources.ApplyResources(Me.txtPaysMax, "txtPaysMax")
        Me.txtPaysMax.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPaysMax.Name = "txtPaysMax"
        '
        'lblPaysTo
        '
        resources.ApplyResources(Me.lblPaysTo, "lblPaysTo")
        Me.lblPaysTo.Name = "lblPaysTo"
        '
        'txtPaysMin
        '
        resources.ApplyResources(Me.txtPaysMin, "txtPaysMin")
        Me.txtPaysMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPaysMin.Name = "txtPaysMin"
        '
        'lblPays
        '
        resources.ApplyResources(Me.lblPays, "lblPays")
        Me.lblPays.Name = "lblPays"
        '
        'txtTreatsMax
        '
        resources.ApplyResources(Me.txtTreatsMax, "txtTreatsMax")
        Me.txtTreatsMax.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTreatsMax.Name = "txtTreatsMax"
        '
        'lblTreatsTo
        '
        resources.ApplyResources(Me.lblTreatsTo, "lblTreatsTo")
        Me.lblTreatsTo.Name = "lblTreatsTo"
        '
        'txtTreatsMin
        '
        resources.ApplyResources(Me.txtTreatsMin, "txtTreatsMin")
        Me.txtTreatsMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTreatsMin.Name = "txtTreatsMin"
        '
        'lblTreats
        '
        resources.ApplyResources(Me.lblTreats, "lblTreats")
        Me.lblTreats.Name = "lblTreats"
        '
        'txtFilterName
        '
        resources.ApplyResources(Me.txtFilterName, "txtFilterName")
        Me.txtFilterName.Name = "txtFilterName"
        '
        'lblName
        '
        resources.ApplyResources(Me.lblName, "lblName")
        Me.lblName.Name = "lblName"
        '
        'FrmPatientDebts
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Name = "FrmPatientDebts"
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFilters.ResumeLayout(False)
        Me.PanelFilters.PerformLayout()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents BS As BindingSource
    Private WithEvents DGV As DevExpress.XtraGrid.GridControl
    Private WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSex As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPhone As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAddress As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHealth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTreat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colImplant As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMobile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrtho As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStruc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBirthY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotalTreats As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotalPays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBalance As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelFilters As Panel
    Friend WithEvents btnClearFilters As Button
    Friend WithEvents btnApplyFilters As Button
    Friend WithEvents txtBalanceMax As TextBox
    Friend WithEvents lblBalanceTo As Label
    Friend WithEvents txtBalanceMin As TextBox
    Friend WithEvents lblBalance As Label
    Friend WithEvents txtPaysMax As TextBox
    Friend WithEvents lblPaysTo As Label
    Friend WithEvents txtPaysMin As TextBox
    Friend WithEvents lblPays As Label
    Friend WithEvents txtTreatsMax As TextBox
    Friend WithEvents lblTreatsTo As Label
    Friend WithEvents txtTreatsMin As TextBox
    Friend WithEvents lblTreats As Label
    Friend WithEvents txtFilterName As TextBox
    Friend WithEvents lblName As Label
End Class
