


Alter Table tblConcessionDetail
Alter Column fkLegalEntityId Int Null
Go
Alter Table tblConcessionDetail
Alter Column fkLegalEntityAccountId Int Null
Go


ALTER VIEW [dbo].[ConcessionInboxView]
AS
SELECT        c.pkConcessionId AS ConcessionId, rg.pkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, le.pkLegalEntityId AS LegalEntityId, le.CustomerName, lea.pkLegalEntityAccountId AS LegalEntityAccountId, 
                         lea.AccountNumber, ct.pkConcessionTypeId AS ConcessionTypeId, ct.Description AS ConcessionType, c.ConcessionDate, s.pkStatusId AS StatusId, s.Description AS Status, ss.pkSubStatusId AS SubStatusId, 
                         ss.Description AS SubStatus, c.ConcessionRef, ms.pkMarketSegmentId AS MarketSegmentId, ms.Description AS Segment, c.DatesentForApproval, cd.pkConcessionDetailId AS ConcessionDetailId, cd.ExpiryDate, 
                         cd.DateApproved, c.fkAAUserId AS AAUserId, c.fkRequestorId AS RequestorId, c.fkBCMUserId AS BCMUserId, c.fkPCMUserId AS PCMUserId, c.fkHOUserId AS HOUserId, ce.pkCentreId AS CentreId, ce.CentreName, 
                         r.pkRegionId AS RegionId, r.Description AS Region, cd.IsMismatched, c.IsActive, c.IsCurrent, cd.PriceExported, cd.PriceExportedDate, c.Archived
FROM            dbo.tblConcession AS c INNER JOIN
                         dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId INNER JOIN
                         dbo.rtblConcessionType AS ct ON ct.pkConcessionTypeId = c.fkConcessionTypeId INNER JOIN
                         dbo.tblConcessionDetail AS cd ON cd.fkConcessionId = c.pkConcessionId Left JOIN
                         dbo.tblLegalEntity AS le ON le.pkLegalEntityId = cd.fkLegalEntityId Left JOIN
                         dbo.tblLegalEntityAccount AS lea ON lea.pkLegalEntityAccountId = cd.fkLegalEntityAccountId INNER JOIN
                         dbo.rtblStatus AS s ON s.pkStatusId = c.fkStatusId INNER JOIN
                         dbo.rtblSubStatus AS ss ON ss.pkSubStatusId = c.fkSubStatusId INNER JOIN
                         dbo.rtblMarketSegment AS ms ON ms.pkMarketSegmentId = rg.fkMarketSegmentId INNER JOIN
                         dbo.tblCentre AS ce ON ce.pkCentreId = c.fkCentreId INNER JOIN
                         dbo.rtblRegion AS r ON r.pkRegionId = ce.fkRegionId

GO