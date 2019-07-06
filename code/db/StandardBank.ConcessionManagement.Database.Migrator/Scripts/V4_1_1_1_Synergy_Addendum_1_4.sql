

Alter Table tblLegalEntity
Alter Column fkRiskGroupId Int Null
Go

-- Add tblConcessionLending.MRS_BRI and populate value from tblConcession.MRS_CRS
Begin Transaction

    Alter Table tblConcessionLending
    Add MRS_BRI Int Not Null Default(0)
    GO

    Update		cl
    Set			cl.MRS_BRI = RIGHT(SUBSTRING(Cast(MRS_CRS As Varchar(16)),0,CHARINDEX('.', Cast(MRS_CRS As Varchar(16)))),2)
    From		tblConcessionLending cl
    Inner Join	tblConcession c			On	c.pkConcessionId	=	cl.fkConcessionId
    GO

    Select	pkConcessionId, MRS_CRS
		    , Cast(MRS_CRS As Varchar(16))
		    , SUBSTRING(Cast(MRS_CRS As Varchar(16)),0,CHARINDEX('.', Cast(MRS_CRS As Varchar(16))))
		    , RIGHT(SUBSTRING(Cast(MRS_CRS As Varchar(16)),0,CHARINDEX('.', Cast(MRS_CRS As Varchar(16)))),2) [Concession_Value]
		    , cl.MRS_BRI
    From	tblConcession c
    Inner Join	tblConcessionLending cl	On	cl.fkConcessionId	=	c.pkConcessionId

Rollback
Commit

Alter Table tblConcession
Alter Column fkRiskGroupId Int Null
Go

Alter Table tblConcession
Add fkLegalEntityId Int Null Foreign Key (fkLegalEntityId) References tblLegalEntity(pkLegalEntityId)
Go


Alter Table tblProductLending
Alter Column fkRiskGroupId Int Null
Go
Alter Table tblProductLending
Alter Column fkLegalEntityId Int Null
Go
Alter Table tblProductCash
Alter Column fkRiskGroupId Int Null
Go
Alter Table tblProductCash
Alter Column fkLegalEntityId Int Null
Go
Alter Table tblProductTransactional
Alter Column fkRiskGroupId Int Null
Go
Alter Table tblProductTransactional
Alter Column fkLegalEntityId Int Null
Go

    ALTER VIEW [dbo].[ConcessionInboxView]
    AS
    SELECT        Distinct c.pkConcessionId AS ConcessionId, rg.pkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, le.pkLegalEntityId AS LegalEntityId, le.CustomerName, le.CustomerNumber, lea.pkLegalEntityAccountId AS LegalEntityAccountId, 
                             lea.AccountNumber, ct.pkConcessionTypeId AS ConcessionTypeId, ct.Description AS ConcessionType, c.ConcessionDate, s.pkStatusId AS StatusId, s.Description AS Status, ss.pkSubStatusId AS SubStatusId, 
                             ss.Description AS SubStatus, c.ConcessionRef, ms.pkMarketSegmentId AS MarketSegmentId, ms.Description AS Segment, c.DatesentForApproval, cd.pkConcessionDetailId AS ConcessionDetailId, cd.ExpiryDate, 
                             cd.DateApproved, c.fkAAUserId AS AAUserId, c.fkRequestorId AS RequestorId, c.fkBCMUserId AS BCMUserId, c.fkPCMUserId AS PCMUserId, c.fkHOUserId AS HOUserId, ce.pkCentreId AS CentreId, ce.CentreName, 
                             r.pkRegionId AS RegionId, r.Description AS Region, cd.IsMismatched, c.IsActive, c.IsCurrent, cd.PriceExported, cd.PriceExportedDate, c.Archived,
							--anu.pkAENumberUserId, aea.fkAccountExecutiveUserId AS CurrentAEUserId
							anu.fkUserId As CurrentAEUserId, aea.fkAccountAssistantUserId CurrentAAUserId
    FROM            dbo.tblConcession AS c LEFT JOIN
                            dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId INNER JOIN
                            dbo.rtblConcessionType AS ct ON ct.pkConcessionTypeId = c.fkConcessionTypeId INNER JOIN
                            dbo.tblConcessionDetail AS cd ON cd.fkConcessionId = c.pkConcessionId Inner JOIN
                            dbo.tblLegalEntity AS le ON le.pkLegalEntityId = cd.fkLegalEntityId Or le.pkLegalEntityId = c.fkLegalEntityId Inner JOIN
                            dbo.tblLegalEntityAccount AS lea ON lea.pkLegalEntityAccountId = cd.fkLegalEntityAccountId INNER JOIN
                            dbo.rtblStatus AS s ON s.pkStatusId = c.fkStatusId INNER JOIN
                            dbo.rtblSubStatus AS ss ON ss.pkSubStatusId = c.fkSubStatusId LEFT JOIN
                            dbo.rtblMarketSegment AS ms ON ms.pkMarketSegmentId = rg.fkMarketSegmentId INNER JOIN
                            dbo.tblCentre AS ce ON ce.pkCentreId = c.fkCentreId INNER JOIN
                            dbo.rtblRegion AS r ON r.pkRegionId = ce.fkRegionId LEFT JOIN
						    dbo.tblAENumberUser AS anu ON anu.pkAENumberUserId = c.fkAENumberUserId LEFT JOIN
						    dbo.tblAccountExecutiveAssistant AS aea ON aea.fkAccountExecutiveUserId = anu.fkUserId


GO