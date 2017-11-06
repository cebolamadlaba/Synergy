CREATE PROCEDURE [dbo].[UpdatePricesAndMismatches]
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

	-- update the loaded prices for cash concessions
	UPDATE cc
	SET cc.[fkLoadedTableNumberId] = lpc.[fkTableNumberId] 
	FROM [dbo].[tblConcessionDetail] cd
	JOIN [dbo].[tblConcessionCash] cc ON cc.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
	JOIN [dbo].[tblConcession] c ON c.[pkConcessionId] = cd.[fkConcessionId]
	LEFT JOIN [dbo].[tblLoadedPriceCash] lpc on lpc.[fkChannelTypeId] = cc.[fkChannelTypeId] and lpc.[fkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1

	-- update the is mismatched flags for cash
	UPDATE cd
	SET cd.[IsMismatched] = CASE WHEN cc.[fkTableNumberId] = cc.[fkLoadedTableNumberId] THEN 0 ELSE 1 END
	FROM [dbo].[tblConcessionDetail] cd
	JOIN [dbo].[tblConcessionCash] cc ON cc.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
	JOIN [dbo].[tblConcession] c ON c.[pkConcessionId] = cd.[fkConcessionId]
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1

	-- update the loaded prices for lending
	TRUNCATE TABLE [dbo].[tblLoadedPriceLending]

	INSERT INTO [dbo].[tblLoadedPriceLending] ([fkProductTypeId], [fkLegalEntityAccountId], [MarginToPrime])
	SELECT rpi.[fkProductId], lea.[pkLegalEntityAccountId], CAST([FlatFee] AS decimal(18,2)) FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblProductImport] rpi on rpi.[ImportFileChannel] = sdi.[Channel]
	JOIN [dbo].[tblLegalEntityAccount] lea ON lea.[AccountNumber] = sdi.[AccountNo]

	-- update the loaded prices for lending concessions
	UPDATE cl
	SET cl.[LoadedMarginToPrime] = lpl.[MarginToPrime] 
	FROM [dbo].[tblConcessionDetail] cd
	JOIN [dbo].[tblConcessionLending] cl on cl.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
	JOIN [dbo].[tblConcession] c ON c.[pkConcessionId] = cd.[fkConcessionId]
	LEFT JOIN [dbo].[tblLoadedPriceLending] lpl on lpl.[fkProductTypeId] = cl.[fkProductTypeId] and lpl.[fkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1

	-- update the is mismatched flags for lending
	UPDATE cd
	SET cd.[IsMismatched] = CASE WHEN cl.[MarginToPrime] = cl.[LoadedMarginToPrime] THEN 0 ELSE 1 END
	FROM [dbo].[tblConcessionDetail] cd
	JOIN [dbo].[tblConcessionLending] cl on cl.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
	JOIN [dbo].[tblConcession] c ON c.[pkConcessionId] = cd.[fkConcessionId]
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1

	-- update the loaded prices for transactional
	TRUNCATE TABLE [dbo].[tblLoadedPriceTransactional]

	INSERT INTO [dbo].[tblLoadedPriceTransactional] ([fkTransactionTypeId], [fkLegalEntityAccountId], [fkTransactionTableNumberId])
	SELECT tti.[fkTransactionTypeId], lea.[pkLegalEntityAccountId], tn.[pkTransactionTableNumberId] FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblTransactionTypeImport] tti on tti.[ImportFileChannel] = sdi.[Channel]
	JOIN [dbo].[tblLegalEntityAccount] lea ON lea.[AccountNumber] = sdi.[AccountNo]
	JOIN [dbo].[rtblTransactionTableNumber] tn ON tn.[TariffTable] = sdi.[TableNo] 
		AND (tn.[AdValorem] = CAST(sdi.[AdvaloremFee] AS decimal(18,2)) OR (tn.[AdValorem] IS NULL AND sdi.[AdvaloremFee] IS NULL))
		AND (tn.[Fee] = CAST(sdi.[FlatFee] AS decimal(18,3)) OR (tn.[Fee] IS NULL AND sdi.[FlatFee] IS NULL))

	-- update the loaded prices for transactional concessions
	UPDATE ct
	SET ct.[fkLoadedTransactionTableNumberId] = lpt.[fkTransactionTableNumberId]
	FROM [dbo].[tblConcessionDetail] cd
	JOIN [dbo].[tblConcessionTransactional] ct on ct.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
	JOIN [dbo].[tblConcession] c ON c.[pkConcessionId] = cd.[fkConcessionId]
	LEFT JOIN [dbo].[tblLoadedPriceTransactional] lpt on lpt.[fkTransactionTypeId] = ct.[fkTransactionTypeId] and lpt.[fkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1

	-- update the is mismatched flags for transactional
	UPDATE cd
	SET cd.[IsMismatched] = CASE WHEN ct.[fkTransactionTableNumberId] = ct.[fkLoadedTransactionTableNumberId] THEN 0 ELSE 1 END
	FROM [dbo].[tblConcessionDetail] cd
	JOIN [dbo].[tblConcessionTransactional] ct on ct.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
	JOIN [dbo].[tblConcession] c ON c.[pkConcessionId] = cd.[fkConcessionId]
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1

END