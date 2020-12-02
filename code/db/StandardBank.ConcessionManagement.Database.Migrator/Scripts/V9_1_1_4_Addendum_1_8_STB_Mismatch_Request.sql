
--Requested by STB for there is mismatch proc

alter table tblConcessionLendingTieredRate
add [LoadedMarginToPrime] [decimal](18, 3) NULL


alter table tblLoadedPriceLending
add [Limit] [decimal](18, 2) NULL



