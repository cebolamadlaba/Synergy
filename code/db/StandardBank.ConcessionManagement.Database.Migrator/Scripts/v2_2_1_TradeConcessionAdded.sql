
GO
CREATE TABLE [dbo].[rtblTradeProduct] (
    [pkTradeProductId]     INT           IDENTITY (1, 1) NOT NULL,
    [Description]          VARCHAR (250) NULL,
    [fkTradeProductTypeId] INT           NULL,
    CONSTRAINT [PK_rtblTradeCode] PRIMARY KEY CLUSTERED ([pkTradeProductId] ASC)
);


GO
PRINT N'Creating [dbo].[rtblTradeProductType]...';


GO
CREATE TABLE [dbo].[rtblTradeProductType] (
    [pkTradeProductTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Description]          VARCHAR (250) NULL,
    CONSTRAINT [PK_rtblTradeCodeType] PRIMARY KEY CLUSTERED ([pkTradeProductTypeId] ASC)
);


GO
PRINT N'Creating [dbo].[tblConcessionTrade]...';


GO
CREATE TABLE [dbo].[tblConcessionTrade] (
    [pkConcessionTradeId]    INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]         INT             NOT NULL,
    [fkConcessionDetailId]   INT             NOT NULL,
    [fkTradeProductId]       INT             NULL,
    [fkLegalEntityAccountId] INT             NULL,
    [LoadedRate]             INT             NULL,
    [ApprovedRate]           INT             NULL,
    [GBBNumber]              VARCHAR (250)   NULL,
    [Term]                   INT             NULL,
    [Min]                    DECIMAL (18, 2) NULL,
    [Max]                    DECIMAL (18, 2) NULL,
    [Communication]          VARCHAR (250)   NULL,
    [FlatFee]                DECIMAL (18, 2) NULL,
    [EstablishmentFee]       DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblConcessionTrade] PRIMARY KEY CLUSTERED ([pkConcessionTradeId] ASC)
);


GO
PRINT N'Creating [dbo].[tblFinancialTrade]...';


GO
CREATE TABLE [dbo].[tblFinancialTrade] (
    [pkFinancialTradeId] INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]      INT             NULL,
    [TotalAccounts]      INT             NULL,
    [AvgFee]             DECIMAL (18, 2) NULL,
    [OverallForexMargin] DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblFinancialTrade] PRIMARY KEY CLUSTERED ([pkFinancialTradeId] ASC)
);


GO
PRINT N'Creating [dbo].[FK_rtblTradeCode_rtblTradeCodeType]...';


GO
ALTER TABLE [dbo].[rtblTradeProduct] WITH NOCHECK
    ADD CONSTRAINT [FK_rtblTradeCode_rtblTradeCodeType] FOREIGN KEY ([fkTradeProductTypeId]) REFERENCES [dbo].[rtblTradeProductType] ([pkTradeProductTypeId]);


GO
PRINT N'Creating [dbo].[FK_tblConcessionTrade_tblConcession]...';


GO
ALTER TABLE [dbo].[tblConcessionTrade] WITH NOCHECK
    ADD CONSTRAINT [FK_tblConcessionTrade_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]);


GO
PRINT N'Creating [dbo].[FK_tblConcessionTrade_tblConcessionDetail]...';


GO
ALTER TABLE [dbo].[tblConcessionTrade] WITH NOCHECK
    ADD CONSTRAINT [FK_tblConcessionTrade_tblConcessionDetail] FOREIGN KEY ([fkConcessionDetailId]) REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId]);


GO
PRINT N'Checking existing data against newly created constraints';




GO
ALTER TABLE [dbo].[rtblTradeProduct] WITH CHECK CHECK CONSTRAINT [FK_rtblTradeCode_rtblTradeCodeType];

ALTER TABLE [dbo].[tblConcessionTrade] WITH CHECK CHECK CONSTRAINT [FK_tblConcessionTrade_tblConcession];

ALTER TABLE [dbo].[tblConcessionTrade] WITH CHECK CHECK CONSTRAINT [FK_tblConcessionTrade_tblConcessionDetail];


GO


CREATE TABLE [dbo].[tblLegalEntityGBBNumber] (
    [pkLegalEntityGBBNumber] INT           IDENTITY (1, 1) NOT NULL,
    [fkLegalEntityAccountId] INT           NULL,
    [GBBNumber]              VARCHAR (250) NULL,
    CONSTRAINT [PK_LegalEntityGBBNumber] PRIMARY KEY CLUSTERED ([pkLegalEntityGBBNumber] ASC),
    CONSTRAINT [FK_LegalEntityGBBNumber_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
);
GO

CREATE NONCLUSTERED INDEX [NonClusteredIndex-20180719-111902]
    ON [dbo].[tblLegalEntityGBBNumber]([fkLegalEntityAccountId] ASC);
GO


CREATE TABLE [dbo].[rtblTradeProductImport](
	[pkTradeProductImportId] [int] IDENTITY(1,1) NOT NULL,
	[fkTradeProductId] [int] NULL,
	[ImportFileChargeCode] [varchar](50) NULL,
 CONSTRAINT [PK_rtblTradeProductImport] PRIMARY KEY CLUSTERED 
(
	[pkTradeProductImportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[rtblTradeProductImport]  WITH CHECK ADD  CONSTRAINT [FK_rtblTradeProductImport_rtblTradeProduct] FOREIGN KEY([fkTradeProductId])
REFERENCES [dbo].[rtblTradeProduct] ([pkTradeProductId])
GO

ALTER TABLE [dbo].[rtblTradeProductImport] CHECK CONSTRAINT [FK_rtblTradeProductImport_rtblTradeProduct]
GO

------------------
----Archive of concecession and details.

--------------------------------------


