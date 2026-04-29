<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ArrowFrm
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
        Me.tbHeadLength = New DevExpress.XtraEditors.TrackBarControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tbheadWidth = New DevExpress.XtraEditors.TrackBarControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbshaftWidthBase = New DevExpress.XtraEditors.TrackBarControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tbshaftWidthTip = New DevExpress.XtraEditors.TrackBarControl()
        Me.btnApply = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnReset = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tbHeadLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbHeadLength.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbheadWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbheadWidth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbshaftWidthBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbshaftWidthBase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbshaftWidthTip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbshaftWidthTip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(12, 40)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(69, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Head Length"
        '
        'tbHeadLength
        '
        Me.tbHeadLength.EditValue = 100
        Me.tbHeadLength.Location = New System.Drawing.Point(116, 36)
        Me.tbHeadLength.Name = "tbHeadLength"
        Me.tbHeadLength.Properties.AutoSizeMode = DevExpress.XtraEditors.Repository.TrackBarAutoSizeMode.Content
        Me.tbHeadLength.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbHeadLength.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbHeadLength.Properties.Maximum = 200
        Me.tbHeadLength.Properties.Minimum = 10
        Me.tbHeadLength.Properties.TickFrequency = 5
        Me.tbHeadLength.Properties.TickStyle = System.Windows.Forms.TickStyle.None
        Me.tbHeadLength.Size = New System.Drawing.Size(156, 22)
        Me.tbHeadLength.TabIndex = 1
        Me.tbHeadLength.Value = 100
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(12, 77)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(65, 15)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Head Width"
        '
        'tbheadWidth
        '
        Me.tbheadWidth.EditValue = 50
        Me.tbheadWidth.Location = New System.Drawing.Point(116, 73)
        Me.tbheadWidth.Name = "tbheadWidth"
        Me.tbheadWidth.Properties.AutoSizeMode = DevExpress.XtraEditors.Repository.TrackBarAutoSizeMode.Content
        Me.tbheadWidth.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbheadWidth.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbheadWidth.Properties.Maximum = 200
        Me.tbheadWidth.Properties.Minimum = 10
        Me.tbheadWidth.Properties.TickFrequency = 5
        Me.tbheadWidth.Properties.TickStyle = System.Windows.Forms.TickStyle.None
        Me.tbheadWidth.Size = New System.Drawing.Size(156, 22)
        Me.tbheadWidth.TabIndex = 3
        Me.tbheadWidth.Value = 50
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(12, 126)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(93, 15)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Shaft Width Base"
        '
        'tbshaftWidthBase
        '
        Me.tbshaftWidthBase.EditValue = 50
        Me.tbshaftWidthBase.Location = New System.Drawing.Point(116, 122)
        Me.tbshaftWidthBase.Name = "tbshaftWidthBase"
        Me.tbshaftWidthBase.Properties.AutoSizeMode = DevExpress.XtraEditors.Repository.TrackBarAutoSizeMode.Content
        Me.tbshaftWidthBase.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbshaftWidthBase.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbshaftWidthBase.Properties.Maximum = 200
        Me.tbshaftWidthBase.Properties.Minimum = 10
        Me.tbshaftWidthBase.Properties.TickFrequency = 5
        Me.tbshaftWidthBase.Properties.TickStyle = System.Windows.Forms.TickStyle.None
        Me.tbshaftWidthBase.Size = New System.Drawing.Size(156, 22)
        Me.tbshaftWidthBase.TabIndex = 5
        Me.tbshaftWidthBase.Value = 50
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(12, 171)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(84, 15)
        Me.LabelControl4.TabIndex = 6
        Me.LabelControl4.Text = "Shaft Width Tip"
        '
        'tbshaftWidthTip
        '
        Me.tbshaftWidthTip.EditValue = 20
        Me.tbshaftWidthTip.Location = New System.Drawing.Point(116, 167)
        Me.tbshaftWidthTip.Name = "tbshaftWidthTip"
        Me.tbshaftWidthTip.Properties.AutoSizeMode = DevExpress.XtraEditors.Repository.TrackBarAutoSizeMode.Content
        Me.tbshaftWidthTip.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.tbshaftWidthTip.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbshaftWidthTip.Properties.Maximum = 200
        Me.tbshaftWidthTip.Properties.Minimum = 10
        Me.tbshaftWidthTip.Properties.TickFrequency = 5
        Me.tbshaftWidthTip.Properties.TickStyle = System.Windows.Forms.TickStyle.None
        Me.tbshaftWidthTip.Size = New System.Drawing.Size(156, 22)
        Me.tbshaftWidthTip.TabIndex = 7
        Me.tbshaftWidthTip.Value = 20
        '
        'btnApply
        '
        Me.btnApply.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnApply.Appearance.Options.UseFont = True
        Me.btnApply.Location = New System.Drawing.Point(22, 213)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 8
        Me.btnApply.Text = "Apply"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(116, 213)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "Cancel"
        '
        'btnReset
        '
        Me.btnReset.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnReset.Appearance.Options.UseFont = True
        Me.btnReset.Location = New System.Drawing.Point(197, 213)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(75, 23)
        Me.btnReset.TabIndex = 10
        Me.btnReset.Text = "Reset"
        '
        'ArrowFrm
        '
        Me.AcceptButton = Me.btnApply
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(294, 268)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.tbshaftWidthTip)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.tbshaftWidthBase)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tbheadWidth)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tbHeadLength)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "ArrowFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Arrow Properties"
        CType(Me.tbHeadLength.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbHeadLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbheadWidth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbheadWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbshaftWidthBase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbshaftWidthBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbshaftWidthTip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbshaftWidthTip, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbHeadLength As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbheadWidth As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbshaftWidthBase As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbshaftWidthTip As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents btnApply As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnReset As DevExpress.XtraEditors.SimpleButton
End Class
