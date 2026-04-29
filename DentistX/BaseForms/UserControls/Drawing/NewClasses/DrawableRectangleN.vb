Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Xml.Serialization

<Serializable()>
Public Class DrawableRectangleN
    Inherits DrawableN

    ' ===== CONSTRUCTORS =====
    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal fore_color As Color, ByVal fill_color As Color, Optional ByVal line_width As Integer = 1,
                   Optional ByVal new_x1 As Integer = 0, Optional ByVal new_y1 As Integer = 0,
                   Optional ByVal new_x2 As Integer = 1, Optional ByVal new_y2 As Integer = 1)
        MyBase.New(fore_color, fill_color, line_width)
        X1 = new_x1
        Y1 = new_y1
        X2 = new_x2
        Y2 = new_y2
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
    End Sub



    ' ===== OVERRIDDEN METHODS =====
    Public Overrides Sub Render(gr As Graphics, dr As DrawableN)
        'Draw(gr)
    End Sub

    ' MODIFIED DRAW METHOD
    Public Overrides Sub Draw(gr As Graphics)
        Dim container As GraphicsContainer = gr.BeginContainer()
        Try
            ApplyTransformations(gr)
            Dim rect = GetBounds()
            If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

            ' Fill rectangle
            If FillColor.A > 0 Then
                Using brush As New SolidBrush(FillColor)
                    gr.FillRectangle(brush, rect)
                End Using
            End If

            ' Draw rectangle border
            Using pen As New Pen(ForeColor, LineWidth)
                gr.DrawRectangle(pen, rect)
            End Using

            ' ===== NEW TEXT RENDERING (FIXED) =====
            If Not String.IsNullOrEmpty(Text) Then
                Dim textRect = Rectangle.Inflate(rect, -TextPadding, -TextPadding)
                If textRect.Width > 0 AndAlso textRect.Height > 0 Then
                    Using format As New StringFormat(),
                      brush As New SolidBrush(TextColor)

                        ' Convert ContentAlignment to StringFormat properties
                        Select Case TextAlignment
                            Case ContentAlignment.TopLeft
                                format.Alignment = StringAlignment.Near
                                format.LineAlignment = StringAlignment.Near
                            Case ContentAlignment.TopCenter
                                format.Alignment = StringAlignment.Center
                                format.LineAlignment = StringAlignment.Near
                            Case ContentAlignment.TopRight
                                format.Alignment = StringAlignment.Far
                                format.LineAlignment = StringAlignment.Near
                            Case ContentAlignment.MiddleLeft
                                format.Alignment = StringAlignment.Near
                                format.LineAlignment = StringAlignment.Center
                            Case ContentAlignment.MiddleCenter
                                format.Alignment = StringAlignment.Center
                                format.LineAlignment = StringAlignment.Center
                            Case ContentAlignment.MiddleRight
                                format.Alignment = StringAlignment.Far
                                format.LineAlignment = StringAlignment.Center
                            Case ContentAlignment.BottomLeft
                                format.Alignment = StringAlignment.Near
                                format.LineAlignment = StringAlignment.Far
                            Case ContentAlignment.BottomCenter
                                format.Alignment = StringAlignment.Center
                                format.LineAlignment = StringAlignment.Far
                            Case ContentAlignment.BottomRight
                                format.Alignment = StringAlignment.Far
                                format.LineAlignment = StringAlignment.Far
                        End Select

                        format.FormatFlags = StringFormatFlags.LineLimit
                        format.Trimming = StringTrimming.EllipsisWord
                        'Font
                        ' Auto-size font to fit rectangle
                        Dim fontSize = Math.Min(textRect.Width \ 10, textRect.Height \ 2)
                        If fontSize < 6 Then fontSize = 6
                        Using autoFont = New Font(TextFont.FontFamily, fontSize)
                            ' Draw text within padded area
                            'gr.DrawString(Text, TextFont, brush, textRect, format)
                            gr.DrawString(Text, autoFont, brush, textRect, format)
                        End Using
                        'gr.DrawString(Text, TextFont, brush, textRect, format)
                    End Using
                End If
            End If
        Finally
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
    Public Overrides Function IsAt(x As Integer, y As Integer) As Boolean
        Dim rect = GetBounds()
        Dim path As New GraphicsPath()
        path.AddRectangle(rect)

        Dim center As New PointF(rect.Left + rect.Width / 2.0F, rect.Top + rect.Height / 2.0F)

        Dim matrix As New Matrix()
        matrix.Translate(center.X, center.Y)
        matrix.Scale(ScaleX, ScaleY)
        matrix.Rotate(RotationAngle)
        matrix.Translate(-center.X, -center.Y)

        path.Transform(matrix)

        Return path.IsVisible(New PointF(x, y))
    End Function

    Public Overrides Function GetBounds() As Rectangle
        Return New Rectangle(
            Math.Min(X1, X2),
            Math.Min(Y1, Y2),
            Math.Abs(X2 - X1),
            Math.Abs(Y2 - Y1)
        )
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
            Case "ns", "sn"
                scaleXFactor = 1.0F ' Lock horizontal
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
