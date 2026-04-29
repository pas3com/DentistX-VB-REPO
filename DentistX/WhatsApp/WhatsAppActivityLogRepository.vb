Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text
Imports Dapper

Public Class WhatsAppActivityLogRepository
    Private Shared _schemaChecked As Boolean
    Private Shared ReadOnly SchemaLock As New Object

    Public Shared Sub EnsureSchema()
        SyncLock SchemaLock
            If _schemaChecked Then
                EnsurePatientDisplayNameColumn()
                Return
            End If
            Try
                Dim sql = "IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'WhatsAppMessageLog' AND schema_id = SCHEMA_ID('dbo')) " &
                    "BEGIN " &
                    "CREATE TABLE dbo.WhatsAppMessageLog (" &
                    "LogId BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY, " &
                    "SentAtUtc DATETIME2 NOT NULL CONSTRAINT DF_WhatsAppMessageLog_Sent DEFAULT SYSUTCDATETIME(), " &
                    "Success BIT NOT NULL, " &
                    "Category NVARCHAR(80) NULL, " &
                    "SourceHint NVARCHAR(200) NULL, " &
                    "TargetNumber NVARCHAR(64) NULL, " &
                    "PatientId INT NULL, " &
                    "PatientDisplayName NVARCHAR(200) NULL, " &
                    "MessagePreview NVARCHAR(500) NULL, " &
                    "HasAttachment BIT NOT NULL CONSTRAINT DF_WhatsAppMessageLog_Att DEFAULT 0, " &
                    "ClinicId NVARCHAR(64) NULL, " &
                    "ResponseOrError NVARCHAR(MAX) NULL); " &
                    "CREATE INDEX IX_WhatsAppMessageLog_SentAt ON dbo.WhatsAppMessageLog(SentAtUtc DESC); " &
                    "CREATE INDEX IX_WhatsAppMessageLog_Patient ON dbo.WhatsAppMessageLog(PatientId) WHERE PatientId IS NOT NULL; " &
                    "END"
                Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                    cn.Execute(sql)
                End Using
                EnsurePatientDisplayNameColumn()
                _schemaChecked = True
            Catch
            End Try
        End SyncLock
    End Sub

    ''' <summary>Adds PatientDisplayName when DB was created by an older build without the column.</summary>
    Private Shared Sub EnsurePatientDisplayNameColumn()
        Try
            Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                cn.Execute("IF COL_LENGTH('dbo.WhatsAppMessageLog', 'PatientDisplayName') IS NULL " &
                           "ALTER TABLE dbo.WhatsAppMessageLog ADD PatientDisplayName NVARCHAR(200) NULL")
            End Using
        Catch
        End Try
    End Sub

    Private Shared Function SelectColumnsSql() As String
        Return "l.LogId, l.SentAtUtc, l.Success, l.Category, l.SourceHint, l.TargetNumber, l.PatientId, " &
            "COALESCE(NULLIF(LTRIM(RTRIM(l.PatientDisplayName)), N''), p.PatientName, N'') AS PatientDisplayName, " &
            "l.MessagePreview, l.HasAttachment, l.ClinicId, " &
            "CASE WHEN @EnglishClinic = 1 THEN ISNULL(c.ClinicNameEn, N'') ELSE ISNULL(c.ClinicNameAr, N'') END AS ClinicDisplayName, " &
            "l.ResponseOrError"
    End Function

    Private Shared Function LogJoinSql() As String
        Return "FROM dbo.WhatsAppMessageLog l " &
            "LEFT JOIN Patient p ON p.PatientID = l.PatientId " &
            "LEFT JOIN Clinic c ON c.ClinicID = TRY_CAST(NULLIF(LTRIM(RTRIM(l.ClinicId)), N'') AS UNIQUEIDENTIFIER)"
    End Function

    Private Shared Sub AppendSearchFilter(sb As StringBuilder)
        sb.Append(" AND (@HasSearch = 0 OR (")
        sb.Append(" l.TargetNumber LIKE @SearchLike OR l.MessagePreview LIKE @SearchLike OR l.Category LIKE @SearchLike OR l.SourceHint LIKE @SearchLike OR ")
        sb.Append(" l.PatientDisplayName LIKE @SearchLike OR p.PatientName LIKE @SearchLike OR ")
        sb.Append(" ISNULL(c.ClinicNameEn, N'') LIKE @SearchLike OR ISNULL(c.ClinicNameAr, N'') LIKE @SearchLike OR ")
        sb.Append(" (@SearchInt IS NOT NULL AND l.PatientId = @SearchInt)")
        sb.Append("))")
    End Sub

    ''' <summary>Latest log row for patient + category at/after a UTC time (for linking queue rows after send).</summary>
    Public Shared Function TryGetLatestLogId(patientId As Integer?, category As String, sentAfterUtc As DateTime) As Long
        If Not patientId.HasValue Then Return 0
        Dim cat = If(category, "").Trim()
        If String.IsNullOrEmpty(cat) Then Return 0
        EnsureSchema()
        Try
            Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                Const sql As String = "SELECT TOP 1 LogId FROM dbo.WhatsAppMessageLog WHERE PatientId = @PatientId AND Category = @Category AND SentAtUtc >= @AfterUtc ORDER BY LogId DESC"
                Dim id = cn.ExecuteScalar(Of Long?)(sql, New With {
                    .PatientId = patientId.Value,
                    .Category = cat,
                    .AfterUtc = sentAfterUtc
                })
                Return If(id.HasValue, id.Value, 0L)
            End Using
        Catch
            Return 0
        End Try
    End Function

    Public Shared Function Insert(success As Boolean, category As String, sourceHint As String, targetNumber As String, patientId As Integer?, patientDisplayName As String, messagePreview As String, hasAttachment As Boolean, clinicId As String, responseOrError As String) As Long
        EnsureSchema()
        Dim preview = TruncateStr(messagePreview, 500)
        Dim detail = TruncateStr(responseOrError, 120000)
        Dim patDisp = TruncateStr(If(patientDisplayName, ""), 200)
        If String.IsNullOrWhiteSpace(patDisp) Then patDisp = Nothing
        Try
            Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                Const ins As String = "INSERT INTO dbo.WhatsAppMessageLog (Success, Category, SourceHint, TargetNumber, PatientId, PatientDisplayName, MessagePreview, HasAttachment, ClinicId, ResponseOrError) VALUES (@Success, @Category, @SourceHint, @TargetNumber, @PatientId, @PatientDisplayName, @MessagePreview, @HasAttachment, @ClinicId, @ResponseOrError); SELECT CAST(SCOPE_IDENTITY() AS BIGINT);"
                Return cn.ExecuteScalar(Of Long)(ins, New With {
                    .Success = success,
                    .Category = category,
                    .SourceHint = sourceHint,
                    .TargetNumber = targetNumber,
                    .PatientId = patientId,
                    .PatientDisplayName = patDisp,
                    .MessagePreview = preview,
                    .HasAttachment = hasAttachment,
                    .ClinicId = clinicId,
                    .ResponseOrError = detail
                })
            End Using
        Catch
            Return 0
        End Try
    End Function

    Public Shared Function GetById(logId As Long) As WhatsAppActivityLogRow
        EnsureSchema()
        Try
            Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                Dim sql = "SELECT " & SelectColumnsSql() & " " & LogJoinSql() & " WHERE l.LogId=@Id"
                Dim p As New DynamicParameters()
                p.Add("Id", logId)
                p.Add("EnglishClinic", If(Eng, 1, 0))
                Return cn.QueryFirstOrDefault(Of WhatsAppActivityLogRow)(sql, p)
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    Public Shared Function QueryPage(skip As Integer, take As Integer, failuresOnly As Boolean, search As String) As WhatsAppLogPageResult
        EnsureSchema()
        Dim result As New WhatsAppLogPageResult()
        If take < 1 Then take = 50
        If take > 5000 Then take = 5000
        If skip < 0 Then skip = 0

        Dim searchTrim = If(search, "").Trim()
        Dim hasSearch = If(searchTrim.Length > 0, 1, 0)
        Dim searchLike As String = Nothing
        Dim searchInt As Integer? = Nothing
        If hasSearch = 1 Then
            searchLike = "%" & searchTrim & "%"
            Dim parsed As Integer
            If Integer.TryParse(searchTrim, parsed) Then searchInt = parsed
        End If

        Dim p As New DynamicParameters()
        p.Add("EnglishClinic", If(Eng, 1, 0))
        p.Add("FailuresOnly", If(failuresOnly, 1, 0))
        p.Add("HasSearch", hasSearch)
        p.Add("SearchLike", searchLike)
        p.Add("SearchInt", searchInt)
        p.Add("Skip", skip)
        p.Add("Take", take)

        Try
            Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                Dim countSb As New StringBuilder()
                countSb.Append("SELECT COUNT(1) ")
                countSb.Append(LogJoinSql())
                countSb.Append(" WHERE (@FailuresOnly = 0 OR l.Success = 0)")
                AppendSearchFilter(countSb)
                result.TotalCount = cn.ExecuteScalar(Of Integer)(countSb.ToString(), p)

                Dim dataSb As New StringBuilder()
                dataSb.Append("SELECT ")
                dataSb.Append(SelectColumnsSql())
                dataSb.Append(" ")
                dataSb.Append(LogJoinSql())
                dataSb.Append(" WHERE (@FailuresOnly = 0 OR l.Success = 0)")
                AppendSearchFilter(dataSb)
                dataSb.Append(" ORDER BY l.SentAtUtc DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY")

                Dim rows = cn.Query(Of WhatsAppActivityLogRow)(dataSb.ToString(), p)
                result.Rows = rows.ToList()
            End Using
        Catch
        End Try
        Return result
    End Function

    Public Shared Function GetRecent(Optional take As Integer = 1000) As List(Of WhatsAppActivityLogRow)
        Return QueryPage(0, take, False, Nothing).Rows
    End Function

    ''' <summary>
    ''' Rows with <see cref="WhatsAppActivityLogRow.SentAtUtc"/> falling on the current local calendar day (midnight to midnight).
    ''' </summary>
    Public Shared Function GetRecentForLocalToday(Optional take As Integer = 500) As List(Of WhatsAppActivityLogRow)
        EnsureSchema()
        If take < 1 Then take = 50
        If take > 5000 Then take = 5000
        Dim startLocal = DateTime.Today
        Dim endLocal = startLocal.AddDays(1)
        Dim startUtc = startLocal.ToUniversalTime()
        Dim endUtc = endLocal.ToUniversalTime()
        Dim p As New DynamicParameters()
        p.Add("EnglishClinic", If(Eng, 1, 0))
        p.Add("StartUtc", startUtc)
        p.Add("EndUtc", endUtc)
        p.Add("Take", take)
        Try
            Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                Dim dataSb As New StringBuilder()
                dataSb.Append("SELECT ")
                dataSb.Append(SelectColumnsSql())
                dataSb.Append(" ")
                dataSb.Append(LogJoinSql())
                dataSb.Append(" WHERE l.SentAtUtc >= @StartUtc AND l.SentAtUtc < @EndUtc")
                dataSb.Append(" ORDER BY l.SentAtUtc DESC OFFSET 0 ROWS FETCH NEXT @Take ROWS ONLY")
                Return cn.Query(Of WhatsAppActivityLogRow)(dataSb.ToString(), p).ToList()
            End Using
        Catch
            Return New List(Of WhatsAppActivityLogRow)()
        End Try
    End Function

    Private Shared Function TruncateStr(s As String, maxLen As Integer) As String
        If String.IsNullOrEmpty(s) Then Return ""
        If s.Length <= maxLen Then Return s
        Return s.Substring(0, maxLen)
    End Function
End Class
