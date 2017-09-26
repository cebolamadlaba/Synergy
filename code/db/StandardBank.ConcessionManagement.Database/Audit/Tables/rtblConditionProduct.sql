CREATE TABLE [Audit].[rtblConditionProduct] (
    [pkAuditConditionProductId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConditionProductId]      INT          NOT NULL,
    [fkAuditTypeId]             INT          NOT NULL,
    [Entity]                    XML          NOT NULL,
    [Username]                  VARCHAR (50) NOT NULL,
    [DateStamp]                 DATETIME     CONSTRAINT [DF_Audit_rtblConditionProduct_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblConditionProduct] PRIMARY KEY CLUSTERED ([pkAuditConditionProductId] ASC),
    CONSTRAINT [FK_Audit_rtblConditionProduct_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

