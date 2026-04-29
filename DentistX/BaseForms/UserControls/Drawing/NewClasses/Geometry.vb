Imports System.Math
Imports System.Drawing.Drawing2D

Module Geometry
    ' Return True if (x1, y1) is within close_distance 
    ' vertically and horizontally of (x2, y2).
    Public Function PointNearPoint(ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer, Optional ByVal close_distance As Integer = 2) As Boolean
        Return (Abs(x2 - x1) <= close_distance) AndAlso _
               (Abs(y2 - y1) <= close_distance)
    End Function

    ' Return True if (px, py) is within close_distance 
    ' if the segment from (x1, y1) to (X2, y2).
    Public Function PointNearSegment(ByVal px As Integer, ByVal py As Integer, ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer, Optional ByVal close_distance As Integer = 2) As Boolean
        Return DistToSegment(px, py, x1, y1, x2, y2) <= close_distance
    End Function

    ' Corrected DistToSegment function
    Private Function DistToSegment(ByVal px As Integer, ByVal py As Integer,
                              ByVal x1 As Integer, ByVal y1 As Integer,
                              ByVal x2 As Integer, ByVal y2 As Integer) As Double
        Dim dx As Double = x2 - x1
        Dim dy As Double = y2 - y1

        If dx = 0 AndAlso dy = 0 Then
            ' It's a point, not a line segment
            Return Math.Sqrt((px - x1) ^ 2 + (py - y1) ^ 2)
        End If

        Dim t As Double = ((px - x1) * dx + (py - y1) * dy) / (dx * dx + dy * dy)
        t = Math.Max(0, Math.Min(1, t)) ' Clamp to [0,1]

        Dim closestX As Double = x1 + t * dx
        Dim closestY As Double = y1 + t * dy

        Return Math.Sqrt((px - closestX) ^ 2 + (py - closestY) ^ 2)
    End Function

    ' Calculate the distance between the point and the segment.
    Private Function DistToSegmentOld(ByVal px As Integer, ByVal py As Integer,
                                      ByVal x1 As Integer, ByVal y1 As Integer,
                                      ByVal x2 As Integer, ByVal y2 As Integer) As Double
        Dim dx As Double
        Dim dy As Double
        Dim t As Double

        dx = x2 - x1
        dy = y2 - y1
        If dx = 0 And dy = 0 Then
            ' It's a point not a line segment.(px, py, x1, y1, x2, y2)
            dx = px - x1
            dy = py - y1
            DistToSegmentOld = Sqrt(dx * dx + dy * dy)
            Exit Function
        End If

        t = (px + py - x1 - y1) / (dx + dy)

        If t < 0 Then
            dx = px - x1
            dy = py - y1
        ElseIf t > 1 Then
            dx = px - x2
            dy = py - y2
        Else
            Dim x3 As Double = x1 + t * dx
            Dim y3 As Double = y1 + t * dy
            dx = px - x3
            dy = py - y3
        End If
        DistToSegmentOld = Sqrt(dx * dx + dy * dy)
    End Function

    ' Return True if the point is inside the ellipse
    ' (expanded by distance close_distance vertically
    ' and horizontally).
    Public Function PointNearEllipse(ByVal px As Integer, ByVal py As Integer, ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer, Optional ByVal close_distance As Integer = 0) As Boolean
        Dim a As Double = Abs(x2 - x1) / 2 + close_distance
        Dim b As Double = Abs(y2 - y1) / 2 + close_distance
        px -= (x2 + x1) \ 2
        py -= (y2 + y1) \ 2
        Return ((px * px) / (a * a) + (py * py) / (b * b) <= 1)
    End Function

    ' Return True if the point is within the polygon.
    Public Function PointNearPolygon(ByVal x As Integer, ByVal y As Integer, ByVal pgon_pts() As PointF) As Boolean
        ' Make a region for the polygon.
        Dim pgon_path As New GraphicsPath(FillMode.Alternate)
        pgon_path.AddPolygon(pgon_pts)
        Dim pgon_region As New Region(pgon_path)

        ' See if the point is in the region.
        Return pgon_region.IsVisible(x, y)
    End Function

    ' Calculate distance between two points
    Public Function Distance(p1 As Point, p2 As Point) As Double
        Return Math.Sqrt((p2.X - p1.X) ^ 2 + (p2.Y - p1.Y) ^ 2)
    End Function

    Public Function Distance(p1 As Point, p2 As PointF) As Double
        Return Math.Sqrt((p2.X - p1.X) ^ 2 + (p2.Y - p1.Y) ^ 2)
    End Function

    Public Function Distance(p1 As PointF, p2 As PointF) As Double
        Return Math.Sqrt((p2.X - p1.X) ^ 2 + (p2.Y - p1.Y) ^ 2)
    End Function

    ' Calculate angle between two points
    Public Function AngleBetween(p1 As Point, p2 As Point) As Double
        Return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * (180 / Math.PI)
    End Function

    ' Draw anchor line during transformations
    Public Sub DrawAnchorLine(gr As Graphics, startPoint As PointF, endPoint As PointF)
        Using anchorPen As New Pen(Color.Blue, 1)
            anchorPen.DashStyle = Drawing2D.DashStyle.Dot
            gr.DrawLine(anchorPen, startPoint, endPoint)
        End Using
    End Sub

    ' Draw rotation guide
    Public Sub DrawRotationGuide(gr As Graphics, center As PointF, radius As Single,
                                startAngle As Single, endAngle As Single)
        Using guidePen As New Pen(Color.Green, 1)
            guidePen.DashStyle = Drawing2D.DashStyle.Dash
            gr.DrawArc(guidePen, center.X - radius, center.Y - radius,
                       radius * 2, radius * 2, startAngle, endAngle - startAngle)
        End Using
    End Sub

End Module
