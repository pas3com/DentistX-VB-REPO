/*
  Inserts 120 sample rows into dbo.AppointmentC for the week that contains "today":
    Saturday through Thursday (6 days), 20 appointments per day, 30-minute slots from 08:00 (20 slots; last start 17:30).
  Patients: mapped evenly over existing PatientID rows (by ordered row number; works with non-contiguous ids).
  Doctors: Cycles 1,2,3,4,6,7,8,10,11,12 (10 doctors).

  "This week" = the Saturday of the 7-day block containing GETDATE() (ref Saturday = 2000-01-01; independent of @@DATEFIRST).

  Requires: Status, WhatsIncludeReason, WhatsIncludeNotes on dbo.AppointmentC (current app; see AppointmentCRepository).
  If your table is missing them, add those columns or remove them from the INSERT and rely on your defaults.

  Seeded rows are marked CreatedBy = N'Seed:ApptC120' (delete with the commented block at the bottom if needed).
*/
SET NOCOUNT ON;

DECLARE @RefSat  DATE    = '2000-01-01';  /* must be a Saturday (calendars agree this one is) */
DECLARE @Today   DATE    = CAST(GETDATE() AS DATE);
DECLARE @WeekStartSat DATE = DATEADD(DAY, -(DATEDIFF(DAY, @RefSat, @Today) % 7), @Today);

DECLARE @PatientCount INT;
SELECT @PatientCount = COUNT(*) FROM dbo.Patient;
IF @PatientCount < 1
BEGIN
    RAISERROR('No rows in dbo.Patient; cannot seed appointments.', 16, 1);
    RETURN;
END;

DECLARE @ApptCount INT  = 120;
DECLARE @DurMin   INT  = 30;   /* duration start → end */
DECLARE @PerDay   INT  = 20;   /* 120 / 6 days */

;WITH
Tally AS (
    SELECT TOP (@ApptCount)
        ROW_NUMBER() OVER (ORDER BY s.object_id) - 1 AS n
    FROM sys.all_objects s
    CROSS JOIN sys.all_objects s2
),
Pnum AS (
    /* r = 1..@PatientCount in stable PatientID order */
    SELECT PatientID, ROW_NUMBER() OVER (ORDER BY PatientID) AS r
    FROM dbo.Patient
),
Drs AS (
    SELECT * FROM (VALUES
        (1, 0), (2, 1), (3, 2), (4, 3), (6, 4), (7, 5), (8, 6), (10, 7), (11, 8), (12, 9)
    ) d(DrID, ord)
),
Reasons AS (
    SELECT * FROM (VALUES
        (0, N'متابعة'), (1, N'فحص'), (2, N'تنظيف'), (3, N'تقويم'),
        (4, N'حشو'), (5, N'خلع'), (6, N'تلبيس'), (7, N'ألم'), (8, N'تجميل')
    ) r(ord, txt)
)
INSERT INTO dbo.AppointmentC (
    PatientID,
    DrID,
    AppDate,
    StartDateTime,
    EndDateTime,
    Reason,
    Notes,
    Status,
    CreatedBy,
    CreatedAt,
    WhatsIncludeReason,
    WhatsIncludeNotes
)
SELECT
    pt.PatientID,
    dr.DrID,
    CAST(ad.d AS SMALLDATETIME)                 AS AppDate,
    s0.tStart,
    s1.tEnd,
    rs.txt,
    NULL                                       AS Notes,
    N'Pending'                                 AS Status,
    N'Seed:ApptC120'                            AS CreatedBy,
    SYSDATETIME()                              AS CreatedAt,
    1,
    1
FROM Tally AS t
INNER JOIN Pnum  AS pt ON pt.r = 1 + (t.n % @PatientCount)
CROSS APPLY (SELECT d.DrID FROM Drs d WHERE d.ord = t.n % 10) dr
CROSS APPLY (SELECT r.txt FROM Reasons r WHERE r.ord = t.n % 9) rs
CROSS APPLY (
    /* Day within Sat..Thu: n = 0..19 → first day, 20..39 → next, ... */
    SELECT DATEADD(DAY, t.n / @PerDay, @WeekStartSat) AS d
) ad
CROSS APPLY (
    /* 08:00 + 0,30,…,570 min (00..19) */
    SELECT
        DATEADD(
            MINUTE,
            30 * (t.n % @PerDay),
            DATETIME2FROMPARTS(
                YEAR(ad.d), MONTH(ad.d), DAY(ad.d), 8, 0, 0, 0, 0
            )
        ) AS tStart
) s0
CROSS APPLY (SELECT DATEADD(MINUTE, @DurMin, s0.tStart) AS tEnd) s1;

-- Verify: 6 days × 20 = 120; 20 appts / day; doctor mix ~12 each if perfect cycle.
-- SELECT CAST(StartDateTime AS DATE) d, COUNT(*) c FROM dbo.AppointmentC WHERE CreatedBy = N'Seed:ApptC120' GROUP BY CAST(StartDateTime AS DATE) ORDER BY d;
-- SELECT DrID, COUNT(*) c FROM dbo.AppointmentC WHERE CreatedBy = N'Seed:ApptC120' GROUP BY DrID;

/*
DELETE FROM dbo.AppointmentC WHERE CreatedBy = N'Seed:ApptC120';
*/
