


/*  
	2019-07-19 - Update: Add LEFT JOIN tblAENumberUser and LEFT JOIN tblCentreUser to get Concession AE Business Center
	2020-01-16 - UPdate: tblLegalEntity/tblLegalEntityAccount changes "Inner Join" to "Left Join";
*/


    ALTER VIEW [dbo].[ConcessionInboxView]
    AS
		SELECT		Distinct 
					c.pkConcessionId AS ConcessionId, rg.pkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, le.pkLegalEntityId AS LegalEntityId, le.CustomerName, le.CustomerNumber, lea.pkLegalEntityAccountId AS LegalEntityAccountId, 
					lea.AccountNumber, ct.pkConcessionTypeId AS ConcessionTypeId, ct.Description AS ConcessionType, c.ConcessionDate, s.pkStatusId AS StatusId, s.Description AS Status, ss.pkSubStatusId AS SubStatusId, 
					ss.Description AS SubStatus, c.ConcessionRef, ms.pkMarketSegmentId AS MarketSegmentId, ms.Description AS Segment, c.DatesentForApproval, cd.pkConcessionDetailId AS ConcessionDetailId, cd.ExpiryDate, 
					cd.DateApproved, c.fkAAUserId AS AAUserId, c.fkRequestorId AS RequestorId, c.fkBCMUserId AS BCMUserId, c.fkPCMUserId AS PCMUserId, c.fkHOUserId AS HOUserId, ce.pkCentreId AS CentreId, ce.CentreName, 
					r.pkRegionId AS RegionId, r.Description AS Region, cd.IsMismatched, c.IsActive, c.IsCurrent, cd.PriceExported, cd.PriceExportedDate, c.Archived,
					--anu.pkAENumberUserId, aea.fkAccountExecutiveUserId AS CurrentAEUserId
					anu.fkUserId As CurrentAEUserId, aea.fkAccountAssistantUserId CurrentAAUserId
		FROM        dbo.tblConcession AS c 
		LEFT JOIN	dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId 
		INNER JOIN	dbo.rtblConcessionType AS ct ON ct.pkConcessionTypeId = c.fkConcessionTypeId 
		INNER JOIN	dbo.tblConcessionDetail AS cd ON cd.fkConcessionId = c.pkConcessionId 
		Left JOIN	dbo.tblLegalEntity AS le ON le.pkLegalEntityId = cd.fkLegalEntityId Or le.pkLegalEntityId = c.fkLegalEntityId 
		Left JOIN	dbo.tblLegalEntityAccount AS lea ON lea.pkLegalEntityAccountId = cd.fkLegalEntityAccountId 
		INNER JOIN	dbo.rtblStatus AS s ON s.pkStatusId = c.fkStatusId
		INNER JOIN	dbo.rtblSubStatus AS ss ON ss.pkSubStatusId = c.fkSubStatusId 
		LEFT JOIN	dbo.rtblMarketSegment AS ms ON ms.pkMarketSegmentId = rg.fkMarketSegmentId 
		LEFT JOIN	tblAENumberUser aenu On	aenu.pkAENumberUserId = c.fkAENumberUserId
		LEFT JOIN	tblCentreUser cu On	cu.fkUserId = aenu.fkUserId
		LEFT JOIN	dbo.tblCentre AS ce ON ce.pkCentreId = cu.fkCentreId 
		LEFT JOIN	dbo.rtblRegion AS r ON r.pkRegionId = ce.fkRegionId  
		LEFT JOIN	dbo.tblAENumberUser AS anu ON anu.pkAENumberUserId = c.fkAENumberUserId 
		LEFT JOIN	dbo.tblAccountExecutiveAssistant AS aea ON aea.fkAccountExecutiveUserId = anu.fkUserId

GO


