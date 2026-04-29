Public Class ChequeInfo
    Public Property FilePath As String
    Public Property FileName As String
    Public Property ChequeNumber As String
    Public Property Amount As String
    Public Property DateIssued As String
    Public Property Payee As String
    Public Property ScanDate As DateTime
    Public Property Regions As Dictionary(Of String, Rectangle)
    Public Property IsProcessed As Boolean = False
    ' Add the missing property
    Public Property OCRConfidence As Single

    Public Sub New()
        Regions = New Dictionary(Of String, Rectangle)
        OCRConfidence = 0
    End Sub
End Class