Imports System.Data.SqlClient
Imports Dapper

Public Class GroupDATA

    Public Function SelectAll() As List(Of Group)
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of Group)("SELECT * FROM Groups").ToList()
        End Using
    End Function

    Public Function SelectRecord(GroupID As Integer) As Group
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.QueryFirstOrDefault(Of Group)("SELECT * FROM Groups WHERE GroupID = @GroupID", New With {.GroupID = GroupID})
        End Using
    End Function

    Public Function Add(grp As Group) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql = "IF NOT EXISTS (SELECT 1 FROM Groups WHERE GroupName = @GroupName)
                                        BEGIN
                                            INSERT INTO Groups (GroupName) VALUES (@GroupName)
                                        END"
            Return conn.Execute(sql, grp) > 0
        End Using
    End Function

    Public Function Update(grp As Group) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql = "UPDATE Groups SET GroupName = @GroupName WHERE GroupID = @GroupID"
            Return conn.Execute(sql, grp) > 0
        End Using
    End Function

    Public Function Delete(GroupID As Integer) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Execute("DELETE FROM Groups WHERE GroupID = @GroupID", New With {.GroupID = GroupID}) > 0
        End Using
    End Function

End Class
