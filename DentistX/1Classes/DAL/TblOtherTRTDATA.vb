Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblOtherTRTDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblOtherTRT)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblOtherTRT)("SELECT * FROM TblOtherTRT")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblOtherTRT As TblOtherTRT) As TblOtherTRT
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblOtherTRT WHERE TblOtherTrtID = @TblOtherTrtID"
			    Return conn.QuerySingleOrDefault(Of TblOtherTRT)(sql, New With { .TblOtherTrtID = clsTblOtherTRT.TblOtherTrtID })
			End Using
		End Function

		Public Function Add(ByVal clsTblOtherTRT As TblOtherTRT) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblOtherTRT (Trt) VALUES (@Trt)" 
			    RowsAffected = conn.Execute(sql, New With { .Trt =  clsTblOtherTRT.Trt })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblOtherTRT As TblOtherTRT, newTblOtherTRT As TblOtherTRT) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewTrt = newTblOtherTRT.Trt, .OldTrt = oldTblOtherTRT.Trt
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblOtherTRT] SET [Trt] = @NewTrt WHERE [Trt] = @OldTrt", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblOtherTRT As TblOtherTRT) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblOtherTRT] 
			WHERE TblOtherTrtID = @TblOtherTrtID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .TblOtherTrtID = clsTblOtherTRT.TblOtherTrtID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
