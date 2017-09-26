CREATE TABLE [Audit].[tblConcessionAccount] (
    [pkAuditConcessionAccountId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionAccountId]      INT          NOT NULL,
    [fkAuditTypeId]              INT          NOT NULL,
    [Entity]                     XML          NOT NULL,
    [Username]                   VARCHAR (50) NOT NULL,
    [DateStamp]                  DATETIME     CONSTRAINT [DF_Audit_tblConcessionAccount_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionAccount] PRIMARY KEY CLUSTERED ([pkAuditConcessionAccountId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionAccount_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

