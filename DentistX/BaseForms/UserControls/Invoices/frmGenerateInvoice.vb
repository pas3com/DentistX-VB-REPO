Imports System.Data.SqlClient
Imports System.Globalization
Imports DevExpress.XtraGrid.Columns

Public Class frmGenerateInvoice
    Private connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private patientID As Integer = CurrentPatient.PatientID ' 0
    Private selectedTreatments As New List(Of TreatmentItem)



    ' Main Form Load
    Private Sub frmGenerateInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If DesignMode Then Return
        Dim form As New FormGenerateInvoice(patientID)
        form.ShowDialog()
        Me.Close()
        Return
        LoadPatients()
        dtpInvoiceDate.Value = Date.Today
        dtpDueDate.Value = Date.Today.AddDays(30)
        lblInvoiceNumber.Text = "Will be generated on save"
    End Sub

    ' Load Patients ComboBox
    Private Sub LoadPatients()
        Try

        Catch ex As Exception
            MessageBox.Show("Error loading patients: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub cbPatients_PatientValueChanged(sender As Object, e As PatientCombo.PatientIndexChangedEvent) Handles cbPatients.PatientValueChanged
        patientID = e.PatientID
        lblPatientID.Text = "Patient ID: " & patientID.ToString()
        LoadPatientTreatments()
    End Sub
    ' Patient Selection Changed


    ' Load Treatments for Selected Patient
    Private Sub LoadPatientTreatments()
        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Dim query As String = "
                    SELECT 
                        pt.TrtID,
                        pt.Detail,
                        pt.TrtDate,
                        pt.TrtValue,
                        ISNULL(pt.Discount, 0) AS Discount,
                        CASE 
                            WHEN EXISTS(SELECT 1 FROM Invoice_Items ii WHERE ii.TrtID = pt.TrtID) THEN 1 
                            ELSE 0 
                        END AS IsInvoiced
                    FROM Patient_Trts pt
                    WHERE pt.PatientID = @PatientID
                    ORDER BY pt.TrtDate DESC"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PatientID", patientID)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        dgvTreatments.Rows.Clear()
                        selectedTreatments.Clear()

                        While reader.Read()
                            Dim isInvoiced As Boolean = reader("IsInvoiced")
                            Dim canSelect As Boolean = Not isInvoiced

                            Dim rowIndex As Integer = dgvTreatments.Rows.Add()
                            dgvTreatments.Rows(rowIndex).Cells("colSelect").Value = canSelect
                            dgvTreatments.Rows(rowIndex).Cells("colSelect").ReadOnly = Not canSelect
                            dgvTreatments.Rows(rowIndex).Cells("colTrtID").Value = reader("TrtID")
                            dgvTreatments.Rows(rowIndex).Cells("colDetail").Value = reader("Detail")
                            dgvTreatments.Rows(rowIndex).Cells("colDate").Value = CDate(reader("TrtDate")).ToString("yyyy-MM-dd")
                            dgvTreatments.Rows(rowIndex).Cells("colValue").Value = FormatCurrency(reader("TrtValue"))
                            dgvTreatments.Rows(rowIndex).Cells("colDiscount").Value = FormatCurrency(reader("Discount"))
                            dgvTreatments.Rows(rowIndex).Cells("colInvoiced").Value = If(isInvoiced, "Yes", "No")

                            ' Add to treatments list
                            Dim treatment As New TreatmentItem With {
                                .TrtID = reader("TrtID"),
                                .Detail = reader("Detail").ToString(),
                                .TrtDate = reader("TrtDate"),
                                .TrtValue = CDec(reader("TrtValue")),
                                .Discount = CDec(reader("Discount")),
                                .IsSelected = False
                            }
                            selectedTreatments.Add(treatment)
                        End While
                    End Using
                End Using
            End Using
            CalculateTotals()
        Catch ex As Exception
            MessageBox.Show("Error loading treatments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Treatment Selection Changed
    Private Sub dgvTreatments_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTreatments.CellContentClick
        If e.ColumnIndex = dgvTreatments.Columns("colSelect").Index AndAlso e.RowIndex >= 0 Then
            Dim isSelected As Boolean = CBool(dgvTreatments.Rows(e.RowIndex).Cells("colSelect").Value)
            dgvTreatments.Rows(e.RowIndex).Cells("colSelect").Value = Not isSelected

            ' Update treatments list
            Dim trtID As Integer = CInt(dgvTreatments.Rows(e.RowIndex).Cells("colTrtID").Value)
            Dim treatment = selectedTreatments.FirstOrDefault(Function(t) t.TrtID = trtID)
            If treatment.TrtID > 0 Then
                Dim index = selectedTreatments.FindIndex(Function(t) t.TrtID = trtID)
                selectedTreatments(index).IsSelected = Not isSelected
            End If

            CalculateTotals()
        End If
    End Sub

    ' Calculate Invoice Totals
    Private Sub CalculateTotals()
        Dim subTotal As Decimal = 0
        Dim totalDiscount As Decimal = 0
        Dim taxRate As Decimal = 0.15D ' 15% tax rate - adjust as needed
        Dim selectedCount As Integer = 0

        For Each treatment In selectedTreatments
            If treatment.IsSelected Then
                subTotal += treatment.TrtValue
                totalDiscount += treatment.Discount
                selectedCount += 1
            End If
        Next

        Dim taxAmount As Decimal = (subTotal - totalDiscount) * taxRate
        Dim totalAmount As Decimal = (subTotal - totalDiscount) + taxAmount

        ' Update display
        lblSubTotal.Text = FormatCurrency(subTotal)
        lblDiscount.Text = FormatCurrency(totalDiscount)
        lblTaxAmount.Text = FormatCurrency(taxAmount)
        lblTotalAmount.Text = FormatCurrency(totalAmount)
        lblSelectedCount.Text = selectedCount.ToString() & " treatment(s) selected"
    End Sub

    ' Generate Invoice Number
    Private Function GenerateInvoiceNumber() As String
        Dim datePart As String = Date.Today.ToString("yyyyMMdd")
        Dim sequence As Integer = 1

        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Dim query As String = "
                    SELECT COUNT(*) + 1 AS NextNumber 
                    FROM Invoices 
                    WHERE InvoiceNumber LIKE 'INV-" & datePart & "-%'"

                Using cmd As New SqlCommand(query, conn)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        sequence = CInt(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Table might not exist yet
            sequence = 1
        End Try

        Return $"INV-{datePart}-{sequence:000}"
    End Function

    ' Preview Invoice
    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        If patientID = 0 Then
            MessageBox.Show("Please select a patient first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If selectedTreatments.Where(Function(t) t.IsSelected).Count() = 0 Then
            MessageBox.Show("Please select at least one treatment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim invoiceNumber As String = GenerateInvoiceNumber()
        lblInvoiceNumber.Text = "Invoice #: " & invoiceNumber

        ' Show preview form
        Dim previewForm As New frmInvoicePreview(patientID, selectedTreatments, invoiceNumber,
                                                 dtpInvoiceDate.Value, dtpDueDate.Value,
                                                 txtNotes.Text, True)
        previewForm.ShowDialog()
    End Sub

    ' Save Invoice
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If patientID = 0 Then
            MessageBox.Show("Please select a patient first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedTreatmentsCount = selectedTreatments.Where(Function(t) t.IsSelected).Count()
        If selectedTreatmentsCount = 0 Then
            MessageBox.Show("Please select at least one treatment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Dim transaction As SqlTransaction = conn.BeginTransaction()

                Try
                    ' 1. Generate invoice number
                    Dim invoiceNumber As String = GenerateInvoiceNumber()

                    ' 2. Calculate totals
                    Dim subTotal As Decimal = 0
                    Dim totalDiscount As Decimal = 0
                    Dim taxRate As Decimal = 0.15D

                    For Each treatment In selectedTreatments
                        If treatment.IsSelected Then
                            subTotal += treatment.TrtValue
                            totalDiscount += treatment.Discount
                        End If
                    Next

                    Dim taxAmount As Decimal = (subTotal - totalDiscount) * taxRate
                    Dim totalAmount As Decimal = (subTotal - totalDiscount) + taxAmount

                    ' 3. Insert invoice header
                    Dim insertInvoiceQuery As String = "
                        INSERT INTO Invoices 
                        (InvoiceNumber, PatientID, InvoiceDate, DueDate, InvoiceStatus, 
                         SubTotal, TaxAmount, DiscountAmount, TotalAmount, AmountPaid, BalanceDue, Notes, CreatedDate)
                        VALUES 
                        (@InvoiceNumber, @PatientID, @InvoiceDate, @DueDate, 1, 
                         @SubTotal, @TaxAmount, @DiscountAmount, @TotalAmount, 0, @TotalAmount, @Notes, GETDATE())
                        SELECT SCOPE_IDENTITY()"

                    Dim invoiceID As Integer
                    Using cmd As New SqlCommand(insertInvoiceQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber)
                        cmd.Parameters.AddWithValue("@PatientID", patientID)
                        cmd.Parameters.AddWithValue("@InvoiceDate", dtpInvoiceDate.Value)
                        cmd.Parameters.AddWithValue("@DueDate", dtpDueDate.Value)
                        cmd.Parameters.AddWithValue("@SubTotal", subTotal)
                        cmd.Parameters.AddWithValue("@TaxAmount", taxAmount)
                        cmd.Parameters.AddWithValue("@DiscountAmount", totalDiscount)
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount)
                        cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(txtNotes.Text), DBNull.Value, txtNotes.Text))

                        invoiceID = CInt(cmd.ExecuteScalar())
                    End Using

                    ' 4. Insert invoice items
                    Dim insertItemQuery As String = "
                        INSERT INTO Invoice_Items 
                        (InvoiceID, TrtID, ItemDescription, Quantity, UnitPrice, Discount, TaxRate, LineTotal)
                        VALUES 
                        (@InvoiceID, @TrtID, @ItemDescription, 1, @UnitPrice, @Discount, @TaxRate, @LineTotal)"

                    For Each treatment In selectedTreatments
                        If treatment.IsSelected Then
                            Using cmd As New SqlCommand(insertItemQuery, conn, transaction)
                                cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                                cmd.Parameters.AddWithValue("@TrtID", treatment.TrtID)
                                cmd.Parameters.AddWithValue("@ItemDescription", treatment.Detail)
                                cmd.Parameters.AddWithValue("@UnitPrice", treatment.TrtValue)
                                cmd.Parameters.AddWithValue("@Discount", treatment.Discount)
                                cmd.Parameters.AddWithValue("@TaxRate", taxRate)
                                cmd.Parameters.AddWithValue("@LineTotal", treatment.TrtValue - treatment.Discount)
                                cmd.ExecuteNonQuery()
                            End Using
                        End If
                    Next

                    ' 5. Add to invoice history
                    Dim insertHistoryQuery As String = "
                        INSERT INTO Invoice_History 
                        (InvoiceID, OldStatus, NewStatus, ChangeDate, ChangeReason)
                        VALUES 
                        (@InvoiceID, NULL, 1, GETDATE(), 'Invoice Created')"

                    Using cmd As New SqlCommand(insertHistoryQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                        cmd.ExecuteNonQuery()
                    End Using

                    transaction.Commit()

                    MessageBox.Show($"Invoice #{invoiceNumber} created successfully!" & vbCrLf &
                                    $"Total Amount: {FormatCurrency(totalAmount)}",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Refresh the form
                    LoadPatientTreatments()
                    lblInvoiceNumber.Text = "Will be generated on save"
                    txtNotes.Clear()

                Catch ex As Exception
                    transaction.Rollback()
                    Throw
                End Try
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving invoice: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Cancel/Close
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    ' Load Existing Invoices
    Private Sub btnViewInvoices_Click(sender As Object, e As EventArgs) Handles btnViewInvoices.Click
        If patientID = 0 Then
            MessageBox.Show("Please select a patient first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim invoiceListForm As New frmInvoiceList(patientID)
        invoiceListForm.ShowDialog()
    End Sub


#Region "For Devx Grid"
    ' Load Treatments for Selected Patient - DevExpress GridControl version
    Private Sub LoadPatientTreatmentsDev()
        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Dim query As String = "
                SELECT 
                    pt.TrtID,
                    pt.Detail,
                    pt.TrtDate,
                    pt.TrtValue,
                    ISNULL(pt.Discount, 0) AS Discount,
                    CASE 
                        WHEN EXISTS(SELECT 1 FROM Invoice_Items ii WHERE ii.TrtID = pt.TrtID) THEN 1 
                        ELSE 0 
                    END AS IsInvoiced
                FROM Patient_Trts pt
                WHERE pt.PatientID = @PatientID
                ORDER BY pt.TrtDate DESC"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PatientID", patientID)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        selectedTreatments.Clear()

                        While reader.Read()
                            Dim isInvoiced As Boolean = reader("IsInvoiced")

                            Dim treatment As New TreatmentItem With {
                            .TrtID = reader("TrtID"),
                            .Detail = reader("Detail").ToString(),
                            .TrtDate = reader("TrtDate"),
                            .TrtValue = CDec(reader("TrtValue")),
                            .Discount = CDec(reader("Discount")),
                            .IsInvoiced = isInvoiced,
                            .IsSelected = False ' Default not selected
                        }
                            selectedTreatments.Add(treatment)
                        End While
                    End Using
                End Using
            End Using

            ' Bind to GridControl
            BindToGridControl()
            CalculateTotals()

        Catch ex As Exception
            MessageBox.Show("Error loading treatments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Bind data to DevExpress GridControl
    Private Sub BindToGridControl()
        ' Create a DataTable for binding
        Dim dt As New DataTable()
        dt.Columns.Add("Select", GetType(Boolean))
        dt.Columns.Add("TrtID", GetType(Integer))
        dt.Columns.Add("Detail", GetType(String))
        dt.Columns.Add("Date", GetType(Date))
        dt.Columns.Add("Value", GetType(Decimal))
        dt.Columns.Add("Discount", GetType(Decimal))
        dt.Columns.Add("Invoiced", GetType(String))

        For Each treatment In selectedTreatments
            Dim row As DataRow = dt.NewRow()
            row("Select") = treatment.IsSelected
            row("TrtID") = treatment.TrtID
            row("Detail") = treatment.Detail
            row("Date") = treatment.TrtDate
            row("Value") = treatment.TrtValue
            row("Discount") = treatment.Discount
            row("Invoiced") = If(treatment.IsInvoiced, "Yes", "No")
            dt.Rows.Add(row)
        Next

        '' Bind to GridControl
        'GridControl1.DataSource = dt
        'GridView1.BestFitColumns()

        '' Customize column appearance
        'Dim colSelect As GridColumn = GridView1.Columns("Select")
        'colSelect.Width = 50
        'colSelect.OptionsColumn.AllowEdit = True

        'Dim colInvoiced As GridColumn = GridView1.Columns("Invoiced")
        'For i As Integer = 0 To GridView1.DataRowCount - 1
        '    If GridView1.GetDataRow(i)("Invoiced").ToString() = "Yes" Then
        '        GridView1.SetRowCellValue(i, "Select", False)
        '        GridView1.SetRowCellDisplayText(i, colSelect, "Already Invoiced")
        '    End If
        'Next
    End Sub




#End Region

End Class