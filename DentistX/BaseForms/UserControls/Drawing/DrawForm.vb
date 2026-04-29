Imports System.ComponentModel
Imports System.Drawing.Drawing2D
'Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text
Imports System.Linq
Imports System.Reflection
Imports System.Globalization
Imports System.Drawing.Text
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors.Repository

Partial Public Class DrawForm
    Private Shared _dicomInitialized As Boolean = False
    Private Shared _nativeCodecAvailable As Boolean = False

    'Private Shared Sub EnsureDicomInitialized()
    '    If _dicomInitialized Then Return
    '    Try
    '        Dim builder As New DicomSetupBuilder()
    '        builder.RegisterServices(Sub(services)
    '                                     services.AddFellowOakDicom()
    '                                     services.AddTranscoderManager(Of NativeTranscoderManager)()
    '                                     services.AddImageManager(Of WinFormsImageManager)()
    '                                 End Sub)
    '        builder.Build()
    '        _nativeCodecAvailable = True
    '    Catch
    '        _nativeCodecAvailable = False
    '    End Try
    '    _dicomInitialized = True
    'End Sub
    Public Sub New()
        InitializeComponent()
        m_BaseWindowTitle = Me.Text
        m_DefaultCanvasSize = picCanvas.ClientSize
        Me.KeyPreview = True
        ApplyArabicBoldUiIfNeeded()
        EnableSmoothCanvasRendering()
        EnsureStatusInfoItem()
        InitializeZoomEditor()
        InitializeFontMenu()
        UpdateCanvasSizePolicy()

        FillLineThicknessList()
        FillForeColorList()
        FillBackColorList()
        HeadLength = 100
        HeadWidth = 50
        ShaftWidthBase = 50
        ShaftWidthTip = 20
    End Sub

    Public Sub New(ByVal Dest As String)
        InitializeComponent()
        m_BaseWindowTitle = Me.Text
        m_DefaultCanvasSize = picCanvas.ClientSize
        Me.KeyPreview = True
        ApplyArabicBoldUiIfNeeded()
        EnableSmoothCanvasRendering()
        EnsureStatusInfoItem()
        InitializeZoomEditor()
        InitializeFontMenu()
        UpdateCanvasSizePolicy()

        FillLineThicknessList()
        FillForeColorList()
        FillBackColorList()
        HeadLength = 100
        HeadWidth = 50
        ShaftWidthBase = 50
        ShaftWidthTip = 20

        Dim img As Image = Nothing
        Dim loadError As String = ""
        If Not TryLoadImageFromPath(Dest, img, loadError) Then
            MessageBox.Show("Error loading image: " & loadError, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        picCanvas.AutoSize = False
        realSize = img.Size
        realWidth = img.Size.Width
        realHeight = img.Size.Height
        UpdateCanvasSizePolicy()
        m_Dest = Dest

        ' World-space bounds preserve pixel size; view is centered via RecenterViewOnDrawable.
        Dim rect As New Rectangle(0, 0, realWidth, realHeight)
        m_NewDrawableN = New DrawableImageN(rect.Left, rect.Top, rect.Right, rect.Bottom)
        If TypeOf m_NewDrawableN Is DrawableImageN Then
            Dim drawableImage As DrawableImageN = DirectCast(m_NewDrawableN, DrawableImageN)
            Try
                drawableImage.Picture = img
                m_NewDrawableN = drawableImage
                m_NewDrawableN.IsSelected = False
                passedImage = True
                m_PictureN.Add(m_NewDrawableN)
            Catch ex As Exception
                m_PictureN.Remove(m_NewDrawableN)
            End Try
        End If
        ' No longer "creating" — do not leave a reference or MouseUp will run the Image tool file dialog.
        m_NewDrawableN = Nothing
        isCreatingNewObject = False
        picCanvas.Invalidate()
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        ApplyInitialViewportForSingleImageDeferred()
    End Sub

    ''' <summary>
    ''' After layout, sets zoom to 100% and centers small images; zoom-to-fits large images within the canvas (via zoom track range).
    ''' </summary>
    Private Sub ApplyInitialViewportForSingleImageDeferred()
        If IsDisposed Then Return
        BeginInvoke(New MethodInvoker(AddressOf ApplyViewportForSingleLoadedImage))
    End Sub

    Private m_ApplyingProgrammaticZoom As Boolean

    ''' <summary>
    ''' Single DrawableImageN only: keep canvas at full panel size; small images at 100% centered;
    ''' larger images zoomed down to fit inside the viewport (trackbar percentage updated).
    ''' </summary>
    Private Sub ApplyViewportForSingleLoadedImage()
        If m_PictureN Is Nothing OrElse m_PictureN.Drawables Is Nothing Then Return
        If m_PictureN.Drawables.Count <> 1 Then Return
        Dim onlyImg = TryCast(m_PictureN.Drawables(0), DrawableImageN)
        If onlyImg Is Nothing OrElse onlyImg.Picture Is Nothing Then Return

        Dim cw = picCanvas.ClientSize.Width
        Dim ch = picCanvas.ClientSize.Height
        If cw < 2 OrElse ch < 2 Then Return

        Dim b = onlyImg.GetTransformedBounds()
        Dim iw = b.Width
        Dim ih = b.Height
        If iw < 1.0F OrElse ih < 1.0F Then Return

        Const margin As Single = 0.98F
        Dim availW = cw * margin
        Dim availH = ch * margin

        Dim pct As Integer
        If iw <= cw AndAlso ih <= ch Then
            pct = 100
        Else
            Dim fit = Math.Min(availW / iw, availH / ih)
            pct = CInt(Math.Round(fit * 100.0F))
            pct = Math.Max(CInt(RepositoryItemZoomTrackBar1.Minimum), Math.Min(pct, CInt(RepositoryItemZoomTrackBar1.Maximum)))
        End If

        m_ApplyingProgrammaticZoom = True
        Try
            m_ViewZoom = pct / 100.0F
            btnZoom.EditValue = CType(pct, Short)
            RecenterViewOnDrawable(onlyImg)
            UpdateAnchorSizeForView()
            UpdateStatusText()
        Finally
            m_ApplyingProgrammaticZoom = False
        End Try
    End Sub

    ''' <summary>
    ''' After a manual zoom change or form resize, recenters a lone image without changing zoom.
    ''' </summary>
    Private Sub RecenterSingleImageIfNeeded()
        If m_PictureN Is Nothing OrElse m_PictureN.Drawables Is Nothing Then Return
        If m_PictureN.Drawables.Count <> 1 Then Return
        Dim onlyImg = TryCast(m_PictureN.Drawables(0), DrawableImageN)
        If onlyImg Is Nothing OrElse onlyImg.Picture Is Nothing Then Return
        If picCanvas.ClientSize.Width < 1 OrElse picCanvas.ClientSize.Height < 1 Then Return
        RecenterViewOnDrawable(onlyImg)
    End Sub

    Public Function LoadImagesAsObjects(imagePaths As IEnumerable(Of String), Optional clearExisting As Boolean = True) As Integer
        If imagePaths Is Nothing Then Return 0

        If clearExisting Then
            m_PictureN = New DrawablePictureN(Me.BackColor)
            m_PictureN.SelectedDrawable = Nothing
            realSize = Size.Empty
            realWidth = 0
            realHeight = 0
            m_ViewZoom = 1.0F
            m_ViewOffset = PointF.Empty
            passedImage = False
        End If

        Dim loadedCount As Integer = 0
        Dim cursorX As Integer = 20
        Dim cursorY As Integer = 20
        Dim rowMaxHeight As Integer = 0
        Dim maxRight As Integer = 0
        Dim maxBottom As Integer = 0
        Dim spacing As Integer = 20
        Dim wrapWidth As Integer = Math.Max(600, picCanvas.ClientSize.Width - 40)

        For Each p In imagePaths
            If String.IsNullOrWhiteSpace(p) Then Continue For
            If Not File.Exists(p) Then Continue For

            Dim img As Image = Nothing
            Dim loadError As String = ""
            If Not TryLoadImageFromPath(p, img, loadError) OrElse img Is Nothing Then Continue For

            Dim imgW As Integer = Math.Max(1, img.Width)
            Dim imgH As Integer = Math.Max(1, img.Height)
            If cursorX > 20 AndAlso cursorX + imgW > wrapWidth Then
                cursorX = 20
                cursorY += rowMaxHeight + spacing
                rowMaxHeight = 0
            End If

            Dim rect As New Rectangle(cursorX, cursorY, imgW, imgH)
            Dim drawable As New DrawableImageN(rect.Left, rect.Top, rect.Right, rect.Bottom)
            Try
                drawable.Picture = img
                drawable.IsSelected = False
                m_PictureN.Add(drawable)
                passedImage = True
                loadedCount += 1

                rowMaxHeight = Math.Max(rowMaxHeight, imgH)
                maxRight = Math.Max(maxRight, rect.Right)
                maxBottom = Math.Max(maxBottom, rect.Bottom)
                cursorX += imgW + spacing
            Catch
                m_PictureN.Remove(drawable)
                img.Dispose()
            End Try
        Next

        If loadedCount > 0 Then
            realWidth = Math.Max(realWidth, maxRight)
            realHeight = Math.Max(realHeight, maxBottom)
            realSize = New Size(Math.Max(1, realWidth), Math.Max(1, realHeight))
            UpdateCanvasSizePolicy()
            If loadedCount > 1 Then
                NormalizeViewportAfterContentChange(False)
            End If
            UpdateStatusText()
            picCanvas.Invalidate()
        End If

        Return loadedCount
    End Function


#Region "Variables"
    Private passedImage As Boolean = False
    Private m_Dest As String = ""
    Dim realSize As Size '= picCanvas.Image.Size
    Dim realWidth As Integer '= picCanvas.Image.Width
    Dim realHeight As Integer '= picCanvas.Image.Height

    Private transformationController As New TransformationController()
    Private isCreatingNewObject As Boolean = False
    Private m_ViewZoom As Single = 1.0F
    Private m_ViewOffset As PointF = PointF.Empty
    Private m_SelectionProxy As DrawableSelectionProxy = Nothing
    Private m_BaseWindowTitle As String = ""
    Private m_StatusInfoItem As BarStaticItem = Nothing
    Private m_HintsInfoItem As BarStaticItem = Nothing
    ''' <summary>Width/height of the current selection; aligned right (LTR) or left (RTL).</summary>
    Private m_DimensionsInfoItem As BarStaticItem = Nothing
    Private m_FontMenuInitialized As Boolean = False
    Private m_DefaultCanvasSize As Size
    'Private WithEvents btnRotLeft As BarButtonItem
    'Private WithEvents btnRotRight As BarButtonItem


    'Cropping
    Private isCropping As Boolean = False
    Private cropStart As Point
    Private cropRect As Rectangle
    Private cropActionPending As String = "" ' "CropBackground", "KeepShapes", or "RemoveShapes"

    ' Holds the picture we are drawing.
    Private m_PictureN As New DrawablePictureN(Me.BackColor)
    ' The object we are currently drawing.
    Private m_NewDrawableN As DrawableN

    ' The current drawing attributes.
    Private m_CurrentAction As String = "Pointer"
    Private m_CurrentTxtInside As String = ""
    Private m_CurrentLineWidth As Integer = 1
    Private m_CurrentForeColor As Color = Color.Black
    Private m_CurrentFillColor As Color = Color.White
    Private m_CurrentFont As Font = Me.Font
    Private m_CurrentTextColor As Color = Color.Blue
    Private m_StarCorners As Integer = 5
    Private m_IsCreatingTextRect As Boolean
    ''' <summary>Overlay editor for DrawableTextN; DevExpress bar clicks often do not steal focus, so LostFocus alone is unreliable.</summary>
    Private m_ActiveTextEdit As TextBox
    ''' <summary>Font used only for txtTextEdit bar appearance; large canvas fonts must not inflate bar1.</summary>
    Private m_ToolbarTextAppearanceFont As Font

    ' The tool we have currently selected.
    Private m_SelectedToolButton As ToolBarButton

    ' The index of the first thickness image in its ImageList.
    Private m_FirstLineThicknessImage As Integer
    Private m_FirstLineColorImage As Integer
    Private m_FirstFillColorImage As Integer
    'Arrow Properties
    Property HeadLength As Integer
    Property HeadWidth As Integer
    Property ShaftWidthBase As Integer
    Property ShaftWidthTip As Integer
    '
    'Resizing Moving Scaling
    Private Enum TransformMode
        None
        Moving
        Rotating
        Scaling
        Resizing
    End Enum

    Private m_SelectedMouseX As Integer
    Private m_SelectedMouseY As Integer
    Private m_TransformMode As TransformMode = TransformMode.None
    Private m_TransformStartPoint As PointF
    Private m_OriginalRotation As Single
    Private m_OriginalScaleX As Single
    Private m_OriginalScaleY As Single
    Private m_ResizeHandleIndex As Integer = -1
    'Cropping
    Private cropAnchor As CropAnchors = CropAnchors.None
    Private cropHandleSize As Integer = 6
    Private Enum CropAnchors
        None
        Move
        TopLeft
        TopRight
        BottomLeft
        BottomRight
        Left
        Right
        Top
        Bottom
    End Enum

#End Region


    '#Region "Menu Items"

    '    Private Sub mnuFileNew_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileNew.ItemClick

    '    End Sub

    '    Private Sub mnuFileOpen_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileOpen.ItemClick

    '    End Sub

    '    Private Sub mnuFileSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileSave.ItemClick

    '    End Sub

    '    Private Sub mnuFileSaveImage_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileSaveImage.ItemClick

    '    End Sub

    '    Private Sub mnuFilePrintPreview_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFilePrintPreview.ItemClick

    '    End Sub

    '    Private Sub mnuFilePrint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFilePrint.ItemClick

    '    End Sub

    '    Private Sub mnuFileExit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileExit.ItemClick

    '    End Sub

    '    Private Sub mnuEditDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEditDelete.ItemClick

    '    End Sub

    '    Private Sub mnuFormatOrderBringToFront_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFormatOrderBringToFront.ItemClick

    '    End Sub

    '    Private Sub mnuFormatOrderSendToBack_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFormatOrderSendToBack.ItemClick

    '    End Sub

    '    Private Sub mnuOptSetBackColor_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuOptSetBackColor.ItemClick

    '    End Sub

    '    Private Sub mnuLoadBackground_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLoadBackground.ItemClick

    '    End Sub

    '    Private Sub mnuClearBackground_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuClearBackground.ItemClick

    '    End Sub

    '    Private Sub mnuLayoutNone_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLayoutNone.ItemClick

    '    End Sub

    '    Private Sub mnuLayoutTile_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLayoutTile.ItemClick

    '    End Sub

    '    Private Sub mnuLayoutStrech_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLayoutStrech.ItemClick

    '    End Sub

    '    Private Sub mnuLayoutCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLayoutCenter.ItemClick

    '    End Sub

    '    Private Sub mnuLayoutZoom_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLayoutZoom.ItemClick

    '    End Sub

    '    Private Sub mnuResetTransform_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuResetTransform.ItemClick

    '    End Sub

    '    Private Sub mnuFlipHorizontal_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFlipHorizontal.ItemClick

    '    End Sub

    '    Private Sub mnuFlipVertical_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFlipVertical.ItemClick

    '    End Sub




    '#End Region

#Region "Bar Items"


    Private Sub UnCheckButtons(ByVal links As DevExpress.XtraBars.BarItemLinkCollection)
        For Each link As DevExpress.XtraBars.BarItemLink In links
            If TypeOf link.Item Is DevExpress.XtraBars.BarButtonItem Then
                'link.Item.Enabled = False
                If link.Item.Name.StartsWith("tbtn") Then
                    Dim bt As DevExpress.XtraBars.BarButtonItem = TryCast(link.Item, DevExpress.XtraBars.BarButtonItem)
                    bt.Down = False
                End If
            End If
        Next
        Me.txtInside.Caption = "Text Inside : "
    End Sub
    Private Sub FillItemBranch(ByVal links As DevExpress.XtraBars.BarItemLinkCollection)
        'barManager1.ForceInitialize()
        ''s ForceInitialize method, before disabling any items
        'bar1.BeginUpdate()
        'Try
        '    FillItemBranch(bar1.ItemLinks)
        '    'For Each link As DevExpress.XtraBars.BarItemLink In Bar1.ItemLinks
        '    '    'For Each btnLink As DevExpress.XtraBars.BarButtonItem In link.Links 'here i am getting an exception which is expected but i have no idea how to accomplish it 
        '    '    link.Item.Enabled = True
        '    '    'Next
        '    'Next
        'Finally
        '    bar1.EndUpdate()
        'End Try
        For Each link As DevExpress.XtraBars.BarItemLink In links
            If TypeOf link.Item Is DevExpress.XtraBars.BarButtonItem Then
                link.Item.Enabled = False
            End If
            If TypeOf link.Item Is DevExpress.XtraBars.BarSubItem Then
                FillItemBranch(CType(link.Item, DevExpress.XtraBars.BarSubItem).ItemLinks)
            End If
        Next
    End Sub

    Private Sub tbtnPointer_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles tbtnPointer.ItemClick
        ActivatePointerTool()
    End Sub

    ''' <summary>Selects the pointer tool and syncs toolbar state.</summary>
    Private Sub ActivatePointerTool()
        CommitPendingTextEditingIfAny()
        UnCheckButtons(bar1.ItemLinks)
        tbtnPointer.Down = True
        m_CurrentAction = "Pointer"
    End Sub

    ''' <summary>Commits inline text editing without changing the active tool (caller sets the tool next).</summary>
    Private Sub CommitPendingTextEditingIfAny()
        If m_ActiveTextEdit Is Nothing OrElse m_ActiveTextEdit.IsDisposed Then
            m_ActiveTextEdit = Nothing
            Return
        End If
        FinishTextEditing(m_ActiveTextEdit, applyPointerTool:=False)
    End Sub

    Private Sub barManager1_ItemClick(sender As Object, e As ItemClickEventArgs) Handles barManager1.ItemClick
        ' Any bar/menu item (colors, thickness, etc.) should dismiss the overlay — focus often stays on the TextBox.
        CommitPendingTextEditingIfAny()
    End Sub

    Private Sub tbtnLine_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles tbtnLine.ItemClick
        CommitPendingTextEditingIfAny()
        UnCheckButtons(bar1.ItemLinks)
        tbtnLine.Down = True
        m_CurrentAction = "Line"
    End Sub

    Private Sub tbtnRectangle_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles tbtnRectangle.ItemClick
        CommitPendingTextEditingIfAny()
        UnCheckButtons(bar1.ItemLinks)
        tbtnRectangle.Down = True
        m_CurrentAction = "Rectangle"
        Me.txtInside.Caption = "Text Inside Rectangle : "
    End Sub

    Private Sub tbtnEllipse_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles tbtnEllipse.ItemClick
        CommitPendingTextEditingIfAny()
        UnCheckButtons(bar1.ItemLinks)
        tbtnEllipse.Down = True
        m_CurrentAction = "Ellipse"
        Me.txtInside.Caption = "Text Inside Ellipse : "
    End Sub

    Private Sub tbtnStar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles tbtnStar.ItemClick
        CommitPendingTextEditingIfAny()
        UnCheckButtons(bar1.ItemLinks)
        tbtnStar.Down = True
        m_CurrentAction = "Star"
        Me.txtInside.Caption = "Text Inside Star : "
    End Sub

    Private Sub tbtnImage_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles tbtnImage.ItemClick
        CommitPendingTextEditingIfAny()
        UnCheckButtons(bar1.ItemLinks)
        tbtnImage.Down = True
        m_CurrentAction = "Image"
    End Sub

    Private Sub tbtnFreehand_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles tbtnFreehand.ItemClick
        CommitPendingTextEditingIfAny()
        UnCheckButtons(bar1.ItemLinks)
        tbtnFreehand.Down = True
        m_CurrentAction = "FreeHand"
    End Sub

    Private Sub tbtnText_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles tbtnText.ItemClick
        CommitPendingTextEditingIfAny()
        UnCheckButtons(bar1.ItemLinks)
        tbtnText.Down = True
        m_CurrentAction = "Text"
    End Sub

    Private Sub tbtnArrow_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles tbtnArrow.ItemClick
        CommitPendingTextEditingIfAny()
        UnCheckButtons(bar1.ItemLinks)
        tbtnArrow.Down = True
        m_CurrentAction = "Arrow"
        Me.txtInside.Caption = "Text Inside Arrow : "
    End Sub

    Private Sub btnBringToFront_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnBringToFront.ItemClick
        CommitPendingTextEditingIfAny()
        UnCheckButtons(bar1.ItemLinks)
        btnBringToFront.Down = False
        m_CurrentAction = "BringToFront"
        m_PictureN.BringToFront(m_PictureN.SelectedDrawable)
        picCanvas.Invalidate()
    End Sub

    Private Sub btnSendToBack_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSendToBack.ItemClick
        CommitPendingTextEditingIfAny()
        UnCheckButtons(bar1.ItemLinks)
        btnSendToBack.Down = False
        m_CurrentAction = "SendToBack"
        m_PictureN.SendToBack(m_PictureN.SelectedDrawable)
        picCanvas.Invalidate()
    End Sub

    Private Sub btnDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        CommitPendingTextEditingIfAny()
        m_PictureN.Delete(m_PictureN.SelectedDrawable)
        picCanvas.Invalidate()
        ActivatePointerTool()
    End Sub

    Private Sub btnArrowProps_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnArrowProps.ItemClick
        Dim FR As New ArrowFrm With {.HeadLength = HeadLength, .HeadWidth = HeadWidth, .ShaftWidthBase = ShaftWidthBase, .ShaftWidthTip = ShaftWidthTip}
        If FR.ShowDialog = DialogResult.OK Then
            HeadLength = FR.HeadLength
            HeadWidth = FR.HeadWidth
            ShaftWidthBase = FR.ShaftWidthBase
            ShaftWidthTip = FR.ShaftWidthTip
            ' Update the selected object if there is one.
            If Not (m_PictureN.SelectedDrawable Is Nothing) Then

                If TypeOf m_PictureN.SelectedDrawable Is DrawableArrowN Then
                    Dim obj As New DrawableArrowN
                    obj = DirectCast(m_PictureN.SelectedDrawable, DrawableArrowN)
                    obj.HeadLength = HeadLength
                    obj.HeadWidth = HeadWidth
                    obj.ShaftWidthBase = ShaftWidthBase
                    obj.ShaftWidthTip = ShaftWidthTip
                    picCanvas.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#Region "Fill List Boxes"

    ''' <summary>Updates the bar text editor look without using the full canvas font size (large sizes inflate bar1).</summary>
    Private Sub ApplyToolbarTextEditAppearance()
        If txtTextEdit Is Nothing Then Return
        If m_ToolbarTextAppearanceFont IsNot Nothing Then
            m_ToolbarTextAppearanceFont.Dispose()
            m_ToolbarTextAppearanceFont = Nothing
        End If
        Const maxToolbarPt As Single = 11.0F
        Dim displaySize = CSng(Math.Min(m_CurrentFont.Size, maxToolbarPt))
        m_ToolbarTextAppearanceFont = New Font(m_CurrentFont.FontFamily, displaySize, m_CurrentFont.Style, GraphicsUnit.Point)
        txtTextEdit.Appearance.Font = m_ToolbarTextAppearanceFont
        txtTextEdit.Appearance.Options.UseFont = True
        txtTextEdit.Appearance.ForeColor = m_CurrentTextColor
        txtTextEdit.Appearance.Options.UseForeColor = True
    End Sub

    Private Sub txtText_EditValueChanged(sender As Object, e As EventArgs) Handles txtText.EditValueChanged
        If txtText.EditValue IsNot Nothing Then
            ApplyToolbarTextEditAppearance()
            m_CurrentTxtInside = txtText.EditValue.ToString
            ' Update the selected object if there is one.
            If Not (m_PictureN.SelectedDrawable Is Nothing) Then

                If TypeOf m_PictureN.SelectedDrawable Is DrawableArrowN Then
                    Dim obj As New DrawableArrowN
                    obj = DirectCast(m_PictureN.SelectedDrawable, DrawableArrowN)
                    obj.Text = m_CurrentTxtInside
                    obj.TextColor = m_CurrentTextColor
                    picCanvas.Invalidate()
                ElseIf TypeOf m_PictureN.SelectedDrawable Is DrawableEllipseN Then
                    Dim obj As New DrawableEllipseN
                    obj = DirectCast(m_PictureN.SelectedDrawable, DrawableEllipseN)
                    obj.Text = m_CurrentTxtInside
                    obj.TextColor = m_CurrentTextColor
                    picCanvas.Invalidate()
                ElseIf TypeOf m_PictureN.SelectedDrawable Is DrawableRectangleN Then
                    Dim obj As New DrawableRectangleN
                    obj = DirectCast(m_PictureN.SelectedDrawable, DrawableRectangleN)
                    obj.Text = m_CurrentTxtInside
                    obj.TextColor = m_CurrentTextColor
                    picCanvas.Invalidate()
                ElseIf TypeOf m_PictureN.SelectedDrawable Is DrawableStarN Then
                    Dim obj As New DrawableStarN
                    obj = DirectCast(m_PictureN.SelectedDrawable, DrawableStarN)
                    obj.Text = m_CurrentTxtInside
                    obj.TextColor = m_CurrentTextColor
                    picCanvas.Invalidate()
                End If

                picCanvas.Invalidate()
            End If
        End If
    End Sub

    Private Sub FillLineThicknessList()
        lstThickness.Items.Clear()

        ' Add items representing thickness 1 to 5
        For i As Integer = 1 To 5
            lstThickness.Items.Add(i)
        Next

        ' Optional: select default thickness
        lstThickness.SelectedIndex = 3
    End Sub
    Private Sub lstThickness_DrawItem(sender As Object, e As DevExpress.XtraEditors.ListBoxDrawItemEventArgs) Handles lstThickness.DrawItem
        If e.Index < 0 Then Return

        Dim thickness As Integer = CInt(lstThickness.Items(e.Index))

        ' Background
        Dim bgColor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected,
                              SystemColors.Highlight, SystemColors.Window)
        Using bgBrush As New SolidBrush(bgColor)
            e.Graphics.FillRectangle(bgBrush, e.Bounds)
        End Using

        ' Draw line
        Dim y As Integer = e.Bounds.Top + e.Bounds.Height \ 2
        Using pen As New Pen(Color.Black, thickness)
            pen.StartCap = Drawing2D.LineCap.Round
            pen.EndCap = Drawing2D.LineCap.Round
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            e.Graphics.DrawLine(pen, e.Bounds.Left + 4, y, e.Bounds.Right - 4, y)
        End Using
    End Sub

    Private Sub btnStarCorners_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnStarCorners.ItemClick

    End Sub

    Private Sub lstStarCorners_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstStarCorners.SelectedIndexChanged

        If lstStarCorners.SelectedIndex < 0 Then Exit Sub
        Dim selectedCorners As Integer = CType(lstStarCorners.SelectedItem, Integer)
        ' Example: use the color
        m_StarCorners = selectedCorners
        ' Update the selected object if there is one.
        If m_PictureN.SelectedDrawable Is Nothing OrElse
       Not TypeOf m_PictureN.SelectedDrawable Is DrawableStarN Then Return

        Dim starObj As DrawableStarN = DirectCast(m_PictureN.SelectedDrawable, DrawableStarN)
        starObj.PointsCount = m_StarCorners
        m_PictureN.SelectedDrawable = starObj
        picCanvas.Invalidate()
        'If Not (m_PictureN.SelectedDrawable Is Nothing) Then
        '    m_PictureN.SelectedDrawable.FillColor = m_CurrentFillColor

        'End If
    End Sub
    Private Sub lstThickness_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstThickness.SelectedIndexChanged
        If lstThickness.SelectedItem IsNot Nothing Then
            m_CurrentLineWidth = CInt(lstThickness.SelectedItem)

            ' Update the selected object if there is one.
            If Not (m_PictureN.SelectedDrawable Is Nothing) Then
                m_PictureN.SelectedDrawable.LineWidth = m_CurrentLineWidth
                picCanvas.Invalidate()
            End If
        End If
    End Sub


    Private Sub FillForeColorList()
        lstForeColor.ItemHeight = 20 ' Enough room for the colored bar
        lstForeColor.Items.Clear()

        ' Enumerate all known colors
        For Each knownClr As KnownColor In [Enum].GetValues(GetType(KnownColor))
            Dim clr As Color = Color.FromKnownColor(knownClr)
            lstForeColor.Items.Add(clr)
        Next


        ' Try to select Color.Blue
        For i As Integer = 0 To lstForeColor.Items.Count - 1
            If CType(lstForeColor.Items(i), Color).ToKnownColor = KnownColor.Blue Then
                lstForeColor.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub


    Private Sub lstForeColor_DrawItem(sender As Object, e As DevExpress.XtraEditors.ListBoxDrawItemEventArgs) Handles lstForeColor.DrawItem

        If e.Index < 0 Then Exit Sub

        Dim colorItem As Color = DirectCast(e.Item, Color)
        Dim g = e.Cache 'e.Graphics
        Dim bounds = e.Bounds

        ' Draw background
        e.Appearance.DrawBackground(g, bounds)

        ' Draw color bar (height = 5px, vertically centered)
        Dim barHeight As Integer = 15
        Dim yCenter = bounds.Top + (bounds.Height - barHeight) \ 2
        Dim barRect As New Rectangle(bounds.Left + 4, yCenter, bounds.Width - 8, barHeight)

        Using brush As New SolidBrush(colorItem)
            g.FillRectangle(brush, barRect)
        End Using

        ' Optional: draw border
        g.DrawRectangle(Pens.Black, barRect)

        e.Handled = True
    End Sub
    Private Sub lstForeColor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstForeColor.SelectedIndexChanged
        If lstForeColor.SelectedIndex < 0 Then Exit Sub
        Dim selectedColor As Color = DirectCast(lstForeColor.SelectedItem, Color)
        ' Example: use the color
        m_CurrentForeColor = selectedColor
        ' Update the selected object if there is one.
        If Not (m_PictureN.SelectedDrawable Is Nothing) Then
            m_PictureN.SelectedDrawable.ForeColor = m_CurrentForeColor
            picCanvas.Invalidate()
        End If
    End Sub


    Private Sub FillBackColorList()
        lstFillColor.ItemHeight = 30 ' Enough room for the colored bar
        lstFillColor.Items.Clear()


        ' Enumerate all known colors
        For Each knownClr As KnownColor In [Enum].GetValues(GetType(KnownColor))
            Dim clr As Color = Color.FromKnownColor(knownClr)
            lstFillColor.Items.Add(clr)
        Next


        ' Try to select Color.Blue
        For i As Integer = 0 To lstFillColor.Items.Count - 1
            If CType(lstFillColor.Items(i), Color).ToKnownColor = KnownColor.Transparent Then
                lstFillColor.SelectedIndex = i
                Exit For
            End If
        Next


        '        Dim topColors As Color() = {
        '    Color.Black, Color.White, Color.Red, Color.Green, Color.Blue,
        '    Color.Yellow, Color.Orange, Color.Purple, Color.Brown, Color.Gray
        '}

        '        For Each clr In topColors
        '            lstFillColor.Items.Add(clr)
        '        Next

        '        lstFillColor.SelectedIndex = 1
    End Sub


    Private Sub lstFillColor_DrawItem(sender As Object, e As DevExpress.XtraEditors.ListBoxDrawItemEventArgs) Handles lstFillColor.DrawItem

        If e.Index < 0 Then Exit Sub

        Dim colorItem As Color = DirectCast(e.Item, Color)
        Dim g = e.Cache 'e.Graphics
        Dim bounds = e.Bounds

        ' Draw background
        e.Appearance.DrawBackground(g, bounds)

        ' Draw color bar (height = 5px, vertically centered)
        Dim barHeight As Integer = 25
        Dim yCenter = bounds.Top + (bounds.Height - barHeight) \ 2
        Dim barRect As New Rectangle(bounds.Left + 4, yCenter, bounds.Width - 8, barHeight)

        Using brush As New SolidBrush(colorItem)
            g.FillRectangle(brush, barRect)
        End Using

        ' Optional: draw border
        g.DrawRectangle(Pens.Black, barRect)

        e.Handled = True
    End Sub
    Private Sub lstFillColor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFillColor.SelectedIndexChanged, lstStarCorners.SelectedIndexChanged
        If lstForeColor.SelectedIndex < 0 Then Exit Sub
        Dim selectedColor As Color = DirectCast(lstFillColor.SelectedItem, Color)
        ' Example: use the color
        m_CurrentFillColor = selectedColor
        ' Update the selected object if there is one.
        If Not (m_PictureN.SelectedDrawable Is Nothing) Then
            m_PictureN.SelectedDrawable.FillColor = m_CurrentFillColor
            picCanvas.Invalidate()
        End If
    End Sub

    Private Sub FontSelector_EditValueChanged(sender As Object, e As EventArgs) Handles FontSelector.EditValueChanged
        Dim selectedFont As Font = Nothing

        Dim fontEditSender = TryCast(sender, DevExpress.XtraEditors.FontEdit)
        If fontEditSender IsNot Nothing Then
            selectedFont = TryCast(fontEditSender.EditValue, Font)
        End If

        If selectedFont Is Nothing Then
            Dim activeFontEdit = TryCast(Me.ActiveControl, DevExpress.XtraEditors.FontEdit)
            If activeFontEdit IsNot Nothing Then
                selectedFont = TryCast(activeFontEdit.EditValue, Font)
            End If
        End If

        If selectedFont Is Nothing Then Return
        ApplySelectedFont(selectedFont)
    End Sub




#End Region


#Region "Form and Controls events"
    ' Setup

    ' Start a new picture.
    Private Sub mnuFileNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileNew.ItemClick
        ' See if the data is safe.
        '...

        ' Start the new picture.
        m_PictureN = New DrawablePictureN(Me.BackColor)
        realSize = Size.Empty
        realWidth = 0
        realHeight = 0
        m_ViewZoom = 1.0F
        m_ViewOffset = PointF.Empty
        NormalizeViewportAfterContentChange(False)
        picCanvas.Invalidate()
    End Sub

    ' Exit.
    Private Sub mnuFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileExit.ItemClick
        ' See if the data is safe.
        '...

        Me.Close()
    End Sub

    ' Save the drawing.
    Private Sub mnuFileSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileSave.ItemClick
        ' Let the user select the file to save into.
        If dlgSavePicture.ShowDialog() = DialogResult.OK Then
            m_PictureN.SavePicture(dlgSavePicture.FileName)
            dlgOpenPicture.InitialDirectory = dlgSavePicture.FileName
        End If
    End Sub

    ' Load a drawing.
    Private Sub mnuFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileOpen.ItemClick
        ' Let the user select the file to load.
        If dlgOpenPicture.ShowDialog() = DialogResult.OK Then
            ' Load the picture.
            Dim new_picture As DrawablePictureN =
                    DrawablePictureN.LoadPicture(dlgOpenPicture.FileName)

            ' If we succeeded, display the new picture.
            If Not (new_picture Is Nothing) Then
                m_PictureN = new_picture
                NormalizeViewportAfterContentChange(True)
                picCanvas.Invalidate()
            End If
            dlgSavePicture.InitialDirectory = dlgOpenPicture.FileName
        End If
    End Sub

    ' Bring the selected object to the front.
    Private Sub mnuFormatOrderBringToFront_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFormatOrderBringToFront.ItemClick
        m_PictureN.BringToFront(m_PictureN.SelectedDrawable)
        picCanvas.Invalidate()
    End Sub

    ' Set the picture's BackColor.
    Private Sub mnuOptSetBackColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOptSetBackColor.ItemClick
        If dlgBackColor.ShowDialog() = DialogResult.OK Then
            m_PictureN.BackColor = dlgBackColor.Color
            picCanvas.Invalidate()
        End If
    End Sub

    ' Send the selected object to the back.
    Private Sub mnuFormatOrderSendToBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFormatOrderSendToBack.ItemClick
        m_PictureN.SendToBack(m_PictureN.SelectedDrawable)
        picCanvas.Invalidate()
    End Sub

    ' Delete the selected object.
    Private Sub mnuEditDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditDelete.ItemClick
        CommitPendingTextEditingIfAny()
        m_PictureN.Delete(m_PictureN.SelectedDrawable)
        picCanvas.Invalidate()
        ActivatePointerTool()
    End Sub

    'Private Sub mnuLoadBackground_Click(sender As Object, e As EventArgs) Handles mnuLoadBackground.ItemClick
    '    Using openDialog As New OpenFileDialog
    '        openDialog.Filter = "Images and DICOM|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.dcm|All Files|*.*"
    '        If openDialog.ShowDialog() = DialogResult.OK Then
    '            Try
    '                LoadBackgroundFromPath(openDialog.FileName)
    '            Catch ex As Exception
    '                MessageBox.Show("Error loading image: " & ex.Message, "Error",
    '                            MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End Try
    '        End If
    '    End Using
    'Using openDialog As New OpenFileDialog
    '    openDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All Files|*.*"
    '    If openDialog.ShowDialog() = DialogResult.OK Then
    '        Try
    '            m_PictureN.BackgroundImage = Image.FromFile(openDialog.FileName)
    '            picCanvas.Invalidate()
    '        Catch ex As Exception
    '            MessageBox.Show("Error loading image: " & ex.Message, "Error",
    '                            MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    End If
    'End Using
    'End Sub
    Private Sub mnuClearBackground_Click(sender As Object, e As EventArgs) Handles mnuClearBackground.ItemClick
        m_PictureN.BackgroundImage = Nothing
        picCanvas.Invalidate()
    End Sub

    'Private Sub mnuFileOpenDicomFile_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuFileOpenDicomFile.ItemClick
    '    Using openDialog As New OpenFileDialog
    '        openDialog.Filter = "DICOM Files|*.*|All Files|*.*"
    '        openDialog.CheckFileExists = True
    '        If openDialog.ShowDialog() = DialogResult.OK Then
    '            Try
    '                LoadDicomBackgroundFromPath(openDialog.FileName)
    '            Catch ex As Exception
    '                MessageBox.Show("Error loading DICOM: " & ex.Message, "Error",
    '                                MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End Try
    '        End If
    '    End Using
    'End Sub

    'Private Sub mnuFileOpenDicomFolder_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuFileOpenDicomFolder.ItemClick
    '    Using folderDialog As New FolderBrowserDialog
    '        If folderDialog.ShowDialog() = DialogResult.OK Then
    '            Try
    '                Dim dicomPath = FindFirstDicomFile(folderDialog.SelectedPath)
    '                If String.IsNullOrWhiteSpace(dicomPath) Then
    '                    Throw New Exception("No DICOM files found in the selected folder.")
    '                End If
    '                LoadDicomBackgroundFromPath(dicomPath)
    '            Catch ex As Exception
    '                MessageBox.Show("Error loading DICOM folder: " & ex.Message, "Error",
    '                                MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End Try
    '        End If
    '    End Using
    'End Sub

    Private Sub mnuLayoutNone_Click(sender As Object, e As EventArgs) Handles mnuLayoutNone.ItemClick
        'Dim menuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        m_PictureN.BackgroundImageLayout = ImageLayout.None 'DirectCast(menuItem.Tag, ImageLayout)
        picCanvas.Invalidate()
    End Sub
    Private Sub mnuLayoutCenter_Click(sender As Object, e As EventArgs) Handles mnuLayoutCenter.ItemClick
        'Dim menuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        m_PictureN.BackgroundImageLayout = ImageLayout.Center 'DirectCast(menuItem.Tag, ImageLayout)
        picCanvas.Invalidate()
    End Sub
    Private Sub mnuLayoutStretch_Click(sender As Object, e As EventArgs) Handles mnuLayoutStrech.ItemClick
        'Dim menuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        m_PictureN.BackgroundImageLayout = ImageLayout.Stretch 'DirectCast(menuItem.Tag, ImageLayout)
        picCanvas.Invalidate()
    End Sub
    Private Sub mnuLayoutTile_Click(sender As Object, e As EventArgs) Handles mnuLayoutTile.ItemClick
        'Dim menuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        m_PictureN.BackgroundImageLayout = ImageLayout.Tile 'DirectCast(menuItem.Tag, ImageLayout)
        picCanvas.Invalidate()
    End Sub
    Private Sub mnuLayoutZoom_Click(sender As Object, e As EventArgs) Handles mnuLayoutZoom.ItemClick
        'Dim menuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        m_PictureN.BackgroundImageLayout = ImageLayout.Zoom 'DirectCast(menuItem.Tag, ImageLayout)
        picCanvas.Invalidate()
    End Sub

    Private Sub mnuFont_Click(sender As Object, e As EventArgs) Handles mnuFont.ItemClick
        If m_FontMenuInitialized AndAlso mnuFont.ItemLinks.Count > 0 Then Return

        If m_PictureN.SelectedDrawable Is Nothing OrElse
       Not TypeOf m_PictureN.SelectedDrawable Is DrawableTextN Then Return

        Dim textObj As DrawableTextN = DirectCast(m_PictureN.SelectedDrawable, DrawableTextN)

        Using dlgFont As New FontDialog()
            ' 🟢 Preload font and color from selected text object
            dlgFont.Font = textObj.Font
            dlgFont.Color = textObj.ForeColor
            dlgFont.ShowColor = True

            If dlgFont.ShowDialog() = DialogResult.OK Then
                ' Update font and color
                m_CurrentFont = dlgFont.Font
                m_CurrentTextColor = dlgFont.Color

                textObj.Font = m_CurrentFont
                textObj.ForeColor = m_CurrentTextColor

                ' Auto-fit new font to text box
                Using g = picCanvas.CreateGraphics()
                    textObj.AdjustSizeToText(g)
                End Using

                ' Optional UI update
                ApplyToolbarTextEditAppearance()
                picCanvas.Invalidate()
            End If
        End Using
    End Sub

    Private Sub mnuFont_Click2(sender As Object, e As EventArgs) 'Handles mnuFont.ItemClick
        Using dlgFont As New FontDialog()
            dlgFont.ShowColor = True

            If dlgFont.ShowDialog() = DialogResult.OK Then

                m_CurrentFont = dlgFont.Font
                m_CurrentTextColor = dlgFont.Color

                ' Update visual appearance
                ApplyToolbarTextEditAppearance()

                ' Update the selected text object
                If m_PictureN.SelectedDrawable IsNot Nothing AndAlso
               TypeOf m_PictureN.SelectedDrawable Is DrawableTextN Then

                    Dim textObj As DrawableTextN = DirectCast(m_PictureN.SelectedDrawable, DrawableTextN)

                    textObj.Font = m_CurrentFont
                    textObj.ForeColor = m_CurrentTextColor

                    ' Recalculate bounding box to fit new font
                    Using g = picCanvas.CreateGraphics()
                        textObj.AdjustSizeToText(g)
                    End Using

                    picCanvas.Invalidate()
                End If
            End If
        End Using
    End Sub

    Private Sub mnuFont_Click1(sender As Object, e As EventArgs) 'Handles mnuFont.ItemClick

        Using dlgFont As New FontDialog()
            'dlgFont.Font = textObj.Font
            'dlgFont.Color = textObj.ForeColor
            dlgFont.ShowColor = True
            If dlgFont.ShowDialog() = DialogResult.OK Then
                m_CurrentFont = dlgFont.Font
                m_CurrentTextColor = dlgFont.Color
                picCanvas.Invalidate()
            End If
        End Using
        ApplyToolbarTextEditAppearance()
        If m_PictureN.SelectedDrawable IsNot Nothing AndAlso
           TypeOf m_PictureN.SelectedDrawable Is DrawableTextN Then
            Dim textObj As DrawableTextN = DirectCast(m_PictureN.SelectedDrawable, DrawableTextN)
            textObj.Font = m_CurrentFont
        End If
    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) 'Handles ToolBar1.ButtonClick
        ' See what kind of button this is.
        If e.Button.Style = ToolBarButtonStyle.ToggleButton Then
            ' ToggleButton.
            If (m_SelectedToolButton Is e.Button) Then
                e.Button.Pushed = True
            Else
                ' Pop the previously selected button up.
                m_SelectedToolButton.Pushed = False

                ' Save a reference to the selected tool.
                m_SelectedToolButton = e.Button
            End If

            ' Deselect any selected Drawable.
            If Not (m_PictureN.SelectedDrawable Is Nothing) Then
                m_PictureN.SelectedDrawable = Nothing
                picCanvas.Invalidate()
            End If
        ElseIf e.Button.Style = ToolBarButtonStyle.PushButton Then
            ' PushButton.
            Select Case e.Button.ToolTipText
                Case "Bring To Front"
                    m_PictureN.BringToFront(m_PictureN.SelectedDrawable)
                    picCanvas.Invalidate()
                Case "Send To Back"
                    m_PictureN.SendToBack(m_PictureN.SelectedDrawable)
                    picCanvas.Invalidate()
                Case "Delete"
                    m_PictureN.Delete(m_PictureN.SelectedDrawable)
                    picCanvas.Invalidate()
            End Select
        End If
    End Sub




#End Region


#Region "New object event handlers"

    Private Sub picCanvas_KeyDown(sender As Object, e As KeyEventArgs) Handles picCanvas.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Z Then Undo()
        If e.Control AndAlso e.KeyCode = Keys.Y Then Redo()
        If e.Control AndAlso e.KeyCode = Keys.A Then
            e.Handled = True
            e.SuppressKeyPress = True
            SelectAllDrawables()
        End If
        If e.KeyCode = Keys.F2 AndAlso m_CurrentAction = "Pointer" AndAlso m_PictureN IsNot Nothing Then
            Dim textObj = TryCast(m_PictureN.SelectedDrawable, DrawableTextN)
            If textObj IsNot Nothing Then
                CommitPendingTextEditingIfAny()
                StartTextEditing(textObj)
                e.Handled = True
                e.SuppressKeyPress = True
            End If
        End If
    End Sub

    Private Sub picCanvas_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseDoubleClick
        If e.Button <> MouseButtons.Left Then Return
        If m_CurrentAction <> "Pointer" OrElse m_PictureN Is Nothing Then Return
        CommitPendingTextEditingIfAny()
        transformationController.EndTransform()
        Dim worldPt = ScreenToWorld(e.Location)
        Dim hit = FindTopDrawableAt(worldPt.X, worldPt.Y)
        Dim textObj = TryCast(hit, DrawableTextN)
        If textObj Is Nothing Then Return
        ClearSelection()
        textObj.IsSelected = True
        SyncSelectedDrawable()
        StartTextEditing(textObj)
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.A) Then
            SelectAllDrawables()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function



    ' Redraw.
    Private Sub picCanvas_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picCanvas.Paint
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality
        UpdateAnchorSizeForView()
        If m_PictureN IsNot Nothing Then
            Dim state = e.Graphics.Save()
            e.Graphics.TranslateTransform(m_ViewOffset.X, m_ViewOffset.Y)
            e.Graphics.ScaleTransform(m_ViewZoom, m_ViewZoom)
            m_PictureN.Draw(e.Graphics)
            e.Graphics.Restore(state)
        End If

        If isSelecting AndAlso selectionRect.Width > 0 AndAlso selectionRect.Height > 0 Then
            Using selectionPen As New Pen(Color.DodgerBlue, 1)
                selectionPen.DashStyle = DashStyle.Dash
                e.Graphics.DrawRectangle(selectionPen, selectionRect)
            End Using
        End If

    End Sub

#Region "Varibles for selecting more than one shape"
    Private isSelecting As Boolean = False
    Private selectionStart As Point
    Private selectionRect As Rectangle
    Private marqueeAdditive As Boolean

    Private isDragging As Boolean = False
    Private dragStartPoint As Point
    Private dragStart As Point



#End Region
    ' Perform an action depending on the currently pushed tool.
    Private Sub picCanvas_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCanvas.MouseDown
        UpdateAnchorSizeForView()
        ' See which button was pressed.
        If e.Button = MouseButtons.Right Then
            ' Right button. See if we're drawing something.
            If m_NewDrawableN Is Nothing Then
                ' We are not drawing. Ignore this button.
            Else
                ' We are drawing something. Cancel it.
                m_PictureN.Remove(m_NewDrawableN)
                m_NewDrawableN = Nothing
                ' Redraw to erase the new object.
                picCanvas.Invalidate()
            End If
        Else

            ' Left button. See which tool is pushed.
            Select Case m_CurrentAction  'm_SelectedToolButton.ToolTipText
                Case "Pointer"
                    ' Reset new object flag
                    isCreatingNewObject = False
                    Dim worldPt As Point = ScreenToWorld(e.Location)
                    Dim additive As Boolean = IsAdditiveSelectionModifier()

                    ' Alt+drag: marquee selection starting anywhere (even over shapes). Ctrl/Shift+Alt: add to selection on release.
                    If (Control.ModifierKeys And Keys.Alt) = Keys.Alt Then
                        If Not additive Then ClearSelection()
                        isSelecting = True
                        marqueeAdditive = additive
                        selectionStart = e.Location
                        selectionRect = New Rectangle(e.X, e.Y, 0, 0)
                        Exit Select
                    End If

                    If m_PictureN.SelectedDrawable IsNot Nothing Then
                        Dim selectedAnchor = GetAnchorAtWithTolerance(m_PictureN.SelectedDrawable, worldPt)
                        If selectedAnchor <> AnchorEnumN.None Then
                            transformationController.StartTransform(m_PictureN.SelectedDrawable, selectedAnchor, worldPt)
                            Exit Select
                        End If

                        ' Keep group selection stable: dragging inside group box moves whole selection
                        ' instead of reselecting a single object under the cursor.
                        If m_SelectionProxy IsNot Nothing AndAlso Object.ReferenceEquals(m_PictureN.SelectedDrawable, m_SelectionProxy) Then
                            If Not additive AndAlso m_SelectionProxy.IsAt(worldPt.X, worldPt.Y) AndAlso IsPointOnAnySelectedDrawable(worldPt) Then
                                transformationController.StartTransform(m_SelectionProxy, AnchorEnumN.Move, worldPt)
                                Exit Select
                            End If
                        End If
                    End If

                    Dim hit = FindTopDrawableAt(worldPt.X, worldPt.Y)
                    If hit Is Nothing Then
                        If Not additive Then ClearSelection()
                        isSelecting = True
                        marqueeAdditive = additive
                        selectionStart = e.Location
                        selectionRect = New Rectangle(e.X, e.Y, 0, 0)
                    Else
                        Dim hitOnAnchor = GetAnchorAtWithTolerance(hit, worldPt) <> AnchorEnumN.None
                        If additive AndAlso Not hitOnAnchor Then
                            hit.IsSelected = Not hit.IsSelected
                        Else
                            ClearSelection()
                            hit.IsSelected = True
                        End If

                        SyncSelectedDrawable()

                        Dim shouldStartTransform = (Not additive) OrElse hitOnAnchor
                        If shouldStartTransform AndAlso m_PictureN.SelectedDrawable IsNot Nothing Then
                            Dim anchor = GetAnchorAtWithTolerance(m_PictureN.SelectedDrawable, worldPt)
                            If anchor = AnchorEnumN.None AndAlso m_PictureN.SelectedDrawable.IsAt(worldPt.X, worldPt.Y) Then
                                anchor = AnchorEnumN.Move
                            End If
                            If anchor <> AnchorEnumN.None Then
                                transformationController.StartTransform(m_PictureN.SelectedDrawable, anchor, worldPt)
                            End If
                        End If
                    End If

                Case "PolyLine"
                    ' Start drawing a line.
                    isCreatingNewObject = True
                    Dim p = ScreenToWorld(e.Location)
                    m_NewDrawableN = New DrawablePolylineN(m_CurrentForeColor, m_CurrentLineWidth, p.X, p.Y, p.X, p.Y)
                    m_PictureN.Add(m_NewDrawableN)
                Case "Arrow"
                    ' Start drawing a line.
                    isCreatingNewObject = True
                    Dim p = ScreenToWorld(e.Location)
                    m_NewDrawableN = New DrawableArrowN(m_CurrentForeColor, m_CurrentFillColor, m_CurrentLineWidth, p.X, p.Y, p.X, p.Y) With {
                                                                                                    .Text = m_CurrentTxtInside,
                                                                                                    .TextColor = m_CurrentTextColor,
                                                                                                    .TextFont = m_CurrentFont,
                                                                                                    .TextAlignment = ContentAlignment.MiddleCenter,
                                                                                                    .HeadLength = HeadLength, .HeadWidth = HeadWidth,
                                                                                                    .ShaftWidthBase = ShaftWidthBase, .ShaftWidthTip = ShaftWidthTip
                                                                                                }
                    m_PictureN.Add(m_NewDrawableN)
                Case "Line"
                    ' Start drawing a line.
                    isCreatingNewObject = True
                    Dim p = ScreenToWorld(e.Location)
                    m_NewDrawableN = New DrawableLineN(m_CurrentForeColor, m_CurrentLineWidth, p.X, p.Y, p.X, p.Y)
                    m_PictureN.Add(m_NewDrawableN)
                Case "Rectangle"
                    ' Start drawing a rectangle.
                    isCreatingNewObject = True
                    Dim p = ScreenToWorld(e.Location)
                    m_NewDrawableN = New DrawableRectangleN(m_CurrentForeColor, m_CurrentFillColor, m_CurrentLineWidth, p.X, p.Y, p.X, p.Y) With {
                                                                                                    .Text = m_CurrentTxtInside,
                                                                                                    .TextColor = m_CurrentTextColor,
                                                                                                    .TextFont = m_CurrentFont,
                                                                                                    .TextAlignment = ContentAlignment.MiddleCenter
                                                                                                }
                    m_PictureN.Add(m_NewDrawableN)
                Case "Ellipse"
                    ' Start drawing an ellipse.
                    isCreatingNewObject = True
                    Dim p = ScreenToWorld(e.Location)
                    m_NewDrawableN = New DrawableEllipseN(m_CurrentForeColor, m_CurrentFillColor, m_CurrentLineWidth, p.X, p.Y, p.X, p.Y) With {
                                                                                                        .Text = m_CurrentTxtInside,
                                                                                                        .TextColor = m_CurrentTextColor,
                                                                                                        .TextFont = m_CurrentFont,
                                                                                                        .TextAlignment = ContentAlignment.MiddleCenter,
                                                                                                        .RotationAngle = 0
                                                                                                    }
                    m_PictureN.Add(m_NewDrawableN)
                Case "Star"
                    ' Start drawing a star.
                    isCreatingNewObject = True
                    Dim p = ScreenToWorld(e.Location)
                    m_NewDrawableN = New DrawableStarN(m_CurrentForeColor, m_CurrentFillColor, m_CurrentLineWidth, p.X, p.Y, p.X, p.Y) With {.PointsCount = m_StarCorners,
                                                                                                        .Text = m_CurrentTxtInside,
                                                                                                            .TextColor = m_CurrentTextColor,
                                                                                                            .TextFont = m_CurrentFont,
                                                                                                            .TextAlignment = ContentAlignment.MiddleCenter,
                                                                                                            .RotationAngle = 0
                                                                                                        }
                    m_PictureN.Add(m_NewDrawableN)
                Case "Image"
                    ' Start drawing an image.
                    isCreatingNewObject = True
                    Dim p = ScreenToWorld(e.Location)
                    m_NewDrawableN = New DrawableImageN(p.X, p.Y, p.X, p.Y)
                    m_PictureN.Add(m_NewDrawableN)
                Case "FreeHand"
                    ' Start freehand drawing
                    isCreatingNewObject = True
                    m_NewDrawableN = New DrawableFreehandN(m_CurrentForeColor, m_CurrentLineWidth)
                    ' Add initial point
                    DirectCast(m_NewDrawableN, DrawableFreehandN).AddPoint(ScreenToWorld(e.Location))
                    m_PictureN.Add(m_NewDrawableN)
                Case "Text"
                    ' Drag to define rectangle; MouseUp opens the editor (see picCanvas_MouseUp).
                    Dim p = ScreenToWorld(e.Location)
                    m_NewDrawableN = New DrawableTextN(m_CurrentForeColor,
                                      m_CurrentFont,
                                       "",
                                       p.X, p.Y, p.X, p.Y) ' Initialize with same point
                    m_IsCreatingTextRect = True
                    m_PictureN.Add(m_NewDrawableN)
                    picCanvas.Invalidate()
                Case "KeepShapes"
                    cropActionPending = "KeepShapes"
                    cropStart = e.Location
                    isCropping = True

                Case "RemoveShapes"
                    cropActionPending = "RemoveShapes"
                    cropStart = e.Location
                    isCropping = True

            End Select

            ' Redraw.
            picCanvas.Invalidate()
        End If
    End Sub

    ' Handle mouse movement for transformations and object creation
    Private Sub picCanvas_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCanvas.MouseMove
        Dim worldPt As Point = ScreenToWorld(e.Location)
        ' Marquee / transforms / object creation must not be throttled
        If isSelecting Then
            Dim x = Math.Min(selectionStart.X, e.X)
            Dim y = Math.Min(selectionStart.Y, e.Y)
            Dim w = Math.Abs(selectionStart.X - e.X)
            Dim h = Math.Abs(selectionStart.Y - e.Y)
            selectionRect = New Rectangle(x, y, w, h)
            picCanvas.Invalidate()
            Return
        End If

        Static lastHoverThrottle As DateTime = DateTime.MinValue
        If e.Button = MouseButtons.None AndAlso
           Not transformationController.IsTransforming AndAlso
           Not isCreatingNewObject AndAlso
           Not m_IsCreatingTextRect Then
            If (DateTime.Now - lastHoverThrottle).TotalMilliseconds < 20 Then Return
            lastHoverThrottle = DateTime.Now
        End If
        ' Handle transformations
        If e.Button = MouseButtons.Left AndAlso transformationController.IsTransforming AndAlso m_PictureN.SelectedDrawable IsNot Nothing Then
            transformationController.UpdateTransform(worldPt)
            picCanvas.Invalidate()
            'ElseIf m_SelectedToolButton.ToolTipText = "Pointer" AndAlso m_PictureN.SelectedDrawable IsNot Nothing Then
        ElseIf m_CurrentAction = "Pointer" AndAlso m_PictureN.SelectedDrawable IsNot Nothing Then
            'Update cursor based on anchor
            Dim anchor = GetAnchorAtWithTolerance(m_PictureN.SelectedDrawable, worldPt)
            picCanvas.Cursor = GetAnchorCursor(anchor)
        End If
        ' Handle new object creation
        If isCreatingNewObject AndAlso m_NewDrawableN IsNot Nothing AndAlso e.Button = MouseButtons.Left Then
            Dim newX As Integer = worldPt.X
            Dim newY As Integer = worldPt.Y
            If TypeOf m_NewDrawableN Is DrawableFreehandN Then
                m_NewDrawableN.NewPoint(newX, newY)
            Else
                ' Get the starting point
                Dim startX As Integer = m_NewDrawableN.X1
                Dim startY As Integer = m_NewDrawableN.Y1
                ' Handle different constraint types
                If TypeOf m_NewDrawableN Is DrawableLineN AndAlso Control.ModifierKeys = Keys.Shift Then
                    ' Line angle constraint (15-degree increments)
                    Dim angle = Math.Atan2(newY - startY, newX - startX) * (180 / Math.PI)
                    Dim constrainedAngle = Math.Round(angle / 15) * 15
                    Dim radians = constrainedAngle * Math.PI / 180
                    Dim length = Geometry.Distance(New Point(startX, startY), New Point(newX, newY))
                    newX = startX + CInt(length * Math.Cos(radians))
                    newY = startY + CInt(length * Math.Sin(radians))
                ElseIf (TypeOf m_NewDrawableN Is DrawableRectangleN OrElse
                    TypeOf m_NewDrawableN Is DrawableEllipseN) AndAlso
                    Control.ModifierKeys = Keys.Shift Then
                    ' Aspect ratio constraint (square/circle)
                    Dim size = Math.Max(Math.Abs(newX - startX), Math.Abs(newY - startY))
                    newX = If(newX > startX, startX + size, startX - size)
                    newY = If(newY > startY, startY + size, startY - size)
                ElseIf Control.ModifierKeys = Keys.Shift Then
                    ' Default constraint (horizontal/vertical)
                    Dim dx = newX - startX
                    Dim dy = newY - startY
                    If Math.Abs(dx) > Math.Abs(dy) Then
                        newY = startY ' Horizontal constraint
                    Else
                        newX = startX ' Vertical constraint
                    End If
                End If
                ' Update the point with potential constraints
                m_NewDrawableN.NewPoint(newX, newY)
            End If
            ' Redraw to show updates
            picCanvas.Invalidate()
        End If
        ' Handle new text object creation
        If m_IsCreatingTextRect AndAlso m_NewDrawableN IsNot Nothing Then
            ' Update text rectangle bounds
            Dim textObj = DirectCast(m_NewDrawableN, DrawableTextN)
            textObj.X2 = worldPt.X
            textObj.Y2 = worldPt.Y
            picCanvas.Invalidate()
        End If

        ' Update cursor for pointer tool
        If Not transformationController.IsTransforming AndAlso m_CurrentAction = "Pointer" Then
            If m_PictureN.SelectedDrawable IsNot Nothing Then
                Dim anchor = GetAnchorAtWithTolerance(m_PictureN.SelectedDrawable, worldPt)
                picCanvas.Cursor = GetAnchorCursor(anchor)
            Else
                picCanvas.Cursor = Cursors.Default
            End If
        End If

    End Sub

    Private Sub picCanvas_MouseWheel(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseWheel
        If (Control.ModifierKeys And Keys.Shift) <> Keys.Shift Then Return

        Dim stepValue As Integer = 10
        Dim currentValue As Integer = 100
        Integer.TryParse(Convert.ToString(btnZoom.EditValue), currentValue)

        Dim deltaSteps As Integer = If(e.Delta > 0, 1, -1)
        Dim newValue As Integer = currentValue + (deltaSteps * stepValue)
        newValue = Math.Max(RepositoryItemZoomTrackBar1.Minimum, Math.Min(newValue, RepositoryItemZoomTrackBar1.Maximum))

        If newValue <> currentValue Then
            btnZoom.EditValue = CType(newValue, Short)
        End If
    End Sub

    '' Finish transformations or object creation
    Private Sub picCanvas_MouseUp(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseUp
        If isSelecting Then
            isSelecting = False
            SelectByMarquee(selectionRect, marqueeAdditive)
            selectionRect = Rectangle.Empty
        End If

        ' Handle text rectangle creation FIRST
        If m_IsCreatingTextRect Then
            m_IsCreatingTextRect = False
            ' Null check before casting
            If m_NewDrawableN IsNot Nothing AndAlso TypeOf m_NewDrawableN Is DrawableTextN Then
                Dim textObj = DirectCast(m_NewDrawableN, DrawableTextN)
                ' Ensure minimum size
                If Math.Abs(textObj.X2 - textObj.X1) < 10 Then textObj.X2 = textObj.X1 + 100
                If Math.Abs(textObj.Y2 - textObj.Y1) < 10 Then textObj.Y2 = textObj.Y1 + 30
                StartTextEditing(textObj)
            Else
                ' Clean up invalid object
                If m_NewDrawableN IsNot Nothing Then
                    m_PictureN.Remove(m_NewDrawableN)
                End If
            End If
            ' Prevent general cleanup from clearing text object reference
            isCreatingNewObject = False
        End If
        ' Handle image creation AFTER text handling (Image tool only: isCreatingNewObject is set on MouseDown).
        ' Programmatic loads (constructor, crop, etc.) must clear m_NewDrawableN so clicks do not open the dialog.
        If isCreatingNewObject AndAlso TypeOf m_NewDrawableN Is DrawableImageN Then
            Dim drawableImage As DrawableImageN = DirectCast(m_NewDrawableN, DrawableImageN)
            If ofdImage.ShowDialog() = DialogResult.OK Then
                Try
                    drawableImage.Picture = New Bitmap(ofdImage.FileName)
                Catch ex As Exception
                    m_PictureN.Remove(m_NewDrawableN)
                End Try
            Else
                m_PictureN.Remove(m_NewDrawableN)
            End If
        End If
        ' Finalize transformations
        transformationController.EndTransform()
        ' Clean up ONLY if still in creation mode
        If isCreatingNewObject Then
            isCreatingNewObject = False
            m_NewDrawableN = Nothing
        End If


        picCanvas.Invalidate()
    End Sub

    Private Function GetAnchorCursor(anchor As AnchorEnumN) As Cursor
        Select Case anchor
            Case AnchorEnumN.Move
                Return Cursors.SizeAll
            Case AnchorEnumN.Nwse, AnchorEnumN.Senw
                Return Cursors.SizeNWSE
            Case AnchorEnumN.Nesw, AnchorEnumN.Swne
                Return Cursors.SizeNESW
            Case AnchorEnumN.Ns, AnchorEnumN.Sn
                Return Cursors.SizeNS
            Case AnchorEnumN.Ew, AnchorEnumN.We
                Return Cursors.SizeWE
            Case AnchorEnumN.Rotate
                Return Cursors.Cross
            Case Else
                Return Cursors.Default
        End Select
    End Function


    ' On mouse move, continue drawing new objects
    Private Sub NewDrawable_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        ' This is now handled in the main MouseMove event
    End Sub
    ' On mouse up, finish drawing the new object.
    Private Sub NewDrawable_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        ' No longer watch for MouseMove or MouseUp.
        ' This is now handled in the main MouseUp event
    End Sub


#End Region ' New object event handlers

#Region "Text"
    Private Sub StartTextEditing(textObj As DrawableTextN)
        CommitPendingTextEditingIfAny()

        Dim txtEdit As New TextBox()
        txtEdit.Multiline = True
        txtEdit.WordWrap = True
        txtEdit.ScrollBars = ScrollBars.Vertical
        txtEdit.Font = textObj.Font
        txtEdit.ForeColor = textObj.ForeColor
        txtEdit.Text = textObj.Text

        txtEdit.BorderStyle = BorderStyle.FixedSingle
        txtEdit.Tag = textObj
        Dim bounds = textObj.GetBounds()
        If bounds.Width <= 0 Then bounds.Width = 100
        If bounds.Height <= 0 Then bounds.Height = 30
        txtEdit.Bounds = WorldToScreenRect(bounds)

        AddHandler txtEdit.KeyDown, AddressOf TextEdit_KeyDown
        AddHandler txtEdit.LostFocus, AddressOf TextEdit_LostFocus
        AddHandler txtEdit.MouseWheel, AddressOf TextEdit_MouseWheel

        m_ActiveTextEdit = txtEdit
        picCanvas.Controls.Add(txtEdit)
        txtEdit.BringToFront()
        txtEdit.Focus()
    End Sub

    Private Sub TextEdit_MouseWheel(sender As Object, e As MouseEventArgs)
        If (Control.ModifierKeys And Keys.Control) <> Keys.Control Then Return
        Dim tb = DirectCast(sender, TextBox)
        AdjustTextBoxFontSize(tb, If(e.Delta > 0, 1.0F, -1.0F))
    End Sub

    Private Sub AdjustTextBoxFontSize(tb As TextBox, delta As Single)
        Dim oldFont = tb.Font
        Dim newSize = CSng(Math.Max(6.0F, Math.Min(120.0F, oldFont.Size + delta)))
        If Math.Abs(newSize - oldFont.Size) < 0.01F Then Return
        tb.Font = New Font(oldFont.FontFamily, newSize, oldFont.Style, GraphicsUnit.Point)
    End Sub

    Private Sub TextEdit_KeyDown(sender As Object, e As KeyEventArgs)
        Dim tb = DirectCast(sender, TextBox)
        If e.Control AndAlso (e.KeyCode = Keys.Add OrElse e.KeyCode = Keys.Oemplus) Then
            e.Handled = True
            e.SuppressKeyPress = True
            AdjustTextBoxFontSize(tb, 1.0F)
            Return
        End If
        If e.Control AndAlso (e.KeyCode = Keys.Subtract OrElse e.KeyCode = Keys.OemMinus) Then
            e.Handled = True
            e.SuppressKeyPress = True
            AdjustTextBoxFontSize(tb, -1.0F)
            Return
        End If
        If e.KeyCode = Keys.Enter AndAlso Not e.Shift Then
            e.Handled = True
            e.SuppressKeyPress = True
            FinishTextEditing(tb)
        ElseIf e.KeyCode = Keys.Escape Then
            e.Handled = True
            e.SuppressKeyPress = True
            CancelTextEditing(tb)
        End If
    End Sub

    Private Sub TextEdit_LostFocus(sender As Object, e As EventArgs)
        FinishTextEditing(DirectCast(sender, TextBox))
    End Sub

    Private Sub FinishTextEditing(txtEdit As TextBox, Optional applyPointerTool As Boolean = True)
        If txtEdit Is Nothing OrElse txtEdit.IsDisposed Then Return
        If Not Object.ReferenceEquals(txtEdit, m_ActiveTextEdit) Then Return

        RemoveHandler txtEdit.KeyDown, AddressOf TextEdit_KeyDown
        RemoveHandler txtEdit.LostFocus, AddressOf TextEdit_LostFocus
        RemoveHandler txtEdit.MouseWheel, AddressOf TextEdit_MouseWheel

        Dim textObj As DrawableTextN = DirectCast(txtEdit.Tag, DrawableTextN)
        textObj.Text = txtEdit.Text
        Dim newFont = CType(txtEdit.Font.Clone(), Font)
        textObj.Font = newFont
        textObj.ForeColor = txtEdit.ForeColor
        m_CurrentFont = newFont
        m_CurrentTextColor = txtEdit.ForeColor

        textObj.SetBounds(ScreenToWorldRect(txtEdit.Bounds))

        m_ActiveTextEdit = Nothing
        picCanvas.Controls.Remove(txtEdit)
        txtEdit.Dispose()
        picCanvas.Invalidate()
        m_NewDrawableN = Nothing
        ApplyToolbarTextEditAppearance()
        If applyPointerTool Then ActivatePointerTool()
    End Sub

    Private Sub CancelTextEditing(txtEdit As TextBox)
        If txtEdit Is Nothing OrElse txtEdit.IsDisposed Then Return
        If Not Object.ReferenceEquals(txtEdit, m_ActiveTextEdit) Then Return

        RemoveHandler txtEdit.KeyDown, AddressOf TextEdit_KeyDown
        RemoveHandler txtEdit.LostFocus, AddressOf TextEdit_LostFocus
        RemoveHandler txtEdit.MouseWheel, AddressOf TextEdit_MouseWheel

        Dim textObj As DrawableTextN = DirectCast(txtEdit.Tag, DrawableTextN)
        m_PictureN.Remove(textObj)
        m_ActiveTextEdit = Nothing
        picCanvas.Controls.Remove(txtEdit)
        txtEdit.Dispose()
        picCanvas.Invalidate()
        m_NewDrawableN = Nothing
        ActivatePointerTool()
    End Sub


#End Region


#Region "Undo"
    Private IsDirty As Boolean = False
    ' Picture state
    Private DrawablePic As New DrawablePictureN()

    ' Undo/redo stacks
    Private UndoStack As New Stack(Of DrawablePictureN)()
    Private RedoStack As New Stack(Of DrawablePictureN)()

    ' Auto-save path
    Private AutoSavePath As String = "autosave.xml"

    ' Snap settings
    Private Const SnapTolerance As Integer = 10
    Private EnableSnap As Boolean = True

    'subs
    Private Sub SavePicture(path As String)
        Dim serializer As New Xml.Serialization.XmlSerializer(GetType(DrawablePictureN))
        Using fs As New FileStream(path, FileMode.Create)
            serializer.Serialize(fs, DrawablePic)
        End Using
    End Sub

    Private Sub LoadPicture(path As String)
        Dim serializer As New Xml.Serialization.XmlSerializer(GetType(DrawablePictureN))
        Using fs As New FileStream(path, FileMode.Open)
            DrawablePic = DirectCast(serializer.Deserialize(fs), DrawablePictureN)
            For Each s In DrawablePic.Drawables
                s.AfterLoad()
            Next
        End Using
        picCanvas.Invalidate()
    End Sub

    Private Sub CommitChange()
        ' Push a clone to undo stack
        UndoStack.Push(DrawablePic.Clone())
        RedoStack.Clear() ' Clear redo stack
        SavePicture(AutoSavePath)
    End Sub

    Private Sub Undo()
        If UndoStack.Count = 0 Then Return
        RedoStack.Push(DrawablePic.Clone())
        DrawablePic = UndoStack.Pop()
        picCanvas.Invalidate()
    End Sub

    Private Sub Redo()
        If RedoStack.Count = 0 Then Return
        UndoStack.Push(DrawablePic.Clone())
        DrawablePic = RedoStack.Pop()
        picCanvas.Invalidate()
    End Sub

    Private Sub GroupSelected()
        Dim selected = DrawablePic.Drawables.Where(Function(s) s.IsSelected).ToList()
        If selected.Count <= 1 Then Return

        For Each s In selected
            DrawablePic.Drawables.Remove(s)
        Next

        Dim group As New DrawableGroupN()
        group.Children.AddRange(selected)
        DrawablePic.Drawables.Add(group)

        CommitChange()
        picCanvas.Invalidate()
    End Sub

    Private Sub UngroupSelected()
        Dim groups = DrawablePic.Drawables.OfType(Of DrawableGroupN).Where(Function(g) g.IsSelected).ToList()
        For Each grp In groups
            DrawablePic.Drawables.Remove(grp)
            DrawablePic.Drawables.AddRange(grp.Children)
        Next

        CommitChange()
        picCanvas.Invalidate()
    End Sub

    Private Function SnapPoint(pt As Point) As Point
        If Not EnableSnap Then Return pt

        Dim closestX = pt.X
        Dim closestY = pt.Y
        For Each s In DrawablePic.Drawables
            Dim bounds = s.GetBounds()
            If Math.Abs(pt.X - bounds.Left) <= SnapTolerance Then closestX = bounds.Left
            If Math.Abs(pt.X - bounds.Right) <= SnapTolerance Then closestX = bounds.Right
            If Math.Abs(pt.Y - bounds.Top) <= SnapTolerance Then closestY = bounds.Top
            If Math.Abs(pt.Y - bounds.Bottom) <= SnapTolerance Then closestY = bounds.Bottom
        Next
        Return New Point(closestX, closestY)
    End Function


    Private Sub tmrAutoSave_Tick(sender As Object, e As EventArgs) Handles tmrAutoSave.Tick
        If IsDirty Then
            m_PictureN.SavePicture(AutoSavePath)
            IsDirty = False
        End If
    End Sub

    ' In main form:
    Private m_UndoStack As New Stack(Of DrawablePictureN)
    Private m_RedoStack As New Stack(Of DrawablePictureN)

    Private Sub SaveUndoState()
        m_RedoStack.Clear()

        ' Clone current state
        Using ms As New MemoryStream()
            Dim formatter As New BinaryFormatter()
            formatter.Serialize(ms, m_PictureN)
            ms.Position = 0
            m_UndoStack.Push(DirectCast(formatter.Deserialize(ms), DrawablePictureN))
        End Using
    End Sub

    Private Sub Undo1()
        If m_UndoStack.Count > 0 Then
            m_RedoStack.Push(m_PictureN)
            m_PictureN = m_UndoStack.Pop()
            picCanvas.Invalidate()
        End If
    End Sub

    Public Sub Redo1()
        If m_RedoStack.Count > 0 Then
            m_UndoStack.Push(m_PictureN.Clone())
            m_PictureN = m_RedoStack.Pop()
            Invalidate()
        End If
    End Sub



#End Region

    Private Sub mnuResetTransform_Click(sender As Object, e As EventArgs) Handles mnuResetTransform.ItemClick
        If m_PictureN.SelectedDrawable IsNot Nothing Then
            m_PictureN.SelectedDrawable.RotationAngle = 0
            m_PictureN.SelectedDrawable.ScaleX = 1
            m_PictureN.SelectedDrawable.ScaleY = 1
            picCanvas.Invalidate()
        End If
    End Sub

    Private Sub mnuFlipHorizontal_Click(sender As Object, e As EventArgs) Handles mnuFlipHorizontal.ItemClick
        If m_PictureN.SelectedDrawable IsNot Nothing Then
            m_PictureN.SelectedDrawable.ScaleX *= -1
            picCanvas.Invalidate()
        End If
    End Sub

    Private Sub mnuFlipVertical_Click(sender As Object, e As EventArgs) Handles mnuFlipVertical.ItemClick
        If m_PictureN.SelectedDrawable IsNot Nothing Then
            m_PictureN.SelectedDrawable.ScaleY *= -1
            picCanvas.Invalidate()
        End If
    End Sub





#Region "To Be Fixed"

#Region "Line Thickness Menu Routines"
    ' The user has selected a new line thickness.
    Private Sub mnuLineThick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Update the current pen.
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        m_CurrentLineWidth = Integer.Parse(menu_item.Text)

        ' Update the toolbar display.
        tcboThickness.ImageIndex = m_CurrentLineWidth +
                m_FirstLineThicknessImage - 1

        ' Update the selected object if there is one.
        If Not (m_PictureN.SelectedDrawable Is Nothing) Then
            m_PictureN.SelectedDrawable.LineWidth = m_CurrentLineWidth
            picCanvas.Invalidate()
        End If

        ' Reselect the currently selected tool.
        m_SelectedToolButton.Pushed = True
    End Sub

    ' Allow room for the MenuItem.
    Private Sub mnuLineThick_MeasureItem(ByVal sender As Object, ByVal e As System.Windows.Forms.MeasureItemEventArgs)
        e.ItemWidth = 16
        e.ItemHeight = 16
    End Sub

    ' Draw the menu item.
    Private Sub mnuLineThick_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        Dim thickness As Integer = Integer.Parse(menu_item.Text)

        ' See if we're selected.
        Dim fg_pen As Pen
        Dim bg_brush As Brush
        If (e.State And DrawItemState.Selected) = 0 Then
            ' Not selected.
            ' Use a light background and dark foreground.
            fg_pen = New Pen(SystemColors.MenuText, thickness)
            bg_brush = New SolidBrush(SystemColors.Menu)
        Else
            ' Selected.
            ' Use a dark background and light foreground.
            fg_pen = New Pen(SystemColors.HighlightText, thickness)
            bg_brush = New SolidBrush(SystemColors.Highlight)
        End If

        ' Erase the background.
        e.Graphics.FillRectangle(bg_brush, e.Bounds)

        ' Draw the line.
        Dim y As Integer = e.Bounds.Y + e.Bounds.Height \ 2
        e.Graphics.DrawLine(fg_pen, e.Bounds.X, y, e.Bounds.Right, y)

        fg_pen.Dispose()
        bg_brush.Dispose()
    End Sub
#End Region ' Line Thickness Menu Routines

#Region "Line Color Menu Routines"
    ' The user has selected a new line color.
    Private Sub mnuLineColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Update the current pen.
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        m_CurrentForeColor = Color.FromName(menu_item.Text)

        ' Update the toolbar display.
        Select Case menu_item.Text
            Case "Red"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 0
            Case "Green"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 1
            Case "Blue"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 2
            Case "Yellow"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 3
            Case "Orange"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 4
            Case "Purple"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 5
            Case "Black"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 6
            Case "White"
                tcboLineColor.ImageIndex = m_FirstLineColorImage + 7
        End Select

        ' Update the selected object if there is one.
        If Not (m_PictureN.SelectedDrawable Is Nothing) Then
            m_PictureN.SelectedDrawable.ForeColor = m_CurrentForeColor
            picCanvas.Invalidate()
        End If

        ' Reselect the currently selected tool.
        m_SelectedToolButton.Pushed = True
    End Sub

    ' Allow room for the MenuItem.
    Private Sub mnuLineColor_MeasureItem(ByVal sender As Object, ByVal e As System.Windows.Forms.MeasureItemEventArgs)
        e.ItemWidth = 16
        e.ItemHeight = 16
    End Sub

    ' Draw the menu item.
    Private Sub mnuLineColor_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        Dim new_color As Color = Color.FromName(menu_item.Text)

        ' See if we're selected.
        Dim bg_brush As Brush
        If (e.State And DrawItemState.Selected) = 0 Then
            ' Not selected.
            ' Use a light background.
            bg_brush = New SolidBrush(SystemColors.Menu)
        Else
            ' Selected.
            ' Use a dark background.
            bg_brush = New SolidBrush(SystemColors.Highlight)
        End If

        ' Erase the background.
        e.Graphics.FillRectangle(bg_brush, e.Bounds)

        ' Draw the line.
        Dim y As Integer = e.Bounds.Y + e.Bounds.Height \ 2
        Dim fg_pen As New Pen(new_color, 4)
        e.Graphics.DrawLine(fg_pen, e.Bounds.X, y, e.Bounds.Right, y)
        fg_pen.Dispose()
    End Sub
#End Region ' Line Color Menu Routines

#Region "Fill Color Menu Routines"
    ' The user has selected a new line color.
    Private Sub mnuFillColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Update the current pen.
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        m_CurrentFillColor = Color.FromName(menu_item.Text)

        ' Update the toolbar display.
        Select Case menu_item.Text
            Case "Red"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 0
            Case "Green"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 1
            Case "Blue"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 2
            Case "Yellow"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 3
            Case "Orange"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 4
            Case "Purple"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 5
            Case "Black"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 6
            Case "White"
                tcboFillColor.ImageIndex = m_FirstFillColorImage + 7
        End Select

        ' Update the selected object if there is one.
        If Not (m_PictureN.SelectedDrawable Is Nothing) Then
            m_PictureN.SelectedDrawable.FillColor = m_CurrentFillColor
            picCanvas.Invalidate()
        End If

        ' Reselect the currently selected tool.
        m_SelectedToolButton.Pushed = True
    End Sub

    ' Allow room for the MenuItem.
    Private Sub mnuFillColor_MeasureItem(ByVal sender As Object, ByVal e As System.Windows.Forms.MeasureItemEventArgs)
        e.ItemWidth = 16
        e.ItemHeight = 16
    End Sub

    ' Draw the menu item.
    Private Sub mnuFillColor_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
        Dim menu_item As MenuItem = DirectCast(sender, MenuItem)
        Dim new_color As Color = Color.FromName(menu_item.Text)

        ' See if we're selected.
        Dim bg_brush As Brush
        If (e.State And DrawItemState.Selected) = 0 Then
            ' Not selected.
            ' Use a light background.
            bg_brush = New SolidBrush(SystemColors.Menu)
        Else
            ' Selected.
            ' Use a dark background.
            bg_brush = New SolidBrush(SystemColors.Highlight)
        End If

        ' Erase the background.
        e.Graphics.FillRectangle(bg_brush, e.Bounds)

        ' Draw a filled area.
        Dim fg_brush As New SolidBrush(new_color)
        e.Graphics.FillRectangle(fg_brush,
                e.Bounds.X + 2,
                e.Bounds.Y + 2,
                e.Bounds.Width - 4,
                e.Bounds.Height - 4)
        fg_brush.Dispose()
    End Sub
#End Region ' Fill Color Menu Routines

#Region "Edit Format Print Menu Routines"


    ' Display the print preview dialog.
    ' Note that ppdPreview.Document = pdPrint was set in the form designer.
    Private Sub mnuFilePrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilePrintPreview.ItemClick
        ppdPrint.ShowDialog()
    End Sub

    ' Print.
    ' Note: PRINT_ENLARGED cannot be True unless PRINT_CENTERED is also.
    ' Note: If PRINT_CENTERED is False, the picture is drawn
    ' in the upper left corner where it will be chopped because
    ' the printer cannot print all the way to the edges of the paper.
    Private Sub pdPrint_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdPrint.PrintPage
#Const PRINT_CENTERED = True
#Const PRINT_ENLARGED = True
        '#Const PRINT_MARGIN = True

#If PRINT_CENTERED Then ' Center the picture.
        ' Get the picture's bounds.
        Dim bounds As RectangleF = m_PictureN.GetBounds()

        ' Translate the drawing to the origin.
        e.Graphics.TranslateTransform(-bounds.X, -bounds.Y)

        Dim scale As Single = 1

#If PRINT_ENLARGED Then ' Scale to fit.
        Dim xscale As Double = 1
        If bounds.Width > 0 Then xscale = e.MarginBounds.Width / bounds.Width
        Dim yscale As Double = 1
        If bounds.Height > 0 Then yscale = e.MarginBounds.Height / bounds.Height

        If xscale > yscale Then
            scale = CSng(yscale)
        Else
            scale = CSng(xscale)
        End If
        e.Graphics.ScaleTransform(scale, scale, Drawing2D.MatrixOrder.Append)
#End If

        ' Translate to center the drawing.
        Dim cx As Integer = CInt((e.MarginBounds.Width - bounds.Width * scale) / 2)
        Dim cy As Integer = CInt((e.MarginBounds.Height - bounds.Height * scale) / 2)
        e.Graphics.TranslateTransform(
                e.MarginBounds.X + cx,
                e.MarginBounds.Y + cy,
                Drawing2D.MatrixOrder.Append)
#End If

        ' Draw the picture.
        m_PictureN.Draw(e.Graphics)

#If PRINT_MARGIN Then
        ' Draw the margin.
        e.Graphics.ResetTransform()
        Using margin_pen As New Pen(Color.Red)
            margin_pen.DashPattern = New Single() {5, 5}
            e.Graphics.DrawRectangle(margin_pen, e.MarginBounds)
        End Using
#End If
    End Sub

    ' Print.
    Private Sub mnuFilePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilePrint.ItemClick
        pdPrint.Print()
    End Sub

    ' Enable or disable the Edit menu commands.
    Private Sub mnuEdit_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEdit.Popup
        mnuEditDelete.Enabled = m_PictureN.SelectedDrawable IsNot Nothing
    End Sub

    ' Enable or disable the Format menu commands.
    Private Sub mnuFormat_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFormat.Popup
        mnuFormatOrder.Enabled = m_PictureN.SelectedDrawable IsNot Nothing
    End Sub

    ' Save the picture as an image.
    Private Sub mnuFileSaveImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileSaveImage.ItemClick
        If sfdImage.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim bm As Bitmap = RenderPictureToBitmap()
            If bm Is Nothing Then
                MessageBox.Show("Nothing to save. The drawing area is empty.", "Save Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Try
                Dim filename As String = sfdImage.FileName
                Dim ext As String = filename.Substring(filename.LastIndexOf("."))
                ext = ext.ToLower()
                Select Case (ext)
                    Case ".bmp"
                        bm.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp)
                    Case ".gif"
                        bm.Save(filename, System.Drawing.Imaging.ImageFormat.Gif)
                    Case ".jpg", ".jpeg"
                        bm.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Case ".png"
                        bm.Save(filename, System.Drawing.Imaging.ImageFormat.Png)
                    Case ".tif", ".tiff"
                        bm.Save(filename, System.Drawing.Imaging.ImageFormat.Tiff)
                    Case Else
                        MessageBox.Show("Unknown file extension '" & ext & "'",
                                "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Select
            Finally
                bm.Dispose()
            End Try
        End If
    End Sub

    Private Sub NewFrmDraw_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Proactively release large GDI+ resources to avoid OutOfMemory on repeated edits
        Try
            ' Dispose background image
            If m_PictureN IsNot Nothing AndAlso m_PictureN.BackgroundImage IsNot Nothing Then
                m_PictureN.BackgroundImage.Dispose()
                m_PictureN.BackgroundImage = Nothing
            End If

            ' Dispose any embedded image bitmaps in drawables
            If m_PictureN IsNot Nothing AndAlso m_PictureN.Drawables IsNot Nothing Then
                For Each dr In m_PictureN.Drawables
                    Dim imgDr = TryCast(dr, DrawableImageN)
                    If imgDr IsNot Nothing AndAlso imgDr.Picture IsNot Nothing Then
                        imgDr.Picture.Dispose()
                        imgDr.Picture = Nothing
                    End If
                Next
                m_PictureN.Drawables.Clear()
            End If
        Catch
            ' Ignore cleanup errors
        End Try
        If m_ToolbarTextAppearanceFont IsNot Nothing Then
            m_ToolbarTextAppearanceFont.Dispose()
            m_ToolbarTextAppearanceFont = Nothing
        End If
    End Sub

    Property SavedImage As Image

    ''' <summary>Full path of the file written by Save && Close (auto-named patient image).</summary>
    Property SavedFilePath As String

    Private Function TryParsePatientIdFromOpenedPath(fullPath As String, ByRef patientId As Integer) As Boolean
        patientId = 0
        If String.IsNullOrWhiteSpace(fullPath) Then Return False
        Dim fn = Path.GetFileNameWithoutExtension(fullPath)
        If fn.Length > 2 AndAlso fn.StartsWith("P-", StringComparison.OrdinalIgnoreCase) Then
            Dim rest = fn.Substring(2)
            Dim dash = rest.IndexOf("-"c)
            If dash > 0 Then
                Return Integer.TryParse(rest.Substring(0, dash), patientId)
            End If
        End If
        Dim d = Path.GetDirectoryName(fullPath)
        If String.IsNullOrEmpty(d) Then Return False
        Dim patientDir = Directory.GetParent(d)
        If patientDir Is Nothing Then Return False
        Dim pn = patientDir.Name
        If pn.StartsWith("Patient_", StringComparison.OrdinalIgnoreCase) Then
            Return Integer.TryParse(pn.Substring("Patient_".Length), patientId)
        End If
        Return False
    End Function

    Private Shared Function ExtractPatientImageSuffixIndex(filePath As String) As Integer
        Dim name = Path.GetFileNameWithoutExtension(filePath)
        Dim parts = name.Split("_"c)
        If parts.Length >= 2 Then
            Dim number As Integer
            If Integer.TryParse(parts(parts.Length - 1), number) Then Return number
        End If
        Return -1
    End Function

    Private Function GetNextPatientImageIndex(imageFolder As String, patientId As Integer) As Integer
        Dim pattern = "P-" & patientId.ToString() & "-IMG_*"
        If Not Directory.Exists(imageFolder) Then Return 0
        Dim existingFiles = Directory.GetFiles(imageFolder, pattern)
        Dim maxIdx = -1
        For Each fp In existingFiles
            Dim i = ExtractPatientImageSuffixIndex(fp)
            If i > maxIdx Then maxIdx = i
        Next
        If maxIdx < 0 Then Return 0
        Return maxIdx + 1
    End Function

    Private Sub btnSaveClose_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnSaveClose.ItemClick
        If m_Dest = "" Then Exit Sub

        SavedFilePath = Nothing

        Dim destDir = Path.GetDirectoryName(m_Dest)
        If String.IsNullOrEmpty(destDir) Then
            MessageBox.Show("Cannot determine save folder.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim patientId As Integer
        If Not TryParsePatientIdFromOpenedPath(m_Dest, patientId) Then
            MessageBox.Show("Cannot determine patient ID from the image path or file name (expected Images\Patient_{id}\... or P-{id}-IMG_*).",
                            "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim ext = Path.GetExtension(m_Dest)
        If String.IsNullOrEmpty(ext) Then ext = ".jpg"
        ext = ext.ToLowerInvariant()

        For Each drawable In m_PictureN.Drawables
            drawable.IsSelected = False
        Next

        Dim bm As Bitmap = RenderPictureToBitmap()
        If bm Is Nothing Then
            MessageBox.Show("Nothing to save. The drawing area is empty.", "Save Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            If Not Directory.Exists(destDir) Then
                Directory.CreateDirectory(destDir)
            End If

            Dim nextIndex = GetNextPatientImageIndex(destDir, patientId)
            Dim filename = Path.Combine(destDir, "P-" & patientId.ToString() & "-IMG_" & nextIndex.ToString() & ext)

            Try
                Select Case ext
                    Case ".bmp"
                        bm.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp)
                    Case ".gif"
                        bm.Save(filename, System.Drawing.Imaging.ImageFormat.Gif)
                    Case ".jpg", ".jpeg"
                        bm.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Case ".png"
                        bm.Save(filename, System.Drawing.Imaging.ImageFormat.Png)
                    Case ".tif", ".tiff"
                        bm.Save(filename, System.Drawing.Imaging.ImageFormat.Tiff)
                    Case Else
                        MessageBox.Show("Unknown file extension '" & ext & "'", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                End Select

                SavedFilePath = filename
                SavedImage = CType(bm.Clone(), Image)
                DialogResult = DialogResult.OK
                Close()

            Catch ex As Exception
                MessageBox.Show("Failed to save image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                bm.Dispose()
            End Try
        Catch ex As Exception
            MessageBox.Show("Failed to save image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnCrop_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnCrop.ItemClick
        Try
            Dim bmp As Bitmap = RenderPictureToBitmap()
            If bmp Is Nothing Then
                MessageBox.Show("Nothing to crop. The drawing area is empty.", "Crop",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Try
                Dim F As New FrmCrop(bmp)
                If F.ShowDialog() = DialogResult.OK Then
                    Dim img As Image = F.CroppedImage
                    If img IsNot Nothing Then
                        Dim rec As New Rectangle(0, 0, img.Width, img.Height)
                        m_NewDrawableN = New DrawableImageN(rec.Left, rec.Top, rec.Right, rec.Bottom)
                        Dim drawableImage As DrawableImageN = DirectCast(m_NewDrawableN, DrawableImageN)
                        Try
                            drawableImage.Picture = img
                            m_NewDrawableN = drawableImage
                            m_NewDrawableN.IsSelected = False
                            passedImage = True
                            m_PictureN.Add(m_NewDrawableN)
                            realSize = img.Size
                            realWidth = img.Width
                            realHeight = img.Height
                            UpdateCanvasSizePolicy()
                            RecenterViewOnDrawable(drawableImage)
                            m_NewDrawableN = Nothing
                            isCreatingNewObject = False
                        Catch ex As Exception
                            m_PictureN.Remove(m_NewDrawableN)
                            m_NewDrawableN = Nothing
                            isCreatingNewObject = False
                        End Try
                    End If
                    picCanvas.Invalidate()
                End If
            Finally
                bmp.Dispose()
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Function TryLoadImageFromPath(path As String, ByRef image As Image, ByRef errorMessage As String) As Boolean
    '    image = Nothing
    '    errorMessage = ""

    '    If String.IsNullOrWhiteSpace(path) Then
    '        errorMessage = "No file selected."
    '        Return False
    '    End If

    '    Dim targetPath = path
    '    If Directory.Exists(path) Then
    '        targetPath = FindFirstDicomFile(path)
    '        If String.IsNullOrWhiteSpace(targetPath) Then
    '            errorMessage = "No DICOM files found in folder."
    '            Return False
    '        End If
    '    End If

    '    Dim ext = System.IO.Path.GetExtension(targetPath)
    '    If IsDicomFile(targetPath) OrElse String.IsNullOrWhiteSpace(ext) Then
    '        If TryLoadDicomImage(targetPath, image, errorMessage) Then
    '            Return True
    '        End If
    '    End If

    '    Return TryLoadStandardImage(targetPath, image, errorMessage)
    'End Function

    Private Function TryLoadImageFromPath(path As String, ByRef image As Image, ByRef errorMessage As String) As Boolean
        image = Nothing
        errorMessage = ""
        If String.IsNullOrWhiteSpace(path) Then
            errorMessage = "No file selected."
            Return False
        End If
        Return TryLoadStandardImage(path, image, errorMessage)
    End Function

    Private Function TryLoadStandardImage(path As String, ByRef image As Image, ByRef errorMessage As String) As Boolean
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

    'Private Function TryLoadDicomImage(path As String, ByRef image As Image, ByRef errorMessage As String) As Boolean
    '    Try
    '        EnsureDicomInitialized()
    '        Dim dicomFile1 = DicomFile.Open(path)
    '        Dim transferSyntax = dicomFile1.FileMetaInfo?.TransferSyntax
    '        If transferSyntax IsNot Nothing AndAlso
    '           transferSyntax.UID IsNot Nothing AndAlso
    '           transferSyntax.UID.Name IsNot Nothing AndAlso
    '           transferSyntax.UID.Name.IndexOf("JPEG Extended", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso
    '           Not _nativeCodecAvailable Then
    '            errorMessage = "This DICOM uses JPEG Extended (12-bit) which is not supported without native codecs."
    '            Return False
    '        End If
    '        Dim pixelData = DicomPixelData.Create(dicomFile1.Dataset, False)
    '        If pixelData.NumberOfFrames <= 0 Then
    '            errorMessage = "DICOM file does not contain image frames."
    '            Return False
    '        End If
    '        Dim dicomImage = New DicomImage(dicomFile1.Dataset)
    '        image = dicomImage.RenderImage(0).AsClonedBitmap()
    '        Return True
    '    Catch ex As Exception
    '        Dim friendly = If(String.IsNullOrWhiteSpace(ex.Message),
    '                          "Failed to read DICOM image data.",
    '                          ex.Message)
    '        If friendly.IndexOf("JPEG Extended", StringComparison.OrdinalIgnoreCase) >= 0 Then
    '            friendly &= " The required JPEG codec is not available."
    '        End If
    '        errorMessage = friendly
    '        Return False
    '    End Try
    'End Function

    Private Function IsDicomFile(path As String) As Boolean
        If String.Equals(System.IO.Path.GetExtension(path), ".dcm", StringComparison.OrdinalIgnoreCase) Then
            Return True
        End If

        Try
            Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                If fs.Length < 132 Then Return False
                fs.Seek(128, SeekOrigin.Begin)
                Dim buffer(3) As Byte
                fs.Read(buffer, 0, 4)
                Dim marker = Encoding.ASCII.GetString(buffer)
                Return String.Equals(marker, "DICM", StringComparison.Ordinal)
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Sub btnRotRight_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnRotRight.ItemClick
        Dim imageDrawable As DrawableImageN = Nothing
        If Not TryGetLoadedImageDrawable(imageDrawable) Then Return

        Dim bounds = imageDrawable.GetBounds()
        imageDrawable.RotationCenter = New PointF(bounds.Left + bounds.Width / 2.0F, bounds.Top + bounds.Height / 2.0F)
        imageDrawable.RotationAngle = NormalizeAngle(imageDrawable.RotationAngle + 90.0F)
        m_PictureN.SelectedDrawable = imageDrawable
        RecenterViewOnDrawable(imageDrawable)
        picCanvas.Invalidate()
    End Sub

    Private Sub btnRotLeft_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnRotLeft.ItemClick
        Dim imageDrawable As DrawableImageN = Nothing
        If Not TryGetLoadedImageDrawable(imageDrawable) Then Return

        Dim bounds = imageDrawable.GetBounds()
        imageDrawable.RotationCenter = New PointF(bounds.Left + bounds.Width / 2.0F, bounds.Top + bounds.Height / 2.0F)
        imageDrawable.RotationAngle = NormalizeAngle(imageDrawable.RotationAngle - 90.0F)
        m_PictureN.SelectedDrawable = imageDrawable
        RecenterViewOnDrawable(imageDrawable)
        picCanvas.Invalidate()
    End Sub

    Private Sub btnZoom_EditValueChanged(sender As Object, e As EventArgs) Handles btnZoom.EditValueChanged
        If m_ApplyingProgrammaticZoom Then Return
        Static lastZoomUpdate As DateTime = DateTime.MinValue
        Dim now = DateTime.Now
        If (now - lastZoomUpdate).TotalMilliseconds < 15 Then Return
        lastZoomUpdate = now

        Dim targetScale As Single = GetZoomScaleFromEditor()
        If targetScale <= 0.0F Then Return
        m_ViewZoom = targetScale
        Dim imageDrawable As DrawableImageN = Nothing
        If TryGetLoadedImageDrawable(imageDrawable) Then
            RecenterViewOnDrawable(imageDrawable)
        End If
        UpdateAnchorSizeForView()
        UpdateStatusText()
        picCanvas.Invalidate()
    End Sub

    Private Function RenderPictureToBitmap() As Bitmap
        Dim rect As RectangleF = m_PictureN.GetBounds()
        If rect.Width <= 0 OrElse rect.Height <= 0 Then Return Nothing

        Dim width As Integer = Math.Max(1, CInt(Math.Ceiling(rect.Width)))
        Dim height As Integer = Math.Max(1, CInt(Math.Ceiling(rect.Height)))
        Dim bm As New Bitmap(width, height)

        Using gr As Graphics = Graphics.FromImage(bm)
            gr.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            gr.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            gr.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            gr.TranslateTransform(-rect.X, -rect.Y)
            m_PictureN.Draw(gr)
        End Using

        Return bm
    End Function

    Private Sub InitializeZoomEditor()
        ' Keep zoom control behavior LTR even when the form is RTL (Arabic),
        ' so '-' always decreases and '+' always increases.
        'RepositoryItemZoomTrackBar1.RightToLeft = RightToLeft.No
        RepositoryItemZoomTrackBar1.Minimum = 10
        RepositoryItemZoomTrackBar1.Maximum = 400
        RepositoryItemZoomTrackBar1.SmallChange = 5
        RepositoryItemZoomTrackBar1.LargeChange = 20
        RepositoryItemZoomTrackBar1.TickFrequency = 10
        btnZoom.EditValue = CType(100, Short)
        UpdateStatusText()
    End Sub

    Private Function GetZoomScaleFromEditor() As Single
        Dim rawValue As Decimal
        If Not Decimal.TryParse(Convert.ToString(btnZoom.EditValue), rawValue) Then Return -1.0F
        rawValue = Math.Max(CDec(RepositoryItemZoomTrackBar1.Minimum), Math.Min(rawValue, CDec(RepositoryItemZoomTrackBar1.Maximum)))
        Return CSng(rawValue / 100D)
    End Function

    Private Sub RecenterViewOnDrawable(dr As DrawableN)
        If dr Is Nothing Then Return
        Dim transformed = dr.GetTransformedBounds()
        If transformed.Width <= 0 OrElse transformed.Height <= 0 Then Return
        RecenterViewOnBounds(transformed)
    End Sub

    Private Sub RecenterViewOnBounds(transformed As RectangleF)
        If transformed.Width <= 0 OrElse transformed.Height <= 0 Then Return
        Dim worldCenter As New PointF(transformed.Left + transformed.Width / 2.0F, transformed.Top + transformed.Height / 2.0F)
        Dim canvasCenter As New PointF(picCanvas.ClientSize.Width / 2.0F, picCanvas.ClientSize.Height / 2.0F)
        m_ViewOffset = New PointF(canvasCenter.X - worldCenter.X * m_ViewZoom, canvasCenter.Y - worldCenter.Y * m_ViewZoom)
    End Sub

    Private Sub NormalizeViewportAfterContentChange(preferLoadedImage As Boolean)
        UpdateCanvasSizePolicy()

        Dim target As DrawableN = Nothing
        If preferLoadedImage Then
            Dim loaded As DrawableImageN = Nothing
            If TryGetLoadedImageDrawable(loaded) Then
                target = loaded
            End If
        End If

        If target Is Nothing AndAlso m_PictureN IsNot Nothing Then
            Dim b As RectangleF = m_PictureN.GetBounds()
            If b.Width > 0 AndAlso b.Height > 0 Then
                RecenterViewOnBounds(b)
                Return
            End If
        End If

        If target IsNot Nothing Then
            RecenterViewOnDrawable(target)
        Else
            m_ViewOffset = PointF.Empty
        End If
    End Sub

    Private Function ScreenToWorld(screenPoint As Point) As Point
        If m_ViewZoom <= 0.0001F Then m_ViewZoom = 1.0F
        Dim x = (screenPoint.X - m_ViewOffset.X) / m_ViewZoom
        Dim y = (screenPoint.Y - m_ViewOffset.Y) / m_ViewZoom
        Return New Point(CInt(Math.Round(x)), CInt(Math.Round(y)))
    End Function

    Private Function WorldToScreenPoint(worldPt As Point) As Point
        If m_ViewZoom <= 0.0001F Then m_ViewZoom = 1.0F
        Return New Point(
            CInt(Math.Round(worldPt.X * m_ViewZoom + m_ViewOffset.X)),
            CInt(Math.Round(worldPt.Y * m_ViewZoom + m_ViewOffset.Y)))
    End Function

    Private Function ScreenToWorldRect(screenRect As Rectangle) As Rectangle
        Dim p1 = ScreenToWorld(New Point(screenRect.Left, screenRect.Top))
        Dim p2 = ScreenToWorld(New Point(screenRect.Right, screenRect.Bottom))
        Return Rectangle.FromLTRB(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y))
    End Function

    Private Function WorldToScreenRect(worldRect As Rectangle) As Rectangle
        Dim p1 = WorldToScreenPoint(New Point(worldRect.Left, worldRect.Top))
        Dim p2 = WorldToScreenPoint(New Point(worldRect.Right, worldRect.Bottom))
        Return Rectangle.FromLTRB(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y))
    End Function

    Private Function FindTopDrawableAt(x As Integer, y As Integer) As DrawableN
        For i As Integer = m_PictureN.Drawables.Count - 1 To 0 Step -1
            Dim dr = m_PictureN.Drawables(i)
            If GetAnchorAtWithTolerance(dr, New Point(x, y)) <> AnchorEnumN.None Then Return dr
            Dim transformedPoint = dr.InverseTransformPoint(New PointF(x, y))
            If dr.IsAt(CInt(transformedPoint.X), CInt(transformedPoint.Y)) Then Return dr
        Next
        Return Nothing
    End Function

    Private Function GetAnchorAtWithTolerance(dr As DrawableN, worldPoint As Point) As AnchorEnumN
        If dr Is Nothing Then Return AnchorEnumN.None

        Dim inflation As Single = 8.0F / Math.Max(0.05F, m_ViewZoom)
        For Each kvp In dr.GetAnchors()
            Dim r = kvp.Value
            r.Inflate(inflation, inflation)
            If r.Contains(worldPoint) Then
                Return kvp.Key
            End If
        Next
        Return AnchorEnumN.None
    End Function

    Private Function IsPointOnAnySelectedDrawable(worldPoint As Point) As Boolean
        If m_PictureN Is Nothing OrElse m_PictureN.Drawables Is Nothing Then Return False

        For Each dr In m_PictureN.Drawables
            If Not dr.IsSelected Then Continue For
            Dim transformedPoint = dr.InverseTransformPoint(New PointF(worldPoint.X, worldPoint.Y))
            If dr.IsAt(CInt(transformedPoint.X), CInt(transformedPoint.Y)) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub ClearSelection()
        For Each dr In m_PictureN.Drawables
            dr.IsSelected = False
        Next
        m_SelectionProxy = Nothing
        m_PictureN.SelectedDrawable = Nothing
        UpdateStatusText()
    End Sub

    Private Sub SyncSelectedDrawable()
        Dim selected = m_PictureN.Drawables.Where(Function(d) d.IsSelected).ToList()
        If selected.Count = 0 Then
            m_SelectionProxy = Nothing
            m_PictureN.SelectedDrawable = Nothing
            UpdateStatusText()
            Return
        End If
        If selected.Count = 1 Then
            m_SelectionProxy = Nothing
            m_PictureN.SelectedDrawable = selected(0)
            selected(0).IsSelected = True
            UpdateStatusText()
            Return
        End If

        m_SelectionProxy = New DrawableSelectionProxy(selected) With {.IsSelected = True}
        m_PictureN.SelectedDrawable = m_SelectionProxy
        UpdateStatusText()
    End Sub

    Private Sub SelectByMarquee(screenRect As Rectangle, additive As Boolean)
        If screenRect.Width <= 0 OrElse screenRect.Height <= 0 Then Return
        Dim p1 = ScreenToWorld(New Point(screenRect.Left, screenRect.Top))
        Dim p2 = ScreenToWorld(New Point(screenRect.Right, screenRect.Bottom))
        Dim worldRect = Rectangle.FromLTRB(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y))

        If Not additive Then ClearSelection()
        For Each dr In m_PictureN.Drawables
            If worldRect.IntersectsWith(Rectangle.Round(dr.GetTransformedBounds())) Then
                dr.IsSelected = True
            End If
        Next
        SyncSelectedDrawable()
    End Sub

    Private Function IsAdditiveSelectionModifier() As Boolean
        Dim mods = Control.ModifierKeys
        Return (mods And Keys.Control) = Keys.Control OrElse (mods And Keys.Shift) = Keys.Shift
    End Function

    Private Sub SelectAllDrawables()
        If m_PictureN Is Nothing OrElse m_PictureN.Drawables Is Nothing Then Return
        For Each dr In m_PictureN.Drawables
            dr.IsSelected = True
        Next
        SyncSelectedDrawable()
        picCanvas.Invalidate()
    End Sub

    Private Sub InitializeFontMenu()
        If m_FontMenuInitialized Then Return
        If mnuFont Is Nothing OrElse barManager1 Is Nothing Then Return

        mnuFont.ClearLinks()
        Dim fontNames = New InstalledFontCollection().
            Families.
            Select(Function(f) f.Name).
            Distinct().
            OrderBy(Function(n) n).
            ToList()

        For Each fontName In fontNames
            Dim item As New BarCheckItem(barManager1) With {
                .Caption = fontName,
                .Tag = fontName,
                .GroupIndex = 9001,
                .AllowAllUp = False
            }
            item.Checked = String.Equals(fontName, m_CurrentFont.FontFamily.Name, StringComparison.OrdinalIgnoreCase)
            AddHandler item.ItemClick, AddressOf FontMenuItem_ItemClick
            mnuFont.AddItem(item)
        Next

        m_FontMenuInitialized = True
    End Sub

    Private Sub EnsureRotationButtons()
        ' Rotation buttons are declared and placed by the designer.
        ' Keep this method as a safe no-op for compatibility.
    End Sub

    Private Sub FontMenuItem_ItemClick(sender As Object, e As ItemClickEventArgs)
        Dim fontName As String = Convert.ToString(e.Item.Tag)
        If String.IsNullOrWhiteSpace(fontName) Then Return

        Try
            Dim newFont As New Font(fontName, m_CurrentFont.Size, m_CurrentFont.Style)
            ApplySelectedFont(newFont)
        Catch
            Dim newFont As New Font(fontName, m_CurrentFont.Size, FontStyle.Regular)
            ApplySelectedFont(newFont)
        End Try
    End Sub

    Private Sub ApplySelectedFont(newFont As Font)
        If newFont Is Nothing Then Return
        m_CurrentFont = newFont

        ApplyToolbarTextEditAppearance()

        If TypeOf m_PictureN.SelectedDrawable Is DrawableTextN Then
            Dim textObj = DirectCast(m_PictureN.SelectedDrawable, DrawableTextN)
            textObj.Font = m_CurrentFont
            Using g = picCanvas.CreateGraphics()
                textObj.AdjustSizeToText(g)
            End Using
        End If

        picCanvas.Invalidate()
    End Sub

    Private Sub UpdateStatusText()
        EnsureStatusInfoItem()
        Dim selectedCount As Integer = 0
        If m_PictureN IsNot Nothing AndAlso m_PictureN.Drawables IsNot Nothing Then
            selectedCount = m_PictureN.Drawables.Where(Function(d) d.IsSelected).Count()
        End If

        Dim zoomLabel As String = "Zoom"
        Dim selectedLabel As String = "Selected"
        Dim isArabic As Boolean = IsArabicUi()
        If isArabic Then
            zoomLabel = "التكبير"
            selectedLabel = "المحدد"
        End If

        Dim statusText As String = zoomLabel & ": " & CInt(Math.Round(m_ViewZoom * 100.0F)).ToString() & "% | " & selectedLabel & ": " & selectedCount.ToString()
        Dim title As String = If(String.IsNullOrWhiteSpace(m_BaseWindowTitle), "Draw", m_BaseWindowTitle)
        Me.Text = title & " | " & statusText
        If m_StatusInfoItem IsNot Nothing Then
            m_StatusInfoItem.Caption = statusText
        End If
        If m_HintsInfoItem IsNot Nothing Then
            m_HintsInfoItem.Caption = BuildStatusHintsCaption()
        End If
        If m_DimensionsInfoItem IsNot Nothing Then
            m_DimensionsInfoItem.Caption = BuildDimensionsStatusCaption()
            UpdateDimensionsItemAlignment()
        End If
    End Sub

    Private Function IsArabicUi() As Boolean
        Return String.Equals(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar", StringComparison.OrdinalIgnoreCase) OrElse
            Me.RightToLeft = RightToLeft.Yes
    End Function

    ''' <summary>Permanent W×H for the current selection (union bounds if group proxy). Uses world-space bounds after transform.</summary>
    Private Function BuildDimensionsStatusCaption() As String
        Dim isArabic = IsArabicUi()
        Dim wLabel As String = If(isArabic, "العرض", "W")
        Dim hLabel As String = If(isArabic, "الارتفاع", "H")
        Const dash As String = "—"

        If m_PictureN Is Nothing OrElse m_PictureN.SelectedDrawable Is Nothing Then
            Return wLabel & ": " & dash & "  " & hLabel & ": " & dash
        End If

        Dim b = m_PictureN.SelectedDrawable.GetTransformedBounds()
        Dim w = CInt(Math.Round(Math.Max(0.0F, b.Width)))
        Dim h = CInt(Math.Round(Math.Max(0.0F, b.Height)))
        Return wLabel & ": " & w.ToString() & "  " & hLabel & ": " & h.ToString()
    End Function

    Private Sub UpdateDimensionsItemAlignment()
        If m_DimensionsInfoItem Is Nothing Then Return
        ' English: pin to the right edge; Arabic (RTL): pin to the left edge.
        m_DimensionsInfoItem.Alignment = If(IsArabicUi(), BarItemLinkAlignment.Left, BarItemLinkAlignment.Right)
    End Sub

    ''' <summary>Status bar hints after zoom: base shortcuts plus contextual help for text and shapes with inside text.</summary>
    Private Function BuildStatusHintsCaption() As String
        Dim baseHints = GetLocalizedUiText("bsiHints.Caption", "Hints: Ctrl+Click or Shift+Click to add/remove selection. Shift+Wheel to zoom.")
        If m_PictureN Is Nothing OrElse m_PictureN.Drawables Is Nothing Then Return baseHints

        Dim selected = m_PictureN.Drawables.Where(Function(d1) d1.IsSelected).ToList()
        If selected.Count <> 1 Then Return baseHints

        Dim d = selected(0)
        Dim extra As String = ""
        If TypeOf d Is DrawableTextN Then
            extra = GetLocalizedUiText("bsiHintsTextSelected.Caption",
                "Text selected: double-click or F2 to edit. While typing: Ctrl+Plus/Minus or Ctrl+Wheel for font size. Enter to finish, Esc to cancel.")
        ElseIf TypeOf d Is DrawableRectangleN OrElse TypeOf d Is DrawableEllipseN OrElse TypeOf d Is DrawableStarN OrElse TypeOf d Is DrawableArrowN Then
            extra = GetLocalizedUiText("bsiHintsShapeInside.Caption",
                "Inside label: use the toolbar text field next to ""Text inside…"" and the font/color menus.")
        End If

        If String.IsNullOrWhiteSpace(extra) Then Return baseHints
        Return baseHints & " | " & extra
    End Function

    Private Sub EnsureStatusInfoItem()
        If barManager1 Is Nothing OrElse bar3 Is Nothing Then Return
        If m_StatusInfoItem IsNot Nothing AndAlso m_HintsInfoItem IsNot Nothing AndAlso m_DimensionsInfoItem IsNot Nothing Then Return

        For Each item As BarItem In barManager1.Items
            Dim candidate = TryCast(item, BarStaticItem)
            If candidate IsNot Nothing AndAlso String.Equals(candidate.Name, "bsiDrawStatus", StringComparison.Ordinal) Then
                m_StatusInfoItem = candidate
            End If
            If candidate IsNot Nothing AndAlso String.Equals(candidate.Name, "bsiHints", StringComparison.Ordinal) Then
                m_HintsInfoItem = candidate
            End If
            If candidate IsNot Nothing AndAlso String.Equals(candidate.Name, "bsiDims", StringComparison.Ordinal) Then
                m_DimensionsInfoItem = candidate
            End If
        Next

        If m_StatusInfoItem Is Nothing Then
            m_StatusInfoItem = New BarStaticItem() With {
                .Name = "bsiDrawStatus",
                .Caption = ""
            }
            barManager1.Items.Add(m_StatusInfoItem)
        End If
        If m_HintsInfoItem Is Nothing Then
            m_HintsInfoItem = New BarStaticItem() With {
                .Name = "bsiHints",
                .Caption = GetLocalizedUiText("bsiHints.Caption", "Hints: Ctrl+Click or Shift+Click to add/remove selection. Shift+Wheel to zoom.")
            }
            barManager1.Items.Add(m_HintsInfoItem)
        End If
        If m_DimensionsInfoItem Is Nothing Then
            m_DimensionsInfoItem = New BarStaticItem() With {
                .Name = "bsiDims",
                .Caption = ""
            }
            barManager1.Items.Add(m_DimensionsInfoItem)
        End If

        EnsureBarItemLinkedToStatusBar(m_StatusInfoItem)
        EnsureBarItemLinkedToStatusBar(m_HintsInfoItem)
        EnsureBarItemLinkedToStatusBar(m_DimensionsInfoItem)
        UpdateDimensionsItemAlignment()
    End Sub

    Private Sub EnsureBarItemLinkedToStatusBar(item As BarItem)
        If bar3 Is Nothing OrElse item Is Nothing Then Return
        For Each link As BarItemLink In bar3.ItemLinks
            If link.Item Is item Then Return
        Next
        bar3.AddItem(item)
    End Sub

    Private Function GetLocalizedUiText(resourceName As String, fallback As String) As String
        Dim res As New ComponentResourceManager(GetType(DrawForm))
        Dim value As String = res.GetString(resourceName)
        If String.IsNullOrWhiteSpace(value) Then Return fallback
        Return value
    End Function

    Private Sub EnableSmoothCanvasRendering()
        Try
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or
                        ControlStyles.UserPaint Or
                        ControlStyles.OptimizedDoubleBuffer, True)
            Me.UpdateStyles()

            Dim pi = GetType(Control).GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
            If pi IsNot Nothing Then
                pi.SetValue(picCanvas, True, Nothing)
                If pnlCanvasHost IsNot Nothing Then pi.SetValue(pnlCanvasHost, True, Nothing)
                pi.SetValue(Panel1, True, Nothing)
            End If
        Catch
            ' Keep running even if reflection is restricted in some runtime contexts.
        End Try
    End Sub

    Private Sub ApplyArabicBoldUiIfNeeded()
        Dim isArabic As Boolean =
            String.Equals(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar", StringComparison.OrdinalIgnoreCase) OrElse
            Me.RightToLeft = RightToLeft.Yes
        If Not isArabic Then Return

        Dim boldFont As New Font(Me.Font, FontStyle.Bold)
        Me.Font = boldFont

        For Each ctrl As Control In GetAllControls(Me)
            ctrl.Font = boldFont
        Next

        If barManager1 IsNot Nothing Then
            For Each item As BarItem In barManager1.Items
                item.ItemAppearance.Normal.Font = boldFont
                item.ItemAppearance.Normal.Options.UseFont = True
                item.ItemAppearance.Hovered.Font = boldFont
                item.ItemAppearance.Hovered.Options.UseFont = True
            Next
        End If
    End Sub

    Private Iterator Function GetAllControls(parent As Control) As IEnumerable(Of Control)
        For Each ctrl As Control In parent.Controls
            Yield ctrl
            For Each child In GetAllControls(ctrl)
                Yield child
            Next
        Next
    End Function

    Private Sub UpdateCanvasSizePolicy()
        If Panel1 Is Nothing OrElse picCanvas Is Nothing OrElse pnlCanvasHost Is Nothing Then Return
        If m_DefaultCanvasSize.Width <= 0 OrElse m_DefaultCanvasSize.Height <= 0 Then
            m_DefaultCanvasSize = picCanvas.ClientSize
        End If

        ' Keep a stable design-time minimum canvas size, but grow with the form and with large images.
        ' This preserves the "default canvas" experience for small images while still allowing large content.
        Dim hostW As Integer = Math.Max(1, pnlCanvasHost.ClientSize.Width)
        Dim hostH As Integer = Math.Max(1, pnlCanvasHost.ClientSize.Height)

        Dim targetW As Integer = Math.Max(m_DefaultCanvasSize.Width, hostW)
        Dim targetH As Integer = Math.Max(m_DefaultCanvasSize.Height, hostH)

        If realWidth > 0 AndAlso realHeight > 0 Then
            targetW = Math.Max(targetW, realWidth)
            targetH = Math.Max(targetH, realHeight)
        End If

        Dim desiredSize As New Size(Math.Max(1, targetW), Math.Max(1, targetH))
        If picCanvas.Dock <> DockStyle.None Then
            picCanvas.Dock = DockStyle.None
            picCanvas.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        End If

        If pnlCanvasHost.AutoScroll = False Then
            pnlCanvasHost.AutoScroll = True
        End If

        If picCanvas.Size <> desiredSize Then
            picCanvas.Size = desiredSize
        End If

        ' Center only when canvas is smaller than viewport; otherwise keep origin for scrolling.
        Dim x As Integer = If(desiredSize.Width < hostW, (hostW - desiredSize.Width) \ 2, 0)
        Dim y As Integer = If(desiredSize.Height < hostH, (hostH - desiredSize.Height) \ 2, 0)
        Dim desiredLocation As New Point(x, y)
        If picCanvas.Location <> desiredLocation Then
            picCanvas.Location = desiredLocation
        End If
    End Sub

    Private Sub Panel1_SizeChanged(sender As Object, e As EventArgs) Handles Panel1.SizeChanged
        UpdateCanvasSizePolicy()
        RecenterSingleImageIfNeeded()
        picCanvas.Invalidate()
    End Sub

    Private Sub UpdateAnchorSizeForView()
        Dim desiredPixels As Integer = 11
        If m_SelectionProxy IsNot Nothing AndAlso Object.ReferenceEquals(m_PictureN.SelectedDrawable, m_SelectionProxy) Then
            desiredPixels = 13
        End If

        Dim sizeInWorld As Integer = CInt(Math.Round(desiredPixels / Math.Max(0.05F, m_ViewZoom)))
        sizeInWorld = Math.Max(8, Math.Min(40, sizeInWorld))

        If m_PictureN Is Nothing OrElse m_PictureN.Drawables Is Nothing Then Return
        For Each dr In m_PictureN.Drawables
            dr.AnchorSize = New Size(sizeInWorld, sizeInWorld)
        Next
        If m_SelectionProxy IsNot Nothing Then
            m_SelectionProxy.AnchorSize = New Size(sizeInWorld, sizeInWorld)
        End If
    End Sub

    Private Function NormalizeAngle(angle As Single) As Single
        Dim normalized = angle Mod 360.0F
        If normalized < 0 Then normalized += 360.0F
        Return normalized
    End Function

    Private Function TryGetLoadedImageDrawable(ByRef imageDrawable As DrawableImageN) As Boolean
        imageDrawable = TryCast(m_PictureN.SelectedDrawable, DrawableImageN)
        If imageDrawable IsNot Nothing AndAlso imageDrawable.Picture IsNot Nothing Then Return True

        Dim bestImage As DrawableImageN = Nothing
        Dim bestArea As Long = -1

        For Each dr As DrawableN In m_PictureN.Drawables
            Dim current = TryCast(dr, DrawableImageN)
            If current Is Nothing OrElse current.Picture Is Nothing Then Continue For

            Dim b = current.GetBounds()
            Dim area As Long = CLng(Math.Max(0, b.Width)) * CLng(Math.Max(0, b.Height))
            If area > bestArea Then
                bestArea = area
                bestImage = current
            End If
        Next

        imageDrawable = bestImage
        Return imageDrawable IsNot Nothing
    End Function

    'Private Function FindFirstDicomFile(folderPath As String) As String
    '    Try
    '        Dim files = Directory.GetFiles(folderPath)
    '        Array.Sort(files, StringComparer.OrdinalIgnoreCase)
    '        For Each file In files
    '            If IsDicomFile(file) Then Return file
    '            If TryOpenDicomFile(file) Then Return file
    '        Next
    '    Catch
    '    End Try
    '    Return ""
    'End Function

    'Private Function TryOpenDicomFile(path As String) As Boolean
    '    Try
    '        DicomFile.Open(path)
    '        Return True
    '    Catch
    '        Return False
    '    End Try
    'End Function

    'Private Sub LoadBackgroundFromPath(path As String)
    '    Dim img As Image = Nothing
    '    Dim loadError As String = ""
    '    If Not TryLoadImageFromPath(path, img, loadError) Then
    '        Throw New Exception(loadError)
    '    End If
    '    m_PictureN.BackgroundImage = img
    '    m_PictureN.BackgroundImageLayout = ImageLayout.Zoom
    '    picCanvas.Invalidate()
    'End Sub

    'Private Sub LoadDicomBackgroundFromPath(path As String)
    '    Dim img As Image = Nothing
    '    Dim loadError As String = ""
    '    If Not TryLoadDicomImage(path, img, loadError) Then
    '        Throw New Exception(If(String.IsNullOrWhiteSpace(loadError),
    '                              "Invalid or unsupported DICOM file.",
    '                              loadError))
    '    End If
    '    m_PictureN.BackgroundImage = img
    '    m_PictureN.BackgroundImageLayout = ImageLayout.Zoom
    '    picCanvas.Invalidate()
    'End Sub







#End Region
#End Region

End Class
