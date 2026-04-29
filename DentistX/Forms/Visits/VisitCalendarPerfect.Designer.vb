<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class VisitCalendarPerfect
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
        Me.topPanel = New DevExpress.XtraEditors.SidePanel()
        Me.containerPanel = New DevExpress.XtraEditors.PanelControl()
        CType(Me.containerPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'topPanel
        '
        Me.topPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.topPanel.Location = New System.Drawing.Point(0, 0)
        Me.topPanel.Name = "topPanel"
        Me.topPanel.Size = New System.Drawing.Size(150, 23)
        Me.topPanel.TabIndex = 0
        Me.topPanel.Text = "SidePanel1"
        '
        'containerPanel
        '
        Me.containerPanel.AutoSize = True
        Me.containerPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.containerPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.containerPanel.Location = New System.Drawing.Point(0, 23)
        Me.containerPanel.Name = "containerPanel"
        Me.containerPanel.Size = New System.Drawing.Size(150, 127)
        Me.containerPanel.TabIndex = 0
        '
        'VisitCalendarPerfect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.containerPanel)
        Me.Controls.Add(Me.topPanel)
        Me.Name = "VisitCalendarPerfect"
        CType(Me.containerPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents topPanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents containerPanel As DevExpress.XtraEditors.PanelControl
End Class
