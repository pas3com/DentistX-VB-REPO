Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class VisitTypeDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of VisitType)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of VisitType)("SELECT * FROM VisitType")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsVisitType As VisitType) As VisitType
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM VisitType WHERE VtID = @VtID"
			    Return conn.QuerySingleOrDefault(Of VisitType)(sql, New With { .VtID = clsVisitType.VtID })
			End Using
		End Function

		Public Function Add(ByVal clsVisitType As VisitType) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO VisitType (VisType) VALUES (@VisType)" 
			    RowsAffected = conn.Execute(sql, New With { .VisType =  clsVisitType.VisitType })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldVisitType As VisitType, newVisitType As VisitType) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewVisType = newVisitType.VisitType, .OldVisType = oldVisitType.VisitType
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [VisitType] SET [VisType] = @NewVisType WHERE [VisType] = @OldVisType", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsVisitType As VisitType) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [VisitType] 
			WHERE VtID = @VtID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .VtID = clsVisitType.VtID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
