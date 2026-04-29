Imports System.Drawing.Drawing2D
Imports DevExpress.XtraEditors
Imports DevExpress.Utils.Svg
Imports System.ComponentModel
Imports DevExpress.Utils

Public Class RotatableSvgBox
    Inherits UserControl

    Private WithEvents innerSvg As New SvgImageBox()

    Public Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint, True)
        Me.BackColor = Color.Transparent
        Me.Controls.Add(innerSvg)
        innerSvg.Dock = DockStyle.Fill
        innerSvg.BackColor = Color.Transparent
        angle = 0
    End Sub

    Private angle As Single
    Public Property RotationAngle As Single
        Get
            Return angle
        End Get
        Set(value As Single)
            angle = value
            Me.Invalidate()
        End Set
    End Property

    Public Sub Rotate(angleDelta As Single)
        Me.RotationAngle += angleDelta
    End Sub


    <Category("Behavior")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(True)>
    Public ReadOnly Property InnerSvgImageBox As SvgImageBox
        Get
            Return innerSvg
        End Get
    End Property




    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <Category("Appearance")>
    <Description("The SVG image to display.")>
    Public Property SvgImage As SvgImage
        Get
            Return innerSvg.SvgImage
        End Get
        Set(value As SvgImage)
            innerSvg.SvgImage = value
        End Set
    End Property


    Public ReadOnly Property RootItems As SvgImageItemCollection
        Get
            Return innerSvg?.RootItems
        End Get
    End Property

    ' Forward common events
    Public Shadows Event Click As EventHandler
    Public Shadows Event MouseDown As MouseEventHandler
    Public Shadows Event MouseUp As MouseEventHandler
    Public Shadows Event MouseMove As MouseEventHandler
    Public Shadows Event MouseWheel As MouseEventHandler
    Public Shadows Event MouseEnter As EventHandler
    Public Shadows Event MouseLeave As EventHandler
    ' DevExpress SvgImageItem events
    Public Event SvgItemClick As EventHandler(Of SvgImageItemMouseEventArgs)

    Public Event SvgItemPress As EventHandler(Of SvgImageItemEventArgs)
    Public Event SvgItemEnter As EventHandler(Of SvgImageItemEventArgs)
    Public Event SvgItemLeave As EventHandler(Of SvgImageItemEventArgs)

    ' Wire up events from innerSvg
    Private Sub innerSvg_SvgItemClick(sender As Object, e As SvgImageItemMouseEventArgs) Handles innerSvg.ItemClick
        RaiseEvent SvgItemClick(Me, e)
    End Sub

    Private Sub innerSvg_SvgItemMouseMove(sender As Object, e As SvgImageItemEventArgs) Handles innerSvg.ItemPress
        RaiseEvent SvgItemPress(Me, e)
    End Sub

    Private Sub innerSvg_SvgItemEnter(sender As Object, e As SvgImageItemMouseEventArgs) Handles innerSvg.ItemEnter
        RaiseEvent SvgItemEnter(Me, e)
    End Sub

    Private Sub innerSvg_SvgItemMouseLeave(sender As Object, e As SvgImageItemMouseEventArgs) Handles innerSvg.ItemLeave
        RaiseEvent SvgItemLeave(Me, e)
    End Sub

    ' Hook events from innerSvg
    Private Sub innerSvg_Click(sender As Object, e As EventArgs) Handles innerSvg.Click
        RaiseEvent Click(Me, e)
    End Sub

    Private Sub innerSvg_MouseDown(sender As Object, e As MouseEventArgs) Handles innerSvg.MouseDown
        RaiseEvent MouseDown(Me, e)
    End Sub

    Private Sub innerSvg_MouseUp(sender As Object, e As MouseEventArgs) Handles innerSvg.MouseUp
        RaiseEvent MouseUp(Me, e)
    End Sub

    Private Sub innerSvg_MouseMove(sender As Object, e As MouseEventArgs) Handles innerSvg.MouseMove
        RaiseEvent MouseMove(Me, e)
    End Sub

    Private Sub innerSvg_MouseWheel(sender As Object, e As MouseEventArgs) Handles innerSvg.MouseWheel
        RaiseEvent MouseWheel(Me, e)
    End Sub

    Private Sub innerSvg_MouseEnter(sender As Object, e As EventArgs) Handles innerSvg.MouseEnter
        RaiseEvent MouseEnter(Me, e)
    End Sub

    Private Sub innerSvg_MouseLeave(sender As Object, e As EventArgs) Handles innerSvg.MouseLeave
        RaiseEvent MouseLeave(Me, e)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        ' Apply rotation to the entire control
        Dim g As Graphics = e.Graphics
        Dim center As New PointF(Me.Width / 2, Me.Height / 2)
        g.TranslateTransform(center.X, center.Y)
        g.RotateTransform(angle)
        g.TranslateTransform(-center.X, -center.Y)
        MyBase.OnPaint(e)
    End Sub

    Private Sub SvgImageBox_Paint(sender As Object, e As PaintEventArgs)
        Dim svgControl As SvgImageBox = DirectCast(sender, SvgImageBox)

        Dim rotationAngle As Single = 0
        If svgControl.Tag IsNot Nothing AndAlso Single.TryParse(svgControl.Tag.ToString(), rotationAngle) Then

            ' Save the current graphics state
            Dim state As Drawing2D.GraphicsState = e.Graphics.Save()

            ' Create rotation around center
            Dim center As New PointF(svgControl.Width / 2, svgControl.Height / 2)
            e.Graphics.TranslateTransform(center.X, center.Y)
            e.Graphics.RotateTransform(rotationAngle)
            e.Graphics.TranslateTransform(-center.X, -center.Y)

            ' Draw the image
            If svgControl.SvgImage IsNot Nothing Then
                Dim s As Size = New Size(svgControl.SvgImage.Width, svgControl.SvgImage.Height)
                svgControl.SvgImage.Render(palette:=New SvgPalette) ',u True, True) ' e.Graphics, svgControl.ClientRectangle)
            End If

            ' Restore the original graphics state
            e.Graphics.Restore(state)

            ' Prevent default paint (optional, if needed)

        End If
    End Sub



End Class
