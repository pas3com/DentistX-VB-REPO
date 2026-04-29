USE [DentistX]
GO
/****** Object:  Table [dbo].[OrthoDiag]    Script Date: 10/03/2026 4:35:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrthoDiag](
	[DiagID] [int] IDENTITY(1,1) NOT NULL,
	[OrthoID] [int] NULL,
	[PatientID] [int] NOT NULL,
	[CloseType] [nvarchar](150) NULL,
	[ClassI] [nvarchar](150) NULL,
	[Bite] [nvarchar](150) NULL,
 CONSTRAINT [PK_OrthoDiag_1] PRIMARY KEY CLUSTERED 
(
	[DiagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrthoInf]    Script Date: 10/03/2026 4:35:21 PM ******/
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
	[TreatDate] [smalldatetime] NULL,
 CONSTRAINT [PK_OrthoInf_1] PRIMARY KEY CLUSTERED 
(
	[OrthoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrthoTreat]    Script Date: 10/03/2026 4:35:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrthoTreat](
	[TreatID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[OrthoID] [int] NULL,
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
 CONSTRAINT [PK_OrthoTreat_1] PRIMARY KEY CLUSTERED 
(
	[TreatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrthoTrtDet]    Script Date: 10/03/2026 4:35:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrthoTrtDet](
	[DetID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[OrthoID] [int] NULL,
	[WorkDate] [smalldatetime] NULL,
	[WireMeasure] [nvarchar](50) NULL,
	[WireType] [nvarchar](50) NULL,
	[WireImg] [nvarchar](50) NULL,
	[WireNotes] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrthoTrtDet_1] PRIMARY KEY CLUSTERED 
(
	[DetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 10/03/2026 4:35:21 PM ******/
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
	[WhatsApp] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Health] [nvarchar](50) NULL,
	[Treat] [bit] NULL,
	[Implant] [bit] NULL,
	[Mobile] [bit] NULL,
	[Ortho] [bit] NULL,
	[Diag] [bit] NULL,
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
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [DF_Patient_StillKid]  DEFAULT ((0)) FOR [StillKid]
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
