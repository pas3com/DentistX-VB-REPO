<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLogin))
        Me.TxtUsername = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPassword = New DevExpress.XtraEditors.TextEdit()
        Me.BtnLogin = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lnkForgotPass = New DevExpress.XtraEditors.HyperlinkLabelControl()
        Me.chkRememberMe = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.TxtUsername.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRememberMe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtUsername
        '
        resources.ApplyResources(Me.TxtUsername, "TxtUsername")
        Me.TxtUsername.EnterMoveNextControl = True
        Me.TxtUsername.Name = "TxtUsername"
        Me.TxtUsername.Properties.Appearance.Font = CType(resources.GetObject("TxtUsername.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtUsername.Properties.Appearance.Options.UseFont = True
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
        'BtnLogin
        '
        resources.ApplyResources(Me.BtnLogin, "BtnLogin")
        Me.BtnLogin.Appearance.Font = CType(resources.GetObject("BtnLogin.Appearance.Font"), System.Drawing.Font)
        Me.BtnLogin.Appearance.Options.UseFont = True
        Me.BtnLogin.ImageOptions.ImageKey = resources.GetString("BtnLogin.ImageOptions.ImageKey")
        Me.BtnLogin.Name = "BtnLogin"
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
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Appearance.Options.UseForeColor = True
        Me.LabelControl3.Appearance.Options.UseTextOptions = True
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl3.Name = "LabelControl3"
        '
        'lnkForgotPass
        '
        resources.ApplyResources(Me.lnkForgotPass, "lnkForgotPass")
        Me.lnkForgotPass.Appearance.Font = CType(resources.GetObject("lnkForgotPass.Appearance.Font"), System.Drawing.Font)
        Me.lnkForgotPass.Appearance.Options.UseFont = True
        Me.lnkForgotPass.Name = "lnkForgotPass"
        '
        'chkRememberMe
        '
        resources.ApplyResources(Me.chkRememberMe, "chkRememberMe")
        Me.chkRememberMe.Name = "chkRememberMe"
        Me.chkRememberMe.Properties.Appearance.Font = CType(resources.GetObject("chkRememberMe.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkRememberMe.Properties.Appearance.Options.UseFont = True
        Me.chkRememberMe.Properties.Caption = resources.GetString("chkRememberMe.Properties.Caption")
        Me.chkRememberMe.Properties.DisplayValueChecked = resources.GetString("chkRememberMe.Properties.DisplayValueChecked")
        Me.chkRememberMe.Properties.DisplayValueGrayed = resources.GetString("chkRememberMe.Properties.DisplayValueGrayed")
        Me.chkRememberMe.Properties.DisplayValueUnchecked = resources.GetString("chkRememberMe.Properties.DisplayValueUnchecked")
        Me.chkRememberMe.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkRememberMe.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'FrmLogin
        '
        Me.AcceptButton = Me.BtnLogin
        resources.ApplyResources(Me, "$this")
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.Controls.Add(Me.chkRememberMe)
        Me.Controls.Add(Me.lnkForgotPass)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnLogin)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.TxtUsername)
        Me.Name = "FrmLogin"
        CType(Me.TxtUsername.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRememberMe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtUsername As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BtnLogin As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkRememberMe As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lnkForgotPass As DevExpress.XtraEditors.HyperlinkLabelControl
End Class
