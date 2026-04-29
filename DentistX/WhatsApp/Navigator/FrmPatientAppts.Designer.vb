<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPatientAppts
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPatientAppts))
        Me.lblSendTo = New DevExpress.XtraEditors.LabelControl()
        Me.BtnSendMessage = New DevExpress.XtraEditors.SimpleButton()
        Me.pnlFilters = New DevExpress.XtraEditors.PanelControl()
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.cboStatus = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblDoctor = New DevExpress.XtraEditors.LabelControl()
        Me.cboDoctor = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblTo = New DevExpress.XtraEditors.LabelControl()
        Me.lblFrom = New DevExpress.XtraEditors.LabelControl()
        Me.dtpTo = New DevExpress.XtraEditors.DateEdit()
        Me.dtpFrom = New DevExpress.XtraEditors.DateEdit()
        Me.btnApplyFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.gcAppts = New DevExpress.XtraGrid.GridControl()
        Me.gvAppts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSend = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.repoSend = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDoctorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.RadioLang = New DevExpress.XtraEditors.RadioGroup()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.pnlFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilters.SuspendLayout()
        CType(Me.cboStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDoctor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcAppts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAppts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repoSend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        resources.ApplyResources(Label1, "Label1")
        Label1.Name = "Label1"
        '
        'lblSendTo
        '
        resources.ApplyResources(Me.lblSendTo, "lblSendTo")
        Me.lblSendTo.Appearance.Font = CType(resources.GetObject("lblSendTo.Appearance.Font"), System.Drawing.Font)
        Me.lblSendTo.Appearance.Options.UseFont = True
        Me.lblSendTo.Name = "lblSendTo"
        '
        'BtnSendMessage
        '
        resources.ApplyResources(Me.BtnSendMessage, "BtnSendMessage")
        Me.BtnSendMessage.Appearance.Font = CType(resources.GetObject("BtnSendMessage.Appearance.Font"), System.Drawing.Font)
        Me.BtnSendMessage.Appearance.Options.UseFont = True
        Me.BtnSendMessage.ImageOptions.ImageKey = resources.GetString("BtnSendMessage.ImageOptions.ImageKey")
        Me.BtnSendMessage.Name = "BtnSendMessage"
        '
        'pnlFilters
        '
        resources.ApplyResources(Me.pnlFilters, "pnlFilters")
        Me.pnlFilters.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlFilters.Controls.Add(Me.lblStatus)
        Me.pnlFilters.Controls.Add(Me.cboStatus)
        Me.pnlFilters.Controls.Add(Me.lblDoctor)
        Me.pnlFilters.Controls.Add(Me.cboDoctor)
        Me.pnlFilters.Controls.Add(Me.lblTo)
        Me.pnlFilters.Controls.Add(Me.lblFrom)
        Me.pnlFilters.Controls.Add(Me.dtpTo)
        Me.pnlFilters.Controls.Add(Me.dtpFrom)
        Me.pnlFilters.Controls.Add(Me.btnApplyFilter)
        Me.pnlFilters.Name = "pnlFilters"
        '
        'lblStatus
        '
        resources.ApplyResources(Me.lblStatus, "lblStatus")
        Me.lblStatus.Appearance.Font = CType(resources.GetObject("lblStatus.Appearance.Font"), System.Drawing.Font)
        Me.lblStatus.Appearance.Options.UseFont = True
        Me.lblStatus.Name = "lblStatus"
        '
        'cboStatus
        '
        resources.ApplyResources(Me.cboStatus, "cboStatus")
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Properties.Appearance.Font = CType(resources.GetObject("cboStatus.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboStatus.Properties.Appearance.Options.UseFont = True
        Me.cboStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboStatus.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblDoctor
        '
        resources.ApplyResources(Me.lblDoctor, "lblDoctor")
        Me.lblDoctor.Appearance.Font = CType(resources.GetObject("lblDoctor.Appearance.Font"), System.Drawing.Font)
        Me.lblDoctor.Appearance.Options.UseFont = True
        Me.lblDoctor.Name = "lblDoctor"
        '
        'cboDoctor
        '
        resources.ApplyResources(Me.cboDoctor, "cboDoctor")
        Me.cboDoctor.Name = "cboDoctor"
        Me.cboDoctor.Properties.Appearance.Font = CType(resources.GetObject("cboDoctor.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboDoctor.Properties.Appearance.Options.UseFont = True
        Me.cboDoctor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboDoctor.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblTo
        '
        resources.ApplyResources(Me.lblTo, "lblTo")
        Me.lblTo.Appearance.Font = CType(resources.GetObject("lblTo.Appearance.Font"), System.Drawing.Font)
        Me.lblTo.Appearance.Options.UseFont = True
        Me.lblTo.Name = "lblTo"
        '
        'lblFrom
        '
        resources.ApplyResources(Me.lblFrom, "lblFrom")
        Me.lblFrom.Appearance.Font = CType(resources.GetObject("lblFrom.Appearance.Font"), System.Drawing.Font)
        Me.lblFrom.Appearance.Options.UseFont = True
        Me.lblFrom.Name = "lblFrom"
        '
        'dtpTo
        '
        resources.ApplyResources(Me.dtpTo, "dtpTo")
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Properties.Appearance.Font = CType(resources.GetObject("dtpTo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtpTo.Properties.Appearance.Options.UseFont = True
        Me.dtpTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpTo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtpTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpTo.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'dtpFrom
        '
        resources.ApplyResources(Me.dtpFrom, "dtpFrom")
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Properties.Appearance.Font = CType(resources.GetObject("dtpFrom.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtpFrom.Properties.Appearance.Options.UseFont = True
        Me.dtpFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpFrom.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtpFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpFrom.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btnApplyFilter
        '
        resources.ApplyResources(Me.btnApplyFilter, "btnApplyFilter")
        Me.btnApplyFilter.Appearance.Font = CType(resources.GetObject("btnApplyFilter.Appearance.Font"), System.Drawing.Font)
        Me.btnApplyFilter.Appearance.Options.UseFont = True
        Me.btnApplyFilter.ImageOptions.ImageKey = resources.GetString("btnApplyFilter.ImageOptions.ImageKey")
        Me.btnApplyFilter.Name = "btnApplyFilter"
        '
        'gcAppts
        '
        resources.ApplyResources(Me.gcAppts, "gcAppts")
        Me.gcAppts.EmbeddedNavigator.AccessibleDescription = resources.GetString("gcAppts.EmbeddedNavigator.AccessibleDescription")
        Me.gcAppts.EmbeddedNavigator.AccessibleName = resources.GetString("gcAppts.EmbeddedNavigator.AccessibleName")
        Me.gcAppts.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gcAppts.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gcAppts.EmbeddedNavigator.Anchor = CType(resources.GetObject("gcAppts.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gcAppts.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gcAppts.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gcAppts.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gcAppts.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gcAppts.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gcAppts.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gcAppts.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gcAppts.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gcAppts.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gcAppts.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gcAppts.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gcAppts.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gcAppts.EmbeddedNavigator.ToolTip = resources.GetString("gcAppts.EmbeddedNavigator.ToolTip")
        Me.gcAppts.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gcAppts.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gcAppts.EmbeddedNavigator.ToolTipTitle = resources.GetString("gcAppts.EmbeddedNavigator.ToolTipTitle")
        Me.gcAppts.MainView = Me.gvAppts
        Me.gcAppts.Name = "gcAppts"
        Me.gcAppts.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repoSend})
        Me.gcAppts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvAppts})
        '
        'gvAppts
        '
        resources.ApplyResources(Me.gvAppts, "gvAppts")
        Me.gvAppts.Appearance.HeaderPanel.Font = CType(resources.GetObject("gvAppts.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.gvAppts.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvAppts.Appearance.Row.Font = CType(resources.GetObject("gvAppts.Appearance.Row.Font"), System.Drawing.Font)
        Me.gvAppts.Appearance.Row.Options.UseFont = True
        Me.gvAppts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSend, Me.colDate, Me.colTime, Me.colDoctorName, Me.colStatus, Me.colReason})
        Me.gvAppts.GridControl = Me.gcAppts
        Me.gvAppts.Name = "gvAppts"
        Me.gvAppts.OptionsView.ShowGroupPanel = False
        '
        'colSend
        '
        resources.ApplyResources(Me.colSend, "colSend")
        Me.colSend.ColumnEdit = Me.repoSend
        Me.colSend.FieldName = "Send"
        Me.colSend.ImageOptions.ImageKey = resources.GetString("colSend.ImageOptions.ImageKey")
        Me.colSend.MaxWidth = 60
        Me.colSend.MinWidth = 50
        Me.colSend.Name = "colSend"
        '
        'repoSend
        '
        resources.ApplyResources(Me.repoSend, "repoSend")
        Me.repoSend.Name = "repoSend"
        '
        'colDate
        '
        resources.ApplyResources(Me.colDate, "colDate")
        Me.colDate.FieldName = "DateText"
        Me.colDate.ImageOptions.ImageKey = resources.GetString("colDate.ImageOptions.ImageKey")
        Me.colDate.Name = "colDate"
        '
        'colTime
        '
        resources.ApplyResources(Me.colTime, "colTime")
        Me.colTime.FieldName = "TimeText"
        Me.colTime.ImageOptions.ImageKey = resources.GetString("colTime.ImageOptions.ImageKey")
        Me.colTime.Name = "colTime"
        '
        'colDoctorName
        '
        resources.ApplyResources(Me.colDoctorName, "colDoctorName")
        Me.colDoctorName.FieldName = "DoctorName"
        Me.colDoctorName.ImageOptions.ImageKey = resources.GetString("colDoctorName.ImageOptions.ImageKey")
        Me.colDoctorName.Name = "colDoctorName"
        '
        'colStatus
        '
        resources.ApplyResources(Me.colStatus, "colStatus")
        Me.colStatus.FieldName = "Status"
        Me.colStatus.ImageOptions.ImageKey = resources.GetString("colStatus.ImageOptions.ImageKey")
        Me.colStatus.Name = "colStatus"
        '
        'colReason
        '
        resources.ApplyResources(Me.colReason, "colReason")
        Me.colReason.FieldName = "Reason"
        Me.colReason.ImageOptions.ImageKey = resources.GetString("colReason.ImageOptions.ImageKey")
        Me.colReason.Name = "colReason"
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
        Me.txtWhats.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.SimpleMaskManager))
        Me.txtWhats.Properties.MaskSettings.Set("MaskManagerSignature", "ignoreMaskBlank=True")
        Me.txtWhats.Properties.MaskSettings.Set("mask", "000000000000")
        Me.txtWhats.Properties.MaskSettings.Set("placeholder", Global.Microsoft.VisualBasic.ChrW(0))
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
        'FrmPatientAppts
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelControl9)
        Me.Controls.Add(Me.RadioLang)
        Me.Controls.Add(Me.lblWhats)
        Me.Controls.Add(Me.cboPrefix)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.txtWhats)
        Me.Controls.Add(Me.gcAppts)
        Me.Controls.Add(Me.pnlFilters)
        Me.Controls.Add(Me.BtnSendMessage)
        Me.Controls.Add(Me.lblSendTo)
        Me.Name = "FrmPatientAppts"
        CType(Me.pnlFilters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlFilters.PerformLayout()
        CType(Me.cboStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDoctor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcAppts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAppts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repoSend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSendTo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnSendMessage As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents pnlFilters As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboStatus As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblDoctor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboDoctor As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblTo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFrom As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtpFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnApplyFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcAppts As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvAppts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSend As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDoctorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents repoSend As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RadioLang As DevExpress.XtraEditors.RadioGroup
End Class
