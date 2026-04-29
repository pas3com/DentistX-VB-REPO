Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Xml.Serialization

<Serializable()>
Public Class DrawablePolylineN
    Inherits DrawableN

    <XmlIgnore>
    Public Property Points As New List(Of PointF)

    ' Serialization helper for XML
    <XmlArray("Points")>
    <XmlArrayItem("Point")>
    Public Property PointsSerialized() As PointF()
        Get
            Return Points.ToArray()
        End Get
        Set(value As PointF())
            Points = New List(Of PointF)(value)
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal fore_color As Color, Optional ByVal line_width As Integer = 0, Optional ByVal startX As Integer = 0, Optional ByVal startY As Integer = 0)
        MyBase.New()
        Points.Add(New PointF(startX, startY))
    End Sub

    Public Sub New(ByVal fore_color As Color, Optional ByVal line_width As Integer = 0,
                  Optional ByVal new_x1 As Integer = 0, Optional ByVal new_y1 As Integer = 0,
                  Optional ByVal new_x2 As Integer = 1, Optional ByVal new_y2 As Integer = 1)
        MyBase.New(fore_color, Nothing, line_width)
        X1 = new_x1
        Y1 = new_y1
        X2 = new_x2
        Y2 = new_y2
    End Sub

    Public Overrides Sub Draw(gr As Graphics)
        ' Save graphics state
        Dim container As GraphicsContainer = gr.BeginContainer()

        Try
            ApplyTransformations(gr)
            Dim rect = GetBounds()
            'ADD BOUNDS VALIDATION
            If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

            ' Fill PolyLine
            If Points.Count < 2 Then Return

            gr.SmoothingMode = SmoothingMode.AntiAlias
            Using pen As New Pen(ForeColor, LineWidth)
                gr.DrawLines(pen, Points.ToArray())
            End Using
        Finally
            ' Restore graphics state
            gr.EndContainer(container)
        End Try

        ' Draw anchors on top (untransformed)
        If IsSelected Then
            Dim rectF As RectangleF = GetTransformedBounds()
            Dim rect As New Rectangle(CInt(rectF.X), CInt(rectF.Y), CInt(rectF.Width), CInt(rectF.Height))
            Using selPen As New Pen(Color.Yellow, 1)
                selPen.DashStyle = DashStyle.Dot
                gr.DrawRectangle(selPen, rect)
            End Using
        End If
    End Sub
    ' Render method added — just calls Draw
    Public Overrides Sub Render(gr As Graphics, dr As DrawableN)
        'Draw(gr)
    End Sub
    Public Overrides Function GetBounds() As Rectangle
        If Points.Count = 0 Then Return Rectangle.Empty

        Dim minX = Points.Min(Function(p) p.X)
        Dim minY = Points.Min(Function(p) p.Y)
        Dim maxX = Points.Max(Function(p) p.X)
        Dim maxY = Points.Max(Function(p) p.Y)

        Return Rectangle.Round(New RectangleF(minX, minY, maxX - minX, maxY - minY))
    End Function

    Public Overrides Function IsAt(x As Integer, y As Integer) As Boolean
        Const tolerance As Integer = 5
        Dim pt = New PointF(x, y)

        For i = 0 To Points.Count - 2
            Dim p1 = Points(i)
            Dim p2 = Points(i + 1)

            If DistancePointToSegment(pt, p1, p2) <= tolerance Then
                Return True
            End If
        Next

        Return False
    End Function

    Private Function DistancePointToSegment(p As PointF, p1 As PointF, p2 As PointF) As Single
        Dim dx = p2.X - p1.X
        Dim dy = p2.Y - p1.Y

        If dx = 0 AndAlso dy = 0 Then
            Return Distance(p, p1)
        End If

        Dim t = ((p.X - p1.X) * dx + (p.Y - p1.Y) * dy) / (dx * dx + dy * dy)
        t = Math.Max(0, Math.Min(1, t))

        Dim projX = p1.X + t * dx
        Dim projY = p1.Y + t * dy

        Return Distance(p, New PointF(projX, projY))
    End Function

    Private Function Distance(p1 As PointF, p2 As PointF) As Single
        Dim dx = p1.X - p2.X
        Dim dy = p1.Y - p2.Y
        Return Math.Sqrt(dx * dx + dy * dy)
    End Function

    Public Overrides Sub NewPoint(x As Integer, y As Integer)
        Points.Add(New PointF(x, y))
    End Sub

    Public Overrides Function IsEmpty() As Boolean
        Return Points.Count < 2
    End Function

    Public Overrides Sub MoveRelative(dx As Integer, dy As Integer)
        For i = 0 To Points.Count - 1
            Points(i) = New PointF(Points(i).X + dx, Points(i).Y + dy)
        Next
    End Sub

    Public Overrides Function GetScreenBounds(gr As Graphics) As RectangleF
        Dim path As New GraphicsPath()
        path.AddRectangle(Me.GetBounds()) ' Or custom logic if you override it
        Dim matrix = gr.Transform.Clone()
        path.Transform(matrix)
        Return path.GetBounds()
    End Function

End Class
