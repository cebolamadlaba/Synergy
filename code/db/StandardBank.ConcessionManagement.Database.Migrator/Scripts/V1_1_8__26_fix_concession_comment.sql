DELETE FROM [dbo].[tblConcessionComment]
WHERE NOT EXISTS (SELECT 1 FROM [dbo].[rtblSubStatus] WHERE [pkSubStatusId] = [fkConcessionSubStatusId])

ALTER TABLE [dbo].[tblConcessionComment]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionComment_rtblSubStatus] FOREIGN KEY([fkConcessionSubStatusId])
REFERENCES [dbo].[rtblSubStatus] ([pkSubStatusId])
GO

ALTER TABLE [dbo].[tblConcessionComment] CHECK CONSTRAINT [FK_tblConcessionComment_rtblSubStatus]
GO