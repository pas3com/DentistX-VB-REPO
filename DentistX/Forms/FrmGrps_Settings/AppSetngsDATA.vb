Imports System.Data.SqlClient
Imports Dapper

Public Class AppSetngsDATA

    Public Function SelectAll() As List(Of AppSetngs)
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of AppSetngs)("SELECT * FROM AppSetngs").ToList()
        End Using
    End Function

    Public Function SelectRecord(SettingKey As String) As AppSetngs
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.QueryFirstOrDefault(Of AppSetngs)("SELECT * FROM AppSetngs WHERE SettingKey = @SettingKey", New With {.SettingKey = SettingKey})
        End Using
    End Function

    Public Function Add(s As AppSetngs) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql = "INSERT INTO AppSetngs (SettingKey, SettingValue) VALUES (@SettingKey, @SettingValue)"
            Return conn.Execute(sql, s) > 0
        End Using
    End Function

    Public Function Update(s As AppSetngs) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql = "UPDATE AppSetngs SET SettingValue = @SettingValue WHERE SettingKey = @SettingKey"
            Return conn.Execute(sql, s) > 0
        End Using
    End Function

    Public Function Delete(SettingKey As String) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Execute("DELETE FROM AppSetngs WHERE SettingKey = @SettingKey", New With {.SettingKey = SettingKey}) > 0
        End Using
    End Function

End Class
