Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class VisitTypesDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of VisitTypes)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of VisitTypes)("SELECT * FROM VisitTypes")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsVisitTypes As VisitTypes) As VisitTypes
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM VisitTypes WHERE VtID = @VtID"
			    Return conn.QuerySingleOrDefault(Of VisitTypes)(sql, New With { .VtID = clsVisitTypes.VtID })
			End Using
		End Function

		Public Function Add(ByVal clsVisitTypes As VisitTypes) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO VisitTypes (VisitType, VisitTypeAr) VALUES (@VisitType, @VisitTypeAr)" 
			    RowsAffected = conn.Execute(sql, New With { .VisitType =  clsVisitTypes.VisitType, .VisitTypeAr =  clsVisitTypes.VisitTypeAr })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldVisitTypes As VisitTypes, newVisitTypes As VisitTypes) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewVisitType = newVisitTypes.VisitType, .OldVisitType = oldVisitTypes.VisitType, .NewVisitTypeAr = newVisitTypes.VisitTypeAr, .OldVisitTypeAr = oldVisitTypes.VisitTypeAr
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [VisitTypes] SET [VisitType] = @NewVisitType, [VisitTypeAr] = @NewVisitTypeAr WHERE [VisitType] = @OldVisitType AND [VisitTypeAr] = @OldVisitTypeAr", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsVisitTypes As VisitTypes) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [VisitTypes] 
			WHERE VtID = @VtID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .VtID = clsVisitTypes.VtID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
