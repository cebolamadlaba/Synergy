CREATE TABLE [Audit].[rtblApprovalType] (
    [pkAuditApprovalTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [pkApprovalTypeId]      INT          NOT NULL,
    [fkAuditTypeId]         INT          NOT NULL,
    [Entity]                XML          NOT NULL,
    [Username]              VARCHAR (50) NOT NULL,
    [DateStamp]             DATETIME     CONSTRAINT [DF_Audit_rtblApprovalType_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblApprovalType] PRIMARY KEY CLUSTERED ([pkAuditApprovalTypeId] ASC),
    CONSTRAINT [FK_Audit_rtblApprovalType_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

