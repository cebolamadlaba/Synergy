CREATE TABLE [Audit].[tblConcessionMas] (
    [pkAuditConcessionMasId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionMasId]      INT          NOT NULL,
    [fkAuditTypeId]          INT          NOT NULL,
    [Entity]                 XML          NOT NULL,
    [Username]               VARCHAR (50) NOT NULL,
    [DateStamp]              DATETIME     CONSTRAINT [DF_Audit_tblConcessionMas_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionMas] PRIMARY KEY CLUSTERED ([pkAuditConcessionMasId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionMas_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

