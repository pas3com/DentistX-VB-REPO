Imports System.Data.SqlClient
Imports Dapper

Public Class frmAddPayment
    Private invoiceID As Integer
    Private patientID As Integer
    Private balanceDue As Decimal
    Private connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Sub New(ByVal invID As Integer)
        InitializeComponent()
        invoiceID = invID
        LoadInvoiceInfo()
        SetupControls()
    End Sub

    Private Sub LoadInvoiceInfo()
        Try
            Using conn As New SqlConnection(connectionString)
                ' Get invoice and patient info
                Dim query As String = "
                    SELECT 
                        i.InvoiceID,
                        i.PatientID,
                        i.InvoiceNumber,
                        i.TotalAmount,
                        i.AmountPaid,
                        i.BalanceDue,
                        p.PatientName
                    FROM Invoices i
                    INNER JOIN Patient p ON i.PatientID = p.PatientID
                    WHERE i.InvoiceID = @InvoiceID"

                Dim result = conn.QueryFirstOrDefault(Of InvoiceInfo)(query, New With {.InvoiceID = invoiceID})

                If result IsNot Nothing Then
                    patientID = result.PatientID
                    balanceDue = result.BalanceDue

                    ' Display info
                    lblInvoiceNumber.Text = result.InvoiceNumber
                    lblPatientName.Text = result.PatientName
                    lblTotalAmount.Text = FormatCurrency(result.TotalAmount)
                    lblAmountPaid.Text = FormatCurrency(result.AmountPaid)
                    lblBalanceDue.Text = FormatCurrency(balanceDue)

                    ' Set max value for payment
                    txtPayValue.Properties.MaxValue = CDec(balanceDue)
                    txtPayValue.EditValue = balanceDue ' Default to full balance
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading invoice info: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupControls()
        HideShowChqs(False)
        ' Set default payment date
        dtpPayDate.DateTime = Date.Today

        ' Load payment types
        cbPayType.Properties.Items.AddRange({
            "Cash", "Credit Card", "Debit Card", "Check",
            "Bank Transfer", "Insurance", "Other"
        })
        cbPayType.SelectedIndex = 0

        ' Show/hide check fields based on payment type
        AddHandler cbPayType.EditValueChanged, AddressOf cbPayType_EditValueChanged

        ' Set default check date
        dtpChqDueDate.DateTime = Date.Today.AddDays(30)
    End Sub

    Private Sub cbPayType_EditValueChanged(sender As Object, e As EventArgs)
        Dim isCheckPayment As Boolean = (cbPayType.Text = "Check")

        HideShowChqs(isCheckPayment)
    End Sub

    Private Sub HideShowChqs(hide As Boolean)
        lblChqNumber.Visible = hide
        txtChqNumber.Visible = hide
        lblChqDueDate.Visible = hide
        dtpChqDueDate.Visible = hide
        lblChqBank.Visible = hide
        txtChqBank.Visible = hide
        lblChqOwner.Visible = hide
        txtChqOwner.Visible = hide
        lblAccountNumber.Visible = hide
        txtAccountNumber.Visible = hide
        btnResetChqs.Visible = hide
        lblChqValue.Visible = hide
        txtChqValue.Visible = hide
        chkIsCashed.Visible = hide
    End Sub
    Private Function ValidateInput() As Boolean
        ' Validate payment amount
        If txtPayValue.EditValue Is Nothing OrElse CDec(txtPayValue.EditValue) <= 0 Then
            MessageBox.Show("Please enter a valid payment amount.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPayValue.Focus()
            Return False
        End If

        Dim paymentAmount As Decimal = CDec(txtPayValue.EditValue)

        If paymentAmount > balanceDue Then
            MessageBox.Show($"Payment amount cannot exceed balance due of {FormatCurrency(balanceDue)}.",
                          "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPayValue.Focus()
            Return False
        End If

        ' Validate payment type
        If String.IsNullOrEmpty(cbPayType.Text) Then
            MessageBox.Show("Please select a payment type.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cbPayType.Focus()
            Return False
        End If

        ' Validate check fields if payment type is check
        If cbPayType.Text = "Check" Then
            If String.IsNullOrEmpty(txtChqNumber.Text) Then
                MessageBox.Show("Please enter check number.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtChqNumber.Focus()
                Return False
            End If

            If String.IsNullOrEmpty(txtChqOwner.Text) Then
                MessageBox.Show("Please enter check owner name.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtChqOwner.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValidateInput() Then
            Return
        End If

        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Dim transaction As SqlTransaction = conn.BeginTransaction()

                Try
                    Dim paymentAmount As Decimal = CDec(txtPayValue.EditValue)
                    Dim payDate As Date = dtpPayDate.DateTime
                    Dim payType As String = cbPayType.Text
                    Dim notes As String = txtNotes.Text

                    ' 1. Insert into Patient_Pays table
                    Dim insertPaymentQuery As String = "
                        INSERT INTO Patient_Pays 
                        (TrtID, PatientID, PayValue, PayDate, Notes, PayType, 
                         ChqOwner, AccountNumber, ChqNumber, ChqDueDate, ChqBank, IsCashed, 
                         InsuranceCompany, InsuranceNotes, IsForward, ForwardFromTo, ReceivedBy, IsReturned)
                        VALUES 
                        (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType,
                         @ChqOwner, @AccountNumber, @ChqNumber, @ChqDueDate, @ChqBank, @IsCashed,
                         @InsuranceCompany, @InsuranceNotes, @IsForward, @ForwardFromTo, @ReceivedBy, @IsReturned)
                        SELECT SCOPE_IDENTITY()"

                    Dim payID As Integer

                    Using cmd As New SqlCommand(insertPaymentQuery, conn, transaction)
                        ' For Patient_Pays, we need a TrtID. Since we're paying an invoice, 
                        ' we need to get a TrtID from the invoice items or use a dummy value.
                        ' Let's get the first treatment ID from the invoice
                        Dim getTrtIDQuery As String = "
                            SELECT TOP 1 TrtID 
                            FROM Invoice_Items 
                            WHERE InvoiceID = @InvoiceID"

                        Dim trtIDCmd As New SqlCommand(getTrtIDQuery, conn, transaction)
                        trtIDCmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                        Dim trtIDObj As Object = trtIDCmd.ExecuteScalar()
                        Dim trtID As Integer = If(trtIDObj IsNot Nothing AndAlso Not IsDBNull(trtIDObj), CInt(trtIDObj), 0)

                        ' Set parameters
                        cmd.Parameters.AddWithValue("@TrtID", trtID)
                        cmd.Parameters.AddWithValue("@PatientID", patientID)
                        cmd.Parameters.AddWithValue("@PayValue", paymentAmount)
                        cmd.Parameters.AddWithValue("@PayDate", payDate)
                        cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(notes), DBNull.Value, notes))
                        cmd.Parameters.AddWithValue("@PayType", payType)

                        ' Check specific parameters
                        If payType = "Check" Then
                            cmd.Parameters.AddWithValue("@ChqOwner", txtChqOwner.Text)
                            cmd.Parameters.AddWithValue("@AccountNumber", If(String.IsNullOrEmpty(txtAccountNumber.Text), DBNull.Value, txtAccountNumber.Text))
                            cmd.Parameters.AddWithValue("@ChqNumber", txtChqNumber.Text)
                            cmd.Parameters.AddWithValue("@ChqDueDate", dtpChqDueDate.DateTime)
                            cmd.Parameters.AddWithValue("@ChqBank", If(String.IsNullOrEmpty(txtChqBank.Text), DBNull.Value, txtChqBank.Text))
                            cmd.Parameters.AddWithValue("@IsCashed", chkIsCashed.Checked)
                        Else
                            cmd.Parameters.AddWithValue("@ChqOwner", DBNull.Value)
                            cmd.Parameters.AddWithValue("@AccountNumber", DBNull.Value)
                            cmd.Parameters.AddWithValue("@ChqNumber", DBNull.Value)
                            cmd.Parameters.AddWithValue("@ChqDueDate", DBNull.Value)
                            cmd.Parameters.AddWithValue("@ChqBank", DBNull.Value)
                            cmd.Parameters.AddWithValue("@IsCashed", DBNull.Value)
                            cmd.Parameters.AddWithValue("@IsForward", False)
                            cmd.Parameters.AddWithValue("@ForwardFromTo", DBNull.Value)
                        End If

                        ' Insurance fields (not implemented in UI, but keeping for structure)
                        cmd.Parameters.AddWithValue("@InsuranceCompany", DBNull.Value)
                        cmd.Parameters.AddWithValue("@InsuranceNotes", DBNull.Value)
                        cmd.Parameters.AddWithValue("@ReceivedBy", DBNull.Value)
                        cmd.Parameters.AddWithValue("@IsReturned", False)


                        payID = CInt(cmd.ExecuteScalar())
                    End Using

                    ' 2. Insert into Invoice_Payments table (link payment to invoice)
                    Dim insertAllocationQuery As String = "
                        INSERT INTO Invoice_Payments 
                        (InvoiceID, PayID, AllocatedAmount, AllocationDate, AllocatedBy, Notes)
                        VALUES 
                        (@InvoiceID, @PayID, @AllocatedAmount, GETDATE(), @AllocatedBy, @Notes)"

                    Using cmd As New SqlCommand(insertAllocationQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                        cmd.Parameters.AddWithValue("@PayID", payID)
                        cmd.Parameters.AddWithValue("@AllocatedAmount", paymentAmount)
                        cmd.Parameters.AddWithValue("@AllocatedBy", CurrentUser.UsID) ' You can set current user ID here
                        cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(txtNotes.Text), DBNull.Value, "Payment allocated to invoice"))

                        cmd.ExecuteNonQuery()
                    End Using

                    ' 3. Update Invoices table (update AmountPaid and BalanceDue)
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

                    ' 4. Add to Invoice_History
                    Dim insertHistoryQuery As String = "
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

                    Using cmd As New SqlCommand(insertHistoryQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                        cmd.Parameters.AddWithValue("@PaymentAmount", paymentAmount)
                        cmd.Parameters.AddWithValue("@ChangeReason", $"Payment of {FormatCurrency(paymentAmount)} received via {payType}")
                        cmd.ExecuteNonQuery()
                    End Using

                    transaction.Commit()

                    MessageBox.Show($"Payment of {FormatCurrency(paymentAmount)} saved successfully!",
                                  "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Me.DialogResult = DialogResult.OK
                    Me.Close()

                Catch ex As Exception
                    transaction.Rollback()
                    Throw
                End Try
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving payment: " & ex.Message, "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' Helper class for invoice info
    Private Class InvoiceInfo
        Public Property InvoiceID As Integer
        Public Property PatientID As Integer
        Public Property InvoiceNumber As String
        Public Property TotalAmount As Decimal
        Public Property AmountPaid As Decimal
        Public Property BalanceDue As Decimal
        Public Property PatientName As String
    End Class
End Class



'Public Class frmAddPayment
'    Private invoiceID As Integer

'    Public Sub New(ByVal invID As Integer)
'        InitializeComponent()
'        invoiceID = invID
'        LoadInvoiceInfo()
'    End Sub

'    Private Sub LoadInvoiceInfo()
'        ' Load invoice balance due
'        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
'            conn.Open()
'            Dim query As String = "SELECT BalanceDue FROM Invoices WHERE InvoiceID = @InvoiceID"
'            Using cmd As New SqlCommand(query, conn)
'                cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
'                Dim balanceDue As Decimal = CDec(cmd.ExecuteScalar())
'                lblBalanceDue.Text = FormatCurrency(balanceDue)
'                txtPayValue.Properties.MaxValue = balanceDue
'            End Using
'        End Using
'    End Sub

'    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
'        ' Save payment logic here
'        Me.DialogResult = DialogResult.OK
'        Me.Close()
'    End Sub

'    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
'        Me.DialogResult = DialogResult.Cancel
'        Me.Close()
'    End Sub
'End Class