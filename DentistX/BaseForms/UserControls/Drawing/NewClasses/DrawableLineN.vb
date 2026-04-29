Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Xml.Serialization

<Serializable()>
Public Class DrawableLineN
    Inherits DrawableN

    ' ===== CONSTRUCTORS =====
    Public Sub New()
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


    ' ===== OVERRIDDEN METHODS =====
    Public Overrides Sub Draw(gr As Graphics)

        ' Save graphics state
        Dim container As GraphicsContainer = gr.BeginContainer()

        Try
            ApplyTransformations(gr)
            Dim rect = GetBounds()
            'ADD BOUNDS VALIDATION
            If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

            ' Fill Line

            If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

            ' Set high-quality rendering
            gr.SmoothingMode = SmoothingMode.AntiAlias
            gr.PixelOffsetMode = PixelOffsetMode.HighQuality
            gr.CompositingQuality = CompositingQuality.HighQuality

            Using pen As New Pen(ForeColor, LineWidth)
                gr.DrawLine(pen, X1, Y1, X2, Y2)
            End Using
        Finally
            ' Restore graphics state
            gr.EndContainer(container)
        End Try

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
        Dim x = Math.Min(X1, X2)
        Dim y = Math.Min(Y1, Y2)
        Dim w = Math.Abs(X2 - X1)
        Dim h = Math.Abs(Y2 - Y1)
        Return New Rectangle(x, y, w, h)
    End Function

    Public Overrides Function IsAt(x As Integer, y As Integer) As Boolean
        ' Hit test: check if point (x,y) is close to line within a tolerance

        Const tolerance As Integer = 5

        ' Calculate distance from point to line segment
        Dim dx = X2 - X1
        Dim dy = Y2 - Y1

        If dx = 0 AndAlso dy = 0 Then
            ' Line is a point
            Return Math.Abs(x - X1) <= tolerance AndAlso Math.Abs(y - Y1) <= tolerance
        End If

        Dim lengthSquared = dx * dx + dy * dy

        ' Project point onto line segment, finding parameter t of closest point
        Dim t = ((x - X1) * dx + (y - Y1) * dy) / lengthSquared
        t = Math.Max(0, Math.Min(1, t))

        ' Closest point on the line segment
        Dim closestX = X1 + t * dx
        Dim closestY = Y1 + t * dy

        ' Distance from (x,y) to closest point
        Dim dist = Math.Sqrt((x - closestX) * (x - closestX) + (y - closestY) * (y - closestY))

        Return dist <= tolerance
    End Function

    Public Overrides Sub NewPoint(x As Integer, y As Integer)
        X2 = x
        Y2 = y
    End Sub

    Public Overrides Function IsEmpty() As Boolean
        Return (X1 = X2) AndAlso (Y1 = Y2)
    End Function

    Public Overrides Sub MoveRelative(dx As Integer, dy As Integer)
        X1 += dx
        Y1 += dy
        X2 += dx
        Y2 += dy
        Dim center As New PointF(GetBounds.Left + GetBounds.Width / 2.0F, GetBounds.Top + GetBounds.Height / 2.0F)
        RotationCenter = center
    End Sub

    Public Overrides Sub UpdateTransform(currentPoint As PointF)
        Dim center = TransformCenter
        Dim startVec As New PointF(
            TransformStartPoint.X - center.X,
            TransformStartPoint.Y - center.Y
        )
        Dim currentVec As New PointF(
            currentPoint.X - center.X,
            currentPoint.Y - center.Y
        )

        ' Calculate scaling factors
        Dim scaleXFactor = If(Math.Abs(startVec.X) > 0.1, currentVec.X / startVec.X, 1)
        Dim scaleYFactor = If(Math.Abs(startVec.Y) > 0.1, currentVec.Y / startVec.Y, 1)
        Select Case CurrentAnchor
            Case AnchorEnumN.Move
                Dim dx = CInt(currentPoint.X - TransformStartPoint.X)
                Dim dy = CInt(currentPoint.Y - TransformStartPoint.Y)
                MoveRelative(dx, dy)
                TransformStartPoint = currentPoint

            Case AnchorEnumN.Rotate
                'Dim center = TransformCenter
                Dim v1 = New PointF(TransformStartPoint.X - center.X, TransformStartPoint.Y - center.Y)
                Dim v2 = New PointF(currentPoint.X - center.X, currentPoint.Y - center.Y)
                Dim angle1 = Math.Atan2(v1.Y, v1.X) * 180 / Math.PI
                Dim angle2 = Math.Atan2(v2.Y, v2.X) * 180 / Math.PI
                RotationAngle = (RotationAngle + CSng(angle2 - angle1)) Mod 360
                TransformStartPoint = currentPoint
            Case AnchorEnumN.Ew, AnchorEnumN.We
                scaleYFactor = 1.0F ' Lock vertical
                ' Apply scaling using base class properties
                ScaleX = OriginalScaleX * scaleXFactor
                ScaleY = OriginalScaleY * scaleYFactor
            Case AnchorEnumN.Ns, AnchorEnumN.Sn
                scaleXFactor = 1.0F ' Lock horizontal
                ' Apply scaling using base class properties
                ScaleX = OriginalScaleX * scaleXFactor
                ScaleY = OriginalScaleY * scaleYFactor
            Case AnchorEnumN.Nwse, AnchorEnumN.Nesw, AnchorEnumN.Senw, AnchorEnumN.Swne
                ' Apply scaling using base class properties
                ScaleX = OriginalScaleX * scaleXFactor
                ScaleY = OriginalScaleY * scaleYFactor
        End Select
    End Sub

    Public Overrides Function GetScreenBounds(gr As Graphics) As RectangleF
        Dim path As New GraphicsPath()
        path.AddRectangle(Me.GetBounds()) ' Or custom logic if you override it
        Dim matrix = gr.Transform.Clone()
        path.Transform(matrix)
        Return path.GetBounds()
    End Function

End Class
