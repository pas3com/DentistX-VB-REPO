Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_DiagInfoDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Patient_DiagInfo)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Patient_DiagInfo)("SELECT * FROM Patient_DiagInfo")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPatient_DiagInfo As Patient_DiagInfo) As Patient_DiagInfo
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Patient_DiagInfo WHERE TrtInfoID = @TrtInfoID"
			    Return conn.QuerySingleOrDefault(Of Patient_DiagInfo)(sql, New With { .TrtInfoID = clsPatient_DiagInfo.TrtInfoID })
			End Using
		End Function

		Public Function Add(ByVal clsPatient_DiagInfo As Patient_DiagInfo) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Patient_DiagInfo (PatientID, ParentDiagID, TrtGroupID, ToothNum, ToothName, TreatDate, Treat, TreatNotes, IsExternal, ExternalClinicName, ExternalTreatmentDate) VALUES (@PatientID, @ParentDiagID, @TrtGroupID, @ToothNum, @ToothName, @TreatDate, @Treat, @TreatNotes, @IsExternal, @ExternalClinicName, @ExternalTreatmentDate)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsPatient_DiagInfo.PatientID, .ParentDiagID =  clsPatient_DiagInfo.ParentDiagID, .TrtGroupID =  clsPatient_DiagInfo.TrtGroupID, .ToothNum =  clsPatient_DiagInfo.ToothNum, .ToothName =  clsPatient_DiagInfo.ToothName, .TreatDate =  clsPatient_DiagInfo.TreatDate, .Treat =  clsPatient_DiagInfo.Treat, .TreatNotes =  clsPatient_DiagInfo.TreatNotes, .IsExternal =  clsPatient_DiagInfo.IsExternal, .ExternalClinicName =  clsPatient_DiagInfo.ExternalClinicName, .ExternalTreatmentDate =  clsPatient_DiagInfo.ExternalTreatmentDate })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldPatient_DiagInfo As Patient_DiagInfo, newPatient_DiagInfo As Patient_DiagInfo) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newPatient_DiagInfo.PatientID, .OldPatientID = oldPatient_DiagInfo.PatientID, .NewParentDiagID = newPatient_DiagInfo.ParentDiagID, .OldParentDiagID = oldPatient_DiagInfo.ParentDiagID, .NewTrtGroupID = newPatient_DiagInfo.TrtGroupID, .OldTrtGroupID = oldPatient_DiagInfo.TrtGroupID, .NewToothNum = newPatient_DiagInfo.ToothNum, .OldToothNum = oldPatient_DiagInfo.ToothNum, .NewToothName = newPatient_DiagInfo.ToothName, .OldToothName = oldPatient_DiagInfo.ToothName, .NewTreatDate = newPatient_DiagInfo.TreatDate, .OldTreatDate = oldPatient_DiagInfo.TreatDate, .NewTreat = newPatient_DiagInfo.Treat, .OldTreat = oldPatient_DiagInfo.Treat, .NewTreatNotes = newPatient_DiagInfo.TreatNotes, .OldTreatNotes = oldPatient_DiagInfo.TreatNotes, .NewIsExternal = newPatient_DiagInfo.IsExternal, .OldIsExternal = oldPatient_DiagInfo.IsExternal, .NewExternalClinicName = newPatient_DiagInfo.ExternalClinicName, .OldExternalClinicName = oldPatient_DiagInfo.ExternalClinicName, .NewExternalTreatmentDate = newPatient_DiagInfo.ExternalTreatmentDate, .OldExternalTreatmentDate = oldPatient_DiagInfo.ExternalTreatmentDate
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_DiagInfo] SET [PatientID] = @NewPatientID, [ParentDiagID] = @NewParentDiagID, [TrtGroupID] = @NewTrtGroupID, [ToothNum] = @NewToothNum, [ToothName] = @NewToothName, [TreatDate] = @NewTreatDate, [Treat] = @NewTreat, [TreatNotes] = @NewTreatNotes, [IsExternal] = @NewIsExternal, [ExternalClinicName] = @NewExternalClinicName, [ExternalTreatmentDate] = @NewExternalTreatmentDate WHERE [PatientID] = @OldPatientID AND [ParentDiagID] = @OldParentDiagID AND [TrtGroupID] = @OldTrtGroupID AND [ToothNum] = @OldToothNum AND [ToothName] = @OldToothName AND [TreatDate] = @OldTreatDate AND [Treat] = @OldTreat AND [TreatNotes] = @OldTreatNotes AND [IsExternal] = @OldIsExternal AND [ExternalClinicName] = @OldExternalClinicName AND [ExternalTreatmentDate] = @OldExternalTreatmentDate", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsPatient_DiagInfo As Patient_DiagInfo) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Patient_DiagInfo] 
			WHERE TrtInfoID = @TrtInfoID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .TrtInfoID = clsPatient_DiagInfo.TrtInfoID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
