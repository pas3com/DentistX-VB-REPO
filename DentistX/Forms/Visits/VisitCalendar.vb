Imports System.Collections.Concurrent
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System.Drawing

Public Class VisitCalendar
    Inherits XtraUserControl

    Private containerPanel As New PanelControl()
    Private dayLabels As New List(Of LabelControl)
    Private headerLabels As New List(Of LabelControl)

    Private _month As Integer = DateTime.Now.Month
    Private _year As Integer = DateTime.Now.Year

    ' Constants for layout
    Private Const labelWidth As Integer = 44
    Private Const labelHeight As Integer = 44
    Private Shadows Const padding As Integer = 5

    ' Resize logic
    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 348
    Private Const OriginalPanelHeight As Integer = 348

    Public Sub New()
        InitializeComponent()
        SetupControl()
        GenerateCalendar()
        StoreOriginalBounds(Me)
        ResizeControlsProportionally()
    End Sub

    Private Sub SetupControl()
        containerPanel.Dock = DockStyle.Fill
        containerPanel.AutoScroll = True
        containerPanel.Appearance.BackColor = Color.WhiteSmoke
        containerPanel.BorderStyle = BorderStyles.Simple
        Me.Controls.Add(containerPanel)
    End Sub

    Public Property InMonth As Integer
        Get
            Return _month
        End Get
        Set(value As Integer)
            _month = value
            GenerateCalendar()
            StoreOriginalBounds(Me) ' Re-store after regenerating
            ResizeControlsProportionally()
        End Set
    End Property

    Public Property InYear As Integer
        Get
            Return _year
        End Get
        Set(value As Integer)
            _year = value
            GenerateCalendar()
            StoreOriginalBounds(Me) ' Re-store after regenerating
            ResizeControlsProportionally()
        End Set
    End Property

    Private Sub GenerateCalendar()
        containerPanel.Controls.Clear()
        dayLabels.Clear()
        headerLabels.Clear()

        Dim daysInMonth = DateTime.DaysInMonth(_year, _month)
        Dim firstDay = New DateTime(_year, _month, 1)
        Dim startDay = CInt(firstDay.DayOfWeek)

        ' Headers
        Dim dayNames = {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"}
        For i As Integer = 0 To 6
            Dim lblHeader As New LabelControl()
            lblHeader.Text = dayNames(i)
            lblHeader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            lblHeader.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            lblHeader.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
            lblHeader.AutoSizeMode = LabelAutoSizeMode.None
            lblHeader.Size = New Size(labelWidth, labelHeight)
            lblHeader.Location = New Point(padding + i * (labelWidth + padding), padding)
            lblHeader.BorderStyle = BorderStyles.NoBorder

            containerPanel.Controls.Add(lblHeader)
            headerLabels.Add(lblHeader)
        Next

        ' Day grid
        For week As Integer = 0 To 5
            For dayOfWeek As Integer = 0 To 6
                Dim dayNumber As Integer = week * 7 + dayOfWeek - startDay + 1
                Dim dayText As String = ""

                If dayNumber > 0 AndAlso dayNumber <= daysInMonth Then
                    Dim currentDate = New DateTime(_year, _month, dayNumber)
                    dayText = $"{dayNumber}{vbCrLf}{currentDate:ddd}"
                End If

                Dim lbl As New LabelControl()
                lbl.Text = dayText
                lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                lbl.Appearance.Font = New Font("Tahoma", 10)
                lbl.AutoSizeMode = LabelAutoSizeMode.None
                lbl.Size = New Size(labelWidth, labelHeight)
                lbl.Location = New Point(padding + dayOfWeek * (labelWidth + padding),
                                         padding * 2 + labelHeight + week * (labelHeight + padding))
                lbl.BorderStyle = BorderStyles.Simple
                lbl.Appearance.BackColor = Color.LightBlue
                lbl.Appearance.BorderColor = Color.SteelBlue

                containerPanel.Controls.Add(lbl)
                dayLabels.Add(lbl)
            Next
        Next
    End Sub

    ' Resize Support

    Private Sub VisitCalendar_Load(sender As Object, e As EventArgs) Handles Me.Load
        ResizeControlsProportionally()
    End Sub

    Private Sub VisitCalendar_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ResizeControlsProportionally()
    End Sub

    Private Sub StoreOriginalBounds(container As Control)
        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
    End Sub

    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return

        Dim widthRatio = CSng(Me.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.Height) / OriginalPanelHeight

        For Each kvp In controlBoundsCache
            kvp.Key.SetBounds(
                CInt(kvp.Value.X * widthRatio),
                CInt(kvp.Value.Y * heightRatio),
                CInt(kvp.Value.Width * widthRatio),
                CInt(kvp.Value.Height * heightRatio))
        Next
    End Sub

End Class

'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls
'Imports System.Drawing

'Public Class VisitCalendar
'    'Inherits XtraUserControl

'    Private containerPanel As New PanelControl()
'    Private dayLabels As New List(Of LabelControl)
'    Private headerLabels As New List(Of LabelControl)

'    Private _month As Integer = DateTime.Now.Month
'    Private _year As Integer = DateTime.Now.Year

'    ' Constants for layout
'    Private Const labelWidth As Integer = 44
'    Private Const labelHeight As Integer = 44
'    Private Const paddings As Integer = 5

'    Public Sub New()
'        InitializeComponent()
'        SetupControl()
'        GenerateCalendar()
'    End Sub

'    Private Sub SetupControl()
'        containerPanel.Dock = DockStyle.Fill
'        containerPanel.AutoScroll = True
'        containerPanel.Appearance.BackColor = Color.WhiteSmoke
'        containerPanel.BorderStyle = BorderStyles.Simple
'        Me.Controls.Add(containerPanel)
'    End Sub

'    Public Property InMonth As Integer
'        Get
'            Return _month
'        End Get
'        Set(value As Integer)
'            _month = value
'            GenerateCalendar()
'        End Set
'    End Property

'    Public Property InYear As Integer
'        Get
'            Return _year
'        End Get
'        Set(value As Integer)
'            _year = value
'            GenerateCalendar()
'        End Set
'    End Property

'    Private Sub GenerateCalendar()
'        containerPanel.Controls.Clear()
'        dayLabels.Clear()
'        headerLabels.Clear()

'        Dim daysInMonth = DateTime.DaysInMonth(_year, _month)
'        Dim firstDay = New DateTime(_year, _month, 1)
'        Dim startDay = CInt(firstDay.DayOfWeek) ' Sunday = 0

'        ' Draw headers (Sun to Sat)
'        Dim dayNames = {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"}
'        For i As Integer = 0 To 6
'            Dim lblHeader As New LabelControl()
'            lblHeader.Text = dayNames(i)
'            lblHeader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
'            lblHeader.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
'            lblHeader.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
'            lblHeader.AutoSizeMode = LabelAutoSizeMode.None
'            lblHeader.Size = New Size(labelWidth, labelHeight)
'            lblHeader.Location = New Point(paddings + i * (labelWidth + paddings), paddings)
'            lblHeader.BorderStyle = BorderStyles.NoBorder
'            containerPanel.Controls.Add(lblHeader)
'            headerLabels.Add(lblHeader)
'        Next

'        ' Draw day labels (6 weeks x 7 days)
'        For week As Integer = 0 To 5
'            For dayOfWeek As Integer = 0 To 6
'                Dim dayNumber As Integer = week * 7 + dayOfWeek - startDay + 1
'                Dim dayText As String = ""

'                If dayNumber > 0 AndAlso dayNumber <= daysInMonth Then
'                    Dim currentDate = New DateTime(_year, _month, dayNumber)
'                    dayText = $"{dayNumber}{vbCrLf}{currentDate:ddd}" ' E.g. "5\nTue"
'                End If

'                Dim lbl As New LabelControl()
'                lbl.Text = dayText
'                lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
'                lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
'                lbl.Appearance.Font = New Font("Tahoma", 10)
'                lbl.AutoSizeMode = LabelAutoSizeMode.None
'                lbl.Size = New Size(labelWidth, labelHeight)
'                lbl.Location = New Point(paddings + dayOfWeek * (labelWidth + paddings),
'                                         paddings * 2 + labelHeight + week * (labelHeight + paddings))
'                lbl.BorderStyle = BorderStyles.Simple
'                lbl.Appearance.BackColor = Color.LightBlue
'                lbl.Appearance.BorderColor = Color.SteelBlue

'                containerPanel.Controls.Add(lbl)
'                dayLabels.Add(lbl)
'            Next
'        Next
'    End Sub

'    Protected Overrides Sub OnSizeChanged(e As EventArgs)
'        MyBase.OnSizeChanged(e)
'        containerPanel.Refresh()
'    End Sub
'End Class
