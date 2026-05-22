-- PatientBalance: totals and balance for one patient.
-- TotalPayments = SUM(PayValue) EXCEPT rows that are returned CHEQUES:
--   IsReturned = 1 AND PayType is English "Cheque" (case-insensitive) OR Arabic "شيك".
-- (Cash / insurance rows are never excluded by IsReturned in this app.)
-- The Windows app uses the same rule in Patient_TrtsDATA.GetPatientBalance (inline SQL);
-- deploy this proc only if other tools/reports must call EXEC PatientBalance.

IF OBJECT_ID(N'dbo.PatientBalance', N'P') IS NOT NULL
    DROP PROCEDURE dbo.PatientBalance;
GO

CREATE PROCEDURE [dbo].[PatientBalance]
 @PatientID INT
AS
BEGIN
    ;WITH TreatmentTotal AS (
             SELECT [PatientID],
                    SUM([TrtValue]) AS TotalTreatments
             FROM   [dbo].[Patient_Trts]
             WHERE  [PatientID] = @PatientID
             GROUP BY
                    [PatientID]
         ),
         PaymentTotal AS (
             SELECT [PatientID],
                    SUM([PayValue]) AS TotalPayments
             FROM   [dbo].[Patient_Pays]
             WHERE  [PatientID] = @PatientID
               AND  NOT (
                        ISNULL([IsReturned], 0) = 1
                    AND (
                            LOWER(LTRIM(RTRIM(ISNULL([PayType], N'')))) = N'cheque'
                         OR LTRIM(RTRIM(ISNULL([PayType], N''))) = N'شيك'
                        )
                    )
             GROUP BY
                    [PatientID]
         )
         , CombinedTotal AS (
             SELECT t.[PatientID],
                    COALESCE(t.TotalTreatments, 0) AS TotalTreatments,
                    COALESCE(p.TotalPayments, 0) AS TotalPayments,
                    COALESCE(p.TotalPayments, 0) - COALESCE(t.TotalTreatments, 0) AS Balance
             FROM   TreatmentTotal t
                    LEFT JOIN PaymentTotal p
                         ON t.[PatientID] = p.[PatientID]
             UNION ALL
             SELECT p.[PatientID],
                    COALESCE(t.TotalTreatments, 0) AS TotalTreatments,
                    COALESCE(p.TotalPayments, 0) AS TotalPayments,
                    COALESCE(p.TotalPayments, 0) - COALESCE(t.TotalTreatments, 0) AS Balance
             FROM   PaymentTotal p
                    LEFT JOIN TreatmentTotal t
                         ON p.[PatientID] = t.[PatientID]
             WHERE  t.[PatientID] IS NULL
         )

    SELECT c.[PatientID],
           pt.[PatientName],
           c.TotalTreatments,
           c.TotalPayments,
           c.Balance
    FROM   CombinedTotal c
           JOIN [dbo].[Patient] pt
                ON c.[PatientID] = pt.[PatientID]
    WHERE  c.[PatientID] = @PatientID;

    RETURN @@ERROR
END
GO
