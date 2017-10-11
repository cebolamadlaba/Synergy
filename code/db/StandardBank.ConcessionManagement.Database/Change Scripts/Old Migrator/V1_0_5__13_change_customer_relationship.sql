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

ALTER TABLE [dbo].[tblConcessionLending]
ADD [fkLegalEntityId] int null

GO

UPDATE [dbo].[tblConcessionLending]
SET [fkLegalEntityId] = (SELECT TOP 1 le.pkLegalEntityId FROM [dbo].[tblConcession] c 
JOIN [dbo].[tblLegalEntity] le on le.fkRiskGroupId = c.fkRiskGroupId
WHERE c.pkConcessionId = [dbo].[tblConcessionLending].fkConcessionId)

GO

ALTER TABLE [dbo].[tblConcessionLending]
ALTER COLUMN [fkLegalEntityId] int not null

GO

ALTER TABLE [dbo].[tblConcessionLending]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionLending_tblLegalEntity] FOREIGN KEY([fkLegalEntityId])
REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId])

GO

ALTER TABLE [dbo].[tblConcessionLending] CHECK CONSTRAINT [FK_tblConcessionLending_tblLegalEntity]

GO
