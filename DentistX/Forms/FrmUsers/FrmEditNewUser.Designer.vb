<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEditNewUser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEditNewUser))
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.BtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.CboUserLevel = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TxtUsername = New DevExpress.XtraEditors.TextEdit()
        Me.LabelDoctor = New DevExpress.XtraEditors.LabelControl()
        Me.LabelSec = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.RadioLink = New DevExpress.XtraEditors.RadioGroup()
        Me.LabelEmp = New DevExpress.XtraEditors.LabelControl()
        Me.CboUserGroup = New DentistX.GroupsCombo()
        Me.CboEmps = New DentistX.EmpsCombo()
        Me.CboSec = New DentistX.SecretariesCombo()
        Me.CboDoctor = New DentistX.DoctorsCombo()
        CType(Me.CboUserLevel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtUsername.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioLink.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnCancel
        '
        Me.BtnCancel.Appearance.Font = CType(resources.GetObject("BtnCancel.Appearance.Font"), System.Drawing.Font)
        Me.BtnCancel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnCancel, "BtnCancel")
        Me.BtnCancel.Name = "BtnCancel"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Name = "LabelControl5"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Name = "LabelControl4"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Name = "LabelControl2"
        '
        'BtnSave
        '
        Me.BtnSave.Appearance.Font = CType(resources.GetObject("BtnSave.Appearance.Font"), System.Drawing.Font)
        Me.BtnSave.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnSave, "BtnSave")
        Me.BtnSave.Name = "BtnSave"
        '
        'CboUserLevel
        '
        Me.CboUserLevel.EnterMoveNextControl = True
        resources.ApplyResources(Me.CboUserLevel, "CboUserLevel")
        Me.CboUserLevel.Name = "CboUserLevel"
        Me.CboUserLevel.Properties.Appearance.Font = CType(resources.GetObject("CboUserLevel.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboUserLevel.Properties.Appearance.Options.UseFont = True
        Me.CboUserLevel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboUserLevel.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'TxtUsername
        '
        Me.TxtUsername.EnterMoveNextControl = True
        resources.ApplyResources(Me.TxtUsername, "TxtUsername")
        Me.TxtUsername.Name = "TxtUsername"
        Me.TxtUsername.Properties.Appearance.Font = CType(resources.GetObject("TxtUsername.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtUsername.Properties.Appearance.Options.UseFont = True
        '
        'LabelDoctor
        '
        Me.LabelDoctor.Appearance.Font = CType(resources.GetObject("LabelDoctor.Appearance.Font"), System.Drawing.Font)
        Me.LabelDoctor.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelDoctor, "LabelDoctor")
        Me.LabelDoctor.Name = "LabelDoctor"
        '
        'LabelSec
        '
        Me.LabelSec.Appearance.Font = CType(resources.GetObject("LabelSec.Appearance.Font"), System.Drawing.Font)
        Me.LabelSec.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelSec, "LabelSec")
        Me.LabelSec.Name = "LabelSec"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Name = "LabelControl6"
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
        Me.RadioLink.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLink.Properties.Items"), Object), resources.GetString("RadioLink.Properties.Items1"), CType(resources.GetObject("RadioLink.Properties.Items2"), Boolean), CType(resources.GetObject("RadioLink.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLink.Properties.Items4"), Object), resources.GetString("RadioLink.Properties.Items5")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLink.Properties.Items6"), Object), resources.GetString("RadioLink.Properties.Items7")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLink.Properties.Items8"), Object), resources.GetString("RadioLink.Properties.Items9"))})
        Me.RadioLink.Properties.Padding = New System.Windows.Forms.Padding(2)
        '
        'LabelEmp
        '
        Me.LabelEmp.Appearance.Font = CType(resources.GetObject("LabelEmp.Appearance.Font"), System.Drawing.Font)
        Me.LabelEmp.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelEmp, "LabelEmp")
        Me.LabelEmp.Name = "LabelEmp"
        '
        'CboUserGroup
        '
        Me.CboUserGroup.BtnAddVisible = True
        Me.CboUserGroup.BtnSearchVisible = True
        Me.CboUserGroup.Filter = ""
        Me.CboUserGroup.GroupID = 1
        Me.CboUserGroup.GroupName = Nothing
        resources.ApplyResources(Me.CboUserGroup, "CboUserGroup")
        Me.CboUserGroup.Name = "CboUserGroup"
        '
        'CboEmps
        '
        Me.CboEmps.BtnAddVisible = True
        Me.CboEmps.BtnSearchVisible = True
        Me.CboEmps.EmpID = 1
        Me.CboEmps.EmpName = "gdgfd"
        Me.CboEmps.Filter = ""
        resources.ApplyResources(Me.CboEmps, "CboEmps")
        Me.CboEmps.Name = "CboEmps"
        '
        'CboSec
        '
        Me.CboSec.BtnAddVisible = True
        Me.CboSec.BtnSearchVisible = True
        Me.CboSec.Filter = ""
        resources.ApplyResources(Me.CboSec, "CboSec")
        Me.CboSec.Name = "CboSec"
        Me.CboSec.SecID = 1
        Me.CboSec.SecName = Nothing
        '
        'CboDoctor
        '
        Me.CboDoctor.BtnAddVisible = True
        Me.CboDoctor.BtnSearchVisible = True
        Me.CboDoctor.DrID = 1
        Me.CboDoctor.DrName = "Dr. Pascal"
        Me.CboDoctor.Filter = ""
        resources.ApplyResources(Me.CboDoctor, "CboDoctor")
        Me.CboDoctor.Name = "CboDoctor"
        '
        'FrmEditNewUser
        '
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CboUserGroup)
        Me.Controls.Add(Me.RadioLink)
        Me.Controls.Add(Me.CboEmps)
        Me.Controls.Add(Me.CboSec)
        Me.Controls.Add(Me.LabelEmp)
        Me.Controls.Add(Me.LabelSec)
        Me.Controls.Add(Me.LabelDoctor)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.CboUserLevel)
        Me.Controls.Add(Me.TxtUsername)
        Me.Controls.Add(Me.CboDoctor)
        Me.Name = "FrmEditNewUser"
        CType(Me.CboUserLevel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtUsername.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioLink.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CboUserLevel As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TxtUsername As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelDoctor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CboEmps As EmpsCombo
    Friend WithEvents CboSec As SecretariesCombo
    Friend WithEvents LabelSec As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CboDoctor As DoctorsCombo
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RadioLink As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents LabelEmp As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CboUserGroup As GroupsCombo
End Class
