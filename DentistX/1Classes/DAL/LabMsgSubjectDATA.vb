Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class LabMsgSubjectDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Function SelectAll() As IEnumerable(Of LabMsgSubject)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of LabMsgSubject)("SELECT * FROM [LabMsgSubject] ORDER BY [SortOrder], [LabMsgSubjectID]")
        End Using
    End Function

    Public Function SelectActive() As IEnumerable(Of LabMsgSubject)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of LabMsgSubject)("SELECT * FROM [LabMsgSubject] WHERE [IsActive] = 1 ORDER BY [SortOrder], [LabMsgSubjectID]")
        End Using
    End Function

    Public Function Select_Record(ByVal clsLabMsgSubject As LabMsgSubject) As LabMsgSubject
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM [LabMsgSubject] WHERE [LabMsgSubjectID] = @LabMsgSubjectID"
            Return conn.QuerySingleOrDefault(Of LabMsgSubject)(sql, New With {.LabMsgSubjectID = clsLabMsgSubject.LabMsgSubjectID})
        End Using
    End Function
    Public Function Select_Record(ByVal LabMsgSubjectID As Integer) As LabMsgSubject
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM [LabMsgSubject] WHERE [LabMsgSubjectID] = @LabMsgSubjectID"
            Return conn.QuerySingleOrDefault(Of LabMsgSubject)(sql, New With {.LabMsgSubjectID = LabMsgSubjectID})
        End Using
    End Function
    Public Function Add(ByVal clsLabMsgSubject As LabMsgSubject) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO [LabMsgSubject] ([SubjectName], [SortOrder], [IsActive]) VALUES (@SubjectName, @SortOrder, @IsActive)"
            RowsAffected = conn.Execute(sql, New With {
                .SubjectName = clsLabMsgSubject.SubjectName,
                .SortOrder = clsLabMsgSubject.SortOrder,
                .IsActive = clsLabMsgSubject.IsActive
            })
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldLabMsgSubject As LabMsgSubject, newLabMsgSubject As LabMsgSubject) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "UPDATE [LabMsgSubject] SET [SubjectName] = @SubjectName, [SortOrder] = @SortOrder, [IsActive] = @IsActive WHERE [LabMsgSubjectID] = @LabMsgSubjectID"
            Dim parameters = New With {
                .LabMsgSubjectID = oldLabMsgSubject.LabMsgSubjectID,
                .SubjectName = newLabMsgSubject.SubjectName,
                .SortOrder = newLabMsgSubject.SortOrder,
                .IsActive = newLabMsgSubject.IsActive
            }
            Dim affectedRows As Integer = conn.Execute(sql, parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsLabMsgSubject As LabMsgSubject) As Boolean
        Dim deleteStatement As String =
        "DELETE FROM [LabMsgSubject]
        WHERE [LabMsgSubjectID] = @LabMsgSubjectID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.LabMsgSubjectID = clsLabMsgSubject.LabMsgSubjectID})
            Return affectedRows > 0
        End Using
    End Function

    'Methods to get parents and childs
    Public Function GetLabMsgDetailChoice(ByVal clsLabMsgSubject As LabMsgSubject) As IEnumerable(Of LabMsgDetailChoice)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [LabMsgDetailChoice] WHERE [LabMsgSubjectID] = @LabMsgSubjectID ORDER BY [SortOrder], [LabMsgDetailChoiceID]"
            Return conn.Query(Of LabMsgDetailChoice)(query, New With {.LabMsgSubjectID = clsLabMsgSubject.LabMsgSubjectID})
        End Using
    End Function

End Class
