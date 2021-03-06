CREATE TABLE [Audit].[tblConcessionTransactional] (
    [pkAuditConcessionTransactionalId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionTransactionalId]      INT          NOT NULL,
    [fkAuditTypeId]                    INT          NOT NULL,
    [Entity]                           XML          NOT NULL,
    [Username]                         VARCHAR (50) NOT NULL,
    [DateStamp]                        DATETIME     CONSTRAINT [DF_Audit_tblConcessionTransactional_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionTransactional] PRIMARY KEY CLUSTERED ([pkAuditConcessionTransactionalId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionTransactional_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

