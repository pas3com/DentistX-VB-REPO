Option Strict On
Option Explicit On

Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraBars.Ribbon
Imports DentistX   ' for ModuleImages.LoadImageAtMaxSize / LoadImageMaxDimension

Partial Public Class ThumbGalleryViewer

#Region "Events / fields"

    Public Event PictureSelected(sender As Object, e As PictureSelectedEventArgs)

    Private _directoryPath As String = String.Empty
    Private _dimension As Integer = 100
    Private _spacing As Integer = 4
    Private _imagePaths As New List(Of String)
    Private _selectedIndex As Integer = -1
    Private _selectedFile As String = String.Empty
    Private _selectedFiles As New List(Of String)
    Private _selectedIndices As New List(Of Integer)
    Private _lastAnchorIndex As Integer = -1

    Private _thumbnailLoadCursorForm As Form = Nothing
    Private _thumbnailLoadCursorPrevious As Cursor = Nothing

#End Region

#Region "Properties (compatible with ThumbsGroup / ImageThumbnailViewer)"

    Public Property DirectoryPath As String
        Get
            Return _directoryPath
        End Get
        Set(value As String)
            _directoryPath = If(value, String.Empty)
        End Set
    End Property

    Public Property Dimension As Integer
        Get
            Return _dimension
        End Get
        Set(value As Integer)
            If value < 32 Then value = 32
            If value > 256 Then value = 256
            _dimension = value
            ConfigureGallery()
        End Set
    End Property

    Public Property Spacing As Integer
        Get
            Return _spacing
        End Get
        Set(value As Integer)
            If value < 0 Then value = 0
            _spacing = value
            ConfigureGallery()
        End Set
    End Property

    Public ReadOnly Property ImageIndex As Integer
        Get
            Return _selectedIndex
        End Get
    End Property

    Public ReadOnly Property SelectedFilePath As String
        Get
            Return _selectedFile
        End Get
    End Property

    Public ReadOnly Property SelectedFilePaths As IReadOnlyList(Of String)
        Get
            Return _selectedFiles.AsReadOnly()
        End Get
    End Property

    Public ReadOnly Property SelectedIndices As IReadOnlyList(Of Integer)
        Get
            Return _selectedIndices.AsReadOnly()
        End Get
    End Property

#End Region

    Public Sub New()
        InitializeComponent()
        ConfigureGallery()
        AddHandler gcThumbs.Gallery.ItemClick, AddressOf Gallery_ItemClick
        AddHandler gcThumbs.KeyDown, AddressOf GalleryControl_KeyDown
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.A) Then
            SelectAll()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub ConfigureGallery()
        Dim g = gcThumbs.Gallery
        g.ShowGroupCaption = False
        g.ShowItemText = False
        SetGalleryCheckMode(g)
        g.ImageSize = New Size(_dimension, _dimension)
        g.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside
        ' REMOVE these two lines that cause errors:
        ' g.ItemImagePadding = New Padding(1)
        ' g.ItemImageSpacing = _spacing
    End Sub

    Private Sub SetGalleryCheckMode(g As Object)
        If g Is Nothing Then Return
        Try
            Dim modeProp = g.GetType().GetProperty("ItemCheckMode")
            If modeProp Is Nothing Then Return

            Dim modeType = modeProp.PropertyType
            Dim names = [Enum].GetNames(modeType)
            Dim targetName As String = "SingleCheck"
            If Array.IndexOf(names, "MultipleCheck") >= 0 Then
                targetName = "MultipleCheck"
            ElseIf Array.IndexOf(names, "Multiple") >= 0 Then
                targetName = "Multiple"
            Else
                For Each n In names
                    If n.IndexOf("multi", StringComparison.OrdinalIgnoreCase) >= 0 Then
                        targetName = n
                        Exit For
                    End If
                Next
            End If
            Dim modeValue = [Enum].Parse(modeType, targetName)
            modeProp.SetValue(g, modeValue, Nothing)
        Catch
            ' keep default mode if this DevExpress version differs
        End Try
    End Sub

    Private Sub BeginThumbnailLoad(total As Integer)
        pbLoad.Style = ProgressBarStyle.Continuous
        pbLoad.Minimum = 0
        pbLoad.Maximum = Math.Max(1, total)
        pbLoad.Value = 0
        pbLoad.Visible = True

        Dim f = Me.FindForm()
        If f IsNot Nothing Then
            _thumbnailLoadCursorForm = f
            _thumbnailLoadCursorPrevious = f.Cursor
            f.Cursor = Cursors.WaitCursor
        End If
    End Sub

    Private Sub ReportThumbnailLoadProgress(index1Based As Integer, total As Integer, filePath As String)
        Dim name = Path.GetFileName(filePath)
        If name.Length > 36 Then
            name = name.Substring(0, 16) & "…" & name.Substring(name.Length - 17)
        End If
        grpMain.Text = $"Loading {index1Based} / {total} — {name}"
        pbLoad.Value = Math.Min(index1Based, pbLoad.Maximum)
        Application.DoEvents()
    End Sub

    Private Sub EndThumbnailLoad()
        pbLoad.Visible = False
        pbLoad.Value = 0

        If _thumbnailLoadCursorForm IsNot Nothing Then
            _thumbnailLoadCursorForm.Cursor = _thumbnailLoadCursorPrevious
            _thumbnailLoadCursorForm = Nothing
            _thumbnailLoadCursorPrevious = Nothing
        End If
    End Sub

#Region "Public API"

    Public Sub Clear()
        _imagePaths.Clear()
        gcThumbs.Gallery.Groups.Clear()
        _selectedIndex = -1
        _selectedFile = String.Empty
        _selectedFiles.Clear()
        _selectedIndices.Clear()
        _lastAnchorIndex = -1
        pbLoad.Visible = False
        pbLoad.Value = 0
        grpMain.Text = "0 Images"
    End Sub

    Public Sub DisplayImages()
        _imagePaths.Clear()
        gcThumbs.Gallery.Groups.Clear()
        _selectedFiles.Clear()
        _selectedIndices.Clear()
        _lastAnchorIndex = -1
        _selectedIndex = -1
        _selectedFile = String.Empty

        If String.IsNullOrWhiteSpace(_directoryPath) OrElse Not Directory.Exists(_directoryPath) Then
            grpMain.Text = "0 Images"
            Return
        End If

        Dim exts = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
            ".jpg", ".jpeg", ".png", ".bmp", ".gif"
        }

        For Each f In Directory.GetFiles(_directoryPath)
            If exts.Contains(Path.GetExtension(f)) Then
                _imagePaths.Add(f)
            End If
        Next

        Dim group As New GalleryItemGroup()
        gcThumbs.Gallery.Groups.Add(group)

        If _imagePaths.Count = 0 Then
            grpMain.Text = "0 Images"
            Return
        End If

        Dim total As Integer = _imagePaths.Count
        BeginThumbnailLoad(total)
        Try
            For i = 0 To total - 1
                Dim path = _imagePaths(i)
                ReportThumbnailLoadProgress(i + 1, total, path)

                Dim thumb As Bitmap = ModuleImages.LoadImageAtMaxSize(path, ModuleImages.LoadImageMaxDimension)
                If thumb Is Nothing Then Continue For

                Dim item As New GalleryItem()
                item.Image = thumb

                ' Build tooltip with file name and size in MB
                Dim sizeText As String = ""
                Try
                    Dim fi As New FileInfo(path)
                    Dim sizeMb As Double = fi.Length / (1024.0 * 1024.0)
                    sizeText = $" ({sizeMb:0.00} MB)"
                Catch
                    sizeText = ""
                End Try

                item.Hint = IO.Path.GetFileName(path) & sizeText
                item.Tag = path         ' full path
                group.Items.Add(item)
            Next

            grpMain.Text = total.ToString() & " Images"
        Finally
            EndThumbnailLoad()
        End Try
    End Sub

    Public Sub RefreshViewer()
        DisplayImages()
    End Sub

    Public Sub SelectAll()
        If _imagePaths.Count = 0 Then Return
        ApplySelectionSet(New HashSet(Of String)(_imagePaths, StringComparer.OrdinalIgnoreCase))
        _lastAnchorIndex = 0
        RaiseSelectionEvents()
    End Sub

    Public Sub ClearSelectionOnly()
        ClearCheckedItems()
        RefreshSelectionState()
        RaiseSelectionEvents()
    End Sub

    Public Sub RemoveAt(index As Integer)
        If index < 0 OrElse index >= _imagePaths.Count Then Return
        _imagePaths.RemoveAt(index)
        DisplayImages()
    End Sub

    Public Sub DeleteImage(path As String)
        If String.IsNullOrWhiteSpace(path) Then Return
        If File.Exists(path) Then
            Try
                File.Delete(path)
            Catch
                ' ignore IO errors here
            End Try
        End If
        _imagePaths.RemoveAll(Function(p) String.Equals(p, path, StringComparison.OrdinalIgnoreCase))
        DisplayImages()
    End Sub

    Public Sub DeleteImages(paths As IEnumerable(Of String))
        If paths Is Nothing Then Return

        Dim targetSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each p In paths
            If Not String.IsNullOrWhiteSpace(p) Then
                targetSet.Add(p)
            End If
        Next
        If targetSet.Count = 0 Then Return

        For Each p In targetSet
            If File.Exists(p) Then
                Try
                    File.Delete(p)
                Catch
                    ' ignore IO errors here
                End Try
            End If
        Next

        _imagePaths.RemoveAll(Function(p) targetSet.Contains(p))
        DisplayImages()
    End Sub

#End Region

#Region "Gallery events"

    Private Sub Gallery_ItemClick(sender As Object, e As GalleryItemClickEventArgs)
        Dim item = e.Item
        If item Is Nothing OrElse item.Tag Is Nothing Then Return

        Dim clickedPath As String = CStr(item.Tag)
        Dim clickedIndex As Integer = _imagePaths.IndexOf(clickedPath)
        If clickedIndex < 0 Then Return

        Dim ctrl As Boolean = (Control.ModifierKeys And Keys.Control) = Keys.Control
        Dim shift As Boolean = (Control.ModifierKeys And Keys.Shift) = Keys.Shift
        Dim baseSelection As New HashSet(Of String)(_selectedFiles, StringComparer.OrdinalIgnoreCase)
        Dim nextSelection As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        If shift AndAlso _lastAnchorIndex >= 0 Then
            Dim startIdx = Math.Min(_lastAnchorIndex, clickedIndex)
            Dim endIdx = Math.Max(_lastAnchorIndex, clickedIndex)
            If ctrl Then
                nextSelection.UnionWith(baseSelection)
            End If
            For i = startIdx To endIdx
                nextSelection.Add(_imagePaths(i))
            Next
        ElseIf ctrl Then
            nextSelection.UnionWith(baseSelection)
            If nextSelection.Contains(clickedPath) Then
                nextSelection.Remove(clickedPath)
            Else
                nextSelection.Add(clickedPath)
            End If
            _lastAnchorIndex = clickedIndex
        Else
            nextSelection.Add(clickedPath)
            _lastAnchorIndex = clickedIndex
        End If

        ApplySelectionSet(nextSelection)
        RaiseSelectionEvents()
    End Sub

    Private Sub GalleryControl_KeyDown(sender As Object, e As KeyEventArgs)
        If e Is Nothing Then Return

        If e.Control AndAlso e.KeyCode = Keys.A Then
            SelectAll()
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub SetCheckedByPath(path As String, isChecked As Boolean)
        If gcThumbs.Gallery.Groups.Count = 0 Then Return
        Dim group = gcThumbs.Gallery.Groups(0)
        For Each galleryItem As GalleryItem In group.Items
            If galleryItem.Tag IsNot Nothing AndAlso String.Equals(CStr(galleryItem.Tag), path, StringComparison.OrdinalIgnoreCase) Then
                galleryItem.Checked = isChecked
                Exit For
            End If
        Next
    End Sub

    Private Sub ClearCheckedItems()
        If gcThumbs.Gallery.Groups.Count = 0 Then Return
        Dim group = gcThumbs.Gallery.Groups(0)
        For Each galleryItem As GalleryItem In group.Items
            galleryItem.Checked = False
        Next
    End Sub

    Private Sub ApplySelectionSet(selectedPaths As HashSet(Of String))
        If selectedPaths Is Nothing Then selectedPaths = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        If gcThumbs.Gallery.Groups.Count = 0 Then Return
        Dim group = gcThumbs.Gallery.Groups(0)
        For Each galleryItem As GalleryItem In group.Items
            Dim p = TryCast(galleryItem.Tag, String)
            galleryItem.Checked = (Not String.IsNullOrWhiteSpace(p)) AndAlso selectedPaths.Contains(p)
        Next
        RefreshSelectionState()
    End Sub

    Private Sub RefreshSelectionState()
        _selectedFiles.Clear()
        _selectedIndices.Clear()

        If gcThumbs.Gallery.Groups.Count > 0 Then
            Dim group = gcThumbs.Gallery.Groups(0)
            For Each galleryItem As GalleryItem In group.Items
                If galleryItem.Checked AndAlso galleryItem.Tag IsNot Nothing Then
                    Dim p As String = CStr(galleryItem.Tag)
                    _selectedFiles.Add(p)
                    _selectedIndices.Add(_imagePaths.IndexOf(p))
                End If
            Next
        End If

        If _selectedFiles.Count > 0 Then
            _selectedFile = _selectedFiles(0)
            _selectedIndex = _selectedIndices(0)
            grpMain.Text = $"{_imagePaths.Count} Images - {_selectedFiles.Count} selected"
        Else
            _selectedFile = String.Empty
            _selectedIndex = -1
            grpMain.Text = $"{_imagePaths.Count} Images"
        End If
    End Sub

    Private Sub RaiseSelectionEvents()
        RaiseEvent PictureSelected(Me, New PictureSelectedEventArgs(_selectedFile, _selectedIndex))
        RaiseEvent PicturesSelectionChanged(Me, New PicturesSelectionChangedEventArgs(New List(Of String)(_selectedFiles), New List(Of Integer)(_selectedIndices)))
    End Sub

#End Region

#Region "Event args (shape compatible with existing)"

    Public Class PictureSelectedEventArgs
        Inherits EventArgs
        Public Property FileName As String
        Public Property Index As Integer
        Public Sub New(fileName As String, index As Integer)
            Me.FileName = fileName
            Me.Index = index
        End Sub
    End Class

    Public Event PicturesSelectionChanged(sender As Object, e As PicturesSelectionChangedEventArgs)

    Public Class PicturesSelectionChangedEventArgs
        Inherits EventArgs
        Public Property FileNames As List(Of String)
        Public Property Indices As List(Of Integer)
        Public Sub New(fileNames As List(Of String), indices As List(Of Integer))
            Me.FileNames = If(fileNames, New List(Of String))
            Me.Indices = If(indices, New List(Of Integer))
        End Sub
    End Class

#End Region

End Class
