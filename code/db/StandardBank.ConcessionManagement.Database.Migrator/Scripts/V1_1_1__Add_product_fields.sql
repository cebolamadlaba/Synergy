ALTER TABLE [dbo].[tblConcessionDetail]
ADD [PriceExported] bit default(0) NOT NULL

GO

ALTER TABLE [dbo].[tblConcessionDetail]
ADD [PriceExportedDate] datetime NULL

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
	[ExportRow] [bit] NOT NULL,
 CONSTRAINT [PK_tblSapDataImport] PRIMARY KEY CLUSTERED 
(
	[PricepointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblSapDataImport] ADD  CONSTRAINT [DF_tblSapDataImport_ImportDate]  DEFAULT (getdate()) FOR [ImportDate]
GO

ALTER TABLE [dbo].[tblSapDataImport] ADD  CONSTRAINT [DF_tblSapDataImport_ExportRow]  DEFAULT ((0)) FOR [ExportRow]
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


-- INSERT INTO [dbo].[tblSapDataImportConfiguration] ([FileImportLocation], [FileExportLocation], [SupportEmailAddress]) VALUES ('C:\Temp\CMS Import', 'C:\Temp\CMS Export', 'thomas.nowe@standardbank.co.za')

-- GO

ALTER VIEW [dbo].[ConcessionInboxView]
AS
SELECT        c.pkConcessionId AS ConcessionId, rg.pkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, le.pkLegalEntityId AS LegalEntityId, le.CustomerName, ct.pkConcessionTypeId AS ConcessionTypeId, 
                         ct.Description AS ConcessionType, c.ConcessionDate, s.pkStatusId AS StatusId, s.Description AS Status, ss.pkSubStatusId AS SubStatusId, ss.Description AS SubStatus, c.ConcessionRef, 
                         ms.pkMarketSegmentId AS MarketSegmentId, ms.Description AS Segment, c.DatesentForApproval, cd.pkConcessionDetailId AS ConcessionDetailId, cd.ExpiryDate, cd.DateApproved, c.fkRequestorId AS RequestorId, 
                         c.fkBCMUserId AS BCMUserId, c.fkPCMUserId AS PCMUserId, c.fkHOUserId AS HOUserId, ce.pkCentreId AS CentreId, ce.CentreName, p.pkProvinceId AS ProvinceId, p.Description AS Province, cd.IsMismatched, c.IsActive, 
                         c.IsCurrent, cd.PriceExported, cd.PriceExportedDate
FROM            dbo.tblConcession AS c INNER JOIN
                         dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId INNER JOIN
                         dbo.rtblConcessionType AS ct ON ct.pkConcessionTypeId = c.fkConcessionTypeId INNER JOIN
                         dbo.tblConcessionDetail AS cd ON cd.fkConcessionId = c.pkConcessionId INNER JOIN
                         dbo.tblLegalEntity AS le ON le.pkLegalEntityId = cd.fkLegalEntityId INNER JOIN
                         dbo.rtblStatus AS s ON s.pkStatusId = c.fkStatusId INNER JOIN
                         dbo.rtblSubStatus AS ss ON ss.pkSubStatusId = c.fkSubStatusId INNER JOIN
                         dbo.rtblMarketSegment AS ms ON ms.pkMarketSegmentId = rg.fkMarketSegmentId INNER JOIN
                         dbo.tblCentre AS ce ON ce.pkCentreId = c.fkCentreId INNER JOIN
                         dbo.rtblProvince AS p ON p.pkProvinceId = ce.fkProvinceId
GO

CREATE TABLE [dbo].[rtblChannelTypeImport](
	[pkChannelTypeImportId] [int] IDENTITY(1,1) NOT NULL,
	[fkChannelTypeId] [int] NOT NULL,
	[ImportFileChannel] [varchar](50) NOT NULL,
 CONSTRAINT [PK_rtblChannelTypeImport] PRIMARY KEY CLUSTERED 
(
	[pkChannelTypeImportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[rtblChannelTypeImport]  WITH CHECK ADD  CONSTRAINT [FK_rtblChannelTypeImport_rtblChannelType] FOREIGN KEY([fkChannelTypeId])
REFERENCES [dbo].[rtblChannelType] ([pkChannelTypeId])
GO

ALTER TABLE [dbo].[rtblChannelTypeImport] CHECK CONSTRAINT [FK_rtblChannelTypeImport_rtblChannelType]
GO


CREATE TABLE [dbo].[rtblProductImport](
	[pkProductImportId] [int] IDENTITY(1,1) NOT NULL,
	[fkProductId] [int] NOT NULL,
	[ImportFileChannel] [varchar](50) NOT NULL,
 CONSTRAINT [PK_rtblProductImport] PRIMARY KEY CLUSTERED 
(
	[pkProductImportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[rtblProductImport]  WITH CHECK ADD  CONSTRAINT [FK_rtblProductImport_rtblProduct] FOREIGN KEY([fkProductId])
REFERENCES [dbo].[rtblProduct] ([pkProductId])
GO

ALTER TABLE [dbo].[rtblProductImport] CHECK CONSTRAINT [FK_rtblProductImport_rtblProduct]
GO


CREATE TABLE [dbo].[rtblTransactionTypeImport](
	[pkTransactionTypeImportId] [int] IDENTITY(1,1) NOT NULL,
	[fkTransactionTypeId] [int] NOT NULL,
	[ImportFileChannel] [varchar](50) NOT NULL,
 CONSTRAINT [PK_rtblTransactionTypeImport] PRIMARY KEY CLUSTERED 
(
	[pkTransactionTypeImportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[rtblTransactionTypeImport]  WITH CHECK ADD  CONSTRAINT [FK_rtblTransactionTypeImport_rtblTransactionType] FOREIGN KEY([fkTransactionTypeId])
REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId])
GO

ALTER TABLE [dbo].[rtblTransactionTypeImport] CHECK CONSTRAINT [FK_rtblTransactionTypeImport_rtblTransactionType]
GO

INSERT INTO [dbo].[rtblTransactionTypeImport] ([fkTransactionTypeId], [ImportFileChannel])
SELECT [pkTransactionTypeId], '405' FROM [dbo].[rtblTransactionType]
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] = 'Minimum Monthly Service Fee'

GO

INSERT INTO [dbo].[rtblTransactionTypeImport] ([fkTransactionTypeId], [ImportFileChannel])
SELECT [pkTransactionTypeId], '152' FROM [dbo].[rtblTransactionType]
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] = 'Cheque Deposit Fee'

GO

INSERT INTO [dbo].[rtblTransactionTypeImport] ([fkTransactionTypeId], [ImportFileChannel])
SELECT [pkTransactionTypeId], '154' FROM [dbo].[rtblTransactionType]
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] = 'Cheque Encashment Fee'

GO

INSERT INTO [dbo].[rtblTransactionTypeImport] ([fkTransactionTypeId], [ImportFileChannel])
SELECT [pkTransactionTypeId], '155' FROM [dbo].[rtblTransactionType]
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] = 'Cheque Encashment Fee'

GO

INSERT INTO [dbo].[rtblTransactionTypeImport] ([fkTransactionTypeId], [ImportFileChannel])
SELECT [pkTransactionTypeId], '159' FROM [dbo].[rtblTransactionType]
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] = 'Cheque Encashment Fee'

GO

INSERT INTO [dbo].[rtblTransactionTypeImport] ([fkTransactionTypeId], [ImportFileChannel])
SELECT [pkTransactionTypeId], '174' FROM [dbo].[rtblTransactionType]
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] = 'Automatic Cheque Clearance'

GO

INSERT INTO [dbo].[rtblTransactionTypeImport] ([fkTransactionTypeId], [ImportFileChannel])
SELECT [pkTransactionTypeId], '403' FROM [dbo].[rtblTransactionType]
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Transactional')
AND [Description] = 'Cheque Service Fee'

GO

INSERT INTO [dbo].[rtblChannelTypeImport] ([fkChannelTypeId], [ImportFileChannel])
SELECT [pkChannelTypeId], '104' FROM [dbo].[rtblChannelType]
WHERE [Description] = 'Branch Only'

GO

INSERT INTO [dbo].[rtblChannelTypeImport] ([fkChannelTypeId], [ImportFileChannel])
SELECT [pkChannelTypeId], '107' FROM [dbo].[rtblChannelType]
WHERE [Description] = 'Cash Centre Only'

GO

INSERT INTO [dbo].[rtblChannelTypeImport] ([fkChannelTypeId], [ImportFileChannel])
SELECT [pkChannelTypeId], '108' FROM [dbo].[rtblChannelType]
WHERE [Description] = 'Autosafe'

GO

INSERT INTO [dbo].[rtblChannelTypeImport] ([fkChannelTypeId], [ImportFileChannel])
SELECT [pkChannelTypeId], '104' FROM [dbo].[rtblChannelType]
WHERE [Description] = 'Bulk Teller'

GO

INSERT INTO [dbo].[rtblProductImport] ([fkProductId], [ImportFileChannel])
SELECT [pkProductId], '6' FROM [dbo].[rtblProduct]
WHERE [fkConcessionTypeId] = (
SELECT [pkConcessionTypeId] FROM [dbo].[rtblConcessionType]
WHERE [Code] = 'Lending')

GO

