CREATE TABLE [Audit].[rtblConditionType] (
    [pkAuditConditionTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConditionTypeId]      INT          NOT NULL,
    [fkAuditTypeId]          INT          NOT NULL,
    [Entity]                 XML          NOT NULL,
    [Username]               VARCHAR (50) NOT NULL,
    [DateStamp]              DATETIME     CONSTRAINT [DF_Audit_rtblConditionType_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblConditionType] PRIMARY KEY CLUSTERED ([pkAuditConditionTypeId] ASC),
    CONSTRAINT [FK_Audit_rtblConditionType_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

