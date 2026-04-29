Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Windows.Forms
Imports System.Linq

''' <summary>
''' Robust thumbnail viewer: no file locking, no memory leaks, proper disposal.
''' Loads images via byte array so files are never held open.
''' </summary>
Public Class ImageThumbnailViewer

#Region "Constants"

    Private Const DefaultThumbSize As Integer = 100
    Private Const DefaultSpacing As Integer = 8
    Private Const DefaultBorder As Integer = 8
    ''' <summary>Max dimension when loading source image for thumbnail (avoids holding huge decoded bitmap).</summary>
    Private Const ThumbnailSourceMaxDimension As Integer = 256
    Private Shared ReadOnly AllowedExtensions As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
        ".bmp", ".gif", ".jpeg", ".jpg", ".png"
    }

    Private Shared ReadOnly ColorSelected As Color = Color.DeepSkyBlue
    Private Shared ReadOnly ColorHover As Color = Color.AliceBlue

#End Region

#Region "Events and backing fields"

    Public Event PictureSelected(sender As Object, e As PictureSelectedEventArgs)

    Private _directoryPath As String = String.Empty
    Private _thumbSize As Integer = DefaultThumbSize
    Private _spacing As Integer = DefaultSpacing
    Private _border As Integer = DefaultBorder
    Private _autoRotate As Boolean = True
    Private _selectedIndex As Integer = -1
    Private _selectedFilePath As String = String.Empty

    ''' <summary>Ordered list of image file paths currently displayed.</summary>
    Private _imagePaths As New List(Of String)
    ''' <summary>Currently selected picture box (reference only; we don't dispose it).</summary>
    Private _selectedPictureBox As PictureBox = Nothing
    Private _selectedImgGrp As GroupBox = Nothing
    Private _scrollPanel As Panel = Nothing

#End Region

#Region "Properties"

    Public Property DirectoryPath As String
        Get
            Return _directoryPath
        End Get
        Set(value As String)
            _directoryPath = If(value, String.Empty).Trim()
        End Set
    End Property

    Public Property ThumbnailSize As Integer
        Get
            Return _thumbSize
        End Get
        Set(value As Integer)
            If value < 32 Then value = 32
            If value > 256 Then value = 256
            _thumbSize = value
            RefreshViewer()
        End Set
    End Property

    Public Property Spacing As Integer
        Get
            Return _spacing
        End Get
        Set(value As Integer)
            If value < 0 Then value = 0
            _spacing = value
            RefreshViewer()
        End Set
    End Property

    ''' <summary>Whether to auto-rotate thumbnails based on EXIF orientation.</summary>
    Public Property AutoRotate As Boolean
        Get
            Return _autoRotate
        End Get
        Set(value As Boolean)
            _autoRotate = value
        End Set
    End Property

    Public ReadOnly Property SelectedIndex As Integer
        Get
            Return _selectedIndex
        End Get
    End Property

    Public ReadOnly Property SelectedFilePath As String
        Get
            Return _selectedFilePath
        End Get
    End Property

    ''' <summary>Returns a copy of the selected image, or Nothing. Caller must dispose the returned image.</summary>
    Public ReadOnly Property SelectedImage As Image
        Get
            If _selectedPictureBox Is Nothing OrElse _selectedPictureBox.Image Is Nothing Then Return Nothing
            Try
                Return New Bitmap(_selectedPictureBox.Image)
            Catch
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property ImageCount As Integer
        Get
            Return _imagePaths.Count
        End Get
    End Property

    ''' <summary>Alias for ThumbnailSize (API compatibility with ThumbsGroup).</summary>
    Public Property Dimension As Integer
        Get
            Return ThumbnailSize
        End Get
        Set(value As Integer)
            ThumbnailSize = value
        End Set
    End Property

    ''' <summary>Alias for SelectedIndex (API compatibility with ThumbsGroup).</summary>
    Public ReadOnly Property ImageIndex As Integer
        Get
            Return SelectedIndex
        End Get
    End Property

#End Region

#Region "Initialization and disposal"

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        EnsureScrollPanel()
    End Sub

    ''' <summary>Creates the scroll panel (same pattern as ThumbsGroup).</summary>
    Private Sub EnsureScrollPanel()
        If _scrollPanel IsNot Nothing Then Return
        _scrollPanel = New Panel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .BackColor = Color.Transparent
        }
        grpMain.Controls.Add(_scrollPanel)
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            ClearAllThumbnails(disposeImagesOnly:=False)
            _selectedPictureBox = Nothing
            _imagePaths.Clear()
            If components IsNot Nothing Then components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

#Region "Load images (no file lock)"

    ''' <summary>Scans DirectoryPath and rebuilds the thumbnail list. Does not lock files.</summary>
    Public Sub DisplayImages()
        _imagePaths.Clear()
        If String.IsNullOrWhiteSpace(_directoryPath) OrElse Not Directory.Exists(_directoryPath) Then
            BuildThumbnails()
            Return
        End If
        Try
            Dim files = Directory.GetFiles(_directoryPath)
            For Each path In files
                If AllowedExtensions.Contains(IO.Path.GetExtension(path)) Then
                    _imagePaths.Add(path)
                End If
            Next
        Catch ex As Exception
            ' Log and continue with empty list
            System.Diagnostics.Debug.WriteLine("ImageThumbnailViewer.DisplayImages: " & ex.Message)
        End Try
        BuildThumbnails()
    End Sub

    ''' <summary>Same as DisplayImages(); alias for API compatibility.</summary>
    Public Sub RefreshViewer()
        DisplayImages()
    End Sub

    ''' <summary>Load thumbnail from file using safe load (max dimension + OOM handling). Does not lock file. Returns Nothing on OOM or error.</summary>
    Private Function LoadThumbnailFromFile(filePath As String) As Bitmap
        If String.IsNullOrWhiteSpace(filePath) OrElse Not File.Exists(filePath) Then Return Nothing
        ' Load at reduced size to avoid OOM on 10+ MB high-res JPGs; EXIF is applied inside LoadImageAtMaxSize
        Dim source As Bitmap = Nothing
        Try
            source = LoadImageAtMaxSize(filePath, ThumbnailSourceMaxDimension)
        Catch ex As OutOfMemoryException
            System.Diagnostics.Debug.WriteLine("ImageThumbnailViewer: OOM loading: " & filePath)
            Return Nothing
        End Try
        If source Is Nothing Then Return Nothing
        Try
            Dim w As Integer = Math.Max(1, Math.Min(_thumbSize, source.Width))
            Dim h As Integer = Math.Max(1, Math.Min(_thumbSize, source.Height))
            Dim bmp As New Bitmap(w, h)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.InterpolationMode = InterpolationMode.HighQualityBicubic
                g.DrawImage(source, 0, 0, w, h)
            End Using
            source.Dispose()
            source = Nothing
            Return bmp
        Catch ex As Exception
            If source IsNot Nothing Then source.Dispose()
            System.Diagnostics.Debug.WriteLine("ImageThumbnailViewer: thumbnail failed: " & filePath & " - " & ex.Message)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Build UI and clear"

    ''' <summary>Disposes all thumbnail images and optionally removes controls.</summary>
    Private Sub ClearAllThumbnails(disposeImagesOnly As Boolean)
        If _scrollPanel Is Nothing Then Return
        For Each ctrl As Control In _scrollPanel.Controls.Cast(Of Control).ToList()
            DisposeThumbnailControl(ctrl)
        Next
        _scrollPanel.Controls.Clear()
        If Not disposeImagesOnly Then
            _selectedPictureBox = Nothing
            _selectedImgGrp = Nothing
            _selectedIndex = -1
            _selectedFilePath = String.Empty
        End If
    End Sub

    Private Sub DisposeThumbnailControl(ctrl As Control)
        If ctrl Is Nothing Then Return
        If TypeOf ctrl Is GroupBox Then
            For Each child As Control In ctrl.Controls.Cast(Of Control).ToList()
                If TypeOf child Is PictureBox Then
                    Dim pb = CType(child, PictureBox)
                    If pb.Image IsNot Nothing Then
                        Try
                            pb.Image.Dispose()
                        Catch
                        End Try
                        pb.Image = Nothing
                    End If
                End If
                child.Dispose()
            Next
            ctrl.Controls.Clear()
        End If
        ctrl.Dispose()
    End Sub

    ''' <summary>Same layout as ThumbsGroup: row/col grid, GroupBox per thumb, PictureBox inside (Dimension+10, Dimension-10, Dimension-20).</summary>
    Private Sub BuildThumbnails()
        EnsureScrollPanel()
        ClearAllThumbnails(disposeImagesOnly:=False)

        Dim row As Integer = _border
        Dim col As Integer = _border

        For i As Integer = 0 To _imagePaths.Count - 1
            Dim path As String = _imagePaths(i)
            Dim thumb As Bitmap = LoadThumbnailFromFile(path)
            If thumb Is Nothing Then Continue For

            Dim grp As New GroupBox With {
                .Size = New Size(_thumbSize + 10, _thumbSize + 10),
                .Text = i.ToString(),
                .Location = New Point(col, row)
            }

            Dim picW As Integer = _thumbSize - 10
            Dim picH As Integer = _thumbSize - 20
            Dim pic As New PictureBox With {
                .Image = thumb,
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Size = New Size(picW, picH),
                .Location = New Point((grp.Width - picW) \ 2, (grp.Height - picH) \ 2),
                .BorderStyle = BorderStyle.FixedSingle,
                .Tag = path,
                .Cursor = Cursors.Hand
            }
            AddHandler pic.Click, AddressOf Thumb_Click
            AddHandler pic.MouseEnter, AddressOf Thumb_MouseEnter
            AddHandler pic.MouseLeave, AddressOf Thumb_MouseLeave

            grp.Controls.Add(pic)
            _scrollPanel.Controls.Add(grp)

            col += _thumbSize + _spacing
            Dim availWidth As Integer = Math.Max(_scrollPanel.Width, 400)
            If (col + _thumbSize + _spacing + _border) > availWidth Then
                col = _border
                row += _thumbSize + _spacing
            End If
        Next

        grpMain.Text = _imagePaths.Count.ToString() & " Images"

        ' Select first if any
        If _scrollPanel.Controls.Count > 0 Then
            Dim firstGrp As GroupBox = TryCast(_scrollPanel.Controls(0), GroupBox)
            If firstGrp IsNot Nothing AndAlso firstGrp.Controls.Count > 0 Then
                Dim firstPic As PictureBox = TryCast(firstGrp.Controls(0), PictureBox)
                If firstPic IsNot Nothing Then
                    SetSelection(firstPic)
                End If
            End If
        End If
    End Sub

    Private Sub SetSelection(pb As PictureBox)
        ClearSelectionHighlight()
        _selectedPictureBox = pb
        _selectedImgGrp = If(pb IsNot Nothing, TryCast(pb.Parent, GroupBox), Nothing)
        If pb Is Nothing Then
            _selectedIndex = -1
            _selectedFilePath = String.Empty
        Else
            _selectedIndex = IndexOfPictureBox(pb)
            _selectedFilePath = CStr(pb.Tag)
            pb.BorderStyle = BorderStyle.Fixed3D
            If pb.Parent IsNot Nothing Then pb.Parent.BackColor = ColorSelected
        End If
        RaiseEvent PictureSelected(Me, New PictureSelectedEventArgs(_selectedFilePath, _selectedIndex))
    End Sub

    Private Sub ClearSelectionHighlight()
        If _selectedPictureBox IsNot Nothing Then
            _selectedPictureBox.BorderStyle = BorderStyle.FixedSingle
            If _selectedPictureBox.Parent IsNot Nothing Then _selectedPictureBox.Parent.BackColor = Color.Transparent
        End If
    End Sub

    Private Function IndexOfPictureBox(pb As PictureBox) As Integer
        If pb Is Nothing OrElse _scrollPanel Is Nothing Then Return -1
        Dim parent As Control = pb.Parent
        If parent Is Nothing Then Return -1
        For i As Integer = 0 To _scrollPanel.Controls.Count - 1
            If _scrollPanel.Controls(i) Is parent Then Return i
        Next
        Return -1
    End Function

#End Region

#Region "Mouse events"

    Private Sub Thumb_Click(sender As Object, e As EventArgs)
        Dim pb = TryCast(sender, PictureBox)
        If pb IsNot Nothing Then SetSelection(pb)
    End Sub

    Private Sub Thumb_MouseEnter(sender As Object, e As EventArgs)
        Dim pb = TryCast(sender, PictureBox)
        If pb Is Nothing Then Return
        If pb Is _selectedPictureBox Then Return
        If pb.Parent IsNot Nothing Then pb.Parent.BackColor = ColorHover
    End Sub

    Private Sub Thumb_MouseLeave(sender As Object, e As EventArgs)
        Dim pb = TryCast(sender, PictureBox)
        If pb Is Nothing Then Return
        If pb.Parent Is Nothing Then Return
        If pb Is _selectedPictureBox Then
            pb.Parent.BackColor = ColorSelected
        Else
            pb.Parent.BackColor = Color.Transparent
        End If
    End Sub

    Private Sub grpMain_Click(sender As Object, e As EventArgs) Handles grpMain.Click
        ClearSelectionHighlight()
        _selectedPictureBox = Nothing
        _selectedImgGrp = Nothing
        _selectedIndex = -1
        _selectedFilePath = String.Empty
        RaiseEvent PictureSelected(Me, New PictureSelectedEventArgs(String.Empty, -1))
    End Sub

#End Region

#Region "Public API: Clear, Remove, Delete"

    ''' <summary>Clears the list and all thumbnails; releases all image resources.</summary>
    Public Sub Clear()
        ClearAllThumbnails(disposeImagesOnly:=False)
        _imagePaths.Clear()
        grpMain.Text = "0 Images"
    End Sub

    ''' <summary>Removes the image at the given index (file not deleted). Compatible with ThumbsGroup.RemoveAt.</summary>
    Public Sub RemoveAt(index As Integer)
        RemoveImageAt(index)
    End Sub

    ''' <summary>Removes the image at the given index from the list and UI (file is not deleted).</summary>
    Public Sub RemoveImageAt(index As Integer)
        If index < 0 OrElse index >= _imagePaths.Count Then Return
        _imagePaths.RemoveAt(index)
        BuildThumbnails()
    End Sub

    ''' <summary>Removes the image with the given path from the list and UI (file is not deleted).</summary>
    Public Sub RemoveImage(filePath As String)
        If String.IsNullOrWhiteSpace(filePath) Then Return
        Dim normalized As String = filePath.Trim()
        _imagePaths.RemoveAll(Function(p) String.Equals(p, normalized, StringComparison.OrdinalIgnoreCase))
        BuildThumbnails()
    End Sub

    ''' <summary>Deletes the file from disk and removes it from the viewer.</summary>
    Public Sub DeleteSelectedImage()
        If String.IsNullOrWhiteSpace(_selectedFilePath) Then Return
        If Not File.Exists(_selectedFilePath) Then
            RemoveImage(_selectedFilePath)
            Return
        End If
        Try
            File.Delete(_selectedFilePath)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("ImageThumbnailViewer.DeleteSelectedImage: " & ex.Message)
            ' Optionally show message to user
            Return
        End Try
        RemoveImage(_selectedFilePath)
    End Sub

    ''' <summary>Deletes the file at the given path from disk and removes it from the viewer.</summary>
    Public Sub DeleteImage(filePath As String)
        If String.IsNullOrWhiteSpace(filePath) Then Return
        If File.Exists(filePath) Then
            Try
                File.Delete(filePath)
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("ImageThumbnailViewer.DeleteImage: " & ex.Message)
                Return
            End Try
        End If
        RemoveImage(filePath)
    End Sub

#End Region

#Region "Nested types (compatible with existing PictureSelectedEventArgs)"

    Public Class PictureSelectedEventArgs
        Inherits EventArgs
        Public Property FileName As String
        Public Property Index As Integer
        Public Sub New(fileName As String, index As Integer)
            Me.FileName = fileName
            Me.Index = index
        End Sub
    End Class

#End Region

End Class
