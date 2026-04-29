<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNewSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNewSettings))
        Me.grpCurrentlyUsed = New DevExpress.XtraEditors.GroupControl()
        Me.LabelSize = New DevExpress.XtraEditors.LabelControl()
        Me.Label7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelDb = New DevExpress.XtraEditors.LabelControl()
        Me.Label5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelStart = New DevExpress.XtraEditors.LabelControl()
        Me.Labelang = New DevExpress.XtraEditors.LabelControl()
        Me.Label8 = New DevExpress.XtraEditors.LabelControl()
        Me.Label3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GrpLangSizeStart = New DevExpress.XtraEditors.GroupControl()
        Me.RadioStartFrm = New DevExpress.XtraEditors.RadioGroup()
        Me.RadioSize = New DevExpress.XtraEditors.RadioGroup()
        Me.RadioLang = New DevExpress.XtraEditors.RadioGroup()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.Label1 = New DevExpress.XtraEditors.LabelControl()
        Me.grpStartupPrefs = New DevExpress.XtraEditors.GroupControl()
        Me.dtEndTime = New DevExpress.XtraEditors.DateEdit()
        Me.dtStartTime = New DevExpress.XtraEditors.DateEdit()
        Me.ShortRemind = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.GrpDbase = New DevExpress.XtraEditors.GroupControl()
        Me.txtSrvrPass = New DevExpress.XtraEditors.TextEdit()
        Me.txtHostName = New DevExpress.XtraEditors.TextEdit()
        Me.RadioDbType = New DevExpress.XtraEditors.RadioGroup()
        Me.Label9 = New DevExpress.XtraEditors.LabelControl()
        Me.Label2 = New DevExpress.XtraEditors.LabelControl()
        Me.BtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSaveExit = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDefault = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.grpCurrentlyUsed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCurrentlyUsed.SuspendLayout()
        CType(Me.GrpLangSizeStart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpLangSizeStart.SuspendLayout()
        CType(Me.RadioStartFrm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpStartupPrefs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpStartupPrefs.SuspendLayout()
        CType(Me.dtEndTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ShortRemind.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpDbase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpDbase.SuspendLayout()
        CType(Me.txtSrvrPass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHostName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioDbType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpCurrentlyUsed
        '
        Me.grpCurrentlyUsed.AppearanceCaption.Font = CType(resources.GetObject("grpCurrentlyUsed.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpCurrentlyUsed.AppearanceCaption.Options.UseFont = True
        Me.grpCurrentlyUsed.AppearanceCaption.Options.UseTextOptions = True
        Me.grpCurrentlyUsed.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.grpCurrentlyUsed.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.grpCurrentlyUsed.Controls.Add(Me.LabelSize)
        Me.grpCurrentlyUsed.Controls.Add(Me.Label7)
        Me.grpCurrentlyUsed.Controls.Add(Me.LabelDb)
        Me.grpCurrentlyUsed.Controls.Add(Me.Label5)
        Me.grpCurrentlyUsed.Controls.Add(Me.LabelStart)
        Me.grpCurrentlyUsed.Controls.Add(Me.Labelang)
        Me.grpCurrentlyUsed.Controls.Add(Me.Label8)
        Me.grpCurrentlyUsed.Controls.Add(Me.Label3)
        resources.ApplyResources(Me.grpCurrentlyUsed, "grpCurrentlyUsed")
        Me.grpCurrentlyUsed.Name = "grpCurrentlyUsed"
        '
        'LabelSize
        '
        Me.LabelSize.Appearance.Font = CType(resources.GetObject("LabelSize.Appearance.Font"), System.Drawing.Font)
        Me.LabelSize.Appearance.ForeColor = System.Drawing.Color.Fuchsia
        Me.LabelSize.Appearance.Options.UseFont = True
        Me.LabelSize.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelSize, "LabelSize")
        Me.LabelSize.Name = "LabelSize"
        '
        'Label7
        '
        Me.Label7.Appearance.Font = CType(resources.GetObject("Label7.Appearance.Font"), System.Drawing.Font)
        Me.Label7.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Appearance.Options.UseFont = True
        Me.Label7.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'LabelDb
        '
        Me.LabelDb.Appearance.Font = CType(resources.GetObject("LabelDb.Appearance.Font"), System.Drawing.Font)
        Me.LabelDb.Appearance.ForeColor = System.Drawing.Color.Fuchsia
        Me.LabelDb.Appearance.Options.UseFont = True
        Me.LabelDb.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelDb, "LabelDb")
        Me.LabelDb.Name = "LabelDb"
        '
        'Label5
        '
        Me.Label5.Appearance.Font = CType(resources.GetObject("Label5.Appearance.Font"), System.Drawing.Font)
        Me.Label5.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Appearance.Options.UseFont = True
        Me.Label5.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'LabelStart
        '
        Me.LabelStart.Appearance.Font = CType(resources.GetObject("LabelStart.Appearance.Font"), System.Drawing.Font)
        Me.LabelStart.Appearance.ForeColor = System.Drawing.Color.Fuchsia
        Me.LabelStart.Appearance.Options.UseFont = True
        Me.LabelStart.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelStart, "LabelStart")
        Me.LabelStart.Name = "LabelStart"
        '
        'Labelang
        '
        Me.Labelang.Appearance.Font = CType(resources.GetObject("Labelang.Appearance.Font"), System.Drawing.Font)
        Me.Labelang.Appearance.ForeColor = System.Drawing.Color.Fuchsia
        Me.Labelang.Appearance.Options.UseFont = True
        Me.Labelang.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.Labelang, "Labelang")
        Me.Labelang.Name = "Labelang"
        '
        'Label8
        '
        Me.Label8.Appearance.Font = CType(resources.GetObject("Label8.Appearance.Font"), System.Drawing.Font)
        Me.Label8.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Appearance.Options.UseFont = True
        Me.Label8.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'Label3
        '
        Me.Label3.Appearance.Font = CType(resources.GetObject("Label3.Appearance.Font"), System.Drawing.Font)
        Me.Label3.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Appearance.Options.UseFont = True
        Me.Label3.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'GrpLangSizeStart
        '
        Me.GrpLangSizeStart.AppearanceCaption.Font = CType(resources.GetObject("GrpLangSizeStart.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GrpLangSizeStart.AppearanceCaption.Options.UseFont = True
        Me.GrpLangSizeStart.AppearanceCaption.Options.UseTextOptions = True
        Me.GrpLangSizeStart.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GrpLangSizeStart.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GrpLangSizeStart.Controls.Add(Me.RadioStartFrm)
        Me.GrpLangSizeStart.Controls.Add(Me.RadioSize)
        Me.GrpLangSizeStart.Controls.Add(Me.RadioLang)
        Me.GrpLangSizeStart.Controls.Add(Me.LabelControl6)
        Me.GrpLangSizeStart.Controls.Add(Me.LabelControl5)
        Me.GrpLangSizeStart.Controls.Add(Me.LabelControl4)
        Me.GrpLangSizeStart.Controls.Add(Me.Label1)
        resources.ApplyResources(Me.GrpLangSizeStart, "GrpLangSizeStart")
        Me.GrpLangSizeStart.Name = "GrpLangSizeStart"
        '
        'RadioStartFrm
        '
        Me.RadioStartFrm.EnterMoveNextControl = True
        resources.ApplyResources(Me.RadioStartFrm, "RadioStartFrm")
        Me.RadioStartFrm.Name = "RadioStartFrm"
        Me.RadioStartFrm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioStartFrm.Properties.Appearance.Font = CType(resources.GetObject("RadioStartFrm.Properties.Appearance.Font"), System.Drawing.Font)
        Me.RadioStartFrm.Properties.Appearance.Options.UseBackColor = True
        Me.RadioStartFrm.Properties.Appearance.Options.UseFont = True
        Me.RadioStartFrm.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioStartFrm.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioStartFrm.Properties.Items"), Object), resources.GetString("RadioStartFrm.Properties.Items1"), CType(resources.GetObject("RadioStartFrm.Properties.Items2"), Boolean), CType(resources.GetObject("RadioStartFrm.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioStartFrm.Properties.Items4"), Object), resources.GetString("RadioStartFrm.Properties.Items5"))})
        '
        'RadioSize
        '
        Me.RadioSize.EnterMoveNextControl = True
        resources.ApplyResources(Me.RadioSize, "RadioSize")
        Me.RadioSize.Name = "RadioSize"
        Me.RadioSize.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioSize.Properties.Appearance.Font = CType(resources.GetObject("RadioSize.Properties.Appearance.Font"), System.Drawing.Font)
        Me.RadioSize.Properties.Appearance.Options.UseBackColor = True
        Me.RadioSize.Properties.Appearance.Options.UseFont = True
        Me.RadioSize.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioSize.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioSize.Properties.Items"), Object), resources.GetString("RadioSize.Properties.Items1"), CType(resources.GetObject("RadioSize.Properties.Items2"), Boolean), CType(resources.GetObject("RadioSize.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioSize.Properties.Items4"), Object), resources.GetString("RadioSize.Properties.Items5"))})
        '
        'RadioLang
        '
        Me.RadioLang.EnterMoveNextControl = True
        resources.ApplyResources(Me.RadioLang, "RadioLang")
        Me.RadioLang.Name = "RadioLang"
        Me.RadioLang.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioLang.Properties.Appearance.Font = CType(resources.GetObject("RadioLang.Properties.Appearance.Font"), System.Drawing.Font)
        Me.RadioLang.Properties.Appearance.Options.UseBackColor = True
        Me.RadioLang.Properties.Appearance.Options.UseFont = True
        Me.RadioLang.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioLang.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLang.Properties.Items"), Object), resources.GetString("RadioLang.Properties.Items1"), CType(resources.GetObject("RadioLang.Properties.Items2"), Boolean), CType(resources.GetObject("RadioLang.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLang.Properties.Items4"), Object), resources.GetString("RadioLang.Properties.Items5"))})
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.ForeColor = System.Drawing.Color.HotPink
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Name = "LabelControl6"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.ForeColor = System.Drawing.Color.DarkViolet
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Name = "LabelControl5"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.Purple
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Name = "LabelControl4"
        '
        'Label1
        '
        Me.Label1.Appearance.Font = CType(resources.GetObject("Label1.Appearance.Font"), System.Drawing.Font)
        Me.Label1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Appearance.Options.UseFont = True
        Me.Label1.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'grpStartupPrefs
        '
        Me.grpStartupPrefs.AppearanceCaption.Font = CType(resources.GetObject("grpStartupPrefs.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpStartupPrefs.AppearanceCaption.Options.UseFont = True
        Me.grpStartupPrefs.AppearanceCaption.Options.UseTextOptions = True
        Me.grpStartupPrefs.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.grpStartupPrefs.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.grpStartupPrefs.Controls.Add(Me.dtEndTime)
        Me.grpStartupPrefs.Controls.Add(Me.dtStartTime)
        Me.grpStartupPrefs.Controls.Add(Me.ShortRemind)
        Me.grpStartupPrefs.Controls.Add(Me.LabelControl2)
        Me.grpStartupPrefs.Controls.Add(Me.LabelControl1)
        Me.grpStartupPrefs.Controls.Add(Me.LabelControl3)
        resources.ApplyResources(Me.grpStartupPrefs, "grpStartupPrefs")
        Me.grpStartupPrefs.Name = "grpStartupPrefs"
        '
        'dtEndTime
        '
        resources.ApplyResources(Me.dtEndTime, "dtEndTime")
        Me.dtEndTime.Name = "dtEndTime"
        Me.dtEndTime.Properties.Appearance.Font = CType(resources.GetObject("dtEndTime.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtEndTime.Properties.Appearance.Options.UseFont = True
        Me.dtEndTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtEndTime.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtEndTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtEndTime.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtEndTime.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtEndTime.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "T"
        Me.dtEndTime.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtEndTime.Properties.CalendarTimeProperties.EditFormat.FormatString = "T"
        Me.dtEndTime.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtEndTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.dtEndTime.Properties.DisplayFormat.FormatString = "t"
        Me.dtEndTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtEndTime.Properties.EditFormat.FormatString = "t"
        Me.dtEndTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtEndTime.Properties.MaskSettings.Set("mask", "t")
        Me.dtEndTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        '
        'dtStartTime
        '
        resources.ApplyResources(Me.dtStartTime, "dtStartTime")
        Me.dtStartTime.Name = "dtStartTime"
        Me.dtStartTime.Properties.Appearance.Font = CType(resources.GetObject("dtStartTime.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtStartTime.Properties.Appearance.Options.UseFont = True
        Me.dtStartTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtStartTime.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtStartTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtStartTime.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtStartTime.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtStartTime.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "T"
        Me.dtStartTime.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtStartTime.Properties.CalendarTimeProperties.EditFormat.FormatString = "T"
        Me.dtStartTime.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtStartTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.dtStartTime.Properties.DisplayFormat.FormatString = "t"
        Me.dtStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtStartTime.Properties.EditFormat.FormatString = "t"
        Me.dtStartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtStartTime.Properties.MaskSettings.Set("mask", "t")
        Me.dtStartTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        '
        'ShortRemind
        '
        resources.ApplyResources(Me.ShortRemind, "ShortRemind")
        Me.ShortRemind.Name = "ShortRemind"
        Me.ShortRemind.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("ShortRemind.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.ShortRemind.Properties.MaxValue = New Decimal(New Integer() {5, 0, 0, 0})
        Me.ShortRemind.Properties.MinValue = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Name = "LabelControl2"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Name = "LabelControl3"
        '
        'GrpDbase
        '
        Me.GrpDbase.AppearanceCaption.Font = CType(resources.GetObject("GrpDbase.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GrpDbase.AppearanceCaption.Options.UseFont = True
        Me.GrpDbase.AppearanceCaption.Options.UseTextOptions = True
        Me.GrpDbase.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GrpDbase.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GrpDbase.Controls.Add(Me.txtSrvrPass)
        Me.GrpDbase.Controls.Add(Me.txtHostName)
        Me.GrpDbase.Controls.Add(Me.RadioDbType)
        Me.GrpDbase.Controls.Add(Me.Label9)
        Me.GrpDbase.Controls.Add(Me.Label2)
        resources.ApplyResources(Me.GrpDbase, "GrpDbase")
        Me.GrpDbase.Name = "GrpDbase"
        '
        'txtSrvrPass
        '
        resources.ApplyResources(Me.txtSrvrPass, "txtSrvrPass")
        Me.txtSrvrPass.Name = "txtSrvrPass"
        '
        'txtHostName
        '
        resources.ApplyResources(Me.txtHostName, "txtHostName")
        Me.txtHostName.Name = "txtHostName"
        '
        'RadioDbType
        '
        Me.RadioDbType.EnterMoveNextControl = True
        resources.ApplyResources(Me.RadioDbType, "RadioDbType")
        Me.RadioDbType.Name = "RadioDbType"
        Me.RadioDbType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioDbType.Properties.Appearance.Font = CType(resources.GetObject("RadioDbType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.RadioDbType.Properties.Appearance.Options.UseBackColor = True
        Me.RadioDbType.Properties.Appearance.Options.UseFont = True
        Me.RadioDbType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioDbType.Properties.Columns = 2
        Me.RadioDbType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioDbType.Properties.Items"), Object), resources.GetString("RadioDbType.Properties.Items1"), CType(resources.GetObject("RadioDbType.Properties.Items2"), Boolean), CType(resources.GetObject("RadioDbType.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioDbType.Properties.Items4"), Object), resources.GetString("RadioDbType.Properties.Items5")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioDbType.Properties.Items6"), Object), resources.GetString("RadioDbType.Properties.Items7")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioDbType.Properties.Items8"), Object), resources.GetString("RadioDbType.Properties.Items9"))})
        '
        'Label9
        '
        Me.Label9.Appearance.Font = CType(resources.GetObject("Label9.Appearance.Font"), System.Drawing.Font)
        Me.Label9.Appearance.ForeColor = System.Drawing.Color.Red
        Me.Label9.Appearance.Options.UseFont = True
        Me.Label9.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'Label2
        '
        Me.Label2.Appearance.Font = CType(resources.GetObject("Label2.Appearance.Font"), System.Drawing.Font)
        Me.Label2.Appearance.ForeColor = System.Drawing.Color.Red
        Me.Label2.Appearance.Options.UseFont = True
        Me.Label2.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'BtnExit
        '
        Me.BtnExit.Appearance.Font = CType(resources.GetObject("BtnExit.Appearance.Font"), System.Drawing.Font)
        Me.BtnExit.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnExit, "BtnExit")
        Me.BtnExit.Name = "BtnExit"
        '
        'BtnSaveExit
        '
        Me.BtnSaveExit.Appearance.Font = CType(resources.GetObject("BtnSaveExit.Appearance.Font"), System.Drawing.Font)
        Me.BtnSaveExit.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnSaveExit, "BtnSaveExit")
        Me.BtnSaveExit.Name = "BtnSaveExit"
        '
        'BtnDefault
        '
        Me.BtnDefault.Appearance.Font = CType(resources.GetObject("BtnDefault.Appearance.Font"), System.Drawing.Font)
        Me.BtnDefault.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnDefault, "BtnDefault")
        Me.BtnDefault.Name = "BtnDefault"
        '
        'FrmNewSettings
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnSaveExit)
        Me.Controls.Add(Me.BtnDefault)
        Me.Controls.Add(Me.GrpDbase)
        Me.Controls.Add(Me.grpStartupPrefs)
        Me.Controls.Add(Me.GrpLangSizeStart)
        Me.Controls.Add(Me.grpCurrentlyUsed)
        Me.Name = "FrmNewSettings"
        CType(Me.grpCurrentlyUsed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCurrentlyUsed.ResumeLayout(False)
        Me.grpCurrentlyUsed.PerformLayout()
        CType(Me.GrpLangSizeStart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpLangSizeStart.ResumeLayout(False)
        Me.GrpLangSizeStart.PerformLayout()
        CType(Me.RadioStartFrm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpStartupPrefs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpStartupPrefs.ResumeLayout(False)
        Me.grpStartupPrefs.PerformLayout()
        CType(Me.dtEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEndTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ShortRemind.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpDbase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpDbase.ResumeLayout(False)
        Me.GrpDbase.PerformLayout()
        CType(Me.txtSrvrPass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHostName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioDbType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpCurrentlyUsed As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpLangSizeStart As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpStartupPrefs As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpDbase As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelSize As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelDb As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelStart As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Labelang As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Label2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RadioLang As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents RadioStartFrm As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents RadioSize As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents RadioDbType As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents ShortRemind As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents BtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSaveExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDefault As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtSrvrPass As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtHostName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtStartTime As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtEndTime As DevExpress.XtraEditors.DateEdit
End Class
