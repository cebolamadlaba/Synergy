ALTER TABLE [dbo].[tblConcessionRelationship]
ADD [CreationDate] datetime NULL

GO


GO

UPDATE [dbo].[tblConcessionRelationship]
SET [CreationDate] = GETDATE()

GO

ALTER TABLE [dbo].[tblConcessionRelationship]
ALTER COLUMN [CreationDate] datetime NOT NULL

GO

ALTER TABLE [dbo].[tblConcessionRelationship]
ADD [fkUserId] int NULL

GO

UPDATE [dbo].[tblConcessionRelationship]
SET [fkUserId] = (SELECT TOP 1 [pkUserId] FROM [dbo].[tblUser] WHERE [IsActive] = 1)

GO

ALTER TABLE [dbo].[tblConcessionRelationship]
ALTER COLUMN [fkUserId] int NOT NULL

GO


GO


GO
