CREATE TABLE [dbo].[tblFinancialTrade] (
    [pkFinancialTradeId] INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]      INT             NULL,
    [TotalAccounts]      INT             NULL,
    [AvgFee]             DECIMAL (18, 2) NULL,
    [OverallForexMargin] DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblFinancialTrade] PRIMARY KEY CLUSTERED ([pkFinancialTradeId] ASC)
);

