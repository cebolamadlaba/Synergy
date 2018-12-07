CREATE TABLE [Audit].[tblConcessionDetail] (
    [pkAuditpkConcessionDetailId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionDetailId]        INT          NOT NULL,
    [fkAuditTypeId]               INT          NOT NULL,
    [Entity]                      XML          NOT NULL,
    [Username]                    VARCHAR (50) NOT NULL,
    [DateStamp]                   DATETIME     CONSTRAINT [DF_Audit_tblConcessionDetail_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionDetail] PRIMARY KEY CLUSTERED ([pkAuditpkConcessionDetailId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionDetail_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

