/*
  Apply on databases that already ran an older WhatsAppOutbound.sql (before ReminderQueueId / WaGroup / NEWID).
  Safe to run multiple times (each block is guarded).
*/

-- CorrelationId default: prefer NEWID() for opaque correlation tokens.
IF OBJECT_ID(N'dbo.WhatsAppOutboundMessage', N'U') IS NOT NULL
BEGIN
    DECLARE @dc sysname =
    (
        SELECT dc.name
        FROM sys.default_constraints dc
        INNER JOIN sys.columns c
            ON c.object_id = dc.parent_object_id AND c.column_id = dc.parent_column_id
        WHERE dc.parent_object_id = OBJECT_ID(N'dbo.WhatsAppOutboundMessage', N'U')
          AND c.name = N'CorrelationId'
    );

    IF @dc IS NOT NULL
    BEGIN
        DECLARE @dropsql nvarchar(600) =
              N'ALTER TABLE dbo.WhatsAppOutboundMessage DROP CONSTRAINT '
            + QUOTENAME(@dc);
        EXEC (@dropsql);
    END;

    IF NOT EXISTS
    (
        SELECT 1
        FROM sys.default_constraints dc
        INNER JOIN sys.columns c
            ON c.object_id = dc.parent_object_id AND c.column_id = dc.parent_column_id
        WHERE dc.parent_object_id = OBJECT_ID(N'dbo.WhatsAppOutboundMessage', N'U')
          AND c.name = N'CorrelationId'
    )
    BEGIN
        ALTER TABLE dbo.WhatsAppOutboundMessage
            ADD CONSTRAINT DF_WhatsAppOutboundMessage_CorrelationId DEFAULT (NEWID()) FOR CorrelationId;
    END
END;
GO

IF COL_LENGTH(N'dbo.WhatsAppOutboundMessage', N'ReminderQueueId') IS NULL
    ALTER TABLE dbo.WhatsAppOutboundMessage ADD ReminderQueueId BIGINT NULL;
GO

IF COL_LENGTH(N'dbo.WhatsAppOutboundMessage', N'WaGroupKey') IS NULL
    ALTER TABLE dbo.WhatsAppOutboundMessage ADD WaGroupKey NVARCHAR (64) NULL;
GO

IF COL_LENGTH(N'dbo.WhatsAppOutboundMessage', N'WaPartIndex') IS NULL
    ALTER TABLE dbo.WhatsAppOutboundMessage ADD WaPartIndex TINYINT NULL;
GO

IF COL_LENGTH(N'dbo.WhatsAppOutboundMessage', N'WaPartTotal') IS NULL
    ALTER TABLE dbo.WhatsAppOutboundMessage ADD WaPartTotal TINYINT NULL;
GO

IF COL_LENGTH(N'dbo.WhatsAppOutboundMessage', N'WaSchedulerMeta') IS NULL
    ALTER TABLE dbo.WhatsAppOutboundMessage ADD WaSchedulerMeta NVARCHAR (MAX) NULL;
GO

IF COL_LENGTH(N'dbo.WhatsAppOutboundMessage', N'RevealMessageCenter') IS NULL
BEGIN
    ALTER TABLE dbo.WhatsAppOutboundMessage
        ADD RevealMessageCenter BIT NOT NULL
            CONSTRAINT DF_WhatsAppOutboundMessage_RevealMC DEFAULT (0);
END;
GO

IF COL_LENGTH(N'dbo.WhatsAppOutboundMessage', N'CallerDisplayName') IS NULL
    ALTER TABLE dbo.WhatsAppOutboundMessage ADD CallerDisplayName NVARCHAR (200) NULL;
GO

IF OBJECT_ID(N'dbo.WhatsAppOutboundMessage', N'U') IS NOT NULL
   AND NOT EXISTS
   (
       SELECT 1
       FROM sys.indexes
       WHERE name = N'IX_WhatsAppOutboundMessage_WaGroupKey'
         AND object_id = OBJECT_ID(N'dbo.WhatsAppOutboundMessage', N'U')
   )
BEGIN
    CREATE NONCLUSTERED INDEX IX_WhatsAppOutboundMessage_WaGroupKey
        ON dbo.WhatsAppOutboundMessage (WaGroupKey ASC)
        WHERE WaGroupKey IS NOT NULL;
END;
GO
