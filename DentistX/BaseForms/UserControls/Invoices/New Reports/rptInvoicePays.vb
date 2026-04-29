Imports System.ComponentModel

Public Class rptInvoicePays

    Private Sub XrLabel28_BeforePrint(sender As Object, e As CancelEventArgs) Handles XrLabel28.BeforePrint
        FormatCurrencyControl(sender)
    End Sub

    Private Sub total2_BeforePrint(sender As Object, e As CancelEventArgs) Handles total2.BeforePrint
        FormatCurrencyControl(sender)
    End Sub

    Private Sub FormatCurrencyControl(sender As Object)
        Dim label = TryCast(sender, DevExpress.XtraReports.UI.XRLabel)
        If label IsNot Nothing Then
            label.Text = FormatCurrencyValue(label.Text)
            Return
        End If

        Dim cell = TryCast(sender, DevExpress.XtraReports.UI.XRTableCell)
        If cell IsNot Nothing Then
            cell.Text = FormatCurrencyValue(cell.Text)
        End If
    End Sub

    Private Function FormatCurrencyValue(value As Object) As String
        Dim amount As Decimal = ToDecimal(value)
        Return amount.ToCurrencyString(CurrencyType.ILS_Ar)
    End Function

    Private Function ToDecimal(value As Object) As Decimal
        If value Is Nothing OrElse IsDBNull(value) Then
            Return 0D
        End If

        Dim result As Decimal
        If Decimal.TryParse(value.ToString(), result) Then
            Return result
        End If

        Return 0D
    End Function

    Private Sub XrLabel19_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrLabel19.BeforePrint

    End Sub
End Class