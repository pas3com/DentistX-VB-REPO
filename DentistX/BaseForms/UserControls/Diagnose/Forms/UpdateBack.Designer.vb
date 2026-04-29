<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateBack
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
        Me.firstColor = New DevExpress.XtraEditors.ColorPickEdit()
        Me.LabelControl22 = New DevExpress.XtraEditors.LabelControl()
        Me.secondColor = New DevExpress.XtraEditors.ColorPickEdit()
        Me.LabelControl21 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cboGradient = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnApply = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cboGlassStyle = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.firstColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.secondColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboGradient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboGlassStyle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'firstColor
        '
        Me.firstColor.EditValue = System.Drawing.Color.Empty
        Me.firstColor.EnterMoveNextControl = True
        Me.firstColor.Location = New System.Drawing.Point(140, 32)
        Me.firstColor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.firstColor.Name = "firstColor"
        Me.firstColor.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold)
        Me.firstColor.Properties.Appearance.Options.UseFont = True
        Me.firstColor.Properties.AutomaticColor = System.Drawing.Color.Black
        Me.firstColor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.firstColor.Properties.ColorDialogOptions.ShowMakeWebSafeButton = True
        Me.firstColor.Properties.ColorDialogType = DevExpress.XtraEditors.Popup.ColorDialogType.Advanced
        Me.firstColor.Size = New System.Drawing.Size(148, 20)
        Me.firstColor.TabIndex = 24
        '
        'LabelControl22
        '
        Me.LabelControl22.Appearance.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl22.Appearance.Options.UseFont = True
        Me.LabelControl22.Location = New System.Drawing.Point(35, 37)
        Me.LabelControl22.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl22.Name = "LabelControl22"
        Me.LabelControl22.Size = New System.Drawing.Size(50, 14)
        Me.LabelControl22.TabIndex = 23
        Me.LabelControl22.Text = "First Color"
        '
        'secondColor
        '
        Me.secondColor.EditValue = System.Drawing.Color.Empty
        Me.secondColor.EnterMoveNextControl = True
        Me.secondColor.Location = New System.Drawing.Point(140, 60)
        Me.secondColor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.secondColor.Name = "secondColor"
        Me.secondColor.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold)
        Me.secondColor.Properties.Appearance.Options.UseFont = True
        Me.secondColor.Properties.AutomaticColor = System.Drawing.Color.Black
        Me.secondColor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.secondColor.Properties.ColorDialogType = DevExpress.XtraEditors.Popup.ColorDialogType.Advanced
        Me.secondColor.Size = New System.Drawing.Size(148, 20)
        Me.secondColor.TabIndex = 26
        '
        'LabelControl21
        '
        Me.LabelControl21.Appearance.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl21.Appearance.Options.UseFont = True
        Me.LabelControl21.Location = New System.Drawing.Point(35, 65)
        Me.LabelControl21.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl21.Name = "LabelControl21"
        Me.LabelControl21.Size = New System.Drawing.Size(63, 14)
        Me.LabelControl21.TabIndex = 25
        Me.LabelControl21.Text = "Second Color"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(35, 95)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(43, 14)
        Me.LabelControl1.TabIndex = 25
        Me.LabelControl1.Text = "Gradient"
        '
        'cboGradient
        '
        Me.cboGradient.Location = New System.Drawing.Point(140, 87)
        Me.cboGradient.Name = "cboGradient"
        Me.cboGradient.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboGradient.Size = New System.Drawing.Size(148, 22)
        Me.cboGradient.TabIndex = 27
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Appearance.ForeColor = System.Drawing.Color.Red
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.Appearance.Options.UseForeColor = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(203, 154)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 34
        Me.btnCancel.Text = "Cancel"
        '
        'btnApply
        '
        Me.btnApply.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnApply.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.btnApply.Appearance.Options.UseFont = True
        Me.btnApply.Appearance.Options.UseForeColor = True
        Me.btnApply.Location = New System.Drawing.Point(101, 154)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 33
        Me.btnApply.Text = "Apply"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(35, 123)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(55, 14)
        Me.LabelControl2.TabIndex = 25
        Me.LabelControl2.Text = "Glass Style"
        '
        'cboGlassStyle
        '
        Me.cboGlassStyle.Location = New System.Drawing.Point(140, 115)
        Me.cboGlassStyle.Name = "cboGlassStyle"
        Me.cboGlassStyle.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboGlassStyle.Size = New System.Drawing.Size(148, 22)
        Me.cboGlassStyle.TabIndex = 27
        '
        'UpdateBack
        '
        Me.AcceptButton = Me.btnApply
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(379, 200)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.cboGlassStyle)
        Me.Controls.Add(Me.cboGradient)
        Me.Controls.Add(Me.firstColor)
        Me.Controls.Add(Me.LabelControl22)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.secondColor)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.LabelControl21)
        Me.Name = "UpdateBack"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Back Color"
        CType(Me.firstColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.secondColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboGradient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboGlassStyle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents firstColor As DevExpress.XtraEditors.ColorPickEdit
    Friend WithEvents LabelControl22 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents secondColor As DevExpress.XtraEditors.ColorPickEdit
    Friend WithEvents LabelControl21 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboGradient As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnApply As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboGlassStyle As DevExpress.XtraEditors.ComboBoxEdit
End Class
