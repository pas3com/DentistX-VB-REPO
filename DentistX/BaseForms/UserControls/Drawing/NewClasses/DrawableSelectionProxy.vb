Imports System.Drawing
Imports System.Linq

Public Class DrawableSelectionProxy
    Inherits DrawableN

    Private Class ShapeSnapshot
        Public Property Shape As DrawableN
        Public Property Center As PointF
        Public Property Rotation As Single
        Public Property ScaleX As Single
        Public Property ScaleY As Single
    End Class

    Private ReadOnly _snapshots As New List(Of ShapeSnapshot)()
    Private _groupCenter As PointF
    Private _startPoint As PointF

    Public Sub New()
        SelectedDrawables = New List(Of DrawableN)()
    End Sub
    Public Sub New(shapes As IEnumerable(Of DrawableN))
        Me.SelectedDrawables = shapes.ToList()
    End Sub

    Public Property SelectedDrawables As New List(Of DrawableN)

    Public Overrides Function GetBounds() As Rectangle
        Return Rectangle.Round(GetTransformedBounds())
    End Function

    Public Overrides Function GetTransformedBounds() As RectangleF
        If SelectedDrawables Is Nothing OrElse SelectedDrawables.Count = 0 Then Return RectangleF.Empty

        Dim union = SelectedDrawables(0).GetTransformedBounds()
        For i = 1 To SelectedDrawables.Count - 1
            union = RectangleF.Union(union, SelectedDrawables(i).GetTransformedBounds())
        Next
        Return union
    End Function

    'Public Overrides Sub Draw(gr As Graphics)
    '    ' Optional: Draw group box
    '    Using pen As New Pen(Color.Blue, 1) With {.DashStyle = Drawing2D.DashStyle.Dot}
    '        gr.DrawRectangle(pen, Rectangle.Round(GetTransformedBounds()))
    '    End Using

    '    ' Draw anchors
    '    DrawAnchors(gr)
    'End Sub

    Public Overrides Sub Draw(gr As Graphics)
        If SelectedDrawables Is Nothing OrElse SelectedDrawables.Count = 0 Then Return

        Dim unionRect As Rectangle = Rectangle.Round(GetTransformedBounds())

        ' Draw selection box in same visual style as selected shapes.
        Using pen As New Pen(Color.Yellow, 1)
            pen.DashStyle = Drawing2D.DashStyle.Dot
            gr.DrawRectangle(pen, unionRect)
        End Using
        DrawAnchors(gr)
    End Sub

    Public Overrides Sub DrawAnchors(gr As Graphics)
        If SelectedDrawables Is Nothing OrElse SelectedDrawables.Count = 0 Then Return

        For Each anchor In GetAnchors()
            Dim rect = anchor.Value
            If anchor.Key = AnchorEnumN.Rotate Then
                Using brush As New SolidBrush(Color.LightGreen)
                    gr.FillEllipse(brush, rect)
                End Using
                gr.DrawEllipse(Pens.Black, rect)
            Else
                Using brush As New SolidBrush(Color.White)
                    gr.FillRectangle(brush, rect)
                End Using
                gr.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height)
            End If
        Next
    End Sub

    Public Sub ApplyOffset(dx As Single, dy As Single)
        For Each shape In SelectedDrawables
            shape.MoveRelative(CInt(Math.Round(dx)), CInt(Math.Round(dy)))
        Next
    End Sub

    Public Overrides Sub StartTransform(anchor As AnchorEnumN, startPoint As PointF)
        If SelectedDrawables Is Nothing OrElse SelectedDrawables.Count = 0 Then Return

        IsTransforming = True
        CurrentAnchor = anchor
        _startPoint = startPoint
        _groupCenter = GetCenter()
        _snapshots.Clear()

        For Each shape In SelectedDrawables
            Dim b = shape.GetTransformedBounds()
            Dim c As New PointF(b.Left + b.Width / 2.0F, b.Top + b.Height / 2.0F)
            _snapshots.Add(New ShapeSnapshot With {
                .Shape = shape,
                .Center = c,
                .Rotation = shape.RotationAngle,
                .ScaleX = shape.ScaleX,
                .ScaleY = shape.ScaleY
            })
        Next
    End Sub

    Public Overrides Sub UpdateTransform(currentPoint As PointF)
        If Not IsTransforming OrElse _snapshots.Count = 0 Then Return

        Select Case CurrentAnchor
            Case AnchorEnumN.Move
                ApplyMove(currentPoint)
            Case AnchorEnumN.Rotate
                ApplyRotate(currentPoint)
            Case Else
                ApplyResize(currentPoint)
        End Select
    End Sub

    Public Overrides Function GetCenter() As PointF
        Dim b = GetTransformedBounds()
        Return New PointF(b.Left + b.Width / 2.0F, b.Top + b.Height / 2.0F)
    End Function

    Private Sub ApplyMove(currentPoint As PointF)
        Dim dx = currentPoint.X - _startPoint.X
        Dim dy = currentPoint.Y - _startPoint.Y
        For Each snap In _snapshots
            Dim current = GetShapeCenter(snap.Shape)
            Dim target As New PointF(snap.Center.X + dx, snap.Center.Y + dy)
            snap.Shape.MoveRelative(CInt(Math.Round(target.X - current.X)), CInt(Math.Round(target.Y - current.Y)))
        Next
    End Sub

    Private Sub ApplyRotate(currentPoint As PointF)
        Dim startAngle = Math.Atan2(_startPoint.Y - _groupCenter.Y, _startPoint.X - _groupCenter.X)
        Dim currentAngle = Math.Atan2(currentPoint.Y - _groupCenter.Y, currentPoint.X - _groupCenter.X)
        Dim deltaDeg As Single = CSng((currentAngle - startAngle) * (180.0 / Math.PI))

        For Each snap In _snapshots
            Dim targetCenter = RotatePoint(snap.Center, _groupCenter, deltaDeg)
            Dim current = GetShapeCenter(snap.Shape)
            snap.Shape.MoveRelative(CInt(Math.Round(targetCenter.X - current.X)), CInt(Math.Round(targetCenter.Y - current.Y)))
            snap.Shape.RotationAngle = NormalizeAngle(snap.Rotation + deltaDeg)
        Next
    End Sub

    Private Sub ApplyResize(currentPoint As PointF)
        Dim startVec As New PointF(_startPoint.X - _groupCenter.X, _startPoint.Y - _groupCenter.Y)
        Dim currentVec As New PointF(currentPoint.X - _groupCenter.X, currentPoint.Y - _groupCenter.Y)

        Dim scaleXFactor As Single = If(Math.Abs(startVec.X) > 0.1F, currentVec.X / startVec.X, 1.0F)
        Dim scaleYFactor As Single = If(Math.Abs(startVec.Y) > 0.1F, currentVec.Y / startVec.Y, 1.0F)

        If Control.ModifierKeys = Keys.Shift Then
            Dim uniform = (Math.Abs(scaleXFactor) + Math.Abs(scaleYFactor)) / 2.0F
            scaleXFactor = Math.Sign(scaleXFactor) * uniform
            scaleYFactor = Math.Sign(scaleYFactor) * uniform
        End If

        Select Case CurrentAnchor
            Case AnchorEnumN.We, AnchorEnumN.Ew
                scaleYFactor = 1.0F
            Case AnchorEnumN.Ns, AnchorEnumN.Sn
                scaleXFactor = 1.0F
        End Select

        If Math.Abs(scaleXFactor) < 0.1F Then scaleXFactor = Math.Sign(scaleXFactor) * 0.1F
        If Math.Abs(scaleYFactor) < 0.1F Then scaleYFactor = Math.Sign(scaleYFactor) * 0.1F

        For Each snap In _snapshots
            Dim vecX = snap.Center.X - _groupCenter.X
            Dim vecY = snap.Center.Y - _groupCenter.Y
            Dim targetCenter As New PointF(_groupCenter.X + vecX * scaleXFactor, _groupCenter.Y + vecY * scaleYFactor)
            Dim current = GetShapeCenter(snap.Shape)
            snap.Shape.MoveRelative(CInt(Math.Round(targetCenter.X - current.X)), CInt(Math.Round(targetCenter.Y - current.Y)))
            snap.Shape.ScaleX = snap.ScaleX * scaleXFactor
            snap.Shape.ScaleY = snap.ScaleY * scaleYFactor
        Next
    End Sub

    Private Function GetShapeCenter(shape As DrawableN) As PointF
        Dim b = shape.GetTransformedBounds()
        Return New PointF(b.Left + b.Width / 2.0F, b.Top + b.Height / 2.0F)
    End Function

    Private Function RotatePoint(pt As PointF, center As PointF, angleDeg As Single) As PointF
        Dim rad = CSng(angleDeg * Math.PI / 180.0)
        Dim cosA = CSng(Math.Cos(rad))
        Dim sinA = CSng(Math.Sin(rad))
        Dim dx = pt.X - center.X
        Dim dy = pt.Y - center.Y
        Return New PointF(center.X + dx * cosA - dy * sinA, center.Y + dx * sinA + dy * cosA)
    End Function

    Private Function NormalizeAngle(angle As Single) As Single
        Dim normalized = angle Mod 360.0F
        If normalized < 0 Then normalized += 360.0F
        Return normalized
    End Function

    ' === Required Overrides ===

    Public Overrides Function IsAt(x As Integer, y As Integer) As Boolean
        Return GetTransformedBounds().Contains(x, y)
    End Function

    Public Overrides Sub NewPoint(x As Integer, y As Integer)
        ' Not applicable — leave empty
    End Sub

    Public Overrides Function IsEmpty() As Boolean
        Return SelectedDrawables Is Nothing OrElse SelectedDrawables.Count = 0
    End Function

    Public Overrides Sub Render(g As Graphics, dr As DrawableN)
        ' Not applicable — just draw self
        Draw(g)
    End Sub

    Public Overrides Function GetScreenBounds(gr As Graphics) As RectangleF
        ' Return bounds for snapping/mouse interaction
        Return GetTransformedBounds()
    End Function

End Class
