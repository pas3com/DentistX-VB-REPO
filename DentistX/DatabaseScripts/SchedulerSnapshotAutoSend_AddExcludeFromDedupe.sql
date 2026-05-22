/*
  Scheduler snapshot auto-send: manual "Test send" should not block the same day's automatic send.

  Adds ExcludeFromDedupe on SchedulerSnapshotAutoSendLog:
    0 = counts toward daily dedupe (scheduled auto send success)
    1 = ignored by HasRecipientSentOnRunDate (test send from SnapShotSender)

  Run once per database that already has SchedulerSnapshotAutoSend.sql applied.
*/

SET NOCOUNT ON;

IF COL_LENGTH(N'dbo.SchedulerSnapshotAutoSendLog', N'ExcludeFromDedupe') IS NULL
BEGIN
    ALTER TABLE dbo.SchedulerSnapshotAutoSendLog
        ADD ExcludeFromDedupe BIT NOT NULL
            CONSTRAINT DF_SSAL_ExcludeFromDedupe DEFAULT (0);
END
GO
