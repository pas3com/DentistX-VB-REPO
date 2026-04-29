<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportViewer
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
        Me.DocumentViewer1 = New DevExpress.XtraPrinting.Preview.DocumentViewer()
        Me.SuspendLayout()
        '
        'DocumentViewer1
        '
        Me.DocumentViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DocumentViewer1.IsMetric = False
        Me.DocumentViewer1.Location = New System.Drawing.Point(0, 0)
        Me.DocumentViewer1.Name = "DocumentViewer1"
        Me.DocumentViewer1.Size = New System.Drawing.Size(896, 537)
        Me.DocumentViewer1.TabIndex = 0
        '
        'frmReportViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(896, 537)
        Me.Controls.Add(Me.DocumentViewer1)
        Me.Name = "frmReportViewer"
        Me.Text = "Report Viewer"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DocumentViewer1 As DevExpress.XtraPrinting.Preview.DocumentViewer
End Class
