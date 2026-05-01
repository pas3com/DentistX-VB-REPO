Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class LabMsgDetailDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Function SelectAll() As IEnumerable(Of LabMsgDetail)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of LabMsgDetail)("SELECT * FROM [LabMsgDetail] ORDER BY [LabMsgID], [SortOrder], [LabMsgDetailID]")
        End Using
    End Function

    Public Function Select_Record(ByVal clsLabMsgDetail As LabMsgDetail) As LabMsgDetail
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM [LabMsgDetail] WHERE [LabMsgDetailID] = @LabMsgDetailID"
            Return conn.QuerySingleOrDefault(Of LabMsgDetail)(sql, New With {.LabMsgDetailID = clsLabMsgDetail.LabMsgDetailID})
        End Using
    End Function

    Public Function SelectByLabMsgID(ByVal LabMsgID As Integer) As IEnumerable(Of LabMsgDetail)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM [LabMsgDetail] WHERE [LabMsgID] = @LabMsgID ORDER BY [SortOrder], [LabMsgDetailID]"
            Return conn.Query(Of LabMsgDetail)(sql, New With {.LabMsgID = LabMsgID})
        End Using
    End Function

    Public Function Add(ByVal clsLabMsgDetail As LabMsgDetail) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO [LabMsgDetail] ([LabMsgID], [LabMsgDetailChoiceID], [DetailText], [SortOrder]) VALUES (@LabMsgID, @LabMsgDetailChoiceID, @DetailText, @SortOrder)"
            RowsAffected = conn.Execute(sql, New With {
                .LabMsgID = clsLabMsgDetail.LabMsgID,
                .LabMsgDetailChoiceID = clsLabMsgDetail.LabMsgDetailChoiceID,
                .DetailText = clsLabMsgDetail.DetailText,
                .SortOrder = clsLabMsgDetail.SortOrder
            })
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldLabMsgDetail As LabMsgDetail, newLabMsgDetail As LabMsgDetail) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "UPDATE [LabMsgDetail] SET [LabMsgID] = @LabMsgID, [LabMsgDetailChoiceID] = @LabMsgDetailChoiceID, [DetailText] = @DetailText, [SortOrder] = @SortOrder WHERE [LabMsgDetailID] = @LabMsgDetailID"
            Dim parameters = New With {
                .LabMsgDetailID = oldLabMsgDetail.LabMsgDetailID,
                .LabMsgID = newLabMsgDetail.LabMsgID,
                .LabMsgDetailChoiceID = newLabMsgDetail.LabMsgDetailChoiceID,
                .DetailText = newLabMsgDetail.DetailText,
                .SortOrder = newLabMsgDetail.SortOrder
            }
            Dim affectedRows As Integer = conn.Execute(sql, parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsLabMsgDetail As LabMsgDetail) As Boolean
        Dim deleteStatement As String =
        "DELETE FROM [LabMsgDetail]
        WHERE [LabMsgDetailID] = @LabMsgDetailID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.LabMsgDetailID = clsLabMsgDetail.LabMsgDetailID})
            Return affectedRows > 0
        End Using
    End Function

    'Methods to get parents and childs
    Public Function GetLabMsgs(ByVal LabMsgID As Integer) As LabMsgs
        Dim parent As LabMsgs = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [LabMsgs] WHERE [LabMsgID] = @LabMsgID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of LabMsgs)(query, New With {.LabMsgID = LabMsgID})
            Catch ex As Exception
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function

    Public Function GetLabMsgDetailChoice(ByVal LabMsgDetailChoiceID As Integer) As LabMsgDetailChoice
        Dim parent As LabMsgDetailChoice = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [LabMsgDetailChoice] WHERE [LabMsgDetailChoiceID] = @LabMsgDetailChoiceID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of LabMsgDetailChoice)(query, New With {.LabMsgDetailChoiceID = LabMsgDetailChoiceID})
            Catch ex As Exception
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function

End Class
