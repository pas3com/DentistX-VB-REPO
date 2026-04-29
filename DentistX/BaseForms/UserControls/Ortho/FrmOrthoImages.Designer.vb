<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmOrthoImages
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmOrthoImages))
        Me.TableLayoutMain = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelBefore = New DevExpress.XtraEditors.PanelControl()
        Me.PictureBefore = New DevExpress.XtraEditors.PictureEdit()
        Me.PanelBeforeTop = New DevExpress.XtraEditors.PanelControl()
        Me.lblBeforeCount = New DevExpress.XtraEditors.LabelControl()
        Me.cmbBeforeFiles = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnBrowseBefore = New DevExpress.XtraEditors.SimpleButton()
        Me.lblBefore = New DevExpress.XtraEditors.LabelControl()
        Me.PanelDuring = New DevExpress.XtraEditors.PanelControl()
        Me.PictureDuring = New DevExpress.XtraEditors.PictureEdit()
        Me.PanelDuringTop = New DevExpress.XtraEditors.PanelControl()
        Me.lblDuringCount = New DevExpress.XtraEditors.LabelControl()
        Me.cmbDuringFiles = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnBrowseDuring = New DevExpress.XtraEditors.SimpleButton()
        Me.lblDuring = New DevExpress.XtraEditors.LabelControl()
        Me.PanelAfter = New DevExpress.XtraEditors.PanelControl()
        Me.PictureAfter = New DevExpress.XtraEditors.PictureEdit()
        Me.PanelAfterTop = New DevExpress.XtraEditors.PanelControl()
        Me.lblAfterCount = New DevExpress.XtraEditors.LabelControl()
        Me.cmbAfterFiles = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnBrowseAfter = New DevExpress.XtraEditors.SimpleButton()
        Me.lblAfter = New DevExpress.XtraEditors.LabelControl()
        Me.BottomPanel = New DevExpress.XtraEditors.PanelControl()
        Me.BottomLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.BottomLeftFlow = New System.Windows.Forms.FlowLayoutPanel()
        Me.chkSyncNavigation = New DevExpress.XtraEditors.CheckEdit()
        Me.chkShowDuring = New DevExpress.XtraEditors.CheckEdit()
        Me.chkFullResolution = New DevExpress.XtraEditors.CheckEdit()
        Me.BottomCenterFlow = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnPrev = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPlayPause = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNext = New DevExpress.XtraEditors.SimpleButton()
        Me.BottomRightPanel = New System.Windows.Forms.Panel()
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.SlideTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutMain.SuspendLayout()
        CType(Me.PanelBefore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelBefore.SuspendLayout()
        CType(Me.PictureBefore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelBeforeTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelBeforeTop.SuspendLayout()
        CType(Me.cmbBeforeFiles.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelDuring, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelDuring.SuspendLayout()
        CType(Me.PictureDuring.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelDuringTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelDuringTop.SuspendLayout()
        CType(Me.cmbDuringFiles.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelAfter.SuspendLayout()
        CType(Me.PictureAfter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelAfterTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelAfterTop.SuspendLayout()
        CType(Me.cmbAfterFiles.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BottomPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BottomPanel.SuspendLayout()
        Me.BottomLayout.SuspendLayout()
        Me.BottomLeftFlow.SuspendLayout()
        CType(Me.chkSyncNavigation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShowDuring.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFullResolution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BottomCenterFlow.SuspendLayout()
        Me.BottomRightPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutMain
        '
        resources.ApplyResources(Me.TableLayoutMain, "TableLayoutMain")
        Me.TableLayoutMain.Controls.Add(Me.PanelBefore, 0, 0)
        Me.TableLayoutMain.Controls.Add(Me.PanelDuring, 1, 0)
        Me.TableLayoutMain.Controls.Add(Me.PanelAfter, 2, 0)
        Me.TableLayoutMain.Name = "TableLayoutMain"
        '
        'PanelBefore
        '
        Me.PanelBefore.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.PanelBefore.Controls.Add(Me.PictureBefore)
        Me.PanelBefore.Controls.Add(Me.PanelBeforeTop)
        resources.ApplyResources(Me.PanelBefore, "PanelBefore")
        Me.PanelBefore.Name = "PanelBefore"
        '
        'PictureBefore
        '
        resources.ApplyResources(Me.PictureBefore, "PictureBefore")
        Me.PictureBefore.Name = "PictureBefore"
        Me.PictureBefore.Properties.Appearance.BackColor = System.Drawing.Color.Black
        Me.PictureBefore.Properties.Appearance.Options.UseBackColor = True
        Me.PictureBefore.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureBefore.Properties.NullText = resources.GetString("PictureBefore.Properties.NullText")
        Me.PictureBefore.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.PictureBefore.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'PanelBeforeTop
        '
        Me.PanelBeforeTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelBeforeTop.Controls.Add(Me.lblBeforeCount)
        Me.PanelBeforeTop.Controls.Add(Me.cmbBeforeFiles)
        Me.PanelBeforeTop.Controls.Add(Me.btnBrowseBefore)
        Me.PanelBeforeTop.Controls.Add(Me.lblBefore)
        resources.ApplyResources(Me.PanelBeforeTop, "PanelBeforeTop")
        Me.PanelBeforeTop.Name = "PanelBeforeTop"
        '
        'lblBeforeCount
        '
        Me.lblBeforeCount.Appearance.Font = CType(resources.GetObject("lblBeforeCount.Appearance.Font"), System.Drawing.Font)
        Me.lblBeforeCount.Appearance.ForeColor = System.Drawing.Color.Silver
        Me.lblBeforeCount.Appearance.Options.UseFont = True
        Me.lblBeforeCount.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblBeforeCount, "lblBeforeCount")
        Me.lblBeforeCount.Name = "lblBeforeCount"
        '
        'cmbBeforeFiles
        '
        resources.ApplyResources(Me.cmbBeforeFiles, "cmbBeforeFiles")
        Me.cmbBeforeFiles.Name = "cmbBeforeFiles"
        Me.cmbBeforeFiles.Properties.Appearance.Font = CType(resources.GetObject("cmbBeforeFiles.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cmbBeforeFiles.Properties.Appearance.Options.UseFont = True
        Me.cmbBeforeFiles.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbBeforeFiles.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbBeforeFiles.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'btnBrowseBefore
        '
        Me.btnBrowseBefore.Appearance.Font = CType(resources.GetObject("btnBrowseBefore.Appearance.Font"), System.Drawing.Font)
        Me.btnBrowseBefore.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBrowseBefore, "btnBrowseBefore")
        Me.btnBrowseBefore.Name = "btnBrowseBefore"
        '
        'lblBefore
        '
        Me.lblBefore.Appearance.Font = CType(resources.GetObject("lblBefore.Appearance.Font"), System.Drawing.Font)
        Me.lblBefore.Appearance.ForeColor = System.Drawing.Color.LightSkyBlue
        Me.lblBefore.Appearance.Options.UseFont = True
        Me.lblBefore.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblBefore, "lblBefore")
        Me.lblBefore.Name = "lblBefore"
        '
        'PanelDuring
        '
        Me.PanelDuring.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.PanelDuring.Controls.Add(Me.PictureDuring)
        Me.PanelDuring.Controls.Add(Me.PanelDuringTop)
        resources.ApplyResources(Me.PanelDuring, "PanelDuring")
        Me.PanelDuring.Name = "PanelDuring"
        '
        'PictureDuring
        '
        resources.ApplyResources(Me.PictureDuring, "PictureDuring")
        Me.PictureDuring.Name = "PictureDuring"
        Me.PictureDuring.Properties.Appearance.BackColor = System.Drawing.Color.Black
        Me.PictureDuring.Properties.Appearance.Options.UseBackColor = True
        Me.PictureDuring.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureDuring.Properties.NullText = resources.GetString("PictureDuring.Properties.NullText")
        Me.PictureDuring.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.PictureDuring.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'PanelDuringTop
        '
        Me.PanelDuringTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelDuringTop.Controls.Add(Me.lblDuringCount)
        Me.PanelDuringTop.Controls.Add(Me.cmbDuringFiles)
        Me.PanelDuringTop.Controls.Add(Me.btnBrowseDuring)
        Me.PanelDuringTop.Controls.Add(Me.lblDuring)
        resources.ApplyResources(Me.PanelDuringTop, "PanelDuringTop")
        Me.PanelDuringTop.Name = "PanelDuringTop"
        '
        'lblDuringCount
        '
        Me.lblDuringCount.Appearance.Font = CType(resources.GetObject("lblDuringCount.Appearance.Font"), System.Drawing.Font)
        Me.lblDuringCount.Appearance.ForeColor = System.Drawing.Color.Silver
        Me.lblDuringCount.Appearance.Options.UseFont = True
        Me.lblDuringCount.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblDuringCount, "lblDuringCount")
        Me.lblDuringCount.Name = "lblDuringCount"
        '
        'cmbDuringFiles
        '
        resources.ApplyResources(Me.cmbDuringFiles, "cmbDuringFiles")
        Me.cmbDuringFiles.Name = "cmbDuringFiles"
        Me.cmbDuringFiles.Properties.Appearance.Font = CType(resources.GetObject("cmbDuringFiles.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cmbDuringFiles.Properties.Appearance.Options.UseFont = True
        Me.cmbDuringFiles.Properties.AppearanceDropDown.Font = CType(resources.GetObject("cmbDuringFiles.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.cmbDuringFiles.Properties.AppearanceDropDown.Options.UseFont = True
        Me.cmbDuringFiles.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbDuringFiles.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbDuringFiles.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'btnBrowseDuring
        '
        Me.btnBrowseDuring.Appearance.Font = CType(resources.GetObject("btnBrowseDuring.Appearance.Font"), System.Drawing.Font)
        Me.btnBrowseDuring.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBrowseDuring, "btnBrowseDuring")
        Me.btnBrowseDuring.Name = "btnBrowseDuring"
        '
        'lblDuring
        '
        Me.lblDuring.Appearance.Font = CType(resources.GetObject("lblDuring.Appearance.Font"), System.Drawing.Font)
        Me.lblDuring.Appearance.ForeColor = System.Drawing.Color.Gold
        Me.lblDuring.Appearance.Options.UseFont = True
        Me.lblDuring.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblDuring, "lblDuring")
        Me.lblDuring.Name = "lblDuring"
        '
        'PanelAfter
        '
        Me.PanelAfter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.PanelAfter.Controls.Add(Me.PictureAfter)
        Me.PanelAfter.Controls.Add(Me.PanelAfterTop)
        resources.ApplyResources(Me.PanelAfter, "PanelAfter")
        Me.PanelAfter.Name = "PanelAfter"
        '
        'PictureAfter
        '
        resources.ApplyResources(Me.PictureAfter, "PictureAfter")
        Me.PictureAfter.Name = "PictureAfter"
        Me.PictureAfter.Properties.Appearance.BackColor = System.Drawing.Color.Black
        Me.PictureAfter.Properties.Appearance.Options.UseBackColor = True
        Me.PictureAfter.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureAfter.Properties.NullText = resources.GetString("PictureAfter.Properties.NullText")
        Me.PictureAfter.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.PictureAfter.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'PanelAfterTop
        '
        Me.PanelAfterTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelAfterTop.Controls.Add(Me.lblAfterCount)
        Me.PanelAfterTop.Controls.Add(Me.cmbAfterFiles)
        Me.PanelAfterTop.Controls.Add(Me.btnBrowseAfter)
        Me.PanelAfterTop.Controls.Add(Me.lblAfter)
        resources.ApplyResources(Me.PanelAfterTop, "PanelAfterTop")
        Me.PanelAfterTop.Name = "PanelAfterTop"
        '
        'lblAfterCount
        '
        Me.lblAfterCount.Appearance.Font = CType(resources.GetObject("lblAfterCount.Appearance.Font"), System.Drawing.Font)
        Me.lblAfterCount.Appearance.ForeColor = System.Drawing.Color.Silver
        Me.lblAfterCount.Appearance.Options.UseFont = True
        Me.lblAfterCount.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblAfterCount, "lblAfterCount")
        Me.lblAfterCount.Name = "lblAfterCount"
        '
        'cmbAfterFiles
        '
        resources.ApplyResources(Me.cmbAfterFiles, "cmbAfterFiles")
        Me.cmbAfterFiles.Name = "cmbAfterFiles"
        Me.cmbAfterFiles.Properties.Appearance.Font = CType(resources.GetObject("cmbAfterFiles.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cmbAfterFiles.Properties.Appearance.Options.UseFont = True
        Me.cmbAfterFiles.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbAfterFiles.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbAfterFiles.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'btnBrowseAfter
        '
        Me.btnBrowseAfter.Appearance.Font = CType(resources.GetObject("btnBrowseAfter.Appearance.Font"), System.Drawing.Font)
        Me.btnBrowseAfter.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBrowseAfter, "btnBrowseAfter")
        Me.btnBrowseAfter.Name = "btnBrowseAfter"
        '
        'lblAfter
        '
        Me.lblAfter.Appearance.Font = CType(resources.GetObject("lblAfter.Appearance.Font"), System.Drawing.Font)
        Me.lblAfter.Appearance.ForeColor = System.Drawing.Color.LightGreen
        Me.lblAfter.Appearance.Options.UseFont = True
        Me.lblAfter.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblAfter, "lblAfter")
        Me.lblAfter.Name = "lblAfter"
        '
        'BottomPanel
        '
        Me.BottomPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.BottomPanel.Controls.Add(Me.BottomLayout)
        resources.ApplyResources(Me.BottomPanel, "BottomPanel")
        Me.BottomPanel.Name = "BottomPanel"
        '
        'BottomLayout
        '
        resources.ApplyResources(Me.BottomLayout, "BottomLayout")
        Me.BottomLayout.Controls.Add(Me.BottomLeftFlow, 0, 0)
        Me.BottomLayout.Controls.Add(Me.BottomCenterFlow, 1, 0)
        Me.BottomLayout.Controls.Add(Me.BottomRightPanel, 2, 0)
        Me.BottomLayout.Name = "BottomLayout"
        '
        'BottomLeftFlow
        '
        Me.BottomLeftFlow.Controls.Add(Me.chkSyncNavigation)
        Me.BottomLeftFlow.Controls.Add(Me.chkShowDuring)
        Me.BottomLeftFlow.Controls.Add(Me.chkFullResolution)
        resources.ApplyResources(Me.BottomLeftFlow, "BottomLeftFlow")
        Me.BottomLeftFlow.Name = "BottomLeftFlow"
        '
        'chkSyncNavigation
        '
        resources.ApplyResources(Me.chkSyncNavigation, "chkSyncNavigation")
        Me.chkSyncNavigation.Name = "chkSyncNavigation"
        Me.chkSyncNavigation.Properties.Appearance.Font = CType(resources.GetObject("chkSyncNavigation.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkSyncNavigation.Properties.Appearance.Options.UseFont = True
        Me.chkSyncNavigation.Properties.Caption = resources.GetString("chkSyncNavigation.Properties.Caption")
        '
        'chkShowDuring
        '
        resources.ApplyResources(Me.chkShowDuring, "chkShowDuring")
        Me.chkShowDuring.Name = "chkShowDuring"
        Me.chkShowDuring.Properties.Appearance.Font = CType(resources.GetObject("chkShowDuring.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkShowDuring.Properties.Appearance.Options.UseFont = True
        Me.chkShowDuring.Properties.Caption = resources.GetString("chkShowDuring.Properties.Caption")
        '
        'chkFullResolution
        '
        resources.ApplyResources(Me.chkFullResolution, "chkFullResolution")
        Me.chkFullResolution.Name = "chkFullResolution"
        Me.chkFullResolution.Properties.Appearance.Font = CType(resources.GetObject("chkFullResolution.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkFullResolution.Properties.Appearance.Options.UseFont = True
        Me.chkFullResolution.Properties.Caption = resources.GetString("chkFullResolution.Properties.Caption")
        '
        'BottomCenterFlow
        '
        resources.ApplyResources(Me.BottomCenterFlow, "BottomCenterFlow")
        Me.BottomCenterFlow.Controls.Add(Me.btnPrev)
        Me.BottomCenterFlow.Controls.Add(Me.btnPlayPause)
        Me.BottomCenterFlow.Controls.Add(Me.btnNext)
        Me.BottomCenterFlow.Name = "BottomCenterFlow"
        '
        'btnPrev
        '
        resources.ApplyResources(Me.btnPrev, "btnPrev")
        Me.btnPrev.Appearance.Font = CType(resources.GetObject("btnPrev.Appearance.Font"), System.Drawing.Font)
        Me.btnPrev.Appearance.Options.UseFont = True
        Me.btnPrev.Name = "btnPrev"
        '
        'btnPlayPause
        '
        resources.ApplyResources(Me.btnPlayPause, "btnPlayPause")
        Me.btnPlayPause.Appearance.Font = CType(resources.GetObject("btnPlayPause.Appearance.Font"), System.Drawing.Font)
        Me.btnPlayPause.Appearance.Options.UseFont = True
        Me.btnPlayPause.Name = "btnPlayPause"
        '
        'btnNext
        '
        resources.ApplyResources(Me.btnNext, "btnNext")
        Me.btnNext.Appearance.Font = CType(resources.GetObject("btnNext.Appearance.Font"), System.Drawing.Font)
        Me.btnNext.Appearance.Options.UseFont = True
        Me.btnNext.Name = "btnNext"
        '
        'BottomRightPanel
        '
        Me.BottomRightPanel.Controls.Add(Me.lblStatus)
        resources.ApplyResources(Me.BottomRightPanel, "BottomRightPanel")
        Me.BottomRightPanel.Name = "BottomRightPanel"
        '
        'lblStatus
        '
        resources.ApplyResources(Me.lblStatus, "lblStatus")
        Me.lblStatus.Appearance.Font = CType(resources.GetObject("lblStatus.Appearance.Font"), System.Drawing.Font)
        Me.lblStatus.Appearance.ForeColor = System.Drawing.Color.Silver
        Me.lblStatus.Appearance.Options.UseFont = True
        Me.lblStatus.Appearance.Options.UseForeColor = True
        Me.lblStatus.Name = "lblStatus"
        '
        'SlideTimer
        '
        Me.SlideTimer.Interval = 3000
        '
        'FrmOrthoImages
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutMain)
        Me.Controls.Add(Me.BottomPanel)
        Me.Name = "FrmOrthoImages"
        Me.TableLayoutMain.ResumeLayout(False)
        CType(Me.PanelBefore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelBefore.ResumeLayout(False)
        CType(Me.PictureBefore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelBeforeTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelBeforeTop.ResumeLayout(False)
        Me.PanelBeforeTop.PerformLayout()
        CType(Me.cmbBeforeFiles.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelDuring, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelDuring.ResumeLayout(False)
        CType(Me.PictureDuring.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelDuringTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelDuringTop.ResumeLayout(False)
        Me.PanelDuringTop.PerformLayout()
        CType(Me.cmbDuringFiles.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelAfter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelAfter.ResumeLayout(False)
        CType(Me.PictureAfter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelAfterTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelAfterTop.ResumeLayout(False)
        Me.PanelAfterTop.PerformLayout()
        CType(Me.cmbAfterFiles.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BottomPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BottomPanel.ResumeLayout(False)
        Me.BottomLayout.ResumeLayout(False)
        Me.BottomLayout.PerformLayout()
        Me.BottomLeftFlow.ResumeLayout(False)
        CType(Me.chkSyncNavigation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShowDuring.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFullResolution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BottomCenterFlow.ResumeLayout(False)
        Me.BottomRightPanel.ResumeLayout(False)
        Me.BottomRightPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutMain As TableLayoutPanel
    Friend WithEvents PanelBefore As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelDuring As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelAfter As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PictureBefore As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents PictureDuring As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents PictureAfter As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents PanelBeforeTop As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelDuringTop As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelAfterTop As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblBefore As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDuring As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAfter As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbBeforeFiles As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbDuringFiles As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbAfterFiles As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnBrowseBefore As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBrowseDuring As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBrowseAfter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblBeforeCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDuringCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAfterCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BottomPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BottomLayout As TableLayoutPanel
    Friend WithEvents BottomLeftFlow As FlowLayoutPanel
    Friend WithEvents BottomCenterFlow As FlowLayoutPanel
    Friend WithEvents BottomRightPanel As Panel
    Friend WithEvents chkShowDuring As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkFullResolution As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkSyncNavigation As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnPrev As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPlayPause As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SlideTimer As System.Windows.Forms.Timer
End Class
