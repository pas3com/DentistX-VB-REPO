/*
 Scheduler snapshot auto-send: scheduled daily WhatsApp distribution list.
  - Job: local send time, weekday mask (Mon=1 .. Sun=64 bits), optional caption, enable flag.
  - Recipient: polymorphic source (Doctor / Employee / Secretary / Contact) + denormalized WhatsApp snapshot.
  - Log: per-run audit (status, paths, errors) for confirming delivery.

  Run once per database. Adjust schema name if not dbo.
*/

SET NOCOUNT ON;

IF OBJECT_ID(N'dbo.SchedulerSnapshotAutoSendLog', N'U') IS NOT NULL
    DROP TABLE dbo.SchedulerSnapshotAutoSendLog;

IF OBJECT_ID(N'dbo.SchedulerSnapshotAutoSendRecipient', N'U') IS NOT NULL
    DROP TABLE dbo.SchedulerSnapshotAutoSendRecipient;

IF OBJECT_ID(N'dbo.SchedulerSnapshotAutoSendJob', N'U') IS NOT NULL
    DROP TABLE dbo.SchedulerSnapshotAutoSendJob;

CREATE TABLE dbo.SchedulerSnapshotAutoSendJob (
    JobId            INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    IsEnabled        BIT NOT NULL CONSTRAINT DF_SSAJ_Enabled DEFAULT (1),
    SendTimeLocal    TIME(0) NOT NULL,
    /* Bitmask: Mon=1, Tue=2, Wed=4, Thu=8, Fri=16, Sat=32, Sun=64. 127 = every day. Weekdays only = 31. */
    DaysOfWeekMask   TINYINT NOT NULL CONSTRAINT DF_SSAJ_Days DEFAULT (31),
    MessageCaption   NVARCHAR(500) NULL,
    Notes            NVARCHAR(500) NULL,
    CreatedAt        DATETIME2(0) NOT NULL CONSTRAINT DF_SSAJ_Created DEFAULT (SYSUTCDATETIME()),
    UpdatedAt        DATETIME2(0) NULL
);

CREATE TABLE dbo.SchedulerSnapshotAutoSendRecipient (
    RecipientId      BIGINT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    JobId            INT NOT NULL,
    SourceType       NVARCHAR(20) NOT NULL,
    SourcePk         INT NOT NULL,
    DisplayName      NVARCHAR(200) NOT NULL,
    WhatsAppPrefix   NVARCHAR(200) NULL,
    WhatsAppLocal    NVARCHAR(32) NULL,
    IsActive         BIT NOT NULL CONSTRAINT DF_SSAR_Active DEFAULT (1),
    CreatedAt        DATETIME2(0) NOT NULL CONSTRAINT DF_SSAR_Created DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT FK_SSAR_Job FOREIGN KEY (JobId) REFERENCES dbo.SchedulerSnapshotAutoSendJob (JobId) ON DELETE CASCADE,
    CONSTRAINT CK_SSAR_SourceType CHECK (SourceType IN (N'Doctor', N'Employee', N'Secretary', N'Contact')),
    CONSTRAINT UQ_SSAR_Job_Source UNIQUE (JobId, SourceType, SourcePk)
);

CREATE INDEX IX_SSAR_Job ON dbo.SchedulerSnapshotAutoSendRecipient (JobId);

CREATE TABLE dbo.SchedulerSnapshotAutoSendLog (
    LogId           BIGINT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    JobId           INT NOT NULL,
    /* No FK to Recipient: avoids SQL Server multiple-CASCADE paths (Job deletes cascade to Log and Recipient). */
    RecipientId     BIGINT NULL,
    RunDate         DATE NOT NULL,
    Status          NVARCHAR(30) NOT NULL,
    StartedAt       DATETIME2(0) NOT NULL,
    CompletedAt     DATETIME2(0) NULL,
    ErrorMessage    NVARCHAR(1000) NULL,
    MediaPath       NVARCHAR(500) NULL,
    CONSTRAINT FK_SSAL_Job FOREIGN KEY (JobId) REFERENCES dbo.SchedulerSnapshotAutoSendJob (JobId) ON DELETE CASCADE
);

CREATE INDEX IX_SSAL_Job_RunDate ON dbo.SchedulerSnapshotAutoSendLog (JobId, RunDate DESC);
CREATE INDEX IX_SSAL_Status ON dbo.SchedulerSnapshotAutoSendLog (Status, StartedAt DESC);
CREATE INDEX IX_SSAL_RecipientId ON dbo.SchedulerSnapshotAutoSendLog (RecipientId) WHERE RecipientId IS NOT NULL;

GO
