<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ShowDiagDetails
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ShowDiagDetails))
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule2 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue2 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
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
        Me.GridViewTrts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDiagID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTrtDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colToothNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupGrid = New DevExpress.XtraEditors.GroupControl()
        Me.DiagDetBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
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
        CType(Me.GridViewTrts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupGrid.SuspendLayout()
        CType(Me.DiagDetBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        CType(Me.grpControls, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpControls.SuspendLayout()
        Me.SuspendLayout()
        '
        'colToothName
        '
        resources.ApplyResources(Me.colToothName, "colToothName")
        Me.colToothName.FieldName = "ToothName"
        Me.colToothName.ImageOptions.ImageKey = resources.GetString("colToothName.ImageOptions.ImageKey")
        Me.colToothName.Name = "colToothName"
        '
        'txtDiagNotes
        '
        resources.ApplyResources(Me.txtDiagNotes, "txtDiagNotes")
        Me.txtDiagNotes.EnterMoveNextControl = True
        Me.txtDiagNotes.Name = "txtDiagNotes"
        '
        'txtDiagDetails
        '
        resources.ApplyResources(Me.txtDiagDetails, "txtDiagDetails")
        Me.txtDiagDetails.EnterMoveNextControl = True
        Me.txtDiagDetails.Name = "txtDiagDetails"
        '
        'LabelControl20
        '
        resources.ApplyResources(Me.LabelControl20, "LabelControl20")
        Me.LabelControl20.Appearance.Font = CType(resources.GetObject("LabelControl20.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl20.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabelControl20.Appearance.Options.UseFont = True
        Me.LabelControl20.Appearance.Options.UseForeColor = True
        Me.LabelControl20.Name = "LabelControl20"
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'LabelControl10
        '
        resources.ApplyResources(Me.LabelControl10, "LabelControl10")
        Me.LabelControl10.Appearance.Font = CType(resources.GetObject("LabelControl10.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl10.Appearance.Options.UseFont = True
        Me.LabelControl10.Name = "LabelControl10"
        '
        'txtTrtPlan
        '
        resources.ApplyResources(Me.txtTrtPlan, "txtTrtPlan")
        Me.txtTrtPlan.EnterMoveNextControl = True
        Me.txtTrtPlan.Name = "txtTrtPlan"
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'txtDiagAgree
        '
        resources.ApplyResources(Me.txtDiagAgree, "txtDiagAgree")
        Me.txtDiagAgree.EnterMoveNextControl = True
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
        resources.ApplyResources(Me.LabelControl23, "LabelControl23")
        Me.LabelControl23.Appearance.Font = CType(resources.GetObject("LabelControl23.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl23.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl23.Appearance.Options.UseFont = True
        Me.LabelControl23.Appearance.Options.UseForeColor = True
        Me.LabelControl23.Name = "LabelControl23"
        '
        'LabelControl6
        '
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Appearance.Options.UseForeColor = True
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
        resources.ApplyResources(Me.LabelControl14, "LabelControl14")
        Me.LabelControl14.Appearance.Font = CType(resources.GetObject("LabelControl14.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl14.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabelControl14.Appearance.Options.UseFont = True
        Me.LabelControl14.Appearance.Options.UseForeColor = True
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
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Appearance.Options.UseForeColor = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.ForeColor = System.Drawing.Color.Red
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.Appearance.Options.UseForeColor = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.ImageOptions.ImageKey = resources.GetString("btnCancel.ImageOptions.ImageKey")
        Me.btnCancel.Name = "btnCancel"
        '
        'btnSaveTrt
        '
        resources.ApplyResources(Me.btnSaveTrt, "btnSaveTrt")
        Me.btnSaveTrt.Appearance.Font = CType(resources.GetObject("btnSaveTrt.Appearance.Font"), System.Drawing.Font)
        Me.btnSaveTrt.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.btnSaveTrt.Appearance.Options.UseFont = True
        Me.btnSaveTrt.Appearance.Options.UseForeColor = True
        Me.btnSaveTrt.ImageOptions.ImageKey = resources.GetString("btnSaveTrt.ImageOptions.ImageKey")
        Me.btnSaveTrt.Name = "btnSaveTrt"
        '
        'Patient_DiagsGrid
        '
        resources.ApplyResources(Me.Patient_DiagsGrid, "Patient_DiagsGrid")
        Me.Patient_DiagsGrid.EmbeddedNavigator.AccessibleDescription = resources.GetString("Patient_DiagsGrid.EmbeddedNavigator.AccessibleDescription")
        Me.Patient_DiagsGrid.EmbeddedNavigator.AccessibleName = resources.GetString("Patient_DiagsGrid.EmbeddedNavigator.AccessibleName")
        Me.Patient_DiagsGrid.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("Patient_DiagsGrid.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.Patient_DiagsGrid.EmbeddedNavigator.Anchor = CType(resources.GetObject("Patient_DiagsGrid.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.Patient_DiagsGrid.EmbeddedNavigator.Appearance.Font = CType(resources.GetObject("Patient_DiagsGrid.EmbeddedNavigator.Appearance.Font"), System.Drawing.Font)
        Me.Patient_DiagsGrid.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.Patient_DiagsGrid.EmbeddedNavigator.AutoSize = CType(resources.GetObject("Patient_DiagsGrid.EmbeddedNavigator.AutoSize"), Boolean)
        Me.Patient_DiagsGrid.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("Patient_DiagsGrid.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.Patient_DiagsGrid.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("Patient_DiagsGrid.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.Append.Hint = resources.GetString("Patient_DiagsGrid.EmbeddedNavigator.Buttons.Append.Hint")
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.CancelEdit.Hint = resources.GetString("Patient_DiagsGrid.EmbeddedNavigator.Buttons.CancelEdit.Hint")
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.CancelEdit.Tag = "Cancel"
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.Edit.Hint = resources.GetString("Patient_DiagsGrid.EmbeddedNavigator.Buttons.Edit.Hint")
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.Edit.Tag = "Edit"
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.EndEdit.Hint = resources.GetString("Patient_DiagsGrid.EmbeddedNavigator.Buttons.EndEdit.Hint")
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.EndEdit.Tag = "EndEdit"
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.Remove.Hint = resources.GetString("Patient_DiagsGrid.EmbeddedNavigator.Buttons.Remove.Hint")
        Me.Patient_DiagsGrid.EmbeddedNavigator.Buttons.Remove.Tag = "Remove"
        Me.Patient_DiagsGrid.EmbeddedNavigator.ImeMode = CType(resources.GetObject("Patient_DiagsGrid.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.Patient_DiagsGrid.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("Patient_DiagsGrid.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.Patient_DiagsGrid.EmbeddedNavigator.TextLocation = CType(resources.GetObject("Patient_DiagsGrid.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.Patient_DiagsGrid.EmbeddedNavigator.ToolTip = resources.GetString("Patient_DiagsGrid.EmbeddedNavigator.ToolTip")
        Me.Patient_DiagsGrid.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("Patient_DiagsGrid.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.Patient_DiagsGrid.EmbeddedNavigator.ToolTipTitle = resources.GetString("Patient_DiagsGrid.EmbeddedNavigator.ToolTipTitle")
        Me.Patient_DiagsGrid.MainView = Me.GridViewTrts
        Me.Patient_DiagsGrid.Name = "Patient_DiagsGrid"
        Me.Patient_DiagsGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewTrts})
        '
        'GridViewTrts
        '
        resources.ApplyResources(Me.GridViewTrts, "GridViewTrts")
        Me.GridViewTrts.Appearance.FooterPanel.FontStyleDelta = CType(resources.GetObject("GridViewTrts.Appearance.FooterPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridViewTrts.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue
        Me.GridViewTrts.Appearance.FooterPanel.Options.UseFont = True
        Me.GridViewTrts.Appearance.FooterPanel.Options.UseForeColor = True
        Me.GridViewTrts.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.GridViewTrts.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewTrts.Appearance.GroupFooter.FontStyleDelta = CType(resources.GetObject("GridViewTrts.Appearance.GroupFooter.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridViewTrts.Appearance.GroupFooter.Options.UseFont = True
        Me.GridViewTrts.Appearance.GroupFooter.Options.UseTextOptions = True
        Me.GridViewTrts.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridViewTrts.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridViewTrts.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridViewTrts.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewTrts.Appearance.Row.Font = CType(resources.GetObject("GridViewTrts.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridViewTrts.Appearance.Row.Options.UseFont = True
        Me.GridViewTrts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colDiagID, Me.colPatientID, Me.colDetail, Me.colTrtDate, Me.colToothName, Me.colToothNum})
        GridFormatRule1.Column = Me.colToothName
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        FormatConditionRuleValue1.Appearance.Font = CType(resources.GetObject("resource.Font"), System.Drawing.Font)
        FormatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.Blue
        FormatConditionRuleValue1.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue1.Appearance.Options.UseFont = True
        FormatConditionRuleValue1.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue1.Expression = "[TrtValue] > 0"
        GridFormatRule1.Rule = FormatConditionRuleValue1
        GridFormatRule2.Column = Me.colToothName
        GridFormatRule2.Name = "Format1"
        FormatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        FormatConditionRuleValue2.Appearance.Font = CType(resources.GetObject("resource.Font1"), System.Drawing.Font)
        FormatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.Red
        FormatConditionRuleValue2.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue2.Appearance.Options.UseFont = True
        FormatConditionRuleValue2.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Less
        FormatConditionRuleValue2.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        GridFormatRule2.Rule = FormatConditionRuleValue2
        Me.GridViewTrts.FormatRules.Add(GridFormatRule1)
        Me.GridViewTrts.FormatRules.Add(GridFormatRule2)
        Me.GridViewTrts.GridControl = Me.Patient_DiagsGrid
        Me.GridViewTrts.Name = "GridViewTrts"
        Me.GridViewTrts.OptionsDetail.EnableMasterViewMode = False
        Me.GridViewTrts.OptionsView.AutoCalcPreviewLineCount = True
        Me.GridViewTrts.OptionsView.EnableAppearanceEvenRow = True
        Me.GridViewTrts.OptionsView.ShowFooter = True
        '
        'colRowNum
        '
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.ImageOptions.ImageKey = resources.GetString("colRowNum.ImageOptions.ImageKey")
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colDiagID
        '
        resources.ApplyResources(Me.colDiagID, "colDiagID")
        Me.colDiagID.FieldName = "DiagID"
        Me.colDiagID.ImageOptions.ImageKey = resources.GetString("colDiagID.ImageOptions.ImageKey")
        Me.colDiagID.Name = "colDiagID"
        '
        'colPatientID
        '
        resources.ApplyResources(Me.colPatientID, "colPatientID")
        Me.colPatientID.FieldName = "PatientID"
        Me.colPatientID.ImageOptions.ImageKey = resources.GetString("colPatientID.ImageOptions.ImageKey")
        Me.colPatientID.Name = "colPatientID"
        '
        'colDetail
        '
        resources.ApplyResources(Me.colDetail, "colDetail")
        Me.colDetail.FieldName = "Treat"
        Me.colDetail.ImageOptions.ImageKey = resources.GetString("colDetail.ImageOptions.ImageKey")
        Me.colDetail.Name = "colDetail"
        '
        'colTrtDate
        '
        resources.ApplyResources(Me.colTrtDate, "colTrtDate")
        Me.colTrtDate.FieldName = "TreatDate"
        Me.colTrtDate.ImageOptions.ImageKey = resources.GetString("colTrtDate.ImageOptions.ImageKey")
        Me.colTrtDate.Name = "colTrtDate"
        '
        'colToothNum
        '
        resources.ApplyResources(Me.colToothNum, "colToothNum")
        Me.colToothNum.FieldName = "ToothNum"
        Me.colToothNum.ImageOptions.ImageKey = resources.GetString("colToothNum.ImageOptions.ImageKey")
        Me.colToothNum.Name = "colToothNum"
        '
        'GroupGrid
        '
        resources.ApplyResources(Me.GroupGrid, "GroupGrid")
        Me.GroupGrid.AppearanceCaption.Font = CType(resources.GetObject("GroupGrid.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupGrid.AppearanceCaption.Options.UseFont = True
        Me.GroupGrid.Controls.Add(Me.Patient_DiagsGrid)
        Me.GroupGrid.Name = "GroupGrid"
        '
        'DiagDetBS
        '
        '
        'BindingNavigator1
        '
        resources.ApplyResources(Me.BindingNavigator1, "BindingNavigator1")
        Me.BindingNavigator1.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.BindingNavigator1.BindingSource = Me.DiagDetBS
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem})
        Me.BindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        '
        'BindingNavigatorAddNewItem
        '
        resources.ApplyResources(Me.BindingNavigatorAddNewItem, "BindingNavigatorAddNewItem")
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        '
        'BindingNavigatorCountItem
        '
        resources.ApplyResources(Me.BindingNavigatorCountItem, "BindingNavigatorCountItem")
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        '
        'BindingNavigatorDeleteItem
        '
        resources.ApplyResources(Me.BindingNavigatorDeleteItem, "BindingNavigatorDeleteItem")
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        '
        'BindingNavigatorMoveFirstItem
        '
        resources.ApplyResources(Me.BindingNavigatorMoveFirstItem, "BindingNavigatorMoveFirstItem")
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        '
        'BindingNavigatorMovePreviousItem
        '
        resources.ApplyResources(Me.BindingNavigatorMovePreviousItem, "BindingNavigatorMovePreviousItem")
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        '
        'BindingNavigatorSeparator
        '
        resources.ApplyResources(Me.BindingNavigatorSeparator, "BindingNavigatorSeparator")
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        '
        'BindingNavigatorPositionItem
        '
        resources.ApplyResources(Me.BindingNavigatorPositionItem, "BindingNavigatorPositionItem")
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        '
        'BindingNavigatorSeparator1
        '
        resources.ApplyResources(Me.BindingNavigatorSeparator1, "BindingNavigatorSeparator1")
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        '
        'BindingNavigatorMoveNextItem
        '
        resources.ApplyResources(Me.BindingNavigatorMoveNextItem, "BindingNavigatorMoveNextItem")
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        '
        'BindingNavigatorMoveLastItem
        '
        resources.ApplyResources(Me.BindingNavigatorMoveLastItem, "BindingNavigatorMoveLastItem")
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        '
        'BindingNavigatorSeparator2
        '
        resources.ApplyResources(Me.BindingNavigatorSeparator2, "BindingNavigatorSeparator2")
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        '
        'btnDelete
        '
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.Appearance.Font = CType(resources.GetObject("btnDelete.Appearance.Font"), System.Drawing.Font)
        Me.btnDelete.Appearance.ForeColor = System.Drawing.Color.Fuchsia
        Me.btnDelete.Appearance.Options.UseFont = True
        Me.btnDelete.Appearance.Options.UseForeColor = True
        Me.btnDelete.ImageOptions.ImageKey = resources.GetString("btnDelete.ImageOptions.ImageKey")
        Me.btnDelete.Name = "btnDelete"
        '
        'grpControls
        '
        resources.ApplyResources(Me.grpControls, "grpControls")
        Me.grpControls.AppearanceCaption.Font = CType(resources.GetObject("grpControls.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpControls.AppearanceCaption.Options.UseFont = True
        Me.grpControls.Controls.Add(Me.btnCancel)
        Me.grpControls.Controls.Add(Me.BindingNavigator1)
        Me.grpControls.Controls.Add(Me.btnDelete)
        Me.grpControls.Controls.Add(Me.btnSaveTrt)
        Me.grpControls.Controls.Add(Me.dtDiagDate)
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
        Me.grpControls.Name = "grpControls"
        '
        'ShowDiagDetails
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpControls)
        Me.Controls.Add(Me.GroupGrid)
        Me.Name = "ShowDiagDetails"
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
        CType(Me.GridViewTrts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupGrid.ResumeLayout(False)
        CType(Me.DiagDetBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
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
    Friend WithEvents GridViewTrts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiagID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrtDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colToothName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colToothNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupGrid As DevExpress.XtraEditors.GroupControl
    Friend WithEvents DiagDetBS As BindingSource
    Friend WithEvents BindingNavigator1 As BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As ToolStripSeparator
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpControls As DevExpress.XtraEditors.GroupControl
End Class
