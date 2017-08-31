
CREATE TABLE [dbo].[rtblRelationship](
	[pkRelationshipId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_rtblRelationship] PRIMARY KEY CLUSTERED 
(
	[pkRelationshipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[rtblRelationship] ADD  CONSTRAINT [DF_rtblRelationship_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO


CREATE TABLE [Audit].[rtblRelationship] (
	[pkAuditRelationshipId] int IDENTITY(1,1) NOT NULL,
	[pkRelationshipId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblRelationship] PRIMARY KEY CLUSTERED 
(
	[pkAuditRelationshipId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblRelationship] ADD  CONSTRAINT [DF_Audit_rtblRelationship_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblRelationship] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblRelationship_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblRelationship] CHECK CONSTRAINT [FK_Audit_rtblRelationship_AuditType]
GO


CREATE TABLE [dbo].[tblConcessionRelationship](
	[pkConcessionRelationshipId] [int] IDENTITY(1,1) NOT NULL,
	[fkParentConcessionId] [int] NOT NULL,
	[fkChildConcessionId] [int] NOT NULL,
	[fkRelationshipId] [int] NOT NULL,
 CONSTRAINT [PK_tblConcessionRelationship] PRIMARY KEY CLUSTERED 
(
	[pkConcessionRelationshipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblConcessionRelationship]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionRelationship_rtblRelationship] FOREIGN KEY([fkRelationshipId])
REFERENCES [dbo].[rtblRelationship] ([pkRelationshipId])
GO

ALTER TABLE [dbo].[tblConcessionRelationship] CHECK CONSTRAINT [FK_tblConcessionRelationship_rtblRelationship]
GO

ALTER TABLE [dbo].[tblConcessionRelationship]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionRelationship_tblConcession_Child] FOREIGN KEY([fkChildConcessionId])
REFERENCES [dbo].[tblConcession] ([pkConcessionId])
GO

ALTER TABLE [dbo].[tblConcessionRelationship] CHECK CONSTRAINT [FK_tblConcessionRelationship_tblConcession_Child]
GO

ALTER TABLE [dbo].[tblConcessionRelationship]  WITH CHECK ADD  CONSTRAINT [FK_tblConcessionRelationship_tblConcession_Parent] FOREIGN KEY([fkParentConcessionId])
REFERENCES [dbo].[tblConcession] ([pkConcessionId])
GO

ALTER TABLE [dbo].[tblConcessionRelationship] CHECK CONSTRAINT [FK_tblConcessionRelationship_tblConcession_Parent]
GO


CREATE TABLE [Audit].[tblConcessionRelationship] (
	[pkAuditConcessionRelationshipId] int IDENTITY(1,1) NOT NULL,
	[pkConcessionRelationshipId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_tblConcessionRelationship] PRIMARY KEY CLUSTERED 
(
	[pkAuditConcessionRelationshipId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[tblConcessionRelationship] ADD  CONSTRAINT [DF_Audit_tblConcessionRelationship_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionRelationship] WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionRelationship_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionRelationship] CHECK CONSTRAINT [FK_Audit_tblConcessionRelationship_AuditType]
GO

SET IDENTITY_INSERT [dbo].[rtblRelationship] ON

MERGE [dbo].[rtblRelationship] AS TARGET
USING (VALUES  (1, 'Extension', 1), (2, 'Renewal', 1)) AS SOURCE ([Id], [Description], [IsActive])
ON TARGET.[pkRelationshipId] = SOURCE.[Id]
WHEN MATCHED THEN UPDATE SET [Description] = SOURCE.[Description], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN INSERT ([pkRelationshipId], [Description], [IsActive]) VALUES (SOURCE.[Id], SOURCE.[Description], SOURCE.[IsActive]);

SET IDENTITY_INSERT [dbo].[rtblRelationship] OFF

