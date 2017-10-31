ALTER TABLE [dbo].[tblConcessionTransactional]
ADD [fkLegalEntityId] int NULL

GO


UPDATE [dbo].[tblConcessionTransactional]
SET [fkLegalEntityId] = (SELECT TOP 1 [pkLegalEntityId] FROM [dbo].[tblLegalEntity])

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
ALTER COLUMN [fkLegalEntityId] int NOT NULL


ALTER TABLE [dbo].[tblConcessionTransactional]
ADD [fkLegalEntityAccountId] int NULL

GO


UPDATE [dbo].[tblConcessionTransactional]
SET [fkLegalEntityAccountId] = 
(SELECT TOP 1 [pkLegalEntityAccountId] FROM [dbo].[tblLegalEntityAccount] WHERE [fkLegalEntityId] = 
	(SELECT TOP 1 [pkLegalEntityId] FROM [dbo].[tblLegalEntity]))

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
ALTER COLUMN [fkLegalEntityAccountId] int NOT NULL

GO


GO


GO


GO


GO