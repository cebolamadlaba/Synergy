ALTER TABLE dbo.tblConcessionCash ADD
	fkApprovedTableNumberId int NULL,
	fkLoadedTableNumberId int NULL
GO

GO

GO

GO

GO
ALTER TABLE dbo.tblConcessionTransactional ADD
	fkApprovedTableNumberId int NULL,
	fkLoadedTableNumberId int NULL
GO

GO

GO

GO

GO
ALTER TABLE dbo.tblConcessionLending ADD
	LoadedMarginToPrime decimal(18,2) NULL
GO