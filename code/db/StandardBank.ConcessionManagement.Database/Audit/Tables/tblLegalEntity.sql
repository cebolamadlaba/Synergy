CREATE TABLE [Audit].[tblLegalEntity] (
    [pkAuditLegalEntityId] INT          IDENTITY (1, 1) NOT NULL,
    [pkLegalEntityId]      INT          NOT NULL,
    [fkAuditTypeId]        INT          NOT NULL,
    [Entity]               XML          NOT NULL,
    [Username]             VARCHAR (50) NOT NULL,
    [DateStamp]            DATETIME     CONSTRAINT [DF_Audit_tblLegalEntity_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblLegalEntity] PRIMARY KEY CLUSTERED ([pkAuditLegalEntityId] ASC),
    CONSTRAINT [FK_Audit_tblLegalEntity_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

