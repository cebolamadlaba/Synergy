ALTER TABLE [dbo].[tblLegalEntity]
ADD [fkUserId] int null

GO

ALTER TABLE [dbo].[tblLegalEntity]  WITH CHECK ADD  CONSTRAINT [FK_tblLegalEntity_tblUser] FOREIGN KEY([fkUserId])
REFERENCES [dbo].[tblUser] ([pkUserId])
GO

ALTER TABLE [dbo].[tblLegalEntity] CHECK CONSTRAINT [FK_tblLegalEntity_tblUser]
GO


