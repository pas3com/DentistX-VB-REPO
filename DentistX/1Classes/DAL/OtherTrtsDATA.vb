Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class OtherTrtsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of OtherTrts)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of OtherTrts)("SELECT * FROM OtherTrts")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsOtherTrts As OtherTrts) As OtherTrts
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM OtherTrts WHERE OTrtID = @OTrtID"
			    Return conn.QuerySingleOrDefault(Of OtherTrts)(sql, New With { .OTrtID = clsOtherTrts.OTrtID })
			End Using
		End Function

		Public Function Add(ByVal clsOtherTrts As OtherTrts) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO OtherTrts (PatientID, TrtItemID, OtherTrtsDet, TrtDate) VALUES (@PatientID, @TrtItemID, @OtherTrtsDet, @TrtDate)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsOtherTrts.PatientID, .TrtItemID =  clsOtherTrts.TrtItemID, .OtherTrtsDet =  clsOtherTrts.OtherTrtsDet, .TrtDate =  clsOtherTrts.TrtDate })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldOtherTrts As OtherTrts, newOtherTrts As OtherTrts) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newOtherTrts.PatientID, .OldPatientID = oldOtherTrts.PatientID, .NewTrtItemID = newOtherTrts.TrtItemID, .OldTrtItemID = oldOtherTrts.TrtItemID, .NewOtherTrtsDet = newOtherTrts.OtherTrtsDet, .OldOtherTrtsDet = oldOtherTrts.OtherTrtsDet, .NewTrtDate = newOtherTrts.TrtDate, .OldTrtDate = oldOtherTrts.TrtDate
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [OtherTrts] SET [PatientID] = @NewPatientID, [TrtItemID] = @NewTrtItemID, [OtherTrtsDet] = @NewOtherTrtsDet, [TrtDate] = @NewTrtDate WHERE [PatientID] = @OldPatientID AND [TrtItemID] = @OldTrtItemID AND [OtherTrtsDet] = @OldOtherTrtsDet AND [TrtDate] = @OldTrtDate", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsOtherTrts As OtherTrts) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [OtherTrts] 
			WHERE OTrtID = @OTrtID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .OTrtID = clsOtherTrts.OTrtID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetPatient(ByVal PatientID As Integer) As Patient
		Dim parent As Patient = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [Patient] WHERE [PatientID] = @PatientID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of Patient)(query, New With {.PatientId = PatientID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
