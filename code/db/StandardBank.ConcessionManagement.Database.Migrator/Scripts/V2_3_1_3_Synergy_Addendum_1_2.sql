



Alter Table tblConcessionTrade
Add Rate Int Null

Update	tblConcessionTrade
Set		Rate = LoadedRate

Select	LoadedRate, Rate
From	tblConcessionTrade