Imports System.Data.SqlClient
Imports Dapper

Public Class FrmTodayVisits
    Private Sub FrmTodayVisits_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = AppIcon
        FillToday()
    End Sub

    Private Sub FillToday(Optional targetDate As Date = Nothing)
        Try
            If targetDate = Nothing Then targetDate = Date.Today

            Using conn As SqlConnection = DentistXDATA.GetConnection
                Dim sql As String = "
                SELECT 
                    v.VisitDetID,
                    v.PatientID,
                    vt.VisitType,
                    v.VisitDay,
                    v.VisTime,
                    v.VisTimeEnd,
                    v.PatientName,
                    v.VisDetail,
                    v.VisNotes,
                    v.VisDateTime
                FROM Visits v
                INNER JOIN VisitTypes vt ON v.VtID = vt.VtID
                WHERE CAST(v.VisDateTime AS DATE) = @Today
            "
                Dim visits = conn.Query(Of Visits)(sql, New With {.Today = targetDate.Date}).ToList()
                VisBS.DataSource = visits
            End Using

        Catch ex As Exception
            MsgBox("Error loading visits: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub FillVisitsBetweenDates(startDate As Date, endDate As Date)
        Try
            Using conn As SqlConnection = DentistXDATA.GetConnection
                Dim sql As String = "
                SELECT 
                    v.VisitDetID,
                    v.PatientID,
                    vt.VisitType,
                    v.VisitDay,
                    v.VisTime,
                    v.VisTimeEnd,
                    v.PatientName,
                    v.VisDetail,
                    v.VisNotes,
                    v.VisDateTime
                FROM Visits v
                INNER JOIN VisitTypes vt ON v.VtID = vt.VtID
                WHERE CAST(v.VisDateTime AS DATE) BETWEEN @StartDate AND @EndDate
            "
                Dim visits = conn.Query(Of Visits)(sql, New With {
                .StartDate = startDate.Date,
                .EndDate = endDate.Date
            }).ToList()

                VisBS.DataSource = visits
            End Using

        Catch ex As Exception
            MsgBox("Error loading visits between dates: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnOnDate_Click(sender As Object, e As EventArgs) Handles btnOnDate.Click
        If dtOnDate.Text.Length < 10 Then
            MsgBox("Select A Date")
            Exit Sub
        End If
        FillToday(CDate(dtOnDate.EditValue))

    End Sub

    Private Sub btn2Dates_Click(sender As Object, e As EventArgs) Handles btn2Dates.Click
        If dtStartDate.Text.Length < 10 Then
            MsgBox("Select A Start Date")
            Exit Sub
        End If
        If dtEndDate.Text.Length < 10 Then
            MsgBox("Select End Date")
            Exit Sub
        End If
        FillVisitsBetweenDates(CDate(dtStartDate.EditValue), CDate(dtEndDate.EditValue))

    End Sub
End Class