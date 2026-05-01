Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class LabMsgDetailChoiceDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Function SelectAll() As IEnumerable(Of LabMsgDetailChoice)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of LabMsgDetailChoice)("SELECT * FROM [LabMsgDetailChoice] ORDER BY [LabMsgSubjectID], [SortOrder], [LabMsgDetailChoiceID]")
        End Using
    End Function

    Public Function SelectActive() As IEnumerable(Of LabMsgDetailChoice)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of LabMsgDetailChoice)("SELECT * FROM [LabMsgDetailChoice] WHERE [IsActive] = 1 ORDER BY [LabMsgSubjectID], [SortOrder], [LabMsgDetailChoiceID]")
        End Using
    End Function

    Public Function Select_Record(ByVal LabMsgDetailChoiceID As Integer) As LabMsgDetailChoice
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM [LabMsgDetailChoice] WHERE [LabMsgDetailChoiceID] = @LabMsgDetailChoiceID"
            Return conn.QuerySingleOrDefault(Of LabMsgDetailChoice)(sql, New With {.LabMsgDetailChoiceID = LabMsgDetailChoiceID})
        End Using
    End Function
    Public Function Select_Record(ByVal clsLabMsgDetailChoice As LabMsgDetailChoice) As LabMsgDetailChoice
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM [LabMsgDetailChoice] WHERE [LabMsgDetailChoiceID] = @LabMsgDetailChoiceID"
            Return conn.QuerySingleOrDefault(Of LabMsgDetailChoice)(sql, New With {.LabMsgDetailChoiceID = clsLabMsgDetailChoice.LabMsgDetailChoiceID})
        End Using
    End Function
    Public Function SelectBySubject(ByVal LabMsgSubjectID As Integer) As IEnumerable(Of LabMsgDetailChoice)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM [LabMsgDetailChoice] WHERE [LabMsgSubjectID] = @LabMsgSubjectID ORDER BY [SortOrder], [LabMsgDetailChoiceID]"
            Return conn.Query(Of LabMsgDetailChoice)(sql, New With {.LabMsgSubjectID = LabMsgSubjectID})
        End Using
    End Function

    Public Function SelectActiveBySubject(ByVal LabMsgSubjectID As Integer) As IEnumerable(Of LabMsgDetailChoice)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM [LabMsgDetailChoice] WHERE [LabMsgSubjectID] = @LabMsgSubjectID AND [IsActive] = 1 ORDER BY [SortOrder], [LabMsgDetailChoiceID]"
            Return conn.Query(Of LabMsgDetailChoice)(sql, New With {.LabMsgSubjectID = LabMsgSubjectID})
        End Using
    End Function

    Public Function Add(ByVal clsLabMsgDetailChoice As LabMsgDetailChoice) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO [LabMsgDetailChoice] ([LabMsgSubjectID], [DetailText], [SortOrder], [IsActive]) VALUES (@LabMsgSubjectID, @DetailText, @SortOrder, @IsActive)"
            RowsAffected = conn.Execute(sql, New With {
                .LabMsgSubjectID = clsLabMsgDetailChoice.LabMsgSubjectID,
                .DetailText = clsLabMsgDetailChoice.DetailText,
                .SortOrder = clsLabMsgDetailChoice.SortOrder,
                .IsActive = clsLabMsgDetailChoice.IsActive
            })
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldLabMsgDetailChoice As LabMsgDetailChoice, newLabMsgDetailChoice As LabMsgDetailChoice) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "UPDATE [LabMsgDetailChoice] SET [LabMsgSubjectID] = @LabMsgSubjectID, [DetailText] = @DetailText, [SortOrder] = @SortOrder, [IsActive] = @IsActive WHERE [LabMsgDetailChoiceID] = @LabMsgDetailChoiceID"
            Dim parameters = New With {
                .LabMsgDetailChoiceID = oldLabMsgDetailChoice.LabMsgDetailChoiceID,
                .LabMsgSubjectID = newLabMsgDetailChoice.LabMsgSubjectID,
                .DetailText = newLabMsgDetailChoice.DetailText,
                .SortOrder = newLabMsgDetailChoice.SortOrder,
                .IsActive = newLabMsgDetailChoice.IsActive
            }
            Dim affectedRows As Integer = conn.Execute(sql, parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsLabMsgDetailChoice As LabMsgDetailChoice) As Boolean
        Dim deleteStatement As String =
        "DELETE FROM [LabMsgDetailChoice]
        WHERE [LabMsgDetailChoiceID] = @LabMsgDetailChoiceID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.LabMsgDetailChoiceID = clsLabMsgDetailChoice.LabMsgDetailChoiceID})
            Return affectedRows > 0
        End Using
    End Function

    'Methods to get parents and childs
    Public Function GetLabMsgSubject(ByVal LabMsgSubjectID As Integer) As LabMsgSubject
        Dim parent As LabMsgSubject = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [LabMsgSubject] WHERE [LabMsgSubjectID] = @LabMsgSubjectID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of LabMsgSubject)(query, New With {.LabMsgSubjectID = LabMsgSubjectID})
            Catch ex As Exception
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function

End Class
