Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class GenderDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Gender)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Gender)("SELECT * FROM Gender")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsGender As Gender) As Gender
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Gender WHERE SID = @SID"
			    Return conn.QuerySingleOrDefault(Of Gender)(sql, New With { .SID = clsGender.SID })
			End Using
		End Function

		Public Function Add(ByVal clsGender As Gender) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Gender (Sex) VALUES (@Sex)" 
			    RowsAffected = conn.Execute(sql, New With { .Sex =  clsGender.Sex })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldGender As Gender, newGender As Gender) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewSex = newGender.Sex, .OldSex = oldGender.Sex
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Gender] SET [Sex] = @NewSex WHERE [Sex] = @OldSex", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsGender As Gender) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Gender] 
			WHERE SID = @SID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .SID = clsGender.SID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
