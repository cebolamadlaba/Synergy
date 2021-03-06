CREATE TABLE [Audit].[tblCentre] (
    [pkAuditCentreId] INT          IDENTITY (1, 1) NOT NULL,
    [pkCentreId]      INT          NOT NULL,
    [fkAuditTypeId]   INT          NOT NULL,
    [Entity]          XML          NOT NULL,
    [Username]        VARCHAR (50) NOT NULL,
    [DateStamp]       DATETIME     CONSTRAINT [DF_Audit_tblCentre_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblCentre] PRIMARY KEY CLUSTERED ([pkAuditCentreId] ASC),
    CONSTRAINT [FK_Audit_tblCentre_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

