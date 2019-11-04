CREATE TABLE [Audit].[tblConcessionGlms] (
    [pkAuditConcessionGlmsId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionGlmsId]      INT          NOT NULL,
    [fkAuditTypeId]           INT          NOT NULL,
    [Entity]                  XML          NOT NULL,
    [Username]                VARCHAR (50) NOT NULL,
    [DateStamp]               DATETIME     CONSTRAINT [DF_Audit_tblConcessionGlms_DateStamp] DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([pkAuditConcessionGlmsId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionGlms_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

