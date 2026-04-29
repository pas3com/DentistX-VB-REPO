
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources

Public Class rptPatientInvoiceAr
    Inherits XtraReport

    Private invoiceID As Integer
    Private connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private isArabic As Boolean = False
    Private resManager As ResourceManager

    Public Sub New(ByVal invID As Integer, Optional ByVal language As String = "en", Optional ByVal clnc As Clinic = Nothing, Optional ByVal pat As Patient = Nothing)
        InitializeComponent()
        InitializeComponent1()
        invoiceID = invID

        ' Set language
        isArabic = (language.ToLower() = "ar")
        resManager = New ResourceManager("DentalSystem.Resources.InvoiceResources", GetType(rptPatientInvoice).Assembly)

        LoadData()
        ApplyLanguage()
    End Sub

    Private Sub InitializeComponent1()
        ' Report initialization
        Me.Landscape = False
        Me.Margins = New System.Drawing.Printing.Margins(10, 10, 10, 10)
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4

        ' Set RTL for Arabic
        If isArabic Then
            Me.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes
            Me.RightToLeftLayout = True
            Me.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        End If

        CreateReportLayout()
    End Sub

    Private Sub ApplyLanguage()
        ' This would be populated from resource files
        ' For now, we'll use hardcoded values with conditional logic
    End Sub

    Private Function GetLocalizedText(key As String) As String
        If resManager IsNot Nothing Then
            Dim culture As CultureInfo = If(isArabic, SettingsRuntimeApply.CreateArabicRegionalCultureGregorian(), CultureInfo.InvariantCulture)
            Dim text = resManager.GetString(key, culture)
            Return If(String.IsNullOrEmpty(text), key, text)
        End If
        Return key
    End Function

    Private Sub CreateReportLayout()
        ' ============ Report Bands ============
        Dim reportHeader As ReportHeaderBand = New ReportHeaderBand()
        Dim pageHeader As PageHeaderBand = New PageHeaderBand()
        Dim detail As DetailBand = New DetailBand()
        Dim detailReportItems As DetailReportBand = New DetailReportBand()
        Dim detailReportPayments As DetailReportBand = New DetailReportBand()
        Dim reportFooter As ReportFooterBand = New ReportFooterBand()
        Dim pageFooter As PageFooterBand = New PageFooterBand()

        Me.Bands.AddRange(New Band() {
        reportHeader,
        pageHeader,
        detail,
        detailReportItems,
        detailReportPayments,
        reportFooter,
        pageFooter
    })

        ' ============ Clinic Header with Logo ============
        ' Clinic Logo
        Dim picLogo As XRPictureBox = New XRPictureBox()
        picLogo.Image = LoadClinicLogo() ' You need to implement this method
        picLogo.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.TopLeft
        picLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        picLogo.Location = New Point(If(isArabic, (PageWidthF - 20) / 2, (PageWidthF - 20) / 2), 0)
        picLogo.Size = New Size(80, 80)
        reportHeader.Controls.Add(picLogo)

        ' Clinic Info
        Dim lblClinicName As XRLabel = New XRLabel()
        lblClinicName.Text = "DENTAL CLINIC"
        lblClinicName.Font = New Font("Arial", 22, FontStyle.Bold)
        lblClinicName.Location = New Point(If(isArabic, 0, (PageWidthF - 490) / 2), 80)
        lblClinicName.Size = New Size(470, 35)
        lblClinicName.TextAlignment = If(isArabic, TextAlignment.TopRight, TextAlignment.TopLeft)
        reportHeader.Controls.Add(lblClinicName)

        ' Doctor Info
        Dim lblDoctorInfo As XRLabel = New XRLabel()
        lblDoctorInfo.Text = "Dr. Ahmed Mohamed" & vbCrLf &
                       "Specialist in Dental Surgery" & vbCrLf &
                       "Phone: +966 55 123 4567" & vbCrLf &
                       "License: MED-12345"
        lblDoctorInfo.Font = New Font("Arial", 9)
        lblDoctorInfo.Location = New Point(If(isArabic, 0, 90), 35)
        lblDoctorInfo.Size = New Size(470, 60)
        lblDoctorInfo.TextAlignment = If(isArabic, TextAlignment.TopRight, TextAlignment.TopLeft)
        lblDoctorInfo.Multiline = True
        reportHeader.Controls.Add(lblDoctorInfo)

        ' Clinic Address
        Dim lblClinicAddress As XRLabel = New XRLabel()
        lblClinicAddress.Text = "King Fahd Road, Riyadh" & vbCrLf &
                          "Saudi Arabia" & vbCrLf &
                          "Tel: +966 11 123 4567" & vbCrLf &
                          "Email: info@dentalclinic.com"
        lblClinicAddress.Font = New Font("Arial", 9)
        lblClinicAddress.Location = New Point(If(isArabic, 470, 0), 0)
        lblClinicAddress.Size = New Size(200, 80)
        lblClinicAddress.TextAlignment = If(isArabic, TextAlignment.TopLeft, TextAlignment.TopRight)
        lblClinicAddress.Multiline = True
        reportHeader.Controls.Add(lblClinicAddress)

        Dim lineHeader As XRLine = New XRLine()
        lineHeader.Location = New Point(0, 85)
        lineHeader.Size = New Size(670, 3)
        lineHeader.LineStyle = Drawing2D.DashStyle.Solid
        lineHeader.LineWidth = 2
        lineHeader.ForeColor = Color.DarkBlue
        reportHeader.Controls.Add(lineHeader)

        ' Barcode for Invoice Number
        Dim barcode As XRBarCode = New XRBarCode()
        barcode.Symbology = New DevExpress.XtraPrinting.BarCode.Code128Generator()
        barcode.AutoModule = True
        barcode.Module = 2
        barcode.Text = "[InvoiceNumber]"
        barcode.Location = New Point(If(isArabic, 0, 470), 90)
        barcode.Size = New Size(200, 50)
        reportHeader.Controls.Add(barcode)

        reportHeader.Height = 150

        ' ============ Invoice Title ============
        Dim lblInvoiceTitle As XRLabel = New XRLabel()
        lblInvoiceTitle.Text = "INVOICE / فاتورة"
        lblInvoiceTitle.Font = New Font("Arial", 24, FontStyle.Bold)
        lblInvoiceTitle.ForeColor = Color.DarkBlue
        lblInvoiceTitle.Location = New Point(0, 10)
        lblInvoiceTitle.Size = New Size(670, 40)
        lblInvoiceTitle.TextAlignment = TextAlignment.TopCenter
        pageHeader.Controls.Add(lblInvoiceTitle)

        pageHeader.Height = 50

        ' ============ Patient Info Table ============
        Dim xrTablePatient As XRTable = New XRTable()
        xrTablePatient.Location = New Point(0, 0)
        xrTablePatient.Size = New Size(670, 120)
        xrTablePatient.Borders = DevExpress.XtraPrinting.BorderSide.All
        xrTablePatient.BorderWidth = 1

        ' Patient Info Header
        Dim patientHeaderRow As XRTableRow = New XRTableRow()
        patientHeaderRow.BackColor = Color.LightBlue

        Dim patientHeaderCell As XRTableCell = New XRTableCell()
        patientHeaderCell.Text = "PATIENT INFORMATION / معلومات المريض"
        patientHeaderCell.Font = New Font("Arial", 12, FontStyle.Bold)
        patientHeaderCell.TextAlignment = TextAlignment.MiddleCenter
        patientHeaderCell.ForeColor = Color.DarkBlue
        patientHeaderRow.Cells.Add(patientHeaderCell)
        xrTablePatient.Rows.Add(patientHeaderRow)

        ' Patient Details Rows
        Dim details As String() = {
        "Name / الاسم: [PatientName]",
        "Address / العنوان: [Address]",
        "Phone / الهاتف: [Phone] | Mobile / الجوال: [Phone]",
        "Age / العمر: [Age] | Gender / الجنس: [Sex]",
        "Medical Record No. / رقم الملف: [PatientNumber]"
    }
        '        "Email / البريد الإلكتروني: [PatientEmail]",

        For Each detailText In details
            Dim row As XRTableRow = New XRTableRow()
            row.Height = 20

            Dim cell As XRTableCell = New XRTableCell()
            cell.Text = detailText
            cell.Font = New Font("Arial", 10)
            cell.TextAlignment = If(isArabic, TextAlignment.MiddleRight, TextAlignment.MiddleLeft)
            cell.Padding = New DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0)
            row.Cells.Add(cell)
            xrTablePatient.Rows.Add(row)
        Next

        detail.Controls.Add(xrTablePatient)
        detail.Height = 130

        ' ============ Invoice Info Table ============
        Dim xrTableInvoice As XRTable = New XRTable()
        xrTableInvoice.Location = New Point(0, 140)
        xrTableInvoice.Size = New Size(670, 100)
        xrTableInvoice.Borders = DevExpress.XtraPrinting.BorderSide.All
        xrTableInvoice.BorderWidth = 1

        ' Invoice Info Header
        Dim invoiceHeaderRow As XRTableRow = New XRTableRow()
        invoiceHeaderRow.BackColor = Color.LightBlue

        Dim invoiceHeaderCell As XRTableCell = New XRTableCell()
        invoiceHeaderCell.Text = "INVOICE DETAILS / تفاصيل الفاتورة"
        invoiceHeaderCell.Font = New Font("Arial", 12, FontStyle.Bold)
        invoiceHeaderCell.TextAlignment = TextAlignment.MiddleCenter
        invoiceHeaderCell.ForeColor = Color.DarkBlue
        invoiceHeaderRow.Cells.Add(invoiceHeaderCell)
        xrTableInvoice.Rows.Add(invoiceHeaderRow)

        ' Invoice Details in 2 columns
        Dim invoiceDetails As String()() = {
        New String() {"Invoice No. / رقم الفاتورة:", "[InvoiceNumber]"},
        New String() {"Invoice Date / تاريخ الفاتورة:", "[InvoiceDate]"},
        New String() {"Due Date / تاريخ الاستحقاق:", "[DueDate]"},
        New String() {"Status / الحالة:", "[InvoiceStatus]"},
        New String() {"Doctor / الطبيب:", "Dr. Ahmed Mohamed"},
        New String() {"Consultation Date / تاريخ الكشف:", "[InvoiceDate]"}
    }

        For i As Integer = 0 To invoiceDetails.Length - 1 Step 2
            Dim row As XRTableRow = New XRTableRow()
            row.Height = 25

            For j As Integer = 0 To 1
                If i + j < invoiceDetails.Length Then
                    Dim detailArray = invoiceDetails(i + j)

                    Dim labelCell As XRTableCell = New XRTableCell()
                    labelCell.Text = detailArray(0)
                    labelCell.Font = New Font("Arial", 10, FontStyle.Bold)
                    labelCell.TextAlignment = If(isArabic, TextAlignment.MiddleLeft, TextAlignment.MiddleRight)
                    labelCell.Width = 200

                    Dim valueCell As XRTableCell = New XRTableCell()
                    valueCell.Text = detailArray(1)
                    valueCell.Font = New Font("Arial", 10)
                    valueCell.TextAlignment = If(isArabic, TextAlignment.MiddleRight, TextAlignment.MiddleLeft)
                    valueCell.Width = 135

                    row.Cells.AddRange(New XRTableCell() {labelCell, valueCell})
                End If
            Next
            xrTableInvoice.Rows.Add(row)
        Next

        detail.Controls.Add(xrTableInvoice)
        detail.Height = 250

        ' ============ Treatment Details Report ============
        CreateTreatmentDetailsReport(detailReportItems)

        ' ============ Payment History Report ============
        CreatePaymentHistoryReport(detailReportPayments)

        ' ============ Report Footer (Totals) ============
        CreateReportFooter(reportFooter)

        ' ============ Page Footer ============
        CreatePageFooter(pageFooter)
    End Sub

    Private Sub CreateTreatmentDetailsReport(detailReport As DetailReportBand)
        ' Header for items table
        Dim detailReportHeader As ReportHeaderBand = New ReportHeaderBand()
        detailReportHeader.Height = 50

        Dim lblItemsTitle As XRLabel = New XRLabel()
        lblItemsTitle.Text = "TREATMENT DETAILS / تفاصيل العلاج"
        lblItemsTitle.Font = New Font("Arial", 14, FontStyle.Bold)
        lblItemsTitle.ForeColor = Color.DarkBlue
        lblItemsTitle.Location = New Point(0, 10)
        lblItemsTitle.Size = New Size(670, 25)
        lblItemsTitle.TextAlignment = TextAlignment.TopCenter
        detailReportHeader.Controls.Add(lblItemsTitle)

        ' Column headers for items
        Dim xrTableHeader As XRTable = New XRTable()
        xrTableHeader.Location = New Point(0, 40)
        xrTableHeader.Size = New Size(670, 30)
        xrTableHeader.Borders = DevExpress.XtraPrinting.BorderSide.All

        Dim xrTableRowHeader As XRTableRow = New XRTableRow()
        xrTableRowHeader.BackColor = Color.LightBlue

        Dim headers As String()() = {
        New String() {"#", "م"},
        New String() {"Treatment / العلاج", "Treatment"},
        New String() {"Date / التاريخ", "Date"},
        New String() {"Amount / المبلغ", "Amount"},
        New String() {"Discount / الخصم", "Discount"},
        New String() {"Total / الإجمالي", "Total"}
    }

        For i As Integer = 0 To headers.Length - 1
            Dim cell As XRTableCell = New XRTableCell()
            cell.Text = If(isArabic, headers(i)(0), headers(i)(1))
            cell.Font = New Font("Arial", 10, FontStyle.Bold)
            cell.TextAlignment = TextAlignment.MiddleCenter
            cell.ForeColor = Color.DarkBlue
            Select Case i
                Case 0 : cell.Width = 40
                Case 1 : cell.Width = 280
                Case 2 : cell.Width = 80
                Case 3 : cell.Width = 90
                Case 4 : cell.Width = 90
                Case 5 : cell.Width = 90
            End Select
            xrTableRowHeader.Cells.Add(cell)
        Next

        xrTableHeader.Rows.Add(xrTableRowHeader)
        detailReportHeader.Controls.Add(xrTableHeader)

        ' Detail band for items
        Dim detailBandItems As DetailBand = New DetailBand()
        detailBandItems.Height = 25

        Dim xrTableItems As XRTable = New XRTable()
        xrTableItems.Location = New Point(0, 0)
        xrTableItems.Size = New Size(670, 25)
        xrTableItems.Borders = DevExpress.XtraPrinting.BorderSide.Bottom

        Dim xrTableRowItem As XRTableRow = New XRTableRow()

        ' Row number
        Dim cellNo As XRTableCell = New XRTableCell()
        cellNo.Text = "[RowNumber]"
        cellNo.TextAlignment = TextAlignment.MiddleCenter
        cellNo.Width = 40

        ' Description
        Dim cellDesc As XRTableCell = New XRTableCell()
        cellDesc.Text = "[ItemDescription]"
        cellDesc.Width = 280
        cellDesc.TextAlignment = If(isArabic, TextAlignment.MiddleRight, TextAlignment.MiddleLeft)

        ' Date
        Dim cellDate As XRTableCell = New XRTableCell()
        cellDate.Text = "[TreatmentDate]"
        cellDate.TextAlignment = TextAlignment.MiddleCenter
        cellDate.Width = 80

        ' Amount
        Dim cellAmount As XRTableCell = New XRTableCell()
        cellAmount.Text = "[UnitPrice]"
        cellAmount.TextAlignment = TextAlignment.MiddleRight
        cellAmount.Width = 90

        ' Discount
        Dim cellDiscount As XRTableCell = New XRTableCell()
        cellDiscount.Text = "[Discount]"
        cellDiscount.TextAlignment = TextAlignment.MiddleRight
        cellDiscount.Width = 90

        ' Total
        Dim cellTotal As XRTableCell = New XRTableCell()
        cellTotal.Text = "[LineTotal]"
        cellTotal.TextAlignment = TextAlignment.MiddleRight
        cellTotal.Width = 90

        xrTableRowItem.Cells.AddRange(New XRTableCell() {cellNo, cellDesc, cellDate, cellAmount, cellDiscount, cellTotal})
        xrTableItems.Rows.Add(xrTableRowItem)
        detailBandItems.Controls.Add(xrTableItems)

        ' Footer for treatment summary
        Dim detailReportFooter As ReportFooterBand = New ReportFooterBand()
        detailReportFooter.Height = 60

        ' Summary table
        Dim xrTableSummary As XRTable = New XRTable()
        xrTableSummary.Location = New Point(If(isArabic, 0, 240), 10)
        xrTableSummary.Size = New Size(430, 50)
        xrTableSummary.Borders = DevExpress.XtraPrinting.BorderSide.All

        ' Summary row
        Dim summaryRow As XRTableRow = New XRTableRow()

        Dim cellSummaryLabel As XRTableCell = New XRTableCell()
        cellSummaryLabel.Text = "Treatment Summary / ملخص العلاج"
        cellSummaryLabel.Font = New Font("Arial", 10, FontStyle.Bold)
        cellSummaryLabel.TextAlignment = TextAlignment.MiddleCenter
        cellSummaryLabel.BackColor = Color.LightGray

        Dim cellTreatmentsCount As XRTableCell = New XRTableCell()
        cellTreatmentsCount.Text = "Count / العدد: [Count(TreatmentDate)]"
        cellTreatmentsCount.Font = New Font("Arial", 9)
        cellTreatmentsCount.TextAlignment = TextAlignment.MiddleCenter

        Dim cellAmountSum As XRTableCell = New XRTableCell()
        cellAmountSum.Text = "Amount Total / إجمالي المبلغ: [Sum(UnitPriceNumeric)]"
        cellAmountSum.Font = New Font("Arial", 9)
        cellAmountSum.TextAlignment = TextAlignment.MiddleCenter

        Dim cellDiscountSum As XRTableCell = New XRTableCell()
        cellDiscountSum.Text = "Discount Total / إجمالي الخصم: [Sum(DiscountNumeric)]"
        cellDiscountSum.Font = New Font("Arial", 9)
        cellDiscountSum.TextAlignment = TextAlignment.MiddleCenter

        Dim cellTotalSum As XRTableCell = New XRTableCell()
        cellTotalSum.Text = "Net Total / الإجمالي الصافي: [Sum(LineTotalNumeric)]"
        cellTotalSum.Font = New Font("Arial", 9)
        cellTotalSum.TextAlignment = TextAlignment.MiddleCenter

        summaryRow.Cells.AddRange(New XRTableCell() {cellSummaryLabel, cellTreatmentsCount, cellAmountSum, cellDiscountSum, cellTotalSum})
        xrTableSummary.Rows.Add(summaryRow)
        detailReportFooter.Controls.Add(xrTableSummary)

        ' Configure detail report
        detailReport.Bands.Add(detailReportHeader)
        detailReport.Bands.Add(detailBandItems)
        detailReport.Bands.Add(detailReportFooter)
        detailReport.DataMember = "InvoiceItems"
    End Sub

    Private Sub CreatePaymentHistoryReport(detailReport As DetailReportBand)
        ' Header for payments
        Dim paymentsHeader As ReportHeaderBand = New ReportHeaderBand()
        paymentsHeader.Height = 50

        Dim lblPaymentsTitle As XRLabel = New XRLabel()
        lblPaymentsTitle.Text = "PAYMENT HISTORY / سجل المدفوعات"
        lblPaymentsTitle.Font = New Font("Arial", 14, FontStyle.Bold)
        lblPaymentsTitle.ForeColor = Color.DarkBlue
        lblPaymentsTitle.Location = New Point(0, 10)
        lblPaymentsTitle.Size = New Size(670, 25)
        lblPaymentsTitle.TextAlignment = TextAlignment.TopCenter
        paymentsHeader.Controls.Add(lblPaymentsTitle)

        ' Payment column headers
        Dim xrTablePaymentsHeader As XRTable = New XRTable()
        xrTablePaymentsHeader.Location = New Point(0, 40)
        xrTablePaymentsHeader.Size = New Size(670, 30)
        xrTablePaymentsHeader.Borders = DevExpress.XtraPrinting.BorderSide.All

        Dim xrTableRowPaymentsHeader As XRTableRow = New XRTableRow()
        xrTableRowPaymentsHeader.BackColor = Color.LightBlue

        Dim paymentHeaders As String()() = {
        New String() {"Date / التاريخ", "Date"},
        New String() {"Payment Type / طريقة الدفع", "Payment Type"},
        New String() {"Amount / المبلغ", "Amount"},
        New String() {"Check No. / رقم الشيك", "Check No"},
        New String() {"Status / الحالة", "Status"},
        New String() {"Notes / ملاحظات", "Notes"}
    }

        For i As Integer = 0 To paymentHeaders.Length - 1
            Dim cell As XRTableCell = New XRTableCell()
            cell.Text = If(isArabic, paymentHeaders(i)(0), paymentHeaders(i)(1))
            cell.Font = New Font("Arial", 10, FontStyle.Bold)
            cell.TextAlignment = TextAlignment.MiddleCenter
            cell.ForeColor = Color.DarkBlue
            Select Case i
                Case 0 : cell.Width = 80
                Case 1 : cell.Width = 100
                Case 2 : cell.Width = 90
                Case 3 : cell.Width = 80
                Case 4 : cell.Width = 80
                Case 5 : cell.Width = 140
            End Select
            xrTableRowPaymentsHeader.Cells.Add(cell)
        Next

        xrTablePaymentsHeader.Rows.Add(xrTableRowPaymentsHeader)
        paymentsHeader.Controls.Add(xrTablePaymentsHeader)

        ' Detail band for payments
        Dim detailBandPayments As DetailBand = New DetailBand()
        detailBandPayments.Height = 25

        Dim xrTablePayments As XRTable = New XRTable()
        xrTablePayments.Location = New Point(0, 0)
        xrTablePayments.Size = New Size(670, 25)
        xrTablePayments.Borders = DevExpress.XtraPrinting.BorderSide.Bottom

        Dim xrTableRowPayment As XRTableRow = New XRTableRow()

        Dim cellPayDate As XRTableCell = New XRTableCell()
        cellPayDate.Text = "[PayDate]"
        cellPayDate.TextAlignment = TextAlignment.MiddleCenter
        cellPayDate.Width = 80

        Dim cellPayType As XRTableCell = New XRTableCell()
        cellPayType.Text = "[PayType]"
        cellPayType.Width = 100
        cellPayType.TextAlignment = If(isArabic, TextAlignment.MiddleRight, TextAlignment.MiddleLeft)

        Dim cellPayAmount As XRTableCell = New XRTableCell()
        cellPayAmount.Text = "[PayValue]"
        cellPayAmount.TextAlignment = TextAlignment.MiddleRight
        cellPayAmount.Width = 90

        Dim cellChqNo As XRTableCell = New XRTableCell()
        cellChqNo.Text = "[ChqNumber]"
        cellChqNo.TextAlignment = TextAlignment.MiddleCenter
        cellChqNo.Width = 80

        Dim cellStatus As XRTableCell = New XRTableCell()
        cellStatus.Text = "[PaymentStatus]"
        cellStatus.TextAlignment = TextAlignment.MiddleCenter
        cellStatus.Width = 80

        Dim cellPayNotes As XRTableCell = New XRTableCell()
        cellPayNotes.Text = "[Notes]"
        cellPayNotes.Width = 140
        cellPayNotes.TextAlignment = If(isArabic, TextAlignment.MiddleRight, TextAlignment.MiddleLeft)

        xrTableRowPayment.Cells.AddRange(New XRTableCell() {cellPayDate, cellPayType, cellPayAmount, cellChqNo, cellStatus, cellPayNotes})
        xrTablePayments.Rows.Add(xrTableRowPayment)
        detailBandPayments.Controls.Add(xrTablePayments)

        ' Footer for payment summary
        Dim paymentsFooter As ReportFooterBand = New ReportFooterBand()
        paymentsFooter.Height = 50

        ' Payment summary table
        Dim xrTablePaymentSummary As XRTable = New XRTable()
        xrTablePaymentSummary.Location = New Point(If(isArabic, 0, 240), 10)
        xrTablePaymentSummary.Size = New Size(430, 40)
        xrTablePaymentSummary.Borders = DevExpress.XtraPrinting.BorderSide.All

        ' Payment summary row
        Dim paymentSummaryRow As XRTableRow = New XRTableRow()

        Dim cellPaymentSummaryLabel As XRTableCell = New XRTableCell()
        cellPaymentSummaryLabel.Text = "Payment Summary / ملخص المدفوعات"
        cellPaymentSummaryLabel.Font = New Font("Arial", 10, FontStyle.Bold)
        cellPaymentSummaryLabel.TextAlignment = TextAlignment.MiddleCenter
        cellPaymentSummaryLabel.BackColor = Color.LightGray

        Dim cellPaymentsCount As XRTableCell = New XRTableCell()
        cellPaymentsCount.Text = "Transactions / المعاملات: [Count(PayDate)]"
        cellPaymentsCount.Font = New Font("Arial", 9)
        cellPaymentsCount.TextAlignment = TextAlignment.MiddleCenter

        Dim cellPaymentsTotal As XRTableCell = New XRTableCell()
        cellPaymentsTotal.Text = "Total Paid / إجمالي المدفوع: [Sum(PayValueNumeric)]"
        cellPaymentsTotal.Font = New Font("Arial", 9)
        cellPaymentsTotal.TextAlignment = TextAlignment.MiddleCenter

        paymentSummaryRow.Cells.AddRange(New XRTableCell() {cellPaymentSummaryLabel, cellPaymentsCount, cellPaymentsTotal})
        xrTablePaymentSummary.Rows.Add(paymentSummaryRow)
        paymentsFooter.Controls.Add(xrTablePaymentSummary)

        ' Configure payments detail report
        detailReport.Bands.Add(paymentsHeader)
        detailReport.Bands.Add(detailBandPayments)
        detailReport.Bands.Add(paymentsFooter)
        detailReport.DataMember = "Payments"
    End Sub

    Private Sub CreateReportFooter(reportFooter As ReportFooterBand)
        reportFooter.Height = 200

        ' Final totals section
        Dim lblFinalTotals As XRLabel = New XRLabel()
        lblFinalTotals.Text = "FINAL TOTALS / الإجماليات النهائية"
        lblFinalTotals.Font = New Font("Arial", 16, FontStyle.Bold)
        lblFinalTotals.ForeColor = Color.DarkBlue
        lblFinalTotals.Location = New Point(0, 10)
        lblFinalTotals.Size = New Size(670, 30)
        lblFinalTotals.TextAlignment = TextAlignment.TopCenter
        reportFooter.Controls.Add(lblFinalTotals)

        ' Create totals table
        Dim xrTableTotals As XRTable = New XRTable()
        xrTableTotals.Location = New Point(If(isArabic, 0, 350), 50)
        xrTableTotals.Size = New Size(320, 140)
        xrTableTotals.Borders = DevExpress.XtraPrinting.BorderSide.All
        xrTableTotals.BorderWidth = 2

        ' Subtotal
        AddTotalRow(xrTableTotals, "Subtotal / الإجمالي الفرعي:", "[SubTotal]", 0, True)

        ' Discount
        AddTotalRow(xrTableTotals, "Discount / الخصم:", "[DiscountAmount]", 1, False)

        ' Tax
        AddTotalRow(xrTableTotals, "Tax (15%) / الضريبة (15%):", "[TaxAmount]", 2, False)

        ' Grand Total line
        Dim totalRow As XRTableRow = New XRTableRow()
        totalRow.Height = 35

        Dim cellTotalLabel As XRTableCell = New XRTableCell()
        cellTotalLabel.Text = "GRAND TOTAL / الإجمالي الكلي:"
        cellTotalLabel.Font = New Font("Arial", 14, FontStyle.Bold)
        cellTotalLabel.TextAlignment = TextAlignment.MiddleRight
        cellTotalLabel.BackColor = Color.LightBlue
        cellTotalLabel.ForeColor = Color.DarkBlue

        Dim cellTotalValue As XRTableCell = New XRTableCell()
        cellTotalValue.Text = "[TotalAmount]"
        cellTotalValue.Font = New Font("Arial", 14, FontStyle.Bold)
        cellTotalValue.TextAlignment = TextAlignment.MiddleRight
        cellTotalValue.BackColor = Color.LightBlue
        cellTotalValue.ForeColor = Color.DarkBlue

        totalRow.Cells.AddRange(New XRTableCell() {cellTotalLabel, cellTotalValue})
        xrTableTotals.Rows.Add(totalRow)

        ' Amount Paid
        AddTotalRow(xrTableTotals, "Amount Paid / المبلغ المدفوع:", "[AmountPaid]", 4, False)

        ' Balance Due
        Dim balanceRow As XRTableRow = New XRTableRow()
        balanceRow.Height = 35

        Dim cellBalanceLabel As XRTableCell = New XRTableCell()
        cellBalanceLabel.Text = "BALANCE DUE / الرصيد المستحق:"
        cellBalanceLabel.Font = New Font("Arial", 13, FontStyle.Bold)
        cellBalanceLabel.ForeColor = Color.Red
        cellBalanceLabel.TextAlignment = TextAlignment.MiddleRight

        Dim cellBalanceValue As XRTableCell = New XRTableCell()
        cellBalanceValue.Text = "[BalanceDue]"
        cellBalanceValue.Font = New Font("Arial", 13, FontStyle.Bold)
        cellBalanceValue.ForeColor = Color.Red
        cellBalanceValue.TextAlignment = TextAlignment.MiddleRight

        balanceRow.Cells.AddRange(New XRTableCell() {cellBalanceLabel, cellBalanceValue})
        xrTableTotals.Rows.Add(balanceRow)

        reportFooter.Controls.Add(xrTableTotals)

        ' Terms and Conditions in Arabic and English
        Dim lblTerms As XRLabel = New XRLabel()
        lblTerms.Text = "Terms & Conditions / الشروط والأحكام:" & vbCrLf &
                   "1. Payment is due within 30 days." & vbCrLf &
                   "    الدفع مستحق خلال 30 يوم." & vbCrLf &
                   "2. Please quote invoice number when making payment." & vbCrLf &
                   "    يرجى ذكر رقم الفاتورة عند الدفع." & vbCrLf &
                   "3. For inquiries, please contact our clinic." & vbCrLf &
                   "    للاستفسارات، يرجى الاتصال بعيادتنا."
        lblTerms.Font = New Font("Arial", 8)
        lblTerms.Location = New Point(0, 50)
        lblTerms.Size = New Size(340, 90)
        lblTerms.Multiline = True
        lblTerms.TextAlignment = If(isArabic, TextAlignment.MiddleRight, TextAlignment.MiddleLeft)
        reportFooter.Controls.Add(lblTerms)

        ' Signature Area
        Dim lblSignature As XRLabel = New XRLabel()
        lblSignature.Text = "___________________________" & vbCrLf &
                      "Authorized Signature / التوقيع المعتمد" & vbCrLf &
                      "Dr. Ahmed Mohamed"
        lblSignature.Font = New Font("Arial", 10)
        lblSignature.Location = New Point(If(isArabic, 0, 400), 150)
        lblSignature.Size = New Size(200, 60)
        lblSignature.TextAlignment = TextAlignment.MiddleCenter
        lblSignature.Multiline = True
        reportFooter.Controls.Add(lblSignature)
    End Sub

    Private Sub CreatePageFooter(pageFooter As PageFooterBand)
        pageFooter.Height = 40

        ' Footer line
        Dim linePageFooter As XRLine = New XRLine()
        linePageFooter.Location = New Point(0, 0)
        linePageFooter.Size = New Size(670, 2)
        linePageFooter.LineWidth = 1
        linePageFooter.ForeColor = Color.DarkBlue
        pageFooter.Controls.Add(linePageFooter)

        ' Thank you message in both languages
        Dim lblThankYou As XRLabel = New XRLabel()
        lblThankYou.Text = "Thank you for choosing our clinic! / شكراً لاختياركم عيادتنا!"
        lblThankYou.Font = New Font("Arial", 9, FontStyle.Italic)
        lblThankYou.ForeColor = Color.DarkGreen
        lblThankYou.Location = New Point(0, 5)
        lblThankYou.Size = New Size(670, 20)
        lblThankYou.TextAlignment = TextAlignment.TopCenter
        pageFooter.Controls.Add(lblThankYou)

        ' Page number
        Dim lblPageNumber As XRLabel = New XRLabel()
        lblPageNumber.Text = "Page [Page#] of [TotalPages#] / صفحة [Page#] من [TotalPages#]"
        lblPageNumber.Font = New Font("Arial", 8)
        lblPageNumber.Location = New Point(0, 25)
        lblPageNumber.Size = New Size(670, 15)
        lblPageNumber.TextAlignment = TextAlignment.TopCenter
        pageFooter.Controls.Add(lblPageNumber)
    End Sub

    Private Sub AddTotalRow(ByRef table As XRTable, label As String, binding As String, rowIndex As Integer, isBold As Boolean)
        Dim row As XRTableRow = New XRTableRow()
        row.Height = 25

        Dim cellLabel As XRTableCell = New XRTableCell()
        cellLabel.Text = label
        cellLabel.Font = New Font("Arial", If(isBold, 11, 10), If(isBold, FontStyle.Bold, FontStyle.Regular))
        cellLabel.TextAlignment = TextAlignment.MiddleRight

        Dim cellValue As XRTableCell = New XRTableCell()
        cellValue.Text = binding
        cellValue.Font = New Font("Arial", If(isBold, 11, 10), If(isBold, FontStyle.Bold, FontStyle.Regular))
        cellValue.TextAlignment = TextAlignment.MiddleRight

        row.Cells.AddRange(New XRTableCell() {cellLabel, cellValue})
        table.Rows.Add(row)
    End Sub

    Private Function LoadClinicLogo() As Image
        ' Load clinic logo from file or resource
        Try
            ' Option 1: From file
            If System.IO.File.Exists("clinic_logo.png") Then
                Return Image.FromFile("clinic_logo.png")
            End If

            ' Option 2: From resources
            ' Return My.Resources.ClinicLogo

            ' Option 3: Create a simple placeholder
            Dim bmp As New Bitmap(80, 80)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.White)
                g.DrawRectangle(Pens.Blue, 0, 0, 79, 79)
                g.DrawString("LOGO", New Font("Arial", 10, FontStyle.Bold), Brushes.Blue, New RectangleF(0, 0, 80, 80))
            End Using
            Return bmp

        Catch ex As Exception
            ' Return empty bitmap if logo not found
            Return New Bitmap(80, 80)
        End Try
    End Function

    Private Sub LoadData()
        Try
            Using conn As New SqlConnection(connectionString)
                ' Create DataSet for report
                Dim ds As New DataSet("InvoiceData")

                ' 1. Load Invoice Header with patient demographics
                '                        ISNULL(p.Email, 'N/A') AS PatientEmail,
                '  DATEDIFF(YEAR, p.BirthY, GETDATE()) AS PatientAge,
                Dim invoiceQuery As String = "
                    SELECT 
                        i.*,
                        p.PatientName,
                        p.Address AS PatientAddress,
                        p.Phone AS PatientPhone,
                        ISNULL(p.Phone, 'N/A') AS PatientMobile,
                        ISNULL(p.Age, 0) AS PatientAge,
                        ISNULL(p.Sex, 'N/A') AS PatientGender,
                        ISNULL(p.PatientNumber, 'N/A') AS MedicalRecordNo,
                        FORMAT(i.InvoiceDate, 'dd/MM/yyyy') AS InvoiceDate,
                        FORMAT(i.DueDate, 'dd/MM/yyyy') AS DueDate,
                        FORMAT(p.CreateDate, 'dd/MM/yyyy') AS ConsultationDate,
                        CASE i.InvoiceStatus
                            WHEN 0 THEN 'Draft / مسودة'
                            WHEN 1 THEN 'Issued / صادرة'
                            WHEN 2 THEN 'Paid / مدفوعة'
                            WHEN 3 THEN 'Partially Paid / مدفوعة جزئياً'
                            WHEN 4 THEN 'Cancelled / ملغاة'
                        END AS InvoiceStatus
                    FROM Invoices i
                    INNER JOIN Patient p ON i.PatientID = p.PatientID
                    WHERE i.InvoiceID = @InvoiceID"

                Dim invoiceAdapter As New SqlDataAdapter(invoiceQuery, conn)
                invoiceAdapter.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID)
                invoiceAdapter.Fill(ds, "InvoiceHeader")

                ' 2. Load Invoice Items with numeric values for calculations
                Dim itemsQuery As String = "
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY ii.SortOrder) AS RowNumber,
                        ii.InvoiceID,
                        ii.ItemDescription,
                        FORMAT(pt.TrtDate, 'dd/MM/yyyy') AS TreatmentDate,
                        FORMAT(ii.UnitPrice, 'C') AS UnitPrice,
                        ii.UnitPrice AS UnitPriceNumeric,
                        FORMAT(ii.Discount, 'C') AS Discount,
                        ii.Discount AS DiscountNumeric,
                        FORMAT(ii.LineTotal, 'C') AS LineTotal,
                        ii.LineTotal AS LineTotalNumeric
                    FROM Invoice_Items ii
                    INNER JOIN Patient_Trts pt ON ii.TrtID = pt.TrtID
                    WHERE ii.InvoiceID = @InvoiceID
                    ORDER BY ii.SortOrder"

                Dim itemsAdapter As New SqlDataAdapter(itemsQuery, conn)
                itemsAdapter.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID)
                itemsAdapter.Fill(ds, "InvoiceItems")

                ' 3. Load Payments with numeric values for calculations
                Dim paymentsQuery As String = "
                    SELECT 
                        ip.InvoiceID,
                        FORMAT(pp.PayDate, 'dd/MM/yyyy') AS PayDate,
                        pp.PayType,
                        FORMAT(pp.PayValue, 'C') AS PayValue,
                        pp.PayValue AS PayValueNumeric,
                        ISNULL(pp.ChqNumber, 'N/A') AS ChqNumber,
                        CASE 
                            WHEN pp.IsCashed = 1 THEN 'Cashed / مسحوبة'
                            WHEN pp.PayType = 'Check' THEN 'Pending / قيد الانتظار'
                            ELSE 'Completed / مكتملة'
                        END AS PaymentStatus,
                        ISNULL(pp.Notes, '') AS Notes
                    FROM Invoice_Payments ip
                    INNER JOIN Patient_Pays pp ON ip.PayID = pp.PayID
                    WHERE ip.InvoiceID = @InvoiceID
                    ORDER BY pp.PayDate DESC"

                Dim paymentsAdapter As New SqlDataAdapter(paymentsQuery, conn)
                paymentsAdapter.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID)
                paymentsAdapter.Fill(ds, "Payments")

                ' Set up relationships
                If ds.Tables("InvoiceHeader").Columns.Contains("InvoiceID") AndAlso
               ds.Tables("InvoiceItems").Columns.Contains("InvoiceID") Then
                    ds.Relations.Add("InvoiceItemsRelation",
                                ds.Tables("InvoiceHeader").Columns("InvoiceID"),
                                ds.Tables("InvoiceItems").Columns("InvoiceID"))
                End If

                If ds.Tables("InvoiceHeader").Columns.Contains("InvoiceID") AndAlso
               ds.Tables("Payments").Columns.Contains("InvoiceID") Then
                    ds.Relations.Add("PaymentsRelation",
                                ds.Tables("InvoiceHeader").Columns("InvoiceID"),
                                ds.Tables("Payments").Columns("InvoiceID"))
                End If

                ' Bind data to report
                Me.DataSource = ds
                Me.DataMember = "InvoiceHeader"

            End Using
        Catch ex As Exception
            Throw New Exception("Error loading invoice data: " & ex.Message)
        End Try
    End Sub
End Class