<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class SupplierInvoice
    Inherits DevExpress.XtraReports.UI.XtraReport

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim XrWatermark1 As DevExpress.XtraReports.UI.XRWatermark = New DevExpress.XtraReports.UI.XRWatermark()
        Me.styleTableHeader = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.styleDetail = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.styleDetailEven = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.styleFooterMuted = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.pageInfoDate = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.pageInfoPages = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.xrPanelColHead = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrTableHead = New DevExpress.XtraReports.UI.XRTable()
        Me.xrRowHead = New DevExpress.XtraReports.UI.XRTableRow()
        Me.xrCellHProduct = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrCellHBatch = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrCellHExp = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrCellHQty = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrCellHUnit = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrCellHTotal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrPanelInvoice = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrLblInvTotalVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblInvTotalCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblInvStatusVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblInvStatusCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblInvDueVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblInvDueCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblInvDateVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblInvDateCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblInvNoVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblInvNoCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblInvoiceBanner = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrPanelSupplier = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrLblSupPayVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupPayCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupAddrVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupAddrCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupEmailVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupEmailCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupPhoneVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupPhoneCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupContactVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupContactCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupNameVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupNameCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblSupplierBanner = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrPanelClinicBand = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrPanelAr = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrLblArEmailVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblArEmailCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblArMobileVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblArMobileCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblArPhoneVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblArPhoneCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblArAddrVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblArAddrCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblArSpecVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblArSpecCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblArDrVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblArDrCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrPanelCenter = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrPicClinicLogo = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.xrLblClinicNameAr = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblClinicNameEn = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblDocSubtitle = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrPanelEng = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrLblEngEmailVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblEngEmailCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblEngMobileVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblEngMobileCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblEngPhoneVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblEngPhoneCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblEngAddrVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblEngAddrCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblEngSpecVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblEngSpecCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblEngDrVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblEngDrCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.xrLineSign = New DevExpress.XtraReports.UI.XRLine()
        Me.xrLblSignCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrPanelGrand = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrLblGrandTotalVal = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLblGrandTotalCap = New DevExpress.XtraReports.UI.XRLabel()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.xrTableDetail = New DevExpress.XtraReports.UI.XRTable()
        Me.xrRowDetail = New DevExpress.XtraReports.UI.XRTableRow()
        Me.xrCellProduct = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrCellBatch = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrCellExp = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrCellQty = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrCellUnit = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrCellLineTotal = New DevExpress.XtraReports.UI.XRTableCell()
        CType(Me.xrTableHead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xrTableDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'styleTableHeader
        '
        Me.styleTableHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.styleTableHeader.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.styleTableHeader.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.styleTableHeader.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.styleTableHeader.ForeColor = System.Drawing.Color.White
        Me.styleTableHeader.Name = "styleTableHeader"
        Me.styleTableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'styleDetail
        '
        Me.styleDetail.BackColor = System.Drawing.Color.White
        Me.styleDetail.BorderColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.styleDetail.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.styleDetail.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!)
        Me.styleDetail.Name = "styleDetail"
        Me.styleDetail.Padding = New DevExpress.XtraPrinting.PaddingInfo(4.0!, 4.0!, 3.0!, 3.0!, 254.0!)
        Me.styleDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'styleDetailEven
        '
        Me.styleDetailEven.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.styleDetailEven.BorderColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.styleDetailEven.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.styleDetailEven.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!)
        Me.styleDetailEven.Name = "styleDetailEven"
        Me.styleDetailEven.Padding = New DevExpress.XtraPrinting.PaddingInfo(4.0!, 4.0!, 3.0!, 3.0!, 254.0!)
        Me.styleDetailEven.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'styleFooterMuted
        '
        Me.styleFooterMuted.Font = New DevExpress.Drawing.DXFont("Calibri", 8.25!, DevExpress.Drawing.DXFontStyle.Italic)
        Me.styleFooterMuted.ForeColor = System.Drawing.Color.DimGray
        Me.styleFooterMuted.Name = "styleFooterMuted"
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.HeightF = 50.8!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.pageInfoDate, Me.pageInfoPages})
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.HeightF = 76.2!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'pageInfoDate
        '
        Me.pageInfoDate.Dpi = 254.0!
        Me.pageInfoDate.Font = New DevExpress.Drawing.DXFont("Calibri", 8.25!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle))
        Me.pageInfoDate.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 12.7!)
        Me.pageInfoDate.Name = "pageInfoDate"
        Me.pageInfoDate.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.pageInfoDate.SizeF = New System.Drawing.SizeF(400.0!, 38.1!)
        Me.pageInfoDate.StyleName = "styleFooterMuted"
        Me.pageInfoDate.StylePriority.UseFont = False
        Me.pageInfoDate.TextFormatString = "Printed {0:yyyy-MM-dd HH:mm}"
        '
        'pageInfoPages
        '
        Me.pageInfoPages.Dpi = 254.0!
        Me.pageInfoPages.Font = New DevExpress.Drawing.DXFont("Calibri", 8.25!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle))
        Me.pageInfoPages.LocationFloat = New DevExpress.Utils.PointFloat(1524.0!, 12.7!)
        Me.pageInfoPages.Name = "pageInfoPages"
        Me.pageInfoPages.SizeF = New System.Drawing.SizeF(400.0!, 38.1!)
        Me.pageInfoPages.StyleName = "styleFooterMuted"
        Me.pageInfoPages.StylePriority.UseFont = False
        Me.pageInfoPages.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.pageInfoPages.TextFormatString = "Page {0} of {1}"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrPanelColHead, Me.xrPanelInvoice, Me.xrPanelSupplier, Me.xrPanelClinicBand})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 1206.975!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'xrPanelColHead
        '
        Me.xrPanelColHead.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.xrPanelColHead.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrTableHead})
        Me.xrPanelColHead.Dpi = 254.0!
        Me.xrPanelColHead.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 1111.025!)
        Me.xrPanelColHead.Name = "xrPanelColHead"
        Me.xrPanelColHead.SizeF = New System.Drawing.SizeF(1899.2!, 90.0!)
        '
        'xrTableHead
        '
        Me.xrTableHead.Dpi = 254.0!
        Me.xrTableHead.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.xrTableHead.Name = "xrTableHead"
        Me.xrTableHead.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.xrRowHead})
        Me.xrTableHead.SizeF = New System.Drawing.SizeF(1899.2!, 76.59998!)
        '
        'xrRowHead
        '
        Me.xrRowHead.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrCellHProduct, Me.xrCellHBatch, Me.xrCellHExp, Me.xrCellHQty, Me.xrCellHUnit, Me.xrCellHTotal})
        Me.xrRowHead.Dpi = 254.0!
        Me.xrRowHead.Name = "xrRowHead"
        Me.xrRowHead.StyleName = "styleTableHeader"
        Me.xrRowHead.Weight = 1.0R
        '
        'xrCellHProduct
        '
        Me.xrCellHProduct.Dpi = 254.0!
        Me.xrCellHProduct.Name = "xrCellHProduct"
        Me.xrCellHProduct.Text = "Product"
        Me.xrCellHProduct.Weight = 0.32R
        '
        'xrCellHBatch
        '
        Me.xrCellHBatch.Dpi = 254.0!
        Me.xrCellHBatch.Name = "xrCellHBatch"
        Me.xrCellHBatch.Text = "Batch"
        Me.xrCellHBatch.Weight = 0.14R
        '
        'xrCellHExp
        '
        Me.xrCellHExp.Dpi = 254.0!
        Me.xrCellHExp.Name = "xrCellHExp"
        Me.xrCellHExp.Text = "Expiry"
        Me.xrCellHExp.Weight = 0.12R
        '
        'xrCellHQty
        '
        Me.xrCellHQty.Dpi = 254.0!
        Me.xrCellHQty.Name = "xrCellHQty"
        Me.xrCellHQty.Text = "Qty"
        Me.xrCellHQty.Weight = 0.07R
        '
        'xrCellHUnit
        '
        Me.xrCellHUnit.Dpi = 254.0!
        Me.xrCellHUnit.Name = "xrCellHUnit"
        Me.xrCellHUnit.Text = "Unit price"
        Me.xrCellHUnit.Weight = 0.13R
        '
        'xrCellHTotal
        '
        Me.xrCellHTotal.Dpi = 254.0!
        Me.xrCellHTotal.Name = "xrCellHTotal"
        Me.xrCellHTotal.Text = "Line total"
        Me.xrCellHTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.xrCellHTotal.Weight = 0.22R
        '
        'xrPanelInvoice
        '
        Me.xrPanelInvoice.BackColor = System.Drawing.Color.White
        Me.xrPanelInvoice.BorderColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.xrPanelInvoice.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.xrPanelInvoice.BorderWidth = 2.0!
        Me.xrPanelInvoice.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLblInvTotalVal, Me.xrLblInvTotalCap, Me.xrLblInvStatusVal, Me.xrLblInvStatusCap, Me.xrLblInvDueVal, Me.xrLblInvDueCap, Me.xrLblInvDateVal, Me.xrLblInvDateCap, Me.xrLblInvNoVal, Me.xrLblInvNoCap, Me.xrLblInvoiceBanner})
        Me.xrPanelInvoice.Dpi = 254.0!
        Me.xrPanelInvoice.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 896.0249!)
        Me.xrPanelInvoice.Name = "xrPanelInvoice"
        Me.xrPanelInvoice.SizeF = New System.Drawing.SizeF(1899.2!, 214.9999!)
        '
        'xrLblInvTotalVal
        '
        Me.xrLblInvTotalVal.Dpi = 254.0!
        Me.xrLblInvTotalVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblInvTotalVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.xrLblInvTotalVal.LocationFloat = New DevExpress.Utils.PointFloat(1460.0!, 125.5!)
        Me.xrLblInvTotalVal.Name = "xrLblInvTotalVal"
        Me.xrLblInvTotalVal.SizeF = New System.Drawing.SizeF(400.0!, 60.0!)
        Me.xrLblInvTotalVal.StylePriority.UseFont = False
        Me.xrLblInvTotalVal.Text = "—"
        Me.xrLblInvTotalVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xrLblInvTotalCap
        '
        Me.xrLblInvTotalCap.Dpi = 254.0!
        Me.xrLblInvTotalCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblInvTotalCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblInvTotalCap.LocationFloat = New DevExpress.Utils.PointFloat(1200.0!, 125.5!)
        Me.xrLblInvTotalCap.Name = "xrLblInvTotalCap"
        Me.xrLblInvTotalCap.SizeF = New System.Drawing.SizeF(250.0!, 60.0!)
        Me.xrLblInvTotalCap.StylePriority.UseFont = False
        Me.xrLblInvTotalCap.Text = "Document total"
        Me.xrLblInvTotalCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xrLblInvStatusVal
        '
        Me.xrLblInvStatusVal.Dpi = 254.0!
        Me.xrLblInvStatusVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblInvStatusVal.LocationFloat = New DevExpress.Utils.PointFloat(205.4!, 127.5!)
        Me.xrLblInvStatusVal.Name = "xrLblInvStatusVal"
        Me.xrLblInvStatusVal.SizeF = New System.Drawing.SizeF(500.0!, 60.0!)
        Me.xrLblInvStatusVal.StylePriority.UseFont = False
        Me.xrLblInvStatusVal.Text = "—"
        '
        'xrLblInvStatusCap
        '
        Me.xrLblInvStatusCap.Dpi = 254.0!
        Me.xrLblInvStatusCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblInvStatusCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblInvStatusCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 127.5!)
        Me.xrLblInvStatusCap.Name = "xrLblInvStatusCap"
        Me.xrLblInvStatusCap.SizeF = New System.Drawing.SizeF(180.0!, 60.0!)
        Me.xrLblInvStatusCap.StylePriority.UseFont = False
        Me.xrLblInvStatusCap.Text = "Status"
        '
        'xrLblInvDueVal
        '
        Me.xrLblInvDueVal.Dpi = 254.0!
        Me.xrLblInvDueVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblInvDueVal.LocationFloat = New DevExpress.Utils.PointFloat(1330.0!, 65.5!)
        Me.xrLblInvDueVal.Name = "xrLblInvDueVal"
        Me.xrLblInvDueVal.SizeF = New System.Drawing.SizeF(320.0!, 60.0!)
        Me.xrLblInvDueVal.StylePriority.UseFont = False
        Me.xrLblInvDueVal.Text = "—"
        '
        'xrLblInvDueCap
        '
        Me.xrLblInvDueCap.Dpi = 254.0!
        Me.xrLblInvDueCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblInvDueCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblInvDueCap.LocationFloat = New DevExpress.Utils.PointFloat(1140.0!, 65.5!)
        Me.xrLblInvDueCap.Name = "xrLblInvDueCap"
        Me.xrLblInvDueCap.SizeF = New System.Drawing.SizeF(180.0!, 60.0!)
        Me.xrLblInvDueCap.StylePriority.UseFont = False
        Me.xrLblInvDueCap.Text = "Due date"
        '
        'xrLblInvDateVal
        '
        Me.xrLblInvDateVal.Dpi = 254.0!
        Me.xrLblInvDateVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblInvDateVal.LocationFloat = New DevExpress.Utils.PointFloat(781.5359!, 65.49991!)
        Me.xrLblInvDateVal.Name = "xrLblInvDateVal"
        Me.xrLblInvDateVal.SizeF = New System.Drawing.SizeF(320.0!, 60.0!)
        Me.xrLblInvDateVal.StylePriority.UseFont = False
        Me.xrLblInvDateVal.Text = "—"
        '
        'xrLblInvDateCap
        '
        Me.xrLblInvDateCap.Dpi = 254.0!
        Me.xrLblInvDateCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblInvDateCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblInvDateCap.LocationFloat = New DevExpress.Utils.PointFloat(580.0001!, 65.49988!)
        Me.xrLblInvDateCap.Name = "xrLblInvDateCap"
        Me.xrLblInvDateCap.SizeF = New System.Drawing.SizeF(201.5358!, 60.00006!)
        Me.xrLblInvDateCap.StylePriority.UseFont = False
        Me.xrLblInvDateCap.Text = "Invoice date"
        '
        'xrLblInvNoVal
        '
        Me.xrLblInvNoVal.Dpi = 254.0!
        Me.xrLblInvNoVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblInvNoVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.xrLblInvNoVal.LocationFloat = New DevExpress.Utils.PointFloat(205.4!, 65.5!)
        Me.xrLblInvNoVal.Name = "xrLblInvNoVal"
        Me.xrLblInvNoVal.SizeF = New System.Drawing.SizeF(320.0!, 60.0!)
        Me.xrLblInvNoVal.StylePriority.UseFont = False
        Me.xrLblInvNoVal.Text = "—"
        '
        'xrLblInvNoCap
        '
        Me.xrLblInvNoCap.Dpi = 254.0!
        Me.xrLblInvNoCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblInvNoCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblInvNoCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 65.5!)
        Me.xrLblInvNoCap.Name = "xrLblInvNoCap"
        Me.xrLblInvNoCap.SizeF = New System.Drawing.SizeF(180.0!, 60.0!)
        Me.xrLblInvNoCap.StylePriority.UseFont = False
        Me.xrLblInvNoCap.Text = "Invoice #"
        '
        'xrLblInvoiceBanner
        '
        Me.xrLblInvoiceBanner.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.xrLblInvoiceBanner.Dpi = 254.0!
        Me.xrLblInvoiceBanner.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblInvoiceBanner.ForeColor = System.Drawing.Color.White
        Me.xrLblInvoiceBanner.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.xrLblInvoiceBanner.Name = "xrLblInvoiceBanner"
        Me.xrLblInvoiceBanner.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 10.0!, 10.0!, 254.0!)
        Me.xrLblInvoiceBanner.SizeF = New System.Drawing.SizeF(1899.2!, 50.0!)
        Me.xrLblInvoiceBanner.Text = "Invoice details  ·  تفاصيل الفاتورة"
        Me.xrLblInvoiceBanner.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrPanelSupplier
        '
        Me.xrPanelSupplier.BackColor = System.Drawing.Color.White
        Me.xrPanelSupplier.BorderColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.xrPanelSupplier.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.xrPanelSupplier.BorderWidth = 1.0!
        Me.xrPanelSupplier.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLblSupPayVal, Me.xrLblSupPayCap, Me.xrLblSupAddrVal, Me.xrLblSupAddrCap, Me.xrLblSupEmailVal, Me.xrLblSupEmailCap, Me.xrLblSupPhoneVal, Me.xrLblSupPhoneCap, Me.xrLblSupContactVal, Me.xrLblSupContactCap, Me.xrLblSupNameVal, Me.xrLblSupNameCap, Me.xrLblSupplierBanner})
        Me.xrPanelSupplier.Dpi = 254.0!
        Me.xrPanelSupplier.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 541.004!)
        Me.xrPanelSupplier.Name = "xrPanelSupplier"
        Me.xrPanelSupplier.SizeF = New System.Drawing.SizeF(1899.2!, 355.0209!)
        '
        'xrLblSupPayVal
        '
        Me.xrLblSupPayVal.Dpi = 254.0!
        Me.xrLblSupPayVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupPayVal.LocationFloat = New DevExpress.Utils.PointFloat(1335.0!, 167.0624!)
        Me.xrLblSupPayVal.Name = "xrLblSupPayVal"
        Me.xrLblSupPayVal.SizeF = New System.Drawing.SizeF(538.2!, 60.0!)
        Me.xrLblSupPayVal.StylePriority.UseFont = False
        Me.xrLblSupPayVal.Text = "—"
        '
        'xrLblSupPayCap
        '
        Me.xrLblSupPayCap.Dpi = 254.0!
        Me.xrLblSupPayCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupPayCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblSupPayCap.LocationFloat = New DevExpress.Utils.PointFloat(1125.0!, 167.0624!)
        Me.xrLblSupPayCap.Name = "xrLblSupPayCap"
        Me.xrLblSupPayCap.SizeF = New System.Drawing.SizeF(200.0!, 60.0!)
        Me.xrLblSupPayCap.StylePriority.UseFont = False
        Me.xrLblSupPayCap.Text = "Payment terms"
        '
        'xrLblSupAddrVal
        '
        Me.xrLblSupAddrVal.Dpi = 254.0!
        Me.xrLblSupAddrVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupAddrVal.LocationFloat = New DevExpress.Utils.PointFloat(258.4!, 287.0623!)
        Me.xrLblSupAddrVal.Multiline = True
        Me.xrLblSupAddrVal.Name = "xrLblSupAddrVal"
        Me.xrLblSupAddrVal.SizeF = New System.Drawing.SizeF(1614.8!, 60.0!)
        Me.xrLblSupAddrVal.StylePriority.UseFont = False
        Me.xrLblSupAddrVal.Text = "—"
        '
        'xrLblSupAddrCap
        '
        Me.xrLblSupAddrCap.Dpi = 254.0!
        Me.xrLblSupAddrCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupAddrCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblSupAddrCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 287.0624!)
        Me.xrLblSupAddrCap.Name = "xrLblSupAddrCap"
        Me.xrLblSupAddrCap.SizeF = New System.Drawing.SizeF(220.0!, 60.0!)
        Me.xrLblSupAddrCap.StylePriority.UseFont = False
        Me.xrLblSupAddrCap.Text = "Address"
        '
        'xrLblSupEmailVal
        '
        Me.xrLblSupEmailVal.Dpi = 254.0!
        Me.xrLblSupEmailVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupEmailVal.LocationFloat = New DevExpress.Utils.PointFloat(860.0!, 227.0624!)
        Me.xrLblSupEmailVal.Name = "xrLblSupEmailVal"
        Me.xrLblSupEmailVal.SizeF = New System.Drawing.SizeF(1013.2!, 60.0!)
        Me.xrLblSupEmailVal.StylePriority.UseFont = False
        Me.xrLblSupEmailVal.Text = "—"
        '
        'xrLblSupEmailCap
        '
        Me.xrLblSupEmailCap.Dpi = 254.0!
        Me.xrLblSupEmailCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupEmailCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblSupEmailCap.LocationFloat = New DevExpress.Utils.PointFloat(700.0!, 227.0624!)
        Me.xrLblSupEmailCap.Name = "xrLblSupEmailCap"
        Me.xrLblSupEmailCap.SizeF = New System.Drawing.SizeF(150.0!, 60.0!)
        Me.xrLblSupEmailCap.StylePriority.UseFont = False
        Me.xrLblSupEmailCap.Text = "Email"
        '
        'xrLblSupPhoneVal
        '
        Me.xrLblSupPhoneVal.Dpi = 254.0!
        Me.xrLblSupPhoneVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupPhoneVal.LocationFloat = New DevExpress.Utils.PointFloat(258.4!, 227.0624!)
        Me.xrLblSupPhoneVal.Name = "xrLblSupPhoneVal"
        Me.xrLblSupPhoneVal.SizeF = New System.Drawing.SizeF(400.0!, 60.0!)
        Me.xrLblSupPhoneVal.StylePriority.UseFont = False
        Me.xrLblSupPhoneVal.Text = "—"
        '
        'xrLblSupPhoneCap
        '
        Me.xrLblSupPhoneCap.Dpi = 254.0!
        Me.xrLblSupPhoneCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupPhoneCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblSupPhoneCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 227.0624!)
        Me.xrLblSupPhoneCap.Name = "xrLblSupPhoneCap"
        Me.xrLblSupPhoneCap.SizeF = New System.Drawing.SizeF(220.0!, 60.0!)
        Me.xrLblSupPhoneCap.StylePriority.UseFont = False
        Me.xrLblSupPhoneCap.Text = "Phone"
        '
        'xrLblSupContactVal
        '
        Me.xrLblSupContactVal.Dpi = 254.0!
        Me.xrLblSupContactVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupContactVal.LocationFloat = New DevExpress.Utils.PointFloat(258.4!, 167.0624!)
        Me.xrLblSupContactVal.Name = "xrLblSupContactVal"
        Me.xrLblSupContactVal.SizeF = New System.Drawing.SizeF(850.0!, 60.0!)
        Me.xrLblSupContactVal.StylePriority.UseFont = False
        Me.xrLblSupContactVal.Text = "—"
        '
        'xrLblSupContactCap
        '
        Me.xrLblSupContactCap.Dpi = 254.0!
        Me.xrLblSupContactCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupContactCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblSupContactCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 167.0624!)
        Me.xrLblSupContactCap.Name = "xrLblSupContactCap"
        Me.xrLblSupContactCap.SizeF = New System.Drawing.SizeF(220.0!, 60.0!)
        Me.xrLblSupContactCap.StylePriority.UseFont = False
        Me.xrLblSupContactCap.Text = "Contact person"
        '
        'xrLblSupNameVal
        '
        Me.xrLblSupNameVal.Dpi = 254.0!
        Me.xrLblSupNameVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupNameVal.LocationFloat = New DevExpress.Utils.PointFloat(258.4!, 101.3958!)
        Me.xrLblSupNameVal.Name = "xrLblSupNameVal"
        Me.xrLblSupNameVal.SizeF = New System.Drawing.SizeF(850.0!, 60.0!)
        Me.xrLblSupNameVal.StylePriority.UseFont = False
        Me.xrLblSupNameVal.Text = "—"
        '
        'xrLblSupNameCap
        '
        Me.xrLblSupNameCap.Dpi = 254.0!
        Me.xrLblSupNameCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupNameCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblSupNameCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 101.3958!)
        Me.xrLblSupNameCap.Name = "xrLblSupNameCap"
        Me.xrLblSupNameCap.SizeF = New System.Drawing.SizeF(220.0!, 60.0!)
        Me.xrLblSupNameCap.StylePriority.UseFont = False
        Me.xrLblSupNameCap.Text = "Supplier name"
        '
        'xrLblSupplierBanner
        '
        Me.xrLblSupplierBanner.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.xrLblSupplierBanner.Dpi = 254.0!
        Me.xrLblSupplierBanner.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSupplierBanner.ForeColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.xrLblSupplierBanner.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 15.04156!)
        Me.xrLblSupplierBanner.Name = "xrLblSupplierBanner"
        Me.xrLblSupplierBanner.Padding = New DevExpress.XtraPrinting.PaddingInfo(15.0!, 15.0!, 10.0!, 10.0!, 254.0!)
        Me.xrLblSupplierBanner.SizeF = New System.Drawing.SizeF(1847.8!, 64.35419!)
        Me.xrLblSupplierBanner.Text = "Supplier  ·  المورد"
        Me.xrLblSupplierBanner.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrPanelClinicBand
        '
        Me.xrPanelClinicBand.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.xrPanelClinicBand.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.xrPanelClinicBand.BorderWidth = 1.0!
        Me.xrPanelClinicBand.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrPanelAr, Me.xrPanelCenter, Me.xrPanelEng})
        Me.xrPanelClinicBand.Dpi = 254.0!
        Me.xrPanelClinicBand.LocationFloat = New DevExpress.Utils.PointFloat(0!, 53.81248!)
        Me.xrPanelClinicBand.Name = "xrPanelClinicBand"
        Me.xrPanelClinicBand.SizeF = New System.Drawing.SizeF(1950.0!, 487.1915!)
        '
        'xrPanelAr
        '
        Me.xrPanelAr.BackColor = System.Drawing.Color.Transparent
        Me.xrPanelAr.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLblArEmailVal, Me.xrLblArEmailCap, Me.xrLblArMobileVal, Me.xrLblArMobileCap, Me.xrLblArPhoneVal, Me.xrLblArPhoneCap, Me.xrLblArAddrVal, Me.xrLblArAddrCap, Me.xrLblArSpecVal, Me.xrLblArSpecCap, Me.xrLblArDrVal, Me.xrLblArDrCap})
        Me.xrPanelAr.Dpi = 254.0!
        Me.xrPanelAr.LocationFloat = New DevExpress.Utils.PointFloat(1300.0!, 25.00001!)
        Me.xrPanelAr.Name = "xrPanelAr"
        Me.xrPanelAr.SizeF = New System.Drawing.SizeF(650.0!, 450.0!)
        '
        'xrLblArEmailVal
        '
        Me.xrLblArEmailVal.Dpi = 254.0!
        Me.xrLblArEmailVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArEmailVal.LocationFloat = New DevExpress.Utils.PointFloat(16.4032!, 362.8201!)
        Me.xrLblArEmailVal.Name = "xrLblArEmailVal"
        Me.xrLblArEmailVal.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArEmailVal.SizeF = New System.Drawing.SizeF(384.8876!, 50.0!)
        Me.xrLblArEmailVal.StylePriority.UseFont = False
        Me.xrLblArEmailVal.StylePriority.UseTextAlignment = False
        Me.xrLblArEmailVal.Text = "—"
        Me.xrLblArEmailVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLblArEmailCap
        '
        Me.xrLblArEmailCap.Dpi = 254.0!
        Me.xrLblArEmailCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArEmailCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblArEmailCap.LocationFloat = New DevExpress.Utils.PointFloat(401.2906!, 370.3201!)
        Me.xrLblArEmailCap.Name = "xrLblArEmailCap"
        Me.xrLblArEmailCap.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArEmailCap.SizeF = New System.Drawing.SizeF(223.7092!, 34.99988!)
        Me.xrLblArEmailCap.StylePriority.UseFont = False
        Me.xrLblArEmailCap.StylePriority.UseTextAlignment = False
        Me.xrLblArEmailCap.Text = "البريد الإلكتروني"
        Me.xrLblArEmailCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLblArMobileVal
        '
        Me.xrLblArMobileVal.Dpi = 254.0!
        Me.xrLblArMobileVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArMobileVal.LocationFloat = New DevExpress.Utils.PointFloat(250.3582!, 324.8201!)
        Me.xrLblArMobileVal.Name = "xrLblArMobileVal"
        Me.xrLblArMobileVal.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArMobileVal.SizeF = New System.Drawing.SizeF(245.3999!, 38.0!)
        Me.xrLblArMobileVal.StylePriority.UseFont = False
        Me.xrLblArMobileVal.StylePriority.UseTextAlignment = False
        Me.xrLblArMobileVal.Text = "—"
        Me.xrLblArMobileVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLblArMobileCap
        '
        Me.xrLblArMobileCap.Dpi = 254.0!
        Me.xrLblArMobileCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArMobileCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblArMobileCap.LocationFloat = New DevExpress.Utils.PointFloat(495.7583!, 327.82!)
        Me.xrLblArMobileCap.Name = "xrLblArMobileCap"
        Me.xrLblArMobileCap.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArMobileCap.SizeF = New System.Drawing.SizeF(128.2417!, 35.0!)
        Me.xrLblArMobileCap.StylePriority.UseFont = False
        Me.xrLblArMobileCap.StylePriority.UseTextAlignment = False
        Me.xrLblArMobileCap.Text = "الجوال"
        Me.xrLblArMobileCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLblArPhoneVal
        '
        Me.xrLblArPhoneVal.Dpi = 254.0!
        Me.xrLblArPhoneVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArPhoneVal.LocationFloat = New DevExpress.Utils.PointFloat(250.3582!, 280.82!)
        Me.xrLblArPhoneVal.Name = "xrLblArPhoneVal"
        Me.xrLblArPhoneVal.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArPhoneVal.SizeF = New System.Drawing.SizeF(245.4!, 38.0!)
        Me.xrLblArPhoneVal.StylePriority.UseFont = False
        Me.xrLblArPhoneVal.StylePriority.UseTextAlignment = False
        Me.xrLblArPhoneVal.Text = "—"
        Me.xrLblArPhoneVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLblArPhoneCap
        '
        Me.xrLblArPhoneCap.Dpi = 254.0!
        Me.xrLblArPhoneCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArPhoneCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblArPhoneCap.LocationFloat = New DevExpress.Utils.PointFloat(495.7583!, 284.32!)
        Me.xrLblArPhoneCap.Name = "xrLblArPhoneCap"
        Me.xrLblArPhoneCap.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArPhoneCap.SizeF = New System.Drawing.SizeF(128.2417!, 35.0!)
        Me.xrLblArPhoneCap.StylePriority.UseFont = False
        Me.xrLblArPhoneCap.StylePriority.UseTextAlignment = False
        Me.xrLblArPhoneCap.Text = "الهاتف"
        Me.xrLblArPhoneCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLblArAddrVal
        '
        Me.xrLblArAddrVal.Dpi = 254.0!
        Me.xrLblArAddrVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArAddrVal.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 200.82!)
        Me.xrLblArAddrVal.Multiline = True
        Me.xrLblArAddrVal.Name = "xrLblArAddrVal"
        Me.xrLblArAddrVal.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArAddrVal.SizeF = New System.Drawing.SizeF(599.2!, 80.0!)
        Me.xrLblArAddrVal.StylePriority.UseFont = False
        Me.xrLblArAddrVal.StylePriority.UseTextAlignment = False
        Me.xrLblArAddrVal.Text = "—"
        Me.xrLblArAddrVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLblArAddrCap
        '
        Me.xrLblArAddrCap.Dpi = 254.0!
        Me.xrLblArAddrCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArAddrCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblArAddrCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 165.82!)
        Me.xrLblArAddrCap.Name = "xrLblArAddrCap"
        Me.xrLblArAddrCap.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArAddrCap.SizeF = New System.Drawing.SizeF(599.2!, 35.0!)
        Me.xrLblArAddrCap.StylePriority.UseFont = False
        Me.xrLblArAddrCap.StylePriority.UseTextAlignment = False
        Me.xrLblArAddrCap.Text = "العنوان"
        Me.xrLblArAddrCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLblArSpecVal
        '
        Me.xrLblArSpecVal.Dpi = 254.0!
        Me.xrLblArSpecVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArSpecVal.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 77.0!)
        Me.xrLblArSpecVal.Name = "xrLblArSpecVal"
        Me.xrLblArSpecVal.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArSpecVal.SizeF = New System.Drawing.SizeF(599.2!, 80.0!)
        Me.xrLblArSpecVal.StylePriority.UseFont = False
        Me.xrLblArSpecVal.StylePriority.UseTextAlignment = False
        Me.xrLblArSpecVal.Text = "—"
        Me.xrLblArSpecVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLblArSpecCap
        '
        Me.xrLblArSpecCap.Dpi = 254.0!
        Me.xrLblArSpecCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArSpecCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblArSpecCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 42.0!)
        Me.xrLblArSpecCap.Name = "xrLblArSpecCap"
        Me.xrLblArSpecCap.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArSpecCap.SizeF = New System.Drawing.SizeF(599.2!, 35.0!)
        Me.xrLblArSpecCap.StylePriority.UseFont = False
        Me.xrLblArSpecCap.StylePriority.UseTextAlignment = False
        Me.xrLblArSpecCap.Text = "التخصص"
        Me.xrLblArSpecCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLblArDrVal
        '
        Me.xrLblArDrVal.Dpi = 254.0!
        Me.xrLblArDrVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArDrVal.LocationFloat = New DevExpress.Utils.PointFloat(25.40002!, 0!)
        Me.xrLblArDrVal.Name = "xrLblArDrVal"
        Me.xrLblArDrVal.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArDrVal.SizeF = New System.Drawing.SizeF(470.9584!, 42.00002!)
        Me.xrLblArDrVal.StylePriority.UseFont = False
        Me.xrLblArDrVal.StylePriority.UseTextAlignment = False
        Me.xrLblArDrVal.Text = "—"
        Me.xrLblArDrVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrLblArDrCap
        '
        Me.xrLblArDrCap.Dpi = 254.0!
        Me.xrLblArDrCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblArDrCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblArDrCap.LocationFloat = New DevExpress.Utils.PointFloat(496.3584!, 3.5!)
        Me.xrLblArDrCap.Name = "xrLblArDrCap"
        Me.xrLblArDrCap.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblArDrCap.SizeF = New System.Drawing.SizeF(128.2417!, 35.0!)
        Me.xrLblArDrCap.StylePriority.UseFont = False
        Me.xrLblArDrCap.StylePriority.UseTextAlignment = False
        Me.xrLblArDrCap.Text = "الطبيب"
        Me.xrLblArDrCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xrPanelCenter
        '
        Me.xrPanelCenter.BackColor = System.Drawing.Color.Transparent
        Me.xrPanelCenter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrPicClinicLogo, Me.xrLblClinicNameAr, Me.xrLblClinicNameEn, Me.xrLblDocSubtitle})
        Me.xrPanelCenter.Dpi = 254.0!
        Me.xrPanelCenter.LocationFloat = New DevExpress.Utils.PointFloat(600.0001!, 25.00001!)
        Me.xrPanelCenter.Name = "xrPanelCenter"
        Me.xrPanelCenter.SizeF = New System.Drawing.SizeF(700.0001!, 450.0!)
        '
        'xrPicClinicLogo
        '
        Me.xrPicClinicLogo.Dpi = 254.0!
        Me.xrPicClinicLogo.LocationFloat = New DevExpress.Utils.PointFloat(225.0!, 183.58!)
        Me.xrPicClinicLogo.Name = "xrPicClinicLogo"
        Me.xrPicClinicLogo.SizeF = New System.Drawing.SizeF(250.0!, 250.0!)
        Me.xrPicClinicLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage
        '
        'xrLblClinicNameAr
        '
        Me.xrLblClinicNameAr.Dpi = 254.0!
        Me.xrLblClinicNameAr.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblClinicNameAr.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 133.58!)
        Me.xrLblClinicNameAr.Name = "xrLblClinicNameAr"
        Me.xrLblClinicNameAr.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
        Me.xrLblClinicNameAr.SizeF = New System.Drawing.SizeF(649.2!, 50.0!)
        Me.xrLblClinicNameAr.StylePriority.UseFont = False
        Me.xrLblClinicNameAr.Text = "—"
        Me.xrLblClinicNameAr.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrLblClinicNameEn
        '
        Me.xrLblClinicNameEn.Dpi = 254.0!
        Me.xrLblClinicNameEn.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblClinicNameEn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.xrLblClinicNameEn.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 78.58!)
        Me.xrLblClinicNameEn.Name = "xrLblClinicNameEn"
        Me.xrLblClinicNameEn.SizeF = New System.Drawing.SizeF(649.2!, 55.0!)
        Me.xrLblClinicNameEn.StylePriority.UseFont = False
        Me.xrLblClinicNameEn.Text = "—"
        Me.xrLblClinicNameEn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrLblDocSubtitle
        '
        Me.xrLblDocSubtitle.Dpi = 254.0!
        Me.xrLblDocSubtitle.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblDocSubtitle.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblDocSubtitle.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 25.4!)
        Me.xrLblDocSubtitle.Name = "xrLblDocSubtitle"
        Me.xrLblDocSubtitle.SizeF = New System.Drawing.SizeF(649.2!, 40.0!)
        Me.xrLblDocSubtitle.StylePriority.UseFont = False
        Me.xrLblDocSubtitle.Text = "SUPPLIER PURCHASE INVOICE"
        Me.xrLblDocSubtitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrPanelEng
        '
        Me.xrPanelEng.BackColor = System.Drawing.Color.Transparent
        Me.xrPanelEng.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLblEngEmailVal, Me.xrLblEngEmailCap, Me.xrLblEngMobileVal, Me.xrLblEngMobileCap, Me.xrLblEngPhoneVal, Me.xrLblEngPhoneCap, Me.xrLblEngAddrVal, Me.xrLblEngAddrCap, Me.xrLblEngSpecVal, Me.xrLblEngSpecCap, Me.xrLblEngDrVal, Me.xrLblEngDrCap})
        Me.xrPanelEng.Dpi = 254.0!
        Me.xrPanelEng.LocationFloat = New DevExpress.Utils.PointFloat(0!, 25.00001!)
        Me.xrPanelEng.Name = "xrPanelEng"
        Me.xrPanelEng.SizeF = New System.Drawing.SizeF(600.0!, 450.0!)
        '
        'xrLblEngEmailVal
        '
        Me.xrLblEngEmailVal.Dpi = 254.0!
        Me.xrLblEngEmailVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblEngEmailVal.LocationFloat = New DevExpress.Utils.PointFloat(153.6417!, 370.82!)
        Me.xrLblEngEmailVal.Name = "xrLblEngEmailVal"
        Me.xrLblEngEmailVal.SizeF = New System.Drawing.SizeF(421.3583!, 50.0!)
        Me.xrLblEngEmailVal.StylePriority.UseFont = False
        Me.xrLblEngEmailVal.Text = "—"
        '
        'xrLblEngEmailCap
        '
        Me.xrLblEngEmailCap.Dpi = 254.0!
        Me.xrLblEngEmailCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblEngEmailCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblEngEmailCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 378.3201!)
        Me.xrLblEngEmailCap.Name = "xrLblEngEmailCap"
        Me.xrLblEngEmailCap.SizeF = New System.Drawing.SizeF(128.2417!, 34.99994!)
        Me.xrLblEngEmailCap.StylePriority.UseFont = False
        Me.xrLblEngEmailCap.Text = "Email"
        Me.xrLblEngEmailCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft
        '
        'xrLblEngMobileVal
        '
        Me.xrLblEngMobileVal.Dpi = 254.0!
        Me.xrLblEngMobileVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblEngMobileVal.LocationFloat = New DevExpress.Utils.PointFloat(153.6417!, 324.82!)
        Me.xrLblEngMobileVal.Name = "xrLblEngMobileVal"
        Me.xrLblEngMobileVal.SizeF = New System.Drawing.SizeF(245.3999!, 38.0!)
        Me.xrLblEngMobileVal.StylePriority.UseFont = False
        Me.xrLblEngMobileVal.Text = "—"
        '
        'xrLblEngMobileCap
        '
        Me.xrLblEngMobileCap.Dpi = 254.0!
        Me.xrLblEngMobileCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblEngMobileCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblEngMobileCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 326.32!)
        Me.xrLblEngMobileCap.Name = "xrLblEngMobileCap"
        Me.xrLblEngMobileCap.SizeF = New System.Drawing.SizeF(128.2417!, 35.0!)
        Me.xrLblEngMobileCap.StylePriority.UseFont = False
        Me.xrLblEngMobileCap.Text = "Mobile"
        Me.xrLblEngMobileCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft
        '
        'xrLblEngPhoneVal
        '
        Me.xrLblEngPhoneVal.Dpi = 254.0!
        Me.xrLblEngPhoneVal.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!)
        Me.xrLblEngPhoneVal.LocationFloat = New DevExpress.Utils.PointFloat(153.6417!, 282.82!)
        Me.xrLblEngPhoneVal.Name = "xrLblEngPhoneVal"
        Me.xrLblEngPhoneVal.SizeF = New System.Drawing.SizeF(245.4!, 37.99997!)
        Me.xrLblEngPhoneVal.Text = "—"
        '
        'xrLblEngPhoneCap
        '
        Me.xrLblEngPhoneCap.Dpi = 254.0!
        Me.xrLblEngPhoneCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblEngPhoneCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblEngPhoneCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 284.32!)
        Me.xrLblEngPhoneCap.Name = "xrLblEngPhoneCap"
        Me.xrLblEngPhoneCap.SizeF = New System.Drawing.SizeF(128.2417!, 35.0!)
        Me.xrLblEngPhoneCap.StylePriority.UseFont = False
        Me.xrLblEngPhoneCap.Text = "Phone"
        Me.xrLblEngPhoneCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft
        '
        'xrLblEngAddrVal
        '
        Me.xrLblEngAddrVal.Dpi = 254.0!
        Me.xrLblEngAddrVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblEngAddrVal.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 200.82!)
        Me.xrLblEngAddrVal.Multiline = True
        Me.xrLblEngAddrVal.Name = "xrLblEngAddrVal"
        Me.xrLblEngAddrVal.SizeF = New System.Drawing.SizeF(550.0!, 80.0!)
        Me.xrLblEngAddrVal.StylePriority.UseFont = False
        Me.xrLblEngAddrVal.Text = "—"
        '
        'xrLblEngAddrCap
        '
        Me.xrLblEngAddrCap.Dpi = 254.0!
        Me.xrLblEngAddrCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblEngAddrCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblEngAddrCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 165.82!)
        Me.xrLblEngAddrCap.Name = "xrLblEngAddrCap"
        Me.xrLblEngAddrCap.SizeF = New System.Drawing.SizeF(550.0!, 35.0!)
        Me.xrLblEngAddrCap.StylePriority.UseFont = False
        Me.xrLblEngAddrCap.Text = "Address"
        Me.xrLblEngAddrCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft
        '
        'xrLblEngSpecVal
        '
        Me.xrLblEngSpecVal.Dpi = 254.0!
        Me.xrLblEngSpecVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblEngSpecVal.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 77.0!)
        Me.xrLblEngSpecVal.Name = "xrLblEngSpecVal"
        Me.xrLblEngSpecVal.SizeF = New System.Drawing.SizeF(550.0!, 80.0!)
        Me.xrLblEngSpecVal.StylePriority.UseFont = False
        Me.xrLblEngSpecVal.Text = "—"
        '
        'xrLblEngSpecCap
        '
        Me.xrLblEngSpecCap.Dpi = 254.0!
        Me.xrLblEngSpecCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblEngSpecCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblEngSpecCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 42.0!)
        Me.xrLblEngSpecCap.Name = "xrLblEngSpecCap"
        Me.xrLblEngSpecCap.SizeF = New System.Drawing.SizeF(470.9584!, 35.0!)
        Me.xrLblEngSpecCap.StylePriority.UseFont = False
        Me.xrLblEngSpecCap.Text = "Specialist"
        Me.xrLblEngSpecCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft
        '
        'xrLblEngDrVal
        '
        Me.xrLblEngDrVal.Dpi = 254.0!
        Me.xrLblEngDrVal.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblEngDrVal.LocationFloat = New DevExpress.Utils.PointFloat(153.6417!, 0!)
        Me.xrLblEngDrVal.Name = "xrLblEngDrVal"
        Me.xrLblEngDrVal.SizeF = New System.Drawing.SizeF(421.7583!, 42.0!)
        Me.xrLblEngDrVal.StylePriority.UseFont = False
        Me.xrLblEngDrVal.Text = "—"
        '
        'xrLblEngDrCap
        '
        Me.xrLblEngDrCap.Dpi = 254.0!
        Me.xrLblEngDrCap.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblEngDrCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblEngDrCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 3.500038!)
        Me.xrLblEngDrCap.Name = "xrLblEngDrCap"
        Me.xrLblEngDrCap.SizeF = New System.Drawing.SizeF(128.2417!, 35.0!)
        Me.xrLblEngDrCap.StylePriority.UseFont = False
        Me.xrLblEngDrCap.Text = "Doctor"
        Me.xrLblEngDrCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLineSign, Me.xrLblSignCap, Me.xrPanelGrand})
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 220.0!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'xrLineSign
        '
        Me.xrLineSign.Dpi = 254.0!
        Me.xrLineSign.LineWidth = 2.0!
        Me.xrLineSign.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 175.0!)
        Me.xrLineSign.Name = "xrLineSign"
        Me.xrLineSign.SizeF = New System.Drawing.SizeF(600.0!, 5.291667!)
        '
        'xrLblSignCap
        '
        Me.xrLblSignCap.Dpi = 254.0!
        Me.xrLblSignCap.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblSignCap.ForeColor = System.Drawing.Color.DimGray
        Me.xrLblSignCap.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 127.0!)
        Me.xrLblSignCap.Name = "xrLblSignCap"
        Me.xrLblSignCap.SizeF = New System.Drawing.SizeF(550.0!, 38.0!)
        Me.xrLblSignCap.StylePriority.UseFont = False
        Me.xrLblSignCap.Text = "Authorized signature"
        '
        'xrPanelGrand
        '
        Me.xrPanelGrand.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.xrPanelGrand.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.xrPanelGrand.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLblGrandTotalVal, Me.xrLblGrandTotalCap})
        Me.xrPanelGrand.Dpi = 254.0!
        Me.xrPanelGrand.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 12.7!)
        Me.xrPanelGrand.Name = "xrPanelGrand"
        Me.xrPanelGrand.SizeF = New System.Drawing.SizeF(1899.2!, 90.0!)
        '
        'xrLblGrandTotalVal
        '
        Me.xrLblGrandTotalVal.Dpi = 254.0!
        Me.xrLblGrandTotalVal.Font = New DevExpress.Drawing.DXFont("Calibri", 16.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblGrandTotalVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.xrLblGrandTotalVal.LocationFloat = New DevExpress.Utils.PointFloat(1360.0!, 14.725!)
        Me.xrLblGrandTotalVal.Name = "xrLblGrandTotalVal"
        Me.xrLblGrandTotalVal.SizeF = New System.Drawing.SizeF(500.0!, 60.0!)
        Me.xrLblGrandTotalVal.Text = "—"
        Me.xrLblGrandTotalVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'xrLblGrandTotalCap
        '
        Me.xrLblGrandTotalCap.Dpi = 254.0!
        Me.xrLblGrandTotalCap.Font = New DevExpress.Drawing.DXFont("Calibri", 12.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLblGrandTotalCap.LocationFloat = New DevExpress.Utils.PointFloat(1000.0!, 22.225!)
        Me.xrLblGrandTotalCap.Name = "xrLblGrandTotalCap"
        Me.xrLblGrandTotalCap.SizeF = New System.Drawing.SizeF(350.0!, 45.0!)
        Me.xrLblGrandTotalCap.Text = "Grand total"
        Me.xrLblGrandTotalCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrTableDetail})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 103.8041!
        Me.Detail.Name = "Detail"
        '
        'xrTableDetail
        '
        Me.xrTableDetail.Dpi = 254.0!
        Me.xrTableDetail.LocationFloat = New DevExpress.Utils.PointFloat(25.4!, 0!)
        Me.xrTableDetail.Name = "xrTableDetail"
        Me.xrTableDetail.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.xrRowDetail})
        Me.xrTableDetail.SizeF = New System.Drawing.SizeF(1899.2!, 65.0!)
        '
        'xrRowDetail
        '
        Me.xrRowDetail.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrCellProduct, Me.xrCellBatch, Me.xrCellExp, Me.xrCellQty, Me.xrCellUnit, Me.xrCellLineTotal})
        Me.xrRowDetail.Dpi = 254.0!
        Me.xrRowDetail.EvenStyleName = "styleDetailEven"
        Me.xrRowDetail.Name = "xrRowDetail"
        Me.xrRowDetail.OddStyleName = "styleDetail"
        Me.xrRowDetail.Weight = 1.0R
        '
        'xrCellProduct
        '
        Me.xrCellProduct.Dpi = 254.0!
        Me.xrCellProduct.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ProductName]")})
        Me.xrCellProduct.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrCellProduct.Name = "xrCellProduct"
        Me.xrCellProduct.StyleName = "styleDetail"
        Me.xrCellProduct.StylePriority.UseFont = False
        Me.xrCellProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.xrCellProduct.Weight = 0.32R
        '
        'xrCellBatch
        '
        Me.xrCellBatch.Dpi = 254.0!
        Me.xrCellBatch.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BatchNumber]")})
        Me.xrCellBatch.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrCellBatch.Name = "xrCellBatch"
        Me.xrCellBatch.StyleName = "styleDetail"
        Me.xrCellBatch.StylePriority.UseFont = False
        Me.xrCellBatch.Weight = 0.14R
        '
        'xrCellExp
        '
        Me.xrCellExp.Dpi = 254.0!
        Me.xrCellExp.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ExpirationDate]")})
        Me.xrCellExp.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrCellExp.Name = "xrCellExp"
        Me.xrCellExp.StyleName = "styleDetail"
        Me.xrCellExp.StylePriority.UseFont = False
        Me.xrCellExp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.xrCellExp.TextFormatString = "{0:dd/MM/yyyy}"
        Me.xrCellExp.Weight = 0.12R
        '
        'xrCellQty
        '
        Me.xrCellQty.Dpi = 254.0!
        Me.xrCellQty.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Quantity]")})
        Me.xrCellQty.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrCellQty.Name = "xrCellQty"
        Me.xrCellQty.StyleName = "styleDetail"
        Me.xrCellQty.StylePriority.UseFont = False
        Me.xrCellQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.xrCellQty.Weight = 0.07R
        '
        'xrCellUnit
        '
        Me.xrCellUnit.Dpi = 254.0!
        Me.xrCellUnit.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UnitPrice]")})
        Me.xrCellUnit.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrCellUnit.Name = "xrCellUnit"
        Me.xrCellUnit.StyleName = "styleDetail"
        Me.xrCellUnit.StylePriority.UseFont = False
        Me.xrCellUnit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.xrCellUnit.TextFormatString = "{0:N2}"
        Me.xrCellUnit.Weight = 0.13R
        '
        'xrCellLineTotal
        '
        Me.xrCellLineTotal.Dpi = 254.0!
        Me.xrCellLineTotal.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[LineTotal]")})
        Me.xrCellLineTotal.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrCellLineTotal.Name = "xrCellLineTotal"
        Me.xrCellLineTotal.StyleName = "styleDetail"
        Me.xrCellLineTotal.StylePriority.UseFont = False
        Me.xrCellLineTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.xrCellLineTotal.TextFormatString = "{0:N2}"
        Me.xrCellLineTotal.Weight = 0.22R
        '
        'SupplierInvoice
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.ReportFooter, Me.Detail})
        Me.Dpi = 254.0!
        Me.Font = New DevExpress.Drawing.DXFont("Calibri", 9.0!)
        Me.Margins = New DevExpress.Drawing.DXMargins(56.0!, 56.0!, 50.8!, 76.2!)
        Me.PageHeightF = 2970.0!
        Me.PageWidthF = 2100.0!
        Me.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.RequestParameters = False
        Me.SnapGridSize = 25.0!
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.styleTableHeader, Me.styleDetail, Me.styleDetailEven, Me.styleFooterMuted})
        Me.Version = "25.1"
        XrWatermark1.Id = "Watermark1"
        Me.Watermarks.AddRange(New DevExpress.XtraPrinting.Drawing.Watermark() {XrWatermark1})
        CType(Me.xrTableHead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xrTableDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents styleTableHeader As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents styleDetail As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents styleDetailEven As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents styleFooterMuted As DevExpress.XtraReports.UI.XRControlStyle

    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents xrPanelClinicBand As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrPanelEng As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrLblEngDrCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblEngDrVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblEngSpecCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblEngSpecVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblEngAddrCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblEngAddrVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblEngPhoneCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblEngPhoneVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblEngMobileCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblEngMobileVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblEngEmailCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblEngEmailVal As DevExpress.XtraReports.UI.XRLabel

    Friend WithEvents xrPanelCenter As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrLblDocSubtitle As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblClinicNameEn As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblClinicNameAr As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrPicClinicLogo As DevExpress.XtraReports.UI.XRPictureBox

    Friend WithEvents xrPanelAr As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrLblArDrCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblArDrVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblArSpecCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblArSpecVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblArAddrCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblArAddrVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblArPhoneCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblArPhoneVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblArMobileCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblArMobileVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblArEmailCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblArEmailVal As DevExpress.XtraReports.UI.XRLabel

    Friend WithEvents xrPanelSupplier As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrLblSupplierBanner As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupNameCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupNameVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupContactCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupContactVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupPhoneCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupPhoneVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupEmailCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupEmailVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupAddrCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupAddrVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupPayCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSupPayVal As DevExpress.XtraReports.UI.XRLabel

    Friend WithEvents xrPanelInvoice As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrLblInvoiceBanner As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblInvNoCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblInvNoVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblInvDateCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblInvDateVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblInvDueCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblInvDueVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblInvStatusCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblInvStatusVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblInvTotalCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblInvTotalVal As DevExpress.XtraReports.UI.XRLabel

    Friend WithEvents xrPanelColHead As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrTableHead As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents xrRowHead As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents xrCellHProduct As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrCellHBatch As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrCellHExp As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrCellHQty As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrCellHUnit As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrCellHTotal As DevExpress.XtraReports.UI.XRTableCell

    Friend WithEvents xrTableDetail As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents xrRowDetail As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents xrCellProduct As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrCellBatch As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrCellExp As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrCellQty As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrCellUnit As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrCellLineTotal As DevExpress.XtraReports.UI.XRTableCell

    Friend WithEvents xrPanelGrand As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrLblGrandTotalCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblGrandTotalVal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLblSignCap As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLineSign As DevExpress.XtraReports.UI.XRLine

    Friend WithEvents pageInfoDate As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents pageInfoPages As DevExpress.XtraReports.UI.XRPageInfo
End Class
