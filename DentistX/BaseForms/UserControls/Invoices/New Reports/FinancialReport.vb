Imports System.ComponentModel

Public Class FinancialReport

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub XrLabel30_BeforePrint(sender As Object, e As CancelEventArgs) Handles XrLabel30.BeforePrint
        FormatCurrencyControl(sender)
    End Sub

    Private Sub XrLabel33_BeforePrint(sender As Object, e As CancelEventArgs) Handles XrLabel33.BeforePrint
        FormatCurrencyControl(sender)
    End Sub

    Private Sub XrLabel29_BeforePrint(sender As Object, e As CancelEventArgs) Handles XrLabel29.BeforePrint
        FormatCurrencyControl(sender)
    End Sub

    Private Sub XrLabel28_BeforePrint(sender As Object, e As CancelEventArgs) Handles XrLabel28.BeforePrint
        FormatCurrencyControl(sender)
    End Sub

    Private Sub XrLabel27_BeforePrint(sender As Object, e As CancelEventArgs) Handles XrLabel27.BeforePrint
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


End Class