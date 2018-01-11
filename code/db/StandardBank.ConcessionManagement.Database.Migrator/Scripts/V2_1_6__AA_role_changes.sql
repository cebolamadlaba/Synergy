SET IDENTITY_INSERT [dbo].[rtblRole] ON

MERGE [dbo].[rtblRole] AS TARGET
USING (VALUES (6, 'AA', 'Account Assistant', 1)) AS SOURCE ([Id], [RoleName], [RoleDescription], [IsActive])
ON TARGET.[pkRoleId] = SOURCE.[Id]
WHEN MATCHED THEN 
UPDATE SET [RoleName] = SOURCE.[RoleName], [RoleDescription] = SOURCE.[RoleDescription], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN
INSERT ([pkRoleId], [RoleName], [RoleDescription], [IsActive]) VALUES (SOURCE.[Id], SOURCE.[RoleName], SOURCE.[RoleDescription], SOURCE.[IsActive]);

SET IDENTITY_INSERT [dbo].[rtblRole] OFF

GO

CREATE TABLE [dbo].[tblAccountExecutiveAssistant](
	[pkAccountExecutiveAssistantId] [int] IDENTITY(1,1) NOT NULL,
	[fkAccountAssistantUserId] [int] NOT NULL,
	[fkAccountExecutiveUserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_tblAccountExecutiveAssistant] PRIMARY KEY CLUSTERED 
(
	[pkAccountExecutiveAssistantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblAccountExecutiveAssistant] ADD  CONSTRAINT [DF_tblAccountExecutiveAssistant_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[tblAccountExecutiveAssistant]  WITH CHECK ADD  CONSTRAINT [FK_tblAccountExecutiveAssistant_tblUser_AA] FOREIGN KEY([fkAccountAssistantUserId])
REFERENCES [dbo].[tblUser] ([pkUserId])
GO

ALTER TABLE [dbo].[tblAccountExecutiveAssistant] CHECK CONSTRAINT [FK_tblAccountExecutiveAssistant_tblUser_AA]
GO

ALTER TABLE [dbo].[tblAccountExecutiveAssistant]  WITH CHECK ADD  CONSTRAINT [FK_tblAccountExecutiveAssistant_tblUser_AE] FOREIGN KEY([fkAccountExecutiveUserId])
REFERENCES [dbo].[tblUser] ([pkUserId])
GO

ALTER TABLE [dbo].[tblAccountExecutiveAssistant] CHECK CONSTRAINT [FK_tblAccountExecutiveAssistant_tblUser_AE]
GO

CREATE TABLE [Audit].[tblAccountExecutiveAssistant](
	[pkAuditAccountExecutiveAssistantId] [int] IDENTITY(1,1) NOT NULL,
	[pkAccountExecutiveAssistantId] [int] NOT NULL,
	[fkAuditTypeId] [int] NOT NULL,
	[Entity] [xml] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[DateStamp] [datetime] NOT NULL,
 CONSTRAINT [PK_Audit_tblAccountExecutiveAssistant] PRIMARY KEY CLUSTERED 
(
	[pkAuditAccountExecutiveAssistantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [Audit].[tblAccountExecutiveAssistant] ADD  CONSTRAINT [DF_Audit_tblAccountExecutiveAssistant_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblAccountExecutiveAssistant]  WITH CHECK ADD  CONSTRAINT [FK_Audit_tblAccountExecutiveAssistant_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblAccountExecutiveAssistant] CHECK CONSTRAINT [FK_Audit_tblAccountExecutiveAssistant_AuditType]
GO

ALTER TABLE [dbo].[tblConcession]
ADD [fkAAUserId] int null

GO

ALTER TABLE [dbo].[tblConcession]  WITH CHECK ADD  CONSTRAINT [FK_tblConcession_tblUser_AA] FOREIGN KEY([fkAAUserId])
REFERENCES [dbo].[tblUser] ([pkUserId])
GO

ALTER TABLE [dbo].[tblConcession] CHECK CONSTRAINT [FK_tblConcession_tblUser_AA]
GO

ALTER VIEW [dbo].[ConcessionInboxView]
AS
SELECT        c.pkConcessionId AS ConcessionId, rg.pkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, le.pkLegalEntityId AS LegalEntityId, le.CustomerName, lea.pkLegalEntityAccountId AS LegalEntityAccountId, 
                         lea.AccountNumber, ct.pkConcessionTypeId AS ConcessionTypeId, ct.Description AS ConcessionType, c.ConcessionDate, s.pkStatusId AS StatusId, s.Description AS Status, ss.pkSubStatusId AS SubStatusId, 
                         ss.Description AS SubStatus, c.ConcessionRef, ms.pkMarketSegmentId AS MarketSegmentId, ms.Description AS Segment, c.DatesentForApproval, cd.pkConcessionDetailId AS ConcessionDetailId, cd.ExpiryDate, 
                         cd.DateApproved, c.fkAAUserId AS AAUserId, c.fkRequestorId AS RequestorId, c.fkBCMUserId AS BCMUserId, c.fkPCMUserId AS PCMUserId, c.fkHOUserId AS HOUserId, ce.pkCentreId AS CentreId, ce.CentreName, 
                         p.pkProvinceId AS ProvinceId, p.Description AS Province, cd.IsMismatched, c.IsActive, c.IsCurrent, cd.PriceExported, cd.PriceExportedDate
FROM            dbo.tblConcession AS c INNER JOIN
                         dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId INNER JOIN
                         dbo.rtblConcessionType AS ct ON ct.pkConcessionTypeId = c.fkConcessionTypeId INNER JOIN
                         dbo.tblConcessionDetail AS cd ON cd.fkConcessionId = c.pkConcessionId INNER JOIN
                         dbo.tblLegalEntity AS le ON le.pkLegalEntityId = cd.fkLegalEntityId INNER JOIN
                         dbo.tblLegalEntityAccount AS lea ON lea.pkLegalEntityAccountId = cd.fkLegalEntityAccountId INNER JOIN
                         dbo.rtblStatus AS s ON s.pkStatusId = c.fkStatusId INNER JOIN
                         dbo.rtblSubStatus AS ss ON ss.pkSubStatusId = c.fkSubStatusId INNER JOIN
                         dbo.rtblMarketSegment AS ms ON ms.pkMarketSegmentId = rg.fkMarketSegmentId INNER JOIN
                         dbo.tblCentre AS ce ON ce.pkCentreId = c.fkCentreId INNER JOIN
                         dbo.rtblProvince AS p ON p.pkProvinceId = ce.fkProvinceId
GO

