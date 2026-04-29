<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChangePassword
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmChangePassword))
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnChangePassword = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtConfirmPassword = New DevExpress.XtraEditors.TextEdit()
        Me.TxtNewPassword = New DevExpress.XtraEditors.TextEdit()
        Me.TxtOldPassword = New DevExpress.XtraEditors.TextEdit()
        Me.ErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.txtUser = New DevExpress.XtraEditors.TextEdit()
        CType(Me.TxtConfirmPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNewPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtOldPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnCancel
        '
        resources.ApplyResources(Me.BtnCancel, "BtnCancel")
        Me.BtnCancel.Appearance.Font = CType(resources.GetObject("BtnCancel.Appearance.Font"), System.Drawing.Font)
        Me.BtnCancel.Appearance.Options.UseFont = True
        Me.BtnCancel.ImageOptions.ImageKey = resources.GetString("BtnCancel.ImageOptions.ImageKey")
        Me.BtnCancel.Name = "BtnCancel"
        '
        'BtnChangePassword
        '
        resources.ApplyResources(Me.BtnChangePassword, "BtnChangePassword")
        Me.BtnChangePassword.Appearance.Font = CType(resources.GetObject("BtnChangePassword.Appearance.Font"), System.Drawing.Font)
        Me.BtnChangePassword.Appearance.Options.UseFont = True
        Me.BtnChangePassword.ImageOptions.ImageKey = resources.GetString("BtnChangePassword.ImageOptions.ImageKey")
        Me.BtnChangePassword.Name = "BtnChangePassword"
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
        'TxtNewPassword
        '
        resources.ApplyResources(Me.TxtNewPassword, "TxtNewPassword")
        Me.TxtNewPassword.EnterMoveNextControl = True
        Me.TxtNewPassword.Name = "TxtNewPassword"
        Me.TxtNewPassword.Properties.Appearance.Font = CType(resources.GetObject("TxtNewPassword.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtNewPassword.Properties.Appearance.Options.UseFont = True
        Me.TxtNewPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        '
        'TxtOldPassword
        '
        resources.ApplyResources(Me.TxtOldPassword, "TxtOldPassword")
        Me.TxtOldPassword.EnterMoveNextControl = True
        Me.TxtOldPassword.Name = "TxtOldPassword"
        Me.TxtOldPassword.Properties.Appearance.Font = CType(resources.GetObject("TxtOldPassword.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtOldPassword.Properties.Appearance.Options.UseFont = True
        Me.TxtOldPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
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
        'txtUser
        '
        resources.ApplyResources(Me.txtUser, "txtUser")
        Me.txtUser.EnterMoveNextControl = True
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Properties.Appearance.Font = CType(resources.GetObject("txtUser.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtUser.Properties.Appearance.Options.UseFont = True
        '
        'FrmChangePassword
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnChangePassword)
        Me.Controls.Add(Me.TxtConfirmPassword)
        Me.Controls.Add(Me.TxtOldPassword)
        Me.Controls.Add(Me.TxtNewPassword)
        Me.Name = "FrmChangePassword"
        CType(Me.TxtConfirmPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNewPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtOldPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnChangePassword As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtConfirmPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtNewPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtOldPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents Timer1 As Windows.Forms.Timer
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtUser As DevExpress.XtraEditors.TextEdit
End Class
