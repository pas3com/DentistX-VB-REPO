/*
  Second daily send time + per-slot dedupe for scheduler snapshot auto-send.

  - SchedulerSnapshotAutoSendJob.SendTimeLocal2: optional TIME; NULL = only first send time is used.
  - SchedulerSnapshotAutoSendLog.SendSlot: 1 = first scheduled time that day, 2 = second (dedupe is per slot).

  Run once per database after SchedulerSnapshotAutoSend.sql / existing migrations.
*/

SET NOCOUNT ON;

IF COL_LENGTH(N'dbo.SchedulerSnapshotAutoSendJob', N'SendTimeLocal2') IS NULL
BEGIN
    ALTER TABLE dbo.SchedulerSnapshotAutoSendJob
        ADD SendTimeLocal2 TIME(0) NULL;
END

IF COL_LENGTH(N'dbo.SchedulerSnapshotAutoSendLog', N'SendSlot') IS NULL
BEGIN
    ALTER TABLE dbo.SchedulerSnapshotAutoSendLog
        ADD SendSlot TINYINT NOT NULL
            CONSTRAINT DF_SSAL_SendSlot DEFAULT (1);
END

GO
