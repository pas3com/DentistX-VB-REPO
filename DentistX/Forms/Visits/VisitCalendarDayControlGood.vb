' VisitCalendarDayControlGood.vb
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

Public Class VisitCalendarDayControlGood
    'Inherits XtraUserControl

    'Private containerPanel As New PanelControl()
    Private headerLabels As New List(Of LabelControl)()
    Private dayControls As New List(Of DayControl)()
    Private _month As Integer = DateTime.Now.Month
    Private _year As Integer = DateTime.Now.Year

    Private WithEvents btnPrevMonth As New SimpleButton With {.Text = "<<", .Width = 40}
    Private WithEvents btnNextMonth As New SimpleButton With {.Text = ">>", .Width = 40}
    Private lblMonthTitle As New LabelControl With {.AutoSizeMode = LabelAutoSizeMode.None, .Height = 30, .Font = New Font("Tahoma", 10, FontStyle.Bold)}


    Private _filterPatient As String = ""
    Public Property FilterPatient As String
        Get
            Return _filterPatient
        End Get
        Set(value As String)
            If _filterPatient <> value Then
                _filterPatient = value
                RefreshCalendar()
            End If
        End Set
    End Property

    Private _filterVisitType As String = ""
    Public Property FilterVisitType As String
        Get
            Return _filterVisitType
        End Get
        Set(value As String)
            If _filterVisitType <> value Then
                _filterVisitType = value
                RefreshCalendar()
            End If
        End Set
    End Property


    Private Const labelWidth As Integer = 100
    Private Const labelHeight As Integer = 100
    Private Shadows Const padding As Integer = 5

    Public Sub New()
        InitializeComponent()
        SetupLayout()
        GenerateCalendar()
    End Sub

    Private Sub SetupLayout()
        'Me.Controls.Clear()

        'containerPanel.Dock = DockStyle.Fill
        containerPanel.AutoScroll = True
        containerPanel.BorderStyle = BorderStyles.Simple
        containerPanel.Appearance.BackColor = Color.WhiteSmoke
        'Me.Controls.Add(containerPanel)

        'Dim topPanel As New PanelControl With {.Dock = DockStyle.Top, .Height = 40, .BorderStyle = BorderStyles.Office2003}
        topPanel.Height = 40
        topPanel.BorderThickness = 2
        btnPrevMonth.Dock = DockStyle.Left
        btnNextMonth.Dock = DockStyle.Right
        lblMonthTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        lblMonthTitle.Dock = DockStyle.Fill
        topPanel.Controls.Add(btnPrevMonth)
        topPanel.Controls.Add(btnNextMonth)
        topPanel.Controls.Add(lblMonthTitle)
        'containerPanel.Controls.Add(topPanel)


    End Sub


    Public Sub RefreshCalendar()
        For Each dayCtl In dayControls
            If dayCtl.Visible Then
                dayCtl.FilterPatient = Me.FilterPatient
                dayCtl.FilterVisitType = Me.FilterVisitType
                dayCtl.DayVisits = dayCtl.DayVisits ' Triggers re-filter
            End If
        Next
    End Sub



    Private Sub GenerateCalendar()
        containerPanel.Controls.Clear()
        headerLabels.Clear()
        dayControls.Clear()

        lblMonthTitle.Text = New DateTime(_year, _month, 1).ToString("MMMM yyyy")

        Dim daysInMonth = DateTime.DaysInMonth(_year, _month)
        Dim firstDay = New DateTime(_year, _month, 1)
        Dim startDay = CInt(firstDay.DayOfWeek)
        Dim dayNames = {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"}

        ' Add weekday headers
        For i As Integer = 0 To 6
            Dim header As New LabelControl With {
                .Text = dayNames(i),
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Size = New Size(labelWidth, 20),
                .Location = New Point(padding + i * (labelWidth + padding), padding)}
            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            containerPanel.Controls.Add(header)
            headerLabels.Add(header)
        Next

        ' Day grid
        For week As Integer = 0 To 5
            For dayOfWeek As Integer = 0 To 6
                Dim dayNumber As Integer = week * 7 + dayOfWeek - startDay + 1
                Dim currentDate As Date = Nothing

                Dim dayCtl As New DayControl With {
                    .Size = New Size(labelWidth, labelHeight),
                    .Location = New Point(padding + dayOfWeek * (labelWidth + padding),
                                          padding * 2 + 20 + week * (labelHeight + padding)),
                    .FilterPatient = Me.FilterPatient,
                    .FilterVisitType = Me.FilterVisitType,
                    .UseVisualThemes = True
                }

                If dayNumber > 0 AndAlso dayNumber <= daysInMonth Then
                    currentDate = New DateTime(_year, _month, dayNumber)
                    dayCtl.DayNumber = dayNumber.ToString()
                    dayCtl.DayVisits = currentDate
                Else
                    dayCtl.Enabled = False
                    dayCtl.Visible = False
                End If

                AddHandler dayCtl.AddVisitRequested, AddressOf HandleAddVisit
                AddHandler dayCtl.VisitLabelClicked, AddressOf HandleVisitLabelClick

                containerPanel.Controls.Add(dayCtl)
                dayControls.Add(dayCtl)
            Next
        Next
    End Sub

    Private Sub HandleAddVisit(visDate As Date)
        MessageBox.Show($"[Add Visit] Clicked for: {visDate:yyyy-MM-dd}")
    End Sub

    Private Sub HandleVisitLabelClick(visit As DayControl.VisitSummary)
        'MessageBox.Show($"[Visit Detail]\nPatient: {visit.PatientName}\nTime: {visit.VisTime:hh\:mm} - {visit.VisTimeEnd:hh\:mm}\nDetail: {visit.VisDetail}")
        '' Replace MessageBox with custom form
        'Dim form As New VisitDetailForm(visit)
        'form.ShowDialog()
    End Sub

    Private Sub btnPrevMonth_Click(sender As Object, e As EventArgs) Handles btnPrevMonth.Click
        If _month = 1 Then
            _month = 12
            _year -= 1
        Else
            _month -= 1
        End If
        GenerateCalendar()
    End Sub

    Private Sub btnNextMonth_Click(sender As Object, e As EventArgs) Handles btnNextMonth.Click
        If _month = 12 Then
            _month = 1
            _year += 1
        Else
            _month += 1
        End If
        GenerateCalendar()
    End Sub

    Public Property InMonth As Integer
        Get
            Return _month
        End Get
        Set(value As Integer)
            _month = value
            GenerateCalendar()
        End Set
    End Property

    Public Property InYear As Integer
        Get
            Return _year
        End Get
        Set(value As Integer)
            _year = value
            GenerateCalendar()
        End Set
    End Property
End Class








'' VisitCalendarDayControlGood.vb
'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls

'Public Class VisitCalendarDayControlGood
'    'Inherits XtraUserControl

'    Private containerPanel As New PanelControl()
'    Private headerLabels As New List(Of LabelControl)()
'    Private dayControls As New List(Of DayControl)()
'    Private _month As Integer = DateTime.Now.Month
'    Private _year As Integer = DateTime.Now.Year

'    Private WithEvents btnPrevMonth As New SimpleButton With {.Text = "<<", .Width = 40}
'    Private WithEvents btnNextMonth As New SimpleButton With {.Text = ">>", .Width = 40}
'    Private lblMonthTitle As New LabelControl With {.AutoSizeMode = LabelAutoSizeMode.None, .Height = 30, .Font = New Font("Tahoma", 10, FontStyle.Bold)}
'    ', .AppearanceText = { .HAlignment = DevExpress.Utils.HorzAlignment.Center}}

'    Public Property FilterPatient As String = ""
'    Public Property FilterVisitType As String = ""

'    Private Const labelWidth As Integer = 100
'    Private Const labelHeight As Integer = 100
'    Private Const padding As Integer = 5

'    Public Sub New()
'        InitializeComponent()
'        SetupLayout()
'        GenerateCalendar()
'    End Sub

'    Private Sub SetupLayout()
'        Me.Controls.Clear()

'        Dim topPanel As New PanelControl With {.Dock = DockStyle.Top, .Height = 40, .BorderStyle = BorderStyles.NoBorder}
'        btnPrevMonth.Dock = DockStyle.Left
'        btnNextMonth.Dock = DockStyle.Right
'        lblMonthTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
'        lblMonthTitle.Dock = DockStyle.Fill
'        topPanel.Controls.Add(btnPrevMonth)
'        topPanel.Controls.Add(btnNextMonth)
'        topPanel.Controls.Add(lblMonthTitle)
'        Me.Controls.Add(topPanel)

'        containerPanel.Dock = DockStyle.Fill
'        containerPanel.AutoScroll = True
'        containerPanel.BorderStyle = BorderStyles.Simple
'        containerPanel.Appearance.BackColor = Color.Wheat
'        Me.Controls.Add(containerPanel)
'    End Sub

'    Private Sub GenerateCalendar()
'        containerPanel.Controls.Clear()
'        headerLabels.Clear()
'        dayControls.Clear()

'        lblMonthTitle.Text = New DateTime(_year, _month, 1).ToString("MMMM yyyy")

'        Dim daysInMonth = DateTime.DaysInMonth(_year, _month)
'        Dim firstDay = New DateTime(_year, _month, 1)
'        Dim startDay = CInt(firstDay.DayOfWeek)
'        Dim dayNames = {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"}

'        ' Add weekday headers
'        For i As Integer = 0 To 6
'            Dim header As New LabelControl With {
'                .Text = dayNames(i),
'                .AutoSizeMode = LabelAutoSizeMode.None,
'                .Size = New Size(labelWidth, 30),
'                .Location = New Point(padding + i * (labelWidth + padding), padding)
'            }
'            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
'            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

'            containerPanel.Controls.Add(header)
'            headerLabels.Add(header)
'        Next

'        ' Day grid
'        For week As Integer = 0 To 5
'            For dayOfWeek As Integer = 0 To 6
'                Dim dayNumber As Integer = week * 7 + dayOfWeek - startDay + 1
'                Dim currentDate As Date = Nothing

'                Dim dayCtl As New DayControl With {
'                    .Size = New Size(labelWidth, labelHeight),
'                    .Location = New Point(padding + dayOfWeek * (labelWidth + padding),
'                                          padding * 2 + 20 + week * (labelHeight + padding)),
'                    .FilterPatient = Me.FilterPatient,
'                    .FilterVisitType = Me.FilterVisitType
'                }

'                If dayNumber > 0 AndAlso dayNumber <= daysInMonth Then
'                    currentDate = New DateTime(_year, _month, dayNumber)
'                    dayCtl.DayNumber = dayNumber.ToString()
'                    dayCtl.DayVisits = currentDate
'                Else
'                    dayCtl.Enabled = False
'                    dayCtl.Visible = False
'                End If

'                AddHandler dayCtl.AddVisitRequested, AddressOf HandleAddVisit

'                containerPanel.Controls.Add(dayCtl)
'                dayControls.Add(dayCtl)
'            Next
'        Next
'    End Sub

'    Private Sub HandleAddVisit(visDate As Date)
'        MessageBox.Show($"[Add Visit] Clicked for: {visDate:yyyy-MM-dd}")
'    End Sub

'    Private Sub btnPrevMonth_Click(sender As Object, e As EventArgs) Handles btnPrevMonth.Click
'        If _month = 1 Then
'            _month = 12
'            _year -= 1
'        Else
'            _month -= 1
'        End If
'        GenerateCalendar()
'    End Sub

'    Private Sub btnNextMonth_Click(sender As Object, e As EventArgs) Handles btnNextMonth.Click
'        If _month = 12 Then
'            _month = 1
'            _year += 1
'        Else
'            _month += 1
'        End If
'        GenerateCalendar()
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
'End Class
