CREATE TABLE [Audit].[tblBusinesOnlineTransactionType] (
    [pkAuditBusinesOnlineTransactionTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [pkBusinesOnlineTransactionTypeId]      INT          NOT NULL,
    [fkAuditTypeId]                         INT          NOT NULL,
    [Entity]                                XML          NOT NULL,
    [Username]                              VARCHAR (50) NOT NULL,
    [DateStamp]                             DATETIME     CONSTRAINT [DF_Audit_tblBusinesOnlineTransactionType_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblBusinesOnlineTransactionType] PRIMARY KEY CLUSTERED ([pkAuditBusinesOnlineTransactionTypeId] ASC),
    CONSTRAINT [FK_Audit_tblBusinesOnlineTransactionType_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

