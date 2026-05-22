Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Linq
Imports System.Runtime.Serialization.Formatters.Binary

Module ModuleImages

    ''' <summary>Serialize image for .pic XML: JPEG when opaque (much smaller than PNG in XML), PNG only when transparency is detected.</summary>
    Public Function ImageBytesForPersistence(image As Image) As Byte()
        If image Is Nothing Then Return Nothing
        Using ms As New MemoryStream()
            If ShouldUsePngForPersistence(image) Then
                image.Save(ms, ImageFormat.Png)
            Else
                Dim codec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(Function(c) c.MimeType = "image/jpeg")
                If codec IsNot Nothing Then
                    Using encParams As New EncoderParameters(1)
                        encParams.Param(0) = New EncoderParameter(Encoder.Quality, 88L)
                        image.Save(ms, codec, encParams)
                    End Using
                Else
                    image.Save(ms, ImageFormat.Jpeg)
                End If
            End If
            Return ms.ToArray()
        End Using
    End Function

    Private Function ShouldUsePngForPersistence(image As Image) As Boolean
        Dim bmp = TryCast(image, Bitmap)
        If bmp Is Nothing Then Return False
        If Not HasAlphaChannelPixelFormat(bmp.PixelFormat) Then Return False
        Try
            Dim w = bmp.Width
            Dim h = bmp.Height
            If w <= 0 OrElse h <= 0 Then Return True
            Dim pts() As Point = {
                New Point(0, 0),
                New Point(w - 1, 0),
                New Point(0, h - 1),
                New Point(w - 1, h - 1),
                New Point(w \ 2, h \ 2)
            }
            For Each p In pts
                If bmp.GetPixel(p.X, p.Y).A < 255 Then Return True
            Next
        Catch
            Return True
        End Try
        Return False
    End Function

    Private Function HasAlphaChannelPixelFormat(pf As PixelFormat) As Boolean
        Select Case pf
            Case PixelFormat.Format32bppArgb, PixelFormat.Format32bppPArgb,
                 PixelFormat.Format64bppArgb, PixelFormat.Format64bppPArgb
                Return True
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>Max dimension (width or height) when loading large images to avoid OOM. Used for thumbnails and preview.</summary>
    Public Const LoadImageMaxDimension As Integer = 256
    ' Max file size (bytes) to attempt load; larger files return Nothing to avoid OOM. ~25 MB.
    'Public Const LoadImageMaxFileSizeBytes As Long = 25 * 1024 * 1024

    ''' <summary>Load image from file and scale down if larger than maxDimension. Never throws. Returns Nothing on OOM, invalid image, file in use, or error. Uses FileShare.ReadWrite so file is not locked. Caller must dispose.</summary>
    Public Function LoadImageAtMaxSize(filePath As String, maxDimension As Integer) As Bitmap
        If String.IsNullOrWhiteSpace(filePath) Then Return Nothing
        Dim fi As FileInfo = Nothing
        'Try
        '    fi = New FileInfo(filePath)
        '    If Not fi.Exists OrElse fi.Length <= 0 OrElse fi.Length > LoadImageMaxFileSizeBytes Then Return Nothing
        'Catch
        '    Return Nothing
        'End Try

        Dim fullImage As Image = Nothing
        Try
            ' FileShare.ReadWrite allows other processes to read/write while we read (avoids "file is being used")
            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                fullImage = Image.FromStream(fs, useEmbeddedColorManagement:=False, validateImageData:=True)
            End Using
        Catch ex As OutOfMemoryException
            Return Nothing
        Catch ex As ArgumentException
            ' "Parameter is not valid" from corrupt or unsupported image
            Return Nothing
        Catch ex As IOException
            ' "The process cannot access the file because it is being used by another process"
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try

        If fullImage Is Nothing Then Return Nothing
        Try
            Dim w As Integer = fullImage.Width
            Dim h As Integer = fullImage.Height
            If w <= 0 OrElse h <= 0 Then
                fullImage.Dispose()
                Return Nothing
            End If
            Dim outW As Integer = w
            Dim outH As Integer = h
            If w > maxDimension OrElse h > maxDimension Then
                If w >= h Then
                    outW = maxDimension
                    outH = Math.Max(1, CInt(Math.Round(h * (maxDimension / CDbl(w)))))
                Else
                    outH = maxDimension
                    outW = Math.Max(1, CInt(Math.Round(w * (maxDimension / CDbl(h)))))
                End If
            End If
            Dim result As New Bitmap(outW, outH)
            Using g As Graphics = Graphics.FromImage(result)
                g.InterpolationMode = InterpolationMode.HighQualityBicubic
                g.DrawImage(fullImage, 0, 0, outW, outH)
            End Using
            fullImage.Dispose()
            fullImage = Nothing
            ' Load image as-is: no EXIF/metadata-based rotation
            Return result
        Catch ex As OutOfMemoryException
            If fullImage IsNot Nothing Then fullImage.Dispose()
            Return Nothing
        Catch ex As ArgumentException
            If fullImage IsNot Nothing Then fullImage.Dispose()
            Return Nothing
        Catch ex As Exception
            If fullImage IsNot Nothing Then fullImage.Dispose()
            Return Nothing
        End Try
    End Function

    Public Sub FixImageOrientation(ByVal img As Image)
        Try
            If img.PropertyIdList.Contains(274) Then
                Dim orientation As Integer = img.GetPropertyItem(274).Value(0)
                Select Case orientation
                    Case 2
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX)
                    Case 3
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone)
                    Case 4
                        img.RotateFlip(RotateFlipType.Rotate180FlipX)
                    Case 5
                        img.RotateFlip(RotateFlipType.Rotate90FlipX)
                    Case 6
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone)
                    Case 7
                        img.RotateFlip(RotateFlipType.Rotate270FlipX)
                    Case 8
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                End Select
                ' Remove the orientation flag so it's not reapplied later
                img.RemovePropertyItem(274)
            End If
        Catch ex As Exception
            ' Optional: log or ignore
        End Try
    End Sub

    Public Function AutoRotateImage(img As Image) As Image
        Try
            If img.PropertyIdList.Contains(274) Then ' 274 = EXIF Orientation tag
                Dim orientation As Integer = img.GetPropertyItem(274).Value(0)
                Dim rotateFlipType As RotateFlipType = RotateFlipType.RotateNoneFlipNone

                Select Case orientation
                    Case 2 : rotateFlipType = RotateFlipType.RotateNoneFlipX
                    Case 3 : rotateFlipType = RotateFlipType.Rotate180FlipNone
                    Case 4 : rotateFlipType = RotateFlipType.Rotate180FlipX
                    Case 5 : rotateFlipType = RotateFlipType.Rotate90FlipX
                    Case 6 : rotateFlipType = RotateFlipType.Rotate90FlipNone
                    Case 7 : rotateFlipType = RotateFlipType.Rotate270FlipX
                    Case 8 : rotateFlipType = RotateFlipType.Rotate270FlipNone
                End Select

                If rotateFlipType <> RotateFlipType.RotateNoneFlipNone Then
                    img.RotateFlip(rotateFlipType)
                    ' Optional: remove EXIF orientation so it doesn't get applied again
                    img.RemovePropertyItem(274)
                End If
            End If
        Catch
            ' Ignore images without EXIF or any errors
        End Try

        Return img
    End Function


    Public Sub Byte2Image(ByRef NewImage As Image, ByVal ByteArr() As Byte)
        '
        Dim ImageStream As MemoryStream

        Try
            If ByteArr.GetUpperBound(0) > 0 Then
                ImageStream = New MemoryStream(ByteArr)
                NewImage = Image.FromStream(ImageStream)
            Else
                NewImage = Nothing
            End If
        Catch ex As Exception
            NewImage = Nothing
        End Try

    End Sub


    Public Sub Image2Byte(ByRef NewImage As Image, ByRef ByteArr() As Byte)
        '
        Dim ImageStream As MemoryStream

        Try
            ReDim ByteArr(0)
            If NewImage IsNot Nothing Then
                ImageStream = New MemoryStream
                NewImage.Save(ImageStream, ImageFormat.Jpeg)
                ReDim ByteArr(CInt(ImageStream.Length - 1))
                ImageStream.Position = 0
                ImageStream.Read(ByteArr, 0, CInt(ImageStream.Length))
            End If
        Catch ex As Exception

        End Try

    End Sub


    'Public Function byteArrayToImage(byteArrayIn As Byte()) As Image
    '    Dim ms As New MemoryStream(byteArrayIn)
    '    Dim returnImage As Image = Image.FromStream(ms)
    '    Return returnImage
    'End Function


#Region "Images"
    Public Function ImageToBase64(ByVal image As Image, ByVal format As System.Drawing.Imaging.ImageFormat) As String
        Using ms As New MemoryStream()
            ' Convert Image to byte[]
            image.Save(ms, format)
            Dim imageBytes As Byte() = ms.ToArray() ' Convert byte[] to Base64 String
            Dim base64String As String = Convert.ToBase64String(imageBytes)
            Return base64String
        End Using
    End Function

    Public Function Base64ToImage(ByVal base64String As String) As Image
        ' Convert Base64 String to byte[]
        Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
        Dim ms As New MemoryStream(imageBytes, 0, imageBytes.Length)

        ' Convert byte[] to Image
        ms.Write(imageBytes, 0, imageBytes.Length)
        Dim ConvertedBase64Image As Image = Image.FromStream(ms, True)
        Return ConvertedBase64Image
    End Function

    Public Function imgToByteArray(ByVal img As Image) As Byte()

        Using mStream As New MemoryStream()
            img.Save(mStream, Imaging.ImageFormat.Jpeg) ' img.RawFormat)
            Return mStream.ToArray()
        End Using
    End Function
    'convert bytearray to image
    Public Function byteArrayToImage(ByVal byteArrayIn As Byte()) As Image
        Using mStream As New MemoryStream(byteArrayIn)
            Return Image.FromStream(mStream)
        End Using
    End Function
    'another easy way to convert image to bytearray
    Public Function imgToByteConverter(ByVal inImg As Image) As Byte()
        Dim imgCon As New ImageConverter()
        Return DirectCast(imgCon.ConvertTo(inImg, GetType(Byte())), Byte())
    End Function

    Public Class MyImageConverter
        Public Sub New()
        End Sub

        Public Function imageToByteArray(ByVal imageIn As System.Drawing.Image) As Byte()
            Dim ms As New MemoryStream()
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Return ms.ToArray()
        End Function

        Public Function byteArrayToImage(ByVal byteArrayIn() As Byte) As Image
            Dim ms As New MemoryStream(byteArrayIn)
            Dim returnImage As Image = Image.FromStream(ms)
            Return returnImage

        End Function


    End Class


    Public Function ObjToImg(ByVal obj As Object) As System.Drawing.Image
        Dim byteArray() As Byte
        If obj Is Nothing Then
            Return Nothing
        Else
            Dim bf As New BinaryFormatter()
            Dim ms As New MemoryStream()
            bf.Serialize(ms, obj)
            byteArray = ms.ToArray() ' Byte Array
            ms.Close()

            ms = New MemoryStream(byteArray, 0, byteArray.Length)
            ms.Seek(0, SeekOrigin.Begin)
            Dim returnImage As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
            Return returnImage
        End If
    End Function
#End Region

End Module
