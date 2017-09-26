CREATE TABLE [Audit].[rtblTransactionGroup] (
    [pkAuditTransactionGroupId] INT          IDENTITY (1, 1) NOT NULL,
    [pkTransactionGroupId]      INT          NOT NULL,
    [fkAuditTypeId]             INT          NOT NULL,
    [Entity]                    XML          NOT NULL,
    [Username]                  VARCHAR (50) NOT NULL,
    [DateStamp]                 DATETIME     CONSTRAINT [DF_Audit_rtblTransactionGroup_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblTransactionGroup] PRIMARY KEY CLUSTERED ([pkAuditTransactionGroupId] ASC),
    CONSTRAINT [FK_Audit_rtblTransactionGroup_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

