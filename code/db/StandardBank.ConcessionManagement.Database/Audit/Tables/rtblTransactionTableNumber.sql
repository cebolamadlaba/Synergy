CREATE TABLE [Audit].[rtblTransactionTableNumber] (
    [pkAuditTransactionTableNumberId] INT          IDENTITY (1, 1) NOT NULL,
    [pkTransactionTableNumberId]      INT          NOT NULL,
    [fkAuditTypeId]                   INT          NOT NULL,
    [Entity]                          XML          NOT NULL,
    [Username]                        VARCHAR (50) NOT NULL,
    [DateStamp]                       DATETIME     CONSTRAINT [DF_Audit_rtblTransactionTableNumber_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblTransactionTableNumber] PRIMARY KEY CLUSTERED ([pkAuditTransactionTableNumberId] ASC),
    CONSTRAINT [FK_Audit_rtblTransactionTableNumber_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

