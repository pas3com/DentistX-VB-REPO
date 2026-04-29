Imports Dapper
Imports System.Data.SqlClient
Public Class DayControlOld
#Region "New Code"
    Private _dayNumber As String
    Private _dayVisits As Date = Now
    Private _visitList As List(Of VisitSummary)
    Public Property DayNumber As String
        Get
            Return _dayNumber
        End Get
        Set(value As String)
            _dayNumber = value
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
            Else
                DayNumLabel.BackColor = Color.LightBlue
                DayNumLabel.ForeColor = Color.Black
            End If
            If value.Year < 2000 OrElse value.Year > 2050 Then Return
            _visitList = LoadVisitsByDate(value)
            If _visitList.Count >= 5 Then ' Threshold for "busy"
                Me.BackColor = Color.LightCoral
            Else
                Me.BackColor = Color.White
            End If
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


    Private Sub RenderVisits()
        VisitPanel.SuspendLayout()
        VisitPanel.Controls.Clear()

        If _visitList Is Nothing OrElse _visitList.Count = 0 Then
            VisitPanel.ResumeLayout()
            Exit Sub
        End If

        Dim tooltip As DevExpress.Utils.ToolTipController = ToolTipController1

        For Each visit In _visitList.OrderBy(Function(v) v.VisTime)
            ' Skip if the visit isn't in the same month as the control's DayVisits
            If visit.VisDateTime.Month <> _dayVisits.Month OrElse visit.VisDateTime.Year <> _dayVisits.Year Then
                Continue For
            End If

            Dim lbl As New DevExpress.XtraEditors.LabelControl With {
            .Text = $"{visit.PatientName} ({visit.VisTime:hh\:mm})",
            .Dock = DockStyle.Top,
            .Height = 24,
            .Padding = New Padding(3),
            .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None}
            lbl.Appearance.BackColor = Color.AliceBlue

            lbl.Cursor = Cursors.Hand
            lbl.Tag = visit ' Optional: attach visit info for later use

            ' Tooltip text
            Dim tooltipText As String = $"🧑 Patient: {visit.PatientName}" & vbCrLf &
                                    $"⏰ Time: {visit.VisTime:hh\:mm} - {visit.VisTimeEnd:hh\:mm}" & vbCrLf &
                                    $"📋 Detail: {visit.VisDetail}" & vbCrLf &
                                    $"📝 Notes: {visit.VisNotes}"

            ' Attach tooltip
            tooltip.SetToolTip(lbl, tooltipText)

            ' Add label to panel
            VisitPanel.Controls.Add(lbl)
            lbl.BringToFront()
        Next

        VisitPanel.ResumeLayout()
        VisitPanel.BringToFront()

    End Sub



    Public Function LoadVisitsByDate(visDate As Date) As List(Of VisitSummary)
        Dim sql As String = "
        SELECT [VisitDetID], [PatientID], [VtID], [VisitDay], [VisTime], [VisTimeEnd],
               [PatientName], [VisDetail], [VisNotes], [VisDateTime]
        FROM [Visits]
        WHERE  CAST([VisDateTime] As Date)  = @VisitDate"
        'WHERE CAST(VisitDay As Date) = @VisitDate"
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of VisitSummary)(sql, New With {.VisitDate = visDate.Date}).ToList()
        End Using
    End Function

    Public Function LoadVisitsByDateAndPatient(visDate As Date, patientName As String) As List(Of VisitSummary)
        Dim sql As String = "
        SELECT [VisitDetID], [PatientID], [VtID], [VisitDay], [VisTime], [VisTimeEnd],
               [PatientName], [VisDetail], [VisNotes], [VisDateTime]
        FROM [Visits]
        WHERE [VisDateTime] = @VisitDate AND [PatientName] LIKE '%' + @PatientName + '%'"
        'WHERE CAST(VisitDay As Date) = @VisitDate And [PatientName] Like '%' + @PatientName + '%'"
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of VisitSummary)(sql, New With {.VisitDate = visDate.Date, .PatientName = patientName}).ToList()
        End Using
    End Function
#End Region
#Region "Class"

    Public Class VisitSummary
        Public Property VisitDetID As Integer
        Public Property PatientID As Integer
        Public Property PatientName As String
        Public Property VtID As Integer
        Public Property VisitDay As String
        Public Property VisTime As String
        Public Property VisTimeEnd As String
        Public Property VisDetail As String
        Public Property VisNotes As String
        Public Property VisDateTime As DateTime
    End Class


#End Region
End Class

'Public Function ParseDateString(dateStr As String) As Date
'    If String.IsNullOrWhiteSpace(dateStr) Then
'        Throw New ArgumentException("Date string is empty or null.")
'    End If

'    ' Normalize separators to slash
'    Dim cleanDateStr = dateStr.Replace("-", "/").Trim()

'    Dim parts = cleanDateStr.Split("/"c)

'    If parts.Length <> 3 Then
'        Throw New FormatException("Date format is invalid. Expected format: dd/MM/yyyy or MM/dd/yyyy")
'    End If

'    Dim day, month, year As Integer

'    ' Try parsing parts
'    If Not Integer.TryParse(parts(0), day) OrElse
'       Not Integer.TryParse(parts(1), month) OrElse
'       Not Integer.TryParse(parts(2), year) Then
'        Throw New FormatException("Date parts are not numeric.")
'    End If

'    ' Adjust if needed (you can flip day/month here if format is ambiguous in your region)
'    Try
'        Return New Date(year, month, day)
'    Catch ex As Exception
'        Throw New ArgumentOutOfRangeException("Invalid date values.", ex)
'    End Try
'End Function
'Public WriteOnly Property DayNameText As String
'    Set(ByVal value As String)
'        DayNameLabel.Text = value
'    End Set
'End Property

'Public WriteOnly Property DetailText As String
'    Set(ByVal value As String)
'        DetLabel.Text = value
'    End Set
'End Property

'Public WriteOnly Property RedNote As Boolean
'    Set(ByVal value As Boolean)
'        RedLabel.Visible = value
'    End Set
'End Property

'Public WriteOnly Property YellowNote As Boolean
'    Set(ByVal value As Boolean)
'        YelloLabel.Visible = value
'    End Set
'End Property

'Public WriteOnly Property GreenNote As Boolean
'    Set(ByVal value As Boolean)
'        GreenLabel.Visible = value
'    End Set
'End Property
