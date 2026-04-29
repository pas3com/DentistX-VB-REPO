Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Xml.Serialization

<Serializable()>
Public Class DrawableArrowN
    Inherits DrawableN

    Public Sub New()
    End Sub
    Public Sub New(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer)
        MyBase.New()
        Me.X1 = x1
        Me.Y1 = y1
        Me.X2 = x2
        Me.Y2 = y2
    End Sub
    Public Sub New(ByVal fore_color As Color, fillColor As Color, Optional ByVal line_width As Integer = 0,
                  Optional ByVal new_x1 As Integer = 0, Optional ByVal new_y1 As Integer = 0,
                  Optional ByVal new_x2 As Integer = 1, Optional ByVal new_y2 As Integer = 1)
        MyBase.New(fore_color, Nothing, line_width)
        X1 = new_x1
        Y1 = new_y1
        X2 = new_x2
        Y2 = new_y2
    End Sub
#Region "Type Text"
    ' ===== TEXT PROPERTIES =====
    Private _text As String = String.Empty
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
    <XmlIgnore> Public Property TextColor As Color = Color.Black

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
    ' Arrow dimensions
    Property HeadLength As Integer
    Property HeadWidth As Integer
    Property ShaftWidthBase As Integer
    Property ShaftWidthTip As Integer
    ' Add this after loading
    Public Overrides Sub AfterLoad()
        MyBase.AfterLoad()
        ' Reset rotation center to shape center
        Dim rect = GetBounds()
        RotationCenter = New PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2)
    End Sub
    Public Overrides Sub Draw(gr As Graphics)
        gr.SmoothingMode = SmoothingMode.AntiAlias
        gr.PixelOffsetMode = PixelOffsetMode.HighQuality
        gr.CompositingQuality = CompositingQuality.HighQuality
        Dim dx = X2 - X1
        Dim dy = Y2 - Y1
        Dim length = Math.Sqrt(dx * dx + dy * dy)
        If length = 0 Then
            If IsSelected Then DrawArrowSelectionOutline(gr)
            Return
        End If
        Dim container As GraphicsContainer = gr.BeginContainer()
        Try
            ApplyTransformations(gr)
        Dim angle = Math.Atan2(dy, dx)
        ' Unit vector
        Dim ux = CSng(dx / length)
        Dim uy = CSng(dy / length)
        Dim px = -uy
        Dim py = ux
        ' Points
        Dim ptTip As New PointF(X2, Y2)
        Dim ptHeadBase As New PointF(X2 - _HeadLength * ux, Y2 - _HeadLength * uy)
        Dim ptHeadLeft As New PointF(ptHeadBase.X + (_HeadWidth / 2) * px, ptHeadBase.Y + (_HeadWidth / 2) * py)
        Dim ptHeadRight As New PointF(ptHeadBase.X - (_HeadWidth / 2) * px, ptHeadBase.Y - (_HeadWidth / 2) * py)
        Dim ptShaftLeft As New PointF(X1 + (_ShaftWidthBase / 2) * px, Y1 + (_ShaftWidthBase / 2) * py)
        Dim ptShaftRight As New PointF(X1 - (_ShaftWidthBase / 2) * px, Y1 - (_ShaftWidthBase / 2) * py)
        Dim ptShaftLeftTip As New PointF(ptHeadBase.X + (_ShaftWidthTip / 2) * px, ptHeadBase.Y + (_ShaftWidthTip / 2) * py)
        Dim ptShaftRightTip As New PointF(ptHeadBase.X - (_ShaftWidthTip / 2) * px, ptHeadBase.Y - (_ShaftWidthTip / 2) * py)
        ' Draw arrow outline
        Using path As New Drawing2D.GraphicsPath()
            path.StartFigure()
            path.AddLine(ptShaftLeft, ptShaftLeftTip)
            path.AddLine(ptShaftLeftTip, ptHeadLeft)
            path.AddLine(ptHeadLeft, ptTip)
            path.AddLine(ptTip, ptHeadRight)
            path.AddLine(ptHeadRight, ptShaftRightTip)
            path.AddLine(ptShaftRightTip, ptShaftRight)
            path.CloseFigure()
            ' Fill first
            Using brush As New SolidBrush(FillColor)
                gr.FillPath(brush, path)
            End Using

            ' Then outline
            Using pen As New Pen(ForeColor, LineWidth)
                gr.DrawPath(pen, path)
            End Using
            'Using pen As New Pen(ForeColor, LineWidth)
            '    gr.DrawPath(pen, path)
            'End Using
        End Using
        ' ===== TEXT INSIDE SHAFT =====
        ' ===== ROTATED TEXT INSIDE ARROW SHAFT =====
        If Not String.IsNullOrEmpty(Text) Then
            Dim textLength = CSng(Math.Sqrt((ptHeadBase.X - X1) ^ 2 + (ptHeadBase.Y - Y1) ^ 2))
            Dim textWidth = textLength
            Dim textHeight = _ShaftWidthBase
            Dim centerX = (X1 + ptHeadBase.X) / 2
            Dim centerY = (Y1 + ptHeadBase.Y) / 2
            ' Text rectangle relative to rotation center
            Dim localRect As New RectangleF(
        -textWidth / 2 + TextPadding,
        -textHeight / 2 + TextPadding,
        textWidth - 2 * TextPadding,
        textHeight - 2 * TextPadding)
            If localRect.Width > 0 AndAlso localRect.Height > 0 Then
                Using format As New StringFormat(),
                  brush As New SolidBrush(TextColor)
                    ' Alignment
                    format.Alignment = StringAlignment.Center
                    format.LineAlignment = StringAlignment.Center
                    format.FormatFlags = StringFormatFlags.LineLimit
                    format.Trimming = StringTrimming.EllipsisWord
                    ' Auto font size
                    Dim fontSize = Math.Min(localRect.Width / 10, localRect.Height)
                    If fontSize < 6 Then fontSize = 6
                    Using autoFont As New Font(TextFont.FontFamily, fontSize, TextFont.Style)
                        ' Save and rotate graphics context
                        Dim state = gr.Save()
                        gr.TranslateTransform(centerX, centerY)
                        gr.RotateTransform(CSng(angle * 180 / Math.PI)) ' Convert radians to degrees
                        ' Draw string in rotated space
                        gr.DrawString(Text, autoFont, brush, localRect, format)
                        gr.Restore(state)
                    End Using
                End Using
            End If
        End If
        Finally
            gr.EndContainer(container)
        End Try
        If IsSelected Then DrawArrowSelectionOutline(gr)
    End Sub

    Private Sub DrawArrowSelectionOutline(gr As Graphics)
        Dim rectF As RectangleF = GetTransformedBounds()
        Dim r As New Rectangle(CInt(rectF.X), CInt(rectF.Y), CInt(rectF.Width), CInt(rectF.Height))
        Using selPen As New Pen(Color.Yellow, 1)
            selPen.DashStyle = DashStyle.Dot
            gr.DrawRectangle(selPen, r)
        End Using
    End Sub
    Public Overrides Sub Render(gr As Graphics, dr As DrawableN)
    End Sub
    Public Overrides Function GetBounds() As Rectangle
        Dim dx = X2 - X1
        Dim dy = Y2 - Y1
        Dim length = Math.Sqrt(dx * dx + dy * dy)
        If length = 0 Then Return New Rectangle(X1, Y1, 1, 1)

        Dim ux = CSng(dx / length)
        Dim uy = CSng(dy / length)
        Dim px = -uy
        Dim py = ux

        ' Head base
        Dim ptHeadBaseX = X2 - HeadLength * ux
        Dim ptHeadBaseY = Y2 - HeadLength * uy

        ' Shaft and head points
        Dim points As PointF() = {
        New PointF(X1 + (ShaftWidthBase / 2) * px, Y1 + (ShaftWidthBase / 2) * py),
        New PointF(X1 - (ShaftWidthBase / 2) * px, Y1 - (ShaftWidthBase / 2) * py),
        New PointF(ptHeadBaseX + (ShaftWidthTip / 2) * px, ptHeadBaseY + (ShaftWidthTip / 2) * py),
        New PointF(ptHeadBaseX - (ShaftWidthTip / 2) * px, ptHeadBaseY - (ShaftWidthTip / 2) * py),
        New PointF(ptHeadBaseX + (HeadWidth / 2) * px, ptHeadBaseY + (HeadWidth / 2) * py),
        New PointF(ptHeadBaseX - (HeadWidth / 2) * px, ptHeadBaseY - (HeadWidth / 2) * py),
        New PointF(X2, Y2)
    }

        ' Find bounds from all points
        Dim minX = points.Min(Function(p) p.X)
        Dim maxX = points.Max(Function(p) p.X)
        Dim minY = points.Min(Function(p) p.Y)
        Dim maxY = points.Max(Function(p) p.Y)

        Return Rectangle.FromLTRB(CInt(Math.Floor(minX)), CInt(Math.Floor(minY)),
                              CInt(Math.Ceiling(maxX)), CInt(Math.Ceiling(maxY)))
    End Function

    Public Function GetBounds1() As Rectangle
        Dim x = Math.Min(X1, X2)
        Dim y = Math.Min(Y1, Y2)
        Dim width = Math.Abs(X2 - X1)
        Dim height = Math.Abs(Y2 - Y1)
        Return New Rectangle(x, y, width, height)
    End Function
    Public Overrides Function IsAt(x As Integer, y As Integer) As Boolean
        Const tolerance As Integer = 5
        Dim p = InverseTransformPoint(New PointF(x, y))
        Dim dx = X2 - X1
        Dim dy = Y2 - Y1
        If dx = 0 AndAlso dy = 0 Then
            Return Math.Abs(p.X - X1) <= tolerance AndAlso Math.Abs(p.Y - Y1) <= tolerance
        End If
        Dim lengthSq = dx * dx + dy * dy
        Dim t = ((p.X - X1) * dx + (p.Y - Y1) * dy) / lengthSq
        t = Math.Max(0, Math.Min(1, t))
        Dim closestX = X1 + t * dx
        Dim closestY = Y1 + t * dy
        Dim dist = Math.Sqrt((p.X - closestX) ^ 2 + (p.Y - closestY) ^ 2)
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
    Public Overrides Function GetScreenBounds(gr As Graphics) As RectangleF
        Dim path As New GraphicsPath()
        path.AddRectangle(Me.GetBounds()) ' Or custom logic if you override it
        Dim matrix = gr.Transform.Clone()
        path.Transform(matrix)
        Return path.GetBounds()
    End Function
End Class
