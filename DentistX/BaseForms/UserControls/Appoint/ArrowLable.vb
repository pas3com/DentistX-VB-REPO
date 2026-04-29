Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Windows.Forms


<ToolboxItem(True)>
Public Class ArrowLable
    Inherits Label

    Public Enum ArrowTextDirection
        Horizontal = 0
        TopToBottom = 1
        BottomToTop = 2
    End Enum

    Private _UseSimpleLabel As Boolean = False
    Private _showLeftTriangle As Boolean = True
    Private _showRightTriangle As Boolean = True
    Private _textDirection As ArrowTextDirection = ArrowTextDirection.Horizontal
    Private _triangleSize As Integer = 10
    Private _cornerRadius As Integer = 8
    Private _glassBaseColor As Color = Color.FromArgb(90, 180, 255)
    Private _glassAccentColor As Color = Color.FromArgb(170, 90, 255)
    Private _glassHighlightColor As Color = Color.FromArgb(220, 255, 255, 255)
    Private _borderColor As Color = Color.FromArgb(80, 20, 60, 110)
    Private _triangleColor As Color = Color.FromArgb(220, 255, 140, 120)

    <Category("Arrow"), Description("Show triangle on the left edge."), DefaultValue(True)>
    Public Property ShowLeftTriangle As Boolean
        Get
            Return _showLeftTriangle
        End Get
        Set(value As Boolean)
            If _showLeftTriangle = value Then Return
            _showLeftTriangle = value
            Invalidate()
        End Set
    End Property

    <Category("Arrow"), Description("Show triangle on the right edge."), DefaultValue(True)>
    Public Property ShowRightTriangle As Boolean
        Get
            Return _showRightTriangle
        End Get
        Set(value As Boolean)
            If _showRightTriangle = value Then Return
            _showRightTriangle = value
            Invalidate()
        End Set
    End Property

    <Category("Arrow"), Description("When true, draws a flat background (no shine or glow)."), DefaultValue(False)>
    Public Property UseSimpleLabel As Boolean
        Get
            Return _UseSimpleLabel
        End Get
        Set(value As Boolean)
            If _UseSimpleLabel = value Then Return
            _UseSimpleLabel = value
            Invalidate()
        End Set
    End Property

    <Category("Arrow"), Description("Triangle size in pixels."), DefaultValue(10)>
    Public Property TriangleSize As Integer
        Get
            Return _triangleSize
        End Get
        Set(value As Integer)
            value = Math.Max(4, value)
            If _triangleSize = value Then Return
            _triangleSize = value
            Invalidate()
        End Set
    End Property

    <Category("Appearance"), Description("Text drawing direction."), DefaultValue(GetType(ArrowTextDirection), "Horizontal")>
    Public Property TextDirection As ArrowTextDirection
        Get
            Return _textDirection
        End Get
        Set(value As ArrowTextDirection)
            If _textDirection = value Then Return
            _textDirection = value
            Invalidate()
        End Set
    End Property

    Private _textAngle As Single = 0.0F
    ''' <summary>Rotation angle in degrees. 0 = horizontal. -90 = bottom-to-top, 90 = top-to-bottom. Set at design time or in code.</summary>
    <Category("Appearance"), Description("Text rotation angle in degrees (0 = horizontal, -90 = bottom-to-top, 90 = top-to-bottom)."), DefaultValue(0.0F)>
    Public Property TextAngle As Single
        Get
            Return _textAngle
        End Get
        Set(value As Single)
            If Math.Abs(_textAngle - value) < 0.01F Then Return
            _textAngle = value
            Invalidate()
        End Set
    End Property

    <Category("Appearance"), Description("Corner radius for the glass background."), DefaultValue(8)>
    Public Property CornerRadius As Integer
        Get
            Return _cornerRadius
        End Get
        Set(value As Integer)
            value = Math.Max(0, value)
            If _cornerRadius = value Then Return
            _cornerRadius = value
            Invalidate()
        End Set
    End Property

    <Category("Appearance"), Description("Base color for the glass background.")>
    Public Property GlassBaseColor As Color
        Get
            Return _glassBaseColor
        End Get
        Set(value As Color)
            If _glassBaseColor = value Then Return
            _glassBaseColor = value
            Invalidate()
        End Set
    End Property

    <Category("Appearance"), Description("Accent glow color for the glass background.")>
    Public Property GlassAccentColor As Color
        Get
            Return _glassAccentColor
        End Get
        Set(value As Color)
            If _glassAccentColor = value Then Return
            _glassAccentColor = value
            Invalidate()
        End Set
    End Property

    <Category("Appearance"), Description("Highlight color for the glass shine.")>
    Public Property GlassHighlightColor As Color
        Get
            Return _glassHighlightColor
        End Get
        Set(value As Color)
            If _glassHighlightColor = value Then Return
            _glassHighlightColor = value
            Invalidate()
        End Set
    End Property

    <Category("Appearance"), Description("Border color for the control.")>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            If _borderColor = value Then Return
            _borderColor = value
            Invalidate()
        End Set
    End Property

    <Category("Appearance"), Description("Triangle fill color.")>
    Public Property TriangleColor As Color
        Get
            Return _triangleColor
        End Get
        Set(value As Color)
            If _triangleColor = value Then Return
            _triangleColor = value
            Invalidate()
        End Set
    End Property

    <Category("Action"), Description("Raised when the arrow label is clicked.")>
    Public Event ArrowClicked As EventHandler(Of MouseEventArgs)

    Private _toolTip As ToolTip

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        DoubleBuffered = True
        AutoSize = False
        MinimumSize = New Size(1, 1)
        TextAlign = ContentAlignment.MiddleCenter
        ForeColor = Color.White
        Font = New Font("Segoe UI", 9, FontStyle.Bold)
        Size = New Size(160, 44)
    End Sub

    Protected Overrides Sub SetBoundsCore(x As Integer, y As Integer, width As Integer, height As Integer, specified As BoundsSpecified)
        width = Math.Max(1, width)
        height = Math.Max(1, height)
        MyBase.SetBoundsCore(x, y, width, height, specified)
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        If _toolTip Is Nothing Then
            _toolTip = New ToolTip() With {
                .AutoPopDelay = 5000,
                .InitialDelay = 300,
                .ReshowDelay = 100,
                .ShowAlways = True
            }
        End If
        UpdateToolTipText()
    End Sub

    Private Sub UpdateToolTipText()
        If _toolTip Is Nothing Then Return
        If String.IsNullOrEmpty(Text) Then
            _toolTip.SetToolTip(Me, Nothing)
        Else
            _toolTip.SetToolTip(Me, Text)
        End If
    End Sub

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e)
        UpdateToolTipText()
        Invalidate()
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso _toolTip IsNot Nothing Then
            _toolTip.Dispose()
            _toolTip = Nothing
        End If
        MyBase.Dispose(disposing)
    End Sub

    Protected Overrides Sub OnFontChanged(e As EventArgs)
        MyBase.OnFontChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnForeColorChanged(e As EventArgs)
        MyBase.OnForeColorChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)
        RaiseEvent ArrowClicked(Me, e)
    End Sub

    ''' <summary>Paints the arrow label: glass or flat background, optional shine and glow, border, optional triangles, and text (horizontal or rotated).</summary>
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        ' Client area and early exit if too small
        Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
        If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

        ' Background rect: full area minus space for left/right triangles
        Dim leftInset As Integer = If(_showLeftTriangle, _triangleSize, 0)
        Dim rightInset As Integer = If(_showRightTriangle, _triangleSize, 0)
        Dim bgW As Integer = rect.Width - leftInset - rightInset
        Dim bgH As Integer = rect.Height
        If bgW <= 0 OrElse bgH <= 0 Then Return
        Dim bgRect As New Rectangle(rect.X + leftInset, rect.Y, bgW, bgH)

        Using bgPath = GetRoundedRectPath(bgRect, _cornerRadius)
            If _UseSimpleLabel Then
                Using bgBrush As New SolidBrush(_glassBaseColor)
                    e.Graphics.FillPath(bgBrush, bgPath)
                End Using
            Else
                Using bgBrush As New LinearGradientBrush(bgRect,
                                                       Color.FromArgb(190, _glassBaseColor),
                                                       Color.FromArgb(140, ControlPaint.Dark(_glassBaseColor)),
                                                       LinearGradientMode.Vertical)
                    e.Graphics.FillPath(bgBrush, bgPath)
                End Using

                ' Shine strip (top portion of bg, lighter gradient)
                Dim shineRect As New Rectangle(bgRect.X + 1, bgRect.Y + 1, bgRect.Width - 2, CInt(bgRect.Height * 0.45))
                Using shinePath = GetRoundedRectPath(shineRect, _cornerRadius)
                    Using shineBrush As New LinearGradientBrush(shineRect,
                                                                Color.FromArgb(160, _glassHighlightColor),
                                                                Color.FromArgb(30, _glassHighlightColor),
                                                                LinearGradientMode.Vertical)
                        e.Graphics.FillPath(shineBrush, shinePath)
                    End Using
                End Using

                ' Glow ellipse: centered in the label (same center used later for text rotation)
                Dim glowRect As New Rectangle(bgRect.X + bgRect.Width \ 4, bgRect.Y + bgRect.Height \ 3, bgRect.Width \ 2, bgRect.Height \ 2)
                Using glowBrush As New SolidBrush(Color.FromArgb(70, _glassAccentColor))
                    e.Graphics.FillEllipse(glowBrush, glowRect)
                End Using
            End If

            ' Border around the rounded background
            Using borderPen As New Pen(_borderColor)
                e.Graphics.DrawPath(borderPen, bgPath)
            End Using
        End Using

        ' Left triangle (arrow nub) if enabled
        Dim midY As Integer = rect.Y + (rect.Height \ 2)
        If _showLeftTriangle Then
            Dim leftBaseX As Integer = bgRect.Left
            Dim leftPts() As Point = {
                New Point(leftBaseX - _triangleSize, midY),
                New Point(leftBaseX, midY - _triangleSize),
                New Point(leftBaseX, midY + _triangleSize)
            }
            Using br As New SolidBrush(_triangleColor)
                e.Graphics.FillPolygon(br, leftPts)
            End Using
            Using pn As New Pen(_borderColor)
                e.Graphics.DrawPolygon(pn, leftPts)
            End Using
        End If

        ' Right triangle (arrow nub) if enabled
        If _showRightTriangle Then
            Dim rightBaseX As Integer = bgRect.Right
            Dim rightPts() As Point = {
                New Point(rightBaseX + _triangleSize, midY),
                New Point(rightBaseX, midY - _triangleSize),
                New Point(rightBaseX, midY + _triangleSize)
            }
            Using br As New SolidBrush(_triangleColor)
                e.Graphics.FillPolygon(br, rightPts)
            End Using
            Using pn As New Pen(_borderColor)
                e.Graphics.DrawPolygon(pn, rightPts)
            End Using
        End If

        ' Text rectangle = strip between the two triangles, on the surface of the label (small padding so text does not touch edges)
        Const TextPad As Integer = 4
        Dim textRect As New Rectangle(leftInset + TextPad, TextPad, rect.Width - leftInset - rightInset - 2 * TextPad, rect.Height - 2 * TextPad)

        ' Rotation anchor = alignment-aware point within textRect
        ' e.g. center for MiddleCenter, top-left for TopLeft, bottom-right for BottomRight, etc.
        Dim rotAnchor As PointF = GetAlignmentAnchor(TextAlign, New RectangleF(textRect.X, textRect.Y, textRect.Width, textRect.Height))
        If textRect.Width > 0 AndAlso textRect.Height > 0 Then
            Dim drawText As String = If(String.IsNullOrEmpty(Text), String.Empty, Text.Trim())
            Dim angle As Single = _textAngle
            If Math.Abs(angle) < 0.01F Then
                Select Case _textDirection
                    Case ArrowTextDirection.TopToBottom : angle = 90.0F
                    Case ArrowTextDirection.BottomToTop : angle = -90.0F
                End Select
            End If
            If Math.Abs(angle) >= 0.01F Then
                DrawTextRotatedPath(e.Graphics, drawText, textRect, angle, rotAnchor.X, rotAnchor.Y)
            Else
                Using format As StringFormat = GetStringFormat(TextAlign)
                    format.Trimming = If(AutoEllipsis, StringTrimming.EllipsisCharacter, StringTrimming.None)
                    Using br As New SolidBrush(ForeColor)
                        e.Graphics.DrawString(drawText, Font, br, textRect, format)
                    End Using
                End Using
            End If
        End If
    End Sub

    ''' <summary>Draws text at any angle. Rotation is around the alignment anchor of the text layout;
    ''' that anchor is then placed at (labelCenterX, labelCenterY) which is the alignment-aware
    ''' point within the text rectangle. Clips output to textRect.</summary>
    Private Sub DrawTextRotatedPath(g As Graphics, drawText As String, textRect As Rectangle, angleDegrees As Single, labelCenterX As Single, labelCenterY As Single)
        If String.IsNullOrEmpty(drawText) OrElse Font Is Nothing Then Return
        Dim rw As Single = Math.Max(1, textRect.Width)
        Dim rh As Single = Math.Max(1, textRect.Height)

        ' For near-±90° rotation the text reads along the height, so swap layout
        ' dimensions: the long axis (height) becomes the layout width the text flows
        ' along, and the short axis (width) becomes the layout height (line thickness).
        Dim layoutW As Single = rw
        Dim layoutH As Single = rh
        If Math.Abs(Math.Abs(angleDegrees) - 90.0F) < 1.0F Then
            layoutW = rh
            layoutH = rw
        End If

        ' Font size in pixels; cap so text fits in the available perpendicular space
        Dim emSize As Single = Font.Size * g.DpiY / 72.0F
        Dim maxEm As Single = layoutH * 0.85F
        If maxEm > 0 AndAlso emSize > maxEm Then emSize = maxEm
        If emSize < 6.0F Then emSize = 6.0F
        Using fmt As StringFormat = GetStringFormat(TextAlign)
            fmt.Trimming = If(AutoEllipsis, StringTrimming.EllipsisCharacter, StringTrimming.None)
            Using path As New GraphicsPath()
                path.AddString(drawText, Font.FontFamily, CInt(Font.Style), emSize, New RectangleF(0, 0, layoutW, layoutH), fmt)
                ' Compute the alignment anchor within the (possibly swapped) layout rect
                Dim pathAnchor As PointF = GetAlignmentAnchor(TextAlign, New RectangleF(0, 0, layoutW, layoutH))
                ' Rotate around the alignment anchor, then translate so the anchor lands at the target position
                Using mat As New Matrix()
                    mat.RotateAt(angleDegrees, pathAnchor)
                    mat.Translate(labelCenterX - pathAnchor.X, labelCenterY - pathAnchor.Y, MatrixOrder.Append)
                    path.Transform(mat)
                End Using
                ' Clip to text rect so overflow is hidden, then fill the path
                Dim prevClip As Region = g.Clip.Clone()
                Try
                    g.SetClip(textRect)
                    Using br As New SolidBrush(ForeColor)
                        g.FillPath(br, path)
                    End Using
                Finally
                    g.Clip = prevClip
                    prevClip.Dispose()
                End Try
            End Using
        End Using
    End Sub

    ''' <summary>Returns a StringFormat that matches the control's TextAlign (ContentAlignment) for use with DrawString or GraphicsPath.AddString.</summary>
    Private Function GetStringFormat(align As ContentAlignment) As StringFormat
        Dim fmt As New StringFormat() With {.FormatFlags = StringFormatFlags.LineLimit}
        Select Case align
            Case ContentAlignment.TopLeft
                fmt.Alignment = StringAlignment.Near
                fmt.LineAlignment = StringAlignment.Near
            Case ContentAlignment.TopCenter
                fmt.Alignment = StringAlignment.Center
                fmt.LineAlignment = StringAlignment.Near
            Case ContentAlignment.TopRight
                fmt.Alignment = StringAlignment.Far
                fmt.LineAlignment = StringAlignment.Near
            Case ContentAlignment.MiddleLeft
                fmt.Alignment = StringAlignment.Near
                fmt.LineAlignment = StringAlignment.Center
            Case ContentAlignment.MiddleCenter
                fmt.Alignment = StringAlignment.Center
                fmt.LineAlignment = StringAlignment.Center
            Case ContentAlignment.MiddleRight
                fmt.Alignment = StringAlignment.Far
                fmt.LineAlignment = StringAlignment.Center
            Case ContentAlignment.BottomLeft
                fmt.Alignment = StringAlignment.Near
                fmt.LineAlignment = StringAlignment.Far
            Case ContentAlignment.BottomCenter
                fmt.Alignment = StringAlignment.Center
                fmt.LineAlignment = StringAlignment.Far
            Case ContentAlignment.BottomRight
                fmt.Alignment = StringAlignment.Far
                fmt.LineAlignment = StringAlignment.Far
        End Select
        Return fmt
    End Function

    ''' <summary>Returns the anchor point within a rectangle for a given ContentAlignment.
    ''' TopLeft → top-left corner, MiddleCenter → center, BottomRight → bottom-right corner, etc.</summary>
    Private Shared Function GetAlignmentAnchor(align As ContentAlignment, rect As RectangleF) As PointF
        Dim x As Single
        Dim y As Single
        ' Horizontal component
        Select Case align
            Case ContentAlignment.TopLeft, ContentAlignment.MiddleLeft, ContentAlignment.BottomLeft
                x = rect.Left
            Case ContentAlignment.TopCenter, ContentAlignment.MiddleCenter, ContentAlignment.BottomCenter
                x = rect.Left + rect.Width / 2.0F
            Case Else ' TopRight, MiddleRight, BottomRight
                x = rect.Right
        End Select
        ' Vertical component
        Select Case align
            Case ContentAlignment.TopLeft, ContentAlignment.TopCenter, ContentAlignment.TopRight
                y = rect.Top
            Case ContentAlignment.MiddleLeft, ContentAlignment.MiddleCenter, ContentAlignment.MiddleRight
                y = rect.Top + rect.Height / 2.0F
            Case Else ' BottomLeft, BottomCenter, BottomRight
                y = rect.Bottom
        End Select
        Return New PointF(x, y)
    End Function

    ''' <summary>Builds a GraphicsPath for a rectangle with rounded corners (four quarter-circle arcs). Used for the glass background and shine shapes.</summary>
    Private Function GetRoundedRectPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        If radius <= 0 Then
            path.AddRectangle(rect)
            Return path
        End If
        Dim d As Integer = radius * 2
        path.AddArc(rect.X, rect.Y, d, d, 180, 90)
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function
End Class



'<ToolboxItem(True)>
'Public Class ArrowLable
'    Inherits Label

'    Public Enum ArrowTextDirection
'        Horizontal = 0
'        TopToBottom = 1
'        BottomToTop = 2
'    End Enum

'    Private _showLeftTriangle As Boolean = True
'    Private _showRightTriangle As Boolean = True
'    Private _textDirection As ArrowTextDirection = ArrowTextDirection.Horizontal
'    Private _triangleSize As Integer = 10
'    Private _cornerRadius As Integer = 8
'    Private _glassBaseColor As Color = Color.FromArgb(90, 180, 255)
'    Private _glassAccentColor As Color = Color.FromArgb(170, 90, 255)
'    Private _glassHighlightColor As Color = Color.FromArgb(220, 255, 255, 255)
'    Private _borderColor As Color = Color.FromArgb(80, 20, 60, 110)
'    Private _triangleColor As Color = Color.FromArgb(220, 255, 140, 120)

'    <Category("Arrow"), Description("Show triangle on the left edge."), DefaultValue(True)>
'    Public Property ShowLeftTriangle As Boolean
'        Get
'            Return _showLeftTriangle
'        End Get
'        Set(value As Boolean)
'            If _showLeftTriangle = value Then Return
'            _showLeftTriangle = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Arrow"), Description("Show triangle on the right edge."), DefaultValue(True)>
'    Public Property ShowRightTriangle As Boolean
'        Get
'            Return _showRightTriangle
'        End Get
'        Set(value As Boolean)
'            If _showRightTriangle = value Then Return
'            _showRightTriangle = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Arrow"), Description("Triangle size in pixels."), DefaultValue(10)>
'    Public Property TriangleSize As Integer
'        Get
'            Return _triangleSize
'        End Get
'        Set(value As Integer)
'            value = Math.Max(4, value)
'            If _triangleSize = value Then Return
'            _triangleSize = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Text drawing direction."), DefaultValue(GetType(ArrowTextDirection), "Horizontal")>
'    Public Property TextDirection As ArrowTextDirection
'        Get
'            Return _textDirection
'        End Get
'        Set(value As ArrowTextDirection)
'            If _textDirection = value Then Return
'            _textDirection = value
'            Invalidate()
'        End Set
'    End Property

'    Private _textAngle As Single = 0.0F
'    ''' <summary>Rotation angle in degrees. 0 = horizontal. -90 = bottom-to-top, 90 = top-to-bottom. Set at design time or in code.</summary>
'    <Category("Appearance"), Description("Text rotation angle in degrees (0 = horizontal, -90 = bottom-to-top, 90 = top-to-bottom)."), DefaultValue(0.0F)>
'    Public Property TextAngle As Single
'        Get
'            Return _textAngle
'        End Get
'        Set(value As Single)
'            If Math.Abs(_textAngle - value) < 0.01F Then Return
'            _textAngle = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Corner radius for the glass background."), DefaultValue(8)>
'    Public Property CornerRadius As Integer
'        Get
'            Return _cornerRadius
'        End Get
'        Set(value As Integer)
'            value = Math.Max(0, value)
'            If _cornerRadius = value Then Return
'            _cornerRadius = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Base color for the glass background.")>
'    Public Property GlassBaseColor As Color
'        Get
'            Return _glassBaseColor
'        End Get
'        Set(value As Color)
'            If _glassBaseColor = value Then Return
'            _glassBaseColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Accent glow color for the glass background.")>
'    Public Property GlassAccentColor As Color
'        Get
'            Return _glassAccentColor
'        End Get
'        Set(value As Color)
'            If _glassAccentColor = value Then Return
'            _glassAccentColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Highlight color for the glass shine.")>
'    Public Property GlassHighlightColor As Color
'        Get
'            Return _glassHighlightColor
'        End Get
'        Set(value As Color)
'            If _glassHighlightColor = value Then Return
'            _glassHighlightColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Border color for the control.")>
'    Public Property BorderColor As Color
'        Get
'            Return _borderColor
'        End Get
'        Set(value As Color)
'            If _borderColor = value Then Return
'            _borderColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Triangle fill color.")>
'    Public Property TriangleColor As Color
'        Get
'            Return _triangleColor
'        End Get
'        Set(value As Color)
'            If _triangleColor = value Then Return
'            _triangleColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Action"), Description("Raised when the arrow label is clicked.")>
'    Public Event ArrowClicked As EventHandler(Of MouseEventArgs)

'    Private _toolTip As ToolTip

'    Public Sub New()
'        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
'        DoubleBuffered = True
'        AutoSize = False
'        TextAlign = ContentAlignment.MiddleCenter
'        ForeColor = Color.White
'        Font = New Font("Segoe UI", 9, FontStyle.Bold)
'        Size = New Size(160, 44)
'    End Sub

'    Protected Overrides Sub OnHandleCreated(e As EventArgs)
'        MyBase.OnHandleCreated(e)
'        If _toolTip Is Nothing Then
'            _toolTip = New ToolTip() With {
'                .AutoPopDelay = 5000,
'                .InitialDelay = 300,
'                .ReshowDelay = 100,
'                .ShowAlways = True
'            }
'        End If
'        UpdateToolTipText()
'    End Sub

'    Private Sub UpdateToolTipText()
'        If _toolTip Is Nothing Then Return
'        If String.IsNullOrEmpty(Text) Then
'            _toolTip.SetToolTip(Me, Nothing)
'        Else
'            _toolTip.SetToolTip(Me, Text)
'        End If
'    End Sub

'    Protected Overrides Sub OnTextChanged(e As EventArgs)
'        MyBase.OnTextChanged(e)
'        UpdateToolTipText()
'        Invalidate()
'    End Sub

'    Protected Overrides Sub Dispose(disposing As Boolean)
'        If disposing AndAlso _toolTip IsNot Nothing Then
'            _toolTip.Dispose()
'            _toolTip = Nothing
'        End If
'        MyBase.Dispose(disposing)
'    End Sub

'    Protected Overrides Sub OnFontChanged(e As EventArgs)
'        MyBase.OnFontChanged(e)
'        Invalidate()
'    End Sub

'    Protected Overrides Sub OnForeColorChanged(e As EventArgs)
'        MyBase.OnForeColorChanged(e)
'        Invalidate()
'    End Sub

'    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
'        MyBase.OnMouseClick(e)
'        RaiseEvent ArrowClicked(Me, e)
'    End Sub

'    ''' <summary>Paints the arrow label: glass background, shine, glow ellipse, border, optional triangles, and text (horizontal or rotated).</summary>
'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

'        ' Client area and early exit if too small
'        Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
'        If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

'        ' Background rect: full area minus space for left/right triangles
'        Dim leftInset As Integer = If(_showLeftTriangle, _triangleSize, 0)
'        Dim rightInset As Integer = If(_showRightTriangle, _triangleSize, 0)
'        Dim bgRect As New Rectangle(rect.X + leftInset, rect.Y, rect.Width - leftInset - rightInset, rect.Height)

'        ' Rounded glass background (gradient fill)
'        Using bgPath = GetRoundedRectPath(bgRect, _cornerRadius)
'            Using bgBrush As New LinearGradientBrush(bgRect,
'                                                   Color.FromArgb(190, _glassBaseColor),
'                                                   Color.FromArgb(140, ControlPaint.Dark(_glassBaseColor)),
'                                                   LinearGradientMode.Vertical)
'                e.Graphics.FillPath(bgBrush, bgPath)
'            End Using

'            ' Shine strip (top portion of bg, lighter gradient)
'            Dim shineRect As New Rectangle(bgRect.X + 1, bgRect.Y + 1, bgRect.Width - 2, CInt(bgRect.Height * 0.45))
'            Using shinePath = GetRoundedRectPath(shineRect, _cornerRadius)
'                Using shineBrush As New LinearGradientBrush(shineRect,
'                                                            Color.FromArgb(160, _glassHighlightColor),
'                                                            Color.FromArgb(30, _glassHighlightColor),
'                                                            LinearGradientMode.Vertical)
'                    e.Graphics.FillPath(shineBrush, shinePath)
'                End Using
'            End Using

'            ' Glow ellipse: centered in the label (same center used later for text rotation)
'            Dim glowRect As New Rectangle(bgRect.X + bgRect.Width \ 4, bgRect.Y + bgRect.Height \ 3, bgRect.Width \ 2, bgRect.Height \ 2)
'            Using glowBrush As New SolidBrush(Color.FromArgb(70, _glassAccentColor))
'                e.Graphics.FillEllipse(glowBrush, glowRect)
'            End Using

'            ' Border around the rounded background
'            Using borderPen As New Pen(_borderColor)
'                e.Graphics.DrawPath(borderPen, bgPath)
'            End Using
'        End Using

'        ' Left triangle (arrow nub) if enabled
'        Dim midY As Integer = rect.Y + (rect.Height \ 2)
'        If _showLeftTriangle Then
'            Dim leftBaseX As Integer = bgRect.Left
'            Dim leftPts() As Point = {
'                New Point(leftBaseX - _triangleSize, midY),
'                New Point(leftBaseX, midY - _triangleSize),
'                New Point(leftBaseX, midY + _triangleSize)
'            }
'            Using br As New SolidBrush(_triangleColor)
'                e.Graphics.FillPolygon(br, leftPts)
'            End Using
'            Using pn As New Pen(_borderColor)
'                e.Graphics.DrawPolygon(pn, leftPts)
'            End Using
'        End If

'        ' Right triangle (arrow nub) if enabled
'        If _showRightTriangle Then
'            Dim rightBaseX As Integer = bgRect.Right
'            Dim rightPts() As Point = {
'                New Point(rightBaseX + _triangleSize, midY),
'                New Point(rightBaseX, midY - _triangleSize),
'                New Point(rightBaseX, midY + _triangleSize)
'            }
'            Using br As New SolidBrush(_triangleColor)
'                e.Graphics.FillPolygon(br, rightPts)
'            End Using
'            Using pn As New Pen(_borderColor)
'                e.Graphics.DrawPolygon(pn, rightPts)
'            End Using
'        End If

'        ' Label center = center of the glow ellipse (same formula as glow rect: X + W/4, Y + H/3, size W/2 x H/2)
'        Dim labelCenterX As Single = CSng(bgRect.X + bgRect.Width \ 4) + (bgRect.Width \ 2) / 2.0F
'        Dim labelCenterY As Single = CSng(bgRect.Y + bgRect.Height \ 3) + (bgRect.Height \ 2) / 2.0F

'        ' Text area (inside bg with padding). Draw rotated or horizontal so text center aligns with label (glow) center.
'        Dim textRect As New Rectangle(bgRect.X + 8, bgRect.Y + 4, bgRect.Width - 16, bgRect.Height - 8)
'        If textRect.Width > 0 AndAlso textRect.Height > 0 Then
'            Dim drawText As String = If(String.IsNullOrEmpty(Text), String.Empty, Text.Trim())
'            Dim angle As Single = _textAngle
'            If Math.Abs(angle) < 0.01F Then
'                Select Case _textDirection
'                    Case ArrowTextDirection.TopToBottom : angle = 90.0F
'                    Case ArrowTextDirection.BottomToTop : angle = -90.0F
'                End Select
'            End If
'            If Math.Abs(angle) >= 0.01F Then
'                DrawTextRotatedPath(e.Graphics, drawText, textRect, angle, labelCenterX, labelCenterY)
'            Else
'                Using format As StringFormat = GetStringFormat(TextAlign)
'                    format.Trimming = If(AutoEllipsis, StringTrimming.EllipsisCharacter, StringTrimming.None)
'                    Using br As New SolidBrush(ForeColor)
'                        e.Graphics.DrawString(drawText, Font, br, textRect, format)
'                    End Using
'                End Using
'            End If
'        End If
'    End Sub

'    ''' <summary>Draws text at any angle. Rotation is around the center of the text; that center is placed at (labelCenterX, labelCenterY), which must match the glow ellipse center. Clips output to textRect.</summary>
'    Private Sub DrawTextRotatedPath(g As Graphics, drawText As String, textRect As Rectangle, angleDegrees As Single, labelCenterX As Single, labelCenterY As Single)
'        If String.IsNullOrEmpty(drawText) OrElse Font Is Nothing Then Return
'        Dim rw As Single = Math.Max(1, textRect.Width)
'        Dim rh As Single = Math.Max(1, textRect.Height)

'        ' Build text as a path in (0,0,rw,rh) using current TextAlign
'        Dim emSize As Single = Font.Size * g.DpiY / 72.0F
'        Using fmt As StringFormat = GetStringFormat(TextAlign)
'            fmt.Trimming = If(AutoEllipsis, StringTrimming.EllipsisCharacter, StringTrimming.None)
'            Using path As New GraphicsPath()
'                path.AddString(drawText, Font.FontFamily, CInt(Font.Style), emSize, New RectangleF(0, 0, rw, rh), fmt)
'                Dim bounds As RectangleF = path.GetBounds()
'                If bounds.Width <= 0 OrElse bounds.Height <= 0 Then
'                    bounds = New RectangleF(0, 0, rw, rh)
'                End If
'                ' Rotate around the center of the text path, then move that center to the label (glow) center
'                Dim pathPivotX As Single = bounds.X + bounds.Width / 2.0F
'                Dim pathPivotY As Single = bounds.Y + bounds.Height / 2.0F
'                Using mat As New Matrix()
'                    mat.RotateAt(angleDegrees, New PointF(pathPivotX, pathPivotY))
'                    mat.Translate(labelCenterX - pathPivotX, labelCenterY - pathPivotY, MatrixOrder.Prepend)
'                    path.Transform(mat)
'                End Using
'                ' Clip to text rect so overflow is hidden, then fill the path
'                Dim prevClip As Region = g.Clip.Clone()
'                Try
'                    g.SetClip(textRect)
'                    Using br As New SolidBrush(ForeColor)
'                        g.FillPath(br, path)
'                    End Using
'                Finally
'                    g.Clip = prevClip
'                    prevClip.Dispose()
'                End Try
'            End Using
'        End Using
'    End Sub

'    ''' <summary>Returns a StringFormat that matches the control's TextAlign (ContentAlignment) for use with DrawString or GraphicsPath.AddString.</summary>
'    Private Function GetStringFormat(align As ContentAlignment) As StringFormat
'        Dim fmt As New StringFormat() With {.FormatFlags = StringFormatFlags.LineLimit}
'        Select Case align
'            Case ContentAlignment.TopLeft
'                fmt.Alignment = StringAlignment.Near
'                fmt.LineAlignment = StringAlignment.Near
'            Case ContentAlignment.TopCenter
'                fmt.Alignment = StringAlignment.Center
'                fmt.LineAlignment = StringAlignment.Near
'            Case ContentAlignment.TopRight
'                fmt.Alignment = StringAlignment.Far
'                fmt.LineAlignment = StringAlignment.Near
'            Case ContentAlignment.MiddleLeft
'                fmt.Alignment = StringAlignment.Near
'                fmt.LineAlignment = StringAlignment.Center
'            Case ContentAlignment.MiddleCenter
'                fmt.Alignment = StringAlignment.Center
'                fmt.LineAlignment = StringAlignment.Center
'            Case ContentAlignment.MiddleRight
'                fmt.Alignment = StringAlignment.Far
'                fmt.LineAlignment = StringAlignment.Center
'            Case ContentAlignment.BottomLeft
'                fmt.Alignment = StringAlignment.Near
'                fmt.LineAlignment = StringAlignment.Far
'            Case ContentAlignment.BottomCenter
'                fmt.Alignment = StringAlignment.Center
'                fmt.LineAlignment = StringAlignment.Far
'            Case ContentAlignment.BottomRight
'                fmt.Alignment = StringAlignment.Far
'                fmt.LineAlignment = StringAlignment.Far
'        End Select
'        Return fmt
'    End Function

'    ''' <summary>Builds a GraphicsPath for a rectangle with rounded corners (four quarter-circle arcs). Used for the glass background and shine shapes.</summary>
'    Private Function GetRoundedRectPath(rect As Rectangle, radius As Integer) As GraphicsPath
'        Dim path As New GraphicsPath()
'        If radius <= 0 Then
'            path.AddRectangle(rect)
'            Return path
'        End If
'        Dim d As Integer = radius * 2
'        path.AddArc(rect.X, rect.Y, d, d, 180, 90)
'        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
'        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
'        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
'        path.CloseFigure()
'        Return path
'    End Function
'End Class


'OROGINAL CODE WITHOUT ROTATION:
'<ToolboxItem(True)>
'Public Class ArrowLable
'    Inherits Label

'    Public Enum ArrowTextDirection
'        Horizontal = 0
'        TopToBottom = 1
'        BottomToTop = 2
'    End Enum

'    Private _showLeftTriangle As Boolean = True
'    Private _showRightTriangle As Boolean = True
'    Private _textDirection As ArrowTextDirection = ArrowTextDirection.Horizontal
'    Private _triangleSize As Integer = 10
'    Private _cornerRadius As Integer = 8
'    Private _glassBaseColor As Color = Color.FromArgb(90, 180, 255)
'    Private _glassAccentColor As Color = Color.FromArgb(170, 90, 255)
'    Private _glassHighlightColor As Color = Color.FromArgb(220, 255, 255, 255)
'    Private _borderColor As Color = Color.FromArgb(80, 20, 60, 110)
'    Private _triangleColor As Color = Color.FromArgb(220, 255, 140, 120)

'    <Category("Arrow"), Description("Show triangle on the left edge."), DefaultValue(True)>
'    Public Property ShowLeftTriangle As Boolean
'        Get
'            Return _showLeftTriangle
'        End Get
'        Set(value As Boolean)
'            If _showLeftTriangle = value Then Return
'            _showLeftTriangle = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Arrow"), Description("Show triangle on the right edge."), DefaultValue(True)>
'    Public Property ShowRightTriangle As Boolean
'        Get
'            Return _showRightTriangle
'        End Get
'        Set(value As Boolean)
'            If _showRightTriangle = value Then Return
'            _showRightTriangle = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Arrow"), Description("Triangle size in pixels."), DefaultValue(10)>
'    Public Property TriangleSize As Integer
'        Get
'            Return _triangleSize
'        End Get
'        Set(value As Integer)
'            value = Math.Max(4, value)
'            If _triangleSize = value Then Return
'            _triangleSize = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Text drawing direction."), DefaultValue(GetType(ArrowTextDirection), "Horizontal")>
'    Public Property TextDirection As ArrowTextDirection
'        Get
'            Return _textDirection
'        End Get
'        Set(value As ArrowTextDirection)
'            If _textDirection = value Then Return
'            _textDirection = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Corner radius for the glass background."), DefaultValue(8)>
'    Public Property CornerRadius As Integer
'        Get
'            Return _cornerRadius
'        End Get
'        Set(value As Integer)
'            value = Math.Max(0, value)
'            If _cornerRadius = value Then Return
'            _cornerRadius = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Base color for the glass background.")>
'    Public Property GlassBaseColor As Color
'        Get
'            Return _glassBaseColor
'        End Get
'        Set(value As Color)
'            If _glassBaseColor = value Then Return
'            _glassBaseColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Accent glow color for the glass background.")>
'    Public Property GlassAccentColor As Color
'        Get
'            Return _glassAccentColor
'        End Get
'        Set(value As Color)
'            If _glassAccentColor = value Then Return
'            _glassAccentColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Highlight color for the glass shine.")>
'    Public Property GlassHighlightColor As Color
'        Get
'            Return _glassHighlightColor
'        End Get
'        Set(value As Color)
'            If _glassHighlightColor = value Then Return
'            _glassHighlightColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Border color for the control.")>
'    Public Property BorderColor As Color
'        Get
'            Return _borderColor
'        End Get
'        Set(value As Color)
'            If _borderColor = value Then Return
'            _borderColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Appearance"), Description("Triangle fill color.")>
'    Public Property TriangleColor As Color
'        Get
'            Return _triangleColor
'        End Get
'        Set(value As Color)
'            If _triangleColor = value Then Return
'            _triangleColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Action"), Description("Raised when the arrow label is clicked.")>
'    Public Event ArrowClicked As EventHandler(Of MouseEventArgs)

'    Public Sub New()
'        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
'        DoubleBuffered = True
'        AutoSize = False
'        TextAlign = ContentAlignment.MiddleCenter
'        ForeColor = Color.White
'        Font = New Font("Segoe UI", 9, FontStyle.Bold)
'        Size = New Size(160, 44)
'    End Sub

'    Protected Overrides Sub OnTextChanged(e As EventArgs)
'        MyBase.OnTextChanged(e)
'        Invalidate()
'    End Sub

'    Protected Overrides Sub OnFontChanged(e As EventArgs)
'        MyBase.OnFontChanged(e)
'        Invalidate()
'    End Sub

'    Protected Overrides Sub OnForeColorChanged(e As EventArgs)
'        MyBase.OnForeColorChanged(e)
'        Invalidate()
'    End Sub

'    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
'        MyBase.OnMouseClick(e)
'        RaiseEvent ArrowClicked(Me, e)
'    End Sub

'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

'        Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
'        If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

'        Dim leftInset As Integer = If(_showLeftTriangle, _triangleSize, 0)
'        Dim rightInset As Integer = If(_showRightTriangle, _triangleSize, 0)
'        Dim bgRect As New Rectangle(rect.X + leftInset, rect.Y, rect.Width - leftInset - rightInset, rect.Height)

'        Using bgPath = GetRoundedRectPath(bgRect, _cornerRadius)
'            Using bgBrush As New LinearGradientBrush(bgRect,
'                                                   Color.FromArgb(190, _glassBaseColor),
'                                                   Color.FromArgb(140, ControlPaint.Dark(_glassBaseColor)),
'                                                   LinearGradientMode.Vertical)
'                e.Graphics.FillPath(bgBrush, bgPath)
'            End Using

'            Dim shineRect As New Rectangle(bgRect.X + 1, bgRect.Y + 1, bgRect.Width - 2, CInt(bgRect.Height * 0.45))
'            Using shinePath = GetRoundedRectPath(shineRect, _cornerRadius)
'                Using shineBrush As New LinearGradientBrush(shineRect,
'                                                            Color.FromArgb(160, _glassHighlightColor),
'                                                            Color.FromArgb(30, _glassHighlightColor),
'                                                            LinearGradientMode.Vertical)
'                    e.Graphics.FillPath(shineBrush, shinePath)
'                End Using
'            End Using

'            Dim glowRect As New Rectangle(bgRect.X + bgRect.Width \ 4, bgRect.Y + bgRect.Height \ 3, bgRect.Width \ 2, bgRect.Height \ 2)
'            Using glowBrush As New SolidBrush(Color.FromArgb(70, _glassAccentColor))
'                e.Graphics.FillEllipse(glowBrush, glowRect)
'            End Using

'            Using borderPen As New Pen(_borderColor)
'                e.Graphics.DrawPath(borderPen, bgPath)
'            End Using
'        End Using

'        Dim midY As Integer = rect.Y + (rect.Height \ 2)
'        If _showLeftTriangle Then
'            Dim leftBaseX As Integer = bgRect.Left
'            Dim leftPts() As Point = {
'                New Point(leftBaseX - _triangleSize, midY),
'                New Point(leftBaseX, midY - _triangleSize),
'                New Point(leftBaseX, midY + _triangleSize)
'            }
'            Using br As New SolidBrush(_triangleColor)
'                e.Graphics.FillPolygon(br, leftPts)
'            End Using
'            Using pn As New Pen(_borderColor)
'                e.Graphics.DrawPolygon(pn, leftPts)
'            End Using
'        End If

'        If _showRightTriangle Then
'            Dim rightBaseX As Integer = bgRect.Right
'            Dim rightPts() As Point = {
'                New Point(rightBaseX + _triangleSize, midY),
'                New Point(rightBaseX, midY - _triangleSize),
'                New Point(rightBaseX, midY + _triangleSize)
'            }
'            Using br As New SolidBrush(_triangleColor)
'                e.Graphics.FillPolygon(br, rightPts)
'            End Using
'            Using pn As New Pen(_borderColor)
'                e.Graphics.DrawPolygon(pn, rightPts)
'            End Using
'        End If

'        Dim textRect As New Rectangle(bgRect.X + 8, bgRect.Y + 4, bgRect.Width - 16, bgRect.Height - 8)
'        If textRect.Width > 0 AndAlso textRect.Height > 0 Then
'            Dim drawText As String = Text
'            If _textDirection = ArrowTextDirection.TopToBottom Then
'                drawText = MakeVerticalText(Text, False)
'            ElseIf _textDirection = ArrowTextDirection.BottomToTop Then
'                drawText = MakeVerticalText(Text, True)
'            End If

'            Using format As StringFormat = GetStringFormat(TextAlign)
'                format.Trimming = If(AutoEllipsis, StringTrimming.EllipsisCharacter, StringTrimming.None)
'                Using br As New SolidBrush(ForeColor)
'                    e.Graphics.DrawString(drawText, Font, br, textRect, format)
'                End Using
'            End Using
'        End If
'    End Sub

'    Private Function MakeVerticalText(value As String, bottomToTop As Boolean) As String
'        If String.IsNullOrEmpty(value) Then Return String.Empty
'        Dim chars = value.Trim().ToCharArray()
'        If bottomToTop Then
'            Array.Reverse(chars)
'        End If
'        Return String.Join(vbCrLf, chars)
'    End Function

'    Private Function GetStringFormat(align As ContentAlignment) As StringFormat
'        Dim fmt As New StringFormat() With {.FormatFlags = StringFormatFlags.LineLimit}
'        Select Case align
'            Case ContentAlignment.TopLeft
'                fmt.Alignment = StringAlignment.Near
'                fmt.LineAlignment = StringAlignment.Near
'            Case ContentAlignment.TopCenter
'                fmt.Alignment = StringAlignment.Center
'                fmt.LineAlignment = StringAlignment.Near
'            Case ContentAlignment.TopRight
'                fmt.Alignment = StringAlignment.Far
'                fmt.LineAlignment = StringAlignment.Near
'            Case ContentAlignment.MiddleLeft
'                fmt.Alignment = StringAlignment.Near
'                fmt.LineAlignment = StringAlignment.Center
'            Case ContentAlignment.MiddleCenter
'                fmt.Alignment = StringAlignment.Center
'                fmt.LineAlignment = StringAlignment.Center
'            Case ContentAlignment.MiddleRight
'                fmt.Alignment = StringAlignment.Far
'                fmt.LineAlignment = StringAlignment.Center
'            Case ContentAlignment.BottomLeft
'                fmt.Alignment = StringAlignment.Near
'                fmt.LineAlignment = StringAlignment.Far
'            Case ContentAlignment.BottomCenter
'                fmt.Alignment = StringAlignment.Center
'                fmt.LineAlignment = StringAlignment.Far
'            Case ContentAlignment.BottomRight
'                fmt.Alignment = StringAlignment.Far
'                fmt.LineAlignment = StringAlignment.Far
'        End Select
'        Return fmt
'    End Function

'    Private Function GetRoundedRectPath(rect As Rectangle, radius As Integer) As GraphicsPath
'        Dim path As New GraphicsPath()
'        If radius <= 0 Then
'            path.AddRectangle(rect)
'            Return path
'        End If

'        Dim d As Integer = radius * 2
'        path.AddArc(rect.X, rect.Y, d, d, 180, 90)
'        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
'        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
'        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
'        path.CloseFigure()
'        Return path
'    End Function
'End Class
