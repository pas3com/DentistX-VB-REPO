<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ApptCard
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlRoot = New System.Windows.Forms.Panel()
        Me.notesLbl = New System.Windows.Forms.Label()
        Me.reasonLbl = New System.Windows.Forms.Label()
        Me.doctorLbl = New System.Windows.Forms.Label()
        Me.patientnameLbl = New System.Windows.Forms.Label()
        Me.endLbl = New System.Windows.Forms.Label()
        Me.startLbl = New System.Windows.Forms.Label()
        Me.accentLBl = New System.Windows.Forms.Label()
        Me.statusLbl = New DentistX.ApptCardStatusLabel()
        Me.pnlRoot.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlRoot
        '
        Me.pnlRoot.Controls.Add(Me.notesLbl)
        Me.pnlRoot.Controls.Add(Me.reasonLbl)
        Me.pnlRoot.Controls.Add(Me.doctorLbl)
        Me.pnlRoot.Controls.Add(Me.patientnameLbl)
        Me.pnlRoot.Controls.Add(Me.endLbl)
        Me.pnlRoot.Controls.Add(Me.startLbl)
        Me.pnlRoot.Controls.Add(Me.statusLbl)
        Me.pnlRoot.Controls.Add(Me.accentLBl)
        Me.pnlRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRoot.Location = New System.Drawing.Point(0, 0)
        Me.pnlRoot.Name = "pnlRoot"
        Me.pnlRoot.Size = New System.Drawing.Size(192, 88)
        Me.pnlRoot.TabIndex = 0
        '
        'notesLbl
        '
        Me.notesLbl.Location = New System.Drawing.Point(24, 70)
        Me.notesLbl.Name = "notesLbl"
        Me.notesLbl.Size = New System.Drawing.Size(124, 16)
        Me.notesLbl.TabIndex = 2
        Me.notesLbl.Text = "Notes"
        '
        'reasonLbl
        '
        Me.reasonLbl.Location = New System.Drawing.Point(24, 54)
        Me.reasonLbl.Name = "reasonLbl"
        Me.reasonLbl.Size = New System.Drawing.Size(124, 16)
        Me.reasonLbl.TabIndex = 2
        Me.reasonLbl.Text = "Reason"
        '
        'doctorLbl
        '
        Me.doctorLbl.Location = New System.Drawing.Point(24, 38)
        Me.doctorLbl.Name = "doctorLbl"
        Me.doctorLbl.Size = New System.Drawing.Size(124, 16)
        Me.doctorLbl.TabIndex = 2
        Me.doctorLbl.Text = "Dr."
        '
        'patientnameLbl
        '
        Me.patientnameLbl.Location = New System.Drawing.Point(24, 20)
        Me.patientnameLbl.Name = "patientnameLbl"
        Me.patientnameLbl.Size = New System.Drawing.Size(124, 18)
        Me.patientnameLbl.TabIndex = 2
        Me.patientnameLbl.Text = "Patient"
        '
        'endLbl
        '
        Me.endLbl.Location = New System.Drawing.Point(78, 2)
        Me.endLbl.Name = "endLbl"
        Me.endLbl.Size = New System.Drawing.Size(50, 16)
        Me.endLbl.TabIndex = 2
        Me.endLbl.Text = "09:00"
        Me.endLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'startLbl
        '
        Me.startLbl.Location = New System.Drawing.Point(24, 2)
        Me.startLbl.Name = "startLbl"
        Me.startLbl.Size = New System.Drawing.Size(50, 16)
        Me.startLbl.TabIndex = 2
        Me.startLbl.Text = "08:00"
        Me.startLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'accentLBl
        '
        Me.accentLBl.BackColor = System.Drawing.Color.Lime
        Me.accentLBl.Dock = System.Windows.Forms.DockStyle.Left
        Me.accentLBl.Location = New System.Drawing.Point(0, 0)
        Me.accentLBl.Name = "accentLBl"
        Me.accentLBl.Size = New System.Drawing.Size(20, 88)
        Me.accentLBl.TabIndex = 0
        '
        'statusLbl
        '
        Me.statusLbl.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.statusLbl.BorderColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.statusLbl.Dock = System.Windows.Forms.DockStyle.Right
        Me.statusLbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.statusLbl.ForeColor = System.Drawing.Color.White
        Me.statusLbl.Location = New System.Drawing.Point(152, 0)
        Me.statusLbl.MinimumSize = New System.Drawing.Size(1, 1)
        Me.statusLbl.Name = "statusLbl"
        Me.statusLbl.Size = New System.Drawing.Size(40, 88)
        Me.statusLbl.TabIndex = 1
        Me.statusLbl.TabStop = False
        Me.statusLbl.Text = "TopToBottom"
        Me.statusLbl.TextDirection = DentistX.ApptCardStatusLabel.StatusTextDirection.TopToBottom
        '
        'ApptCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlRoot)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ApptCard"
        Me.Size = New System.Drawing.Size(192, 88)
        Me.pnlRoot.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlRoot As Panel
    Friend WithEvents accentLBl As Label
    Friend WithEvents statusLbl As ApptCardStatusLabel
    Friend WithEvents startLbl As Label
    Friend WithEvents endLbl As Label
    Friend WithEvents patientnameLbl As Label
    Friend WithEvents doctorLbl As Label
    Friend WithEvents reasonLbl As Label
    Friend WithEvents notesLbl As Label
End Class
