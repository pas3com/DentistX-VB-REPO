<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ThumbNailViewer
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ThumbNailViewer))
        Me.RadiosPanel = New DevExpress.XtraEditors.SidePanel()
        Me.btnShowImages = New DevExpress.XtraEditors.SimpleButton()
        Me.RadioBefor = New System.Windows.Forms.RadioButton()
        Me.RadioDur = New System.Windows.Forms.RadioButton()
        Me.RadioAfter = New System.Windows.Forms.RadioButton()
        Me.cmdStart = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSaveImage = New DevExpress.XtraEditors.SimpleButton()
        Me.btLoadPics = New DevExpress.XtraEditors.SimpleButton()
        Me.btAddPics = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDel = New DevExpress.XtraEditors.SimpleButton()
        Me.BottomPanel = New DevExpress.XtraEditors.PanelControl()
        Me.PanelFilePath = New DevExpress.XtraEditors.SidePanel()
        Me.btnImgInPpt = New DevExpress.XtraEditors.SimpleButton()
        Me.btnBrowse = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelFilePath = New DevExpress.XtraEditors.LabelControl()
        Me.TextFilePath = New System.Windows.Forms.TextBox()
        Me.PanelPatientName = New DevExpress.XtraEditors.SidePanel()
        Me.ChkDrive = New DevExpress.XtraEditors.CheckEdit()
        Me.btnOpenEditor = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDicomViewer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCrop = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelPatname = New DevExpress.XtraEditors.LabelControl()
        Me.LabelPatientName = New DevExpress.XtraEditors.LabelControl()
        Me.ThumbsPanel = New DevExpress.XtraEditors.PanelControl()
        Me.ThumbsGrp = New DentistX.ThumbGalleryViewer()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsImageSize = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsX64 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsX128 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsX256 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsDeleteImage = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.CanvasPanel = New DevExpress.XtraEditors.PanelControl()
        Me.picCanvas = New DentistX.ImageBox()
        Me.sfdImage = New System.Windows.Forms.SaveFileDialog()
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog()
        Me.FBDlg = New DevExpress.XtraEditors.XtraFolderBrowserDialog(Me.components)
        Me.FolderBrwsrDlg = New System.Windows.Forms.FolderBrowserDialog()
        Me.PicsBSBefore = New System.Windows.Forms.BindingSource(Me.components)
        Me.PatientBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Patient_ImgsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PicsBSDuring = New System.Windows.Forms.BindingSource(Me.components)
        Me.PicsBSAfter = New System.Windows.Forms.BindingSource(Me.components)
        Me.RadiosPanel.SuspendLayout()
        CType(Me.BottomPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BottomPanel.SuspendLayout()
        Me.PanelFilePath.SuspendLayout()
        Me.PanelPatientName.SuspendLayout()
        CType(Me.ChkDrive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ThumbsPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ThumbsPanel.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.CanvasPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CanvasPanel.SuspendLayout()
        CType(Me.PicsBSBefore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Patient_ImgsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicsBSDuring, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicsBSAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadiosPanel
        '
        Me.RadiosPanel.AllowResize = False
        Me.RadiosPanel.Controls.Add(Me.btnShowImages)
        Me.RadiosPanel.Controls.Add(Me.RadioBefor)
        Me.RadiosPanel.Controls.Add(Me.RadioDur)
        Me.RadiosPanel.Controls.Add(Me.RadioAfter)
        resources.ApplyResources(Me.RadiosPanel, "RadiosPanel")
        Me.RadiosPanel.Name = "RadiosPanel"
        '
        'btnShowImages
        '
        Me.btnShowImages.Appearance.Font = CType(resources.GetObject("btnShowImages.Appearance.Font"), System.Drawing.Font)
        Me.btnShowImages.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnShowImages, "btnShowImages")
        Me.btnShowImages.Name = "btnShowImages"
        '
        'RadioBefor
        '
        resources.ApplyResources(Me.RadioBefor, "RadioBefor")
        Me.RadioBefor.Checked = True
        Me.RadioBefor.Name = "RadioBefor"
        Me.RadioBefor.TabStop = True
        Me.RadioBefor.UseVisualStyleBackColor = True
        '
        'RadioDur
        '
        resources.ApplyResources(Me.RadioDur, "RadioDur")
        Me.RadioDur.Name = "RadioDur"
        Me.RadioDur.UseVisualStyleBackColor = True
        '
        'RadioAfter
        '
        resources.ApplyResources(Me.RadioAfter, "RadioAfter")
        Me.RadioAfter.Name = "RadioAfter"
        Me.RadioAfter.UseVisualStyleBackColor = True
        '
        'cmdStart
        '
        Me.cmdStart.Appearance.Font = CType(resources.GetObject("cmdStart.Appearance.Font"), System.Drawing.Font)
        Me.cmdStart.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.cmdStart, "cmdStart")
        Me.cmdStart.Name = "cmdStart"
        '
        'btnEdit
        '
        Me.btnEdit.Appearance.Font = CType(resources.GetObject("btnEdit.Appearance.Font"), System.Drawing.Font)
        Me.btnEdit.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnEdit, "btnEdit")
        Me.btnEdit.Name = "btnEdit"
        '
        'btnSaveImage
        '
        Me.btnSaveImage.Appearance.Font = CType(resources.GetObject("btnSaveImage.Appearance.Font"), System.Drawing.Font)
        Me.btnSaveImage.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSaveImage, "btnSaveImage")
        Me.btnSaveImage.Name = "btnSaveImage"
        '
        'btLoadPics
        '
        Me.btLoadPics.Appearance.Font = CType(resources.GetObject("btLoadPics.Appearance.Font"), System.Drawing.Font)
        Me.btLoadPics.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btLoadPics, "btLoadPics")
        Me.btLoadPics.Name = "btLoadPics"
        '
        'btAddPics
        '
        Me.btAddPics.Appearance.Font = CType(resources.GetObject("btAddPics.Appearance.Font"), System.Drawing.Font)
        Me.btAddPics.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btAddPics, "btAddPics")
        Me.btAddPics.Name = "btAddPics"
        '
        'BtnDel
        '
        Me.BtnDel.Appearance.Font = CType(resources.GetObject("BtnDel.Appearance.Font"), System.Drawing.Font)
        Me.BtnDel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnDel, "BtnDel")
        Me.BtnDel.Name = "BtnDel"
        '
        'BottomPanel
        '
        Me.BottomPanel.Controls.Add(Me.PanelFilePath)
        Me.BottomPanel.Controls.Add(Me.PanelPatientName)
        resources.ApplyResources(Me.BottomPanel, "BottomPanel")
        Me.BottomPanel.Name = "BottomPanel"
        '
        'PanelFilePath
        '
        Me.PanelFilePath.AllowResize = False
        Me.PanelFilePath.Controls.Add(Me.btnImgInPpt)
        Me.PanelFilePath.Controls.Add(Me.cmdStart)
        Me.PanelFilePath.Controls.Add(Me.btnBrowse)
        Me.PanelFilePath.Controls.Add(Me.LabelFilePath)
        Me.PanelFilePath.Controls.Add(Me.TextFilePath)
        resources.ApplyResources(Me.PanelFilePath, "PanelFilePath")
        Me.PanelFilePath.Name = "PanelFilePath"
        '
        'btnImgInPpt
        '
        Me.btnImgInPpt.Appearance.Font = CType(resources.GetObject("btnImgInPpt.Appearance.Font"), System.Drawing.Font)
        Me.btnImgInPpt.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnImgInPpt, "btnImgInPpt")
        Me.btnImgInPpt.Name = "btnImgInPpt"
        '
        'btnBrowse
        '
        Me.btnBrowse.Appearance.Font = CType(resources.GetObject("btnBrowse.Appearance.Font"), System.Drawing.Font)
        Me.btnBrowse.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBrowse, "btnBrowse")
        Me.btnBrowse.Name = "btnBrowse"
        '
        'LabelFilePath
        '
        Me.LabelFilePath.Appearance.Font = CType(resources.GetObject("LabelFilePath.Appearance.Font"), System.Drawing.Font)
        Me.LabelFilePath.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelFilePath, "LabelFilePath")
        Me.LabelFilePath.Name = "LabelFilePath"
        '
        'TextFilePath
        '
        resources.ApplyResources(Me.TextFilePath, "TextFilePath")
        Me.TextFilePath.Name = "TextFilePath"
        '
        'PanelPatientName
        '
        Me.PanelPatientName.AllowResize = False
        Me.PanelPatientName.Controls.Add(Me.ChkDrive)
        Me.PanelPatientName.Controls.Add(Me.btnOpenEditor)
        Me.PanelPatientName.Controls.Add(Me.btnDicomViewer)
        Me.PanelPatientName.Controls.Add(Me.btnCrop)
        Me.PanelPatientName.Controls.Add(Me.btnEdit)
        Me.PanelPatientName.Controls.Add(Me.btnSaveImage)
        Me.PanelPatientName.Controls.Add(Me.btLoadPics)
        Me.PanelPatientName.Controls.Add(Me.btAddPics)
        Me.PanelPatientName.Controls.Add(Me.BtnDel)
        Me.PanelPatientName.Controls.Add(Me.LabelPatname)
        Me.PanelPatientName.Controls.Add(Me.LabelPatientName)
        resources.ApplyResources(Me.PanelPatientName, "PanelPatientName")
        Me.PanelPatientName.Name = "PanelPatientName"
        '
        'ChkDrive
        '
        resources.ApplyResources(Me.ChkDrive, "ChkDrive")
        Me.ChkDrive.Name = "ChkDrive"
        Me.ChkDrive.Properties.Appearance.Font = CType(resources.GetObject("ChkDrive.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ChkDrive.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.ChkDrive.Properties.Appearance.Options.UseFont = True
        Me.ChkDrive.Properties.Appearance.Options.UseForeColor = True
        Me.ChkDrive.Properties.Caption = resources.GetString("ChkDrive.Properties.Caption")
        '
        'btnOpenEditor
        '
        Me.btnOpenEditor.Appearance.Font = CType(resources.GetObject("btnOpenEditor.Appearance.Font"), System.Drawing.Font)
        Me.btnOpenEditor.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnOpenEditor, "btnOpenEditor")
        Me.btnOpenEditor.Name = "btnOpenEditor"
        '
        'btnDicomViewer
        '
        Me.btnDicomViewer.Appearance.Font = CType(resources.GetObject("btnDicomViewer.Appearance.Font"), System.Drawing.Font)
        Me.btnDicomViewer.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnDicomViewer, "btnDicomViewer")
        Me.btnDicomViewer.Name = "btnDicomViewer"
        '
        'btnCrop
        '
        Me.btnCrop.Appearance.Font = CType(resources.GetObject("btnCrop.Appearance.Font"), System.Drawing.Font)
        Me.btnCrop.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnCrop, "btnCrop")
        Me.btnCrop.Name = "btnCrop"
        '
        'LabelPatname
        '
        Me.LabelPatname.Appearance.Font = CType(resources.GetObject("LabelPatname.Appearance.Font"), System.Drawing.Font)
        Me.LabelPatname.Appearance.ForeColor = System.Drawing.Color.Fuchsia
        Me.LabelPatname.Appearance.Options.UseFont = True
        Me.LabelPatname.Appearance.Options.UseForeColor = True
        Me.LabelPatname.Appearance.Options.UseTextOptions = True
        Me.LabelPatname.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelPatname.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabelPatname, "LabelPatname")
        Me.LabelPatname.Name = "LabelPatname"
        '
        'LabelPatientName
        '
        Me.LabelPatientName.Appearance.Font = CType(resources.GetObject("LabelPatientName.Appearance.Font"), System.Drawing.Font)
        Me.LabelPatientName.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelPatientName, "LabelPatientName")
        Me.LabelPatientName.Name = "LabelPatientName"
        '
        'ThumbsPanel
        '
        Me.ThumbsPanel.Controls.Add(Me.ThumbsGrp)
        Me.ThumbsPanel.Controls.Add(Me.RadiosPanel)
        resources.ApplyResources(Me.ThumbsPanel, "ThumbsPanel")
        Me.ThumbsPanel.Name = "ThumbsPanel"
        '
        'ThumbsGrp
        '
        Me.ThumbsGrp.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ThumbsGrp.Dimension = 100
        Me.ThumbsGrp.DirectoryPath = ""
        resources.ApplyResources(Me.ThumbsGrp, "ThumbsGrp")
        Me.ThumbsGrp.Name = "ThumbsGrp"
        Me.ThumbsGrp.Spacing = 10
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsImageSize, Me.tsDeleteImage, Me.tsReload})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        resources.ApplyResources(Me.ContextMenuStrip1, "ContextMenuStrip1")
        '
        'tsImageSize
        '
        Me.tsImageSize.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsX64, Me.tsX128, Me.tsX256})
        Me.tsImageSize.Name = "tsImageSize"
        resources.ApplyResources(Me.tsImageSize, "tsImageSize")
        '
        'tsX64
        '
        Me.tsX64.AutoToolTip = True
        Me.tsX64.Name = "tsX64"
        resources.ApplyResources(Me.tsX64, "tsX64")
        '
        'tsX128
        '
        Me.tsX128.AutoToolTip = True
        Me.tsX128.Name = "tsX128"
        resources.ApplyResources(Me.tsX128, "tsX128")
        '
        'tsX256
        '
        Me.tsX256.AutoToolTip = True
        Me.tsX256.Name = "tsX256"
        resources.ApplyResources(Me.tsX256, "tsX256")
        '
        'tsDeleteImage
        '
        Me.tsDeleteImage.AutoToolTip = True
        Me.tsDeleteImage.Name = "tsDeleteImage"
        resources.ApplyResources(Me.tsDeleteImage, "tsDeleteImage")
        '
        'tsReload
        '
        Me.tsReload.AutoToolTip = True
        Me.tsReload.Name = "tsReload"
        resources.ApplyResources(Me.tsReload, "tsReload")
        '
        'CanvasPanel
        '
        Me.CanvasPanel.Controls.Add(Me.picCanvas)
        resources.ApplyResources(Me.CanvasPanel, "CanvasPanel")
        Me.CanvasPanel.Name = "CanvasPanel"
        '
        'picCanvas
        '
        resources.ApplyResources(Me.picCanvas, "picCanvas")
        Me.picCanvas.ImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picCanvas.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        Me.picCanvas.Name = "picCanvas"
        '
        'sfdImage
        '
        resources.ApplyResources(Me.sfdImage, "sfdImage")
        '
        'ofdImage
        '
        Me.ofdImage.FileName = "OpenFileDialog1"
        resources.ApplyResources(Me.ofdImage, "ofdImage")
        '
        'FBDlg
        '
        Me.FBDlg.DialogStyle = DevExpress.Utils.CommonDialogs.FolderBrowserDialogStyle.Wide
        Me.FBDlg.Multiselect = True
        Me.FBDlg.RootFolder = System.Environment.SpecialFolder.MyComputer
        resources.ApplyResources(Me.FBDlg, "FBDlg")
        '
        'ThumbNailViewer
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CanvasPanel)
        Me.Controls.Add(Me.ThumbsPanel)
        Me.Controls.Add(Me.BottomPanel)
        Me.Name = "ThumbNailViewer"
        Me.RadiosPanel.ResumeLayout(False)
        Me.RadiosPanel.PerformLayout()
        CType(Me.BottomPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BottomPanel.ResumeLayout(False)
        Me.PanelFilePath.ResumeLayout(False)
        Me.PanelFilePath.PerformLayout()
        Me.PanelPatientName.ResumeLayout(False)
        Me.PanelPatientName.PerformLayout()
        CType(Me.ChkDrive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ThumbsPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ThumbsPanel.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.CanvasPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CanvasPanel.ResumeLayout(False)
        CType(Me.PicsBSBefore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Patient_ImgsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicsBSDuring, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicsBSAfter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BottomPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ThumbsPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents CanvasPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents RadiosPanel As DevExpress.XtraEditors.SidePanel
    Friend WithEvents LabelPatientName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelPatname As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelFilePath As DevExpress.XtraEditors.SidePanel
    Friend WithEvents LabelFilePath As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TextFilePath As TextBox
    Friend WithEvents ThumbsGrp As ThumbGalleryViewer
    Friend WithEvents picCanvas As ImageBox
    Friend WithEvents RadioDur As RadioButton
    Friend WithEvents cmdStart As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RadioAfter As RadioButton
    Friend WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RadioBefor As RadioButton
    Friend WithEvents btnSaveImage As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLoadPics As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btAddPics As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelPatientName As DevExpress.XtraEditors.SidePanel
    Friend WithEvents PicsBSBefore As BindingSource
    Friend WithEvents PatientBindingSource As BindingSource
    Friend WithEvents Patient_ImgsBindingSource As BindingSource
    Friend WithEvents PicsBSDuring As BindingSource
    Friend WithEvents PicsBSAfter As BindingSource
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents tsImageSize As ToolStripMenuItem
    Friend WithEvents tsX64 As ToolStripMenuItem
    Friend WithEvents tsX128 As ToolStripMenuItem
    Friend WithEvents tsX256 As ToolStripMenuItem
    Friend WithEvents tsDeleteImage As ToolStripMenuItem
    Friend WithEvents tsReload As ToolStripMenuItem
    Friend WithEvents sfdImage As SaveFileDialog
    Friend WithEvents ofdImage As OpenFileDialog
    Friend WithEvents btnDicomViewer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCrop As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBrowse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FBDlg As DevExpress.XtraEditors.XtraFolderBrowserDialog
    Friend WithEvents btnOpenEditor As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnImgInPpt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FolderBrwsrDlg As FolderBrowserDialog
    Friend WithEvents btnShowImages As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ChkDrive As DevExpress.XtraEditors.CheckEdit
End Class
