Imports System.Globalization
Imports System.Threading

''' <summary>
''' While a module hub (stock, staff, …) is open, force the same UI language and
''' <see cref="Module1.Eng"/> flag as Arabic mode so .resx satellites and If(Eng,…) code match Arabic layout.
''' Reference-counted so nested hubs share one saved <c>Eng</c>/culture restore.
''' </summary>
Friend Module StockUiLanguageScope

    Private _depth As Integer
    Private _savedEng As Boolean
    Private _savedUi As CultureInfo

    Public Sub EnterArabicStockShell()
        If _depth = 0 Then
            _savedEng = Module1.Eng
            _savedUi = Thread.CurrentThread.CurrentUICulture
            Module1.Eng = False
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar")
        End If
        _depth += 1
    End Sub

    Public Sub LeaveArabicStockShell()
        If _depth <= 0 Then Return
        _depth -= 1
        If _depth = 0 Then
            If _savedUi IsNot Nothing Then
                Thread.CurrentThread.CurrentUICulture = _savedUi
            End If
            Module1.Eng = _savedEng
        End If
    End Sub

End Module
