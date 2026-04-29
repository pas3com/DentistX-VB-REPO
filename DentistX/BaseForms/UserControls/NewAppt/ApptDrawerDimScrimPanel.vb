Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>
''' Shared semi-transparent scrim for month/week slide drawers. Mirrors Gemini <c>MonthDrawerDimPanel</c>:
''' Win32 <c>WS_EX_TRANSPARENT</c> for composition with siblings, no opaque background erase,
''' and no double-buffer that would flatten alpha. <c>WM_NCHITTEST</c> returns HTCLIENT so
''' the overlay still receives clicks (plain WS_EX_TRANSPARENT would pass clicks through).
''' </summary>
Friend MustInherit Class ApptDrawerDimScrimPanel
    Inherits Panel

    Private Const WM_NCHITTEST As Integer = &H84
    Private Const HTCLIENT As Integer = 1

    ''' <summary>~40% blue tint (ARGB alpha 100), same as Gemini branch.</summary>
    Protected Shared ReadOnly ScrimBrushColor As Color = Color.FromArgb(100, 30, 80, 140)

    Private ReadOnly _onDismiss As Action

    Protected Sub New(onDismiss As Action)
        _onDismiss = onDismiss
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        ' Double-buffered bitmaps are typically opaque — breaks real alpha blend over the grid.
        SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.DoubleBuffer, False)
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.Opaque, False)
        UpdateStyles()
        BackColor = Color.Transparent
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp = MyBase.CreateParams
            If Not DesignMode Then
                cp.ExStyle = cp.ExStyle Or &H20 ' WS_EX_TRANSPARENT (Gemini); siblings paint first for blend
            End If
            Return cp
        End Get
    End Property

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        ' Do not call MyBase — it would fill an opaque "transparent" wash and kill alpha over the grid.
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        If ClientRectangle.Width <= 0 OrElse ClientRectangle.Height <= 0 Then Return
        e.Graphics.CompositingMode = CompositingMode.SourceOver
        Using br As New SolidBrush(ScrimBrushColor)
            e.Graphics.FillRectangle(br, ClientRectangle)
        End Using
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)
        If m.Msg = WM_NCHITTEST Then
            m.Result = New IntPtr(HTCLIENT)
        End If
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)
        If _onDismiss IsNot Nothing Then _onDismiss.Invoke()
    End Sub
End Class
