Public Class BeginOrthTrtEventArgs
    Inherits EventArgs

    Private _NewPatID As Integer
    Private _NewPatName As String
    Private _Fin As Boolean
    Public Property NewPatID() As Integer
        Get
            Return _NewPatID
        End Get
        Set(ByVal value As Integer)
            _NewPatID = value
        End Set
    End Property

    Public Property NewPatName() As String
        Get
            Return _NewPatName
        End Get
        Set(ByVal value As String)
            _NewPatName = value
        End Set
    End Property



    Public Sub New(ByVal currentNewPatID As Integer, ByVal currentNewPatName As String)
        NewPatID = currentNewPatID
        NewPatName = currentNewPatName

    End Sub
End Class
