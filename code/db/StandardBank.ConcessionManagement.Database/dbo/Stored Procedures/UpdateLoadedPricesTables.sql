CREATE PROCEDURE [dbo].[UpdateLoadedPricesTables]
AS

BEGIN
	SET NOCOUNT ON;

	-- update the loaded prices for cash
	TRUNCATE TABLE [dbo].[tblLoadedPriceCash]

	INSERT INTO [dbo].[tblLoadedPriceCash] ([fkChannelTypeId], [fkLegalEntityAccountId], [fkTableNumberId])
	SELECT cti.[fkChannelTypeId], lea.[pkLegalEntityAccountId], tn.[pkTableNumberId] FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblChannelTypeImport] cti ON cti.[ImportFileChannel] = sdi.[Channel]
	JOIN [dbo].[tblLegalEntityAccount] lea ON lea.[AccountNumber] = sdi.[AccountNo]
	JOIN [dbo].[rtblTableNumber] tn ON tn.[TariffTable] = sdi.[TableNo] 
		AND (tn.[AdValorem] = CAST(sdi.[AdvaloremFee] AS decimal(18,2)) OR (tn.[AdValorem] IS NULL AND sdi.[AdvaloremFee] IS NULL))
		AND (tn.[BaseRate] = CAST(sdi.[FlatFee] AS decimal(18,3)) OR (tn.[BaseRate] IS NULL AND sdi.[FlatFee] IS NULL))

	-- update the loaded prices for lending
	TRUNCATE TABLE [dbo].[tblLoadedPriceLending]

	INSERT INTO [dbo].[tblLoadedPriceLending] ([fkProductTypeId], [fkLegalEntityAccountId], [MarginToPrime])
	SELECT rpi.[fkProductId], lea.[pkLegalEntityAccountId], CAST([FlatFee] AS decimal(18,2)) FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblProductImport] rpi on rpi.[ImportFileChannel] = sdi.[Channel]
	JOIN [dbo].[tblLegalEntityAccount] lea ON lea.[AccountNumber] = sdi.[AccountNo]

	-- update the loaded prices for transactional
	TRUNCATE TABLE [dbo].[tblLoadedPriceTransactional]

	INSERT INTO [dbo].[tblLoadedPriceTransactional] ([fkTransactionTypeId], [fkLegalEntityAccountId], [fkTransactionTableNumberId])
	SELECT tti.[fkTransactionTypeId], lea.[pkLegalEntityAccountId], tn.[pkTransactionTableNumberId] FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblTransactionTypeImport] tti on tti.[ImportFileChannel] = sdi.[Channel]
	JOIN [dbo].[tblLegalEntityAccount] lea ON lea.[AccountNumber] = sdi.[AccountNo]
	JOIN [dbo].[rtblTransactionTableNumber] tn ON tn.[TariffTable] = sdi.[TableNo] 
		AND (tn.[AdValorem] = CAST(sdi.[AdvaloremFee] AS decimal(18,2)) OR (tn.[AdValorem] IS NULL AND sdi.[AdvaloremFee] IS NULL))
		AND (tn.[Fee] = CAST(sdi.[FlatFee] AS decimal(18,3)) OR (tn.[Fee] IS NULL AND sdi.[FlatFee] IS NULL))
END