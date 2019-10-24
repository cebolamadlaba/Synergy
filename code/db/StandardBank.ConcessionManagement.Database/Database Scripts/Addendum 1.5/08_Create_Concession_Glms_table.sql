
CREATE TABLE [dbo].[tblConcessionGlms](
	[pkConcessionGlmsId] [int] IDENTITY(1,1) PRIMARY KEY IDENTITY NOT NULL,
	[fkConcessionId] [int] NOT NULL,
	[fkConcessionDetailId] [int] NOT NULL,
	[fkProductId] [int] NOT NULL,
	[fkLegalEntityAccountId] [int] NOT NULL,
	[fkGroupId] [int] NULL,
	[fkInterestPricingCategoryId][int] NULL,
	[fkSlabTypeId] [int] NULL,
	[fkInterestTypeId] [int] NULL,
)

ALTER TABLE [dbo].[tblConcessionGlms]  WITH CHECK ADD FOREIGN KEY([fkConcessionId])
REFERENCES [dbo].[tblConcession] ([pkConcessionId])
GO

ALTER TABLE [dbo].[tblConcessionGlms]  WITH CHECK ADD FOREIGN KEY([fkConcessionDetailId])
REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
GO

ALTER TABLE [dbo].[tblConcessionGlms]  WITH CHECK ADD FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblConcessionGlms]  WITH CHECK ADD FOREIGN KEY([fkProductId])
REFERENCES [dbo].[tblProductGlms] ([pkProductGlmsId])
GO

ALTER TABLE [dbo].[tblConcessionGlms]  WITH CHECK ADD FOREIGN KEY([fkGroupId])
REFERENCES [dbo].[tblGlmsGroup] ([pkGlmsGroupId])
GO

ALTER TABLE [dbo].[tblConcessionGlms]  WITH CHECK ADD FOREIGN KEY([fkInterestPricingCategoryId])
REFERENCES [dbo].[tblInterestPricingCategory] ([pkInterestPricingCategoryId])
GO

ALTER TABLE [dbo].[tblProductGlms]  WITH CHECK ADD FOREIGN KEY([fkSlabTypeId])
REFERENCES [dbo].[tblSlabType] ([pkSlabTypeId])
GO

ALTER TABLE [dbo].[tblConcessionGlms]  WITH CHECK ADD FOREIGN KEY([fkInterestTypeId])
REFERENCES [dbo].[tblInterestType] ([pkInterestTypeId])
GO



