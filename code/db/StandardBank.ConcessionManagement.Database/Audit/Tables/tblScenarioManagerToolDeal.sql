CREATE TABLE [Audit].[tblScenarioManagerToolDeal] (
    [pkAuditScenarioManagerToolDealId] INT          IDENTITY (1, 1) NOT NULL,
    [pkScenarioManagerToolDealId]      INT          NOT NULL,
    [fkAuditTypeId]                    INT          NOT NULL,
    [Entity]                           XML          NOT NULL,
    [Username]                         VARCHAR (50) NOT NULL,
    [DateStamp]                        DATETIME     CONSTRAINT [DF_Audit_tblScenarioManagerToolDeal_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblScenarioManagerToolDeal] PRIMARY KEY CLUSTERED ([pkAuditScenarioManagerToolDealId] ASC),
    CONSTRAINT [FK_Audit_tblScenarioManagerToolDeal_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

