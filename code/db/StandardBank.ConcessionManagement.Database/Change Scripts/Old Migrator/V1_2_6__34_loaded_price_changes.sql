CREATE TABLE [dbo].[tblLoadedPriceCash](
	[pkLoadedPriceCashId] [int] IDENTITY(1,1) NOT NULL,
	[fkChannelTypeId] [int] NOT NULL,
	[fkLegalEntityAccountId] [int] NOT NULL,
	[fkTableNumberId] [int] NOT NULL,
 CONSTRAINT [PK_tblLoadedPriceCash] PRIMARY KEY CLUSTERED 
(
	[pkLoadedPriceCashId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblLoadedPriceCash]  WITH CHECK ADD  CONSTRAINT [FK_tblLoadedPriceCash_rtblChannelType] FOREIGN KEY([fkChannelTypeId])
REFERENCES [dbo].[rtblChannelType] ([pkChannelTypeId])
GO

ALTER TABLE [dbo].[tblLoadedPriceCash] CHECK CONSTRAINT [FK_tblLoadedPriceCash_rtblChannelType]
GO

ALTER TABLE [dbo].[tblLoadedPriceCash]  WITH CHECK ADD  CONSTRAINT [FK_tblLoadedPriceCash_rtblTableNumber] FOREIGN KEY([fkTableNumberId])
REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId])
GO

ALTER TABLE [dbo].[tblLoadedPriceCash] CHECK CONSTRAINT [FK_tblLoadedPriceCash_rtblTableNumber]
GO

ALTER TABLE [dbo].[tblLoadedPriceCash]  WITH CHECK ADD  CONSTRAINT [FK_tblLoadedPriceCash_tblLegalEntityAccount] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblLoadedPriceCash] CHECK CONSTRAINT [FK_tblLoadedPriceCash_tblLegalEntityAccount]
GO


INSERT INTO [dbo].[tblLoadedPriceCash] ([fkChannelTypeId], [fkLegalEntityAccountId], [fkTableNumberId])
SELECT ct.[pkChannelTypeId], lea.[pkLegalEntityAccountId], (SELECT TOP 1 [pkTableNumberId] FROM [dbo].[rtblTableNumber] WHERE [fkConcessionTypeId] = 7) FROM [dbo].[rtblChannelType] ct, [dbo].[tblLegalEntityAccount] lea

GO

CREATE TABLE [dbo].[tblLoadedPriceLending](
	[pkLoadedPriceLendingId] [int] IDENTITY(1,1) NOT NULL,
	[fkProductTypeId] [int] NOT NULL,
	[fkLegalEntityAccountId] [int] NOT NULL,
	[MarginToPrime] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_tblLoadedPriceLending] PRIMARY KEY CLUSTERED 
(
	[pkLoadedPriceLendingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblLoadedPriceLending]  WITH CHECK ADD  CONSTRAINT [FK_tblLoadedPriceLending_rtblProduct] FOREIGN KEY([fkProductTypeId])
REFERENCES [dbo].[rtblProduct] ([pkProductId])
GO

ALTER TABLE [dbo].[tblLoadedPriceLending] CHECK CONSTRAINT [FK_tblLoadedPriceLending_rtblProduct]
GO

ALTER TABLE [dbo].[tblLoadedPriceLending]  WITH CHECK ADD  CONSTRAINT [FK_tblLoadedPriceLending_tblLegalEntityAccount] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblLoadedPriceLending] CHECK CONSTRAINT [FK_tblLoadedPriceLending_tblLegalEntityAccount]
GO


INSERT INTO [dbo].[tblLoadedPriceLending] ([fkProductTypeId], [fkLegalEntityAccountId], [MarginToPrime])
SELECT p.[pkProductId], lea.[pkLegalEntityAccountId], 100 FROM [dbo].[rtblProduct] p, [dbo].[tblLegalEntityAccount] lea

GO

CREATE TABLE [dbo].[tblLoadedPriceTransactional](
	[pkLoadedPriceTransactionalId] [int] IDENTITY(1,1) NOT NULL,
	[fkTransactionTypeId] [int] NOT NULL,
	[fkLegalEntityAccountId] [int] NOT NULL,
	[fkTableNumberId] [int] NOT NULL,
 CONSTRAINT [PK_tblLoadedPriceTransactional] PRIMARY KEY CLUSTERED 
(
	[pkLoadedPriceTransactionalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblLoadedPriceTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblLoadedPriceTransactional_rtblTableNumber] FOREIGN KEY([fkTableNumberId])
REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId])
GO

ALTER TABLE [dbo].[tblLoadedPriceTransactional] CHECK CONSTRAINT [FK_tblLoadedPriceTransactional_rtblTableNumber]
GO

ALTER TABLE [dbo].[tblLoadedPriceTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblLoadedPriceTransactional_rtblTransactionType] FOREIGN KEY([fkTransactionTypeId])
REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId])
GO

ALTER TABLE [dbo].[tblLoadedPriceTransactional] CHECK CONSTRAINT [FK_tblLoadedPriceTransactional_rtblTransactionType]
GO

ALTER TABLE [dbo].[tblLoadedPriceTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblLoadedPriceTransactional_tblLegalEntityAccount] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblLoadedPriceTransactional] CHECK CONSTRAINT [FK_tblLoadedPriceTransactional_tblLegalEntityAccount]
GO

INSERT INTO [dbo].[rtblTableNumber] ([TariffTable], [AdValorem], [BaseRate], [IsActive], [fkConcessionTypeId]) VALUES (999, 9.99, 0.99, 1, 5)

GO

INSERT INTO [dbo].[tblLoadedPriceTransactional] ([fkTransactionTypeId], [fkLegalEntityAccountId], [fkTableNumberId])
SELECT tt.[pkTransactionTypeId], lea.[pkLegalEntityAccountId], (SELECT TOP 1 [pkTableNumberId] FROM [dbo].[rtblTableNumber] WHERE [fkConcessionTypeId] = 5) FROM [dbo].[rtblTransactionType] tt, [dbo].[tblLegalEntityAccount] lea

GO