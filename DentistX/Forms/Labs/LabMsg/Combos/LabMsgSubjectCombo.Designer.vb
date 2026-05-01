<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LabMsgSubjectCombo
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
        Me.BtnAddLabMsgSubject = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.CboLabMsgSubject = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.CboLabMsgSubject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtnAddLabMsgSubject)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelControl1.Location = New System.Drawing.Point(128, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(22, 25)
        Me.PanelControl1.TabIndex = 0
        '
        'BtnAddLabMsgSubject
        '
        Me.BtnAddLabMsgSubject.ImageOptions.Image = Global.DentistX.My.Resources.Resources.add_16x16
        Me.BtnAddLabMsgSubject.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtnAddLabMsgSubject.Location = New System.Drawing.Point(0, 0)
        Me.BtnAddLabMsgSubject.Name = "BtnAddLabMsgSubject"
        Me.BtnAddLabMsgSubject.Size = New System.Drawing.Size(20, 20)
        Me.BtnAddLabMsgSubject.TabIndex = 1
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.CboLabMsgSubject)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(128, 25)
        Me.PanelControl2.TabIndex = 1
        '
        'CboLabMsgSubject
        '
        Me.CboLabMsgSubject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CboLabMsgSubject.Location = New System.Drawing.Point(2, 2)
        Me.CboLabMsgSubject.Name = "CboLabMsgSubject"
        Me.CboLabMsgSubject.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboLabMsgSubject.Properties.Appearance.Options.UseFont = True
        Me.CboLabMsgSubject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CboLabMsgSubject.Size = New System.Drawing.Size(124, 22)
        Me.CboLabMsgSubject.TabIndex = 0
        '
        'LabMsgSubjectCombo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "LabMsgSubjectCombo"
        Me.Size = New System.Drawing.Size(150, 25)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.CboLabMsgSubject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnAddLabMsgSubject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CboLabMsgSubject As DevExpress.XtraEditors.ComboBoxEdit
End Class
