Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Linq
Imports Dapper

Public Class UserFormAccessDATA

    Private ReadOnly _cs As String = DentistXDATA.GetConnection.ConnectionString

    Public Function GetAllowedFormNamesForUser(usId As Integer) As List(Of String)
        Const sql = "
SELECT f.FormName
FROM UserFormAccess ufa
INNER JOIN Forms f ON f.FormID = ufa.FormID
WHERE ufa.UsID = @UsID AND ufa.IsAllowed = 1"
        Using conn As New SqlConnection(_cs)
            Return conn.Query(Of String)(sql, New With {.UsID = usId}).ToList()
        End Using
    End Function

    Public Function GetAllowedFormIdsForUser(usId As Integer) As HashSet(Of Integer)
        Const sql = "SELECT FormID FROM UserFormAccess WHERE UsID = @UsID AND IsAllowed = 1"
        Using conn As New SqlConnection(_cs)
            Return New HashSet(Of Integer)(conn.Query(Of Integer)(sql, New With {.UsID = usId}))
        End Using
    End Function

    Public Sub ReplaceAllForUser(usId As Integer, allowedFormIds As IEnumerable(Of Integer))
        Using conn As New SqlConnection(_cs)
            conn.Open()
            Using tran = conn.BeginTransaction()
                conn.Execute("DELETE FROM UserFormAccess WHERE UsID = @UsID", New With {.UsID = usId}, tran)
                For Each fid In allowedFormIds.Distinct()
                    conn.Execute(
                        "INSERT INTO UserFormAccess (UsID, FormID, IsAllowed) VALUES (@UsID, @FormID, 1)",
                        New With {.UsID = usId, .FormID = fid}, tran)
                Next
                tran.Commit()
            End Using
        End Using
    End Sub

End Class
