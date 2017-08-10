ALTER TABLE [dbo].[tblConcession]
DROP FK_tblConcession_tblLegalEntity

GO 

ALTER TABLE [dbo].[tblConcession]
DROP COLUMN [fkLegalEntityId]

GO
