Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_ToothTrtHistoryDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of Patient_ToothTrtHistory)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_ToothTrtHistory)("SELECT * FROM Patient_ToothTrtHistory")
        End Using
    End Function


    Public Function Select_Record(ByVal clsPatient_ToothTrtHistory As Patient_ToothTrtHistory) As Patient_ToothTrtHistory
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient_ToothTrtHistory WHERE TrtHistID = @TrtHistID"
            Return conn.QuerySingleOrDefault(Of Patient_ToothTrtHistory)(sql, New With {.TrtHistID = clsPatient_ToothTrtHistory.TrtHistID})
        End Using
    End Function

    Public Function Add(ByVal clsPatient_ToothTrtHistory As Patient_ToothTrtHistory) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Patient_ToothTrtHistory (TrtID,
			PatientID,
			ToothNum,
			PropertyName,
			FillColor,
			BorderThickness,
			BorderColor,
			Treatment,
			TreatPlan,
			TreatDetails,
			TreatStatus,
			StartDate,
			EndDate,
			Notes,
			Finished) VALUES (@TrtID,
			@PatientID,
			@ToothNum,
			@PropertyName,
			@FillColor,
			@BorderThickness,
			@BorderColor,
			@Treatment,
			@TreatPlan,
			@TreatDetails,
			@TreatStatus,
			@StartDate,
			@EndDate,
			@Notes,
			@Finished)"
            RowsAffected = conn.Execute(sql, New With {.TrtID = clsPatient_ToothTrtHistory.TrtID,
                    .PatientID = clsPatient_ToothTrtHistory.PatientID,
                    .ToothNum = clsPatient_ToothTrtHistory.ToothNum,
                    .PropertyName = clsPatient_ToothTrtHistory.PropertyName,
                    .FillColor = clsPatient_ToothTrtHistory.FillColor,
                    .BorderThickness = clsPatient_ToothTrtHistory.BorderThickness,
                    .BorderColor = clsPatient_ToothTrtHistory.BorderColor,
                    .Treatment = clsPatient_ToothTrtHistory.Treatment,
                    .TreatPlan = clsPatient_ToothTrtHistory.TreatPlan,
                    .TreatDetails = clsPatient_ToothTrtHistory.TreatDetails,
                    .TreatStatus = clsPatient_ToothTrtHistory.TreatStatus,
                    .StartDate = clsPatient_ToothTrtHistory.StartDate,
                    .EndDate = clsPatient_ToothTrtHistory.EndDate,
                    .Notes = clsPatient_ToothTrtHistory.Notes,
                    .Finished = clsPatient_ToothTrtHistory.Finished})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldPatient_ToothTrtHistory As Patient_ToothTrtHistory, newPatient_ToothTrtHistory As Patient_ToothTrtHistory) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .NewTrtHistID = newPatient_ToothTrtHistory.TrtHistID, .OldTrtHistID = oldPatient_ToothTrtHistory.TrtHistID,
                    .NewTrtID = newPatient_ToothTrtHistory.TrtID, .OldTrtID = oldPatient_ToothTrtHistory.TrtID,
                    .NewPatientID = newPatient_ToothTrtHistory.PatientID, .OldPatientID = oldPatient_ToothTrtHistory.PatientID,
                    .NewToothNum = newPatient_ToothTrtHistory.ToothNum, .OldToothNum = oldPatient_ToothTrtHistory.ToothNum,
                    .NewPropertyName = newPatient_ToothTrtHistory.PropertyName, .OldPropertyName = oldPatient_ToothTrtHistory.PropertyName,
                    .NewFillColor = newPatient_ToothTrtHistory.FillColor, .OldFillColor = oldPatient_ToothTrtHistory.FillColor,
                    .NewBorderThickness = newPatient_ToothTrtHistory.BorderThickness, .OldBorderThickness = oldPatient_ToothTrtHistory.BorderThickness,
                    .NewBorderColor = newPatient_ToothTrtHistory.BorderColor, .OldBorderColor = oldPatient_ToothTrtHistory.BorderColor,
                    .NewTreatment = newPatient_ToothTrtHistory.Treatment, .OldTreatment = oldPatient_ToothTrtHistory.Treatment,
                    .NewTreatPlan = newPatient_ToothTrtHistory.TreatPlan, .OldTreatPlan = oldPatient_ToothTrtHistory.TreatPlan,
                    .NewTreatDetails = newPatient_ToothTrtHistory.TreatDetails, .OldTreatDetails = oldPatient_ToothTrtHistory.TreatDetails,
                    .NewTreatStatus = newPatient_ToothTrtHistory.TreatStatus, .OldTreatStatus = oldPatient_ToothTrtHistory.TreatStatus,
                    .NewStartDate = newPatient_ToothTrtHistory.StartDate, .OldStartDate = oldPatient_ToothTrtHistory.StartDate,
                    .NewEndDate = newPatient_ToothTrtHistory.EndDate, .OldEndDate = oldPatient_ToothTrtHistory.EndDate,
                    .NewNotes = newPatient_ToothTrtHistory.Notes, .OldNotes = oldPatient_ToothTrtHistory.Notes,
                    .NewFinished = newPatient_ToothTrtHistory.Finished, .OldFinished = oldPatient_ToothTrtHistory.Finished
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_ToothTrtHistory] SET 
			[TrtHistID] = @NewTrtHistID,
			[TrtID] = @NewTrtID,
			[PatientID] = @NewPatientID,
			[ToothNum] = @NewToothNum,
			[PropertyName] = @NewPropertyName,
			[FillColor] = @NewFillColor,
			[BorderThickness] = @NewBorderThickness,
			[BorderColor] = @NewBorderColor,
			[Treatment] = @NewTreatment,
			[TreatPlan] = @NewTreatPlan,
			[TreatDetails] = @NewTreatDetails,
			[TreatStatus] = @NewTreatStatus,
			[StartDate] = @NewStartDate,
			[EndDate] = @NewEndDate,
			[Notes] = @NewNotes,
			[Finished] = @NewFinished
			 WHERE 
			TrtHistID = @TrtHistID", parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsPatient_ToothTrtHistory As Patient_ToothTrtHistory) As Boolean
        Dim deleteStatement As String =
            "DELETE FROM [Patient_ToothTrtHistory] 
			WHERE TrtHistID = @TrtHistID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.TrtHistID = clsPatient_ToothTrtHistory.TrtHistID})
            Return affectedRows > 0
        End Using
    End Function


    'Methods to get parents and childs
    Public Function GetPatient_ToothTrt(ByVal TrtID As Integer) As Patient_ToothTrt
        Dim parent As Patient_ToothTrt = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [Patient_ToothTrt] WHERE [TrtID] = @TrtID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of Patient_ToothTrt)(query, New With {.TrtID = TrtID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function

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

    'Public Function GetPatient_ToothTrt(ByVal PatientID As Integer) As Patient_ToothTrt
    '    Dim parent As Patient_ToothTrt = Nothing
    '    Using conn As New SqlConnection(ConnectionString)
    '        Dim query As String = "SELECT * FROM [Patient_ToothTrt] WHERE [PatientID] = @PatientID"
    '        Try
    '            conn.Open()
    '            parent = conn.QuerySingleOrDefault(Of Patient_ToothTrt)(query, New With {.PatientID = PatientID})
    '        Catch ex As Exception
    '            ' Handle exceptions
    '        Finally
    '            If conn.State = ConnectionState.Open Then conn.Close()
    '        End Try
    '    End Using
    '    Return parent
    'End Function

    'Public Function GetPatient_ToothTrt(ByVal ToothNum As Integer) As Patient_ToothTrt
    '    Dim parent As Patient_ToothTrt = Nothing
    '    Using conn As New SqlConnection(ConnectionString)
    '        Dim query As String = "SELECT * FROM [Patient_ToothTrt] WHERE [ToothNum] = @ToothNum"
    '        Try
    '            conn.Open()
    '            parent = conn.QuerySingleOrDefault(Of Patient_ToothTrt)(query, New With {.ToothNum = ToothNum})
    '        Catch ex As Exception
    '            ' Handle exceptions
    '        Finally
    '            If conn.State = ConnectionState.Open Then conn.Close()
    '        End Try
    '    End Using
    '    Return parent
    'End Function

End Class
