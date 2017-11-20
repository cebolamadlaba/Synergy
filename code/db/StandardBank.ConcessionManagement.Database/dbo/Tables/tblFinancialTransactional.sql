CREATE TABLE [dbo].[tblFinancialTransactional] (
    [pkFinancialTransactionalId]   INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]                INT             NOT NULL,
    [TotalNumberOfAccounts]        DECIMAL (18, 2) NOT NULL,
    [AverageAccountManagementFee]  DECIMAL (18, 2) NOT NULL,
    [AverageMinimumMonthlyFee]     DECIMAL (18, 2) NOT NULL,
    [TotalChequeIssuingVolumes]    DECIMAL (18, 2) NOT NULL,
    [TotalChequeDepositVolumes]    DECIMAL (18, 2) NOT NULL,
    [TotalChequeEncashmentVolumes] DECIMAL (18, 2) NOT NULL,
    [TotalChequeEncashmentValues]  DECIMAL (18, 2) NOT NULL,
    [TotalCashWithdrawalVolumes]   DECIMAL (18, 2) NOT NULL,
    [TotalCashWithdrawalValues]    DECIMAL (18, 2) NOT NULL,
    [AverageChequeIssuingValue]    DECIMAL (18, 2) NOT NULL,
    [AverageChequeIssuingPrice]    DECIMAL (18, 2) NOT NULL,
    [AverageChequeDepositValue]    DECIMAL (18, 2) NOT NULL,
    [AverageChequeDepositPrice]    DECIMAL (18, 2) NOT NULL,
    [AverageChequeEncashmentPrice] DECIMAL (18, 2) NOT NULL,
    [AverageCashWithdrawalPrice]   DECIMAL (18, 2) NOT NULL,
    [LatestCrsOrMrs]               DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_tblFinancialTransactional] PRIMARY KEY CLUSTERED ([pkFinancialTransactionalId] ASC),
    CONSTRAINT [FK_tblFinancialTransactional_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);




GO
CREATE NONCLUSTERED INDEX [IX_tblFinancialTransactional]
    ON [dbo].[tblFinancialTransactional]([fkRiskGroupId] ASC);

