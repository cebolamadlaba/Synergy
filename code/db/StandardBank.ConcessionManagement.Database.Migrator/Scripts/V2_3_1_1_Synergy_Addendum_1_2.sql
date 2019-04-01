Begin Transaction

	Update	tblConcessionCondition
	Set		Value = ExpectedTurnoverValue
	Where	Value Is Null
	And		ExpectedTurnoverValue Is Not Null

	Select	Value, ExpectedTurnoverValue
	From	tblConcessionCondition

    Alter Table tblConcessionCondition
    Drop Column ExpectedTurnoverValue
    GO





    ALTER VIEW [dbo].[ConcessionConditionView]
    AS
    SELECT        cc.pkConcessionConditionId AS ConcessionConditionId, cc.fkConcessionId AS ConcessionId, c.fkRequestorId AS RequestorId, c.ConcessionRef AS ReferenceNumber, rct.pkConcessionTypeId AS ConcessionTypeId, 
                             rct.Description AS ConcessionType, c.fkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, cc.fkConditionTypeId AS ConditionTypeId, ct.Description AS ConditionType, 
                             cc.fkConditionProductId AS ConditionProductId, cp.Description AS ConditionProduct, cc.fkPeriodTypeId AS PeriodTypeId, pt.Description AS PeriodType, cc.fkPeriodId AS PeriodId, p.Description AS Period, cc.InterestRate, 
                             cc.Volume, cc.Value, cc.ConditionMet, cc.DateApproved, cc.ExpiryDate, cc.IsActive, cc.ActualVolume, cc.ActualValue, cc.ActualTurnover
    FROM            dbo.tblConcessionCondition AS cc INNER JOIN
                             dbo.tblConcession AS c ON c.pkConcessionId = cc.fkConcessionId INNER JOIN
                             dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId INNER JOIN
                             dbo.rtblConditionType AS ct ON ct.pkConditionTypeId = cc.fkConditionTypeId INNER JOIN
                             dbo.rtblConditionProduct AS cp ON cp.pkConditionProductId = cc.fkConditionProductId INNER JOIN
                             dbo.rtblPeriodType AS pt ON pt.pkPeriodTypeId = cc.fkPeriodTypeId INNER JOIN
                             dbo.rtblPeriod AS p ON p.pkPeriodId = cc.fkPeriodId INNER JOIN
                             dbo.rtblConcessionType AS rct ON rct.pkConcessionTypeId = c.fkConcessionTypeId
    WHERE        (c.IsActive = 1) AND (c.fkStatusId IN (2, 3))
    GO





    Create Table tblLegalEntityAddress 
    (
	    pkLegalEntityAddressId Int Identity(1,1) Not Null Primary Key,
	    fkLegalEntityId Int Not Null Foreign Key (fkLegalEntityId) References tblLegalEntity(pkLegalEntityId),
	    ContactPerson Varchar(150) Null,
	    CustomerName Varchar(200) Not Null,
	    PostalAddress Varchar(250) Null,
	    City Varchar(150) Null,
	    PostalCode Varchar(50) Null,
	    DateCreated DateTime Not Null,
	    Datemodified DateTime Null
    )
    GO




    /* Changed Left Join tblLegalEntity and tblLegalEntityAccount to Inner Join */
    /****** Object:  View [dbo].[ConcessionInboxView]    Script Date: 17/01/2019 09:11:51 ******/

    ALTER VIEW [dbo].[ConcessionInboxView]
    AS
    SELECT        Distinct c.pkConcessionId AS ConcessionId, rg.pkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, le.pkLegalEntityId AS LegalEntityId, le.CustomerName, lea.pkLegalEntityAccountId AS LegalEntityAccountId, 
                             lea.AccountNumber, ct.pkConcessionTypeId AS ConcessionTypeId, ct.Description AS ConcessionType, c.ConcessionDate, s.pkStatusId AS StatusId, s.Description AS Status, ss.pkSubStatusId AS SubStatusId, 
                             ss.Description AS SubStatus, c.ConcessionRef, ms.pkMarketSegmentId AS MarketSegmentId, ms.Description AS Segment, c.DatesentForApproval, cd.pkConcessionDetailId AS ConcessionDetailId, cd.ExpiryDate, 
                             cd.DateApproved, c.fkAAUserId AS AAUserId, c.fkRequestorId AS RequestorId, c.fkBCMUserId AS BCMUserId, c.fkPCMUserId AS PCMUserId, c.fkHOUserId AS HOUserId, ce.pkCentreId AS CentreId, ce.CentreName, 
                             r.pkRegionId AS RegionId, r.Description AS Region, cd.IsMismatched, c.IsActive, c.IsCurrent, cd.PriceExported, cd.PriceExportedDate, c.Archived,
			    anu.pkAENumberUserId, aea.fkAccountExecutiveUserId AS CurrentAEUserId
    FROM            dbo.tblConcession AS c INNER JOIN
                            dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId INNER JOIN
                            dbo.rtblConcessionType AS ct ON ct.pkConcessionTypeId = c.fkConcessionTypeId INNER JOIN
                            dbo.tblConcessionDetail AS cd ON cd.fkConcessionId = c.pkConcessionId Inner JOIN
                            dbo.tblLegalEntity AS le ON le.pkLegalEntityId = cd.fkLegalEntityId Inner JOIN
                            dbo.tblLegalEntityAccount AS lea ON lea.pkLegalEntityAccountId = cd.fkLegalEntityAccountId INNER JOIN
                            dbo.rtblStatus AS s ON s.pkStatusId = c.fkStatusId INNER JOIN
                            dbo.rtblSubStatus AS ss ON ss.pkSubStatusId = c.fkSubStatusId INNER JOIN
                            dbo.rtblMarketSegment AS ms ON ms.pkMarketSegmentId = rg.fkMarketSegmentId INNER JOIN
                            dbo.tblCentre AS ce ON ce.pkCentreId = c.fkCentreId INNER JOIN
                            dbo.rtblRegion AS r ON r.pkRegionId = ce.fkRegionId LEFT JOIN
						    dbo.tblAENumberUser AS anu ON anu.pkAENumberUserId = c.fkAENumberUserId LEFT JOIN
						    dbo.tblAccountExecutiveAssistant AS aea ON aea.fkAccountExecutiveUserId = anu.fkUserId

    GO





Rollback
Commit