ALTER TABLE [dbo].[tblConcession]
ADD [fkRegionId] int null

GO

UPDATE [dbo].[tblConcession]
SET [fkRegionId] = (SELECT TOP 1 [pkRegionId] FROM [dbo].[rtblRegion])

GO

ALTER TABLE [dbo].[tblConcession]
ALTER COLUMN [fkRegionId] int not null

GO

ALTER TABLE [dbo].[tblConcession]  WITH CHECK ADD  CONSTRAINT [FK_tblConcession_rtblRegion] FOREIGN KEY([fkRegionId])
REFERENCES [dbo].[rtblRegion] ([pkRegionId])
GO

ALTER TABLE [dbo].[tblConcession] CHECK CONSTRAINT [FK_tblConcession_rtblRegion]
GO


UPDATE [dbo].[tblConcession]
SET [CentreId] = (SELECT TOP 1 [pkCentreId] FROM [dbo].[tblCentre])
WHERE [CentreId] is null

GO

ALTER TABLE [dbo].[tblConcession]
ALTER COLUMN [CentreId] int not null

GO

