ALTER TABLE [dbo].[rtblChannelType]
ADD [ImportFileChannel] varchar(50) NULL

GO

ALTER TABLE [dbo].[rtblTransactionType]
ADD [ImportFileChannel] varchar(50) NULL

GO

ALTER TABLE [dbo].[rtblProduct]
ADD [ImportFileChannel] varchar(50) NULL

GO

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


INSERT INTO [dbo].[tblSapDataImportConfiguration] ([FileImportLocation], [FileExportLocation], [SupportEmailAddress]) VALUES ('C:\Temp\CMS Import', 'C:\Temp\CMS Export', 'heathesh@kohde.io')

GO

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

