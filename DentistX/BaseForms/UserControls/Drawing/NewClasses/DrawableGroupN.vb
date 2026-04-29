Imports System.Linq
Imports System.Xml.Serialization

<Serializable()>
Public Class DrawableGroupN
    Inherits DrawableN

    <XmlElement(GetType(DrawableN)),
     XmlElement(GetType(DrawableArrowN)),
     XmlElement(GetType(DrawableEllipseN)),
     XmlElement(GetType(DrawableFreehandN)),
     XmlElement(GetType(DrawableGroupN)),
     XmlElement(GetType(DrawableImageN)),
     XmlElement(GetType(DrawableLineN)),
     XmlElement(GetType(DrawablePolylineN)),
     XmlElement(GetType(DrawableRectangleN)),
     XmlElement(GetType(DrawableStarN)),
     XmlElement(GetType(DrawableTextN))>
    Public Property Children As New List(Of DrawableN)

    Public Overrides Sub Draw(gr As Graphics)
        For Each shape In Children
            shape.Draw(gr)
        Next
    End Sub

    Public Overrides Sub NewPoint(x As Integer, y As Integer)
        For Each shape In Children
            shape.NewPoint(x, y)
        Next
    End Sub

    Public Overrides Function GetBounds() As Rectangle
        If Children.Count = 0 Then Return Rectangle.Empty
        Return Children.Select(Function(s) s.GetBounds()).Aggregate(Function(a, b) Rectangle.Union(a, b))
    End Function

    Public Overrides Function IsAt(x As Integer, y As Integer) As Boolean
        Return Children.Any(Function(s) s.IsAt(x, y))
    End Function

    Public Overrides Function IsEmpty() As Boolean
        Return Children.Count = 0 OrElse Children.All(Function(s) s.IsEmpty())
    End Function

    Public Overrides Sub Render(g As Graphics, dr As DrawableN)
        For Each shape In Children
            shape.Render(g, shape)
        Next
    End Sub

    Public Overrides Function GetScreenBounds(gr As Graphics) As RectangleF
        If Children.Count = 0 Then Return RectangleF.Empty
        Return Children.Select(Function(s) s.GetScreenBounds(gr)).Aggregate(Function(a, b) RectangleF.Union(a, b))
    End Function
End Class

'<Serializable()>
'Public Class DrawableGroupN
'    Inherits DrawableN

'    Public Property Children As New List(Of DrawableN)

'    Public Overrides Sub Draw(gr As Graphics)
'        For Each shape In Children
'            shape.Draw(gr)
'        Next
'    End Sub

'    Public Overrides Sub NewPoint(x As Integer, y As Integer)
'        ' Move all children
'        For Each s In Children
'            s.NewPoint(x, y)
'        Next
'    End Sub
'End Class
