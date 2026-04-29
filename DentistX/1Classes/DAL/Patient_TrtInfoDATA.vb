Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_TrtInfoDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Patient_TrtInfo)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Patient_TrtInfo)("SELECT * FROM Patient_TrtInfo")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPatient_TrtInfo As Patient_TrtInfo) As Patient_TrtInfo
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Patient_TrtInfo WHERE TrtInfoID = @TrtInfoID"
			    Return conn.QuerySingleOrDefault(Of Patient_TrtInfo)(sql, New With { .TrtInfoID = clsPatient_TrtInfo.TrtInfoID })
			End Using
		End Function

		Public Function Add(ByVal clsPatient_TrtInfo As Patient_TrtInfo) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Patient_TrtInfo (PatientID, ParentToothTrtID, TrtGroupID, ToothNum, ToothName, TreatDate, Treat, TreatNotes, IsExternal, ExternalClinicName, ExternalTreatmentDate) VALUES (@PatientID, @ParentToothTrtID, @TrtGroupID, @ToothNum, @ToothName, @TreatDate, @Treat, @TreatNotes, @IsExternal, @ExternalClinicName, @ExternalTreatmentDate)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsPatient_TrtInfo.PatientID, .ParentToothTrtID =  clsPatient_TrtInfo.ParentToothTrtID, .TrtGroupID =  clsPatient_TrtInfo.TrtGroupID, .ToothNum =  clsPatient_TrtInfo.ToothNum, .ToothName =  clsPatient_TrtInfo.ToothName, .TreatDate =  clsPatient_TrtInfo.TreatDate, .Treat =  clsPatient_TrtInfo.Treat, .TreatNotes =  clsPatient_TrtInfo.TreatNotes, .IsExternal =  clsPatient_TrtInfo.IsExternal, .ExternalClinicName =  clsPatient_TrtInfo.ExternalClinicName, .ExternalTreatmentDate =  clsPatient_TrtInfo.ExternalTreatmentDate })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldPatient_TrtInfo As Patient_TrtInfo, newPatient_TrtInfo As Patient_TrtInfo) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newPatient_TrtInfo.PatientID, .OldPatientID = oldPatient_TrtInfo.PatientID, .NewParentToothTrtID = newPatient_TrtInfo.ParentToothTrtID, .OldParentToothTrtID = oldPatient_TrtInfo.ParentToothTrtID, .NewTrtGroupID = newPatient_TrtInfo.TrtGroupID, .OldTrtGroupID = oldPatient_TrtInfo.TrtGroupID, .NewToothNum = newPatient_TrtInfo.ToothNum, .OldToothNum = oldPatient_TrtInfo.ToothNum, .NewToothName = newPatient_TrtInfo.ToothName, .OldToothName = oldPatient_TrtInfo.ToothName, .NewTreatDate = newPatient_TrtInfo.TreatDate, .OldTreatDate = oldPatient_TrtInfo.TreatDate, .NewTreat = newPatient_TrtInfo.Treat, .OldTreat = oldPatient_TrtInfo.Treat, .NewTreatNotes = newPatient_TrtInfo.TreatNotes, .OldTreatNotes = oldPatient_TrtInfo.TreatNotes, .NewIsExternal = newPatient_TrtInfo.IsExternal, .OldIsExternal = oldPatient_TrtInfo.IsExternal, .NewExternalClinicName = newPatient_TrtInfo.ExternalClinicName, .OldExternalClinicName = oldPatient_TrtInfo.ExternalClinicName, .NewExternalTreatmentDate = newPatient_TrtInfo.ExternalTreatmentDate, .OldExternalTreatmentDate = oldPatient_TrtInfo.ExternalTreatmentDate
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_TrtInfo] SET [PatientID] = @NewPatientID, [ParentToothTrtID] = @NewParentToothTrtID, [TrtGroupID] = @NewTrtGroupID, [ToothNum] = @NewToothNum, [ToothName] = @NewToothName, [TreatDate] = @NewTreatDate, [Treat] = @NewTreat, [TreatNotes] = @NewTreatNotes, [IsExternal] = @NewIsExternal, [ExternalClinicName] = @NewExternalClinicName, [ExternalTreatmentDate] = @NewExternalTreatmentDate WHERE [PatientID] = @OldPatientID AND [ParentToothTrtID] = @OldParentToothTrtID AND [TrtGroupID] = @OldTrtGroupID AND [ToothNum] = @OldToothNum AND [ToothName] = @OldToothName AND [TreatDate] = @OldTreatDate AND [Treat] = @OldTreat AND [TreatNotes] = @OldTreatNotes AND [IsExternal] = @OldIsExternal AND [ExternalClinicName] = @OldExternalClinicName AND [ExternalTreatmentDate] = @OldExternalTreatmentDate", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsPatient_TrtInfo As Patient_TrtInfo) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Patient_TrtInfo] 
			WHERE TrtInfoID = @TrtInfoID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .TrtInfoID = clsPatient_TrtInfo.TrtInfoID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
