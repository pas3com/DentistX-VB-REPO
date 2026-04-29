Imports System.Data.SqlClient
Imports Dapper

Public Class Patient_MobInfoDiagDATA


    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of Patient_MobInfoDiag)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_MobInfoDiag)("SELECT * FROM Patient_MobInfoDiag")
        End Using
    End Function


    Public Function Select_Record(ByVal clsPatient_MobInfo As Patient_MobInfoDiag) As Patient_MobInfoDiag
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient_MobInfoDiag WHERE MobInfoID = @MobInfoID"
            Return conn.QuerySingleOrDefault(Of Patient_MobInfoDiag)(sql, New With {.MobInfoID = clsPatient_MobInfo.MobInfoID})
        End Using
    End Function

    Public Function Add(ByVal clsPatient_MobInfo As Patient_MobInfoDiag) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Patient_MobInfoDiag (PatientID, ParentToothTrtID, TrtGroupID, ToothNum, ToothName, TreatDate, Treat, TreatNotes, IsExternal, ExternalClinicName, ExternalTreatmentDate) VALUES (@PatientID, @ParentToothTrtID, @TrtGroupID, @ToothNum, @ToothName, @TreatDate, @Treat, @TreatNotes, @IsExternal, @ExternalClinicName, @ExternalTreatmentDate)"
            RowsAffected = conn.Execute(sql, New With {.PatientID = clsPatient_MobInfo.PatientID, .ParentToothTrtID = clsPatient_MobInfo.ParentToothTrtID, .TrtGroupID = clsPatient_MobInfo.TrtGroupID, .ToothNum = clsPatient_MobInfo.ToothNum, .ToothName = clsPatient_MobInfo.ToothName, .TreatDate = clsPatient_MobInfo.TreatDate, .Treat = clsPatient_MobInfo.Treat, .TreatNotes = clsPatient_MobInfo.TreatNotes, .IsExternal = clsPatient_MobInfo.IsExternal, .ExternalClinicName = clsPatient_MobInfo.ExternalClinicName, .ExternalTreatmentDate = clsPatient_MobInfo.ExternalTreatmentDate})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldPatient_MobInfo As Patient_MobInfoDiag, newPatient_MobInfo As Patient_MobInfoDiag) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                .NewPatientID = newPatient_MobInfo.PatientID, .OldPatientID = oldPatient_MobInfo.PatientID, .NewParentToothTrtID = newPatient_MobInfo.ParentToothTrtID, .OldParentToothTrtID = oldPatient_MobInfo.ParentToothTrtID, .NewTrtGroupID = newPatient_MobInfo.TrtGroupID, .OldTrtGroupID = oldPatient_MobInfo.TrtGroupID, .NewToothNum = newPatient_MobInfo.ToothNum, .OldToothNum = oldPatient_MobInfo.ToothNum, .NewToothName = newPatient_MobInfo.ToothName, .OldToothName = oldPatient_MobInfo.ToothName, .NewTreatDate = newPatient_MobInfo.TreatDate, .OldTreatDate = oldPatient_MobInfo.TreatDate, .NewTreat = newPatient_MobInfo.Treat, .OldTreat = oldPatient_MobInfo.Treat, .NewTreatNotes = newPatient_MobInfo.TreatNotes, .OldTreatNotes = oldPatient_MobInfo.TreatNotes, .NewIsExternal = newPatient_MobInfo.IsExternal, .OldIsExternal = oldPatient_MobInfo.IsExternal, .NewExternalClinicName = newPatient_MobInfo.ExternalClinicName, .OldExternalClinicName = oldPatient_MobInfo.ExternalClinicName, .NewExternalTreatmentDate = newPatient_MobInfo.ExternalTreatmentDate, .OldExternalTreatmentDate = oldPatient_MobInfo.ExternalTreatmentDate
                                      }
            Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_MobInfoDiag] SET [PatientID] = @NewPatientID, [ParentToothTrtID] = @NewParentToothTrtID, [TrtGroupID] = @NewTrtGroupID, [ToothNum] = @NewToothNum, [ToothName] = @NewToothName, [TreatDate] = @NewTreatDate, [Treat] = @NewTreat, [TreatNotes] = @NewTreatNotes, [IsExternal] = @NewIsExternal, [ExternalClinicName] = @NewExternalClinicName, [ExternalTreatmentDate] = @NewExternalTreatmentDate WHERE [PatientID] = @OldPatientID AND [ParentToothTrtID] = @OldParentToothTrtID AND [TrtGroupID] = @OldTrtGroupID AND [ToothNum] = @OldToothNum AND [ToothName] = @OldToothName AND [TreatDate] = @OldTreatDate AND [Treat] = @OldTreat AND [TreatNotes] = @OldTreatNotes AND [IsExternal] = @OldIsExternal AND [ExternalClinicName] = @OldExternalClinicName AND [ExternalTreatmentDate] = @OldExternalTreatmentDate", parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsPatient_MobInfo As Patient_MobInfoDiag) As Boolean
        Dim deleteStatement As String =
        "DELETE FROM [Patient_MobInfoDiag] 
			WHERE MobInfoID = @MobInfoID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.MobInfoID = clsPatient_MobInfo.MobInfoID})
            Return affectedRows > 0
        End Using
    End Function


    'Methods to get parents and childs
End Class

