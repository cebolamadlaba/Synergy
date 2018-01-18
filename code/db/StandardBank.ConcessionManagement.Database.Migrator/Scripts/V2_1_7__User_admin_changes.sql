ALTER TABLE [dbo].[tblRiskGroup]
DROP CONSTRAINT [FK_tblRiskGroup_rtblRegion]

GO

ALTER TABLE [dbo].[tblRiskGroup]
DROP COLUMN [fkRegionId]

GO

ALTER TABLE [dbo].[tblCentre]
DROP CONSTRAINT [FK_tblCentre_rtblProvince]

GO

ALTER TABLE [dbo].[tblCentre]
DROP COLUMN [fkProvinceId]

GO

DROP TABLE [dbo].[rtblProvince]

GO

DROP TABLE [Audit].[rtblProvince]

GO

ALTER TABLE [dbo].[tblCentre]
ADD [fkRegionId] int null

GO


ALTER TABLE [dbo].[tblCentre]  WITH CHECK ADD  CONSTRAINT [FK_tblCentre_rtblRegion] FOREIGN KEY([fkRegionId])
REFERENCES [dbo].[rtblRegion] ([pkRegionId])
GO

ALTER TABLE [dbo].[tblCentre] CHECK CONSTRAINT [FK_tblCentre_rtblRegion]
GO

UPDATE [dbo].[tblCentre]
SET [fkRegionId] = (SELECT TOP 1 [pkRegionId] FROM [dbo].[rtblRegion] WHERE [Description] = 'Gauteng')

GO

ALTER TABLE [dbo].[tblCentre]
ALTER COLUMN [fkRegionId] int NOT NULL

GO

ALTER VIEW [dbo].[ConcessionInboxView]
AS
SELECT        c.pkConcessionId AS ConcessionId, rg.pkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, le.pkLegalEntityId AS LegalEntityId, le.CustomerName, lea.pkLegalEntityAccountId AS LegalEntityAccountId, 
                         lea.AccountNumber, ct.pkConcessionTypeId AS ConcessionTypeId, ct.Description AS ConcessionType, c.ConcessionDate, s.pkStatusId AS StatusId, s.Description AS Status, ss.pkSubStatusId AS SubStatusId, 
                         ss.Description AS SubStatus, c.ConcessionRef, ms.pkMarketSegmentId AS MarketSegmentId, ms.Description AS Segment, c.DatesentForApproval, cd.pkConcessionDetailId AS ConcessionDetailId, cd.ExpiryDate, 
                         cd.DateApproved, c.fkAAUserId AS AAUserId, c.fkRequestorId AS RequestorId, c.fkBCMUserId AS BCMUserId, c.fkPCMUserId AS PCMUserId, c.fkHOUserId AS HOUserId, ce.pkCentreId AS CentreId, ce.CentreName, 
                         r.pkRegionId AS RegionId, r.Description AS Region, cd.IsMismatched, c.IsActive, c.IsCurrent, cd.PriceExported, cd.PriceExportedDate
FROM            dbo.tblConcession AS c INNER JOIN
                         dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId INNER JOIN
                         dbo.rtblConcessionType AS ct ON ct.pkConcessionTypeId = c.fkConcessionTypeId INNER JOIN
                         dbo.tblConcessionDetail AS cd ON cd.fkConcessionId = c.pkConcessionId INNER JOIN
                         dbo.tblLegalEntity AS le ON le.pkLegalEntityId = cd.fkLegalEntityId INNER JOIN
                         dbo.tblLegalEntityAccount AS lea ON lea.pkLegalEntityAccountId = cd.fkLegalEntityAccountId INNER JOIN
                         dbo.rtblStatus AS s ON s.pkStatusId = c.fkStatusId INNER JOIN
                         dbo.rtblSubStatus AS ss ON ss.pkSubStatusId = c.fkSubStatusId INNER JOIN
                         dbo.rtblMarketSegment AS ms ON ms.pkMarketSegmentId = rg.fkMarketSegmentId INNER JOIN
                         dbo.tblCentre AS ce ON ce.pkCentreId = c.fkCentreId INNER JOIN
                         dbo.rtblRegion AS r ON r.pkRegionId = ce.fkRegionId

GO
