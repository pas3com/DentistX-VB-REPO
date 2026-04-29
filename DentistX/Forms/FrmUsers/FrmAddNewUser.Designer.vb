<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAddNewUser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAddNewUser))
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.BtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtConfirmPassword = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPassword = New DevExpress.XtraEditors.TextEdit()
        Me.TxtUsername = New DevExpress.XtraEditors.TextEdit()
        Me.LabelDoctor = New DevExpress.XtraEditors.LabelControl()
        Me.LabelSec = New DevExpress.XtraEditors.LabelControl()
        Me.CboUserLevel = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.RadioLink = New DevExpress.XtraEditors.RadioGroup()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelEmp = New DevExpress.XtraEditors.LabelControl()
        Me.CboDoctor = New DentistX.DoctorsCombo()
        Me.CboSec = New DentistX.SecretariesCombo()
        Me.CboUserGroup = New DentistX.GroupsCombo()
        Me.CboEmps = New DentistX.EmpsCombo()
        CType(Me.TxtConfirmPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtUsername.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboUserLevel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioLink.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnCancel
        '
        resources.ApplyResources(Me.BtnCancel, "BtnCancel")
        Me.BtnCancel.Appearance.Font = CType(resources.GetObject("BtnCancel.Appearance.Font"), System.Drawing.Font)
        Me.BtnCancel.Appearance.Options.UseFont = True
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.ImageOptions.ImageKey = resources.GetString("BtnCancel.ImageOptions.ImageKey")
        Me.BtnCancel.Name = "BtnCancel"
        '
        'LabelControl5
        '
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Name = "LabelControl5"
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
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'BtnSave
        '
        resources.ApplyResources(Me.BtnSave, "BtnSave")
        Me.BtnSave.Appearance.Font = CType(resources.GetObject("BtnSave.Appearance.Font"), System.Drawing.Font)
        Me.BtnSave.Appearance.Options.UseFont = True
        Me.BtnSave.ImageOptions.ImageKey = resources.GetString("BtnSave.ImageOptions.ImageKey")
        Me.BtnSave.Name = "BtnSave"
        '
        'TxtConfirmPassword
        '
        resources.ApplyResources(Me.TxtConfirmPassword, "TxtConfirmPassword")
        Me.TxtConfirmPassword.EnterMoveNextControl = True
        Me.TxtConfirmPassword.Name = "TxtConfirmPassword"
        Me.TxtConfirmPassword.Properties.Appearance.Font = CType(resources.GetObject("TxtConfirmPassword.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtConfirmPassword.Properties.Appearance.Options.UseFont = True
        Me.TxtConfirmPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        '
        'TxtPassword
        '
        resources.ApplyResources(Me.TxtPassword, "TxtPassword")
        Me.TxtPassword.EnterMoveNextControl = True
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.Properties.Appearance.Font = CType(resources.GetObject("TxtPassword.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPassword.Properties.Appearance.Options.UseFont = True
        Me.TxtPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        '
        'TxtUsername
        '
        resources.ApplyResources(Me.TxtUsername, "TxtUsername")
        Me.TxtUsername.EnterMoveNextControl = True
        Me.TxtUsername.Name = "TxtUsername"
        Me.TxtUsername.Properties.Appearance.Font = CType(resources.GetObject("TxtUsername.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtUsername.Properties.Appearance.Options.UseFont = True
        '
        'LabelDoctor
        '
        resources.ApplyResources(Me.LabelDoctor, "LabelDoctor")
        Me.LabelDoctor.Appearance.Font = CType(resources.GetObject("LabelDoctor.Appearance.Font"), System.Drawing.Font)
        Me.LabelDoctor.Appearance.Options.UseFont = True
        Me.LabelDoctor.Name = "LabelDoctor"
        '
        'LabelSec
        '
        resources.ApplyResources(Me.LabelSec, "LabelSec")
        Me.LabelSec.Appearance.Font = CType(resources.GetObject("LabelSec.Appearance.Font"), System.Drawing.Font)
        Me.LabelSec.Appearance.Options.UseFont = True
        Me.LabelSec.Name = "LabelSec"
        '
        'CboUserLevel
        '
        resources.ApplyResources(Me.CboUserLevel, "CboUserLevel")
        Me.CboUserLevel.EnterMoveNextControl = True
        Me.CboUserLevel.Name = "CboUserLevel"
        Me.CboUserLevel.Properties.Appearance.Font = CType(resources.GetObject("CboUserLevel.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboUserLevel.Properties.Appearance.Options.UseFont = True
        Me.CboUserLevel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboUserLevel.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.CboUserLevel.Properties.Items.AddRange(New Object() {resources.GetString("CboUserLevel.Properties.Items"), resources.GetString("CboUserLevel.Properties.Items1"), resources.GetString("CboUserLevel.Properties.Items2"), resources.GetString("CboUserLevel.Properties.Items3"), resources.GetString("CboUserLevel.Properties.Items4")})
        '
        'RadioLink
        '
        resources.ApplyResources(Me.RadioLink, "RadioLink")
        Me.RadioLink.EnterMoveNextControl = True
        Me.RadioLink.Name = "RadioLink"
        Me.RadioLink.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadioLink.Properties.Appearance.Font = CType(resources.GetObject("RadioLink.Properties.Appearance.Font"), System.Drawing.Font)
        Me.RadioLink.Properties.Appearance.Options.UseBackColor = True
        Me.RadioLink.Properties.Appearance.Options.UseFont = True
        Me.RadioLink.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioLink.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.RadioLink.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLink.Properties.Items"), Object), resources.GetString("RadioLink.Properties.Items1"), CType(resources.GetObject("RadioLink.Properties.Items2"), Boolean), CType(resources.GetObject("RadioLink.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLink.Properties.Items4"), Object), resources.GetString("RadioLink.Properties.Items5"), CType(resources.GetObject("RadioLink.Properties.Items6"), Boolean), CType(resources.GetObject("RadioLink.Properties.Items7"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLink.Properties.Items8"), Object), resources.GetString("RadioLink.Properties.Items9"), CType(resources.GetObject("RadioLink.Properties.Items10"), Boolean), CType(resources.GetObject("RadioLink.Properties.Items11"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLink.Properties.Items12"), Object), resources.GetString("RadioLink.Properties.Items13"), CType(resources.GetObject("RadioLink.Properties.Items14"), Boolean), CType(resources.GetObject("RadioLink.Properties.Items15"), Object))})
        Me.RadioLink.Properties.Padding = New System.Windows.Forms.Padding(2)
        '
        'LabelControl6
        '
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Name = "LabelControl6"
        '
        'LabelEmp
        '
        resources.ApplyResources(Me.LabelEmp, "LabelEmp")
        Me.LabelEmp.Appearance.Font = CType(resources.GetObject("LabelEmp.Appearance.Font"), System.Drawing.Font)
        Me.LabelEmp.Appearance.Options.UseFont = True
        Me.LabelEmp.Name = "LabelEmp"
        '
        'CboDoctor
        '
        resources.ApplyResources(Me.CboDoctor, "CboDoctor")
        Me.CboDoctor.DrID = 0
        Me.CboDoctor.DrName = Nothing
        Me.CboDoctor.Name = "CboDoctor"
        '
        'CboSec
        '
        resources.ApplyResources(Me.CboSec, "CboSec")
        Me.CboSec.Name = "CboSec"
        Me.CboSec.SecID = 0
        Me.CboSec.SecName = Nothing
        '
        'CboUserGroup
        '
        resources.ApplyResources(Me.CboUserGroup, "CboUserGroup")
        Me.CboUserGroup.GroupID = 0
        Me.CboUserGroup.GroupName = Nothing
        Me.CboUserGroup.Name = "CboUserGroup"
        '
        'CboEmps
        '
        resources.ApplyResources(Me.CboEmps, "CboEmps")
        Me.CboEmps.EmpID = 0
        Me.CboEmps.EmpName = Nothing
        Me.CboEmps.Name = "CboEmps"
        '
        'FrmAddNewUser
        '
        Me.AcceptButton = Me.BtnSave
        resources.ApplyResources(Me, "$this")
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.Controls.Add(Me.CboEmps)
        Me.Controls.Add(Me.LabelEmp)
        Me.Controls.Add(Me.RadioLink)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.CboUserGroup)
        Me.Controls.Add(Me.CboSec)
        Me.Controls.Add(Me.LabelSec)
        Me.Controls.Add(Me.CboDoctor)
        Me.Controls.Add(Me.LabelDoctor)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.CboUserLevel)
        Me.Controls.Add(Me.TxtConfirmPassword)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.TxtUsername)
        Me.Name = "FrmAddNewUser"
        CType(Me.TxtConfirmPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtUsername.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboUserLevel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioLink.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtConfirmPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtUsername As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelDoctor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CboDoctor As DoctorsCombo
    Friend WithEvents LabelSec As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CboSec As SecretariesCombo
    Friend WithEvents CboUserGroup As GroupsCombo
    Friend WithEvents CboUserLevel As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents RadioLink As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CboEmps As EmpsCombo
    Friend WithEvents LabelEmp As DevExpress.XtraEditors.LabelControl
End Class
