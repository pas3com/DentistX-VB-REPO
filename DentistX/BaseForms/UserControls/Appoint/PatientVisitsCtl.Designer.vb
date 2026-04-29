<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PatientVisitsCtl
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim lblWhatsApp As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PatientVisitsCtl))
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.chkIncludeNotes = New DevExpress.XtraEditors.CheckEdit()
        Me.chkIncludeReason = New DevExpress.XtraEditors.CheckEdit()
        Me.grpWhats = New DevExpress.XtraEditors.GroupControl()
        Me.RadioLang = New DevExpress.XtraEditors.RadioGroup()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.chk24H = New DevExpress.XtraEditors.CheckEdit()
        Me.chk2H = New DevExpress.XtraEditors.CheckEdit()
        Me.chkWhats = New DevExpress.XtraEditors.CheckEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.slctdDate = New DevExpress.XtraEditors.Controls.CalendarControl()
        Me.cboStatus = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.chkAllAppts = New DevExpress.XtraEditors.CheckEdit()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.DoctorsCombo1 = New DentistX.DoctorsCombo()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.apptDate = New DevExpress.XtraEditors.DateEdit()
        Me.dtpEnd = New DevExpress.XtraEditors.DateEdit()
        Me.dtpStart = New DevExpress.XtraEditors.DateEdit()
        Me.txtNotes = New DevExpress.XtraEditors.TextEdit()
        Me.txtReason = New DevExpress.XtraEditors.TextEdit()
        Me.txtDrId = New DevExpress.XtraEditors.TextEdit()
        Me.txtPatientName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.gridResults = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAppID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFromTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPaient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDoctor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        lblWhatsApp = New System.Windows.Forms.Label()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.chkIncludeNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeReason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpWhats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpWhats.SuspendLayout()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk24H.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk2H.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.slctdDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllAppts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.apptDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.apptDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDrId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPatientName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.gridResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblWhatsApp
        '
        resources.ApplyResources(lblWhatsApp, "lblWhatsApp")
        lblWhatsApp.Name = "lblWhatsApp"
        '
        'PanelControl1
        '
        resources.ApplyResources(Me.PanelControl1, "PanelControl1")
        Me.PanelControl1.Controls.Add(Me.chkIncludeNotes)
        Me.PanelControl1.Controls.Add(Me.chkIncludeReason)
        Me.PanelControl1.Controls.Add(Me.grpWhats)
        Me.PanelControl1.Controls.Add(Me.slctdDate)
        Me.PanelControl1.Controls.Add(Me.cboStatus)
        Me.PanelControl1.Controls.Add(Me.chkAllAppts)
        Me.PanelControl1.Controls.Add(Me.btnDelete)
        Me.PanelControl1.Controls.Add(Me.DoctorsCombo1)
        Me.PanelControl1.Controls.Add(Me.btnSave)
        Me.PanelControl1.Controls.Add(Me.apptDate)
        Me.PanelControl1.Controls.Add(Me.dtpEnd)
        Me.PanelControl1.Controls.Add(Me.dtpStart)
        Me.PanelControl1.Controls.Add(Me.txtNotes)
        Me.PanelControl1.Controls.Add(Me.txtReason)
        Me.PanelControl1.Controls.Add(Me.txtDrId)
        Me.PanelControl1.Controls.Add(Me.txtPatientName)
        Me.PanelControl1.Controls.Add(Me.LabelControl7)
        Me.PanelControl1.Controls.Add(Me.LabelControl6)
        Me.PanelControl1.Controls.Add(Me.LabelControl5)
        Me.PanelControl1.Controls.Add(Me.LabelControl8)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl13)
        Me.PanelControl1.Controls.Add(Me.LabelControl12)
        Me.PanelControl1.Controls.Add(Me.LabelControl11)
        Me.PanelControl1.Controls.Add(Me.LabelControl10)
        Me.PanelControl1.Controls.Add(Me.LabelControl9)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Name = "PanelControl1"
        '
        'chkIncludeNotes
        '
        resources.ApplyResources(Me.chkIncludeNotes, "chkIncludeNotes")
        Me.chkIncludeNotes.Name = "chkIncludeNotes"
        Me.chkIncludeNotes.Properties.Appearance.Font = CType(resources.GetObject("chkIncludeNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkIncludeNotes.Properties.Appearance.Options.UseFont = True
        Me.chkIncludeNotes.Properties.Caption = resources.GetString("chkIncludeNotes.Properties.Caption")
        Me.chkIncludeNotes.Properties.DisplayValueChecked = resources.GetString("chkIncludeNotes.Properties.DisplayValueChecked")
        Me.chkIncludeNotes.Properties.DisplayValueGrayed = resources.GetString("chkIncludeNotes.Properties.DisplayValueGrayed")
        Me.chkIncludeNotes.Properties.DisplayValueUnchecked = resources.GetString("chkIncludeNotes.Properties.DisplayValueUnchecked")
        Me.chkIncludeNotes.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkIncludeNotes.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'chkIncludeReason
        '
        resources.ApplyResources(Me.chkIncludeReason, "chkIncludeReason")
        Me.chkIncludeReason.Name = "chkIncludeReason"
        Me.chkIncludeReason.Properties.Appearance.Font = CType(resources.GetObject("chkIncludeReason.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkIncludeReason.Properties.Appearance.Options.UseFont = True
        Me.chkIncludeReason.Properties.Caption = resources.GetString("chkIncludeReason.Properties.Caption")
        Me.chkIncludeReason.Properties.DisplayValueChecked = resources.GetString("chkIncludeReason.Properties.DisplayValueChecked")
        Me.chkIncludeReason.Properties.DisplayValueGrayed = resources.GetString("chkIncludeReason.Properties.DisplayValueGrayed")
        Me.chkIncludeReason.Properties.DisplayValueUnchecked = resources.GetString("chkIncludeReason.Properties.DisplayValueUnchecked")
        Me.chkIncludeReason.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkIncludeReason.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'grpWhats
        '
        resources.ApplyResources(Me.grpWhats, "grpWhats")
        Me.grpWhats.AppearanceCaption.Font = CType(resources.GetObject("grpWhats.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpWhats.AppearanceCaption.Options.UseFont = True
        Me.grpWhats.Controls.Add(Me.RadioLang)
        Me.grpWhats.Controls.Add(Me.lblWhats)
        Me.grpWhats.Controls.Add(Me.LabelControl14)
        Me.grpWhats.Controls.Add(Me.cboPrefix)
        Me.grpWhats.Controls.Add(lblWhatsApp)
        Me.grpWhats.Controls.Add(Me.chk24H)
        Me.grpWhats.Controls.Add(Me.chk2H)
        Me.grpWhats.Controls.Add(Me.chkWhats)
        Me.grpWhats.Controls.Add(Me.txtWhats)
        Me.grpWhats.Name = "grpWhats"
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
        'LabelControl14
        '
        resources.ApplyResources(Me.LabelControl14, "LabelControl14")
        Me.LabelControl14.Appearance.Font = CType(resources.GetObject("LabelControl14.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl14.Appearance.Options.UseFont = True
        Me.LabelControl14.Name = "LabelControl14"
        '
        'cboPrefix
        '
        resources.ApplyResources(Me.cboPrefix, "cboPrefix")
        Me.cboPrefix.Name = "cboPrefix"
        Me.cboPrefix.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPrefix.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'chk24H
        '
        resources.ApplyResources(Me.chk24H, "chk24H")
        Me.chk24H.Name = "chk24H"
        Me.chk24H.Properties.Appearance.Font = CType(resources.GetObject("chk24H.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chk24H.Properties.Appearance.Options.UseFont = True
        Me.chk24H.Properties.Caption = resources.GetString("chk24H.Properties.Caption")
        Me.chk24H.Properties.DisplayValueChecked = resources.GetString("chk24H.Properties.DisplayValueChecked")
        Me.chk24H.Properties.DisplayValueGrayed = resources.GetString("chk24H.Properties.DisplayValueGrayed")
        Me.chk24H.Properties.DisplayValueUnchecked = resources.GetString("chk24H.Properties.DisplayValueUnchecked")
        Me.chk24H.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chk24H.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'chk2H
        '
        resources.ApplyResources(Me.chk2H, "chk2H")
        Me.chk2H.Name = "chk2H"
        Me.chk2H.Properties.Appearance.Font = CType(resources.GetObject("chk2H.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chk2H.Properties.Appearance.Options.UseFont = True
        Me.chk2H.Properties.Caption = resources.GetString("chk2H.Properties.Caption")
        Me.chk2H.Properties.DisplayValueChecked = resources.GetString("chk2H.Properties.DisplayValueChecked")
        Me.chk2H.Properties.DisplayValueGrayed = resources.GetString("chk2H.Properties.DisplayValueGrayed")
        Me.chk2H.Properties.DisplayValueUnchecked = resources.GetString("chk2H.Properties.DisplayValueUnchecked")
        Me.chk2H.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chk2H.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'chkWhats
        '
        resources.ApplyResources(Me.chkWhats, "chkWhats")
        Me.chkWhats.Name = "chkWhats"
        Me.chkWhats.Properties.Appearance.Font = CType(resources.GetObject("chkWhats.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkWhats.Properties.Appearance.Options.UseFont = True
        Me.chkWhats.Properties.Caption = resources.GetString("chkWhats.Properties.Caption")
        Me.chkWhats.Properties.DisplayValueChecked = resources.GetString("chkWhats.Properties.DisplayValueChecked")
        Me.chkWhats.Properties.DisplayValueGrayed = resources.GetString("chkWhats.Properties.DisplayValueGrayed")
        Me.chkWhats.Properties.DisplayValueUnchecked = resources.GetString("chkWhats.Properties.DisplayValueUnchecked")
        Me.chkWhats.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkWhats.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'txtWhats
        '
        resources.ApplyResources(Me.txtWhats, "txtWhats")
        Me.txtWhats.Name = "txtWhats"
        Me.txtWhats.Properties.Appearance.Font = CType(resources.GetObject("txtWhats.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtWhats.Properties.Appearance.Options.UseFont = True
        Me.txtWhats.Properties.MaxLength = 10
        '
        'slctdDate
        '
        resources.ApplyResources(Me.slctdDate, "slctdDate")
        Me.slctdDate.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.slctdDate.CalendarAppearance.WeekDay.Font = CType(resources.GetObject("slctdDate.CalendarAppearance.WeekDay.Font"), System.Drawing.Font)
        Me.slctdDate.CalendarAppearance.WeekDay.Options.UseFont = True
        Me.slctdDate.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("slctdDate.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.slctdDate.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Fluent
        Me.slctdDate.CaseWeekDayAbbreviations = DevExpress.XtraEditors.Controls.TextCaseMode.System
        Me.slctdDate.FirstDayOfWeek = System.DayOfWeek.Saturday
        Me.slctdDate.Name = "slctdDate"
        Me.slctdDate.ShowClearButton = True
        Me.slctdDate.ShowMonthNavigationButtons = DevExpress.Utils.DefaultBoolean.[True]
        Me.slctdDate.ShowYearNavigationButtons = DevExpress.Utils.DefaultBoolean.[True]
        '
        'cboStatus
        '
        resources.ApplyResources(Me.cboStatus, "cboStatus")
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Properties.Appearance.Font = CType(resources.GetObject("cboStatus.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboStatus.Properties.Appearance.Options.UseFont = True
        Me.cboStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboStatus.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboStatus.Properties.Items.AddRange(New Object() {resources.GetString("cboStatus.Properties.Items"), resources.GetString("cboStatus.Properties.Items1"), resources.GetString("cboStatus.Properties.Items2"), resources.GetString("cboStatus.Properties.Items3"), resources.GetString("cboStatus.Properties.Items4")})
        '
        'chkAllAppts
        '
        resources.ApplyResources(Me.chkAllAppts, "chkAllAppts")
        Me.chkAllAppts.Name = "chkAllAppts"
        Me.chkAllAppts.Properties.Appearance.Font = CType(resources.GetObject("chkAllAppts.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkAllAppts.Properties.Appearance.Options.UseFont = True
        Me.chkAllAppts.Properties.Caption = resources.GetString("chkAllAppts.Properties.Caption")
        Me.chkAllAppts.Properties.DisplayValueChecked = resources.GetString("chkAllAppts.Properties.DisplayValueChecked")
        Me.chkAllAppts.Properties.DisplayValueGrayed = resources.GetString("chkAllAppts.Properties.DisplayValueGrayed")
        Me.chkAllAppts.Properties.DisplayValueUnchecked = resources.GetString("chkAllAppts.Properties.DisplayValueUnchecked")
        Me.chkAllAppts.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkAllAppts.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'btnDelete
        '
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.Appearance.Font = CType(resources.GetObject("btnDelete.Appearance.Font"), System.Drawing.Font)
        Me.btnDelete.Appearance.Options.UseFont = True
        Me.btnDelete.ImageOptions.ImageKey = resources.GetString("btnDelete.ImageOptions.ImageKey")
        Me.btnDelete.Name = "btnDelete"
        '
        'DoctorsCombo1
        '
        resources.ApplyResources(Me.DoctorsCombo1, "DoctorsCombo1")
        Me.DoctorsCombo1.Appearance.Font = CType(resources.GetObject("DoctorsCombo1.Appearance.Font"), System.Drawing.Font)
        Me.DoctorsCombo1.Appearance.Options.UseFont = True
        Me.DoctorsCombo1.BtnAddVisible = True
        Me.DoctorsCombo1.BtnSearchVisible = True
        Me.DoctorsCombo1.DrID = 1
        Me.DoctorsCombo1.DrName = "Dr. Pascal"
        Me.DoctorsCombo1.Filter = ""
        Me.DoctorsCombo1.Name = "DoctorsCombo1"
        '
        'btnSave
        '
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Appearance.Font = CType(resources.GetObject("btnSave.Appearance.Font"), System.Drawing.Font)
        Me.btnSave.Appearance.Options.UseFont = True
        Me.btnSave.ImageOptions.ImageKey = resources.GetString("btnSave.ImageOptions.ImageKey")
        Me.btnSave.Name = "btnSave"
        '
        'apptDate
        '
        resources.ApplyResources(Me.apptDate, "apptDate")
        Me.apptDate.Name = "apptDate"
        Me.apptDate.Properties.Appearance.Font = CType(resources.GetObject("apptDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.apptDate.Properties.Appearance.Options.UseFont = True
        Me.apptDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("apptDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.apptDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("apptDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.apptDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Fluent
        Me.apptDate.Properties.DisplayFormat.FormatString = "dddd, dd/MM/yyyy"
        Me.apptDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.apptDate.Properties.EditFormat.FormatString = "dddd, dd/MM/yyyy"
        Me.apptDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.apptDate.Properties.FirstDayOfWeek = System.DayOfWeek.Saturday
        Me.apptDate.Properties.MaskSettings.Set("mask", "dddd, dd/MM/yyyy")
        Me.apptDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        '
        'dtpEnd
        '
        resources.ApplyResources(Me.dtpEnd, "dtpEnd")
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Properties.Appearance.Font = CType(resources.GetObject("dtpEnd.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtpEnd.Properties.Appearance.Options.UseFont = True
        Me.dtpEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpEnd.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtpEnd.Properties.CalendarDateEditing = False
        Me.dtpEnd.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtpEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpEnd.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtpEnd.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.dtpEnd.Properties.DisplayFormat.FormatString = "t"
        Me.dtpEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpEnd.Properties.MaskSettings.Set("mask", "t")
        Me.dtpEnd.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtpEnd.Properties.ValidateOnEnterKey = True
        Me.dtpEnd.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        '
        'dtpStart
        '
        resources.ApplyResources(Me.dtpStart, "dtpStart")
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Properties.Appearance.Font = CType(resources.GetObject("dtpStart.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtpStart.Properties.Appearance.Options.UseFont = True
        Me.dtpStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpStart.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtpStart.Properties.CalendarDateEditing = False
        Me.dtpStart.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtpStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpStart.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtpStart.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.dtpStart.Properties.DisplayFormat.FormatString = "t"
        Me.dtpStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpStart.Properties.MaskSettings.Set("mask", "t")
        Me.dtpStart.Properties.ShowMonthHeaders = False
        Me.dtpStart.Properties.ShowMonthNavigationButtons = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtpStart.Properties.ShowToday = False
        Me.dtpStart.Properties.ShowYearNavigationButtons = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtpStart.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        '
        'txtNotes
        '
        resources.ApplyResources(Me.txtNotes, "txtNotes")
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Properties.Appearance.Font = CType(resources.GetObject("txtNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtNotes.Properties.Appearance.Options.UseFont = True
        '
        'txtReason
        '
        resources.ApplyResources(Me.txtReason, "txtReason")
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Properties.Appearance.Font = CType(resources.GetObject("txtReason.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtReason.Properties.Appearance.Options.UseFont = True
        '
        'txtDrId
        '
        resources.ApplyResources(Me.txtDrId, "txtDrId")
        Me.txtDrId.Name = "txtDrId"
        Me.txtDrId.Properties.Appearance.Font = CType(resources.GetObject("txtDrId.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtDrId.Properties.Appearance.Options.UseFont = True
        '
        'txtPatientName
        '
        resources.ApplyResources(Me.txtPatientName, "txtPatientName")
        Me.txtPatientName.Name = "txtPatientName"
        Me.txtPatientName.Properties.Appearance.Font = CType(resources.GetObject("txtPatientName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtPatientName.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl7
        '
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Name = "LabelControl7"
        '
        'LabelControl6
        '
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Name = "LabelControl6"
        '
        'LabelControl5
        '
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Name = "LabelControl5"
        '
        'LabelControl8
        '
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Name = "LabelControl8"
        '
        'LabelControl4
        '
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Name = "LabelControl4"
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'LabelControl13
        '
        resources.ApplyResources(Me.LabelControl13, "LabelControl13")
        Me.LabelControl13.Appearance.BackColor = System.Drawing.Color.LightGray
        Me.LabelControl13.Appearance.Font = CType(resources.GetObject("LabelControl13.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl13.Appearance.Options.UseBackColor = True
        Me.LabelControl13.Appearance.Options.UseFont = True
        Me.LabelControl13.Appearance.Options.UseTextOptions = True
        Me.LabelControl13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl13.Name = "LabelControl13"
        '
        'LabelControl12
        '
        resources.ApplyResources(Me.LabelControl12, "LabelControl12")
        Me.LabelControl12.Appearance.BackColor = System.Drawing.Color.LightCoral
        Me.LabelControl12.Appearance.Font = CType(resources.GetObject("LabelControl12.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl12.Appearance.Options.UseBackColor = True
        Me.LabelControl12.Appearance.Options.UseFont = True
        Me.LabelControl12.Appearance.Options.UseTextOptions = True
        Me.LabelControl12.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl12.Name = "LabelControl12"
        '
        'LabelControl11
        '
        resources.ApplyResources(Me.LabelControl11, "LabelControl11")
        Me.LabelControl11.Appearance.BackColor = System.Drawing.Color.LightGreen
        Me.LabelControl11.Appearance.Font = CType(resources.GetObject("LabelControl11.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl11.Appearance.Options.UseBackColor = True
        Me.LabelControl11.Appearance.Options.UseFont = True
        Me.LabelControl11.Appearance.Options.UseTextOptions = True
        Me.LabelControl11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl11.Name = "LabelControl11"
        '
        'LabelControl10
        '
        resources.ApplyResources(Me.LabelControl10, "LabelControl10")
        Me.LabelControl10.Appearance.BackColor = System.Drawing.Color.LightSkyBlue
        Me.LabelControl10.Appearance.Font = CType(resources.GetObject("LabelControl10.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl10.Appearance.Options.UseBackColor = True
        Me.LabelControl10.Appearance.Options.UseFont = True
        Me.LabelControl10.Appearance.Options.UseTextOptions = True
        Me.LabelControl10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl10.Name = "LabelControl10"
        '
        'LabelControl9
        '
        resources.ApplyResources(Me.LabelControl9, "LabelControl9")
        Me.LabelControl9.Appearance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.LabelControl9.Appearance.Font = CType(resources.GetObject("LabelControl9.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl9.Appearance.Options.UseBackColor = True
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Appearance.Options.UseTextOptions = True
        Me.LabelControl9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl9.Name = "LabelControl9"
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'PanelControl2
        '
        resources.ApplyResources(Me.PanelControl2, "PanelControl2")
        Me.PanelControl2.Controls.Add(Me.gridResults)
        Me.PanelControl2.Name = "PanelControl2"
        '
        'gridResults
        '
        resources.ApplyResources(Me.gridResults, "gridResults")
        Me.gridResults.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridResults.EmbeddedNavigator.AccessibleDescription")
        Me.gridResults.EmbeddedNavigator.AccessibleName = resources.GetString("gridResults.EmbeddedNavigator.AccessibleName")
        Me.gridResults.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridResults.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridResults.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridResults.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridResults.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridResults.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridResults.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridResults.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridResults.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridResults.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridResults.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridResults.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridResults.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridResults.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridResults.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridResults.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridResults.EmbeddedNavigator.ToolTip = resources.GetString("gridResults.EmbeddedNavigator.ToolTip")
        Me.gridResults.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridResults.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridResults.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridResults.EmbeddedNavigator.ToolTipTitle")
        Me.gridResults.MainView = Me.dgView
        Me.gridResults.Name = "gridResults"
        Me.gridResults.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        resources.ApplyResources(Me.dgView, "dgView")
        Me.dgView.Appearance.HeaderPanel.Font = CType(resources.GetObject("dgView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
        Me.dgView.Appearance.Row.Font = CType(resources.GetObject("dgView.Appearance.Row.Font"), System.Drawing.Font)
        Me.dgView.Appearance.Row.Options.UseFont = True
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colAppID, Me.colDate, Me.colFromTo, Me.colPaient, Me.colDoctor, Me.colDetail, Me.colStatus})
        Me.dgView.GridControl = Me.gridResults
        Me.dgView.Name = "dgView"
        '
        'colRowNum
        '
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "Num"
        Me.colRowNum.ImageOptions.ImageKey = resources.GetString("colRowNum.ImageOptions.ImageKey")
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colAppID
        '
        resources.ApplyResources(Me.colAppID, "colAppID")
        Me.colAppID.FieldName = "AppointmentID"
        Me.colAppID.ImageOptions.ImageKey = resources.GetString("colAppID.ImageOptions.ImageKey")
        Me.colAppID.Name = "colAppID"
        '
        'colDate
        '
        resources.ApplyResources(Me.colDate, "colDate")
        Me.colDate.FieldName = "ApptDate"
        Me.colDate.ImageOptions.ImageKey = resources.GetString("colDate.ImageOptions.ImageKey")
        Me.colDate.Name = "colDate"
        '
        'colFromTo
        '
        resources.ApplyResources(Me.colFromTo, "colFromTo")
        Me.colFromTo.FieldName = "FromTo"
        Me.colFromTo.ImageOptions.ImageKey = resources.GetString("colFromTo.ImageOptions.ImageKey")
        Me.colFromTo.Name = "colFromTo"
        '
        'colPaient
        '
        resources.ApplyResources(Me.colPaient, "colPaient")
        Me.colPaient.FieldName = "PatientName"
        Me.colPaient.ImageOptions.ImageKey = resources.GetString("colPaient.ImageOptions.ImageKey")
        Me.colPaient.Name = "colPaient"
        '
        'colDoctor
        '
        resources.ApplyResources(Me.colDoctor, "colDoctor")
        Me.colDoctor.FieldName = "DoctorName"
        Me.colDoctor.ImageOptions.ImageKey = resources.GetString("colDoctor.ImageOptions.ImageKey")
        Me.colDoctor.Name = "colDoctor"
        '
        'colDetail
        '
        resources.ApplyResources(Me.colDetail, "colDetail")
        Me.colDetail.FieldName = "Detail"
        Me.colDetail.ImageOptions.ImageKey = resources.GetString("colDetail.ImageOptions.ImageKey")
        Me.colDetail.Name = "colDetail"
        '
        'colStatus
        '
        resources.ApplyResources(Me.colStatus, "colStatus")
        Me.colStatus.FieldName = "Status"
        Me.colStatus.ImageOptions.ImageKey = resources.GetString("colStatus.ImageOptions.ImageKey")
        Me.colStatus.Name = "colStatus"
        '
        'PatientVisitsCtl
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "PatientVisitsCtl"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.chkIncludeNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeReason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpWhats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpWhats.ResumeLayout(False)
        Me.grpWhats.PerformLayout()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk24H.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk2H.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.slctdDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllAppts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.apptDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.apptDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDrId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPatientName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.gridResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents gridResults As DevExpress.XtraGrid.GridControl
    Friend WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAppID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFromTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPaient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDoctor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkAllAppts As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents DoctorsCombo1 As DoctorsCombo
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents apptDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtpEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtpStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtNotes As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtReason As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDrId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPatientName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cboStatus As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents slctdDate As DevExpress.XtraEditors.Controls.CalendarControl
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RadioLang As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents chkWhats As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grpWhats As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chk24H As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chk2H As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkIncludeNotes As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkIncludeReason As DevExpress.XtraEditors.CheckEdit
End Class
