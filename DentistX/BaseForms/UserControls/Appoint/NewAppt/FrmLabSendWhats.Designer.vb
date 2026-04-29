<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmLabSendWhats
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtSendMessage = New DevExpress.XtraEditors.MemoEdit()
        Me.btnWhatsSend = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.lookUpSource = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.TxtSendMessage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpSource.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(27, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(62, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Choose Lab"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(27, 84)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(47, 15)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Message"
        '
        'TxtSendMessage
        '
        Me.TxtSendMessage.Location = New System.Drawing.Point(127, 40)
        Me.TxtSendMessage.Name = "TxtSendMessage"
        Me.TxtSendMessage.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.TxtSendMessage.Properties.Appearance.Options.UseFont = True
        Me.TxtSendMessage.Size = New System.Drawing.Size(275, 102)
        Me.TxtSendMessage.TabIndex = 3
        '
        'btnWhatsSend
        '
        Me.btnWhatsSend.ImageOptions.ImageKey = Nothing
        Me.btnWhatsSend.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnWhatsSend.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.whatsapp
        Me.btnWhatsSend.ImageOptions.SvgImageSize = New System.Drawing.Size(20, 20)
        Me.btnWhatsSend.Location = New System.Drawing.Point(163, 164)
        Me.btnWhatsSend.Name = "btnWhatsSend"
        Me.btnWhatsSend.Size = New System.Drawing.Size(58, 34)
        Me.btnWhatsSend.TabIndex = 15
        '
        'btnClose
        '
        Me.btnClose.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnClose.Appearance.Options.UseFont = True
        Me.btnClose.Location = New System.Drawing.Point(238, 164)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(58, 34)
        Me.btnClose.TabIndex = 16
        Me.btnClose.Text = "Close"
        '
        'lookUpSource
        '
        Me.lookUpSource.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lookUpSource.Location = New System.Drawing.Point(127, 8)
        Me.lookUpSource.Name = "lookUpSource"
        Me.lookUpSource.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lookUpSource.Properties.Appearance.Options.UseFont = True
        Me.lookUpSource.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpSource.Properties.NullText = ""
        Me.lookUpSource.Size = New System.Drawing.Size(275, 22)
        Me.lookUpSource.TabIndex = 1
        '
        'FrmLabSendWhats
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(459, 218)
        Me.Controls.Add(Me.lookUpSource)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnWhatsSend)
        Me.Controls.Add(Me.TxtSendMessage)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Name = "FrmLabSendWhats"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Lab WhatsApp Message Send"
        CType(Me.TxtSendMessage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpSource.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtSendMessage As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnWhatsSend As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lookUpSource As DevExpress.XtraEditors.LookUpEdit
End Class
