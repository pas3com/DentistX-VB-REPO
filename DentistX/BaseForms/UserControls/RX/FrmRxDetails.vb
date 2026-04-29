Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports DevExpress.Drawing
Imports DevExpress.Drawing.Printing
Imports System.Text.RegularExpressions

Public Class FrmRxDetails

    Private fileName As String
    Private LogoByte() As Byte
    Private wtrByte() As Byte
    Private clsRxBody As IEnumerable(Of RxBody)
    Private clsRxBodyData As New RxBodyDATA

    Dim labelFont1 As New Font("Blackadder ITC", 12, FontStyle.Bold Or FontStyle.Italic)
    Dim labelFont2 As New Font("Simple Outline Pat", 12.75, FontStyle.Bold Or FontStyle.Italic)
    Dim maxCharsArHdr As Integer = EstimateMaxCharCount(labelWidthPx:=552, labelHeightPx:=188, font:=labelFont1, paddingLeft:=5, paddingRight:=5,
                                                           wordWrap:=True, isMultiline:=True) 'Blackadder ITC, 12pt, style=Bold, Italic



    Private Sub FrmRxDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If RxBodyBindingSource Is Nothing Then
                RxBodyBindingSource = New BindingSource()
            End If
            clsRxBody = clsRxBodyData.SelectAll()
            RxBodyBindingSource.DataSource = clsRxBody.ToList()
            BindData()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#Region "Typing"
    Public Sub HandleMemoCharCount(sender As Object, e As EventArgs)
        Dim memo As DevExpress.XtraEditors.MemoEdit = TryCast(sender, DevExpress.XtraEditors.MemoEdit)
        If memo Is Nothing OrElse memo.Parent Is Nothing Then Exit Sub

        ' Derive associated Label name: remove "TextBox", add "lbl" prefix
        Dim lblName As String = "lbl" & memo.Name.Replace("TextBox", "")

        ' Find the corresponding label control in the same container (e.g., the Form or a Panel)
        Dim lblControl As DevExpress.XtraEditors.LabelControl = TryCast(memo.Parent.Controls.Find(lblName, True).FirstOrDefault(), DevExpress.XtraEditors.LabelControl)
        If lblControl Is Nothing Then Exit Sub

        ' Estimate max chars using font and size
        'Dim memFont As Font = memo.Font
        Dim maxChars As Integer = EstimateMaxCharCount(memo.Size.Width, memo.Size.Height, memo.Font, memo.Padding.Left, memo.Padding.Right, memo.Properties.WordWrap, True)

        ' Compute remaining characters
        Dim remainingChars As Integer = maxChars - memo.Text.Length
        If remainingChars < 0 Then remainingChars = 0

        lblControl.Text = $"Remaining: {remainingChars}"
    End Sub

    Private Sub ArHdrAdresTextBox_EditValueChanged_1(sender As Object, e As EventArgs) Handles ArHdrAdresTextBox.EditValueChanged
        HandleMemoCharCount(sender, e)
    End Sub
    Private Sub ArHdrNameTextBox_EditValueChanged(sender As Object, e As EventArgs) Handles ArHdrNameTextBox.EditValueChanged
        HandleMemoCharCount(sender, e)
    End Sub

    Private Sub EnHdrNameTextBox_EditValueChanged(sender As Object, e As EventArgs) Handles EnHdrNameTextBox.EditValueChanged
        HandleMemoCharCount(sender, e)
    End Sub

    Private Sub EnHdrAdresTextBox_EditValueChanged(sender As Object, e As EventArgs) Handles EnHdrAdresTextBox.EditValueChanged
        HandleMemoCharCount(sender, e)
    End Sub

    Private Sub DetailTextBox_EditValueChanged(sender As Object, e As EventArgs) Handles DetailTextBox.EditValueChanged
        HandleMemoCharCount(sender, e)
    End Sub

    Private Sub DrNameText_EditValueChanged(sender As Object, e As EventArgs) Handles DrNameText.EditValueChanged
        HandleMemoCharCount(sender, e)
    End Sub

    Private Sub EnFtrTextBox_EditValueChanged(sender As Object, e As EventArgs) Handles EnFtrTextBox.EditValueChanged
        HandleMemoCharCount(sender, e)
    End Sub

    Private Sub ArFtrTextBox_EditValueChanged(sender As Object, e As EventArgs) Handles ArFtrTextBox.EditValueChanged
        HandleMemoCharCount(sender, e)
    End Sub

    Private Function EstimateMaxCharCount(labelWidthPx As Integer,
                                      labelHeightPx As Integer,
                                      font As Font,
                                      paddingLeft As Integer,
                                      paddingRight As Integer,
                                      wordWrap As Boolean,
                                      isMultiline As Boolean) As Integer
        Dim usableWidth As Integer = labelWidthPx - paddingLeft - paddingRight

        If usableWidth <= 0 Then Return 0

        Using bmp As New Bitmap(1, 1)
            Using g As Graphics = Graphics.FromImage(bmp)
                Dim testChar As String = If(ContainsArabic("م"), "م", "W")
                Dim charSize As SizeF = g.MeasureString(testChar, font)

                If wordWrap AndAlso isMultiline Then
                    ' Estimate line count
                    Dim lineHeight As Integer = CInt(charSize.Height)
                    Dim maxLines As Integer = CInt(labelHeightPx / lineHeight)
                    Dim charsPerLine As Integer = CInt(usableWidth / charSize.Width)
                    Return maxLines * charsPerLine
                Else
                    ' Single-line case
                    Return CInt(usableWidth / charSize.Width)
                End If
            End Using
        End Using
    End Function

    Private Shared Function ContainsArabic(text As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(text, "[\u0600-\u06FF]")
    End Function

    Private Function ContainsArabic1(text As String) As Boolean
        ' Unicode Arabic range: 0600–06FF
        Return Regex.IsMatch(text, "[\u0600-\u06FF]")
    End Function


    Public Function ConvertTenthsOfMmToPixels(widthTenthsMm As Single, heightTenthsMm As Single, Optional dpi As Single = 96) As Size
        ' Convert tenths of a millimeter to inches
        Dim widthInches As Single = (widthTenthsMm * 0.1F) / 25.4F
        Dim heightInches As Single = (heightTenthsMm * 0.1F) / 25.4F

        ' Convert inches to pixels
        Dim widthPixels As Integer = CInt(widthInches * dpi)
        Dim heightPixels As Integer = CInt(heightInches * dpi)

        Return New Size(widthPixels, heightPixels)
    End Function


#End Region

    Private Sub UnBindData()
        ' Clear previous bindings
        RxBdyIDTextBox.DataBindings.Clear()
        ArFtrTextBox.DataBindings.Clear()
        ArHdrAdresTextBox.DataBindings.Clear()
        ArHdrNameTextBox.DataBindings.Clear()
        DetailTextBox.DataBindings.Clear()
        DrNameText.DataBindings.Clear()
        EnFtrTextBox.DataBindings.Clear()
        EnHdrAdresTextBox.DataBindings.Clear()
        EnHdrNameTextBox.DataBindings.Clear()
        Logo.DataBindings.Clear()
        UseWtrImg.DataBindings.Clear()
        UseWtrText.DataBindings.Clear()
        Wtr.DataBindings.Clear()
        WtrTextTextBox.DataBindings.Clear()
    End Sub


    Private Sub BindData()
        ' Clear previous bindings
        RxBdyIDTextBox.DataBindings.Clear()
        ArFtrTextBox.DataBindings.Clear()
        ArHdrAdresTextBox.DataBindings.Clear()
        ArHdrNameTextBox.DataBindings.Clear()
        DetailTextBox.DataBindings.Clear()
        DrNameText.DataBindings.Clear()
        EnFtrTextBox.DataBindings.Clear()
        EnHdrAdresTextBox.DataBindings.Clear()
        EnHdrNameTextBox.DataBindings.Clear()
        Logo.DataBindings.Clear()
        UseWtrImg.DataBindings.Clear()
        UseWtrText.DataBindings.Clear()
        Wtr.DataBindings.Clear()
        WtrTextTextBox.DataBindings.Clear()

        ' Add new bindings
        RxBdyIDTextBox.DataBindings.Add("Text", RxBodyBindingSource, "RxBdyID")
        ArFtrTextBox.DataBindings.Add("Text", RxBodyBindingSource, "ArFtr")
        ArHdrAdresTextBox.DataBindings.Add("Text", RxBodyBindingSource, "ArHdrAdres")
        ArHdrNameTextBox.DataBindings.Add("Text", RxBodyBindingSource, "ArHdrName")
        DetailTextBox.DataBindings.Add("Text", RxBodyBindingSource, "Detail")
        DrNameText.DataBindings.Add("Text", RxBodyBindingSource, "DrName")
        EnFtrTextBox.DataBindings.Add("Text", RxBodyBindingSource, "EnFtr")
        EnHdrAdresTextBox.DataBindings.Add("Text", RxBodyBindingSource, "EnHdrAdres")
        EnHdrNameTextBox.DataBindings.Add("Text", RxBodyBindingSource, "EnHdrName")
        Logo.DataBindings.Add("Image", RxBodyBindingSource, "Logo", True)
        UseWtrImg.DataBindings.Add("Checked", RxBodyBindingSource, "UseWtrImg")
        UseWtrText.DataBindings.Add("Checked", RxBodyBindingSource, "UseWtrText")
        Wtr.DataBindings.Add("Image", RxBodyBindingSource, "WtrImg", True)
        WtrTextTextBox.DataBindings.Add("Text", RxBodyBindingSource, "WtrText")
    End Sub


#Region "Image Utilities"

    Private Function GenerateThumbnail(image As Image, width As Integer, height As Integer) As Image
        Try
            ' Create a new bitmap with proper pixel format
            Dim thumbnail As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)

            Using g As Graphics = Graphics.FromImage(thumbnail)
                ' Set high quality rendering
                g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality

                ' Use a white background for images without transparency
                g.Clear(Color.White)

                ' Calculate aspect ratio
                Dim sourceWidth = image.Width
                Dim sourceHeight = image.Height
                Dim sourceRatio = sourceWidth / sourceHeight
                Dim destRatio = width / height

                Dim destX As Integer = 0
                Dim destY As Integer = 0
                Dim destWidth As Integer = width
                Dim destHeight As Integer = height

                ' Maintain aspect ratio while fitting within thumbnail
                If sourceRatio > destRatio Then
                    ' Source is wider
                    destHeight = CInt(width / sourceRatio)
                    destY = CInt((height - destHeight) / 2)
                Else
                    ' Source is taller
                    destWidth = CInt(height * sourceRatio)
                    destX = CInt((width - destWidth) / 2)
                End If

                ' Draw the image
                g.DrawImage(image, destX, destY, destWidth, destHeight)
            End Using

            Return thumbnail
        Catch ex As Exception
            ' Return original image if thumbnail creation fails
            Return image
        End Try
    End Function

    Private Function ImageToByteArray(imageIn As Image) As Byte()
        If imageIn Is Nothing Then Return Nothing

        Try
            Using ms As New MemoryStream()
                ' Save as JPEG with quality setting
                If imageIn.RawFormat.Equals(ImageFormat.Png) Or
               imageIn.RawFormat.Equals(ImageFormat.Bmp) Or
               imageIn.RawFormat.Equals(ImageFormat.Gif) Then
                    ' If it's a format with transparency, convert to JPEG properly
                    Dim codecInfo As ImageCodecInfo = GetEncoderInfo("image/jpeg")
                    Dim encoderParams As New Imaging.EncoderParameters(1)
                    encoderParams.Param(0) = New Imaging.EncoderParameter(Imaging.Encoder.Quality, 85L)
                    imageIn.Save(ms, codecInfo, encoderParams)
                Else
                    ' Use original format if it's JPEG
                    imageIn.Save(ms, imageIn.RawFormat)
                End If
                Return ms.ToArray()
            End Using
        Catch ex As Exception
            ' Fallback: save as JPEG
            Using ms As New MemoryStream()
                imageIn.Save(ms, ImageFormat.Jpeg)
                Return ms.ToArray()
            End Using
        End Try
    End Function

    Private Function GetEncoderInfo(mimeType As String) As ImageCodecInfo
        Dim encoders() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        For Each encoder As ImageCodecInfo In encoders
            If encoder.MimeType = mimeType Then
                Return encoder
            End If
        Next
        Return Nothing
    End Function

#End Region

#Region "Browse Image"

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            Dim oldImg As Image = Logo.Image
            OFD.Filter = Filters
            OFD.FilterIndex = 1 ' Default to first filter option
            OFD.Title = "Select Logo Image"
            OFD.Multiselect = False ' Only allow single file selection
            OFD.CheckFileExists = True
            OFD.CheckPathExists = True
            If OFD.ShowDialog = DialogResult.OK Then
                Logo.Image = GenerateThumbnail(Image.FromFile(OFD.FileName), 100, 100)
            Else
                Logo.Image = oldImg
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnBrowsWtr_Click(sender As Object, e As EventArgs) Handles BtnBrowsWtr.Click
        Try
            Dim oldImg As Image = Wtr.Image
            OFD.Filter = Filters
            OFD.FilterIndex = 1 ' Default to first filter option
            OFD.Title = "Select Water Image"
            OFD.Multiselect = False ' Only allow single file selection
            OFD.CheckFileExists = True
            OFD.CheckPathExists = True
            If OFD.ShowDialog = DialogResult.OK Then
                Wtr.Image = GenerateThumbnail(Image.FromFile(OFD.FileName), 350, 350)
            Else
                Wtr.Image = oldImg
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

#Region "Add or Update RxBody"

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Logo.Image IsNot Nothing Then
                LogoByte = ImageToByteArray(Logo.Image)
            Else
                LogoByte = Nothing
            End If

            If Wtr.Image IsNot Nothing Then
                wtrByte = ImageToByteArray(Wtr.Image)
            Else
                wtrByte = Nothing
            End If


            Dim newBody As New RxBody With {
                .ArFtr = ArFtrTextBox.Text,
                .ArHdrAdres = ArHdrAdresTextBox.Text,
                .ArHdrName = ArHdrNameTextBox.Text,
                .Detail = DetailTextBox.Text,
                .DrName = DrNameText.Text,
                .EnFtr = EnFtrTextBox.Text,
                .EnHdrAdres = EnHdrAdresTextBox.Text,
                .EnHdrName = EnHdrNameTextBox.Text,
                .Logo = LogoByte,
                .UseWtrImg = UseWtrImg.Checked,
                .UseWtrText = UseWtrText.Checked,
                .WtrImg = wtrByte,
                .WtrText = WtrTextTextBox.Text
            }

            Dim success As Boolean
            If RxBodyBindingSource.Count > 0 Then
                Dim oldBody = CType(RxBodyBindingSource.Current, RxBody)
                success = clsRxBodyData.Update(oldBody, newBody)
            Else
                success = clsRxBodyData.Add(newBody)
            End If

            If success Then
                clsRxBody = clsRxBodyData.SelectAll()
                RxBodyBindingSource.DataSource = clsRxBody.ToList()
                'Me.Close()
            Else
                MsgBox("Save operation failed.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
    '    Try
    '        Dim wimg As Image = Nothing
    '        Dim s As String = ""
    '        Dim useimg, usetxt As Boolean
    '        Dim row As RxBody = CType(RxBodyBindingSource.Current, RxBody)
    '        If Me.RxBodyBindingSource.Count > 0 Then
    '            useimg = row.UseWtrImg
    '            usetxt = row.UseWtrText
    '            If row.WtrImg.Length = 0 Then
    '                Dim ms As New IO.MemoryStream(CType(row.WtrImg, Byte())) 'This is correct...
    '                Dim returnImage As Image = Image.FromStream(ms)
    '                wimg = returnImage
    '                '============
    '                'wimg = ObjToImg(row.WtrImg)
    '                Dim _Path, appPath, picPath As String
    '                Dim ret As Boolean = False
    '                _Path = "\Images\"
    '                appPath = Application.StartupPath
    '                picPath = appPath & _Path
    '                ret = My.Computer.FileSystem.DirectoryExists(picPath)
    '                If ret = True Then
    '                    _Path = "\Images\Wtr.jpg"
    '                    picPath = appPath & _Path
    '                    If My.Computer.FileSystem.FileExists(picPath) = True Then
    '                        My.Computer.FileSystem.DeleteFile(picPath)
    '                    End If
    '                    wimg.Save(picPath, ImageFormat.Jpeg)
    '                End If
    '            End If
    '            If row.WtrText.Length = 0 Then
    '                s = row.WtrText
    '            End If
    '            Dim x As New MainRX(2, 7487)
    '            Dim pictureWatermark As New Watermark()
    '            If wimg IsNot Nothing Then
    '                If useimg Then
    '                    'Check water jpg
    '                    Dim _Path, appPath, picPath As String
    '                    Dim ret As Boolean = False
    '                    _Path = "\Images\Wtr.jpg"
    '                    appPath = Application.StartupPath
    '                    picPath = appPath & _Path ' "\Images\Patient_" & PatientID & "\Before\")
    '                    ret = My.Computer.FileSystem.FileExists(picPath)
    '                    If ret = True Then
    '                        pictureWatermark.ImageSource = ImageSource.FromFile(picPath)
    '                    End If
    '                    '============================
    '                    pictureWatermark.ImageAlign = ContentAlignment.MiddleCenter
    '                    pictureWatermark.ImageTiling = False
    '                    pictureWatermark.ImageViewMode = ImageViewMode.Zoom
    '                    pictureWatermark.ImageTransparency = 200
    '                    pictureWatermark.ShowBehind = True
    '                    'pictureWatermark.PageRange = "2,4"
    '                End If
    '                If s.Length > 0 Then
    '                    If usetxt Then
    '                        pictureWatermark.Text = s
    '                        pictureWatermark.TextDirection = DirectionMode.ForwardDiagonal
    '                        'textWatermark.Font = New DXFont(textWatermark.Font.Name, 40)
    '                        pictureWatermark.ForeColor = Color.DodgerBlue
    '                        pictureWatermark.TextTransparency = 50
    '                        pictureWatermark.ShowBehind = False
    '                        'textWatermark.PageRange = "1,3-5"
    '                        'x.Watermark.CopyFrom(pictureWatermark)
    '                    End If
    '                End If
    '                x.Watermark.CopyFrom(pictureWatermark)
    '            ElseIf wimg Is Nothing AndAlso usetxt = True Then
    '                If s.Length > 0 Then
    '                    If usetxt Then
    '                        pictureWatermark.Text = s
    '                        pictureWatermark.TextDirection = DirectionMode.ForwardDiagonal
    '                        'textWatermark.Font = New DXFont(textWatermark.Font.Name, 40)
    '                        pictureWatermark.ForeColor = Color.DodgerBlue
    '                        pictureWatermark.TextTransparency = 50
    '                        pictureWatermark.ShowBehind = False
    '                        'textWatermark.PageRange = "1,3-5"
    '                        'x.Watermark.CopyFrom(pictureWatermark)
    '                    End If

    '                End If
    '                x.Watermark.CopyFrom(pictureWatermark)
    '            End If
    '            '' Set paper size to A5 dimensions explicitly
    '            'x.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom
    '            'x.PageWidth = 148 ' Width of A5 in 1/100 of an inch
    '            'x.PageHeight = 210 ' Height of A5 in 1/100 of an inch
    '            'x.Landscape = False
    '            '' Force page setup to A5 dimensions before previewing
    '            ''x.CreateDocument()
    '            ''x.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup, New Object() {})
    '            Dim printTool As New ReportPrintTool(x)
    '            printTool.ShowPreviewDialog()
    '        Else
    '            If Eng Then
    '                MsgBox("The Rx Details Is Empty....." & vbCrLf & "Fill The Rx Details And Try Again.")
    '                Exit Sub
    '            Else
    '                MsgBox("تفاصيل الوصفة فارغة...." & vbCrLf & "قم بتعبئة تفاصيل الوصفة وحاول مرة اخرى.")
    '                Exit Sub
    '            End If
    '        End If
    '        ''If s.Length > 0 Then
    '        ''    Dim textWatermark As New Watermark()
    '        ''    textWatermark.Text = s
    '        ''    textWatermark.TextDirection = DirectionMode.ForwardDiagonal
    '        ''    'textWatermark.Font = New DXFont(textWatermark.Font.Name, 40)
    '        ''    textWatermark.ForeColor = Color.DodgerBlue
    '        ''    textWatermark.TextTransparency = 50
    '        ''    textWatermark.ShowBehind = False
    '        ''    'textWatermark.PageRange = "1,3-5"
    '        ''    x.Watermark.CopyFrom(textWatermark)
    '        ''End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

#End Region

#Region "Preview Rx"

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Try
            If RxBodyBindingSource.Count = 0 Then
                MsgBox(If(Eng, "The Rx Details Is Empty....." & vbCrLf & "Fill The Rx Details And Try Again.",
                              "تفاصيل الوصفة فارغة...." & vbCrLf & "قم بتعبئة تفاصيل الوصفة وحاول مرة اخرى."))
                Return
            End If

            Dim row As RxBody = CType(RxBodyBindingSource.Current, RxBody)
            Dim useImg As Boolean = row.UseWtrImg
            Dim useTxt As Boolean = row.UseWtrText
            Dim watermarkImg As Image = Nothing

            'If row.WtrImg?.Length > 0 Then
            '    Using ms As New MemoryStream(row.WtrImg)
            '        Using tempImage As Image = Image.FromStream(ms)
            '            watermarkImg = CType(tempImage.Clone(), Image)
            '        End Using
            '    End Using

            '    Dim wtrPath = Path.Combine(Application.StartupPath, "Images\Wtr.jpg")
            '    Directory.CreateDirectory(Path.GetDirectoryName(wtrPath))
            '    If File.Exists(wtrPath) Then File.Delete(wtrPath)

            '    watermarkImg.Save(wtrPath, ImageFormat.Jpeg)
            'End If

            If row.WtrImg?.Length > 0 Then
                Dim wtrPath = Path.Combine(Application.StartupPath, "Images\Wtr.jpg")
                Directory.CreateDirectory(Path.GetDirectoryName(wtrPath))
                If File.Exists(wtrPath) Then File.Delete(wtrPath)

                ' Clone as a new bitmap to fully disconnect from original stream
                Using ms As New MemoryStream(row.WtrImg)
                    Using originalImage As Image = Image.FromStream(ms)
                        Using bmp As New Bitmap(originalImage.Width, originalImage.Height)
                            Using g As Graphics = Graphics.FromImage(bmp)
                                g.DrawImage(originalImage, Point.Empty)
                            End Using
                            bmp.Save(wtrPath, ImageFormat.Jpeg)
                            watermarkImg = CType(bmp.Clone(), Image) ' if you need it later
                        End Using
                    End Using
                End Using
            End If

            Dim report As New MainRX '(106, 3)
            Dim pictureWatermark As New Watermark With {
                .ImageAlign = ContentAlignment.BottomCenter,
                .ImageTiling = False,
                .ImageViewMode = ImageViewMode.Zoom,
                .ImageTransparency = 200,
                .ShowBehind = True
            }

            If useImg AndAlso watermarkImg IsNot Nothing Then
                pictureWatermark.ImageSource = ImageSource.FromFile(Path.Combine(Application.StartupPath, "Images\Wtr.jpg"))
            End If

            If useTxt AndAlso Not String.IsNullOrWhiteSpace(row.WtrText) Then
                pictureWatermark.Text = row.WtrText
                pictureWatermark.TextDirection = DirectionMode.ForwardDiagonal
                pictureWatermark.ForeColor = Color.DodgerBlue
                pictureWatermark.TextTransparency = 50
                pictureWatermark.ShowBehind = False
            End If

            report.Watermark.CopyFrom(pictureWatermark)
            Dim printTool As New ReportPrintTool(report)
            printTool.ShowPreviewDialog()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDummy_Click(sender As Object, e As EventArgs) Handles btnDummy.Click
        Try
            If Logo.Image IsNot Nothing Then
                LogoByte = ImageToByteArray(Logo.Image)
            Else
                LogoByte = Nothing
            End If

            If Wtr.Image IsNot Nothing Then
                wtrByte = ImageToByteArray(Wtr.Image)
            Else
                wtrByte = Nothing
            End If


            Dim newBody As New RxBody With {
                .ArFtr = ArFtrTextBox.Text,
                .ArHdrAdres = ArHdrAdresTextBox.Text,
                .ArHdrName = ArHdrNameTextBox.Text,
                .Detail = DetailTextBox.Text,
                .DrName = DrNameText.Text,
                .EnFtr = EnFtrTextBox.Text,
                .EnHdrAdres = EnHdrAdresTextBox.Text,
                .EnHdrName = EnHdrNameTextBox.Text,
                .Logo = LogoByte,
                .UseWtrImg = UseWtrImg.Checked,
                .UseWtrText = UseWtrText.Checked,
                .WtrImg = wtrByte,
                .WtrText = WtrTextTextBox.Text
            }


            RxBodyBindingSource.DataSource = newBody

            'Catch ex As Exception
            '    MsgBox(ex.Message)
            'End Try
            'Try
            If RxBodyBindingSource.Count = 0 Then
                MsgBox(If(Eng, "The Rx Details Is Empty....." & vbCrLf & "Fill The Rx Details And Try Again.",
                              "تفاصيل الوصفة فارغة...." & vbCrLf & "قم بتعبئة تفاصيل الوصفة وحاول مرة اخرى."))
                Return
            End If

            Dim row As RxBody = newBody ' CType(RxBodyBindingSource.Current, RxBody)
            Dim useImg As Boolean = row.UseWtrImg
            Dim useTxt As Boolean = row.UseWtrText
            Dim watermarkImg As Image = Nothing

            'If row.WtrImg?.Length > 0 Then
            '    Using ms As New MemoryStream(row.WtrImg)
            '        Using tempImage As Image = Image.FromStream(ms)
            '            watermarkImg = CType(tempImage.Clone(), Image)
            '        End Using
            '    End Using

            '    Dim wtrPath = Path.Combine(Application.StartupPath, "Images\Wtr.jpg")
            '    Directory.CreateDirectory(Path.GetDirectoryName(wtrPath))
            '    If File.Exists(wtrPath) Then File.Delete(wtrPath)

            '    watermarkImg.Save(wtrPath, ImageFormat.Jpeg)
            'End If

            If row.WtrImg?.Length > 0 Then
                Dim wtrPath = Path.Combine(Application.StartupPath, "Images\Wtr.jpg")
                Directory.CreateDirectory(Path.GetDirectoryName(wtrPath))
                If File.Exists(wtrPath) Then File.Delete(wtrPath)

                ' Clone as a new bitmap to fully disconnect from original stream
                Using ms As New MemoryStream(row.WtrImg)
                    Using originalImage As Image = Image.FromStream(ms)
                        Using bmp As New Bitmap(originalImage.Width, originalImage.Height)
                            Using g As Graphics = Graphics.FromImage(bmp)
                                g.DrawImage(originalImage, Point.Empty)
                            End Using
                            bmp.Save(wtrPath, ImageFormat.Jpeg)
                            watermarkImg = CType(bmp.Clone(), Image) ' if you need it later
                        End Using
                    End Using
                End Using
            End If

            Dim report As New MainRX With {.DataSource = newBody}  '(106, 3)
            Dim pictureWatermark As New Watermark With {
                .ImageAlign = ContentAlignment.MiddleCenter,
                .ImageTiling = False,
                .ImageViewMode = ImageViewMode.Zoom,
                .ImageTransparency = 200,
                .ShowBehind = True
            }

            If useImg AndAlso watermarkImg IsNot Nothing Then
                pictureWatermark.ImageSource = ImageSource.FromFile(Path.Combine(Application.StartupPath, "Images\Wtr.jpg"))
            End If

            If useTxt AndAlso Not String.IsNullOrWhiteSpace(row.WtrText) Then
                pictureWatermark.Text = row.WtrText
                pictureWatermark.TextDirection = DirectionMode.ForwardDiagonal
                pictureWatermark.ForeColor = Color.DodgerBlue
                pictureWatermark.TextTransparency = 50
                pictureWatermark.ShowBehind = False
            End If

            report.Watermark.CopyFrom(pictureWatermark)
            Dim printTool As New ReportPrintTool(report)
            printTool.ShowPreviewDialog()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        Try
            If RxBodyBindingSource Is Nothing Then
                RxBodyBindingSource = New BindingSource()
            End If
            clsRxBody = clsRxBodyData.SelectAll()
            RxBodyBindingSource.DataSource = clsRxBody.ToList()
            BindData()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub UseWtrImg_CheckedChanged(sender As Object, e As EventArgs) Handles UseWtrImg.CheckedChanged

    End Sub

    Private Sub UseWtrText_CheckedChanged(sender As Object, e As EventArgs) Handles UseWtrText.CheckedChanged

    End Sub

    Private Sub btnEditMode_Click(sender As Object, e As EventArgs) Handles btnEditMode.Click
        UnBindData()
    End Sub

    Private Sub btnGetDr_Click(sender As Object, e As EventArgs) Handles btnGetDr.Click
        Me.DrNameText.Text = If(CurrentDoctor IsNot Nothing, CurrentDoctor.DrName, "")
    End Sub







#End Region

End Class

'Public Class FrmRxDetails

'    Dim fileName As String
'    Dim LogoByte() As Byte
'    Dim wtrByte() As Byte
'    Private clsRxBody As IEnumerable(Of RxBody)
'    Private clsRxBodyData As New RxBodyDATA
'    Private Sub FrmRxDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        Try
'            If RxBodyBindingSource Is Nothing Then
'                RxBodyBindingSource = New BindingSource()
'            End If
'            clsRxBody = clsRxBodyData.SelectAll
'            RxBodyBindingSource.DataSource = clsRxBody.ToList
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'    End Sub
'    Private Function GenerateThumbnail(image As Image, width As Integer, height As Integer) As Image
'        Dim thumbnail As New Bitmap(width, height)
'        Using g As Graphics = Graphics.FromImage(thumbnail)
'            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
'            g.DrawImage(image, New Rectangle(0, 0, width, height))
'        End Using
'        Return thumbnail
'    End Function
'    Public Sub Image2Byte(ByRef NewImage As Image, ByRef ByteArr() As Byte)
'        Dim ImageStream As MemoryStream
'        Try
'            ReDim ByteArr(0)
'            If NewImage IsNot Nothing Then
'                ImageStream = New MemoryStream
'                NewImage.Save(ImageStream, ImageFormat.Jpeg)
'                ReDim ByteArr(CInt(ImageStream.Length - 1))
'                ImageStream.Position = 0
'                ImageStream.Read(ByteArr, 0, CInt(ImageStream.Length))
'            End If
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'    End Sub
'    Public Function imageToByteArray(ByVal imageIn As System.Drawing.Image) As Byte()
'        Dim ms As New MemoryStream()
'        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
'        Return ms.ToArray()
'    End Function
'    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
'        Try
'            Dim img As Image = Logo.Image
'            If OFD.ShowDialog = DialogResult.OK Then
'                fileName = OFD.FileName
'                Logo.Image = GenerateThumbnail(Image.FromFile(fileName), 100, 100)
'            Else
'                Logo.Image = img
'            End If
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'    End Sub
'    Private Sub BtnBrowsWtr_Click(sender As Object, e As EventArgs) Handles BtnBrowsWtr.Click
'        Try
'            '"Text files (*.txt)|*.txt|All files (*.*)|*.*"
'            '.Filter = "All files (*.*)|*.*|Bitmap Files(*.bmp)|*.bmp|JPeg Files(*.jpg)|*.jpg|Gif Files(*.gif)|*.gif"
'            Dim img As Image = Wtr.Image
'            If OFD.ShowDialog = DialogResult.OK Then
'                fileName = OFD.FileName
'                Wtr.Image = GenerateThumbnail(Image.FromFile(fileName), 350, 350)
'            Else
'                Wtr.Image = img
'            End If
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'    End Sub
'    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
'        Try
'            If Logo.Image IsNot Nothing Then
'                Image2Byte(Logo.Image, LogoByte)
'            End If
'            If Wtr.Image IsNot Nothing Then
'                Image2Byte(Wtr.Image, wtrByte)
'            End If
'            If RxBodyBindingSource.Count > 0 Then
'                Dim oldBody As RxBody = CType(RxBodyBindingSource.Current, RxBody)
'                Dim newBody As New RxBody With {.ArFtr = ArFtrTextBox.Text, .ArHdrAdres = ArHdrAdresTextBox.Text, .ArHdrName = ArHdrNameTextBox.Text,
'                                                .Detail = DetailTextBox.Text, .DrName = DrNameText.Text, .EnFtr = EnFtrTextBox.Text, .EnHdrAdres = EnHdrAdresTextBox.Text,
'                                                .EnHdrName = EnHdrNameTextBox.Text, .Logo = LogoByte, .UseWtrImg = UseWtrImg.Checked, .UseWtrText = UseWtrText.Checked,
'                                                .WtrImg = wtrByte, .WtrText = WtrTextTextBox.Text}
'                If clsRxBodyData.Update(oldBody, newBody) Then
'                    clsRxBody = clsRxBodyData.SelectAll
'                    RxBodyBindingSource.DataSource = clsRxBody.ToList
'                    Me.Close()
'                Else
'                    MsgBox("Update fields has failed..")
'                End If
'            Else
'                Dim newBody As New RxBody With {.ArFtr = ArFtrTextBox.Text, .ArHdrAdres = ArHdrAdresTextBox.Text, .ArHdrName = ArHdrNameTextBox.Text,
'                                                .Detail = DetailTextBox.Text, .DrName = DrNameText.Text, .EnFtr = EnFtrTextBox.Text, .EnHdrAdres = EnHdrAdresTextBox.Text,
'                                                .EnHdrName = EnHdrNameTextBox.Text, .Logo = LogoByte, .UseWtrImg = UseWtrImg.Checked, .UseWtrText = UseWtrText.Checked,
'                                                .WtrImg = wtrByte, .WtrText = WtrTextTextBox.Text}

'                If clsRxBodyData.Add(newBody) Then
'                    clsRxBody = clsRxBodyData.SelectAll
'                    RxBodyBindingSource.DataSource = clsRxBody.ToList
'                    Me.Close()
'                Else
'                    MsgBox("Insert new fields has failed..")
'                End If
'            End If
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'    End Sub
'    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
'        Try
'            Dim wimg As Image = Nothing
'            Dim s As String = ""
'            Dim useimg, usetxt As Boolean
'            Dim row As RxBody = CType(RxBodyBindingSource.Current, RxBody)
'            If Me.RxBodyBindingSource.Count > 0 Then
'                useimg = row.UseWtrImg
'                usetxt = row.UseWtrText
'                If row.WtrImg.Length = 0 Then
'                    Dim ms As New IO.MemoryStream(CType(row.WtrImg, Byte())) 'This is correct...
'                    Dim returnImage As Image = Image.FromStream(ms)
'                    wimg = returnImage
'                    '============
'                    'wimg = ObjToImg(row.WtrImg)
'                    Dim _Path, appPath, picPath As String
'                    Dim ret As Boolean = False
'                    _Path = "\Images\"
'                    appPath = Application.StartupPath
'                    picPath = appPath & _Path
'                    ret = My.Computer.FileSystem.DirectoryExists(picPath)
'                    If ret = True Then
'                        _Path = "\Images\Wtr.jpg"
'                        picPath = appPath & _Path
'                        If My.Computer.FileSystem.FileExists(picPath) = True Then
'                            My.Computer.FileSystem.DeleteFile(picPath)
'                        End If
'                        wimg.Save(picPath, ImageFormat.Jpeg)
'                    End If
'                End If
'                If row.WtrText.Length = 0 Then
'                    s = row.WtrText
'                End If
'                Dim x As New MainRX(2, 7487)
'                Dim pictureWatermark As New Watermark()
'                If wimg IsNot Nothing Then
'                    If useimg Then
'                        'Check water jpg
'                        Dim _Path, appPath, picPath As String
'                        Dim ret As Boolean = False
'                        _Path = "\Images\Wtr.jpg"
'                        appPath = Application.StartupPath
'                        picPath = appPath & _Path ' "\Images\Patient_" & PatientID & "\Before\")
'                        ret = My.Computer.FileSystem.FileExists(picPath)
'                        If ret = True Then
'                            pictureWatermark.ImageSource = ImageSource.FromFile(picPath)
'                        End If
'                        '============================
'                        pictureWatermark.ImageAlign = ContentAlignment.MiddleCenter
'                        pictureWatermark.ImageTiling = False
'                        pictureWatermark.ImageViewMode = ImageViewMode.Zoom
'                        pictureWatermark.ImageTransparency = 200
'                        pictureWatermark.ShowBehind = True
'                        'pictureWatermark.PageRange = "2,4"
'                    End If
'                    If s.Length > 0 Then
'                        If usetxt Then
'                            pictureWatermark.Text = s
'                            pictureWatermark.TextDirection = DirectionMode.ForwardDiagonal
'                            'textWatermark.Font = New DXFont(textWatermark.Font.Name, 40)
'                            pictureWatermark.ForeColor = Color.DodgerBlue
'                            pictureWatermark.TextTransparency = 50
'                            pictureWatermark.ShowBehind = False
'                            'textWatermark.PageRange = "1,3-5"
'                            'x.Watermark.CopyFrom(pictureWatermark)
'                        End If
'                    End If
'                    x.Watermark.CopyFrom(pictureWatermark)
'                ElseIf wimg Is Nothing AndAlso usetxt = True Then
'                    If s.Length > 0 Then
'                        If usetxt Then
'                            pictureWatermark.Text = s
'                            pictureWatermark.TextDirection = DirectionMode.ForwardDiagonal
'                            'textWatermark.Font = New DXFont(textWatermark.Font.Name, 40)
'                            pictureWatermark.ForeColor = Color.DodgerBlue
'                            pictureWatermark.TextTransparency = 50
'                            pictureWatermark.ShowBehind = False
'                            'textWatermark.PageRange = "1,3-5"
'                            'x.Watermark.CopyFrom(pictureWatermark)
'                        End If

'                    End If
'                    x.Watermark.CopyFrom(pictureWatermark)
'                End If
'                '' Set paper size to A5 dimensions explicitly
'                'x.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom
'                'x.PageWidth = 148 ' Width of A5 in 1/100 of an inch
'                'x.PageHeight = 210 ' Height of A5 in 1/100 of an inch
'                'x.Landscape = False
'                '' Force page setup to A5 dimensions before previewing
'                ''x.CreateDocument()
'                ''x.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup, New Object() {})
'                Dim printTool As New ReportPrintTool(x)
'                printTool.ShowPreviewDialog()
'            Else
'                If Eng Then
'                    MsgBox("The Rx Details Is Empty....." & vbCrLf & "Fill The Rx Details And Try Again.")
'                    Exit Sub
'                Else
'                    MsgBox("تفاصيل الوصفة فارغة...." & vbCrLf & "قم بتعبئة تفاصيل الوصفة وحاول مرة اخرى.")
'                    Exit Sub
'                End If
'            End If
'            ''If s.Length > 0 Then
'            ''    Dim textWatermark As New Watermark()
'            ''    textWatermark.Text = s
'            ''    textWatermark.TextDirection = DirectionMode.ForwardDiagonal
'            ''    'textWatermark.Font = New DXFont(textWatermark.Font.Name, 40)
'            ''    textWatermark.ForeColor = Color.DodgerBlue
'            ''    textWatermark.TextTransparency = 50
'            ''    textWatermark.ShowBehind = False
'            ''    'textWatermark.PageRange = "1,3-5"
'            ''    x.Watermark.CopyFrom(textWatermark)
'            ''End If
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'    End Sub
'End Class