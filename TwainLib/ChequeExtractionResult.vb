Imports System.Text.RegularExpressions

Public Class ChequeExtractionResult
    Public Property ChequeNumber As String
    Public Property AccountNumber As String
    Public Property IDNumber As String
    Public Property AccountOwnerName As String
    Public Property Address As String
    Public Property PhoneNumber As String
    Public Property BankName As String
    Public Property RawText As String

    Public Property Confidence As Single
    Public Sub New()
        ChequeNumber = "Not Found"
        AccountNumber = "Not Found"
        IDNumber = "Not Found"
        AccountOwnerName = "Not Found"
        Address = "Not Found"
        PhoneNumber = "Not Found"
        BankName = "Not Found"
        RawText = ""
    End Sub
End Class

