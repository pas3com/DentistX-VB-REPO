-- Adds WhatsApp country prefix and local number columns for schedule snapshot / bulk WhatsApp features.
-- Run once per database. Safe to re-run if columns already exist (adjust or remove IF checks as needed for your SQL Server version).
-- WhatsAppPrefix stores the full combo line (e.g. "Palestine (+970)"); use NVARCHAR(200)+ so values are not truncated.

IF COL_LENGTH('dbo.Doctors', 'WhatsAppPrefix') IS NULL
    ALTER TABLE dbo.Doctors ADD WhatsAppPrefix NVARCHAR(16) NULL;

IF COL_LENGTH('dbo.Doctors', 'WhatsApp') IS NULL
    ALTER TABLE dbo.Doctors ADD WhatsApp NVARCHAR(32) NULL;

IF COL_LENGTH('dbo.Contacts', 'WhatsAppPrefix') IS NULL
    ALTER TABLE dbo.Contacts ADD WhatsAppPrefix NVARCHAR(16) NULL;

IF COL_LENGTH('dbo.Contacts', 'WhatsApp') IS NULL
    ALTER TABLE dbo.Contacts ADD WhatsApp NVARCHAR(32) NULL;

IF COL_LENGTH('dbo.Secretaries', 'WhatsAppPrefix') IS NULL
    ALTER TABLE dbo.Secretaries ADD WhatsAppPrefix NVARCHAR(200) NULL;

IF COL_LENGTH('dbo.Secretaries', 'WhatsApp') IS NULL
    ALTER TABLE dbo.Secretaries ADD WhatsApp NVARCHAR(32) NULL;

IF COL_LENGTH('dbo.Emp', 'WhatsAppPrefix') IS NULL
    ALTER TABLE dbo.Emp ADD WhatsAppPrefix NVARCHAR(200) NULL;

IF COL_LENGTH('dbo.Emp', 'WhatsApp') IS NULL
    ALTER TABLE dbo.Emp ADD WhatsApp NVARCHAR(32) NULL;

IF COL_LENGTH('dbo.Suppliers', 'WhatsAppPrefix') IS NULL
    ALTER TABLE dbo.Suppliers ADD WhatsAppPrefix NVARCHAR(200) NULL;

IF COL_LENGTH('dbo.Suppliers', 'WhatsApp') IS NULL
    ALTER TABLE dbo.Suppliers ADD WhatsApp NVARCHAR(32) NULL;

-- Widen prefix columns if a previous script used NVARCHAR(16) (ignore failure if already wide)
BEGIN TRY
    ALTER TABLE dbo.Doctors ALTER COLUMN WhatsAppPrefix NVARCHAR(200) NULL;
END TRY BEGIN CATCH END CATCH
BEGIN TRY
    ALTER TABLE dbo.Contacts ALTER COLUMN WhatsAppPrefix NVARCHAR(200) NULL;
END TRY BEGIN CATCH END CATCH

GO
