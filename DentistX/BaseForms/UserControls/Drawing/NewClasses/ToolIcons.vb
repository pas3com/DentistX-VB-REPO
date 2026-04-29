Public Class ToolIcons
    Public Shared Function GetPolylineIcon() As Bitmap
        Dim bmp As New Bitmap(16, 16)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.Clear(Color.Transparent)
            Using pen As New Pen(Color.Black, 1.5F)
                g.DrawLines(pen, {
                    New Point(2, 14),
                    New Point(5, 6),
                    New Point(8, 10),
                    New Point(11, 4),
                    New Point(14, 8)
                })
            End Using
        End Using
        Return bmp
    End Function

    Public Shared Function GetArrowIcon() As Bitmap
        Dim bmp As New Bitmap(16, 16)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.Clear(Color.Transparent)
            Using pen As New Pen(Color.Black, 1.5F)
                ' Arrow line
                g.DrawLine(pen, 3, 8, 12, 8)

                ' Arrow head
                g.DrawLine(pen, 12, 8, 9, 5)
                g.DrawLine(pen, 12, 8, 9, 11)
            End Using
        End Using
        Return bmp
    End Function

    Public Shared Function GetTriangleIcon() As Bitmap
        Dim bmp As New Bitmap(16, 16)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.Clear(Color.Transparent)
            Using brush As New SolidBrush(Color.Black)
                g.FillPolygon(brush, {
                    New Point(8, 2),
                    New Point(2, 14),
                    New Point(14, 14)
                })
            End Using
        End Using
        Return bmp
    End Function

    Public Shared Function GetFreehandIcon() As Bitmap
        Dim bmp As New Bitmap(16, 16)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.Clear(Color.Transparent)
            Using pen As New Pen(Color.Black, 1.5F)
                g.DrawCurve(pen, {
                    New Point(2, 10),
                    New Point(4, 5),
                    New Point(7, 8),
                    New Point(10, 3),
                    New Point(13, 7),
                    New Point(14, 12)
                })
            End Using
        End Using
        Return bmp
    End Function

    Public Shared Function GetTextIcon() As Bitmap
        Dim bmp As New Bitmap(16, 16)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.Clear(Color.Transparent)
            Using font As New Font("Arial", 10, FontStyle.Bold)
                Using brush As New SolidBrush(Color.Black)
                    g.DrawString("T", font, brush, 4, 1)
                End Using
            End Using

            ' Underline
            Using pen As New Pen(Color.Black, 1)
                g.DrawLine(pen, 4, 14, 12, 14)
            End Using
        End Using
        Return bmp
    End Function

    '#Region "Crop"
    '    ' Add this to your Form with a PictureBox named picCanvas
    '    ' and a ToolStrip with 3 buttons: btnCropBackground, btnKeepShapes, btnRemoveShapes

    '    Private cropStart As Point
    '    Private cropRect As Rectangle
    '    Private isCropping As Boolean = False

    '    Private Sub picCanvas_MouseDown(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseDown
    '        If e.Button = MouseButtons.Left Then
    '            cropStart = e.Location
    '            isCropping = True
    '            cropRect = New Rectangle(e.X, e.Y, 0, 0)
    '            picCanvas.Invalidate()
    '        End If
    '    End Sub

    '    Private Sub picCanvas_MouseMove(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseMove
    '        If isCropping Then
    '            Dim x = Math.Min(cropStart.X, e.X)
    '            Dim y = Math.Min(cropStart.Y, e.Y)
    '            Dim w = Math.Abs(cropStart.X - e.X)
    '            Dim h = Math.Abs(cropStart.Y - e.Y)
    '            cropRect = New Rectangle(x, y, w, h)
    '            picCanvas.Invalidate()
    '        End If
    '    End Sub

    '    Private Sub picCanvas_MouseUp(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseUp
    '        If isCropping Then
    '            isCropping = False
    '            picCanvas.Invalidate()
    '        End If
    '    End Sub

    '    Private Sub picCanvas_Paint(sender As Object, e As PaintEventArgs) Handles picCanvas.Paint
    '        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
    '        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality

    '        ' Draw everything using DrawablePictureN
    '        If m_PictureN IsNot Nothing Then
    '            m_PictureN.Draw(e.Graphics)
    '        End If

    '        ' Draw crop rectangle
    '        If cropRect.Width > 0 AndAlso cropRect.Height > 0 Then
    '            Using pen As New Pen(Color.Red, 2)
    '                pen.DashStyle = DashStyle.Dash
    '                e.Graphics.DrawRectangle(pen, cropRect)
    '            End Using
    '        End If
    '    End Sub

    '    ' Button: Crop Background
    '    Private Sub btnCropBackground_Click(sender As Object, e As EventArgs) Handles btnCropBackground.Click
    '        If m_PictureN IsNot Nothing Then
    '            m_PictureN.CropBackground(cropRect)
    '            cropRect = Rectangle.Empty
    '            picCanvas.Invalidate()
    '        End If
    '    End Sub

    '    ' Button: Keep Only Shapes Inside
    '    Private Sub btnKeepShapes_Click(sender As Object, e As EventArgs) Handles btnKeepShapes.Click
    '        If m_PictureN IsNot Nothing Then
    '            m_PictureN.Drawables = m_PictureN.Drawables.
    '            Where(Function(d) cropRect.IntersectsWith(d.GetBounds())).ToList()
    '            cropRect = Rectangle.Empty
    '            picCanvas.Invalidate()
    '        End If
    '    End Sub

    '    ' Button: Remove Shapes Outside
    '    Private Sub btnRemoveShapes_Click(sender As Object, e As EventArgs) Handles btnRemoveShapes.Click
    '        If m_PictureN IsNot Nothing Then
    '            m_PictureN.Drawables = m_PictureN.Drawables.
    '            Where(Function(d) cropRect.Contains(d.GetBounds())).ToList()
    '            cropRect = Rectangle.Empty
    '            picCanvas.Invalidate()
    '        End If
    '    End Sub

    '    ' DrawablePictureN method to crop background image
    '    Public Sub CropBackground(rect As Rectangle)
    '        If BackgroundImage Is Nothing Then Exit Sub
    '        Dim cropped As New Bitmap(rect.Width, rect.Height)
    '        Using g As Graphics = Graphics.FromImage(cropped)
    '            g.DrawImage(BackgroundImage, New Rectangle(0, 0, rect.Width, rect.Height),
    '                    rect, GraphicsUnit.Pixel)
    '        End Using
    '        BackgroundImage = cropped
    '    End Sub

    '#End Region


End Class