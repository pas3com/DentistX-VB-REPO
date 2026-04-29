Public Class GridChangedEvent
    Inherits EventArgs

    Private _NewPatID As Integer
    Private _NewPatName As String
    Private _Grid As Boolean
    Private _Full As Boolean
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

    Public Property IsFull() As Boolean
        Get
            Return _Full
        End Get
        Set(ByVal value As Boolean)
            _Full = value
        End Set
    End Property

    Public Property IsGrid() As Boolean
        Get
            Return _Grid
        End Get
        Set(ByVal value As Boolean)
            _Grid = value
        End Set
    End Property


    Public Sub New(ByVal currentNewPatID As Integer, ByVal currentNewPatName As String, ByVal full As Boolean, ByVal grid As Boolean)
        NewPatID = currentNewPatID
        NewPatName = currentNewPatName
        IsFull = full
        IsGrid = grid
    End Sub
End Class
