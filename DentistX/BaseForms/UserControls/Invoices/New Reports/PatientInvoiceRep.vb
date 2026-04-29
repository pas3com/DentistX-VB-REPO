Imports System.ComponentModel

Public Class PatientInvoiceRep
    Public Sub New(ByVal invID As Integer, patID As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.parInvoiceID.Value = invID
        Me.parPatientID.Value = patID
    End Sub

    Private Sub XrLabel31_BeforePrint(sender As Object, e As CancelEventArgs) Handles XrLabel31.BeforePrint
        FormatCurrencyControl(sender)
    End Sub

    Private Sub XrLabel32_BeforePrint(sender As Object, e As CancelEventArgs) Handles XrLabel32.BeforePrint
        FormatCurrencyControl(sender)
    End Sub

    Private Sub XrLabel33_BeforePrint(sender As Object, e As CancelEventArgs) Handles XrLabel33.BeforePrint
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
End Class