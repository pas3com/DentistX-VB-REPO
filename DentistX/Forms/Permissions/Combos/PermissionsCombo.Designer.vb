<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PermissionsCombo
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
        Me.BtnAddPermissions = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.CboPermissions = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.CboPermissions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        'PanelControl1
        Me.PanelControl1.Controls.Add(Me.BtnAddPermissions)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelControl1.Location = New System.Drawing.Point(128, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(22, 22)
        Me.PanelControl1.TabIndex = 0

        'BtnAddPermissions
        Me.BtnAddPermissions.ImageOptions.Image = Global.DentistX.My.Resources.Resources.add_16x16
        Me.BtnAddPermissions.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtnAddPermissions.Location = New System.Drawing.Point(0, 0)
        Me.BtnAddPermissions.Name = "BtnAddPermissions"
        Me.BtnAddPermissions.Size = New System.Drawing.Size(20, 20)
        Me.BtnAddPermissions.TabIndex = 1

        'PanelControl2
        Me.PanelControl2.Controls.Add(Me.CboPermissions)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(128, 22)
        Me.PanelControl2.TabIndex = 1

        'CboPermissions
        Me.CboPermissions.Location = New System.Drawing.Point(0, 0)
        Me.CboPermissions.Name = "CboPermissions"
        Me.CboPermissions.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboPermissions.Properties.Appearance.Options.UseFont = True
        Me.CboPermissions.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CboPermissions.Size = New System.Drawing.Size(130, 22)
        Me.CboPermissions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CboPermissions.TabIndex = 0

        'PermissionsCombo
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "PermissionsCombo"
        Me.Size = New System.Drawing.Size(150, 22)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.CboPermissions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnAddPermissions As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CboPermissions As DevExpress.XtraEditors.ComboBoxEdit
End Class
