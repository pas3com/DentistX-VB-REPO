' DayControlGood.vb
Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

Public Class DayControlGood

#Region "Fields & Properties"
    Private _dayNumber As String
    Private _dayVisits As Date = Date.Today
    Private _visitList As List(Of VisitSummary)

    Public Event VisitLabelClicked(visit As VisitSummary)

    Public Event AddVisitRequested(visDate As Date)

    Public Property DayNumber As String
        Get
            Return _dayNumber
        End Get
        Set(value As String)
            _dayNumber = value
            DayNumLabel.Text = value
        End Set
    End Property

    Public Property DayVisits As Date
        Get
            Return _dayVisits
        End Get
        Set(value As Date)
            _dayVisits = value
            If value.Year < 2000 OrElse value.Year > 2050 Then Return
            UpdateHeaderStyle()
            _visitList = LoadVisitsByDate(value)
            RenderVisits()
        End Set
    End Property

    Public Property VisitList As List(Of VisitSummary)
        Get
            Return _visitList
        End Get
        Set(value As List(Of VisitSummary))
            _visitList = value
            RenderVisits()
        End Set
    End Property


    Private _filterPatient As String = ""
    Public Property FilterPatient As String
        Get
            Return _filterPatient
        End Get
        Set(value As String)
            If _filterPatient <> value Then
                _filterPatient = value
                'RefreshCalendar()
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
                'RefreshCalendar()
            End If
        End Set
    End Property




    Private WithEvents btnAddVisit As New SimpleButton With {.Text = "+", .Dock = DockStyle.Bottom, .Height = 22}
    Private DayNumLabel As New LabelControl With {.Dock = DockStyle.Top, .Height = 22}
    Private VisitPanel As New PanelControl With {.Dock = DockStyle.Fill, .BorderStyle = BorderStyles.NoBorder, .AutoScroll = True}
    Public ToolTipController1 As New DevExpress.Utils.ToolTipController()
#End Region

#Region "Constructor"
    Public Sub New()
        Me.BorderStyle = BorderStyles.Simple
        Me.Controls.Add(VisitPanel)
        DayNumLabel.Appearance.Font = New Font("Tahoma", 12, FontStyle.Bold)
        DayNumLabel.Appearance.ForeColor = Color.Blue
        Me.Controls.Add(DayNumLabel)
        Me.Controls.Add(btnAddVisit)
    End Sub
#End Region

#Region "Methods"
    Private Sub UpdateHeaderStyle()
        If _dayVisits.Date = Date.Today Then
            DayNumLabel.BackColor = Color.OliveDrab
            DayNumLabel.ForeColor = Color.White
            DayNumLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Else
            DayNumLabel.BackColor = Color.LightBlue
            DayNumLabel.ForeColor = Color.Black
            DayNumLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        End If
        Me.BackColor = If(_visitList?.Count >= 5, Color.LightCoral, Color.LightBlue)
    End Sub

    Private Sub btnAddVisit_Click(sender As Object, e As EventArgs) Handles btnAddVisit.Click
        RaiseEvent AddVisitRequested(_dayVisits)
    End Sub

    Private Sub RenderVisits()
        VisitPanel.Controls.Clear()
        If _visitList Is Nothing OrElse _visitList.Count = 0 Then Exit Sub

        For Each visit In _visitList.OrderBy(Function(v) v.VisTime)
            Dim lbl As New DevExpress.XtraEditors.LabelControl With {
            .Text = $"{visit.PatientName} ({visit.VisTime:hh\:mm})",
            .Dock = DockStyle.Top,
            .Height = 22,
            .Padding = New Padding(3),
            .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
            .Cursor = Cursors.Hand
        }

            ' Theme-based styling
            Dim bgColor As Color = If(visit.FlagColor <> Color.Empty, visit.FlagColor, Color.AliceBlue)
            lbl.Appearance.BackColor = bgColor
            lbl.Appearance.Options.UseBackColor = True
            lbl.Appearance.Font = New Font("Tahoma", 8.5, FontStyle.Regular)

            ' ToolTip
            ToolTipController1.SetToolTip(lbl, $"🧑 {visit.PatientName}" & vbCrLf &
                                          $"⏰ {visit.VisTime:hh\:mm} - {visit.VisTimeEnd:hh\:mm}" & vbCrLf &
                                          $"📋 {visit.VisDetail}" & vbCrLf &
                                          $"📝 {visit.VisNotes}")

            ' Click event for detail popup
            AddHandler lbl.Click, Sub(s, e)
                                      RaiseEvent VisitLabelClicked(visit)
                                  End Sub

            VisitPanel.Controls.Add(lbl)
            lbl.BringToFront()
        Next
    End Sub


    Private Function GetColorForVisitType(visDetail As String) As Color
        If visDetail.Contains("Surgery") Then
            Return Color.MistyRose
        ElseIf visDetail.Contains("Checkup") Then
            Return Color.LightGreen
        Else
            Return Color.AliceBlue
        End If
    End Function

    Public Function LoadVisitsByDate(visDate As Date) As List(Of VisitSummary)

        Dim sql As String = ""
        sql = "Select  dbo.Visits.VisitDetID, dbo.Visits.PatientID,dbo.Visits.VtID, dbo.VisitTypes.VisitTypeAr, dbo.Visits.VisitDay, dbo.Visits.VisTime,
                dbo.Visits.VisTimeEnd, dbo.Visits.PatientName, dbo.Visits.VisDetail, dbo.Visits.VisNotes, dbo.Visits.VisDateTime
                From dbo.VisitTypes INNER Join
                dbo.Visits ON dbo.VisitTypes.VtID = dbo.Visits.VtID
                WHERE CAST(VisDateTime AS DATE) = @VisitDate "

        If Not String.IsNullOrEmpty(FilterPatient) Then
            sql &= " And PatientName Like '%' + @PatientName + '%'"
        End If
        If Not String.IsNullOrEmpty(FilterVisitType) Then
            sql &= " AND dbo.VisitTypes.VisitTypeAr LIKE '%' + @VisType + '%'"
        End If

        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of VisitSummary)(sql, New With {.VisitDate = visDate.Date, .PatientName = FilterPatient, .VisType = FilterVisitType}).ToList()
        End Using
    End Function

    Public Shared Function GetColorForVisitType(vtID As Integer) As Color
        Select Case vtID
            Case 1 : Return Color.LightSalmon
            Case 2 : Return Color.LightGreen
            Case 3 : Return Color.LightSkyBlue
            Case 4 : Return Color.Khaki
            Case Else : Return Color.AliceBlue
        End Select
    End Function


#End Region

#Region "Data Class"
    Public Class VisitSummary
        Public Property VisitDetID As Integer
        Public Property PatientID As Integer
        Public Property PatientName As String
        Public Property VtID As Integer ' Visit type ID
        Public Property VisitTypeAr As String
        Public Property VisitDay As String
        Public Property VisTime As String
        Public Property VisTimeEnd As String
        Public Property VisDetail As String
        Public Property VisNotes As String
        Public Property VisDateTime As DateTime
        Public Property FlagColor As Color ' ← Color representing the theme (e.g., urgency/type)
    End Class
#End Region
End Class

'Private Sub RenderVisits1()
'    VisitPanel.Controls.Clear()
'    If _visitList Is Nothing OrElse _visitList.Count = 0 Then Return

'    Dim maxToShow As Integer = 3
'    Dim shown As Integer = 0

'    For Each visit In _visitList.OrderBy(Function(v) v.VisTime)
'        If shown >= maxToShow Then Exit For
'        shown += 1

'        Dim lbl As New LabelControl With {
'                .Text = $"{visit.PatientName} ({visit.VisTime:hh\:mm})",
'                .Dock = DockStyle.Top,
'                .Height = 22,
'                .Padding = New Padding(3),
'                .AutoSizeMode = LabelAutoSizeMode.None
'            }

'        ' Hover effect
'        AddHandler lbl.MouseEnter, Sub() lbl.BackColor = Color.LightYellow
'        AddHandler lbl.MouseLeave, Sub() lbl.BackColor = Color.AliceBlue

'        ' Click to show details
'        AddHandler lbl.Click, Sub()
'                                  MessageBox.Show($"Visit Details:{vbCrLf}Patient: {visit.PatientName}{vbCrLf}Time: {visit.VisTime:hh\:mm} - {visit.VisTimeEnd:hh\:mm}{vbCrLf}Detail: {visit.VisDetail}{vbCrLf}Notes: {visit.VisNotes}")
'                              End Sub

'        lbl.Appearance.BackColor = GetColorForVisitType(visit.VisitTypeAr)

'        ToolTipController1.SetToolTip(lbl, $"Patient: {visit.PatientName}{vbCrLf}Time: {visit.VisTime:hh\:mm} - {visit.VisTimeEnd:hh\:mm}{vbCrLf}Detail: {visit.VisDetail}{vbCrLf}Notes: {visit.VisNotes}")

'        VisitPanel.Controls.Add(lbl)
'        lbl.BringToFront()
'    Next

'    If _visitList.Count > maxToShow Then
'        Dim moreLbl As New LabelControl With {
'                .Text = $"More... ({_visitList.Count - maxToShow})",
'                .Dock = DockStyle.Top,
'                .Height = 20}
'        moreLbl.Appearance.ForeColor = Color.DarkBlue

'        AddHandler moreLbl.Click, Sub()
'                                      MessageBox.Show($"Visits for {_dayVisits:yyyy-MM-dd}:{vbCrLf}" &
'                                                          String.Join(vbCrLf, _visitList.Select(Function(v) $"{v.PatientName} ({v.VisTime:hh\:mm})")))
'                                  End Sub
'        VisitPanel.Controls.Add(moreLbl)
'        moreLbl.BringToFront()
'    End If
'End Sub
