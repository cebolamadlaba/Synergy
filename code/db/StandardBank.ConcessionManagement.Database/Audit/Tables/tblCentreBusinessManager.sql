CREATE TABLE [Audit].[tblCentreBusinessManager] (
    [pkAuditCentreBusinessManagerId] INT          IDENTITY (1, 1) NOT NULL,
    [pkCentreBusinessManagerId]      INT          NOT NULL,
    [fkAuditTypeId]                  INT          NOT NULL,
    [Entity]                         XML          NOT NULL,
    [Username]                       VARCHAR (50) NOT NULL,
    [DateStamp]                      DATETIME     CONSTRAINT [DF_Audit_tblCentreBusinessManager_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblCentreBusinessManager] PRIMARY KEY CLUSTERED ([pkAuditCentreBusinessManagerId] ASC),
    CONSTRAINT [FK_Audit_tblCentreBusinessManager_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

