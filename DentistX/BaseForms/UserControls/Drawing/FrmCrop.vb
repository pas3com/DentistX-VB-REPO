
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Public Class FrmCrop

    Public Sub New(ByVal img As Image)
        InitializeComponent()
        OriginalImageFullRes = New Bitmap(img)
        OriginPic.Image = img
        CroppedPic.Image = img
    End Sub

    ''' <param name="sourceImagePath">Full path to the patient image file (P-{{id}}-IMG_* under Images\Patient_{{id}}\...).</param>
    Public Sub New(ByVal img As Image, sourceImagePath As String)
        InitializeComponent()
        _SourceImagePath = sourceImagePath
        If Not String.IsNullOrWhiteSpace(sourceImagePath) AndAlso File.Exists(sourceImagePath) Then
            OriginalImageFullRes = LoadBitmapFromFileFullRes(sourceImagePath)
            OriginPic.Image = OriginalImageFullRes
            CroppedPic.Image = OriginalImageFullRes
        Else
            OriginalImageFullRes = New Bitmap(img)
            OriginPic.Image = img
            CroppedPic.Image = img
        End If
    End Sub

    ''' <summary>Loads the file into a Bitmap at native pixel dimensions (no display-size downscaling).</summary>
    Private Shared Function LoadBitmapFromFileFullRes(path As String) As Bitmap
        Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Using src As Image = Image.FromStream(fs, useEmbeddedColorManagement:=False, validateImageData:=True)
                Return New Bitmap(src)
            End Using
        End Using
    End Function

    Private OriginalImageFullRes As Bitmap

    ''' <summary>Image opened via Open button — owned by this form, disposed on close.</summary>
    Private _openedWorkspaceImage As Image = Nothing

    Dim tempCnt As Boolean

    ''' <summary>Full path of the image this crop session is based on (for auto-save naming).</summary>
    Private _SourceImagePath As String = ""

    Property CroppedImage As Image

    ''' <summary>Full path written when Apply & Exit auto-saves a patient folder image.</summary>
    Property SavedFilePath As String

#Region "Image Cropping"
    Dim cropX As Integer
    Dim cropY As Integer
    Dim cropWidth As Integer
    Dim cropHeight As Integer

    Dim cropBitmap As Bitmap

    Private _cropDragging As Boolean

    Public cropPenSize As Integer = 2
    Public cropPenColor As Color = Color.Red

    Private Shared Function NormalizeRect(r As Rectangle) As Rectangle
        Dim x = Math.Min(r.Left, r.Right)
        Dim y = Math.Min(r.Top, r.Bottom)
        Dim w = Math.Abs(r.Width)
        Dim h = Math.Abs(r.Height)
        Return New Rectangle(x, y, w, h)
    End Function

    Private Sub crobPictureBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles OriginPic.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Left Then
                _cropDragging = True
                cropX = e.X
                cropY = e.Y
                cropWidth = 0
                cropHeight = 0
                Cursor = Cursors.Cross
                OriginPic.Invalidate()
            End If
        Catch
        End Try
    End Sub

    Private Sub crobPictureBox_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles OriginPic.MouseMove
        Try
            If OriginPic.Image Is Nothing Then Exit Sub
            If e.Button = Windows.Forms.MouseButtons.Left AndAlso _cropDragging Then
                cropWidth = e.X - cropX
                cropHeight = e.Y - cropY
                OriginPic.Invalidate()
            End If
        Catch
        End Try
    End Sub

    Private Sub OriginPic_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles OriginPic.Paint
        If Not _cropDragging OrElse OriginPic.Image Is Nothing Then Return
        Dim norm = NormalizeRect(New Rectangle(cropX, cropY, cropWidth, cropHeight))
        If norm.Width < 1 OrElse norm.Height < 1 Then Return
        Using p As New Pen(cropPenColor, cropPenSize)
            p.DashStyle = DashStyle.DashDotDot
            e.Graphics.DrawRectangle(p, norm)
        End Using
    End Sub


    ''' <summary>Maps control coordinates to source image pixels. OriginPic uses SizeMode StretchImage (fills client).</summary>
    Private Function MapStretchSelectionToSourceRect(normSel As Rectangle) As Rectangle
        Dim cw = Math.Max(1, OriginPic.ClientSize.Width)
        Dim ch = Math.Max(1, OriginPic.ClientSize.Height)
        Dim iw = CSng(OriginalImageFullRes.Width)
        Dim ih = CSng(OriginalImageFullRes.Height)
        Dim scaleX = iw / CSng(cw)
        Dim scaleY = ih / CSng(ch)

        Dim rectF As New RectangleF(
            normSel.X * scaleX,
            normSel.Y * scaleY,
            normSel.Width * scaleX,
            normSel.Height * scaleY)
        rectF.Intersect(New RectangleF(0, 0, iw, ih))

        Dim rect = Rectangle.Round(rectF)
        If rect.Width < 1 Then rect.Width = 1
        If rect.Height < 1 Then rect.Height = 1
        rect.Intersect(New Rectangle(0, 0, OriginalImageFullRes.Width, OriginalImageFullRes.Height))
        Return rect
    End Function

    ''' <summary>Copies pixels 1:1 from source when possible (Clone); otherwise nearest-neighbor blit (no blur).</summary>
    Private Function ExtractCropFromSource(src As Bitmap, rect As Rectangle) As Bitmap
        rect.Intersect(New Rectangle(0, 0, src.Width, src.Height))
        If rect.Width <= 0 OrElse rect.Height <= 0 Then Return Nothing

        Try
            Return DirectCast(src.Clone(rect, src.PixelFormat), Bitmap)
        Catch
        End Try

        Dim dest As New Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(dest)
            g.InterpolationMode = InterpolationMode.NearestNeighbor
            g.PixelOffsetMode = PixelOffsetMode.Half
            g.CompositingMode = CompositingMode.SourceCopy
            g.DrawImage(src, New Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel)
        End Using
        Return dest
    End Function

    Private Sub crobPictureBox_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles OriginPic.MouseUp
        Try
            _cropDragging = False
            OriginPic.Invalidate()

            Dim normSel = NormalizeRect(New Rectangle(cropX, cropY, cropWidth, cropHeight))
            normSel.Intersect(New Rectangle(0, 0, OriginPic.ClientSize.Width, OriginPic.ClientSize.Height))
            If normSel.Width < 1 OrElse normSel.Height < 1 Then Exit Sub

            cropX = normSel.X
            cropY = normSel.Y
            cropWidth = normSel.Width
            cropHeight = normSel.Height

            Dim rect = MapStretchSelectionToSourceRect(normSel)
            If rect.Width <= 0 OrElse rect.Height <= 0 Then Exit Sub

            Dim _img = ExtractCropFromSource(OriginalImageFullRes, rect)
            If _img Is Nothing Then Exit Sub

            ReplaceCroppedBitmap(_img)
        Catch ex As Exception
            MsgBox("Cropping failed: " & ex.Message)
        End Try

        Cursor = Cursors.Default
    End Sub

    Private Sub ReplaceCroppedBitmap(newBmp As Bitmap)
        If cropBitmap IsNot Nothing AndAlso Not Object.ReferenceEquals(cropBitmap, OriginPic.Image) Then
            cropBitmap.Dispose()
        End If
        cropBitmap = newBmp
        CroppedPic.Image = newBmp
        CroppedImage = newBmp
    End Sub

#End Region

    Private Sub FrmCrop_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Private Sub ReleaseOpenedWorkspaceImage()
        If _openedWorkspaceImage Is Nothing Then Return
        If Object.ReferenceEquals(OriginPic.Image, _openedWorkspaceImage) Then OriginPic.Image = Nothing
        If Object.ReferenceEquals(CroppedPic.Image, _openedWorkspaceImage) Then CroppedPic.Image = Nothing
        If Object.ReferenceEquals(OriginalImageFullRes, _openedWorkspaceImage) Then
            OriginalImageFullRes = Nothing
        End If
        _openedWorkspaceImage.Dispose()
        _openedWorkspaceImage = Nothing
    End Sub

    Private Sub cropCancelBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cropCancelBtn1.Click
        If cropBitmap IsNot Nothing AndAlso Not Object.ReferenceEquals(cropBitmap, OriginPic.Image) Then
            cropBitmap.Dispose()
        End If
        cropBitmap = Nothing
        CroppedPic.Image = Nothing
        CroppedImage = Nothing
        ReleaseOpenedWorkspaceImage()
        OriginPic.Image = Nothing
    End Sub

    Private Sub cropSaveBtn_Click(sender As Object, e As EventArgs) Handles cropSaveBtn.Click
        If cropBitmap Is Nothing Then
            MsgBox("No cropped image available.")
            Return
        End If

        If String.IsNullOrWhiteSpace(_SourceImagePath) Then
            MsgBox("No source file path is set; use Apply && Exit or open an image from disk first.")
            Return
        End If

        Dim fname As String = "Cropped_" & Path.GetFileNameWithoutExtension(_SourceImagePath) & ".jpg"
        Using svdlg As New SaveFileDialog()
            svdlg.Filter = "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*"
            svdlg.FilterIndex = 1
            svdlg.RestoreDirectory = True
            svdlg.InitialDirectory = Path.GetDirectoryName(_SourceImagePath)
            svdlg.FileName = fname

            If svdlg.ShowDialog() = DialogResult.OK Then
                If SavePhoto(cropBitmap, svdlg.FileName) Then
                    MsgBox("Image saved successfully.")
                End If
            End If
        End Using
    End Sub

    Public Function SavePhoto(ByVal src As Image, ByVal dest As String) As Boolean
        Try
            Dim jpegEncoder As ImageCodecInfo = ImageCodecInfo.GetImageEncoders().
                FirstOrDefault(Function(c) c.FormatDescription = "JPEG")

            If jpegEncoder Is Nothing Then
                Throw New Exception("JPEG encoder not found.")
            End If

            Using encoderParams As New EncoderParameters(1)
                encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, 100L)
                src.Save(dest, jpegEncoder, encoderParams)
            End Using
            Return True
        Catch ex As Exception
            MsgBox("Error saving image: " & ex.Message)
            Return False
        End Try
    End Function

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Using openDlg As New OpenFileDialog()
            openDlg.Filter = "Image Files|*.jpg;*.jpeg;*.bmp;*.gif;*.png|All Files|*.*"
            openDlg.Title = "Select an image"

            If openDlg.ShowDialog() = DialogResult.OK Then
                Try
                    ReleaseOpenedWorkspaceImage()
                    If OriginalImageFullRes IsNot Nothing Then
                        OriginalImageFullRes.Dispose()
                        OriginalImageFullRes = Nothing
                    End If
                    If cropBitmap IsNot Nothing AndAlso Not Object.ReferenceEquals(cropBitmap, OriginPic.Image) Then
                        cropBitmap.Dispose()
                    End If
                    cropBitmap = Nothing
                    CroppedPic.Image = Nothing
                    CroppedImage = Nothing

                    Dim fromDisk = LoadBitmapFromFileFullRes(openDlg.FileName)
                    OriginalImageFullRes = fromDisk
                    OriginPic.Image = fromDisk
                    CroppedPic.Image = fromDisk
                    _openedWorkspaceImage = fromDisk
                    _SourceImagePath = openDlg.FileName
                Catch ex As Exception
                    MsgBox("Error loading image: " & ex.Message)
                End Try
            End If
        End Using
    End Sub

    Private Function TryParsePatientIdFromOpenedPath(fullPath As String, ByRef patientId As Integer) As Boolean
        patientId = 0
        If String.IsNullOrWhiteSpace(fullPath) Then Return False
        Dim fn = Path.GetFileNameWithoutExtension(fullPath)
        If fn.Length > 2 AndAlso fn.StartsWith("P-", StringComparison.OrdinalIgnoreCase) Then
            Dim rest = fn.Substring(2)
            Dim dash = rest.IndexOf("-"c)
            If dash > 0 Then
                Return Integer.TryParse(rest.Substring(0, dash), patientId)
            End If
        End If
        Dim d = Path.GetDirectoryName(fullPath)
        If String.IsNullOrEmpty(d) Then Return False
        Dim patientDir = Directory.GetParent(d)
        If patientDir Is Nothing Then Return False
        Dim pn = patientDir.Name
        If pn.StartsWith("Patient_", StringComparison.OrdinalIgnoreCase) Then
            Return Integer.TryParse(pn.Substring("Patient_".Length), patientId)
        End If
        Return False
    End Function

    Private Shared Function ExtractPatientImageSuffixIndex(filePath As String) As Integer
        Dim name = Path.GetFileNameWithoutExtension(filePath)
        Dim parts = name.Split("_"c)
        If parts.Length >= 2 Then
            Dim number As Integer
            If Integer.TryParse(parts(parts.Length - 1), number) Then Return number
        End If
        Return -1
    End Function

    Private Function GetNextPatientImageIndex(imageFolder As String, patientId As Integer) As Integer
        Dim pattern = "P-" & patientId.ToString() & "-IMG_*"
        If Not Directory.Exists(imageFolder) Then Return 0
        Dim existingFiles = Directory.GetFiles(imageFolder, pattern)
        Dim maxIdx = -1
        For Each fp In existingFiles
            Dim i = ExtractPatientImageSuffixIndex(fp)
            If i > maxIdx Then maxIdx = i
        Next
        If maxIdx < 0 Then Return 0
        Return maxIdx + 1
    End Function

    Private Function TrySaveCroppedAsNextPatientImage(bmp As Bitmap, sourceImagePath As String, ByRef writtenPath As String) As Boolean
        writtenPath = Nothing
        Dim destDir = Path.GetDirectoryName(sourceImagePath)
        If String.IsNullOrEmpty(destDir) Then
            MessageBox.Show("Cannot determine save folder.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Dim patientId As Integer
        If Not TryParsePatientIdFromOpenedPath(sourceImagePath, patientId) Then
            MessageBox.Show("Cannot determine patient ID from the image path or file name (expected Images\Patient_{id}\... or P-{id}-IMG_*).",
                            "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Dim ext = Path.GetExtension(sourceImagePath)
        If String.IsNullOrEmpty(ext) Then ext = ".jpg"
        ext = ext.ToLowerInvariant()

        Try
            If Not Directory.Exists(destDir) Then
                Directory.CreateDirectory(destDir)
            End If

            Dim nextIndex = GetNextPatientImageIndex(destDir, patientId)
            Dim filename = Path.Combine(destDir, "P-" & patientId.ToString() & "-IMG_" & nextIndex.ToString() & ext)

            Select Case ext
                Case ".bmp"
                    bmp.Save(filename, ImageFormat.Bmp)
                Case ".gif"
                    bmp.Save(filename, ImageFormat.Gif)
                Case ".jpg", ".jpeg"
                    bmp.Save(filename, ImageFormat.Jpeg)
                Case ".png"
                    bmp.Save(filename, ImageFormat.Png)
                Case ".tif", ".tiff"
                    bmp.Save(filename, ImageFormat.Tiff)
                Case Else
                    MessageBox.Show("Unknown file extension '" & ext & "'", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
            End Select

            writtenPath = filename
            Return True
        Catch ex As Exception
            MessageBox.Show("Failed to save image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub btnApplyExit_Click(sender As Object, e As EventArgs) Handles btnApplyExit.Click
        If cropBitmap Is Nothing Then Return

        SavedFilePath = Nothing

        If Not String.IsNullOrWhiteSpace(_SourceImagePath) Then
            Dim pathOut As String = Nothing
            If Not TrySaveCroppedAsNextPatientImage(cropBitmap, _SourceImagePath, pathOut) Then
                Return
            End If
            SavedFilePath = pathOut
        End If

        CroppedImage = cropBitmap
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub FrmCrop_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ReleaseOpenedWorkspaceImage()
        If OriginalImageFullRes IsNot Nothing Then
            OriginalImageFullRes.Dispose()
            OriginalImageFullRes = Nothing
        End If
        If DialogResult <> DialogResult.OK Then
            If cropBitmap IsNot Nothing AndAlso Not Object.ReferenceEquals(cropBitmap, OriginPic.Image) Then
                cropBitmap.Dispose()
            End If
        End If
        cropBitmap = Nothing
    End Sub

#Region "Image Resizing"
    Private Sub resizingTrackBar_ValueChanged(sender As Object, e As EventArgs) Handles resizingTrackBar.ValueChanged
        Try
            If cropBitmap Is Nothing Then Exit Sub

            Dim scalePercent As Integer = resizingTrackBar.Value
            If scalePercent <= 0 Then Exit Sub
            Dim scaleFactor As Double = scalePercent / 100.0

            Dim newWidth As Integer = Math.Max(1, CInt(cropBitmap.Width * scaleFactor))
            Dim newHeight As Integer = Math.Max(1, CInt(cropBitmap.Height * scaleFactor))

            Dim bmDest As New Bitmap(newWidth, newHeight)
            Using grDest As Graphics = Graphics.FromImage(bmDest)
                grDest.SmoothingMode = SmoothingMode.HighQuality
                grDest.InterpolationMode = InterpolationMode.HighQualityBicubic
                grDest.PixelOffsetMode = PixelOffsetMode.HighQuality
                grDest.DrawImage(cropBitmap, 0, 0, newWidth, newHeight)
            End Using

            If cropBitmap IsNot Nothing AndAlso Not Object.ReferenceEquals(cropBitmap, OriginPic.Image) Then
                cropBitmap.Dispose()
            End If
            cropBitmap = bmDest
            CroppedPic.Image = bmDest
            CroppedImage = bmDest
            tempCnt = True
        Catch ex As Exception
            MsgBox("Zoom failed: " & ex.Message)
        End Try
    End Sub

#End Region

End Class
