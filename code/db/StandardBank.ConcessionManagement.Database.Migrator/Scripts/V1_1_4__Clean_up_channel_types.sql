UPDATE [dbo].[tblConcessionCash]
SET [fkChannelTypeId] = (SELECT TOP 1 [fkChannelTypeId] FROM [dbo].[rtblChannelTypeImport])
WHERE [fkChannelTypeId] NOT IN (
SELECT [fkChannelTypeId] FROM [dbo].[rtblChannelTypeImport])

GO

DELETE FROM [dbo].[rtblChannelType]
WHERE [pkChannelTypeId] NOT IN (
SELECT [fkChannelTypeId] FROM [dbo].[rtblChannelTypeImport])

GO

