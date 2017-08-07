ALTER TABLE [dbo].[tblConcessionCondition]
ADD [fkPeriodTypeId] int NULL

GO

ALTER TABLE [dbo].[tblConcessionCondition]
ADD [fkPeriodId] int NULL

GO

ALTER TABLE [dbo].[tblConcessionCondition]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionCondition_rtblPeriod] FOREIGN KEY([fkPeriodId])
REFERENCES [dbo].[rtblPeriod] ([pkPeriodId])
GO

ALTER TABLE [dbo].[tblConcessionCondition] CHECK CONSTRAINT [FK_tblConcessionCondition_rtblPeriod]
GO

ALTER TABLE [dbo].[tblConcessionCondition]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionCondition_rtblPeriodType] FOREIGN KEY([fkPeriodTypeId])
REFERENCES [dbo].[rtblPeriodType] ([pkPeriodTypeId])
GO

ALTER TABLE [dbo].[tblConcessionCondition] CHECK CONSTRAINT [FK_tblConcessionCondition_rtblPeriodType]
GO

CREATE SCHEMA [Audit]
GO

CREATE TABLE [Audit].[AuditType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AuditType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [Audit].[AuditType] ON

MERGE [Audit].[AuditType] AS TARGET
USING (VALUES  (1, 'Insert'), (2, 'Update'), (3, 'Delete')) AS SOURCE ([Id], [Description])
ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN UPDATE SET [Description] = SOURCE.[Description]
WHEN NOT MATCHED THEN INSERT ([Id], [Description]) VALUES (SOURCE.[Id], SOURCE.[Description]);

SET IDENTITY_INSERT [Audit].[AuditType] OFF

CREATE TABLE [Audit].[rtblApprovalType] (
	[pkAuditApprovalTypeId] int IDENTITY(1,1) NOT NULL,
	[pkApprovalTypeId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblApprovalType] PRIMARY KEY CLUSTERED 
(
	[pkAuditApprovalTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblApprovalType] ADD  CONSTRAINT [DF_Audit_rtblApprovalType_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblApprovalType] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblApprovalType_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblApprovalType] CHECK CONSTRAINT [FK_Audit_rtblApprovalType_AuditType]
GO

CREATE TABLE [Audit].[rtblBaseRate] (
	[pkAuditBaseRateId] int IDENTITY(1,1) NOT NULL,
	[pkBaseRateId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblBaseRate] PRIMARY KEY CLUSTERED 
(
	[pkAuditBaseRateId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblBaseRate] ADD  CONSTRAINT [DF_Audit_rtblBaseRate_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblBaseRate] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblBaseRate_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblBaseRate] CHECK CONSTRAINT [FK_Audit_rtblBaseRate_AuditType]
GO

CREATE TABLE [Audit].[rtblChannelType] (
	[pkAuditChannelTypeId] int IDENTITY(1,1) NOT NULL,
	[pkChannelTypeId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblChannelType] PRIMARY KEY CLUSTERED 
(
	[pkAuditChannelTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblChannelType] ADD  CONSTRAINT [DF_Audit_rtblChannelType_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblChannelType] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblChannelType_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblChannelType] CHECK CONSTRAINT [FK_Audit_rtblChannelType_AuditType]
GO

CREATE TABLE [Audit].[rtblConcessionType] (
	[pkAuditConcessionTypeId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionTypeId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblConcessionType] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblConcessionType] ADD  CONSTRAINT [DF_Audit_rtblConcessionType_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblConcessionType] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblConcessionType_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblConcessionType] CHECK CONSTRAINT [FK_Audit_rtblConcessionType_AuditType]
GO

CREATE TABLE [Audit].[rtblConditionProduct] (
	[pkAuditConditionProductId] int IDENTITY(1,1) NOT NULL,
	[pkConditionProductId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblConditionProduct] PRIMARY KEY CLUSTERED 
(
	[pkAuditConditionProductId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblConditionProduct] ADD  CONSTRAINT [DF_Audit_rtblConditionProduct_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblConditionProduct] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblConditionProduct_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblConditionProduct] CHECK CONSTRAINT [FK_Audit_rtblConditionProduct_AuditType]
GO

CREATE TABLE [Audit].[rtblConditionType] (
	[pkAuditConditionTypeId] int IDENTITY(1,1) NOT NULL,
	[pkConditionTypeId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblConditionType] PRIMARY KEY CLUSTERED 
(
	[pkAuditConditionTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblConditionType] ADD  CONSTRAINT [DF_Audit_rtblConditionType_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblConditionType] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblConditionType_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblConditionType] CHECK CONSTRAINT [FK_Audit_rtblConditionType_AuditType]
GO

CREATE TABLE [Audit].[rtblMarketSegment] (
	[pkAuditMarketSegmentId] int IDENTITY(1,1) NOT NULL,
	[pkMarketSegmentId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblMarketSegment] PRIMARY KEY CLUSTERED 
(
	[pkAuditMarketSegmentId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblMarketSegment] ADD  CONSTRAINT [DF_Audit_rtblMarketSegment_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblMarketSegment] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblMarketSegment_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblMarketSegment] CHECK CONSTRAINT [FK_Audit_rtblMarketSegment_AuditType]
GO

CREATE TABLE [Audit].[rtblProduct] (
	[pkAuditProductId] int IDENTITY(1,1) NOT NULL,
	[pkProductId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblProduct] PRIMARY KEY CLUSTERED 
(
	[pkAuditProductId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblProduct] ADD  CONSTRAINT [DF_Audit_rtblProduct_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblProduct] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblProduct_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblProduct] CHECK CONSTRAINT [FK_Audit_rtblProduct_AuditType]
GO

CREATE TABLE [Audit].[rtblProvince] (
	[pkAuditProvinceId] int IDENTITY(1,1) NOT NULL,
	[pkProvinceId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblProvince] PRIMARY KEY CLUSTERED 
(
	[pkAuditProvinceId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblProvince] ADD  CONSTRAINT [DF_Audit_rtblProvince_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblProvince] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblProvince_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblProvince] CHECK CONSTRAINT [FK_Audit_rtblProvince_AuditType]
GO

CREATE TABLE [Audit].[rtblReviewFeeType] (
	[pkAuditReviewFeeTypeId] int IDENTITY(1,1) NOT NULL,
	[pkReviewFeeTypeId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblReviewFeeType] PRIMARY KEY CLUSTERED 
(
	[pkAuditReviewFeeTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblReviewFeeType] ADD  CONSTRAINT [DF_Audit_rtblReviewFeeType_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblReviewFeeType] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblReviewFeeType_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblReviewFeeType] CHECK CONSTRAINT [FK_Audit_rtblReviewFeeType_AuditType]
GO

CREATE TABLE [Audit].[rtblRole] (
	[pkAuditRoleId] int IDENTITY(1,1) NOT NULL,
	[pkRoleId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblRole] PRIMARY KEY CLUSTERED 
(
	[pkAuditRoleId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblRole] ADD  CONSTRAINT [DF_Audit_rtblRole_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblRole] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblRole_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblRole] CHECK CONSTRAINT [FK_Audit_rtblRole_AuditType]
GO

CREATE TABLE [Audit].[rtblStatus] (
	[pkAuditStatusId] int IDENTITY(1,1) NOT NULL,
	[pkStatusId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblStatus] PRIMARY KEY CLUSTERED 
(
	[pkAuditStatusId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblStatus] ADD  CONSTRAINT [DF_Audit_rtblStatus_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblStatus] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblStatus_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblStatus] CHECK CONSTRAINT [FK_Audit_rtblStatus_AuditType]
GO

CREATE TABLE [Audit].[rtblSubStatus] (
	[pkAuditSubStatusId] int IDENTITY(1,1) NOT NULL,
	[pkSubStatusId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblSubStatus] PRIMARY KEY CLUSTERED 
(
	[pkAuditSubStatusId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblSubStatus] ADD  CONSTRAINT [DF_Audit_rtblSubStatus_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblSubStatus] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblSubStatus_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblSubStatus] CHECK CONSTRAINT [FK_Audit_rtblSubStatus_AuditType]
GO

CREATE TABLE [Audit].[tblConcession] (
	[pkAuditConcessionId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcession] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcession] ADD  CONSTRAINT [DF_Audit_tblConcession_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcession] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcession_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcession] CHECK CONSTRAINT [FK_Audit_tblConcession_AuditType]
GO

CREATE TABLE [Audit].[rtblTransactionGroup] (
	[pkAuditTransactionGroupId] int IDENTITY(1,1) NOT NULL,
	[pkTransactionGroupId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblTransactionGroup] PRIMARY KEY CLUSTERED 
(
	[pkAuditTransactionGroupId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblTransactionGroup] ADD  CONSTRAINT [DF_Audit_rtblTransactionGroup_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblTransactionGroup] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblTransactionGroup_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblTransactionGroup] CHECK CONSTRAINT [FK_Audit_rtblTransactionGroup_AuditType]
GO

CREATE TABLE [Audit].[rtblTransactionType] (
	[pkAuditTransactionTypeId] int IDENTITY(1,1) NOT NULL,
	[pkTransactionTypeId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblTransactionType] PRIMARY KEY CLUSTERED 
(
	[pkAuditTransactionTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblTransactionType] ADD  CONSTRAINT [DF_Audit_rtblTransactionType_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblTransactionType] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblTransactionType_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblTransactionType] CHECK CONSTRAINT [FK_Audit_rtblTransactionType_AuditType]
GO

CREATE TABLE [Audit].[rtblType] (
	[pkAuditTypeId] int IDENTITY(1,1) NOT NULL,
	[pkTypeId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblType] PRIMARY KEY CLUSTERED 
(
	[pkAuditTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblType] ADD  CONSTRAINT [DF_Audit_rtblType_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblType] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblType_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblType] CHECK CONSTRAINT [FK_Audit_rtblType_AuditType]
GO

CREATE TABLE [Audit].[tblBolUser] (
	[pkAuditBolUserId] int IDENTITY(1,1) NOT NULL,
	[pkBolUserId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblBolUser] PRIMARY KEY CLUSTERED 
(
	[pkAuditBolUserId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblBolUser] ADD  CONSTRAINT [DF_Audit_tblBolUser_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblBolUser] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblBolUser_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblBolUser] CHECK CONSTRAINT [FK_Audit_tblBolUser_AuditType]
GO

CREATE TABLE [Audit].[tblBusinesOnlineTransactionType] (
	[pkAuditBusinesOnlineTransactionTypeId] int IDENTITY(1,1) NOT NULL,
	[pkBusinesOnlineTransactionTypeId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblBusinesOnlineTransactionType] PRIMARY KEY CLUSTERED 
(
	[pkAuditBusinesOnlineTransactionTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblBusinesOnlineTransactionType] ADD  CONSTRAINT [DF_Audit_tblBusinesOnlineTransactionType_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblBusinesOnlineTransactionType] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblBusinesOnlineTransactionType_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblBusinesOnlineTransactionType] CHECK CONSTRAINT [FK_Audit_tblBusinesOnlineTransactionType_AuditType]
GO

CREATE TABLE [Audit].[tblCentre] (
	[pkAuditCentreId] int IDENTITY(1,1) NOT NULL,
	[pkCentreId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblCentre] PRIMARY KEY CLUSTERED 
(
	[pkAuditCentreId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblCentre] ADD  CONSTRAINT [DF_Audit_tblCentre_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblCentre] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblCentre_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblCentre] CHECK CONSTRAINT [FK_Audit_tblCentre_AuditType]
GO

CREATE TABLE [Audit].[tblCentreBusinessManager] (
	[pkAuditCentreBusinessManagerId] int IDENTITY(1,1) NOT NULL,
	[pkCentreBusinessManagerId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblCentreBusinessManager] PRIMARY KEY CLUSTERED 
(
	[pkAuditCentreBusinessManagerId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblCentreBusinessManager] ADD  CONSTRAINT [DF_Audit_tblCentreBusinessManager_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblCentreBusinessManager] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblCentreBusinessManager_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblCentreBusinessManager] CHECK CONSTRAINT [FK_Audit_tblCentreBusinessManager_AuditType]
GO

CREATE TABLE [Audit].[tblCentreUser] (
	[pkAuditCentreUserId] int IDENTITY(1,1) NOT NULL,
	[pkCentreUserId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblCentreUser] PRIMARY KEY CLUSTERED 
(
	[pkAuditCentreUserId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblCentreUser] ADD  CONSTRAINT [DF_Audit_tblCentreUser_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblCentreUser] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblCentreUser_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblCentreUser] CHECK CONSTRAINT [FK_Audit_tblCentreUser_AuditType]
GO

CREATE TABLE [Audit].[tblChannelTypeBaseRate] (
	[pkAuditChannelTypeBaseRateId] int IDENTITY(1,1) NOT NULL,
	[pkChannelTypeBaseRateId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblChannelTypeBaseRate] PRIMARY KEY CLUSTERED 
(
	[pkAuditChannelTypeBaseRateId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblChannelTypeBaseRate] ADD  CONSTRAINT [DF_Audit_tblChannelTypeBaseRate_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblChannelTypeBaseRate] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblChannelTypeBaseRate_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblChannelTypeBaseRate] CHECK CONSTRAINT [FK_Audit_tblChannelTypeBaseRate_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionAccount] (
	[pkAuditConcessionAccountId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionAccountId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionAccount] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionAccountId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionAccount] ADD  CONSTRAINT [DF_Audit_tblConcessionAccount_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionAccount] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionAccount_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionAccount] CHECK CONSTRAINT [FK_Audit_tblConcessionAccount_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionApproval] (
	[pkAuditConcessionApprovalId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionApprovalId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionApproval] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionApprovalId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionApproval] ADD  CONSTRAINT [DF_Audit_tblConcessionApproval_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionApproval] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionApproval_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionApproval] CHECK CONSTRAINT [FK_Audit_tblConcessionApproval_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionBol] (
	[pkAuditConcessionBolId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionBolId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionBol] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionBolId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionBol] ADD  CONSTRAINT [DF_Audit_tblConcessionBol_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionBol] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionBol_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionBol] CHECK CONSTRAINT [FK_Audit_tblConcessionBol_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionCash] (
	[pkAuditConcessionCashId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionCashId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionCash] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionCashId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionCash] ADD  CONSTRAINT [DF_Audit_tblConcessionCash_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionCash] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionCash_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionCash] CHECK CONSTRAINT [FK_Audit_tblConcessionCash_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionComment] (
	[pkAuditConcessionCommentId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionCommentId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionComment] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionCommentId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionComment] ADD  CONSTRAINT [DF_Audit_tblConcessionComment_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionComment] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionComment_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionComment] CHECK CONSTRAINT [FK_Audit_tblConcessionComment_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionCondition] (
	[pkAuditConcessionConditionId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionConditionId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionCondition] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionConditionId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionCondition] ADD  CONSTRAINT [DF_Audit_tblConcessionCondition_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionCondition] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionCondition_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionCondition] CHECK CONSTRAINT [FK_Audit_tblConcessionCondition_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionInvestment] (
	[pkAuditConcessionInvestmentId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionInvestmentId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionInvestment] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionInvestmentId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionInvestment] ADD  CONSTRAINT [DF_Audit_tblConcessionInvestment_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionInvestment] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionInvestment_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionInvestment] CHECK CONSTRAINT [FK_Audit_tblConcessionInvestment_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionMas] (
	[pkAuditConcessionMasId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionMasId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionMas] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionMasId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionMas] ADD  CONSTRAINT [DF_Audit_tblConcessionMas_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionMas] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionMas_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionMas] CHECK CONSTRAINT [FK_Audit_tblConcessionMas_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionLending] (
	[pkAuditConcessionLendingId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionLendingId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionLending] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionLendingId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionLending] ADD  CONSTRAINT [DF_Audit_tblConcessionLending_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionLending] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionLending_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionLending] CHECK CONSTRAINT [FK_Audit_tblConcessionLending_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionRemovalRequest] (
	[pkAuditConcessionRemovalRequestId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionRemovalRequestId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionRemovalRequest] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionRemovalRequestId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionRemovalRequest] ADD  CONSTRAINT [DF_Audit_tblConcessionRemovalRequest_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionRemovalRequest] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionRemovalRequest_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionRemovalRequest] CHECK CONSTRAINT [FK_Audit_tblConcessionRemovalRequest_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionTrade] (
	[pkAuditConcessionTradeId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionTradeId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionTrade] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionTradeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionTrade] ADD  CONSTRAINT [DF_Audit_tblConcessionTrade_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionTrade] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionTrade_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionTrade] CHECK CONSTRAINT [FK_Audit_tblConcessionTrade_AuditType]
GO

CREATE TABLE [Audit].[tblConcessionTransactional] (
	[pkAuditConcessionTransactionalId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionTransactionalId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionTransactional] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionTransactionalId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionTransactional] ADD  CONSTRAINT [DF_Audit_tblConcessionTransactional_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionTransactional] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionTransactional_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionTransactional] CHECK CONSTRAINT [FK_Audit_tblConcessionTransactional_AuditType]
GO

CREATE TABLE [Audit].[tblConditionTypeProduct] (
	[pkAuditConditionTypeProductId] int IDENTITY(1,1) NOT NULL,
	[pkConditionTypeProductId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConditionTypeProduct] PRIMARY KEY CLUSTERED 
(
	[pkAuditConditionTypeProductId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConditionTypeProduct] ADD  CONSTRAINT [DF_Audit_tblConditionTypeProduct_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConditionTypeProduct] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConditionTypeProduct_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConditionTypeProduct] CHECK CONSTRAINT [FK_Audit_tblConditionTypeProduct_AuditType]
GO

CREATE TABLE [Audit].[tblRiskGroup] (
	[pkAuditRiskGroupId] int IDENTITY(1,1) NOT NULL,
	[pkRiskGroupId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblRiskGroup] PRIMARY KEY CLUSTERED 
(
	[pkAuditRiskGroupId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblRiskGroup] ADD  CONSTRAINT [DF_Audit_tblRiskGroup_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblRiskGroup] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblRiskGroup_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblRiskGroup] CHECK CONSTRAINT [FK_Audit_tblRiskGroup_AuditType]
GO

CREATE TABLE [Audit].[tblLegalEntityAccount] (
	[pkAuditLegalEntityAccountId] int IDENTITY(1,1) NOT NULL,
	[pkLegalEntityAccountId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblLegalEntityAccount] PRIMARY KEY CLUSTERED 
(
	[pkAuditLegalEntityAccountId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblLegalEntityAccount] ADD  CONSTRAINT [DF_Audit_tblLegalEntityAccount_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblLegalEntityAccount] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblLegalEntityAccount_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblLegalEntityAccount] CHECK CONSTRAINT [FK_Audit_tblLegalEntityAccount_AuditType]
GO

CREATE TABLE [Audit].[rtblPeriodType] (
	[pkAuditPeriodTypeId] int IDENTITY(1,1) NOT NULL,
	[pkPeriodTypeId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblPeriodType] PRIMARY KEY CLUSTERED 
(
	[pkAuditPeriodTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblPeriodType] ADD  CONSTRAINT [DF_Audit_rtblPeriodType_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblPeriodType] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblPeriodType_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblPeriodType] CHECK CONSTRAINT [FK_Audit_rtblPeriodType_AuditType]
GO

CREATE TABLE [Audit].[tblLegalEntity] (
	[pkAuditLegalEntityId] int IDENTITY(1,1) NOT NULL,
	[pkLegalEntityId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblLegalEntity] PRIMARY KEY CLUSTERED 
(
	[pkAuditLegalEntityId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblLegalEntity] ADD  CONSTRAINT [DF_Audit_tblLegalEntity_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblLegalEntity] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblLegalEntity_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblLegalEntity] CHECK CONSTRAINT [FK_Audit_tblLegalEntity_AuditType]
GO

CREATE TABLE [Audit].[tblScenarioManagerToolDeal] (
	[pkAuditScenarioManagerToolDealId] int IDENTITY(1,1) NOT NULL,
	[pkScenarioManagerToolDealId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblScenarioManagerToolDeal] PRIMARY KEY CLUSTERED 
(
	[pkAuditScenarioManagerToolDealId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblScenarioManagerToolDeal] ADD  CONSTRAINT [DF_Audit_tblScenarioManagerToolDeal_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblScenarioManagerToolDeal] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblScenarioManagerToolDeal_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblScenarioManagerToolDeal] CHECK CONSTRAINT [FK_Audit_tblScenarioManagerToolDeal_AuditType]
GO

CREATE TABLE [Audit].[rtblPeriod] (
	[pkAuditPeriodId] int IDENTITY(1,1) NOT NULL,
	[pkPeriodId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblPeriod] PRIMARY KEY CLUSTERED 
(
	[pkAuditPeriodId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblPeriod] ADD  CONSTRAINT [DF_Audit_rtblPeriod_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblPeriod] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblPeriod_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblPeriod] CHECK CONSTRAINT [FK_Audit_rtblPeriod_AuditType]
GO

CREATE TABLE [Audit].[tblUser] (
	[pkAuditUserId] int IDENTITY(1,1) NOT NULL,
	[pkUserId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblUser] PRIMARY KEY CLUSTERED 
(
	[pkAuditUserId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblUser] ADD  CONSTRAINT [DF_Audit_tblUser_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblUser] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblUser_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblUser] CHECK CONSTRAINT [FK_Audit_tblUser_AuditType]
GO

CREATE TABLE [Audit].[tblUserRole] (
	[pkAuditUserRoleId] int IDENTITY(1,1) NOT NULL,
	[pkUserRoleId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblUserRole] PRIMARY KEY CLUSTERED 
(
	[pkAuditUserRoleId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblUserRole] ADD  CONSTRAINT [DF_Audit_tblUserRole_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblUserRole] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblUserRole_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblUserRole] CHECK CONSTRAINT [FK_Audit_tblUserRole_AuditType]
GO

CREATE TABLE [Audit].[rtblRegion] (
	[pkAuditRegionId] int IDENTITY(1,1) NOT NULL,
	[pkRegionId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblRegion] PRIMARY KEY CLUSTERED 
(
	[pkAuditRegionId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblRegion] ADD  CONSTRAINT [DF_Audit_rtblRegion_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblRegion] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblRegion_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblRegion] CHECK CONSTRAINT [FK_Audit_rtblRegion_AuditType]
GO

CREATE TABLE [Audit].[tblUserRegion] (
	[pkAuditUserRegionId] int IDENTITY(1,1) NOT NULL,
	[pkUserRegionId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblUserRegion] PRIMARY KEY CLUSTERED 
(
	[pkAuditUserRegionId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblUserRegion] ADD  CONSTRAINT [DF_Audit_tblUserRegion_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblUserRegion] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblUserRegion_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblUserRegion] CHECK CONSTRAINT [FK_Audit_tblUserRegion_AuditType]
GO

CREATE TABLE [Audit].[rtblAdValorem] (
	[pkAuditAdValoremId] int IDENTITY(1,1) NOT NULL,
	[pkAdValoremId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblAdValorem] PRIMARY KEY CLUSTERED 
(
	[pkAuditAdValoremId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblAdValorem] ADD  CONSTRAINT [DF_Audit_rtblAdValorem_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblAdValorem] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblAdValorem_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblAdValorem] CHECK CONSTRAINT [FK_Audit_rtblAdValorem_AuditType]
GO

