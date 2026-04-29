<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DicomViewerFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DicomViewerFrm))
        Me.panelTop = New DevExpress.XtraEditors.SidePanel()
        Me.chkBounce = New DevExpress.XtraEditors.CheckEdit()
        Me.speedTrack = New DevExpress.XtraEditors.TrackBarControl()
        Me.lblSpeed = New DevExpress.XtraEditors.LabelControl()
        Me.btnPlay = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNext = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrev = New DevExpress.XtraEditors.SimpleButton()
        Me.btnOpenFolder = New DevExpress.XtraEditors.SimpleButton()
        Me.btnOpenFile = New DevExpress.XtraEditors.SimpleButton()
        Me.lstFilesPanel = New DevExpress.XtraEditors.SidePanel()
        Me.lstFiles = New DevExpress.XtraEditors.ListBoxControl()
        Me.panelRight = New DevExpress.XtraEditors.SidePanel()
        Me.memoMeta = New DevExpress.XtraEditors.MemoEdit()
        Me.lstSeries = New DevExpress.XtraEditors.ListBoxControl()
        Me.lblBurnedIn = New DevExpress.XtraEditors.LabelControl()
        Me.lblOverlay = New DevExpress.XtraEditors.LabelControl()
        Me.lblMeta = New DevExpress.XtraEditors.LabelControl()
        Me.lblSeries = New DevExpress.XtraEditors.LabelControl()
        Me.lblLevel = New DevExpress.XtraEditors.LabelControl()
        Me.lblWindow = New DevExpress.XtraEditors.LabelControl()
        Me.btnExportSeries = New DevExpress.XtraEditors.SimpleButton()
        Me.btnExportFrame = New DevExpress.XtraEditors.SimpleButton()
        Me.btnWlReset = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFramePlay = New DevExpress.XtraEditors.SimpleButton()
        Me.levelTrack = New DevExpress.XtraEditors.TrackBarControl()
        Me.windowTrack = New DevExpress.XtraEditors.TrackBarControl()
        Me.frameTrack = New DevExpress.XtraEditors.TrackBarControl()
        Me.lblFrame = New DevExpress.XtraEditors.LabelControl()
        Me.PanelBottom = New DevExpress.XtraEditors.SidePanel()
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.picPanel = New DevExpress.XtraEditors.SidePanel()
        Me.picView = New DevExpress.XtraEditors.PictureEdit()
        Me.ofd = New DevExpress.XtraEditors.XtraOpenFileDialog(Me.components)
        Me.sfdExport = New DevExpress.XtraEditors.XtraSaveFileDialog(Me.components)
        Me.fbdExport = New DevExpress.XtraEditors.XtraFolderBrowserDialog(Me.components)
        Me.fbd = New DevExpress.XtraEditors.XtraFolderBrowserDialog(Me.components)
        Me.panelTop.SuspendLayout()
        CType(Me.chkBounce.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.speedTrack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.speedTrack.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lstFilesPanel.SuspendLayout()
        CType(Me.lstFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelRight.SuspendLayout()
        CType(Me.memoMeta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lstSeries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.levelTrack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.levelTrack.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.windowTrack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.windowTrack.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.frameTrack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.frameTrack.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelBottom.SuspendLayout()
        Me.picPanel.SuspendLayout()
        CType(Me.picView.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelTop
        '
        Me.panelTop.Controls.Add(Me.chkBounce)
        Me.panelTop.Controls.Add(Me.speedTrack)
        Me.panelTop.Controls.Add(Me.lblSpeed)
        Me.panelTop.Controls.Add(Me.btnPlay)
        Me.panelTop.Controls.Add(Me.btnNext)
        Me.panelTop.Controls.Add(Me.btnPrev)
        Me.panelTop.Controls.Add(Me.btnOpenFolder)
        Me.panelTop.Controls.Add(Me.btnOpenFile)
        resources.ApplyResources(Me.panelTop, "panelTop")
        Me.panelTop.Name = "panelTop"
        '
        'chkBounce
        '
        resources.ApplyResources(Me.chkBounce, "chkBounce")
        Me.chkBounce.Name = "chkBounce"
        Me.chkBounce.Properties.Appearance.Font = CType(resources.GetObject("chkBounce.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkBounce.Properties.Appearance.Options.UseFont = True
        Me.chkBounce.Properties.Caption = resources.GetString("chkBounce.Properties.Caption")
        '
        'speedTrack
        '
        resources.ApplyResources(Me.speedTrack, "speedTrack")
        Me.speedTrack.Name = "speedTrack"
        Me.speedTrack.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.speedTrack.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.speedTrack.Properties.LargeChange = 500
        Me.speedTrack.Properties.Maximum = 3000
        Me.speedTrack.Properties.Minimum = 10
        Me.speedTrack.Properties.ShowValueToolTip = True
        Me.speedTrack.Properties.SmallChange = 100
        Me.speedTrack.Value = 10
        '
        'lblSpeed
        '
        Me.lblSpeed.Appearance.Font = CType(resources.GetObject("lblSpeed.Appearance.Font"), System.Drawing.Font)
        Me.lblSpeed.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblSpeed, "lblSpeed")
        Me.lblSpeed.Name = "lblSpeed"
        '
        'btnPlay
        '
        Me.btnPlay.Appearance.Font = CType(resources.GetObject("btnPlay.Appearance.Font"), System.Drawing.Font)
        Me.btnPlay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnPlay, "btnPlay")
        Me.btnPlay.Name = "btnPlay"
        '
        'btnNext
        '
        Me.btnNext.Appearance.Font = CType(resources.GetObject("btnNext.Appearance.Font"), System.Drawing.Font)
        Me.btnNext.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnNext, "btnNext")
        Me.btnNext.Name = "btnNext"
        '
        'btnPrev
        '
        Me.btnPrev.Appearance.Font = CType(resources.GetObject("btnPrev.Appearance.Font"), System.Drawing.Font)
        Me.btnPrev.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnPrev, "btnPrev")
        Me.btnPrev.Name = "btnPrev"
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.Appearance.Font = CType(resources.GetObject("btnOpenFolder.Appearance.Font"), System.Drawing.Font)
        Me.btnOpenFolder.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnOpenFolder, "btnOpenFolder")
        Me.btnOpenFolder.Name = "btnOpenFolder"
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Appearance.Font = CType(resources.GetObject("btnOpenFile.Appearance.Font"), System.Drawing.Font)
        Me.btnOpenFile.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnOpenFile, "btnOpenFile")
        Me.btnOpenFile.Name = "btnOpenFile"
        '
        'lstFilesPanel
        '
        Me.lstFilesPanel.Controls.Add(Me.lstFiles)
        resources.ApplyResources(Me.lstFilesPanel, "lstFilesPanel")
        Me.lstFilesPanel.Name = "lstFilesPanel"
        '
        'lstFiles
        '
        Me.lstFiles.Appearance.Font = CType(resources.GetObject("lstFiles.Appearance.Font"), System.Drawing.Font)
        Me.lstFiles.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lstFiles, "lstFiles")
        Me.lstFiles.Name = "lstFiles"
        '
        'panelRight
        '
        Me.panelRight.Controls.Add(Me.memoMeta)
        Me.panelRight.Controls.Add(Me.lstSeries)
        Me.panelRight.Controls.Add(Me.lblBurnedIn)
        Me.panelRight.Controls.Add(Me.lblOverlay)
        Me.panelRight.Controls.Add(Me.lblMeta)
        Me.panelRight.Controls.Add(Me.lblSeries)
        Me.panelRight.Controls.Add(Me.lblLevel)
        Me.panelRight.Controls.Add(Me.lblWindow)
        Me.panelRight.Controls.Add(Me.btnExportSeries)
        Me.panelRight.Controls.Add(Me.btnExportFrame)
        Me.panelRight.Controls.Add(Me.btnWlReset)
        Me.panelRight.Controls.Add(Me.btnFramePlay)
        Me.panelRight.Controls.Add(Me.levelTrack)
        Me.panelRight.Controls.Add(Me.windowTrack)
        Me.panelRight.Controls.Add(Me.frameTrack)
        Me.panelRight.Controls.Add(Me.lblFrame)
        resources.ApplyResources(Me.panelRight, "panelRight")
        Me.panelRight.Name = "panelRight"
        '
        'memoMeta
        '
        resources.ApplyResources(Me.memoMeta, "memoMeta")
        Me.memoMeta.Name = "memoMeta"
        Me.memoMeta.Properties.Appearance.Font = CType(resources.GetObject("memoMeta.Properties.Appearance.Font"), System.Drawing.Font)
        Me.memoMeta.Properties.Appearance.Options.UseFont = True
        '
        'lstSeries
        '
        Me.lstSeries.Appearance.Font = CType(resources.GetObject("lstSeries.Appearance.Font"), System.Drawing.Font)
        Me.lstSeries.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lstSeries, "lstSeries")
        Me.lstSeries.Name = "lstSeries"
        '
        'lblBurnedIn
        '
        Me.lblBurnedIn.Appearance.Font = CType(resources.GetObject("lblBurnedIn.Appearance.Font"), System.Drawing.Font)
        Me.lblBurnedIn.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblBurnedIn, "lblBurnedIn")
        Me.lblBurnedIn.Name = "lblBurnedIn"
        '
        'lblOverlay
        '
        Me.lblOverlay.Appearance.Font = CType(resources.GetObject("lblOverlay.Appearance.Font"), System.Drawing.Font)
        Me.lblOverlay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblOverlay, "lblOverlay")
        Me.lblOverlay.Name = "lblOverlay"
        '
        'lblMeta
        '
        Me.lblMeta.Appearance.Font = CType(resources.GetObject("lblMeta.Appearance.Font"), System.Drawing.Font)
        Me.lblMeta.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblMeta, "lblMeta")
        Me.lblMeta.Name = "lblMeta"
        '
        'lblSeries
        '
        Me.lblSeries.Appearance.Font = CType(resources.GetObject("lblSeries.Appearance.Font"), System.Drawing.Font)
        Me.lblSeries.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblSeries, "lblSeries")
        Me.lblSeries.Name = "lblSeries"
        '
        'lblLevel
        '
        Me.lblLevel.Appearance.Font = CType(resources.GetObject("lblLevel.Appearance.Font"), System.Drawing.Font)
        Me.lblLevel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblLevel, "lblLevel")
        Me.lblLevel.Name = "lblLevel"
        '
        'lblWindow
        '
        Me.lblWindow.Appearance.Font = CType(resources.GetObject("lblWindow.Appearance.Font"), System.Drawing.Font)
        Me.lblWindow.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblWindow, "lblWindow")
        Me.lblWindow.Name = "lblWindow"
        '
        'btnExportSeries
        '
        Me.btnExportSeries.Appearance.Font = CType(resources.GetObject("btnExportSeries.Appearance.Font"), System.Drawing.Font)
        Me.btnExportSeries.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnExportSeries, "btnExportSeries")
        Me.btnExportSeries.Name = "btnExportSeries"
        '
        'btnExportFrame
        '
        Me.btnExportFrame.Appearance.Font = CType(resources.GetObject("btnExportFrame.Appearance.Font"), System.Drawing.Font)
        Me.btnExportFrame.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnExportFrame, "btnExportFrame")
        Me.btnExportFrame.Name = "btnExportFrame"
        '
        'btnWlReset
        '
        Me.btnWlReset.Appearance.Font = CType(resources.GetObject("btnWlReset.Appearance.Font"), System.Drawing.Font)
        Me.btnWlReset.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnWlReset, "btnWlReset")
        Me.btnWlReset.Name = "btnWlReset"
        '
        'btnFramePlay
        '
        Me.btnFramePlay.Appearance.Font = CType(resources.GetObject("btnFramePlay.Appearance.Font"), System.Drawing.Font)
        Me.btnFramePlay.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnFramePlay, "btnFramePlay")
        Me.btnFramePlay.Name = "btnFramePlay"
        '
        'levelTrack
        '
        resources.ApplyResources(Me.levelTrack, "levelTrack")
        Me.levelTrack.Name = "levelTrack"
        Me.levelTrack.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.levelTrack.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.levelTrack.Properties.LargeChange = 100
        Me.levelTrack.Properties.Maximum = 3071
        Me.levelTrack.Properties.Minimum = 1024
        Me.levelTrack.Properties.ShowValueToolTip = True
        Me.levelTrack.Properties.SmallChange = 10
        Me.levelTrack.Value = 1024
        '
        'windowTrack
        '
        resources.ApplyResources(Me.windowTrack, "windowTrack")
        Me.windowTrack.Name = "windowTrack"
        Me.windowTrack.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.windowTrack.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.windowTrack.Properties.LargeChange = 100
        Me.windowTrack.Properties.Maximum = 4096
        Me.windowTrack.Properties.Minimum = 1
        Me.windowTrack.Properties.ShowValueToolTip = True
        Me.windowTrack.Properties.SmallChange = 10
        Me.windowTrack.Value = 1
        '
        'frameTrack
        '
        resources.ApplyResources(Me.frameTrack, "frameTrack")
        Me.frameTrack.Name = "frameTrack"
        Me.frameTrack.Properties.LabelAppearance.Options.UseTextOptions = True
        Me.frameTrack.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.frameTrack.Properties.Maximum = 0
        Me.frameTrack.Properties.ShowValueToolTip = True
        '
        'lblFrame
        '
        Me.lblFrame.Appearance.Font = CType(resources.GetObject("lblFrame.Appearance.Font"), System.Drawing.Font)
        Me.lblFrame.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblFrame, "lblFrame")
        Me.lblFrame.Name = "lblFrame"
        '
        'PanelBottom
        '
        Me.PanelBottom.Controls.Add(Me.lblStatus)
        resources.ApplyResources(Me.PanelBottom, "PanelBottom")
        Me.PanelBottom.Name = "PanelBottom"
        '
        'lblStatus
        '
        Me.lblStatus.Appearance.Font = CType(resources.GetObject("lblStatus.Appearance.Font"), System.Drawing.Font)
        Me.lblStatus.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblStatus, "lblStatus")
        Me.lblStatus.Name = "lblStatus"
        '
        'picPanel
        '
        Me.picPanel.Controls.Add(Me.picView)
        resources.ApplyResources(Me.picPanel, "picPanel")
        Me.picPanel.Name = "picPanel"
        '
        'picView
        '
        resources.ApplyResources(Me.picView, "picView")
        Me.picView.Name = "picView"
        Me.picView.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.picView.Properties.ShowMenu = False
        Me.picView.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.[True]
        Me.picView.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'ofd
        '
        Me.ofd.FileName = "XtraOpenFileDialog1"
        '
        'sfdExport
        '
        Me.sfdExport.FileName = "XtraSaveFileDialog1"
        '
        'fbdExport
        '
        resources.ApplyResources(Me.fbdExport, "fbdExport")
        '
        'fbd
        '
        resources.ApplyResources(Me.fbd, "fbd")
        '
        'DicomViewerFrm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.picPanel)
        Me.Controls.Add(Me.PanelBottom)
        Me.Controls.Add(Me.panelRight)
        Me.Controls.Add(Me.lstFilesPanel)
        Me.Controls.Add(Me.panelTop)
        Me.IconOptions.Icon = CType(resources.GetObject("DicomViewerFrm.IconOptions.Icon"), System.Drawing.Icon)
        Me.Name = "DicomViewerFrm"
        Me.panelTop.ResumeLayout(False)
        Me.panelTop.PerformLayout()
        CType(Me.chkBounce.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.speedTrack.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.speedTrack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lstFilesPanel.ResumeLayout(False)
        CType(Me.lstFiles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelRight.ResumeLayout(False)
        Me.panelRight.PerformLayout()
        CType(Me.memoMeta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lstSeries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.levelTrack.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.levelTrack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.windowTrack.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.windowTrack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.frameTrack.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.frameTrack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelBottom.ResumeLayout(False)
        Me.PanelBottom.PerformLayout()
        Me.picPanel.ResumeLayout(False)
        CType(Me.picView.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelTop As DevExpress.XtraEditors.SidePanel
    Friend WithEvents lstFilesPanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents panelRight As DevExpress.XtraEditors.SidePanel
    Friend WithEvents PanelBottom As DevExpress.XtraEditors.SidePanel
    Friend WithEvents picPanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents btnOpenFolder As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnOpenFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrev As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblSpeed As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnPlay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents speedTrack As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents chkBounce As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lstFiles As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents picView As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents lblFrame As DevExpress.XtraEditors.LabelControl
    Friend WithEvents frameTrack As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents btnFramePlay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblWindow As DevExpress.XtraEditors.LabelControl
    Friend WithEvents windowTrack As DevExpress.XtraEditors.TrackBarControl
    Friend WithEvents lblLevel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSeries As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnWlReset As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lstSeries As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents lblMeta As DevExpress.XtraEditors.LabelControl
    Friend WithEvents memoMeta As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents lblBurnedIn As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOverlay As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnExportSeries As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExportFrame As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ofd As DevExpress.XtraEditors.XtraOpenFileDialog
    Friend WithEvents sfdExport As DevExpress.XtraEditors.XtraSaveFileDialog
    Friend WithEvents fbdExport As DevExpress.XtraEditors.XtraFolderBrowserDialog
    Friend WithEvents fbd As DevExpress.XtraEditors.XtraFolderBrowserDialog
    Friend WithEvents levelTrack As DevExpress.XtraEditors.TrackBarControl
End Class
