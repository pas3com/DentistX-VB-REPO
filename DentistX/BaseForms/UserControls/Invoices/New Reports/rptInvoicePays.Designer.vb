<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptInvoicePays
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
        Me.components = New System.ComponentModel.Container()
        Dim StoredProcQuery1 As DevExpress.DataAccess.Sql.StoredProcQuery = New DevExpress.DataAccess.Sql.StoredProcQuery()
        Dim QueryParameter1 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter2 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptInvoicePays))
        Dim XrWatermark1 As DevExpress.XtraReports.UI.XRWatermark = New DevExpress.XtraReports.UI.XRWatermark()
        Me.parInvoiceID = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parPatientID = New DevExpress.XtraReports.Parameters.Parameter()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.pageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.pageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.label1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.DetailReport = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail1 = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.SqlDataSource1 = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
        Me.DetailReport1 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail2 = New DevExpress.XtraReports.UI.DetailBand()
        Me.xrPanel2 = New DevExpress.XtraReports.UI.XRPanel()
        Me.XrLabel37 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel39 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel36 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel32 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel30 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel29 = New DevExpress.XtraReports.UI.XRLabel()
        Me.invoiceInfoTable = New DevExpress.XtraReports.UI.XRTable()
        Me.invoiceNumberRow = New DevExpress.XtraReports.UI.XRTableRow()
        Me.invoiceNumber = New DevExpress.XtraReports.UI.XRTableCell()
        Me.invoiceDateRow = New DevExpress.XtraReports.UI.XRTableRow()
        Me.invoiceDate = New DevExpress.XtraReports.UI.XRTableCell()
        Me.invoiceTotalTable = New DevExpress.XtraReports.UI.XRTable()
        Me.invoiceTotalTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.total2Caption = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.invoiceTotalTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.total2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.DetailReport2 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeader3 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.table7 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow7 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell55 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell57 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell58 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell59 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell60 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell61 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Detail3 = New DevExpress.XtraReports.UI.DetailBand()
        Me.table8 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow8 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell62 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell64 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell65 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell66 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell67 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell68 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel26 = New DevExpress.XtraReports.UI.XRLabel()
        Me.DetailReport3 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeader4 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel28 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel31 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel33 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Detail4 = New DevExpress.XtraReports.UI.DetailBand()
        Me.DetailReport4 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeader5 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel27 = New DevExpress.XtraReports.UI.XRLabel()
        Me.table9 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow9 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell69 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell70 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell71 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell72 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell73 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell74 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell75 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Detail5 = New DevExpress.XtraReports.UI.DetailBand()
        Me.table10 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow10 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell76 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell77 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell78 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell79 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell80 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell81 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell82 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Title = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailCaption2 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData2 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData3_Odd = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.PageInfo = New DevExpress.XtraReports.UI.XRControlStyle()
        CType(Me.invoiceInfoTable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invoiceTotalTable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.table7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.table8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.table9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.table10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'parInvoiceID
        '
        Me.parInvoiceID.Description = "InvoiceID"
        Me.parInvoiceID.Name = "parInvoiceID"
        Me.parInvoiceID.Type = GetType(Integer)
        Me.parInvoiceID.ValueInfo = "0"
        '
        'parPatientID
        '
        Me.parPatientID.Description = "PatientID"
        Me.parPatientID.Name = "parPatientID"
        Me.parPatientID.Type = GetType(Integer)
        Me.parPatientID.ValueInfo = "0"
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 25.4!
        Me.TopMargin.HeightF = 10.0!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.pageInfo1, Me.pageInfo2})
        Me.BottomMargin.Dpi = 25.4!
        Me.BottomMargin.HeightF = 10.0!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'pageInfo1
        '
        Me.pageInfo1.Dpi = 25.4!
        Me.pageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 4.0!)
        Me.pageInfo1.Name = "pageInfo1"
        Me.pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.pageInfo1.SizeF = New System.Drawing.SizeF(95.0!, 6.0!)
        Me.pageInfo1.StyleName = "PageInfo"
        '
        'pageInfo2
        '
        Me.pageInfo2.Dpi = 25.4!
        Me.pageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(95.0!, 4.0!)
        Me.pageInfo2.Name = "pageInfo2"
        Me.pageInfo2.SizeF = New System.Drawing.SizeF(95.0!, 6.0!)
        Me.pageInfo2.StyleName = "PageInfo"
        Me.pageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.pageInfo2.TextFormatString = "Page {0} of {1}"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel10, Me.XrLabel9, Me.XrLabel8, Me.XrLabel7, Me.XrLabel6, Me.XrLabel5, Me.XrLabel4, Me.XrLabel3, Me.XrLabel2, Me.XrLabel1, Me.XrPictureBox1, Me.label1})
        Me.ReportHeader.Dpi = 25.4!
        Me.ReportHeader.HeightF = 33.28583!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrLabel10
        '
        Me.XrLabel10.Dpi = 25.4!
        Me.XrLabel10.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Result1].[SpecialistEn]")})
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(34.21469!, 25.4!)
        Me.XrLabel10.Multiline = True
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(60.78531!, 5.842001!)
        Me.XrLabel10.Text = "XrLabel10"
        '
        'XrLabel9
        '
        Me.XrLabel9.Dpi = 25.4!
        Me.XrLabel9.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Result1].[Phone]")})
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(0.9340636!, 25.52345!)
        Me.XrLabel9.Multiline = True
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.XrLabel9.Text = "XrLabel9"
        '
        'XrLabel8
        '
        Me.XrLabel8.Dpi = 25.4!
        Me.XrLabel8.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Result1].[AddressEn]")})
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(0.9340636!, 19.68145!)
        Me.XrLabel8.Multiline = True
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.XrLabel8.Text = "XrLabel8"
        '
        'XrLabel7
        '
        Me.XrLabel7.Dpi = 25.4!
        Me.XrLabel7.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Result1].[DrNameEn]")})
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(0.9340636!, 13.83945!)
        Me.XrLabel7.Multiline = True
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.XrLabel7.Text = "XrLabel7"
        '
        'XrLabel6
        '
        Me.XrLabel6.Dpi = 25.4!
        Me.XrLabel6.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Result1].[ClinicNameEn]")})
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(0.9340636!, 7.997445!)
        Me.XrLabel6.Multiline = True
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.XrLabel6.Text = "XrLabel6"
        '
        'XrLabel5
        '
        Me.XrLabel5.Dpi = 25.4!
        Me.XrLabel5.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Result1].[Phone]")})
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(162.06!, 25.52345!)
        Me.XrLabel5.Multiline = True
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.XrLabel5.Text = "XrLabel5"
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 25.4!
        Me.XrLabel4.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Result1].[SpecialistAr]")})
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(95.0!, 25.4!)
        Me.XrLabel4.Multiline = True
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(60.78531!, 5.842001!)
        Me.XrLabel4.Text = "XrLabel4"
        '
        'XrLabel3
        '
        Me.XrLabel3.Dpi = 25.4!
        Me.XrLabel3.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Result1].[AddressAr]")})
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(162.06!, 19.68145!)
        Me.XrLabel3.Multiline = True
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.XrLabel3.Text = "XrLabel3"
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 25.4!
        Me.XrLabel2.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Result1].[DrNameAr]")})
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(162.06!, 13.83945!)
        Me.XrLabel2.Multiline = True
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.XrLabel2.Text = "XrLabel2"
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 25.4!
        Me.XrLabel1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Result1].[ClinicNameAr]")})
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(162.06!, 7.997445!)
        Me.XrLabel1.Multiline = True
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.XrLabel1.Text = "XrLabel1"
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.Dpi = 25.4!
        Me.XrPictureBox1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[Result1].[ClinicLogo]")})
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(82.3!, 7.997445!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(25.4!, 17.40256!)
        Me.XrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.Gainsboro
        Me.label1.Dpi = 25.4!
        Me.label1.Font = New DevExpress.Drawing.DXFont("Arial Black", 14.25!)
        Me.label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.label1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.label1.Name = "label1"
        Me.label1.SizeF = New System.Drawing.SizeF(190.0!, 7.997444!)
        Me.label1.StyleName = "Title"
        Me.label1.StylePriority.UseBackColor = False
        Me.label1.StylePriority.UseFont = False
        Me.label1.StylePriority.UseForeColor = False
        Me.label1.StylePriority.UseTextAlignment = False
        Me.label1.Text = "Treats Invoice"
        Me.label1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'Detail
        '
        Me.Detail.Dpi = 25.4!
        Me.Detail.HeightF = 0!
        Me.Detail.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        '
        'DetailReport
        '
        Me.DetailReport.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail1})
        Me.DetailReport.DataMember = "GetReport_Invoice.Result2"
        Me.DetailReport.DataSource = Me.SqlDataSource1
        Me.DetailReport.Dpi = 25.4!
        Me.DetailReport.Level = 0
        Me.DetailReport.Name = "DetailReport"
        '
        'Detail1
        '
        Me.Detail1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel25, Me.XrLabel24, Me.XrLabel23, Me.XrLabel22, Me.XrLabel21, Me.XrLabel20, Me.XrLabel19, Me.XrLabel18, Me.XrLabel12, Me.XrLabel13, Me.XrLabel14, Me.XrLabel15, Me.XrLabel16, Me.XrLabel17, Me.XrLabel11})
        Me.Detail1.Dpi = 25.4!
        Me.Detail1.HeightF = 35.89868!
        Me.Detail1.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail1.Name = "Detail1"
        '
        'XrLabel25
        '
        Me.XrLabel25.BackColor = System.Drawing.Color.WhiteSmoke
        Me.XrLabel25.Dpi = 25.4!
        Me.XrLabel25.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[GetReport_Invoice].[Result2].[Sex]")})
        Me.XrLabel25.Font = New DevExpress.Drawing.DXFont("Calibri", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel25.LocationFloat = New DevExpress.Utils.PointFloat(83.7829!, 11.63244!)
        Me.XrLabel25.Multiline = True
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel25.SizeF = New System.Drawing.SizeF(20.87413!, 5.842001!)
        Me.XrLabel25.StylePriority.UseBackColor = False
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.StylePriority.UseTextAlignment = False
        Me.XrLabel25.Text = "XrLabel25"
        Me.XrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel24
        '
        Me.XrLabel24.BackColor = System.Drawing.Color.WhiteSmoke
        Me.XrLabel24.Dpi = 25.4!
        Me.XrLabel24.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[GetReport_Invoice].[Result2].[PatientName]")})
        Me.XrLabel24.Font = New DevExpress.Drawing.DXFont("Calibri", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel24.LocationFloat = New DevExpress.Utils.PointFloat(101.6641!, 19.14445!)
        Me.XrLabel24.Multiline = True
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel24.SizeF = New System.Drawing.SizeF(61.95164!, 5.841999!)
        Me.XrLabel24.StylePriority.UseBackColor = False
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.StylePriority.UseTextAlignment = False
        Me.XrLabel24.Text = "XrLabel24"
        Me.XrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel23
        '
        Me.XrLabel23.BackColor = System.Drawing.Color.WhiteSmoke
        Me.XrLabel23.Dpi = 25.4!
        Me.XrLabel23.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[GetReport_Invoice].[Result2].[PatientNumber]")})
        Me.XrLabel23.Font = New DevExpress.Drawing.DXFont("Calibri", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(121.6962!, 26.88169!)
        Me.XrLabel23.Multiline = True
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(34.08906!, 5.841999!)
        Me.XrLabel23.StylePriority.UseBackColor = False
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.StylePriority.UseTextAlignment = False
        Me.XrLabel23.Text = "XrLabel23"
        Me.XrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel22
        '
        Me.XrLabel22.BackColor = System.Drawing.Color.WhiteSmoke
        Me.XrLabel22.Dpi = 25.4!
        Me.XrLabel22.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[GetReport_Invoice].[Result2].[Phone]")})
        Me.XrLabel22.Font = New DevExpress.Drawing.DXFont("Calibri", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(0.9340636!, 11.63244!)
        Me.XrLabel22.Multiline = True
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.XrLabel22.StylePriority.UseBackColor = False
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.StylePriority.UseTextAlignment = False
        Me.XrLabel22.Text = "XrLabel22"
        Me.XrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel21
        '
        Me.XrLabel21.BackColor = System.Drawing.Color.WhiteSmoke
        Me.XrLabel21.Dpi = 25.4!
        Me.XrLabel21.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[GetReport_Invoice].[Result2].[PatientID]")})
        Me.XrLabel21.Font = New DevExpress.Drawing.DXFont("Calibri", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(138.2157!, 11.63244!)
        Me.XrLabel21.Multiline = True
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.XrLabel21.StylePriority.UseBackColor = False
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.StylePriority.UseTextAlignment = False
        Me.XrLabel21.Text = "XrLabel21"
        Me.XrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel20
        '
        Me.XrLabel20.BackColor = System.Drawing.Color.WhiteSmoke
        Me.XrLabel20.Dpi = 25.4!
        Me.XrLabel20.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[GetReport_Invoice].[Result2].[Age]")})
        Me.XrLabel20.Font = New DevExpress.Drawing.DXFont("Calibri", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(45.46739!, 11.63244!)
        Me.XrLabel20.Multiline = True
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(16.52111!, 5.842001!)
        Me.XrLabel20.StylePriority.UseBackColor = False
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "XrLabel20"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel19
        '
        Me.XrLabel19.BackColor = System.Drawing.Color.WhiteSmoke
        Me.XrLabel19.Dpi = 25.4!
        Me.XrLabel19.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[GetReport_Invoice].[Result2].[Address]")})
        Me.XrLabel19.Font = New DevExpress.Drawing.DXFont("Calibri", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(5.753181!, 19.14445!)
        Me.XrLabel19.Multiline = True
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(51.59375!, 5.841999!)
        Me.XrLabel19.StylePriority.UseBackColor = False
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "XrLabel19"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel18
        '
        Me.XrLabel18.BorderColor = System.Drawing.Color.Black
        Me.XrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel18.BorderWidth = 1.0!
        Me.XrLabel18.Dpi = 25.4!
        Me.XrLabel18.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(157.0012!, 26.24669!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.2!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(29.69734!, 7.112003!)
        Me.XrLabel18.StylePriority.UseBackColor = False
        Me.XrLabel18.StylePriority.UseBorderColor = False
        Me.XrLabel18.StylePriority.UseBorders = False
        Me.XrLabel18.StylePriority.UseBorderWidth = False
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UseForeColor = False
        Me.XrLabel18.StylePriority.UsePadding = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = ":رقم ملف المريض"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel12
        '
        Me.XrLabel12.BorderColor = System.Drawing.Color.Black
        Me.XrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel12.BorderWidth = 1.0!
        Me.XrLabel12.Dpi = 25.4!
        Me.XrLabel12.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(163.6157!, 10.99744!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.2!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(23.08276!, 7.112!)
        Me.XrLabel12.StylePriority.UseBackColor = False
        Me.XrLabel12.StylePriority.UseBorderColor = False
        Me.XrLabel12.StylePriority.UseBorders = False
        Me.XrLabel12.StylePriority.UseBorderWidth = False
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseForeColor = False
        Me.XrLabel12.StylePriority.UsePadding = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = ":رقم المريض"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel13
        '
        Me.XrLabel13.BorderColor = System.Drawing.Color.Black
        Me.XrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel13.BorderWidth = 1.0!
        Me.XrLabel13.Dpi = 25.4!
        Me.XrLabel13.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(163.6157!, 18.50944!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.2!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(23.08276!, 7.112002!)
        Me.XrLabel13.StylePriority.UseBackColor = False
        Me.XrLabel13.StylePriority.UseBorderColor = False
        Me.XrLabel13.StylePriority.UseBorders = False
        Me.XrLabel13.StylePriority.UseBorderWidth = False
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseForeColor = False
        Me.XrLabel13.StylePriority.UsePadding = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = ":اسم المريض"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel14
        '
        Me.XrLabel14.BorderColor = System.Drawing.Color.Black
        Me.XrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel14.BorderWidth = 1.0!
        Me.XrLabel14.Dpi = 25.4!
        Me.XrLabel14.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(104.657!, 10.99744!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.2!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(17.03922!, 7.112001!)
        Me.XrLabel14.StylePriority.UseBackColor = False
        Me.XrLabel14.StylePriority.UseBorderColor = False
        Me.XrLabel14.StylePriority.UseBorders = False
        Me.XrLabel14.StylePriority.UseBorderWidth = False
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseForeColor = False
        Me.XrLabel14.StylePriority.UsePadding = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = ":الجنس"
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel15
        '
        Me.XrLabel15.BorderColor = System.Drawing.Color.Black
        Me.XrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel15.BorderWidth = 1.0!
        Me.XrLabel15.Dpi = 25.4!
        Me.XrLabel15.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(61.9885!, 10.99744!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.2!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(13.33506!, 7.112!)
        Me.XrLabel15.StylePriority.UseBackColor = False
        Me.XrLabel15.StylePriority.UseBorderColor = False
        Me.XrLabel15.StylePriority.UseBorders = False
        Me.XrLabel15.StylePriority.UseBorderWidth = False
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseForeColor = False
        Me.XrLabel15.StylePriority.UsePadding = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = ":العمر"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel16
        '
        Me.XrLabel16.BorderColor = System.Drawing.Color.Black
        Me.XrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel16.BorderWidth = 1.0!
        Me.XrLabel16.Dpi = 25.4!
        Me.XrLabel16.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(57.34693!, 18.50945!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.2!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(17.97663!, 7.112!)
        Me.XrLabel16.StylePriority.UseBackColor = False
        Me.XrLabel16.StylePriority.UseBorderColor = False
        Me.XrLabel16.StylePriority.UseBorders = False
        Me.XrLabel16.StylePriority.UseBorderWidth = False
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseForeColor = False
        Me.XrLabel16.StylePriority.UsePadding = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = ":العنوان"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel17
        '
        Me.XrLabel17.BorderColor = System.Drawing.Color.Black
        Me.XrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel17.BorderWidth = 1.0!
        Me.XrLabel17.Dpi = 25.4!
        Me.XrLabel17.Font = New DevExpress.Drawing.DXFont("Calibri", 13.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Underline), DevExpress.Drawing.DXFontStyle))
        Me.XrLabel17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(26.33406!, 10.99744!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.2!, 0.5!, 0!, 0!, 25.4!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(13.33506!, 7.112!)
        Me.XrLabel17.StylePriority.UseBackColor = False
        Me.XrLabel17.StylePriority.UseBorderColor = False
        Me.XrLabel17.StylePriority.UseBorders = False
        Me.XrLabel17.StylePriority.UseBorderWidth = False
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseForeColor = False
        Me.XrLabel17.StylePriority.UsePadding = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = ":الهاتف"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel11
        '
        Me.XrLabel11.BackColor = System.Drawing.Color.Gainsboro
        Me.XrLabel11.Dpi = 25.4!
        Me.XrLabel11.Font = New DevExpress.Drawing.DXFont("Arial Black", 14.25!)
        Me.XrLabel11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(0!, 3.0!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(190.0!, 7.997444!)
        Me.XrLabel11.StyleName = "Title"
        Me.XrLabel11.StylePriority.UseBackColor = False
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseForeColor = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "Patient Info"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'SqlDataSource1
        '
        Me.SqlDataSource1.ConnectionName = "DentistX.My.MySettings.DentistXConnectionString"
        Me.SqlDataSource1.Name = "SqlDataSource1"
        StoredProcQuery1.Name = "GetReport_Invoice"
        QueryParameter1.Name = "@InvoiceID"
        QueryParameter1.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter1.Value = New DevExpress.DataAccess.Expression("?parInvoiceID", GetType(Integer))
        QueryParameter2.Name = "@PatientID"
        QueryParameter2.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter2.Value = New DevExpress.DataAccess.Expression("?parPatientID", GetType(Integer))
        StoredProcQuery1.Parameters.AddRange(New DevExpress.DataAccess.Sql.QueryParameter() {QueryParameter1, QueryParameter2})
        StoredProcQuery1.StoredProcName = "GetReport_Invoice"
        Me.SqlDataSource1.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {StoredProcQuery1})
        Me.SqlDataSource1.ResultSchemaSerializable = resources.GetString("SqlDataSource1.ResultSchemaSerializable")
        '
        'DetailReport1
        '
        Me.DetailReport1.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail2})
        Me.DetailReport1.DataMember = "GetReport_Invoice.Result3"
        Me.DetailReport1.DataSource = Me.SqlDataSource1
        Me.DetailReport1.Dpi = 25.4!
        Me.DetailReport1.Level = 1
        Me.DetailReport1.Name = "DetailReport1"
        '
        'Detail2
        '
        Me.Detail2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrPanel2})
        Me.Detail2.Dpi = 25.4!
        Me.Detail2.HeightF = 44.11132!
        Me.Detail2.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail2.Name = "Detail2"
        '
        'xrPanel2
        '
        Me.xrPanel2.AnchorHorizontal = CType((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left Or DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right), DevExpress.XtraReports.UI.HorizontalAnchorStyles)
        Me.xrPanel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.xrPanel2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel37, Me.XrLabel39, Me.XrLabel36, Me.XrLabel32, Me.XrLabel30, Me.XrLabel29, Me.invoiceInfoTable, Me.invoiceTotalTable})
        Me.xrPanel2.Dpi = 25.4!
        Me.xrPanel2.LocationFloat = New DevExpress.Utils.PointFloat(0!, 6.445246!)
        Me.xrPanel2.Name = "xrPanel2"
        Me.xrPanel2.SizeF = New System.Drawing.SizeF(190.0!, 35.12608!)
        Me.xrPanel2.StylePriority.UseBackColor = False
        '
        'XrLabel37
        '
        Me.XrLabel37.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel37.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel37.BorderWidth = 2.0!
        Me.XrLabel37.Dpi = 25.4!
        Me.XrLabel37.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([InvoiceStatus] = 1, 'Issued'," & Global.Microsoft.VisualBasic.ChrW(10) & "    Iif([InvoiceStatus] = 2, 'Paid'," & Global.Microsoft.VisualBasic.ChrW(10) & "        I" &
                    "if([InvoiceStatus] = 3, 'Partially Paid'," & Global.Microsoft.VisualBasic.ChrW(10) & "            Iif([InvoiceStatus] = 4, '" &
                    "Completed', '')" & Global.Microsoft.VisualBasic.ChrW(10) & "        )" & Global.Microsoft.VisualBasic.ChrW(10) & "    )" & Global.Microsoft.VisualBasic.ChrW(10) & ")" & Global.Microsoft.VisualBasic.ChrW(10))})
        Me.XrLabel37.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.XrLabel37.ForeColor = System.Drawing.Color.Black
        Me.XrLabel37.LocationFloat = New DevExpress.Utils.PointFloat(101.6641!, 25.84451!)
        Me.XrLabel37.Name = "XrLabel37"
        Me.XrLabel37.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel37.SizeF = New System.Drawing.SizeF(21.60287!, 6.371166!)
        Me.XrLabel37.StyleName = "DetailData2"
        Me.XrLabel37.StylePriority.UseBorderColor = False
        Me.XrLabel37.StylePriority.UseBorders = False
        Me.XrLabel37.StylePriority.UseBorderWidth = False
        Me.XrLabel37.StylePriority.UseFont = False
        Me.XrLabel37.StylePriority.UseForeColor = False
        Me.XrLabel37.StylePriority.UsePadding = False
        Me.XrLabel37.StylePriority.UseTextAlignment = False
        Me.XrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel39
        '
        Me.XrLabel39.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel39.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel39.BorderWidth = 2.0!
        Me.XrLabel39.Dpi = 25.4!
        Me.XrLabel39.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InvoiceStatusText]")})
        Me.XrLabel39.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.XrLabel39.ForeColor = System.Drawing.Color.Black
        Me.XrLabel39.LocationFloat = New DevExpress.Utils.PointFloat(153.1939!, 25.84451!)
        Me.XrLabel39.Name = "XrLabel39"
        Me.XrLabel39.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel39.SizeF = New System.Drawing.SizeF(36.80609!, 6.371167!)
        Me.XrLabel39.StyleName = "DetailData2"
        Me.XrLabel39.StylePriority.UseBorderColor = False
        Me.XrLabel39.StylePriority.UseBorders = False
        Me.XrLabel39.StylePriority.UseBorderWidth = False
        Me.XrLabel39.StylePriority.UseFont = False
        Me.XrLabel39.StylePriority.UseForeColor = False
        Me.XrLabel39.StylePriority.UsePadding = False
        Me.XrLabel39.StylePriority.UseTextAlignment = False
        Me.XrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel36
        '
        Me.XrLabel36.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel36.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel36.BorderWidth = 2.0!
        Me.XrLabel36.Dpi = 25.4!
        Me.XrLabel36.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DueDate]")})
        Me.XrLabel36.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.XrLabel36.ForeColor = System.Drawing.Color.Black
        Me.XrLabel36.LocationFloat = New DevExpress.Utils.PointFloat(20.21334!, 25.47409!)
        Me.XrLabel36.Name = "XrLabel36"
        Me.XrLabel36.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel36.SizeF = New System.Drawing.SizeF(26.38425!, 6.371162!)
        Me.XrLabel36.StyleName = "DetailData2"
        Me.XrLabel36.StylePriority.UseBorderColor = False
        Me.XrLabel36.StylePriority.UseBorders = False
        Me.XrLabel36.StylePriority.UseBorderWidth = False
        Me.XrLabel36.StylePriority.UseFont = False
        Me.XrLabel36.StylePriority.UseForeColor = False
        Me.XrLabel36.StylePriority.UsePadding = False
        Me.XrLabel36.StylePriority.UseTextAlignment = False
        Me.XrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel36.TextFormatString = "{0:dd/MM/yyyy}"
        '
        'XrLabel32
        '
        Me.XrLabel32.BackColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.XrLabel32.BorderColor = System.Drawing.Color.White
        Me.XrLabel32.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel32.BorderWidth = 2.0!
        Me.XrLabel32.Dpi = 25.4!
        Me.XrLabel32.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel32.LocationFloat = New DevExpress.Utils.PointFloat(123.267!, 25.47409!)
        Me.XrLabel32.Name = "XrLabel32"
        Me.XrLabel32.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel32.SizeF = New System.Drawing.SizeF(29.92692!, 7.112001!)
        Me.XrLabel32.StyleName = "DetailCaption2"
        Me.XrLabel32.StylePriority.UseBackColor = False
        Me.XrLabel32.StylePriority.UseBorderColor = False
        Me.XrLabel32.StylePriority.UseBorders = False
        Me.XrLabel32.StylePriority.UseBorderWidth = False
        Me.XrLabel32.StylePriority.UseFont = False
        Me.XrLabel32.StylePriority.UseForeColor = False
        Me.XrLabel32.StylePriority.UsePadding = False
        Me.XrLabel32.StylePriority.UseTextAlignment = False
        Me.XrLabel32.Text = "Invoice Status Text"
        Me.XrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel30
        '
        Me.XrLabel30.BackColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.XrLabel30.BorderColor = System.Drawing.Color.White
        Me.XrLabel30.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel30.BorderWidth = 2.0!
        Me.XrLabel30.Dpi = 25.4!
        Me.XrLabel30.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel30.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel30.LocationFloat = New DevExpress.Utils.PointFloat(78.30908!, 25.47409!)
        Me.XrLabel30.Name = "XrLabel30"
        Me.XrLabel30.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel30.SizeF = New System.Drawing.SizeF(23.35501!, 7.112001!)
        Me.XrLabel30.StyleName = "DetailCaption2"
        Me.XrLabel30.StylePriority.UseBackColor = False
        Me.XrLabel30.StylePriority.UseBorderColor = False
        Me.XrLabel30.StylePriority.UseBorders = False
        Me.XrLabel30.StylePriority.UseBorderWidth = False
        Me.XrLabel30.StylePriority.UseFont = False
        Me.XrLabel30.StylePriority.UseForeColor = False
        Me.XrLabel30.StylePriority.UsePadding = False
        Me.XrLabel30.StylePriority.UseTextAlignment = False
        Me.XrLabel30.Text = "Invoice Status"
        Me.XrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel29
        '
        Me.XrLabel29.BackColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.XrLabel29.BorderColor = System.Drawing.Color.White
        Me.XrLabel29.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel29.BorderWidth = 2.0!
        Me.XrLabel29.Dpi = 25.4!
        Me.XrLabel29.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel29.LocationFloat = New DevExpress.Utils.PointFloat(0.4147397!, 25.10367!)
        Me.XrLabel29.Name = "XrLabel29"
        Me.XrLabel29.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel29.SizeF = New System.Drawing.SizeF(19.79855!, 7.112!)
        Me.XrLabel29.StyleName = "DetailCaption2"
        Me.XrLabel29.StylePriority.UseBackColor = False
        Me.XrLabel29.StylePriority.UseBorderColor = False
        Me.XrLabel29.StylePriority.UseBorders = False
        Me.XrLabel29.StylePriority.UseBorderWidth = False
        Me.XrLabel29.StylePriority.UseFont = False
        Me.XrLabel29.StylePriority.UseForeColor = False
        Me.XrLabel29.StylePriority.UsePadding = False
        Me.XrLabel29.StylePriority.UseTextAlignment = False
        Me.XrLabel29.Text = "Due Date"
        Me.XrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'invoiceInfoTable
        '
        Me.invoiceInfoTable.Dpi = 25.4!
        Me.invoiceInfoTable.LocationFloat = New DevExpress.Utils.PointFloat(6.350002!, 2.540002!)
        Me.invoiceInfoTable.Name = "invoiceInfoTable"
        Me.invoiceInfoTable.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.invoiceNumberRow, Me.invoiceDateRow})
        Me.invoiceInfoTable.SizeF = New System.Drawing.SizeF(64.76999!, 17.13992!)
        '
        'invoiceNumberRow
        '
        Me.invoiceNumberRow.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.invoiceNumber})
        Me.invoiceNumberRow.Dpi = 25.4!
        Me.invoiceNumberRow.Name = "invoiceNumberRow"
        Me.invoiceNumberRow.Weight = 0.76137257033012162R
        '
        'invoiceNumber
        '
        Me.invoiceNumber.CanGrow = False
        Me.invoiceNumber.Dpi = 25.4!
        Me.invoiceNumber.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InvoiceNumber]")})
        Me.invoiceNumber.Font = New DevExpress.Drawing.DXFont("Segoe UI", 19.0!)
        Me.invoiceNumber.Name = "invoiceNumber"
        Me.invoiceNumber.StylePriority.UseFont = False
        Me.invoiceNumber.StylePriority.UseTextAlignment = False
        Me.invoiceNumber.Text = "Invoice / 000001"
        Me.invoiceNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        Me.invoiceNumber.TextFormatString = "Invoice / {0}"
        Me.invoiceNumber.Weight = 0.912698488401723R
        '
        'invoiceDateRow
        '
        Me.invoiceDateRow.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.invoiceDate})
        Me.invoiceDateRow.Dpi = 25.4!
        Me.invoiceDateRow.Name = "invoiceDateRow"
        Me.invoiceDateRow.Weight = 0.72209877320573135R
        '
        'invoiceDate
        '
        Me.invoiceDate.CanGrow = False
        Me.invoiceDate.Dpi = 25.4!
        Me.invoiceDate.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InvoiceDate]")})
        Me.invoiceDate.Font = New DevExpress.Drawing.DXFont("Segoe UI", 12.0!)
        Me.invoiceDate.Name = "invoiceDate"
        Me.invoiceDate.StylePriority.UseFont = False
        Me.invoiceDate.StylePriority.UseTextAlignment = False
        Me.invoiceDate.Text = "InvoiceDate"
        Me.invoiceDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        Me.invoiceDate.TextFormatString = "{0:MMMM d, yyyy}"
        Me.invoiceDate.Weight = 0.912698488401723R
        '
        'invoiceTotalTable
        '
        Me.invoiceTotalTable.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right
        Me.invoiceTotalTable.Dpi = 25.4!
        Me.invoiceTotalTable.LocationFloat = New DevExpress.Utils.PointFloat(78.30907!, 0!)
        Me.invoiceTotalTable.Name = "invoiceTotalTable"
        Me.invoiceTotalTable.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.invoiceTotalTableRow1, Me.invoiceTotalTableRow2})
        Me.invoiceTotalTable.SizeF = New System.Drawing.SizeF(111.6909!, 23.97125!)
        '
        'invoiceTotalTableRow1
        '
        Me.invoiceTotalTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.total2Caption, Me.XrTableCell1})
        Me.invoiceTotalTableRow1.Dpi = 25.4!
        Me.invoiceTotalTableRow1.Name = "invoiceTotalTableRow1"
        Me.invoiceTotalTableRow1.Weight = 0.41275249766448796R
        '
        'total2Caption
        '
        Me.total2Caption.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.total2Caption.Dpi = 25.4!
        Me.total2Caption.Font = New DevExpress.Drawing.DXFont("Segoe UI", 10.5!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.total2Caption.ForeColor = System.Drawing.Color.White
        Me.total2Caption.Name = "total2Caption"
        Me.total2Caption.Padding = New DevExpress.XtraPrinting.PaddingInfo(2.54!, 0!, 2.54!, 0!, 25.4!)
        Me.total2Caption.StylePriority.UseBackColor = False
        Me.total2Caption.StylePriority.UseFont = False
        Me.total2Caption.StylePriority.UseForeColor = False
        Me.total2Caption.StylePriority.UsePadding = False
        Me.total2Caption.StylePriority.UseTextAlignment = False
        Me.total2Caption.Text = "TOTAL"
        Me.total2Caption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        Me.total2Caption.Weight = 1.2528791205526308R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.XrTableCell1.Dpi = 25.4!
        Me.XrTableCell1.Font = New DevExpress.Drawing.DXFont("Segoe UI", 10.5!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrTableCell1.ForeColor = System.Drawing.Color.White
        Me.XrTableCell1.Multiline = True
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2.54!, 0!, 2.54!, 0!, 25.4!)
        Me.XrTableCell1.StylePriority.UseBackColor = False
        Me.XrTableCell1.StylePriority.UseFont = False
        Me.XrTableCell1.StylePriority.UseForeColor = False
        Me.XrTableCell1.StylePriority.UsePadding = False
        Me.XrTableCell1.StylePriority.UseTextAlignment = False
        Me.XrTableCell1.Text = "Balance"
        Me.XrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        Me.XrTableCell1.Weight = 0.74712087944736916R
        '
        'invoiceTotalTableRow2
        '
        Me.invoiceTotalTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.total2, Me.XrTableCell2})
        Me.invoiceTotalTableRow2.Dpi = 25.4!
        Me.invoiceTotalTableRow2.Name = "invoiceTotalTableRow2"
        Me.invoiceTotalTableRow2.Weight = 1.3210798682220846R
        '
        'total2
        '
        Me.total2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.total2.Dpi = 25.4!
        Me.total2.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalAmount]")})
        Me.total2.Font = New DevExpress.Drawing.DXFont("Segoe UI", 40.0!)
        Me.total2.ForeColor = System.Drawing.Color.White
        Me.total2.Name = "total2"
        Me.total2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0!, 2.54!, 0!, 1.27!, 25.4!)
        Me.total2.StylePriority.UseBackColor = False
        Me.total2.StylePriority.UseFont = False
        Me.total2.StylePriority.UseForeColor = False
        Me.total2.StylePriority.UsePadding = False
        Me.total2.StylePriority.UseTextAlignment = False
        Me.total2.Text = "$0.00"
        Me.total2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight
        Me.total2.TextFormatString = "{0:$0.00}"
        Me.total2.Weight = 1.2528791205526308R
        '
        'XrTableCell2
        '
        Me.XrTableCell2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.XrTableCell2.Dpi = 25.4!
        Me.XrTableCell2.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BalanceDue]")})
        Me.XrTableCell2.Font = New DevExpress.Drawing.DXFont("Segoe UI", 25.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrTableCell2.ForeColor = System.Drawing.Color.Red
        Me.XrTableCell2.Multiline = True
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0!, 2.54!, 0!, 1.27!, 25.4!)
        Me.XrTableCell2.StylePriority.UseBackColor = False
        Me.XrTableCell2.StylePriority.UseFont = False
        Me.XrTableCell2.StylePriority.UseForeColor = False
        Me.XrTableCell2.StylePriority.UsePadding = False
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        Me.XrTableCell2.Text = "XrTableCell2"
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight
        Me.XrTableCell2.TextFormatString = "{0:N0}"
        Me.XrTableCell2.Weight = 0.74712087944736916R
        '
        'DetailReport2
        '
        Me.DetailReport2.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader3, Me.Detail3, Me.GroupHeader1})
        Me.DetailReport2.DataMember = "GetReport_Invoice.Result4"
        Me.DetailReport2.DataSource = Me.SqlDataSource1
        Me.DetailReport2.Dpi = 25.4!
        Me.DetailReport2.Level = 2
        Me.DetailReport2.Name = "DetailReport2"
        '
        'GroupHeader3
        '
        Me.GroupHeader3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.table7})
        Me.GroupHeader3.Dpi = 25.4!
        Me.GroupHeader3.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader3.HeightF = 10.81617!
        Me.GroupHeader3.Name = "GroupHeader3"
        '
        'table7
        '
        Me.table7.Dpi = 25.4!
        Me.table7.LocationFloat = New DevExpress.Utils.PointFloat(0.00004239082!, 1.164173!)
        Me.table7.Name = "table7"
        Me.table7.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow7})
        Me.table7.SizeF = New System.Drawing.SizeF(190.0!, 7.112!)
        '
        'tableRow7
        '
        Me.tableRow7.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell55, Me.tableCell57, Me.tableCell58, Me.tableCell59, Me.tableCell60, Me.tableCell61})
        Me.tableRow7.Dpi = 25.4!
        Me.tableRow7.Name = "tableRow7"
        Me.tableRow7.Weight = 1.0R
        '
        'tableCell55
        '
        Me.tableCell55.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.tableCell55.Dpi = 25.4!
        Me.tableCell55.Name = "tableCell55"
        Me.tableCell55.StyleName = "DetailCaption2"
        Me.tableCell55.StylePriority.UseBorders = False
        Me.tableCell55.StylePriority.UseTextAlignment = False
        Me.tableCell55.Text = "Number"
        Me.tableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell55.Weight = 0.091929066144198213R
        '
        'tableCell57
        '
        Me.tableCell57.Dpi = 25.4!
        Me.tableCell57.Name = "tableCell57"
        Me.tableCell57.StyleName = "DetailCaption2"
        Me.tableCell57.StylePriority.UseTextAlignment = False
        Me.tableCell57.Text = "Item Description"
        Me.tableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell57.Weight = 0.5022519972037861R
        '
        'tableCell58
        '
        Me.tableCell58.Dpi = 25.4!
        Me.tableCell58.Name = "tableCell58"
        Me.tableCell58.StyleName = "DetailCaption2"
        Me.tableCell58.StylePriority.UseTextAlignment = False
        Me.tableCell58.Text = "Treatment Date"
        Me.tableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell58.Weight = 0.13628359333831777R
        '
        'tableCell59
        '
        Me.tableCell59.Dpi = 25.4!
        Me.tableCell59.Name = "tableCell59"
        Me.tableCell59.StyleName = "DetailCaption2"
        Me.tableCell59.StylePriority.UseTextAlignment = False
        Me.tableCell59.Text = "Price"
        Me.tableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell59.Weight = 0.089458104109817665R
        '
        'tableCell60
        '
        Me.tableCell60.Dpi = 25.4!
        Me.tableCell60.Name = "tableCell60"
        Me.tableCell60.StyleName = "DetailCaption2"
        Me.tableCell60.StylePriority.UseTextAlignment = False
        Me.tableCell60.Text = "Discount"
        Me.tableCell60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell60.Weight = 0.086770388892787625R
        '
        'tableCell61
        '
        Me.tableCell61.Dpi = 25.4!
        Me.tableCell61.Name = "tableCell61"
        Me.tableCell61.StyleName = "DetailCaption2"
        Me.tableCell61.StylePriority.UseTextAlignment = False
        Me.tableCell61.Text = "Line Total"
        Me.tableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell61.Weight = 0.093306852574045848R
        '
        'Detail3
        '
        Me.Detail3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.table8})
        Me.Detail3.Dpi = 25.4!
        Me.Detail3.HeightF = 9.143999!
        Me.Detail3.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail3.Name = "Detail3"
        '
        'table8
        '
        Me.table8.Dpi = 25.4!
        Me.table8.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.table8.Name = "table8"
        Me.table8.OddStyleName = "DetailData3_Odd"
        Me.table8.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow8})
        Me.table8.SizeF = New System.Drawing.SizeF(190.0!, 6.371167!)
        '
        'tableRow8
        '
        Me.tableRow8.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell62, Me.tableCell64, Me.tableCell65, Me.tableCell66, Me.tableCell67, Me.tableCell68})
        Me.tableRow8.Dpi = 25.4!
        Me.tableRow8.Name = "tableRow8"
        Me.tableRow8.Weight = 11.039999351201134R
        '
        'tableCell62
        '
        Me.tableCell62.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.tableCell62.Dpi = 25.4!
        Me.tableCell62.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RowNumber]")})
        Me.tableCell62.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell62.Name = "tableCell62"
        Me.tableCell62.StyleName = "DetailData2"
        Me.tableCell62.StylePriority.UseBorders = False
        Me.tableCell62.StylePriority.UseFont = False
        Me.tableCell62.StylePriority.UseTextAlignment = False
        Me.tableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell62.Weight = 0.09192904360648757R
        '
        'tableCell64
        '
        Me.tableCell64.Dpi = 25.4!
        Me.tableCell64.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ItemDescription]")})
        Me.tableCell64.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell64.Name = "tableCell64"
        Me.tableCell64.StyleName = "DetailData2"
        Me.tableCell64.StylePriority.UseFont = False
        Me.tableCell64.StylePriority.UseTextAlignment = False
        Me.tableCell64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell64.Weight = 0.50225208430790658R
        '
        'tableCell65
        '
        Me.tableCell65.Dpi = 25.4!
        Me.tableCell65.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TreatmentDate]")})
        Me.tableCell65.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell65.Name = "tableCell65"
        Me.tableCell65.StyleName = "DetailData2"
        Me.tableCell65.StylePriority.UseFont = False
        Me.tableCell65.StylePriority.UseTextAlignment = False
        Me.tableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell65.TextFormatString = "{0:dd/MM/yyyy}"
        Me.tableCell65.Weight = 0.13628353224276879R
        '
        'tableCell66
        '
        Me.tableCell66.Dpi = 25.4!
        Me.tableCell66.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UnitPrice]")})
        Me.tableCell66.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell66.Name = "tableCell66"
        Me.tableCell66.StyleName = "DetailData2"
        Me.tableCell66.StylePriority.UseFont = False
        Me.tableCell66.StylePriority.UseTextAlignment = False
        Me.tableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell66.TextFormatString = "{0:N0}"
        Me.tableCell66.Weight = 0.089458033132764625R
        '
        'tableCell67
        '
        Me.tableCell67.Dpi = 25.4!
        Me.tableCell67.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Discount]")})
        Me.tableCell67.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell67.Name = "tableCell67"
        Me.tableCell67.StyleName = "DetailData2"
        Me.tableCell67.StylePriority.UseFont = False
        Me.tableCell67.StylePriority.UseTextAlignment = False
        Me.tableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell67.TextFormatString = "{0:N0}"
        Me.tableCell67.Weight = 0.086770478651094857R
        '
        'tableCell68
        '
        Me.tableCell68.Dpi = 25.4!
        Me.tableCell68.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[LineTotal]")})
        Me.tableCell68.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell68.Name = "tableCell68"
        Me.tableCell68.StyleName = "DetailData2"
        Me.tableCell68.StylePriority.UseFont = False
        Me.tableCell68.StylePriority.UseTextAlignment = False
        Me.tableCell68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell68.TextFormatString = "{0:N0}"
        Me.tableCell68.Weight = 0.093306932236046719R
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel26})
        Me.GroupHeader1.Dpi = 25.4!
        Me.GroupHeader1.HeightF = 17.72708!
        Me.GroupHeader1.Level = 1
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'XrLabel26
        '
        Me.XrLabel26.BackColor = System.Drawing.Color.Lime
        Me.XrLabel26.BorderColor = System.Drawing.Color.White
        Me.XrLabel26.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel26.BorderWidth = 2.0!
        Me.XrLabel26.Dpi = 25.4!
        Me.XrLabel26.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel26.LocationFloat = New DevExpress.Utils.PointFloat(0!, 8.572506!)
        Me.XrLabel26.Name = "XrLabel26"
        Me.XrLabel26.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel26.SizeF = New System.Drawing.SizeF(190.0!, 7.112001!)
        Me.XrLabel26.StyleName = "DetailCaption2"
        Me.XrLabel26.StylePriority.UseBackColor = False
        Me.XrLabel26.StylePriority.UseBorderColor = False
        Me.XrLabel26.StylePriority.UseBorders = False
        Me.XrLabel26.StylePriority.UseBorderWidth = False
        Me.XrLabel26.StylePriority.UseFont = False
        Me.XrLabel26.StylePriority.UseForeColor = False
        Me.XrLabel26.StylePriority.UsePadding = False
        Me.XrLabel26.StylePriority.UseTextAlignment = False
        Me.XrLabel26.Text = "Invoice Details"
        Me.XrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'DetailReport3
        '
        Me.DetailReport3.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader4, Me.Detail4})
        Me.DetailReport3.DataMember = "GetReport_Invoice.Result4"
        Me.DetailReport3.DataSource = Me.SqlDataSource1
        Me.DetailReport3.Dpi = 25.4!
        Me.DetailReport3.Level = 3
        Me.DetailReport3.Name = "DetailReport3"
        '
        'GroupHeader4
        '
        Me.GroupHeader4.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel28, Me.XrLabel31, Me.XrLabel33})
        Me.GroupHeader4.Dpi = 25.4!
        Me.GroupHeader4.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader4.HeightF = 7.112!
        Me.GroupHeader4.Name = "GroupHeader4"
        '
        'XrLabel28
        '
        Me.XrLabel28.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel28.Dpi = 25.4!
        Me.XrLabel28.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Sum([LineTotal])")})
        Me.XrLabel28.LocationFloat = New DevExpress.Utils.PointFloat(171.0558!, 0!)
        Me.XrLabel28.Multiline = True
        Me.XrLabel28.Name = "XrLabel28"
        Me.XrLabel28.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel28.SizeF = New System.Drawing.SizeF(16.74402!, 5.842!)
        Me.XrLabel28.StylePriority.UseBorders = False
        Me.XrLabel28.StylePriority.UseTextAlignment = False
        Me.XrLabel28.Text = "XrLabel28"
        Me.XrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel28.TextFormatString = "{0:N0}"
        '
        'XrLabel31
        '
        Me.XrLabel31.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel31.Dpi = 25.4!
        Me.XrLabel31.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sum([Discount])")})
        Me.XrLabel31.LocationFloat = New DevExpress.Utils.PointFloat(155.7853!, 0!)
        Me.XrLabel31.Multiline = True
        Me.XrLabel31.Name = "XrLabel31"
        Me.XrLabel31.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel31.SizeF = New System.Drawing.SizeF(15.27048!, 5.842!)
        Me.XrLabel31.StylePriority.UseBorders = False
        Me.XrLabel31.StylePriority.UseTextAlignment = False
        Me.XrLabel31.Text = "XrLabel28"
        Me.XrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel31.TextFormatString = "{0:N0}"
        '
        'XrLabel33
        '
        Me.XrLabel33.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel33.Dpi = 25.4!
        Me.XrLabel33.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sum([UnitPrice])")})
        Me.XrLabel33.LocationFloat = New DevExpress.Utils.PointFloat(138.7883!, 0!)
        Me.XrLabel33.Multiline = True
        Me.XrLabel33.Name = "XrLabel33"
        Me.XrLabel33.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel33.SizeF = New System.Drawing.SizeF(16.99702!, 5.842!)
        Me.XrLabel33.StylePriority.UseBorders = False
        Me.XrLabel33.StylePriority.UseTextAlignment = False
        Me.XrLabel33.Text = "XrLabel28"
        Me.XrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel33.TextFormatString = "{0:N0}"
        '
        'Detail4
        '
        Me.Detail4.Dpi = 25.4!
        Me.Detail4.HeightF = 6.371167!
        Me.Detail4.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail4.Name = "Detail4"
        '
        'DetailReport4
        '
        Me.DetailReport4.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader5, Me.Detail5})
        Me.DetailReport4.DataMember = "GetReport_Invoice.Result5"
        Me.DetailReport4.DataSource = Me.SqlDataSource1
        Me.DetailReport4.Dpi = 25.4!
        Me.DetailReport4.Level = 4
        Me.DetailReport4.Name = "DetailReport4"
        '
        'GroupHeader5
        '
        Me.GroupHeader5.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel27, Me.table9})
        Me.GroupHeader5.Dpi = 25.4!
        Me.GroupHeader5.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader5.HeightF = 14.82723!
        Me.GroupHeader5.Name = "GroupHeader5"
        '
        'XrLabel27
        '
        Me.XrLabel27.BackColor = System.Drawing.Color.Cyan
        Me.XrLabel27.BorderColor = System.Drawing.Color.White
        Me.XrLabel27.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel27.BorderWidth = 2.0!
        Me.XrLabel27.Dpi = 25.4!
        Me.XrLabel27.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel27.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0.6052256!)
        Me.XrLabel27.Name = "XrLabel27"
        Me.XrLabel27.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel27.SizeF = New System.Drawing.SizeF(190.0!, 7.112001!)
        Me.XrLabel27.StyleName = "DetailCaption2"
        Me.XrLabel27.StylePriority.UseBackColor = False
        Me.XrLabel27.StylePriority.UseBorderColor = False
        Me.XrLabel27.StylePriority.UseBorders = False
        Me.XrLabel27.StylePriority.UseBorderWidth = False
        Me.XrLabel27.StylePriority.UseFont = False
        Me.XrLabel27.StylePriority.UseForeColor = False
        Me.XrLabel27.StylePriority.UsePadding = False
        Me.XrLabel27.StylePriority.UseTextAlignment = False
        Me.XrLabel27.Text = "Payments Details"
        Me.XrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'table9
        '
        Me.table9.Dpi = 25.4!
        Me.table9.LocationFloat = New DevExpress.Utils.PointFloat(0!, 7.717227!)
        Me.table9.Name = "table9"
        Me.table9.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow9})
        Me.table9.SizeF = New System.Drawing.SizeF(190.0!, 7.11!)
        '
        'tableRow9
        '
        Me.tableRow9.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell69, Me.tableCell70, Me.tableCell71, Me.tableCell72, Me.tableCell73, Me.tableCell74, Me.tableCell75})
        Me.tableRow9.Dpi = 25.4!
        Me.tableRow9.Name = "tableRow9"
        Me.tableRow9.Weight = 1.0R
        '
        'tableCell69
        '
        Me.tableCell69.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.tableCell69.Dpi = 25.4!
        Me.tableCell69.Name = "tableCell69"
        Me.tableCell69.StyleName = "DetailCaption2"
        Me.tableCell69.StylePriority.UseBorders = False
        Me.tableCell69.StylePriority.UseTextAlignment = False
        Me.tableCell69.Text = "Invoice ID"
        Me.tableCell69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell69.Weight = 0.1386003293489155R
        '
        'tableCell70
        '
        Me.tableCell70.Dpi = 25.4!
        Me.tableCell70.Name = "tableCell70"
        Me.tableCell70.StyleName = "DetailCaption2"
        Me.tableCell70.Text = "Pay Date"
        Me.tableCell70.Weight = 0.12698135375976563R
        '
        'tableCell71
        '
        Me.tableCell71.Dpi = 25.4!
        Me.tableCell71.Name = "tableCell71"
        Me.tableCell71.StyleName = "DetailCaption2"
        Me.tableCell71.Text = "Pay Type"
        Me.tableCell71.Weight = 0.13085813020405015R
        '
        'tableCell72
        '
        Me.tableCell72.Dpi = 25.4!
        Me.tableCell72.Name = "tableCell72"
        Me.tableCell72.StyleName = "DetailCaption2"
        Me.tableCell72.StylePriority.UseTextAlignment = False
        Me.tableCell72.Text = "Pay Value"
        Me.tableCell72.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell72.Weight = 0.13863443073473478R
        '
        'tableCell73
        '
        Me.tableCell73.Dpi = 25.4!
        Me.tableCell73.Name = "tableCell73"
        Me.tableCell73.StyleName = "DetailCaption2"
        Me.tableCell73.Text = "Chq Number"
        Me.tableCell73.Weight = 0.16831849750719571R
        '
        'tableCell74
        '
        Me.tableCell74.Dpi = 25.4!
        Me.tableCell74.Name = "tableCell74"
        Me.tableCell74.StyleName = "DetailCaption2"
        Me.tableCell74.Text = "Payment Status"
        Me.tableCell74.Weight = 0.20330043591951069R
        '
        'tableCell75
        '
        Me.tableCell75.Dpi = 25.4!
        Me.tableCell75.Name = "tableCell75"
        Me.tableCell75.StyleName = "DetailCaption2"
        Me.tableCell75.Text = "Notes"
        Me.tableCell75.Weight = 0.0933068225258275R
        '
        'Detail5
        '
        Me.Detail5.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.table10})
        Me.Detail5.Dpi = 25.4!
        Me.Detail5.HeightF = 7.016739!
        Me.Detail5.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail5.Name = "Detail5"
        '
        'table10
        '
        Me.table10.Dpi = 25.4!
        Me.table10.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.table10.Name = "table10"
        Me.table10.OddStyleName = "DetailData3_Odd"
        Me.table10.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow10})
        Me.table10.SizeF = New System.Drawing.SizeF(190.0!, 6.371167!)
        '
        'tableRow10
        '
        Me.tableRow10.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell76, Me.tableCell77, Me.tableCell78, Me.tableCell79, Me.tableCell80, Me.tableCell81, Me.tableCell82})
        Me.tableRow10.Dpi = 25.4!
        Me.tableRow10.Name = "tableRow10"
        Me.tableRow10.Weight = 11.039999351201134R
        '
        'tableCell76
        '
        Me.tableCell76.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.tableCell76.Dpi = 25.4!
        Me.tableCell76.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InvoiceID]")})
        Me.tableCell76.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell76.Name = "tableCell76"
        Me.tableCell76.StyleName = "DetailData2"
        Me.tableCell76.StylePriority.UseBorders = False
        Me.tableCell76.StylePriority.UseFont = False
        Me.tableCell76.StylePriority.UseTextAlignment = False
        Me.tableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell76.Weight = 0.1386003293489155R
        '
        'tableCell77
        '
        Me.tableCell77.Dpi = 25.4!
        Me.tableCell77.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PayDate]")})
        Me.tableCell77.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell77.Name = "tableCell77"
        Me.tableCell77.StyleName = "DetailData2"
        Me.tableCell77.StylePriority.UseFont = False
        Me.tableCell77.TextFormatString = "{0:dd/MM/yyyy}"
        Me.tableCell77.Weight = 0.12698135375976563R
        '
        'tableCell78
        '
        Me.tableCell78.Dpi = 25.4!
        Me.tableCell78.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PayType]")})
        Me.tableCell78.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell78.Name = "tableCell78"
        Me.tableCell78.StyleName = "DetailData2"
        Me.tableCell78.StylePriority.UseFont = False
        Me.tableCell78.Weight = 0.13085813020405015R
        '
        'tableCell79
        '
        Me.tableCell79.Dpi = 25.4!
        Me.tableCell79.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PayValue]")})
        Me.tableCell79.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell79.Name = "tableCell79"
        Me.tableCell79.StyleName = "DetailData2"
        Me.tableCell79.StylePriority.UseFont = False
        Me.tableCell79.StylePriority.UseTextAlignment = False
        Me.tableCell79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell79.TextFormatString = "{0:C2}"
        Me.tableCell79.Weight = 0.13863443073473478R
        '
        'tableCell80
        '
        Me.tableCell80.Dpi = 25.4!
        Me.tableCell80.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ChqNumber]")})
        Me.tableCell80.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell80.Name = "tableCell80"
        Me.tableCell80.StyleName = "DetailData2"
        Me.tableCell80.StylePriority.UseFont = False
        Me.tableCell80.Weight = 0.16831849750719571R
        '
        'tableCell81
        '
        Me.tableCell81.Dpi = 25.4!
        Me.tableCell81.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PaymentStatus]")})
        Me.tableCell81.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell81.Name = "tableCell81"
        Me.tableCell81.StyleName = "DetailData2"
        Me.tableCell81.StylePriority.UseFont = False
        Me.tableCell81.Weight = 0.20330043591951069R
        '
        'tableCell82
        '
        Me.tableCell82.Dpi = 25.4!
        Me.tableCell82.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Notes]")})
        Me.tableCell82.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell82.Name = "tableCell82"
        Me.tableCell82.StyleName = "DetailData2"
        Me.tableCell82.StylePriority.UseFont = False
        Me.tableCell82.Weight = 0.093306852641858548R
        '
        'Title
        '
        Me.Title.BackColor = System.Drawing.Color.Transparent
        Me.Title.BorderColor = System.Drawing.Color.Black
        Me.Title.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.Title.BorderWidth = 1.0!
        Me.Title.Font = New DevExpress.Drawing.DXFont("Arial", 14.25!)
        Me.Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Title.Name = "Title"
        Me.Title.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        '
        'DetailCaption2
        '
        Me.DetailCaption2.BackColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.DetailCaption2.BorderColor = System.Drawing.Color.White
        Me.DetailCaption2.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.DetailCaption2.BorderWidth = 2.0!
        Me.DetailCaption2.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.DetailCaption2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.DetailCaption2.Name = "DetailCaption2"
        Me.DetailCaption2.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
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
        Me.DetailData2.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.DetailData2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailData3_Odd
        '
        Me.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.DetailData3_Odd.BorderColor = System.Drawing.Color.Transparent
        Me.DetailData3_Odd.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.DetailData3_Odd.BorderWidth = 1.0!
        Me.DetailData3_Odd.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.DetailData3_Odd.ForeColor = System.Drawing.Color.Black
        Me.DetailData3_Odd.Name = "DetailData3_Odd"
        Me.DetailData3_Odd.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.DetailData3_Odd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'PageInfo
        '
        Me.PageInfo.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.PageInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.PageInfo.Name = "PageInfo"
        Me.PageInfo.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        '
        'rptInvoicePays
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.Detail, Me.DetailReport, Me.DetailReport1, Me.DetailReport2, Me.DetailReport3, Me.DetailReport4})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.SqlDataSource1})
        Me.DataMember = "GetReport_Invoice"
        Me.DataSource = Me.SqlDataSource1
        Me.Dpi = 25.4!
        Me.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!)
        Me.Margins = New DevExpress.Drawing.DXMargins(10.0!, 10.0!, 10.0!, 10.0!)
        Me.PageHeightF = 297.0!
        Me.PageWidthF = 210.0!
        Me.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.parInvoiceID, Me.parPatientID})
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.Millimeters
        Me.SnapGridSize = 2.5!
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.Title, Me.DetailCaption2, Me.DetailData2, Me.DetailData3_Odd, Me.PageInfo})
        Me.Version = "25.1"
        XrWatermark1.Id = "Watermark1"
        Me.Watermarks.AddRange(New DevExpress.XtraPrinting.Drawing.Watermark() {XrWatermark1})
        CType(Me.invoiceInfoTable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invoiceTotalTable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.table7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.table8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.table9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.table10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents parInvoiceID As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents parPatientID As DevExpress.XtraReports.Parameters.Parameter
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
    Friend WithEvents Detail2 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents DetailReport2 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeader3 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents Detail3 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents DetailReport3 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeader4 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents table7 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow7 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell55 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell57 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell58 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell59 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell60 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell61 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Detail4 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents table8 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow8 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell62 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell64 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell65 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell66 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell67 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell68 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents DetailReport4 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeader5 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents table9 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow9 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell69 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell70 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell71 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell72 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell73 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell74 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell75 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Detail5 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents table10 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow10 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell76 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell77 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell78 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell79 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell80 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell81 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell82 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Title As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailCaption2 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData2 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData3_Odd As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents PageInfo As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrPanel2 As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents invoiceInfoTable As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents invoiceNumberRow As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents invoiceNumber As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents invoiceDateRow As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents invoiceDate As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents invoiceTotalTable As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents invoiceTotalTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents total2Caption As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents invoiceTotalTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents total2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel37 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel39 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel36 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel32 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel30 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel29 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel26 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel27 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel33 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel31 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel28 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
End Class
