
CREATE PROCEDURE [dbo].[UpdatePricesAndMismatches]
AS

BEGIN
	SET NOCOUNT ON;

	-- update the loaded prices
	TRUNCATE TABLE [dbo].[tblLoadedPriceCash]

	INSERT INTO [dbo].[tblLoadedPriceCash] ([fkChannelTypeId], [fkLegalEntityAccountId], [fkTableNumberId])
	SELECT cti.[fkChannelTypeId], lea.[pkLegalEntityAccountId], tn.[pkTableNumberId] FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblChannelTypeImport] cti ON cti.[ImportFileChannel] = sdi.[Channel]
	JOIN [dbo].[tblLegalEntityAccount] lea ON lea.[AccountNumber] = sdi.[AccountNo]
	JOIN [dbo].[rtblTableNumber] tn ON tn.[TariffTable] = sdi.[TableNo] AND tn.[AdValorem] = sdi.[AdvaloremFee] AND tn.[BaseRate] = sdi.[FlatFee]

	-- update the is mismatched flags
	UPDATE cd
	SET cd.[IsMismatched] = 
		(CASE WHEN lpc.[pkLoadedPriceCashId] IS NULL THEN 1 ELSE 
			CASE WHEN lpc.[fkTableNumberId] = cc.[fkApprovedTableNumberId] THEN 1 ELSE 0 END
		END)
	FROM [dbo].[tblConcessionDetail] cd
	JOIN [dbo].[tblConcessionCash] cc ON cc.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
	JOIN [dbo].[tblConcession] c ON c.[pkConcessionId] = cd.[fkConcessionId]
	LEFT JOIN [dbo].[tblLoadedPriceCash] lpc on lpc.[fkChannelTypeId] = cc.[fkChannelTypeId] and lpc.[fkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1

	-- update the loaded prices
	TRUNCATE TABLE [dbo].[tblLoadedPriceLending]

	INSERT INTO [dbo].[tblLoadedPriceLending] ([fkProductTypeId], [fkLegalEntityAccountId], [MarginToPrime])
	SELECT rpi.[fkProductId], lea.[pkLegalEntityAccountId], CAST([FlatFee] AS decimal(18,2)) FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblProductImport] rpi on rpi.[ImportFileChannel] = sdi.[Channel]
	JOIN [dbo].[tblLegalEntityAccount] lea ON lea.[AccountNumber] = sdi.[AccountNo]

	-- update the is mismatched flags
	UPDATE cd
	SET cd.[IsMismatched] = 
		(CASE WHEN lpl.[pkLoadedPriceLendingId] IS NULL THEN 1 ELSE 
			CASE WHEN lpl.[MarginToPrime] = cl.[ApprovedMarginToPrime] THEN 1 ELSE 0 END
		END)
	FROM [dbo].[tblConcessionDetail] cd
	JOIN [dbo].[tblConcessionLending] cl on cl.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
	JOIN [dbo].[tblConcession] c ON c.[pkConcessionId] = cd.[fkConcessionId]
	LEFT JOIN [dbo].[tblLoadedPriceLending] lpl on lpl.[fkProductTypeId] = cl.[fkProductTypeId] and lpl.[fkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1

	-- update the loaded prices
	TRUNCATE TABLE [dbo].[tblLoadedPriceTransactional]

	INSERT INTO [dbo].[tblLoadedPriceTransactional] ([fkTransactionTypeId], [fkLegalEntityAccountId], [fkTransactionTableNumberId])
	SELECT tti.[fkTransactionTypeId], lea.[pkLegalEntityAccountId], tn.[pkTableNumberId] FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblTransactionTypeImport] tti on tti.[ImportFileChannel] = sdi.[Channel]
	JOIN [dbo].[tblLegalEntityAccount] lea ON lea.[AccountNumber] = sdi.[AccountNo]
	JOIN [dbo].[rtblTableNumber] tn ON tn.[TariffTable] = sdi.[TableNo] AND tn.[AdValorem] = sdi.[AdvaloremFee] AND tn.[BaseRate] = sdi.[FlatFee]

	-- update the is mismatched flags
	UPDATE cd
	SET cd.[IsMismatched] = 
		(CASE WHEN lpt.[pkLoadedPriceTransactionalId] IS NULL THEN 1 ELSE 
			CASE WHEN lpt.[fkTransactionTableNumberId] = ct.[fkApprovedTransactionTableNumberId] THEN 1 ELSE 0 END
		END)
	FROM [dbo].[tblConcessionDetail] cd
	JOIN [dbo].[tblConcessionTransactional] ct on ct.[fkConcessionDetailId] = cd.[pkConcessionDetailId]
	JOIN [dbo].[tblConcession] c ON c.[pkConcessionId] = cd.[fkConcessionId]
	LEFT JOIN [dbo].[tblLoadedPriceTransactional] lpt on lpt.[fkTransactionTypeId] = ct.[fkTransactionTypeId] and lpt.[fkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1
END