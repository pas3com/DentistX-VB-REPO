Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports Dapper

Public Class FormAccessDATA

    Public Function SelectAll() As List(Of FormAccess)
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of FormAccess)("SELECT * FROM Forms").ToList()
        End Using
    End Function

    Public Function SelectRecord(FormID As Integer) As FormAccess
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.QueryFirstOrDefault(Of FormAccess)("SELECT * FROM Forms WHERE FormID = @FormID", New With {.FormID = FormID})
        End Using
    End Function

    Public Function Add(form As FormAccess) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql = "INSERT INTO Forms (FormName, Description, DisplayTitle, DisplayTitleAr) VALUES (@FormName, @Description, @DisplayTitle, @DisplayTitleAr)"
            Return conn.Execute(sql, form) > 0
        End Using
    End Function

    Public Function Update(form As FormAccess) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql = "UPDATE Forms SET FormName = @FormName, Description = @Description, DisplayTitle = @DisplayTitle, DisplayTitleAr = @DisplayTitleAr WHERE FormID = @FormID"
            Return conn.Execute(sql, form) > 0
        End Using
    End Function

    Public Sub UpdateDisplayTitles(rows As IEnumerable(Of UserFormAccessRowVm))
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Open()
            Using tran = conn.BeginTransaction()
                For Each r In rows
                    Dim t = If(r.Title, "").Trim()
                    Dim ta = If(r.TitleAr, "").Trim()
                    conn.Execute(
                        "UPDATE Forms SET DisplayTitle = @DisplayTitle, DisplayTitleAr = @DisplayTitleAr WHERE FormID = @FormID",
                        New With {
                            .FormID = r.FormID,
                            .DisplayTitle = If(t.Length = 0, CType(Nothing, String), t),
                            .DisplayTitleAr = If(ta.Length = 0, CType(Nothing, String), ta)},
                        tran)
                Next
                tran.Commit()
            End Using
        End Using
    End Sub

    Public Function Delete(FormID As Integer) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Execute("DELETE FROM Forms WHERE FormID = @FormID", New With {.FormID = FormID}) > 0
        End Using
    End Function

End Class
