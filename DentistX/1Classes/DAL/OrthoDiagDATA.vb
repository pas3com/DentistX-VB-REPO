Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class OrthoDiagDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of OrthoDiag)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of OrthoDiag)("SELECT * FROM OrthoDiag")
        End Using
    End Function


    Public Function Select_Record(ByVal clsOrthoDiag As OrthoDiag) As OrthoDiag
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM OrthoDiag WHERE OrthoID = @OrthoID AND DiagID = @DiagID And PatientID = @PatientID"
            Return conn.QuerySingleOrDefault(Of OrthoDiag)(sql, New With {.OrthoID = clsOrthoDiag.OrthoID, .DiagID = clsOrthoDiag.DiagID, .PatientID = clsOrthoDiag.PatientID})
        End Using
    End Function

    ''' <summary>Gets all OrthoDiag records for a patient.</summary>
    Public Function SelectByPatientID(patientID As Integer) As IEnumerable(Of OrthoDiag)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim sql As String = "SELECT * FROM OrthoDiag WHERE PatientID = @PatientID ORDER BY DiagID"
            Return conn.Query(Of OrthoDiag)(sql, New With {.PatientID = patientID})
        End Using
    End Function

    Public Function Add(ByVal clsOrthoDiag As OrthoDiag) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO OrthoDiag (OrthoID, PatientID, CloseType, ClassI, Bite) VALUES (@OrthoID, @PatientID, @CloseType, @ClassI, @Bite)"
            RowsAffected = conn.Execute(sql, New With {.OrthoID = clsOrthoDiag.OrthoID, .PatientID = clsOrthoDiag.PatientID, .CloseType = clsOrthoDiag.CloseType, .ClassI = clsOrthoDiag.ClassI, .Bite = clsOrthoDiag.Bite})
            Return RowsAffected > 0
        End Using
    End Function
    Public Function AddTrans(conn As SqlConnection, trans As SqlTransaction, ByVal clsOrthoDiag As OrthoDiag) As Boolean
        Dim RowsAffected As Integer = 0
        'Using conn As New SqlConnection(ConnectionString)
        Dim sql As String = "INSERT INTO OrthoDiag (OrthoID, PatientID, CloseType, ClassI, Bite) VALUES (@OrthoID, @PatientID, @CloseType, @ClassI, @Bite)"
        RowsAffected = conn.Execute(sql, New With {.OrthoID = clsOrthoDiag.OrthoID, .PatientID = clsOrthoDiag.PatientID, .CloseType = clsOrthoDiag.CloseType, .ClassI = clsOrthoDiag.ClassI, .Bite = clsOrthoDiag.Bite}, trans)
        Return RowsAffected > 0
        'End Using
    End Function
    Public Function Update(oldOrthoDiag As OrthoDiag, newOrthoDiag As OrthoDiag) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                .NewOrthoID = newOrthoDiag.OrthoID, .OldOrthoID = oldOrthoDiag.OrthoID, .OldDiagID = oldOrthoDiag.DiagID, .NewPatientID = newOrthoDiag.PatientID, .OldPatientID = oldOrthoDiag.PatientID, .NewCloseType = newOrthoDiag.CloseType, .OldCloseType = oldOrthoDiag.CloseType, .NewClassI = newOrthoDiag.ClassI, .OldClassI = oldOrthoDiag.ClassI, .NewBite = newOrthoDiag.Bite, .OldBite = oldOrthoDiag.Bite
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [OrthoDiag] SET [OrthoID] = @NewOrthoID, [PatientID] = @NewPatientID, [CloseType] = @NewCloseType, [ClassI] = @NewClassI, [Bite] = @NewBite WHERE [OrthoID] = @OldOrthoID AND [DiagID] = @OldDiagID AND [PatientID] = @OldPatientID", parameters)
            Return affectedRows > 0
        End Using
    End Function
    Public Function UpdateOrtho(ortho As Boolean, patientID As Integer) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim updateStatement As String = "UPDATE [Patient] SET  [Ortho] = @ortho  WHERE [PatientID] = @PatientID"
            Dim affectedRows As Integer = conn.Execute(updateStatement, New With {.PatientID = patientID, .Ortho = ortho})
            Return affectedRows > 0
        End Using
    End Function

    ''' <summary>UpdateOrtho within a transaction.</summary>
    Public Function UpdateOrthoTrans(conn As SqlConnection, trans As SqlTransaction, ortho As Boolean, patientID As Integer) As Boolean
        Dim updateStatement As String = "UPDATE [Patient] SET [Ortho] = @ortho WHERE [PatientID] = @PatientID"
        Dim affectedRows As Integer = conn.Execute(updateStatement, New With {.PatientID = patientID, .Ortho = ortho}, trans)
        Return affectedRows > 0
    End Function

    ''' <summary>Deletes OrthoDiag and cascades to OrthoTrtDet and OrthoTreat (to satisfy FK constraints).</summary>
    Public Function Delete(ByVal clsOrthoDiag As OrthoDiag) As Boolean
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Using transaction = connection.BeginTransaction()
                Try
                    Dim diagParams = New With {.OrthoID = clsOrthoDiag.OrthoID, .DiagID = clsOrthoDiag.DiagID, .PatientID = clsOrthoDiag.PatientID}
                    ' Delete children first (OrthoTrtDet, then OrthoTreat) to satisfy FK_OrthoTreat_OrthoDiag and any OrthoTrtDet FK
                    connection.Execute("DELETE FROM [OrthoTrtDet] WHERE PatientID = @PatientID AND DiagID = @DiagID", diagParams, transaction)
                    connection.Execute("DELETE FROM [OrthoTreat] WHERE PatientID = @PatientID AND DiagID = @DiagID", diagParams, transaction)
                    Dim affectedRows As Integer = connection.Execute("DELETE FROM [OrthoDiag] WHERE OrthoID = @OrthoID AND DiagID = @DiagID AND PatientID = @PatientID", diagParams, transaction)
                    transaction.Commit()
                    Return affectedRows > 0
                Catch ex As Exception
                    transaction.Rollback()
                    Throw
                End Try
            End Using
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
