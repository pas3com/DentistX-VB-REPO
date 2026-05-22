/*
  DentistX — dedupe implant lookup tables and enforce uniqueness.

  Run once against your clinic database (backup first).

  - Builds keeper maps (lowest ID per trimmed TypeName / BrandName).
  - Remaps FOREIGN KEY columns on ImplantVariation, ImplantMeasure (TypeID / BrandID when present), then deletes duplicates.
  - Deletes duplicate ImplantDiameter / ImplantLength rows (verify no other FKs to those IDs in your DB).
  - Creates unique indexes where missing.

  NOTE: If dbo.ImplantType has CHECK constraint limiting TypeName values, adjust separately.

  If you already partially ran an older script: restore from backup or fix orphaned FKs manually before re-running.
*/

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRANSACTION;

-------------------------------------------------------------------------------
-- ImplantType: temp map -> remap dependents -> delete duplicates
-------------------------------------------------------------------------------
IF OBJECT_ID(N'tempdb..#ImplantTypeDedupe') IS NOT NULL
    DROP TABLE #ImplantTypeDedupe;

SELECT TypeID,
       MIN(TypeID) OVER (PARTITION BY LTRIM(RTRIM(TypeName))) AS KeeperID
INTO #ImplantTypeDedupe
FROM dbo.ImplantType;

IF OBJECT_ID(N'dbo.ImplantVariation', N'U') IS NOT NULL
BEGIN
    UPDATE iv
    SET iv.TypeID = m.KeeperID
    FROM dbo.ImplantVariation AS iv
    INNER JOIN #ImplantTypeDedupe AS m ON iv.TypeID = m.TypeID
    WHERE m.TypeID <> m.KeeperID;
END;

IF OBJECT_ID(N'dbo.ImplantMeasure', N'U') IS NOT NULL
   AND COL_LENGTH(N'dbo.ImplantMeasure', N'TypeID') IS NOT NULL
BEGIN
    UPDATE im
    SET im.TypeID = m.KeeperID
    FROM dbo.ImplantMeasure AS im
    INNER JOIN #ImplantTypeDedupe AS m ON im.TypeID = m.TypeID
    WHERE m.TypeID <> m.KeeperID;
END;

DELETE t
FROM dbo.ImplantType AS t
INNER JOIN #ImplantTypeDedupe AS m ON t.TypeID = m.TypeID
WHERE m.TypeID <> m.KeeperID;

DROP TABLE #ImplantTypeDedupe;

-------------------------------------------------------------------------------
-- ImplantBrand: temp map -> remap dependents -> delete duplicates
-------------------------------------------------------------------------------
IF OBJECT_ID(N'tempdb..#ImplantBrandDedupe') IS NOT NULL
    DROP TABLE #ImplantBrandDedupe;

SELECT BrandID,
       MIN(BrandID) OVER (PARTITION BY LTRIM(RTRIM(BrandName))) AS KeeperID
INTO #ImplantBrandDedupe
FROM dbo.ImplantBrand;

IF OBJECT_ID(N'dbo.ImplantVariation', N'U') IS NOT NULL
BEGIN
    UPDATE iv
    SET iv.BrandID = m.KeeperID
    FROM dbo.ImplantVariation AS iv
    INNER JOIN #ImplantBrandDedupe AS m ON iv.BrandID = m.BrandID
    WHERE m.BrandID <> m.KeeperID;
END;

IF OBJECT_ID(N'dbo.ImplantMeasure', N'U') IS NOT NULL
   AND COL_LENGTH(N'dbo.ImplantMeasure', N'BrandID') IS NOT NULL
BEGIN
    UPDATE im
    SET im.BrandID = m.KeeperID
    FROM dbo.ImplantMeasure AS im
    INNER JOIN #ImplantBrandDedupe AS m ON im.BrandID = m.BrandID
    WHERE m.BrandID <> m.KeeperID;
END;

DELETE b
FROM dbo.ImplantBrand AS b
INNER JOIN #ImplantBrandDedupe AS m ON b.BrandID = m.BrandID
WHERE m.BrandID <> m.KeeperID;

DROP TABLE #ImplantBrandDedupe;

-------------------------------------------------------------------------------
-- ImplantDiameter / ImplantLength (same numeric MM); assumes no FK to these IDs
-------------------------------------------------------------------------------
IF OBJECT_ID(N'dbo.ImplantDiameter', N'U') IS NOT NULL
BEGIN
    ;WITH d AS (
        SELECT DiameterID,
               ROW_NUMBER() OVER (PARTITION BY DiameterMM ORDER BY DiameterID) AS rn
        FROM dbo.ImplantDiameter
    )
    DELETE FROM dbo.ImplantDiameter
    WHERE DiameterID IN (SELECT DiameterID FROM d WHERE rn > 1);
END;

IF OBJECT_ID(N'dbo.ImplantLength', N'U') IS NOT NULL
BEGIN
    ;WITH d AS (
        SELECT LengthID,
               ROW_NUMBER() OVER (PARTITION BY LengthMM ORDER BY LengthID) AS rn
        FROM dbo.ImplantLength
    )
    DELETE FROM dbo.ImplantLength
    WHERE LengthID IN (SELECT LengthID FROM d WHERE rn > 1);
END;

-------------------------------------------------------------------------------
-- Unique indexes (skip if a unique index already includes that column as key)
-------------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes AS i
    INNER JOIN sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    INNER JOIN sys.columns AS c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
    WHERE i.object_id = OBJECT_ID(N'dbo.ImplantType')
      AND i.is_unique = 1
      AND c.name = N'TypeName'
      AND ic.key_ordinal > 0
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UQ_ImplantType_TypeName ON dbo.ImplantType (TypeName);
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes AS i
    INNER JOIN sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    INNER JOIN sys.columns AS c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
    WHERE i.object_id = OBJECT_ID(N'dbo.ImplantBrand')
      AND i.is_unique = 1
      AND c.name = N'BrandName'
      AND ic.key_ordinal > 0
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UQ_ImplantBrand_BrandName ON dbo.ImplantBrand (BrandName);
END;

IF OBJECT_ID(N'dbo.ImplantDiameter', N'U') IS NOT NULL
   AND NOT EXISTS (
    SELECT 1
    FROM sys.indexes AS i
    INNER JOIN sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    INNER JOIN sys.columns AS c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
    WHERE i.object_id = OBJECT_ID(N'dbo.ImplantDiameter')
      AND i.is_unique = 1
      AND c.name = N'DiameterMM'
      AND ic.key_ordinal > 0
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UQ_ImplantDiameter_DiameterMM ON dbo.ImplantDiameter (DiameterMM);
END;

IF OBJECT_ID(N'dbo.ImplantLength', N'U') IS NOT NULL
   AND NOT EXISTS (
    SELECT 1
    FROM sys.indexes AS i
    INNER JOIN sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    INNER JOIN sys.columns AS c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
    WHERE i.object_id = OBJECT_ID(N'dbo.ImplantLength')
      AND i.is_unique = 1
      AND c.name = N'LengthMM'
      AND ic.key_ordinal > 0
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UQ_ImplantLength_LengthMM ON dbo.ImplantLength (LengthMM);
END;

COMMIT TRANSACTION;

PRINT N'DedupeImplantReferenceTables finished successfully.';
