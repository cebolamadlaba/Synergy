USE [CMS_Dev_V2]
GO

/****** Object:  Table [dbo].[rtblBOLChargeCodeType]    Script Date: 2018/03/14 12:51:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[rtblBOLChargeCodeType](
	[pkChargeCodeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](250) NULL,
 CONSTRAINT [PK_rtblChargeCodeType] PRIMARY KEY CLUSTERED 
(
	[pkChargeCodeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[rtblBOLChargeCode]    Script Date: 2018/03/14 12:51:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[rtblBOLChargeCode](
	[pkChargeCodeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](250) NULL,
	[ChargeCode] [varchar](50) NULL,
	[Length] [int] NULL,
	[fkChargeCodeTypeId] [int] NULL,
 CONSTRAINT [PK_rtblChargeCode] PRIMARY KEY CLUSTERED 
(
	[pkChargeCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[rtblBOLChargeCode]  WITH CHECK ADD  CONSTRAINT [FK_rtblChargeCode_rtblChargeCodeType] FOREIGN KEY([fkChargeCodeTypeId])
REFERENCES [dbo].[rtblBOLChargeCodeType] ([pkChargeCodeTypeId])
GO

ALTER TABLE [dbo].[rtblBOLChargeCode] CHECK CONSTRAINT [FK_rtblChargeCode_rtblChargeCodeType]
GO



/****** Object:  Table [dbo].[tblLegalEntityBOLUser]    Script Date: 2018/03/14 12:51:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblLegalEntityBOLUser](
	[pkLegalEntityBOLUserId] [int] IDENTITY(1,1) NOT NULL,
	[fkLegalEntityAccountId] [int] NULL,
	[BOLUserId] [varchar](250) NULL,
 CONSTRAINT [PK_tblLegalEntityBOLUser] PRIMARY KEY CLUSTERED 
(
	[pkLegalEntityBOLUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblLegalEntityBOLUser]  WITH CHECK ADD  CONSTRAINT [FK_tblLegalEntityBOLUser_tblLegalEntity] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblLegalEntityBOLUser] CHECK CONSTRAINT [FK_tblLegalEntityBOLUser_tblLegalEntity]
GO



/****** Object:  Table [dbo].[tblProductBOL]    Script Date: 2018/03/14 12:52:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblProductBOL](
	[pkProductBOLId] [int] IDENTITY(1,1) NOT NULL,
	[fkRiskGroupId] [int] NULL,
	[fkLegalEntityId] [int] NULL,
	[fkLegalEntityBOLUserId] [int] NULL,
	[fkChargeCodeId] [int] NULL,
	[LoadedRate] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tblProductBOL] PRIMARY KEY CLUSTERED 
(
	[pkProductBOLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblProductBOL]  WITH CHECK ADD  CONSTRAINT [FK_tblProductBOL_rtblBOLChargeCode] FOREIGN KEY([fkChargeCodeId])
REFERENCES [dbo].[rtblBOLChargeCode] ([pkChargeCodeId])
GO

ALTER TABLE [dbo].[tblProductBOL] CHECK CONSTRAINT [FK_tblProductBOL_rtblBOLChargeCode]
GO

ALTER TABLE [dbo].[tblProductBOL]  WITH CHECK ADD  CONSTRAINT [FK_tblProductBOL_tblLegalEntity] FOREIGN KEY([fkLegalEntityId])
REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId])
GO

ALTER TABLE [dbo].[tblProductBOL] CHECK CONSTRAINT [FK_tblProductBOL_tblLegalEntity]
GO

ALTER TABLE [dbo].[tblProductBOL]  WITH CHECK ADD  CONSTRAINT [FK_tblProductBOL_tblLegalEntityBOLUser] FOREIGN KEY([fkLegalEntityBOLUserId])
REFERENCES [dbo].[tblLegalEntityBOLUser] ([pkLegalEntityBOLUserId])
GO

ALTER TABLE [dbo].[tblProductBOL] CHECK CONSTRAINT [FK_tblProductBOL_tblLegalEntityBOLUser]
GO

ALTER TABLE [dbo].[tblProductBOL]  WITH CHECK ADD  CONSTRAINT [FK_tblProductBOL_tblRiskGroup] FOREIGN KEY([fkRiskGroupId])
REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
GO

ALTER TABLE [dbo].[tblProductBOL] CHECK CONSTRAINT [FK_tblProductBOL_tblRiskGroup]
GO











