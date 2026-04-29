<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class MainRxFly
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
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
        Me.DsMainRX1 = New DentistX.DsMainRx()
        Me.RxFly_BdyVWTableAdapter = New DentistX.DsMainRxTableAdapters.RxFly_BdyVWTableAdapter()
        CType(Me.DsMainRX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 25.4!
        Me.TopMargin.HeightF = 0.5!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 25.4!
        Me.BottomMargin.HeightF = 0!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel18, Me.XrLabel17, Me.XrLabel16, Me.XrLabel15, Me.XrLabel14, Me.XrLabel13, Me.XrLabel12, Me.XrLabel10, Me.XrLabel11, Me.XrLabel9, Me.XrLabel8, Me.XrLine1})
        Me.Detail.Dpi = 25.4!
        Me.Detail.HeightF = 150.0!
        Me.Detail.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail.Name = "Detail"
        '
        'XrLabel18
        '
        Me.XrLabel18.Dpi = 25.4!
        Me.XrLabel18.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DrName]")})
        Me.XrLabel18.Font = New DevExpress.Drawing.DXFont("Blackadder ITC", 12.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle), DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(109.0375!, 144.158!)
        Me.XrLabel18.Multiline = True
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(28.31042!, 5.84201!)
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "Dr Imad Abatly"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel17
        '
        Me.XrLabel17.Dpi = 25.4!
        Me.XrLabel17.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RX]")})
        Me.XrLabel17.Font = New DevExpress.Drawing.DXFont("Rockwell", 9.75!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle), DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(15.37499!, 26.64059!)
        Me.XrLabel17.Multiline = True
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(121.9729!, 115.3477!)
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "XrLabel17"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel16
        '
        Me.XrLabel16.Dpi = 25.4!
        Me.XrLabel16.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientAge]")})
        Me.XrLabel16.Font = New DevExpress.Drawing.DXFont("Arial", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(2.5!, 2.5!)
        Me.XrLabel16.Multiline = True
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "XrLabel16"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel15
        '
        Me.XrLabel15.Dpi = 25.4!
        Me.XrLabel15.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RXDate]")})
        Me.XrLabel15.Font = New DevExpress.Drawing.DXFont("Arial", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(2.500001!, 8.342!)
        Me.XrLabel15.Multiline = True
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(23.81249!, 5.841999!)
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "XrLabel15"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel15.TextFormatString = "{0:d}"
        '
        'XrLabel14
        '
        Me.XrLabel14.Dpi = 25.4!
        Me.XrLabel14.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientSex]")})
        Me.XrLabel14.Font = New DevExpress.Drawing.DXFont("Arial", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(95.6562!, 8.342!)
        Me.XrLabel14.Multiline = True
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(25.4!, 5.841999!)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "XrLabel14"
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel13
        '
        Me.XrLabel13.Dpi = 25.4!
        Me.XrLabel13.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientName]")})
        Me.XrLabel13.Font = New DevExpress.Drawing.DXFont("Arial", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(56.29996!, 2.5!)
        Me.XrLabel13.Multiline = True
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(63.85624!, 5.842!)
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
        Me.XrLabel12.Dpi = 25.4!
        Me.XrLabel12.Font = New DevExpress.Drawing.DXFont("Forte", 16.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle), DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(2.41041!, 20.026!)
        Me.XrLabel12.Multiline = True
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(10.84792!, 5.841997!)
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
        Me.XrLabel10.Dpi = 25.4!
        Me.XrLabel10.Font = New DevExpress.Drawing.DXFont("Arial", 12.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(28.95833!, 2.5!)
        Me.XrLabel10.Multiline = True
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(11.64167!, 5.842!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.StylePriority.UseTextAlignment = False
        Me.XrLabel10.Text = ": العمر"
        Me.XrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel11
        '
        Me.XrLabel11.AutoWidth = True
        Me.XrLabel11.Dpi = 25.4!
        Me.XrLabel11.Font = New DevExpress.Drawing.DXFont("Arial", 12.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(26.84167!, 8.342!)
        Me.XrLabel11.Multiline = True
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(13.75833!, 5.841999!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = ": التاريخ"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel9
        '
        Me.XrLabel9.AutoWidth = True
        Me.XrLabel9.Dpi = 25.4!
        Me.XrLabel9.Font = New DevExpress.Drawing.DXFont("Arial", 12.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(121.8563!, 8.342!)
        Me.XrLabel9.Multiline = True
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(19.84374!, 5.841999!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.Text = ": الجنس"
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel8
        '
        Me.XrLabel8.AutoWidth = True
        Me.XrLabel8.Dpi = 25.4!
        Me.XrLabel8.Font = New DevExpress.Drawing.DXFont("Arial", 12.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(120.1563!, 2.5!)
        Me.XrLabel8.Multiline = True
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(21.54375!, 5.842!)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = ": اسم المريض"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 25.4!
        Me.XrLine1.LineWidth = 2.0!
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(1.0875!, 14.184!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(144.7271!, 5.842!)
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPanel1})
        Me.PageHeader.Dpi = 25.4!
        Me.PageHeader.HeightF = 40.0!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrPanel1
        '
        Me.XrPanel1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrPanel1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel3, Me.XrLabel7, Me.XrLabel6, Me.XrLabel5, Me.XrLabel4, Me.XrPictureBox1})
        Me.XrPanel1.Dpi = 25.4!
        Me.XrPanel1.LocationFloat = New DevExpress.Utils.PointFloat(1.0875!, 0!)
        Me.XrPanel1.Name = "XrPanel1"
        Me.XrPanel1.SizeF = New System.Drawing.SizeF(144.7271!, 40.0!)
        Me.XrPanel1.StylePriority.UseBorders = False
        '
        'XrLabel3
        '
        Me.XrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel3.Dpi = 25.4!
        Me.XrLabel3.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EnHdrName]")})
        Me.XrLabel3.Font = New DevExpress.Drawing.DXFont("Arial", 14.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(1.677084!, 4.14201!)
        Me.XrLabel3.Multiline = True
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(57.94375!, 5.842!)
        Me.XrLabel3.StylePriority.UseBorders = False
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.Text = "XrLabel3"
        '
        'XrLabel7
        '
        Me.XrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel7.Dpi = 25.4!
        Me.XrLabel7.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Detail]")})
        Me.XrLabel7.Font = New DevExpress.Drawing.DXFont("Arial", 14.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(1.322909!, 31.64199!)
        Me.XrLabel7.Multiline = True
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(141.0896!, 7.500002!)
        Me.XrLabel7.StylePriority.UseBorders = False
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "XrLabel7"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel6
        '
        Me.XrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel6.Dpi = 25.4!
        Me.XrLabel6.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ArHdrAdres]")})
        Me.XrLabel6.Font = New DevExpress.Drawing.DXFont("Arial", 14.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(85.37711!, 12.77309!)
        Me.XrLabel6.Multiline = True
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(55.23542!, 18.8689!)
        Me.XrLabel6.StylePriority.UseBorders = False
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "XrLabel6"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel5
        '
        Me.XrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel5.Dpi = 25.4!
        Me.XrLabel5.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ArHdrName]")})
        Me.XrLabel5.Font = New DevExpress.Drawing.DXFont("Arial", 14.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(85.37711!, 4.142003!)
        Me.XrLabel5.Multiline = True
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(55.2354!, 5.842!)
        Me.XrLabel5.StylePriority.UseBorders = False
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "XrLabel5"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel4
        '
        Me.XrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel4.Dpi = 25.4!
        Me.XrLabel4.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EnHdrAdres]")})
        Me.XrLabel4.Font = New DevExpress.Drawing.DXFont("Arial", 14.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(1.677084!, 12.77309!)
        Me.XrLabel4.Multiline = True
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(57.94375!, 18.8689!)
        Me.XrLabel4.StylePriority.UseBorders = False
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "XrLabel4"
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.Dpi = 25.4!
        Me.XrPictureBox1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[Logo]")})
        Me.XrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(59.97709!, 2.500006!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(25.4!, 25.142!)
        Me.XrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPanel2})
        Me.PageFooter.Dpi = 25.4!
        Me.PageFooter.HeightF = 12.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrPanel2
        '
        Me.XrPanel2.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrPanel2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabel2})
        Me.XrPanel2.Dpi = 25.4!
        Me.XrPanel2.LocationFloat = New DevExpress.Utils.PointFloat(0.06668682!, 0!)
        Me.XrPanel2.Name = "XrPanel2"
        Me.XrPanel2.SizeF = New System.Drawing.SizeF(145.7479!, 11.0!)
        Me.XrPanel2.StylePriority.UseBorders = False
        '
        'XrLabel1
        '
        Me.XrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel1.Dpi = 25.4!
        Me.XrLabel1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ArFtr]")})
        Me.XrLabel1.Font = New DevExpress.Drawing.DXFont("Simple Outline Pat", 12.75!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle), DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(178, Byte))})
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(73.73022!, 0.5!)
        Me.XrLabel1.Multiline = True
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(68.19997!, 10.0!)
        Me.XrLabel1.StylePriority.UseBorders = False
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel2
        '
        Me.XrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel2.Dpi = 25.4!
        Me.XrLabel2.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EnFtr]")})
        Me.XrLabel2.Font = New DevExpress.Drawing.DXFont("Segoe Print", 12.75!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle), DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(3.817694!, 0.5!)
        Me.XrLabel2.Multiline = True
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(68.19998!, 10.0!)
        Me.XrLabel2.StylePriority.UseBorders = False
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "XrLabel2"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'DsMainRX1
        '
        Me.DsMainRX1.DataSetName = "DsMainRX"
        Me.DsMainRX1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RxFly_BdyVWTableAdapter
        '
        Me.RxFly_BdyVWTableAdapter.ClearBeforeFill = True
        '
        'MainRxFly
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.Detail, Me.PageHeader, Me.PageFooter})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.DsMainRX1})
        Me.DataAdapter = Me.RxFly_BdyVWTableAdapter
        Me.DataMember = "RxFly_BdyVW"
        Me.DataSource = Me.DsMainRX1
        Me.Dpi = 25.4!
        Me.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!)
        Me.Margins = New DevExpress.Drawing.DXMargins(1.0!, 1.0!, 0.5!, 0!)
        Me.PageHeightF = 210.1!
        Me.PageWidthF = 148.1!
        Me.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.Millimeters
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.SnapGridSize = 2.5!
        Me.Version = "25.1"
        XrWatermark1.Id = "Watermark1"
        XrWatermark1.TextTransparency = 30
        Me.Watermarks.AddRange(New DevExpress.XtraPrinting.Drawing.Watermark() {XrWatermark1})
        CType(Me.DsMainRX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents DsMainRX1 As DsMainRx
    Friend WithEvents RxFly_BdyVWTableAdapter As DsMainRxTableAdapters.RxFly_BdyVWTableAdapter
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
