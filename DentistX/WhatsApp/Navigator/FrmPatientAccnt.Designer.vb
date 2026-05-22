<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPatientAccnt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPatientAccnt))
        Me.lblSendTo = New DevExpress.XtraEditors.LabelControl()
        Me.pnlFilters = New DevExpress.XtraEditors.PanelControl()
        Me.chkIncludeZeroTrts = New DevExpress.XtraEditors.CheckEdit()
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.cboStatus = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblTo = New DevExpress.XtraEditors.LabelControl()
        Me.lblFrom = New DevExpress.XtraEditors.LabelControl()
        Me.dtpTo = New DevExpress.XtraEditors.DateEdit()
        Me.dtpFrom = New DevExpress.XtraEditors.DateEdit()
        Me.btnApplyFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.gcAccnt = New DevExpress.XtraGrid.GridControl()
        Me.gvAccnt = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSend = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.repoSend = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BtnSendMessage = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSendAllAccnt = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.RadioLang = New DevExpress.XtraEditors.RadioGroup()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.chkFullDetail = New DevExpress.XtraEditors.CheckEdit()
        Me.btnSelectAll = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSelectNone = New DevExpress.XtraEditors.SimpleButton()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.pnlFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilters.SuspendLayout()
        CType(Me.chkIncludeZeroTrts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcAccnt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAccnt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repoSend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFullDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'pnlFilters
        '
        resources.ApplyResources(Me.pnlFilters, "pnlFilters")
        Me.pnlFilters.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlFilters.Controls.Add(Me.chkIncludeZeroTrts)
        Me.pnlFilters.Controls.Add(Me.lblStatus)
        Me.pnlFilters.Controls.Add(Me.cboStatus)
        Me.pnlFilters.Controls.Add(Me.lblTo)
        Me.pnlFilters.Controls.Add(Me.lblFrom)
        Me.pnlFilters.Controls.Add(Me.dtpTo)
        Me.pnlFilters.Controls.Add(Me.dtpFrom)
        Me.pnlFilters.Controls.Add(Me.btnApplyFilter)
        Me.pnlFilters.Name = "pnlFilters"
        '
        'chkIncludeZeroTrts
        '
        resources.ApplyResources(Me.chkIncludeZeroTrts, "chkIncludeZeroTrts")
        Me.chkIncludeZeroTrts.EnterMoveNextControl = True
        Me.chkIncludeZeroTrts.Name = "chkIncludeZeroTrts"
        Me.chkIncludeZeroTrts.Properties.Appearance.Font = CType(resources.GetObject("chkIncludeZeroTrts.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkIncludeZeroTrts.Properties.Appearance.Options.UseFont = True
        Me.chkIncludeZeroTrts.Properties.Caption = resources.GetString("chkIncludeZeroTrts.Properties.Caption")
        Me.chkIncludeZeroTrts.Properties.DisplayValueChecked = resources.GetString("chkIncludeZeroTrts.Properties.DisplayValueChecked")
        Me.chkIncludeZeroTrts.Properties.DisplayValueGrayed = resources.GetString("chkIncludeZeroTrts.Properties.DisplayValueGrayed")
        Me.chkIncludeZeroTrts.Properties.DisplayValueUnchecked = resources.GetString("chkIncludeZeroTrts.Properties.DisplayValueUnchecked")
        Me.chkIncludeZeroTrts.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkIncludeZeroTrts.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
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
        Me.cboStatus.EnterMoveNextControl = True
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Properties.Appearance.Font = CType(resources.GetObject("cboStatus.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboStatus.Properties.Appearance.Options.UseFont = True
        Me.cboStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboStatus.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
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
        Me.dtpTo.EnterMoveNextControl = True
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Properties.Appearance.Font = CType(resources.GetObject("dtpTo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtpTo.Properties.Appearance.Options.UseFont = True
        Me.dtpTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpTo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtpTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpTo.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'dtpFrom
        '
        resources.ApplyResources(Me.dtpFrom, "dtpFrom")
        Me.dtpFrom.EnterMoveNextControl = True
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
        'gcAccnt
        '
        resources.ApplyResources(Me.gcAccnt, "gcAccnt")
        Me.gcAccnt.EmbeddedNavigator.AccessibleDescription = resources.GetString("gcAccnt.EmbeddedNavigator.AccessibleDescription")
        Me.gcAccnt.EmbeddedNavigator.AccessibleName = resources.GetString("gcAccnt.EmbeddedNavigator.AccessibleName")
        Me.gcAccnt.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gcAccnt.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gcAccnt.EmbeddedNavigator.Anchor = CType(resources.GetObject("gcAccnt.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gcAccnt.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gcAccnt.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gcAccnt.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gcAccnt.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gcAccnt.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gcAccnt.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gcAccnt.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gcAccnt.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gcAccnt.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gcAccnt.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gcAccnt.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gcAccnt.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gcAccnt.EmbeddedNavigator.ToolTip = resources.GetString("gcAccnt.EmbeddedNavigator.ToolTip")
        Me.gcAccnt.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gcAccnt.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gcAccnt.EmbeddedNavigator.ToolTipTitle = resources.GetString("gcAccnt.EmbeddedNavigator.ToolTipTitle")
        Me.gcAccnt.MainView = Me.gvAccnt
        Me.gcAccnt.Name = "gcAccnt"
        Me.gcAccnt.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repoSend})
        Me.gcAccnt.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvAccnt})
        '
        'gvAccnt
        '
        resources.ApplyResources(Me.gvAccnt, "gvAccnt")
        Me.gvAccnt.Appearance.HeaderPanel.Font = CType(resources.GetObject("gvAccnt.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.gvAccnt.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvAccnt.Appearance.Row.Font = CType(resources.GetObject("gvAccnt.Appearance.Row.Font"), System.Drawing.Font)
        Me.gvAccnt.Appearance.Row.Options.UseFont = True
        Me.gvAccnt.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSend, Me.colDate, Me.colType, Me.colDetail, Me.colValue, Me.colExtra})
        Me.gvAccnt.GridControl = Me.gcAccnt
        Me.gvAccnt.Name = "gvAccnt"
        Me.gvAccnt.OptionsView.ShowGroupPanel = False
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
        'colType
        '
        resources.ApplyResources(Me.colType, "colType")
        Me.colType.FieldName = "TypeText"
        Me.colType.ImageOptions.ImageKey = resources.GetString("colType.ImageOptions.ImageKey")
        Me.colType.Name = "colType"
        '
        'colDetail
        '
        resources.ApplyResources(Me.colDetail, "colDetail")
        Me.colDetail.FieldName = "Detail"
        Me.colDetail.ImageOptions.ImageKey = resources.GetString("colDetail.ImageOptions.ImageKey")
        Me.colDetail.Name = "colDetail"
        '
        'colValue
        '
        resources.ApplyResources(Me.colValue, "colValue")
        Me.colValue.FieldName = "ValueText"
        Me.colValue.ImageOptions.ImageKey = resources.GetString("colValue.ImageOptions.ImageKey")
        Me.colValue.Name = "colValue"
        '
        'colExtra
        '
        resources.ApplyResources(Me.colExtra, "colExtra")
        Me.colExtra.FieldName = "Extra"
        Me.colExtra.ImageOptions.ImageKey = resources.GetString("colExtra.ImageOptions.ImageKey")
        Me.colExtra.Name = "colExtra"
        '
        'BtnSendMessage
        '
        resources.ApplyResources(Me.BtnSendMessage, "BtnSendMessage")
        Me.BtnSendMessage.Appearance.Font = CType(resources.GetObject("BtnSendMessage.Appearance.Font"), System.Drawing.Font)
        Me.BtnSendMessage.Appearance.Options.UseFont = True
        Me.BtnSendMessage.ImageOptions.ImageKey = resources.GetString("BtnSendMessage.ImageOptions.ImageKey")
        Me.BtnSendMessage.Name = "BtnSendMessage"
        '
        'BtnSendAllAccnt
        '
        resources.ApplyResources(Me.BtnSendAllAccnt, "BtnSendAllAccnt")
        Me.BtnSendAllAccnt.Appearance.Font = CType(resources.GetObject("BtnSendAllAccnt.Appearance.Font"), System.Drawing.Font)
        Me.BtnSendAllAccnt.Appearance.Options.UseFont = True
        Me.BtnSendAllAccnt.ImageOptions.ImageKey = resources.GetString("BtnSendAllAccnt.ImageOptions.ImageKey")
        Me.BtnSendAllAccnt.Name = "BtnSendAllAccnt"
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
        'chkFullDetail
        '
        resources.ApplyResources(Me.chkFullDetail, "chkFullDetail")
        Me.chkFullDetail.Name = "chkFullDetail"
        Me.chkFullDetail.Properties.Appearance.Font = CType(resources.GetObject("chkFullDetail.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkFullDetail.Properties.Appearance.Options.UseFont = True
        Me.chkFullDetail.Properties.Caption = resources.GetString("chkFullDetail.Properties.Caption")
        Me.chkFullDetail.Properties.DisplayValueChecked = resources.GetString("chkFullDetail.Properties.DisplayValueChecked")
        Me.chkFullDetail.Properties.DisplayValueGrayed = resources.GetString("chkFullDetail.Properties.DisplayValueGrayed")
        Me.chkFullDetail.Properties.DisplayValueUnchecked = resources.GetString("chkFullDetail.Properties.DisplayValueUnchecked")
        Me.chkFullDetail.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkFullDetail.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'btnSelectAll
        '
        resources.ApplyResources(Me.btnSelectAll, "btnSelectAll")
        Me.btnSelectAll.Appearance.Font = CType(resources.GetObject("btnSelectAll.Appearance.Font"), System.Drawing.Font)
        Me.btnSelectAll.Appearance.Options.UseFont = True
        Me.btnSelectAll.ImageOptions.ImageKey = resources.GetString("btnSelectAll.ImageOptions.ImageKey")
        Me.btnSelectAll.Name = "btnSelectAll"
        '
        'btnSelectNone
        '
        resources.ApplyResources(Me.btnSelectNone, "btnSelectNone")
        Me.btnSelectNone.Appearance.Font = CType(resources.GetObject("btnSelectNone.Appearance.Font"), System.Drawing.Font)
        Me.btnSelectNone.Appearance.Options.UseFont = True
        Me.btnSelectNone.ImageOptions.ImageKey = resources.GetString("btnSelectNone.ImageOptions.ImageKey")
        Me.btnSelectNone.Name = "btnSelectNone"
        '
        'FrmPatientAccnt
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.chkFullDetail)
        Me.Controls.Add(Me.LabelControl9)
        Me.Controls.Add(Me.RadioLang)
        Me.Controls.Add(Me.lblWhats)
        Me.Controls.Add(Me.cboPrefix)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.txtWhats)
        Me.Controls.Add(Me.btnSelectNone)
        Me.Controls.Add(Me.BtnSendAllAccnt)
        Me.Controls.Add(Me.btnSelectAll)
        Me.Controls.Add(Me.BtnSendMessage)
        Me.Controls.Add(Me.gcAccnt)
        Me.Controls.Add(Me.pnlFilters)
        Me.Controls.Add(Me.lblSendTo)
        Me.Name = "FrmPatientAccnt"
        CType(Me.pnlFilters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlFilters.PerformLayout()
        CType(Me.chkIncludeZeroTrts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcAccnt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAccnt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repoSend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFullDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblSendTo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnlFilters As DevExpress.XtraEditors.PanelControl
    Friend WithEvents chkIncludeZeroTrts As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboStatus As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblTo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFrom As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtpFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnApplyFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcAccnt As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvAccnt As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSend As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents repoSend As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BtnSendMessage As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSendAllAccnt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RadioLang As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkFullDetail As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnSelectAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSelectNone As DevExpress.XtraEditors.SimpleButton
End Class
