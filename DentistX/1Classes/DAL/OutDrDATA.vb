Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class OutDrDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of OutDr)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of OutDr)("SELECT * FROM OutDr")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsOutDr As OutDr) As OutDr
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM OutDr WHERE DrID = @DrID"
			    Return conn.QuerySingleOrDefault(Of OutDr)(sql, New With { .DrID = clsOutDr.DrID })
			End Using
		End Function

		Public Function Add(ByVal clsOutDr As OutDr) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO OutDr (DrName, DrAdres, Drphone, DrMobile) VALUES (@DrName, @DrAdres, @Drphone, @DrMobile)" 
			    RowsAffected = conn.Execute(sql, New With { .DrName =  clsOutDr.DrName, .DrAdres =  clsOutDr.DrAdres, .Drphone =  clsOutDr.Drphone, .DrMobile =  clsOutDr.DrMobile })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldOutDr As OutDr, newOutDr As OutDr) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewDrName = newOutDr.DrName, .OldDrName = oldOutDr.DrName, .NewDrAdres = newOutDr.DrAdres, .OldDrAdres = oldOutDr.DrAdres, .NewDrphone = newOutDr.Drphone, .OldDrphone = oldOutDr.Drphone, .NewDrMobile = newOutDr.DrMobile, .OldDrMobile = oldOutDr.DrMobile
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [OutDr] SET [DrName] = @NewDrName, [DrAdres] = @NewDrAdres, [Drphone] = @NewDrphone, [DrMobile] = @NewDrMobile WHERE [DrName] = @OldDrName AND [DrAdres] = @OldDrAdres AND [Drphone] = @OldDrphone AND [DrMobile] = @OldDrMobile", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsOutDr As OutDr) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [OutDr] 
			WHERE DrID = @DrID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .DrID = clsOutDr.DrID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
