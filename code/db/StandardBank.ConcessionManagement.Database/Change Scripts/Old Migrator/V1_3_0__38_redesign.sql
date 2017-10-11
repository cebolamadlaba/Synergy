ALTER TABLE [dbo].[tblConcessionCash]
DROP CONSTRAINT [FK_tblConcessionCash_tblLegalEntityAccount]

GO

ALTER TABLE [dbo].[tblConcessionCash]
DROP CONSTRAINT [FK_tblConcessionCash_tblLegalEntity]

GO

ALTER TABLE [dbo].[tblConcessionCash]
DROP COLUMN [fkLegalEntityId]

GO

ALTER TABLE [dbo].[tblConcessionCash]
DROP COLUMN [fkLegalEntityAccountId]

GO

ALTER TABLE [dbo].[tblConcessionLending] 
DROP CONSTRAINT [FK_tblConcessionLending_tblLegalEntityAccount]

GO

ALTER TABLE [dbo].[tblConcessionLending] 
DROP CONSTRAINT [FK_tblConcessionLending_tblLegalEntity]

GO

ALTER TABLE [dbo].[tblConcessionLending] 
DROP COLUMN [fkLegalEntityId]

GO

ALTER TABLE [dbo].[tblConcessionLending] 
DROP COLUMN [fkLegalEntityAccountId]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP CONSTRAINT [FK_tblConcessionTransactional_tblLegalEntityAccount]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP CONSTRAINT [FK_tblConcessionTransactional_tblLegalEntity]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP COLUMN [fkLegalEntityId]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP COLUMN [fkLegalEntityAccountId]

GO


EXEC sys.sp_rename 
    @objname = N'dbo.tblConcession.CentreId', 
    @newname = 'fkCentreId', 
    @objtype = 'COLUMN'

GO


ALTER TABLE [dbo].[tblConcession]
DROP COLUMN [ExpiryDate]

GO



CREATE TABLE [dbo].[tblConcessionDetail](
	[pkConcessionDetailId] [int] IDENTITY(1,1) NOT NULL,
	[fkConcessionId] [int] NOT NULL,
	[fkLegalEntityId] [int] NOT NULL,
	[fkLegalEntityAccountId] [int] NOT NULL,
	[ExpiryDate] [datetime] NULL,
 CONSTRAINT [PK_tblConcessionDetail] PRIMARY KEY CLUSTERED 
(
	[pkConcessionDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblConcessionDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionDetail_tblConcession] FOREIGN KEY([fkConcessionId])
REFERENCES [dbo].[tblConcession] ([pkConcessionId])
GO

ALTER TABLE [dbo].[tblConcessionDetail] CHECK CONSTRAINT [FK_tblConcessionDetail_tblConcession]
GO

ALTER TABLE [dbo].[tblConcessionDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionDetail_tblLegalEntity] FOREIGN KEY([fkLegalEntityId])
REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId])
GO

ALTER TABLE [dbo].[tblConcessionDetail] CHECK CONSTRAINT [FK_tblConcessionDetail_tblLegalEntity]
GO

ALTER TABLE [dbo].[tblConcessionDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionDetail_tblLegalEntityAccount] FOREIGN KEY([fkLegalEntityAccountId])
REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
GO

ALTER TABLE [dbo].[tblConcessionDetail] CHECK CONSTRAINT [FK_tblConcessionDetail_tblLegalEntityAccount]
GO


ALTER TABLE [dbo].[rtblProduct]  WITH CHECK ADD  CONSTRAINT [FK_rtblProduct_rtblConcessionType] FOREIGN KEY([fkConcessionTypeId])
REFERENCES [dbo].[rtblConcessionType] ([pkConcessionTypeId])
GO

ALTER TABLE [dbo].[rtblProduct] CHECK CONSTRAINT [FK_rtblProduct_rtblConcessionType]
GO


ALTER TABLE [dbo].[tblConcessionCash]
DROP CONSTRAINT [FK_tblConcessionCash_rtblBaseRate]

GO

ALTER TABLE [dbo].[tblConcessionCash]
DROP COLUMN [fkBaseRateId]

GO

ALTER TABLE [dbo].[tblConcessionCash]
DROP COLUMN [CashVolume]

GO

ALTER TABLE [dbo].[tblConcessionCash]
DROP COLUMN [CashValue]

GO

DROP TABLE [dbo].[tblConcessionExtension]

GO

DROP TABLE [dbo].[tblConcessionRemovalRequest]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP CONSTRAINT [FK_tblConcessionTransactional_rtblBaseRate]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP CONSTRAINT [FK_tblConcessionTransactional_rtblChannelType]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP COLUMN [fkBaseRateId]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP COLUMN [fkChannelTypeId]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP COLUMN [TransactionVolume]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
DROP COLUMN  [TransactionValue]

GO

ALTER TABLE [dbo].[tblFinancialCash]
DROP COLUMN [LoadedPrice]

GO


ALTER TABLE [dbo].[tblUserRegion]  WITH CHECK ADD  CONSTRAINT [FK_tblUserRegion_rtblRegion] FOREIGN KEY([fkRegionId])
REFERENCES [dbo].[rtblRegion] ([pkRegionId])
GO

ALTER TABLE [dbo].[tblUserRegion] CHECK CONSTRAINT [FK_tblUserRegion_rtblRegion]
GO

ALTER TABLE [dbo].[tblUserRegion]  WITH CHECK ADD  CONSTRAINT [FK_tblUserRegion_tblUser] FOREIGN KEY([fkUserId])
REFERENCES [dbo].[tblUser] ([pkUserId])
GO

ALTER TABLE [dbo].[tblUserRegion] CHECK CONSTRAINT [FK_tblUserRegion_tblUser]
GO


ALTER TABLE [dbo].[tblLegalEntity]
ALTER COLUMN [fkRiskGroupId] int NOT NULL

GO

DELETE FROM [dbo].[tblConcessionCash]

GO

ALTER TABLE [dbo].[tblConcessionCash]
ADD [fkConcessionDetailId] int NOT NULL

GO

ALTER TABLE [dbo].[tblConcessionCash]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionCash_tblConcessionDetail] FOREIGN KEY([fkConcessionDetailId])
REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
GO

ALTER TABLE [dbo].[tblConcessionCash] CHECK CONSTRAINT [FK_tblConcessionCash_tblConcessionDetail]
GO

DELETE FROM [dbo].[tblConcessionLending]

GO

ALTER TABLE [dbo].[tblConcessionLending]
ADD [fkConcessionDetailId] int NOT NULL

GO

ALTER TABLE [dbo].[tblConcessionLending]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionLending_tblConcessionDetail] FOREIGN KEY([fkConcessionDetailId])
REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
GO

ALTER TABLE [dbo].[tblConcessionLending] CHECK CONSTRAINT [FK_tblConcessionLending_tblConcessionDetail]
GO


DELETE FROM [dbo].[tblConcessionBol]

GO

ALTER TABLE [dbo].[tblConcessionBol]
ADD [fkConcessionDetailId] int NOT NULL

GO

ALTER TABLE [dbo].[tblConcessionBol]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionBol_tblConcessionDetail] FOREIGN KEY([fkConcessionDetailId])
REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
GO

ALTER TABLE [dbo].[tblConcessionBol] CHECK CONSTRAINT [FK_tblConcessionBol_tblConcessionDetail]
GO


DELETE FROM [dbo].[tblConcessionInvestment]

GO

ALTER TABLE [dbo].[tblConcessionInvestment]
ADD [fkConcessionDetailId] int NOT NULL

GO

ALTER TABLE [dbo].[tblConcessionInvestment]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionInvestment_tblConcessionDetail] FOREIGN KEY([fkConcessionDetailId])
REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
GO

ALTER TABLE [dbo].[tblConcessionInvestment] CHECK CONSTRAINT [FK_tblConcessionInvestment_tblConcessionDetail]
GO


DELETE FROM [dbo].[tblConcessionMas]

GO

ALTER TABLE [dbo].[tblConcessionMas]
ADD [fkConcessionDetailId] int NOT NULL

GO

ALTER TABLE [dbo].[tblConcessionMas]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionMas_tblConcessionDetail] FOREIGN KEY([fkConcessionDetailId])
REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
GO

ALTER TABLE [dbo].[tblConcessionMas] CHECK CONSTRAINT [FK_tblConcessionMas_tblConcessionDetail]
GO


DELETE FROM [dbo].[tblConcessionTrade]

GO

ALTER TABLE [dbo].[tblConcessionTrade]
ADD [fkConcessionDetailId] int NOT NULL

GO

ALTER TABLE [dbo].[tblConcessionTrade]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionTrade_tblConcessionDetail] FOREIGN KEY([fkConcessionDetailId])
REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
GO

ALTER TABLE [dbo].[tblConcessionTrade] CHECK CONSTRAINT [FK_tblConcessionTrade_tblConcessionDetail]
GO


DELETE FROM [dbo].[tblConcessionTransactional]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
ADD [fkConcessionDetailId] int NOT NULL

GO

ALTER TABLE [dbo].[tblConcessionTransactional]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionTransactional_tblConcessionDetail] FOREIGN KEY([fkConcessionDetailId])
REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
GO

ALTER TABLE [dbo].[tblConcessionTransactional] CHECK CONSTRAINT [FK_tblConcessionTransactional_tblConcessionDetail]
GO


