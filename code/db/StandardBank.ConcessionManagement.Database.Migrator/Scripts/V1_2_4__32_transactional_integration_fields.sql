CREATE TABLE [dbo].[tblProductTransactional](
	[pkProductTransactionalId] [int] IDENTITY(1,1) NOT NULL,
	[fkRiskGroupId] [int] NOT NULL,
	[fkLegalEntityId] [int] NOT NULL,
	[fkLegalEntityAccountId] [int] NOT NULL,
	[fkTableNumberId] [int] NOT NULL,
	[fkTransactionTypeId] [int] NOT NULL,
	[Volume] [decimal](18, 2) NULL,
	[Value] [decimal](18, 2) NULL,
	[LoadedPrice] [varchar](50) NULL,
 CONSTRAINT [PK_tblProductTransactional] PRIMARY KEY CLUSTERED 
(
	[pkProductTransactionalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblProductTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblProductTransactional_rtblTableNumber] FOREIGN KEY([fkTableNumberId])
REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId])
GO

ALTER TABLE [dbo].[tblProductTransactional] CHECK CONSTRAINT [FK_tblProductTransactional_rtblTableNumber]
GO

ALTER TABLE [dbo].[tblProductTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblProductTransactional_rtblTransactionType] FOREIGN KEY([fkTransactionTypeId])
REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId])
GO

ALTER TABLE [dbo].[tblProductTransactional] CHECK CONSTRAINT [FK_tblProductTransactional_rtblTransactionType]
GO

ALTER TABLE [dbo].[tblProductTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblProductTransactional_tblLegalEntity] FOREIGN KEY([fkLegalEntityId])
REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId])
GO

ALTER TABLE [dbo].[tblProductTransactional] CHECK CONSTRAINT [FK_tblProductTransactional_tblLegalEntity]
GO

ALTER TABLE [dbo].[tblProductTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblProductTransactional_tblLegalEntityAccount] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblProductTransactional] CHECK CONSTRAINT [FK_tblProductTransactional_tblLegalEntityAccount]
GO

ALTER TABLE [dbo].[tblProductTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblProductTransactional_tblRiskGroup] FOREIGN KEY([fkRiskGroupId])
REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
GO

ALTER TABLE [dbo].[tblProductTransactional] CHECK CONSTRAINT [FK_tblProductTransactional_tblRiskGroup]
GO


CREATE TABLE [dbo].[tblFinancialTransactional](
	[pkFinancialTransactionalId] [int] IDENTITY(1,1) NOT NULL,
	[fkRiskGroupId] [int] NOT NULL,
	[TotalNumberOfAccounts] [decimal](18, 2) NOT NULL,
	[AverageAccountManagementFee] [decimal](18, 2) NOT NULL,
	[AvergageMinimumMonthlyFee] [decimal](18, 2) NOT NULL,
	[TotalChequeIssuingVolumes] [decimal](18, 2) NOT NULL,
	[TotalChequeDepositVolumes] [decimal](18, 2) NOT NULL,
	[TotalChequeEncashmentVolumes] [decimal](18, 2) NOT NULL,
	[TotalChequeEncashmentValues] [decimal](18, 2) NOT NULL,
	[TotalCashWithdrawalVolumes] [decimal](18, 2) NOT NULL,
	[TotalCashWithdrawalValues] [decimal](18, 2) NOT NULL,
	[AvergageChequeIssuingValue] [decimal](18, 2) NOT NULL,
	[AverageChequeIssuingPrice] [decimal](18, 2) NOT NULL,
	[AverageChequeDepositValue] [decimal](18, 2) NOT NULL,
	[AverageChequeDepositPrice] [decimal](18, 2) NOT NULL,
	[AverageChequeEncashmentPrice] [decimal](18, 2) NOT NULL,
	[AverageCashWithdrawalPrice] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_tblFinancialTransactional] PRIMARY KEY CLUSTERED 
(
	[pkFinancialTransactionalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblFinancialTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblFinancialTransactional_tblRiskGroup] FOREIGN KEY([fkRiskGroupId])
REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
GO

ALTER TABLE [dbo].[tblFinancialTransactional] CHECK CONSTRAINT [FK_tblFinancialTransactional_tblRiskGroup]
GO





