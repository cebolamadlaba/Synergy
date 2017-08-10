ALTER TABLE [dbo].[tblConcession]
DROP FK_tblConcession_tblLegalEntity

GO 

ALTER TABLE [dbo].[tblConcession]
DROP COLUMN [fkLegalEntityId]

GO

ALTER TABLE [dbo].[tblConcession]
ADD [fkRiskGroupId] int NULL

GO

UPDATE [dbo].[tblConcession]
SET [fkRiskGroupId] = (SELECT TOP 1 [pkRiskGroupId] FROM [dbo].[tblRiskGroup])

GO

ALTER TABLE [dbo].[tblConcession]
ALTER COLUMN [fkRiskGroupId] int NOT NULL

GO

ALTER TABLE [dbo].[tblConcession]  WITH CHECK ADD  CONSTRAINT [FK_tblConcession_tblRiskGroup] FOREIGN KEY([fkRiskGroupId])
REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])

GO

ALTER TABLE [dbo].[tblConcession] CHECK CONSTRAINT [FK_tblConcession_tblRiskGroup]

GO

