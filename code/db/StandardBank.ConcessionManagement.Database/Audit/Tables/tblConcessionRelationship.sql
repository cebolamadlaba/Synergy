CREATE TABLE [Audit].[tblConcessionRelationship] (
    [pkAuditConcessionRelationshipId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionRelationshipId]      INT          NOT NULL,
    [fkAuditTypeId]                   INT          NOT NULL,
    [Entity]                          XML          NOT NULL,
    [Username]                        VARCHAR (50) NOT NULL,
    [DateStamp]                       DATETIME     CONSTRAINT [DF_Audit_tblConcessionRelationship_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionRelationship] PRIMARY KEY CLUSTERED ([pkAuditConcessionRelationshipId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionRelationship_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

