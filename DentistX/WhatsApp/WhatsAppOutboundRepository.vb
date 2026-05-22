Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports Dapper

Public NotInheritable Class WhatsAppOutboundRepository
    Private Sub New()
    End Sub

    Private Shared Function Cn() As SqlConnection
        Return New SqlConnection(DentistXDATA.GetEffectiveConnectionString())
    End Function

    ''' <summary>Unspecified <see cref="DateTime"/> values are treated as local clinic workstation time (same convention as appointment reminders).</summary>
    Public Shared Function NormalizeLocalToUtc(dt As DateTime) As DateTime
        Select Case dt.Kind
            Case DateTimeKind.Utc
                Return dt
            Case DateTimeKind.Local
                Return dt.ToUniversalTime()
            Case Else
                Return DateTime.SpecifyKind(dt, DateTimeKind.Local).ToUniversalTime()
        End Select
    End Function

    ''' <summary>Backoff after the given completed-attempt count (post-increment target).</summary>
    Public Shared Function ComputeBackoffSeconds(completedAttemptsAfterThisFailure As Integer) As Integer
        Dim a = Math.Max(1, completedAttemptsAfterThisFailure)
        If a <= 1 Then Return 45
        If a = 2 Then Return 90
        If a = 3 Then Return 180
        If a = 4 Then Return 300
        Return Math.Min(900, 120 * (a - 1))
    End Function

    Public Shared Sub EnsureSchemaAndRecoverStuck()
        If Not TableExists() Then Return
        PatchCorrelationDefaultToNewId()
        EnsureColumn("ReminderQueueId", "ALTER TABLE dbo.WhatsAppOutboundMessage ADD ReminderQueueId BIGINT NULL")
        EnsureColumn("RevealMessageCenter", "ALTER TABLE dbo.WhatsAppOutboundMessage ADD RevealMessageCenter BIT NOT NULL CONSTRAINT DF_WO_RevealMC_Temp DEFAULT (0)")
        EnsureColumn("WaGroupKey", "ALTER TABLE dbo.WhatsAppOutboundMessage ADD WaGroupKey NVARCHAR(64) NULL")
        EnsureColumn("WaPartIndex", "ALTER TABLE dbo.WhatsAppOutboundMessage ADD WaPartIndex TINYINT NULL")
        EnsureColumn("WaPartTotal", "ALTER TABLE dbo.WhatsAppOutboundMessage ADD WaPartTotal TINYINT NULL")
        EnsureColumn("WaSchedulerMeta", "ALTER TABLE dbo.WhatsAppOutboundMessage ADD WaSchedulerMeta NVARCHAR(MAX) NULL")
        EnsureColumn("CallerDisplayName", "ALTER TABLE dbo.WhatsAppOutboundMessage ADD CallerDisplayName NVARCHAR(200) NULL")
        EnsureIndexWaGroupKey()
        TryRecoverStuckSending()
        TryExpireUndeliverablePending()
    End Sub

    Private Shared Function TableExists() As Boolean
        Try
            Using cn1 = Cn()
                Dim n = cn1.ExecuteScalar(Of Integer?)(
                    "SELECT 1 FROM sys.tables WHERE name = N'WhatsAppOutboundMessage' AND schema_id = SCHEMA_ID(N'dbo')")
                Return n.HasValue AndAlso n.Value = 1
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Shared Sub PatchCorrelationDefaultToNewId()
        Try
            Using cn1 = Cn()
                Dim dcName = cn1.ExecuteScalar(Of String)(
                    "SELECT TOP 1 dc.name FROM sys.default_constraints dc " &
                    "INNER JOIN sys.columns c ON c.object_id = dc.parent_object_id AND c.column_id = dc.parent_column_id " &
                    "WHERE dc.parent_object_id = OBJECT_ID(N'dbo.WhatsAppOutboundMessage') AND c.name = N'CorrelationId'")
                If Not String.IsNullOrEmpty(dcName) Then
                    cn1.Execute("ALTER TABLE dbo.WhatsAppOutboundMessage DROP CONSTRAINT " & QuoteBrak(dcName))
                End If
                Dim still = cn1.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM sys.default_constraints dc " &
                    "INNER JOIN sys.columns c ON c.object_id = dc.parent_object_id AND c.column_id = dc.parent_column_id " &
                    "WHERE dc.parent_object_id = OBJECT_ID(N'dbo.WhatsAppOutboundMessage') AND c.name = N'CorrelationId'")
                If still = 0 Then
                    cn1.Execute("ALTER TABLE dbo.WhatsAppOutboundMessage ADD CONSTRAINT DF_WhatsAppOutboundMessage_CorrelationId DEFAULT NEWID() FOR CorrelationId")
                End If
            End Using
        Catch ex As Exception
            Trace.TraceError("WhatsAppOutboundRepository.PatchCorrelationDefault: " & ex.Message)
        End Try
    End Sub

    Private Shared Function QuoteBrak(name As String) As String
        Return "[" & name.Replace("]", "]]") & "]"
    End Function

    Private Shared Sub EnsureColumn(sqlName As String, alterClause As String)
        Try
            Using cn1 = Cn()
                Dim safe = sqlName.Replace("'", "''")
                cn1.Execute("IF COL_LENGTH(N'dbo.WhatsAppOutboundMessage', N'" & safe & "') IS NULL BEGIN " & alterClause & " END")
            End Using
        Catch ex As Exception
            Trace.TraceError("WhatsAppOutboundRepository.EnsureColumn " & sqlName & ": " & ex.Message)
        End Try
    End Sub

    Private Shared Sub EnsureIndexWaGroupKey()
        Try
            Using cn1 = Cn()
                Dim exists = cn1.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.WhatsAppOutboundMessage') AND name = N'IX_WhatsAppOutboundMessage_WaGroupKey'")
                If exists = 0 Then
                    cn1.Execute(
                        "CREATE NONCLUSTERED INDEX IX_WhatsAppOutboundMessage_WaGroupKey ON dbo.WhatsAppOutboundMessage (WaGroupKey) WHERE WaGroupKey IS NOT NULL")
                End If
            End Using
        Catch ex As Exception
            Trace.TraceError("WhatsAppOutboundRepository.EnsureIndexWaGroupKey: " & ex.Message)
        End Try
    End Sub

    Private Shared Sub TryRecoverStuckSending()
        Try
            Using cn1 = Cn()
                cn1.Execute(
                    "UPDATE dbo.WhatsAppOutboundMessage SET Status = N'Pending', LastError = N'Stuck Sending reset', " &
                    "NextAttemptAtUtc = SYSUTCDATETIME(), UpdatedAtUtc = SYSUTCDATETIME() " &
                    "WHERE Status = N'Sending' AND UpdatedAtUtc < DATEADD(MINUTE, -12, SYSUTCDATETIME())")
            End Using
        Catch
        End Try
    End Sub

    ''' <summary>Pending rows past ExpiresAtUtc are never dequeued; mark Dead so status matches reality.</summary>
    Private Shared Sub TryExpireUndeliverablePending()
        Try
            Using cn1 = Cn()
                cn1.Execute(
                    "UPDATE dbo.WhatsAppOutboundMessage SET Status = N'Dead', LastError = N'Expired before dispatch', " &
                    "UpdatedAtUtc = SYSUTCDATETIME() WHERE Status = N'Pending' AND CancelledBeforeSend = 0 " &
                    "AND ExpiresAtUtc IS NOT NULL AND ExpiresAtUtc < SYSUTCDATETIME()")
            End Using
        Catch
        End Try
    End Sub

    Public Shared Function TryEnqueueAppointmentReminder(clinicId As String, row As ApptTwoHourReminderQueueRow, dto As AppointmentReminderDto,
                                                         targetDigits As String, body As String, is24h As Boolean) As Boolean
        If row Is Nothing OrElse String.IsNullOrWhiteSpace(targetDigits) OrElse String.IsNullOrWhiteSpace(body) Then Return False
        If String.IsNullOrWhiteSpace(clinicId) Then Return False
        EnsureSchemaAndRecoverStuck()
        If Not TableExists() Then Return False

        Dim leg = If(is24h, ApptTwoHourReminderQueueRepository.Kind24Hour, ApptTwoHourReminderQueueRepository.Kind2Hour)
        Dim idem = BuildReminderIdempotencyKey(row.AppointmentId, leg, row.ApptStartSnapshot)
        Dim category = If(is24h, WhatsAppMessageCategories.AppointmentReminder, WhatsAppMessageCategories.AppointmentTwoHourReminder)
        Dim source = If(is24h, NameOf(AppointmentReminderService), NameOf(AppointmentTwoHourReminderService))
        Dim sendAtUtc = NormalizeLocalToUtc(If(is24h, row.SendAt24h.GetValueOrDefault(), row.SendAt2h.GetValueOrDefault()))
        Dim apptStartUtc = NormalizeLocalToUtc(row.ApptStartSnapshot)
        Dim expires As DateTime?
        If is24h Then
            Dim capSix = sendAtUtc.AddHours(6)
            Dim capStart = apptStartUtc.AddMinutes(-5)
            expires = If(capSix <= capStart, capSix, capStart)
        Else
            expires = apptStartUtc.AddMinutes(-30)
        End If

        Dim notBefore = sendAtUtc
        Dim patientId As Integer? = row.PatientId
        If dto IsNot Nothing Then patientId = dto.PatientID
        Dim now = DateTime.UtcNow
        Dim nextAttempt = If(now >= notBefore, now, notBefore)

        Try
            Using cn1 = Cn()
                Dim term = cn1.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.WhatsAppOutboundMessage WHERE IdempotencyKey = @idem AND Status IN (N'Sent', N'Dead', N'Cancelled')",
                    New With {.idem = idem})
                If term > 0 Then Return False
                Dim live = cn1.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.WhatsAppOutboundMessage WHERE IdempotencyKey = @idem AND Status IN (N'Pending', N'Sending')",
                    New With {.idem = idem})
                If live > 0 Then Return True

                Const ins As String = "
INSERT INTO dbo.WhatsAppOutboundMessage (
  IdempotencyKey, ClinicId, MessageCategory, SourceHint, PatientId, AppointmentId, ReminderLeg, AppointmentStartUtc,
  TargetDigits, BodyPlain, AttachmentPath, Priority, Status,
  NotBeforeUtc, ExpiresAtUtc, NextAttemptAtUtc, AttemptCount,
  CancelledBeforeSend, SuppressUiFeedback, RevealMessageCenter, ReminderQueueId,
  WaGroupKey, WaPartIndex, WaPartTotal, WaSchedulerMeta)
VALUES (
  @IdempotencyKey, @ClinicId, @MessageCategory, @SourceHint, @PatientId, @AppointmentId, @ReminderLeg, @AppointmentStartUtc,
  @TargetDigits, @BodyPlain, NULL, @Priority, @Status,
  @NotBeforeUtc, @ExpiresAtUtc, @NextAttemptAtUtc, 0,
  0, 1, 0, @ReminderQueueId,
  NULL, NULL, NULL, NULL)"
                Dim n = cn1.Execute(ins, New With {
                    .IdempotencyKey = idem,
                    .ClinicId = clinicId.Trim(),
                    .MessageCategory = category,
                    .SourceHint = source,
                    .PatientId = patientId,
                    .AppointmentId = row.AppointmentId,
                    .ReminderLeg = leg,
                    .AppointmentStartUtc = apptStartUtc,
                    .TargetDigits = targetDigits.Trim(),
                    .BodyPlain = body,
                    .Priority = CByte(2),
                    .Status = WhatsAppOutboundStatuses.Pending,
                    .NotBeforeUtc = notBefore,
                    .ExpiresAtUtc = expires,
                    .NextAttemptAtUtc = nextAttempt,
                    .ReminderQueueId = row.QueueId
                })
                Return n > 0
            End Using
        Catch ex As Exception
            Trace.TraceError("TryEnqueueAppointmentReminder: " & ex.ToString())
            Return False
        End Try
    End Function

    Public Shared Function BuildReminderIdempotencyKey(appointmentId As Integer, leg As String, apptStartSnapshot As DateTime) As String
        Dim legSafe = If(leg, "").Trim().ToLowerInvariant().Replace("|", "")
        Dim ticks = NormalizeLocalToUtc(apptStartSnapshot).Ticks.ToString(CultureInfo.InvariantCulture)
        Dim s = "APT|" & appointmentId.ToString(CultureInfo.InvariantCulture) & "|" & legSafe & "|" & ticks
        If s.Length > 128 Then s = s.Substring(0, 128)
        Return s
    End Function

    Public Shared Function TryEnqueueSchedulerSnapshotPart(
        clinicId As String,
        targetDigits As String,
        caption As String,
        attachmentPath As String,
        expiresAtUtc As DateTime,
        idempotencyKey As String,
        groupKey As String,
        partIndex As Byte,
        partTotal As Byte,
        waSchedulerMetaLines As String,
        sourceHint As String,
        revealMessageCenter As Boolean,
        suppressUi As Boolean) As Boolean

        If String.IsNullOrWhiteSpace(clinicId) OrElse String.IsNullOrWhiteSpace(targetDigits) OrElse String.IsNullOrWhiteSpace(caption) Then Return False
        If String.IsNullOrWhiteSpace(attachmentPath) Then Return False
        EnsureSchemaAndRecoverStuck()
        If Not TableExists() Then Return False

        Dim idem = Trunc(idempotencyKey, 128)
        Dim now = DateTime.UtcNow

        Try
            Using cn1 = Cn()
                Dim term = cn1.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.WhatsAppOutboundMessage WHERE IdempotencyKey = @idem AND Status IN (N'Sent', N'Dead', N'Cancelled')",
                    New With {.idem = idem})
                If term > 0 Then Return False
                Dim live = cn1.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.WhatsAppOutboundMessage WHERE IdempotencyKey = @idem AND Status IN (N'Pending', N'Sending')",
                    New With {.idem = idem})
                If live > 0 Then Return True

                Const ins As String = "
INSERT INTO dbo.WhatsAppOutboundMessage (
  IdempotencyKey, ClinicId, MessageCategory, SourceHint, PatientId, AppointmentId, ReminderLeg, AppointmentStartUtc,
  TargetDigits, BodyPlain, AttachmentPath, Priority, Status,
  NotBeforeUtc, ExpiresAtUtc, NextAttemptAtUtc, AttemptCount,
  CancelledBeforeSend, SuppressUiFeedback, RevealMessageCenter, ReminderQueueId,
  WaGroupKey, WaPartIndex, WaPartTotal, WaSchedulerMeta)
VALUES (
  @IdempotencyKey, @ClinicId, @MessageCategory, @SourceHint, NULL, NULL, NULL, NULL,
  @TargetDigits, @BodyPlain, @AttachmentPath, @Priority, @Status,
  @NotBeforeUtc, @ExpiresAtUtc, @NextAttemptAtUtc, 0,
  0, @SuppressUi, @Reveal, NULL,
  @WaGroupKey, @WaPartIndex, @WaPartTotal, @WaSchedulerMeta)"

                Dim n = cn1.Execute(ins, New With {
                    .IdempotencyKey = idem,
                    .ClinicId = clinicId.Trim(),
                    .MessageCategory = WhatsAppMessageCategories.SchedulerSnapshotAuto,
                    .SourceHint = Trunc(sourceHint, 240),
                    .TargetDigits = targetDigits.Trim(),
                    .BodyPlain = caption,
                    .AttachmentPath = Trunc(attachmentPath, 1020),
                    .Priority = CByte(4),
                    .Status = WhatsAppOutboundStatuses.Pending,
                    .NotBeforeUtc = now,
                    .ExpiresAtUtc = expiresAtUtc,
                    .NextAttemptAtUtc = now,
                    .SuppressUi = suppressUi,
                    .Reveal = revealMessageCenter,
                    .WaGroupKey = Trunc(groupKey, 64),
                    .WaPartIndex = partIndex,
                    .WaPartTotal = partTotal,
                    .WaSchedulerMeta = waSchedulerMetaLines
                })
                Return n > 0
            End Using
        Catch ex As Exception
            Trace.TraceError("TryEnqueueSchedulerSnapshotPart: " & ex.ToString())
            Return False
        End Try
    End Function

    ''' <summary>Manual / Navigator casual sends via outbox — same dispatcher as reminders; Priority 1 drains before reminders (2).</summary>
    Public Shared Function TryEnqueueCasualFromUi(
        clinicId As String,
        targetDigits As String,
        bodyPlain As String,
        attachmentPathMayBeEmpty As String,
        messageCategory As String,
        patientId As Integer?,
        callerDisplayName As String,
        sourceHint As String,
        revealMessageCenter As Boolean) As Boolean
        If String.IsNullOrWhiteSpace(clinicId) OrElse String.IsNullOrWhiteSpace(targetDigits) Then Return False
        Dim attachment = Trunc(If(attachmentPathMayBeEmpty, "").Trim(), 1020)
        If attachment.Length > 0 AndAlso Not IO.File.Exists(attachment) Then Return False
        Dim body = If(bodyPlain, "").Trim()
        If body.Length = 0 AndAlso attachment.Length = 0 Then Return False
        If body.Length = 0 Then body = " "
        Dim category = Trunc(If(messageCategory, WhatsAppMessageCategories.General), 64).Trim()
        EnsureSchemaAndRecoverStuck()
        If Not TableExists() Then Return False

        Dim idem = BuildCasualUiIdempotencyKey()
        Dim caller = Trunc(If(callerDisplayName, "").Trim(), 200)
        If caller.Length = 0 Then caller = Nothing
        Dim src = Trunc(If(sourceHint, "").Trim(), 240)
        If src.Length = 0 Then src = Nothing
        Dim now = DateTime.UtcNow

        Try
            Using cn1 = Cn()
                Const ins As String = "
INSERT INTO dbo.WhatsAppOutboundMessage (
  IdempotencyKey, ClinicId, MessageCategory, SourceHint, CallerDisplayName, PatientId, AppointmentId, ReminderLeg, AppointmentStartUtc,
  TargetDigits, BodyPlain, AttachmentPath, Priority, Status,
  NotBeforeUtc, ExpiresAtUtc, NextAttemptAtUtc, AttemptCount,
  CancelledBeforeSend, SuppressUiFeedback, RevealMessageCenter, ReminderQueueId,
  WaGroupKey, WaPartIndex, WaPartTotal, WaSchedulerMeta)
VALUES (
  @IdempotencyKey, @ClinicId, @MessageCategory, @SourceHint, @CallerDisplayName, @PatientId, NULL, NULL, NULL,
  @TargetDigits, @BodyPlain, @AttachmentPath, @Priority, @Status,
  @NotBeforeUtc, NULL, @NextAttemptAtUtc, 0,
  0, 0, @Reveal, NULL,
  NULL, NULL, NULL, NULL)"
                Dim n = cn1.Execute(ins, New With {
                    .IdempotencyKey = idem,
                    .ClinicId = clinicId.Trim(),
                    .MessageCategory = category,
                    .SourceHint = src,
                    .CallerDisplayName = caller,
                    .PatientId = patientId,
                    .TargetDigits = targetDigits.Trim(),
                    .BodyPlain = body,
                    .AttachmentPath = If(attachment.Length = 0, CType(Nothing, String), attachment),
                    .Priority = CByte(1),
                    .Status = WhatsAppOutboundStatuses.Pending,
                    .NotBeforeUtc = now,
                    .NextAttemptAtUtc = now,
                    .Reveal = revealMessageCenter
                })
                Return n > 0
            End Using
        Catch ex As Exception
            Trace.TraceError("TryEnqueueCasualFromUi: " & ex.ToString())
            Return False
        End Try
    End Function

    Private Shared Function BuildCasualUiIdempotencyKey() As String
        Dim g = Guid.NewGuid().ToString("N")
        Dim s = "UI|" & g
        If s.Length <= 128 Then Return s
        Return s.Substring(0, 128)
    End Function

    ''' <summary>One-off manual ~24h reminder (no <see cref="ApptTwoHourReminderQueueRepository"/> row). Distinct leg from queued automatic reminders.</summary>
    Public Shared Function BuildManual24hReminderIdempotencyKey(appointmentId As Integer, apptStartLocal As DateTime) As String
        Dim ticks = NormalizeLocalToUtc(apptStartLocal).Ticks.ToString(CultureInfo.InvariantCulture)
        Return Trunc("APT|" & appointmentId.ToString(CultureInfo.InvariantCulture) & "|manual24|" & ticks, 128)
    End Function

    ''' <summary>Ties outbound crash uploads to file path + write time without exceeding 128 chars.</summary>
    Public Shared Function BuildCrashReportOutboundIdempotencyKey(crashReportFullPath As String) As String
        Dim fp = If(crashReportFullPath, "").Trim()
        If fp.Length = 0 Then Return Nothing
        Dim ticksStr As String
        Try
            ticksStr = IO.File.GetLastWriteTimeUtc(fp).Ticks.ToString(CultureInfo.InvariantCulture)
        Catch
            Return Nothing
        End Try
        Dim name = IO.Path.GetFileName(fp)
        Dim s = "CRASH|" & ticksStr & "|" & name & "|" & fp.Length.ToString(CultureInfo.InvariantCulture)
        Return Trunc(s, 128)
    End Function

    Private Shared Function ComputeOutboundPriorityForCategory(category As String) As Byte
        If String.IsNullOrWhiteSpace(category) Then Return 3
        If String.Equals(category, WhatsAppMessageCategories.ManualSend, StringComparison.OrdinalIgnoreCase) OrElse
            String.Equals(category, WhatsAppMessageCategories.NavigatorWhatsApp, StringComparison.OrdinalIgnoreCase) OrElse
            String.Equals(category, WhatsAppMessageCategories.General, StringComparison.OrdinalIgnoreCase) Then Return 1
        If String.Equals(category, WhatsAppMessageCategories.AppointmentReminder, StringComparison.OrdinalIgnoreCase) OrElse
            String.Equals(category, WhatsAppMessageCategories.AppointmentTwoHourReminder, StringComparison.OrdinalIgnoreCase) Then Return 2
        If String.Equals(category, WhatsAppMessageCategories.SchedulerSnapshotAuto, StringComparison.OrdinalIgnoreCase) Then Return 4
        Return 3
    End Function

    Private Class OutboundInsertRowPeek
        Public Property MessageId As Long
        Public Property CorrelationId As Guid
    End Class

    ''' <summary>
    ''' Central enqueue for generic sends: reminders / UI / navigator / attachments share one INSERT and idempotency contract.
    ''' Returns <see cref="WhatsAppOutboundUnifiedEnqueueResult.BlockDirectGatewaySend"/> when <see cref="WhatsAppService.SendMessageAsync"/> must not POST immediately.
    ''' </summary>
    Public Shared Function TryEnqueueUnifiedFromSendIntent(
        clinicId As String,
        targetDigits As String,
        bodyPlainEffective As String,
        attachmentPathMayBeEmpty As String,
        context As WhatsAppSendContext,
        resolvedMessageCategory As String) As WhatsAppOutboundUnifiedEnqueueResult

        Dim r As New WhatsAppOutboundUnifiedEnqueueResult()
        If String.IsNullOrWhiteSpace(clinicId) OrElse String.IsNullOrWhiteSpace(targetDigits) Then Return r
        Dim attachment = Trunc(If(attachmentPathMayBeEmpty, "").Trim(), 1020)
        If attachment.Length > 0 AndAlso Not IO.File.Exists(attachment) Then Return r

        Dim body = If(bodyPlainEffective, "").Trim()
        If body.Length = 0 AndAlso attachment.Length = 0 Then Return r
        If body.Length = 0 Then body = " "

        Dim category = Trunc(If(resolvedMessageCategory, WhatsAppMessageCategories.General), 64).Trim()
        EnsureSchemaAndRecoverStuck()
        If Not TableExists() Then Return r

        Dim idemRaw = If(context IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(context.IdempotencyKey), context.IdempotencyKey.Trim(), Nothing)
        Dim idem As String = If(String.IsNullOrEmpty(idemRaw), "GEN|" & Guid.NewGuid().ToString("N"), Trunc(idemRaw, 128))

        Dim callerDisp As String = Nothing
        If context IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(context.DisplayName) Then
            callerDisp = Trunc(context.DisplayName.Trim(), 200)
            If callerDisp.Length = 0 Then callerDisp = Nothing
        End If

        Dim src As String = Nothing
        If context IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(context.SourceHint) Then
            src = Trunc(context.SourceHint.Trim(), 240)
            If src.Length = 0 Then src = Nothing
        End If

        Dim pid As Integer? = If(context IsNot Nothing, context.PatientId, Nothing)
        Dim apptId As Integer? = If(context IsNot Nothing, context.AppointmentId, Nothing)
        Dim apptStartUtc As DateTime? = Nothing
        If context IsNot Nothing AndAlso context.AppointmentStartUtc.HasValue Then apptStartUtc = NormalizeLocalToUtc(context.AppointmentStartUtc.Value)

        Dim expires As DateTime? = Nothing
        If context IsNot Nothing AndAlso context.OutboundExpiresAtUtc.HasValue Then
            Dim e0 = context.OutboundExpiresAtUtc.Value
            expires = If(e0.Kind = DateTimeKind.Utc, e0, NormalizeLocalToUtc(e0))
        End If

        Dim notBefore = DateTime.UtcNow
        If context IsNot Nothing AndAlso context.OutboundNotBeforeUtc.HasValue Then
            Dim n0 = context.OutboundNotBeforeUtc.Value
            notBefore = If(n0.Kind = DateTimeKind.Utc, n0, NormalizeLocalToUtc(n0))
        End If

        Dim nowUtc = DateTime.UtcNow
        Dim nextAttempt = If(nowUtc >= notBefore, nowUtc, notBefore)

        Dim sup As Boolean = context IsNot Nothing AndAlso context.SuppressUiFeedback
        Dim rev As Boolean = context IsNot Nothing AndAlso context.RevealMessageCenter
        Dim pr = ComputeOutboundPriorityForCategory(category)

        Try
            Using cn1 = Cn()
                Dim priorTerm = cn1.ExecuteScalar(Of String)(
                    "SELECT TOP 1 Status FROM dbo.WhatsAppOutboundMessage WHERE IdempotencyKey = @idem AND Status IN (N'Sent', N'Dead', N'Cancelled')",
                    New With {.idem = idem})
                If Not String.IsNullOrWhiteSpace(priorTerm) Then
                    r.BlockDirectGatewaySend = True
                    r.ShouldRequestImmediateDispatch = False
                    r.TerminalPriorStatus = priorTerm.Trim()
                    Return r
                End If

                Dim live = cn1.QuerySingleOrDefault(Of OutboundInsertRowPeek)(
                    "SELECT TOP 1 MessageId, CorrelationId FROM dbo.WhatsAppOutboundMessage WHERE IdempotencyKey = @idem AND (Status = @P OR Status = @S)",
                    New With {.idem = idem, .P = WhatsAppOutboundStatuses.Pending, .S = WhatsAppOutboundStatuses.Sending})
                If live IsNot Nothing AndAlso live.MessageId > 0 Then
                    r.BlockDirectGatewaySend = True
                    r.ShouldRequestImmediateDispatch = True
                    r.InsertedMessageId = live.MessageId
                    r.CorrelationId = live.CorrelationId
                    r.IsLiveDuplicate = True
                    Return r
                End If

                Const insSql As String =
                    "INSERT INTO dbo.WhatsAppOutboundMessage (" &
                    "  IdempotencyKey, ClinicId, MessageCategory, SourceHint, CallerDisplayName, PatientId, AppointmentId, ReminderLeg, AppointmentStartUtc," &
                    "  TargetDigits, BodyPlain, AttachmentPath, Priority, Status," &
                    "  NotBeforeUtc, ExpiresAtUtc, NextAttemptAtUtc, AttemptCount," &
                    "  CancelledBeforeSend, SuppressUiFeedback, RevealMessageCenter, ReminderQueueId," &
                    "  WaGroupKey, WaPartIndex, WaPartTotal, WaSchedulerMeta)" &
                    "OUTPUT INSERTED.MessageId, INSERTED.CorrelationId " &
                    "VALUES (" &
                    "  @IdempotencyKey, @ClinicId, @MessageCategory, @SourceHint, @CallerDisplayName, @PatientId, @AppointmentId, NULL, @AppointmentStartUtc," &
                    "  @TargetDigits, @BodyPlain, @AttachmentPath, @Priority, @Status," &
                    "  @NotBeforeUtc, @ExpiresAtUtc, @NextAttemptAtUtc, 0," &
                    "  0, @SuppressUi, @Reveal, NULL," &
                    "  NULL, NULL, NULL, NULL)"

                Dim insRow = cn1.QuerySingleOrDefault(Of OutboundInsertRowPeek)(insSql, New With {
                    .IdempotencyKey = idem,
                    .ClinicId = clinicId.Trim(),
                    .MessageCategory = category,
                    .SourceHint = src,
                    .CallerDisplayName = callerDisp,
                    .PatientId = pid,
                    .AppointmentId = apptId,
                    .AppointmentStartUtc = apptStartUtc,
                    .TargetDigits = targetDigits.Trim(),
                    .BodyPlain = body,
                    .AttachmentPath = If(attachment.Length = 0, CType(Nothing, String), attachment),
                    .Priority = pr,
                    .Status = WhatsAppOutboundStatuses.Pending,
                    .NotBeforeUtc = notBefore,
                    .ExpiresAtUtc = expires,
                    .NextAttemptAtUtc = nextAttempt,
                    .SuppressUi = sup,
                    .Reveal = rev
                })
                If insRow Is Nothing OrElse insRow.MessageId <= 0 Then Return r
                r.BlockDirectGatewaySend = True
                r.ShouldRequestImmediateDispatch = True
                r.InsertedMessageId = insRow.MessageId
                r.CorrelationId = insRow.CorrelationId
                r.IsLiveDuplicate = False
                Return r
            End Using
        Catch ex As Exception
            Trace.TraceError("TryEnqueueUnifiedFromSendIntent: " & ex.ToString())
            Return r
        End Try
    End Function

    ''' <summary>Cancellable casual rows for this clinic (ManualSend, NavigatorWhatsApp, General — Pending, not cancelled).</summary>
    Public Shared Function ListPendingCasualForClinic(clinicId As String, take As Integer) As IList(Of WhatsAppOutboundPendingCasualRow)
        Dim list As New List(Of WhatsAppOutboundPendingCasualRow)()
        If String.IsNullOrWhiteSpace(clinicId) Then Return list
        If take < 1 Then take = 50
        If take > 500 Then take = 500
        If Not TableExists() Then Return list
        Try
            Dim takeStr = take.ToString(CultureInfo.InvariantCulture)
            Using cn1 = Cn()
                Dim sql =
                    "SELECT TOP (" & takeStr & ") MessageId, MessageCategory, TargetDigits, " &
                    "LEFT(BodyPlain, 500) AS MessagePreview, AttachmentPath, CreatedAtUtc " &
                    "FROM dbo.WhatsAppOutboundMessage WHERE ClinicId = @C " &
                    "AND CancelledBeforeSend = 0 AND Status = N'Pending' " &
                    "AND MessageCategory IN (N'ManualSend', N'NavigatorWhatsApp', N'General') " &
                    "ORDER BY MessageId DESC"
                Dim rows = cn1.Query(Of WhatsAppOutboundPendingCasualRow)(sql, New With {.C = clinicId.Trim()})
                If rows IsNot Nothing Then Return rows.ToList()
            End Using
        Catch ex As Exception
            Trace.TraceError("ListPendingCasualForClinic: " & ex.ToString())
        End Try
        Return list
    End Function

    ''' <summary>Mark row cancelled before gateway send; excluded by Dequeue / claim.</summary>
    Public Shared Function TryCancelPendingCasualOutbound(messageId As Long, clinicId As String) As Boolean
        If messageId <= 0 OrElse String.IsNullOrWhiteSpace(clinicId) Then Return False
        If Not TableExists() Then Return False
        Dim note = Trunc("Cancelled by user", 2000)
        Try
            Using cn1 = Cn()
                Dim n = cn1.Execute(
                    "UPDATE dbo.WhatsAppOutboundMessage SET CancelledBeforeSend = 1, Status = @X, LastError = @E, " &
                    "UpdatedAtUtc = SYSUTCDATETIME() WHERE MessageId = @Id AND ClinicId = @C " &
                    "AND Status = @P AND CancelledBeforeSend = 0 " &
                    "AND MessageCategory IN (N'ManualSend', N'NavigatorWhatsApp', N'General')",
                    New With {
                        .Id = messageId,
                        .C = clinicId.Trim(),
                        .P = WhatsAppOutboundStatuses.Pending,
                        .X = WhatsAppOutboundStatuses.Cancelled,
                        .E = note
                    })
                Return n > 0
            End Using
        Catch ex As Exception
            Trace.TraceError("TryCancelPendingCasualOutbound: " & ex.ToString())
            Return False
        End Try
    End Function

    ''' <summary>Same as <see cref="DequeueDueBatch"/> but only rows whose <see cref="WhatsAppOutboundMessageRow.WaGroupKey"/> is in <paramref name="waGroupKeys"/>.</summary>
    Public Shared Function DequeuePendingBatchForWaGroupKeys(waGroupKeys As IList(Of String), take As Integer) As IList(Of WhatsAppOutboundMessageRow)
        Dim list As New List(Of WhatsAppOutboundMessageRow)()
        If waGroupKeys Is Nothing OrElse waGroupKeys.Count = 0 Then Return list
        If take < 1 Then take = 10
        If take > 100 Then take = 100
        If Not TableExists() Then Return list
        Try
            Dim takeStr = take.ToString(CultureInfo.InvariantCulture)
            Dim keysSanitized = waGroupKeys.
                Where(Function(s) Not String.IsNullOrWhiteSpace(s)).
                Select(Function(s) s.Trim()).
                Distinct(StringComparer.OrdinalIgnoreCase).
                ToList()
            If keysSanitized.Count = 0 Then Return list
            Using cn1 = Cn()
                Dim sql =
                    "SELECT TOP (" & takeStr & ") MessageId, CorrelationId, IdempotencyKey, ClinicId, MessageCategory, SourceHint, CallerDisplayName, " &
                    "PatientId, AppointmentId, ReminderLeg, AppointmentStartUtc, TargetDigits, BodyPlain, AttachmentPath, Priority, Status, GatewayJobId, " &
                    "CreatedAtUtc, UpdatedAtUtc, NotBeforeUtc, ExpiresAtUtc, NextAttemptAtUtc, AttemptCount, LastError, " &
                    "CancelledBeforeSend, SuppressUiFeedback, RevealMessageCenter, ReminderQueueId, WaGroupKey, WaPartIndex, WaPartTotal, WaSchedulerMeta " &
                    "FROM dbo.WhatsAppOutboundMessage WHERE CancelledBeforeSend = 0 AND Status = @st " &
                    "AND NextAttemptAtUtc <= SYSUTCDATETIME() AND NotBeforeUtc <= SYSUTCDATETIME() AND (ExpiresAtUtc IS NULL OR ExpiresAtUtc >= SYSUTCDATETIME()) " &
                    "AND WaGroupKey IN @keys " &
                    "ORDER BY Priority ASC, NextAttemptAtUtc ASC, MessageId ASC"
                Dim rows = cn1.Query(Of WhatsAppOutboundMessageRow)(sql, New With {.st = WhatsAppOutboundStatuses.Pending, .keys = keysSanitized})
                If rows IsNot Nothing Then Return rows.ToList()
            End Using
        Catch ex As Exception
            Trace.TraceError("DequeuePendingBatchForWaGroupKeys: " & ex.ToString())
        End Try
        Return list
    End Function

    Public Shared Function DequeueDueBatch(take As Integer) As IList(Of WhatsAppOutboundMessageRow)
        Dim list As New List(Of WhatsAppOutboundMessageRow)()
        If take < 1 Then take = 10
        If take > 100 Then take = 100
        If Not TableExists() Then Return list
        Try
            Dim takeStr = take.ToString(CultureInfo.InvariantCulture)
            Using cn1 = Cn()
                Dim sql =
                    "SELECT TOP (" & takeStr & ") MessageId, CorrelationId, IdempotencyKey, ClinicId, MessageCategory, SourceHint, CallerDisplayName, " &
                    "PatientId, AppointmentId, ReminderLeg, AppointmentStartUtc, TargetDigits, BodyPlain, AttachmentPath, Priority, Status, GatewayJobId, " &
                    "CreatedAtUtc, UpdatedAtUtc, NotBeforeUtc, ExpiresAtUtc, NextAttemptAtUtc, AttemptCount, LastError, " &
                    "CancelledBeforeSend, SuppressUiFeedback, RevealMessageCenter, ReminderQueueId, WaGroupKey, WaPartIndex, WaPartTotal, WaSchedulerMeta " &
                    "FROM dbo.WhatsAppOutboundMessage WHERE CancelledBeforeSend = 0 AND Status = @st " &
                    "AND NextAttemptAtUtc <= SYSUTCDATETIME() AND NotBeforeUtc <= SYSUTCDATETIME() AND (ExpiresAtUtc IS NULL OR ExpiresAtUtc >= SYSUTCDATETIME()) " &
                    "ORDER BY Priority ASC, NextAttemptAtUtc ASC, MessageId ASC"
                Dim rows = cn1.Query(Of WhatsAppOutboundMessageRow)(sql, New With {.st = WhatsAppOutboundStatuses.Pending})
                If rows IsNot Nothing Then Return rows.ToList()
            End Using
        Catch ex As Exception
            Trace.TraceError("DequeueDueBatch: " & ex.ToString())
        End Try
        Return list
    End Function

    Public Shared Function TryClaimSending(messageId As Long) As Boolean
        Try
            Using cn1 = Cn()
                Dim n = cn1.Execute(
                    "UPDATE dbo.WhatsAppOutboundMessage SET Status = @S, UpdatedAtUtc = SYSUTCDATETIME() " &
                    "WHERE MessageId = @Id AND Status = @P AND CancelledBeforeSend = 0",
                    New With {
                        .Id = messageId,
                        .S = WhatsAppOutboundStatuses.Sending,
                        .P = WhatsAppOutboundStatuses.Pending
                    })
                Return n > 0
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Sub MarkSendSucceeded(messageId As Long, gatewayJobIdOptional As String)
        Dim gj = Trunc(If(gatewayJobIdOptional, "").Trim(), 128)
        If String.IsNullOrEmpty(gj) Then gj = Nothing
        Try
            Using cn1 = Cn()
                cn1.Execute(
                    "UPDATE dbo.WhatsAppOutboundMessage SET Status = @S, GatewayJobId = @G, LastError = NULL, " &
                    "AttemptCount = AttemptCount + 1, UpdatedAtUtc = SYSUTCDATETIME() " &
                    "WHERE MessageId = @Id AND Status = @Sending",
                    New With {
                        .Id = messageId,
                        .S = WhatsAppOutboundStatuses.Sent,
                        .Sending = WhatsAppOutboundStatuses.Sending,
                        .G = gj
                    })
            End Using
        Catch
        End Try
    End Sub

    ''' <summary>Returns True if marked Dead (expired retries).</summary>
    Public Shared Function ApplySendFailure(messageId As Long, rawError As String) As Boolean
        Dim err = Trunc(If(rawError, ""), 2000)
        If String.IsNullOrWhiteSpace(err) Then err = "Send failed."
        Dim nowUtc = DateTime.UtcNow
        Dim isDead As Boolean = False

        Try
            Using cn1 = Cn()
                Dim row = cn1.QuerySingleOrDefault(Of WhatsAppOutboundMessageRow)(
                    "SELECT MessageId, ExpiresAtUtc, AttemptCount FROM dbo.WhatsAppOutboundMessage WHERE MessageId = @Id AND Status = @S",
                    New With {.Id = messageId, .S = WhatsAppOutboundStatuses.Sending})
                If row Is Nothing Then Return False

                Dim newAttempt = row.AttemptCount + 1
                If row.ExpiresAtUtc.HasValue AndAlso nowUtc > row.ExpiresAtUtc.Value Then
                    isDead = True
                    cn1.Execute(
                        "UPDATE dbo.WhatsAppOutboundMessage SET Status = @D, LastError = @E, AttemptCount = AttemptCount + 1, " &
                        "UpdatedAtUtc = SYSUTCDATETIME() WHERE MessageId = @Id AND Status = @S",
                        New With {.Id = messageId, .D = WhatsAppOutboundStatuses.Dead, .S = WhatsAppOutboundStatuses.Sending, .E = err})
                Else
                    Dim delaySec = ComputeBackoffSeconds(newAttempt)
                    Dim nextAt = nowUtc.AddSeconds(delaySec)
                    cn1.Execute(
                        "UPDATE dbo.WhatsAppOutboundMessage SET Status = @P, LastError = @E, AttemptCount = AttemptCount + 1, " &
                        "NextAttemptAtUtc = @N, UpdatedAtUtc = SYSUTCDATETIME() WHERE MessageId = @Id AND Status = @S",
                        New With {
                            .Id = messageId,
                            .P = WhatsAppOutboundStatuses.Pending,
                            .S = WhatsAppOutboundStatuses.Sending,
                            .E = err,
                            .N = nextAt
                        })
                End If
            End Using
        Catch
        End Try
        Return isDead
    End Function

    Public Shared Function GetWaGroupStatusCounts(waGroupKey As String) As Dictionary(Of String, Integer)
        Dim d As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        If String.IsNullOrWhiteSpace(waGroupKey) Then Return d
        Try
            Using cn1 = Cn()
                Dim rows = cn1.Query(Of StatusCountRow)(
                    "SELECT Status, COUNT(*) AS Cnt FROM dbo.WhatsAppOutboundMessage WHERE WaGroupKey = @K GROUP BY Status",
                    New With {.K = waGroupKey.Trim()})
                If rows Is Nothing Then Return d
                For Each t In rows
                    If t Is Nothing OrElse String.IsNullOrWhiteSpace(t.Status) Then Continue For
                    d(t.Status.Trim()) = t.Cnt
                Next
            End Using
        Catch
        End Try
        Return d
    End Function

    Private Class StatusCountRow
        Public Property Status As String
        Public Property Cnt As Integer
    End Class

    Public Shared Function TryGetFirstSchedulerFailureInGroup(waGroupKey As String) As String
        If String.IsNullOrWhiteSpace(waGroupKey) Then Return Nothing
        Try
            Using cn1 = Cn()
                Return cn1.ExecuteScalar(Of String)(
                    "SELECT TOP 1 LastError FROM dbo.WhatsAppOutboundMessage WHERE WaGroupKey = @K AND Status = N'Dead' ORDER BY MessageId",
                    New With {.K = waGroupKey.Trim()})
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    Public Shared Function CountSchedulerLogsForRecipientRun(jobId As Integer, recipientId As Long, runDate As Date, sendSlot As Byte) As Integer
        Try
            Using cn1 = Cn()
                Return cn1.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.SchedulerSnapshotAutoSendLog WHERE JobId = @J AND RecipientId = @R AND RunDate = @D AND SendSlot = @S",
                    New With {.J = jobId, .R = recipientId, .D = runDate.Date, .S = sendSlot})
            End Using
        Catch
            Return 0
        End Try
    End Function

    Friend Shared Function Trunc(s As String, maxLen As Integer) As String
        If String.IsNullOrEmpty(s) Then Return ""
        If s.Length <= maxLen Then Return s
        Return s.Substring(0, maxLen)
    End Function

    ''' <summary>Ensures optional columns/default exist; True when dbo.WhatsAppOutboundMessage is on the database.</summary>
    Public Shared Function IsOutboundInfrastructureReady() As Boolean
        EnsureSchemaAndRecoverStuck()
        Return TableExists()
    End Function

    Public Shared Function CountRowsForWaGroup(waGroupKey As String) As Integer
        If String.IsNullOrWhiteSpace(waGroupKey) Then Return 0
        Try
            Using cn1 = Cn()
                Return cn1.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.WhatsAppOutboundMessage WHERE WaGroupKey = @K",
                    New With {.K = waGroupKey.Trim()})
            End Using
        Catch
            Return 0
        End Try
    End Function

    ''' <summary>Paged browse of outbound queue rows (same database as dbo.WhatsAppMessageLog).</summary>
    Public Shared Function QueryOutboundArchivePage(skip As Integer, take As Integer, problemsOnly As Boolean, search As String) As WhatsAppOutboundArchivePageResult
        Dim result As New WhatsAppOutboundArchivePageResult()
        EnsureSchemaAndRecoverStuck()
        If Not TableExists() Then Return result

        If take < 1 Then take = 50
        If take > 5000 Then take = 5000
        If skip < 0 Then skip = 0

        Dim searchTrim = If(search, "").Trim()
        Dim hasSearch = If(searchTrim.Length > 0, 1, 0)
        Dim searchLike As String = Nothing
        Dim searchInt As Integer? = Nothing
        Dim searchBigInt As Long? = Nothing
        If hasSearch = 1 Then
            searchLike = "%" & searchTrim & "%"
            Dim pint As Integer
            If Integer.TryParse(searchTrim, pint) Then searchInt = pint
            Dim pbig As Long
            If Long.TryParse(searchTrim, pbig) Then searchBigInt = pbig
        End If

        Dim p As New DynamicParameters()
        p.Add("ProblemsOnly", If(problemsOnly, 1, 0))
        p.Add("HasSearch", hasSearch)
        p.Add("SearchLike", searchLike)
        p.Add("SearchInt", searchInt)
        p.Add("SearchBigInt", searchBigInt)
        p.Add("Skip", skip)
        p.Add("Take", take)

        Dim whereRest As String =
            " AND (@ProblemsOnly = 0 OR m.Status IN (N'Dead', N'Cancelled'))" &
            " AND (@HasSearch = 0 OR (" &
            " m.TargetDigits LIKE @SearchLike OR m.BodyPlain LIKE @SearchLike OR m.MessageCategory LIKE @SearchLike OR " &
            " ISNULL(m.SourceHint, N'') LIKE @SearchLike OR ISNULL(m.LastError, N'') LIKE @SearchLike OR " &
            " ISNULL(m.GatewayJobId, N'') LIKE @SearchLike OR m.IdempotencyKey LIKE @SearchLike OR m.ClinicId LIKE @SearchLike OR " &
            " (@SearchBigInt IS NOT NULL AND m.MessageId = @SearchBigInt) OR " &
            " (@SearchInt IS NOT NULL AND (m.PatientId = @SearchInt OR m.AppointmentId = @SearchInt))" &
            "))"

        Try
            Using cn1 = Cn()
                result.TotalCount = cn1.ExecuteScalar(Of Integer)("SELECT COUNT(1) FROM dbo.WhatsAppOutboundMessage m WHERE 1=1" & whereRest, p)

                Dim dataSql =
                    "SELECT m.MessageId, m.CreatedAtUtc, m.UpdatedAtUtc, m.Status, m.MessageCategory, m.SourceHint, m.TargetDigits, " &
                    "LEFT(ISNULL(m.BodyPlain, N''), 500) AS BodyPreview, " &
                    "LEFT(ISNULL(m.AttachmentPath, N''), 120) AS AttachmentPathPreview, " &
                    "m.PatientId, m.AppointmentId, m.Priority, " &
                    "LEFT(ISNULL(m.LastError, N''), 300) AS LastErrorPreview, " &
                    "m.CancelledBeforeSend, m.GatewayJobId, m.ClinicId, m.NextAttemptAtUtc, m.AttemptCount, m.ExpiresAtUtc " &
                    "FROM dbo.WhatsAppOutboundMessage m WHERE 1=1" & whereRest &
                    " ORDER BY m.MessageId DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY"

                Dim rows = cn1.Query(Of WhatsAppOutboundArchiveRow)(dataSql, p)
                If rows IsNot Nothing Then result.Rows = rows.ToList()
            End Using
        Catch ex As Exception
            Trace.TraceError("QueryOutboundArchivePage: " & ex.ToString())
        End Try

        Return result
    End Function

    Public Shared Sub DeletePendingRowsForWaGroup(waGroupKey As String)
        If String.IsNullOrWhiteSpace(waGroupKey) Then Return
        Try
            Using cn1 = Cn()
                cn1.Execute(
                    "DELETE FROM dbo.WhatsAppOutboundMessage WHERE WaGroupKey = @K AND Status = @P",
                    New With {.K = waGroupKey.Trim(), .P = WhatsAppOutboundStatuses.Pending})
            End Using
        Catch ex As Exception
            Trace.TraceError("DeletePendingRowsForWaGroup: " & ex.Message)
        End Try
    End Sub
End Class

''' <summary>Result from <see cref="WhatsAppOutboundRepository.TryEnqueueUnifiedFromSendIntent"/>.</summary>
Public Structure WhatsAppOutboundUnifiedEnqueueResult
    ''' <summary>When True, callers must not POST to the WhatsApp gateway in the same call (already queued / terminal dedupe).</summary>
    Public BlockDirectGatewaySend As Boolean
    ''' <summary>Inserted a new Pending row or a live duplicate exists — ping the dispatcher soon.</summary>
    Public ShouldRequestImmediateDispatch As Boolean
    ''' <summary>Populated after insert or when a live Pending/Sending row exists.</summary>
    Public InsertedMessageId As Long
    Public CorrelationId As Guid?
    ''' <summary>When set, BlockDirectGatewaySend is True and POST must be skipped prior attempt already terminal for IdempotencyKey.</summary>
    Public TerminalPriorStatus As String
    ''' <summary>True when Pending/Sending already existed.</summary>
    Public IsLiveDuplicate As Boolean
End Structure
