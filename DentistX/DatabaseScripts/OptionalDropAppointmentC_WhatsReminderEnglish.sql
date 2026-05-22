-- Optional: remove column if you already ran the old migration and want the table to match the app (no longer used).

IF COL_LENGTH(N'dbo.AppointmentC', N'WhatsReminderEnglish') IS NOT NULL
BEGIN
    ALTER TABLE dbo.AppointmentC DROP COLUMN WhatsReminderEnglish;
END
GO
