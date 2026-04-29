-- Persists "include reason / include notes in WhatsApp" for manual + queued (24h / short-lead) reminder text.
-- Safe to run once; skips if columns already exist.

IF COL_LENGTH('dbo.AppointmentC', 'WhatsIncludeReason') IS NULL
BEGIN
    ALTER TABLE dbo.AppointmentC ADD
        WhatsIncludeReason BIT NOT NULL CONSTRAINT DF_AppointmentC_WhatsIncludeReason DEFAULT (1);
END
GO

IF COL_LENGTH('dbo.AppointmentC', 'WhatsIncludeNotes') IS NULL
BEGIN
    ALTER TABLE dbo.AppointmentC ADD
        WhatsIncludeNotes BIT NOT NULL CONSTRAINT DF_AppointmentC_WhatsIncludeNotes DEFAULT (1);
END
GO
