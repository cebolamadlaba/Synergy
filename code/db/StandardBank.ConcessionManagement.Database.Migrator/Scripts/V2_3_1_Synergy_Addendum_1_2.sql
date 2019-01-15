Begin Transaction

	Update	tblConcessionCondition
	Set		Value = ExpectedTurnoverValue
	Where	Value Is Null
	And		ExpectedTurnoverValue Is Not Null

	Select	Value, ExpectedTurnoverValue
	From	tblConcessionCondition

    Alter Table tblConcessionCondition
    Drop Column ExpectedTurnoverValue



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

Rollback
Commit