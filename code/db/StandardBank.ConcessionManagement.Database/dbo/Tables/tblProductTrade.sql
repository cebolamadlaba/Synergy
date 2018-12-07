CREATE TABLE [dbo].[tblProductTrade] (
    [pkProductTradeId]       INT          IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]          INT          NULL,
    [fkLegalEntityId]        INT          NULL,
    [fkLegalEntityAccountId] INT          NULL,
    [fkTradeProductId]       INT          NULL,
    [LoadedRate]             VARCHAR (50) NULL,
    CONSTRAINT [PK_tblProductTrade] PRIMARY KEY CLUSTERED ([pkProductTradeId] ASC),
    CONSTRAINT [FK_tblProductTrade_tblLegalEntity] FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId]),
    CONSTRAINT [FK_tblProductTrade_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId]),
    CONSTRAINT [FK_tblProductTrade_tblProductTrade] FOREIGN KEY ([pkProductTradeId]) REFERENCES [dbo].[tblProductTrade] ([pkProductTradeId]),
    CONSTRAINT [FK_tblProductTrade_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);

