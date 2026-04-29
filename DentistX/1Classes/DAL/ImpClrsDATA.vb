Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImpClrsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of ImpClrs)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of ImpClrs)("SELECT * FROM ImpClrs")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsImpClrs As ImpClrs) As ImpClrs
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM ImpClrs WHERE ImpClrID = @ImpClrID"
			    Return conn.QuerySingleOrDefault(Of ImpClrs)(sql, New With { .ImpClrID = clsImpClrs.ImpClrID })
			End Using
		End Function

		Public Function Add(ByVal clsImpClrs As ImpClrs) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO ImpClrs (ImpClr) VALUES (@ImpClr)" 
			    RowsAffected = conn.Execute(sql, New With { .ImpClr =  clsImpClrs.ImpClr })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldImpClrs As ImpClrs, newImpClrs As ImpClrs) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewImpClr = newImpClrs.ImpClr, .OldImpClr = oldImpClrs.ImpClr
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [ImpClrs] SET [ImpClr] = @NewImpClr WHERE [ImpClr] = @OldImpClr", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsImpClrs As ImpClrs) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [ImpClrs] 
			WHERE ImpClrID = @ImpClrID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ImpClrID = clsImpClrs.ImpClrID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
