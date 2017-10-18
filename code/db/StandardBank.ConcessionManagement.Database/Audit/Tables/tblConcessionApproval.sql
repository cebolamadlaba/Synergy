CREATE TABLE [Audit].[tblConcessionApproval] (
    [pkAuditConcessionApprovalId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionApprovalId]      INT          NOT NULL,
    [fkAuditTypeId]               INT          NOT NULL,
    [Entity]                      XML          NOT NULL,
    [Username]                    VARCHAR (50) NOT NULL,
    [DateStamp]                   DATETIME     CONSTRAINT [DF_Audit_tblConcessionApproval_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionApproval] PRIMARY KEY CLUSTERED ([pkAuditConcessionApprovalId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionApproval_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

