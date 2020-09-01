Create Table tblConcessionLendingTieredRate
(
	pkConcessionLendingTieredRateId Int Identity(1,1) Not Null Primary Key,
	fkConcessionLendingId Int Not Null Foreign Key (fkConcessionLendingId) References tblConcessionLending(pkConcessionLendingId),
	Limit Decimal(18,2) Null,
	MarginToPrime Decimal(18,3) Null
)

-- Insert Existing Limit and MarginToPrime combination into tblConcessionLendingTieredRate
Insert Into	tblConcessionLendingTieredRate(fkConcessionLendingId, Limit, MarginToPrime)
Select		Distinct cl.pkConcessionLendingId, cl.Limit, cl.MarginToPrime
From		tblConcessionLending cl
Left Join	tblConcessionLendingTieredRate tr	On	tr.fkConcessionLendingId	=	cl.pkConcessionLendingId
Where		(cl.Limit <> 0 Or cl.MarginToPrime <> 0 )
And			tr.pkConcessionLendingTieredRateId Is Null