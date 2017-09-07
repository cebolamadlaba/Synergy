
CREATE TABLE [dbo].[tblFinancialCash](
	[pkFinancialCashId] [int] IDENTITY(1,1) NOT NULL,
	[fkRiskGroupId] [int] NOT NULL,
	[WeightedAverageBranchPrice] [decimal](18, 2) NOT NULL,
	[TotalCashCentrCashTurnover] [decimal](18, 2) NOT NULL,
	[TotalCashCentrCashVolume] [decimal](18, 2) NOT NULL,
	[TotalBranchCashTurnover] [decimal](18, 2) NOT NULL,
	[TotalBranchCashVolume] [decimal](18, 2) NOT NULL,
	[TotalAutosafeCashTurnover] [decimal](18, 2) NOT NULL,
	[TotalAutosafeCashVolume] [decimal](18, 2) NOT NULL,
	[WeightedAverageCCPrice] [decimal](18, 2) NOT NULL,
	[WeightedAverageAFPrice] [decimal](18, 2) NOT NULL,
	[LatestCrsOrMrs] [decimal](18, 2) NOT NULL,
	[LoadedPrice] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_tblFinancialCash] PRIMARY KEY CLUSTERED 
(
	[pkFinancialCashId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblFinancialCash]  WITH CHECK ADD  CONSTRAINT [FK_tblFinancialCash_tblRiskGroup] FOREIGN KEY([fkRiskGroupId])
REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
GO

ALTER TABLE [dbo].[tblFinancialCash] CHECK CONSTRAINT [FK_tblFinancialCash_tblRiskGroup]
GO


CREATE TABLE [dbo].[tblProductCash](
	[pkProductCashId] [int] IDENTITY(1,1) NOT NULL,
	[fkRiskGroupId] [int] NOT NULL,
	[fkLegalEntityId] [int] NOT NULL,
	[fkLegalEntityAccountId] [int] NOT NULL,
	[fkTableNumberId] [int] NOT NULL,
	[Channel] [varchar](150) NOT NULL,
	[BpId] [int] NOT NULL,
	[Volume] [decimal](18, 2) NOT NULL,
	[Value] [decimal](18, 2) NOT NULL,
	[LoadedPrice] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_tblProductCash] PRIMARY KEY CLUSTERED 
(
	[pkProductCashId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblProductCash]  WITH CHECK ADD  CONSTRAINT [FK_tblProductCash_rtblTableNumber] FOREIGN KEY([fkTableNumberId])
REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId])
GO

ALTER TABLE [dbo].[tblProductCash] CHECK CONSTRAINT [FK_tblProductCash_rtblTableNumber]
GO

ALTER TABLE [dbo].[tblProductCash]  WITH CHECK ADD  CONSTRAINT [FK_tblProductCash_tblLegalEntity] FOREIGN KEY([fkLegalEntityId])
REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId])
GO

ALTER TABLE [dbo].[tblProductCash] CHECK CONSTRAINT [FK_tblProductCash_tblLegalEntity]
GO

ALTER TABLE [dbo].[tblProductCash]  WITH CHECK ADD  CONSTRAINT [FK_tblProductCash_tblLegalEntityAccount] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblProductCash] CHECK CONSTRAINT [FK_tblProductCash_tblLegalEntityAccount]
GO

ALTER TABLE [dbo].[tblProductCash]  WITH CHECK ADD  CONSTRAINT [FK_tblProductCash_tblRiskGroup] FOREIGN KEY([fkRiskGroupId])
REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
GO

ALTER TABLE [dbo].[tblProductCash] CHECK CONSTRAINT [FK_tblProductCash_tblRiskGroup]
GO


