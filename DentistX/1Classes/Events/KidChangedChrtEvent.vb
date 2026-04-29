Public Class KidChartChangedEvent
    Inherits EventArgs

    Private _NewPatID As Integer
    Private _Kid As Boolean
    Private _Tbl As DataTable
    Private _PassedTrt As String

    Public Property NewPatID() As Integer
        Get
            Return _NewPatID
        End Get
        Set(ByVal value As Integer)
            _NewPatID = value
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


    Public Property PassedTrt() As String
        Get
            Return _PassedTrt
        End Get
        Set(ByVal value As String)
            _PassedTrt = value
        End Set
    End Property


    Public Property HasTbl() As DataTable
        Get
            Return _Tbl
        End Get
        Set(ByVal value As DataTable)
            _Tbl = value
        End Set
    End Property
    Public Sub New(ByVal currentNewPatID As Integer, ByVal passed_Trt As String, ByVal Tbl As DataTable, ByVal kid As Boolean)
        NewPatID = currentNewPatID
        PassedTrt = passed_Trt
        HasTbl = Tbl
        IsKid = kid
    End Sub
End Class


