<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Navigator3
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
        Dim PatientNameLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Navigator3))
        Dim SexLabel As System.Windows.Forms.Label
        Dim AgeLabel As System.Windows.Forms.Label
        Dim PhoneLabel As System.Windows.Forms.Label
        Dim AddressLabel As System.Windows.Forms.Label
        Dim HealthLabel As System.Windows.Forms.Label
        Dim NotesLabel As System.Windows.Forms.Label
        Dim BirthYLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim ButtonImageOptions1 As DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions = New DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions()
        Me.PatientBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.btNewPatient = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLast = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNext = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrev = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFirst = New DevExpress.XtraEditors.SimpleButton()
        Me.PopupPatient = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.grpPatientInfo = New DevExpress.XtraEditors.GroupControl()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.DiagCheck = New System.Windows.Forms.CheckBox()
        Me.RadioFemale = New System.Windows.Forms.RadioButton()
        Me.RadioMale = New System.Windows.Forms.RadioButton()
        Me.CboCity = New DentistX.CityCombo()
        Me.CboHealth = New DentistX.HlthCombo()
        Me.lblAge = New DevExpress.XtraEditors.LabelControl()
        Me.lblBalance = New DevExpress.XtraEditors.LabelControl()
        Me.btnDeletePatient = New DevExpress.XtraEditors.SimpleButton()
        Me.btnUpdatePatient = New DevExpress.XtraEditors.SimpleButton()
        Me.PatientNameTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.PatientIDEdit = New DevExpress.XtraEditors.TextEdit()
        Me.PicBox = New System.Windows.Forms.PictureBox()
        Me.SexTextBox = New DevExpress.XtraEditors.TextEdit()
        Me.AgeSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.PhoneTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.lblPNum = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.AddressTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.HealthTextBox = New DevExpress.XtraEditors.TextEdit()
        Me.TreatCheckBox = New System.Windows.Forms.CheckBox()
        Me.ImplantCheckBox = New System.Windows.Forms.CheckBox()
        Me.MobileCheckBox = New System.Windows.Forms.CheckBox()
        Me.OrthoCheckBox = New System.Windows.Forms.CheckBox()
        Me.StrucCheckBox = New System.Windows.Forms.CheckBox()
        Me.NotesTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.BirthYSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.SurgeryBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LabelBal = New DevExpress.XtraEditors.LabelControl()
        Me.txtCount = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.SplashScreenManager1 = New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(Global.DentistX.WaitForm1), True, True, GetType(System.Windows.Forms.UserControl))
        Me.posTxt = New DevExpress.XtraEditors.TextEdit()
        Me.btnAdultChart = New DevExpress.XtraEditors.CheckButton()
        Me.LabelAge = New DevExpress.XtraEditors.LabelControl()
        Me.IsKidLabel = New DevExpress.XtraEditors.LabelControl()
        Me.btnResetKid = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelAddress = New DevExpress.XtraEditors.LabelControl()
        Me.lblPatientNum = New DevExpress.XtraEditors.LabelControl()
        Me.Side1 = New DevExpress.XtraEditors.SidePanel()
        Me.lblPatientName = New DevExpress.XtraEditors.LabelControl()
        Me.lblTxtTrts = New DevExpress.XtraEditors.LabelControl()
        Me.lblTrts = New DevExpress.XtraEditors.LabelControl()
        Me.lblTxtPays = New DevExpress.XtraEditors.LabelControl()
        Me.lblPays = New DevExpress.XtraEditors.LabelControl()
        Me.lbltxtBal = New DevExpress.XtraEditors.LabelControl()
        Me.txtPatientName = New DevExpress.XtraEditors.TextEdit()
        Me.btnSearchResults = New DevExpress.XtraEditors.SimpleButton()
        Me.FlyoutPatientInfo = New DevExpress.Utils.FlyoutPanel()
        Me.FlyoutPanelControl1 = New DevExpress.Utils.FlyoutPanelControl()
        Me.btnWhatsSend = New DevExpress.XtraEditors.SimpleButton()
        PatientNameLabel = New System.Windows.Forms.Label()
        SexLabel = New System.Windows.Forms.Label()
        AgeLabel = New System.Windows.Forms.Label()
        PhoneLabel = New System.Windows.Forms.Label()
        AddressLabel = New System.Windows.Forms.Label()
        HealthLabel = New System.Windows.Forms.Label()
        NotesLabel = New System.Windows.Forms.Label()
        BirthYLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.PatientBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpPatientInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPatientInfo.SuspendLayout()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.SurgeryBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.posTxt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Side1.SuspendLayout()
        CType(Me.txtPatientName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FlyoutPatientInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlyoutPatientInfo.SuspendLayout()
        CType(Me.FlyoutPanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlyoutPanelControl1.SuspendLayout()
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
        'PatientBS
        '
        Me.PatientBS.DataSource = GetType(DentistX.Patient)
        '
        'btNewPatient
        '
        resources.ApplyResources(Me.btNewPatient, "btNewPatient")
        Me.btNewPatient.Appearance.Font = CType(resources.GetObject("btNewPatient.Appearance.Font"), System.Drawing.Font)
        Me.btNewPatient.Appearance.Options.UseFont = True
        Me.btNewPatient.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btNewPatient.Name = "btNewPatient"
        '
        'btnLast
        '
        resources.ApplyResources(Me.btnLast, "btnLast")
        Me.btnLast.Appearance.Font = CType(resources.GetObject("btnLast.Appearance.Font"), System.Drawing.Font)
        Me.btnLast.Appearance.Options.UseFont = True
        Me.btnLast.Name = "btnLast"
        '
        'btnNext
        '
        resources.ApplyResources(Me.btnNext, "btnNext")
        Me.btnNext.Appearance.Font = CType(resources.GetObject("btnNext.Appearance.Font"), System.Drawing.Font)
        Me.btnNext.Appearance.Options.UseFont = True
        Me.btnNext.Name = "btnNext"
        '
        'btnPrev
        '
        Me.btnPrev.Appearance.Font = CType(resources.GetObject("btnPrev.Appearance.Font"), System.Drawing.Font)
        Me.btnPrev.Appearance.Options.UseFont = True
        Me.btnPrev.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.btnPrev, "btnPrev")
        Me.btnPrev.Name = "btnPrev"
        '
        'btnFirst
        '
        Me.btnFirst.Appearance.Font = CType(resources.GetObject("btnFirst.Appearance.Font"), System.Drawing.Font)
        Me.btnFirst.Appearance.Options.UseFont = True
        Me.btnFirst.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.btnFirst, "btnFirst")
        Me.btnFirst.Name = "btnFirst"
        '
        'PopupPatient
        '
        resources.ApplyResources(Me.PopupPatient, "PopupPatient")
        Me.PopupPatient.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBS, "PatientName", True))
        Me.PopupPatient.Name = "PopupPatient"
        Me.PopupPatient.Properties.Appearance.BackColor = System.Drawing.Color.SeaShell
        Me.PopupPatient.Properties.Appearance.Font = CType(resources.GetObject("PopupPatient.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PopupPatient.Properties.Appearance.Options.UseBackColor = True
        Me.PopupPatient.Properties.Appearance.Options.UseFont = True
        Me.PopupPatient.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("PopupPatient.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.PopupPatient.Properties.NullText = resources.GetString("PopupPatient.Properties.NullText")
        Me.PopupPatient.Properties.NullValuePrompt = resources.GetString("PopupPatient.Properties.NullValuePrompt")
        Me.PopupPatient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        '
        'grpPatientInfo
        '
        Me.grpPatientInfo.Appearance.Font = CType(resources.GetObject("grpPatientInfo.Appearance.Font"), System.Drawing.Font)
        Me.grpPatientInfo.Appearance.Options.UseFont = True
        Me.grpPatientInfo.AppearanceCaption.Font = CType(resources.GetObject("grpPatientInfo.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpPatientInfo.AppearanceCaption.Options.UseFont = True
        Me.grpPatientInfo.Controls.Add(Me.txtWhats)
        Me.grpPatientInfo.Controls.Add(Me.lblWhats)
        Me.grpPatientInfo.Controls.Add(Me.cboPrefix)
        Me.grpPatientInfo.Controls.Add(Label1)
        Me.grpPatientInfo.Controls.Add(Me.btnAdd)
        Me.grpPatientInfo.Controls.Add(Me.DiagCheck)
        Me.grpPatientInfo.Controls.Add(Me.RadioFemale)
        Me.grpPatientInfo.Controls.Add(Me.RadioMale)
        Me.grpPatientInfo.Controls.Add(Me.CboCity)
        Me.grpPatientInfo.Controls.Add(Me.CboHealth)
        Me.grpPatientInfo.Controls.Add(Me.lblAge)
        Me.grpPatientInfo.Controls.Add(Me.lblBalance)
        Me.grpPatientInfo.Controls.Add(Me.btnDeletePatient)
        Me.grpPatientInfo.Controls.Add(Me.btnUpdatePatient)
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
        Me.grpPatientInfo.Controls.Add(Me.LabelControl4)
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
        resources.ApplyResources(Me.grpPatientInfo, "grpPatientInfo")
        Me.grpPatientInfo.Name = "grpPatientInfo"
        '
        'txtWhats
        '
        Me.txtWhats.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtWhats, "txtWhats")
        Me.txtWhats.Name = "txtWhats"
        Me.txtWhats.Properties.Appearance.Font = CType(resources.GetObject("txtWhats.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtWhats.Properties.Appearance.Options.UseFont = True
        Me.txtWhats.Properties.MaxLength = 10
        '
        'lblWhats
        '
        Me.lblWhats.Appearance.Font = CType(resources.GetObject("lblWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblWhats.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblWhats.Appearance.Options.UseFont = True
        Me.lblWhats.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblWhats, "lblWhats")
        Me.lblWhats.Name = "lblWhats"
        '
        'cboPrefix
        '
        Me.cboPrefix.EnterMoveNextControl = True
        resources.ApplyResources(Me.cboPrefix, "cboPrefix")
        Me.cboPrefix.Name = "cboPrefix"
        Me.cboPrefix.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPrefix.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btnAdd
        '
        Me.btnAdd.Appearance.Font = CType(resources.GetObject("btnAdd.Appearance.Font"), System.Drawing.Font)
        Me.btnAdd.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.Name = "btnAdd"
        '
        'DiagCheck
        '
        resources.ApplyResources(Me.DiagCheck, "DiagCheck")
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
        Me.CboCity.BtnAddVisible = True
        Me.CboCity.BtnSearchVisible = True
        Me.CboCity.CityID = 2
        Me.CboCity.CityName = "قلقيلية"
        Me.CboCity.Filter = ""
        resources.ApplyResources(Me.CboCity, "CboCity")
        Me.CboCity.Name = "CboCity"
        '
        'CboHealth
        '
        Me.CboHealth.BtnAddVisible = True
        Me.CboHealth.BtnSearchVisible = True
        Me.CboHealth.Filter = ""
        Me.CboHealth.HealthStat = "سليم"
        Me.CboHealth.HID = 1
        resources.ApplyResources(Me.CboHealth, "CboHealth")
        Me.CboHealth.Name = "CboHealth"
        '
        'lblAge
        '
        Me.lblAge.Appearance.Font = CType(resources.GetObject("lblAge.Appearance.Font"), System.Drawing.Font)
        Me.lblAge.Appearance.Options.UseFont = True
        Me.lblAge.Appearance.Options.UseTextOptions = True
        Me.lblAge.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblAge.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.lblAge, "lblAge")
        Me.lblAge.Name = "lblAge"
        '
        'lblBalance
        '
        Me.lblBalance.Appearance.Font = CType(resources.GetObject("lblBalance.Appearance.Font"), System.Drawing.Font)
        Me.lblBalance.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblBalance, "lblBalance")
        Me.lblBalance.Name = "lblBalance"
        '
        'btnDeletePatient
        '
        Me.btnDeletePatient.Appearance.Font = CType(resources.GetObject("btnDeletePatient.Appearance.Font"), System.Drawing.Font)
        Me.btnDeletePatient.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnDeletePatient, "btnDeletePatient")
        Me.btnDeletePatient.Name = "btnDeletePatient"
        '
        'btnUpdatePatient
        '
        Me.btnUpdatePatient.Appearance.Font = CType(resources.GetObject("btnUpdatePatient.Appearance.Font"), System.Drawing.Font)
        Me.btnUpdatePatient.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnUpdatePatient, "btnUpdatePatient")
        Me.btnUpdatePatient.Name = "btnUpdatePatient"
        '
        'PatientNameTextEdit
        '
        Me.PatientNameTextEdit.EnterMoveNextControl = True
        resources.ApplyResources(Me.PatientNameTextEdit, "PatientNameTextEdit")
        Me.PatientNameTextEdit.Name = "PatientNameTextEdit"
        Me.PatientNameTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PatientNameTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PatientNameTextEdit.Properties.Appearance.Options.UseFont = True
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
        'SexTextBox
        '
        Me.SexTextBox.EnterMoveNextControl = True
        resources.ApplyResources(Me.SexTextBox, "SexTextBox")
        Me.SexTextBox.Name = "SexTextBox"
        Me.SexTextBox.Properties.Appearance.Font = CType(resources.GetObject("SexTextBox.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SexTextBox.Properties.Appearance.Options.UseFont = True
        '
        'AgeSpinEdit
        '
        resources.ApplyResources(Me.AgeSpinEdit, "AgeSpinEdit")
        Me.AgeSpinEdit.EnterMoveNextControl = True
        Me.AgeSpinEdit.Name = "AgeSpinEdit"
        Me.AgeSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("AgeSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.AgeSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.AgeSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("AgeSpinEdit.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.AgeSpinEdit.Properties.IsFloatValue = False
        Me.AgeSpinEdit.Properties.MaxValue = New Decimal(New Integer() {140, 0, 0, 0})
        Me.AgeSpinEdit.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'PhoneTextEdit
        '
        Me.PhoneTextEdit.EnterMoveNextControl = True
        resources.ApplyResources(Me.PhoneTextEdit, "PhoneTextEdit")
        Me.PhoneTextEdit.Name = "PhoneTextEdit"
        Me.PhoneTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PhoneTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PhoneTextEdit.Properties.Appearance.Options.UseFont = True
        '
        'lblPNum
        '
        Me.lblPNum.Appearance.Font = CType(resources.GetObject("lblPNum.Appearance.Font"), System.Drawing.Font)
        Me.lblPNum.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblPNum, "lblPNum")
        Me.lblPNum.Name = "lblPNum"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Name = "LabelControl4"
        '
        'AddressTextEdit
        '
        Me.AddressTextEdit.EnterMoveNextControl = True
        resources.ApplyResources(Me.AddressTextEdit, "AddressTextEdit")
        Me.AddressTextEdit.Name = "AddressTextEdit"
        Me.AddressTextEdit.Properties.Appearance.Font = CType(resources.GetObject("AddressTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.AddressTextEdit.Properties.Appearance.Options.UseFont = True
        '
        'HealthTextBox
        '
        Me.HealthTextBox.EnterMoveNextControl = True
        resources.ApplyResources(Me.HealthTextBox, "HealthTextBox")
        Me.HealthTextBox.Name = "HealthTextBox"
        Me.HealthTextBox.Properties.Appearance.Font = CType(resources.GetObject("HealthTextBox.Properties.Appearance.Font"), System.Drawing.Font)
        Me.HealthTextBox.Properties.Appearance.Options.UseFont = True
        '
        'TreatCheckBox
        '
        resources.ApplyResources(Me.TreatCheckBox, "TreatCheckBox")
        Me.TreatCheckBox.Name = "TreatCheckBox"
        Me.TreatCheckBox.UseVisualStyleBackColor = True
        '
        'ImplantCheckBox
        '
        resources.ApplyResources(Me.ImplantCheckBox, "ImplantCheckBox")
        Me.ImplantCheckBox.Name = "ImplantCheckBox"
        Me.ImplantCheckBox.UseVisualStyleBackColor = True
        '
        'MobileCheckBox
        '
        resources.ApplyResources(Me.MobileCheckBox, "MobileCheckBox")
        Me.MobileCheckBox.Name = "MobileCheckBox"
        Me.MobileCheckBox.UseVisualStyleBackColor = True
        '
        'OrthoCheckBox
        '
        resources.ApplyResources(Me.OrthoCheckBox, "OrthoCheckBox")
        Me.OrthoCheckBox.Name = "OrthoCheckBox"
        Me.OrthoCheckBox.UseVisualStyleBackColor = True
        '
        'StrucCheckBox
        '
        resources.ApplyResources(Me.StrucCheckBox, "StrucCheckBox")
        Me.StrucCheckBox.Name = "StrucCheckBox"
        Me.StrucCheckBox.UseVisualStyleBackColor = True
        '
        'NotesTextEdit
        '
        Me.NotesTextEdit.EnterMoveNextControl = True
        resources.ApplyResources(Me.NotesTextEdit, "NotesTextEdit")
        Me.NotesTextEdit.Name = "NotesTextEdit"
        Me.NotesTextEdit.Properties.Appearance.Font = CType(resources.GetObject("NotesTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.NotesTextEdit.Properties.Appearance.Options.UseFont = True
        '
        'BirthYSpinEdit
        '
        resources.ApplyResources(Me.BirthYSpinEdit, "BirthYSpinEdit")
        Me.BirthYSpinEdit.EnterMoveNextControl = True
        Me.BirthYSpinEdit.Name = "BirthYSpinEdit"
        Me.BirthYSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("BirthYSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.BirthYSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.BirthYSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("BirthYSpinEdit.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.BirthYSpinEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.BirthYSpinEdit.Properties.MaskSettings.Set("mask", "d")
        Me.BirthYSpinEdit.Properties.MaskSettings.Set("hideInsignificantZeros", True)
        Me.BirthYSpinEdit.Properties.MaskSettings.Set("autoHideDecimalSeparator", True)
        '
        'LabelBal
        '
        Me.LabelBal.Appearance.Font = CType(resources.GetObject("LabelBal.Appearance.Font"), System.Drawing.Font)
        Me.LabelBal.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelBal, "LabelBal")
        Me.LabelBal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBS, "Balance", True))
        Me.LabelBal.Name = "LabelBal"
        '
        'txtCount
        '
        resources.ApplyResources(Me.txtCount, "txtCount")
        Me.txtCount.Name = "txtCount"
        Me.txtCount.Properties.Appearance.BackColor = System.Drawing.Color.Honeydew
        Me.txtCount.Properties.Appearance.Font = CType(resources.GetObject("txtCount.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtCount.Properties.Appearance.Options.UseBackColor = True
        Me.txtCount.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl8
        '
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Name = "LabelControl8"
        '
        'SplashScreenManager1
        '
        Me.SplashScreenManager1.ClosingDelay = 500
        '
        'posTxt
        '
        resources.ApplyResources(Me.posTxt, "posTxt")
        Me.posTxt.Name = "posTxt"
        Me.posTxt.Properties.Appearance.BackColor = System.Drawing.Color.MintCream
        Me.posTxt.Properties.Appearance.Font = CType(resources.GetObject("posTxt.Properties.Appearance.Font"), System.Drawing.Font)
        Me.posTxt.Properties.Appearance.Options.UseBackColor = True
        Me.posTxt.Properties.Appearance.Options.UseFont = True
        '
        'btnAdultChart
        '
        Me.btnAdultChart.Appearance.Font = CType(resources.GetObject("btnAdultChart.Appearance.Font"), System.Drawing.Font)
        Me.btnAdultChart.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnAdultChart, "btnAdultChart")
        Me.btnAdultChart.Name = "btnAdultChart"
        Me.btnAdultChart.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.[True]
        '
        'LabelAge
        '
        Me.LabelAge.Appearance.Font = CType(resources.GetObject("LabelAge.Appearance.Font"), System.Drawing.Font)
        Me.LabelAge.Appearance.Options.UseFont = True
        Me.LabelAge.Appearance.Options.UseTextOptions = True
        Me.LabelAge.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelAge.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabelAge, "LabelAge")
        Me.LabelAge.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBS, "Age", True))
        Me.LabelAge.Name = "LabelAge"
        '
        'IsKidLabel
        '
        Me.IsKidLabel.Appearance.Font = CType(resources.GetObject("IsKidLabel.Appearance.Font"), System.Drawing.Font)
        Me.IsKidLabel.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.IsKidLabel.Appearance.Options.UseFont = True
        Me.IsKidLabel.Appearance.Options.UseForeColor = True
        Me.IsKidLabel.Appearance.Options.UseTextOptions = True
        Me.IsKidLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.IsKidLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.IsKidLabel, "IsKidLabel")
        Me.IsKidLabel.Name = "IsKidLabel"
        '
        'btnResetKid
        '
        Me.btnResetKid.Appearance.Font = CType(resources.GetObject("btnResetKid.Appearance.Font"), System.Drawing.Font)
        Me.btnResetKid.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnResetKid, "btnResetKid")
        Me.btnResetKid.Name = "btnResetKid"
        '
        'LabelAddress
        '
        Me.LabelAddress.Appearance.Font = CType(resources.GetObject("LabelAddress.Appearance.Font"), System.Drawing.Font)
        Me.LabelAddress.Appearance.Options.UseFont = True
        Me.LabelAddress.Appearance.Options.UseTextOptions = True
        Me.LabelAddress.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.LabelAddress, "LabelAddress")
        Me.LabelAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBS, "Address", True))
        Me.LabelAddress.Name = "LabelAddress"
        '
        'lblPatientNum
        '
        Me.lblPatientNum.Appearance.Font = CType(resources.GetObject("lblPatientNum.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientNum.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblPatientNum, "lblPatientNum")
        Me.lblPatientNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PatientBS, "PatientNumber", True))
        Me.lblPatientNum.Name = "lblPatientNum"
        '
        'Side1
        '
        Me.Side1.Controls.Add(Me.lblPatientName)
        Me.Side1.Controls.Add(Me.lblTxtTrts)
        Me.Side1.Controls.Add(Me.lblTrts)
        Me.Side1.Controls.Add(Me.lblTxtPays)
        Me.Side1.Controls.Add(Me.lblPays)
        Me.Side1.Controls.Add(Me.lbltxtBal)
        Me.Side1.Controls.Add(Me.LabelBal)
        Me.Side1.Controls.Add(Me.LabelAddress)
        Me.Side1.Controls.Add(Me.LabelAge)
        resources.ApplyResources(Me.Side1, "Side1")
        Me.Side1.Name = "Side1"
        '
        'lblPatientName
        '
        Me.lblPatientName.Appearance.Font = CType(resources.GetObject("lblPatientName.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientName.Appearance.ForeColor = System.Drawing.Color.Magenta
        Me.lblPatientName.Appearance.Options.UseFont = True
        Me.lblPatientName.Appearance.Options.UseForeColor = True
        Me.lblPatientName.Appearance.Options.UseTextOptions = True
        Me.lblPatientName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblPatientName, "lblPatientName")
        Me.lblPatientName.Name = "lblPatientName"
        '
        'lblTxtTrts
        '
        Me.lblTxtTrts.Appearance.Font = CType(resources.GetObject("lblTxtTrts.Appearance.Font"), System.Drawing.Font)
        Me.lblTxtTrts.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblTxtTrts.Appearance.Options.UseFont = True
        Me.lblTxtTrts.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblTxtTrts, "lblTxtTrts")
        Me.lblTxtTrts.Name = "lblTxtTrts"
        '
        'lblTrts
        '
        Me.lblTrts.Appearance.Font = CType(resources.GetObject("lblTrts.Appearance.Font"), System.Drawing.Font)
        Me.lblTrts.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblTrts.Appearance.Options.UseFont = True
        Me.lblTrts.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblTrts, "lblTrts")
        Me.lblTrts.Name = "lblTrts"
        '
        'lblTxtPays
        '
        Me.lblTxtPays.Appearance.Font = CType(resources.GetObject("lblTxtPays.Appearance.Font"), System.Drawing.Font)
        Me.lblTxtPays.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTxtPays.Appearance.Options.UseFont = True
        Me.lblTxtPays.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblTxtPays, "lblTxtPays")
        Me.lblTxtPays.Name = "lblTxtPays"
        '
        'lblPays
        '
        Me.lblPays.Appearance.Font = CType(resources.GetObject("lblPays.Appearance.Font"), System.Drawing.Font)
        Me.lblPays.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPays.Appearance.Options.UseFont = True
        Me.lblPays.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblPays, "lblPays")
        Me.lblPays.Name = "lblPays"
        '
        'lbltxtBal
        '
        Me.lbltxtBal.Appearance.Font = CType(resources.GetObject("lbltxtBal.Appearance.Font"), System.Drawing.Font)
        Me.lbltxtBal.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lbltxtBal, "lbltxtBal")
        Me.lbltxtBal.Name = "lbltxtBal"
        '
        'txtPatientName
        '
        resources.ApplyResources(Me.txtPatientName, "txtPatientName")
        Me.txtPatientName.Name = "txtPatientName"
        Me.txtPatientName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtPatientName.Properties.Appearance.Font = CType(resources.GetObject("txtPatientName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtPatientName.Properties.Appearance.Options.UseBackColor = True
        Me.txtPatientName.Properties.Appearance.Options.UseFont = True
        Me.txtPatientName.Properties.AutoHeight = CType(resources.GetObject("txtPatientName.Properties.AutoHeight"), Boolean)
        Me.txtPatientName.Properties.NullValuePrompt = resources.GetString("txtPatientName.Properties.NullValuePrompt")
        '
        'btnSearchResults
        '
        Me.btnSearchResults.Appearance.Font = CType(resources.GetObject("btnSearchResults.Appearance.Font"), System.Drawing.Font)
        Me.btnSearchResults.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSearchResults, "btnSearchResults")
        Me.btnSearchResults.Name = "btnSearchResults"
        Me.btnSearchResults.TabStop = False
        '
        'FlyoutPatientInfo
        '
        Me.FlyoutPatientInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.FlyoutPatientInfo.Controls.Add(Me.FlyoutPanelControl1)
        resources.ApplyResources(Me.FlyoutPatientInfo, "FlyoutPatientInfo")
        Me.FlyoutPatientInfo.Name = "FlyoutPatientInfo"
        Me.FlyoutPatientInfo.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual
        Me.FlyoutPatientInfo.Options.AnimationType = DevExpress.Utils.Win.PopupToolWindowAnimation.Fade
        Me.FlyoutPatientInfo.Options.Location = New System.Drawing.Point(299, 74)
        Me.FlyoutPatientInfo.OptionsBeakPanel.CloseOnOuterClick = False
        Me.FlyoutPatientInfo.OptionsButtonPanel.AppearanceButton.Normal.Font = CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.AppearanceButton.Normal.Font"), System.Drawing.Font)
        Me.FlyoutPatientInfo.OptionsButtonPanel.AppearanceButton.Normal.Options.UseFont = True
        ButtonImageOptions1.Image = Global.DentistX.My.Resources.Resources.cancel_16x16
        Me.FlyoutPatientInfo.OptionsButtonPanel.Buttons.AddRange(New DevExpress.XtraEditors.ButtonPanel.IBaseButton() {New DevExpress.Utils.PeekFormButton(resources.GetString("FlyoutPatientInfo.OptionsButtonPanel.Buttons"), CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.Buttons1"), Boolean), ButtonImageOptions1, CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.Buttons2"), DevExpress.XtraBars.Docking2010.ButtonStyle), resources.GetString("FlyoutPatientInfo.OptionsButtonPanel.Buttons3"), CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.Buttons4"), Integer), CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.Buttons5"), Boolean), CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.Buttons6"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.Buttons7"), Boolean), CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.Buttons8"), Boolean), CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.Buttons9"), Boolean), CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.Buttons10"), Object), CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.Buttons11"), Integer), CType(resources.GetObject("FlyoutPatientInfo.OptionsButtonPanel.Buttons12"), Boolean))})
        Me.FlyoutPatientInfo.OptionsButtonPanel.ShowButtonPanel = True
        Me.FlyoutPatientInfo.OwnerControl = Me
        '
        'FlyoutPanelControl1
        '
        Me.FlyoutPanelControl1.Controls.Add(Me.grpPatientInfo)
        resources.ApplyResources(Me.FlyoutPanelControl1, "FlyoutPanelControl1")
        Me.FlyoutPanelControl1.FlyoutPanel = Me.FlyoutPatientInfo
        Me.FlyoutPanelControl1.Name = "FlyoutPanelControl1"
        '
        'btnWhatsSend
        '
        Me.btnWhatsSend.ImageOptions.ImageKey = resources.GetString("btnWhatsSend.ImageOptions.ImageKey")
        Me.btnWhatsSend.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnWhatsSend.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.whatsapp
        Me.btnWhatsSend.ImageOptions.SvgImageSize = New System.Drawing.Size(20, 20)
        resources.ApplyResources(Me.btnWhatsSend, "btnWhatsSend")
        Me.btnWhatsSend.Name = "btnWhatsSend"
        '
        'Navigator3
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnSearchResults)
        Me.Controls.Add(Me.txtPatientName)
        Me.Controls.Add(Me.lblPatientNum)
        Me.Controls.Add(Me.btnResetKid)
        Me.Controls.Add(Me.btnWhatsSend)
        Me.Controls.Add(Me.FlyoutPatientInfo)
        Me.Controls.Add(Me.IsKidLabel)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.btnAdultChart)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.posTxt)
        Me.Controls.Add(Me.txtCount)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.PopupPatient)
        Me.Controls.Add(Me.btNewPatient)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.Side1)
        Me.Name = "Navigator3"
        CType(Me.PatientBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpPatientInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPatientInfo.ResumeLayout(False)
        Me.grpPatientInfo.PerformLayout()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.SurgeryBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.posTxt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Side1.ResumeLayout(False)
        Me.Side1.PerformLayout()
        CType(Me.txtPatientName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FlyoutPatientInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlyoutPatientInfo.ResumeLayout(False)
        CType(Me.FlyoutPanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlyoutPanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btNewPatient As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLast As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrev As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFirst As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PopupPatient As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LabelBal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SurgeryBindingSource As Windows.Forms.BindingSource
    Friend WithEvents PatientBS As Windows.Forms.BindingSource
    Friend WithEvents SplashScreenManager1 As DevExpress.XtraSplashScreen.SplashScreenManager
    Friend WithEvents posTxt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnAdultChart As DevExpress.XtraEditors.CheckButton
    Friend WithEvents LabelAge As DevExpress.XtraEditors.LabelControl
    Friend WithEvents IsKidLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnResetKid As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelAddress As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPatientNum As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Side1 As DevExpress.XtraEditors.SidePanel
    Friend WithEvents txtPatientName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grpPatientInfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents DiagCheck As CheckBox
    Friend WithEvents RadioFemale As RadioButton
    Friend WithEvents RadioMale As RadioButton
    Friend WithEvents CboCity As CityCombo
    Friend WithEvents CboHealth As HlthCombo
    Friend WithEvents lblAge As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBalance As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnDeletePatient As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnUpdatePatient As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PatientNameTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PatientIDEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PicBox As PictureBox
    Friend WithEvents SexTextBox As DevExpress.XtraEditors.TextEdit
    Friend WithEvents AgeSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents PhoneTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblPNum As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents AddressTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents HealthTextBox As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TreatCheckBox As CheckBox
    Friend WithEvents ImplantCheckBox As CheckBox
    Friend WithEvents MobileCheckBox As CheckBox
    Friend WithEvents OrthoCheckBox As CheckBox
    Friend WithEvents StrucCheckBox As CheckBox
    Friend WithEvents NotesTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BirthYSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FlyoutPatientInfo As DevExpress.Utils.FlyoutPanel
    Friend WithEvents FlyoutPanelControl1 As DevExpress.Utils.FlyoutPanelControl
    Friend WithEvents btnWhatsSend As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblTxtTrts As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTrts As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTxtPays As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPays As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbltxtBal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPatientName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSearchResults As DevExpress.XtraEditors.SimpleButton
End Class
