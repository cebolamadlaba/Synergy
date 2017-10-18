CREATE TABLE [Audit].[tblConcessionBol] (
    [pkAuditConcessionBolId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionBolId]      INT          NOT NULL,
    [fkAuditTypeId]          INT          NOT NULL,
    [Entity]                 XML          NOT NULL,
    [Username]               VARCHAR (50) NOT NULL,
    [DateStamp]              DATETIME     CONSTRAINT [DF_Audit_tblConcessionBol_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionBol] PRIMARY KEY CLUSTERED ([pkAuditConcessionBolId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionBol_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

