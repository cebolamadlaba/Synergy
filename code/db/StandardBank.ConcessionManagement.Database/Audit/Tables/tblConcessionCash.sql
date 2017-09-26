CREATE TABLE [Audit].[tblConcessionCash] (
    [pkAuditConcessionCashId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionCashId]      INT          NOT NULL,
    [fkAuditTypeId]           INT          NOT NULL,
    [Entity]                  XML          NOT NULL,
    [Username]                VARCHAR (50) NOT NULL,
    [DateStamp]               DATETIME     CONSTRAINT [DF_Audit_tblConcessionCash_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionCash] PRIMARY KEY CLUSTERED ([pkAuditConcessionCashId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionCash_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

