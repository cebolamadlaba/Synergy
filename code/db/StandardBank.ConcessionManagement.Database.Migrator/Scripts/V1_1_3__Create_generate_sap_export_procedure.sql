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
	SET sdi.[ExportRow] = 1
	-- TODO: For Lending we still don't know what field(s) to update with the Margin Above Prime
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