<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ImageThumbnailViewer
    Inherits DevExpress.XtraEditors.XtraUserControl

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.grpMain = New DevExpress.XtraEditors.GroupControl()
        CType(Me.grpMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpMain
        '
        Me.grpMain.AppearanceCaption.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.grpMain.AppearanceCaption.Options.UseFont = True
        Me.grpMain.AutoSize = True
        Me.grpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.grpMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.grpMain.CaptionLocation = DevExpress.Utils.Locations.Bottom
        Me.grpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpMain.Location = New System.Drawing.Point(0, 0)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(645, 150)
        Me.grpMain.TabIndex = 0
        Me.grpMain.Text = "0 Images"
        '
        'ImageThumbnailViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpMain)
        Me.Name = "ImageThumbnailViewer"
        Me.Size = New System.Drawing.Size(645, 150)
        CType(Me.grpMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents grpMain As DevExpress.XtraEditors.GroupControl
End Class
