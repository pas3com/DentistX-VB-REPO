<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmApptsWhats
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
    Friend WithEvents LblDoctor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DateFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents CboDoctor As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents BtnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridAppts As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewAppts As DevExpress.XtraGrid.Views.Grid.GridView

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmApptsWhats))
        Me.PanelFilters = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.RadioLang = New DevExpress.XtraEditors.RadioGroup()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.BtnOpenWhatsAppLog = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnOpenAutoReminderQueue = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSendAll = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSchedule = New DevExpress.XtraEditors.SimpleButton()
        Me.LblDateFrom = New DevExpress.XtraEditors.LabelControl()
        Me.LblDateTo = New DevExpress.XtraEditors.LabelControl()
        Me.LblDoctor = New DevExpress.XtraEditors.LabelControl()
        Me.DateFrom = New DevExpress.XtraEditors.DateEdit()
        Me.DateTo = New DevExpress.XtraEditors.DateEdit()
        Me.CboDoctor = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BtnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.GridAppts = New DevExpress.XtraGrid.GridControl()
        Me.ViewAppts = New DevExpress.XtraGrid.Views.Grid.GridView()
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
        CType(Me.CboDoctor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridAppts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewAppts, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.PanelFilters.Controls.Add(Me.BtnOpenWhatsAppLog)
        Me.PanelFilters.Controls.Add(Me.BtnOpenAutoReminderQueue)
        Me.PanelFilters.Controls.Add(Me.BtnSendAll)
        Me.PanelFilters.Controls.Add(Me.BtnSchedule)
        Me.PanelFilters.Controls.Add(Me.LblDateFrom)
        Me.PanelFilters.Controls.Add(Me.LblDateTo)
        Me.PanelFilters.Controls.Add(Me.LblDoctor)
        Me.PanelFilters.Controls.Add(Me.DateFrom)
        Me.PanelFilters.Controls.Add(Me.DateTo)
        Me.PanelFilters.Controls.Add(Me.CboDoctor)
        Me.PanelFilters.Controls.Add(Me.BtnRefresh)
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
        Me.cboPrefix.Name = "cboPrefix"
        Me.cboPrefix.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPrefix.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'txtWhats
        '
        resources.ApplyResources(Me.txtWhats, "txtWhats")
        Me.txtWhats.Name = "txtWhats"
        Me.txtWhats.Properties.Appearance.Font = CType(resources.GetObject("txtWhats.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtWhats.Properties.Appearance.Options.UseFont = True
        Me.txtWhats.Properties.MaxLength = 10
        '
        'BtnOpenWhatsAppLog
        '
        resources.ApplyResources(Me.BtnOpenWhatsAppLog, "BtnOpenWhatsAppLog")
        Me.BtnOpenWhatsAppLog.Appearance.Font = CType(resources.GetObject("BtnOpenWhatsAppLog.Appearance.Font"), System.Drawing.Font)
        Me.BtnOpenWhatsAppLog.Appearance.Options.UseFont = True
        Me.BtnOpenWhatsAppLog.ImageOptions.ImageKey = resources.GetString("BtnOpenWhatsAppLog.ImageOptions.ImageKey")
        Me.BtnOpenWhatsAppLog.Name = "BtnOpenWhatsAppLog"
        '
        'BtnOpenAutoReminderQueue
        '
        resources.ApplyResources(Me.BtnOpenAutoReminderQueue, "BtnOpenAutoReminderQueue")
        Me.BtnOpenAutoReminderQueue.Appearance.Font = CType(resources.GetObject("BtnOpenAutoReminderQueue.Appearance.Font"), System.Drawing.Font)
        Me.BtnOpenAutoReminderQueue.Appearance.Options.UseFont = True
        Me.BtnOpenAutoReminderQueue.ImageOptions.ImageKey = resources.GetString("BtnOpenAutoReminderQueue.ImageOptions.ImageKey")
        Me.BtnOpenAutoReminderQueue.Name = "BtnOpenAutoReminderQueue"
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
        'LblDoctor
        '
        resources.ApplyResources(Me.LblDoctor, "LblDoctor")
        Me.LblDoctor.Appearance.Font = CType(resources.GetObject("LblDoctor.Appearance.Font"), System.Drawing.Font)
        Me.LblDoctor.Appearance.Options.UseFont = True
        Me.LblDoctor.Name = "LblDoctor"
        '
        'DateFrom
        '
        resources.ApplyResources(Me.DateFrom, "DateFrom")
        Me.DateFrom.Name = "DateFrom"
        Me.DateFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("DateFrom.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.DateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("DateFrom.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.DateFrom.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.DateFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DateFrom.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.DateFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DateFrom.Properties.MaskSettings.Set("mask", "dd/MM/yyyy")
        '
        'DateTo
        '
        resources.ApplyResources(Me.DateTo, "DateTo")
        Me.DateTo.Name = "DateTo"
        Me.DateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("DateTo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.DateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("DateTo.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.DateTo.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.DateTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DateTo.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.DateTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DateTo.Properties.MaskSettings.Set("mask", "dd/MM/yyyy")
        '
        'CboDoctor
        '
        resources.ApplyResources(Me.CboDoctor, "CboDoctor")
        Me.CboDoctor.Name = "CboDoctor"
        Me.CboDoctor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboDoctor.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'BtnRefresh
        '
        resources.ApplyResources(Me.BtnRefresh, "BtnRefresh")
        Me.BtnRefresh.Appearance.Font = CType(resources.GetObject("BtnRefresh.Appearance.Font"), System.Drawing.Font)
        Me.BtnRefresh.Appearance.Options.UseFont = True
        Me.BtnRefresh.ImageOptions.ImageKey = resources.GetString("BtnRefresh.ImageOptions.ImageKey")
        Me.BtnRefresh.Name = "BtnRefresh"
        '
        'GridAppts
        '
        resources.ApplyResources(Me.GridAppts, "GridAppts")
        Me.GridAppts.EmbeddedNavigator.AccessibleDescription = resources.GetString("GridAppts.EmbeddedNavigator.AccessibleDescription")
        Me.GridAppts.EmbeddedNavigator.AccessibleName = resources.GetString("GridAppts.EmbeddedNavigator.AccessibleName")
        Me.GridAppts.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridAppts.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridAppts.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridAppts.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridAppts.EmbeddedNavigator.AutoSize = CType(resources.GetObject("GridAppts.EmbeddedNavigator.AutoSize"), Boolean)
        Me.GridAppts.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("GridAppts.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.GridAppts.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridAppts.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridAppts.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridAppts.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridAppts.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("GridAppts.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.GridAppts.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridAppts.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridAppts.EmbeddedNavigator.ToolTip = resources.GetString("GridAppts.EmbeddedNavigator.ToolTip")
        Me.GridAppts.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridAppts.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridAppts.EmbeddedNavigator.ToolTipTitle = resources.GetString("GridAppts.EmbeddedNavigator.ToolTipTitle")
        Me.GridAppts.MainView = Me.ViewAppts
        Me.GridAppts.Name = "GridAppts"
        Me.GridAppts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewAppts})
        '
        'ViewAppts
        '
        resources.ApplyResources(Me.ViewAppts, "ViewAppts")
        Me.ViewAppts.Appearance.HeaderPanel.Font = CType(resources.GetObject("ViewAppts.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.ViewAppts.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewAppts.Appearance.Row.Font = CType(resources.GetObject("ViewAppts.Appearance.Row.Font"), System.Drawing.Font)
        Me.ViewAppts.Appearance.Row.Options.UseFont = True
        Me.ViewAppts.GridControl = Me.GridAppts
        Me.ViewAppts.Name = "ViewAppts"
        '
        'FrmApptsWhats
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridAppts)
        Me.Controls.Add(Me.PanelFilters)
        Me.Name = "FrmApptsWhats"
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
        CType(Me.CboDoctor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridAppts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewAppts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnSchedule As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSendAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnOpenAutoReminderQueue As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnOpenWhatsAppLog As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RadioLang As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
End Class
