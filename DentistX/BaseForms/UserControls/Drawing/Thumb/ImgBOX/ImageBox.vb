Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Windows.Forms.Layout


' Cyotek ImageBox
' Copyright (c) 2010 Cyotek. All Rights Reserved.
' http://cyotek.com

<DefaultProperty("Image"), ToolboxBitmap(GetType(ImageBox))>
Partial Public Class ImageBox
    Inherits ScrollableControl

#Region "Private Class Member Declarations  "

    Private Shared ReadOnly MinZoom As Integer = 10
    Private Shared ReadOnly MaxZoom As Integer = 3500

#End Region
#Region "Private Member Declarations  "



    Private _autoCenter As Boolean
    Private _autoPan As Boolean
    Private _borderStyle As BorderStyle
    Private _gridCellSize As Integer
    Private _gridColor As Color
    Private _gridColorAlternate As Color
    <Category("Property Changed")>
    Private _gridScale As ImageBoxGridScale
    Private _gridTile As Bitmap
    Private _image As System.Drawing.Image
    Private _interpolationMode As InterpolationMode
    Private _invertMouse As Boolean
    Private _isPanning As Boolean
    Private _sizeToFit As Boolean
    Private _startMousePosition As Point
    Private _startScrollPosition As Point
    Private _texture As TextureBrush
    Private _zoom As Integer
    Private _zoomIncrement As Integer
    Private _gridDisplayMode As ImageBoxGridDisplayMode

#End Region

#Region "Public Constructors  "

    Public Sub New()
        InitializeComponent()

        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        Me.SetStyle(ControlStyles.StandardDoubleClick, False)
        Me.UpdateStyles()

        Me.BackColor = Color.White
        Me.AutoSize = True
        Me.GridScale = ImageBoxGridScale.Small
        Me.GridDisplayMode = ImageBoxGridDisplayMode.Client
        Me.GridColor = Color.Gainsboro
        Me.GridColorAlternate = Color.White
        Me.GridCellSize = 8
        Me.BorderStyle = BorderStyle.FixedSingle
        Me.AutoPan = True
        Me.Zoom = 100
        Me.ZoomIncrement = 20
        Me.InterpolationMode = InterpolationMode.Default
        Me.AutoCenter = True
    End Sub

#End Region

#Region "Events  "

    <Category("Property Changed")>
    Public Event AutoCenterChanged As EventHandler

    <Category("Property Changed")>
    Public Event AutoPanChanged As EventHandler

    <Category("Property Changed")>
    Public Event BorderStyleChanged As EventHandler

    <Category("Property Changed")>
    Public Event GridCellSizeChanged As EventHandler

    <Category("Property Changed")>
    Public Event GridColorAlternateChanged As EventHandler

    <Category("Property Changed")>
    Public Event GridColorChanged As EventHandler

    <Category("Property Changed")>
    Public Event GridDisplayModeChanged As EventHandler

    <Category("Property Changed")>
    Public Event GridScaleChanged As EventHandler

    <Category("Property Changed")>
    Public Event ImageChanged As EventHandler

    <Category("Property Changed")>
    Public Event InterpolationModeChanged As EventHandler

    <Category("Property Changed")>
    Public Event InvertMouseChanged As EventHandler

    <Category("Property Changed")>
    Public Event PanEnd As EventHandler

    <Category("Property Changed")>
    Public Event PanStart As EventHandler

    <Category("Property Changed")>
    Public Event SizeToFitChanged As EventHandler

    <Category("Property Changed")>
    Public Event ZoomChanged As EventHandler

    <Category("Property Changed")>
    Public Event ZoomIncrementChanged As EventHandler

#End Region

#Region "Overriden Properties  "

    <Browsable(True), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue(True)>
    Public Overrides Property AutoSize() As Boolean
        Get
            Return MyBase.AutoSize
        End Get
        Set(ByVal value As Boolean)
            If MyBase.AutoSize <> value Then
                MyBase.AutoSize = value
                Me.AdjustLayout()
            End If
        End Set
    End Property

    <DefaultValue(GetType(Color), "White")>
    Public Overrides Property BackColor() As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(ByVal value As Color)
            MyBase.BackColor = value
        End Set
    End Property

    <Category("Appearance"), System.ComponentModel.DefaultValue(CType(Nothing, Object))>
    Public Overridable Property Image() As Image
        Get
            Return _image
        End Get
        Set(ByVal value As Image)
            If _image IsNot value Then
                _image = value
                Me.OnImageChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Private _sizeMode As PictureBoxSizeMode
    <Browsable(True), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ImageSizeMode() As PictureBoxSizeMode 'ImageLayout
        Get
            Return _sizeMode 'MyBase.BackgroundImageLayout
        End Get
        Set(ByVal value As PictureBoxSizeMode)
            If _sizeMode <> value Then
                _sizeMode = value
                Me.AdjustLayout()
            End If
        End Set
    End Property

    Private _layoutMode As ImageLayout
    <Browsable(True), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ImageLayout() As ImageLayout 'PictureBoxSizeMode '
        Get
            Return _layoutMode 'MyBase.BackgroundImageLayout
        End Get
        Set(ByVal value As ImageLayout)
            If _layoutMode <> value Then
                _layoutMode = value
                Me.AdjustLayout()
            End If
        End Set
    End Property


    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Overrides Property BackgroundImage() As Image
        Get
            Return MyBase.BackgroundImage
        End Get
        Set(ByVal value As Image)
            MyBase.BackgroundImage = value
        End Set
    End Property



    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Overrides Property BackgroundImageLayout() As ImageLayout
        Get
            Return MyBase.BackgroundImageLayout
        End Get
        Set(ByVal value As ImageLayout)
            MyBase.BackgroundImageLayout = value
        End Set
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Overrides Property Font() As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
        End Set
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

#End Region

#Region "Public Overridden Methods  "

    Public Overrides Function GetPreferredSize(ByVal proposedSize As Size) As Size
        Dim imgSize As Size

        If Me.Image IsNot Nothing Then
            Dim imgWidth As Integer
            Dim imgHeight As Integer

            ' get the size of the image
            imgWidth = Me.ScaledImageWidth
            imgHeight = Me.ScaledImageHeight

            ' add an offset based on padding
            imgWidth += Me.Padding.Horizontal
            imgHeight += Me.Padding.Vertical

            ' add an offset based on the border style
            imgWidth += Me.GetBorderOffset()
            imgHeight += Me.GetBorderOffset()

            imgSize = New Size(imgWidth, imgHeight)
        Else
            imgSize = MyBase.GetPreferredSize(proposedSize)
        End If

        Return imgSize
    End Function

#End Region

#Region "Protected Overridden Methods  "

    ''' <summary> 
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If

            If _texture IsNot Nothing Then
                _texture.Dispose()
                _texture = Nothing
            End If

            If _gridTile IsNot Nothing Then
                _gridTile.Dispose()
                _gridTile = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Protected Overrides Function IsInputKey(ByVal keyData As Keys) As Boolean
        Dim result As Boolean

        If (keyData And Keys.Right) = Keys.Right Or (keyData And Keys.Left) = Keys.Left Or (keyData And Keys.Up) = Keys.Up Or (keyData And Keys.Down) = Keys.Down Then
            result = True
        Else
            result = MyBase.IsInputKey(keyData)
        End If

        Return result
    End Function

    Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
        MyBase.OnBackColorChanged(e)

        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnDockChanged(ByVal e As EventArgs)
        MyBase.OnDockChanged(e)

        If Me.Dock <> DockStyle.None Then
            Me.AutoSize = False
        End If
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
        MyBase.OnKeyDown(e)

        Select Case e.KeyCode
            Case Keys.Left
                Me.AdjustScroll(-(If(e.Modifiers = Keys.None, Me.HorizontalScroll.SmallChange, Me.HorizontalScroll.LargeChange)), 0)
            Case Keys.Right
                Me.AdjustScroll(If(e.Modifiers = Keys.None, Me.HorizontalScroll.SmallChange, Me.HorizontalScroll.LargeChange), 0)
            Case Keys.Up
                Me.AdjustScroll(0, -(If(e.Modifiers = Keys.None, Me.VerticalScroll.SmallChange, Me.VerticalScroll.LargeChange)))
            Case Keys.Down
                Me.AdjustScroll(0, If(e.Modifiers = Keys.None, Me.VerticalScroll.SmallChange, Me.VerticalScroll.LargeChange))
        End Select
    End Sub

    Protected Overrides Sub OnMouseClick(ByVal e As MouseEventArgs)
        If Not Me.IsPanning AndAlso Not Me.SizeToFit Then
            If e.Button = MouseButtons.Left AndAlso Control.ModifierKeys = Keys.None Then
                If Me.Zoom >= 100 Then
                    Me.Zoom = CInt(Math.Truncate(Math.Round(CDbl(Me.Zoom + 100) / 100))) * 100
                ElseIf Me.Zoom >= 75 Then
                    Me.Zoom = 100
                Else
                    Me.Zoom = CInt(Math.Truncate(Me.Zoom / 0.75F))
                End If
            ElseIf e.Button = MouseButtons.Right OrElse (e.Button = MouseButtons.Left AndAlso Control.ModifierKeys <> Keys.None) Then
                If Me.Zoom > 100 AndAlso Me.Zoom <= 125 Then
                    Me.Zoom = 100
                ElseIf Me.Zoom > 100 Then
                    Me.Zoom = CInt(Math.Truncate(Math.Round(CDbl(Me.Zoom - 100) / 100))) * 100
                Else
                    Me.Zoom = CInt(Math.Truncate(Me.Zoom * 0.75F))
                End If
            End If
        End If

        MyBase.OnMouseClick(e)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)

        If Not Me.Focused Then
            Me.Focus()
        End If
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If Image IsNot Nothing Then
            If MouseMoveOverImageEvent IsNot Nothing Then
                Dim p As Point = PointOnImage(e.Location)
                If p.X >= 0 AndAlso p.X < Image.Width AndAlso p.Y >= 0 AndAlso p.Y < Image.Height Then
                    Dim ne As New MouseEventArgs(e.Button, e.Clicks, p.X, p.Y, e.Delta)
                    RaiseEvent MouseMoveOverImage(Me, ne)
                End If
            End If
        End If
        If e.Button = MouseButtons.Left AndAlso Me.AutoPan AndAlso Me.Image IsNot Nothing Then
            If Not Me.IsPanning Then
                _startMousePosition = e.Location
                Me.IsPanning = True
            End If

            If Me.IsPanning Then
                Dim x As Integer
                Dim y As Integer
                Dim position As Point

                If Not Me.InvertMouse Then
                    x = -_startScrollPosition.X + (_startMousePosition.X - e.Location.X)
                    y = -_startScrollPosition.Y + (_startMousePosition.Y - e.Location.Y)
                Else
                    x = -(_startScrollPosition.X + (_startMousePosition.X - e.Location.X))
                    y = -(_startScrollPosition.Y + (_startMousePosition.Y - e.Location.Y))
                End If

                position = New Point(x, y)

                Me.UpdateScrollPosition(position)
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MyBase.OnMouseUp(e)

        If Me.IsPanning Then
            Me.IsPanning = False
        End If
    End Sub

    Protected Overrides Sub OnMouseWheel(ByVal e As MouseEventArgs)
        If Not Me.SizeToFit Then
            Dim increment As Integer

            If Control.ModifierKeys = Keys.None Then
                increment = Me.ZoomIncrement
            Else
                increment = Me.ZoomIncrement * 5
            End If

            If e.Delta < 0 Then
                increment = -increment
            End If

            Me.Zoom += increment
        End If
    End Sub

    Protected Overrides Sub OnPaddingChanged(ByVal e As System.EventArgs)
        MyBase.OnPaddingChanged(e)
        Me.AdjustLayout()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim innerRectangle As Rectangle

        ' draw the borders
        Select Case Me.BorderStyle
            Case BorderStyle.FixedSingle
                ControlPaint.DrawBorder(e.Graphics, Me.ClientRectangle, Me.ForeColor, ButtonBorderStyle.Solid)
            Case BorderStyle.Fixed3D
                ControlPaint.DrawBorder3D(e.Graphics, Me.ClientRectangle, Border3DStyle.Sunken)
        End Select

        innerRectangle = Me.GetInsideViewPort()

        ' draw the background
        Using brush As New SolidBrush(Me.BackColor)
            e.Graphics.FillRectangle(brush, innerRectangle)
        End Using

        If _texture IsNot Nothing AndAlso Me.GridDisplayMode <> ImageBoxGridDisplayMode.None Then
            Select Case Me.GridDisplayMode
                Case ImageBoxGridDisplayMode.Image
                    Dim fillRectangle As Rectangle

                    fillRectangle = Me.GetImageViewPort()
                    e.Graphics.FillRectangle(_texture, fillRectangle)

                    If Not fillRectangle.Equals(innerRectangle) Then
                        fillRectangle.Inflate(1, 1)
                        ControlPaint.DrawBorder(e.Graphics, fillRectangle, Me.ForeColor, ButtonBorderStyle.Solid)
                    End If
                Case ImageBoxGridDisplayMode.Client
                    e.Graphics.FillRectangle(_texture, innerRectangle)
            End Select
        End If

        ' draw the image
        If Me.Image IsNot Nothing Then
            Me.DrawImage(e.Graphics)
        End If

        MyBase.OnPaint(e)
    End Sub

    Protected Overrides Sub OnParentChanged(ByVal e As System.EventArgs)
        MyBase.OnParentChanged(e)
        Me.AdjustLayout()
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        Me.AdjustLayout()

        MyBase.OnResize(e)
    End Sub

    Protected Overrides Sub OnScroll(ByVal se As ScrollEventArgs)
        Me.Invalidate()

        MyBase.OnScroll(se)
    End Sub

#End Region

#Region "Public Methods  "

    Public Overridable Function GetImageViewPort() As Rectangle
        Dim viewPort As Rectangle

        If Me.Image IsNot Nothing Then
            Dim innerRectangle As Rectangle
            Dim offset As Point

            innerRectangle = Me.GetInsideViewPort()

            If Me.AutoCenter Then
                Dim x As Integer
                Dim y As Integer

                x = If(Not Me.HScroll, (innerRectangle.Width - (Me.ScaledImageWidth + Me.Padding.Horizontal)) \ 2, 0)
                y = If(Not Me.VScroll, (innerRectangle.Height - (Me.ScaledImageHeight + Me.Padding.Vertical)) \ 2, 0)

                offset = New Point(x, y)
            Else
                offset = Point.Empty
            End If

            viewPort = New Rectangle(offset.X + innerRectangle.Left + Me.Padding.Left, offset.Y + innerRectangle.Top + Me.Padding.Top, innerRectangle.Width - (Me.Padding.Horizontal + (offset.X * 2)), innerRectangle.Height - (Me.Padding.Vertical + (offset.Y * 2)))
        Else
            viewPort = Rectangle.Empty
        End If

        Return viewPort
    End Function

    Public Function GetInsideViewPort() As Rectangle
        Return Me.GetInsideViewPort(False)
    End Function

    Public Overridable Function GetInsideViewPort(ByVal includePadding As Boolean) As Rectangle
        Dim leftInner As Integer
        Dim topInner As Integer
        Dim widthInner As Integer
        Dim heightInner As Integer
        Dim borderOffset As Integer

        borderOffset = Me.GetBorderOffset()
        leftInner = borderOffset
        topInner = borderOffset
        widthInner = Me.ClientSize.Width - (borderOffset * 2)
        heightInner = Me.ClientSize.Height - (borderOffset * 2)

        If includePadding Then
            leftInner += Me.Padding.Left
            topInner += Me.Padding.Top
            widthInner -= Me.Padding.Horizontal
            heightInner -= Me.Padding.Vertical
        End If

        Return New Rectangle(leftInner, topInner, widthInner, heightInner)
    End Function

    Public Overridable Function GetSourceImageRegion() As Rectangle
        Dim sourceLeft As Integer
        Dim sourceTop As Integer
        Dim sourceWidth As Integer
        Dim sourceHeight As Integer
        Dim viewPort As Rectangle

        Dim imgRegion As Rectangle

        If Me.Image IsNot Nothing Then
            viewPort = Me.GetImageViewPort()
            sourceLeft = CInt(Math.Truncate(-Me.AutoScrollPosition.X / Me.ZoomFactor))
            sourceTop = CInt(Math.Truncate(-Me.AutoScrollPosition.Y / Me.ZoomFactor))
            sourceWidth = CInt(Math.Truncate(viewPort.Width / Me.ZoomFactor))
            sourceHeight = CInt(Math.Truncate(viewPort.Height / Me.ZoomFactor))

            imgRegion = New Rectangle(sourceLeft, sourceTop, sourceWidth, sourceHeight)
        Else
            imgRegion = Rectangle.Empty
        End If

        Return imgRegion
    End Function

    Public Overridable Sub ZoomToFit()
        If Me.Image IsNot Nothing Then
            Dim innerRectangle As Rectangle

            Dim zoomValue As Double
            Dim aspectRatio As Double

            Me.AutoScrollMinSize = Size.Empty

            innerRectangle = Me.GetInsideViewPort(True)

            If Me.Image.Width > Me.Image.Height Then
                aspectRatio = (CDbl(innerRectangle.Width)) / (CDbl(Me.Image.Width))
                zoomValue = aspectRatio * 100.0

                If innerRectangle.Height < ((Me.Image.Height * zoomValue) / 100.0) Then
                    aspectRatio = (CDbl(innerRectangle.Height)) / (CDbl(Me.Image.Height))
                    zoomValue = aspectRatio * 100.0
                End If
            Else
                aspectRatio = (CDbl(innerRectangle.Height)) / (CDbl(Me.Image.Height))
                zoomValue = aspectRatio * 100.0

                If innerRectangle.Width < ((Me.Image.Width * zoomValue) / 100.0) Then
                    aspectRatio = (CDbl(innerRectangle.Width)) / (CDbl(Me.Image.Width))
                    zoomValue = aspectRatio * 100.0
                End If
            End If

            Me.Zoom = CInt(Math.Truncate(Math.Round(Math.Floor(zoomValue))))
        End If
    End Sub

#End Region

#Region "Public Properties  "

    <DefaultValue(True), Category("Appearance")>
    Public Property AutoCenter() As Boolean
        Get
            Return _autoCenter
        End Get
        Set(ByVal value As Boolean)
            If _autoCenter <> value Then
                _autoCenter = value
                Me.OnAutoCenterChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <DefaultValue(True), Category("Behavior")>
    Public Property AutoPan() As Boolean
        Get
            Return _autoPan
        End Get
        Set(ByVal value As Boolean)
            If _autoPan <> value Then
                _autoPan = value
                Me.OnAutoPanChanged(EventArgs.Empty)

                If value Then
                    Me.SizeToFit = False
                End If
            End If
        End Set
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Shadows Property AutoScrollMinSize() As Size
        Get
            Return MyBase.AutoScrollMinSize
        End Get
        Set(ByVal value As Size)
            MyBase.AutoScrollMinSize = value
        End Set
    End Property

    <Category("Appearance"), DefaultValue(GetType(BorderStyle), "FixedSingle")>
    Public Property BorderStyle() As BorderStyle
        Get
            Return _borderStyle
        End Get
        Set(ByVal value As BorderStyle)
            If _borderStyle <> value Then
                _borderStyle = value
                Me.OnBorderStyleChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <Category("Appearance"), DefaultValue(8)>
    Public Property GridCellSize() As Integer
        Get
            Return _gridCellSize
        End Get
        Set(ByVal value As Integer)
            If _gridCellSize <> value Then
                _gridCellSize = value
                Me.OnGridCellSizeChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <Category("Appearance"), DefaultValue(GetType(Color), "Gainsboro")>
    Public Property GridColor() As Color
        Get
            Return _gridColor
        End Get
        Set(ByVal value As Color)
            If _gridColor <> value Then
                _gridColor = value
                Me.OnGridColorChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <Category("Appearance"), DefaultValue(GetType(Color), "White")>
    Public Property GridColorAlternate() As Color
        Get
            Return _gridColorAlternate
        End Get
        Set(ByVal value As Color)
            If _gridColorAlternate <> value Then
                _gridColorAlternate = value
                Me.OnGridColorAlternateChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <DefaultValue(ImageBoxGridDisplayMode.Client), Category("Appearance")>
    Public Property GridDisplayMode() As ImageBoxGridDisplayMode
        Get
            Return _gridDisplayMode
        End Get
        Set(ByVal value As ImageBoxGridDisplayMode)
            If _gridDisplayMode <> value Then
                _gridDisplayMode = value
                Me.OnGridDisplayModeChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <DefaultValue(GetType(ImageBoxGridScale), "Small"), Category("Appearance")>
    Public Property GridScale() As ImageBoxGridScale
        Get
            Return _gridScale
        End Get
        Set(ByVal value As ImageBoxGridScale)
            If _gridScale <> value Then
                _gridScale = value
                Me.OnGridScaleChanged(EventArgs.Empty)
            End If
        End Set
    End Property



    <DefaultValue(InterpolationMode.Default), Category("Appearance")>
    Public Property InterpolationMode() As InterpolationMode
        Get
            Return _interpolationMode
        End Get
        Set(ByVal value As InterpolationMode)
            If value = InterpolationMode.Invalid Then
                value = InterpolationMode.Default
            End If

            If _interpolationMode <> value Then
                _interpolationMode = value
                Me.OnInterpolationModeChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <DefaultValue(False), Category("Behavior")>
    Public Property InvertMouse() As Boolean
        Get
            Return _invertMouse
        End Get
        Set(ByVal value As Boolean)
            If _invertMouse <> value Then
                _invertMouse = value
                Me.OnInvertMouseChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <DefaultValue(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)>
    Public Property IsPanning() As Boolean
        Get
            Return _isPanning
        End Get
        Protected Set(ByVal value As Boolean)
            If _isPanning <> value Then
                _isPanning = value
                _startScrollPosition = Me.AutoScrollPosition

                If value Then
                    Me.Cursor = Cursors.SizeAll
                    Me.OnPanStart(EventArgs.Empty)
                Else
                    Me.Cursor = Cursors.Default
                    Me.OnPanEnd(EventArgs.Empty)
                End If
            End If
        End Set
    End Property

    <DefaultValue(False), Category("Appearance")>
    Public Property SizeToFit() As Boolean
        Get
            Return _sizeToFit
        End Get
        Set(ByVal value As Boolean)
            If _sizeToFit <> value Then
                _sizeToFit = value
                Me.OnSizeToFitChanged(EventArgs.Empty)

                If value Then
                    Me.AutoPan = False
                End If
            End If
        End Set
    End Property

    <DefaultValue(100), Category("Appearance")>
    Public Property Zoom() As Integer
        Get
            Return _zoom
        End Get
        Set(ByVal value As Integer)
            If value < ImageBox.MinZoom Then
                value = ImageBox.MinZoom
            ElseIf value > ImageBox.MaxZoom Then
                value = ImageBox.MaxZoom
            End If

            If _zoom <> value Then
                _zoom = value
                Me.OnZoomChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <DefaultValue(20), Category("Behavior")>
    Public Property ZoomIncrement() As Integer
        Get
            Return _zoomIncrement
        End Get
        Set(ByVal value As Integer)
            If _zoomIncrement <> value Then
                _zoomIncrement = value
                Me.OnZoomIncrementChanged(EventArgs.Empty)
            End If
        End Set
    End Property

#End Region

#Region "Private Methods  "

    Private Function GetBorderOffset() As Integer
        Dim offset As Integer

        Select Case Me.BorderStyle
            Case BorderStyle.Fixed3D
                offset = 2
            Case BorderStyle.FixedSingle
                offset = 1
            Case Else
                offset = 0
        End Select

        Return offset
    End Function

    Private Sub InitializeGridTile()
        If _texture IsNot Nothing Then
            _texture.Dispose()
        End If

        If _gridTile IsNot Nothing Then
            _gridTile.Dispose()
        End If

        If Me.GridDisplayMode <> ImageBoxGridDisplayMode.None AndAlso Me.GridCellSize <> 0 Then
            _gridTile = Me.CreateGridTileImage(Me.GridCellSize, Me.GridColor, Me.GridColorAlternate)
            _texture = New TextureBrush(_gridTile)
        End If

        Me.Invalidate()
    End Sub

#End Region

#Region "Protected Properties  "

    Protected Overridable ReadOnly Property ScaledImageHeight() As Integer
        Get
            Return If(Me.Image IsNot Nothing, CInt(Math.Truncate(Me.Image.Size.Height * Me.ZoomFactor)), 0)
        End Get
    End Property

    Protected Overridable ReadOnly Property ScaledImageWidth() As Integer
        Get
            Return If(Me.Image IsNot Nothing, CInt(Math.Truncate(Me.Image.Size.Width * Me.ZoomFactor)), 0)
        End Get
    End Property

    Protected Overridable ReadOnly Property ZoomFactor() As Double
        Get
            Return CDbl(Me.Zoom) / 100
        End Get
    End Property

#End Region

#Region "Protected Methods  "

    Protected Overridable Sub AdjustLayout()
        If Me.AutoSize Then
            Me.AdjustSize()
        ElseIf Me.SizeToFit Then
            Me.ZoomToFit()
        ElseIf Me.AutoScroll Then
            Me.AdjustViewPort()
        End If
        Me.Invalidate()
    End Sub

    Protected Overridable Sub AdjustScroll(ByVal x As Integer, ByVal y As Integer)
        Dim scrollPosition As Point

        scrollPosition = New Point(Me.HorizontalScroll.Value + x, Me.VerticalScroll.Value + y)

        Me.UpdateScrollPosition(scrollPosition)
    End Sub

    Protected Overridable Sub AdjustSize()
        If Me.AutoSize AndAlso Me.Dock = DockStyle.None Then
            MyBase.Size = MyBase.PreferredSize
        End If
    End Sub

    Protected Overridable Sub AdjustViewPort()
        If Me.AutoScroll AndAlso Me.Image IsNot Nothing Then
            Me.AutoScrollMinSize = New Size(Me.ScaledImageWidth + Me.Padding.Horizontal, Me.ScaledImageHeight + Me.Padding.Vertical)
        End If
    End Sub

    Protected Overridable Function CreateGridTileImage(ByVal cellSize As Integer, ByVal firstColor As Color, ByVal secondColor As Color) As Bitmap
        Dim result As Bitmap
        Dim bmpWidth As Integer
        Dim bmpHeight As Integer
        Dim gridScale As Single

        ' rescale the cell size
        Select Case Me.GridScale
            Case ImageBoxGridScale.Medium
                gridScale = 1.5F
            Case ImageBoxGridScale.Large
                gridScale = 2
            Case Else
                gridScale = 1
        End Select

        cellSize = CInt(Math.Truncate(cellSize * gridScale))

        ' draw the tile
        bmpWidth = cellSize * 2
        bmpHeight = cellSize * 2
        result = New Bitmap(bmpWidth, bmpHeight)
        Using g As Graphics = Graphics.FromImage(result)
            Using brush As New SolidBrush(firstColor)
                g.FillRectangle(brush, New Rectangle(0, 0, bmpWidth, bmpHeight))
            End Using

            Using brush As New SolidBrush(secondColor)
                g.FillRectangle(brush, New Rectangle(0, 0, cellSize, cellSize))
                g.FillRectangle(brush, New Rectangle(cellSize, cellSize, cellSize, cellSize))
            End Using
        End Using

        Return result
    End Function

    Protected Overridable Sub DrawImage(ByVal g As Graphics)
        g.InterpolationMode = Me.InterpolationMode
        g.DrawImage(Me.Image, Me.GetImageViewPort(), Me.GetSourceImageRegion(), GraphicsUnit.Pixel)
    End Sub

    Protected Overridable Sub OnAutoCenterChanged(ByVal e As EventArgs)
        Me.Invalidate()

        RaiseEvent AutoCenterChanged(Me, e)
    End Sub

    Protected Overridable Sub OnAutoPanChanged(ByVal e As EventArgs)
        RaiseEvent AutoPanChanged(Me, e)
    End Sub

    Protected Overridable Sub OnBorderStyleChanged(ByVal e As EventArgs)
        Me.AdjustLayout()

        RaiseEvent BorderStyleChanged(Me, e)
    End Sub

    Protected Overridable Sub OnGridCellSizeChanged(ByVal e As EventArgs)
        Me.InitializeGridTile()

        RaiseEvent GridCellSizeChanged(Me, e)
    End Sub

    Protected Overridable Sub OnGridColorAlternateChanged(ByVal e As EventArgs)
        Me.InitializeGridTile()

        RaiseEvent GridColorAlternateChanged(Me, e)
    End Sub

    Protected Overridable Sub OnGridColorChanged(ByVal e As EventArgs)
        Me.InitializeGridTile()

        RaiseEvent GridColorChanged(Me, e)

    End Sub

    Protected Overridable Sub OnGridDisplayModeChanged(ByVal e As EventArgs)
        Me.InitializeGridTile()
        Me.Invalidate()

        RaiseEvent GridDisplayModeChanged(Me, e)
    End Sub

    Protected Overridable Sub OnGridScaleChanged(ByVal e As EventArgs)
        Me.InitializeGridTile()

        RaiseEvent GridScaleChanged(Me, e)
    End Sub

    Protected Overridable Sub OnImageChanged(ByVal e As EventArgs)
        Me.AdjustLayout()

        RaiseEvent ImageChanged(Me, e)
    End Sub

    Protected Overridable Sub OnInterpolationModeChanged(ByVal e As EventArgs)
        Me.Invalidate()

        RaiseEvent InterpolationModeChanged(Me, e)
    End Sub

    Protected Overridable Sub OnInvertMouseChanged(ByVal e As EventArgs)
        RaiseEvent InvertMouseChanged(Me, e)
    End Sub

    Protected Overridable Sub OnPanEnd(ByVal e As EventArgs)
        RaiseEvent PanEnd(Me, e)
    End Sub

    Protected Overridable Sub OnPanStart(ByVal e As EventArgs)
        RaiseEvent PanStart(Me, e)
    End Sub

    Protected Overridable Sub OnSizeToFitChanged(ByVal e As EventArgs)
        Me.AdjustLayout()

        RaiseEvent SizeToFitChanged(Me, e)
    End Sub

    Protected Overridable Sub OnZoomChanged(ByVal e As EventArgs)
        Me.AdjustLayout()

        RaiseEvent ZoomChanged(Me, e)
    End Sub

    Protected Overridable Sub OnZoomIncrementChanged(ByVal e As EventArgs)
        RaiseEvent ZoomIncrementChanged(Me, e)
    End Sub

    Protected Overridable Sub UpdateScrollPosition(ByVal position As Point)
        Me.AutoScrollPosition = position
        Me.Invalidate()
        Me.OnScroll(New ScrollEventArgs(ScrollEventType.ThumbPosition, 0))
    End Sub

#End Region


#Region "ExtendPicBox"

#Region "Events"

    ''' <summary>
    ''' Handler for when the mouse moves over the image part of the picture box
    ''' </summary>
    Public Delegate Sub MouseMoveOverImageHandler(ByVal sender As Object, ByVal e As MouseEventArgs)

    ''' <summary>
    ''' Occurs when the mouse have moved over the image part of a picture box
    ''' </summary>
    Public Event MouseMoveOverImage As MouseMoveOverImageHandler

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets the mouse position relative to the <see cref="PictureBox.Image">Image</see> top left corner
    ''' </summary>
    ''' <value>The location of the mouse translated onto the <see cref="PictureBox.Image">Image</see> .</value>
    Public ReadOnly Property ePos() As Point
        Get
            Dim local As Point = PointToClient(MousePosition)
            Return PointOnImage(local)
        End Get
    End Property
#End Region


#Region "Methods"
#Region "Public Methods"

    Protected Overridable Sub AdjustSizeMode()
        If Me.AutoSize Then
            Me.AdjustSize()
        ElseIf Me.SizeToFit Then
            Me.ZoomToFit()
        ElseIf Me.AutoScroll Then
            Me.AdjustViewPort()
        End If
        Me.Invalidate()
        Select Case _layoutMode
            Case PictureBoxSizeMode.Normal
                If Image Is Nothing Then Return

            Case PictureBoxSizeMode.AutoSize

            Case PictureBoxSizeMode.CenterImage

            Case PictureBoxSizeMode.StretchImage

            Case PictureBoxSizeMode.Zoom

        End Select
        ' Normal = 0
        ' Summary:
        '     The image is placed in the upper-left corner of the System.Windows.Forms.PictureBox.
        '     The image is clipped if it is larger than the System.Windows.Forms.PictureBox
        '     it is contained in.

        ' StretchImage = 1
        ' Summary:
        '     The image within the System.Windows.Forms.PictureBox is stretched or shrunk to
        '     fit the size of the System.Windows.Forms.PictureBox.

        'AutoSize = 2
        ' Summary:
        '     The System.Windows.Forms.PictureBox is sized equal to the size of the image that
        '     it contains.

        ' CenterImage = 3
        ' Summary:
        '     The image is displayed in the center if the System.Windows.Forms.PictureBox is
        '     larger than the image. If the image is larger than the System.Windows.Forms.PictureBox,
        '     the picture is placed in the center of the System.Windows.Forms.PictureBox and
        '     the outside edges are clipped.

        'Zoom = 4
        ' Summary:
        '     The size of the image is increased or decreased maintaining the size ratio.

    End Sub



    ''' <summary>
    ''' Translates a point to coordinates relative to the <see cref="PictureBox.Image">Image</see>.
    ''' The supplied point is taken relativce to the control's upper left corner
    ''' </summary>
    ''' <param name="controlCoordinates">The point to translate, relative to the control's upper left corner.</param>
    ''' <returns>A new point representing where over the <see cref="PictureBox.Image">Image</see> the supplied point is.</returns>
    Public Function PointOnImage(ByVal controlCoordinates As Point) As Point
        Select Case ImageLayout
            Case PictureBoxSizeMode.Normal
                Return TranslateNormalMousePosition(controlCoordinates)
            Case PictureBoxSizeMode.AutoSize
                Return TranslateAutoSizeMousePosition(controlCoordinates)
            Case PictureBoxSizeMode.CenterImage
                Return TranslateCenterImageMousePosition(controlCoordinates)
            Case PictureBoxSizeMode.StretchImage
                Return TranslateStretchImageMousePosition(controlCoordinates)
            Case PictureBoxSizeMode.Zoom
                Return TranslateZoomMousePosition(controlCoordinates)
        End Select
        Throw New NotImplementedException("PictureBox.SizeMode was not in a valid state")
    End Function
#End Region

#Region "Protected Methods"
    ''' <summary>
    ''' Gets the mouse position over the image when the <see cref="PictureBox">PictureBox's</see> <see cref="PictureBox.SizeMode">SizeMode</see> is set to AutoSize
    ''' </summary>
    ''' <param name="coordinates">Point to translate</param>
    ''' <returns>A point relative to the top left corner of the <see cref="PictureBox.Image">Image</see></returns>
    ''' <remarks>
    ''' In AutoSize mode, the <see cref="PictureBox">PictureBox</see> is automagically resized* to the size of the <see cref="PictureBox.Image">Image.</see>
    ''' Thus, the image is at the top left corner of the control, and no translation takes place.
    ''' * This is not necessary true.  The <see cref="PictureBox">PictureBox</see> may NOT be resized depending on how it is docked in it's parent.
    ''' However, even in these cases no translation is needed, as the image is rendered the same as if it was in Normal mode
    ''' </remarks>
    Protected Function TranslateAutoSizeMousePosition(ByVal coordinates As Point) As Point
        'TODO: When we implement scrolling, we will have to make sure we test that properly. As of now, not sure how the rendering will take place
        Return coordinates
    End Function

    ''' <summary>
    ''' Gets the mouse position over the image when the <see cref="PictureBox">PictureBox's</see> <see cref="PictureBox.SizeMode">SizeMode</see> is set to Zoom
    ''' </summary>
    ''' <param name="coordinates">Point to translate</param>
    ''' <returns>A point relative to the top left corner of the <see cref="PictureBox.Image">Image</see>
    ''' If the Image is null, no translation is performed
    ''' </returns>
    Protected Function TranslateZoomMousePosition(ByVal coordinates As Point) As Point
        '	test to make sure our image is not null
        If Image Is Nothing Then
            Return coordinates
        End If
        '	Make sure our control width and height are not 0 and our image width and height are not 0
        If Width = 0 OrElse Height = 0 OrElse Image.Width = 0 OrElse Image.Height = 0 Then
            Return coordinates
        End If
        '	This is the one that gets a little tricky.  Essentially, need to check the aspect ratio of the image to the aspect ratio of the control
        ' to determine how it is being rendered
        Dim imageAspect As Single = CSng(Image.Width) / Image.Height
        Dim controlAspect As Single = CSng(Width) / Height
        Dim newX As Single = coordinates.X
        Dim newY As Single = coordinates.Y
        If imageAspect > controlAspect Then
            '	This means that we are limited by width, meaning the image fills up the entire control from left to right
            Dim ratioWidth As Single = CSng(Image.Width) / Width
            newX *= ratioWidth
            'INSTANT VB NOTE: The variable scale was renamed since Visual Basic does not handle local variables named the same as class members well:
            Dim scale_Renamed As Single = CSng(Width) / Image.Width
            Dim displayHeight As Single = scale_Renamed * Image.Height
            Dim diffHeight As Single = Height - displayHeight
            diffHeight /= 2
            newY -= diffHeight
            newY /= scale_Renamed
        Else
            '	This means that we are limited by height, meaning the image fills up the entire control from top to bottom
            Dim ratioHeight As Single = CSng(Image.Height) / Height
            newY *= ratioHeight
            'INSTANT VB NOTE: The variable scale was renamed since Visual Basic does not handle local variables named the same as class members well:
            Dim scale_Renamed As Single = CSng(Height) / Image.Height
            Dim displayWidth As Single = scale_Renamed * Image.Width
            Dim diffWidth As Single = Width - displayWidth
            diffWidth /= 2
            newX -= diffWidth
            newX /= scale_Renamed
        End If
        Return New Point(CInt(Math.Truncate(newX)), CInt(Math.Truncate(newY)))
    End Function

    ''' <summary>
    ''' Gets the mouse position over the image when the <see cref="PictureBox">PictureBox's</see> <see cref="PictureBox.SizeMode">SizeMode</see> is set to StretchImage
    ''' </summary>
    ''' <param name="coordinates">Point to translate</param>
    ''' <returns>A point relative to the top left corner of the <see cref="PictureBox.Image">Image</see>
    ''' If the Image is null, no translation is performed
    ''' </returns>
    Protected Function TranslateStretchImageMousePosition(ByVal coordinates As Point) As Point
        '	test to make sure our image is not null
        If Image Is Nothing Then
            Return coordinates
        End If
        '	Make sure our control width and height are not 0
        If Width = 0 OrElse Height = 0 Then
            Return coordinates
        End If
        '	First, get the ratio (image to control) the height and width
        Dim ratioWidth As Single = CSng(Image.Width) / Width
        Dim ratioHeight As Single = CSng(Image.Height) / Height
        '	Scale the points by our ratio
        Dim newX As Single = coordinates.X
        Dim newY As Single = coordinates.Y
        newX *= ratioWidth
        newY *= ratioHeight
        Return New Point(CInt(Math.Truncate(newX)), CInt(Math.Truncate(newY)))
    End Function

    ''' <summary>
    ''' Gets the mouse position over the image when the <see cref="PictureBox">PictureBox's</see> <see cref="PictureBox.SizeMode">SizeMode</see> is set to Center
    ''' </summary>
    ''' <param name="coordinates">Point to translate</param>
    ''' <returns>A point relative to the top left corner of the <see cref="PictureBox.Image">Image</see>
    ''' If the Image is null, no translation is performed
    ''' </returns>
    Protected Function TranslateCenterImageMousePosition(ByVal coordinates As Point) As Point
        '	Test to make sure our image is not null
        If Image Is Nothing Then
            Return coordinates
        End If
        '	First, get the top location (relative to the top left of the control) of the image itself
        ' To do this, we know that the image is centered, so we get the difference in size (width and height) of the image to the control
        Dim diffWidth As Integer = Width - Image.Width
        Dim diffHeight As Integer = Height - Image.Height
        '	We now divide in half to accomadate each side of the image
        diffWidth \= 2
        diffHeight \= 2
        '	Finally, we subtract this numer from the original coordinates
        ' In the case that the image is larger than the picture box, this still works
        coordinates.X -= diffWidth
        coordinates.Y -= diffHeight
        Return coordinates
    End Function

    ''' <summary>
    ''' Gets the mouse position over the image when the <see cref="PictureBox">PictureBox's</see> <see cref="PictureBox.SizeMode">SizeMode</see> is set to Normal
    ''' </summary>
    ''' <param name="coordinates">Point to translate</param>
    ''' <returns>A point relative to the top left corner of the <see cref="PictureBox.Image">Image</see></returns>
    ''' <remarks>
    ''' In normal mode, the image is placed in the top left corner, and as such the point does not need to be translated.
    ''' The resulting point is the same as the original point
    ''' </remarks>
    Protected Function TranslateNormalMousePosition(ByVal coordinates As Point) As Point
        '	TODO: When we implement scrolling in this, we will need to test for scroll offset
        '	NOTE: As it stands now, this could be made static, but in the future we will be making this handle scaling
        Return coordinates
    End Function

    ' Raises the Control.MouseMove event.
    ' If the mouse is over the image, raises the MouseMoveOverImage event.
    ' Parameter e contains the event data.
    'Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
    '    MyBase.OnMouseMove(e)
    '    If Image IsNot Nothing Then
    '        If MouseMoveOverImageEvent IsNot Nothing Then
    '            Dim p As Point = PointOnImage(e.Location)
    '            If p.X >= 0 AndAlso p.X < Image.Width AndAlso p.Y >= 0 AndAlso p.Y < Image.Height Then
    '                Dim ne As New MouseEventArgs(e.Button, e.Clicks, p.X, p.Y, e.Delta)
    '                RaiseEvent MouseMoveOverImage(Me, ne)
    '            End If
    '        End If
    '    End If

    'End Sub


#End Region
#End Region

#End Region
End Class

