Imports System.Data.SqlClient
Imports Dapper

Public Class AuditLogEntryDATA
    Private ReadOnly ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Function SelectAll() As List(Of AuditLogEntry)
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM AuditLogEntry"
            Return conn.Query(Of AuditLogEntry)(query).ToList()
        End Using
    End Function

    Public Function SelectRecord(auditId As Integer) As AuditLogEntry
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM AuditLogEntry WHERE AuditID = @AuditID"
            Return conn.QueryFirstOrDefault(Of AuditLogEntry)(query, New With {.AuditID = auditId})
        End Using
    End Function

    Public Function Add(entry As AuditLogEntry) As Integer
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "
                INSERT INTO AuditLogEntry (ActionType, TableName, RecordID, OldValue, NewValue, ChangedBy, ChangeDate)
                VALUES (@ActionType, @TableName, @RecordID, @OldValue, @NewValue, @ChangedBy, @ChangeDate);
                SELECT CAST(SCOPE_IDENTITY() AS INT);"
            Return conn.ExecuteScalar(Of Integer)(query, entry)
        End Using
    End Function

    Public Function Update(entry As AuditLogEntry) As Integer
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "
                UPDATE AuditLogEntry 
                SET ActionType = @ActionType,
                    TableName = @TableName,
                    RecordID = @RecordID,
                    OldValue = @OldValue,
                    NewValue = @NewValue,
                    ChangedBy = @ChangedBy,
                    ChangeDate = @ChangeDate
                WHERE AuditID = @AuditID"
            Return conn.Execute(query, entry)
        End Using
    End Function

    Public Function Delete(auditId As Integer) As Integer
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "DELETE FROM AuditLogEntry WHERE AuditID = @AuditID"
            Return conn.Execute(query, New With {.AuditID = auditId})
        End Using
    End Function

    Public Function DeleteAll() As Integer
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "DELETE FROM AuditLogEntry "
            Return conn.Execute(query)
        End Using
    End Function
End Class
