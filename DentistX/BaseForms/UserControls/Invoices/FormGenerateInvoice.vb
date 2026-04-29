Imports System.Data.SqlClient
Imports System.Globalization
Imports Dapper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

Public Class FormGenerateInvoice
    Private connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private patientID As Integer = 0
    Private selectedTreatments As New List(Of TreatmentItem)
    Private selectedPayments As New List(Of PaymentItem)
    Private ReadOnly _invoiceService As InvoiceService
    Private DefaultTaxRate As Decimal = 0.16D

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _invoiceService = New InvoiceService(connectionString)
    End Sub
    Public Sub New(passedPatientID As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        patientID = passedPatientID
        _invoiceService = New InvoiceService(connectionString)
    End Sub
    ' Main Form Load
    Private Sub FormGenerateInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPatients(patientID)
        dtpInvoiceDate.Value = Date.Today
        dtpDueDate.Value = Date.Today.AddDays(30)
        lblInvoiceNumber.Text = "Will be generated on save"
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(TreatmentsGrid)
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(PaymentsGrid)
    End Sub


    ' Load specific patient or all patients
    Private Sub LoadPatients(Optional specificPatientID As Integer = 0)
        Try
            Dim items = _invoiceService.GetPatients(specificPatientID)
            cbPatients.Items.Clear()
            For Each item In items
                cbPatients.Items.Add(item)
            Next
            If specificPatientID > 0 AndAlso cbPatients.Items.Count > 0 Then
                cbPatients.SelectedIndex = 0
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading patients: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    ' Patient Selection Changed
    Private Sub cbPatients_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPatients.SelectedIndexChanged
        If cbPatients.SelectedItem IsNot Nothing Then
            Dim selectedItem As KeyValuePair(Of Integer, String) = CType(cbPatients.SelectedItem, KeyValuePair(Of Integer, String))
            patientID = selectedItem.Key
            lblPatientID.Text = "Patient ID: " & patientID.ToString()
            LoadPatientTreatments()
            LoadPatientPayments()
        End If
    End Sub



#Region "Grid Control"

    ' Load Treatments for Selected Patient - DevExpress GridControl version
    Private Sub LoadPatientTreatments()
        Try
            selectedTreatments = _invoiceService.GetTreatments(patientID)

            ' Bind to GridControl
            With GridViewTrts.Columns("Value").SummaryItem
                .SummaryType = DevExpress.Data.SummaryItemType.Custom
                .DisplayFormat = "Total (Selected): {0:n2}"
            End With

            BindToTreatGrid()
            CalculateTotals()

        Catch ex As Exception
            MessageBox.Show("Error loading treatments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Bind data to DevExpress GridControl
    Private Sub BindToTreatGrid()
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

        ' Bind to GridControl
        TreatmentsGrid.DataSource = dt
        GridViewTrts.BestFitColumns()

        ' Customize column appearance
        Dim colSelect As GridColumn = GridViewTrts.Columns("Select")
        colSelect.Width = 50
        colSelect.OptionsColumn.AllowEdit = True

        Dim colInvoiced As GridColumn = GridViewTrts.Columns("Invoiced")
        For i As Integer = 0 To GridViewTrts.DataRowCount - 1
            If GridViewTrts.GetDataRow(i)("Invoiced").ToString() = "Yes" Then
                GridViewTrts.SetRowCellValue(i, "Select", False)
                GridViewTrts.SetRowCellValue(i, colInvoiced, "Already Invoiced")
            End If
        Next
    End Sub

    ' Load Treatments for Selected Patient - DevExpress Version
    Private Sub LoadPatientPayments()
        Try
            selectedPayments = _invoiceService.GetPayments(patientID)

            ' Bind to GridControl
            BindToPaymentGrid()
            CalculateTotals()

        Catch ex As Exception
            MessageBox.Show("Error loading treatments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Bind data to DevExpress GridControl
    Private Sub BindToPaymentGrid()
        ' Create a DataTable for binding
        Dim dt As New DataTable()
        dt.Columns.Add("Select", GetType(Boolean))
        dt.Columns.Add("PayID", GetType(Integer))
        dt.Columns.Add("Notes", GetType(String))
        dt.Columns.Add("PayDate", GetType(Date))
        dt.Columns.Add("PayValue", GetType(Decimal))
        dt.Columns.Add("Invoiced", GetType(String))

        For Each payment In selectedPayments
            Dim row As DataRow = dt.NewRow()
            row("Select") = payment.IsSelected
            row("PayID") = payment.PayID
            row("Notes") = payment.Notes
            row("PayDate") = payment.PayDate
            row("PayValue") = payment.PayValue
            row("Invoiced") = If(payment.IsInvoiced, "Yes", "No")
            dt.Rows.Add(row)
        Next

        ' Bind to GridControl
        PaymentsGrid.DataSource = dt
        GridViewPays.BestFitColumns()

        ' Customize column appearance
        Dim colSelect As GridColumn = GridViewPays.Columns("Select")
        colSelect.Width = 50
        colSelect.OptionsColumn.AllowEdit = True

        Dim colInvoiced As GridColumn = GridViewPays.Columns("Invoiced")
        For i As Integer = 0 To GridViewPays.DataRowCount - 1
            If GridViewPays.GetDataRow(i)("Invoiced").ToString() = "Yes" Then
                GridViewPays.SetRowCellValue(i, "Select", False)
                GridViewPays.SetRowCellValue(i, colInvoiced, "Already Invoiced")
            End If
        Next
    End Sub

    ' Configure GridView appearance and behavior
    Private Sub ConfigureTrtGridView()
        ' Configure the Select column
        Dim colSelect As GridColumn = GridViewTrts.Columns("Select")
        colSelect.OptionsColumn.AllowEdit = True

        ' Disable editing for already invoiced rows using a repository item
        Dim repoCheckEdit As New RepositoryItemCheckEdit()
        AddHandler repoCheckEdit.EditValueChanged, AddressOf RepositoryCheckEdit_EditValueChanged
        colSelect.ColumnEdit = repoCheckEdit

        ' Set column widths
        colSelect.Width = 50
        GridViewTrts.Columns("TrtID").Width = 50
        GridViewTrts.Columns("Detail").Width = 300
        GridViewTrts.Columns("Date").Width = 90
        GridViewTrts.Columns("Value").Width = 100
        GridViewTrts.Columns("Discount").Width = 100
        GridViewTrts.Columns("Invoiced").Width = 70

        ' Center align some columns
        colSelect.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        GridViewTrts.Columns("TrtID").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        GridViewTrts.Columns("Invoiced").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    End Sub

    ' Handle checkbox changes directly
    Private Sub RepositoryCheckEdit_EditValueChanged(sender As Object, e As EventArgs)
        Dim checkEdit As CheckEdit = TryCast(sender, CheckEdit)
        If checkEdit IsNot Nothing AndAlso GridViewTrts.IsEditing Then
            Dim rowHandle As Integer = GridViewTrts.FocusedRowHandle
            Dim row As DataRow = GridViewTrts.GetDataRow(rowHandle)

            If row IsNot Nothing Then
                Dim trtID As Integer = CInt(row("TrtID"))
                Dim isInvoiced As Boolean = row("Invoiced").ToString() = "Yes" OrElse "Already Invoiced"
                Dim trtDate As DateTime = CDate(row("Date"))
                Dim trtValue As Decimal = CDec(row("Value"))
                Dim trtDiscount As Decimal = CDec(row("Discount"))

                If isInvoiced Then
                    ' Cancel the change
                    checkEdit.Checked = False
                    Return
                End If

                Dim isSelected As Boolean = checkEdit.Checked

                ' Update our list
                Dim treatmentIndex = selectedTreatments.FindIndex(Function(t) t.TrtID = trtID)
                If treatmentIndex >= 0 Then
                    selectedTreatments(treatmentIndex).IsSelected = isSelected
                    selectedTreatments(treatmentIndex).TrtValue = trtValue
                    selectedTreatments(treatmentIndex).Discount = trtDiscount
                    selectedTreatments(treatmentIndex).TrtDate = trtDate
                    CalculateTotals()
                End If
            End If
        End If
    End Sub

    Private Sub GridViewTrts_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridViewTrts.CellValueChanged

        If e.Column.FieldName <> "Select" Then Return

        Dim isSelected As Boolean = CBool(e.Value)

        Dim row As DataRow = GridViewTrts.GetDataRow(e.RowHandle)
        If row Is Nothing Then Return

        Dim isInvoiced As Boolean =
        row("Invoiced").ToString() = "Yes" OrElse
        row("Invoiced").ToString() = "Already Invoiced"

        If isInvoiced Then Return

        Dim trtID As Integer = CInt(row("TrtID"))

        Dim treatmentIndex = selectedTreatments.FindIndex(Function(t) t.TrtID = trtID)

        If treatmentIndex >= 0 Then
            selectedTreatments(treatmentIndex).IsSelected = isSelected
            selectedTreatments(treatmentIndex).TrtValue = CDec(row("Value"))
            selectedTreatments(treatmentIndex).Discount = CDec(row("Discount"))
            selectedTreatments(treatmentIndex).TrtDate = CDate(row("Date"))
        End If

        CalculateTotals()

    End Sub

    ' GridView Cell Value Changed Event - DevExpress Version
    Private Sub GridViewPays_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridViewPays.CellValueChanged
        If e.Column.FieldName = "Select" Then
            Dim rowHandle As Integer = e.RowHandle
            Dim row As DataRow = GridViewPays.GetDataRow(rowHandle)
            If payPage Then
                If row IsNot Nothing Then
                    Dim payID As Integer = CInt(row("PayID"))
                    Dim isInvoiced As Boolean = row("Invoiced").ToString() = "Yes" OrElse row("Invoiced").ToString() = "Already Invoiced"
                    Dim payDate As DateTime = CDate(row("PayDate"))
                    Dim payValue As Decimal = CDec(row("PayValue"))
                    If isInvoiced Then
                        ' Just exit without doing anything
                        Return
                    End If

                    Dim isSelected As Boolean = CBool(e.Value)

                    ' Find and update the treatment in our list
                    Dim paymentIndex = selectedPayments.FindIndex(Function(t) t.PayID = payID)
                    If paymentIndex >= 0 Then
                        selectedPayments(paymentIndex).IsSelected = isSelected
                        selectedPayments(paymentIndex).PayValue = payValue
                        selectedPayments(paymentIndex).PayDate = payDate
                        CalculateTotals()
                    End If
                End If
            End If
        End If
    End Sub

    ' Custom Draw Cell to disable already invoiced rows
    Private Sub GridViewTrts_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridViewTrts.CustomDrawCell
        If e.Column.FieldName = "Select" Then
            Dim row As DataRow = GridViewTrts.GetDataRow(e.RowHandle)
            If trtPage Then
                If row IsNot Nothing AndAlso (row("Invoiced").ToString() = "Yes" OrElse row("Invoiced").ToString() = "Already Invoiced") Then
                    ' Disable the checkbox
                    e.Appearance.BackColor = Color.LightGray
                    e.Appearance.ForeColor = Color.DarkGray

                    ' This prevents the checkbox from being clickable
                    e.Handled = True
                End If
            End If
        End If
    End Sub
    ' Custom Draw Cell to disable already invoiced rows
    Private Sub GridViewPays_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridViewPays.CustomDrawCell
        If e.Column.FieldName = "Select" Then
            Dim row As DataRow = GridViewPays.GetDataRow(e.RowHandle)
            If payPage Then
                If row IsNot Nothing AndAlso (row("Invoiced").ToString() = "Yes" OrElse row("Invoiced").ToString() = "Already Invoiced") Then
                    ' Disable the checkbox
                    e.Appearance.BackColor = Color.LightGray
                    e.Appearance.ForeColor = Color.DarkGray

                    ' This prevents the checkbox from being clickable
                    e.Handled = True
                End If
            End If
        End If
    End Sub

    ' Prevent editing of selection column for invoiced rows
    Private Sub GridViewTrts_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridViewTrts.ShowingEditor
        If trtPage Then
            If GridViewTrts.FocusedColumn.FieldName = "Select" Then
                Dim row As DataRow = GridViewTrts.GetDataRow(GridViewTrts.FocusedRowHandle)
                If row IsNot Nothing AndAlso (row("Invoiced").ToString() = "Yes" OrElse row("Invoiced").ToString() = "Already Invoiced") Then
                    e.Cancel = True ' Don't show editor for already invoiced rows
                End If
            End If
        End If
    End Sub
    ' Prevent editing of selection column for invoiced rows
    Private Sub GridViewPays_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridViewPays.ShowingEditor
        If payPage Then
            If GridViewPays.FocusedColumn.FieldName = "Select" Then
                Dim row As DataRow = GridViewPays.GetDataRow(GridViewPays.FocusedRowHandle)
                If row IsNot Nothing AndAlso (row("Invoiced").ToString() = "Yes" OrElse row("Invoiced").ToString() = "Already Invoiced") Then
                    e.Cancel = True ' Don't show editor for already invoiced rows
                End If
            End If
        End If
    End Sub

    ' Row-click to select/deselect (optional)
    Private Sub GridViewTrts_RowClick(sender As Object, e As RowClickEventArgs) Handles GridViewTrts.RowClick
        Dim rowHandle As Integer = GridViewTrts.FocusedRowHandle
        Dim currentValue As Boolean = CBool(GridViewTrts.GetRowCellValue(rowHandle, "Select"))
        GridViewTrts.SetRowCellValue(rowHandle, "Select", Not currentValue)
        CalculateTotals()
    End Sub

    ' Double-click to select/deselect (optional)
    Private Sub GridViewPays_RowClick(sender As Object, e As RowClickEventArgs) Handles GridViewPays.RowClick
        Dim rowHandle As Integer = GridViewPays.FocusedRowHandle
        Dim currentValue As Boolean = CBool(GridViewPays.GetRowCellValue(rowHandle, "Select"))
        GridViewPays.SetRowCellValue(rowHandle, "Select", Not currentValue)
        CalculateTotals()
    End Sub

#End Region


    ' If you have "Select All" and "Select None" buttons, update them:
    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        If trtPage Then
            For i As Integer = 0 To GridViewTrts.DataRowCount - 1
                Dim row As DataRow = GridViewTrts.GetDataRow(i)
                If row IsNot Nothing AndAlso (row("Invoiced").ToString() = "Yes" OrElse row("Invoiced").ToString() = "Already Invoiced") Then
                    GridViewTrts.SetRowCellValue(i, "Select", False)
                Else
                    GridViewTrts.SetRowCellValue(i, "Select", True)
                    ' Update our list
                    Dim trtID As Integer = CInt(row("TrtID"))
                    Dim treatmentIndex = selectedTreatments.FindIndex(Function(t) t.TrtID = trtID)
                    If treatmentIndex >= 0 Then
                        selectedTreatments(treatmentIndex).IsSelected = True
                    End If
                End If
            Next
            CalculateTotals()
        ElseIf payPage Then
            For i As Integer = 0 To GridViewPays.DataRowCount - 1
                Dim row As DataRow = GridViewPays.GetDataRow(i)
                If row IsNot Nothing AndAlso (row("Invoiced").ToString() = "Yes" OrElse row("Invoiced").ToString() = "Already Invoiced") Then
                    GridViewPays.SetRowCellValue(i, "Select", False)
                Else
                    GridViewPays.SetRowCellValue(i, "Select", True)
                    ' Update our list
                    Dim payID As Integer = CInt(row("PayID"))
                    Dim paymentIndex = selectedPayments.FindIndex(Function(t) t.PayID = payID)
                    If paymentIndex >= 0 Then
                        selectedPayments(paymentIndex).IsSelected = True
                    End If
                End If
            Next
            CalculateTotals()
        End If

    End Sub

    Private Sub btnSelectNone_Click(sender As Object, e As EventArgs) Handles btnSelectNone.Click
        If trtPage Then
            For i As Integer = 0 To GridViewTrts.DataRowCount - 1
                GridViewTrts.SetRowCellValue(i, "Select", False)

                ' Update our list
                Dim row As DataRow = GridViewTrts.GetDataRow(i)
                If row IsNot Nothing Then
                    Dim trtID As Integer = CInt(row("TrtID"))
                    Dim treatmentIndex = selectedTreatments.FindIndex(Function(t) t.TrtID = trtID)
                    If treatmentIndex >= 0 Then
                        selectedTreatments(treatmentIndex).IsSelected = False
                    End If
                End If
            Next
            CalculateTotals()
        ElseIf payPage Then
            For i As Integer = 0 To GridViewPays.DataRowCount - 1
                Dim row As DataRow = GridViewPays.GetDataRow(i)
                If row IsNot Nothing AndAlso (row("Invoiced").ToString() <> "Yes" OrElse row("Invoiced").ToString() = "Already Invoiced") Then
                    GridViewPays.SetRowCellValue(i, "Select", True)

                    ' Update our list
                    Dim payID As Integer = CInt(row("PayID"))
                    Dim paymentIndex = selectedPayments.FindIndex(Function(t) t.PayID = payID)
                    If paymentIndex >= 0 Then
                        selectedPayments(paymentIndex).IsSelected = False
                    End If
                End If
            Next
            CalculateTotals()
        End If

    End Sub






    ' Usage:
    'lblSubTotal.Text = FormatCustomCurrency(subTotal, "$")
    'lblSubTotal.Text = FormatCustomCurrency(subTotal, "€")


    ' Calculate Invoice Totals
    Private Sub CalculateTotals()
        Dim totals = _invoiceService.CalculateTotals(selectedTreatments, selectedPayments, chkTax.Checked, DefaultTaxRate)

        lblSubTotal.Text = totals.SubTotal.ToCurrencyString(CurrencyType.ILS_Ar)
        lblDiscount.Text = totals.DiscountAmount.ToCurrencyString(CurrencyType.ILS_Ar)
        lblTaxAmount.Text = totals.TaxAmount.ToCurrencyString(CurrencyType.ILS_Ar)
        lblTotalAmount.Text = totals.BalanceDue.ToCurrencyString(CurrencyType.ILS_Ar, True)
        lblTotalPays.Text = totals.TotalPayments.ToCurrencyString(CurrencyType.ILS_Ar, True)

        lblSelectedTrtCount.Text = totals.SelectedTreatments.ToString() & " treatment(s) selected"
        lblSelectedPayCount.Text = totals.SelectedPayments.ToString() & " payment(s) selected"
    End Sub

    ' Generate Invoice Number
    Private Function GenerateInvoiceNumber() As String
        Return _invoiceService.GetNextInvoiceNumber()
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
                                                 txtNotes.Text, chkTax.Checked)
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
            Dim result = _invoiceService.CreateInvoice(
                patientID,
                dtpInvoiceDate.Value,
                dtpDueDate.Value,
                txtNotes.Text,
                chkTax.Checked,
                DefaultTaxRate,
                selectedTreatments,
                selectedPayments,
                CurrentUser.UsID)

            MessageBox.Show($"Invoice #{result.InvoiceNumber} created successfully!" & vbCrLf &
                            $"Total Amount: {FormatCurrency(result.Totals.TotalBeforePayments)}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadPatientTreatments()
            lblInvoiceNumber.Text = "Will be generated on save"
            txtNotes.Clear()
            Return
        Catch ex As Exception
            MessageBox.Show("Error saving invoice: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

#If False Then
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
                    Dim taxRate As Decimal = 0.16D

                    For Each treatment In selectedTreatments
                        If treatment.IsSelected Then
                            subTotal += treatment.TrtValue
                            totalDiscount += treatment.Discount
                        End If
                    Next

                    Dim taxAmount As Decimal = (subTotal - totalDiscount) * taxRate
                    'Dim totalAmount As Decimal = (subTotal - totalDiscount) + taxAmount
                    Dim totalAmount As Decimal = 0
                    If chkTax.Checked Then
                        totalAmount = (subTotal - totalDiscount) + taxAmount
                    Else
                        totalAmount = (subTotal - totalDiscount)
                    End If
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

                    '============================================
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

                    '============================================
                    ' 6. If we have selected payments, allocate them
                    If selectedPayments.Any(Function(p) p.IsSelected) Then
                        ' 1. Insert into Invoice_Payments table (link payment to invoice)
                        Dim insertAllocationQuery As String = "
                        INSERT INTO Invoice_Payments 
                        (InvoiceID, PayID, AllocatedAmount, AllocationDate, AllocatedBy, Notes)
                        VALUES 
                        (@InvoiceID, @PayID, @AllocatedAmount, GETDATE(), @AllocatedBy, @Notes)"

                        Dim paymentAmount As Decimal = 0
                        For Each payment In selectedPayments
                            If Not payment.IsSelected Then Continue For
                            Using cmd As New SqlCommand(insertAllocationQuery, conn, transaction)
                                cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                                cmd.Parameters.AddWithValue("@PayID", payment.PayID)
                                cmd.Parameters.AddWithValue("@AllocatedAmount", payment.PayValue)
                                cmd.Parameters.AddWithValue("@AllocatedBy", CurrentUser.UsID) ' You can set current user ID here
                                cmd.Parameters.AddWithValue("@Notes", payment.Notes) ' If(String.IsNullOrEmpty(txtNotes.Text), DBNull.Value, "Payment allocated to invoice"))
                                paymentAmount += payment.PayValue
                                cmd.ExecuteNonQuery()
                            End Using
                        Next
                        '============================================
                        ' 2. Update Invoices table (update AmountPaid and BalanceDue)
                        Dim updateInvoiceQuery As String = "
                        UPDATE Invoices 
                        SET 
                            AmountPaid = AmountPaid + @PaymentAmount,
                            BalanceDue = BalanceDue - @PaymentAmount,
                            InvoiceStatus = CASE 
                                WHEN (BalanceDue - @PaymentAmount) <= 0 THEN 2 -- Paid
                                WHEN (BalanceDue - @PaymentAmount) < TotalAmount THEN 3 -- Partially Paid
                                ELSE InvoiceStatus 
                            END,
                            ModifiedDate = GETDATE()
                        WHERE InvoiceID = @InvoiceID"

                        Using cmd As New SqlCommand(updateInvoiceQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@PaymentAmount", paymentAmount)
                            cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                            cmd.ExecuteNonQuery()
                        End Using

                        ' 3. Add to Invoice_History
                        Dim insertHistoryQuery2 As String = "
                        INSERT INTO Invoice_History 
                        (InvoiceID, OldStatus, NewStatus, ChangeDate, ChangeReason)
                        SELECT 
                            @InvoiceID,
                            InvoiceStatus,
                            CASE 
                                WHEN (BalanceDue - @PaymentAmount) <= 0 THEN 2 -- Paid
                                WHEN (BalanceDue - @PaymentAmount) < TotalAmount THEN 3 -- Partially Paid
                                ELSE InvoiceStatus 
                            END,
                            GETDATE(),
                            @ChangeReason
                        FROM Invoices 
                        WHERE InvoiceID = @InvoiceID"

                        Using cmd As New SqlCommand(insertHistoryQuery2, conn, transaction)
                            cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                            cmd.Parameters.AddWithValue("@PaymentAmount", paymentAmount)
                            cmd.Parameters.AddWithValue("@ChangeReason", $"Payment of {paymentAmount.ToCurrencyString(CurrencyType.ILS_Ar)} received ")
                            cmd.ExecuteNonQuery()
                        End Using

                    End If
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

#End If
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

    Private Sub chkTax_CheckedChanged(sender As Object, e As EventArgs) Handles chkTax.CheckedChanged
        CalculateTotals()
    End Sub

    Private trtPage, payPage As Boolean

    Private Sub ctlTabs_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles ctlTabs.SelectedPageChanged

        If e.Page Is TreatPage Then
            trtPage = True
            payPage = False
        ElseIf e.Page Is PaymentPage Then
            trtPage = False
            payPage = True
        End If
    End Sub

    Private Sub SpinTax_EditValueChanged(sender As Object, e As EventArgs) Handles SpinTax.EditValueChanged
        LabelTax.Text = If(Eng, $"Tax ({SpinTax.Value}%):", $"ض. ق. م. {SpinTax.Value}%")
        DefaultTaxRate = SpinTax.Value
    End Sub

    Private Sub ctlTabs_Selected(sender As Object, e As DevExpress.XtraTab.TabPageEventArgs) Handles ctlTabs.Selected
        If e.Page Is TreatPage Then
            trtPage = True
            payPage = False
        ElseIf e.Page Is PaymentPage Then
            trtPage = False
            payPage = True
        End If
    End Sub


End Class