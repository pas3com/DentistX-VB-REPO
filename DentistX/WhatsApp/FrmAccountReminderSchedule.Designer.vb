<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAccountReminderSchedule
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    Friend WithEvents PanelTop As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LblPatient As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblInterval As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SpnInterval As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents BtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnRemove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridSchedule As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewSchedule As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LblHelp As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelWhatsApp As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LblWhatsAppPrompt As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtWhatsApp As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BtnSaveWhatsApp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnCancelWhatsApp As DevExpress.XtraEditors.SimpleButton

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAccountReminderSchedule))
        Me.PanelTop = New DevExpress.XtraEditors.PanelControl()
        Me.CboPatient = New DentistX.PatientCombo()
        Me.LblPatient = New DevExpress.XtraEditors.LabelControl()
        Me.LblInterval = New DevExpress.XtraEditors.LabelControl()
        Me.SpnInterval = New DevExpress.XtraEditors.SpinEdit()
        Me.BtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnRemove = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelWhatsApp = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.RadioLang = New DevExpress.XtraEditors.RadioGroup()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.LblWhatsAppPrompt = New DevExpress.XtraEditors.LabelControl()
        Me.TxtWhatsApp = New DevExpress.XtraEditors.TextEdit()
        Me.BtnSaveWhatsApp = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnCancelWhatsApp = New DevExpress.XtraEditors.SimpleButton()
        Me.LblHelp = New DevExpress.XtraEditors.LabelControl()
        Me.GridSchedule = New DevExpress.XtraGrid.GridControl()
        Me.ViewSchedule = New DevExpress.XtraGrid.Views.Grid.GridView()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.PanelTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTop.SuspendLayout()
        CType(Me.SpnInterval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelWhatsApp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelWhatsApp.SuspendLayout()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtWhatsApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        resources.ApplyResources(Label1, "Label1")
        Label1.Name = "Label1"
        '
        'PanelTop
        '
        resources.ApplyResources(Me.PanelTop, "PanelTop")
        Me.PanelTop.Controls.Add(Me.CboPatient)
        Me.PanelTop.Controls.Add(Me.LblPatient)
        Me.PanelTop.Controls.Add(Me.LblInterval)
        Me.PanelTop.Controls.Add(Me.SpnInterval)
        Me.PanelTop.Controls.Add(Me.BtnAdd)
        Me.PanelTop.Controls.Add(Me.BtnRemove)
        Me.PanelTop.Controls.Add(Me.BtnRefresh)
        Me.PanelTop.Controls.Add(Me.PanelWhatsApp)
        Me.PanelTop.Controls.Add(Me.LblHelp)
        Me.PanelTop.Name = "PanelTop"
        '
        'CboPatient
        '
        resources.ApplyResources(Me.CboPatient, "CboPatient")
        Me.CboPatient.BtnAddVisible = True
        Me.CboPatient.BtnSearchVisible = True
        Me.CboPatient.Filter = ""
        Me.CboPatient.Name = "CboPatient"
        '
        'LblPatient
        '
        resources.ApplyResources(Me.LblPatient, "LblPatient")
        Me.LblPatient.Appearance.Font = CType(resources.GetObject("LblPatient.Appearance.Font"), System.Drawing.Font)
        Me.LblPatient.Appearance.Options.UseFont = True
        Me.LblPatient.Name = "LblPatient"
        '
        'LblInterval
        '
        resources.ApplyResources(Me.LblInterval, "LblInterval")
        Me.LblInterval.Appearance.Font = CType(resources.GetObject("LblInterval.Appearance.Font"), System.Drawing.Font)
        Me.LblInterval.Appearance.Options.UseFont = True
        Me.LblInterval.Name = "LblInterval"
        '
        'SpnInterval
        '
        resources.ApplyResources(Me.SpnInterval, "SpnInterval")
        Me.SpnInterval.Name = "SpnInterval"
        Me.SpnInterval.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("SpnInterval.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.SpnInterval.Properties.MaxValue = New Decimal(New Integer() {365, 0, 0, 0})
        Me.SpnInterval.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'BtnAdd
        '
        resources.ApplyResources(Me.BtnAdd, "BtnAdd")
        Me.BtnAdd.Appearance.Font = CType(resources.GetObject("BtnAdd.Appearance.Font"), System.Drawing.Font)
        Me.BtnAdd.Appearance.Options.UseFont = True
        Me.BtnAdd.ImageOptions.ImageKey = resources.GetString("BtnAdd.ImageOptions.ImageKey")
        Me.BtnAdd.Name = "BtnAdd"
        '
        'BtnRemove
        '
        resources.ApplyResources(Me.BtnRemove, "BtnRemove")
        Me.BtnRemove.Appearance.Font = CType(resources.GetObject("BtnRemove.Appearance.Font"), System.Drawing.Font)
        Me.BtnRemove.Appearance.Options.UseFont = True
        Me.BtnRemove.ImageOptions.ImageKey = resources.GetString("BtnRemove.ImageOptions.ImageKey")
        Me.BtnRemove.Name = "BtnRemove"
        '
        'BtnRefresh
        '
        resources.ApplyResources(Me.BtnRefresh, "BtnRefresh")
        Me.BtnRefresh.Appearance.Font = CType(resources.GetObject("BtnRefresh.Appearance.Font"), System.Drawing.Font)
        Me.BtnRefresh.Appearance.Options.UseFont = True
        Me.BtnRefresh.ImageOptions.ImageKey = resources.GetString("BtnRefresh.ImageOptions.ImageKey")
        Me.BtnRefresh.Name = "BtnRefresh"
        '
        'PanelWhatsApp
        '
        resources.ApplyResources(Me.PanelWhatsApp, "PanelWhatsApp")
        Me.PanelWhatsApp.Appearance.BackColor = System.Drawing.Color.LightYellow
        Me.PanelWhatsApp.Appearance.Options.UseBackColor = True
        Me.PanelWhatsApp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelWhatsApp.Controls.Add(Me.LabelControl9)
        Me.PanelWhatsApp.Controls.Add(Me.RadioLang)
        Me.PanelWhatsApp.Controls.Add(Me.lblWhats)
        Me.PanelWhatsApp.Controls.Add(Me.cboPrefix)
        Me.PanelWhatsApp.Controls.Add(Label1)
        Me.PanelWhatsApp.Controls.Add(Me.txtWhats)
        Me.PanelWhatsApp.Controls.Add(Me.LblWhatsAppPrompt)
        Me.PanelWhatsApp.Controls.Add(Me.TxtWhatsApp)
        Me.PanelWhatsApp.Controls.Add(Me.BtnSaveWhatsApp)
        Me.PanelWhatsApp.Controls.Add(Me.BtnCancelWhatsApp)
        Me.PanelWhatsApp.Name = "PanelWhatsApp"
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
        'LblWhatsAppPrompt
        '
        resources.ApplyResources(Me.LblWhatsAppPrompt, "LblWhatsAppPrompt")
        Me.LblWhatsAppPrompt.Appearance.Font = CType(resources.GetObject("LblWhatsAppPrompt.Appearance.Font"), System.Drawing.Font)
        Me.LblWhatsAppPrompt.Appearance.Options.UseFont = True
        Me.LblWhatsAppPrompt.Name = "LblWhatsAppPrompt"
        '
        'TxtWhatsApp
        '
        resources.ApplyResources(Me.TxtWhatsApp, "TxtWhatsApp")
        Me.TxtWhatsApp.Name = "TxtWhatsApp"
        Me.TxtWhatsApp.Properties.Appearance.Font = CType(resources.GetObject("TxtWhatsApp.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtWhatsApp.Properties.Appearance.Options.UseFont = True
        Me.TxtWhatsApp.Properties.MaxLength = 15
        Me.TxtWhatsApp.Properties.NullText = resources.GetString("TxtWhatsApp.Properties.NullText")
        '
        'BtnSaveWhatsApp
        '
        resources.ApplyResources(Me.BtnSaveWhatsApp, "BtnSaveWhatsApp")
        Me.BtnSaveWhatsApp.Appearance.Font = CType(resources.GetObject("BtnSaveWhatsApp.Appearance.Font"), System.Drawing.Font)
        Me.BtnSaveWhatsApp.Appearance.Options.UseFont = True
        Me.BtnSaveWhatsApp.ImageOptions.ImageKey = resources.GetString("BtnSaveWhatsApp.ImageOptions.ImageKey")
        Me.BtnSaveWhatsApp.Name = "BtnSaveWhatsApp"
        '
        'BtnCancelWhatsApp
        '
        resources.ApplyResources(Me.BtnCancelWhatsApp, "BtnCancelWhatsApp")
        Me.BtnCancelWhatsApp.Appearance.Font = CType(resources.GetObject("BtnCancelWhatsApp.Appearance.Font"), System.Drawing.Font)
        Me.BtnCancelWhatsApp.Appearance.Options.UseFont = True
        Me.BtnCancelWhatsApp.ImageOptions.ImageKey = resources.GetString("BtnCancelWhatsApp.ImageOptions.ImageKey")
        Me.BtnCancelWhatsApp.Name = "BtnCancelWhatsApp"
        '
        'LblHelp
        '
        resources.ApplyResources(Me.LblHelp, "LblHelp")
        Me.LblHelp.Appearance.Font = CType(resources.GetObject("LblHelp.Appearance.Font"), System.Drawing.Font)
        Me.LblHelp.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.LblHelp.Appearance.Options.UseFont = True
        Me.LblHelp.Appearance.Options.UseForeColor = True
        Me.LblHelp.Name = "LblHelp"
        '
        'GridSchedule
        '
        resources.ApplyResources(Me.GridSchedule, "GridSchedule")
        Me.GridSchedule.EmbeddedNavigator.AccessibleDescription = resources.GetString("GridSchedule.EmbeddedNavigator.AccessibleDescription")
        Me.GridSchedule.EmbeddedNavigator.AccessibleName = resources.GetString("GridSchedule.EmbeddedNavigator.AccessibleName")
        Me.GridSchedule.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridSchedule.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridSchedule.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridSchedule.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridSchedule.EmbeddedNavigator.AutoSize = CType(resources.GetObject("GridSchedule.EmbeddedNavigator.AutoSize"), Boolean)
        Me.GridSchedule.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("GridSchedule.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.GridSchedule.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridSchedule.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridSchedule.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridSchedule.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridSchedule.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("GridSchedule.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.GridSchedule.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridSchedule.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridSchedule.EmbeddedNavigator.ToolTip = resources.GetString("GridSchedule.EmbeddedNavigator.ToolTip")
        Me.GridSchedule.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridSchedule.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridSchedule.EmbeddedNavigator.ToolTipTitle = resources.GetString("GridSchedule.EmbeddedNavigator.ToolTipTitle")
        Me.GridSchedule.MainView = Me.ViewSchedule
        Me.GridSchedule.Name = "GridSchedule"
        Me.GridSchedule.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewSchedule})
        '
        'ViewSchedule
        '
        resources.ApplyResources(Me.ViewSchedule, "ViewSchedule")
        Me.ViewSchedule.Appearance.HeaderPanel.Font = CType(resources.GetObject("ViewSchedule.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.ViewSchedule.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewSchedule.Appearance.Row.Font = CType(resources.GetObject("ViewSchedule.Appearance.Row.Font"), System.Drawing.Font)
        Me.ViewSchedule.Appearance.Row.Options.UseFont = True
        Me.ViewSchedule.GridControl = Me.GridSchedule
        Me.ViewSchedule.Name = "ViewSchedule"
        '
        'FrmAccountReminderSchedule
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridSchedule)
        Me.Controls.Add(Me.PanelTop)
        Me.Name = "FrmAccountReminderSchedule"
        CType(Me.PanelTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTop.ResumeLayout(False)
        Me.PanelTop.PerformLayout()
        CType(Me.SpnInterval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelWhatsApp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelWhatsApp.ResumeLayout(False)
        Me.PanelWhatsApp.PerformLayout()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtWhatsApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RadioLang As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CboPatient As PatientCombo
End Class
