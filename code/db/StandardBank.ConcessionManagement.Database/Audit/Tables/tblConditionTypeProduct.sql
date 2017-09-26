CREATE TABLE [Audit].[tblConditionTypeProduct] (
    [pkAuditConditionTypeProductId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConditionTypeProductId]      INT          NOT NULL,
    [fkAuditTypeId]                 INT          NOT NULL,
    [Entity]                        XML          NOT NULL,
    [Username]                      VARCHAR (50) NOT NULL,
    [DateStamp]                     DATETIME     CONSTRAINT [DF_Audit_tblConditionTypeProduct_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConditionTypeProduct] PRIMARY KEY CLUSTERED ([pkAuditConditionTypeProductId] ASC),
    CONSTRAINT [FK_Audit_tblConditionTypeProduct_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

