USE [CMS_Dev_V2]
GO


CREATE TABLE [Audit].[tblConcessionGlms](
	[pkAuditConcessionGlmsId] [int] PRIMARY KEY IDENTITY(1,1)  NOT NULL,
	[pkConcessionGlmsId] [int] NOT NULL,
	[fkAuditTypeId] [int] NOT NULL,
	[Entity] [xml] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[DateStamp] [datetime] NOT NULL,
) 

GO


ALTER TABLE [Audit].[tblConcessionGlms] ADD  CONSTRAINT [DF_Audit_tblConcessionGlms_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionGlms]  WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionGlms_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionGlms] CHECK CONSTRAINT [FK_Audit_tblConcessionGlms_AuditType]
GO


