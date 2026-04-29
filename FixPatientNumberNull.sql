-- =============================================================================
-- Fix: Ensure PatientNumber is never NULL after insert
-- Run this script against your DentistX database
-- =============================================================================

USE [DentistX]
GO

-- Step 1: Fix any existing patients with NULL PatientNumber (assign YYNNNN format)
-- Run this only if you have existing NULL PatientNumbers
/*
DECLARE @CurrentYear CHAR(2) = RIGHT(CAST(YEAR(GETDATE()) AS VARCHAR(4)), 2);
DECLARE @Seq INT = 0;
DECLARE @PatientID INT;
DECLARE @NewNum NVARCHAR(10);

DECLARE cur CURSOR FOR 
    SELECT PatientID FROM Patient WHERE PatientNumber IS NULL ORDER BY PatientID;

OPEN cur;
FETCH NEXT FROM cur INTO @PatientID;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @Seq = @Seq + 1;
    SET @NewNum = @CurrentYear + RIGHT('0000' + CAST(@Seq AS NVARCHAR(10)), 4);
    UPDATE Patient SET PatientNumber = @NewNum WHERE PatientID = @PatientID;
    FETCH NEXT FROM cur INTO @PatientID;
END
CLOSE cur;
DEALLOCATE cur;
GO
*/

-- Step 2: Harden GetNextPatientNumberSP - ensure it never returns NULL
ALTER PROCEDURE [dbo].[GetNextPatientNumberSP]
    @NextPatientNumber NVARCHAR(10) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Initialize output to avoid NULL
    SET @NextPatientNumber = '';

    DECLARE @CurrentYear CHAR(2) = RIGHT(CAST(YEAR(GETDATE()) AS VARCHAR(4)), 2);
    DECLARE @MaxSequence INT;

    -- Get the maximum sequence number for current year
    SELECT @MaxSequence = ISNULL(MAX(CAST(RIGHT(PatientNumber, 4) AS INT)), 0)
    FROM Patient 
    WHERE PatientNumber IS NOT NULL
      AND PatientNumber LIKE @CurrentYear + '%'
      AND ISNUMERIC(RIGHT(PatientNumber, 4)) = 1
      AND LEN(PatientNumber) = 6;  -- Ensure format is YYNNNN

    -- If no patients found for current year, start from 1
    IF @MaxSequence IS NULL
        SET @MaxSequence = 0;

    -- Generate next number (guaranteed non-null)
    SET @NextPatientNumber = @CurrentYear + RIGHT('0000' + CAST((@MaxSequence + 1) AS NVARCHAR(10)), 4);

    -- Final safeguard: if still empty, use fallback
    IF @NextPatientNumber IS NULL OR LEN(LTRIM(RTRIM(@NextPatientNumber))) = 0
        SET @NextPatientNumber = @CurrentYear + '0001';
END
GO

-- Step 3: Harden PatientInsert - refuse to insert if PatientNumber would be NULL
ALTER PROC [dbo].[PatientInsert] 
    @PatientName NVARCHAR(50),
    @Sex NVARCHAR(10) = NULL,
    @Age INT = NULL,
    @Phone NVARCHAR(16) = NULL,
    @WhatsApp NVARCHAR(50) = NULL,
    @Address NVARCHAR(50) = NULL,
    @Health NVARCHAR(50) = NULL,
    @Treat BIT = NULL,
    @Implant BIT = NULL,
    @Mobile BIT = NULL,
    @Ortho BIT = NULL,
    @Diag BIT = NULL,
    @Struc BIT = NULL,
    @Notes NVARCHAR(150) = NULL,
    @BirthY INT = NULL,
    @CreatedBy INT = NULL,
    @CreateDate DATETIME = NULL,
    @ReturnValue INT OUTPUT
AS
BEGIN
    SET NOCOUNT OFF;
    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @PID INT = 0;
        DECLARE @Rows INT = 0;
        DECLARE @NextPatientNumber NVARCHAR(10) = '';

        -- Generate the next patient number safely
        EXEC dbo.GetNextPatientNumberSP @NextPatientNumber OUTPUT;

        -- CRITICAL: Never insert without a valid PatientNumber
        IF @NextPatientNumber IS NULL OR LEN(LTRIM(RTRIM(@NextPatientNumber))) = 0
        BEGIN
            SET @ReturnValue = -3;  -- PatientNumber generation failed
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Check if the PatientName already exists
        IF EXISTS (SELECT 1 FROM [dbo].[Patient] WHERE [PatientName] = @PatientName)
        BEGIN
            SET @ReturnValue = -2;  -- Duplicate name
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Insert into Patient table (with PatientNumber - guaranteed non-null)
        INSERT INTO [dbo].[Patient]
        ([PatientName], [PatientNumber], [Sex], [Age], [Phone], [WhatsApp], [Address], [Health],
         [Treat], [Implant], [Mobile], [Ortho], [Diag], [Struc], [Notes],
         [BirthY], [CreatedBy], [CreateDate])
        VALUES
        (@PatientName, @NextPatientNumber, @Sex, @Age, @Phone, @WhatsApp, @Address, @Health,
         @Treat, @Implant, @Mobile, @Ortho, @Diag, @Struc, @Notes,
         @BirthY, @CreatedBy, @CreateDate);

        SET @PID = SCOPE_IDENTITY();
        SET @Rows = @Rows + 1;

        -- Related inserts
        EXEC dbo.PatientColorsInsert '#D8BFD8','#DDA0DD',5,0,@PID; SET @Rows += 1;

        EXEC dbo.LDInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.LDInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.LDInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.LDInsert @PID,'','','','','','','',''; SET @Rows += 1;

        EXEC dbo.LUInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.LUInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.LUInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.LUInsert @PID,'','','','','','','',''; SET @Rows += 1;

        EXEC dbo.RDInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.RDInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.RDInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.RDInsert @PID,'','','','','','','',''; SET @Rows += 1;

        EXEC dbo.RUInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.RUInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.RUInsert @PID,'','','','','','','',''; SET @Rows += 1;
        EXEC dbo.RUInsert @PID,'','','','','','','',''; SET @Rows += 1;

        COMMIT TRANSACTION;

        SET @ReturnValue = @Rows;
        RETURN @ReturnValue;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @ReturnValue = -1;
        RETURN @ReturnValue;
    END CATCH
END;
GO

-- Step 4 (optional): Add NOT NULL constraint after fixing existing NULLs
-- Uncomment and run ONLY after Step 1 has been executed to fix existing NULLs
/*
ALTER TABLE [dbo].[Patient] ALTER COLUMN [PatientNumber] NVARCHAR(10) NOT NULL;
GO
ALTER TABLE [dbo].[Patient] ADD CONSTRAINT [DF_Patient_PatientNumber] DEFAULT (RIGHT(CAST(YEAR(GETDATE()) AS VARCHAR(4)), 2) + '0001') FOR [PatientNumber];
GO
*/
