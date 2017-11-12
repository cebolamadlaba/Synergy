CREATE PROCEDURE [dbo].[UpdateLoadedPrices]
	@AccountNo varchar(50),
	@ChannelType varchar(50),
	@FlatFee varchar(50),
	@TableNo varchar(50)
AS

BEGIN
	
	SET NOCOUNT ON;

	-- update or insert any cash loaded prices
	MERGE [dbo].[tblLoadedPriceCash] AS TARGET
	USING (
	SELECT cti.[fkChannelTypeId], lea.[pkLegalEntityAccountId], tn.[pkTableNumberId] FROM [dbo].[rtblChannelTypeImport] cti
	JOIN [dbo].[tblLegalEntityAccount] lea on lea.[AccountNumber] = @AccountNo
	JOIN [dbo].[rtblTableNumber] tn on tn.[TariffTable] = @TableNo
	JOIN [dbo].[rtblConcessionType] ct on ct.[pkConcessionTypeId] = tn.[fkConcessionTypeId]
	WHERE cti.[ImportFileChannel] = @ChannelType
	AND ct.[Code] = 'Cash') AS SOURCE
	ON TARGET.[fkChannelTypeId] = SOURCE.[fkChannelTypeId] AND TARGET.[fkLegalEntityAccountId] = SOURCE.[pkLegalEntityAccountId]
	WHEN MATCHED THEN
	UPDATE SET [fkTableNumberId] = SOURCE.[pkTableNumberId]
	WHEN NOT MATCHED THEN
	INSERT ([fkChannelTypeId], [fkLegalEntityAccountId], [fkTableNumberId]) VALUES 
	(SOURCE.[fkChannelTypeId], SOURCE.[pkLegalEntityAccountId], SOURCE.[pkTableNumberId]);

	-- update or insert any lending loaded prices
	MERGE [dbo].[tblLoadedPriceLending] AS TARGET
	USING (
	SELECT rpi.[fkProductId], lea.[pkLegalEntityAccountId], @FlatFee [MarginToPrime] FROM [dbo].[rtblProductImport] rpi
	JOIN [dbo].[tblLegalEntityAccount] lea on lea.[AccountNumber] = @AccountNo
	WHERE rpi.[ImportFileChannel] = @ChannelType) AS SOURCE
	ON TARGET.[fkProductTypeId] = SOURCE.[fkProductId] AND TARGET.[fkLegalEntityAccountId] = SOURCE.[pkLegalEntityAccountId]
	WHEN MATCHED THEN
	UPDATE SET [MarginToPrime] = SOURCE.[MarginToPrime]
	WHEN NOT MATCHED THEN
	INSERT ([fkProductTypeId], [fkLegalEntityAccountId], [MarginToPrime])
	VALUES (SOURCE.[fkProductId], SOURCE.[pkLegalEntityAccountId], SOURCE.[MarginToPrime]);

	-- update or insert any transactional loaded prices
	MERGE [dbo].[tblLoadedPriceTransactional] AS TARGET
	USING (
	SELECT tti.[fkTransactionTypeId], lea.[pkLegalEntityAccountId], ttn.[pkTransactionTableNumberId] FROM [dbo].[rtblTransactionTypeImport] tti
	JOIN [dbo].[tblLegalEntityAccount] lea on lea.[AccountNumber] = @AccountNo
	JOIN [dbo].[rtblTransactionTableNumber] ttn on ttn.[fkTransactionTypeId] = tti.[fkTransactionTypeId] AND ttn.[TariffTable] = @TableNo
	WHERE tti.[ImportFileChannel] = @ChannelType) AS SOURCE
	ON TARGET.[fkTransactionTypeId] = SOURCE.[fkTransactionTypeId] AND TARGET.[fkLegalEntityAccountId] = SOURCE.[pkLegalEntityAccountId]
	WHEN MATCHED THEN
	UPDATE SET [fkTransactionTableNumberId] = SOURCE.[pkTransactionTableNumberId]
	WHEN NOT MATCHED THEN
	INSERT ([fkTransactionTypeId], [fkLegalEntityAccountId], [fkTransactionTableNumberId])
	VALUES (SOURCE.[fkTransactionTypeId], SOURCE.[pkLegalEntityAccountId], SOURCE.[pkTransactionTableNumberId]);

END