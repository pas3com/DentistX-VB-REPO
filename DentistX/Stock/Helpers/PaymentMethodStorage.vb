''' <summary>Values must match CK_Payments_PaymentMethod on dbo.Payments (Cash, Cheque, BankTransfer, CreditCard).</summary>
Public Module PaymentMethodStorage

    Public Function NormalizeForPaymentsTable(methodText As String) As String
        Dim raw = If(methodText, "").Trim()
        If raw.Length = 0 Then Return raw
        Dim compact = raw.ToUpperInvariant().Replace(" ", String.Empty)
        Select Case compact
            Case "CASH"
                Return "Cash"
            Case "CHEQUE", "CHECK"
                Return "Cheque"
            Case "BANKTRANSFER"
                Return "BankTransfer"
            Case "CREDITCARD"
                Return "CreditCard"
            Case Else
                Exit Select
        End Select
        If raw = "شيك" Then Return "Cheque"
        If raw = "نقدي" OrElse raw = "نقد" Then Return "Cash"
        If raw = "تحويل بنكي" Then Return "BankTransfer"
        If raw = "بطاقة ائتمان" Then Return "CreditCard"
        Return raw
    End Function

End Module
