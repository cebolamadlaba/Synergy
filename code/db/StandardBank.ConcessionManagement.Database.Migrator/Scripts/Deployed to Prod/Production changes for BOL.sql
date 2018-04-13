use cms;

drop table tblConcessionBol;


GO
DROP TABLE [dbo].[tblBolUser];




GO
PRINT N'Creating [dbo].[rtblBOLChargeCode]...';


GO
CREATE TABLE [dbo].[rtblBOLChargeCode] (
    [pkChargeCodeId]     INT           IDENTITY (1, 1) NOT NULL,
    [Description]        VARCHAR (250) NULL,
    [ChargeCode]         VARCHAR (50)  NULL,
    [Length]             INT           NULL,
    [fkChargeCodeTypeId] INT           NULL,
    CONSTRAINT [PK_rtblChargeCode] PRIMARY KEY CLUSTERED ([pkChargeCodeId] ASC)
);


GO
PRINT N'Creating [dbo].[rtblBOLChargeCodeType]...';


GO
CREATE TABLE [dbo].[rtblBOLChargeCodeType] (
    [pkChargeCodeTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Description]        VARCHAR (250) NULL,
    CONSTRAINT [PK_rtblChargeCodeType] PRIMARY KEY CLUSTERED ([pkChargeCodeTypeId] ASC)
);


GO
PRINT N'Creating [dbo].[rtblPrimeRate]...';


GO
CREATE TABLE [dbo].[rtblPrimeRate] (
    [PRM_ID]          INT        IDENTITY (1, 1) NOT NULL,
    [PRM_Prime_Rate]  FLOAT (53) NULL,
    [PRM_Active_From] DATETIME   NULL,
    CONSTRAINT [PK_rtblPrimeRate] PRIMARY KEY CLUSTERED ([PRM_ID] ASC)
);


GO
PRINT N'Creating [dbo].[tblFinancialBol]...';


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
PRINT N'Creating [dbo].[tblLegalEntityBOLUser]...';


GO
CREATE TABLE [dbo].[tblLegalEntityBOLUser] (
    [pkLegalEntityBOLUserId] INT           IDENTITY (1, 1) NOT NULL,
    [fkLegalEntityAccountId] INT           NULL,
    [BOLUserId]              VARCHAR (250) NULL,
    CONSTRAINT [PK_tblLegalEntityBOLUser] PRIMARY KEY CLUSTERED ([pkLegalEntityBOLUserId] ASC)
);


GO
PRINT N'Creating [dbo].[tblProductBOL]...';


GO
CREATE TABLE [dbo].[tblProductBOL] (
    [pkProductBOLId]         INT          IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]          INT          NULL,
    [fkLegalEntityId]        INT          NULL,
    [fkLegalEntityBOLUserId] INT          NULL,
    [fkChargeCodeId]         INT          NULL,
    [LoadedRate]             VARCHAR (50) NULL,
    CONSTRAINT [PK_tblProductBOL] PRIMARY KEY CLUSTERED ([pkProductBOLId] ASC)
);


GO
ALTER TABLE [dbo].[rtblBOLChargeCode] WITH NOCHECK
    ADD CONSTRAINT [FK_rtblChargeCode_rtblChargeCodeType] FOREIGN KEY ([fkChargeCodeTypeId]) REFERENCES [dbo].[rtblBOLChargeCodeType] ([pkChargeCodeTypeId]);


GO
PRINT N'Creating [dbo].[FK_tblLegalEntityBOLUser_tblLegalEntity]...';


GO
ALTER TABLE [dbo].[tblLegalEntityBOLUser] WITH NOCHECK
    ADD CONSTRAINT [FK_tblLegalEntityBOLUser_tblLegalEntity] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId]);


GO
PRINT N'Creating [dbo].[FK_tblProductBOL_rtblBOLChargeCode]...';


GO
ALTER TABLE [dbo].[tblProductBOL] WITH NOCHECK
    ADD CONSTRAINT [FK_tblProductBOL_rtblBOLChargeCode] FOREIGN KEY ([fkChargeCodeId]) REFERENCES [dbo].[rtblBOLChargeCode] ([pkChargeCodeId]);


GO
PRINT N'Creating [dbo].[FK_tblProductBOL_tblRiskGroup]...';


GO
ALTER TABLE [dbo].[tblProductBOL] WITH NOCHECK
    ADD CONSTRAINT [FK_tblProductBOL_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId]);


GO
PRINT N'Creating [dbo].[FK_tblProductBOL_tblLegalEntity]...';


GO
ALTER TABLE [dbo].[tblProductBOL] WITH NOCHECK
    ADD CONSTRAINT [FK_tblProductBOL_tblLegalEntity] FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId]);


GO
PRINT N'Creating [dbo].[FK_tblProductBOL_tblLegalEntityBOLUser]...';


GO
ALTER TABLE [dbo].[tblProductBOL] WITH NOCHECK
    ADD CONSTRAINT [FK_tblProductBOL_tblLegalEntityBOLUser] FOREIGN KEY ([fkLegalEntityBOLUserId]) REFERENCES [dbo].[tblLegalEntityBOLUser] ([pkLegalEntityBOLUserId]);


GO
PRINT N'Checking existing data against newly created constraints';





GO

ALTER TABLE [dbo].[rtblBOLChargeCode] WITH CHECK CHECK CONSTRAINT [FK_rtblChargeCode_rtblChargeCodeType];

ALTER TABLE [dbo].[tblLegalEntityBOLUser] WITH CHECK CHECK CONSTRAINT [FK_tblLegalEntityBOLUser_tblLegalEntity];

ALTER TABLE [dbo].[tblProductBOL] WITH CHECK CHECK CONSTRAINT [FK_tblProductBOL_rtblBOLChargeCode];

ALTER TABLE [dbo].[tblProductBOL] WITH CHECK CHECK CONSTRAINT [FK_tblProductBOL_tblRiskGroup];

ALTER TABLE [dbo].[tblProductBOL] WITH CHECK CHECK CONSTRAINT [FK_tblProductBOL_tblLegalEntity];

ALTER TABLE [dbo].[tblProductBOL] WITH CHECK CHECK CONSTRAINT [FK_tblProductBOL_tblLegalEntityBOLUser];


GO
PRINT N'Update complete.';


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


update rtblConcessionType set Code = 'Bol' where Description = 'Business Online';
update rtblPeriodType set description = 'Once-off' where pkPeriodTypeId = 1;

INSERT INTO [dbo].[rtblPrimeRate]
           ([PRM_Prime_Rate]
           ,[PRM_Active_From])
     VALUES (0, '2017-01-01')

INSERT INTO [dbo].[rtblPrimeRate]
           ([PRM_Prime_Rate]
           ,[PRM_Active_From])
     VALUES (10.0, '2018-04-01')

INSERT INTO [dbo].[rtblProduct]
           ([fkConcessionTypeId]
           ,[Description]
           ,[IsActive])
		   values (1, 'Temporary Overdraft', (1))

