Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_OtherTRTDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of Patient_OtherTRT)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            ' Materialize before connection closes (Dapper IEnumerable is deferred).
            Return conn.Query(Of Patient_OtherTRT)("SELECT * FROM Patient_OtherTRT").ToList()
        End Using
    End Function

    Public Function SelectAll(ByVal patientId As Integer) As IEnumerable(Of Patient_OtherTRT)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_OtherTRT)("SELECT * FROM Patient_OtherTRT WHERE PatientID = @PatientID", New With {.PatientID = patientId}).ToList()
        End Using
    End Function
    Public Function Select_Record(ByVal clsPatient_OtherTRT As Patient_OtherTRT) As Patient_OtherTRT
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient_OtherTRT WHERE OtherTrtID = @OtherTrtID"
            Return conn.QuerySingleOrDefault(Of Patient_OtherTRT)(sql, New With {.OtherTrtID = clsPatient_OtherTRT.OtherTrtID})
        End Using
    End Function

    Public Function Add(ByVal clsPatient_OtherTRT As Patient_OtherTRT) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Patient_OtherTRT (PatientID, Trt, TreatDate, TrtDetails, IsPaid) VALUES (@PatientID, @Trt, @TreatDate, @TrtDetails, @IsPaid)"
            RowsAffected = conn.Execute(sql, New With {.PatientID = clsPatient_OtherTRT.PatientID, .Trt = clsPatient_OtherTRT.Trt, .TreatDate = clsPatient_OtherTRT.TreatDate, .TrtDetails = clsPatient_OtherTRT.TrtDetails, .IsPaid = clsPatient_OtherTRT.IsPaid})
            Return RowsAffected > 0
        End Using
    End Function
    Public Function AddTransAndGetID(conn As SqlConnection, trans As SqlTransaction, ByVal clsPatient_OtherTRT As Patient_OtherTRT) As Integer
        Try
            Dim sql As String = "INSERT INTO Patient_OtherTRT (PatientID, Trt, TreatDate, TrtDetails, IsPaid) 
                            OUTPUT INSERTED.OtherTrtID 
                            VALUES (@PatientID, @Trt, @TreatDate, @TrtDetails, @IsPaid)"
            Dim result = conn.ExecuteScalar(sql, New With {
            .PatientID = clsPatient_OtherTRT.PatientID,
            .Trt = clsPatient_OtherTRT.Trt,
            .TreatDate = clsPatient_OtherTRT.TreatDate,
            .TrtDetails = clsPatient_OtherTRT.TrtDetails,
            .IsPaid = clsPatient_OtherTRT.IsPaid
        }, trans)

            Return If(result IsNot Nothing, Convert.ToInt32(result), -1)
        Catch ex As Exception
            MsgBox($"Error in AddTransAndGetID (Other Treat): {ex.Message}")
            Return -1
        End Try
    End Function
    Public Function Update(oldPatient_OtherTRT As Patient_OtherTRT, newPatient_OtherTRT As Patient_OtherTRT) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                .NewPatientID = newPatient_OtherTRT.PatientID, .OldPatientID = oldPatient_OtherTRT.PatientID, .NewTrt = newPatient_OtherTRT.Trt, .OldTrt = oldPatient_OtherTRT.Trt, .NewTreatDate = newPatient_OtherTRT.TreatDate, .OldTreatDate = oldPatient_OtherTRT.TreatDate, .NewTrtDetails = newPatient_OtherTRT.TrtDetails, .OldTrtDetails = oldPatient_OtherTRT.TrtDetails, .NewIsPaid = newPatient_OtherTRT.IsPaid, .OldIsPaid = oldPatient_OtherTRT.IsPaid
                                      }
            Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_OtherTRT] SET [PatientID] = @NewPatientID, [Trt] = @NewTrt, [TreatDate] = @NewTreatDate, [TrtDetails] = @NewTrtDetails, [IsPaid] = @NewIsPaid WHERE [PatientID] = @OldPatientID AND [Trt] = @OldTrt AND [TreatDate] = @OldTreatDate AND [TrtDetails] = @OldTrtDetails AND [IsPaid] = @OldIsPaid", parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsPatient_OtherTRT As Patient_OtherTRT) As Boolean
        Dim deleteStatement As String =
        "DELETE FROM [Patient_OtherTRT] 
			WHERE OtherTrtID = @OtherTrtID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.OtherTrtID = clsPatient_OtherTRT.OtherTrtID})
            Return affectedRows > 0
        End Using
    End Function


    'Methods to get parents and childs
End Class
