CREATE TABLE [Audit].[tblConcessionTrade] (
    [pkAuditConcessionTradeId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionTradeId]      INT          NOT NULL,
    [fkAuditTypeId]            INT          NOT NULL,
    [Entity]                   XML          NOT NULL,
    [Username]                 VARCHAR (50) NOT NULL,
    [DateStamp]                DATETIME     CONSTRAINT [DF_Audit_tblConcessionTrade_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionTrade] PRIMARY KEY CLUSTERED ([pkAuditConcessionTradeId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionTrade_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

