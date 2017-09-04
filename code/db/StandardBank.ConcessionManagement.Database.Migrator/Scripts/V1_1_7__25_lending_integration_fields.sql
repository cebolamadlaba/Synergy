ALTER TABLE [dbo].[tblConcessionLending]
ADD [AverageBalance] decimal (18,2) null

GO

CREATE TABLE [dbo].[tblProductLending](
	[pkProductLendingId] [int] IDENTITY(1,1) NOT NULL,
	[fkRiskGroupId] [int] NOT NULL,
	[fkLegalEntityId] [int] NOT NULL,
	[fkLegalEntityAccountId] [int] NOT NULL,
	[fkProductId] [int] NOT NULL,
	[Limit] [decimal](18, 2) NOT NULL,
	[AverageBalance] [decimal](18, 2) NOT NULL,
	[LoadedMap] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_tblProductLending] PRIMARY KEY CLUSTERED 
(
	[pkProductLendingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblProductLending]  WITH CHECK ADD  CONSTRAINT [FK_tblProductLending_rtblProduct] FOREIGN KEY([fkProductId])
REFERENCES [dbo].[rtblProduct] ([pkProductId])
GO

ALTER TABLE [dbo].[tblProductLending] CHECK CONSTRAINT [FK_tblProductLending_rtblProduct]
GO

ALTER TABLE [dbo].[tblProductLending]  WITH CHECK ADD  CONSTRAINT [FK_tblProductLending_tblLegalEntity] FOREIGN KEY([fkLegalEntityId])
REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId])
GO

ALTER TABLE [dbo].[tblProductLending] CHECK CONSTRAINT [FK_tblProductLending_tblLegalEntity]
GO

ALTER TABLE [dbo].[tblProductLending]  WITH CHECK ADD  CONSTRAINT [FK_tblProductLending_tblLegalEntityAccount] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblProductLending] CHECK CONSTRAINT [FK_tblProductLending_tblLegalEntityAccount]
GO

ALTER TABLE [dbo].[tblProductLending]  WITH CHECK ADD  CONSTRAINT [FK_tblProductLending_tblRiskGroup] FOREIGN KEY([fkRiskGroupId])
REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
GO

ALTER TABLE [dbo].[tblProductLending] CHECK CONSTRAINT [FK_tblProductLending_tblRiskGroup]
GO


