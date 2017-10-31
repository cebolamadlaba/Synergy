ALTER TABLE [dbo].[tblConcessionCondition]
ADD [fkPeriodTypeId] int NULL

GO

ALTER TABLE [dbo].[tblConcessionCondition]
ADD [fkPeriodId] int NULL

GO


GO


GO


GO


GO


GO


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


GO


GO


GO



GO


GO


GO


GO

