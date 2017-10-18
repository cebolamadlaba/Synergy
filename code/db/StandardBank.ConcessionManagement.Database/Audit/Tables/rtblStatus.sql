CREATE TABLE [Audit].[rtblStatus] (
    [pkAuditStatusId] INT          IDENTITY (1, 1) NOT NULL,
    [pkStatusId]      INT          NOT NULL,
    [fkAuditTypeId]   INT          NOT NULL,
    [Entity]          XML          NOT NULL,
    [Username]        VARCHAR (50) NOT NULL,
    [DateStamp]       DATETIME     CONSTRAINT [DF_Audit_rtblStatus_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblStatus] PRIMARY KEY CLUSTERED ([pkAuditStatusId] ASC),
    CONSTRAINT [FK_Audit_rtblStatus_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

