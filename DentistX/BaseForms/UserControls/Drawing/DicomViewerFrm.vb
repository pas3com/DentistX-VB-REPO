Imports System.IO
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports FellowOakDicom
Imports FellowOakDicom.Imaging
Imports FellowOakDicom.Imaging.NativeCodec
Public Class DicomViewerFrm



    Private ReadOnly playTimer As Timer
    Private ReadOnly frameTimer As Timer
    Private fileList As List(Of String) = New List(Of String)()
    Private currentIndex As Integer = -1
    Private playDirection As Integer = 1
    Private currentFrameIndex As Integer = 0
    Private currentFrameCount As Integer = 1
    Private isDicomCurrent As Boolean = False
    Private currentDicomFile As String = ""
    Private seriesMap As New Dictionary(Of String, List(Of String))()
    Private fileCache As New Dictionary(Of String, DicomFile)(StringComparer.OrdinalIgnoreCase)
    Private suppressWlEvents As Boolean = False
    Private userWlOverride As Boolean = False
    Private hasWindowLevelInDataset As Boolean = False

    Private Shared _dicomInitialized As Boolean = False
    Private Shared _nativeCodecAvailable As Boolean = False

    Public Sub New()
        InitializeComponent()
        'MyBase.New()
        Text = "DICOM Viewer"
        Width = 1368
        Height = 770
        StartPosition = FormStartPosition.CenterParent



        ofd.Filter = "DICOM files (*.dcm)|*.dcm|Images (*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff)|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff|All files (*.*)|*"
        ofd.CheckFileExists = True



        sfdExport.Filter = "PNG (*.png)|*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Bitmap (*.bmp)|*.bmp|TIFF (*.tif;*.tiff)|*.tif;*.tiff"



        AddHandler btnOpenFile.Click, AddressOf HandleOpenFile
        AddHandler btnOpenFolder.Click, AddressOf HandleOpenFolder
        AddHandler btnPrev.Click, AddressOf HandlePrev
        AddHandler btnNext.Click, AddressOf HandleNext
        AddHandler btnPlay.Click, AddressOf HandlePlayToggle
        AddHandler lstFiles.SelectedIndexChanged, AddressOf HandleSelectFile
        AddHandler speedTrack.EditValueChanged, AddressOf HandleSpeedChanged
        AddHandler frameTrack.EditValueChanged, AddressOf HandleFrameChanged
        AddHandler btnFramePlay.Click, AddressOf HandleFramePlayToggle
        AddHandler windowTrack.EditValueChanged, AddressOf HandleWindowLevelChanged
        AddHandler levelTrack.EditValueChanged, AddressOf HandleWindowLevelChanged
        AddHandler btnWlReset.Click, AddressOf HandleWindowLevelReset
        AddHandler lstSeries.SelectedIndexChanged, AddressOf HandleSeriesSelected
        AddHandler btnExportFrame.Click, AddressOf HandleExportFrame
        AddHandler btnExportSeries.Click, AddressOf HandleExportSeries

        playTimer = New Timer() With {
        .Interval = Convert.ToInt32(speedTrack.EditValue)
    }
        AddHandler playTimer.Tick, AddressOf HandlePlayTick

        frameTimer = New Timer() With {
        .Interval = 300
    }
        AddHandler frameTimer.Tick, AddressOf HandleFrameTick

        ' Configure window/level sliders for typical DICOM ranges
        ' Window width: positive range of displayed intensities
        windowTrack.Properties.Minimum = 1
        windowTrack.Properties.Maximum = 4096
        windowTrack.EditValue = 1200

        ' Window center: can be negative (e.g. CT Hounsfield Units)
        levelTrack.Properties.Minimum = -1024
        levelTrack.Properties.Maximum = 3071
        levelTrack.EditValue = 0
    End Sub

    Public Sub LoadPath(path As String)
        If String.IsNullOrWhiteSpace(path) Then Return

        If System.IO.File.Exists(path) Then
            Dim folder = System.IO.Path.GetDirectoryName(path)
            If Not String.IsNullOrWhiteSpace(folder) Then
                LoadFolder(folder, path)
            Else
                LoadSingleFile(path)
            End If
            Return
        End If

        If Directory.Exists(path) Then
            LoadFolder(path, Nothing)
        End If
    End Sub

    Private Sub HandleOpenFile(sender As Object, e As EventArgs)
        If ofd.ShowDialog(Me) = DialogResult.OK Then
            LoadPath(ofd.FileName)
        End If
    End Sub

    Private Sub HandleOpenFolder(sender As Object, e As EventArgs)
        If fbd.ShowDialog(Me) = DialogResult.OK Then
            LoadFolder(fbd.SelectedPath, Nothing)
        End If
    End Sub

    Private Sub HandlePrev(sender As Object, e As EventArgs)
        If fileList.Count = 0 Then Return
        If currentIndex <= 0 Then Return
        ShowFileAt(currentIndex - 1)
    End Sub

    Private Sub HandleNext(sender As Object, e As EventArgs)
        If fileList.Count = 0 Then Return
        If currentIndex >= fileList.Count - 1 Then Return
        ShowFileAt(currentIndex + 1)
    End Sub

    Private Sub HandlePlayToggle(sender As Object, e As EventArgs)
        If fileList.Count = 0 Then Return
        If playTimer.Enabled Then
            playTimer.Stop()
            btnPlay.Text = "Play"
            Return
        End If

        If isDicomCurrent Then
            userWlOverride = True
        End If
        playDirection = 1
        playTimer.Start()
        btnPlay.Text = "Stop"
    End Sub

    Private Sub HandlePlayTick(sender As Object, e As EventArgs)
        If fileList.Count = 0 Then
            HandlePlayToggle(Nothing, EventArgs.Empty)
            Return
        End If

        Dim nextIndex = currentIndex + playDirection
        If chkBounce.Checked Then
            If nextIndex >= fileList.Count Then
                playDirection = -1
                nextIndex = Math.Max(0, fileList.Count - 2)
            ElseIf nextIndex < 0 Then
                playDirection = 1
                nextIndex = Math.Min(fileList.Count - 1, 1)
            End If
        ElseIf nextIndex >= fileList.Count Then
            nextIndex = 0
        ElseIf nextIndex < 0 Then
            nextIndex = fileList.Count - 1
        End If
        ShowFileAt(nextIndex)
    End Sub

    Private Sub HandleSpeedChanged(sender As Object, e As EventArgs)
        Dim value = Convert.ToInt32(speedTrack.EditValue)
        If value < 100 Then value = 100
        playTimer.Interval = value
    End Sub

    Private Sub HandleFrameChanged(sender As Object, e As EventArgs)
        If Not isDicomCurrent Then Return
        Dim idx = Convert.ToInt32(frameTrack.EditValue)
        If idx = currentFrameIndex Then Return
        currentFrameIndex = idx
        RenderCurrentFrame()
    End Sub

    Private Sub HandleFramePlayToggle(sender As Object, e As EventArgs)
        If Not isDicomCurrent OrElse currentFrameCount <= 1 Then Return
        If frameTimer.Enabled Then
            frameTimer.Stop()
            btnFramePlay.Text = "Play"
            Return
        End If
        frameTimer.Start()
        btnFramePlay.Text = "Stop"
    End Sub

    Private Sub HandleFrameTick(sender As Object, e As EventArgs)
        If Not isDicomCurrent OrElse currentFrameCount <= 1 Then
            If frameTimer.Enabled Then
                frameTimer.Stop()
                btnFramePlay.Text = "Play"
            End If
            Return
        End If
        Dim nextIndex = currentFrameIndex + 1
        If nextIndex >= currentFrameCount Then nextIndex = 0
        currentFrameIndex = nextIndex
        frameTrack.EditValue = currentFrameIndex
        RenderCurrentFrame()
    End Sub

    Private Sub HandleWindowLevelChanged(sender As Object, e As EventArgs)
        If Not isDicomCurrent Then Return
        If suppressWlEvents Then Return
        userWlOverride = True
        RenderCurrentFrame()
    End Sub

    Private Sub HandleWindowLevelReset(sender As Object, e As EventArgs)
        If Not isDicomCurrent Then Return
        userWlOverride = False
        ApplyWindowLevelFromDataset()
        RenderCurrentFrame()
    End Sub

    Private Sub HandleSeriesSelected(sender As Object, e As EventArgs)
        Dim idx = lstSeries.SelectedIndex
        If idx < 0 Then Return
        Dim key = TryCast(lstSeries.Items(idx), String)
        If String.IsNullOrWhiteSpace(key) Then Return
        If Not seriesMap.ContainsKey(key) Then Return
        fileList = seriesMap(key)
        lstFiles.Items.Clear()
        For Each f In fileList
            lstFiles.Items.Add(System.IO.Path.GetFileName(f))
        Next
        If fileList.Count > 0 Then
            lstFiles.SelectedIndex = 0
            ShowFileAt(0)
        End If
    End Sub

    Private Sub HandleExportFrame(sender As Object, e As EventArgs)
        If currentIndex < 0 Then Return
        If sfdExport.ShowDialog(Me) <> DialogResult.OK Then Return
        Dim img = GetCurrentRenderedImage()
        If img Is Nothing Then Return
        SaveImageToPath(img, sfdExport.FileName)
        img.Dispose()
    End Sub

    Private Sub HandleExportSeries(sender As Object, e As EventArgs)
        If currentIndex < 0 Then Return
        If fbdExport.ShowDialog(Me) <> DialogResult.OK Then Return
        Dim outDir = fbdExport.SelectedPath
        Directory.CreateDirectory(outDir)

        Dim files = If(fileList IsNot Nothing, fileList, New List(Of String)())
        For Each f In files
            Dim df = GetDicomFile(f)
            If df Is Nothing Then Continue For
            Dim dataset = df.Dataset
            Dim pixelData = DicomPixelData.Create(dataset, False)
            Dim frames = Math.Max(1, pixelData.NumberOfFrames)
            For i As Integer = 0 To frames - 1
                Dim dicomImage = New DicomImage(dataset)
                ApplyWindowLevelToImage(dicomImage)
                Dim bmp = dicomImage.RenderImage(i).AsClonedBitmap()
                Dim name = $"{System.IO.Path.GetFileNameWithoutExtension(f)}_f{i + 1:000}.png"
                Dim path = System.IO.Path.Combine(outDir, name)
                bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png)
                bmp.Dispose()
            Next
        Next
    End Sub

    Private Sub HandleSelectFile(sender As Object, e As EventArgs)
        Dim idx = lstFiles.SelectedIndex
        If idx < 0 OrElse idx >= fileList.Count Then Return
        ShowFileAt(idx)
    End Sub

    Private Sub LoadFolder(folderPath As String, Optional selectedFile As String = Nothing)
        fileList = GetImageFiles(folderPath)
        lstFiles.Items.Clear()
        If playTimer.Enabled Then
            playTimer.Stop()
            btnPlay.Text = "Play"
        End If
        If frameTimer.Enabled Then
            frameTimer.Stop()
            btnFramePlay.Text = "Play"
        End If
        userWlOverride = False

        BuildSeriesList(folderPath)

        For Each f In fileList
            lstFiles.Items.Add(Path.GetFileName(f))
        Next

        If fileList.Count = 0 Then
            UpdateStatus("No images found in folder.")
            ClearImage()
            Return
        End If

        Dim idx = 0
        If Not String.IsNullOrWhiteSpace(selectedFile) Then
            Dim match = fileList.FindIndex(Function(p) String.Equals(p, selectedFile, StringComparison.OrdinalIgnoreCase))
            If match >= 0 Then idx = match
        End If

        lstFiles.SelectedIndex = idx
        ShowFileAt(idx)
    End Sub

    Private Sub LoadSingleFile(path As String)
        fileList = New List(Of String) From {path}
        lstFiles.Items.Clear()
        If playTimer.Enabled Then
            playTimer.Stop()
            btnPlay.Text = "Play"
        End If
        If frameTimer.Enabled Then
            frameTimer.Stop()
            btnFramePlay.Text = "Play"
        End If
        userWlOverride = False
        ClearSeriesList()
        lstFiles.Items.Add(System.IO.Path.GetFileName(path))
        lstFiles.SelectedIndex = 0
        ShowFileAt(0)
    End Sub

    Private Sub ShowFileAt(index As Integer)
        If index < 0 OrElse index >= fileList.Count Then Return
        currentIndex = index
        Dim path = fileList(index)
        currentDicomFile = path

        Dim img As Image = Nothing
        Dim err As String = ""
        If Not TryLoadImageFromPath(path, img, err) Then
            UpdateStatus("Load failed: " & err)
            ClearImage()
            Return
        End If

        SetImage(img)
        UpdateStatus($"{System.IO.Path.GetFileName(path)} ({currentIndex + 1}/{fileList.Count})")
        UpdateMetadataAndControls(path)
    End Sub

    Private Sub UpdateStatus(text As String)
        lblStatus.Text = text
    End Sub

    Private Sub ClearImage()
        If picView.Image IsNot Nothing Then
            Dim old = picView.Image
            picView.Image = Nothing
            old.Dispose()
        End If
    End Sub

    Private Sub SetImage(img As Image)
        ClearImage()
        picView.Image = img
    End Sub

    Private Shared Function GetImageFiles(folderPath As String) As List(Of String)
        Dim files = New List(Of String)()
        Try
            For Each file In Directory.GetFiles(folderPath)
                Dim ext = Path.GetExtension(file).ToLowerInvariant()
                If ext = ".dcm" OrElse ext = ".bmp" OrElse ext = ".jpg" OrElse ext = ".jpeg" OrElse
               ext = ".png" OrElse ext = ".gif" OrElse ext = ".tif" OrElse ext = ".tiff" OrElse
               IsDicomFile(file) Then
                    files.Add(file)
                End If
            Next
        Catch
        End Try
        Return files
    End Function

    Private Sub BuildSeriesList(folderPath As String)
        seriesMap.Clear()
        lstSeries.Items.Clear()

        Dim dicomFiles = Directory.GetFiles(folderPath).
        Where(Function(f) IsDicomFile(f)).
        ToList()

        If dicomFiles.Count = 0 Then
            lstSeries.Items.Add("All Images")
            lstSeries.SelectedIndex = 0
            Return
        End If

        For Each f In dicomFiles
            Dim df = GetDicomFile(f)
            If df Is Nothing Then Continue For
            Dim dataset = df.Dataset
            Dim seriesUid = SafeGetString(dataset, DicomTag.SeriesInstanceUID, "UnknownSeries")
            Dim seriesDesc = SafeGetString(dataset, DicomTag.SeriesDescription, seriesUid)
            Dim key = $"{seriesDesc} ({seriesUid})"
            If Not seriesMap.ContainsKey(key) Then
                seriesMap(key) = New List(Of String)()
            End If
            seriesMap(key).Add(f)
        Next

        For Each key In seriesMap.Keys.OrderBy(Function(k) k)
            lstSeries.Items.Add(key)
        Next

        If lstSeries.Items.Count > 0 Then
            lstSeries.SelectedIndex = 0
        End If
    End Sub

    Private Sub ClearSeriesList()
        seriesMap.Clear()
        lstSeries.Items.Clear()
    End Sub

    Private Sub UpdateMetadataAndControls(path As String)
        If IsDicomFile(path) Then
            isDicomCurrent = True
            Dim df = GetDicomFile(path)
            If df IsNot Nothing Then
                UpdateFrameControls(df.Dataset)
                If Not userWlOverride Then
                    ApplyWindowLevelFromDataset()
                End If
                UpdateMetadata(df.Dataset)
                UpdateOverlayIndicators(df.Dataset)
                RenderCurrentFrame()
            End If
        Else
            isDicomCurrent = False
            currentFrameCount = 1
            currentFrameIndex = 0
            frameTrack.Properties.Maximum = 0
            frameTrack.EditValue = 0
            memoMeta.Text = "Non-DICOM image."
            lblOverlay.Text = "Overlay: N/A"
            lblBurnedIn.Text = "Burned-in: N/A"
        End If
        UpdateUiForDicom(isDicomCurrent)
    End Sub

    Private Sub UpdateUiForDicom(isDicom As Boolean)
        frameTrack.Enabled = isDicom AndAlso currentFrameCount > 1
        btnFramePlay.Enabled = isDicom AndAlso currentFrameCount > 1
        If btnFramePlay.Enabled Then
            btnFramePlay.ToolTip = "Play/stop frames"
        ElseIf isDicom Then
            btnFramePlay.ToolTip = "Disabled: single-frame DICOM"
        Else
            btnFramePlay.ToolTip = "Disabled: not a DICOM image"
        End If
        windowTrack.Enabled = isDicom
        levelTrack.Enabled = isDicom
        btnWlReset.Enabled = isDicom
        lstSeries.Enabled = seriesMap.Count > 0
        memoMeta.Enabled = isDicom
        lblOverlay.Enabled = isDicom
        lblBurnedIn.Enabled = isDicom
        btnExportSeries.Enabled = isDicom AndAlso seriesMap.Count > 0
    End Sub

    Private Sub UpdateFrameControls(dataset As DicomDataset)
        Dim pixelData = DicomPixelData.Create(dataset, False)
        currentFrameCount = Math.Max(1, pixelData.NumberOfFrames)
        currentFrameIndex = Math.Min(currentFrameIndex, currentFrameCount - 1)
        frameTrack.Properties.Maximum = Math.Max(0, currentFrameCount - 1)
        frameTrack.EditValue = currentFrameIndex
        lblFrame.Text = $"Frame: {currentFrameIndex + 1}/{currentFrameCount}"
    End Sub

    Private Sub ApplyWindowLevelFromDataset()
        Dim df = GetDicomFile(currentDicomFile)
        If df Is Nothing Then Return
        Dim dataset = df.Dataset
        hasWindowLevelInDataset = dataset.Contains(DicomTag.WindowWidth) AndAlso dataset.Contains(DicomTag.WindowCenter)

        Dim ww As Double = GetFirstNumericValue(dataset, DicomTag.WindowWidth, 0)
        Dim wc As Double = GetFirstNumericValue(dataset, DicomTag.WindowCenter, 0)

        ' To avoid error: Both WW and WC must be non-zero and positive
        If (ww <= 0 OrElse wc = 0) Then
            Try
                Dim dicomImage = New DicomImage(dataset)
                ' RenderImage may throw, so wrap in try-catch.
                dicomImage.RenderImage(0)

                If ww <= 0 Then
                    If dicomImage.WindowWidth > 0 Then
                        ww = dicomImage.WindowWidth
                    Else
                        ww = 1200
                    End If
                End If
                If wc = 0 Then
                    wc = dicomImage.WindowCenter
                    If wc = 0 Then wc = ww / 2
                End If
            Catch
                ' Fallback defaults to avoid exceptions if loading/rendering fails
                ww = 1200
                wc = 600
            End Try
        End If

        If ww <= 0 Then ww = 1200
        If wc = 0 Then wc = ww / 2

        SetWindowLevel(ww, wc, False)
    End Sub

    Private Sub SetWindowLevel(windowWidth As Double, windowCenter As Double, markOverride As Boolean)
        Dim ww = Math.Min(windowTrack.Properties.Maximum, Math.Max(windowTrack.Properties.Minimum, windowWidth))
        Dim wc = Math.Min(levelTrack.Properties.Maximum, Math.Max(levelTrack.Properties.Minimum, windowCenter))
        suppressWlEvents = True
        windowTrack.EditValue = CInt(ww)
        levelTrack.EditValue = CInt(wc)
        suppressWlEvents = False
        If markOverride Then
            userWlOverride = True
        End If
    End Sub

    Private Sub RenderCurrentFrame()
        If String.IsNullOrWhiteSpace(currentDicomFile) Then Return
        Dim img = GetCurrentRenderedImage()
        If img Is Nothing Then Return
        SetImage(img)
        lblFrame.Text = $"Frame: {currentFrameIndex + 1}/{currentFrameCount}"
    End Sub

    Private Function GetCurrentRenderedImage() As Image
        If Not IsDicomFile(currentDicomFile) Then
            Return If(picView.Image IsNot Nothing, CType(picView.Image.Clone(), Image), Nothing)
        End If

        Dim df = GetDicomFile(currentDicomFile)
        If df Is Nothing Then Return Nothing
        Dim dicomImage = New DicomImage(df.Dataset)
        ApplyWindowLevelToImage(dicomImage)
        Dim bmp = dicomImage.RenderImage(currentFrameIndex).AsClonedBitmap()
        Return bmp
    End Function

    Private Sub ApplyWindowLevelToImage(dicomImage As DicomImage)
        Dim ww = Convert.ToDouble(windowTrack.EditValue)
        Dim wc = Convert.ToDouble(levelTrack.EditValue)
        Try
            dicomImage.WindowWidth = ww
            dicomImage.WindowCenter = wc
        Catch
        End Try
    End Sub

    Private Sub UpdateMetadata(dataset As DicomDataset)
        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine($"Patient: {SafeGetString(dataset, DicomTag.PatientName, "Unknown")}")
        sb.AppendLine($"Study Date: {SafeGetString(dataset, DicomTag.StudyDate, "Unknown")}")
        sb.AppendLine($"Modality: {SafeGetString(dataset, DicomTag.Modality, "Unknown")}")
        sb.AppendLine($"Series: {SafeGetString(dataset, DicomTag.SeriesDescription, "Unknown")}")
        sb.AppendLine($"Instance: {SafeGetString(dataset, DicomTag.SOPInstanceUID, "Unknown")}")
        sb.AppendLine($"Image Type: {SafeGetString(dataset, DicomTag.ImageType, "Unknown")}")
        sb.AppendLine($"Rows x Cols: {GetFirstNumericValue(dataset, DicomTag.Rows, 0)} x {GetFirstNumericValue(dataset, DicomTag.Columns, 0)}")
        sb.AppendLine($"Bits Stored: {GetFirstNumericValue(dataset, DicomTag.BitsStored, 0)}")
        memoMeta.Text = sb.ToString()
    End Sub

    Private Sub UpdateOverlayIndicators(dataset As DicomDataset)
        Dim hasOverlay = dataset.Any(Function(i) i.Tag.Group >= &H6000 AndAlso i.Tag.Group <= &H60FF AndAlso i.Tag.Element = &H3000)
        Dim burned = SafeGetString(dataset, DicomTag.BurnedInAnnotation, "Unknown")
        lblOverlay.Text = $"Overlay: {If(hasOverlay, "Yes", "No")}"
        lblBurnedIn.Text = $"Burned-in: {burned}"
    End Sub

    Private Function GetDicomFile(path As String) As DicomFile
        If String.IsNullOrWhiteSpace(path) Then Return Nothing
        If fileCache.ContainsKey(path) Then Return fileCache(path)
        Try
            EnsureDicomInitialized()
            Dim df = DicomFile.Open(path)
            fileCache(path) = df
            Return df
        Catch
            Return Nothing
        End Try
    End Function

    Private Shared Function SafeGetString(dataset As DicomDataset, tag As DicomTag, fallback As String) As String
        Try
            Dim value As String = ""
            If dataset.TryGetSingleValue(tag, value) Then
                If Not String.IsNullOrWhiteSpace(value) Then Return value
            End If
        Catch
        End Try
        Return fallback
    End Function

    Private Shared Function GetFirstNumericValue(dataset As DicomDataset, tag As DicomTag, fallback As Double) As Double
        Try
            Dim values As Double() = Nothing
            If dataset.TryGetValues(tag, values) AndAlso values IsNot Nothing AndAlso values.Length > 0 Then
                Return values(0)
            End If
        Catch
        End Try
        Return fallback
    End Function

    Private Shared Sub SaveImageToPath(img As Image, path As String)
        Dim ext = System.IO.Path.GetExtension(path).ToLowerInvariant()
        Select Case ext
            Case ".jpg", ".jpeg"
                img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg)
            Case ".bmp"
                img.Save(path, System.Drawing.Imaging.ImageFormat.Bmp)
            Case ".tif", ".tiff"
                img.Save(path, System.Drawing.Imaging.ImageFormat.Tiff)
            Case Else
                img.Save(path, System.Drawing.Imaging.ImageFormat.Png)
        End Select
    End Sub

    Private Shared Sub EnsureDicomInitialized()
        If _dicomInitialized Then Return
        Try
            Dim builder As New DicomSetupBuilder()
            builder.RegisterServices(Sub(services)
                                         services.AddFellowOakDicom()
                                         services.AddTranscoderManager(Of NativeTranscoderManager)()
                                         services.AddImageManager(Of WinFormsImageManager)()
                                     End Sub)
            builder.Build()
            _nativeCodecAvailable = True
        Catch
            _nativeCodecAvailable = False
        End Try
        _dicomInitialized = True
    End Sub

    Private Shared Function TryLoadImageFromPath(path As String, ByRef image As Image, ByRef errorMessage As String) As Boolean
        image = Nothing
        errorMessage = ""

        If String.IsNullOrWhiteSpace(path) Then
            errorMessage = "No file selected."
            Return False
        End If

        If IsDicomFile(path) Then
            Return TryLoadDicomImage(path, image, errorMessage)
        End If

        Return TryLoadStandardImage(path, image, errorMessage)
    End Function

    Private Shared Function TryLoadStandardImage(path As String, ByRef image As Image, ByRef errorMessage As String) As Boolean
        Try
            Dim bytes = System.IO.File.ReadAllBytes(path)
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

    Private Shared Function TryLoadDicomImage(path As String, ByRef image As Image, ByRef errorMessage As String) As Boolean
        Try
            EnsureDicomInitialized()
            Dim dicomFile1 = DicomFile.Open(path)
            Dim transferSyntax = dicomFile1.FileMetaInfo?.TransferSyntax
            If transferSyntax IsNot Nothing AndAlso
           transferSyntax.UID IsNot Nothing AndAlso
           transferSyntax.UID.Name IsNot Nothing AndAlso
           transferSyntax.UID.Name.IndexOf("JPEG Extended", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso
           Not _nativeCodecAvailable Then
                errorMessage = "This DICOM uses JPEG Extended (12-bit) which is not supported without native codecs."
                Return False
            End If

            Dim pixelData = DicomPixelData.Create(dicomFile1.Dataset, False)
            If pixelData.NumberOfFrames <= 0 Then
                errorMessage = "DICOM file does not contain image frames."
                Return False
            End If

            Dim dicomImage = New DicomImage(dicomFile1.Dataset)
            image = dicomImage.RenderImage(0).AsClonedBitmap()
            Return True
        Catch ex As Exception
            errorMessage = If(String.IsNullOrWhiteSpace(ex.Message),
                          "Failed to read DICOM image data.",
                          ex.Message)
            Return False
        End Try
    End Function

    Private Shared Function IsDicomFile(path As String) As Boolean
        If String.Equals(System.IO.Path.GetExtension(path), ".dcm", StringComparison.OrdinalIgnoreCase) Then
            Return True
        End If

        Try
            Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                If fs.Length < 132 Then Return False
                fs.Seek(128, SeekOrigin.Begin)
                Dim buffer(3) As Byte
                fs.Read(buffer, 0, 4)
                Dim marker = System.Text.Encoding.ASCII.GetString(buffer)
                Return String.Equals(marker, "DICM", StringComparison.Ordinal)
            End Using
        Catch
            Return False
        End Try
    End Function
End Class
