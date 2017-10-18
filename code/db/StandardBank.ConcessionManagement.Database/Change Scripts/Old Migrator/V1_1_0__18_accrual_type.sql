CREATE TABLE [dbo].[rtblAccrualType](
	[pkAccrualTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_rtblAccrualType] PRIMARY KEY CLUSTERED 
(
	[pkAccrualTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[rtblAccrualType] ADD  CONSTRAINT [DF_rtblAccrualType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO


CREATE TABLE [Audit].[rtblAccrualType] (
	[pkAuditAccrualTypeId] int IDENTITY(1,1) NOT NULL,
	[pkAccrualTypeId] int NOT NULL,
	[fkAuditTypeId] int NOT NULL,
	[Entity] xml NOT NULL,
	[Username] varchar(50) NOT NULL,
	[DateStamp] datetime NOT NULL,
CONSTRAINT [PK_Audit_rtblAccrualType] PRIMARY KEY CLUSTERED 
(
	[pkAuditAccrualTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Audit].[rtblAccrualType] ADD  CONSTRAINT [DF_Audit_rtblAccrualType_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[rtblAccrualType] WITH CHECK ADD  CONSTRAINT [FK_Audit_rtblAccrualType_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[rtblAccrualType] CHECK CONSTRAINT [FK_Audit_rtblAccrualType_AuditType]
GO


SET IDENTITY_INSERT [dbo].[rtblAccrualType] ON

MERGE [dbo].[rtblAccrualType] AS TARGET
USING (VALUES  (1, 'Immediately', 1), (2, 'Daily', 1), (3, 'Monthly', 1)) AS SOURCE ([Id], [Description], [IsActive])
ON TARGET.[pkAccrualTypeId] = SOURCE.[Id]
WHEN MATCHED THEN UPDATE SET [Description] = SOURCE.[Description], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN INSERT ([pkAccrualTypeId], [Description], [IsActive]) VALUES (SOURCE.[Id], SOURCE.[Description], SOURCE.[IsActive]);

SET IDENTITY_INSERT [dbo].[rtblAccrualType] OFF

