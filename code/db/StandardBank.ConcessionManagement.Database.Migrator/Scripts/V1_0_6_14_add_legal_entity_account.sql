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

ALTER TABLE [dbo].[tblConcessionLending]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionLending_tblLegalEntityAccount] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblConcessionLending] CHECK CONSTRAINT [FK_tblConcessionLending_tblLegalEntityAccount]
GO
