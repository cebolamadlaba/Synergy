ALTER TABLE [dbo].[rtblChannelType]
ADD [ImportFileProductId] varchar(50) NULL

GO

ALTER TABLE [dbo].[rtblTransactionType]
ADD [ImportFileProductId] varchar(50) NULL

GO

ALTER TABLE [dbo].[rtblProduct]
ADD [ImportFileProductId] varchar(50) NULL

GO

ALTER TABLE [dbo].[tblConcessionDetail]
ADD [PriceExported] bit default(0) NOT NULL

GO

ALTER TABLE [dbo].[tblConcessionDetail]
ADD [PriceExportedDate] datetime NULL

GO

UPDATE [dbo].[rtblTransactionType]
SET [ImportFileProductId] = 'SX900_3'
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] = 'Minimum Monthly Service Fee'

UPDATE [dbo].[rtblTransactionType]
SET [ImportFileProductId] = 'SX901_3'
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] = 'Cheque Deposit Fee'

UPDATE [dbo].[rtblTransactionType]
SET [ImportFileProductId] = 'SX902_1'
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] = 'Cheque Encashment Fee'

UPDATE [dbo].[rtblTransactionType]
SET [ImportFileProductId] = 'SX900_1'
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] = 'Cheque Service Fee'

UPDATE [dbo].[rtblChannelType]
SET [ImportFileProductId] = 'SX901_1'
WHERE [Description] = 'Branch Only'

UPDATE [dbo].[rtblChannelType]
SET [ImportFileProductId] = 'SX901_2'
WHERE [Description] = 'Cash Centre Only'

UPDATE [dbo].[rtblChannelType]
SET [ImportFileProductId] = 'SX901_4'
WHERE [Description] = 'ANA/ATM'

UPDATE [dbo].[rtblChannelType]
SET [ImportFileProductId] = 'AS_1_AutoSafe_CDF_10'
WHERE [Description] = 'Autosafe & Cash Centre Only'

GO

CREATE TABLE [dbo].[tblSapDataImport](
	[PricepointId] [int] NOT NULL,
	[CustomerId] [varchar](50) NULL,
	[AccountName] [varchar](50) NULL,
	[ProductId] [varchar](50) NULL,
	[Description] [varchar](500) NULL,
	[GroupId] [varchar](50) NULL,
	[SubGroupId] [varchar](50) NULL,
	[BankIdentifierId] [varchar](50) NULL,
	[AccountNo] [varchar](50) NULL,
	[OptionId] [varchar](50) NULL,
	[UserId] [varchar](50) NULL,
	[TierFromValue] [varchar](50) NULL,
	[TierToValue] [varchar](50) NULL,
	[AdvaloremFee] [varchar](50) NULL,
	[MinimumFee] [varchar](50) NULL,
	[MaximumFee] [varchar](50) NULL,
	[FlatFee] [varchar](50) NULL,
	[CommunicationFee] [varchar](50) NULL,
	[TableNo] [varchar](50) NULL,
	[TransactionVolume] [varchar](50) NULL,
	[TransactionRevenue] [varchar](50) NULL,
	[ProductName] [varchar](50) NULL,
	[Channel] [varchar](50) NULL,
	[MarketSegment] [varchar](50) NULL,
	[SequenceId] [varchar](50) NULL,
	[EntryDate] [varchar](50) NULL,
	[EffectiveDate] [varchar](50) NULL,
	[ExpiryDate] [varchar](50) NULL,
	[TerminationDate] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[ImportDate] [datetime] NOT NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblSapDataImport] PRIMARY KEY CLUSTERED 
(
	[PricepointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblSapDataImport] ADD  CONSTRAINT [DF_tblSapDataImport_ImportDate]  DEFAULT (getdate()) FOR [ImportDate]
GO


CREATE TABLE [dbo].[tblSapDataImportConfiguration](
	[pkSapDataImportConfigurationId] [int] IDENTITY(1,1) NOT NULL,
	[FileImportLocation] [varchar](500) NOT NULL,
	[FileExportLocation] [varchar](500) NOT NULL,
	[SupportEmailAddress] [varchar](255) NOT NULL,
 CONSTRAINT [PK_tblSapDataImportConfiguration] PRIMARY KEY CLUSTERED 
(
	[pkSapDataImportConfigurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


INSERT INTO [dbo].[tblSapDataImportConfiguration] ([FileImportLocation], [FileExportLocation], [SupportEmailAddress]) VALUES ('C:\Temp\CMS Import', 'C:\Temp\CMS Export', 'heathesh@kohde.io')

GO

