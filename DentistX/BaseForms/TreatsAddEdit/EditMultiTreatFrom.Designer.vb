<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EditMultiTreatFrom
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditMultiTreatFrom))
        Me.grpHeader = New DevExpress.XtraEditors.GroupControl()
        Me.txtPatientName = New DevExpress.XtraEditors.LabelControl()
        Me.lblPatientCaption = New DevExpress.XtraEditors.LabelControl()
        Me.grpTreats = New DevExpress.XtraEditors.GroupControl()
        Me.clstTreats = New DevExpress.XtraEditors.CheckedListBoxControl()
        Me.pnlSelectButtons = New DevExpress.XtraEditors.PanelControl()
        Me.btnSelectAll = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSelectNone = New DevExpress.XtraEditors.SimpleButton()
        Me.grpChanges = New DevExpress.XtraEditors.GroupControl()
        Me.pnlChangesScroll = New DevExpress.XtraEditors.PanelControl()
        Me.txtTrtNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.chkApplyTrtNotes = New DevExpress.XtraEditors.CheckEdit()
        Me.txtTrtDetails = New DevExpress.XtraEditors.MemoEdit()
        Me.chkApplyTrtDetails = New DevExpress.XtraEditors.CheckEdit()
        Me.txtTrtPlan = New DevExpress.XtraEditors.MemoEdit()
        Me.chkApplyTrtPlan = New DevExpress.XtraEditors.CheckEdit()
        Me.txtPayValue = New DevExpress.XtraEditors.TextEdit()
        Me.chkApplyPayValue = New DevExpress.XtraEditors.CheckEdit()
        Me.txtTrtValue = New DevExpress.XtraEditors.TextEdit()
        Me.chkApplyTrtValue = New DevExpress.XtraEditors.CheckEdit()
        Me.txtExtClinic = New DevExpress.XtraEditors.TextEdit()
        Me.ceIsExternal = New DevExpress.XtraEditors.CheckEdit()
        Me.chkApplyIsExternal = New DevExpress.XtraEditors.CheckEdit()
        Me.dtTreatEnd = New DevExpress.XtraEditors.DateEdit()
        Me.ceFinished = New DevExpress.XtraEditors.CheckEdit()
        Me.chkApplyFinished = New DevExpress.XtraEditors.CheckEdit()
        Me.cboTreatmentType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.chkApplyTreatmentType = New DevExpress.XtraEditors.CheckEdit()
        Me.dtTreatDate = New DevExpress.XtraEditors.DateEdit()
        Me.chkApplyTreatDate = New DevExpress.XtraEditors.CheckEdit()
        Me.pnlButtons = New DevExpress.XtraEditors.PanelControl()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.grpHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHeader.SuspendLayout()
        CType(Me.grpTreats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTreats.SuspendLayout()
        CType(Me.clstTreats, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlSelectButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSelectButtons.SuspendLayout()
        CType(Me.grpChanges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpChanges.SuspendLayout()
        CType(Me.pnlChangesScroll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlChangesScroll.SuspendLayout()
        CType(Me.txtTrtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyTrtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTrtDetails.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyTrtDetails.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTrtPlan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyTrtPlan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyPayValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTrtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyTrtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExtClinic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceIsExternal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyIsExternal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTreatEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTreatEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceFinished.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyFinished.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTreatmentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyTreatmentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTreatDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTreatDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyTreatDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpHeader
        '
        resources.ApplyResources(Me.grpHeader, "grpHeader")
        Me.grpHeader.Controls.Add(Me.txtPatientName)
        Me.grpHeader.Controls.Add(Me.lblPatientCaption)
        Me.grpHeader.Name = "grpHeader"
        Me.grpHeader.ShowCaption = False
        '
        'txtPatientName
        '
        resources.ApplyResources(Me.txtPatientName, "txtPatientName")
        Me.txtPatientName.Appearance.Font = CType(resources.GetObject("txtPatientName.Appearance.Font"), System.Drawing.Font)
        Me.txtPatientName.Appearance.ForeColor = System.Drawing.Color.DarkBlue
        Me.txtPatientName.Appearance.Options.UseFont = True
        Me.txtPatientName.Appearance.Options.UseForeColor = True
        Me.txtPatientName.Name = "txtPatientName"
        '
        'lblPatientCaption
        '
        resources.ApplyResources(Me.lblPatientCaption, "lblPatientCaption")
        Me.lblPatientCaption.Appearance.Font = CType(resources.GetObject("lblPatientCaption.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientCaption.Appearance.Options.UseFont = True
        Me.lblPatientCaption.Name = "lblPatientCaption"
        '
        'grpTreats
        '
        resources.ApplyResources(Me.grpTreats, "grpTreats")
        Me.grpTreats.AppearanceCaption.Font = CType(resources.GetObject("grpTreats.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpTreats.AppearanceCaption.Options.UseFont = True
        Me.grpTreats.Controls.Add(Me.clstTreats)
        Me.grpTreats.Controls.Add(Me.pnlSelectButtons)
        Me.grpTreats.Name = "grpTreats"
        '
        'clstTreats
        '
        resources.ApplyResources(Me.clstTreats, "clstTreats")
        Me.clstTreats.Appearance.Font = CType(resources.GetObject("clstTreats.Appearance.Font"), System.Drawing.Font)
        Me.clstTreats.Appearance.Options.UseFont = True
        Me.clstTreats.Name = "clstTreats"
        '
        'pnlSelectButtons
        '
        resources.ApplyResources(Me.pnlSelectButtons, "pnlSelectButtons")
        Me.pnlSelectButtons.Controls.Add(Me.btnSelectAll)
        Me.pnlSelectButtons.Controls.Add(Me.btnSelectNone)
        Me.pnlSelectButtons.Name = "pnlSelectButtons"
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
        'grpChanges
        '
        resources.ApplyResources(Me.grpChanges, "grpChanges")
        Me.grpChanges.AppearanceCaption.Font = CType(resources.GetObject("grpChanges.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpChanges.AppearanceCaption.Options.UseFont = True
        Me.grpChanges.Controls.Add(Me.pnlChangesScroll)
        Me.grpChanges.Name = "grpChanges"
        '
        'pnlChangesScroll
        '
        resources.ApplyResources(Me.pnlChangesScroll, "pnlChangesScroll")
        Me.pnlChangesScroll.Controls.Add(Me.txtTrtNotes)
        Me.pnlChangesScroll.Controls.Add(Me.chkApplyTrtNotes)
        Me.pnlChangesScroll.Controls.Add(Me.txtTrtDetails)
        Me.pnlChangesScroll.Controls.Add(Me.chkApplyTrtDetails)
        Me.pnlChangesScroll.Controls.Add(Me.txtTrtPlan)
        Me.pnlChangesScroll.Controls.Add(Me.chkApplyTrtPlan)
        Me.pnlChangesScroll.Controls.Add(Me.txtPayValue)
        Me.pnlChangesScroll.Controls.Add(Me.chkApplyPayValue)
        Me.pnlChangesScroll.Controls.Add(Me.txtTrtValue)
        Me.pnlChangesScroll.Controls.Add(Me.chkApplyTrtValue)
        Me.pnlChangesScroll.Controls.Add(Me.txtExtClinic)
        Me.pnlChangesScroll.Controls.Add(Me.ceIsExternal)
        Me.pnlChangesScroll.Controls.Add(Me.chkApplyIsExternal)
        Me.pnlChangesScroll.Controls.Add(Me.dtTreatEnd)
        Me.pnlChangesScroll.Controls.Add(Me.ceFinished)
        Me.pnlChangesScroll.Controls.Add(Me.chkApplyFinished)
        Me.pnlChangesScroll.Controls.Add(Me.cboTreatmentType)
        Me.pnlChangesScroll.Controls.Add(Me.chkApplyTreatmentType)
        Me.pnlChangesScroll.Controls.Add(Me.dtTreatDate)
        Me.pnlChangesScroll.Controls.Add(Me.chkApplyTreatDate)
        Me.pnlChangesScroll.Name = "pnlChangesScroll"
        '
        'txtTrtNotes
        '
        resources.ApplyResources(Me.txtTrtNotes, "txtTrtNotes")
        Me.txtTrtNotes.Name = "txtTrtNotes"
        Me.txtTrtNotes.Properties.Appearance.Font = CType(resources.GetObject("txtTrtNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtTrtNotes.Properties.Appearance.Options.UseFont = True
        '
        'chkApplyTrtNotes
        '
        resources.ApplyResources(Me.chkApplyTrtNotes, "chkApplyTrtNotes")
        Me.chkApplyTrtNotes.Name = "chkApplyTrtNotes"
        Me.chkApplyTrtNotes.Properties.Appearance.Font = CType(resources.GetObject("chkApplyTrtNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkApplyTrtNotes.Properties.Appearance.Options.UseFont = True
        Me.chkApplyTrtNotes.Properties.Caption = resources.GetString("chkApplyTrtNotes.Properties.Caption")
        Me.chkApplyTrtNotes.Properties.DisplayValueChecked = resources.GetString("chkApplyTrtNotes.Properties.DisplayValueChecked")
        Me.chkApplyTrtNotes.Properties.DisplayValueGrayed = resources.GetString("chkApplyTrtNotes.Properties.DisplayValueGrayed")
        Me.chkApplyTrtNotes.Properties.DisplayValueUnchecked = resources.GetString("chkApplyTrtNotes.Properties.DisplayValueUnchecked")
        Me.chkApplyTrtNotes.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkApplyTrtNotes.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'txtTrtDetails
        '
        resources.ApplyResources(Me.txtTrtDetails, "txtTrtDetails")
        Me.txtTrtDetails.Name = "txtTrtDetails"
        Me.txtTrtDetails.Properties.Appearance.Font = CType(resources.GetObject("txtTrtDetails.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtTrtDetails.Properties.Appearance.Options.UseFont = True
        '
        'chkApplyTrtDetails
        '
        resources.ApplyResources(Me.chkApplyTrtDetails, "chkApplyTrtDetails")
        Me.chkApplyTrtDetails.Name = "chkApplyTrtDetails"
        Me.chkApplyTrtDetails.Properties.Appearance.Font = CType(resources.GetObject("chkApplyTrtDetails.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkApplyTrtDetails.Properties.Appearance.Options.UseFont = True
        Me.chkApplyTrtDetails.Properties.Caption = resources.GetString("chkApplyTrtDetails.Properties.Caption")
        Me.chkApplyTrtDetails.Properties.DisplayValueChecked = resources.GetString("chkApplyTrtDetails.Properties.DisplayValueChecked")
        Me.chkApplyTrtDetails.Properties.DisplayValueGrayed = resources.GetString("chkApplyTrtDetails.Properties.DisplayValueGrayed")
        Me.chkApplyTrtDetails.Properties.DisplayValueUnchecked = resources.GetString("chkApplyTrtDetails.Properties.DisplayValueUnchecked")
        Me.chkApplyTrtDetails.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkApplyTrtDetails.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'txtTrtPlan
        '
        resources.ApplyResources(Me.txtTrtPlan, "txtTrtPlan")
        Me.txtTrtPlan.Name = "txtTrtPlan"
        Me.txtTrtPlan.Properties.Appearance.Font = CType(resources.GetObject("txtTrtPlan.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtTrtPlan.Properties.Appearance.Options.UseFont = True
        '
        'chkApplyTrtPlan
        '
        resources.ApplyResources(Me.chkApplyTrtPlan, "chkApplyTrtPlan")
        Me.chkApplyTrtPlan.Name = "chkApplyTrtPlan"
        Me.chkApplyTrtPlan.Properties.Appearance.Font = CType(resources.GetObject("chkApplyTrtPlan.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkApplyTrtPlan.Properties.Appearance.Options.UseFont = True
        Me.chkApplyTrtPlan.Properties.Caption = resources.GetString("chkApplyTrtPlan.Properties.Caption")
        Me.chkApplyTrtPlan.Properties.DisplayValueChecked = resources.GetString("chkApplyTrtPlan.Properties.DisplayValueChecked")
        Me.chkApplyTrtPlan.Properties.DisplayValueGrayed = resources.GetString("chkApplyTrtPlan.Properties.DisplayValueGrayed")
        Me.chkApplyTrtPlan.Properties.DisplayValueUnchecked = resources.GetString("chkApplyTrtPlan.Properties.DisplayValueUnchecked")
        Me.chkApplyTrtPlan.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkApplyTrtPlan.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'txtPayValue
        '
        resources.ApplyResources(Me.txtPayValue, "txtPayValue")
        Me.txtPayValue.Name = "txtPayValue"
        Me.txtPayValue.Properties.Appearance.Font = CType(resources.GetObject("txtPayValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtPayValue.Properties.Appearance.Options.UseFont = True
        Me.txtPayValue.Properties.DisplayFormat.FormatString = "n0"
        Me.txtPayValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtPayValue.Properties.EditFormat.FormatString = "n0"
        Me.txtPayValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '
        'chkApplyPayValue
        '
        resources.ApplyResources(Me.chkApplyPayValue, "chkApplyPayValue")
        Me.chkApplyPayValue.Name = "chkApplyPayValue"
        Me.chkApplyPayValue.Properties.Appearance.Font = CType(resources.GetObject("chkApplyPayValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkApplyPayValue.Properties.Appearance.Options.UseFont = True
        Me.chkApplyPayValue.Properties.Caption = resources.GetString("chkApplyPayValue.Properties.Caption")
        Me.chkApplyPayValue.Properties.DisplayValueChecked = resources.GetString("chkApplyPayValue.Properties.DisplayValueChecked")
        Me.chkApplyPayValue.Properties.DisplayValueGrayed = resources.GetString("chkApplyPayValue.Properties.DisplayValueGrayed")
        Me.chkApplyPayValue.Properties.DisplayValueUnchecked = resources.GetString("chkApplyPayValue.Properties.DisplayValueUnchecked")
        Me.chkApplyPayValue.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkApplyPayValue.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'txtTrtValue
        '
        resources.ApplyResources(Me.txtTrtValue, "txtTrtValue")
        Me.txtTrtValue.Name = "txtTrtValue"
        Me.txtTrtValue.Properties.Appearance.Font = CType(resources.GetObject("txtTrtValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtTrtValue.Properties.Appearance.Options.UseFont = True
        Me.txtTrtValue.Properties.DisplayFormat.FormatString = "n0"
        Me.txtTrtValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtTrtValue.Properties.EditFormat.FormatString = "n0"
        Me.txtTrtValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '
        'chkApplyTrtValue
        '
        resources.ApplyResources(Me.chkApplyTrtValue, "chkApplyTrtValue")
        Me.chkApplyTrtValue.Name = "chkApplyTrtValue"
        Me.chkApplyTrtValue.Properties.Appearance.Font = CType(resources.GetObject("chkApplyTrtValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkApplyTrtValue.Properties.Appearance.Options.UseFont = True
        Me.chkApplyTrtValue.Properties.Caption = resources.GetString("chkApplyTrtValue.Properties.Caption")
        Me.chkApplyTrtValue.Properties.DisplayValueChecked = resources.GetString("chkApplyTrtValue.Properties.DisplayValueChecked")
        Me.chkApplyTrtValue.Properties.DisplayValueGrayed = resources.GetString("chkApplyTrtValue.Properties.DisplayValueGrayed")
        Me.chkApplyTrtValue.Properties.DisplayValueUnchecked = resources.GetString("chkApplyTrtValue.Properties.DisplayValueUnchecked")
        Me.chkApplyTrtValue.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkApplyTrtValue.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'txtExtClinic
        '
        resources.ApplyResources(Me.txtExtClinic, "txtExtClinic")
        Me.txtExtClinic.Name = "txtExtClinic"
        Me.txtExtClinic.Properties.Appearance.Font = CType(resources.GetObject("txtExtClinic.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtExtClinic.Properties.Appearance.Options.UseFont = True
        '
        'ceIsExternal
        '
        resources.ApplyResources(Me.ceIsExternal, "ceIsExternal")
        Me.ceIsExternal.Name = "ceIsExternal"
        Me.ceIsExternal.Properties.Appearance.Font = CType(resources.GetObject("ceIsExternal.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ceIsExternal.Properties.Appearance.Options.UseFont = True
        Me.ceIsExternal.Properties.Caption = resources.GetString("ceIsExternal.Properties.Caption")
        Me.ceIsExternal.Properties.DisplayValueChecked = resources.GetString("ceIsExternal.Properties.DisplayValueChecked")
        Me.ceIsExternal.Properties.DisplayValueGrayed = resources.GetString("ceIsExternal.Properties.DisplayValueGrayed")
        Me.ceIsExternal.Properties.DisplayValueUnchecked = resources.GetString("ceIsExternal.Properties.DisplayValueUnchecked")
        Me.ceIsExternal.Properties.GlyphVerticalAlignment = CType(resources.GetObject("ceIsExternal.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'chkApplyIsExternal
        '
        resources.ApplyResources(Me.chkApplyIsExternal, "chkApplyIsExternal")
        Me.chkApplyIsExternal.Name = "chkApplyIsExternal"
        Me.chkApplyIsExternal.Properties.Appearance.Font = CType(resources.GetObject("chkApplyIsExternal.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkApplyIsExternal.Properties.Appearance.Options.UseFont = True
        Me.chkApplyIsExternal.Properties.Caption = resources.GetString("chkApplyIsExternal.Properties.Caption")
        Me.chkApplyIsExternal.Properties.DisplayValueChecked = resources.GetString("chkApplyIsExternal.Properties.DisplayValueChecked")
        Me.chkApplyIsExternal.Properties.DisplayValueGrayed = resources.GetString("chkApplyIsExternal.Properties.DisplayValueGrayed")
        Me.chkApplyIsExternal.Properties.DisplayValueUnchecked = resources.GetString("chkApplyIsExternal.Properties.DisplayValueUnchecked")
        Me.chkApplyIsExternal.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkApplyIsExternal.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'dtTreatEnd
        '
        resources.ApplyResources(Me.dtTreatEnd, "dtTreatEnd")
        Me.dtTreatEnd.Name = "dtTreatEnd"
        Me.dtTreatEnd.Properties.Appearance.Font = CType(resources.GetObject("dtTreatEnd.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtTreatEnd.Properties.Appearance.Options.UseFont = True
        Me.dtTreatEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTreatEnd.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtTreatEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTreatEnd.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'ceFinished
        '
        resources.ApplyResources(Me.ceFinished, "ceFinished")
        Me.ceFinished.Name = "ceFinished"
        Me.ceFinished.Properties.Appearance.Font = CType(resources.GetObject("ceFinished.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ceFinished.Properties.Appearance.Options.UseFont = True
        Me.ceFinished.Properties.Caption = resources.GetString("ceFinished.Properties.Caption")
        Me.ceFinished.Properties.DisplayValueChecked = resources.GetString("ceFinished.Properties.DisplayValueChecked")
        Me.ceFinished.Properties.DisplayValueGrayed = resources.GetString("ceFinished.Properties.DisplayValueGrayed")
        Me.ceFinished.Properties.DisplayValueUnchecked = resources.GetString("ceFinished.Properties.DisplayValueUnchecked")
        Me.ceFinished.Properties.GlyphVerticalAlignment = CType(resources.GetObject("ceFinished.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'chkApplyFinished
        '
        resources.ApplyResources(Me.chkApplyFinished, "chkApplyFinished")
        Me.chkApplyFinished.Name = "chkApplyFinished"
        Me.chkApplyFinished.Properties.Appearance.Font = CType(resources.GetObject("chkApplyFinished.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkApplyFinished.Properties.Appearance.Options.UseFont = True
        Me.chkApplyFinished.Properties.Caption = resources.GetString("chkApplyFinished.Properties.Caption")
        Me.chkApplyFinished.Properties.DisplayValueChecked = resources.GetString("chkApplyFinished.Properties.DisplayValueChecked")
        Me.chkApplyFinished.Properties.DisplayValueGrayed = resources.GetString("chkApplyFinished.Properties.DisplayValueGrayed")
        Me.chkApplyFinished.Properties.DisplayValueUnchecked = resources.GetString("chkApplyFinished.Properties.DisplayValueUnchecked")
        Me.chkApplyFinished.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkApplyFinished.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'cboTreatmentType
        '
        resources.ApplyResources(Me.cboTreatmentType, "cboTreatmentType")
        Me.cboTreatmentType.Name = "cboTreatmentType"
        Me.cboTreatmentType.Properties.Appearance.Font = CType(resources.GetObject("cboTreatmentType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboTreatmentType.Properties.Appearance.Options.UseFont = True
        Me.cboTreatmentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboTreatmentType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboTreatmentType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'chkApplyTreatmentType
        '
        resources.ApplyResources(Me.chkApplyTreatmentType, "chkApplyTreatmentType")
        Me.chkApplyTreatmentType.Name = "chkApplyTreatmentType"
        Me.chkApplyTreatmentType.Properties.Appearance.Font = CType(resources.GetObject("chkApplyTreatmentType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkApplyTreatmentType.Properties.Appearance.Options.UseFont = True
        Me.chkApplyTreatmentType.Properties.Caption = resources.GetString("chkApplyTreatmentType.Properties.Caption")
        Me.chkApplyTreatmentType.Properties.DisplayValueChecked = resources.GetString("chkApplyTreatmentType.Properties.DisplayValueChecked")
        Me.chkApplyTreatmentType.Properties.DisplayValueGrayed = resources.GetString("chkApplyTreatmentType.Properties.DisplayValueGrayed")
        Me.chkApplyTreatmentType.Properties.DisplayValueUnchecked = resources.GetString("chkApplyTreatmentType.Properties.DisplayValueUnchecked")
        Me.chkApplyTreatmentType.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkApplyTreatmentType.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'dtTreatDate
        '
        resources.ApplyResources(Me.dtTreatDate, "dtTreatDate")
        Me.dtTreatDate.Name = "dtTreatDate"
        Me.dtTreatDate.Properties.Appearance.Font = CType(resources.GetObject("dtTreatDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtTreatDate.Properties.Appearance.Options.UseFont = True
        Me.dtTreatDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTreatDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtTreatDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTreatDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'chkApplyTreatDate
        '
        resources.ApplyResources(Me.chkApplyTreatDate, "chkApplyTreatDate")
        Me.chkApplyTreatDate.Name = "chkApplyTreatDate"
        Me.chkApplyTreatDate.Properties.Appearance.Font = CType(resources.GetObject("chkApplyTreatDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkApplyTreatDate.Properties.Appearance.Options.UseFont = True
        Me.chkApplyTreatDate.Properties.Caption = resources.GetString("chkApplyTreatDate.Properties.Caption")
        Me.chkApplyTreatDate.Properties.DisplayValueChecked = resources.GetString("chkApplyTreatDate.Properties.DisplayValueChecked")
        Me.chkApplyTreatDate.Properties.DisplayValueGrayed = resources.GetString("chkApplyTreatDate.Properties.DisplayValueGrayed")
        Me.chkApplyTreatDate.Properties.DisplayValueUnchecked = resources.GetString("chkApplyTreatDate.Properties.DisplayValueUnchecked")
        Me.chkApplyTreatDate.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkApplyTreatDate.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'pnlButtons
        '
        resources.ApplyResources(Me.pnlButtons, "pnlButtons")
        Me.pnlButtons.Controls.Add(Me.btnSave)
        Me.pnlButtons.Controls.Add(Me.btnCancel)
        Me.pnlButtons.Name = "pnlButtons"
        '
        'btnSave
        '
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Appearance.Font = CType(resources.GetObject("btnSave.Appearance.Font"), System.Drawing.Font)
        Me.btnSave.Appearance.Options.UseFont = True
        Me.btnSave.ImageOptions.ImageKey = resources.GetString("btnSave.ImageOptions.ImageKey")
        Me.btnSave.Name = "btnSave"
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.ImageOptions.ImageKey = resources.GetString("btnCancel.ImageOptions.ImageKey")
        Me.btnCancel.Name = "btnCancel"
        '
        'EditMultiTreatFrom
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpChanges)
        Me.Controls.Add(Me.grpTreats)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.grpHeader)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EditMultiTreatFrom"
        Me.ShowInTaskbar = False
        CType(Me.grpHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpHeader.ResumeLayout(False)
        Me.grpHeader.PerformLayout()
        CType(Me.grpTreats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTreats.ResumeLayout(False)
        CType(Me.clstTreats, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlSelectButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSelectButtons.ResumeLayout(False)
        CType(Me.grpChanges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpChanges.ResumeLayout(False)
        CType(Me.pnlChangesScroll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlChangesScroll.ResumeLayout(False)
        CType(Me.txtTrtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyTrtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTrtDetails.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyTrtDetails.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTrtPlan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyTrtPlan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyPayValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTrtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyTrtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExtClinic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceIsExternal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyIsExternal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTreatEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTreatEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceFinished.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyFinished.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTreatmentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyTreatmentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTreatDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTreatDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyTreatDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpHeader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblPatientCaption As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPatientName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpTreats As DevExpress.XtraEditors.GroupControl
    Friend WithEvents clstTreats As DevExpress.XtraEditors.CheckedListBoxControl
    Friend WithEvents pnlSelectButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnSelectAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSelectNone As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpChanges As DevExpress.XtraEditors.GroupControl
    Friend WithEvents pnlChangesScroll As DevExpress.XtraEditors.PanelControl
    Friend WithEvents chkApplyTreatDate As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dtTreatDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents chkApplyTreatmentType As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cboTreatmentType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents chkApplyFinished As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceFinished As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dtTreatEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents chkApplyIsExternal As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ceIsExternal As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtExtClinic As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkApplyTrtValue As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtTrtValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkApplyPayValue As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtPayValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkApplyTrtPlan As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtTrtPlan As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents chkApplyTrtDetails As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtTrtDetails As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents chkApplyTrtNotes As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtTrtNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents pnlButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
End Class
