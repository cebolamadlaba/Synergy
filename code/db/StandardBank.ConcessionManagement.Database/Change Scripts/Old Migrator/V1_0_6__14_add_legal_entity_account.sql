ALTER TABLE [dbo].[tblConcessionLending]
ADD [fkLegalEntityAccountId] int null

GO

UPDATE [dbo].[tblConcessionLending]
SET [fkLegalEntityAccountId] = 
(SELECT TOP 1 [pkLegalEntityAccountId] FROM [dbo].[tblLegalEntityAccount]
WHERE [fkLegalEntityId] = [dbo].[tblConcessionLending].fkLegalEntityId)

GO

ALTER TABLE [dbo].[tblConcessionLending]
ALTER COLUMN [fkLegalEntityAccountId] int not null

GO


GO


GO
