''' <summary>Canonical English PayType strings stored in DB; map from localized UI / legacy Arabic values.</summary>
Public Module PayTypeLabels

    Public Const CashEn As String = "Cash"
    Public Const ChequeEn As String = "Cheque"
    Public Const InsuranceEn As String = "Insurance"
    Public Const CreditCardEn As String = "Credit Card"
    Public Const TransferEn As String = "Transfer"
    Public Const OtherEn As String = "Other"

    ''' <summary>Maps pay-type combo item index: 0 = All Payments (not a stored type); 1–5 = Cash … Transfer.</summary>
    Public Function EnglishForComboIndex(selectedIndex As Integer) As String
        Select Case selectedIndex
            Case 1 : Return CashEn
            Case 2 : Return ChequeEn
            Case 3 : Return InsuranceEn
            Case 4 : Return CreditCardEn
            Case 5 : Return TransferEn
            Case Else : Return OtherEn
        End Select
    End Function

    Public Function NormalizeToEnglish(stored As String) As String
        If String.IsNullOrWhiteSpace(stored) Then Return ""
        Dim t = stored.Trim()
        If String.Equals(t, CashEn, StringComparison.OrdinalIgnoreCase) OrElse t = "نقدا" Then Return CashEn
        If String.Equals(t, ChequeEn, StringComparison.OrdinalIgnoreCase) OrElse t = "شيك" Then Return ChequeEn
        If String.Equals(t, InsuranceEn, StringComparison.OrdinalIgnoreCase) OrElse t = "تأمين" Then Return InsuranceEn
        If String.Equals(t, CreditCardEn, StringComparison.OrdinalIgnoreCase) OrElse t = "بطاقة اعتماد" Then Return CreditCardEn
        If String.Equals(t, TransferEn, StringComparison.OrdinalIgnoreCase) OrElse t = "تحويل" Then Return TransferEn
        Return t
    End Function

    Public Function ToArabic(englishNormalized As String) As String
        If String.IsNullOrWhiteSpace(englishNormalized) Then Return ""
        Select Case englishNormalized.Trim()
            Case CashEn : Return "نقدا"
            Case ChequeEn : Return "شيك"
            Case InsuranceEn : Return "تأمين"
            Case CreditCardEn : Return "بطاقة اعتماد"
            Case TransferEn : Return "تحويل"
            Case Else : Return englishNormalized
        End Select
    End Function

End Module
