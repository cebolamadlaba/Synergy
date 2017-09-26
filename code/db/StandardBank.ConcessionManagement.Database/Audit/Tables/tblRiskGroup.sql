CREATE TABLE [Audit].[tblRiskGroup] (
    [pkAuditRiskGroupId] INT          IDENTITY (1, 1) NOT NULL,
    [pkRiskGroupId]      INT          NOT NULL,
    [fkAuditTypeId]      INT          NOT NULL,
    [Entity]             XML          NOT NULL,
    [Username]           VARCHAR (50) NOT NULL,
    [DateStamp]          DATETIME     CONSTRAINT [DF_Audit_tblRiskGroup_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblRiskGroup] PRIMARY KEY CLUSTERED ([pkAuditRiskGroupId] ASC),
    CONSTRAINT [FK_Audit_tblRiskGroup_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

