<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HtmlCard
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.pnlCard = New System.Windows.Forms.Panel()
        Me.accentStrip = New System.Windows.Forms.Label()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.statusLbl = New DentistX.ApptCardStatusLabel()
        Me.pnlCard.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlCard
        '
        Me.pnlCard.BackColor = System.Drawing.SystemColors.Window
        Me.pnlCard.Controls.Add(Me.LabelControl1)
        Me.pnlCard.Controls.Add(Me.statusLbl)
        Me.pnlCard.Controls.Add(Me.accentStrip)
        Me.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCard.Location = New System.Drawing.Point(0, 0)
        Me.pnlCard.Name = "pnlCard"
        Me.pnlCard.Size = New System.Drawing.Size(200, 100)
        Me.pnlCard.TabIndex = 0
        '
        'accentStrip
        '
        Me.accentStrip.BackColor = System.Drawing.Color.SteelBlue
        Me.accentStrip.Dock = System.Windows.Forms.DockStyle.Left
        Me.accentStrip.Location = New System.Drawing.Point(0, 0)
        Me.accentStrip.Name = "accentStrip"
        Me.accentStrip.Size = New System.Drawing.Size(6, 100)
        Me.accentStrip.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.AllowHtmlString = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.LabelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelControl1.Location = New System.Drawing.Point(6, 0)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(4, 2, 4, 1)
        Me.LabelControl1.Size = New System.Drawing.Size(132, 100)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "LabelControl1"
        '
        'statusLbl
        '
        Me.statusLbl.BackColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.statusLbl.BorderColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.statusLbl.Dock = System.Windows.Forms.DockStyle.Right
        Me.statusLbl.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.statusLbl.ForeColor = System.Drawing.Color.White
        Me.statusLbl.Location = New System.Drawing.Point(138, 0)
        Me.statusLbl.MinimumSize = New System.Drawing.Size(1, 1)
        Me.statusLbl.Name = "statusLbl"
        Me.statusLbl.Size = New System.Drawing.Size(32, 100)
        Me.statusLbl.TabIndex = 1
        Me.statusLbl.TabStop = False
        Me.statusLbl.Text = "Status"
        Me.statusLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.statusLbl.TextDirection = DentistX.ApptCardStatusLabel.StatusTextDirection.BottomToTop
        '
        'HtmlCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlCard)
        Me.Name = "HtmlCard"
        Me.Size = New System.Drawing.Size(200, 100)
        Me.pnlCard.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlCard As System.Windows.Forms.Panel
    Friend WithEvents accentStrip As System.Windows.Forms.Label
    Friend WithEvents statusLbl As ApptCardStatusLabel
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
