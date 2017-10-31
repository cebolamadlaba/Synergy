--DROP STATISTICS [tblConcessionTransactional].[TableNumber]

--GO

--DROP STATISTICS [tblConcessionTransactional].[AdValorem]

--GO

--DROP STATISTICS [tblConcessionTransactional].[fkConcessionId]

--GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP COLUMN [TableNumber]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
ALTER COLUMN [AdValorem] decimal (18,2) NULL

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
ADD [BaseRate] decimal (18,3) NULL

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
ADD [fkTableNumberId] int NULL

GO

UPDATE [dbo].[tblConcessionTransactional]
SET [fkTableNumberId] = (SELECT TOP 1 [pkTableNumberId] FROM [dbo].[rtblTableNumber])

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
ALTER COLUMN [fkTableNumberId] int NOT NULL

GO



GO


GO



ALTER TABLE [dbo].[tblConcessionTransactional]
ALTER COLUMN [fkConcessionId] int NOT NULL

GO

