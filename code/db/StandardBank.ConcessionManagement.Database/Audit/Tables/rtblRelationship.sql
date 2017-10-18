CREATE TABLE [Audit].[rtblRelationship] (
    [pkAuditRelationshipId] INT          IDENTITY (1, 1) NOT NULL,
    [pkRelationshipId]      INT          NOT NULL,
    [fkAuditTypeId]         INT          NOT NULL,
    [Entity]                XML          NOT NULL,
    [Username]              VARCHAR (50) NOT NULL,
    [DateStamp]             DATETIME     CONSTRAINT [DF_Audit_rtblRelationship_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblRelationship] PRIMARY KEY CLUSTERED ([pkAuditRelationshipId] ASC),
    CONSTRAINT [FK_Audit_rtblRelationship_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

