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