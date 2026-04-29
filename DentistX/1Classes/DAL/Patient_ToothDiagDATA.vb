Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_ToothDiagDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Patient_ToothDiag)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Patient_ToothDiag)("SELECT * FROM Patient_ToothDiag")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPatient_ToothDiag As Patient_ToothDiag) As Patient_ToothDiag
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Patient_ToothDiag WHERE DiagID = @DiagID And PatientID = @PatientID"
			    Return conn.QuerySingleOrDefault(Of Patient_ToothDiag)(sql, New With { .DiagID = clsPatient_ToothDiag.DiagID, .PatientID = clsPatient_ToothDiag.PatientID })
			End Using
		End Function

		Public Function Add(ByVal clsPatient_ToothDiag As Patient_ToothDiag) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Patient_ToothDiag (PatientID, ToothNum, ToothName, DiagDate, Diagnose, DiagDetails, DiagNotes) VALUES (@PatientID, @ToothNum, @ToothName, @DiagDate, @Diagnose, @DiagDetails, @DiagNotes)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsPatient_ToothDiag.PatientID, .ToothNum =  clsPatient_ToothDiag.ToothNum, .ToothName =  clsPatient_ToothDiag.ToothName, .DiagDate =  clsPatient_ToothDiag.DiagDate, .Diagnose =  clsPatient_ToothDiag.Diagnose, .DiagDetails =  clsPatient_ToothDiag.DiagDetails, .DiagNotes =  clsPatient_ToothDiag.DiagNotes })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldPatient_ToothDiag As Patient_ToothDiag, newPatient_ToothDiag As Patient_ToothDiag) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newPatient_ToothDiag.PatientID, .OldPatientID = oldPatient_ToothDiag.PatientID, .NewToothNum = newPatient_ToothDiag.ToothNum, .OldToothNum = oldPatient_ToothDiag.ToothNum, .NewToothName = newPatient_ToothDiag.ToothName, .OldToothName = oldPatient_ToothDiag.ToothName, .NewDiagDate = newPatient_ToothDiag.DiagDate, .OldDiagDate = oldPatient_ToothDiag.DiagDate, .NewDiagnose = newPatient_ToothDiag.Diagnose, .OldDiagnose = oldPatient_ToothDiag.Diagnose, .NewDiagDetails = newPatient_ToothDiag.DiagDetails, .OldDiagDetails = oldPatient_ToothDiag.DiagDetails, .NewDiagNotes = newPatient_ToothDiag.DiagNotes, .OldDiagNotes = oldPatient_ToothDiag.DiagNotes
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_ToothDiag] SET [PatientID] = @NewPatientID, [ToothNum] = @NewToothNum, [ToothName] = @NewToothName, [DiagDate] = @NewDiagDate, [Diagnose] = @NewDiagnose, [DiagDetails] = @NewDiagDetails, [DiagNotes] = @NewDiagNotes WHERE [PatientID] = @OldPatientID AND [ToothNum] = @OldToothNum AND [ToothName] = @OldToothName AND [DiagDate] = @OldDiagDate AND [Diagnose] = @OldDiagnose AND [DiagDetails] = @OldDiagDetails AND [DiagNotes] = @OldDiagNotes", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsPatient_ToothDiag As Patient_ToothDiag) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Patient_ToothDiag] 
			WHERE DiagID = @DiagID AND PatientID = @PatientID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .DiagID = clsPatient_ToothDiag.DiagID, .PatientID = clsPatient_ToothDiag.PatientID })
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
