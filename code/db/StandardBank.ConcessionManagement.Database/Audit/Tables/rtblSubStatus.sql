CREATE TABLE [Audit].[rtblSubStatus] (
    [pkAuditSubStatusId] INT          IDENTITY (1, 1) NOT NULL,
    [pkSubStatusId]      INT          NOT NULL,
    [fkAuditTypeId]      INT          NOT NULL,
    [Entity]             XML          NOT NULL,
    [Username]           VARCHAR (50) NOT NULL,
    [DateStamp]          DATETIME     CONSTRAINT [DF_Audit_rtblSubStatus_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblSubStatus] PRIMARY KEY CLUSTERED ([pkAuditSubStatusId] ASC),
    CONSTRAINT [FK_Audit_rtblSubStatus_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

