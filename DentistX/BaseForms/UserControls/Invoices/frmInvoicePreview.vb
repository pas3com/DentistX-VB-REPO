Imports System.Data.SqlClient

Public Class frmInvoicePreview
    Private patientID As Integer
    Private treatments As List(Of TreatmentItem)
    Private invoiceNumber As String
    Private invoiceDate As Date
    Private dueDate As Date
    Private notes As String
    Private withtax As Boolean
    Public Sub New(ByVal patID As Integer, ByVal trtList As List(Of TreatmentItem),
                   ByVal invNumber As String, ByVal invDate As Date,
                   ByVal dDate As Date, ByVal invNotes As String, ByVal incldTax As Boolean)
        InitializeComponent()

        patientID = patID
        treatments = trtList
        invoiceNumber = invNumber
        invoiceDate = invDate
        dueDate = dDate
        notes = invNotes
        withtax = incldTax
        LoadPreviewDev()
    End Sub


    Private Sub LoadPreviewDev()
        Try
            ' Load patient info
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                Dim query As String = "
                    SELECT PatientID, PatientName, Address, Phone, Sex 
                    FROM Patient 
                    WHERE PatientID = @PatientID"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PatientID", patientID)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            lblPatientName.Text = reader("PatientName")
                            lblAddress.Text = reader("Address").ToString()
                            lblPhone.Text = reader("Phone").ToString()
                            lblEmail.Text = reader("Sex").ToString()
                        End If
                    End Using
                End Using
            End Using

            ' Set invoice details
            lblInvoiceNo.Text = invoiceNumber
            lblDate.Text = invoiceDate.ToString("dd/MM/yyyy")
            lblDueDate.Text = dueDate.ToString("dd/MM/yyyy")
            txtNotes.Text = notes

            ' Clear existing rows
            GridView1.GridControl.DataSource = Nothing
            GridView1.Columns.Clear()

            ' Create columns
            GridView1.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                .FieldName = "Detail",
                .Caption = "Treatment Detail",
                .VisibleIndex = 0,
                .Width = 300
            })

            GridView1.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                .FieldName = "Date",
                .Caption = "Date",
                .VisibleIndex = 1,
                .Width = 80
            })

            GridView1.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                .FieldName = "Amount",
                .Caption = "Amount",
                .VisibleIndex = 2,
                .Width = 100
            })

            GridView1.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                .FieldName = "Discount",
                .Caption = "Discount",
                .VisibleIndex = 3,
                .Width = 100
            })

            GridView1.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
                .FieldName = "Total",
                .Caption = "Total",
                .VisibleIndex = 4,
                .Width = 100
            })

            ' Create a list to bind
            Dim previewItems As New List(Of PreviewItem)()
            Dim subTotal As Decimal = 0
            Dim totalDiscount As Decimal = 0

            For Each treatment In treatments.Where(Function(t) t.IsSelected)
                previewItems.Add(New PreviewItem With {
                    .Detail = treatment.Detail,
                    .Date = treatment.TrtDate.ToString("dd/MM/yyyy"),
                    .Amount = FormatCurrency(treatment.TrtValue),
                    .Discount = FormatCurrency(treatment.Discount),
                    .Total = FormatCurrency(treatment.TrtValue - treatment.Discount)
                })

                subTotal += treatment.TrtValue
                totalDiscount += treatment.Discount
            Next

            ' Bind to GridControl
            dgvPreview.DataSource = previewItems

            ' Calculate totals
            Dim taxRate As Decimal = 0.16D
            Dim taxAmount As Decimal = (subTotal - totalDiscount) * taxRate
            Dim totalAmount As Decimal = 0
            If withtax Then
                totalAmount = (subTotal - totalDiscount) + taxAmount
            Else
                totalAmount = (subTotal - totalDiscount)
            End If

            lblSubTotal.Text = FormatCurrency(subTotal)
            lblDiscount.Text = FormatCurrency(totalDiscount)
            lblTax.Text = FormatCurrency(taxAmount)
            lblTotal.Text = FormatCurrency(totalAmount)
            lblAmountWords.Text = "Amount in words: " & ConvertNumberToWords(totalAmount)


        Catch ex As Exception
            MessageBox.Show("Error loading preview: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub LoadPreview()
        Try
            ' Load patient info
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                Dim query As String = "
                    SELECT PatientName, Address, Phone, Sex 
                    FROM Patient 
                    WHERE PatientID = @PatientID"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PatientID", patientID)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            lblPatientName.Text = reader("PatientName")
                            lblAddress.Text = reader("Address").ToString()
                            lblPhone.Text = reader("Phone").ToString()
                            lblEmail.Text = reader("Sex").ToString()
                        End If
                    End Using
                End Using
            End Using

            ' Set invoice details
            lblInvoiceNo.Text = invoiceNumber
            lblDate.Text = invoiceDate.ToString("dd/MM/yyyy")
            lblDueDate.Text = dueDate.ToString("dd/MM/yyyy")
            txtNotes.Text = notes

            ' Load treatments
            Dim subTotal As Decimal = 0
            Dim totalDiscount As Decimal = 0

            'dgvPreview.Rows.Clear()
            'For Each treatment In treatments.Where(Function(t) t.IsSelected)
            '    Dim rowIndex As Integer = dgvPreview.Rows.Add()
            '    dgvPreview.Rows(rowIndex).Cells("colPreviewDetail").Value = treatment.Detail
            '    dgvPreview.Rows(rowIndex).Cells("colPreviewDate").Value = treatment.TrtDate.ToString("dd/MM/yyyy")
            '    dgvPreview.Rows(rowIndex).Cells("colPreviewAmount").Value = FormatCurrency(treatment.TrtValue)
            '    dgvPreview.Rows(rowIndex).Cells("colPreviewDiscount").Value = FormatCurrency(treatment.Discount)
            '    dgvPreview.Rows(rowIndex).Cells("colPreviewTotal").Value = FormatCurrency(treatment.TrtValue - treatment.Discount)

            '    subTotal += treatment.TrtValue
            '    totalDiscount += treatment.Discount
            'Next

            ' Calculate totals
            Dim taxRate As Decimal = 0.15D
            Dim taxAmount As Decimal = (subTotal - totalDiscount) * taxRate
            Dim totalAmount As Decimal = (subTotal - totalDiscount) + taxAmount

            lblSubTotal.Text = FormatCurrency(subTotal)
            lblDiscount.Text = FormatCurrency(totalDiscount)
            lblTax.Text = FormatCurrency(taxAmount)
            lblTotal.Text = FormatCurrency(totalAmount)
            lblAmountWords.Text = "Amount in words: " & ConvertNumberToWords(totalAmount)

        Catch ex As Exception
            MessageBox.Show("Error loading preview: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ConvertNumberToWords(ByVal amount As Decimal) As String
        ' Simple number to words conversion (you can enhance this)
        Return amount.ToString("N2") & " only"
    End Function

    ' After binding the data, add a footer
    Private Sub AddGridFooter()
        GridView1.OptionsView.ShowFooter = True

        ' Set footer text for each column
        GridView1.Columns("Amount").Summary.Clear()
        GridView1.Columns("Amount").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Amount", "Subtotal: {0:c2}")

        GridView1.Columns("Discount").Summary.Clear()
        GridView1.Columns("Discount").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Discount", "Discount: {0:c2}")

        GridView1.Columns("Total").Summary.Clear()
        GridView1.Columns("Total").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Total", "Net Total: {0:c2}")
    End Sub


    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim printDoc As New Printing.PrintDocument()
            AddHandler printDoc.PrintPage, AddressOf PrintInvoicePage

            Dim printDialog As New PrintDialog()
            printDialog.Document = printDoc

            If printDialog.ShowDialog() = DialogResult.OK Then
                printDoc.Print()
            End If
        Catch ex As Exception
            MessageBox.Show("Error printing: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintInvoicePage(ByVal sender As Object, ByVal e As Printing.PrintPageEventArgs)
        ' Add your printing logic here
        ' You can use e.Graphics to draw the invoice
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmInvoicePreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class


