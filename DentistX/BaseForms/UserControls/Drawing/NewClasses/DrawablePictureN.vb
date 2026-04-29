Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text
Imports System.IO
Imports System.Collections.Generic
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Linq

<Serializable()>
Public Class DrawablePictureN
    ' The list where we will store objects.
    <XmlElement(GetType(DrawableN)),
     XmlElement(GetType(DrawableArrowN)),
     XmlElement(GetType(DrawableEllipseN)),
     XmlElement(GetType(DrawableFreehandN)),
     XmlElement(GetType(DrawableImageN)),
     XmlElement(GetType(DrawableLineN)),
     XmlElement(GetType(DrawablePolylineN)),
     XmlElement(GetType(DrawableRectangleN)),
     XmlElement(GetType(DrawableStarN)),
     XmlElement(GetType(DrawableTextN)),
     XmlElement(GetType(DrawableGroupN))>
    Public Drawables As New List(Of DrawableN)


    ' The background color.
    <XmlIgnore> Public BackColor As Color = SystemColors.Control

    ' Property to serialize/deserialize BackColor
    <XmlAttribute("BackColor")>
    Public Property BackColorArgb() As Integer
        Get
            Return BackColor.ToArgb()
        End Get
        Set(Value As Integer)
            BackColor = Color.FromArgb(Value)
        End Set
    End Property

    ' Background image
    <XmlIgnore> Public BackgroundImage As Image = Nothing

    ' Property to serialize/deserialize background image
    <XmlElement("BackgroundImage")>
    Public Property BackgroundImageSerialized() As Byte()
        Get
            If BackgroundImage Is Nothing Then Return Nothing
            Return ImageBytesForPersistence(BackgroundImage)
        End Get
        Set(Value As Byte())
            If Value Is Nothing Then
                BackgroundImage = Nothing
            Else
                Using ms As New MemoryStream(Value)
                    BackgroundImage = Image.FromStream(ms)
                End Using
            End If
        End Set
    End Property

    ' Background image layout
    <XmlAttribute("BackgroundImageLayout")>
    Public Property BackgroundImageLayout As ImageLayout = ImageLayout.None

    ' Constructors
    Public Sub New()
    End Sub

    Public Sub New(background_color As Color)
        BackColor = background_color
    End Sub

    ' Currently selected object
    Private m_SelectedDrawable As DrawableN


    'Public Property GroupProxy As DrawableSelectionProxy
    'Public Sub UpdateGroupProxy()
    '    Dim selected = Drawables.Where(Function(d) d.IsSelected).ToList()
    '    If selected.Count = 0 Then
    '        GroupProxy = Nothing
    '    Else
    '        GroupProxy = New DrawableSelectionProxy With {
    '        .IsSelected = True,
    '        .SelectedDrawables = selected
    '    }
    '    End If
    'End Sub


    ''' <summary>Runtime selection only. Must not be XML-serialized: XmlSerializer duplicates graph nodes,
    ''' so embedding the same drawable here and in Drawables doubled image blobs and caused OOM on load.</summary>
    <XmlIgnore>
    Public Property SelectedDrawable() As DrawableN
        Get
            Return m_SelectedDrawable
        End Get
        Set(Value As DrawableN)
            If Value Is Nothing Then
                If m_SelectedDrawable IsNot Nothing Then
                    m_SelectedDrawable.IsSelected = False
                End If
                m_SelectedDrawable = Nothing
                Return
            End If

            If TypeOf Value Is DrawableSelectionProxy Then
                If TypeOf m_SelectedDrawable Is DrawableSelectionProxy Then
                    m_SelectedDrawable.IsSelected = False
                End If
                m_SelectedDrawable = Value
                Value.IsSelected = True
                Return
            End If

            If m_SelectedDrawable IsNot Nothing Then
                If TypeOf m_SelectedDrawable Is DrawableSelectionProxy Then
                    m_SelectedDrawable.IsSelected = False
                Else
                    m_SelectedDrawable.IsSelected = False
                End If
            End If
            m_SelectedDrawable = Value
            m_SelectedDrawable.IsSelected = True
        End Set
    End Property

    ' Add/remove objects
    Public Sub Add(new_drawable As DrawableN)
        Drawables.Add(new_drawable)
    End Sub

    Public Sub Remove(target As DrawableN)
        Drawables.Remove(target)
    End Sub

    ' Select object at point (with transformation support)
    Public Function SelectObjectAt(x As Integer, y As Integer) As DrawableN
        ' Deselect previous
        SelectedDrawable = Nothing

        ' Check from top to bottom (reverse order)
        For i As Integer = Drawables.Count - 1 To 0 Step -1
            Dim dr = Drawables(i)

            ' First check if we hit an anchor
            Dim anchor = dr.GetAnchorAt(x, y)
            If anchor <> AnchorEnumN.None Then
                SelectedDrawable = dr
                Return dr
            End If

            ' Transform point to object coordinates
            Dim transformedPoint = dr.InverseTransformPoint(New PointF(x, y))

            ' Check if point hits object
            If dr.IsAt(CInt(transformedPoint.X), CInt(transformedPoint.Y)) Then
                SelectedDrawable = dr
                Return dr
            End If
        Next

        Return Nothing
    End Function

    ' Draw all objects with proper transformation handling
    Public Sub Draw(gr As Graphics)
        ' Clear background
        gr.Clear(BackColor)

        ' Draw background image
        DrawBackground(gr)

        ' Set quality
        gr.SmoothingMode = SmoothingMode.AntiAlias

        Dim groupProxy = TryCast(SelectedDrawable, DrawableSelectionProxy)
        Dim useGroupProxy As Boolean = groupProxy IsNot Nothing AndAlso groupProxy.SelectedDrawables IsNot Nothing AndAlso groupProxy.SelectedDrawables.Count > 1
        Dim selectedState As New Dictionary(Of DrawableN, Boolean)()

        If useGroupProxy Then
            For Each dr As DrawableN In Drawables
                selectedState(dr) = dr.IsSelected
                dr.IsSelected = False
            Next
        End If

        ' Draw all objects with transformations
        For Each dr As DrawableN In Drawables
            ' Save state to isolate transformations
            Dim state As GraphicsState = gr.Save()

            ' Draw the object
            dr.Draw(gr)

            ' Restore graphics state
            gr.Restore(state)
        Next

        If useGroupProxy Then
            For Each kvp In selectedState
                kvp.Key.IsSelected = kvp.Value
            Next
        End If

        ' Draw selection overlays.
        If useGroupProxy Then
            groupProxy.Draw(gr)
            If groupProxy.IsTransforming Then
                groupProxy.DrawTransformationGuides(gr)
            End If
        Else
            For Each dr As DrawableN In Drawables
                If dr.IsSelected Then
                    dr.DrawAnchors(gr)
                    If dr.IsTransforming Then
                        Dim state As GraphicsState = gr.Save()
                        dr.DrawTransformationGuides(gr)
                        gr.Restore(state)
                    End If
                End If
            Next
        End If
    End Sub

    ' Draw background image with proper layout
    Private Sub DrawBackground(gr As Graphics)
        If BackgroundImage Is Nothing Then Return

        Select Case BackgroundImageLayout
            Case ImageLayout.None
                gr.DrawImage(BackgroundImage, 0, 0)

            Case ImageLayout.Tile
                Using brush As New TextureBrush(BackgroundImage)
                    gr.FillRectangle(brush, gr.VisibleClipBounds)
                End Using

            Case ImageLayout.Center
                Dim x = CInt((gr.VisibleClipBounds.Width - BackgroundImage.Width) / 2)
                Dim y = CInt((gr.VisibleClipBounds.Height - BackgroundImage.Height) / 2)
                gr.DrawImage(BackgroundImage, x, y)

            Case ImageLayout.Stretch
                gr.DrawImage(BackgroundImage, 0, 0,
                             gr.VisibleClipBounds.Width,
                             gr.VisibleClipBounds.Height)

            Case ImageLayout.Zoom
                Dim ratioX = gr.VisibleClipBounds.Width / BackgroundImage.Width
                Dim ratioY = gr.VisibleClipBounds.Height / BackgroundImage.Height
                Dim ratio = Math.Min(ratioX, ratioY)
                Dim newWidth = CInt(BackgroundImage.Width * ratio)
                Dim newHeight = CInt(BackgroundImage.Height * ratio)
                Dim x = CInt((gr.VisibleClipBounds.Width - newWidth) / 2)
                Dim y = CInt((gr.VisibleClipBounds.Height - newHeight) / 2)
                gr.DrawImage(BackgroundImage, x, y, newWidth, newHeight)
        End Select
    End Sub

    ' Z-order management
    Public Sub SendToBack(dr As DrawableN)
        If dr Is Nothing Then Return
        Dim p = TryCast(dr, DrawableSelectionProxy)
        If p IsNot Nothing Then
            For Each s In p.SelectedDrawables.OrderByDescending(Function(x) Drawables.IndexOf(x)).ToList()
                SendToBack(s)
            Next
            Return
        End If
        Drawables.Remove(dr)
        Drawables.Insert(0, dr)
    End Sub

    Public Sub BringToFront(dr As DrawableN)
        If dr Is Nothing Then Return
        Dim p = TryCast(dr, DrawableSelectionProxy)
        If p IsNot Nothing Then
            For Each s In p.SelectedDrawables.OrderBy(Function(x) Drawables.IndexOf(x)).ToList()
                BringToFront(s)
            Next
            Return
        End If
        Drawables.Remove(dr)
        Drawables.Add(dr)
    End Sub

    Public Sub Delete(dr As DrawableN)
        If dr Is Nothing Then Return
        Dim p = TryCast(dr, DrawableSelectionProxy)
        If p IsNot Nothing Then
            For Each s In p.SelectedDrawables.ToList()
                Drawables.Remove(s)
            Next
            SelectedDrawable = Nothing
            Return
        End If
        Drawables.Remove(dr)
        If m_SelectedDrawable Is dr Then m_SelectedDrawable = Nothing
    End Sub

    ' Serialization
    Public Sub SavePicture(file_name As String)
        Try
            m_SelectedDrawable = Nothing
            Dim serializer = New XmlSerializer(GetType(DrawablePictureN))
            Dim settings As New XmlWriterSettings() With {
                .Encoding = New UTF8Encoding(False),
                .Indent = True,
                .CloseOutput = True
            }
            Using writer = XmlWriter.Create(file_name, settings)
                serializer.Serialize(writer, Me)
            End Using
        Catch ex As Exception
            Dim sb As New StringBuilder()
            sb.AppendLine($"Error: {ex.Message}")

            Dim inner = ex.InnerException
            While inner IsNot Nothing
                sb.AppendLine($"Inner: {inner.Message}")
                inner = inner.InnerException
            End While
            sb = sb
            MessageBox.Show(sb.ToString(), "Serialization Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ' Call after deserialization
    Public Sub AfterLoad()
        m_SelectedDrawable = Nothing
        For Each item In Drawables
            item.AfterLoad()
        Next
    End Sub

    Public Function Clone() As DrawablePictureN
        Using ms As New IO.MemoryStream()
            Dim serializer As New Xml.Serialization.XmlSerializer(GetType(DrawablePictureN))
            serializer.Serialize(ms, Me)
            ms.Position = 0
            Return DirectCast(serializer.Deserialize(ms), DrawablePictureN)
        End Using
    End Function


    Public Shared Function LoadPicture(file_name As String) As DrawablePictureN
        Try
            Dim fi As New FileInfo(file_name)
            Const maxBytes As Long = 80 * 1024 * 1024
            If fi.Exists AndAlso fi.Length > maxBytes Then
                MessageBox.Show(
                    "This picture file is too large to load safely (" & (fi.Length \ (1024 * 1024)).ToString() & " MB). " &
                    "Try re-saving from a newer build after removing large backgrounds, or split the content.",
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return Nothing
            End If

            Dim serializer = New XmlSerializer(GetType(DrawablePictureN))
            Using stream = New FileStream(file_name, FileMode.Open)
                Dim picture = DirectCast(serializer.Deserialize(stream), DrawablePictureN)
                picture.AfterLoad() ' Call after-load initialization
                Return picture
            End Using
            'Dim serializer1 = New XmlSerializer(GetType(DrawablePictureN))
            'Using stream = New FileStream(file_name, FileMode.Open)
            '    Return DirectCast(serializer.Deserialize(stream), DrawablePictureN)
            'End Using

        Catch ex As Exception
            Dim errorMsg = $"Load error: {ex.Message}"
            If ex.InnerException IsNot Nothing Then
                errorMsg += $"{Environment.NewLine}Inner: {ex.InnerException.Message}"
                If ex.InnerException.InnerException IsNot Nothing Then
                    errorMsg += $"{Environment.NewLine}Inner2: {ex.InnerException.InnerException.Message}"
                End If
            End If
            MessageBox.Show(errorMsg, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    ' Get bounds of all objects
    Public Function GetBounds() As RectangleF
        If Drawables.Count = 0 Then Return New Rectangle(0, 0, 0, 0)

        Dim bounds = Drawables(0).GetTransformedBounds()

        For i = 1 To Drawables.Count - 1
            bounds = RectangleF.Union(bounds, Drawables(i).GetTransformedBounds())
        Next

        Return bounds
    End Function

    ' Get all objects that intersect with a rectangle
    Public Function GetObjectsInRect(rect As RectangleF) As List(Of DrawableN)
        Dim result As New List(Of DrawableN)

        For Each dr As DrawableN In Drawables
            Dim bounds = dr.GetTransformedBounds()
            If rect.IntersectsWith(bounds) Then
                result.Add(dr)
            End If
        Next

        Return result
    End Function

    Friend Sub ClearSelectio()
        For Each d In Drawables
            d.IsSelected = False
        Next
        m_SelectedDrawable = Nothing
    End Sub
End Class