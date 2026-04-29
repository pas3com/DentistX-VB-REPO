<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCrop
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
        Me.TopPanel = New DevExpress.XtraEditors.SidePanel()
        Me.resizingTrackBar = New System.Windows.Forms.TrackBar()
        Me.btnApplyExit = New DevExpress.XtraEditors.SimpleButton()
        Me.cropCancelBtn1 = New DevExpress.XtraEditors.SimpleButton()
        Me.cropSaveBtn = New DevExpress.XtraEditors.SimpleButton()
        Me.btnOpen = New DevExpress.XtraEditors.SimpleButton()
        Me.Label1 = New DevExpress.XtraEditors.LabelControl()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.OriginPic = New System.Windows.Forms.PictureBox()
        Me.CroppedPic = New System.Windows.Forms.PictureBox()
        Me.TopPanel.SuspendLayout()
        CType(Me.resizingTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.OriginPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CroppedPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TopPanel
        '
        Me.TopPanel.Controls.Add(Me.resizingTrackBar)
        Me.TopPanel.Controls.Add(Me.btnApplyExit)
        Me.TopPanel.Controls.Add(Me.cropCancelBtn1)
        Me.TopPanel.Controls.Add(Me.cropSaveBtn)
        Me.TopPanel.Controls.Add(Me.btnOpen)
        Me.TopPanel.Controls.Add(Me.Label1)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(1360, 51)
        Me.TopPanel.TabIndex = 0
        Me.TopPanel.Text = "SidePanel1"
        '
        'resizingTrackBar
        '
        Me.resizingTrackBar.AutoSize = False
        Me.resizingTrackBar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.resizingTrackBar.LargeChange = 1
        Me.resizingTrackBar.Location = New System.Drawing.Point(662, 13)
        Me.resizingTrackBar.Name = "resizingTrackBar"
        Me.resizingTrackBar.Size = New System.Drawing.Size(195, 25)
        Me.resizingTrackBar.SmallChange = 0
        Me.resizingTrackBar.TabIndex = 26
        Me.resizingTrackBar.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'btnApplyExit
        '
        Me.btnApplyExit.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnApplyExit.Appearance.Options.UseFont = True
        Me.btnApplyExit.Location = New System.Drawing.Point(488, 14)
        Me.btnApplyExit.Name = "btnApplyExit"
        Me.btnApplyExit.Size = New System.Drawing.Size(86, 23)
        Me.btnApplyExit.TabIndex = 25
        Me.btnApplyExit.Text = "Apply &&  Exit"
        '
        'cropCancelBtn1
        '
        Me.cropCancelBtn1.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cropCancelBtn1.Appearance.Options.UseFont = True
        Me.cropCancelBtn1.Location = New System.Drawing.Point(407, 14)
        Me.cropCancelBtn1.Name = "cropCancelBtn1"
        Me.cropCancelBtn1.Size = New System.Drawing.Size(75, 23)
        Me.cropCancelBtn1.TabIndex = 24
        Me.cropCancelBtn1.Text = "Clear"
        '
        'cropSaveBtn
        '
        Me.cropSaveBtn.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cropSaveBtn.Appearance.Options.UseFont = True
        Me.cropSaveBtn.Location = New System.Drawing.Point(326, 14)
        Me.cropSaveBtn.Name = "cropSaveBtn"
        Me.cropSaveBtn.Size = New System.Drawing.Size(75, 23)
        Me.cropSaveBtn.TabIndex = 23
        Me.cropSaveBtn.Text = "Save"
        '
        'btnOpen
        '
        Me.btnOpen.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnOpen.Appearance.Options.UseFont = True
        Me.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOpen.Location = New System.Drawing.Point(255, 14)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(65, 23)
        Me.btnOpen.TabIndex = 22
        Me.btnOpen.Text = "Open"
        '
        'Label1
        '
        Me.Label1.Appearance.Font = New System.Drawing.Font("Century Schoolbook", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Appearance.Options.UseFont = True
        Me.Label1.Location = New System.Drawing.Point(1058, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 14)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Preview"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 51)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.OriginPic)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.CroppedPic)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1360, 685)
        Me.SplitContainerControl1.SplitterPosition = 663
        Me.SplitContainerControl1.TabIndex = 1
        '
        'OriginPic
        '
        Me.OriginPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.OriginPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.OriginPic.Cursor = System.Windows.Forms.Cursors.Cross
        Me.OriginPic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OriginPic.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.OriginPic.Location = New System.Drawing.Point(0, 0)
        Me.OriginPic.Name = "OriginPic"
        Me.OriginPic.Size = New System.Drawing.Size(663, 685)
        Me.OriginPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.OriginPic.TabIndex = 1
        Me.OriginPic.TabStop = False
        '
        'CroppedPic
        '
        Me.CroppedPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CroppedPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CroppedPic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CroppedPic.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.CroppedPic.Location = New System.Drawing.Point(0, 0)
        Me.CroppedPic.Name = "CroppedPic"
        Me.CroppedPic.Size = New System.Drawing.Size(691, 685)
        Me.CroppedPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.CroppedPic.TabIndex = 7
        Me.CroppedPic.TabStop = False
        '
        'FrmCrop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1360, 736)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.TopPanel)
        Me.Name = "FrmCrop"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FrmCrop"
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        CType(Me.resizingTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.OriginPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CroppedPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TopPanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents btnApplyExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cropCancelBtn1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cropSaveBtn As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnOpen As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents resizingTrackBar As TrackBar
    Friend WithEvents OriginPic As PictureBox
    Friend WithEvents CroppedPic As PictureBox
End Class
