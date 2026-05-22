/*
================================================================================
Backfill OrthoID on OrthoTreat and OrthoTrtDet from OrthoInf (legacy NULL / 0)
================================================================================
Context
  - Legacy rows often have only PatientID; OrthoID was left NULL (one-episode model).
  - OrthoInf is the canonical source of OrthoID.

Prerequisites
  - dbo.OrthoInf (OrthoID, PatientID). TreatDate column improves multi-episode matching.
  - dbo.OrthoTreat and dbo.OrthoTrtDet must have an OrthoID column (nullable int).

Rules
  A) Exactly one OrthoInf per PatientID
       -> All orphan OrthoTreat / OrthoTrtDet for that patient get that OrthoID.

  B) Multiple OrthoInf per PatientID
       - OrthoTreat: OrthoInf row with smallest distance between TreatDate and BeginDate;
         NULL dates sort with a small penalty then OrthoID tie-break.
       - OrthoTrtDet: prefer OrthoTreat whose BeginDate is closest to WorkDate (uses
         OrthoTreat.OrthoID already set in steps 1–2). Remaining orphans: same
         distance logic against OrthoInf.TreatDate vs WorkDate.

  Rows with OrthoID already set (non-zero) are not modified.

Usage
  1) Run PREVIEW only (section below) on a copy or read-only — inspect counts.
  2) Set @Commit = 1 only when ready; keep 0 for dry run (rolls back).
  3) Backup production before update.
  4) After the updates, a final report runs (summary + detail) inside the same
     transaction so you still see post-update orphans when @Commit = 0.

Optional: OrthoDiag.OrthoID
  If your OrthoDiag table has OrthoID and legacy NULLs, run the OPTIONAL block
  at the bottom (uncomment / adjust after reviewing).
================================================================================
*/

SET NOCOUNT ON;
SET XACT_ABORT ON;

/* ------------- CONFIG ------------- */
DECLARE @Commit bit = 1;  /* 0 = rollback (safe trial). 1 = commit. */
/* ---------------------------------- */

/* =========================  PREVIEW (run alone; no transaction)  =========================
-- Orphan OrthoTreat rows
SELECT COUNT(*) AS OrphanOrthoTreatRows
FROM   dbo.OrthoTreat t
WHERE  ISNULL(t.OrthoID, 0) = 0;

-- Orphan OrthoTrtDet rows
SELECT COUNT(*) AS OrphanOrthoTrtDetRows
FROM   dbo.OrthoTrtDet d
WHERE  ISNULL(d.OrthoID, 0) = 0;

-- Patients with orphan treats but no OrthoInf (cannot auto-fix)
SELECT t.PatientID, COUNT(*) AS OrphanTreatRows
FROM   dbo.OrthoTreat t
WHERE  ISNULL(t.OrthoID, 0) = 0
       AND NOT EXISTS (SELECT 1 FROM dbo.OrthoInf i WHERE i.PatientID = t.PatientID)
GROUP BY t.PatientID;

-- Patients with multiple OrthoInf (heuristic will be used)
SELECT i.PatientID, COUNT(*) AS OrthoInfRows
FROM   dbo.OrthoInf i
GROUP BY i.PatientID
HAVING COUNT(*) > 1;
=============================================================================================== */

BEGIN TRANSACTION trOrthoBackfill;

BEGIN TRY
    /* -------------------------------------------------------------------------
       1) OrthoTreat — single OrthoInf per patient
       ------------------------------------------------------------------------- */
    ;WITH inf_one AS (
        SELECT  PatientID,
                MIN(OrthoID) AS OrthoID
        FROM    dbo.OrthoInf
        GROUP BY PatientID
        HAVING  COUNT(*) = 1
    )
    UPDATE  t
    SET     OrthoID = o.OrthoID
    FROM    dbo.OrthoTreat AS t
    INNER JOIN inf_one AS o ON o.PatientID = t.PatientID
    WHERE   ISNULL(t.OrthoID, 0) = 0;

    /* -------------------------------------------------------------------------
       2) OrthoTreat — multiple OrthoInf: nearest TreatDate to BeginDate
       ------------------------------------------------------------------------- */
    ;WITH ranked AS (
        SELECT  t.TreatID,
                t.PatientID,
                inf.OrthoID,
                ROW_NUMBER() OVER (
                    PARTITION BY t.TreatID, t.PatientID
                    ORDER BY
                        CASE
                            WHEN inf.TreatDate IS NULL AND t.BeginDate IS NULL THEN 0
                            WHEN inf.TreatDate IS NULL OR t.BeginDate IS NULL THEN 100000
                            ELSE ABS(DATEDIFF(DAY, inf.TreatDate, t.BeginDate))
                        END ASC,
                        inf.OrthoID ASC
                ) AS rn
        FROM    dbo.OrthoTreat AS t
        INNER JOIN dbo.OrthoInf AS inf ON inf.PatientID = t.PatientID
        INNER JOIN (
            SELECT PatientID
            FROM   dbo.OrthoInf
            GROUP BY PatientID
            HAVING COUNT(*) > 1
        ) AS multi ON multi.PatientID = t.PatientID
        WHERE   ISNULL(t.OrthoID, 0) = 0
    )
    UPDATE  t
    SET     OrthoID = r.OrthoID
    FROM    dbo.OrthoTreat AS t
    INNER JOIN ranked AS r
            ON r.TreatID = t.TreatID
           AND r.PatientID = t.PatientID
           AND r.rn = 1
    WHERE   ISNULL(t.OrthoID, 0) = 0;

    /* -------------------------------------------------------------------------
       3) OrthoTrtDet — single OrthoInf per patient
       ------------------------------------------------------------------------- */
    ;WITH inf_one AS (
        SELECT  PatientID,
                MIN(OrthoID) AS OrthoID
        FROM    dbo.OrthoInf
        GROUP BY PatientID
        HAVING  COUNT(*) = 1
    )
    UPDATE  d
    SET     OrthoID = o.OrthoID
    FROM    dbo.OrthoTrtDet AS d
    INNER JOIN inf_one AS o ON o.PatientID = d.PatientID
    WHERE   ISNULL(d.OrthoID, 0) = 0;

    /* -------------------------------------------------------------------------
       4) OrthoTrtDet — match to OrthoTreat: closest WorkDate to BeginDate
       ------------------------------------------------------------------------- */
    ;WITH ranked AS (
        SELECT  d.DetID,
                d.PatientID,
                tr.OrthoID,
                ROW_NUMBER() OVER (
                    PARTITION BY d.DetID, d.PatientID
                    ORDER BY
                        CASE
                            WHEN d.WorkDate IS NULL OR tr.BeginDate IS NULL THEN 100000
                            ELSE ABS(DATEDIFF(DAY, d.WorkDate, tr.BeginDate))
                        END ASC,
                        tr.TreatID ASC
                ) AS rn
        FROM    dbo.OrthoTrtDet AS d
        INNER JOIN dbo.OrthoTreat AS tr ON tr.PatientID = d.PatientID
        WHERE   ISNULL(d.OrthoID, 0) = 0
                AND ISNULL(tr.OrthoID, 0) <> 0
    )
    UPDATE  d
    SET     OrthoID = r.OrthoID
    FROM    dbo.OrthoTrtDet AS d
    INNER JOIN ranked AS r
            ON r.DetID = d.DetID
           AND r.PatientID = d.PatientID
           AND r.rn = 1
    WHERE   ISNULL(d.OrthoID, 0) = 0;

    /* -------------------------------------------------------------------------
       5) OrthoTrtDet — still orphan: map WorkDate to OrthoInf.TreatDate
          (covers no usable OrthoTreat.OrthoID yet, or multi-episode edge cases)
       ------------------------------------------------------------------------- */
    ;WITH ranked AS (
        SELECT  d.DetID,
                d.PatientID,
                inf.OrthoID,
                ROW_NUMBER() OVER (
                    PARTITION BY d.DetID, d.PatientID
                    ORDER BY
                        CASE
                            WHEN d.WorkDate IS NULL AND inf.TreatDate IS NULL THEN 0
                            WHEN d.WorkDate IS NULL OR inf.TreatDate IS NULL THEN 100000
                            ELSE ABS(DATEDIFF(DAY, d.WorkDate, inf.TreatDate))
                        END ASC,
                        inf.OrthoID ASC
                ) AS rn
        FROM    dbo.OrthoTrtDet AS d
        INNER JOIN dbo.OrthoInf AS inf ON inf.PatientID = d.PatientID
        WHERE   ISNULL(d.OrthoID, 0) = 0
    )
    UPDATE  d
    SET     OrthoID = r.OrthoID
    FROM    dbo.OrthoTrtDet AS d
    INNER JOIN ranked AS r
            ON r.DetID = d.DetID
           AND r.PatientID = d.PatientID
           AND r.rn = 1
    WHERE   ISNULL(d.OrthoID, 0) = 0;



    ;WITH inf_one AS (
    SELECT  PatientID, MIN(OrthoID) AS OrthoID
    FROM    dbo.OrthoInf
    GROUP BY PatientID
    HAVING COUNT(*) = 1
)
UPDATE  dg
SET     OrthoID = o.OrthoID
FROM    dbo.OrthoDiag AS dg
INNER JOIN inf_one AS o ON o.PatientID = dg.PatientID
WHERE   ISNULL(dg.OrthoID, 0) = 0;
/*================================================================================
OPTIONAL — OrthoDiag.OrthoID (uncomment if column exists and needs backfill)
================================================================================*/

;WITH ranked AS (
    SELECT  dg.DiagID,
            dg.PatientID,
            inf.OrthoID,
            ROW_NUMBER() OVER (
                PARTITION BY dg.DiagID, dg.PatientID
                ORDER BY inf.OrthoID ASC
            ) AS rn
    FROM    dbo.OrthoDiag AS dg
    INNER JOIN dbo.OrthoInf AS inf ON inf.PatientID = dg.PatientID
    INNER JOIN (
        SELECT PatientID FROM dbo.OrthoInf GROUP BY PatientID HAVING COUNT(*) > 1
    ) AS m ON m.PatientID = dg.PatientID
    WHERE   ISNULL(dg.OrthoID, 0) = 0
)
UPDATE  dg
SET     OrthoID = r.OrthoID
FROM    dbo.OrthoDiag AS dg
INNER JOIN ranked AS r
        ON r.DiagID = dg.DiagID AND r.PatientID = dg.PatientID AND r.rn = 1
WHERE   ISNULL(dg.OrthoID, 0) = 0;

    /* -------------------------------------------------------------------------
       Final report — rows still missing OrthoID (NULL / 0)
       Runs inside this transaction so results reflect updates even when @Commit = 0.
       ------------------------------------------------------------------------- */
    PRINT N'';
    PRINT N'========== FINAL REPORT (after backfill steps) ==========';

    SELECT  (SELECT COUNT(*) FROM dbo.OrthoTreat   WHERE ISNULL(OrthoID, 0) = 0) AS OrphanOrthoTreatRows,
            (SELECT COUNT(*) FROM dbo.OrthoTrtDet WHERE ISNULL(OrthoID, 0) = 0) AS OrphanOrthoTrtDetRows;

    SELECT  v.SourceTable,
            v.RowId,
            v.PatientID,
            v.RefDate,
            v.OrthoID AS OrthoID_Current,
            v.HasOrthoInfForPatient,
            v.OrthoInfRowCount
    FROM    (
                SELECT  N'OrthoTreat' AS SourceTable,
                        t.TreatID AS RowId,
                        t.PatientID,
                        t.BeginDate AS RefDate,
                        t.OrthoID,
                        CASE WHEN EXISTS (SELECT 1 FROM dbo.OrthoInf i WHERE i.PatientID = t.PatientID) THEN N'Yes' ELSE N'No' END AS HasOrthoInfForPatient,
                        (SELECT COUNT(*) FROM dbo.OrthoInf i2 WHERE i2.PatientID = t.PatientID) AS OrthoInfRowCount
                FROM    dbo.OrthoTreat AS t
                WHERE   ISNULL(t.OrthoID, 0) = 0
                UNION ALL
                SELECT  N'OrthoTrtDet',
                        d.DetID,
                        d.PatientID,
                        d.WorkDate,
                        d.OrthoID,
                        CASE WHEN EXISTS (SELECT 1 FROM dbo.OrthoInf i WHERE i.PatientID = d.PatientID) THEN N'Yes' ELSE N'No' END,
                        (SELECT COUNT(*) FROM dbo.OrthoInf i2 WHERE i2.PatientID = d.PatientID)
                FROM    dbo.OrthoTrtDet AS d
                WHERE   ISNULL(d.OrthoID, 0) = 0
            ) AS v
    ORDER BY v.SourceTable, v.PatientID, v.RowId;

    PRINT N'========== END REPORT — next: ' + CASE WHEN @Commit = 1 THEN N'COMMIT' ELSE N'ROLLBACK (dry run)' END + N' ==========';
    PRINT N'';

    IF @Commit = 1
        COMMIT TRANSACTION trOrthoBackfill;
    ELSE
        ROLLBACK TRANSACTION trOrthoBackfill;

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION trOrthoBackfill;
    THROW;
END CATCH;
GO

/*
================================================================================
OPTIONAL — OrthoDiag.OrthoID (uncomment if column exists and needs backfill)
================================================================================
BEGIN TRANSACTION;

;WITH inf_one AS (
    SELECT  PatientID, MIN(OrthoID) AS OrthoID
    FROM    dbo.OrthoInf
    GROUP BY PatientID
    HAVING COUNT(*) = 1
)
UPDATE  dg
SET     OrthoID = o.OrthoID
FROM    dbo.OrthoDiag AS dg
INNER JOIN inf_one AS o ON o.PatientID = dg.PatientID
WHERE   ISNULL(dg.OrthoID, 0) = 0;

;WITH ranked AS (
    SELECT  dg.DiagID,
            dg.PatientID,
            inf.OrthoID,
            ROW_NUMBER() OVER (
                PARTITION BY dg.DiagID, dg.PatientID
                ORDER BY inf.OrthoID ASC
            ) AS rn
    FROM    dbo.OrthoDiag AS dg
    INNER JOIN dbo.OrthoInf AS inf ON inf.PatientID = dg.PatientID
    INNER JOIN (
        SELECT PatientID FROM dbo.OrthoInf GROUP BY PatientID HAVING COUNT(*) > 1
    ) AS m ON m.PatientID = dg.PatientID
    WHERE   ISNULL(dg.OrthoID, 0) = 0
)
UPDATE  dg
SET     OrthoID = r.OrthoID
FROM    dbo.OrthoDiag AS dg
INNER JOIN ranked AS r
        ON r.DiagID = dg.DiagID AND r.PatientID = dg.PatientID AND r.rn = 1
WHERE   ISNULL(dg.OrthoID, 0) = 0;

-- COMMIT or ROLLBACK as appropriate
================================================================================
*/
