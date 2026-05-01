USE [DentistX]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'[dbo].[LabMsgDetail]', N'U') IS NOT NULL
	DROP TABLE [dbo].[LabMsgDetail]
GO

IF OBJECT_ID(N'[dbo].[LabMsgs]', N'U') IS NOT NULL
	DROP TABLE [dbo].[LabMsgs]
GO

IF OBJECT_ID(N'[dbo].[LabMsgDetailChoice]', N'U') IS NOT NULL
	DROP TABLE [dbo].[LabMsgDetailChoice]
GO

IF OBJECT_ID(N'[dbo].[LabMsgSubject]', N'U') IS NOT NULL
	DROP TABLE [dbo].[LabMsgSubject]
GO

CREATE TABLE [dbo].[LabMsgSubject](
	[LabMsgSubjectID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [nvarchar](150) NOT NULL,
	[SortOrder] [int] NOT NULL CONSTRAINT [DF_LabMsgSubject_SortOrder] DEFAULT ((1)),
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_LabMsgSubject_IsActive] DEFAULT ((1)),
 CONSTRAINT [PK_LabMsgSubject] PRIMARY KEY CLUSTERED
(
	[LabMsgSubjectID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_LabMsgSubject_SubjectName] UNIQUE NONCLUSTERED
(
	[SubjectName] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LabMsgDetailChoice](
	[LabMsgDetailChoiceID] [int] IDENTITY(1,1) NOT NULL,
	[LabMsgSubjectID] [int] NOT NULL,
	[DetailText] [nvarchar](300) NOT NULL,
	[SortOrder] [int] NOT NULL CONSTRAINT [DF_LabMsgDetailChoice_SortOrder] DEFAULT ((1)),
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_LabMsgDetailChoice_IsActive] DEFAULT ((1)),
 CONSTRAINT [PK_LabMsgDetailChoice] PRIMARY KEY CLUSTERED
(
	[LabMsgDetailChoiceID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_LabMsgDetailChoice_Subject_Detail] UNIQUE NONCLUSTERED
(
	[LabMsgSubjectID] ASC,
	[DetailText] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LabMsgs](
	[LabMsgID] [int] IDENTITY(1,1) NOT NULL,
	[ClinicID] [uniqueidentifier] NULL,
	[ClinicName] [nvarchar](200) NOT NULL,
	[LabID] [int] NULL,
	[LabName] [nvarchar](200) NOT NULL,
	[PatientID] [int] NULL,
	[PatientName] [nvarchar](200) NOT NULL,
	[LabMsgSubjectID] [int] NOT NULL,
	[SubjectText] [nvarchar](250) NOT NULL,
	[ReceiveDate] [smalldatetime] NULL,
	[Note] [nvarchar](1000) NULL,
	[MessageBody] [nvarchar](max) NULL,
	[MsgDate] [smalldatetime] NOT NULL CONSTRAINT [DF_LabMsgs_MsgDate] DEFAULT (GETDATE()),
	[SentDate] [smalldatetime] NULL,
	[IsSent] [bit] NOT NULL CONSTRAINT [DF_LabMsgs_IsSent] DEFAULT ((0)),
 CONSTRAINT [PK_LabMsgs] PRIMARY KEY CLUSTERED
(
	[LabMsgID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[LabMsgDetail](
	[LabMsgDetailID] [int] IDENTITY(1,1) NOT NULL,
	[LabMsgID] [int] NOT NULL,
	[LabMsgDetailChoiceID] [int] NULL,
	[DetailText] [nvarchar](300) NOT NULL,
	[SortOrder] [int] NOT NULL CONSTRAINT [DF_LabMsgDetail_SortOrder] DEFAULT ((1)),
 CONSTRAINT [PK_LabMsgDetail] PRIMARY KEY CLUSTERED
(
	[LabMsgDetailID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LabMsgDetailChoice] WITH CHECK ADD CONSTRAINT [FK_LabMsgDetailChoice_LabMsgSubject]
FOREIGN KEY([LabMsgSubjectID]) REFERENCES [dbo].[LabMsgSubject] ([LabMsgSubjectID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[LabMsgDetailChoice] CHECK CONSTRAINT [FK_LabMsgDetailChoice_LabMsgSubject]
GO

ALTER TABLE [dbo].[LabMsgs] WITH CHECK ADD CONSTRAINT [FK_LabMsgs_LabMsgSubject]
FOREIGN KEY([LabMsgSubjectID]) REFERENCES [dbo].[LabMsgSubject] ([LabMsgSubjectID])
GO

ALTER TABLE [dbo].[LabMsgs] CHECK CONSTRAINT [FK_LabMsgs_LabMsgSubject]
GO

ALTER TABLE [dbo].[LabMsgDetail] WITH CHECK ADD CONSTRAINT [FK_LabMsgDetail_LabMsgs]
FOREIGN KEY([LabMsgID]) REFERENCES [dbo].[LabMsgs] ([LabMsgID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[LabMsgDetail] CHECK CONSTRAINT [FK_LabMsgDetail_LabMsgs]
GO

ALTER TABLE [dbo].[LabMsgDetail] WITH CHECK ADD CONSTRAINT [FK_LabMsgDetail_LabMsgDetailChoice]
FOREIGN KEY([LabMsgDetailChoiceID]) REFERENCES [dbo].[LabMsgDetailChoice] ([LabMsgDetailChoiceID])
GO

ALTER TABLE [dbo].[LabMsgDetail] CHECK CONSTRAINT [FK_LabMsgDetail_LabMsgDetailChoice]
GO

CREATE NONCLUSTERED INDEX [IX_LabMsgs_LabID_MsgDate]
ON [dbo].[LabMsgs]([LabID] ASC, [MsgDate] DESC)
GO

CREATE NONCLUSTERED INDEX [IX_LabMsgs_LabName_MsgDate]
ON [dbo].[LabMsgs]([LabName] ASC, [MsgDate] DESC)
GO

CREATE NONCLUSTERED INDEX [IX_LabMsgDetail_LabMsgID_SortOrder]
ON [dbo].[LabMsgDetail]([LabMsgID] ASC, [SortOrder] ASC)
GO

INSERT INTO [dbo].[LabMsgSubject] ([SubjectName], [SortOrder], [IsActive])
VALUES
	(N'إعادة طباعة مؤقت', 1, 1),
	(N'خراطة أسنان', 2, 1),
	(N'معلومات عامة', 3, 1)
GO

INSERT INTO [dbo].[LabMsgDetailChoice] ([LabMsgSubjectID], [DetailText], [SortOrder], [IsActive])
SELECT s.[LabMsgSubjectID], v.[DetailText], v.[SortOrder], 1
FROM [dbo].[LabMsgSubject] s
INNER JOIN (
	VALUES
		(N'إعادة طباعة مؤقت', N'طباعة مؤقت علوي - جزئي', 1),
		(N'إعادة طباعة مؤقت', N'طباعة مؤقت علوي - كامل', 2),
		(N'إعادة طباعة مؤقت', N'طباعة مؤقت سفلي - جزئي', 3),
		(N'إعادة طباعة مؤقت', N'طباعة مؤقت سفلي - كامل', 4),
		(N'خراطة أسنان', N'خراطة دائم علوي - جزئي', 1),
		(N'خراطة أسنان', N'خراطة دائم علوي - كامل', 2),
		(N'خراطة أسنان', N'خراطة دائم سفلي - جزئي', 3),
		(N'خراطة أسنان', N'خراطة دائم سفلي - كامل', 4),
		(N'معلومات عامة', N'ملاحظات عامة', 1)
) v([SubjectName], [DetailText], [SortOrder])
	ON s.[SubjectName] = v.[SubjectName]
GO
