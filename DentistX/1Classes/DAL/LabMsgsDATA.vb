Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class LabMsgsDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Function SelectAll() As IEnumerable(Of LabMsgs)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of LabMsgs)("SELECT * FROM [LabMsgs] ORDER BY [MsgDate] DESC, [LabMsgID] DESC")
        End Using
    End Function

    Public Function Select_Record(ByVal clsLabMsgs As LabMsgs) As LabMsgs
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM [LabMsgs] WHERE [LabMsgID] = @LabMsgID"
            Return conn.QuerySingleOrDefault(Of LabMsgs)(sql, New With {.LabMsgID = clsLabMsgs.LabMsgID})
        End Using
    End Function

    Public Function SelectByLabID(ByVal LabID As Integer) As IEnumerable(Of LabMsgs)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM [LabMsgs] WHERE [LabID] = @LabID ORDER BY [MsgDate] DESC, [LabMsgID] DESC"
            Return conn.Query(Of LabMsgs)(sql, New With {.LabID = LabID})
        End Using
    End Function

    Public Function SelectByLabName(ByVal LabName As String) As IEnumerable(Of LabMsgs)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM [LabMsgs] WHERE [LabName] = @LabName ORDER BY [MsgDate] DESC, [LabMsgID] DESC"
            Return conn.Query(Of LabMsgs)(sql, New With {.LabName = LabName})
        End Using
    End Function

    Public Function Add(ByVal clsLabMsgs As LabMsgs) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO [LabMsgs] ([ClinicID], [ClinicName], [LabID], [LabName], [PatientID], [PatientName], [LabMsgSubjectID], [SubjectText], [ReceiveDate], [Note], [MessageBody], [SentDate], [IsSent]) VALUES (@ClinicID, @ClinicName, @LabID, @LabName, @PatientID, @PatientName, @LabMsgSubjectID, @SubjectText, @ReceiveDate, @Note, @MessageBody, @SentDate, @IsSent)"
            RowsAffected = conn.Execute(sql, New With {
                .ClinicID = clsLabMsgs.ClinicID,
                .ClinicName = clsLabMsgs.ClinicName,
                .LabID = clsLabMsgs.LabID,
                .LabName = clsLabMsgs.LabName,
                .PatientID = clsLabMsgs.PatientID,
                .PatientName = clsLabMsgs.PatientName,
                .LabMsgSubjectID = clsLabMsgs.LabMsgSubjectID,
                .SubjectText = clsLabMsgs.SubjectText,
                .ReceiveDate = clsLabMsgs.ReceiveDate,
                .Note = clsLabMsgs.Note,
                .MessageBody = clsLabMsgs.MessageBody,
                .SentDate = clsLabMsgs.SentDate,
                .IsSent = clsLabMsgs.IsSent
            })
            Return RowsAffected > 0
        End Using
    End Function

    Public Function AddAndGetId(ByVal clsLabMsgs As LabMsgs) As Integer
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO [LabMsgs] ([ClinicID], [ClinicName], [LabID], [LabName], [PatientID], [PatientName], [LabMsgSubjectID], [SubjectText], [ReceiveDate], [Note], [MessageBody], [SentDate], [IsSent]) VALUES (@ClinicID, @ClinicName, @LabID, @LabName, @PatientID, @PatientName, @LabMsgSubjectID, @SubjectText, @ReceiveDate, @Note, @MessageBody, @SentDate, @IsSent); SELECT CAST(SCOPE_IDENTITY() AS INT);"
            conn.Open()
            Return conn.ExecuteScalar(Of Integer)(sql, New With {
                .ClinicID = clsLabMsgs.ClinicID,
                .ClinicName = clsLabMsgs.ClinicName,
                .LabID = clsLabMsgs.LabID,
                .LabName = clsLabMsgs.LabName,
                .PatientID = clsLabMsgs.PatientID,
                .PatientName = clsLabMsgs.PatientName,
                .LabMsgSubjectID = clsLabMsgs.LabMsgSubjectID,
                .SubjectText = clsLabMsgs.SubjectText,
                .ReceiveDate = clsLabMsgs.ReceiveDate,
                .Note = clsLabMsgs.Note,
                .MessageBody = clsLabMsgs.MessageBody,
                .SentDate = clsLabMsgs.SentDate,
                .IsSent = clsLabMsgs.IsSent
            })
        End Using
    End Function

    Public Function Update(oldLabMsgs As LabMsgs, newLabMsgs As LabMsgs) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "UPDATE [LabMsgs] SET [ClinicID] = @ClinicID, [ClinicName] = @ClinicName, [LabID] = @LabID, [LabName] = @LabName, [PatientID] = @PatientID, [PatientName] = @PatientName, [LabMsgSubjectID] = @LabMsgSubjectID, [SubjectText] = @SubjectText, [ReceiveDate] = @ReceiveDate, [Note] = @Note, [MessageBody] = @MessageBody, [MsgDate] = @MsgDate, [SentDate] = @SentDate, [IsSent] = @IsSent WHERE [LabMsgID] = @LabMsgID"
            Dim parameters = New With {
                .LabMsgID = oldLabMsgs.LabMsgID,
                .ClinicID = newLabMsgs.ClinicID,
                .ClinicName = newLabMsgs.ClinicName,
                .LabID = newLabMsgs.LabID,
                .LabName = newLabMsgs.LabName,
                .PatientID = newLabMsgs.PatientID,
                .PatientName = newLabMsgs.PatientName,
                .LabMsgSubjectID = newLabMsgs.LabMsgSubjectID,
                .SubjectText = newLabMsgs.SubjectText,
                .ReceiveDate = newLabMsgs.ReceiveDate,
                .Note = newLabMsgs.Note,
                .MessageBody = newLabMsgs.MessageBody,
                .MsgDate = newLabMsgs.MsgDate,
                .SentDate = newLabMsgs.SentDate,
                .IsSent = newLabMsgs.IsSent
            }
            Dim affectedRows As Integer = conn.Execute(sql, parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsLabMsgs As LabMsgs) As Boolean
        Dim deleteStatement As String =
        "DELETE FROM [LabMsgs]
        WHERE [LabMsgID] = @LabMsgID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.LabMsgID = clsLabMsgs.LabMsgID})
            Return affectedRows > 0
        End Using
    End Function

    'Methods to get parents and childs
    Public Function GetLabMsgDetail(ByVal clsLabMsgs As LabMsgs) As IEnumerable(Of LabMsgDetail)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [LabMsgDetail] WHERE [LabMsgID] = @LabMsgID ORDER BY [SortOrder], [LabMsgDetailID]"
            Return conn.Query(Of LabMsgDetail)(query, New With {.LabMsgID = clsLabMsgs.LabMsgID})
        End Using
    End Function

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
