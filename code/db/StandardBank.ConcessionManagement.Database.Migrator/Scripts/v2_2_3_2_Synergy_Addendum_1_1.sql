
INSERT INTO [dbo].[rtblPeriod]([Description],[IsActive])
VALUES('Monthly',1)
GO

Create Table tblAENumberUser
(
	pkAENumberUserId Int Identity(1,1) Not Null Primary Key,
	AENumber Varchar(25) Not Null,
	fkUserId Int Not Null Foreign Key (fkUserId) References tblUser(pkUserId),
	IsActive Bit Not Null Default(1)
)

Alter Table tblConcession
Add fkAENumberUserId Int Null Foreign Key (fkAENumberUserId) References tblAENumberUser(pkAENumberUserId)

Begin Transaction
	Update		c
	Set			c.fkAENumberUserId	=	au.pkAENumberUserId
	From		tblConcession c
	Inner Join	tblAENumberUser au	On	au.fkUserId = c.fkRequestorId

	Select		*
	From		tblConcession c
	Inner Join	tblAENumberUser au	On	au.fkUserId = c.fkRequestorId

Rollback
Commit

--Alter Table tblConcession
--Alter Column fkAENumberUserId Int Not Null Foreign Key (fkAENumberUserId) References tblAENumberUser(pkAENumberUserId)


GO
ALTER VIEW [dbo].[ConcessionInboxView]
AS
SELECT        c.pkConcessionId AS ConcessionId, rg.pkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, le.pkLegalEntityId AS LegalEntityId, le.CustomerName, lea.pkLegalEntityAccountId AS LegalEntityAccountId, 
                         lea.AccountNumber, ct.pkConcessionTypeId AS ConcessionTypeId, ct.Description AS ConcessionType, c.ConcessionDate, s.pkStatusId AS StatusId, s.Description AS Status, ss.pkSubStatusId AS SubStatusId, 
                         ss.Description AS SubStatus, c.ConcessionRef, ms.pkMarketSegmentId AS MarketSegmentId, ms.Description AS Segment, c.DatesentForApproval, cd.pkConcessionDetailId AS ConcessionDetailId, cd.ExpiryDate, 
                         cd.DateApproved, c.fkAAUserId AS AAUserId, c.fkRequestorId AS RequestorId, c.fkBCMUserId AS BCMUserId, c.fkPCMUserId AS PCMUserId, c.fkHOUserId AS HOUserId, ce.pkCentreId AS CentreId, ce.CentreName, 
                         r.pkRegionId AS RegionId, r.Description AS Region, cd.IsMismatched, c.IsActive, c.IsCurrent, cd.PriceExported, cd.PriceExportedDate, c.Archived,
			anu.pkAENumberUserId, aea.fkAccountExecutiveUserId AS CurrentAEUserId, aea.fkAccountAssistantUserId AS CurrentAAUserId
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
                        dbo.rtblRegion AS r ON r.pkRegionId = ce.fkRegionId LEFT JOIN
						dbo.tblAENumberUser AS anu ON anu.pkAENumberUserId = c.fkAENumberUserId LEFT JOIN
						dbo.tblAccountExecutiveAssistant AS aea ON aea.fkAccountExecutiveUserId = anu.fkUserId
GO