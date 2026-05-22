<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PatientAddEditForm
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
        Dim PatientNameLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PatientAddEditForm))
        Dim SexLabel As System.Windows.Forms.Label
        Dim AgeLabel As System.Windows.Forms.Label
        Dim PhoneLabel As System.Windows.Forms.Label
        Dim AddressLabel As System.Windows.Forms.Label
        Dim HealthLabel As System.Windows.Forms.Label
        Dim NotesLabel As System.Windows.Forms.Label
        Dim BirthYLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Me.TxtName = New DevExpress.XtraEditors.TextEdit()
        Me.PatientIDEdit = New DevExpress.XtraEditors.TextEdit()
        Me.PicBox = New System.Windows.Forms.PictureBox()
        Me.SpinAge = New DevExpress.XtraEditors.SpinEdit()
        Me.TxtPhone = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TreatCheck = New System.Windows.Forms.CheckBox()
        Me.ImplntCheck = New System.Windows.Forms.CheckBox()
        Me.MobileCheck = New System.Windows.Forms.CheckBox()
        Me.OrthoCheck = New System.Windows.Forms.CheckBox()
        Me.StrucCheck = New System.Windows.Forms.CheckBox()
        Me.TxtNotes = New DevExpress.XtraEditors.TextEdit()
        Me.SpinYear = New DevExpress.XtraEditors.SpinEdit()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.RadioFemale = New System.Windows.Forms.RadioButton()
        Me.RadioMale = New System.Windows.Forms.RadioButton()
        Me.TxtSex = New DevExpress.XtraEditors.TextEdit()
        Me.TxtHealth = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAdrs = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAge = New DevExpress.XtraEditors.TextEdit()
        Me.CboCity = New DentistX.CityCombo()
        Me.CboHealth = New DentistX.HlthCombo()
        Me.DiagCheck = New System.Windows.Forms.CheckBox()
        Me.WhatsAppTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
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
        CType(Me.TxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientIDEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpinAge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpinYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSex.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtHealth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAdrs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WhatsAppTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'TxtName
        '
        resources.ApplyResources(Me.TxtName, "TxtName")
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Properties.Appearance.Font = CType(resources.GetObject("TxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtName.Properties.Appearance.Options.UseFont = True
        '
        'PatientIDEdit
        '
        resources.ApplyResources(Me.PatientIDEdit, "PatientIDEdit")
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
        'SpinAge
        '
        resources.ApplyResources(Me.SpinAge, "SpinAge")
        Me.SpinAge.Name = "SpinAge"
        Me.SpinAge.Properties.Appearance.Font = CType(resources.GetObject("SpinAge.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SpinAge.Properties.Appearance.Options.UseFont = True
        Me.SpinAge.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("SpinAge.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.SpinAge.Properties.IsFloatValue = False
        Me.SpinAge.Properties.MaskSettings.Set("mask", "N00")
        Me.SpinAge.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.SpinAge.Properties.MinValue = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'TxtPhone
        '
        resources.ApplyResources(Me.TxtPhone, "TxtPhone")
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Properties.Appearance.Font = CType(resources.GetObject("TxtPhone.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPhone.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'TreatCheck
        '
        resources.ApplyResources(Me.TreatCheck, "TreatCheck")
        Me.TreatCheck.Name = "TreatCheck"
        Me.TreatCheck.UseVisualStyleBackColor = True
        '
        'ImplntCheck
        '
        resources.ApplyResources(Me.ImplntCheck, "ImplntCheck")
        Me.ImplntCheck.Name = "ImplntCheck"
        Me.ImplntCheck.UseVisualStyleBackColor = True
        '
        'MobileCheck
        '
        resources.ApplyResources(Me.MobileCheck, "MobileCheck")
        Me.MobileCheck.Name = "MobileCheck"
        Me.MobileCheck.UseVisualStyleBackColor = True
        '
        'OrthoCheck
        '
        resources.ApplyResources(Me.OrthoCheck, "OrthoCheck")
        Me.OrthoCheck.Name = "OrthoCheck"
        Me.OrthoCheck.UseVisualStyleBackColor = True
        '
        'StrucCheck
        '
        resources.ApplyResources(Me.StrucCheck, "StrucCheck")
        Me.StrucCheck.Name = "StrucCheck"
        Me.StrucCheck.UseVisualStyleBackColor = True
        '
        'TxtNotes
        '
        resources.ApplyResources(Me.TxtNotes, "TxtNotes")
        Me.TxtNotes.Name = "TxtNotes"
        Me.TxtNotes.Properties.Appearance.Font = CType(resources.GetObject("TxtNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtNotes.Properties.Appearance.Options.UseFont = True
        '
        'SpinYear
        '
        resources.ApplyResources(Me.SpinYear, "SpinYear")
        Me.SpinYear.Name = "SpinYear"
        Me.SpinYear.Properties.Appearance.Font = CType(resources.GetObject("SpinYear.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SpinYear.Properties.Appearance.Options.UseFont = True
        Me.SpinYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("SpinYear.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.SpinYear.Properties.IsFloatValue = False
        Me.SpinYear.Properties.MaskSettings.Set("mask", "N00")
        Me.SpinYear.Properties.MaxValue = New Decimal(New Integer() {2040, 0, 0, 0})
        Me.SpinYear.Properties.MinValue = New Decimal(New Integer() {1930, 0, 0, 0})
        '
        'btnAdd
        '
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.Appearance.Font = CType(resources.GetObject("btnAdd.Appearance.Font"), System.Drawing.Font)
        Me.btnAdd.Appearance.Options.UseFont = True
        Me.btnAdd.ImageOptions.ImageKey = resources.GetString("btnAdd.ImageOptions.ImageKey")
        Me.btnAdd.Name = "btnAdd"
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.ImageOptions.ImageKey = resources.GetString("btnCancel.ImageOptions.ImageKey")
        Me.btnCancel.Name = "btnCancel"
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
        'TxtSex
        '
        resources.ApplyResources(Me.TxtSex, "TxtSex")
        Me.TxtSex.Name = "TxtSex"
        Me.TxtSex.Properties.Appearance.Font = CType(resources.GetObject("TxtSex.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtSex.Properties.Appearance.Options.UseFont = True
        '
        'TxtHealth
        '
        resources.ApplyResources(Me.TxtHealth, "TxtHealth")
        Me.TxtHealth.Name = "TxtHealth"
        Me.TxtHealth.Properties.Appearance.Font = CType(resources.GetObject("TxtHealth.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtHealth.Properties.Appearance.Options.UseFont = True
        '
        'TxtAdrs
        '
        resources.ApplyResources(Me.TxtAdrs, "TxtAdrs")
        Me.TxtAdrs.Name = "TxtAdrs"
        Me.TxtAdrs.Properties.Appearance.Font = CType(resources.GetObject("TxtAdrs.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAdrs.Properties.Appearance.Options.UseFont = True
        '
        'TxtAge
        '
        resources.ApplyResources(Me.TxtAge, "TxtAge")
        Me.TxtAge.Name = "TxtAge"
        Me.TxtAge.Properties.Appearance.Font = CType(resources.GetObject("TxtAge.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAge.Properties.Appearance.Options.UseFont = True
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
        'DiagCheck
        '
        resources.ApplyResources(Me.DiagCheck, "DiagCheck")
        Me.DiagCheck.Name = "DiagCheck"
        Me.DiagCheck.UseVisualStyleBackColor = True
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
        'PatientAddEditForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblWhats)
        Me.Controls.Add(Me.cboPrefix)
        Me.Controls.Add(Label2)
        Me.Controls.Add(Me.txtWhats)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.WhatsAppTextEdit)
        Me.Controls.Add(Me.CboCity)
        Me.Controls.Add(Me.CboHealth)
        Me.Controls.Add(Me.RadioFemale)
        Me.Controls.Add(Me.RadioMale)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(PatientNameLabel)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.PatientIDEdit)
        Me.Controls.Add(SexLabel)
        Me.Controls.Add(AgeLabel)
        Me.Controls.Add(Me.SpinAge)
        Me.Controls.Add(PhoneLabel)
        Me.Controls.Add(Me.TxtSex)
        Me.Controls.Add(Me.TxtAdrs)
        Me.Controls.Add(Me.TxtHealth)
        Me.Controls.Add(Me.TxtAge)
        Me.Controls.Add(Me.TxtPhone)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(AddressLabel)
        Me.Controls.Add(HealthLabel)
        Me.Controls.Add(Me.TreatCheck)
        Me.Controls.Add(Me.ImplntCheck)
        Me.Controls.Add(Me.MobileCheck)
        Me.Controls.Add(Me.OrthoCheck)
        Me.Controls.Add(Me.DiagCheck)
        Me.Controls.Add(Me.StrucCheck)
        Me.Controls.Add(NotesLabel)
        Me.Controls.Add(Me.TxtNotes)
        Me.Controls.Add(BirthYLabel)
        Me.Controls.Add(Me.SpinYear)
        Me.Controls.Add(Me.PicBox)
        Me.Name = "PatientAddEditForm"
        CType(Me.TxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientIDEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpinAge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPhone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpinYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSex.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtHealth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAdrs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WhatsAppTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PatientIDEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PicBox As Windows.Forms.PictureBox
    Friend WithEvents SpinAge As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents TxtPhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TreatCheck As Windows.Forms.CheckBox
    Friend WithEvents ImplntCheck As Windows.Forms.CheckBox
    Friend WithEvents MobileCheck As Windows.Forms.CheckBox
    Friend WithEvents OrthoCheck As Windows.Forms.CheckBox
    Friend WithEvents StrucCheck As Windows.Forms.CheckBox
    Friend WithEvents TxtNotes As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SpinYear As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RadioFemale As Windows.Forms.RadioButton
    Friend WithEvents RadioMale As Windows.Forms.RadioButton
    Friend WithEvents TxtSex As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtHealth As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAdrs As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAge As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CboHealth As HlthCombo
    Friend WithEvents CboCity As CityCombo
    Friend WithEvents DiagCheck As CheckBox
    Friend WithEvents WhatsAppTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
End Class
