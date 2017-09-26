CREATE TABLE [Audit].[rtblTransactionType] (
    [pkAuditTransactionTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [pkTransactionTypeId]      INT          NOT NULL,
    [fkAuditTypeId]            INT          NOT NULL,
    [Entity]                   XML          NOT NULL,
    [Username]                 VARCHAR (50) NOT NULL,
    [DateStamp]                DATETIME     CONSTRAINT [DF_Audit_rtblTransactionType_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblTransactionType] PRIMARY KEY CLUSTERED ([pkAuditTransactionTypeId] ASC),
    CONSTRAINT [FK_Audit_rtblTransactionType_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

