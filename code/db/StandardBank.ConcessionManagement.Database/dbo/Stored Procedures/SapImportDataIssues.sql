CREATE PROCEDURE [dbo].[SapImportDataIssues]
AS

BEGIN
	SET NOCOUNT ON;

	-- is the sap import account number in the legal entity account number table
	SELECT 
		'[dbo].[tblSapDataImport].[AccountNo]: ' + [AccountNo] [Column], 
		'Missing from [tblLegalEntityAccount]' [Issue] FROM [dbo].[tblSapDataImport]
	WHERE [AccountNo] NOT IN (	SELECT [AccountNumber] FROM [dbo].[tblLegalEntityAccount])
	
	-- is the legal entity entity account numbers in the sap data import table
	UNION
	SELECT
		 '[dbo].[tblLegalEntityAccount].[AccountNumber]: ' + lea.[AccountNumber] [Column], 
		 'Missing from [dbo].[tblSapDataImport]' [Issue] 
	FROM [dbo].[tblLegalEntityAccount] lea
	WHERE lea.[AccountNumber] NOT IN (
	SELECT [AccountNo] FROM [dbo].[tblSapDataImport])
	
	-- are the channel type import channels in the sap data import table
	UNION
	SELECT 
		'[dbo].[rtblChannelTypeImport].[ImportFileChannel]: ' + cti.[ImportFileChannel] [Column], 
		'Missing from [dbo].[tblSapDataImport]' [Issue] 
	FROM [dbo].[rtblChannelTypeImport] cti
	LEFT JOIN [dbo].[tblSapDataImport] sdi on sdi.[Channel] = cti.[ImportFileChannel]
	WHERE sdi.[PricepointId] IS NULL

	-- are the product import channels in the sap data import table 
	UNION
	SELECT 
		'[dbo].[rtblProductImport].[ImportFileChannel]: ' + rpi.[ImportFileChannel] [Column], 
		'Missing from [dbo].[tblSapDataImport]' [Issue] 
	FROM [dbo].[rtblProductImport] rpi
	LEFT JOIN [dbo].[tblSapDataImport] sdi on sdi.[Channel] = rpi.[ImportFileChannel]
	WHERE sdi.[PricepointId] IS NULL

	-- are the transaction type import channels in the sap data import table
	UNION
	SELECT
		'[dbo].[rtblTransactionTypeImport].[ImportFileChannel]: ' + tti.[ImportFileChannel] [Column], 
		'Missing from [dbo].[tblSapDataImport]' [Issue] 
	FROM [dbo].[rtblTransactionTypeImport] tti
	LEFT JOIN [dbo].[tblSapDataImport] sdi on sdi.[Channel] = tti.[ImportFileChannel]
	WHERE sdi.[PricepointId] IS NULL
	
	-- are the cash specific table numbers in the sap data import table
	UNION
	SELECT
		'Cash Specific [dbo].[rtblTableNumber].[TariffTable]: ' + CAST([TariffTable] AS VARCHAR(50)) [Column],
		'Missing from [dbo].[tblSapDataImport]' [Issue] 
	FROM [dbo].[rtblTableNumber]
	WHERE [fkConcessionTypeId] = (
	SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
	WHERE [Code] = 'Cash')
	AND [TariffTable] NOT IN (
	SELECT sdi.[TableNo] FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblChannelTypeImport] cti on cti.[ImportFileChannel] = sdi.[Channel])

	-- are there cash specific table numbers in the sap data import table that aren't in the table numbers lookup table for cash
	UNION
	SELECT
		'[dbo].[tblSapDataImport].[TableNo]: ' + [TableNo] [Column], 
		'Missing from [rtblTableNumber] for Cash Concession Type' [Issue]
	FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblChannelTypeImport] cti on cti.[ImportFileChannel] = sdi.[Channel]
	WHERE sdi.[TableNo] NOT IN (
	SELECT CAST([TariffTable] AS VARCHAR(50))
	FROM [dbo].[rtblTableNumber]
	WHERE [fkConcessionTypeId] = (
	SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
	WHERE [Code] = 'Cash'))

	-- are the transactional specific table numbers in the sap data import table
	UNION
		SELECT
		'[dbo].[rtblTransactionTableNumber].[TariffTable]: ' + CAST([TariffTable] AS VARCHAR(50)) [Column],
		'Missing from [dbo].[tblSapDataImport]' [Issue] 
	FROM [dbo].[rtblTransactionTableNumber]
	WHERE [TariffTable] NOT IN (
	SELECT sdi.[TableNo] FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblTransactionTypeImport] tti on tti.[ImportFileChannel] = sdi.[Channel])

	-- are there transactional specific table numbers in the sap data import table that aren't in the table numbers lookup table for transactional 
	UNION
	SELECT
		'[dbo].[tblSapDataImport].[TableNo]: ' + [TableNo] [Column], 
		'Missing from [rtblTransactionTableNumber]' [Issue]
	FROM [dbo].[tblSapDataImport] sdi
	JOIN [dbo].[rtblTransactionTypeImport] tti on tti.[ImportFileChannel] = sdi.[Channel]
	WHERE sdi.[TableNo] NOT IN (
	SELECT CAST([TariffTable] AS VARCHAR(50))
	FROM [dbo].[rtblTransactionTableNumber])

END