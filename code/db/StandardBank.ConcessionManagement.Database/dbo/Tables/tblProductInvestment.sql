CREATE TABLE [dbo].[tblProductInvestment] (
    [pkProductInvestmentId]  INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]          INT             NOT NULL,
    [fkLegalEntityId]        INT             NOT NULL,
    [fkLegalEntityAccountId] INT             NOT NULL,
    [fkProductId]            INT             NOT NULL,
    [AverageBalance]         DECIMAL (18, 2) NOT NULL,
    [LoadedCustomerRate]     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_tblProductInvestment] PRIMARY KEY CLUSTERED ([pkProductInvestmentId] ASC),
    CONSTRAINT [FK_tblProductInvestment_rtblProduct] FOREIGN KEY ([fkProductId]) REFERENCES [dbo].[rtblProduct] ([pkProductId]),
    CONSTRAINT [FK_tblProductInvestment_tblLegalEntity] FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId]),
    CONSTRAINT [FK_tblProductInvestment_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId]),
    CONSTRAINT [FK_tblProductInvestment_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);

