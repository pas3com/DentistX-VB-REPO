<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRxDetails
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
        Me.components = New System.ComponentModel.Container()
        Dim WtrTextLabel As DevExpress.XtraEditors.LabelControl
        Dim WtrImgLabel As DevExpress.XtraEditors.LabelControl
        Dim RxBdyIDLabel As DevExpress.XtraEditors.LabelControl
        Dim ArHdrNameLabel As DevExpress.XtraEditors.LabelControl
        Dim ArHdrAdresLabel As DevExpress.XtraEditors.LabelControl
        Dim EnHdrNameLabel As DevExpress.XtraEditors.LabelControl
        Dim EnHdrAdresLabel As DevExpress.XtraEditors.LabelControl
        Dim LogoLabel As DevExpress.XtraEditors.LabelControl
        Dim Label1 As DevExpress.XtraEditors.LabelControl
        Dim DetailLabel As DevExpress.XtraEditors.LabelControl
        Dim ArFtrLabel As DevExpress.XtraEditors.LabelControl
        Dim EnFtrLabel As DevExpress.XtraEditors.LabelControl
        Me.btnPreview = New DevExpress.XtraEditors.SimpleButton()
        Me.Wtr = New System.Windows.Forms.PictureBox()
        Me.UseWtrText = New DevExpress.XtraEditors.CheckEdit()
        Me.UseWtrImg = New DevExpress.XtraEditors.CheckEdit()
        Me.BtnBrowsWtr = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnBrowse = New DevExpress.XtraEditors.SimpleButton()
        Me.RxBdyIDTextBox = New DevExpress.XtraEditors.TextEdit()
        Me.Logo = New DevExpress.XtraEditors.PictureEdit()
        Me.OFD = New System.Windows.Forms.OpenFileDialog()
        Me.RxBodyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblArHdrName = New DevExpress.XtraEditors.LabelControl()
        Me.lblEnHdrName = New DevExpress.XtraEditors.LabelControl()
        Me.ArHdrAdresTextBox = New DevExpress.XtraEditors.MemoEdit()
        Me.EnHdrNameTextBox = New DevExpress.XtraEditors.MemoEdit()
        Me.ArHdrNameTextBox = New DevExpress.XtraEditors.MemoEdit()
        Me.EnHdrAdresTextBox = New DevExpress.XtraEditors.MemoEdit()
        Me.DetailTextBox = New DevExpress.XtraEditors.MemoEdit()
        Me.DrNameText = New DevExpress.XtraEditors.MemoEdit()
        Me.ArFtrTextBox = New DevExpress.XtraEditors.MemoEdit()
        Me.EnFtrTextBox = New DevExpress.XtraEditors.MemoEdit()
        Me.WtrTextTextBox = New DevExpress.XtraEditors.MemoEdit()
        Me.lblArHdrAdres = New DevExpress.XtraEditors.LabelControl()
        Me.lblEnHdrAdres = New DevExpress.XtraEditors.LabelControl()
        Me.lblArFtr = New DevExpress.XtraEditors.LabelControl()
        Me.lblEnFtr = New DevExpress.XtraEditors.LabelControl()
        Me.lblDetail = New DevExpress.XtraEditors.LabelControl()
        Me.btnDummy = New DevExpress.XtraEditors.SimpleButton()
        Me.btnReload = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditMode = New DevExpress.XtraEditors.SimpleButton()
        Me.btnGetDr = New DevExpress.XtraEditors.SimpleButton()
        WtrTextLabel = New DevExpress.XtraEditors.LabelControl()
        WtrImgLabel = New DevExpress.XtraEditors.LabelControl()
        RxBdyIDLabel = New DevExpress.XtraEditors.LabelControl()
        ArHdrNameLabel = New DevExpress.XtraEditors.LabelControl()
        ArHdrAdresLabel = New DevExpress.XtraEditors.LabelControl()
        EnHdrNameLabel = New DevExpress.XtraEditors.LabelControl()
        EnHdrAdresLabel = New DevExpress.XtraEditors.LabelControl()
        LogoLabel = New DevExpress.XtraEditors.LabelControl()
        Label1 = New DevExpress.XtraEditors.LabelControl()
        DetailLabel = New DevExpress.XtraEditors.LabelControl()
        ArFtrLabel = New DevExpress.XtraEditors.LabelControl()
        EnFtrLabel = New DevExpress.XtraEditors.LabelControl()
        CType(Me.Wtr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UseWtrText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UseWtrImg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RxBdyIDTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Logo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RxBodyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArHdrAdresTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EnHdrNameTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArHdrNameTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EnHdrAdresTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetailTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrNameText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArFtrTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EnFtrTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WtrTextTextBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'WtrTextLabel
        '
        WtrTextLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        WtrTextLabel.Appearance.Options.UseFont = True
        WtrTextLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        WtrTextLabel.Location = New System.Drawing.Point(534, 391)
        WtrTextLabel.Name = "WtrTextLabel"
        WtrTextLabel.Size = New System.Drawing.Size(66, 15)
        WtrTextLabel.TabIndex = 25
        WtrTextLabel.Text = "Water Text:"
        '
        'WtrImgLabel
        '
        WtrImgLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        WtrImgLabel.Location = New System.Drawing.Point(385, 244)
        WtrImgLabel.Name = "WtrImgLabel"
        WtrImgLabel.Size = New System.Drawing.Size(73, 15)
        WtrImgLabel.TabIndex = 21
        WtrImgLabel.Text = "Water Image:"
        '
        'RxBdyIDLabel
        '
        RxBdyIDLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        RxBdyIDLabel.Appearance.Options.UseFont = True
        RxBdyIDLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        RxBdyIDLabel.Location = New System.Drawing.Point(344, 6)
        RxBdyIDLabel.Name = "RxBdyIDLabel"
        RxBdyIDLabel.Size = New System.Drawing.Size(54, 15)
        RxBdyIDLabel.TabIndex = 0
        RxBdyIDLabel.Text = "Rx Bdy ID:"
        '
        'ArHdrNameLabel
        '
        ArHdrNameLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        ArHdrNameLabel.Appearance.Options.UseFont = True
        ArHdrNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        ArHdrNameLabel.Location = New System.Drawing.Point(701, 49)
        ArHdrNameLabel.Name = "ArHdrNameLabel"
        ArHdrNameLabel.Size = New System.Drawing.Size(117, 15)
        ArHdrNameLabel.TabIndex = 2
        ArHdrNameLabel.Text = "Arabic Header Name:"
        '
        'ArHdrAdresLabel
        '
        ArHdrAdresLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        ArHdrAdresLabel.Appearance.Options.UseFont = True
        ArHdrAdresLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        ArHdrAdresLabel.Location = New System.Drawing.Point(701, 101)
        ArHdrAdresLabel.Name = "ArHdrAdresLabel"
        ArHdrAdresLabel.Size = New System.Drawing.Size(128, 15)
        ArHdrAdresLabel.TabIndex = 4
        ArHdrAdresLabel.Text = "Arabic Header Address:"
        '
        'EnHdrNameLabel
        '
        EnHdrNameLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        EnHdrNameLabel.Appearance.Options.UseFont = True
        EnHdrNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        EnHdrNameLabel.Location = New System.Drawing.Point(28, 49)
        EnHdrNameLabel.Name = "EnHdrNameLabel"
        EnHdrNameLabel.Size = New System.Drawing.Size(120, 15)
        EnHdrNameLabel.TabIndex = 6
        EnHdrNameLabel.Text = "English Header Name:"
        '
        'EnHdrAdresLabel
        '
        EnHdrAdresLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        EnHdrAdresLabel.Appearance.Options.UseFont = True
        EnHdrAdresLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        EnHdrAdresLabel.Location = New System.Drawing.Point(15, 101)
        EnHdrAdresLabel.Name = "EnHdrAdresLabel"
        EnHdrAdresLabel.Size = New System.Drawing.Size(131, 15)
        EnHdrAdresLabel.TabIndex = 8
        EnHdrAdresLabel.Text = "English Header Address:"
        '
        'LogoLabel
        '
        LogoLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        LogoLabel.Location = New System.Drawing.Point(407, 30)
        LogoLabel.Name = "LogoLabel"
        LogoLabel.Size = New System.Drawing.Size(28, 15)
        LogoLabel.TabIndex = 10
        LogoLabel.Text = "Logo:"
        '
        'Label1
        '
        Label1.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Label1.Appearance.Options.UseFont = True
        Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label1.Location = New System.Drawing.Point(527, 412)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(53, 15)
        Label1.TabIndex = 15
        Label1.Text = "Dr Name:"
        '
        'DetailLabel
        '
        DetailLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        DetailLabel.Appearance.Options.UseFont = True
        DetailLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        DetailLabel.Location = New System.Drawing.Point(79, 219)
        DetailLabel.Name = "DetailLabel"
        DetailLabel.Size = New System.Drawing.Size(36, 15)
        DetailLabel.TabIndex = 13
        DetailLabel.Text = "Detail:"
        '
        'ArFtrLabel
        '
        ArFtrLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        ArFtrLabel.Appearance.Options.UseFont = True
        ArFtrLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        ArFtrLabel.Location = New System.Drawing.Point(701, 481)
        ArFtrLabel.Name = "ArFtrLabel"
        ArFtrLabel.Size = New System.Drawing.Size(78, 15)
        ArFtrLabel.TabIndex = 17
        ArFtrLabel.Text = "Arabic Footer:"
        '
        'EnFtrLabel
        '
        EnFtrLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        EnFtrLabel.Appearance.Options.UseFont = True
        EnFtrLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        EnFtrLabel.Location = New System.Drawing.Point(64, 481)
        EnFtrLabel.Name = "EnFtrLabel"
        EnFtrLabel.Size = New System.Drawing.Size(81, 15)
        EnFtrLabel.TabIndex = 19
        EnFtrLabel.Text = "English Footer:"
        '
        'btnPreview
        '
        Me.btnPreview.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnPreview.Location = New System.Drawing.Point(376, 529)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 30
        Me.btnPreview.Text = "Preview"
        '
        'Wtr
        '
        Me.Wtr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Wtr.Location = New System.Drawing.Point(371, 264)
        Me.Wtr.Name = "Wtr"
        Me.Wtr.Size = New System.Drawing.Size(100, 91)
        Me.Wtr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Wtr.TabIndex = 61
        Me.Wtr.TabStop = False
        '
        'UseWtrText
        '
        Me.UseWtrText.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.UseWtrText.Location = New System.Drawing.Point(170, 389)
        Me.UseWtrText.Name = "UseWtrText"
        Me.UseWtrText.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.UseWtrText.Properties.Appearance.Options.UseFont = True
        Me.UseWtrText.Properties.Caption = "Use Water Text ?"
        Me.UseWtrText.Size = New System.Drawing.Size(143, 19)
        Me.UseWtrText.TabIndex = 28
        '
        'UseWtrImg
        '
        Me.UseWtrImg.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.UseWtrImg.Location = New System.Drawing.Point(170, 295)
        Me.UseWtrImg.Name = "UseWtrImg"
        Me.UseWtrImg.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.UseWtrImg.Properties.Appearance.Options.UseFont = True
        Me.UseWtrImg.Properties.Caption = "Use Water Image ?"
        Me.UseWtrImg.Size = New System.Drawing.Size(143, 19)
        Me.UseWtrImg.TabIndex = 24
        '
        'BtnBrowsWtr
        '
        Me.BtnBrowsWtr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnBrowsWtr.Location = New System.Drawing.Point(384, 360)
        Me.BtnBrowsWtr.Name = "BtnBrowsWtr"
        Me.BtnBrowsWtr.Size = New System.Drawing.Size(75, 23)
        Me.BtnBrowsWtr.TabIndex = 22
        Me.BtnBrowsWtr.Text = "Browse"
        '
        'btnAdd
        '
        Me.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAdd.Location = New System.Drawing.Point(285, 529)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(81, 23)
        Me.btnAdd.TabIndex = 29
        Me.btnAdd.Text = "Add / Update"
        '
        'btnBrowse
        '
        Me.btnBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnBrowse.Location = New System.Drawing.Point(384, 150)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 11
        Me.btnBrowse.Text = "Browse"
        '
        'RxBdyIDTextBox
        '
        Me.RxBdyIDTextBox.Location = New System.Drawing.Point(404, 3)
        Me.RxBdyIDTextBox.Name = "RxBdyIDTextBox"
        Me.RxBdyIDTextBox.Properties.ReadOnly = True
        Me.RxBdyIDTextBox.Size = New System.Drawing.Size(100, 22)
        Me.RxBdyIDTextBox.TabIndex = 1
        '
        'Logo
        '
        Me.Logo.Location = New System.Drawing.Point(371, 48)
        Me.Logo.Name = "Logo"
        Me.Logo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.Logo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.Logo.Size = New System.Drawing.Size(100, 96)
        Me.Logo.TabIndex = 12
        '
        'OFD
        '
        Me.OFD.Filter = "All files (*.*)|*.*|Bitmap Files(*.bmp)|*.bmp|JPeg Files(*.jpg)|*.jpg|PNG Files(*" &
    ".png)|*.png"
        '
        'RxBodyBindingSource
        '
        Me.RxBodyBindingSource.DataMember = "RxBody"
        '
        'lblArHdrName
        '
        Me.lblArHdrName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblArHdrName.Location = New System.Drawing.Point(518, 22)
        Me.lblArHdrName.Name = "lblArHdrName"
        Me.lblArHdrName.Size = New System.Drawing.Size(135, 17)
        Me.lblArHdrName.TabIndex = 62
        Me.lblArHdrName.Text = "LabelControl1"
        '
        'lblEnHdrName
        '
        Me.lblEnHdrName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblEnHdrName.Location = New System.Drawing.Point(194, 18)
        Me.lblEnHdrName.Name = "lblEnHdrName"
        Me.lblEnHdrName.Size = New System.Drawing.Size(135, 21)
        Me.lblEnHdrName.TabIndex = 62
        Me.lblEnHdrName.Text = "LabelControl1"
        '
        'ArHdrAdresTextBox
        '
        Me.ArHdrAdresTextBox.EditValue = ""
        Me.ArHdrAdresTextBox.Location = New System.Drawing.Point(481, 73)
        Me.ArHdrAdresTextBox.Name = "ArHdrAdresTextBox"
        Me.ArHdrAdresTextBox.Properties.Appearance.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ArHdrAdresTextBox.Properties.Appearance.Options.UseFont = True
        Me.ArHdrAdresTextBox.Properties.Appearance.Options.UseTextOptions = True
        Me.ArHdrAdresTextBox.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.ArHdrAdresTextBox.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.ArHdrAdresTextBox.Properties.LinesCount = 3
        Me.ArHdrAdresTextBox.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.ArHdrAdresTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ArHdrAdresTextBox.Size = New System.Drawing.Size(209, 71)
        Me.ArHdrAdresTextBox.TabIndex = 63
        '
        'EnHdrNameTextBox
        '
        Me.EnHdrNameTextBox.EditValue = ""
        Me.EnHdrNameTextBox.Location = New System.Drawing.Point(157, 45)
        Me.EnHdrNameTextBox.Name = "EnHdrNameTextBox"
        Me.EnHdrNameTextBox.Properties.Appearance.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnHdrNameTextBox.Properties.Appearance.Options.UseFont = True
        Me.EnHdrNameTextBox.Properties.Appearance.Options.UseTextOptions = True
        Me.EnHdrNameTextBox.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EnHdrNameTextBox.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.EnHdrNameTextBox.Properties.LinesCount = 1
        Me.EnHdrNameTextBox.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.EnHdrNameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.EnHdrNameTextBox.Size = New System.Drawing.Size(209, 22)
        Me.EnHdrNameTextBox.TabIndex = 63
        '
        'ArHdrNameTextBox
        '
        Me.ArHdrNameTextBox.EditValue = ""
        Me.ArHdrNameTextBox.Location = New System.Drawing.Point(481, 45)
        Me.ArHdrNameTextBox.Name = "ArHdrNameTextBox"
        Me.ArHdrNameTextBox.Properties.Appearance.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ArHdrNameTextBox.Properties.Appearance.Options.UseFont = True
        Me.ArHdrNameTextBox.Properties.Appearance.Options.UseTextOptions = True
        Me.ArHdrNameTextBox.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.ArHdrNameTextBox.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.ArHdrNameTextBox.Properties.LinesCount = 1
        Me.ArHdrNameTextBox.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.ArHdrNameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ArHdrNameTextBox.Size = New System.Drawing.Size(209, 22)
        Me.ArHdrNameTextBox.TabIndex = 63
        '
        'EnHdrAdresTextBox
        '
        Me.EnHdrAdresTextBox.EditValue = ""
        Me.EnHdrAdresTextBox.Location = New System.Drawing.Point(157, 73)
        Me.EnHdrAdresTextBox.Name = "EnHdrAdresTextBox"
        Me.EnHdrAdresTextBox.Properties.Appearance.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnHdrAdresTextBox.Properties.Appearance.Options.UseFont = True
        Me.EnHdrAdresTextBox.Properties.Appearance.Options.UseTextOptions = True
        Me.EnHdrAdresTextBox.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EnHdrAdresTextBox.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.EnHdrAdresTextBox.Properties.LinesCount = 3
        Me.EnHdrAdresTextBox.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.EnHdrAdresTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.EnHdrAdresTextBox.Size = New System.Drawing.Size(209, 71)
        Me.EnHdrAdresTextBox.TabIndex = 63
        '
        'DetailTextBox
        '
        Me.DetailTextBox.EditValue = ""
        Me.DetailTextBox.Location = New System.Drawing.Point(157, 211)
        Me.DetailTextBox.Name = "DetailTextBox"
        Me.DetailTextBox.Properties.Appearance.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DetailTextBox.Properties.Appearance.Options.UseFont = True
        Me.DetailTextBox.Properties.Appearance.Options.UseTextOptions = True
        Me.DetailTextBox.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DetailTextBox.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DetailTextBox.Properties.LinesCount = 1
        Me.DetailTextBox.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DetailTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DetailTextBox.Size = New System.Drawing.Size(533, 28)
        Me.DetailTextBox.TabIndex = 63
        '
        'DrNameText
        '
        Me.DrNameText.EditValue = ""
        Me.DrNameText.Location = New System.Drawing.Point(583, 411)
        Me.DrNameText.Name = "DrNameText"
        Me.DrNameText.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.DrNameText.Properties.Appearance.Options.UseFont = True
        Me.DrNameText.Properties.Appearance.Options.UseTextOptions = True
        Me.DrNameText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.DrNameText.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.DrNameText.Properties.LinesCount = 1
        Me.DrNameText.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DrNameText.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DrNameText.Size = New System.Drawing.Size(107, 22)
        Me.DrNameText.TabIndex = 63
        '
        'ArFtrTextBox
        '
        Me.ArFtrTextBox.EditValue = ""
        Me.ArFtrTextBox.Location = New System.Drawing.Point(432, 463)
        Me.ArFtrTextBox.Name = "ArFtrTextBox"
        Me.ArFtrTextBox.Properties.Appearance.Font = New System.Drawing.Font("Simple Outline Pat", 12.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.ArFtrTextBox.Properties.Appearance.Options.UseFont = True
        Me.ArFtrTextBox.Properties.Appearance.Options.UseTextOptions = True
        Me.ArFtrTextBox.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.ArFtrTextBox.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.ArFtrTextBox.Properties.LinesCount = 2
        Me.ArFtrTextBox.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.ArFtrTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ArFtrTextBox.Size = New System.Drawing.Size(258, 60)
        Me.ArFtrTextBox.TabIndex = 63
        '
        'EnFtrTextBox
        '
        Me.EnFtrTextBox.EditValue = ""
        Me.EnFtrTextBox.Location = New System.Drawing.Point(157, 463)
        Me.EnFtrTextBox.Name = "EnFtrTextBox"
        Me.EnFtrTextBox.Properties.Appearance.Font = New System.Drawing.Font("Segoe Print", 12.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.EnFtrTextBox.Properties.Appearance.Options.UseFont = True
        Me.EnFtrTextBox.Properties.Appearance.Options.UseTextOptions = True
        Me.EnFtrTextBox.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EnFtrTextBox.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.EnFtrTextBox.Properties.LinesCount = 2
        Me.EnFtrTextBox.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.EnFtrTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.EnFtrTextBox.Size = New System.Drawing.Size(258, 60)
        Me.EnFtrTextBox.TabIndex = 63
        '
        'WtrTextTextBox
        '
        Me.WtrTextTextBox.EditValue = ""
        Me.WtrTextTextBox.Location = New System.Drawing.Point(319, 386)
        Me.WtrTextTextBox.Name = "WtrTextTextBox"
        Me.WtrTextTextBox.Properties.Appearance.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WtrTextTextBox.Properties.Appearance.Options.UseFont = True
        Me.WtrTextTextBox.Properties.Appearance.Options.UseTextOptions = True
        Me.WtrTextTextBox.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.WtrTextTextBox.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.WtrTextTextBox.Properties.LinesCount = 1
        Me.WtrTextTextBox.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.WtrTextTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.WtrTextTextBox.Size = New System.Drawing.Size(209, 22)
        Me.WtrTextTextBox.TabIndex = 63
        '
        'lblArHdrAdres
        '
        Me.lblArHdrAdres.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblArHdrAdres.Location = New System.Drawing.Point(518, 154)
        Me.lblArHdrAdres.Name = "lblArHdrAdres"
        Me.lblArHdrAdres.Size = New System.Drawing.Size(135, 17)
        Me.lblArHdrAdres.TabIndex = 62
        Me.lblArHdrAdres.Text = "LabelControl1"
        '
        'lblEnHdrAdres
        '
        Me.lblEnHdrAdres.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblEnHdrAdres.Location = New System.Drawing.Point(194, 150)
        Me.lblEnHdrAdres.Name = "lblEnHdrAdres"
        Me.lblEnHdrAdres.Size = New System.Drawing.Size(135, 21)
        Me.lblEnHdrAdres.TabIndex = 62
        Me.lblEnHdrAdres.Text = "LabelControl1"
        '
        'lblArFtr
        '
        Me.lblArFtr.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblArFtr.Location = New System.Drawing.Point(494, 442)
        Me.lblArFtr.Name = "lblArFtr"
        Me.lblArFtr.Size = New System.Drawing.Size(135, 17)
        Me.lblArFtr.TabIndex = 62
        Me.lblArFtr.Text = "LabelControl1"
        '
        'lblEnFtr
        '
        Me.lblEnFtr.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblEnFtr.Location = New System.Drawing.Point(219, 436)
        Me.lblEnFtr.Name = "lblEnFtr"
        Me.lblEnFtr.Size = New System.Drawing.Size(135, 21)
        Me.lblEnFtr.TabIndex = 62
        Me.lblEnFtr.Text = "LabelControl1"
        '
        'lblDetail
        '
        Me.lblDetail.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblDetail.Location = New System.Drawing.Point(356, 184)
        Me.lblDetail.Name = "lblDetail"
        Me.lblDetail.Size = New System.Drawing.Size(135, 21)
        Me.lblDetail.TabIndex = 62
        Me.lblDetail.Text = "LabelControl1"
        '
        'btnDummy
        '
        Me.btnDummy.Location = New System.Drawing.Point(564, 529)
        Me.btnDummy.Name = "btnDummy"
        Me.btnDummy.Size = New System.Drawing.Size(126, 23)
        Me.btnDummy.TabIndex = 64
        Me.btnDummy.Text = "Preview Dummy Data"
        '
        'btnReload
        '
        Me.btnReload.Location = New System.Drawing.Point(704, 529)
        Me.btnReload.Name = "btnReload"
        Me.btnReload.Size = New System.Drawing.Size(87, 23)
        Me.btnReload.TabIndex = 65
        Me.btnReload.Text = "Reload Data"
        '
        'btnEditMode
        '
        Me.btnEditMode.Location = New System.Drawing.Point(157, 529)
        Me.btnEditMode.Name = "btnEditMode"
        Me.btnEditMode.Size = New System.Drawing.Size(75, 23)
        Me.btnEditMode.TabIndex = 66
        Me.btnEditMode.Text = "Edit Mode"
        '
        'btnGetDr
        '
        Me.btnGetDr.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnGetDr.Appearance.Options.UseFont = True
        Me.btnGetDr.Location = New System.Drawing.Point(704, 409)
        Me.btnGetDr.Name = "btnGetDr"
        Me.btnGetDr.Size = New System.Drawing.Size(125, 23)
        Me.btnGetDr.TabIndex = 67
        Me.btnGetDr.Text = "Get Current Doctor"
        '
        'FrmRxDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(848, 568)
        Me.Controls.Add(Me.btnGetDr)
        Me.Controls.Add(Me.btnEditMode)
        Me.Controls.Add(Me.btnReload)
        Me.Controls.Add(Me.btnDummy)
        Me.Controls.Add(Me.DrNameText)
        Me.Controls.Add(Me.WtrTextTextBox)
        Me.Controls.Add(Me.ArHdrNameTextBox)
        Me.Controls.Add(Me.EnHdrNameTextBox)
        Me.Controls.Add(Me.EnHdrAdresTextBox)
        Me.Controls.Add(Me.DetailTextBox)
        Me.Controls.Add(Me.EnFtrTextBox)
        Me.Controls.Add(Me.ArFtrTextBox)
        Me.Controls.Add(Me.ArHdrAdresTextBox)
        Me.Controls.Add(Me.lblDetail)
        Me.Controls.Add(Me.lblEnFtr)
        Me.Controls.Add(Me.lblEnHdrAdres)
        Me.Controls.Add(Me.lblArFtr)
        Me.Controls.Add(Me.lblArHdrAdres)
        Me.Controls.Add(Me.lblEnHdrName)
        Me.Controls.Add(Me.lblArHdrName)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.Wtr)
        Me.Controls.Add(Me.UseWtrText)
        Me.Controls.Add(Me.UseWtrImg)
        Me.Controls.Add(WtrTextLabel)
        Me.Controls.Add(Me.BtnBrowsWtr)
        Me.Controls.Add(WtrImgLabel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(RxBdyIDLabel)
        Me.Controls.Add(Me.RxBdyIDTextBox)
        Me.Controls.Add(ArHdrNameLabel)
        Me.Controls.Add(ArHdrAdresLabel)
        Me.Controls.Add(EnHdrNameLabel)
        Me.Controls.Add(EnHdrAdresLabel)
        Me.Controls.Add(LogoLabel)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Label1)
        Me.Controls.Add(DetailLabel)
        Me.Controls.Add(ArFtrLabel)
        Me.Controls.Add(EnFtrLabel)
        Me.Name = "FrmRxDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FrmRxDetails"
        CType(Me.Wtr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UseWtrText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UseWtrImg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RxBdyIDTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Logo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RxBodyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArHdrAdresTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EnHdrNameTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArHdrNameTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EnHdrAdresTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetailTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrNameText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArFtrTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EnFtrTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WtrTextTextBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnPreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Wtr As PictureBox
    Friend WithEvents UseWtrText As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents UseWtrImg As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents BtnBrowsWtr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBrowse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RxBdyIDTextBox As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Logo As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents OFD As OpenFileDialog
    Friend WithEvents RxBodyBindingSource As BindingSource
    Friend WithEvents lblArHdrName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEnHdrName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ArHdrAdresTextBox As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents EnHdrNameTextBox As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ArHdrNameTextBox As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents EnHdrAdresTextBox As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents DetailTextBox As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents DrNameText As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ArFtrTextBox As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents EnFtrTextBox As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents WtrTextTextBox As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents lblArHdrAdres As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEnHdrAdres As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblArFtr As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEnFtr As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDetail As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnDummy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnReload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditMode As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnGetDr As DevExpress.XtraEditors.SimpleButton
End Class
