' DayControl.vb
Imports System.Globalization
Imports System.Windows.Forms

Public Class DayControlNew
    'Inherits UserControl

    Private dayLabel As New Label()
    Private visitsPanel As New FlowLayoutPanel()

    Public Property DayNumber As String
        Get
            Return dayLabel.Text
        End Get
        Set(value As String)
            dayLabel.Text = value
        End Set
    End Property

    Public Property DayVisits As Date
    Public Property FilterPatient As String
    Public Property FilterVisitType As String
    Public Property FirstDayOfWeek As DayOfWeek

    Public Event AddVisitRequested(dateValue As Date)
    Public Event VisitLabelClicked(visit As VisitSummary)

    Public Sub New()
        Me.Width = 100
        Me.Height = 100
        Me.BackColor = Color.White

        dayLabel.Dock = DockStyle.Top
        dayLabel.TextAlign = ContentAlignment.MiddleCenter
        dayLabel.Font = New Font("Tahoma", 9, FontStyle.Bold)
        dayLabel.Height = 20

        visitsPanel.Dock = DockStyle.Fill
        visitsPanel.FlowDirection = FlowDirection.TopDown
        visitsPanel.WrapContents = False

        Me.Controls.Add(visitsPanel)
        Me.Controls.Add(dayLabel)
    End Sub

    Public Sub SetCulture(culture As CultureInfo)
        ' Format DayLabel using localized culture if needed
        If DayVisits <> Nothing Then
            Dim format = culture.DateTimeFormat
            Dim localizedDay = format.GetDayName(DayVisits.DayOfWeek)
            Dim localizedDayNumber = DayVisits.Day

            dayLabel.Text = localizedDayNumber.ToString()
            dayLabel.RightToLeft = If(culture.TextInfo.IsRightToLeft, RightToLeft.Yes, RightToLeft.No)
            Me.RightToLeft = If(culture.TextInfo.IsRightToLeft, RightToLeft.Yes, RightToLeft.No)
            visitsPanel.RightToLeft = Me.RightToLeft
        End If
    End Sub

    Public Class VisitSummary
        Public Property VisitDate As Date
        Public Property VisitDetails As String
    End Class
End Class
