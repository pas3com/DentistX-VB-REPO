/*
  Adds SendContentMode to existing SchedulerSnapshotAutoSendJob (for databases created before this column).
  0 = PNG only, 1 = HTML only, 2 = PNG and HTML.
  Run once per database. Safe to re-run: skips if the column already exists.
*/

SET NOCOUNT ON;

IF COL_LENGTH('dbo.SchedulerSnapshotAutoSendJob', 'SendContentMode') IS NULL
BEGIN
    ALTER TABLE dbo.SchedulerSnapshotAutoSendJob
    ADD SendContentMode TINYINT NOT NULL
        CONSTRAINT DF_SSAJ_SendContentMode DEFAULT (0);
END
GO
