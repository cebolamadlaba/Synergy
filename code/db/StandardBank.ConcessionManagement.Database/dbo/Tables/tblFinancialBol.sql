CREATE TABLE [dbo].[tblFinancialBol] (
    [pkFinancialBolId] INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]    INT             NULL,
    [TotalPayments]    DECIMAL (18, 2) NULL,
    [TotalCollections] DECIMAL (18, 2) NULL,
    [TotalValueAdded]  DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblFinancialBol] PRIMARY KEY CLUSTERED ([pkFinancialBolId] ASC)
);

