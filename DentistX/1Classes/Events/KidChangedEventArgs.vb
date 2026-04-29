Public Class KidAgeChangedEventArgs
    Inherits EventArgs

    Private _NewPatID As Integer
    Private _NewPatName As String
    Private _Fin As Boolean
    Private _Kid As Boolean

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

    Public Property FinLoad() As Boolean
        Get
            Return _Fin
        End Get
        Set(ByVal value As Boolean)
            _Fin = value
        End Set
    End Property




    Public Property IsKid() As Boolean
        Get
            Return _Kid
        End Get
        Set(ByVal value As Boolean)
            _Kid = value
        End Set
    End Property
    Public Sub New(ByVal currentNewPatID As Integer, ByVal currentNewPatName As String, ByVal Finish As Boolean, ByVal kid As Boolean)
        NewPatID = currentNewPatID
        NewPatName = currentNewPatName
        FinLoad = Finish
        IsKid = kid
    End Sub
End Class

