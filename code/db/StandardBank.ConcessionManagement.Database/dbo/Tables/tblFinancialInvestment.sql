CREATE TABLE [dbo].[tblFinancialInvestment] (
    [pkFinancialInvestmentId]  INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]            INT             NOT NULL,
    [TotalLiabilityBalances]   DECIMAL (18, 2) NOT NULL,
    [WeightedAverageMTP]       DECIMAL (18, 2) NOT NULL,
    [WeightedAverageNetMargin] DECIMAL (18, 2) NOT NULL,
    [LatestCrsOrMrs]           DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_tblFinancialInvestment] PRIMARY KEY CLUSTERED ([pkFinancialInvestmentId] ASC),
    CONSTRAINT [FK_tblFinancialInvestment_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);

