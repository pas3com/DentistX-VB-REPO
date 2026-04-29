Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Y_TRTDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Y_TRT)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Y_TRT)("SELECT * FROM Y_TRT")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsY_TRT As Y_TRT) As Y_TRT
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Y_TRT WHERE YName = @YName"
			    Return conn.QuerySingleOrDefault(Of Y_TRT)(sql, New With { .YName = clsY_TRT.YName })
			End Using
		End Function

		Public Function Add(ByVal clsY_TRT As Y_TRT) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Y_TRT (YName, YYName) VALUES (@YName, @YYName)" 
			    RowsAffected = conn.Execute(sql, New With { .YName =  clsY_TRT.YName, .YYName =  clsY_TRT.YYName })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldY_TRT As Y_TRT, newY_TRT As Y_TRT) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewYName = newY_TRT.YName, .OldYName = oldY_TRT.YName, .NewYYName = newY_TRT.YYName, .OldYYName = oldY_TRT.YYName
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Y_TRT] SET [YName] = @NewYName, [YYName] = @NewYYName WHERE [YName] = @OldYName AND [YYName] = @OldYYName", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsY_TRT As Y_TRT) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Y_TRT] 
			WHERE YName = @YName"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .YName = clsY_TRT.YName })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
