CREATE TABLE [Audit].[rtblPeriod] (
    [pkAuditPeriodId] INT          IDENTITY (1, 1) NOT NULL,
    [pkPeriodId]      INT          NOT NULL,
    [fkAuditTypeId]   INT          NOT NULL,
    [Entity]          XML          NOT NULL,
    [Username]        VARCHAR (50) NOT NULL,
    [DateStamp]       DATETIME     CONSTRAINT [DF_Audit_rtblPeriod_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblPeriod] PRIMARY KEY CLUSTERED ([pkAuditPeriodId] ASC),
    CONSTRAINT [FK_Audit_rtblPeriod_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

