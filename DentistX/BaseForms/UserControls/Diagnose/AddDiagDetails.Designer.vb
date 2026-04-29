<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AddDiagDetails
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddDiagDetails))
        Dim GridFormatRule3 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue3 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule4 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue4 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Me.colToothName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtDiagNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.txtDiagDetails = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl20 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.txtTrtPlan = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtDiagAgree = New DevExpress.XtraEditors.MemoEdit()
        Me.txtPayValue = New DevExpress.XtraEditors.TextEdit()
        Me.txtTrtValue = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl23 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.dtTrtToStart = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.dtDiagDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSaveTrt = New DevExpress.XtraEditors.SimpleButton()
        Me.Patient_DiagsGrid = New DevExpress.XtraGrid.GridControl()
        Me.GridViewDiags = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDiagID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTrtDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colToothNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupGrid = New DevExpress.XtraEditors.GroupControl()
        Me.slctCheck = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.chkAddToVisits = New DevExpress.XtraEditors.CheckEdit()
        Me.btnSelectNone = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSelectAll = New DevExpress.XtraEditors.SimpleButton()
        Me.grpControls = New DevExpress.XtraEditors.GroupControl()
        CType(Me.txtDiagNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiagDetails.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTrtPlan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiagAgree.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTrtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTrtToStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTrtToStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDiagDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDiagDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Patient_DiagsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewDiags, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupGrid.SuspendLayout()
        CType(Me.slctCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAddToVisits.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpControls, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControls.SuspendLayout()
        Me.SuspendLayout()
        '
        'colToothName
        '
        resources.ApplyResources(Me.colToothName, "colToothName")
        Me.colToothName.FieldName = "ToothName"
        Me.colToothName.Name = "colToothName"
        '
        'txtDiagNotes
        '
        Me.txtDiagNotes.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtDiagNotes, "txtDiagNotes")
        Me.txtDiagNotes.Name = "txtDiagNotes"
        '
        'txtDiagDetails
        '
        Me.txtDiagDetails.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtDiagDetails, "txtDiagDetails")
        Me.txtDiagDetails.Name = "txtDiagDetails"
        '
        'LabelControl20
        '
        Me.LabelControl20.Appearance.Font = CType(resources.GetObject("LabelControl20.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl20.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabelControl20.Appearance.Options.UseFont = True
        Me.LabelControl20.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl20, "LabelControl20")
        Me.LabelControl20.Name = "LabelControl20"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = CType(resources.GetObject("LabelControl10.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl10.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl10, "LabelControl10")
        Me.LabelControl10.Name = "LabelControl10"
        '
        'txtTrtPlan
        '
        Me.txtTrtPlan.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtTrtPlan, "txtTrtPlan")
        Me.txtTrtPlan.Name = "txtTrtPlan"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Name = "LabelControl2"
        '
        'txtDiagAgree
        '
        Me.txtDiagAgree.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtDiagAgree, "txtDiagAgree")
        Me.txtDiagAgree.Name = "txtDiagAgree"
        '
        'txtPayValue
        '
        resources.ApplyResources(Me.txtPayValue, "txtPayValue")
        Me.txtPayValue.EnterMoveNextControl = True
        Me.txtPayValue.Name = "txtPayValue"
        Me.txtPayValue.Properties.Appearance.Font = CType(resources.GetObject("txtPayValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtPayValue.Properties.Appearance.Options.UseFont = True
        Me.txtPayValue.Properties.DisplayFormat.FormatString = "N"
        Me.txtPayValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtPayValue.Properties.EditFormat.FormatString = "N"
        Me.txtPayValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtPayValue.Properties.NullText = resources.GetString("txtPayValue.Properties.NullText")
        Me.txtPayValue.Properties.NullValuePrompt = resources.GetString("txtPayValue.Properties.NullValuePrompt")
        Me.txtPayValue.Properties.TextPadding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.txtPayValue.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.txtPayValue.Properties.UseMaskAsDisplayFormat = CType(resources.GetObject("txtPayValue.Properties.UseMaskAsDisplayFormat"), Boolean)
        '
        'txtTrtValue
        '
        resources.ApplyResources(Me.txtTrtValue, "txtTrtValue")
        Me.txtTrtValue.EnterMoveNextControl = True
        Me.txtTrtValue.Name = "txtTrtValue"
        Me.txtTrtValue.Properties.Appearance.Font = CType(resources.GetObject("txtTrtValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtTrtValue.Properties.Appearance.Options.UseFont = True
        Me.txtTrtValue.Properties.NullText = resources.GetString("txtTrtValue.Properties.NullText")
        Me.txtTrtValue.Properties.NullValuePrompt = resources.GetString("txtTrtValue.Properties.NullValuePrompt")
        Me.txtTrtValue.Properties.TextPadding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.txtTrtValue.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.txtTrtValue.Properties.UseMaskAsDisplayFormat = CType(resources.GetObject("txtTrtValue.Properties.UseMaskAsDisplayFormat"), Boolean)
        '
        'LabelControl23
        '
        Me.LabelControl23.Appearance.Font = CType(resources.GetObject("LabelControl23.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl23.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl23.Appearance.Options.UseFont = True
        Me.LabelControl23.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl23, "LabelControl23")
        Me.LabelControl23.Name = "LabelControl23"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Name = "LabelControl6"
        '
        'dtTrtToStart
        '
        resources.ApplyResources(Me.dtTrtToStart, "dtTrtToStart")
        Me.dtTrtToStart.EnterMoveNextControl = True
        Me.dtTrtToStart.Name = "dtTrtToStart"
        Me.dtTrtToStart.Properties.Appearance.Font = CType(resources.GetObject("dtTrtToStart.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtTrtToStart.Properties.Appearance.Options.UseFont = True
        Me.dtTrtToStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTrtToStart.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtTrtToStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTrtToStart.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.Font = CType(resources.GetObject("LabelControl14.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl14.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabelControl14.Appearance.Options.UseFont = True
        Me.LabelControl14.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl14, "LabelControl14")
        Me.LabelControl14.Name = "LabelControl14"
        '
        'dtDiagDate
        '
        resources.ApplyResources(Me.dtDiagDate, "dtDiagDate")
        Me.dtDiagDate.EnterMoveNextControl = True
        Me.dtDiagDate.Name = "dtDiagDate"
        Me.dtDiagDate.Properties.Appearance.Font = CType(resources.GetObject("dtDiagDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtDiagDate.Properties.Appearance.Options.UseFont = True
        Me.dtDiagDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtDiagDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtDiagDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtDiagDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Name = "LabelControl3"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.ForeColor = System.Drawing.Color.Red
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.Appearance.Options.UseForeColor = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Name = "btnCancel"
        '
        'btnSaveTrt
        '
        Me.btnSaveTrt.Appearance.Font = CType(resources.GetObject("btnSaveTrt.Appearance.Font"), System.Drawing.Font)
        Me.btnSaveTrt.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.btnSaveTrt.Appearance.Options.UseFont = True
        Me.btnSaveTrt.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.btnSaveTrt, "btnSaveTrt")
        Me.btnSaveTrt.Name = "btnSaveTrt"
        '
        'Patient_DiagsGrid
        '
        resources.ApplyResources(Me.Patient_DiagsGrid, "Patient_DiagsGrid")
        Me.Patient_DiagsGrid.EmbeddedNavigator.Appearance.Font = CType(resources.GetObject("Patient_DiagsGrid.EmbeddedNavigator.Appearance.Font"), System.Drawing.Font)
        Me.Patient_DiagsGrid.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.CancelEdit.Tag = "Cancel"
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.Edit.Tag = "Edit"
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.EndEdit.Tag = "EndEdit"
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.Remove.Tag = "Remove"
        Me.Patient_DiagsGrid.MainView = Me.GridViewDiags
        Me.Patient_DiagsGrid.Name = "Patient_DiagsGrid"
        Me.Patient_DiagsGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewDiags})
        '
        'GridViewDiags
        '
        Me.GridViewDiags.Appearance.FooterPanel.FontStyleDelta = CType(resources.GetObject("GridViewDiags.Appearance.FooterPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridViewDiags.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue
        Me.GridViewDiags.Appearance.FooterPanel.Options.UseFont = True
        Me.GridViewDiags.Appearance.FooterPanel.Options.UseForeColor = True
        Me.GridViewDiags.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.GridViewDiags.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewDiags.Appearance.GroupFooter.FontStyleDelta = CType(resources.GetObject("GridViewDiags.Appearance.GroupFooter.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridViewDiags.Appearance.GroupFooter.Options.UseFont = True
        Me.GridViewDiags.Appearance.GroupFooter.Options.UseTextOptions = True
        Me.GridViewDiags.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewDiags.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridViewDiags.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridViewDiags.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewDiags.Appearance.Row.Font = CType(resources.GetObject("GridViewDiags.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridViewDiags.Appearance.Row.Options.UseFont = True
        Me.GridViewDiags.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSelect, Me.colRowNum, Me.colDiagID, Me.colPatientID, Me.colDetail, Me.colTrtDate, Me.colToothName, Me.colToothNum, Me.colStatus})
        GridFormatRule3.Column = Me.colToothName
        GridFormatRule3.Name = "Format0"
        FormatConditionRuleValue3.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        FormatConditionRuleValue3.Appearance.Font = CType(resources.GetObject("resource.Font"), System.Drawing.Font)
        FormatConditionRuleValue3.Appearance.ForeColor = System.Drawing.Color.Blue
        FormatConditionRuleValue3.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue3.Appearance.Options.UseFont = True
        FormatConditionRuleValue3.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue3.Expression = "[TrtValue] > 0"
        GridFormatRule3.Rule = FormatConditionRuleValue3
        GridFormatRule4.Column = Me.colToothName
        GridFormatRule4.Name = "Format1"
        FormatConditionRuleValue4.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        FormatConditionRuleValue4.Appearance.Font = CType(resources.GetObject("resource.Font1"), System.Drawing.Font)
        FormatConditionRuleValue4.Appearance.ForeColor = System.Drawing.Color.Red
        FormatConditionRuleValue4.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue4.Appearance.Options.UseFont = True
        FormatConditionRuleValue4.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Less
        FormatConditionRuleValue4.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        GridFormatRule4.Rule = FormatConditionRuleValue4
        Me.GridViewDiags.FormatRules.Add(GridFormatRule3)
        Me.GridViewDiags.FormatRules.Add(GridFormatRule4)
        Me.GridViewDiags.GridControl = Me.Patient_DiagsGrid
        Me.GridViewDiags.Name = "GridViewDiags"
        Me.GridViewDiags.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click
        Me.GridViewDiags.OptionsDetail.EnableMasterViewMode = False
        Me.GridViewDiags.OptionsSelection.MultiSelect = True
        Me.GridViewDiags.OptionsView.AutoCalcPreviewLineCount = True
        Me.GridViewDiags.OptionsView.EnableAppearanceEvenRow = True
        Me.GridViewDiags.OptionsView.ShowFooter = True
        '
        'colSelect
        '
        resources.ApplyResources(Me.colSelect, "colSelect")
        Me.colSelect.FieldName = "Select"
        Me.colSelect.Name = "colSelect"
        Me.colSelect.UnboundDataType = GetType(Boolean)
        '
        'colRowNum
        '
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colDiagID
        '
        Me.colDiagID.FieldName = "DiagID"
        Me.colDiagID.Name = "colDiagID"
        '
        'colPatientID
        '
        Me.colPatientID.FieldName = "PatientID"
        Me.colPatientID.Name = "colPatientID"
        '
        'colDetail
        '
        resources.ApplyResources(Me.colDetail, "colDetail")
        Me.colDetail.FieldName = "Treat"
        Me.colDetail.Name = "colDetail"
        '
        'colTrtDate
        '
        resources.ApplyResources(Me.colTrtDate, "colTrtDate")
        Me.colTrtDate.FieldName = "TreatDate"
        Me.colTrtDate.Name = "colTrtDate"
        '
        'colToothNum
        '
        resources.ApplyResources(Me.colToothNum, "colToothNum")
        Me.colToothNum.FieldName = "ToothNum"
        Me.colToothNum.Name = "colToothNum"
        '
        'colStatus
        '
        resources.ApplyResources(Me.colStatus, "colStatus")
        Me.colStatus.FieldName = "Status"
        Me.colStatus.Name = "colStatus"
        '
        'GroupGrid
        '
        Me.GroupGrid.AppearanceCaption.Font = CType(resources.GetObject("GroupGrid.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupGrid.AppearanceCaption.Options.UseFont = True
        Me.GroupGrid.Controls.Add(Me.Patient_DiagsGrid)
        resources.ApplyResources(Me.GroupGrid, "GroupGrid")
        Me.GroupGrid.Name = "GroupGrid"
        '
        'slctCheck
        '
        resources.ApplyResources(Me.slctCheck, "slctCheck")
        Me.slctCheck.Name = "slctCheck"
        Me.slctCheck.Properties.Appearance.Font = CType(resources.GetObject("slctCheck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.slctCheck.Properties.Appearance.Options.UseFont = True
        Me.slctCheck.Properties.Caption = resources.GetString("slctCheck.Properties.Caption")
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Name = "LabelControl4"
        '
        'chkAddToVisits
        '
        resources.ApplyResources(Me.chkAddToVisits, "chkAddToVisits")
        Me.chkAddToVisits.Name = "chkAddToVisits"
        Me.chkAddToVisits.Properties.Appearance.Font = CType(resources.GetObject("chkAddToVisits.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkAddToVisits.Properties.Appearance.Options.UseFont = True
        Me.chkAddToVisits.Properties.Caption = resources.GetString("chkAddToVisits.Properties.Caption")
        '
        'btnSelectNone
        '
        Me.btnSelectNone.Appearance.Font = CType(resources.GetObject("btnSelectNone.Appearance.Font"), System.Drawing.Font)
        Me.btnSelectNone.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.btnSelectNone.Appearance.Options.UseFont = True
        Me.btnSelectNone.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.btnSelectNone, "btnSelectNone")
        Me.btnSelectNone.Name = "btnSelectNone"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Appearance.Font = CType(resources.GetObject("btnSelectAll.Appearance.Font"), System.Drawing.Font)
        Me.btnSelectAll.Appearance.ForeColor = System.Drawing.Color.RoyalBlue
        Me.btnSelectAll.Appearance.Options.UseFont = True
        Me.btnSelectAll.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.btnSelectAll, "btnSelectAll")
        Me.btnSelectAll.Name = "btnSelectAll"
        '
        'grpControls
        '
        Me.grpControls.AppearanceCaption.Font = CType(resources.GetObject("grpControls.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpControls.AppearanceCaption.Options.UseFont = True
        Me.grpControls.Controls.Add(Me.btnSelectNone)
        Me.grpControls.Controls.Add(Me.btnSelectAll)
        Me.grpControls.Controls.Add(Me.chkAddToVisits)
        Me.grpControls.Controls.Add(Me.slctCheck)
        Me.grpControls.Controls.Add(Me.btnCancel)
        Me.grpControls.Controls.Add(Me.btnSaveTrt)
        Me.grpControls.Controls.Add(Me.dtDiagDate)
        Me.grpControls.Controls.Add(Me.LabelControl4)
        Me.grpControls.Controls.Add(Me.LabelControl3)
        Me.grpControls.Controls.Add(Me.dtTrtToStart)
        Me.grpControls.Controls.Add(Me.LabelControl14)
        Me.grpControls.Controls.Add(Me.txtPayValue)
        Me.grpControls.Controls.Add(Me.txtTrtValue)
        Me.grpControls.Controls.Add(Me.LabelControl23)
        Me.grpControls.Controls.Add(Me.LabelControl6)
        Me.grpControls.Controls.Add(Me.txtDiagNotes)
        Me.grpControls.Controls.Add(Me.txtDiagAgree)
        Me.grpControls.Controls.Add(Me.LabelControl2)
        Me.grpControls.Controls.Add(Me.txtDiagDetails)
        Me.grpControls.Controls.Add(Me.LabelControl20)
        Me.grpControls.Controls.Add(Me.LabelControl1)
        Me.grpControls.Controls.Add(Me.LabelControl10)
        Me.grpControls.Controls.Add(Me.txtTrtPlan)
        resources.ApplyResources(Me.grpControls, "grpControls")
        Me.grpControls.Name = "grpControls"
        '
        'AddDiagDetails
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpControls)
        Me.Controls.Add(Me.GroupGrid)
        Me.Name = "AddDiagDetails"
        CType(Me.txtDiagNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiagDetails.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTrtPlan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiagAgree.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTrtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTrtToStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTrtToStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDiagDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDiagDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Patient_DiagsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewDiags, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupGrid.ResumeLayout(False)
        CType(Me.slctCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAddToVisits.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpControls, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpControls.ResumeLayout(False)
        Me.grpControls.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents txtDiagNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtDiagDetails As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl20 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtTrtPlan As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtDiagAgree As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtPayValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTrtValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl23 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtTrtToStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtDiagDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSaveTrt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Patient_DiagsGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewDiags As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiagID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrtDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colToothName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colToothNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupGrid As DevExpress.XtraEditors.GroupControl
    Friend WithEvents slctCheck As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkAddToVisits As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents colSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnSelectNone As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSelectAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents colStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grpControls As DevExpress.XtraEditors.GroupControl
End Class
