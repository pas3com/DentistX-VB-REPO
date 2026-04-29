Imports System
'Imports Microsoft.Office.Interop.PowerPoint '.Presentation
'Imports DocumentFormat.OpenXml.Packaging
Imports System.Drawing
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Threading
Imports DevExpress.XtraEditors
Imports DentistX
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports Microsoft.Office.Core

Public Class ThumbNailViewer
    Implements IPatientAwareUserControl


    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        SyncCurrentPatientFromForm(patientId)
        Me.SuspendLayout()
        LoadPatientData(patientId)
        Me.ResumeLayout()
    End Sub
    Private Sub SyncCurrentPatientFromForm(patientId As Integer)
        Dim ws = PatientAwareHelper.TryGetPatientWorkspace(Me)
        If ws IsNot Nothing AndAlso ws.Current_Patient IsNot Nothing AndAlso ws.Current_Patient.PatientID = patientId Then
            CurrentPatient = ws.Current_Patient
        ElseIf FormManager.Instance.IsBasePatientFormOpen AndAlso FormManager.Instance.GetCurrentPatient() IsNot Nothing AndAlso FormManager.Instance.GetCurrentPatient().PatientID = patientId Then
            CurrentPatient = FormManager.Instance.GetCurrentPatient()
        End If
    End Sub

    Public Sub LoadPatientData(patientId As Integer)
        LoadData(patientId)
    End Sub


    Public Sub New()
        InitializeComponent()
        ' Enable double buffering to reduce flickering
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.UserPaint Or
                 ControlStyles.DoubleBuffer, True)
        UpdateStyles()
    End Sub

    Private _currentPatient As Patient
    Friend Property CurrentPatient As Patient
        Get
            Return _currentPatient
        End Get
        Set(value As Patient)
            _currentPatient = value
        End Set
    End Property

    Private _suppressChkDrive As Boolean

    Private Function GetThumbPatientFolderLabel() As String
        If CurrentPatient Is Nothing Then Return "Patient"
        Dim id = CurrentPatient.PatientID
        If id > 0 Then
            Dim fromDb = PatientDriveMirror.TryGetPatientNameForDriveFolder(id)
            If Not String.IsNullOrWhiteSpace(fromDb) Then Return fromDb
        End If
        Dim n = If(CurrentPatient.PatientName, "").Trim()
        If Not String.IsNullOrWhiteSpace(n) Then Return n
        If id > 0 Then Return "Patient_" & id.ToString()
        Return "Patient"
    End Function

    Private Function ActivePatientIdForImages() As Integer
        If CurrentPatient IsNot Nothing Then Return CurrentPatient.PatientID
        Return 0
    End Function

    Private Sub ChkDrive_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDrive.CheckedChanged
        If _suppressChkDrive Then Return
        If Not ChkDrive.Checked Then Return
        If Not PatientDriveMirror.TryConfirmCloudPatientFiles(Me) Then
            _suppressChkDrive = True
            ChkDrive.Checked = False
            _suppressChkDrive = False
        End If
    End Sub

    Dim str_PresFileOpen As String = ""
    Private Sub ThumbNailViewer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            InitializeEditModeContextMenu()
            ApplyContextMenuLocalization()
            PatientBindingSource.DataSource = CurrentPatient
            'PatientID = Qrs.FrstID
            'LoadData(PatientID)
            ' reduce flickering
            Me.DoubleBuffered = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub InitializeEditModeContextMenu()
        If ContextMenuStrip1 Is Nothing Then Return
        If _tsEditMode IsNot Nothing Then Return

        _tsEditMode = New ToolStripMenuItem()
        _tsEditModeSeparate = New ToolStripMenuItem()
        _tsEditModeSingleCanvas = New ToolStripMenuItem()

        AddHandler _tsEditModeSeparate.Click, AddressOf tsEditModeSeparate_Click
        AddHandler _tsEditModeSingleCanvas.Click, AddressOf tsEditModeSingleCanvas_Click

        _tsEditMode.DropDownItems.Add(_tsEditModeSeparate)
        _tsEditMode.DropDownItems.Add(_tsEditModeSingleCanvas)
        ContextMenuStrip1.Items.Add(New ToolStripSeparator())
        ContextMenuStrip1.Items.Add(_tsEditMode)
        ApplyContextMenuLocalization()
        UpdateEditModeMenuChecks()
    End Sub

    Private Sub ApplyContextMenuLocalization()
        If Eng Then
            tsImageSize.Text = "Thumbnail Size"
            tsX64.Text = "64 x 64"
            tsX128.Text = "128 x 128"
            tsX256.Text = "256 x 256"
            tsDeleteImage.Text = "Delete Selected"
            tsReload.Text = "Reload"

            If _tsEditMode IsNot Nothing Then _tsEditMode.Text = "Edit Mode"
            If _tsEditModeSeparate IsNot Nothing Then _tsEditModeSeparate.Text = "Open each image in separate editor"
            If _tsEditModeSingleCanvas IsNot Nothing Then _tsEditModeSingleCanvas.Text = "Open all selected in one editor (as objects)"
        Else
            tsImageSize.Text = "حجم الصور المصغرة"
            tsX64.Text = "64 x 64"
            tsX128.Text = "128 x 128"
            tsX256.Text = "256 x 256"
            tsDeleteImage.Text = "حذف المحدد"
            tsReload.Text = "إعادة التحميل"

            If _tsEditMode IsNot Nothing Then _tsEditMode.Text = "وضع التحرير"
            If _tsEditModeSeparate IsNot Nothing Then _tsEditModeSeparate.Text = "فتح كل صورة في محرر مستقل"
            If _tsEditModeSingleCanvas IsNot Nothing Then _tsEditModeSingleCanvas.Text = "فتح كل الصور المحددة في محرر واحد (كعناصر)"
        End If
    End Sub

    Private Sub tsEditModeSeparate_Click(sender As Object, e As EventArgs)
        _editOpenMode = BatchEditOpenMode.SeparateEditors
        UpdateEditModeMenuChecks()
    End Sub

    Private Sub tsEditModeSingleCanvas_Click(sender As Object, e As EventArgs)
        _editOpenMode = BatchEditOpenMode.SingleEditorWithAllObjects
        UpdateEditModeMenuChecks()
    End Sub

    Private Sub UpdateEditModeMenuChecks()
        If _tsEditModeSeparate IsNot Nothing Then
            _tsEditModeSeparate.Checked = (_editOpenMode = BatchEditOpenMode.SeparateEditors)
        End If
        If _tsEditModeSingleCanvas IsNot Nothing Then
            _tsEditModeSingleCanvas.Checked = (_editOpenMode = BatchEditOpenMode.SingleEditorWithAllObjects)
        End If
    End Sub

    Public WriteOnly Property ValueFromParent() As Integer
        Set(ByVal Value As Integer)
            LoadData(Value)

        End Set
    End Property

    Dim P As Point
    Dim _Dimension As Integer = 128
    Dim index As Integer = 0
    Dim selectedThumb As String = ""
    Dim BeDuAf As String = "Before"
    Dim appPath As String = Application.StartupPath
    Dim picPath As String = ""
    Private _lastPreviewPath As String = String.Empty
    Private _editOpenMode As BatchEditOpenMode = BatchEditOpenMode.SeparateEditors
    Private _tsEditMode As ToolStripMenuItem = Nothing
    Private _tsEditModeSeparate As ToolStripMenuItem = Nothing
    Private _tsEditModeSingleCanvas As ToolStripMenuItem = Nothing

    Private Enum BatchEditOpenMode
        SeparateEditors = 0
        SingleEditorWithAllObjects = 1
    End Enum

    Private Sub picCanvas_MouseMoveOverImage(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCanvas.MouseMoveOverImage
        P = e.Location
    End Sub


#Region "ThumbViewer"

#Region "ThumbSubFunc"
    Public Sub LoadData(ByVal PatientID As Integer)
        If CurrentPatient Is Nothing Then Exit Sub
        Try
            Me.Hide()
            LabelPatname.Text = "الصور الخاصة بالمريض " & CurrentPatient.PatientName
            LoadPics(BeDuAf)
            'SetThumbs()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Me.Show()
        End Try
    End Sub
    Private Sub SetThumbs()
        ThumbsGrp.Dimension = _Dimension
        ThumbsGrp.Spacing = 10
        Select Case _Dimension
            Case 64
                ThumbsPanel.Width = (_Dimension * 5) + 80
                'ThumbsGrp.ForceRefreshViewer()
            Case 128
                ThumbsPanel.Width = (_Dimension * 4) + 60
                'ThumbsGrp.ForceRefreshViewer()
            Case 256
                ThumbsPanel.Width = (_Dimension * 2) + 60
                'ThumbsGrp.ForceRefreshViewer()
        End Select
    End Sub
    Private Function CheckDir(ByVal PatId As Integer, ByVal Dir As String) As Boolean
        Dim _Path, appPath, picPath As String
        Dim ret As Boolean = False
        _Path = "\Images\Patient_" & PatientID & "\" & Dir & "\"
        appPath = Application.StartupPath
        picPath = appPath & _Path ' "\Images\Patient_" & PatientID & "\Before\")
        ret = My.Computer.FileSystem.DirectoryExists(picPath)
        Return ret
    End Function
    Private Function ImgCount(ByVal PatId As Integer, ByVal Dir As String) As Integer
        'Dim _Path, appPath, picPath As String
        Dim ret As Integer = 0
        '_Path = "\Images\Patient_" & PatientID & "\" & Dir & "\"
        'appPath = Application.StartupPath
        'picPath = Dir ' appPath & _Path ' "\Images\Patient_" & PatientID & "\Before\")
        If My.Computer.FileSystem.DirectoryExists(Dir) = True Then
            ret = My.Computer.FileSystem.GetFiles(Dir).Count
        End If
        Return ret
    End Function
    Public Sub AddPictures()
        Try
            Dim imageIndex As Integer = 0
            Dim files() As String, srcPaths() As String


            If RadioBefor.Checked Then
                picPath = Path.Combine(appPath, $"Images\Patient_{ActivePatientIdForImages()}\Before\")
            ElseIf RadioDur.Checked Then
                picPath = Path.Combine(appPath, $"Images\Patient_{ActivePatientIdForImages()}\During\")
            ElseIf RadioAfter.Checked Then
                picPath = Path.Combine(appPath, $"Images\Patient_{ActivePatientIdForImages()}\After\")
            End If

            Using dlg As New OpenFileDialog With {
            .Multiselect = True,
            .CheckFileExists = True,
            .ShowReadOnly = True,
            .Filter = "All files (*.*)|*.*|Bitmap Files(*.bmp)|*.bmp|JPeg Files(*.jpg)|*.jpg|Gif Files(*.gif)|*.gif",
            .FilterIndex = 3
        }
                If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub

                files = dlg.SafeFileNames
                srcPaths = dlg.FileNames

                Dim fSystem = My.Computer.FileSystem
                If Not fSystem.DirectoryExists(picPath) Then
                    fSystem.CreateDirectory(picPath)
                End If

                ' Determine next image index
                Dim existingFiles = Directory.GetFiles(picPath, $"P-{ActivePatientIdForImages()}-IMG_*")
                Dim indices = existingFiles.Select(Function(fn) ExtractIndex(fn)) _
                           .Where(Function(i) i >= 0) _
                           .ToList()

                If indices.Count > 0 Then imageIndex = indices.Max() + 1

                ' Copy files with new names
                For i = 0 To srcPaths.Length - 1
                    Dim ext = Path.GetExtension(srcPaths(i))
                    Dim newFileName = $"P-{ActivePatientIdForImages()}-IMG_{imageIndex}{ext}"
                    Dim destPath = Path.Combine(picPath, newFileName)
                    fSystem.CopyFile(srcPaths(i), destPath, True)
                    If ChkDrive.Checked Then PatientDriveMirror.TryMirrorStartupRelativeFile(destPath, GetThumbPatientFolderLabel(), Me)
                    imageIndex += 1
                Next

                LoadPics(BeDuAf)
            End Using

        Catch ex As IOException
            MsgBox("File error: " & ex.Message)
        Catch ex As Exception
            MsgBox("Unexpected error: " & ex.Message)
        End Try
    End Sub
    ' Helper function to extract the index number from a file name
    Private Function ExtractIndex(filePath As String) As Integer
        Dim name = Path.GetFileNameWithoutExtension(filePath)
        Dim parts = name.Split("_"c)
        If parts.Length >= 2 Then
            Dim number As Integer
            If Integer.TryParse(parts(parts.Length - 1), number) Then
                Return number
            End If
        End If
        Return -1
    End Function
    Public Sub LoadPics(BeDuAf As String)
        Try
            str_PresFileOpen = ""
            TextFilePath.Text = ""
            index = -1
            _lastPreviewPath = String.Empty
            Me.ThumbsGrp.Clear()
            Me.picCanvas.Image = Nothing ' picCanvas.InitialImage
            If Me.PatientBindingSource.Count = 0 Then Exit Sub

            picPath = Path.Combine(Application.StartupPath, "Images", $"Patient_{CurrentPatient.PatientID}", BeDuAf) ' "Before")

            Me.PicsBSBefore.DataSource = Nothing

            If Not Directory.Exists(picPath) Then
                Exit Sub
            End If

            ' Setup thumbnails group
            Me.ThumbsGrp.Clear()
            Me.ThumbsGrp.Dimension = _Dimension
            Me.PicsBSBefore.DataSource = Directory.GetFiles(picPath)
            Me.ThumbsGrp.DirectoryPath = picPath
            Me.ThumbsGrp.DisplayImages()

        Catch ex As IOException
            MsgBox("Error loading images: " & ex.Message)
        End Try
    End Sub

    Private Function GetSelectedPathsOrFallback() As List(Of String)
        Dim selected As New List(Of String)(ThumbsGrp.SelectedFilePaths)
        If selected.Count = 0 AndAlso Not String.IsNullOrWhiteSpace(str_PresFileOpen) AndAlso IO.File.Exists(str_PresFileOpen) Then
            selected.Add(str_PresFileOpen)
        End If
        Return selected
    End Function

    Private Sub SyncSelectionUiFromThumbs()
        Dim selected = New List(Of String)(ThumbsGrp.SelectedFilePaths)
        If selected.Count = 0 Then
            index = -1
            str_PresFileOpen = ""
            TextFilePath.Text = ""
            _lastPreviewPath = String.Empty
            If picCanvas.Image IsNot Nothing Then
                picCanvas.Image.Dispose()
                picCanvas.Image = Nothing
                picCanvas.Invalidate()
            End If
            Return
        End If

        Dim firstPath = selected(0)
        Dim firstIndex = -1
        If ThumbsGrp.SelectedIndices.Count > 0 Then
            firstIndex = ThumbsGrp.SelectedIndices(0)
        End If

        str_PresFileOpen = firstPath
        index = firstIndex
        TextFilePath.Text = If(selected.Count = 1, firstPath, selected.Count.ToString() & " selected")

        If String.Equals(_lastPreviewPath, firstPath, StringComparison.OrdinalIgnoreCase) Then Return
        LoadPreviewImage(firstPath, firstIndex)
    End Sub

    Private Sub LoadPreviewImage(imagePath As String, selectedIndex As Integer)
        Dim loadedImage As Bitmap = Nothing
        Try
            loadedImage = ModuleImages.LoadImageAtMaxSize(imagePath, 1024)
            If loadedImage Is Nothing Then
                MsgBox("Image could not be loaded. The file may be in use, too large, or invalid.",
                       MsgBoxStyle.Exclamation, "Image")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("Image could not be loaded. The file may be in use, too large, or invalid." & Environment.NewLine & ex.Message,
                   MsgBoxStyle.Exclamation, "Image")
            Exit Sub
        End Try

        Try
            If Me.picCanvas.Image IsNot Nothing Then
                Me.picCanvas.Image.Dispose()
                Me.picCanvas.Image = Nothing
            End If
            Me.picCanvas.Image = loadedImage
            Me.picCanvas.Invalidate()
            str_PresFileOpen = imagePath
            index = selectedIndex
            _lastPreviewPath = imagePath
            selectedThumb = ThumbsGrp.Name
        Catch ex As Exception
            If loadedImage IsNot Nothing Then loadedImage.Dispose()
            MsgBox("Image could not be displayed.", MsgBoxStyle.Exclamation, "Image")
        End Try
    End Sub

    Private Sub DeleteSelectedImagesBatch()
        Dim selected = GetSelectedPathsOrFallback()
        If selected.Count = 0 Then Exit Sub

        Dim prompt As String = If(selected.Count = 1,
                                  "Delete selected image?",
                                  "Delete " & selected.Count.ToString() & " selected images?")
        If MessageBox.Show(prompt, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Exit Sub
        End If

        Me.picCanvas.Image = Nothing
        ThumbsGrp.DeleteImages(selected)
        LoadPics(BeDuAf)
        index = -1
        str_PresFileOpen = ""
        TextFilePath.Text = ""
    End Sub

    Private Sub ExportSelectedImagesBatch(selected As List(Of String))
        If selected Is Nothing OrElse selected.Count = 0 Then Exit Sub

        FolderBrwsrDlg.SelectedPath = picPath
        If FolderBrwsrDlg.ShowDialog() <> DialogResult.OK Then Exit Sub
        Dim targetDir = FolderBrwsrDlg.SelectedPath
        If String.IsNullOrWhiteSpace(targetDir) OrElse Not IO.Directory.Exists(targetDir) Then Exit Sub

        Dim exportedCount As Integer = 0
        For Each src In selected
            If Not IO.File.Exists(src) Then Continue For
            Try
                Dim baseName = IO.Path.GetFileNameWithoutExtension(src)
                Dim ext = IO.Path.GetExtension(src)
                Dim dest = IO.Path.Combine(targetDir, baseName & ext)
                Dim suffix As Integer = 1
                While IO.File.Exists(dest)
                    dest = IO.Path.Combine(targetDir, baseName & "_" & suffix.ToString() & ext)
                    suffix += 1
                End While
                IO.File.Copy(src, dest, False)
                exportedCount += 1
                If ChkDrive.Checked Then PatientDriveMirror.TryMirrorStartupRelativeFile(dest, GetThumbPatientFolderLabel(), Me)
            Catch
                ' skip file-level export errors and continue
            End Try
        Next

        MessageBox.Show(exportedCount.ToString() & " image(s) exported.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region

#Region "Thumb Buttons"
    Private Sub ThumbsGrp_PictureSelected(sender As Object, e As ThumbGalleryViewer.PictureSelectedEventArgs) Handles ThumbsGrp.PictureSelected
        SyncSelectionUiFromThumbs()
    End Sub

    Private Sub ThumbsGrp_PicturesSelectionChanged(sender As Object, e As ThumbGalleryViewer.PicturesSelectionChangedEventArgs) Handles ThumbsGrp.PicturesSelectionChanged
        SyncSelectionUiFromThumbs()
    End Sub
    Private Sub BtnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDel.Click
        Try
            DeleteSelectedImagesBatch()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btLoadPics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btLoadPics.Click
        Try
            If Me.PatientBindingSource.Count = 0 Then
                Exit Sub
            Else
                Me.PicsBSBefore.DataSource = Nothing
                If Me.RadioBefor.Checked = True Then
                    LoadPics("Before")
                ElseIf Me.RadioDur.Checked = True Then
                    LoadPics("During")
                ElseIf Me.RadioAfter.Checked = True Then
                    LoadPics("After")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub RadioBefor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioBefor.CheckedChanged
        If Me.RadioBefor.Checked = True Then
            If Eng Then
                Me.btAddPics.Text = "Add Before IMGS"
            Else
                Me.btAddPics.Text = "إضافة صور قبل"
            End If
            BeDuAf = "Before"
            LoadPics("Before")
            'SetThumbs()
        End If
    End Sub
    Private Sub RadioDur_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioDur.CheckedChanged
        If Me.RadioDur.Checked = True Then
            If Eng Then
                Me.btAddPics.Text = "Add During IMGS"
            Else
                Me.btAddPics.Text = "إضافة صور أثناء"
            End If
            BeDuAf = "During"
            LoadPics("During")
            'SetThumbs()
        End If
    End Sub
    Private Sub RadioAfter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioAfter.CheckedChanged
        If Me.RadioAfter.Checked = True Then
            If Eng Then
                Me.btAddPics.Text = "Add After IMGS"
            Else
                Me.btAddPics.Text = "إضافة صور بعد"
            End If
            BeDuAf = "After"
            LoadPics("After")
            'SetThumbs()
        End If
    End Sub
    Private Sub btAddPics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddPics.Click
        Try
            If Me.PatientBindingSource.Count = 0 Then
                Exit Sub
            Else
                If Me.RadioBefor.Checked = True Then
                    AddPictures()
                ElseIf Me.RadioDur.Checked = True Then
                    AddPictures()
                ElseIf Me.RadioAfter.Checked = True Then
                    AddPictures()
                End If
                'Dim PicName, PicPath, PicDes As String
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function ThumbNailAbort() As Boolean
        'Do Nothing Here
        Return ThumbNailAbort
    End Function

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        FBDlg.RootFolder = Environment.SpecialFolder.System
        FBDlg.SelectedPath = TextFilePath.Text
        If FBDlg.ShowDialog() = DialogResult.OK Then
            InsertImageFolderIntoPowerPoint(FBDlg.SelectedPath)
        End If
        '("C:\Users\You\Pictures\MySlides")
    End Sub

    Private Sub btnImgInPpt_Click(sender As Object, e As EventArgs) Handles btnImgInPpt.Click
        InsertSingleImageIntoPowerPoint(picCanvas.Image)
    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        Try
            Dim objPPT As PowerPoint.Application
            Dim objPres As PowerPoint.Presentation
            objPPT = New PowerPoint.Application
            objPPT.Visible = MsoTriState.msoTrue
            objPPT.WindowState = PowerPoint.PpWindowState.ppWindowMaximized
            'Add Presentation
            objPres = objPPT.Presentations.Add(MsoTriState.msoTrue)
            Dim objSlide As PowerPoint.Slide
            Dim objCustomLayout As PowerPoint.CustomLayout
            'Create a custom layout based on the first layout in the slide master.
            'This is used simply for creating the slide
            objCustomLayout = objPres.SlideMaster.CustomLayouts.Item(1)
            'Create slide
            objSlide = objPres.Slides.AddSlide(1, objCustomLayout)
            'Set the layout
            objSlide.Layout = PowerPoint.PpSlideLayout.ppLayoutBlank
            'objSlide.Shapes.Title.Delete()
            'objSlide.Shapes.AddPicture(str_PresFileOpen, False, True, 150, 150, 500, 350)
            'Clean up
            objCustomLayout.Delete()
            objCustomLayout = Nothing
            objSlide = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub InsertSingleImageIntoPowerPoint(img As Image)
        Try
            ' Save image to temp path
            Dim tempPath As String = IO.Path.Combine(IO.Path.GetTempPath(), "tempPPTImage.jpg")
            img.Save(tempPath, Imaging.ImageFormat.Jpeg)

            ' Create PowerPoint application and presentation
            Dim pptApp As New PowerPoint.Application
            Dim pptPres As PowerPoint.Presentation = pptApp.Presentations.Add(MsoTriState.msoTrue)
            pptApp.Visible = MsoTriState.msoTrue
            pptApp.WindowState = PowerPoint.PpWindowState.ppWindowMaximized

            ' Add slide
            Dim slide As PowerPoint.Slide = pptPres.Slides.Add(1, PowerPoint.PpSlideLayout.ppLayoutBlank)
            slide.Shapes.AddPicture(tempPath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 50, 50, 640, 480)

            ' Cleanup
            IO.File.Delete(tempPath)

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Public Sub InsertImageFolderIntoPowerPoint(folderPath As String)
        Try
            If Not IO.Directory.Exists(folderPath) Then
                MsgBox("Folder not found.")
                Exit Sub
            End If

            ' Get image files
            Dim imageFiles = IO.Directory.GetFiles(folderPath, "*.*").
                         Where(Function(f) f.ToLower.EndsWith(".jpg") OrElse f.ToLower.EndsWith(".png") OrElse f.ToLower.EndsWith(".bmp")).ToArray()

            If imageFiles.Length = 0 Then
                MsgBox("No image files found in folder.")
                Exit Sub
            End If

            ' Create PowerPoint app
            Dim pptApp As New PowerPoint.Application
            Dim pptPres As PowerPoint.Presentation = pptApp.Presentations.Add(MsoTriState.msoTrue)
            pptApp.Visible = MsoTriState.msoTrue
            pptApp.WindowState = PowerPoint.PpWindowState.ppWindowMaximized

            ' Add each image as a slide
            For i As Integer = 0 To imageFiles.Length - 1
                Dim slide As PowerPoint.Slide = pptPres.Slides.Add(i + 1, PowerPoint.PpSlideLayout.ppLayoutBlank)
                slide.Shapes.AddPicture(imageFiles(i), MsoTriState.msoFalse, MsoTriState.msoCTrue, 50, 50, 640, 480)
            Next

            'MsgBox("Inserted " & imageFiles.Length & " images into PowerPoint.")

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub



    'PPT Addin
    'Imports System.IO



    'Public Sub SendImagesToPowerPoint(imagesFolder As String)
    '    If Not Directory.Exists(Path.GetDirectoryName(sharedFilePath)) Then
    '        Directory.CreateDirectory(Path.GetDirectoryName(sharedFilePath))
    '    End If
    '    File.WriteAllText(sharedFilePath, imagesFolder)

    '    ' Launch PowerPoint
    '    Dim pptApp As New PowerPoint.Application
    '    pptApp.Visible = Microsoft.Office.Core.MsoTriState.msoTrue
    '    pptApp.Presentations.Add() ' Triggers ThisAddIn_Startup
    'End Sub

    'Public Sub SendImagesToPowerPoint1(imagesFolder As String)
    '    ''Dim sharedFile As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DentistX\ppt_instruction.txt")
    '    'If Not Directory.Exists(Path.GetDirectoryName(sharedFilePath)) Then
    '    '    Directory.CreateDirectory(Path.GetDirectoryName(sharedFilePath))
    '    'End If
    '    'File.WriteAllText(sharedFilePath, imagesFolder)


    '    ' Save the folder to shared file
    '    If Not Directory.Exists(Path.GetDirectoryName(sharedFilePath)) Then
    '        Directory.CreateDirectory(Path.GetDirectoryName(sharedFilePath))
    '    End If
    '    File.WriteAllText(sharedFilePath, imagesFolder)

    '    ' Start PowerPoint to trigger add-in
    '    Dim pptApp As New Microsoft.Office.Interop.PowerPoint.Application
    '    pptApp.Visible = Microsoft.Office.Core.MsoTriState.msoTrue
    '    pptApp.Presentations.Add() ' Add empty presentation to trigger Startup
    'End Sub




#End Region

#Region "ThumbContext"
    Private Sub tsDeleteImage_Click(sender As Object, e As EventArgs) Handles tsDeleteImage.Click
        Try
            DeleteSelectedImagesBatch()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub tsX256_Click(sender As Object, e As EventArgs) Handles tsX256.Click
        _Dimension = 256
        SetThumbs()
    End Sub
    Private Sub tsX128_Click(sender As Object, e As EventArgs) Handles tsX128.Click
        _Dimension = 128
        SetThumbs()
    End Sub
    Private Sub tsX64_Click(sender As Object, e As EventArgs) Handles tsX64.Click
        _Dimension = 64
        SetThumbs()
    End Sub
    Private Sub tsReload_Click(sender As Object, e As EventArgs) Handles tsReload.Click
        ThumbsGrp.RefreshViewer()
    End Sub
    Private Sub ThumbsGrp_Click(sender As Object, e As EventArgs) Handles ThumbsGrp.Click
        SyncSelectionUiFromThumbs()
    End Sub

#End Region

#End Region


    Private Sub btnSaveImage_Click(sender As Object, e As EventArgs) Handles btnSaveImage.Click

        If RadioBefor.Checked Then
            picPath = Path.Combine(appPath, $"Images\Patient_{ActivePatientIdForImages()}\Before\")
        ElseIf RadioDur.Checked Then
            picPath = Path.Combine(appPath, $"Images\Patient_{ActivePatientIdForImages()}\During\")
        ElseIf RadioAfter.Checked Then
            picPath = Path.Combine(appPath, $"Images\Patient_{ActivePatientIdForImages()}\After\")
        End If

        Dim selected = New List(Of String)(ThumbsGrp.SelectedFilePaths)
        If selected.Count > 1 Then
            ExportSelectedImagesBatch(selected)
            Return
        End If

        sfdImage.InitialDirectory = picPath
        If sfdImage.ShowDialog() = DialogResult.OK Then
            Dim bm As Bitmap = Nothing
            Try

                Dim srcImage As Image = picCanvas.Image
                If srcImage Is Nothing Then
                    MessageBox.Show("No image to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                bm = New Bitmap(srcImage.Width, srcImage.Height)
                Using gr As Graphics = Graphics.FromImage(bm)
                    gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    gr.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                    gr.DrawImage(srcImage, 0, 0, bm.Width, bm.Height)
                End Using

                Dim filename As String = sfdImage.FileName

                Dim ext As String = IO.Path.GetExtension(filename).ToLower()

                Select Case ext
                    Case ".bmp"
                        bm.Save(filename, ImageFormat.Bmp)
                    Case ".gif"
                        bm.Save(filename, ImageFormat.Gif)
                    Case ".jpg", ".jpeg"
                        Dim quality As Long = 100L
                        Using encoderParams As New EncoderParameters(1)
                            encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, quality)
                            Dim jpegCodec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(Function(c) c.FormatID = ImageFormat.Jpeg.Guid)
                            If jpegCodec IsNot Nothing Then
                                bm.Save(filename, jpegCodec, encoderParams)
                            Else
                                bm.Save(filename, ImageFormat.Jpeg)
                            End If
                        End Using
                    Case ".png"
                        bm.Save(filename, ImageFormat.Png)
                    Case ".tif", ".tiff"
                        bm.Save(filename, ImageFormat.Tiff)
                    Case Else
                        MessageBox.Show("Unknown or unsupported file extension: " & ext, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                End Select

                If ChkDrive.Checked Then PatientDriveMirror.TryMirrorStartupRelativeFile(filename, GetThumbPatientFolderLabel(), Me)
                MessageBox.Show("Image saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show("Error saving image: " & ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If bm IsNot Nothing Then bm.Dispose()
            End Try
        End If
    End Sub
    Public Function PictureBoxZoom(ByVal img As Image, ByVal size As Size) As Image
        Dim w = Convert.ToInt32(img.Width * size.Width)
        Dim h = Convert.ToInt32(img.Height * size.Height)
        Return New Bitmap(img, w, h)
    End Function
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            Dim selected = GetSelectedPathsOrFallback()
            If selected.Count = 0 Then
                MsgBox("There Is No Image Selected!!")
                Exit Sub
            End If

            If selected.Count > 1 AndAlso _editOpenMode = BatchEditOpenMode.SingleEditorWithAllObjects Then
                OpenSelectedInSingleDrawForm(selected)
                Exit Sub
            End If

            For i = 0 To selected.Count - 1
                Dim selectedPath = selected(i)
                If Not IO.File.Exists(selectedPath) Then Continue For

                Dim F As New DrawForm(selectedPath)
                F.ShowDialog()
                If F.DialogResult = DialogResult.OK AndAlso F.SavedImage IsNot Nothing Then
                    Dim newPic As Image = F.SavedImage
                    Dim newPath = F.SavedFilePath
                    If picCanvas.Image IsNot Nothing Then picCanvas.Image.Dispose()
                    If Not String.IsNullOrWhiteSpace(newPath) Then
                        LoadPics(BeDuAf)
                        str_PresFileOpen = newPath
                        TextFilePath.Text = newPath
                        If ChkDrive.Checked Then PatientDriveMirror.TryMirrorStartupRelativeFile(newPath, GetThumbPatientFolderLabel(), Me)
                    End If
                    picCanvas.Image = newPic
                    picCanvas.Invalidate()
                End If
            Next
        Catch ex As Exception
            MsgBox("Could not open editor: " & ex.Message)
        End Try
    End Sub

    Private Sub OpenSelectedInSingleDrawForm(selectedPaths As List(Of String))
        If selectedPaths Is Nothing OrElse selectedPaths.Count = 0 Then Exit Sub

        Dim F As New DrawForm()
        Dim loaded = F.LoadImagesAsObjects(selectedPaths, True)
        If loaded <= 0 Then
            F.Dispose()
            MsgBox("Could not load selected images into editor.")
            Exit Sub
        End If

        F.ShowDialog()
        If F.DialogResult = DialogResult.OK AndAlso F.SavedImage IsNot Nothing Then
            Dim newPic As Image = F.SavedImage
            Dim newPath = F.SavedFilePath
            If picCanvas.Image IsNot Nothing Then picCanvas.Image.Dispose()
            If Not String.IsNullOrWhiteSpace(newPath) Then
                LoadPics(BeDuAf)
                str_PresFileOpen = newPath
                TextFilePath.Text = newPath
                If ChkDrive.Checked Then PatientDriveMirror.TryMirrorStartupRelativeFile(newPath, GetThumbPatientFolderLabel(), Me)
            End If
            picCanvas.Image = newPic
            picCanvas.Invalidate()
        End If
    End Sub
    Private Sub btnCrop_Click(sender As Object, e As EventArgs) Handles btnCrop.Click
        Try
            If Not IO.File.Exists(str_PresFileOpen) Then
                MsgBox("There Is No Image Selected!!")
                Exit Sub
            End If
            Dim img1 As Bitmap = Nothing
            Try
                ' Use the original, full-resolution image for cropping
                Using fs As New IO.FileStream(str_PresFileOpen, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
                    Using original As Image = Image.FromStream(fs)
                        img1 = New Bitmap(original)
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Image could not be loaded (file in use, too large, or invalid)." & Environment.NewLine & ex.Message,
                       MsgBoxStyle.Exclamation, "Image")
                Exit Sub
            End Try
            Dim F As New FrmCrop(img1, str_PresFileOpen)
            F.ShowDialog()
            If F.DialogResult = DialogResult.OK AndAlso F.CroppedImage IsNot Nothing Then
                Dim newPic As Image = F.CroppedImage
                Dim newPath = F.SavedFilePath
                If picCanvas.Image IsNot Nothing Then picCanvas.Image.Dispose()
                If Not String.IsNullOrWhiteSpace(newPath) Then
                    LoadPics(BeDuAf)
                    str_PresFileOpen = newPath
                    TextFilePath.Text = newPath
                    If ChkDrive.Checked Then PatientDriveMirror.TryMirrorStartupRelativeFile(newPath, GetThumbPatientFolderLabel(), Me)
                End If
                picCanvas.Image = newPic
                picCanvas.Invalidate()
            End If
            img1.Dispose()
        Catch ex As Exception
            MsgBox("Could not open crop: " & ex.Message)
        End Try
    End Sub

    'Private Sub btnDicomViewer_Click(sender As Object, e As EventArgs) Handles btnDicomViewer.Click
    '    Try
    '        Dim exePath As String = GetDicomViewerExePath()
    '        If String.IsNullOrEmpty(exePath) Then
    '            MsgBox("DICOM Viewer (DicomImageViewer.exe) not found. Build the DicomImageViewer project (x64) first.")
    '            Return
    '        End If
    '        Dim pathToLoad As String = Nothing
    '        If Not String.IsNullOrWhiteSpace(TextFilePath.Text) AndAlso
    '           (IO.File.Exists(TextFilePath.Text) OrElse IO.Directory.Exists(TextFilePath.Text)) Then
    '            pathToLoad = TextFilePath.Text
    '        ElseIf Not String.IsNullOrWhiteSpace(picPath) AndAlso IO.Directory.Exists(picPath) Then
    '            pathToLoad = picPath
    '        End If
    '        Dim lang As String = GetLangForDicomViewer()
    '        Dim args As New List(Of String)
    '        If Not String.IsNullOrWhiteSpace(pathToLoad) Then
    '            args.Add("""" & pathToLoad.Replace("""", """""") & """")
    '        End If
    '        If CurrentPatient IsNot Nothing Then
    '            args.Add("--patient")
    '            args.Add(CurrentPatient.PatientID.ToString())
    '            args.Add("--phase")
    '            args.Add(BeDuAf)
    '            If Not String.IsNullOrWhiteSpace(CurrentPatient.PatientName) Then
    '                args.Add("--patientname")
    '                args.Add("""" & CurrentPatient.PatientName.Replace("""", """""") & """")
    '            End If
    '        End If
    '        args.Add("--lang")
    '        args.Add(lang)
    '        Dim psi As New ProcessStartInfo(exePath) With {
    '            .Arguments = String.Join(" ", args),
    '            .UseShellExecute = True
    '        }
    '        Process.Start(psi)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    ''' <summary>
    ''' Resolves path to DicomImageViewer.exe.
    ''' First tries the installed path (C:\Pascal\DicomViewer\DicomImageViewer.exe),
    ''' then falls back to common bin folders relative to the solution root.
    ''' </summary>
    Private Shared Function GetDicomViewerExePath() As String
        ' 1) Preferred installed location on this machine
        Dim installedPath As String = "C:\Pascal\DicomViewer\DicomImageViewer.exe"
        If File.Exists(installedPath) Then
            Return installedPath
        End If

        ' 2) Fallback: relative to solution root for dev environments
        Dim baseDir As String = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "..", ".."))
        Dim candidates() As String = {
            Path.Combine(baseDir, "DicomImageViewer", "DicomImageViewer", "bin", "x64", "Debug", "DicomImageViewer.exe"),
            Path.Combine(baseDir, "DicomImageViewer", "DicomImageViewer", "bin", "x64", "Release", "DicomImageViewer.exe"),
            Path.Combine(baseDir, "DicomImageViewer", "DicomImageViewer", "bin", "Debug", "DicomImageViewer.exe"),
            Path.Combine(baseDir, "DicomImageViewer", "DicomImageViewer", "bin", "Release", "DicomImageViewer.exe")
        }
        For Each p As String In candidates
            If File.Exists(p) Then Return p
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Returns "ar" or "en" for the DICOM viewer. Uses My.Settings.Lang if present, else current UI culture.
    ''' </summary>
    Private Shared Function GetLangForDicomViewer() As String
        Dim lang As String = "en"
        Try
            Dim prop = My.Settings.GetType().GetProperty("Lang")
            If prop IsNot Nothing Then
                Dim v = prop.GetValue(My.Settings)
                If v IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(CStr(v)) Then
                    lang = CStr(v).Trim().ToLowerInvariant()
                End If
            End If
        Catch
        End Try
        If String.IsNullOrWhiteSpace(lang) OrElse (lang <> "ar" AndAlso lang <> "en") Then
            lang = If(Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName = "ar", "ar", "en")
        End If
        Return If(lang = "ar", "ar", "en")
    End Function



    Private Sub btnOpenEditor_Click(sender As Object, e As EventArgs) Handles btnOpenEditor.Click
        Try
            Dim F As New DrawForm()
            F.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnShowImages_Click(sender As Object, e As EventArgs) Handles btnShowImages.Click
        FrmOrthoImages.ShowForPatient(ActivePatientIdForImages())
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.A) Then
            ThumbsGrp.SelectAll()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub btnDicomViewer_Click(sender As Object, e As EventArgs) Handles btnDicomViewer.Click
        Try
            Dim viewer As New DicomViewerFrm()
            If Not String.IsNullOrWhiteSpace(TextFilePath.Text) AndAlso
               (IO.File.Exists(TextFilePath.Text) OrElse IO.Directory.Exists(TextFilePath.Text)) Then
                viewer.LoadPath(TextFilePath.Text)
            ElseIf IO.Directory.Exists(picPath) Then
                viewer.LoadPath(picPath)
            End If
            viewer.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub





    '#Region "Cropping"
    '    Dim P As Point
    '    Dim isSelected As Boolean = False
    '    Dim cropX As Integer
    '    Dim cropY As Integer
    '    Dim cropWidth As Integer
    '    Dim cropHeight As Integer
    '    Dim oCropX As Integer
    '    Dim oCropY As Integer
    '    Dim cropBitmap As Bitmap
    '    Dim Cropping As Boolean = False
    '    Dim cropPen As Pen
    '    Dim cropPenSize As Integer = 2 '2
    '    Dim cropDashStyle As Drawing2D.DashStyle = Drawing2D.DashStyle.Solid
    '    Dim cropPenColor As Color = Color.Red
    '    Private Sub picCanvas_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picCanvas.Paint

    '    End Sub

    '    Private Sub picCanvas_MouseDown(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseDown
    '        Try
    '            picCanvas.Image = picCanvas.Image
    '            picCanvas.Visible = True
    '            cropX = e.X
    '            cropY = e.Y
    '            cropPen = New Pen(cropPenColor, cropPenSize)
    '            cropPen.DashStyle = DashStyle.DashDotDot
    '            Cursor = Cursors.Cross
    '            picCanvas.Refresh()
    '        Catch exc As Exception
    '        End Try
    '    End Sub
    '    Private Sub picCanvas_MouseMove(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseMove
    '        If e.Button = System.Windows.Forms.MouseButtons.Left Then
    '            Try
    '                Cropping = True
    '                If picCanvas.Image Is Nothing Then Exit Try
    '                picCanvas.Refresh()
    '                cropWidth = e.X - cropX
    '                cropHeight = e.Y - cropY
    '                'Dim g As Graphics = Graphics.FromImage(picCanvas.Image)
    '                picCanvas.CreateGraphics.DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight)
    '                ' GC.Collect()
    '            Catch exc As Exception
    '                If Err.Number = 5 Then Exit Sub
    '            End Try
    '        End If
    '    End Sub
    '    Private Sub picCanvas_MouseUp(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseUp
    '        Try
    '            If cropWidth < 1 Then
    '                Exit Sub
    '            End If
    '            Dim rect As Rectangle = New Rectangle(cropX, cropY, cropWidth, cropHeight)
    '            'First we define a rectangle with the help of already calculated points
    '            Dim OriginalImage As Bitmap = New Bitmap(picCanvas.Image, picCanvas.Width, picCanvas.Height)
    '            'Original image
    '            Dim _img As New Bitmap(cropWidth, cropHeight) ' for cropinf image
    '            Dim g As Graphics = Graphics.FromImage(_img) ' create graphics
    '            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
    '            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
    '            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
    '            'set image attributes
    '            g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel)
    '            picCanvas.Image = _img
    '            'm_Picture = New DrawablePictureN(_img)
    '            cropBitmap = _img
    '            Cropping = False
    '        Catch ex As Exception
    '        End Try
    '        Cursor = Cursors.Default
    '    End Sub


    '#End Region



    '#Region "Old Code"
    'Public Sub LoadBefPics()
    '    Try
    '        str_PresFileOpen = ""
    '        TextFilePath.Text = ""
    '        index = -1
    '        Me.ThumbsGrp.Clear()

    '        If Me.PatientBindingSource.Count = 0 Then Exit Sub

    '        Dim picPath As String = Path.Combine(Application.StartupPath, "Images", $"Patient_{PatientID}", "Before")

    '        Me.PicsBSBefore.DataSource = Nothing

    '        If Not Directory.Exists(picPath) Then
    '            Exit Sub
    '        End If

    '        ' Setup thumbnails group
    '        Me.ThumbsGrp.Clear()
    '        Me.ThumbsGrp.Dimension = _Dimension
    '        Me.PicsBSBefore.DataSource = Directory.GetFiles(picPath)
    '        Me.ThumbsGrp.DirectoryPath = picPath
    '        Me.ThumbsGrp.DisplayImages()

    '    Catch ex As IOException
    '        MsgBox("Error loading images: " & ex.Message)
    '    End Try
    'End Sub
    'Public Sub AddDurPics()
    '    Try
    '        Dim x As Integer = 0
    '        SetThumbs()
    '        Dim files(), srcPath() As String
    '        Dim file, appPath, picPath As String
    '        Dim OpFDlg As New OpenFileDialog
    '        With OpFDlg
    '            .Multiselect = True
    '            .FilterIndex = 2
    '            .CheckFileExists = True
    '            .ShowReadOnly = True
    '            '"Text files (*.txt)|*.txt|All files (*.*)|*.*"
    '            .Filter = "All files (*.*)|*.*|Bitmap Files(*.bmp)|*.bmp|JPeg Files(*.jpg)|*.jpg|Gif Files(*.gif)|*.gif"
    '            .FilterIndex = 3
    '            If .ShowDialog = DialogResult.OK Then
    '                ' Load the specified files into a Temp Folder .
    '                files = .SafeFileNames
    '                srcPath = .FileNames
    '                'PicName = .SafeFileName
    '                'PicPath = .FileName
    '                Dim Pathchars() As Char
    '                'nam = PicName
    '                'PicPath = PicPath.TrimEnd(nam)
    '                Pathchars = My.Computer.FileSystem.CurrentDirectory & "\"
    '                appPath = Application.StartupPath
    '                picPath = appPath & "\Images\Patient_" & PatientID & "\During\"
    '                'Me.StatBar.Panels.Item("Bal").Text = picPath
    '                With My.Computer.FileSystem
    '                    .CurrentDirectory = Application.StartupPath
    '                    If .DirectoryExists(picPath) = False Then
    '                        .CreateDirectory(picPath)
    '                        Dim i As Integer = 0
    '                        For Each path1 In srcPath
    '                            file = files(i)
    '                            Dim fi As New IO.FileInfo(file)
    '                            Dim extn As String = fi.Extension
    '                            Dim fname As String = IO.Path.GetFileNameWithoutExtension(file)
    '                            fname = "P-" & PatientID & "-IMG_" & x & extn
    '                            .CopyFile(path1, picPath & fname, True)
    '                            '.CopyFile(path1, picPath & file, True)
    '                            i += 1
    '                            x += 1
    '                        Next
    '                        LoadDurPics()
    '                    Else
    '                        Dim i As Integer = 0
    '                        Dim flist() As IO.FileInfo = Nothing
    '                        Dim xs As New List(Of Integer)
    '                        Dim d As New IO.DirectoryInfo(picPath)
    '                        For Each path1 In srcPath
    '                            flist = d.GetFiles("P-" & PatientID & "-IMG_*")
    '                        Next
    '                        Dim s As New List(Of String)
    '                        If flist.Length > 0 Then

    '                            For j = 0 To flist.Count - 1
    '                                s.Add(GetSubStr(flist(j).Name, "_", 2))

    '                            Next
    '                        End If
    '                        For k = 0 To s.Count - 1
    '                            xs.Add(CInt(Val(DigitsOnly(s(k)))))
    '                        Next

    '                        If xs.Count > 0 Then
    '                            x = xs.Max
    '                        End If
    '                        flist = Nothing
    '                        xs = Nothing
    '                        s = Nothing
    '                        x = x + 1
    '                        For Each path1 In srcPath
    '                            file = files(i)
    '                            Dim fi As New IO.FileInfo(file)
    '                            Dim extn As String = fi.Extension
    '                            Dim fname As String = IO.Path.GetFileNameWithoutExtension(file)
    '                            fname = "P-" & PatientID & "-IMG_" & x & extn
    '                            .CopyFile(path1, picPath & fname, True)
    '                            '.CopyFile(path1, picPath & file, True)
    '                            i += 1
    '                            x += 1
    '                        Next
    '                        LoadDurPics()

    '                    End If
    '                    ' .CopyFile(srcPath, "..\..\Temp\" & files)
    '                End With


    '            Else
    '                Exit Sub
    '            End If
    '        End With
    '        ' PicDes = DescTxt.Value
    '    Catch ex As System.IO.IOException
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Public Sub LoadDurPics()
    '    Try
    '        str_PresFileOpen = ""
    '        'picCanvas.Image = picCanvas.InitialImage
    '        TextFilePath.Text = ""
    '        index = -1
    '        Me.ThumbsGrp.Clear()

    '        Dim _Path, appPath, picPath As String
    '        Me.PicsBSDuring.DataSource = Nothing
    '        _Path = "\Images\Patient_" & PatientID & "\During\"
    '        appPath = Application.StartupPath
    '        picPath = appPath & _Path
    '        If My.Computer.FileSystem.DirectoryExists(picPath) = False Then

    '            Exit Sub
    '        Else
    '            'Me.StatBar.Panels.Item("Bal").Text = picPath
    '            If picPath.Trim.Length <> 0 Then
    '                Me.ThumbsGrp.Clear()
    '                Me.ThumbsGrp.Dimension = _Dimension
    '                Me.ThumbsGrp.DirectoryPath = picPath
    '                Me.ThumbsGrp.DisplayImages()
    '            Else
    '                If Eng Then
    '                    MessageBox.Show("Please Browse Folder")
    '                Else
    '                    MessageBox.Show("الرجاء استعرض مجلدا")
    '                End If
    '            End If
    '        End If

    '    Catch ex As System.IO.IOException
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Public Sub AddAfterPics()
    '    Try
    '        Dim x As Integer = 0
    '        SetThumbs()
    '        Dim files(), srcPath() As String
    '        Dim file, appPath, picPath As String
    '        Dim OpFDlg As New OpenFileDialog
    '        With OpFDlg
    '            .Multiselect = True
    '            .FilterIndex = 2
    '            .CheckFileExists = True
    '            .ShowReadOnly = True
    '            '"Text files (*.txt)|*.txt|All files (*.*)|*.*"
    '            .Filter = "All files (*.*)|*.*|Bitmap Files(*.bmp)|*.bmp|JPeg Files(*.jpg)|*.jpg|Gif Files(*.gif)|*.gif"
    '            .FilterIndex = 3
    '            If .ShowDialog = DialogResult.OK Then
    '                ' Load the specified files into a Temp Folder .
    '                files = .SafeFileNames
    '                srcPath = .FileNames
    '                'PicName = .SafeFileName
    '                'PicPath = .FileName
    '                Dim Pathchars() As Char
    '                'nam = PicName
    '                'PicPath = PicPath.TrimEnd(nam)
    '                Pathchars = My.Computer.FileSystem.CurrentDirectory & "\"
    '                appPath = Application.StartupPath
    '                picPath = appPath & "\Images\Patient_" & PatientID & "\After\"
    '                'Me.StatBar.Panels.Item("Bal").Text = picPath
    '                With My.Computer.FileSystem
    '                    .CurrentDirectory = Application.StartupPath
    '                    If .DirectoryExists(picPath) = False Then
    '                        .CreateDirectory(picPath)
    '                        Dim i As Integer = 0
    '                        For Each path1 In srcPath
    '                            file = files(i)
    '                            Dim fi As New IO.FileInfo(file)
    '                            Dim extn As String = fi.Extension
    '                            Dim fname As String = IO.Path.GetFileNameWithoutExtension(file)
    '                            fname = "P-" & PatientID & "-IMG_" & x & extn
    '                            .CopyFile(path1, picPath & fname, True)
    '                            '.CopyFile(path1, picPath & file, True)
    '                            i += 1
    '                            x += 1
    '                        Next
    '                        LoadAftPics()
    '                    Else
    '                        Dim i As Integer = 0
    '                        Dim flist() As IO.FileInfo = Nothing
    '                        Dim xs As New List(Of Integer)
    '                        Dim d As New IO.DirectoryInfo(picPath)
    '                        For Each path1 In srcPath
    '                            flist = d.GetFiles("P-" & PatientID & "-IMG_*")
    '                        Next
    '                        Dim s As New List(Of String)
    '                        If flist.Length > 0 Then

    '                            For j = 0 To flist.Count - 1
    '                                s.Add(GetSubStr(flist(j).Name, "_", 2))

    '                            Next
    '                        End If
    '                        For k = 0 To s.Count - 1
    '                            xs.Add(CInt(Val(DigitsOnly(s(k)))))
    '                        Next

    '                        If xs.Count > 0 Then
    '                            x = xs.Max
    '                        End If
    '                        flist = Nothing
    '                        xs = Nothing
    '                        s = Nothing
    '                        x = x + 1
    '                        For Each path1 In srcPath
    '                            file = files(i)
    '                            Dim fi As New IO.FileInfo(file)
    '                            Dim extn As String = fi.Extension
    '                            Dim fname As String = IO.Path.GetFileNameWithoutExtension(file)
    '                            fname = "P-" & PatientID & "-IMG_" & x & extn
    '                            .CopyFile(path1, picPath & fname, True)
    '                            '.CopyFile(path1, picPath & file, True)
    '                            i += 1
    '                            x += 1
    '                        Next
    '                        LoadAftPics()

    '                    End If
    '                    ' .CopyFile(srcPath, "..\..\Temp\" & files)
    '                End With


    '            Else
    '                Exit Sub
    '            End If
    '        End With
    '        ' PicDes = DescTxt.Value
    '    Catch ex As System.IO.IOException
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Public Sub LoadAftPics()
    '    Try
    '        str_PresFileOpen = ""
    '        'picCanvas.Image = picCanvas.InitialImage
    '        TextFilePath.Text = ""
    '        index = -1
    '        Me.ThumbsGrp.Clear()
    '        'Dim row As DentistX.DsImgNote.PatientRow
    '        'If Me.PatientBindingSource.Count = 0 Then
    '        '    Exit Sub
    '        'Else
    '        '    row = CType(CType(Me.PatientBindingSource.Current, DataRowView).Row, DentistX.DsImgNote.PatientRow)
    '        '    PatientID = row.PatientID
    '        '    Me.PicsBSAfter.DataSource = Nothing
    '        'End If
    '        Dim _Path, appPath, picPath As String

    '        _Path = "\Images\Patient_" & PatientID & "\After\"
    '        appPath = Application.StartupPath
    '        picPath = appPath & "\Images\Patient_" & PatientID & "\After\"

    '        If My.Computer.FileSystem.DirectoryExists(picPath) = False Then

    '            Exit Sub
    '        Else
    '            'Me.StatBar.Panels.Item("Bal").Text = picPath
    '            If picPath.Trim.Length <> 0 Then
    '                Me.ThumbsGrp.Clear()
    '                Me.ThumbsGrp.Dimension = _Dimension
    '                Me.PicsBSAfter.DataSource = My.Computer.FileSystem.GetFiles(picPath)
    '                Me.ThumbsGrp.DirectoryPath = picPath
    '                Me.ThumbsGrp.DisplayImages()



    '            Else
    '                If Eng Then
    '                    MessageBox.Show("Please Browse Folder")
    '                Else
    '                    MessageBox.Show("الرجاء استعرض مجلدا")
    '                End If
    '            End If
    '        End If

    '    Catch ex As System.IO.IOException
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    '    Private Sub PicsBSBefore_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PicsBSBefore.PositionChanged
    '        'If Me.PicsBSBefore.Count = 0 Then
    '        '    Me.PicMain.Image = Me.PicMain.InitialImage
    '        'Else
    '        '    Me.PicMain.Image = Image.FromFile(Me.PicsBSBefore.Current.ToString)
    '        '    str_PresFileOpen = Me.PicsBSBefore.Current.ToString
    '        '    TextBox1.Text = str_PresFileOpen
    '        'End If

    '    End Sub


    '    Public Sub AddBeforePicsOld()
    '        Try
    '            Dim x As Integer = 0

    '            Dim files(), srcPath() As String
    '            Dim file, path1, appPath, picPath As String 'picPath1, picPath2
    '            Dim OpFDlg As New OpenFileDialog

    '            appPath = Application.StartupPath
    '            picPath = "" 'My.Computer.FileSystem.CombinePath(appPath, "\Images\Patient_" & PatientID & "\Before\")
    '            SetThumbs()

    '            If Me.RadioBefor.Checked = True Then
    '                picPath = appPath & "\Images\Patient_" & PatientID & "\Before\"
    '            ElseIf Me.RadioDur.Checked = True Then
    '                picPath = appPath & "\Images\Patient_" & PatientID & "\During\"
    '            ElseIf Me.RadioAfter.Checked = True Then
    '                picPath = appPath & "\Images\Patient_" & PatientID & "\After\"
    '            End If


    '            With OpFDlg
    '                .Multiselect = True
    '                .CheckFileExists = True
    '                .ShowReadOnly = True
    '                .Filter = "All files (*.*)|*.*|Bitmap Files(*.bmp)|*.bmp|JPeg Files(*.jpg)|*.jpg|Gif Files(*.gif)|*.gif" '"Text files (*.txt)|*.txt|All files (*.*)|*.*"
    '                .FilterIndex = 3
    '                If .ShowDialog = DialogResult.OK Then
    '                    ' Load the specified files into a Temp Folder .
    '                    files = .SafeFileNames
    '                    srcPath = .FileNames

    '                    With My.Computer.FileSystem
    '                        .CurrentDirectory = Application.StartupPath
    '                        If .DirectoryExists(picPath) = False Then
    '                            .CreateDirectory(picPath)
    '                            Dim i As Integer = 0
    '                            For Each path1 In srcPath
    '                                file = files(i)
    '                                Dim fi As New IO.FileInfo(file)
    '                                Dim extn As String = fi.Extension
    '                                Dim fname As String = IO.Path.GetFileNameWithoutExtension(file)
    '                                fname = "P-" & PatientID & "-IMG_" & x & extn
    '                                .CopyFile(path1, picPath & fname, True)
    '                                '.CopyFile(path1, picPath & file, True)
    '                                i += 1
    '                                x += 1
    '                            Next
    '                            LoadBefPics()
    '                        Else
    '                            Dim i As Integer = 0
    '                            Dim flist() As IO.FileInfo = Nothing
    '                            Dim xs As New List(Of Integer)
    '                            Dim d As New IO.DirectoryInfo(picPath)
    '                            For Each path1 In srcPath
    '                                flist = d.GetFiles("P-" & PatientID & "-IMG_*")
    '                            Next
    '                            Dim s As New List(Of String)
    '                            If flist.Length > 0 Then

    '                                For j = 0 To flist.Count - 1
    '                                    s.Add(GetSubStr(flist(j).Name, "_", 2))

    '                                Next
    '                            End If
    '                            For k = 0 To s.Count - 1
    '                                xs.Add(CInt(Val(DigitsOnly(s(k)))))
    '                            Next

    '                            If xs.Count > 0 Then
    '                                x = xs.Max
    '                            End If
    '                            flist = Nothing
    '                            xs = Nothing
    '                            s = Nothing
    '                            x = x + 1
    '                            For Each path1 In srcPath
    '                                file = files(i)
    '                                Dim fi As New IO.FileInfo(file)
    '                                Dim extn As String = fi.Extension
    '                                Dim fname As String = IO.Path.GetFileNameWithoutExtension(file)
    '                                fname = "P-" & PatientID & "-IMG_" & x & extn
    '                                .CopyFile(path1, picPath & fname, True)
    '                                '.CopyFile(path1, picPath & file, True)
    '                                i += 1
    '                                x += 1
    '                            Next
    '                            LoadBefPics()

    '                        End If
    '                        ' .CopyFile(srcPath, "..\..\Temp\" & files)
    '                    End With


    '                Else
    '                    Exit Sub
    '                End If
    '            End With
    '            ' PicDes = DescTxt.Value
    '        Catch ex As System.IO.IOException
    '            MsgBox(ex.Message)
    '        End Try
    '    End Sub

    '    Public Sub LoadBefPicsOld()
    '        Try
    '            str_PresFileOpen = ""
    '            'picCanvas.Image = picCanvas.InitialImage
    '            TextFilePath.Text = ""
    '            index = -1
    '            Me.ThumbsGrp.Clear()
    '            If Me.PatientBindingSource.Count = 0 Then
    '                Exit Sub
    '            Else

    '            End If
    '            Dim _Path, appPath, picPath As String
    '            Me.PicsBSBefore.DataSource = Nothing
    '            _Path = "\Images\Patient_" & PatientID & "\Before\"
    '            appPath = Application.StartupPath
    '            picPath = appPath & _Path ' "\Images\Patient_" & PatientID & "\Before\")
    '            If My.Computer.FileSystem.DirectoryExists(picPath) = False Then

    '                Exit Sub
    '            Else
    '                'Me.StatBar.Panels.Item("Bal").Text = picPath
    '                If picPath.Trim.Length <> 0 Then
    '                    Me.ThumbsGrp.Clear()
    '                    Me.ThumbsGrp.Dimension = _Dimension

    '                    Me.PicsBSBefore.DataSource = My.Computer.FileSystem.GetFiles(picPath)
    '                    Me.ThumbsGrp.DirectoryPath = picPath
    '                    Me.ThumbsGrp.DisplayImages()
    '                    'picCanvas.Image = img
    '                Else
    '                    If Eng Then
    '                        MessageBox.Show("Please Browse Folder")
    '                    Else
    '                        MessageBox.Show("الرجاء استعرض مجلدا")
    '                    End If

    '                End If
    '            End If
    '        Catch ex As System.IO.IOException
    '            MsgBox(ex.Message)
    '        End Try
    '    End Sub


    '#Region "Zoom"

    '    'Private _originalSize As Size = Nothing
    '    'Private _scale As Single = 1
    '    'Private _scaleDelta As Single = 0.0003
    '    'Private _ratWidth, _ratHeight As Double

    '    Private Sub picCanvas_MouseWheel(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles picCanvas.MouseWheel

    '        ''if very sensitive mouse, change 0.00005 to something even smaller   
    '        '_scaleDelta = Math.Sqrt(picCanvas.Width * picCanvas.Height) * 0.00005

    '        'If e.Delta < 0 Then
    '        '    _scale -= _scaleDelta
    '        'ElseIf e.Delta > 0 Then
    '        '    _scale += _scaleDelta
    '        'End If

    '        'If e.Delta <> 0 Then
    '        '    'picCanvas.Size = New Size(CInt(Math.Round((_originalSize.Width * _ratWidth) * _scale)),
    '        '    '                    CInt(Math.Round((_originalSize.Height * _ratHeight) * _scale)))

    '        '    picCanvas.Size = New Size(CInt(Math.Round(_originalSize.Width * _scale)),
    '        '                           CInt(Math.Round(_originalSize.Height * _scale)))
    '        'End If
    '    End Sub


    '#End Region


    '#End Region



End Class
