Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing

Public Class FrmOrthoImages

    Private beforeFiles As List(Of String) = New List(Of String)()
    Private duringFiles As List(Of String) = New List(Of String)()
    Private afterFiles As List(Of String) = New List(Of String)()
    Private beforeIndex As Integer = 0
    Private duringIndex As Integer = 0
    Private afterIndex As Integer = 0
    Private isPlaying As Boolean = False
    Private _isSyncingSelection As Boolean = False
    Private _formWidthWithoutDuring As Integer = 0

    Private Enum OrthoImageStage
        BeforeStage = 0
        DuringStage = 1
        AfterStage = 2
    End Enum

    Private ReadOnly imageExtensions As String() = {".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff"}

    Public Sub New()
        InitializeComponent()
        Me.LookAndFeel.SkinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName
        Me.Icon = AppIcon
    End Sub

    Private Shared Function T(en As String, ar As String) As String
        Return If(Eng, en, ar)
    End Function

    Private Sub ApplyUiLanguage()
        lblBefore.Text = T("BEFORE", "قبل")
        lblDuring.Text = T("DURING", "خلال")
        lblAfter.Text = T("AFTER", "بعد")
        btnBrowseBefore.Text = T("Browse...", "استعراض...")
        btnBrowseDuring.Text = T("Browse...", "استعراض...")
        btnBrowseAfter.Text = T("Browse...", "استعراض...")
        chkSyncNavigation.Properties.Caption = T("Sync navigation", "مزامنة التنقل")
        chkShowDuring.Properties.Caption = T("Show During", "إظهار خلال")
        chkFullResolution.Properties.Caption = T("Full-Resolution Preview", "معاينة بالدقة الكاملة")
        btnPrev.Text = T("Prev", "السابق")
        btnNext.Text = T("Next", "التالي")
        btnPlayPause.Text = If(isPlaying, T("Pause", "إيقاف"), T("Play", "تشغيل"))
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then
            Text = T("Orthodontic Images", "صور التقويم")
        End If
    End Sub

    Private Sub SafeDisposePicture(target As DevExpress.XtraEditors.PictureEdit)
        If target Is Nothing Then Return
        Dim img = target.Image
        target.Image = Nothing
        If img IsNot Nothing Then img.Dispose()
    End Sub

    Private Function ResolvePictureByStage(stage As OrthoImageStage) As DevExpress.XtraEditors.PictureEdit
        Select Case stage
            Case OrthoImageStage.BeforeStage
                Return PictureBefore
            Case OrthoImageStage.DuringStage
                Return PictureDuring
            Case Else
                Return PictureAfter
        End Select
    End Function

    Private Sub SetPictureImage(target As DevExpress.XtraEditors.PictureEdit, newImage As Image)
        If target Is Nothing Then
            If newImage IsNot Nothing Then newImage.Dispose()
            Return
        End If
        Dim oldImg = target.Image
        target.Image = newImage
        If oldImg IsNot Nothing Then oldImg.Dispose()
    End Sub

    Private Function BuildDisplayBitmap(original As Image, target As DevExpress.XtraEditors.PictureEdit) As Bitmap
        If chkFullResolution IsNot Nothing AndAlso chkFullResolution.Checked Then
            Return New Bitmap(original)
        End If

        Dim srcW = Math.Max(1, original.Width)
        Dim srcH = Math.Max(1, original.Height)
        Dim boxW = If(target IsNot Nothing AndAlso target.ClientSize.Width > 0, target.ClientSize.Width, 1200)
        Dim boxH = If(target IsNot Nothing AndAlso target.ClientSize.Height > 0, target.ClientSize.Height, 900)

        ' Hard cap to avoid huge allocations on very large source images.
        Dim maxEdge As Double = 2200.0R
        Dim scaleByEdge As Double = Math.Min(maxEdge / srcW, maxEdge / srcH)
        If scaleByEdge > 1.0R Then scaleByEdge = 1.0R

        ' Also scale down to fit current preview box when larger.
        Dim scaleByBox As Double = Math.Min(boxW / CDbl(srcW), boxH / CDbl(srcH))
        If scaleByBox > 1.0R Then scaleByBox = 1.0R

        Dim scale = Math.Min(scaleByEdge, scaleByBox)
        If scale <= 0 Then scale = scaleByEdge
        If scale <= 0 Then scale = 1.0R

        Dim dstW = Math.Max(1, CInt(Math.Round(srcW * scale)))
        Dim dstH = Math.Max(1, CInt(Math.Round(srcH * scale)))
        Dim bmp As New Bitmap(dstW, dstH, Imaging.PixelFormat.Format24bppRgb)
        Using g = Graphics.FromImage(bmp)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.Clear(Color.Black)
            g.DrawImage(original, New Rectangle(0, 0, dstW, dstH))
        End Using
        Return bmp
    End Function

    Private Sub FrmOrthoImages_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyUiLanguage()
            chkSyncNavigation.Checked = False
            chkShowDuring.Checked = True
            chkFullResolution.Checked = False
            If _formWidthWithoutDuring <= 0 Then _formWidthWithoutDuring = Me.Width
            SetShowDuringLayout(True)
            LoadDefaultFolders()
        Catch ex As Exception
            MessageBox.Show(T("Error while loading images: ", "حدث خطأ أثناء تحميل الصور: ") & ex.Message,
                            T("Error", "خطأ"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkShowDuring_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowDuring.CheckedChanged
        SetShowDuringLayout(chkShowDuring.Checked)
        CenterFormOnCurrentScreen()
        UpdateStatus()
    End Sub

    Private Sub chkFullResolution_CheckedChanged(sender As Object, e As EventArgs) Handles chkFullResolution.CheckedChanged
        ReloadCurrentPreviewImages()
    End Sub

    Private Sub ReloadCurrentPreviewImages()
        Try
            If beforeFiles.Count > 0 AndAlso beforeIndex >= 0 AndAlso beforeIndex < beforeFiles.Count Then
                ShowImage(beforeFiles(beforeIndex), OrthoImageStage.BeforeStage)
            End If
            If duringFiles.Count > 0 AndAlso duringIndex >= 0 AndAlso duringIndex < duringFiles.Count Then
                ShowImage(duringFiles(duringIndex), OrthoImageStage.DuringStage)
            End If
            If afterFiles.Count > 0 AndAlso afterIndex >= 0 AndAlso afterIndex < afterFiles.Count Then
                ShowImage(afterFiles(afterIndex), OrthoImageStage.AfterStage)
            End If
        Catch
        End Try
    End Sub

    Private Sub SetShowDuringLayout(showDuring As Boolean)
        If _formWidthWithoutDuring <= 0 Then _formWidthWithoutDuring = Me.Width
        If TableLayoutMain Is Nothing OrElse TableLayoutMain.ColumnStyles.Count < 3 Then Return

        TableLayoutMain.SuspendLayout()
        BottomPanel.SuspendLayout()
        Try
            If showDuring Then
                TableLayoutMain.ColumnStyles(0).SizeType = SizeType.Percent
                TableLayoutMain.ColumnStyles(0).Width = 33.3333F
                TableLayoutMain.ColumnStyles(1).SizeType = SizeType.Percent
                TableLayoutMain.ColumnStyles(1).Width = 33.3333F
                TableLayoutMain.ColumnStyles(2).SizeType = SizeType.Percent
                TableLayoutMain.ColumnStyles(2).Width = 33.3333F
                PanelDuring.Visible = True
                Dim targetWidth As Integer = Math.Min(1400, Screen.FromControl(Me).WorkingArea.Width)
                If targetWidth > 0 Then Me.Width = targetWidth
            Else
                PanelDuring.Visible = False
                TableLayoutMain.ColumnStyles(0).SizeType = SizeType.Percent
                TableLayoutMain.ColumnStyles(0).Width = 50.0F
                TableLayoutMain.ColumnStyles(1).SizeType = SizeType.Absolute
                TableLayoutMain.ColumnStyles(1).Width = 0.0F
                TableLayoutMain.ColumnStyles(2).SizeType = SizeType.Percent
                TableLayoutMain.ColumnStyles(2).Width = 50.0F
                If Me.Width <> _formWidthWithoutDuring Then Me.Width = _formWidthWithoutDuring
            End If
        Finally
            BottomPanel.ResumeLayout(True)
            TableLayoutMain.ResumeLayout(True)
        End Try
    End Sub

    Private Sub LoadDefaultFolders()
        Dim basePath As String = Application.StartupPath
        Dim beforePath As String = Path.Combine(basePath, "Images", "Ortho", "Before")
        Dim duringPath As String = Path.Combine(basePath, "Images", "Ortho", "During")
        Dim afterPath As String = Path.Combine(basePath, "Images", "Ortho", "After")

        If Directory.Exists(beforePath) Then
            LoadImageList(beforePath, OrthoImageStage.BeforeStage)
        End If

        If Directory.Exists(duringPath) Then
            LoadImageList(duringPath, OrthoImageStage.DuringStage)
        End If

        If Directory.Exists(afterPath) Then
            LoadImageList(afterPath, OrthoImageStage.AfterStage)
        End If

        UpdateStatus()
    End Sub

    ''' <summary>
    ''' Show this viewer for a specific patient, loading that patient's
    ''' orthodontic before/after folders if they exist.
    ''' Expected structure:
    '''   Images\Patient_{PatientID}\Before
    '''   Images\Patient_{PatientID}\After
    ''' </summary>
    Public Sub ShowForPatient(patientId As Integer)
        Try
            Dim basePath As String = Application.StartupPath
            Dim beforePath As String = Path.Combine(basePath, "Images", $"Patient_{patientId}", "Before")
            Dim duringPath As String = Path.Combine(basePath, "Images", $"Patient_{patientId}", "During")
            Dim afterPath As String = Path.Combine(basePath, "Images", $"Patient_{patientId}", "After")

            ' Clear current lists
            beforeFiles.Clear()
            duringFiles.Clear()
            afterFiles.Clear()
            cmbBeforeFiles.Properties.Items.Clear()
            If cmbDuringFiles IsNot Nothing Then cmbDuringFiles.Properties.Items.Clear()
            cmbAfterFiles.Properties.Items.Clear()
            SafeDisposePicture(PictureBefore)
            SafeDisposePicture(PictureDuring)
            SafeDisposePicture(PictureAfter)

            If Directory.Exists(beforePath) Then
                LoadImageList(beforePath, OrthoImageStage.BeforeStage)
            End If

            If Directory.Exists(duringPath) Then
                LoadImageList(duringPath, OrthoImageStage.DuringStage)
            End If

            If Directory.Exists(afterPath) Then
                LoadImageList(afterPath, OrthoImageStage.AfterStage)
            End If

            UpdateStatus()

            ' Optionally reflect patient in the title
            Me.Text = T("Orthodontic Images - Patient ", "صور التقويم - المريض ") & patientId.ToString()

            Me.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(T("Error loading patient images: ", "حدث خطأ أثناء تحميل صور المريض: ") & ex.Message,
                            T("Error", "خطأ"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadImageList(folder As String, stage As OrthoImageStage)
        Dim files = Directory.GetFiles(folder).
            Where(Function(f) imageExtensions.Contains(Path.GetExtension(f).ToLowerInvariant())).
            OrderBy(Function(f) f).ToList()

        Select Case stage
            Case OrthoImageStage.BeforeStage
                beforeFiles = files
                beforeIndex = 0
                cmbBeforeFiles.Properties.Items.Clear()
                cmbBeforeFiles.Properties.Items.AddRange(beforeFiles.Select(Function(f) Path.GetFileName(f)).ToArray())
                lblBeforeCount.Text = String.Format(T("({0} images loaded)", "(تم تحميل {0} صورة)"), beforeFiles.Count)
                If beforeFiles.Count > 0 Then
                    cmbBeforeFiles.SelectedIndex = 0
                    ShowImage(beforeFiles(0), OrthoImageStage.BeforeStage)
                Else
                    SafeDisposePicture(PictureBefore)
                    cmbBeforeFiles.EditValue = Nothing
                End If

            Case OrthoImageStage.DuringStage
                duringFiles = files
                duringIndex = 0
                If cmbDuringFiles IsNot Nothing Then
                    cmbDuringFiles.Properties.Items.Clear()
                    cmbDuringFiles.Properties.Items.AddRange(duringFiles.Select(Function(f) Path.GetFileName(f)).ToArray())
                    If duringFiles.Count > 0 Then
                        cmbDuringFiles.SelectedIndex = 0
                        ShowImage(duringFiles(0), OrthoImageStage.DuringStage)
                    Else
                        SafeDisposePicture(PictureDuring)
                        cmbDuringFiles.EditValue = Nothing
                    End If
                End If
                If lblDuringCount IsNot Nothing Then lblDuringCount.Text = String.Format(T("({0} images loaded)", "(تم تحميل {0} صورة)"), duringFiles.Count)

            Case Else
                afterFiles = files
                afterIndex = 0
                cmbAfterFiles.Properties.Items.Clear()
                cmbAfterFiles.Properties.Items.AddRange(afterFiles.Select(Function(f) Path.GetFileName(f)).ToArray())
                lblAfterCount.Text = String.Format(T("({0} images loaded)", "(تم تحميل {0} صورة)"), afterFiles.Count)
                If afterFiles.Count > 0 Then
                    cmbAfterFiles.SelectedIndex = 0
                    ShowImage(afterFiles(0), OrthoImageStage.AfterStage)
                Else
                    SafeDisposePicture(PictureAfter)
                    cmbAfterFiles.EditValue = Nothing
                End If
        End Select
    End Sub

    Private Sub ShowImage(path As String, stage As OrthoImageStage)
        Dim target = ResolvePictureByStage(stage)
        If String.IsNullOrWhiteSpace(path) OrElse Not File.Exists(path) Then
            SetPictureImage(target, Nothing)
            Return
        End If

        Try
            Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using original As Image = Image.FromStream(fs)
                    Dim displayBmp = BuildDisplayBitmap(original, target)
                    SetPictureImage(target, displayBmp)
                End Using
            End Using
        Catch ex As OutOfMemoryException
            SetPictureImage(target, Nothing)
            MessageBox.Show(
                T("Image is too large or invalid and could not be loaded: ", "الصورة كبيرة جدًا أو غير صالحة وتعذر تحميلها: ") &
                IO.Path.GetFileName(path),
                T("Image Error", "خطأ الصورة"),
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            SetPictureImage(target, Nothing)
            MessageBox.Show(T("Error loading image: ", "حدث خطأ أثناء تحميل الصورة: ") & ex.Message,
                            T("Image Error", "خطأ الصورة"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub btnBrowseBefore_Click(sender As Object, e As EventArgs) Handles btnBrowseBefore.Click
        Using dlg As New FolderBrowserDialog()
            dlg.Description = T("Select BEFORE images folder", "اختر مجلد صور قبل")
            dlg.RootFolder = Environment.SpecialFolder.MyComputer
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                LoadImageList(dlg.SelectedPath, OrthoImageStage.BeforeStage)
                UpdateStatus()
            End If
        End Using
    End Sub

    Private Sub btnBrowseDuring_Click(sender As Object, e As EventArgs) Handles btnBrowseDuring.Click
        Using dlg As New FolderBrowserDialog()
            dlg.Description = T("Select DURING images folder", "اختر مجلد صور خلال")
            dlg.RootFolder = Environment.SpecialFolder.MyComputer
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                LoadImageList(dlg.SelectedPath, OrthoImageStage.DuringStage)
                UpdateStatus()
            End If
        End Using
    End Sub

    Private Sub btnBrowseAfter_Click(sender As Object, e As EventArgs) Handles btnBrowseAfter.Click
        Using dlg As New FolderBrowserDialog()
            dlg.Description = T("Select AFTER images folder", "اختر مجلد صور بعد")
            dlg.RootFolder = Environment.SpecialFolder.MyComputer
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                LoadImageList(dlg.SelectedPath, OrthoImageStage.AfterStage)
                UpdateStatus()
            End If
        End Using
    End Sub

    Private Sub cmbBeforeFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBeforeFiles.SelectedIndexChanged
        If cmbBeforeFiles.SelectedIndex < 0 OrElse cmbBeforeFiles.SelectedIndex >= beforeFiles.Count Then Return
        beforeIndex = cmbBeforeFiles.SelectedIndex
        ShowImage(beforeFiles(beforeIndex), OrthoImageStage.BeforeStage)
        If chkSyncNavigation.Checked AndAlso Not _isSyncingSelection Then
            _isSyncingSelection = True
            Try
                SyncOtherStagesFromIndex(beforeIndex, OrthoImageStage.BeforeStage)
            Finally
                _isSyncingSelection = False
            End Try
        End If
        UpdateStatus()
    End Sub

    Private Sub cmbDuringFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDuringFiles.SelectedIndexChanged
        If cmbDuringFiles Is Nothing Then Return
        If cmbDuringFiles.SelectedIndex < 0 OrElse cmbDuringFiles.SelectedIndex >= duringFiles.Count Then Return
        duringIndex = cmbDuringFiles.SelectedIndex
        ShowImage(duringFiles(duringIndex), OrthoImageStage.DuringStage)
        If chkSyncNavigation.Checked AndAlso Not _isSyncingSelection Then
            _isSyncingSelection = True
            Try
                SyncOtherStagesFromIndex(duringIndex, OrthoImageStage.DuringStage)
            Finally
                _isSyncingSelection = False
            End Try
        End If
        UpdateStatus()
    End Sub

    Private Sub cmbAfterFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAfterFiles.SelectedIndexChanged
        If cmbAfterFiles.SelectedIndex < 0 OrElse cmbAfterFiles.SelectedIndex >= afterFiles.Count Then Return
        afterIndex = cmbAfterFiles.SelectedIndex
        ShowImage(afterFiles(afterIndex), OrthoImageStage.AfterStage)
        If chkSyncNavigation.Checked AndAlso Not _isSyncingSelection Then
            _isSyncingSelection = True
            Try
                SyncOtherStagesFromIndex(afterIndex, OrthoImageStage.AfterStage)
            Finally
                _isSyncingSelection = False
            End Try
        End If
        UpdateStatus()
    End Sub

    Private Sub SyncOtherStagesFromIndex(sourceIndex As Integer, sourceStage As OrthoImageStage)
        If sourceStage <> OrthoImageStage.BeforeStage AndAlso beforeFiles.Count > 0 Then
            beforeIndex = Math.Min(sourceIndex, beforeFiles.Count - 1)
            cmbBeforeFiles.SelectedIndex = beforeIndex
        End If

        If sourceStage <> OrthoImageStage.DuringStage AndAlso duringFiles.Count > 0 AndAlso cmbDuringFiles IsNot Nothing Then
            duringIndex = Math.Min(sourceIndex, duringFiles.Count - 1)
            cmbDuringFiles.SelectedIndex = duringIndex
        End If

        If sourceStage <> OrthoImageStage.AfterStage AndAlso afterFiles.Count > 0 Then
            afterIndex = Math.Min(sourceIndex, afterFiles.Count - 1)
            cmbAfterFiles.SelectedIndex = afterIndex
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        NavigateRelative(-1)
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        NavigateRelative(1)
    End Sub

    Private Sub NavigateRelative(stepDelta As Integer)
        If beforeFiles.Count = 0 AndAlso duringFiles.Count = 0 AndAlso afterFiles.Count = 0 Then Return

        If beforeFiles.Count > 0 Then
            beforeIndex = (beforeIndex + stepDelta + beforeFiles.Count) Mod beforeFiles.Count
            cmbBeforeFiles.SelectedIndex = beforeIndex
        End If

        If duringFiles.Count > 0 AndAlso cmbDuringFiles IsNot Nothing Then
            Dim targetDuring As Integer
            If chkSyncNavigation.Checked AndAlso beforeFiles.Count > 0 Then
                targetDuring = Math.Min(beforeIndex, duringFiles.Count - 1)
            Else
                targetDuring = (duringIndex + stepDelta + duringFiles.Count) Mod duringFiles.Count
            End If
            duringIndex = targetDuring
            cmbDuringFiles.SelectedIndex = duringIndex
        End If

        If afterFiles.Count > 0 Then
            Dim targetIndex As Integer
            If chkSyncNavigation.Checked AndAlso beforeFiles.Count > 0 Then
                targetIndex = Math.Min(beforeIndex, afterFiles.Count - 1)
            Else
                targetIndex = (afterIndex + stepDelta + afterFiles.Count) Mod afterFiles.Count
            End If
            afterIndex = targetIndex
            cmbAfterFiles.SelectedIndex = afterIndex
        End If

        UpdateStatus()
    End Sub

    Private Sub btnPlayPause_Click(sender As Object, e As EventArgs) Handles btnPlayPause.Click
        If Not isPlaying Then
            If beforeFiles.Count = 0 AndAlso duringFiles.Count = 0 AndAlso afterFiles.Count = 0 Then
                MessageBox.Show(T("No images loaded to play.", "لا توجد صور محمّلة للتشغيل."),
                                T("Slide Show", "عرض الشرائح"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            SlideTimer.Start()
            isPlaying = True
            btnPlayPause.Text = T("Pause", "إيقاف")
            lblStatus.Text = T("Playing slide show (every 3 seconds)...", "يتم تشغيل عرض الشرائح (كل 3 ثوانٍ)...")
        Else
            SlideTimer.Stop()
            isPlaying = False
            btnPlayPause.Text = T("Play", "تشغيل")
            UpdateStatus()
        End If
    End Sub

    Private Sub SlideTimer_Tick(sender As Object, e As EventArgs) Handles SlideTimer.Tick
        NavigateRelative(1)
    End Sub

    Private Sub UpdateStatus()
        Dim beforeText As String = String.Format(T("{0} before", "{0} قبل"), beforeFiles.Count)
        Dim duringText As String = String.Format(T("{0} during", "{0} خلال"), duringFiles.Count)
        Dim afterText As String = String.Format(T("{0} after", "{0} بعد"), afterFiles.Count)
        Dim posText As String = ""

        If beforeFiles.Count > 0 OrElse duringFiles.Count > 0 OrElse afterFiles.Count > 0 Then
            Dim maxCount As Integer = Math.Max(
                If(beforeFiles.Count > 0, beforeFiles.Count, 0),
                Math.Max(If(duringFiles.Count > 0, duringFiles.Count, 0),
                         If(afterFiles.Count > 0, afterFiles.Count, 0)))

            Dim current As Integer = Math.Max(
                If(beforeFiles.Count > 0, beforeIndex + 1, 0),
                Math.Max(If(duringFiles.Count > 0, duringIndex + 1, 0),
                         If(afterFiles.Count > 0, afterIndex + 1, 0)))

            If maxCount > 0 AndAlso current > 0 Then
                posText = String.Format(T(" - Image {0} of {1}", " - صورة {0} من {1}"), current, maxCount)
            End If
        End If

        If chkShowDuring IsNot Nothing AndAlso chkShowDuring.Checked Then
            lblStatus.Text = String.Format(T("Ready - {0} / {1} / {2}{3}", "جاهز - {0} / {1} / {2}{3}"), beforeText, duringText, afterText, posText)
        Else
            lblStatus.Text = String.Format(T("Ready - {0} / {1}{2}", "جاهز - {0} / {1}{2}"), beforeText, afterText, posText)
        End If
    End Sub

    Private Sub CenterFormOnCurrentScreen()
        Try
            Dim wa = Screen.FromControl(Me).WorkingArea
            Dim x = wa.Left + Math.Max(0, (wa.Width - Me.Width) \ 2)
            Dim y = wa.Top + Math.Max(0, (wa.Height - Me.Height) \ 2)
            Me.Location = New Point(x, y)
        Catch
            ' Keep toggle robust even if centering fails.
        End Try
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        Try
            If SlideTimer IsNot Nothing Then
                SlideTimer.Stop()
            End If
            SafeDisposePicture(PictureBefore)
            SafeDisposePicture(PictureDuring)
            SafeDisposePicture(PictureAfter)
        Catch
            ' Keep close path safe.
        End Try
        MyBase.OnFormClosed(e)
    End Sub

End Class