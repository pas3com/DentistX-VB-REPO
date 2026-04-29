<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmResetPassword
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmResetPassword))
        Me.TxtNewPassword = New DevExpress.XtraEditors.TextEdit()
        Me.BtnResetPassword = New DevExpress.XtraEditors.SimpleButton()
        Me.CmbUsers = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtUser = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.TxtNewPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbUsers.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtNewPassword
        '
        resources.ApplyResources(Me.TxtNewPassword, "TxtNewPassword")
        Me.TxtNewPassword.Name = "TxtNewPassword"
        Me.TxtNewPassword.Properties.Appearance.Font = CType(resources.GetObject("TxtNewPassword.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtNewPassword.Properties.Appearance.Options.UseFont = True
        Me.TxtNewPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        '
        'BtnResetPassword
        '
        resources.ApplyResources(Me.BtnResetPassword, "BtnResetPassword")
        Me.BtnResetPassword.Appearance.Font = CType(resources.GetObject("BtnResetPassword.Appearance.Font"), System.Drawing.Font)
        Me.BtnResetPassword.Appearance.Options.UseFont = True
        Me.BtnResetPassword.ImageOptions.ImageKey = resources.GetString("BtnResetPassword.ImageOptions.ImageKey")
        Me.BtnResetPassword.Name = "BtnResetPassword"
        '
        'CmbUsers
        '
        resources.ApplyResources(Me.CmbUsers, "CmbUsers")
        Me.CmbUsers.Name = "CmbUsers"
        Me.CmbUsers.Properties.Appearance.Font = CType(resources.GetObject("CmbUsers.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CmbUsers.Properties.Appearance.Options.UseFont = True
        Me.CmbUsers.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CmbUsers.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
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
        'txtUser
        '
        resources.ApplyResources(Me.txtUser, "txtUser")
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Properties.Appearance.Font = CType(resources.GetObject("txtUser.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtUser.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'FrmResetPassword
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.CmbUsers)
        Me.Controls.Add(Me.BtnResetPassword)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.TxtNewPassword)
        Me.Name = "FrmResetPassword"
        CType(Me.TxtNewPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbUsers.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtNewPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BtnResetPassword As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CmbUsers As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtUser As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
End Class
