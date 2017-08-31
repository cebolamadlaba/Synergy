ALTER TABLE [dbo].[tblConcessionRelationship]
ADD [CreationDate] datetime NULL

GO

ALTER TABLE [dbo].[tblConcessionRelationship] ADD  CONSTRAINT [DF_tblConcessionRelationship_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
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

ALTER TABLE [dbo].[tblConcessionRelationship]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionRelationship_tblUser] FOREIGN KEY([fkUserId])
REFERENCES [dbo].[tblUser] ([pkUserId])
GO

ALTER TABLE [dbo].[tblConcessionRelationship] CHECK CONSTRAINT [FK_tblConcessionRelationship_tblUser]
GO
