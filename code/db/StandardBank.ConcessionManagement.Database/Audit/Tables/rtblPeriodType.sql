CREATE TABLE [Audit].[rtblPeriodType] (
    [pkAuditPeriodTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [pkPeriodTypeId]      INT          NOT NULL,
    [fkAuditTypeId]       INT          NOT NULL,
    [Entity]              XML          NOT NULL,
    [Username]            VARCHAR (50) NOT NULL,
    [DateStamp]           DATETIME     CONSTRAINT [DF_Audit_rtblPeriodType_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblPeriodType] PRIMARY KEY CLUSTERED ([pkAuditPeriodTypeId] ASC),
    CONSTRAINT [FK_Audit_rtblPeriodType_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

