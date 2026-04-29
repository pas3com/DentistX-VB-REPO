Imports DevExpress.Utils.Controls
Imports DevExpress.XtraEditors

Public Class ToothSelectedEvent
    Inherits EventArgs


    Private _svg As SvgImageBox
    'Private col As SvgImageItemCollection = _svg.RootItems
    Private _outTrtList As New List(Of String)
    Private _topTrtList As New List(Of String)
    Private _inTrtList As New List(Of String)
    Private _NewPatID As Integer
    Private _NewPatName As String


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
    Public Property SVG() As SvgImageBox
        Get
            Return _svg
        End Get
        Set(ByVal value As SvgImageBox)
            _svg = value
        End Set
    End Property


    Public Property OutTrtList() As List(Of String)
        Get
            Return _outTrtList
        End Get
        Set(ByVal value As List(Of String))
            _outTrtList = value
        End Set
    End Property

    Public Property TopTrtList() As List(Of String)
        Get
            Return _topTrtList
        End Get
        Set(ByVal value As List(Of String))
            _topTrtList = value
        End Set
    End Property

    Public Property INTrtList() As List(Of String)
        Get
            Return _inTrtList
        End Get
        Set(ByVal value As List(Of String))
            _inTrtList = value
        End Set
    End Property
    Public Sub New(ByVal currentNewPatID As Integer, ByVal currentNewPatName As String, ByVal svgImg As SvgImageBox, ByVal outlist As List(Of String), ByVal toplist As List(Of String), ByVal inlist As List(Of String))
        NewPatID = currentNewPatID
        NewPatName = currentNewPatName
        SVG = svgImg
        OutTrtList = outlist
        TopTrtList = toplist
        INTrtList = inlist
    End Sub
End Class

