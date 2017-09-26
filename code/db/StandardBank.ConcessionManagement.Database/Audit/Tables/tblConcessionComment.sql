CREATE TABLE [Audit].[tblConcessionComment] (
    [pkAuditConcessionCommentId] INT          IDENTITY (1, 1) NOT NULL,
    [pkConcessionCommentId]      INT          NOT NULL,
    [fkAuditTypeId]              INT          NOT NULL,
    [Entity]                     XML          NOT NULL,
    [Username]                   VARCHAR (50) NOT NULL,
    [DateStamp]                  DATETIME     CONSTRAINT [DF_Audit_tblConcessionComment_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblConcessionComment] PRIMARY KEY CLUSTERED ([pkAuditConcessionCommentId] ASC),
    CONSTRAINT [FK_Audit_tblConcessionComment_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

