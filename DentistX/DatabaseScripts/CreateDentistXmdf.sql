
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[AllInvNet] 

(
	--@InvID int
)

RETURNS  money

 AS  
BEGIN 
declare @Total money

declare @n money
--set @n=  [dbo].[InvBdyNet] (@InvID)
set @Total =(SELECT       sum([InvoiceNet])  FROM .[dbo].[TblInvoicesHeader])
if @Total = null
set @Total = 0

return @Total
END
GO
/****** Object:  UserDefinedFunction [dbo].[AllPaysOfRes]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[AllPaysOfRes]

(
	 @ResID int
)

RETURNS  money

 AS  
BEGIN 
declare @Total money

declare @n money

set @Total = ( select isnull(sum(TblInvPay.Amount),0) from TblInvPay    where TblInvPay.ResId = @ResID )
if @Total = null
set @Total = 0

return @Total
END

GO
/****** Object:  UserDefinedFunction [dbo].[AnyMnthAnyYr]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[AnyMnthAnyYr]
(
    @m INT,   -- Month (1–12)
    @Yr INT   -- Year
)
RETURNS @Res TABLE 
(
    MNo INT,  
    MName NVARCHAR(50),
    YearT INT,

    TrtTot MONEY, TrtPay MONEY, TrtRem MONEY,
    InvTot MONEY, InvPay MONEY, InvRem MONEY,
    LabTot MONEY, LabPay MONEY, LabRem MONEY,
    DocTot MONEY, DocPay MONEY, DocRem MONEY,
    ExpTot MONEY, ExpPay MONEY, ExpRem MONEY
)
AS
BEGIN
    INSERT INTO @Res
    SELECT 
        @m AS MNo,
        --DATENAME(MONTH, DATEFROMPARTS(@Yr, @m, 1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8 AS MName,
         DATENAME( MONTH, DATEADD( MONTH, @m, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8  AS MName ,
				
        @Yr AS YearT,

        -- Treatments
        ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @m AND YEAR(TrtDate) = @Yr), 0),
        ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0),
        ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) -
        ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @m AND YEAR(TrtDate) = @Yr), 0),

        -- Invoices
        ISNULL((SELECT SUM(InvoiceNet) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @m AND YEAR(InvoiceDate) = @Yr), 0),
        ISNULL((SELECT SUM(Amount) FROM TblInvPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0),
        ISNULL((SELECT SUM(Amount) FROM TblInvPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) -
        ISNULL((SELECT SUM(InvoiceNet) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @m AND YEAR(InvoiceDate) = @Yr), 0),

        -- Labs
        ISNULL((SELECT SUM(Price) FROM LabOrder WHERE MONTH(DeliveryDate) = @m AND YEAR(DeliveryDate) = @Yr), 0),
        ISNULL((SELECT SUM(PayValue) FROM LabPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0),
        ISNULL((SELECT SUM(PayValue) FROM LabPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) -
        ISNULL((SELECT SUM(Price) FROM LabOrder WHERE MONTH(DeliveryDate) = @m AND YEAR(DeliveryDate) = @Yr), 0),

        -- Doctors
        ISNULL((SELECT SUM(WrkVal) FROM DrWork WHERE MONTH(WrkDate) = @m AND YEAR(WrkDate) = @Yr), 0),
        ISNULL((SELECT SUM(PayValue) FROM DrWorkPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0),
        ISNULL((SELECT SUM(PayValue) FROM DrWorkPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) -
        ISNULL((SELECT SUM(WrkVal) FROM DrWork WHERE MONTH(WrkDate) = @m AND YEAR(WrkDate) = @Yr), 0),

        -- Expenses
        ISNULL((SELECT SUM(MasrofAmount) FROM TblMasareef WHERE MONTH(MasrofDate) = @m AND YEAR(MasrofDate) = @Yr), 0),
        ISNULL((SELECT SUM(PayValue) FROM TblExpenPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0),
        ISNULL((SELECT SUM(PayValue) FROM TblExpenPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) -
        ISNULL((SELECT SUM(MasrofAmount) FROM TblMasareef WHERE MONTH(MasrofDate) = @m AND YEAR(MasrofDate) = @Yr), 0)

    RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[AnyQrtr]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
CREATE FUNCTION [dbo].[AnyQrtr]

(	
	-- Add the parameters for the function here
	--@Qrt nvarchar(50),
	@m int,
	--@i int,
	--@i int,
	@Yr int
)
RETURNS @Res
TABLE (MNo INT,  MName nvarchar(50),YearT int,
TrtTot money,TrtPay money,TrtRem money ,
InvTot money,InvPay money,InvRem money ,
LabTot money,LabPay money,LabRem money,DocTot money,
DocPay money,DocRem money,ExpTot money,ExpPay money,
ExpRem money)
AS
BEGIN

DECLARE @i int = 1
	
 		IF @m=1
		Begin
		Set @i=1
		WHILE (@i < 4)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================
		
		ELSE IF @m=2
		Begin
		Set @i=4
		WHILE (@i < 7)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================
--		--====
		ELSE IF @m=3
		Begin
		Set @i=7
		WHILE (@i < 10)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================
--			--==========
			ELSE IF @m=4
		Begin
		Set @i=10
		WHILE (@i < 13)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================

 RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AnyQrtr2Years]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
CREATE FUNCTION [dbo].[AnyQrtr2Years]
(
    @m INT,
    @Yr1 INT,
    @Yr2 INT
)
RETURNS @Res TABLE 
(
    MNo INT,  
    MName NVARCHAR(50),
    Year1 INT,
    Year2 INT,
    TrtTot MONEY, TrtPay MONEY, TrtRem MONEY,
    InvTot MONEY, InvPay MONEY, InvRem MONEY,
    LabTot MONEY, LabPay MONEY, LabRem MONEY,
    DocTot MONEY, DocPay MONEY, DocRem MONEY,
    ExpTot MONEY, ExpPay MONEY, ExpRem MONEY
)
AS
BEGIN
    DECLARE @i INT = CASE @m
                        WHEN 1 THEN 1
                        WHEN 2 THEN 4
                        WHEN 3 THEN 7
                        WHEN 4 THEN 10
                    END;

    DECLARE @max INT = @i + 3;

    WHILE @i < @max
    BEGIN
        INSERT INTO @Res
        SELECT  
            @i,
            DATENAME(MONTH, DATEADD(MONTH, @i - 1, 0)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            @Yr1,
            @Yr2,

            -- Trts
            ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0)
            - ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) BETWEEN @Yr1 AND @Yr2), 0),

            -- Invoices
            ISNULL((SELECT SUM(InvoiceNet) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @i AND YEAR(InvoiceDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(Amount) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(Amount) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0)
            - ISNULL((SELECT SUM(InvoiceNet) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @i AND YEAR(InvoiceDate) BETWEEN @Yr1 AND @Yr2), 0),

            -- Lab
            ISNULL((SELECT SUM(Price) FROM LabOrder WHERE MONTH(DeliveryDate) = @i AND YEAR(DeliveryDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM LabPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM LabPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0)
            - ISNULL((SELECT SUM(Price) FROM LabOrder WHERE MONTH(DeliveryDate) = @i AND YEAR(DeliveryDate) BETWEEN @Yr1 AND @Yr2), 0),

            -- DrWork
            ISNULL((SELECT SUM(WrkVal) FROM DrWork WHERE MONTH(WrkDate) = @i AND YEAR(WrkDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM DrWorkPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM DrWorkPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0)
            - ISNULL((SELECT SUM(WrkVal) FROM DrWork WHERE MONTH(WrkDate) = @i AND YEAR(WrkDate) BETWEEN @Yr1 AND @Yr2), 0),

            -- Expenses
            ISNULL((SELECT SUM(MasrofAmount) FROM TblMasareef WHERE MONTH(MasrofDate) = @i AND YEAR(MasrofDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM TblExpenPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM TblExpenPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0)
            - ISNULL((SELECT SUM(MasrofAmount) FROM TblMasareef WHERE MONTH(MasrofDate) = @i AND YEAR(MasrofDate) BETWEEN @Yr1 AND @Yr2), 0)

        SET @i = @i + 1
    END

    RETURN
END

GO
/****** Object:  UserDefinedFunction [dbo].[AnyQrtr2Yrs]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
create FUNCTION [dbo].[AnyQrtr2Yrs]

(	
	-- Add the parameters for the function here
	--@Qrt nvarchar(50),
	@m int,
	@Yr1 int,
	@Yr2 int
)
RETURNS @Res
TABLE (MNo INT,  MName nvarchar(50),Year1 int,Year2 int,
TrtTot money,TrtPay money,TrtRem money ,
InvTot money,InvPay money,InvRem money ,
LabTot money,LabPay money,LabRem money,DocTot money,
DocPay money,DocRem money,ExpTot money,ExpPay money,
ExpRem money)
AS
BEGIN

DECLARE @i int = 1
	
 		IF @m=1
		Begin
		Set @i=1
		WHILE (@i < 4)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr1),
				(SELECT      @Yr2),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate]) between @Yr1 and @Yr2)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate]) between @Yr1 and @Yr2)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2 )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================
		
		ELSE IF @m=2
		Begin
		Set @i=4
		WHILE (@i < 7)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr1),
				(SELECT      @Yr2),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate]) between @Yr1 and @Yr2)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate]) between @Yr1 and @Yr2)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2 )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================
--		--====
		ELSE IF @m=3
		Begin
		Set @i=7
		WHILE (@i < 10)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr1),
				(SELECT      @Yr2),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate]) between @Yr1 and @Yr2)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate]) between @Yr1 and @Yr2)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2 )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================
--			--==========
			ELSE IF @m=4
		Begin
		Set @i=10
		WHILE (@i < 13)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr1),
				(SELECT      @Yr2),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate]) between @Yr1 and @Yr2)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate]) between @Yr1 and @Yr2)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2 )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================

 RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AnyQrtrAnyYear]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  FUNCTION [dbo].[AnyQrtrAnyYear]
(
    @m INT,   -- Quarter Number: 1 to 4
    @Yr INT
)
RETURNS @Res TABLE 
(
    MNo INT, MName NVARCHAR(50), YearT INT,
    TrtTot MONEY, TrtPay MONEY, TrtRem MONEY,
    InvTot MONEY, InvPay MONEY, InvRem MONEY,
    LabTot MONEY, LabPay MONEY, LabRem MONEY,
    DocTot MONEY, DocPay MONEY, DocRem MONEY,
    ExpTot MONEY, ExpPay MONEY, ExpRem MONEY
)
AS
BEGIN
    DECLARE @StartMonth INT = CASE @m
                                WHEN 1 THEN 1
                                WHEN 2 THEN 4
                                WHEN 3 THEN 7
                                WHEN 4 THEN 10
                              END

    DECLARE @i INT = @StartMonth

    WHILE @i < @StartMonth + 3
    BEGIN
        DECLARE 
            @TrtTot MONEY = (SELECT ISNULL(SUM(trtValue), 0) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) = @Yr),
            @TrtPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @InvTot MONEY = (SELECT ISNULL(SUM(InvoiceNet), 0) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @i AND YEAR(InvoiceDate) = @Yr),
            @InvPay MONEY = (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @LabTot MONEY = (SELECT ISNULL(SUM(Price), 0) FROM LabOrder WHERE MONTH(DeliveryDate) = @i AND YEAR(DeliveryDate) = @Yr),
            @LabPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM LabPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @DocTot MONEY = (SELECT ISNULL(SUM(WrkVal), 0) FROM DrWork WHERE MONTH(WrkDate) = @i AND YEAR(WrkDate) = @Yr),
            @DocPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM DrWorkPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @ExpTot MONEY = (SELECT ISNULL(SUM(MasrofAmount), 0) FROM TblMasareef WHERE MONTH(MasrofDate) = @i AND YEAR(MasrofDate) = @Yr),
            @ExpPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM TblExpenPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr)

        INSERT INTO @Res
        SELECT 
            @i,
            DATENAME(MONTH, DATEFROMPARTS(@Yr, @i, 1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            @Yr,
            @TrtTot, @TrtPay, @TrtPay - @TrtTot,
            @InvTot, @InvPay, @InvPay - @InvTot,
            @LabTot, @LabPay, @LabPay - @LabTot,
            @DocTot, @DocPay, @DocPay - @DocTot,
            @ExpTot, @ExpPay, @ExpPay - @ExpTot

        SET @i += 1
    END

    RETURN
END

GO
/****** Object:  UserDefinedFunction [dbo].[AnyQrtrAnyYr]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
CREATE FUNCTION [dbo].[AnyQrtrAnyYr]

(	
	-- Add the parameters for the function here
	--@Qrt nvarchar(50),
	@m int,
	--@i int,
	--@i int,
	@Yr int
)
RETURNS @Res
TABLE (MNo INT,  MName nvarchar(50),YearT int,
TrtTot money,TrtPay money,TrtRem money ,
InvTot money,InvPay money,InvRem money ,
LabTot money,LabPay money,LabRem money,DocTot money,
DocPay money,DocRem money,ExpTot money,ExpPay money,
ExpRem money)
AS
BEGIN

DECLARE @i int = 1
	
 		IF @m=1
		Begin
		Set @i=1
		WHILE (@i < 4)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================
		
		ELSE IF @m=2
		Begin
		Set @i=4
		WHILE (@i < 7)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================
--		--====
		ELSE IF @m=3
		Begin
		Set @i=7
		WHILE (@i < 10)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================
--			--==========
			ELSE IF @m=4
		Begin
		Set @i=10
		WHILE (@i < 13)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				SELECT      @i,
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				(SELECT      @Yr),
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================

 RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AnyQruarter]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
CREATE FUNCTION [dbo].[AnyQruarter]
(
    @m INT,  -- Quarter (1 to 4)
    @Yr INT
)
RETURNS @Res TABLE 
(
    MNo INT,
    MName NVARCHAR(50),
    YearT INT,
    TrtTot MONEY, TrtPay MONEY, TrtRem MONEY,
    InvTot MONEY, InvPay MONEY, InvRem MONEY,
    LabTot MONEY, LabPay MONEY, LabRem MONEY,
    DocTot MONEY, DocPay MONEY, DocRem MONEY,
    ExpTot MONEY, ExpPay MONEY, ExpRem MONEY
)
AS
BEGIN
    DECLARE @StartMonth INT = CASE @m
                                WHEN 1 THEN 1
                                WHEN 2 THEN 4
                                WHEN 3 THEN 7
                                WHEN 4 THEN 10
                              END

    DECLARE @i INT = @StartMonth

    WHILE @i < @StartMonth + 3
    BEGIN
        DECLARE 
            @TrtTot MONEY = (SELECT ISNULL(SUM(trtValue), 0) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) = @Yr),
            @TrtPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @InvTot MONEY = (SELECT ISNULL(SUM(InvoiceNet), 0) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @i AND YEAR(InvoiceDate) = @Yr),
            @InvPay MONEY = (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @LabTot MONEY = (SELECT ISNULL(SUM(Price), 0) FROM LabOrder WHERE MONTH(DeliveryDate) = @i AND YEAR(DeliveryDate) = @Yr),
            @LabPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM LabPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @DocTot MONEY = (SELECT ISNULL(SUM(WrkVal), 0) FROM DrWork WHERE MONTH(WrkDate) = @i AND YEAR(WrkDate) = @Yr),
            @DocPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM DrWorkPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @ExpTot MONEY = (SELECT ISNULL(SUM(MasrofAmount), 0) FROM TblMasareef WHERE MONTH(MasrofDate) = @i AND YEAR(MasrofDate) = @Yr),
            @ExpPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM TblExpenPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr)

        INSERT INTO @Res
        SELECT 
            @i,
            DATENAME(MONTH, DATEFROMPARTS(@Yr, @i, 1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            @Yr,
            @TrtTot, @TrtPay, @TrtPay - @TrtTot,
            @InvTot, @InvPay, @InvPay - @InvTot,
            @LabTot, @LabPay, @LabPay - @LabTot,
            @DocTot, @DocPay, @DocPay - @DocTot,
            @ExpTot, @ExpPay, @ExpPay - @ExpTot

        SET @i += 1
    END

    RETURN
END

GO
/****** Object:  UserDefinedFunction [dbo].[AnyYear]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
CREATE FUNCTION [dbo].[AnyYear]

(	
	-- Add the parameters for the function here
	--@Qrt nvarchar(50),
	
	--@i int,
	--@i int,
	@Yr int
)
RETURNS @Res
TABLE (YearT int,MNo INT,  MName nvarchar(50),
TrtTot money,TrtPay money,TrtRem money ,
InvTot money,InvPay money,InvRem money ,
LabTot money,LabPay money,LabRem money,DocTot money,
DocPay money,DocRem money,ExpTot money,ExpPay money,
ExpRem money)
AS
BEGIN

DECLARE @i int = 1
	
 		
		Begin
		Set @i=1
		WHILE (@i < 13)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				 SELECT      @Yr,(SELECT      @i),
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================
		
		

 RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AnyYearClean]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
CREATE FUNCTION [dbo].[AnyYearClean]
(
    @Yr INT
)
RETURNS @Res TABLE 
(
    YearT INT,
    MNo INT,
    MName NVARCHAR(50),
    TrtTot MONEY, TrtPay MONEY, TrtRem MONEY,
    InvTot MONEY, InvPay MONEY, InvRem MONEY,
    LabTot MONEY, LabPay MONEY, LabRem MONEY,
    DocTot MONEY, DocPay MONEY, DocRem MONEY,
    ExpTot MONEY, ExpPay MONEY, ExpRem MONEY
)
AS
BEGIN
    DECLARE @i INT = 1;

    WHILE (@i < 13)
    BEGIN
        INSERT INTO @Res
        SELECT  
            @Yr,
            @i,
            DATENAME(MONTH, DATEADD(MONTH, @i - 1, 0)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,

            -- Treatments
            ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) = @Yr), 0),
            ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0),
            ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0)
            - ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) = @Yr), 0),

            -- Invoices
            ISNULL((SELECT SUM(InvoiceNet) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @i AND YEAR(InvoiceDate) = @Yr), 0),
            ISNULL((SELECT SUM(Amount) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0),
            ISNULL((SELECT SUM(Amount) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0)
            - ISNULL((SELECT SUM(InvoiceNet) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @i AND YEAR(InvoiceDate) = @Yr), 0),

            -- Labs
            ISNULL((SELECT SUM(Price) FROM LabOrder WHERE MONTH(DeliveryDate) = @i AND YEAR(DeliveryDate) = @Yr), 0),
            ISNULL((SELECT SUM(PayValue) FROM LabPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0),
            ISNULL((SELECT SUM(PayValue) FROM LabPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0)
            - ISNULL((SELECT SUM(Price) FROM LabOrder WHERE MONTH(DeliveryDate) = @i AND YEAR(DeliveryDate) = @Yr), 0),

            -- Doctor Work
            ISNULL((SELECT SUM(WrkVal) FROM DrWork WHERE MONTH(WrkDate) = @i AND YEAR(WrkDate) = @Yr), 0),
            ISNULL((SELECT SUM(PayValue) FROM DrWorkPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0),
            ISNULL((SELECT SUM(PayValue) FROM DrWorkPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0)
            - ISNULL((SELECT SUM(WrkVal) FROM DrWork WHERE MONTH(WrkDate) = @i AND YEAR(WrkDate) = @Yr), 0),

            -- Expenses
            ISNULL((SELECT SUM(MasrofAmount) FROM TblMasareef WHERE MONTH(MasrofDate) = @i AND YEAR(MasrofDate) = @Yr), 0),
            ISNULL((SELECT SUM(PayValue) FROM TblExpenPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0),
            ISNULL((SELECT SUM(PayValue) FROM TblExpenPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0)
            - ISNULL((SELECT SUM(MasrofAmount) FROM TblMasareef WHERE MONTH(MasrofDate) = @i AND YEAR(MasrofDate) = @Yr), 0)

        SET @i = @i + 1
    END

    RETURN
END

GO
/****** Object:  UserDefinedFunction [dbo].[AnyYrDOC]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
create FUNCTION [dbo].[AnyYrDOC] 
(

	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
  
   DocTot money,DocPay money,DocRem money 
   )
AS

BEGIN
DECLARE @i int = 1
WHILE (@i < 13)
BEGIN
	INSERT INTO @REs
		 SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		--======================================================================================================
		(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
		(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
		(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
		(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) 
	SET @i = @i + 1
END
RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AnyYrEXP]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
create FUNCTION [dbo].[AnyYrEXP] 
(

	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
  
   ExpTot money,ExpPay money,ExpRem money
   )
AS

BEGIN
DECLARE @i int = 1
WHILE (@i < 13)
BEGIN
	INSERT INTO @REs
		 SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		--======================================================================================================
		(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
		(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
		(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
		(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr )  
	SET @i = @i + 1
END
RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AnyYrINV]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
create FUNCTION [dbo].[AnyYrINV] 
(

	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
  
   InvTot money,InvPay money,InvRem money
   )
AS

BEGIN
DECLARE @i int = 1
WHILE (@i < 13)
BEGIN
	INSERT INTO @REs
		 SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		--======================================================================================================
		(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
		( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 		( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
		(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) 
	SET @i = @i + 1
END
RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AnyYrLAB]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
create FUNCTION [dbo].[AnyYrLAB] 
(

	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
  
   LabTot money,LabPay money,LabRem money 
   )
AS

BEGIN
DECLARE @i int = 1
WHILE (@i < 13)
BEGIN
	INSERT INTO @REs
		 SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		--======================================================================================================
		(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
		(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
		(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
		(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr )
	SET @i = @i + 1
END
RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AnyYrTRT]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[AnyYrTRT] 
(

	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
   TrtTot money,
   TrtPay money,
   TrtRem money
   )
AS

BEGIN
DECLARE @i int = 1
WHILE (@i < 13)
BEGIN
	INSERT INTO @REs
		 SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
	SET @i = @i + 1
END
RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[AnyYrTRTClean]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[AnyYrTRTClean] 
(
    @Yr INT
)
RETURNS @Res TABLE 
(
    MNo INT,
    MName NVARCHAR(50),
    TrtTot MONEY,
    TrtPay MONEY,
    TrtRem MONEY
)
AS
BEGIN
    DECLARE @i INT = 1

    WHILE (@i <= 12)
    BEGIN
        DECLARE @TrtTot MONEY = (
            SELECT ISNULL(SUM(trtValue), 0)
            FROM Patient_Trts
            WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) = @Yr
        )

        DECLARE @TrtPay MONEY = (
            SELECT ISNULL(SUM(PayValue), 0)
            FROM Patient_Pays
            WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr
        )

        INSERT INTO @Res
        SELECT
            @i,
            DATENAME(MONTH, DATEADD(MONTH, @i - 1, 0)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            @TrtTot,
            @TrtPay,
            @TrtPay - @TrtTot

        SET @i += 1
    END

    RETURN
END

GO
/****** Object:  UserDefinedFunction [dbo].[HasPermission]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[HasPermission](@UserID INT, @PermName NVARCHAR(100))
RETURNS BIT
AS
BEGIN
    DECLARE @PermID INT, @IsAllowed BIT

    SELECT @PermID = PermID FROM Permissions WHERE PermName = @PermName

    -- First check user-specific permission
    SELECT TOP 1 @IsAllowed = IsAllowed 
    FROM UserPermissions 
    WHERE UsID = @UserID AND PermID = @PermID

    IF @IsAllowed IS NOT NULL
        RETURN @IsAllowed

    -- Else check group-level permission
    SELECT @IsAllowed = gp.IsAllowed
    FROM GroupPermissions gp
    INNER JOIN USERS u ON u.GroupID = gp.GroupID
    WHERE u.UsID = @UserID AND gp.PermID = @PermID

    RETURN ISNULL(@IsAllowed, 0)  -- Default deny
END
GO
/****** Object:  UserDefinedFunction [dbo].[InvBdyNet]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[InvBdyNet] 

(
	@InvID 	int
)

RETURNS  money

 AS  
BEGIN 
declare @Total money
set @Total = ( select sum ((isnull(TblInvoiceBody.Price,0) * isnull(TblInvoiceBody.Quantity,0)) - isnull(TblInvoiceBody.ItemHasm,0)) from TblInvoiceBody  where TblInvoiceBody.InvId = @InvID )
if @Total = null
set @Total = 0

return @Total
END
GO
/****** Object:  UserDefinedFunction [dbo].[InvNet]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[InvNet] 

(
	@InvID int
)

RETURNS  money

 AS  
BEGIN 
declare @Total money

declare @n money
set @n=  [dbo].[InvBdyNet] (@InvID)
set @Total =(@n - ( select TblInvoicesHeader.Hasm from TblInvoicesHeader  where TblInvoicesHeader.InvoiceId = @InvID ))
if @Total = null
set @Total = 0

return @Total
END
GO
/****** Object:  UserDefinedFunction [dbo].[InvTotalPays]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
create FUNCTION [dbo].[InvTotalPays]

(
	 @InvId int
)

RETURNS  money

 AS  
BEGIN 
declare @Total money

declare @n money

set @Total = ( select isnull(sum(TblInvPay.Amount),0) from TblInvPay    where dbo.TblInvPay.InvoiceId = @InvId )
if @Total = null
set @Total = 0

return @Total
END

GO
/****** Object:  UserDefinedFunction [dbo].[ItmTotlDisc]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>([dbo].[invnet]([invoiceid]))
-- =============================================
create FUNCTION [dbo].[ItmTotlDisc]

(
	@InvID int
)

RETURNS money
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @Total money

	-- Add the T-SQL statements to compute the return value here
	 set @Total = ( select  sum(isnull(TblInvoiceBody.ItemHasm,0))  from TblInvoiceBody  where TblInvoiceBody.InvId = @InvID )
if @Total = null
set @Total = 0
	-- Return the result of the function
	RETURN @Total

END
GO
/****** Object:  UserDefinedFunction [dbo].[ItmTotlPrice]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[ItmTotlPrice]

(
	@InvID int

)

RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @Total float

	-- Add the T-SQL statements to compute the return value here
	 set @Total = ( select  sum(isnull(TblInvoiceBody.Quantity,0) * isnull(TblInvoiceBody.Price,0))  from TblInvoiceBody  where TblInvoiceBody.InvId = @InvID )
if @Total = null
set @Total = 0
	-- Return the result of the function
	RETURN @Total

END
GO
/****** Object:  UserDefinedFunction [dbo].[ItmTotlQuant]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[ItmTotlQuant]

(
	@InvID int
)

RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @Total float

	-- Add the T-SQL statements to compute the return value here
	 set @Total = ( select  sum(isnull(TblInvoiceBody.Quantity,0))  from TblInvoiceBody  where TblInvoiceBody.InvId = @InvID )
if @Total = null
set @Total = 0
	-- Return the result of the function
	RETURN @Total

END
GO
/****** Object:  UserDefinedFunction [dbo].[Qrt1Fun]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[Qrt1Fun]
(	
	-- Add the parameters for the function here
	--@i int,
	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
   MTrt money,
   MPay money,
   MRemain money
   
)
AS


BEGIN
DECLARE @i int = 1
WHILE (@i < 13)
BEGIN
	INSERT INTO @REs
		 SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
	SET @i = @i + 1
END
RETURN 
END
	

GO
/****** Object:  UserDefinedFunction [dbo].[Qrt2Fun]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create FUNCTION [dbo].[Qrt2Fun]
(	

	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
   MTrt money,
   MPay money,
   MRemain money
   )
AS

BEGIN
DECLARE @i int = 4
WHILE (@i < 7)
BEGIN
	INSERT INTO @REs
		 SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
	SET @i = @i + 1
END
RETURN 
END
	

GO
/****** Object:  UserDefinedFunction [dbo].[Qrtr2YrDOC]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create FUNCTION [dbo].[Qrtr2YrDOC]
(	
	-- Add the parameters for the function here
	@M int,
	@Yr1 int,
	@Yr2 int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
 DocTot money,DocPay money,DocRem money
   
)
AS


BEGIN
	DECLARE @i int = 1
	if @m >=1 AND @m <4
		Begin
		Set @i=1
		WHILE (@i < 4)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2)
			SET @i = @i + 1
			END
		END
	 else if @m >=4 AND @m <7
		Begin
		Set @i=4
		WHILE (@i < 7)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2)
			SET @i = @i + 1
			END
			END
	 else if  @m >=7 AND @m <10
		Begin
		Set @i=7
		WHILE (@i < 10)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2)
			SET @i = @i + 1
			END
			END
	 else if  @m >=10 AND @m <13
		Begin
		Set @i=10
		WHILE (@i < 13)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2)
			SET @i = @i + 1
			END
	End

RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[Qrtr2YrEXP]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create FUNCTION [dbo].[Qrtr2YrEXP]
(	
	-- Add the parameters for the function here
	@M int,
	@Yr1 int,
	@Yr2 int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
 ExpTot money,ExpPay money,ExpRem money
   
)
AS


BEGIN
	DECLARE @i int = 1
	if @m >=1 AND @m <4
		Begin
		Set @i=1
		WHILE (@i < 4)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 )
			SET @i = @i + 1
			END
		END
	 else if @m >=4 AND @m <7
		Begin
		Set @i=4
		WHILE (@i < 7)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 )
			SET @i = @i + 1
			END
			END
	 else if  @m >=7 AND @m <10
		Begin
		Set @i=7
		WHILE (@i < 10)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 )
			SET @i = @i + 1
			END
			END
	 else if  @m >=10 AND @m <13
		Begin
		Set @i=10
		WHILE (@i < 13)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 )
			SET @i = @i + 1
			END
	End

RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[Qrtr2YrINV]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create FUNCTION [dbo].[Qrtr2YrINV]
(	
	-- Add the parameters for the function here
	@M int,
	@Yr1 int,
	@Yr2 int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
   InvTot money,InvPay money,InvRem money
   
)
AS


BEGIN
	DECLARE @i int = 1
	if @m >=1 AND @m <4
		Begin
		Set @i=1
		WHILE (@i < 4)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 )   
			 
			SET @i = @i + 1
			END
		END
	 else if @m >=4 AND @m <7
		Begin
		Set @i=4
		WHILE (@i < 7)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 )  
			 
			SET @i = @i + 1
			END
			END
	 else if  @m >=7 AND @m <10
		Begin
		Set @i=7
		WHILE (@i < 10)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 )  
			 
			SET @i = @i + 1
			END
			END
	 else if  @m >=10 AND @m <13
		Begin
		Set @i=10
		WHILE (@i < 13)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 )  
			 
			SET @i = @i + 1
			END
	End

RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[Qrtr2YrLAB]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create FUNCTION [dbo].[Qrtr2YrLAB]
(	
	-- Add the parameters for the function here
	@M int,
	@Yr1 int,
	@Yr2 int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
  LabTot money,LabPay money,LabRem money
   
)
AS


BEGIN
	DECLARE @i int = 1
	if @m >=1 AND @m <4
		Begin
		Set @i=1
		WHILE (@i < 4)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) 
			SET @i = @i + 1
			END
		END
	 else if @m >=4 AND @m <7
		Begin
		Set @i=4
		WHILE (@i < 7)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) 
			SET @i = @i + 1
			END
			END
	 else if  @m >=7 AND @m <10
		Begin
		Set @i=7
		WHILE (@i < 10)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) 
			SET @i = @i + 1
			END
			END
	 else if  @m >=10 AND @m <13
		Begin
		Set @i=10
		WHILE (@i < 13)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) 
			SET @i = @i + 1
			END
	End

RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[Qrtr2YrTrt]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[Qrtr2YrTrt]
(	
	-- Add the parameters for the function here
	@M int,
	@Yr1 int,
	@Yr2 int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
  TrtTot money,
   TrtPay money,
   TrtRem money
   
)
AS


BEGIN
	DECLARE @i int = 1
	if @m >=1 AND @m <4
		Begin
		Set @i=1
		WHILE (@i < 4)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate]) between @Yr1 and @Yr2)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate]) between @Yr1 and @Yr2)   
			 
			SET @i = @i + 1
			END
		END
	 else if @m >=4 AND @m <7
		Begin
		Set @i=4
		WHILE (@i < 7)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate]) between @Yr1 and @Yr2)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate]) between @Yr1 and @Yr2)   
			 
			SET @i = @i + 1
			END
			END
	 else if  @m >=7 AND @m <10
		Begin
		Set @i=7
		WHILE (@i < 10)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate]) between @Yr1 and @Yr2)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate]) between @Yr1 and @Yr2)   
			 
			SET @i = @i + 1
			END
			END
	 else if  @m >=10 AND @m <13
		Begin
		Set @i=10
		WHILE (@i < 13)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate]) between @Yr1 and @Yr2)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate]) between @Yr1 and @Yr2)   
			 
			SET @i = @i + 1
			END
	End

RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[Qrtr2YrTrtClean]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[Qrtr2YrTrtClean]
(
    @M INT,     -- Month inside the desired quarter (e.g., 1, 4, 7, 10)
    @Yr1 INT,   -- Start year
    @Yr2 INT    -- End year
)
RETURNS @Res TABLE 
(
    MNo INT,
    MName NVARCHAR(50),
    TrtTot MONEY,
    TrtPay MONEY,
    TrtRem MONEY
)
AS
BEGIN
    DECLARE @i INT, @endMonth INT

    -- Normalize input to quarter start
    IF @M BETWEEN 1 AND 3 SET @i = 1
    ELSE IF @M BETWEEN 4 AND 6 SET @i = 4
    ELSE IF @M BETWEEN 7 AND 9 SET @i = 7
    ELSE IF @M BETWEEN 10 AND 12 SET @i = 10
    ELSE RETURN  -- Invalid @M, exit early

    SET @endMonth = @i + 3  -- Not inclusive in WHILE

    WHILE @i < @endMonth
    BEGIN
        INSERT INTO @Res
        SELECT
            @i,
            DATENAME(MONTH, DATEADD(MONTH, @i - 1, 0)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0) -
            ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) BETWEEN @Yr1 AND @Yr2), 0)

        SET @i += 1
    END

    RETURN
END

GO
/****** Object:  UserDefinedFunction [dbo].[QrtrYr]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[QrtrYr]
(	
	-- Add the parameters for the function here
	@M int,
	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
   MTrt money,
   MPay money,
   MRemain money
   
)
AS


BEGIN
	DECLARE @i int = 1
	if @m=1
		Begin
		Set @i=1
		WHILE (@i < 4)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
			SET @i = @i + 1
			END
		END
	 else if @m=2
		Begin
		Set @i=4
		WHILE (@i < 7)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
			SET @i = @i + 1
			END
			END
	 else if @m=3
		Begin
		Set @i=7
		WHILE (@i < 10)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
			SET @i = @i + 1
			END
			END
	 else if @m=4
		Begin
		Set @i=10
		WHILE (@i < 13)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
			SET @i = @i + 1
			END
	End

RETURN 
END
	

GO
/****** Object:  UserDefinedFunction [dbo].[QrtrYrDOC]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create FUNCTION [dbo].[QrtrYrDOC]
(	
	-- Add the parameters for the function here
	@M int,
	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
 DocTot money,DocPay money,DocRem money
   
)
AS


BEGIN
	DECLARE @i int = 1
	if @m >=1 AND @m <4
		Begin
		Set @i=1
		WHILE (@i < 4)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )
			SET @i = @i + 1
			END
		END
	 else if @m >=4 AND @m <7
		Begin
		Set @i=4
		WHILE (@i < 7)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )
			SET @i = @i + 1
			END
			END
	 else if  @m >=7 AND @m <10
		Begin
		Set @i=7
		WHILE (@i < 10)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )
			SET @i = @i + 1
			END
			END
	 else if  @m >=10 AND @m <13
		Begin
		Set @i=10
		WHILE (@i < 13)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate])=@Yr )
			SET @i = @i + 1
			END
	End

RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[QrtrYrEXP]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create FUNCTION [dbo].[QrtrYrEXP]
(	
	-- Add the parameters for the function here
	@M int,
	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
 ExpTot money,ExpPay money,ExpRem money
   
)
AS


BEGIN
	DECLARE @i int = 1
	if @m >=1 AND @m <4
		Begin
		Set @i=1
		WHILE (@i < 4)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr )
			SET @i = @i + 1
			END
		END
	 else if @m >=4 AND @m <7
		Begin
		Set @i=4
		WHILE (@i < 7)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr )
			SET @i = @i + 1
			END
			END
	 else if  @m >=7 AND @m <10
		Begin
		Set @i=7
		WHILE (@i < 10)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr )
			SET @i = @i + 1
			END
			END
	 else if  @m >=10 AND @m <13
		Begin
		Set @i=10
		WHILE (@i < 13)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate])=@Yr )
			SET @i = @i + 1
			END
	End

RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[QrtrYrINV]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create FUNCTION [dbo].[QrtrYrINV]
(	
	-- Add the parameters for the function here
	@M int,
	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
   InvTot money,InvPay money,InvRem money
   
)
AS


BEGIN
	DECLARE @i int = 1
	if @m >=1 AND @m <4
		Begin
		Set @i=1
		WHILE (@i < 4)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr )   
			 
			SET @i = @i + 1
			END
		END
	 else if @m >=4 AND @m <7
		Begin
		Set @i=4
		WHILE (@i < 7)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr )  
			 
			SET @i = @i + 1
			END
			END
	 else if  @m >=7 AND @m <10
		Begin
		Set @i=7
		WHILE (@i < 10)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr )  
			 
			SET @i = @i + 1
			END
			END
	 else if  @m >=10 AND @m <13
		Begin
		Set @i=10
		WHILE (@i < 13)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate])=@Yr) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate])=@Yr )  
			 
			SET @i = @i + 1
			END
	End

RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[QrtrYrLAB]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create FUNCTION [dbo].[QrtrYrLAB]
(	
	-- Add the parameters for the function here
	@M int,
	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
  LabTot money,LabPay money,LabRem money
   
)
AS


BEGIN
	DECLARE @i int = 1
	if @m >=1 AND @m <4
		Begin
		Set @i=1
		WHILE (@i < 4)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) 
			SET @i = @i + 1
			END
		END
	 else if @m >=4 AND @m <7
		Begin
		Set @i=4
		WHILE (@i < 7)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) 
			SET @i = @i + 1
			END
			END
	 else if  @m >=7 AND @m <10
		Begin
		Set @i=7
		WHILE (@i < 10)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) 
			SET @i = @i + 1
			END
			END
	 else if  @m >=10 AND @m <13
		Begin
		Set @i=10
		WHILE (@i < 13)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate])=@Yr) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate])=@Yr ) 
			SET @i = @i + 1
			END
	End

RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[QrtrYrTrt]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[QrtrYrTrt]
(	
	-- Add the parameters for the function here
	@M int,
	@Yr int
)
RETURNS @REs
TABLE 
(
    MNo INT,
   MName nvarchar(50),
   TrtTot money,
   TrtPay money,
   TrtRem money
   
)
AS


BEGIN
	DECLARE @i int = 1
	if @m >=1 AND @m <4
		Begin
		Set @i=1
		WHILE (@i < 4)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
			SET @i = @i + 1
			END
		END
	 else if @m >=4 AND @m <7
		Begin
		Set @i=4
		WHILE (@i < 7)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
			SET @i = @i + 1
			END
			END
	 else if  @m >=7 AND @m <10
		Begin
		Set @i=7
		WHILE (@i < 10)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
			SET @i = @i + 1
			END
			END
	 else if  @m >=10 AND @m <13
		Begin
		Set @i=10
		WHILE (@i < 13)
			BEGIN
			INSERT INTO @REs
			SELECT      @i,
			(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
			(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
			(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
			SET @i = @i + 1
			END
	End

RETURN 
END
GO
/****** Object:  UserDefinedFunction [dbo].[QrtrYrTrtClean]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[QrtrYrTrtClean]
(
    @M INT,
    @Yr INT
)
RETURNS @Res TABLE 
(
    MNo INT,
    MName NVARCHAR(50),
    TrtTot MONEY,
    TrtPay MONEY,
    TrtRem MONEY
)
AS
BEGIN
    DECLARE @i INT, @endMonth INT

    -- Normalize @M to quarter start
    IF @M BETWEEN 1 AND 3 SET @i = 1
    ELSE IF @M BETWEEN 4 AND 6 SET @i = 4
    ELSE IF @M BETWEEN 7 AND 9 SET @i = 7
    ELSE IF @M BETWEEN 10 AND 12 SET @i = 10
    ELSE RETURN  -- Invalid @M input

    SET @endMonth = @i + 3  -- upper limit (non-inclusive in WHILE)

    WHILE (@i < @endMonth)
    BEGIN
        INSERT INTO @Res
        SELECT
            @i,
            DATENAME(MONTH, DATEADD(MONTH, @i - 1, 0)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) = @Yr), 0),
            ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0),
            ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0) -
            ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) = @Yr), 0)

        SET @i += 1
    END

    RETURN
END

GO
/****** Object:  UserDefinedFunction [dbo].[ResBal]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
Create FUNCTION [dbo].[ResBal] 

(
	@ResID int
)

RETURNS  money

 AS  
BEGIN 
declare @Total money

declare @n money

set @Total = ( (select isnull(sum(TblInvPay.Amount),0) from TblInvPay  where TblInvPay.ResId = @ResID )- (select isnull(dbo.resinvnet(@ResID),0) ))
if @Total = null
set @Total = 0

return @Total
END
GO
/****** Object:  UserDefinedFunction [dbo].[ResInvNet]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[ResInvNet] 

(
	@ResID int
)

RETURNS  money

 AS  
BEGIN 
declare @Total money

declare @n money

set @Total = ( select isnull(sum(TblInvoicesHeader.InvoiceNet),0) from TblInvoicesHeader  where TblInvoicesHeader.ResId = @ResID )
if @Total = null
set @Total = 0

return @Total
END
GO
/****** Object:  UserDefinedFunction [dbo].[ResTotalPays]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
Create FUNCTION [dbo].[ResTotalPays] 

(
	@ResID int
)

RETURNS  money

 AS  
BEGIN 
declare @Total money

declare @n money

set @Total = ( select isnull(sum(TblInvPay.Amount),0) from TblInvPay  where TblInvPay.ResId = @ResID )
if @Total = null
set @Total = 0

return @Total
END
GO
/****** Object:  UserDefinedFunction [dbo].[TotalPatientPays]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[TotalPatientPays]
(
	@PatientID int
)

RETURNS  money

 AS  
BEGIN 
declare @TotalPays money
set @TotalPays = ( select sum (payvalue) from patient_pays  where patient_pays.PatientID = @PatientID )
if @TotalPays = null
set @TotalPays = 0

return @TotalPays
END




GO
/****** Object:  UserDefinedFunction [dbo].[TotalPatientTrts]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[TotalPatientTrts]
(
	@PatientID int
)
 
RETURNS  money

 AS  
BEGIN 
declare @TotalTrts money
set @TotalTrts = ( select sum (trtValue) from patient_Trts  where patient_Trts.PatientID = @PatientID )
if @TotalTrts = null
set @TotalTrts = 0

return @TotalTrts
END
GO
/****** Object:  UserDefinedFunction [dbo].[TotalPaysPerTrt]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[TotalPaysPerTrt] (@TrtID int)  
RETURNS  money

 AS  
BEGIN 
declare @TotalPays money
set @TotalPays = ( select sum (payvalue) from patient_pays  where TrtID = @TrtID )
if @TotalPays = null
set @TotalPays = 0

return @TotalPays
END


GO
/****** Object:  UserDefinedFunction [dbo].[TotalTrtsPerPatient]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[TotalTrtsPerPatient] (@PatientID int)  
RETURNS  money

 AS  
BEGIN 
declare @TotalTrts money
set @TotalTrts = ( select sum(trtValue) from patient_Trts  where PatientID = @PatientID )
if @TotalTrts = null
set @TotalTrts = 0

return @TotalTrts
END
GO
/****** Object:  UserDefinedFunction [dbo].[TowYears]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
CREATE FUNCTION [dbo].[TowYears]

(	
	-- Add the parameters for the function here
	--@Qrt nvarchar(50),
	
	--@i int,
	@Yr1 int,
	@Yr2 int
)
RETURNS @Res
TABLE (Year1 int,Year2 int,MNo INT,  MName nvarchar(50),
TrtTot money,TrtPay money,TrtRem money ,
InvTot money,InvPay money,InvRem money ,
LabTot money,LabPay money,LabRem money,DocTot money,
DocPay money,DocRem money,ExpTot money,ExpPay money,
ExpRem money)
AS
BEGIN

DECLARE @i int = 1
	
 		
		Begin
		Set @i=1
		WHILE (@i < 13)
--===========Trts==============================================================
			BEGIN
				INSERT INTO @REs
				 SELECT      @Yr1,@Yr2,(SELECT      @i),
				(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
				
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate]) between @Yr1 and @Yr2)  ,
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) , 
				(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate]) between @Yr1 and @Yr2)   ,
----==========Invoice========================================================================
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) ,
				( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) ,
 			    ( SELECT isnull(sum(TblInvPay.Amount),0) FROM TblInvPay    WHERE month(TblInvPay.PayDate) =@i and Year(TblInvPay.[PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([InvoiceNet]),0) FROM [TblInvoicesHeader] WHERE  	month([InvoiceDate])=@i AND Year([InvoiceDate]) between @Yr1 and @Yr2 ) , 
--==============Labs=================================================
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [LabPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([Price]),0) FROM [LabOrder] WHERE  	month([DeliveryDate])=@i AND Year([DeliveryDate]) between @Yr1 and @Yr2 )   ,
--===============Out Docs================================
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) ,
				(SELECT isnull(sum(PayValue),0) FROM [DrWorkPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([WrkVal]),0) FROM [DrWork] WHERE  	month([WrkDate])=@i AND Year([WrkDate]) between @Yr1 and @Yr2 )  ,
--================Expenses=======================================
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2)  ,
				(SELECT isnull(sum(PayValue),0) FROM [TblExpenPay]  WHERE 	month([PayDate])=@i and Year([PayDate]) between @Yr1 and @Yr2) -
				(SELECT isnull(sum([MasrofAmount]),0) FROM [TblMasareef] WHERE  	month([MasrofDate])=@i AND Year([MasrofDate]) between @Yr1 and @Yr2 ) 
				SET @i = @i + 1
		END
		END
----========Fin=======================================
		
		

 RETURN 
END
GO
/****** Object:  Table [dbo].[TblTRT]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblTRT](
	[TrtID] [int] IDENTITY(1,1) NOT NULL,
	[Trt] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TblTRT] PRIMARY KEY CLUSTERED 
(
	[TrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblTrtClr]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblTrtClr](
	[TrtClrID] [int] IDENTITY(1,1) NOT NULL,
	[TrtID] [int] NOT NULL,
	[FillColor] [nvarchar](50) NULL,
	[BorderColor] [nvarchar](50) NULL,
	[BorderThick] [tinyint] NULL,
	[FillColorDef] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblTrtClr] PRIMARY KEY CLUSTERED 
(
	[TrtClrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_TreatsWithColor]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_TreatsWithColor] AS
SELECT 
    T.TrtID,
    T.Trt,
    T.ShapeID,
    T.TrtDetails,
    T.TrtAr,
    T.TrtArDetails,
    T.ToothID,
    T.ToothIDkID,
    T.OldTrt,
    T.TrtGroup,
    T.KidTrt,
    T.TrtClrID,
    C.TrtColor
FROM TblTRT T
LEFT JOIN TblTrtClr C ON T.TrtClrID = C.TrtClrID
GO
/****** Object:  Table [dbo].[Patient_Pays]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_Pays](
	[PayID] [int] IDENTITY(1,1) NOT NULL,
	[TrtID] [int] NOT NULL,
	[PatientID] [int] NULL,
	[PayValue] [money] NOT NULL,
	[PayDate] [smalldatetime] NOT NULL,
	[Notes] [nvarchar](50) NULL,
	[PayType] [nvarchar](50) NULL,
	[ChqOwner] [nvarchar](100) NULL,
	[AccountNumber] [nvarchar](100) NULL,
	[ChqNumber] [nchar](10) NULL,
	[ChqDueDate] [smalldatetime] NULL,
	[ChqBank] [nvarchar](100) NULL,
	[IsCashed] [bit] NULL,
	[InsuranceCompany] [nvarchar](100) NULL,
	[InsuranceNotes] [nvarchar](100) NULL,
	[IsForward] [bit] NULL,
	[ForwardFromTo] [nvarchar](250) NULL,
 CONSTRAINT [PK_patient_pays] PRIMARY KEY CLUSTERED 
(
	[PayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_Trts]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_Trts](
	[TrtID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[ToothTrtID] [int] NULL,
	[OrthoID] [int] NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[TrtDate] [smalldatetime] NOT NULL,
	[TrtValue] [money] NOT NULL,
	[IsMultiTooth] [bit] NULL,
	[Discount] [money] NULL,
	[DiscountType] [tinyint] NULL,
 CONSTRAINT [PK_Patient_Trts] PRIMARY KEY CLUSTERED 
(
	[TrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[TreatPays]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TreatPays]
AS
SELECT        dbo.Patient_Trts.TrtID, ISNULL(SUM(dbo.Patient_Pays.PayValue), 0) AS TreatPays
FROM            dbo.Patient_Pays INNER JOIN
                         dbo.Patient_Trts ON dbo.Patient_Pays.TrtID = dbo.Patient_Trts.TrtID
GROUP BY dbo.Patient_Trts.TrtID
GO
/****** Object:  View [dbo].[TreatBal]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TreatBal]
AS
SELECT        dbo.Patient_Trts.TrtID, ISNULL(dbo.TreatPays.TreatPays - dbo.Patient_Trts.trtValue, 0) AS TrtBal
FROM            dbo.TreatPays INNER JOIN
                         dbo.Patient_Trts ON dbo.TreatPays.TrtID = dbo.Patient_Trts.TrtID
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[PatientID] [int] IDENTITY(1,1) NOT NULL,
	[PatientName] [nvarchar](50) NOT NULL,
	[PatientNumber] [nvarchar](10) NULL,
	[Sex] [nvarchar](10) NULL,
	[Age] [int] NULL,
	[StillKid] [bit] NULL,
	[Phone] [nvarchar](16) NULL,
	[Address] [nvarchar](50) NULL,
	[Health] [nvarchar](50) NULL,
	[Treat] [bit] NULL,
	[Implant] [bit] NULL,
	[Mobile] [bit] NULL,
	[Ortho] [bit] NULL,
	[Struc] [bit] NULL,
	[Notes] [nvarchar](350) NULL,
	[BirthY] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_patient] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PatientPAYS]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PatientPAYS]
AS
SELECT        dbo.Patient.PatientID, ISNULL(SUM(dbo.Patient_Pays.PayValue), 0) AS PAYS
FROM            dbo.Patient LEFT OUTER JOIN
                         dbo.Patient_Pays ON dbo.Patient.PatientID = dbo.Patient_Pays.PatientID
GROUP BY dbo.Patient.PatientID
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Groups_GroupName] UNIQUE NONCLUSTERED 
(
	[GroupName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[UsID] [int] IDENTITY(1,1) NOT NULL,
	[UsName] [nvarchar](50) NOT NULL,
	[UsPassHash] [varbinary](50) NOT NULL,
	[UsSalt] [varbinary](50) NOT NULL,
	[GroupID] [int] NOT NULL,
	[UsLvl] [int] NOT NULL,
	[UsGrp] [nvarchar](50) NOT NULL,
	[DrID] [int] NULL,
	[SecID] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermID] [int] IDENTITY(1,1) NOT NULL,
	[PermName] [nvarchar](100) NOT NULL,
	[PermDescription] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[PermID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupPermissions]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPermissions](
	[GroupID] [int] NOT NULL,
	[PermID] [int] NOT NULL,
	[IsAllowed] [bit] NOT NULL,
 CONSTRAINT [PK_GroupPermissions] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC,
	[PermID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermissions]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermissions](
	[UsID] [int] NOT NULL,
	[PermID] [int] NOT NULL,
	[IsAllowed] [bit] NOT NULL,
 CONSTRAINT [PK_UserPermissions] PRIMARY KEY CLUSTERED 
(
	[UsID] ASC,
	[PermID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_EffectivePermissions]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[vw_EffectivePermissions]
AS
SELECT 
    u.UsID,
    u.UsName,
    g.GroupName,
    p.PermName,
    gp.IsAllowed AS GroupAllowed,
    up.IsAllowed AS UserOverride,
    ISNULL(up.IsAllowed, gp.IsAllowed) AS EffectiveAllowed
FROM USERS u
INNER JOIN Groups g ON u.GroupID = g.GroupID
CROSS JOIN Permissions p
LEFT JOIN GroupPermissions gp 
    ON gp.GroupID = g.GroupID AND gp.PermID = p.PermID
LEFT JOIN UserPermissions up 
    ON up.UsID = u.UsID AND up.PermID = p.PermID;
GO
/****** Object:  Table [dbo].[ImplantBrand]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImplantBrand](
	[BrandID] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ImplantBrand] PRIMARY KEY CLUSTERED 
(
	[BrandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_ImplantBrand] UNIQUE NONCLUSTERED 
(
	[BrandName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImplantType]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImplantType](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
	[IsSlim] [bit] NOT NULL,
 CONSTRAINT [PK_ImplantType] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImplantMeasure]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImplantMeasure](
	[MeasureID] [int] IDENTITY(1,1) NOT NULL,
	[DiameterMM] [decimal](4, 2) NOT NULL,
	[LengthMM] [decimal](4, 2) NOT NULL,
 CONSTRAINT [PK_ImplantMeasure] PRIMARY KEY CLUSTERED 
(
	[MeasureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ImplantVW]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ImplantVW]
AS
SELECT        m.MeasureID, b.BrandName, t.TypeName, CASE WHEN t .IsSlim = 1 THEN 'Slim' ELSE 'Standard' END AS Design, m.DiameterMM, m.LengthMM, CAST(m.DiameterMM AS VARCHAR(10)) 
                         + 'x' + CAST(m.LengthMM AS VARCHAR(10)) + 'mm' AS Size, b.BrandName + ' ' + t.TypeName + CASE WHEN t .IsSlim = 1 THEN ' Slim' ELSE '' END + ' - ' + CAST(m.DiameterMM AS VARCHAR(10)) 
                         + 'x' + CAST(m.LengthMM AS VARCHAR(10)) + 'mm' AS DisplayName
FROM            dbo.ImplantBrand AS b INNER JOIN
                         dbo.ImplantType AS t ON b.BrandID = t.BrandID INNER JOIN
                         dbo.ImplantMeasure AS m ON m.TypeID = t.TypeID
GO
/****** Object:  View [dbo].[PatientTRTS]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PatientTRTS]
AS
SELECT        dbo.Patient.PatientID, ISNULL(SUM(dbo.Patient_Trts.trtValue), 0) AS TRTS
FROM            dbo.Patient LEFT OUTER JOIN
                         dbo.Patient_Trts ON dbo.Patient.PatientID = dbo.Patient_Trts.PatientID
GROUP BY dbo.Patient.PatientID
GO
/****** Object:  View [dbo].[BALANCE]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BALANCE]
AS
SELECT        TOP (100) PERCENT dbo.patient.PatientID, ISNULL(dbo.PatientPAYS.PAYS - dbo.PatientTRTS.TRTS, 0) AS BAL
FROM            dbo.patient LEFT OUTER JOIN
                         dbo.PatientTRTS ON dbo.patient.PatientID = dbo.PatientTRTS.PatientID LEFT OUTER JOIN
                         dbo.PatientPAYS ON dbo.patient.PatientID = dbo.PatientPAYS.PatientID
ORDER BY dbo.patient.PatientID
GO
/****** Object:  View [dbo].[LstPatientView]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[LstPatientView]
AS
SELECT        dbo.patient.PatientID, dbo.patient.PatientName, ISNULL(YEAR(GETDATE()) - dbo.patient.BirthY, 0) AS Age, dbo.patient.Sex, dbo.patient.Phone, dbo.patient.Address, dbo.patient.Health, dbo.patient.Treat, dbo.patient.Implant, 
                         dbo.patient.Mobile, dbo.patient.Ortho, dbo.patient.Struc, dbo.patient.Notes, ISNULL(dbo.PatientTRTS.TRTS, 0) AS Treats, ISNULL(dbo.PatientPAYS.PAYS, 0) AS Pays, ISNULL(dbo.PatientPAYS.PAYS, 0) 
                         - ISNULL(dbo.PatientTRTS.TRTS, 0) AS Balan
FROM            dbo.patient LEFT OUTER JOIN
                         dbo.BALANCE ON dbo.patient.PatientID = dbo.BALANCE.PatientID LEFT OUTER JOIN
                         dbo.PatientPAYS ON dbo.patient.PatientID = dbo.PatientPAYS.PatientID LEFT OUTER JOIN
                         dbo.PatientTRTS ON dbo.patient.PatientID = dbo.PatientTRTS.PatientID
GO
/****** Object:  Table [dbo].[TblInvPay]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblInvPay](
	[PayID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[ResID] [int] NOT NULL,
	[PayDate] [datetime] NOT NULL,
	[Amount] [money] NOT NULL,
	[Notes] [nvarchar](50) NULL,
	[InvRemain]  AS (isnull([Amount],(0))-isnull([dbo].[InvNet]([InvoiceID]),(0))),
 CONSTRAINT [PK_TblInvPay] PRIMARY KEY CLUSTERED 
(
	[PayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblResources]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblResources](
	[ResID] [int] IDENTITY(1,1) NOT NULL,
	[ResName] [nvarchar](75) NULL,
	[CityID] [int] NULL,
	[Address] [nvarchar](500) NULL,
	[Contacts] [nvarchar](500) NULL,
	[ResInvsNet]  AS ([dbo].[ResInvNet]([ResID])),
	[ResTotalPays]  AS ([dbo].[ResTotalPays]([ResID])),
	[ResBal]  AS ([dbo].[ResBal]([ResID])),
 CONSTRAINT [PK_tblresources] PRIMARY KEY CLUSTERED 
(
	[ResID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[InvPayments]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[InvPayments] 
(	
	@InvId int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT        dbo.TblInvPay.PayID, dbo.TblResources.ResName, dbo.TblInvPay.PayDate, dbo.TblInvPay.Amount, dbo.TblInvPay.Notes, dbo.TblInvPay.InvRemain,
 dbo.TblInvPay.InvoiceId, dbo.TblInvPay.ResID
FROM            dbo.TblResources INNER JOIN
                         dbo.TblInvPay ON dbo.TblResources.ResId = dbo.TblInvPay.ResID 
						 where dbo.TblInvPay.InvoiceId=@InvId
)
GO
/****** Object:  View [dbo].[AccView]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AccView]
AS
SELECT        PatientID, PatientName, Age, Sex, Address, Phone,
                             (SELECT        ISNULL(SUM(PayValue), 0) AS Expr1
                                FROM            dbo.Patient_Pays
                                WHERE        (dbo.patient.PatientID = PatientID)) AS PAYS,
                             (SELECT        ISNULL(SUM(trtValue), 0) AS Expr1
                                FROM            dbo.Patient_Trts
                                WHERE        (dbo.patient.PatientID = PatientID)) AS TRTS,
                             (SELECT        ISNULL(SUM(PayValue), 0) AS Expr1
                                FROM            dbo.Patient_Pays AS Patient_Pays_1
                                WHERE        (dbo.patient.PatientID = PatientID)) -
                             (SELECT        ISNULL(SUM(trtValue), 0) AS Expr1
                                FROM            dbo.Patient_Trts AS Patient_Trts_1
                                WHERE        (dbo.patient.PatientID = PatientID)) AS BAL
FROM            dbo.patient
GO
/****** Object:  Table [dbo].[TblCategories]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblCategories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](75) NULL,
	[ParentCategory] [int] NULL,
 CONSTRAINT [PK_TblCategories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblInvoiceBody]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblInvoiceBody](
	[InvID] [int] NOT NULL,
	[ItemID] [int] NOT NULL,
	[Quantity] [float] NULL,
	[Price] [money] NULL,
	[ItemHasm] [money] NULL,
	[Note] [nvarchar](100) NULL,
	[ItemNet]  AS (isnull([Price],(0))*isnull([Quantity],(0))-isnull([ItemHasm],(0))),
	[BdyNet]  AS ([dbo].[InvBdyNet]([InvID])),
 CONSTRAINT [PK_TblInvoiceBody] PRIMARY KEY CLUSTERED 
(
	[InvID] ASC,
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblItems]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblItems](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](75) NULL,
	[ItemEx] [nvarchar](500) NULL,
	[CatID] [int] NULL,
	[UnitID] [int] NULL,
	[LastPrice] [money] NULL,
	[QuantityNow] [float] NULL,
 CONSTRAINT [PK_TblItems] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblUnits]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblUnits](
	[UnitID] [int] IDENTITY(1,1) NOT NULL,
	[UnitName] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblunits] PRIMARY KEY CLUSTERED 
(
	[UnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[InvBdyView]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InvBdyView]
AS
SELECT        dbo.TblInvoiceBody.InvId, dbo.TblItems.ItemId, dbo.TblItems.ItemName, dbo.TblCategories.CategoryId, dbo.TblCategories.CategoryName, dbo.TblUnits.UnitId, dbo.TblUnits.UnitName, dbo.TblInvoiceBody.Quantity, 
                         dbo.TblInvoiceBody.Price, dbo.TblInvoiceBody.ItemHasm, dbo.TblInvoiceBody.Note
FROM            dbo.TblCategories INNER JOIN
                         dbo.TblItems ON dbo.TblCategories.CategoryId = dbo.TblItems.CatId INNER JOIN
                         dbo.TblInvoiceBody ON dbo.TblItems.ItemId = dbo.TblInvoiceBody.ItemId INNER JOIN
                         dbo.TblUnits ON dbo.TblItems.UnitId = dbo.TblUnits.UnitId
GO
/****** Object:  Table [dbo].[TblInvoicesHeader]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblInvoicesHeader](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceType] [tinyint] NULL,
	[InvoiceDate] [smalldatetime] NULL,
	[ResID] [int] NULL,
	[DocNo] [int] NULL,
	[InvoiceEx] [nvarchar](500) NULL,
	[Hasm] [money] NULL,
	[InvTotlQuantItms]  AS ([dbo].[ItmTotlQuant]([InvoiceID])),
	[InvTotlPriceItms]  AS ([dbo].[ItmTotlPrice]([InvoiceID])),
	[InvTotlDiscItms]  AS ([dbo].[ItmTotlDisc]([InvoiceID])),
	[InvTotlDisc]  AS ([dbo].[ItmTotlDisc]([InvoiceID])+[Hasm]),
	[InvoiceNet]  AS ([dbo].[InvNet]([InvoiceID])),
 CONSTRAINT [PK_TblInvoicesHeader] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[InvBodyVW]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InvBodyVW]
AS
SELECT        dbo.TblInvoicesHeader.InvoiceId, dbo.TblItems.ItemId, dbo.TblItems.ItemName, dbo.TblCategories.CategoryId, dbo.TblCategories.CategoryName, dbo.TblUnits.UnitId, dbo.TblUnits.UnitName, dbo.TblInvoiceBody.Quantity, 
                         dbo.TblInvoiceBody.Price, dbo.TblInvoiceBody.ItemHasm, dbo.TblInvoiceBody.Note
FROM            dbo.TblCategories INNER JOIN
                         dbo.TblItems ON dbo.TblCategories.CategoryId = dbo.TblItems.CatId INNER JOIN
                         dbo.TblInvoiceBody ON dbo.TblItems.ItemId = dbo.TblInvoiceBody.ItemId INNER JOIN
                         dbo.TblInvoicesHeader ON dbo.TblInvoiceBody.InvId = dbo.TblInvoicesHeader.InvoiceId INNER JOIN
                         dbo.TblUnits ON dbo.TblItems.UnitId = dbo.TblUnits.UnitId
GO
/****** Object:  View [dbo].[InvHdrVW]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InvHdrVW]
AS
SELECT        dbo.TblInvoicesHeader.InvoiceId, dbo.TblInvoicesHeader.InvoiceType, dbo.TblInvoicesHeader.InvoiceDate, dbo.TblResources.ResId, dbo.TblResources.ResName, dbo.TblInvoicesHeader.DocNo, 
                         dbo.TblInvoicesHeader.InvoiceEx, dbo.TblInvoicesHeader.Hasm
FROM            dbo.TblInvoicesHeader INNER JOIN
                         dbo.TblResources ON dbo.TblInvoicesHeader.ResId = dbo.TblResources.ResId
GO
/****** Object:  Table [dbo].[Repayments]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Repayments](
	[RepaymentID] [int] IDENTITY(1,1) NOT NULL,
	[LoanID] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[RepaymentDate] [smalldatetime] NOT NULL,
	[Notes] [nvarchar](255) NULL,
	[CreatedAt] [smalldatetime] NULL,
 CONSTRAINT [PK_Repayments] PRIMARY KEY CLUSTERED 
(
	[RepaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loans]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loans](
	[LoanID] [int] IDENTITY(1,1) NOT NULL,
	[ContactID] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[Direction] [nvarchar](10) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[LoanDate] [smalldatetime] NOT NULL,
	[CreatedAt] [smalldatetime] NULL,
 CONSTRAINT [PK_Loans] PRIMARY KEY CLUSTERED 
(
	[LoanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[CName] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedAt] [smalldatetime] NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_LoanBalances]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =========================================
-- View: Loan Balances
-- =========================================
CREATE VIEW [dbo].[vw_LoanBalances] AS
SELECT 
    l.LoanID,
    l.ContactID,
    c.CName,
    l.Amount AS OriginalAmount,
    ISNULL(SUM(r.Amount), 0) AS TotalRepaid,
    l.Amount - ISNULL(SUM(r.Amount), 0) AS RemainingBalance,
    l.Direction,
    l.LoanDate,
    l.Description
FROM Loans l
JOIN Contacts c ON c.ContactID = l.ContactID
LEFT JOIN Repayments r ON r.LoanID = l.LoanID
GROUP BY 
    l.LoanID, l.ContactID, c.CName, l.Amount, l.Direction, l.LoanDate, l.Description;

GO
/****** Object:  Table [dbo].[TblCities]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblCities](
	[CityID] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblcities] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[InvVIEW]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InvVIEW]
AS
SELECT        dbo.TblInvoicesHeader.InvoiceID, dbo.TblInvoicesHeader.InvoiceType, dbo.TblInvoicesHeader.InvoiceDate, dbo.TblInvoicesHeader.DocNo, dbo.TblInvoicesHeader.InvoiceEx, dbo.TblInvoicesHeader.Hasm, 
                         dbo.TblResources.ResID, dbo.TblResources.ResName, dbo.TblCities.CityName, dbo.TblResources.Address, dbo.TblResources.Contacts, dbo.TblItems.ItemID, dbo.TblItems.ItemName, dbo.TblInvoiceBody.Quantity, 
                         dbo.TblInvoiceBody.Price, dbo.TblInvoiceBody.ItemHasm, dbo.TblInvoiceBody.ItemNet, dbo.TblInvoiceBody.Note, dbo.TblUnits.UnitName, dbo.TblInvoicesHeader.InvoiceNet, dbo.TblInvoiceBody.BdyNet, 
                         dbo.TblResources.ResInvsNet, dbo.TblResources.ResTotalPays, dbo.TblResources.ResBal
FROM            dbo.TblInvoiceBody INNER JOIN
                         dbo.TblInvoicesHeader ON dbo.TblInvoiceBody.InvID = dbo.TblInvoicesHeader.InvoiceID INNER JOIN
                         dbo.TblItems ON dbo.TblInvoiceBody.ItemID = dbo.TblItems.ItemID INNER JOIN
                         dbo.TblResources ON dbo.TblInvoicesHeader.ResID = dbo.TblResources.ResID INNER JOIN
                         dbo.TblCities ON dbo.TblResources.CityID = dbo.TblCities.CityID INNER JOIN
                         dbo.TblUnits ON dbo.TblItems.UnitID = dbo.TblUnits.UnitID
GO
/****** Object:  View [dbo].[NamesView]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[NamesView]
AS
SELECT        PatientID, PatientName, Treat, Implant, Mobile, Ortho, Struc
FROM            dbo.patient
GO
/****** Object:  View [dbo].[vw_ContactBalances]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_ContactBalances]
AS
SELECT        c.ContactID, c.CName, SUM(CASE WHEN l.Direction = 'Lend' THEN l.Amount ELSE - l.Amount END) AS TotalLoanedOrBorrowed, SUM(CASE WHEN l.Direction = 'Lend' THEN ISNULL(r.Amount, 0) ELSE - ISNULL(r.Amount, 0) 
                         END) AS TotalRepaid, SUM(CASE WHEN l.Direction = 'Lend' THEN l.Amount - ISNULL(r.Amount, 0) ELSE - (l.Amount - ISNULL(r.Amount, 0)) END) AS NetBalance, 
                         CASE WHEN SUM(CASE WHEN l.Direction = 'Lend' THEN l.Amount - ISNULL(r.Amount, 0) ELSE - (l.Amount - ISNULL(r.Amount, 0)) END) 
                         > 0 THEN 'They owe you' WHEN SUM(CASE WHEN l.Direction = 'Lend' THEN l.Amount - ISNULL(r.Amount, 0) ELSE - (l.Amount - ISNULL(r.Amount, 0)) END) < 0 THEN 'You owe them' ELSE 'Settled' END AS Status
FROM            dbo.Contacts AS c LEFT OUTER JOIN
                         dbo.Loans AS l ON l.ContactID = c.ContactID LEFT OUTER JOIN
                         dbo.Repayments AS r ON r.LoanID = l.LoanID
GROUP BY c.ContactID, c.CName
GO
/****** Object:  Table [dbo].[Patient_RX]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_RX](
	[RxID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[RXDate] [smalldatetime] NULL,
	[RX] [nvarchar](500) NULL,
 CONSTRAINT [PK_patient_RX] PRIMARY KEY CLUSTERED 
(
	[RxID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PatientRXVIEW]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PatientRXVIEW]
AS
SELECT        dbo.Patient_RX.RxID, dbo.Patient.PatientID, dbo.Patient.PatientName, dbo.Patient.Sex, dbo.Patient.Age, dbo.Patient_RX.RXDate, dbo.Patient_RX.RX
FROM            dbo.Patient INNER JOIN
                         dbo.Patient_RX ON dbo.Patient.PatientID = dbo.Patient_RX.PatientID
GO
/****** Object:  Table [dbo].[RxBody]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RxBody](
	[RxBdyID] [int] NOT NULL,
	[ArHdrName] [nvarchar](150) NULL,
	[ArHdrAdres] [nvarchar](400) NULL,
	[EnHdrName] [nvarchar](150) NULL,
	[EnHdrAdres] [nvarchar](400) NULL,
	[Logo] [image] NULL,
	[Detail] [nvarchar](350) NULL,
	[ArFtr] [nvarchar](150) NULL,
	[EnFtr] [nvarchar](150) NULL,
	[WtrImg] [image] NULL,
	[WtrText] [nvarchar](50) NULL,
	[UseWtrImg] [bit] NULL,
	[UseWtrText] [bit] NULL,
	[DrName] [nvarchar](100) NULL,
 CONSTRAINT [PK_RxBody] PRIMARY KEY CLUSTERED 
(
	[RxBdyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[Rx_BdyVW]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Rx_BdyVW]
AS
SELECT        dbo.RxBody.RxBdyID, dbo.Patient.PatientID, dbo.Patient.PatientName, dbo.Patient.Sex, YEAR(GETDATE()) - dbo.Patient.BirthY AS BirthY, dbo.Patient_RX.RxID, dbo.Patient_RX.RXDate, dbo.Patient_RX.RX, 
                         dbo.RxBody.EnHdrName, dbo.RxBody.EnHdrAdres, dbo.RxBody.Logo, dbo.RxBody.Detail, dbo.RxBody.ArHdrName, dbo.RxBody.ArHdrAdres, dbo.RxBody.EnFtr, dbo.RxBody.ArFtr, dbo.RxBody.DrName, dbo.RxBody.WtrImg, 
                         dbo.RxBody.WtrText, dbo.RxBody.UseWtrImg, dbo.RxBody.UseWtrText
FROM            dbo.Patient_RX INNER JOIN
                         dbo.Patient ON dbo.Patient_RX.PatientID = dbo.Patient.PatientID CROSS JOIN
                         dbo.RxBody
GO
/****** Object:  Table [dbo].[Visits]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visits](
	[VisitDetID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[VtID] [int] NULL,
	[VisitDay] [nvarchar](10) NOT NULL,
	[VisTime] [nvarchar](10) NULL,
	[VisTimeEnd] [nvarchar](50) NULL,
	[PatientName] [nvarchar](50) NULL,
	[VisDetail] [nvarchar](50) NULL,
	[VisNotes] [nvarchar](50) NULL,
	[VisDateTime] [datetime] NULL,
 CONSTRAINT [PK_Visits] PRIMARY KEY CLUSTERED 
(
	[VisitDetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VisitVW]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VisitVW]
AS
SELECT     VisitDetID, VisitDay, VisTime, PatientName, PatientID
FROM         dbo.Visits
GO
/****** Object:  Table [dbo].[Products]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[ProductDescription] [nvarchar](max) NULL,
	[UnitID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[ReorderLevel] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Batchess]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batchess](
	[BatchID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[SupplierID] [int] NOT NULL,
	[BatchNumber] [nvarchar](100) NOT NULL,
	[ManufacturingDate] [date] NULL,
	[ExpirationDate] [date] NOT NULL,
	[ReceivedQuantity] [int] NOT NULL,
	[CurrentQuantity] [int] NOT NULL,
	[Barcode] [nvarchar](100) NULL,
 CONSTRAINT [PK_Batchess] PRIMARY KEY CLUSTERED 
(
	[BatchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ExpiringBatchessView]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ExpiringBatchessView]
AS
SELECT 
    b.BatchNumber,
    p.ProductName,
    b.ExpirationDate,
    b.CurrentQuantity,
    DATEDIFF(DAY, GETDATE(), b.ExpirationDate) AS DaysUntilExpiration
FROM Batchess b
JOIN Products p ON b.ProductID = p.ProductID
WHERE b.CurrentQuantity > 0 
AND b.ExpirationDate BETWEEN GETDATE() AND DATEADD(DAY, 90, GETDATE());
GO
/****** Object:  Table [dbo].[Units]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[UnitID] [int] IDENTITY(1,1) NOT NULL,
	[UnitName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[UnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UnitName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ProductStockView]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProductStockView]
AS
SELECT 
    p.ProductID,
    p.ProductName,
    u.UnitName,
    SUM(b.CurrentQuantity) AS TotalStock,
    p.ReorderLevel
FROM Products p
JOIN Batchess b ON p.ProductID = b.ProductID
JOIN Units u ON p.UnitID = u.UnitID
GROUP BY p.ProductID, p.ProductName, u.UnitName, p.ReorderLevel;
GO
/****** Object:  Table [dbo].[AppointmentC]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppointmentC](
	[AppointmentID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[DrID] [int] NOT NULL,
	[AppDate] [smalldatetime] NOT NULL,
	[StartDateTime] [datetime2](7) NOT NULL,
	[EndDateTime] [datetime2](7) NOT NULL,
	[Reason] [nvarchar](200) NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AppointmentC] PRIMARY KEY CLUSTERED 
(
	[AppointmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppSetngs]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppSetngs](
	[SettingKey] [nvarchar](100) NOT NULL,
	[SettingValue] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_AppSetngs] PRIMARY KEY CLUSTERED 
(
	[SettingKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditLog]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[ActionType] [varchar](50) NULL,
	[TableName] [varchar](255) NULL,
	[RecordID] [int] NULL,
	[OldValue] [varchar](max) NULL,
	[NewValue] [varchar](max) NULL,
	[ChangedBy] [varchar](255) NULL,
	[ChangeDate] [datetime] NULL,
 CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Barcodes]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Barcodes](
	[BarcodeID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[BarcodeValue] [nvarchar](100) NOT NULL,
	[BarcodeType] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Barcodes] PRIMARY KEY CLUSTERED 
(
	[BarcodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[BarcodeValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BuyInvoiceLineItems]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuyInvoiceLineItems](
	[LineItemID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[BatchNumber] [nvarchar](100) NULL,
	[ExpirationDate] [date] NULL,
 CONSTRAINT [PK_BuyInvoiceLineItems] PRIMARY KEY CLUSTERED 
(
	[LineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BuyInvoices]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuyInvoices](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[InvoiceDate] [date] NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[DueDate] [date] NOT NULL,
	[InvoiceStatus] [nvarchar](20) NOT NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_BuyInvoices] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[DrID] [int] IDENTITY(1,1) NOT NULL,
	[DrName] [nvarchar](50) NOT NULL,
	[DrAdres] [nvarchar](150) NULL,
	[DrPhone] [char](10) NULL,
	[DrMobile] [char](10) NULL,
	[DrColor] [nvarchar](50) NULL,
 CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
(
	[DrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DrWork]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DrWork](
	[WorkID] [int] IDENTITY(1,1) NOT NULL,
	[DrID] [int] NOT NULL,
	[PatientID] [int] NULL,
	[WrkDate] [datetime] NULL,
	[WrkDetail] [nvarchar](50) NULL,
	[WrkVal] [money] NULL,
	[PayVal] [money] NULL,
	[Imp] [bit] NULL,
	[Orth] [bit] NULL,
	[Surg] [bit] NULL,
	[Notes] [nvarchar](50) NULL,
 CONSTRAINT [PK_DrWork] PRIMARY KEY CLUSTERED 
(
	[WorkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DrWorkPay]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DrWorkPay](
	[WorkPayID] [int] IDENTITY(1,1) NOT NULL,
	[WorkID] [int] NOT NULL,
	[DrID] [int] NOT NULL,
	[PayValue] [money] NOT NULL,
	[PayDate] [datetime] NOT NULL,
	[Notes] [nvarchar](50) NULL,
 CONSTRAINT [PK_DrWorkPay] PRIMARY KEY CLUSTERED 
(
	[WorkPayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Emp]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emp](
	[EmpID] [int] IDENTITY(1,1) NOT NULL,
	[EmpName] [nvarchar](50) NULL,
	[EmpPhone] [nchar](10) NULL,
	[EmpAddress] [nvarchar](50) NULL,
	[EmpImg] [image] NULL,
 CONSTRAINT [PK_Emp] PRIMARY KEY CLUSTERED 
(
	[EmpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forms]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forms](
	[FormID] [int] IDENTITY(1,1) NOT NULL,
	[FormName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_Forms] PRIMARY KEY CLUSTERED 
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[FormName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[SID] [int] IDENTITY(1,1) NOT NULL,
	[Sex] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Health]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Health](
	[HID] [int] IDENTITY(1,1) NOT NULL,
	[HealthStat] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Health] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Imags]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Imags](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[IMG] [varbinary](max) NULL,
	[Height] [float] NULL,
	[Width] [float] NULL,
	[Sze] [float] NULL,
	[DatePictureTaken] [datetime] NULL,
	[EquipmentMaker] [varchar](150) NULL,
	[EquipmentModel] [varchar](150) NULL,
	[Thumbnail] [varbinary](max) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_Imags] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImpClrs]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImpClrs](
	[ImpClrID] [int] IDENTITY(1,1) NOT NULL,
	[ImpClr] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ImpClrs] PRIMARY KEY CLUSTERED 
(
	[ImpClrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImplantDiameter]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImplantDiameter](
	[DiameterID] [int] IDENTITY(1,1) NOT NULL,
	[DiameterMM] [decimal](4, 2) NOT NULL,
 CONSTRAINT [PK_ImplantDiameter] PRIMARY KEY CLUSTERED 
(
	[DiameterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImplantLength]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImplantLength](
	[LengthID] [int] IDENTITY(1,1) NOT NULL,
	[LengthMM] [decimal](4, 2) NOT NULL,
 CONSTRAINT [PK_ImplantLength] PRIMARY KEY CLUSTERED 
(
	[LengthID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImprDet]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImprDet](
	[ImpDetID] [int] IDENTITY(1,1) NOT NULL,
	[imprID] [int] NOT NULL,
	[ImprDetail] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ImprDet] PRIMARY KEY CLUSTERED 
(
	[ImpDetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Impression]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Impression](
	[ImprID] [int] IDENTITY(1,1) NOT NULL,
	[ImprType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Impression] PRIMARY KEY CLUSTERED 
(
	[ImprID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lab]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lab](
	[LabID] [int] IDENTITY(1,1) NOT NULL,
	[LabName] [nvarchar](50) NOT NULL,
	[Adres] [nvarchar](50) NULL,
	[Phone] [nchar](10) NULL,
	[Mobile] [nchar](10) NULL,
 CONSTRAINT [PK_Lab] PRIMARY KEY CLUSTERED 
(
	[LabID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LabOrder]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LabOrder](
	[LabOrderID] [int] IDENTITY(1,1) NOT NULL,
	[LabID] [int] NOT NULL,
	[PatientID] [int] NOT NULL,
	[ImprType] [nvarchar](150) NOT NULL,
	[ImprDet] [nvarchar](150) NOT NULL,
	[ImprClr] [nvarchar](150) NOT NULL,
	[ImprCount] [int] NOT NULL,
	[DeliveryDate] [datetime] NULL,
	[Price] [int] NOT NULL,
	[RecieveDate] [datetime] NULL,
	[Notes] [nvarchar](150) NULL,
 CONSTRAINT [PK_LabOrder] PRIMARY KEY CLUSTERED 
(
	[LabOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LabPay]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LabPay](
	[LabPayID] [int] IDENTITY(1,1) NOT NULL,
	[LabID] [int] NOT NULL,
	[LabOrderID] [int] NOT NULL,
	[PayValue] [int] NOT NULL,
	[PayDate] [datetime] NOT NULL,
	[PayDetail] [nvarchar](50) NULL,
	[Notes] [nvarchar](50) NULL,
 CONSTRAINT [PK_LabPay] PRIMARY KEY CLUSTERED 
(
	[LabPayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LD]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LD](
	[LDID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[LD1] [nvarchar](150) NULL,
	[LD2] [nvarchar](150) NULL,
	[LD3] [nvarchar](150) NULL,
	[LD4] [nvarchar](150) NULL,
	[LD5] [nvarchar](150) NULL,
	[LD6] [nvarchar](150) NULL,
	[LD7] [nvarchar](150) NULL,
	[LD8] [nvarchar](150) NULL,
 CONSTRAINT [PK_LD] PRIMARY KEY CLUSTERED 
(
	[LDID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LDPL]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LDPL](
	[LDcellID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[CellAddres] [int] NOT NULL,
	[ForeColor] [nvarchar](50) NULL,
 CONSTRAINT [PK_LDPL] PRIMARY KEY CLUSTERED 
(
	[LDcellID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LDSTYLE]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LDSTYLE](
	[LDcellID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[CellAddres] [int] NOT NULL,
	[BakImg] [image] NOT NULL,
	[ImgName] [nvarchar](150) NULL,
	[ColName] [nvarchar](50) NULL,
	[RowIndex] [int] NULL,
 CONSTRAINT [PK_LDSTYLE] PRIMARY KEY CLUSTERED 
(
	[LDcellID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LU]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LU](
	[LUID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[LU1] [nvarchar](150) NULL,
	[LU2] [nvarchar](150) NULL,
	[LU3] [nvarchar](150) NULL,
	[LU4] [nvarchar](150) NULL,
	[LU5] [nvarchar](150) NULL,
	[LU6] [nvarchar](150) NULL,
	[LU7] [nvarchar](150) NULL,
	[LU8] [nvarchar](150) NULL,
 CONSTRAINT [PK_LU] PRIMARY KEY CLUSTERED 
(
	[LUID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LUPL]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LUPL](
	[LUcellID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[CellAddres] [int] NOT NULL,
	[ForeColor] [nvarchar](50) NULL,
 CONSTRAINT [PK_LUPL] PRIMARY KEY CLUSTERED 
(
	[LUcellID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LUSTYLE]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LUSTYLE](
	[LUcellID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[CellAddres] [int] NOT NULL,
	[BakImg] [image] NOT NULL,
	[ImgName] [nvarchar](150) NULL,
	[ColName] [nvarchar](50) NULL,
	[RowIndex] [int] NULL,
 CONSTRAINT [PK_LUSTYLE] PRIMARY KEY CLUSTERED 
(
	[LUcellID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicineDoze]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicineDoze](
	[DozeID] [int] IDENTITY(1,1) NOT NULL,
	[ShapeID] [int] NOT NULL,
	[Doze] [nvarchar](50) NULL,
 CONSTRAINT [PK_MedicineDoze] PRIMARY KEY CLUSTERED 
(
	[DozeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicineFamily]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicineFamily](
	[SubCatID] [int] IDENTITY(1,1) NOT NULL,
	[MedicineID] [int] NOT NULL,
	[MedicineSubCat] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MedicineFamily] PRIMARY KEY CLUSTERED 
(
	[SubCatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicineGroups]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicineGroups](
	[MedicineID] [int] IDENTITY(1,1) NOT NULL,
	[MedicineFamily] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MedicineGroups] PRIMARY KEY CLUSTERED 
(
	[MedicineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicineItems]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicineItems](
	[MedicineItemID] [int] IDENTITY(1,1) NOT NULL,
	[ScincID] [int] NOT NULL,
	[CommName] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NULL,
	[Notes] [nvarchar](150) NULL,
 CONSTRAINT [PK_MedicineItems] PRIMARY KEY CLUSTERED 
(
	[MedicineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicineShape]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicineShape](
	[ShapeID] [int] IDENTITY(1,1) NOT NULL,
	[MedicineItemID] [int] NOT NULL,
	[MedicineShape] [nvarchar](50) NULL,
	[ShapeInfo] [nvarchar](50) NULL,
 CONSTRAINT [PK_MedicineShape] PRIMARY KEY CLUSTERED 
(
	[ShapeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedScienceFamily]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedScienceFamily](
	[ScincID] [int] IDENTITY(1,1) NOT NULL,
	[SubCatID] [int] NOT NULL,
	[ScienceName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MedScienceFamily] PRIMARY KEY CLUSTERED 
(
	[ScincID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MobileStructure]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MobileStructure](
	[StructureID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[Arch] [char](1) NULL,
	[ToothNum] [tinyint] NULL,
	[IsMissing] [bit] NULL,
	[ImplantPresent] [bit] NULL,
	[CrownPresent] [bit] NULL,
	[BridgePresent] [bit] NULL,
	[StrucName] [nvarchar](100) NULL,
	[StrucType] [nvarchar](100) NULL,
	[TeethType] [nvarchar](100) NULL,
	[StrucDate] [smalldatetime] NULL,
	[ToothLoc] [nvarchar](50) NULL,
	[ToothNumbers] [nvarchar](150) NULL,
	[AddToothDate] [smalldatetime] NULL,
	[MobilityLevel] [tinyint] NULL,
	[HasRestoration] [bit] NULL,
	[PocketDepth] [float] NULL,
	[BleedingOnProbing] [bit] NULL,
	[AttachmentLoss] [float] NULL,
	[FurcationInvolvement] [tinyint] NULL,
	[CrownCondition] [nvarchar](50) NULL,
	[Notes] [nvarchar](200) NULL,
 CONSTRAINT [PK_MobileStructure] PRIMARY KEY CLUSTERED 
(
	[StructureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrthoDiag]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrthoDiag](
	[DiagID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[CloseType] [nvarchar](150) NULL,
	[ClassI] [nvarchar](150) NULL,
	[Bite] [nvarchar](150) NULL,
 CONSTRAINT [PK_OrthoDiag] PRIMARY KEY CLUSTERED 
(
	[DiagID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrthoInf]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrthoInf](
	[OrthoID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[Compliants] [nvarchar](50) NULL,
	[Birth] [nvarchar](50) NULL,
	[Feed] [nvarchar](50) NULL,
	[MilkTeethChng] [nvarchar](50) NULL,
	[MilkTeethAppear] [nvarchar](50) NULL,
	[TeethLoss] [nvarchar](50) NULL,
	[BurriedTeeth] [nvarchar](50) NULL,
	[OverLoadTeeth] [nvarchar](50) NULL,
	[LipsCut] [nvarchar](50) NULL,
	[ThroatCut] [nvarchar](50) NULL,
	[IllnesPeriod] [nvarchar](50) NULL,
	[CousinsHFactor] [nvarchar](50) NULL,
	[BadHabits] [nvarchar](50) NULL,
	[Malfunction] [nvarchar](50) NULL,
	[Khota] [nvarchar](150) NULL,
	[PrevOrth] [nvarchar](50) NULL,
	[PrevIll] [nvarchar](50) NULL,
 CONSTRAINT [PK_OrthoInf] PRIMARY KEY CLUSTERED 
(
	[OrthoID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrthoTreat]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrthoTreat](
	[TreatID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[BeginDate] [smalldatetime] NULL,
	[OrthoType] [nvarchar](50) NULL,
	[ExtraUL] [nvarchar](50) NULL,
	[ExtraLL] [nvarchar](50) NULL,
	[ExtraUR] [nvarchar](50) NULL,
	[ExtraLR] [nvarchar](50) NULL,
	[FixerDate] [smalldatetime] NULL,
	[FixerType] [nvarchar](50) NULL,
	[BraketType] [nvarchar](50) NULL,
	[FinishDate] [smalldatetime] NULL,
 CONSTRAINT [PK_OrthoTreat] PRIMARY KEY CLUSTERED 
(
	[TreatID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrthoTrtDet]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrthoTrtDet](
	[DetID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[WorkDate] [smalldatetime] NULL,
	[WireMeasure] [nvarchar](50) NULL,
	[WireType] [nvarchar](50) NULL,
	[WireImg] [nvarchar](50) NULL,
 CONSTRAINT [PK_OrthoTrtDet] PRIMARY KEY CLUSTERED 
(
	[DetID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_ExternalToothTrt]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_ExternalToothTrt](
	[ExternalTrtID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[ToothNum] [tinyint] NOT NULL,
	[TreatmentType] [nvarchar](100) NULL,
	[ClinicName] [nvarchar](100) NULL,
	[TreatmentDate] [smalldatetime] NULL,
	[Notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_Patient_ExternalToothTrt] PRIMARY KEY CLUSTERED 
(
	[ExternalTrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_Imgs]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_Imgs](
	[PicID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[PicName] [nvarchar](50) NULL,
	[PicPath] [nvarchar](50) NULL,
	[FullName] [nvarchar](512) NULL,
 CONSTRAINT [PK_patient_imgs] PRIMARY KEY CLUSTERED 
(
	[PicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_Mobile]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_Mobile](
	[MobID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NULL,
	[ShapeID] [int] NULL,
	[ToothNums] [nvarchar](200) NULL,
	[ToothNum] [tinyint] NULL,
	[ToothName] [nvarchar](50) NULL,
	[LVL] [tinyint] NULL,
	[PropertyName] [nvarchar](100) NULL,
	[FillColor] [nvarchar](50) NULL,
	[BorderThickness] [tinyint] NULL,
	[BorderColor] [nvarchar](50) NULL,
	[TreatmentType] [nvarchar](100) NULL,
	[TreatDate] [smalldatetime] NULL,
	[Treat] [nvarchar](250) NULL,
	[TreatPlan] [nvarchar](500) NULL,
	[TreatDetails] [nvarchar](500) NULL,
	[TreatNotes] [nvarchar](500) NULL,
	[Finished] [tinyint] NULL,
	[TreatEndDate] [smalldatetime] NULL,
	[QrtrTable] [nvarchar](50) NULL,
	[QrtrID] [int] NULL,
	[QrtrAddress] [int] NULL,
	[IsExternal] [bit] NULL,
	[ExternalClinicName] [nvarchar](100) NULL,
	[ExternalTreatmentDate] [smalldatetime] NULL,
	[IsPaid] [bit] NULL,
	[IsMultiTrt] [bit] NULL,
	[ParentToothTrtID] [int] NULL,
	[TrtGroupID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Patient_Mobile] PRIMARY KEY CLUSTERED 
(
	[MobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_MobInfo]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_MobInfo](
	[MobInfoID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NULL,
	[ParentToothTrtID] [int] NULL,
	[TrtGroupID] [uniqueidentifier] NULL,
	[ToothNum] [tinyint] NULL,
	[ToothName] [nvarchar](100) NULL,
	[TreatDate] [smalldatetime] NULL,
	[Treat] [nvarchar](250) NULL,
	[TreatNotes] [nvarchar](500) NULL,
	[IsExternal] [bit] NULL,
	[ExternalClinicName] [nvarchar](100) NULL,
	[ExternalTreatmentDate] [smalldatetime] NULL,
 CONSTRAINT [PK_Patient_MobInfo] PRIMARY KEY CLUSTERED 
(
	[MobInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_MobStruc]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_MobStruc](
	[StrucID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[StrucName] [nvarchar](50) NULL,
	[StrucType] [nvarchar](50) NULL,
	[TeethType] [nvarchar](50) NULL,
	[StrucDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Patient_MobStruc] PRIMARY KEY CLUSTERED 
(
	[StrucID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_MobStrucAdd]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_MobStrucAdd](
	[AddTothID] [int] IDENTITY(1,1) NOT NULL,
	[StrucID] [int] NOT NULL,
	[StrucName] [nvarchar](50) NULL,
	[ToothLoc] [nvarchar](50) NULL,
	[ToothNum] [nvarchar](50) NULL,
	[AddTothDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Patient_MobStrucAdd] PRIMARY KEY CLUSTERED 
(
	[AddTothID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_Notes]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_Notes](
	[NoteID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[NoteDate] [smalldatetime] NULL,
	[Note] [nvarchar](250) NULL,
 CONSTRAINT [PK_Patient_Notes] PRIMARY KEY CLUSTERED 
(
	[NoteID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_OtherTRT]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_OtherTRT](
	[OtherTrtID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[Trt] [nvarchar](250) NOT NULL,
	[TrtDate] [smalldatetime] NOT NULL,
	[TrtDetails] [nvarchar](500) NULL,
	[IsPaid] [bit] NOT NULL,
 CONSTRAINT [PK_Patient_OtherTRT] PRIMARY KEY CLUSTERED 
(
	[OtherTrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_ToothChart]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_ToothChart](
	[PatientID] [int] NOT NULL,
	[TrtID] [int] IDENTITY(1,1) NOT NULL,
	[ToothNum] [tinyint] NOT NULL,
	[ToothName] [nvarchar](50) NOT NULL,
	[IsExist] [bit] NOT NULL,
	[HasImp] [bit] NOT NULL,
	[ImpType] [nvarchar](50) NULL,
	[ImpName] [nvarchar](50) NULL,
	[ImpDate] [datetime] NULL,
	[CrownType] [nvarchar](50) NULL,
	[CrownName] [nvarchar](50) NULL,
	[CrownDate] [datetime] NULL,
	[TrtDate] [datetime] NOT NULL,
	[TrtName] [nvarchar](100) NOT NULL,
	[ClassID] [tinyint] NOT NULL,
	[ClassName] [nvarchar](100) NOT NULL,
	[TrtStage] [nvarchar](50) NULL,
	[TrtDetails] [nvarchar](150) NULL,
	[TrtNotes] [nvarchar](150) NULL,
 CONSTRAINT [PK_Patient_ToothChart] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC,
	[TrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_ToothCheck]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_ToothCheck](
	[ToothNum] [tinyint] NOT NULL,
	[PatientID] [int] NOT NULL,
	[ToothName] [nvarchar](50) NOT NULL,
	[IsExist] [tinyint] NOT NULL,
	[COM_1] [tinyint] NOT NULL,
	[COM_2M] [tinyint] NOT NULL,
	[COM_2D] [tinyint] NOT NULL,
	[COM_2MOD] [tinyint] NOT NULL,
	[COM_3M] [tinyint] NOT NULL,
	[COM_4] [tinyint] NOT NULL,
	[COM_5] [tinyint] NOT NULL,
	[COM_FACING] [tinyint] NOT NULL,
	[COM_3D] [tinyint] NOT NULL,
	[RCT] [tinyint] NOT NULL,
	[RCC] [tinyint] NOT NULL,
	[RCC_NS] [tinyint] NOT NULL,
	[RCF] [tinyint] NOT NULL,
	[RCF_COM] [tinyint] NOT NULL,
	[EXTRACTION] [tinyint] NOT NULL,
	[PULPOTOMY] [tinyint] NOT NULL,
	[PULPECTOMY] [tinyint] NOT NULL,
	[INDIRECT_PC] [tinyint] NOT NULL,
	[DIRECT_PC] [tinyint] NOT NULL,
	[FIBER_POST] [tinyint] NOT NULL,
	[TF] [tinyint] NOT NULL,
	[REDO_RCC] [tinyint] NOT NULL,
	[AMALGM] [tinyint] NOT NULL,
	[RCT_NECROTIC] [tinyint] NOT NULL,
	[PULPOTOMY_MTA] [tinyint] NOT NULL,
	[ABCESS_DRAINAGE] [tinyint] NOT NULL,
	[MTA_Bulk_Flow] [tinyint] NOT NULL,
	[Build_Up_Com] [tinyint] NOT NULL,
	[Build_Up_Acr] [tinyint] NOT NULL,
	[Build_Up_GI] [tinyint] NOT NULL,
	[GI_M] [tinyint] NOT NULL,
	[GI] [tinyint] NOT NULL,
	[GI_D] [tinyint] NOT NULL,
	[CheckDate] [datetime] NULL,
	[CheckNotes] [nvarchar](150) NULL,
 CONSTRAINT [PK_Patient_ToothCheck] PRIMARY KEY CLUSTERED 
(
	[ToothNum] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_ToothTrt]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_ToothTrt](
	[ToothTrtID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NULL,
	[ShapeID] [int] NULL,
	[ToothNum] [tinyint] NULL,
	[ToothName] [nvarchar](50) NULL,
	[LVL] [tinyint] NULL,
	[PropertyName] [nvarchar](100) NULL,
	[FillColor] [nvarchar](50) NULL,
	[BorderThickness] [tinyint] NULL,
	[BorderColor] [nvarchar](50) NULL,
	[TreatmentType] [nvarchar](100) NULL,
	[TreatDate] [smalldatetime] NULL,
	[Treat] [nvarchar](250) NULL,
	[TreatPlan] [nvarchar](500) NULL,
	[TreatDetails] [nvarchar](500) NULL,
	[TreatNotes] [nvarchar](500) NULL,
	[Finished] [tinyint] NULL,
	[TreatEndDate] [smalldatetime] NULL,
	[QrtrTable] [nvarchar](50) NULL,
	[TrtLoc] [nvarchar](50) NULL,
	[QrtrID] [int] NULL,
	[QrtrAddress] [int] NULL,
	[QrtrColumnName] [nvarchar](50) NULL,
	[QrtrColumnValue] [nvarchar](500) NULL,
	[IsExternal] [bit] NULL,
	[ExternalClinicName] [nvarchar](100) NULL,
	[ExternalTreatmentDate] [smalldatetime] NULL,
	[IsPaid] [bit] NULL,
	[IsMultiTrt] [bit] NULL,
	[ParentToothTrtID] [int] NULL,
	[TrtGroupID] [uniqueidentifier] NULL,
	[isOnImplant] [bit] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_Patient_ToothTrt] PRIMARY KEY CLUSTERED 
(
	[ToothTrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_TrtInfo]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_TrtInfo](
	[TrtInfoID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NULL,
	[ParentToothTrtID] [int] NULL,
	[TrtGroupID] [uniqueidentifier] NULL,
	[ToothNum] [tinyint] NULL,
	[ToothName] [nvarchar](100) NULL,
	[TreatDate] [smalldatetime] NULL,
	[Treat] [nvarchar](250) NULL,
	[TreatNotes] [nvarchar](500) NULL,
	[IsExternal] [bit] NULL,
	[ExternalClinicName] [nvarchar](100) NULL,
	[ExternalTreatmentDate] [smalldatetime] NULL,
 CONSTRAINT [PK_Patient_TrtInfo] PRIMARY KEY CLUSTERED 
(
	[TrtInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_TrtScope]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_TrtScope](
	[ScopeID] [int] IDENTITY(1,1) NOT NULL,
	[TrtID] [int] NOT NULL,
	[MobID] [int] NOT NULL,
	[LVL] [tinyint] NULL,
	[ToothCode] [nvarchar](50) NULL,
	[ToothNum] [tinyint] NULL,
	[PropertyName] [nvarchar](100) NULL,
	[Treat] [nvarchar](250) NULL,
	[FillColor] [nvarchar](50) NULL,
	[BorderColor] [nvarchar](50) NULL,
	[ExternalClinicName] [nvarchar](100) NULL,
	[Notes] [nvarchar](250) NULL,
	[IsExternal] [bit] NULL,
 CONSTRAINT [PK_Patient_TrtScope] PRIMARY KEY CLUSTERED 
(
	[ScopeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientColors]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientColors](
	[ColorID] [int] IDENTITY(1,1) NOT NULL,
	[Color1] [nvarchar](9) NULL,
	[Color2] [nvarchar](9) NULL,
	[GradientIndex] [tinyint] NULL,
	[AlphaValue] [int] NULL,
	[PatientID] [int] NULL,
 CONSTRAINT [PK_PatientColors] PRIMARY KEY CLUSTERED 
(
	[ColorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientHistory]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientHistory](
	[HistoryID] [int] IDENTITY(1,1) NOT NULL,
	[ActionTime] [datetime] NOT NULL,
	[LoggedBy] [nvarchar](100) NULL,
	[PatientName] [nvarchar](200) NULL,
	[TableName] [nvarchar](100) NULL,
	[Action] [nvarchar](100) NULL,
	[AdditionalInfo] [nvarchar](max) NULL,
 CONSTRAINT [PK_PatientHistory] PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientNumberSequence]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientNumberSequence](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LastNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_PatientNumberSequence] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 10/28/2025 9:13:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentDate] [date] NOT NULL,
	[PaymentMethod] [nvarchar](20) NOT NULL,
	[ReferenceNumber] [nvarchar](100) NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Raseed]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Raseed](
	[PatientID] [int] NOT NULL,
	[Bal]  AS (isnull([dbo].[TotalPatientPays]([PatientID])-[dbo].[TotalPatientTrts]([PatientID]),(0))),
 CONSTRAINT [PK_Raseed] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RD]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RD](
	[RDID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[RD1] [nvarchar](150) NULL,
	[RD2] [nvarchar](150) NULL,
	[RD3] [nvarchar](150) NULL,
	[RD4] [nvarchar](150) NULL,
	[RD5] [nvarchar](150) NULL,
	[RD6] [nvarchar](150) NULL,
	[RD7] [nvarchar](150) NULL,
	[RD8] [nvarchar](150) NULL,
 CONSTRAINT [PK_RD] PRIMARY KEY CLUSTERED 
(
	[RDID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RDPL]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RDPL](
	[RDcellID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[CellAddres] [int] NOT NULL,
	[ForeColor] [nvarchar](50) NULL,
 CONSTRAINT [PK_RDPL] PRIMARY KEY CLUSTERED 
(
	[RDcellID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RDSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RDSTYLE](
	[RDcellID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[CellAddres] [int] NOT NULL,
	[BakImg] [image] NOT NULL,
	[ImgName] [nvarchar](150) NULL,
	[ColName] [nvarchar](50) NULL,
	[RowIndex] [int] NULL,
 CONSTRAINT [PK_RDSTYLE] PRIMARY KEY CLUSTERED 
(
	[RDcellID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RU](
	[RUID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[RU1] [nvarchar](150) NULL,
	[RU2] [nvarchar](150) NULL,
	[RU3] [nvarchar](150) NULL,
	[RU4] [nvarchar](150) NULL,
	[RU5] [nvarchar](150) NULL,
	[RU6] [nvarchar](150) NULL,
	[RU7] [nvarchar](150) NULL,
	[RU8] [nvarchar](150) NULL,
 CONSTRAINT [PK_RU] PRIMARY KEY CLUSTERED 
(
	[RUID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RUPL]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RUPL](
	[RUcellID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[CellAddres] [int] NOT NULL,
	[ForeColor] [nvarchar](50) NULL,
 CONSTRAINT [PK_RUPL] PRIMARY KEY CLUSTERED 
(
	[RUcellID] ASC,
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RUSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RUSTYLE](
	[RUcellID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[CellAddres] [int] NOT NULL,
	[BakImg] [image] NOT NULL,
	[ImgName] [nvarchar](150) NULL,
	[ColName] [nvarchar](50) NULL,
	[RowIndex] [int] NULL,
 CONSTRAINT [PK_RUSTYLE] PRIMARY KEY CLUSTERED 
(
	[RUcellID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RxFly]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RxFly](
	[RxID] [int] IDENTITY(1,1) NOT NULL,
	[PatientName] [nvarchar](50) NULL,
	[PatientAge] [int] NULL,
	[RxDate] [smalldatetime] NULL,
	[RX] [nvarchar](500) NULL,
 CONSTRAINT [PK_RxFly] PRIMARY KEY CLUSTERED 
(
	[RxID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Secretaries]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Secretaries](
	[SecID] [int] IDENTITY(1,1) NOT NULL,
	[SecName] [nvarchar](50) NOT NULL,
	[SecAdres] [nvarchar](150) NULL,
	[SecPhone] [char](10) NULL,
	[SecMobile] [char](10) NULL,
	[SecColor] [nvarchar](50) NULL,
 CONSTRAINT [PK_Secretaries] PRIMARY KEY CLUSTERED 
(
	[SecID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shapes]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shapes](
	[ShapeID] [int] NOT NULL,
	[ShapeName] [nvarchar](50) NOT NULL,
	[ShapeDetail] [nvarchar](50) NULL,
	[OutID] [nvarchar](50) NULL,
	[TopID] [nvarchar](50) NULL,
	[INID] [nvarchar](50) NULL,
	[ShapeColor] [nvarchar](50) NULL,
 CONSTRAINT [PK_Shapes] PRIMARY KEY CLUSTERED 
(
	[ShapeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sheet1]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sheet1](
	[ShapeID] [int] NULL,
	[ShapeName] [varchar](100) NULL,
	[ShapeDetails] [varchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockMovements]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockMovements](
	[MovementID] [int] IDENTITY(1,1) NOT NULL,
	[BatchID] [int] NULL,
	[ProductID] [int] NOT NULL,
	[QuantityChange] [int] NOT NULL,
	[MovementType] [nvarchar](20) NOT NULL,
	[MovementDate] [datetime] NOT NULL,
	[Notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_StockMovements] PRIMARY KEY CLUSTERED 
(
	[MovementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [nvarchar](255) NOT NULL,
	[ContactPerson] [nvarchar](255) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[EmailAddress] [nvarchar](255) NULL,
	[PhysicalAddress] [nvarchar](500) NULL,
	[PaymentTerms] [nvarchar](500) NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Surgery]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Surgery](
	[SurgID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[SurgeryDet] [nvarchar](50) NULL,
	[SurDate] [smalldatetime] NULL,
 CONSTRAINT [PK_Surgery] PRIMARY KEY CLUSTERED 
(
	[SurgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblBnodMsareef]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblBnodMsareef](
	[BandID] [int] IDENTITY(1,1) NOT NULL,
	[BandName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblBnodMsareef] PRIMARY KEY CLUSTERED 
(
	[BandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblCustomers]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblCustomers](
	[CusID] [int] IDENTITY(1,1) NOT NULL,
	[CusName] [nvarchar](75) NULL,
	[CityID] [int] NULL,
	[Address] [nvarchar](500) NULL,
	[Contacts] [nvarchar](500) NULL,
 CONSTRAINT [PK_tblcustomers] PRIMARY KEY CLUSTERED 
(
	[CusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblExpenPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblExpenPay](
	[ExpPayID] [int] IDENTITY(1,1) NOT NULL,
	[MasrofID] [int] NOT NULL,
	[PayValue] [money] NOT NULL,
	[PayDate] [datetime] NOT NULL,
	[Notes] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblExpenPay] PRIMARY KEY CLUSTERED 
(
	[ExpPayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblMasareef]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblMasareef](
	[MasrofID] [int] IDENTITY(1,1) NOT NULL,
	[MasrofDate] [smalldatetime] NULL,
	[BandID] [int] NULL,
	[MasrofAmount] [money] NULL,
	[MasrofEx] [nvarchar](500) NULL,
 CONSTRAINT [PK_TblMasareef] PRIMARY KEY CLUSTERED 
(
	[MasrofID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblMeasure]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblMeasure](
	[MeasureID] [int] IDENTITY(1,1) NOT NULL,
	[Measure] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TblMeasure] PRIMARY KEY CLUSTERED 
(
	[MeasureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblMobTRT]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblMobTRT](
	[TrtID] [int] NOT NULL,
	[Trt] [nvarchar](50) NOT NULL,
	[ShapeID] [int] NULL,
	[TrtDetails] [nvarchar](150) NULL,
	[TrtLVL] [nvarchar](150) NULL,
	[TrtArDetails] [tinyint] NULL,
	[ToothID] [nvarchar](50) NULL,
	[ToothIDkID] [nvarchar](50) NULL,
	[OldTrt] [nvarchar](50) NULL,
	[TrtGroup] [nvarchar](50) NULL,
	[ParentGroup] [varchar](50) NULL,
	[TrtColor] [nvarchar](50) NULL,
	[KidTrt] [tinyint] NOT NULL,
	[TrtClrID] [int] NULL,
 CONSTRAINT [PK_TblMobTRT] PRIMARY KEY CLUSTERED 
(
	[TrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblOtherTRT]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblOtherTRT](
	[TblOtherTrtID] [int] IDENTITY(1,1) NOT NULL,
	[Trt] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_TblOtherTRT] PRIMARY KEY CLUSTERED 
(
	[TblOtherTrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblPaids]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblPaids](
	[PayID] [int] IDENTITY(1,1) NOT NULL,
	[PayType] [nvarchar](5) NULL,
	[PayDate] [smalldatetime] NULL,
	[ResCusId] [int] NULL,
	[PayAmount] [money] NULL,
	[PayEx] [nvarchar](300) NULL,
 CONSTRAINT [PK_TblPaids] PRIMARY KEY CLUSTERED 
(
	[PayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblSalesBody]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblSalesBody](
	[SaleID] [int] NULL,
	[ItemID] [int] NULL,
	[Quantity] [float] NULL,
	[Price] [money] NULL,
	[ItemHasm] [money] NULL,
	[Note] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblSalesHeader]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblSalesHeader](
	[SaleID] [int] IDENTITY(1,1) NOT NULL,
	[SaleType] [tinyint] NULL,
	[SaleDate] [smalldatetime] NULL,
	[CusID] [int] NULL,
	[DocNo] [int] NULL,
	[SaleEx] [nvarchar](500) NULL,
	[Hasm] [money] NULL,
 CONSTRAINT [PK_TblSalesHeader] PRIMARY KEY CLUSTERED 
(
	[SaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblStrucType]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblStrucType](
	[StrucTypeID] [int] IDENTITY(1,1) NOT NULL,
	[StrucName] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_TblStrucType] PRIMARY KEY CLUSTERED 
(
	[StrucTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblToothType]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblToothType](
	[ToothTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ToothType] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_TblToothType] PRIMARY KEY CLUSTERED 
(
	[ToothTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblTRTS]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblTRTS](
	[TrtID] [int] IDENTITY(1,1) NOT NULL,
	[Trt] [nvarchar](150) NOT NULL,
	[TrtValue] [money] NULL,
	[ShapeID] [int] NULL,
	[TrtAr] [nvarchar](150) NULL,
	[TrtArDetails] [nvarchar](150) NULL,
	[TrtDetails] [nvarchar](150) NULL,
	[TrtLVL] [nvarchar](150) NULL,
	[TrtLoc] [nvarchar](50) NULL,
	[ToothID] [nvarchar](50) NULL,
	[ToothIDkID] [nvarchar](50) NULL,
	[OldTrt] [nvarchar](50) NULL,
	[TrtGroup] [nvarchar](50) NULL,
	[ParentGroup] [varchar](50) NULL,
	[TrtColor] [nvarchar](50) NULL,
	[TrtBrdrClr] [nvarchar](50) NULL,
	[TrtBrdrThick] [tinyint] NULL,
	[KidTrt] [tinyint] NOT NULL,
	[TrtClrID] [int] NULL,
	[DefFillColor] [nvarchar](50) NULL,
	[DefBrdrColor] [nvarchar](50) NULL,
	[DefBrdrThick] [tinyint] NULL,
 CONSTRAINT [PK_TblTRTS] PRIMARY KEY CLUSTERED 
(
	[TrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblWireType]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblWireType](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[WireType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TblWireType] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsAtend]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsAtend](
	[AtndID] [int] IDENTITY(1,1) NOT NULL,
	[UsID] [int] NOT NULL,
	[AtnDay] [datetime] NOT NULL,
	[AtnNote] [nvarchar](50) NULL,
	[AbsPrsnt] [bit] NOT NULL,
 CONSTRAINT [PK_UsAtend] PRIMARY KEY CLUSTERED 
(
	[AtndID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsrPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsrPay](
	[UsSalID] [int] IDENTITY(1,1) NOT NULL,
	[UsID] [int] NOT NULL,
	[MonthPay] [int] NOT NULL,
	[DayPay] [int] NOT NULL,
	[FromDT] [datetime] NOT NULL,
	[ToDT] [datetime] NOT NULL,
	[DaysCount] [int] NOT NULL,
	[PayDate] [datetime] NOT NULL,
	[PayNote] [nvarchar](50) NULL,
 CONSTRAINT [PK_UsrPay] PRIMARY KEY CLUSTERED 
(
	[UsSalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorPays]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorPays](
	[PayID] [int] IDENTITY(1,1) NOT NULL,
	[SalesID] [int] NOT NULL,
	[VendID] [int] NULL,
	[PayValue] [money] NOT NULL,
	[PayDate] [smalldatetime] NOT NULL,
	[Notes] [nvarchar](50) NULL,
	[PayType] [nvarchar](50) NULL,
	[ChqOwner] [nvarchar](100) NULL,
	[AccountNumber] [nvarchar](100) NULL,
	[ChqNumber] [nchar](10) NULL,
	[ChqDueDate] [smalldatetime] NULL,
	[ChqBank] [nvarchar](100) NULL,
	[IsCashed] [bit] NULL,
	[IsForward] [bit] NULL,
	[ForwardFromTo] [nvarchar](250) NULL,
 CONSTRAINT [PK_VendorPays] PRIMARY KEY CLUSTERED 
(
	[PayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendors](
	[VendID] [int] IDENTITY(1,1) NOT NULL,
	[VendName] [nvarchar](150) NULL,
	[CityID] [int] NULL,
	[VendAddress] [nvarchar](200) NULL,
	[Contacts] [nvarchar](200) NULL,
 CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED 
(
	[VendID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorSales]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorSales](
	[SalesID] [int] IDENTITY(1,1) NOT NULL,
	[VendID] [int] NOT NULL,
	[Detail] [nvarchar](200) NOT NULL,
	[SalesDate] [smalldatetime] NOT NULL,
	[SalesValue] [money] NOT NULL,
 CONSTRAINT [PK_VendorSales] PRIMARY KEY CLUSTERED 
(
	[SalesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VisitTypes]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitTypes](
	[VtID] [int] IDENTITY(1,1) NOT NULL,
	[VisitType] [nvarchar](150) NULL,
	[VisitTypeAr] [nvarchar](150) NULL,
 CONSTRAINT [PK_VisitTypes] PRIMARY KEY CLUSTERED 
(
	[VtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Batchess_Expiration]    Script Date: 10/28/2025 9:13:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_Batchess_Expiration] ON [dbo].[Batchess]
(
	[ExpirationDate] ASC
)
INCLUDE([CurrentQuantity]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Batchess_ExpirationDate]    Script Date: 10/28/2025 9:13:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_Batchess_ExpirationDate] ON [dbo].[Batchess]
(
	[ExpirationDate] ASC
)
INCLUDE([CurrentQuantity]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PatientHistory_ActionTime]    Script Date: 10/28/2025 9:13:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientHistory_ActionTime] ON [dbo].[PatientHistory]
(
	[ActionTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_Category]    Script Date: 10/28/2025 9:13:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_Products_Category] ON [dbo].[Products]
(
	[CategoryID] ASC
)
INCLUDE([ProductName],[ReorderLevel]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StockMovements_MovementDate]    Script Date: 10/28/2025 9:13:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_StockMovements_MovementDate] ON [dbo].[StockMovements]
(
	[MovementDate] ASC
)
INCLUDE([QuantityChange]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppointmentC] ADD  CONSTRAINT [DF_AppointmentC_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[AuditLog] ADD  CONSTRAINT [DF_AuditLog_ChangeDate]  DEFAULT (getdate()) FOR [ChangeDate]
GO
ALTER TABLE [dbo].[BuyInvoices] ADD  CONSTRAINT [DF_BuyInvoices_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Contacts] ADD  CONSTRAINT [DF_Contacts_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[GroupPermissions] ADD  DEFAULT ((1)) FOR [IsAllowed]
GO
ALTER TABLE [dbo].[ImplantType] ADD  CONSTRAINT [DF_ImplantType_IsSlim]  DEFAULT ((0)) FOR [IsSlim]
GO
ALTER TABLE [dbo].[Loans] ADD  CONSTRAINT [DF_Loans_LoanDate]  DEFAULT (CONVERT([date],getdate())) FOR [LoanDate]
GO
ALTER TABLE [dbo].[Loans] ADD  CONSTRAINT [DF_Loans_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [DF_Patient_StillKid]  DEFAULT ((0)) FOR [StillKid]
GO
ALTER TABLE [dbo].[Patient_Mobile] ADD  CONSTRAINT [DF_Patient_Mobile_IsExternal]  DEFAULT ((0)) FOR [IsExternal]
GO
ALTER TABLE [dbo].[Patient_Mobile] ADD  CONSTRAINT [DF_Patient_Mobile_IsMultiTrt]  DEFAULT ((0)) FOR [IsMultiTrt]
GO
ALTER TABLE [dbo].[Patient_MobInfo] ADD  CONSTRAINT [DF_Patient_MobInfo_IsExternal]  DEFAULT ((0)) FOR [IsExternal]
GO
ALTER TABLE [dbo].[Patient_ToothTrt] ADD  CONSTRAINT [DF_Patient_ToothTrt_IsExternal]  DEFAULT ((0)) FOR [IsExternal]
GO
ALTER TABLE [dbo].[Patient_ToothTrt] ADD  CONSTRAINT [DF_Patient_ToothTrt_IsMultiTrt]  DEFAULT ((0)) FOR [IsMultiTrt]
GO
ALTER TABLE [dbo].[Patient_ToothTrt] ADD  CONSTRAINT [DF_Patient_ToothTrt_isOnImplant]  DEFAULT ((0)) FOR [isOnImplant]
GO
ALTER TABLE [dbo].[Patient_TrtInfo] ADD  CONSTRAINT [DF_Patient_TrtInfo_IsExternal]  DEFAULT ((0)) FOR [IsExternal]
GO
ALTER TABLE [dbo].[Patient_Trts] ADD  CONSTRAINT [DF_Patient_Trts_IsMultiTooth]  DEFAULT ((0)) FOR [IsMultiTooth]
GO
ALTER TABLE [dbo].[Patient_Trts] ADD  CONSTRAINT [DF_Patient_Trts_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[Patient_Trts] ADD  CONSTRAINT [DF_Patient_Trts_DiscountType]  DEFAULT ((0)) FOR [DiscountType]
GO
ALTER TABLE [dbo].[PatientHistory] ADD  CONSTRAINT [DF_PatientHistory_ActionTime]  DEFAULT (getdate()) FOR [ActionTime]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_ReorderLevel]  DEFAULT ((0)) FOR [ReorderLevel]
GO
ALTER TABLE [dbo].[Repayments] ADD  CONSTRAINT [DF_Repayments_RepaymentDate]  DEFAULT (CONVERT([date],getdate())) FOR [RepaymentDate]
GO
ALTER TABLE [dbo].[Repayments] ADD  CONSTRAINT [DF_Repayments_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[StockMovements] ADD  CONSTRAINT [DF_StockMovements_MovementDate]  DEFAULT (getdate()) FOR [MovementDate]
GO
ALTER TABLE [dbo].[TblMobTRT] ADD  CONSTRAINT [DF_TblMobTRT_TrtArDetails]  DEFAULT ((0)) FOR [TrtArDetails]
GO
ALTER TABLE [dbo].[TblMobTRT] ADD  CONSTRAINT [DF_TblMobTRT_KidTrt]  DEFAULT ((0)) FOR [KidTrt]
GO
ALTER TABLE [dbo].[TblTRTS] ADD  CONSTRAINT [DF_TblTRTS_TrtValue]  DEFAULT ((0)) FOR [TrtValue]
GO
ALTER TABLE [dbo].[TblTRTS] ADD  CONSTRAINT [DF_TblTRTS_TrtLoc]  DEFAULT ((0)) FOR [TrtLoc]
GO
ALTER TABLE [dbo].[TblTRTS] ADD  CONSTRAINT [DF_TblTRTS_KidTrt]  DEFAULT ((0)) FOR [KidTrt]
GO
ALTER TABLE [dbo].[AppointmentC]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentC_Doctors] FOREIGN KEY([DrID])
REFERENCES [dbo].[Doctors] ([DrID])
GO
ALTER TABLE [dbo].[AppointmentC] CHECK CONSTRAINT [FK_AppointmentC_Doctors]
GO
ALTER TABLE [dbo].[AppointmentC]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentC_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
GO
ALTER TABLE [dbo].[AppointmentC] CHECK CONSTRAINT [FK_AppointmentC_Patient]
GO
ALTER TABLE [dbo].[Barcodes]  WITH CHECK ADD  CONSTRAINT [FK_Barcodes_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Barcodes] CHECK CONSTRAINT [FK_Barcodes_Products]
GO
ALTER TABLE [dbo].[Batchess]  WITH CHECK ADD  CONSTRAINT [FK_Batchess_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Batchess] CHECK CONSTRAINT [FK_Batchess_Products]
GO
ALTER TABLE [dbo].[Batchess]  WITH CHECK ADD  CONSTRAINT [FK_Batchess_Suppliers] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[Batchess] CHECK CONSTRAINT [FK_Batchess_Suppliers]
GO
ALTER TABLE [dbo].[BuyInvoiceLineItems]  WITH CHECK ADD  CONSTRAINT [FK_BuyInvoiceLineItems_BuyInvoices] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[BuyInvoices] ([InvoiceID])
GO
ALTER TABLE [dbo].[BuyInvoiceLineItems] CHECK CONSTRAINT [FK_BuyInvoiceLineItems_BuyInvoices]
GO
ALTER TABLE [dbo].[BuyInvoiceLineItems]  WITH CHECK ADD  CONSTRAINT [FK_BuyInvoiceLineItems_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[BuyInvoiceLineItems] CHECK CONSTRAINT [FK_BuyInvoiceLineItems_Products]
GO
ALTER TABLE [dbo].[BuyInvoices]  WITH CHECK ADD  CONSTRAINT [FK_BuyInvoices_Suppliers] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[BuyInvoices] CHECK CONSTRAINT [FK_BuyInvoices_Suppliers]
GO
ALTER TABLE [dbo].[GroupPermissions]  WITH CHECK ADD  CONSTRAINT [FK_GP_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([GroupID])
GO
ALTER TABLE [dbo].[GroupPermissions] CHECK CONSTRAINT [FK_GP_Groups]
GO
ALTER TABLE [dbo].[GroupPermissions]  WITH CHECK ADD  CONSTRAINT [FK_GP_Permissions] FOREIGN KEY([PermID])
REFERENCES [dbo].[Permissions] ([PermID])
GO
ALTER TABLE [dbo].[GroupPermissions] CHECK CONSTRAINT [FK_GP_Permissions]
GO
ALTER TABLE [dbo].[ImprDet]  WITH CHECK ADD  CONSTRAINT [FK_ImprDet_Impression] FOREIGN KEY([imprID])
REFERENCES [dbo].[Impression] ([ImprID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImprDet] CHECK CONSTRAINT [FK_ImprDet_Impression]
GO
ALTER TABLE [dbo].[LabOrder]  WITH CHECK ADD  CONSTRAINT [FK_LabOrder_Lab] FOREIGN KEY([LabID])
REFERENCES [dbo].[Lab] ([LabID])
GO
ALTER TABLE [dbo].[LabOrder] CHECK CONSTRAINT [FK_LabOrder_Lab]
GO
ALTER TABLE [dbo].[LabOrder]  WITH CHECK ADD  CONSTRAINT [FK_LabOrder_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
GO
ALTER TABLE [dbo].[LabOrder] CHECK CONSTRAINT [FK_LabOrder_Patient]
GO
ALTER TABLE [dbo].[LabPay]  WITH CHECK ADD  CONSTRAINT [FK_LabPay_Lab] FOREIGN KEY([LabID])
REFERENCES [dbo].[Lab] ([LabID])
GO
ALTER TABLE [dbo].[LabPay] CHECK CONSTRAINT [FK_LabPay_Lab]
GO
ALTER TABLE [dbo].[LD]  WITH NOCHECK ADD  CONSTRAINT [FK_LD_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LD] CHECK CONSTRAINT [FK_LD_Patient]
GO
ALTER TABLE [dbo].[LDSTYLE]  WITH NOCHECK ADD  CONSTRAINT [FK_LDSTYLE_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LDSTYLE] CHECK CONSTRAINT [FK_LDSTYLE_patient]
GO
ALTER TABLE [dbo].[Loans]  WITH CHECK ADD  CONSTRAINT [FK_Loans_Contacts] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Contacts] ([ContactID])
GO
ALTER TABLE [dbo].[Loans] CHECK CONSTRAINT [FK_Loans_Contacts]
GO
ALTER TABLE [dbo].[LU]  WITH NOCHECK ADD  CONSTRAINT [FK_LU_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LU] CHECK CONSTRAINT [FK_LU_Patient]
GO
ALTER TABLE [dbo].[LUSTYLE]  WITH NOCHECK ADD  CONSTRAINT [FK_LUSTYLE_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LUSTYLE] CHECK CONSTRAINT [FK_LUSTYLE_patient]
GO
ALTER TABLE [dbo].[MedicineDoze]  WITH NOCHECK ADD  CONSTRAINT [FK_MedicineDoze_MedicineShape] FOREIGN KEY([ShapeID])
REFERENCES [dbo].[MedicineShape] ([ShapeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MedicineDoze] CHECK CONSTRAINT [FK_MedicineDoze_MedicineShape]
GO
ALTER TABLE [dbo].[MedicineFamily]  WITH CHECK ADD  CONSTRAINT [FK_MedicineFamily_MedicineGroups] FOREIGN KEY([MedicineID])
REFERENCES [dbo].[MedicineGroups] ([MedicineID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MedicineFamily] CHECK CONSTRAINT [FK_MedicineFamily_MedicineGroups]
GO
ALTER TABLE [dbo].[MedicineItems]  WITH CHECK ADD  CONSTRAINT [FK_MedicineItems_MedScienceFamily] FOREIGN KEY([ScincID])
REFERENCES [dbo].[MedScienceFamily] ([ScincID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MedicineItems] CHECK CONSTRAINT [FK_MedicineItems_MedScienceFamily]
GO
ALTER TABLE [dbo].[MedicineShape]  WITH NOCHECK ADD  CONSTRAINT [FK_MedicineShape_MedicineItems] FOREIGN KEY([MedicineItemID])
REFERENCES [dbo].[MedicineItems] ([MedicineItemID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MedicineShape] CHECK CONSTRAINT [FK_MedicineShape_MedicineItems]
GO
ALTER TABLE [dbo].[MedScienceFamily]  WITH CHECK ADD  CONSTRAINT [FK_MedScienceFamily_MedicineFamily] FOREIGN KEY([SubCatID])
REFERENCES [dbo].[MedicineFamily] ([SubCatID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MedScienceFamily] CHECK CONSTRAINT [FK_MedScienceFamily_MedicineFamily]
GO
ALTER TABLE [dbo].[MobileStructure]  WITH CHECK ADD  CONSTRAINT [FK_MobileStructure_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
GO
ALTER TABLE [dbo].[MobileStructure] CHECK CONSTRAINT [FK_MobileStructure_Patient]
GO
ALTER TABLE [dbo].[OrthoDiag]  WITH NOCHECK ADD  CONSTRAINT [FK_OrthoDiag_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrthoDiag] CHECK CONSTRAINT [FK_OrthoDiag_patient]
GO
ALTER TABLE [dbo].[OrthoInf]  WITH NOCHECK ADD  CONSTRAINT [FK_OrthoInf_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrthoInf] CHECK CONSTRAINT [FK_OrthoInf_patient]
GO
ALTER TABLE [dbo].[OrthoTreat]  WITH NOCHECK ADD  CONSTRAINT [FK_OrthoTreat_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrthoTreat] CHECK CONSTRAINT [FK_OrthoTreat_patient]
GO
ALTER TABLE [dbo].[OrthoTrtDet]  WITH NOCHECK ADD  CONSTRAINT [FK_OrthoTrtDet_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrthoTrtDet] CHECK CONSTRAINT [FK_OrthoTrtDet_patient]
GO
ALTER TABLE [dbo].[Patient_ExternalToothTrt]  WITH CHECK ADD  CONSTRAINT [FK_Patient_ExternalToothTrt_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient_ExternalToothTrt] CHECK CONSTRAINT [FK_Patient_ExternalToothTrt_Patient]
GO
ALTER TABLE [dbo].[Patient_Imgs]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_Imgs_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient_Imgs] CHECK CONSTRAINT [FK_Patient_Imgs_Patient]
GO
ALTER TABLE [dbo].[Patient_Mobile]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Mobile_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient_Mobile] CHECK CONSTRAINT [FK_Patient_Mobile_Patient]
GO
ALTER TABLE [dbo].[Patient_MobStruc]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_MobStruc_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient_MobStruc] CHECK CONSTRAINT [FK_Patient_MobStruc_patient]
GO
ALTER TABLE [dbo].[Patient_MobStrucAdd]  WITH CHECK ADD  CONSTRAINT [FK_Patient_MobStrucAdd_Patient_MobStruc] FOREIGN KEY([StrucID])
REFERENCES [dbo].[Patient_MobStruc] ([StrucID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient_MobStrucAdd] CHECK CONSTRAINT [FK_Patient_MobStrucAdd_Patient_MobStruc]
GO
ALTER TABLE [dbo].[Patient_Notes]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_Notes_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient_Notes] CHECK CONSTRAINT [FK_Patient_Notes_Patient]
GO
ALTER TABLE [dbo].[Patient_Pays]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_Pays_Patient_Trts] FOREIGN KEY([TrtID])
REFERENCES [dbo].[Patient_Trts] ([TrtID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient_Pays] CHECK CONSTRAINT [FK_Patient_Pays_Patient_Trts]
GO
ALTER TABLE [dbo].[Patient_RX]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_RX_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient_RX] CHECK CONSTRAINT [FK_Patient_RX_Patient]
GO
ALTER TABLE [dbo].[Patient_ToothChart]  WITH CHECK ADD  CONSTRAINT [FK_Patient_ToothChart_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Patient_ToothChart] CHECK CONSTRAINT [FK_Patient_ToothChart_Patient]
GO
ALTER TABLE [dbo].[Patient_ToothCheck]  WITH CHECK ADD  CONSTRAINT [FK_Patient_ToothCheck_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Patient_ToothCheck] CHECK CONSTRAINT [FK_Patient_ToothCheck_Patient]
GO
ALTER TABLE [dbo].[Patient_ToothTrt]  WITH CHECK ADD  CONSTRAINT [FK_Patient_ToothTrt_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient_ToothTrt] CHECK CONSTRAINT [FK_Patient_ToothTrt_Patient]
GO
ALTER TABLE [dbo].[Patient_Trts]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_Trts_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient_Trts] CHECK CONSTRAINT [FK_Patient_Trts_Patient]
GO
ALTER TABLE [dbo].[Patient_TrtScope]  WITH CHECK ADD  CONSTRAINT [FK_Patient_TrtScope_Patient_Trts] FOREIGN KEY([TrtID])
REFERENCES [dbo].[Patient_Trts] ([TrtID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patient_TrtScope] CHECK CONSTRAINT [FK_Patient_TrtScope_Patient_Trts]
GO
ALTER TABLE [dbo].[PatientColors]  WITH CHECK ADD  CONSTRAINT [FK_PatientColors_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PatientColors] CHECK CONSTRAINT [FK_PatientColors_Patient]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_BuyInvoices] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[BuyInvoices] ([InvoiceID])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_BuyInvoices]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Units] FOREIGN KEY([UnitID])
REFERENCES [dbo].[Units] ([UnitID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Units]
GO
ALTER TABLE [dbo].[RD]  WITH NOCHECK ADD  CONSTRAINT [FK_RD_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RD] CHECK CONSTRAINT [FK_RD_Patient]
GO
ALTER TABLE [dbo].[RDSTYLE]  WITH NOCHECK ADD  CONSTRAINT [FK_RDSTYLE_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RDSTYLE] CHECK CONSTRAINT [FK_RDSTYLE_patient]
GO
ALTER TABLE [dbo].[Repayments]  WITH CHECK ADD  CONSTRAINT [FK_Repayments_Loans] FOREIGN KEY([LoanID])
REFERENCES [dbo].[Loans] ([LoanID])
GO
ALTER TABLE [dbo].[Repayments] CHECK CONSTRAINT [FK_Repayments_Loans]
GO
ALTER TABLE [dbo].[RU]  WITH NOCHECK ADD  CONSTRAINT [FK_RU_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RU] CHECK CONSTRAINT [FK_RU_Patient]
GO
ALTER TABLE [dbo].[RUSTYLE]  WITH NOCHECK ADD  CONSTRAINT [FK_RUSTYLE_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RUSTYLE] CHECK CONSTRAINT [FK_RUSTYLE_Patient]
GO
ALTER TABLE [dbo].[StockMovements]  WITH CHECK ADD  CONSTRAINT [FK_StockMovements_Batchess] FOREIGN KEY([BatchID])
REFERENCES [dbo].[Batchess] ([BatchID])
GO
ALTER TABLE [dbo].[StockMovements] CHECK CONSTRAINT [FK_StockMovements_Batchess]
GO
ALTER TABLE [dbo].[StockMovements]  WITH CHECK ADD  CONSTRAINT [FK_StockMovements_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[StockMovements] CHECK CONSTRAINT [FK_StockMovements_Products]
GO
ALTER TABLE [dbo].[Surgery]  WITH NOCHECK ADD  CONSTRAINT [FK_Surgery_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Surgery] CHECK CONSTRAINT [FK_Surgery_Patient]
GO
ALTER TABLE [dbo].[TblCustomers]  WITH CHECK ADD  CONSTRAINT [FK_tblcustomers_tblcities] FOREIGN KEY([CityID])
REFERENCES [dbo].[TblCities] ([CityID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TblCustomers] CHECK CONSTRAINT [FK_tblcustomers_tblcities]
GO
ALTER TABLE [dbo].[TblInvoiceBody]  WITH CHECK ADD  CONSTRAINT [FK_TblInvoiceBody_TblInvoicesHeader] FOREIGN KEY([InvID])
REFERENCES [dbo].[TblInvoicesHeader] ([InvoiceID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TblInvoiceBody] CHECK CONSTRAINT [FK_TblInvoiceBody_TblInvoicesHeader]
GO
ALTER TABLE [dbo].[TblInvoiceBody]  WITH CHECK ADD  CONSTRAINT [FK_TblInvoiceBody_TblItems] FOREIGN KEY([ItemID])
REFERENCES [dbo].[TblItems] ([ItemID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TblInvoiceBody] CHECK CONSTRAINT [FK_TblInvoiceBody_TblItems]
GO
ALTER TABLE [dbo].[TblInvoicesHeader]  WITH CHECK ADD  CONSTRAINT [FK_TblInvoicesHeader_TblResources] FOREIGN KEY([ResID])
REFERENCES [dbo].[TblResources] ([ResID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TblInvoicesHeader] CHECK CONSTRAINT [FK_TblInvoicesHeader_TblResources]
GO
ALTER TABLE [dbo].[TblInvPay]  WITH CHECK ADD  CONSTRAINT [FK_TblInvPay_TblInvoicesHeader] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[TblInvoicesHeader] ([InvoiceID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TblInvPay] CHECK CONSTRAINT [FK_TblInvPay_TblInvoicesHeader]
GO
ALTER TABLE [dbo].[TblInvPay]  WITH CHECK ADD  CONSTRAINT [FK_TblInvPay_TblResources] FOREIGN KEY([ResID])
REFERENCES [dbo].[TblResources] ([ResID])
GO
ALTER TABLE [dbo].[TblInvPay] CHECK CONSTRAINT [FK_TblInvPay_TblResources]
GO
ALTER TABLE [dbo].[TblItems]  WITH CHECK ADD  CONSTRAINT [FK_TblItems_TblCategories] FOREIGN KEY([CatID])
REFERENCES [dbo].[TblCategories] ([CategoryID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TblItems] CHECK CONSTRAINT [FK_TblItems_TblCategories]
GO
ALTER TABLE [dbo].[TblItems]  WITH CHECK ADD  CONSTRAINT [FK_TblItems_TblUnits] FOREIGN KEY([UnitID])
REFERENCES [dbo].[TblUnits] ([UnitID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TblItems] CHECK CONSTRAINT [FK_TblItems_TblUnits]
GO
ALTER TABLE [dbo].[TblMasareef]  WITH CHECK ADD  CONSTRAINT [FK_TblMasareef_TblBnodMsareef] FOREIGN KEY([BandID])
REFERENCES [dbo].[TblBnodMsareef] ([BandID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TblMasareef] CHECK CONSTRAINT [FK_TblMasareef_TblBnodMsareef]
GO
ALTER TABLE [dbo].[TblResources]  WITH CHECK ADD  CONSTRAINT [FK_tblresources_tblcities] FOREIGN KEY([CityID])
REFERENCES [dbo].[TblCities] ([CityID])
GO
ALTER TABLE [dbo].[TblResources] CHECK CONSTRAINT [FK_tblresources_tblcities]
GO
ALTER TABLE [dbo].[TblSalesBody]  WITH CHECK ADD  CONSTRAINT [FK_TblSalesBody_TblItems] FOREIGN KEY([ItemID])
REFERENCES [dbo].[TblItems] ([ItemID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TblSalesBody] CHECK CONSTRAINT [FK_TblSalesBody_TblItems]
GO
ALTER TABLE [dbo].[TblSalesBody]  WITH CHECK ADD  CONSTRAINT [FK_TblSalesBody_TblSalesHeader] FOREIGN KEY([SaleID])
REFERENCES [dbo].[TblSalesHeader] ([SaleID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TblSalesBody] CHECK CONSTRAINT [FK_TblSalesBody_TblSalesHeader]
GO
ALTER TABLE [dbo].[TblSalesHeader]  WITH CHECK ADD  CONSTRAINT [FK_TblSalesHeader_TblCustomers] FOREIGN KEY([CusID])
REFERENCES [dbo].[TblCustomers] ([CusID])
GO
ALTER TABLE [dbo].[TblSalesHeader] CHECK CONSTRAINT [FK_TblSalesHeader_TblCustomers]
GO
ALTER TABLE [dbo].[UserPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UP_Permissions] FOREIGN KEY([PermID])
REFERENCES [dbo].[Permissions] ([PermID])
GO
ALTER TABLE [dbo].[UserPermissions] CHECK CONSTRAINT [FK_UP_Permissions]
GO
ALTER TABLE [dbo].[UserPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UP_Users] FOREIGN KEY([UsID])
REFERENCES [dbo].[USERS] ([UsID])
GO
ALTER TABLE [dbo].[UserPermissions] CHECK CONSTRAINT [FK_UP_Users]
GO
ALTER TABLE [dbo].[USERS]  WITH CHECK ADD  CONSTRAINT [FK_Users_Doctors] FOREIGN KEY([DrID])
REFERENCES [dbo].[Doctors] ([DrID])
GO
ALTER TABLE [dbo].[USERS] CHECK CONSTRAINT [FK_Users_Doctors]
GO
ALTER TABLE [dbo].[USERS]  WITH CHECK ADD  CONSTRAINT [FK_Users_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([GroupID])
GO
ALTER TABLE [dbo].[USERS] CHECK CONSTRAINT [FK_Users_Groups]
GO
ALTER TABLE [dbo].[USERS]  WITH CHECK ADD  CONSTRAINT [FK_Users_Secretaries] FOREIGN KEY([SecID])
REFERENCES [dbo].[Secretaries] ([SecID])
GO
ALTER TABLE [dbo].[USERS] CHECK CONSTRAINT [FK_Users_Secretaries]
GO
ALTER TABLE [dbo].[VendorPays]  WITH CHECK ADD  CONSTRAINT [FK_VendorPays_Vendors] FOREIGN KEY([VendID])
REFERENCES [dbo].[Vendors] ([VendID])
GO
ALTER TABLE [dbo].[VendorPays] CHECK CONSTRAINT [FK_VendorPays_Vendors]
GO
ALTER TABLE [dbo].[VendorPays]  WITH NOCHECK ADD  CONSTRAINT [FK_VendorPays_VendorSales] FOREIGN KEY([SalesID])
REFERENCES [dbo].[VendorSales] ([SalesID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VendorPays] CHECK CONSTRAINT [FK_VendorPays_VendorSales]
GO
ALTER TABLE [dbo].[Vendors]  WITH CHECK ADD  CONSTRAINT [FK_Vendors_TblCities] FOREIGN KEY([CityID])
REFERENCES [dbo].[TblCities] ([CityID])
GO
ALTER TABLE [dbo].[Vendors] CHECK CONSTRAINT [FK_Vendors_TblCities]
GO
ALTER TABLE [dbo].[VendorSales]  WITH NOCHECK ADD  CONSTRAINT [FK_VendorSales_Vendors] FOREIGN KEY([VendID])
REFERENCES [dbo].[Vendors] ([VendID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VendorSales] CHECK CONSTRAINT [FK_VendorSales_Vendors]
GO
ALTER TABLE [dbo].[Visits]  WITH NOCHECK ADD  CONSTRAINT [FK_Visits_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Visits] CHECK CONSTRAINT [FK_Visits_Patient]
GO
ALTER TABLE [dbo].[Barcodes]  WITH CHECK ADD  CONSTRAINT [CK_Barcodes_type] CHECK  (([BarcodeType]='Custom' OR [BarcodeType]='QR' OR [BarcodeType]='EAN' OR [BarcodeType]='UPC'))
GO
ALTER TABLE [dbo].[Barcodes] CHECK CONSTRAINT [CK_Barcodes_type]
GO
ALTER TABLE [dbo].[BuyInvoices]  WITH CHECK ADD  CONSTRAINT [CK_BuyInvoices_status] CHECK  (([InvoiceStatus]='Partial' OR [InvoiceStatus]='Unpaid' OR [InvoiceStatus]='Paid'))
GO
ALTER TABLE [dbo].[BuyInvoices] CHECK CONSTRAINT [CK_BuyInvoices_status]
GO
ALTER TABLE [dbo].[ImplantMeasure]  WITH CHECK ADD  CONSTRAINT [CK_ImplantMeasure_DiameterMM] CHECK  (([DiameterMM]>=(2.8) AND [DiameterMM]<=(6.0)))
GO
ALTER TABLE [dbo].[ImplantMeasure] CHECK CONSTRAINT [CK_ImplantMeasure_DiameterMM]
GO
ALTER TABLE [dbo].[ImplantMeasure]  WITH CHECK ADD  CONSTRAINT [CK_ImplantMeasure_length] CHECK  (([LengthMM]>=(5.0) AND [LengthMM]<=(20.0)))
GO
ALTER TABLE [dbo].[ImplantMeasure] CHECK CONSTRAINT [CK_ImplantMeasure_length]
GO
ALTER TABLE [dbo].[ImplantType]  WITH CHECK ADD  CONSTRAINT [CK_ImplantType_type] CHECK  (([TypeName]='COMP' OR [TypeName]='BSI'))
GO
ALTER TABLE [dbo].[ImplantType] CHECK CONSTRAINT [CK_ImplantType_type]
GO
ALTER TABLE [dbo].[Loans]  WITH CHECK ADD  CONSTRAINT [CK_Loans_ti] CHECK  (([Direction]='Borrow' OR [Direction]='Lend'))
GO
ALTER TABLE [dbo].[Loans] CHECK CONSTRAINT [CK_Loans_ti]
GO
ALTER TABLE [dbo].[MobileStructure]  WITH CHECK ADD  CONSTRAINT [CK_MobileStructure_Arch] CHECK  (([Arch]='L' OR [Arch]='U'))
GO
ALTER TABLE [dbo].[MobileStructure] CHECK CONSTRAINT [CK_MobileStructure_Arch]
GO
ALTER TABLE [dbo].[MobileStructure]  WITH CHECK ADD  CONSTRAINT [CK_MobileStructure_ToothNum] CHECK  (([ToothNum]>=(11) AND [ToothNum]<=(48)))
GO
ALTER TABLE [dbo].[MobileStructure] CHECK CONSTRAINT [CK_MobileStructure_ToothNum]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [CK_Payments_PaymentMethod] CHECK  (([PaymentMethod]='BankTransfer' OR [PaymentMethod]='CreditCard' OR [PaymentMethod]='Cash'))
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [CK_Payments_PaymentMethod]
GO
ALTER TABLE [dbo].[Repayments]  WITH CHECK ADD  CONSTRAINT [CK_Repayments_Amount] CHECK  (([Amount]>(0)))
GO
ALTER TABLE [dbo].[Repayments] CHECK CONSTRAINT [CK_Repayments_Amount]
GO
ALTER TABLE [dbo].[StockMovements]  WITH CHECK ADD  CONSTRAINT [CK_StockMovements_type] CHECK  (([MovementType]='Return' OR [MovementType]='Adjustment' OR [MovementType]='Usage' OR [MovementType]='Purchase'))
GO
ALTER TABLE [dbo].[StockMovements] CHECK CONSTRAINT [CK_StockMovements_type]
GO
ALTER TABLE [dbo].[TblMobTRT]  WITH CHECK ADD  CONSTRAINT [CK_TblMobTRT_id] CHECK  (([KidTrt]=(2) OR [KidTrt]=(1) OR [KidTrt]=(0)))
GO
ALTER TABLE [dbo].[TblMobTRT] CHECK CONSTRAINT [CK_TblMobTRT_id]
GO
ALTER TABLE [dbo].[TblTRTS]  WITH CHECK ADD  CONSTRAINT [CK_TblTRTS_id] CHECK  (([KidTrt]=(2) OR [KidTrt]=(1) OR [KidTrt]=(0)))
GO
ALTER TABLE [dbo].[TblTRTS] CHECK CONSTRAINT [CK_TblTRTS_id]
GO
ALTER TABLE [dbo].[USERS]  WITH CHECK ADD  CONSTRAINT [CK_USERS_cid] CHECK  (([GroupID]=(2) AND [DrID] IS NOT NULL AND [SecID] IS NULL OR [GroupID]=(3) AND [SecID] IS NOT NULL AND [DrID] IS NULL OR [GroupID]=(1) AND [DrID] IS NULL AND [SecID] IS NULL))
GO
ALTER TABLE [dbo].[USERS] CHECK CONSTRAINT [CK_USERS_cid]
GO
/****** Object:  StoredProcedure [dbo].[Delete_LD]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_LD] (
@LDID int,
@PatientID int
) AS BEGIN
DELETE FROM LD
WHERE LDID = @LDID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[delete_RxFly]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[delete_RxFly] 
   
AS 
	
	 
	
	BEGIN TRAN
	
	
declare  @RxID_1 	[int]
set @RxID_1=(select max(RxID) from .[dbo].[RxFly] )
 DELETE .[dbo].[RxFly] 

WHERE 
	( [RxID]	 = @RxID_1 )
	 
	COMMIT





GO
/****** Object:  StoredProcedure [dbo].[DrWorkDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DrWorkDelete] 
    @WorkID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[DrWork]
	WHERE  [WorkID] = @WorkID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[DrWorkInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DrWorkInsert] 
    @DrID int,
    @PatientID int = NULL,
    @WrkDate datetime = NULL,
    @WrkDetail nvarchar(50) = NULL,
    @WrkVal money = NULL,
    @PayVal money = NULL,
    @Imp bit = NULL,
    @Orth bit = NULL,
    @Surg bit = NULL,
    @Notes nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[DrWork] ([DrID], [PatientID], [WrkDate], [WrkDetail], [WrkVal], [PayVal], [Imp], [Orth], [Surg], [Notes])
	SELECT @DrID, @PatientID, @WrkDate, @WrkDetail, @WrkVal, @PayVal, @Imp, @Orth, @Surg, @Notes
		               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[DrWorkSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DrWorkSelect] 
    @WorkID int
AS 
	
	 

	BEGIN TRAN

	SELECT [WorkID], [DrID], [PatientID], [WrkDate], [WrkDetail], [WrkVal], [PayVal], [Imp], [Orth], [Surg], [Notes] 
	FROM   [dbo].[DrWork] 
	WHERE  ([WorkID] = @WorkID OR @WorkID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[DrWorkUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DrWorkUpdate] 
    @WorkID int,
    @DrID int,
    @PatientID int = NULL,
    @WrkDate datetime = NULL,
    @WrkDetail nvarchar(50) = NULL,
    @WrkVal money = NULL,
    @PayVal money = NULL,
    @Imp bit = NULL,
    @Orth bit = NULL,
    @Surg bit = NULL,
    @Notes nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[DrWork]
	SET    [DrID] = @DrID, [PatientID] = @PatientID, [WrkDate] = @WrkDate, [WrkDetail] = @WrkDetail, [WrkVal] = @WrkVal, [PayVal] = @PayVal, [Imp] = @Imp, [Orth] = @Orth, [Surg] = @Surg, [Notes] = @Notes
	WHERE  [WorkID] = @WorkID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[EmpDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[EmpDelete] 
    @EmpID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Emp]
	WHERE  [EmpID] = @EmpID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[EmpInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[EmpInsert] 
    @EmpName nvarchar(50) = NULL,
    @EmpPhone nchar(10) = NULL,
    @EmpAddress nvarchar(50) = NULL,
    @EmpImg image = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Emp] ([EmpName], [EmpPhone], [EmpAddress], [EmpImg])
	SELECT @EmpName, @EmpPhone, @EmpAddress, @EmpImg
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[EmpSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[EmpSelect] 
    @EmpID int
AS 
	
	 

	BEGIN TRAN

	SELECT [EmpID], [EmpName], [EmpPhone], [EmpAddress], [EmpImg] 
	FROM   [dbo].[Emp] 
	WHERE  ([EmpID] = @EmpID OR @EmpID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[EmpUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[EmpUpdate] 
    @EmpID int,
    @EmpName nvarchar(50) = NULL,
    @EmpPhone nchar(10) = NULL,
    @EmpAddress nvarchar(50) = NULL,
    @EmpImg image = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Emp]
	SET    [EmpName] = @EmpName, [EmpPhone] = @EmpPhone, [EmpAddress] = @EmpAddress, [EmpImg] = @EmpImg
	WHERE  [EmpID] = @EmpID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[GenderDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GenderDelete] 
    @SID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Gender]
	WHERE  [SID] = @SID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[GenderInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GenderInsert] 
    @Sex nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Gender] ([Sex])
	SELECT @Sex
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[GenderSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GenderSelect] 
    @SID int
AS 
	
	 

	BEGIN TRAN

	SELECT [SID], [Sex] 
	FROM   [dbo].[Gender] 
	WHERE  ([SID] = @SID OR @SID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[GenderUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GenderUpdate] 
    @SID int,
    @Sex nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Gender]
	SET    [Sex] = @Sex
	WHERE  [SID] = @SID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[GenerateDatabaseScript]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GenerateDatabaseScript]
    @IncludeData BIT = 0,
    @Tables NVARCHAR(MAX) = NULL  -- comma-separated list like 'Patients,Appointments'
AS
BEGIN
    SET NOCOUNT ON;

    -- Declare everything upfront to avoid scope issues
    DECLARE 
        @sql NVARCHAR(MAX) = '',
        @sqlData NVARCHAR(MAX) = '',
        @tbl NVARCHAR(255),
        @cols NVARCHAR(MAX),
        @crlf CHAR(2) = CHAR(13) + CHAR(10);

    DECLARE @list TABLE (tname NVARCHAR(255));

    -- Header
    SET @sql = @sql + '/****** AUTO-GENERATED DATABASE SCRIPT ******/'+@crlf;
    SET @sql = @sql + 'USE [' + DB_NAME() + '];' + @crlf + 'GO' + @crlf;

    -----------------------------------------------------------------------
    -- TABLES
    -----------------------------------------------------------------------
    SELECT @sql = @sql + 
        'CREATE TABLE [' + s.name + '].[' + t.name + '] (' + @crlf +
        STRING_AGG('    [' + c.name + '] ' + 
            UPPER(tp.name) + 
            CASE 
                WHEN tp.name IN ('varchar','nvarchar','char','nchar') 
                     THEN '(' + CASE WHEN c.max_length=-1 THEN 'MAX' ELSE CAST(c.max_length AS VARCHAR(10)) END + ')' 
                WHEN tp.name IN ('decimal','numeric') 
                     THEN '(' + CAST(c.precision AS VARCHAR(10)) + ',' + CAST(c.scale AS VARCHAR(10)) + ')' 
                ELSE '' END +
            CASE WHEN c.is_nullable=1 THEN ' NULL' ELSE ' NOT NULL' END +
            ISNULL(' DEFAULT ' + dc.definition, '') +
            ',' + @crlf, '') 
        + ');' + @crlf + 'GO' + @crlf
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    JOIN sys.columns c ON t.object_id = c.object_id
    JOIN sys.types tp ON c.user_type_id = tp.user_type_id
    LEFT JOIN sys.default_constraints dc ON c.default_object_id = dc.object_id
    WHERE t.is_ms_shipped = 0
    GROUP BY s.name, t.name
    ORDER BY s.name, t.name;

    -----------------------------------------------------------------------
    -- PRIMARY KEYS
    -----------------------------------------------------------------------
    SELECT @sql = @sql + 
        'ALTER TABLE [' + s.name + '].[' + t.name + '] ADD CONSTRAINT [' + i.name + '] PRIMARY KEY (' +
        STRING_AGG('[' + c.name + ']', ', ') + ');' + @crlf + 'GO' + @crlf
    FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    JOIN sys.indexes i ON t.object_id = i.object_id AND i.is_primary_key = 1
    JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
    GROUP BY s.name, t.name, i.name
    ORDER BY s.name, t.name;

    -----------------------------------------------------------------------
    -- FOREIGN KEYS
    -----------------------------------------------------------------------
    SELECT @sql = @sql + 
        'ALTER TABLE [' + s.name + '].[' + t.name + '] ADD CONSTRAINT [' + fk.name + '] FOREIGN KEY ([' + c.name + ']) ' +
        'REFERENCES [' + rs.name + '].[' + rt.name + ']([' + rc.name + ']);' + @crlf + 'GO' + @crlf
    FROM sys.foreign_keys fk
    JOIN sys.tables t ON fk.parent_object_id = t.object_id
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    JOIN sys.tables rt ON fk.referenced_object_id = rt.object_id
    JOIN sys.schemas rs ON rt.schema_id = rs.schema_id
    JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
    JOIN sys.columns c ON fkc.parent_column_id = c.column_id AND fkc.parent_object_id = c.object_id
    JOIN sys.columns rc ON fkc.referenced_column_id = rc.column_id AND fkc.referenced_object_id = rc.object_id
    ORDER BY s.name, t.name;

    -----------------------------------------------------------------------
    -- DATA (optional)
    -----------------------------------------------------------------------
    IF @IncludeData = 1
    BEGIN
        -- fill table list
        IF @Tables IS NULL 
            INSERT INTO @list SELECT name FROM sys.tables WHERE is_ms_shipped = 0;
        ELSE
            INSERT INTO @list SELECT TRIM(value) FROM STRING_SPLIT(@Tables, ',');

        DECLARE cur CURSOR FOR SELECT tname FROM @list;
        OPEN cur; 
        FETCH NEXT FROM cur INTO @tbl;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @sqlData = @sqlData + '-- Data for table [' + @tbl + ']' + @crlf;

            SELECT @cols = STRING_AGG(QUOTENAME(name), ', ') 
            FROM sys.columns 
            WHERE object_id = OBJECT_ID(@tbl);

            SET @sqlData = @sqlData + 
                'INSERT INTO [' + @tbl + '] (' + @cols + ')' + @crlf +
                'SELECT ' + @cols + ' FROM [' + @tbl + '];' + @crlf + 'GO' + @crlf;

            FETCH NEXT FROM cur INTO @tbl;
        END

        CLOSE cur; DEALLOCATE cur;
        SET @sql = @sql + @sqlData + @crlf;
    END

    -----------------------------------------------------------------------
    -- OUTPUT FINAL SCRIPT
    -----------------------------------------------------------------------
    SELECT @sql AS FullScript;
END
GO
/****** Object:  StoredProcedure [dbo].[GenerateFullDatabaseScript]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GenerateFullDatabaseScript]
    @DatabaseName SYSNAME,
    @OutputFile NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CRLF NVARCHAR(2) = CHAR(13) + CHAR(10);

    ------------------------------------------------------------
    -- 1️⃣ Database header
    ------------------------------------------------------------
    DECLARE @Header NVARCHAR(MAX) = 
        '-- =============================================' + @CRLF +
        '--  Database Script for: ' + QUOTENAME(@DatabaseName) + @CRLF +
        '--  Generated on: ' + CONVERT(VARCHAR(30), GETDATE(), 120) + @CRLF +
        '-- =============================================' + @CRLF + @CRLF;

    ------------------------------------------------------------
    -- 2️⃣ Schemas
    ------------------------------------------------------------
    DECLARE @Schemas NVARCHAR(MAX) = '';
    
    SELECT @Schemas = COALESCE(@Schemas + 'CREATE SCHEMA ' + QUOTENAME(s.name) + ';' + @CRLF, '')
    FROM sys.schemas s
    WHERE s.schema_id > 4 AND s.principal_id > 4;
    
    SET @Schemas = ISNULL(@Schemas, '') + @CRLF;

    ------------------------------------------------------------
    -- 3️⃣ Tables + columns
    ------------------------------------------------------------
    DECLARE @Tables NVARCHAR(MAX) = '';

    WITH TableColumns AS (
        SELECT 
            s.name as SchemaName,
            t.name as TableName,
            c.column_id,
            '    ' + QUOTENAME(c.name) + ' ' +
            UPPER(ty.name) +
            CASE
                WHEN ty.name IN ('varchar','nvarchar','char','nchar','varbinary') THEN 
                    '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length AS VARCHAR(10)) END + ')'
                WHEN ty.name IN ('decimal','numeric') THEN 
                    '(' + CAST(c.precision AS VARCHAR(10)) + ',' + CAST(c.scale AS VARCHAR(10)) + ')'
                WHEN ty.name IN ('float') THEN 
                    '(' + CAST(c.precision AS VARCHAR(10)) + ')'
                WHEN ty.name IN ('datetime2','datetimeoffset','time') THEN 
                    '(' + CAST(c.scale AS VARCHAR(10)) + ')'
                ELSE ''
            END +
            CASE WHEN c.is_identity = 1 THEN ' IDENTITY(' + CAST(ISNULL(ic.seed_value, 1) AS VARCHAR(10)) + ',' + CAST(ISNULL(ic.increment_value, 1) AS VARCHAR(10)) + ')' ELSE '' END +
            CASE WHEN c.is_nullable = 0 THEN ' NOT NULL' ELSE ' NULL' END +
            CASE WHEN dc.definition IS NOT NULL THEN ' DEFAULT ' + dc.definition ELSE '' END
            as ColumnDefinition
        FROM sys.tables t
        JOIN sys.schemas s ON s.schema_id = t.schema_id
        JOIN sys.columns c ON c.object_id = t.object_id
        JOIN sys.types ty ON ty.user_type_id = c.user_type_id
        LEFT JOIN sys.default_constraints dc ON dc.object_id = c.default_object_id AND dc.object_id != 0
        LEFT JOIN sys.identity_columns ic ON ic.object_id = t.object_id AND ic.column_id = c.column_id
        WHERE t.is_ms_shipped = 0
    ),
    AggregatedTables AS (
        SELECT
            SchemaName,
            TableName,
            (SELECT ColumnDefinition + ', ' + @CRLF
             FROM TableColumns tc2 
             WHERE tc2.SchemaName = tc1.SchemaName AND tc2.TableName = tc1.TableName
             ORDER BY tc2.column_id
             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') as ColumnsScript
        FROM (SELECT DISTINCT SchemaName, TableName FROM TableColumns) tc1
    )
    SELECT @Tables = COALESCE(@Tables + @CRLF + @CRLF, '') + 
           'CREATE TABLE ' + QUOTENAME(SchemaName) + '.' + QUOTENAME(TableName) + ' (' + @CRLF +
           LEFT(ColumnsScript, LEN(ColumnsScript) - 3) + -- Remove trailing comma and CRLF
           @CRLF + ');'
    FROM AggregatedTables
    ORDER BY SchemaName, TableName;

    SET @Tables = ISNULL(@Tables, '') + @CRLF + @CRLF;

    ------------------------------------------------------------
    -- 4️⃣ Primary keys
    ------------------------------------------------------------
    DECLARE @PKs NVARCHAR(MAX) = '';
    
    WITH PKColumns AS (
        SELECT 
            s.name as SchemaName,
            t.name as TableName,
            k.name as ConstraintName,
            i.type as IndexType,
            c.name as ColumnName,
            ic.key_ordinal
        FROM sys.key_constraints k
        JOIN sys.tables t ON t.object_id = k.parent_object_id
        JOIN sys.schemas s ON s.schema_id = t.schema_id
        JOIN sys.indexes i ON i.object_id = t.object_id AND i.index_id = k.unique_index_id
        JOIN sys.index_columns ic ON ic.object_id = t.object_id AND ic.index_id = i.index_id
        JOIN sys.columns c ON c.object_id = t.object_id AND c.column_id = ic.column_id
        WHERE k.type = 'PK' AND t.is_ms_shipped = 0
    ),
    AggregatedPKs AS (
        SELECT
            SchemaName,
            TableName,
            ConstraintName,
            IndexType,
            (SELECT QUOTENAME(ColumnName) + ', '
             FROM PKColumns pc2 
             WHERE pc2.SchemaName = pc1.SchemaName AND pc2.TableName = pc1.TableName AND pc2.ConstraintName = pc1.ConstraintName
             ORDER BY pc2.key_ordinal
             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') as ColumnsList
        FROM (SELECT DISTINCT SchemaName, TableName, ConstraintName, IndexType FROM PKColumns) pc1
    )
    SELECT @PKs = COALESCE(@PKs + @CRLF + @CRLF, '') +
           'ALTER TABLE ' + QUOTENAME(SchemaName) + '.' + QUOTENAME(TableName) + 
           ' ADD CONSTRAINT ' + QUOTENAME(ConstraintName) + ' PRIMARY KEY ' +
           CASE WHEN IndexType = 1 THEN 'CLUSTERED' ELSE 'NONCLUSTERED' END + ' (' +
           LEFT(ColumnsList, LEN(ColumnsList) - 1) + ');' -- Remove trailing comma
    FROM AggregatedPKs;

    SET @PKs = ISNULL(@PKs, '') + @CRLF + @CRLF;

    ------------------------------------------------------------
    -- 5️⃣ Foreign keys
    ------------------------------------------------------------
    DECLARE @FKs NVARCHAR(MAX) = '';
    
    WITH FKColumns AS (
        SELECT 
            fk.name as ConstraintName,
            SCHEMA_NAME(fk.schema_id) as ParentSchema,
            OBJECT_NAME(fk.parent_object_id) as ParentTable,
            pc.name as ParentColumn,
            SCHEMA_NAME(rt.schema_id) as ReferencedSchema,
            rt.name as ReferencedTable,
            rc.name as ReferencedColumn,
            pcc.constraint_column_id,
            fk.delete_referential_action,
            fk.update_referential_action
        FROM sys.foreign_keys fk
        JOIN sys.foreign_key_columns pcc ON pcc.constraint_object_id = fk.object_id
        JOIN sys.columns pc ON pc.object_id = pcc.parent_object_id AND pc.column_id = pcc.parent_column_id
        JOIN sys.columns rc ON rc.object_id = pcc.referenced_object_id AND rc.column_id = pcc.referenced_column_id
        JOIN sys.tables rt ON rt.object_id = fk.referenced_object_id
        WHERE fk.is_ms_shipped = 0
    ),
    AggregatedFKs AS (
        SELECT
            ConstraintName,
            ParentSchema,
            ParentTable,
            ReferencedSchema,
            ReferencedTable,
            delete_referential_action,
            update_referential_action,
            (SELECT QUOTENAME(ParentColumn) + ', '
             FROM FKColumns fc2 
             WHERE fc2.ConstraintName = fc1.ConstraintName 
             ORDER BY fc2.constraint_column_id
             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') as ParentColumns,
            (SELECT QUOTENAME(ReferencedColumn) + ', '
             FROM FKColumns fc2 
             WHERE fc2.ConstraintName = fc1.ConstraintName 
             ORDER BY fc2.constraint_column_id
             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') as ReferencedColumns
        FROM (SELECT DISTINCT ConstraintName, ParentSchema, ParentTable, ReferencedSchema, ReferencedTable, 
                     delete_referential_action, update_referential_action 
              FROM FKColumns) fc1
    )
    SELECT @FKs = COALESCE(@FKs + @CRLF + @CRLF, '') +
           'ALTER TABLE ' + QUOTENAME(ParentSchema) + '.' + QUOTENAME(ParentTable) +
           ' ADD CONSTRAINT ' + QUOTENAME(ConstraintName) + ' FOREIGN KEY (' +
           LEFT(ParentColumns, LEN(ParentColumns) - 1) + ') REFERENCES ' +
           QUOTENAME(ReferencedSchema) + '.' + QUOTENAME(ReferencedTable) + ' (' +
           LEFT(ReferencedColumns, LEN(ReferencedColumns) - 1) + ')' +
           CASE WHEN delete_referential_action = 1 THEN ' ON DELETE CASCADE'
                WHEN delete_referential_action = 2 THEN ' ON DELETE SET NULL'
                WHEN delete_referential_action = 3 THEN ' ON DELETE SET DEFAULT'
                ELSE '' END +
           CASE WHEN update_referential_action = 1 THEN ' ON UPDATE CASCADE'
                WHEN update_referential_action = 2 THEN ' ON UPDATE SET NULL'
                WHEN update_referential_action = 3 THEN ' ON UPDATE SET DEFAULT'
                ELSE '' END + ';'
    FROM AggregatedFKs;

    SET @FKs = ISNULL(@FKs, '') + @CRLF + @CRLF;

    ------------------------------------------------------------
    -- 6️⃣ Combine schema
    ------------------------------------------------------------
    DECLARE @SchemaSQL NVARCHAR(MAX) = @Header + ISNULL(@Schemas, '') + ISNULL(@Tables, '') + ISNULL(@PKs, '') + ISNULL(@FKs, '');

    ------------------------------------------------------------
    -- 7️⃣ Write schema to file - SIMPLIFIED APPROACH
    ------------------------------------------------------------
    DECLARE @Cmd VARCHAR(8000);
    
    -- Check if xp_cmdshell is enabled
    IF EXISTS (SELECT 1 FROM sys.configurations WHERE name = 'xp_cmdshell' AND value_in_use = 1)
    BEGIN
        PRINT 'xp_cmdshell is enabled. Writing schema to file...';
        
        -- Create a temporary table to hold the schema SQL in chunks
        CREATE TABLE #SchemaOutput (ID INT IDENTITY, Line VARCHAR(8000));
        
        -- Split the SQL into manageable chunks and insert into temp table
        DECLARE @ChunkSize INT = 8000;
        DECLARE @Pos INT = 1;
        DECLARE @Len INT = LEN(@SchemaSQL);
        
        WHILE @Pos <= @Len
        BEGIN
            INSERT INTO #SchemaOutput (Line)
            VALUES (SUBSTRING(@SchemaSQL, @Pos, @ChunkSize));
            
            SET @Pos = @Pos + @ChunkSize;
        END
        
        -- Use bcp to export the temporary table
        SET @Cmd = 'bcp "SELECT Line FROM tempdb..#SchemaOutput ORDER BY ID" queryout "' + CAST(@OutputFile AS VARCHAR(4000)) + '" -c -T -S ' + CAST(@@SERVERNAME AS VARCHAR(256));
        
        -- Execute bcp command
        EXEC xp_cmdshell @Cmd, NO_OUTPUT;
        
        -- Add success message to file
        SET @Cmd = 'echo ' + '-- Database script generated successfully on: ' + CONVERT(VARCHAR(30), GETDATE(), 120) + ' >> "' + CAST(@OutputFile AS VARCHAR(4000)) + '"';
        EXEC xp_cmdshell @Cmd, NO_OUTPUT;
        
        -- Clean up
        DROP TABLE #SchemaOutput;
        
        PRINT 'Database schema script generated successfully at: ' + @OutputFile;
    END
    ELSE
    BEGIN
        -- xp_cmdshell is not enabled, return the SQL as a result set
        PRINT 'xp_cmdshell is not enabled. The generated schema SQL is too large to display.';
        PRINT 'Please enable xp_cmdshell or use an alternative method to export the schema.';
        PRINT 'Generated schema length: ' + CAST(LEN(@SchemaSQL) AS VARCHAR(20)) + ' characters';
        
        -- Return the SQL in chunks if it's too large for messages
        DECLARE @Chunk VARCHAR(8000);
        DECLARE @Counter INT = 1;
        SET @Pos = 1;
        SET @Len = LEN(@SchemaSQL);
        
        WHILE @Pos <= @Len
        BEGIN
            SET @Chunk = SUBSTRING(@SchemaSQL, @Pos, 8000);
            PRINT '-- Chunk ' + CAST(@Counter AS VARCHAR(10)) + ':';
            PRINT @Chunk;
            SET @Pos = @Pos + 8000;
            SET @Counter = @Counter + 1;
        END
    END
END
GO
/****** Object:  StoredProcedure [dbo].[GenerateFullDatabaseScript1]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GenerateFullDatabaseScript1]
    @DatabaseName SYSNAME,
    @OutputFile NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CRLF NVARCHAR(2) = CHAR(13) + CHAR(10);

    ------------------------------------------------------------
    -- 1️⃣ Database header
    ------------------------------------------------------------
    DECLARE @Header NVARCHAR(MAX) = 
        '-- =============================================' + @CRLF +
        '--  Database Script for: ' + QUOTENAME(@DatabaseName) + @CRLF +
        '--  Generated on: ' + CONVERT(VARCHAR(30), GETDATE(), 120) + @CRLF +
        '-- =============================================' + @CRLF + @CRLF;

    ------------------------------------------------------------
    -- 2️⃣ Schemas
    ------------------------------------------------------------
    DECLARE @Schemas NVARCHAR(MAX) = '';
    
    SELECT @Schemas = COALESCE(@Schemas + 'CREATE SCHEMA ' + QUOTENAME(s.name) + ';' + @CRLF, '')
    FROM sys.schemas s
    WHERE s.schema_id > 4 AND s.principal_id > 4;
    
    SET @Schemas = ISNULL(@Schemas, '') + @CRLF;

    ------------------------------------------------------------
    -- 3️⃣ Tables + columns
    ------------------------------------------------------------
    DECLARE @Tables NVARCHAR(MAX) = '';

    WITH TableColumns AS (
        SELECT 
            s.name as SchemaName,
            t.name as TableName,
            c.column_id,
            '    ' + QUOTENAME(c.name) + ' ' +
            UPPER(ty.name) +
            CASE
                WHEN ty.name IN ('varchar','nvarchar','char','nchar','varbinary') THEN 
                    '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length AS VARCHAR(10)) END + ')'
                WHEN ty.name IN ('decimal','numeric') THEN 
                    '(' + CAST(c.precision AS VARCHAR(10)) + ',' + CAST(c.scale AS VARCHAR(10)) + ')'
                WHEN ty.name IN ('float') THEN 
                    '(' + CAST(c.precision AS VARCHAR(10)) + ')'
                WHEN ty.name IN ('datetime2','datetimeoffset','time') THEN 
                    '(' + CAST(c.scale AS VARCHAR(10)) + ')'
                ELSE ''
            END +
            CASE WHEN c.is_identity = 1 THEN ' IDENTITY(' + CAST(ISNULL(ic.seed_value, 1) AS VARCHAR(10)) + ',' + CAST(ISNULL(ic.increment_value, 1) AS VARCHAR(10)) + ')' ELSE '' END +
            CASE WHEN c.is_nullable = 0 THEN ' NOT NULL' ELSE ' NULL' END +
            CASE WHEN dc.definition IS NOT NULL THEN ' DEFAULT ' + dc.definition ELSE '' END
            as ColumnDefinition
        FROM sys.tables t
        JOIN sys.schemas s ON s.schema_id = t.schema_id
        JOIN sys.columns c ON c.object_id = t.object_id
        JOIN sys.types ty ON ty.user_type_id = c.user_type_id
        LEFT JOIN sys.default_constraints dc ON dc.object_id = c.default_object_id AND dc.object_id != 0
        LEFT JOIN sys.identity_columns ic ON ic.object_id = t.object_id AND ic.column_id = c.column_id
        WHERE t.is_ms_shipped = 0
    ),
    AggregatedTables AS (
        SELECT
            SchemaName,
            TableName,
            (SELECT ColumnDefinition + ', ' + @CRLF
             FROM TableColumns tc2 
             WHERE tc2.SchemaName = tc1.SchemaName AND tc2.TableName = tc1.TableName
             ORDER BY tc2.column_id
             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') as ColumnsScript
        FROM (SELECT DISTINCT SchemaName, TableName FROM TableColumns) tc1
    )
    SELECT @Tables = COALESCE(@Tables + @CRLF + @CRLF, '') + 
           'CREATE TABLE ' + QUOTENAME(SchemaName) + '.' + QUOTENAME(TableName) + ' (' + @CRLF +
           LEFT(ColumnsScript, LEN(ColumnsScript) - 3) + -- Remove trailing comma and CRLF
           @CRLF + ');'
    FROM AggregatedTables
    ORDER BY SchemaName, TableName;

    SET @Tables = ISNULL(@Tables, '') + @CRLF + @CRLF;

    ------------------------------------------------------------
    -- 4️⃣ Primary keys
    ------------------------------------------------------------
    DECLARE @PKs NVARCHAR(MAX) = '';
    
    WITH PKColumns AS (
        SELECT 
            s.name as SchemaName,
            t.name as TableName,
            k.name as ConstraintName,
            i.type as IndexType,
            c.name as ColumnName,
            ic.key_ordinal
        FROM sys.key_constraints k
        JOIN sys.tables t ON t.object_id = k.parent_object_id
        JOIN sys.schemas s ON s.schema_id = t.schema_id
        JOIN sys.indexes i ON i.object_id = t.object_id AND i.index_id = k.unique_index_id
        JOIN sys.index_columns ic ON ic.object_id = t.object_id AND ic.index_id = i.index_id
        JOIN sys.columns c ON c.object_id = t.object_id AND c.column_id = ic.column_id
        WHERE k.type = 'PK' AND t.is_ms_shipped = 0
    ),
    AggregatedPKs AS (
        SELECT
            SchemaName,
            TableName,
            ConstraintName,
            IndexType,
            (SELECT QUOTENAME(ColumnName) + ', '
             FROM PKColumns pc2 
             WHERE pc2.SchemaName = pc1.SchemaName AND pc2.TableName = pc1.TableName AND pc2.ConstraintName = pc1.ConstraintName
             ORDER BY pc2.key_ordinal
             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') as ColumnsList
        FROM (SELECT DISTINCT SchemaName, TableName, ConstraintName, IndexType FROM PKColumns) pc1
    )
    SELECT @PKs = COALESCE(@PKs + @CRLF + @CRLF, '') +
           'ALTER TABLE ' + QUOTENAME(SchemaName) + '.' + QUOTENAME(TableName) + 
           ' ADD CONSTRAINT ' + QUOTENAME(ConstraintName) + ' PRIMARY KEY ' +
           CASE WHEN IndexType = 1 THEN 'CLUSTERED' ELSE 'NONCLUSTERED' END + ' (' +
           LEFT(ColumnsList, LEN(ColumnsList) - 1) + ');' -- Remove trailing comma
    FROM AggregatedPKs;

    SET @PKs = ISNULL(@PKs, '') + @CRLF + @CRLF;

    ------------------------------------------------------------
    -- 5️⃣ Foreign keys
    ------------------------------------------------------------
    DECLARE @FKs NVARCHAR(MAX) = '';
    
    WITH FKColumns AS (
        SELECT 
            fk.name as ConstraintName,
            SCHEMA_NAME(fk.schema_id) as ParentSchema,
            OBJECT_NAME(fk.parent_object_id) as ParentTable,
            pc.name as ParentColumn,
            SCHEMA_NAME(rt.schema_id) as ReferencedSchema,
            rt.name as ReferencedTable,
            rc.name as ReferencedColumn,
            pcc.constraint_column_id,
            fk.delete_referential_action,
            fk.update_referential_action
        FROM sys.foreign_keys fk
        JOIN sys.foreign_key_columns pcc ON pcc.constraint_object_id = fk.object_id
        JOIN sys.columns pc ON pc.object_id = pcc.parent_object_id AND pc.column_id = pcc.parent_column_id
        JOIN sys.columns rc ON rc.object_id = pcc.referenced_object_id AND rc.column_id = pcc.referenced_column_id
        JOIN sys.tables rt ON rt.object_id = fk.referenced_object_id
        WHERE fk.is_ms_shipped = 0
    ),
    AggregatedFKs AS (
        SELECT
            ConstraintName,
            ParentSchema,
            ParentTable,
            ReferencedSchema,
            ReferencedTable,
            delete_referential_action,
            update_referential_action,
            (SELECT QUOTENAME(ParentColumn) + ', '
             FROM FKColumns fc2 
             WHERE fc2.ConstraintName = fc1.ConstraintName 
             ORDER BY fc2.constraint_column_id
             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') as ParentColumns,
            (SELECT QUOTENAME(ReferencedColumn) + ', '
             FROM FKColumns fc2 
             WHERE fc2.ConstraintName = fc1.ConstraintName 
             ORDER BY fc2.constraint_column_id
             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') as ReferencedColumns
        FROM (SELECT DISTINCT ConstraintName, ParentSchema, ParentTable, ReferencedSchema, ReferencedTable, 
                     delete_referential_action, update_referential_action 
              FROM FKColumns) fc1
    )
    SELECT @FKs = COALESCE(@FKs + @CRLF + @CRLF, '') +
           'ALTER TABLE ' + QUOTENAME(ParentSchema) + '.' + QUOTENAME(ParentTable) +
           ' ADD CONSTRAINT ' + QUOTENAME(ConstraintName) + ' FOREIGN KEY (' +
           LEFT(ParentColumns, LEN(ParentColumns) - 1) + ') REFERENCES ' +
           QUOTENAME(ReferencedSchema) + '.' + QUOTENAME(ReferencedTable) + ' (' +
           LEFT(ReferencedColumns, LEN(ReferencedColumns) - 1) + ')' +
           CASE WHEN delete_referential_action = 1 THEN ' ON DELETE CASCADE'
                WHEN delete_referential_action = 2 THEN ' ON DELETE SET NULL'
                WHEN delete_referential_action = 3 THEN ' ON DELETE SET DEFAULT'
                ELSE '' END +
           CASE WHEN update_referential_action = 1 THEN ' ON UPDATE CASCADE'
                WHEN update_referential_action = 2 THEN ' ON UPDATE SET NULL'
                WHEN update_referential_action = 3 THEN ' ON UPDATE SET DEFAULT'
                ELSE '' END + ';'
    FROM AggregatedFKs;

    SET @FKs = ISNULL(@FKs, '') + @CRLF + @CRLF;

    ------------------------------------------------------------
    -- 6️⃣ Combine schema
    ------------------------------------------------------------
    DECLARE @SchemaSQL NVARCHAR(MAX) = @Header + ISNULL(@Schemas, '') + ISNULL(@Tables, '') + ISNULL(@PKs, '') + ISNULL(@FKs, '');

    ------------------------------------------------------------
    -- 7️⃣ Output the schema
    ------------------------------------------------------------
    IF @OutputFile IS NOT NULL
    BEGIN
        DECLARE @Cmd VARCHAR(8000);
        DECLARE @ResultTable TABLE (OutputText VARCHAR(8000));
        
        PRINT 'Attempting to write to file: ' + @OutputFile;
        
        -- Test if we can write to the directory first
        SET @Cmd = 'dir "' + @OutputFile + '"';
        INSERT INTO @ResultTable EXEC xp_cmdshell @Cmd;
        
        IF EXISTS (SELECT 1 FROM @ResultTable WHERE OutputText LIKE '%Not Found%' OR OutputText IS NULL)
        BEGIN
            PRINT 'Directory access test failed. Trying alternative approach...';
            
            -- Try using a path that SQL Server definitely has access to
            DECLARE @DefaultPath NVARCHAR(4000);
            SET @DefaultPath = 'C:\Temp\DentistX_Schema.sql';
            
            PRINT 'Trying default path: ' + @DefaultPath;
            
            -- Create a temporary table to hold the schema SQL
            CREATE TABLE #SchemaOutput (Line NVARCHAR(MAX));
            
            -- Split the SQL into manageable chunks
            DECLARE @ChunkSize INT = 8000;
            DECLARE @Pos INT = 1;
            DECLARE @Len INT = LEN(@SchemaSQL);
            
            WHILE @Pos <= @Len
            BEGIN
                INSERT INTO #SchemaOutput (Line)
                VALUES (SUBSTRING(@SchemaSQL, @Pos, @ChunkSize));
                SET @Pos = @Pos + @ChunkSize;
            END
            
            -- Use bcp to export
            SET @Cmd = 'bcp "SELECT Line FROM tempdb..#SchemaOutput ORDER BY (SELECT NULL)" queryout "' + @DefaultPath + '" -c -T -S ' + @@SERVERNAME;
            
            DELETE FROM @ResultTable;
            INSERT INTO @ResultTable EXEC xp_cmdshell @Cmd;
            
            IF EXISTS (SELECT 1 FROM @ResultTable WHERE OutputText LIKE '%rows copied%')
            BEGIN
                PRINT 'Success! Schema written to: ' + @DefaultPath;
                SELECT @DefaultPath AS OutputFile;
            END
            ELSE
            BEGIN
                PRINT 'File write failed. Returning schema as result instead.';
                SELECT @SchemaSQL AS DatabaseSchema;
            END
            
            DROP TABLE #SchemaOutput;
        END
        ELSE
        BEGIN
            -- Directory exists, try to write the file
            CREATE TABLE #SchemaOutput2 (Line NVARCHAR(MAX));
            
            DECLARE @ChunkSize2 INT = 8000;
            DECLARE @Pos2 INT = 1;
            DECLARE @Len2 INT = LEN(@SchemaSQL);
            
            WHILE @Pos2 <= @Len2
            BEGIN
                INSERT INTO #SchemaOutput2 (Line)
                VALUES (SUBSTRING(@SchemaSQL, @Pos2, @ChunkSize2));
                SET @Pos2 = @Pos2 + @ChunkSize2;
            END
            
            SET @Cmd = 'bcp "SELECT Line FROM tempdb..#SchemaOutput2 ORDER BY (SELECT NULL)" queryout "' + @OutputFile + '" -c -T -S ' + @@SERVERNAME;
            
            DELETE FROM @ResultTable;
            INSERT INTO @ResultTable EXEC xp_cmdshell @Cmd;
            
            IF EXISTS (SELECT 1 FROM @ResultTable WHERE OutputText LIKE '%rows copied%')
            BEGIN
                PRINT 'Success! Schema written to: ' + @OutputFile;
                SELECT @OutputFile AS OutputFile;
            END
            ELSE
            BEGIN
                PRINT 'File write failed. BCP output:';
                SELECT OutputText FROM @ResultTable WHERE OutputText IS NOT NULL;
                PRINT 'Returning schema as result instead.';
                SELECT @SchemaSQL AS DatabaseSchema;
            END
            
            DROP TABLE #SchemaOutput2;
        END
    END
    ELSE
    BEGIN
        -- No output file specified, return as result
        SELECT @SchemaSQL AS DatabaseSchema;
        PRINT 'Database schema generated successfully. Copy the result above and save to a .sql file.';
    END
END
GO
/****** Object:  StoredProcedure [dbo].[GetAnyMnthAnyYr_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAnyMnthAnyYr_Proc]
    @m INT,   -- Month (1–12)
    @Yr INT   -- Year
AS
BEGIN
    SELECT 
        @m AS MNo,
        DATENAME(MONTH, DATEADD(MONTH, @m, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8 AS MName,
        @Yr AS YearT,

        -- Treatments
        ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @m AND YEAR(TrtDate) = @Yr), 0) AS TrtTot,
        ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) AS TrtPay,
        ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) -
        ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @m AND YEAR(TrtDate) = @Yr), 0) AS TrtRem,

        -- Invoices
        ISNULL((SELECT SUM(InvoiceNet) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @m AND YEAR(InvoiceDate) = @Yr), 0) AS InvTot,
        ISNULL((SELECT SUM(Amount) FROM TblInvPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) AS InvPay,
        ISNULL((SELECT SUM(Amount) FROM TblInvPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) -
        ISNULL((SELECT SUM(InvoiceNet) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @m AND YEAR(InvoiceDate) = @Yr), 0) AS InvRem,

        -- Labs
        ISNULL((SELECT SUM(Price) FROM LabOrder WHERE MONTH(DeliveryDate) = @m AND YEAR(DeliveryDate) = @Yr), 0) AS LabTot,
        ISNULL((SELECT SUM(PayValue) FROM LabPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) AS LabPay,
        ISNULL((SELECT SUM(PayValue) FROM LabPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) -
        ISNULL((SELECT SUM(Price) FROM LabOrder WHERE MONTH(DeliveryDate) = @m AND YEAR(DeliveryDate) = @Yr), 0) AS LabRem,

        -- Doctors
        ISNULL((SELECT SUM(WrkVal) FROM DrWork WHERE MONTH(WrkDate) = @m AND YEAR(WrkDate) = @Yr), 0) AS DocTot,
        ISNULL((SELECT SUM(PayValue) FROM DrWorkPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) AS DocPay,
        ISNULL((SELECT SUM(PayValue) FROM DrWorkPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) -
        ISNULL((SELECT SUM(WrkVal) FROM DrWork WHERE MONTH(WrkDate) = @m AND YEAR(WrkDate) = @Yr), 0) AS DocRem,

        -- Expenses
        ISNULL((SELECT SUM(MasrofAmount) FROM TblMasareef WHERE MONTH(MasrofDate) = @m AND YEAR(MasrofDate) = @Yr), 0) AS ExpTot,
        ISNULL((SELECT SUM(PayValue) FROM TblExpenPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) AS ExpPay,
        ISNULL((SELECT SUM(PayValue) FROM TblExpenPay WHERE MONTH(PayDate) = @m AND YEAR(PayDate) = @Yr), 0) -
        ISNULL((SELECT SUM(MasrofAmount) FROM TblMasareef WHERE MONTH(MasrofDate) = @m AND YEAR(MasrofDate) = @Yr), 0) AS ExpRem
END
GO
/****** Object:  StoredProcedure [dbo].[GetAnyQrtr_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAnyQrtr_Proc]
    @m INT,   -- Quarter (1-4)
    @Yr INT   -- Year
AS
BEGIN
    -- Create a temporary table to hold the results
    CREATE TABLE #Results (
        MNo INT,
        MName NVARCHAR(50),
        YearT INT,
        TrtTot MONEY,
        TrtPay MONEY,
        TrtRem MONEY,
        InvTot MONEY,
        InvPay MONEY,
        InvRem MONEY,
        LabTot MONEY,
        LabPay MONEY,
        LabRem MONEY,
        DocTot MONEY,
        DocPay MONEY,
        DocRem MONEY,
        ExpTot MONEY,
        ExpPay MONEY,
        ExpRem MONEY
    )

    DECLARE @i INT
    
    -- Quarter 1 (January-March)
    IF @m = 1
    BEGIN
        SET @i = 1
        WHILE (@i < 4)
        BEGIN
            INSERT INTO #Results
            SELECT 
                @i,
                DATENAME(MONTH, DATEADD(MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
                @Yr,
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) = @Yr),
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) = @Yr),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) = @Yr),
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) = @Yr),
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) = @Yr),
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) = @Yr)
            
            SET @i = @i + 1
        END
    END
    
    -- Quarter 2 (April-June)
    ELSE IF @m = 2
    BEGIN
        SET @i = 4
        WHILE (@i < 7)
        BEGIN
            INSERT INTO #Results
            SELECT 
                @i,
                DATENAME(MONTH, DATEADD(MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
                @Yr,
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) = @Yr),
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) = @Yr),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) = @Yr),
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) = @Yr),
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) = @Yr),
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) = @Yr)
            
            SET @i = @i + 1
        END
    END
    
    -- Quarter 3 (July-September)
    ELSE IF @m = 3
    BEGIN
        SET @i = 7
        WHILE (@i < 10)
        BEGIN
            INSERT INTO #Results
            SELECT 
                @i,
                DATENAME(MONTH, DATEADD(MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
                @Yr,
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) = @Yr),
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) = @Yr),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) = @Yr),
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) = @Yr),
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) = @Yr),
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) = @Yr)
            
            SET @i = @i + 1
        END
    END
    
    -- Quarter 4 (October-December)
    ELSE IF @m = 4
    BEGIN
        SET @i = 10
        WHILE (@i < 13)
        BEGIN
            INSERT INTO #Results
            SELECT 
                @i,
                DATENAME(MONTH, DATEADD(MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
                @Yr,
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) = @Yr),
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) = @Yr),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) = @Yr),
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) = @Yr),
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) = @Yr),
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) = @Yr) -
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) = @Yr)
            
            SET @i = @i + 1
        END
    END

    -- Return the results
    SELECT * FROM #Results
    
    -- Clean up
    DROP TABLE #Results
END
GO
/****** Object:  StoredProcedure [dbo].[GetAnyQrtr2Years_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAnyQrtr2Years_Proc]
(
    @m INT,
    @Yr1 INT,
    @Yr2 INT
)
AS
BEGIN
    -- Create temporary table to hold results
    CREATE TABLE #Results (
        MNo INT,  
        MName NVARCHAR(50),
        Year1 INT,
        Year2 INT,
        TrtTot MONEY, 
        TrtPay MONEY, 
        TrtRem MONEY,
        InvTot MONEY, 
        InvPay MONEY, 
        InvRem MONEY,
        LabTot MONEY, 
        LabPay MONEY, 
        LabRem MONEY,
        DocTot MONEY, 
        DocPay MONEY, 
        DocRem MONEY,
        ExpTot MONEY, 
        ExpPay MONEY, 
        ExpRem MONEY
    )

    DECLARE @i INT = CASE @m
                        WHEN 1 THEN 1
                        WHEN 2 THEN 4
                        WHEN 3 THEN 7
                        WHEN 4 THEN 10
                     END;

    DECLARE @max INT = @i + 3;

    WHILE @i < @max
    BEGIN
        INSERT INTO #Results
        SELECT  
            @i,
            DATENAME(MONTH, DATEADD(MONTH, @i - 1, 0)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            @Yr1,
            @Yr2,

            -- Treatments
            ISNULL((SELECT SUM(trtValue) FROM Patient_Trts 
                   WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM Patient_Pays 
                   WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM Patient_Pays 
                   WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0)
            - ISNULL((SELECT SUM(trtValue) FROM Patient_Trts 
                     WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) BETWEEN @Yr1 AND @Yr2), 0),

            -- Invoices
            ISNULL((SELECT SUM(InvoiceNet) FROM TblInvoicesHeader 
                   WHERE MONTH(InvoiceDate) = @i AND YEAR(InvoiceDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(Amount) FROM TblInvPay 
                   WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(Amount) FROM TblInvPay 
                   WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0)
            - ISNULL((SELECT SUM(InvoiceNet) FROM TblInvoicesHeader 
                     WHERE MONTH(InvoiceDate) = @i AND YEAR(InvoiceDate) BETWEEN @Yr1 AND @Yr2), 0),

            -- Lab
            ISNULL((SELECT SUM(Price) FROM LabOrder 
                   WHERE MONTH(DeliveryDate) = @i AND YEAR(DeliveryDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM LabPay 
                   WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM LabPay 
                   WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0)
            - ISNULL((SELECT SUM(Price) FROM LabOrder 
                     WHERE MONTH(DeliveryDate) = @i AND YEAR(DeliveryDate) BETWEEN @Yr1 AND @Yr2), 0),

            -- Doctor Work
            ISNULL((SELECT SUM(WrkVal) FROM DrWork 
                   WHERE MONTH(WrkDate) = @i AND YEAR(WrkDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM DrWorkPay 
                   WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM DrWorkPay 
                   WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0)
            - ISNULL((SELECT SUM(WrkVal) FROM DrWork 
                     WHERE MONTH(WrkDate) = @i AND YEAR(WrkDate) BETWEEN @Yr1 AND @Yr2), 0),

            -- Expenses
            ISNULL((SELECT SUM(MasrofAmount) FROM TblMasareef 
                   WHERE MONTH(MasrofDate) = @i AND YEAR(MasrofDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM TblExpenPay 
                   WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0),
            ISNULL((SELECT SUM(PayValue) FROM TblExpenPay 
                   WHERE MONTH(PayDate) = @i AND YEAR(PayDate) BETWEEN @Yr1 AND @Yr2), 0)
            - ISNULL((SELECT SUM(MasrofAmount) FROM TblMasareef 
                     WHERE MONTH(MasrofDate) = @i AND YEAR(MasrofDate) BETWEEN @Yr1 AND @Yr2), 0)

        SET @i = @i + 1
    END

    -- Return the results
    SELECT * FROM #Results
    
    -- Clean up
    DROP TABLE #Results
END
GO
/****** Object:  StoredProcedure [dbo].[GetAnyQrtr2Yrs_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAnyQrtr2Yrs_Proc]
(
    @m INT,
    @Yr1 INT,
    @Yr2 INT
)
AS
BEGIN
    -- Create temporary table to hold results
    CREATE TABLE #Results (
        MNo INT,  
        MName NVARCHAR(50),
        Year1 INT,
        Year2 INT,
        TrtTot MONEY, 
        TrtPay MONEY, 
        TrtRem MONEY,
        InvTot MONEY, 
        InvPay MONEY, 
        InvRem MONEY,
        LabTot MONEY, 
        LabPay MONEY, 
        LabRem MONEY,
        DocTot MONEY, 
        DocPay MONEY, 
        DocRem MONEY,
        ExpTot MONEY, 
        ExpPay MONEY, 
        ExpRem MONEY
    )

    DECLARE @i INT
    
    -- Quarter 1 (January-March)
    IF @m = 1
    BEGIN
        SET @i = 1
        WHILE (@i < 4)
        BEGIN
            INSERT INTO #Results
            SELECT 
                @i,
                DATENAME(MONTH, DATEADD(MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
                @Yr1,
                @Yr2,
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) BETWEEN @Yr1 AND @Yr2)
            
            SET @i = @i + 1
        END
    END
    
    -- Quarter 2 (April-June)
    ELSE IF @m = 2
    BEGIN
        SET @i = 4
        WHILE (@i < 7)
        BEGIN
            INSERT INTO #Results
            SELECT 
                @i,
                DATENAME(MONTH, DATEADD(MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
                @Yr1,
                @Yr2,
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) BETWEEN @Yr1 AND @Yr2)
            
            SET @i = @i + 1
        END
    END
    
    -- Quarter 3 (July-September)
    ELSE IF @m = 3
    BEGIN
        SET @i = 7
        WHILE (@i < 10)
        BEGIN
            INSERT INTO #Results
            SELECT 
                @i,
                DATENAME(MONTH, DATEADD(MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
                @Yr1,
                @Yr2,
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) BETWEEN @Yr1 AND @Yr2)
            
            SET @i = @i + 1
        END
    END
    
    -- Quarter 4 (October-December)
    ELSE IF @m = 4
    BEGIN
        SET @i = 10
        WHILE (@i < 13)
        BEGIN
            INSERT INTO #Results
            SELECT 
                @i,
                DATENAME(MONTH, DATEADD(MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
                @Yr1,
                @Yr2,
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate]) = @i AND YEAR([InvoiceDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate]) = @i AND YEAR([DeliveryDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate]) = @i AND YEAR([WrkDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2),
                (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2) -
                (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) BETWEEN @Yr1 AND @Yr2)
            
            SET @i = @i + 1
        END
    END

    -- Return the results
    SELECT * FROM #Results
    
    -- Clean up
    DROP TABLE #Results
END
GO
/****** Object:  StoredProcedure [dbo].[GetAnyQrtrAnyYear_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAnyQrtrAnyYear_Proc]
(
    @m INT,   -- Quarter Number: 1 to 4
    @Yr INT
)
AS
BEGIN
    -- Create temporary table to hold results
    CREATE TABLE #Results (
        MNo INT, 
        MName NVARCHAR(50), 
        YearT INT,
        TrtTot MONEY, 
        TrtPay MONEY, 
        TrtRem MONEY,
        InvTot MONEY, 
        InvPay MONEY, 
        InvRem MONEY,
        LabTot MONEY, 
        LabPay MONEY, 
        LabRem MONEY,
        DocTot MONEY, 
        DocPay MONEY, 
        DocRem MONEY,
        ExpTot MONEY, 
        ExpPay MONEY, 
        ExpRem MONEY
    )

    DECLARE @StartMonth INT = CASE @m
                                WHEN 1 THEN 1
                                WHEN 2 THEN 4
                                WHEN 3 THEN 7
                                WHEN 4 THEN 10
                              END

    DECLARE @i INT = @StartMonth

    WHILE @i < @StartMonth + 3
    BEGIN
        DECLARE 
            @TrtTot MONEY = (SELECT ISNULL(SUM(trtValue), 0) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) = @Yr),
            @TrtPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @InvTot MONEY = (SELECT ISNULL(SUM(InvoiceNet), 0) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @i AND YEAR(InvoiceDate) = @Yr),
            @InvPay MONEY = (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @LabTot MONEY = (SELECT ISNULL(SUM(Price), 0) FROM LabOrder WHERE MONTH(DeliveryDate) = @i AND YEAR(DeliveryDate) = @Yr),
            @LabPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM LabPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @DocTot MONEY = (SELECT ISNULL(SUM(WrkVal), 0) FROM DrWork WHERE MONTH(WrkDate) = @i AND YEAR(WrkDate) = @Yr),
            @DocPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM DrWorkPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @ExpTot MONEY = (SELECT ISNULL(SUM(MasrofAmount), 0) FROM TblMasareef WHERE MONTH(MasrofDate) = @i AND YEAR(MasrofDate) = @Yr),
            @ExpPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM TblExpenPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr)

        INSERT INTO #Results
        SELECT 
            @i,
            DATENAME(MONTH, DATEFROMPARTS(@Yr, @i, 1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            @Yr,
            @TrtTot, @TrtPay, @TrtPay - @TrtTot,
            @InvTot, @InvPay, @InvPay - @InvTot,
            @LabTot, @LabPay, @LabPay - @LabTot,
            @DocTot, @DocPay, @DocPay - @DocTot,
            @ExpTot, @ExpPay, @ExpPay - @ExpTot

        SET @i += 1
    END

    -- Return the results
    SELECT * FROM #Results
    
    -- Clean up
    DROP TABLE #Results
END
GO
/****** Object:  StoredProcedure [dbo].[GetAnyQruarter_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAnyQruarter_Proc]
(
    @m INT,  -- Quarter (1 to 4)
    @Yr INT
)
AS
BEGIN
    -- Create temporary table to hold results
    CREATE TABLE #Results (
        MNo INT,
        MName NVARCHAR(50),
        YearT INT,
        TrtTot MONEY, 
        TrtPay MONEY, 
        TrtRem MONEY,
        InvTot MONEY, 
        InvPay MONEY, 
        InvRem MONEY,
        LabTot MONEY, 
        LabPay MONEY, 
        LabRem MONEY,
        DocTot MONEY, 
        DocPay MONEY, 
        DocRem MONEY,
        ExpTot MONEY, 
        ExpPay MONEY, 
        ExpRem MONEY
    )

    -- Determine starting month based on quarter
    DECLARE @StartMonth INT = CASE @m
                                WHEN 1 THEN 1
                                WHEN 2 THEN 4
                                WHEN 3 THEN 7
                                WHEN 4 THEN 10
                              END

    DECLARE @i INT = @StartMonth

    -- Process each month in the quarter
    WHILE @i < @StartMonth + 3
    BEGIN
        -- Calculate all metrics for the current month
        DECLARE 
            @TrtTot MONEY = (SELECT ISNULL(SUM(trtValue), 0) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) = @Yr),
            @TrtPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @InvTot MONEY = (SELECT ISNULL(SUM(InvoiceNet), 0) FROM TblInvoicesHeader WHERE MONTH(InvoiceDate) = @i AND YEAR(InvoiceDate) = @Yr),
            @InvPay MONEY = (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @LabTot MONEY = (SELECT ISNULL(SUM(Price), 0) FROM LabOrder WHERE MONTH(DeliveryDate) = @i AND YEAR(DeliveryDate) = @Yr),
            @LabPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM LabPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @DocTot MONEY = (SELECT ISNULL(SUM(WrkVal), 0) FROM DrWork WHERE MONTH(WrkDate) = @i AND YEAR(WrkDate) = @Yr),
            @DocPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM DrWorkPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr),

            @ExpTot MONEY = (SELECT ISNULL(SUM(MasrofAmount), 0) FROM TblMasareef WHERE MONTH(MasrofDate) = @i AND YEAR(MasrofDate) = @Yr),
            @ExpPay MONEY = (SELECT ISNULL(SUM(PayValue), 0) FROM TblExpenPay WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr)

        -- Insert results for current month
        INSERT INTO #Results
        SELECT 
            @i,
            DATENAME(MONTH, DATEFROMPARTS(@Yr, @i, 1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            @Yr,
            @TrtTot, @TrtPay, @TrtPay - @TrtTot,
            @InvTot, @InvPay, @InvPay - @InvTot,
            @LabTot, @LabPay, @LabPay - @LabTot,
            @DocTot, @DocPay, @DocPay - @DocTot,
            @ExpTot, @ExpPay, @ExpPay - @ExpTot

        SET @i += 1
    END

    -- Return the results
    SELECT * FROM #Results
    
    -- Clean up
    DROP TABLE #Results
END
GO
/****** Object:  StoredProcedure [dbo].[GetAnyYear_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAnyYear_Proc]
(
    @Yr INT
)
AS
BEGIN
    -- Create temporary table to hold results
    CREATE TABLE #Results (
        YearT INT,
        MNo INT,  
        MName NVARCHAR(50),
        TrtTot MONEY,
        TrtPay MONEY, 
        TrtRem MONEY,
        InvTot MONEY,
        InvPay MONEY, 
        InvRem MONEY,
        LabTot MONEY,
        LabPay MONEY, 
        LabRem MONEY,
        DocTot MONEY,
        DocPay MONEY, 
        DocRem MONEY,
        ExpTot MONEY,
        ExpPay MONEY, 
        ExpRem MONEY
    )

    DECLARE @i INT = 1
    
    -- Process each month of the year
    WHILE (@i < 13)
    BEGIN
        -- Insert monthly data
        INSERT INTO #Results
        SELECT      
            @Yr,
            @i,
            DATENAME(MONTH, DATEADD(MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            -- Treatments
            (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate])=@i AND YEAR([TrtDate])=@Yr),
            (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate])=@i AND YEAR([PayDate])=@Yr),
            (SELECT ISNULL(SUM(PayValue), 0) FROM [Patient_Pays] WHERE MONTH([PayDate])=@i AND YEAR([PayDate])=@Yr) -
            (SELECT ISNULL(SUM(trtValue), 0) FROM [Patient_Trts] WHERE MONTH([TrtDate])=@i AND YEAR([TrtDate])=@Yr),
            -- Invoices
            (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate])=@i AND YEAR([InvoiceDate])=@Yr),
            (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate)=@i AND YEAR([PayDate])=@Yr),
            (SELECT ISNULL(SUM(Amount), 0) FROM TblInvPay WHERE MONTH(PayDate)=@i AND YEAR([PayDate])=@Yr) -
            (SELECT ISNULL(SUM([InvoiceNet]), 0) FROM [TblInvoicesHeader] WHERE MONTH([InvoiceDate])=@i AND YEAR([InvoiceDate])=@Yr),
            -- Labs
            (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate])=@i AND YEAR([DeliveryDate])=@Yr),
            (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate])=@i AND YEAR([PayDate])=@Yr),
            (SELECT ISNULL(SUM(PayValue), 0) FROM [LabPay] WHERE MONTH([PayDate])=@i AND YEAR([PayDate])=@Yr) -
            (SELECT ISNULL(SUM([Price]), 0) FROM [LabOrder] WHERE MONTH([DeliveryDate])=@i AND YEAR([DeliveryDate])=@Yr),
            -- Doctors
            (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate])=@i AND YEAR([WrkDate])=@Yr),
            (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate])=@i AND YEAR([PayDate])=@Yr),
            (SELECT ISNULL(SUM(PayValue), 0) FROM [DrWorkPay] WHERE MONTH([PayDate])=@i AND YEAR([PayDate])=@Yr) -
            (SELECT ISNULL(SUM([WrkVal]), 0) FROM [DrWork] WHERE MONTH([WrkDate])=@i AND YEAR([WrkDate])=@Yr),
            -- Expenses
            (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate])=@i AND YEAR([MasrofDate])=@Yr),
            (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate])=@i AND YEAR([PayDate])=@Yr),
            (SELECT ISNULL(SUM(PayValue), 0) FROM [TblExpenPay] WHERE MONTH([PayDate])=@i AND YEAR([PayDate])=@Yr) -
            (SELECT ISNULL(SUM([MasrofAmount]), 0) FROM [TblMasareef] WHERE MONTH([MasrofDate])=@i AND YEAR([MasrofDate])=@Yr)
        
        SET @i = @i + 1
    END

    -- Return the results
    SELECT * FROM #Results
    ORDER BY MNo
    
    -- Clean up
    DROP TABLE #Results
END
GO
/****** Object:  StoredProcedure [dbo].[GetAnyYrTRT_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAnyYrTRT_Proc]
(
    @Yr INT
)
AS
BEGIN
    -- Create temporary table for results
    CREATE TABLE #Results (
        MNo INT,
        MName NVARCHAR(50),
        TrtTot MONEY,
        TrtPay MONEY,
        TrtRem MONEY
    )

    DECLARE @i INT = 1

    -- Process each month of the year
    WHILE (@i <= 12)
    BEGIN
        -- Calculate treatment metrics
        DECLARE @TrtTot MONEY, @TrtPay MONEY
        
        -- Get total treatment values for the month/year
        SELECT @TrtTot = ISNULL(SUM(trtValue), 0)
        FROM Patient_Trts
        WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) = @Yr
        
        -- Get total payments for the month/year
        SELECT @TrtPay = ISNULL(SUM(PayValue), 0)
        FROM Patient_Pays
        WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr

        -- Insert results for current month
        INSERT INTO #Results
        SELECT
            @i,
            DATENAME(MONTH, DATEADD(MONTH, @i - 1, 0)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            @TrtTot,
            @TrtPay,
            @TrtPay - @TrtTot

        SET @i += 1
    END

    -- Return results ordered by month number
    SELECT * FROM #Results
    ORDER BY MNo
    
    -- Clean up temporary table
    DROP TABLE #Results
END
GO
/****** Object:  StoredProcedure [dbo].[GetEffectivePermissionsByUser]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetEffectivePermissionsByUser]
    @UsID INT
AS
BEGIN
    SELECT 
        p.PermName,
        ISNULL(up.IsAllowed, gp.IsAllowed) AS IsAllowed
    FROM USERS u
    INNER JOIN Groups g ON u.GroupID = g.GroupID
    CROSS JOIN Permissions p
    LEFT JOIN GroupPermissions gp ON gp.GroupID = g.GroupID AND gp.PermID = p.PermID
    LEFT JOIN UserPermissions up ON up.UsID = u.UsID AND up.PermID = p.PermID
    WHERE u.UsID = @UsID;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetInvPayments_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInvPayments_Proc]
(
    @InvId INT
)
AS
BEGIN
    SELECT        
        dbo.TblInvPay.PayID, 
        dbo.TblResources.ResName, 
        dbo.TblInvPay.PayDate, 
        dbo.TblInvPay.Amount, 
        dbo.TblInvPay.Notes, 
        dbo.TblInvPay.InvRemain,
        dbo.TblInvPay.InvoiceId, 
        dbo.TblInvPay.ResID
    FROM            
        dbo.TblResources 
    INNER JOIN
        dbo.TblInvPay ON dbo.TblResources.ResId = dbo.TblInvPay.ResID 
    WHERE
        dbo.TblInvPay.InvoiceId = @InvId
    ORDER BY
        dbo.TblInvPay.PayDate  -- Added sorting for consistent results
END
GO
/****** Object:  StoredProcedure [dbo].[GetNextPatientNumberSP]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---- Create a sequence tracking table (only once)
--CREATE TABLE dbo.PatientNumberSequence (
--    ID INT IDENTITY(1,1) PRIMARY KEY,
--    LastNumber BIGINT NOT NULL
--);

---- Initialize it once with your current max
--INSERT INTO dbo.PatientNumberSequence (LastNumber)
--SELECT ISNULL(MAX(CAST(PatientNumber AS BIGINT)), 0)
--FROM dbo.Patient
--WHERE ISNUMERIC(PatientNumber) = 1;
--GO
CREATE   PROC [dbo].[GetNextPatientNumberSP]
    @NextPatientNumber NVARCHAR(10) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewVal BIGINT;

    BEGIN TRANSACTION;

    UPDATE dbo.PatientNumberSequence
    SET @NewVal = LastNumber = LastNumber + 1;

    COMMIT TRANSACTION;

    SET @NextPatientNumber = RIGHT(REPLICATE('0', 5) + CAST(@NewVal AS NVARCHAR(10)), 5);
END;
GO
/****** Object:  StoredProcedure [dbo].[GetPatientFinancialDetails]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPatientFinancialDetails]
    @PatientID INT
AS
BEGIN


WITH TreatmentTotals AS (
    SELECT PatientID, SUM(ISNULL(TrtValue, 0)) AS TotalTreatments
    FROM Patient_Trts
    WHERE PatientID = @PatientID
    GROUP BY PatientID
),
PaymentTotals AS (
    SELECT pt.PatientID, SUM(ISNULL(pp.PayValue, 0)) AS TotalPayments
    FROM Patient_Trts pt
    JOIN Patient_Pays pp ON pt.TrtID = pp.TrtID
    WHERE pt.PatientID = @PatientID
    GROUP BY pt.PatientID
)
SELECT 
    p.PatientID, 
    p.PatientName, 
    p.Sex, 
    p.Age, 
    p.Phone, 
    p.Address, 
    pt.TrtID, 
    pt.Detail AS TreatmentDetail, 
    pt.TrtDate AS TreatmentDate, 
    ISNULL(pt.TrtValue, 0) AS TreatmentValue,
    pp.PayID,
    ISNULL(pp.PayValue, 0) AS PayValue,
    pp.PayDate,
    pp.Notes AS PaymentNotes,
    ISNULL(tt.TotalTreatments, 0) AS TotalTreatments,
    ISNULL(ptot.TotalPayments, 0) AS TotalPayments,
    ISNULL(ptot.TotalPayments, 0) - ISNULL(tt.TotalTreatments, 0) AS Balance
FROM 
    dbo.Patient p
LEFT JOIN 
    dbo.Patient_Trts pt ON p.PatientID = pt.PatientID
LEFT JOIN 
    dbo.Patient_Pays pp ON pt.TrtID = pp.TrtID
LEFT JOIN 
    TreatmentTotals tt ON p.PatientID = tt.PatientID
LEFT JOIN 
    PaymentTotals ptot ON p.PatientID = ptot.PatientID
WHERE 
    p.PatientID = @PatientID
ORDER BY 
    pt.TrtDate DESC,
    pp.PayDate DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[GetPatientFinancialReport]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPatientFinancialReport]
    @PatientID INT
AS
BEGIN
    -- Patient Details
 
SELECT 
    p.PatientID, 
    p.PatientName, 
    p.Sex, 
    p.Age, 
    p.Phone, 
    p.Address,
    'PATIENT' AS RecordType
FROM 
    dbo.Patient p
WHERE 
    p.PatientID = @PatientID;

-- All Treatments
SELECT 
    pt.TrtID,
    pt.Detail AS TreatmentDetail,
    pt.TrtDate AS TreatmentDate,
    ISNULL(pt.TrtValue, 0) AS TreatmentValue,
    'TREATMENT' AS RecordType
FROM 
    dbo.Patient_Trts pt
WHERE 
    pt.PatientID = @PatientID
ORDER BY 
    pt.TrtDate DESC;

-- All Payments
SELECT 
    pp.PayID,
    pp.PayDate,
    ISNULL(pp.PayValue, 0) AS PayValue,
    pp.Notes AS PaymentNotes,
    'PAYMENT' AS RecordType
FROM 
    dbo.Patient_Pays pp
INNER JOIN 
    dbo.Patient_Trts pt ON pp.TrtID = pt.TrtID
WHERE 
    pt.PatientID = @PatientID
ORDER BY 
    pp.PayDate DESC;


-- Financial Summary with corrected totals
-- Separate sums to avoid duplication
;WITH TrtTotal AS (
    SELECT SUM(ISNULL(TrtValue, 0)) AS TotalTreatments
    FROM dbo.Patient_Trts
    WHERE PatientID = @PatientID
),
PayTotal AS (
    SELECT SUM(ISNULL(PayValue, 0)) AS TotalPayments
    FROM dbo.Patient_Pays
    WHERE TrtID IN (
        SELECT TrtID FROM dbo.Patient_Trts WHERE PatientID = @PatientID
    )
)
SELECT 
    ISNULL(t.TotalTreatments, 0) AS TotalTreatments,
    ISNULL(p.TotalPayments, 0) AS TotalPayments,
    ISNULL(p.TotalPayments, 0) - ISNULL(t.TotalTreatments, 0) AS Balance,
    'SUMMARY' AS RecordType
FROM 
    TrtTotal t
CROSS JOIN 
    PayTotal p;


END
GO
/****** Object:  StoredProcedure [dbo].[GetPatientFinancialReport11]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPatientFinancialReport11]
    @PatientID INT
AS
BEGIN
    -- Patient Details
 
SELECT 
    p.PatientID, 
    p.PatientName, 
    p.Sex, 
    p.Age, 
    p.Phone, 
    p.Address,
    'PATIENT' AS RecordType
FROM 
    dbo.Patient p
WHERE 
    p.PatientID = @PatientID;

-- All Treatments
SELECT 
    pt.TrtID,
    pt.Detail AS TreatmentDetail,
    pt.TrtDate AS TreatmentDate,
    ISNULL(pt.TrtValue, 0) AS TreatmentValue,
    'TREATMENT' AS RecordType
FROM 
    dbo.Patient_Trts pt
WHERE 
    pt.PatientID = @PatientID
ORDER BY 
    pt.TrtDate DESC;

-- All Payments
SELECT 
    pp.PayID,
    pp.PayDate,
    ISNULL(pp.PayValue, 0) AS PayValue,
    pp.Notes AS PaymentNotes,
    'PAYMENT' AS RecordType
FROM 
    dbo.Patient_Pays pp
INNER JOIN 
    dbo.Patient_Trts pt ON pp.TrtID = pt.TrtID
WHERE 
    pt.PatientID = @PatientID
ORDER BY 
    pp.PayDate DESC;

-- Financial Summary with corrected totals
-- Separate sums to avoid duplication
;WITH TrtTotal AS (
    SELECT SUM(ISNULL(TrtValue, 0)) AS TotalTreatments
    FROM dbo.Patient_Trts
    WHERE PatientID = @PatientID
),
PayTotal AS (
    SELECT SUM(ISNULL(PayValue, 0)) AS TotalPayments
    FROM dbo.Patient_Pays
    WHERE TrtID IN (
        SELECT TrtID FROM dbo.Patient_Trts WHERE PatientID = @PatientID
    )
)
SELECT 
    ISNULL(t.TotalTreatments, 0) AS TotalTreatments,
    ISNULL(p.TotalPayments, 0) AS TotalPayments,
    ISNULL(p.TotalPayments, 0) - ISNULL(t.TotalTreatments, 0) AS Balance,
    'SUMMARY' AS RecordType
FROM 
    TrtTotal t
CROSS JOIN 
    PayTotal p;


END
GO
/****** Object:  StoredProcedure [dbo].[GetPatientFinancialReport14]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPatientFinancialReport14]
    @PatientID INT
AS
BEGIN
    -- Patient Details
 
SELECT 
    p.PatientID, 
    p.PatientName, 
    p.Sex, 
    p.Age, 
    p.Phone, 
    p.Address,
    'PATIENT' AS RecordType
FROM 
    dbo.Patient p
WHERE 
    p.PatientID = @PatientID;

-- All Treatments
SELECT 
    pt.TrtID,
    pt.Detail AS TreatmentDetail,
    pt.TrtDate AS TreatmentDate,
    ISNULL(pt.TrtValue, 0) AS TreatmentValue,
    'TREATMENT' AS RecordType
FROM 
    dbo.Patient_Trts pt
WHERE 
    pt.PatientID = @PatientID
ORDER BY 
    pt.TrtDate DESC;

-- All Payments
SELECT 
    pp.PayID,
    pp.PayDate,
    ISNULL(pp.PayValue, 0) AS PayValue,
    pp.Notes AS PaymentNotes,
    'PAYMENT' AS RecordType
FROM 
    dbo.Patient_Pays pp
INNER JOIN 
    dbo.Patient_Trts pt ON pp.TrtID = pt.TrtID
WHERE 
    pt.PatientID = @PatientID
ORDER BY 
    pp.PayDate DESC;

-- Financial Summary with corrected totals
-- Separate sums to avoid duplication
SELECT 
    ISNULL((
        SELECT SUM(ISNULL(TrtValue, 0))
        FROM dbo.Patient_Trts
        WHERE PatientID = @PatientID
    ), 0) AS TotalTreatments,

    ISNULL((
        SELECT SUM(ISNULL(PayValue, 0))
        FROM dbo.Patient_Pays
        WHERE TrtID IN (
            SELECT TrtID FROM dbo.Patient_Trts WHERE PatientID = @PatientID
        )
    ), 0) AS TotalPayments,

    ISNULL((
        SELECT SUM(ISNULL(PayValue, 0))
        FROM dbo.Patient_Pays
        WHERE TrtID IN (
            SELECT TrtID FROM dbo.Patient_Trts WHERE PatientID = @PatientID
        )
    ), 0) 
    - 
    ISNULL((
        SELECT SUM(ISNULL(TrtValue, 0))
        FROM dbo.Patient_Trts
        WHERE PatientID = @PatientID
    ), 0) AS Balance,

    'SUMMARY' AS RecordType;



END
GO
/****** Object:  StoredProcedure [dbo].[GetPatientFinancialReportUnified]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPatientFinancialReportUnified]
    @PatientID INT
AS
BEGIN
--DECLARE @PatientID INT = 2
    -- Create temp table for patient details
    CREATE TABLE #PatientData (
        PatientID INT,
        PatientName NVARCHAR(200),
        Sex NVARCHAR(20),
        Age INT,
        Phone NVARCHAR(20),
        Address NVARCHAR(200),
        RecordType NVARCHAR(20),
        DisplayOrder INT
    );
    
    -- Create temp table for treatments
    CREATE TABLE #TreatmentData (
        TrtID INT,
        TreatmentDetail NVARCHAR(MAX),
        TreatmentDate DATE,
        TreatmentValue DECIMAL(18,2),
        RecordType NVARCHAR(20),
        DisplayOrder INT
    );
    
    -- Create temp table for payments
    CREATE TABLE #PaymentData (
        PayID INT,
        PayDate DATE,
        PayValue DECIMAL(18,2),
        PaymentNotes NVARCHAR(MAX),
        RecordType NVARCHAR(20),
        DisplayOrder INT
    );
    
    -- Create temp table for summary
    CREATE TABLE #SummaryData (
        TotalTreatments DECIMAL(18,2),
        TotalPayments DECIMAL(18,2),
        Balance DECIMAL(18,2),
        RecordType NVARCHAR(20),
        DisplayOrder INT
    );
    
    -- Insert patient data
    INSERT INTO #PatientData
    SELECT 
        p.PatientID, 
        p.PatientName, 
        p.Sex, 
        p.Age, 
        p.Phone, 
        p.Address,
        'PATIENT' AS RecordType,
        1 AS DisplayOrder
    FROM 
        dbo.Patient p
    WHERE 
        p.PatientID = @PatientID;
    
    -- Insert treatments
    INSERT INTO #TreatmentData
    SELECT 
        pt.TrtID,
        pt.Detail AS TreatmentDetail,
        pt.TrtDate AS TreatmentDate,
        ISNULL(pt.TrtValue, 0) AS TreatmentValue,
        'TREATMENT' AS RecordType,
        2 AS DisplayOrder
    FROM 
        dbo.Patient_Trts pt
    WHERE 
        pt.PatientID = @PatientID;
    
    -- Insert payments
    INSERT INTO #PaymentData
    SELECT 
        pp.PayID,
        pp.PayDate,
        ISNULL(pp.PayValue, 0) AS PayValue,
        pp.Notes AS PaymentNotes,
        'PAYMENT' AS RecordType,
        3 AS DisplayOrder
    FROM 
        dbo.Patient_Pays pp
    INNER JOIN 
        dbo.Patient_Trts pt ON pp.TrtID = pt.TrtID
    WHERE 
        pt.PatientID = @PatientID;
    
    -- Calculate and insert summary
    INSERT INTO #SummaryData
    SELECT 
        SUM(ISNULL(pt.TrtValue, 0)) AS TotalTreatments,
        SUM(ISNULL(pp.PayValue, 0)) AS TotalPayments,
        CASE 
            WHEN SUM(ISNULL(pt.TrtValue, 0)) > SUM(ISNULL(pp.PayValue, 0))
            THEN -(SUM(ISNULL(pt.TrtValue, 0)) - SUM(ISNULL(pp.PayValue, 0)))
            ELSE SUM(ISNULL(pp.PayValue, 0)) - SUM(ISNULL(pt.TrtValue, 0))
        END AS Balance,
        'SUMMARY' AS RecordType,
        4 AS DisplayOrder
    FROM 
        dbo.Patient p
    LEFT JOIN 
        dbo.Patient_Trts pt ON p.PatientID = pt.PatientID
    LEFT JOIN 
        dbo.Patient_Pays pp ON pt.TrtID = pp.TrtID
    WHERE 
        p.PatientID = @PatientID;
    
    -- Return unified result set
    SELECT * FROM (
        -- Patient details
        SELECT 
            PatientID AS ID,
            PatientName AS Description,
            Sex,
            Age,
            Phone,
            Address,
            NULL AS Date,
            NULL AS Amount,
            NULL AS Notes,
            RecordType,
            DisplayOrder
        FROM #PatientData
        
        UNION ALL
        
        -- Treatments
        SELECT 
            TrtID AS ID,
            TreatmentDetail AS Description,
            NULL AS Sex,
            NULL AS Age,
            NULL AS Phone,
            NULL AS Address,
            TreatmentDate AS Date,
            TreatmentValue AS Amount,
            NULL AS Notes,
            RecordType,
            DisplayOrder
        FROM #TreatmentData
        
        UNION ALL
        
        -- Payments
        SELECT 
            PayID AS ID,
            'Payment' AS Description,
            NULL AS Sex,
            NULL AS Age,
            NULL AS Phone,
            NULL AS Address,
            PayDate AS Date,
            PayValue AS Amount,
            PaymentNotes AS Notes,
            RecordType,
            DisplayOrder
        FROM #PaymentData
        
        UNION ALL
        
        -- Summary
        SELECT 
            NULL AS ID,
            'Financial Summary' AS Description,
            NULL AS Sex,
            NULL AS Age,
            NULL AS Phone,
            NULL AS Address,
            NULL AS Date,
            Balance AS Amount,
            CONCAT('Treatments: ', TotalTreatments, ' | Payments: ', TotalPayments) AS Notes,
            RecordType,
            DisplayOrder
        FROM #SummaryData
    ) AS CombinedData
    ORDER BY DisplayOrder, Date DESC;
    
    -- Clean up
    DROP TABLE #PatientData;
    DROP TABLE #TreatmentData;
    DROP TABLE #PaymentData;
    DROP TABLE #SummaryData;
END
GO
/****** Object:  StoredProcedure [dbo].[GetQrtr2YrEXP_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQrtr2YrEXP_Proc]
(
    @M INT,
    @Yr1 INT,
    @Yr2 INT
)
AS
BEGIN
    -- Create temporary table for results
    CREATE TABLE #Results (
        MNo INT,
        MName NVARCHAR(50),
        ExpTot MONEY,
        ExpPay MONEY,
        ExpRem MONEY
    )

    DECLARE @i INT, @endMonth INT

    -- Determine quarter based on input month
    IF @M BETWEEN 1 AND 3
    BEGIN
        SET @i = 1
        SET @endMonth = 4
    END
    ELSE IF @M BETWEEN 4 AND 6
    BEGIN
        SET @i = 4
        SET @endMonth = 7
    END
    ELSE IF @M BETWEEN 7 AND 9
    BEGIN
        SET @i = 7
        SET @endMonth = 10
    END
    ELSE IF @M BETWEEN 10 AND 12
    BEGIN
        SET @i = 10
        SET @endMonth = 13
    END
    ELSE
    BEGIN
        -- Invalid month input - return empty result
        SELECT * FROM #Results
        RETURN
    END

    -- Process each month in the quarter
    WHILE (@i < @endMonth)
    BEGIN
        -- Calculate expense metrics
        DECLARE @ExpTot MONEY, @ExpPay MONEY
        
        SELECT @ExpTot = ISNULL(SUM([MasrofAmount]), 0)
        FROM [TblMasareef] 
        WHERE MONTH([MasrofDate]) = @i AND YEAR([MasrofDate]) BETWEEN @Yr1 AND @Yr2
        
        SELECT @ExpPay = ISNULL(SUM(PayValue), 0)
        FROM [TblExpenPay] 
        WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2

        -- Insert results for current month
        INSERT INTO #Results
        SELECT 
            @i,
            DATENAME(MONTH, DATEADD(MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            @ExpTot,
            @ExpPay,
            @ExpPay - @ExpTot

        SET @i = @i + 1
    END

    -- Return results ordered by month
    SELECT * FROM #Results
    ORDER BY MNo
    
    -- Clean up
    DROP TABLE #Results
END
GO
/****** Object:  StoredProcedure [dbo].[GetQrtr2YrTrt_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQrtr2YrTrt_Proc]
(
    @M INT,
    @Yr1 INT,
    @Yr2 INT
)
AS
BEGIN
    -- Create temporary table for results
    CREATE TABLE #Results (
        MNo INT,
        MName NVARCHAR(50),
        TrtTot MONEY,
        TrtPay MONEY,
        TrtRem MONEY
    )

    DECLARE @i INT, @endMonth INT

    -- Determine quarter based on input month
    IF @M BETWEEN 1 AND 3
    BEGIN
        SET @i = 1
        SET @endMonth = 4
    END
    ELSE IF @M BETWEEN 4 AND 6
    BEGIN
        SET @i = 4
        SET @endMonth = 7
    END
    ELSE IF @M BETWEEN 7 AND 9
    BEGIN
        SET @i = 7
        SET @endMonth = 10
    END
    ELSE IF @M BETWEEN 10 AND 12
    BEGIN
        SET @i = 10
        SET @endMonth = 13
    END
    ELSE
    BEGIN
        -- Invalid month input - return empty result
        SELECT * FROM #Results
        RETURN
    END

    -- Process each month in the quarter
    WHILE (@i < @endMonth)
    BEGIN
        -- Calculate treatment metrics
        DECLARE @TrtTot MONEY, @TrtPay MONEY
        
        SELECT @TrtTot = ISNULL(SUM(trtValue), 0) 
        FROM [Patient_Trts] 
        WHERE MONTH([TrtDate]) = @i AND YEAR([TrtDate]) BETWEEN @Yr1 AND @Yr2
        
        SELECT @TrtPay = ISNULL(SUM(PayValue), 0) 
        FROM [Patient_Pays] 
        WHERE MONTH([PayDate]) = @i AND YEAR([PayDate]) BETWEEN @Yr1 AND @Yr2

        -- Insert results for current month
        INSERT INTO #Results
        SELECT 
            @i,
            DATENAME(MONTH, DATEADD(MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            @TrtTot,
            @TrtPay,
            @TrtPay - @TrtTot

        SET @i = @i + 1
    END

    -- Return results ordered by month
    SELECT * FROM #Results
    ORDER BY MNo
    
    -- Clean up
    DROP TABLE #Results
END
GO
/****** Object:  StoredProcedure [dbo].[GetQrtrYrTrt_Proc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQrtrYrTrt_Proc]
(
    @M INT,
    @Yr INT
)
AS
BEGIN
    -- Create temporary table to hold results
    CREATE TABLE #Results (
        MNo INT,
        MName NVARCHAR(50),
        TrtTot MONEY,
        TrtPay MONEY,
        TrtRem MONEY
    )

    DECLARE @i INT, @endMonth INT

    -- Determine quarter start month based on input month
    IF @M BETWEEN 1 AND 3 SET @i = 1
    ELSE IF @M BETWEEN 4 AND 6 SET @i = 4
    ELSE IF @M BETWEEN 7 AND 9 SET @i = 7
    ELSE IF @M BETWEEN 10 AND 12 SET @i = 10
    ELSE 
    BEGIN
        -- Return empty result for invalid month input
        SELECT * FROM #Results
        RETURN
    END

    SET @endMonth = @i + 3  -- Set upper limit (non-inclusive in WHILE)

    -- Process each month in the quarter
    WHILE (@i < @endMonth)
    BEGIN
        -- Calculate treatment metrics for current month
        DECLARE 
            @TrtTot MONEY = ISNULL((SELECT SUM(trtValue) FROM Patient_Trts WHERE MONTH(TrtDate) = @i AND YEAR(TrtDate) = @Yr), 0),
            @TrtPay MONEY = ISNULL((SELECT SUM(PayValue) FROM Patient_Pays WHERE MONTH(PayDate) = @i AND YEAR(PayDate) = @Yr), 0)

        -- Insert results for current month
        INSERT INTO #Results
        SELECT
            @i,
            DATENAME(MONTH, DATEADD(MONTH, @i - 1, 0)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8,
            @TrtTot,
            @TrtPay,
            @TrtPay - @TrtTot

        SET @i += 1
    END

    -- Return the results ordered by month number
    SELECT * FROM #Results
    ORDER BY MNo
    
    -- Clean up
    DROP TABLE #Results
END
GO
/****** Object:  StoredProcedure [dbo].[GetSupplierPurchaseSummary]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSupplierPurchaseSummary]
    @SupplierID INT
AS
BEGIN
    SELECT 
        s.SupplierName,
        COUNT(bi.InvoiceID) AS TotalOrders,
        SUM(bi.TotalAmount) AS TotalSpent,
        AVG(DATEDIFF(DAY, bi.InvoiceDate, bi.DueDate)) AS AvgPaymentTerms
    FROM BuyInvoices bi
    JOIN Suppliers s ON bi.SupplierID = s.SupplierID
    WHERE bi.SupplierID = @SupplierID
    GROUP BY s.SupplierName;
END;
GO
/****** Object:  StoredProcedure [dbo].[HealthDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[HealthDelete] 
    @HID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Health]
	WHERE  [HID] = @HID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[HealthInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[HealthInsert] 
    @HealthStat nvarchar(100)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Health] ([HealthStat])
	SELECT @HealthStat
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[HealthSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[HealthSelect] 
    @HID int
AS 
	
	 

	BEGIN TRAN

	SELECT [HID], [HealthStat] 
	FROM   [dbo].[Health] 
	WHERE  ([HID] = @HID OR @HID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[HealthUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[HealthUpdate] 
    @HID int,
    @HealthStat nvarchar(100)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Health]
	SET    [HealthStat] = @HealthStat
	WHERE  [HID] = @HID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImagsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImagsDelete] 
    @ImageID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Imags]
	WHERE  [ImageID] = @ImageID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImagsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImagsInsert] 
    @IMG varbinary(MAX) = NULL,
    @Height float = NULL,
    @Width float = NULL,
    @Sze float = NULL,
    @DatePictureTaken datetime = NULL,
    @EquipmentMaker varchar(150) = NULL,
    @EquipmentModel varchar(150) = NULL,
    @Thumbnail varbinary(MAX) = NULL,
    @DateCreated datetime = NULL,
    @DateModified datetime = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Imags] ([IMG], [Height], [Width], [Sze], [DatePictureTaken], [EquipmentMaker], [EquipmentModel], [Thumbnail], [DateCreated], [DateModified])
	SELECT @IMG, @Height, @Width, @Sze, @DatePictureTaken, @EquipmentMaker, @EquipmentModel, @Thumbnail, @DateCreated, @DateModified
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImagsSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImagsSelect] 
    @ImageID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ImageID], [IMG], [Height], [Width], [Sze], [DatePictureTaken], [EquipmentMaker], [EquipmentModel], [Thumbnail], [DateCreated], [DateModified] 
	FROM   [dbo].[Imags] 
	WHERE  ([ImageID] = @ImageID OR @ImageID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImagsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImagsUpdate] 
    @ImageID int,
    @IMG varbinary(MAX) = NULL,
    @Height float = NULL,
    @Width float = NULL,
    @Sze float = NULL,
    @DatePictureTaken datetime = NULL,
    @EquipmentMaker varchar(150) = NULL,
    @EquipmentModel varchar(150) = NULL,
    @Thumbnail varbinary(MAX) = NULL,
    @DateCreated datetime = NULL,
    @DateModified datetime = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Imags]
	SET    [IMG] = @IMG, [Height] = @Height, [Width] = @Width, [Sze] = @Sze, [DatePictureTaken] = @DatePictureTaken, [EquipmentMaker] = @EquipmentMaker, [EquipmentModel] = @EquipmentModel, [Thumbnail] = @Thumbnail, [DateCreated] = @DateCreated, [DateModified] = @DateModified
	WHERE  [ImageID] = @ImageID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImpClrsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImpClrsDelete] 
    @ImpClrID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[ImpClrs]
	WHERE  [ImpClrID] = @ImpClrID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImpClrsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImpClrsInsert] 
    @ImpClr nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ImpClrs] ([ImpClr])
	SELECT @ImpClr
	 
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImpClrsSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImpClrsSelect] 
    @ImpClrID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ImpClrID], [ImpClr] 
	FROM   [dbo].[ImpClrs] 
	WHERE  ([ImpClrID] = @ImpClrID OR @ImpClrID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImpClrsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImpClrsUpdate] 
    @ImpClrID int,
    @ImpClr nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[ImpClrs]
	SET    [ImpClr] = @ImpClr
	WHERE  [ImpClrID] = @ImpClrID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImprDetDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImprDetDelete] 
    @ImpDetID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[ImprDet]
	WHERE  [ImpDetID] = @ImpDetID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImprDetInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImprDetInsert] 
    @imprID int,
    @ImprDetail nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ImprDet] ([imprID], [ImprDetail])
	SELECT @imprID, @ImprDetail
	    
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImprDetSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImprDetSelect] 
    @ImpDetID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ImpDetID], [imprID], [ImprDetail] 
	FROM   [dbo].[ImprDet] 
	WHERE  ([ImpDetID] = @ImpDetID OR @ImpDetID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImprDetUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImprDetUpdate] 
    @ImpDetID int,
    @imprID int,
    @ImprDetail nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[ImprDet]
	SET    [imprID] = @imprID, [ImprDetail] = @ImprDetail
	WHERE  [ImpDetID] = @ImpDetID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImpressionDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImpressionDelete] 
    @ImprID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Impression]
	WHERE  [ImprID] = @ImprID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImpressionInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImpressionInsert] 
    @ImprType nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Impression] ([ImprType])
	SELECT @ImprType
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImpressionSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImpressionSelect] 
    @ImprID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ImprID], [ImprType] 
	FROM   [dbo].[Impression] 
	WHERE  ([ImprID] = @ImprID OR @ImprID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[ImpressionUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ImpressionUpdate] 
    @ImprID int,
    @ImprType nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Impression]
	SET    [ImprType] = @ImprType
	WHERE  [ImprID] = @ImprID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Insert_Appointments]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Appointments] (
@Type int, @StartDate datetime, @EndDate datetime, @QueryStartDate datetime, @QueryEndDate datetime, @AllDay bit, @Subject nvarchar, @Location nvarchar, @Description nvarchar, @Status int, @Label int, @ResourceID int, @ResourceIDs nvarchar, @ReminderInfo nvarchar, @RecurrenceInfo nvarchar, @TimeZoneId nvarchar, @CustomField1 nvarchar, @PatientID int
) AS BEGIN
INSERT INTO Appointments (Type, StartDate, EndDate, QueryStartDate, QueryEndDate, AllDay, Subject, Location, Description, Status, Label, ResourceID, ResourceIDs, ReminderInfo, RecurrenceInfo, TimeZoneId, CustomField1, PatientID)
VALUES (@Type, @StartDate, @EndDate, @QueryStartDate, @QueryEndDate, @AllDay, @Subject, @Location, @Description, @Status, @Label, @ResourceID, @ResourceIDs, @ReminderInfo, @RecurrenceInfo, @TimeZoneId, @CustomField1, @PatientID)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_DrWork]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_DrWork] (
@DrID int, @PatientID int, @WrkDate datetime, @WrkDetail nvarchar, @WrkVal money, @PayVal money, @Imp bit, @Orth bit, @Surg bit, @Notes nvarchar
) AS BEGIN
INSERT INTO DrWork (DrID, PatientID, WrkDate, WrkDetail, WrkVal, PayVal, Imp, Orth, Surg, Notes)
VALUES (@DrID, @PatientID, @WrkDate, @WrkDetail, @WrkVal, @PayVal, @Imp, @Orth, @Surg, @Notes)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_DrWorkPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_DrWorkPay] (
@WorkID int, @DrID int, @PayValue money, @PayDate datetime, @Notes nvarchar
) AS BEGIN
INSERT INTO DrWorkPay (WorkID, DrID, PayValue, PayDate, Notes)
VALUES (@WorkID, @DrID, @PayValue, @PayDate, @Notes)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Emp]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Emp] (
@EmpName nvarchar, @EmpPhone nchar, @EmpAddress nvarchar, @EmpImg image
) AS BEGIN
INSERT INTO Emp (EmpName, EmpPhone, EmpAddress, EmpImg)
VALUES (@EmpName, @EmpPhone, @EmpAddress, @EmpImg)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_EmpAtend]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_EmpAtend] (
@EmpID int, @AtnDay datetime, @AtnNote nvarchar, @AbsPrsnt bit
) AS BEGIN
INSERT INTO EmpAtend (EmpID, AtnDay, AtnNote, AbsPrsnt)
VALUES (@EmpID, @AtnDay, @AtnNote, @AbsPrsnt)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_EmpPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_EmpPay] (
@EmpID int, @MonthPay int, @DayPay int, @FromDT datetime, @ToDT datetime, @DaysCount int, @PayDate datetime, @PayNote nvarchar
) AS BEGIN
INSERT INTO EmpPay (EmpID, MonthPay, DayPay, FromDT, ToDT, DaysCount, PayDate, PayNote)
VALUES (@EmpID, @MonthPay, @DayPay, @FromDT, @ToDT, @DaysCount, @PayDate, @PayNote)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Gender]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Gender] (
@Sex nvarchar
) AS BEGIN
INSERT INTO Gender (Sex)
VALUES (@Sex)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Health]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Health] (
@HealthStat nvarchar
) AS BEGIN
INSERT INTO Health (HealthStat)
VALUES (@HealthStat)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Imags]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Imags] (
@IMG varbinary, @Height float, @Width float, @Sze float, @DatePictureTaken datetime, @EquipmentMaker varchar, @EquipmentModel varchar, @Thumbnail varbinary, @DateCreated datetime, @DateModified datetime
) AS BEGIN
INSERT INTO Imags (IMG, Height, Width, Sze, DatePictureTaken, EquipmentMaker, EquipmentModel, Thumbnail, DateCreated, DateModified)
VALUES (@IMG, @Height, @Width, @Sze, @DatePictureTaken, @EquipmentMaker, @EquipmentModel, @Thumbnail, @DateCreated, @DateModified)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_ImpClrs]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_ImpClrs] (
@ImpClr nvarchar
) AS BEGIN
INSERT INTO ImpClrs (ImpClr)
VALUES (@ImpClr)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_ImprDet]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_ImprDet] (
@imprID int, @ImprDetail nvarchar
) AS BEGIN
INSERT INTO ImprDet (imprID, ImprDetail)
VALUES (@imprID, @ImprDetail)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Impression]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Impression] (
@ImprType nvarchar
) AS BEGIN
INSERT INTO Impression (ImprType)
VALUES (@ImprType)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Lab]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Lab] (
@LabName nvarchar, @Adres nvarchar, @Phone nchar, @Mobile nchar
) AS BEGIN
INSERT INTO Lab (LabName, Adres, Phone, Mobile)
VALUES (@LabName, @Adres, @Phone, @Mobile)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_LabOrder]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_LabOrder] (
@LabID int, @PatientID int, @ImprType nvarchar, @ImprDet nvarchar, @ImprClr nvarchar, @ImprCount int, @DeliveryDate datetime, @Price int, @RecieveDate datetime, @Notes nvarchar
) AS BEGIN
INSERT INTO LabOrder (LabID, PatientID, ImprType, ImprDet, ImprClr, ImprCount, DeliveryDate, Price, RecieveDate, Notes)
VALUES (@LabID, @PatientID, @ImprType, @ImprDet, @ImprClr, @ImprCount, @DeliveryDate, @Price, @RecieveDate, @Notes)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_LabPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_LabPay] (
@LabID int, @LabOrderID int, @PayValue int, @PayDate datetime, @PayDetail nvarchar, @Notes nvarchar
) AS BEGIN
INSERT INTO LabPay (LabID, LabOrderID, PayValue, PayDate, PayDetail, Notes)
VALUES (@LabID, @LabOrderID, @PayValue, @PayDate, @PayDetail, @Notes)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_LD]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_LD] (
@PatientID int, @LD1 nvarchar(50), @LD2 nvarchar(50), @LD3 nvarchar(50), @LD4 nvarchar(50), @LD5 nvarchar(50), @LD6 nvarchar(50), @LD7 nvarchar(50), @LD8 nvarchar(50)
) AS BEGIN
INSERT INTO LD (PatientID, LD1, LD2, LD3, LD4, LD5, LD6, LD7, LD8)
VALUES (@PatientID, @LD1, @LD2, @LD3, @LD4, @LD5, @LD6, @LD7, @LD8)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_LDPL]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_LDPL] (
@PatientID int, @CellAddres int, @ForeColor nvarchar
) AS BEGIN
INSERT INTO LDPL (PatientID, CellAddres, ForeColor)
VALUES (@PatientID, @CellAddres, @ForeColor)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_LDSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_LDSTYLE] (
@PatientID int, @CellAddres int, @BakImg image
) AS BEGIN
INSERT INTO LDSTYLE (PatientID, CellAddres, BakImg)
VALUES (@PatientID, @CellAddres, @BakImg)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_LU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_LU] (
@PatientID int, @LU1 nvarchar, @LU2 nvarchar, @LU3 nvarchar, @LU4 nvarchar, @LU5 nvarchar, @LU6 nvarchar, @LU7 nvarchar, @LU8 nvarchar
) AS BEGIN
INSERT INTO LU (PatientID, LU1, LU2, LU3, LU4, LU5, LU6, LU7, LU8)
VALUES (@PatientID, @LU1, @LU2, @LU3, @LU4, @LU5, @LU6, @LU7, @LU8)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_LUPL]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_LUPL] (
@PatientID int, @CellAddres int, @ForeColor nvarchar
) AS BEGIN
INSERT INTO LUPL (PatientID, CellAddres, ForeColor)
VALUES (@PatientID, @CellAddres, @ForeColor)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_LUSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_LUSTYLE] (
@PatientID int, @CellAddres int, @BakImg image
) AS BEGIN
INSERT INTO LUSTYLE (PatientID, CellAddres, BakImg)
VALUES (@PatientID, @CellAddres, @BakImg)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_M_TRT]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_M_TRT] (
@MNo int, @MName nvarchar, @MTrt money, @MPay money, @MRemain money
) AS BEGIN
INSERT INTO M_TRT (MNo, MName, MTrt, MPay, MRemain)
VALUES (@MNo, @MName, @MTrt, @MPay, @MRemain)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_MedicineDoze]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_MedicineDoze] (
@ShapeID int, @Doze nvarchar
) AS BEGIN
INSERT INTO MedicineDoze (ShapeID, Doze)
VALUES (@ShapeID, @Doze)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_MedicineFamily]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_MedicineFamily] (
@MedicineID int, @MedicineSubCat nvarchar
) AS BEGIN
INSERT INTO MedicineFamily (MedicineID, MedicineSubCat)
VALUES (@MedicineID, @MedicineSubCat)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_MedicineGroups]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_MedicineGroups] (
@MedicineFamily nvarchar
) AS BEGIN
INSERT INTO MedicineGroups (MedicineFamily)
VALUES (@MedicineFamily)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_MedicineItems]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_MedicineItems] (
@ScincID int, @CommName nvarchar, @Company nvarchar, @Notes nvarchar
) AS BEGIN
INSERT INTO MedicineItems (ScincID, CommName, Company, Notes)
VALUES (@ScincID, @CommName, @Company, @Notes)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_MedicineShape]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_MedicineShape] (
@MedicineItemID int, @MedicineShape nvarchar, @ShapeInfo nvarchar
) AS BEGIN
INSERT INTO MedicineShape (MedicineItemID, MedicineShape, ShapeInfo)
VALUES (@MedicineItemID, @MedicineShape, @ShapeInfo)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_MedScienceFamily]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_MedScienceFamily] (
@SubCatID int, @ScienceName nvarchar
) AS BEGIN
INSERT INTO MedScienceFamily (SubCatID, ScienceName)
VALUES (@SubCatID, @ScienceName)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_OrthoDiag]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_OrthoDiag] (
@PatientID int, @CloseType nvarchar, @ClassI nvarchar, @Bite nvarchar
) AS BEGIN
INSERT INTO OrthoDiag (PatientID, CloseType, ClassI, Bite)
VALUES (@PatientID, @CloseType, @ClassI, @Bite)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_OrthoInf]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_OrthoInf] (
@PatientID int, @Compliants nvarchar, @Birth nvarchar, @Feed nvarchar, @MilkTeethChng nvarchar, @MilkTeethAppear nvarchar, @TeethLoss nvarchar, @BurriedTeeth nvarchar, @OverLoadTeeth nvarchar, @LipsCut nvarchar, @ThroatCut nvarchar, @IllnesPeriod nvarchar, @CousinsHFactor nvarchar, @BadHabits nvarchar, @Malfunction nvarchar, @Khota nvarchar, @PrevOrth nvarchar, @PrevIll nvarchar
) AS BEGIN
INSERT INTO OrthoInf (PatientID, Compliants, Birth, Feed, MilkTeethChng, MilkTeethAppear, TeethLoss, BurriedTeeth, OverLoadTeeth, LipsCut, ThroatCut, IllnesPeriod, CousinsHFactor, BadHabits, Malfunction, Khota, PrevOrth, PrevIll)
VALUES (@PatientID, @Compliants, @Birth, @Feed, @MilkTeethChng, @MilkTeethAppear, @TeethLoss, @BurriedTeeth, @OverLoadTeeth, @LipsCut, @ThroatCut, @IllnesPeriod, @CousinsHFactor, @BadHabits, @Malfunction, @Khota, @PrevOrth, @PrevIll)
END
GO
/****** Object:  StoredProcedure [dbo].[insert_OrthoTreat]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[insert_OrthoTreat]
	(
	 @PatientID_2 	[int],
	 @BeginDate_3 	[smalldatetime],
	 @OrthoType_4 	[nvarchar](50),
	 @ExtraUL_5 	[nvarchar](50),
	 @ExtraLL_6 	[nvarchar](50),
	 @ExtraUR_7 	[nvarchar](50),
	 @ExtraLR_8 	[nvarchar](50),
	 --@FixerDate_9 	[smalldatetime],
	 @FixerType_10 	[nvarchar](50),
	 @BraketType_11 	[nvarchar](50)
	-- @FinishDate_12 	[smalldatetime]
			)

AS INSERT INTO .[dbo].[OrthoTreat] 
	 (
	 [PatientID],
	 [BeginDate],
	 [OrthoType],
	 [ExtraUL],
	 [ExtraLL],
	 [ExtraUR],
	 [ExtraLR],
	-- [FixerDate],
	 [FixerType],
	 [BraketType]
	-- [FinishDate]
			) 
 
VALUES 
	(
	 @PatientID_2,
	 @BeginDate_3,
	 @OrthoType_4,
	 @ExtraUL_5,
	 @ExtraLL_6,
	 @ExtraUR_7,
	 @ExtraLR_8,
	-- @FixerDate_9,
	 @FixerType_10,
	 @BraketType_11
	-- @FinishDate_12
			)

GO
/****** Object:  StoredProcedure [dbo].[Insert_OrthoTrtDet]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_OrthoTrtDet] (
@PatientID int, @WorkDate smalldatetime, @WireMeasure nvarchar, @WireType nvarchar, @WireImg nvarchar
) AS BEGIN
INSERT INTO OrthoTrtDet (PatientID, WorkDate, WireMeasure, WireType, WireImg)
VALUES (@PatientID, @WorkDate, @WireMeasure, @WireType, @WireImg)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_OutDr]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_OutDr] (
@DrName nvarchar, @DrAdres nvarchar, @Drphone char, @DrMobile char
) AS BEGIN
INSERT INTO OutDr (DrName, DrAdres, Drphone, DrMobile)
VALUES (@DrName, @DrAdres, @Drphone, @DrMobile)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Patient]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Patient] (
@PatientName nvarchar, @Sex nvarchar, @Age int, @Phone nvarchar, @Address nvarchar, @Health nvarchar, @Treat bit, @Implant bit, @Mobile bit, @Ortho bit, @Struc bit, @Notes nvarchar, @BirthY int, @CreatedBy int, @CreateDate datetime
) AS BEGIN
INSERT INTO Patient (PatientName, Sex, Age, Phone, Address, Health, Treat, Implant, Mobile, Ortho, Struc, Notes, BirthY, CreatedBy, CreateDate)
VALUES (@PatientName, @Sex, @Age, @Phone, @Address, @Health, @Treat, @Implant, @Mobile, @Ortho, @Struc, @Notes, @BirthY, @CreatedBy, @CreateDate)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Patient_Chart]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Patient_Chart] (
@PatientID int, @ToothNum tinyint, @ToothName nvarchar, @IsExist bit, @HasImp bit, @ImpType nvarchar, @ImpName nvarchar, @ImpDate datetime, @CrownType nvarchar, @CrownName nvarchar, @CrownDate datetime, @TrtDate datetime, @TrtName nvarchar, @ClassID tinyint, @ClassName nvarchar, @TrtStage nvarchar, @TrtDetails nvarchar, @TrtNotes nvarchar
) AS BEGIN
INSERT INTO Patient_Chart (PatientID, ToothNum, ToothName, IsExist, HasImp, ImpType, ImpName, ImpDate, CrownType, CrownName, CrownDate, TrtDate, TrtName, ClassID, ClassName, TrtStage, TrtDetails, TrtNotes)
VALUES (@PatientID, @ToothNum, @ToothName, @IsExist, @HasImp, @ImpType, @ImpName, @ImpDate, @CrownType, @CrownName, @CrownDate, @TrtDate, @TrtName, @ClassID, @ClassName, @TrtStage, @TrtDetails, @TrtNotes)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Patient_ChartCheck]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Patient_ChartCheck] (
@ToothNum tinyint, @PatientID int, @ToothName nvarchar, @IsExist tinyint, @CL1 tinyint, @CL2 tinyint, @CL3 tinyint, @CL4 tinyint, @CL5 tinyint, @CL6 tinyint, @CL7 tinyint, @CL8 tinyint, @CL9 tinyint, @CL10 tinyint, @CL11 tinyint, @CL12 tinyint, @CL13 tinyint, @CL14 tinyint, @CheckDate datetime, @CheckNotes nvarchar
) AS BEGIN
INSERT INTO Patient_ChartCheck (ToothNum, PatientID, ToothName, IsExist, CL1, CL2, CL3, CL4, CL5, CL6, CL7, CL8, CL9, CL10, CL11, CL12, CL13, CL14, CheckDate, CheckNotes)
VALUES (@ToothNum, @PatientID, @ToothName, @IsExist, @CL1, @CL2, @CL3, @CL4, @CL5, @CL6, @CL7, @CL8, @CL9, @CL10, @CL11, @CL12, @CL13, @CL14, @CheckDate, @CheckNotes)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Patient_Imgs]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Patient_Imgs] (
@PatientID int, @PicName nvarchar, @PicPath nvarchar, @FullName nvarchar
) AS BEGIN
INSERT INTO Patient_Imgs (PatientID, PicName, PicPath, FullName)
VALUES (@PatientID, @PicName, @PicPath, @FullName)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Patient_MobStruc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Patient_MobStruc] (
@PatientID int, @StrucName nvarchar, @StrucType nvarchar, @TeethType nvarchar, @StrucDate smalldatetime
) AS BEGIN
INSERT INTO Patient_MobStruc (PatientID, StrucName, StrucType, TeethType, StrucDate)
VALUES (@PatientID, @StrucName, @StrucType, @TeethType, @StrucDate)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Patient_MobStrucAdd]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Patient_MobStrucAdd] (
@StrucID int, @StrucName nvarchar, @ToothLoc nvarchar, @ToothNum nvarchar, @AddTothDate smalldatetime
) AS BEGIN
INSERT INTO Patient_MobStrucAdd (StrucID, StrucName, ToothLoc, ToothNum, AddTothDate)
VALUES (@StrucID, @StrucName, @ToothLoc, @ToothNum, @AddTothDate)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Patient_Notes]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Patient_Notes] (
@PatientID int, @NoteDate smalldatetime, @Note nvarchar
) AS BEGIN
INSERT INTO Patient_Notes (PatientID, NoteDate, Note)
VALUES (@PatientID, @NoteDate, @Note)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Patient_Pays]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Patient_Pays] (
@TrtID int, @PatientID int, @PayValue money, @PayDate smalldatetime, @Notes nvarchar
) AS BEGIN
INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes)
VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Patient_RX]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Patient_RX] (
@PatientID int, @RXDate smalldatetime, @RX nvarchar
) AS BEGIN
INSERT INTO Patient_RX (PatientID, RXDate, RX)
VALUES (@PatientID, @RXDate, @RX)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Patient_ToothCheck]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Patient_ToothCheck] (
@ToothNum tinyint, @PatientID int, @ToothName nvarchar, @IsExist tinyint, @COM_1 tinyint, @COM_2M tinyint, @COM_2D tinyint, @COM_2MOD tinyint, @COM_3M tinyint, @COM_4 tinyint, @COM_5 tinyint, @COM_FACING tinyint, @COM_3D tinyint, @RCT tinyint, @RCC tinyint, @RCC_NS tinyint, @RCF tinyint, @RCF_COM tinyint, @EXTRACTION tinyint, @PULPOTOMY tinyint, @PULPECTOMY tinyint, @INDIRECT_PC tinyint, @DIRECT_PC tinyint, @FIBER_POST tinyint, @TF tinyint, @REDO_RCC tinyint, @AMALGM tinyint, @RCT_NECROTIC tinyint, @PULPOTOMY_MTA tinyint, @ABCESS_DRAINAGE tinyint, @MTA_Bulk_Flow tinyint, @Build_Up_Com tinyint, @Build_Up_Acr tinyint, @Build_Up_GI tinyint, @GI_M tinyint, @GI tinyint, @GI_D tinyint, @CheckDate datetime, @CheckNotes nvarchar
) AS BEGIN
INSERT INTO Patient_ToothCheck (ToothNum, PatientID, ToothName, IsExist, COM_1, COM_2M, COM_2D, COM_2MOD, COM_3M, COM_4, COM_5, COM_FACING, COM_3D, RCT, RCC, RCC_NS, RCF, RCF_COM, EXTRACTION, PULPOTOMY, PULPECTOMY, INDIRECT_PC, DIRECT_PC, FIBER_POST, TF, REDO_RCC, AMALGM, RCT_NECROTIC, PULPOTOMY_MTA, ABCESS_DRAINAGE, MTA_Bulk_Flow, Build_Up_Com, Build_Up_Acr, Build_Up_GI, GI_M, GI, GI_D, CheckDate, CheckNotes)
VALUES (@ToothNum, @PatientID, @ToothName, @IsExist, @COM_1, @COM_2M, @COM_2D, @COM_2MOD, @COM_3M, @COM_4, @COM_5, @COM_FACING, @COM_3D, @RCT, @RCC, @RCC_NS, @RCF, @RCF_COM, @EXTRACTION, @PULPOTOMY, @PULPECTOMY, @INDIRECT_PC, @DIRECT_PC, @FIBER_POST, @TF, @REDO_RCC, @AMALGM, @RCT_NECROTIC, @PULPOTOMY_MTA, @ABCESS_DRAINAGE, @MTA_Bulk_Flow, @Build_Up_Com, @Build_Up_Acr, @Build_Up_GI, @GI_M, @GI, @GI_D, @CheckDate, @CheckNotes)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Patient_Trts]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Patient_Trts] (
@PatientID int, @Detail nvarchar, @TrtDate smalldatetime, @TrtValue money
) AS BEGIN
INSERT INTO Patient_Trts (PatientID, Detail, TrtDate, TrtValue)
VALUES (@PatientID, @Detail, @TrtDate, @TrtValue)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_PatientColors]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_PatientColors] (
@Color1 nvarchar, @Color2 nvarchar, @GradientIndex tinyint, @AlphaValue int, @PatientID int
) AS BEGIN
INSERT INTO PatientColors (Color1, Color2, GradientIndex, AlphaValue, PatientID)
VALUES (@Color1, @Color2, @GradientIndex, @AlphaValue, @PatientID)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Raseed]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Raseed] (
@PatientID int
) AS BEGIN
INSERT INTO Raseed (PatientID)
VALUES (@PatientID)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_RD]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_RD] (
@PatientID int, @RD1 nvarchar(50), @RD2 nvarchar(50), @RD3 nvarchar(50), @RD4 nvarchar(50), @RD5 nvarchar(50), @RD6 nvarchar(50), @RD7 nvarchar(50), @RD8 nvarchar(50)
) AS BEGIN
INSERT INTO RD (PatientID, RD1, RD2, RD3, RD4, RD5, RD6, RD7, RD8)
VALUES (@PatientID, @RD1, @RD2, @RD3, @RD4, @RD5, @RD6, @RD7, @RD8)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_RDPL]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_RDPL] (
@PatientID int, @CellAddres int, @ForeColor nvarchar(50)
) AS BEGIN
INSERT INTO RDPL (PatientID, CellAddres, ForeColor)
VALUES (@PatientID, @CellAddres, @ForeColor)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_RDSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_RDSTYLE] (
@PatientID int, @CellAddres int, @BakImg image
) AS BEGIN
INSERT INTO RDSTYLE (PatientID, CellAddres, BakImg)
VALUES (@PatientID, @CellAddres, @BakImg)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Resources]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Resources] (
@ResourceID int, @ResourceName nvarchar(50), @Color int, @Image image, @CustomField1 nvarchar(MAX)
) AS BEGIN
INSERT INTO Resources (ResourceID, ResourceName, Color, Image, CustomField1)
VALUES (@ResourceID, @ResourceName, @Color, @Image, @CustomField1)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_rs]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_rs] (
@UsName nvarchar(50), @UsPass nvarchar(50), @UsLvl int, @UsGrp nvarchar(50)
) AS BEGIN
INSERT INTO rs (UsName, UsPass, UsLvl, UsGrp)
VALUES (@UsName, @UsPass, @UsLvl, @UsGrp)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_RU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_RU] (
@PatientID int, @RU1 nvarchar(50), @RU2 nvarchar(50), @RU3 nvarchar(50), @RU4 nvarchar(50), @RU5 nvarchar(50), @RU6 nvarchar(50), @RU7 nvarchar(50), @RU8 nvarchar(50)
) AS BEGIN
INSERT INTO RU (PatientID, RU1, RU2, RU3, RU4, RU5, RU6, RU7, RU8)
VALUES (@PatientID, @RU1, @RU2, @RU3, @RU4, @RU5, @RU6, @RU7, @RU8)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_RUPL]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_RUPL] (
@PatientID int, @CellAddres int, @ForeColor nvarchar(50)
) AS BEGIN
INSERT INTO RUPL (PatientID, CellAddres, ForeColor)
VALUES (@PatientID, @CellAddres, @ForeColor)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_RUSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_RUSTYLE] (
@PatientID int, @CellAddres int, @BakImg image
) AS BEGIN
INSERT INTO RUSTYLE (PatientID, CellAddres, BakImg)
VALUES (@PatientID, @CellAddres, @BakImg)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_RxBody]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_RxBody] (
@RxBdyID int, @ArHdrName nvarchar(50), @ArHdrAdres nvarchar(100), @EnHdrName nvarchar(50), @EnHdrAdres nvarchar(100), @Logo image, @Detail nvarchar(150), @ArFtr nvarchar(50), @EnFtr nvarchar(50), @WtrImg image, @WtrText nvarchar(50), @UseWtrImg bit, @UseWtrText BIT,@DrName  nvarchar(50)
) AS BEGIN
INSERT INTO RxBody (RxBdyID, ArHdrName, ArHdrAdres, EnHdrName, EnHdrAdres, Logo, Detail, ArFtr, EnFtr, WtrImg, WtrText, UseWtrImg, UseWtrText,DrName)
VALUES (@RxBdyID, @ArHdrName, @ArHdrAdres, @EnHdrName, @EnHdrAdres, @Logo, @Detail, @ArFtr, @EnFtr, @WtrImg, @WtrText, @UseWtrImg, @UseWtrText,@DrName)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_RxFly]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_RxFly] (
@PatientName nvarchar(50), @PatientAge int, @RxDate smalldatetime, @RX nvarchar(500)
) AS BEGIN
INSERT INTO RxFly (PatientName, PatientAge, RxDate, RX)
VALUES (@PatientName, @PatientAge, @RxDate, @RX)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Surgery]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Surgery] (
@PatientID int, @SurgeryDet nvarchar(50), @SurDate smalldatetime
) AS BEGIN
INSERT INTO Surgery (PatientID, SurgeryDet, SurDate)
VALUES (@PatientID, @SurgeryDet, @SurDate)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblBnodMsareef]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblBnodMsareef] (
@BandName nvarchar(50)
) AS BEGIN
INSERT INTO TblBnodMsareef (BandName)
VALUES (@BandName)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblCategories]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblCategories] (
@CategoryName nvarchar(75), @ParentCategory int
) AS BEGIN
INSERT INTO TblCategories (CategoryName, ParentCategory)
VALUES (@CategoryName, @ParentCategory)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblCities]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblCities] (
@CityName nvarchar(50)
) AS BEGIN
INSERT INTO TblCities (CityName)
VALUES (@CityName)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblCustomers]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblCustomers] (
@CusName nvarchar(75), @CityID int, @Address nvarchar(500), @Contacts nvarchar(500)
) AS BEGIN
INSERT INTO TblCustomers (CusName, CityID, Address, Contacts)
VALUES (@CusName, @CityID, @Address, @Contacts)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblExpenPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblExpenPay] (
@MasrofID int, @PayValue money, @PayDate datetime, @Notes nvarchar(50)
) AS BEGIN
INSERT INTO TblExpenPay (MasrofID, PayValue, PayDate, Notes)
VALUES (@MasrofID, @PayValue, @PayDate, @Notes)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblInvoiceBody]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblInvoiceBody] (
@InvID int, @ItemID int, @Quantity float, @Price money, @ItemHasm money, @Note nvarchar(100)
) AS BEGIN
INSERT INTO TblInvoiceBody (InvID, ItemID, Quantity, Price, ItemHasm, Note)
VALUES (@InvID, @ItemID, @Quantity, @Price, @ItemHasm, @Note)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblInvoicesHeader]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblInvoicesHeader] (
@InvoiceType tinyint, @InvoiceDate smalldatetime, @ResID int, @DocNo int, @InvoiceEx nvarchar(500), @Hasm money
) AS BEGIN
INSERT INTO TblInvoicesHeader (InvoiceType, InvoiceDate, ResID, DocNo, InvoiceEx, Hasm)
VALUES (@InvoiceType, @InvoiceDate, @ResID, @DocNo, @InvoiceEx, @Hasm)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblInvPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblInvPay] (
@InvoiceID int, @ResID int, @PayDate datetime, @Amount money, @Notes nvarchar(50)
) AS BEGIN
INSERT INTO TblInvPay (InvoiceID, ResID, PayDate, Amount, Notes)
VALUES (@InvoiceID, @ResID, @PayDate, @Amount, @Notes)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblItems]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblItems] (
@ItemName nvarchar(75), @ItemEx nvarchar(500), @CatID int, @UnitID int, @LastPrice money, @QuantityNow float
) AS BEGIN
INSERT INTO TblItems (ItemName, ItemEx, CatID, UnitID, LastPrice, QuantityNow)
VALUES (@ItemName, @ItemEx, @CatID, @UnitID, @LastPrice, @QuantityNow)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblMasareef]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblMasareef] (
@MasrofDate smalldatetime, @BandID int, @MasrofAmount money, @MasrofEx nvarchar(500)
) AS BEGIN
INSERT INTO TblMasareef (MasrofDate, BandID, MasrofAmount, MasrofEx)
VALUES (@MasrofDate, @BandID, @MasrofAmount, @MasrofEx)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblMeasure]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblMeasure] (
@Measure nvarchar(50)
) AS BEGIN
INSERT INTO TblMeasure (Measure)
VALUES (@Measure)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblPaids]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblPaids] (
@PayType nvarchar(5), @PayDate smalldatetime, @ResCusId int, @PayAmount money, @PayEx nvarchar(300)
) AS BEGIN
INSERT INTO TblPaids (PayType, PayDate, ResCusId, PayAmount, PayEx)
VALUES (@PayType, @PayDate, @ResCusId, @PayAmount, @PayEx)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblResources]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblResources] (
@ResName nvarchar(75), @CityID int, @Address nvarchar(500), @Contacts nvarchar(500)
) AS BEGIN
INSERT INTO TblResources (ResName, CityID, Address, Contacts)
VALUES (@ResName, @CityID, @Address, @Contacts)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblSalesBody]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblSalesBody] (
@SaleID int, @ItemID int, @Quantity float, @Price money, @ItemHasm money, @Note nvarchar(100)
) AS BEGIN
INSERT INTO TblSalesBody (SaleID, ItemID, Quantity, Price, ItemHasm, Note)
VALUES (@SaleID, @ItemID, @Quantity, @Price, @ItemHasm, @Note)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblSalesHeader]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblSalesHeader] (
@SaleType tinyint, @SaleDate smalldatetime, @CusID int, @DocNo int, @SaleEx nvarchar(500), @Hasm money
) AS BEGIN
INSERT INTO TblSalesHeader (SaleType, SaleDate, CusID, DocNo, SaleEx, Hasm)
VALUES (@SaleType, @SaleDate, @CusID, @DocNo, @SaleEx, @Hasm)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblTRT]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblTRT] (
@Trt nvarchar(50)
) AS BEGIN
INSERT INTO TblTRT (Trt)
VALUES (@Trt)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblUnits]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblUnits] (
@UnitName nvarchar(50)
) AS BEGIN
INSERT INTO TblUnits (UnitName)
VALUES (@UnitName)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_TblWireType]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_TblWireType] (
@WireType nvarchar(50)
) AS BEGIN
INSERT INTO TblWireType (WireType)
VALUES (@WireType)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_VisitType]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_VisitType] (
@VisType nvarchar(200)
) AS BEGIN
INSERT INTO VisitType (VisType)
VALUES (@VisType)
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_Y_TRT]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Y_TRT] (
@YName int
) AS BEGIN
INSERT INTO Y_TRT (YName)
VALUES (@YName)
END
GO
/****** Object:  StoredProcedure [dbo].[LabDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabDelete] 
    @LabID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Lab]
	WHERE  [LabID] = @LabID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabInsert] 
    @LabName nvarchar(50),
    @Adres nvarchar(50) = NULL,
    @Phone nchar(10) = NULL,
    @Mobile nchar(10) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Lab] ([LabName], [Adres], [Phone], [Mobile])
	SELECT @LabName, @Adres, @Phone, @Mobile
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabOrderDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabOrderDelete] 
    @LabOrderID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[LabOrder]
	WHERE  [LabOrderID] = @LabOrderID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabOrderInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabOrderInsert] 
    @LabID int,
    @PatientID int,
    @ImprType nvarchar(50),
    @ImprDet nvarchar(50),
    @ImprClr nvarchar(50),
    @ImprCount int,
    @DeliveryDate datetime = NULL,
    @Price int,
    @RecieveDate datetime = NULL,
    @Notes nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LabOrder] ([LabID], [PatientID], [ImprType], [ImprDet], [ImprClr], [ImprCount], [DeliveryDate], [Price], [RecieveDate], [Notes])
	SELECT @LabID, @PatientID, @ImprType, @ImprDet, @ImprClr, @ImprCount, @DeliveryDate, @Price, @RecieveDate, @Notes
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabOrderRecUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[LabOrderRecUpdate] 
    @LabOrderID int,
    @LabID int,
    @PatientID int,
    @ImprType nvarchar(50),
    @ImprDet nvarchar(50),
    @ImprClr nvarchar(50),
    @ImprCount int,
    @DeliveryDate datetime = NULL,
    @Price int,
    @RecieveDate datetime = NULL,
    @Notes nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[LabOrder]
	SET    [LabID] = @LabID, [PatientID] = @PatientID, [ImprType] = @ImprType, [ImprDet] = @ImprDet, [ImprClr] = @ImprClr, [ImprCount] = @ImprCount, [DeliveryDate] = @DeliveryDate, [Price] = @Price, [RecieveDate] = @RecieveDate, [Notes] = @Notes
	WHERE  [LabOrderID] = @LabOrderID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabOrderSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabOrderSelect] 
    @LabOrderID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LabOrderID], [LabID], [PatientID], [ImprType], [ImprDet], [ImprClr], [ImprCount], [DeliveryDate], [Price], [RecieveDate], [Notes] 
	FROM   [dbo].[LabOrder] 
	WHERE  ([LabOrderID] = @LabOrderID OR @LabOrderID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabOrderSendInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[LabOrderSendInsert] 
    @LabID int,
    @PatientID int,
    @ImprType nvarchar(50),
    @ImprDet nvarchar(50),
    @ImprClr nvarchar(50),
    @ImprCount int,
    @DeliveryDate datetime = NULL,
    @Price int,
    --@RecieveDate datetime = NULL,
    @Notes nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LabOrder] ([LabID], [PatientID], [ImprType], [ImprDet], [ImprClr], [ImprCount], [DeliveryDate], [Price],  [Notes])
	SELECT @LabID, @PatientID, @ImprType, @ImprDet, @ImprClr, @ImprCount, @DeliveryDate, @Price,@Notes --@RecieveDate, 
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabOrderUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabOrderUpdate] 
    @LabOrderID int,
    @LabID int,
    @PatientID int,
    @ImprType nvarchar(50),
    @ImprDet nvarchar(50),
    @ImprClr nvarchar(50),
    @ImprCount int,
    @DeliveryDate datetime = NULL,
    @Price int,
    @RecieveDate datetime = NULL,
    @Notes nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[LabOrder]
	SET    [LabID] = @LabID, [PatientID] = @PatientID, [ImprType] = @ImprType, [ImprDet] = @ImprDet, [ImprClr] = @ImprClr, [ImprCount] = @ImprCount, [DeliveryDate] = @DeliveryDate, [Price] = @Price, [RecieveDate] = @RecieveDate, [Notes] = @Notes
	WHERE  [LabOrderID] = @LabOrderID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabPayDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabPayDelete] 
    @LabPayID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[LabPay]
	WHERE  [LabPayID] = @LabPayID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabPayInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabPayInsert] 
    @LabID int,
    @LabOrderID int,
    @PayValue int,
    @PayDate datetime,
    @PayDetail nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LabPay] ([LabID], [LabOrderID], [PayValue], [PayDate], [PayDetail])
	SELECT @LabID, @LabOrderID, @PayValue, @PayDate, @PayDetail
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabPaySelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabPaySelect] 
    @LabPayID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LabPayID], [LabID], [LabOrderID], [PayValue], [PayDate], [PayDetail] 
	FROM   [dbo].[LabPay] 
	WHERE  ([LabPayID] = @LabPayID OR @LabPayID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabPayUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabPayUpdate] 
    @LabPayID int,
    @LabID int,
    @LabOrderID int,
    @PayValue int,
    @PayDate datetime,
    @PayDetail nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[LabPay]
	SET    [LabID] = @LabID, [LabOrderID] = @LabOrderID, [PayValue] = @PayValue, [PayDate] = @PayDate, [PayDetail] = @PayDetail
	WHERE  [LabPayID] = @LabPayID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabSelect] 
    @LabID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LabID], [LabName], [Adres], [Phone], [Mobile] 
	FROM   [dbo].[Lab] 
	WHERE  ([LabID] = @LabID OR @LabID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LabUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LabUpdate] 
    @LabID int,
    @LabName nvarchar(50),
    @Adres nvarchar(50) = NULL,
    @Phone nchar(10) = NULL,
    @Mobile nchar(10) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Lab]
	SET    [LabName] = @LabName, [Adres] = @Adres, [Phone] = @Phone, [Mobile] = @Mobile
	WHERE  [LabID] = @LabID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDDelete] 
    @LDID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[LD]
	WHERE  [LDID] = @LDID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDInsert] 
    @PatientID int,
    @LD1 nvarchar(50) = NULL,
    @LD2 nvarchar(50) = NULL,
    @LD3 nvarchar(50) = NULL,
    @LD4 nvarchar(50) = NULL,
    @LD5 nvarchar(50) = NULL,
    @LD6 nvarchar(50) = NULL,
    @LD7 nvarchar(50) = NULL,
    @LD8 nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LD] ([PatientID], [LD1], [LD2], [LD3], [LD4], [LD5], [LD6], [LD7], [LD8])
	SELECT @PatientID, @LD1, @LD2, @LD3, @LD4, @LD5, @LD6, @LD7, @LD8
	 
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDPL_IU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LDPL_IU]
  @LDcellID int,
  @PatientID int,
  @CellAddres int,
  @ForeColor nvarchar(50) AS
BEGIN
  IF EXISTS
  (SELECT * FROM dbo.LDPL WHERE LDcellID = @LDcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres )
    UPDATE dbo.LDPL SET
      PatientID = @PatientID,
      CellAddres = @CellAddres,
      ForeColor = @ForeColor
    WHERE LDcellID = @LDcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres
  ELSE
    INSERT INTO dbo.LDPL (
      --LDcellID,
      PatientID,
      CellAddres,
      ForeColor)
    VALUES(
      --@LDcellID,
      @PatientID,
      @CellAddres,
      @ForeColor)
END
GO
/****** Object:  StoredProcedure [dbo].[LDPLDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDPLDelete] 
    @LDcellID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[LDPL]
	WHERE  [LDcellID] = @LDcellID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDPLInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDPLInsert] 
    @PatientID int,
    @CellAddres int,
    @ForeColor nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LDPL] ([PatientID], [CellAddres], [ForeColor])
	SELECT @PatientID, @CellAddres, @ForeColor
	   
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDPLSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDPLSelect] 
    @LDcellID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LDcellID], [PatientID], [CellAddres], [ForeColor] 
	FROM   [dbo].[LDPL] 
	WHERE  ([LDcellID] = @LDcellID OR @LDcellID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDPLUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDPLUpdate] 
    @LDcellID int,
    @PatientID int,
    @CellAddres int,
    @ForeColor nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[LDPL]
	SET    [CellAddres] = @CellAddres, [ForeColor] = @ForeColor
	WHERE  [LDcellID] = @LDcellID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDSelect] 
    @LDID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LDID], [PatientID], [LD1], [LD2], [LD3], [LD4], [LD5], [LD6], [LD7], [LD8] 
	FROM   [dbo].[LD] 
	WHERE  ([LDID] = @LDID OR @LDID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDSTYLE_IU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LDSTYLE_IU]
  @LDcellID int,
  @PatientID int,
  @CellAddres int,
  @BakImg image AS
BEGIN
  IF EXISTS
  (SELECT * FROM dbo.LDSTYLE WHERE LDcellID = @LDcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres)
    UPDATE dbo.LDSTYLE SET
      PatientID = @PatientID,
      CellAddres = @CellAddres,
      BakImg = @BakImg
    WHERE LDcellID = @LDcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres
  ELSE
    INSERT INTO dbo.LDSTYLE (
      --LDcellID,
      PatientID,
      CellAddres,
      BakImg)
    VALUES(
     -- @LDcellID,
      @PatientID,
      @CellAddres,
      @BakImg)
END
GO
/****** Object:  StoredProcedure [dbo].[LDSTYLEDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDSTYLEDelete] 
    @LDcellID  [int],
	 @PatientID 	[int],
	 @CellAdres [int]
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[LDSTYLE]
	WHERE  [LDcellID] = @LDcellID AND [PatientID] = @PatientID AND CellAddres=@CellAdres

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDSTYLEInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDSTYLEInsert] 
    @PatientID int,
    @CellAddres int,
    @BakImg image
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LDSTYLE] ([PatientID], [CellAddres], [BakImg])
	SELECT @PatientID, @CellAddres, @BakImg
	 
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDSTYLESelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDSTYLESelect] 
    @LDcellID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LDcellID], [PatientID], [CellAddres], [BakImg] 
	FROM   [dbo].[LDSTYLE] 
	WHERE  ([LDcellID] = @LDcellID OR @LDcellID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDSTYLEUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDSTYLEUpdate] 
    @LDcellID int,
    @PatientID int,
    @CellAddres int,
    @BakImg image
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[LDSTYLE]
	SET    [PatientID] = @PatientID, [CellAddres] = @CellAddres, [BakImg] = @BakImg
	WHERE  [LDcellID] = @LDcellID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LDUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LDUpdate] 
    @LDID int,
    @PatientID int,
    @LD1 nvarchar(50) = NULL,
    @LD2 nvarchar(50) = NULL,
    @LD3 nvarchar(50) = NULL,
    @LD4 nvarchar(50) = NULL,
    @LD5 nvarchar(50) = NULL,
    @LD6 nvarchar(50) = NULL,
    @LD7 nvarchar(50) = NULL,
    @LD8 nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[LD]
	SET    [LD1] = @LD1, [LD2] = @LD2, [LD3] = @LD3, [LD4] = @LD4, [LD5] = @LD5, [LD6] = @LD6, [LD7] = @LD7, [LD8] = @LD8
	WHERE  [LDID] = @LDID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Llect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Llect] 
    @LUID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LUID], [PatientID], [LU1], [LU2], [LU3], [LU4], [LU5], [LU6], [LU7], [LU8] 
	FROM   [dbo].[LU] 
	WHERE  ([LUID] = @LUID OR @LUID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LUDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LUDelete] 
    @LUID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[LU]
	WHERE  [LUID] = @LUID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LUInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LUInsert] 
    @PatientID int,
    @LU1 nvarchar(50) = NULL,
    @LU2 nvarchar(50) = NULL,
    @LU3 nvarchar(50) = NULL,
    @LU4 nvarchar(50) = NULL,
    @LU5 nvarchar(50) = NULL,
    @LU6 nvarchar(50) = NULL,
    @LU7 nvarchar(50) = NULL,
    @LU8 nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LU] ([PatientID], [LU1], [LU2], [LU3], [LU4], [LU5], [LU6], [LU7], [LU8])
	SELECT @PatientID, @LU1, @LU2, @LU3, @LU4, @LU5, @LU6, @LU7, @LU8
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LUPL_IU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LUPL_IU]
  @LUcellID int,
  @PatientID int,
  @CellAddres int,
  @ForeColor nvarchar(50) AS
BEGIN
  IF EXISTS
  (SELECT * FROM dbo.LUPL WHERE LUcellID = @LUcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres)
    UPDATE dbo.LUPL SET
      PatientID = @PatientID,
      CellAddres = @CellAddres,
      ForeColor = @ForeColor
    WHERE LUcellID = @LUcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres
  ELSE
    INSERT INTO dbo.LUPL (
      --LUcellID,
      PatientID,
      CellAddres,
      ForeColor)
    VALUES(
      --@LUcellID,
      @PatientID,
      @CellAddres,
      @ForeColor)
END
GO
/****** Object:  StoredProcedure [dbo].[LUPLDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LUPLDelete] 
    @LUcellID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[LUPL]
	WHERE  [LUcellID] = @LUcellID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LUPLInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LUPLInsert] 
    @PatientID int,
    @CellAddres int,
    @ForeColor nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LUPL] ([PatientID], [CellAddres], [ForeColor])
	SELECT @PatientID, @CellAddres, @ForeColor
	    
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LUPLSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LUPLSelect] 
    @LUcellID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LUcellID], [PatientID], [CellAddres], [ForeColor] 
	FROM   [dbo].[LUPL] 
	WHERE  ([LUcellID] = @LUcellID OR @LUcellID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LUPLUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LUPLUpdate] 
    @LUcellID int,
    @PatientID int,
    @CellAddres int,
    @ForeColor nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[LUPL]
	SET    [CellAddres] = @CellAddres, [ForeColor] = @ForeColor
	WHERE  [LUcellID] = @LUcellID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LUSTYLE_IU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LUSTYLE_IU]
  @LUcellID int,
  @PatientID int,
  @CellAddres int,
  @BakImg image AS
BEGIN
  IF EXISTS
  (SELECT * FROM dbo.LUSTYLE WHERE LUcellID = @LUcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres)
    UPDATE dbo.LUSTYLE SET
      PatientID = @PatientID,
      CellAddres = @CellAddres,
      BakImg = @BakImg
    WHERE LUcellID = @LUcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres
  ELSE
    INSERT INTO dbo.LUSTYLE (
      --LUcellID,
      PatientID,
      CellAddres,
      BakImg)
    VALUES(
      --@LUcellID,
      @PatientID,
      @CellAddres,
      @BakImg)
END
GO
/****** Object:  StoredProcedure [dbo].[LUSTYLEDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LUSTYLEDelete] 
    @LUcellID  [int],
	 @PatientID 	[int],
	 @CellAdres [int]
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[LUSTYLE]
	WHERE  [LUcellID] = @LUcellID AND [PatientID] = @PatientID AND CellAddres=@CellAdres

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LUSTYLEInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LUSTYLEInsert] 
    @PatientID int,
    @CellAddres int,
    @BakImg image
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[LUSTYLE] ([PatientID], [CellAddres], [BakImg])
	SELECT @PatientID, @CellAddres, @BakImg
	   
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LUSTYLESelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LUSTYLESelect] 
    @LUcellID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LUcellID], [PatientID], [CellAddres], [BakImg] 
	FROM   [dbo].[LUSTYLE] 
	WHERE  ([LUcellID] = @LUcellID OR @LUcellID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LUSTYLEUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LUSTYLEUpdate] 
    @LUcellID int,
    @PatientID int,
    @CellAddres int,
    @BakImg image
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[LUSTYLE]
	SET    [PatientID] = @PatientID, [CellAddres] = @CellAddres, [BakImg] = @BakImg
	WHERE  [LUcellID] = @LUcellID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[LUUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LUUpdate] 
    @LUID int,
    @PatientID int,
    @LU1 nvarchar(50) = NULL,
    @LU2 nvarchar(50) = NULL,
    @LU3 nvarchar(50) = NULL,
    @LU4 nvarchar(50) = NULL,
    @LU5 nvarchar(50) = NULL,
    @LU6 nvarchar(50) = NULL,
    @LU7 nvarchar(50) = NULL,
    @LU8 nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[LU]
	SET    [LU1] = @LU1, [LU2] = @LU2, [LU3] = @LU3, [LU4] = @LU4, [LU5] = @LU5, [LU6] = @LU6, [LU7] = @LU7, [LU8] = @LU8
	WHERE  [LUID] = @LUID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineDozeDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineDozeDelete] 
    @DozeID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedicineDoze]
	WHERE  [DozeID] = @DozeID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineDozeInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineDozeInsert] 
    @ShapeID int,
    @Doze nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MedicineDoze] ([ShapeID], [Doze])
	SELECT @ShapeID, @Doze
	    
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineDozeSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineDozeSelect] 
    @DozeID int
AS 
	
	 

	BEGIN TRAN

	SELECT [DozeID], [ShapeID], [Doze] 
	FROM   [dbo].[MedicineDoze] 
	WHERE  ([DozeID] = @DozeID OR @DozeID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineDozeUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineDozeUpdate] 
    @DozeID int,
    @ShapeID int,
    @Doze nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[MedicineDoze]
	SET    [ShapeID] = @ShapeID, [Doze] = @Doze
	WHERE  [DozeID] = @DozeID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineFamilyDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineFamilyDelete] 
    @SubCatID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedicineFamily]
	WHERE  [SubCatID] = @SubCatID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineFamilyInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineFamilyInsert] 
    @MedicineID int,
    @MedicineSubCat nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MedicineFamily] ([MedicineID], [MedicineSubCat])
	SELECT @MedicineID, @MedicineSubCat
	   
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineFamilySelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineFamilySelect] 
    @SubCatID int
AS 
	
	 

	BEGIN TRAN

	SELECT [SubCatID], [MedicineID], [MedicineSubCat] 
	FROM   [dbo].[MedicineFamily] 
	WHERE  ([SubCatID] = @SubCatID OR @SubCatID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineFamilyUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineFamilyUpdate] 
    @SubCatID int,
    @MedicineID int,
    @MedicineSubCat nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[MedicineFamily]
	SET    [MedicineID] = @MedicineID, [MedicineSubCat] = @MedicineSubCat
	WHERE  [SubCatID] = @SubCatID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineGroupsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineGroupsDelete] 
    @MedicineID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedicineGroups]
	WHERE  [MedicineID] = @MedicineID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineGroupsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineGroupsInsert] 
    @MedicineFamily nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MedicineGroups] ([MedicineFamily])
	SELECT @MedicineFamily
	    
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineGroupsSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineGroupsSelect] 
    @MedicineID int
AS 
	
	 

	BEGIN TRAN

	SELECT [MedicineID], [MedicineFamily] 
	FROM   [dbo].[MedicineGroups] 
	WHERE  ([MedicineID] = @MedicineID OR @MedicineID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineGroupsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineGroupsUpdate] 
    @MedicineID int,
    @MedicineFamily nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[MedicineGroups]
	SET    [MedicineFamily] = @MedicineFamily
	WHERE  [MedicineID] = @MedicineID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineItemsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineItemsDelete] 
    @MedicineItemID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedicineItems]
	WHERE  [MedicineItemID] = @MedicineItemID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineItemsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineItemsInsert] 
    @ScincID int,
    @CommName nvarchar(50) = NULL,
    @Company nvarchar(50) = NULL,
    @Notes nvarchar(150) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MedicineItems] ([ScincID], [CommName], [Company], [Notes])
	SELECT @ScincID, @CommName, @Company, @Notes
	     
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineItemsSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineItemsSelect] 
    @MedicineItemID int
AS 
	
	 

	BEGIN TRAN

	SELECT [MedicineItemID], [ScincID], [CommName], [Company], [Notes] 
	FROM   [dbo].[MedicineItems] 
	WHERE  ([MedicineItemID] = @MedicineItemID OR @MedicineItemID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineItemsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineItemsUpdate] 
    @MedicineItemID int,
    @ScincID int,
    @CommName nvarchar(50) = NULL,
    @Company nvarchar(50) = NULL,
    @Notes nvarchar(150) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[MedicineItems]
	SET    [ScincID] = @ScincID, [CommName] = @CommName, [Company] = @Company, [Notes] = @Notes
	WHERE  [MedicineItemID] = @MedicineItemID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineShapeDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineShapeDelete] 
    @ShapeID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedicineShape]
	WHERE  [ShapeID] = @ShapeID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineShapeInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineShapeInsert] 
    @MedicineItemID int,
    @MedicineShape nvarchar(50) = NULL,
    @ShapeInfo nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MedicineShape] ([MedicineItemID], [MedicineShape], [ShapeInfo])
	SELECT @MedicineItemID, @MedicineShape, @ShapeInfo
	    
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineShapeSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineShapeSelect] 
    @ShapeID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo] 
	FROM   [dbo].[MedicineShape] 
	WHERE  ([ShapeID] = @ShapeID OR @ShapeID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedicineShapeUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedicineShapeUpdate] 
    @ShapeID int,
    @MedicineItemID int,
    @MedicineShape nvarchar(50) = NULL,
    @ShapeInfo nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[MedicineShape]
	SET    [MedicineItemID] = @MedicineItemID, [MedicineShape] = @MedicineShape, [ShapeInfo] = @ShapeInfo
	WHERE  [ShapeID] = @ShapeID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedScienceFamilyDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedScienceFamilyDelete] 
    @ScincID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedScienceFamily]
	WHERE  [ScincID] = @ScincID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedScienceFamilyInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedScienceFamilyInsert] 
    @SubCatID int,
    @ScienceName nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MedScienceFamily] ([SubCatID], [ScienceName])
	SELECT @SubCatID, @ScienceName
	    
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedScienceFamilySelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedScienceFamilySelect] 
    @ScincID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ScincID], [SubCatID], [ScienceName] 
	FROM   [dbo].[MedScienceFamily] 
	WHERE  ([ScincID] = @ScincID OR @ScincID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MedScienceFamilyUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MedScienceFamilyUpdate] 
    @ScincID int,
    @SubCatID int,
    @ScienceName nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[MedScienceFamily]
	SET    [SubCatID] = @SubCatID, [ScienceName] = @ScienceName
	WHERE  [ScincID] = @ScincID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[MoveFirstRowToNewRowPerPatient]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MoveFirstRowToNewRowPerPatient]
AS
BEGIN
    SET NOCOUNT ON;

    -- Step 1: Retrieve and store the first row's data
    WITH FirstRowCTE AS (
        SELECT 
            [LDID], 
            [PatientID],
            [LD1], 
            [LD2], 
            [LD3], 
            [LD4], 
            [LD5], 
            [LD6], 
            [LD7], 
            [LD8],
            ROW_NUMBER() OVER (PARTITION BY [PatientID] ORDER BY [LDID]) AS RowNum
        FROM [dbo].[LD]
    )
    SELECT 
        [LDID], 
        [PatientID],
        [LD1], 
        [LD2], 
        [LD3], 
        [LD4], 
        [LD5], 
        [LD6], 
        [LD7], 
        [LD8]
    INTO #FirstRows
    FROM FirstRowCTE
    WHERE RowNum = 1;

    -- Step 2: Insert the new empty rows
    INSERT INTO [dbo].[LD] ([PatientID], [LD1], [LD2], [LD3], [LD4], [LD5], [LD6], [LD7], [LD8])
    SELECT DISTINCT [PatientID], NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
    FROM [dbo].[LD];

    -- Step 3: Update the new empty rows with the first row's data
    WITH NewEmptyRows AS (
        SELECT 
            [LDID],
            [PatientID],
            ROW_NUMBER() OVER (PARTITION BY [PatientID] ORDER BY [LDID] DESC) AS RowNum
        FROM [dbo].[LD]
        WHERE 
            [LD1] IS NULL AND [LD2] IS NULL AND [LD3] IS NULL AND [LD4] IS NULL AND
            [LD5] IS NULL AND [LD6] IS NULL AND [LD7] IS NULL AND [LD8] IS NULL
    )
    UPDATE LD
    SET 
        LD.[LD1] = FR.[LD1], 
        LD.[LD2] = FR.[LD2], 
        LD.[LD3] = FR.[LD3], 
        LD.[LD4] = FR.[LD4], 
        LD.[LD5] = FR.[LD5], 
        LD.[LD6] = FR.[LD6], 
        LD.[LD7] = FR.[LD7], 
        LD.[LD8] = FR.[LD8]
    FROM [dbo].[LD] LD
    JOIN #FirstRows FR ON LD.[PatientID] = FR.[PatientID]
    JOIN NewEmptyRows NR ON LD.[LDID] = NR.[LDID] AND NR.RowNum = 1;

    -- Step 4: Delete the original first rows
    DELETE FROM [dbo].[LD]
    WHERE [LDID] IN (SELECT [LDID] FROM #FirstRows);

    -- Clean up temporary table
    DROP TABLE #FirstRows;

    SET NOCOUNT OFF;
END;
GO
/****** Object:  StoredProcedure [dbo].[NwVisitProc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[NwVisitProc]
(@PatientID 	[int])
as

	set dateformat dmy
SELECT  [VisitDetID]
      ,[PatientID]
      ,[VisitDay]
      ,[VisTime]
      ,[PatientName]
      ,[VisDetail]
      ,[VisNotes]
      ,[VisDateTime]
  FROM [dbo].[Visits] 
where convert(smalldatetime,VisitDay) >= FORMAT (getdate(), 'dd/MM/yyyy ') AND PatientID=@PatientID
order by convert(smalldatetime,VisitDay)
GO
/****** Object:  StoredProcedure [dbo].[OrthoDiagDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoDiagDelete] 
    @DiagID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[OrthoDiag]
	WHERE  [DiagID] = @DiagID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoDiagInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoDiagInsert] 
    @PatientID int,
    @CloseType nvarchar(150) = NULL,
    @ClassI nvarchar(150) = NULL,
    @Bite nvarchar(150) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[OrthoDiag] ([PatientID], [CloseType], [ClassI], [Bite])
	SELECT @PatientID, @CloseType, @ClassI, @Bite
	   
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoDiagSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoDiagSelect] 
    @DiagID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [DiagID], [PatientID], [CloseType], [ClassI], [Bite] 
	FROM   [dbo].[OrthoDiag] 
	WHERE  ([DiagID] = @DiagID OR @DiagID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoDiagUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoDiagUpdate] 
    @DiagID int,
    @PatientID int,
    @CloseType nvarchar(150) = NULL,
    @ClassI nvarchar(150) = NULL,
    @Bite nvarchar(150) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[OrthoDiag]
	SET    [CloseType] = @CloseType, [ClassI] = @ClassI, [Bite] = @Bite
	WHERE  [DiagID] = @DiagID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoInfDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoInfDelete] 
    @OrthoID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[OrthoInf]
	WHERE  [OrthoID] = @OrthoID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoInfInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoInfInsert] 
    @PatientID int,
    @Compliants nvarchar(50) = NULL,
    @Birth nvarchar(50) = NULL,
    @Feed nvarchar(50) = NULL,
    @MilkTeethChng nvarchar(50) = NULL,
    @MilkTeethAppear nvarchar(50) = NULL,
    @TeethLoss nvarchar(50) = NULL,
    @BurriedTeeth nvarchar(50) = NULL,
    @OverLoadTeeth nvarchar(50) = NULL,
    @LipsCut nvarchar(50) = NULL,
    @ThroatCut nvarchar(50) = NULL,
    @IllnesPeriod nvarchar(50) = NULL,
    @CousinsHFactor nvarchar(50) = NULL,
    @BadHabits nvarchar(50) = NULL,
    @Malfunction nvarchar(50) = NULL,
    @Khota nvarchar(150) = NULL,
    @PrevOrth nvarchar(50) = NULL,
    @PrevIll nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[OrthoInf] ([PatientID], [Compliants], [Birth], [Feed], [MilkTeethChng], [MilkTeethAppear], [TeethLoss], [BurriedTeeth], [OverLoadTeeth], [LipsCut], [ThroatCut], [IllnesPeriod], [CousinsHFactor], [BadHabits], [Malfunction], [Khota], [PrevOrth], [PrevIll])
	SELECT @PatientID, @Compliants, @Birth, @Feed, @MilkTeethChng, @MilkTeethAppear, @TeethLoss, @BurriedTeeth, @OverLoadTeeth, @LipsCut, @ThroatCut, @IllnesPeriod, @CousinsHFactor, @BadHabits, @Malfunction, @Khota, @PrevOrth, @PrevIll
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoInfSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoInfSelect] 
    @OrthoID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [OrthoID], [PatientID], [Compliants], [Birth], [Feed], [MilkTeethChng], [MilkTeethAppear], [TeethLoss], [BurriedTeeth], [OverLoadTeeth], [LipsCut], [ThroatCut], [IllnesPeriod], [CousinsHFactor], [BadHabits], [Malfunction], [Khota], [PrevOrth], [PrevIll] 
	FROM   [dbo].[OrthoInf] 
	WHERE  ([OrthoID] = @OrthoID OR @OrthoID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoInfUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoInfUpdate] 
    @OrthoID int,
    @PatientID int,
    @Compliants nvarchar(50) = NULL,
    @Birth nvarchar(50) = NULL,
    @Feed nvarchar(50) = NULL,
    @MilkTeethChng nvarchar(50) = NULL,
    @MilkTeethAppear nvarchar(50) = NULL,
    @TeethLoss nvarchar(50) = NULL,
    @BurriedTeeth nvarchar(50) = NULL,
    @OverLoadTeeth nvarchar(50) = NULL,
    @LipsCut nvarchar(50) = NULL,
    @ThroatCut nvarchar(50) = NULL,
    @IllnesPeriod nvarchar(50) = NULL,
    @CousinsHFactor nvarchar(50) = NULL,
    @BadHabits nvarchar(50) = NULL,
    @Malfunction nvarchar(50) = NULL,
    @Khota nvarchar(150) = NULL,
    @PrevOrth nvarchar(50) = NULL,
    @PrevIll nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[OrthoInf]
	SET    [Compliants] = @Compliants, [Birth] = @Birth, [Feed] = @Feed, [MilkTeethChng] = @MilkTeethChng, [MilkTeethAppear] = @MilkTeethAppear, [TeethLoss] = @TeethLoss, [BurriedTeeth] = @BurriedTeeth, [OverLoadTeeth] = @OverLoadTeeth, [LipsCut] = @LipsCut, [ThroatCut] = @ThroatCut, [IllnesPeriod] = @IllnesPeriod, [CousinsHFactor] = @CousinsHFactor, [BadHabits] = @BadHabits, [Malfunction] = @Malfunction, [Khota] = @Khota, [PrevOrth] = @PrevOrth, [PrevIll] = @PrevIll
	WHERE  [OrthoID] = @OrthoID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoTreatDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoTreatDelete] 
    @TreatID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[OrthoTreat]
	WHERE  [TreatID] = @TreatID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoTreatInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoTreatInsert] 
    @PatientID int,
    @BeginDate smalldatetime = NULL,
    @OrthoType nvarchar(50) = NULL,
    @ExtraUL nvarchar(50) = NULL,
    @ExtraLL nvarchar(50) = NULL,
    @ExtraUR nvarchar(50) = NULL,
    @ExtraLR nvarchar(50) = NULL,
    @FixerDate smalldatetime = NULL,
    @FixerType nvarchar(50) = NULL,
    @BraketType nvarchar(50) = NULL,
    @FinishDate smalldatetime = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[OrthoTreat] ([PatientID], [BeginDate], [OrthoType], [ExtraUL], [ExtraLL], [ExtraUR], [ExtraLR], [FixerDate], [FixerType], [BraketType], [FinishDate])
	SELECT @PatientID, @BeginDate, @OrthoType, @ExtraUL, @ExtraLL, @ExtraUR, @ExtraLR, @FixerDate, @FixerType, @BraketType, @FinishDate
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoTreatSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoTreatSelect] 
    @TreatID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [TreatID], [PatientID], [BeginDate], [OrthoType], [ExtraUL], [ExtraLL], [ExtraUR], [ExtraLR], [FixerDate], [FixerType], [BraketType], [FinishDate] 
	FROM   [dbo].[OrthoTreat] 
	WHERE  ([TreatID] = @TreatID OR @TreatID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoTreatUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoTreatUpdate] 
    @TreatID int,
    @PatientID int,
    @BeginDate smalldatetime = NULL,
    @OrthoType nvarchar(50) = NULL,
    @ExtraUL nvarchar(50) = NULL,
    @ExtraLL nvarchar(50) = NULL,
    @ExtraUR nvarchar(50) = NULL,
    @ExtraLR nvarchar(50) = NULL,
    @FixerDate smalldatetime = NULL,
    @FixerType nvarchar(50) = NULL,
    @BraketType nvarchar(50) = NULL,
    @FinishDate smalldatetime = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[OrthoTreat]
	SET    [BeginDate] = @BeginDate, [OrthoType] = @OrthoType, [ExtraUL] = @ExtraUL, [ExtraLL] = @ExtraLL, [ExtraUR] = @ExtraUR, [ExtraLR] = @ExtraLR, [FixerDate] = @FixerDate, [FixerType] = @FixerType, [BraketType] = @BraketType, [FinishDate] = @FinishDate
	WHERE  [TreatID] = @TreatID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoTrtDetDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoTrtDetDelete] 
    @DetID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[OrthoTrtDet]
	WHERE  [DetID] = @DetID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoTrtDetInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoTrtDetInsert] 
    @PatientID int,
    @WorkDate smalldatetime = NULL,
    @WireMeasure nvarchar(50) = NULL,
    @WireType nvarchar(50) = NULL,
    @WireImg nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[OrthoTrtDet] ([PatientID], [WorkDate], [WireMeasure], [WireType], [WireImg])
	SELECT @PatientID, @WorkDate, @WireMeasure, @WireType, @WireImg
	  
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoTrtDetSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoTrtDetSelect] 
    @DetID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [DetID], [PatientID], [WorkDate], [WireMeasure], [WireType], [WireImg] 
	FROM   [dbo].[OrthoTrtDet] 
	WHERE  ([DetID] = @DetID OR @DetID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OrthoTrtDetUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OrthoTrtDetUpdate] 
    @DetID int,
    @PatientID int,
    @WorkDate smalldatetime = NULL,
    @WireMeasure nvarchar(50) = NULL,
    @WireType nvarchar(50) = NULL,
    @WireImg nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[OrthoTrtDet]
	SET    [WorkDate] = @WorkDate, [WireMeasure] = @WireMeasure, [WireType] = @WireType, [WireImg] = @WireImg
	WHERE  [DetID] = @DetID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OutDrDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OutDrDelete] 
    @DrID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[OutDr]
	WHERE  [DrID] = @DrID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OutDrInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OutDrInsert] 
    @DrName nvarchar(50),
    @DrAdres nvarchar(50) = NULL,
    @Drphone char(10) = NULL,
    @DrMobile char(10) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[OutDr] ([DrName], [DrAdres], [Drphone], [DrMobile])
	SELECT @DrName, @DrAdres, @Drphone, @DrMobile
	    
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OutDrSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OutDrSelect] 
    @DrID int
AS 
	
	 

	BEGIN TRAN

	SELECT [DrID], [DrName], [DrAdres], [Drphone], [DrMobile] 
	FROM   [dbo].[OutDr] 
	WHERE  ([DrID] = @DrID OR @DrID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[OutDrUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OutDrUpdate] 
    @DrID int,
    @DrName nvarchar(50),
    @DrAdres nvarchar(50) = NULL,
    @Drphone char(10) = NULL,
    @DrMobile char(10) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[OutDr]
	SET    [DrName] = @DrName, [DrAdres] = @DrAdres, [Drphone] = @Drphone, [DrMobile] = @DrMobile
	WHERE  [DrID] = @DrID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[p_DeleteRxBody]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_DeleteRxBody]

(
    @RxBdyID int
)
AS
BEGIN
    SET NOCOUNT ON
    DECLARE @rowcount INT, @error INT

    -- start transaction
	BEGIN TRANSACTION

    -- delete record using the specified criteria, 1 record deletion is expected
    DELETE FROM [dbo].[RxBody]
    WHERE  RxBdyID = @RxBdyID

    -- capture operation completion code and number of records affected
	SELECT @rowcount = @@ROWCOUNT, 
           @error = @@ERROR

    -- check for errors
	IF @error != 0
    BEGIN
        -- cancel transaction, undo changes
        ROLLBACK TRANSACTION

		-- report error and exit with non-zero exit code
        RAISERROR('Unable to delete record. See previous message for details.', 16, 1) 
		RETURN @error
    END
    -- check for rows updated
    IF @rowcount != 1 
    BEGIN
        -- cancel transaction, undo changes
        ROLLBACK TRANSACTION

		-- report error and exit with non-zero exit code
		IF @rowcount = 0
            RAISERROR('Warning. No records found for the specified criteria, while just 1 was expected.', 10, 1) 
		ELSE
            RAISERROR('Critical error. More than 1 record found for the specified criteria, while just 1 was expected.', 16, 1) 
		RETURN 1
    END 

    -- commit changes and return 0 code indicating successful completion
    COMMIT TRANSACTION
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[p_SaveRxBody]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_SaveRxBody]
(
    @RxBdyID int = NULL,
    @ArHdrName nvarchar(50) = NULL,
    @ArHdrAdres nvarchar(100) = NULL,
    @EnHdrName nvarchar(50) = NULL,
    @EnHdrAdres nvarchar(100) = NULL,
    @Logo image = NULL,
    @Detail nvarchar(150) = NULL,
    @ArFtr nvarchar(50) = NULL,
    @EnFtr nvarchar(50) = NULL,
    @WtrImg image = NULL,
    @WtrText nvarchar(50) = NULL,
    @UseWtrImg bit = NULL,
    @UseWtrText bit = NULL
)
AS
BEGIN
    SET NOCOUNT ON
    DECLARE @rowcount INT, @error INT, @id INT

    -- start transaction
	BEGIN TRANSACTION

    -- check if the specified record already exists, if yes, update it, if no, create it
    IF NOT EXISTS 
    (    
         SELECT * 
         FROM [dbo].[RxBody]
         WHERE  RxBdyID = @RxBdyID
    )
    BEGIN 
         -- insert new record
         INSERT INTO [dbo].[RxBody]
         (
             RxBdyID,
             ArHdrName,
             ArHdrAdres,
             EnHdrName,
             EnHdrAdres,
             Logo,
             Detail,
             ArFtr,
             EnFtr,
             WtrImg,
             WtrText,
             UseWtrImg,
             UseWtrText
         )
         VALUES 
         (
             @RxBdyID,
             @ArHdrName,
             @ArHdrAdres,
             @EnHdrName,
             @EnHdrAdres,
             @Logo,
             @Detail,
             @ArFtr,
             @EnFtr,
             @WtrImg,
             @WtrText,
             @UseWtrImg,
             @UseWtrText
         )
    END 
    ELSE
    BEGIN
         -- update existing record
         UPDATE [dbo].[RxBody]
         SET RxBdyID = @RxBdyID,
             ArHdrName = @ArHdrName,
             ArHdrAdres = @ArHdrAdres,
             EnHdrName = @EnHdrName,
             EnHdrAdres = @EnHdrAdres,
             Logo = @Logo,
             Detail = @Detail,
             ArFtr = @ArFtr,
             EnFtr = @EnFtr,
             WtrImg = @WtrImg,
             WtrText = @WtrText,
             UseWtrImg = @UseWtrImg,
             UseWtrText = @UseWtrText
         WHERE  RxBdyID = @RxBdyID
    END

    -- capture operation completion code and number of records affected
	SELECT @rowcount = @@ROWCOUNT, 
           @error = @@ERROR,
           @id = SCOPE_IDENTITY()

	IF @error != 0
    BEGIN
        -- cancel transaction, undo changes
        ROLLBACK TRANSACTION

		-- report error and exit with non-zero exit code
        RAISERROR('Unable to update or insert new record. See previous message for details.', 16, 1) 
		RETURN @error
    END
    IF @rowcount != 1 
    BEGIN
        -- cancel transaction, undo changes
        ROLLBACK TRANSACTION

		-- report error and exit with non-zero exit code
        RAISERROR('Critical error. More than 1 record found for the specified criteria, just 1 is expected.', 16, 1) 
		RETURN 1
    END 

    -- commit changes and return 0 code indicating successful completion
    COMMIT TRANSACTION

	-- if operation type 'Add record', return result set with the last inserted column value 
    IF @id IS NOT NULL
        SELECT @id AS NewRecordID
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[Patient_ImgsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_ImgsDelete] 
    @PicID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_Imgs]
	WHERE  [PicID] = @PicID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_ImgsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_ImgsInsert] 
    @PatientID int,
    @PicName nvarchar(50) = NULL,
    @PicPath nvarchar(50) = NULL,
    @FullName nvarchar(512) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Patient_Imgs] ([PatientID], [PicName], [PicPath], [FullName])
	SELECT @PatientID, @PicName, @PicPath, @FullName
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_ImgsSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_ImgsSelect] 
    @PicID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PicID], [PatientID], [PicName], [PicPath], [FullName] 
	FROM   [dbo].[Patient_Imgs] 
	WHERE  ([PicID] = @PicID OR @PicID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_ImgsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_ImgsUpdate] 
    @PicID int,
    @PatientID int,
    @PicName nvarchar(50) = NULL,
    @PicPath nvarchar(50) = NULL,
    @FullName nvarchar(512) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Patient_Imgs]
	SET    [PatientID] = @PatientID, [PicName] = @PicName, [PicPath] = @PicPath, [FullName] = @FullName
	WHERE  [PicID] = @PicID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_MobStrucAddDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_MobStrucAddDelete] 
    @AddTothID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_MobStrucAdd]
	WHERE  [AddTothID] = @AddTothID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_MobStrucAddInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_MobStrucAddInsert] 
    @StrucID int,
    @StrucName nvarchar(50) = NULL,
    @ToothLoc nvarchar(50) = NULL,
    @ToothNum nvarchar(50) = NULL,
    @AddTothDate smalldatetime
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Patient_MobStrucAdd] ([StrucID], [StrucName], [ToothLoc], [ToothNum], [AddTothDate])
	SELECT @StrucID, @StrucName, @ToothLoc, @ToothNum, @AddTothDate
	 
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_MobStrucAddSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_MobStrucAddSelect] 
    @AddTothID int
AS 
	
	 

	BEGIN TRAN

	SELECT [AddTothID], [StrucID], [StrucName], [ToothLoc], [ToothNum], [AddTothDate] 
	FROM   [dbo].[Patient_MobStrucAdd] 
	WHERE  ([AddTothID] = @AddTothID OR @AddTothID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_MobStrucAddUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_MobStrucAddUpdate] 
    @AddTothID int,
    @StrucID int,
    @StrucName nvarchar(50) = NULL,
    @ToothLoc nvarchar(50) = NULL,
    @ToothNum nvarchar(50) = NULL,
    @AddTothDate smalldatetime
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Patient_MobStrucAdd]
	SET    [StrucID] = @StrucID, [StrucName] = @StrucName, [ToothLoc] = @ToothLoc, [ToothNum] = @ToothNum, [AddTothDate] = @AddTothDate
	WHERE  [AddTothID] = @AddTothID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_MobStrucDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_MobStrucDelete] 
    @StrucID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_MobStruc]
	WHERE  [StrucID] = @StrucID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_MobStrucInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_MobStrucInsert] 
    @PatientID int,
    @StrucName nvarchar(50) = NULL,
    @StrucType nvarchar(50) = NULL,
    @TeethType nvarchar(50) = NULL,
    @StrucDate smalldatetime
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Patient_MobStruc] ([PatientID], [StrucName], [StrucType], [TeethType], [StrucDate])
	SELECT @PatientID, @StrucName, @StrucType, @TeethType, @StrucDate
	   
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_MobStrucSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_MobStrucSelect] 
    @StrucID int
AS 
	
	 

	BEGIN TRAN

	SELECT [StrucID], [PatientID], [StrucName], [StrucType], [TeethType], [StrucDate] 
	FROM   [dbo].[Patient_MobStruc] 
	WHERE  ([StrucID] = @StrucID OR @StrucID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_MobStrucUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_MobStrucUpdate] 
    @StrucID int,
    @PatientID int,
    @StrucName nvarchar(50) = NULL,
    @StrucType nvarchar(50) = NULL,
    @TeethType nvarchar(50) = NULL,
    @StrucDate smalldatetime
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Patient_MobStruc]
	SET    [PatientID] = @PatientID, [StrucName] = @StrucName, [StrucType] = @StrucType, [TeethType] = @TeethType, [StrucDate] = @StrucDate
	WHERE  [StrucID] = @StrucID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_NotesDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_NotesDelete] 
    @NoteID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_Notes]
	WHERE  [NoteID] = @NoteID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_NotesInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_NotesInsert] 
    @PatientID int,
    @NoteDate smalldatetime = NULL,
    @Note nvarchar(250) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Patient_Notes] ([PatientID], [NoteDate], [Note])
	SELECT @PatientID, @NoteDate, @Note
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_NotesSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_NotesSelect] 
    @NoteID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [NoteID], [PatientID], [NoteDate], [Note] 
	FROM   [dbo].[Patient_Notes] 
	WHERE  ([NoteID] = @NoteID OR @NoteID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_NotesUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_NotesUpdate] 
    @NoteID int,
    @PatientID int,
    @NoteDate smalldatetime = NULL,
    @Note nvarchar(250) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Patient_Notes]
	SET    [NoteDate] = @NoteDate, [Note] = @Note
	WHERE  [NoteID] = @NoteID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_PaysDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_PaysDelete] 
    @PayID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_Pays]
	WHERE  [PayID] = @PayID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_PaysInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_PaysInsert] 
    @TrtID int,
    @PatientID int = NULL,
    @PayValue money,
    @PayDate smalldatetime,
    @Notes nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Patient_Pays] ([TrtID], [PatientID], [PayValue], [PayDate], [Notes])
	SELECT @TrtID, @PatientID, @PayValue, @PayDate, @Notes
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_PaysSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_PaysSelect] 
    @PayID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PayID], [TrtID], [PatientID], [PayValue], [PayDate], [Notes] 
	FROM   [dbo].[Patient_Pays] 
	WHERE  ([PayID] = @PayID OR @PayID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_PaysUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_PaysUpdate] 
    @PayID int,
    @TrtID int,
    @PatientID int = NULL,
    @PayValue money,
    @PayDate smalldatetime,
    @Notes nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Patient_Pays]
	SET    [TrtID] = @TrtID, [PatientID] = @PatientID, [PayValue] = @PayValue, [PayDate] = @PayDate, [Notes] = @Notes
	WHERE  [PayID] = @PayID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_RXDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_RXDelete] 
    @RxID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_RX]
	WHERE  [RxID] = @RxID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_RXInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_RXInsert] 
    @PatientID int,
    @RXDate smalldatetime = NULL,
    @RX nvarchar(500) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Patient_RX] ([PatientID], [RXDate], [RX])
	SELECT @PatientID, @RXDate, @RX
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_RXSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_RXSelect] 
    @RxID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RxID], [PatientID], [RXDate], [RX] 
	FROM   [dbo].[Patient_RX] 
	WHERE  ([RxID] = @RxID OR @RxID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_RXUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_RXUpdate] 
    @RxID int,
    @PatientID int,
    @RXDate smalldatetime = NULL,
    @RX nvarchar(500) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Patient_RX]
	SET    [PatientID] = @PatientID, [RXDate] = @RXDate, [RX] = @RX
	WHERE  [RxID] = @RxID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_TrtsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_TrtsDelete] 
    @TrtID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_Trts]
	WHERE  [TrtID] = @TrtID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_TrtsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_TrtsInsert] 
    @PatientID int,
    @Detail nvarchar(50),
    @TrtDate smalldatetime,
    @TrtValue money
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Patient_Trts] ([PatientID], [Detail], [TrtDate], [TrtValue])
	SELECT @PatientID, @Detail, @TrtDate, @TrtValue
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_TrtsSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_TrtsSelect] 
    @TrtID int
AS 
	
	 

	BEGIN TRAN

	SELECT [TrtID], [PatientID], [Detail], [TrtDate], [TrtValue] 
	FROM   [dbo].[Patient_Trts] 
	WHERE  ([TrtID] = @TrtID OR @TrtID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[Patient_TrtsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Patient_TrtsUpdate] 
    @TrtID int,
    @PatientID int,
    @Detail nvarchar(50),
    @TrtDate smalldatetime,
    @TrtValue money
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Patient_Trts]
	SET    [PatientID] = @PatientID, [Detail] = @Detail, [TrtDate] = @TrtDate, [TrtValue] = @TrtValue
	WHERE  [TrtID] = @TrtID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[PatientBalance]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PatientBalance]
 @PatientID INT
AS
BEGIN
    /*****************************************************************
     * Time: 30/07/2024 7:53:39 PM
     * Author: Pascal
     * Comments: This procedure was generated using SQL Assistant's 
     * Refactoring -> Extract Procedure feature.
     *****************************************************************/
 
   
    -- Set the value for @PatientID
    --SET @PatientID =1100;
    
    WITH TreatmentTotal AS (
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
             GROUP BY
                    [PatientID]
         )
         , CombinedTotal AS (
             SELECT t.[PatientID],
                    COALESCE(t.TotalTreatments, 0) AS TotalTreatments,
                    COALESCE(p.TotalPayments, 0) AS TotalPayments,
                 COALESCE(p.TotalPayments, 0) -  COALESCE(t.TotalTreatments, 0)   AS Balance
             FROM   TreatmentTotal t
                    LEFT JOIN PaymentTotal p
                         ON  t.[PatientID] = p.[PatientID]
             UNION ALL
             SELECT p.[PatientID],
                    COALESCE(t.TotalTreatments, 0) AS TotalTreatments,
                    COALESCE(p.TotalPayments, 0) AS TotalPayments,
               COALESCE(p.TotalPayments, 0) - COALESCE(t.TotalTreatments, 0) AS Balance
             FROM   PaymentTotal p
                    LEFT JOIN TreatmentTotal t
                         ON  p.[PatientID] = t.[PatientID]
             WHERE  t.[PatientID] IS NULL
         )
    
    SELECT c.[PatientID],
           p.[PatientName],
           c.TotalTreatments,
           c.TotalPayments,
           c.Balance
    FROM   CombinedTotal c
           JOIN [dbo].[Patient] p
                ON  c.[PatientID] = p.[PatientID]
    WHERE  c.[PatientID] = @PatientID;
    

    RETURN @@ERROR
END
GO
/****** Object:  StoredProcedure [dbo].[PatientColorsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[PatientColorsDelete] 
    @ColorID int
AS 
	
		
begin
	set nocount on;
	declare @trancount int;
	set @trancount = @@trancount;
	begin try
		if @trancount = 0
			begin transaction
		else
			save transaction PatientColorsDelete;

		-- Do the actual work here
	DELETE
	FROM   [dbo].[PatientColors]
	WHERE  [ColorID] = @ColorID

	
	
lbexit:
		if @trancount = 0	
			commit;
	end try
	begin catch
		declare @error int, @message varchar(4000), @xstate int;
		select @error = ERROR_NUMBER(), @message = ERROR_MESSAGE(), @xstate = XACT_STATE();
		if @xstate = -1
			rollback;
		if @xstate = 1 and @trancount = 0
			rollback
		if @xstate = 1 and @trancount > 0
			rollback transaction PatientColorsDelete;;

		raiserror ('PatientColorsDelete: %d: %s', 16, 1, @error, @message) ;
	end catch	
end
GO
/****** Object:  StoredProcedure [dbo].[PatientColorsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[PatientColorsInsert] 
    @Color1 nvarchar(9) = NULL,
    @Color2 nvarchar(9) = NULL,
    @GradientIndex tinyint = NULL,
    @AlphaValue int = NULL,
    @PatientID int = NULL
AS 
		
begin
	set nocount on;
	declare @trancount int;
	set @trancount = @@trancount;
	begin try
		if @trancount = 0
			begin transaction
		else
			save transaction PatientColorsInsert;

		-- Do the actual work here
	INSERT INTO [dbo].[PatientColors] ([Color1], [Color2], [GradientIndex], [AlphaValue], [PatientID])
	VALUES (@Color1, @Color2, @GradientIndex, @AlphaValue, @PatientID)
	
	
lbexit:
		if @trancount = 0	
			commit;
	end try
	begin catch
		declare @error int, @message varchar(4000), @xstate int;
		select @error = ERROR_NUMBER(), @message = ERROR_MESSAGE(), @xstate = XACT_STATE();
		if @xstate = -1
			rollback;
		if @xstate = 1 and @trancount = 0
			rollback
		if @xstate = 1 and @trancount > 0
			rollback transaction PatientColorsInsert;;

		raiserror ('PatientColorsInsert: %d: %s', 16, 1, @error, @message) ;
	end catch	
end
GO
/****** Object:  StoredProcedure [dbo].[PatientColorsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PatientColorsUpdate] 
    @ColorID int,
    @Color1 nvarchar(9) = NULL,
    @Color2 nvarchar(9) = NULL,
    @GradientIndex tinyint = NULL,
    @AlphaValue int = NULL,
    @PatientID int = NULL
AS 

begin
	set nocount on;
	declare @trancount int;
	set @trancount = @@trancount;
	begin try
		if @trancount = 0
			begin transaction
		else
			save transaction PatientColorsUpdate;

		-- Do the actual work here
	UPDATE [dbo].[PatientColors]
	SET    [Color1] = @Color1, [Color2] = @Color2, [GradientIndex] = @GradientIndex, [AlphaValue] = @AlphaValue, [PatientID] = @PatientID
	WHERE  [ColorID] = @ColorID
	
	
	
lbexit:
		if @trancount = 0	
			commit;
	end try
	begin catch
		declare @error int, @message varchar(4000), @xstate int;
		select @error = ERROR_NUMBER(), @message = ERROR_MESSAGE(), @xstate = XACT_STATE();
		if @xstate = -1
			rollback;
		if @xstate = 1 and @trancount = 0
			rollback
		if @xstate = 1 and @trancount > 0
			rollback transaction PatientColorsUpdate;;

		raiserror ('PatientColorsUpdate:  %d: %s', 16, 1, @error, @message) ;
	end catch	
end
GO
/****** Object:  StoredProcedure [dbo].[PatientDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PatientDelete] 
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient]
	WHERE  [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[PatientInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---- Create a sequence tracking table (only once)
--CREATE TABLE dbo.PatientNumberSequence (
--    ID INT IDENTITY(1,1) PRIMARY KEY,
--    LastNumber BIGINT NOT NULL
--);

---- Initialize it once with your current max
--INSERT INTO dbo.PatientNumberSequence (LastNumber)
--SELECT ISNULL(MAX(CAST(PatientNumber AS BIGINT)), 0)
--FROM dbo.Patient
--WHERE ISNUMERIC(PatientNumber) = 1;
--GO
--CREATE OR ALTER PROC dbo.GetNextPatientNumberSP
--    @NextPatientNumber NVARCHAR(10) OUTPUT
--AS
--BEGIN
--    SET NOCOUNT ON;

--    DECLARE @NewVal BIGINT;

--    BEGIN TRANSACTION;

--    UPDATE dbo.PatientNumberSequence
--    SET @NewVal = LastNumber = LastNumber + 1;

--    COMMIT TRANSACTION;

--    SET @NextPatientNumber = RIGHT(REPLICATE('0', 10) + CAST(@NewVal AS NVARCHAR(10)), 10);
--END;
--GO


CREATE PROC [dbo].[PatientInsert] 
    @PatientName NVARCHAR(50),
    @Sex NVARCHAR(10) = NULL,
    @Age INT = NULL,
    @Phone NVARCHAR(16) = NULL,
    @Address NVARCHAR(50) = NULL,
    @Health NVARCHAR(50) = NULL,
    @Treat BIT = NULL,
    @Implant BIT = NULL,
    @Mobile BIT = NULL,
    @Ortho BIT = NULL,
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
        DECLARE @NextPatientNumber NVARCHAR(10);

        -- Generate the next patient number safely
        EXEC dbo.GetNextPatientNumberSP @NextPatientNumber OUTPUT;

        -- Check if the PatientName already exists
        IF EXISTS (SELECT 1 FROM [dbo].[Patient] WHERE [PatientName] = @PatientName)
        BEGIN
            SET @ReturnValue = -2;  -- Duplicate name
            ROLLBACK TRANSACTION;
            RETURN @ReturnValue;
        END

        -- Insert into Patient table (with PatientNumber)
        INSERT INTO [dbo].[Patient]
        ([PatientName], [PatientNumber], [Sex], [Age], [Phone], [Address], [Health],
         [Treat], [Implant], [Mobile], [Ortho], [Struc], [Notes],
         [BirthY], [CreatedBy], [CreateDate])
        VALUES
        (@PatientName, @NextPatientNumber, @Sex, @Age, @Phone, @Address, @Health,
         @Treat, @Implant, @Mobile, @Ortho, @Struc, @Notes,
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
/****** Object:  StoredProcedure [dbo].[PatientsBalance]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PatientsBalance]
    
AS
BEGIN
    /*****************************************************************
     * Time: 30/07/2024 7:52:02 PM
     * Author: Pascal
     * Comments: This procedure was generated using SQL Assistant's 
     * Refactoring -> Extract Procedure feature.
     *****************************************************************/
 
    --DECLARE @PatientID INT;
    -- Set the value for @PatientID
    --SET @PatientID =1100;
    
    WITH TreatmentTotal AS (
             SELECT [PatientID],
                    SUM([TrtValue]) AS TotalTreatments
             FROM   [dbo].[Patient_Trts]
                    --WHERE
                    --    [PatientID] = @PatientID
             GROUP BY
                    [PatientID]
         ),
         PaymentTotal AS (
             SELECT [PatientID],
                    SUM([PayValue]) AS TotalPayments
             FROM   [dbo].[Patient_Pays]
                    --WHERE
                    --    [PatientID] = @PatientID
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
                         ON  t.[PatientID] = p.[PatientID]
             UNION ALL
             SELECT p.[PatientID],
                    COALESCE(t.TotalTreatments, 0) AS TotalTreatments,
                    COALESCE(p.TotalPayments, 0) AS TotalPayments,
                    COALESCE(p.TotalPayments, 0) - COALESCE(t.TotalTreatments, 0) AS Balance
             FROM   PaymentTotal p
                    LEFT JOIN TreatmentTotal t
                         ON  p.[PatientID] = t.[PatientID]
             WHERE  t.[PatientID] IS NULL
         )
    
    SELECT c.[PatientID],
           p.[PatientName],
           c.TotalTreatments,
           c.TotalPayments,
           c.Balance
    FROM   CombinedTotal c
           JOIN [dbo].[Patient] p
                ON  c.[PatientID] = p.[PatientID]
                    --WHERE
                    --    c.[PatientID] = @PatientID;
    

    RETURN @@ERROR
END
GO
/****** Object:  StoredProcedure [dbo].[PatientsDebts]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PatientsDebts]
AS
BEGIN
    -- Treats subquery: total treatment values per patient
    WITH TreatTotals AS (
        SELECT PatientID, SUM(TrtValue) AS TotalTreats
        FROM Patient_Trts
        GROUP BY PatientID
    ),
    -- Pays subquery: total pay values per patient via TrtID
    PayTotals AS (
        SELECT pt.PatientID, SUM(pp.PayValue) AS TotalPays
        FROM Patient_Trts pt
        JOIN Patient_Pays pp ON pt.TrtID = pp.TrtID
        GROUP BY pt.PatientID
    )

    SELECT 
        p.PatientID,
        p.PatientName,
        p.Sex,
        p.Age,
        p.Phone,
        p.Address,
        p.Health,
        p.Treat,
        p.Implant,
        p.Mobile,
        p.Ortho,
        p.Struc,
        p.Notes,
        p.BirthY,
        ISNULL(tt.TotalTreats, 0) AS TotalTreats,
        ISNULL(pt.TotalPays, 0) AS TotalPays,
        ISNULL(pt.TotalPays, 0) - ISNULL(tt.TotalTreats, 0) AS Balance
    FROM Patient p
    LEFT JOIN TreatTotals tt ON p.PatientID = tt.PatientID
    LEFT JOIN PayTotals pt ON p.PatientID = pt.PatientID
    ORDER BY p.PatientName;

    RETURN @@ERROR
END;

GO
/****** Object:  StoredProcedure [dbo].[PatientSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PatientSelect] 
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PatientID], [PatientName], [Sex], [Age], [Phone], [Address], [Health], [Treat], [Implant], [Mobile], [Ortho], [Struc], [Notes], [BirthY], [CreatedBy], [CreateDate] 
	FROM   [dbo].[Patient] 
	WHERE  ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[PatientTrtPays]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PatientTrtPays]
@PatientID INT    
AS
BEGIN
    /*****************************************************************
     * Time: 30/07/2024 10:13:28 PM
     * Author: Pascal
     * Comments: This procedure was generated using SQL Assistant's 
     * Refactoring -> Extract Procedure feature.
     *****************************************************************/
 
   
    
    WITH TreatmentPayments AS (
             SELECT t.[TrtID],
                    t.[PatientID],
                    t.[TrtDate]           AS TransDate,
                    N'علاج: ' + t.[Detail]  AS Details,
                    t.[TrtValue]          AS TreatValue,
                    ISNULL(SUM(p.[PayValue]), 0) AS TotalPayments,
                  ISNULL(SUM(p.[PayValue]), 0) -  t.[TrtValue]  AS Balance
             FROM   [dbo].[Patient_Trts] t
                    LEFT JOIN [dbo].[Patient_Pays] p
                         ON  t.[TrtID] = p.[TrtID]
             WHERE  t.[PatientID] = @PatientID
             GROUP BY
                    t.[TrtID],
                    t.[PatientID],
                    t.[TrtDate],
                    t.[Detail],
                    t.[TrtValue]
         )
    
    SELECT tp.[TrtID],
           tp.[PatientID],
           tp.TransDate,
           tp.Details,
           tp.TreatValue      AS VALUE,
           tp.TotalPayments,
           tp.Balance
    FROM   TreatmentPayments     tp
    ORDER BY
           tp.TransDate,
           tp.[TrtID];
    

    RETURN @@ERROR
END
GO
/****** Object:  StoredProcedure [dbo].[PatientUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PatientUpdate] 
    @PatientID int,
    @PatientName nvarchar(50),
    @Sex nvarchar(10) = NULL,
    @Age int = NULL,
    @Phone nvarchar(16) = NULL,
    @Address nvarchar(50) = NULL,
    @Health nvarchar(50) = NULL,
    @Treat bit = NULL,
    @Implant bit = NULL,
    @Mobile bit = NULL,
    @Ortho bit = NULL,
    @Struc bit = NULL,
    @Notes nvarchar(150) = NULL,
    @BirthY int = NULL,
    @CreatedBy int = NULL,
    @CreateDate datetime = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Patient]
	SET    [PatientName] = @PatientName, [Sex] = @Sex, [Age] = @Age, [Phone] = @Phone, [Address] = @Address, [Health] = @Health, [Treat] = @Treat, [Implant] = @Implant, [Mobile] = @Mobile, [Ortho] = @Ortho, [Struc] = @Struc, [Notes] = @Notes, [BirthY] = @BirthY, [CreatedBy] = @CreatedBy, [CreateDate] = @CreateDate
	WHERE  [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[procUtils_GenerateClass]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 
CREATE PROCEDURE [dbo].[procUtils_GenerateClass]
@TableName [varchar](50)
WITH EXECUTE AS CALLER
AS
BEGIN -- proc start                                      
SET NOCOUNT ON;                                      
 
DECLARE @DbName nvarchar(200 ) 
DECLARE @CSharpDataType nvarchar(200)
select @DbName = DB_NAME()               
declare @strCode nvarchar(max) 
set @strCode = ''
 
-- use this variable while generating code 
DECLARE @WinNewLine nvarchar(2)
set @WinNewLine = CONVERT ( nvarchar(2) , CHAR(13) + CHAR(10))
 
-- this is a horizontal tab
DECLARE @Tab nvarchar(1)
set @Tab = CONVERT(NVARCHAR(1) , CHAR(9))
 
SET @DbName = 'Cas'
 
BEGIN TRY        --begin try                            
 
set @strCode = @strCode +  'using System ;  ' + @WinNewLine + @WinNewLine
 
set @strCode = @strCode +  'namespace ' + @DbName + '.Model  {' + @WinNewLine  
set @strCode = @strCode +  'public class ' + @TableName + @Tab +  '{ ' + @WinNewLine  + @WinNewLine
set @strCode = @strCode +  @WinNewLine + '#region FieldsAndProps ' + @WinNewLine + @WinNewLine
--CODE SNIPPET TO LIST TABLE COLUMNS           
-- RUN IN SSMS WITH cTRL + t FIRST TO OUTPUT THE RESULT TO TEXT FOR COPY PASTE          
--FIRST SEARCH THE TABLE WHICH HAD A "Feature" in its name           
--SELECT NAME FROM SYS.TABLES WHERE NAME LIKE '%Feature%'          
--select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='Feature'              
DECLARE @ColNames TABLE                                
(                                
Number [int] IDENTITY(1,1), --Auto incrementing Identity column                              
ColName [varchar](300) , --The string value                        ,         
DataType varchar(50) ,  --the datatype                       
IS_NULLABLE nvarchar(5) , --should we add =null in front         
CHARACTER_MAXIMUM_LENGTH INT        
)                                
--Decalre a variable to remember the position of the current delimiter                                
DECLARE @CurrentDelimiterPositionVar INT                                 
DECLARE @PkColName varchar(200)      
set @PkColName = ''
declare @ColumnName varchar(200)  
--Decalre a variable to remember the number of rows in the table                                
DECLARE @Count INT                                
--Populate the TABLE variable using some logic                                
-- SELECT * from INFORMATION_SCHEMA.COLUMNS                             
 
--SET IDENTITY_INSERT ON ; 
INSERT INTO @ColNames         
SELECT column_name ,  Data_type , IS_NULLABLE , CHARACTER_MAXIMUM_LENGTH  
from INFORMATION_SCHEMA.COLUMNS                             
where TABLE_NAME=@TableName                                
--Initialize the looper variable                                
SET @CurrentDelimiterPositionVar = 1                                
--Determine the number of rows in the Table                                
SELECT @Count=max(Number) from @ColNames                                
--A variable to hold the currently selected value from the table                                
DECLARE @ColName varchar(300);                                
DECLARE @DataType varchar(50)                      
DECLARE @IS_NULLABLE VARCHAR(5)        
DECLARE @CHARACTER_MAXIMUM_LENGTH INT        
-- START GENERATING PROPERTIES   
--Loop through until all row processing is done              
WHILE @CurrentDelimiterPositionVar <= @Count --1st loop        
BEGIN                                
--Load current value from the Table                                
SELECT @ColName = ColName FROM @ColNames         
WHERE Number = @CurrentDelimiterPositionVar        
SELECT @DataType = DataType FROM @ColNames         
WHERE Number = @CurrentDelimiterPositionVar               
SELECT @IS_NULLABLE = IS_NULLABLE FROM @ColNames         
WHERE Number = @CurrentDelimiterPositionVar                    
SELECT @CHARACTER_MAXIMUM_LENGTH = CHARACTER_MAXIMUM_LENGTH FROM @ColNames         
WHERE Number = @CurrentDelimiterPositionVar                    
 
IF @DataType = 'timestamp'        
begin 
SET @CurrentDelimiterPositionVar = @CurrentDelimiterPositionVar + 1;                                
continue ; 
end 
 
 
 
-- get the C# type based on the passed sqlType, )( needs the DbVsCSharpTypes table ) 
set @CSharpDataType=( SELECT  dbo.funcGetCLRTypeBySqlType(@DataType) )
 
 
 
DECLARE @varPrivate nvarchar(200) 
set @varPrivate = '_' + @ColName 
 
 
 
-- GENERATE THE START REGION PART 
SET @strCode = @strCode + @WinNewLine + @Tab+ @Tab + '#region ' + @ColName + @WinNewLine + @WinNewLine 
 
-- set the nullable 
IF @IS_NULLABLE = 'YES'  and @CSharpDataType <> 'string'      
SET @StrCode = @strCode +   'private ' + @CSharpDataType + '? ' +  @varPrivate + ' ;' + @WinNewLine 
ELSE 
SET @StrCode = @strCode +   'private ' + @CSharpDataType + ' ' +  @varPrivate + ' ;' + @WinNewLine 
 
 
 
 
 
-- GENERATE THE PUBLIC MEMBER 
IF @IS_NULLABLE = 'YES'  and @CSharpDataType <> 'string'
SET @StrCode = @strCode +   'public ' + @CSharpDataType +  '? ' + @ColName + @WinNewLine + '{' + @WinNewLine
ELSE 
SET @StrCode = @strCode +   'public ' + @CSharpDataType +  ' ' + @ColName + @WinNewLine + '{' + @WinNewLine
 
SET @StrCode = @strCode + @Tab + @Tab + 'get { return ' + @varPrivate + '; } ' 
SET @strCode = @strCode + @Tab + @Tab + 'set { ' + @varPrivate +' = value ; }' + @WinNewLine 
SET @strCode = @strCode + @Tab + '} //eof prop ' + @ColName + @WinNewLine 
 
-- GENERATE THE ENDREGION PART FOR THE PROPERTY
SET @strCode = @strCode + @WinNewLine + @Tab + @Tab + '#endregion ' + @ColName  + @WinNewLine
 
 
--if @CurrentDelimiterPositionVar != @Count         
--SET @StrCode = @StrCode + ''        
 
--PRINT @StrCode         
--DEBUGGING        
--PRINT '@ColName - ' + @ColName         
--PRINT '@CSharpDataType - ' + @CSharpDataType                  
--PRINT '@IS_NULLABLE - ' + @IS_NULLABLE         
--PRINT '@CHARACTER_MAXIMUM_LENGTH - ' +  CONVERT ( VARCHAR , @CHARACTER_MAXIMUM_LENGTH )   
 
set @strCode = @strCode + @WinNewLine     
SELECT @strCode 
SET @strCode = ''
SET @CurrentDelimiterPositionVar = @CurrentDelimiterPositionVar + 1;                                
END   --eof while 1-st loop
SELECT @strCode --IF WE ARE GENERATING MORE THAN 4000 CHARS 
SET @strCode = ''                  
set @strCode = @strCode + @WinNewLine+ @WinNewLine + @Tab + @Tab +  '#endregion FieldsAndProps ' + @WinNewLine + @WinNewLine
-- END GENERATION PROPERTIES 
 
 
-- START CONSTRUCTOR 
SET @CurrentDelimiterPositionVar = 1 --RESTART THE COUNTER
set @strCode = @strCode +  @WinNewLine + '#region Constructor' + @WinNewLine + @WinNewLine
set @strCode = @strCode + 'public ' + @TableName + '(System.Data.DataRow dr ) ' + @WinNewLine + '{'
 
 
WHILE @CurrentDelimiterPositionVar <= @Count --2nd loop        
BEGIN                                
--Load current value from the Table                                
SELECT @ColName = ColName FROM @ColNames         
WHERE Number = @CurrentDelimiterPositionVar        
SELECT @DataType = DataType FROM @ColNames         
WHERE Number = @CurrentDelimiterPositionVar               
SELECT @IS_NULLABLE = IS_NULLABLE FROM @ColNames         
WHERE Number = @CurrentDelimiterPositionVar                    
SELECT @CHARACTER_MAXIMUM_LENGTH = CHARACTER_MAXIMUM_LENGTH FROM @ColNames         
WHERE Number = @CurrentDelimiterPositionVar                    
 
IF @DataType = 'timestamp'        
begin 
SET @CurrentDelimiterPositionVar = @CurrentDelimiterPositionVar + 1;                                
continue ; 
end 
 
 
 
-- get the C# type based on the passed sqlType, )( needs the DbVsCSharpTypes table ) 
set @CSharpDataType =( SELECT  dbo.funcGetCLRTypeBySqlType(@DataType) )
 
 
 
 
set @varPrivate = '_' + @ColName 
 
 
 
-- GENERATE THE START REGION PART 
SET @strCode = @strCode + @WinNewLine + @Tab+ @Tab + '#region ' + @ColName + @WinNewLine  + @WinNewLine
 
---- set the nullable 
--IF @IS_NULLABLE = 'YES'  and @Data <> 'string'      
--    SET @StrCode = @strCode +   'private ' + @CSharpDataType + '? ' +  @varPrivate + ' ;' + @WinNewLine 
--ELSE 
--    SET @StrCode = @strCode +   'private ' + @CSharpDataType + ' ' +  @varPrivate + ' ;' + @WinNewLine 
 
 
DECLARE @strConvertToCode nvarchar(200) 
set @strConvertToCode  =( SELECT  dbo.funcGetCLRConvertToCodeBySqlType(@DataType) )
 
 
-- THE DBNULL
--IF @IS_NULLABLE = 'YES'  and @DataType <> 'string'
--    BEGIN
SET @StrCode = @strCode + @Tab + @Tab + @Tab + 
'if (dr["' + @ColName + '"] != null && !(dr["' + @ColName + '"] is DBNull))
this.' + @ColName + ' = ' + @strConvertToCode  + '(dr["' + @ColName + '"]);' + @WinNewLine
 
--    END --EOF IF @IS_NULLABLE = 'YES'  and @DataType <> 'string'
--ELSE 
--    BEGIN
--    SET @StrCode = @strCode +   'public ' + @DataType +  ' ' + @ColName + @WinNewLine + '{' + @WinNewLine
--    END -- EOF ELSE IF @IS_NULLABLE = 'YES'  and @DataType <> 'string'
 
 
 
-- GENERATE THE END REGION PART FOR THE IF DR
SET @strCode = @strCode + @WinNewLine + @Tab+ @Tab + '#endregion ' + @ColName  + @WinNewLine + @WinNewLine
 
 
--??
--if @CurrentDelimiterPositionVar != @Count         
--SET @StrCode = @StrCode + ''        
 
--PRINT @StrCode         
--DEBUGGING        
--PRINT '@ColName - ' + @ColName         
--PRINT '@DataType - ' + @DataType                  
--PRINT '@IS_NULLABLE - ' + @IS_NULLABLE         
--PRINT '@CHARACTER_MAXIMUM_LENGTH - ' +  CONVERT ( VARCHAR , @CHARACTER_MAXIMUM_LENGTH )   
 
SELECT @strCode --IF WE ARE GENERATING MORE THAN 4000 CHARS 
set @strCode = ''
SET @CurrentDelimiterPositionVar = @CurrentDelimiterPositionVar + 1;                                
END --eof while 1-st loop 
 
SET @strCode = ''
 
set @strCode = @strCode + @Tab + '} //eof const for'+ @TableName 
set @strCode = @strCode + @WinNewLine + @WinNewLine + @Tab + '#endregion Constructor' + @WinNewLine + @WinNewLine
-- END CONSTRUCTOR 
 
-- BEGIN PARAMETERLESS CONST
set @strCode = @strCode +  @WinNewLine + '#region Parameterless Constructor' + @WinNewLine + @WinNewLine
set @strCode = @strCode + 'public ' + @TableName + '() ' + @WinNewLine + '{'
-- NOTHING HAPPENS HERE
set @strCode = @strCode + @Tab + '} //eof Parameterless const for'+ @TableName 
set @strCode = @strCode + @WinNewLine + @WinNewLine + @Tab + '#endregion Parameterless Constructor' + @WinNewLine + @WinNewLine
-- END PARAMETERLESS CONST
 
 
 
 
 
set @strCode = + @strCode + @WinNewLine + ' } //eof class ' + @TableName + @WinNewLine
set @strCode = + @strCode + @WinNewLine + ' } //eof namespace  ' + @WinNewLine
 
 
SELECT @strCode 
END TRY        --end try                            
BEGIN CATCH                                  
print ' Error number: ' + CAST(ERROR_NUMBER() AS varchar(100)) +                         
'Error message: ' + ERROR_MESSAGE() + 'Error severity: ' +           
CAST(ERROR_SEVERITY() AS VARCHAR(9)) +                         
'Error state: ' + CAST(ERROR_STATE() AS varchar(100)) +           
'XACT_STATE: ' + CAST(XACT_STATE() AS varchar(100))                                  
END CATCH                                  
 
END --procedure end                                       
/* 
<doc> Generates a C# class base on DataType conversion</doc>
*/
GO
/****** Object:  StoredProcedure [dbo].[Qrtr1Yr]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

create PROCEDURE [dbo].[Qrtr1Yr]
	(--@LDcellID 	[int],
	 
	 @Yr 	[int] )
	

AS
set nocount off

	
	
	Begin
	BEGIN TRAN
DECLARE @i int = 1
 

WHILE (@i < 4)
BEGIN
		Delete M_TRT

	   insert into M_TRT (MNo,MName,MTrt,MPay,MRemain) 
        SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
	SET @i = @i + 1
		if @i=4
		begin

		SELECT  *
		FROM [dbo].[M_TRT]
		order by MNo
		end

END
commit

ROLLBACK

End

GO
/****** Object:  StoredProcedure [dbo].[Qrtr2Yr]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

create PROCEDURE [dbo].[Qrtr2Yr]
	(--@LDcellID 	[int],
	 
	 @Yr 	[int] )
	

AS
set nocount off

	
	
	Begin
	BEGIN TRAN
DECLARE @i int = 4
 

WHILE (@i < 7)
BEGIN
		Delete M_TRT

	   insert into M_TRT (MNo,MName,MTrt,MPay,MRemain) 
        SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
	SET @i = @i + 1
		if @i=7
		begin

		SELECT  *
		FROM [dbo].[M_TRT]
		order by MNo
		end

END
commit

ROLLBACK

End

GO
/****** Object:  StoredProcedure [dbo].[Qrtr3Yr]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

create PROCEDURE [dbo].[Qrtr3Yr]
	(--@LDcellID 	[int],
	 
	 @Yr 	[int] )
	

AS
set nocount off

	
	
	Begin
	BEGIN TRAN
DECLARE @i int = 7
 

WHILE (@i < 10)
BEGIN
		Delete M_TRT

	   insert into M_TRT (MNo,MName,MTrt,MPay,MRemain) 
        SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
	SET @i = @i + 1
		if @i=10
		begin

		SELECT  *
		FROM [dbo].[M_TRT]
		order by MNo
		end

END
commit

ROLLBACK

End

GO
/****** Object:  StoredProcedure [dbo].[Qrtr4Yr]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

create PROCEDURE [dbo].[Qrtr4Yr]
	(--@LDcellID 	[int],
	 
	 @Yr 	[int] )
	

AS
set nocount off

	
	
	Begin
	BEGIN TRAN
DECLARE @i int = 10
 

WHILE (@i < 13)
BEGIN
		Delete M_TRT

	   insert into M_TRT (MNo,MName,MTrt,MPay,MRemain) 
        SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
	SET @i = @i + 1
		if @i=13
		begin

		SELECT  *
		FROM [dbo].[M_TRT]
		order by MNo
		end

END
commit

ROLLBACK

End

GO
/****** Object:  StoredProcedure [dbo].[QrtrAllYr]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

create PROCEDURE [dbo].[QrtrAllYr]
	(--@LDcellID 	[int],
	 
	 @Yr 	[int] )
	

AS
set nocount off

	
	
	Begin
	BEGIN TRAN
DECLARE @i int = 1
 

WHILE (@i < 13)
BEGIN
		Delete M_TRT

	   insert into M_TRT (MNo,MName,MTrt,MPay,MRemain) 
        SELECT      @i,
		(SELECT DATENAME( MONTH, DATEADD( MONTH, @i, -1)) COLLATE Arabic_100_CS_AI_KS_SC_UTF8)  ,
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i  and Year([TrtDate])=@Yr)  ,
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) , 
		(select isnull(sum(PayValue),0) from [Patient_Pays]  where 	month([PayDate])=@i and Year([PayDate])=@Yr) -
		(select isnull(sum(trtValue),0) from [Patient_Trts]  where 	month([TrtDate])=@i and Year([TrtDate])=@Yr)   
			 
	SET @i = @i + 1
		if @i=13
		begin

		SELECT  *
		FROM [dbo].[M_TRT]
		order by MNo
		end

END
commit

ROLLBACK

End

GO
/****** Object:  StoredProcedure [dbo].[RaseedDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RaseedDelete] 
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Raseed]
	WHERE  [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RaseedInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RaseedInsert] 
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Raseed] ([PatientID])
	SELECT @PatientID
	  
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RaseedSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RaseedSelect] 
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PatientID], [Bal] 
	FROM   [dbo].[Raseed] 
	WHERE  ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDDelete] 
    @RDID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[RD]
	WHERE  [RDID] = @RDID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDInsert] 
    @PatientID int,
    @RD1 nvarchar(50) = NULL,
    @RD2 nvarchar(50) = NULL,
    @RD3 nvarchar(50) = NULL,
    @RD4 nvarchar(50) = NULL,
    @RD5 nvarchar(50) = NULL,
    @RD6 nvarchar(50) = NULL,
    @RD7 nvarchar(50) = NULL,
    @RD8 nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[RD] ([PatientID], [RD1], [RD2], [RD3], [RD4], [RD5], [RD6], [RD7], [RD8])
	SELECT @PatientID, @RD1, @RD2, @RD3, @RD4, @RD5, @RD6, @RD7, @RD8
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDPL_IU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RDPL_IU]
  @RDcellID int,
  @PatientID int,
  @CellAddres int,
  @ForeColor nvarchar(50) AS
BEGIN
  IF EXISTS
  (SELECT * FROM dbo.RDPL WHERE RDcellID = @RDcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres)
    UPDATE dbo.RDPL SET
      PatientID = @PatientID,
      CellAddres = @CellAddres,
      ForeColor = @ForeColor
    WHERE RDcellID = @RDcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres
  ELSE
    INSERT INTO dbo.RDPL (
      --RDcellID,
      PatientID,
      CellAddres,
      ForeColor)
    VALUES(
      --@RDcellID,
      @PatientID,
      @CellAddres,
      @ForeColor)
END
GO
/****** Object:  StoredProcedure [dbo].[RDPLDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDPLDelete] 
    @RDcellID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[RDPL]
	WHERE  [RDcellID] = @RDcellID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDPLInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDPLInsert] 
    @PatientID int,
    @CellAddres int,
    @ForeColor nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[RDPL] ([PatientID], [CellAddres], [ForeColor])
	SELECT @PatientID, @CellAddres, @ForeColor
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDPLSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDPLSelect] 
    @RDcellID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RDcellID], [PatientID], [CellAddres], [ForeColor] 
	FROM   [dbo].[RDPL] 
	WHERE  ([RDcellID] = @RDcellID OR @RDcellID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDPLUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDPLUpdate] 
    @RDcellID int,
    @PatientID int,
    @CellAddres int,
    @ForeColor nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[RDPL]
	SET    [CellAddres] = @CellAddres, [ForeColor] = @ForeColor
	WHERE  [RDcellID] = @RDcellID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDSelect] 
    @RDID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RDID], [PatientID], [RD1], [RD2], [RD3], [RD4], [RD5], [RD6], [RD7], [RD8] 
	FROM   [dbo].[RD] 
	WHERE  ([RDID] = @RDID OR @RDID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDSTYLE_IU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RDSTYLE_IU]
  @RDcellID int,
  @PatientID int,
  @CellAddres int,
  @BakImg image AS
BEGIN
  IF EXISTS
  (SELECT * FROM dbo.RDSTYLE WHERE RDcellID = @RDcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres)
    UPDATE dbo.RDSTYLE SET
      PatientID = @PatientID,
      CellAddres = @CellAddres,
      BakImg = @BakImg
    WHERE RDcellID = @RDcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres
  ELSE
    INSERT INTO dbo.RDSTYLE (
      --RDcellID,
      PatientID,
      CellAddres,
      BakImg)
    VALUES(
      --@RDcellID,
      @PatientID,
      @CellAddres,
      @BakImg)
END
GO
/****** Object:  StoredProcedure [dbo].[RDSTYLEDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDSTYLEDelete] 
    @RDcellID  [int],
	 @PatientID 	[int],
	 @CellAdres [int]
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[RDSTYLE]
	WHERE  [RDcellID] = @RDcellID AND [PatientID] = @PatientID AND CellAddres=@CellAdres

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDSTYLEInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDSTYLEInsert] 
    @PatientID int,
    @CellAddres int,
    @BakImg image
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[RDSTYLE] ([PatientID], [CellAddres], [BakImg])
	SELECT @PatientID, @CellAddres, @BakImg
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDSTYLESelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDSTYLESelect] 
    @RDcellID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RDcellID], [PatientID], [CellAddres], [BakImg] 
	FROM   [dbo].[RDSTYLE] 
	WHERE  ([RDcellID] = @RDcellID OR @RDcellID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDSTYLEUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDSTYLEUpdate] 
    @RDcellID int,
    @PatientID int,
    @CellAddres int,
    @BakImg image
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[RDSTYLE]
	SET    [PatientID] = @PatientID, [CellAddres] = @CellAddres, [BakImg] = @BakImg
	WHERE  [RDcellID] = @RDcellID
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RDUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RDUpdate] 
    @RDID int,
    @PatientID int,
    @RD1 nvarchar(50) = NULL,
    @RD2 nvarchar(50) = NULL,
    @RD3 nvarchar(50) = NULL,
    @RD4 nvarchar(50) = NULL,
    @RD5 nvarchar(50) = NULL,
    @RD6 nvarchar(50) = NULL,
    @RD7 nvarchar(50) = NULL,
    @RD8 nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[RD]
	SET    [RD1] = @RD1, [RD2] = @RD2, [RD3] = @RD3, [RD4] = @RD4, [RD5] = @RD5, [RD6] = @RD6, [RD7] = @RD7, [RD8] = @RD8
	WHERE  [RDID] = @RDID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RecordStockMovement]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RecordStockMovement]
    @BatchID INT,
    @QuantityChange INT,
    @MovementType NVARCHAR(20),
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        UPDATE Batchess
        SET CurrentQuantity = CurrentQuantity + @QuantityChange
        WHERE BatchID = @BatchID;

        INSERT INTO StockMovements (BatchID, ProductID, QuantityChange, MovementType, Notes)
        SELECT @BatchID, ProductID, @QuantityChange, @MovementType, @Notes
        FROM Batchess WHERE BatchID = @BatchID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[Rlect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Rlect] 
    @RUID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RUID], [PatientID], [RU1], [RU2], [RU3], [RU4], [RU5], [RU6], [RU7], [RU8] 
	FROM   [dbo].[RU] 
	WHERE  ([RUID] = @RUID OR @RUID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[rsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rsDelete] 
    @UsID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[rs]
	WHERE  [UsID] = @UsID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[rsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rsInsert] 
    @UsName nvarchar(50),
    @UsPass nvarchar(50),
    @UsLvl int,
    @UsGrp nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[rs] ([UsName], [UsPass], [UsLvl], [UsGrp])
	SELECT @UsName, @UsPass, @UsLvl, @UsGrp
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[rsSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rsSelect] 
    @UsID int
AS 
	
	 

	BEGIN TRAN

	SELECT [UsID], [UsName], [UsPass], [UsLvl], [UsGrp] 
	FROM   [dbo].[rs] 
	WHERE  ([UsID] = @UsID OR @UsID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[rsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[rsUpdate] 
    @UsID int,
    @UsName nvarchar(50),
    @UsPass nvarchar(50),
    @UsLvl int,
    @UsGrp nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[rs]
	SET    [UsName] = @UsName, [UsPass] = @UsPass, [UsLvl] = @UsLvl, [UsGrp] = @UsGrp
	WHERE  [UsID] = @UsID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RUDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RUDelete] 
    @RUID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[RU]
	WHERE  [RUID] = @RUID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RUInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RUInsert] 
    @PatientID int,
    @RU1 nvarchar(50) = NULL,
    @RU2 nvarchar(50) = NULL,
    @RU3 nvarchar(50) = NULL,
    @RU4 nvarchar(50) = NULL,
    @RU5 nvarchar(50) = NULL,
    @RU6 nvarchar(50) = NULL,
    @RU7 nvarchar(50) = NULL,
    @RU8 nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[RU] ([PatientID], [RU1], [RU2], [RU3], [RU4], [RU5], [RU6], [RU7], [RU8])
	SELECT @PatientID, @RU1, @RU2, @RU3, @RU4, @RU5, @RU6, @RU7, @RU8
	    
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RUPL_IU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RUPL_IU]
  @RUcellID int,
  @PatientID int,
  @CellAddres int,
  @ForeColor nvarchar(50) AS
BEGIN
  IF EXISTS
  (SELECT * FROM dbo.RUPL WHERE RUcellID = @RUcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres)
    UPDATE dbo.RUPL SET
      PatientID = @PatientID,
      CellAddres = @CellAddres,
      ForeColor = @ForeColor
    WHERE RUcellID = @RUcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres
  ELSE
    INSERT INTO dbo.RUPL (
      --RUcellID,
      PatientID,
      CellAddres,
      ForeColor)
    VALUES(
      --@RUcellID,
      @PatientID,
      @CellAddres,
      @ForeColor)
END
GO
/****** Object:  StoredProcedure [dbo].[RUPLDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RUPLDelete] 
    @RUcellID int,
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[RUPL]
	WHERE  [RUcellID] = @RUcellID
	       AND [PatientID] = @PatientID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RUPLInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RUPLInsert] 
    @PatientID int,
    @CellAddres int,
    @ForeColor nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[RUPL] ([PatientID], [CellAddres], [ForeColor])
	SELECT @PatientID, @CellAddres, @ForeColor
	    
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RUPLSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RUPLSelect] 
    @RUcellID int,
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RUcellID], [PatientID], [CellAddres], [ForeColor] 
	FROM   [dbo].[RUPL] 
	WHERE  ([RUcellID] = @RUcellID OR @RUcellID IS NULL) 
	       AND ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RUPLUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RUPLUpdate] 
    @RUcellID int,
    @PatientID int,
    @CellAddres int,
    @ForeColor nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[RUPL]
	SET    [CellAddres] = @CellAddres, [ForeColor] = @ForeColor
	WHERE  [RUcellID] = @RUcellID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RUSTYLE_IU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RUSTYLE_IU]
  @RUcellID int,
  @PatientID int,
  @CellAddres int,
  @BakImg image AS
BEGIN
  IF EXISTS
  (SELECT * FROM dbo.RUSTYLE WHERE RUcellID = @RUcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres)
    UPDATE dbo.RUSTYLE SET
      PatientID = @PatientID,
      CellAddres = @CellAddres,
      BakImg = @BakImg
    WHERE RUcellID = @RUcellID AND  PatientID = @PatientID AND CellAddres = @CellAddres
  ELSE
    INSERT INTO dbo.RUSTYLE (
      --RUcellID,
      PatientID,
      CellAddres,
      BakImg)
    VALUES(
      --@RUcellID,
      @PatientID,
      @CellAddres,
      @BakImg)
END
GO
/****** Object:  StoredProcedure [dbo].[RUSTYLEDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RUSTYLEDelete] 
    @RUcellID  [int],
	 @PatientID 	[int],
	 @CellAdres [int]
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[RUSTYLE]
	WHERE  [RUcellID] = @RUcellID AND [PatientID] = @PatientID AND CellAddres=@CellAdres

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RUSTYLEInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RUSTYLEInsert] 
    @PatientID int,
    @CellAddres int,
    @BakImg image
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[RUSTYLE] ([PatientID], [CellAddres], [BakImg])
	SELECT @PatientID, @CellAddres, @BakImg
	 
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RUSTYLESelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RUSTYLESelect] 
    @RUcellID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RUcellID], [PatientID], [CellAddres], [BakImg] 
	FROM   [dbo].[RUSTYLE] 
	WHERE  ([RUcellID] = @RUcellID OR @RUcellID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RUSTYLEUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RUSTYLEUpdate] 
    @RUcellID int,
    @PatientID int,
    @CellAddres int,
    @BakImg image
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[RUSTYLE]
	SET    [PatientID] = @PatientID, [CellAddres] = @CellAddres, [BakImg] = @BakImg
	WHERE  [RUcellID] = @RUcellID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RUUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RUUpdate] 
    @RUID int,
    @PatientID int,
    @RU1 nvarchar(50) = NULL,
    @RU2 nvarchar(50) = NULL,
    @RU3 nvarchar(50) = NULL,
    @RU4 nvarchar(50) = NULL,
    @RU5 nvarchar(50) = NULL,
    @RU6 nvarchar(50) = NULL,
    @RU7 nvarchar(50) = NULL,
    @RU8 nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[RU]
	SET    [RU1] = @RU1, [RU2] = @RU2, [RU3] = @RU3, [RU4] = @RU4, [RU5] = @RU5, [RU6] = @RU6, [RU7] = @RU7, [RU8] = @RU8
	WHERE  [RUID] = @RUID
	       AND [PatientID] = @PatientID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RxFlyDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RxFlyDelete] 
    @RxID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[RxFly]
	WHERE  [RxID] = @RxID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RxFlyInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RxFlyInsert] 
    @PatientName nvarchar(50) = NULL,
    @PatientAge int = NULL,
    @RxDate smalldatetime = NULL,
    @RX nvarchar(500) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[RxFly] ([PatientName], [PatientAge], [RxDate], [RX])
	SELECT @PatientName, @PatientAge, @RxDate, @RX
	    
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RxFlySelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RxFlySelect] 
    @RxID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RxID], [PatientName], [PatientAge], [RxDate], [RX] 
	FROM   [dbo].[RxFly] 
	WHERE  ([RxID] = @RxID OR @RxID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[RxFlyUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RxFlyUpdate] 
    @RxID int,
    @PatientName nvarchar(50) = NULL,
    @PatientAge int = NULL,
    @RxDate smalldatetime = NULL,
    @RX nvarchar(500) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[RxFly]
	SET    [PatientName] = @PatientName, [PatientAge] = @PatientAge, [RxDate] = @RxDate, [RX] = @RX
	WHERE  [RxID] = @RxID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[select_LDSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





create PROCEDURE [dbo].[select_LDSTYLE]
	(--@LDcellID 	[int],
	 @PatientID 	[int],
	 @CellAddres 	[int] )
	

AS
set nocount off

	
	
	Begin
	--set @LDcellID=(select LDcellID from inserted)
	select BakImg from  LDSTYLE  where 
	                          (PatientID= @PatientID  and       CellAddres = @CellAddres )-- and  LDcellID=@LDcellID )
	     
	
	End



GO
/****** Object:  StoredProcedure [dbo].[select_LUSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





create PROCEDURE [dbo].[select_LUSTYLE]
	(--@LDcellID 	[int],
	 @PatientID 	[int],
	 @CellAddres 	[int] )
	

AS
set nocount off

	
	
	Begin
	--set @LDcellID=(select LDcellID from inserted)
	select BakImg from  LUSTYLE  where 
	                          (PatientID= @PatientID  and       CellAddres = @CellAddres )-- and  LDcellID=@LDcellID )
	     
	
	End



GO
/****** Object:  StoredProcedure [dbo].[select_RDSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





create PROCEDURE [dbo].[select_RDSTYLE]
	(--@LDcellID 	[int],
	 @PatientID 	[int],
	 @CellAddres 	[int] )
	

AS
set nocount off

	
	Begin
	--set @LDcellID=(select LDcellID from inserted)
	select BakImg from  RDSTYLE  where 
	                          (PatientID= @PatientID  and       CellAddres = @CellAddres )-- and  LDcellID=@LDcellID )
	     
	
	End



GO
/****** Object:  StoredProcedure [dbo].[select_RUSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





create PROCEDURE [dbo].[select_RUSTYLE]
	(--@LDcellID 	[int],
	 @PatientID 	[int],
	 @CellAddres 	[int] )
	

AS
set nocount off

	
	
	Begin
	--set @LDcellID=(select LDcellID from inserted)
	select BakImg from  RUSTYLE  where 
	                          (PatientID= @PatientID  and       CellAddres = @CellAddres )-- and  LDcellID=@LDcellID )
	     
	
	End



GO
/****** Object:  StoredProcedure [dbo].[SelectAll_LD]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectAll_LD] AS
BEGIN
SELECT * FROM LD
END
GO
/****** Object:  StoredProcedure [dbo].[SelectOne_LD]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectOne_LD] (
@LDID int, @PatientID int)
AS
BEGIN
   SELECT * FROM LD WHERE LDID = @LDID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_AddLoan]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =========================================
-- Procedure: Add Loan
-- =========================================
CREATE PROCEDURE [dbo].[sp_AddLoan]
    @ContactID INT,
    @Amount DECIMAL(10,2),
    @Direction NVARCHAR(10),
    @Description NVARCHAR(255) = NULL,
    @LoanDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @LoanDate IS NULL
        SET @LoanDate = CONVERT(DATE, GETDATE());

    INSERT INTO Loans (ContactID, Amount, Direction, Description, LoanDate)
    VALUES (@ContactID, @Amount, @Direction, @Description, @LoanDate);

    SELECT SCOPE_IDENTITY() AS NewLoanID;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_AddRepayment]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =========================================
-- Procedure: Add Repayment
-- =========================================
CREATE PROCEDURE [dbo].[sp_AddRepayment]
    @LoanID INT,
    @Amount DECIMAL(10,2),
    @Notes NVARCHAR(255) = NULL,
    @RepaymentDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @RepaymentDate IS NULL
        SET @RepaymentDate = CONVERT(DATE, GETDATE());

    INSERT INTO Repayments (LoanID, Amount, Notes, RepaymentDate)
    VALUES (@LoanID, @Amount, @Notes, @RepaymentDate);

    SELECT SCOPE_IDENTITY() AS NewRepaymentID;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_AddTransaction]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create a stored procedure for adding transactions
CREATE PROCEDURE [dbo].[sp_AddTransaction]
    @ContactID INT,
    @Amount DECIMAL(10,2),
    @TransactionType NVARCHAR(20),
    @Description NVARCHAR(255) = NULL,
    @TransactionDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @TransactionDate IS NULL
        SET @TransactionDate = CONVERT(DATE, GETDATE());
    
    INSERT INTO Transactions (ContactID, Amount, TransactionType, Description, TransactionDate)
    VALUES (@ContactID, @Amount, @TransactionType, @Description, @TransactionDate);
    
    SELECT SCOPE_IDENTITY() AS NewTransactionID;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetContactTransactions]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--CREATE PROCEDURE sp_GetContactTransactions
--    @ContactID INT
--AS
--BEGIN
--    SELECT 
--        TransactionID,
--        TransactionType,
--        Amount,
--        TransactionDate,
--        Description,
--        CreatedAt
--    FROM dbo.Transactions
--    WHERE ContactID = @ContactID
--    ORDER BY TransactionDate DESC, TransactionID DESC
--END
--GO
CREATE PROCEDURE [dbo].[sp_GetContactTransactions]
    @ContactID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.TransactionID,
        t.TransactionType,
        t.Amount,
        t.TransactionDate,
        t.Description,
        t.CreatedAt,
        b.Balance  -- include balance from Balances table
    FROM Transactions t
    INNER JOIN Balances b ON t.ContactID = b.ContactID
    WHERE t.ContactID = @ContactID
    ORDER BY t.TransactionDate
END
GO
/****** Object:  StoredProcedure [dbo].[SurgeryDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurgeryDelete] 
    @SurgID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Surgery]
	WHERE  [SurgID] = @SurgID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[SurgeryInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurgeryInsert] 
    @PatientID int,
    @SurgeryDet nvarchar(50) = NULL,
    @SurDate smalldatetime = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Surgery] ([PatientID], [SurgeryDet], [SurDate])
	SELECT @PatientID, @SurgeryDet, @SurDate
	 
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[SurgerySelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurgerySelect] 
    @SurgID int
AS 
	
	 

	BEGIN TRAN

	SELECT [SurgID], [PatientID], [SurgeryDet], [SurDate] 
	FROM   [dbo].[Surgery] 
	WHERE  ([SurgID] = @SurgID OR @SurgID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[SurgeryUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurgeryUpdate] 
    @SurgID int,
    @PatientID int,
    @SurgeryDet nvarchar(50) = NULL,
    @SurDate smalldatetime = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Surgery]
	SET    [PatientID] = @PatientID, [SurgeryDet] = @SurgeryDet, [SurDate] = @SurDate
	WHERE  [SurgID] = @SurgID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblBnodMsareefDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblBnodMsareefDelete] 
    @BandID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblBnodMsareef]
	WHERE  [BandID] = @BandID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblBnodMsareefInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblBnodMsareefInsert] 
    @BandName nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblBnodMsareef] ([BandName])
	SELECT @BandName
	   
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblBnodMsareefSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblBnodMsareefSelect] 
    @BandID int
AS 
	
	 

	BEGIN TRAN

	SELECT [BandID], [BandName] 
	FROM   [dbo].[TblBnodMsareef] 
	WHERE  ([BandID] = @BandID OR @BandID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblBnodMsareefUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblBnodMsareefUpdate] 
    @BandID int,
    @BandName nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblBnodMsareef]
	SET    [BandName] = @BandName
	WHERE  [BandID] = @BandID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCategoriesDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCategoriesDelete] 
    @CategoryID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblCategories]
	WHERE  [CategoryID] = @CategoryID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCategoriesInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCategoriesInsert] 
    @CategoryName nvarchar(75) = NULL,
    @ParentCategory int = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblCategories] ([CategoryName], [ParentCategory])
	SELECT @CategoryName, @ParentCategory
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCategoriesSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCategoriesSelect] 
    @CategoryID int
AS 
	
	 

	BEGIN TRAN

	SELECT [CategoryID], [CategoryName], [ParentCategory] 
	FROM   [dbo].[TblCategories] 
	WHERE  ([CategoryID] = @CategoryID OR @CategoryID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCategoriesUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCategoriesUpdate] 
    @CategoryID int,
    @CategoryName nvarchar(75) = NULL,
    @ParentCategory int = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblCategories]
	SET    [CategoryName] = @CategoryName, [ParentCategory] = @ParentCategory
	WHERE  [CategoryID] = @CategoryID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCitiesDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCitiesDelete] 
    @CityID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblCities]
	WHERE  [CityID] = @CityID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCitiesInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCitiesInsert] 
    @CityName nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblCities] ([CityName])
	SELECT @CityName
	   
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCitiesSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCitiesSelect] 
    @CityID int
AS 
	
	 

	BEGIN TRAN

	SELECT [CityID], [CityName] 
	FROM   [dbo].[TblCities] 
	WHERE  ([CityID] = @CityID OR @CityID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCitiesUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCitiesUpdate] 
    @CityID int,
    @CityName nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblCities]
	SET    [CityName] = @CityName
	WHERE  [CityID] = @CityID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCustomersDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCustomersDelete] 
    @CusID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblCustomers]
	WHERE  [CusID] = @CusID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCustomersInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCustomersInsert] 
    @CusName nvarchar(75) = NULL,
    @CityID int = NULL,
    @Address nvarchar(500) = NULL,
    @Contacts nvarchar(500) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblCustomers] ([CusName], [CityID], [Address], [Contacts])
	SELECT @CusName, @CityID, @Address, @Contacts
	    
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCustomersSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCustomersSelect] 
    @CusID int
AS 
	
	 

	BEGIN TRAN

	SELECT [CusID], [CusName], [CityID], [Address], [Contacts] 
	FROM   [dbo].[TblCustomers] 
	WHERE  ([CusID] = @CusID OR @CusID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblCustomersUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblCustomersUpdate] 
    @CusID int,
    @CusName nvarchar(75) = NULL,
    @CityID int = NULL,
    @Address nvarchar(500) = NULL,
    @Contacts nvarchar(500) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblCustomers]
	SET    [CusName] = @CusName, [CityID] = @CityID, [Address] = @Address, [Contacts] = @Contacts
	WHERE  [CusID] = @CusID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblExpenPayInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TblExpenPayInsert] (
@MasrofID int, @PayValue money, @PayDate datetime, @Notes nvarchar(50)
) AS BEGIN
INSERT INTO TblExpenPay (MasrofID, PayValue, PayDate, Notes)
VALUES (@MasrofID, @PayValue, @PayDate, @Notes)
END
GO
/****** Object:  StoredProcedure [dbo].[TblInvoiceBodyDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvoiceBodyDelete] 
    @InvID int,
    @ItemID int
AS 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblInvoiceBody]
	WHERE  [InvID] = @InvID
	       AND [ItemID] = @ItemID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblInvoiceBodyInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvoiceBodyInsert] 
    @InvID int,
    @ItemID int,
    @Quantity float = NULL,
    @Price money = NULL,
    @ItemHasm money = NULL,
    @Note nvarchar(100) = NULL
AS 

	BEGIN TRAN
	
	INSERT INTO [dbo].[TblInvoiceBody] ([InvID], [ItemID], [Quantity], [Price], [ItemHasm], [Note])
	SELECT @InvID, @ItemID, @Quantity, @Price, @ItemHasm, @Note
	
	
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblInvoiceBodySelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvoiceBodySelect] 
    @InvID int,
    @ItemID int
AS 
	
	BEGIN TRAN

	SELECT [InvID], [ItemID], [Quantity], [Price], [ItemHasm], [Note], [ItemNet], [BdyNet] 
	FROM   [dbo].[TblInvoiceBody] 
	WHERE  ([InvID] = @InvID OR @InvID IS NULL) 
	       AND ([ItemID] = @ItemID OR @ItemID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblInvoiceBodyUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvoiceBodyUpdate] 
    @InvID int,
    @ItemID int,
    @Quantity float = NULL,
    @Price money = NULL,
    @ItemHasm money = NULL,
    @Note nvarchar(100) = NULL
AS 
	
	
	BEGIN TRAN

	UPDATE [dbo].[TblInvoiceBody]
	SET    [Quantity] = @Quantity, [Price] = @Price, [ItemHasm] = @ItemHasm, [Note] = @Note
	WHERE  [InvID] = @InvID
	       AND [ItemID] = @ItemID
	

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblInvoicesHeaderDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvoicesHeaderDelete] 
    @InvoiceID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblInvoicesHeader]
	WHERE  [InvoiceID] = @InvoiceID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblInvoicesHeaderInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvoicesHeaderInsert] 
    @InvoiceType tinyint = NULL,
    @InvoiceDate smalldatetime = NULL,
    @ResID int = NULL,
    @DocNo int = NULL,
    @InvoiceEx nvarchar(500) = NULL,
    @Hasm money = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblInvoicesHeader] ([InvoiceType], [InvoiceDate], [ResID], [DocNo], [InvoiceEx], [Hasm])
	SELECT @InvoiceType, @InvoiceDate, @ResID, @DocNo, @InvoiceEx, @Hasm
	  
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblInvoicesHeaderSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvoicesHeaderSelect] 
    @InvoiceID int
AS 
	
	 

	BEGIN TRAN

	SELECT [InvoiceID], [InvoiceType], [InvoiceDate], [ResID], [DocNo], [InvoiceEx], [Hasm], [InvTotlQuantItms], [InvTotlPriceItms], [InvTotlDiscItms], [InvTotlDisc], [InvoiceNet] 
	FROM   [dbo].[TblInvoicesHeader] 
	WHERE  ([InvoiceID] = @InvoiceID OR @InvoiceID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblInvoicesHeaderUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvoicesHeaderUpdate] 
    @InvoiceID int,
    @InvoiceType tinyint = NULL,
    @InvoiceDate smalldatetime = NULL,
    @ResID int = NULL,
    @DocNo int = NULL,
    @InvoiceEx nvarchar(500) = NULL,
    @Hasm money = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblInvoicesHeader]
	SET    [InvoiceType] = @InvoiceType, [InvoiceDate] = @InvoiceDate, [ResID] = @ResID, [DocNo] = @DocNo, [InvoiceEx] = @InvoiceEx, [Hasm] = @Hasm
	WHERE  [InvoiceID] = @InvoiceID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblInvPayDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvPayDelete] 
    @PayID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblInvPay]
	WHERE  [PayID] = @PayID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblInvPayInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvPayInsert] 
    @InvoiceID int,
    @ResID int,
    @PayDate datetime,
    @Amount money,
    @Notes nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblInvPay] ([InvoiceID], [ResID], [PayDate], [Amount], [Notes])
	SELECT @InvoiceID, @ResID, @PayDate, @Amount, @Notes
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblInvPaySelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvPaySelect] 
    @PayID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PayID], [InvoiceID], [ResID], [PayDate], [Amount], [Notes], [InvRemain] 
	FROM   [dbo].[TblInvPay] 
	WHERE  ([PayID] = @PayID OR @PayID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblInvPayUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblInvPayUpdate] 
    @PayID int,
    @InvoiceID int,
    @ResID int,
    @PayDate datetime,
    @Amount money,
    @Notes nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblInvPay]
	SET    [InvoiceID] = @InvoiceID, [ResID] = @ResID, [PayDate] = @PayDate, [Amount] = @Amount, [Notes] = @Notes
	WHERE  [PayID] = @PayID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblItemsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblItemsDelete] 
    @ItemID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblItems]
	WHERE  [ItemID] = @ItemID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblItemsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblItemsInsert] 
    @ItemName nvarchar(75) = NULL,
    @ItemEx nvarchar(500) = NULL,
    @CatID int = NULL,
    @UnitID int = NULL,
    @LastPrice money = NULL,
    @QuantityNow float = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblItems] ([ItemName], [ItemEx], [CatID], [UnitID], [LastPrice], [QuantityNow])
	SELECT @ItemName, @ItemEx, @CatID, @UnitID, @LastPrice, @QuantityNow
	  
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblItemsSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblItemsSelect] 
    @ItemID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ItemID], [ItemName], [ItemEx], [CatID], [UnitID], [LastPrice], [QuantityNow] 
	FROM   [dbo].[TblItems] 
	WHERE  ([ItemID] = @ItemID OR @ItemID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblItemsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblItemsUpdate] 
    @ItemID int,
    @ItemName nvarchar(75) = NULL,
    @ItemEx nvarchar(500) = NULL,
    @CatID int = NULL,
    @UnitID int = NULL,
    @LastPrice money = NULL,
    @QuantityNow float = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblItems]
	SET    [ItemName] = @ItemName, [ItemEx] = @ItemEx, [CatID] = @CatID, [UnitID] = @UnitID, [LastPrice] = @LastPrice, [QuantityNow] = @QuantityNow
	WHERE  [ItemID] = @ItemID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblMasareefDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblMasareefDelete] 
    @MasrofID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblMasareef]
	WHERE  [MasrofID] = @MasrofID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblMasareefInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblMasareefInsert] 
    @MasrofDate smalldatetime = NULL,
    @BandID int = NULL,
    @MasrofAmount money = NULL,
    @MasrofEx nvarchar(500) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblMasareef] ([MasrofDate], [BandID], [MasrofAmount], [MasrofEx])
	SELECT @MasrofDate, @BandID, @MasrofAmount, @MasrofEx
	     
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblMasareefSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblMasareefSelect] 
    @MasrofID int
AS 
	
	 

	BEGIN TRAN

	SELECT [MasrofID], [MasrofDate], [BandID], [MasrofAmount], [MasrofEx] 
	FROM   [dbo].[TblMasareef] 
	WHERE  ([MasrofID] = @MasrofID OR @MasrofID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblMasareefUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblMasareefUpdate] 
    @MasrofID int,
    @MasrofDate smalldatetime = NULL,
    @BandID int = NULL,
    @MasrofAmount money = NULL,
    @MasrofEx nvarchar(500) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblMasareef]
	SET    [MasrofDate] = @MasrofDate, [BandID] = @BandID, [MasrofAmount] = @MasrofAmount, [MasrofEx] = @MasrofEx
	WHERE  [MasrofID] = @MasrofID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblMeasureDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblMeasureDelete] 
    @MeasureID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblMeasure]
	WHERE  [MeasureID] = @MeasureID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblMeasureInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblMeasureInsert] 
    @Measure nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblMeasure] ([Measure])
	SELECT @Measure
	     
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblMeasureSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblMeasureSelect] 
    @MeasureID int
AS 
	
	 

	BEGIN TRAN

	SELECT [MeasureID], [Measure] 
	FROM   [dbo].[TblMeasure] 
	WHERE  ([MeasureID] = @MeasureID OR @MeasureID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblMeasureUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblMeasureUpdate] 
    @MeasureID int,
    @Measure nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblMeasure]
	SET    [Measure] = @Measure
	WHERE  [MeasureID] = @MeasureID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblPaidsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblPaidsDelete] 
    @PayID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblPaids]
	WHERE  [PayID] = @PayID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblPaidsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblPaidsInsert] 
    @PayType nvarchar(5) = NULL,
    @PayDate smalldatetime = NULL,
    @ResCusId int = NULL,
    @PayAmount money = NULL,
    @PayEx nvarchar(300) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblPaids] ([PayType], [PayDate], [ResCusId], [PayAmount], [PayEx])
	SELECT @PayType, @PayDate, @ResCusId, @PayAmount, @PayEx
	 
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblPaidsSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblPaidsSelect] 
    @PayID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PayID], [PayType], [PayDate], [ResCusId], [PayAmount], [PayEx] 
	FROM   [dbo].[TblPaids] 
	WHERE  ([PayID] = @PayID OR @PayID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblPaidsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblPaidsUpdate] 
    @PayID int,
    @PayType nvarchar(5) = NULL,
    @PayDate smalldatetime = NULL,
    @ResCusId int = NULL,
    @PayAmount money = NULL,
    @PayEx nvarchar(300) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblPaids]
	SET    [PayType] = @PayType, [PayDate] = @PayDate, [ResCusId] = @ResCusId, [PayAmount] = @PayAmount, [PayEx] = @PayEx
	WHERE  [PayID] = @PayID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblResourcesDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblResourcesDelete] 
    @ResID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblResources]
	WHERE  [ResID] = @ResID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblResourcesInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblResourcesInsert] 
    @ResName nvarchar(75) = NULL,
    @CityID int = NULL,
    @Address nvarchar(500) = NULL,
    @Contacts nvarchar(500) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblResources] ([ResName], [CityID], [Address], [Contacts])
	SELECT @ResName, @CityID, @Address, @Contacts
	  
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblResourcesSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblResourcesSelect] 
    @ResID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ResID], [ResName], [CityID], [Address], [Contacts], [ResInvsNet], [ResTotalPays], [ResBal] 
	FROM   [dbo].[TblResources] 
	WHERE  ([ResID] = @ResID OR @ResID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblResourcesUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblResourcesUpdate] 
    @ResID int,
    @ResName nvarchar(75) = NULL,
    @CityID int = NULL,
    @Address nvarchar(500) = NULL,
    @Contacts nvarchar(500) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblResources]
	SET    [ResName] = @ResName, [CityID] = @CityID, [Address] = @Address, [Contacts] = @Contacts
	WHERE  [ResID] = @ResID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblSalesHeaderDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblSalesHeaderDelete] 
    @SaleID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblSalesHeader]
	WHERE  [SaleID] = @SaleID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblSalesHeaderInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblSalesHeaderInsert] 
    @SaleType tinyint = NULL,
    @SaleDate smalldatetime = NULL,
    @CusID int = NULL,
    @DocNo int = NULL,
    @SaleEx nvarchar(500) = NULL,
    @Hasm money = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblSalesHeader] ([SaleType], [SaleDate], [CusID], [DocNo], [SaleEx], [Hasm])
	SELECT @SaleType, @SaleDate, @CusID, @DocNo, @SaleEx, @Hasm
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblSalesHeaderSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblSalesHeaderSelect] 
    @SaleID int
AS 
	
	 

	BEGIN TRAN

	SELECT [SaleID], [SaleType], [SaleDate], [CusID], [DocNo], [SaleEx], [Hasm] 
	FROM   [dbo].[TblSalesHeader] 
	WHERE  ([SaleID] = @SaleID OR @SaleID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblSalesHeaderUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblSalesHeaderUpdate] 
    @SaleID int,
    @SaleType tinyint = NULL,
    @SaleDate smalldatetime = NULL,
    @CusID int = NULL,
    @DocNo int = NULL,
    @SaleEx nvarchar(500) = NULL,
    @Hasm money = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblSalesHeader]
	SET    [SaleType] = @SaleType, [SaleDate] = @SaleDate, [CusID] = @CusID, [DocNo] = @DocNo, [SaleEx] = @SaleEx, [Hasm] = @Hasm
	WHERE  [SaleID] = @SaleID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblTRTDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblTRTDelete] 
    @TrtID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblTRT]
	WHERE  [TrtID] = @TrtID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblTRTInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblTRTInsert] 
    @Trt nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblTRT] ([Trt])
	SELECT @Trt
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblTRTSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblTRTSelect] 
    @TrtID int
AS 
	
	 

	BEGIN TRAN

	SELECT [TrtID], [Trt] 
	FROM   [dbo].[TblTRT] 
	WHERE  ([TrtID] = @TrtID OR @TrtID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblTRTUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblTRTUpdate] 
    @TrtID int,
    @Trt nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblTRT]
	SET    [Trt] = @Trt
	WHERE  [TrtID] = @TrtID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblUnitsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblUnitsDelete] 
    @UnitID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblUnits]
	WHERE  [UnitID] = @UnitID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblUnitsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblUnitsInsert] 
    @UnitName nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblUnits] ([UnitName])
	SELECT @UnitName
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblUnitsSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblUnitsSelect] 
    @UnitID int
AS 
	
	 

	BEGIN TRAN

	SELECT [UnitID], [UnitName] 
	FROM   [dbo].[TblUnits] 
	WHERE  ([UnitID] = @UnitID OR @UnitID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblUnitsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblUnitsUpdate] 
    @UnitID int,
    @UnitName nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblUnits]
	SET    [UnitName] = @UnitName
	WHERE  [UnitID] = @UnitID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblWireTypeDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblWireTypeDelete] 
    @TypeID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblWireType]
	WHERE  [TypeID] = @TypeID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblWireTypeInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblWireTypeInsert] 
    @WireType nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblWireType] ([WireType])
	SELECT @WireType
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblWireTypeSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblWireTypeSelect] 
    @TypeID int
AS 
	
	 

	BEGIN TRAN

	SELECT [TypeID], [WireType] 
	FROM   [dbo].[TblWireType] 
	WHERE  ([TypeID] = @TypeID OR @TypeID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TblWireTypeUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TblWireTypeUpdate] 
    @TypeID int,
    @WireType nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[TblWireType]
	SET    [WireType] = @WireType
	WHERE  [TypeID] = @TypeID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[TrtsPaysList]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TrtsPaysList]
@PatientID INT    
AS
BEGIN
    /*****************************************************************
     * Time: 30/07/2024 8:09:04 PM
     * Author: Pascal
     * Comments: This procedure was generated using SQL Assistant's 
     * Refactoring -> Extract Procedure feature.
     *****************************************************************/
 
  
    
    WITH CombinedTransactions AS (
             SELECT [PatientID],
                    [TrtDate]           AS TransDate,
                    N'علاج: ' + [Detail]  AS Details,
                    [TrtValue]          AS VALUE
             FROM   [dbo].[Patient_Trts]
             WHERE  [PatientID] = @PatientID
             
             UNION ALL
             
             SELECT [PatientID],
                    [PayDate]          AS TransDate,
                    N'دفعة: ' + [Notes]  AS Details,
                    -[PayValue]        AS VALUE -- Negative to deduct from balance
             FROM   [dbo].[Patient_Pays]
             WHERE  [PatientID] = @PatientID
         )
         , SortedTransactions AS (
             SELECT [PatientID],
                    TransDate,
                    Details,
                    VALUE,
                    ROW_NUMBER() OVER(ORDER BY TransDate, VALUE DESC) AS RowNum
             FROM   CombinedTransactions
         )
    
    SELECT st.[PatientID],
           st.TransDate,
           st.Details,
           st.Value,
           SUM(st.Value) OVER(PARTITION BY st.[PatientID] ORDER BY st.RowNum) AS Balance
    FROM   SortedTransactions st
    ORDER BY
           st.TransDate,
           st.RowNum;
    

    RETURN @@ERROR
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Appointments]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Appointments] (
@Type int,
@StartDate datetime,
@EndDate datetime,
@QueryStartDate datetime,
@QueryEndDate datetime,
@AllDay bit,
@Subject nvarchar(50),
@Location nvarchar(50),
@Description nvarchar(MAX),
@Status int,
@Label int,
@ResourceID int,
@ResourceIDs nvarchar(MAX),
@ReminderInfo nvarchar(MAX),
@RecurrenceInfo nvarchar(MAX),
@TimeZoneId nvarchar(MAX),
@CustomField1 nvarchar(MAX),
@PatientID int,
@UniqueID int
) AS BEGIN
    UPDATE Appointments SET Type = @Type, StartDate = @StartDate, EndDate = @EndDate, QueryStartDate = @QueryStartDate, QueryEndDate = @QueryEndDate, AllDay = @AllDay, Subject = @Subject, Location = @Location, Description = @Description, Status = @Status, Label = @Label, ResourceID = @ResourceID, ResourceIDs = @ResourceIDs, ReminderInfo = @ReminderInfo, RecurrenceInfo = @RecurrenceInfo, TimeZoneId = @TimeZoneId, CustomField1 = @CustomField1, PatientID = @PatientID
    WHERE UniqueID = @UniqueID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_DrWork]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_DrWork] (
@DrID int,
@PatientID int,
@WrkDate datetime,
@WrkDetail nvarchar(50),
@WrkVal money,
@PayVal money,
@Imp bit,
@Orth bit,
@Surg bit,
@Notes nvarchar(50),
@WorkID int
) AS BEGIN
    UPDATE DrWork SET DrID = @DrID, PatientID = @PatientID, WrkDate = @WrkDate, WrkDetail = @WrkDetail, WrkVal = @WrkVal, PayVal = @PayVal, Imp = @Imp, Orth = @Orth, Surg = @Surg, Notes = @Notes
    WHERE WorkID = @WorkID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_DrWorkPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_DrWorkPay] (
@WorkID int,
@DrID int,
@PayValue money,
@PayDate datetime,
@Notes nvarchar(50),
@WorkPayID int
) AS BEGIN
    UPDATE DrWorkPay SET WorkID = @WorkID, DrID = @DrID, PayValue = @PayValue, PayDate = @PayDate, Notes = @Notes
    WHERE WorkPayID = @WorkPayID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Emp]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Emp] (
@EmpName nvarchar(50),
@EmpPhone nchar,
@EmpAddress nvarchar(50),
@EmpImg image,
@EmpID int
) AS BEGIN
    UPDATE Emp SET EmpName = @EmpName, EmpPhone = @EmpPhone, EmpAddress = @EmpAddress, EmpImg = @EmpImg
    WHERE EmpID = @EmpID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_EmpAtend]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_EmpAtend] (
@EmpID int,
@AtnDay datetime,
@AtnNote nvarchar(50),
@AbsPrsnt bit,
@AtndID int
) AS BEGIN
    UPDATE EmpAtend SET EmpID = @EmpID, AtnDay = @AtnDay, AtnNote = @AtnNote, AbsPrsnt = @AbsPrsnt
    WHERE AtndID = @AtndID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_EmpPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_EmpPay] (
@EmpID int,
@MonthPay int,
@DayPay int,
@FromDT datetime,
@ToDT datetime,
@DaysCount int,
@PayDate datetime,
@PayNote nvarchar(50),
@UsSalID int
) AS BEGIN
    UPDATE EmpPay SET EmpID = @EmpID, MonthPay = @MonthPay, DayPay = @DayPay, FromDT = @FromDT, ToDT = @ToDT, DaysCount = @DaysCount, PayDate = @PayDate, PayNote = @PayNote
    WHERE UsSalID = @UsSalID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Gender]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Gender] (
@Sex nvarchar(50),
@SID int
) AS BEGIN
    UPDATE Gender SET Sex = @Sex
    WHERE SID = @SID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Health]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Health] (
@HealthStat nvarchar(100),
@HID int
) AS BEGIN
    UPDATE Health SET HealthStat = @HealthStat
    WHERE HID = @HID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Imags]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Imags] (
@IMG varbinary,
@Height float,
@Width float,
@Sze float,
@DatePictureTaken datetime,
@EquipmentMaker varchar(150),
@EquipmentModel varchar(150),
@Thumbnail varbinary,
@DateCreated datetime,
@DateModified datetime,
@ImageID int
) AS BEGIN
    UPDATE Imags SET IMG = @IMG, Height = @Height, Width = @Width, Sze = @Sze, DatePictureTaken = @DatePictureTaken, EquipmentMaker = @EquipmentMaker, EquipmentModel = @EquipmentModel, Thumbnail = @Thumbnail, DateCreated = @DateCreated, DateModified = @DateModified
    WHERE ImageID = @ImageID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_ImpClrs]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_ImpClrs] (
@ImpClr nvarchar(50),
@ImpClrID int
) AS BEGIN
    UPDATE ImpClrs SET ImpClr = @ImpClr
    WHERE ImpClrID = @ImpClrID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_ImprDet]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_ImprDet] (
@imprID int,
@ImprDetail nvarchar(50),
@ImpDetID int
) AS BEGIN
    UPDATE ImprDet SET imprID = @imprID, ImprDetail = @ImprDetail
    WHERE ImpDetID = @ImpDetID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Impression]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Impression] (
@ImprType nvarchar(50),
@ImprID int
) AS BEGIN
    UPDATE Impression SET ImprType = @ImprType
    WHERE ImprID = @ImprID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Lab]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Lab] (
@LabName nvarchar(50),
@Adres nvarchar(50),
@Phone nchar,
@Mobile nchar,
@LabID int
) AS BEGIN
    UPDATE Lab SET LabName = @LabName, Adres = @Adres, Phone = @Phone, Mobile = @Mobile
    WHERE LabID = @LabID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_LabOrder]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_LabOrder] (
@LabID int,
@PatientID int,
@ImprType nvarchar(50),
@ImprDet nvarchar(50),
@ImprClr nvarchar(50),
@ImprCount int,
@DeliveryDate datetime,
@Price int,
@RecieveDate datetime,
@Notes nvarchar(50),
@LabOrderID int
) AS BEGIN
    UPDATE LabOrder SET LabID = @LabID, PatientID = @PatientID, ImprType = @ImprType, ImprDet = @ImprDet, ImprClr = @ImprClr, ImprCount = @ImprCount, DeliveryDate = @DeliveryDate, Price = @Price, RecieveDate = @RecieveDate, Notes = @Notes
    WHERE LabOrderID = @LabOrderID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_LabPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_LabPay] (
@LabID int,
@LabOrderID int,
@PayValue int,
@PayDate datetime,
@PayDetail nvarchar(50),
@Notes nvarchar(50),
@LabPayID int
) AS BEGIN
    UPDATE LabPay SET LabID = @LabID, LabOrderID = @LabOrderID, PayValue = @PayValue, PayDate = @PayDate, PayDetail = @PayDetail, Notes = @Notes
    WHERE LabPayID = @LabPayID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_LD]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_LD] (
@PatientID int,
@LD1 nvarchar(50),
@LD2 nvarchar(50),
@LD3 nvarchar(50),
@LD4 nvarchar(50),
@LD5 nvarchar(50),
@LD6 nvarchar(50),
@LD7 nvarchar(50),
@LD8 nvarchar(50),
@LDID int
) AS BEGIN
    UPDATE LD SET LD1 = @LD1, LD2 = @LD2, LD3 = @LD3, LD4 = @LD4, LD5 = @LD5, LD6 = @LD6, LD7 = @LD7, LD8 = @LD8
    WHERE LDID = @LDID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_LDPL]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_LDPL] (
@PatientID int,
@CellAddres int,
@ForeColor nvarchar(50),
@LDcellID int
) AS BEGIN
    UPDATE LDPL SET CellAddres = @CellAddres, ForeColor = @ForeColor
    WHERE LDcellID = @LDcellID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_LDSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_LDSTYLE] (
@PatientID int,
@CellAddres int,
@BakImg image,
@LDcellID int
) AS BEGIN
    UPDATE LDSTYLE SET PatientID = @PatientID, CellAddres = @CellAddres, BakImg = @BakImg
    WHERE LDcellID = @LDcellID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_LU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_LU] (
@PatientID int,
@LU1 nvarchar(50),
@LU2 nvarchar(50),
@LU3 nvarchar(50),
@LU4 nvarchar(50),
@LU5 nvarchar(50),
@LU6 nvarchar(50),
@LU7 nvarchar(50),
@LU8 nvarchar(50),
@LUID int
) AS BEGIN
    UPDATE LU SET LU1 = @LU1, LU2 = @LU2, LU3 = @LU3, LU4 = @LU4, LU5 = @LU5, LU6 = @LU6, LU7 = @LU7, LU8 = @LU8
    WHERE LUID = @LUID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_LUPL]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_LUPL] (
@PatientID int,
@CellAddres int,
@ForeColor nvarchar(50),
@LUcellID int
) AS BEGIN
    UPDATE LUPL SET CellAddres = @CellAddres, ForeColor = @ForeColor
    WHERE LUcellID = @LUcellID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_LUSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_LUSTYLE] (
@PatientID int,
@CellAddres int,
@BakImg image,
@LUcellID int
) AS BEGIN
    UPDATE LUSTYLE SET PatientID = @PatientID, CellAddres = @CellAddres, BakImg = @BakImg
    WHERE LUcellID = @LUcellID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_M_TRT]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_M_TRT] (
@MNo int,
@MName nvarchar(50),
@MTrt money,
@MPay money,
@MRemain money
) AS BEGIN
    UPDATE M_TRT SET MName = @MName, MTrt = @MTrt, MPay = @MPay, MRemain = @MRemain
    WHERE MNo = @MNo
END
GO
/****** Object:  StoredProcedure [dbo].[Update_MedicineDoze]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_MedicineDoze] (
@ShapeID int,
@Doze nvarchar(50),
@DozeID int
) AS BEGIN
    UPDATE MedicineDoze SET ShapeID = @ShapeID, Doze = @Doze
    WHERE DozeID = @DozeID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_MedicineFamily]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_MedicineFamily] (
@MedicineID int,
@MedicineSubCat nvarchar(50),
@SubCatID int
) AS BEGIN
    UPDATE MedicineFamily SET MedicineID = @MedicineID, MedicineSubCat = @MedicineSubCat
    WHERE SubCatID = @SubCatID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_MedicineGroups]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_MedicineGroups] (
@MedicineFamily nvarchar(50),
@MedicineID int
) AS BEGIN
    UPDATE MedicineGroups SET MedicineFamily = @MedicineFamily
    WHERE MedicineID = @MedicineID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_MedicineItems]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_MedicineItems] (
@ScincID int,
@CommName nvarchar(50),
@Company nvarchar(50),
@Notes nvarchar(150),
@MedicineItemID int
) AS BEGIN
    UPDATE MedicineItems SET ScincID = @ScincID, CommName = @CommName, Company = @Company, Notes = @Notes
    WHERE MedicineItemID = @MedicineItemID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_MedicineShape]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_MedicineShape] (
@MedicineItemID int,
@MedicineShape nvarchar(50),
@ShapeInfo nvarchar(50),
@ShapeID int
) AS BEGIN
    UPDATE MedicineShape SET MedicineItemID = @MedicineItemID, MedicineShape = @MedicineShape, ShapeInfo = @ShapeInfo
    WHERE ShapeID = @ShapeID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_MedScienceFamily]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_MedScienceFamily] (
@SubCatID int,
@ScienceName nvarchar(50),
@ScincID int
) AS BEGIN
    UPDATE MedScienceFamily SET SubCatID = @SubCatID, ScienceName = @ScienceName
    WHERE ScincID = @ScincID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_OrthoDiag]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_OrthoDiag] (
@PatientID int,
@CloseType nvarchar(150),
@ClassI nvarchar(150),
@Bite nvarchar(150),
@DiagID int
) AS BEGIN
    UPDATE OrthoDiag SET CloseType = @CloseType, ClassI = @ClassI, Bite = @Bite
    WHERE DiagID = @DiagID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_OrthoInf]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_OrthoInf] (
@PatientID int,
@Compliants nvarchar(50),
@Birth nvarchar(50),
@Feed nvarchar(50),
@MilkTeethChng nvarchar(50),
@MilkTeethAppear nvarchar(50),
@TeethLoss nvarchar(50),
@BurriedTeeth nvarchar(50),
@OverLoadTeeth nvarchar(50),
@LipsCut nvarchar(50),
@ThroatCut nvarchar(50),
@IllnesPeriod nvarchar(50),
@CousinsHFactor nvarchar(50),
@BadHabits nvarchar(50),
@Malfunction nvarchar(50),
@Khota nvarchar(150),
@PrevOrth nvarchar(50),
@PrevIll nvarchar(50),
@OrthoID int
) AS BEGIN
    UPDATE OrthoInf SET Compliants = @Compliants, Birth = @Birth, Feed = @Feed, MilkTeethChng = @MilkTeethChng, MilkTeethAppear = @MilkTeethAppear, TeethLoss = @TeethLoss, BurriedTeeth = @BurriedTeeth, OverLoadTeeth = @OverLoadTeeth, LipsCut = @LipsCut, ThroatCut = @ThroatCut, IllnesPeriod = @IllnesPeriod, CousinsHFactor = @CousinsHFactor, BadHabits = @BadHabits, Malfunction = @Malfunction, Khota = @Khota, PrevOrth = @PrevOrth, PrevIll = @PrevIll
    WHERE OrthoID = @OrthoID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_OrthoTreat]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_OrthoTreat] (
@PatientID int,
@BeginDate smalldatetime,
@OrthoType nvarchar(50),
@ExtraUL nvarchar(50),
@ExtraLL nvarchar(50),
@ExtraUR nvarchar(50),
@ExtraLR nvarchar(50),
@FixerDate smalldatetime,
@FixerType nvarchar(50),
@BraketType nvarchar(50),
@FinishDate smalldatetime,
@TreatID int
) AS BEGIN
    UPDATE OrthoTreat SET BeginDate = @BeginDate, OrthoType = @OrthoType, ExtraUL = @ExtraUL, ExtraLL = @ExtraLL, ExtraUR = @ExtraUR, ExtraLR = @ExtraLR, FixerDate = @FixerDate, FixerType = @FixerType, BraketType = @BraketType, FinishDate = @FinishDate
    WHERE TreatID = @TreatID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_OrthoTrtDet]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_OrthoTrtDet] (
@PatientID int,
@WorkDate smalldatetime,
@WireMeasure nvarchar(50),
@WireType nvarchar(50),
@WireImg nvarchar(50),
@DetID int
) AS BEGIN
    UPDATE OrthoTrtDet SET WorkDate = @WorkDate, WireMeasure = @WireMeasure, WireType = @WireType, WireImg = @WireImg
    WHERE DetID = @DetID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_OutDr]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_OutDr] (
@DrName nvarchar(50),
@DrAdres nvarchar(50),
@Drphone char,
@DrMobile char,
@DrID int
) AS BEGIN
    UPDATE OutDr SET DrName = @DrName, DrAdres = @DrAdres, Drphone = @Drphone, DrMobile = @DrMobile
    WHERE DrID = @DrID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Patient]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Patient] (
@PatientName nvarchar(50),
@Sex nvarchar(10),
@Age int,
@Phone nvarchar(16),
@Address nvarchar(100),
@Health nvarchar(150),
@Treat bit,
@Implant bit,
@Mobile bit,
@Ortho bit,
@Struc bit,
@Notes nvarchar(150),
@BirthY int,
@CreatedBy int,
@CreateDate datetime,
@PatientID int
) AS BEGIN
    UPDATE Patient SET PatientName = @PatientName, Sex = @Sex, Age = @Age, Phone = @Phone, Address = @Address, Health = @Health, Treat = @Treat, Implant = @Implant, Mobile = @Mobile, Ortho = @Ortho, Struc = @Struc, Notes = @Notes, BirthY = @BirthY, CreatedBy = @CreatedBy, CreateDate = @CreateDate
    WHERE PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Patient_Chart]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Patient_Chart] (
@PatientID int,
@ToothNum tinyint,
@ToothName nvarchar(50),
@IsExist bit,
@HasImp bit,
@ImpType nvarchar(50),
@ImpName nvarchar(50),
@ImpDate datetime,
@CrownType nvarchar(50),
@CrownName nvarchar(50),
@CrownDate datetime,
@TrtDate datetime,
@TrtName nvarchar(100),
@ClassID tinyint,
@ClassName nvarchar(100),
@TrtStage nvarchar(50),
@TrtDetails nvarchar(150),
@TrtNotes nvarchar(150),
@TrtID int
) AS BEGIN
    UPDATE Patient_Chart SET PatientID = @PatientID, ToothNum = @ToothNum, ToothName = @ToothName, IsExist = @IsExist, HasImp = @HasImp, ImpType = @ImpType, ImpName = @ImpName, ImpDate = @ImpDate, CrownType = @CrownType, CrownName = @CrownName, CrownDate = @CrownDate, TrtDate = @TrtDate, TrtName = @TrtName, ClassID = @ClassID, ClassName = @ClassName, TrtStage = @TrtStage, TrtDetails = @TrtDetails, TrtNotes = @TrtNotes
    WHERE TrtID = @TrtID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Patient_ChartCheck]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Patient_ChartCheck] (
@ToothNum tinyint,
@PatientID int,
@ToothName nvarchar(50),
@IsExist tinyint,
@CL1 tinyint,
@CL2 tinyint,
@CL3 tinyint,
@CL4 tinyint,
@CL5 tinyint,
@CL6 tinyint,
@CL7 tinyint,
@CL8 tinyint,
@CL9 tinyint,
@CL10 tinyint,
@CL11 tinyint,
@CL12 tinyint,
@CL13 tinyint,
@CL14 tinyint,
@CheckDate datetime,
@CheckNotes nvarchar(150)
) AS BEGIN
    UPDATE Patient_ChartCheck SET ToothName = @ToothName, IsExist = @IsExist, CL1 = @CL1, CL2 = @CL2, CL3 = @CL3, CL4 = @CL4, CL5 = @CL5, CL6 = @CL6, CL7 = @CL7, CL8 = @CL8, CL9 = @CL9, CL10 = @CL10, CL11 = @CL11, CL12 = @CL12, CL13 = @CL13, CL14 = @CL14, CheckDate = @CheckDate, CheckNotes = @CheckNotes
    WHERE ToothNum = @ToothNum AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Patient_Imgs]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Patient_Imgs] (
@PatientID int,
@PicName nvarchar(50),
@PicPath nvarchar(50),
@FullName nvarchar(512),
@PicID int
) AS BEGIN
    UPDATE Patient_Imgs SET PatientID = @PatientID, PicName = @PicName, PicPath = @PicPath, FullName = @FullName
    WHERE PicID = @PicID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Patient_MobStruc]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Patient_MobStruc] (
@PatientID int,
@StrucName nvarchar(50),
@StrucType nvarchar(50),
@TeethType nvarchar(50),
@StrucDate smalldatetime,
@StrucID int
) AS BEGIN
    UPDATE Patient_MobStruc SET PatientID = @PatientID, StrucName = @StrucName, StrucType = @StrucType, TeethType = @TeethType, StrucDate = @StrucDate
    WHERE StrucID = @StrucID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Patient_MobStrucAdd]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Patient_MobStrucAdd] (
@StrucID int,
@StrucName nvarchar(50),
@ToothLoc nvarchar(50),
@ToothNum nvarchar(50),
@AddTothDate smalldatetime,
@AddTothID int
) AS BEGIN
    UPDATE Patient_MobStrucAdd SET StrucID = @StrucID, StrucName = @StrucName, ToothLoc = @ToothLoc, ToothNum = @ToothNum, AddTothDate = @AddTothDate
    WHERE AddTothID = @AddTothID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Patient_Notes]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Patient_Notes] (
@PatientID int,
@NoteDate smalldatetime,
@Note nvarchar(250),
@NoteID int
) AS BEGIN
    UPDATE Patient_Notes SET PatientID = @PatientID, NoteDate = @NoteDate, Note = @Note
    WHERE NoteID = @NoteID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Patient_Pays]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Patient_Pays] (
@TrtID int,
@PatientID int,
@PayValue money,
@PayDate smalldatetime,
@Notes nvarchar(50),
@PayID int
) AS BEGIN
    UPDATE Patient_Pays SET TrtID = @TrtID, PatientID = @PatientID, PayValue = @PayValue, PayDate = @PayDate, Notes = @Notes
    WHERE PayID = @PayID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Patient_RX]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Patient_RX] (
@PatientID int,
@RXDate smalldatetime,
@RX nvarchar(500),
@RxID int
) AS BEGIN
    UPDATE Patient_RX SET PatientID = @PatientID, RXDate = @RXDate, RX = @RX
    WHERE RxID = @RxID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Patient_ToothCheck]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Patient_ToothCheck] (
@ToothNum tinyint,
@PatientID int,
@ToothName nvarchar(50),
@IsExist tinyint,
@COM_1 tinyint,
@COM_2M tinyint,
@COM_2D tinyint,
@COM_2MOD tinyint,
@COM_3M tinyint,
@COM_4 tinyint,
@COM_5 tinyint,
@COM_FACING tinyint,
@COM_3D tinyint,
@RCT tinyint,
@RCC tinyint,
@RCC_NS tinyint,
@RCF tinyint,
@RCF_COM tinyint,
@EXTRACTION tinyint,
@PULPOTOMY tinyint,
@PULPECTOMY tinyint,
@INDIRECT_PC tinyint,
@DIRECT_PC tinyint,
@FIBER_POST tinyint,
@TF tinyint,
@REDO_RCC tinyint,
@AMALGM tinyint,
@RCT_NECROTIC tinyint,
@PULPOTOMY_MTA tinyint,
@ABCESS_DRAINAGE tinyint,
@MTA_Bulk_Flow tinyint,
@Build_Up_Com tinyint,
@Build_Up_Acr tinyint,
@Build_Up_GI tinyint,
@GI_M tinyint,
@GI tinyint,
@GI_D tinyint,
@CheckDate datetime,
@CheckNotes nvarchar(150)
) AS BEGIN
    UPDATE Patient_ToothCheck SET ToothName = @ToothName, IsExist = @IsExist, COM_1 = @COM_1, COM_2M = @COM_2M, COM_2D = @COM_2D, COM_2MOD = @COM_2MOD, COM_3M = @COM_3M, COM_4 = @COM_4, COM_5 = @COM_5, COM_FACING = @COM_FACING, COM_3D = @COM_3D, RCT = @RCT, RCC = @RCC, RCC_NS = @RCC_NS, RCF = @RCF, RCF_COM = @RCF_COM, EXTRACTION = @EXTRACTION, PULPOTOMY = @PULPOTOMY, PULPECTOMY = @PULPECTOMY, INDIRECT_PC = @INDIRECT_PC, DIRECT_PC = @DIRECT_PC, FIBER_POST = @FIBER_POST, TF = @TF, REDO_RCC = @REDO_RCC, AMALGM = @AMALGM, RCT_NECROTIC = @RCT_NECROTIC, PULPOTOMY_MTA = @PULPOTOMY_MTA, ABCESS_DRAINAGE = @ABCESS_DRAINAGE, MTA_Bulk_Flow = @MTA_Bulk_Flow, Build_Up_Com = @Build_Up_Com, Build_Up_Acr = @Build_Up_Acr, Build_Up_GI = @Build_Up_GI, GI_M = @GI_M, GI = @GI, GI_D = @GI_D, CheckDate = @CheckDate, CheckNotes = @CheckNotes
    WHERE ToothNum = @ToothNum AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Patient_Trts]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Patient_Trts] (
@PatientID int,
@Detail nvarchar(50),
@TrtDate smalldatetime,
@TrtValue money,
@TrtID int
) AS BEGIN
    UPDATE Patient_Trts SET PatientID = @PatientID, Detail = @Detail, TrtDate = @TrtDate, TrtValue = @TrtValue
    WHERE TrtID = @TrtID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_PatientColors]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_PatientColors] (
@Color1 nvarchar(9),
@Color2 nvarchar(9),
@GradientIndex tinyint,
@AlphaValue int,
@PatientID int,
@ColorID int
) AS BEGIN
    UPDATE PatientColors SET Color1 = @Color1, Color2 = @Color2, GradientIndex = @GradientIndex, AlphaValue = @AlphaValue, PatientID = @PatientID
    WHERE ColorID = @ColorID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_RD]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_RD] (
@PatientID int,
@RD1 nvarchar(50),
@RD2 nvarchar(50),
@RD3 nvarchar(50),
@RD4 nvarchar(50),
@RD5 nvarchar(50),
@RD6 nvarchar(50),
@RD7 nvarchar(50),
@RD8 nvarchar(50),
@RDID int
) AS BEGIN
    UPDATE RD SET RD1 = @RD1, RD2 = @RD2, RD3 = @RD3, RD4 = @RD4, RD5 = @RD5, RD6 = @RD6, RD7 = @RD7, RD8 = @RD8
    WHERE RDID = @RDID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_RDPL]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_RDPL] (
@PatientID int,
@CellAddres int,
@ForeColor nvarchar(50),
@RDcellID int
) AS BEGIN
    UPDATE RDPL SET CellAddres = @CellAddres, ForeColor = @ForeColor
    WHERE RDcellID = @RDcellID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_RDSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_RDSTYLE] (
@PatientID int,
@CellAddres int,
@BakImg image,
@RDcellID int
) AS BEGIN
    UPDATE RDSTYLE SET PatientID = @PatientID, CellAddres = @CellAddres, BakImg = @BakImg
    WHERE RDcellID = @RDcellID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Resources]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Resources] (
@ResourceID int,
@ResourceName nvarchar(50),
@Color int,
@Image image,
@CustomField1 nvarchar(MAX),
@UniqueID int
) AS BEGIN
    UPDATE Resources SET ResourceID = @ResourceID, ResourceName = @ResourceName, Color = @Color, Image = @Image, CustomField1 = @CustomField1
    WHERE UniqueID = @UniqueID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_rs]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_rs] (
@UsName nvarchar(50),
@UsPass nvarchar(50),
@UsLvl int,
@UsGrp nvarchar(50),
@UsID int
) AS BEGIN
    UPDATE rs SET UsName = @UsName, UsPass = @UsPass, UsLvl = @UsLvl, UsGrp = @UsGrp
    WHERE UsID = @UsID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_RU]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_RU] (
@PatientID int,
@RU1 nvarchar(50),
@RU2 nvarchar(50),
@RU3 nvarchar(50),
@RU4 nvarchar(50),
@RU5 nvarchar(50),
@RU6 nvarchar(50),
@RU7 nvarchar(50),
@RU8 nvarchar(50),
@RUID int
) AS BEGIN
    UPDATE RU SET RU1 = @RU1, RU2 = @RU2, RU3 = @RU3, RU4 = @RU4, RU5 = @RU5, RU6 = @RU6, RU7 = @RU7, RU8 = @RU8
    WHERE RUID = @RUID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_RUPL]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_RUPL] (
@PatientID int,
@CellAddres int,
@ForeColor nvarchar(50),
@RUcellID int
) AS BEGIN
    UPDATE RUPL SET CellAddres = @CellAddres, ForeColor = @ForeColor
    WHERE RUcellID = @RUcellID AND PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_RUSTYLE]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_RUSTYLE] (
@PatientID int,
@CellAddres int,
@BakImg image,
@RUcellID int
) AS BEGIN
    UPDATE RUSTYLE SET PatientID = @PatientID, CellAddres = @CellAddres, BakImg = @BakImg
    WHERE RUcellID = @RUcellID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_RxBody]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_RxBody] (
@RxBdyID int,
@ArHdrName nvarchar(50),
@ArHdrAdres nvarchar(100),
@EnHdrName nvarchar(50),
@EnHdrAdres nvarchar(100),
@Logo image,
@Detail nvarchar(150),
@ArFtr nvarchar(50),
@EnFtr nvarchar(50),
@WtrImg image,
@WtrText nvarchar(50),
@UseWtrImg bit,
@UseWtrText BIT,
@DrName nvarchar(50)
) AS BEGIN
    UPDATE RxBody SET ArHdrName = @ArHdrName, ArHdrAdres = @ArHdrAdres, EnHdrName = @EnHdrName, EnHdrAdres = @EnHdrAdres, Logo = @Logo, Detail = @Detail, ArFtr = @ArFtr, EnFtr = @EnFtr, WtrImg = @WtrImg, WtrText = @WtrText, UseWtrImg = @UseWtrImg, UseWtrText = @UseWtrText,DrName =@DrName
    WHERE RxBdyID = @RxBdyID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_RxFly]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_RxFly] (
@PatientName nvarchar(50),
@PatientAge int,
@RxDate smalldatetime,
@RX nvarchar(500),
@RxID int
) AS BEGIN
    UPDATE RxFly SET PatientName = @PatientName, PatientAge = @PatientAge, RxDate = @RxDate, RX = @RX
    WHERE RxID = @RxID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Surgery]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Surgery] (
@PatientID int,
@SurgeryDet nvarchar(50),
@SurDate smalldatetime,
@SurgID int
) AS BEGIN
    UPDATE Surgery SET PatientID = @PatientID, SurgeryDet = @SurgeryDet, SurDate = @SurDate
    WHERE SurgID = @SurgID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblBnodMsareef]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblBnodMsareef] (
@BandName nvarchar(50),
@BandID int
) AS BEGIN
    UPDATE TblBnodMsareef SET BandName = @BandName
    WHERE BandID = @BandID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblCategories]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblCategories] (
@CategoryName nvarchar(75),
@ParentCategory int,
@CategoryID int
) AS BEGIN
    UPDATE TblCategories SET CategoryName = @CategoryName, ParentCategory = @ParentCategory
    WHERE CategoryID = @CategoryID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblCities]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblCities] (
@CityName nvarchar(50),
@CityID int
) AS BEGIN
    UPDATE TblCities SET CityName = @CityName
    WHERE CityID = @CityID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblCustomers]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblCustomers] (
@CusName nvarchar(75),
@CityID int,
@Address nvarchar(500),
@Contacts nvarchar(500),
@CusID int
) AS BEGIN
    UPDATE TblCustomers SET CusName = @CusName, CityID = @CityID, Address = @Address, Contacts = @Contacts
    WHERE CusID = @CusID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblExpenPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblExpenPay] (
@MasrofID int,
@PayValue money,
@PayDate datetime,
@Notes nvarchar(50),
@ExpPayID int
) AS BEGIN
    UPDATE TblExpenPay SET MasrofID = @MasrofID, PayValue = @PayValue, PayDate = @PayDate, Notes = @Notes
    WHERE ExpPayID = @ExpPayID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblInvoiceBody]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblInvoiceBody] (
@InvID int,
@ItemID int,
@Quantity float,
@Price money,
@ItemHasm money,
@Note nvarchar(100)
) AS BEGIN
    UPDATE TblInvoiceBody SET Quantity = @Quantity, Price = @Price, ItemHasm = @ItemHasm, Note = @Note
    WHERE InvID = @InvID AND ItemID = @ItemID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblInvoicesHeader]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblInvoicesHeader] (
@InvoiceType tinyint,
@InvoiceDate smalldatetime,
@ResID int,
@DocNo int,
@InvoiceEx nvarchar(500),
@Hasm money,
@InvoiceID int
) AS BEGIN
    UPDATE TblInvoicesHeader SET InvoiceType = @InvoiceType, InvoiceDate = @InvoiceDate, ResID = @ResID, DocNo = @DocNo, InvoiceEx = @InvoiceEx, Hasm = @Hasm
    WHERE InvoiceID = @InvoiceID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblInvPay]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblInvPay] (
@InvoiceID int,
@ResID int,
@PayDate datetime,
@Amount money,
@Notes nvarchar(50),
@PayID int
) AS BEGIN
    UPDATE TblInvPay SET InvoiceID = @InvoiceID, ResID = @ResID, PayDate = @PayDate, Amount = @Amount, Notes = @Notes
    WHERE PayID = @PayID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblItems]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblItems] (
@ItemName nvarchar(75),
@ItemEx nvarchar(500),
@CatID int,
@UnitID int,
@LastPrice money,
@QuantityNow float,
@ItemID int
) AS BEGIN
    UPDATE TblItems SET ItemName = @ItemName, ItemEx = @ItemEx, CatID = @CatID, UnitID = @UnitID, LastPrice = @LastPrice, QuantityNow = @QuantityNow
    WHERE ItemID = @ItemID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblMasareef]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblMasareef] (
@MasrofDate smalldatetime,
@BandID int,
@MasrofAmount money,
@MasrofEx nvarchar(500),
@MasrofID int
) AS BEGIN
    UPDATE TblMasareef SET MasrofDate = @MasrofDate, BandID = @BandID, MasrofAmount = @MasrofAmount, MasrofEx = @MasrofEx
    WHERE MasrofID = @MasrofID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblMeasure]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblMeasure] (
@Measure nvarchar(50),
@MeasureID int
) AS BEGIN
    UPDATE TblMeasure SET Measure = @Measure
    WHERE MeasureID = @MeasureID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblPaids]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblPaids] (
@PayType nvarchar(5),
@PayDate smalldatetime,
@ResCusId int,
@PayAmount money,
@PayEx nvarchar(300),
@PayID int
) AS BEGIN
    UPDATE TblPaids SET PayType = @PayType, PayDate = @PayDate, ResCusId = @ResCusId, PayAmount = @PayAmount, PayEx = @PayEx
    WHERE PayID = @PayID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblResources]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblResources] (
@ResName nvarchar(75),
@CityID int,
@Address nvarchar(500),
@Contacts nvarchar(500),
@ResID int
) AS BEGIN
    UPDATE TblResources SET ResName = @ResName, CityID = @CityID, Address = @Address, Contacts = @Contacts
    WHERE ResID = @ResID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblSalesBody]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblSalesBody] (
@SaleID int,
@ItemID int,
@Quantity float,
@Price money,
@ItemHasm money,
@Note nvarchar(100)
) AS BEGIN
    UPDATE TblSalesBody SET Quantity = @Quantity, Price = @Price, ItemHasm = @ItemHasm, Note = @Note
    WHERE SaleID = @SaleID AND ItemID = @ItemID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblSalesHeader]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblSalesHeader] (
@SaleType tinyint,
@SaleDate smalldatetime,
@CusID int,
@DocNo int,
@SaleEx nvarchar(500),
@Hasm money,
@SaleID int
) AS BEGIN
    UPDATE TblSalesHeader SET SaleType = @SaleType, SaleDate = @SaleDate, CusID = @CusID, DocNo = @DocNo, SaleEx = @SaleEx, Hasm = @Hasm
    WHERE SaleID = @SaleID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblTRT]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblTRT] (
@Trt nvarchar(50),
@TrtID int
) AS BEGIN
    UPDATE TblTRT SET Trt = @Trt
    WHERE TrtID = @TrtID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblUnits]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblUnits] (
@UnitName nvarchar(50),
@UnitID int
) AS BEGIN
    UPDATE TblUnits SET UnitName = @UnitName
    WHERE UnitID = @UnitID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_TblWireType]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_TblWireType] (
@WireType nvarchar(50),
@TypeID int
) AS BEGIN
    UPDATE TblWireType SET WireType = @WireType
    WHERE TypeID = @TypeID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_VisitType]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_VisitType] (
@VisType nvarchar(200),
@VtID int
) AS BEGIN
    UPDATE VisitType SET VisType = @VisType
    WHERE VtID = @VtID
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Y_TRT]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Y_TRT] (
@YName int,
@YYName nchar
) AS BEGIN
    UPDATE Y_TRT SET YYName = @YYName
    WHERE YName = @YName
END
GO
/****** Object:  StoredProcedure [dbo].[UsAtendDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UsAtendDelete] 
    @AtndID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[UsAtend]
	WHERE  [AtndID] = @AtndID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[UsAtendInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UsAtendInsert] 
    @UsID int,
    @AtnDay datetime,
    @AtnNote nvarchar(50) = NULL,
    @AbsPrsnt bit
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[UsAtend] ([UsID], [AtnDay], [AtnNote], [AbsPrsnt])
	SELECT @UsID, @AtnDay, @AtnNote, @AbsPrsnt
	  
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[UsAtendSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UsAtendSelect] 
    @AtndID int
AS 
	
	 

	BEGIN TRAN

	SELECT [AtndID], [UsID], [AtnDay], [AtnNote], [AbsPrsnt] 
	FROM   [dbo].[UsAtend] 
	WHERE  ([AtndID] = @AtndID OR @AtndID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[UsAtendUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UsAtendUpdate] 
    @AtndID int,
    @UsID int,
    @AtnDay datetime,
    @AtnNote nvarchar(50) = NULL,
    @AbsPrsnt bit
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[UsAtend]
	SET    [UsID] = @UsID, [AtnDay] = @AtnDay, [AtnNote] = @AtnNote, [AbsPrsnt] = @AbsPrsnt
	WHERE  [AtndID] = @AtndID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteDuplicatesForPatients]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_DeleteDuplicatesForPatients]
    @TableName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IDColumnName NVARCHAR(50);

    -- Determine the ID column based on @TableName
    IF @TableName = 'LUPL'
        SET @IDColumnName = 'LUcellID';
    ELSE IF @TableName = 'LDPL'
        SET @IDColumnName = 'LDcellID';
    ELSE IF @TableName = 'RUPL'
        SET @IDColumnName = 'RUcellID';
    ELSE IF @TableName = 'RDPL'
        SET @IDColumnName = 'RDcellID';
    ELSE
    BEGIN
        RAISERROR ('Invalid @TableName specified.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @sql NVARCHAR(MAX);

        SET @sql = '
        WITH CTE AS (
            SELECT 
                ' + QUOTENAME(@IDColumnName) + ',
                PatientID,
                CellAddres,
                ForeColor,
                ROW_NUMBER() OVER(PARTITION BY PatientID, CellAddres ORDER BY ' + QUOTENAME(@IDColumnName) + ') AS RowNum
            FROM 
                [dbo].[' + @TableName + ']
        )
        DELETE FROM CTE WHERE RowNum > 1;';

        EXEC sp_executesql @sql;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(MAX);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteDuplicatesForStyle]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_DeleteDuplicatesForStyle]
    @TableName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IDColumnName NVARCHAR(50);

    -- Determine the ID column based on @TableName
    IF @TableName = 'LUSTYLE'
        SET @IDColumnName = 'LUcellID';
    ELSE IF @TableName = 'LDSTYLE'
        SET @IDColumnName = 'LDcellID';
    ELSE IF @TableName = 'RUSTYLE'
        SET @IDColumnName = 'RUcellID';
    ELSE IF @TableName = 'RDSTYLE'
        SET @IDColumnName = 'RDcellID';
    ELSE
    BEGIN
        RAISERROR ('Invalid @TableName specified.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @sql NVARCHAR(MAX);

        SET @sql = '
        WITH CTE AS (
            SELECT 
                ' + QUOTENAME(@IDColumnName) + ',
                PatientID,
                CellAddres,
                BakImg,
                ROW_NUMBER() OVER(PARTITION BY PatientID, CellAddres ORDER BY ' + QUOTENAME(@IDColumnName) + ') AS RowNum
            FROM 
                [dbo].[' + @TableName + ']
        )
        DELETE FROM CTE WHERE RowNum > 1;';

        EXEC sp_executesql @sql;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(MAX);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_RxBodyUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_RxBodyUpdate] 
    @RxBdyID int,
    @ArHdrName nvarchar(50) = NULL,
    @ArHdrAdres nvarchar(100) = NULL,
    @EnHdrName nvarchar(50) = NULL,
    @EnHdrAdres nvarchar(1000) = NULL,
    @Logo image = NULL,
    @Detail nvarchar(150) = NULL,
    @ArFtr nvarchar(50) = NULL,
    @EnFtr nvarchar(50) = NULL,
    @WtrImg image = NULL,
    @WtrText nvarchar(50) = NULL,
    @UseWtrImg bit = NULL,
    @UseWtrText bit = NULL,
	@DrName nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[RxBody]
	SET    [ArHdrName] = @ArHdrName, [ArHdrAdres] = @ArHdrAdres, [EnHdrName] = @EnHdrName, [EnHdrAdres] = @EnHdrAdres, [Logo] = @Logo, [Detail] = @Detail, [ArFtr] = @ArFtr, [EnFtr] = @EnFtr, [WtrImg] = @WtrImg, [WtrText] = @WtrText, [UseWtrImg] = @UseWtrImg, [UseWtrText] = @UseWtrText,DrName =@DrName
	WHERE  [RxBdyID] = @RxBdyID
	
	-- Begin Return Select <- do not remove
	SELECT [RxBdyID], [ArHdrName], [ArHdrAdres], [EnHdrName], [EnHdrAdres], [Logo], [Detail], [ArFtr], [EnFtr], [WtrImg], [WtrText], [UseWtrImg], [UseWtrText]
	FROM   [dbo].[RxBody]
	WHERE  [RxBdyID] = @RxBdyID	
	-- End Return Select <- do not remove

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[UsrPayDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UsrPayDelete] 
    @UsSalID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[UsrPay]
	WHERE  [UsSalID] = @UsSalID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[UsrPayInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UsrPayInsert] 
    @UsID int,
    @MonthPay int,
    @DayPay int,
    @FromDT datetime,
    @ToDT datetime,
    @DaysCount int,
    @PayDate datetime,
    @PayNote nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[UsrPay] ([UsID], [MonthPay], [DayPay], [FromDT], [ToDT], [DaysCount], [PayDate], [PayNote])
	SELECT @UsID, @MonthPay, @DayPay, @FromDT, @ToDT, @DaysCount, @PayDate, @PayNote
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[UsrPaySelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UsrPaySelect] 
    @UsSalID int
AS 
	
	 

	BEGIN TRAN

	SELECT [UsSalID], [UsID], [MonthPay], [DayPay], [FromDT], [ToDT], [DaysCount], [PayDate], [PayNote] 
	FROM   [dbo].[UsrPay] 
	WHERE  ([UsSalID] = @UsSalID OR @UsSalID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[UsrPayUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UsrPayUpdate] 
    @UsSalID int,
    @UsID int,
    @MonthPay int,
    @DayPay int,
    @FromDT datetime,
    @ToDT datetime,
    @DaysCount int,
    @PayDate datetime,
    @PayNote nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[UsrPay]
	SET    [UsID] = @UsID, [MonthPay] = @MonthPay, [DayPay] = @DayPay, [FromDT] = @FromDT, [ToDT] = @ToDT, [DaysCount] = @DaysCount, [PayDate] = @PayDate, [PayNote] = @PayNote
	WHERE  [UsSalID] = @UsSalID
	
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[VisitsDelete]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[VisitsDelete] 
    @VisitDetID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Visits]
	WHERE  [VisitDetID] = @VisitDetID

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[VisitsInsert]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[VisitsInsert] 
    @PatientID int,
    @VisitDay nvarchar(10),
    @VisTime nvarchar(10) = NULL,
    @PatientName nvarchar(50) = NULL,
    @VisDetail nvarchar(50) = NULL,
    @VisNotes nvarchar(50) = NULL,
    @VisDateTime datetime = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Visits] ([PatientID], [VisitDay], [VisTime], [PatientName], [VisDetail], [VisNotes], [VisDateTime])
	SELECT @PatientID, @VisitDay, @VisTime, @PatientName, @VisDetail, @VisNotes, @VisDateTime
	 
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[VisitsSelect]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[VisitsSelect] 
    @VisitDetID int
AS 
	
	 

	BEGIN TRAN

	SELECT [VisitDetID], [PatientID], [VisitDay], [VisTime], [PatientName], [VisDetail], [VisNotes], [VisDateTime] 
	FROM   [dbo].[Visits] 
	WHERE  ([VisitDetID] = @VisitDetID OR @VisitDetID IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[VisitsUpdate]    Script Date: 10/28/2025 9:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[VisitsUpdate] 
    @VisitDetID int,
    @PatientID int,
    @VisitDay nvarchar(10),
    @VisTime nvarchar(10) = NULL,
    @PatientName nvarchar(50) = NULL,
    @VisDetail nvarchar(50) = NULL,
    @VisNotes nvarchar(50) = NULL,
    @VisDateTime datetime = NULL
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[Visits]
	SET    [PatientID] = @PatientID, [VisitDay] = @VisitDay, [VisTime] = @VisTime, [PatientName] = @PatientName, [VisDetail] = @VisDetail, [VisNotes] = @VisNotes, [VisDateTime] = @VisDateTime
	WHERE  [VisitDetID] = @VisitDetID
	
	COMMIT
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "patient"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 353
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[13] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "patient"
            Begin Extent = 
               Top = 32
               Left = 365
               Bottom = 162
               Right = 535
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PatientTRTS"
            Begin Extent = 
               Top = 33
               Left = 135
               Bottom = 129
               Right = 305
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PatientPAYS"
            Begin Extent = 
               Top = 33
               Left = 586
               Bottom = 129
               Right = 756
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BALANCE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BALANCE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 102
               Left = 38
               Bottom = 232
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ImplantVW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ImplantVW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TblCategories"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblItems"
            Begin Extent = 
               Top = 6
               Left = 247
               Bottom = 136
               Right = 417
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblInvoiceBody"
            Begin Extent = 
               Top = 6
               Left = 455
               Bottom = 136
               Right = 625
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblUnits"
            Begin Extent = 
               Top = 6
               Left = 871
               Bottom = 102
               Right = 1041
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or =' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InvBdyView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InvBdyView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InvBdyView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TblCategories"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblItems"
            Begin Extent = 
               Top = 12
               Left = 349
               Bottom = 210
               Right = 519
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblInvoicesHeader"
            Begin Extent = 
               Top = 25
               Left = 794
               Bottom = 155
               Right = 964
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblInvoiceBody"
            Begin Extent = 
               Top = 15
               Left = 564
               Bottom = 197
               Right = 734
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblUnits"
            Begin Extent = 
               Top = 210
               Left = 27
               Bottom = 306
               Right = 197
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin Criteria' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InvBodyVW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'Pane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InvBodyVW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InvBodyVW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TblInvoicesHeader"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 214
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblResources"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 175
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InvHdrVW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InvHdrVW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TblUnits"
            Begin Extent = 
               Top = 6
               Left = 1089
               Bottom = 102
               Right = 1259
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblItems"
            Begin Extent = 
               Top = 6
               Left = 465
               Bottom = 136
               Right = 635
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblInvoiceBody"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblCities"
            Begin Extent = 
               Top = 6
               Left = 881
               Bottom = 102
               Right = 1051
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblResources"
            Begin Extent = 
               Top = 6
               Left = 673
               Bottom = 136
               Right = 843
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TblInvoicesHeader"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 427
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 25
         Width = 284
         Width = 1500
     ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InvVIEW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'    Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InvVIEW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'InvVIEW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "patient"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 335
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "BALANCE"
            Begin Extent = 
               Top = 172
               Left = 245
               Bottom = 268
               Right = 415
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PatientPAYS"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 102
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PatientTRTS"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 102
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 19
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         O' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LstPatientView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'utput = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LstPatientView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LstPatientView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "patient"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 10
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'NamesView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'NamesView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Patient_Pays"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 210
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Patient"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientPAYS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientPAYS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Patient_RX"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Patient"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientRXVIEW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientRXVIEW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Patient_Trts"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 171
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Patient"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientTRTS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientTRTS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Patient_RX"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Patient"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RxBody"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 284
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Rx_BdyVW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Rx_BdyVW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TreatPays"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Patient_Trts"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TreatBal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TreatBal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Patient_Pays"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Patient_Trts"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 136
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TreatPays'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TreatPays'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "r"
            Begin Extent = 
               Top = 80
               Left = 682
               Bottom = 274
               Right = 871
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 89
               Left = 99
               Bottom = 287
               Right = 285
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "l"
            Begin Extent = 
               Top = 64
               Left = 390
               Bottom = 271
               Right = 576
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 2115
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_ContactBalances'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_ContactBalances'
GO
