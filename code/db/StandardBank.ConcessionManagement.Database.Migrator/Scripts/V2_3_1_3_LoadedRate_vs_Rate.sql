



Alter Table tblConcessionTrade
Add Rate Decimal(18,5) Null

Update	tblConcessionTrade
Set		Rate = LoadRate

Select	LoadedRate, Rate
From	tblConcessionTrade