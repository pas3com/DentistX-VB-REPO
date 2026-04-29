' VisitCalendarDayControl.vb
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System.Globalization

Public Class VisitCalendarDayControl

    Private headerLabels As New List(Of LabelControl)()
    Private dayControls As New List(Of DayControl)()
    Private _month As Integer = DateTime.Now.Month
    Private _year As Integer = DateTime.Now.Year

    'Private WithEvents btnPrevMonth As New SimpleButton With {.Text = "<<", .Width = 40, .ToolTip = "Previous Month"}
    'Private WithEvents btnNextMonth As New SimpleButton With {.Text = ">>", .Width = 40, .ToolTip = "Next Month"}
    'Private WithEvents btnPrevYear As New SimpleButton With {.Text = "⏮", .Width = 40, .ToolTip = "Previous Year"}
    'Private WithEvents btnNextYear As New SimpleButton With {.Text = "⏭", .Width = 40, .ToolTip = "Next Year"}

    'Private lblMonthTitle As New LabelControl With {.AutoSizeMode = LabelAutoSizeMode.None, .Height = 30, .Font = New Font("Tahoma", 10, FontStyle.Bold)}
    'Private cboFirstDay As New ComboBoxEdit With {.Width = 120, .Dock = DockStyle.Right, .Text = "Sunday"}
    'Private cboLanguage As New ComboBoxEdit With {.Width = 100, .Dock = DockStyle.Left, .Text = "English"}

    Private _firstDayOfWeek As DayOfWeek = DayOfWeek.Sunday
    Private dayOfWeekValues As List(Of DayOfWeek)

    Public Property FilterPatient As String = ""
    Public Property FilterVisitType As String = ""

    Private Const labelWidth As Integer = 150
    Private Const labelHeight As Integer = 85
    Private Const paddings As Integer = 5

    Public Sub New()
        InitializeComponent()
        SetupLayout()
        SetupComboBoxes()
        UpdateTooltips()
        GenerateCalendar()
    End Sub

    Private Sub SetupLayout()
        containerPanel2.AutoScroll = True
        containerPanel2.BorderStyle = BorderStyles.Simple
        containerPanel2.Appearance.BackColor = Color.WhiteSmoke

        'topPanel.Height = 51
        'topPanel.BorderThickness = 2

        'btnPrevYear.Dock = DockStyle.Left
        'btnPrevMonth.Dock = DockStyle.Left
        'btnNextYear.Dock = DockStyle.Right
        'btnNextMonth.Dock = DockStyle.Right
        'lblMonthTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        'lblMonthTitle.Dock = DockStyle.Fill

        'topPanel.Controls.Add(btnPrevYear)
        'topPanel.Controls.Add(btnPrevMonth)
        'topPanel.Controls.Add(btnNextYear)
        'topPanel.Controls.Add(btnNextMonth)
        'topPanel.Controls.Add(lblMonthTitle)
        'topPanel.Controls.Add(cboFirstDay)
        'topPanel.Controls.Add(cboLanguage)
    End Sub

    Private Sub SetupComboBoxes()
        dayOfWeekValues = [Enum].GetValues(GetType(DayOfWeek)).Cast(Of DayOfWeek).ToList()
        cboLanguage.Properties.Items.AddRange({"English", "Arabic"})
        AddHandler cboLanguage.SelectedIndexChanged, AddressOf cboLanguage_SelectedIndexChanged
        RefreshFirstDayItems()
        AddHandler cboFirstDay.SelectedIndexChanged, AddressOf cboFirstDay_SelectedIndexChanged
    End Sub

    Private Sub RefreshFirstDayItems()
        Dim culture = Threading.Thread.CurrentThread.CurrentUICulture
        cboFirstDay.Properties.Items.Clear()

        For Each day In dayOfWeekValues
            cboFirstDay.Properties.Items.Add(culture.DateTimeFormat.GetDayName(day))
        Next

        cboFirstDay.SelectedIndex = dayOfWeekValues.IndexOf(_firstDayOfWeek)
    End Sub

    Private Sub UpdateTooltips()
        If cboLanguage.Text = "Arabic" Then
            btnPrevMonth.ToolTip = "الشهر السابق"
            btnNextMonth.ToolTip = "الشهر التالي"
            btnPrevYear.ToolTip = "السنة السابقة"
            btnNextYear.ToolTip = "السنة التالية"
        Else
            btnPrevMonth.ToolTip = "Previous Month"
            btnNextMonth.ToolTip = "Next Month"
            btnPrevYear.ToolTip = "Previous Year"
            btnNextYear.ToolTip = "Next Year"
        End If
    End Sub

    Private Sub cboFirstDay_SelectedIndexChanged(sender As Object, e As EventArgs)
        If cboFirstDay.SelectedIndex >= 0 Then
            _firstDayOfWeek = dayOfWeekValues(cboFirstDay.SelectedIndex)
            GenerateCalendar()
        End If
    End Sub

    Private Sub cboLanguage_SelectedIndexChanged(sender As Object, e As EventArgs)
        If cboLanguage.Text = "Arabic" Then
            Threading.Thread.CurrentThread.CurrentUICulture = SettingsRuntimeApply.CreateArabicRegionalCultureGregorian()
        Else
            Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        End If
        UpdateTooltips()
        RefreshFirstDayItems()
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

        containerPanel2.Controls.Clear()
        headerLabels.Clear()
        dayControls.Clear()

        lblMonthTitle.Text = New DateTime(_year, _month, 1).ToString("MMMM yyyy", culture)

        Dim days = [Enum].GetValues(GetType(DayOfWeek)).Cast(Of DayOfWeek)().ToList()
        Dim orderedDays = days.Skip(_firstDayOfWeek).Concat(days.Take(_firstDayOfWeek)).ToList()

        For i As Integer = 0 To 6
            Dim columnIndex = If(RightToLeft = RightToLeft.Yes, 6 - i, i)
            Dim dayName = culture.DateTimeFormat.GetAbbreviatedDayName(orderedDays(i))
            Dim header As New LabelControl With {
                .Text = dayName,
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Size = New Size(labelWidth, 20),
                .Location = New Point(paddings + columnIndex * (labelWidth + paddings), paddings),
                .BackColor = If(orderedDays(i) = _firstDayOfWeek, Color.MediumSlateBlue, Color.LightGray),
                .ForeColor = If(orderedDays(i) = _firstDayOfWeek, Color.White, Color.Black)
            }
            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            containerPanel2.Controls.Add(header)
            headerLabels.Add(header)
        Next

        Dim firstDayOfMonth = New DateTime(_year, _month, 1)
        Dim startOffset = (7 + (firstDayOfMonth.DayOfWeek - _firstDayOfWeek)) Mod 7
        Dim daysInMonth = DateTime.DaysInMonth(_year, _month)

        For week As Integer = 0 To 5
            For dayIndex As Integer = 0 To 6
                Dim columnIndex = If(RightToLeft = RightToLeft.Yes, 6 - dayIndex, dayIndex)
                Dim dayNumber As Integer = week * 7 + dayIndex - startOffset + 1
                Dim currentDate As Date = Nothing

                Dim dayCtl As New DayControl With {
                    .Size = New Size(labelWidth, labelHeight),
                    .Location = New Point(paddings + columnIndex * (labelWidth + paddings), paddings * 2 + 20 + week * (labelHeight + paddings)),
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
                    dayCtl.SetCulture(Threading.Thread.CurrentThread.CurrentUICulture) ' <- localize day control
                Else
                    dayCtl.Enabled = False
                    dayCtl.Visible = False
                End If

                AddHandler dayCtl.AddVisitRequested, AddressOf HandleAddVisit
                AddHandler dayCtl.VisitLabelClicked, AddressOf HandleVisitLabelClick

                containerPanel2.Controls.Add(dayCtl)
                dayControls.Add(dayCtl)
            Next
        Next
    End Sub

    Private Sub HandleAddVisit(visDate As Date)
        MessageBox.Show($"[Add Visit] Clicked for: {visDate:yyyy-MM-dd}")
        Dim form As New FrmEditVisit(visDate)
        form.ShowDialog(Me)
        If dayControls.Count > 0 Then
            dayControls(0).RefreshVisits()
        End If
    End Sub

    Private Sub HandleVisitLabelClick(visit As DayControl.VisitSummary)
        ' Implement custom handling logic if needed
        ' Open the edit visit form
        Dim frm As New FrmEditVisit(visit)
        frm.ShowDialog(Me)
        If dayControls.Count > 0 Then
            dayControls(0).RefreshVisits()
        End If
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




'' VisitCalendarDayControl.vb
'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls
'Imports System.Globalization

'Public Class VisitCalendarDayControl

'    Private headerLabels As New List(Of LabelControl)()
'    Private dayControls As New List(Of DayControl)()
'    Private _month As Integer = DateTime.Now.Month
'    Private _year As Integer = DateTime.Now.Year

'    Private WithEvents btnPrevMonth As New SimpleButton With {.Text = "<<", .Width = 40, .ToolTip = "Previous Month"}
'    Private WithEvents btnNextMonth As New SimpleButton With {.Text = ">>", .Width = 40, .ToolTip = "Next Month"}
'    Private WithEvents btnPrevYear As New SimpleButton With {.Text = "⏮", .Width = 40, .ToolTip = "Previous Year"}
'    Private WithEvents btnNextYear As New SimpleButton With {.Text = "⏭", .Width = 40, .ToolTip = "Next Year"}

'    Private lblMonthTitle As New LabelControl With {.AutoSizeMode = LabelAutoSizeMode.None, .Height = 30, .Font = New Font("Tahoma", 10, FontStyle.Bold)}
'    Private cboFirstDay As New ComboBoxEdit With {.Width = 120, .Dock = DockStyle.Right, .Text = "Sunday"}
'    Private cboLanguage As New ComboBoxEdit With {.Width = 100, .Dock = DockStyle.Left, .Text = "English"}

'    Private _firstDayOfWeek As DayOfWeek = DayOfWeek.Sunday
'    Private dayOfWeekValues As List(Of DayOfWeek)

'    Public Property FilterPatient As String = ""
'    Public Property FilterVisitType As String = ""

'    Private Const labelWidth As Integer = 100
'    Private Const labelHeight As Integer = 100
'    Private Const paddings As Integer = 5

'    Public Sub New()
'        InitializeComponent()
'        SetupLayout()
'        SetupComboBoxes()
'        GenerateCalendar()
'    End Sub

'    Private Sub SetupLayout()
'        containerPanel2.AutoScroll = True
'        containerPanel2.BorderStyle = BorderStyles.Simple
'        containerPanel2.Appearance.BackColor = Color.WhiteSmoke

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
'        topPanel.Controls.Add(cboLanguage)
'    End Sub

'    Private Sub SetupComboBoxes()
'        dayOfWeekValues = [Enum].GetValues(GetType(DayOfWeek)).Cast(Of DayOfWeek)().ToList()
'        cboLanguage.Properties.Items.AddRange({"English", "Arabic"})
'        AddHandler cboLanguage.SelectedIndexChanged, AddressOf cboLanguage_SelectedIndexChanged
'        RefreshFirstDayItems()
'        AddHandler cboFirstDay.SelectedIndexChanged, AddressOf cboFirstDay_SelectedIndexChanged
'    End Sub


'    'Private Sub SetupComboBoxes()
'    '    cboFirstDay.Properties.Items.AddRange([Enum].GetNames(GetType(DayOfWeek)))
'    '    AddHandler cboFirstDay.SelectedIndexChanged, AddressOf cboFirstDay_SelectedIndexChanged

'    '    cboLanguage.Properties.Items.AddRange({"English", "Arabic"})
'    '    AddHandler cboLanguage.SelectedIndexChanged, AddressOf cboLanguage_SelectedIndexChanged
'    'End Sub

'    Private Sub RefreshFirstDayItems()
'        'RemoveHandler cboFirstDay.SelectedIndexChanged, AddressOf cboFirstDay_SelectedIndexChanged
'        Dim culture = Threading.Thread.CurrentThread.CurrentUICulture
'        cboFirstDay.Properties.Items.Clear()

'        For Each day In dayOfWeekValues
'            cboFirstDay.Properties.Items.Add(culture.DateTimeFormat.GetDayName(day))
'        Next

'        ' Re-select currently selected day based on enum index
'        cboFirstDay.SelectedIndex = dayOfWeekValues.IndexOf(_firstDayOfWeek)
'        'AddHandler cboFirstDay.SelectedIndexChanged, AddressOf cboFirstDay_SelectedIndexChanged
'    End Sub


'    Private Sub UpdateTooltips()
'        If cboLanguage.Text = "Arabic" Then
'            btnPrevMonth.ToolTip = "الشهر السابق"
'            btnNextMonth.ToolTip = "الشهر التالي"
'            btnPrevYear.ToolTip = "السنة السابقة"
'            btnNextYear.ToolTip = "السنة التالية"
'        Else
'            btnPrevMonth.ToolTip = "Previous Month"
'            btnNextMonth.ToolTip = "Next Month"
'            btnPrevYear.ToolTip = "Previous Year"
'            btnNextYear.ToolTip = "Next Year"
'        End If
'    End Sub

'    Private Sub cboFirstDay_SelectedIndexChanged(sender As Object, e As EventArgs)
'        If cboFirstDay.SelectedIndex >= 0 Then
'            _firstDayOfWeek = dayOfWeekValues(cboFirstDay.SelectedIndex)
'            GenerateCalendar()
'        End If
'        '_firstDayOfWeek = CType([Enum].Parse(GetType(DayOfWeek), cboFirstDay.Text), DayOfWeek)
'        'GenerateCalendar()
'    End Sub

'    Private Sub cboLanguage_SelectedIndexChanged(sender As Object, e As EventArgs)
'        If cboLanguage.Text = "Arabic" Then
'            Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("ar-EG")
'        Else
'            Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
'        End If

'        UpdateTooltips()
'        RefreshFirstDayItems()
'        GenerateCalendar()
'    End Sub



'    'Private Sub cboLanguage_SelectedIndexChanged(sender As Object, e As EventArgs)
'    '    If cboLanguage.Text = "Arabic" Then
'    '        Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("ar-EG")
'    '    Else
'    '        Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
'    '    End If

'    '    UpdateTooltips()

'    '    'Remove Handlers
'    '    RemoveHandler cboFirstDay.SelectedIndexChanged, AddressOf cboFirstDay_SelectedIndexChanged
'    '    ' Refresh first-day ComboBox with localized day names
'    '    Dim culture = Threading.Thread.CurrentThread.CurrentUICulture
'    '    Dim days = [Enum].GetValues(GetType(DayOfWeek)).Cast(Of DayOfWeek)().ToList()

'    '    cboFirstDay.Properties.Items.Clear()
'    '    For Each day In days
'    '        cboFirstDay.Properties.Items.Add(culture.DateTimeFormat.GetDayName(day))
'    '    Next
'    '    ' Re-select current first day by its index
'    '    cboFirstDay.SelectedIndex = CInt(_firstDayOfWeek)
'    '    AddHandler cboFirstDay.SelectedIndexChanged, AddressOf cboFirstDay_SelectedIndexChanged

'    '    GenerateCalendar()
'    'End Sub
'    '|||||||||||||||||||||||||||||||||||||||||||||||||||||||
'    'Private Sub cboLanguage_SelectedIndexChanged(sender As Object, e As EventArgs)
'    '    If cboLanguage.Text = "Arabic" Then
'    '        Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("ar-EG")
'    '    Else
'    '        Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
'    '    End If
'    '    UpdateTooltips()
'    '    GenerateCalendar()
'    'End Sub

'    Public Sub RefreshCalendar()
'        For Each dayCtl In dayControls
'            If dayCtl.Visible Then
'                dayCtl.FilterPatient = Me.FilterPatient
'                dayCtl.FilterVisitType = Me.FilterVisitType
'                dayCtl.DayVisits = dayCtl.DayVisits
'            End If
'        Next
'    End Sub

'    Private Sub GenerateCalendar()
'        Dim culture = Threading.Thread.CurrentThread.CurrentUICulture

'        If culture.TwoLetterISOLanguageName = "ar" Then
'            Me.RightToLeft = RightToLeft.Yes
'            'Me.RightToLeftLayout = True
'        Else
'            Me.RightToLeft = RightToLeft.No
'            'Me.RightToLeftLayout = False
'        End If

'        containerPanel2.Controls.Clear()
'        headerLabels.Clear()
'        dayControls.Clear()

'        lblMonthTitle.Text = New DateTime(_year, _month, 1).ToString("MMMM yyyy", culture)

'        Dim days = [Enum].GetValues(GetType(DayOfWeek)).Cast(Of DayOfWeek)().ToList()
'        Dim orderedDays = days.Skip(_firstDayOfWeek).Concat(days.Take(_firstDayOfWeek)).ToList()

'        For i As Integer = 0 To 6
'            Dim columnIndex = If(RightToLeft = RightToLeft.Yes, 6 - i, i)
'            Dim dayName = culture.DateTimeFormat.GetAbbreviatedDayName(orderedDays(i))
'            Dim header As New LabelControl With {
'                .Text = dayName,
'                .AutoSizeMode = LabelAutoSizeMode.None,
'                .Size = New Size(labelWidth, 20),
'                .Location = New Point(paddings + columnIndex * (labelWidth + paddings), paddings),
'                .BackColor = If(orderedDays(i) = _firstDayOfWeek, Color.MediumSlateBlue, Color.LightGray),
'                .ForeColor = If(orderedDays(i) = _firstDayOfWeek, Color.White, Color.Black)
'            }
'            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
'            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

'            containerPanel2.Controls.Add(header)
'            headerLabels.Add(header)
'        Next

'        Dim firstDayOfMonth = New DateTime(_year, _month, 1)
'        Dim startOffset = (7 + (firstDayOfMonth.DayOfWeek - _firstDayOfWeek)) Mod 7
'        Dim daysInMonth = DateTime.DaysInMonth(_year, _month)

'        For week As Integer = 0 To 5
'            For dayIndex As Integer = 0 To 6
'                Dim columnIndex = If(RightToLeft = RightToLeft.Yes, 6 - dayIndex, dayIndex)
'                Dim dayNumber As Integer = week * 7 + dayIndex - startOffset + 1
'                Dim currentDate As Date = Nothing

'                Dim dayCtl As New DayControl With {
'                    .Size = New Size(labelWidth, labelHeight),
'                    .Location = New Point(paddings + columnIndex * (labelWidth + paddings), paddings * 2 + 20 + week * (labelHeight + paddings)),
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

'                containerPanel2.Controls.Add(dayCtl)
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


'{{{{{{{{{{{{{{{{{}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}
'' VisitCalendarDayControl.vb
'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls
'Imports System.Globalization

'Public Class VisitCalendarDayControl

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
'    Private cboLanguage As New ComboBoxEdit With {.Width = 100, .Dock = DockStyle.Left, .Text = "English"}

'    Private _firstDayOfWeek As DayOfWeek = DayOfWeek.Sunday

'    Public Property FilterPatient As String = ""
'    Public Property FilterVisitType As String = ""

'    Private Const labelWidth As Integer = 100
'    Private Const labelHeight As Integer = 100
'    Private Const paddings As Integer = 5

'    Public Sub New()
'        InitializeComponent()
'        SetupLayout()
'        SetupComboBoxes()
'        GenerateCalendar()
'    End Sub

'    Private Sub SetupLayout()
'        containerPanel2.AutoScroll = True
'        containerPanel2.BorderStyle = BorderStyles.Simple
'        containerPanel2.Appearance.BackColor = Color.WhiteSmoke

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
'        topPanel.Controls.Add(cboLanguage)
'    End Sub

'    Private Sub SetupComboBoxes()
'        cboFirstDay.Properties.Items.AddRange([Enum].GetNames(GetType(DayOfWeek)))
'        AddHandler cboFirstDay.SelectedIndexChanged, AddressOf cboFirstDay_SelectedIndexChanged

'        cboLanguage.Properties.Items.AddRange({"English", "Arabic"})
'        AddHandler cboLanguage.SelectedIndexChanged, AddressOf cboLanguage_SelectedIndexChanged
'    End Sub

'    Private Sub cboFirstDay_SelectedIndexChanged(sender As Object, e As EventArgs)
'        _firstDayOfWeek = CType([Enum].Parse(GetType(DayOfWeek), cboFirstDay.Text), DayOfWeek)
'        GenerateCalendar()
'    End Sub

'    Private Sub cboLanguage_SelectedIndexChanged(sender As Object, e As EventArgs)
'        If cboLanguage.Text = "Arabic" Then
'            Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("ar-EG")
'        Else
'            Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
'        End If
'        GenerateCalendar()
'    End Sub

'    Public Sub RefreshCalendar()
'        For Each dayCtl In dayControls
'            If dayCtl.Visible Then
'                dayCtl.FilterPatient = Me.FilterPatient
'                dayCtl.FilterVisitType = Me.FilterVisitType
'                dayCtl.DayVisits = dayCtl.DayVisits
'            End If
'        Next
'    End Sub

'    Private Sub GenerateCalendar()
'        Dim culture = Threading.Thread.CurrentThread.CurrentUICulture

'        If culture.TwoLetterISOLanguageName = "ar" Then
'            Me.RightToLeft = RightToLeft.Yes
'            'Me.RightToLeftLayout = True
'        Else
'            Me.RightToLeft = RightToLeft.No
'            'Me.RightToLeftLayout = False
'        End If

'        containerPanel2.Controls.Clear()
'        headerLabels.Clear()
'        dayControls.Clear()

'        lblMonthTitle.Text = New DateTime(_year, _month, 1).ToString("MMMM yyyy", culture)

'        Dim days = [Enum].GetValues(GetType(DayOfWeek)).Cast(Of DayOfWeek)().ToList()
'        Dim orderedDays = days.Skip(_firstDayOfWeek).Concat(days.Take(_firstDayOfWeek)).ToList()

'        For i As Integer = 0 To 6
'            Dim dayName = culture.DateTimeFormat.GetAbbreviatedDayName(orderedDays(i))
'            Dim header As New LabelControl With {
'                .Text = dayName,
'                .AutoSizeMode = LabelAutoSizeMode.None,
'                .Size = New Size(labelWidth, 20),
'                .Location = New Point(paddings + i * (labelWidth + paddings), paddings),
'                .BackColor = If(orderedDays(i) = _firstDayOfWeek, Color.MediumSlateBlue, Color.LightGray),
'                .ForeColor = If(orderedDays(i) = _firstDayOfWeek, Color.White, Color.Black)
'            }
'            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
'            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

'            containerPanel2.Controls.Add(header)
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
'                    .Location = New Point(paddings + dayIndex * (labelWidth + paddings), paddings * 2 + 20 + week * (labelHeight + paddings)),
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

'                containerPanel2.Controls.Add(dayCtl)
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




'' VisitCalendarDayControl.vb
'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls

'Public Class VisitCalendarDayControl

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
'    Private Const paddings As Integer = 5

'    Public Sub New()
'        InitializeComponent()
'        SetupLayout()
'        SetupComboBox()
'        GenerateCalendar()
'    End Sub

'    Private Sub SetupLayout()
'        containerPanel2.AutoScroll = True
'        containerPanel2.BorderStyle = BorderStyles.Simple
'        containerPanel2.Appearance.BackColor = Color.WhiteSmoke

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
'        containerPanel2.Controls.Clear()
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
'                .Location = New Point(paddings + i * (labelWidth + paddings), paddings),
'                .BackColor = If(orderedDays(i) = _firstDayOfWeek, Color.MediumSlateBlue, Color.LightGray),
'                .ForeColor = If(orderedDays(i) = _firstDayOfWeek, Color.White, Color.Black)
'            }
'            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
'            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

'            containerPanel2.Controls.Add(header)
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
'                    .Location = New Point(paddings + dayIndex * (labelWidth + paddings), paddings * 2 + 20 + week * (labelHeight + paddings)),
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

'                containerPanel2.Controls.Add(dayCtl)
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

'' VisitCalendarDayControl.vb
'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls

'Public Class VisitCalendarDayControl

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
'    Private Const paddings As Integer = 5

'    Public Sub New()
'        InitializeComponent()
'        SetupLayout()
'        SetupComboBox()
'        GenerateCalendar()
'    End Sub

'    Private Sub SetupLayout()
'        containerPanel2.AutoScroll = True
'        containerPanel2.BorderStyle = BorderStyles.Simple
'        containerPanel2.Appearance.BackColor = Color.WhiteSmoke

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
'        containerPanel2.Controls.Clear()
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
'                .Location = New Point(paddings + i * (labelWidth + paddings), paddings),
'                .BackColor = If(orderedDays(i) = _firstDayOfWeek, Color.MediumSlateBlue, Color.LightGray),
'                .ForeColor = If(orderedDays(i) = _firstDayOfWeek, Color.White, Color.Black)
'            }
'            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
'            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

'            containerPanel2.Controls.Add(header)
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
'                    .Location = New Point(paddings + dayIndex * (labelWidth + paddings), paddings * 2 + 20 + week * (labelHeight + paddings)),
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

'                containerPanel2.Controls.Add(dayCtl)
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


'' VisitCalendarDayControl.vb
'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls

'Public Class VisitCalendarDayControl

'    'Private containerPanel2 As New PanelControl()
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
'    Private Const paddings As Integer = 5

'    Public Sub New()
'        InitializeComponent()
'        SetupLayout()
'        GenerateCalendar()
'    End Sub

'    Private Sub SetupLayout()
'        'Me.Controls.Clear()
'        'containerPanel2.Dock = DockStyle.Fill
'        containerPanel2.AutoScroll = True
'        containerPanel2.BorderStyle = BorderStyles.Simple
'        containerPanel2.Appearance.BackColor = Color.WhiteSmoke
'        'Me.Controls.Add(containerPanel2)
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
'        'containerPanel2.Controls.Add(topPanel)
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
'        containerPanel2.Controls.Clear()
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
'                .Location = New Point(paddings + i * (labelWidth + paddings), paddings)}
'            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
'            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

'            containerPanel2.Controls.Add(header)
'            headerLabels.Add(header)
'        Next

'        ' Day grid
'        For week As Integer = 0 To 5
'            For dayOfWeek As Integer = 0 To 6
'                Dim dayNumber As Integer = week * 7 + dayOfWeek - startDay + 1
'                Dim currentDate As Date = Nothing

'                Dim dayCtl As New DayControl With {
'                    .Size = New Size(labelWidth, labelHeight),
'                    .Location = New Point(paddings + dayOfWeek * (labelWidth + paddings),
'                                          paddings * 2 + 20 + week * (labelHeight + paddings)),
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

'                containerPanel2.Controls.Add(dayCtl)
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

'' VisitCalendarDayControl.vb
'Imports DevExpress.XtraEditors
'Imports DevExpress.XtraEditors.Controls

'Public Class VisitCalendarDayControl
'    'Inherits XtraUserControl

'    Private containerPanel2 As New PanelControl()
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
'    Private Const paddings As Integer = 5

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

'        containerPanel2.Dock = DockStyle.Fill
'        containerPanel2.AutoScroll = True
'        containerPanel2.BorderStyle = BorderStyles.Simple
'        containerPanel2.Appearance.BackColor = Color.Wheat
'        Me.Controls.Add(containerPanel2)
'    End Sub

'    Private Sub GenerateCalendar()
'        containerPanel2.Controls.Clear()
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
'                .Location = New Point(paddings + i * (labelWidth + paddings), paddings)
'            }
'            header.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
'            header.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

'            containerPanel2.Controls.Add(header)
'            headerLabels.Add(header)
'        Next

'        ' Day grid
'        For week As Integer = 0 To 5
'            For dayOfWeek As Integer = 0 To 6
'                Dim dayNumber As Integer = week * 7 + dayOfWeek - startDay + 1
'                Dim currentDate As Date = Nothing

'                Dim dayCtl As New DayControl With {
'                    .Size = New Size(labelWidth, labelHeight),
'                    .Location = New Point(paddings + dayOfWeek * (labelWidth + paddings),
'                                          paddings * 2 + 20 + week * (labelHeight + paddings)),
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

'                containerPanel2.Controls.Add(dayCtl)
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
