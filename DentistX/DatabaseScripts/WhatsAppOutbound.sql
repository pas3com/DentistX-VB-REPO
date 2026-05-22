/*
  WhatsAppOutbound — centralized outbound queue (outbox) for the WhatsApp dispatcher.

  Application role
  -   Producers enqueue rows (or upsert by IdempotencyKey); one client-side poller picks due rows,
    sends via the gateway API, updates status / NextAttemptAtUtc / AttemptCount.
  - Application code: most producers call WhatsAppService.SendMessageAsync, which inserts into this table
    when dbo.WhatsAppOutboundMessage exists and BypassOutboundQueue is false; the dispatcher replays with BypassOutboundQueue true.

  Retry / expiry (enforced by application when inserting/updating rows; SQL documents contract)
  - Casual / message center: CancelledBeforeSend = 1 means never send; ExpiresAtUtc may be NULL
    (retry until cancelled or optional global max attempts in code).
  - Scheduler snapshot auto-send: set ExpiresAtUtc to align with product window (e.g. ~6h after
    scheduled send slot — same intent as DueGraceMinutes in SchedulerSnapshotAutoSendService).
  - Appointment ~24h reminder: NotBeforeUtc = first eligible send (~24h before start).
    ExpiresAtUtc should be min(SendAt24h + 6 hours, appointment start minus a small buffer) so
    no message fires after the visit has realistically begun (buffer is app-defined).

  Appointment short-lead reminder (variable hours before start, “~2h” by default) — PATIENT-CENTRIC
  - First attempt at NotBeforeUtc = appointment start minus the user’s ShortReminder hours (from settings).
  - Retries (backoff in app) continue while Status is pending-send and GETUTCDATETIME() <= ExpiresAtUtc.
  - ExpiresAtUtc MUST be set to: appointment start (UTC) minus 30 minutes.
    Example: appointment 16:00 local (stored as AppointmentStartUtc), short-lead first send ~14:00;
    retries may run until 15:30 (start − 30 min), independent of ShortReminder value.
    ShortReminder only shifts when the first attempt is allowed (NotBeforeUtc), not the final cutoff.

  Idempotency: unique index on IdempotencyKey prevents duplicate deliveries for the same logical send.
*/

IF OBJECT_ID(N'dbo.WhatsAppOutboundMessage', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[WhatsAppOutboundMessage] (
        [MessageId]            BIGINT IDENTITY (1, 1) NOT NULL,
        [CorrelationId]        UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_WhatsAppOutboundMessage_CorrelationId]
                               DEFAULT NEWID (),
        [IdempotencyKey]       NVARCHAR (128) NOT NULL,
        [ClinicId]             NVARCHAR (64) NOT NULL,
        [MessageCategory]      NVARCHAR (64) NOT NULL,
        [SourceHint]           NVARCHAR (256) NULL,
        [CallerDisplayName]    NVARCHAR (200) NULL,
        [PatientId]            INT NULL,
        [AppointmentId]        INT NULL,
        [ReminderLeg]          NVARCHAR (8) NULL,
        /* UTC instant of appointment start when known; used by app to set ExpiresAtUtc for patient-centric short-lead (start − 30 min). */
        [AppointmentStartUtc]  DATETIME2 (3) NULL,
        [TargetDigits]         NVARCHAR (32) NOT NULL,
        [BodyPlain]            NVARCHAR (MAX) NOT NULL,
        [AttachmentPath]       NVARCHAR (1024) NULL,
        [Priority]             TINYINT NOT NULL CONSTRAINT [DF_WhatsAppOutboundMessage_Priority] DEFAULT (5),
        [Status]               NVARCHAR (32) NOT NULL CONSTRAINT [DF_WhatsAppOutboundMessage_Status]
                               DEFAULT N'Pending',
        [GatewayJobId]         NVARCHAR (128) NULL,
        [CreatedAtUtc]         DATETIME2 (3) NOT NULL CONSTRAINT [DF_WhatsAppOutboundMessage_CreatedAtUtc]
                               DEFAULT SYSUTCDATETIME (),
        [UpdatedAtUtc]         DATETIME2 (3) NOT NULL CONSTRAINT [DF_WhatsAppOutboundMessage_UpdatedAtUtc]
                               DEFAULT SYSUTCDATETIME (),
        [NotBeforeUtc]         DATETIME2 (3) NOT NULL CONSTRAINT [DF_WhatsAppOutboundMessage_NotBeforeUtc]
                               DEFAULT SYSUTCDATETIME (),
        [ExpiresAtUtc]         DATETIME2 (3) NULL,
        [NextAttemptAtUtc]     DATETIME2 (3) NOT NULL CONSTRAINT [DF_WhatsAppOutboundMessage_NextAttemptAtUtc]
                               DEFAULT SYSUTCDATETIME (),
        [AttemptCount]         INT NOT NULL CONSTRAINT [DF_WhatsAppOutboundMessage_AttemptCount] DEFAULT (0),
        [LastError]            NVARCHAR (2000) NULL,
        [CancelledBeforeSend]  BIT NOT NULL CONSTRAINT [DF_WhatsAppOutboundMessage_CancelledBeforeSend] DEFAULT (0),
        [SuppressUiFeedback]   BIT NOT NULL CONSTRAINT [DF_WhatsAppOutboundMessage_SuppressUiFeedback] DEFAULT (0),
        [RevealMessageCenter]  BIT NOT NULL CONSTRAINT [DF_WhatsAppOutboundMessage_RevealMessageCenter] DEFAULT (0),

        /* Link dbo.ApptTwoHourWhatsAppQueue.QueueId for reminder finalization after send. */
        [ReminderQueueId]      BIGINT NULL,
        /*
          Scheduler snapshot auto-send: one outbox row per WhatsApp attachment; rows sharing WaGroupKey form one recipient batch.
          When all parts are terminal (Sent/Dead), Dispatcher writes one row to SchedulerSnapshotAutoSendLog.
        */
        [WaGroupKey]           NVARCHAR (64) NULL,
        [WaPartIndex]          TINYINT NULL,
        [WaPartTotal]          TINYINT NULL,
        /* Pipe-delimited scheduler log context: JobId|RecipientId|yyyy-MM-dd|SendSlot|PathBundle|ForceRun 0/1|ExcludeDedupe 0/1 */
        [WaSchedulerMeta]      NVARCHAR (MAX) NULL,

        CONSTRAINT [PK_WhatsAppOutboundMessage] PRIMARY KEY CLUSTERED ([MessageId] ASC)
    );

    ALTER TABLE [dbo].[WhatsAppOutboundMessage]
        ADD CONSTRAINT [AK_WhatsAppOutboundMessage_IdempotencyKey]
        UNIQUE NONCLUSTERED ([IdempotencyKey] ASC);

    CREATE NONCLUSTERED INDEX [IX_WhatsAppOutboundMessage_Dispatch]
        ON [dbo].[WhatsAppOutboundMessage] ([Status] ASC, [NextAttemptAtUtc] ASC)
        INCLUDE ([Priority], [ClinicId], [CancelledBeforeSend]);

    CREATE NONCLUSTERED INDEX [IX_WhatsAppOutboundMessage_Clinic_Status]
        ON [dbo].[WhatsAppOutboundMessage] ([ClinicId] ASC, [Status] ASC);

    CREATE NONCLUSTERED INDEX [IX_WhatsAppOutboundMessage_Appointment]
        ON [dbo].[WhatsAppOutboundMessage] ([AppointmentId] ASC, [ReminderLeg] ASC)
        WHERE [AppointmentId] IS NOT NULL;

    CREATE NONCLUSTERED INDEX [IX_WhatsAppOutboundMessage_WaGroupKey]
        ON [dbo].[WhatsAppOutboundMessage] ([WaGroupKey] ASC)
        WHERE [WaGroupKey] IS NOT NULL;
END
GO
