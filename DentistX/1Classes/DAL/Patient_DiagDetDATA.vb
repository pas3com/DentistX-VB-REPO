Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_DiagDetDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of Patient_DiagDet)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_DiagDet)("SELECT * FROM Patient_DiagDet")
        End Using
    End Function

    Public Function SelectAllByPatient(patientID As Integer) As IEnumerable(Of Patient_DiagDet)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_DiagDet)("SELECT * FROM Patient_DiagDet WHERE PatientID=@PatientID", New With {.PatientID = patientID})
        End Using
    End Function

    Public Function Select_Record(ByVal clsPatient_DiagDet As Patient_DiagDet) As Patient_DiagDet
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient_DiagDet WHERE DiagDetID = @DiagDetID"
            Return conn.QuerySingleOrDefault(Of Patient_DiagDet)(sql, New With {.DiagDetID = clsPatient_DiagDet.DiagDetID})
        End Using
    End Function

    Public Function Add(ByVal clsPatient_DiagDet As Patient_DiagDet) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Patient_DiagDet (DiagIDs, PatientID, DiagDate, TreatPlan, DiagAgreament, DiagDetails, DiagNotes, DateToStart, TotalValue, AdvancePay, UserID) VALUES (@DiagIDs, @PatientID, @DiagDate, @TreatPlan, @DiagAgreament, @DiagDetails, @DiagNotes, @DateToStart, @TotalValue, @AdvancePay, @UserID)"
            RowsAffected = conn.Execute(sql, New With {.DiagIDs = clsPatient_DiagDet.DiagIDs, .PatientID = clsPatient_DiagDet.PatientID, .DiagDate = clsPatient_DiagDet.DiagDate, .TreatPlan = clsPatient_DiagDet.TreatPlan, .DiagAgreament = clsPatient_DiagDet.DiagAgreament, .DiagDetails = clsPatient_DiagDet.DiagDetails, .DiagNotes = clsPatient_DiagDet.DiagNotes, .DateToStart = clsPatient_DiagDet.DateToStart, .TotalValue = clsPatient_DiagDet.TotalValue, .AdvancePay = clsPatient_DiagDet.AdvancePay, .UserID = clsPatient_DiagDet.UserID})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldPatient_DiagDet As Patient_DiagDet, newPatient_DiagDet As Patient_DiagDet) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
            .NewDiagIDs = newPatient_DiagDet.DiagIDs,
            .NewPatientID = newPatient_DiagDet.PatientID,
            .NewDiagDate = newPatient_DiagDet.DiagDate,
            .NewTreatPlan = newPatient_DiagDet.TreatPlan,
            .NewDiagAgreament = newPatient_DiagDet.DiagAgreament,
            .NewDiagDetails = newPatient_DiagDet.DiagDetails,
            .NewDiagNotes = newPatient_DiagDet.DiagNotes,
            .NewDateToStart = newPatient_DiagDet.DateToStart,
            .NewTotalValue = newPatient_DiagDet.TotalValue,
            .NewAdvancePay = newPatient_DiagDet.AdvancePay,
            .NewUserID = newPatient_DiagDet.UserID,
            .DiagDetID = oldPatient_DiagDet.DiagDetID,
            .OldPatientID = oldPatient_DiagDet.PatientID,
            .OldDiagIDs = oldPatient_DiagDet.DiagIDs
        }

            Dim affectedRows As Integer = conn.Execute(
            "UPDATE [Patient_DiagDet] SET 
                [DiagIDs] = @NewDiagIDs, 
                [PatientID] = @NewPatientID, 
                [DiagDate] = @NewDiagDate, 
                [TreatPlan] = @NewTreatPlan, 
                [DiagAgreament] = @NewDiagAgreament, 
                [DiagDetails] = @NewDiagDetails, 
                [DiagNotes] = @NewDiagNotes, 
                [DateToStart] = @NewDateToStart, 
                [TotalValue] = @NewTotalValue, 
                [AdvancePay] = @NewAdvancePay, 
                [UserID] = @NewUserID 
            WHERE [DiagDetID] = @DiagDetID 
                AND [PatientID] = @OldPatientID 
                AND [DiagIDs] = @OldDiagIDs",
            parameters)

            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsPatient_DiagDet As Patient_DiagDet) As Boolean
        Dim deleteStatement As String =
        "DELETE FROM [Patient_DiagDet] 
			WHERE DiagDetID = @DiagDetID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.DiagDetID = clsPatient_DiagDet.DiagDetID})
            Return affectedRows > 0
        End Using
    End Function


    'Methods to get parents and childs
End Class
