Imports DentistX
Imports DevExpress.Data
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class FrmExchMny

    Private clsContactDATA As New ContactDATA
    Private clsLoanDATA As New LoanDATA
    Private clsContact As New Contact
    Private clsLoan As New Loan
    Private Sub FrmExchMny_Load(sender As Object, e As EventArgs) Handles Me.Load
        ContactBalanceBS.DataSource = clsLoanDATA.GetContactBalances
        'LoanGiven' THEN Amount
        'RepaymentReceived' THEN Amount
        'LoanReceived' THEN -Amount
        'RepaymentGiven'
        cboTransType.Properties.Items.Clear()
        cboTransType.Properties.Items.AddRange(If(Eng, {"Lend", "Borrow"}, {"إقراض", "إقتراض"}))
        If GridViewContactInfo.RowCount = 0 Then Exit Sub
        Dim row, contactId As Integer
        row = GridViewContactInfo.FocusedRowHandle
        contactId = CInt(GridViewContactInfo.GetRowCellDisplayText(row, colContactID))
        DisplayContactLoans(contactId)
    End Sub

    Private Sub btnAddTrans_Click(sender As Object, e As EventArgs) Handles btnAddTrans.Click
        If ContactsCombo1.CboContacts.Properties.Items.Count = 0 Then
            MsgBox("Select A Contact....")
            Exit Sub
        End If
        If cboTransType.SelectedIndex < 0 Then
            MsgBox("Select A Transaction Type....")
            Exit Sub
        End If
        If txtAmount.Text.Length = 0 OrElse Val(txtAmount.Text) = 0 Then
            MsgBox("Set Money value....")
            Exit Sub
        End If
        If txtDescription.Text.Length = 0 Then
            MsgBox("Set Transaction Description....")
            Exit Sub
        End If
        If dtTransDate.Text.Length < 10 Then
            MsgBox("Set Transaction Date....")
            Exit Sub
        End If

        clsLoan = New Loan With {.ContactID = ContactsCombo1.ContactID,
                                         .Amount = Val(txtAmount.Text),
                                         .Direction = cboTransType.Text,
                                         .Description = txtDescription.Text,
                                         .LoanDate = dtTransDate.DateTime,
                                         .CreatedAt = Now}
        If clsLoanDATA.AddLoan(clsLoan) Then
            ContactBalanceBS.DataSource = clsLoanDATA.GetContactBalances '(ContactCombo1.ContactID)
            DisplayContactLoans(clsLoan.ContactID)
        End If
    End Sub

    ' In your form where you display the transactions (e.g., when a contact is selected)
    Private Sub DisplayContactLoans1(contactId As Integer)
        Try
            ' Get the transactions
            Dim transactions = clsLoanDATA.GetLoansByContact(contactId)
            LoansBS.DataSource = transactions

            GridLoansInfo.DataSource = LoansBS
        Catch ex As Exception
            MessageBox.Show($"Error loading transactions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DisplayContactLoans(contactId As Integer)
        ' Load loans into the grid
        Dim loans = clsLoanDATA.GetLoansByContact(contactId)
        LoansBS.DataSource = loans

        GridLoansInfo.DataSource = LoansBS


        ' If at least one loan, manually call DisplayLoanPayments for the first one
        If loans.Count > 0 Then
            Dim firstLoanId As Integer = loans(0).LoanID
            DisplayLoanPayments(firstLoanId)

            ' Optional: set focus to first row so it’s visually selected
            GridViewLoansInfo.FocusedRowHandle = 0
        Else
            GridPayments.DataSource = Nothing
            lblLoanID.Text = String.Empty
        End If
    End Sub


    Private Sub DisplayLoanPayments(loanId As Integer)
        Try
            ' Get the transactions
            Dim payments = clsLoanDATA.GetRepayments(loanId)
            PaysBS.DataSource = payments

            GridPayments.DataSource = PaysBS
        Catch ex As Exception
            MessageBox.Show($"Error loading transactions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowLoansInfo(contactId As Integer)
        Dim loans = clsLoanDATA.GetLoansByContact(contactId)

        If loans.Count = 0 Then
            'lblLoanInfo.Text = "No loans found for this contact."
            Return
        End If

        Dim sb As New Text.StringBuilder()

        For Each loan In loans
            sb.AppendLine($"Loan ID: {loan.LoanID}")
            sb.AppendLine($"Direction: {loan.Direction}")
            sb.AppendLine($"Amount: {loan.OriginalAmount:C2}")
            sb.AppendLine($"Repaid: {loan.TotalRepaid:C2}")
            sb.AppendLine($"Remaining: {loan.RemainingBalance:C2}")
            sb.AppendLine($"Date: {loan.LoanDate:yyyy-MM-dd}")
            sb.AppendLine($"Note: {loan.Description}")
            sb.AppendLine(New String("-"c, 40))
        Next

        'lblLoanInfo.Text = sb.ToString()
    End Sub





    Private ContactID As Integer = 0
    Private ContactName As String = ""
    Private Sub ContactCombo1_ContactValueChanged(sender As Object, e As ContactsCombo.ContactsIndexChangedEvent) Handles ContactsCombo1.ContactsValueChanged
        ContactBalanceBS.DataSource = clsLoanDATA.GetContactBalances '(e.ContactID)
        ContactID = e.ContactID
        ContactName = e.CName
        If ContactID > 0 Then DisplayContactLoans(ContactID)
    End Sub

    Private Sub LoansBS_CurrentChanged(sender As Object, e As EventArgs) Handles LoansBS.CurrentChanged
        Dim cur = TryCast(LoansBS.Current, LoanBalance)
        If cur Is Nothing Then
            lblLoanID.Text = String.Empty
            Return
        End If
        lblLoanID.Text = cur.LoanID.ToString()
    End Sub

    Private Function TryGetSelectedRepayment() As Repayment
        Dim r = TryCast(PaysBS.Current, Repayment)
        If r IsNot Nothing Then Return r
        If GridViewPayments.RowCount = 0 OrElse GridViewPayments.FocusedRowHandle < 0 Then Return Nothing
        Return TryCast(GridViewPayments.GetRow(GridViewPayments.FocusedRowHandle), Repayment)
    End Function

    Private Sub btnEditPay_Click(sender As Object, e As EventArgs) Handles btnEditPay.Click
        Dim r = TryGetSelectedRepayment()
        If r Is Nothing Then
            MsgBox(If(Eng, "Select a repayment row.", "اختر صف السداد."), MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Using dlg As New FrmEditRepayment()
            dlg.RepaymentID = r.RepaymentID
            dlg.LoanID = r.LoanID
            dlg.SetValues(r.Amount, r.RepaymentDate, r.Notes)
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                DisplayLoanPayments(r.LoanID)
            End If
        End Using
    End Sub

    Private Sub btnDelPay_Click(sender As Object, e As EventArgs) Handles btnDelPay.Click
        Dim r = TryGetSelectedRepayment()
        If r Is Nothing Then
            MsgBox(If(Eng, "Select a repayment row.", "اختر صف السداد."), MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox(If(Eng, "Delete this repayment?", "حذف هذا السداد؟"),
                  MsgBoxStyle.YesNo Or MsgBoxStyle.Question) <> MsgBoxResult.Yes Then Exit Sub
        If clsLoanDATA.DeleteRepayment(r.RepaymentID) Then
            DisplayLoanPayments(r.LoanID)
        Else
            MsgBox(If(Eng, "Repayment could not be deleted.", "تعذر حذف السداد."), MsgBoxStyle.Exclamation)
        End If
    End Sub
    Private Sub btnAddPay_Click(sender As Object, e As EventArgs) Handles btnAddPay.Click
        If lblLoanID.Text.Length = 0 Then
            MsgBox("Select A Loan....")
            Exit Sub
        End If

        If ContactsCombo1.CboContacts.Properties.Items.Count = 0 Then
            MsgBox("Select A Contact....")
            Exit Sub
        End If

        If txtRepayValue.Text.Length = 0 OrElse Val(txtRepayValue.Text) = 0 Then
            MsgBox("Set Pay value....")
            Exit Sub
        End If

        If dtRepayDate.Text.Length < 10 Then
            MsgBox("Set Payment Date....")
            Exit Sub
        End If

        Dim clsRepayment As New Repayment With {
                                                .LoanID = CInt(lblLoanID.Text),
                                                .Amount = Val(txtRepayValue.Text),
                                                .Notes = txtNotes.Text,
                                                .RepaymentDate = dtRepayDate.DateTime,
                                                .CreatedAt = Now}
        clsLoanDATA.AddRepayment(clsRepayment)
        DisplayLoanPayments(clsRepayment.LoanID)
    End Sub



    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits (0-9)
        If Char.IsDigit(e.KeyChar) Then
            Return
        End If

        ' Allow only ONE decimal point (for prices)
        If e.KeyChar = "."c AndAlso Not txtAmount.Text.Contains(".") Then
            Return
        End If

        ' Block any other character
        e.Handled = True
    End Sub

    Private Sub txtAmount_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtAmount.PreviewKeyDown
        ' Allow Ctrl+V (paste) - We'll handle validation separately
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Return
        End If

        ' Allow Backspace, Delete, Arrows, Tab
        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
       e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
       e.KeyCode = Keys.Tab Then
            Return
        End If

        ' Allow numbers (0-9)
        If e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9 Then
            Return
        End If

        ' Allow numpad numbers (0-9)
        If e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9 Then
            Return
        End If

        ' Allow ONE decimal point (if needed)
        If e.KeyCode = Keys.Decimal AndAlso Not txtAmount.Text.Contains(".") Then
            Return
        End If

        ' Block all other keys
        e.IsInputKey = False
    End Sub

    Private Sub txtAmount_EditValueChanged(sender As Object, e As EventArgs) Handles txtAmount.EditValueChanged
        ' Skip if empty
        If String.IsNullOrEmpty(txtAmount.Text) Then Return

        ' Store cursor position
        Dim cursorPos = txtAmount.SelectionStart

        ' Remove all non-numeric characters (except one decimal point)
        Dim cleanedText As New System.Text.StringBuilder()
        Dim hasDecimal As Boolean = False

        For Each c As Char In txtAmount.Text
            If Char.IsDigit(c) Then
                cleanedText.Append(c)
            ElseIf c = "."c AndAlso Not hasDecimal Then
                cleanedText.Append(c)
                hasDecimal = True
            End If
        Next

        ' If text was modified (due to invalid pasted chars), update it
        If cleanedText.ToString() <> txtAmount.Text Then
            txtAmount.Text = cleanedText.ToString()
            ' Restore cursor position (adjusting for removed characters)
            txtAmount.SelectionStart = Math.Min(cursorPos, txtAmount.Text.Length)
        End If
    End Sub

    Private Sub txtRepayValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRepayValue.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits (0-9)
        If Char.IsDigit(e.KeyChar) Then
            Return
        End If

        ' Allow only ONE decimal point (for prices)
        If e.KeyChar = "."c AndAlso Not txtRepayValue.Text.Contains(".") Then
            Return
        End If

        ' Block any other character
        e.Handled = True
    End Sub

    Private Sub txtRepayValue_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtRepayValue.PreviewKeyDown
        ' Allow Ctrl+V (paste) - We'll handle validation separately
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Return
        End If

        ' Allow Backspace, Delete, Arrows, Tab
        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
       e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
       e.KeyCode = Keys.Tab Then
            Return
        End If

        ' Allow numbers (0-9)
        If e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9 Then
            Return
        End If

        ' Allow numpad numbers (0-9)
        If e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9 Then
            Return
        End If

        ' Allow ONE decimal point (if needed)
        If e.KeyCode = Keys.Decimal AndAlso Not txtRepayValue.Text.Contains(".") Then
            Return
        End If

        ' Block all other keys
        e.IsInputKey = False
    End Sub

    Private Sub txtRepayValue_EditValueChanged(sender As Object, e As EventArgs) Handles txtRepayValue.EditValueChanged
        ' Skip if empty
        If String.IsNullOrEmpty(txtRepayValue.Text) Then Return

        ' Store cursor position
        Dim cursorPos = txtRepayValue.SelectionStart

        ' Remove all non-numeric characters (except one decimal point)
        Dim cleanedText As New System.Text.StringBuilder()
        Dim hasDecimal As Boolean = False

        For Each c As Char In txtRepayValue.Text
            If Char.IsDigit(c) Then
                cleanedText.Append(c)
            ElseIf c = "."c AndAlso Not hasDecimal Then
                cleanedText.Append(c)
                hasDecimal = True
            End If
        Next

        ' If text was modified (due to invalid pasted chars), update it
        If cleanedText.ToString() <> txtRepayValue.Text Then
            txtRepayValue.Text = cleanedText.ToString()
            ' Restore cursor position (adjusting for removed characters)
            txtRepayValue.SelectionStart = Math.Min(cursorPos, txtRepayValue.Text.Length)
        End If
    End Sub




    Private Sub GridViewContactInfo_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GridViewContactInfo.FocusedRowChanged
        If GridViewContactInfo.RowCount = 0 Then Exit Sub
        Dim row, contactId As Integer
        row = GridViewContactInfo.FocusedRowHandle
        contactId = CInt(GridViewContactInfo.GetRowCellDisplayText(row, colContactID))
        DisplayContactLoans(contactId)
    End Sub


    Private Sub GridViewLoansInfo_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GridViewLoansInfo.FocusedRowChanged
        If GridViewLoansInfo.RowCount = 0 Then Exit Sub
        Dim row, loanId As Integer
        row = GridViewLoansInfo.FocusedRowHandle
        loanId = CInt(GridViewLoansInfo.GetRowCellDisplayText(row, colLoanID))
        DisplayLoanPayments(loanId)
    End Sub

    Private Sub btnEditLoan_Click(sender As Object, e As EventArgs) Handles btnEditLoan.Click
        If GridViewLoansInfo.RowCount = 0 Then Exit Sub
        Dim row, loanId, contactId As Integer
        Dim cName, direction, desc As String
        Dim amount As Decimal
        Dim dt As Date
        row = GridViewLoansInfo.FocusedRowHandle
        loanId = CInt(GridViewLoansInfo.GetRowCellDisplayText(row, colLoanID))
        contactId = CInt(GridViewLoansInfo.GetRowCellDisplayText(row, colContactID2))
        cName = CStr(GridViewLoansInfo.GetRowCellDisplayText(row, colCName2))
        direction = CStr(GridViewLoansInfo.GetRowCellDisplayText(row, colDirection))
        desc = CStr(GridViewLoansInfo.GetRowCellDisplayText(row, colDescription))
        amount = CDec(GridViewLoansInfo.GetRowCellDisplayText(row, colOriginalAmount))
        dt = CDate(GridViewLoansInfo.GetRowCellDisplayText(row, colLoanDate))

        Dim FR As New FrmEditLoan("Edit")
        With FR
            .ContactsCombo1.SetSelectedName(contactId)
            .cboTransType.SelectedItem = direction.Trim()
            .txtAmount.Text = amount
            .txtDescription.Text = desc
            .dtTransDate.EditValue = dt
            .LoanID = loanId
            If .ShowDialog(Me) = DialogResult.OK Then
                DisplayContactLoans(contactId)
            End If
        End With

    End Sub

    Private Sub btnDelLoan_Click(sender As Object, e As EventArgs) Handles btnDelLoan.Click
        If GridViewLoansInfo.RowCount = 0 Then Exit Sub
        Dim row, loanId, contactId As Integer
        Dim cName, direction, desc As String
        Dim amount As Decimal
        Dim dt As Date
        row = GridViewLoansInfo.FocusedRowHandle
        loanId = CInt(GridViewLoansInfo.GetRowCellDisplayText(row, colLoanID))
        contactId = CInt(GridViewLoansInfo.GetRowCellDisplayText(row, colContactID2))
        cName = CStr(GridViewLoansInfo.GetRowCellDisplayText(row, colCName2))
        direction = CStr(GridViewLoansInfo.GetRowCellDisplayText(row, colDirection))
        desc = CStr(GridViewLoansInfo.GetRowCellDisplayText(row, colDescription))
        amount = CDec(GridViewLoansInfo.GetRowCellDisplayText(row, colOriginalAmount))
        dt = CDate(GridViewLoansInfo.GetRowCellDisplayText(row, colLoanDate))

        Dim repo As New LoanDATA()
        Dim repayments = repo.GetRepayments(loanId)
        ' ✅ Check if repayments exist
        If repayments.Count > 0 Then
            MsgBox("This loan has repayments and cannot be deleted.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If


        Dim FR As New FrmEditLoan("Del")
        With FR
            .ContactsCombo1.SetSelectedName(contactId)
            .cboTransType.SelectedItem = direction.Trim()
            .txtAmount.Text = amount
            .txtDescription.Text = desc
            .dtTransDate.EditValue = dt
            .LoanID = loanId
            '
            .ContactsCombo1.Enabled = False
            .cboTransType.Enabled = False
            .txtAmount.Enabled = False
            .txtDescription.Enabled = False
            .dtTransDate.Enabled = False
            If .ShowDialog(Me) = DialogResult.OK Then
                DisplayContactLoans(contactId)
            End If
        End With
    End Sub
End Class