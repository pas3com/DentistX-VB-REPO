Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_TrtScopeDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString




    Public Function GetScopeByTrtID(ByVal trtID As Integer) As IEnumerable(Of Patient_TrtScope)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient_TrtScope WHERE TrtID = @trtID"
            Return conn.Query(Of Patient_TrtScope)(sql, New With {.TrtID = trtID})
        End Using
    End Function


    Public Function SelectAll() As IEnumerable(Of Patient_TrtScope)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_TrtScope)("SELECT * FROM Patient_TrtScope")
        End Using
    End Function


    Public Function Select_Record(ByVal clsPatient_TrtScope As Patient_TrtScope) As Patient_TrtScope
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient_TrtScope WHERE ScopeID = @ScopeID"
            Return conn.QuerySingleOrDefault(Of Patient_TrtScope)(sql, New With {.ScopeID = clsPatient_TrtScope.ScopeID})
        End Using
    End Function

    Public Function Add(ByVal clsPatient_TrtScope As Patient_TrtScope) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Patient_TrtScope (TrtID, MobID, LVL, ToothCode, ToothNum, PropertyName, Treat, FillColor, BorderColor,
                                                                ExternalClinicName, Notes, IsExternal)
                                        VALUES
                    (@TrtID, @MobID, @LVL, @ToothCode, @ToothNum, @PropertyName, @Treat, @FillColor, @BorderColor, @ExternalClinicName, @Notes, @IsExternal)"
            RowsAffected = conn.Execute(sql, New With {.TrtID = clsPatient_TrtScope.TrtID, .MobID = clsPatient_TrtScope.MobID, .LVL = clsPatient_TrtScope.LVL, .ToothCode = clsPatient_TrtScope.ToothCode, .ToothNum = clsPatient_TrtScope.ToothNum, .PropertyName = clsPatient_TrtScope.PropertyName, .Treat = clsPatient_TrtScope.Treat, .FillColor = clsPatient_TrtScope.FillColor, .BorderColor = clsPatient_TrtScope.BorderColor, .ExternalClinicName = clsPatient_TrtScope.ExternalClinicName, .Notes = clsPatient_TrtScope.Notes, .IsExternal = clsPatient_TrtScope.IsExternal})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldPatient_TrtScope As Patient_TrtScope, newPatient_TrtScope As Patient_TrtScope) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                        .NewTrtID = newPatient_TrtScope.TrtID, .OldTrtID = oldPatient_TrtScope.TrtID, .NewMobID = newPatient_TrtScope.MobID, .OldMobID = oldPatient_TrtScope.MobID, .NewLVL = newPatient_TrtScope.LVL, .OldLVL = oldPatient_TrtScope.LVL, .NewToothCode = newPatient_TrtScope.ToothCode, .OldToothCode = oldPatient_TrtScope.ToothCode, .NewToothNum = newPatient_TrtScope.ToothNum, .OldToothNum = oldPatient_TrtScope.ToothNum, .NewPropertyName = newPatient_TrtScope.PropertyName, .OldPropertyName = oldPatient_TrtScope.PropertyName, .NewTreat = newPatient_TrtScope.Treat, .OldTreat = oldPatient_TrtScope.Treat, .NewFillColor = newPatient_TrtScope.FillColor, .OldFillColor = oldPatient_TrtScope.FillColor, .NewBorderColor = newPatient_TrtScope.BorderColor, .OldBorderColor = oldPatient_TrtScope.BorderColor, .NewExternalClinicName = newPatient_TrtScope.ExternalClinicName, .OldExternalClinicName = oldPatient_TrtScope.ExternalClinicName, .NewNotes = newPatient_TrtScope.Notes, .OldNotes = oldPatient_TrtScope.Notes, .NewIsExternal = newPatient_TrtScope.IsExternal, .OldIsExternal = oldPatient_TrtScope.IsExternal
                                              }
            Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_TrtScope] SET [TrtID] = @NewTrtID, [MobID] = @NewMobID, [LVL] = @NewLVL, [ToothCode] = @NewToothCode, [ToothNum] = @NewToothNum, [PropertyName] = @NewPropertyName, [Treat] = @NewTreat, [FillColor] = @NewFillColor, [BorderColor] = @NewBorderColor, [ExternalClinicName] = @NewExternalClinicName, [Notes] = @NewNotes, [IsExternal] = @NewIsExternal WHERE [TrtID] = @OldTrtID AND [MobID] = @OldMobID AND [LVL] = @OldLVL AND [ToothCode] = @OldToothCode AND [ToothNum] = @OldToothNum AND [PropertyName] = @OldPropertyName AND [Treat] = @OldTreat AND [FillColor] = @OldFillColor AND [BorderColor] = @OldBorderColor AND [ExternalClinicName] = @OldExternalClinicName AND [Notes] = @OldNotes AND [IsExternal] = @OldIsExternal", parameters)
            Return affectedRows > 0
        End Using
    End Function


    Public Function Delete(ByVal clsPatient_TrtScope As Patient_TrtScope) As Boolean
        Dim deleteStatement As String =
                "DELETE FROM [Patient_TrtScope] 
			    WHERE ScopeID = @ScopeID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.ScopeID = clsPatient_TrtScope.ScopeID})
            Return affectedRows > 0
        End Using
    End Function




    'Methods to get parents and childs
    Public Function GetPatient_Trts(ByVal TrtID As Integer) As Patient_Trts
        Dim parent As Patient_Trts = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [Patient_Trts] WHERE [TrtID] = @TrtID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of Patient_Trts)(query, New With {.TrtID = TrtID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function

    Public Function GetPatient_Mobile(ByVal MobID As Integer) As Patient_Mobile
        Dim parent As Patient_Mobile = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [Patient_Mobile] WHERE [MobID] = @MobID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of Patient_Mobile)(query, New With {.MobID = MobID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function
End Class
