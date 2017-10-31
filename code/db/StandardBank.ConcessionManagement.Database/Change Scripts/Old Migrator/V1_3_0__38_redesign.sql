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




GO


GO


GO


GO


GO


GO


GO



GO


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



GO


GO


GO


GO


ALTER TABLE [dbo].[tblLegalEntity]
ALTER COLUMN [fkRiskGroupId] int NOT NULL

GO

DELETE FROM [dbo].[tblConcessionCash]

GO

ALTER TABLE [dbo].[tblConcessionCash]
ADD [fkConcessionDetailId] int NOT NULL

GO


GO


GO

DELETE FROM [dbo].[tblConcessionLending]

GO

ALTER TABLE [dbo].[tblConcessionLending]
ADD [fkConcessionDetailId] int NOT NULL

GO


GO


GO


DELETE FROM [dbo].[tblConcessionBol]

GO

ALTER TABLE [dbo].[tblConcessionBol]
ADD [fkConcessionDetailId] int NOT NULL

GO


GO


GO


DELETE FROM [dbo].[tblConcessionInvestment]

GO

ALTER TABLE [dbo].[tblConcessionInvestment]
ADD [fkConcessionDetailId] int NOT NULL

GO


GO


GO


DELETE FROM [dbo].[tblConcessionMas]

GO

ALTER TABLE [dbo].[tblConcessionMas]
ADD [fkConcessionDetailId] int NOT NULL

GO


GO


GO


DELETE FROM [dbo].[tblConcessionTrade]

GO

ALTER TABLE [dbo].[tblConcessionTrade]
ADD [fkConcessionDetailId] int NOT NULL

GO


GO


GO


DELETE FROM [dbo].[tblConcessionTransactional]

GO

ALTER TABLE [dbo].[tblConcessionTransactional]
ADD [fkConcessionDetailId] int NOT NULL

GO


GO


GO


