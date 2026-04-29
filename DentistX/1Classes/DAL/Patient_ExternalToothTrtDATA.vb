Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_ExternalToothTrtDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Patient_ExternalToothTrt)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Patient_ExternalToothTrt)("SELECT * FROM Patient_ExternalToothTrt")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPatient_ExternalToothTrt As Patient_ExternalToothTrt) As Patient_ExternalToothTrt
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Patient_ExternalToothTrt WHERE ExternalTrtID = @ExternalTrtID"
			    Return conn.QuerySingleOrDefault(Of Patient_ExternalToothTrt)(sql, New With { .ExternalTrtID = clsPatient_ExternalToothTrt.ExternalTrtID })
			End Using
		End Function

		Public Function Add(ByVal clsPatient_ExternalToothTrt As Patient_ExternalToothTrt) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Patient_ExternalToothTrt (PatientID, ToothNum, TreatmentType, ClinicName, TreatmentDate, Notes) VALUES (@PatientID, @ToothNum, @TreatmentType, @ClinicName, @TreatmentDate, @Notes)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsPatient_ExternalToothTrt.PatientID, .ToothNum =  clsPatient_ExternalToothTrt.ToothNum, .TreatmentType =  clsPatient_ExternalToothTrt.TreatmentType, .ClinicName =  clsPatient_ExternalToothTrt.ClinicName, .TreatmentDate =  clsPatient_ExternalToothTrt.TreatmentDate, .Notes =  clsPatient_ExternalToothTrt.Notes })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldPatient_ExternalToothTrt As Patient_ExternalToothTrt, newPatient_ExternalToothTrt As Patient_ExternalToothTrt) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newPatient_ExternalToothTrt.PatientID, .OldPatientID = oldPatient_ExternalToothTrt.PatientID, .NewToothNum = newPatient_ExternalToothTrt.ToothNum, .OldToothNum = oldPatient_ExternalToothTrt.ToothNum, .NewTreatmentType = newPatient_ExternalToothTrt.TreatmentType, .OldTreatmentType = oldPatient_ExternalToothTrt.TreatmentType, .NewClinicName = newPatient_ExternalToothTrt.ClinicName, .OldClinicName = oldPatient_ExternalToothTrt.ClinicName, .NewTreatmentDate = newPatient_ExternalToothTrt.TreatmentDate, .OldTreatmentDate = oldPatient_ExternalToothTrt.TreatmentDate, .NewNotes = newPatient_ExternalToothTrt.Notes, .OldNotes = oldPatient_ExternalToothTrt.Notes
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_ExternalToothTrt] SET [PatientID] = @NewPatientID, [ToothNum] = @NewToothNum, [TreatmentType] = @NewTreatmentType, [ClinicName] = @NewClinicName, [TreatmentDate] = @NewTreatmentDate, [Notes] = @NewNotes WHERE [PatientID] = @OldPatientID AND [ToothNum] = @OldToothNum AND [TreatmentType] = @OldTreatmentType AND [ClinicName] = @OldClinicName AND [TreatmentDate] = @OldTreatmentDate AND [Notes] = @OldNotes", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsPatient_ExternalToothTrt As Patient_ExternalToothTrt) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Patient_ExternalToothTrt] 
			WHERE ExternalTrtID = @ExternalTrtID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ExternalTrtID = clsPatient_ExternalToothTrt.ExternalTrtID })
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
				parent = conn.QuerySingleOrDefault(Of Patient)(query, New With {.PatientID = PatientID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
