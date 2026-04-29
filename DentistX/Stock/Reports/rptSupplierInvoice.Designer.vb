<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptSupplierInvoice
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

    Private _reportFont As System.Drawing.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim XrWatermark1 As DevExpress.XtraReports.UI.XRWatermark = New DevExpress.XtraReports.UI.XRWatermark()
        Me.parClinicName = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parClinicAddress = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parClinicPhone = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parSupplierName = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parInvoiceNumber = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parInvoiceDate = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parDueDate = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parInvoiceStatus = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parTotalAmount = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parShowPayments = New DevExpress.XtraReports.Parameters.Parameter()
        Me.parPaymentsText = New DevExpress.XtraReports.Parameters.Parameter()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.pageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.xrPanelClinic = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrLabelTitle = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelClinicName = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelClinicAddress = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelClinicPhone = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrPanelInfo = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrLabelSupplierCaption = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelSupplierValue = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelInvoiceCaption = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelInvoiceValue = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelInvoiceDateCaption = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelInvoiceDateValue = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelDueDateCaption = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelDueDateValue = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelStatusCaption = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelStatusValue = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelTotalCaption = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelTotalValue = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrPanelTableHeader = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrTableHeader = New DevExpress.XtraReports.UI.XRTable()
        Me.xrTableHeaderRow = New DevExpress.XtraReports.UI.XRTableRow()
        Me.xrTableCellHeaderProduct = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrTableCellHeaderBatch = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrTableCellHeaderExp = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrTableCellHeaderQty = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrTableCellHeaderUnit = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrTableCellHeaderTotal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.xrLabelGrandTotalCaption = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelGrandTotalValue = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelPaymentsCaption = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrPanelPayments = New DevExpress.XtraReports.UI.XRPanel()
        Me.xrLabelPaymentsText = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLabelSignatureCaption = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrLineSignature = New DevExpress.XtraReports.UI.XRLine()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.xrTableDetail = New DevExpress.XtraReports.UI.XRTable()
        Me.xrTableDetailRow = New DevExpress.XtraReports.UI.XRTableRow()
        Me.xrTableCellProduct = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrTableCellBatch = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrTableCellExp = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrTableCellQty = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrTableCellUnit = New DevExpress.XtraReports.UI.XRTableCell()
        Me.xrTableCellLineTotal = New DevExpress.XtraReports.UI.XRTableCell()
        CType(Me.xrTableHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xrTableDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'parClinicName
        '
        Me.parClinicName.Name = "parClinicName"
        '
        'parClinicAddress
        '
        Me.parClinicAddress.Name = "parClinicAddress"
        '
        'parClinicPhone
        '
        Me.parClinicPhone.Name = "parClinicPhone"
        '
        'parSupplierName
        '
        Me.parSupplierName.Name = "parSupplierName"
        '
        'parInvoiceNumber
        '
        Me.parInvoiceNumber.Name = "parInvoiceNumber"
        Me.parInvoiceNumber.Type = GetType(Integer)
        '
        'parInvoiceDate
        '
        Me.parInvoiceDate.Name = "parInvoiceDate"
        Me.parInvoiceDate.Type = GetType(Date)
        '
        'parDueDate
        '
        Me.parDueDate.Name = "parDueDate"
        Me.parDueDate.Type = GetType(Date)
        '
        'parInvoiceStatus
        '
        Me.parInvoiceStatus.Name = "parInvoiceStatus"
        '
        'parTotalAmount
        '
        Me.parTotalAmount.Name = "parTotalAmount"
        Me.parTotalAmount.Type = GetType(Decimal)
        '
        'parShowPayments
        '
        Me.parShowPayments.Name = "parShowPayments"
        Me.parShowPayments.Type = GetType(Boolean)
        '
        'parPaymentsText
        '
        Me.parPaymentsText.Name = "parPaymentsText"
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.HeightF = 50.8!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.pageInfo1})
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.HeightF = 520.3825!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'pageInfo1
        '
        Me.pageInfo1.Dpi = 254.0!
        Me.pageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(24.3416!, 52.91667!)
        Me.pageInfo1.Name = "pageInfo1"
        Me.pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.pageInfo1.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        Me.pageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.pageInfo1.TextFormatString = "{0:yyyy-MM-dd}"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrPanelClinic, Me.xrPanelInfo, Me.xrPanelTableHeader})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 480.5891!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'xrPanelClinic
        '
        Me.xrPanelClinic.BorderWidth = 0!
        Me.xrPanelClinic.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLabelTitle, Me.xrLabelClinicName, Me.xrLabelClinicAddress, Me.xrLabelClinicPhone})
        Me.xrPanelClinic.Dpi = 254.0!
        Me.xrPanelClinic.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.xrPanelClinic.Name = "xrPanelClinic"
        Me.xrPanelClinic.SizeF = New System.Drawing.SizeF(1950.0!, 156.1041!)
        '
        'xrLabelTitle
        '
        Me.xrLabelTitle.Dpi = 254.0!
        Me.xrLabelTitle.Font = New DevExpress.Drawing.DXFont("Calibri", 16.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLabelTitle.LocationFloat = New DevExpress.Utils.PointFloat(50.8!, 12.7!)
        Me.xrLabelTitle.Name = "xrLabelTitle"
        Me.xrLabelTitle.SizeF = New System.Drawing.SizeF(1016.0!, 50.8!)
        Me.xrLabelTitle.Text = "SUPPLIER INVOICE"
        '
        'xrLabelClinicName
        '
        Me.xrLabelClinicName.Dpi = 254.0!
        Me.xrLabelClinicName.LocationFloat = New DevExpress.Utils.PointFloat(50.8!, 68.58!)
        Me.xrLabelClinicName.Name = "xrLabelClinicName"
        Me.xrLabelClinicName.SizeF = New System.Drawing.SizeF(1524.0!, 38.1!)
        Me.xrLabelClinicName.Text = "-"
        '
        'xrLabelClinicAddress
        '
        Me.xrLabelClinicAddress.Dpi = 254.0!
        Me.xrLabelClinicAddress.LocationFloat = New DevExpress.Utils.PointFloat(50.80001!, 106.68!)
        Me.xrLabelClinicAddress.Name = "xrLabelClinicAddress"
        Me.xrLabelClinicAddress.SizeF = New System.Drawing.SizeF(1874.2!, 38.09999!)
        Me.xrLabelClinicAddress.Text = "-"
        '
        'xrLabelClinicPhone
        '
        Me.xrLabelClinicPhone.Dpi = 254.0!
        Me.xrLabelClinicPhone.LocationFloat = New DevExpress.Utils.PointFloat(1574.8!, 68.57999!)
        Me.xrLabelClinicPhone.Name = "xrLabelClinicPhone"
        Me.xrLabelClinicPhone.SizeF = New System.Drawing.SizeF(350.2001!, 38.10001!)
        Me.xrLabelClinicPhone.Text = "-"
        '
        'xrPanelInfo
        '
        Me.xrPanelInfo.BorderWidth = 1.0!
        Me.xrPanelInfo.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLabelTotalValue, Me.xrLabelSupplierCaption, Me.xrLabelSupplierValue, Me.xrLabelInvoiceCaption, Me.xrLabelInvoiceValue, Me.xrLabelInvoiceDateCaption, Me.xrLabelInvoiceDateValue, Me.xrLabelDueDateCaption, Me.xrLabelDueDateValue, Me.xrLabelStatusCaption, Me.xrLabelStatusValue, Me.xrLabelTotalCaption})
        Me.xrPanelInfo.Dpi = 254.0!
        Me.xrPanelInfo.LocationFloat = New DevExpress.Utils.PointFloat(0!, 156.104!)
        Me.xrPanelInfo.Name = "xrPanelInfo"
        Me.xrPanelInfo.SizeF = New System.Drawing.SizeF(1950.0!, 221.5093!)
        '
        'xrLabelSupplierCaption
        '
        Me.xrLabelSupplierCaption.Dpi = 254.0!
        Me.xrLabelSupplierCaption.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 5.079956!)
        Me.xrLabelSupplierCaption.Name = "xrLabelSupplierCaption"
        Me.xrLabelSupplierCaption.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrLabelSupplierValue
        '
        Me.xrLabelSupplierValue.Dpi = 254.0!
        Me.xrLabelSupplierValue.LocationFloat = New DevExpress.Utils.PointFloat(304.8!, 5.079956!)
        Me.xrLabelSupplierValue.Name = "xrLabelSupplierValue"
        Me.xrLabelSupplierValue.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrLabelInvoiceCaption
        '
        Me.xrLabelInvoiceCaption.Dpi = 254.0!
        Me.xrLabelInvoiceCaption.LocationFloat = New DevExpress.Utils.PointFloat(1320.8!, 25.0!)
        Me.xrLabelInvoiceCaption.Name = "xrLabelInvoiceCaption"
        Me.xrLabelInvoiceCaption.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrLabelInvoiceValue
        '
        Me.xrLabelInvoiceValue.Dpi = 254.0!
        Me.xrLabelInvoiceValue.LocationFloat = New DevExpress.Utils.PointFloat(1600.6!, 24.99992!)
        Me.xrLabelInvoiceValue.Name = "xrLabelInvoiceValue"
        Me.xrLabelInvoiceValue.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrLabelInvoiceDateCaption
        '
        Me.xrLabelInvoiceDateCaption.Dpi = 254.0!
        Me.xrLabelInvoiceDateCaption.LocationFloat = New DevExpress.Utils.PointFloat(24.3416!, 100.5417!)
        Me.xrLabelInvoiceDateCaption.Name = "xrLabelInvoiceDateCaption"
        Me.xrLabelInvoiceDateCaption.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrLabelInvoiceDateValue
        '
        Me.xrLabelInvoiceDateValue.Dpi = 254.0!
        Me.xrLabelInvoiceDateValue.LocationFloat = New DevExpress.Utils.PointFloat(278.3416!, 100.5417!)
        Me.xrLabelInvoiceDateValue.Name = "xrLabelInvoiceDateValue"
        Me.xrLabelInvoiceDateValue.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrLabelDueDateCaption
        '
        Me.xrLabelDueDateCaption.Dpi = 254.0!
        Me.xrLabelDueDateCaption.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 158.9617!)
        Me.xrLabelDueDateCaption.Name = "xrLabelDueDateCaption"
        Me.xrLabelDueDateCaption.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrLabelDueDateValue
        '
        Me.xrLabelDueDateValue.Dpi = 254.0!
        Me.xrLabelDueDateValue.LocationFloat = New DevExpress.Utils.PointFloat(279.0!, 158.9617!)
        Me.xrLabelDueDateValue.Name = "xrLabelDueDateValue"
        Me.xrLabelDueDateValue.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrLabelStatusCaption
        '
        Me.xrLabelStatusCaption.Dpi = 254.0!
        Me.xrLabelStatusCaption.LocationFloat = New DevExpress.Utils.PointFloat(1134.721!, 100.5417!)
        Me.xrLabelStatusCaption.Name = "xrLabelStatusCaption"
        Me.xrLabelStatusCaption.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrLabelStatusValue
        '
        Me.xrLabelStatusValue.Dpi = 254.0!
        Me.xrLabelStatusValue.LocationFloat = New DevExpress.Utils.PointFloat(1394.092!, 100.5417!)
        Me.xrLabelStatusValue.Name = "xrLabelStatusValue"
        Me.xrLabelStatusValue.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrLabelTotalCaption
        '
        Me.xrLabelTotalCaption.Dpi = 254.0!
        Me.xrLabelTotalCaption.LocationFloat = New DevExpress.Utils.PointFloat(1130.791!, 158.9617!)
        Me.xrLabelTotalCaption.Name = "xrLabelTotalCaption"
        Me.xrLabelTotalCaption.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrLabelTotalValue
        '
        Me.xrLabelTotalValue.Dpi = 254.0!
        Me.xrLabelTotalValue.LocationFloat = New DevExpress.Utils.PointFloat(1394.092!, 158.9617!)
        Me.xrLabelTotalValue.Name = "xrLabelTotalValue"
        Me.xrLabelTotalValue.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'xrPanelTableHeader
        '
        Me.xrPanelTableHeader.BorderWidth = 1.0!
        Me.xrPanelTableHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrTableHeader})
        Me.xrPanelTableHeader.Dpi = 254.0!
        Me.xrPanelTableHeader.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 377.6133!)
        Me.xrPanelTableHeader.Name = "xrPanelTableHeader"
        Me.xrPanelTableHeader.SizeF = New System.Drawing.SizeF(1915.0!, 84.98419!)
        '
        'xrTableHeader
        '
        Me.xrTableHeader.Dpi = 254.0!
        Me.xrTableHeader.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0.0001220703!)
        Me.xrTableHeader.Name = "xrTableHeader"
        Me.xrTableHeader.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.xrTableHeaderRow})
        Me.xrTableHeader.SizeF = New System.Drawing.SizeF(1915.0!, 84.98407!)
        '
        'xrTableHeaderRow
        '
        Me.xrTableHeaderRow.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCellHeaderProduct, Me.xrTableCellHeaderBatch, Me.xrTableCellHeaderExp, Me.xrTableCellHeaderQty, Me.xrTableCellHeaderUnit, Me.xrTableCellHeaderTotal})
        Me.xrTableHeaderRow.Dpi = 254.0!
        Me.xrTableHeaderRow.Name = "xrTableHeaderRow"
        Me.xrTableHeaderRow.Weight = 1.0454545454545454R
        '
        'xrTableCellHeaderProduct
        '
        Me.xrTableCellHeaderProduct.Dpi = 254.0!
        Me.xrTableCellHeaderProduct.Name = "xrTableCellHeaderProduct"
        Me.xrTableCellHeaderProduct.Text = "Product"
        Me.xrTableCellHeaderProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.xrTableCellHeaderProduct.Weight = 0.309530621393779R
        '
        'xrTableCellHeaderBatch
        '
        Me.xrTableCellHeaderBatch.Dpi = 254.0!
        Me.xrTableCellHeaderBatch.Name = "xrTableCellHeaderBatch"
        Me.xrTableCellHeaderBatch.Text = "Batch"
        Me.xrTableCellHeaderBatch.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.xrTableCellHeaderBatch.Weight = 0.14893359040729276R
        '
        'xrTableCellHeaderExp
        '
        Me.xrTableCellHeaderExp.Dpi = 254.0!
        Me.xrTableCellHeaderExp.Name = "xrTableCellHeaderExp"
        Me.xrTableCellHeaderExp.Text = "Exp."
        Me.xrTableCellHeaderExp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.xrTableCellHeaderExp.Weight = 0.11170021099366589R
        '
        'xrTableCellHeaderQty
        '
        Me.xrTableCellHeaderQty.Dpi = 254.0!
        Me.xrTableCellHeaderQty.Name = "xrTableCellHeaderQty"
        Me.xrTableCellHeaderQty.Text = "Qty"
        Me.xrTableCellHeaderQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.xrTableCellHeaderQty.Weight = 0.068261155740982238R
        '
        'xrTableCellHeaderUnit
        '
        Me.xrTableCellHeaderUnit.Dpi = 254.0!
        Me.xrTableCellHeaderUnit.Name = "xrTableCellHeaderUnit"
        Me.xrTableCellHeaderUnit.Text = "Unit"
        Me.xrTableCellHeaderUnit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.xrTableCellHeaderUnit.Weight = 0.13652263217417293R
        '
        'xrTableCellHeaderTotal
        '
        Me.xrTableCellHeaderTotal.Dpi = 254.0!
        Me.xrTableCellHeaderTotal.Name = "xrTableCellHeaderTotal"
        Me.xrTableCellHeaderTotal.Text = "Line Total"
        Me.xrTableCellHeaderTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.xrTableCellHeaderTotal.Weight = 0.23829386197416683R
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLabelGrandTotalCaption, Me.xrLabelGrandTotalValue, Me.xrLabelPaymentsCaption, Me.xrPanelPayments, Me.xrLabelSignatureCaption, Me.xrLineSignature})
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 295.381!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'xrLabelGrandTotalCaption
        '
        Me.xrLabelGrandTotalCaption.Dpi = 254.0!
        Me.xrLabelGrandTotalCaption.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLabelGrandTotalCaption.LocationFloat = New DevExpress.Utils.PointFloat(50.8!, 25.0!)
        Me.xrLabelGrandTotalCaption.Name = "xrLabelGrandTotalCaption"
        Me.xrLabelGrandTotalCaption.SizeF = New System.Drawing.SizeF(381.0!, 38.1!)
        Me.xrLabelGrandTotalCaption.Text = "Grand Total:"
        '
        'xrLabelGrandTotalValue
        '
        Me.xrLabelGrandTotalValue.Dpi = 254.0!
        Me.xrLabelGrandTotalValue.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLabelGrandTotalValue.LocationFloat = New DevExpress.Utils.PointFloat(431.8!, 25.0!)
        Me.xrLabelGrandTotalValue.Name = "xrLabelGrandTotalValue"
        Me.xrLabelGrandTotalValue.SizeF = New System.Drawing.SizeF(508.0!, 38.1!)
        Me.xrLabelGrandTotalValue.Text = "-"
        '
        'xrLabelPaymentsCaption
        '
        Me.xrLabelPaymentsCaption.Dpi = 254.0!
        Me.xrLabelPaymentsCaption.Font = New DevExpress.Drawing.DXFont("Calibri", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.xrLabelPaymentsCaption.LocationFloat = New DevExpress.Utils.PointFloat(50.8!, 63.10001!)
        Me.xrLabelPaymentsCaption.Name = "xrLabelPaymentsCaption"
        Me.xrLabelPaymentsCaption.SizeF = New System.Drawing.SizeF(508.0!, 38.1!)
        Me.xrLabelPaymentsCaption.Text = "Payments"
        Me.xrLabelPaymentsCaption.Visible = False
        '
        'xrPanelPayments
        '
        Me.xrPanelPayments.BorderWidth = 1.0!
        Me.xrPanelPayments.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrLabelPaymentsText})
        Me.xrPanelPayments.Dpi = 254.0!
        Me.xrPanelPayments.LocationFloat = New DevExpress.Utils.PointFloat(50.80001!, 101.2!)
        Me.xrPanelPayments.Name = "xrPanelPayments"
        Me.xrPanelPayments.SizeF = New System.Drawing.SizeF(1899.2!, 139.7!)
        Me.xrPanelPayments.Visible = False
        '
        'xrLabelPaymentsText
        '
        Me.xrLabelPaymentsText.Dpi = 254.0!
        Me.xrLabelPaymentsText.LocationFloat = New DevExpress.Utils.PointFloat(25.40001!, 25.0!)
        Me.xrLabelPaymentsText.Name = "xrLabelPaymentsText"
        Me.xrLabelPaymentsText.SizeF = New System.Drawing.SizeF(1873.8!, 88.90002!)
        Me.xrLabelPaymentsText.Text = "-"
        '
        'xrLabelSignatureCaption
        '
        Me.xrLabelSignatureCaption.Dpi = 254.0!
        Me.xrLabelSignatureCaption.LocationFloat = New DevExpress.Utils.PointFloat(50.79999!, 240.9!)
        Me.xrLabelSignatureCaption.Name = "xrLabelSignatureCaption"
        Me.xrLabelSignatureCaption.SizeF = New System.Drawing.SizeF(635.0!, 38.10001!)
        Me.xrLabelSignatureCaption.Text = "Authorized Signature"
        '
        'xrLineSignature
        '
        Me.xrLineSignature.Dpi = 254.0!
        Me.xrLineSignature.LineWidth = 2.0!
        Me.xrLineSignature.LocationFloat = New DevExpress.Utils.PointFloat(50.79999!, 279.0!)
        Me.xrLineSignature.Name = "xrLineSignature"
        Me.xrLineSignature.SizeF = New System.Drawing.SizeF(660.4!, 5.291687!)
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrTableDetail})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 110.1725!
        Me.Detail.HierarchyPrintOptions.Indent = 50.8!
        Me.Detail.Name = "Detail"
        '
        'xrTableDetail
        '
        Me.xrTableDetail.Dpi = 254.0!
        Me.xrTableDetail.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.xrTableDetail.Name = "xrTableDetail"
        Me.xrTableDetail.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.xrTableDetailRow})
        Me.xrTableDetail.SizeF = New System.Drawing.SizeF(1950.0!, 62.63!)
        '
        'xrTableDetailRow
        '
        Me.xrTableDetailRow.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTableCellProduct, Me.xrTableCellBatch, Me.xrTableCellExp, Me.xrTableCellQty, Me.xrTableCellUnit, Me.xrTableCellLineTotal})
        Me.xrTableDetailRow.Dpi = 254.0!
        Me.xrTableDetailRow.Name = "xrTableDetailRow"
        Me.xrTableDetailRow.Weight = 1.0454545454545454R
        '
        'xrTableCellProduct
        '
        Me.xrTableCellProduct.Dpi = 254.0!
        Me.xrTableCellProduct.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ProductName]")})
        Me.xrTableCellProduct.Name = "xrTableCellProduct"
        Me.xrTableCellProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.xrTableCellProduct.Weight = 0.31438935912938332R
        '
        'xrTableCellBatch
        '
        Me.xrTableCellBatch.Dpi = 254.0!
        Me.xrTableCellBatch.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BatchNumber]")})
        Me.xrTableCellBatch.Name = "xrTableCellBatch"
        Me.xrTableCellBatch.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.xrTableCellBatch.Weight = 0.14510278113663846R
        '
        'xrTableCellExp
        '
        Me.xrTableCellExp.Dpi = 254.0!
        Me.xrTableCellExp.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ExpirationDate]")})
        Me.xrTableCellExp.Name = "xrTableCellExp"
        Me.xrTableCellExp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.xrTableCellExp.TextFormatString = "{0:dd/MM/yyyy}"
        Me.xrTableCellExp.Weight = 0.10882708585247884R
        '
        'xrTableCellQty
        '
        Me.xrTableCellQty.Dpi = 254.0!
        Me.xrTableCellQty.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Quantity]")})
        Me.xrTableCellQty.Name = "xrTableCellQty"
        Me.xrTableCellQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.xrTableCellQty.Weight = 0.066505441354292621R
        '
        'xrTableCellUnit
        '
        Me.xrTableCellUnit.Dpi = 254.0!
        Me.xrTableCellUnit.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UnitPrice]")})
        Me.xrTableCellUnit.Name = "xrTableCellUnit"
        Me.xrTableCellUnit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.xrTableCellUnit.TextFormatString = "{0:n2}"
        Me.xrTableCellUnit.Weight = 0.13301088270858524R
        '
        'xrTableCellLineTotal
        '
        Me.xrTableCellLineTotal.Dpi = 254.0!
        Me.xrTableCellLineTotal.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif(IsNull([Quantity]), 0, [Quantity]) * Iif(IsNull([UnitPrice]), 0, [UnitPrice])" &
                    "")})
        Me.xrTableCellLineTotal.Name = "xrTableCellLineTotal"
        Me.xrTableCellLineTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.xrTableCellLineTotal.TextFormatString = "{0:n2}"
        Me.xrTableCellLineTotal.Weight = 0.23216444981862153R
        '
        'rptSupplierInvoice2
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.ReportFooter, Me.Detail})
        Me.Dpi = 254.0!
        Me.Margins = New DevExpress.Drawing.DXMargins(56.0!, 79.0!, 50.8!, 520.3825!)
        Me.PageHeightF = 2970.0!
        Me.PageWidthF = 2100.0!
        Me.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.parClinicName, Me.parClinicAddress, Me.parClinicPhone, Me.parSupplierName, Me.parInvoiceNumber, Me.parInvoiceDate, Me.parDueDate, Me.parInvoiceStatus, Me.parTotalAmount, Me.parShowPayments, Me.parPaymentsText})
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.SnapGridSize = 25.0!
        Me.Version = "25.1"
        XrWatermark1.Font = New DevExpress.Drawing.DXFont("Calibri", 48.0!, DevExpress.Drawing.DXFontStyle.Bold)
        XrWatermark1.Id = "Watermark1"
        XrWatermark1.Text = "SUPPLIER INVOICE"
        Me.Watermarks.AddRange(New DevExpress.XtraPrinting.Drawing.Watermark() {XrWatermark1})
        CType(Me.xrTableHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xrTableDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents parClinicName As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents parClinicAddress As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents parClinicPhone As DevExpress.XtraReports.Parameters.Parameter

    Friend WithEvents parSupplierName As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents parInvoiceNumber As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents parInvoiceDate As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents parDueDate As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents parInvoiceStatus As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents parTotalAmount As DevExpress.XtraReports.Parameters.Parameter

    Friend WithEvents parShowPayments As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents parPaymentsText As DevExpress.XtraReports.Parameters.Parameter

    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand

    Friend WithEvents xrPanelClinic As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrLabelClinicName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelClinicAddress As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelClinicPhone As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelTitle As DevExpress.XtraReports.UI.XRLabel

    Friend WithEvents xrPanelInfo As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrLabelSupplierCaption As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelSupplierValue As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelInvoiceCaption As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelInvoiceValue As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelInvoiceDateCaption As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelInvoiceDateValue As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelDueDateCaption As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelDueDateValue As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelStatusCaption As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelStatusValue As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelTotalCaption As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelTotalValue As DevExpress.XtraReports.UI.XRLabel

    Friend WithEvents xrPanelTableHeader As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrTableHeader As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents xrTableHeaderRow As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents xrTableCellHeaderProduct As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrTableCellHeaderBatch As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrTableCellHeaderExp As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrTableCellHeaderQty As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrTableCellHeaderUnit As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrTableCellHeaderTotal As DevExpress.XtraReports.UI.XRTableCell

    Friend WithEvents xrTableDetail As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents xrTableDetailRow As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents xrTableCellProduct As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrTableCellBatch As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrTableCellExp As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrTableCellQty As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrTableCellUnit As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrTableCellLineTotal As DevExpress.XtraReports.UI.XRTableCell

    Friend WithEvents xrLabelPaymentsCaption As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrPanelPayments As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents xrLabelPaymentsText As DevExpress.XtraReports.UI.XRLabel

    Friend WithEvents xrLabelGrandTotalCaption As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLabelGrandTotalValue As DevExpress.XtraReports.UI.XRLabel

    Friend WithEvents xrLabelSignatureCaption As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrLineSignature As DevExpress.XtraReports.UI.XRLine

    Friend WithEvents pageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
End Class

