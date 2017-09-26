CREATE TABLE [Audit].[tblConcessionLending] (
    [pkAuditConcessionLendingId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionLendingId]      INT          NOT NULL,
    [fkAuditTypeId]              INT          NOT NULL,
    [Entity]                     XML          NOT NULL,
    [Username]                   VARCHAR (50) NOT NULL,
    [DateStamp]                  DATETIME     CONSTRAINT [DF_Audit_tblConcessionLending_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionLending] PRIMARY KEY CLUSTERED ([pkAuditConcessionLendingId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionLending_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

