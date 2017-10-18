CREATE TABLE [Audit].[rtblConcessionType] (
    [pkAuditConcessionTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionTypeId]      INT          NOT NULL,
    [fkAuditTypeId]           INT          NOT NULL,
    [Entity]                  XML          NOT NULL,
    [Username]                VARCHAR (50) NOT NULL,
    [DateStamp]               DATETIME     CONSTRAINT [DF_Audit_rtblConcessionType_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblConcessionType] PRIMARY KEY CLUSTERED ([pkAuditConcessionTypeId] ASC),
    CONSTRAINT [FK_Audit_rtblConcessionType_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

