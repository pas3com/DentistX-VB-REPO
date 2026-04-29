<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class FrmChngFinancePass
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim uiFont As New System.Drawing.Font("Calibri", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblIntro = New DevExpress.XtraEditors.LabelControl()
        Me.lblCurrent = New DevExpress.XtraEditors.LabelControl()
        Me.txtCurrentPass = New DevExpress.XtraEditors.TextEdit()
        Me.lblNew = New DevExpress.XtraEditors.LabelControl()
        Me.txtNewPass = New DevExpress.XtraEditors.TextEdit()
        Me.lblConfirm = New DevExpress.XtraEditors.LabelControl()
        Me.txtConfirmPass = New DevExpress.XtraEditors.TextEdit()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRemoveProtection = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.txtCurrentPass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewPass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtConfirmPass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblIntro
        '
        Me.lblIntro.Appearance.Font = uiFont
        Me.lblIntro.Appearance.Options.UseFont = True
        Me.lblIntro.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblIntro.Location = New System.Drawing.Point(14, 14)
        Me.lblIntro.Name = "lblIntro"
        Me.lblIntro.Size = New System.Drawing.Size(412, 44)
        Me.lblIntro.TabIndex = 0
        Me.lblIntro.Text = "Intro"
        '
        'lblCurrent
        '
        Me.lblCurrent.Appearance.Font = uiFont
        Me.lblCurrent.Appearance.Options.UseFont = True
        Me.lblCurrent.Location = New System.Drawing.Point(14, 66)
        Me.lblCurrent.Name = "lblCurrent"
        Me.lblCurrent.Size = New System.Drawing.Size(120, 17)
        Me.lblCurrent.TabIndex = 1
        Me.lblCurrent.Text = "Current password"
        '
        'txtCurrentPass
        '
        Me.txtCurrentPass.Location = New System.Drawing.Point(14, 87)
        Me.txtCurrentPass.Name = "txtCurrentPass"
        Me.txtCurrentPass.Properties.Appearance.Font = uiFont
        Me.txtCurrentPass.Properties.Appearance.Options.UseFont = True
        Me.txtCurrentPass.Properties.UseSystemPasswordChar = True
        Me.txtCurrentPass.Size = New System.Drawing.Size(412, 24)
        Me.txtCurrentPass.TabIndex = 2
        '
        'lblNew
        '
        Me.lblNew.Appearance.Font = uiFont
        Me.lblNew.Appearance.Options.UseFont = True
        Me.lblNew.Location = New System.Drawing.Point(14, 121)
        Me.lblNew.Name = "lblNew"
        Me.lblNew.Size = New System.Drawing.Size(120, 17)
        Me.lblNew.TabIndex = 3
        Me.lblNew.Text = "New password"
        '
        'txtNewPass
        '
        Me.txtNewPass.Location = New System.Drawing.Point(14, 142)
        Me.txtNewPass.Name = "txtNewPass"
        Me.txtNewPass.Properties.Appearance.Font = uiFont
        Me.txtNewPass.Properties.Appearance.Options.UseFont = True
        Me.txtNewPass.Properties.UseSystemPasswordChar = True
        Me.txtNewPass.Size = New System.Drawing.Size(412, 24)
        Me.txtNewPass.TabIndex = 4
        '
        'lblConfirm
        '
        Me.lblConfirm.Appearance.Font = uiFont
        Me.lblConfirm.Appearance.Options.UseFont = True
        Me.lblConfirm.Location = New System.Drawing.Point(14, 176)
        Me.lblConfirm.Name = "lblConfirm"
        Me.lblConfirm.Size = New System.Drawing.Size(120, 17)
        Me.lblConfirm.TabIndex = 5
        Me.lblConfirm.Text = "Confirm new"
        '
        'txtConfirmPass
        '
        Me.txtConfirmPass.Location = New System.Drawing.Point(14, 197)
        Me.txtConfirmPass.Name = "txtConfirmPass"
        Me.txtConfirmPass.Properties.Appearance.Font = uiFont
        Me.txtConfirmPass.Properties.Appearance.Options.UseFont = True
        Me.txtConfirmPass.Properties.UseSystemPasswordChar = True
        Me.txtConfirmPass.Size = New System.Drawing.Size(412, 24)
        Me.txtConfirmPass.TabIndex = 6
        '
        'btnSave
        '
        Me.btnSave.Appearance.Font = uiFont
        Me.btnSave.Appearance.Options.UseFont = True
        Me.btnSave.Location = New System.Drawing.Point(14, 242)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(120, 30)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = uiFont
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(148, 242)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 30)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        '
        'btnRemoveProtection
        '
        Me.btnRemoveProtection.Appearance.Font = uiFont
        Me.btnRemoveProtection.Appearance.Options.UseFont = True
        Me.btnRemoveProtection.Location = New System.Drawing.Point(282, 242)
        Me.btnRemoveProtection.Name = "btnRemoveProtection"
        Me.btnRemoveProtection.Size = New System.Drawing.Size(144, 30)
        Me.btnRemoveProtection.TabIndex = 9
        Me.btnRemoveProtection.Text = "Remove protection"
        '
        'FrmChngFinancePass
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(444, 292)
        Me.Controls.Add(Me.btnRemoveProtection)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtConfirmPass)
        Me.Controls.Add(Me.lblConfirm)
        Me.Controls.Add(Me.txtNewPass)
        Me.Controls.Add(Me.lblNew)
        Me.Controls.Add(Me.txtCurrentPass)
        Me.Controls.Add(Me.lblCurrent)
        Me.Controls.Add(Me.lblIntro)
        Me.Font = uiFont
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmChngFinancePass"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Finance dashboard password"
        CType(Me.txtCurrentPass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewPass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtConfirmPass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblIntro As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCurrent As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCurrentPass As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblNew As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNewPass As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblConfirm As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtConfirmPass As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRemoveProtection As DevExpress.XtraEditors.SimpleButton
End Class
