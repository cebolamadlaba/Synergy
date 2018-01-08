ALTER PROCEDURE [dbo].[UpdatePricesAndMismatches]
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
		--AND (tn.[AdValorem] = CAST(sdi.[AdvaloremFee] AS decimal(18,2)) OR (tn.[AdValorem] IS NULL AND sdi.[AdvaloremFee] IS NULL))
		--AND (tn.[BaseRate] = CAST(sdi.[FlatFee] AS decimal(18,3)) OR (tn.[BaseRate] IS NULL AND sdi.[FlatFee] IS NULL))

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
		--AND (tn.[AdValorem] = CAST(sdi.[AdvaloremFee] AS decimal(18,2)) OR (tn.[AdValorem] IS NULL AND sdi.[AdvaloremFee] IS NULL))
		--AND (tn.[Fee] = CAST(sdi.[FlatFee] AS decimal(18,3)) OR (tn.[Fee] IS NULL AND sdi.[FlatFee] IS NULL))

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

GO

ALTER PROCEDURE [dbo].[GenerateSapExport]
AS

BEGIN
	SET NOCOUNT ON;

	-- update all the transactional records that have been approved today for export
	UPDATE sdi
	SET sdi.[ExportRow] = 1, 
	sdi.[TableNo] = CAST(ttn.[TariffTable] AS VARCHAR(50)), 
	sdi.[AdvaloremFee] = CAST(ttn.[AdValorem] AS VARCHAR(50)),
	sdi.[FlatFee] = CAST(ttn.[Fee] AS VARCHAR(50))
	FROM [dbo].[tblConcessionTransactional] ct
	JOIN [dbo].[rtblTransactionTypeImport] tti on tti.[fkTransactionTypeId] = ct.[fkTransactionTypeId]
	JOIN [dbo].[tblConcessionDetail] cd on cd.[pkConcessionDetailId] = ct.[fkConcessionDetailId]
	JOIN [dbo].[tblConcession] c on c.[pkConcessionId] = cd.[fkConcessionId]
	JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	JOIN [dbo].[rtblTransactionTableNumber] ttn on ttn.[pkTransactionTableNumberId] = ct.[fkApprovedTransactionTableNumberId]
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = tti.[ImportFileChannel] 
		AND CAST(sdi.[AccountNo] AS bigint) = CAST(lea.[AccountNumber] AS bigint)
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1
	AND cd.[PriceExported] = 0

	-- update the transactional records as exported
	UPDATE cd
	SET cd.[PriceExported] = 1, 
	cd.[PriceExportedDate] = GETDATE()
	FROM [dbo].[tblConcessionTransactional] ct
	JOIN [dbo].[rtblTransactionTypeImport] tti on tti.[fkTransactionTypeId] = ct.[fkTransactionTypeId]
	JOIN [dbo].[tblConcessionDetail] cd on cd.[pkConcessionDetailId] = ct.[fkConcessionDetailId]
	JOIN [dbo].[tblConcession] c on c.[pkConcessionId] = cd.[fkConcessionId]
	JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	JOIN [dbo].[rtblTransactionTableNumber] ttn on ttn.[pkTransactionTableNumberId] = ct.[fkApprovedTransactionTableNumberId]
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = tti.[ImportFileChannel] 
		AND CAST(sdi.[AccountNo] AS bigint) = CAST(lea.[AccountNumber] AS bigint)
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1
	AND cd.[PriceExported] = 0

	-- update all the cash records that have been approved today for export
	UPDATE sdi
	SET sdi.[ExportRow] = 1, 
	sdi.[TableNo] = CAST(tn.[TariffTable] AS VARCHAR(50)), 
	sdi.[AdvaloremFee] = CAST(tn.[AdValorem] AS VARCHAR(50)),
	sdi.[FlatFee] = CAST(tn.[BaseRate] AS VARCHAR(50))
	FROM [dbo].[tblConcessionCash] cc
	JOIN [dbo].[rtblChannelTypeImport] cti on cti.[fkChannelTypeId] = cc.[fkChannelTypeId]
	JOIN [dbo].[tblConcessionDetail] cd on cd.[pkConcessionDetailId] = cc.[fkConcessionDetailId]
	JOIN [dbo].[tblConcession] c on c.[pkConcessionId] = cd.[fkConcessionId]
	JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	JOIN [dbo].[rtblTableNumber] tn on tn.[pkTableNumberId] = cc.[fkApprovedTableNumberId]
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = cti.[ImportFileChannel] 
		AND CAST(sdi.[AccountNo] AS bigint) = CAST(lea.[AccountNumber] AS bigint)
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1
	AND cd.[PriceExported] = 0

	-- update the cash records as exported
	UPDATE cd
	SET cd.[PriceExported] = 1, 
	cd.[PriceExportedDate] = GETDATE()
	FROM [dbo].[tblConcessionCash] cc
	JOIN [dbo].[rtblChannelTypeImport] cti on cti.[fkChannelTypeId] = cc.[fkChannelTypeId]
	JOIN [dbo].[tblConcessionDetail] cd on cd.[pkConcessionDetailId] = cc.[fkConcessionDetailId]
	JOIN [dbo].[tblConcession] c on c.[pkConcessionId] = cd.[fkConcessionId]
	JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	JOIN [dbo].[rtblTableNumber] tn on tn.[pkTableNumberId] = cc.[fkApprovedTableNumberId]
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = cti.[ImportFileChannel] 
		AND CAST(sdi.[AccountNo] AS bigint) = CAST(lea.[AccountNumber] AS bigint)
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1
	AND cd.[PriceExported] = 0

	-- update all the lending records that have been approved today for export
	UPDATE sdi
	SET sdi.[ExportRow] = 1,
	sdi.[FlatFee] = CAST(cl.[ApprovedMarginToPrime] AS VARCHAR(50))
	FROM [dbo].[tblConcessionLending] cl
	JOIN [dbo].[rtblProductImport] rpi on rpi.[fkProductId] = cl.[fkProductTypeId] 
	JOIN [dbo].[tblConcessionDetail] cd on cd.[pkConcessionDetailId] = cl.[fkConcessionDetailId]
	JOIN [dbo].[tblConcession] c on c.[pkConcessionId] = cd.[fkConcessionId]
	JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = rpi.[ImportFileChannel] 
		AND CAST(sdi.[AccountNo] AS bigint) = CAST(lea.[AccountNumber] AS bigint)
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1
	AND cd.[PriceExported] = 0

	-- update the lending records as exported
	UPDATE cd
	SET cd.[PriceExported] = 1, 
	cd.[PriceExportedDate] = GETDATE()
	FROM [dbo].[tblConcessionLending] cl
	JOIN [dbo].[rtblProductImport] rpi on rpi.[fkProductId] = cl.[fkProductTypeId] 
	JOIN [dbo].[tblConcessionDetail] cd on cd.[pkConcessionDetailId] = cl.[fkConcessionDetailId]
	JOIN [dbo].[tblConcession] c on c.[pkConcessionId] = cd.[fkConcessionId]
	JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = rpi.[ImportFileChannel] 
		AND CAST(sdi.[AccountNo] AS bigint) = CAST(lea.[AccountNumber] AS bigint)
	WHERE c.[fkStatusId] IN (2, 3)
	AND c.[IsActive] = 1
	AND c.[IsCurrent] = 1
	AND cd.[PriceExported] = 0

	-- return all the data that needs to be exported
	SELECT [PricepointId], [CustomerId], [AccountName], [ProductId], [Description], [GroupId], [SubGroupId], [BankIdentifierId], [AccountNo], [OptionId], [UserId], [TierFromValue], [TierToValue], [AdvaloremFee], [MinimumFee], [MaximumFee], [FlatFee], [CommunicationFee], [TableNo], [TransactionVolume], [TransactionRevenue], [ProductName], [Channel], [MarketSegment], [SequenceId], [EntryDate], [EffectiveDate], [ExpiryDate], [TerminationDate], [Status], [ImportDate], [LastUpdatedDate], [ExportRow] 
	FROM [dbo].[tblSapDataImport] 
	WHERE [ExportRow] = 1

END

GO

ALTER PROCEDURE [dbo].[UpdateLoadedPricesTables]
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
		--AND (tn.[AdValorem] = CAST(sdi.[AdvaloremFee] AS decimal(18,2)) OR (tn.[AdValorem] IS NULL AND sdi.[AdvaloremFee] IS NULL))
		--AND (tn.[BaseRate] = CAST(sdi.[FlatFee] AS decimal(18,3)) OR (tn.[BaseRate] IS NULL AND sdi.[FlatFee] IS NULL))

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
		--AND (tn.[AdValorem] = CAST(sdi.[AdvaloremFee] AS decimal(18,2)) OR (tn.[AdValorem] IS NULL AND sdi.[AdvaloremFee] IS NULL))
		--AND (tn.[Fee] = CAST(sdi.[FlatFee] AS decimal(18,3)) OR (tn.[Fee] IS NULL AND sdi.[FlatFee] IS NULL))
END

GO

ALTER PROCEDURE [dbo].[DeleteConcessionData]
	@AreYouSure int
AS

BEGIN

	IF (@AreYouSure = 1)
	BEGIN
		DELETE FROM [dbo].[tblConcessionApproval]

		DELETE FROM [dbo].[tblConcessionAccount]

		DELETE FROM [dbo].[tblConcessionCondition]

		DELETE FROM [dbo].[tblConcessionRelationship]

		DELETE FROM [dbo].[tblConcessionComment]

		DELETE FROM [dbo].[tblConcessionBol]

		DELETE FROM [dbo].[tblConcessionCash] 

		DELETE FROM [dbo].[tblConcessionInvestment]

		DELETE FROM [dbo].[tblConcessionLending]

		DELETE FROM [dbo].[tblConcessionMas]

		DELETE FROM [dbo].[tblConcessionTrade]

		DELETE FROM [dbo].[tblConcessionTransactional]

		DELETE FROM [dbo].[tblConcessionDetail]

		DELETE FROM [dbo].[tblConcession]

	END

END

GO

