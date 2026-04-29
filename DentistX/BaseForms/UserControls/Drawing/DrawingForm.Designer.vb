<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DrawingForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DrawingForm))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.mnuFileNew = New System.Windows.Forms.MenuItem()
        Me.mnuFileOpen = New System.Windows.Forms.MenuItem()
        Me.mnuFileSave = New System.Windows.Forms.MenuItem()
        Me.mnuFileSaveImage = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.mnuFilePrintPreview = New System.Windows.Forms.MenuItem()
        Me.mnuFilePrint = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.mnuFileExit = New System.Windows.Forms.MenuItem()
        Me.mnuEdit = New System.Windows.Forms.MenuItem()
        Me.mnuEditDelete = New System.Windows.Forms.MenuItem()
        Me.mnuFormat = New System.Windows.Forms.MenuItem()
        Me.mnuFormatOrder = New System.Windows.Forms.MenuItem()
        Me.mnuFormatOrderBringToFront = New System.Windows.Forms.MenuItem()
        Me.mnuFormatOrderSendToBack = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.mnuOptSetBackColor = New System.Windows.Forms.MenuItem()
        Me.mnuFont = New System.Windows.Forms.MenuItem()
        Me.mnuBackground = New System.Windows.Forms.MenuItem()
        Me.mnuLoadBackground = New System.Windows.Forms.MenuItem()
        Me.mnuClearBackground = New System.Windows.Forms.MenuItem()
        Me.mnuBackgroundLayout = New System.Windows.Forms.MenuItem()
        Me.mnuLayoutNone = New System.Windows.Forms.MenuItem()
        Me.mnuLayoutTile = New System.Windows.Forms.MenuItem()
        Me.mnuLayoutCenter = New System.Windows.Forms.MenuItem()
        Me.mnuLayoutStretch = New System.Windows.Forms.MenuItem()
        Me.mnuLayoutZoom = New System.Windows.Forms.MenuItem()
        Me.mnuTransformation = New System.Windows.Forms.MenuItem()
        Me.mnuResetTransform = New System.Windows.Forms.MenuItem()
        Me.mnuFlipHorizontal = New System.Windows.Forms.MenuItem()
        Me.mnuFlipVertical = New System.Windows.Forms.MenuItem()
        Me.ctxLineThickness = New System.Windows.Forms.ContextMenu()
        Me.mnuThick1 = New System.Windows.Forms.MenuItem()
        Me.mnuThick2 = New System.Windows.Forms.MenuItem()
        Me.mnuThick3 = New System.Windows.Forms.MenuItem()
        Me.mnuThick4 = New System.Windows.Forms.MenuItem()
        Me.mnuThick5 = New System.Windows.Forms.MenuItem()
        Me.ctxLineColor = New System.Windows.Forms.ContextMenu()
        Me.mnuLineColorRed = New System.Windows.Forms.MenuItem()
        Me.mnuLineColorGreen = New System.Windows.Forms.MenuItem()
        Me.mnuLineColorBlue = New System.Windows.Forms.MenuItem()
        Me.mnuLineColorYellow = New System.Windows.Forms.MenuItem()
        Me.mnuLineColorOrange = New System.Windows.Forms.MenuItem()
        Me.mnuLineColorPurple = New System.Windows.Forms.MenuItem()
        Me.mnuLineColorBlack = New System.Windows.Forms.MenuItem()
        Me.mnuLineColorWhite = New System.Windows.Forms.MenuItem()
        Me.ctxFillColor = New System.Windows.Forms.ContextMenu()
        Me.mnuFillColorRed = New System.Windows.Forms.MenuItem()
        Me.mnuFillColorGreen = New System.Windows.Forms.MenuItem()
        Me.mnuFillColorBlue = New System.Windows.Forms.MenuItem()
        Me.mnuFillColorYellow = New System.Windows.Forms.MenuItem()
        Me.mnuFillColorOrange = New System.Windows.Forms.MenuItem()
        Me.mnuFillColorPurple = New System.Windows.Forms.MenuItem()
        Me.mnuFillColorBlack = New System.Windows.Forms.MenuItem()
        Me.mnuFillColorWhite = New System.Windows.Forms.MenuItem()
        Me.imlToolbarButtons = New System.Windows.Forms.ImageList(Me.components)
        Me.dlgSavePicture = New System.Windows.Forms.SaveFileDialog()
        Me.dlgOpenPicture = New System.Windows.Forms.OpenFileDialog()
        Me.dlgBackColor = New System.Windows.Forms.ColorDialog()
        Me.pdPrint = New System.Drawing.Printing.PrintDocument()
        Me.ppdPrint = New System.Windows.Forms.PrintPreviewDialog()
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog()
        Me.sfdImage = New System.Windows.Forms.SaveFileDialog()
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.tbtnPointer = New System.Windows.Forms.ToolBarButton()
        Me.tbtnLine = New System.Windows.Forms.ToolBarButton()
        Me.tbtnRectangle = New System.Windows.Forms.ToolBarButton()
        Me.tbtnEllipse = New System.Windows.Forms.ToolBarButton()
        Me.tbtnStar = New System.Windows.Forms.ToolBarButton()
        Me.tbtnImage = New System.Windows.Forms.ToolBarButton()
        Me.btnFreehand = New System.Windows.Forms.ToolBarButton()
        Me.tbtnPolyLine = New System.Windows.Forms.ToolBarButton()
        Me.tbtnArrow = New System.Windows.Forms.ToolBarButton()
        Me.btnText = New System.Windows.Forms.ToolBarButton()
        Me.btnCropBackground = New System.Windows.Forms.ToolBarButton()
        Me.tbarSep1 = New System.Windows.Forms.ToolBarButton()
        Me.tcboThickness = New System.Windows.Forms.ToolBarButton()
        Me.tcboLineColor = New System.Windows.Forms.ToolBarButton()
        Me.tcboFillColor = New System.Windows.Forms.ToolBarButton()
        Me.tbarSep2 = New System.Windows.Forms.ToolBarButton()
        Me.tbtnBringToFront = New System.Windows.Forms.ToolBarButton()
        Me.tbtnSendToBack = New System.Windows.Forms.ToolBarButton()
        Me.tbtnDelete = New System.Windows.Forms.ToolBarButton()
        Me.btnKeepShapes = New System.Windows.Forms.ToolBarButton()
        Me.btnRemoveShapes = New System.Windows.Forms.ToolBarButton()
        Me.picCanvas = New System.Windows.Forms.PictureBox()
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.mnuEdit, Me.mnuFormat, Me.MenuItem5, Me.mnuFont, Me.mnuBackground, Me.mnuTransformation})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileNew, Me.mnuFileOpen, Me.mnuFileSave, Me.mnuFileSaveImage, Me.MenuItem2, Me.mnuFilePrintPreview, Me.mnuFilePrint, Me.MenuItem6, Me.mnuFileExit})
        Me.MenuItem1.Text = "&File"
        '
        'mnuFileNew
        '
        Me.mnuFileNew.Index = 0
        Me.mnuFileNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN
        Me.mnuFileNew.Text = "&New"
        '
        'mnuFileOpen
        '
        Me.mnuFileOpen.Index = 1
        Me.mnuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
        Me.mnuFileOpen.Text = "&Open"
        '
        'mnuFileSave
        '
        Me.mnuFileSave.Index = 2
        Me.mnuFileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.mnuFileSave.Text = "&Save"
        '
        'mnuFileSaveImage
        '
        Me.mnuFileSaveImage.Index = 3
        Me.mnuFileSaveImage.Text = "Save &Image..."
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 4
        Me.MenuItem2.Text = "-"
        '
        'mnuFilePrintPreview
        '
        Me.mnuFilePrintPreview.Index = 5
        Me.mnuFilePrintPreview.Text = "Print Preview..."
        '
        'mnuFilePrint
        '
        Me.mnuFilePrint.Index = 6
        Me.mnuFilePrint.Text = "Print..."
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 7
        Me.MenuItem6.Text = "-"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Index = 8
        Me.mnuFileExit.Text = "E&xit"
        '
        'mnuEdit
        '
        Me.mnuEdit.Index = 1
        Me.mnuEdit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuEditDelete})
        Me.mnuEdit.Text = "&Edit"
        '
        'mnuEditDelete
        '
        Me.mnuEditDelete.Index = 0
        Me.mnuEditDelete.Shortcut = System.Windows.Forms.Shortcut.Del
        Me.mnuEditDelete.Text = "&Delete"
        '
        'mnuFormat
        '
        Me.mnuFormat.Index = 2
        Me.mnuFormat.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFormatOrder})
        Me.mnuFormat.Text = "F&ormat"
        '
        'mnuFormatOrder
        '
        Me.mnuFormatOrder.Index = 0
        Me.mnuFormatOrder.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFormatOrderBringToFront, Me.mnuFormatOrderSendToBack})
        Me.mnuFormatOrder.Text = "&Order"
        '
        'mnuFormatOrderBringToFront
        '
        Me.mnuFormatOrderBringToFront.Index = 0
        Me.mnuFormatOrderBringToFront.Text = "&Bring to Front"
        '
        'mnuFormatOrderSendToBack
        '
        Me.mnuFormatOrderSendToBack.Index = 1
        Me.mnuFormatOrderSendToBack.Text = "&Send to Back"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 3
        Me.MenuItem5.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuOptSetBackColor})
        Me.MenuItem5.Text = "&Options"
        '
        'mnuOptSetBackColor
        '
        Me.mnuOptSetBackColor.Index = 0
        Me.mnuOptSetBackColor.Shortcut = System.Windows.Forms.Shortcut.F4
        Me.mnuOptSetBackColor.Text = "&Set Background Color"
        '
        'mnuFont
        '
        Me.mnuFont.Index = 4
        Me.mnuFont.Text = "Font"
        '
        'mnuBackground
        '
        Me.mnuBackground.Index = 5
        Me.mnuBackground.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuLoadBackground, Me.mnuClearBackground, Me.mnuBackgroundLayout})
        Me.mnuBackground.Text = "BackGround"
        '
        'mnuLoadBackground
        '
        Me.mnuLoadBackground.Index = 0
        Me.mnuLoadBackground.Text = "Load Image"
        '
        'mnuClearBackground
        '
        Me.mnuClearBackground.Index = 1
        Me.mnuClearBackground.Text = "Clear Image"
        '
        'mnuBackgroundLayout
        '
        Me.mnuBackgroundLayout.Index = 2
        Me.mnuBackgroundLayout.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuLayoutNone, Me.mnuLayoutTile, Me.mnuLayoutCenter, Me.mnuLayoutStretch, Me.mnuLayoutZoom})
        Me.mnuBackgroundLayout.Text = "Image LayOut"
        '
        'mnuLayoutNone
        '
        Me.mnuLayoutNone.Index = 0
        Me.mnuLayoutNone.Text = "None"
        '
        'mnuLayoutTile
        '
        Me.mnuLayoutTile.Index = 1
        Me.mnuLayoutTile.Text = "Tile"
        '
        'mnuLayoutCenter
        '
        Me.mnuLayoutCenter.Index = 2
        Me.mnuLayoutCenter.Text = "Center"
        '
        'mnuLayoutStretch
        '
        Me.mnuLayoutStretch.Index = 3
        Me.mnuLayoutStretch.Text = "Strech"
        '
        'mnuLayoutZoom
        '
        Me.mnuLayoutZoom.Index = 4
        Me.mnuLayoutZoom.Text = "Zoom"
        '
        'mnuTransformation
        '
        Me.mnuTransformation.Index = 6
        Me.mnuTransformation.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuResetTransform, Me.mnuFlipHorizontal, Me.mnuFlipVertical})
        Me.mnuTransformation.Text = "Transformation"
        '
        'mnuResetTransform
        '
        Me.mnuResetTransform.Index = 0
        Me.mnuResetTransform.Text = "Reset Transform"
        '
        'mnuFlipHorizontal
        '
        Me.mnuFlipHorizontal.Index = 1
        Me.mnuFlipHorizontal.Text = "Flip Horizontal"
        '
        'mnuFlipVertical
        '
        Me.mnuFlipVertical.Index = 2
        Me.mnuFlipVertical.Text = "Flip Vertical"
        '
        'ctxLineThickness
        '
        Me.ctxLineThickness.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuThick1, Me.mnuThick2, Me.mnuThick3, Me.mnuThick4, Me.mnuThick5})
        '
        'mnuThick1
        '
        Me.mnuThick1.Index = 0
        Me.mnuThick1.Text = "1"
        '
        'mnuThick2
        '
        Me.mnuThick2.Index = 1
        Me.mnuThick2.Text = "2"
        '
        'mnuThick3
        '
        Me.mnuThick3.Index = 2
        Me.mnuThick3.Text = "3"
        '
        'mnuThick4
        '
        Me.mnuThick4.Index = 3
        Me.mnuThick4.Text = "4"
        '
        'mnuThick5
        '
        Me.mnuThick5.Index = 4
        Me.mnuThick5.Text = "5"
        '
        'ctxLineColor
        '
        Me.ctxLineColor.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuLineColorRed, Me.mnuLineColorGreen, Me.mnuLineColorBlue, Me.mnuLineColorYellow, Me.mnuLineColorOrange, Me.mnuLineColorPurple, Me.mnuLineColorBlack, Me.mnuLineColorWhite})
        '
        'mnuLineColorRed
        '
        Me.mnuLineColorRed.Index = 0
        Me.mnuLineColorRed.Text = "Red"
        '
        'mnuLineColorGreen
        '
        Me.mnuLineColorGreen.Index = 1
        Me.mnuLineColorGreen.Text = "Green"
        '
        'mnuLineColorBlue
        '
        Me.mnuLineColorBlue.Index = 2
        Me.mnuLineColorBlue.Text = "Blue"
        '
        'mnuLineColorYellow
        '
        Me.mnuLineColorYellow.Index = 3
        Me.mnuLineColorYellow.Text = "Yellow"
        '
        'mnuLineColorOrange
        '
        Me.mnuLineColorOrange.Index = 4
        Me.mnuLineColorOrange.Text = "Orange"
        '
        'mnuLineColorPurple
        '
        Me.mnuLineColorPurple.Index = 5
        Me.mnuLineColorPurple.Text = "Purple"
        '
        'mnuLineColorBlack
        '
        Me.mnuLineColorBlack.Index = 6
        Me.mnuLineColorBlack.Text = "Black"
        '
        'mnuLineColorWhite
        '
        Me.mnuLineColorWhite.Index = 7
        Me.mnuLineColorWhite.Text = "White"
        '
        'ctxFillColor
        '
        Me.ctxFillColor.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFillColorRed, Me.mnuFillColorGreen, Me.mnuFillColorBlue, Me.mnuFillColorYellow, Me.mnuFillColorOrange, Me.mnuFillColorPurple, Me.mnuFillColorBlack, Me.mnuFillColorWhite})
        '
        'mnuFillColorRed
        '
        Me.mnuFillColorRed.Index = 0
        Me.mnuFillColorRed.Text = "Red"
        '
        'mnuFillColorGreen
        '
        Me.mnuFillColorGreen.Index = 1
        Me.mnuFillColorGreen.Text = "Green"
        '
        'mnuFillColorBlue
        '
        Me.mnuFillColorBlue.Index = 2
        Me.mnuFillColorBlue.Text = "Blue"
        '
        'mnuFillColorYellow
        '
        Me.mnuFillColorYellow.Index = 3
        Me.mnuFillColorYellow.Text = "Yellow"
        '
        'mnuFillColorOrange
        '
        Me.mnuFillColorOrange.Index = 4
        Me.mnuFillColorOrange.Text = "Orange"
        '
        'mnuFillColorPurple
        '
        Me.mnuFillColorPurple.Index = 5
        Me.mnuFillColorPurple.Text = "Purple"
        '
        'mnuFillColorBlack
        '
        Me.mnuFillColorBlack.Index = 6
        Me.mnuFillColorBlack.Text = "Black"
        '
        'mnuFillColorWhite
        '
        Me.mnuFillColorWhite.Index = 7
        Me.mnuFillColorWhite.Text = "White"
        '
        'imlToolbarButtons
        '
        Me.imlToolbarButtons.ImageStream = CType(resources.GetObject("imlToolbarButtons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlToolbarButtons.TransparentColor = System.Drawing.Color.Transparent
        Me.imlToolbarButtons.Images.SetKeyName(0, "")
        Me.imlToolbarButtons.Images.SetKeyName(1, "")
        Me.imlToolbarButtons.Images.SetKeyName(2, "")
        Me.imlToolbarButtons.Images.SetKeyName(3, "")
        Me.imlToolbarButtons.Images.SetKeyName(4, "")
        Me.imlToolbarButtons.Images.SetKeyName(5, "")
        Me.imlToolbarButtons.Images.SetKeyName(6, "")
        Me.imlToolbarButtons.Images.SetKeyName(7, "")
        Me.imlToolbarButtons.Images.SetKeyName(8, "tbtnImage.bmp")
        Me.imlToolbarButtons.Images.SetKeyName(9, "tbtnArrowRight16.png")
        Me.imlToolbarButtons.Images.SetKeyName(10, "tbtnCrop16.png")
        Me.imlToolbarButtons.Images.SetKeyName(11, "tbtnEraser16.png")
        Me.imlToolbarButtons.Images.SetKeyName(12, "tbtnFlip-Horizontal16.png")
        Me.imlToolbarButtons.Images.SetKeyName(13, "tbtnFlip-Vertical16.png")
        Me.imlToolbarButtons.Images.SetKeyName(14, "tbtnFreeHand16.png")
        Me.imlToolbarButtons.Images.SetKeyName(15, "tbtnPolyline16.png")
        Me.imlToolbarButtons.Images.SetKeyName(16, "tbtnRotate-ccw16.png")
        Me.imlToolbarButtons.Images.SetKeyName(17, "tbtnRotate-cw16.png")
        Me.imlToolbarButtons.Images.SetKeyName(18, "tbtnText16.png")
        '
        'dlgSavePicture
        '
        Me.dlgSavePicture.Filter = "Picture Files (*.pic)|*.pic|All FIles (*.*)|*.*"
        '
        'dlgOpenPicture
        '
        Me.dlgOpenPicture.Filter = "Picture Files (*.pic)|*.pic|All FIles (*.*)|*.*"
        '
        'pdPrint
        '
        '
        'ppdPrint
        '
        Me.ppdPrint.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.ppdPrint.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.ppdPrint.ClientSize = New System.Drawing.Size(400, 300)
        Me.ppdPrint.Document = Me.pdPrint
        Me.ppdPrint.Enabled = True
        Me.ppdPrint.Icon = CType(resources.GetObject("ppdPrint.Icon"), System.Drawing.Icon)
        Me.ppdPrint.Name = "ppdPrint"
        Me.ppdPrint.Visible = False
        '
        'ofdImage
        '
        Me.ofdImage.FileName = "OpenFileDialog1"
        Me.ofdImage.Filter = "Graphics Files|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tif;*.tiff|All Files|*.*"
        '
        'sfdImage
        '
        Me.sfdImage.Filter = "Graphics Files|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tif;*.tiff|All Files|*.*"
        '
        'ToolBar1
        '
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbtnPointer, Me.tbtnLine, Me.tbtnRectangle, Me.tbtnEllipse, Me.tbtnStar, Me.tbtnImage, Me.btnFreehand, Me.tbtnPolyLine, Me.tbtnArrow, Me.btnText, Me.btnCropBackground, Me.tbarSep1, Me.tcboThickness, Me.tcboLineColor, Me.tcboFillColor, Me.tbarSep2, Me.tbtnBringToFront, Me.tbtnSendToBack, Me.tbtnDelete, Me.btnKeepShapes, Me.btnRemoveShapes})
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.imlToolbarButtons
        Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(1190, 28)
        Me.ToolBar1.TabIndex = 3
        '
        'tbtnPointer
        '
        Me.tbtnPointer.ImageIndex = 0
        Me.tbtnPointer.Name = "tbtnPointer"
        Me.tbtnPointer.Pushed = True
        Me.tbtnPointer.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbtnPointer.ToolTipText = "Pointer"
        '
        'tbtnLine
        '
        Me.tbtnLine.ImageIndex = 1
        Me.tbtnLine.Name = "tbtnLine"
        Me.tbtnLine.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbtnLine.ToolTipText = "Line"
        '
        'tbtnRectangle
        '
        Me.tbtnRectangle.ImageIndex = 2
        Me.tbtnRectangle.Name = "tbtnRectangle"
        Me.tbtnRectangle.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbtnRectangle.ToolTipText = "Rectangle"
        '
        'tbtnEllipse
        '
        Me.tbtnEllipse.ImageIndex = 3
        Me.tbtnEllipse.Name = "tbtnEllipse"
        Me.tbtnEllipse.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbtnEllipse.ToolTipText = "Ellipse"
        '
        'tbtnStar
        '
        Me.tbtnStar.ImageIndex = 4
        Me.tbtnStar.Name = "tbtnStar"
        Me.tbtnStar.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbtnStar.ToolTipText = "Star"
        '
        'tbtnImage
        '
        Me.tbtnImage.ImageIndex = 8
        Me.tbtnImage.Name = "tbtnImage"
        Me.tbtnImage.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbtnImage.ToolTipText = "Image"
        '
        'btnFreehand
        '
        Me.btnFreehand.ImageIndex = 14
        Me.btnFreehand.Name = "btnFreehand"
        Me.btnFreehand.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.btnFreehand.ToolTipText = "FreeHand"
        '
        'tbtnPolyLine
        '
        Me.tbtnPolyLine.ImageIndex = 15
        Me.tbtnPolyLine.Name = "tbtnPolyLine"
        Me.tbtnPolyLine.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbtnPolyLine.ToolTipText = "PolyLine"
        '
        'tbtnArrow
        '
        Me.tbtnArrow.ImageIndex = 9
        Me.tbtnArrow.Name = "tbtnArrow"
        Me.tbtnArrow.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbtnArrow.ToolTipText = "Arrow"
        '
        'btnText
        '
        Me.btnText.ImageIndex = 18
        Me.btnText.Name = "btnText"
        Me.btnText.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.btnText.ToolTipText = "Text"
        '
        'btnCropBackground
        '
        Me.btnCropBackground.ImageIndex = 10
        Me.btnCropBackground.Name = "btnCropBackground"
        Me.btnCropBackground.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.btnCropBackground.ToolTipText = "CropBackground"
        '
        'tbarSep1
        '
        Me.tbarSep1.Name = "tbarSep1"
        Me.tbarSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tcboThickness
        '
        Me.tcboThickness.DropDownMenu = Me.ctxLineThickness
        Me.tcboThickness.Name = "tcboThickness"
        Me.tcboThickness.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton
        Me.tcboThickness.ToolTipText = "Line thickness"
        '
        'tcboLineColor
        '
        Me.tcboLineColor.DropDownMenu = Me.ctxLineColor
        Me.tcboLineColor.Name = "tcboLineColor"
        Me.tcboLineColor.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton
        Me.tcboLineColor.ToolTipText = "Line color"
        '
        'tcboFillColor
        '
        Me.tcboFillColor.DropDownMenu = Me.ctxFillColor
        Me.tcboFillColor.Name = "tcboFillColor"
        Me.tcboFillColor.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton
        Me.tcboFillColor.ToolTipText = "Fill color"
        '
        'tbarSep2
        '
        Me.tbarSep2.Name = "tbarSep2"
        Me.tbarSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tbtnBringToFront
        '
        Me.tbtnBringToFront.ImageIndex = 5
        Me.tbtnBringToFront.Name = "tbtnBringToFront"
        Me.tbtnBringToFront.ToolTipText = "Bring To Front"
        '
        'tbtnSendToBack
        '
        Me.tbtnSendToBack.ImageIndex = 6
        Me.tbtnSendToBack.Name = "tbtnSendToBack"
        Me.tbtnSendToBack.ToolTipText = "Send To Back"
        '
        'tbtnDelete
        '
        Me.tbtnDelete.ImageIndex = 7
        Me.tbtnDelete.Name = "tbtnDelete"
        Me.tbtnDelete.ToolTipText = "Delete"
        '
        'btnKeepShapes
        '
        Me.btnKeepShapes.ImageIndex = 12
        Me.btnKeepShapes.Name = "btnKeepShapes"
        Me.btnKeepShapes.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.btnKeepShapes.ToolTipText = "KeepShapes"
        '
        'btnRemoveShapes
        '
        Me.btnRemoveShapes.ImageIndex = 13
        Me.btnRemoveShapes.Name = "btnRemoveShapes"
        Me.btnRemoveShapes.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.btnRemoveShapes.ToolTipText = "RemoveShapes"
        '
        'picCanvas
        '
        Me.picCanvas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picCanvas.Location = New System.Drawing.Point(0, 28)
        Me.picCanvas.Name = "picCanvas"
        Me.picCanvas.Size = New System.Drawing.Size(1190, 453)
        Me.picCanvas.TabIndex = 4
        Me.picCanvas.TabStop = False
        '
        'DrawingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1190, 481)
        Me.Controls.Add(Me.picCanvas)
        Me.Controls.Add(Me.ToolBar1)
        Me.Menu = Me.MainMenu1
        Me.Name = "DrawingForm"
        Me.Text = "DrawingForm"
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MainMenu1 As MainMenu
    Friend WithEvents MenuItem1 As MenuItem
    Friend WithEvents mnuFileNew As MenuItem
    Friend WithEvents mnuFileOpen As MenuItem
    Friend WithEvents mnuFileSave As MenuItem
    Friend WithEvents mnuFileSaveImage As MenuItem
    Friend WithEvents MenuItem2 As MenuItem
    Friend WithEvents mnuFilePrintPreview As MenuItem
    Friend WithEvents mnuFilePrint As MenuItem
    Friend WithEvents MenuItem6 As MenuItem
    Friend WithEvents mnuFileExit As MenuItem
    Friend WithEvents mnuEdit As MenuItem
    Friend WithEvents mnuEditDelete As MenuItem
    Friend WithEvents mnuFormat As MenuItem
    Friend WithEvents mnuFormatOrder As MenuItem
    Friend WithEvents mnuFormatOrderBringToFront As MenuItem
    Friend WithEvents mnuFormatOrderSendToBack As MenuItem
    Friend WithEvents MenuItem5 As MenuItem
    Friend WithEvents mnuOptSetBackColor As MenuItem
    Friend WithEvents mnuFont As MenuItem
    Friend WithEvents mnuBackground As MenuItem
    Friend WithEvents mnuLoadBackground As MenuItem
    Friend WithEvents mnuClearBackground As MenuItem
    Friend WithEvents mnuBackgroundLayout As MenuItem
    Friend WithEvents mnuLayoutNone As MenuItem
    Friend WithEvents mnuLayoutTile As MenuItem
    Friend WithEvents mnuLayoutCenter As MenuItem
    Friend WithEvents mnuLayoutStretch As MenuItem
    Friend WithEvents mnuLayoutZoom As MenuItem
    Friend WithEvents mnuTransformation As MenuItem
    Friend WithEvents mnuResetTransform As MenuItem
    Friend WithEvents mnuFlipHorizontal As MenuItem
    Friend WithEvents mnuFlipVertical As MenuItem
    Friend WithEvents ctxLineThickness As ContextMenu
    Friend WithEvents mnuThick1 As MenuItem
    Friend WithEvents mnuThick2 As MenuItem
    Friend WithEvents mnuThick3 As MenuItem
    Friend WithEvents mnuThick4 As MenuItem
    Friend WithEvents mnuThick5 As MenuItem
    Friend WithEvents ctxLineColor As ContextMenu
    Friend WithEvents mnuLineColorRed As MenuItem
    Friend WithEvents mnuLineColorGreen As MenuItem
    Friend WithEvents mnuLineColorBlue As MenuItem
    Friend WithEvents mnuLineColorYellow As MenuItem
    Friend WithEvents mnuLineColorOrange As MenuItem
    Friend WithEvents mnuLineColorPurple As MenuItem
    Friend WithEvents mnuLineColorBlack As MenuItem
    Friend WithEvents mnuLineColorWhite As MenuItem
    Friend WithEvents ctxFillColor As ContextMenu
    Friend WithEvents mnuFillColorRed As MenuItem
    Friend WithEvents mnuFillColorGreen As MenuItem
    Friend WithEvents mnuFillColorBlue As MenuItem
    Friend WithEvents mnuFillColorYellow As MenuItem
    Friend WithEvents mnuFillColorOrange As MenuItem
    Friend WithEvents mnuFillColorPurple As MenuItem
    Friend WithEvents mnuFillColorBlack As MenuItem
    Friend WithEvents mnuFillColorWhite As MenuItem
    Friend WithEvents imlToolbarButtons As ImageList
    Friend WithEvents dlgSavePicture As SaveFileDialog
    Friend WithEvents dlgOpenPicture As OpenFileDialog
    Friend WithEvents dlgBackColor As ColorDialog
    Friend WithEvents pdPrint As Printing.PrintDocument
    Friend WithEvents ppdPrint As PrintPreviewDialog
    Friend WithEvents ofdImage As OpenFileDialog
    Friend WithEvents sfdImage As SaveFileDialog
    Friend WithEvents ToolBar1 As ToolBar
    Friend WithEvents tbtnPointer As ToolBarButton
    Friend WithEvents tbtnLine As ToolBarButton
    Friend WithEvents tbtnRectangle As ToolBarButton
    Friend WithEvents tbtnEllipse As ToolBarButton
    Friend WithEvents tbtnStar As ToolBarButton
    Friend WithEvents tbtnImage As ToolBarButton
    Friend WithEvents btnFreehand As ToolBarButton
    Friend WithEvents btnText As ToolBarButton
    Friend WithEvents tbarSep1 As ToolBarButton
    Friend WithEvents tcboThickness As ToolBarButton
    Friend WithEvents tcboLineColor As ToolBarButton
    Friend WithEvents tcboFillColor As ToolBarButton
    Friend WithEvents tbarSep2 As ToolBarButton
    Friend WithEvents tbtnBringToFront As ToolBarButton
    Friend WithEvents tbtnSendToBack As ToolBarButton
    Friend WithEvents tbtnDelete As ToolBarButton
    Friend WithEvents picCanvas As PictureBox
    Friend WithEvents btnCropBackground As ToolBarButton
    Friend WithEvents btnKeepShapes As ToolBarButton
    Friend WithEvents btnRemoveShapes As ToolBarButton
    Friend WithEvents tbtnPolyLine As ToolBarButton
    Friend WithEvents tbtnArrow As ToolBarButton
End Class
