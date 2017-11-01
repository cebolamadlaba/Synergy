CREATE PROCEDURE [dbo].[GenerateSapExport]
AS

BEGIN
	SET NOCOUNT ON;

	DECLARE
		@ConcessionType varchar(50),
		@ConcessionDetailId int

	DECLARE approvedConcessionsNotExported CURSOR FOR
	SELECT [ConcessionType], [ConcessionDetailId]
	FROM [dbo].[ConcessionInboxView]
	WHERE [StatusId] IN (2, 3)
	AND [IsActive] = 1
	AND [IsCurrent] = 1
	AND [PriceExported] = 0

	OPEN approvedConcessionsNotExported

	FETCH NEXT FROM approvedConcessionsNotExported INTO @ConcessionType, @ConcessionDetailId

	WHILE @@FETCH_STATUS <> -1
	BEGIN
		IF @ConcessionType = 'Transactional'
		BEGIN

			UPDATE sdi
			SET sdi.[ExportRow] = 1, 
			sdi.[TableNo] = CAST(ttn.[TariffTable] AS VARCHAR(50)), 
			sdi.[AdvaloremFee] = CAST(ttn.[AdValorem] AS VARCHAR(50)),
			sdi.[FlatFee] = CAST(ttn.[Fee] AS VARCHAR(50))
			FROM [dbo].[tblConcessionTransactional] c
			JOIN [dbo].[rtblTransactionType] tt on tt.[pkTransactionTypeId] = c.[fkTransactionTypeId]
			JOIN [dbo].[tblConcessionDetail] cd on cd.[pkConcessionDetailId] = c.[fkConcessionDetailId]
			JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
			JOIN [dbo].[rtblTransactionTableNumber] ttn on ttn.[pkTransactionTableNumberId] = c.[fkApprovedTransactionTableNumberId]
			JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = tt.[ImportFileChannel] AND sdi.[AccountNo] = lea.[AccountNumber]
			WHERE c.[fkConcessionDetailId] = @ConcessionDetailId

		END

		IF @ConcessionType = 'Lending'
		BEGIN
			
			PRINT ' TO DO '
			--UPDATE sdi
			--SET sdi.[ExportRow] = 1, 
			--sdi.[TableNo] = CAST(ttn.[TariffTable] AS VARCHAR(50)), 
			--sdi.[AdvaloremFee] = CAST(ttn.[AdValorem] AS VARCHAR(50)),
			--sdi.[FlatFee] = CAST(ttn.[Fee] AS VARCHAR(50))
			--FROM [dbo].[tblConcessionLending] c
			--JOIN [dbo].[rtblProduct] p on p.[pkProductId] = c.[fkProductTypeId]
			--JOIN [dbo].[tblConcessionDetail] cd on cd.[pkConcessionDetailId] = c.[fkConcessionDetailId]
			--JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
			--JOIN [dbo].[rtblTransactionTableNumber] ttn on ttn.[pkTransactionTableNumberId] = c.[fkApprovedTransactionTableNumberId]
			--JOIN 
			--JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = p.[ImportFileChannel] AND sdi.[AccountNo] = lea.[AccountNumber]
			--WHERE c.[fkConcessionDetailId] = @ConcessionDetailId


		END

		IF @ConcessionType = 'Cash'
		BEGIN

			UPDATE sdi
			SET sdi.[ExportRow] = 1, 
			sdi.[TableNo] = CAST(tn.[TariffTable] AS VARCHAR(50)), 
			sdi.[AdvaloremFee] = CAST(tn.[AdValorem] AS VARCHAR(50)),
			sdi.[FlatFee] = CAST(tn.[BaseRate] AS VARCHAR(50))
			FROM [dbo].[tblConcessionCash] c
			JOIN [dbo].[rtblChannelType] ct on ct.[pkChannelTypeId] = c.[fkChannelTypeId]
			JOIN [dbo].[tblConcessionDetail] cd on cd.[pkConcessionDetailId] = c.[fkConcessionDetailId]
			JOIN [dbo].[tblLegalEntityAccount] lea on lea.[pkLegalEntityAccountId] = cd.[fkLegalEntityAccountId]
			JOIN [dbo].[rtblTableNumber] tn on tn.[pkTableNumberId] = c.[fkApprovedTableNumberId]
			JOIN [dbo].[tblSapDataImport] sdi ON sdi.[Channel] = ct.[ImportFileChannel] AND sdi.[AccountNo] = lea.[AccountNumber]
			WHERE c.[fkConcessionDetailId] = @ConcessionDetailId

		END

		-- mark the concession as exported
		UPDATE [dbo].[tblConcessionDetail]
		SET [PriceExported] = 1, [PriceExportedDate] = GETDATE()
		WHERE [pkConcessionDetailId] = @ConcessionDetailId

		FETCH NEXT FROM approvedConcessionsNotExported INTO @ConcessionType, @ConcessionDetailId
	END

	CLOSE approvedConcessionsNotExported
	DEALLOCATE approvedConcessionsNotExported

	SELECT [PricepointId], [CustomerId], [AccountName], [ProductId], [Description], [GroupId], [SubGroupId], [BankIdentifierId], [AccountNo], [OptionId], [UserId], [TierFromValue], [TierToValue], [AdvaloremFee], [MinimumFee], [MaximumFee], [FlatFee], [CommunicationFee], [TableNo], [TransactionVolume], [TransactionRevenue], [ProductName], [Channel], [MarketSegment], [SequenceId], [EntryDate], [EffectiveDate], [ExpiryDate], [TerminationDate], [Status], [ImportDate], [LastUpdatedDate], [ExportRow] 
	FROM [dbo].[tblSapDataImport] 
	WHERE [ExportRow] = 1

END