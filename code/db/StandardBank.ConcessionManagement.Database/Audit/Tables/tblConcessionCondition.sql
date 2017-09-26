CREATE TABLE [Audit].[tblConcessionCondition] (
    [pkAuditConcessionConditionId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionConditionId]      INT          NOT NULL,
    [fkAuditTypeId]                INT          NOT NULL,
    [Entity]                       XML          NOT NULL,
    [Username]                     VARCHAR (50) NOT NULL,
    [DateStamp]                    DATETIME     CONSTRAINT [DF_Audit_tblConcessionCondition_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionCondition] PRIMARY KEY CLUSTERED ([pkAuditConcessionConditionId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionCondition_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

