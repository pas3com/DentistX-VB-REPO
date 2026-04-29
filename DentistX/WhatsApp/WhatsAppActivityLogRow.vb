Imports System.Globalization
Imports System.Text.Json

Public Class WhatsAppActivityLogRow

    Public Property LogId As Long
    Public Property SentAtUtc As DateTime
    Public Property Success As Boolean
    Public Property Category As String
    Public Property SourceHint As String
    Public Property TargetNumber As String
    Public Property PatientId As Integer?
    ''' <summary>Denormalized name from send context; grid also merges Patient.PatientName in queries.</summary>
    Public Property PatientDisplayName As String
    Public Property MessagePreview As String
    ''' <summary>Full message text (in-memory only, not persisted to SQL). Falls back to <see cref="MessagePreview"/> if not set.</summary>
    Public Property FullMessage As String
    Public Property HasAttachment As Boolean
    Public Property ClinicId As String
    ''' <summary>Resolved from Clinic table by ClinicId GUID (UI language).</summary>
    Public Property ClinicDisplayName As String
    Public Property ResponseOrError As String

    ''' <summary>True when the send was triggered after processing a local reminder queue row (session UI); not persisted in SQL yet.</summary>
    Public Property SentAfterLocalQueue As Boolean

    ''' <summary>Gateway queue job id from the send API response (in-memory / parsed from <see cref="ResponseOrError"/>).</summary>
    Public Property QueueJobId As String
    ''' <summary>When the gateway plans to send (UTC), from API JSON (optional; message-center strip uses <see cref="QueueDelaySeconds"/> only).</summary>
    Public Property ScheduledSendUtc As DateTime?

    ''' <summary>Last <c>delaySeconds</c> from GET /queue for this row (remaining seconds at <see cref="QueueDelayBaselineUtc"/>).</summary>
    Public Property QueueDelaySeconds As Integer?

    ''' <summary>UTC moment when <see cref="QueueDelaySeconds"/> was last taken from GET /queue; drives smooth local countdown between polls.</summary>
    Public Property QueueDelayBaselineUtc As DateTime?

    ''' <summary>True only after a successful GET /queue matched this row to a live item with a numeric <c>delaySeconds</c>. Never trust the send-response job id alone for UI.</summary>
    Public Property ListedInLiveGatewayQueue As Boolean

    ''' <summary>Parse scheduled time strings from GET /queue or similar (ISO, local, unix seconds/ms as string).</summary>
    Public Shared Function TryParseGatewayScheduledString(scheduledRaw As String) As DateTime?
        If String.IsNullOrWhiteSpace(scheduledRaw) Then Return Nothing
        Dim s = scheduledRaw.Trim()
        Dim dt As DateTime
        ' ISO-8601 without Z/offset (common from .NET Json): treat as UTC so countdown matches gateway delay.
        If s.Length >= 19 AndAlso s(10) = "T"c AndAlso Not s.Contains("+"c) AndAlso Not s.EndsWith("Z"c, StringComparison.OrdinalIgnoreCase) Then
            If DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal Or DateTimeStyles.AdjustToUniversal, dt) Then
                Return DateTime.SpecifyKind(dt, DateTimeKind.Utc)
            End If
        End If
        If DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, dt) Then Return DateTime.SpecifyKind(dt.ToUniversalTime(), DateTimeKind.Utc)
        If DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal Or DateTimeStyles.AdjustToUniversal, dt) Then Return DateTime.SpecifyKind(dt.ToUniversalTime(), DateTimeKind.Utc)
        If DateTime.TryParse(s, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, dt) Then Return dt.ToUniversalTime()
        Dim unix As Long
        If Long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, unix) Then
            Try
                If unix > 100000000000L Then Return DateTimeOffset.FromUnixTimeMilliseconds(unix).UtcDateTime
                Return DateTimeOffset.FromUnixTimeSeconds(unix).UtcDateTime
            Catch
            End Try
        End If
        Return Nothing
    End Function

    ''' <summary>Normalize to UTC for comparisons (SQL/Dapper often returns <see cref="DateTimeKind.Unspecified"/>).</summary>
    Public Shared Function ToUtcAssumeStoredAsUtc(t As DateTime) As DateTime
        If t.Kind = DateTimeKind.Utc Then Return t
        If t.Kind = DateTimeKind.Local Then Return t.ToUniversalTime()
        Return DateTime.SpecifyKind(t, DateTimeKind.Utc)
    End Function

    ''' <summary>Seconds remaining until send from live queue snapshot: <see cref="QueueDelaySeconds"/> minus elapsed since <see cref="QueueDelayBaselineUtc"/>.</summary>
    Public Shared Function GetQueueCountdownRemainingSeconds(row As WhatsAppActivityLogRow, nowUtc As DateTime) As Integer
        If row Is Nothing OrElse Not row.ListedInLiveGatewayQueue OrElse Not row.QueueDelayBaselineUtc.HasValue OrElse Not row.QueueDelaySeconds.HasValue Then Return 0
        Dim elapsed = (nowUtc - ToUtcAssumeStoredAsUtc(row.QueueDelayBaselineUtc.Value)).TotalSeconds
        Dim remi = CInt(Math.Ceiling(row.QueueDelaySeconds.Value - elapsed))
        If remi < 0 Then Return 0
        Return remi
    End Function

    ''' <summary>Extract job id and schedule from logged API response JSON (same shapes as GET queue).</summary>
    Public Shared Sub TryApplyQueueMetadataFromResponse(row As WhatsAppActivityLogRow)
        If row Is Nothing Then Return
        Dim raw0 = If(row.ResponseOrError, "").Trim()
        If raw0.Length > 0 AndAlso raw0(0) = ChrW(&HFEFF) Then raw0 = raw0.Substring(1).Trim()
        Dim start = raw0.IndexOf("{"c)
        If start < 0 Then Return
        Dim raw = raw0.Substring(start)
        row.QueueJobId = Nothing
        row.ScheduledSendUtc = Nothing
        row.QueueDelaySeconds = Nothing
        row.QueueDelayBaselineUtc = Nothing
        row.ListedInLiveGatewayQueue = False
        Try
            Using doc = JsonDocument.Parse(raw)
                Dim root = doc.RootElement
                ExtractQueueFieldsFromObject(row, root, True, True)
                Dim nestedNames = {"data", "result", "payload", "value", "job", "queueItem", "item", "message"}
                For Each nm In nestedNames
                    Dim el As JsonElement
                    If root.TryGetProperty(nm, el) AndAlso el.ValueKind = JsonValueKind.Object Then
                        ExtractQueueFieldsFromObject(row, el, String.IsNullOrWhiteSpace(row.QueueJobId), Not row.ScheduledSendUtc.HasValue)
                    End If
                Next
            End Using
        Catch
            row.QueueJobId = Nothing
            row.ScheduledSendUtc = Nothing
            row.QueueDelaySeconds = Nothing
            row.QueueDelayBaselineUtc = Nothing
            row.ListedInLiveGatewayQueue = False
        End Try
    End Sub

    Private Shared Sub ExtractQueueFieldsFromObject(row As WhatsAppActivityLogRow, o As JsonElement, fillJobIfEmpty As Boolean, fillSchedIfEmpty As Boolean)
        Dim el As JsonElement
        If fillJobIfEmpty AndAlso String.IsNullOrWhiteSpace(row.QueueJobId) Then
            Dim jobKeys = {"jobId", "messageId", "queueJobId", "pendingJobId", "pendingMessageId"}
            For Each key In jobKeys
                If o.TryGetProperty(key, el) AndAlso el.ValueKind <> JsonValueKind.Null AndAlso el.ValueKind <> JsonValueKind.Undefined Then
                    If el.ValueKind = JsonValueKind.String Then
                        Dim v = el.GetString()
                        If Not String.IsNullOrWhiteSpace(v) Then row.QueueJobId = v.Trim() : Exit For
                    ElseIf el.ValueKind = JsonValueKind.Number Then
                        row.QueueJobId = el.GetRawText().Trim() : Exit For
                    End If
                End If
            Next
            If String.IsNullOrWhiteSpace(row.QueueJobId) AndAlso o.TryGetProperty("id", el) AndAlso el.ValueKind <> JsonValueKind.Null AndAlso el.ValueKind <> JsonValueKind.Undefined Then
                If el.ValueKind = JsonValueKind.String AndAlso Not String.IsNullOrWhiteSpace(el.GetString()) Then
                    row.QueueJobId = el.GetString().Trim()
                ElseIf el.ValueKind = JsonValueKind.Number Then
                    row.QueueJobId = el.GetRawText().Trim()
                End If
            End If
            If String.IsNullOrWhiteSpace(row.QueueJobId) Then row.QueueJobId = Nothing
        End If
        If fillSchedIfEmpty AndAlso Not row.ScheduledSendUtc.HasValue Then
            row.ScheduledSendUtc = TryParseScheduledFromJson(o, row.SentAtUtc)
        End If
    End Sub

    Private Shared Function TryParseScheduledFromJson(root As JsonElement, sentAtUtc As DateTime) As DateTime?
        Dim el As JsonElement
        If root.TryGetProperty("delaySeconds", el) AndAlso el.ValueKind = JsonValueKind.Number Then
            Dim ds = el.GetDouble()
            If ds > 0R Then Return DateTime.UtcNow.AddSeconds(ds)
        End If
        Dim schedKeys = {"scheduledAt", "scheduledSendAt", "sendAt", "scheduledTime", "scheduledFor", "runAt", "executeAt", "fireAt"}
        For Each key In schedKeys
            If root.TryGetProperty(key, el) Then
                Dim d = ParseDateElement(el)
                If d.HasValue Then Return d
                If el.ValueKind = JsonValueKind.String Then
                    Dim p = TryParseGatewayScheduledString(el.GetString())
                    If p.HasValue Then Return p
                End If
            End If
        Next
        If root.TryGetProperty("etaSeconds", el) AndAlso el.ValueKind = JsonValueKind.Number Then
            Return sentAtUtc.AddSeconds(el.GetDouble())
        End If
        If root.TryGetProperty("delaySeconds", el) AndAlso el.ValueKind = JsonValueKind.Number Then
            Return sentAtUtc.AddSeconds(el.GetDouble())
        End If
        Return Nothing
    End Function

    Private Shared Function ParseDateElement(el As JsonElement) As DateTime?
        If el.ValueKind = JsonValueKind.String Then
            Dim s = el.GetString()
            If String.IsNullOrWhiteSpace(s) Then Return Nothing
            Dim dt As DateTime
            If DateTime.TryParse(s,
                                 CultureInfo.InvariantCulture,
                                 DateTimeStyles.AssumeUniversal Or DateTimeStyles.AdjustToUniversal,
                                 dt) Then
                Return dt.ToUniversalTime()
            End If
            If DateTime.TryParse(s,
                                 CultureInfo.CurrentCulture,
                                 DateTimeStyles.AssumeLocal,
                                 dt) Then
                Return dt.ToUniversalTime()
            End If
            Return Nothing
        End If
        If el.ValueKind = JsonValueKind.Number Then
            Try
                Dim unix = el.GetDouble()
                If unix > 1.0E+12 Then
                    Return DateTimeOffset.FromUnixTimeMilliseconds(CLng(unix)).UtcDateTime
                End If
                Return DateTimeOffset.FromUnixTimeSeconds(CLng(unix)).UtcDateTime
            Catch
                Return Nothing
            End Try
        End If
        Return Nothing
    End Function
End Class
