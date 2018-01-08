ALTER TABLE [dbo].[tblLegalEntity]
ADD [fkUserId] int null

GO

ALTER TABLE [dbo].[tblLegalEntity]  WITH CHECK ADD  CONSTRAINT [FK_tblLegalEntity_tblUser] FOREIGN KEY([fkUserId])
REFERENCES [dbo].[tblUser] ([pkUserId])
GO

ALTER TABLE [dbo].[tblLegalEntity] CHECK CONSTRAINT [FK_tblLegalEntity_tblUser]
GO

---- script for developers to set at least one legal entity per risk group to a particular user
---- in this case I'm using Mpho's user
--with legalentities as (
--select 
--	le.[pkLegalEntityId], 
--	le.[fkUserId],
--	ROW_NUMBER() over(partition by le.[fkRiskGroupId] order by le.[fkRiskGroupId]) as rownum 
--from [dbo].[tblLegalEntity] le)
--update legalentities
--set [fkUserId] = (select top 1 [pkUserId] from [dbo].[tblUser] where [EmailAddress] = 'mpho.nxiwa@standardbank.co.za')
--where rownum =