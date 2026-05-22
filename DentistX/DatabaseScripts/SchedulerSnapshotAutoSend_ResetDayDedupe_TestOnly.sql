/*
  TEST / troubleshooting only — scheduler snapshot auto-send daily dedupe.

  Automatic send skips recipients when SchedulerSnapshotAutoSendLog has Status='Sent'
  for that job + calendar day with ExcludeFromDedupe = 0.

  Rows created BEFORE ExcludeFromDedupe was added default to 0, so an old "test" send
  still blocks auto until you clear or mark those rows.

  Pick ONE approach after editing JobId / RunDate:

  A) Delete today's log rows for this job (simplest for dev retest; loses audit for that day):

    DELETE FROM dbo.SchedulerSnapshotAutoSendLog
    WHERE JobId = 1 AND RunDate = '2026-05-04';

  B) Mark ALL Sent rows that day as test-only (allows another auto send THE SAME DAY —
     only use if every Sent row that day was a manual test, not a real scheduled send):

    UPDATE dbo.SchedulerSnapshotAutoSendLog
    SET ExcludeFromDedupe = 1
    WHERE JobId = 1 AND RunDate = '2026-05-04' AND Status = N'Sent';

  Required once per database (if not already applied):
    SchedulerSnapshotAutoSend_AddExcludeFromDedupe.sql
*/

SET NOCOUNT ON;
-- No default batch: uncomment ONE statement above and run manually.
