ALTER TABLE [dbo].[tblConcessionCash]
ADD [fkLegalEntityId] int NULL

GO


UPDATE [dbo].[tblConcessionCash]
SET [fkLegalEntityId] = (SELECT TOP 1 [pkLegalEntityId] FROM [dbo].[tblLegalEntity])

GO

ALTER TABLE [dbo].[tblConcessionCash]
ALTER COLUMN [fkLegalEntityId] int NOT NULL


ALTER TABLE [dbo].[tblConcessionCash]
ADD [fkLegalEntityAccountId] int NULL

GO


UPDATE [dbo].[tblConcessionCash]
SET [fkLegalEntityAccountId] = 
(SELECT TOP 1 [pkLegalEntityAccountId] FROM [dbo].[tblLegalEntityAccount] WHERE [fkLegalEntityId] = 
	(SELECT TOP 1 [pkLegalEntityId] FROM [dbo].[tblLegalEntity]))

GO

ALTER TABLE [dbo].[tblConcessionCash]
ALTER COLUMN [fkLegalEntityAccountId] int NOT NULL

GO


GO


GO


GO


GO