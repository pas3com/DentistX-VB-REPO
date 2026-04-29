Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports DevExpress.XtraBars
Imports DevExpress.XtraGrid.Views.Base

Partial Public Class FrmClinic


    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub

    Private clsClinicData As New ClinicDATA
    Private clsDentistXData As New DentistXDATA
    Private LogoByte() As Byte

    Private Sub FrmClinic_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDGV()
        ShowToolStripItems("Cancel")
    End Sub
    Private bAddMode As Boolean = False
    Private bEditMode As Boolean = False
    Private bDeleteMode As Boolean = False
    Private iRow As Int32 = 0

    Public Property AddMode() As Boolean
        Get
            Return bAddMode
        End Get
        Set(ByVal value As Boolean)
            bAddMode = value
        End Set
    End Property

    Public Property EditMode() As Boolean
        Get
            Return bEditMode
        End Get
        Set(ByVal value As Boolean)
            bEditMode = value
        End Set
    End Property

    Public Property DeleteMode() As Boolean
        Get
            Return bDeleteMode
        End Get
        Set(ByVal value As Boolean)
            bDeleteMode = value
        End Set
    End Property

    Public Property Row() As Int32
        Get
            Return iRow
        End Get
        Set(ByVal value As Int32)
            iRow = value
        End Set
    End Property

    Private Sub LoadDGV()
        Try
            With DGV
                .DataSource = clsClinicData.SelectAll
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    ClearRecord()
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGV_Click(sender As Object, e As System.EventArgs) Handles DGV.Click
        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGV_SelectionChanged(sender As Object, e As System.EventArgs) Handles dgView.SelectionChanged
        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub EnableRecord(ByVal YesNo As Boolean)
        ClinicNameEnTextEdit.Enabled = YesNo
        ClinicNameArTextEdit.Enabled = YesNo
        DrNameEnTextEdit.Enabled = YesNo
        DrNameArTextEdit.Enabled = YesNo
        SpecialistEnTextEdit.Enabled = YesNo
        SpecialistArTextEdit.Enabled = YesNo
        AddressEnTextEdit.Enabled = YesNo
        AddressArTextEdit.Enabled = YesNo
        PhoneTextEdit.Enabled = YesNo
        MobileTextEdit.Enabled = YesNo
        EmailTextEdit.Enabled = YesNo
    End Sub


    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Select Case btnDel.Text
            Case "Delete"
                Delete()
                btnDel.Text = "Apply Delete"
            Case "Apply Delete"
                Me.DeleteRecord()
                ClearRecord()
                btnDel.Text = "Delete"
                Me.DeleteMode = False
        End Select
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Select Case btnEdit.Text
            Case "Edit"
                Edit()
                btnEdit.Text = "Apply Edit"
            Case "Apply Edit"
                Me.UpdateRecord()
                ClearRecord()
                btnEdit.Text = "Edit"
                Me.EditMode = False
        End Select
    End Sub
    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Me.Close()
    End Sub

    Private Async Sub butApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Select Case btnAdd.Text
            Case "Add"
                Add()
                btnAdd.Text = "Apply Add"
            Case "Apply Add"
                Await Me.InsertRecordAsync()
                ClearRecord()
                btnAdd.Text = "Add"
                Me.AddMode = False
        End Select
    End Sub



    Private Sub Display()
        ClearRecord()
        Dim clsClinic As New Clinic
        Dim idText As String = dgView.GetRowCellDisplayText(Row, colClinicID)
        Dim parsedId As Guid
        If Not String.IsNullOrWhiteSpace(idText) AndAlso Guid.TryParse(idText, parsedId) Then
            clsClinic.ClinicID = parsedId
            clsClinic = clsClinicData.Select_Record(parsedId)
        Else
            clsClinic = Nothing
        End If
        If clsClinic IsNot Nothing Then
            Try
                ClinicIDSpinEdit.Text = clsClinic.ClinicID.ToString()
                ClinicNameEnTextEdit.Text = Convert.ToString(clsClinic.ClinicNameEn)
                ClinicNameArTextEdit.Text = Convert.ToString(clsClinic.ClinicNameAr)
                DrNameEnTextEdit.Text = Convert.ToString(clsClinic.DrNameEn)
                DrNameArTextEdit.Text = Convert.ToString(clsClinic.DrNameAr)
                SpecialistEnTextEdit.Text = Convert.ToString(clsClinic.SpecialistEn)
                SpecialistArTextEdit.Text = Convert.ToString(clsClinic.SpecialistAr)
                AddressEnTextEdit.Text = Convert.ToString(clsClinic.AddressEn)
                AddressArTextEdit.Text = Convert.ToString(clsClinic.AddressAr)
                PhoneTextEdit.Text = Convert.ToString(clsClinic.Phone)
                MobileTextEdit.Text = Convert.ToString(clsClinic.Mobile)
                EmailTextEdit.Text = Convert.ToString(clsClinic.Email)
                If clsClinic.ClinicLogo IsNot Nothing AndAlso clsClinic.ClinicLogo.Length > 0 Then
                    Logo.Image = ByteArrayToImage(clsClinic.ClinicLogo)
                    LogoByte = clsClinic.ClinicLogo
                Else
                    Logo.Image = Nothing
                    LogoByte = Nothing
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Add()
        Me.AddMode = True
        Me.EditMode = False
        Me.DeleteMode = False
        ClearRecord()
        ShowToolStripItems("Add")
        ClinicIDSpinEdit.Text = Guid.NewGuid().ToString()
        ClinicIDSpinEdit.Select()
    End Sub

    Private Sub Edit()
        Me.AddMode = False
        Me.EditMode = True
        Me.DeleteMode = False
        Row = dgView.FocusedRowHandle
        If Row < 0 OrElse Row >= dgView.RowCount Then
            MsgBox("Please select a clinic to edit.", MsgBoxStyle.Exclamation, "Edit")
            Return
        End If
        Display()
        ShowToolStripItems("Edit")
    End Sub

    Private Sub Delete()
        Me.AddMode = False
        Me.EditMode = False
        Me.DeleteMode = True
        ShowToolStripItems("Delete")
    End Sub

    Private Sub ClearRecord()
        ClinicIDSpinEdit.Text = ""
        ClinicNameEnTextEdit.Text = Nothing
        ClinicNameArTextEdit.Text = Nothing
        DrNameEnTextEdit.Text = Nothing
        DrNameArTextEdit.Text = Nothing
        SpecialistEnTextEdit.Text = Nothing
        SpecialistArTextEdit.Text = Nothing
        AddressEnTextEdit.Text = Nothing
        AddressArTextEdit.Text = Nothing
        PhoneTextEdit.Text = Nothing
        MobileTextEdit.Text = Nothing
        EmailTextEdit.Text = Nothing
    End Sub

    Private Sub GoBack_To_Grid()
        RemoveHandler dgView.SelectionChanged, AddressOf DGV_SelectionChanged
        Dim gridOK As Boolean = False
        Try
            LoadDGV()
            ShowToolStripItems("Cancel")
            dgView.SelectRow(Row)
            dgView.FocusedRowHandle = Row
            dgView.Focus()
            gridOK = True
        Catch ex As Exception
            MsgBox(ex.Message)
            'Hide error message.
        Finally
            If gridOK = False Then
                ''''
                ShowToolStripItems("Cancel")
                ''''
            End If
        End Try
        AddHandler dgView.SelectionChanged, AddressOf DGV_SelectionChanged
    End Sub
    Private Sub SetData(ByVal clsClinic As Clinic)
        If Logo.Image IsNot Nothing Then
            LogoByte = ImageToByteArray(Logo.Image)
        Else
            LogoByte = Nothing
        End If
        With clsClinic
            Dim idText As String = If(ClinicIDSpinEdit.Text, "").Trim()
            Dim parsedId As Guid
            If Not String.IsNullOrEmpty(idText) AndAlso Guid.TryParse(idText, parsedId) Then .ClinicID = parsedId
            .ClinicNameEn = System.Convert.ToString(ClinicNameEnTextEdit.Text)
            .ClinicNameAr = System.Convert.ToString(ClinicNameArTextEdit.Text)
            .DrNameEn = System.Convert.ToString(DrNameEnTextEdit.Text)
            .DrNameAr = System.Convert.ToString(DrNameArTextEdit.Text)
            .SpecialistEn = System.Convert.ToString(SpecialistEnTextEdit.Text)
            .SpecialistAr = System.Convert.ToString(SpecialistArTextEdit.Text)
            .AddressEn = System.Convert.ToString(AddressEnTextEdit.Text)
            .AddressAr = System.Convert.ToString(AddressArTextEdit.Text)
            .Phone = System.Convert.ToString(PhoneTextEdit.Text)
            .Mobile = System.Convert.ToString(MobileTextEdit.Text)
            .Email = System.Convert.ToString(EmailTextEdit.Text)
            .ClinicLogo = LogoByte
        End With
    End Sub

    ''' <summary>
    ''' Adds clinic locally and to WhatsApp API. Registers on WhatsApp API first so ConnectAsync can work
    ''' when the user opens WhatsAppForm (clinic must exist on remote before connecting).
    ''' </summary>
    Private Async Function InsertRecordAsync() As Task
        Dim clsClinic As New Clinic
        If VerifyData() = True Then
            SetData(clsClinic)

            ' 1. Register clinic on WhatsApp API first (required before ConnectAsync can work)
            Try
                Dim waService As New WhatsAppService()
                Dim result = Await waService.CreateCustomerAsync(clsClinic)
                If Not result.Success Then
                    Dim msg = If(String.IsNullOrEmpty(result.ErrorMessage),
                        "WhatsApp API sync failed. Clinic will be saved locally only.",
                        "WhatsApp API error:" & vbCrLf & vbCrLf & result.ErrorMessage)
                    MsgBox(msg, MsgBoxStyle.Exclamation, "WhatsApp Sync Warning")
                End If
            Catch ex As Exception
                MsgBox("WhatsApp API sync failed. Clinic will be saved locally only." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "WhatsApp Sync Warning")
            End Try

            ' 2. Add locally (always, so user has data even if offline)
            Dim bSuccess As Boolean = clsClinicData.Add(clsClinic)
            If bSuccess Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Function

    Private Sub UpdateRecord()
        Dim oclsClinic As New Clinic
        Dim clsClinic As New Clinic
        Dim idText As String = dgView.GetRowCellDisplayText(Row, colClinicID)
        Dim parsedId As Guid
        If String.IsNullOrWhiteSpace(idText) OrElse Not Guid.TryParse(idText, parsedId) Then Return
        oclsClinic.ClinicID = parsedId
        oclsClinic = clsClinicData.Select_Record(parsedId)
        If VerifyData() = True Then
            SetData(clsClinic)
            Dim bSuccess As Boolean
            bSuccess = clsClinicData.Update(oclsClinic, clsClinic)
            If bSuccess = True Then
                ' Sync clinic to WhatsApp API (fire-and-forget; do not block UI)
                Task.Run(Async Function()
                             Try
                                 Dim waService As New WhatsAppService()
                                 Dim result = Await waService.UpdateCustomerAsync(clsClinic)
                                 If Not result.Success Then
                                     Dim msg = If(String.IsNullOrEmpty(result.ErrorMessage),
                                         "Clinic updated locally but failed to sync with WhatsApp API.",
                                         "Clinic updated locally. WhatsApp API error:" & vbCrLf & vbCrLf & result.ErrorMessage)
                                     Me.BeginInvoke(New Action(Sub()
                                                                   MsgBox(msg, MsgBoxStyle.Exclamation, "Sync Warning")
                                                               End Sub))
                                 End If
                             Catch ex As Exception
                                 Me.BeginInvoke(New Action(Sub()
                                                               MsgBox("Clinic updated locally but failed to sync with WhatsApp API: " & ex.Message, MsgBoxStyle.Exclamation, "Sync Warning")
                                                           End Sub))
                             End Try
                         End Function)
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsClinic As New Clinic
        Dim idText As String = dgView.GetRowCellDisplayText(Row, colClinicID)
        Dim parsedId As Guid
        If String.IsNullOrWhiteSpace(idText) OrElse Not Guid.TryParse(idText, parsedId) Then Return
        clsClinic.ClinicID = parsedId
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsClinicData.Delete(clsClinic)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields (local + WhatsApp API)
        If String.IsNullOrWhiteSpace(ClinicIDSpinEdit.Text) Then
            MsgBox("ClinicID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            ClinicIDSpinEdit.Select()
            Return False
        End If
        If String.IsNullOrWhiteSpace(ClinicNameEnTextEdit.Text) Then
            MsgBox("ClinicNameEn is required.", MsgBoxStyle.OkOnly, "Entry Error")
            ClinicNameEnTextEdit.Select()
            Return False
        End If
        ' WhatsApp API requires these for CreateCustomerAsync
        If String.IsNullOrWhiteSpace(ClinicNameArTextEdit.Text) Then
            MsgBox("ClinicNameAr is required for WhatsApp sync.", MsgBoxStyle.OkOnly, "Entry Error")
            ClinicNameArTextEdit.Select()
            Return False
        End If
        If String.IsNullOrWhiteSpace(DrNameEnTextEdit.Text) Then
            MsgBox("DrNameEn is required for WhatsApp sync.", MsgBoxStyle.OkOnly, "Entry Error")
            DrNameEnTextEdit.Select()
            Return False
        End If
        If String.IsNullOrWhiteSpace(DrNameArTextEdit.Text) Then
            MsgBox("DrNameAr is required for WhatsApp sync.", MsgBoxStyle.OkOnly, "Entry Error")
            DrNameArTextEdit.Select()
            Return False
        End If
        Return True
    End Function

    Private Sub ShowToolStripItems(ByVal Item As String)

        Select Case Item
            Case "Add"
                EnableRecord(True)
                ClinicIDSpinEdit.Enabled = True
            Case "Edit"
                EnableRecord(True)
                ClinicIDSpinEdit.Enabled = False
            Case "Delete"
                EnableRecord(False)
                ClinicIDSpinEdit.Enabled = False
            Case "Cancel"
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                    EnableRecord(False)
                    ClinicIDSpinEdit.Enabled = False
                Else
                    EnableRecord(False)
                    ClinicIDSpinEdit.Enabled = False
                    DGV.Focus()
                End If
            Case "Refresh"

                LoadDGV()
            Case "No Record"

        End Select
    End Sub

#Region "Image Utilities"

    Private Function ByteArrayToImage(bytes As Byte()) As Image
        If bytes Is Nothing OrElse bytes.Length = 0 Then Return Nothing
        Try
            Using ms As New MemoryStream(bytes)
                Return New Bitmap(Image.FromStream(ms))
            End Using
        Catch
            Return Nothing
        End Try
    End Function

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



#End Region

End Class
