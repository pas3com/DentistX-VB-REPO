Public Class AuditLogEntry
    Public Property AuditID As Integer
    Public Property ActionType As String
    Public Property TableName As String
    Public Property RecordID As Integer
    Public Property OldValue As String
    Public Property NewValue As String
    Public Property ChangedBy As String
    Public Property ChangeDate As DateTime
End Class
