Partial Public Class DrawForm
    Inherits DevExpress.XtraEditors.XtraForm

    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DrawForm))
        Me.barManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.bar1 = New DevExpress.XtraBars.Bar()
        Me.tbtnPointer = New DevExpress.XtraBars.BarButtonItem()
        Me.tbtnLine = New DevExpress.XtraBars.BarButtonItem()
        Me.tbtnRectangle = New DevExpress.XtraBars.BarButtonItem()
        Me.tbtnEllipse = New DevExpress.XtraBars.BarButtonItem()
        Me.tbtnStar = New DevExpress.XtraBars.BarButtonItem()
        Me.tbtnImage = New DevExpress.XtraBars.BarButtonItem()
        Me.tbtnFreehand = New DevExpress.XtraBars.BarButtonItem()
        Me.tbtnText = New DevExpress.XtraBars.BarButtonItem()
        Me.tbtnArrow = New DevExpress.XtraBars.BarButtonItem()
        Me.tcboThickness = New DevExpress.XtraBars.BarButtonItem()
        Me.popThickness = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.lstThickness = New DevExpress.XtraEditors.ListBoxControl()
        Me.tcboLineColor = New DevExpress.XtraBars.BarButtonItem()
        Me.popForeColor = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.lstForeColor = New DevExpress.XtraEditors.ListBoxControl()
        Me.tcboFillColor = New DevExpress.XtraBars.BarButtonItem()
        Me.popFillColor = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.lstFillColor = New DevExpress.XtraEditors.ListBoxControl()
        Me.btnBringToFront = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSendToBack = New DevExpress.XtraBars.BarButtonItem()
        Me.btnDelete = New DevExpress.XtraBars.BarButtonItem()
        Me.btnRotLeft = New DevExpress.XtraBars.BarButtonItem()
        Me.btnRotRight = New DevExpress.XtraBars.BarButtonItem()
        Me.btnCrop = New DevExpress.XtraBars.BarButtonItem()
        Me.txtInside = New DevExpress.XtraBars.BarStaticItem()
        Me.txtText = New DevExpress.XtraBars.BarEditItem()
        Me.txtTextEdit = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.btnStarCorners = New DevExpress.XtraBars.BarButtonItem()
        Me.popStarCorners = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.lstStarCorners = New DevExpress.XtraEditors.ListBoxControl()
        Me.btnArrowProps = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.bar2 = New DevExpress.XtraBars.Bar()
        Me.mnuFile = New DevExpress.XtraBars.BarSubItem()
        Me.mnuFileNew = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFileOpen = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFileOpenDicomFile = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFileOpenDicomFolder = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFileSave = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFileSaveImage = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFilePrintPreview = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFilePrint = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFileExit = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEdit = New DevExpress.XtraBars.BarSubItem()
        Me.mnuEditDelete = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFormat = New DevExpress.XtraBars.BarSubItem()
        Me.mnuFormatOrder = New DevExpress.XtraBars.BarSubItem()
        Me.mnuFormatOrderBringToFront = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFormatOrderSendToBack = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuOptSet = New DevExpress.XtraBars.BarSubItem()
        Me.mnuOptSetBackColor = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFont = New DevExpress.XtraBars.BarSubItem()
        Me.mnuBackground = New DevExpress.XtraBars.BarSubItem()
        Me.mnuLoadBackground = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuClearBackground = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuBackgroundLayout = New DevExpress.XtraBars.BarSubItem()
        Me.mnuLayoutNone = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLayoutTile = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLayoutStrech = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLayoutCenter = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuLayoutZoom = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTransformation = New DevExpress.XtraBars.BarSubItem()
        Me.mnuResetTransform = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFlipHorizontal = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFlipVertical = New DevExpress.XtraBars.BarButtonItem()
        Me.bar3 = New DevExpress.XtraBars.Bar()
        Me.btnZoom = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemZoomTrackBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.FontSelector = New DevExpress.XtraEditors.Repository.RepositoryItemFontEdit()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.picCanvas = New System.Windows.Forms.PictureBox()
        Me.dlgBackColor = New System.Windows.Forms.ColorDialog()
        Me.pdPrint = New System.Drawing.Printing.PrintDocument()
        Me.ppdPrint = New System.Windows.Forms.PrintPreviewDialog()
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog()
        Me.sfdImage = New System.Windows.Forms.SaveFileDialog()
        Me.dlgOpenPicture = New System.Windows.Forms.OpenFileDialog()
        Me.dlgSavePicture = New System.Windows.Forms.SaveFileDialog()
        Me.tmrAutoSave = New System.Windows.Forms.Timer(Me.components)
        Me.pnlCanvasHost = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.barManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.popThickness, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.popThickness.SuspendLayout()
        CType(Me.lstThickness, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.popForeColor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.popForeColor.SuspendLayout()
        CType(Me.lstForeColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.popFillColor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.popFillColor.SuspendLayout()
        CType(Me.lstFillColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTextEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.popStarCorners, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.popStarCorners.SuspendLayout()
        CType(Me.lstStarCorners, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FontSelector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCanvasHost.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'barManager1
        '
        Me.barManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.bar1, Me.bar2, Me.bar3})
        Me.barManager1.DockControls.Add(Me.barDockControlTop)
        Me.barManager1.DockControls.Add(Me.barDockControlBottom)
        Me.barManager1.DockControls.Add(Me.barDockControlLeft)
        Me.barManager1.DockControls.Add(Me.barDockControlRight)
        Me.barManager1.Form = Me
        Me.barManager1.Images = Me.ImageCollection1
        Me.barManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mnuFile, Me.mnuFileNew, Me.mnuFileOpen, Me.mnuFileOpenDicomFile, Me.mnuFileOpenDicomFolder, Me.mnuFileSave, Me.mnuFileSaveImage, Me.mnuFilePrintPreview, Me.mnuFilePrint, Me.mnuFileExit, Me.mnuEdit, Me.mnuEditDelete, Me.mnuFormat, Me.mnuFormatOrder, Me.mnuFormatOrderBringToFront, Me.mnuFormatOrderSendToBack, Me.mnuOptSet, Me.mnuOptSetBackColor, Me.mnuFont, Me.mnuBackground, Me.mnuLoadBackground, Me.mnuClearBackground, Me.mnuBackgroundLayout, Me.mnuLayoutNone, Me.mnuLayoutTile, Me.mnuLayoutStrech, Me.mnuLayoutCenter, Me.mnuLayoutZoom, Me.mnuTransformation, Me.mnuResetTransform, Me.mnuFlipHorizontal, Me.mnuFlipVertical, Me.tbtnLine, Me.tbtnRectangle, Me.tbtnEllipse, Me.tbtnStar, Me.tbtnImage, Me.tbtnFreehand, Me.tbtnText, Me.tcboThickness, Me.tcboLineColor, Me.tcboFillColor, Me.btnBringToFront, Me.btnSendToBack, Me.btnDelete, Me.tbtnPointer, Me.txtInside, Me.tbtnArrow, Me.txtText, Me.btnSaveClose, Me.btnCrop, Me.btnStarCorners, Me.btnArrowProps, Me.btnRotLeft, Me.btnRotRight, Me.btnZoom})
        Me.barManager1.LargeImages = Me.ImageCollection1
        Me.barManager1.MainMenu = Me.bar2
        Me.barManager1.MaxItemId = 61
        Me.barManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.FontSelector, Me.txtTextEdit, Me.RepositoryItemTextEdit1, Me.RepositoryItemZoomTrackBar1})
        Me.barManager1.StatusBar = Me.bar3
        '
        'bar1
        '
        resources.ApplyResources(Me.bar1, "bar1")
        Me.bar1.BarName = "Tools"
        Me.bar1.DockCol = 0
        Me.bar1.DockRow = 1
        Me.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tbtnPointer), New DevExpress.XtraBars.LinkPersistInfo(Me.tbtnLine, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tbtnRectangle, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tbtnEllipse, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tbtnStar, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tbtnImage, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tbtnFreehand, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tbtnText, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tbtnArrow, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tcboThickness, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tcboLineColor, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tcboFillColor, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btnBringToFront, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSendToBack), New DevExpress.XtraBars.LinkPersistInfo(Me.btnDelete, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btnRotLeft, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btnRotRight), New DevExpress.XtraBars.LinkPersistInfo(Me.btnCrop), New DevExpress.XtraBars.LinkPersistInfo(Me.txtInside), New DevExpress.XtraBars.LinkPersistInfo(Me.txtText), New DevExpress.XtraBars.LinkPersistInfo(Me.btnStarCorners, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btnArrowProps, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose, True)})
        '
        'tbtnPointer
        '
        resources.ApplyResources(Me.tbtnPointer, "tbtnPointer")
        Me.tbtnPointer.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.tbtnPointer.Down = True
        Me.tbtnPointer.Id = 44
        Me.tbtnPointer.ImageOptions.ImageIndex = CType(resources.GetObject("tbtnPointer.ImageOptions.ImageIndex"), Integer)
        Me.tbtnPointer.ImageOptions.ImageKey = resources.GetString("tbtnPointer.ImageOptions.ImageKey")
        Me.tbtnPointer.ImageOptions.LargeImageIndex = CType(resources.GetObject("tbtnPointer.ImageOptions.LargeImageIndex"), Integer)
        Me.tbtnPointer.ImageOptions.LargeImageKey = resources.GetString("tbtnPointer.ImageOptions.LargeImageKey")
        Me.tbtnPointer.ImageOptions.SvgImage = CType(resources.GetObject("tbtnPointer.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tbtnPointer.Name = "tbtnPointer"
        '
        'tbtnLine
        '
        resources.ApplyResources(Me.tbtnLine, "tbtnLine")
        Me.tbtnLine.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.tbtnLine.Id = 31
        Me.tbtnLine.ImageOptions.ImageIndex = CType(resources.GetObject("tbtnLine.ImageOptions.ImageIndex"), Integer)
        Me.tbtnLine.ImageOptions.ImageKey = resources.GetString("tbtnLine.ImageOptions.ImageKey")
        Me.tbtnLine.ImageOptions.LargeImageIndex = CType(resources.GetObject("tbtnLine.ImageOptions.LargeImageIndex"), Integer)
        Me.tbtnLine.ImageOptions.LargeImageKey = resources.GetString("tbtnLine.ImageOptions.LargeImageKey")
        Me.tbtnLine.ImageOptions.SvgImage = CType(resources.GetObject("tbtnLine.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tbtnLine.Name = "tbtnLine"
        '
        'tbtnRectangle
        '
        resources.ApplyResources(Me.tbtnRectangle, "tbtnRectangle")
        Me.tbtnRectangle.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.tbtnRectangle.Id = 32
        Me.tbtnRectangle.ImageOptions.ImageIndex = CType(resources.GetObject("tbtnRectangle.ImageOptions.ImageIndex"), Integer)
        Me.tbtnRectangle.ImageOptions.ImageKey = resources.GetString("tbtnRectangle.ImageOptions.ImageKey")
        Me.tbtnRectangle.ImageOptions.LargeImageIndex = CType(resources.GetObject("tbtnRectangle.ImageOptions.LargeImageIndex"), Integer)
        Me.tbtnRectangle.ImageOptions.LargeImageKey = resources.GetString("tbtnRectangle.ImageOptions.LargeImageKey")
        Me.tbtnRectangle.ImageOptions.SvgImage = CType(resources.GetObject("tbtnRectangle.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tbtnRectangle.Name = "tbtnRectangle"
        '
        'tbtnEllipse
        '
        resources.ApplyResources(Me.tbtnEllipse, "tbtnEllipse")
        Me.tbtnEllipse.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.tbtnEllipse.Id = 33
        Me.tbtnEllipse.ImageOptions.ImageIndex = CType(resources.GetObject("tbtnEllipse.ImageOptions.ImageIndex"), Integer)
        Me.tbtnEllipse.ImageOptions.ImageKey = resources.GetString("tbtnEllipse.ImageOptions.ImageKey")
        Me.tbtnEllipse.ImageOptions.LargeImageIndex = CType(resources.GetObject("tbtnEllipse.ImageOptions.LargeImageIndex"), Integer)
        Me.tbtnEllipse.ImageOptions.LargeImageKey = resources.GetString("tbtnEllipse.ImageOptions.LargeImageKey")
        Me.tbtnEllipse.ImageOptions.SvgImage = CType(resources.GetObject("tbtnEllipse.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tbtnEllipse.Name = "tbtnEllipse"
        '
        'tbtnStar
        '
        resources.ApplyResources(Me.tbtnStar, "tbtnStar")
        Me.tbtnStar.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.tbtnStar.Id = 34
        Me.tbtnStar.ImageOptions.ImageIndex = CType(resources.GetObject("tbtnStar.ImageOptions.ImageIndex"), Integer)
        Me.tbtnStar.ImageOptions.ImageKey = resources.GetString("tbtnStar.ImageOptions.ImageKey")
        Me.tbtnStar.ImageOptions.LargeImageIndex = CType(resources.GetObject("tbtnStar.ImageOptions.LargeImageIndex"), Integer)
        Me.tbtnStar.ImageOptions.LargeImageKey = resources.GetString("tbtnStar.ImageOptions.LargeImageKey")
        Me.tbtnStar.ImageOptions.SvgImage = CType(resources.GetObject("tbtnStar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tbtnStar.Name = "tbtnStar"
        '
        'tbtnImage
        '
        resources.ApplyResources(Me.tbtnImage, "tbtnImage")
        Me.tbtnImage.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.tbtnImage.Id = 35
        Me.tbtnImage.ImageOptions.ImageIndex = CType(resources.GetObject("tbtnImage.ImageOptions.ImageIndex"), Integer)
        Me.tbtnImage.ImageOptions.ImageKey = resources.GetString("tbtnImage.ImageOptions.ImageKey")
        Me.tbtnImage.ImageOptions.LargeImageIndex = CType(resources.GetObject("tbtnImage.ImageOptions.LargeImageIndex"), Integer)
        Me.tbtnImage.ImageOptions.LargeImageKey = resources.GetString("tbtnImage.ImageOptions.LargeImageKey")
        Me.tbtnImage.ImageOptions.SvgImage = CType(resources.GetObject("tbtnImage.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tbtnImage.Name = "tbtnImage"
        '
        'tbtnFreehand
        '
        resources.ApplyResources(Me.tbtnFreehand, "tbtnFreehand")
        Me.tbtnFreehand.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.tbtnFreehand.Id = 36
        Me.tbtnFreehand.ImageOptions.Image = Global.DentistX.My.Resources.Resources.tbtnFreeHand16
        Me.tbtnFreehand.ImageOptions.ImageIndex = CType(resources.GetObject("tbtnFreehand.ImageOptions.ImageIndex"), Integer)
        Me.tbtnFreehand.ImageOptions.ImageKey = resources.GetString("tbtnFreehand.ImageOptions.ImageKey")
        Me.tbtnFreehand.ImageOptions.LargeImageIndex = CType(resources.GetObject("tbtnFreehand.ImageOptions.LargeImageIndex"), Integer)
        Me.tbtnFreehand.ImageOptions.LargeImageKey = resources.GetString("tbtnFreehand.ImageOptions.LargeImageKey")
        Me.tbtnFreehand.ImageOptions.SvgImage = CType(resources.GetObject("tbtnFreehand.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tbtnFreehand.Name = "tbtnFreehand"
        '
        'tbtnText
        '
        resources.ApplyResources(Me.tbtnText, "tbtnText")
        Me.tbtnText.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.tbtnText.Id = 37
        Me.tbtnText.ImageOptions.Image = Global.DentistX.My.Resources.Resources.tbtnText16
        Me.tbtnText.ImageOptions.ImageIndex = CType(resources.GetObject("tbtnText.ImageOptions.ImageIndex"), Integer)
        Me.tbtnText.ImageOptions.ImageKey = resources.GetString("tbtnText.ImageOptions.ImageKey")
        Me.tbtnText.ImageOptions.LargeImageIndex = CType(resources.GetObject("tbtnText.ImageOptions.LargeImageIndex"), Integer)
        Me.tbtnText.ImageOptions.LargeImageKey = resources.GetString("tbtnText.ImageOptions.LargeImageKey")
        Me.tbtnText.ImageOptions.SvgImage = CType(resources.GetObject("tbtnText.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tbtnText.Name = "tbtnText"
        '
        'tbtnArrow
        '
        resources.ApplyResources(Me.tbtnArrow, "tbtnArrow")
        Me.tbtnArrow.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.tbtnArrow.Id = 48
        Me.tbtnArrow.ImageOptions.Image = Global.DentistX.My.Resources.Resources.tbtnArrowRight16
        Me.tbtnArrow.ImageOptions.ImageIndex = CType(resources.GetObject("tbtnArrow.ImageOptions.ImageIndex"), Integer)
        Me.tbtnArrow.ImageOptions.ImageKey = resources.GetString("tbtnArrow.ImageOptions.ImageKey")
        Me.tbtnArrow.ImageOptions.LargeImageIndex = CType(resources.GetObject("tbtnArrow.ImageOptions.LargeImageIndex"), Integer)
        Me.tbtnArrow.ImageOptions.LargeImageKey = resources.GetString("tbtnArrow.ImageOptions.LargeImageKey")
        Me.tbtnArrow.ImageOptions.SvgImage = CType(resources.GetObject("tbtnArrow.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tbtnArrow.Name = "tbtnArrow"
        '
        'tcboThickness
        '
        resources.ApplyResources(Me.tcboThickness, "tcboThickness")
        Me.tcboThickness.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.tcboThickness.DropDownControl = Me.popThickness
        Me.tcboThickness.Id = 38
        Me.tcboThickness.ImageOptions.ImageIndex = CType(resources.GetObject("tcboThickness.ImageOptions.ImageIndex"), Integer)
        Me.tcboThickness.ImageOptions.ImageKey = resources.GetString("tcboThickness.ImageOptions.ImageKey")
        Me.tcboThickness.ImageOptions.LargeImageIndex = CType(resources.GetObject("tcboThickness.ImageOptions.LargeImageIndex"), Integer)
        Me.tcboThickness.ImageOptions.LargeImageKey = resources.GetString("tcboThickness.ImageOptions.LargeImageKey")
        Me.tcboThickness.ImageOptions.SvgImage = CType(resources.GetObject("tcboThickness.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tcboThickness.Name = "tcboThickness"
        '
        'popThickness
        '
        resources.ApplyResources(Me.popThickness, "popThickness")
        Me.popThickness.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.popThickness.Controls.Add(Me.lstThickness)
        Me.popThickness.Manager = Me.barManager1
        Me.popThickness.Name = "popThickness"
        '
        'lstThickness
        '
        resources.ApplyResources(Me.lstThickness, "lstThickness")
        Me.lstThickness.Name = "lstThickness"
        '
        'tcboLineColor
        '
        resources.ApplyResources(Me.tcboLineColor, "tcboLineColor")
        Me.tcboLineColor.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.tcboLineColor.DropDownControl = Me.popForeColor
        Me.tcboLineColor.Id = 39
        Me.tcboLineColor.ImageOptions.ImageIndex = CType(resources.GetObject("tcboLineColor.ImageOptions.ImageIndex"), Integer)
        Me.tcboLineColor.ImageOptions.ImageKey = resources.GetString("tcboLineColor.ImageOptions.ImageKey")
        Me.tcboLineColor.ImageOptions.LargeImageIndex = CType(resources.GetObject("tcboLineColor.ImageOptions.LargeImageIndex"), Integer)
        Me.tcboLineColor.ImageOptions.LargeImageKey = resources.GetString("tcboLineColor.ImageOptions.LargeImageKey")
        Me.tcboLineColor.ImageOptions.SvgImage = CType(resources.GetObject("tcboLineColor.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tcboLineColor.Name = "tcboLineColor"
        '
        'popForeColor
        '
        resources.ApplyResources(Me.popForeColor, "popForeColor")
        Me.popForeColor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.popForeColor.Controls.Add(Me.lstForeColor)
        Me.popForeColor.Name = "popForeColor"
        '
        'lstForeColor
        '
        resources.ApplyResources(Me.lstForeColor, "lstForeColor")
        Me.lstForeColor.Name = "lstForeColor"
        '
        'tcboFillColor
        '
        resources.ApplyResources(Me.tcboFillColor, "tcboFillColor")
        Me.tcboFillColor.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.tcboFillColor.DropDownControl = Me.popFillColor
        Me.tcboFillColor.Id = 40
        Me.tcboFillColor.ImageOptions.ImageIndex = CType(resources.GetObject("tcboFillColor.ImageOptions.ImageIndex"), Integer)
        Me.tcboFillColor.ImageOptions.ImageKey = resources.GetString("tcboFillColor.ImageOptions.ImageKey")
        Me.tcboFillColor.ImageOptions.LargeImageIndex = CType(resources.GetObject("tcboFillColor.ImageOptions.LargeImageIndex"), Integer)
        Me.tcboFillColor.ImageOptions.LargeImageKey = resources.GetString("tcboFillColor.ImageOptions.LargeImageKey")
        Me.tcboFillColor.ImageOptions.SvgImage = CType(resources.GetObject("tcboFillColor.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.tcboFillColor.Name = "tcboFillColor"
        '
        'popFillColor
        '
        resources.ApplyResources(Me.popFillColor, "popFillColor")
        Me.popFillColor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.popFillColor.Controls.Add(Me.lstFillColor)
        Me.popFillColor.Name = "popFillColor"
        '
        'lstFillColor
        '
        resources.ApplyResources(Me.lstFillColor, "lstFillColor")
        Me.lstFillColor.Name = "lstFillColor"
        '
        'btnBringToFront
        '
        resources.ApplyResources(Me.btnBringToFront, "btnBringToFront")
        Me.btnBringToFront.Id = 41
        Me.btnBringToFront.ImageOptions.ImageIndex = CType(resources.GetObject("btnBringToFront.ImageOptions.ImageIndex"), Integer)
        Me.btnBringToFront.ImageOptions.ImageKey = resources.GetString("btnBringToFront.ImageOptions.ImageKey")
        Me.btnBringToFront.ImageOptions.LargeImageIndex = CType(resources.GetObject("btnBringToFront.ImageOptions.LargeImageIndex"), Integer)
        Me.btnBringToFront.ImageOptions.LargeImageKey = resources.GetString("btnBringToFront.ImageOptions.LargeImageKey")
        Me.btnBringToFront.ImageOptions.SvgImage = CType(resources.GetObject("btnBringToFront.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnBringToFront.Name = "btnBringToFront"
        '
        'btnSendToBack
        '
        resources.ApplyResources(Me.btnSendToBack, "btnSendToBack")
        Me.btnSendToBack.Id = 42
        Me.btnSendToBack.ImageOptions.ImageIndex = CType(resources.GetObject("btnSendToBack.ImageOptions.ImageIndex"), Integer)
        Me.btnSendToBack.ImageOptions.ImageKey = resources.GetString("btnSendToBack.ImageOptions.ImageKey")
        Me.btnSendToBack.ImageOptions.LargeImageIndex = CType(resources.GetObject("btnSendToBack.ImageOptions.LargeImageIndex"), Integer)
        Me.btnSendToBack.ImageOptions.LargeImageKey = resources.GetString("btnSendToBack.ImageOptions.LargeImageKey")
        Me.btnSendToBack.ImageOptions.SvgImage = CType(resources.GetObject("btnSendToBack.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnSendToBack.Name = "btnSendToBack"
        '
        'btnDelete
        '
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.btnDelete.Id = 43
        Me.btnDelete.ImageOptions.ImageIndex = CType(resources.GetObject("btnDelete.ImageOptions.ImageIndex"), Integer)
        Me.btnDelete.ImageOptions.ImageKey = resources.GetString("btnDelete.ImageOptions.ImageKey")
        Me.btnDelete.ImageOptions.LargeImageIndex = CType(resources.GetObject("btnDelete.ImageOptions.LargeImageIndex"), Integer)
        Me.btnDelete.ImageOptions.LargeImageKey = resources.GetString("btnDelete.ImageOptions.LargeImageKey")
        Me.btnDelete.ImageOptions.SvgImage = CType(resources.GetObject("btnDelete.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnDelete.Name = "btnDelete"
        '
        'btnRotLeft
        '
        resources.ApplyResources(Me.btnRotLeft, "btnRotLeft")
        Me.btnRotLeft.Id = 57
        Me.btnRotLeft.ImageOptions.Image = Global.DentistX.My.Resources.Resources.tbtnRotate_ccw16
        Me.btnRotLeft.ImageOptions.ImageIndex = CType(resources.GetObject("btnRotLeft.ImageOptions.ImageIndex"), Integer)
        Me.btnRotLeft.ImageOptions.ImageKey = resources.GetString("btnRotLeft.ImageOptions.ImageKey")
        Me.btnRotLeft.ImageOptions.LargeImageIndex = CType(resources.GetObject("btnRotLeft.ImageOptions.LargeImageIndex"), Integer)
        Me.btnRotLeft.ImageOptions.LargeImageKey = resources.GetString("btnRotLeft.ImageOptions.LargeImageKey")
        Me.btnRotLeft.ImageOptions.SvgImage = CType(resources.GetObject("btnRotLeft.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnRotLeft.Name = "btnRotLeft"
        '
        'btnRotRight
        '
        resources.ApplyResources(Me.btnRotRight, "btnRotRight")
        Me.btnRotRight.Id = 58
        Me.btnRotRight.ImageOptions.Image = Global.DentistX.My.Resources.Resources.tbtnRotate_cw16
        Me.btnRotRight.ImageOptions.ImageIndex = CType(resources.GetObject("btnRotRight.ImageOptions.ImageIndex"), Integer)
        Me.btnRotRight.ImageOptions.ImageKey = resources.GetString("btnRotRight.ImageOptions.ImageKey")
        Me.btnRotRight.ImageOptions.LargeImageIndex = CType(resources.GetObject("btnRotRight.ImageOptions.LargeImageIndex"), Integer)
        Me.btnRotRight.ImageOptions.LargeImageKey = resources.GetString("btnRotRight.ImageOptions.LargeImageKey")
        Me.btnRotRight.ImageOptions.SvgImage = CType(resources.GetObject("btnRotRight.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnRotRight.Name = "btnRotRight"
        '
        'btnCrop
        '
        resources.ApplyResources(Me.btnCrop, "btnCrop")
        Me.btnCrop.Id = 52
        Me.btnCrop.ImageOptions.ImageIndex = CType(resources.GetObject("btnCrop.ImageOptions.ImageIndex"), Integer)
        Me.btnCrop.ImageOptions.ImageKey = resources.GetString("btnCrop.ImageOptions.ImageKey")
        Me.btnCrop.ImageOptions.LargeImageIndex = CType(resources.GetObject("btnCrop.ImageOptions.LargeImageIndex"), Integer)
        Me.btnCrop.ImageOptions.LargeImageKey = resources.GetString("btnCrop.ImageOptions.LargeImageKey")
        Me.btnCrop.ImageOptions.SvgImage = CType(resources.GetObject("btnCrop.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnCrop.Name = "btnCrop"
        '
        'txtInside
        '
        resources.ApplyResources(Me.txtInside, "txtInside")
        Me.txtInside.Id = 45
        Me.txtInside.ImageOptions.ImageIndex = CType(resources.GetObject("txtInside.ImageOptions.ImageIndex"), Integer)
        Me.txtInside.ImageOptions.ImageKey = resources.GetString("txtInside.ImageOptions.ImageKey")
        Me.txtInside.ImageOptions.LargeImageIndex = CType(resources.GetObject("txtInside.ImageOptions.LargeImageIndex"), Integer)
        Me.txtInside.ImageOptions.LargeImageKey = resources.GetString("txtInside.ImageOptions.LargeImageKey")
        Me.txtInside.ImageOptions.SvgImage = CType(resources.GetObject("txtInside.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.txtInside.Name = "txtInside"
        '
        'txtText
        '
        resources.ApplyResources(Me.txtText, "txtText")
        Me.txtText.Edit = Me.txtTextEdit
        Me.txtText.Id = 50
        Me.txtText.ImageOptions.ImageIndex = CType(resources.GetObject("txtText.ImageOptions.ImageIndex"), Integer)
        Me.txtText.ImageOptions.ImageKey = resources.GetString("txtText.ImageOptions.ImageKey")
        Me.txtText.ImageOptions.LargeImageIndex = CType(resources.GetObject("txtText.ImageOptions.LargeImageIndex"), Integer)
        Me.txtText.ImageOptions.LargeImageKey = resources.GetString("txtText.ImageOptions.LargeImageKey")
        Me.txtText.ImageOptions.SvgImage = CType(resources.GetObject("txtText.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.txtText.Name = "txtText"
        '
        'txtTextEdit
        '
        resources.ApplyResources(Me.txtTextEdit, "txtTextEdit")
        Me.txtTextEdit.Name = "txtTextEdit"
        '
        'btnStarCorners
        '
        resources.ApplyResources(Me.btnStarCorners, "btnStarCorners")
        Me.btnStarCorners.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.btnStarCorners.DropDownControl = Me.popStarCorners
        Me.btnStarCorners.Id = 53
        Me.btnStarCorners.ImageOptions.ImageIndex = CType(resources.GetObject("btnStarCorners.ImageOptions.ImageIndex"), Integer)
        Me.btnStarCorners.ImageOptions.ImageKey = resources.GetString("btnStarCorners.ImageOptions.ImageKey")
        Me.btnStarCorners.ImageOptions.LargeImageIndex = CType(resources.GetObject("btnStarCorners.ImageOptions.LargeImageIndex"), Integer)
        Me.btnStarCorners.ImageOptions.LargeImageKey = resources.GetString("btnStarCorners.ImageOptions.LargeImageKey")
        Me.btnStarCorners.ImageOptions.SvgImage = CType(resources.GetObject("btnStarCorners.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnStarCorners.Name = "btnStarCorners"
        '
        'popStarCorners
        '
        resources.ApplyResources(Me.popStarCorners, "popStarCorners")
        Me.popStarCorners.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.popStarCorners.Controls.Add(Me.lstStarCorners)
        Me.popStarCorners.Name = "popStarCorners"
        '
        'lstStarCorners
        '
        resources.ApplyResources(Me.lstStarCorners, "lstStarCorners")
        Me.lstStarCorners.Items.AddRange(New Object() {resources.GetString("lstStarCorners.Items"), resources.GetString("lstStarCorners.Items1"), resources.GetString("lstStarCorners.Items2"), resources.GetString("lstStarCorners.Items3"), resources.GetString("lstStarCorners.Items4"), resources.GetString("lstStarCorners.Items5"), resources.GetString("lstStarCorners.Items6"), resources.GetString("lstStarCorners.Items7")})
        Me.lstStarCorners.Name = "lstStarCorners"
        '
        'btnArrowProps
        '
        resources.ApplyResources(Me.btnArrowProps, "btnArrowProps")
        Me.btnArrowProps.Id = 54
        Me.btnArrowProps.ImageOptions.ImageIndex = CType(resources.GetObject("btnArrowProps.ImageOptions.ImageIndex"), Integer)
        Me.btnArrowProps.ImageOptions.ImageKey = resources.GetString("btnArrowProps.ImageOptions.ImageKey")
        Me.btnArrowProps.ImageOptions.LargeImageIndex = CType(resources.GetObject("btnArrowProps.ImageOptions.LargeImageIndex"), Integer)
        Me.btnArrowProps.ImageOptions.LargeImageKey = resources.GetString("btnArrowProps.ImageOptions.LargeImageKey")
        Me.btnArrowProps.ImageOptions.SvgImage = CType(resources.GetObject("btnArrowProps.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnArrowProps.Name = "btnArrowProps"
        '
        'btnSaveClose
        '
        resources.ApplyResources(Me.btnSaveClose, "btnSaveClose")
        Me.btnSaveClose.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.btnSaveClose.Id = 51
        Me.btnSaveClose.ImageOptions.ImageIndex = CType(resources.GetObject("btnSaveClose.ImageOptions.ImageIndex"), Integer)
        Me.btnSaveClose.ImageOptions.ImageKey = resources.GetString("btnSaveClose.ImageOptions.ImageKey")
        Me.btnSaveClose.ImageOptions.LargeImageIndex = CType(resources.GetObject("btnSaveClose.ImageOptions.LargeImageIndex"), Integer)
        Me.btnSaveClose.ImageOptions.LargeImageKey = resources.GetString("btnSaveClose.ImageOptions.LargeImageKey")
        Me.btnSaveClose.ImageOptions.SvgImage = CType(resources.GetObject("btnSaveClose.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnSaveClose.ItemAppearance.Normal.Font = CType(resources.GetObject("btnSaveClose.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.btnSaveClose.ItemAppearance.Normal.Options.UseFont = True
        Me.btnSaveClose.Name = "btnSaveClose"
        '
        'bar2
        '
        resources.ApplyResources(Me.bar2, "bar2")
        Me.bar2.BarName = "Main menu"
        Me.bar2.DockCol = 0
        Me.bar2.DockRow = 0
        Me.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFile), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEdit), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFormat), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuOptSet), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFont), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuBackground), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuTransformation)})
        Me.bar2.OptionsBar.MultiLine = True
        Me.bar2.OptionsBar.UseWholeRow = True
        '
        'mnuFile
        '
        resources.ApplyResources(Me.mnuFile, "mnuFile")
        Me.mnuFile.Id = 0
        Me.mnuFile.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFile.ImageOptions.ImageIndex"), Integer)
        Me.mnuFile.ImageOptions.ImageKey = resources.GetString("mnuFile.ImageOptions.ImageKey")
        Me.mnuFile.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFile.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFile.ImageOptions.LargeImageKey = resources.GetString("mnuFile.ImageOptions.LargeImageKey")
        Me.mnuFile.ImageOptions.SvgImage = CType(resources.GetObject("mnuFile.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFile.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFileNew), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFileOpen), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFileOpenDicomFile), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFileOpenDicomFolder), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFileSave), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFileSaveImage), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFilePrintPreview), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFilePrint), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFileExit)})
        Me.mnuFile.Name = "mnuFile"
        '
        'mnuFileNew
        '
        resources.ApplyResources(Me.mnuFileNew, "mnuFileNew")
        Me.mnuFileNew.Id = 1
        Me.mnuFileNew.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFileNew.ImageOptions.ImageIndex"), Integer)
        Me.mnuFileNew.ImageOptions.ImageKey = resources.GetString("mnuFileNew.ImageOptions.ImageKey")
        Me.mnuFileNew.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFileNew.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFileNew.ImageOptions.LargeImageKey = resources.GetString("mnuFileNew.ImageOptions.LargeImageKey")
        Me.mnuFileNew.ImageOptions.SvgImage = CType(resources.GetObject("mnuFileNew.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFileNew.Name = "mnuFileNew"
        '
        'mnuFileOpen
        '
        resources.ApplyResources(Me.mnuFileOpen, "mnuFileOpen")
        Me.mnuFileOpen.Id = 2
        Me.mnuFileOpen.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFileOpen.ImageOptions.ImageIndex"), Integer)
        Me.mnuFileOpen.ImageOptions.ImageKey = resources.GetString("mnuFileOpen.ImageOptions.ImageKey")
        Me.mnuFileOpen.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFileOpen.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFileOpen.ImageOptions.LargeImageKey = resources.GetString("mnuFileOpen.ImageOptions.LargeImageKey")
        Me.mnuFileOpen.ImageOptions.SvgImage = CType(resources.GetObject("mnuFileOpen.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFileOpen.Name = "mnuFileOpen"
        '
        'mnuFileOpenDicomFile
        '
        resources.ApplyResources(Me.mnuFileOpenDicomFile, "mnuFileOpenDicomFile")
        Me.mnuFileOpenDicomFile.Id = 55
        Me.mnuFileOpenDicomFile.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFileOpenDicomFile.ImageOptions.ImageIndex"), Integer)
        Me.mnuFileOpenDicomFile.ImageOptions.ImageKey = resources.GetString("mnuFileOpenDicomFile.ImageOptions.ImageKey")
        Me.mnuFileOpenDicomFile.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFileOpenDicomFile.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFileOpenDicomFile.ImageOptions.LargeImageKey = resources.GetString("mnuFileOpenDicomFile.ImageOptions.LargeImageKey")
        Me.mnuFileOpenDicomFile.ImageOptions.SvgImage = CType(resources.GetObject("mnuFileOpenDicomFile.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFileOpenDicomFile.Name = "mnuFileOpenDicomFile"
        '
        'mnuFileOpenDicomFolder
        '
        resources.ApplyResources(Me.mnuFileOpenDicomFolder, "mnuFileOpenDicomFolder")
        Me.mnuFileOpenDicomFolder.Id = 56
        Me.mnuFileOpenDicomFolder.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFileOpenDicomFolder.ImageOptions.ImageIndex"), Integer)
        Me.mnuFileOpenDicomFolder.ImageOptions.ImageKey = resources.GetString("mnuFileOpenDicomFolder.ImageOptions.ImageKey")
        Me.mnuFileOpenDicomFolder.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFileOpenDicomFolder.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFileOpenDicomFolder.ImageOptions.LargeImageKey = resources.GetString("mnuFileOpenDicomFolder.ImageOptions.LargeImageKey")
        Me.mnuFileOpenDicomFolder.ImageOptions.SvgImage = CType(resources.GetObject("mnuFileOpenDicomFolder.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFileOpenDicomFolder.Name = "mnuFileOpenDicomFolder"
        '
        'mnuFileSave
        '
        resources.ApplyResources(Me.mnuFileSave, "mnuFileSave")
        Me.mnuFileSave.Id = 3
        Me.mnuFileSave.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFileSave.ImageOptions.ImageIndex"), Integer)
        Me.mnuFileSave.ImageOptions.ImageKey = resources.GetString("mnuFileSave.ImageOptions.ImageKey")
        Me.mnuFileSave.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFileSave.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFileSave.ImageOptions.LargeImageKey = resources.GetString("mnuFileSave.ImageOptions.LargeImageKey")
        Me.mnuFileSave.ImageOptions.SvgImage = CType(resources.GetObject("mnuFileSave.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFileSave.Name = "mnuFileSave"
        '
        'mnuFileSaveImage
        '
        resources.ApplyResources(Me.mnuFileSaveImage, "mnuFileSaveImage")
        Me.mnuFileSaveImage.Id = 4
        Me.mnuFileSaveImage.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFileSaveImage.ImageOptions.ImageIndex"), Integer)
        Me.mnuFileSaveImage.ImageOptions.ImageKey = resources.GetString("mnuFileSaveImage.ImageOptions.ImageKey")
        Me.mnuFileSaveImage.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFileSaveImage.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFileSaveImage.ImageOptions.LargeImageKey = resources.GetString("mnuFileSaveImage.ImageOptions.LargeImageKey")
        Me.mnuFileSaveImage.ImageOptions.SvgImage = CType(resources.GetObject("mnuFileSaveImage.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFileSaveImage.Name = "mnuFileSaveImage"
        '
        'mnuFilePrintPreview
        '
        resources.ApplyResources(Me.mnuFilePrintPreview, "mnuFilePrintPreview")
        Me.mnuFilePrintPreview.Id = 5
        Me.mnuFilePrintPreview.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFilePrintPreview.ImageOptions.ImageIndex"), Integer)
        Me.mnuFilePrintPreview.ImageOptions.ImageKey = resources.GetString("mnuFilePrintPreview.ImageOptions.ImageKey")
        Me.mnuFilePrintPreview.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFilePrintPreview.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFilePrintPreview.ImageOptions.LargeImageKey = resources.GetString("mnuFilePrintPreview.ImageOptions.LargeImageKey")
        Me.mnuFilePrintPreview.ImageOptions.SvgImage = CType(resources.GetObject("mnuFilePrintPreview.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFilePrintPreview.Name = "mnuFilePrintPreview"
        '
        'mnuFilePrint
        '
        resources.ApplyResources(Me.mnuFilePrint, "mnuFilePrint")
        Me.mnuFilePrint.Id = 6
        Me.mnuFilePrint.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFilePrint.ImageOptions.ImageIndex"), Integer)
        Me.mnuFilePrint.ImageOptions.ImageKey = resources.GetString("mnuFilePrint.ImageOptions.ImageKey")
        Me.mnuFilePrint.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFilePrint.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFilePrint.ImageOptions.LargeImageKey = resources.GetString("mnuFilePrint.ImageOptions.LargeImageKey")
        Me.mnuFilePrint.ImageOptions.SvgImage = CType(resources.GetObject("mnuFilePrint.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFilePrint.Name = "mnuFilePrint"
        '
        'mnuFileExit
        '
        resources.ApplyResources(Me.mnuFileExit, "mnuFileExit")
        Me.mnuFileExit.Id = 7
        Me.mnuFileExit.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFileExit.ImageOptions.ImageIndex"), Integer)
        Me.mnuFileExit.ImageOptions.ImageKey = resources.GetString("mnuFileExit.ImageOptions.ImageKey")
        Me.mnuFileExit.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFileExit.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFileExit.ImageOptions.LargeImageKey = resources.GetString("mnuFileExit.ImageOptions.LargeImageKey")
        Me.mnuFileExit.ImageOptions.SvgImage = CType(resources.GetObject("mnuFileExit.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFileExit.Name = "mnuFileExit"
        '
        'mnuEdit
        '
        resources.ApplyResources(Me.mnuEdit, "mnuEdit")
        Me.mnuEdit.Id = 8
        Me.mnuEdit.ImageOptions.ImageIndex = CType(resources.GetObject("mnuEdit.ImageOptions.ImageIndex"), Integer)
        Me.mnuEdit.ImageOptions.ImageKey = resources.GetString("mnuEdit.ImageOptions.ImageKey")
        Me.mnuEdit.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuEdit.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuEdit.ImageOptions.LargeImageKey = resources.GetString("mnuEdit.ImageOptions.LargeImageKey")
        Me.mnuEdit.ImageOptions.SvgImage = CType(resources.GetObject("mnuEdit.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEdit.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEditDelete)})
        Me.mnuEdit.Name = "mnuEdit"
        '
        'mnuEditDelete
        '
        resources.ApplyResources(Me.mnuEditDelete, "mnuEditDelete")
        Me.mnuEditDelete.Id = 9
        Me.mnuEditDelete.ImageOptions.ImageIndex = CType(resources.GetObject("mnuEditDelete.ImageOptions.ImageIndex"), Integer)
        Me.mnuEditDelete.ImageOptions.ImageKey = resources.GetString("mnuEditDelete.ImageOptions.ImageKey")
        Me.mnuEditDelete.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuEditDelete.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuEditDelete.ImageOptions.LargeImageKey = resources.GetString("mnuEditDelete.ImageOptions.LargeImageKey")
        Me.mnuEditDelete.ImageOptions.SvgImage = CType(resources.GetObject("mnuEditDelete.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEditDelete.Name = "mnuEditDelete"
        '
        'mnuFormat
        '
        resources.ApplyResources(Me.mnuFormat, "mnuFormat")
        Me.mnuFormat.Id = 10
        Me.mnuFormat.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFormat.ImageOptions.ImageIndex"), Integer)
        Me.mnuFormat.ImageOptions.ImageKey = resources.GetString("mnuFormat.ImageOptions.ImageKey")
        Me.mnuFormat.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFormat.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFormat.ImageOptions.LargeImageKey = resources.GetString("mnuFormat.ImageOptions.LargeImageKey")
        Me.mnuFormat.ImageOptions.SvgImage = CType(resources.GetObject("mnuFormat.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFormat.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFormatOrder)})
        Me.mnuFormat.Name = "mnuFormat"
        '
        'mnuFormatOrder
        '
        resources.ApplyResources(Me.mnuFormatOrder, "mnuFormatOrder")
        Me.mnuFormatOrder.Id = 11
        Me.mnuFormatOrder.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFormatOrder.ImageOptions.ImageIndex"), Integer)
        Me.mnuFormatOrder.ImageOptions.ImageKey = resources.GetString("mnuFormatOrder.ImageOptions.ImageKey")
        Me.mnuFormatOrder.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFormatOrder.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFormatOrder.ImageOptions.LargeImageKey = resources.GetString("mnuFormatOrder.ImageOptions.LargeImageKey")
        Me.mnuFormatOrder.ImageOptions.SvgImage = CType(resources.GetObject("mnuFormatOrder.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFormatOrder.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFormatOrderBringToFront), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFormatOrderSendToBack)})
        Me.mnuFormatOrder.Name = "mnuFormatOrder"
        '
        'mnuFormatOrderBringToFront
        '
        resources.ApplyResources(Me.mnuFormatOrderBringToFront, "mnuFormatOrderBringToFront")
        Me.mnuFormatOrderBringToFront.Id = 12
        Me.mnuFormatOrderBringToFront.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFormatOrderBringToFront.ImageOptions.ImageIndex"), Integer)
        Me.mnuFormatOrderBringToFront.ImageOptions.ImageKey = resources.GetString("mnuFormatOrderBringToFront.ImageOptions.ImageKey")
        Me.mnuFormatOrderBringToFront.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFormatOrderBringToFront.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFormatOrderBringToFront.ImageOptions.LargeImageKey = resources.GetString("mnuFormatOrderBringToFront.ImageOptions.LargeImageKey")
        Me.mnuFormatOrderBringToFront.ImageOptions.SvgImage = CType(resources.GetObject("mnuFormatOrderBringToFront.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFormatOrderBringToFront.Name = "mnuFormatOrderBringToFront"
        '
        'mnuFormatOrderSendToBack
        '
        resources.ApplyResources(Me.mnuFormatOrderSendToBack, "mnuFormatOrderSendToBack")
        Me.mnuFormatOrderSendToBack.Id = 13
        Me.mnuFormatOrderSendToBack.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFormatOrderSendToBack.ImageOptions.ImageIndex"), Integer)
        Me.mnuFormatOrderSendToBack.ImageOptions.ImageKey = resources.GetString("mnuFormatOrderSendToBack.ImageOptions.ImageKey")
        Me.mnuFormatOrderSendToBack.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFormatOrderSendToBack.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFormatOrderSendToBack.ImageOptions.LargeImageKey = resources.GetString("mnuFormatOrderSendToBack.ImageOptions.LargeImageKey")
        Me.mnuFormatOrderSendToBack.ImageOptions.SvgImage = CType(resources.GetObject("mnuFormatOrderSendToBack.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFormatOrderSendToBack.Name = "mnuFormatOrderSendToBack"
        '
        'mnuOptSet
        '
        resources.ApplyResources(Me.mnuOptSet, "mnuOptSet")
        Me.mnuOptSet.Id = 14
        Me.mnuOptSet.ImageOptions.ImageIndex = CType(resources.GetObject("mnuOptSet.ImageOptions.ImageIndex"), Integer)
        Me.mnuOptSet.ImageOptions.ImageKey = resources.GetString("mnuOptSet.ImageOptions.ImageKey")
        Me.mnuOptSet.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuOptSet.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuOptSet.ImageOptions.LargeImageKey = resources.GetString("mnuOptSet.ImageOptions.LargeImageKey")
        Me.mnuOptSet.ImageOptions.SvgImage = CType(resources.GetObject("mnuOptSet.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuOptSet.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuOptSetBackColor)})
        Me.mnuOptSet.Name = "mnuOptSet"
        '
        'mnuOptSetBackColor
        '
        resources.ApplyResources(Me.mnuOptSetBackColor, "mnuOptSetBackColor")
        Me.mnuOptSetBackColor.Id = 15
        Me.mnuOptSetBackColor.ImageOptions.ImageIndex = CType(resources.GetObject("mnuOptSetBackColor.ImageOptions.ImageIndex"), Integer)
        Me.mnuOptSetBackColor.ImageOptions.ImageKey = resources.GetString("mnuOptSetBackColor.ImageOptions.ImageKey")
        Me.mnuOptSetBackColor.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuOptSetBackColor.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuOptSetBackColor.ImageOptions.LargeImageKey = resources.GetString("mnuOptSetBackColor.ImageOptions.LargeImageKey")
        Me.mnuOptSetBackColor.ImageOptions.SvgImage = CType(resources.GetObject("mnuOptSetBackColor.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuOptSetBackColor.Name = "mnuOptSetBackColor"
        '
        'mnuFont
        '
        resources.ApplyResources(Me.mnuFont, "mnuFont")
        Me.mnuFont.Id = 16
        Me.mnuFont.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFont.ImageOptions.ImageIndex"), Integer)
        Me.mnuFont.ImageOptions.ImageKey = resources.GetString("mnuFont.ImageOptions.ImageKey")
        Me.mnuFont.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFont.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFont.ImageOptions.LargeImageKey = resources.GetString("mnuFont.ImageOptions.LargeImageKey")
        Me.mnuFont.ImageOptions.SvgImage = CType(resources.GetObject("mnuFont.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFont.Name = "mnuFont"
        '
        'mnuBackground
        '
        resources.ApplyResources(Me.mnuBackground, "mnuBackground")
        Me.mnuBackground.Id = 17
        Me.mnuBackground.ImageOptions.ImageIndex = CType(resources.GetObject("mnuBackground.ImageOptions.ImageIndex"), Integer)
        Me.mnuBackground.ImageOptions.ImageKey = resources.GetString("mnuBackground.ImageOptions.ImageKey")
        Me.mnuBackground.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuBackground.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuBackground.ImageOptions.LargeImageKey = resources.GetString("mnuBackground.ImageOptions.LargeImageKey")
        Me.mnuBackground.ImageOptions.SvgImage = CType(resources.GetObject("mnuBackground.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuBackground.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuLoadBackground), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuClearBackground), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuBackgroundLayout)})
        Me.mnuBackground.Name = "mnuBackground"
        '
        'mnuLoadBackground
        '
        resources.ApplyResources(Me.mnuLoadBackground, "mnuLoadBackground")
        Me.mnuLoadBackground.Id = 18
        Me.mnuLoadBackground.ImageOptions.ImageIndex = CType(resources.GetObject("mnuLoadBackground.ImageOptions.ImageIndex"), Integer)
        Me.mnuLoadBackground.ImageOptions.ImageKey = resources.GetString("mnuLoadBackground.ImageOptions.ImageKey")
        Me.mnuLoadBackground.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuLoadBackground.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuLoadBackground.ImageOptions.LargeImageKey = resources.GetString("mnuLoadBackground.ImageOptions.LargeImageKey")
        Me.mnuLoadBackground.ImageOptions.SvgImage = CType(resources.GetObject("mnuLoadBackground.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuLoadBackground.Name = "mnuLoadBackground"
        '
        'mnuClearBackground
        '
        resources.ApplyResources(Me.mnuClearBackground, "mnuClearBackground")
        Me.mnuClearBackground.Id = 19
        Me.mnuClearBackground.ImageOptions.ImageIndex = CType(resources.GetObject("mnuClearBackground.ImageOptions.ImageIndex"), Integer)
        Me.mnuClearBackground.ImageOptions.ImageKey = resources.GetString("mnuClearBackground.ImageOptions.ImageKey")
        Me.mnuClearBackground.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuClearBackground.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuClearBackground.ImageOptions.LargeImageKey = resources.GetString("mnuClearBackground.ImageOptions.LargeImageKey")
        Me.mnuClearBackground.ImageOptions.SvgImage = CType(resources.GetObject("mnuClearBackground.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuClearBackground.Name = "mnuClearBackground"
        '
        'mnuBackgroundLayout
        '
        resources.ApplyResources(Me.mnuBackgroundLayout, "mnuBackgroundLayout")
        Me.mnuBackgroundLayout.Id = 20
        Me.mnuBackgroundLayout.ImageOptions.ImageIndex = CType(resources.GetObject("mnuBackgroundLayout.ImageOptions.ImageIndex"), Integer)
        Me.mnuBackgroundLayout.ImageOptions.ImageKey = resources.GetString("mnuBackgroundLayout.ImageOptions.ImageKey")
        Me.mnuBackgroundLayout.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuBackgroundLayout.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuBackgroundLayout.ImageOptions.LargeImageKey = resources.GetString("mnuBackgroundLayout.ImageOptions.LargeImageKey")
        Me.mnuBackgroundLayout.ImageOptions.SvgImage = CType(resources.GetObject("mnuBackgroundLayout.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuBackgroundLayout.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuLayoutNone), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuLayoutTile), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuLayoutStrech), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuLayoutCenter), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuLayoutZoom)})
        Me.mnuBackgroundLayout.Name = "mnuBackgroundLayout"
        '
        'mnuLayoutNone
        '
        resources.ApplyResources(Me.mnuLayoutNone, "mnuLayoutNone")
        Me.mnuLayoutNone.Id = 21
        Me.mnuLayoutNone.ImageOptions.ImageIndex = CType(resources.GetObject("mnuLayoutNone.ImageOptions.ImageIndex"), Integer)
        Me.mnuLayoutNone.ImageOptions.ImageKey = resources.GetString("mnuLayoutNone.ImageOptions.ImageKey")
        Me.mnuLayoutNone.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuLayoutNone.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuLayoutNone.ImageOptions.LargeImageKey = resources.GetString("mnuLayoutNone.ImageOptions.LargeImageKey")
        Me.mnuLayoutNone.ImageOptions.SvgImage = CType(resources.GetObject("mnuLayoutNone.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuLayoutNone.Name = "mnuLayoutNone"
        '
        'mnuLayoutTile
        '
        resources.ApplyResources(Me.mnuLayoutTile, "mnuLayoutTile")
        Me.mnuLayoutTile.Id = 22
        Me.mnuLayoutTile.ImageOptions.ImageIndex = CType(resources.GetObject("mnuLayoutTile.ImageOptions.ImageIndex"), Integer)
        Me.mnuLayoutTile.ImageOptions.ImageKey = resources.GetString("mnuLayoutTile.ImageOptions.ImageKey")
        Me.mnuLayoutTile.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuLayoutTile.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuLayoutTile.ImageOptions.LargeImageKey = resources.GetString("mnuLayoutTile.ImageOptions.LargeImageKey")
        Me.mnuLayoutTile.ImageOptions.SvgImage = CType(resources.GetObject("mnuLayoutTile.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuLayoutTile.Name = "mnuLayoutTile"
        '
        'mnuLayoutStrech
        '
        resources.ApplyResources(Me.mnuLayoutStrech, "mnuLayoutStrech")
        Me.mnuLayoutStrech.Id = 23
        Me.mnuLayoutStrech.ImageOptions.ImageIndex = CType(resources.GetObject("mnuLayoutStrech.ImageOptions.ImageIndex"), Integer)
        Me.mnuLayoutStrech.ImageOptions.ImageKey = resources.GetString("mnuLayoutStrech.ImageOptions.ImageKey")
        Me.mnuLayoutStrech.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuLayoutStrech.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuLayoutStrech.ImageOptions.LargeImageKey = resources.GetString("mnuLayoutStrech.ImageOptions.LargeImageKey")
        Me.mnuLayoutStrech.ImageOptions.SvgImage = CType(resources.GetObject("mnuLayoutStrech.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuLayoutStrech.Name = "mnuLayoutStrech"
        '
        'mnuLayoutCenter
        '
        resources.ApplyResources(Me.mnuLayoutCenter, "mnuLayoutCenter")
        Me.mnuLayoutCenter.Id = 24
        Me.mnuLayoutCenter.ImageOptions.ImageIndex = CType(resources.GetObject("mnuLayoutCenter.ImageOptions.ImageIndex"), Integer)
        Me.mnuLayoutCenter.ImageOptions.ImageKey = resources.GetString("mnuLayoutCenter.ImageOptions.ImageKey")
        Me.mnuLayoutCenter.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuLayoutCenter.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuLayoutCenter.ImageOptions.LargeImageKey = resources.GetString("mnuLayoutCenter.ImageOptions.LargeImageKey")
        Me.mnuLayoutCenter.ImageOptions.SvgImage = CType(resources.GetObject("mnuLayoutCenter.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuLayoutCenter.Name = "mnuLayoutCenter"
        '
        'mnuLayoutZoom
        '
        resources.ApplyResources(Me.mnuLayoutZoom, "mnuLayoutZoom")
        Me.mnuLayoutZoom.Id = 25
        Me.mnuLayoutZoom.ImageOptions.ImageIndex = CType(resources.GetObject("mnuLayoutZoom.ImageOptions.ImageIndex"), Integer)
        Me.mnuLayoutZoom.ImageOptions.ImageKey = resources.GetString("mnuLayoutZoom.ImageOptions.ImageKey")
        Me.mnuLayoutZoom.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuLayoutZoom.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuLayoutZoom.ImageOptions.LargeImageKey = resources.GetString("mnuLayoutZoom.ImageOptions.LargeImageKey")
        Me.mnuLayoutZoom.ImageOptions.SvgImage = CType(resources.GetObject("mnuLayoutZoom.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuLayoutZoom.Name = "mnuLayoutZoom"
        '
        'mnuTransformation
        '
        resources.ApplyResources(Me.mnuTransformation, "mnuTransformation")
        Me.mnuTransformation.Id = 26
        Me.mnuTransformation.ImageOptions.ImageIndex = CType(resources.GetObject("mnuTransformation.ImageOptions.ImageIndex"), Integer)
        Me.mnuTransformation.ImageOptions.ImageKey = resources.GetString("mnuTransformation.ImageOptions.ImageKey")
        Me.mnuTransformation.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuTransformation.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuTransformation.ImageOptions.LargeImageKey = resources.GetString("mnuTransformation.ImageOptions.LargeImageKey")
        Me.mnuTransformation.ImageOptions.SvgImage = CType(resources.GetObject("mnuTransformation.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuTransformation.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuResetTransform), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFlipHorizontal), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuFlipVertical)})
        Me.mnuTransformation.Name = "mnuTransformation"
        '
        'mnuResetTransform
        '
        resources.ApplyResources(Me.mnuResetTransform, "mnuResetTransform")
        Me.mnuResetTransform.Id = 27
        Me.mnuResetTransform.ImageOptions.ImageIndex = CType(resources.GetObject("mnuResetTransform.ImageOptions.ImageIndex"), Integer)
        Me.mnuResetTransform.ImageOptions.ImageKey = resources.GetString("mnuResetTransform.ImageOptions.ImageKey")
        Me.mnuResetTransform.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuResetTransform.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuResetTransform.ImageOptions.LargeImageKey = resources.GetString("mnuResetTransform.ImageOptions.LargeImageKey")
        Me.mnuResetTransform.ImageOptions.SvgImage = CType(resources.GetObject("mnuResetTransform.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuResetTransform.Name = "mnuResetTransform"
        '
        'mnuFlipHorizontal
        '
        resources.ApplyResources(Me.mnuFlipHorizontal, "mnuFlipHorizontal")
        Me.mnuFlipHorizontal.Id = 28
        Me.mnuFlipHorizontal.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFlipHorizontal.ImageOptions.ImageIndex"), Integer)
        Me.mnuFlipHorizontal.ImageOptions.ImageKey = resources.GetString("mnuFlipHorizontal.ImageOptions.ImageKey")
        Me.mnuFlipHorizontal.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFlipHorizontal.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFlipHorizontal.ImageOptions.LargeImageKey = resources.GetString("mnuFlipHorizontal.ImageOptions.LargeImageKey")
        Me.mnuFlipHorizontal.ImageOptions.SvgImage = CType(resources.GetObject("mnuFlipHorizontal.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFlipHorizontal.Name = "mnuFlipHorizontal"
        '
        'mnuFlipVertical
        '
        resources.ApplyResources(Me.mnuFlipVertical, "mnuFlipVertical")
        Me.mnuFlipVertical.Id = 29
        Me.mnuFlipVertical.ImageOptions.ImageIndex = CType(resources.GetObject("mnuFlipVertical.ImageOptions.ImageIndex"), Integer)
        Me.mnuFlipVertical.ImageOptions.ImageKey = resources.GetString("mnuFlipVertical.ImageOptions.ImageKey")
        Me.mnuFlipVertical.ImageOptions.LargeImageIndex = CType(resources.GetObject("mnuFlipVertical.ImageOptions.LargeImageIndex"), Integer)
        Me.mnuFlipVertical.ImageOptions.LargeImageKey = resources.GetString("mnuFlipVertical.ImageOptions.LargeImageKey")
        Me.mnuFlipVertical.ImageOptions.SvgImage = CType(resources.GetObject("mnuFlipVertical.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFlipVertical.Name = "mnuFlipVertical"
        '
        'bar3
        '
        resources.ApplyResources(Me.bar3, "bar3")
        Me.bar3.BarName = "Status bar"
        Me.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.bar3.DockCol = 0
        Me.bar3.DockRow = 0
        Me.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.bar3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnZoom)})
        Me.bar3.OptionsBar.AllowQuickCustomization = False
        Me.bar3.OptionsBar.DrawDragBorder = False
        Me.bar3.OptionsBar.UseWholeRow = True
        '
        'btnZoom
        '
        resources.ApplyResources(Me.btnZoom, "btnZoom")
        Me.btnZoom.Edit = Me.RepositoryItemZoomTrackBar1
        Me.btnZoom.Id = 60
        Me.btnZoom.ImageOptions.ImageIndex = CType(resources.GetObject("btnZoom.ImageOptions.ImageIndex"), Integer)
        Me.btnZoom.ImageOptions.ImageKey = resources.GetString("btnZoom.ImageOptions.ImageKey")
        Me.btnZoom.ImageOptions.LargeImageIndex = CType(resources.GetObject("btnZoom.ImageOptions.LargeImageIndex"), Integer)
        Me.btnZoom.ImageOptions.LargeImageKey = resources.GetString("btnZoom.ImageOptions.LargeImageKey")
        Me.btnZoom.ImageOptions.SvgImage = CType(resources.GetObject("btnZoom.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnZoom.Name = "btnZoom"
        '
        'RepositoryItemZoomTrackBar1
        '
        Me.RepositoryItemZoomTrackBar1.Maximum = 100
        Me.RepositoryItemZoomTrackBar1.Minimum = 10
        Me.RepositoryItemZoomTrackBar1.Name = "RepositoryItemZoomTrackBar1"
        resources.ApplyResources(Me.RepositoryItemZoomTrackBar1, "RepositoryItemZoomTrackBar1")
        '
        'barDockControlTop
        '
        resources.ApplyResources(Me.barDockControlTop, "barDockControlTop")
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Manager = Me.barManager1
        '
        'barDockControlBottom
        '
        resources.ApplyResources(Me.barDockControlBottom, "barDockControlBottom")
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Manager = Me.barManager1
        '
        'barDockControlLeft
        '
        resources.ApplyResources(Me.barDockControlLeft, "barDockControlLeft")
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Manager = Me.barManager1
        '
        'barDockControlRight
        '
        resources.ApplyResources(Me.barDockControlRight, "barDockControlRight")
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Manager = Me.barManager1
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnPointer, "tbtnPointer", GetType(Global.DentistX.My.Resources.Resources), 0)
        Me.ImageCollection1.Images.SetKeyName(0, "tbtnPointer")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnLine, "tbtnLine", GetType(Global.DentistX.My.Resources.Resources), 1)
        Me.ImageCollection1.Images.SetKeyName(1, "tbtnLine")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnRectangle, "tbtnRectangle", GetType(Global.DentistX.My.Resources.Resources), 2)
        Me.ImageCollection1.Images.SetKeyName(2, "tbtnRectangle")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnEllipse, "tbtnEllipse", GetType(Global.DentistX.My.Resources.Resources), 3)
        Me.ImageCollection1.Images.SetKeyName(3, "tbtnEllipse")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnStar, "tbtnStar", GetType(Global.DentistX.My.Resources.Resources), 4)
        Me.ImageCollection1.Images.SetKeyName(4, "tbtnStar")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnImage, "tbtnImage", GetType(Global.DentistX.My.Resources.Resources), 5)
        Me.ImageCollection1.Images.SetKeyName(5, "tbtnImage")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnBringToFront, "tbtnBringToFront", GetType(Global.DentistX.My.Resources.Resources), 6)
        Me.ImageCollection1.Images.SetKeyName(6, "tbtnBringToFront")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnSendToBack, "tbtnSendToBack", GetType(Global.DentistX.My.Resources.Resources), 7)
        Me.ImageCollection1.Images.SetKeyName(7, "tbtnSendToBack")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnDelete, "tbtnDelete", GetType(Global.DentistX.My.Resources.Resources), 8)
        Me.ImageCollection1.Images.SetKeyName(8, "tbtnDelete")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.Colorfill16, "Colorfill16", GetType(Global.DentistX.My.Resources.Resources), 9)
        Me.ImageCollection1.Images.SetKeyName(9, "Colorfill16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.Ellipse16, "Ellipse16", GetType(Global.DentistX.My.Resources.Resources), 10)
        Me.ImageCollection1.Images.SetKeyName(10, "Ellipse16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.ForeColor, "ForeColor", GetType(Global.DentistX.My.Resources.Resources), 11)
        Me.ImageCollection1.Images.SetKeyName(11, "ForeColor")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.LightStar16, "LightStar16", GetType(Global.DentistX.My.Resources.Resources), 12)
        Me.ImageCollection1.Images.SetKeyName(12, "LightStar16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.Line16, "Line16", GetType(Global.DentistX.My.Resources.Resources), 13)
        Me.ImageCollection1.Images.SetKeyName(13, "Line16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.lineweight16, "lineweight16", GetType(Global.DentistX.My.Resources.Resources), 14)
        Me.ImageCollection1.Images.SetKeyName(14, "lineweight16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.Mouse_pointer16, "Mouse_pointer16", GetType(Global.DentistX.My.Resources.Resources), 15)
        Me.ImageCollection1.Images.SetKeyName(15, "Mouse_pointer16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.Rectangle16, "Rectangle16", GetType(Global.DentistX.My.Resources.Resources), 16)
        Me.ImageCollection1.Images.SetKeyName(16, "Rectangle16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnArrowRight16, "tbtnArrowRight16", GetType(Global.DentistX.My.Resources.Resources), 17)
        Me.ImageCollection1.Images.SetKeyName(17, "tbtnArrowRight16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnCrop16, "tbtnCrop16", GetType(Global.DentistX.My.Resources.Resources), 18)
        Me.ImageCollection1.Images.SetKeyName(18, "tbtnCrop16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnEraser16, "tbtnEraser16", GetType(Global.DentistX.My.Resources.Resources), 19)
        Me.ImageCollection1.Images.SetKeyName(19, "tbtnEraser16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnFlip_Horizontal16, "tbtnFlip_Horizontal16", GetType(Global.DentistX.My.Resources.Resources), 20)
        Me.ImageCollection1.Images.SetKeyName(20, "tbtnFlip_Horizontal16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnFlip_Vertical16, "tbtnFlip_Vertical16", GetType(Global.DentistX.My.Resources.Resources), 21)
        Me.ImageCollection1.Images.SetKeyName(21, "tbtnFlip_Vertical16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnFreeHand16, "tbtnFreeHand16", GetType(Global.DentistX.My.Resources.Resources), 22)
        Me.ImageCollection1.Images.SetKeyName(22, "tbtnFreeHand16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnPolyline16, "tbtnPolyline16", GetType(Global.DentistX.My.Resources.Resources), 23)
        Me.ImageCollection1.Images.SetKeyName(23, "tbtnPolyline16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnRotate_ccw16, "tbtnRotate_ccw16", GetType(Global.DentistX.My.Resources.Resources), 24)
        Me.ImageCollection1.Images.SetKeyName(24, "tbtnRotate_ccw16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnRotate_cw16, "tbtnRotate_cw16", GetType(Global.DentistX.My.Resources.Resources), 25)
        Me.ImageCollection1.Images.SetKeyName(25, "tbtnRotate_cw16")
        Me.ImageCollection1.InsertImage(Global.DentistX.My.Resources.Resources.tbtnText16, "tbtnText16", GetType(Global.DentistX.My.Resources.Resources), 26)
        Me.ImageCollection1.Images.SetKeyName(26, "tbtnText16")
        '
        'FontSelector
        '
        resources.ApplyResources(Me.FontSelector, "FontSelector")
        Me.FontSelector.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("FontSelector.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.FontSelector.Name = "FontSelector"
        Me.FontSelector.PopupSizeable = True
        '
        'RepositoryItemTextEdit1
        '
        resources.ApplyResources(Me.RepositoryItemTextEdit1, "RepositoryItemTextEdit1")
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'picCanvas
        '
        resources.ApplyResources(Me.picCanvas, "picCanvas")
        Me.picCanvas.BackColor = System.Drawing.SystemColors.Control
        Me.picCanvas.Name = "picCanvas"
        Me.picCanvas.TabStop = False
        '
        'pdPrint
        '
        '
        'ppdPrint
        '
        resources.ApplyResources(Me.ppdPrint, "ppdPrint")
        Me.ppdPrint.Document = Me.pdPrint
        Me.ppdPrint.Name = "ppdPrint"
        '
        'ofdImage
        '
        Me.ofdImage.FileName = "OpenFileDialog1"
        resources.ApplyResources(Me.ofdImage, "ofdImage")
        '
        'sfdImage
        '
        resources.ApplyResources(Me.sfdImage, "sfdImage")
        '
        'dlgOpenPicture
        '
        resources.ApplyResources(Me.dlgOpenPicture, "dlgOpenPicture")
        '
        'dlgSavePicture
        '
        resources.ApplyResources(Me.dlgSavePicture, "dlgSavePicture")
        '
        'tmrAutoSave
        '
        Me.tmrAutoSave.Interval = 5000
        '
        'pnlCanvasHost
        '
        resources.ApplyResources(Me.pnlCanvasHost, "pnlCanvasHost")
        Me.pnlCanvasHost.Controls.Add(Me.picCanvas)
        Me.pnlCanvasHost.Name = "pnlCanvasHost"
        '
        'Panel1
        '
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Controls.Add(Me.pnlCanvasHost)
        Me.Panel1.Controls.Add(Me.popForeColor)
        Me.Panel1.Controls.Add(Me.popThickness)
        Me.Panel1.Controls.Add(Me.popStarCorners)
        Me.Panel1.Controls.Add(Me.popFillColor)
        Me.Panel1.Name = "Panel1"
        '
        'DrawForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "DrawForm"
        CType(Me.barManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.popThickness, System.ComponentModel.ISupportInitialize).EndInit()
        Me.popThickness.ResumeLayout(False)
        CType(Me.lstThickness, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.popForeColor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.popForeColor.ResumeLayout(False)
        CType(Me.lstForeColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.popFillColor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.popFillColor.ResumeLayout(False)
        CType(Me.lstFillColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTextEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.popStarCorners, System.ComponentModel.ISupportInitialize).EndInit()
        Me.popStarCorners.ResumeLayout(False)
        CType(Me.lstStarCorners, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FontSelector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCanvasHost.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private WithEvents barManager1 As DevExpress.XtraBars.BarManager
    Private WithEvents bar1 As DevExpress.XtraBars.Bar
    Private WithEvents bar2 As DevExpress.XtraBars.Bar
    Private WithEvents bar3 As DevExpress.XtraBars.Bar
    Private WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents mnuFile As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuFileNew As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFileOpen As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFileOpenDicomFile As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFileOpenDicomFolder As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFileSave As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFileSaveImage As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFilePrintPreview As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFilePrint As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFileExit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEdit As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuEditDelete As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFormat As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuFormatOrder As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuFormatOrderBringToFront As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFormatOrderSendToBack As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuOptSet As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuOptSetBackColor As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFont As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuBackground As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuLoadBackground As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuClearBackground As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuBackgroundLayout As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuLayoutNone As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuLayoutTile As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuLayoutStrech As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuLayoutCenter As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuLayoutZoom As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTransformation As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuResetTransform As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFlipHorizontal As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFlipVertical As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbtnLine As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbtnRectangle As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbtnEllipse As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbtnStar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbtnImage As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbtnFreehand As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbtnText As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tcboThickness As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tcboLineColor As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tcboFillColor As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnBringToFront As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSendToBack As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnDelete As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents picCanvas As PictureBox
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents tbtnPointer As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents popThickness As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents lstThickness As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents txtInside As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents FontSelector As DevExpress.XtraEditors.Repository.RepositoryItemFontEdit
    Friend WithEvents popFillColor As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents lstFillColor As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents popForeColor As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents lstForeColor As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents tbtnArrow As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents dlgBackColor As ColorDialog
    Friend WithEvents pdPrint As Printing.PrintDocument
    Friend WithEvents ppdPrint As PrintPreviewDialog
    Friend WithEvents ofdImage As OpenFileDialog
    Friend WithEvents sfdImage As SaveFileDialog
    Friend WithEvents dlgOpenPicture As OpenFileDialog
    Friend WithEvents dlgSavePicture As SaveFileDialog
    Friend WithEvents txtText As DevExpress.XtraBars.BarEditItem
    Friend WithEvents txtTextEdit As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents tmrAutoSave As Timer
    Friend WithEvents btnSaveClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnCrop As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnStarCorners As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents popStarCorners As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents lstStarCorners As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents btnArrowProps As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pnlCanvasHost As Panel
    Friend WithEvents btnRotLeft As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnRotRight As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents btnZoom As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemZoomTrackBar1 As DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar
End Class
