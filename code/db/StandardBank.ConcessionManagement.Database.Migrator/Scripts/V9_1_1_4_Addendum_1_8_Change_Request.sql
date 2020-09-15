

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
	Set			tr.ApprovedMarginToPrime = 
					Case When cl.MarginToPrime = 0 And cl.MarginToPrime < tr.MarginToPrime
					Then tr.MarginToPrime
					Else cl.ApprovedMarginToPrime
					End
    --Select *
	From		tblConcession c
	Inner Join	tblConcessionLending cl				On	cl.fkConcessionId			=	c.pkConcessionId
	Inner Join	tblConcessionLendingTieredRate tr	On	tr.fkConcessionLendingId	=	cl.pkConcessionLendingId
	Where		c.fkStatusId In (2,3)

    -- remove any tblConcessionLendingTieredRate entries that are not linked to product type overdraft/tempoverdraft
    Delete		tblConcessionLendingTieredRate
    Where		pkConcessionLendingTieredRateId In
    (
	    Select		Distinct tr.pkConcessionLendingTieredRateId
	    From		tblConcessionLending cl
	    Inner Join	tblConcessionLendingTieredRate tr	On	tr.fkConcessionLendingId	=	cl.pkConcessionLendingId
	    Where		cl.fkProductTypeId Not In (1,15)
    )

Rollback
Commit