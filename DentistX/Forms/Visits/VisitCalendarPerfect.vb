' VisitCalendarPerfect.vb
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System.Globalization

Public Class VisitCalendarPerfect

    Private headerLabels As New List(Of LabelControl)()
    Private dayControls As New List(Of DayControl)()
    Private _month As Integer = DateTime.Now.Month
    Private _year As Integer = DateTime.Now.Year

    Private WithEvents btnPrevMonth As New SimpleButton With {.Text = "<<", .Width = 40}
    Private WithEvents btnNextMonth As New SimpleButton With {.Text = ">>", .Width = 40}
    Private WithEvents btnPrevYear As New SimpleButton With {.Text = "⏮", .Width = 40}
    Private WithEvents btnNextYear As New SimpleButton With {.Text = "⏭", .Width = 40}

    Private lblMonthTitle As New LabelControl With {.AutoSizeMode = LabelAutoSizeMode.None, .Height = 30, .Font = New Font("Tahoma", 10, FontStyle.Bold)}
    Private cboFirstDay As New ComboBoxEdit With {.Width = 120, .Dock = DockStyle.Right, .Text = "Sunday"}
    Private cboLanguage As New ComboBoxEdit With {.Width = 100, .Dock = DockStyle.Left, .Text = "English"}

    Private _firstDayOfWeek As DayOfWeek = DayOfWeek.Sunday

    Public Property FilterPatient As String = ""
    Public Property FilterVisitType As String = ""

    Private Const labelWidth As Integer = 100
    Private Const labelHeight As Integer = 100
    Private Shadows Const padding As Integer = 5

    Public Sub New()
        InitializeComponent()
        SetupLayout()
        SetupComboBoxes()
        GenerateCalendar()
    End Sub

    Private Sub SetupLayout()
        containerPanel.AutoScroll = True
        containerPanel.BorderStyle = BorderStyles.Simple
        containerPanel.Appearance.BackColor = Color.WhiteSmoke

        topPanel.Height = 40
        topPanel.BorderThickness = 2

        btnPrevYear.Dock = DockStyle.Left
        btnPrevMonth.Dock = DockStyle.Left
        btnNextYear.Dock = DockStyle.Right
        btnNextMonth.Dock = DockStyle.Right
        lblMonthTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        lblMonthTitle.Dock = DockStyle.Fill

        topPanel.Controls.Add(btnPrevYear)
        topPanel.Controls.Add(btnPrevMonth)
        topPanel.Controls.Add(btnNextYear)
        topPanel.Controls.Add(btnNextMonth)
        topPanel.Controls.Add(lblMonthTitle)
        topPanel.Controls.Add(cboFirstDay)
        topPanel.Controls.Add(cboLanguage)
    End Sub

    Private Sub SetupComboBoxes()
        cboFirstDay.Properties.Items.AddRange([Enum].GetNames(GetType(DayOfWeek)))
        AddHandler cboFirstDay.SelectedIndexChanged, AddressOf cboFirstDay_SelectedIndexChanged

        cboLanguage.Properties.Items.AddRange({"English", "Arabic"})
        AddHandler cboLanguage.SelectedIndexChanged, AddressOf cboLanguage_SelectedIndexChanged
    End Sub

    Private Sub cboFirstDay_SelectedIndexChanged(sender As Object, e As EventArgs)
        _firstDayOfWeek = CType([Enum].Parse(GetType(DayOfWeek), cboFirstDay.Text), DayOfWeek)
        GenerateCalendar()
    End Sub

    Private Sub cboLanguage_SelectedIndexChanged(sender As Object, e As EventArgs)
        If cboLanguage.Text = "Arabic" Then
            Threading.Thread.CurrentThread.CurrentUICulture = SettingsRuntimeApply.CreateArabicRegionalCultureGregorian()
        Else
            Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        End If
        GenerateCalendar()
    End Sub

    Public Sub RefreshCalendar()
        For Each dayCtl In dayControls
            If dayCtl.Visible Then
                dayCtl.FilterPatient = Me.FilterPatient
                dayCtl.FilterVisitType = Me.FilterVisitType
                dayCtl.DayVisits = dayCtl.DayVisits
            End If
        Next
    End Sub

    Private Sub GenerateCalendar()
        Dim culture = Threading.Thread.CurrentThread.CurrentUICulture

        If culture.TwoLetterISOLanguageName = "ar" Then
            Me.RightToLeft = RightToLeft.Yes
            'Me.RightToLeftLayout = True
        Else
            Me.RightToLeft = RightToLeft.No
            'Me.RightToLeftLayout = False
        End If

        containerPanel.Controls.Clear()
        headerLabels.Clear()
        dayControls.Clear()

        lblMonthTitle.Text = New DateTime(_year, _month, 1).ToString("MMMM yyyy", culture)

        Dim days = [Enum].GetValues(GetType(DayOfWeek)).Cast(Of DayOfWeek)().ToList()
        Dim orderedDays = days.Skip(_firstDayOfWeek).Concat(days.Take(_firstDayOfWeek)).ToList()

        For i As Integer = 0 To 6
            Dim dayName = culture.DateTimeFormat.GetAbbreviatedDayName(orderedDays(i))
            Dim header As New LabelControl With {
                .Text = dayName,
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Size = New Size(labelWidth, 20),
                .Location = New Point(padding + i * (labelWidth + padding), padding),
                .BackColor = If(orderedDays(i) = _firstDayOfWeek, Color.MediumSlateBlue, Color.LightGray),
                .ForeColor = If(orderedDays(i) = _firstDayOfWeek, Color.White, Color.Black)
            }
            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            containerPanel.Controls.Add(header)
            headerLabels.Add(header)
        Next

        Dim firstDayOfMonth = New DateTime(_year, _month, 1)
        Dim startOffset = (7 + (firstDayOfMonth.DayOfWeek - _firstDayOfWeek)) Mod 7
        Dim daysInMonth = DateTime.DaysInMonth(_year, _month)

        For week As Integer = 0 To 5
            For dayIndex As Integer = 0 To 6
                Dim dayNumber As Integer = week * 7 + dayIndex - startOffset + 1
                Dim currentDate As Date = Nothing

                Dim dayCtl As New DayControl With {
                    .Size = New Size(labelWidth, labelHeight),
                    .Location = New Point(padding + dayIndex * (labelWidth + padding), padding * 2 + 20 + week * (labelHeight + padding)),
                    .FilterPatient = Me.FilterPatient,
                    .FilterVisitType = Me.FilterVisitType,
                    .UseVisualThemes = True,
                    .EnableIcons = True,
                    .FirstDayOfWeek = _firstDayOfWeek
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
        ' Implement custom handling logic if needed
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

    Private Sub btnPrevYear_Click(sender As Object, e As EventArgs) Handles btnPrevYear.Click
        _year -= 1
        GenerateCalendar()
    End Sub

    Private Sub btnNextYear_Click(sender As Object, e As EventArgs) Handles btnNextYear.Click
        _year += 1
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




'' VisitCalendarPerfect.vb
'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls

'Public Class VisitCalendarPerfect

'    Private headerLabels As New List(Of LabelControl)()
'    Private dayControls As New List(Of DayControl)()
'    Private _month As Integer = DateTime.Now.Month
'    Private _year As Integer = DateTime.Now.Year

'    Private WithEvents btnPrevMonth As New SimpleButton With {.Text = "<<", .Width = 40}
'    Private WithEvents btnNextMonth As New SimpleButton With {.Text = ">>", .Width = 40}
'    Private WithEvents btnPrevYear As New SimpleButton With {.Text = "⏮", .Width = 40}
'    Private WithEvents btnNextYear As New SimpleButton With {.Text = "⏭", .Width = 40}

'    Private lblMonthTitle As New LabelControl With {.AutoSizeMode = LabelAutoSizeMode.None, .Height = 30, .Font = New Font("Tahoma", 10, FontStyle.Bold)}
'    Private cboFirstDay As New ComboBoxEdit With {.Width = 120, .Dock = DockStyle.Right, .Text = "Sunday"}

'    Private _firstDayOfWeek As DayOfWeek = DayOfWeek.Sunday

'    Public Property FilterPatient As String = ""
'    Public Property FilterVisitType As String = ""

'    Private Const labelWidth As Integer = 100
'    Private Const labelHeight As Integer = 100
'    Private Const padding As Integer = 5

'    Public Sub New()
'        InitializeComponent()
'        SetupLayout()
'        SetupComboBox()
'        GenerateCalendar()
'    End Sub

'    Private Sub SetupLayout()
'        containerPanel.AutoScroll = True
'        containerPanel.BorderStyle = BorderStyles.Simple
'        containerPanel.Appearance.BackColor = Color.WhiteSmoke

'        topPanel.Height = 40
'        topPanel.BorderThickness = 2

'        btnPrevYear.Dock = DockStyle.Left
'        btnPrevMonth.Dock = DockStyle.Left
'        btnNextYear.Dock = DockStyle.Right
'        btnNextMonth.Dock = DockStyle.Right
'        lblMonthTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
'        lblMonthTitle.Dock = DockStyle.Fill

'        topPanel.Controls.Add(btnPrevYear)
'        topPanel.Controls.Add(btnPrevMonth)
'        topPanel.Controls.Add(btnNextYear)
'        topPanel.Controls.Add(btnNextMonth)
'        topPanel.Controls.Add(lblMonthTitle)
'        topPanel.Controls.Add(cboFirstDay)
'    End Sub

'    Private Sub SetupComboBox()
'        cboFirstDay.Properties.Items.AddRange([Enum].GetNames(GetType(DayOfWeek)))
'        AddHandler cboFirstDay.SelectedIndexChanged, AddressOf cboFirstDay_SelectedIndexChanged
'    End Sub

'    Private Sub cboFirstDay_SelectedIndexChanged(sender As Object, e As EventArgs)
'        _firstDayOfWeek = CType([Enum].Parse(GetType(DayOfWeek), cboFirstDay.Text), DayOfWeek)
'        GenerateCalendar()
'    End Sub

'    Public Sub RefreshCalendar()
'        For Each dayCtl In dayControls
'            If dayCtl.Visible Then
'                dayCtl.FilterPatient = Me.FilterPatient
'                dayCtl.FilterVisitType = Me.FilterVisitType
'                dayCtl.DayVisits = dayCtl.DayVisits ' Triggers re-filter
'            End If
'        Next
'    End Sub

'    Private Sub GenerateCalendar()
'        containerPanel.Controls.Clear()
'        headerLabels.Clear()
'        dayControls.Clear()

'        lblMonthTitle.Text = New DateTime(_year, _month, 1).ToString("MMMM yyyy")

'        Dim days = [Enum].GetValues(GetType(DayOfWeek)).Cast(Of DayOfWeek)().ToList()
'        Dim orderedDays = days.Skip(_firstDayOfWeek).Concat(days.Take(_firstDayOfWeek)).ToList()

'        For i As Integer = 0 To 6
'            Dim dayName = orderedDays(i).ToString().Substring(0, 3)
'            Dim header As New LabelControl With {
'                .Text = dayName,
'                .AutoSizeMode = LabelAutoSizeMode.None,
'                .Size = New Size(labelWidth, 20),
'                .Location = New Point(padding + i * (labelWidth + padding), padding),
'                .BackColor = If(orderedDays(i) = _firstDayOfWeek, Color.MediumSlateBlue, Color.LightGray),
'                .ForeColor = If(orderedDays(i) = _firstDayOfWeek, Color.White, Color.Black)
'            }
'            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
'            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

'            containerPanel.Controls.Add(header)
'            headerLabels.Add(header)
'        Next

'        Dim firstDayOfMonth = New DateTime(_year, _month, 1)
'        Dim startOffset = (7 + (firstDayOfMonth.DayOfWeek - _firstDayOfWeek)) Mod 7
'        Dim daysInMonth = DateTime.DaysInMonth(_year, _month)

'        For week As Integer = 0 To 5
'            For dayIndex As Integer = 0 To 6
'                Dim dayNumber As Integer = week * 7 + dayIndex - startOffset + 1
'                Dim currentDate As Date = Nothing

'                Dim dayCtl As New DayControl With {
'                    .Size = New Size(labelWidth, labelHeight),
'                    .Location = New Point(padding + dayIndex * (labelWidth + padding), padding * 2 + 20 + week * (labelHeight + padding)),
'                    .FilterPatient = Me.FilterPatient,
'                    .FilterVisitType = Me.FilterVisitType,
'                    .UseVisualThemes = True,
'                    .EnableIcons = True,
'                    .FirstDayOfWeek = _firstDayOfWeek
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
'                AddHandler dayCtl.VisitLabelClicked, AddressOf HandleVisitLabelClick

'                containerPanel.Controls.Add(dayCtl)
'                dayControls.Add(dayCtl)
'            Next
'        Next
'    End Sub

'    Private Sub HandleAddVisit(visDate As Date)
'        MessageBox.Show($"[Add Visit] Clicked for: {visDate:yyyy-MM-dd}")
'    End Sub

'    Private Sub HandleVisitLabelClick(visit As DayControl.VisitSummary)
'        ' Implement custom handling logic if needed
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

'    Private Sub btnPrevYear_Click(sender As Object, e As EventArgs) Handles btnPrevYear.Click
'        _year -= 1
'        GenerateCalendar()
'    End Sub

'    Private Sub btnNextYear_Click(sender As Object, e As EventArgs) Handles btnNextYear.Click
'        _year += 1
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


'{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{
'}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}

'' VisitCalendarPerfect.vb
'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls

'Public Class VisitCalendarPerfect

'    Private headerLabels As New List(Of LabelControl)()
'    Private dayControls As New List(Of DayControl)()
'    Private _month As Integer = DateTime.Now.Month
'    Private _year As Integer = DateTime.Now.Year

'    Private WithEvents btnPrevMonth As New SimpleButton With {.Text = "<<", .Width = 40}
'    Private WithEvents btnNextMonth As New SimpleButton With {.Text = ">>", .Width = 40}
'    Private lblMonthTitle As New LabelControl With {.AutoSizeMode = LabelAutoSizeMode.None, .Height = 30, .Font = New Font("Tahoma", 10, FontStyle.Bold)}
'    Private cboFirstDay As New ComboBoxEdit With {.Width = 120, .Dock = DockStyle.Right, .Text = "Sunday"}

'    Private _firstDayOfWeek As DayOfWeek = DayOfWeek.Sunday

'    Public Property FilterPatient As String = ""
'    Public Property FilterVisitType As String = ""

'    Private Const labelWidth As Integer = 100
'    Private Const labelHeight As Integer = 100
'    Private Const padding As Integer = 5

'    Public Sub New()
'        InitializeComponent()
'        SetupLayout()
'        SetupComboBox()
'        GenerateCalendar()
'    End Sub

'    Private Sub SetupLayout()
'        containerPanel.AutoScroll = True
'        containerPanel.BorderStyle = BorderStyles.Simple
'        containerPanel.Appearance.BackColor = Color.WhiteSmoke

'        topPanel.Height = 40
'        topPanel.BorderThickness = 2

'        btnPrevMonth.Dock = DockStyle.Left
'        btnNextMonth.Dock = DockStyle.Right
'        lblMonthTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
'        lblMonthTitle.Dock = DockStyle.Fill

'        topPanel.Controls.Add(btnPrevMonth)
'        topPanel.Controls.Add(btnNextMonth)
'        topPanel.Controls.Add(lblMonthTitle)
'        topPanel.Controls.Add(cboFirstDay)
'    End Sub

'    Private Sub SetupComboBox()
'        cboFirstDay.Properties.Items.AddRange([Enum].GetNames(GetType(DayOfWeek)))
'        AddHandler cboFirstDay.SelectedIndexChanged, AddressOf cboFirstDay_SelectedIndexChanged
'    End Sub

'    Private Sub cboFirstDay_SelectedIndexChanged(sender As Object, e As EventArgs)
'        _firstDayOfWeek = CType([Enum].Parse(GetType(DayOfWeek), cboFirstDay.Text), DayOfWeek)
'        GenerateCalendar()
'    End Sub

'    Public Sub RefreshCalendar()
'        For Each dayCtl In dayControls
'            If dayCtl.Visible Then
'                dayCtl.FilterPatient = Me.FilterPatient
'                dayCtl.FilterVisitType = Me.FilterVisitType
'                dayCtl.DayVisits = dayCtl.DayVisits ' Triggers re-filter
'            End If
'        Next
'    End Sub

'    Private Sub GenerateCalendar()
'        containerPanel.Controls.Clear()
'        headerLabels.Clear()
'        dayControls.Clear()

'        lblMonthTitle.Text = New DateTime(_year, _month, 1).ToString("MMMM yyyy")

'        Dim days = [Enum].GetValues(GetType(DayOfWeek)).Cast(Of DayOfWeek)().ToList()
'        Dim orderedDays = days.Skip(_firstDayOfWeek).Concat(days.Take(_firstDayOfWeek)).ToList()

'        For i As Integer = 0 To 6
'            Dim dayName = orderedDays(i).ToString().Substring(0, 3)
'            Dim header As New LabelControl With {
'                .Text = dayName,
'                .AutoSizeMode = LabelAutoSizeMode.None,
'                .Size = New Size(labelWidth, 20),
'                .Location = New Point(padding + i * (labelWidth + padding), padding),
'                .BackColor = If(orderedDays(i) = _firstDayOfWeek, Color.MediumSlateBlue, Color.LightGray),
'                .ForeColor = If(orderedDays(i) = _firstDayOfWeek, Color.White, Color.Black)
'            }
'            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
'            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

'            containerPanel.Controls.Add(header)
'            headerLabels.Add(header)
'        Next

'        Dim firstDayOfMonth = New DateTime(_year, _month, 1)
'        Dim startOffset = (7 + (firstDayOfMonth.DayOfWeek - _firstDayOfWeek)) Mod 7
'        Dim daysInMonth = DateTime.DaysInMonth(_year, _month)

'        For week As Integer = 0 To 5
'            For dayIndex As Integer = 0 To 6
'                Dim dayNumber As Integer = week * 7 + dayIndex - startOffset + 1
'                Dim currentDate As Date = Nothing

'                Dim dayCtl As New DayControl With {
'                    .Size = New Size(labelWidth, labelHeight),
'                    .Location = New Point(padding + dayIndex * (labelWidth + padding), padding * 2 + 20 + week * (labelHeight + padding)),
'                    .FilterPatient = Me.FilterPatient,
'                    .FilterVisitType = Me.FilterVisitType,
'                    .UseVisualThemes = True,
'                    .EnableIcons = True,
'                    .FirstDayOfWeek = _firstDayOfWeek
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
'                AddHandler dayCtl.VisitLabelClicked, AddressOf HandleVisitLabelClick

'                containerPanel.Controls.Add(dayCtl)
'                dayControls.Add(dayCtl)
'            Next
'        Next
'    End Sub

'    Private Sub HandleAddVisit(visDate As Date)
'        MessageBox.Show($"[Add Visit] Clicked for: {visDate:yyyy-MM-dd}")
'    End Sub

'    Private Sub HandleVisitLabelClick(visit As DayControl.VisitSummary)
'        ' Implement custom handling logic if needed
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

'|||||||||||||||||||||||||||||||||||||||||||||
'||||||||||||||||||||||||||||||||||||||||||||||||||||


'' VisitCalendarPerfect.vb
'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls

'Public Class VisitCalendarPerfect

'    'Private containerPanel As New PanelControl()
'    Private headerLabels As New List(Of LabelControl)()
'    Private dayControls As New List(Of DayControl)()
'    Private _month As Integer = DateTime.Now.Month
'    Private _year As Integer = DateTime.Now.Year

'    Private WithEvents btnPrevMonth As New SimpleButton With {.Text = "<<", .Width = 40}
'    Private WithEvents btnNextMonth As New SimpleButton With {.Text = ">>", .Width = 40}
'    Private lblMonthTitle As New LabelControl With {.AutoSizeMode = LabelAutoSizeMode.None, .Height = 30, .Font = New Font("Tahoma", 10, FontStyle.Bold)}
'    Private _filterPatient As String = ""
'    Public Property FilterPatient As String
'        Get
'            Return _filterPatient
'        End Get
'        Set(value As String)
'            If _filterPatient <> value Then
'                _filterPatient = value
'                RefreshCalendar()
'            End If
'        End Set
'    End Property

'    Private _filterVisitType As String = ""
'    Public Property FilterVisitType As String
'        Get
'            Return _filterVisitType
'        End Get
'        Set(value As String)
'            If _filterVisitType <> value Then
'                _filterVisitType = value
'                RefreshCalendar()
'            End If
'        End Set
'    End Property

'    Private Const labelWidth As Integer = 100
'    Private Const labelHeight As Integer = 100
'    Private Const padding As Integer = 5

'    Public Sub New()
'        InitializeComponent()
'        SetupLayout()
'        GenerateCalendar()
'    End Sub

'    Private Sub SetupLayout()
'        'Me.Controls.Clear()
'        'containerPanel.Dock = DockStyle.Fill
'        containerPanel.AutoScroll = True
'        containerPanel.BorderStyle = BorderStyles.Simple
'        containerPanel.Appearance.BackColor = Color.WhiteSmoke
'        'Me.Controls.Add(containerPanel)
'        'Dim topPanel As New PanelControl With {.Dock = DockStyle.Top, .Height = 40, .BorderStyle = BorderStyles.Office2003}
'        topPanel.Height = 40
'        topPanel.BorderThickness = 2
'        btnPrevMonth.Dock = DockStyle.Left
'        btnNextMonth.Dock = DockStyle.Right
'        lblMonthTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
'        lblMonthTitle.Dock = DockStyle.Fill
'        topPanel.Controls.Add(btnPrevMonth)
'        topPanel.Controls.Add(btnNextMonth)
'        topPanel.Controls.Add(lblMonthTitle)
'        'containerPanel.Controls.Add(topPanel)
'    End Sub

'    Public Sub RefreshCalendar()
'        For Each dayCtl In dayControls
'            If dayCtl.Visible Then
'                dayCtl.FilterPatient = Me.FilterPatient
'                dayCtl.FilterVisitType = Me.FilterVisitType
'                dayCtl.DayVisits = dayCtl.DayVisits ' Triggers re-filter
'            End If
'        Next
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
'                .Size = New Size(labelWidth, 20),
'                .Location = New Point(padding + i * (labelWidth + padding), padding)}
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
'                    .FilterVisitType = Me.FilterVisitType,
'                    .UseVisualThemes = True,
'                    .EnableIcons = True
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
'                AddHandler dayCtl.VisitLabelClicked, AddressOf HandleVisitLabelClick

'                containerPanel.Controls.Add(dayCtl)
'                dayControls.Add(dayCtl)
'            Next
'        Next
'    End Sub

'    Private Sub HandleAddVisit(visDate As Date)
'        MessageBox.Show($"[Add Visit] Clicked for: {visDate:yyyy-MM-dd}")
'    End Sub

'    Private Sub HandleVisitLabelClick(visit As DayControl.VisitSummary)
'        'MessageBox.Show($"[Visit Detail]\nPatient: {visit.PatientName}\nTime: {visit.VisTime:hh\:mm} - {visit.VisTimeEnd:hh\:mm}\nDetail: {visit.VisDetail}")
'        '' Replace MessageBox with custom form
'        'Dim form As New VisitDetailForm(visit)
'        'form.ShowDialog()
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


'******************************
'*                            *
'*                            *
'*                            *
'******************************

'' VisitCalendarPerfect.vb
'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls

'Public Class VisitCalendarPerfect
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
