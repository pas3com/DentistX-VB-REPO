Imports DevExpress.XtraEditors

Public Class GradientButton
    Inherits SimpleButton

    Private _startColor As Color = Color.LightBlue
    Private _endColor As Color = Color.DarkBlue
    Private _gradientMode As Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.Vertical

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
        Me.DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        ' Draw gradient background
        Using brush As New Drawing2D.LinearGradientBrush(
            Me.ClientRectangle,
            _startColor,
            _endColor,
            _gradientMode)

            e.Graphics.FillRectangle(brush, Me.ClientRectangle)
        End Using

        ' Draw button text
        Dim textFormat As New StringFormat()
        textFormat.Alignment = StringAlignment.Center
        textFormat.LineAlignment = StringAlignment.Center

        Using brush As New SolidBrush(Me.ForeColor)
            e.Graphics.DrawString(Me.Text, Me.Font, brush, Me.ClientRectangle, textFormat)
        End Using
    End Sub
End Class