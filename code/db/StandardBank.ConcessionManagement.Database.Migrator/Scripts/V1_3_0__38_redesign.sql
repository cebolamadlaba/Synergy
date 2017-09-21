ALTER TABLE [dbo].[tblConcession]
ADD [fkLegalEntityId] int NULL

GO

ALTER TABLE [dbo].[tblConcession]
ADD [fkLegalEntityAccountId] int NULL

GO

ALTER TABLE [dbo].[tblConcession]
ADD [fkMarketSegmentId] int NULL

GO

UPDATE [dbo].[tblConcession]
SET [fkLegalEntityId] = (SELECT TOP 1 [pkLegalEntityId] FROM [dbo].[tblLegalEntity])

GO

UPDATE [dbo].[tblConcession]
SET [fkLegalEntityAccountId] = (SELECT TOP 1 [pkLegalEntityAccountId] FROM [dbo].[tblLegalEntityAccount])

GO

UPDATE [dbo].[tblConcession]
SET [fkMarketSegmentId] = (SELECT TOP 1 [fkMarketSegmentId] FROM [dbo].[tblLegalEntity])

GO

ALTER TABLE [dbo].[tblConcession]
ALTER COLUMN [fkLegalEntityId] int NOT NULL

GO

ALTER TABLE [dbo].[tblConcession]
ALTER COLUMN [fkLegalEntityAccountId] int NOT NULL

GO

ALTER TABLE [dbo].[tblConcession]
ALTER COLUMN [fkMarketSegmentId] int NOT NULL

GO

ALTER TABLE [dbo].[tblConcession]  WITH CHECK ADD  CONSTRAINT [FK_tblConcession_tblLegalEntity] FOREIGN KEY([fkLegalEntityId])
REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId])
GO

ALTER TABLE [dbo].[tblConcession] CHECK CONSTRAINT [FK_tblConcession_tblLegalEntity]
GO

ALTER TABLE [dbo].[tblConcession]  WITH CHECK ADD  CONSTRAINT [FK_tblConcession_tblLegalEntityAccount] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblConcession] CHECK CONSTRAINT [FK_tblConcession_tblLegalEntityAccount]
GO

ALTER TABLE [dbo].[tblConcession]  WITH CHECK ADD  CONSTRAINT [FK_tblConcession_rtblMarketSegment] FOREIGN KEY([fkMarketSegmentId])
REFERENCES [dbo].[rtblMarketSegment] ([pkMarketSegmentId])
GO

ALTER TABLE [dbo].[tblConcession] CHECK CONSTRAINT [FK_tblConcession_rtblMarketSegment]
GO

ALTER TABLE [dbo].[tblConcessionCash]
DROP CONSTRAINT [FK_tblConcessionCash_tblLegalEntityAccount]

GO

ALTER TABLE [dbo].[tblConcessionCash]
DROP CONSTRAINT [FK_tblConcessionCash_tblLegalEntity]

GO

ALTER TABLE [dbo].[tblConcessionCash]
DROP COLUMN [fkLegalEntityId]

GO

ALTER TABLE [dbo].[tblConcessionCash]
DROP COLUMN [fkLegalEntityAccountId]

GO

ALTER TABLE [dbo].[tblConcessionLending] 
DROP CONSTRAINT [FK_tblConcessionLending_tblLegalEntityAccount]

GO

ALTER TABLE [dbo].[tblConcessionLending] 
DROP CONSTRAINT [FK_tblConcessionLending_tblLegalEntity]

GO

ALTER TABLE [dbo].[tblConcessionLending] 
DROP COLUMN [fkLegalEntityId]

GO

ALTER TABLE [dbo].[tblConcessionLending] 
DROP COLUMN [fkLegalEntityAccountId]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP CONSTRAINT [FK_tblConcessionTransactional_tblLegalEntityAccount]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP CONSTRAINT [FK_tblConcessionTransactional_tblLegalEntity]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP COLUMN [fkLegalEntityId]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP COLUMN [fkLegalEntityAccountId]

GO


