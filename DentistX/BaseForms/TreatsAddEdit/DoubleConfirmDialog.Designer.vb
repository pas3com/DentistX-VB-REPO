<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DoubleConfirmDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DoubleConfirmDialog))
        Me.btnConfirm = New DevExpress.XtraEditors.SimpleButton()
        Me.lblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.chkConfirm = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.chkConfirm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnConfirm
        '
        resources.ApplyResources(Me.btnConfirm, "btnConfirm")
        Me.btnConfirm.Appearance.Font = CType(resources.GetObject("btnConfirm.Appearance.Font"), System.Drawing.Font)
        Me.btnConfirm.Appearance.Options.UseFont = True
        Me.btnConfirm.ImageOptions.ImageKey = resources.GetString("btnConfirm.ImageOptions.ImageKey")
        Me.btnConfirm.Name = "btnConfirm"
        '
        'lblMessage
        '
        resources.ApplyResources(Me.lblMessage, "lblMessage")
        Me.lblMessage.Appearance.Font = CType(resources.GetObject("lblMessage.Appearance.Font"), System.Drawing.Font)
        Me.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Appearance.Options.UseFont = True
        Me.lblMessage.Appearance.Options.UseForeColor = True
        Me.lblMessage.Appearance.Options.UseTextOptions = True
        Me.lblMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblMessage.Name = "lblMessage"
        '
        'chkConfirm
        '
        resources.ApplyResources(Me.chkConfirm, "chkConfirm")
        Me.chkConfirm.Name = "chkConfirm"
        Me.chkConfirm.Properties.Appearance.Font = CType(resources.GetObject("chkConfirm.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkConfirm.Properties.Appearance.Options.UseFont = True
        Me.chkConfirm.Properties.Caption = resources.GetString("chkConfirm.Properties.Caption")
        Me.chkConfirm.Properties.DisplayValueChecked = resources.GetString("chkConfirm.Properties.DisplayValueChecked")
        Me.chkConfirm.Properties.DisplayValueGrayed = resources.GetString("chkConfirm.Properties.DisplayValueGrayed")
        Me.chkConfirm.Properties.DisplayValueUnchecked = resources.GetString("chkConfirm.Properties.DisplayValueUnchecked")
        Me.chkConfirm.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkConfirm.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'DoubleConfirmDialog
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.chkConfirm)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.btnConfirm)
        Me.Name = "DoubleConfirmDialog"
        CType(Me.chkConfirm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnConfirm As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkConfirm As DevExpress.XtraEditors.CheckEdit
End Class
