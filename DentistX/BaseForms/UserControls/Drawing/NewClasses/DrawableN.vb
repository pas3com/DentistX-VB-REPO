Imports System.Xml.Serialization
Imports System.Drawing
Imports System.Drawing.Drawing2D

' In DrawableN base class file (add at class level)
<XmlInclude(GetType(DrawableArrowN))>
<XmlInclude(GetType(DrawableEllipseN))>
<XmlInclude(GetType(DrawableFreehandN))>
<XmlInclude(GetType(DrawableGroupN))>
<XmlInclude(GetType(DrawableImageN))>
<XmlInclude(GetType(DrawableLineN))>
<XmlInclude(GetType(DrawablePolylineN))>
<XmlInclude(GetType(DrawableRectangleN))>
<XmlInclude(GetType(DrawableStarN))>
<XmlInclude(GetType(DrawableTextN))>
<Serializable()>
Public MustInherit Class DrawableN

#Region "Visual Appearance"

    <XmlIgnore> Private _lineColor As Color = Color.Black
    <XmlIgnore> Public Property ForeColor As Color = Color.Black
    <XmlIgnore> Public Property FillColor As Color = Color.White
    <XmlAttribute> Public Property LineWidth As Integer = 1

    '    Private _lineColor As Color = Color.Black
    ' Fixed LineColor with surrogate
    <XmlIgnore> Public Property LineColor As Color
        Get
            Return _lineColor
        End Get
        Set(value As Color)
            _lineColor = value
        End Set
    End Property

    <XmlAttribute("LineColor")>
    Public Property LineColorArgb As Integer
        Get
            Return LineColor.ToArgb()
        End Get
        Set(value As Integer)
            LineColor = Color.FromArgb(value)
        End Set
    End Property

    ' Keep existing ForeColor/FillColor surrogates
    <XmlAttribute("ForeColor")>
    Public Property ForeColorArgb As Integer
        Get
            Return ForeColor.ToArgb()
        End Get
        Set(value As Integer)
            ForeColor = Color.FromArgb(value)
        End Set
    End Property

    <XmlAttribute("FillColor")>
    Public Property FillColorArgb As Integer
        Get
            Return FillColor.ToArgb()
        End Get
        Set(value As Integer)
            FillColor = Color.FromArgb(value)
        End Set
    End Property
#End Region


#Region "Position"
    <XmlAttribute> Public Property X1 As Integer
    <XmlAttribute> Public Property Y1 As Integer
    <XmlAttribute> Public Property X2 As Integer
    <XmlAttribute> Public Property Y2 As Integer
#End Region

#Region "Transform"
    <XmlIgnore> Public Property TransformCurrentPoint As PointF
    ' ===== FIX: Proper RotationAngle Declaration =====
    <XmlAttribute>
    Public Overridable Property RotationAngle As Single = 0.0F
    '<XmlAttribute> Public Property RotationAngle As Single = 0.0F
    <XmlIgnore> Public Property ScaleX As Single = 1.0F
    <XmlIgnore> Public Property ScaleY As Single = 1.0F
    <XmlIgnore> Public Property RotationCenter As PointF
    <XmlIgnore> Public Property TransformType As String = ""

    <XmlAttribute("RotationCenterX")>
    Public Property RotationCenterX As Single
        Get
            Return RotationCenter.X
        End Get
        Set(value As Single)
            RotationCenter = New PointF(value, RotationCenter.Y)
        End Set
    End Property

    <XmlAttribute("RotationCenterY")>
    Public Property RotationCenterY As Single
        Get
            Return RotationCenter.Y
        End Get
        Set(value As Single)
            RotationCenter = New PointF(RotationCenter.X, value)
        End Set
    End Property

    <XmlAttribute("ScaleX")>
    Public Property ScaleXSerialized As Single
        Get
            Return ScaleX
        End Get
        Set(value As Single)
            ScaleX = value
        End Set
    End Property

    <XmlAttribute("ScaleY")>
    Public Property ScaleYSerialized As Single
        Get
            Return ScaleY
        End Get
        Set(value As Single)
            ScaleY = value
        End Set
    End Property
#End Region

#Region "State"
    <XmlIgnore> Public Property IsSelected As Boolean = False
    <XmlIgnore> Public Property IsTransforming As Boolean = False
    <XmlIgnore> Public Property CurrentAnchor As AnchorEnumN = AnchorEnumN.None
    <XmlIgnore> Public Property AnchorSize As Size = New Size(7, 7)
    <XmlIgnore> Public Property TransformStartPoint As PointF
    <XmlIgnore> Public Property TransformCenter As PointF
    <XmlIgnore> Public Property OriginalRotation As Single
    <XmlIgnore> Public Property OriginalScaleX As Single = 1.0F
    <XmlIgnore> Public Property OriginalScaleY As Single = 1.0F
#End Region

#Region "Constructors"
    Public Sub New()
    End Sub

    Public Sub New(foreColor As Color, fillColor As Color, Optional lineWidth As Integer = 1)
        Me.ForeColor = foreColor
        Me.FillColor = fillColor
        Me.LineWidth = lineWidth
    End Sub
#End Region

#Region "Core Functionality"
    Public MustOverride Sub Draw(gr As Graphics)
    Public MustOverride Function GetBounds() As Rectangle
    Public MustOverride Function IsAt(x As Integer, y As Integer) As Boolean
    Public MustOverride Sub NewPoint(x As Integer, y As Integer)
    Public MustOverride Function IsEmpty() As Boolean
    Public MustOverride Sub Render(g As Graphics, dr As DrawableN)
    Public MustOverride Function GetScreenBounds(gr As Graphics) As RectangleF
    Public Overridable Function Clone() As DrawableN
        ' Create shallow copy as base implementation
        Return DirectCast(Me.MemberwiseClone(), DrawableN)
    End Function

    ' Add this method to reset state after loading
    Public Overridable Sub AfterLoad()
        If LineWidth <= 0 Then LineWidth = 1
        If ForeColor = Color.Empty Then ForeColor = Color.Black
        If FillColor = Color.Empty Then FillColor = Color.White
        IsSelected = False
        IsTransforming = False
        CurrentAnchor = AnchorEnumN.None
        OriginalScaleX = If(ScaleX = 0, 1.0F, ScaleX)
        OriginalScaleY = If(ScaleY = 0, 1.0F, ScaleY)
    End Sub

#End Region

#Region "Transformations"

    Public Overridable Sub MoveRelative(dx As Integer, dy As Integer)
        X1 += dx : Y1 += dy : X2 += dx : Y2 += dy
        RotationCenter = New PointF(RotationCenter.X + dx, RotationCenter.Y + dy)
    End Sub

    Public Overridable Sub ApplyTransformations(gr As Graphics)
        If ScaleX = 0 Then ScaleX = 1
        If ScaleY = 0 Then ScaleY = 1
        If RotationCenter.IsEmpty Then RotationCenter = GetCenter()
        gr.TranslateTransform(RotationCenter.X, RotationCenter.Y)
        gr.RotateTransform(RotationAngle)
        gr.ScaleTransform(ScaleX, ScaleY)
        gr.TranslateTransform(-RotationCenter.X, -RotationCenter.Y)
    End Sub

    Public Function GetRawCenter() As PointF
        Dim x = Math.Min(X1, X2) + Math.Abs(X2 - X1) / 2
        Dim y = Math.Min(Y1, Y2) + Math.Abs(Y2 - Y1) / 2
        Return New PointF(x, y)
    End Function

    Public Function GetTransformMatrix() As Matrix
        If RotationCenter.IsEmpty Then RotationCenter = GetRawCenter()
        Dim matrix As New Matrix()
        matrix.Translate(RotationCenter.X, RotationCenter.Y)
        matrix.Rotate(RotationAngle)
        matrix.Scale(ScaleX, ScaleY)
        matrix.Translate(-RotationCenter.X, -RotationCenter.Y)
        Return matrix
    End Function

    Public Function TransformPoint(pt As PointF) As PointF
        Dim points() As PointF = {pt}
        GetTransformMatrix().TransformPoints(points)
        Return points(0)
    End Function

    Public Function InverseTransformPoint(pt As PointF) As PointF
        Try
            Dim matrix = GetTransformMatrix()
            matrix.Invert()
            Dim points() As PointF = {pt}
            matrix.TransformPoints(points)
            Return points(0)
        Catch
            Return pt
        End Try
    End Function

    Public Overridable Sub DrawTransformationGuides(gr As Graphics)
        If Not IsTransforming Then Return
        Dim center = TransformPoint(GetCenter())
        Select Case TransformType
            Case "ROTATE" : DrawRotationGuide(gr, center)
            Case "RESIZE" : DrawAnchorLine(gr, center, TransformCurrentPoint)
        End Select
    End Sub
#End Region

#Region "Anchor Management"

    Public Overridable Function GetAnchors() As Dictionary(Of AnchorEnumN, RectangleF)
        Dim anchors As New Dictionary(Of AnchorEnumN, RectangleF)
        ' Anchor points must start from local/untransformed bounds.
        ' They are transformed exactly once via ApplyTransform below.
        Dim baseRect As RectangleF = RectangleF.op_Implicit(GetBounds())

        ' Calculate anchor points in local (untransformed) space
        Dim corners As New Dictionary(Of AnchorEnumN, PointF) From {
        {AnchorEnumN.Nwse, New PointF(baseRect.Left, baseRect.Top)},
        {AnchorEnumN.Nesw, New PointF(baseRect.Right, baseRect.Top)},
        {AnchorEnumN.Senw, New PointF(baseRect.Right, baseRect.Bottom)},
        {AnchorEnumN.Swne, New PointF(baseRect.Left, baseRect.Bottom)},
        {AnchorEnumN.Ns, New PointF(baseRect.Left + baseRect.Width / 2, baseRect.Top)},
        {AnchorEnumN.Sn, New PointF(baseRect.Left + baseRect.Width / 2, baseRect.Bottom)},
        {AnchorEnumN.Ew, New PointF(baseRect.Right, baseRect.Top + baseRect.Height / 2)},
        {AnchorEnumN.We, New PointF(baseRect.Left, baseRect.Top + baseRect.Height / 2)},
        {AnchorEnumN.Move, New PointF(baseRect.Left + baseRect.Width / 2, baseRect.Top + baseRect.Height / 2)},
        {AnchorEnumN.Rotate, New PointF(baseRect.Left + baseRect.Width / 2, baseRect.Top - AnchorSize.Height * 2)}
    }

        ' Transform and create anchor rects
        For Each kvp In corners
            Dim pt = ApplyTransform(kvp.Value.X, kvp.Value.Y)
            anchors(kvp.Key) = CreateAnchorRect(pt.X, pt.Y)
        Next

        Return anchors
    End Function

    Public Overridable Function GetAnchorAt(x As Integer, y As Integer) As AnchorEnumN
        Dim pt As New PointF(x, y)
        For Each anchor In GetAnchors()
            If anchor.Value.Contains(pt) Then Return anchor.Key
        Next
        Return AnchorEnumN.None
    End Function
    ' Add Move anchor to the drawing logic
    Public Overridable Sub DrawAnchors(gr As Graphics)
        If Not IsSelected Then Return

        For Each anchor In GetAnchors()
            Dim rect = anchor.Value
            Dim brush = If(CurrentAnchor = anchor.Key, Brushes.Yellow,
                    If(anchor.Key = AnchorEnumN.Rotate, Brushes.LightGreen,
                    If(anchor.Key = AnchorEnumN.Move, Brushes.LightBlue, Brushes.WhiteSmoke)))

            ' Draw all anchors as rectangles except rotation
            If anchor.Key = AnchorEnumN.Rotate Then
                gr.FillEllipse(brush, rect)
                gr.DrawEllipse(Pens.Black, rect)
            Else
                gr.FillRectangle(brush, rect)
                gr.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height)
            End If
        Next
    End Sub
    Private Function CreateAnchorRect(x As Single, y As Single) As RectangleF
        Return New RectangleF(x - AnchorSize.Width / 2, y - AnchorSize.Height / 2, AnchorSize.Width, AnchorSize.Height)
    End Function

    Public Function GetResizeAnchorBounds() As RectangleF
        Dim anchors = GetAnchors()

        ' Define the keys for resize-only anchors
        Dim resizeKeys As AnchorEnumN() = {
        AnchorEnumN.Nwse, AnchorEnumN.Nesw, AnchorEnumN.Senw, AnchorEnumN.Swne,
        AnchorEnumN.Ns, AnchorEnumN.Sn, AnchorEnumN.Ew, AnchorEnumN.We
    }

        Dim unionRect As RectangleF = RectangleF.Empty

        For Each key In resizeKeys
            If anchors.ContainsKey(key) Then
                If unionRect.IsEmpty Then
                    unionRect = anchors(key)
                Else
                    unionRect = RectangleF.Union(unionRect, anchors(key))
                End If
            End If
        Next

        Return unionRect
    End Function

#End Region

#Region "Transformation Actions"
    Private Sub DrawRotationGuide(gr As Graphics, center As PointF)
        DrawAnchorLine(gr, center, TransformCurrentPoint)
        Dim angle1 = Math.Atan2(TransformStartPoint.Y - center.Y, TransformStartPoint.X - center.X)
        Dim angle2 = Math.Atan2(TransformCurrentPoint.Y - center.Y, TransformCurrentPoint.X - center.X)
        Dim startAngle = CSng(angle1 * 180 / Math.PI)
        Dim endAngle = CSng(angle2 * 180 / Math.PI)
        Dim radius = Distance(center, TransformStartPoint)
        DrawRotationGuide(gr, center, radius, startAngle, endAngle)
    End Sub

    Private Sub DrawRotationGuide(gr As Graphics, center As PointF, radius As Single,
                                 startAngle As Single, endAngle As Single)
        Using p As New Pen(Color.Magenta, 5)
            gr.DrawArc(p, center.X - radius, center.Y - radius,
                      radius * 2, radius * 2, startAngle, endAngle - startAngle)
        End Using
    End Sub

    Public Overridable Sub StartTransform(anchor As AnchorEnumN, startPoint As PointF)
        IsTransforming = True
        CurrentAnchor = anchor
        TransformStartPoint = startPoint
        OriginalRotation = RotationAngle
        OriginalScaleX = ScaleX
        OriginalScaleY = ScaleY

        'ALWAYS recalculate center before transform
        RotationCenter = GetCenter()
        TransformCenter = RotationCenter
    End Sub

    Public Overridable Sub UpdateTransform(currentPoint As PointF)
        If Not IsTransforming Then Return
        TransformCurrentPoint = currentPoint

        Select Case CurrentAnchor
            Case AnchorEnumN.Move : HandleMove(currentPoint)
            Case AnchorEnumN.Rotate : HandleRotation(currentPoint)
            Case Else : HandleResize(currentPoint)
        End Select
    End Sub

    Protected Overridable Sub HandleMove(currentPoint As PointF)
        Dim dx = currentPoint.X - TransformStartPoint.X
        Dim dy = currentPoint.Y - TransformStartPoint.Y
        MoveRelative(CInt(dx), CInt(dy))
        TransformStartPoint = currentPoint
    End Sub

    Protected Overridable Sub HandleRotation(currentPoint As PointF)
        'Get center in SCREEN coordinates
        Dim c = TransformCenter

        'Calculate angles relative to center
        Dim startAngle = Math.Atan2(
        TransformStartPoint.Y - c.Y,
        TransformStartPoint.X - c.X
    )

        Dim currentAngle = Math.Atan2(
        currentPoint.Y - c.Y,
        currentPoint.X - c.X
    )

        'Calculate delta in degrees
        Dim delta = CSng((currentAngle - startAngle) * (180 / Math.PI))

        'Apply rotation
        RotationAngle = (OriginalRotation + delta) Mod 360
    End Sub

    ' Add new helper function for safe scaling calculations
    Private Function CalculateSafeScaleFactor(startValue As Single, currentValue As Single) As Single
        If Math.Abs(startValue) < 0.1 Then Return 1.0F
        Dim scale = currentValue / startValue
        Return Math.Max(0.1F, Math.Min(10.0F, scale))
    End Function

    ' Replace the entire HandleResize method with this corrected version
    Protected Overridable Sub HandleResize(currentPoint As PointF)
        Dim c = TransformCenter
        Dim startVec As New PointF(TransformStartPoint.X - c.X, TransformStartPoint.Y - c.Y)
        Dim currentVec As New PointF(currentPoint.X - c.X, currentPoint.Y - c.Y)

        ' Calculate raw scaling factors with safety checks
        Dim scaleXFactor = CalculateSafeScaleFactor(startVec.X, currentVec.X)
        Dim scaleYFactor = CalculateSafeScaleFactor(startVec.Y, currentVec.Y)

        ' Apply shift-key constraint (uniform scaling)
        If Control.ModifierKeys = Keys.Shift Then
            Dim uniform = (scaleXFactor + scaleYFactor) / 2
            scaleXFactor = uniform
            scaleYFactor = uniform
        End If

        ' Anchor-based locking
        Select Case CurrentAnchor.ToString.ToLower
            Case "we", "ew"
                scaleYFactor = 1.0F ' Lock vertical
                Console.WriteLine($"CurrentAnchor: {CurrentAnchor.ToString()}, Lock vertical: {scaleYFactor}")
            Case "ns", "sn"
                scaleXFactor = 1.0F ' Lock horizontal
                Console.WriteLine($"CurrentAnchor: {CurrentAnchor.ToString()}, Lock horizontal: {scaleXFactor}")
        End Select
        ' Apply final scaling with clamping
        ScaleX = OriginalScaleX * scaleXFactor
        ScaleY = OriginalScaleY * scaleYFactor
        Console.WriteLine($"Scaling: X={ScaleX.ToString("0.00")}, Y={ScaleY.ToString("0.00")}")
        Console.WriteLine($"CurrentAnchor: {CurrentAnchor.ToString()}, Transformed: {GetTransformedBounds()}")
    End Sub

    Private Function ApplyTransform(x As Single, y As Single) As PointF
        Dim cx = RotationCenter.X
        Dim cy = RotationCenter.Y

        ' Translate to origin
        Dim dx = x - cx
        Dim dy = y - cy

        ' Scale
        dx *= ScaleX
        dy *= ScaleY

        ' Rotate
        Dim angleRad = RotationAngle * Math.PI / 180.0
        Dim cosA = Math.Cos(angleRad)
        Dim sinA = Math.Sin(angleRad)

        Dim rx = dx * cosA - dy * sinA
        Dim ry = dx * sinA + dy * cosA

        ' Translate back
        Return New PointF(rx + cx, ry + cy)
    End Function

#End Region

#Region "Helpers"
    ' Fix rotation center calculation
    Public Overridable Function GetCenter() As PointF
        ' Use transformed bounds for accurate center
        Dim bounds = GetTransformedBounds()
        Return New PointF(
        bounds.X + bounds.Width / 2,
        bounds.Y + bounds.Height / 2
    )
    End Function

    Public Overridable Function GetTransformedBounds() As RectangleF
        ' Get original untransformed rectangle
        Dim baseRect As RectangleF = RectangleF.op_Implicit(GetBounds()) ' GetOriginalBounds()

        ' Get 4 corners of the rectangle
        Dim pts() As PointF = {
        New PointF(baseRect.Left, baseRect.Top),
        New PointF(baseRect.Right, baseRect.Top),
        New PointF(baseRect.Right, baseRect.Bottom),
        New PointF(baseRect.Left, baseRect.Bottom)
    }

        ' Apply full transform to each point
        For i = 0 To pts.Length - 1
            pts(i) = ApplyTransform(pts(i).X, pts(i).Y)
        Next

        ' Calculate bounding box of transformed points
        Dim minX = pts.Min(Function(p) p.X)
        Dim minY = pts.Min(Function(p) p.Y)
        Dim maxX = pts.Max(Function(p) p.X)
        Dim maxY = pts.Max(Function(p) p.Y)

        Return New RectangleF(minX, minY, maxX - minX, maxY - minY)
    End Function

    'DELETE GeometryN class - replace with simpler methods
    'Add these methods directly to DrawableN instead:
    Private Sub DrawAnchorLine(gr As Graphics, p1 As PointF, p2 As PointF)
        Using p As New Pen(Color.Gray, 1)
            gr.DrawLine(p, p1, p2)
        End Using
    End Sub



    Private Function Distance(p1 As PointF, p2 As PointF) As Single
        Dim dx = p1.X - p2.X
        Dim dy = p1.Y - p2.Y
        Return CSng(Math.Sqrt(dx * dx + dy * dy))
    End Function
#End Region

End Class
Public Enum AnchorEnumN
    None
    Move       '← Add this for movement
    Nwse       ' Top-left
    Ns         ' Top
    Nesw       ' Top-right
    Ew         ' Right
    Senw       ' Bottom-right
    Sn         ' Bottom
    Swne       ' Bottom-left
    We         ' Left
    Rotate
End Enum
