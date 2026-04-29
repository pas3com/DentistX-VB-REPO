Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Xml.Serialization

<Serializable()>
Public Class DrawableTextN
    Inherits DrawableN

    ' Text content
    Public Property Text As String = "Sample Text"

    ' Font properties
    <XmlIgnore>
    Public Property Font As New Font("Arial", 12, FontStyle.Regular)

    ' For XML serialization of Font
    <XmlElement("FontName")>
    Public Property FontName As String
        Get
            Return Font.Name
        End Get
        Set(value As String)
            Font = New Font(value, Font.Size, Font.Style)
        End Set
    End Property

    <XmlElement("FontSize")>
    Public Property FontSize As Single
        Get
            Return Font.Size
        End Get
        Set(value As Single)
            Font = New Font(Font.Name, value, Font.Style)
        End Set
    End Property

    <XmlElement("FontStyle")>
    Public Property FontStyleValue As FontStyle
        Get
            Return Font.Style
        End Get
        Set(value As FontStyle)
            Font = New Font(Font.Name, Font.Size, value)
        End Set
    End Property

    ' Text color
    ' ===== FIX 1: TextColor Serialization =====
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

    ' Text alignment
    Public Property Alignment As StringAlignment = StringAlignment.Near
    Public Property VerticalAlignment As StringAlignment = StringAlignment.Near

    ' Rotation angle in degrees
    'Public Overloads Property RotationAngle As Single = 0

    Public Sub New()
        MyBase.New(Color.Black, Color.Transparent, 0)
        RotationAngle = 0  ' Set base property value here if needed
    End Sub

    Public Sub New(foreColor As Color, font As Font, text As String,
                   Optional ByVal x1 As Integer = 0, Optional ByVal y1 As Integer = 0,
                   Optional ByVal x2 As Integer = 1, Optional ByVal y2 As Integer = 1)
        MyBase.New(foreColor, Color.Transparent, 0)
        Me.Text = text
        Me.Font = font
        Me.X1 = x1
        Me.Y1 = y1
        Me.X2 = x2
        Me.Y2 = y2
        UpdateRotationCenter()
    End Sub

    ' Add this after loading
    Public Overrides Sub AfterLoad()
        MyBase.AfterLoad()
        ' Reset rotation center to shape center
        Dim rect = GetBounds()
        RotationCenter = New PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2)
    End Sub

    Public Overrides Sub Draw(gr As Graphics)
        ' Save graphics state
        Dim container As GraphicsContainer = gr.BeginContainer()
        Try
            ApplyTransformations(gr)
            Dim rect = GetNormalizedBounds()
            ' Validate bounds
            If rect.Width <= 0 OrElse rect.Height <= 0 Then Return
            rect = New Rectangle(rect.X, rect.Y, Math.Max(1, rect.Width), Math.Max(1, rect.Height))
            ' Draw placeholder or text
            If String.IsNullOrEmpty(Text) Then
                Using pen As New Pen(Color.Gray)
                    pen.DashStyle = DashStyle.Dash
                    gr.DrawRectangle(pen, rect)
                End Using
            Else
                Using brush As New SolidBrush(Me.ForeColor)
                    gr.DrawString(Text, Font, brush, rect)
                End Using
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


    'Public Overrides Sub Draw(gr As Graphics)
    '    ' Save graphics state
    '    Dim container As GraphicsContainer = gr.BeginContainer()

    '    Try
    '        ApplyTransformations(gr)
    '        Dim rect = GetNormalizedBounds()

    '        ' Validate bounds
    '        If rect.Width <= 0 OrElse rect.Height <= 0 Then Return
    '        rect = New Rectangle(rect.X, rect.Y, Math.Max(1, rect.Width), Math.Max(1, rect.Height))

    '        ' Draw placeholder or text
    '        If String.IsNullOrEmpty(Text) Then
    '            Using pen As New Pen(Color.Gray)
    '                pen.DashStyle = DashStyle.Dash
    '                gr.DrawRectangle(pen, rect)
    '            End Using
    '        Else
    '            Using brush As New SolidBrush(Me.ForeColor)
    '                gr.DrawString(Text, Font, brush, rect)
    '            End Using
    '        End If
    '    Finally
    '        gr.EndContainer(container)
    '    End Try

    '    ' Draw selection indicators
    '    If IsSelected OrElse IsTransforming Then
    '        Dim borderColor = If(IsTransforming, Color.Cyan, Color.Yellow)
    '        Using pen As New Pen(borderColor, 2)
    '            pen.DashStyle = If(IsTransforming, DashStyle.Dash, DashStyle.Solid)
    '            gr.DrawRectangle(pen, GetNormalizedBounds())
    '        End Using
    '    End If

    '    DrawAnchors(gr)
    'End Sub

    Public Overrides Sub Render(gr As Graphics, dr As DrawableN)
        'Draw(gr)
    End Sub

    Public Overrides Function GetBounds() As Rectangle
        Return GetNormalizedBounds()
    End Function

    Public Sub SetBounds(rect As Rectangle)
        X1 = rect.Left
        Y1 = rect.Top
        X2 = rect.Right
        Y2 = rect.Bottom
    End Sub

    Private Function GetNormalizedBounds() As Rectangle
        ' Create normalized rectangle regardless of drag direction
        Return New Rectangle(
            Math.Min(X1, X2),
            Math.Min(Y1, Y2),
            Math.Abs(X2 - X1),
            Math.Abs(Y2 - Y1))
    End Function


    Public Overrides Function IsAt(x As Integer, y As Integer) As Boolean
        Dim rect = GetNormalizedBounds()
        Using path As New GraphicsPath()
            path.AddRectangle(rect)

            ' Apply inverse rotation for hit testing
            Dim matrix As New Matrix()
            matrix.RotateAt(-RotationAngle, RotationCenter)
            path.Transform(matrix)

            Return path.IsVisible(x, y)
        End Using
    End Function

    Public Overrides Sub NewPoint(x As Integer, y As Integer)
        X2 = x
        Y2 = y
        UpdateRotationCenter()
    End Sub

    Public Overrides Function IsEmpty() As Boolean
        Return String.IsNullOrWhiteSpace(Text)
    End Function

    Public Overrides Sub MoveRelative(dx As Integer, dy As Integer)
        X1 += dx
        Y1 += dy
        X2 += dx
        Y2 += dy
        UpdateRotationCenter()
    End Sub

    Private Sub UpdateRotationCenter()
        Dim rect = GetNormalizedBounds()
        RotationCenter = New PointF(
            rect.X + rect.Width / 2,
            rect.Y + rect.Height / 2)
    End Sub

    ' Called when text is updated to auto-size if needed
    Public Sub AdjustSizeToText1(gr As Graphics)
        If String.IsNullOrEmpty(Text) OrElse Font Is Nothing Then Return

        Dim size = gr.MeasureString(Text, Font)
        Dim rect = GetNormalizedBounds()

        ' Only adjust height, preserve width
        Y2 = Y1 + Math.Abs(Y2 - Y1) ' Maintain Y direction
        X2 = X1 + Math.Sign(X2 - X1) * rect.Width ' Maintain X direction
        Y2 = Y1 + Math.Sign(Y2 - Y1) * CInt(size.Height)
    End Sub

    Public Sub AdjustSizeToText(g As Graphics)
        If String.IsNullOrEmpty(Me.Text) Then Return

        Dim maxWidth = 2000 ' Allow wide line wrapping
        Dim layoutSize As New SizeF(maxWidth, 10000)

        Dim textSize As SizeF = g.MeasureString(Me.Text, Me.Font, layoutSize.Width)

        Dim newWidth = Math.Ceiling(textSize.Width) + 10
        Dim newHeight = Math.Ceiling(textSize.Height) + 5

        ' Keep the top-left corner fixed
        Dim rect = Me.GetNormalizedBounds()
        X2 = X1 + CInt(newWidth)
        Y2 = Y1 + CInt(newHeight)
    End Sub

    Public Overrides Function GetScreenBounds(gr As Graphics) As RectangleF
        Dim path As New GraphicsPath()
        path.AddRectangle(Me.GetBounds()) ' Or custom logic if you override it
        Dim matrix = gr.Transform.Clone()
        path.Transform(matrix)
        Return path.GetBounds()
    End Function

End Class
