/****** Object:  Table [Audit].[tblConcessionDetail]    Script Date: 2018/07/16 11:29:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Audit].[tblConcessionDetail](
	[pkAuditpkConcessionDetailId] [int] IDENTITY(1,1) NOT NULL,
	[pkConcessionDetailId] [int] NOT NULL,
	[fkAuditTypeId] [int] NOT NULL,
	[Entity] [xml] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[DateStamp] [datetime] NOT NULL,
 CONSTRAINT [PK_Audit_tblConcessionDetail] PRIMARY KEY CLUSTERED 
(
	[pkAuditpkConcessionDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [Audit].[tblConcessionDetail] ADD  CONSTRAINT [DF_Audit_tblConcessionDetail_DateStamp]  DEFAULT (getdate()) FOR [DateStamp]
GO

ALTER TABLE [Audit].[tblConcessionDetail]  WITH CHECK ADD  CONSTRAINT [FK_Audit_tblConcessionDetail_AuditType] FOREIGN KEY([fkAuditTypeId])
REFERENCES [Audit].[AuditType] ([Id])
GO

ALTER TABLE [Audit].[tblConcessionDetail] CHECK CONSTRAINT [FK_Audit_tblConcessionDetail_AuditType]
GO

ALTER TABLE tblConcessionDetail
add Archived  datetime;

ALTER TABLE tblConcession
add Archived  datetime;