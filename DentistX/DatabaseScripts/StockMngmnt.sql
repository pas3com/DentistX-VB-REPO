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
/****** Object:  Table [dbo].[Batchess]    Script Date: 21/01/2026 11:35:49 AM ******/
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
/****** Object:  Table [dbo].[BuyInvoiceLineItems]    Script Date: 21/01/2026 11:35:49 AM ******/
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
/****** Object:  Table [dbo].[BuyInvoices]    Script Date: 21/01/2026 11:35:49 AM ******/
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
/****** Object:  Table [dbo].[Categories]    Script Date: 21/01/2026 11:35:49 AM ******/
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
/****** Object:  Table [dbo].[Payments]    Script Date: 21/01/2026 11:35:49 AM ******/
CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[SupplierID] [int] NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentDate] [date] NOT NULL,
	[PaymentMethod] [nvarchar](20) NOT NULL,
	[ChqOwner] [nvarchar](100) NULL,
	[AccountNumber] [nvarchar](100) NULL,
	[ChqNumber] [nchar](10) NULL,
	[ChqDueDate] [smalldatetime] NULL,
	[ChqBank] [nvarchar](100) NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 21/01/2026 11:35:49 AM ******/
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
/****** Object:  Table [dbo].[Repayments]    Script Date: 21/01/2026 11:35:49 AM ******/
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
/****** Object:  Table [dbo].[StockMovements]    Script Date: 21/01/2026 11:35:49 AM ******/
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
ALTER TABLE [dbo].[BuyInvoices] ADD  CONSTRAINT [DF_BuyInvoices_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_ReorderLevel]  DEFAULT ((0)) FOR [ReorderLevel]
GO
ALTER TABLE [dbo].[Repayments] ADD  CONSTRAINT [DF_Repayments_RepaymentDate]  DEFAULT (CONVERT([date],getdate())) FOR [RepaymentDate]
GO
ALTER TABLE [dbo].[Repayments] ADD  CONSTRAINT [DF_Repayments_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[StockMovements] ADD  CONSTRAINT [DF_StockMovements_MovementDate]  DEFAULT (getdate()) FOR [MovementDate]
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
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_BuyInvoices] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[BuyInvoices] ([InvoiceID])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_BuyInvoices]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Suppliers] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Suppliers]
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
ALTER TABLE [dbo].[Repayments]  WITH CHECK ADD  CONSTRAINT [FK_Repayments_Loans] FOREIGN KEY([LoanID])
REFERENCES [dbo].[Loans] ([LoanID])
GO
ALTER TABLE [dbo].[Repayments] CHECK CONSTRAINT [FK_Repayments_Loans]
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
ALTER TABLE [dbo].[Barcodes]  WITH CHECK ADD  CONSTRAINT [CK_Barcodes_type] CHECK  (([BarcodeType]='Custom' OR [BarcodeType]='QR' OR [BarcodeType]='EAN' OR [BarcodeType]='UPC'))
GO
ALTER TABLE [dbo].[Barcodes] CHECK CONSTRAINT [CK_Barcodes_type]
GO
ALTER TABLE [dbo].[BuyInvoices]  WITH CHECK ADD  CONSTRAINT [CK_BuyInvoices_status] CHECK  (([InvoiceStatus]='Partial' OR [InvoiceStatus]='Unpaid' OR [InvoiceStatus]='Paid'))
GO
ALTER TABLE [dbo].[BuyInvoices] CHECK CONSTRAINT [CK_BuyInvoices_status]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [CK_Payments_PaymentMethod] CHECK  (([PaymentMethod]='BankTransfer' OR [PaymentMethod]='CreditCard' OR [PaymentMethod]='Cash' OR [PaymentMethod]='Cheque'))
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

/****** Object:  Table [dbo].[ExpenseCategories] ******/
CREATE TABLE [dbo].[ExpenseCategories](
	[ExpenseCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_ExpenseCategories] PRIMARY KEY CLUSTERED
(
	[ExpenseCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expenses] ******/
CREATE TABLE [dbo].[Expenses](
	[ExpenseID] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseCategoryID] [int] NOT NULL,
	[SupplierID] [int] NULL,
	[ExpenseDate] [date] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentMethod] [nvarchar](20) NOT NULL,
	[ReferenceNumber] [nvarchar](100) NULL,
	[Notes] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED
(
	[ExpenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Expenses] ADD CONSTRAINT [DF_Expenses_CreatedDate] DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Expenses] WITH CHECK ADD CONSTRAINT [FK_Expenses_ExpenseCategories] FOREIGN KEY([ExpenseCategoryID])
REFERENCES [dbo].[ExpenseCategories] ([ExpenseCategoryID])
GO
ALTER TABLE [dbo].[Expenses] CHECK CONSTRAINT [FK_Expenses_ExpenseCategories]
GO
ALTER TABLE [dbo].[Expenses] WITH CHECK ADD CONSTRAINT [FK_Expenses_Suppliers] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[Expenses] CHECK CONSTRAINT [FK_Expenses_Suppliers]
GO
ALTER TABLE [dbo].[Expenses] WITH CHECK ADD CONSTRAINT [CK_Expenses_Amount] CHECK (([Amount]>(0)))
GO
ALTER TABLE [dbo].[Expenses] CHECK CONSTRAINT [CK_Expenses_Amount]
GO
CREATE NONCLUSTERED INDEX [IX_Expenses_ExpenseDate] ON [dbo].[Expenses]
(
	[ExpenseDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Expenses_ExpenseCategory] ON [dbo].[Expenses]
(
	[ExpenseCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Expenses] WITH CHECK ADD CONSTRAINT [CK_Expenses_PaymentMethod] CHECK (([PaymentMethod]='BankTransfer' OR [PaymentMethod]='CreditCard' OR [PaymentMethod]='Cash'))
GO
ALTER TABLE [dbo].[Expenses] CHECK CONSTRAINT [CK_Expenses_PaymentMethod]
GO
