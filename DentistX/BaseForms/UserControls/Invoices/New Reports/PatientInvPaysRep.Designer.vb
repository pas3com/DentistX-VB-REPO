<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class PatientInvPaysRep
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
        Dim QueryParameter2 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PatientInvPaysRep))
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrWatermark1 As DevExpress.XtraReports.UI.XRWatermark = New DevExpress.XtraReports.UI.XRWatermark()
        Me.parInvoiceID = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parPatientID = New DevExpress.XtraReports.Parameters.Parameter()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.pageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.pageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.label1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.clinicDetail = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.Detail1 = New DevExpress.XtraReports.UI.DetailBand()
        Me.clinicPanel = New DevExpress.XtraReports.UI.XRPanel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.pictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.SqlDataSource1 = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
        Me.DetailReport1 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel29 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Detail2 = New DevExpress.XtraReports.UI.DetailBand()
        Me.patientPanel = New DevExpress.XtraReports.UI.XRPanel()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel26 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel28 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel27 = New DevExpress.XtraReports.UI.XRLabel()
        Me.DetailReport2 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeader3 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.invHeaderPanel = New DevExpress.XtraReports.UI.XRPanel()
        Me.XrLabel48 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel49 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel50 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel51 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel52 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel53 = New DevExpress.XtraReports.UI.XRLabel()
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
        Me.Detail3 = New DevExpress.XtraReports.UI.DetailBand()
        Me.DetailReport3 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeader4 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel30 = New DevExpress.XtraReports.UI.XRLabel()
        Me.table7 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow7 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell59 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell61 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell62 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell63 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell64 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell65 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Detail4 = New DevExpress.XtraReports.UI.DetailBand()
        Me.table8 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow8 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell66 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell68 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell69 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell70 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell71 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell72 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.DetailReport4 = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.GroupHeader5 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel31 = New DevExpress.XtraReports.UI.XRLabel()
        Me.table9 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow9 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell73 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell74 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell75 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell76 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell77 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell78 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell79 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Detail5 = New DevExpress.XtraReports.UI.DetailBand()
        Me.table10 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow10 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell80 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell81 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell82 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell83 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell84 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell85 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell86 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Title = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailCaption2 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData2 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData3_Odd = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.PageInfo = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrLabel33 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel32 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel34 = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupFooter2 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrLabel35 = New DevExpress.XtraReports.UI.XRLabel()
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
        Me.parInvoiceID.Name = "parInvoiceID"
        Me.parInvoiceID.Type = GetType(Integer)
        Me.parInvoiceID.ValueInfo = "0"
        Me.parInvoiceID.Visible = False
        '
        'parPatientID
        '
        Me.parPatientID.Name = "parPatientID"
        Me.parPatientID.Type = GetType(Integer)
        Me.parPatientID.ValueInfo = "0"
        Me.parPatientID.Visible = False
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
        Me.pageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.pageInfo1.Name = "pageInfo1"
        Me.pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.pageInfo1.SizeF = New System.Drawing.SizeF(95.0!, 6.0!)
        Me.pageInfo1.StyleName = "PageInfo"
        Me.pageInfo1.StylePriority.UseTextAlignment = False
        Me.pageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'pageInfo2
        '
        Me.pageInfo2.Dpi = 25.4!
        Me.pageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(95.0!, 0!)
        Me.pageInfo2.Name = "pageInfo2"
        Me.pageInfo2.SizeF = New System.Drawing.SizeF(95.0!, 6.0!)
        Me.pageInfo2.StyleName = "PageInfo"
        Me.pageInfo2.StylePriority.UseTextAlignment = False
        Me.pageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.pageInfo2.TextFormatString = "Page {0} of {1}"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.label1})
        Me.ReportHeader.Dpi = 25.4!
        Me.ReportHeader.HeightF = 10.53167!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.DimGray
        Me.label1.Dpi = 25.4!
        Me.label1.Font = New DevExpress.Drawing.DXFont("Calibri", 25.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.label1.ForeColor = System.Drawing.Color.White
        Me.label1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.label1.Name = "label1"
        Me.label1.SizeF = New System.Drawing.SizeF(190.0!, 10.11411!)
        Me.label1.StyleName = "Title"
        Me.label1.StylePriority.UseBackColor = False
        Me.label1.StylePriority.UseFont = False
        Me.label1.StylePriority.UseForeColor = False
        Me.label1.StylePriority.UseTextAlignment = False
        Me.label1.Text = "Medical Invoice"
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
        'clinicDetail
        '
        Me.clinicDetail.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader1, Me.Detail1})
        Me.clinicDetail.DataMember = "GetReport_Invoice.Result1"
        Me.clinicDetail.DataSource = Me.SqlDataSource1
        Me.clinicDetail.Dpi = 25.4!
        Me.clinicDetail.Level = 0
        Me.clinicDetail.Name = "clinicDetail"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Dpi = 25.4!
        Me.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader1.HeightF = 0.5712507!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'Detail1
        '
        Me.Detail1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.clinicPanel})
        Me.Detail1.Dpi = 25.4!
        Me.Detail1.HeightF = 42.21159!
        Me.Detail1.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail1.Name = "Detail1"
        '
        'clinicPanel
        '
        Me.clinicPanel.BorderColor = System.Drawing.Color.Transparent
        Me.clinicPanel.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.clinicPanel.BorderWidth = 2.0!
        Me.clinicPanel.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel8, Me.XrLabel6, Me.XrLabel4, Me.XrLabel2, Me.pictureBox1, Me.XrLabel3, Me.XrLabel5, Me.XrLabel7, Me.XrLabel9, Me.XrLabel22, Me.XrLabel23, Me.XrLabel24, Me.XrLabel12, Me.XrLabel10, Me.XrLabel11})
        Me.clinicPanel.Dpi = 25.4!
        Me.clinicPanel.KeepTogether = False
        Me.clinicPanel.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.clinicPanel.Name = "clinicPanel"
        Me.clinicPanel.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.clinicPanel.SizeF = New System.Drawing.SizeF(190.0!, 40.82368!)
        Me.clinicPanel.StyleName = "DetailData2"
        Me.clinicPanel.StylePriority.UseBorderColor = False
        Me.clinicPanel.StylePriority.UseBorders = False
        Me.clinicPanel.StylePriority.UseBorderWidth = False
        Me.clinicPanel.StylePriority.UseFont = False
        Me.clinicPanel.StylePriority.UseForeColor = False
        Me.clinicPanel.StylePriority.UsePadding = False
        Me.clinicPanel.StylePriority.UseTextAlignment = False
        '
        'XrLabel8
        '
        Me.XrLabel8.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel8.BorderWidth = 2.0!
        Me.XrLabel8.Dpi = 25.4!
        Me.XrLabel8.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AddressEn]")})
        Me.XrLabel8.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel8.ForeColor = System.Drawing.Color.Black
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(2.539999!, 22.87207!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(71.09061!, 6.371167!)
        Me.XrLabel8.StyleName = "DetailData2"
        Me.XrLabel8.StylePriority.UseBorderColor = False
        Me.XrLabel8.StylePriority.UseBorders = False
        Me.XrLabel8.StylePriority.UseBorderWidth = False
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseForeColor = False
        Me.XrLabel8.StylePriority.UsePadding = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel6
        '
        Me.XrLabel6.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel6.BorderWidth = 2.0!
        Me.XrLabel6.Dpi = 25.4!
        Me.XrLabel6.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SpecialistEn]")})
        Me.XrLabel6.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel6.ForeColor = System.Drawing.Color.Black
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(2.285485!, 15.76007!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(71.09061!, 6.371167!)
        Me.XrLabel6.StyleName = "DetailData2"
        Me.XrLabel6.StylePriority.UseBorderColor = False
        Me.XrLabel6.StylePriority.UseBorders = False
        Me.XrLabel6.StylePriority.UseBorderWidth = False
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseForeColor = False
        Me.XrLabel6.StylePriority.UsePadding = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel4
        '
        Me.XrLabel4.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel4.BorderWidth = 2.0!
        Me.XrLabel4.Dpi = 25.4!
        Me.XrLabel4.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DrNameEn]")})
        Me.XrLabel4.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel4.ForeColor = System.Drawing.Color.Black
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(2.285485!, 8.648067!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(71.09061!, 6.371167!)
        Me.XrLabel4.StyleName = "DetailData2"
        Me.XrLabel4.StylePriority.UseBorderColor = False
        Me.XrLabel4.StylePriority.UseBorders = False
        Me.XrLabel4.StylePriority.UseBorderWidth = False
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseForeColor = False
        Me.XrLabel4.StylePriority.UsePadding = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel2
        '
        Me.XrLabel2.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel2.BorderWidth = 2.0!
        Me.XrLabel2.Dpi = 25.4!
        Me.XrLabel2.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ClinicNameEn]")})
        Me.XrLabel2.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel2.ForeColor = System.Drawing.Color.Black
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(2.285485!, 1.536066!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(71.09061!, 6.371166!)
        Me.XrLabel2.StyleName = "DetailData2"
        Me.XrLabel2.StylePriority.UseBorderColor = False
        Me.XrLabel2.StylePriority.UseBorders = False
        Me.XrLabel2.StylePriority.UseBorderWidth = False
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseForeColor = False
        Me.XrLabel2.StylePriority.UsePadding = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'pictureBox1
        '
        Me.pictureBox1.AnchorHorizontal = CType((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left Or DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right), DevExpress.XtraReports.UI.HorizontalAnchorStyles)
        Me.pictureBox1.AnchorVertical = CType((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top Or DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom), DevExpress.XtraReports.UI.VerticalAnchorStyles)
        Me.pictureBox1.Dpi = 25.4!
        Me.pictureBox1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[ClinicLogo]")})
        Me.pictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(82.5!, 2.54!)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.SizeF = New System.Drawing.SizeF(25.0!, 25.0!)
        Me.pictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage
        '
        'XrLabel3
        '
        Me.XrLabel3.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel3.BorderWidth = 2.0!
        Me.XrLabel3.Dpi = 25.4!
        Me.XrLabel3.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ClinicNameAr]")})
        Me.XrLabel3.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel3.ForeColor = System.Drawing.Color.Black
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(116.3694!, 1.536066!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(71.09061!, 6.371166!)
        Me.XrLabel3.StyleName = "DetailData2"
        Me.XrLabel3.StylePriority.UseBorderColor = False
        Me.XrLabel3.StylePriority.UseBorders = False
        Me.XrLabel3.StylePriority.UseBorderWidth = False
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseForeColor = False
        Me.XrLabel3.StylePriority.UsePadding = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel5
        '
        Me.XrLabel5.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel5.BorderWidth = 2.0!
        Me.XrLabel5.Dpi = 25.4!
        Me.XrLabel5.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DrNameAr]")})
        Me.XrLabel5.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel5.ForeColor = System.Drawing.Color.Black
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(116.3694!, 8.648067!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(71.09061!, 6.371166!)
        Me.XrLabel5.StyleName = "DetailData2"
        Me.XrLabel5.StylePriority.UseBorderColor = False
        Me.XrLabel5.StylePriority.UseBorders = False
        Me.XrLabel5.StylePriority.UseBorderWidth = False
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseForeColor = False
        Me.XrLabel5.StylePriority.UsePadding = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel7
        '
        Me.XrLabel7.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel7.BorderWidth = 2.0!
        Me.XrLabel7.Dpi = 25.4!
        Me.XrLabel7.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SpecialistAr]")})
        Me.XrLabel7.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel7.ForeColor = System.Drawing.Color.Black
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(116.3694!, 15.76007!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(71.09061!, 6.371167!)
        Me.XrLabel7.StyleName = "DetailData2"
        Me.XrLabel7.StylePriority.UseBorderColor = False
        Me.XrLabel7.StylePriority.UseBorders = False
        Me.XrLabel7.StylePriority.UseBorderWidth = False
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseForeColor = False
        Me.XrLabel7.StylePriority.UsePadding = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel9
        '
        Me.XrLabel9.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel9.BorderWidth = 2.0!
        Me.XrLabel9.Dpi = 25.4!
        Me.XrLabel9.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AddressAr]")})
        Me.XrLabel9.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel9.ForeColor = System.Drawing.Color.Black
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(116.3694!, 22.87207!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(71.09062!, 6.371168!)
        Me.XrLabel9.StyleName = "DetailData2"
        Me.XrLabel9.StylePriority.UseBorderColor = False
        Me.XrLabel9.StylePriority.UseBorders = False
        Me.XrLabel9.StylePriority.UseBorderWidth = False
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseForeColor = False
        Me.XrLabel9.StylePriority.UsePadding = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel22
        '
        Me.XrLabel22.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel22.BorderColor = System.Drawing.Color.White
        Me.XrLabel22.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel22.BorderWidth = 2.0!
        Me.XrLabel22.Dpi = 25.4!
        Me.XrLabel22.Font = New DevExpress.Drawing.DXFont("Arial", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel22.ForeColor = System.Drawing.Color.Black
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(2.91298!, 31.24324!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(18.0!, 7.111996!)
        Me.XrLabel22.StyleName = "DetailCaption2"
        Me.XrLabel22.StylePriority.UseBackColor = False
        Me.XrLabel22.StylePriority.UseBorderColor = False
        Me.XrLabel22.StylePriority.UseBorders = False
        Me.XrLabel22.StylePriority.UseBorderWidth = False
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.StylePriority.UseForeColor = False
        Me.XrLabel22.StylePriority.UsePadding = False
        Me.XrLabel22.StylePriority.UseTextAlignment = False
        Me.XrLabel22.Text = "Phone"
        Me.XrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel23
        '
        Me.XrLabel23.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel23.BorderColor = System.Drawing.Color.White
        Me.XrLabel23.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel23.BorderWidth = 2.0!
        Me.XrLabel23.Dpi = 25.4!
        Me.XrLabel23.Font = New DevExpress.Drawing.DXFont("Arial", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel23.ForeColor = System.Drawing.Color.Black
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(53.53441!, 31.24324!)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(18.0!, 7.111996!)
        Me.XrLabel23.StyleName = "DetailCaption2"
        Me.XrLabel23.StylePriority.UseBackColor = False
        Me.XrLabel23.StylePriority.UseBorderColor = False
        Me.XrLabel23.StylePriority.UseBorders = False
        Me.XrLabel23.StylePriority.UseBorderWidth = False
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.StylePriority.UseForeColor = False
        Me.XrLabel23.StylePriority.UsePadding = False
        Me.XrLabel23.StylePriority.UseTextAlignment = False
        Me.XrLabel23.Text = "Mobile"
        Me.XrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel24
        '
        Me.XrLabel24.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel24.BorderColor = System.Drawing.Color.White
        Me.XrLabel24.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel24.BorderWidth = 2.0!
        Me.XrLabel24.Dpi = 25.4!
        Me.XrLabel24.Font = New DevExpress.Drawing.DXFont("Arial", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel24.ForeColor = System.Drawing.Color.Black
        Me.XrLabel24.LocationFloat = New DevExpress.Utils.PointFloat(114.8592!, 31.24324!)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel24.SizeF = New System.Drawing.SizeF(15.0!, 7.111996!)
        Me.XrLabel24.StyleName = "DetailCaption2"
        Me.XrLabel24.StylePriority.UseBackColor = False
        Me.XrLabel24.StylePriority.UseBorderColor = False
        Me.XrLabel24.StylePriority.UseBorders = False
        Me.XrLabel24.StylePriority.UseBorderWidth = False
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.StylePriority.UseForeColor = False
        Me.XrLabel24.StylePriority.UsePadding = False
        Me.XrLabel24.StylePriority.UseTextAlignment = False
        Me.XrLabel24.Text = "Email"
        Me.XrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel12
        '
        Me.XrLabel12.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel12.BorderWidth = 2.0!
        Me.XrLabel12.Dpi = 25.4!
        Me.XrLabel12.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Email]")})
        Me.XrLabel12.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel12.ForeColor = System.Drawing.Color.Black
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(129.8592!, 31.61366!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(50.56322!, 6.371166!)
        Me.XrLabel12.StyleName = "DetailData2"
        Me.XrLabel12.StylePriority.UseBorderColor = False
        Me.XrLabel12.StylePriority.UseBorders = False
        Me.XrLabel12.StylePriority.UseBorderWidth = False
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseForeColor = False
        Me.XrLabel12.StylePriority.UsePadding = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel10
        '
        Me.XrLabel10.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel10.BorderWidth = 2.0!
        Me.XrLabel10.Dpi = 25.4!
        Me.XrLabel10.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Phone]")})
        Me.XrLabel10.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel10.ForeColor = System.Drawing.Color.Black
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(20.91298!, 31.61366!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(27.21405!, 6.371166!)
        Me.XrLabel10.StyleName = "DetailData2"
        Me.XrLabel10.StylePriority.UseBorderColor = False
        Me.XrLabel10.StylePriority.UseBorders = False
        Me.XrLabel10.StylePriority.UseBorderWidth = False
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.StylePriority.UseForeColor = False
        Me.XrLabel10.StylePriority.UsePadding = False
        Me.XrLabel10.StylePriority.UseTextAlignment = False
        Me.XrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel11
        '
        Me.XrLabel11.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel11.BorderWidth = 2.0!
        Me.XrLabel11.Dpi = 25.4!
        Me.XrLabel11.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Mobile]")})
        Me.XrLabel11.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel11.ForeColor = System.Drawing.Color.Black
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(72.21489!, 31.61366!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(38.883!, 6.371166!)
        Me.XrLabel11.StyleName = "DetailData2"
        Me.XrLabel11.StylePriority.UseBorderColor = False
        Me.XrLabel11.StylePriority.UseBorders = False
        Me.XrLabel11.StylePriority.UseBorderWidth = False
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseForeColor = False
        Me.XrLabel11.StylePriority.UsePadding = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
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
        Me.DetailReport1.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader2, Me.Detail2})
        Me.DetailReport1.DataMember = "GetReport_Invoice.Result2"
        Me.DetailReport1.DataSource = Me.SqlDataSource1
        Me.DetailReport1.Dpi = 25.4!
        Me.DetailReport1.Level = 1
        Me.DetailReport1.Name = "DetailReport1"
        '
        'GroupHeader2
        '
        Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel29})
        Me.GroupHeader2.Dpi = 25.4!
        Me.GroupHeader2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader2.HeightF = 12.41626!
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'XrLabel29
        '
        Me.XrLabel29.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.XrLabel29.Dpi = 25.4!
        Me.XrLabel29.Font = New DevExpress.Drawing.DXFont("Calibri", 18.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel29.LocationFloat = New DevExpress.Utils.PointFloat(0!, 1.151076!)
        Me.XrLabel29.Name = "XrLabel29"
        Me.XrLabel29.SizeF = New System.Drawing.SizeF(190.0!, 10.11411!)
        Me.XrLabel29.StyleName = "Title"
        Me.XrLabel29.StylePriority.UseBackColor = False
        Me.XrLabel29.StylePriority.UseFont = False
        Me.XrLabel29.StylePriority.UseTextAlignment = False
        Me.XrLabel29.Text = "Patient Info"
        Me.XrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'Detail2
        '
        Me.Detail2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.patientPanel})
        Me.Detail2.Dpi = 25.4!
        Me.Detail2.HeightF = 26.66297!
        Me.Detail2.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail2.Name = "Detail2"
        '
        'patientPanel
        '
        Me.patientPanel.BackColor = System.Drawing.Color.Gainsboro
        Me.patientPanel.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel21, Me.XrLabel19, Me.XrLabel20, Me.XrLabel14, Me.XrLabel1, Me.XrLabel13, Me.XrLabel16, Me.XrLabel18, Me.XrLabel26, Me.XrLabel17, Me.XrLabel15, Me.XrLabel28, Me.XrLabel25, Me.XrLabel27})
        Me.patientPanel.Dpi = 25.4!
        Me.patientPanel.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.patientPanel.Name = "patientPanel"
        Me.patientPanel.SizeF = New System.Drawing.SizeF(190.0!, 26.66297!)
        Me.patientPanel.StylePriority.UseBackColor = False
        '
        'XrLabel21
        '
        Me.XrLabel21.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel21.BorderColor = System.Drawing.Color.White
        Me.XrLabel21.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel21.BorderWidth = 2.0!
        Me.XrLabel21.Dpi = 25.4!
        Me.XrLabel21.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel21.ForeColor = System.Drawing.Color.Black
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(2.540001!, 18.02319!)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(29.46301!, 7.112!)
        Me.XrLabel21.StyleName = "DetailCaption2"
        Me.XrLabel21.StylePriority.UseBackColor = False
        Me.XrLabel21.StylePriority.UseBorderColor = False
        Me.XrLabel21.StylePriority.UseBorders = False
        Me.XrLabel21.StylePriority.UseBorderWidth = False
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.StylePriority.UseForeColor = False
        Me.XrLabel21.StylePriority.UsePadding = False
        Me.XrLabel21.StylePriority.UseTextAlignment = False
        Me.XrLabel21.Text = "Patient Number"
        Me.XrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel19
        '
        Me.XrLabel19.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel19.BorderColor = System.Drawing.Color.White
        Me.XrLabel19.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel19.BorderWidth = 2.0!
        Me.XrLabel19.Dpi = 25.4!
        Me.XrLabel19.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel19.ForeColor = System.Drawing.Color.Black
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(2.540001!, 1.799196!)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(29.46301!, 7.112!)
        Me.XrLabel19.StyleName = "DetailCaption2"
        Me.XrLabel19.StylePriority.UseBackColor = False
        Me.XrLabel19.StylePriority.UseBorderColor = False
        Me.XrLabel19.StylePriority.UseBorders = False
        Me.XrLabel19.StylePriority.UseBorderWidth = False
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UseForeColor = False
        Me.XrLabel19.StylePriority.UsePadding = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "Patient ID"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel20
        '
        Me.XrLabel20.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel20.BorderColor = System.Drawing.Color.White
        Me.XrLabel20.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel20.BorderWidth = 2.0!
        Me.XrLabel20.Dpi = 25.4!
        Me.XrLabel20.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel20.ForeColor = System.Drawing.Color.Black
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(2.540001!, 9.911184!)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(29.46301!, 7.112!)
        Me.XrLabel20.StyleName = "DetailCaption2"
        Me.XrLabel20.StylePriority.UseBackColor = False
        Me.XrLabel20.StylePriority.UseBorderColor = False
        Me.XrLabel20.StylePriority.UseBorders = False
        Me.XrLabel20.StylePriority.UseBorderWidth = False
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UseForeColor = False
        Me.XrLabel20.StylePriority.UsePadding = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "Patient Name"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel14
        '
        Me.XrLabel14.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel14.BorderWidth = 2.0!
        Me.XrLabel14.Dpi = 25.4!
        Me.XrLabel14.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientNumber]")})
        Me.XrLabel14.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.XrLabel14.ForeColor = System.Drawing.Color.Black
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(32.00301!, 18.39361!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(29.46301!, 6.371166!)
        Me.XrLabel14.StyleName = "DetailData2"
        Me.XrLabel14.StylePriority.UseBorderColor = False
        Me.XrLabel14.StylePriority.UseBorders = False
        Me.XrLabel14.StylePriority.UseBorderWidth = False
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseForeColor = False
        Me.XrLabel14.StylePriority.UsePadding = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel1.BorderWidth = 2.0!
        Me.XrLabel1.Dpi = 25.4!
        Me.XrLabel1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientID]")})
        Me.XrLabel1.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.XrLabel1.ForeColor = System.Drawing.Color.Black
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(32.00301!, 2.169612!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(29.46301!, 6.371167!)
        Me.XrLabel1.StyleName = "DetailData2"
        Me.XrLabel1.StylePriority.UseBorderColor = False
        Me.XrLabel1.StylePriority.UseBorders = False
        Me.XrLabel1.StylePriority.UseBorderWidth = False
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseForeColor = False
        Me.XrLabel1.StylePriority.UsePadding = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel13
        '
        Me.XrLabel13.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel13.BorderWidth = 2.0!
        Me.XrLabel13.Dpi = 25.4!
        Me.XrLabel13.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientName]")})
        Me.XrLabel13.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.XrLabel13.ForeColor = System.Drawing.Color.Black
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(32.00301!, 10.2816!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(46.88865!, 6.371167!)
        Me.XrLabel13.StyleName = "DetailData2"
        Me.XrLabel13.StylePriority.UseBorderColor = False
        Me.XrLabel13.StylePriority.UseBorders = False
        Me.XrLabel13.StylePriority.UseBorderWidth = False
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseForeColor = False
        Me.XrLabel13.StylePriority.UsePadding = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel16
        '
        Me.XrLabel16.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel16.BorderWidth = 2.0!
        Me.XrLabel16.Dpi = 25.4!
        Me.XrLabel16.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Age]")})
        Me.XrLabel16.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.XrLabel16.ForeColor = System.Drawing.Color.Black
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(154.3694!, 2.169612!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(15.72931!, 6.371167!)
        Me.XrLabel16.StyleName = "DetailData2"
        Me.XrLabel16.StylePriority.UseBorderColor = False
        Me.XrLabel16.StylePriority.UseBorders = False
        Me.XrLabel16.StylePriority.UseBorderWidth = False
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseForeColor = False
        Me.XrLabel16.StylePriority.UsePadding = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel18
        '
        Me.XrLabel18.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel18.BorderWidth = 2.0!
        Me.XrLabel18.Dpi = 25.4!
        Me.XrLabel18.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Address]")})
        Me.XrLabel18.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.XrLabel18.ForeColor = System.Drawing.Color.Black
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(131.21!, 18.39361!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(49.97787!, 6.371166!)
        Me.XrLabel18.StyleName = "DetailData2"
        Me.XrLabel18.StylePriority.UseBorderColor = False
        Me.XrLabel18.StylePriority.UseBorders = False
        Me.XrLabel18.StylePriority.UseBorderWidth = False
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UseForeColor = False
        Me.XrLabel18.StylePriority.UsePadding = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel26
        '
        Me.XrLabel26.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel26.BorderColor = System.Drawing.Color.White
        Me.XrLabel26.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel26.BorderWidth = 2.0!
        Me.XrLabel26.Dpi = 25.4!
        Me.XrLabel26.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel26.ForeColor = System.Drawing.Color.Black
        Me.XrLabel26.LocationFloat = New DevExpress.Utils.PointFloat(137.0941!, 1.799196!)
        Me.XrLabel26.Name = "XrLabel26"
        Me.XrLabel26.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel26.SizeF = New System.Drawing.SizeF(15.72931!, 7.112!)
        Me.XrLabel26.StyleName = "DetailCaption2"
        Me.XrLabel26.StylePriority.UseBackColor = False
        Me.XrLabel26.StylePriority.UseBorderColor = False
        Me.XrLabel26.StylePriority.UseBorders = False
        Me.XrLabel26.StylePriority.UseBorderWidth = False
        Me.XrLabel26.StylePriority.UseFont = False
        Me.XrLabel26.StylePriority.UseForeColor = False
        Me.XrLabel26.StylePriority.UsePadding = False
        Me.XrLabel26.StylePriority.UseTextAlignment = False
        Me.XrLabel26.Text = "Age"
        Me.XrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel17
        '
        Me.XrLabel17.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel17.BorderWidth = 2.0!
        Me.XrLabel17.Dpi = 25.4!
        Me.XrLabel17.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Phone]")})
        Me.XrLabel17.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.XrLabel17.ForeColor = System.Drawing.Color.Black
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(131.21!, 10.2816!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(21.61342!, 6.371167!)
        Me.XrLabel17.StyleName = "DetailData2"
        Me.XrLabel17.StylePriority.UseBorderColor = False
        Me.XrLabel17.StylePriority.UseBorders = False
        Me.XrLabel17.StylePriority.UseBorderWidth = False
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseForeColor = False
        Me.XrLabel17.StylePriority.UsePadding = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel15
        '
        Me.XrLabel15.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel15.BorderWidth = 2.0!
        Me.XrLabel15.Dpi = 25.4!
        Me.XrLabel15.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Sex]")})
        Me.XrLabel15.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.XrLabel15.ForeColor = System.Drawing.Color.Black
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(120.2503!, 2.169612!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(15.17513!, 6.371167!)
        Me.XrLabel15.StyleName = "DetailData2"
        Me.XrLabel15.StylePriority.UseBorderColor = False
        Me.XrLabel15.StylePriority.UseBorders = False
        Me.XrLabel15.StylePriority.UseBorderWidth = False
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseForeColor = False
        Me.XrLabel15.StylePriority.UsePadding = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel28
        '
        Me.XrLabel28.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel28.BorderColor = System.Drawing.Color.White
        Me.XrLabel28.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel28.BorderWidth = 2.0!
        Me.XrLabel28.Dpi = 25.4!
        Me.XrLabel28.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel28.ForeColor = System.Drawing.Color.Black
        Me.XrLabel28.LocationFloat = New DevExpress.Utils.PointFloat(104.8257!, 18.02319!)
        Me.XrLabel28.Name = "XrLabel28"
        Me.XrLabel28.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel28.SizeF = New System.Drawing.SizeF(26.38426!, 7.112!)
        Me.XrLabel28.StyleName = "DetailCaption2"
        Me.XrLabel28.StylePriority.UseBackColor = False
        Me.XrLabel28.StylePriority.UseBorderColor = False
        Me.XrLabel28.StylePriority.UseBorders = False
        Me.XrLabel28.StylePriority.UseBorderWidth = False
        Me.XrLabel28.StylePriority.UseFont = False
        Me.XrLabel28.StylePriority.UseForeColor = False
        Me.XrLabel28.StylePriority.UsePadding = False
        Me.XrLabel28.StylePriority.UseTextAlignment = False
        Me.XrLabel28.Text = "Address"
        Me.XrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel25
        '
        Me.XrLabel25.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel25.BorderColor = System.Drawing.Color.White
        Me.XrLabel25.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel25.BorderWidth = 2.0!
        Me.XrLabel25.Dpi = 25.4!
        Me.XrLabel25.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel25.ForeColor = System.Drawing.Color.Black
        Me.XrLabel25.LocationFloat = New DevExpress.Utils.PointFloat(105.0752!, 1.799196!)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel25.SizeF = New System.Drawing.SizeF(15.17513!, 7.112!)
        Me.XrLabel25.StyleName = "DetailCaption2"
        Me.XrLabel25.StylePriority.UseBackColor = False
        Me.XrLabel25.StylePriority.UseBorderColor = False
        Me.XrLabel25.StylePriority.UseBorders = False
        Me.XrLabel25.StylePriority.UseBorderWidth = False
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.StylePriority.UseForeColor = False
        Me.XrLabel25.StylePriority.UsePadding = False
        Me.XrLabel25.StylePriority.UseTextAlignment = False
        Me.XrLabel25.Text = "Sex"
        Me.XrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel27
        '
        Me.XrLabel27.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel27.BorderColor = System.Drawing.Color.White
        Me.XrLabel27.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel27.BorderWidth = 2.0!
        Me.XrLabel27.Dpi = 25.4!
        Me.XrLabel27.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel27.ForeColor = System.Drawing.Color.Black
        Me.XrLabel27.LocationFloat = New DevExpress.Utils.PointFloat(105.0752!, 9.911184!)
        Me.XrLabel27.Name = "XrLabel27"
        Me.XrLabel27.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel27.SizeF = New System.Drawing.SizeF(21.61342!, 7.112!)
        Me.XrLabel27.StyleName = "DetailCaption2"
        Me.XrLabel27.StylePriority.UseBackColor = False
        Me.XrLabel27.StylePriority.UseBorderColor = False
        Me.XrLabel27.StylePriority.UseBorders = False
        Me.XrLabel27.StylePriority.UseBorderWidth = False
        Me.XrLabel27.StylePriority.UseFont = False
        Me.XrLabel27.StylePriority.UseForeColor = False
        Me.XrLabel27.StylePriority.UsePadding = False
        Me.XrLabel27.StylePriority.UseTextAlignment = False
        Me.XrLabel27.Text = "Phone"
        Me.XrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailReport2
        '
        Me.DetailReport2.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader3, Me.Detail3})
        Me.DetailReport2.DataMember = "GetReport_Invoice.Result3"
        Me.DetailReport2.DataSource = Me.SqlDataSource1
        Me.DetailReport2.Dpi = 25.4!
        Me.DetailReport2.Level = 2
        Me.DetailReport2.Name = "DetailReport2"
        '
        'GroupHeader3
        '
        Me.GroupHeader3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.invHeaderPanel})
        Me.GroupHeader3.Dpi = 25.4!
        Me.GroupHeader3.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader3.HeightF = 36.92875!
        Me.GroupHeader3.Name = "GroupHeader3"
        '
        'invHeaderPanel
        '
        Me.invHeaderPanel.AnchorHorizontal = CType((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left Or DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right), DevExpress.XtraReports.UI.HorizontalAnchorStyles)
        Me.invHeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.invHeaderPanel.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel48, Me.XrLabel49, Me.XrLabel50, Me.XrLabel51, Me.XrLabel52, Me.XrLabel53, Me.invoiceInfoTable, Me.invoiceTotalTable})
        Me.invHeaderPanel.Dpi = 25.4!
        Me.invHeaderPanel.LocationFloat = New DevExpress.Utils.PointFloat(0!, 1.802673!)
        Me.invHeaderPanel.Name = "invHeaderPanel"
        Me.invHeaderPanel.SizeF = New System.Drawing.SizeF(190.0!, 35.12608!)
        Me.invHeaderPanel.StylePriority.UseBackColor = False
        '
        'XrLabel48
        '
        Me.XrLabel48.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel48.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel48.BorderWidth = 2.0!
        Me.XrLabel48.Dpi = 25.4!
        Me.XrLabel48.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([InvoiceStatus] = 1, 'Issued'," & Global.Microsoft.VisualBasic.ChrW(10) & "    Iif([InvoiceStatus] = 2, 'Paid'," & Global.Microsoft.VisualBasic.ChrW(10) & "        I" &
                    "if([InvoiceStatus] = 3, 'Partially Paid'," & Global.Microsoft.VisualBasic.ChrW(10) & "            Iif([InvoiceStatus] = 4, '" &
                    "Completed', '')" & Global.Microsoft.VisualBasic.ChrW(10) & "        )" & Global.Microsoft.VisualBasic.ChrW(10) & "    )" & Global.Microsoft.VisualBasic.ChrW(10) & ")" & Global.Microsoft.VisualBasic.ChrW(10))})
        Me.XrLabel48.Font = New DevExpress.Drawing.DXFont("Arial", 9.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel48.ForeColor = System.Drawing.Color.Black
        Me.XrLabel48.LocationFloat = New DevExpress.Utils.PointFloat(90.91524!, 25.84451!)
        Me.XrLabel48.Name = "XrLabel48"
        Me.XrLabel48.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel48.SizeF = New System.Drawing.SizeF(25.45415!, 6.371166!)
        Me.XrLabel48.StyleName = "DetailData2"
        Me.XrLabel48.StylePriority.UseBorderColor = False
        Me.XrLabel48.StylePriority.UseBorders = False
        Me.XrLabel48.StylePriority.UseBorderWidth = False
        Me.XrLabel48.StylePriority.UseFont = False
        Me.XrLabel48.StylePriority.UseForeColor = False
        Me.XrLabel48.StylePriority.UsePadding = False
        Me.XrLabel48.StylePriority.UseTextAlignment = False
        Me.XrLabel48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel49
        '
        Me.XrLabel49.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel49.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel49.BorderWidth = 2.0!
        Me.XrLabel49.Dpi = 25.4!
        Me.XrLabel49.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InvoiceStatusText]")})
        Me.XrLabel49.Font = New DevExpress.Drawing.DXFont("Arial", 9.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel49.ForeColor = System.Drawing.Color.Black
        Me.XrLabel49.LocationFloat = New DevExpress.Utils.PointFloat(153.1939!, 25.84451!)
        Me.XrLabel49.Name = "XrLabel49"
        Me.XrLabel49.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel49.SizeF = New System.Drawing.SizeF(36.80609!, 6.371167!)
        Me.XrLabel49.StyleName = "DetailData2"
        Me.XrLabel49.StylePriority.UseBorderColor = False
        Me.XrLabel49.StylePriority.UseBorders = False
        Me.XrLabel49.StylePriority.UseBorderWidth = False
        Me.XrLabel49.StylePriority.UseFont = False
        Me.XrLabel49.StylePriority.UseForeColor = False
        Me.XrLabel49.StylePriority.UsePadding = False
        Me.XrLabel49.StylePriority.UseTextAlignment = False
        Me.XrLabel49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel50
        '
        Me.XrLabel50.BorderColor = System.Drawing.Color.Transparent
        Me.XrLabel50.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel50.BorderWidth = 2.0!
        Me.XrLabel50.Dpi = 25.4!
        Me.XrLabel50.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DueDate]")})
        Me.XrLabel50.Font = New DevExpress.Drawing.DXFont("Arial", 9.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel50.ForeColor = System.Drawing.Color.Black
        Me.XrLabel50.LocationFloat = New DevExpress.Utils.PointFloat(20.21334!, 25.47409!)
        Me.XrLabel50.Name = "XrLabel50"
        Me.XrLabel50.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel50.SizeF = New System.Drawing.SizeF(26.38425!, 6.371162!)
        Me.XrLabel50.StyleName = "DetailData2"
        Me.XrLabel50.StylePriority.UseBorderColor = False
        Me.XrLabel50.StylePriority.UseBorders = False
        Me.XrLabel50.StylePriority.UseBorderWidth = False
        Me.XrLabel50.StylePriority.UseFont = False
        Me.XrLabel50.StylePriority.UseForeColor = False
        Me.XrLabel50.StylePriority.UsePadding = False
        Me.XrLabel50.StylePriority.UseTextAlignment = False
        Me.XrLabel50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel50.TextFormatString = "{0:dd/MM/yyyy}"
        '
        'XrLabel51
        '
        Me.XrLabel51.BackColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.XrLabel51.BorderColor = System.Drawing.Color.White
        Me.XrLabel51.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel51.BorderWidth = 2.0!
        Me.XrLabel51.Dpi = 25.4!
        Me.XrLabel51.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel51.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel51.LocationFloat = New DevExpress.Utils.PointFloat(116.3694!, 25.47408!)
        Me.XrLabel51.Name = "XrLabel51"
        Me.XrLabel51.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel51.SizeF = New System.Drawing.SizeF(36.82452!, 7.112001!)
        Me.XrLabel51.StyleName = "DetailCaption2"
        Me.XrLabel51.StylePriority.UseBackColor = False
        Me.XrLabel51.StylePriority.UseBorderColor = False
        Me.XrLabel51.StylePriority.UseBorders = False
        Me.XrLabel51.StylePriority.UseBorderWidth = False
        Me.XrLabel51.StylePriority.UseFont = False
        Me.XrLabel51.StylePriority.UseForeColor = False
        Me.XrLabel51.StylePriority.UsePadding = False
        Me.XrLabel51.StylePriority.UseTextAlignment = False
        Me.XrLabel51.Text = "Invoice Status Text"
        Me.XrLabel51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel52
        '
        Me.XrLabel52.BackColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.XrLabel52.BorderColor = System.Drawing.Color.White
        Me.XrLabel52.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel52.BorderWidth = 2.0!
        Me.XrLabel52.Dpi = 25.4!
        Me.XrLabel52.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel52.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel52.LocationFloat = New DevExpress.Utils.PointFloat(61.46601!, 25.47408!)
        Me.XrLabel52.Name = "XrLabel52"
        Me.XrLabel52.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel52.SizeF = New System.Drawing.SizeF(29.4492!, 7.112001!)
        Me.XrLabel52.StyleName = "DetailCaption2"
        Me.XrLabel52.StylePriority.UseBackColor = False
        Me.XrLabel52.StylePriority.UseBorderColor = False
        Me.XrLabel52.StylePriority.UseBorders = False
        Me.XrLabel52.StylePriority.UseBorderWidth = False
        Me.XrLabel52.StylePriority.UseFont = False
        Me.XrLabel52.StylePriority.UseForeColor = False
        Me.XrLabel52.StylePriority.UsePadding = False
        Me.XrLabel52.StylePriority.UseTextAlignment = False
        Me.XrLabel52.Text = "Invoice Status"
        Me.XrLabel52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel53
        '
        Me.XrLabel53.BackColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.XrLabel53.BorderColor = System.Drawing.Color.White
        Me.XrLabel53.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.XrLabel53.BorderWidth = 2.0!
        Me.XrLabel53.Dpi = 25.4!
        Me.XrLabel53.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel53.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.XrLabel53.LocationFloat = New DevExpress.Utils.PointFloat(0.4147397!, 25.10367!)
        Me.XrLabel53.Name = "XrLabel53"
        Me.XrLabel53.Padding = New DevExpress.XtraPrinting.PaddingInfo(1.524!, 1.524!, 0!, 0!, 25.4!)
        Me.XrLabel53.SizeF = New System.Drawing.SizeF(19.79855!, 7.112!)
        Me.XrLabel53.StyleName = "DetailCaption2"
        Me.XrLabel53.StylePriority.UseBackColor = False
        Me.XrLabel53.StylePriority.UseBorderColor = False
        Me.XrLabel53.StylePriority.UseBorders = False
        Me.XrLabel53.StylePriority.UseBorderWidth = False
        Me.XrLabel53.StylePriority.UseFont = False
        Me.XrLabel53.StylePriority.UseForeColor = False
        Me.XrLabel53.StylePriority.UsePadding = False
        Me.XrLabel53.StylePriority.UseTextAlignment = False
        Me.XrLabel53.Text = "Due Date"
        Me.XrLabel53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
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
        Me.invoiceTotalTableRow1.Weight = 0.604124696223205R
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
        Me.invoiceTotalTableRow2.Weight = 1.1297076696633677R
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
        'Detail3
        '
        Me.Detail3.Dpi = 25.4!
        Me.Detail3.HeightF = 0.03830516!
        Me.Detail3.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail3.Name = "Detail3"
        '
        'DetailReport3
        '
        Me.DetailReport3.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader4, Me.Detail4, Me.GroupFooter1})
        Me.DetailReport3.DataMember = "GetReport_Invoice.Result4"
        Me.DetailReport3.DataSource = Me.SqlDataSource1
        Me.DetailReport3.Dpi = 25.4!
        Me.DetailReport3.Level = 3
        Me.DetailReport3.Name = "DetailReport3"
        '
        'GroupHeader4
        '
        Me.GroupHeader4.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel30, Me.table7})
        Me.GroupHeader4.Dpi = 25.4!
        Me.GroupHeader4.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader4.HeightF = 16.14123!
        Me.GroupHeader4.Name = "GroupHeader4"
        Me.GroupHeader4.RepeatEveryPage = True
        '
        'XrLabel30
        '
        Me.XrLabel30.BackColor = System.Drawing.Color.SeaShell
        Me.XrLabel30.Dpi = 25.4!
        Me.XrLabel30.Font = New DevExpress.Drawing.DXFont("Calibri", 16.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel30.LocationFloat = New DevExpress.Utils.PointFloat(0!, 2.619287!)
        Me.XrLabel30.Name = "XrLabel30"
        Me.XrLabel30.SizeF = New System.Drawing.SizeF(190.0!, 6.409944!)
        Me.XrLabel30.StyleName = "Title"
        Me.XrLabel30.StylePriority.UseBackColor = False
        Me.XrLabel30.StylePriority.UseFont = False
        Me.XrLabel30.StylePriority.UseTextAlignment = False
        Me.XrLabel30.Text = "Treatment Details"
        Me.XrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'table7
        '
        Me.table7.BackColor = System.Drawing.Color.MistyRose
        Me.table7.Dpi = 25.4!
        Me.table7.LocationFloat = New DevExpress.Utils.PointFloat(0!, 9.02923!)
        Me.table7.Name = "table7"
        Me.table7.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow7})
        Me.table7.SizeF = New System.Drawing.SizeF(190.0!, 7.112!)
        Me.table7.StylePriority.UseBackColor = False
        '
        'tableRow7
        '
        Me.tableRow7.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell59, Me.tableCell61, Me.tableCell62, Me.tableCell63, Me.tableCell64, Me.tableCell65})
        Me.tableRow7.Dpi = 25.4!
        Me.tableRow7.Name = "tableRow7"
        Me.tableRow7.Weight = 1.0R
        '
        'tableCell59
        '
        Me.tableCell59.BackColor = System.Drawing.Color.MistyRose
        Me.tableCell59.BorderColor = System.Drawing.Color.Firebrick
        Me.tableCell59.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.tableCell59.Dpi = 25.4!
        Me.tableCell59.ForeColor = System.Drawing.Color.Black
        Me.tableCell59.Name = "tableCell59"
        Me.tableCell59.StyleName = "DetailCaption2"
        Me.tableCell59.StylePriority.UseBackColor = False
        Me.tableCell59.StylePriority.UseBorderColor = False
        Me.tableCell59.StylePriority.UseBorders = False
        Me.tableCell59.StylePriority.UseForeColor = False
        Me.tableCell59.StylePriority.UseTextAlignment = False
        Me.tableCell59.Text = "Number"
        Me.tableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell59.Weight = 0.08751586680127553R
        '
        'tableCell61
        '
        Me.tableCell61.BackColor = System.Drawing.Color.MistyRose
        Me.tableCell61.BorderColor = System.Drawing.Color.Firebrick
        Me.tableCell61.Dpi = 25.4!
        Me.tableCell61.ForeColor = System.Drawing.Color.Black
        Me.tableCell61.Name = "tableCell61"
        Me.tableCell61.StyleName = "DetailCaption2"
        Me.tableCell61.StylePriority.UseBackColor = False
        Me.tableCell61.StylePriority.UseBorderColor = False
        Me.tableCell61.StylePriority.UseForeColor = False
        Me.tableCell61.StylePriority.UseTextAlignment = False
        Me.tableCell61.Text = "Item Description"
        Me.tableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell61.Weight = 0.37673962676433403R
        '
        'tableCell62
        '
        Me.tableCell62.BackColor = System.Drawing.Color.MistyRose
        Me.tableCell62.BorderColor = System.Drawing.Color.Firebrick
        Me.tableCell62.Dpi = 25.4!
        Me.tableCell62.ForeColor = System.Drawing.Color.Black
        Me.tableCell62.Name = "tableCell62"
        Me.tableCell62.StyleName = "DetailCaption2"
        Me.tableCell62.StylePriority.UseBackColor = False
        Me.tableCell62.StylePriority.UseBorderColor = False
        Me.tableCell62.StylePriority.UseForeColor = False
        Me.tableCell62.StylePriority.UseTextAlignment = False
        Me.tableCell62.Text = "Treatment Date"
        Me.tableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell62.Weight = 0.17625108016164678R
        '
        'tableCell63
        '
        Me.tableCell63.BackColor = System.Drawing.Color.MistyRose
        Me.tableCell63.BorderColor = System.Drawing.Color.Firebrick
        Me.tableCell63.Dpi = 25.4!
        Me.tableCell63.ForeColor = System.Drawing.Color.Black
        Me.tableCell63.Name = "tableCell63"
        Me.tableCell63.StyleName = "DetailCaption2"
        Me.tableCell63.StylePriority.UseBackColor = False
        Me.tableCell63.StylePriority.UseBorderColor = False
        Me.tableCell63.StylePriority.UseForeColor = False
        Me.tableCell63.StylePriority.UseTextAlignment = False
        Me.tableCell63.Text = "Unit Price"
        Me.tableCell63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell63.Weight = 0.12215063195479543R
        '
        'tableCell64
        '
        Me.tableCell64.BackColor = System.Drawing.Color.MistyRose
        Me.tableCell64.BorderColor = System.Drawing.Color.Firebrick
        Me.tableCell64.Dpi = 25.4!
        Me.tableCell64.ForeColor = System.Drawing.Color.Black
        Me.tableCell64.Name = "tableCell64"
        Me.tableCell64.StyleName = "DetailCaption2"
        Me.tableCell64.StylePriority.UseBackColor = False
        Me.tableCell64.StylePriority.UseBorderColor = False
        Me.tableCell64.StylePriority.UseForeColor = False
        Me.tableCell64.StylePriority.UseTextAlignment = False
        Me.tableCell64.Text = "Discount"
        Me.tableCell64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell64.Weight = 0.11406945680317125R
        '
        'tableCell65
        '
        Me.tableCell65.BackColor = System.Drawing.Color.MistyRose
        Me.tableCell65.BorderColor = System.Drawing.Color.Firebrick
        Me.tableCell65.Dpi = 25.4!
        Me.tableCell65.ForeColor = System.Drawing.Color.Black
        Me.tableCell65.Name = "tableCell65"
        Me.tableCell65.StyleName = "DetailCaption2"
        Me.tableCell65.StylePriority.UseBackColor = False
        Me.tableCell65.StylePriority.UseBorderColor = False
        Me.tableCell65.StylePriority.UseForeColor = False
        Me.tableCell65.StylePriority.UseTextAlignment = False
        Me.tableCell65.Text = "Line Total"
        Me.tableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell65.Weight = 0.12327330739874588R
        '
        'Detail4
        '
        Me.Detail4.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.table8})
        Me.Detail4.Dpi = 25.4!
        Me.Detail4.HeightF = 6.371284!
        Me.Detail4.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail4.Name = "Detail4"
        '
        'table8
        '
        Me.table8.Dpi = 25.4!
        Me.table8.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.table8.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.table8.Name = "table8"
        Me.table8.OddStyleName = "DetailData3_Odd"
        Me.table8.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow8})
        Me.table8.SizeF = New System.Drawing.SizeF(190.0!, 6.371167!)
        Me.table8.StylePriority.UseFont = False
        '
        'tableRow8
        '
        Me.tableRow8.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell66, Me.tableCell68, Me.tableCell69, Me.tableCell70, Me.tableCell71, Me.tableCell72})
        Me.tableRow8.Dpi = 25.4!
        Me.tableRow8.Name = "tableRow8"
        Me.tableRow8.Weight = 11.039999351201134R
        '
        'tableCell66
        '
        Me.tableCell66.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.tableCell66.Dpi = 25.4!
        Me.tableCell66.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RowNumber]")})
        Me.tableCell66.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell66.Name = "tableCell66"
        Me.tableCell66.StyleName = "DetailData2"
        Me.tableCell66.StylePriority.UseBorders = False
        Me.tableCell66.StylePriority.UseFont = False
        Me.tableCell66.StylePriority.UseTextAlignment = False
        Me.tableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell66.Weight = 0.087515859212485989R
        '
        'tableCell68
        '
        Me.tableCell68.Dpi = 25.4!
        Me.tableCell68.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ItemDescription]")})
        Me.tableCell68.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell68.Name = "tableCell68"
        Me.tableCell68.StyleName = "DetailData2"
        Me.tableCell68.StylePriority.UseFont = False
        Me.tableCell68.Weight = 0.37673963435312358R
        '
        'tableCell69
        '
        Me.tableCell69.Dpi = 25.4!
        Me.tableCell69.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TreatmentDate]")})
        Me.tableCell69.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell69.Name = "tableCell69"
        Me.tableCell69.StyleName = "DetailData2"
        Me.tableCell69.StylePriority.UseFont = False
        Me.tableCell69.StylePriority.UseTextAlignment = False
        Me.tableCell69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell69.TextFormatString = "{0:dd/MM/yyyy}"
        Me.tableCell69.Weight = 0.17625108016164678R
        '
        'tableCell70
        '
        Me.tableCell70.Dpi = 25.4!
        Me.tableCell70.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UnitPrice]")})
        Me.tableCell70.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell70.Name = "tableCell70"
        Me.tableCell70.StyleName = "DetailData2"
        Me.tableCell70.StylePriority.UseFont = False
        Me.tableCell70.StylePriority.UseTextAlignment = False
        Me.tableCell70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell70.TextFormatString = "{0:C2}"
        Me.tableCell70.Weight = 0.12215063195479543R
        '
        'tableCell71
        '
        Me.tableCell71.Dpi = 25.4!
        Me.tableCell71.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Discount]")})
        Me.tableCell71.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell71.Name = "tableCell71"
        Me.tableCell71.StyleName = "DetailData2"
        Me.tableCell71.StylePriority.UseFont = False
        Me.tableCell71.StylePriority.UseTextAlignment = False
        Me.tableCell71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell71.TextFormatString = "{0:C2}"
        Me.tableCell71.Weight = 0.11406945680317125R
        '
        'tableCell72
        '
        Me.tableCell72.Dpi = 25.4!
        Me.tableCell72.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[LineTotal]")})
        Me.tableCell72.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell72.Name = "tableCell72"
        Me.tableCell72.StyleName = "DetailData2"
        Me.tableCell72.StylePriority.UseFont = False
        Me.tableCell72.StylePriority.UseTextAlignment = False
        Me.tableCell72.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell72.TextFormatString = "{0:C2}"
        Me.tableCell72.Weight = 0.12327334755345394R
        '
        'DetailReport4
        '
        Me.DetailReport4.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader5, Me.Detail5, Me.GroupFooter2})
        Me.DetailReport4.DataMember = "GetReport_Invoice.Result5"
        Me.DetailReport4.DataSource = Me.SqlDataSource1
        Me.DetailReport4.Dpi = 25.4!
        Me.DetailReport4.Level = 4
        Me.DetailReport4.Name = "DetailReport4"
        '
        'GroupHeader5
        '
        Me.GroupHeader5.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel31, Me.table9})
        Me.GroupHeader5.Dpi = 25.4!
        Me.GroupHeader5.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader5.HeightF = 15.10118!
        Me.GroupHeader5.Name = "GroupHeader5"
        '
        'XrLabel31
        '
        Me.XrLabel31.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.XrLabel31.Dpi = 25.4!
        Me.XrLabel31.Font = New DevExpress.Drawing.DXFont("Calibri", 16.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel31.LocationFloat = New DevExpress.Utils.PointFloat(0!, 1.579234!)
        Me.XrLabel31.Name = "XrLabel31"
        Me.XrLabel31.SizeF = New System.Drawing.SizeF(190.0!, 6.409944!)
        Me.XrLabel31.StyleName = "Title"
        Me.XrLabel31.StylePriority.UseBackColor = False
        Me.XrLabel31.StylePriority.UseFont = False
        Me.XrLabel31.StylePriority.UseTextAlignment = False
        Me.XrLabel31.Text = "Payment Details"
        Me.XrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'table9
        '
        Me.table9.Dpi = 25.4!
        Me.table9.LocationFloat = New DevExpress.Utils.PointFloat(0!, 7.989177!)
        Me.table9.Name = "table9"
        Me.table9.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow9})
        Me.table9.SizeF = New System.Drawing.SizeF(190.0!, 7.112!)
        '
        'tableRow9
        '
        Me.tableRow9.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell73, Me.tableCell74, Me.tableCell75, Me.tableCell76, Me.tableCell77, Me.tableCell78, Me.tableCell79})
        Me.tableRow9.Dpi = 25.4!
        Me.tableRow9.Name = "tableRow9"
        Me.tableRow9.Weight = 1.0R
        '
        'tableCell73
        '
        Me.tableCell73.BackColor = System.Drawing.Color.FloralWhite
        Me.tableCell73.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.tableCell73.Dpi = 25.4!
        Me.tableCell73.ForeColor = System.Drawing.Color.Black
        Me.tableCell73.Name = "tableCell73"
        Me.tableCell73.StyleName = "DetailCaption2"
        Me.tableCell73.StylePriority.UseBackColor = False
        Me.tableCell73.StylePriority.UseBorders = False
        Me.tableCell73.StylePriority.UseForeColor = False
        Me.tableCell73.StylePriority.UseTextAlignment = False
        Me.tableCell73.Text = "Number"
        Me.tableCell73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell73.Weight = 0.087515861109683388R
        '
        'tableCell74
        '
        Me.tableCell74.BackColor = System.Drawing.Color.FloralWhite
        Me.tableCell74.Dpi = 25.4!
        Me.tableCell74.ForeColor = System.Drawing.Color.Black
        Me.tableCell74.Name = "tableCell74"
        Me.tableCell74.StyleName = "DetailCaption2"
        Me.tableCell74.StylePriority.UseBackColor = False
        Me.tableCell74.StylePriority.UseForeColor = False
        Me.tableCell74.Text = "Pay Date"
        Me.tableCell74.Weight = 0.12654168982254832R
        '
        'tableCell75
        '
        Me.tableCell75.BackColor = System.Drawing.Color.FloralWhite
        Me.tableCell75.Dpi = 25.4!
        Me.tableCell75.ForeColor = System.Drawing.Color.Black
        Me.tableCell75.Name = "tableCell75"
        Me.tableCell75.StyleName = "DetailCaption2"
        Me.tableCell75.StylePriority.UseBackColor = False
        Me.tableCell75.StylePriority.UseForeColor = False
        Me.tableCell75.Text = "Pay Type"
        Me.tableCell75.Weight = 0.10944778040835733R
        '
        'tableCell76
        '
        Me.tableCell76.BackColor = System.Drawing.Color.FloralWhite
        Me.tableCell76.Dpi = 25.4!
        Me.tableCell76.ForeColor = System.Drawing.Color.Black
        Me.tableCell76.Name = "tableCell76"
        Me.tableCell76.StyleName = "DetailCaption2"
        Me.tableCell76.StylePriority.UseBackColor = False
        Me.tableCell76.StylePriority.UseForeColor = False
        Me.tableCell76.StylePriority.UseTextAlignment = False
        Me.tableCell76.Text = "Pay Value"
        Me.tableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell76.Weight = 0.11966098986173931R
        '
        'tableCell77
        '
        Me.tableCell77.BackColor = System.Drawing.Color.FloralWhite
        Me.tableCell77.Dpi = 25.4!
        Me.tableCell77.ForeColor = System.Drawing.Color.Black
        Me.tableCell77.Name = "tableCell77"
        Me.tableCell77.StyleName = "DetailCaption2"
        Me.tableCell77.StylePriority.UseBackColor = False
        Me.tableCell77.StylePriority.UseForeColor = False
        Me.tableCell77.Text = "Chq Number"
        Me.tableCell77.Weight = 0.161355952212685R
        '
        'tableCell78
        '
        Me.tableCell78.BackColor = System.Drawing.Color.FloralWhite
        Me.tableCell78.Dpi = 25.4!
        Me.tableCell78.ForeColor = System.Drawing.Color.Black
        Me.tableCell78.Name = "tableCell78"
        Me.tableCell78.StyleName = "DetailCaption2"
        Me.tableCell78.StylePriority.UseBackColor = False
        Me.tableCell78.StylePriority.UseForeColor = False
        Me.tableCell78.Text = "Payment Status"
        Me.tableCell78.Weight = 0.15813478168688325R
        '
        'tableCell79
        '
        Me.tableCell79.BackColor = System.Drawing.Color.FloralWhite
        Me.tableCell79.Dpi = 25.4!
        Me.tableCell79.ForeColor = System.Drawing.Color.Black
        Me.tableCell79.Name = "tableCell79"
        Me.tableCell79.StyleName = "DetailCaption2"
        Me.tableCell79.StylePriority.UseBackColor = False
        Me.tableCell79.StylePriority.UseForeColor = False
        Me.tableCell79.Text = "Notes"
        Me.tableCell79.Weight = 0.2373429448981034R
        '
        'Detail5
        '
        Me.Detail5.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.table10})
        Me.Detail5.Dpi = 25.4!
        Me.Detail5.HeightF = 12.39433!
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
        Me.tableRow10.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell80, Me.tableCell81, Me.tableCell82, Me.tableCell83, Me.tableCell84, Me.tableCell85, Me.tableCell86})
        Me.tableRow10.Dpi = 25.4!
        Me.tableRow10.Name = "tableRow10"
        Me.tableRow10.Weight = 11.039999351201134R
        '
        'tableCell80
        '
        Me.tableCell80.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.tableCell80.Dpi = 25.4!
        Me.tableCell80.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RowNumber]")})
        Me.tableCell80.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell80.Name = "tableCell80"
        Me.tableCell80.StyleName = "DetailData2"
        Me.tableCell80.StylePriority.UseBorders = False
        Me.tableCell80.StylePriority.UseFont = False
        Me.tableCell80.StylePriority.UseTextAlignment = False
        Me.tableCell80.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell80.Weight = 0.08751585957122196R
        '
        'tableCell81
        '
        Me.tableCell81.Dpi = 25.4!
        Me.tableCell81.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PayDate]")})
        Me.tableCell81.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell81.Name = "tableCell81"
        Me.tableCell81.StyleName = "DetailData2"
        Me.tableCell81.StylePriority.UseFont = False
        Me.tableCell81.TextFormatString = "{0:dd/MM/yyyy}"
        Me.tableCell81.Weight = 0.12654168980930738R
        '
        'tableCell82
        '
        Me.tableCell82.Dpi = 25.4!
        Me.tableCell82.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PayType]")})
        Me.tableCell82.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell82.Name = "tableCell82"
        Me.tableCell82.StyleName = "DetailData2"
        Me.tableCell82.StylePriority.UseFont = False
        Me.tableCell82.StylePriority.UseTextAlignment = False
        Me.tableCell82.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell82.Weight = 0.10944777976356256R
        '
        'tableCell83
        '
        Me.tableCell83.Dpi = 25.4!
        Me.tableCell83.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PayValue]")})
        Me.tableCell83.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell83.Name = "tableCell83"
        Me.tableCell83.StyleName = "DetailData2"
        Me.tableCell83.StylePriority.UseFont = False
        Me.tableCell83.StylePriority.UseTextAlignment = False
        Me.tableCell83.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell83.TextFormatString = "{0:C2}"
        Me.tableCell83.Weight = 0.11966098929033454R
        '
        'tableCell84
        '
        Me.tableCell84.Dpi = 25.4!
        Me.tableCell84.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ChqNumber]")})
        Me.tableCell84.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell84.Name = "tableCell84"
        Me.tableCell84.StyleName = "DetailData2"
        Me.tableCell84.StylePriority.UseFont = False
        Me.tableCell84.StylePriority.UseTextAlignment = False
        Me.tableCell84.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell84.Weight = 0.16135595200300079R
        '
        'tableCell85
        '
        Me.tableCell85.Dpi = 25.4!
        Me.tableCell85.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PaymentStatus]")})
        Me.tableCell85.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell85.Name = "tableCell85"
        Me.tableCell85.StyleName = "DetailData2"
        Me.tableCell85.StylePriority.UseFont = False
        Me.tableCell85.StylePriority.UseTextAlignment = False
        Me.tableCell85.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell85.Weight = 0.15813478032667297R
        '
        'tableCell86
        '
        Me.tableCell86.Dpi = 25.4!
        Me.tableCell86.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Notes]")})
        Me.tableCell86.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.tableCell86.Name = "tableCell86"
        Me.tableCell86.StyleName = "DetailData2"
        Me.tableCell86.StylePriority.UseFont = False
        Me.tableCell86.StylePriority.UseTextAlignment = False
        Me.tableCell86.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell86.Weight = 0.23734297935193077R
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
        Me.DetailCaption2.BackColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.DetailCaption2.BorderColor = System.Drawing.Color.White
        Me.DetailCaption2.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.DetailCaption2.BorderWidth = 2.0!
        Me.DetailCaption2.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.DetailCaption2.ForeColor = System.Drawing.Color.White
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
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel33, Me.XrLabel32, Me.XrLabel34})
        Me.GroupFooter1.Dpi = 25.4!
        Me.GroupFooter1.HeightF = 6.137093!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrLabel33
        '
        Me.XrLabel33.Dpi = 25.4!
        Me.XrLabel33.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Sum([LineTotal])")})
        Me.XrLabel33.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel33.ForeColor = System.Drawing.Color.Blue
        Me.XrLabel33.LocationFloat = New DevExpress.Utils.PointFloat(166.5781!, 0!)
        Me.XrLabel33.Multiline = True
        Me.XrLabel33.Name = "XrLabel33"
        Me.XrLabel33.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel33.SizeF = New System.Drawing.SizeF(23.42191!, 5.841997!)
        Me.XrLabel33.StylePriority.UseFont = False
        Me.XrLabel33.StylePriority.UseForeColor = False
        Me.XrLabel33.StylePriority.UseTextAlignment = False
        Me.XrLabel33.Text = "XrLabel33"
        Me.XrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel33.TextFormatString = "{0:N}"
        '
        'XrLabel32
        '
        Me.XrLabel32.Dpi = 25.4!
        Me.XrLabel32.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Sum([UnitPrice])")})
        Me.XrLabel32.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel32.ForeColor = System.Drawing.Color.Red
        Me.XrLabel32.LocationFloat = New DevExpress.Utils.PointFloat(121.6963!, 0!)
        Me.XrLabel32.Multiline = True
        Me.XrLabel32.Name = "XrLabel32"
        Me.XrLabel32.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel32.SizeF = New System.Drawing.SizeF(23.20863!, 5.841997!)
        Me.XrLabel32.StylePriority.UseFont = False
        Me.XrLabel32.StylePriority.UseForeColor = False
        Me.XrLabel32.StylePriority.UseTextAlignment = False
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel32.Summary = XrSummary1
        Me.XrLabel32.Text = "XrLabel32"
        Me.XrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel32.TextFormatString = "{0:N}"
        '
        'XrLabel34
        '
        Me.XrLabel34.Dpi = 25.4!
        Me.XrLabel34.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Sum([Discount])")})
        Me.XrLabel34.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel34.ForeColor = System.Drawing.Color.Fuchsia
        Me.XrLabel34.LocationFloat = New DevExpress.Utils.PointFloat(144.9049!, 0!)
        Me.XrLabel34.Multiline = True
        Me.XrLabel34.Name = "XrLabel34"
        Me.XrLabel34.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel34.SizeF = New System.Drawing.SizeF(21.6732!, 5.841997!)
        Me.XrLabel34.StylePriority.UseFont = False
        Me.XrLabel34.StylePriority.UseForeColor = False
        Me.XrLabel34.StylePriority.UseTextAlignment = False
        Me.XrLabel34.Text = "XrLabel31"
        Me.XrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel34.TextFormatString = "{0:N}"
        '
        'GroupFooter2
        '
        Me.GroupFooter2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel35})
        Me.GroupFooter2.Dpi = 25.4!
        Me.GroupFooter2.HeightF = 5.842001!
        Me.GroupFooter2.Name = "GroupFooter2"
        '
        'XrLabel35
        '
        Me.XrLabel35.Dpi = 25.4!
        Me.XrLabel35.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Sum([PayValue])")})
        Me.XrLabel35.Font = New DevExpress.Drawing.DXFont("Arial", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel35.ForeColor = System.Drawing.Color.Red
        Me.XrLabel35.LocationFloat = New DevExpress.Utils.PointFloat(61.46602!, 0!)
        Me.XrLabel35.Multiline = True
        Me.XrLabel35.Name = "XrLabel35"
        Me.XrLabel35.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5291666!, 0.5291666!, 0!, 0!, 25.4!)
        Me.XrLabel35.SizeF = New System.Drawing.SizeF(22.73559!, 5.842001!)
        Me.XrLabel35.StylePriority.UseFont = False
        Me.XrLabel35.StylePriority.UseForeColor = False
        Me.XrLabel35.Text = "XrLabel35"
        Me.XrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel35.TextFormatString = "{0:N0}"
        '
        'PatientInvPaysRep
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.Detail, Me.clinicDetail, Me.DetailReport1, Me.DetailReport2, Me.DetailReport3, Me.DetailReport4})
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
    Friend WithEvents clinicDetail As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents Detail1 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents pictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents SqlDataSource1 As DevExpress.DataAccess.Sql.SqlDataSource
    Friend WithEvents DetailReport1 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents Detail2 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents DetailReport2 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeader3 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents Detail3 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents DetailReport3 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeader4 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents table7 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow7 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell59 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell61 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell62 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell63 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell64 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell65 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Detail4 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents table8 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow8 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell66 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell68 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell69 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell70 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell71 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell72 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents DetailReport4 As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents GroupHeader5 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents table9 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow9 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell73 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell74 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell75 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell76 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell77 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell78 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell79 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Detail5 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents table10 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow10 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell80 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell81 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell82 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell83 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell84 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell85 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell86 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Title As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailCaption2 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData2 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData3_Odd As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents PageInfo As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents clinicPanel As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel28 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel27 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel26 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel29 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents invHeaderPanel As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents XrLabel48 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel49 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel50 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel51 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel52 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel53 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents invoiceInfoTable As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents invoiceNumberRow As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents invoiceNumber As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents invoiceDateRow As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents invoiceDate As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents invoiceTotalTable As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents invoiceTotalTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents total2Caption As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents invoiceTotalTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents total2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents patientPanel As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents XrLabel30 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel31 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLabel33 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel32 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel34 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupFooter2 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLabel35 As DevExpress.XtraReports.UI.XRLabel
End Class
