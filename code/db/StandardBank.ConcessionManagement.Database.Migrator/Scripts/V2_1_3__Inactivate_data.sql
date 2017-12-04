UPDATE [dbo].[rtblProduct]
SET [IsActive] = 0
WHERE [fkConcessionTypeId] in (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Lending')
AND [Description] in ('Commercial Property Finance',
'Debtors’ Finance',
'VAF Instalment sale',
'VAF Full maintenance lease',
'VAF Operating Rental')

GO

UPDATE [dbo].[rtblChannelType]
SET [IsActive] = 0
WHERE [Description] = 'Bulk Teller'

GO

UPDATE [dbo].[rtblTransactionType]
SET [IsActive] = 0
WHERE [fkConcessionTypeId] IN (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] in ('Monthly Management Fee',
'Automatic Cheque Clearance')

GO

DELETE FROM [dbo].[tblSapDataImportConfiguration]

GO

