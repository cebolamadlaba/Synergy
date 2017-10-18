ALTER TABLE [dbo].[tblConcessionCash]
ADD [BaseRate] decimal(18,2) NULL

GO

ALTER TABLE [dbo].[tblConcessionCash]
ADD [fkAccrualTypeId] int NULL

GO

UPDATE [dbo].[tblConcessionCash]
SET [fkAccrualTypeId] = (SELECT TOP 1 [pkAccrualTypeId] FROM [dbo].[rtblAccrualType])

ALTER TABLE [dbo].[tblConcessionCash]
ALTER COLUMN [fkAccrualTypeId] int NOT NULL

ALTER TABLE [dbo].[tblConcessionCash]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionCash_rtblAccrualType] FOREIGN KEY([fkAccrualTypeId])
REFERENCES [dbo].[rtblAccrualType] ([pkAccrualTypeId])
GO

ALTER TABLE [dbo].[tblConcessionCash] CHECK CONSTRAINT [FK_tblConcessionCash_rtblAccrualType]
GO