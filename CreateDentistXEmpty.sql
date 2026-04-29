

/* [DentistX].[dbo].[AccView] */

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

/* [DentistX].[dbo].[AllInvNet] */

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

/* [DentistX].[dbo].[AllPaysOfRes] */

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

/* [DentistX].[dbo].[AnyQrtr] */

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

/* [DentistX].[dbo].[BALANCE] */

CREATE VIEW [dbo].[BALANCE]
AS
SELECT        TOP (100) PERCENT dbo.patient.PatientID, ISNULL(dbo.PatientPAYS.PAYS - dbo.PatientTRTS.TRTS, 0) AS BAL
FROM            dbo.patient LEFT OUTER JOIN
                         dbo.PatientTRTS ON dbo.patient.PatientID = dbo.PatientTRTS.PatientID LEFT OUTER JOIN
                         dbo.PatientPAYS ON dbo.patient.PatientID = dbo.PatientPAYS.PatientID
ORDER BY dbo.patient.PatientID
GO

/* [DentistX].[dbo].[Barcodes] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Barcodes](
	[BarcodeID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[BarcodeValue] [nvarchar](100) NOT NULL,
	[BarcodeType] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BarcodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[BarcodeValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Barcodes]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO

ALTER TABLE [dbo].[Barcodes]  WITH CHECK ADD CHECK  (([BarcodeType]='Custom' OR [BarcodeType]='QR' OR [BarcodeType]='EAN' OR [BarcodeType]='UPC'))
GO
GO

/* [DentistX].[dbo].[Batchess] */

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
PRIMARY KEY CLUSTERED 
(
	[BatchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_Batchess_Expiration] ON [dbo].[Batchess]
(
	[ExpirationDate] ASC
)
INCLUDE([CurrentQuantity]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Batchess_ExpirationDate] ON [dbo].[Batchess]
(
	[ExpirationDate] ASC
)
INCLUDE([CurrentQuantity]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Batchess]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO

ALTER TABLE [dbo].[Batchess]  WITH CHECK ADD FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
GO

/* [DentistX].[dbo].[BuyInvoiceLineItems] */

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
PRIMARY KEY CLUSTERED 
(
	[LineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BuyInvoiceLineItems]  WITH CHECK ADD FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[BuyInvoices] ([InvoiceID])
GO

ALTER TABLE [dbo].[BuyInvoiceLineItems]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
GO

/* [DentistX].[dbo].[BuyInvoices] */

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
PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BuyInvoices] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[BuyInvoices]  WITH CHECK ADD FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO

ALTER TABLE [dbo].[BuyInvoices]  WITH CHECK ADD CHECK  (([InvoiceStatus]='Partial' OR [InvoiceStatus]='Unpaid' OR [InvoiceStatus]='Paid'))
GO
GO

/* [DentistX].[dbo].[Categories] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[Delete_LD] */

CREATE PROCEDURE [dbo].[Delete_LD] (
@LDID int,
@PatientID int
) AS BEGIN
DELETE FROM LD
WHERE LDID = @LDID AND PatientID = @PatientID
END
GO

/* [DentistX].[dbo].[delete_RxFly] */

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

/* [DentistX].[dbo].[DrWork] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[DrWorkDelete] */

CREATE PROC [dbo].[DrWorkDelete] 
    @WorkID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[DrWork]
	WHERE  [WorkID] = @WorkID

	COMMIT
GO

/* [DentistX].[dbo].[DrWorkInsert] */

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

/* [DentistX].[dbo].[DrWorkPay] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[DrWorkSelect] */

CREATE PROC [dbo].[DrWorkSelect] 
    @WorkID int
AS 
	
	 

	BEGIN TRAN

	SELECT [WorkID], [DrID], [PatientID], [WrkDate], [WrkDetail], [WrkVal], [PayVal], [Imp], [Orth], [Surg], [Notes] 
	FROM   [dbo].[DrWork] 
	WHERE  ([WorkID] = @WorkID OR @WorkID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[DrWorkUpdate] */

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

/* [DentistX].[dbo].[Emp] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[EmpDelete] */

CREATE PROC [dbo].[EmpDelete] 
    @EmpID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Emp]
	WHERE  [EmpID] = @EmpID

	COMMIT
GO

/* [DentistX].[dbo].[EmpInsert] */

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

/* [DentistX].[dbo].[EmpSelect] */

CREATE PROC [dbo].[EmpSelect] 
    @EmpID int
AS 
	
	 

	BEGIN TRAN

	SELECT [EmpID], [EmpName], [EmpPhone], [EmpAddress], [EmpImg] 
	FROM   [dbo].[Emp] 
	WHERE  ([EmpID] = @EmpID OR @EmpID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[EmpUpdate] */

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

/* [DentistX].[dbo].[ExpiringBatchessView] */

CREATE VIEW ExpiringBatchessView
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

/* [DentistX].[dbo].[Gender] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[GenderDelete] */

CREATE PROC [dbo].[GenderDelete] 
    @SID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Gender]
	WHERE  [SID] = @SID

	COMMIT
GO

/* [DentistX].[dbo].[GenderInsert] */

CREATE PROC [dbo].[GenderInsert] 
    @Sex nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Gender] ([Sex])
	SELECT @Sex
	
	COMMIT
GO

/* [DentistX].[dbo].[GenderSelect] */

CREATE PROC [dbo].[GenderSelect] 
    @SID int
AS 
	
	 

	BEGIN TRAN

	SELECT [SID], [Sex] 
	FROM   [dbo].[Gender] 
	WHERE  ([SID] = @SID OR @SID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[GenderUpdate] */

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

/* [DentistX].[dbo].[GetSupplierPurchaseSummary] */

CREATE PROCEDURE GetSupplierPurchaseSummary
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

/* [DentistX].[dbo].[Health] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[HealthDelete] */

CREATE PROC [dbo].[HealthDelete] 
    @HID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Health]
	WHERE  [HID] = @HID

	COMMIT
GO

/* [DentistX].[dbo].[HealthInsert] */

CREATE PROC [dbo].[HealthInsert] 
    @HealthStat nvarchar(100)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Health] ([HealthStat])
	SELECT @HealthStat
	
	COMMIT
GO

/* [DentistX].[dbo].[HealthSelect] */

CREATE PROC [dbo].[HealthSelect] 
    @HID int
AS 
	
	 

	BEGIN TRAN

	SELECT [HID], [HealthStat] 
	FROM   [dbo].[Health] 
	WHERE  ([HID] = @HID OR @HID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[HealthUpdate] */

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

/* [DentistX].[dbo].[Imags] */

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
 CONSTRAINT [PK_image] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[ImagsDelete] */

CREATE PROC [dbo].[ImagsDelete] 
    @ImageID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Imags]
	WHERE  [ImageID] = @ImageID

	COMMIT
GO

/* [DentistX].[dbo].[ImagsInsert] */

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

/* [DentistX].[dbo].[ImagsSelect] */

CREATE PROC [dbo].[ImagsSelect] 
    @ImageID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ImageID], [IMG], [Height], [Width], [Sze], [DatePictureTaken], [EquipmentMaker], [EquipmentModel], [Thumbnail], [DateCreated], [DateModified] 
	FROM   [dbo].[Imags] 
	WHERE  ([ImageID] = @ImageID OR @ImageID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[ImagsUpdate] */

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

/* [DentistX].[dbo].[ImpClrs] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[ImpClrsDelete] */

CREATE PROC [dbo].[ImpClrsDelete] 
    @ImpClrID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[ImpClrs]
	WHERE  [ImpClrID] = @ImpClrID

	COMMIT
GO

/* [DentistX].[dbo].[ImpClrsInsert] */

CREATE PROC [dbo].[ImpClrsInsert] 
    @ImpClr nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ImpClrs] ([ImpClr])
	SELECT @ImpClr
	 
	COMMIT
GO

/* [DentistX].[dbo].[ImpClrsSelect] */

CREATE PROC [dbo].[ImpClrsSelect] 
    @ImpClrID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ImpClrID], [ImpClr] 
	FROM   [dbo].[ImpClrs] 
	WHERE  ([ImpClrID] = @ImpClrID OR @ImpClrID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[ImpClrsUpdate] */

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

/* [DentistX].[dbo].[ImplantBrand] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ImplantBrand](
	[BrandID] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ImplantB_DAD4F3BE0773FD5C] PRIMARY KEY CLUSTERED 
(
	[BrandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_ImplantB_2206CE9B2C5D9560] UNIQUE NONCLUSTERED 
(
	[BrandName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[ImplantMeasure] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ImplantMeasure](
	[MeasureID] [int] IDENTITY(1,1) NOT NULL,
	[VariationID] [int] NOT NULL,
	[DiameterMM] [decimal](4, 1) NOT NULL,
	[LengthMM] [decimal](4, 1) NOT NULL,
 CONSTRAINT [PK_ImplantM] PRIMARY KEY CLUSTERED 
(
	[MeasureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ImplantMeasure]  WITH CHECK ADD  CONSTRAINT [FK_ImplantMe_Varia] FOREIGN KEY([VariationID])
REFERENCES [dbo].[ImplantVariation] ([VariationID])
GO

ALTER TABLE [dbo].[ImplantMeasure] CHECK CONSTRAINT [FK_ImplantMe_Varia]
GO

ALTER TABLE [dbo].[ImplantMeasure]  WITH CHECK ADD  CONSTRAINT [CK_ImplantMe_Diame] CHECK  (([DiameterMM]>=(2.0) AND [DiameterMM]<=(6.0)))
GO

ALTER TABLE [dbo].[ImplantMeasure] CHECK CONSTRAINT [CK_ImplantMe_Diame]
GO

ALTER TABLE [dbo].[ImplantMeasure]  WITH CHECK ADD  CONSTRAINT [CK_ImplantMe_Length] CHECK  (([LengthMM]>=(5.0) AND [LengthMM]<=(20.0)))
GO

ALTER TABLE [dbo].[ImplantMeasure] CHECK CONSTRAINT [CK_ImplantMe_Length]
GO
GO

/* [DentistX].[dbo].[ImplantType] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ImplantType](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ImplantT] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ImplantType]  WITH CHECK ADD  CONSTRAINT [CK_ImplantTy_TypeN] CHECK  (([TypeName]='COMP' OR [TypeName]='BSI'))
GO

ALTER TABLE [dbo].[ImplantType] CHECK CONSTRAINT [CK_ImplantTy_TypeN]
GO
GO

/* [DentistX].[dbo].[ImplantVariation] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ImplantVariation](
	[VariationID] [int] IDENTITY(1,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
	[IsSlim] [bit] NOT NULL,
 CONSTRAINT [PK_ImplantV] PRIMARY KEY CLUSTERED 
(
	[VariationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ImplantVariation] ADD  CONSTRAINT [DF_ImplantVa_IsSli_03DB89B3]  DEFAULT ((0)) FOR [IsSlim]
GO

ALTER TABLE [dbo].[ImplantVariation]  WITH CHECK ADD  CONSTRAINT [FK_ImplantVa_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[ImplantBrand] ([BrandID])
GO

ALTER TABLE [dbo].[ImplantVariation] CHECK CONSTRAINT [FK_ImplantVa_Brand]
GO

ALTER TABLE [dbo].[ImplantVariation]  WITH CHECK ADD  CONSTRAINT [FK_ImplantVa_TypeI] FOREIGN KEY([TypeID])
REFERENCES [dbo].[ImplantType] ([TypeID])
GO

ALTER TABLE [dbo].[ImplantVariation] CHECK CONSTRAINT [FK_ImplantVa_TypeI]
GO
GO

/* [DentistX].[dbo].[ImprDet] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ImprDet]  WITH CHECK ADD  CONSTRAINT [FK_ImprDet_Impression] FOREIGN KEY([imprID])
REFERENCES [dbo].[Impression] ([ImprID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ImprDet] CHECK CONSTRAINT [FK_ImprDet_Impression]
GO
GO

/* [DentistX].[dbo].[ImprDetDelete] */

CREATE PROC [dbo].[ImprDetDelete] 
    @ImpDetID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[ImprDet]
	WHERE  [ImpDetID] = @ImpDetID

	COMMIT
GO

/* [DentistX].[dbo].[ImprDetInsert] */

CREATE PROC [dbo].[ImprDetInsert] 
    @imprID int,
    @ImprDetail nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ImprDet] ([imprID], [ImprDetail])
	SELECT @imprID, @ImprDetail
	    
	COMMIT
GO

/* [DentistX].[dbo].[ImprDetSelect] */

CREATE PROC [dbo].[ImprDetSelect] 
    @ImpDetID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ImpDetID], [imprID], [ImprDetail] 
	FROM   [dbo].[ImprDet] 
	WHERE  ([ImpDetID] = @ImpDetID OR @ImpDetID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[ImprDetUpdate] */

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

/* [DentistX].[dbo].[Impression] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[ImpressionDelete] */

CREATE PROC [dbo].[ImpressionDelete] 
    @ImprID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Impression]
	WHERE  [ImprID] = @ImprID

	COMMIT
GO

/* [DentistX].[dbo].[ImpressionInsert] */

CREATE PROC [dbo].[ImpressionInsert] 
    @ImprType nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Impression] ([ImprType])
	SELECT @ImprType
	
	COMMIT
GO

/* [DentistX].[dbo].[ImpressionSelect] */

CREATE PROC [dbo].[ImpressionSelect] 
    @ImprID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ImprID], [ImprType] 
	FROM   [dbo].[Impression] 
	WHERE  ([ImprID] = @ImprID OR @ImprID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[ImpressionUpdate] */

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

/* [DentistX].[dbo].[Insert_Appointments] */

CREATE PROCEDURE [dbo].[Insert_Appointments] (
@Type int, @StartDate datetime, @EndDate datetime, @QueryStartDate datetime, @QueryEndDate datetime, @AllDay bit, @Subject nvarchar, @Location nvarchar, @Description nvarchar, @Status int, @Label int, @ResourceID int, @ResourceIDs nvarchar, @ReminderInfo nvarchar, @RecurrenceInfo nvarchar, @TimeZoneId nvarchar, @CustomField1 nvarchar, @PatientID int
) AS BEGIN
INSERT INTO Appointments (Type, StartDate, EndDate, QueryStartDate, QueryEndDate, AllDay, Subject, Location, Description, Status, Label, ResourceID, ResourceIDs, ReminderInfo, RecurrenceInfo, TimeZoneId, CustomField1, PatientID)
VALUES (@Type, @StartDate, @EndDate, @QueryStartDate, @QueryEndDate, @AllDay, @Subject, @Location, @Description, @Status, @Label, @ResourceID, @ResourceIDs, @ReminderInfo, @RecurrenceInfo, @TimeZoneId, @CustomField1, @PatientID)
END
GO

/* [DentistX].[dbo].[Insert_DrWork] */

CREATE PROCEDURE [dbo].[Insert_DrWork] (
@DrID int, @PatientID int, @WrkDate datetime, @WrkDetail nvarchar, @WrkVal money, @PayVal money, @Imp bit, @Orth bit, @Surg bit, @Notes nvarchar
) AS BEGIN
INSERT INTO DrWork (DrID, PatientID, WrkDate, WrkDetail, WrkVal, PayVal, Imp, Orth, Surg, Notes)
VALUES (@DrID, @PatientID, @WrkDate, @WrkDetail, @WrkVal, @PayVal, @Imp, @Orth, @Surg, @Notes)
END
GO

/* [DentistX].[dbo].[Insert_DrWorkPay] */

CREATE PROCEDURE [dbo].[Insert_DrWorkPay] (
@WorkID int, @DrID int, @PayValue money, @PayDate datetime, @Notes nvarchar
) AS BEGIN
INSERT INTO DrWorkPay (WorkID, DrID, PayValue, PayDate, Notes)
VALUES (@WorkID, @DrID, @PayValue, @PayDate, @Notes)
END
GO

/* [DentistX].[dbo].[Insert_Emp] */

CREATE PROCEDURE [dbo].[Insert_Emp] (
@EmpName nvarchar, @EmpPhone nchar, @EmpAddress nvarchar, @EmpImg image
) AS BEGIN
INSERT INTO Emp (EmpName, EmpPhone, EmpAddress, EmpImg)
VALUES (@EmpName, @EmpPhone, @EmpAddress, @EmpImg)
END
GO

/* [DentistX].[dbo].[Insert_EmpAtend] */

CREATE PROCEDURE [dbo].[Insert_EmpAtend] (
@EmpID int, @AtnDay datetime, @AtnNote nvarchar, @AbsPrsnt bit
) AS BEGIN
INSERT INTO EmpAtend (EmpID, AtnDay, AtnNote, AbsPrsnt)
VALUES (@EmpID, @AtnDay, @AtnNote, @AbsPrsnt)
END
GO

/* [DentistX].[dbo].[Insert_EmpPay] */

CREATE PROCEDURE [dbo].[Insert_EmpPay] (
@EmpID int, @MonthPay int, @DayPay int, @FromDT datetime, @ToDT datetime, @DaysCount int, @PayDate datetime, @PayNote nvarchar
) AS BEGIN
INSERT INTO EmpPay (EmpID, MonthPay, DayPay, FromDT, ToDT, DaysCount, PayDate, PayNote)
VALUES (@EmpID, @MonthPay, @DayPay, @FromDT, @ToDT, @DaysCount, @PayDate, @PayNote)
END
GO

/* [DentistX].[dbo].[Insert_Gender] */

CREATE PROCEDURE [dbo].[Insert_Gender] (
@Sex nvarchar
) AS BEGIN
INSERT INTO Gender (Sex)
VALUES (@Sex)
END
GO

/* [DentistX].[dbo].[Insert_Health] */

CREATE PROCEDURE [dbo].[Insert_Health] (
@HealthStat nvarchar
) AS BEGIN
INSERT INTO Health (HealthStat)
VALUES (@HealthStat)
END
GO

/* [DentistX].[dbo].[Insert_Imags] */

CREATE PROCEDURE [dbo].[Insert_Imags] (
@IMG varbinary, @Height float, @Width float, @Sze float, @DatePictureTaken datetime, @EquipmentMaker varchar, @EquipmentModel varchar, @Thumbnail varbinary, @DateCreated datetime, @DateModified datetime
) AS BEGIN
INSERT INTO Imags (IMG, Height, Width, Sze, DatePictureTaken, EquipmentMaker, EquipmentModel, Thumbnail, DateCreated, DateModified)
VALUES (@IMG, @Height, @Width, @Sze, @DatePictureTaken, @EquipmentMaker, @EquipmentModel, @Thumbnail, @DateCreated, @DateModified)
END
GO

/* [DentistX].[dbo].[Insert_ImpClrs] */

CREATE PROCEDURE [dbo].[Insert_ImpClrs] (
@ImpClr nvarchar
) AS BEGIN
INSERT INTO ImpClrs (ImpClr)
VALUES (@ImpClr)
END
GO

/* [DentistX].[dbo].[Insert_ImprDet] */

CREATE PROCEDURE [dbo].[Insert_ImprDet] (
@imprID int, @ImprDetail nvarchar
) AS BEGIN
INSERT INTO ImprDet (imprID, ImprDetail)
VALUES (@imprID, @ImprDetail)
END
GO

/* [DentistX].[dbo].[Insert_Impression] */

CREATE PROCEDURE [dbo].[Insert_Impression] (
@ImprType nvarchar
) AS BEGIN
INSERT INTO Impression (ImprType)
VALUES (@ImprType)
END
GO

/* [DentistX].[dbo].[Insert_Lab] */

CREATE PROCEDURE [dbo].[Insert_Lab] (
@LabName nvarchar, @Adres nvarchar, @Phone nchar, @Mobile nchar
) AS BEGIN
INSERT INTO Lab (LabName, Adres, Phone, Mobile)
VALUES (@LabName, @Adres, @Phone, @Mobile)
END
GO

/* [DentistX].[dbo].[Insert_LabOrder] */

CREATE PROCEDURE [dbo].[Insert_LabOrder] (
@LabID int, @PatientID int, @ImprType nvarchar, @ImprDet nvarchar, @ImprClr nvarchar, @ImprCount int, @DeliveryDate datetime, @Price int, @RecieveDate datetime, @Notes nvarchar
) AS BEGIN
INSERT INTO LabOrder (LabID, PatientID, ImprType, ImprDet, ImprClr, ImprCount, DeliveryDate, Price, RecieveDate, Notes)
VALUES (@LabID, @PatientID, @ImprType, @ImprDet, @ImprClr, @ImprCount, @DeliveryDate, @Price, @RecieveDate, @Notes)
END
GO

/* [DentistX].[dbo].[Insert_LabPay] */

CREATE PROCEDURE [dbo].[Insert_LabPay] (
@LabID int, @LabOrderID int, @PayValue int, @PayDate datetime, @PayDetail nvarchar, @Notes nvarchar
) AS BEGIN
INSERT INTO LabPay (LabID, LabOrderID, PayValue, PayDate, PayDetail, Notes)
VALUES (@LabID, @LabOrderID, @PayValue, @PayDate, @PayDetail, @Notes)
END
GO

/* [DentistX].[dbo].[Insert_LD] */

CREATE PROCEDURE [dbo].[Insert_LD] (
@PatientID int, @LD1 nvarchar(50), @LD2 nvarchar(50), @LD3 nvarchar(50), @LD4 nvarchar(50), @LD5 nvarchar(50), @LD6 nvarchar(50), @LD7 nvarchar(50), @LD8 nvarchar(50)
) AS BEGIN
INSERT INTO LD (PatientID, LD1, LD2, LD3, LD4, LD5, LD6, LD7, LD8)
VALUES (@PatientID, @LD1, @LD2, @LD3, @LD4, @LD5, @LD6, @LD7, @LD8)
END
GO

/* [DentistX].[dbo].[Insert_LDPL] */

CREATE PROCEDURE [dbo].[Insert_LDPL] (
@PatientID int, @CellAddres int, @ForeColor nvarchar
) AS BEGIN
INSERT INTO LDPL (PatientID, CellAddres, ForeColor)
VALUES (@PatientID, @CellAddres, @ForeColor)
END
GO

/* [DentistX].[dbo].[Insert_LDSTYLE] */

CREATE PROCEDURE [dbo].[Insert_LDSTYLE] (
@PatientID int, @CellAddres int, @BakImg image
) AS BEGIN
INSERT INTO LDSTYLE (PatientID, CellAddres, BakImg)
VALUES (@PatientID, @CellAddres, @BakImg)
END
GO

/* [DentistX].[dbo].[Insert_LU] */

CREATE PROCEDURE [dbo].[Insert_LU] (
@PatientID int, @LU1 nvarchar, @LU2 nvarchar, @LU3 nvarchar, @LU4 nvarchar, @LU5 nvarchar, @LU6 nvarchar, @LU7 nvarchar, @LU8 nvarchar
) AS BEGIN
INSERT INTO LU (PatientID, LU1, LU2, LU3, LU4, LU5, LU6, LU7, LU8)
VALUES (@PatientID, @LU1, @LU2, @LU3, @LU4, @LU5, @LU6, @LU7, @LU8)
END
GO

/* [DentistX].[dbo].[Insert_LUPL] */

CREATE PROCEDURE [dbo].[Insert_LUPL] (
@PatientID int, @CellAddres int, @ForeColor nvarchar
) AS BEGIN
INSERT INTO LUPL (PatientID, CellAddres, ForeColor)
VALUES (@PatientID, @CellAddres, @ForeColor)
END
GO

/* [DentistX].[dbo].[Insert_LUSTYLE] */

CREATE PROCEDURE [dbo].[Insert_LUSTYLE] (
@PatientID int, @CellAddres int, @BakImg image
) AS BEGIN
INSERT INTO LUSTYLE (PatientID, CellAddres, BakImg)
VALUES (@PatientID, @CellAddres, @BakImg)
END
GO

/* [DentistX].[dbo].[Insert_M_TRT] */

CREATE PROCEDURE [dbo].[Insert_M_TRT] (
@MNo int, @MName nvarchar, @MTrt money, @MPay money, @MRemain money
) AS BEGIN
INSERT INTO M_TRT (MNo, MName, MTrt, MPay, MRemain)
VALUES (@MNo, @MName, @MTrt, @MPay, @MRemain)
END
GO

/* [DentistX].[dbo].[Insert_MedicineDoze] */

CREATE PROCEDURE [dbo].[Insert_MedicineDoze] (
@ShapeID int, @Doze nvarchar
) AS BEGIN
INSERT INTO MedicineDoze (ShapeID, Doze)
VALUES (@ShapeID, @Doze)
END
GO

/* [DentistX].[dbo].[Insert_MedicineFamily] */

CREATE PROCEDURE [dbo].[Insert_MedicineFamily] (
@MedicineID int, @MedicineSubCat nvarchar
) AS BEGIN
INSERT INTO MedicineFamily (MedicineID, MedicineSubCat)
VALUES (@MedicineID, @MedicineSubCat)
END
GO

/* [DentistX].[dbo].[Insert_MedicineGroups] */

CREATE PROCEDURE [dbo].[Insert_MedicineGroups] (
@MedicineFamily nvarchar
) AS BEGIN
INSERT INTO MedicineGroups (MedicineFamily)
VALUES (@MedicineFamily)
END
GO

/* [DentistX].[dbo].[Insert_MedicineItems] */

CREATE PROCEDURE [dbo].[Insert_MedicineItems] (
@ScincID int, @CommName nvarchar, @Company nvarchar, @Notes nvarchar
) AS BEGIN
INSERT INTO MedicineItems (ScincID, CommName, Company, Notes)
VALUES (@ScincID, @CommName, @Company, @Notes)
END
GO

/* [DentistX].[dbo].[Insert_MedicineShape] */

CREATE PROCEDURE [dbo].[Insert_MedicineShape] (
@MedicineItemID int, @MedicineShape nvarchar, @ShapeInfo nvarchar
) AS BEGIN
INSERT INTO MedicineShape (MedicineItemID, MedicineShape, ShapeInfo)
VALUES (@MedicineItemID, @MedicineShape, @ShapeInfo)
END
GO

/* [DentistX].[dbo].[Insert_MedScienceFamily] */

CREATE PROCEDURE [dbo].[Insert_MedScienceFamily] (
@SubCatID int, @ScienceName nvarchar
) AS BEGIN
INSERT INTO MedScienceFamily (SubCatID, ScienceName)
VALUES (@SubCatID, @ScienceName)
END
GO

/* [DentistX].[dbo].[Insert_OrthoDiag] */

CREATE PROCEDURE [dbo].[Insert_OrthoDiag] (
@PatientID int, @CloseType nvarchar, @ClassI nvarchar, @Bite nvarchar
) AS BEGIN
INSERT INTO OrthoDiag (PatientID, CloseType, ClassI, Bite)
VALUES (@PatientID, @CloseType, @ClassI, @Bite)
END
GO

/* [DentistX].[dbo].[Insert_OrthoInf] */

CREATE PROCEDURE [dbo].[Insert_OrthoInf] (
@PatientID int, @Compliants nvarchar, @Birth nvarchar, @Feed nvarchar, @MilkTeethChng nvarchar, @MilkTeethAppear nvarchar, @TeethLoss nvarchar, @BurriedTeeth nvarchar, @OverLoadTeeth nvarchar, @LipsCut nvarchar, @ThroatCut nvarchar, @IllnesPeriod nvarchar, @CousinsHFactor nvarchar, @BadHabits nvarchar, @Malfunction nvarchar, @Khota nvarchar, @PrevOrth nvarchar, @PrevIll nvarchar
) AS BEGIN
INSERT INTO OrthoInf (PatientID, Compliants, Birth, Feed, MilkTeethChng, MilkTeethAppear, TeethLoss, BurriedTeeth, OverLoadTeeth, LipsCut, ThroatCut, IllnesPeriod, CousinsHFactor, BadHabits, Malfunction, Khota, PrevOrth, PrevIll)
VALUES (@PatientID, @Compliants, @Birth, @Feed, @MilkTeethChng, @MilkTeethAppear, @TeethLoss, @BurriedTeeth, @OverLoadTeeth, @LipsCut, @ThroatCut, @IllnesPeriod, @CousinsHFactor, @BadHabits, @Malfunction, @Khota, @PrevOrth, @PrevIll)
END
GO

/* [DentistX].[dbo].[insert_OrthoTreat] */

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

/* [DentistX].[dbo].[Insert_OrthoTrtDet] */

CREATE PROCEDURE [dbo].[Insert_OrthoTrtDet] (
@PatientID int, @WorkDate smalldatetime, @WireMeasure nvarchar, @WireType nvarchar, @WireImg nvarchar
) AS BEGIN
INSERT INTO OrthoTrtDet (PatientID, WorkDate, WireMeasure, WireType, WireImg)
VALUES (@PatientID, @WorkDate, @WireMeasure, @WireType, @WireImg)
END
GO

/* [DentistX].[dbo].[Insert_OutDr] */

CREATE PROCEDURE [dbo].[Insert_OutDr] (
@DrName nvarchar, @DrAdres nvarchar, @Drphone char, @DrMobile char
) AS BEGIN
INSERT INTO OutDr (DrName, DrAdres, Drphone, DrMobile)
VALUES (@DrName, @DrAdres, @Drphone, @DrMobile)
END
GO

/* [DentistX].[dbo].[Insert_Patient] */

CREATE PROCEDURE [dbo].[Insert_Patient] (
@PatientName nvarchar, @Sex nvarchar, @Age int, @Phone nvarchar, @Address nvarchar, @Health nvarchar, @Treat bit, @Implant bit, @Mobile bit, @Ortho bit, @Struc bit, @Notes nvarchar, @BirthY int, @CreatedBy int, @CreateDate datetime
) AS BEGIN
INSERT INTO Patient (PatientName, Sex, Age, Phone, Address, Health, Treat, Implant, Mobile, Ortho, Struc, Notes, BirthY, CreatedBy, CreateDate)
VALUES (@PatientName, @Sex, @Age, @Phone, @Address, @Health, @Treat, @Implant, @Mobile, @Ortho, @Struc, @Notes, @BirthY, @CreatedBy, @CreateDate)
END
GO

/* [DentistX].[dbo].[Insert_Patient_Chart] */

CREATE PROCEDURE [dbo].[Insert_Patient_Chart] (
@PatientID int, @ToothNum tinyint, @ToothName nvarchar, @IsExist bit, @HasImp bit, @ImpType nvarchar, @ImpName nvarchar, @ImpDate datetime, @CrownType nvarchar, @CrownName nvarchar, @CrownDate datetime, @TrtDate datetime, @TrtName nvarchar, @ClassID tinyint, @ClassName nvarchar, @TrtStage nvarchar, @TrtDetails nvarchar, @TrtNotes nvarchar
) AS BEGIN
INSERT INTO Patient_Chart (PatientID, ToothNum, ToothName, IsExist, HasImp, ImpType, ImpName, ImpDate, CrownType, CrownName, CrownDate, TrtDate, TrtName, ClassID, ClassName, TrtStage, TrtDetails, TrtNotes)
VALUES (@PatientID, @ToothNum, @ToothName, @IsExist, @HasImp, @ImpType, @ImpName, @ImpDate, @CrownType, @CrownName, @CrownDate, @TrtDate, @TrtName, @ClassID, @ClassName, @TrtStage, @TrtDetails, @TrtNotes)
END
GO

/* [DentistX].[dbo].[Insert_Patient_ChartCheck] */

CREATE PROCEDURE [dbo].[Insert_Patient_ChartCheck] (
@ToothNum tinyint, @PatientID int, @ToothName nvarchar, @IsExist tinyint, @CL1 tinyint, @CL2 tinyint, @CL3 tinyint, @CL4 tinyint, @CL5 tinyint, @CL6 tinyint, @CL7 tinyint, @CL8 tinyint, @CL9 tinyint, @CL10 tinyint, @CL11 tinyint, @CL12 tinyint, @CL13 tinyint, @CL14 tinyint, @CheckDate datetime, @CheckNotes nvarchar
) AS BEGIN
INSERT INTO Patient_ChartCheck (ToothNum, PatientID, ToothName, IsExist, CL1, CL2, CL3, CL4, CL5, CL6, CL7, CL8, CL9, CL10, CL11, CL12, CL13, CL14, CheckDate, CheckNotes)
VALUES (@ToothNum, @PatientID, @ToothName, @IsExist, @CL1, @CL2, @CL3, @CL4, @CL5, @CL6, @CL7, @CL8, @CL9, @CL10, @CL11, @CL12, @CL13, @CL14, @CheckDate, @CheckNotes)
END
GO

/* [DentistX].[dbo].[Insert_Patient_Imgs] */

CREATE PROCEDURE [dbo].[Insert_Patient_Imgs] (
@PatientID int, @PicName nvarchar, @PicPath nvarchar, @FullName nvarchar
) AS BEGIN
INSERT INTO Patient_Imgs (PatientID, PicName, PicPath, FullName)
VALUES (@PatientID, @PicName, @PicPath, @FullName)
END
GO

/* [DentistX].[dbo].[Insert_Patient_MobStruc] */

CREATE PROCEDURE [dbo].[Insert_Patient_MobStruc] (
@PatientID int, @StrucName nvarchar, @StrucType nvarchar, @TeethType nvarchar, @StrucDate smalldatetime
) AS BEGIN
INSERT INTO Patient_MobStruc (PatientID, StrucName, StrucType, TeethType, StrucDate)
VALUES (@PatientID, @StrucName, @StrucType, @TeethType, @StrucDate)
END
GO

/* [DentistX].[dbo].[Insert_Patient_MobStrucAdd] */

CREATE PROCEDURE [dbo].[Insert_Patient_MobStrucAdd] (
@StrucID int, @StrucName nvarchar, @ToothLoc nvarchar, @ToothNum nvarchar, @AddTothDate smalldatetime
) AS BEGIN
INSERT INTO Patient_MobStrucAdd (StrucID, StrucName, ToothLoc, ToothNum, AddTothDate)
VALUES (@StrucID, @StrucName, @ToothLoc, @ToothNum, @AddTothDate)
END
GO

/* [DentistX].[dbo].[Insert_Patient_Notes] */

CREATE PROCEDURE [dbo].[Insert_Patient_Notes] (
@PatientID int, @NoteDate smalldatetime, @Note nvarchar
) AS BEGIN
INSERT INTO Patient_Notes (PatientID, NoteDate, Note)
VALUES (@PatientID, @NoteDate, @Note)
END
GO

/* [DentistX].[dbo].[Insert_Patient_Pays] */

CREATE PROCEDURE [dbo].[Insert_Patient_Pays] (
@TrtID int, @PatientID int, @PayValue money, @PayDate smalldatetime, @Notes nvarchar
) AS BEGIN
INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes)
VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes)
END
GO

/* [DentistX].[dbo].[Insert_Patient_RX] */

CREATE PROCEDURE [dbo].[Insert_Patient_RX] (
@PatientID int, @RXDate smalldatetime, @RX nvarchar
) AS BEGIN
INSERT INTO Patient_RX (PatientID, RXDate, RX)
VALUES (@PatientID, @RXDate, @RX)
END
GO

/* [DentistX].[dbo].[Insert_Patient_ToothCheck] */

CREATE PROCEDURE [dbo].[Insert_Patient_ToothCheck] (
@ToothNum tinyint, @PatientID int, @ToothName nvarchar, @IsExist tinyint, @COM_1 tinyint, @COM_2M tinyint, @COM_2D tinyint, @COM_2MOD tinyint, @COM_3M tinyint, @COM_4 tinyint, @COM_5 tinyint, @COM_FACING tinyint, @COM_3D tinyint, @RCT tinyint, @RCC tinyint, @RCC_NS tinyint, @RCF tinyint, @RCF_COM tinyint, @EXTRACTION tinyint, @PULPOTOMY tinyint, @PULPECTOMY tinyint, @INDIRECT_PC tinyint, @DIRECT_PC tinyint, @FIBER_POST tinyint, @TF tinyint, @REDO_RCC tinyint, @AMALGM tinyint, @RCT_NECROTIC tinyint, @PULPOTOMY_MTA tinyint, @ABCESS_DRAINAGE tinyint, @MTA_Bulk_Flow tinyint, @Build_Up_Com tinyint, @Build_Up_Acr tinyint, @Build_Up_GI tinyint, @GI_M tinyint, @GI tinyint, @GI_D tinyint, @CheckDate datetime, @CheckNotes nvarchar
) AS BEGIN
INSERT INTO Patient_ToothCheck (ToothNum, PatientID, ToothName, IsExist, COM_1, COM_2M, COM_2D, COM_2MOD, COM_3M, COM_4, COM_5, COM_FACING, COM_3D, RCT, RCC, RCC_NS, RCF, RCF_COM, EXTRACTION, PULPOTOMY, PULPECTOMY, INDIRECT_PC, DIRECT_PC, FIBER_POST, TF, REDO_RCC, AMALGM, RCT_NECROTIC, PULPOTOMY_MTA, ABCESS_DRAINAGE, MTA_Bulk_Flow, Build_Up_Com, Build_Up_Acr, Build_Up_GI, GI_M, GI, GI_D, CheckDate, CheckNotes)
VALUES (@ToothNum, @PatientID, @ToothName, @IsExist, @COM_1, @COM_2M, @COM_2D, @COM_2MOD, @COM_3M, @COM_4, @COM_5, @COM_FACING, @COM_3D, @RCT, @RCC, @RCC_NS, @RCF, @RCF_COM, @EXTRACTION, @PULPOTOMY, @PULPECTOMY, @INDIRECT_PC, @DIRECT_PC, @FIBER_POST, @TF, @REDO_RCC, @AMALGM, @RCT_NECROTIC, @PULPOTOMY_MTA, @ABCESS_DRAINAGE, @MTA_Bulk_Flow, @Build_Up_Com, @Build_Up_Acr, @Build_Up_GI, @GI_M, @GI, @GI_D, @CheckDate, @CheckNotes)
END
GO

/* [DentistX].[dbo].[Insert_Patient_Trts] */

CREATE PROCEDURE [dbo].[Insert_Patient_Trts] (
@PatientID int, @Detail nvarchar, @TrtDate smalldatetime, @TrtValue money
) AS BEGIN
INSERT INTO Patient_Trts (PatientID, Detail, TrtDate, TrtValue)
VALUES (@PatientID, @Detail, @TrtDate, @TrtValue)
END
GO

/* [DentistX].[dbo].[Insert_PatientColors] */

CREATE PROCEDURE [dbo].[Insert_PatientColors] (
@Color1 nvarchar, @Color2 nvarchar, @GradientIndex tinyint, @AlphaValue int, @PatientID int
) AS BEGIN
INSERT INTO PatientColors (Color1, Color2, GradientIndex, AlphaValue, PatientID)
VALUES (@Color1, @Color2, @GradientIndex, @AlphaValue, @PatientID)
END
GO

/* [DentistX].[dbo].[Insert_Raseed] */

CREATE PROCEDURE [dbo].[Insert_Raseed] (
@PatientID int
) AS BEGIN
INSERT INTO Raseed (PatientID)
VALUES (@PatientID)
END
GO

/* [DentistX].[dbo].[Insert_RD] */

CREATE PROCEDURE [dbo].[Insert_RD] (
@PatientID int, @RD1 nvarchar(50), @RD2 nvarchar(50), @RD3 nvarchar(50), @RD4 nvarchar(50), @RD5 nvarchar(50), @RD6 nvarchar(50), @RD7 nvarchar(50), @RD8 nvarchar(50)
) AS BEGIN
INSERT INTO RD (PatientID, RD1, RD2, RD3, RD4, RD5, RD6, RD7, RD8)
VALUES (@PatientID, @RD1, @RD2, @RD3, @RD4, @RD5, @RD6, @RD7, @RD8)
END
GO

/* [DentistX].[dbo].[Insert_RDPL] */

CREATE PROCEDURE [dbo].[Insert_RDPL] (
@PatientID int, @CellAddres int, @ForeColor nvarchar(50)
) AS BEGIN
INSERT INTO RDPL (PatientID, CellAddres, ForeColor)
VALUES (@PatientID, @CellAddres, @ForeColor)
END
GO

/* [DentistX].[dbo].[Insert_RDSTYLE] */

CREATE PROCEDURE [dbo].[Insert_RDSTYLE] (
@PatientID int, @CellAddres int, @BakImg image
) AS BEGIN
INSERT INTO RDSTYLE (PatientID, CellAddres, BakImg)
VALUES (@PatientID, @CellAddres, @BakImg)
END
GO

/* [DentistX].[dbo].[Insert_Resources] */

CREATE PROCEDURE [dbo].[Insert_Resources] (
@ResourceID int, @ResourceName nvarchar(50), @Color int, @Image image, @CustomField1 nvarchar(MAX)
) AS BEGIN
INSERT INTO Resources (ResourceID, ResourceName, Color, Image, CustomField1)
VALUES (@ResourceID, @ResourceName, @Color, @Image, @CustomField1)
END
GO

/* [DentistX].[dbo].[Insert_rs] */

CREATE PROCEDURE [dbo].[Insert_rs] (
@UsName nvarchar(50), @UsPass nvarchar(50), @UsLvl int, @UsGrp nvarchar(50)
) AS BEGIN
INSERT INTO rs (UsName, UsPass, UsLvl, UsGrp)
VALUES (@UsName, @UsPass, @UsLvl, @UsGrp)
END
GO

/* [DentistX].[dbo].[Insert_RU] */

CREATE PROCEDURE [dbo].[Insert_RU] (
@PatientID int, @RU1 nvarchar(50), @RU2 nvarchar(50), @RU3 nvarchar(50), @RU4 nvarchar(50), @RU5 nvarchar(50), @RU6 nvarchar(50), @RU7 nvarchar(50), @RU8 nvarchar(50)
) AS BEGIN
INSERT INTO RU (PatientID, RU1, RU2, RU3, RU4, RU5, RU6, RU7, RU8)
VALUES (@PatientID, @RU1, @RU2, @RU3, @RU4, @RU5, @RU6, @RU7, @RU8)
END
GO

/* [DentistX].[dbo].[Insert_RUPL] */

CREATE PROCEDURE [dbo].[Insert_RUPL] (
@PatientID int, @CellAddres int, @ForeColor nvarchar(50)
) AS BEGIN
INSERT INTO RUPL (PatientID, CellAddres, ForeColor)
VALUES (@PatientID, @CellAddres, @ForeColor)
END
GO

/* [DentistX].[dbo].[Insert_RUSTYLE] */

CREATE PROCEDURE [dbo].[Insert_RUSTYLE] (
@PatientID int, @CellAddres int, @BakImg image
) AS BEGIN
INSERT INTO RUSTYLE (PatientID, CellAddres, BakImg)
VALUES (@PatientID, @CellAddres, @BakImg)
END
GO

/* [DentistX].[dbo].[Insert_RxBody] */

CREATE PROCEDURE [dbo].[Insert_RxBody] (
@RxBdyID int, @ArHdrName nvarchar(50), @ArHdrAdres nvarchar(100), @EnHdrName nvarchar(50), @EnHdrAdres nvarchar(100), @Logo image, @Detail nvarchar(150), @ArFtr nvarchar(50), @EnFtr nvarchar(50), @WtrImg image, @WtrText nvarchar(50), @UseWtrImg bit, @UseWtrText BIT,@DrName  nvarchar(50)
) AS BEGIN
INSERT INTO RxBody (RxBdyID, ArHdrName, ArHdrAdres, EnHdrName, EnHdrAdres, Logo, Detail, ArFtr, EnFtr, WtrImg, WtrText, UseWtrImg, UseWtrText,DrName)
VALUES (@RxBdyID, @ArHdrName, @ArHdrAdres, @EnHdrName, @EnHdrAdres, @Logo, @Detail, @ArFtr, @EnFtr, @WtrImg, @WtrText, @UseWtrImg, @UseWtrText,@DrName)
END
GO

/* [DentistX].[dbo].[Insert_RxFly] */

CREATE PROCEDURE [dbo].[Insert_RxFly] (
@PatientName nvarchar(50), @PatientAge int, @RxDate smalldatetime, @RX nvarchar(500)
) AS BEGIN
INSERT INTO RxFly (PatientName, PatientAge, RxDate, RX)
VALUES (@PatientName, @PatientAge, @RxDate, @RX)
END
GO

/* [DentistX].[dbo].[Insert_Surgery] */

CREATE PROCEDURE [dbo].[Insert_Surgery] (
@PatientID int, @SurgeryDet nvarchar(50), @SurDate smalldatetime
) AS BEGIN
INSERT INTO Surgery (PatientID, SurgeryDet, SurDate)
VALUES (@PatientID, @SurgeryDet, @SurDate)
END
GO

/* [DentistX].[dbo].[Insert_TblBnodMsareef] */

CREATE PROCEDURE [dbo].[Insert_TblBnodMsareef] (
@BandName nvarchar(50)
) AS BEGIN
INSERT INTO TblBnodMsareef (BandName)
VALUES (@BandName)
END
GO

/* [DentistX].[dbo].[Insert_TblCategories] */

CREATE PROCEDURE [dbo].[Insert_TblCategories] (
@CategoryName nvarchar(75), @ParentCategory int
) AS BEGIN
INSERT INTO TblCategories (CategoryName, ParentCategory)
VALUES (@CategoryName, @ParentCategory)
END
GO

/* [DentistX].[dbo].[Insert_TblCities] */

CREATE PROCEDURE [dbo].[Insert_TblCities] (
@CityName nvarchar(50)
) AS BEGIN
INSERT INTO TblCities (CityName)
VALUES (@CityName)
END
GO

/* [DentistX].[dbo].[Insert_TblCustomers] */

CREATE PROCEDURE [dbo].[Insert_TblCustomers] (
@CusName nvarchar(75), @CityID int, @Address nvarchar(500), @Contacts nvarchar(500)
) AS BEGIN
INSERT INTO TblCustomers (CusName, CityID, Address, Contacts)
VALUES (@CusName, @CityID, @Address, @Contacts)
END
GO

/* [DentistX].[dbo].[Insert_TblExpenPay] */

CREATE PROCEDURE [dbo].[Insert_TblExpenPay] (
@MasrofID int, @PayValue money, @PayDate datetime, @Notes nvarchar(50)
) AS BEGIN
INSERT INTO TblExpenPay (MasrofID, PayValue, PayDate, Notes)
VALUES (@MasrofID, @PayValue, @PayDate, @Notes)
END
GO

/* [DentistX].[dbo].[Insert_TblInvoiceBody] */

CREATE PROCEDURE [dbo].[Insert_TblInvoiceBody] (
@InvID int, @ItemID int, @Quantity float, @Price money, @ItemHasm money, @Note nvarchar(100)
) AS BEGIN
INSERT INTO TblInvoiceBody (InvID, ItemID, Quantity, Price, ItemHasm, Note)
VALUES (@InvID, @ItemID, @Quantity, @Price, @ItemHasm, @Note)
END
GO

/* [DentistX].[dbo].[Insert_TblInvoicesHeader] */

CREATE PROCEDURE [dbo].[Insert_TblInvoicesHeader] (
@InvoiceType tinyint, @InvoiceDate smalldatetime, @ResID int, @DocNo int, @InvoiceEx nvarchar(500), @Hasm money
) AS BEGIN
INSERT INTO TblInvoicesHeader (InvoiceType, InvoiceDate, ResID, DocNo, InvoiceEx, Hasm)
VALUES (@InvoiceType, @InvoiceDate, @ResID, @DocNo, @InvoiceEx, @Hasm)
END
GO

/* [DentistX].[dbo].[Insert_TblInvPay] */

CREATE PROCEDURE [dbo].[Insert_TblInvPay] (
@InvoiceID int, @ResID int, @PayDate datetime, @Amount money, @Notes nvarchar(50)
) AS BEGIN
INSERT INTO TblInvPay (InvoiceID, ResID, PayDate, Amount, Notes)
VALUES (@InvoiceID, @ResID, @PayDate, @Amount, @Notes)
END
GO

/* [DentistX].[dbo].[Insert_TblItems] */

CREATE PROCEDURE [dbo].[Insert_TblItems] (
@ItemName nvarchar(75), @ItemEx nvarchar(500), @CatID int, @UnitID int, @LastPrice money, @QuantityNow float
) AS BEGIN
INSERT INTO TblItems (ItemName, ItemEx, CatID, UnitID, LastPrice, QuantityNow)
VALUES (@ItemName, @ItemEx, @CatID, @UnitID, @LastPrice, @QuantityNow)
END
GO

/* [DentistX].[dbo].[Insert_TblMasareef] */

CREATE PROCEDURE [dbo].[Insert_TblMasareef] (
@MasrofDate smalldatetime, @BandID int, @MasrofAmount money, @MasrofEx nvarchar(500)
) AS BEGIN
INSERT INTO TblMasareef (MasrofDate, BandID, MasrofAmount, MasrofEx)
VALUES (@MasrofDate, @BandID, @MasrofAmount, @MasrofEx)
END
GO

/* [DentistX].[dbo].[Insert_TblMeasure] */

CREATE PROCEDURE [dbo].[Insert_TblMeasure] (
@Measure nvarchar(50)
) AS BEGIN
INSERT INTO TblMeasure (Measure)
VALUES (@Measure)
END
GO

/* [DentistX].[dbo].[Insert_TblPaids] */

CREATE PROCEDURE [dbo].[Insert_TblPaids] (
@PayType nvarchar(5), @PayDate smalldatetime, @ResCusId int, @PayAmount money, @PayEx nvarchar(300)
) AS BEGIN
INSERT INTO TblPaids (PayType, PayDate, ResCusId, PayAmount, PayEx)
VALUES (@PayType, @PayDate, @ResCusId, @PayAmount, @PayEx)
END
GO

/* [DentistX].[dbo].[Insert_TblResources] */

CREATE PROCEDURE [dbo].[Insert_TblResources] (
@ResName nvarchar(75), @CityID int, @Address nvarchar(500), @Contacts nvarchar(500)
) AS BEGIN
INSERT INTO TblResources (ResName, CityID, Address, Contacts)
VALUES (@ResName, @CityID, @Address, @Contacts)
END
GO

/* [DentistX].[dbo].[Insert_TblSalesBody] */

CREATE PROCEDURE [dbo].[Insert_TblSalesBody] (
@SaleID int, @ItemID int, @Quantity float, @Price money, @ItemHasm money, @Note nvarchar(100)
) AS BEGIN
INSERT INTO TblSalesBody (SaleID, ItemID, Quantity, Price, ItemHasm, Note)
VALUES (@SaleID, @ItemID, @Quantity, @Price, @ItemHasm, @Note)
END
GO

/* [DentistX].[dbo].[Insert_TblSalesHeader] */

CREATE PROCEDURE [dbo].[Insert_TblSalesHeader] (
@SaleType tinyint, @SaleDate smalldatetime, @CusID int, @DocNo int, @SaleEx nvarchar(500), @Hasm money
) AS BEGIN
INSERT INTO TblSalesHeader (SaleType, SaleDate, CusID, DocNo, SaleEx, Hasm)
VALUES (@SaleType, @SaleDate, @CusID, @DocNo, @SaleEx, @Hasm)
END
GO

/* [DentistX].[dbo].[Insert_TblTRT] */

CREATE PROCEDURE [dbo].[Insert_TblTRT] (
@Trt nvarchar(50)
) AS BEGIN
INSERT INTO TblTRT (Trt)
VALUES (@Trt)
END
GO

/* [DentistX].[dbo].[Insert_TblUnits] */

CREATE PROCEDURE [dbo].[Insert_TblUnits] (
@UnitName nvarchar(50)
) AS BEGIN
INSERT INTO TblUnits (UnitName)
VALUES (@UnitName)
END
GO

/* [DentistX].[dbo].[Insert_TblWireType] */

CREATE PROCEDURE [dbo].[Insert_TblWireType] (
@WireType nvarchar(50)
) AS BEGIN
INSERT INTO TblWireType (WireType)
VALUES (@WireType)
END
GO

/* [DentistX].[dbo].[Insert_Users] */

CREATE PROCEDURE [dbo].[Insert_Users] (
@UsName nvarchar(50), @UsPass nvarchar(50), @UsLvl int, @UsGrp nvarchar(50), @CreatedBy int, @LastAccby int, @LastUpdate datetime
) AS BEGIN
INSERT INTO Users (UsName, UsPass, UsLvl, UsGrp, CreatedBy, LastAccby, LastUpdate)
VALUES (@UsName, @UsPass, @UsLvl, @UsGrp, @CreatedBy, @LastAccby, @LastUpdate)
END
GO

/* [DentistX].[dbo].[Insert_VisitType] */

CREATE PROCEDURE [dbo].[Insert_VisitType] (
@VisType nvarchar(200)
) AS BEGIN
INSERT INTO VisitType (VisType)
VALUES (@VisType)
END
GO

/* [DentistX].[dbo].[Insert_Y_TRT] */

CREATE PROCEDURE [dbo].[Insert_Y_TRT] (
@YName int
) AS BEGIN
INSERT INTO Y_TRT (YName)
VALUES (@YName)
END
GO

/* [DentistX].[dbo].[InvBdyNet] */

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

/* [DentistX].[dbo].[InvBdyView] */

CREATE VIEW [dbo].[InvBdyView]
AS
SELECT        dbo.TblInvoiceBody.InvId, dbo.TblItems.ItemId, dbo.TblItems.ItemName, dbo.TblCategories.CategoryId, dbo.TblCategories.CategoryName, dbo.TblUnits.UnitId, dbo.TblUnits.UnitName, dbo.TblInvoiceBody.Quantity, 
                         dbo.TblInvoiceBody.Price, dbo.TblInvoiceBody.ItemHasm, dbo.TblInvoiceBody.Note
FROM            dbo.TblCategories INNER JOIN
                         dbo.TblItems ON dbo.TblCategories.CategoryId = dbo.TblItems.CatId INNER JOIN
                         dbo.TblInvoiceBody ON dbo.TblItems.ItemId = dbo.TblInvoiceBody.ItemId INNER JOIN
                         dbo.TblUnits ON dbo.TblItems.UnitId = dbo.TblUnits.UnitId
GO

/* [DentistX].[dbo].[InvBodyVW] */

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

/* [DentistX].[dbo].[InvHdrVW] */

CREATE VIEW [dbo].[InvHdrVW]
AS
SELECT        dbo.TblInvoicesHeader.InvoiceId, dbo.TblInvoicesHeader.InvoiceType, dbo.TblInvoicesHeader.InvoiceDate, dbo.TblResources.ResId, dbo.TblResources.ResName, dbo.TblInvoicesHeader.DocNo, 
                         dbo.TblInvoicesHeader.InvoiceEx, dbo.TblInvoicesHeader.Hasm
FROM            dbo.TblInvoicesHeader INNER JOIN
                         dbo.TblResources ON dbo.TblInvoicesHeader.ResId = dbo.TblResources.ResId
GO

/* [DentistX].[dbo].[InvNet] */

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

/* [DentistX].[dbo].[InvPayments] */

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

/* [DentistX].[dbo].[InvTotalPays] */

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

/* [DentistX].[dbo].[InvVIEW] */

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

/* [DentistX].[dbo].[ItmTotlDisc] */

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

/* [DentistX].[dbo].[ItmTotlPrice] */

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

/* [DentistX].[dbo].[ItmTotlQuant] */

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

/* [DentistX].[dbo].[Lab] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[LabDelete] */

CREATE PROC [dbo].[LabDelete] 
    @LabID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Lab]
	WHERE  [LabID] = @LabID

	COMMIT
GO

/* [DentistX].[dbo].[LabInsert] */

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

/* [DentistX].[dbo].[LabOrder] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LabOrder](
	[LabOrderID] [int] IDENTITY(1,1) NOT NULL,
	[LabID] [int] NOT NULL,
	[PatientID] [int] NOT NULL,
	[ImprType] [nvarchar](50) NOT NULL,
	[ImprDet] [nvarchar](50) NOT NULL,
	[ImprClr] [nvarchar](50) NOT NULL,
	[ImprCount] [int] NOT NULL,
	[DeliveryDate] [datetime] NULL,
	[Price] [int] NOT NULL,
	[RecieveDate] [datetime] NULL,
	[Notes] [nvarchar](50) NULL,
 CONSTRAINT [PK_LabOrder] PRIMARY KEY CLUSTERED 
(
	[LabOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

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
GO

/* [DentistX].[dbo].[LabOrderDelete] */

CREATE PROC [dbo].[LabOrderDelete] 
    @LabOrderID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[LabOrder]
	WHERE  [LabOrderID] = @LabOrderID

	COMMIT
GO

/* [DentistX].[dbo].[LabOrderInsert] */

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

/* [DentistX].[dbo].[LabOrderRecUpdate] */

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

/* [DentistX].[dbo].[LabOrderSelect] */

CREATE PROC [dbo].[LabOrderSelect] 
    @LabOrderID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LabOrderID], [LabID], [PatientID], [ImprType], [ImprDet], [ImprClr], [ImprCount], [DeliveryDate], [Price], [RecieveDate], [Notes] 
	FROM   [dbo].[LabOrder] 
	WHERE  ([LabOrderID] = @LabOrderID OR @LabOrderID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[LabOrderSendInsert] */

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

/* [DentistX].[dbo].[LabOrderUpdate] */

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

/* [DentistX].[dbo].[LabPay] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[LabPay]  WITH CHECK ADD  CONSTRAINT [FK_LabPay_Lab] FOREIGN KEY([LabID])
REFERENCES [dbo].[Lab] ([LabID])
GO

ALTER TABLE [dbo].[LabPay] CHECK CONSTRAINT [FK_LabPay_Lab]
GO
GO

/* [DentistX].[dbo].[LabPayDelete] */

CREATE PROC [dbo].[LabPayDelete] 
    @LabPayID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[LabPay]
	WHERE  [LabPayID] = @LabPayID

	COMMIT
GO

/* [DentistX].[dbo].[LabPayInsert] */

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

/* [DentistX].[dbo].[LabPaySelect] */

CREATE PROC [dbo].[LabPaySelect] 
    @LabPayID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LabPayID], [LabID], [LabOrderID], [PayValue], [PayDate], [PayDetail] 
	FROM   [dbo].[LabPay] 
	WHERE  ([LabPayID] = @LabPayID OR @LabPayID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[LabPayUpdate] */

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

/* [DentistX].[dbo].[LabSelect] */

CREATE PROC [dbo].[LabSelect] 
    @LabID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LabID], [LabName], [Adres], [Phone], [Mobile] 
	FROM   [dbo].[Lab] 
	WHERE  ([LabID] = @LabID OR @LabID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[LabUpdate] */

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

/* [DentistX].[dbo].[LD] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[LD]  WITH NOCHECK ADD  CONSTRAINT [FK_LD_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[LD] CHECK CONSTRAINT [FK_LD_Patient]
GO
GO

/* [DentistX].[dbo].[LDDelete] */

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

/* [DentistX].[dbo].[LDInsert] */

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

/* [DentistX].[dbo].[LDPL] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[LDPL_IU] */

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

/* [DentistX].[dbo].[LDPLDelete] */

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

/* [DentistX].[dbo].[LDPLInsert] */

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

/* [DentistX].[dbo].[LDPLSelect] */

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

/* [DentistX].[dbo].[LDPLUpdate] */

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

/* [DentistX].[dbo].[LDSelect] */

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

/* [DentistX].[dbo].[LDSTYLE] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[LDSTYLE]  WITH NOCHECK ADD  CONSTRAINT [FK_LDSTYLE_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[LDSTYLE] CHECK CONSTRAINT [FK_LDSTYLE_patient]
GO
GO

/* [DentistX].[dbo].[LDSTYLE_IU] */

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

/* [DentistX].[dbo].[LDSTYLEDelete] */

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

/* [DentistX].[dbo].[LDSTYLEInsert] */

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

/* [DentistX].[dbo].[LDSTYLESelect] */

CREATE PROC [dbo].[LDSTYLESelect] 
    @LDcellID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LDcellID], [PatientID], [CellAddres], [BakImg] 
	FROM   [dbo].[LDSTYLE] 
	WHERE  ([LDcellID] = @LDcellID OR @LDcellID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[LDSTYLEUpdate] */

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

/* [DentistX].[dbo].[LDUpdate] */

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

/* [DentistX].[dbo].[Llect] */

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

/* [DentistX].[dbo].[LstPatientView] */

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

/* [DentistX].[dbo].[LU] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[LU]  WITH NOCHECK ADD  CONSTRAINT [FK_LU_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[LU] CHECK CONSTRAINT [FK_LU_Patient]
GO
GO

/* [DentistX].[dbo].[LUDelete] */

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

/* [DentistX].[dbo].[LUInsert] */

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

/* [DentistX].[dbo].[LUPL] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[LUPL_IU] */

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

/* [DentistX].[dbo].[LUPLDelete] */

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

/* [DentistX].[dbo].[LUPLInsert] */

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

/* [DentistX].[dbo].[LUPLSelect] */

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

/* [DentistX].[dbo].[LUPLUpdate] */

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

/* [DentistX].[dbo].[LUSTYLE] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[LUSTYLE]  WITH NOCHECK ADD  CONSTRAINT [FK_LUSTYLE_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[LUSTYLE] CHECK CONSTRAINT [FK_LUSTYLE_patient]
GO
GO

/* [DentistX].[dbo].[LUSTYLE_IU] */

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

/* [DentistX].[dbo].[LUSTYLEDelete] */

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

/* [DentistX].[dbo].[LUSTYLEInsert] */

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

/* [DentistX].[dbo].[LUSTYLESelect] */

CREATE PROC [dbo].[LUSTYLESelect] 
    @LUcellID int
AS 
	
	 

	BEGIN TRAN

	SELECT [LUcellID], [PatientID], [CellAddres], [BakImg] 
	FROM   [dbo].[LUSTYLE] 
	WHERE  ([LUcellID] = @LUcellID OR @LUcellID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[LUSTYLEUpdate] */

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

/* [DentistX].[dbo].[LUUpdate] */

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

/* [DentistX].[dbo].[MedicineDoze] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicineDoze](
	[DozeID] [int] IDENTITY(1,1) NOT NULL,
	[ShapeID] [int] NOT NULL,
	[Doze] [nvarchar](50) NULL,
 CONSTRAINT [PK_Doze] PRIMARY KEY CLUSTERED 
(
	[DozeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MedicineDoze]  WITH NOCHECK ADD  CONSTRAINT [FK_Doze_MedicShape] FOREIGN KEY([ShapeID])
REFERENCES [dbo].[MedicineShape] ([ShapeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MedicineDoze] CHECK CONSTRAINT [FK_Doze_MedicShape]
GO
GO

/* [DentistX].[dbo].[MedicineDozeDelete] */

CREATE PROC [dbo].[MedicineDozeDelete] 
    @DozeID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedicineDoze]
	WHERE  [DozeID] = @DozeID

	COMMIT
GO

/* [DentistX].[dbo].[MedicineDozeInsert] */

CREATE PROC [dbo].[MedicineDozeInsert] 
    @ShapeID int,
    @Doze nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MedicineDoze] ([ShapeID], [Doze])
	SELECT @ShapeID, @Doze
	    
	COMMIT
GO

/* [DentistX].[dbo].[MedicineDozeSelect] */

CREATE PROC [dbo].[MedicineDozeSelect] 
    @DozeID int
AS 
	
	 

	BEGIN TRAN

	SELECT [DozeID], [ShapeID], [Doze] 
	FROM   [dbo].[MedicineDoze] 
	WHERE  ([DozeID] = @DozeID OR @DozeID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[MedicineDozeUpdate] */

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

/* [DentistX].[dbo].[MedicineFamily] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicineFamily](
	[SubCatID] [int] IDENTITY(1,1) NOT NULL,
	[MedicineID] [int] NOT NULL,
	[MedicineSubCat] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ScienceFamily] PRIMARY KEY CLUSTERED 
(
	[SubCatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MedicineFamily]  WITH CHECK ADD  CONSTRAINT [FK_MedicineFamily_MedicineGroups] FOREIGN KEY([MedicineID])
REFERENCES [dbo].[MedicineGroups] ([MedicineID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MedicineFamily] CHECK CONSTRAINT [FK_MedicineFamily_MedicineGroups]
GO
GO

/* [DentistX].[dbo].[MedicineFamilyDelete] */

CREATE PROC [dbo].[MedicineFamilyDelete] 
    @SubCatID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedicineFamily]
	WHERE  [SubCatID] = @SubCatID

	COMMIT
GO

/* [DentistX].[dbo].[MedicineFamilyInsert] */

CREATE PROC [dbo].[MedicineFamilyInsert] 
    @MedicineID int,
    @MedicineSubCat nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MedicineFamily] ([MedicineID], [MedicineSubCat])
	SELECT @MedicineID, @MedicineSubCat
	   
	COMMIT
GO

/* [DentistX].[dbo].[MedicineFamilySelect] */

CREATE PROC [dbo].[MedicineFamilySelect] 
    @SubCatID int
AS 
	
	 

	BEGIN TRAN

	SELECT [SubCatID], [MedicineID], [MedicineSubCat] 
	FROM   [dbo].[MedicineFamily] 
	WHERE  ([SubCatID] = @SubCatID OR @SubCatID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[MedicineFamilyUpdate] */

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

/* [DentistX].[dbo].[MedicineGroups] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicineGroups](
	[MedicineID] [int] IDENTITY(1,1) NOT NULL,
	[MedicineFamily] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MedicineCat] PRIMARY KEY CLUSTERED 
(
	[MedicineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[MedicineGroupsDelete] */

CREATE PROC [dbo].[MedicineGroupsDelete] 
    @MedicineID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedicineGroups]
	WHERE  [MedicineID] = @MedicineID

	COMMIT
GO

/* [DentistX].[dbo].[MedicineGroupsInsert] */

CREATE PROC [dbo].[MedicineGroupsInsert] 
    @MedicineFamily nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MedicineGroups] ([MedicineFamily])
	SELECT @MedicineFamily
	    
	COMMIT
GO

/* [DentistX].[dbo].[MedicineGroupsSelect] */

CREATE PROC [dbo].[MedicineGroupsSelect] 
    @MedicineID int
AS 
	
	 

	BEGIN TRAN

	SELECT [MedicineID], [MedicineFamily] 
	FROM   [dbo].[MedicineGroups] 
	WHERE  ([MedicineID] = @MedicineID OR @MedicineID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[MedicineGroupsUpdate] */

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

/* [DentistX].[dbo].[MedicineItems] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MedicineItems]  WITH CHECK ADD  CONSTRAINT [FK_MedicineItems_ScienceFamily] FOREIGN KEY([ScincID])
REFERENCES [dbo].[MedScienceFamily] ([ScincID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MedicineItems] CHECK CONSTRAINT [FK_MedicineItems_ScienceFamily]
GO
GO

/* [DentistX].[dbo].[MedicineItemsDelete] */

CREATE PROC [dbo].[MedicineItemsDelete] 
    @MedicineItemID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedicineItems]
	WHERE  [MedicineItemID] = @MedicineItemID

	COMMIT
GO

/* [DentistX].[dbo].[MedicineItemsInsert] */

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

/* [DentistX].[dbo].[MedicineItemsSelect] */

CREATE PROC [dbo].[MedicineItemsSelect] 
    @MedicineItemID int
AS 
	
	 

	BEGIN TRAN

	SELECT [MedicineItemID], [ScincID], [CommName], [Company], [Notes] 
	FROM   [dbo].[MedicineItems] 
	WHERE  ([MedicineItemID] = @MedicineItemID OR @MedicineItemID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[MedicineItemsUpdate] */

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

/* [DentistX].[dbo].[MedicineShape] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicineShape](
	[ShapeID] [int] IDENTITY(1,1) NOT NULL,
	[MedicineItemID] [int] NOT NULL,
	[MedicineShape] [nvarchar](50) NULL,
	[ShapeInfo] [nvarchar](50) NULL,
 CONSTRAINT [PK_MedicShape] PRIMARY KEY CLUSTERED 
(
	[ShapeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MedicineShape]  WITH NOCHECK ADD  CONSTRAINT [FK_MedicShape_MedicineItems] FOREIGN KEY([MedicineItemID])
REFERENCES [dbo].[MedicineItems] ([MedicineItemID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MedicineShape] CHECK CONSTRAINT [FK_MedicShape_MedicineItems]
GO
GO

/* [DentistX].[dbo].[MedicineShapeDelete] */

CREATE PROC [dbo].[MedicineShapeDelete] 
    @ShapeID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedicineShape]
	WHERE  [ShapeID] = @ShapeID

	COMMIT
GO

/* [DentistX].[dbo].[MedicineShapeInsert] */

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

/* [DentistX].[dbo].[MedicineShapeSelect] */

CREATE PROC [dbo].[MedicineShapeSelect] 
    @ShapeID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ShapeID], [MedicineItemID], [MedicineShape], [ShapeInfo] 
	FROM   [dbo].[MedicineShape] 
	WHERE  ([ShapeID] = @ShapeID OR @ShapeID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[MedicineShapeUpdate] */

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

/* [DentistX].[dbo].[MedScienceFamily] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedScienceFamily](
	[ScincID] [int] IDENTITY(1,1) NOT NULL,
	[SubCatID] [int] NOT NULL,
	[ScienceName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ScienceFamily_1] PRIMARY KEY CLUSTERED 
(
	[ScincID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MedScienceFamily]  WITH CHECK ADD  CONSTRAINT [FK_ScienceFamily_MedicineFamily] FOREIGN KEY([SubCatID])
REFERENCES [dbo].[MedicineFamily] ([SubCatID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MedScienceFamily] CHECK CONSTRAINT [FK_ScienceFamily_MedicineFamily]
GO
GO

/* [DentistX].[dbo].[MedScienceFamilyDelete] */

CREATE PROC [dbo].[MedScienceFamilyDelete] 
    @ScincID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MedScienceFamily]
	WHERE  [ScincID] = @ScincID

	COMMIT
GO

/* [DentistX].[dbo].[MedScienceFamilyInsert] */

CREATE PROC [dbo].[MedScienceFamilyInsert] 
    @SubCatID int,
    @ScienceName nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MedScienceFamily] ([SubCatID], [ScienceName])
	SELECT @SubCatID, @ScienceName
	    
	COMMIT
GO

/* [DentistX].[dbo].[MedScienceFamilySelect] */

CREATE PROC [dbo].[MedScienceFamilySelect] 
    @ScincID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ScincID], [SubCatID], [ScienceName] 
	FROM   [dbo].[MedScienceFamily] 
	WHERE  ([ScincID] = @ScincID OR @ScincID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[MedScienceFamilyUpdate] */

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

/* [DentistX].[dbo].[MoveFirstRowToNewRowPerPatient] */

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
        FROM [DentistX].[dbo].[LD]
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
    INSERT INTO [DentistX].[dbo].[LD] ([PatientID], [LD1], [LD2], [LD3], [LD4], [LD5], [LD6], [LD7], [LD8])
    SELECT DISTINCT [PatientID], NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
    FROM [DentistX].[dbo].[LD];

    -- Step 3: Update the new empty rows with the first row's data
    WITH NewEmptyRows AS (
        SELECT 
            [LDID],
            [PatientID],
            ROW_NUMBER() OVER (PARTITION BY [PatientID] ORDER BY [LDID] DESC) AS RowNum
        FROM [DentistX].[dbo].[LD]
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
    FROM [DentistX].[dbo].[LD] LD
    JOIN #FirstRows FR ON LD.[PatientID] = FR.[PatientID]
    JOIN NewEmptyRows NR ON LD.[LDID] = NR.[LDID] AND NR.RowNum = 1;

    -- Step 4: Delete the original first rows
    DELETE FROM [DentistX].[dbo].[LD]
    WHERE [LDID] IN (SELECT [LDID] FROM #FirstRows);

    -- Clean up temporary table
    DROP TABLE #FirstRows;

    SET NOCOUNT OFF;
END;
GO

/* [DentistX].[dbo].[NamesView] */

CREATE VIEW [dbo].[NamesView]
AS
SELECT        PatientID, PatientName, Treat, Implant, Mobile, Ortho, Struc
FROM            dbo.patient
GO

/* [DentistX].[dbo].[NwVisitProc] */

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

/* [DentistX].[dbo].[OrthoDiag] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OrthoDiag]  WITH NOCHECK ADD  CONSTRAINT [FK_OrthoDiag_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrthoDiag] CHECK CONSTRAINT [FK_OrthoDiag_patient]
GO
GO

/* [DentistX].[dbo].[OrthoDiagDelete] */

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

/* [DentistX].[dbo].[OrthoDiagInsert] */

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

/* [DentistX].[dbo].[OrthoDiagSelect] */

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

/* [DentistX].[dbo].[OrthoDiagUpdate] */

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

/* [DentistX].[dbo].[OrthoInf] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OrthoInf]  WITH NOCHECK ADD  CONSTRAINT [FK_OrthoInf_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrthoInf] CHECK CONSTRAINT [FK_OrthoInf_patient]
GO
GO

/* [DentistX].[dbo].[OrthoInfDelete] */

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

/* [DentistX].[dbo].[OrthoInfInsert] */

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

/* [DentistX].[dbo].[OrthoInfSelect] */

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

/* [DentistX].[dbo].[OrthoInfUpdate] */

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

/* [DentistX].[dbo].[OrthoTreat] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OrthoTreat]  WITH NOCHECK ADD  CONSTRAINT [FK_OrthoTreat_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrthoTreat] CHECK CONSTRAINT [FK_OrthoTreat_patient]
GO
GO

/* [DentistX].[dbo].[OrthoTreatDelete] */

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

/* [DentistX].[dbo].[OrthoTreatInsert] */

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

/* [DentistX].[dbo].[OrthoTreatSelect] */

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

/* [DentistX].[dbo].[OrthoTreatUpdate] */

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

/* [DentistX].[dbo].[OrthoTrtDet] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OrthoTrtDet]  WITH NOCHECK ADD  CONSTRAINT [FK_OrthoTrtDet_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrthoTrtDet] CHECK CONSTRAINT [FK_OrthoTrtDet_patient]
GO
GO

/* [DentistX].[dbo].[OrthoTrtDetDelete] */

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

/* [DentistX].[dbo].[OrthoTrtDetInsert] */

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

/* [DentistX].[dbo].[OrthoTrtDetSelect] */

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

/* [DentistX].[dbo].[OrthoTrtDetUpdate] */

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

/* [DentistX].[dbo].[OutDr] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OutDr](
	[DrID] [int] IDENTITY(1,1) NOT NULL,
	[DrName] [nvarchar](50) NOT NULL,
	[DrAdres] [nvarchar](50) NULL,
	[Drphone] [char](10) NULL,
	[DrMobile] [char](10) NULL,
 CONSTRAINT [PK_OutDr] PRIMARY KEY CLUSTERED 
(
	[DrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[OutDrDelete] */

CREATE PROC [dbo].[OutDrDelete] 
    @DrID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[OutDr]
	WHERE  [DrID] = @DrID

	COMMIT
GO

/* [DentistX].[dbo].[OutDrInsert] */

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

/* [DentistX].[dbo].[OutDrSelect] */

CREATE PROC [dbo].[OutDrSelect] 
    @DrID int
AS 
	
	 

	BEGIN TRAN

	SELECT [DrID], [DrName], [DrAdres], [Drphone], [DrMobile] 
	FROM   [dbo].[OutDr] 
	WHERE  ([DrID] = @DrID OR @DrID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[OutDrUpdate] */

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

/* [DentistX].[dbo].[p_DeleteRxBody] */

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

/* [DentistX].[dbo].[p_SaveRxBody] */

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

/* [DentistX].[dbo].[Patient] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Patient](
	[PatientID] [int] IDENTITY(1,1) NOT NULL,
	[PatientName] [nvarchar](50) NOT NULL,
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
	[Notes] [nvarchar](150) NULL,
	[BirthY] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_patient] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [DF_Patient_StillKid]  DEFAULT ((0)) FOR [StillKid]
GO
GO

/* [DentistX].[dbo].[Patient_Imgs] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Patient_Imgs]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_Imgs_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Patient_Imgs] CHECK CONSTRAINT [FK_Patient_Imgs_Patient]
GO
GO

/* [DentistX].[dbo].[Patient_ImgsDelete] */

CREATE PROC [dbo].[Patient_ImgsDelete] 
    @PicID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_Imgs]
	WHERE  [PicID] = @PicID

	COMMIT
GO

/* [DentistX].[dbo].[Patient_ImgsInsert] */

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

/* [DentistX].[dbo].[Patient_ImgsSelect] */

CREATE PROC [dbo].[Patient_ImgsSelect] 
    @PicID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PicID], [PatientID], [PicName], [PicPath], [FullName] 
	FROM   [dbo].[Patient_Imgs] 
	WHERE  ([PicID] = @PicID OR @PicID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[Patient_ImgsUpdate] */

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

/* [DentistX].[dbo].[Patient_Mobile] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Patient_Mobile](
	[MobID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[TeethNums] [nvarchar](50) NULL,
	[BridgeType] [nvarchar](50) NULL,
	[BridgeDate] [datetime] NULL,
 CONSTRAINT [PK_Patient_Mobile] PRIMARY KEY CLUSTERED 
(
	[MobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[Patient_MobInfo] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Patient_MobInfo](
	[MobInfoID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[ToothNum] [tinyint] NOT NULL,
	[ToothName] [nvarchar](100) NOT NULL,
	[Extracted] [tinyint] NOT NULL,
	[Implanted] [tinyint] NOT NULL,
 CONSTRAINT [PK_Patient_MobInfo] PRIMARY KEY CLUSTERED 
(
	[MobInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[Patient_MobStruc] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Patient_MobStruc]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_MobStruc_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Patient_MobStruc] CHECK CONSTRAINT [FK_Patient_MobStruc_patient]
GO
GO

/* [DentistX].[dbo].[Patient_MobStrucAdd] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Patient_MobStrucAdd]  WITH CHECK ADD  CONSTRAINT [FK_Patient_MobStrucAdd_Patient_MobStruc] FOREIGN KEY([StrucID])
REFERENCES [dbo].[Patient_MobStruc] ([StrucID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Patient_MobStrucAdd] CHECK CONSTRAINT [FK_Patient_MobStrucAdd_Patient_MobStruc]
GO
GO

/* [DentistX].[dbo].[Patient_MobStrucAddDelete] */

CREATE PROC [dbo].[Patient_MobStrucAddDelete] 
    @AddTothID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_MobStrucAdd]
	WHERE  [AddTothID] = @AddTothID

	COMMIT
GO

/* [DentistX].[dbo].[Patient_MobStrucAddInsert] */

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

/* [DentistX].[dbo].[Patient_MobStrucAddSelect] */

CREATE PROC [dbo].[Patient_MobStrucAddSelect] 
    @AddTothID int
AS 
	
	 

	BEGIN TRAN

	SELECT [AddTothID], [StrucID], [StrucName], [ToothLoc], [ToothNum], [AddTothDate] 
	FROM   [dbo].[Patient_MobStrucAdd] 
	WHERE  ([AddTothID] = @AddTothID OR @AddTothID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[Patient_MobStrucAddUpdate] */

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

/* [DentistX].[dbo].[Patient_MobStrucDelete] */

CREATE PROC [dbo].[Patient_MobStrucDelete] 
    @StrucID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_MobStruc]
	WHERE  [StrucID] = @StrucID

	COMMIT
GO

/* [DentistX].[dbo].[Patient_MobStrucInsert] */

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

/* [DentistX].[dbo].[Patient_MobStrucSelect] */

CREATE PROC [dbo].[Patient_MobStrucSelect] 
    @StrucID int
AS 
	
	 

	BEGIN TRAN

	SELECT [StrucID], [PatientID], [StrucName], [StrucType], [TeethType], [StrucDate] 
	FROM   [dbo].[Patient_MobStruc] 
	WHERE  ([StrucID] = @StrucID OR @StrucID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[Patient_MobStrucUpdate] */

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

/* [DentistX].[dbo].[Patient_Notes] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Patient_Notes]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_Notes_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Patient_Notes] CHECK CONSTRAINT [FK_Patient_Notes_Patient]
GO
GO

/* [DentistX].[dbo].[Patient_NotesDelete] */

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

/* [DentistX].[dbo].[Patient_NotesInsert] */

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

/* [DentistX].[dbo].[Patient_NotesSelect] */

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

/* [DentistX].[dbo].[Patient_NotesUpdate] */

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

/* [DentistX].[dbo].[Patient_Pays] */

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
 CONSTRAINT [PK_patient_pays] PRIMARY KEY CLUSTERED 
(
	[PayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Patient_Pays]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_Pays_Patient_Trts] FOREIGN KEY([TrtID])
REFERENCES [dbo].[Patient_Trts] ([TrtID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Patient_Pays] CHECK CONSTRAINT [FK_Patient_Pays_Patient_Trts]
GO
GO

/* [DentistX].[dbo].[Patient_PaysDelete] */

CREATE PROC [dbo].[Patient_PaysDelete] 
    @PayID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_Pays]
	WHERE  [PayID] = @PayID

	COMMIT
GO

/* [DentistX].[dbo].[Patient_PaysInsert] */

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

/* [DentistX].[dbo].[Patient_PaysSelect] */

CREATE PROC [dbo].[Patient_PaysSelect] 
    @PayID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PayID], [TrtID], [PatientID], [PayValue], [PayDate], [Notes] 
	FROM   [dbo].[Patient_Pays] 
	WHERE  ([PayID] = @PayID OR @PayID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[Patient_PaysUpdate] */

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

/* [DentistX].[dbo].[Patient_RX] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Patient_RX]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_RX_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Patient_RX] CHECK CONSTRAINT [FK_Patient_RX_Patient]
GO
GO

/* [DentistX].[dbo].[Patient_RXDelete] */

CREATE PROC [dbo].[Patient_RXDelete] 
    @RxID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_RX]
	WHERE  [RxID] = @RxID

	COMMIT
GO

/* [DentistX].[dbo].[Patient_RXInsert] */

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

/* [DentistX].[dbo].[Patient_RXSelect] */

CREATE PROC [dbo].[Patient_RXSelect] 
    @RxID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RxID], [PatientID], [RXDate], [RX] 
	FROM   [dbo].[Patient_RX] 
	WHERE  ([RxID] = @RxID OR @RxID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[Patient_RXUpdate] */

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

/* [DentistX].[dbo].[Patient_ToothChart] */

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
 CONSTRAINT [PK_Patient_Chart] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC,
	[TrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Patient_ToothChart]  WITH CHECK ADD  CONSTRAINT [FK_Patient_ToothChart_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Patient_ToothChart] CHECK CONSTRAINT [FK_Patient_ToothChart_Patient]
GO
GO

/* [DentistX].[dbo].[Patient_ToothCheck] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Patient_ToothCheck]  WITH CHECK ADD  CONSTRAINT [FK_Patient_ToothCheck_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Patient_ToothCheck] CHECK CONSTRAINT [FK_Patient_ToothCheck_Patient]
GO
GO

/* [DentistX].[dbo].[Patient_ToothTrt] */

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
	[QrtrID] [int] NULL,
	[QrtrAddress] [int] NULL,
 CONSTRAINT [PK_Patient_ToothTrt] PRIMARY KEY CLUSTERED 
(
	[ToothTrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Patient_ToothTrt]  WITH CHECK ADD  CONSTRAINT [FK_Patient_ToothTrt_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Patient_ToothTrt] CHECK CONSTRAINT [FK_Patient_ToothTrt_Patient]
GO
GO

/* [DentistX].[dbo].[Patient_Trts] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Patient_Trts](
	[TrtID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[Detail] [nvarchar](50) NOT NULL,
	[TrtDate] [smalldatetime] NOT NULL,
	[TrtValue] [money] NOT NULL,
 CONSTRAINT [PK_Patient_Trts] PRIMARY KEY CLUSTERED 
(
	[TrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Patient_Trts]  WITH NOCHECK ADD  CONSTRAINT [FK_Patient_Trts_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Patient_Trts] CHECK CONSTRAINT [FK_Patient_Trts_Patient]
GO
GO

/* [DentistX].[dbo].[Patient_TrtsDelete] */

CREATE PROC [dbo].[Patient_TrtsDelete] 
    @TrtID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient_Trts]
	WHERE  [TrtID] = @TrtID

	COMMIT
GO

/* [DentistX].[dbo].[Patient_TrtsInsert] */

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

/* [DentistX].[dbo].[Patient_TrtsSelect] */

CREATE PROC [dbo].[Patient_TrtsSelect] 
    @TrtID int
AS 
	
	 

	BEGIN TRAN

	SELECT [TrtID], [PatientID], [Detail], [TrtDate], [TrtValue] 
	FROM   [dbo].[Patient_Trts] 
	WHERE  ([TrtID] = @TrtID OR @TrtID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[Patient_TrtsUpdate] */

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

/* [DentistX].[dbo].[PatientBalance] */

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

/* [DentistX].[dbo].[PatientColors] */

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
PRIMARY KEY CLUSTERED 
(
	[ColorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PatientColors]  WITH CHECK ADD  CONSTRAINT [FK_PatientColors_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PatientColors] CHECK CONSTRAINT [FK_PatientColors_Patient]
GO
GO

/* [DentistX].[dbo].[PatientColorsDelete] */

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

/* [DentistX].[dbo].[PatientColorsInsert] */

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

/* [DentistX].[dbo].[PatientColorsUpdate] */

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

/* [DentistX].[dbo].[PatientDelete] */

CREATE PROC [dbo].[PatientDelete] 
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Patient]
	WHERE  [PatientID] = @PatientID

	COMMIT
GO

/* [DentistX].[dbo].[PatientInsert] */

CREATE PROC [dbo].[PatientInsert] 
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
    @CreateDate datetime = NULL,
    @ReturnValue int OUTPUT
AS 
BEGIN
    SET NOCOUNT OFF;
    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @PID int = 0;
        DECLARE @Rows int = 0;

        -- Check if the PatientName already exists
        IF EXISTS (SELECT 1 FROM [dbo].[Patient] WHERE [PatientName] = @PatientName)
        BEGIN
            -- If patient already exists, set the return value to indicate a duplicate and exit
            SET @ReturnValue = -2;  -- Custom value to indicate duplicate patient
            ROLLBACK TRANSACTION;
            RETURN @ReturnValue;
        END

        -- Insert into the Patient table
        INSERT INTO [dbo].[Patient] ([PatientName], [Sex], [Age], [Phone], [Address], [Health], [Treat], [Implant], [Mobile], [Ortho], [Struc], [Notes], [BirthY], [CreatedBy], [CreateDate])
        SELECT @PatientName, @Sex, @Age, @Phone, @Address, @Health, @Treat, @Implant, @Mobile, @Ortho, @Struc, @Notes, @BirthY, @CreatedBy, @CreateDate;

        SET @PID = SCOPE_IDENTITY();
        SET @Rows = @Rows + 1;

        -- Insert into related tables
        EXEC dbo.PatientColorsInsert '#D8BFD8','#DDA0DD',5,0,@PID;
        SET @Rows = @Rows + 1;

        EXEC dbo.LDInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.LDInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.LDInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.LDInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.LUInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.LUInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.LUInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.LUInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.RDInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.RDInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.RDInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.RDInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.RUInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.RUInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.RUInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        EXEC dbo.RUInsert @PID,'','','','','','','','';
        SET @Rows = @Rows + 1;

        -- No need to check @@ROWCOUNT as you've already manually counted the rows
        COMMIT TRANSACTION;
        SET @ReturnValue = @Rows;
        RETURN @ReturnValue;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @ReturnValue = -1;
        RETURN @ReturnValue;
    END CATCH
END


--ALTER PROC [dbo].[PatientInsert] 
--    @PatientName nvarchar(50),
--    @Sex nvarchar(10) = NULL,
--    @Age int = NULL,
--    @Phone nvarchar(16) = NULL,
--    @Address nvarchar(50) = NULL,
--    @Health nvarchar(50) = NULL,
--    @Treat bit = NULL,
--    @Implant bit = NULL,
--    @Mobile bit = NULL,
--    @Ortho bit = NULL,
--    @Struc bit = NULL,
--    @Notes nvarchar(150) = NULL,
--    @BirthY int = NULL,
--    @CreatedBy int = NULL,
--    @CreateDate datetime = NULL,
--    @ReturnValue int OUTPUT
--AS 
--BEGIN
--    SET NOCOUNT OFF
--    BEGIN TRANSACTION 

--    BEGIN TRY 
--        Declare @PID int = 0
--        Declare @Rows int = 0

--        -- Insert into Patient table
--        INSERT INTO [dbo].[Patient] ([PatientName], [Sex], [Age], [Phone], [Address], [Health], [Treat], [Implant], [Mobile], [Ortho], [Struc], [Notes], [BirthY], [CreatedBy], [CreateDate])
--        SELECT @PatientName, @Sex, @Age, @Phone, @Address, @Health, @Treat, @Implant, @Mobile, @Ortho, @Struc, @Notes, @BirthY, @CreatedBy, @CreateDate
--        SET @PID = SCOPE_IDENTITY()
--        SET @Rows = @Rows + 1 -- This counts 1 row for the Patient insert
--		--=============
--		-- Check row count after each block of inserts
--PRINT 'Rows after Patient insert 1: ' + CAST(@Rows AS NVARCHAR(10))

---- After each call to LDInsert, LUInsert, etc.
---- Continue printing for all insert operations

--        -- Insert into PatientColors
--        EXEC dbo.PatientColorsInsert '#D8BFD8','#DDA0DD',5,0,@PID
--        SET @Rows = @Rows + 1
--PRINT 'Rows after PatientColors 2: ' + CAST(@Rows AS NVARCHAR(10))

--        -- LDInsert statements
--        EXEC dbo.LDInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--        PRINT 'Rows after LDInsert 3: ' + CAST(@Rows AS NVARCHAR(10))

--        EXEC dbo.LDInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--        PRINT 'Rows after LDInsert 4: ' + CAST(@Rows AS NVARCHAR(10))

--        EXEC dbo.LDInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--        PRINT 'Rows after LDInsert 5: ' + CAST(@Rows AS NVARCHAR(10))

--        EXEC dbo.LDInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--		        PRINT 'Rows after LDInsert 6: ' + CAST(@Rows AS NVARCHAR(10))

--        -- LUInsert statements
--        EXEC dbo.LUInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--        PRINT 'Rows after LUInsert 7: ' + CAST(@Rows AS NVARCHAR(10))

--        EXEC dbo.LUInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--                PRINT 'Rows after LUInsert 8: ' + CAST(@Rows AS NVARCHAR(10))

--        EXEC dbo.LUInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--                PRINT 'Rows after LUInsert 9: ' + CAST(@Rows AS NVARCHAR(10))

--        EXEC dbo.LUInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--		        PRINT 'Rows after LUInsert 10: ' + CAST(@Rows AS NVARCHAR(10))

--        -- RDInsert statements
--        EXEC dbo.RDInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--                PRINT 'Rows after RDInsert 11: ' + CAST(@Rows AS NVARCHAR(10))

--        EXEC dbo.RDInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--                PRINT 'Rows after RDInsert 12: ' + CAST(@Rows AS NVARCHAR(10))
--        EXEC dbo.RDInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--                PRINT 'Rows after RDInsert 13: ' + CAST(@Rows AS NVARCHAR(10))
--        EXEC dbo.RDInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--                PRINT 'Rows after RDInsert 14: ' + CAST(@Rows AS NVARCHAR(10))

--        -- RUInsert statements
--        EXEC dbo.RUInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--                PRINT 'Rows after RUInsert 15: ' + CAST(@Rows AS NVARCHAR(10))
                
--        EXEC dbo.RUInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--                        PRINT 'Rows after RUInsert 16: ' + CAST(@Rows AS NVARCHAR(10))

--        EXEC dbo.RUInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--                        PRINT 'Rows after RUInsert 17: ' + CAST(@Rows AS NVARCHAR(10))

--        EXEC dbo.RUInsert @PID, '', '', '', '', '', '', '', ''
--        SET @Rows = @Rows + 1
--		                PRINT 'Rows after RUInsert 18: ' + CAST(@Rows AS NVARCHAR(10))

--        -- Commit or rollback the transaction based on row count
--        IF @Rows = 18
--        BEGIN
--            COMMIT TRANSACTION
--            SET @ReturnValue = @Rows
--        END
--        ELSE
--        BEGIN
--            ROLLBACK TRANSACTION
--            SET @ReturnValue = @Rows -- Still return rows for debugging purposes
--        END
--    END TRY

--    BEGIN CATCH
--        ROLLBACK TRANSACTION
--        DECLARE @ErrorMessage varchar(150)
--        SET @ErrorMessage = ERROR_NUMBER() + ' ' + ERROR_MESSAGE()
--        RAISERROR(@ErrorMessage, 16, 1)
--        SET @ReturnValue = -1
--    END CATCH
--END




--ALTER PROC [dbo].[PatientInsert] 
--    @PatientName nvarchar(50),
--    @Sex nvarchar(10) = NULL,
--    @Age int = NULL,
--    @Phone nvarchar(16) = NULL,
--    @Address nvarchar(50) = NULL,
--    @Health nvarchar(50) = NULL,
--    @Treat bit = NULL,
--    @Implant bit = NULL,
--    @Mobile bit = NULL,
--    @Ortho bit = NULL,
--    @Struc bit = NULL,
--    @Notes nvarchar(150) = NULL,
--    @BirthY int = NULL,
--    @CreatedBy int = NULL,
--    @CreateDate datetime = NULL,
--	@ReturnValue int OUTPUT
--AS 
--	BEGIN

--SET NOCOUNT OFF

--BEGIN TRANSACTION 

--BEGIN TRY 

--	 Declare @PID int=0
--	Declare @Rows int=0
--	--BEGIN TRAN
	
--	INSERT INTO [dbo].[Patient] ([PatientName], [Sex], [Age], [Phone], [Address], [Health], [Treat], [Implant], [Mobile], [Ortho], [Struc], [Notes], [BirthY], [CreatedBy], [CreateDate])
--	SELECT @PatientName, @Sex, @Age, @Phone, @Address, @Health, @Treat, @Implant, @Mobile, @Ortho, @Struc, @Notes, @BirthY, @CreatedBy, @CreateDate
--	 SET @PID = SCOPE_IDENTITY()
--	 SET @Rows=@Rows + 1
--	 --=================================================
--	EXEC dbo.PatientColorsInsert '#D8BFD8','#DDA0DD',5,0,@PID
--	SET @Rows=@Rows + 1
--	--=================================================
--	exec dbo.LDInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.LDInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.LDInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.LDInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	--=================================================
--	exec dbo.LUInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.LUInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.LUInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.LUInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	--=================================================
--	exec dbo.RDInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.RDInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.RDInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.RDInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	--=================================================
--	exec dbo.RUInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.RUInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.RUInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	exec dbo.RUInsert @PID,'','','','','','','',''
--	SET @Rows=@Rows + 1
--	--=================================================
	
--	--Select @Rows
--	--COMMIT

--IF @@ROWCOUNT = 0
--BEGIN
--     ROLLBACK TRANSACTION
--     SET @ReturnValue = 0
--     RETURN @ReturnValue
--END
--ELSE
--BEGIN
--     COMMIT TRANSACTION
--     SET @ReturnValue = @Rows
--     RETURN @ReturnValue
--END

--END TRY

--BEGIN CATCH
--     DECLARE @Error_Message varchar(150)
--     SET @Error_Message = ERROR_NUMBER() + ' ' + ERROR_MESSAGE()
--     ROLLBACK TRANSACTION
--     RAISERROR(@Error_Message,16,1)
--      SET @ReturnValue = -1
--      RETURN @ReturnValue
--END CATCH

--END
GO

/* [DentistX].[dbo].[PatientPAYS] */

CREATE VIEW [dbo].[PatientPAYS]
AS
SELECT        dbo.Patient.PatientID, ISNULL(SUM(dbo.Patient_Pays.PayValue), 0) AS PAYS
FROM            dbo.Patient LEFT OUTER JOIN
                         dbo.Patient_Pays ON dbo.Patient.PatientID = dbo.Patient_Pays.PatientID
GROUP BY dbo.Patient.PatientID
GO

/* [DentistX].[dbo].[PatientRXVIEW] */

CREATE VIEW [dbo].[PatientRXVIEW]
AS
SELECT        dbo.Patient_RX.RxID, dbo.Patient.PatientID, dbo.Patient.PatientName, dbo.Patient.Sex, dbo.Patient.Age, dbo.Patient_RX.RXDate, dbo.Patient_RX.RX
FROM            dbo.Patient INNER JOIN
                         dbo.Patient_RX ON dbo.Patient.PatientID = dbo.Patient_RX.PatientID
GO

/* [DentistX].[dbo].[PatientsBalance] */

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

/* [DentistX].[dbo].[PatientsDebts] */

CREATE PROCEDURE [dbo].[PatientsDebts]
    
AS
BEGIN
    /*****************************************************************
     * Time: 19/08/2024 9:55:03 PM
     * Author: Pascal
     * Comments: This procedure was generated using SQL Assistant's 
     * Refactoring -> Extract Procedure feature.
     *****************************************************************/
 
    SELECT p.[PatientID],
           p.[PatientName],
           p.[Sex],
           p.[Age],
           p.[Phone],
           p.[Address],
           p.[Health],
           p.[Treat],
           p.[Implant],
           p.[Mobile],
           p.[Ortho],
           p.[Struc],
           p.[Notes],
           p.[BirthY],
           ISNULL(SUM(t.[TrtValue]), 0)   AS [TotalTreats],
           ISNULL(SUM(pv.[PayValue]), 0)  AS [TotalPays],
           --ISNULL(SUM(t.[TrtValue]), 0) - ISNULL(SUM(pv.[PayValue]), 0) AS [Balance]
           ISNULL(SUM(pv.[PayValue]), 0) - ISNULL(SUM(t.[TrtValue]), 0) AS [Balance]
    FROM   [dbo].[Patient] p
           LEFT JOIN [dbo].[Patient_Trts] t
                ON  p.[PatientID] = t.[PatientID]
           LEFT JOIN [dbo].[Patient_Pays] pv
                ON  t.[TrtID] = pv.[TrtID]
    GROUP BY
           p.[PatientID],
           p.[PatientName],
           p.[Sex],
           p.[Age],
           p.[Phone],
           p.[Address],
           p.[Health],
           p.[Treat],
           p.[Implant],
           p.[Mobile],
           p.[Ortho],
           p.[Struc],
           p.[Notes],
           p.[BirthY]
    ORDER BY
           p.[PatientName];
    

    RETURN @@ERROR
END
GO

/* [DentistX].[dbo].[PatientSelect] */

CREATE PROC [dbo].[PatientSelect] 
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PatientID], [PatientName], [Sex], [Age], [Phone], [Address], [Health], [Treat], [Implant], [Mobile], [Ortho], [Struc], [Notes], [BirthY], [CreatedBy], [CreateDate] 
	FROM   [dbo].[Patient] 
	WHERE  ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[PatientTrtPays] */

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

/* [DentistX].[dbo].[PatientTRTS] */

CREATE VIEW [dbo].[PatientTRTS]
AS
SELECT        dbo.Patient.PatientID, ISNULL(SUM(dbo.Patient_Trts.trtValue), 0) AS TRTS
FROM            dbo.Patient LEFT OUTER JOIN
                         dbo.Patient_Trts ON dbo.Patient.PatientID = dbo.Patient_Trts.PatientID
GROUP BY dbo.Patient.PatientID
GO

/* [DentistX].[dbo].[PatientUpdate] */

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

/* [DentistX].[dbo].[Payments] */

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
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[BuyInvoices] ([InvoiceID])
GO

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD CHECK  (([PaymentMethod]='BankTransfer' OR [PaymentMethod]='CreditCard' OR [PaymentMethod]='Cash'))
GO
GO

/* [DentistX].[dbo].[procUtils_GenerateClass] */

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

/* [DentistX].[dbo].[Products] */

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
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_Products_Category] ON [dbo].[Products]
(
	[CategoryID] ASC
)
INCLUDE([ProductName],[ReorderLevel]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [ReorderLevel]
GO

ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO

ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([UnitID])
REFERENCES [dbo].[Units] ([UnitID])
GO
GO

/* [DentistX].[dbo].[ProductStockView] */

CREATE VIEW ProductStockView
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

/* [DentistX].[dbo].[Qrt1Fun] */

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

/* [DentistX].[dbo].[Qrt2Fun] */

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

/* [DentistX].[dbo].[Qrtr1Yr] */

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
		FROM [DentistX].[dbo].[M_TRT]
		order by MNo
		end

END
commit

ROLLBACK

End
GO

/* [DentistX].[dbo].[Qrtr2Yr] */

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
		FROM [DentistX].[dbo].[M_TRT]
		order by MNo
		end

END
commit

ROLLBACK

End
GO

/* [DentistX].[dbo].[Qrtr3Yr] */

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
		FROM [DentistX].[dbo].[M_TRT]
		order by MNo
		end

END
commit

ROLLBACK

End
GO

/* [DentistX].[dbo].[Qrtr4Yr] */

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
		FROM [DentistX].[dbo].[M_TRT]
		order by MNo
		end

END
commit

ROLLBACK

End
GO

/* [DentistX].[dbo].[QrtrAllYr] */

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
		FROM [DentistX].[dbo].[M_TRT]
		order by MNo
		end

END
commit

ROLLBACK

End
GO

/* [DentistX].[dbo].[QrtrYr] */

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

/* [DentistX].[dbo].[Raseed] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[RaseedDelete] */

CREATE PROC [dbo].[RaseedDelete] 
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Raseed]
	WHERE  [PatientID] = @PatientID

	COMMIT
GO

/* [DentistX].[dbo].[RaseedInsert] */

CREATE PROC [dbo].[RaseedInsert] 
    @PatientID int
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Raseed] ([PatientID])
	SELECT @PatientID
	  
	COMMIT
GO

/* [DentistX].[dbo].[RaseedSelect] */

CREATE PROC [dbo].[RaseedSelect] 
    @PatientID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PatientID], [Bal] 
	FROM   [dbo].[Raseed] 
	WHERE  ([PatientID] = @PatientID OR @PatientID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[RD] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RD]  WITH NOCHECK ADD  CONSTRAINT [FK_RD_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[RD] CHECK CONSTRAINT [FK_RD_Patient]
GO
GO

/* [DentistX].[dbo].[RDDelete] */

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

/* [DentistX].[dbo].[RDInsert] */

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

/* [DentistX].[dbo].[RDPL] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[RDPL_IU] */

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

/* [DentistX].[dbo].[RDPLDelete] */

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

/* [DentistX].[dbo].[RDPLInsert] */

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

/* [DentistX].[dbo].[RDPLSelect] */

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

/* [DentistX].[dbo].[RDPLUpdate] */

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

/* [DentistX].[dbo].[RDSelect] */

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

/* [DentistX].[dbo].[RDSTYLE] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[RDSTYLE]  WITH NOCHECK ADD  CONSTRAINT [FK_RDSTYLE_patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[RDSTYLE] CHECK CONSTRAINT [FK_RDSTYLE_patient]
GO
GO

/* [DentistX].[dbo].[RDSTYLE_IU] */

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

/* [DentistX].[dbo].[RDSTYLEDelete] */

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

/* [DentistX].[dbo].[RDSTYLEInsert] */

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

/* [DentistX].[dbo].[RDSTYLESelect] */

CREATE PROC [dbo].[RDSTYLESelect] 
    @RDcellID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RDcellID], [PatientID], [CellAddres], [BakImg] 
	FROM   [dbo].[RDSTYLE] 
	WHERE  ([RDcellID] = @RDcellID OR @RDcellID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[RDSTYLEUpdate] */

CREATE PROC [dbo].[RDSTYLEUpdate] 
    @RDcellID int,
    @PatientID int,
    @CellAddres int,
    @BakImg image
AS 
	
	 
	
	BEGIN TRAN

	UPDATE [dbo].[RDSTYLE]
	SET    [PatientID] = @PatientID, [CellAddres] = @CellAddres, [BakImg] = @BakImg

	COMMIT
GO

/* [DentistX].[dbo].[RDUpdate] */

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

/* [DentistX].[dbo].[RecordStockMovement] */

CREATE PROCEDURE RecordStockMovement
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

/* [DentistX].[dbo].[ResBal] */

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

/* [DentistX].[dbo].[ResInvNet] */

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

/* [DentistX].[dbo].[ResTotalPays] */

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

/* [DentistX].[dbo].[Rlect] */

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

/* [DentistX].[dbo].[rs] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[rs](
	[UsID] [int] IDENTITY(1,1) NOT NULL,
	[UsName] [nvarchar](50) NOT NULL,
	[UsPass] [nvarchar](50) NOT NULL,
	[UsLvl] [int] NOT NULL,
	[UsGrp] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_rs] PRIMARY KEY CLUSTERED 
(
	[UsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[rsDelete] */

CREATE PROC [dbo].[rsDelete] 
    @UsID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[rs]
	WHERE  [UsID] = @UsID

	COMMIT
GO

/* [DentistX].[dbo].[rsInsert] */

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

/* [DentistX].[dbo].[rsSelect] */

CREATE PROC [dbo].[rsSelect] 
    @UsID int
AS 
	
	 

	BEGIN TRAN

	SELECT [UsID], [UsName], [UsPass], [UsLvl], [UsGrp] 
	FROM   [dbo].[rs] 
	WHERE  ([UsID] = @UsID OR @UsID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[rsUpdate] */

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

/* [DentistX].[dbo].[RU] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RU]  WITH NOCHECK ADD  CONSTRAINT [FK_RU_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[RU] CHECK CONSTRAINT [FK_RU_Patient]
GO
GO

/* [DentistX].[dbo].[RUDelete] */

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

/* [DentistX].[dbo].[RUInsert] */

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

/* [DentistX].[dbo].[RUPL] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[RUPL_IU] */

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

/* [DentistX].[dbo].[RUPLDelete] */

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

/* [DentistX].[dbo].[RUPLInsert] */

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

/* [DentistX].[dbo].[RUPLSelect] */

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

/* [DentistX].[dbo].[RUPLUpdate] */

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

/* [DentistX].[dbo].[RUSTYLE] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[RUSTYLE]  WITH NOCHECK ADD  CONSTRAINT [FK_RUSTYLE_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[RUSTYLE] CHECK CONSTRAINT [FK_RUSTYLE_Patient]
GO
GO

/* [DentistX].[dbo].[RUSTYLE_IU] */

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

/* [DentistX].[dbo].[RUSTYLEDelete] */

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

/* [DentistX].[dbo].[RUSTYLEInsert] */

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

/* [DentistX].[dbo].[RUSTYLESelect] */

CREATE PROC [dbo].[RUSTYLESelect] 
    @RUcellID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RUcellID], [PatientID], [CellAddres], [BakImg] 
	FROM   [dbo].[RUSTYLE] 
	WHERE  ([RUcellID] = @RUcellID OR @RUcellID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[RUSTYLEUpdate] */

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

/* [DentistX].[dbo].[RUUpdate] */

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

/* [DentistX].[dbo].[Rx_BdyVW] */

CREATE VIEW [dbo].[Rx_BdyVW]
AS
SELECT        dbo.Patient.PatientID, dbo.Patient.PatientName, dbo.Patient.Sex, YEAR(GETDATE()) - dbo.Patient.BirthY AS BirthY, dbo.Patient_RX.RxID, dbo.Patient_RX.RXDate, dbo.Patient_RX.RX, dbo.RxBody.EnHdrName, 
                         dbo.RxBody.EnHdrAdres, dbo.RxBody.Logo, dbo.RxBody.Detail, dbo.RxBody.ArHdrName, dbo.RxBody.ArHdrAdres, dbo.RxBody.EnFtr, dbo.RxBody.ArFtr, dbo.RxBody.DrName
FROM            dbo.Patient_RX INNER JOIN
                         dbo.Patient ON dbo.Patient_RX.PatientID = dbo.Patient.PatientID CROSS JOIN
                         dbo.RxBody
GO

/* [DentistX].[dbo].[RxBody] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RxBody](
	[RxBdyID] [int] NOT NULL,
	[ArHdrName] [nvarchar](50) NULL,
	[ArHdrAdres] [nvarchar](100) NULL,
	[EnHdrName] [nvarchar](50) NULL,
	[EnHdrAdres] [nvarchar](100) NULL,
	[Logo] [image] NULL,
	[Detail] [nvarchar](150) NULL,
	[ArFtr] [nvarchar](50) NULL,
	[EnFtr] [nvarchar](50) NULL,
	[WtrImg] [image] NULL,
	[WtrText] [nvarchar](50) NULL,
	[UseWtrImg] [bit] NULL,
	[UseWtrText] [bit] NULL,
	[DrName] [nvarchar](50) NULL,
 CONSTRAINT [PK_RxBody] PRIMARY KEY CLUSTERED 
(
	[RxBdyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[RxFly] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[RxFlyDelete] */

CREATE PROC [dbo].[RxFlyDelete] 
    @RxID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[RxFly]
	WHERE  [RxID] = @RxID

	COMMIT
GO

/* [DentistX].[dbo].[RxFlyInsert] */

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

/* [DentistX].[dbo].[RxFlySelect] */

CREATE PROC [dbo].[RxFlySelect] 
    @RxID int
AS 
	
	 

	BEGIN TRAN

	SELECT [RxID], [PatientName], [PatientAge], [RxDate], [RX] 
	FROM   [dbo].[RxFly] 
	WHERE  ([RxID] = @RxID OR @RxID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[RxFlyUpdate] */

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

/* [DentistX].[dbo].[select_LDSTYLE] */

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

/* [DentistX].[dbo].[select_LUSTYLE] */

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

/* [DentistX].[dbo].[select_RDSTYLE] */

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

/* [DentistX].[dbo].[select_RUSTYLE] */

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

/* [DentistX].[dbo].[SelectAll_LD] */

CREATE PROCEDURE [dbo].[SelectAll_LD] AS
BEGIN
SELECT * FROM LD
END
GO

/* [DentistX].[dbo].[SelectOne_LD] */

CREATE PROCEDURE [dbo].[SelectOne_LD] (
@LDID int, @PatientID int)
AS
BEGIN
   SELECT * FROM LD WHERE LDID = @LDID AND PatientID = @PatientID
END
GO

/* [DentistX].[dbo].[Shapes] */

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
 CONSTRAINT [PK_Shapes_70FC83A198828C5F] PRIMARY KEY CLUSTERED 
(
	[ShapeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[Sheet1] */

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
GO

/* [DentistX].[dbo].[StockMovements] */

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
PRIMARY KEY CLUSTERED 
(
	[MovementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_StockMovements_MovementDate] ON [dbo].[StockMovements]
(
	[MovementDate] ASC
)
INCLUDE([QuantityChange]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[StockMovements] ADD  DEFAULT (getdate()) FOR [MovementDate]
GO

ALTER TABLE [dbo].[StockMovements]  WITH CHECK ADD FOREIGN KEY([BatchID])
REFERENCES [dbo].[Batchess] ([BatchID])
GO

ALTER TABLE [dbo].[StockMovements]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO

ALTER TABLE [dbo].[StockMovements]  WITH CHECK ADD CHECK  (([MovementType]='Return' OR [MovementType]='Adjustment' OR [MovementType]='Usage' OR [MovementType]='Purchase'))
GO
GO

/* [DentistX].[dbo].[Suppliers] */

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
PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[Surgery] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Surgery]  WITH NOCHECK ADD  CONSTRAINT [FK_Surgery_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Surgery] CHECK CONSTRAINT [FK_Surgery_Patient]
GO
GO

/* [DentistX].[dbo].[SurgeryDelete] */

CREATE PROC [dbo].[SurgeryDelete] 
    @SurgID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Surgery]
	WHERE  [SurgID] = @SurgID

	COMMIT
GO

/* [DentistX].[dbo].[SurgeryInsert] */

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

/* [DentistX].[dbo].[SurgerySelect] */

CREATE PROC [dbo].[SurgerySelect] 
    @SurgID int
AS 
	
	 

	BEGIN TRAN

	SELECT [SurgID], [PatientID], [SurgeryDet], [SurDate] 
	FROM   [dbo].[Surgery] 
	WHERE  ([SurgID] = @SurgID OR @SurgID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[SurgeryUpdate] */

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


/* [DentistX].[dbo].[TblBnodMsareef] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[TblBnodMsareefDelete] */

CREATE PROC [dbo].[TblBnodMsareefDelete] 
    @BandID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblBnodMsareef]
	WHERE  [BandID] = @BandID

	COMMIT
GO

/* [DentistX].[dbo].[TblBnodMsareefInsert] */

CREATE PROC [dbo].[TblBnodMsareefInsert] 
    @BandName nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblBnodMsareef] ([BandName])
	SELECT @BandName
	   
	COMMIT
GO

/* [DentistX].[dbo].[TblBnodMsareefSelect] */

CREATE PROC [dbo].[TblBnodMsareefSelect] 
    @BandID int
AS 
	
	 

	BEGIN TRAN

	SELECT [BandID], [BandName] 
	FROM   [dbo].[TblBnodMsareef] 
	WHERE  ([BandID] = @BandID OR @BandID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblBnodMsareefUpdate] */

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

/* [DentistX].[dbo].[TblCategories] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[TblCategoriesDelete] */

CREATE PROC [dbo].[TblCategoriesDelete] 
    @CategoryID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblCategories]
	WHERE  [CategoryID] = @CategoryID

	COMMIT
GO

/* [DentistX].[dbo].[TblCategoriesInsert] */

CREATE PROC [dbo].[TblCategoriesInsert] 
    @CategoryName nvarchar(75) = NULL,
    @ParentCategory int = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblCategories] ([CategoryName], [ParentCategory])
	SELECT @CategoryName, @ParentCategory
	
	COMMIT
GO

/* [DentistX].[dbo].[TblCategoriesSelect] */

CREATE PROC [dbo].[TblCategoriesSelect] 
    @CategoryID int
AS 
	
	 

	BEGIN TRAN

	SELECT [CategoryID], [CategoryName], [ParentCategory] 
	FROM   [dbo].[TblCategories] 
	WHERE  ([CategoryID] = @CategoryID OR @CategoryID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblCategoriesUpdate] */

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

/* [DentistX].[dbo].[TblCities] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[TblCitiesDelete] */

CREATE PROC [dbo].[TblCitiesDelete] 
    @CityID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblCities]
	WHERE  [CityID] = @CityID

	COMMIT
GO

/* [DentistX].[dbo].[TblCitiesInsert] */

CREATE PROC [dbo].[TblCitiesInsert] 
    @CityName nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblCities] ([CityName])
	SELECT @CityName
	   
	COMMIT
GO

/* [DentistX].[dbo].[TblCitiesSelect] */

CREATE PROC [dbo].[TblCitiesSelect] 
    @CityID int
AS 
	
	 

	BEGIN TRAN

	SELECT [CityID], [CityName] 
	FROM   [dbo].[TblCities] 
	WHERE  ([CityID] = @CityID OR @CityID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblCitiesUpdate] */

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

/* [DentistX].[dbo].[TblCustomers] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TblCustomers]  WITH CHECK ADD  CONSTRAINT [FK_tblcustomers_tblcities] FOREIGN KEY([CityID])
REFERENCES [dbo].[TblCities] ([CityID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TblCustomers] CHECK CONSTRAINT [FK_tblcustomers_tblcities]
GO
GO

/* [DentistX].[dbo].[TblCustomersDelete] */

CREATE PROC [dbo].[TblCustomersDelete] 
    @CusID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblCustomers]
	WHERE  [CusID] = @CusID

	COMMIT
GO

/* [DentistX].[dbo].[TblCustomersInsert] */

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

/* [DentistX].[dbo].[TblCustomersSelect] */

CREATE PROC [dbo].[TblCustomersSelect] 
    @CusID int
AS 
	
	 

	BEGIN TRAN

	SELECT [CusID], [CusName], [CityID], [Address], [Contacts] 
	FROM   [dbo].[TblCustomers] 
	WHERE  ([CusID] = @CusID OR @CusID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblCustomersUpdate] */

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

/* [DentistX].[dbo].[TblInvoiceBody] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

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
GO

/* [DentistX].[dbo].[TblInvoiceBodyDelete] */

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

/* [DentistX].[dbo].[TblInvoiceBodyInsert] */

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

/* [DentistX].[dbo].[TblInvoiceBodySelect] */

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

/* [DentistX].[dbo].[TblInvoiceBodyUpdate] */

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

/* [DentistX].[dbo].[TblInvoicesHeader] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TblInvoicesHeader]  WITH CHECK ADD  CONSTRAINT [FK_TblInvoicesHeader_TblResources] FOREIGN KEY([ResID])
REFERENCES [dbo].[TblResources] ([ResID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TblInvoicesHeader] CHECK CONSTRAINT [FK_TblInvoicesHeader_TblResources]
GO
GO

/* [DentistX].[dbo].[TblInvoicesHeaderDelete] */

CREATE PROC [dbo].[TblInvoicesHeaderDelete] 
    @InvoiceID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblInvoicesHeader]
	WHERE  [InvoiceID] = @InvoiceID

	COMMIT
GO

/* [DentistX].[dbo].[TblInvoicesHeaderInsert] */

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

/* [DentistX].[dbo].[TblInvoicesHeaderSelect] */

CREATE PROC [dbo].[TblInvoicesHeaderSelect] 
    @InvoiceID int
AS 
	
	 

	BEGIN TRAN

	SELECT [InvoiceID], [InvoiceType], [InvoiceDate], [ResID], [DocNo], [InvoiceEx], [Hasm], [InvTotlQuantItms], [InvTotlPriceItms], [InvTotlDiscItms], [InvTotlDisc], [InvoiceNet] 
	FROM   [dbo].[TblInvoicesHeader] 
	WHERE  ([InvoiceID] = @InvoiceID OR @InvoiceID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblInvoicesHeaderUpdate] */

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

/* [DentistX].[dbo].[TblInvPay] */

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
 CONSTRAINT [PK_InvPay] PRIMARY KEY CLUSTERED 
(
	[PayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

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
GO

/* [DentistX].[dbo].[TblInvPayDelete] */

CREATE PROC [dbo].[TblInvPayDelete] 
    @PayID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblInvPay]
	WHERE  [PayID] = @PayID

	COMMIT
GO

/* [DentistX].[dbo].[TblInvPayInsert] */

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

/* [DentistX].[dbo].[TblInvPaySelect] */

CREATE PROC [dbo].[TblInvPaySelect] 
    @PayID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PayID], [InvoiceID], [ResID], [PayDate], [Amount], [Notes], [InvRemain] 
	FROM   [dbo].[TblInvPay] 
	WHERE  ([PayID] = @PayID OR @PayID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblInvPayUpdate] */

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

/* [DentistX].[dbo].[TblItems] */

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
 CONSTRAINT [PK_TblItemss] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TblItems]  WITH CHECK ADD  CONSTRAINT [FK_TblItemss_TblCategories] FOREIGN KEY([CatID])
REFERENCES [dbo].[TblCategories] ([CategoryID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TblItems] CHECK CONSTRAINT [FK_TblItemss_TblCategories]
GO

ALTER TABLE [dbo].[TblItems]  WITH CHECK ADD  CONSTRAINT [FK_TblItemss_TblUnits] FOREIGN KEY([UnitID])
REFERENCES [dbo].[TblUnits] ([UnitID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TblItems] CHECK CONSTRAINT [FK_TblItemss_TblUnits]
GO
GO

/* [DentistX].[dbo].[TblItemsDelete] */

CREATE PROC [dbo].[TblItemsDelete] 
    @ItemID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblItems]
	WHERE  [ItemID] = @ItemID

	COMMIT
GO

/* [DentistX].[dbo].[TblItemsInsert] */

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

/* [DentistX].[dbo].[TblItemsSelect] */

CREATE PROC [dbo].[TblItemsSelect] 
    @ItemID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ItemID], [ItemName], [ItemEx], [CatID], [UnitID], [LastPrice], [QuantityNow] 
	FROM   [dbo].[TblItems] 
	WHERE  ([ItemID] = @ItemID OR @ItemID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblItemsUpdate] */

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

/* [DentistX].[dbo].[TblMasareef] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TblMasareef]  WITH CHECK ADD  CONSTRAINT [FK_TblMasareef_TblBnodMsareef] FOREIGN KEY([BandID])
REFERENCES [dbo].[TblBnodMsareef] ([BandID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TblMasareef] CHECK CONSTRAINT [FK_TblMasareef_TblBnodMsareef]
GO
GO

/* [DentistX].[dbo].[TblMasareefDelete] */

CREATE PROC [dbo].[TblMasareefDelete] 
    @MasrofID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblMasareef]
	WHERE  [MasrofID] = @MasrofID

	COMMIT
GO

/* [DentistX].[dbo].[TblMasareefInsert] */

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

/* [DentistX].[dbo].[TblMasareefSelect] */

CREATE PROC [dbo].[TblMasareefSelect] 
    @MasrofID int
AS 
	
	 

	BEGIN TRAN

	SELECT [MasrofID], [MasrofDate], [BandID], [MasrofAmount], [MasrofEx] 
	FROM   [dbo].[TblMasareef] 
	WHERE  ([MasrofID] = @MasrofID OR @MasrofID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblMasareefUpdate] */

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

/* [DentistX].[dbo].[TblMeasure] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[TblMeasureDelete] */

CREATE PROC [dbo].[TblMeasureDelete] 
    @MeasureID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblMeasure]
	WHERE  [MeasureID] = @MeasureID

	COMMIT
GO

/* [DentistX].[dbo].[TblMeasureInsert] */

CREATE PROC [dbo].[TblMeasureInsert] 
    @Measure nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblMeasure] ([Measure])
	SELECT @Measure
	     
	COMMIT
GO

/* [DentistX].[dbo].[TblMeasureSelect] */

CREATE PROC [dbo].[TblMeasureSelect] 
    @MeasureID int
AS 
	
	 

	BEGIN TRAN

	SELECT [MeasureID], [Measure] 
	FROM   [dbo].[TblMeasure] 
	WHERE  ([MeasureID] = @MeasureID OR @MeasureID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblMeasureUpdate] */

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

/* [DentistX].[dbo].[TblPaids] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[TblPaidsDelete] */

CREATE PROC [dbo].[TblPaidsDelete] 
    @PayID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblPaids]
	WHERE  [PayID] = @PayID

	COMMIT
GO

/* [DentistX].[dbo].[TblPaidsInsert] */

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

/* [DentistX].[dbo].[TblPaidsSelect] */

CREATE PROC [dbo].[TblPaidsSelect] 
    @PayID int
AS 
	
	 

	BEGIN TRAN

	SELECT [PayID], [PayType], [PayDate], [ResCusId], [PayAmount], [PayEx] 
	FROM   [dbo].[TblPaids] 
	WHERE  ([PayID] = @PayID OR @PayID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblPaidsUpdate] */

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

/* [DentistX].[dbo].[TblResources] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TblResources]  WITH CHECK ADD  CONSTRAINT [FK_tblresources_tblcities] FOREIGN KEY([CityID])
REFERENCES [dbo].[TblCities] ([CityID])
GO

ALTER TABLE [dbo].[TblResources] CHECK CONSTRAINT [FK_tblresources_tblcities]
GO
GO

/* [DentistX].[dbo].[TblResourcesDelete] */

CREATE PROC [dbo].[TblResourcesDelete] 
    @ResID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblResources]
	WHERE  [ResID] = @ResID

	COMMIT
GO

/* [DentistX].[dbo].[TblResourcesInsert] */

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

/* [DentistX].[dbo].[TblResourcesSelect] */

CREATE PROC [dbo].[TblResourcesSelect] 
    @ResID int
AS 
	
	 

	BEGIN TRAN

	SELECT [ResID], [ResName], [CityID], [Address], [Contacts], [ResInvsNet], [ResTotalPays], [ResBal] 
	FROM   [dbo].[TblResources] 
	WHERE  ([ResID] = @ResID OR @ResID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblResourcesUpdate] */

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

/* [DentistX].[dbo].[TblSalesBody] */

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
GO

/* [DentistX].[dbo].[TblSalesHeader] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TblSalesHeader]  WITH CHECK ADD  CONSTRAINT [FK_TblSalesHeader_TblCustomers] FOREIGN KEY([CusID])
REFERENCES [dbo].[TblCustomers] ([CusID])
GO

ALTER TABLE [dbo].[TblSalesHeader] CHECK CONSTRAINT [FK_TblSalesHeader_TblCustomers]
GO
GO

/* [DentistX].[dbo].[TblSalesHeaderDelete] */

CREATE PROC [dbo].[TblSalesHeaderDelete] 
    @SaleID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblSalesHeader]
	WHERE  [SaleID] = @SaleID

	COMMIT
GO

/* [DentistX].[dbo].[TblSalesHeaderInsert] */

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

/* [DentistX].[dbo].[TblSalesHeaderSelect] */

CREATE PROC [dbo].[TblSalesHeaderSelect] 
    @SaleID int
AS 
	
	 

	BEGIN TRAN

	SELECT [SaleID], [SaleType], [SaleDate], [CusID], [DocNo], [SaleEx], [Hasm] 
	FROM   [dbo].[TblSalesHeader] 
	WHERE  ([SaleID] = @SaleID OR @SaleID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblSalesHeaderUpdate] */

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

/* [DentistX].[dbo].[TblTRT] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblTRT](
	[TrtID] [int] IDENTITY(1,1) NOT NULL,
	[Trt] [nvarchar](50) NOT NULL,
	[ShapeID] [int] NULL,
	[TrtDetails] [nvarchar](150) NULL,
	[TrtAr] [nvarchar](150) NULL,
	[TrtArDetails] [nvarchar](150) NULL,
	[ToothID] [nvarchar](50) NULL,
	[ToothIDkID] [nvarchar](50) NULL,
	[OldTrt] [nvarchar](50) NULL,
	[TrtGroup] [nvarchar](50) NULL,
	[TrtColor] [nvarchar](50) NULL,
	[KidTrt] [tinyint] NOT NULL,
 CONSTRAINT [PK_TblTRT] PRIMARY KEY CLUSTERED 
(
	[TrtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TblTRT] ADD  CONSTRAINT [DF_TblTRT_KidTrt]  DEFAULT ((0)) FOR [KidTrt]
GO

ALTER TABLE [dbo].[TblTRT]  WITH CHECK ADD  CONSTRAINT [CK_TblTRT_KidTrt_ValidValues] CHECK  (([KidTrt]=(2) OR [KidTrt]=(1) OR [KidTrt]=(0)))
GO

ALTER TABLE [dbo].[TblTRT] CHECK CONSTRAINT [CK_TblTRT_KidTrt_ValidValues]
GO
GO

/* [DentistX].[dbo].[TblTRTDelete] */

CREATE PROC [dbo].[TblTRTDelete] 
    @TrtID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblTRT]
	WHERE  [TrtID] = @TrtID

	COMMIT
GO

/* [DentistX].[dbo].[TblTRTInsert] */

CREATE PROC [dbo].[TblTRTInsert] 
    @Trt nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblTRT] ([Trt])
	SELECT @Trt
	
	COMMIT
GO

/* [DentistX].[dbo].[TblTRTSelect] */

CREATE PROC [dbo].[TblTRTSelect] 
    @TrtID int
AS 
	
	 

	BEGIN TRAN

	SELECT [TrtID], [Trt] 
	FROM   [dbo].[TblTRT] 
	WHERE  ([TrtID] = @TrtID OR @TrtID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblTRTUpdate] */

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

/* [DentistX].[dbo].[TblUnits] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[TblUnitsDelete] */

CREATE PROC [dbo].[TblUnitsDelete] 
    @UnitID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblUnits]
	WHERE  [UnitID] = @UnitID

	COMMIT
GO

/* [DentistX].[dbo].[TblUnitsInsert] */

CREATE PROC [dbo].[TblUnitsInsert] 
    @UnitName nvarchar(50) = NULL
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblUnits] ([UnitName])
	SELECT @UnitName
	
	COMMIT
GO

/* [DentistX].[dbo].[TblUnitsSelect] */

CREATE PROC [dbo].[TblUnitsSelect] 
    @UnitID int
AS 
	
	 

	BEGIN TRAN

	SELECT [UnitID], [UnitName] 
	FROM   [dbo].[TblUnits] 
	WHERE  ([UnitID] = @UnitID OR @UnitID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblUnitsUpdate] */

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

/* [DentistX].[dbo].[TblWireType] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[TblWireTypeDelete] */

CREATE PROC [dbo].[TblWireTypeDelete] 
    @TypeID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TblWireType]
	WHERE  [TypeID] = @TypeID

	COMMIT
GO

/* [DentistX].[dbo].[TblWireTypeInsert] */

CREATE PROC [dbo].[TblWireTypeInsert] 
    @WireType nvarchar(50)
AS 
	
	 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TblWireType] ([WireType])
	SELECT @WireType
	
	COMMIT
GO

/* [DentistX].[dbo].[TblWireTypeSelect] */

CREATE PROC [dbo].[TblWireTypeSelect] 
    @TypeID int
AS 
	
	 

	BEGIN TRAN

	SELECT [TypeID], [WireType] 
	FROM   [dbo].[TblWireType] 
	WHERE  ([TypeID] = @TypeID OR @TypeID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[TblWireTypeUpdate] */

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

/* [DentistX].[dbo].[TotalPatientPays] */

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

/* [DentistX].[dbo].[TotalPatientTrts] */

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

/* [DentistX].[dbo].[TotalPaysPerTrt] */

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

/* [DentistX].[dbo].[TotalTrtsPerPatient] */

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

/* [DentistX].[dbo].[TreatBal] */

CREATE VIEW [dbo].[TreatBal]
AS
SELECT        dbo.Patient_Trts.TrtID, ISNULL(dbo.TreatPays.TreatPays - dbo.Patient_Trts.trtValue, 0) AS TrtBal
FROM            dbo.TreatPays INNER JOIN
                         dbo.Patient_Trts ON dbo.TreatPays.TrtID = dbo.Patient_Trts.TrtID
GO

/* [DentistX].[dbo].[TreatPays] */

CREATE VIEW [dbo].[TreatPays]
AS
SELECT        dbo.Patient_Trts.TrtID, ISNULL(SUM(dbo.Patient_Pays.PayValue), 0) AS TreatPays
FROM            dbo.Patient_Pays INNER JOIN
                         dbo.Patient_Trts ON dbo.Patient_Pays.TrtID = dbo.Patient_Trts.TrtID
GROUP BY dbo.Patient_Trts.TrtID
GO

/* [DentistX].[dbo].[TrtsPaysList] */

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

/* [DentistX].[dbo].[Units] */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Units](
	[UnitID] [int] IDENTITY(1,1) NOT NULL,
	[UnitName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UnitName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO

/* [DentistX].[dbo].[Update_Appointments] */

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

/* [DentistX].[dbo].[Update_DrWork] */

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

/* [DentistX].[dbo].[Update_DrWorkPay] */

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

/* [DentistX].[dbo].[Update_Emp] */

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

/* [DentistX].[dbo].[Update_EmpAtend] */

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

/* [DentistX].[dbo].[Update_EmpPay] */

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

/* [DentistX].[dbo].[Update_Gender] */

CREATE PROCEDURE [dbo].[Update_Gender] (
@Sex nvarchar(50),
@SID int
) AS BEGIN
    UPDATE Gender SET Sex = @Sex
    WHERE SID = @SID
END
GO

/* [DentistX].[dbo].[Update_Health] */

CREATE PROCEDURE [dbo].[Update_Health] (
@HealthStat nvarchar(100),
@HID int
) AS BEGIN
    UPDATE Health SET HealthStat = @HealthStat
    WHERE HID = @HID
END
GO

/* [DentistX].[dbo].[Update_Imags] */

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

/* [DentistX].[dbo].[Update_ImpClrs] */

CREATE PROCEDURE [dbo].[Update_ImpClrs] (
@ImpClr nvarchar(50),
@ImpClrID int
) AS BEGIN
    UPDATE ImpClrs SET ImpClr = @ImpClr
    WHERE ImpClrID = @ImpClrID
END
GO

/* [DentistX].[dbo].[Update_ImprDet] */

CREATE PROCEDURE [dbo].[Update_ImprDet] (
@imprID int,
@ImprDetail nvarchar(50),
@ImpDetID int
) AS BEGIN
    UPDATE ImprDet SET imprID = @imprID, ImprDetail = @ImprDetail
    WHERE ImpDetID = @ImpDetID
END
GO

/* [DentistX].[dbo].[Update_Impression] */

CREATE PROCEDURE [dbo].[Update_Impression] (
@ImprType nvarchar(50),
@ImprID int
) AS BEGIN
    UPDATE Impression SET ImprType = @ImprType
    WHERE ImprID = @ImprID
END
GO

/* [DentistX].[dbo].[Update_Lab] */

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

/* [DentistX].[dbo].[Update_LabOrder] */

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

/* [DentistX].[dbo].[Update_LabPay] */

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

/* [DentistX].[dbo].[Update_LD] */

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

/* [DentistX].[dbo].[Update_LDPL] */

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

/* [DentistX].[dbo].[Update_LDSTYLE] */

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

/* [DentistX].[dbo].[Update_LU] */

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

/* [DentistX].[dbo].[Update_LUPL] */

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

/* [DentistX].[dbo].[Update_LUSTYLE] */

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

/* [DentistX].[dbo].[Update_M_TRT] */

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

/* [DentistX].[dbo].[Update_MedicineDoze] */

CREATE PROCEDURE [dbo].[Update_MedicineDoze] (
@ShapeID int,
@Doze nvarchar(50),
@DozeID int
) AS BEGIN
    UPDATE MedicineDoze SET ShapeID = @ShapeID, Doze = @Doze
    WHERE DozeID = @DozeID
END
GO

/* [DentistX].[dbo].[Update_MedicineFamily] */

CREATE PROCEDURE [dbo].[Update_MedicineFamily] (
@MedicineID int,
@MedicineSubCat nvarchar(50),
@SubCatID int
) AS BEGIN
    UPDATE MedicineFamily SET MedicineID = @MedicineID, MedicineSubCat = @MedicineSubCat
    WHERE SubCatID = @SubCatID
END
GO

/* [DentistX].[dbo].[Update_MedicineGroups] */

CREATE PROCEDURE [dbo].[Update_MedicineGroups] (
@MedicineFamily nvarchar(50),
@MedicineID int
) AS BEGIN
    UPDATE MedicineGroups SET MedicineFamily = @MedicineFamily
    WHERE MedicineID = @MedicineID
END
GO

/* [DentistX].[dbo].[Update_MedicineItems] */

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

/* [DentistX].[dbo].[Update_MedicineShape] */

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

/* [DentistX].[dbo].[Update_MedScienceFamily] */

CREATE PROCEDURE [dbo].[Update_MedScienceFamily] (
@SubCatID int,
@ScienceName nvarchar(50),
@ScincID int
) AS BEGIN
    UPDATE MedScienceFamily SET SubCatID = @SubCatID, ScienceName = @ScienceName
    WHERE ScincID = @ScincID
END
GO

/* [DentistX].[dbo].[Update_OrthoDiag] */

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

/* [DentistX].[dbo].[Update_OrthoInf] */

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

/* [DentistX].[dbo].[Update_OrthoTreat] */

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

/* [DentistX].[dbo].[Update_OrthoTrtDet] */

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

/* [DentistX].[dbo].[Update_OutDr] */

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

/* [DentistX].[dbo].[Update_Patient] */

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

/* [DentistX].[dbo].[Update_Patient_Chart] */

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

/* [DentistX].[dbo].[Update_Patient_ChartCheck] */

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

/* [DentistX].[dbo].[Update_Patient_Imgs] */

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

/* [DentistX].[dbo].[Update_Patient_MobStruc] */

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

/* [DentistX].[dbo].[Update_Patient_MobStrucAdd] */

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

/* [DentistX].[dbo].[Update_Patient_Notes] */

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

/* [DentistX].[dbo].[Update_Patient_Pays] */

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

/* [DentistX].[dbo].[Update_Patient_RX] */

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

/* [DentistX].[dbo].[Update_Patient_ToothCheck] */

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

/* [DentistX].[dbo].[Update_Patient_Trts] */

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

/* [DentistX].[dbo].[Update_PatientColors] */

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

/* [DentistX].[dbo].[Update_RD] */

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

/* [DentistX].[dbo].[Update_RDPL] */

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

/* [DentistX].[dbo].[Update_RDSTYLE] */

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

/* [DentistX].[dbo].[Update_Resources] */

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

/* [DentistX].[dbo].[Update_rs] */

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

/* [DentistX].[dbo].[Update_RU] */

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

/* [DentistX].[dbo].[Update_RUPL] */

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

/* [DentistX].[dbo].[Update_RUSTYLE] */

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

/* [DentistX].[dbo].[Update_RxBody] */

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

/* [DentistX].[dbo].[Update_RxFly] */

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

/* [DentistX].[dbo].[Update_Surgery] */

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

/* [DentistX].[dbo].[Update_TblBnodMsareef] */

CREATE PROCEDURE [dbo].[Update_TblBnodMsareef] (
@BandName nvarchar(50),
@BandID int
) AS BEGIN
    UPDATE TblBnodMsareef SET BandName = @BandName
    WHERE BandID = @BandID
END
GO

/* [DentistX].[dbo].[Update_TblCategories] */

CREATE PROCEDURE [dbo].[Update_TblCategories] (
@CategoryName nvarchar(75),
@ParentCategory int,
@CategoryID int
) AS BEGIN
    UPDATE TblCategories SET CategoryName = @CategoryName, ParentCategory = @ParentCategory
    WHERE CategoryID = @CategoryID
END
GO

/* [DentistX].[dbo].[Update_TblCities] */

CREATE PROCEDURE [dbo].[Update_TblCities] (
@CityName nvarchar(50),
@CityID int
) AS BEGIN
    UPDATE TblCities SET CityName = @CityName
    WHERE CityID = @CityID
END
GO

/* [DentistX].[dbo].[Update_TblCustomers] */

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

/* [DentistX].[dbo].[Update_TblExpenPay] */

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

/* [DentistX].[dbo].[Update_TblInvoiceBody] */

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

/* [DentistX].[dbo].[Update_TblInvoicesHeader] */

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

/* [DentistX].[dbo].[Update_TblInvPay] */

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

/* [DentistX].[dbo].[Update_TblItems] */

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

/* [DentistX].[dbo].[Update_TblMasareef] */

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

/* [DentistX].[dbo].[Update_TblMeasure] */

CREATE PROCEDURE [dbo].[Update_TblMeasure] (
@Measure nvarchar(50),
@MeasureID int
) AS BEGIN
    UPDATE TblMeasure SET Measure = @Measure
    WHERE MeasureID = @MeasureID
END
GO

/* [DentistX].[dbo].[Update_TblPaids] */

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

/* [DentistX].[dbo].[Update_TblResources] */

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

/* [DentistX].[dbo].[Update_TblSalesBody] */

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

/* [DentistX].[dbo].[Update_TblSalesHeader] */

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

/* [DentistX].[dbo].[Update_TblTRT] */

CREATE PROCEDURE [dbo].[Update_TblTRT] (
@Trt nvarchar(50),
@TrtID int
) AS BEGIN
    UPDATE TblTRT SET Trt = @Trt
    WHERE TrtID = @TrtID
END
GO

/* [DentistX].[dbo].[Update_TblUnits] */

CREATE PROCEDURE [dbo].[Update_TblUnits] (
@UnitName nvarchar(50),
@UnitID int
) AS BEGIN
    UPDATE TblUnits SET UnitName = @UnitName
    WHERE UnitID = @UnitID
END
GO

/* [DentistX].[dbo].[Update_TblWireType] */

CREATE PROCEDURE [dbo].[Update_TblWireType] (
@WireType nvarchar(50),
@TypeID int
) AS BEGIN
    UPDATE TblWireType SET WireType = @WireType
    WHERE TypeID = @TypeID
END
GO

/* [DentistX].[dbo].[Update_Users] */

CREATE PROCEDURE [dbo].[Update_Users] (
@UsName nvarchar(50),
@UsPass nvarchar(50),
@UsLvl int,
@UsGrp nvarchar(50),
@CreatedBy int,
@LastAccby int,
@LastUpdate datetime,
@UsID int
) AS BEGIN
    UPDATE Users SET UsName = @UsName, UsPass = @UsPass, UsLvl = @UsLvl, UsGrp = @UsGrp, CreatedBy = @CreatedBy, LastAccby = @LastAccby, LastUpdate = @LastUpdate
    WHERE UsID = @UsID
END
GO

/* [DentistX].[dbo].[Update_VisitType] */

CREATE PROCEDURE [dbo].[Update_VisitType] (
@VisType nvarchar(200),
@VtID int
) AS BEGIN
    UPDATE VisitType SET VisType = @VisType
    WHERE VtID = @VtID
END
GO

/* [DentistX].[dbo].[Update_Y_TRT] */

CREATE PROCEDURE [dbo].[Update_Y_TRT] (
@YName int,
@YYName nchar
) AS BEGIN
    UPDATE Y_TRT SET YYName = @YYName
    WHERE YName = @YName
END
GO

/* [DentistX].[dbo].[UsAtend] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UsAtend]  WITH CHECK ADD  CONSTRAINT [FK_UsAtend_rs] FOREIGN KEY([UsID])
REFERENCES [dbo].[rs] ([UsID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UsAtend] CHECK CONSTRAINT [FK_UsAtend_rs]
GO
GO

/* [DentistX].[dbo].[UsAtendDelete] */

CREATE PROC [dbo].[UsAtendDelete] 
    @AtndID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[UsAtend]
	WHERE  [AtndID] = @AtndID

	COMMIT
GO

/* [DentistX].[dbo].[UsAtendInsert] */

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

/* [DentistX].[dbo].[UsAtendSelect] */

CREATE PROC [dbo].[UsAtendSelect] 
    @AtndID int
AS 
	
	 

	BEGIN TRAN

	SELECT [AtndID], [UsID], [AtnDay], [AtnNote], [AbsPrsnt] 
	FROM   [dbo].[UsAtend] 
	WHERE  ([AtndID] = @AtndID OR @AtndID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[UsAtendUpdate] */

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

/* [DentistX].[dbo].[usp_DeleteDuplicatesForPatients] */

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
                [DentistX].[dbo].[' + @TableName + ']
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

/* [DentistX].[dbo].[usp_DeleteDuplicatesForStyle] */

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
                [DentistX].[dbo].[' + @TableName + ']
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

/* [DentistX].[dbo].[usp_RxBodyUpdate] */

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

/* [DentistX].[dbo].[UsrPay] */

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UsrPay]  WITH CHECK ADD  CONSTRAINT [FK_UsrPay_rs] FOREIGN KEY([UsID])
REFERENCES [dbo].[rs] ([UsID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UsrPay] CHECK CONSTRAINT [FK_UsrPay_rs]
GO
GO

/* [DentistX].[dbo].[UsrPayDelete] */

CREATE PROC [dbo].[UsrPayDelete] 
    @UsSalID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[UsrPay]
	WHERE  [UsSalID] = @UsSalID

	COMMIT
GO

/* [DentistX].[dbo].[UsrPayInsert] */

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

/* [DentistX].[dbo].[UsrPaySelect] */

CREATE PROC [dbo].[UsrPaySelect] 
    @UsSalID int
AS 
	
	 

	BEGIN TRAN

	SELECT [UsSalID], [UsID], [MonthPay], [DayPay], [FromDT], [ToDT], [DaysCount], [PayDate], [PayNote] 
	FROM   [dbo].[UsrPay] 
	WHERE  ([UsSalID] = @UsSalID OR @UsSalID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[UsrPayUpdate] */

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

/* [DentistX].[dbo].[Visits] */

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
 CONSTRAINT [PK_VisitDet] PRIMARY KEY CLUSTERED 
(
	[VisitDetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Visits]  WITH NOCHECK ADD  CONSTRAINT [FK_Visits_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([PatientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Visits] CHECK CONSTRAINT [FK_Visits_Patient]
GO
GO

/* [DentistX].[dbo].[VisitsDelete] */

CREATE PROC [dbo].[VisitsDelete] 
    @VisitDetID int
AS 
	
	 
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Visits]
	WHERE  [VisitDetID] = @VisitDetID

	COMMIT
GO

/* [DentistX].[dbo].[VisitsSelect] */

CREATE PROC [dbo].[VisitsSelect] 
    @VisitDetID int
AS 
	
	 

	BEGIN TRAN

	SELECT [VisitDetID], [PatientID], [VisitDay], [VisTime], [PatientName], [VisDetail], [VisNotes], [VisDateTime] 
	FROM   [dbo].[Visits] 
	WHERE  ([VisitDetID] = @VisitDetID OR @VisitDetID IS NULL) 

	COMMIT
GO

/* [DentistX].[dbo].[VisitsUpdate] */

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

/* [DentistX].[dbo].[VisitVW] */

CREATE VIEW [dbo].[VisitVW]
AS
SELECT     VisitDetID, VisitDay, VisTime, PatientName, PatientID
FROM         dbo.Visits
GO

/* [DentistX].[dbo].[Vw_ImplantCatalog] */

CREATE VIEW dbo.Vw_ImplantCatalog
AS
SELECT        v.VariationID, b.BrandName, t.TypeName, CASE WHEN v.IsSlim = 1 THEN 'Slim' ELSE 'Standard' END AS Design, m.DiameterMM, m.LengthMM, CAST(m.DiameterMM AS VARCHAR(10)) 
                         + 'x' + CAST(m.LengthMM AS VARCHAR(10)) + 'mm' AS Size, b.BrandName + ' ' + t.TypeName + CASE WHEN v.IsSlim = 1 THEN ' Slim' ELSE '' END + ' - ' + CAST(m.DiameterMM AS VARCHAR(10)) 
                         + 'x' + CAST(m.LengthMM AS VARCHAR(10)) + 'mm' AS DisplayName
FROM            dbo.ImplantVariation AS v INNER JOIN
                         dbo.ImplantBrand AS b ON v.BrandID = b.BrandID INNER JOIN
                         dbo.ImplantType AS t ON v.TypeID = t.TypeID INNER JOIN
                         dbo.ImplantMeasure AS m ON v.VariationID = m.VariationID
GO

/* [DentistX].[dbo].[Vw_ImplantQuickSelect] */

CREATE VIEW [dbo].[Vw_ImplantQuickSelect] AS
SELECT 
    VariationID,
    BrandName,
    TypeName,
    Design,
    CAST(DiameterMM AS VARCHAR(10)) + 'x' + CAST(LengthMM AS VARCHAR(10)) + 'mm' AS Size,
    DisplayName
FROM [Vw_ImplantCatalog];
GO


