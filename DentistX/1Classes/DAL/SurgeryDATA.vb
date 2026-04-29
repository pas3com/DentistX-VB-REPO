Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class SurgeryDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Surgery)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Surgery)("SELECT * FROM Surgery")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsSurgery As Surgery) As Surgery
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Surgery WHERE SurgID = @SurgID"
			    Return conn.QuerySingleOrDefault(Of Surgery)(sql, New With { .SurgID = clsSurgery.SurgID })
			End Using
		End Function

		Public Function Add(ByVal clsSurgery As Surgery) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Surgery (PatientID, SurgeryDet, SurDate) VALUES (@PatientID, @SurgeryDet, @SurDate)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsSurgery.PatientID, .SurgeryDet =  clsSurgery.SurgeryDet, .SurDate =  clsSurgery.SurDate })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldSurgery As Surgery, newSurgery As Surgery) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newSurgery.PatientID, .OldPatientID = oldSurgery.PatientID, .NewSurgeryDet = newSurgery.SurgeryDet, .OldSurgeryDet = oldSurgery.SurgeryDet, .NewSurDate = newSurgery.SurDate, .OldSurDate = oldSurgery.SurDate
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Surgery] SET [PatientID] = @NewPatientID, [SurgeryDet] = @NewSurgeryDet, [SurDate] = @NewSurDate WHERE [PatientID] = @OldPatientID AND [SurgeryDet] = @OldSurgeryDet AND [SurDate] = @OldSurDate", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsSurgery As Surgery) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Surgery] 
			WHERE SurgID = @SurgID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .SurgID = clsSurgery.SurgID })
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
