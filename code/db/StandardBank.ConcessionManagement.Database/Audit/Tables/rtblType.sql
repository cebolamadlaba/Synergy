CREATE TABLE [Audit].[rtblType] (
    [pkAuditTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [pkTypeId]      INT          NOT NULL,
    [fkAuditTypeId] INT          NOT NULL,
    [Entity]        XML          NOT NULL,
    [Username]      VARCHAR (50) NOT NULL,
    [DateStamp]     DATETIME     CONSTRAINT [DF_Audit_rtblType_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblType] PRIMARY KEY CLUSTERED ([pkAuditTypeId] ASC),
    CONSTRAINT [FK_Audit_rtblType_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

