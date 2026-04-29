-- Run once on DentistX after adding cheque payments. Allows PaymentMethod = 'Cheque' on dbo.Payments.
IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = N'CK_Payments_PaymentMethod' AND parent_object_id = OBJECT_ID(N'dbo.Payments'))
    ALTER TABLE dbo.Payments DROP CONSTRAINT CK_Payments_PaymentMethod;
GO
ALTER TABLE dbo.Payments WITH CHECK ADD CONSTRAINT CK_Payments_PaymentMethod CHECK (
    PaymentMethod IN (N'Cash', N'BankTransfer', N'CreditCard', N'Cheque'));
GO
ALTER TABLE dbo.Payments CHECK CONSTRAINT CK_Payments_PaymentMethod;
GO
