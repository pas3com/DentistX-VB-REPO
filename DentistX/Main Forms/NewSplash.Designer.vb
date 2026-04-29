<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NewSplash
    Inherits DevExpress.XtraSplashScreen.SplashScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewSplash))
        Me.labelStatus = New DevExpress.XtraEditors.LabelControl()
        Me.labelCopyright = New DevExpress.XtraEditors.LabelControl()
        Me.progressBarControl = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.peImage = New DevExpress.XtraEditors.PictureEdit()
        Me.CultureMngr = New Infralution.Localization.CultureManager(Me.components)
        Me.RadioAr = New System.Windows.Forms.RadioButton()
        Me.RadioEn = New System.Windows.Forms.RadioButton()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.progressBarControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.peImage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'labelStatus
        '
        Me.labelStatus.Appearance.Font = CType(resources.GetObject("labelStatus.Appearance.Font"), System.Drawing.Font)
        Me.labelStatus.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.labelStatus, "labelStatus")
        Me.labelStatus.Name = "labelStatus"
        '
        'labelCopyright
        '
        Me.labelCopyright.Appearance.Font = CType(resources.GetObject("labelCopyright.Appearance.Font"), System.Drawing.Font)
        Me.labelCopyright.Appearance.Options.UseFont = True
        Me.labelCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.labelCopyright, "labelCopyright")
        Me.labelCopyright.Name = "labelCopyright"
        '
        'progressBarControl
        '
        resources.ApplyResources(Me.progressBarControl, "progressBarControl")
        Me.progressBarControl.Name = "progressBarControl"
        '
        'peImage
        '
        Me.peImage.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.peImage, "peImage")
        Me.peImage.EditValue = Global.DentistX.My.Resources.Resources.DentistXLogo2
        Me.peImage.Name = "peImage"
        Me.peImage.Properties.AllowFocused = False
        Me.peImage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.peImage.Properties.Appearance.Options.UseBackColor = True
        Me.peImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.peImage.Properties.ShowMenu = False
        Me.peImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.peImage.Properties.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        '
        'CultureMngr
        '
        Me.CultureMngr.ManagedControl = Me
        '
        'RadioAr
        '
        resources.ApplyResources(Me.RadioAr, "RadioAr")
        Me.RadioAr.Name = "RadioAr"
        Me.RadioAr.UseVisualStyleBackColor = True
        '
        'RadioEn
        '
        resources.ApplyResources(Me.RadioEn, "RadioEn")
        Me.RadioEn.Checked = True
        Me.RadioEn.Name = "RadioEn"
        Me.RadioEn.TabStop = True
        Me.RadioEn.UseVisualStyleBackColor = True
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = Global.DentistX.My.Resources.Resources.DentistXLogoBot
        resources.ApplyResources(Me.PictureEdit1, "PictureEdit1")
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.PictureEdit1.Properties.ShowMenu = False
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal
        '
        'NewSplash
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.labelStatus)
        Me.Controls.Add(Me.labelCopyright)
        Me.Controls.Add(Me.progressBarControl)
        Me.Controls.Add(Me.peImage)
        Me.Controls.Add(Me.RadioEn)
        Me.Controls.Add(Me.RadioAr)
        Me.Controls.Add(Me.PictureEdit1)
        Me.Name = "NewSplash"
        CType(Me.progressBarControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.peImage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents peImage As DevExpress.XtraEditors.PictureEdit
    Private WithEvents labelStatus As DevExpress.XtraEditors.LabelControl
    Private WithEvents labelCopyright As DevExpress.XtraEditors.LabelControl
    Private WithEvents progressBarControl As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents CultureMngr As Infralution.Localization.CultureManager
    Friend WithEvents RadioEn As RadioButton
    Friend WithEvents RadioAr As RadioButton
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
End Class
