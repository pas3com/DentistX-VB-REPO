Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Xml.Serialization

<Serializable()>
Public Class DrawableImageN
    Inherits DrawableN

    ' ===== IMAGE STORAGE =====
    Private _picture As Image

    <XmlIgnore>
    Public Property Picture As Image
        Get
            Return _picture
        End Get
        Set(value As Image)
            If Object.ReferenceEquals(_picture, value) Then Return
            If _picture IsNot Nothing Then
                _picture.Dispose()
            End If
            _picture = value
        End Set
    End Property

    <XmlElement("ImageData")>
    Public Property ImageData As Byte()
        Get
            If Picture Is Nothing Then Return Nothing
            Return ImageBytesForPersistence(Picture)
        End Get
        Set(value As Byte())
            If value Is Nothing Then
                Picture = Nothing
            Else
                Using ms As New MemoryStream(value)
                    Picture = Image.FromStream(ms)
                End Using
            End If
        End Set
    End Property

    ' ===== CONSTRUCTORS =====
    Public Sub New()
    End Sub

    Public Sub New(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer)
        Me.X1 = x1
        Me.Y1 = y1
        Me.X2 = x2
        Me.Y2 = y2
    End Sub

    Public Sub New(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer, image As Image)
        Me.New(x1, y1, x2, y2)
        SetImage(image, True)
    End Sub

    ' ===== IMAGE SETTER =====
    Public Sub SetImage(image As Image, preserveAspectRatio As Boolean)
        If image Is Nothing Then Return

        Picture = image

        If preserveAspectRatio Then
            Dim rect = GetBounds()
            Dim aspectRatio = image.Width / image.Height

            If rect.Width / rect.Height > aspectRatio Then
                ' Fit to height
                Dim newWidth = CInt(rect.Height * aspectRatio)
                Dim centerX = (rect.Left + rect.Right) \ 2
                X1 = centerX - newWidth \ 2
                X2 = centerX + newWidth \ 2
            Else
                ' Fit to width
                Dim newHeight = CInt(rect.Width / aspectRatio)
                Dim centerY = (rect.Top + rect.Bottom) \ 2
                Y1 = centerY - newHeight \ 2
                Y2 = centerY + newHeight \ 2
            End If
        End If
    End Sub

    ' Add this after loading
    Public Overrides Sub AfterLoad()
        MyBase.AfterLoad()
        ' Reset rotation center to shape center
        Dim rect = GetBounds()
        RotationCenter = New PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2)
    End Sub


    ' ===== DRAWING =====
    Public Overrides Sub Draw(gr As Graphics)
        gr.SmoothingMode = SmoothingMode.AntiAlias
        gr.InterpolationMode = InterpolationMode.HighQualityBicubic
        gr.PixelOffsetMode = PixelOffsetMode.HighQuality
        gr.CompositingQuality = CompositingQuality.HighQuality

        ' Save graphics state
        Dim container As GraphicsContainer = gr.BeginContainer()

        Try
            ApplyTransformations(gr)
            Dim rect = GetBounds()
            'ADD BOUNDS VALIDATION
            If rect.Width <= 0 OrElse rect.Height <= 0 Then Return
            ' Fill image
            If Picture IsNot Nothing Then
                gr.DrawImage(Picture, rect)
            Else
                Using brush As New SolidBrush(Color.LightGray)
                    gr.FillRectangle(brush, rect)
                End Using
                Using pen As New Pen(Color.DarkGray, 1)
                    gr.DrawRectangle(pen, rect)
                End Using
            End If

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

    Public Overrides Sub Render(gr As Graphics, dr As DrawableN)
        'Draw(gr)
    End Sub

    ' ===== GEOMETRY =====
    Public Overrides Function GetBounds() As Rectangle
        Return New Rectangle(
            Math.Min(X1, X2),
            Math.Min(Y1, Y2),
            Math.Abs(X2 - X1),
            Math.Abs(Y2 - Y1)
        )
    End Function

    Public Overrides Function IsAt(x As Integer, y As Integer) As Boolean
        Return GetBounds().Contains(x, y)
    End Function

    ' ===== POINT AND TRANSFORMATION =====
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

    ' ===== TRANSFORMATION OVERRIDES =====
    Protected Overrides Sub HandleResize(currentPoint As PointF)
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

        ' Apply constraint for uniform scaling with Shift key
        If Control.ModifierKeys = Keys.Shift Then
            Dim uniformScale = (scaleXFactor + scaleYFactor) / 2.0F
            scaleXFactor = uniformScale
            scaleYFactor = uniformScale
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
        ' Apply scaling using base class properties
        ScaleX = OriginalScaleX * scaleXFactor
        ScaleY = OriginalScaleY * scaleYFactor

        ' Apply minimum scale
        If Math.Abs(ScaleX) < 0.1 Then ScaleX = Math.Sign(ScaleX) * 0.1F
        If Math.Abs(ScaleY) < 0.1 Then ScaleY = Math.Sign(ScaleY) * 0.1F
    End Sub

    Public Overrides Function GetScreenBounds(gr As Graphics) As RectangleF
        Dim path As New GraphicsPath()
        path.AddRectangle(Me.GetBounds()) ' Or custom logic if you override it
        Dim matrix = gr.Transform.Clone()
        path.Transform(matrix)
        Return path.GetBounds()
    End Function

End Class
