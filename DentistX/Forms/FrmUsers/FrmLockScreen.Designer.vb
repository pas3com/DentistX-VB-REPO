<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLockScreen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLockScreen))
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnUnlock = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtPassword = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblUserName = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.TxtPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'BtnUnlock
        '
        resources.ApplyResources(Me.BtnUnlock, "BtnUnlock")
        Me.BtnUnlock.Appearance.Font = CType(resources.GetObject("BtnUnlock.Appearance.Font"), System.Drawing.Font)
        Me.BtnUnlock.Appearance.Options.UseFont = True
        Me.BtnUnlock.ImageOptions.ImageKey = resources.GetString("BtnUnlock.ImageOptions.ImageKey")
        Me.BtnUnlock.Name = "BtnUnlock"
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
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'lblUserName
        '
        resources.ApplyResources(Me.lblUserName, "lblUserName")
        Me.lblUserName.Appearance.Font = CType(resources.GetObject("lblUserName.Appearance.Font"), System.Drawing.Font)
        Me.lblUserName.Appearance.Options.UseFont = True
        Me.lblUserName.Appearance.Options.UseTextOptions = True
        Me.lblUserName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblUserName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblUserName.Name = "lblUserName"
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl2.Name = "LabelControl2"
        '
        'FrmLockScreen
        '
        Me.AcceptButton = Me.BtnUnlock
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.lblUserName)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnUnlock)
        Me.Controls.Add(Me.TxtPassword)
        Me.Name = "FrmLockScreen"
        CType(Me.TxtPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnUnlock As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblUserName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
