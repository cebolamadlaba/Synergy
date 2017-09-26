CREATE TABLE [Audit].[rtblTableNumber] (
    [pkAuditTableNumberId] INT          IDENTITY (1, 1) NOT NULL,
    [pkTableNumberId]      INT          NOT NULL,
    [fkAuditTypeId]        INT          NOT NULL,
    [Entity]               XML          NOT NULL,
    [Username]             VARCHAR (50) NOT NULL,
    [DateStamp]            DATETIME     CONSTRAINT [DF_Audit_rtblTableNumber_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblTableNumber] PRIMARY KEY CLUSTERED ([pkAuditTableNumberId] ASC),
    CONSTRAINT [FK_Audit_rtblTableNumber_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

