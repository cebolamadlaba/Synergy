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

ALTER TABLE [dbo].[tblConcessionTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionTransactional_tblLegalEntity] FOREIGN KEY([fkLegalEntityId])
REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId])
GO

ALTER TABLE [dbo].[tblConcessionTransactional] CHECK CONSTRAINT [FK_tblConcessionTransactional_tblLegalEntity]
GO

ALTER TABLE [dbo].[tblConcessionTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionTransactional_tblLegalEntityAccount] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblConcessionTransactional] CHECK CONSTRAINT [FK_tblConcessionTransactional_tblLegalEntityAccount]
GO