<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class MainRX
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim XrWatermark1 As DevExpress.XtraReports.UI.XRWatermark = New DevExpress.XtraReports.UI.XRWatermark()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrPanel1 = New DevExpress.XtraReports.UI.XRPanel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrPanel2 = New DevExpress.XtraReports.UI.XRPanel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.DsMainRx1 = New DentistX.DsMainRx()
        Me.Rx_BdyVWTableAdapter = New DentistX.DsMainRxTableAdapters.Rx_BdyVWTableAdapter()
        CType(Me.DsMainRx1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.HeightF = 5.0!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.HeightF = 0!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel18, Me.XrLabel17, Me.XrLabel16, Me.XrLabel15, Me.XrLabel14, Me.XrLabel13, Me.XrLabel12, Me.XrLabel10, Me.XrLabel11, Me.XrLabel9, Me.XrLabel8, Me.XrLine1})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 1500.0!
        Me.Detail.HierarchyPrintOptions.Indent = 50.8!
        Me.Detail.Name = "Detail"
        '
        'XrLabel18
        '
        Me.XrLabel18.Dpi = 254.0!
        Me.XrLabel18.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DrName]")})
        Me.XrLabel18.Font = New DevExpress.Drawing.DXFont("Blackadder ITC", 12.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle), DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(1090.375!, 1441.58!)
        Me.XrLabel18.Multiline = True
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(283.1042!, 58.42004!)
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "Dr Imad Abatly"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel17
        '
        Me.XrLabel17.Dpi = 254.0!
        Me.XrLabel17.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RX]")})
        Me.XrLabel17.Font = New DevExpress.Drawing.DXFont("Rockwell", 9.75!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle), DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(153.7499!, 266.4059!)
        Me.XrLabel17.Multiline = True
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(1219.729!, 1153.477!)
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "XrLabel17"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel16
        '
        Me.XrLabel16.Dpi = 254.0!
        Me.XrLabel16.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BirthY]")})
        Me.XrLabel16.Font = New DevExpress.Drawing.DXFont("Arial", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(25.0!, 25.0!)
        Me.XrLabel16.Multiline = True
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "XrLabel16"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel15
        '
        Me.XrLabel15.Dpi = 254.0!
        Me.XrLabel15.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RXDate]")})
        Me.XrLabel15.Font = New DevExpress.Drawing.DXFont("Arial", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 83.42!)
        Me.XrLabel15.Multiline = True
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(238.1249!, 58.42!)
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "XrLabel15"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel15.TextFormatString = "{0:d}"
        '
        'XrLabel14
        '
        Me.XrLabel14.Dpi = 254.0!
        Me.XrLabel14.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Sex]")})
        Me.XrLabel14.Font = New DevExpress.Drawing.DXFont("Arial", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(956.562!, 83.42!)
        Me.XrLabel14.Multiline = True
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "XrLabel14"
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel13
        '
        Me.XrLabel13.Dpi = 254.0!
        Me.XrLabel13.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientName]")})
        Me.XrLabel13.Font = New DevExpress.Drawing.DXFont("Arial", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(562.9996!, 25.0!)
        Me.XrLabel13.Multiline = True
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(638.5624!, 58.42!)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "XrLabel13"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel12
        '
        Me.XrLabel12.BackColor = System.Drawing.Color.Snow
        Me.XrLabel12.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.Font = New DevExpress.Drawing.DXFont("Forte", 16.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle), DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(24.1041!, 200.26!)
        Me.XrLabel12.Multiline = True
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(108.4792!, 58.41998!)
        Me.XrLabel12.StylePriority.UseBackColor = False
        Me.XrLabel12.StylePriority.UseBorders = False
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "Rx"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel10
        '
        Me.XrLabel10.AutoWidth = True
        Me.XrLabel10.Dpi = 254.0!
        Me.XrLabel10.Font = New DevExpress.Drawing.DXFont("Arial", 12.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(289.5833!, 25.0!)
        Me.XrLabel10.Multiline = True
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(116.4167!, 58.42!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.StylePriority.UseTextAlignment = False
        Me.XrLabel10.Text = ": العمر"
        Me.XrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel11
        '
        Me.XrLabel11.AutoWidth = True
        Me.XrLabel11.Dpi = 254.0!
        Me.XrLabel11.Font = New DevExpress.Drawing.DXFont("Arial", 12.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(268.4167!, 83.42!)
        Me.XrLabel11.Multiline = True
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(137.5833!, 58.42!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = ": التاريخ"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel9
        '
        Me.XrLabel9.AutoWidth = True
        Me.XrLabel9.Dpi = 254.0!
        Me.XrLabel9.Font = New DevExpress.Drawing.DXFont("Arial", 12.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(1218.563!, 83.42!)
        Me.XrLabel9.Multiline = True
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(198.4375!, 58.42!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.Text = ": الجنس"
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel8
        '
        Me.XrLabel8.AutoWidth = True
        Me.XrLabel8.Dpi = 254.0!
        Me.XrLabel8.Font = New DevExpress.Drawing.DXFont("Arial", 12.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(1201.563!, 25.0!)
        Me.XrLabel8.Multiline = True
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(215.4375!, 58.42!)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = ": اسم المريض"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 2.0!
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(10.875!, 141.84!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(1447.271!, 58.42!)
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPanel1})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 400.0!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrPanel1
        '
        Me.XrPanel1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrPanel1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel3, Me.XrLabel7, Me.XrLabel6, Me.XrLabel5, Me.XrLabel4, Me.XrPictureBox1})
        Me.XrPanel1.Dpi = 254.0!
        Me.XrPanel1.LocationFloat = New DevExpress.Utils.PointFloat(10.875!, 0!)
        Me.XrPanel1.Name = "XrPanel1"
        Me.XrPanel1.SizeF = New System.Drawing.SizeF(1447.271!, 400.0!)
        Me.XrPanel1.StylePriority.UseBorders = False
        '
        'XrLabel3
        '
        Me.XrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EnHdrName]")})
        Me.XrLabel3.Font = New DevExpress.Drawing.DXFont("Arial", 14.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(16.77084!, 41.4201!)
        Me.XrLabel3.Multiline = True
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(579.4375!, 58.42!)
        Me.XrLabel3.StylePriority.UseBorders = False
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.Text = "XrLabel3"
        '
        'XrLabel7
        '
        Me.XrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Detail]")})
        Me.XrLabel7.Font = New DevExpress.Drawing.DXFont("Arial", 14.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(13.22909!, 316.4199!)
        Me.XrLabel7.Multiline = True
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(1410.896!, 75.00003!)
        Me.XrLabel7.StylePriority.UseBorders = False
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "XrLabel7"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel6
        '
        Me.XrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ArHdrAdres]")})
        Me.XrLabel6.Font = New DevExpress.Drawing.DXFont("Arial", 14.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(853.7712!, 127.7309!)
        Me.XrLabel6.Multiline = True
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(552.3542!, 188.689!)
        Me.XrLabel6.StylePriority.UseBorders = False
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "XrLabel6"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel5
        '
        Me.XrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ArHdrName]")})
        Me.XrLabel5.Font = New DevExpress.Drawing.DXFont("Arial", 14.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(853.7711!, 41.42003!)
        Me.XrLabel5.Multiline = True
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(552.3541!, 58.41999!)
        Me.XrLabel5.StylePriority.UseBorders = False
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "XrLabel5"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel4
        '
        Me.XrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EnHdrAdres]")})
        Me.XrLabel4.Font = New DevExpress.Drawing.DXFont("Arial", 14.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(16.77084!, 127.7309!)
        Me.XrLabel4.Multiline = True
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(579.4375!, 188.689!)
        Me.XrLabel4.StylePriority.UseBorders = False
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "XrLabel4"
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.Dpi = 254.0!
        Me.XrPictureBox1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[Logo]")})
        Me.XrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(599.7709!, 25.00006!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(254.0!, 251.42!)
        Me.XrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPanel2})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 120.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrPanel2
        '
        Me.XrPanel2.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrPanel2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabel2})
        Me.XrPanel2.Dpi = 254.0!
        Me.XrPanel2.LocationFloat = New DevExpress.Utils.PointFloat(0.6668682!, 0!)
        Me.XrPanel2.Name = "XrPanel2"
        Me.XrPanel2.SizeF = New System.Drawing.SizeF(1457.479!, 110.0!)
        Me.XrPanel2.StylePriority.UseBorders = False
        '
        'XrLabel1
        '
        Me.XrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ArFtr]")})
        Me.XrLabel1.Font = New DevExpress.Drawing.DXFont("Simple Outline Pat", 12.75!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle), DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(178, Byte))})
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(737.3022!, 5.0!)
        Me.XrLabel1.Multiline = True
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(681.9998!, 100.0!)
        Me.XrLabel1.StylePriority.UseBorders = False
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel2
        '
        Me.XrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EnFtr]")})
        Me.XrLabel2.Font = New DevExpress.Drawing.DXFont("Segoe Print", 12.75!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle), DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(38.17694!, 5.0!)
        Me.XrLabel2.Multiline = True
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(681.9998!, 100.0!)
        Me.XrLabel2.StylePriority.UseBorders = False
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "XrLabel2"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'DsMainRx1
        '
        Me.DsMainRx1.DataSetName = "DsMainRx"
        Me.DsMainRx1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Rx_BdyVWTableAdapter
        '
        Me.Rx_BdyVWTableAdapter.ClearBeforeFill = True
        '
        'MainRX
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.Detail, Me.PageHeader, Me.PageFooter})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.DsMainRx1})
        Me.DataAdapter = Me.Rx_BdyVWTableAdapter
        Me.DataMember = "Rx_BdyVW"
        Me.DataSource = Me.DsMainRx1
        Me.Dpi = 254.0!
        Me.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!)
        Me.Margins = New DevExpress.Drawing.DXMargins(10.0!, 10.0!, 5.0!, 0!)
        Me.PageHeight = 2101.0!
        Me.PageWidth = 1481.0!
        Me.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.SnapGridSize = 25.0!
        Me.Version = "25.1"
        XrWatermark1.Id = "Watermark1"
        XrWatermark1.TextTransparency = 30
        Me.Watermarks.AddRange(New DevExpress.XtraPrinting.Drawing.Watermark() {XrWatermark1})
        CType(Me.DsMainRx1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents DsMainRx1 As DsMainRx
    Friend WithEvents Rx_BdyVWTableAdapter As DsMainRxTableAdapters.Rx_BdyVWTableAdapter
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPanel1 As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents XrPanel2 As DevExpress.XtraReports.UI.XRPanel
End Class
