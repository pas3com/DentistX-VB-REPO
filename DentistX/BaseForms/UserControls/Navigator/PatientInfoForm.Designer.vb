<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PatientInfoForm
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
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
        Dim PatientNameLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PatientInfoForm))
        Dim SexLabel As System.Windows.Forms.Label
        Dim AgeLabel As System.Windows.Forms.Label
        Dim PhoneLabel As System.Windows.Forms.Label
        Dim AddressLabel As System.Windows.Forms.Label
        Dim HealthLabel As System.Windows.Forms.Label
        Dim NotesLabel As System.Windows.Forms.Label
        Dim BirthYLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Me.PatientBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grpPatientInfo = New DevExpress.XtraEditors.GroupControl()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.WhatsAppTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DiagCheck = New System.Windows.Forms.CheckBox()
        Me.RadioFemale = New System.Windows.Forms.RadioButton()
        Me.RadioMale = New System.Windows.Forms.RadioButton()
        Me.CboCity = New DentistX.CityCombo()
        Me.CboHealth = New DentistX.HlthCombo()
        Me.LabelAge = New DevExpress.XtraEditors.LabelControl()
        Me.LabelBal = New DevExpress.XtraEditors.LabelControl()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditPat = New DevExpress.XtraEditors.SimpleButton()
        Me.PatientNameTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.PatientIDEdit = New DevExpress.XtraEditors.TextEdit()
        Me.PicBox = New System.Windows.Forms.PictureBox()
        Me.SexTextBox = New DevExpress.XtraEditors.TextEdit()
        Me.AgeSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.PhoneTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.lblPNum = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.AddressTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.HealthTextBox = New DevExpress.XtraEditors.TextEdit()
        Me.TreatCheckBox = New System.Windows.Forms.CheckBox()
        Me.ImplantCheckBox = New System.Windows.Forms.CheckBox()
        Me.MobileCheckBox = New System.Windows.Forms.CheckBox()
        Me.OrthoCheckBox = New System.Windows.Forms.CheckBox()
        Me.StrucCheckBox = New System.Windows.Forms.CheckBox()
        Me.NotesTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.BirthYSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        PatientNameLabel = New System.Windows.Forms.Label()
        SexLabel = New System.Windows.Forms.Label()
        AgeLabel = New System.Windows.Forms.Label()
        PhoneLabel = New System.Windows.Forms.Label()
        AddressLabel = New System.Windows.Forms.Label()
        HealthLabel = New System.Windows.Forms.Label()
        NotesLabel = New System.Windows.Forms.Label()
        BirthYLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        CType(Me.PatientBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpPatientInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPatientInfo.SuspendLayout()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WhatsAppTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientIDEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SexTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AgeSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AddressTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HealthTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NotesTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BirthYSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PatientNameLabel
        '
        resources.ApplyResources(PatientNameLabel, "PatientNameLabel")
        PatientNameLabel.Name = "PatientNameLabel"
        '
        'SexLabel
        '
        resources.ApplyResources(SexLabel, "SexLabel")
        SexLabel.Name = "SexLabel"
        '
        'AgeLabel
        '
        resources.ApplyResources(AgeLabel, "AgeLabel")
        AgeLabel.Name = "AgeLabel"
        '
        'PhoneLabel
        '
        resources.ApplyResources(PhoneLabel, "PhoneLabel")
        PhoneLabel.Name = "PhoneLabel"
        '
        'AddressLabel
        '
        resources.ApplyResources(AddressLabel, "AddressLabel")
        AddressLabel.Name = "AddressLabel"
        '
        'HealthLabel
        '
        resources.ApplyResources(HealthLabel, "HealthLabel")
        HealthLabel.Name = "HealthLabel"
        '
        'NotesLabel
        '
        resources.ApplyResources(NotesLabel, "NotesLabel")
        NotesLabel.Name = "NotesLabel"
        '
        'BirthYLabel
        '
        resources.ApplyResources(BirthYLabel, "BirthYLabel")
        BirthYLabel.Name = "BirthYLabel"
        '
        'Label1
        '
        resources.ApplyResources(Label1, "Label1")
        Label1.Name = "Label1"
        '
        'Label2
        '
        resources.ApplyResources(Label2, "Label2")
        Label2.Name = "Label2"
        '
        'grpPatientInfo
        '
        resources.ApplyResources(Me.grpPatientInfo, "grpPatientInfo")
        Me.grpPatientInfo.Appearance.Font = CType(resources.GetObject("grpPatientInfo.Appearance.Font"), System.Drawing.Font)
        Me.grpPatientInfo.Appearance.Options.UseFont = True
        Me.grpPatientInfo.AppearanceCaption.Font = CType(resources.GetObject("grpPatientInfo.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpPatientInfo.AppearanceCaption.Options.UseFont = True
        Me.grpPatientInfo.Controls.Add(Me.lblWhats)
        Me.grpPatientInfo.Controls.Add(Me.cboPrefix)
        Me.grpPatientInfo.Controls.Add(Label2)
        Me.grpPatientInfo.Controls.Add(Me.txtWhats)
        Me.grpPatientInfo.Controls.Add(Label1)
        Me.grpPatientInfo.Controls.Add(Me.WhatsAppTextEdit)
        Me.grpPatientInfo.Controls.Add(Me.DiagCheck)
        Me.grpPatientInfo.Controls.Add(Me.RadioFemale)
        Me.grpPatientInfo.Controls.Add(Me.RadioMale)
        Me.grpPatientInfo.Controls.Add(Me.CboCity)
        Me.grpPatientInfo.Controls.Add(Me.CboHealth)
        Me.grpPatientInfo.Controls.Add(Me.LabelAge)
        Me.grpPatientInfo.Controls.Add(Me.LabelBal)
        Me.grpPatientInfo.Controls.Add(Me.btnDelete)
        Me.grpPatientInfo.Controls.Add(Me.btnEditPat)
        Me.grpPatientInfo.Controls.Add(PatientNameLabel)
        Me.grpPatientInfo.Controls.Add(Me.PatientNameTextEdit)
        Me.grpPatientInfo.Controls.Add(Me.PatientIDEdit)
        Me.grpPatientInfo.Controls.Add(SexLabel)
        Me.grpPatientInfo.Controls.Add(Me.PicBox)
        Me.grpPatientInfo.Controls.Add(Me.SexTextBox)
        Me.grpPatientInfo.Controls.Add(AgeLabel)
        Me.grpPatientInfo.Controls.Add(Me.AgeSpinEdit)
        Me.grpPatientInfo.Controls.Add(PhoneLabel)
        Me.grpPatientInfo.Controls.Add(Me.PhoneTextEdit)
        Me.grpPatientInfo.Controls.Add(Me.lblPNum)
        Me.grpPatientInfo.Controls.Add(Me.LabelControl1)
        Me.grpPatientInfo.Controls.Add(AddressLabel)
        Me.grpPatientInfo.Controls.Add(Me.AddressTextEdit)
        Me.grpPatientInfo.Controls.Add(HealthLabel)
        Me.grpPatientInfo.Controls.Add(Me.HealthTextBox)
        Me.grpPatientInfo.Controls.Add(Me.TreatCheckBox)
        Me.grpPatientInfo.Controls.Add(Me.ImplantCheckBox)
        Me.grpPatientInfo.Controls.Add(Me.MobileCheckBox)
        Me.grpPatientInfo.Controls.Add(Me.OrthoCheckBox)
        Me.grpPatientInfo.Controls.Add(Me.StrucCheckBox)
        Me.grpPatientInfo.Controls.Add(NotesLabel)
        Me.grpPatientInfo.Controls.Add(Me.NotesTextEdit)
        Me.grpPatientInfo.Controls.Add(BirthYLabel)
        Me.grpPatientInfo.Controls.Add(Me.BirthYSpinEdit)
        Me.grpPatientInfo.Name = "grpPatientInfo"
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
        'WhatsAppTextEdit
        '
        resources.ApplyResources(Me.WhatsAppTextEdit, "WhatsAppTextEdit")
        Me.WhatsAppTextEdit.Name = "WhatsAppTextEdit"
        Me.WhatsAppTextEdit.Properties.Appearance.Font = CType(resources.GetObject("WhatsAppTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.WhatsAppTextEdit.Properties.Appearance.Options.UseFont = True
        Me.WhatsAppTextEdit.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.SimpleMaskManager))
        Me.WhatsAppTextEdit.Properties.MaskSettings.Set("MaskManagerSignature", "ignoreMaskBlank=True")
        Me.WhatsAppTextEdit.Properties.MaskSettings.Set("mask", "000000000000")
        '
        'DiagCheck
        '
        resources.ApplyResources(Me.DiagCheck, "DiagCheck")
        Me.DiagCheck.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.PatientBindingSource, "Diag", True))
        Me.DiagCheck.Name = "DiagCheck"
        Me.DiagCheck.UseVisualStyleBackColor = True
        '
        'RadioFemale
        '
        resources.ApplyResources(Me.RadioFemale, "RadioFemale")
        Me.RadioFemale.Name = "RadioFemale"
        Me.RadioFemale.UseVisualStyleBackColor = True
        '
        'RadioMale
        '
        resources.ApplyResources(Me.RadioMale, "RadioMale")
        Me.RadioMale.Checked = True
        Me.RadioMale.Name = "RadioMale"
        Me.RadioMale.TabStop = True
        Me.RadioMale.UseVisualStyleBackColor = True
        '
        'CboCity
        '
        resources.ApplyResources(Me.CboCity, "CboCity")
        Me.CboCity.BtnAddVisible = True
        Me.CboCity.BtnSearchVisible = True
        Me.CboCity.CityID = 2
        Me.CboCity.CityName = "قلقيلية"
        Me.CboCity.Filter = ""
        Me.CboCity.Name = "CboCity"
        '
        'CboHealth
        '
        resources.ApplyResources(Me.CboHealth, "CboHealth")
        Me.CboHealth.BtnAddVisible = True
        Me.CboHealth.BtnSearchVisible = True
        Me.CboHealth.Filter = ""
        Me.CboHealth.HealthStat = "سليم"
        Me.CboHealth.HID = 1
        Me.CboHealth.Name = "CboHealth"
        '
        'LabelAge
        '
        resources.ApplyResources(Me.LabelAge, "LabelAge")
        Me.LabelAge.Appearance.Font = CType(resources.GetObject("LabelAge.Appearance.Font"), System.Drawing.Font)
        Me.LabelAge.Appearance.Options.UseFont = True
        Me.LabelAge.Appearance.Options.UseTextOptions = True
        Me.LabelAge.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelAge.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelAge.Name = "LabelAge"
        '
        'LabelBal
        '
        resources.ApplyResources(Me.LabelBal, "LabelBal")
        Me.LabelBal.Appearance.Font = CType(resources.GetObject("LabelBal.Appearance.Font"), System.Drawing.Font)
        Me.LabelBal.Appearance.Options.UseFont = True
        Me.LabelBal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBindingSource, "Balance", True))
        Me.LabelBal.Name = "LabelBal"
        '
        'btnDelete
        '
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.Appearance.Font = CType(resources.GetObject("btnDelete.Appearance.Font"), System.Drawing.Font)
        Me.btnDelete.Appearance.Options.UseFont = True
        Me.btnDelete.ImageOptions.ImageKey = resources.GetString("btnDelete.ImageOptions.ImageKey")
        Me.btnDelete.Name = "btnDelete"
        '
        'btnEditPat
        '
        resources.ApplyResources(Me.btnEditPat, "btnEditPat")
        Me.btnEditPat.Appearance.Font = CType(resources.GetObject("btnEditPat.Appearance.Font"), System.Drawing.Font)
        Me.btnEditPat.Appearance.Options.UseFont = True
        Me.btnEditPat.ImageOptions.ImageKey = resources.GetString("btnEditPat.ImageOptions.ImageKey")
        Me.btnEditPat.Name = "btnEditPat"
        '
        'PatientNameTextEdit
        '
        resources.ApplyResources(Me.PatientNameTextEdit, "PatientNameTextEdit")
        Me.PatientNameTextEdit.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBindingSource, "PatientName", True))
        Me.PatientNameTextEdit.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.PatientBindingSource, "PatientName", True))
        Me.PatientNameTextEdit.Name = "PatientNameTextEdit"
        Me.PatientNameTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PatientNameTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PatientNameTextEdit.Properties.Appearance.Options.UseFont = True
        '
        'PatientIDEdit
        '
        resources.ApplyResources(Me.PatientIDEdit, "PatientIDEdit")
        Me.PatientIDEdit.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBindingSource, "PatientID", True))
        Me.PatientIDEdit.Name = "PatientIDEdit"
        Me.PatientIDEdit.Properties.Appearance.Font = CType(resources.GetObject("PatientIDEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PatientIDEdit.Properties.Appearance.Options.UseFont = True
        '
        'PicBox
        '
        resources.ApplyResources(Me.PicBox, "PicBox")
        Me.PicBox.Name = "PicBox"
        Me.PicBox.TabStop = False
        '
        'SexTextBox
        '
        resources.ApplyResources(Me.SexTextBox, "SexTextBox")
        Me.SexTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBindingSource, "Sex", True))
        Me.SexTextBox.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.PatientBindingSource, "Sex", True))
        Me.SexTextBox.Name = "SexTextBox"
        Me.SexTextBox.Properties.Appearance.Font = CType(resources.GetObject("SexTextBox.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SexTextBox.Properties.Appearance.Options.UseFont = True
        '
        'AgeSpinEdit
        '
        resources.ApplyResources(Me.AgeSpinEdit, "AgeSpinEdit")
        Me.AgeSpinEdit.Name = "AgeSpinEdit"
        Me.AgeSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("AgeSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.AgeSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.AgeSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("AgeSpinEdit.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.AgeSpinEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.AgeSpinEdit.Properties.MaskSettings.Set("mask", "d")
        '
        'PhoneTextEdit
        '
        resources.ApplyResources(Me.PhoneTextEdit, "PhoneTextEdit")
        Me.PhoneTextEdit.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBindingSource, "Phone", True))
        Me.PhoneTextEdit.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.PatientBindingSource, "Phone", True))
        Me.PhoneTextEdit.Name = "PhoneTextEdit"
        Me.PhoneTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PhoneTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PhoneTextEdit.Properties.Appearance.Options.UseFont = True
        '
        'lblPNum
        '
        resources.ApplyResources(Me.lblPNum, "lblPNum")
        Me.lblPNum.Appearance.Font = CType(resources.GetObject("lblPNum.Appearance.Font"), System.Drawing.Font)
        Me.lblPNum.Appearance.Options.UseFont = True
        Me.lblPNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBindingSource, "PatientNumber", True))
        Me.lblPNum.Name = "lblPNum"
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'AddressTextEdit
        '
        resources.ApplyResources(Me.AddressTextEdit, "AddressTextEdit")
        Me.AddressTextEdit.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBindingSource, "Address", True))
        Me.AddressTextEdit.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.PatientBindingSource, "Address", True))
        Me.AddressTextEdit.Name = "AddressTextEdit"
        Me.AddressTextEdit.Properties.Appearance.Font = CType(resources.GetObject("AddressTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.AddressTextEdit.Properties.Appearance.Options.UseFont = True
        '
        'HealthTextBox
        '
        resources.ApplyResources(Me.HealthTextBox, "HealthTextBox")
        Me.HealthTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBindingSource, "Health", True))
        Me.HealthTextBox.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.PatientBindingSource, "Health", True))
        Me.HealthTextBox.Name = "HealthTextBox"
        Me.HealthTextBox.Properties.Appearance.Font = CType(resources.GetObject("HealthTextBox.Properties.Appearance.Font"), System.Drawing.Font)
        Me.HealthTextBox.Properties.Appearance.Options.UseFont = True
        '
        'TreatCheckBox
        '
        resources.ApplyResources(Me.TreatCheckBox, "TreatCheckBox")
        Me.TreatCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.PatientBindingSource, "Treat", True))
        Me.TreatCheckBox.Name = "TreatCheckBox"
        Me.TreatCheckBox.UseVisualStyleBackColor = True
        '
        'ImplantCheckBox
        '
        resources.ApplyResources(Me.ImplantCheckBox, "ImplantCheckBox")
        Me.ImplantCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.PatientBindingSource, "Implant", True))
        Me.ImplantCheckBox.Name = "ImplantCheckBox"
        Me.ImplantCheckBox.UseVisualStyleBackColor = True
        '
        'MobileCheckBox
        '
        resources.ApplyResources(Me.MobileCheckBox, "MobileCheckBox")
        Me.MobileCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.PatientBindingSource, "Mobile", True))
        Me.MobileCheckBox.Name = "MobileCheckBox"
        Me.MobileCheckBox.UseVisualStyleBackColor = True
        '
        'OrthoCheckBox
        '
        resources.ApplyResources(Me.OrthoCheckBox, "OrthoCheckBox")
        Me.OrthoCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.PatientBindingSource, "Ortho", True))
        Me.OrthoCheckBox.Name = "OrthoCheckBox"
        Me.OrthoCheckBox.UseVisualStyleBackColor = True
        '
        'StrucCheckBox
        '
        resources.ApplyResources(Me.StrucCheckBox, "StrucCheckBox")
        Me.StrucCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.PatientBindingSource, "Struc", True))
        Me.StrucCheckBox.Name = "StrucCheckBox"
        Me.StrucCheckBox.UseVisualStyleBackColor = True
        '
        'NotesTextEdit
        '
        resources.ApplyResources(Me.NotesTextEdit, "NotesTextEdit")
        Me.NotesTextEdit.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBindingSource, "Notes", True))
        Me.NotesTextEdit.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.PatientBindingSource, "Notes", True))
        Me.NotesTextEdit.Name = "NotesTextEdit"
        Me.NotesTextEdit.Properties.Appearance.Font = CType(resources.GetObject("NotesTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.NotesTextEdit.Properties.Appearance.Options.UseFont = True
        '
        'BirthYSpinEdit
        '
        resources.ApplyResources(Me.BirthYSpinEdit, "BirthYSpinEdit")
        Me.BirthYSpinEdit.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.PatientBindingSource, "BirthY", True))
        Me.BirthYSpinEdit.Name = "BirthYSpinEdit"
        Me.BirthYSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("BirthYSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.BirthYSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.BirthYSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("BirthYSpinEdit.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.BirthYSpinEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.BirthYSpinEdit.Properties.MaskSettings.Set("mask", "d")
        Me.BirthYSpinEdit.Properties.MaskSettings.Set("hideInsignificantZeros", True)
        Me.BirthYSpinEdit.Properties.MaskSettings.Set("autoHideDecimalSeparator", True)
        '
        'PatientInfoForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpPatientInfo)
        Me.Name = "PatientInfoForm"
        CType(Me.PatientBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpPatientInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPatientInfo.ResumeLayout(False)
        Me.grpPatientInfo.PerformLayout()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WhatsAppTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientIDEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SexTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AgeSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AddressTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HealthTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NotesTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BirthYSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PatientBindingSource As BindingSource
    Friend WithEvents grpPatientInfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditPat As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PatientNameTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PatientIDEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PicBox As PictureBox
    Friend WithEvents SexTextBox As DevExpress.XtraEditors.TextEdit
    Friend WithEvents AgeSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents PhoneTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents AddressTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents HealthTextBox As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TreatCheckBox As CheckBox
    Friend WithEvents ImplantCheckBox As CheckBox
    Friend WithEvents MobileCheckBox As CheckBox
    Friend WithEvents OrthoCheckBox As CheckBox
    Friend WithEvents StrucCheckBox As CheckBox
    Friend WithEvents NotesTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BirthYSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelAge As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelBal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CboCity As CityCombo
    Friend WithEvents CboHealth As HlthCombo
    Friend WithEvents RadioFemale As RadioButton
    Friend WithEvents RadioMale As RadioButton
    Friend WithEvents lblPNum As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DiagCheck As CheckBox
    Friend WithEvents WhatsAppTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
End Class
