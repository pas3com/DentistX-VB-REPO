Public Class ChequeOCRResult
    Public Property RawText As String
    Public Property ChequeNumber As String
    Public Property Confidence As Single
    Public Property ErrorMessage As String

    Public ReadOnly Property IsSuccessful As Boolean
        Get
            Return String.IsNullOrEmpty(ErrorMessage) AndAlso Confidence > 0.3
        End Get
    End Property

    Public Sub New()
        RawText = ""
        ChequeNumber = "Not Found"
        Confidence = 0
        ErrorMessage = ""
    End Sub
End Class