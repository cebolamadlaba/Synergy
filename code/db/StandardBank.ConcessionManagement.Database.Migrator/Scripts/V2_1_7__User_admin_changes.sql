ALTER TABLE [dbo].[tblRiskGroup]
DROP CONSTRAINT [FK_tblRiskGroup_rtblRegion]

GO

ALTER TABLE [dbo].[tblRiskGroup]
DROP COLUMN [fkRegionId]

GO

