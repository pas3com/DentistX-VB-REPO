<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WeekSnapshotPreviewForm
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlScroll = New System.Windows.Forms.Panel()
        Me.picPreview = New System.Windows.Forms.PictureBox()
        Me.tlpFooter = New System.Windows.Forms.TableLayoutPanel()
        Me.flpWhatsRow = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblWhatsCaption = New System.Windows.Forms.Label()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.flpButtonRow = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnWhats = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.pnlScroll.SuspendLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpFooter.SuspendLayout()
        Me.flpWhatsRow.SuspendLayout()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.flpButtonRow.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlScroll
        '
        Me.pnlScroll.AutoScroll = True
        Me.pnlScroll.BackColor = System.Drawing.SystemColors.ControlDark
        Me.pnlScroll.Controls.Add(Me.picPreview)
        Me.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlScroll.Location = New System.Drawing.Point(0, 0)
        Me.pnlScroll.Name = "pnlScroll"
        Me.pnlScroll.Size = New System.Drawing.Size(920, 641)
        Me.pnlScroll.TabIndex = 0
        '
        'picPreview
        '
        Me.picPreview.BackColor = System.Drawing.Color.White
        Me.picPreview.Location = New System.Drawing.Point(0, 0)
        Me.picPreview.Name = "picPreview"
        Me.picPreview.Size = New System.Drawing.Size(100, 50)
        Me.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picPreview.TabIndex = 0
        Me.picPreview.TabStop = False
        '
        'tlpFooter
        '
        Me.tlpFooter.AutoSize = True
        Me.tlpFooter.ColumnCount = 1
        Me.tlpFooter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpFooter.Controls.Add(Me.flpWhatsRow, 0, 0)
        Me.tlpFooter.Controls.Add(Me.flpButtonRow, 0, 1)
        Me.tlpFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tlpFooter.Location = New System.Drawing.Point(0, 641)
        Me.tlpFooter.Name = "tlpFooter"
        Me.tlpFooter.RowCount = 2
        Me.tlpFooter.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpFooter.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpFooter.Size = New System.Drawing.Size(920, 79)
        Me.tlpFooter.TabIndex = 1
        '
        'flpWhatsRow
        '
        Me.flpWhatsRow.AutoSize = True
        Me.flpWhatsRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpWhatsRow.Controls.Add(Me.lblWhatsCaption)
        Me.flpWhatsRow.Controls.Add(Me.cboPrefix)
        Me.flpWhatsRow.Controls.Add(Me.txtWhats)
        Me.flpWhatsRow.Controls.Add(Me.lblWhats)
        Me.flpWhatsRow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpWhatsRow.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.flpWhatsRow.Location = New System.Drawing.Point(0, 0)
        Me.flpWhatsRow.Margin = New System.Windows.Forms.Padding(0)
        Me.flpWhatsRow.Name = "flpWhatsRow"
        Me.flpWhatsRow.Padding = New System.Windows.Forms.Padding(8, 6, 8, 4)
        Me.flpWhatsRow.Size = New System.Drawing.Size(920, 36)
        Me.flpWhatsRow.TabIndex = 0
        Me.flpWhatsRow.WrapContents = False
        '
        'lblWhatsCaption
        '
        Me.lblWhatsCaption.AutoSize = True
        Me.lblWhatsCaption.Location = New System.Drawing.Point(8, 14)
        Me.lblWhatsCaption.Margin = New System.Windows.Forms.Padding(0, 8, 6, 0)
        Me.lblWhatsCaption.Name = "lblWhatsCaption"
        Me.lblWhatsCaption.Size = New System.Drawing.Size(68, 15)
        Me.lblWhatsCaption.TabIndex = 0
        Me.lblWhatsCaption.Text = "WhatsApp:"
        '
        'cboPrefix
        '
        Me.cboPrefix.Location = New System.Drawing.Point(82, 10)
        Me.cboPrefix.Margin = New System.Windows.Forms.Padding(0, 4, 8, 0)
        Me.cboPrefix.Name = "cboPrefix"
        Me.cboPrefix.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.cboPrefix.Properties.Appearance.Options.UseFont = True
        Me.cboPrefix.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboPrefix.Size = New System.Drawing.Size(133, 22)
        Me.cboPrefix.TabIndex = 1
        '
        'txtWhats
        '
        Me.txtWhats.Location = New System.Drawing.Point(223, 10)
        Me.txtWhats.Margin = New System.Windows.Forms.Padding(0, 4, 8, 0)
        Me.txtWhats.Name = "txtWhats"
        Me.txtWhats.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtWhats.Properties.Appearance.Options.UseFont = True
        Me.txtWhats.Properties.MaxLength = 10
        Me.txtWhats.Size = New System.Drawing.Size(130, 22)
        Me.txtWhats.TabIndex = 2
        '
        'lblWhats
        '
        Me.lblWhats.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblWhats.Appearance.Options.UseFont = True
        Me.lblWhats.Location = New System.Drawing.Point(361, 14)
        Me.lblWhats.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.lblWhats.Name = "lblWhats"
        Me.lblWhats.Size = New System.Drawing.Size(0, 15)
        Me.lblWhats.TabIndex = 3
        '
        'flpButtonRow
        '
        Me.flpButtonRow.AutoSize = True
        Me.flpButtonRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpButtonRow.Controls.Add(Me.btnSave)
        Me.flpButtonRow.Controls.Add(Me.btnWhats)
        Me.flpButtonRow.Controls.Add(Me.btnCancel)
        Me.flpButtonRow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpButtonRow.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.flpButtonRow.Location = New System.Drawing.Point(0, 36)
        Me.flpButtonRow.Margin = New System.Windows.Forms.Padding(0)
        Me.flpButtonRow.Name = "flpButtonRow"
        Me.flpButtonRow.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
        Me.flpButtonRow.Size = New System.Drawing.Size(920, 43)
        Me.flpButtonRow.TabIndex = 1
        Me.flpButtonRow.WrapContents = False
        '
        'btnSave
        '
        Me.btnSave.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Appearance.Options.UseFont = True
        Me.btnSave.Location = New System.Drawing.Point(12, 8)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnWhats
        '
        Me.btnWhats.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnWhats.Appearance.Options.UseFont = True
        Me.btnWhats.Location = New System.Drawing.Point(95, 8)
        Me.btnWhats.Margin = New System.Windows.Forms.Padding(4)
        Me.btnWhats.Name = "btnWhats"
        Me.btnWhats.Size = New System.Drawing.Size(75, 23)
        Me.btnWhats.TabIndex = 1
        Me.btnWhats.Text = "WhatsApp"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.Location = New System.Drawing.Point(178, 8)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'WeekSnapshotPreviewForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(920, 720)
        Me.Controls.Add(Me.pnlScroll)
        Me.Controls.Add(Me.tlpFooter)
        Me.Name = "WeekSnapshotPreviewForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "WeekSnapshotPreviewForm"
        Me.pnlScroll.ResumeLayout(False)
        Me.pnlScroll.PerformLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpFooter.ResumeLayout(False)
        Me.tlpFooter.PerformLayout()
        Me.flpWhatsRow.ResumeLayout(False)
        Me.flpWhatsRow.PerformLayout()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.flpButtonRow.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlScroll As System.Windows.Forms.Panel
    Friend WithEvents picPreview As System.Windows.Forms.PictureBox
    Friend WithEvents tlpFooter As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents flpWhatsRow As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblWhatsCaption As System.Windows.Forms.Label
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents flpButtonRow As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnWhats As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
End Class
