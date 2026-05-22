-- Adds Patient_Pays.IsReturned when missing (cheque bounced / returned; excluded from balance when set).
IF NOT EXISTS (
    SELECT 1 FROM sys.columns c
    INNER JOIN sys.tables t ON c.object_id = t.object_id
    WHERE t.name = N'Patient_Pays' AND c.name = N'IsReturned' AND SCHEMA_NAME(t.schema_id) = N'dbo')
BEGIN
    ALTER TABLE [dbo].[Patient_Pays] ADD [IsReturned] BIT NULL;
END
GO
