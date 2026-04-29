Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblMeasureDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblMeasure)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblMeasure)("SELECT * FROM TblMeasure")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblMeasure As TblMeasure) As TblMeasure
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblMeasure WHERE MeasureID = @MeasureID"
			    Return conn.QuerySingleOrDefault(Of TblMeasure)(sql, New With { .MeasureID = clsTblMeasure.MeasureID })
			End Using
		End Function

		Public Function Add(ByVal clsTblMeasure As TblMeasure) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblMeasure (Measure) VALUES (@Measure)" 
			    RowsAffected = conn.Execute(sql, New With { .Measure =  clsTblMeasure.Measure })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblMeasure As TblMeasure, newTblMeasure As TblMeasure) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewMeasure = newTblMeasure.Measure, .OldMeasure = oldTblMeasure.Measure
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblMeasure] SET [Measure] = @NewMeasure WHERE [Measure] = @OldMeasure", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblMeasure As TblMeasure) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblMeasure] 
			WHERE MeasureID = @MeasureID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .MeasureID = clsTblMeasure.MeasureID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
