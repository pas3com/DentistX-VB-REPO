Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Public Class VisitCalendarDayControlOld

    Private containerPanel As New PanelControl()
    Private dayLabels As New List(Of DayControlOld)
    Private headerLabels As New List(Of LabelControl)

    Private _month As Integer = DateTime.Now.Month
    Private _year As Integer = DateTime.Now.Year

    ' Constants for layout
    Private Const labelWidth As Integer = 100
    Private Const labelHeight As Integer = 100
    Private Shadows Const padding As Integer = 5
    Public Sub New()
        InitializeComponent()
        SetupControl()
        GenerateCalendar()
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
            If _month = value Then Return ' ✅ Skip if value hasn't changed
            _month = value
            GenerateCalendar()
        End Set
    End Property
    Public Property InYear As Integer
        Get
            Return _year
        End Get
        Set(value As Integer)
            If _year = value Then Return ' ✅ Skip if value hasn't changed
            _year = value
            If _year < 2000 OrElse _year > 2050 Then Return
            GenerateCalendar()
        End Set
    End Property

    Private Sub GenerateCalendar()
        containerPanel.SuspendLayout()
        containerPanel.Controls.Clear()
        dayLabels.Clear()
        headerLabels.Clear()

        Dim daysInMonth = DateTime.DaysInMonth(_year, _month)
        Dim firstDay = New DateTime(_year, _month, 1)
        Dim startDay = CInt(firstDay.DayOfWeek)

        ' Weekday headers
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
            lblHeader.BorderStyle = BorderStyles.Office2003
            lblHeader.Appearance.BackColor = Color.AntiqueWhite
            lblHeader.BringToFront()
            lblHeader.Visible = True
            containerPanel.Controls.Add(lblHeader)
            headerLabels.Add(lblHeader)
        Next

        ' Day cells
        For week As Integer = 0 To 5
            For dayOfWeek As Integer = 0 To 6
                Dim dayNumber As Integer = week * 7 + dayOfWeek - startDay + 1
                Dim lbl As New DayControlOld With {
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Size = New Size(labelWidth, labelHeight),
                .Location = New Point(padding + dayOfWeek * (labelWidth + padding),
                                      padding * 2 + labelHeight + week * (labelHeight + padding)),
                .BorderStyle = BorderStyles.Simple
            }

                If dayNumber > 0 AndAlso dayNumber <= daysInMonth Then
                    Dim currentDate = New DateTime(_year, _month, dayNumber)
                    lbl.DayNumber = dayNumber.ToString()
                    lbl.DayVisits = currentDate ' Loads visits automatically
                Else
                    lbl.Text = ""
                    lbl.DayNumber = ""
                End If

                containerPanel.Controls.Add(lbl)
                dayLabels.Add(lbl)
            Next
        Next

        containerPanel.ResumeLayout()
    End Sub





End Class
