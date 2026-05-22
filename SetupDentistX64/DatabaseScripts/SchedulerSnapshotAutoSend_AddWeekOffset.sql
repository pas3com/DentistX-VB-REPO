/*
  Drops legacy WeekOffset column if it exists (older builds). New behavior: each run sends
  two PNGs (current Sat–Fri week and next week). Safe to run multiple times.
*/
SET NOCOUNT ON;

IF COL_LENGTH(N'dbo.SchedulerSnapshotAutoSendJob', N'WeekOffset') IS NULL
BEGIN
    RETURN;
END

DECLARE @sql nvarchar(max);

IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.SchedulerSnapshotAutoSendJob') AND name = N'CK_SSAJ_WeekOffset')
BEGIN
    ALTER TABLE dbo.SchedulerSnapshotAutoSendJob DROP CONSTRAINT CK_SSAJ_WeekOffset;
END

SELECT @sql = N'ALTER TABLE dbo.SchedulerSnapshotAutoSendJob DROP CONSTRAINT ' + QUOTENAME(dc.name)
FROM sys.default_constraints dc
INNER JOIN sys.columns AS c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
WHERE dc.parent_object_id = OBJECT_ID(N'dbo.SchedulerSnapshotAutoSendJob') AND c.name = N'WeekOffset';

IF @sql IS NOT NULL
    EXEC sp_executesql @sql;

ALTER TABLE dbo.SchedulerSnapshotAutoSendJob DROP COLUMN WeekOffset;
GO
