CREATE TABLE [Audit].[tblConcession] (
    [pkAuditConcessionId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionId]      INT          NOT NULL,
    [fkAuditTypeId]       INT          NOT NULL,
    [Entity]              XML          NOT NULL,
    [Username]            VARCHAR (50) NOT NULL,
    [DateStamp]           DATETIME     CONSTRAINT [DF_Audit_tblConcession_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcession] PRIMARY KEY CLUSTERED ([pkAuditConcessionId] ASC),
    CONSTRAINT [FK_Audit_tblConcession_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

