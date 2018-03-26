CREATE TABLE [dbo].[rtblBOLChargeCodeType] (
    [pkChargeCodeTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Description]        VARCHAR (250) NULL,
    CONSTRAINT [PK_rtblChargeCodeType] PRIMARY KEY CLUSTERED ([pkChargeCodeTypeId] ASC)
);
GO



CREATE TABLE [dbo].[rtblBOLChargeCode] (
    [pkChargeCodeId]     INT           IDENTITY (1, 1) NOT NULL,
    [Description]        VARCHAR (250) NULL,
    [ChargeCode]         VARCHAR (50)  NULL,
    [Length]             INT           NULL,
    [fkChargeCodeTypeId] INT           NULL,
    CONSTRAINT [PK_rtblChargeCode] PRIMARY KEY CLUSTERED ([pkChargeCodeId] ASC),
    CONSTRAINT [FK_rtblChargeCode_rtblChargeCodeType] FOREIGN KEY ([fkChargeCodeTypeId]) REFERENCES [dbo].[rtblBOLChargeCodeType] ([pkChargeCodeTypeId])
);
GO

CREATE TABLE [dbo].[tblFinancialBol] (
    [pkFinancialBolId] INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]    INT             NULL,
    [TotalPayments]    DECIMAL (18, 2) NULL,
    [TotalCollections] DECIMAL (18, 2) NULL,
    [TotalValueAdded]  DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblFinancialBol] PRIMARY KEY CLUSTERED ([pkFinancialBolId] ASC)
);
GO

CREATE TABLE [dbo].[tblLegalEntityBOLUser] (
    [pkLegalEntityBOLUserId] INT           IDENTITY (1, 1) NOT NULL,
    [fkLegalEntityAccountId] INT           NULL,
    [BOLUserId]              VARCHAR (250) NULL,
    CONSTRAINT [PK_tblLegalEntityBOLUser] PRIMARY KEY CLUSTERED ([pkLegalEntityBOLUserId] ASC),
    CONSTRAINT [FK_tblLegalEntityBOLUser_tblLegalEntity] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
);
GO

CREATE TABLE [dbo].[tblProductBOL] (
    [pkProductBOLId]         INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]          INT             NULL,
    [fkLegalEntityId]        INT             NULL,
    [fkLegalEntityBOLUserId] INT             NULL,
    [fkChargeCodeId]         INT             NULL,
    [LoadedRate]             DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblProductBOL] PRIMARY KEY CLUSTERED ([pkProductBOLId] ASC),
    CONSTRAINT [FK_tblProductBOL_rtblBOLChargeCode] FOREIGN KEY ([fkChargeCodeId]) REFERENCES [dbo].[rtblBOLChargeCode] ([pkChargeCodeId]),
    CONSTRAINT [FK_tblProductBOL_tblLegalEntity] FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId]),
    CONSTRAINT [FK_tblProductBOL_tblLegalEntityBOLUser] FOREIGN KEY ([fkLegalEntityBOLUserId]) REFERENCES [dbo].[tblLegalEntityBOLUser] ([pkLegalEntityBOLUserId]),
    CONSTRAINT [FK_tblProductBOL_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);
GO


/****** Object:  Table [dbo].[tblConcessionBol]    Script Date: 2018/03/22 11:07:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblConcessionBol](
	[pkConcessionBolId] [int] IDENTITY(1,1) NOT NULL,
	[fkConcessionId] [int] NOT NULL,
	[fkConcessionDetailId] [int] NOT NULL,
	[fkLegalEntityBOLUserId] [int] NULL,
	[fkChargeCodeId] [int] NULL,
	[LoadedRate] [decimal](18, 2) NULL,
	[ApprovedRate] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tblConcessionBol] PRIMARY KEY CLUSTERED 
(
	[pkConcessionBolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblConcessionBol]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionBol_rtblBOLChargeCode] FOREIGN KEY([fkChargeCodeId])
REFERENCES [dbo].[rtblBOLChargeCode] ([pkChargeCodeId])
GO

ALTER TABLE [dbo].[tblConcessionBol] CHECK CONSTRAINT [FK_tblConcessionBol_rtblBOLChargeCode]
GO

ALTER TABLE [dbo].[tblConcessionBol]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionBol_tblBolUser] FOREIGN KEY([fkLegalEntityBOLUserId])
REFERENCES [dbo].[tblLegalEntityBOLUser] ([pkLegalEntityBOLUserId])
GO

ALTER TABLE [dbo].[tblConcessionBol] CHECK CONSTRAINT [FK_tblConcessionBol_tblBolUser]
GO

ALTER TABLE [dbo].[tblConcessionBol]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionBol_tblConcession] FOREIGN KEY([fkConcessionId])
REFERENCES [dbo].[tblConcession] ([pkConcessionId])
GO

ALTER TABLE [dbo].[tblConcessionBol] CHECK CONSTRAINT [FK_tblConcessionBol_tblConcession]
GO

ALTER TABLE [dbo].[tblConcessionBol]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionBol_tblConcessionDetail] FOREIGN KEY([fkConcessionDetailId])
REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
GO

ALTER TABLE [dbo].[tblConcessionBol] CHECK CONSTRAINT [FK_tblConcessionBol_tblConcessionDetail]
GO




ALTER TABLE tblProductBOL
ALTER column LoadedRate  varchar (50);




