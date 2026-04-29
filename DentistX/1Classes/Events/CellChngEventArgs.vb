Public Class CellChngEventArgs
    Inherits EventArgs


    Private _NewCallval As String


    Public Property NewCellValue() As String
        Get
            Return _NewCallval
        End Get
        Set(ByVal value As String)
            _NewCallval = value
        End Set
    End Property


    Public Sub New(ByVal currentNewCallval As String)

        NewCellValue = currentNewCallval

    End Sub
End Class
