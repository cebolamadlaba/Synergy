CREATE TABLE [Audit].[rtblAccrualType] (
    [pkAuditAccrualTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [pkAccrualTypeId]      INT          NOT NULL,
    [fkAuditTypeId]        INT          NOT NULL,
    [Entity]               XML          NOT NULL,
    [Username]             VARCHAR (50) NOT NULL,
    [DateStamp]            DATETIME     NOT NULL,
    CONSTRAINT [PK_Audit_rtblAccrualType] PRIMARY KEY CLUSTERED ([pkAuditAccrualTypeId] ASC),
    CONSTRAINT [FK_Audit_rtblAccrualType_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);



