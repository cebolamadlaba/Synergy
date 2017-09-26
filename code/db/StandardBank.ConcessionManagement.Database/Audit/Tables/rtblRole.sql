CREATE TABLE [Audit].[rtblRole] (
    [pkAuditRoleId] INT          IDENTITY (1, 1) NOT NULL,
    [pkRoleId]      INT          NOT NULL,
    [fkAuditTypeId] INT          NOT NULL,
    [Entity]        XML          NOT NULL,
    [Username]      VARCHAR (50) NOT NULL,
    [DateStamp]     DATETIME     CONSTRAINT [DF_Audit_rtblRole_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblRole] PRIMARY KEY CLUSTERED ([pkAuditRoleId] ASC),
    CONSTRAINT [FK_Audit_rtblRole_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

