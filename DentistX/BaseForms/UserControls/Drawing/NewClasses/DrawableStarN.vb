Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Xml.Serialization

<Serializable()>
Public Class DrawableStarN
    Inherits DrawableN

    Public Property PointsCount As Integer = 8
    Public Property InnerRadiusRatio As Single = 0.5F ' Ratio of inner radius to outer radius

    Public Sub New()
    End Sub
    Public Sub New(ByVal fore_color As Color, ByVal fill_color As Color, Optional ByVal line_width As Integer = 0, Optional ByVal new_x1 As Integer = 0, Optional ByVal new_y1 As Integer = 0, Optional ByVal new_x2 As Integer = 1, Optional ByVal new_y2 As Integer = 1)
        MyBase.New(fore_color, fill_color, line_width)
        ' Make star outline follow the currently selected border color
        Me.LineColor = fore_color

        X1 = new_x1
        Y1 = new_y1
        X2 = new_x2
        Y2 = new_y2
        ' Initialize rotation center
        UpdateRotationCenter()
    End Sub
    Public Sub New(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer, Optional points As Integer = 5)
        Me.X1 = x1
        Me.Y1 = y1
        Me.X2 = x2
        Me.Y2 = y2
        Me.PointsCount = points
    End Sub



#Region "Type Text"

    ' ===== TEXT PROPERTIES =====
    Private _text As String = String.Empty
    'Private _textColor As Color = Color.Black
    Private _textFont As New Font("Arial", 12)
    Private _textAlignment As ContentAlignment = ContentAlignment.MiddleCenter
    Private _textPadding As Integer = 5

    Public Property Text As String
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
        End Set
    End Property

    ' Add XML serialization for TextColor
    <XmlIgnore>
    Public Property TextColor As Color = Color.Black

    <XmlElement("TextColor")>
    Public Property TextColorArgb As Integer
        Get
            Return TextColor.ToArgb()
        End Get
        Set(value As Integer)
            TextColor = Color.FromArgb(value)
        End Set
    End Property




    <XmlIgnore>
    Public Property TextFont As Font
        Get
            Return _textFont
        End Get
        Set(value As Font)
            _textFont = value
        End Set
    End Property

    Public Property TextFontSerializable As String
        Get
            Return New FontConverter().ConvertToString(TextFont)
        End Get
        Set(value As String)
            TextFont = CType(New FontConverter().ConvertFromString(value), Font)
        End Set
    End Property

    Public Property TextAlignment As ContentAlignment
        Get
            Return _textAlignment
        End Get
        Set(value As ContentAlignment)
            _textAlignment = value
        End Set
    End Property

    Public Property TextPadding As Integer
        Get
            Return _textPadding
        End Get
        Set(value As Integer)
            _textPadding = Math.Max(0, value)
        End Set
    End Property


    ' Converts ContentAlignment to StringAlignment
    Private Function GetStringAlignment(align As ContentAlignment) As StringAlignment
        Select Case align
            Case ContentAlignment.TopLeft, ContentAlignment.MiddleLeft, ContentAlignment.BottomLeft
                Return StringAlignment.Near
            Case ContentAlignment.TopRight, ContentAlignment.MiddleRight, ContentAlignment.BottomRight
                Return StringAlignment.Far
            Case Else
                Return StringAlignment.Center
        End Select
    End Function

#End Region

    ' Add this after loading
    Public Overrides Sub AfterLoad()
        MyBase.AfterLoad()
        ' Reset rotation center to shape center
        Dim rect = GetBounds()
        RotationCenter = New PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2)

        ' Ensure outline color tracks foreground for older/saved stars
        If LineColor = Color.Empty OrElse LineColor = Color.Black Then
            LineColor = ForeColor
        End If
    End Sub

    Public Overrides Sub Draw(gr As Graphics)
        ' Save graphics state
        Dim container As GraphicsContainer = gr.BeginContainer()

        Try
            ApplyTransformations(gr)
            Dim rect = GetBounds()

            ' Bounds validation
            If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

            ' Create star path (rotation/scale come from ApplyTransformations only — avoid double-rotating the path)
            Using path As GraphicsPath = CreateStarPath(rect, PointsCount, InnerRadiusRatio)
                ' Fill
                Using brush As New SolidBrush(Me.FillColor)
                    gr.FillPath(brush, path)
                End Using

                ' Stroke
                Using pen As New Pen(Me.LineColor, LineWidth)
                    gr.DrawPath(pen, path)
                End Using
            End Using

            ' Inside text — same transform as shape
            If Not String.IsNullOrEmpty(Text) Then
                Dim textRect As New RectangleF(rect.X, rect.Y, rect.Width, rect.Height)
                textRect.Inflate(-TextPadding, -TextPadding)
                If textRect.Width > 0 AndAlso textRect.Height > 0 Then
                    Using format As New StringFormat(),
                      brush As New SolidBrush(TextColor)

                        format.Alignment = StringAlignment.Center
                        format.LineAlignment = StringAlignment.Center
                        format.FormatFlags = StringFormatFlags.LineLimit
                        format.Trimming = StringTrimming.EllipsisWord

                        Dim fontSize = CSng(Math.Min(textRect.Width / 10.0F, textRect.Height / 2.0F))
                        If fontSize < 6 Then fontSize = 6
                        Using autoFont = New Font(TextFont.FontFamily, fontSize)
                            gr.DrawString(Text, autoFont, brush, textRect, format)
                        End Using
                    End Using
                End If
            End If
        Finally
            gr.EndContainer(container)
        End Try

        ' ==== Optional: Draw selection rectangle & anchors ====
        If IsSelected Then
            Dim rectF As RectangleF = GetTransformedBounds()
            Dim rect As New Rectangle(CInt(rectF.X), CInt(rectF.Y), CInt(rectF.Width), CInt(rectF.Height))
            Using selPen As New Pen(Color.Yellow, 1)
                selPen.DashStyle = DashStyle.Dot
                gr.DrawRectangle(selPen, rect)
            End Using
        End If
    End Sub


    Public Sub Draw1(gr As Graphics)

        ' Save graphics state
        Dim container As GraphicsContainer = gr.BeginContainer()

        Try
            ApplyTransformations(gr)
            Dim rect = GetBounds()
            'ADD BOUNDS VALIDATION
            If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

            ' Fill star

            If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

            Using path As GraphicsPath = CreateStarPath(rect, PointsCount, InnerRadiusRatio)
                ' Apply transform (rotation)
                Dim matrix As New Matrix()
                matrix.RotateAt(RotationAngle, TransformCenter)
                path.Transform(matrix)

                ' Fill
                Using brush As New SolidBrush(Me.FillColor)
                    gr.FillPath(brush, path)
                End Using

                ' Stroke
                Using pen As New Pen(Me.LineColor, LineWidth)
                    gr.DrawPath(pen, path)
                End Using
            End Using
        Finally
            ' Restore graphics state
            gr.EndContainer(container)
        End Try

        ' Draw anchors on top (untransformed)
        'If IsSelected Then DrawAnchors(gr)
        If IsSelected OrElse IsTransforming Then
            Dim borderColor = If(IsTransforming, Color.Cyan, Color.Yellow)
            Using pen As New Pen(borderColor, 1)
                pen.DashStyle = If(IsTransforming, DashStyle.Dash, DashStyle.Solid)
                gr.DrawRectangle(pen, GetBounds())
            End Using
            DrawAnchors(gr)
        End If


    End Sub



    ' Render method added — just calls Draw
    Public Overrides Sub Render(gr As Graphics, dr As DrawableN)
        'Draw(gr)
    End Sub
    Public Overrides Function GetBounds() As Rectangle
        Return New Rectangle(
            Math.Min(X1, X2),
            Math.Min(Y1, Y2),
            Math.Abs(X2 - X1),
            Math.Abs(Y2 - Y1)
        )
    End Function

    Private Sub UpdateRotationCenter()
        Dim rect = GetBounds()
        RotationCenter = New PointF(
        rect.Left + rect.Width / 2.0F,
        rect.Top + rect.Height / 2.0F
    )
    End Sub

    Private Function CreateStarPath(bounds As Rectangle, points As Integer, innerRatio As Single) As GraphicsPath
        Dim path As New GraphicsPath()

        Dim centerX = bounds.X + bounds.Width / 2.0F
        Dim centerY = bounds.Y + bounds.Height / 2.0F
        Dim radiusOuter = Math.Min(bounds.Width, bounds.Height) / 2.0F
        Dim radiusInner = radiusOuter * innerRatio

        Dim angleStep = Math.PI / points
        Dim angle = -Math.PI / 2

        Dim pts As New List(Of PointF)()

        For i = 0 To points * 2 - 1
            Dim r = If(i Mod 2 = 0, radiusOuter, radiusInner)
            Dim x = centerX + CSng(r * Math.Cos(angle))
            Dim y = centerY + CSng(r * Math.Sin(angle))
            pts.Add(New PointF(x, y))
            angle += angleStep
        Next

        path.AddPolygon(pts.ToArray())
        Return path
    End Function

    Public Overrides Function IsAt(x As Integer, y As Integer) As Boolean
        Dim localPt = InverseTransformPoint(New PointF(x, y))
        Dim rect = GetBounds()
        Using path As GraphicsPath = CreateStarPath(rect, PointsCount, InnerRadiusRatio)
            Return path.IsVisible(localPt.X, localPt.Y)
        End Using
    End Function

    Public Overrides Sub NewPoint(x As Integer, y As Integer)
        X2 = x
        Y2 = y
        UpdateRotationCenter()
    End Sub

    Public Overrides Function IsEmpty() As Boolean
        Return X1 = X2 AndAlso Y1 = Y2
    End Function

    Public Overrides Sub MoveRelative(dx As Integer, dy As Integer)
        X1 += dx
        Y1 += dy
        X2 += dx
        Y2 += dy
        ' Correctly update rotation center by displacement
        ' Update both rotation center and transform center
        RotationCenter = New PointF(RotationCenter.X + dx, RotationCenter.Y + dy)
        TransformCenter = New PointF(TransformCenter.X + dx, TransformCenter.Y + dy)        'Dim center As New PointF(GetBounds.Left + GetBounds.Width / 2.0F, GetBounds.Top + GetBounds.Height / 2.0F)
        'RotationCenter = center
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
                Dim dx = currentPoint.X - TransformStartPoint.X
                Dim dy = currentPoint.Y - TransformStartPoint.Y
                MoveShapeInScreenSpace(dx, dy)
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
    Public Overridable Sub MoveShapeInScreenSpace(dx As Single, dy As Single)
        X1 += dx
        Y1 += dy
        X2 += dx
        Y2 += dy

        ' Update both centers
        RotationCenter = New PointF(RotationCenter.X + dx, RotationCenter.Y + dy)
        TransformCenter = New PointF(TransformCenter.X + dx, TransformCenter.Y + dy)
    End Sub

    Public Overrides Function GetScreenBounds(gr As Graphics) As RectangleF
        Dim path As New GraphicsPath()
        path.AddRectangle(Me.GetBounds()) ' Or custom logic if you override it
        Dim matrix = gr.Transform.Clone()
        path.Transform(matrix)
        Return path.GetBounds()
    End Function

End Class

'' ===== TRANSFORMATION OVERRIDES =====
'Protected Overrides Sub HandleResize(currentPoint As PointF)
'    Dim center = TransformCenter
'    Dim startVec As New PointF(
'        TransformStartPoint.X - center.X,
'        TransformStartPoint.Y - center.Y
'    )
'    Dim currentVec As New PointF(
'        currentPoint.X - center.X,
'        currentPoint.Y - center.Y
'    )

'    ' Calculate scaling factors
'    Dim scaleXFactor = If(Math.Abs(startVec.X) > 0.1, currentVec.X / startVec.X, 1)
'    Dim scaleYFactor = If(Math.Abs(startVec.Y) > 0.1, currentVec.Y / startVec.Y, 1)

'    ' Apply constraint for uniform scaling with Shift key
'    If Control.ModifierKeys = Keys.Shift Then
'        Dim uniformScale = (scaleXFactor + scaleYFactor) / 2.0F
'        scaleXFactor = uniformScale
'        scaleYFactor = uniformScale
'    End If
'    ' Anchor-based locking
'    Select Case CurrentAnchor.ToString.ToLower
'        Case "we", "ew"
'            scaleYFactor = 1.0F ' Lock vertical
'            Console.WriteLine($"CurrentAnchor: {CurrentAnchor.ToString()}, Lock vertical: {scaleYFactor}")
'        Case "ns", "sn"
'            scaleXFactor = 1.0F ' Lock horizontal
'            Console.WriteLine($"CurrentAnchor: {CurrentAnchor.ToString()}, Lock horizontal: {scaleXFactor}")
'    End Select
'    ' Apply scaling using base class properties
'    ScaleX = OriginalScaleX * scaleXFactor
'    ScaleY = OriginalScaleY * scaleYFactor

'    ' Apply minimum scale
'    If Math.Abs(ScaleX) < 0.1 Then ScaleX = Math.Sign(ScaleX) * 0.1F
'    If Math.Abs(ScaleY) < 0.1 Then ScaleY = Math.Sign(ScaleY) * 0.1F
'End Sub





