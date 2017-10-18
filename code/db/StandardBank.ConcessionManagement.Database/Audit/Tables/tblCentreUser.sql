CREATE TABLE [Audit].[tblCentreUser] (
    [pkAuditCentreUserId] INT          IDENTITY (1, 1) NOT NULL,
    [pkCentreUserId]      INT          NOT NULL,
    [fkAuditTypeId]       INT          NOT NULL,
    [Entity]              XML          NOT NULL,
    [Username]            VARCHAR (50) NOT NULL,
    [DateStamp]           DATETIME     CONSTRAINT [DF_Audit_tblCentreUser_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblCentreUser] PRIMARY KEY CLUSTERED ([pkAuditCentreUserId] ASC),
    CONSTRAINT [FK_Audit_tblCentreUser_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

