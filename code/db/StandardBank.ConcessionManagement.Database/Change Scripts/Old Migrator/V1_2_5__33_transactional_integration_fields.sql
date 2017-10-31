
GO


GO


GO


GO


GO


GO


GO


GO


GO


GO


GO



GO


GO


GO


ALTER TABLE [dbo].[rtblTableNumber]
ADD [fkConcessionTypeId] int NULL

GO

UPDATE [dbo].[rtblTableNumber]
SET [fkConcessionTypeId] = (SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType] WHERE Code = 'Cash')

GO

ALTER TABLE [dbo].[rtblTableNumber]
ALTER COLUMN [fkConcessionTypeId] int NOT NULL

GO



GO


GO