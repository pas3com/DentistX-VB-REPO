<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class FinancialReport
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
        Me.components = New System.ComponentModel.Container()
        Dim StoredProcQuery1 As DevExpress.DataAccess.Sql.StoredProcQuery = New DevExpress.DataAccess.Sql.StoredProcQuery()
        Dim QueryParameter1 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FinancialReport))
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrWatermark1 As DevExpress.XtraReports.UI.XRWatermark = New DevExpress.XtraReports.UI.XRWatermark()
        Me.parPatientID = New DevExpress.XtraReports.Parameters.Parameter()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.pageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.pageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.label1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.DetailReport = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail1 = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.SqlDataSource1 = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
        Me.DetailReport1 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.table3 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow3 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell16 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell17 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell18 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell19 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Detail2 = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrLabel31 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel30 = New DevExpress.XtraReports.UI.XRLabel()
        Me.DetailReport2 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeader3 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.table5 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow5 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell26 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell27 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell28 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell29 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Detail3 = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter1 = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrLabel33 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel32 = New DevExpress.XtraReports.UI.XRLabel()
        Me.DetailReport3 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeader4 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel26 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Detail4 = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel29 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel28 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel27 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Title = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailCaption2 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData2 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData3_Odd = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.PageInfo = New DevExpress.XtraReports.UI.XRControlStyle()
        CType(Me.table3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.table5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'parPatientID
        '
        Me.parPatientID.Name = "parPatientID"
        Me.parPatientID.Type = GetType(Integer)
        Me.parPatientID.ValueInfo = "0"
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.HeightF = 254.0!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.pageInfo1, Me.pageInfo2})
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.HeightF = 254.0!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'pageInfo1
        '
        Me.pageInfo1.Dpi = 254.0!
        Me.pageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.pageInfo1.Name = "pageInfo1"
        Me.pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.pageInfo1.SizeF = New System.Drawing.SizeF(950.0!, 58.0!)
        Me.pageInfo1.StyleName = "PageInfo"
        '
        'pageInfo2
        '
        Me.pageInfo2.Dpi = 254.0!
        Me.pageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(950.0!, 0!)
        Me.pageInfo2.Name = "pageInfo2"
        Me.pageInfo2.SizeF = New System.Drawing.SizeF(950.0!, 58.0!)
        Me.pageInfo2.StyleName = "PageInfo"
        Me.pageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.pageInfo2.TextFormatString = "Page {0} of {1}"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.label1})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 91.54585!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'label1
        '
        Me.label1.Dpi = 254.0!
        Me.label1.Font = New DevExpress.Drawing.DXFont("Arial", 18.0!)
        Me.label1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.label1.Name = "label1"
        Me.label1.SizeF = New System.Drawing.SizeF(1900.0!, 87.91195!)
        Me.label1.StyleName = "Title"
        Me.label1.StylePriority.UseFont = False
        Me.label1.StylePriority.UseTextAlignment = False
        Me.label1.Text = "تفاصيل العلاجات والدفعات"
        Me.label1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'Detail
        '
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 0!
        Me.Detail.HierarchyPrintOptions.Indent = 50.8!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        '
        'DetailReport
        '
        Me.DetailReport.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail1})
        Me.DetailReport.DataMember = "GetPatientFinancialReport.Result1"
        Me.DetailReport.DataSource = Me.SqlDataSource1
        Me.DetailReport.Dpi = 254.0!
        Me.DetailReport.Level = 0
        Me.DetailReport.Name = "DetailReport"
        '
        'Detail1
        '
        Me.Detail1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine1, Me.XrLabel12, Me.XrLabel11, Me.XrLabel10, Me.XrLabel9, Me.XrLabel8, Me.XrLabel7, Me.XrLabel6, Me.XrLabel2, Me.XrLabel3, Me.XrLabel4, Me.XrLabel5, Me.XrLabel1})
        Me.Detail1.Dpi = 254.0!
        Me.Detail1.HeightF = 211.9408!
        Me.Detail1.HierarchyPrintOptions.Indent = 50.8!
        Me.Detail1.Name = "Detail1"
        '
        'XrLine1
        '
        Me.XrLine1.BorderWidth = 2.0!
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(5.833341!, 188.7875!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(1886.479!, 5.0!)
        Me.XrLine1.StylePriority.UseBorderWidth = False
        '
        'XrLabel12
        '
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Age]")})
        Me.XrLabel12.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(644.1094!, 31.35001!)
        Me.XrLabel12.Multiline = True
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(133.5446!, 58.41999!)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "XrLabel12"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel11
        '
        Me.XrLabel11.Dpi = 254.0!
        Me.XrLabel11.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Sex]")})
        Me.XrLabel11.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(914.6138!, 31.35001!)
        Me.XrLabel11.Multiline = True
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(164.0417!, 58.41999!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "XrLabel11"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel10
        '
        Me.XrLabel10.Dpi = 254.0!
        Me.XrLabel10.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Phone]")})
        Me.XrLabel10.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(256.759!, 31.35!)
        Me.XrLabel10.Multiline = True
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.StylePriority.UseTextAlignment = False
        Me.XrLabel10.Text = "XrLabel10"
        Me.XrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel9
        '
        Me.XrLabel9.Dpi = 254.0!
        Me.XrLabel9.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Address]")})
        Me.XrLabel9.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(248.0142!, 106.47!)
        Me.XrLabel9.Multiline = True
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(486.8332!, 58.41999!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.Text = "XrLabel9"
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel8
        '
        Me.XrLabel8.Dpi = 254.0!
        Me.XrLabel8.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientName]")})
        Me.XrLabel8.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(914.6138!, 106.47!)
        Me.XrLabel8.Multiline = True
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(727.2411!, 58.41999!)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = "XrLabel8"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel7
        '
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientID]")})
        Me.XrLabel7.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(1387.855!, 31.35!)
        Me.XrLabel7.Multiline = True
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "XrLabel7"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel6
        '
        Me.XrLabel6.BorderColor = System.Drawing.Color.Black
        Me.XrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel6.BorderWidth = 1.0!
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(734.8475!, 100.12!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(179.7663!, 71.12001!)
        Me.XrLabel6.StylePriority.UseBackColor = False
        Me.XrLabel6.StylePriority.UseBorderColor = False
        Me.XrLabel6.StylePriority.UseBorders = False
        Me.XrLabel6.StylePriority.UseBorderWidth = False
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseForeColor = False
        Me.XrLabel6.StylePriority.UsePadding = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = ":العنوان"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel2
        '
        Me.XrLabel2.BorderColor = System.Drawing.Color.Black
        Me.XrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel2.BorderWidth = 1.0!
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(1652.173!, 100.12!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(230.8276!, 71.12002!)
        Me.XrLabel2.StylePriority.UseBackColor = False
        Me.XrLabel2.StylePriority.UseBorderColor = False
        Me.XrLabel2.StylePriority.UseBorders = False
        Me.XrLabel2.StylePriority.UseBorderWidth = False
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseForeColor = False
        Me.XrLabel2.StylePriority.UsePadding = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = ":اسم المريض"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel3
        '
        Me.XrLabel3.BorderColor = System.Drawing.Color.Black
        Me.XrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel3.BorderWidth = 1.0!
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(1080.656!, 25.00001!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(170.3922!, 71.12001!)
        Me.XrLabel3.StylePriority.UseBackColor = False
        Me.XrLabel3.StylePriority.UseBorderColor = False
        Me.XrLabel3.StylePriority.UseBorders = False
        Me.XrLabel3.StylePriority.UseBorderWidth = False
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseForeColor = False
        Me.XrLabel3.StylePriority.UsePadding = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = ":الجنس"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel4
        '
        Me.XrLabel4.BorderColor = System.Drawing.Color.Black
        Me.XrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel4.BorderWidth = 1.0!
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(778.5058!, 25.0!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(133.3506!, 71.12001!)
        Me.XrLabel4.StylePriority.UseBackColor = False
        Me.XrLabel4.StylePriority.UseBorderColor = False
        Me.XrLabel4.StylePriority.UseBorders = False
        Me.XrLabel4.StylePriority.UseBorderWidth = False
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseForeColor = False
        Me.XrLabel4.StylePriority.UsePadding = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = ":العمر"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel5
        '
        Me.XrLabel5.BorderColor = System.Drawing.Color.Black
        Me.XrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel5.BorderWidth = 1.0!
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(510.759!, 25.0!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(133.3506!, 71.12001!)
        Me.XrLabel5.StylePriority.UseBackColor = False
        Me.XrLabel5.StylePriority.UseBorderColor = False
        Me.XrLabel5.StylePriority.UseBorders = False
        Me.XrLabel5.StylePriority.UseBorderWidth = False
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseForeColor = False
        Me.XrLabel5.StylePriority.UsePadding = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = ":الهاتف"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel1
        '
        Me.XrLabel1.BorderColor = System.Drawing.Color.Black
        Me.XrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel1.BorderWidth = 1.0!
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(1652.173!, 25.00001!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(230.8276!, 71.12!)
        Me.XrLabel1.StylePriority.UseBackColor = False
        Me.XrLabel1.StylePriority.UseBorderColor = False
        Me.XrLabel1.StylePriority.UseBorders = False
        Me.XrLabel1.StylePriority.UseBorderWidth = False
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseForeColor = False
        Me.XrLabel1.StylePriority.UsePadding = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = ":رقم المريض"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'SqlDataSource1
        '
        Me.SqlDataSource1.ConnectionName = "DentistX.My.MySettings.DentistXConnectionString"
        Me.SqlDataSource1.Name = "SqlDataSource1"
        StoredProcQuery1.Name = "GetPatientFinancialReport"
        QueryParameter1.Name = "@PatientID"
        QueryParameter1.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter1.Value = New DevExpress.DataAccess.Expression("?parPatientID", GetType(Integer))
        StoredProcQuery1.Parameters.AddRange(New DevExpress.DataAccess.Sql.QueryParameter() {QueryParameter1})
        StoredProcQuery1.StoredProcName = "GetPatientFinancialReport"
        Me.SqlDataSource1.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {StoredProcQuery1})
        Me.SqlDataSource1.ResultSchemaSerializable = resources.GetString("SqlDataSource1.ResultSchemaSerializable")
        '
        'DetailReport1
        '
        Me.DetailReport1.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader2, Me.Detail2, Me.ReportFooter})
        Me.DetailReport1.DataMember = "GetPatientFinancialReport.Result2"
        Me.DetailReport1.DataSource = Me.SqlDataSource1
        Me.DetailReport1.Dpi = 254.0!
        Me.DetailReport1.Level = 1
        Me.DetailReport1.Name = "DetailReport1"
        '
        'GroupHeader2
        '
        Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine2, Me.XrLabel22, Me.table3})
        Me.GroupHeader2.Dpi = 254.0!
        Me.GroupHeader2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader2.HeightF = 154.976!
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'XrLine2
        '
        Me.XrLine2.BorderWidth = 2.0!
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(5.833341!, 0!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(1886.479!, 5.0!)
        Me.XrLine2.StylePriority.UseBorderWidth = False
        '
        'XrLabel22
        '
        Me.XrLabel22.BorderColor = System.Drawing.Color.Black
        Me.XrLabel22.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel22.BorderWidth = 2.0!
        Me.XrLabel22.Dpi = 254.0!
        Me.XrLabel22.Font = New DevExpress.Drawing.DXFont("Arial", 16.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(794.2916!, 5.0!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(305.4122!, 71.12001!)
        Me.XrLabel22.StylePriority.UseBackColor = False
        Me.XrLabel22.StylePriority.UseBorderColor = False
        Me.XrLabel22.StylePriority.UseBorders = False
        Me.XrLabel22.StylePriority.UseBorderWidth = False
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.StylePriority.UseForeColor = False
        Me.XrLabel22.StylePriority.UsePadding = False
        Me.XrLabel22.StylePriority.UseTextAlignment = False
        Me.XrLabel22.Text = "العلاجات"
        Me.XrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'table3
        '
        Me.table3.Dpi = 254.0!
        Me.table3.ForeColor = System.Drawing.Color.Gainsboro
        Me.table3.LocationFloat = New DevExpress.Utils.PointFloat(0!, 83.12001!)
        Me.table3.Name = "table3"
        Me.table3.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow3})
        Me.table3.SizeF = New System.Drawing.SizeF(1900.0!, 71.12!)
        Me.table3.StylePriority.UseForeColor = False
        '
        'tableRow3
        '
        Me.tableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell16, Me.tableCell17, Me.tableCell18, Me.tableCell19})
        Me.tableRow3.Dpi = 254.0!
        Me.tableRow3.Name = "tableRow3"
        Me.tableRow3.Weight = 1.0R
        '
        'tableCell16
        '
        Me.tableCell16.BackColor = System.Drawing.Color.Black
        Me.tableCell16.Dpi = 254.0!
        Me.tableCell16.Name = "tableCell16"
        Me.tableCell16.StyleName = "DetailCaption2"
        Me.tableCell16.StylePriority.UseBackColor = False
        Me.tableCell16.StylePriority.UseTextAlignment = False
        Me.tableCell16.Text = "قيمة العلاج"
        Me.tableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell16.Weight = 0.165233067376814R
        '
        'tableCell17
        '
        Me.tableCell17.BackColor = System.Drawing.Color.Black
        Me.tableCell17.Dpi = 254.0!
        Me.tableCell17.Name = "tableCell17"
        Me.tableCell17.StyleName = "DetailCaption2"
        Me.tableCell17.StylePriority.UseBackColor = False
        Me.tableCell17.StylePriority.UseTextAlignment = False
        Me.tableCell17.Text = "تاريخ العلاج"
        Me.tableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell17.Weight = 0.18392599038998497R
        '
        'tableCell18
        '
        Me.tableCell18.BackColor = System.Drawing.Color.Black
        Me.tableCell18.Dpi = 254.0!
        Me.tableCell18.Name = "tableCell18"
        Me.tableCell18.StyleName = "DetailCaption2"
        Me.tableCell18.StylePriority.UseBackColor = False
        Me.tableCell18.StylePriority.UseTextAlignment = False
        Me.tableCell18.Text = "تفاصيل العلاج"
        Me.tableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell18.Weight = 0.45763310307047489R
        '
        'tableCell19
        '
        Me.tableCell19.BackColor = System.Drawing.Color.Black
        Me.tableCell19.Dpi = 254.0!
        Me.tableCell19.ForeColor = System.Drawing.Color.White
        Me.tableCell19.Name = "tableCell19"
        Me.tableCell19.StyleName = "DetailCaption2"
        Me.tableCell19.StylePriority.UseBackColor = False
        Me.tableCell19.StylePriority.UseForeColor = False
        Me.tableCell19.StylePriority.UseTextAlignment = False
        Me.tableCell19.Text = "رقم العلاج"
        Me.tableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell19.Weight = 0.19320783113178455R
        '
        'Detail2
        '
        Me.Detail2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel16, Me.XrLabel15, Me.XrLabel14, Me.XrLabel13})
        Me.Detail2.Dpi = 254.0!
        Me.Detail2.HeightF = 60.04648!
        Me.Detail2.HierarchyPrintOptions.Indent = 50.8!
        Me.Detail2.Name = "Detail2"
        '
        'XrLabel16
        '
        Me.XrLabel16.Dpi = 254.0!
        Me.XrLabel16.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TreatmentValue]")})
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(29.97142!, 0!)
        Me.XrLabel16.Multiline = True
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "XrLabel16"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel16.TextFormatString = "{0:#.00}"
        '
        'XrLabel15
        '
        Me.XrLabel15.Dpi = 254.0!
        Me.XrLabel15.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TreatmentDate]")})
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(313.9428!, 0!)
        Me.XrLabel15.Multiline = True
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(349.4594!, 58.42!)
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "XrLabel15"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel15.TextFormatString = "{0:d}"
        '
        'XrLabel14
        '
        Me.XrLabel14.Dpi = 254.0!
        Me.XrLabel14.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TreatmentDetail]")})
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(663.4022!, 0!)
        Me.XrLabel14.Multiline = True
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(869.5029!, 58.42!)
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "XrLabel14"
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel13
        '
        Me.XrLabel13.Dpi = 254.0!
        Me.XrLabel13.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TrtID]")})
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(1606.563!, 0!)
        Me.XrLabel13.Multiline = True
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "XrLabel13"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel31, Me.XrLabel30})
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 71.11996!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrLabel31
        '
        Me.XrLabel31.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.XrLabel31.BorderColor = System.Drawing.Color.White
        Me.XrLabel31.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel31.BorderWidth = 2.0!
        Me.XrLabel31.Dpi = 254.0!
        Me.XrLabel31.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel31.ForeColor = System.Drawing.Color.White
        Me.XrLabel31.LocationFloat = New DevExpress.Utils.PointFloat(313.9428!, 0!)
        Me.XrLabel31.Name = "XrLabel31"
        Me.XrLabel31.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        Me.XrLabel31.SizeF = New System.Drawing.SizeF(239.0424!, 71.11996!)
        Me.XrLabel31.StyleName = "DetailCaption2"
        Me.XrLabel31.StylePriority.UseBackColor = False
        Me.XrLabel31.StylePriority.UseBorderColor = False
        Me.XrLabel31.StylePriority.UseBorders = False
        Me.XrLabel31.StylePriority.UseBorderWidth = False
        Me.XrLabel31.StylePriority.UseFont = False
        Me.XrLabel31.StylePriority.UseForeColor = False
        Me.XrLabel31.StylePriority.UsePadding = False
        Me.XrLabel31.StylePriority.UseTextAlignment = False
        Me.XrLabel31.Text = "مجموع العلاجات"
        Me.XrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel30
        '
        Me.XrLabel30.Dpi = 254.0!
        Me.XrLabel30.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "isnull(Sum([TreatmentValue]),0)")})
        Me.XrLabel30.LocationFloat = New DevExpress.Utils.PointFloat(29.97142!, 0!)
        Me.XrLabel30.Multiline = True
        Me.XrLabel30.Name = "XrLabel30"
        Me.XrLabel30.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel30.SizeF = New System.Drawing.SizeF(254.0!, 71.11996!)
        Me.XrLabel30.StylePriority.UseTextAlignment = False
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel30.Summary = XrSummary1
        Me.XrLabel30.Text = "XrLabel16"
        Me.XrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel30.TextFormatString = "{0:#.00}"
        '
        'DetailReport2
        '
        Me.DetailReport2.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader3, Me.Detail3, Me.ReportFooter1})
        Me.DetailReport2.DataMember = "GetPatientFinancialReport.Result3"
        Me.DetailReport2.DataSource = Me.SqlDataSource1
        Me.DetailReport2.Dpi = 254.0!
        Me.DetailReport2.Level = 2
        Me.DetailReport2.Name = "DetailReport2"
        '
        'GroupHeader3
        '
        Me.GroupHeader3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine3, Me.XrLabel17, Me.table5})
        Me.GroupHeader3.Dpi = 254.0!
        Me.GroupHeader3.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader3.HeightF = 164.438!
        Me.GroupHeader3.Name = "GroupHeader3"
        '
        'XrLine3
        '
        Me.XrLine3.BorderWidth = 2.0!
        Me.XrLine3.Dpi = 254.0!
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(5.833341!, 0!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(1886.479!, 5.0!)
        Me.XrLine3.StylePriority.UseBorderWidth = False
        '
        'XrLabel17
        '
        Me.XrLabel17.BorderColor = System.Drawing.Color.Black
        Me.XrLabel17.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel17.BorderWidth = 2.0!
        Me.XrLabel17.Dpi = 254.0!
        Me.XrLabel17.Font = New DevExpress.Drawing.DXFont("Arial", 16.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(794.2916!, 12.0!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(305.4122!, 71.12001!)
        Me.XrLabel17.StylePriority.UseBackColor = False
        Me.XrLabel17.StylePriority.UseBorderColor = False
        Me.XrLabel17.StylePriority.UseBorders = False
        Me.XrLabel17.StylePriority.UseBorderWidth = False
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseForeColor = False
        Me.XrLabel17.StylePriority.UsePadding = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "الدفعات"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'table5
        '
        Me.table5.Dpi = 254.0!
        Me.table5.LocationFloat = New DevExpress.Utils.PointFloat(0!, 85.12001!)
        Me.table5.Name = "table5"
        Me.table5.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow5})
        Me.table5.SizeF = New System.Drawing.SizeF(1900.0!, 71.12!)
        '
        'tableRow5
        '
        Me.tableRow5.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell26, Me.tableCell27, Me.tableCell28, Me.tableCell29})
        Me.tableRow5.Dpi = 254.0!
        Me.tableRow5.Name = "tableRow5"
        Me.tableRow5.Weight = 1.0R
        '
        'tableCell26
        '
        Me.tableCell26.Dpi = 254.0!
        Me.tableCell26.Name = "tableCell26"
        Me.tableCell26.StyleName = "DetailCaption2"
        Me.tableCell26.StylePriority.UseTextAlignment = False
        Me.tableCell26.Text = "قيمة الدفعة"
        Me.tableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell26.Weight = 0.16523306488276349R
        '
        'tableCell27
        '
        Me.tableCell27.Dpi = 254.0!
        Me.tableCell27.Name = "tableCell27"
        Me.tableCell27.StyleName = "DetailCaption2"
        Me.tableCell27.StylePriority.UseTextAlignment = False
        Me.tableCell27.Text = "تاريخ الدفعة"
        Me.tableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell27.Weight = 0.18392599031780679R
        '
        'tableCell28
        '
        Me.tableCell28.Dpi = 254.0!
        Me.tableCell28.Name = "tableCell28"
        Me.tableCell28.StyleName = "DetailCaption2"
        Me.tableCell28.StylePriority.UseTextAlignment = False
        Me.tableCell28.Text = "ملاحطات الدفعة"
        Me.tableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell28.Weight = 0.45763311180432248R
        '
        'tableCell29
        '
        Me.tableCell29.Dpi = 254.0!
        Me.tableCell29.Name = "tableCell29"
        Me.tableCell29.StyleName = "DetailCaption2"
        Me.tableCell29.StylePriority.UseTextAlignment = False
        Me.tableCell29.Text = "رقم الدفعة"
        Me.tableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell29.Weight = 0.19320778480945749R
        '
        'Detail3
        '
        Me.Detail3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel21, Me.XrLabel20, Me.XrLabel19, Me.XrLabel18})
        Me.Detail3.Dpi = 254.0!
        Me.Detail3.HeightF = 78.11377!
        Me.Detail3.HierarchyPrintOptions.Indent = 50.8!
        Me.Detail3.Name = "Detail3"
        '
        'XrLabel21
        '
        Me.XrLabel21.Dpi = 254.0!
        Me.XrLabel21.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PayValue]")})
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(29.97142!, 12.87559!)
        Me.XrLabel21.Multiline = True
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        Me.XrLabel21.StylePriority.UseTextAlignment = False
        Me.XrLabel21.Text = "XrLabel21"
        Me.XrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel21.TextFormatString = "{0:#.00}"
        '
        'XrLabel20
        '
        Me.XrLabel20.Dpi = 254.0!
        Me.XrLabel20.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PayDate]")})
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(313.9428!, 12.87559!)
        Me.XrLabel20.Multiline = True
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(349.4594!, 58.42!)
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "XrLabel20"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel20.TextFormatString = "{0:d}"
        '
        'XrLabel19
        '
        Me.XrLabel19.Dpi = 254.0!
        Me.XrLabel19.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PaymentNotes]")})
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(663.4022!, 12.87559!)
        Me.XrLabel19.Multiline = True
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(869.5029!, 58.42!)
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "XrLabel19"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel18
        '
        Me.XrLabel18.Dpi = 254.0!
        Me.XrLabel18.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PayID]")})
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(1606.563!, 12.87559!)
        Me.XrLabel18.Multiline = True
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "XrLabel18"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'ReportFooter1
        '
        Me.ReportFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel33, Me.XrLabel32})
        Me.ReportFooter1.Dpi = 254.0!
        Me.ReportFooter1.HeightF = 72.69421!
        Me.ReportFooter1.Name = "ReportFooter1"
        '
        'XrLabel33
        '
        Me.XrLabel33.Dpi = 254.0!
        Me.XrLabel33.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "isnull(Sum([PayValue]),0)")})
        Me.XrLabel33.LocationFloat = New DevExpress.Utils.PointFloat(29.97144!, 0!)
        Me.XrLabel33.Multiline = True
        Me.XrLabel33.Name = "XrLabel33"
        Me.XrLabel33.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel33.SizeF = New System.Drawing.SizeF(254.0!, 71.12!)
        Me.XrLabel33.StylePriority.UseTextAlignment = False
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel33.Summary = XrSummary2
        Me.XrLabel33.Text = "XrLabel21"
        Me.XrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel33.TextFormatString = "{0:#.00}"
        '
        'XrLabel32
        '
        Me.XrLabel32.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.XrLabel32.BorderColor = System.Drawing.Color.White
        Me.XrLabel32.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel32.BorderWidth = 2.0!
        Me.XrLabel32.Dpi = 254.0!
        Me.XrLabel32.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel32.ForeColor = System.Drawing.Color.White
        Me.XrLabel32.LocationFloat = New DevExpress.Utils.PointFloat(313.9428!, 0!)
        Me.XrLabel32.Name = "XrLabel32"
        Me.XrLabel32.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        Me.XrLabel32.SizeF = New System.Drawing.SizeF(239.0424!, 71.12!)
        Me.XrLabel32.StyleName = "DetailCaption2"
        Me.XrLabel32.StylePriority.UseBackColor = False
        Me.XrLabel32.StylePriority.UseBorderColor = False
        Me.XrLabel32.StylePriority.UseBorders = False
        Me.XrLabel32.StylePriority.UseBorderWidth = False
        Me.XrLabel32.StylePriority.UseFont = False
        Me.XrLabel32.StylePriority.UseForeColor = False
        Me.XrLabel32.StylePriority.UsePadding = False
        Me.XrLabel32.StylePriority.UseTextAlignment = False
        Me.XrLabel32.Text = "مجموع الدفعات"
        Me.XrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailReport3
        '
        Me.DetailReport3.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader4, Me.Detail4})
        Me.DetailReport3.DataMember = "GetPatientFinancialReport.Result4"
        Me.DetailReport3.DataSource = Me.SqlDataSource1
        Me.DetailReport3.Dpi = 254.0!
        Me.DetailReport3.Level = 3
        Me.DetailReport3.Name = "DetailReport3"
        '
        'GroupHeader4
        '
        Me.GroupHeader4.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine4, Me.XrLabel23, Me.XrLabel24, Me.XrLabel25, Me.XrLabel26})
        Me.GroupHeader4.Dpi = 254.0!
        Me.GroupHeader4.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader4.HeightF = 190.12!
        Me.GroupHeader4.Name = "GroupHeader4"
        '
        'XrLine4
        '
        Me.XrLine4.BorderWidth = 2.0!
        Me.XrLine4.Dpi = 254.0!
        Me.XrLine4.LocationFloat = New DevExpress.Utils.PointFloat(5.833341!, 2.0!)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.SizeF = New System.Drawing.SizeF(1886.479!, 5.0!)
        Me.XrLine4.StylePriority.UseBorderWidth = False
        '
        'XrLabel23
        '
        Me.XrLabel23.BorderColor = System.Drawing.Color.Black
        Me.XrLabel23.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel23.BorderWidth = 2.0!
        Me.XrLabel23.Dpi = 254.0!
        Me.XrLabel23.Font = New DevExpress.Drawing.DXFont("Arial", 16.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(764.5696!, 21.0!)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(364.8563!, 71.12001!)
        Me.XrLabel23.StylePriority.UseBackColor = False
        Me.XrLabel23.StylePriority.UseBorderColor = False
        Me.XrLabel23.StylePriority.UseBorders = False
        Me.XrLabel23.StylePriority.UseBorderWidth = False
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.StylePriority.UseForeColor = False
        Me.XrLabel23.StylePriority.UsePadding = False
        Me.XrLabel23.StylePriority.UseTextAlignment = False
        Me.XrLabel23.Text = "الرصيد النهائي"
        Me.XrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel24
        '
        Me.XrLabel24.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.XrLabel24.BorderColor = System.Drawing.Color.White
        Me.XrLabel24.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel24.BorderWidth = 2.0!
        Me.XrLabel24.Dpi = 254.0!
        Me.XrLabel24.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel24.ForeColor = System.Drawing.Color.White
        Me.XrLabel24.LocationFloat = New DevExpress.Utils.PointFloat(490.9893!, 119.0!)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        Me.XrLabel24.SizeF = New System.Drawing.SizeF(239.0424!, 71.12!)
        Me.XrLabel24.StyleName = "DetailCaption2"
        Me.XrLabel24.StylePriority.UseBackColor = False
        Me.XrLabel24.StylePriority.UseBorderColor = False
        Me.XrLabel24.StylePriority.UseBorders = False
        Me.XrLabel24.StylePriority.UseBorderWidth = False
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.StylePriority.UseForeColor = False
        Me.XrLabel24.StylePriority.UsePadding = False
        Me.XrLabel24.StylePriority.UseTextAlignment = False
        Me.XrLabel24.Text = "الرصيد"
        Me.XrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel25
        '
        Me.XrLabel25.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.XrLabel25.BorderColor = System.Drawing.Color.White
        Me.XrLabel25.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel25.BorderWidth = 2.0!
        Me.XrLabel25.Dpi = 254.0!
        Me.XrLabel25.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel25.ForeColor = System.Drawing.Color.White
        Me.XrLabel25.LocationFloat = New DevExpress.Utils.PointFloat(840.2391!, 119.0!)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        Me.XrLabel25.SizeF = New System.Drawing.SizeF(239.0424!, 71.12!)
        Me.XrLabel25.StyleName = "DetailCaption2"
        Me.XrLabel25.StylePriority.UseBackColor = False
        Me.XrLabel25.StylePriority.UseBorderColor = False
        Me.XrLabel25.StylePriority.UseBorders = False
        Me.XrLabel25.StylePriority.UseBorderWidth = False
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.StylePriority.UseForeColor = False
        Me.XrLabel25.StylePriority.UsePadding = False
        Me.XrLabel25.StylePriority.UseTextAlignment = False
        Me.XrLabel25.Text = "مجموع الدفعات"
        Me.XrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel26
        '
        Me.XrLabel26.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.XrLabel26.BorderColor = System.Drawing.Color.White
        Me.XrLabel26.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel26.BorderWidth = 2.0!
        Me.XrLabel26.Dpi = 254.0!
        Me.XrLabel26.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel26.ForeColor = System.Drawing.Color.White
        Me.XrLabel26.LocationFloat = New DevExpress.Utils.PointFloat(1170.968!, 119.0!)
        Me.XrLabel26.Name = "XrLabel26"
        Me.XrLabel26.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        Me.XrLabel26.SizeF = New System.Drawing.SizeF(239.0424!, 71.11996!)
        Me.XrLabel26.StyleName = "DetailCaption2"
        Me.XrLabel26.StylePriority.UseBackColor = False
        Me.XrLabel26.StylePriority.UseBorderColor = False
        Me.XrLabel26.StylePriority.UseBorders = False
        Me.XrLabel26.StylePriority.UseBorderWidth = False
        Me.XrLabel26.StylePriority.UseFont = False
        Me.XrLabel26.StylePriority.UseForeColor = False
        Me.XrLabel26.StylePriority.UsePadding = False
        Me.XrLabel26.StylePriority.UseTextAlignment = False
        Me.XrLabel26.Text = "مجموع العلاجات"
        Me.XrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'Detail4
        '
        Me.Detail4.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel29, Me.XrLabel28, Me.XrLabel27})
        Me.Detail4.Dpi = 254.0!
        Me.Detail4.HeightF = 82.8932!
        Me.Detail4.HierarchyPrintOptions.Indent = 50.8!
        Me.Detail4.Name = "Detail4"
        '
        'XrLabel29
        '
        Me.XrLabel29.Dpi = 254.0!
        Me.XrLabel29.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalTreatments]")})
        Me.XrLabel29.LocationFloat = New DevExpress.Utils.PointFloat(1170.968!, 10.34756!)
        Me.XrLabel29.Multiline = True
        Me.XrLabel29.Name = "XrLabel29"
        Me.XrLabel29.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel29.SizeF = New System.Drawing.SizeF(239.0424!, 58.42!)
        Me.XrLabel29.StylePriority.UseTextAlignment = False
        Me.XrLabel29.Text = "XrLabel29"
        Me.XrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel29.TextFormatString = "{0:#.00}"
        '
        'XrLabel28
        '
        Me.XrLabel28.Dpi = 254.0!
        Me.XrLabel28.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalPayments]")})
        Me.XrLabel28.LocationFloat = New DevExpress.Utils.PointFloat(840.2391!, 10.34756!)
        Me.XrLabel28.Multiline = True
        Me.XrLabel28.Name = "XrLabel28"
        Me.XrLabel28.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel28.SizeF = New System.Drawing.SizeF(239.0424!, 58.42!)
        Me.XrLabel28.StylePriority.UseTextAlignment = False
        Me.XrLabel28.Text = "XrLabel28"
        Me.XrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel28.TextFormatString = "{0:#.00}"
        '
        'XrLabel27
        '
        Me.XrLabel27.Dpi = 254.0!
        Me.XrLabel27.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Balance]")})
        Me.XrLabel27.LocationFloat = New DevExpress.Utils.PointFloat(490.9893!, 0!)
        Me.XrLabel27.Multiline = True
        Me.XrLabel27.Name = "XrLabel27"
        Me.XrLabel27.Padding = New DevExpress.XtraPrinting.PaddingInfo(5.0!, 5.0!, 0!, 0!, 254.0!)
        Me.XrLabel27.SizeF = New System.Drawing.SizeF(239.0424!, 58.42!)
        Me.XrLabel27.StylePriority.UseTextAlignment = False
        Me.XrLabel27.Text = "XrLabel27"
        Me.XrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel27.TextFormatString = "{0:#.00}"
        '
        'Title
        '
        Me.Title.BackColor = System.Drawing.Color.Transparent
        Me.Title.BorderColor = System.Drawing.Color.Black
        Me.Title.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.Title.BorderWidth = 1.0!
        Me.Title.Font = New DevExpress.Drawing.DXFont("Arial", 14.25!)
        Me.Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Title.Name = "Title"
        Me.Title.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        '
        'DetailCaption2
        '
        Me.DetailCaption2.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.DetailCaption2.BorderColor = System.Drawing.Color.White
        Me.DetailCaption2.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.DetailCaption2.BorderWidth = 2.0!
        Me.DetailCaption2.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.DetailCaption2.ForeColor = System.Drawing.Color.White
        Me.DetailCaption2.Name = "DetailCaption2"
        Me.DetailCaption2.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        Me.DetailCaption2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailData2
        '
        Me.DetailData2.BorderColor = System.Drawing.Color.Transparent
        Me.DetailData2.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.DetailData2.BorderWidth = 2.0!
        Me.DetailData2.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.DetailData2.ForeColor = System.Drawing.Color.Black
        Me.DetailData2.Name = "DetailData2"
        Me.DetailData2.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        Me.DetailData2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailData3_Odd
        '
        Me.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(231, Byte), Integer))
        Me.DetailData3_Odd.BorderColor = System.Drawing.Color.Transparent
        Me.DetailData3_Odd.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.DetailData3_Odd.BorderWidth = 1.0!
        Me.DetailData3_Odd.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.DetailData3_Odd.ForeColor = System.Drawing.Color.Black
        Me.DetailData3_Odd.Name = "DetailData3_Odd"
        Me.DetailData3_Odd.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        Me.DetailData3_Odd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'PageInfo
        '
        Me.PageInfo.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.PageInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.PageInfo.Name = "PageInfo"
        Me.PageInfo.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 0!, 0!, 254.0!)
        '
        'FinancialReport
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.Detail, Me.DetailReport, Me.DetailReport1, Me.DetailReport2, Me.DetailReport3})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.SqlDataSource1})
        Me.DataMember = "GetPatientFinancialReport"
        Me.DataSource = Me.SqlDataSource1
        Me.Dpi = 254.0!
        Me.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!)
        Me.Margins = New DevExpress.Drawing.DXMargins(98.0!, 101.0!, 254.0!, 254.0!)
        Me.PageHeightF = 2970.0!
        Me.PageWidthF = 2100.0!
        Me.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.parPatientID})
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.SnapGridSize = 25.0!
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.Title, Me.DetailCaption2, Me.DetailData2, Me.DetailData3_Odd, Me.PageInfo})
        Me.Version = "25.1"
        XrWatermark1.Id = "Watermark1"
        Me.Watermarks.AddRange(New DevExpress.XtraPrinting.Drawing.Watermark() {XrWatermark1})
        CType(Me.table3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.table5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents pageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents pageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents label1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents DetailReport As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents Detail1 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents SqlDataSource1 As DevExpress.DataAccess.Sql.SqlDataSource
    Friend WithEvents DetailReport1 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents table3 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell16 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Detail2 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents DetailReport2 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeader3 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents table5 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow5 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell26 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell27 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell28 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell29 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Detail3 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents DetailReport3 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeader4 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents Detail4 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents Title As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailCaption2 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData2 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData3_Odd As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents PageInfo As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel26 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel29 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel28 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel27 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrLabel31 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel30 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter1 As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrLabel33 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel32 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents parPatientID As DevExpress.XtraReports.Parameters.Parameter
End Class
