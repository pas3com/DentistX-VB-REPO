Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.XtraReports.UI

Public Class RxDetailFrm



    Dim f As String
    Dim LogoByte() As Byte
    Dim wtrByte() As Byte

    Private Sub RxDetailFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RxDetails.RxBody' table. You can move, or remove it, as needed.

        Try
            'Me.RxBodyTableAdapter.Fill(Me.RxDetails.RxBody)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    '=================================================
    Private Function GenerateThumbnail(image As Image, width As Integer, height As Integer) As Image
        Dim thumbnail As New Bitmap(width, height)
        Using g As Graphics = Graphics.FromImage(thumbnail)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(image, New Rectangle(0, 0, width, height))
        End Using

        Return thumbnail
    End Function

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
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Function imageToByteArray(ByVal imageIn As System.Drawing.Image) As Byte()
        Dim ms As New MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            Dim img As Image = Logo.Image
            If OFD.ShowDialog = DialogResult.OK Then
                f = OFD.FileName
                Logo.Image = GenerateThumbnail(Image.FromFile(f), 100, 100)
            Else
                Logo.Image = img
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub BtnBrowsWtr_Click(sender As Object, e As EventArgs) Handles BtnBrowsWtr.Click
        Try



            '"Text files (*.txt)|*.txt|All files (*.*)|*.*"
            '.Filter = "All files (*.*)|*.*|Bitmap Files(*.bmp)|*.bmp|JPeg Files(*.jpg)|*.jpg|Gif Files(*.gif)|*.gif"
            Dim img As Image = Wtr.Image
            If OFD.ShowDialog = DialogResult.OK Then
                f = OFD.FileName
                Wtr.Image = GenerateThumbnail(Image.FromFile(f), 350, 350)
            Else
                Wtr.Image = img
            End If



            'Dim _Path, appPath, picPath As String
            'Dim ret As Boolean = False
            '_Path = "\Images\"
            'appPath = Application.StartupPath
            'picPath = appPath & _Path ' "\Images\Patient_" & PatientID & "\Before\")
            'ret = My.Computer.FileSystem.DirectoryExists(picPath)
            'If ret = True Then
            '    Wtr.Image.Save(picPath & "Wtr.jpg")
            'End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try

            If Logo.Image IsNot Nothing Then
                Image2Byte(Logo.Image, LogoByte)

            End If

            If Wtr.Image IsNot Nothing Then
                Image2Byte(Wtr.Image, wtrByte)

            End If


            'If RxBodyBindingSource.Count > 0 Then
            '    Me.RxBodyTableAdapter.Update(ArHdrNameTextBox.Text, ArHdrAdresTextBox.Text, EnHdrNameTextBox.Text,
            '                                               EnHdrAdresTextBox.Text, LogoByte, DetailTextBox.Text, ArFtrTextBox.Text,
            '                                               EnFtrTextBox.Text, wtrByte, WtrTextTextBox.Text, UseWtrImg.Checked,
            '                                               UseWtrText.Checked, DrNameText.Text, 1)
            '    Me.RxBodyTableAdapter.Fill(Me.RxDetails.RxBody)
            '    Me.Close()
            'Else
            '    Me.RxBodyTableAdapter.Insert(1, ArHdrNameTextBox.Text, ArHdrAdresTextBox.Text, EnHdrNameTextBox.Text,
            '                                           EnHdrAdresTextBox.Text, LogoByte, DetailTextBox.Text, ArFtrTextBox.Text,
            '                                           EnFtrTextBox.Text, wtrByte, WtrTextTextBox.Text, UseWtrImg.Checked, UseWtrText.Checked, DrNameText.Text)
            '    Me.RxBodyTableAdapter.Fill(Me.RxDetails.RxBody)
            '    Me.Close()
            'End If




        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click

        Try
            Dim wimg As Image = Nothing
            Dim s As String = ""
            'Dim row As RxDetails.RxBodyRow
            'If Me.RxBodyBindingSource.Count > 0 Then
            '    row = CType(CType(Me.RxBodyBindingSource.Current, DataRowView).Row, DentistX.RxDetails.RxBodyRow)
            '    useimg = row.UseWtrImg
            '    usetxt = row.UseWtrText

            '    If row.IsWtrImgNull = False Then
            '        Dim ms As New IO.MemoryStream(CType(row.WtrImg, Byte())) 'This is correct...
            '        Dim returnImage As Image = Image.FromStream(ms)
            '        wimg = returnImage
            '        '============
            '        'wimg = ObjToImg(row.WtrImg)
            '        Dim _Path, appPath, picPath As String
            '        Dim ret As Boolean = False
            '        _Path = "\Images\"
            '        appPath = Application.StartupPath
            '        picPath = appPath & _Path
            '        ret = My.Computer.FileSystem.DirectoryExists(picPath)
            '        If ret = True Then
            '            _Path = "\Images\Wtr.jpg"
            '            picPath = appPath & _Path
            '            If My.Computer.FileSystem.FileExists(picPath) = True Then
            '                My.Computer.FileSystem.DeleteFile(picPath)
            '            End If

            '            wimg.Save(picPath, ImageFormat.Jpeg)
            '        End If
            '    End If

            '    If row.IsWtrTextNull = False Then
            '        s = row.WtrText
            '    End If

            '    Dim x As New MainRX(2, 7487)
            '    Dim pictureWatermark As New Watermark()

            '    If wimg IsNot Nothing Then
            '        If useimg Then

            '            '==========================================
            '            'Check water jpg
            '            Dim _Path, appPath, picPath As String
            '            Dim ret As Boolean = False
            '            _Path = "\Images\Wtr.jpg"
            '            appPath = Application.StartupPath
            '            picPath = appPath & _Path ' "\Images\Patient_" & PatientID & "\Before\")
            '            ret = My.Computer.FileSystem.FileExists(picPath)
            '            If ret = True Then
            '                pictureWatermark.ImageSource = ImageSource.FromFile(picPath)
            '            End If
            '            '============================
            '            pictureWatermark.ImageAlign = ContentAlignment.MiddleCenter
            '            pictureWatermark.ImageTiling = False
            '            pictureWatermark.ImageViewMode = ImageViewMode.Zoom
            '            pictureWatermark.ImageTransparency = 200
            '            pictureWatermark.ShowBehind = True
            '            'pictureWatermark.PageRange = "2,4"
            '        End If
            '        If s.Length > 0 Then
            '            If usetxt Then
            '                pictureWatermark.Text = s
            '                pictureWatermark.TextDirection = DirectionMode.ForwardDiagonal
            '                'textWatermark.Font = New DXFont(textWatermark.Font.Name, 40)
            '                pictureWatermark.ForeColor = Color.DodgerBlue
            '                pictureWatermark.TextTransparency = 50
            '                pictureWatermark.ShowBehind = False
            '                'textWatermark.PageRange = "1,3-5"
            '                'x.Watermark.CopyFrom(pictureWatermark)
            '            End If

            '        End If
            '        x.Watermark.CopyFrom(pictureWatermark)
            '    ElseIf wimg Is Nothing AndAlso usetxt = True Then
            '        If s.Length > 0 Then
            '            If usetxt Then
            '                pictureWatermark.Text = s
            '                pictureWatermark.TextDirection = DirectionMode.ForwardDiagonal
            '                'textWatermark.Font = New DXFont(textWatermark.Font.Name, 40)
            '                pictureWatermark.ForeColor = Color.DodgerBlue
            '                pictureWatermark.TextTransparency = 50
            '                pictureWatermark.ShowBehind = False
            '                'textWatermark.PageRange = "1,3-5"
            '                'x.Watermark.CopyFrom(pictureWatermark)
            '            End If

            '        End If
            '        x.Watermark.CopyFrom(pictureWatermark)
            '    End If
            '    '' Set paper size to A5 dimensions explicitly
            '    'x.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom
            '    'x.PageWidth = 148 ' Width of A5 in 1/100 of an inch
            '    'x.PageHeight = 210 ' Height of A5 in 1/100 of an inch
            '    'x.Landscape = False

            '    '' Force page setup to A5 dimensions before previewing
            '    ''x.CreateDocument()
            '    ''x.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup, New Object() {})

            '    Dim printTool As New ReportPrintTool(x)
            '    printTool.ShowPreviewDialog()



            'Else
            '    If Eng Then
            '        MsgBox("The Rx Details Is Empty....." & vbCrLf & "Fill The Rx Details And Try Again.")
            '        Exit Sub
            '    Else
            '        MsgBox("تفاصيل الوصفة فارغة...." & vbCrLf & "قم بتعبئة تفاصيل الوصفة وحاول مرة اخرى.")
            '        Exit Sub
            '    End If

            'End If




            ''If s.Length > 0 Then
            ''    Dim textWatermark As New Watermark()
            ''    textWatermark.Text = s
            ''    textWatermark.TextDirection = DirectionMode.ForwardDiagonal
            ''    'textWatermark.Font = New DXFont(textWatermark.Font.Name, 40)
            ''    textWatermark.ForeColor = Color.DodgerBlue
            ''    textWatermark.TextTransparency = 50
            ''    textWatermark.ShowBehind = False
            ''    'textWatermark.PageRange = "1,3-5"
            ''    x.Watermark.CopyFrom(textWatermark)
            ''End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class