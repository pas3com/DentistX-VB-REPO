' DayControl.vb
Imports Dapper
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils
Imports System.Globalization
Imports System.Text

Public Class DayControl

    Public Event AddVisitRequested(visDate As Date)
    Public Event VisitLabelClicked(visit As VisitSummary)

    Private _dayNumber As String
    Private _dayVisits As Date = Now
    Private _visitList As List(Of VisitSummary)
    Private currentCulture As CultureInfo = Threading.Thread.CurrentThread.CurrentUICulture
    Public Property FilterPatient As String = ""
    Public Property FilterVisitType As String = ""
    Public Property UseVisualThemes As Boolean = False
    Public Property EnableIcons As Boolean = False
    Public Property FirstDayOfWeek As DayOfWeek = DayOfWeek.Sunday

    Private WithEvents DayNumLabel As New LabelControl With {.Dock = DockStyle.Top, .Height = 20, .AutoSizeMode = LabelAutoSizeMode.None}
    Private WithEvents AddButton As New SimpleButton With {.Text = "+", .Dock = DockStyle.Bottom, .Height = 24, .Visible = True}
    Private VisitPanel As New PanelControl With {.Dock = DockStyle.Fill, .BorderStyle = BorderStyles.NoBorder}
    Private ToolTipController1 As New ToolTipController()

    Public Sub New()
        InitializeComponent()
        Me.Controls.Add(VisitPanel)
        Me.Controls.Add(AddButton)
        Me.Controls.Add(DayNumLabel)
        Me.BorderStyle = BorderStyles.Simple
    End Sub

    Public Property DayNumber As String
        Get
            Return _dayNumber
        End Get
        Set(value As String)
            _dayNumber = value
            DayNumLabel.Appearance.Font = New Font("Tahoma", 12, FontStyle.Bold)
            DayNumLabel.Text = value.ToString()
        End Set
    End Property

    Public Property DayVisits As Date
        Get
            Return _dayVisits
        End Get
        Set(value As Date)
            _dayVisits = value

            If value.Date = Date.Today Then
                DayNumLabel.BackColor = Color.Orange
                DayNumLabel.ForeColor = Color.White
            ElseIf value.DayOfWeek = FirstDayOfWeek Then
                DayNumLabel.BackColor = Color.MediumSlateBlue
                DayNumLabel.ForeColor = Color.White
            Else
                DayNumLabel.BackColor = Color.LightBlue
                DayNumLabel.ForeColor = Color.Black
            End If

            If value.Year < 2000 OrElse value.Year > 2050 Then Return
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

    Public Sub RefreshVisits()
        If _dayVisits.Year >= 2000 AndAlso _dayVisits.Year <= 2050 Then
            _visitList = LoadVisitsByDate(_dayVisits)
            RenderVisits()
        End If
    End Sub


    Private Sub HandleAddVisit(dateValue As Date)
        'Dim form As New FrmEditVisit(dateValue)
        'form.ShowDialog(Me)
        'RefreshVisits()
    End Sub

    Private Sub HandleEditVisit(visit As DayControl.VisitSummary)
        'Dim form As New FrmEditVisit(visit)
        'form.ShowDialog(Me)
        'RefreshVisits()
    End Sub

    Public Sub RenderVisits2()
        '(visitList As List(Of VisitSummary), toolTip As ToolTip, culture As CultureInfo, enableIcons As Boolean, useThemes As Boolean)
        VisitPanel.Controls.Clear()

        If VisitList Is Nothing OrElse VisitList.Count = 0 Then Exit Sub

        Dim filtered = VisitList.AsEnumerable()

        If Not String.IsNullOrWhiteSpace(FilterPatient) Then
            filtered = filtered.Where(Function(v) v.VisDetail.ToLower().Contains(FilterPatient.ToLower()))
        End If

        If Not String.IsNullOrWhiteSpace(FilterVisitType) Then
            filtered = filtered.Where(Function(v) v.VisDetail.ToLower().Contains(FilterVisitType.ToLower()))
        End If

        Dim visitCount As Integer = 0
        Dim maxVisits As Integer = 1

        For Each visit In filtered.OrderBy(Function(v) v.VisDateTime.TimeOfDay)
            If visitCount >= maxVisits Then
                Dim moreLabel As New Label() With {
                    .Dock = DockStyle.Top,
                    .Height = 22,
                    .Cursor = Cursors.Hand,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Text = If(currentCulture.TextInfo.IsRightToLeft, "المزيد ....", "More...")
                }
                AddHandler moreLabel.Click, Sub(sender, e)
                                                MessageBox.Show("Show all visits popup")
                                            End Sub
                VisitPanel.Controls.Add(moreLabel)
                moreLabel.BringToFront()
                Exit For
            End If

            Dim labelText As String = $"{visit.VisDetail} ({visit.VisDateTime:hh\:mm})"
            If EnableIcons Then
                labelText = "🦷 " & labelText
            End If
            Dim lbl As New Label() With {
                .Text = labelText,
                .Dock = DockStyle.Top,
                .Height = 22,
                .Padding = New Padding(3),
                .Tag = visit,
                .Cursor = Cursors.Hand,
                .BackColor = If(UseVisualThemes, Color.LightCyan, Color.AliceBlue),
                .TextAlign = ContentAlignment.MiddleLeft
            }
            ToolTipController1.SetToolTip(lbl, $"📝 {visit.VisDetail}\n{visit.VisDateTime:hh\:mm}")

            AddHandler lbl.Click, Sub(sender, e)
                                      RaiseEvent VisitLabelClicked(visit)
                                  End Sub
            AddHandler lbl.DoubleClick, Sub(sender, e)
                                            HandleEditVisit(visit)
                                        End Sub

            VisitPanel.Controls.Add(lbl)
            lbl.BringToFront()
            visitCount += 1
        Next
    End Sub


    Private Sub RenderVisits()
        VisitPanel.Controls.Clear()
        If _visitList Is Nothing OrElse _visitList.Count = 0 Then Exit Sub

        Dim filtered = _visitList.AsEnumerable()

        If Not String.IsNullOrWhiteSpace(FilterPatient) Then
            filtered = filtered.Where(Function(v) v.PatientName.ToLower().Contains(FilterPatient.ToLower()))
        End If

        If Not String.IsNullOrWhiteSpace(FilterVisitType) Then
            filtered = filtered.Where(Function(v) v.VisDetail.ToLower().Contains(FilterVisitType.ToLower()))
        End If

        Dim visitCount As Integer = 0
        Dim maxVisits As Integer = 1

        For Each visit In filtered.OrderBy(Function(v) v.VisTime)
            If visitCount >= maxVisits Then
                Dim moreLabel As New LabelControl With {.Dock = DockStyle.Top,
                                      .Height = 22,
                    .AutoSizeMode = LabelAutoSizeMode.None,
                    .Cursor = Cursors.Hand}
                If currentCulture.TextInfo.IsRightToLeft Then
                    moreLabel.Text = "المزيد ...."
                Else
                    moreLabel.Text = "More..."
                End If
                'End If
                moreLabel.Appearance.ForeColor = Color.DarkSlateGray
                moreLabel.Font = New Font("Tahoma", 8.25, FontStyle.Italic)

                AddHandler moreLabel.Click, Sub(sender, e)
                                                ShowAllVisitsPopup(filtered.ToList())
                                            End Sub

                VisitPanel.Controls.Add(moreLabel)
                moreLabel.BringToFront()
                Exit For
            End If

            Dim labelText As String = $"{visit.PatientName} ({visit.VisTime:hh\:mm})"
            If EnableIcons Then
                labelText = "🦷 " & labelText
            End If
            Dim lbl As New LabelControl With {
                .Text = labelText,
                .Dock = DockStyle.Top,
                .Height = 22,
                .Padding = New Padding(3),
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Tag = visit,
                .Cursor = Cursors.Hand}
            If UseVisualThemes Then
                lbl.Appearance.BackColor = GetColorForVisitType(visit.VtID)
            Else
                lbl.Appearance.BackColor = Color.AliceBlue
            End If

            ToolTipController1.SetToolTip(lbl, $"🧑 {visit.PatientName}" & vbCrLf &
                                          $"⏰ {visit.VisTime:hh\:mm} - {visit.VisTimeEnd:hh\:mm}" & vbCrLf &
                                          $"📋 {visit.VisDetail}" & vbCrLf &
                                          $"📝 {visit.VisNotes}")
            AddHandler lbl.Click, Sub(sender, e)
                                      ShowAllVisitsPopup(filtered.ToList())
                                      'RaiseEvent VisitLabelClicked(visit)
                                  End Sub

            AddHandler lbl.DoubleClick, Sub(sender, e)
                                            HandleEditVisit(visit)
                                        End Sub
            VisitPanel.Controls.Add(lbl)
            lbl.BringToFront()
            visitCount += 1
        Next
    End Sub

    Private Sub OnVisitLabelClicked(visit As DayControl.VisitSummary)
        '' Open the edit visit form
        'Dim frm As New FrmEditVisit(visit)
        'frm.ShowDialog(Me)
    End Sub

    Private Sub ShowAllVisitsPopup(allVisits As List(Of VisitSummary))
        Dim frm As New XtraForm With {
            .Text = $"Visits on {_dayVisits:yyyy-MM-dd}",
            .FormBorderStyle = FormBorderStyle.FixedToolWindow,
            .StartPosition = FormStartPosition.CenterParent,
            .Size = New Size(400, 300)
        }
        If currentCulture.TextInfo.IsRightToLeft Then
            frm.Text = $"زيارات هذا اليوم  {_dayVisits:yyyy-MM-dd}"
            frm.RightToLeftLayout = True
            frm.RightToLeft = RightToLeft.Yes
        Else
            frm.Text = $"Visits on {_dayVisits:yyyy-MM-dd}"
            frm.RightToLeftLayout = False
            frm.RightToLeft = RightToLeft.No
        End If
        Dim listPanel As New PanelControl With {.Dock = DockStyle.Fill, .AutoScroll = True, .BorderStyle = BorderStyles.NoBorder}

        For Each visit In allVisits.OrderByDescending(Function(v) v.VisTime)
            Dim lbl As New LabelControl With {
                .Text = $"{visit.PatientName} ({visit.VisTime:hh\:mm})",
                .Dock = DockStyle.Top,
                .Height = 24,
                 .Font = New Font("tahoma", 12, FontStyle.Bold),
                .Padding = New Padding(3),
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Cursor = Cursors.Hand,
                .Tag = visit
            }

            If UseVisualThemes Then
                lbl.Appearance.BackColor = GetColorForVisitType(visit.VtID)
            Else
                lbl.Appearance.BackColor = Color.AliceBlue
            End If

            AddHandler lbl.DoubleClick, Sub(sender, e)
                                            frm.Close()
                                            RaiseEvent VisitLabelClicked(visit)
                                        End Sub
            AddHandler lbl.Click, Sub(s, e)
                                      frm.Close()
                                      Edit(visit)
                                  End Sub
            '==================

            Dim lblVisTime As New LabelControl With {
                .Text = $"⏰ {visit.VisTime:hh\:mm} - {visit.VisTimeEnd:hh\:mm}",
                .Dock = DockStyle.Top,
                .Height = 24,
                 .Font = New Font("tahoma", 10, FontStyle.Bold),
                .Padding = New Padding(20),
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Cursor = Cursors.Hand,
                .BackColor = Color.FromArgb(254, 215, 205),
                .Tag = visit
            }
            Dim lblVisDetail As New LabelControl With {
                .Text = $"📋 {visit.VisDetail}",
                .Dock = DockStyle.Top,
                .Height = 24,
                 .Font = New Font("tahoma", 10, FontStyle.Bold),
                .Padding = New Padding(20),
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Cursor = Cursors.Hand,
                .BackColor = Color.FromArgb(254, 235, 205),
                .Tag = visit
            }
            Dim lblVisNotes As New LabelControl With {
                .Text = $"📝 {visit.VisNotes}",
                .Dock = DockStyle.Top,
                .Height = 24,
                .Font = New Font("tahoma", 10, FontStyle.Bold),
                .Padding = New Padding(20),
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Cursor = Cursors.Hand,
                .BackColor = Color.FromArgb(254, 245, 205),
                .Tag = visit
            }
            listPanel.Controls.AddRange({lblVisNotes, lblVisDetail, lblVisTime, lbl})
            'lbl.BringToFront()
        Next

        frm.Controls.Add(listPanel)
        frm.ShowDialog()
    End Sub

    Private Sub Edit(ByVal vists As VisitSummary)
        ' Open the edit visit form
        Dim frm As New FrmEditVisit(vists)
        frm.ShowDialog(Me)
    End Sub
    Private Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click
        RaiseEvent AddVisitRequested(_dayVisits)
        HandleAddVisit(_dayVisits)
    End Sub

    Public Function LoadVisitsByDate(visDate As Date) As List(Of VisitSummary)
        Dim sql As New StringBuilder()
        sql.AppendLine("SELECT dbo.Visits.VisitDetID, dbo.Visits.PatientID, dbo.Visits.VtID,")
        sql.AppendLine("       dbo.VisitTypes.VisitType, dbo.VisitTypes.VisitTypeAr,")
        sql.AppendLine("       dbo.Visits.VisitDay, dbo.Visits.VisTime, dbo.Visits.VisTimeEnd,")
        sql.AppendLine("       dbo.Visits.PatientName, dbo.Visits.VisDetail,")
        sql.AppendLine("       dbo.Visits.VisNotes, dbo.Visits.VisDateTime")
        sql.AppendLine("FROM   dbo.VisitTypes")
        sql.AppendLine("INNER JOIN dbo.Visits ON dbo.VisitTypes.VtID = dbo.Visits.VtID")
        sql.AppendLine("WHERE CAST([VisDateTime] AS DATE) = @VisitDate")

        ' Prepare parameter object dynamically
        Dim parameters = New DynamicParameters()
        parameters.Add("@VisitDate", visDate.Date)

        If Not String.IsNullOrEmpty(FilterPatient) Then
            sql.AppendLine("AND PatientName LIKE '%' + @PatientName + '%'")
            parameters.Add("@PatientName", FilterPatient)
        End If

        If Not String.IsNullOrEmpty(FilterVisitType) Then
            sql.AppendLine("AND (VisitType LIKE '%' + @VisType + '%' OR VisitTypeAr LIKE '%' + @VisType + '%')")
            parameters.Add("@VisType", FilterVisitType)
        End If

        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of VisitSummary)(sql.ToString(), parameters).ToList()
        End Using
    End Function




    Public Function LoadVisitsByDate1(visDate As Date) As List(Of VisitSummary)
        Dim sql As String = ""
        sql = sql & "SELECT dbo.Visits.VisitDetID, dbo.Visits.PatientID, dbo.Visits.VtID, dbo.VisitTypes.VisitType, dbo.VisitTypes.VisitTypeAr,"
        sql = sql & "       dbo.Visits.VisitDay, dbo.Visits.VisTime, dbo.Visits.VisTimeEnd, dbo.Visits.PatientName, dbo.Visits.VisDetail, "
        sql = sql & "       dbo.Visits.VisNotes, dbo.Visits.VisDateTime"
        sql = sql & "FROM   dbo.VisitTypes INNER JOIN"
        sql = sql & "       dbo.Visits ON dbo.VisitTypes.VtID = dbo.Visits.VtID "
        sql = sql & " WHERE CAST([VisDateTime] AS DATE) = @VisitDate "
        '==================================
        'sql = sql & "SELECT [VisitDetID], [PatientID], [VtID], [VisitDay], [VisTime], [VisTimeEnd],"
        'sql = sql & " [PatientName], [VisDetail], [VisNotes], [VisDateTime] "
        'sql = sql & " FROM [Visits] "
        'sql = sql & " WHERE CAST([VisDateTime] AS DATE) = @VisitDate "
        '==================================
        If Not String.IsNullOrEmpty(FilterPatient) Then
            sql &= " AND PatientName LIKE '%' + @PatientName + '%'"
        End If
        If Not String.IsNullOrEmpty(FilterVisitType) Then
            sql &= " AND (VisitType LIKE '%' + @VisType + '%' OR  VisitTypeAr LIKE '%' + @VisType + '%')"
        End If

        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of VisitSummary)(sql, New With {.VisitDate = visDate.Date, .PatientName = FilterPatient, .VisType = FilterVisitType}).ToList()
        End Using
    End Function

    Public Function LoadVisitsByDateAndPatient(visDate As Date, patientName As String) As List(Of VisitSummary)
        Dim sql As String = ""
        sql = sql & "SELECT dbo.Visits.VisitDetID, dbo.Visits.PatientID, dbo.Visits.VtID, dbo.VisitTypes.VisitType, dbo.VisitTypes.VisitTypeAr,"
        sql = sql & "       dbo.Visits.VisitDay, dbo.Visits.VisTime, dbo.Visits.VisTimeEnd, dbo.Visits.PatientName, dbo.Visits.VisDetail, "
        sql = sql & "       dbo.Visits.VisNotes, dbo.Visits.VisDateTime"
        sql = sql & "FROM   dbo.VisitTypes INNER JOIN"
        sql = sql & "       dbo.Visits ON dbo.VisitTypes.VtID = dbo.Visits.VtID "
        sql = sql & " WHERE CAST([VisDateTime] As Date) = @VisitDate AND [PatientName] LIKE '%' + @PatientName + '%'"

        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of VisitSummary)(sql, New With {.VisitDate = visDate.Date, .PatientName = patientName}).ToList()
        End Using
    End Function

    Public Function LoadVisitsByPatient(patientName As String) As List(Of VisitSummary)
        Dim sql As String = ""
        sql = sql & "SELECT dbo.Visits.VisitDetID, dbo.Visits.PatientID, dbo.Visits.VtID, dbo.VisitTypes.VisitType, dbo.VisitTypes.VisitTypeAr,"
        sql = sql & "       dbo.Visits.VisitDay, dbo.Visits.VisTime, dbo.Visits.VisTimeEnd, dbo.Visits.PatientName, dbo.Visits.VisDetail, "
        sql = sql & "       dbo.Visits.VisNotes, dbo.Visits.VisDateTime"
        sql = sql & "FROM   dbo.VisitTypes INNER JOIN"
        sql = sql & "       dbo.Visits ON dbo.VisitTypes.VtID = dbo.Visits.VtID "
        sql = sql & " WHERE [PatientName] LIKE '%' + @PatientName + '%'"

        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of VisitSummary)(sql, New With {.PatientName = patientName}).ToList()
        End Using
    End Function

    Public Function LoadVisitsByPatientID(patientID As Integer) As List(Of VisitSummary)
        Dim sql As String = ""
        sql = sql & "SELECT dbo.Visits.VisitDetID, dbo.Visits.PatientID, dbo.Visits.VtID, dbo.VisitTypes.VisitType, dbo.VisitTypes.VisitTypeAr,"
        sql = sql & "       dbo.Visits.VisitDay, dbo.Visits.VisTime, dbo.Visits.VisTimeEnd, dbo.Visits.PatientName, dbo.Visits.VisDetail, "
        sql = sql & "       dbo.Visits.VisNotes, dbo.Visits.VisDateTime"
        sql = sql & "FROM   dbo.VisitTypes INNER JOIN"
        sql = sql & "       dbo.Visits ON dbo.VisitTypes.VtID = dbo.Visits.VtID "
        sql = sql & " WHERE [PatientID] = @PatientID "

        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of VisitSummary)(sql, New With {.PatientID = patientID}).ToList()
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

    Public Sub SetCulture(culture As CultureInfo)
        currentCulture = culture
        ' Format DayLabel using localized culture if needed
        If DayVisits <> Nothing Then
            Dim format = culture.DateTimeFormat
            Dim localizedDay = format.GetDayName(DayVisits.DayOfWeek)
            Dim localizedDayNumber = DayVisits.Day

            DayNumLabel.Text = localizedDayNumber.ToString()
            DayNumLabel.RightToLeft = If(culture.TextInfo.IsRightToLeft, RightToLeft.Yes, RightToLeft.No)
            Me.RightToLeft = If(culture.TextInfo.IsRightToLeft, RightToLeft.Yes, RightToLeft.No)
            VisitPanel.RightToLeft = Me.RightToLeft
        End If
    End Sub

    Public Class VisitSummary
        Public Property VisitDetID As Integer
        Public Property PatientID As Integer
        Public Property PatientName As String
        Public Property VtID As Integer 'VisitType
        Public Property VisitType As String
        Public Property VisitTypeAr As String
        Public Property VisitDay As String
        Public Property VisTime As String
        Public Property VisTimeEnd As String
        Public Property VisDetail As String
        Public Property VisNotes As String
        Public Property VisDateTime As DateTime
    End Class
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
