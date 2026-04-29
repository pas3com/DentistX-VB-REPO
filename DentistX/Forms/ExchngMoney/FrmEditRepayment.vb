Imports System.Globalization
Imports DevExpress.XtraEditors

Public Class FrmEditRepayment

    Public Property RepaymentID As Integer
    Public Property LoanID As Integer

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        ApplyLocalizedUi()
    End Sub

    Private Sub ApplyLocalizedUi()
        Text = If(Eng, "Edit Repayment", "تعديل السداد")
        lblAmount.Text = If(Eng, "Amount:", "المبلغ:")
        lblDate.Text = If(Eng, "Date:", "التاريخ:")
        lblNotes.Text = If(Eng, "Notes:", "ملاحظات:")
        btnSave.Text = If(Eng, "Save", "حفظ")
        btnCancel.Text = If(Eng, "Cancel", "إلغاء")
    End Sub

    Public Sub SetValues(amount As Decimal, repaymentDate As Date, notes As String)
        txtAmount.Text = amount.ToString(CultureInfo.CurrentCulture)
        dtRepaymentDate.DateTime = repaymentDate
        txtNotes.Text = If(notes, String.Empty)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim amt As Decimal
        If Not Decimal.TryParse(txtAmount.Text, NumberStyles.Currency Or NumberStyles.Number,
                               CultureInfo.CurrentCulture, amt) OrElse amt = 0 Then
            XtraMessageBox.Show(Me, If(Eng, "Enter a valid amount.", "أدخل مبلغاً صالحاً."))
            Exit Sub
        End If

        Dim repo As New LoanDATA()
        Dim updated As New Repayment With {
            .RepaymentID = RepaymentID,
            .LoanID = LoanID,
            .Amount = amt,
            .RepaymentDate = dtRepaymentDate.DateTime,
            .Notes = If(txtNotes.Text, String.Empty)
        }
        If repo.UpdateRepayment(updated) Then
            DialogResult = DialogResult.OK
            Close()
        Else
            XtraMessageBox.Show(Me, If(Eng, "Update failed.", "فشل التحديث."))
        End If
    End Sub
End Class
