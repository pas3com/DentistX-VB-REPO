Imports DevExpress.XtraEditors

Public Class GradientGroup
    Inherits GroupControl

    Private _startColor As Color = Color.LightBlue
    Private _endColor As Color = Color.DarkBlue
    Private _gradientMode As Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.Vertical
    Private _isPainting As Boolean = False

    Public Property StartColor As Color
        Get
            Return _startColor
        End Get
        Set(value As Color)
            _startColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Property EndColor As Color
        Get
            Return _endColor
        End Get
        Set(value As Color)
            _endColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Property GradientMode As Drawing2D.LinearGradientMode
        Get
            Return _gradientMode
        End Get
        Set(value As Drawing2D.LinearGradientMode)
            _gradientMode = value
            Me.Invalidate()
        End Set
    End Property

    Public Sub New()
        ' Set up control for custom painting
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        ' Make sure we can see through to the gradient
        Me.Appearance.BackColor = Color.Transparent
        Me.Appearance.BackColor2 = Color.Transparent
        Me.Appearance.Options.UseBackColor = True
    End Sub

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        ' Don't call MyBase.OnPaintBackground to prevent default background painting
        ' We'll handle all painting in OnPaint
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        If _isPainting Then Return

        _isPainting = True
        Try
            ' Clear the background
            e.Graphics.Clear(Me.BackColor)

            ' Draw gradient
            Using brush As New Drawing2D.LinearGradientBrush(
                Me.ClientRectangle,
                _startColor,
                _endColor,
                _gradientMode)

                brush.GammaCorrection = True
                e.Graphics.FillRectangle(brush, Me.ClientRectangle)
            End Using

            ' Now let base class draw borders and text
            MyBase.OnPaint(e)

        Catch ex As Exception
            ' Fall back to default painting
            MyBase.OnPaint(e)
        Finally
            _isPainting = False
        End Try
    End Sub
End Class