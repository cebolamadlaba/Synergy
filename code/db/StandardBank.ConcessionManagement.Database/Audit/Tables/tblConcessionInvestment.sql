CREATE TABLE [Audit].[tblConcessionInvestment] (
    [pkAuditConcessionInvestmentId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionInvestmentId]      INT          NOT NULL,
    [fkAuditTypeId]                 INT          NOT NULL,
    [Entity]                        XML          NOT NULL,
    [Username]                      VARCHAR (50) NOT NULL,
    [DateStamp]                     DATETIME     CONSTRAINT [DF_Audit_tblConcessionInvestment_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionInvestment] PRIMARY KEY CLUSTERED ([pkAuditConcessionInvestmentId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionInvestment_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

