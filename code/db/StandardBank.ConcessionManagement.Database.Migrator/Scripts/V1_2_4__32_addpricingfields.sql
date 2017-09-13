ALTER TABLE dbo.tblConcessionCash ADD
	fkApprovedTableNumberId int NULL
GO
ALTER TABLE dbo.tblConcessionCash ADD CONSTRAINT
	FK_tblConcessionCash_rtblTableNumber_approved FOREIGN KEY
	(fkApprovedTableNumberId) REFERENCES dbo.rtblTableNumber
	(pkTableNumberId)
GO
ALTER TABLE [dbo].[tblConcessionCash] CHECK CONSTRAINT [FK_tblConcessionCash_rtblTableNumber_approved]
GO
ALTER TABLE dbo.tblConcessionTransactional ADD
	fkApprovedTableNumberId int NULL
GO
ALTER TABLE dbo.tblConcessionTransactional ADD CONSTRAINT
	FK_tblConcessionTransactional_rtblTableNumber_approved FOREIGN KEY
	(fkApprovedTableNumberId) REFERENCES dbo.rtblTableNumber
	(pkTableNumberId)
GO
ALTER TABLE [dbo].[tblConcessionTransactional] CHECK CONSTRAINT [FK_tblConcessionTransactional_rtblTableNumber_approved]
GO