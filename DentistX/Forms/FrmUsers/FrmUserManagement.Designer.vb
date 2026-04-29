<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmUserManagement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmUserManagement))
        Me.TxtUsername = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPassword = New DevExpress.XtraEditors.TextEdit()
        Me.TxtConfirmPassword = New DevExpress.XtraEditors.TextEdit()
        Me.CboUserLevel = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CboUserGroup = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnUpdate = New DevExpress.XtraEditors.SimpleButton()
        Me.ErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.CboDoctor = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelDoctor = New DevExpress.XtraEditors.LabelControl()
        CType(Me.TxtUsername.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtConfirmPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboUserLevel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboUserGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboDoctor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtUsername
        '
        resources.ApplyResources(Me.TxtUsername, "TxtUsername")
        Me.TxtUsername.Name = "TxtUsername"
        Me.TxtUsername.Properties.Appearance.Font = CType(resources.GetObject("TxtUsername.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtUsername.Properties.Appearance.Options.UseFont = True
        '
        'TxtPassword
        '
        resources.ApplyResources(Me.TxtPassword, "TxtPassword")
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.Properties.Appearance.Font = CType(resources.GetObject("TxtPassword.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPassword.Properties.Appearance.Options.UseFont = True
        '
        'TxtConfirmPassword
        '
        resources.ApplyResources(Me.TxtConfirmPassword, "TxtConfirmPassword")
        Me.TxtConfirmPassword.Name = "TxtConfirmPassword"
        Me.TxtConfirmPassword.Properties.Appearance.Font = CType(resources.GetObject("TxtConfirmPassword.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtConfirmPassword.Properties.Appearance.Options.UseFont = True
        '
        'CboUserLevel
        '
        resources.ApplyResources(Me.CboUserLevel, "CboUserLevel")
        Me.CboUserLevel.Name = "CboUserLevel"
        Me.CboUserLevel.Properties.Appearance.Font = CType(resources.GetObject("CboUserLevel.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboUserLevel.Properties.Appearance.Options.UseFont = True
        Me.CboUserLevel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboUserLevel.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'CboUserGroup
        '
        resources.ApplyResources(Me.CboUserGroup, "CboUserGroup")
        Me.CboUserGroup.Name = "CboUserGroup"
        Me.CboUserGroup.Properties.Appearance.Font = CType(resources.GetObject("CboUserGroup.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboUserGroup.Properties.Appearance.Options.UseFont = True
        Me.CboUserGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboUserGroup.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'BtnSave
        '
        resources.ApplyResources(Me.BtnSave, "BtnSave")
        Me.BtnSave.Appearance.Font = CType(resources.GetObject("BtnSave.Appearance.Font"), System.Drawing.Font)
        Me.BtnSave.Appearance.Options.UseFont = True
        Me.BtnSave.ImageOptions.ImageKey = resources.GetString("BtnSave.ImageOptions.ImageKey")
        Me.BtnSave.Name = "BtnSave"
        '
        'BtnUpdate
        '
        resources.ApplyResources(Me.BtnUpdate, "BtnUpdate")
        Me.BtnUpdate.Appearance.Font = CType(resources.GetObject("BtnUpdate.Appearance.Font"), System.Drawing.Font)
        Me.BtnUpdate.Appearance.Options.UseFont = True
        Me.BtnUpdate.ImageOptions.ImageKey = resources.GetString("BtnUpdate.ImageOptions.ImageKey")
        Me.BtnUpdate.Name = "BtnUpdate"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'LabelControl4
        '
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Name = "LabelControl4"
        '
        'LabelControl5
        '
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Name = "LabelControl5"
        '
        'BtnCancel
        '
        resources.ApplyResources(Me.BtnCancel, "BtnCancel")
        Me.BtnCancel.Appearance.Font = CType(resources.GetObject("BtnCancel.Appearance.Font"), System.Drawing.Font)
        Me.BtnCancel.Appearance.Options.UseFont = True
        Me.BtnCancel.ImageOptions.ImageKey = resources.GetString("BtnCancel.ImageOptions.ImageKey")
        Me.BtnCancel.Name = "BtnCancel"
        '
        'CboDoctor
        '
        resources.ApplyResources(Me.CboDoctor, "CboDoctor")
        Me.CboDoctor.Name = "CboDoctor"
        Me.CboDoctor.Properties.Appearance.Font = CType(resources.GetObject("CboDoctor.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboDoctor.Properties.Appearance.Options.UseFont = True
        Me.CboDoctor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboDoctor.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelDoctor
        '
        resources.ApplyResources(Me.LabelDoctor, "LabelDoctor")
        Me.LabelDoctor.Appearance.Font = CType(resources.GetObject("LabelDoctor.Appearance.Font"), System.Drawing.Font)
        Me.LabelDoctor.Appearance.Options.UseFont = True
        Me.LabelDoctor.Name = "LabelDoctor"
        '
        'FrmUserManagement
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.LabelDoctor)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.CboDoctor)
        Me.Controls.Add(Me.CboUserGroup)
        Me.Controls.Add(Me.CboUserLevel)
        Me.Controls.Add(Me.TxtConfirmPassword)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.TxtUsername)
        Me.Name = "FrmUserManagement"
        CType(Me.TxtUsername.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtConfirmPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboUserLevel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboUserGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboDoctor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtUsername As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtConfirmPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CboUserLevel As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CboUserGroup As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents BtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelDoctor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CboDoctor As DevExpress.XtraEditors.ComboBoxEdit
End Class
