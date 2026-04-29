<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmVisits
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
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
        Me.VisitCalendarDayControl1 = New DentistX.VisitCalendarDayControl()
        Me.SuspendLayout()
        '
        'VisitCalendarDayControl1
        '
        Me.VisitCalendarDayControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VisitCalendarDayControl1.FilterPatient = ""
        Me.VisitCalendarDayControl1.FilterVisitType = ""
        Me.VisitCalendarDayControl1.InMonth = 5
        Me.VisitCalendarDayControl1.InYear = 2025
        Me.VisitCalendarDayControl1.Location = New System.Drawing.Point(0, 0)
        Me.VisitCalendarDayControl1.Name = "VisitCalendarDayControl1"
        Me.VisitCalendarDayControl1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.VisitCalendarDayControl1.Size = New System.Drawing.Size(1208, 622)
        Me.VisitCalendarDayControl1.TabIndex = 0
        '
        'FrmVisits
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1208, 622)
        Me.Controls.Add(Me.VisitCalendarDayControl1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmVisits"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Visits"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents VisitCalendarDayControl1 As VisitCalendarDayControl
End Class
