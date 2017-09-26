CREATE TABLE [Audit].[rtblAdValorem] (
    [pkAuditAdValoremId] INT          IDENTITY (1, 1) NOT NULL,
    [pkAdValoremId]      INT          NOT NULL,
    [fkAuditTypeId]      INT          NOT NULL,
    [Entity]             XML          NOT NULL,
    [Username]           VARCHAR (50) NOT NULL,
    [DateStamp]          DATETIME     CONSTRAINT [DF_Audit_rtblAdValorem_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblAdValorem] PRIMARY KEY CLUSTERED ([pkAuditAdValoremId] ASC),
    CONSTRAINT [FK_Audit_rtblAdValorem_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

