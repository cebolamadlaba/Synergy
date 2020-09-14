

Alter Table tblConcessionLendingTieredRate
Add ApprovedMarginToPrime Decimal(18,3) Null


Begin Transaction
	Update		tr
	Set			tr.ApprovedMarginToPrime = cl.ApprovedMarginToPrime,
	--Select	*
	From		tblConcessionLending cl
	Inner Join	tblConcessionLendingTieredRate tr	On	tr.fkConcessionLendingId	=	cl.pkConcessionLendingId
	Where	tr.pkConcessionLendingTieredRateId =
	(
			Select  Max(tr2.pkConcessionLendingTieredRateId)
			From	tblConcessionLendingTieredRate tr2
			Where	tr2.fkConcessionLendingId = tr.fkConcessionLendingId
	)
	And		cl.ApprovedMarginToPrime Is Not Null

	Update		tr
	Set			tr.ApprovedMarginToPrime = tr.MarginToPrime
	--Select	*
	From		tblConcession c
	Inner Join	tblConcessionLending cl				On	cl.fkConcessionId			=	c.pkConcessionId
	Inner Join	tblConcessionLendingTieredRate tr	On	tr.fkConcessionLendingId	=	cl.pkConcessionLendingId
	Where		c.fkStatusId In (2,3)
	And			cl.ApprovedMarginToPrime Is Not Null
	And			tr.ApprovedMarginToPrime Is Null

Rollback
Commit