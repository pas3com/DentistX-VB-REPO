Imports System.Data.SqlClient
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI

Public Class frmInvoiceDetails
    Private invoiceID As Integer

    Public Sub New(ByVal invID As Integer)
        InitializeComponent()
        invoiceID = invID
        LoadInvoiceDetails()
    End Sub

    Private Sub LoadInvoiceDetails()
        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()

                ' Load invoice header
                Dim statusDraft = If(Eng, "Draft", "مسودة")
                Dim statusIssued = If(Eng, "Issued", "صادرة")
                Dim statusPaid = If(Eng, "Paid", "مدفوعة")
                Dim statusPartial = If(Eng, "Partially Paid", "مدفوعة جزئيا")
                Dim statusCancelled = If(Eng, "Cancelled", "ملغاة")
                Dim headerQuery As String = "
                    SELECT 
                        i.InvoiceNumber,
                        i.InvoiceDate,
                        i.DueDate,
                        i.SubTotal,
                        i.TaxAmount,
                        i.DiscountAmount,
                        i.TotalAmount,
                        i.AmountPaid,
                        i.BalanceDue,
                        i.Notes,
                        CASE i.InvoiceStatus
                            WHEN 0 THEN '" & statusDraft & "'
                            WHEN 1 THEN '" & statusIssued & "'
                            WHEN 2 THEN '" & statusPaid & "'
                            WHEN 3 THEN '" & statusPartial & "'
                            WHEN 4 THEN '" & statusCancelled & "'
                        END AS Status,
                        p.PatientName,
                        p.Address,
                        p.Phone,
                        p.Sex
                    FROM Invoices i
                    INNER JOIN Patient p ON i.PatientID = p.PatientID
                    WHERE i.InvoiceID = @InvoiceID"

                Using cmd As New SqlCommand(headerQuery, conn)
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' Populate header info
                            lblInvoiceNumber.Text = reader("InvoiceNumber").ToString()
                            lblInvoiceDate.Text = CDate(reader("InvoiceDate")).ToString("dd/MM/yyyy")
                            lblDueDate.Text = CDate(reader("DueDate")).ToString("dd/MM/yyyy")
                            lblStatus.Text = reader("Status").ToString()
                            lblPatientName.Text = reader("PatientName").ToString()
                            lblAddress.Text = reader("Address").ToString()
                            lblPhone.Text = reader("Phone").ToString()
                            lblEmail.Text = reader("Sex").ToString()
                            lblNotes.Text = reader("Notes").ToString()

                            ' Financial info
                            lblSubTotal.Text = FormatCurrency(reader("SubTotal"))
                            lblTax.Text = FormatCurrency(reader("TaxAmount"))
                            lblDiscount.Text = FormatCurrency(reader("DiscountAmount"))
                            lblTotal.Text = FormatCurrency(reader("TotalAmount"))
                            lblAmountPaid.Text = FormatCurrency(reader("AmountPaid"))
                            lblBalanceDue.Text = FormatCurrency(reader("BalanceDue"))

                            ' Color code status
                            Select Case reader("Status").ToString()
                                Case statusPaid
                                    lblStatus.ForeColor = Color.Green
                                Case statusPartial
                                    lblStatus.ForeColor = Color.Orange
                                Case statusIssued
                                    lblStatus.ForeColor = Color.Red
                                Case statusCancelled
                                    lblStatus.ForeColor = Color.Gray
                            End Select
                        End If
                    End Using
                End Using

                ' Load invoice items
                Dim itemsQuery As String = "
                    SELECT 
                        ii.ItemDescription,
                        ii.Quantity,
                        ii.UnitPrice,
                        ii.Discount,
                        ii.LineTotal,
                        pt.TrtDate
                    FROM Invoice_Items ii
                    INNER JOIN Patient_Trts pt ON ii.TrtID = pt.TrtID
                    WHERE ii.InvoiceID = @InvoiceID
                    ORDER BY ii.SortOrder"

                Using cmd As New SqlCommand(itemsQuery, conn)
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dtItems As New DataTable()
                        adapter.Fill(dtItems)

                        ' Bind to DevExpress GridControl
                        GridControl1.DataSource = dtItems
                        GridView1.BestFitColumns()
                        ApplyInvoiceItemsGridCaptions()

                        ' Format columns
                        GridView1.Columns("UnitPrice").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("UnitPrice").DisplayFormat.FormatString = "c2"

                        GridView1.Columns("Discount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Discount").DisplayFormat.FormatString = "c2"

                        GridView1.Columns("LineTotal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("LineTotal").DisplayFormat.FormatString = "c2"

                        GridView1.Columns("TrtDate").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        GridView1.Columns("TrtDate").DisplayFormat.FormatString = "dd/MM/yyyy"
                    End Using
                End Using

                ' Load payment history
                LoadPaymentHistory(conn)

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading invoice details: " & ex.Message, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadPaymentHistory(ByVal conn As SqlConnection)
        Dim yesText = If(Eng, "Yes", "نعم")
        Dim noText = If(Eng, "No", "لا")
        Dim paymentsQuery As String = "
            SELECT 
                pp.PayDate,
                pp.PayValue,
                pp.PayType,
                pp.Notes,
                ip.AllocatedAmount,
                pp.ChqNumber,
                pp.ChqDueDate,
                CASE WHEN pp.IsCashed = 1 THEN '" & yesText & "' ELSE '" & noText & "' END AS IsCashed
            FROM Invoice_Payments ip
            INNER JOIN Patient_Pays pp ON ip.PayID = pp.PayID
            WHERE ip.InvoiceID = @InvoiceID
            ORDER BY pp.PayDate DESC"

        Using cmd As New SqlCommand(paymentsQuery, conn)
            cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
            Using adapter As New SqlDataAdapter(cmd)
                Dim dtPayments As New DataTable()
                adapter.Fill(dtPayments)

                ' Bind to second GridControl (for payments)
                GridControl2.DataSource = dtPayments
                GridView2.BestFitColumns()
                ApplyInvoicePaymentsGridCaptions()

                ' Format columns
                GridView2.Columns("PayValue").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("PayValue").DisplayFormat.FormatString = "c2"

                GridView2.Columns("AllocatedAmount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("AllocatedAmount").DisplayFormat.FormatString = "c2"

                GridView2.Columns("PayDate").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView2.Columns("PayDate").DisplayFormat.FormatString = "dd/MM/yyyy"

                GridView2.Columns("ChqDueDate").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                GridView2.Columns("ChqDueDate").DisplayFormat.FormatString = "dd/MM/yyyy"
            End Using
        End Using
    End Sub

    Private Sub ApplyInvoiceItemsGridCaptions()
        If GridView1 Is Nothing OrElse GridView1.Columns Is Nothing Then Return
        If GridView1.Columns("ItemDescription") IsNot Nothing Then
            GridView1.Columns("ItemDescription").Caption = If(Eng, "Item Description", "وصف البند")
        End If
        If GridView1.Columns("Quantity") IsNot Nothing Then
            GridView1.Columns("Quantity").Caption = If(Eng, "Quantity", "الكمية")
        End If
        If GridView1.Columns("UnitPrice") IsNot Nothing Then
            GridView1.Columns("UnitPrice").Caption = If(Eng, "Unit Price", "سعر الوحدة")
        End If
        If GridView1.Columns("Discount") IsNot Nothing Then
            GridView1.Columns("Discount").Caption = If(Eng, "Discount", "الخصم")
        End If
        If GridView1.Columns("LineTotal") IsNot Nothing Then
            GridView1.Columns("LineTotal").Caption = If(Eng, "Line Total", "إجمالي البند")
        End If
        If GridView1.Columns("TrtDate") IsNot Nothing Then
            GridView1.Columns("TrtDate").Caption = If(Eng, "Treatment Date", "تاريخ العلاج")
        End If
    End Sub

    Private Sub ApplyInvoicePaymentsGridCaptions()
        If GridView2 Is Nothing OrElse GridView2.Columns Is Nothing Then Return
        If GridView2.Columns("PayDate") IsNot Nothing Then
            GridView2.Columns("PayDate").Caption = If(Eng, "Payment Date", "تاريخ الدفع")
        End If
        If GridView2.Columns("PayValue") IsNot Nothing Then
            GridView2.Columns("PayValue").Caption = If(Eng, "Payment Amount", "قيمة الدفع")
        End If
        If GridView2.Columns("PayType") IsNot Nothing Then
            GridView2.Columns("PayType").Caption = If(Eng, "Payment Type", "طريقة الدفع")
        End If
        If GridView2.Columns("Notes") IsNot Nothing Then
            GridView2.Columns("Notes").Caption = If(Eng, "Notes", "ملاحظات")
        End If
        If GridView2.Columns("AllocatedAmount") IsNot Nothing Then
            GridView2.Columns("AllocatedAmount").Caption = If(Eng, "Allocated Amount", "المبلغ المخصص")
        End If
        If GridView2.Columns("ChqNumber") IsNot Nothing Then
            GridView2.Columns("ChqNumber").Caption = If(Eng, "Cheque Number", "رقم الشيك")
        End If
        If GridView2.Columns("ChqDueDate") IsNot Nothing Then
            GridView2.Columns("ChqDueDate").Caption = If(Eng, "Cheque Due Date", "تاريخ استحقاق الشيك")
        End If
        If GridView2.Columns("IsCashed") IsNot Nothing Then
            GridView2.Columns("IsCashed").Caption = If(Eng, "Is Cashed", "مصروف")
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        ' Print invoice functionality
        ' In your invoice details form, add a Print/Preview button:
        Try
            'Dim invoiceID As Integer = invoiceID 'GetCurrentInvoiceID() ' Get current invoice ID
            Dim lang As String = If(Eng, "en", "ar")
            If chkWithPay.Checked Then
                ' Print with payments
                Dim report As New PatientInvPaysRep(invoiceID, PatientID)
                'report.Parameters("parPatientID").Value = PatientID
                'report.Parameters("parPatientID").Visible = False
                'report.Parameters("parInvoiceID").Value = invoiceID
                'report.Parameters("parInvoiceID").Visible = False
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(report)
                ' Option 2: Direct Print
                report.Print()
            Else
                ' Print without payments
                Dim report As New PatientInvoiceRep(invoiceID, PatientID) 'invoiceID, "ar", Nothing, CurrentPatient)
                'report.Parameters("parPatientID").Value = PatientID
                'report.Parameters("parPatientID").Visible = False
                'report.Parameters("parInvoiceID").Value = invoiceID
                'report.Parameters("parInvoiceID").Visible = False
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(report)
                ' Option 2: Direct Print
                report.Print()
            End If

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnViewInv_Click(sender As Object, e As EventArgs) Handles btnViewInv.Click
        Try
            ' Create and show report
            Dim lang As String = If(Eng, "en", "ar")
            Dim report As New XtraReport

            Dim printTool As DevExpress.XtraReports.UI.ReportPrintTool
            If chkWithPay.Checked Then
                ' Print with payments
                report = New PatientInvPaysRep(invoiceID, PatientID)
                'report.Parameters("parPatientID").Value = PatientID
                'report.Parameters("parPatientID").Visible = False
                'report.Parameters("parInvoiceID").Value = invoiceID
                'report.Parameters("parInvoiceID").Visible = False
                printTool = New DevExpress.XtraReports.UI.ReportPrintTool(report)
            Else
                ' Print without payments
                report = New PatientInvoiceRep(invoiceID, PatientID) 'invoiceID, "ar", Nothing, CurrentPatient)
                'report.Parameters("parPatientID").Value = PatientID
                'report.Parameters("parPatientID").Visible = False
                'report.Parameters("parInvoiceID").Value = invoiceID
                'report.Parameters("parInvoiceID").Visible = False
                printTool = New DevExpress.XtraReports.UI.ReportPrintTool(report)
            End If
            ' Get the Preview Form
            Dim previewForm As Form = printTool.PreviewForm
            ' Customize form settings
            previewForm.Size = New Size(1366, 768)
            previewForm.StartPosition = FormStartPosition.CenterScreen
            previewForm.WindowState = FormWindowState.Normal ' Or Normal / Minimized
            ' Show it
            printTool.ShowPreviewDialog()

        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnAddPayment_Click(sender As Object, e As EventArgs) Handles btnAddPayment.Click
        ' Open payment form
        Dim paymentForm As New frmAddPayment(invoiceID)
        If paymentForm.ShowDialog() = DialogResult.OK Then
            ' Refresh the details
            LoadInvoiceDetails()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExportPDF_Click(sender As Object, e As EventArgs) Handles btnExportPDF.Click
        ' Export to PDF functionality
        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
            saveDialog.FileName = $"Invoice_{lblInvoiceNumber.Text}.pdf"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Dim report As New rptInvoicePays()
                ' Option 1: Show in Preview Form
                'Dim previewForm As New frmReportViewer(report)
                'previewForm.ShowDialog()


                report.Parameters("parPatientID").Value = PatientID
                report.Parameters("parPatientID").Visible = False
                report.Parameters("parInvoiceID").Value = invoiceID
                report.Parameters("parInvoiceID").Visible = False

                report.ExportToPdf(saveDialog.FileName, Nothing)
                ' Using DevExpress Export to PDF
                'GridControl1.ExportToPdf(saveDialog.FileName)
                MessageBox.Show($"Invoice exported to: {saveDialog.FileName}",
                              "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error exporting to PDF: " & ex.Message, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkPDF_CheckedChanged(sender As Object, e As EventArgs) Handles chkWithPay.CheckedChanged
        If chkWithPay.Checked Then
            chkWithPay.Text = If(Eng, "Without Payments", "بدون المدفوعات")
        Else
            chkWithPay.Text = If(Eng, "With Payments", "مع المدفوعات")
        End If
    End Sub
End Class