ALTER TABLE [dbo].[tblConcession]
ADD [fkRegionId] int null

GO

UPDATE [dbo].[tblConcession]
SET [fkRegionId] = (SELECT TOP 1 [pkRegionId] FROM [dbo].[rtblRegion])

GO

ALTER TABLE [dbo].[tblConcession]
ALTER COLUMN [fkRegionId] int not null

GO


GO


GO


UPDATE [dbo].[tblConcession]
SET [CentreId] = (SELECT TOP 1 [pkCentreId] FROM [dbo].[tblCentre])
WHERE [CentreId] is null

GO

ALTER TABLE [dbo].[tblConcession]
ALTER COLUMN [CentreId] int not null

GO

