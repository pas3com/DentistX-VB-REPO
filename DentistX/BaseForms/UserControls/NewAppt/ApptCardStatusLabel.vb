Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms

''' <summary>Vertical status text for <see cref="ApptCard"/>: no ToolTip / ArrowLable — draws flat fill, border, and ±90° text (same roles as <see cref="ArrowLable.ArrowTextDirection"/>).</summary>
<ToolboxItem(True)>
Public Class ApptCardStatusLabel
    Inherits Control

    Public Enum StatusTextDirection
        TopToBottom
        BottomToTop
    End Enum

    Private _textDir As StatusTextDirection = StatusTextDirection.BottomToTop
    Private _borderColor As Color = Color.FromArgb(80, 20, 60, 110)
    Private _textAlign As ContentAlignment = ContentAlignment.MiddleCenter

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        UpdateStyles()
        DoubleBuffered = True
        TabStop = False
        MinimumSize = New Size(1, 1)
        ForeColor = Color.White
        Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        BackColor = Color.FromArgb(90, 180, 255)
    End Sub

    <Browsable(True), Category("Appearance"), DefaultValue(GetType(StatusTextDirection), "BottomToTop")>
    Public Property TextDirection As StatusTextDirection
        Get
            Return _textDir
        End Get
        Set(value As StatusTextDirection)
            If _textDir = value Then Return
            _textDir = value
            Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Appearance")>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Appearance"), DefaultValue(GetType(ContentAlignment), "MiddleCenter")>
    Public Property TextAlign As ContentAlignment
        Get
            Return _textAlign
        End Get
        Set(value As ContentAlignment)
            If _textAlign = value Then Return
            _textAlign = value
            Invalidate()
        End Set
    End Property

    ''' <summary>Base Control does not invalidate on text changes; the designer and runtime need this for repaints.</summary>
    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub SetBoundsCore(x As Integer, y As Integer, width As Integer, height As Integer, specified As BoundsSpecified)
        width = Math.Max(1, width)
        height = Math.Max(1, height)
        MyBase.SetBoundsCore(x, y, width, height, specified)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.TextRenderingHint = TextRenderingHint.AntiAlias
        g.Clear(BackColor)
        If Width <= 0 OrElse Height <= 0 Then Return
        Using p As New Pen(_borderColor, 1.0F)
            g.DrawRectangle(p, 0, 0, Width - 1, Height - 1)
        End Using
        Dim drawText = If(Text, String.Empty).Trim()
        If LicenseManager.UsageMode = LicenseUsageMode.Designtime AndAlso drawText.Length = 0 Then
            drawText = "Status"
        End If
        If drawText.Length = 0 OrElse Font Is Nothing OrElse Font.FontFamily Is Nothing Then Return
        Const pad As Single = 2.0F
        Dim r As New RectangleF(pad, pad, CSng(Width) - 2.0F * pad, CSng(Height) - 2.0F * pad)
        If r.Width < 1.0F OrElse r.Height < 1.0F Then Return
        Dim textRectI As New Rectangle(
            CInt(Math.Floor(r.X)),
            CInt(Math.Floor(r.Y)),
            Math.Max(1, CInt(Math.Ceiling(r.Width - 0.001F))),
            Math.Max(1, CInt(Math.Ceiling(r.Height - 0.001F))))
        ' Same as ArrowLable: TopToBottom = +90°, BottomToTop = -90°
        Dim angleDeg = If(_textDir = StatusTextDirection.TopToBottom, 90.0F, -90.0F)
        Dim rotAnchor = GetAlignmentAnchor(_textAlign, r)
        Dim usePlaceholder = LicenseManager.UsageMode = LicenseUsageMode.Designtime AndAlso String.IsNullOrEmpty(If(Text, String.Empty).Trim())
        Dim fillColor = If(usePlaceholder, Color.FromArgb(200, ForeColor.R, ForeColor.G, ForeColor.B), ForeColor)
        Using br As New SolidBrush(fillColor)
            DrawTextRotatedPath(g, drawText, textRectI, angleDeg, rotAnchor.X, rotAnchor.Y, br)
        End Using
    End Sub

    ''' <summary>Same approach as <see cref="ArrowLable"/>'s DrawTextRotatedPath: outline text -> matrix -> FillPath. Avoids DrawString+transform/RTL/DC issues.</summary>
    Private Sub DrawTextRotatedPath(
        g As Graphics,
        drawText As String,
        textRect As Rectangle,
        angleDegrees As Single,
        labelCenterX As Single,
        labelCenterY As Single,
        br As SolidBrush)
        If String.IsNullOrEmpty(drawText) OrElse Font Is Nothing OrElse br Is Nothing Then Return
        Dim rw As Single = Math.Max(1, textRect.Width)
        Dim rh As Single = Math.Max(1, textRect.Height)
        ' For near-±90° the text runs along the strip height: swap layout dimensions.
        Dim layoutW As Single = rw
        Dim layoutH As Single = rh
        If Math.Abs(Math.Abs(angleDegrees) - 90.0F) < 1.0F Then
            layoutW = rh
            layoutH = rw
        End If
        Dim emSize As Single = Font.Size * g.DpiY / 72.0F
        Dim maxEm As Single = layoutH * 0.85F
        If maxEm > 0 AndAlso emSize > maxEm Then emSize = maxEm
        If emSize < 6.0F Then emSize = 6.0F
        Using fmt = GetStringFormatForAlign(_textAlign)
            fmt.Trimming = StringTrimming.None
            ' ±90°: RTL format mirrors progression along the strip — Arabic looked bottom-to-top. Rotation alone defines flow.
            Dim vertical = Math.Abs(Math.Abs(angleDegrees) - 90.0F) < 1.0F
            If RightToLeft = RightToLeft.Yes AndAlso Not vertical Then
                fmt.FormatFlags = fmt.FormatFlags Or StringFormatFlags.DirectionRightToLeft
            End If
            Using path As New GraphicsPath()
                path.AddString(drawText, Font.FontFamily, CInt(Font.Style), emSize, New RectangleF(0, 0, layoutW, layoutH), fmt)
                Dim pathAnchor As PointF = GetAlignmentAnchor(_textAlign, New RectangleF(0, 0, layoutW, layoutH))
                Using mat As New Matrix()
                    mat.RotateAt(angleDegrees, pathAnchor)
                    mat.Translate(labelCenterX - pathAnchor.X, labelCenterY - pathAnchor.Y, MatrixOrder.Append)
                    path.Transform(mat)
                End Using
                Dim prevClip As Region = g.Clip.Clone()
                Try
                    g.SetClip(textRect)
                    g.FillPath(br, path)
                Finally
                    g.Clip = prevClip
                    prevClip.Dispose()
                End Try
            End Using
        End Using
    End Sub

    Private Shared Function GetStringFormatForAlign(align As ContentAlignment) As StringFormat
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

    Private Shared Function GetAlignmentAnchor(align As ContentAlignment, rect As RectangleF) As PointF
        Dim x As Single
        Dim y As Single
        Select Case align
            Case ContentAlignment.TopLeft, ContentAlignment.MiddleLeft, ContentAlignment.BottomLeft
                x = rect.Left
            Case ContentAlignment.TopCenter, ContentAlignment.MiddleCenter, ContentAlignment.BottomCenter
                x = rect.Left + rect.Width / 2.0F
            Case Else
                x = rect.Right
        End Select
        Select Case align
            Case ContentAlignment.TopLeft, ContentAlignment.TopCenter, ContentAlignment.TopRight
                y = rect.Top
            Case ContentAlignment.MiddleLeft, ContentAlignment.MiddleCenter, ContentAlignment.MiddleRight
                y = rect.Top + rect.Height / 2.0F
            Case Else
                y = rect.Bottom
        End Select
        Return New PointF(x, y)
    End Function
End Class
