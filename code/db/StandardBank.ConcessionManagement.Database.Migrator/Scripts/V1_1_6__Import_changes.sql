ALTER VIEW [dbo].[ConcessionInboxView]
AS
SELECT        c.pkConcessionId AS ConcessionId, rg.pkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, le.pkLegalEntityId AS LegalEntityId, le.CustomerName, lea.pkLegalEntityAccountId AS LegalEntityAccountId, 
                         lea.AccountNumber, ct.pkConcessionTypeId AS ConcessionTypeId, ct.Description AS ConcessionType, c.ConcessionDate, s.pkStatusId AS StatusId, s.Description AS Status, ss.pkSubStatusId AS SubStatusId, 
                         ss.Description AS SubStatus, c.ConcessionRef, ms.pkMarketSegmentId AS MarketSegmentId, ms.Description AS Segment, c.DatesentForApproval, cd.pkConcessionDetailId AS ConcessionDetailId, cd.ExpiryDate, 
                         cd.DateApproved, c.fkRequestorId AS RequestorId, c.fkBCMUserId AS BCMUserId, c.fkPCMUserId AS PCMUserId, c.fkHOUserId AS HOUserId, ce.pkCentreId AS CentreId, ce.CentreName, p.pkProvinceId AS ProvinceId, 
                         p.Description AS Province, cd.IsMismatched, c.IsActive, c.IsCurrent, cd.PriceExported, cd.PriceExportedDate
FROM            dbo.tblConcession AS c INNER JOIN
                         dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId INNER JOIN
                         dbo.rtblConcessionType AS ct ON ct.pkConcessionTypeId = c.fkConcessionTypeId INNER JOIN
                         dbo.tblConcessionDetail AS cd ON cd.fkConcessionId = c.pkConcessionId INNER JOIN
                         dbo.tblLegalEntity AS le ON le.pkLegalEntityId = cd.fkLegalEntityId INNER JOIN
                         dbo.tblLegalEntityAccount AS lea ON lea.pkLegalEntityAccountId = cd.fkLegalEntityAccountId INNER JOIN
                         dbo.rtblStatus AS s ON s.pkStatusId = c.fkStatusId INNER JOIN
                         dbo.rtblSubStatus AS ss ON ss.pkSubStatusId = c.fkSubStatusId INNER JOIN
                         dbo.rtblMarketSegment AS ms ON ms.pkMarketSegmentId = rg.fkMarketSegmentId INNER JOIN
                         dbo.tblCentre AS ce ON ce.pkCentreId = c.fkCentreId INNER JOIN
                         dbo.rtblProvince AS p ON p.pkProvinceId = ce.fkProvinceId
GO

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

GO

CREATE PROCEDURE [dbo].[UpdateMismatches]
AS

BEGIN
	SET NOCOUNT ON;

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

