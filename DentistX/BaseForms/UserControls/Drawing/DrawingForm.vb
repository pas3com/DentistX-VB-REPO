Imports System.Collections.Generic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text
' DICOM viewing is done by the external DicomImageViewer project; no FellowOakDicom reference here.

Public Class DrawingForm

#Region "Variables"

    Private transformationController As New TransformationController()
    Private isCreatingNewObject As Boolean = False

    'Cropping
    Private isCropping As Boolean = False
    Private cropStart As Point
    Private cropRect As Rectangle
    Private cropActionPending As String = "" ' "CropBackground", "KeepShapes", or "RemoveShapes"

    ' Holds the picture we are drawing.
    Private m_PictureN As New DrawablePictureN(Me.BackColor)

    ' The current drawing attributes.
    Private m_CurrentLineWidth As Integer = 1
    Private m_CurrentForeColor As Color = Color.Black
    Private m_CurrentFillColor As Color = Color.White

    ' The object we are currently drawing.
    Private m_NewDrawableN As DrawableN
    Private m_IsCreatingTextRect As Boolean
    Private m_TextRectStart As Point

    ' The tool we have currently selected.
    Private m_SelectedToolButton As ToolBarButton

    ' The index of the first thickness image in its ImageList.
    Private m_FirstLineThicknessImage As Integer
    Private m_FirstLineColorImage As Integer
    Private m_FirstFillColorImage As Integer
    '
    'Resizing Moving Scaling
    Private Enum TransformMode
        None
        Moving
        Rotating
        Scaling
        Resizing
    End Enum

    Private m_SelectedMouseX As Integer
    Private m_SelectedMouseY As Integer
    Private m_TransformMode As TransformMode = TransformMode.None
    Private m_TransformStartPoint As PointF
    Private m_OriginalRotation As Single
    Private m_OriginalScaleX As Single
    Private m_OriginalScaleY As Single
    Private m_ResizeHandleIndex As Integer = -1
    'Cropping
    Private cropAnchor As CropAnchors = CropAnchors.None
    Private cropHandleSize As Integer = 6
    Private Enum CropAnchors
        None
        Move
        TopLeft
        TopRight
        BottomLeft
        BottomRight
        Left
        Right
        Top
        Bottom
    End Enum

#End Region

#Region "Setup"
    ' Get ready.
    Private Sub NewFrmDraw_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' In form initialization
        'btnFreehand = New ToolBarButton With {.ToolTipText = "Freehand",
        '        .Style = ToolBarButtonStyle.ToggleButton}
        'ToolBar1.Buttons.Add(btnFreehand)
        ' Set the initial directory for dialogs.
        Dim init_dir As String = Application.StartupPath
        If init_dir.EndsWith("\bin") Then init_dir = init_dir.Substring(0, init_dir.Length - 4)
        dlgSavePicture.InitialDirectory = init_dir
        dlgOpenPicture.InitialDirectory = init_dir

        ' Start with the Pointer tool selected.
        m_SelectedToolButton = tbtnPointer
        m_SelectedToolButton.Pushed = True

        ' Make line color images.
        MakeLineThicknessImages()

        ' Set line thickness menu event handlers.
        PrepareThicknessMenu(mnuThick1)
        PrepareThicknessMenu(mnuThick2)
        PrepareThicknessMenu(mnuThick3)
        PrepareThicknessMenu(mnuThick4)
        PrepareThicknessMenu(mnuThick5)
        mnuThick1.PerformClick()

        ' Make line color images.
        MakeLineColorImages()

        ' Set line color menu event handlers.
        PrepareLineColorMenu(mnuLineColorRed)
        PrepareLineColorMenu(mnuLineColorGreen)
        PrepareLineColorMenu(mnuLineColorBlue)
        PrepareLineColorMenu(mnuLineColorYellow)
        PrepareLineColorMenu(mnuLineColorOrange)
        PrepareLineColorMenu(mnuLineColorPurple)
        PrepareLineColorMenu(mnuLineColorBlack)
        PrepareLineColorMenu(mnuLineColorWhite)
        mnuLineColorBlack.PerformClick()

        ' Make fill color images.
        MakeFillColorImages()

        ' Set fill color menu event handlers.
        PrepareFillColorMenu(mnuFillColorRed)
        PrepareFillColorMenu(mnuFillColorGreen)
        PrepareFillColorMenu(mnuFillColorBlue)
        PrepareFillColorMenu(mnuFillColorYellow)
        PrepareFillColorMenu(mnuFillColorOrange)
        PrepareFillColorMenu(mnuFillColorPurple)
        PrepareFillColorMenu(mnuFillColorBlack)
        PrepareFillColorMenu(mnuFillColorWhite)
        mnuFillColorWhite.PerformClick()
    End Sub

    ' Make line thickness images.
    Private Sub MakeLineThicknessImages()
        m_FirstLineThicknessImage = imlToolbarButtons.Images.Count
        For i As Integer = 1 To 5
            Dim bm As New Bitmap(16, 16)
            Using gr As Graphics = Graphics.FromImage(bm)
                gr.Clear(SystemColors.Control)
                Using p As New Pen(Color.Black, i)
                    gr.DrawLine(p, 0, 8, 16, 8)
                End Using
                imlToolbarButtons.Images.Add(CType(bm.Clone(), Image))
            End Using
            bm.Dispose()
        Next i
    End Sub

    ' Add Click, MeasureItem, and DrawItem event handlers
    ' to this MenuItem.
    Private Sub PrepareThicknessMenu(ByVal menu_item As MenuItem)
        AddHandler menu_item.Click, AddressOf mnuLineThick_Click
        AddHandler menu_item.MeasureItem, AddressOf mnuLineThick_MeasureItem
        AddHandler menu_item.DrawItem, AddressOf mnuLineThick_DrawItem
        menu_item.OwnerDraw = True
    End Sub

    ' Make line color images.
    Private Sub MakeLineColorImages()
        Dim colors() As Color = {Color.Transparent,
                Color.Red, Color.Green, Color.Blue,
                Color.Yellow, Color.Orange, Color.Purple,
                Color.Black, Color.White}
        m_FirstLineColorImage = imlToolbarButtons.Images.Count
        For Each clr As Color In colors
            Dim bm As New Bitmap(16, 16)
            Using gr As Graphics = Graphics.FromImage(bm)
                gr.Clear(SystemColors.Control)
                Using p As New Pen(clr, 4)
                    gr.DrawLine(p, 0, 8, 16, 8)
                End Using
                imlToolbarButtons.Images.Add(CType(bm.Clone(), Image))
            End Using
            bm.Dispose()
        Next clr
    End Sub

    ' Add Click, MeasureItem, and DrawItem event handlers
    ' to this MenuItem.
    Private Sub PrepareLineColorMenu(ByVal menu_item As MenuItem)
        AddHandler menu_item.Click, AddressOf mnuLineColor_Click
        AddHandler menu_item.MeasureItem, AddressOf mnuLineColor_MeasureItem
        AddHandler menu_item.DrawItem, AddressOf mnuLineColor_DrawItem
        menu_item.OwnerDraw = True
    End Sub

    ' Make flll color images.
    Private Sub MakeFillColorImages()
        Dim colors() As Color = {
                Color.Red, Color.Green, Color.Blue,
                Color.Yellow, Color.Orange, Color.Purple,
                Color.Black, Color.White}
        m_FirstFillColorImage = imlToolbarButtons.Images.Count
        For Each clr As Color In colors
            Dim bm As New Bitmap(16, 16)
            Using gr As Graphics = Graphics.FromImage(bm)
                gr.Clear(clr)
                imlToolbarButtons.Images.Add(CType(bm.Clone(), Image))
            End Using
            bm.Dispose()
        Next clr
    End Sub

    ' Add Click, MeasureItem, and DrawItem event handlers
    ' to this MenuItem.
    Private Sub PrepareFillColorMenu(ByVal menu_item As MenuItem)
        AddHandler menu_item.Click, AddressOf mnuFillColor_Click
        AddHandler menu_item.MeasureItem, AddressOf mnuFillColor_MeasureItem
        AddHandler menu_item.DrawItem, AddressOf mnuFillColor_DrawItem
        menu_item.OwnerDraw = True
    End Sub
#End Region

#Region "Form and Controls events"
    ' Setup

    ' Start a new picture.
    Private Sub mnuFileNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileNew.Click
        ' See if the data is safe.
        '...

        ' Start the new picture.
        Dim oldBg = m_PictureN.BackgroundImage
        m_PictureN = New DrawablePictureN(Me.BackColor)
        If oldBg IsNot Nothing Then oldBg.Dispose()
        picCanvas.Invalidate()
    End Sub

    ' Exit.
    Private Sub mnuFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click
        ' See if the data is safe.
        '...

        Me.Close()
    End Sub

    ' Save the drawing.
    Private Sub mnuFileSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileSave.Click
        ' Let the user select the file to save into.
        If dlgSavePicture.ShowDialog() = DialogResult.OK Then
            m_PictureN.SavePicture(dlgSavePicture.FileName)
            dlgOpenPicture.InitialDirectory = dlgSavePicture.FileName
        End If
    End Sub

    ' Load a drawing.
    Private Sub mnuFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileOpen.Click
        ' Let the user select the file to load.
        If dlgOpenPicture.ShowDialog() = DialogResult.OK Then
            ' Load the picture.
            Dim new_picture As DrawablePictureN =
                    DrawablePictureN.LoadPicture(dlgOpenPicture.FileName)

            ' If we succeeded, display the new picture.
            If Not (new_picture Is Nothing) Then
                m_PictureN = new_picture
                picCanvas.Invalidate()
            End If
            dlgSavePicture.InitialDirectory = dlgOpenPicture.FileName
        End If
    End Sub

    ' Bring the selected object to the front.
    Private Sub mnuFormatOrderBringToFront_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFormatOrderBringToFront.Click
        m_PictureN.BringToFront(m_PictureN.SelectedDrawable)
        picCanvas.Invalidate()
    End Sub

    ' Set the picture's BackColor.
    Private Sub mnuOptSetBackColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOptSetBackColor.Click
        If dlgBackColor.ShowDialog() = DialogResult.OK Then
            m_PictureN.BackColor = dlgBackColor.Color
            picCanvas.Invalidate()
        End If
    End Sub

    ' Send the selected object to the back.
    Private Sub mnuFormatOrderSendToBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFormatOrderSendToBack.Click
        m_PictureN.SendToBack(m_PictureN.SelectedDrawable)
        picCanvas.Invalidate()
    End Sub

    ' Delete the selected object.
    Private Sub mnuEditDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditDelete.Click
        m_PictureN.Delete(m_PictureN.SelectedDrawable)
        picCanvas.Invalidate()
    End Sub

    Private Sub mnuLoadBackground_Click(sender As Object, e As EventArgs) Handles mnuLoadBackground.Click
        Using openDialog As New OpenFileDialog
            openDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All Files|*.*"
            If openDialog.ShowDialog() = DialogResult.OK Then
                Try
                    LoadBackgroundFromPath(openDialog.FileName)
                Catch ex As Exception
                    MessageBox.Show("Error loading image: " & ex.Message, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub
    Private Sub mnuClearBackground_Click(sender As Object, e As EventArgs) Handles mnuClearBackground.Click
        ReplaceBackgroundImage(Nothing)
        picCanvas.Invalidate()
    End Sub

    Private Sub ReplaceBackgroundImage(newImage As Image)
        Dim old = m_PictureN.BackgroundImage
        m_PictureN.BackgroundImage = newImage
        If old IsNot Nothing Then old.Dispose()
    End Sub

    'Private Sub mnuFileOpenDicomFile_Click(sender As Object, e As EventArgs) Handles mnuFileOpenDicomFile.Click
    '    Using openDialog As New OpenFileDialog
    '        openDialog.Filter = "DICOM Files|*.*|All Files|*.*"
    '        openDialog.CheckFileExists = True
    '        If openDialog.ShowDialog() = DialogResult.OK Then
    '            Try
    '                LoadDicomBackgroundFromPath(openDialog.FileName)
    '            Catch ex As Exception
    '                MessageBox.Show("Error loading DICOM: " & ex.Message, "Error",
    '                                MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End Try
    '        End If
    '    End Using
    'End Sub

    'Private Sub mnuFileOpenDicomFolder_Click(sender As Object, e As EventArgs) Handles mnuFileOpenDicomFolder.Click
    '    Using folderDialog As New FolderBrowserDialog
    '        If folderDialog.ShowDialog() = DialogResult.OK Then
    '            Try
    '                Dim dicomPath = FindFirstDicomFile(folderDialog.SelectedPath)
    '                If String.IsNullOrWhiteSpace(dicomPath) Then
    '                    Throw New Exception("No DICOM files found in the selected folder.")
    '                End If
    '                LoadDicomBackgroundFromPath(dicomPath)
    '            Catch ex As Exception
    '                MessageBox.Show("Error loading DICOM folder: " & ex.Message, "Error",
    '                                MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End Try
    '        End If
    '    End Using
    'End Sub

    Private Sub mnuLayoutNone_Click(sender As Object, e As EventArgs) Handles mnuLayoutNone.Click
        'Dim menuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        m_PictureN.BackgroundImageLayout = ImageLayout.None 'DirectCast(menuItem.Tag, ImageLayout)
        picCanvas.Invalidate()
    End Sub
    Private Sub mnuLayoutCenter_Click(sender As Object, e As EventArgs) Handles mnuLayoutCenter.Click
        'Dim menuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        m_PictureN.BackgroundImageLayout = ImageLayout.Center 'DirectCast(menuItem.Tag, ImageLayout)
        picCanvas.Invalidate()
    End Sub
    Private Sub mnuLayoutStretch_Click(sender As Object, e As EventArgs) Handles mnuLayoutStretch.Click
        'Dim menuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        m_PictureN.BackgroundImageLayout = ImageLayout.Stretch 'DirectCast(menuItem.Tag, ImageLayout)
        picCanvas.Invalidate()
    End Sub
    Private Sub mnuLayoutTile_Click(sender As Object, e As EventArgs) Handles mnuLayoutTile.Click
        'Dim menuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        m_PictureN.BackgroundImageLayout = ImageLayout.Tile 'DirectCast(menuItem.Tag, ImageLayout)
        picCanvas.Invalidate()
    End Sub
    Private Sub mnuLayoutZoom_Click(sender As Object, e As EventArgs) Handles mnuLayoutZoom.Click
        'Dim menuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        m_PictureN.BackgroundImageLayout = ImageLayout.Zoom 'DirectCast(menuItem.Tag, ImageLayout)
        picCanvas.Invalidate()
    End Sub

    Private Sub mnuFont_Click(sender As Object, e As EventArgs) Handles mnuFont.Click
        If m_PictureN.SelectedDrawable IsNot Nothing AndAlso
           TypeOf m_PictureN.SelectedDrawable Is DrawableTextN Then

            Dim textObj As DrawableTextN = DirectCast(m_PictureN.SelectedDrawable, DrawableTextN)
            Using dlgFont As New FontDialog()
                dlgFont.Font = textObj.Font
                dlgFont.Color = textObj.ForeColor
                dlgFont.ShowColor = True

                If dlgFont.ShowDialog() = DialogResult.OK Then
                    textObj.Font = dlgFont.Font
                    textObj.ForeColor = dlgFont.Color
                    picCanvas.Invalidate()
                End If
            End Using
        End If
    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        ' See what kind of button this is.
        If e.Button.Style = ToolBarButtonStyle.ToggleButton Then
            ' ToggleButton.
            If (m_SelectedToolButton Is e.Button) Then
                e.Button.Pushed = True
            Else
                ' Pop the previously selected button up.
                m_SelectedToolButton.Pushed = False

                ' Save a reference to the selected tool.
                m_SelectedToolButton = e.Button
            End If

            ' Deselect any selected Drawable.
            If Not (m_PictureN.SelectedDrawable Is Nothing) Then
                m_PictureN.SelectedDrawable = Nothing
                picCanvas.Invalidate()
            End If
        ElseIf e.Button.Style = ToolBarButtonStyle.PushButton Then
            ' PushButton.
            Select Case e.Button.ToolTipText
                Case "Bring To Front"
                    m_PictureN.BringToFront(m_PictureN.SelectedDrawable)
                    picCanvas.Invalidate()
                Case "Send To Back"
                    m_PictureN.SendToBack(m_PictureN.SelectedDrawable)
                    picCanvas.Invalidate()
                Case "Delete"
                    m_PictureN.Delete(m_PictureN.SelectedDrawable)
                    picCanvas.Invalidate()
            End Select
        End If
    End Sub

#End Region

#Region "New object event handlers"

#Region "Cropping"


    Private Function GetCropHandlePoints(rect As Rectangle) As List(Of Rectangle)
        Dim size = cropHandleSize
        Return New List(Of Rectangle) From {
        New Rectangle(rect.Left - size \ 2, rect.Top - size \ 2, size, size), ' TopLeft
        New Rectangle(rect.Right - size \ 2, rect.Top - size \ 2, size, size), ' TopRight
        New Rectangle(rect.Left - size \ 2, rect.Bottom - size \ 2, size, size), ' BottomLeft
        New Rectangle(rect.Right - size \ 2, rect.Bottom - size \ 2, size, size), ' BottomRight
        New Rectangle(rect.Left + rect.Width \ 2 - size \ 2, rect.Top - size \ 2, size, size), ' Top
        New Rectangle(rect.Left + rect.Width \ 2 - size \ 2, rect.Bottom - size \ 2, size, size), ' Bottom
        New Rectangle(rect.Left - size \ 2, rect.Top + rect.Height \ 2 - size \ 2, size, size), ' Left
        New Rectangle(rect.Right - size \ 2, rect.Top + rect.Height \ 2 - size \ 2, size, size)  ' Right
    }
    End Function


    Private Function NormalizeRect(r As Rectangle) As Rectangle
        Dim x = Math.Min(r.Left, r.Right)
        Dim y = Math.Min(r.Top, r.Bottom)
        Dim w = Math.Abs(r.Width)
        Dim h = Math.Abs(r.Height)
        Return New Rectangle(x, y, w, h)
    End Function

    Private Sub CropBackgroundImage(rect As Rectangle)
        If m_PictureN.BackgroundImage Is Nothing Then Return

        ' Map from control (canvas) space to image space
        Dim img = m_PictureN.BackgroundImage
        Dim imgRect As Rectangle

        Select Case m_PictureN.BackgroundImageLayout
            Case ImageLayout.Stretch
                imgRect = New Rectangle(0, 0, picCanvas.Width, picCanvas.Height)

            Case ImageLayout.Zoom
                Dim ratioX = picCanvas.Width / img.Width
                Dim ratioY = picCanvas.Height / img.Height
                Dim ratio = Math.Min(ratioX, ratioY)
                Dim newWidth = CInt(img.Width * ratio)
                Dim newHeight = CInt(img.Height * ratio)
                Dim offsetX = (picCanvas.Width - newWidth) \ 2
                Dim offsetY = (picCanvas.Height - newHeight) \ 2
                imgRect = New Rectangle(offsetX, offsetY, newWidth, newHeight)

            Case Else
                imgRect = New Rectangle(0, 0, img.Width, img.Height)
        End Select

        ' Convert crop rect from canvas to image coordinates
        Dim scaleX = img.Width / imgRect.Width
        Dim scaleY = img.Height / imgRect.Height
        Dim cropInImage = New Rectangle(
        CInt((rect.X - imgRect.X) * scaleX),
        CInt((rect.Y - imgRect.Y) * scaleY),
        CInt(rect.Width * scaleX),
        CInt(rect.Height * scaleY)
    )

        ' Clamp crop to image bounds
        cropInImage.Intersect(New Rectangle(0, 0, img.Width, img.Height))
        If cropInImage.Width <= 0 OrElse cropInImage.Height <= 0 Then Exit Sub

        ' Crop the image
        Dim croppedBmp As New Bitmap(cropInImage.Width, cropInImage.Height)
        Using g As Graphics = Graphics.FromImage(croppedBmp)
            g.DrawImage(img, New Rectangle(0, 0, croppedBmp.Width, croppedBmp.Height), cropInImage, GraphicsUnit.Pixel)
        End Using

        ReplaceBackgroundImage(croppedBmp)
        picCanvas.Invalidate()
    End Sub


    Private Sub KeepOnlyShapesInside(rect As Rectangle)

        Using g As Graphics = picCanvas.CreateGraphics()
            m_PictureN.Drawables = m_PictureN.Drawables.
            Where(Function(d) rect.IntersectsWith(Rectangle.Round(d.GetScreenBounds(g)))).ToList()
        End Using
        picCanvas.Invalidate()

    End Sub

    Private Sub RemoveShapesOutside(rect As Rectangle)
        Using g As Graphics = picCanvas.CreateGraphics()
            m_PictureN.Drawables = m_PictureN.Drawables.
            Where(Function(d) rect.Contains(Rectangle.Round(d.GetScreenBounds(g)))).ToList()
        End Using
        picCanvas.Invalidate()
    End Sub
#End Region
    ' Redraw.
    Private Sub picCanvas_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picCanvas.Paint

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality

        If m_PictureN IsNot Nothing Then
            m_PictureN.Draw(e.Graphics) ' ✅ this draws background and shapes
        End If
        ' Draw crop rectangle
        'If isCropping AndAlso cropRect.Width > 0 AndAlso cropRect.Height > 0 Then
        '    Using pen As New Pen(Color.Red, 2) With {.DashStyle = DashStyle.Dash}
        '        e.Graphics.DrawRectangle(pen, cropRect)
        '    End Using
        'End If
        If isCropping AndAlso Not cropRect.IsEmpty Then
            Using pen As New Pen(Color.Red, 2)
                e.Graphics.DrawRectangle(pen, cropRect)
            End Using

            ' Draw handles
            For Each pt In GetCropHandlePoints(cropRect)
                e.Graphics.FillRectangle(Brushes.White, pt)
                e.Graphics.DrawRectangle(Pens.Black, pt)
            Next
        End If

    End Sub

    ' Perform an action depending on the currently pushed tool.
    Private Sub picCanvas_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCanvas.MouseDown
        ' See which button was pressed.
        If e.Button = MouseButtons.Right Then
            ' Right button. See if we're drawing something.
            If m_NewDrawableN Is Nothing Then
                ' We are not drawing. Ignore this button.
            Else
                ' We are drawing something. Cancel it.
                m_PictureN.Remove(m_NewDrawableN)
                m_NewDrawableN = Nothing
                ' Redraw to erase the new object.
                picCanvas.Invalidate()
            End If
        Else
            ' Left button. See which tool is pushed.
            Select Case m_SelectedToolButton.ToolTipText
                Case "Pointer"
                    ' Reset new object flag
                    isCreatingNewObject = False
                    ' Select an object and check for anchors and start transform
                    Dim selected = m_PictureN.SelectObjectAt(e.X, e.Y)
                    If selected IsNot Nothing Then
                        Dim anchor = selected.GetAnchorAt(e.X, e.Y)
                        transformationController.StartTransform(selected, anchor, e.Location)
                    End If
                Case "PolyLine"
                    ' Start drawing a line.
                    isCreatingNewObject = True
                    m_NewDrawableN = New DrawablePolylineN(m_CurrentForeColor, m_CurrentLineWidth, e.X, e.Y, e.X, e.Y)
                    m_PictureN.Add(m_NewDrawableN)
                Case "Arrow"
                    ' Start drawing a line.
                    isCreatingNewObject = True
                    m_NewDrawableN = New DrawableArrowN(m_CurrentForeColor, m_CurrentFillColor, m_CurrentLineWidth, e.X, e.Y, e.X, e.Y) With {
                                                                                                    .Text = "Hello World!",
                                                                                                    .TextColor = Color.DarkBlue,
                                                                                                    .TextFont = New Font("Verdana", 14, FontStyle.Bold),
                                                                                                    .TextAlignment = ContentAlignment.MiddleCenter
                                                                                                }
                    m_PictureN.Add(m_NewDrawableN)
                Case "Line"
                    ' Start drawing a line.
                    isCreatingNewObject = True
                    m_NewDrawableN = New DrawableLineN(m_CurrentForeColor, m_CurrentLineWidth, e.X, e.Y, e.X, e.Y)
                    m_PictureN.Add(m_NewDrawableN)
                Case "Rectangle"
                    ' Start drawing a rectangle.
                    isCreatingNewObject = True
                    m_NewDrawableN = New DrawableRectangleN(m_CurrentForeColor, m_CurrentFillColor, m_CurrentLineWidth, e.X, e.Y, e.X, e.Y) With {
                                                                                                    .Text = "Hello World!",
                                                                                                    .TextColor = Color.DarkBlue,
                                                                                                    .TextFont = New Font("Verdana", 14, FontStyle.Bold),
                                                                                                    .TextAlignment = ContentAlignment.MiddleCenter
                                                                                                }
                    m_PictureN.Add(m_NewDrawableN)
                Case "Ellipse"
                    ' Start drawing an ellipse.
                    isCreatingNewObject = True
                    m_NewDrawableN = New DrawableEllipseN(m_CurrentForeColor, m_CurrentFillColor, m_CurrentLineWidth, e.X, e.Y, e.X, e.Y) With {
                    .Text = "Ellipse Text",
                        .TextColor = Color.DarkRed,
                        .TextFont = New Font("Tahoma", 14, FontStyle.Italic),
                        .TextAlignment = ContentAlignment.MiddleCenter,
                        .RotationAngle = 0
                    }
                    m_PictureN.Add(m_NewDrawableN)
                Case "Star"
                    ' Start drawing a star.
                    isCreatingNewObject = True
                    m_NewDrawableN = New DrawableStarN(m_CurrentForeColor, m_CurrentFillColor, m_CurrentLineWidth, e.X, e.Y, e.X, e.Y) With {
                    .Text = "Ellipse Text",
                        .TextColor = Color.DarkRed,
                        .TextFont = New Font("Tahoma", 14, FontStyle.Italic),
                        .TextAlignment = ContentAlignment.MiddleCenter,
                        .RotationAngle = 0
                    }
                    m_PictureN.Add(m_NewDrawableN)
                Case "Image"
                    ' Start drawing an image.
                    isCreatingNewObject = True
                    m_NewDrawableN = New DrawableImageN(e.X, e.Y, e.X, e.Y)
                    m_PictureN.Add(m_NewDrawableN)
                Case "FreeHand"
                    ' Start freehand drawing
                    isCreatingNewObject = True
                    m_NewDrawableN = New DrawableFreehandN(m_CurrentForeColor, m_CurrentLineWidth)
                    ' Add initial point
                    DirectCast(m_NewDrawableN, DrawableFreehandN).AddPoint(New Point(e.X, e.Y))
                    m_PictureN.Add(m_NewDrawableN)
                Case "Text"
                    ' Start drawing text rectangle
                    m_NewDrawableN = New DrawableTextN(m_CurrentForeColor,
                                       New Font("Arial", 12),
                                       "",
                                       e.X, e.Y, e.X, e.Y) ' Initialize with same point
                    m_IsCreatingTextRect = True
                    m_TextRectStart = New Point(e.X, e.Y)
                    m_PictureN.Add(m_NewDrawableN)
                    picCanvas.Invalidate()
                    ' Start text editing
                    StartTextEditing(DirectCast(m_NewDrawableN, DrawableTextN))
                Case "CropBackground"
                    cropActionPending = "CropBackground"
                    cropStart = e.Location
                    isCropping = True
                    If isCropping Then
                        Dim crophandles = GetCropHandlePoints(cropRect)
                        For i = 0 To crophandles.Count - 1
                            If crophandles(i).Contains(e.Location) Then
                                cropAnchor = CType(i + 1, CropAnchors) ' Because 0 is CropAnchor.None
                                Exit Sub
                            End If
                        Next

                        ' If clicked inside, allow move
                        If cropRect.Contains(e.Location) Then
                            cropAnchor = CropAnchors.Move
                            cropStart = e.Location
                            Exit Sub
                        End If
                    End If

                    ' Start new crop
                    cropAnchor = CropAnchors.BottomRight
                    cropStart = e.Location
                    cropRect = New Rectangle(e.X, e.Y, 0, 0)
                    isCropping = True
                    picCanvas.Invalidate()
                Case "KeepShapes"
                    cropActionPending = "KeepShapes"
                    cropStart = e.Location
                    isCropping = True

                Case "RemoveShapes"
                    cropActionPending = "RemoveShapes"
                    cropStart = e.Location
                    isCropping = True

            End Select

            ' Redraw.
            picCanvas.Invalidate()
        End If
    End Sub

    ' Handle mouse movement for transformations and object creation
    Private Sub picCanvas_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCanvas.MouseMove
        ' In MouseMove event handler
        Static lastUpdate As DateTime = DateTime.MinValue
        If (DateTime.Now - lastUpdate).TotalMilliseconds < 20 Then Return ' 50 FPS throttle
        lastUpdate = DateTime.Now

        ' Handle transformations
        If e.Button = MouseButtons.Left AndAlso transformationController.IsTransforming AndAlso m_PictureN.SelectedDrawable IsNot Nothing Then
            transformationController.UpdateTransform(e.Location)
            picCanvas.Invalidate()
        ElseIf m_SelectedToolButton.ToolTipText = "Pointer" AndAlso m_PictureN.SelectedDrawable IsNot Nothing Then
            'Update cursor based on anchor
            Dim anchor = m_PictureN.SelectedDrawable.GetAnchorAt(e.X, e.Y)
            picCanvas.Cursor = GetAnchorCursor(anchor)
        End If
        ' Handle new object creation
        If isCreatingNewObject AndAlso m_NewDrawableN IsNot Nothing AndAlso e.Button = MouseButtons.Left Then
            Dim newX As Integer = e.X
            Dim newY As Integer = e.Y

            If TypeOf m_NewDrawableN Is DrawableFreehandN Then
                m_NewDrawableN.NewPoint(newX, newY)
            Else
                ' Get the starting point
                Dim startX As Integer = m_NewDrawableN.X1
                Dim startY As Integer = m_NewDrawableN.Y1
                ' Handle different constraint types
                If TypeOf m_NewDrawableN Is DrawableLineN AndAlso Control.ModifierKeys = Keys.Shift Then
                    ' Line angle constraint (15-degree increments)
                    Dim angle = Math.Atan2(newY - startY, newX - startX) * (180 / Math.PI)
                    Dim constrainedAngle = Math.Round(angle / 15) * 15
                    Dim radians = constrainedAngle * Math.PI / 180
                    Dim length = Geometry.Distance(New Point(startX, startY), New Point(newX, newY))

                    newX = startX + CInt(length * Math.Cos(radians))
                    newY = startY + CInt(length * Math.Sin(radians))

                ElseIf (TypeOf m_NewDrawableN Is DrawableRectangleN OrElse
                    TypeOf m_NewDrawableN Is DrawableEllipseN) AndAlso
                    Control.ModifierKeys = Keys.Shift Then
                    ' Aspect ratio constraint (square/circle)
                    Dim size = Math.Max(Math.Abs(newX - startX), Math.Abs(newY - startY))
                    newX = If(newX > startX, startX + size, startX - size)
                    newY = If(newY > startY, startY + size, startY - size)

                ElseIf Control.ModifierKeys = Keys.Shift Then
                    ' Default constraint (horizontal/vertical)
                    Dim dx = newX - startX
                    Dim dy = newY - startY

                    If Math.Abs(dx) > Math.Abs(dy) Then
                        newY = startY ' Horizontal constraint
                    Else
                        newX = startX ' Vertical constraint
                    End If
                End If

                ' Update the point with potential constraints
                m_NewDrawableN.NewPoint(newX, newY)
            End If

            ' Redraw to show updates
            picCanvas.Invalidate()
        End If
        If m_IsCreatingTextRect AndAlso m_NewDrawableN IsNot Nothing Then
            ' Update text rectangle bounds
            Dim textObj = DirectCast(m_NewDrawableN, DrawableTextN)
            textObj.X2 = e.X
            textObj.Y2 = e.Y
            picCanvas.Invalidate()
        End If
        ' Update cursor for pointer tool
        If Not transformationController.IsTransforming AndAlso m_SelectedToolButton.ToolTipText = "Pointer" Then
            If m_PictureN.SelectedDrawable IsNot Nothing Then
                Dim anchor = m_PictureN.SelectedDrawable.GetAnchorAt(e.X, e.Y)
                picCanvas.Cursor = GetAnchorCursor(anchor)
            Else
                picCanvas.Cursor = Cursors.Default
            End If
        End If
        If isCropping AndAlso cropAnchor <> CropAnchors.None AndAlso e.Button = MouseButtons.Left Then
            Dim dx = e.X - cropStart.X
            Dim dy = e.Y - cropStart.Y

            Dim r = cropRect

            Select Case cropAnchor
                Case CropAnchors.Move
                    r.Offset(dx, dy)
                Case CropAnchors.TopLeft
                    r.X += dx : r.Width -= dx
                    r.Y += dy : r.Height -= dy
                Case CropAnchors.TopRight
                    r.Width += dx
                    r.Y += dy : r.Height -= dy
                Case CropAnchors.BottomLeft
                    r.X += dx : r.Width -= dx
                    r.Height += dy
                Case CropAnchors.BottomRight
                    r.Width += dx
                    r.Height += dy
                Case CropAnchors.Left
                    r.X += dx : r.Width -= dx
                Case CropAnchors.Right
                    r.Width += dx
                Case CropAnchors.Top
                    r.Y += dy : r.Height -= dy
                Case CropAnchors.Bottom
                    r.Height += dy
            End Select

            cropRect = NormalizeRect(r)
            cropStart = e.Location
            picCanvas.Invalidate()
        End If

    End Sub

    '' Finish transformations or object creation
    Private Sub picCanvas_MouseUp(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseUp
        ' Handle text rectangle creation FIRST
        If m_IsCreatingTextRect Then
            m_IsCreatingTextRect = False

            ' Null check before casting
            If m_NewDrawableN IsNot Nothing AndAlso TypeOf m_NewDrawableN Is DrawableTextN Then
                Dim textObj = DirectCast(m_NewDrawableN, DrawableTextN)

                ' Ensure minimum size
                If Math.Abs(textObj.X2 - textObj.X1) < 10 Then textObj.X2 = textObj.X1 + 100
                If Math.Abs(textObj.Y2 - textObj.Y1) < 10 Then textObj.Y2 = textObj.Y1 + 30

                StartTextEditing(textObj)
            Else
                ' Clean up invalid object
                If m_NewDrawableN IsNot Nothing Then
                    m_PictureN.Remove(m_NewDrawableN)
                End If
            End If

            ' Prevent general cleanup from clearing text object reference
            isCreatingNewObject = False
        End If

        ' Handle image creation AFTER text handling
        If TypeOf m_NewDrawableN Is DrawableImageN Then
            Dim drawableImage As DrawableImageN = DirectCast(m_NewDrawableN, DrawableImageN)

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Try
                    drawableImage.Picture = New Bitmap(ofdImage.FileName)
                Catch ex As Exception
                    m_PictureN.Remove(m_NewDrawableN)
                End Try
            Else
                m_PictureN.Remove(m_NewDrawableN)
            End If
        End If

        ' Finalize transformations
        transformationController.EndTransform()

        ' Clean up ONLY if still in creation mode
        If isCreatingNewObject Then
            isCreatingNewObject = False
            m_NewDrawableN = Nothing
        End If
        If isCropping Then
            isCropping = False
            picCanvas.Invalidate()

            Select Case cropActionPending
                Case "CropBackground"
                    CropBackgroundImage(cropRect)
                Case "KeepShapes"
                    KeepOnlyShapesInside(cropRect)
                Case "RemoveShapes"
                    RemoveShapesOutside(cropRect)
            End Select
            cropAnchor = CropAnchors.None
            cropActionPending = ""
            cropRect = Rectangle.Empty
        End If
        picCanvas.Invalidate()
    End Sub

    Private Function GetAnchorCursor(anchor As AnchorEnumN) As Cursor
        Select Case anchor
            Case AnchorEnumN.Move
                Return Cursors.SizeAll
            Case AnchorEnumN.Nwse, AnchorEnumN.Senw
                Return Cursors.SizeNWSE
            Case AnchorEnumN.Nesw, AnchorEnumN.Swne
                Return Cursors.SizeNESW
            Case AnchorEnumN.Ns, AnchorEnumN.Sn
                Return Cursors.SizeNS
            Case AnchorEnumN.Ew, AnchorEnumN.We
                Return Cursors.SizeWE
            Case AnchorEnumN.Rotate
                Return Cursors.Cross
            Case Else
                Return Cursors.Default
        End Select
    End Function


    ' On mouse move, continue drawing new objects
    Private Sub NewDrawable_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        ' This is now handled in the main MouseMove event
    End Sub

    ' On mouse up, finish drawing the new object.

    Private Sub NewDrawable_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        ' No longer watch for MouseMove or MouseUp.
        ' This is now handled in the main MouseUp event

    End Sub


#Region "Text"
    ' ... [Text editing code remains unchanged] ...
#End Region
#End Region ' New object event handlers

#Region "Text"

    Private Sub StartTextEditing1(textObj As DrawableTextN)
        ' Create a textbox for editing
        Dim txtEdit As New TextBox()
        txtEdit.Multiline = True
        txtEdit.Font = textObj.Font
        txtEdit.ForeColor = textObj.ForeColor
        txtEdit.Text = textObj.Text
        txtEdit.Bounds = textObj.GetBounds()
        txtEdit.BorderStyle = BorderStyle.FixedSingle
        txtEdit.Tag = textObj

        ' Handle events
        AddHandler txtEdit.KeyDown, AddressOf TextEdit_KeyDown
        AddHandler txtEdit.LostFocus, AddressOf TextEdit_LostFocus

        ' Add to canvas
        picCanvas.Controls.Add(txtEdit)
        txtEdit.BringToFront()
        txtEdit.Focus()
        txtEdit.SelectAll()
    End Sub
    Private Sub StartTextEditing(textObj As DrawableTextN)
        Dim txtEdit As New TextBox()
        txtEdit.Multiline = True
        txtEdit.Font = textObj.Font
        txtEdit.ForeColor = textObj.ForeColor
        txtEdit.Text = textObj.Text

        txtEdit.BorderStyle = BorderStyle.FixedSingle
        txtEdit.Tag = textObj
        '===========
        'txtEdit.Bounds = textObj.GetBounds()
        ' Ensure valid bounds
        Dim bounds = textObj.GetBounds()
        If bounds.Width <= 0 Then bounds.Width = 100
        If bounds.Height <= 0 Then bounds.Height = 30
        txtEdit.Bounds = bounds
        ' Auto-resize when typing
        AddHandler txtEdit.TextChanged, Sub(sender, e)
                                            Dim tb = DirectCast(sender, TextBox)
                                            Using g = tb.CreateGraphics()
                                                Dim size = g.MeasureString(tb.Text, tb.Font, tb.Width)
                                                tb.Height = Math.Max(CInt(size.Height) + 5, textObj.GetBounds().Height)
                                            End Using
                                        End Sub

        ' Add event handlers
        AddHandler txtEdit.KeyDown, AddressOf TextEdit_KeyDown
        AddHandler txtEdit.LostFocus, AddressOf TextEdit_LostFocus

        ' Add to canvas
        picCanvas.Controls.Add(txtEdit)
        txtEdit.BringToFront()
        txtEdit.Focus()
    End Sub
    Private Sub TextEdit_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter AndAlso Not e.Shift Then
            e.Handled = True
            e.SuppressKeyPress = True
            FinishTextEditing(DirectCast(sender, TextBox))
        ElseIf e.KeyCode = Keys.Escape Then
            e.Handled = True
            e.SuppressKeyPress = True
            CancelTextEditing(DirectCast(sender, TextBox))
        End If
    End Sub

    Private Sub TextEdit_LostFocus(sender As Object, e As EventArgs)
        FinishTextEditing(DirectCast(sender, TextBox))
    End Sub

    Private Sub FinishTextEditing(txtEdit As TextBox)
        Dim textObj As DrawableTextN = DirectCast(txtEdit.Tag, DrawableTextN)
        textObj.Text = txtEdit.Text
        textObj.Font = txtEdit.Font
        textObj.ForeColor = txtEdit.ForeColor

        ' Auto-adjust height to fit text
        Using g = picCanvas.CreateGraphics()
            textObj.AdjustSizeToText(g)
        End Using

        picCanvas.Controls.Remove(txtEdit)
        picCanvas.Invalidate()
        m_NewDrawableN = Nothing
    End Sub
    Private Sub FinishTextEditing2(txtEdit As TextBox)
        Dim textObj As DrawableTextN = DirectCast(txtEdit.Tag, DrawableTextN)
        textObj.Text = txtEdit.Text
        textObj.Font = txtEdit.Font
        textObj.ForeColor = txtEdit.ForeColor

        ' Update bounds to fit content
        Using g = picCanvas.CreateGraphics()
            Dim size = g.MeasureString(textObj.Text, textObj.Font, textObj.GetBounds().Width)
            textObj.Y2 = textObj.Y1 + CInt(size.Height)
        End Using

        picCanvas.Controls.Remove(txtEdit)
        picCanvas.Invalidate()
        m_NewDrawableN = Nothing
    End Sub

    Private Sub CancelTextEditing(txtEdit As TextBox)
        Dim textObj As DrawableTextN = DirectCast(txtEdit.Tag, DrawableTextN)
        m_PictureN.Remove(textObj)
        picCanvas.Controls.Remove(txtEdit)
        picCanvas.Invalidate()
        m_NewDrawableN = Nothing
    End Sub
    Private Sub FinishTextEditing1(txtEdit As TextBox)
        Dim textObj As DrawableTextN = DirectCast(txtEdit.Tag, DrawableTextN)
        textObj.Text = txtEdit.Text
        textObj.Font = txtEdit.Font
        textObj.ForeColor = txtEdit.ForeColor
        picCanvas.Controls.Remove(txtEdit)
        picCanvas.Invalidate()
        m_NewDrawableN = Nothing
    End Sub

    Private Sub CancelTextEditing1(txtEdit As TextBox)
        Dim textObj As DrawableTextN = DirectCast(txtEdit.Tag, DrawableTextN)
        m_PictureN.Remove(textObj)
        picCanvas.Controls.Remove(txtEdit)
        picCanvas.Invalidate()
        m_NewDrawableN = Nothing
    End Sub
#End Region

#Region "Line Thickness Menu Routines"
    ' The user has selected a new line thickness.
    Private Sub mnuLineThick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Update the current pen.
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        m_CurrentLineWidth = Integer.Parse(menu_item.Text)

        ' Update the toolbar display.
        tcboThickness.ImageIndex = m_CurrentLineWidth +
                m_FirstLineThicknessImage - 1

        ' Update the selected object if there is one.
        If Not (m_PictureN.SelectedDrawable Is Nothing) Then
            m_PictureN.SelectedDrawable.LineWidth = m_CurrentLineWidth
            picCanvas.Invalidate()
        End If

        ' Reselect the currently selected tool.
        m_SelectedToolButton.Pushed = True
    End Sub

    ' Allow room for the MenuItem.
    Private Sub mnuLineThick_MeasureItem(ByVal sender As Object, ByVal e As System.Windows.Forms.MeasureItemEventArgs)
        e.ItemWidth = 16
        e.ItemHeight = 16
    End Sub

    ' Draw the menu item.
    Private Sub mnuLineThick_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        Dim thickness As Integer = Integer.Parse(menu_item.Text)

        ' See if we're selected.
        Dim fg_pen As Pen
        Dim bg_brush As Brush
        If (e.State And DrawItemState.Selected) = 0 Then
            ' Not selected.
            ' Use a light background and dark foreground.
            fg_pen = New Pen(SystemColors.MenuText, thickness)
            bg_brush = New SolidBrush(SystemColors.Menu)
        Else
            ' Selected.
            ' Use a dark background and light foreground.
            fg_pen = New Pen(SystemColors.HighlightText, thickness)
            bg_brush = New SolidBrush(SystemColors.Highlight)
        End If

        ' Erase the background.
        e.Graphics.FillRectangle(bg_brush, e.Bounds)

        ' Draw the line.
        Dim y As Integer = e.Bounds.Y + e.Bounds.Height \ 2
        e.Graphics.DrawLine(fg_pen, e.Bounds.X, y, e.Bounds.Right, y)

        fg_pen.Dispose()
        bg_brush.Dispose()
    End Sub
#End Region ' Line Thickness Menu Routines

#Region "Line Color Menu Routines"
    ' The user has selected a new line color.
    Private Sub mnuLineColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Update the current pen.
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        m_CurrentForeColor = Color.FromName(menu_item.Text)

        ' Update the toolbar display.
        Select Case menu_item.Text
            Case "Red"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 0
            Case "Green"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 1
            Case "Blue"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 2
            Case "Yellow"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 3
            Case "Orange"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 4
            Case "Purple"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 5
            Case "Black"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 6
            Case "White"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 7
        End Select

        ' Update the selected object if there is one.
        If Not (m_PictureN.SelectedDrawable Is Nothing) Then
            m_PictureN.SelectedDrawable.ForeColor = m_CurrentForeColor
            picCanvas.Invalidate()
        End If

        ' Reselect the currently selected tool.
        m_SelectedToolButton.Pushed = True
    End Sub

    ' Allow room for the MenuItem.
    Private Sub mnuLineColor_MeasureItem(ByVal sender As Object, ByVal e As System.Windows.Forms.MeasureItemEventArgs)
        e.ItemWidth = 16
        e.ItemHeight = 16
    End Sub

    ' Draw the menu item.
    Private Sub mnuLineColor_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        Dim new_color As Color = Color.FromName(menu_item.Text)

        ' See if we're selected.
        Dim bg_brush As Brush
        If (e.State And DrawItemState.Selected) = 0 Then
            ' Not selected.
            ' Use a light background.
            bg_brush = New SolidBrush(SystemColors.Menu)
        Else
            ' Selected.
            ' Use a dark background.
            bg_brush = New SolidBrush(SystemColors.Highlight)
        End If

        ' Erase the background.
        e.Graphics.FillRectangle(bg_brush, e.Bounds)

        ' Draw the line.
        Dim y As Integer = e.Bounds.Y + e.Bounds.Height \ 2
        Dim fg_pen As New Pen(new_color, 4)
        e.Graphics.DrawLine(fg_pen, e.Bounds.X, y, e.Bounds.Right, y)
        fg_pen.Dispose()
    End Sub
#End Region ' Line Color Menu Routines

#Region "Fill Color Menu Routines"
    ' The user has selected a new line color.
    Private Sub mnuFillColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Update the current pen.
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        m_CurrentFillColor = Color.FromName(menu_item.Text)

        ' Update the toolbar display.
        Select Case menu_item.Text
            Case "Red"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 0
            Case "Green"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 1
            Case "Blue"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 2
            Case "Yellow"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 3
            Case "Orange"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 4
            Case "Purple"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 5
            Case "Black"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 6
            Case "White"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 7
        End Select

        ' Update the selected object if there is one.
        If Not (m_PictureN.SelectedDrawable Is Nothing) Then
            m_PictureN.SelectedDrawable.FillColor = m_CurrentFillColor
            picCanvas.Invalidate()
        End If

        ' Reselect the currently selected tool.
        m_SelectedToolButton.Pushed = True
    End Sub

    ' Allow room for the MenuItem.
    Private Sub mnuFillColor_MeasureItem(ByVal sender As Object, ByVal e As System.Windows.Forms.MeasureItemEventArgs)
        e.ItemWidth = 16
        e.ItemHeight = 16
    End Sub

    ' Draw the menu item.
    Private Sub mnuFillColor_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        Dim new_color As Color = Color.FromName(menu_item.Text)

        ' See if we're selected.
        Dim bg_brush As Brush
        If (e.State And DrawItemState.Selected) = 0 Then
            ' Not selected.
            ' Use a light background.
            bg_brush = New SolidBrush(SystemColors.Menu)
        Else
            ' Selected.
            ' Use a dark background.
            bg_brush = New SolidBrush(SystemColors.Highlight)
        End If

        ' Erase the background.
        e.Graphics.FillRectangle(bg_brush, e.Bounds)

        ' Draw a filled area.
        Dim fg_brush As New SolidBrush(new_color)
        e.Graphics.FillRectangle(fg_brush,
                e.Bounds.X + 2,
                e.Bounds.Y + 2,
                e.Bounds.Width - 4,
                e.Bounds.Height - 4)
        fg_brush.Dispose()
    End Sub
#End Region ' Fill Color Menu Routines

#Region "Edit Format Print Menu Routines"


    ' Display the print preview dialog.
    ' Note that ppdPreview.Document = pdPrint was set in the form designer.
    Private Sub mnuFilePrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilePrintPreview.Click
        ppdPrint.ShowDialog()
    End Sub

    ' Print.
    ' Note: PRINT_ENLARGED cannot be True unless PRINT_CENTERED is also.
    ' Note: If PRINT_CENTERED is False, the picture is drawn
    ' in the upper left corner where it will be chopped because
    ' the printer cannot print all the way to the edges of the paper.
    Private Sub pdPrint_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdPrint.PrintPage
#Const PRINT_CENTERED = True
#Const PRINT_ENLARGED = True
        '#Const PRINT_MARGIN = True

#If PRINT_CENTERED Then ' Center the picture.
        ' Get the picture's bounds.
        Dim bounds As RectangleF = m_PictureN.GetBounds()

        ' Translate the drawing to the origin.
        e.Graphics.TranslateTransform(-bounds.X, -bounds.Y)

        Dim scale As Single = 1

#If PRINT_ENLARGED Then ' Scale to fit.
        Dim xscale As Double = 1
        If bounds.Width > 0 Then xscale = e.MarginBounds.Width / bounds.Width
        Dim yscale As Double = 1
        If bounds.Height > 0 Then yscale = e.MarginBounds.Height / bounds.Height

        If xscale > yscale Then
            scale = CSng(yscale)
        Else
            scale = CSng(xscale)
        End If
        e.Graphics.ScaleTransform(scale, scale, Drawing2D.MatrixOrder.Append)
#End If

        ' Translate to center the drawing.
        Dim cx As Integer = CInt((e.MarginBounds.Width - bounds.Width * scale) / 2)
        Dim cy As Integer = CInt((e.MarginBounds.Height - bounds.Height * scale) / 2)
        e.Graphics.TranslateTransform(
                e.MarginBounds.X + cx,
                e.MarginBounds.Y + cy,
                Drawing2D.MatrixOrder.Append)
#End If

        ' Draw the picture.
        m_PictureN.Draw(e.Graphics)

#If PRINT_MARGIN Then
        ' Draw the margin.
        e.Graphics.ResetTransform()
        Using margin_pen As New Pen(Color.Red)
            margin_pen.DashPattern = New Single() {5, 5}
            e.Graphics.DrawRectangle(margin_pen, e.MarginBounds)
        End Using
#End If
    End Sub

    ' Print.
    Private Sub mnuFilePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilePrint.Click
        pdPrint.Print()
    End Sub

    ' Enable or disable the Edit menu commands.
    Private Sub mnuEdit_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEdit.Popup
        mnuEditDelete.Enabled = m_PictureN.SelectedDrawable IsNot Nothing
    End Sub

    ' Enable or disable the Format menu commands.
    Private Sub mnuFormat_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFormat.Popup
        mnuFormatOrder.Enabled = m_PictureN.SelectedDrawable IsNot Nothing
    End Sub

    ' Save the picture as an image.
    Private Sub mnuFileSaveImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileSaveImage.Click
        If sfdImage.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim bm As Bitmap = Nothing
            Try
                Dim rect As RectangleF = m_PictureN.GetBounds()
                bm = New Bitmap(CInt(rect.Width), CInt(rect.Height))
                Using gr As Graphics = Graphics.FromImage(bm)
                    m_PictureN.Draw(gr)
                End Using

                Dim filename As String = sfdImage.FileName
                Dim ext As String = filename.Substring(filename.LastIndexOf("."))
                ext = ext.ToLower()
                Select Case (ext)
                    Case ".bmp"
                        bm.Save(filename, ImageFormat.Bmp)
                    Case ".gif"
                        bm.Save(filename, ImageFormat.Gif)
                    Case ".jpg", ".jpeg"
                        bm.Save(filename, ImageFormat.Jpeg)
                    Case ".png"
                        bm.Save(filename, ImageFormat.Png)
                    Case ".tif", ".tiff"
                        bm.Save(filename, ImageFormat.Tiff)
                    Case Else
                        MessageBox.Show("Unknown file extension '" & ext & "'",
                                "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Select
            Finally
                If bm IsNot Nothing Then bm.Dispose()
            End Try
        End If
    End Sub

    Private Sub NewFrmDraw_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ReplaceBackgroundImage(Nothing)
    End Sub

#End Region

#Region "Undo"

    ' In main form:
    Private m_UndoStack As New Stack(Of DrawablePictureN)
    Private m_RedoStack As New Stack(Of DrawablePictureN)

    Private Sub SaveUndoState()
        m_RedoStack.Clear()

        ' Clone current state
        Using ms As New MemoryStream()
            Dim formatter As New BinaryFormatter()
            formatter.Serialize(ms, m_PictureN)
            ms.Position = 0
            m_UndoStack.Push(DirectCast(formatter.Deserialize(ms), DrawablePictureN))
        End Using
    End Sub

    Private Sub Undo()
        If m_UndoStack.Count > 0 Then
            m_RedoStack.Push(m_PictureN)
            m_PictureN = m_UndoStack.Pop()
            picCanvas.Invalidate()
        End If
    End Sub

    Private Sub mnuResetTransform_Click(sender As Object, e As EventArgs) Handles mnuResetTransform.Click
        If m_PictureN.SelectedDrawable IsNot Nothing Then
            m_PictureN.SelectedDrawable.RotationAngle = 0
            m_PictureN.SelectedDrawable.ScaleX = 1
            m_PictureN.SelectedDrawable.ScaleY = 1
            picCanvas.Invalidate()
        End If
    End Sub

    Private Sub mnuFlipHorizontal_Click(sender As Object, e As EventArgs) Handles mnuFlipHorizontal.Click
        If m_PictureN.SelectedDrawable IsNot Nothing Then
            m_PictureN.SelectedDrawable.ScaleX *= -1
            picCanvas.Invalidate()
        End If
    End Sub

    Private Sub mnuFlipVertical_Click(sender As Object, e As EventArgs) Handles mnuFlipVertical.Click
        If m_PictureN.SelectedDrawable IsNot Nothing Then
            m_PictureN.SelectedDrawable.ScaleY *= -1
            picCanvas.Invalidate()
        End If
    End Sub

    Private Function TryLoadImageFromPath(path As String, ByRef image As Image, ByRef errorMessage As String) As Boolean
        image = Nothing
        errorMessage = ""

        If String.IsNullOrWhiteSpace(path) Then
            errorMessage = "No file selected."
            Return False
        End If

        Dim targetPath = path
        If Directory.Exists(path) Then
            targetPath = FindFirstDicomFile(path)
            If String.IsNullOrWhiteSpace(targetPath) Then
                errorMessage = "No DICOM files found in folder."
                Return False
            End If
        End If

        Dim ext = System.IO.Path.GetExtension(targetPath)
        If IsDicomFile(targetPath) OrElse String.IsNullOrWhiteSpace(ext) Then
            If TryLoadDicomImage(targetPath, image, errorMessage) Then
                Return True
            End If
            Return False
        End If

        Return TryLoadStandardImage(targetPath, image, errorMessage)
    End Function

    Private Function TryLoadStandardImage(path As String, ByRef image As Image, ByRef errorMessage As String) As Boolean
        Try
            Dim bytes = File.ReadAllBytes(path)
            Using ms As New MemoryStream(bytes)
                Using raw = Image.FromStream(ms)
                    image = New Bitmap(raw)
                End Using
            End Using
            Return True
        Catch ex As Exception
            errorMessage = ex.Message
            Return False
        End Try
    End Function

    ''' <summary>DICOM loading is handled by the external DicomImageViewer project. This stub returns False with a message.</summary>
    Private Function TryLoadDicomImage(path As String, ByRef image As Image, ByRef errorMessage As String) As Boolean
        image = Nothing
        errorMessage = "DICOM files are opened in the external DICOM Viewer. Use the Images section and click the DICOM Viewer button."
        Return False
    End Function

    Private Function IsDicomFile(path As String) As Boolean
        If String.Equals(System.IO.Path.GetExtension(path), ".dcm", StringComparison.OrdinalIgnoreCase) Then
            Return True
        End If

        Try
            Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                If fs.Length < 132 Then Return False
                fs.Seek(128, SeekOrigin.Begin)
                Dim buffer(3) As Byte
                fs.Read(buffer, 0, 4)
                Dim marker = Encoding.ASCII.GetString(buffer)
                Return String.Equals(marker, "DICM", StringComparison.Ordinal)
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Function FindFirstDicomFile(folderPath As String) As String
        Try
            Dim files = Directory.GetFiles(folderPath)
            Array.Sort(files, StringComparer.OrdinalIgnoreCase)
            For Each file In files
                If IsDicomFile(file) Then Return file
            Next
        Catch
        End Try
        Return ""
    End Function

    Private Sub LoadBackgroundFromPath(path As String)
        Dim img As Image = Nothing
        Dim loadError As String = ""
        If Not TryLoadImageFromPath(path, img, loadError) Then
            Throw New Exception(loadError)
        End If
        ReplaceBackgroundImage(img)
        m_PictureN.BackgroundImageLayout = ImageLayout.Zoom
        picCanvas.Invalidate()
    End Sub

    Private Sub LoadDicomBackgroundFromPath(path As String)
        Dim img As Image = Nothing
        Dim loadError As String = ""
        If Not TryLoadDicomImage(path, img, loadError) Then
            MessageBox.Show(If(String.IsNullOrWhiteSpace(loadError),
                               "Open DICOM files using the DICOM Viewer from the Images section.",
                               loadError),
                            "DICOM", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        ReplaceBackgroundImage(img)
        m_PictureN.BackgroundImageLayout = ImageLayout.Zoom
        picCanvas.Invalidate()
    End Sub




#End Region

End Class