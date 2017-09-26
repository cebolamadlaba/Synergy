CREATE TABLE [Audit].[tblLegalEntityAccount] (
    [pkAuditLegalEntityAccountId] INT          IDENTITY (1, 1) NOT NULL,
    [pkLegalEntityAccountId]      INT          NOT NULL,
    [fkAuditTypeId]               INT          NOT NULL,
    [Entity]                      XML          NOT NULL,
    [Username]                    VARCHAR (50) NOT NULL,
    [DateStamp]                   DATETIME     CONSTRAINT [DF_Audit_tblLegalEntityAccount_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblLegalEntityAccount] PRIMARY KEY CLUSTERED ([pkAuditLegalEntityAccountId] ASC),
    CONSTRAINT [FK_Audit_tblLegalEntityAccount_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

