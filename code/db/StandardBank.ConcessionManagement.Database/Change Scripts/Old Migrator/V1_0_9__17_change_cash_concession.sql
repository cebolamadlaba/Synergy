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

ALTER TABLE [dbo].[tblConcessionCash]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionCash_tblLegalEntity] FOREIGN KEY([fkLegalEntityId])
REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId])
GO

ALTER TABLE [dbo].[tblConcessionCash] CHECK CONSTRAINT [FK_tblConcessionCash_tblLegalEntity]
GO

ALTER TABLE [dbo].[tblConcessionCash]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionCash_tblLegalEntityAccount] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblConcessionCash] CHECK CONSTRAINT [FK_tblConcessionCash_tblLegalEntityAccount]
GO