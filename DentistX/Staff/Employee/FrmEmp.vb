Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraBars
Imports DevExpress.XtraGrid.Views.Base
Imports DentistX

Partial Public Class FrmEmp


    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub

    Private Sub bbiPrintPreview_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
        DGV.ShowRibbonPrintPreview()
    End Sub
    Private clsEmpData As New EmpDATA
    Private clsDentistXData As New DentistXDATA


    Private Sub FrmEmp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WhatsHelper.AttachWhatsLocalDigitsOnlyKeyDown(EmpWhatsTextEdit)
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
                .DataSource = clsEmpData.SelectAll
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
        EmpIDSpinEdit.Enabled = False
        EmpNameTextEdit.Enabled = YesNo
        EmpPhoneTextEdit.Enabled = YesNo
        EmpAddressTextEdit.Enabled = YesNo
        EmpImgImageEdit.Enabled = YesNo
        btnBrowse.Enabled = YesNo
        EmpWhatsPrefixCombo.Enabled = YesNo
        EmpWhatsTextEdit.Enabled = YesNo
    End Sub

    Private Sub SetApplyCaptionAdd()
        btnAdd.Text = If(Eng, "Apply Add", "تطبيق الإضافة")
    End Sub

    Private Sub SetApplyCaptionEdit()
        btnEdit.Text = If(Eng, "Apply Edit", "تطبيق التعديل")
    End Sub

    Private Sub SetApplyCaptionDelete()
        btnDel.Text = If(Eng, "Apply Delete", "تطبيق الحذف")
    End Sub

    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel.Click
        If Not bDeleteMode Then
            Display()
            Delete()
            SetApplyCaptionDelete()
        Else
            DeleteRecord()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Not bEditMode Then
            Display()
            Edit()
            SetApplyCaptionEdit()
        Else
            UpdateRecord()
        End If
    End Sub

    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Me.Close()
    End Sub

    Private Sub butCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butCancel.Click
        ShowToolStripItems("Cancel")
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Not bAddMode Then
            Add()
            SetApplyCaptionAdd()
        Else
            InsertRecord()
        End If
    End Sub


    Private Sub Display()
        ClearRecord()
        Dim clsEmp As New Emp
        clsEmp.EmpID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colEmpID))
        clsEmp = clsEmpData.Select_Record(clsEmp)
        If Not clsEmp Is Nothing Then
            Try
                EmpIDSpinEdit.Value = System.Convert.ToInt32(clsEmp.EmpID)
                EmpNameTextEdit.Text = Convert.ToString(clsEmp.EmpName)
                EmpPhoneTextEdit.Text = Convert.ToString(clsEmp.EmpPhone)
                EmpAddressTextEdit.Text = Convert.ToString(clsEmp.EmpAddress)
                If clsEmp.EmpImg IsNot Nothing Then
                    Using ms As New System.IO.MemoryStream(clsEmp.EmpImg)
                        EmpImgImageEdit.Image = System.Drawing.Image.FromStream(ms)
                    End Using
                End If
                WhatsHelper.BindGenericWhatsPrefixAndLocal(EmpWhatsPrefixCombo, EmpWhatsTextEdit,
                    Convert.ToString(clsEmp.WhatsAppPrefix), Convert.ToString(clsEmp.WhatsApp))
                WhatsHelper.RefreshFullWhatsDigitsLabel(EmpWhatsPrefixCombo, EmpWhatsTextEdit, lblEmpWhats)

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
        EmpIDSpinEdit.Select()
        EmpIDSpinEdit.Text = DentistXDATA.getAutoID("New", "Emp")
    End Sub

    Private Sub Edit()
        Me.AddMode = False
        Me.EditMode = True
        Me.DeleteMode = False
        ShowToolStripItems("Edit")
    End Sub

    Private Sub Delete()
        Me.AddMode = False
        Me.EditMode = False
        Me.DeleteMode = True
        ShowToolStripItems("Delete")
    End Sub

    Private Sub ClearRecord()
        EmpIDSpinEdit.Value = 0
        EmpNameTextEdit.Text = Nothing
        EmpPhoneTextEdit.Text = Nothing
        EmpAddressTextEdit.Text = Nothing
        EmpImgImageEdit.Text = Nothing
        EmpWhatsTextEdit.Text = Nothing
        lblEmpWhats.Text = ""
        WhatsHelper.FillCboPrefixOnce(EmpWhatsPrefixCombo)
        If EmpWhatsPrefixCombo.Properties.Items.Count > 0 Then EmpWhatsPrefixCombo.SelectedIndex = 0
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
    Private Sub SetData(ByVal clsEmp As Emp)
        With clsEmp
            .EmpID = System.Convert.ToInt32(EmpIDSpinEdit.Value)
            .EmpName = System.Convert.ToString(EmpNameTextEdit.Text)
            .EmpPhone = System.Convert.ToString(EmpPhoneTextEdit.Text)
            .EmpAddress = System.Convert.ToString(EmpAddressTextEdit.Text)
            ' Handle image conversion
            If EmpImgImageEdit.Image IsNot Nothing Then
                ' Use ImageToByteArray function to ensure consistent format
                .EmpImg = ImageToByteArray(EmpImgImageEdit.Image)
            Else
                .EmpImg = Nothing ' Or New Byte() {} for empty byte array
            End If
            Dim waP As String = Nothing
            Dim waL As String = Nothing
            WhatsHelper.ReadGenericWhatsFromControls(EmpWhatsPrefixCombo, EmpWhatsTextEdit, waP, waL)
            .WhatsAppPrefix = waP
            .WhatsApp = waL

        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsEmp As New Emp
        If VerifyData() = True Then
            SetData(clsEmp)
            Dim bSuccess As Boolean
            bSuccess = clsEmpData.Add(clsEmp)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub UpdateRecord()
        Dim oclsEmp As New Emp
        Dim clsEmp As New Emp
        oclsEmp.EmpID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colEmpID))
        oclsEmp = clsEmpData.Select_Record(oclsEmp)
        If VerifyData() = True Then
            SetData(clsEmp)
            Dim bSuccess As Boolean
            bSuccess = clsEmpData.Update(oclsEmp, clsEmp)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Sub DeleteRecord()
        Dim clsEmp As New Emp
        clsEmp.EmpID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colEmpID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim bSuccess As Boolean
            bSuccess = clsEmpData.Delete(clsEmp)
            If bSuccess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        ' Check required fields
        If EmpIDSpinEdit.Value = Nothing OrElse EmpIDSpinEdit.Value.ToString() = "" Then
            MsgBox("EmpID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            EmpIDSpinEdit.Select()
            Return False
        End If
        Return True
    End Function

    Private Sub ResetCrudButtonCaptions()
        Dim resources As New ComponentResourceManager(GetType(FrmEmp))
        resources.ApplyResources(btnAdd, btnAdd.Name)
        resources.ApplyResources(btnEdit, btnEdit.Name)
        resources.ApplyResources(btnDel, btnDel.Name)
    End Sub

    Private Sub ShowToolStripItems(ByVal Item As String)
        btnAdd.Enabled = False
        btnEdit.Enabled = False
        btnDel.Enabled = False
        butCancel.Enabled = True

        Select Case Item
            Case "Add"
                EnableRecord(True)
                btnAdd.Enabled = True
            Case "Edit"
                EnableRecord(True)
                btnEdit.Enabled = True
            Case "Delete"
                EnableRecord(False)
                btnDel.Enabled = True
            Case "Cancel"
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    ResetCrudButtonCaptions()
                    AddMode = False
                    EditMode = False
                    DeleteMode = False
                    EnableRecord(False)
                    btnAdd.Enabled = True
                    btnEdit.Enabled = True
                    btnDel.Enabled = True
                    butCancel.Enabled = False
                    Try
                        If Row >= 0 AndAlso Row < dgView.RowCount Then
                            Display()
                        End If
                    Catch
                    End Try
                    DGV.Focus()
                End If
            Case "Refresh"
                ResetCrudButtonCaptions()
                AddMode = False
                EditMode = False
                DeleteMode = False
                btnAdd.Enabled = True
                btnEdit.Enabled = True
                btnDel.Enabled = True
                butCancel.Enabled = False
                LoadDGV()
            Case "No Record"
                ResetCrudButtonCaptions()
                AddMode = False
                EditMode = False
                DeleteMode = False
                btnAdd.Enabled = True
                butCancel.Enabled = False
                EnableRecord(False)
        End Select
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            Dim OFD As New OpenFileDialog
            Dim oldImg As Image = EmpImgImageEdit.Image
            ' Set the filter
            OFD.Filter = Filters
            OFD.FilterIndex = 1 ' Default to first filter option
            OFD.Title = "Select Employee Image"
            OFD.Multiselect = False ' Only allow single file selection
            OFD.CheckFileExists = True
            OFD.CheckPathExists = True
            If OFD.ShowDialog = DialogResult.OK Then
                EmpImgImageEdit.Image = GenerateThumbnail(Image.FromFile(OFD.FileName), 100, 100)
            Else
                EmpImgImageEdit.Image = oldImg
            End If
        Catch ex As Exception
            MsgBox("Error loading image: " & ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

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

    Private Sub EmpWhats_FullNumberRefresh(sender As Object, e As EventArgs) Handles EmpWhatsPrefixCombo.SelectedIndexChanged, EmpWhatsPrefixCombo.EditValueChanged, EmpWhatsTextEdit.EditValueChanged
        WhatsHelper.RefreshFullWhatsDigitsLabel(EmpWhatsPrefixCombo, EmpWhatsTextEdit, lblEmpWhats)
    End Sub

End Class
