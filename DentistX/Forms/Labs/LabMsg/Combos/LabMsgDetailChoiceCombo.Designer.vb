<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LabMsgDetailChoiceCombo
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnAddLabMsgDetailChoice = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.CboLabMsgDetailChoice = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.CboLabMsgDetailChoice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtnAddLabMsgDetailChoice)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelControl1.Location = New System.Drawing.Point(128, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(22, 25)
        Me.PanelControl1.TabIndex = 0
        '
        'BtnAddLabMsgDetailChoice
        '
        Me.BtnAddLabMsgDetailChoice.ImageOptions.Image = Global.DentistX.My.Resources.Resources.add_16x16
        Me.BtnAddLabMsgDetailChoice.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtnAddLabMsgDetailChoice.Location = New System.Drawing.Point(0, 0)
        Me.BtnAddLabMsgDetailChoice.Name = "BtnAddLabMsgDetailChoice"
        Me.BtnAddLabMsgDetailChoice.Size = New System.Drawing.Size(20, 20)
        Me.BtnAddLabMsgDetailChoice.TabIndex = 1
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.CboLabMsgDetailChoice)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(128, 25)
        Me.PanelControl2.TabIndex = 1
        '
        'CboLabMsgDetailChoice
        '
        Me.CboLabMsgDetailChoice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CboLabMsgDetailChoice.Location = New System.Drawing.Point(2, 2)
        Me.CboLabMsgDetailChoice.Name = "CboLabMsgDetailChoice"
        Me.CboLabMsgDetailChoice.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboLabMsgDetailChoice.Properties.Appearance.Options.UseFont = True
        Me.CboLabMsgDetailChoice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CboLabMsgDetailChoice.Size = New System.Drawing.Size(124, 22)
        Me.CboLabMsgDetailChoice.TabIndex = 0
        '
        'LabMsgDetailChoiceCombo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "LabMsgDetailChoiceCombo"
        Me.Size = New System.Drawing.Size(150, 25)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.CboLabMsgDetailChoice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnAddLabMsgDetailChoice As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CboLabMsgDetailChoice As DevExpress.XtraEditors.ComboBoxEdit
End Class
