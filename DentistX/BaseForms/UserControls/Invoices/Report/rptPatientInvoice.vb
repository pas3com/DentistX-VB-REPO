Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting


Public Class rptPatientInvoice

    Private invoiceID As Integer
    Private connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Sub New(ByVal invID As Integer)
        InitializeComponent()
        InitializeComponent1()
        invoiceID = invID
        LoadData()
    End Sub

    Private Sub InitializeComponent1()
        ' Report initialization   253, 102, 78
        Me.Landscape = False
        Me.Margins = New System.Drawing.Printing.Margins(10, 10, 10, 10)
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4

        CreateReportLayout()
    End Sub

    Private Sub CreateReportLayout()
            ' ============ Report Bands ============
            Dim reportHeader As ReportHeaderBand = New ReportHeaderBand()
            Dim pageHeader As PageHeaderBand = New PageHeaderBand()
            Dim detail As DetailBand = New DetailBand()
            Dim detailReport As DetailReportBand = New DetailReportBand()
            Dim reportFooter As ReportFooterBand = New ReportFooterBand()
            Dim pageFooter As PageFooterBand = New PageFooterBand()

            ' Add bands to report
            Me.Bands.AddRange(New Band() {
            reportHeader,
            pageHeader,
            detail,
            detailReport,
            reportFooter,
            pageFooter
        })

            ' ============ Company Header ============
            Dim lblClinicName As XRLabel = New XRLabel()
            lblClinicName.Text = "DENTAL CLINIC"
            lblClinicName.Font = New Font("Arial", 20, FontStyle.Bold)
            lblClinicName.Location = New Point(0, 0)
            lblClinicName.Size = New Size(650, 40)
        lblClinicName.TextAlignment = TextAlignment.TopCenter
        reportHeader.Controls.Add(lblClinicName)

            Dim lblClinicAddress As XRLabel = New XRLabel()
            lblClinicAddress.Text = "123 Medical Street, City, Country" & vbCrLf & "Phone: (123) 456-7890 | Email: info@clinic.com"
            lblClinicAddress.Font = New Font("Arial", 10)
            lblClinicAddress.Location = New Point(0, 45)
            lblClinicAddress.Size = New Size(650, 30)
            lblClinicAddress.TextAlignment = TextAlignment.TopCenter
            reportHeader.Controls.Add(lblClinicAddress)

            Dim lineHeader As XRLine = New XRLine()
            lineHeader.Location = New Point(0, 80)
            lineHeader.Size = New Size(650, 5)
            lineHeader.LineStyle = Drawing2D.DashStyle.Solid
            lineHeader.LineWidth = 2
            reportHeader.Controls.Add(lineHeader)

            reportHeader.Height = 100

            ' ============ Invoice Title ============
            Dim lblInvoiceTitle As XRLabel = New XRLabel()
            lblInvoiceTitle.Text = "INVOICE"
            lblInvoiceTitle.Font = New Font("Arial", 18, FontStyle.Bold)
            lblInvoiceTitle.Location = New Point(0, 10)
            lblInvoiceTitle.Size = New Size(650, 30)
            lblInvoiceTitle.TextAlignment = TextAlignment.TopCenter
            pageHeader.Controls.Add(lblInvoiceTitle)

            pageHeader.Height = 40

            ' ============ Patient & Invoice Info (Detail Band) ============
            ' Create a table for patient and invoice info
            Dim xrTableInfo As XRTable = New XRTable()
            xrTableInfo.Location = New Point(0, 0)
            xrTableInfo.Size = New Size(650, 100)

            Dim xrTableRow1 As XRTableRow = New XRTableRow()
            Dim xrTableRow2 As XRTableRow = New XRTableRow()

            ' Patient Info Cell
            Dim xrCellPatient As XRTableCell = New XRTableCell()
        xrCellPatient.Text = "Patient Information:" & vbCrLf &
                            "Name: [PatientName]" & vbCrLf &
                            "Address: [Address]" & vbCrLf &
                            "Phone: [Phone]" & vbCrLf &
                            "Age: [Age]" & vbCrLf &
                            "Sex: [Sex]"
        xrCellPatient.Font = New Font("Arial", 10)
            xrCellPatient.WordWrap = True

            ' Invoice Info Cell
            Dim xrCellInvoice As XRTableCell = New XRTableCell()
            xrCellInvoice.Text = "Invoice Details:" & vbCrLf &
                            "Invoice #: [InvoiceNumber]" & vbCrLf &
                            "Date: [InvoiceDate]" & vbCrLf &
                            "Due Date: [DueDate]" & vbCrLf &
                            "Status: [InvoiceStatus]"
            xrCellInvoice.Font = New Font("Arial", 10)
            xrCellInvoice.TextAlignment = TextAlignment.TopRight
            xrCellInvoice.WordWrap = True

            xrTableRow1.Cells.AddRange(New XRTableCell() {xrCellPatient, xrCellInvoice})
            xrTableInfo.Rows.Add(xrTableRow1)

            detail.Controls.Add(xrTableInfo)
            detail.Height = 110

            ' ============ Invoice Items Detail Report ============
            ' Header for items table
            Dim detailReportHeader As ReportHeaderBand = New ReportHeaderBand()
            detailReportHeader.Height = 40

            Dim lblItemsTitle As XRLabel = New XRLabel()
            lblItemsTitle.Text = "Treatment Details"
            lblItemsTitle.Font = New Font("Arial", 12, FontStyle.Bold)
            lblItemsTitle.Location = New Point(0, 10)
            lblItemsTitle.Size = New Size(650, 20)
            detailReportHeader.Controls.Add(lblItemsTitle)

            ' Column headers for items
            Dim xrTableHeader As XRTable = New XRTable()
            xrTableHeader.Location = New Point(0, 35)
            xrTableHeader.Size = New Size(650, 25)

            Dim xrTableRowHeader As XRTableRow = New XRTableRow()
            xrTableRowHeader.BackColor = Color.LightGray

            Dim headers As String() = {"#", "Treatment Description", "Date", "Amount", "Discount", "Total"}
            Dim widths As Integer() = {40, 300, 80, 80, 80, 70}

            For i As Integer = 0 To headers.Length - 1
                Dim cell As XRTableCell = New XRTableCell()
                cell.Text = headers(i)
                cell.Font = New Font("Arial", 10, FontStyle.Bold)
                cell.TextAlignment = TextAlignment.MiddleCenter
                cell.Width = widths(i)
                xrTableRowHeader.Cells.Add(cell)
            Next

            xrTableHeader.Rows.Add(xrTableRowHeader)
            detailReportHeader.Controls.Add(xrTableHeader)

            ' Detail band for items
            Dim detailBandItems As DetailBand = New DetailBand()
            detailBandItems.Height = 25

            Dim xrTableItems As XRTable = New XRTable()
            xrTableItems.Location = New Point(0, 0)
            xrTableItems.Size = New Size(650, 25)

            Dim xrTableRowItem As XRTableRow = New XRTableRow()

            ' Add cells with data bindings
            Dim cellNo As XRTableCell = New XRTableCell()
            cellNo.Text = "[RowNumber]"
            cellNo.TextAlignment = TextAlignment.MiddleCenter
            cellNo.Width = 40

            Dim cellDesc As XRTableCell = New XRTableCell()
            cellDesc.Text = "[ItemDescription]"
            cellDesc.Width = 300

            Dim cellDate As XRTableCell = New XRTableCell()
            cellDate.Text = "[TreatmentDate]"
            cellDate.TextAlignment = TextAlignment.MiddleCenter
            cellDate.Width = 80

            Dim cellAmount As XRTableCell = New XRTableCell()
            cellAmount.Text = "[UnitPrice]"
            cellAmount.TextAlignment = TextAlignment.MiddleRight
            cellAmount.Width = 80

            Dim cellDiscount As XRTableCell = New XRTableCell()
            cellDiscount.Text = "[Discount]"
            cellDiscount.TextAlignment = TextAlignment.MiddleRight
            cellDiscount.Width = 80

            Dim cellTotal As XRTableCell = New XRTableCell()
            cellTotal.Text = "[LineTotal]"
            cellTotal.TextAlignment = TextAlignment.MiddleRight
            cellTotal.Width = 70

            xrTableRowItem.Cells.AddRange(New XRTableCell() {cellNo, cellDesc, cellDate, cellAmount, cellDiscount, cellTotal})
            xrTableItems.Rows.Add(xrTableRowItem)
            detailBandItems.Controls.Add(xrTableItems)

            ' Configure detail report
            detailReport.Bands.Add(detailReportHeader)
            detailReport.Bands.Add(detailBandItems)
            detailReport.DataMember = "InvoiceItems"

            ' ============ Payments Section ============
            Dim detailReportPayments As DetailReportBand = New DetailReportBand()

            ' Header for payments
            Dim paymentsHeader As ReportHeaderBand = New ReportHeaderBand()
            paymentsHeader.Height = 40

            Dim lblPaymentsTitle As XRLabel = New XRLabel()
            lblPaymentsTitle.Text = "Payment History"
            lblPaymentsTitle.Font = New Font("Arial", 12, FontStyle.Bold)
            lblPaymentsTitle.Location = New Point(0, 10)
            lblPaymentsTitle.Size = New Size(650, 20)
            paymentsHeader.Controls.Add(lblPaymentsTitle)

            ' Payment column headers
            Dim xrTablePaymentsHeader As XRTable = New XRTable()
            xrTablePaymentsHeader.Location = New Point(0, 35)
            xrTablePaymentsHeader.Size = New Size(650, 25)

            Dim xrTableRowPaymentsHeader As XRTableRow = New XRTableRow()
            xrTableRowPaymentsHeader.BackColor = Color.LightGray

            Dim paymentHeaders As String() = {"Date", "Payment Type", "Amount", "Check No", "Status", "Notes"}
            Dim paymentWidths As Integer() = {80, 100, 80, 80, 80, 130}

            For i As Integer = 0 To paymentHeaders.Length - 1
                Dim cell As XRTableCell = New XRTableCell()
                cell.Text = paymentHeaders(i)
                cell.Font = New Font("Arial", 10, FontStyle.Bold)
                cell.TextAlignment = TextAlignment.MiddleCenter
                cell.Width = paymentWidths(i)
                xrTableRowPaymentsHeader.Cells.Add(cell)
            Next

            xrTablePaymentsHeader.Rows.Add(xrTableRowPaymentsHeader)
            paymentsHeader.Controls.Add(xrTablePaymentsHeader)

            ' Detail band for payments
            Dim detailBandPayments As DetailBand = New DetailBand()
            detailBandPayments.Height = 25

            Dim xrTablePayments As XRTable = New XRTable()
            xrTablePayments.Location = New Point(0, 0)
            xrTablePayments.Size = New Size(650, 25)

            Dim xrTableRowPayment As XRTableRow = New XRTableRow()

            Dim cellPayDate As XRTableCell = New XRTableCell()
            cellPayDate.Text = "[PayDate]"
            cellPayDate.TextAlignment = TextAlignment.MiddleCenter
            cellPayDate.Width = 80

            Dim cellPayType As XRTableCell = New XRTableCell()
            cellPayType.Text = "[PayType]"
            cellPayType.Width = 100

            Dim cellPayAmount As XRTableCell = New XRTableCell()
            cellPayAmount.Text = "[PayValue]"
            cellPayAmount.TextAlignment = TextAlignment.MiddleRight
            cellPayAmount.Width = 80

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
            cellPayNotes.Width = 130

            xrTableRowPayment.Cells.AddRange(New XRTableCell() {cellPayDate, cellPayType, cellPayAmount, cellChqNo, cellStatus, cellPayNotes})
            xrTablePayments.Rows.Add(xrTableRowPayment)
            detailBandPayments.Controls.Add(xrTablePayments)

            ' Configure payments detail report
            detailReportPayments.Bands.Add(paymentsHeader)
            detailReportPayments.Bands.Add(detailBandPayments)
            detailReportPayments.DataMember = "Payments"

            ' Add payments detail report to main report
            Me.Bands.Add(detailReportPayments)

            ' ============ Report Footer (Totals) ============
            reportFooter.Height = 150

            ' Separator line
            Dim lineFooter As XRLine = New XRLine()
            lineFooter.Location = New Point(0, 10)
            lineFooter.Size = New Size(650, 2)
            lineFooter.LineWidth = 1
            reportFooter.Controls.Add(lineFooter)

            ' Create totals table
            Dim xrTableTotals As XRTable = New XRTable()
            xrTableTotals.Location = New Point(400, 20)
            xrTableTotals.Size = New Size(250, 120)

            ' Subtotal
            AddTotalRow(xrTableTotals, "Subtotal:", "[SubTotal]", 0)

            ' Discount
            AddTotalRow(xrTableTotals, "Discount:", "[DiscountAmount]", 1)

            ' Tax
            AddTotalRow(xrTableTotals, "Tax (15%):", "[TaxAmount]", 2)

            ' Line for total
            Dim totalRow As XRTableRow = New XRTableRow()
            totalRow.Height = 30

            Dim cellTotalLabel As XRTableCell = New XRTableCell()
            cellTotalLabel.Text = "TOTAL:"
            cellTotalLabel.Font = New Font("Arial", 12, FontStyle.Bold)
            cellTotalLabel.TextAlignment = TextAlignment.MiddleRight
            cellTotalLabel.BackColor = Color.LightGray

            Dim cellTotalValue As XRTableCell = New XRTableCell()
            cellTotalValue.Text = "[TotalAmount]"
            cellTotalValue.Font = New Font("Arial", 12, FontStyle.Bold)
            cellTotalValue.TextAlignment = TextAlignment.MiddleRight
            cellTotalValue.BackColor = Color.LightGray

            totalRow.Cells.AddRange(New XRTableCell() {cellTotalLabel, cellTotalValue})
            xrTableTotals.Rows.Add(totalRow)

            ' Amount Paid
            AddTotalRow(xrTableTotals, "Amount Paid:", "[AmountPaid]", 4)

            ' Balance Due
            Dim balanceRow As XRTableRow = New XRTableRow()
            balanceRow.Height = 25

            Dim cellBalanceLabel As XRTableCell = New XRTableCell()
            cellBalanceLabel.Text = "BALANCE DUE:"
            cellBalanceLabel.Font = New Font("Arial", 11, FontStyle.Bold)
            cellBalanceLabel.ForeColor = Color.Red
            cellBalanceLabel.TextAlignment = TextAlignment.MiddleRight

            Dim cellBalanceValue As XRTableCell = New XRTableCell()
            cellBalanceValue.Text = "[BalanceDue]"
            cellBalanceValue.Font = New Font("Arial", 11, FontStyle.Bold)
            cellBalanceValue.ForeColor = Color.Red
            cellBalanceValue.TextAlignment = TextAlignment.MiddleRight

            balanceRow.Cells.AddRange(New XRTableCell() {cellBalanceLabel, cellBalanceValue})
            xrTableTotals.Rows.Add(balanceRow)

            reportFooter.Controls.Add(xrTableTotals)

            ' Terms and Conditions
            Dim lblTerms As XRLabel = New XRLabel()
            lblTerms.Text = "Terms & Conditions:" & vbCrLf &
                       "1. Payment is due within 30 days." & vbCrLf &
                       "2. Late payments may incur additional charges." & vbCrLf &
                       "3. Please quote invoice number when making payment."
            lblTerms.Font = New Font("Arial", 8)
            lblTerms.Location = New Point(0, 20)
            lblTerms.Size = New Size(350, 60)
            lblTerms.Multiline = True
            reportFooter.Controls.Add(lblTerms)

            ' ============ Page Footer ============
            pageFooter.Height = 50

            ' Footer line
            Dim linePageFooter As XRLine = New XRLine()
            linePageFooter.Location = New Point(0, 0)
            linePageFooter.Size = New Size(650, 2)
            pageFooter.Controls.Add(linePageFooter)

            ' Thank you message
            Dim lblThankYou As XRLabel = New XRLabel()
            lblThankYou.Text = "Thank you for your business!"
            lblThankYou.Font = New Font("Arial", 10, FontStyle.Italic)
            lblThankYou.Location = New Point(0, 10)
            lblThankYou.Size = New Size(650, 20)
            lblThankYou.TextAlignment = TextAlignment.TopCenter
            pageFooter.Controls.Add(lblThankYou)

            ' Page number
            Dim lblPageNumber As XRLabel = New XRLabel()
            lblPageNumber.Text = "Page [Page#] of [TotalPages#]"
            lblPageNumber.Font = New Font("Arial", 8)
            lblPageNumber.Location = New Point(0, 30)
            lblPageNumber.Size = New Size(650, 15)
            lblPageNumber.TextAlignment = TextAlignment.TopCenter
            pageFooter.Controls.Add(lblPageNumber)
        End Sub

        Private Sub AddTotalRow(ByRef table As XRTable, label As String, binding As String, rowIndex As Integer)
            Dim row As XRTableRow = New XRTableRow()
            row.Height = 20

            Dim cellLabel As XRTableCell = New XRTableCell()
            cellLabel.Text = label
            cellLabel.Font = New Font("Arial", 10)
            cellLabel.TextAlignment = TextAlignment.MiddleRight

            Dim cellValue As XRTableCell = New XRTableCell()
            cellValue.Text = binding
            cellValue.Font = New Font("Arial", 10)
            cellValue.TextAlignment = TextAlignment.MiddleRight

            row.Cells.AddRange(New XRTableCell() {cellLabel, cellValue})
            table.Rows.Add(row)
        End Sub

        Private Sub LoadData()
            Try
                Using conn As New SqlConnection(connectionString)
                    ' Create DataSet for report
                    Dim ds As New DataSet("InvoiceData")

                    ' 1. Load Invoice Header
                    Dim invoiceQuery As String = "
                    SELECT 
                        i.*,
                        p.PatientName AS PatientName,
                        p.Address AS Address,
                        p.Phone AS Phone,
                        p.Age AS Age,
                        p.Sex AS Sex,
                        i.InvoiceID,
                        FORMAT(i.InvoiceDate, 'dd/MM/yyyy') AS InvoiceDate,
                        FORMAT(i.DueDate, 'dd/MM/yyyy') AS DueDate,
                        CASE i.InvoiceStatus
                            WHEN 0 THEN 'Draft'
                            WHEN 1 THEN 'Issued'
                            WHEN 2 THEN 'Paid'
                            WHEN 3 THEN 'Partially Paid'
                            WHEN 4 THEN 'Cancelled'
                        END AS InvoiceStatus
                    FROM Invoices i
                    INNER JOIN Patient p ON i.PatientID = p.PatientID
                    WHERE i.InvoiceID = @InvoiceID"

                    Dim invoiceAdapter As New SqlDataAdapter(invoiceQuery, conn)
                    invoiceAdapter.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID)
                    invoiceAdapter.Fill(ds, "InvoiceHeader")

                    ' 2. Load Invoice Items
                    Dim itemsQuery As String = "
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY ii.SortOrder) AS RowNumber,
                        ii.InvoiceID,
                        ii.ItemDescription,
                        FORMAT(pt.TrtDate, 'dd/MM/yyyy') AS TreatmentDate,
                        FORMAT(ii.UnitPrice, 'C') AS UnitPrice,
                        FORMAT(ii.Discount, 'C') AS Discount,
                        FORMAT(ii.LineTotal, 'C') AS LineTotal
                    FROM Invoice_Items ii
                    INNER JOIN Patient_Trts pt ON ii.TrtID = pt.TrtID
                    WHERE ii.InvoiceID = @InvoiceID
                    ORDER BY ii.SortOrder"

                    Dim itemsAdapter As New SqlDataAdapter(itemsQuery, conn)
                    itemsAdapter.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID)
                    itemsAdapter.Fill(ds, "InvoiceItems")

                    ' 3. Load Payments
                    Dim paymentsQuery As String = "
                    SELECT 
                        FORMAT(pp.PayDate, 'dd/MM/yyyy') AS PayDate,
                        ip.InvoiceID,
                        pp.PayType,
                        FORMAT(pp.PayValue, 'C') AS PayValue,
                        ISNULL(pp.ChqNumber, 'N/A') AS ChqNumber,
                        CASE 
                            WHEN pp.IsCashed = 1 THEN 'Cashed'
                            WHEN pp.PayType = 'Check' THEN 'Pending'
                            ELSE 'Completed'
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
                    ds.Relations.Add("InvoiceItemsRelation",
                                ds.Tables("InvoiceHeader").Columns("InvoiceID"),
                                ds.Tables("InvoiceItems").Columns("InvoiceID"))

                    ds.Relations.Add("PaymentsRelation",
                                ds.Tables("InvoiceHeader").Columns("InvoiceID"),
                                ds.Tables("Payments").Columns("InvoiceID"))

                    ' Bind data to report
                    Me.DataSource = ds
                    Me.DataMember = "InvoiceHeader"

                End Using
            Catch ex As Exception
                Throw New Exception("Error loading invoice data: " & ex.Message)
            End Try
        End Sub
    End Class