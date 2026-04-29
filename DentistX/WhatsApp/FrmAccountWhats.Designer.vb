<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAccountWhats
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    Friend WithEvents PanelFilters As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LblDateFrom As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblDateTo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblPatient As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblPayType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblTrtAmount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblPayAmount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DateFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents TxtPatient As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CboPayType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents SpnTrtMin As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents SpnTrtMax As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents SpnPayMin As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents SpnPayMax As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents ChkBalanceOnly As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents BtnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSendAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSchedule As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridAccounts As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewAccounts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelFooter As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LblSummary As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblFilterHelp As DevExpress.XtraEditors.LabelControl

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAccountWhats))
        Me.PanelFilters = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.RadioLang = New DevExpress.XtraEditors.RadioGroup()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.LblDateFrom = New DevExpress.XtraEditors.LabelControl()
        Me.LblDateTo = New DevExpress.XtraEditors.LabelControl()
        Me.LblPatient = New DevExpress.XtraEditors.LabelControl()
        Me.LblPayType = New DevExpress.XtraEditors.LabelControl()
        Me.LblTrtAmount = New DevExpress.XtraEditors.LabelControl()
        Me.LblPayAmount = New DevExpress.XtraEditors.LabelControl()
        Me.DateFrom = New DevExpress.XtraEditors.DateEdit()
        Me.DateTo = New DevExpress.XtraEditors.DateEdit()
        Me.TxtPatient = New DevExpress.XtraEditors.TextEdit()
        Me.CboPayType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.SpnTrtMin = New DevExpress.XtraEditors.SpinEdit()
        Me.SpnTrtMax = New DevExpress.XtraEditors.SpinEdit()
        Me.SpnPayMin = New DevExpress.XtraEditors.SpinEdit()
        Me.SpnPayMax = New DevExpress.XtraEditors.SpinEdit()
        Me.ChkBalanceOnly = New DevExpress.XtraEditors.CheckEdit()
        Me.BtnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSendAll = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSchedule = New DevExpress.XtraEditors.SimpleButton()
        Me.LblFilterHelp = New DevExpress.XtraEditors.LabelControl()
        Me.GridAccounts = New DevExpress.XtraGrid.GridControl()
        Me.ViewAccounts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelFooter = New DevExpress.XtraEditors.PanelControl()
        Me.LblSummary = New DevExpress.XtraEditors.LabelControl()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.PanelFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFilters.SuspendLayout()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboPayType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpnTrtMin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpnTrtMax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpnPayMin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpnPayMax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkBalanceOnly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelFooter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        resources.ApplyResources(Label1, "Label1")
        Label1.Name = "Label1"
        '
        'PanelFilters
        '
        resources.ApplyResources(Me.PanelFilters, "PanelFilters")
        Me.PanelFilters.Controls.Add(Me.LabelControl9)
        Me.PanelFilters.Controls.Add(Me.RadioLang)
        Me.PanelFilters.Controls.Add(Me.lblWhats)
        Me.PanelFilters.Controls.Add(Me.cboPrefix)
        Me.PanelFilters.Controls.Add(Label1)
        Me.PanelFilters.Controls.Add(Me.txtWhats)
        Me.PanelFilters.Controls.Add(Me.LblDateFrom)
        Me.PanelFilters.Controls.Add(Me.LblDateTo)
        Me.PanelFilters.Controls.Add(Me.LblPatient)
        Me.PanelFilters.Controls.Add(Me.LblPayType)
        Me.PanelFilters.Controls.Add(Me.LblTrtAmount)
        Me.PanelFilters.Controls.Add(Me.LblPayAmount)
        Me.PanelFilters.Controls.Add(Me.DateFrom)
        Me.PanelFilters.Controls.Add(Me.DateTo)
        Me.PanelFilters.Controls.Add(Me.TxtPatient)
        Me.PanelFilters.Controls.Add(Me.CboPayType)
        Me.PanelFilters.Controls.Add(Me.SpnTrtMin)
        Me.PanelFilters.Controls.Add(Me.SpnTrtMax)
        Me.PanelFilters.Controls.Add(Me.SpnPayMin)
        Me.PanelFilters.Controls.Add(Me.SpnPayMax)
        Me.PanelFilters.Controls.Add(Me.ChkBalanceOnly)
        Me.PanelFilters.Controls.Add(Me.BtnRefresh)
        Me.PanelFilters.Controls.Add(Me.BtnSendAll)
        Me.PanelFilters.Controls.Add(Me.BtnSchedule)
        Me.PanelFilters.Controls.Add(Me.LblFilterHelp)
        Me.PanelFilters.Name = "PanelFilters"
        '
        'LabelControl9
        '
        resources.ApplyResources(Me.LabelControl9, "LabelControl9")
        Me.LabelControl9.Appearance.Font = CType(resources.GetObject("LabelControl9.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Name = "LabelControl9"
        '
        'RadioLang
        '
        resources.ApplyResources(Me.RadioLang, "RadioLang")
        Me.RadioLang.EnterMoveNextControl = True
        Me.RadioLang.Name = "RadioLang"
        Me.RadioLang.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioLang.Properties.Appearance.Font = CType(resources.GetObject("RadioLang.Properties.Appearance.Font"), System.Drawing.Font)
        Me.RadioLang.Properties.Appearance.Options.UseBackColor = True
        Me.RadioLang.Properties.Appearance.Options.UseFont = True
        Me.RadioLang.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioLang.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLang.Properties.Items"), Object), resources.GetString("RadioLang.Properties.Items1"), CType(resources.GetObject("RadioLang.Properties.Items2"), Boolean), CType(resources.GetObject("RadioLang.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLang.Properties.Items4"), Object), resources.GetString("RadioLang.Properties.Items5"))})
        '
        'lblWhats
        '
        resources.ApplyResources(Me.lblWhats, "lblWhats")
        Me.lblWhats.Appearance.Font = CType(resources.GetObject("lblWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblWhats.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblWhats.Appearance.Options.UseFont = True
        Me.lblWhats.Appearance.Options.UseForeColor = True
        Me.lblWhats.Name = "lblWhats"
        '
        'cboPrefix
        '
        resources.ApplyResources(Me.cboPrefix, "cboPrefix")
        Me.cboPrefix.EnterMoveNextControl = True
        Me.cboPrefix.Name = "cboPrefix"
        Me.cboPrefix.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPrefix.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'txtWhats
        '
        resources.ApplyResources(Me.txtWhats, "txtWhats")
        Me.txtWhats.EnterMoveNextControl = True
        Me.txtWhats.Name = "txtWhats"
        Me.txtWhats.Properties.Appearance.Font = CType(resources.GetObject("txtWhats.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtWhats.Properties.Appearance.Options.UseFont = True
        Me.txtWhats.Properties.MaxLength = 10
        '
        'LblDateFrom
        '
        resources.ApplyResources(Me.LblDateFrom, "LblDateFrom")
        Me.LblDateFrom.Appearance.Font = CType(resources.GetObject("LblDateFrom.Appearance.Font"), System.Drawing.Font)
        Me.LblDateFrom.Appearance.Options.UseFont = True
        Me.LblDateFrom.Name = "LblDateFrom"
        '
        'LblDateTo
        '
        resources.ApplyResources(Me.LblDateTo, "LblDateTo")
        Me.LblDateTo.Appearance.Font = CType(resources.GetObject("LblDateTo.Appearance.Font"), System.Drawing.Font)
        Me.LblDateTo.Appearance.Options.UseFont = True
        Me.LblDateTo.Name = "LblDateTo"
        '
        'LblPatient
        '
        resources.ApplyResources(Me.LblPatient, "LblPatient")
        Me.LblPatient.Appearance.Font = CType(resources.GetObject("LblPatient.Appearance.Font"), System.Drawing.Font)
        Me.LblPatient.Appearance.Options.UseFont = True
        Me.LblPatient.Name = "LblPatient"
        '
        'LblPayType
        '
        resources.ApplyResources(Me.LblPayType, "LblPayType")
        Me.LblPayType.Appearance.Font = CType(resources.GetObject("LblPayType.Appearance.Font"), System.Drawing.Font)
        Me.LblPayType.Appearance.Options.UseFont = True
        Me.LblPayType.Name = "LblPayType"
        '
        'LblTrtAmount
        '
        resources.ApplyResources(Me.LblTrtAmount, "LblTrtAmount")
        Me.LblTrtAmount.Appearance.Font = CType(resources.GetObject("LblTrtAmount.Appearance.Font"), System.Drawing.Font)
        Me.LblTrtAmount.Appearance.Options.UseFont = True
        Me.LblTrtAmount.Name = "LblTrtAmount"
        '
        'LblPayAmount
        '
        resources.ApplyResources(Me.LblPayAmount, "LblPayAmount")
        Me.LblPayAmount.Appearance.Font = CType(resources.GetObject("LblPayAmount.Appearance.Font"), System.Drawing.Font)
        Me.LblPayAmount.Appearance.Options.UseFont = True
        Me.LblPayAmount.Name = "LblPayAmount"
        '
        'DateFrom
        '
        resources.ApplyResources(Me.DateFrom, "DateFrom")
        Me.DateFrom.EnterMoveNextControl = True
        Me.DateFrom.Name = "DateFrom"
        Me.DateFrom.Properties.Appearance.Font = CType(resources.GetObject("DateFrom.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DateFrom.Properties.Appearance.Options.UseFont = True
        Me.DateFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("DateFrom.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.DateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("DateFrom.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.DateFrom.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.DateFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DateFrom.Properties.MaskSettings.Set("mask", "dd/MM/yyyy")
        '
        'DateTo
        '
        resources.ApplyResources(Me.DateTo, "DateTo")
        Me.DateTo.EnterMoveNextControl = True
        Me.DateTo.Name = "DateTo"
        Me.DateTo.Properties.Appearance.Font = CType(resources.GetObject("DateTo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DateTo.Properties.Appearance.Options.UseFont = True
        Me.DateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("DateTo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.DateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("DateTo.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.DateTo.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.DateTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DateTo.Properties.MaskSettings.Set("mask", "dd/MM/yyyy")
        '
        'TxtPatient
        '
        resources.ApplyResources(Me.TxtPatient, "TxtPatient")
        Me.TxtPatient.EnterMoveNextControl = True
        Me.TxtPatient.Name = "TxtPatient"
        Me.TxtPatient.Properties.Appearance.Font = CType(resources.GetObject("TxtPatient.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPatient.Properties.Appearance.Options.UseFont = True
        Me.TxtPatient.Properties.NullText = resources.GetString("TxtPatient.Properties.NullText")
        '
        'CboPayType
        '
        resources.ApplyResources(Me.CboPayType, "CboPayType")
        Me.CboPayType.EnterMoveNextControl = True
        Me.CboPayType.Name = "CboPayType"
        Me.CboPayType.Properties.Appearance.Font = CType(resources.GetObject("CboPayType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboPayType.Properties.Appearance.Options.UseFont = True
        Me.CboPayType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboPayType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'SpnTrtMin
        '
        resources.ApplyResources(Me.SpnTrtMin, "SpnTrtMin")
        Me.SpnTrtMin.EnterMoveNextControl = True
        Me.SpnTrtMin.Name = "SpnTrtMin"
        Me.SpnTrtMin.Properties.Appearance.Font = CType(resources.GetObject("SpnTrtMin.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SpnTrtMin.Properties.Appearance.Options.UseFont = True
        Me.SpnTrtMin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("SpnTrtMin.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.SpnTrtMin.Properties.DisplayFormat.FormatString = "N2"
        Me.SpnTrtMin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SpnTrtMin.Properties.EditFormat.FormatString = "N2"
        Me.SpnTrtMin.Properties.NullText = resources.GetString("SpnTrtMin.Properties.NullText")
        '
        'SpnTrtMax
        '
        resources.ApplyResources(Me.SpnTrtMax, "SpnTrtMax")
        Me.SpnTrtMax.EnterMoveNextControl = True
        Me.SpnTrtMax.Name = "SpnTrtMax"
        Me.SpnTrtMax.Properties.Appearance.Font = CType(resources.GetObject("SpnTrtMax.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SpnTrtMax.Properties.Appearance.Options.UseFont = True
        Me.SpnTrtMax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("SpnTrtMax.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.SpnTrtMax.Properties.DisplayFormat.FormatString = "N2"
        Me.SpnTrtMax.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SpnTrtMax.Properties.EditFormat.FormatString = "N2"
        Me.SpnTrtMax.Properties.NullText = resources.GetString("SpnTrtMax.Properties.NullText")
        '
        'SpnPayMin
        '
        resources.ApplyResources(Me.SpnPayMin, "SpnPayMin")
        Me.SpnPayMin.EnterMoveNextControl = True
        Me.SpnPayMin.Name = "SpnPayMin"
        Me.SpnPayMin.Properties.Appearance.Font = CType(resources.GetObject("SpnPayMin.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SpnPayMin.Properties.Appearance.Options.UseFont = True
        Me.SpnPayMin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("SpnPayMin.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.SpnPayMin.Properties.DisplayFormat.FormatString = "N2"
        Me.SpnPayMin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SpnPayMin.Properties.EditFormat.FormatString = "N2"
        Me.SpnPayMin.Properties.NullText = resources.GetString("SpnPayMin.Properties.NullText")
        '
        'SpnPayMax
        '
        resources.ApplyResources(Me.SpnPayMax, "SpnPayMax")
        Me.SpnPayMax.EnterMoveNextControl = True
        Me.SpnPayMax.Name = "SpnPayMax"
        Me.SpnPayMax.Properties.Appearance.Font = CType(resources.GetObject("SpnPayMax.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SpnPayMax.Properties.Appearance.Options.UseFont = True
        Me.SpnPayMax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("SpnPayMax.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.SpnPayMax.Properties.DisplayFormat.FormatString = "N2"
        Me.SpnPayMax.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SpnPayMax.Properties.EditFormat.FormatString = "N2"
        Me.SpnPayMax.Properties.NullText = resources.GetString("SpnPayMax.Properties.NullText")
        '
        'ChkBalanceOnly
        '
        resources.ApplyResources(Me.ChkBalanceOnly, "ChkBalanceOnly")
        Me.ChkBalanceOnly.EnterMoveNextControl = True
        Me.ChkBalanceOnly.Name = "ChkBalanceOnly"
        Me.ChkBalanceOnly.Properties.Appearance.Font = CType(resources.GetObject("ChkBalanceOnly.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ChkBalanceOnly.Properties.Appearance.Options.UseFont = True
        Me.ChkBalanceOnly.Properties.Caption = resources.GetString("ChkBalanceOnly.Properties.Caption")
        Me.ChkBalanceOnly.Properties.DisplayValueChecked = resources.GetString("ChkBalanceOnly.Properties.DisplayValueChecked")
        Me.ChkBalanceOnly.Properties.DisplayValueGrayed = resources.GetString("ChkBalanceOnly.Properties.DisplayValueGrayed")
        Me.ChkBalanceOnly.Properties.DisplayValueUnchecked = resources.GetString("ChkBalanceOnly.Properties.DisplayValueUnchecked")
        Me.ChkBalanceOnly.Properties.GlyphVerticalAlignment = CType(resources.GetObject("ChkBalanceOnly.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'BtnRefresh
        '
        resources.ApplyResources(Me.BtnRefresh, "BtnRefresh")
        Me.BtnRefresh.Appearance.Font = CType(resources.GetObject("BtnRefresh.Appearance.Font"), System.Drawing.Font)
        Me.BtnRefresh.Appearance.Options.UseFont = True
        Me.BtnRefresh.ImageOptions.ImageKey = resources.GetString("BtnRefresh.ImageOptions.ImageKey")
        Me.BtnRefresh.Name = "BtnRefresh"
        '
        'BtnSendAll
        '
        resources.ApplyResources(Me.BtnSendAll, "BtnSendAll")
        Me.BtnSendAll.Appearance.Font = CType(resources.GetObject("BtnSendAll.Appearance.Font"), System.Drawing.Font)
        Me.BtnSendAll.Appearance.Options.UseFont = True
        Me.BtnSendAll.ImageOptions.ImageKey = resources.GetString("BtnSendAll.ImageOptions.ImageKey")
        Me.BtnSendAll.Name = "BtnSendAll"
        '
        'BtnSchedule
        '
        resources.ApplyResources(Me.BtnSchedule, "BtnSchedule")
        Me.BtnSchedule.Appearance.Font = CType(resources.GetObject("BtnSchedule.Appearance.Font"), System.Drawing.Font)
        Me.BtnSchedule.Appearance.Options.UseFont = True
        Me.BtnSchedule.ImageOptions.ImageKey = resources.GetString("BtnSchedule.ImageOptions.ImageKey")
        Me.BtnSchedule.Name = "BtnSchedule"
        '
        'LblFilterHelp
        '
        resources.ApplyResources(Me.LblFilterHelp, "LblFilterHelp")
        Me.LblFilterHelp.Appearance.Font = CType(resources.GetObject("LblFilterHelp.Appearance.Font"), System.Drawing.Font)
        Me.LblFilterHelp.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.LblFilterHelp.Appearance.Options.UseFont = True
        Me.LblFilterHelp.Appearance.Options.UseForeColor = True
        Me.LblFilterHelp.Name = "LblFilterHelp"
        '
        'GridAccounts
        '
        resources.ApplyResources(Me.GridAccounts, "GridAccounts")
        Me.GridAccounts.EmbeddedNavigator.AccessibleDescription = resources.GetString("GridAccounts.EmbeddedNavigator.AccessibleDescription")
        Me.GridAccounts.EmbeddedNavigator.AccessibleName = resources.GetString("GridAccounts.EmbeddedNavigator.AccessibleName")
        Me.GridAccounts.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridAccounts.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridAccounts.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridAccounts.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridAccounts.EmbeddedNavigator.AutoSize = CType(resources.GetObject("GridAccounts.EmbeddedNavigator.AutoSize"), Boolean)
        Me.GridAccounts.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("GridAccounts.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.GridAccounts.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridAccounts.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridAccounts.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridAccounts.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridAccounts.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("GridAccounts.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.GridAccounts.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridAccounts.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridAccounts.EmbeddedNavigator.ToolTip = resources.GetString("GridAccounts.EmbeddedNavigator.ToolTip")
        Me.GridAccounts.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridAccounts.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridAccounts.EmbeddedNavigator.ToolTipTitle = resources.GetString("GridAccounts.EmbeddedNavigator.ToolTipTitle")
        Me.GridAccounts.MainView = Me.ViewAccounts
        Me.GridAccounts.Name = "GridAccounts"
        Me.GridAccounts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewAccounts})
        '
        'ViewAccounts
        '
        resources.ApplyResources(Me.ViewAccounts, "ViewAccounts")
        Me.ViewAccounts.Appearance.HeaderPanel.Font = CType(resources.GetObject("ViewAccounts.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.ViewAccounts.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewAccounts.Appearance.Row.Font = CType(resources.GetObject("ViewAccounts.Appearance.Row.Font"), System.Drawing.Font)
        Me.ViewAccounts.Appearance.Row.Options.UseFont = True
        Me.ViewAccounts.GridControl = Me.GridAccounts
        Me.ViewAccounts.Name = "ViewAccounts"
        Me.ViewAccounts.OptionsView.ShowFooter = True
        '
        'PanelFooter
        '
        resources.ApplyResources(Me.PanelFooter, "PanelFooter")
        Me.PanelFooter.Controls.Add(Me.LblSummary)
        Me.PanelFooter.Name = "PanelFooter"
        '
        'LblSummary
        '
        resources.ApplyResources(Me.LblSummary, "LblSummary")
        Me.LblSummary.Appearance.Font = CType(resources.GetObject("LblSummary.Appearance.Font"), System.Drawing.Font)
        Me.LblSummary.Appearance.Options.UseFont = True
        Me.LblSummary.Appearance.Options.UseTextOptions = True
        Me.LblSummary.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LblSummary.Name = "LblSummary"
        '
        'FrmAccountWhats
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridAccounts)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.PanelFilters)
        Me.Name = "FrmAccountWhats"
        CType(Me.PanelFilters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFilters.ResumeLayout(False)
        Me.PanelFilters.PerformLayout()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboPayType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpnTrtMin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpnTrtMax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpnPayMin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpnPayMax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkBalanceOnly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelFooter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFooter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RadioLang As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
End Class
