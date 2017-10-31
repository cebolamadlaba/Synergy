
GO


GO


GO


GO


GO


GO


GO


INSERT INTO [dbo].[tblLoadedPriceCash] ([fkChannelTypeId], [fkLegalEntityAccountId], [fkTableNumberId])
SELECT ct.[pkChannelTypeId], lea.[pkLegalEntityAccountId], (SELECT TOP 1 [pkTableNumberId] FROM [dbo].[rtblTableNumber] WHERE [fkConcessionTypeId] = 7) FROM [dbo].[rtblChannelType] ct, [dbo].[tblLegalEntityAccount] lea

GO


GO


GO


GO


GO


GO


INSERT INTO [dbo].[tblLoadedPriceLending] ([fkProductTypeId], [fkLegalEntityAccountId], [MarginToPrime])
SELECT p.[pkProductId], lea.[pkLegalEntityAccountId], 100 FROM [dbo].[rtblProduct] p, [dbo].[tblLegalEntityAccount] lea

GO


GO


GO


GO


GO


GO


GO


GO

INSERT INTO [dbo].[rtblTableNumber] ([TariffTable], [AdValorem], [BaseRate], [IsActive], [fkConcessionTypeId]) VALUES (999, 9.99, 0.99, 1, 5)

GO

INSERT INTO [dbo].[tblLoadedPriceTransactional] ([fkTransactionTypeId], [fkLegalEntityAccountId], [fkTableNumberId])
SELECT tt.[pkTransactionTypeId], lea.[pkLegalEntityAccountId], (SELECT TOP 1 [pkTableNumberId] FROM [dbo].[rtblTableNumber] WHERE [fkConcessionTypeId] = 5) FROM [dbo].[rtblTransactionType] tt, [dbo].[tblLegalEntityAccount] lea

GO