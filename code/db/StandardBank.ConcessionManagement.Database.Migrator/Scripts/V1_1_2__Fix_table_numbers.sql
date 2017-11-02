ALTER TABLE [dbo].[rtblTableNumber]
ADD [TempBaseRate] decimal (18, 3) NULL

GO

UPDATE [dbo].[rtblTableNumber]
SET [TempBaseRate] = [BaseRate]

GO

UPDATE [dbo].[rtblTableNumber]
SET [BaseRate] = [AdValorem]

GO

UPDATE [dbo].[rtblTableNumber]
SET [AdValorem] = [TempBaseRate]

GO

ALTER TABLE [dbo].[rtblTableNumber]
DROP COLUMN [TempBaseRate] 

GO

ALTER TABLE [dbo].[tblConcessionCash]
ADD [TempBaseRate] decimal (18, 3) NULL

GO

UPDATE [dbo].[tblConcessionCash]
SET [TempBaseRate] = [BaseRate]

GO

UPDATE [dbo].[tblConcessionCash]
SET [BaseRate] = [AdValorem]

GO

UPDATE [dbo].[tblConcessionCash]
SET [AdValorem] = [TempBaseRate]

GO

ALTER TABLE [dbo].[tblConcessionCash]
DROP COLUMN [TempBaseRate] 

GO

