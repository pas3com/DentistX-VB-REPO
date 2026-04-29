Imports System.Runtime.CompilerServices

Public Module CurrencyExtensions
    <Extension()>
    Public Function ToCurrencyString(value As Decimal, currencyType As CurrencyType) As String
        Return FormatCustomCurrency(value, currencyType)
    End Function

    <Extension()>
    Public Function ToCurrencyString(value As Decimal, currencyType As CurrencyType,
                                     includeName As Boolean) As String
        Return FormatCustomCurrency(value, currencyType, includeName)
    End Function

    Public Function FormatCustomCurrency1(value As Decimal, symbol As String) As String
        FormatCustomCurrency1 = symbol & " " & value.ToString("###,##0.00")
    End Function

    Public Function FormatCustomCurrency(value As Decimal, currencyType As CurrencyType) As String
        Dim info = CurrencyManager.GetCurrencyInfo(currencyType)

        ' Format the number part
        Dim formattedValue = value.ToString(info.Format)

        ' Determine position based on language
        Select Case currencyType
            Case CurrencyType.USD_En, CurrencyType.EUR_En, CurrencyType.JOD_En,
             CurrencyType.ILS_En, CurrencyType.EGP_En, CurrencyType.SAR_En,
             CurrencyType.QAR_En, CurrencyType.AED_En
                ' English: Symbol + Space + Value
                Return info.Symbol & " " & formattedValue

            Case CurrencyType.USD_Ar, CurrencyType.EUR_Ar, CurrencyType.JOD_Ar,
             CurrencyType.ILS_Ar, CurrencyType.EGP_Ar, CurrencyType.SAR_Ar,
             CurrencyType.QAR_Ar, CurrencyType.AED_Ar
                ' Arabic: Value + Space + Symbol (right-to-left)
                Return formattedValue & " " & info.Symbol
        End Select

        Return formattedValue ' Fallback
    End Function

    ' Optional: Overloaded version with currency name
    Public Function FormatCustomCurrency(value As Decimal, currencyType As CurrencyType,
                                     Optional includeName As Boolean = False) As String
        Dim info = CurrencyManager.GetCurrencyInfo(currencyType)
        Dim formattedValue = value.ToString(info.Format)
        Dim strValue As String = ""
        If includeName Then
            Select Case currencyType
                Case CurrencyType.USD_En, CurrencyType.EUR_En, CurrencyType.JOD_En,
                 CurrencyType.ILS_En, CurrencyType.EGP_En, CurrencyType.SAR_En,
                 CurrencyType.QAR_En, CurrencyType.AED_En
                    strValue = info.Symbol & " " & formattedValue & " (" & info.EnglishAbbreviation & ")"

                Case CurrencyType.USD_Ar, CurrencyType.EUR_Ar, CurrencyType.JOD_Ar,
                 CurrencyType.ILS_Ar, CurrencyType.EGP_Ar, CurrencyType.SAR_Ar,
                 CurrencyType.QAR_Ar, CurrencyType.AED_Ar
                    strValue = formattedValue & " " & info.Symbol & " (" & info.ArabicAbbreviation & ")"
            End Select
        Else
            strValue = FormatCustomCurrency(currencyType, value)
        End If
        Return strValue
    End Function


    Public Enum CurrencyType
        USD_En = 1
        USD_Ar = 2
        EUR_En = 3
        EUR_Ar = 4
        JOD_En = 5
        JOD_Ar = 6
        ILS_En = 7
        ILS_Ar = 8
        EGP_En = 9
        EGP_Ar = 10
        SAR_En = 11
        SAR_Ar = 12
        QAR_En = 13
        QAR_Ar = 14
        AED_En = 15
        AED_Ar = 16
    End Enum

    Public Class CurrencyInfo
        Public Property Symbol As String
        Public Property EnglishName As String
        Public Property ArabicName As String
        Public Property EnglishAbbreviation As String
        Public Property ArabicAbbreviation As String
        Public Property Format As String

        Public Sub New(symbol As String, enName As String, arName As String,
                   enAbbr As String, arAbbr As String, Optional format As String = "###,##0.00")
            Me.Symbol = symbol
            Me.EnglishName = enName
            Me.ArabicName = arName
            Me.EnglishAbbreviation = enAbbr
            Me.ArabicAbbreviation = arAbbr
            Me.Format = format
        End Sub
    End Class



    Public Class CurrencyManager
        Private Shared ReadOnly _currencies As Dictionary(Of CurrencyType, CurrencyInfo)

        Shared Sub New()
            _currencies = New Dictionary(Of CurrencyType, CurrencyInfo)()
            InitializeCurrencies()
        End Sub

        Private Shared Sub InitializeCurrencies()
            ' US Dollar
            _currencies.Add(CurrencyType.USD_En, New CurrencyInfo("$", "US Dollar", "دولار أمريكي", "USD", "دولار"))
            _currencies.Add(CurrencyType.USD_Ar, New CurrencyInfo("$", "US Dollar", "دولار أمريكي", "USD", "دولار"))

            ' Euro
            _currencies.Add(CurrencyType.EUR_En, New CurrencyInfo("€", "Euro", "يورو", "EUR", "يورو"))
            _currencies.Add(CurrencyType.EUR_Ar, New CurrencyInfo("€", "Euro", "يورو", "EUR", "يورو"))

            ' Jordanian Dinar
            _currencies.Add(CurrencyType.JOD_En, New CurrencyInfo("JD", "Jordanian Dinar", "دينار أردني", "JOD", "د.أ"))
            _currencies.Add(CurrencyType.JOD_Ar, New CurrencyInfo("د.أ", "Jordanian Dinar", "دينار أردني", "JOD", "د.أ"))

            ' Israeli Shekel
            _currencies.Add(CurrencyType.ILS_En, New CurrencyInfo("₪", "Israeli Shekel", "شيقل إسرائيلي", "ILS", "شيقل"))
            _currencies.Add(CurrencyType.ILS_Ar, New CurrencyInfo("₪", "Israeli Shekel", "شيقل إسرائيلي", "ILS", "شيقل"))

            ' Egyptian Pound
            _currencies.Add(CurrencyType.EGP_En, New CurrencyInfo("E£", "Egyptian Pound", "جنيه مصري", "EGP", "ج.م"))
            _currencies.Add(CurrencyType.EGP_Ar, New CurrencyInfo("ج.م", "Egyptian Pound", "جنيه مصري", "EGP", "ج.م"))

            ' Saudi Riyal
            _currencies.Add(CurrencyType.SAR_En, New CurrencyInfo("SR", "Saudi Riyal", "ريال سعودي", "SAR", "ر.س"))
            _currencies.Add(CurrencyType.SAR_Ar, New CurrencyInfo("ر.س", "Saudi Riyal", "ريال سعودي", "SAR", "ر.س"))

            ' Qatari Riyal
            _currencies.Add(CurrencyType.QAR_En, New CurrencyInfo("QR", "Qatari Riyal", "ريال قطري", "QAR", "ر.ق"))
            _currencies.Add(CurrencyType.QAR_Ar, New CurrencyInfo("ر.ق", "Qatari Riyal", "ريال قطري", "QAR", "ر.ق"))

            ' UAE Dirham
            _currencies.Add(CurrencyType.AED_En, New CurrencyInfo("AED", "UAE Dirham", "درهم إماراتي", "AED", "د.إ"))
            _currencies.Add(CurrencyType.AED_Ar, New CurrencyInfo("د.إ", "UAE Dirham", "درهم إماراتي", "AED", "د.إ"))
        End Sub

        Public Shared Function GetCurrencyInfo(currencyType As CurrencyType) As CurrencyInfo
            Return _currencies(currencyType)
        End Function
    End Class



End Module

'' Usage with extension method:
'lblSubTotal.Text = amount.ToCurrencyString(CurrencyType.JOD_Ar)
'lblSubTotal.Text = amount.ToCurrencyString(CurrencyType.JOD_En, True)