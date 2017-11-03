CREATE PROCEDURE [dbo].[GenerateSapExport]
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
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = tti.[ImportFileChannel] AND sdi.[AccountNo] = lea.[AccountNumber]
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
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = tti.[ImportFileChannel] AND sdi.[AccountNo] = lea.[AccountNumber]
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
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = cti.[ImportFileChannel] AND sdi.[AccountNo] = lea.[AccountNumber]
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
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = cti.[ImportFileChannel] AND sdi.[AccountNo] = lea.[AccountNumber]
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
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = rpi.[ImportFileChannel] AND sdi.[AccountNo] = lea.[AccountNumber]
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
	JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = rpi.[ImportFileChannel] AND sdi.[AccountNo] = lea.[AccountNumber]
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

GO

CREATE PROCEDURE [dbo].[SapImportDataIssues]
AS

BEGIN
	SET NOCOUNT ON;

	SELECT 
		'[dbo].[tblSapDataImport].[AccountNo]: ' + [AccountNo] [Column], 
		'Missing from [tblLegalEntityAccount]' [Issue] FROM [dbo].[tblSapDataImport]
	WHERE [AccountNo] NOT IN (	SELECT [AccountNumber] FROM [dbo].[tblLegalEntityAccount])
	UNION
	SELECT 
		'[dbo].[rtblChannelTypeImport].[ImportFileChannel]: ' + cti.[ImportFileChannel] [Column], 
		'Missing from [dbo].[tblSapDataImport]' [Issue] 
	FROM [dbo].[rtblChannelTypeImport] cti
	LEFT JOIN [dbo].[tblSapDataImport] sdi on sdi.[Channel] = cti.[ImportFileChannel]
	WHERE sdi.[PricepointId] IS NULL
	UNION
	SELECT 
		'[dbo].[rtblProductImport].[ImportFileChannel]: ' + rpi.[ImportFileChannel] [Column], 
		'Missing from [dbo].[tblSapDataImport]' [Issue] 
	FROM [dbo].[rtblProductImport] rpi
	LEFT JOIN [dbo].[tblSapDataImport] sdi on sdi.[Channel] = rpi.[ImportFileChannel]
	WHERE sdi.[PricepointId] IS NULL
	UNION
	SELECT
		'[dbo].[rtblTransactionTypeImport].[ImportFileChannel]: ' + tti.[ImportFileChannel] [Column], 
		'Missing from [dbo].[tblSapDataImport]' [Issue] 
	FROM [dbo].[rtblTransactionTypeImport] tti
	LEFT JOIN [dbo].[tblSapDataImport] sdi on sdi.[Channel] = tti.[ImportFileChannel]
	WHERE sdi.[PricepointId] IS NULL
	UNION
	SELECT
		 '[dbo].[tblLegalEntityAccount].[AccountNumber]: ' + lea.[AccountNumber] [Column], 
		 'Missing from [dbo].[tblSapDataImport]' [Issue] 
	FROM [dbo].[tblLegalEntityAccount] lea
	WHERE lea.[AccountNumber] NOT IN (
	SELECT [AccountNo] FROM [dbo].[tblSapDataImport])

END

GO