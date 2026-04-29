<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WhatsAppSender
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WhatsAppSender))
        Me.btnSendNormal = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAppts = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSendAcct = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSecretaries = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPayments = New DevExpress.XtraEditors.SimpleButton()
        Me.SuspendLayout()
        '
        'btnSendNormal
        '
        resources.ApplyResources(Me.btnSendNormal, "btnSendNormal")
        Me.btnSendNormal.Appearance.Font = CType(resources.GetObject("btnSendNormal.Appearance.Font"), System.Drawing.Font)
        Me.btnSendNormal.Appearance.Options.UseFont = True
        Me.btnSendNormal.ImageOptions.ImageKey = resources.GetString("btnSendNormal.ImageOptions.ImageKey")
        Me.btnSendNormal.Name = "btnSendNormal"
        '
        'btnAppts
        '
        resources.ApplyResources(Me.btnAppts, "btnAppts")
        Me.btnAppts.Appearance.Font = CType(resources.GetObject("btnAppts.Appearance.Font"), System.Drawing.Font)
        Me.btnAppts.Appearance.Options.UseFont = True
        Me.btnAppts.ImageOptions.ImageKey = resources.GetString("btnAppts.ImageOptions.ImageKey")
        Me.btnAppts.Name = "btnAppts"
        '
        'btnSendAcct
        '
        resources.ApplyResources(Me.btnSendAcct, "btnSendAcct")
        Me.btnSendAcct.Appearance.Font = CType(resources.GetObject("btnSendAcct.Appearance.Font"), System.Drawing.Font)
        Me.btnSendAcct.Appearance.Options.UseFont = True
        Me.btnSendAcct.ImageOptions.ImageKey = resources.GetString("btnSendAcct.ImageOptions.ImageKey")
        Me.btnSendAcct.Name = "btnSendAcct"
        '
        'btnSecretaries
        '
        resources.ApplyResources(Me.btnSecretaries, "btnSecretaries")
        Me.btnSecretaries.Appearance.Font = CType(resources.GetObject("btnSecretaries.Appearance.Font"), System.Drawing.Font)
        Me.btnSecretaries.Appearance.Options.UseFont = True
        Me.btnSecretaries.ImageOptions.ImageKey = resources.GetString("btnSecretaries.ImageOptions.ImageKey")
        Me.btnSecretaries.Name = "btnSecretaries"
        '
        'btnPayments
        '
        resources.ApplyResources(Me.btnPayments, "btnPayments")
        Me.btnPayments.Appearance.Font = CType(resources.GetObject("btnPayments.Appearance.Font"), System.Drawing.Font)
        Me.btnPayments.Appearance.Options.UseFont = True
        Me.btnPayments.ImageOptions.ImageKey = resources.GetString("btnPayments.ImageOptions.ImageKey")
        Me.btnPayments.Name = "btnPayments"
        '
        'WhatsAppSender
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnSendNormal)
        Me.Controls.Add(Me.btnAppts)
        Me.Controls.Add(Me.btnSendAcct)
        Me.Controls.Add(Me.btnSecretaries)
        Me.Controls.Add(Me.btnPayments)
        Me.Name = "WhatsAppSender"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnSendNormal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAppts As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSendAcct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSecretaries As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPayments As DevExpress.XtraEditors.SimpleButton
End Class
