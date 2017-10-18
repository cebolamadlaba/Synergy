CREATE TABLE [Audit].[tblConcessionRemovalRequest] (
    [pkAuditConcessionRemovalRequestId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionRemovalRequestId]      INT          NOT NULL,
    [fkAuditTypeId]                     INT          NOT NULL,
    [Entity]                            XML          NOT NULL,
    [Username]                          VARCHAR (50) NOT NULL,
    [DateStamp]                         DATETIME     CONSTRAINT [DF_Audit_tblConcessionRemovalRequest_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionRemovalRequest] PRIMARY KEY CLUSTERED ([pkAuditConcessionRemovalRequestId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionRemovalRequest_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

