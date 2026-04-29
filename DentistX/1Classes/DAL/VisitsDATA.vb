Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class VisitsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Visits)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Visits)("SELECT * FROM Visits")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsVisits As Visits) As Visits
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Visits WHERE VisitDetID = @VisitDetID"
			    Return conn.QuerySingleOrDefault(Of Visits)(sql, New With { .VisitDetID = clsVisits.VisitDetID })
			End Using
		End Function

		Public Function Add(ByVal clsVisits As Visits) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Visits (PatientID, VtID, VisitDay, VisTime, VisTimeEnd, PatientName, VisDetail, VisNotes, VisDateTime) VALUES (@PatientID, @VtID, @VisitDay, @VisTime, @VisTimeEnd, @PatientName, @VisDetail, @VisNotes, @VisDateTime)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsVisits.PatientID, .VtID =  clsVisits.VtID, .VisitDay =  clsVisits.VisitDay, .VisTime =  clsVisits.VisTime, .VisTimeEnd =  clsVisits.VisTimeEnd, .PatientName =  clsVisits.PatientName, .VisDetail =  clsVisits.VisDetail, .VisNotes =  clsVisits.VisNotes, .VisDateTime =  clsVisits.VisDateTime })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldVisits As Visits, newVisits As Visits) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newVisits.PatientID, .OldPatientID = oldVisits.PatientID, .NewVtID = newVisits.VtID, .OldVtID = oldVisits.VtID, .NewVisitDay = newVisits.VisitDay, .OldVisitDay = oldVisits.VisitDay, .NewVisTime = newVisits.VisTime, .OldVisTime = oldVisits.VisTime, .NewVisTimeEnd = newVisits.VisTimeEnd, .OldVisTimeEnd = oldVisits.VisTimeEnd, .NewPatientName = newVisits.PatientName, .OldPatientName = oldVisits.PatientName, .NewVisDetail = newVisits.VisDetail, .OldVisDetail = oldVisits.VisDetail, .NewVisNotes = newVisits.VisNotes, .OldVisNotes = oldVisits.VisNotes, .NewVisDateTime = newVisits.VisDateTime, .OldVisDateTime = oldVisits.VisDateTime
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Visits] SET [PatientID] = @NewPatientID, [VtID] = @NewVtID, [VisitDay] = @NewVisitDay, [VisTime] = @NewVisTime, [VisTimeEnd] = @NewVisTimeEnd, [PatientName] = @NewPatientName, [VisDetail] = @NewVisDetail, [VisNotes] = @NewVisNotes, [VisDateTime] = @NewVisDateTime WHERE [PatientID] = @OldPatientID AND [VtID] = @OldVtID AND [VisitDay] = @OldVisitDay AND [VisTime] = @OldVisTime AND [VisTimeEnd] = @OldVisTimeEnd AND [PatientName] = @OldPatientName AND [VisDetail] = @OldVisDetail AND [VisNotes] = @OldVisNotes AND [VisDateTime] = @OldVisDateTime", parameters)
			    Return affectedRows > 0
			End Using
		End Function


    Public Function Delete(ByVal clsVisits As Visits) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Visits] 
			WHERE VisitDetID = @VisitDetID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .VisitDetID = clsVisits.VisitDetID })
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
