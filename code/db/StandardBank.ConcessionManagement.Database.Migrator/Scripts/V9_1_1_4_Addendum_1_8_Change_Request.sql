

Alter Table tblConcessionLendingTieredRate
Add ApprovedMarginToPrime Decimal(18,3) Null


Begin Transaction

	-- get list of non-overdraft lending concessions that have tiered rates;
	-- populate the first row tiered rate into tblConcessionLending.Limit and tblConcessionLending.MarginToPrime;
	-- for approved concessions populate the tblConcessionLending.ApprovedMarginToPrime from first row tiered rate MarginToPrime;
	Update		cl
	Set			cl.Limit = tr.Limit,
				cl.MarginToPrime = tr.MarginToPrime
	--Select		*
	From		tblConcession c
	Inner Join	tblConcessionLending cl	On	cl.fkConcessionId	=	c.pkConcessionId
	Inner Join	tblConcessionLendingTieredRate tr	On	tr.fkConcessionLendingId	=	cl.pkConcessionLendingId
	Where	    cl.fkProductTypeId Not In (1, 15)	
	And		    (cl.Limit = 0 And cl.MarginToPrime = 0)

    -- update the first tiered rate row approved margin against prime
	Update		tr
	Set			tr.ApprovedMarginToPrime = cl.ApprovedMarginToPrime
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

    -- update tiered rate approved margin to prime with either tiered rate margin to prime or line item approved margin to prime
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