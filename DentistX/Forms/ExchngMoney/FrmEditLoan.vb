Public Class FrmEditLoan


    Public Sub New(ByVal oper As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        cboTransType.Properties.Items.Clear()
        cboTransType.Properties.Items.AddRange(If(Eng, {"Lend", "Borrow"}, {"إقراض", "إقتراض"}))
        If oper = "Edit" Then
            Me.Text = "Edit Loan"
            Me.btnSave.Text = "Save"
        ElseIf oper = "Del" Then
            Me.Text = "Delete Loan"
            Me.btnSave.Text = "Delete"
        End If
    End Sub

    Property LoanID As Integer

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim repo As New LoanDATA()
        Dim repayments = repo.GetRepayments(LoanID)
        Select Case btnSave.Text
            Case "Save"
                Dim clsLoan As New Loan With {
                                             .Amount = Val(txtAmount.Text),
                                             .ContactID = ContactsCombo1.ContactID,
                                             .Description = txtDescription.Text,
                                             .Direction = cboTransType.Text,
                                             .LoanDate = dtTransDate.DateTime,
                                             .LoanID = LoanID}
                If repo.UpdateLoan(clsLoan) Then
                    MsgBox("Loan Updated successfully.")
                    ' Refresh UI
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MsgBox("Loan could not be deleted.")
                End If
            Case "Delete"
                ' Optional: confirm deletion
                If MsgBox("Are you sure you want to delete this loan?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    If repo.DeleteLoan(LoanID) Then
                        MsgBox("Loan deleted successfully.")
                        ' Refresh UI
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                    Else
                        MsgBox("Loan could not be deleted.")
                    End If
                End If
        End Select



        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub FrmEditLoan_Load(sender As Object, e As EventArgs) Handles Me.Load
        'cboTransType.Properties.Items.AddRange(If(Eng, {"Lend", "Borrow"}, {"إقراض", "إقتراض"}))
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class