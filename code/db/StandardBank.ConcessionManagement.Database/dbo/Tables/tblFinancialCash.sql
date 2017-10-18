CREATE TABLE [dbo].[tblFinancialCash] (
    [pkFinancialCashId]          INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]              INT             NOT NULL,
    [WeightedAverageBranchPrice] DECIMAL (18, 2) NOT NULL,
    [TotalCashCentrCashTurnover] DECIMAL (18, 2) NOT NULL,
    [TotalCashCentrCashVolume]   DECIMAL (18, 2) NOT NULL,
    [TotalBranchCashTurnover]    DECIMAL (18, 2) NOT NULL,
    [TotalBranchCashVolume]      DECIMAL (18, 2) NOT NULL,
    [TotalAutosafeCashTurnover]  DECIMAL (18, 2) NOT NULL,
    [TotalAutosafeCashVolume]    DECIMAL (18, 2) NOT NULL,
    [WeightedAverageCCPrice]     DECIMAL (18, 2) NOT NULL,
    [WeightedAverageAFPrice]     DECIMAL (18, 2) NOT NULL,
    [LatestCrsOrMrs]             DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_tblFinancialCash] PRIMARY KEY CLUSTERED ([pkFinancialCashId] ASC),
    CONSTRAINT [FK_tblFinancialCash_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);

