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

