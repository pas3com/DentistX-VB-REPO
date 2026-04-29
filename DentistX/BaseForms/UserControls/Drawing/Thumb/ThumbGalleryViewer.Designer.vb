<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ThumbGalleryViewer
    Inherits DevExpress.XtraEditors.XtraUserControl

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.grpMain = New DevExpress.XtraEditors.GroupControl()
        Me.pbLoad = New System.Windows.Forms.ProgressBar()
        Me.gcThumbs = New DevExpress.XtraBars.Ribbon.GalleryControl()
        Me.GalleryControlClient1 = New DevExpress.XtraBars.Ribbon.GalleryControlClient()
        CType(Me.grpMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMain.SuspendLayout()
        CType(Me.gcThumbs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcThumbs.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpMain
        '
        Me.grpMain.AppearanceCaption.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.grpMain.AppearanceCaption.Options.UseFont = True
        Me.grpMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.grpMain.CaptionLocation = DevExpress.Utils.Locations.Bottom
        Me.grpMain.Controls.Add(Me.pbLoad)
        Me.grpMain.Controls.Add(Me.gcThumbs)
        Me.grpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpMain.Location = New System.Drawing.Point(0, 0)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(645, 150)
        Me.grpMain.TabIndex = 0
        Me.grpMain.Text = "0 Images"
        '
        'pbLoad
        '
        Me.pbLoad.Dock = System.Windows.Forms.DockStyle.Top
        Me.pbLoad.Location = New System.Drawing.Point(2, 1)
        Me.pbLoad.Margin = New System.Windows.Forms.Padding(0)
        Me.pbLoad.Maximum = 100
        Me.pbLoad.Name = "pbLoad"
        Me.pbLoad.Size = New System.Drawing.Size(641, 8)
        Me.pbLoad.TabIndex = 2
        Me.pbLoad.TabStop = False
        Me.pbLoad.Visible = False
        '
        'gcThumbs
        '
        Me.gcThumbs.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.gcThumbs.Controls.Add(Me.GalleryControlClient1)
        Me.gcThumbs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcThumbs.Location = New System.Drawing.Point(2, 1)
        Me.gcThumbs.Name = "gcThumbs"
        Me.gcThumbs.Size = New System.Drawing.Size(641, 122)
        Me.gcThumbs.TabIndex = 0
        '
        'GalleryControlClient1
        '
        Me.GalleryControlClient1.GalleryControl = Me.gcThumbs
        Me.GalleryControlClient1.Location = New System.Drawing.Point(1, 1)
        Me.GalleryControlClient1.Size = New System.Drawing.Size(622, 120)
        '
        'ThumbGalleryViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpMain)
        Me.Name = "ThumbGalleryViewer"
        Me.Size = New System.Drawing.Size(645, 150)
        CType(Me.grpMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMain.ResumeLayout(False)
        CType(Me.gcThumbs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcThumbs.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpMain As DevExpress.XtraEditors.GroupControl
    Friend WithEvents pbLoad As System.Windows.Forms.ProgressBar
    Friend WithEvents gcThumbs As DevExpress.XtraBars.Ribbon.GalleryControl
    Friend WithEvents GalleryControlClient1 As DevExpress.XtraBars.Ribbon.GalleryControlClient
End Class