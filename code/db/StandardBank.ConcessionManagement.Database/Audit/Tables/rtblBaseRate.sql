CREATE TABLE [Audit].[rtblBaseRate] (
    [pkAuditBaseRateId] INT          IDENTITY (1, 1) NOT NULL,
    [pkBaseRateId]      INT          NOT NULL,
    [fkAuditTypeId]     INT          NOT NULL,
    [Entity]            XML          NOT NULL,
    [Username]          VARCHAR (50) NOT NULL,
    [DateStamp]         DATETIME     CONSTRAINT [DF_Audit_rtblBaseRate_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblBaseRate] PRIMARY KEY CLUSTERED ([pkAuditBaseRateId] ASC),
    CONSTRAINT [FK_Audit_rtblBaseRate_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

